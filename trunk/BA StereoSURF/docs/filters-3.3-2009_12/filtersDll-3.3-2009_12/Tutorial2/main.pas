unit main;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls,
  wrapper_filtersdll, image, imageIOVideo;

type
  TForm1 = class(TForm)
    txtVersion: TEdit;
    Label1: TLabel;
    cmdLoad: TButton;
    Panel1: TPanel;
    ViewerImage: TImage;
    txtImageToLoad: TEdit;
    cmdSobel: TButton;
    txtImageToSave: TEdit;
    cmdSave: TButton;
    procedure FormCreate(Sender: TObject);
    procedure cmdLoadClick(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure cmdSobelClick(Sender: TObject);
    procedure cmdSaveClick(Sender: TObject);
  private
    filterImageLoader, filterImageSaver, filterSobel : Integer;
    imageSobel : PFBitmap32;
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
  filterImageSaver := createFilter( 'filterImageSaver' );
  filterSobel := createFilter( 'filterSobel' );
  imageSobel := nil;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  image.freeImage( imageSobel );
  deleteFilter( filterSobel );
  deleteFilter( filterImageSaver );
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

procedure TForm1.cmdSobelClick(Sender: TObject);
var
  images : PArrayOfPFBitmap32;
begin
  images := getOutputImages( filterImageLoader, 'outImages' );
  if (images<>nil) and (Length(images^)>0) then begin
    image.freeImage( imageSobel );
    imageSobel := image.createImageLike( images^[0] );
    setParameterImage( filterSobel, 'inImage', images^[0] );
    setParameterImage( filterSobel, 'outImage', imageSobel );
    setParameterInteger( filterSobel, 'blurIteration', 2 );
    setParameterInteger( filterSobel, 'gain', 10 );
    setParameterInteger( filterSobel, 'thresholdLower', 1 );
    setParameterInteger( filterSobel, 'thresholdUpper', 255 );
    run( filterSobel );
    imageIOVideo.showImage( imageSobel, ViewerImage );
  end;
end;

procedure TForm1.cmdSaveClick(Sender: TObject);
var
  imageToSave :  array[0..255] of char;
begin
  if imageSobel<>nil then begin
    StrPCopy( imageToSave, txtImageToSave.Text );
    setParameterString( filterImageSaver, 'fileName', imageToSave );
    setParameterImage( filterImageSaver, 'inImage', imageSobel );
    run( filterImageSaver );
  end;
end;

end.
