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
    Panel1: TPanel;
    ViewerImage: TImage;
    txtImageToLoad: TEdit;
    cmdSobel: TButton;
    txtImageToSave: TEdit;
    txtInfo: TEdit;
    procedure cmdTestClick(Sender: TObject);
  private
  public
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}


procedure TForm1.cmdTestClick(Sender: TObject);
var
  images : PArrayOfPFBitmap32;
  imageToLoad :  array[0..255] of char;
  filterImageLoader, filterImageSaver, filterSobel : Integer;
  imageSobel : PFBitmap32;
  i : Integer;
begin
  txtInfo.Text := 'Begin...';
  Application.ProcessMessages();
  for i:=0 to 10000 do begin
    // init
    initialize();
    txtVersion.Text := StrPas( getVersion() );
    filterImageLoader := createFilter( 'filterImageLoader' );
    filterImageSaver := createFilter( 'filterImageSaver' );
    filterSobel := createFilter( 'filterSobel' );
    imageSobel := nil;
    // load image
    StrPCopy( imageToLoad, txtImageToLoad.Text );
    setParameterString( filterImageLoader, 'filesName', imageToLoad );
    run( filterImageLoader );
    images := getOutputImages( filterImageLoader, 'outImages' );
    // show
    if (images<>nil) and (Length(images^)>0) then begin
      //imageIOVideo.showImage( images^[0], ViewerImage );
    end;
    // sobel
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
      //imageIOVideo.showImage( imageSobel, ViewerImage );
    end;
    // dispose
    image.freeImage( imageSobel );
    deleteFilter( filterSobel );
    deleteFilter( filterImageSaver );
    deleteFilter( filterImageLoader );
    unInitialize();
  end;
  txtInfo.Text := 'End.';
end;


end.
