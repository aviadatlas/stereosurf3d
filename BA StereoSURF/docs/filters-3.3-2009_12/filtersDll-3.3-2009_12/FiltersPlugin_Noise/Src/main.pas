unit main;

interface
uses
  image;

type
  TFiltersPlugin = class(TObject)
  protected
    _inImageParameter : PBitmap32;
    _outImageParameter : PBitmap32;
    _scale : Integer;
    // Region Of Interest
    _roi : TFRect;
  public
    constructor Create;
    destructor Destroy;

    procedure run();
    procedure runCommand( const aCommand : String);

    function getParametersCount() : Integer;
    procedure getParameterName( const aIndex : Integer; var aName : String );
    procedure getParameterHelp( const aIndex : Integer; var aHelp : String );
    procedure setParameterBoolean( const aName : String;  const aValue : Boolean );
    procedure setParameterInteger( const aName : String; const aValue : Int64 );
    procedure setParameterFloat( const aName : String; const aValue : Single );
    procedure setParameterString( const aName : String; const aValue : String );
    procedure setParameterImage( const aName : String; const aImage : PBitmap32 );
    procedure setParameterImagesCount( const aName : String; const aCount : Integer );
    procedure setParameterImagesImageAtIndex( const aName : String; const aImage : PBitmap32; const aIndex : Integer );
    procedure setParameterPointer( const aName : String; const aPointer : Pointer );

    function getOutputsCount() :  Integer;
    procedure getOutputName( const aIndex : Integer; var aName : String );
    function getOutputImage( const aName : String ) : PFBitmap32;
    function getOutputImages( const aName : String ) : PArrayOfPFBitmap32;
    function getOutputInteger( const aName : String ) : Integer;
    function getOutputFloat( const aName : String ) : Single;
    function getOutputArrayPointers( const aName : String ) : PArrayOfPointers;

    procedure setRegionOfInterest( const roi : PFRect );
    procedure unsetRegionOfInterest( );

  end;

var
  FiltersPlugin_Version : String = 'V20061204';


implementation
uses
  SysUtils, Math;

constructor TFiltersPlugin.Create;
begin
  _inImageParameter := nil;
  _outImageParameter := nil;
  _scale := 100;
  unsetRegionOfInterest();
  Randomize();
end;

destructor TFiltersPlugin.Destroy;
begin
end;

// It's a scholar example, and not a good and fast method !
procedure TFiltersPlugin.run();
var
  x, xMin, xMax, y, yMin, yMax : Integer;
  pDest : PColor32Array;
  roi : TFRect;
  r : Integer;
begin
  // if we have input images
  if (_inImageParameter<>nil) and (_outImageParameter<>nil) then begin
    // both input images must be of the same size
    if image.isSameSize( _inImageParameter, _outImageParameter )=True then begin
      // we start by copy the [inImage] to [outImage]
      image.copyImageToImage( _inImageParameter, _outImageParameter );
      // we get the ROI (if ROI=(-1,-1)(-1,-1), then getValidROI return the full image)
      roi := image.getValidROI( _inImageParameter, _roi );
      // for each pixel in the ROI
      xMin := Round( roi.Left );
      xMax := Round( roi.Right );
      yMin := Round( roi.Top );
      yMax := Round( roi.Bottom );
      pDest := _outImageParameter.Bits;
      for y:=yMin to yMax do begin
        for x:=xMin to xMax do begin
          // sometime we add salt and pepper noise
          r := Random(2*_scale);
          if r=0 then begin
            pDest^[0] := clBlack32;
          end else if r=1 then begin
            pDest^[0] := clWhite32;
          end;
          Inc( pDest );
        end;
      end;
    end;
  end;
end;

procedure TFiltersPlugin.runCommand( const aCommand : String );
begin
  run();
end;

function TFiltersPlugin.getParametersCount() : Integer;
begin
  Result := 2;
end;

procedure TFiltersPlugin.getParameterName( const aIndex : Integer; var aName : String );
begin
  case aIndex of
  0 : aName := 'inImage';
  1 : aName := 'outImage';
  2 : aName := 'scale';
  end;
end;

procedure TFiltersPlugin.getParameterHelp( const aIndex : Integer; var aHelp : String );
begin
  case aIndex of
  0 : aHelp := 'the input image';
  1 : aHelp := 'the output image (must be of the same size than [inImage])';
  2 : aHelp := 'if scale=100, then for each pixel, there is 1/scale chance to have noise';
  end;
end;

procedure TFiltersPlugin.setParameterBoolean( const aName : String;  const aValue : Boolean );
begin
end;

procedure TFiltersPlugin.setParameterInteger( const aName : String; const aValue : Int64 );
begin
  if aName='scale' then _scale := aValue;
end;

procedure TFiltersPlugin.setParameterFloat( const aName : String; const aValue : Single );
begin
end;

procedure TFiltersPlugin.setParameterString( const aName : String; const aValue : String );
begin
end;

procedure TFiltersPlugin.setParameterImage( const aName : String; const aImage : PFBitmap32 );
begin
  if aName='inImage' then _inImageParameter := aImage
  else if aName='outImage' then _outImageParameter := aImage;
end;

procedure TFiltersPlugin.setParameterImagesCount( const aName : String; const aCount : Integer );
begin
end;

procedure TFiltersPlugin.setParameterImagesImageAtIndex( const aName : String; const aImage : PBitmap32; const aIndex : Integer );
begin
end;

procedure TFiltersPlugin.setParameterPointer( const aName : String; const aPointer : Pointer );
begin
end;

function TFiltersPlugin.getOutputsCount() :  Integer;
begin
  Result := 0;
end;

procedure TFiltersPlugin.getOutputName( const aIndex : Integer; var aName : String );
begin
end;

function TFiltersPlugin.getOutputImage( const aName : String ) : PFBitmap32;
begin
end;

function TFiltersPlugin.getOutputImages( const aName : String ) : PArrayOfPFBitmap32;
begin
end;

function TFiltersPlugin.getOutputInteger( const aName : String ) : Integer;
begin
end;

function TFiltersPlugin.getOutputFloat( const aName : String ) : Single;
begin
end;

function TFiltersPlugin.getOutputArrayPointers( const aName : String ) : PArrayOfPointers;
begin
end;

procedure TFiltersPlugin.setRegionOfInterest( const roi : PFRect );
begin
  _roi := roi^;
end;

procedure TFiltersPlugin.unsetRegionOfInterest();
begin
  _roi.Left := -1;
  _roi.Top := -1;
  _roi.Right := -1;
  _roi.Bottom := -1;
end;


end.
