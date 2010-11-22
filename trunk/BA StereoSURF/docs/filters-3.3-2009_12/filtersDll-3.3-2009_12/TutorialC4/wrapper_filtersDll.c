// wrapper_filtersDll.c
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
 *   mailto:filters@edurand.com
 *   mailto:filters@burgel.com
 * Site :
 *   http://filters.sourceforge.net/
 *
 * ***** END LICENSE BLOCK ***** *)
*/

/*
 edurand (filters@edurand.com)
*/


#include "wrapper_filtersDll.h"


HINSTANCE hFiltersDll;

LPFNDLL_SETPROPERTY _filters_setProperty = NULL;
LPFNDLL_INITIALIZE _filters_initialize = NULL;
LPFNDLL_UNINITIALIZE _filters_unInitialize = NULL;

LPFNDLL_GETVERSION _filters_getVersion = NULL;
LPFNDLL_GETLASTERROR _filters_getLastError = NULL;

LPFNDLL_GETFILTERSCOUNT _filters_getFiltersCount;
LPFNDLL_GETFILTERSNAMEATINDEX _filters_getFiltersNameAtIndex;

LPFNDLL_CREATEFILTER _filters_createFilter;
LPFNDLL_DELETEFILTER _filters_deleteFilter;

LPFNDLL_RUN _filters_run;
LPFNDLL_RUNCOMMAND _filters_runCommand;

LPFNDLL_GETPARAMETERSCOUNT _filters_getParametersCount;
LPFNDLL_GETPARAMETERNAME _filters_getParameterName;
LPFNDLL_GETPARAMETERHELP _filters_getParameterHelp;
LPFNDLL_SETPARAMETERSTRING _filters_setParameterString;
LPFNDLL_SETPARAMETERIMAGE _filters_setParameterImage;
LPFNDLL_SETPARAMETERIMAGES _filters_setParameterImages;
LPFNDLL_SETPARAMETERINTEGER _filters_setParameterInteger;
LPFNDLL_SETPARAMETERFLOAT _filters_setParameterFloat;
LPFNDLL_SETPARAMETERBOOLEAN _filters_setParameterBoolean;
LPFNDLL_SETPARAMETERPOINTER _filters_setParameterPointer;

LPFNDLL_GETOUTPUTSCOUNT _filters_getOutputsCount;
LPFNDLL_GETOUTPUTNAME _filters_getOutputName;
LPFNDLL_GETOUTPUTINTEGER _filters_getOutputInteger;
LPFNDLL_GETOUTPUTFLOAT _filters_getOutputFloat;
LPFNDLL_GETOUTPUTIMAGE _filters_getOutputImage;
LPFNDLL_GETOUTPUTIMAGES _filters_getOutputImages;
LPFNDLL_GETOUTPUTARRAYPOINTERS _filters_getOutputArrayPointers;
LPFNDLL_GETOUTPUTARRAYPOINTERSLENGTH _filters_getOutputArrayPointersLength;

LPFNDLL_SETREGIONOFINTEREST _filters_setRegionOfInterest;
LPFNDLL_UNSETREGIONOFINTEREST _filters_unsetRegionOfInterest;

LPFNDLL_IMAGE_CREATEIMAGE _image_createImage;
LPFNDLL_IMAGE_CREATEIMAGELIKE _image_createImageLike;
LPFNDLL_IMAGE_CREATEIMAGETEST _image_createImageTest;
LPFNDLL_IMAGE_CREATEIMAGEFROMIMAGE _image_createImageFromImage;
LPFNDLL_IMAGE_ERASEORCREATEIMAGELIKE _image_eraseOrCreateImageLike;
LPFNDLL_IMAGE_ERASEIMAGE _image_eraseImage;
LPFNDLL_IMAGE_COPYIMAGETOIMAGE _image_copyImageToImage;
LPFNDLL_IMAGE_FREEIMAGE _image_freeImage;
LPFNDLL_IMAGE_FREEIMAGES _image_freeImages;

LPFNDLL_IMAGE_GETIMAGESCOUNT _image_getImagesCount;
LPFNDLL_IMAGE_GETIMAGESIMAGEATINDEX _image_getImagesImageAtIndex;

LPFNDLL_IMAGE_INTENSITY _image_intensity;
LPFNDLL_IMAGE_LUMINOSITY _image_luminosity;
LPFNDLL_IMAGE_REDCOMPONENT _image_redComponent;
LPFNDLL_IMAGE_GREENCOMPONENT _image_greenComponent;
LPFNDLL_IMAGE_BLUECOMPONENT _image_blueComponent;
LPFNDLL_IMAGE_COLOR32 _image_color32;
LPFNDLL_IMAGE_GRAY32 _image_gray32;

LPFNDLL_IMAGE_SETPIXEL _image_setPixel;
LPFNDLL_IMAGE_SETPIXELASSINGLE _image_setPixelAsSingle;
LPFNDLL_IMAGE_GETPIXEL _image_getPixel;
LPFNDLL_IMAGE_GETPIXELASSINGLE _image_getPixelAsSingle;
LPFNDLL_IMAGE_GETPIXELASSTRING _image_getPixelAsString;

LPFNDLL_IMAGE_DRAWLINE _image_drawLine;
LPFNDLL_IMAGE_DRAWLINESEGMENT _image_drawLineSegment;
LPFNDLL_IMAGE_DRAWRECT _image_drawRect;
LPFNDLL_IMAGE_DRAWRECTFILLED _image_drawRectFilled;
LPFNDLL_IMAGE_DRAWDISK _image_drawDisk;
LPFNDLL_IMAGE_HSLtoRGB _image_HSLtoRGB;
LPFNDLL_IMAGE_ISSAMESIZE _image_isSameSize;
LPFNDLL_IMAGE_GETVALIDROI _image_getValidROI;


char properties[1024][2][10];
int properties_count = 0;
void filters_setProperty( char* aName, char* aValue )
{
	strcpy( properties[0][properties_count], aName );
	strcpy( properties[1][properties_count], aValue );
	properties_count++;
}

void filters_initialize(void)
{
	int i;
	hFiltersDll = LoadLibrary( "filtersDll.dll" );
	if (hFiltersDll != NULL)
	{
	   _filters_getVersion = (LPFNDLL_GETVERSION)GetProcAddress( hFiltersDll, "getVersion" );
	   if (!_filters_getVersion)
	   {
		  FreeLibrary( hFiltersDll );
	   }else{
			_filters_setProperty = (LPFNDLL_SETPROPERTY)GetProcAddress( hFiltersDll, "setProperty" );
			_filters_initialize = (LPFNDLL_INITIALIZE)GetProcAddress( hFiltersDll, "initialize" );
			_filters_unInitialize = (LPFNDLL_UNINITIALIZE)GetProcAddress( hFiltersDll, "unInitialize" );
			_filters_getLastError = (LPFNDLL_GETLASTERROR)GetProcAddress( hFiltersDll, "getLastError" );
			_filters_getFiltersCount = (LPFNDLL_GETFILTERSCOUNT)GetProcAddress( hFiltersDll, "getFiltersCount" );
			_filters_getFiltersNameAtIndex = (LPFNDLL_GETFILTERSNAMEATINDEX)GetProcAddress( hFiltersDll, "getFiltersNameAtIndex" );
			_filters_createFilter = (LPFNDLL_CREATEFILTER)GetProcAddress( hFiltersDll, "createFilter" );
			_filters_deleteFilter = (LPFNDLL_DELETEFILTER)GetProcAddress( hFiltersDll, "deleteFilter" );
			_filters_run = (LPFNDLL_RUN)GetProcAddress( hFiltersDll, "run" );
			_filters_runCommand = (LPFNDLL_RUNCOMMAND)GetProcAddress( hFiltersDll, "runCommand" );
			_filters_getParametersCount = (LPFNDLL_GETPARAMETERSCOUNT)GetProcAddress( hFiltersDll, "getParametersCount" );
			_filters_getParameterName = (LPFNDLL_GETPARAMETERNAME)GetProcAddress( hFiltersDll, "getParameterName" );
			_filters_getParameterHelp = (LPFNDLL_GETPARAMETERHELP)GetProcAddress( hFiltersDll, "getParameterHelp" );
			_filters_setParameterString = (LPFNDLL_SETPARAMETERSTRING)GetProcAddress( hFiltersDll, "setParameterString" );
			_filters_setParameterImage = (LPFNDLL_SETPARAMETERIMAGE)GetProcAddress( hFiltersDll, "setParameterImage" );
			_filters_setParameterImages = (LPFNDLL_SETPARAMETERIMAGES)GetProcAddress( hFiltersDll, "setParameterImages" );
			_filters_setParameterInteger = (LPFNDLL_SETPARAMETERINTEGER)GetProcAddress( hFiltersDll, "setParameterInteger" );
			_filters_setParameterFloat = (LPFNDLL_SETPARAMETERFLOAT)GetProcAddress( hFiltersDll, "setParameterFloat" );
			_filters_setParameterBoolean = (LPFNDLL_SETPARAMETERBOOLEAN)GetProcAddress( hFiltersDll, "setParameterBoolean" );
			_filters_setParameterPointer = (LPFNDLL_SETPARAMETERPOINTER)GetProcAddress( hFiltersDll, "setParameterPointer" );
			_filters_getOutputsCount = (LPFNDLL_GETOUTPUTSCOUNT)GetProcAddress( hFiltersDll, "getOutputsCount" );
			_filters_getOutputName = (LPFNDLL_GETOUTPUTNAME)GetProcAddress( hFiltersDll, "getOutputName" );
			_filters_getOutputInteger = (LPFNDLL_GETOUTPUTINTEGER)GetProcAddress( hFiltersDll, "getOutputInteger" );
			_filters_getOutputFloat = (LPFNDLL_GETOUTPUTFLOAT)GetProcAddress( hFiltersDll, "getOutputFloat" );
			_filters_getOutputImage = (LPFNDLL_GETOUTPUTIMAGE)GetProcAddress( hFiltersDll, "getOutputImage" );
			_filters_getOutputImages = (LPFNDLL_GETOUTPUTIMAGES)GetProcAddress( hFiltersDll, "getOutputImages" );
			_filters_getOutputArrayPointers = (LPFNDLL_GETOUTPUTARRAYPOINTERS)GetProcAddress( hFiltersDll, "getOutputArrayPointers" );
			_filters_getOutputArrayPointersLength = (LPFNDLL_GETOUTPUTARRAYPOINTERSLENGTH)GetProcAddress( hFiltersDll, "getOutputArrayPointersLength" );
			_filters_setRegionOfInterest = (LPFNDLL_SETREGIONOFINTEREST)GetProcAddress( hFiltersDll, "setRegionOfInterest" );
			_filters_unsetRegionOfInterest = (LPFNDLL_UNSETREGIONOFINTEREST)GetProcAddress( hFiltersDll, "unsetRegionOfInterest" );
			_image_createImageLike = (LPFNDLL_IMAGE_CREATEIMAGELIKE)GetProcAddress( hFiltersDll, "image_createImageLike" );
			_image_createImageTest = (LPFNDLL_IMAGE_CREATEIMAGETEST)GetProcAddress( hFiltersDll, "image_createImageTest" );
			_image_createImage = (LPFNDLL_IMAGE_CREATEIMAGE)GetProcAddress( hFiltersDll, "image_createImage" );
			_image_createImageFromImage = (LPFNDLL_IMAGE_CREATEIMAGEFROMIMAGE)GetProcAddress( hFiltersDll, "image_createImageFromImage" );
			_image_eraseOrCreateImageLike = (LPFNDLL_IMAGE_ERASEORCREATEIMAGELIKE)GetProcAddress( hFiltersDll, "image_eraseOrCreateImageLike" );
			_image_eraseImage = (LPFNDLL_IMAGE_ERASEIMAGE)GetProcAddress( hFiltersDll, "image_eraseImage" );
			_image_copyImageToImage = (LPFNDLL_IMAGE_COPYIMAGETOIMAGE)GetProcAddress( hFiltersDll, "image_copyImageToImage" );
			_image_freeImage = (LPFNDLL_IMAGE_FREEIMAGE)GetProcAddress( hFiltersDll, "image_freeImage" );
			_image_freeImages = (LPFNDLL_IMAGE_FREEIMAGES)GetProcAddress( hFiltersDll, "image_freeImages" );
			_image_getImagesCount = (LPFNDLL_IMAGE_GETIMAGESCOUNT)GetProcAddress( hFiltersDll, "image_getImagesCount" );
			_image_getImagesImageAtIndex = (LPFNDLL_IMAGE_GETIMAGESIMAGEATINDEX)GetProcAddress( hFiltersDll, "image_getImagesImageAtIndex" );
			_image_intensity = (LPFNDLL_IMAGE_INTENSITY)GetProcAddress( hFiltersDll, "image_intensity" );
			_image_luminosity = (LPFNDLL_IMAGE_LUMINOSITY)GetProcAddress( hFiltersDll, "image_luminosity" );
			_image_redComponent = (LPFNDLL_IMAGE_REDCOMPONENT)GetProcAddress( hFiltersDll, "image_redComponent" );
			_image_greenComponent = (LPFNDLL_IMAGE_GREENCOMPONENT)GetProcAddress( hFiltersDll, "image_greenComponent" );
			_image_blueComponent = (LPFNDLL_IMAGE_BLUECOMPONENT)GetProcAddress( hFiltersDll, "image_blueComponent" );
			_image_color32 = (LPFNDLL_IMAGE_COLOR32)GetProcAddress( hFiltersDll, "image_color32" );
			_image_gray32 = (LPFNDLL_IMAGE_GRAY32)GetProcAddress( hFiltersDll, "image_gray32" );
			_image_setPixel = (LPFNDLL_IMAGE_SETPIXEL)GetProcAddress( hFiltersDll, "image_setPixel" );
			_image_setPixelAsSingle = (LPFNDLL_IMAGE_SETPIXELASSINGLE)GetProcAddress( hFiltersDll, "image_setPixelAsSingle" );
			_image_getPixel = (LPFNDLL_IMAGE_GETPIXEL)GetProcAddress( hFiltersDll, "image_getPixel" );
			_image_getPixelAsSingle = (LPFNDLL_IMAGE_GETPIXELASSINGLE)GetProcAddress( hFiltersDll, "image_getPixelAsSingle" );
			_image_getPixelAsString = (LPFNDLL_IMAGE_GETPIXELASSTRING)GetProcAddress( hFiltersDll, "image_getPixelAsString" );
			_image_drawLine = (LPFNDLL_IMAGE_DRAWLINE)GetProcAddress( hFiltersDll, "image_drawLine" );
			_image_drawLineSegment = (LPFNDLL_IMAGE_DRAWLINESEGMENT)GetProcAddress( hFiltersDll, "image_drawLineSegment" );
			_image_drawRect = (LPFNDLL_IMAGE_DRAWRECT)GetProcAddress( hFiltersDll, "image_drawRect" );
			_image_drawRectFilled = (LPFNDLL_IMAGE_DRAWRECTFILLED)GetProcAddress( hFiltersDll, "image_drawRectFilled" );
			_image_drawDisk = (LPFNDLL_IMAGE_DRAWDISK)GetProcAddress( hFiltersDll, "image_drawDisk" );
			_image_HSLtoRGB = (LPFNDLL_IMAGE_HSLtoRGB)GetProcAddress( hFiltersDll, "image_HSLtoRGB" );
			_image_isSameSize = (LPFNDLL_IMAGE_ISSAMESIZE)GetProcAddress( hFiltersDll, "image_isSameSize" );
			_image_getValidROI = (LPFNDLL_IMAGE_GETVALIDROI)GetProcAddress( hFiltersDll, "image_getValidROI" );
			//
			for( i=0; i<properties_count; i++ ){
				_filters_setProperty( properties[0][i], properties[1][i] );
			}
			_filters_initialize();
	   }
	}
}

void filters_unInitialize(void)
{
	_filters_unInitialize();
	FreeLibrary( hFiltersDll );
}

char* filters_getVersion(void)
{
	if( _filters_getVersion ){
		return _filters_getVersion();
	}
	return NULL;
}

char* filters_getLastError(void)
{
	if( _filters_getLastError ){
		return _filters_getLastError();
	}
	return NULL;
}

__int32 filters_getFiltersCount( void )
{
	if( _filters_getFiltersCount ){
		return _filters_getFiltersCount();
	}
	return -1;
}

char* filters_getFiltersNameAtIndex( __int32 aIndexFilter )
{
	if( _filters_getFiltersNameAtIndex ){
		return _filters_getFiltersNameAtIndex( aIndexFilter );
	}
	return NULL;
}

__int32 filters_createFilter( char* aFilterName )
{
	if( _filters_createFilter ){
		return _filters_createFilter( aFilterName );
	}
	return -1;
}

void filters_deleteFilter( __int32 aFilterInstance )
{
	if( _filters_deleteFilter ){
		_filters_deleteFilter( aFilterInstance );
	}
}

__int32 filters_getParametersCount( __int32 aFilterInstance )
{
	if( _filters_getParametersCount ){
		return _filters_getParametersCount( aFilterInstance );
	}
	return -1;
}

void filters_getParameterName( __int32 aFilterInstance, __int32 aIndexParameter, char* aParameterName )
{
	if( _filters_getParameterName ){
		_filters_getParameterName( aFilterInstance, aIndexParameter, aParameterName );
	}
}

void filters_getParameterHelp( __int32 aFilterInstance, __int32 aIndexParameter, char* aParameterHelp )
{
	if( _filters_getParameterHelp ){
		_filters_getParameterHelp( aFilterInstance, aIndexParameter, aParameterHelp );
	}
}

__int32 filters_getOutputsCount( __int32 aFilterInstance )
{
	if( _filters_getOutputsCount ){
		return _filters_getOutputsCount( aFilterInstance );
	}
	return -1;
}

void filters_getOutputName( __int32 aFilterInstance, __int32 aIndexOutput, char* aOutputName )
{
	if( _filters_getOutputName ){
		_filters_getOutputName( aFilterInstance, aIndexOutput, aOutputName );
	}
}

void filters_run( __int32 aFilterInstance )
{
	if( _filters_run ){
		_filters_run( aFilterInstance );
	}
}

void filters_runCommand( __int32 aFilterInstance, char* aCommand )
{
	if( _filters_runCommand ){
		_filters_runCommand( aFilterInstance, aCommand );
	}
}

void filters_setParameterString( __int32 aFilterInstance, char* aParameterName, char* aString )
{
	if( _filters_setParameterString ){
		_filters_setParameterString( aFilterInstance, aParameterName, aString );
	}
}

void filters_setParameterImage( __int32 aFilterInstance, char* aParameterName, PFBitmap32 aImage )
{
	if( _filters_setParameterImage ){
		_filters_setParameterImage( aFilterInstance, aParameterName, aImage );
	}
}

void filters_setParameterImages( __int32 aFilterInstance, char* aParameterName, PArrayOfPFBitmap32 aImages )
{
	if( _filters_setParameterImages ){
		_filters_setParameterImages( aFilterInstance, aParameterName, aImages );
	}
}

void filters_setParameterInteger( __int32 aFilterInstance, char* aParameterName, __int64 aInteger )
{
	if( _filters_setParameterInteger ){
		_filters_setParameterInteger( aFilterInstance, aParameterName, aInteger );
	}
}

void filters_setParameterFloat( __int32 aFilterInstance, char* aParameterName, float aFloat )
{
	if( _filters_setParameterFloat ){
		_filters_setParameterFloat( aFilterInstance, aParameterName, aFloat );
	}
}

void filters_setParameterBoolean( __int32 aFilterInstance, char* aParameterName, __int8 aBoolean )
{
	if( _filters_setParameterBoolean ){
		_filters_setParameterBoolean( aFilterInstance, aParameterName, aBoolean );
	}
}

void filters_setParameterPointer( __int32 aFilterInstance, char* aParameterName, void* aPointer )
{
	if( _filters_setParameterPointer ){
		_filters_setParameterPointer( aFilterInstance, aParameterName, aPointer );
	}
}

__int32 filters_getOutputInteger( __int32 aFilterInstance, char* aOutputName )
{
	if( _filters_getOutputInteger ){
		return _filters_getOutputInteger( aFilterInstance, aOutputName );
	}
	return -1;
}

float filters_getOutputFloat( __int32 aFilterInstance, char* aOutputName )
{
	if( _filters_getOutputFloat ){
		return _filters_getOutputFloat( aFilterInstance, aOutputName );
	}
	return -1.0;
}

PFBitmap32 filters_getOutputImage( __int32 aFilterInstance, char* aOutputName )
{
	if( _filters_getOutputImage ){
		return _filters_getOutputImage( aFilterInstance, aOutputName );
	}
	return NULL;
}

PArrayOfPFBitmap32 filters_getOutputImages( __int32 aFilterInstance, char* aOutputName )
{
	if( _filters_getOutputImages ){
		return _filters_getOutputImages( aFilterInstance, aOutputName );
	}
	return NULL;
}

PArrayOfPointer filters_getOutputArrayPointers( __int32 aFilterInstance, char* aOutputName )
{
	if( _filters_getOutputArrayPointers ){
		return _filters_getOutputArrayPointers( aFilterInstance, aOutputName );
	}
	return NULL;
}

__int32 filters_getOutputArrayPointersLength( __int32 aFilterInstance, char* aOutputName )
{
	if( _filters_getOutputArrayPointersLength ){
		return _filters_getOutputArrayPointersLength( aFilterInstance, aOutputName );
	}
	return -1;
}

void filters_setRegionOfInterest( __int32 aFilterInstance, PIRect roi )
{
	if( _filters_setRegionOfInterest ){
		_filters_setRegionOfInterest( aFilterInstance, roi );
	}
}

void filters_unsetRegionOfInterest( __int32 aFilterInstance )
{
	if( _filters_unsetRegionOfInterest ){
		_filters_unsetRegionOfInterest( aFilterInstance );
	}
}

PFBitmap32 image_createImageLike( PFBitmap32 aImage )
{
	if( _image_createImageLike ){
		return _image_createImageLike( aImage );
	}
	return NULL;
}

PFBitmap32 image_createImageTest( __int32 aWidth, __int32 aHeight )
{
	if( _image_createImageTest ){
		return _image_createImageTest( aWidth, aHeight );
	}
	return NULL;
}

PFBitmap32 image_createImage( __int32 aWidth, __int32 aHeight )
{
	if( _image_createImage ){
		return _image_createImage( aWidth, aHeight );
	}
	return NULL;
}

PFBitmap32 image_createImageFromImage( PFBitmap32 aImage )
{
	if( _image_createImageFromImage ){
		return _image_createImageFromImage( aImage );
	}
	return NULL;
}

PFBitmap32 image_eraseOrCreateImageLike( PFBitmap32 lastImage, PFBitmap32 aImage )
{
	if( _image_eraseOrCreateImageLike ){
		return _image_eraseOrCreateImageLike( lastImage, aImage );
	}
	return NULL;
}

void image_eraseImage( PFBitmap32 aImage )
{
	if( _image_eraseImage ){
		_image_eraseImage( aImage );
	}
}

void image_copyImageToImage( PFBitmap32 aImageFrom, PFBitmap32 aImageTo )
{
	if( _image_copyImageToImage ){
		_image_copyImageToImage( aImageFrom, aImageTo );
	}
}

void image_freeImage( PFBitmap32 aImage )
{
	if( _image_freeImage ){
		_image_freeImage( aImage );
	}
}

void image_freeImages( PArrayOfPFBitmap32 aImages )
{
	if( _image_freeImages ){
		_image_freeImages( aImages );
	}
}

__int32 image_getImagesCount( PArrayOfPFBitmap32 aImages )
{
	if( _image_getImagesCount ){
		return _image_getImagesCount( aImages );
	}
	return -1;
}

PFBitmap32 image_getImagesImageAtIndex( PArrayOfPFBitmap32 aImages , __int32 aIndex )
{
	if( _image_getImagesImageAtIndex ){
		return _image_getImagesImageAtIndex( aImages, aIndex );
	}
	return NULL;
}

unsigned __int8 image_intensity( TFColor32 aColor32 )
{
	return _image_intensity( aColor32 );
}

unsigned __int8 image_luminosity( TFColor32 aColor32 )
{
	return _image_luminosity( aColor32 );
}

unsigned __int8 image_redComponent( TFColor32 aColor32 )
{
	return _image_redComponent( aColor32 );
}

unsigned __int8 image_greenComponent( TFColor32 aColor32 )
{
	return _image_greenComponent( aColor32 );
}

unsigned __int8 image_blueComponent( TFColor32 aColor32 )
{
	return _image_blueComponent( aColor32 );
}

TFColor32 image_color32( __int8 R, __int8 G, __int8 B )
{
	return _image_color32( R, G, B );
}

TFColor32 image_gray32( __int8 intensity )
{
	return _image_gray32( intensity );
}

void image_setPixel( PFBitmap32 aImage, __int32 x, __int32 y, TFColor32 aColor32 )
{
	_image_setPixel( aImage, x, y, aColor32 );
}

void image_setPixelAsSingle( PFBitmap32 aImage, __int32 x, __int32 y, float data )
{
	_image_setPixelAsSingle( aImage, x, y, data );
}

TFColor32 image_getPixel( PFBitmap32 aImage, __int32 x, __int32 y )
{
	return _image_getPixel( aImage, x, y );
}

float image_getPixelAsSingle( PFBitmap32 aImage, __int32 x, __int32 y )
{
	return _image_getPixelAsSingle( aImage, x, y );
}

void image_getPixelAsString( PFBitmap32 aImage, __int32 x, __int32 y, char* aStr )
{
	_image_getPixelAsString( aImage, x, y, aStr );
}

void image_drawLine( PFBitmap32 aImage, __int32 FromX, __int32 FromY, __int32 ToX, __int32 ToY, TFColor32 aColor32, float aThick )
{
	_image_drawLine( aImage, FromX, FromY, ToX, ToY, aColor32, aThick );
}

void image_drawLineSegment( PFBitmap32 aImage, PFSegment aSegment, TFColor32 aColor32, float aThick )
{
	_image_drawLineSegment( aImage, aSegment, aColor32, aThick );
}

void image_drawRect( PFBitmap32 aImage, PFRect aRect, TFColor32 aColor32, float aThick )
{
	_image_drawRect( aImage, aRect, aColor32, aThick );
}

void image_drawRectFilled( PFBitmap32 aImage, PFRect aRect, TFColor32 aColor32 )
{
	_image_drawRectFilled( aImage, aRect, aColor32 );
}

void image_drawDisk( PFBitmap32 aImage, float aCenterX, float aCenterY, float aradius, TFColor32 aColor32 )
{
	_image_drawDisk( aImage, aCenterX, aCenterY, aradius, aColor32 );
}

TFColor32 image_HSLtoRGB( float H , float S, float L )
{
	return _image_HSLtoRGB( H, S, L );
}

__int8 image_isSameSize( PFBitmap32 aImage1, PFBitmap32 aImage2 )
{
	return _image_isSameSize( aImage1, aImage2 );
}

void image_getValidROI( PFBitmap32 aImage, PFRect aROI, PFRect aValidROI )
{
	_image_getValidROI( aImage, aROI, aValidROI );
}


PFBitmap32 helper_loadImage( char* strImageToLoad )
{
	PFBitmap32 imageLoaded = NULL;
	PArrayOfPFBitmap32 images;
	__int32 imagesCount;
	PFBitmap32 result = NULL;
	__int32 filterImageLoader;
	filterImageLoader = filters_createFilter( "filterImageLoader" );
	filters_setParameterString( filterImageLoader, "filesName", strImageToLoad );
	filters_run( filterImageLoader );
	images = filters_getOutputImages( filterImageLoader, "outImages" );	
	imagesCount = image_getImagesCount( images );
	if( imagesCount>0 ){
		imageLoaded =  image_getImagesImageAtIndex( images, 0 );	
		if( imageLoaded != NULL ) {
			result = image_createImageFromImage( imageLoaded );
		}
	}
	filters_deleteFilter( filterImageLoader );
	return result;
}

void helper_saveImage( PFBitmap32 aImage, char* strImageToSave )
{
	//printf( "save image to [%s]\n", strImageToSave );
	__int32 filterImageSaver = filters_createFilter( "filterImageSaver" );
	filters_setParameterString( filterImageSaver, "fileName", strImageToSave );
	filters_setParameterImage( filterImageSaver, "inImage", aImage );
	filters_run( filterImageSaver );	
	filters_deleteFilter( filterImageSaver );
}


// END OF FILE