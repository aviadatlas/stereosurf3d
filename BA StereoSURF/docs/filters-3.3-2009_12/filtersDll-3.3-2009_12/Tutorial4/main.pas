unit main;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls,
  wrapper_filtersdll, image, imageIOVideo, Spin, ComCtrls;

type
  TFormMain = class(TForm)
    txtVersion: TEdit;
    Label1: TLabel;
    cmdLoad: TButton;
    Panel1: TPanel;
    ViewerImage: TImage;
    txtImageToLoad: TEdit;
    PageControl: TPageControl;
    TabSheet1: TTabSheet;
    cmdSobel: TButton;
    TabSheet2: TTabSheet;
    cmdCanny: TButton;
    Label2: TLabel;
    Label3: TLabel;
    Canny_threshold1: TSpinEdit;
    Canny_threshold2: TSpinEdit;
    TabSheet3: TTabSheet;
    Label4: TLabel;
    btnBobExplorer: TButton;
    txtPixelAreaMax: TEdit;
    TabSheet4: TTabSheet;
    cmdTestMicrobubbles: TButton;
    Panel2: TPanel;
    txtOutput: TMemo;
    Panel3: TPanel;
    TabSheet5: TTabSheet;
    btnBlobExplorerVector: TButton;
    TabSheet6: TTabSheet;
    btnSubstract: TButton;
    TabSheet7: TTabSheet;
    btnTextureCompute: TButton;
    imageFunctions: TTabSheet;
    btnImageFunctionsTest: TButton;
    btnBlobExplorerSurfcePerimeter: TButton;
    TabSheet8: TTabSheet;
    cmdTestFromTImage: TButton;
    testDelphiTImage: TImage;
    procedure FormCreate(Sender: TObject);
    procedure cmdLoadClick(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure cmdSobelClick(Sender: TObject);
    procedure cmdCannyClick(Sender: TObject);
    procedure Canny_threshold2Change(Sender: TObject);
    procedure Canny_threshold1Change(Sender: TObject);
    procedure btnBobExplorerClick(Sender: TObject);
    procedure cmdTestMicrobubblesClick(Sender: TObject);
    procedure PageControlChange(Sender: TObject);
    procedure btnBlobExplorerVectorClick(Sender: TObject);
    procedure btnSubstractClick(Sender: TObject);
    procedure btnTextureComputeClick(Sender: TObject);
    procedure btnImageFunctionsTestClick(Sender: TObject);
    procedure btnBlobExplorerSurfcePerimeterClick(Sender: TObject);
    procedure cmdTestFromTImageClick(Sender: TObject);
  private
    imageLoaded : PFBitmap32;
    function loadOneImage( filename : PChar ) : PFBitmap32;
  public
  end;

var
  FormMain: TFormMain;

implementation
uses
  Math;

{$R *.dfm}


procedure TFormMain.FormCreate(Sender: TObject);
begin
  initialize();
  txtVersion.Text := StrPas( getVersion() );
  imageLoaded := nil;
end;

procedure TFormMain.FormDestroy(Sender: TObject);
begin
  image.freeImage( imageLoaded );
  unInitialize();
end;

procedure TFormMain.cmdLoadClick(Sender: TObject);
var
  imageToLoad :  array[0..255] of char;
begin
  image.freeImage( imageLoaded );
  StrPCopy( imageToLoad, txtImageToLoad.Text );
  imageLoaded := loadOneImage( imageToLoad );
  if (imageLoaded<>nil) then begin
    imageIOVideo.showImage( imageLoaded, ViewerImage );
  end;
end;

function TFormMain.loadOneImage( filename : PChar ) : PFBitmap32;
var
  filterImageLoader : Integer;
  images : PArrayOfPFBitmap32;
begin
  Result := nil;
  filterImageLoader := createFilter( 'filterImageLoader' );
  setParameterString( filterImageLoader, 'filesName', filename );
  run( filterImageLoader );
  images := getOutputImages( filterImageLoader, 'outImages' );
  if (images<>nil) and (Length(images^)>0) then begin
    Result := image.createImageFromImage( images^[0] );
  end;
  deleteFilter( filterImageLoader );
end;

procedure TFormMain.cmdSobelClick(Sender: TObject);
var
  filterSobel : Integer;
  imageSobel : PFBitmap32;
begin
  cmdLoadClick( Self );
  imageSobel := image.createImageLike( imageLoaded );
  filterSobel := createFilter( 'filterSobel' );
  setParameterImage( filterSobel, 'inImage', imageLoaded );
  setParameterImage( filterSobel, 'outImage', imageSobel );
  setParameterInteger( filterSobel, 'blurIteration', 2 );
  setParameterInteger( filterSobel, 'gain', 10 );
  setParameterInteger( filterSobel, 'thresholdLower', 1 );
  setParameterInteger( filterSobel, 'thresholdUpper', 255 );
  run( filterSobel );
  imageIOVideo.showImage( imageSobel, ViewerImage );
  deleteFilter( filterSobel );
  image.freeImage( imageSobel );
end;

procedure TFormMain.cmdCannyClick(Sender: TObject);
var
  imageCanny : PFBitmap32;
  filterCanny : Integer;
begin
  cmdLoadClick( Self );
  imageCanny := image.createImageLike( imageLoaded );
  filterCanny := createFilter( 'filterCanny' );
  setParameterImage( filterCanny, 'inImage', imageLoaded );
  setParameterImage( filterCanny, 'outImage', imageCanny );
  setParameterInteger( filterCanny, 'threshold1', Canny_threshold1.Value );
  setParameterInteger( filterCanny, 'threshold2', Canny_threshold2.Value );
  run( filterCanny );
  imageIOVideo.showImage( imageCanny, ViewerImage );
  deleteFilter( filterCanny );
  image.freeImage( imageCanny );
end;

procedure TFormMain.Canny_threshold1Change(Sender: TObject);
begin
  cmdCannyClick(self);
end;

procedure TFormMain.Canny_threshold2Change(Sender: TObject);
begin
  cmdCannyClick(self);
end;

procedure TFormMain.btnBobExplorerClick(Sender: TObject);
var
  filterBlobExplorer : Integer;
  imageBlob : PFBitmap32;
  b : Integer;
  pblobs : PArrayOfPointers;
  blob : PBlob;
  pixelAreaMax : Integer;
begin
  cmdLoadClick( Self );
  imageBlob := image.createImageLike( imageLoaded );
  filterBlobExplorer := createFilter( 'filterBlobExplorer' );

  setParameterImage( filterBlobExplorer, 'inImage', imageLoaded );
  setParameterImage( filterBlobExplorer, 'outImage', imageBlob );
  setParameterInteger( filterBlobExplorer, 'intensityBackground', 100 );
  setParameterInteger( filterBlobExplorer, 'intensityPrecision', 40 );
  setParameterString( filterBlobExplorer, 'enableBlobArea', 'TRUE' ); // we keep only blobs of a certain area
  setParameterInteger( filterBlobExplorer, 'blobAreaMin', 100 );
  setParameterInteger( filterBlobExplorer, 'blobAreaMax', 999999 );
  setParameterBoolean( filterBlobExplorer, 'blobSurfaceInfo', True );  // to have pixelArea calculated
  run( filterBlobExplorer );
  imageIOVideo.showImage( imageBlob, ViewerImage );

  // get all extracted blobs
  pixelAreaMax := 0;
  pblobs := getOutputArrayPointers( filterBlobExplorer, 'blobs' );
  for b:=0 to Length( pblobs^ )-1 do begin
    blob := PBlob( pblobs^[b] );
    pixelAreaMax := Max( pixelAreaMax, blob.pixelArea );
  end;
  txtPixelAreaMax.Text := IntToStr( pixelAreaMax );

  deleteFilter( filterBlobExplorer );
  image.freeImage( imageBlob );
end;

procedure TFormMain.cmdTestMicrobubblesClick(Sender: TObject);
var
  filterBlobExplorer, filterThresholdBinary : Integer;
  imageBlob, imageThreshold : PFBitmap32;
begin
  cmdLoadClick( Self );
  filterBlobExplorer := createFilter( 'filterBlobExplorer' );
  filterThresholdBinary := createFilter( 'filterThresholdBinary' );
  imageBlob := image.createImageLike( imageLoaded );
  imageThreshold := image.createImageLike( imageLoaded );

  setParameterImage( filterBlobExplorer, 'inImage', imageLoaded );
  setParameterImage( filterBlobExplorer, 'outImage', imageBlob );
  setParameterInteger( filterBlobExplorer, 'intensityBackground', 255 );
  setParameterInteger( filterBlobExplorer, 'intensityPrecision', 1 );
  setParameterString( filterBlobExplorer, 'enableBlobArea', 'TRUE' );
  setParameterInteger( filterBlobExplorer, 'blobAreaMin', 15 );
  setParameterInteger( filterBlobExplorer, 'blobAreaMax', 999999 );
  setParameterBoolean( filterBlobExplorer, 'blobSurfaceInfo', False );
  run( filterBlobExplorer );

  setParameterImage( filterThresholdBinary, 'inImage', imageBlob );
  setParameterImage( filterThresholdBinary, 'outImage', imageThreshold );
  setParameterInteger( filterThresholdBinary, 'thresholdLower', 0 );
  setParameterInteger( filterThresholdBinary, 'thresholdUpper', 0 );
  setParameterInteger( filterThresholdBinary, 'lowcolor', 0 );
  setParameterInteger( filterThresholdBinary, 'middlecolor', 2 );
  setParameterInteger( filterThresholdBinary, 'highcolor', 0 );
  run( filterThresholdBinary );

  setParameterImage( filterBlobExplorer, 'inImage', imageThreshold );
  setParameterImage( filterBlobExplorer, 'outImage', imageBlob );
  setParameterInteger( filterBlobExplorer, 'intensityBackground', 255 );
  setParameterInteger( filterBlobExplorer, 'intensityPrecision', 1 );
  setParameterString( filterBlobExplorer, 'enableBlobArea', 'FALSE' );
  //setParameterBoolean( filterBlobExplorer, 'blobSurfaceInfo', True );  // to have blob area
  run( filterBlobExplorer );

  imageIOVideo.showImage( imageBlob, ViewerImage );

  deleteFilter( filterThresholdBinary );
  deleteFilter( filterBlobExplorer );
  image.freeImage( imageBlob );
  image.freeImage( imageThreshold );
end;

procedure TFormMain.PageControlChange(Sender: TObject);
begin
  txtImageToLoad.Text := '.\lenna_color.bmp';
  if PageControl.ActivePage.Caption='microbubbles' then txtImageToLoad.Text := '.\microbubbles.tif'
  else if PageControl.ActivePage.Caption='BlobExplorer' then txtImageToLoad.Text := '.\blob2.tif'
  else if PageControl.ActivePage.Caption='BlobExplorer (2)' then txtImageToLoad.Text := '.\SUSAN2.bmp'
  else if PageControl.ActivePage.Caption='Texture' then txtImageToLoad.Text := '.\texture.jpg';
end;

procedure TFormMain.btnBlobExplorerVectorClick(Sender: TObject);
var
  imageToLoad :  array[0..255] of char;
  images : PArrayOfPFBitmap32;
  img : PFBitmap32;
  filterImageLoader : Integer;
  filterBlobExplorer : Integer;
  imageBlob : PFBitmap32;
  blobs : PArrayOfPointers;
  blob : PBlob;
  b, s : Integer;
begin
  txtOutput.Clear();
  cmdLoadClick( Self );
  // with a image already loaded
  if imageLoaded<>nil then begin
    // we use filterBlobExplorer to vectorize
    filterBlobExplorer := createFilter( 'filterBlobExplorer' );
    imageBlob := image.createImageLike( imageLoaded );
    setParameterImage( filterBlobExplorer, 'inImage', imageLoaded );
    setParameterImage( filterBlobExplorer, 'outImage', imageBlob );
    setParameterInteger( filterBlobExplorer, 'intensityBackground', 0 );
    setParameterInteger( filterBlobExplorer, 'intensityPrecision', 5 );  // if the image was already segmented, we can set '1'
    setParameterString( filterBlobExplorer, 'enableBlobArea', 'FALSE' );
    setParameterBoolean( filterBlobExplorer, 'blobSurfaceInfo', False );
    setParameterBoolean( filterBlobExplorer, 'criticalPoints', True );  // we want vectorization
    setParameterInteger( filterBlobExplorer, 'contourCriticalPointsAppoximationAccuracy', 20 );  // we want approximation
    run( filterBlobExplorer );
    imageIOVideo.showImage( imageBlob, ViewerImage );  // we show extracted blobs
    // we get extracted blobs
    blobs := getOutputArrayPointers( filterBlobExplorer, 'blobs' );
    // for each blobs
    for b:=0 to Length( blobs^ )-1 do begin
      blob := blobs^[b];
      txtOutput.Lines.Add('Blob['+IntToStr(b)+'] :');
      // and we get it's vectorized contour
      for s:=0 to Length( blob^.approximatedSegmentList )-1 do begin
        txtOutput.Lines.Add(
          '  (' +
          FloatToStr(blob^.approximatedSegmentList[s].p1.x)+','+FloatToStr(blob^.approximatedSegmentList[s].p1.y) +
          ') -> (' +
          FloatToStr(blob^.approximatedSegmentList[s].p2.x)+','+FloatToStr(blob^.approximatedSegmentList[s].p2.y) +
          ')'
        );
      end;
    end;
    image.freeImage( imageBlob );
    deleteFilter( filterBlobExplorer );
  end;
end;

procedure TFormMain.btnBlobExplorerSurfcePerimeterClick(Sender: TObject);
var
  imageToLoad :  array[0..255] of char;
  images : PArrayOfPFBitmap32;
  img : PFBitmap32;
  filterImageLoader : Integer;
  filterBlobExplorer : Integer;
  imageBlob : PFBitmap32;
  blobs : PArrayOfPointers;
  blob : PBlob;
  b, s : Integer;
begin
  txtOutput.Clear();
  cmdLoadClick( Self );
  // with a image already loaded
  if imageLoaded<>nil then begin
    // we use filterBlobExplorer to vectorize
    filterBlobExplorer := createFilter( 'filterBlobExplorer' );
    imageBlob := image.createImageLike( imageLoaded );
    setParameterImage( filterBlobExplorer, 'inImage', imageLoaded );
    setParameterImage( filterBlobExplorer, 'outImage', imageBlob );
    setParameterInteger( filterBlobExplorer, 'intensityBackground', 0 );
    setParameterInteger( filterBlobExplorer, 'intensityPrecision', 5 );  // if the image was already segmented, we can set '1'
    setParameterString( filterBlobExplorer, 'enableBlobArea', 'FALSE' );
    setParameterBoolean( filterBlobExplorer, 'blobSurfaceInfo', True );  // we want surface info
    setParameterBoolean( filterBlobExplorer, 'criticalPoints', True );   // we want perimeter info, so we need vectorization
    run( filterBlobExplorer );
    imageIOVideo.showImage( imageBlob, ViewerImage );  // we show extracted blobs
    // we get extracted blobs
    blobs := getOutputArrayPointers( filterBlobExplorer, 'blobs' );
    // for each blobs
    for b:=0 to Length( blobs^ )-1 do begin
      blob := blobs^[b];
      txtOutput.Lines.Add('Blob['+IntToStr(b)+'] :');
      // and we get it's surface
      txtOutput.Lines.Add(' pixelArea=[' + FloatToStr(blob^.pixelArea) + '], perimeter=[' + FloatToStr(blob^.perimeter) + ']');
    end;
    image.freeImage( imageBlob );
    deleteFilter( filterBlobExplorer );
  end;
end;

procedure TFormMain.btnSubstractClick(Sender: TObject);
var
  filterBlur, filterArithmeticSubstract : Integer;
  imageToSubstract, imageOut : PFBitmap32;
begin
  cmdLoadClick( Self );
  // with a image already loaded by the function cmdLoadClick
  if imageLoaded<>nil then begin
    // for this example, we create a imageToSubstract wich is the blured imageLoader
    imageToSubstract := image.createImageLike( imageLoaded );
    filterBlur := createFilter( 'filterBlur' );
    setParameterImage( filterBlur, 'inImage', imageLoaded );
    setParameterImage( filterBlur, 'outImage', imageToSubstract );
    setParameterInteger( filterBlur, 'mode', 0 );
    setParameterInteger( filterBlur, 'radius', 100 );
    run( filterBlur );
    deleteFilter( filterBlur );

    // now we will obtain imageOut = imageLoaded - imageToSubstract
    imageOut := image.createImageLike( imageLoaded );
    filterArithmeticSubstract := createFilter( 'filterArithmeticSubstract' );
    setParameterImage( filterArithmeticSubstract, 'inImage1', imageLoaded );
    setParameterImage( filterArithmeticSubstract, 'inImage2', imageToSubstract );
    setParameterImage( filterArithmeticSubstract, 'outImage', imageOut );
    setParameterString( filterArithmeticSubstract, 'mode', 'SUBSTRACT' );  // SUBSTRACT: imageOut=imageLoaded-imageToSubstract; DIFFERENCE: imageOut=Abs(imageLoaded-imageToSubstract)
    run( filterArithmeticSubstract );
    imageIOVideo.showImage( imageOut, ViewerImage );  // we show imageOut
    deleteFilter( filterArithmeticSubstract );

    image.freeImage( imageOut );
    image.freeImage( imageToSubstract );
  end;
end;

procedure TFormMain.btnTextureComputeClick(Sender: TObject);
var
  filterCoOccurrenceMatrix : Integer;
  imageCoOccurrenceMatrix, imageMask : PFBitmap32;
  tmpSingle : Single;
  i, iMax, j, jMax : Integer;
  value : Single;
  features_occurrenceMax : Single;
  features_occurrenceMax_col, features_occurrenceMax_row : Integer;
begin
  txtOutput.Clear();
  // load image to analyse in 'imageLoaded'
  cmdLoadClick( Self );

  // create filter 'filterCoOccurrenceMatrix'
  filterCoOccurrenceMatrix := createFilter( 'filterCoOccurrenceMatrix' );
  // set 'inImage'
  setParameterImage( filterCoOccurrenceMatrix, 'inImage', imageLoaded );
  // we would like to analyse only a part of the image, we use a mask
  // load the mask image
  imageMask := loadOneImage( '.\texture_mask.jpg' );
  // set 'mask'
  setParameterImage( filterCoOccurrenceMatrix, 'mask', imageMask );
  // set pixels to compare : p1(c,r) <-> p2(c+1,r+0)
  setParameterInteger( filterCoOccurrenceMatrix, 'horizontalDistance', 1 );
  setParameterInteger( filterCoOccurrenceMatrix, 'verticalDistance', 0 );
  // set data to compare : p1(0=Intensity) <-> p2(0=Intensity)
  setParameterInteger( filterCoOccurrenceMatrix, 'xAnalyse', 0 );
  setParameterInteger( filterCoOccurrenceMatrix, 'yAnalyse', 0 );
  // set 'scale'
  setParameterFloat( filterCoOccurrenceMatrix, 'scale',  1 );
  // run
  run( filterCoOccurrenceMatrix );

  // show build in texture features computed by the filter
  tmpSingle := getOutputFloat( filterCoOccurrenceMatrix, 'features_angularSecondMoment' );
  txtOutput.Lines.Add( 'features_angularSecondMoment = ' + FloatToStrF(tmpSingle,ffFixed,10,10) );
  tmpSingle := getOutputFloat( filterCoOccurrenceMatrix, 'features_energy' );
  txtOutput.Lines.Add( 'features_energy = ' + FloatToStrF(tmpSingle,ffFixed,10,10) );
  tmpSingle := getOutputFloat( filterCoOccurrenceMatrix, 'features_entropy' );
  txtOutput.Lines.Add( 'features_entropy = ' + FloatToStrF(tmpSingle,ffFixed,10,10) );
  tmpSingle := getOutputFloat( filterCoOccurrenceMatrix, 'features_contrast' );
  txtOutput.Lines.Add( 'features_contrast = ' + FloatToStrF(tmpSingle,ffFixed,10,10) );
  tmpSingle := getOutputFloat( filterCoOccurrenceMatrix, 'features_inverseDifferenceMoment' );
  txtOutput.Lines.Add( 'features_inverseDifferenceMoment = ' + FloatToStrF(tmpSingle,ffFixed,10,10) );

  // now we will compute our own texture features : the mean
  // get 'outImage' (= the co-occurrence matrix) (this image contains float value, not pixels)
  imageCoOccurrenceMatrix := getOutputImage( filterCoOccurrenceMatrix, 'outImage' );
  features_occurrenceMax := -1.0;
  features_occurrenceMax_col := -1;
  features_occurrenceMax_row := -1;
  iMax := imageCoOccurrenceMatrix.Height-1;
  jMax := imageCoOccurrenceMatrix.Width-1;
  for i:=0 to iMax do begin
    for j:=0 to jMax do begin
      value := image.getPixelAsSingle( imageCoOccurrenceMatrix, j, i );
      if value>features_occurrenceMax then begin
        features_occurrenceMax := value;
        features_occurrenceMax_col := j;
        features_occurrenceMax_row := i;
      end;
    end;
  end;
  txtOutput.Lines.Add( 'my feature : features_occurrenceMax = ' + FloatToStrF(features_occurrenceMax,ffFixed,5,10) +
    ', for co-occurrence of intensity [' + IntToStr(features_occurrenceMax_col) + ']' +
    ' and intensity [' + IntToStr(features_occurrenceMax_row) + ']' );

  // delete the filter
  deleteFilter( filterCoOccurrenceMatrix );
  // delete 'imageMask'
  image.freeImage( imageMask );
end;

procedure TFormMain.btnImageFunctionsTestClick(Sender: TObject);
var
  image1, image2 : PFBitmap32;
  color, newColor : TFColor32;
  redChannel : Byte;
  row, col : Integer;
  i : Integer;
	segment : TFSegment;
	r : TFRect;
begin
	image1 := image.createImageTest( 512, 512 );
	// keep only the red channel in the rect (100,100)(200,200)
	for row:=100 to 200 do begin
		for col:=100 to 200 do begin
			color := image.getPixel( image1, col, row );
			redChannel := image.redComponent( color );
			newColor := image.color32( redChannel, 0, 0 );
			image.setPixel( image1, col, row, newColor );
		end;
	end;
	// create image of the same size than image1
	image2 := image.createImageLike( image1 );
	// copy image
	image.copyImageToImage( image1, image2 );
	// draw lines
	image.drawLine( image2, 50, 300, 100, 300, clYellow32, 1 );
	image.drawLine( image2, 50, 350, 100, 350, clAqua32, 5 );
	image.drawLine( image2, 50, 400, 100, 450, clFuchsia32, 1 );
	image.drawLine( image2, 50, 400, 100, 500, clBlue32, 1 );
	segment.p1.x := 10; segment.p1.y := 10; segment.p2.x := 500; segment.p2.y := 10;
	for i:=1 to 10 do begin
		image.drawLine( image2, segment, clWhite32, i );
		segment.p2.y := segment.p2.y + 20;
	end;
	// draw rects
	r.Left := 270; r.Top := 300; r.Right := 410; r.Bottom := 370;
	image.drawRect( image2, r, clWhite32, 1 );
	r.Top := r.Top + 120; r.Bottom := r.Bottom + 120;
	image.drawRectFilled( image2, r, clWhite32 );
	// draw disk
	image.drawDisk( image2, 100.0, 150.0, 50.0, clLime32 );
	// draw disk
	image.drawCircle( image2, 200.0, 300.0, 50.0, clLime32 );
  // show image
  imageIOVideo.showImage( image2, ViewerImage );  // we show imageOut
	// dispose
	image.freeImage( image1 );
	image.freeImage( image2 );
end;

procedure TFormMain.cmdTestFromTImageClick(Sender: TObject);
var
  image : PFBitmap32;
begin
  // load a image with Delphi in a Delphi TImage object
  testDelphiTImage.Picture.LoadFromFile( txtImageToLoad.Text );
  // convert to a Filters image
  //   (this function is slow, so try to use filterImageLoader, filterPlugin_Video or filterPlugin_WebCam)
  image := imageIOVideo.createImageFromBitmap( testDelphiTImage.Picture.Bitmap );
  // show a Filters image in a Delphi TImage
  imageIOVideo.showImage( image, ViewerImage );
end;

end.
