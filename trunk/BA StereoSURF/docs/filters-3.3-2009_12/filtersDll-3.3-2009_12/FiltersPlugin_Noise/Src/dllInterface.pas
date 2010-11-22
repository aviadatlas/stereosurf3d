unit dllInterface;

interface
uses
  classes, image, main;

function getVersion : PChar; stdcall; export;

function createFilter() : Integer; stdcall; export;
procedure deleteFilter(const aHandle : Integer); stdcall; export;

procedure run(const aHandle : Integer); stdcall; export;
procedure runCommand(const aHandle : Integer; const aCommand : PChar); stdcall; export;

function getParametersCount(const aHandle : Integer) : Integer; stdcall; export;
procedure getParameterName(const aHandle : Integer; const aIndex : Integer; aName : PChar ); stdcall; export;
procedure getParameterHelp(const aHandle : Integer; const aIndex : Integer; aName : PChar ); stdcall; export;

procedure setParameterInteger(const aHandle : Integer; const aName : PChar; const aValue : Int64 ); stdcall; export;
procedure setParameterFloat(const aHandle : Integer; const aName : PChar; const aValue : Single ); stdcall; export;
procedure setParameterBoolean(const aHandle : Integer; const aName : PChar; const aValue : Shortint ); stdcall; export;
procedure setParameterString(const aHandle : Integer; const aName : PChar; const aValue : PChar ); stdcall; export;
procedure setParameterImage(const aHandle : Integer; const aName: PChar; const aImage : PFBitmap32 ); stdcall; export;
procedure setParameterImagesCount(const aHandle : Integer; const aName: PChar; const aCount : Integer ); stdcall; export;
procedure setParameterImagesImageAtIndex(const aHandle : Integer; const aName: PChar; const aImage : PFBitmap32; const aIndex : Integer ); stdcall; export;
procedure setParameterPointer(const aHandle : Integer; const aName: PChar; const aPointer : Pointer ); stdcall; export;

function getOutputsCount(const aHandle : Integer) : Integer; stdcall; export;
procedure getOutputName(const aHandle : Integer; const aIndex : Integer; aName : PChar ); stdcall; export;
function getOutputImage(const aHandle : Integer; const aName: PChar) : PFBitmap32; stdcall; export;
function getOutputImagesCount(const aHandle : Integer; const aName: PChar) : Integer; stdcall; export;
function getOutputImagesImageAtIndex(const aHandle : Integer; const aName: PChar; const aIndex : Integer) : PFBitmap32; stdcall; export;
function getOutputInteger(const aHandle : Integer; const aName: PChar) : Integer; stdcall; export;
function getOutputFloat(const aHandle : Integer; const aName: PChar) : Single; stdcall; export;
function getOutputArrayPointersCount(const aHandle : Integer; const aName: PChar) : Integer; stdcall; export;
function getOutputArrayPointersPointerAtIndex(const aHandle : Integer; const aName: PChar; const aIndex : Integer) : Pointer; stdcall; export;

procedure setRegionOfInterest(const aHandle : Integer; const roi : PFRect); stdcall; export;
procedure unsetRegionOfInterest(const aHandle : Integer); stdcall; export;

var
  _cversion : array[0..255] of Char;
  _filtersInstance : TList;
  _i : Integer;

implementation
uses
  SysUtils;


function getVersion : PChar;
begin
  StrPCopy( _cversion, FiltersPlugin_Version );
  Result := _cversion;
end;

function createFilter() : Integer;
var
  handle : Pointer;
begin
  handle := TFiltersPlugin.Create();
  _filtersInstance.Add( handle );
  Result := Integer( handle );
end;

procedure deleteFilter(const aHandle : Integer);
begin
  TFiltersPlugin(aHandle).Free();
  _filtersInstance.Remove( Pointer(aHandle) );
end;

procedure run(const aHandle : Integer);
begin
  TFiltersPlugin(aHandle).run();
end;

procedure runCommand(const aHandle : Integer; const aCommand : PChar);
begin
  TFiltersPlugin(aHandle).runCommand( StrPas(aCommand) );
end;

function getParametersCount(const aHandle : Integer) : Integer;
begin
  Result := TFiltersPlugin(aHandle).getParametersCount();
end;

procedure getParameterName(const aHandle : Integer; const aIndex : Integer; aName : PChar); stdcall; export;
var
  sName : String;
begin
  TFiltersPlugin(aHandle).getParameterName( aIndex, sName );
  StrPCopy( aName, sName );
end;

procedure getParameterHelp(const aHandle : Integer; const aIndex : Integer; aName : PChar); stdcall; export;
var
  sName : String;
begin
  TFiltersPlugin(aHandle).getParameterHelp( aIndex, sName );
  StrPCopy( aName, sName );
end;

procedure setParameterInteger(const aHandle : Integer; const aName : PChar; const aValue : Int64 );
begin
  TFiltersPlugin(aHandle).setParameterInteger( StrPas(aName) , aValue );
end;

procedure setParameterFloat(const aHandle : Integer; const aName : PChar; const aValue : Single );
begin
  TFiltersPlugin(aHandle).setParameterFloat( StrPas(aName) , aValue );
end;

procedure setParameterBoolean(const aHandle : Integer; const aName : PChar; const aValue : Shortint );
begin
  TFiltersPlugin(aHandle).setParameterBoolean( StrPas(aName) , Boolean(aValue) );
end;

procedure setParameterString(const aHandle : Integer; const aName : PChar; const aValue : PChar );
begin
  TFiltersPlugin(aHandle).setParameterString( StrPas(aName) , StrPas(aValue) );
end;

procedure setParameterImage(const aHandle : Integer; const aName: PChar; const aImage: PFBitmap32);
begin
  TFiltersPlugin(aHandle).setParameterImage( StrPas(aName) , aImage );
end;

procedure setParameterImagesCount(const aHandle : Integer; const aName: PChar; const aCount : Integer );
begin
  TFiltersPlugin(aHandle).setParameterImagesCount( StrPas(aName) , aCount );
end;

procedure setParameterImagesImageAtIndex(const aHandle : Integer; const aName: PChar; const aImage: PFBitmap32; const aIndex : Integer );
begin
  TFiltersPlugin(aHandle).setParameterImagesImageAtIndex( StrPas(aName) , aImage, aIndex );
end;

procedure setParameterPointer(const aHandle : Integer; const aName: PChar; const aPointer: Pointer);
begin
  TFiltersPlugin(aHandle).setParameterPointer( StrPas(aName) , aPointer );
end;

function getOutputsCount(const aHandle : Integer) : Integer;
begin
  Result := TFiltersPlugin(aHandle).getOutputsCount();
end;

procedure getOutputName(const aHandle : Integer; const aIndex : Integer; aName : PChar); stdcall; export;
var
  sName : String;
begin
  TFiltersPlugin(aHandle).getOutputName( aIndex, sName );
  StrPCopy( aName, sName );
end;

function getOutputImage(const aHandle : Integer; const aName : PChar) : PFBitmap32;
begin
  Result := TFiltersPlugin(aHandle).getOutputImage( StrPas(aName) );
end;

function getOutputImagesCount(const aHandle : Integer; const aName : PChar) : Integer;
var
  tmpPArrayOfPFBitmap32 : PArrayOfPFBitmap32;
begin
  Result := 0;
  tmpPArrayOfPFBitmap32 := TFiltersPlugin(aHandle).getOutputImages( StrPas(aName) );
  if (tmpPArrayOfPFBitmap32<>nil) then begin
    Result := Length( tmpPArrayOfPFBitmap32^ );
  end;
end;

function getOutputImagesImageAtIndex(const aHandle : Integer; const aName : PChar; const aIndex : Integer) : PFBitmap32;
var
  tmpPArrayOfPFBitmap32 : PArrayOfPFBitmap32;
begin
  Result := nil;
  tmpPArrayOfPFBitmap32 := TFiltersPlugin(aHandle).getOutputImages( StrPas(aName) );
  if (tmpPArrayOfPFBitmap32<>nil) then begin
    Result := tmpPArrayOfPFBitmap32^[aIndex];
  end;
end;

function getOutputInteger(const aHandle : Integer; const aName : PChar) : Integer;
begin
  Result := TFiltersPlugin(aHandle).getOutputInteger( StrPas(aName) );
end;

function getOutputFloat(const aHandle : Integer; const aName : PChar) : Single;
begin
  Result := TFiltersPlugin(aHandle).getOutputFloat( StrPas(aName) );
end;

function getOutputArrayPointersCount(const aHandle : Integer; const aName: PChar) : Integer;
var
  tmpPArrayOfPointers : PArrayOfPointers;
begin
  Result := 0;
  tmpPArrayOfPointers := TFiltersPlugin(aHandle).getOutputArrayPointers( StrPas(aName) );
  if tmpPArrayOfPointers<>nil then begin
    Result := Length( tmpPArrayOfPointers^ );
  end;
end;

function getOutputArrayPointersPointerAtIndex(const aHandle : Integer; const aName: PChar; const aIndex : Integer) : Pointer;
var
  tmpPArrayOfPointers : PArrayOfPointers;
begin
  Result := nil;
  tmpPArrayOfPointers := TFiltersPlugin(aHandle).getOutputArrayPointers( StrPas(aName) );
  if tmpPArrayOfPointers<>nil then begin
    Result := tmpPArrayOfPointers^[aIndex];
  end;
end;

procedure setRegionOfInterest(const aHandle : Integer; const roi : PFRect);
begin
  TFiltersPlugin(aHandle).setRegionOfInterest( roi );
end;

procedure unsetRegionOfInterest(const aHandle : Integer);
begin
  TFiltersPlugin(aHandle).unsetRegionOfInterest();
end;


initialization
begin
  _filtersInstance := TList.Create;
end

finalization
begin
  for _i:=0 to _filtersInstance.Count-1 do begin
    TFiltersPlugin(_filtersInstance.Items[_i]).Free();
  end;
  _filtersInstance.Clear();
  _filtersInstance.Free();
end;

end.
