unit image;
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
 * Site :
 *   http://filters.sourceforge.net/
 *
 * ***** END LICENSE BLOCK ***** *)

{
 edurand (filters@edurand.com)
 eburgel (filters@burgel.com)
}

{
  - part of code from TBitmap32 of Graphics32 project
      homepage: http://graphics32.org
}

{
  - part of code (Bresenham's Line/Circle/Ellipse algorithm) adapted from :
      Author: Finn Tolderlund / Denmark / Date: 17.10.2002
      homepage: http://www.tolderlund.eu/
                http://home20.inet.tele.dk/tolderlund/
                http://finn.mobilixnet.dk/
      e-mail:   finn@mail.tdcadsl.dk
                finn.tolderlund@mobilixnet.dk
}

interface
uses
  SysUtils, windows;

type
  PFColor32 = ^TFColor32;
  TFColor32 = Type Cardinal;
  PColor32 = PFColor32;
  TColor32 = TFColor32;
  TColorRec = Record
                Case boolean of
                  True : (value : Cardinal) ;
                  false : (Blue : Byte ;
                           Green : Byte ;
                           Red : Byte;
                           alpha : Byte)
              End ;

  PFColor32Array = ^TFColor32Array;
  TFColor32Array = array [0..0] of TFColor32;
  PColor32Array = PFColor32Array;
  TColor32Array = TFColor32Array;

  PFBitmap32 = ^TBitmap32;
  PBitmap32 = PFBitmap32;
  TFBitmap32 = record
    Width: Integer;
    Height: Integer;
    //Hline : PColor32Array;
    Bits: PColor32Array;
  end;
  TBitmap32 = TFBitmap32;

  ArrayOfPFBitmap32 = array of PFBitmap32;
  ArrayOfPBitmap32 = ArrayOfPFBitmap32;
  PArrayOfPFBitmap32 = ^ArrayOfPFBitmap32;
  PArrayOfPBitmap32 = PArrayOfPFBitmap32;

  PFPoint = ^TFPoint;
  TFPoint = record
    x,y: Single;
  end;

  PFRect = ^TFRect;
  TFRect = record
      Left, Top, Right, Bottom: Single;
  end;

  PFSegment = ^TFSegment;
  PSegment = PFSegment;
  TFSegment = record
    p1, p2 : TFPoint;
    width : Single;
  end;
  TSegment = TFSegment;

  TFSegmentList = array of TFSegment;
  TSegmentList = TFSegmentList;

  TLineFunction = record
    a, b : Single;   // y=ax+b
  end;

  TWedge = record
    p0,p1 : TFPoint;
    pw1,pw2 : TFPoint;
    line1, line2 : TLineFunction;
  end;

  TVector = record
    fromPoint, toPoint : TFPoint;
    length : Single;
  end;

  TFVector = record
    point : TFPoint;
    length : Single;
    angle : Single;
  end;

  ArrayOfPointers = array of Pointer;
  PArrayOfPointers = ^ArrayOfPointers;

  PFBlob = ^TFBlob;
  PBlob = PFBlob;
  TFBlob = record
    index : Integer;
    color : TFColor32;
    segmentList : TFSegmentList;
    length_segmentList : Integer;
    approximatedSegmentList : TFSegmentList;
    length_approximatedSegmentList : Integer;
    vectorChain : array of TFVector;
    length_vectorChain : Integer;
    rectangleContainer : TFRect;
    pixelArea : Integer;
    gravityCenter : TFPoint;
    perimeter : Single;
  end;
  TBlob = TFBlob;

const
  SizeOfColor32 : Integer = 4;
  _PI2 : Single = PI/2;
  _2PI : Single = 2*PI;

function isSameSize( const image1, image2 : PBitmap32) : Boolean;

function createImage( const aWidth,aHeight:Integer) : PBitmap32;
function createImageTest( const aWidth,aHeight:Integer) : PBitmap32;
function createImageLike( const aImage:PBitmap32) : PBitmap32;
function createImageFromImage( const aImage:PBitmap32) : PBitmap32;
procedure copyImageToImage( const aImageFrom, aImageTo:PBitmap32);
function eraseOrCreateImageLike( lastImage:PBitmap32; const aImage:PBitmap32) : PBitmap32;
procedure eraseImage( const aImage:PBitmap32);
procedure freeImage(var aImage:PBitmap32);
procedure freeImages( images : PArrayOfPBitmap32 );

function Color32( const R, G, B: Byte;  const A: Byte = $FF): TColor32; overload; //ebu ? overload
function Gray32( const Intensity: Byte;  const Alpha: Byte = $FF): TColor32;
// Color component access
function RedComponent( const Color32: TColor32): Byte;
function GreenComponent( const Color32: TColor32): Byte;
function BlueComponent( const Color32: TColor32): Byte;
function Intensity( const Color32: TColor32): Byte;
function Luminosity( const Color32: TColor32): Byte;
procedure setPixel( const bitmap:PBitmap32 ;  const x, y : SmallInt;  const color:TColor32) ; overload;
procedure setPixel( const bitmap:PBitmap32 ;  const x, y : Single;  const color:TColor32) ; overload;
procedure setPixelAsSingle( const bitmap:PBitmap32 ;  const x, y : SmallInt;  const data:Single) ; overload;
procedure setPixelAsSingle( const pixel:PColor32Array ; const data:Single) ; overload;
function getPixel( const bitmap:PBitmap32 ;  const x, y : Word) : TColor32;
function getPixelAsSingle( const bitmap:PBitmap32 ;  const x, y : Word) : Single;
function getPixelAsString( const bitmap:PBitmap32 ;  const x, y : Word) : String;
function getValidPixel( const bitmap:PBitmap32 ;  const x, y : Word) : TColor32;
function getValidX( const bitmap:PBitmap32 ;  const x : Single) : Integer; overload;
function getValidX( const bitmap:PBitmap32 ;  const x : SmallInt) : Integer; overload;
function getValidY( const bitmap:PBitmap32 ;  const y : SmallInt) : Integer; overload;
function getValidY( const bitmap:PBitmap32 ;  const y : Single) : Integer; overload;
function isValidPoint( const bitmap:PBitmap32; const x,y:SmallInt) : boolean;
// region of interest functions
function getValidROI( const bitmap : PBitmap32;  const RegionOfInterest : TRect) : TRect;
function scanlineWithROI( const bitmap : PBitmap32;  const RegionOfInterest : TRect ;  const aY: Word): PColor32Array;
procedure drawLine( const bitmap : PBitmap32;  const FromX, FromY, ToX, ToY: Integer;  const Color: TColor32;  const Thick : Single = 1); overload;
procedure drawLine( const bitmap : PBitmap32; FromX, FromY, ToX, ToY: Single; Color: TColor32; Thick : Single = 1); overload;
procedure drawLine( const bitmap : PBitmap32;  const p1,p2 : TFPoint;  const Color: TColor32;  const Thick : Single = 1); overload;
procedure drawLine( const bitmap : PBitmap32;  const segment : TSegment;  const Color: TColor32;  const Thick : Single = 1); overload;
procedure drawLines( const bitmap : PBitmap32; const segments : TSegmentList; const ColorLine, ColorPoint : TColor32; const Thick : Single = 1);
procedure drawEmptyThickLine( const bitmap : PBitmap32;  const FromX, FromY, ToX, ToY: Integer;  const Color: TColor32;  const Thick : Integer); overload;
procedure drawRect( const bitmap : PBitmap32; const r : TRect; const color : TColor32;  const thick : Single = 1); overload;
procedure drawRect( const bitmap : PBitmap32; const r : TFRect; const color : TColor32;  const thick : Single = 1); overload;
procedure drawRectFilled( const bitmap : PBitmap32; const r : TRect; const color : TColor32); overload;
procedure drawRectFilled( const bitmap : PBitmap32; const r : TFRect; const color : TColor32); overload;
procedure drawDisk( const bitmap : PBitmap32;  const CenterX, CenterY, radius : Single;  const Color: TColor32; Feather : Single = 0);
procedure drawCircle( const bitmap : PBitmap32;  const CenterX, CenterY, radius : Single;  const Color: TColor32 );

function HSLtoRGB(H, S, L: Single): TColor32;
procedure RGBtoHSL(RGB: TColor32; out H, S, L : Single);

procedure resetNextColor();
function getNextColor() : TColor32;

procedure addSegment(var segmentList:TSegmentList; const p1,p2:TFPoint; const width:Single);
function getDistance( const p1,p2:TFPoint) : Single;
function getWedge( const p0,p1:TFPoint; const lastWedge:TWedge; const epsylone:Single; const imageMonitoring : PBitmap32) : TWedge;
function getLine( const p0,p1:TFPoint) : TLineFunction;
function getLinePerpendicular( const line:TLineFunction; const p:TFPoint) : TLineFunction;
function getVector( const p0,p1:TFPoint) : TVector;
function getVectorPerpendicular( const vector : TVector) : TVector;
function resizeVector( const vector : TVector;  const r : Single) : TVector;
function moveVector( const vector : TVector;  const p : TFPoint) : TVector;
function invertVector( const vector : TVector) : TVector;
function isPointInWedge( const point : TFPoint;  const wedge : TWedge) : boolean;
function convertToVector(s : TSegment) : TFVector;

procedure setWeight(image:PBitmap32 ; x, y : Word; weight:ShortInt) ;

function getRectIntersection( const R1, R2: TFRect ): TFRect;
function getRectPerimeter( const R : TFRect ) : Single;


const
  // Some predefined color constants
  clBlack32               = TColor32($FF000000);
  clDimGray32             = TColor32($FF3F3F3F);
  clGray32                = TColor32($FF7F7F7F);
  clLightGray32           = TColor32($FFBFBFBF);
  clWhite32               = TColor32($FFFFFFFF);
  clMaroon32              = TColor32($FF7F0000);
  clGreen32               = TColor32($FF007F00);
  clOlive32               = TColor32($FF7F7F00);
  clNavy32                = TColor32($FF00007F);
  clPurple32              = TColor32($FF7F007F);
  clTeal32                = TColor32($FF007F7F);
  clRed32                 = TColor32($FFFF0000);
  clLime32                = TColor32($FF00FF00);
  clYellow32              = TColor32($FFFFFF00);
  clBlue32                = TColor32($FF0000FF);
  clFuchsia32             = TColor32($FFFF00FF);
  clAqua32                = TColor32($FF00FFFF);

var
  _nextColorIndex : Integer;
  _nextColorArray : array of TColor32;
  _onePixelImage : PBitmap32;


implementation

uses Math ;

function HSLtoRGB(H, S, L: Single): TColor32;
const
  OneOverThree = 1 / 3;
var
  M1, M2: Single;
  R, G, B: Byte;

  function HueToColor(Hue: Single): Byte;
  var
    V: Double;
  begin
    Hue := Hue - Floor(Hue);
    if 6 * Hue < 1 then V := M1 + (M2 - M1) * Hue * 6
    else if 2 * Hue < 1 then V := M2
    else if 3 * Hue < 2 then V := M1 + (M2 - M1) * (2 / 3 - Hue) * 6
    else V := M1;
    Result := Round(255 * V);
  end;

begin
  if S = 0 then
  begin
    R := Round(255 * L);
    G := R;
    B := R;
  end
  else
  begin
    if L <= 0.5 then M2 := L * (1 + S)
    else M2 := L + S - L * S;
    M1 := 2 * L - M2;
    R := HueToColor(H + OneOverThree);
    G := HueToColor(H);
    B := HueToColor(H - OneOverThree)
  end;
  Result := Color32(R, G, B, 255);
end;

procedure RGBtoHSL(RGB: TColor32; out H, S, L : Single);
var
  R, G, B, D, Cmax, Cmin: Single;
begin
  R := RedComponent(RGB) / 255;
  G := GreenComponent(RGB) / 255;
  B := BlueComponent(RGB) / 255;
  Cmax := Max(R, Max(G, B));
  Cmin := Min(R, Min(G, B));
  L := (Cmax + Cmin) / 2;

  if Cmax = Cmin then
  begin
    H := 0;
    S := 0
  end
  else
  begin
    D := Cmax - Cmin;
    if L < 0.5 then S := D / (Cmax + Cmin)
    else S := D / (2 - Cmax - Cmin);
    if R = Cmax then H := (G - B) / D
    else
      if G = Cmax then H  := 2 + (B - R) / D
      else H := 4 + (R - G) / D;
    H := H / 6;
    if H < 0 then H := H + 1
  end;
end;

procedure setPixel( const bitmap:PBitmap32 ;  const x, y : SmallInt;  const color:TColor32) ; overload;
var
  pbits : PColor32Array;
Begin
  pbits := bitmap.Bits;
  pbits[ y*bitmap.Width + x ] := color ;
End ;

procedure setPixel( const bitmap:PBitmap32 ;  const x, y : Single;  const color:TColor32) ; overload;
begin
  setPixel(bitmap,SmallInt(Trunc(x+0.5)),SmallInt(Trunc(y+0.5)),color) ;
end;

procedure setPixelAsSingle( const bitmap:PBitmap32 ;  const x, y : SmallInt;  const data:Single) ; overload;
var
  pbits : PColor32Array;
Begin
  pbits := bitmap.Bits;
  Move( data, pbits[ y*bitmap.Width + x ], 4 );
end;

procedure setPixelAsSingle( const pixel:PColor32Array;  const data:Single) ; overload;
var
  pbits : PColor32Array;
Begin
  pbits := pixel;
  Move( data, pbits[0], 4 );
end;

function getValidX( const bitmap:PBitmap32 ;  const x : Single) : Integer; overload;
begin
  Result:=Round(x);
  if Result<0 then Result:=0;
  if Result>=bitmap.Width then Result:=bitmap.Width-1;
end;

function getValidX( const bitmap:PBitmap32 ;  const x : SmallInt) : Integer; overload;
begin
  Result:=x;
  if Result<0 then Result:=0;
  if Result>=bitmap.Width then Result:=bitmap.Width-1;
end;

function getValidY( const bitmap:PBitmap32 ;  const y : SmallInt) : Integer; overload;
begin
  Result:=y;
  if y<0 then Result:=0;
  if y>=bitmap.Height then Result:=bitmap.Height-1;
end;

function getValidY( const bitmap:PBitmap32 ;  const y : Single) : Integer; overload;
begin
  Result:=Round(y);
  if Result<0 then Result:=0;
  if Result>=bitmap.Height then Result:=bitmap.Height-1;
end;

function isValidPoint( const bitmap:PBitmap32; const x,y:SmallInt) : boolean;
begin
  Result:=false;
  if (x>=0) and (x<bitmap.Width) and (y>=0) and (y<bitmap.Height) then Result:=true;
end;

function getPixel( const bitmap:PBitmap32 ;  const x, y : Word) : TColor32;
var
  pbits : PColor32Array ;
begin
  pbits := bitmap.Bits ;
  result := pbits[y*bitmap.Width+x];
end ;

function getPixelAsSingle( const bitmap:PBitmap32 ;  const x, y : Word) : Single;
var
  pbits : PColor32Array ;
begin
  pbits := bitmap.Bits ;
  Move( pbits[y*bitmap.Width+x], Result, 4 );
end ;

function getPixelAsString( const bitmap:PBitmap32 ;  const x, y : Word) : String;
var
  pbits : PColor32Array;
  cr : TColorRec;
begin
  pbits := bitmap.Bits ;
  cr := TColorRec(pbits[y*bitmap.Width+x]);
  Result := format('R=%3d, G=%3d, B=%3d, Color=$%.8x, Intensity=%d', [cr.Red, cr.Green, cr.Blue, cr.value, Intensity(cr.value)]);
end;

function getValidPixel( const bitmap:PBitmap32 ;  const x, y : Word) : TColor32;
begin
  if isValidPoint(bitmap, x, y)=true then begin
    Result := getPixel(bitmap, x, y);
  end else begin
    Result := clBlack32;
  end;
end;

procedure freeImage( var aImage:PBitmap32 );
begin
  if (aImage <> nil) then Begin
    //if(aImage.Hline<>nil) then begin
    //  FreeMem(aImage.Hline);
    //end;
    if(aImage.Bits<>nil) then begin
      FreeMem(aImage.Bits);
    end;
    Dispose(aImage);
    aImage := nil ;
  end ;
end;

procedure freeImages( images : PArrayOfPBitmap32 );
var
  i : Integer;
  ptmpImage : PBitmap32;
begin
  if images<>nil then begin
    for i:=0 to Length(images^)-1 do begin
      ptmpImage := images^[i];
      if (ptmpImage<>nil) then begin
        image.freeImage( ptmpImage );
      end;
    end;
    SetLength( images^, 0 );
  end;
end;

function createImage( const aWidth,aHeight:Integer) : PBitmap32;
var
  image : PBitmap32;
  w,h : Integer;
begin
  w:=aWidth;
  h:=aHeight;
  New(image);
  FillChar(image^, SizeOf(TBitmap32), 0);
  if w<=0 then w:=1;
  if h<=0 then h:=1;
  image.Width:=w;
  image.Height:=h;

  //GetMem(image.Hline, image.Width*SizeOf(TColor32)) ;
  //FillChar(image.Hline^, image.Width*SizeOf(TColor32), 0);

  GetMem(image.Bits,image.Width*image.Height*SizeOf(TColor32));
  if image.Bits = nil then begin
    raise Exception.Create('Can''t allocate memory for image');
  end else begin
    FillChar(image.Bits^,image.Width*image.Height*SizeOf(TColor32),0);
  end ;

  eraseImage(image);

  Result:=image;
end;

procedure eraseImage( const aImage:PBitmap32);
var
  PDest : PColor32Array;
begin
  if aImage<>nil then begin
    pDest:=aImage.Bits;
    FillChar(pDest^,aImage.Width*aImage.Height*SizeOf(TColor32),0);
    //pDest:=aImage.Hline;
    //FillChar(pDest^,aImage.Width*SizeOf(TColor32),0);
  end;
end;

function createImageTest( const aWidth,aHeight:Integer) : PBitmap32;
var
  img : PBitmap32;
  row, col : Integer;
  PDest : PColor32Array;
  value : TColor32 ;
  height1_2, height3_4 : Integer;
  middleWidth : Integer;
  motif : Integer;
begin
  img:=image.createImage(aWidth,aHeight);
  pDest:=img.Bits;
  height1_2:=img.Height div 2;
  middleWidth:=img.Width div 2;
  height3_4:=height1_2+img.Height div 4;
  value := 0 ;
  for row:=0 to img.Height-1 do begin
    for col:=0 to img.Width-1 do begin
      if row<height1_2 then begin
        if col<middleWidth then begin
          motif:=1;
        end else begin
          motif:=3;
        end;
      end else begin
        if col<middleWidth then begin
          motif:=2;
        end else begin
          if row<height3_4 then begin
            motif:=4;
          end else begin
            motif:=5;
          end;
        end;
      end;
      // motif
      case motif of
      1: value:=Gray32(random(255));
      2: value:=Gray32(row mod 255);
      3: value:=Gray32((row mod 255*10) mod 255);
      4: begin
           if (col mod 2)=0 then value:=Gray32(200) else value:=Gray32(126);
         end;
      5: begin
           if (col mod 3)=0 then value:=Gray32(200) else value:=Gray32(126);
         end;
      end;
      // set
      pDest[0] := value ;
      inc(pDest) ;
    end;
  end;
  DrawDisk(img, 512, 256+64, 64, clRed32, 1) ;
  Result:=img;
end;

function createImageLike( const aImage:PBitmap32) : PBitmap32;
var
  img : PBitmap32;
begin
  if aImage=nil then raise Exception.Create('error in updateOrCreateImageLike : image is null');
  img:=image.createImage(aImage.Width,aImage.Height);
  Result:=img;
end;

function eraseOrCreateImageLike( lastImage:PBitmap32; const aImage:PBitmap32) : PBitmap32;
begin
  if aImage=nil then raise Exception.Create('error in updateOrCreateImageLike : image is null');
  if lastImage=nil then begin
    lastImage:=createImageLike(aImage);
  end else begin
    if isSameSize(lastImage,aImage) then begin
      eraseImage(lastImage);
    end else begin
      freeImage(lastImage);
      lastImage:=createImageLike(aImage);
    end;
  end;
  Result:=lastImage;
end;

function createImageFromImage( const aImage:PBitmap32) : PBitmap32;
var
  img : PBitmap32;
begin
  img:=image.createImage(aImage.Width,aImage.Height);
  copyImageToImage(aImage,img);
  Result:=img;
end;

procedure copyImageToImage( const aImageFrom, aImageTo:PBitmap32);
begin
  if aImageFrom=nil then raise Exception.Create('error in imageIO.copyImageToImage : aImageFrom is null');
  if aImageTo=nil then raise Exception.Create('error in imageIO.copyImageToImage : aImageTo is null');
  if (aImageFrom.Width = aImageTo.Width) and (aImageFrom.Height = aImageTo.Height) then
    Move(aImageFrom.Bits^, aImageTo.Bits^, aImageTo.Width*aImageTo.Height*SizeOf(TColor32))
  else
    raise Exception.Create('error in imageIO.copyImageToImage : different image size') ;
end;

function isSameSize( const image1, image2 : PBitmap32) : Boolean;
begin
  Result:=false;
  if (image1<>nil) and (image2<>nil) then begin
    Result:= (image1.Width=image2.Width) and (image1.Height=image2.Height);
  end;
end;

function Color32( const R, G, B: Byte;  const A: Byte = $FF): TColor32 ; overload ;
asm
        MOV  AH,A
        SHL  EAX,16
        MOV  AH,DL
        MOV  AL,CL
end;

function Gray32( const Intensity: Byte;  const Alpha: Byte = $FF): TColor32;
begin
  Result := TColor32(Alpha) shl 24 + TColor32(Intensity) shl 16 +
    TColor32(Intensity) shl 8 + TColor32(Intensity);
end;

function RedComponent( const Color32: TColor32): Byte;
begin
  //Result := (Color32 and $00FF0000) shr 16;
  Result := Byte(Color32 shr 16);
end;

function GreenComponent( const Color32: TColor32): Byte;
begin
  //Result := (Color32 and $0000FF00) shr 8;
  Result := Byte(Color32 shr 8);
end;

function BlueComponent( const Color32: TColor32): Byte;
begin
  //Result := Color32 and $000000FF;
  Result := Byte(Color32);
end;

function Intensity( const Color32: TColor32): Byte;
begin
// (R * 61 + G * 174 + B * 21) / 256
//  Result := (
//    (Color32 and $00FF0000) shr 16 * 61 +
//    (Color32 and $0000FF00) shr 8 * 174 +
//    (Color32 and $000000FF) * 21
//    ) shr 8;
  Result := (
    (Byte(Color32 shr 16) * 61) +
    (Byte(Color32 shr 8) * 174) +
    (Byte(Color32) * 21)
            ) shr 8;
end;

function Luminosity( const Color32: TColor32): Byte;
begin
//  Result := (
//    (Color32 and $00FF0000) shr 16 * 85 +
//    (Color32 and $0000FF00) shr 8 * 86 +
//    (Color32 and $000000FF) * 85
//    ) shr 8;
  Result := (
    (Byte(Color32 shr 16) * 85) +
    (Byte(Color32 shr 8) * 86) +
    (Byte(Color32) * 85)
            ) shr 8;
end;

//****************************************************************

{ CBitmap32 }
(*
constructor CBitmap32.create(width, height: Integer);
begin
  bitmap := createBitmap(width, height) ;
end;

destructor CBitmap32.Done;
begin
  freeBitmap(bitmap) ;
end;

procedure CBitmap32.setPixel(x, y: Word; color: TColor32);
begin
  image.setPixel(bitmap, x, y, color) ;
end;

function CBitmap32.scanline(y: Word): PColor32Array;
var
  pSrc : PColor32Array ;
begin
  pSrc:= bitmap.Bits ;
  inc(pSrc, y*bitmap.Width) ;
  Result := pSrc ;
end;
*)

function getValidROI( const bitmap : PBitmap32;  const RegionOfInterest : TRect) : TRect;
var
  validROI : TRect;
begin
  with RegionOfInterest do Begin
    if (Left=0) and (Top=0) and (Right=0) and (Bottom=0) then begin
      validROI.Left:=0;
      validROI.Right:=bitmap.Width-1;
      validROI.Top:=0;
      validROI.Bottom:=bitmap.Height-1
    end else begin
      //   Right
      if Right>=bitmap.Width then
        validROI.Right:=bitmap.Width-1
      else if Right<1 then
        validROI.Right:=1
      else
        validROI.Right:=Right;
      //  Left
      if Left<0 then
        validROI.Left:=0
      else if Left>=bitmap.Width then
        validROI.Left:=bitmap.Width-1
      else if Left>validROI.Right then
        validROI.Left:=validROI.Right-1
      else
        validROI.Left:=Left;
        //   Bottom
      if Bottom>=bitmap.Height then
        validROI.Bottom:=bitmap.Height-1
      else if Bottom<1 then
        validROI.Bottom:=1
      else
        validROI.Bottom:=Bottom;
      //  Top
      if Top<0 then
        validROI.Top:=0
      else if Top>=bitmap.Height then
        validROI.Top:=bitmap.Height-1
      else if Top>validROI.Bottom then
        validROI.Top:=validROI.Bottom-1
      else
        validROI.Top:=Top;
    end;
  end;
  Result:=validROI;
end;

function scanlineWithROI( const bitmap : PBitmap32;  const RegionOfInterest : TRect ;  const aY: Word): PColor32Array;
var
  pSrc : PColor32Array ;
  validROI : TRect;
  y : Word;
begin
  y:=aY;
  validROI:=getValidROI(bitmap,RegionOfInterest);
  // scanline
  pSrc:= bitmap.Bits ;
  inc(y,validROI.Top) ;
  inc(pSrc, y*bitmap.Width+validROI.Left) ;
  Result := pSrc ;
end;

procedure BresenhamLine(bitmap : PBitmap32; FromX, FromY, ToX, ToY: SmallInt; Color: TColor32; thickness : Single); overload;
{
Bresenham's Line Algorithm
Bresenham's Circle Algorithm
fra denne side: http://www.funducode.com/freec/graphics/graphics2.htm
}
const
  INCR = 1;
  DECR = -1;
  PREDX = 1;
  PREDY = 0;
var
  dx, dy, e, e_inc, e_noinc: Integer;

 procedure DrawPixel(centralX,centralY : Integer);
 var
   thicknessDiv2 : Single;
   //x,xMin,xMax,y,yMin,yMax:Integer;
 begin
   if thickness=1 then begin
     setPixel(bitmap,getValidX(bitmap,centralX),getValidY(bitmap,centralY),Color);
   end else begin
     thicknessDiv2 := thickness / 2;
     DrawDisk(bitmap, getValidX(bitmap,centralX),getValidY(bitmap,centralY),thicknessDiv2,Color);
   end;
 end;

 procedure DrawLine(x1, y1, x2, y2, pred, incdec: Integer);
 var
  i, istart, iend, ivar: Integer;
 begin
  if ( pred = PREDX ) then
    begin
      istart := x1 ;
      iend := x2 ;
      ivar := y1 ;
    end
  else
    begin
      istart := y1 ;
      iend := y2 ;
      ivar := x1 ;
    end;
  for i := istart to iend do
    begin
      if ( pred = PREDY ) then
        DrawPixel(ivar,i)
      else
        DrawPixel(i,ivar);
      if ( e < 0 ) then
        e := e + e_noinc
      else
        begin
          ivar := ivar + incdec ;
          e := e + e_inc ;
        end;
    end;
 end;

var
  t, i: Integer;
  x1, y1, x2, y2: Integer;
begin
  x1 := FromX;
  y1 := FromY;
  x2 := ToX;
  y2 := ToY;

  if ( x1 > x2 ) then
    begin
      t := x1 ; x1 := x2 ; x2 := t ;
      t := y1 ; y1 := y2 ; y2 := t ;
    end;

  dx := x2 - x1 ; dy := y2 - y1 ;

  if ( dx = 0 ) then //* vertical line */
  begin
    if ( y1 > y2 ) then
      begin
        t := y1 ; y1 := y2 ; y2 := t ;
      end;
    for i := y1 to y2 do
      DrawPixel(x1,i);
    Exit;
  end;

  if ( dy = 0 ) then  //* horizontal line */
    begin
      for i := x1 to x2 do
        DrawPixel(i,y1);
      Exit;
    end;

  //* 0 < m < 1 */
  if ( dy < dx) and (dy > 0 ) then
    begin
      e_noinc := 2 * dy ;
      e := 2 * dy - dx ;
      e_inc := 2 * ( dy - dx ) ;
      DrawLine( x1, y1, x2, y2, PREDX, INCR ) ;
    end;

  //* m = 1 */
  if ( dy = dx) and (dy > 0 ) then
    begin
      e_noinc := 2 * dy ;
      e := 2 * dy - dx ;
      e_inc := 2 * ( dy - dx ) ;
      drawline ( x1, y1, x2, y2, PREDX, INCR ) ;
    end;

  //* 1 < m < infinity */
  if ( dy > dx) and (dy > 0 ) then
    begin
      e_noinc := 2 * dx ;
      e := 2 * dx - dy ;
      e_inc := 2 * ( dx - dy ) ;
      drawline ( x1, y1, x2, y2, PREDY, INCR ) ;
    end;

  //* 0 > m > -1 */
  if ( -dy < dx) and (dy < 0 ) then
    begin
      dy := -dy ;
      e_noinc := 2 * dy ;
      e := 2 * dy - dx ;
      e_inc := 2 * ( dy - dx ) ;
      drawline ( x1, y1, x2, y2, PREDX, DECR ) ;
    end;

  //* m = -1 */
  if ( dy = -dx) and (dy < 0 ) then
    begin
      dy := -dy ;
      e_noinc := ( 2 * dy ) ;
      e := 2 * dy - dx ;
      e_inc := 2 * ( dy - dx ) ;
      drawline ( x1, y1, x2, y2, PREDX, DECR ) ;
    end;

  //* -1 > m > 0 */
  if ( -dy > dx) and (dy < 0 ) then
    begin
      dx := -dx ;
      e_noinc := - ( 2*dx ) ; e := 2 * dx - dy ;
      e_inc := - 2 * ( dx - dy ) ;
      drawline ( x2, y2, x1, y1, PREDY, DECR ) ;
    end;

end;

procedure BresenhamLine(bitmap : PBitmap32; FromX, FromY, ToX, ToY: Single; Color: TColor32; thickness : Single); overload;
begin
  BresenhamLine(bitmap,SmallInt(Round(FromX)),SmallInt(Round(FromY)),SmallInt(Round(ToX)),SmallInt(Round(ToY)),Color,thickness);
end;

// inspired by http://www.rgagnon.com/javadetails/java-0260.html
procedure BresenhamEmptyThickLine(bitmap : PBitmap32; x1, y1, x2, y2: Integer; Color: TColor32; thickness : Integer);
var
  dX, dY : Integer;
  lineLength, scale : Single;
  deltaX, deltaY : Integer;
  p0,p1,p2,p3 : TFPoint;
begin
  dX := x2 - x1;
  dY := y2 - y1;
  lineLength := sqrt(dX * dX + dY * dY);
  scale := (thickness) / (2 * lineLength);
  // The x and y increments from an endpoint needed to create a rectangle...
  deltaX := Round(-scale * dY);
  deltaY := Round(scale * dX);
  // Now we can compute the corner points...
  p0.x := x1 + deltaX; p0.y := y1 + deltaY;
  p1.x := x1 - deltaX; p1.y := y1 - deltaY;
  p2.x := x2 - deltaX; p2.y := y2 - deltaY;
  p3.x := x2 + deltaX; p3.y := y2 + deltaY;
  BresenhamLine(bitmap,p0.x,p0.y,p1.x,p1.y,Color,1);
  BresenhamLine(bitmap,p1.x,p1.y,p2.x,p2.y,Color,1);
  BresenhamLine(bitmap,p2.x,p2.y,p3.x,p3.y,Color,1);
  BresenhamLine(bitmap,p3.x,p3.y,p0.x,p0.y,Color,1);
end;

procedure drawLine( const bitmap : PBitmap32;  const FromX, FromY, ToX, ToY: Integer;  const Color: TColor32;  const Thick : Single = 1); overload;
begin
  BresenhamLine(bitmap,FromX, FromY, ToX, ToY, Color, Thick);
end;

procedure drawLine( const bitmap : PBitmap32; FromX, FromY, ToX, ToY: Single; Color: TColor32; Thick : Single = 1); overload;
begin
  BresenhamLine(bitmap,FromX, FromY, ToX, ToY, Color, Thick);
end;

procedure drawLine( const bitmap : PBitmap32;  const p1,p2 : TFPoint;  const Color: TColor32;  const Thick : Single = 1); overload;
begin
  drawLine(bitmap,p1.x,p1.y,p2.x,p2.y,Color,Thick);
end;

procedure drawLine( const bitmap : PBitmap32;  const segment : TSegment;  const Color: TColor32;  const Thick : Single = 1); overload;
begin
  drawLine(bitmap,segment.p1.x,segment.p1.y,segment.p2.x,segment.p2.y,Color,Thick);
end;

procedure drawLines( const bitmap : PBitmap32; const segments : TSegmentList; const ColorLine, ColorPoint : TColor32; const Thick : Single = 1);
var
  s : Integer;
begin
    for s:=0 to Length(segments)-1 do begin
      image.drawLine(bitmap,segments[s],ColorLine);
      image.setPixel(bitmap,segments[s].p1.x,segments[s].p1.y,ColorPoint);
      image.setPixel(bitmap,segments[s].p2.x,segments[s].p2.y,ColorPoint);
    end;
end;

procedure drawEmptyThickLine( const bitmap : PBitmap32;  const FromX, FromY, ToX, ToY: Integer;  const Color: TColor32;  const Thick : Integer); overload;
begin
  BresenhamEmptyThickLine(bitmap,FromX, FromY, ToX, ToY, Color, Thick);
end;

// inspired from : http://immortals.fake.hu/delphiportal/modules.php?name=News&file=article&sid=2372
// with correction of bugs, and adaptation
procedure drawDisk( const bitmap : PBitmap32;  const CenterX, CenterY, radius : Single;  const Color: TColor32; Feather : Single);
{Draw a disk on Bitmap. Bitmap must be a 256 color (pf8bit) palette bitmap, and parts outside the disk will get palette index 0, parts inside will get palette index 255, and in the antialiased area (feather), the pixels will get values in between.
Parameters:
Bitmap:
  The bitmap to draw on
CenterX, CenterY:
  The center of the disk (float precision). Note that [0, 0] would be the center of the first pixel. To draw in the exact middle of a 100x100 bitmap,  use CenterX = 49.5 and CenterY = 49.5
Radius:
  The radius of the drawn disk in pixels (float precision)
Feather:
  The feather area. Use 1 pixel for a 1-pixel antialiased area. Pixel centers outside 'Radius + Feather / 2' become 0, pixel centers inside 'Radius - Feather/2' become 255. Using a value of 0 will yield a bilevel image.
Copyright (c) 2003 Nils Haeck M.Sc. www.simdesign.nl}
var
  x, y: integer;
  LX, RX, LY, RY: integer;
  Fact: integer;
  RPF2, RMF2: single;
  P: PColor32Array;
  SqY, SqDist: single;
  sqX: array of single;
  f : Single;
begin
//Feather:=1;
  {Determine some helpful values (singles)}
  RPF2 := sqr(Radius + Feather / 2);
  RMF2 := sqr(Radius - Feather / 2);
  {Determine bounds:}
  LX := Max(floor(CenterX - Radius), 0);
  RX := Min(ceil(CenterX + Radius), Bitmap.Width - 1);
  LY := Max(floor(CenterY - Radius), 0);
  RY := Min(ceil(CenterY + Radius), Bitmap.Height - 1);
  {Optimization run: find squares of X first}
  SetLength(SqX, RX - LX + 1);
  for x := LX to RX do
    SqX[x - LX] := sqr(x - CenterX);
  {Loop through Y values}
  for y := LY to RY do
  begin
    P := bitmap.Bits;
    Inc( P, y*bitmap.Width );
    SqY := Sqr(y - CenterY);
    {Loop through X values}
    for x := LX to RX do
    begin
      {Determine squared distance from center for this pixel}
      SqDist := SqY + SqX[x - LX];
      {Inside inner circle? Most often...}
      if sqdist < RMF2 then
      begin
        {Inside the inner circle.. just give the scanline the new color}
        P[x] := Color
      end
      else
      begin
        {Inside outer circle?}
        if sqdist < RPF2 then
        begin
          //We are inbetween the inner and outer bound, now mix the color
          f := ((Radius - sqrt(sqdist)) * 2 / Feather);
          Fact := round( f * 127.5 + 127.5 );
          Fact := Max(0, Min(Fact, 255)); //just in case limit to [0, 255]
          f := Fact / 255; // convert to percent
          //P[x] := Fact;
          TColorRec(P[x]).Red := Round(TColorRec(P[x]).Red*(1.0-f) + TColorRec(Color).Red*f);
          TColorRec(P[x]).Green := Round(TColorRec(P[x]).Green*(1.0-f) + TColorRec(Color).Green*f);
          TColorRec(P[x]).Blue := Round(TColorRec(P[x]).Blue*(1.0-f) + TColorRec(Color).Blue*f);
        end
        else
        begin
          // let unchanged
        end;
      end;
    end;
  end;
end;

procedure drawCircle( const bitmap : PBitmap32; const CenterX, CenterY, Radius : Single; const Color: TColor32 );
var
  x, y, r: Integer;
  x1, y1, p: Integer;
  pbits : PColor32Array;
  bitmapWidth, bitmapHeight : Integer;

  procedure plotCircle(x, y, x1, y1: Integer);
  var
    tmpY, tmpX : Integer;
  begin
    tmpY := (y + y1);
    tmpX := (x + x1);
    if (tmpY<bitmapHeight) and (tmpX<bitmapWidth) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
    tmpY := (y + y1);
    tmpX := (x - x1);
    if (tmpY<bitmapHeight) and (tmpX>=0) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
    tmpY := (y - y1);
    tmpX := (x + x1);
    if (tmpY>=0) and (tmpX<bitmapWidth) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
    tmpY := (y - y1);
    tmpX := (x - x1);
    if (tmpY>=0) and (tmpX>=0) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
    tmpY := (y + x1);
    tmpX := (x + y1);
    if (tmpY<bitmapHeight) and (tmpX<bitmapWidth) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
    tmpY := (y + x1);
    tmpX := (x - y1);
    if (tmpY<bitmapHeight) and (tmpX>=0) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
    tmpY := (y - x1);
    tmpX := (x + y1);
    if (tmpY>=0) and (tmpX<bitmapWidth) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
    tmpY := (y - x1);
    tmpX := (x - y1);
    if (tmpY>=0) and (tmpX>=0) then
      pbits[ tmpY*bitmapWidth + tmpX ] := Color;
  end;

begin
  pbits := bitmap.Bits;
  bitmapWidth := bitmap.Width;
  bitmapHeight := bitmap.Height;
  x := round( CenterX );
  y := round( CenterY );
  r := round( Radius );
  x1 := 0;
  y1 := r;
  p := 3 - 2 * r;
  while ( x1 < y1 ) do
  begin
    plotcircle ( x, y, x1, y1) ;
    if ( p < 0 ) then
      p := p + 4 * x1 + 6
    else
      begin
        p := p + 4 * ( x1 - y1 ) + 10 ;
        y1 := y1 - 1 ;
      end;
    x1 := x1 + 1 ;
  end;
  if ( x1 = y1 ) then
    plotcircle ( x, y, x1, y1) ;
end;

{procedure drawDisk( const bitmap : PBitmap32;  const X, Y, radius: Single;  const Color: TColor32);
var
  roundRadius : Integer ;
  Radius2 : Single ;
  T, U, minT, minU, maxT, maxU : Integer ;
  D2 : Single ;
Begin
  roundRadius := Round(radius) ;
  Radius2 := radius * radius ;
  // limite les bord pour X
  if X<roundRadius then
    minT := round(-x)
  else
    minT := -roundRadius ;
  if X>=bitmap.Width-roundRadius Then
    maxT := bitmap.Width-round(X)-1
  else
    maxT := roundRadius ;
  // limite les bord pour Y
  if Y<roundRadius then
    minU := round(-Y)
  else
    minU := -roundRadius ;
  if Y>=bitmap.Height-roundRadius Then
    maxU := bitmap.Height-round(Y)-1
  else
    maxU := roundRadius ;

  for U := minU to maxU do Begin
    for T := minT to maxT do Begin
      D2 := U*U+T*T ;
      if Radius2 > d2 Then Begin
        //setPixel(bitmap, getValidX(bitmap,x+t), getValidY(bitmap,y+u), Color);
        setPixel(bitmap, x+t, y+u, Color);
      End ;
    end ;
  end ;
End ;}

procedure drawRect( const bitmap : PBitmap32; const r : TRect; const color : TColor32;  const thick : Single = 1);
begin
  drawLine(bitmap,r.Left,r.Top,r.Right,r.Top,color,thick);
  drawLine(bitmap,r.Right,r.Top,r.Right,r.Bottom,color,thick);
  drawLine(bitmap,r.Right,r.Bottom,r.Left,r.Bottom,color,thick);
  drawLine(bitmap,r.Left,r.Bottom,r.Left,r.Top,color,thick);
end;

procedure drawRect( const bitmap : PBitmap32; const r : TFRect; const color : TColor32;  const thick : Single = 1);
var
  rectSubImg : TRect;
begin
  rectSubImg.Left:=Round(r.Left);
  rectSubImg.Top:=Round(r.Top);
  rectSubImg.Right:=Round(r.Right);
  rectSubImg.Bottom:=Round(r.Bottom);
  drawRect(bitmap,rectSubImg,color,thick);
end;

// of course, to optimize
procedure drawRectFilled( const bitmap : PBitmap32; const r : TRect; const color : TColor32);
var
  row : Integer;
begin
  for row:=r.Top to r.Bottom do begin
    drawLine( bitmap, r.Left, row, r.Right, row, color );
  end;
end;

procedure drawRectFilled( const bitmap : PBitmap32; const r : TFRect; const color : TColor32);
var
  rect : TRect;
begin
  rect.Left := Floor( r.Left );
  rect.Top := Floor( r.Top );
  rect.Right := Floor( r.Right );
  rect.Bottom := Floor( r.Bottom );
  drawRectFilled( bitmap, rect, color);
end;

procedure resetNextColor();
begin
  _nextColorIndex:=-1;
end;

function getNextColor() : TColor32;
begin
  Inc(_nextColorIndex);
  if _nextColorIndex>=Length(_nextColorArray) then begin
    SetLength(_nextColorArray,_nextColorIndex+1);
    _nextColorArray[_nextColorIndex]:=Color32(random(255),random(255),random(255));
  end;
  Result:=_nextColorArray[_nextColorIndex];
end;


procedure addSegment(var segmentList:TSegmentList; const p1,p2:TFPoint; const width:Single);
var
  segmentListLength : Integer;
  segment : TSegment;
begin
  segmentListLength:=Length(segmentList);
  Inc(segmentListLength);
  SetLength(segmentList,segmentListLength);
  segment.p1:=p1;
  segment.p2:=p2;
  segment.width:=Abs(width);
  segmentList[segmentListLength-1]:=segment;
end;

function getWedge( const p0,p1:TFPoint; const lastWedge:TWedge; const epsylone:Single; const imageMonitoring : PBitmap32) : TWedge;
var
  wedge : TWedge;
  lineP0P1 : TLineFunction;
  vectorP0P1 : TVector;
  vectorPerpendicularP0P1 : TVector;
  vectorPerpendicularP0P1r : TVector;
  vectorP1WP1,vectorP1WP2 : TVector;
  r : Single;
begin
  wedge.p0:=p0;
  wedge.p1:=p1;
  lineP0P1:=getLine(p0,p1);
  vectorP0P1:=getVector(p0,p1);
  vectorPerpendicularP0P1:=getVectorPerpendicular(vectorP0P1);
  if vectorPerpendicularP0P1.length<>0 then begin
    r:=epsylone/vectorPerpendicularP0P1.length;
  end else begin
    r:=1;
  end;
  vectorPerpendicularP0P1r:=resizeVector(vectorPerpendicularP0P1,r);
  vectorP1WP1:=moveVector(vectorPerpendicularP0P1r,p1);
  vectorP1WP2:=invertVector(vectorP1WP1);
  wedge.pw1:=vectorP1WP1.toPoint;
  wedge.pw2:=vectorP1WP2.toPoint;
  wedge.line1:=getLine(p0,wedge.pw1);
  wedge.line2:=getLine(p0,wedge.pw2);
  if lastWedge.line1.a<>MaxSingle then begin
  end;
  // if monitoring
  if imageMonitoring<>nil then begin
    // draw vector
    drawLine(imageMonitoring,p0,p1,clYellow32,1);
    // draw wedge limit
    drawLine(imageMonitoring,p0,wedge.pw1,clFuchsia32);
    drawLine(imageMonitoring,p0,wedge.pw2,clFuchsia32);
  end;
  Result:=wedge;
end;


function getLine( const p0,p1:TFPoint) : TLineFunction;
var
  line : TLineFunction;
  dH, dV : Single;
begin
  dH := p0.x-p1.x;
  dV := p0.y-p1.y;
  if dH <> 0 then begin
    line.a := dV / dH;
    line.b := p0.y - (line.a*p0.x);
  end else begin
    line.a:=0;
    line.b:=0;
  end;
  Result:=line;
end;

function getLinePerpendicular( const line:TLineFunction; const p:TFPoint) : TLineFunction;
var
  linePerpendicular : TLineFunction;
begin
  if line.a <> 0 then begin
    linePerpendicular.a := -(1.0/line.a);
    linePerpendicular.b := p.y - (linePerpendicular.a * p.x);
  end else begin
    linePerpendicular.a:=0;
    linePerpendicular.b:=0;
  end;
  Result:=linePerpendicular;
end;

function getVector( const p0,p1:TFPoint) : TVector;
var
  vector : TVector;
begin
  vector.fromPoint:=p0;
  vector.toPoint:=p1;
  vector.length:=getDistance(p0,p1);
  Result:=vector;
end;

function getVectorPerpendicular( const vector : TVector) : TVector;
begin
  with Result do begin
    fromPoint := vector.fromPoint;
    toPoint.x := vector.fromPoint.x + (vector.toPoint.y-vector.fromPoint.y);
    toPoint.y := vector.fromPoint.y - (vector.toPoint.x-vector.fromPoint.x);
    length := vector.length;
  end;;
end;

function resizeVector( const vector : TVector;  const r : Single) : TVector;
var
  resizedVector : TVector;
  dx,dy:Single;
begin
  resizedVector.fromPoint:=vector.fromPoint;
  dx:=vector.toPoint.x-vector.fromPoint.x;
  resizedVector.toPoint.x:=vector.fromPoint.x+dx*r;
  dy:=vector.toPoint.y-vector.fromPoint.y;
  resizedVector.toPoint.y:=vector.fromPoint.y+dy*r;
  Result:=resizedVector;
end;

function moveVector( const vector : TVector;  const p : TFPoint) : TVector;
var
  movedVector : TVector;
begin
  movedVector.fromPoint:=p;
  movedVector.toPoint.x:=p.x+(vector.toPoint.x-vector.fromPoint.x);
  movedVector.toPoint.y:=p.y+(vector.toPoint.y-vector.fromPoint.y);
  movedVector.length:=vector.length;
  Result:=movedVector;
end;

function invertVector( const vector : TVector) : TVector;
var
  invertedVector : TVector;
begin
  invertedVector.fromPoint:=vector.fromPoint;
  invertedVector.toPoint.x:=vector.fromPoint.x-(vector.toPoint.x-vector.fromPoint.x);
  invertedVector.toPoint.y:=vector.fromPoint.y-(vector.toPoint.y-vector.fromPoint.y);
  invertedVector.length:=vector.length;
  Result:=invertedVector;
end;

function getDistance( const p1,p2:TFPoint) : Single;
var
  distance : Single;
begin
  distance:=sqrt(sqr(p2.x-p1.x)+sqr(p2.y-p1.y));
  Result:=distance;
end;

function isPointInWedge( const point : TFPoint;  const wedge : TWedge) : boolean;
var
  yW1,yW2 : Single;
begin
  Result:=true;
  // if we are in the special case where the line is a pure vertical
 if (wedge.line1.a=0) and (wedge.line1.b=0) then begin
  if point.x<>wedge.p0.x then begin
    Result:=false;
  end;
 end else begin
  // in all other case
  yW1:=wedge.line1.a*point.x+wedge.line1.b;
  if wedge.p0.x<wedge.pw1.x then begin
    if point.y<yW1 then begin
      Result:=false;
    end;
  end else begin
    if point.y>yW1 then begin
      Result:=false;
    end;
  end;
  yW2:=wedge.line2.a*point.x+wedge.line2.b;
  if wedge.p0.x<wedge.pw2.x then begin
    if point.y>yW2 then begin
      Result:=false;
    end;
  end else begin
    if point.y<yW2 then begin
      Result:=false;
    end;
  end;
 end;
end;

// http://fr.wikipedia.org/wiki/Coordonn%C3%A9es_polaires
function convertToVector(s : TSegment) : TFVector;
var
  vector : TFVector;
  x,y : Single;
  signY : Integer;
begin
  vector.point:=s.p1;
  vector.length:=getDistance(s.p1,s.p2);
  x:=s.p2.x-s.p1.x;
  y:=s.p2.y-s.p1.y;
  if x<>0 then begin
    vector.angle:=ArcTan(y/x);
  end else begin
    vector.angle:=_PI2;
  end;
  signY:=1;
  if y<0 then begin
    signY:=-1;
  end;
  if x<0 then begin
    vector.angle:=vector.angle + PI * signY;
  end;
  if vector.angle<0 then begin
    vector.angle:=_2PI + vector.angle;
  end;
  Result:=vector;
end;

procedure setWeight(image:PBitmap32 ; x, y : Word; weight:ShortInt) ;
var
  pbits : PColor32Array ;
Begin
  pbits := image.Bits ;
  inc(pbits, y*image.Width+x) ;
  pbits[0] := Gray32(weight+128) ;
End ;

function getNullRect() : TFRect;
begin
  with Result do begin
    Top := 0;
    Left := 0;
    Bottom := 0;
    Right := 0;
  end;
end;

function isRectValid( const R: TFRect ): Boolean;
begin
  with R do
    Result := (Left <= Right) and (Top <= Bottom);
end;

// inspired from Project JEDI Code Library (JCL), JclQGraphUtils.pas
function getRectIntersection( const R1, R2: TFRect ) : TFRect;
begin
  with Result do begin
    Left := Math.Max( R1.Left, R2.Left );
    Top := Math.Max( R1.Top, R2.Top );
    Right := Math.Min( R1.Right, R2.Right );
    Bottom := Math.Min( R1.Bottom, R2.Bottom );
  end;
  if isRectValid( Result ) = false then begin
    Result := getNullRect();
  end;
end;

function getRectPerimeter( const R : TFRect ) : Single;
begin
  with R do
    Result := (Right-Left) * (Bottom-Top);
end;


initialization
begin
 _onePixelImage := image.createImage(1,1);
end;

finalization
begin
  freeImage( _onePixelImage );
end;

end.






