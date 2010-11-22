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


extern "C" APIENTRY
BOOL DllMain( HANDLE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved  )
{
	filters_Initialize();
	return TRUE;
}

char G_Version[255];

#define DECLAREDLLFCT extern "C" far pascal
 
 
DECLAREDLLFCT
char* 
getVersion()
{
	strcpy( G_Version, FiltersPlugin_Version );
	return G_Version;
}

DECLAREDLLFCT
__int32
createFilter()
{
	return (__int32)new TFiltersPlugin();
}

DECLAREDLLFCT
void 
deleteFilter(const __int32 aHandle)
{
	delete ((TFiltersPlugin*)aHandle);
}

DECLAREDLLFCT
void
run(const __int32 aHandle)
{
	((TFiltersPlugin*)aHandle)->_run();
}

DECLAREDLLFCT
void 
runCommand(const __int32 aHandle, const char* aCommand)
{
	((TFiltersPlugin*)aHandle)->_run( aCommand );
}

DECLAREDLLFCT
__int64  
getParametersCount(const __int32 aHandle)
{
	return ((TFiltersPlugin*)aHandle)->_getParametersCount();
}

DECLAREDLLFCT
void 
getParameterName(const __int32 aHandle, const __int32 aIndex, char* aName)
{
	((TFiltersPlugin*)aHandle)->_getParameterName( aIndex, aName );
}

DECLAREDLLFCT
void 
getParameterHelp(const __int32 aHandle, const __int32 aIndex, char* aHelp)
{
	((TFiltersPlugin*)aHandle)->_getParameterHelp( aIndex, aHelp );
}

DECLAREDLLFCT
void 
setParameterInteger(const __int32 aHandle, const char* aName, const __int64 aValue)
{
	((TFiltersPlugin*)aHandle)->_setParameterInteger( aName, aValue );
}

DECLAREDLLFCT
void 
setParameterFloat(const __int32 aHandle, const char* aName, const float aValue)
{
	((TFiltersPlugin*)aHandle)->_setParameterFloat( aName, aValue );
}

DECLAREDLLFCT
void 
setParameterBoolean(const __int32 aHandle, const char* aName, const bool aValue )
{
	((TFiltersPlugin*)aHandle)->_setParameterBoolean( aName, aValue );
}

DECLAREDLLFCT
void 
setParameterString(const __int32 aHandle, const char* aName, const char* aValue )
{
	((TFiltersPlugin*)aHandle)->_setParameterString( aName, aValue );
}

DECLAREDLLFCT
void 
setParameterImage(const __int32 aHandle, const char* aName, const PFBitmap32 aImage )
{
	((TFiltersPlugin*)aHandle)->_setParameterImage( aName, aImage );
}

DECLAREDLLFCT
void 
setParameterImagesCount(const __int32 aHandle, const char* aName, const __int32 aCount )
{
	((TFiltersPlugin*)aHandle)->_setParameterImagesCount( aName, aCount );
}

DECLAREDLLFCT
void 
setParameterImagesImageAtIndex(const __int32 aHandle, const char* aName, const PFBitmap32 aImage, const __int32 aIndex )
{
	((TFiltersPlugin*)aHandle)->_setParameterImagesImageAtIndex( aName, aImage, aIndex );
}

DECLAREDLLFCT
void 
setParameterPointer(const __int32 aHandle, const char* aName, const Pointer aPointer)
{
}

DECLAREDLLFCT
__int32 
getOutputsCount(const __int32 aHandle)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputsCount();
}

DECLAREDLLFCT
void 
getOutputName(const __int32 aHandle, const __int32 aIndex, char* aName)
{
	((TFiltersPlugin*)aHandle)->_getOutputName( aIndex, aName );
}

DECLAREDLLFCT
PFBitmap32 
getOutputImage(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImage( aName );
}

DECLAREDLLFCT
__int32 
getOutputImagesCount(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImagesCount( aName );
}

DECLAREDLLFCT
PFBitmap32 
getOutputImagesImageAtIndex(const __int32 aHandle, const char* aName, const __int32 aIndex)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputImagesImageAtIndex( aName, aIndex );
}

DECLAREDLLFCT
__int32 
getOutputInteger(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputInteger(aName);
}

DECLAREDLLFCT
float 
getOutputFloat(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputFloat(aName);
}

DECLAREDLLFCT
__int32 
getOutputArrayPointersCount(const __int32 aHandle, const char* aName)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputArrayPointersCount( aName );
}

DECLAREDLLFCT
Pointer 
getOutputArrayPointersPointerAtIndex(const __int32 aHandle, const char* aName, const __int32 aIndex)
{
	return ((TFiltersPlugin*)aHandle)->_getOutputArrayPointersPointerAtIndex( aName, aIndex );
}

DECLAREDLLFCT
void 
setRegionOfInterest(const __int32 aHandle, const PFRect roi)
{
	((TFiltersPlugin*)aHandle)->_setRegionOfInterest( roi );
}

DECLAREDLLFCT
void 
unsetRegionOfInterest(const __int32 aHandle)
{
	((TFiltersPlugin*)aHandle)->_unsetRegionOfInterest();
}

