unit main;

interface
uses
  image;

type
  TFiltersPlugin = class(TObject)
  protected
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
  unsetRegionOfInterest();
end;

destructor TFiltersPlugin.Destroy;
begin
end;

procedure TFiltersPlugin.run();
begin
end;

procedure TFiltersPlugin.runCommand( const aCommand : String );
begin
  run();
end;

function TFiltersPlugin.getParametersCount() : Integer;
begin
  Result := 0; 
end;

procedure TFiltersPlugin.getParameterName( const aIndex : Integer; var aName : String );
begin
end;

procedure TFiltersPlugin.getParameterHelp( const aIndex : Integer; var aHelp : String );
begin
end;

procedure TFiltersPlugin.setParameterBoolean( const aName : String;  const aValue : Boolean );
begin
end;

procedure TFiltersPlugin.setParameterInteger( const aName : String; const aValue : Int64 );
begin
end;

procedure TFiltersPlugin.setParameterFloat( const aName : String; const aValue : Single );
begin
end;

procedure TFiltersPlugin.setParameterString( const aName : String; const aValue : String );
begin
end;

procedure TFiltersPlugin.setParameterImage( const aName : String; const aImage : PFBitmap32 );
begin
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
