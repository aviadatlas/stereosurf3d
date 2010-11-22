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


#include <windows.h>
#include <stdlib.h>

#include "main.h"

extern "C" {
#include "wrapper_filtersDll.h"
}

//#include <hash_map>
//using namespace std;
//list<TFiltersPlugin*> G_PFiltersPluginList;


BOOL APIENTRY DllMain( HANDLE hModule, 
					  DWORD  ul_reason_for_call, 
					  LPVOID lpReserved
					  )
{
	filters_Initialize();

	return TRUE;
}

char G_Version[255];

char* far pascal 
getVersion()
{
	strcpy_s( G_Version, FiltersPlugin_Version );
	return G_Version;
}

__int32 far pascal
createFilter()
{
	return (__int32)new TFiltersPlugin();
}

void far pascal
deleteFilter(const __int32 aHandle)
{
	delete ((TFiltersPlugin*)aHandle);
}

void far pascal
run(const __int32 aHandle)
{
	((TFiltersPlugin*)aHandle)->_run();
}

void far pascal
runCommand(const __int32 aHandle, const char* aCommand)
{
	((TFiltersPlugin*)aHandle)->_run( aCommand );
}

__int64 far pascal getParametersCount(const __int32 aHandle)
{
	return ((TFiltersPlugin*)aHandle)->_getParametersCount();
}

void far pascal
getParameterName(const __int32 aHandle, const __int32 aIndex, char* aName)
{
	((TFiltersPlugin*)aHandle)->_getParameterName( aIndex, aName );
}

void far pascal
getParameterHelp(const __int32 aHandle, const __int32 aIndex, char* aHelp)
{
	((TFiltersPlugin*)aHandle)->_getParameterHelp( aIndex, aHelp );
}

void far pascal
setParameterInteger(const __int32 aHandle, const char* aName, const __int64 aValue)
{
	((TFiltersPlugin*)aHandle)->_setParameterInteger( aName, aValue );
}

void far pascal
setParameterFloat(const __int32 aHandle, const char* aName, const float aValue)
{
	((TFiltersPlugin*)aHandle)->_setParameterFloat( aName, aValue );
}

void far pascal
setParameterBoolean(const __int32 aHandle, const char* aName, const bool aValue )
{
	((TFiltersPlugin*)aHandle)->_setParameterBoolean( aName, aValue );
}

void far pascal
setParameterString(const __int32 aHandle, const char* aName, const char* aValue )
{
	((TFiltersPlugin*)aHandle)->_setParameterString( aName, aValue );
}

void far pascal
setParameterImage(const __int32 aHandle, const char* aName, const PFBitmap32 aImage )
{
	((TFiltersPlugin*)aHandle)->_setParameterImage( aName, aImage );
}

void far pascal
setParameterImagesCount(const __int32 aHandle, const char* aName, const __int32 aCount )
{
	((TFiltersPlugin*)aHandle)->_setParameterImagesCount( aName, aCount );
}

void far pascal
setParameterImagesImageAtIndex(const __int32 aHandle, const char* aName, const PFBitmap32 aImage, const __int32 aIndex )
{
	((TFiltersPlugin*)aHandle)->_setParameterImagesImageAtIndex( aName, aImage, aIndex );
}

void far pascal
setParameterPointer(const __int32 aHandle, const char* aName, const Pointer aPointer)
{
}

__int32 far pascal
getOutputsCount(const __int32 aHandle)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputsCount();
}

void far pascal
getOutputName(const __int32 aHandle, const __int32 aIndex, char* aName)
{
	((TFiltersPlugin*)aHandle)->_getOutputName( aIndex, aName );
}

PFBitmap32 far pascal
getOutputImage(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImage( aName );
}

__int32 far pascal
getOutputImagesCount(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImagesCount( aName );
}

PFBitmap32 far pascal
getOutputImagesImageAtIndex(const __int32 aHandle, const char* aName, const __int32 aIndex)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImagesImageAtIndex( aName, aIndex );
}

__int32 far pascal
getOutputInteger(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputInteger(aName);
}

float far pascal
getOutputFloat(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputFloat(aName);
}

__int32 far pascal
getOutputArrayPointersCount(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputArrayPointersCount( aName );
}

Pointer far pascal
getOutputArrayPointersPointerAtIndex(const __int32 aHandle, const char* aName, const __int32 aIndex)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputArrayPointersPointerAtIndex( aName, aIndex );
}

void far pascal
setRegionOfInterest(const __int32 aHandle, const PFRect roi)
{
	((TFiltersPlugin*)aHandle)->_setRegionOfInterest( roi );
}

void far pascal
unsetRegionOfInterest(const __int32 aHandle)
{
	((TFiltersPlugin*)aHandle)->_unsetRegionOfInterest();
}

