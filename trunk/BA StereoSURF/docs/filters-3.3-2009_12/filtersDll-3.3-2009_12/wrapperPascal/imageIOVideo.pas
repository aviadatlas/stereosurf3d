unit imageIOVideo;
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
 eburgel (filters@burgel.com)
}

interface
uses
  image, Graphics, SysUtils, ExtCtrls;

procedure showImage( const aImage:PBitmap32; const aViewer : TImage);
function createImageFromBitmap( const aBitmap:graphics.TBitmap ) : PBitmap32;


implementation

uses Windows;


procedure showImage( const aImage:PBitmap32; const aViewer : TImage);
var
  row : Integer;
  pSrc, pDest : PColor32Array;
  b : graphics.TBitmap;
begin
  if aImage<>nil then begin
    b:=aViewer.Picture.Bitmap;
    b.PixelFormat := pf32bit;
    b.Width  := aImage.Width;
    b.Height := aImage.Height;
    pSrc:=aImage.Bits ;

    //OutputDebugString(PChar(intToStr(Cardinal(b.ScanLine[0]))));
    for row:=0 to b.Height-1 do
    begin
      pDest:=b.ScanLine[row];
      Move(pSrc^, pDest^,SizeOfColor32*b.Width);
      inc(pSrc, b.Width) ;
    end;

    aViewer.Invalidate;
    aViewer.Refresh ;
  end;
end;

function createImageFromBitmap(const aBitmap:Graphics.TBitmap) : PBitmap32;
Type
  TRGBRecord = record
     B,G,R : Byte;
  end;
  TRGBMatrix = array[0..32767] of TRGBRecord;
  pRGBMatrix = ^TRGBMatrix;
var
  img : PBitmap32;
  row, col : Integer;
  PDest : PColor32Array;
  PSrc : pRGBMatrix;
  value:Integer;
begin
  aBitmap.PixelFormat := pf24Bit;
  img := image.createImage(aBitmap.Width, aBitmap.Height);
  pDest := img.Bits;
  for row:=0 to aBitmap.Height-1 do begin
    PSrc := aBitmap.ScanLine[row];
    for col:=0 to aBitmap.Width-1 do begin
      value := image.Color32( PSrc[Col].R, PSrc[Col].G, PSrc[Col].B );
      pDest^[0]:= value;
      Inc(pDest);
    end;
  end;
  Result := img;
end;

procedure convertToBitmap(Source : TGraphic; Bitmap : graphics.TBitmap);
begin
 if Bitmap = nil then
  Bitmap := graphics.TBitmap.Create
 else Bitmap.FreeImage;
 Bitmap.Width := Source.Width;
 Bitmap.Height := Source.Height;
 Bitmap.Canvas.Draw(0,0,Source);
end;


end.
