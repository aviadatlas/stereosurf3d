// wrapper_filtersDll.h
/*
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
*/

/*
 edurand (filters@edurand.com)
*/


#if !defined(__wrapper_filtersDll_h)
#define __wrapper_filtersDll_h


#include "windows.h"


typedef unsigned __int32 TFColor32;
typedef TFColor32* PFColor32;

typedef struct _TFBitmap32
{
	__int32 Width;
	__int32 Height;
	TFColor32 *Bits;
} TFBitmap32, *PFBitmap32;

typedef PFBitmap32* ArrayOfPFBitmap32;
typedef ArrayOfPFBitmap32* PArrayOfPFBitmap32;

typedef void* Pointer;
typedef Pointer* ArrayOfPointer;
typedef ArrayOfPointer* PArrayOfPointer;

typedef struct _TFPoint
{
    float x,y;
} TFPoint, *PFPoint;

typedef struct _TFSegment
{
    TFPoint p1, p2;
    float width;
} TFSegment, *PFSegment;

typedef TFSegment* TFSegmentList;

typedef struct _TFVector
{
    TFPoint point;
    float length;
    float angle;
} TFVector, *PFVector;

typedef TFVector* TFVectorList;

typedef struct _TFRect
{
	float Left, Top, Right, Bottom;
} TFRect, *PFRect;

typedef struct _TIRect
{
	__int32 Left, Top, Right, Bottom;
} TIRect, *PIRect;

typedef struct _TFBlob
{
	__int32 index;
	TFColor32 color;
	TFSegmentList segmentList;
	__int32 length_segmentList;
    TFSegmentList approximatedSegmentList;
	__int32 length_approximatedSegmentList;
	TFVectorList vectorChain;
	__int32 length_vectorChain;
    TFRect rectangleContainer;
    __int32 pixelArea;
    TFPoint gravityCenter;
    float perimeter;
} TFBlob, *PFBlob;


typedef char* (CALLBACK* LPFNDLL_SETPROPERTY)( char*, char* );
typedef char* (CALLBACK* LPFNDLL_INITIALIZE)();
typedef char* (CALLBACK* LPFNDLL_UNINITIALIZE)();

typedef char* (CALLBACK* LPFNDLL_GETVERSION)();
typedef char* (CALLBACK* LPFNDLL_GETLASTERROR)();
typedef __int32 (CALLBACK* LPFNDLL_GETFILTERSCOUNT)();
typedef char* (CALLBACK* LPFNDLL_GETFILTERSNAMEATINDEX)( __int32 );

typedef __int32 (CALLBACK* LPFNDLL_CREATEFILTER)( char* );
typedef void (CALLBACK* LPFNDLL_DELETEFILTER)( __int32 );

typedef void (CALLBACK* LPFNDLL_RUN)( __int32 );
typedef void (CALLBACK* LPFNDLL_RUNCOMMAND)( __int32, char* );

typedef __int32 (CALLBACK* LPFNDLL_GETPARAMETERSCOUNT)( __int32 );
typedef void (CALLBACK* LPFNDLL_GETPARAMETERNAME)( __int32, __int32, char* );
typedef void (CALLBACK* LPFNDLL_GETPARAMETERHELP)( __int32, __int32, char* );
typedef void (CALLBACK* LPFNDLL_SETPARAMETERSTRING)( __int32, char*, char* );
typedef void (CALLBACK* LPFNDLL_SETPARAMETERIMAGE)( __int32, char*, PFBitmap32 );
typedef void (CALLBACK* LPFNDLL_SETPARAMETERIMAGES)( __int32, char*, PArrayOfPFBitmap32 );
typedef void (CALLBACK* LPFNDLL_SETPARAMETERINTEGER)( __int32, char*, __int64 );
typedef void (CALLBACK* LPFNDLL_SETPARAMETERFLOAT)( __int32, char*, float );
typedef void (CALLBACK* LPFNDLL_SETPARAMETERBOOLEAN)( __int32, char*, __int8 );
typedef void (CALLBACK* LPFNDLL_SETPARAMETERPOINTER)( __int32, char*, void* );

typedef __int32 (CALLBACK* LPFNDLL_GETOUTPUTSCOUNT)( __int32 );
typedef void (CALLBACK* LPFNDLL_GETOUTPUTNAME)( __int32, __int32, char* );
typedef __int32 (CALLBACK* LPFNDLL_GETOUTPUTINTEGER)( __int32, char* );
typedef float (CALLBACK* LPFNDLL_GETOUTPUTFLOAT)( __int32, char* );
typedef PFBitmap32 (CALLBACK* LPFNDLL_GETOUTPUTIMAGE)( __int32, char* );
typedef PArrayOfPFBitmap32 (CALLBACK* LPFNDLL_GETOUTPUTIMAGES)( __int32, char* );
typedef PArrayOfPointer (CALLBACK* LPFNDLL_GETOUTPUTARRAYPOINTERS)( __int32, char* );
typedef __int32 (CALLBACK* LPFNDLL_GETOUTPUTARRAYPOINTERSLENGTH)( __int32, char* );

typedef void (CALLBACK* LPFNDLL_SETREGIONOFINTEREST)( __int32, PIRect );
typedef void (CALLBACK* LPFNDLL_UNSETREGIONOFINTEREST)( __int32 );

typedef PFBitmap32 (CALLBACK* LPFNDLL_IMAGE_CREATEIMAGELIKE)( PFBitmap32 );
typedef PFBitmap32 (CALLBACK* LPFNDLL_IMAGE_CREATEIMAGE)( __int32, __int32 );
typedef PFBitmap32 (CALLBACK* LPFNDLL_IMAGE_CREATEIMAGEFROMIMAGE)( PFBitmap32 );
typedef PFBitmap32 (CALLBACK* LPFNDLL_IMAGE_CREATEIMAGETEST)( __int32, __int32 );
typedef PFBitmap32 (CALLBACK* LPFNDLL_IMAGE_ERASEORCREATEIMAGELIKE)( PFBitmap32, PFBitmap32 );
typedef void (CALLBACK* LPFNDLL_IMAGE_ERASEIMAGE)( PFBitmap32 );
typedef void (CALLBACK* LPFNDLL_IMAGE_COPYIMAGETOIMAGE)( PFBitmap32, PFBitmap32 );
typedef void (CALLBACK* LPFNDLL_IMAGE_FREEIMAGE)( PFBitmap32 );
typedef void (CALLBACK* LPFNDLL_IMAGE_FREEIMAGES)( PArrayOfPFBitmap32 );

typedef __int32 (CALLBACK* LPFNDLL_IMAGE_GETIMAGESCOUNT)( PArrayOfPFBitmap32 );
typedef PFBitmap32 (CALLBACK* LPFNDLL_IMAGE_GETIMAGESIMAGEATINDEX)( PArrayOfPFBitmap32, __int32 );

typedef unsigned __int8 (CALLBACK* LPFNDLL_IMAGE_INTENSITY)( TFColor32 );
typedef unsigned __int8 (CALLBACK* LPFNDLL_IMAGE_LUMINOSITY)( TFColor32 );
typedef unsigned __int8 (CALLBACK* LPFNDLL_IMAGE_REDCOMPONENT)( TFColor32 );
typedef unsigned __int8 (CALLBACK* LPFNDLL_IMAGE_GREENCOMPONENT)( TFColor32 );
typedef unsigned __int8 (CALLBACK* LPFNDLL_IMAGE_BLUECOMPONENT)( TFColor32 );
typedef TFColor32 (CALLBACK* LPFNDLL_IMAGE_COLOR32)( __int8, __int8, __int8 );
typedef TFColor32 (CALLBACK* LPFNDLL_IMAGE_GRAY32)( __int8 );

typedef void (CALLBACK* LPFNDLL_IMAGE_SETPIXEL)( PFBitmap32, __int32, __int32, TFColor32  );
typedef void (CALLBACK* LPFNDLL_IMAGE_SETPIXELASSINGLE)( PFBitmap32, __int32, __int32, float );
typedef TFColor32 (CALLBACK* LPFNDLL_IMAGE_GETPIXEL)( PFBitmap32, __int32, __int32 );
typedef float (CALLBACK* LPFNDLL_IMAGE_GETPIXELASSINGLE)( PFBitmap32, __int32, __int32 );
typedef char* (CALLBACK* LPFNDLL_IMAGE_GETPIXELASSTRING)( PFBitmap32, __int32, __int32, char* );
typedef void (CALLBACK* LPFNDLL_IMAGE_DRAWLINE)( PFBitmap32, __int32, __int32, __int32, __int32, TFColor32, float );
typedef void (CALLBACK* LPFNDLL_IMAGE_DRAWLINESEGMENT)( PFBitmap32, PFSegment, TFColor32, float );
typedef void (CALLBACK* LPFNDLL_IMAGE_DRAWRECT)( PFBitmap32, PFRect, TFColor32, float aThick );
typedef void (CALLBACK* LPFNDLL_IMAGE_DRAWRECTFILLED)( PFBitmap32, PFRect, TFColor32 );
typedef void (CALLBACK* LPFNDLL_IMAGE_DRAWDISK)( PFBitmap32, float, float, float, TFColor32 );
typedef TFColor32 (CALLBACK* LPFNDLL_IMAGE_HSLtoRGB)( float, float, float );
typedef __int8 (CALLBACK* LPFNDLL_IMAGE_ISSAMESIZE)( PFBitmap32, PFBitmap32 );
typedef void (CALLBACK* LPFNDLL_IMAGE_GETVALIDROI)( PFBitmap32, PFRect, PFRect );


const char filters_true = 1;
const char filters_false = 0;


void filters_setProperty( char*, char* );
void filters_initialize( void );
void filters_unInitialize( void );

char* filters_getVersion( void );
char* filters_getLastError( void );

__int32 filters_getFiltersCount( void );
char* filters_getFiltersNameAtIndex( __int32 aIndexFilter );

__int32 filters_createFilter( char* aFilterName );
void filters_deleteFilter( __int32 aFilterInstance );

void filters_run( __int32 aFilterInstance );
void filters_runCommand( __int32 aFilterInstance, char* aCommand );

__int32 filters_getParametersCount( __int32 aFilterInstance );
void filters_getParameterName( __int32 aFilterInstance, __int32 aIndexParameter, char* aParameterName );
void filters_getParameterHelp( __int32 aFilterInstance, __int32 aIndexParameter, char* aParameterName );
void filters_setParameterString( __int32 aFilterInstance, char* aParameterName, char* aString );
void filters_setParameterImage( __int32 aFilterInstance, char* aParameterName, PFBitmap32 aImage );
void filters_setParameterImages( __int32 aFilterInstance, char* aParameterName, PArrayOfPFBitmap32 aImages );
void filters_setParameterInteger( __int32 aFilterInstance, char* aParameterName, __int64 aInteger );
void filters_setParameterFloat( __int32 aFilterInstance, char* aParameterName, float aFloat );
void filters_setParameterBoolean( __int32 aFilterInstance, char* aParameterName, __int8 aBoolean );
void filters_setParameterPointer( __int32 aFilterInstance, char* aParameterName, void* aPointer );

__int32 filters_getOutputsCount( __int32 aFilterInstance );
void filters_getOutputName( __int32 aFilterInstance, __int32 aIndexOutput, char* aOutputName );
__int32 filters_getOutputInteger( __int32 aFilterInstance, char* aOutputName );
float filters_getOutputFloat( __int32 aFilterInstance, char* aOutputName );
PFBitmap32 filters_getOutputImage( __int32 aFilterInstance, char* aOutputName );
PArrayOfPFBitmap32 filters_getOutputImages( __int32 aFilterInstance, char* aOutputName );
PArrayOfPointer filters_getOutputArrayPointers( __int32 aFilterInstance, char* aOutputName );
__int32 filters_getOutputArrayPointersLength( __int32 aFilterInstance, char* aOutputName );

void filters_setRegionOfInterest( __int32 aFilterInstance, PIRect roi );
void filters_unsetRegionOfInterest( __int32 aFilterInstance );

PFBitmap32 image_createImageLike( PFBitmap32 aImage );
PFBitmap32 image_createImageTest( __int32 aWidth, __int32 aHeight );
PFBitmap32 image_createImage( __int32 aWidth, __int32 aHeight );
PFBitmap32 image_createImageFromImage( PFBitmap32 aImage );
PFBitmap32 image_eraseOrCreateImageLike( PFBitmap32 lastImage, PFBitmap32 aImage );
void image_eraseImage( PFBitmap32 aImage );
void image_copyImageToImage( PFBitmap32 aImageFrom, PFBitmap32 aImageTo );
void image_freeImage( PFBitmap32 aImage );
void image_freeImages( PArrayOfPFBitmap32 aImages );

__int32 image_getImagesCount( PArrayOfPFBitmap32 aImages );
PFBitmap32 image_getImagesImageAtIndex( PArrayOfPFBitmap32 aImages , __int32 aIndex );

unsigned __int8 image_intensity( TFColor32 aColor32 );
unsigned __int8 image_luminosity( TFColor32 aColor32 );
unsigned __int8 image_redComponent( TFColor32 aColor32 );
unsigned __int8 image_greenComponent( TFColor32 aColor32 );
unsigned __int8 image_blueComponent( TFColor32 aColor32 );
TFColor32 image_color32( __int8 R, __int8 G, __int8 B );
TFColor32 image_gray32( __int8 intensity );

void image_setPixel( PFBitmap32 aImage, __int32 x, __int32 y, TFColor32 aColor32 );
void image_setPixelAsSingle( PFBitmap32 aImage, __int32 x, __int32 y, float data );
TFColor32 image_getPixel( PFBitmap32 aImage, __int32 x, __int32 y );
float image_getPixelAsSingle( PFBitmap32 aImage, __int32 x, __int32 y );
void image_getPixelAsString( PFBitmap32 aImage, __int32 x, __int32 y, char* aStr );

void image_drawLine( PFBitmap32 aImage, __int32 FromX, __int32 FromY, __int32 ToX, __int32 ToY, TFColor32 aColor32, float aThick );
void image_drawLineSegment( PFBitmap32 aImage, PFSegment aSegment, TFColor32 aColor32, float aThick );
void image_drawRect( PFBitmap32 aImage, PFRect aRect, TFColor32 aColor32, float aThick );
void image_drawRectFilled( PFBitmap32 aImage, PFRect aRect, TFColor32 aColor32 );
void image_drawDisk( PFBitmap32 aImage, float aCenterX, float aCenterY, float aradius, TFColor32 aColor32 );
TFColor32 image_HSLtoRGB( float H, float S, float L );
__int8 image_isSameSize( PFBitmap32 aImage1, PFBitmap32 aImage2 );
void image_getValidROI( PFBitmap32 aImage, PFRect aROI, PFRect aValidROI );

PFBitmap32 helper_loadImage( char* );
void helper_saveImage( PFBitmap32, char* );


// Some predefined color constants
const TFColor32 clBlack32 = 0xFF000000;
const TFColor32 clDimGray32= 0xFF3F3F3F;
const TFColor32 clGray32= 0xFF7F7F7F;
const TFColor32 clLightGray32= 0xFFBFBFBF;
const TFColor32 clWhite32= 0xFFFFFFFF;
const TFColor32 clMaroon32= 0xFF7F0000;
const TFColor32 clGreen32= 0xFF007F00;
const TFColor32 clOlive32= 0xFF7F7F00;
const TFColor32 clNavy32= 0xFF00007F;
const TFColor32 clPurple32= 0xFF7F007F;
const TFColor32 clTeal32= 0xFF007F7F;
const TFColor32 clRed32= 0xFFFF0000;
const TFColor32 clLime32= 0xFF00FF00;
const TFColor32 clYellow32= 0xFFFFFF00;
const TFColor32 clBlue32= 0xFF0000FF;
const TFColor32 clFuchsia32= 0xFFFF00FF;
const TFColor32 clAqua32= 0xFF00FFFF;


#endif