unit wrapper_filtersdll;
(* ***** BEGIN LICENSE BLOCK *****
 * Copyright (C) 2004 Durand Emmanuel
 * Copyright (C) 2004 Burgel Eric
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 * Contact :
 *   filters@edurand.com
 *   filters@burgel.com
 *
 * ***** END LICENSE BLOCK ***** *)

{
 edurand (filters@edurand.com)
}

interface
uses
    WinTypes, image;

type
  StringC = array [0..255] of Char;
  ArrayOfStringC = array of StringC;

procedure setProperty( const aName, aValue : PAnsiChar ); stdcall; export;
procedure initialize(); stdcall; export;
procedure unInitialize(); stdcall; export;

function getVersion() : PAnsiChar; far; stdcall;
function getLastError() : PAnsiChar; far; stdcall;

function getFiltersList() : ArrayOfStringC; stdcall; export;

function createFilter(pName:PAnsiChar) : Integer; far; stdcall;
procedure deleteFilter(pHandle : Integer); far; stdcall;

procedure run(pHandle : Integer); far; stdcall;
procedure runCommand(pHandle : Integer; const aCommand : PAnsiChar); far; stdcall;

function getParametersCount(const aHandle : Integer) : Integer; far; stdcall;
procedure getParameterName(const aHandle : Integer; const aIndex : Integer; aName : PAnsiChar ); far; stdcall;
procedure getParameterHelp(const aHandle : Integer; const aIndex : Integer; aName : PAnsiChar ); far; stdcall;

procedure setParameterInteger(const aHandle : Integer; const aName : PAnsiChar; const aValue : Int64 ); far; stdcall;
procedure setParameterFloat(const aHandle : Integer; const aName : PAnsiChar; const aValue : Single ); far; stdcall;
procedure setParameterBoolean(const aHandle : Integer; const aName : PAnsiChar; const aValue : Boolean ); far; stdcall;
procedure setParameterString(const aHandle : Integer; const aName : PAnsiChar; const aValue : String ); far; stdcall;
procedure _setParameterString(const aHandle : Integer; const aName : PAnsiChar; const aValue : PAnsiChar ); far; stdcall;
procedure setParameterImage(const aHandle : Integer; const aName: PAnsiChar; const aImage: PFBitmap32); far; stdcall;
procedure setParameterImages(const aHandle : Integer; const aName: PAnsiChar; const aImages: PArrayOfPFBitmap32); far; stdcall;
procedure setParameterPointer(const aHandle : Integer; const aName: PAnsiChar; const aPointer: Pointer); far; stdcall;

function getOutputsCount(const aHandle : Integer) : Integer; far; stdcall;
procedure getOutputName(const aHandle : Integer; const aIndex : Integer; aName : PAnsiChar ); far; stdcall;
function getOutputImage(const aHandle : Integer; const aName: PAnsiChar) : PFBitmap32; far; stdcall;
function getOutputImages(const aHandle : Integer; const aName: PAnsiChar) : PArrayOfPFBitmap32; far; stdcall;
function getOutputInteger(const aHandle : Integer; const aName: PAnsiChar) : Integer; far; stdcall;
function getOutputFloat(const aHandle : Integer; const aName: PAnsiChar) : Single; far; stdcall;
function getOutputArrayPointers(const aHandle : Integer; const aName: PAnsiChar) : PArrayOfPointers; far; stdcall;
function getOutputArrayPointersLength(const aHandle : Integer; const aName: PAnsiChar) : Integer; far; stdcall;

procedure setRegionOfInterest(const aHandle : Integer; const roi : PRect); stdcall; export;
procedure unsetRegionOfInterest(const aHandle : Integer); stdcall; export;


implementation
uses
  SysUtils;

procedure setProperty( const aName, aValue : PAnsiChar ); stdcall; export;
          external 'filtersdll.dll' name 'setProperty';

procedure initialize(); stdcall; export;
          external 'filtersdll.dll' name 'initialize';
          
procedure unInitialize(); stdcall; export;
          external 'filtersdll.dll' name 'unInitialize';

function getVersion() : PAnsiChar; far; stdcall;
          external 'filtersdll.dll' name 'getVersion';

function getLastError() : PAnsiChar; far; stdcall;
          external 'filtersdll.dll' name 'getLastError';

function getFiltersList() : ArrayOfStringC; far; stdcall;
          external 'filtersdll.dll' name 'getFiltersList';

function createFilter(pName:PAnsiChar) : Integer; far; stdcall;
          external 'filtersdll.dll' name 'createFilter';

procedure deleteFilter(pHandle : Integer); far; stdcall;
          external 'filtersdll.dll' name 'deleteFilter';

procedure run(pHandle : Integer); far; stdcall;
          external 'filtersdll.dll' name 'run';

procedure runCommand(pHandle : Integer; const aCommand : PAnsiChar); far; stdcall;
          external 'filtersdll.dll' name 'runCommand';

function getParametersCount(const aHandle : Integer) : Integer; far; stdcall;
          external 'filtersdll.dll' name 'getParametersCount';

procedure getParameterName(const aHandle : Integer; const aIndex : Integer; aName : PAnsiChar ); far; stdcall;
          external 'filtersdll.dll' name 'getParameterName';

procedure getParameterHelp(const aHandle : Integer; const aIndex : Integer; aName : PAnsiChar ); far; stdcall;
          external 'filtersdll.dll' name 'getParameterHelp';

procedure setParameterInteger(const aHandle : Integer; const aName : PAnsiChar; const aValue : Int64 ); far; stdcall;
          external 'filtersdll.dll' name 'setParameterInteger';

procedure setParameterFloat(const aHandle : Integer; const aName : PAnsiChar; const aValue : Single ); far; stdcall;
          external 'filtersdll.dll' name 'setParameterFloat';

procedure setParameterBoolean(const aHandle : Integer; const aName : PAnsiChar; const aValue : Boolean ); far; stdcall;
          external 'filtersdll.dll' name 'setParameterBoolean';

procedure setParameterString(const aHandle : Integer; const aName : PAnsiChar; const aValue : String ); far; stdcall;
var
  tmpCStr :  array of char;
begin
  if Length(aValue)>0 then begin
    SetLength( tmpCStr, Length(aValue)+1 );
    StrPCopy( PAnsiChar(tmpCStr), aValue );
    _setParameterString( aHandle, aName, PAnsiChar(tmpCStr) );
    SetLength( tmpCStr, 0 );
  end;
end;

procedure _setParameterString(const aHandle : Integer; const aName : PAnsiChar; const aValue : PAnsiChar ); far; stdcall;
          external 'filtersdll.dll' name 'setParameterString';

procedure setParameterImage(const aHandle : Integer; const aName: PAnsiChar; const aImage: PFBitmap32); far; stdcall;
          external 'filtersdll.dll' name 'setParameterImage';

procedure setParameterImages(const aHandle : Integer; const aName: PAnsiChar; const aImages: PArrayOfPFBitmap32); far; stdcall;
          external 'filtersdll.dll' name 'setParameterImages';

procedure setParameterPointer(const aHandle : Integer; const aName: PAnsiChar; const aPointer: Pointer); far; stdcall;
          external 'filtersdll.dll' name 'setParameterPointer';

function getOutputImage(const aHandle : Integer; const aName: PAnsiChar) : PFBitmap32; far; stdcall;
          external 'filtersdll.dll' name 'getOutputImage';

function getOutputImages(const aHandle : Integer; const aName: PAnsiChar) : PArrayOfPFBitmap32; far; stdcall;
          external 'filtersdll.dll' name 'getOutputImages';

function getOutputInteger(const aHandle : Integer; const aName: PAnsiChar) : Integer; far; stdcall;
          external 'filtersdll.dll' name 'getOutputInteger';

function getOutputFloat(const aHandle : Integer; const aName: PAnsiChar) : Single; far; stdcall;
          external 'filtersdll.dll' name 'getOutputFloat';

function getOutputsCount(const aHandle : Integer) : Integer; far; stdcall;
          external 'filtersdll.dll' name 'getOutputsCount';

procedure getOutputName(const aHandle : Integer; const aIndex : Integer; aName : PAnsiChar ); far; stdcall;
          external 'filtersdll.dll' name 'getOutputName';

function getOutputArrayPointers(const aHandle : Integer; const aName: PAnsiChar) : PArrayOfPointers; far; stdcall;
          external 'filtersdll.dll' name 'getOutputArrayPointers';

function getOutputArrayPointersLength(const aHandle : Integer; const aName: PAnsiChar) : Integer; far; stdcall;
          external 'filtersdll.dll' name 'getOutputArrayPointersLength';

procedure setRegionOfInterest(const aHandle : Integer; const roi : PRect); stdcall; export;
          external 'filtersdll.dll' name 'setRegionOfInterest';
procedure unsetRegionOfInterest(const aHandle : Integer); stdcall; export;
          external 'filtersdll.dll' name 'unsetRegionOfInterest';


end.

