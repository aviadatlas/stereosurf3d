#include <windows.h>
#include <stdlib.h>
#include <stdio.h>
#include <math.h>

#include "main.h"


TFiltersPlugin::TFiltersPlugin()
{
	_inImageParameter = NULL;
	_outImageParameter = NULL;
	_scale = 100;
	_unsetRegionOfInterest();
}

TFiltersPlugin::~TFiltersPlugin()
{
}

// It's a scholar example, and not a good and fast method !
void TFiltersPlugin::_run(void)
{
	int x, xMin, xMax, y, yMin, yMax;
	TFColor32* pDest;
	TFRect roi;
	int r;

	// if we have input images
	if( (_inImageParameter!=NULL) && (_outImageParameter!=NULL) ){
		// both input images must be of the same size
		//if( (_inImageParameter->Width==_outImageParameter->Width) && (_inImageParameter->Height==_outImageParameter->Height) ){
		if( image_isSameSize( _inImageParameter, _outImageParameter ) == TRUE ){
			// we start by copy the [inImage] to [outImage]
			image_copyImageToImage( _inImageParameter, _outImageParameter );
			// we get the ROI (if ROI=(-1,-1)(-1,-1), then getValidROI return the full image)
			image_getValidROI( _inImageParameter, &_roi, &roi );
			// for each pixel in the ROI
			xMin = ceil( roi.Left );
			xMax = ceil( roi.Right );
			yMin = ceil( roi.Top );
			yMax = ceil( roi.Bottom );
			pDest = _outImageParameter->Bits;
			for( y=yMin; y<=yMax ; y++ ){
				for( x=xMin; x<=xMax; x++ ){
					// sometime we add salt and pepper noise
					int RANGE_MIN = 0;
					int RANGE_MAX = 2*_scale;
					int r = (((double) rand() / (double) RAND_MAX) * RANGE_MAX + RANGE_MIN);
					if (r==0){
						*pDest = clBlack32;
					}else if( r==1 ){
						*pDest = clWhite32;
					}
					pDest++;
				}
			}
		}
	}
}

void TFiltersPlugin::_run( const char* aCommand )
{
	_run();
}

__int64 TFiltersPlugin::_getParametersCount()
{
  return 3;
}

void TFiltersPlugin::_getParameterName( const __int32 aIndex, char* aName )
{
	switch( aIndex ){
	case 0 : strcpy( aName, "inImage" ); break;
	case 1 : strcpy( aName, "outImage" ); break;
	case 2 : strcpy( aName, "scale" ); break;
	}
}

void TFiltersPlugin::_getParameterHelp( const __int32 aIndex, char* aHelp )
{
	switch( aIndex ){
	case 0 : strcpy( aHelp, "the input image" ); break;
	case 1 : strcpy( aHelp, "the output image (must be of the same size than [inImage])" ); break;
	case 2 : strcpy( aHelp, "if scale=100, then for each pixel, there is 1/scale chance to have noise" ); break;
	}
}

void TFiltersPlugin::_setParameterInteger( const char* aName,  const __int64 aValue )
{
	if( strcmp("scale", aName)==0 )_scale = aValue;
}

void TFiltersPlugin::_setParameterFloat( const char* aName, const float aValue )
{
}

void TFiltersPlugin::_setParameterBoolean( const char* aName, const bool aValue )
{
}

void TFiltersPlugin::_setParameterString( const char* aName, const char* aValue )
{
}

void TFiltersPlugin::_setParameterImage( const char* aName, const PFBitmap32 aImage )
{
	if( strcmp("inImage", aName)==0 )_inImageParameter = aImage;
	else if( strcmp("outImage", aName)==0 )_outImageParameter = aImage;
}

void TFiltersPlugin::_setParameterImagesCount( const char* aName, const __int32 aCount )
{
}

void TFiltersPlugin::_setParameterImagesImageAtIndex( const char* aName, const PFBitmap32 aImage, const __int32 aIndex )
{
}

void TFiltersPlugin::_setParameterPointer( const char* aName,  const Pointer aPointer )
{
}

__int32 TFiltersPlugin::_getOutputsCount()
{
  return 0;
}

void TFiltersPlugin::_getOutputName( const __int32 aIndex, char* aName )
{
}

PFBitmap32 TFiltersPlugin::_getOutputImage( const char* aName )
{
	return NULL;
}

__int32 TFiltersPlugin::_getOutputImagesCount( const char* aName )
{
	return 0;
}

PFBitmap32 TFiltersPlugin::_getOutputImagesImageAtIndex( const char* aName, const __int32 aIndex )
{
	return NULL;
}

__int32 TFiltersPlugin::_getOutputInteger( const char* aName )
{
	return -1;
}

float TFiltersPlugin::_getOutputFloat( const char* aName )
{
	return -1.0;
}

__int32 TFiltersPlugin::_getOutputArrayPointersCount( const char* aName )
{
	return 0;
}

Pointer TFiltersPlugin::_getOutputArrayPointersPointerAtIndex( const char* aName, const __int32 aIndex )
{
	return NULL;
}

void TFiltersPlugin::_setRegionOfInterest(  const PFRect roi )
{
	_roi = *roi;
}

void TFiltersPlugin::_unsetRegionOfInterest()
{
	_roi.Left = _roi.Top = _roi.Right = _roi.Bottom = -1;
}