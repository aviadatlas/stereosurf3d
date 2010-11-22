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

#ifndef DECLAREDLLFCT_C
#define DECLAREDLLFCT far pascal
#else
#define DECLAREDLLFCT extern "C" far pascal
#endif

BOOL 
DECLAREDLLFCT
DllMain( HANDLE hModule, 
					  DWORD  ul_reason_for_call, 
					  LPVOID lpReserved
					  )
{
	filters_Initialize();

	return TRUE;
}



char G_Version[255];

 
char* 
DECLAREDLLFCT
getVersion()
{
	strcpy( G_Version, FiltersPlugin_Version );
	return G_Version;
}

__int32
DECLAREDLLFCT
createFilter()
{
	return (__int32)new TFiltersPlugin();
}

void 
DECLAREDLLFCT
deleteFilter(const __int32 aHandle)
{
	delete ((TFiltersPlugin*)aHandle);
}

void
DECLAREDLLFCT
run(const __int32 aHandle)
{
	((TFiltersPlugin*)aHandle)->_run();
}

void 
DECLAREDLLFCT
runCommand(const __int32 aHandle, const char* aCommand)
{
	((TFiltersPlugin*)aHandle)->_run( aCommand );
}

__int64  
DECLAREDLLFCT
getParametersCount(const __int32 aHandle)
{
	return ((TFiltersPlugin*)aHandle)->_getParametersCount();
}

void 
DECLAREDLLFCT
getParameterName(const __int32 aHandle, const __int32 aIndex, char* aName)
{
	((TFiltersPlugin*)aHandle)->_getParameterName( aIndex, aName );
}

void 
DECLAREDLLFCT
getParameterHelp(const __int32 aHandle, const __int32 aIndex, char* aHelp)
{
	((TFiltersPlugin*)aHandle)->_getParameterHelp( aIndex, aHelp );
}

void 
DECLAREDLLFCT
setParameterInteger(const __int32 aHandle, const char* aName, const __int64 aValue)
{
	((TFiltersPlugin*)aHandle)->_setParameterInteger( aName, aValue );
}

void 
DECLAREDLLFCT
setParameterFloat(const __int32 aHandle, const char* aName, const float aValue)
{
	((TFiltersPlugin*)aHandle)->_setParameterFloat( aName, aValue );
}

void 
DECLAREDLLFCT
setParameterBoolean(const __int32 aHandle, const char* aName, const bool aValue )
{
	((TFiltersPlugin*)aHandle)->_setParameterBoolean( aName, aValue );
}

void 
DECLAREDLLFCT
setParameterString(const __int32 aHandle, const char* aName, const char* aValue )
{
	((TFiltersPlugin*)aHandle)->_setParameterString( aName, aValue );
}

void 
DECLAREDLLFCT
setParameterImage(const __int32 aHandle, const char* aName, const PFBitmap32 aImage )
{
	((TFiltersPlugin*)aHandle)->_setParameterImage( aName, aImage );
}

void 
DECLAREDLLFCT
setParameterImagesCount(const __int32 aHandle, const char* aName, const __int32 aCount )
{
	((TFiltersPlugin*)aHandle)->_setParameterImagesCount( aName, aCount );
}

void 
DECLAREDLLFCT
setParameterImagesImageAtIndex(const __int32 aHandle, const char* aName, const PFBitmap32 aImage, const __int32 aIndex )
{
	((TFiltersPlugin*)aHandle)->_setParameterImagesImageAtIndex( aName, aImage, aIndex );
}

void 
DECLAREDLLFCT
setParameterPointer(const __int32 aHandle, const char* aName, const Pointer aPointer)
{
}

__int32 
DECLAREDLLFCT
getOutputsCount(const __int32 aHandle)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputsCount();
}

void 
DECLAREDLLFCT
getOutputName(const __int32 aHandle, const __int32 aIndex, char* aName)
{
	((TFiltersPlugin*)aHandle)->_getOutputName( aIndex, aName );
}

PFBitmap32 
DECLAREDLLFCT
getOutputImage(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImage( aName );
}

__int32 
DECLAREDLLFCT
getOutputImagesCount(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImagesCount( aName );
}

PFBitmap32 
DECLAREDLLFCT
getOutputImagesImageAtIndex(const __int32 aHandle, const char* aName, const __int32 aIndex)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImagesImageAtIndex( aName, aIndex );
}

__int32 
DECLAREDLLFCT
getOutputInteger(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputInteger(aName);
}

float 
DECLAREDLLFCT
getOutputFloat(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputFloat(aName);
}

__int32 
DECLAREDLLFCT
getOutputArrayPointersCount(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputArrayPointersCount( aName );
}

Pointer 
DECLAREDLLFCT
getOutputArrayPointersPointerAtIndex(const __int32 aHandle, const char* aName, const __int32 aIndex)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputArrayPointersPointerAtIndex( aName, aIndex );
}

void 
DECLAREDLLFCT
setRegionOfInterest(const __int32 aHandle, const PFRect roi)
{
	((TFiltersPlugin*)aHandle)->_setRegionOfInterest( roi );
}

void 
DECLAREDLLFCT
unsetRegionOfInterest(const __int32 aHandle)
{
	((TFiltersPlugin*)aHandle)->_unsetRegionOfInterest();
}

