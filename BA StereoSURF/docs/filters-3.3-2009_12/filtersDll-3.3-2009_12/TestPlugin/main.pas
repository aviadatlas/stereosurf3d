unit main;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls,
  wrapper_filtersdll, image, imageIOVideo, Spin;

type
  TForm1 = class(TForm)
    txtVersion: TEdit;
    Label1: TLabel;
    cmdLoad: TButton;
    Panel1: TPanel;
    ViewerImage: TImage;
    txtImageToLoad: TEdit;
    cmdRun: TButton;
    Label2: TLabel;
    seScale: TSpinEdit;
    procedure FormCreate(Sender: TObject);
    procedure cmdLoadClick(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure cmdRunClick(Sender: TObject);
    procedure seScaleChange(Sender: TObject);
  private
    filterImageLoader : Integer;
    filtersPlugin_Noise : Integer;
    imageWithNoise : PFBitmap32;
  public
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}


procedure TForm1.FormCreate(Sender: TObject);
begin
  initialize();
  txtVersion.Text := StrPas( getVersion() );
  filterImageLoader := createFilter( 'filterImageLoader' );
  filtersPlugin_Noise := createFilter( 'filtersPlugin_Noise' );
  imageWithNoise := nil;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  image.freeImage( imageWithNoise );
  deleteFilter( filtersPlugin_Noise );
  deleteFilter( filterImageLoader );
  unInitialize();
end;

procedure TForm1.cmdLoadClick(Sender: TObject);
var
  images : PArrayOfPFBitmap32;
  imageToLoad :  array[0..255] of char;
begin
  StrPCopy( imageToLoad, txtImageToLoad.Text );
  setParameterString( filterImageLoader, 'filesName', imageToLoad );
  run( filterImageLoader );
  images := getOutputImages( filterImageLoader, 'outImages' );
  if (images<>nil) and (Length(images^)>0) then begin
    imageIOVideo.showImage( images^[0], ViewerImage );
  end;
end;

procedure TForm1.cmdRunClick(Sender: TObject);
var
  images : PArrayOfPFBitmap32;
  scale : Integer;
begin
  if Trim(seScale.Text)<>'' then begin
    scale := seScale.Value;
    images := getOutputImages( filterImageLoader, 'outImages' );
    if (images<>nil) and (Length(images^)>0) then begin
      image.freeImage( imageWithNoise );
      imageWithNoise := image.createImageLike( images^[0] );
      setParameterInteger( filtersPlugin_Noise, 'scale', scale );
      setParameterImage( filtersPlugin_Noise, 'inImage', images^[0] );
      setParameterImage( filtersPlugin_Noise, 'outImage', imageWithNoise );
      run( filtersPlugin_Noise );
      imageIOVideo.showImage( imageWithNoise, ViewerImage );
    end;
  end;
end;

procedure TForm1.seScaleChange(Sender: TObject);
begin
  cmdRun.Click();
end;

end.
