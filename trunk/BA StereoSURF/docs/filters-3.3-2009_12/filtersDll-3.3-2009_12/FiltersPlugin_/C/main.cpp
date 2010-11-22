#include <windows.h>
#include <stdlib.h>
#include <stdio.h>

#include "main.h"


TFiltersPlugin::TFiltersPlugin()
{
    // initialize Region Of Interest
	_unsetRegionOfInterest();
}

TFiltersPlugin::~TFiltersPlugin()
{
}

void TFiltersPlugin::_run(void)
{
}

void TFiltersPlugin::_run( const char* aCommand )
{
	_run();
}

__int64 TFiltersPlugin::_getParametersCount()
{
  return 0;
}

void TFiltersPlugin::_getParameterName( const __int32 aIndex, char* aName )
{
}

void TFiltersPlugin::_getParameterHelp( const __int32 aIndex, char* aHelp )
{
}

void TFiltersPlugin::_setParameterInteger( const char* aName,  const __int64 aValue )
{
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