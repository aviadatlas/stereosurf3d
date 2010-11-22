// main.cpp of TutorialC2
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

extern "C" {
  #include "wrapper_filtersDll.h"
}


#include <stdio.h>
 int main()
 {
	printf("TutorialC2 : Hello Filters !\n");
	printf("(from an Eclipse C++ CDT based project)\n");

	// initialize Filters
	filters_initialize();
	printf("\nFilters version = [%s] \n", filters_getVersion() );

	// list all filters, and display info for each	
	printf("\n------------------------------\n");
	printf("->list all filters, and display info for each\n\n");
	int fMax = filters_getFiltersCount();
	printf( "Filters count = [%d] \n", fMax );
	for( int f=0; f<fMax; f++ ){
		// for each filter
		// get it's name
		char* filterName = filters_getFiltersNameAtIndex( f );
		printf("  [%s] \n", filterName );
		// create it
		__int32 filter = filters_createFilter( filterName );
		// list all parameters of this filter
		int pMax = filters_getParametersCount( filter );
		printf("    Parameters count [%d] \n", pMax );
		char parameterName[1024];
		char parameterHelp[1024];
		for( int p=0; p<pMax; p++ ){
			filters_getParameterName( filter, p, parameterName );
			filters_getParameterHelp( filter, p, parameterHelp );
			printf("      [%s] : %s\n", parameterName, parameterHelp );
		}
		// list all outputs of this filter
		int oMax = filters_getOutputsCount( filter );
		printf("    Outputs count [%d] \n", oMax );
		char outputName[256];
		for( int o=0; o<oMax; o++ ){
			filters_getOutputName( filter, o, outputName );
			printf("      [%s]\n", outputName );
		}
		// we delete the filter
		filters_deleteFilter( filter );
	}
	
	// load images, then apply a Sobel on the first image, and then save
	printf("->load images, than apply a Sobel on the first image, and then save\n\n");
	__int32 filterImageLoader = filters_createFilter( "filterImageLoader" );
	// exception if file not found, so be careful of your working directory
	char* strImageToLoad = "blob2.tif;lenna_color.bmp";
	printf( "load images [%s]\n", strImageToLoad );
	filters_setParameterString( filterImageLoader, "filesName", strImageToLoad );
	filters_run( filterImageLoader );
	PArrayOfPFBitmap32 images = filters_getOutputImages( filterImageLoader, "outImages" );	
	__int32 imagesCount = image_getImagesCount( images );
	printf( "  imagesCount loaded [%d]\n", imagesCount );
	if( imagesCount>0 ){
		PFBitmap32 imageLoaded = image_getImagesImageAtIndex( images, 0 );
		__int32 filterSobel = filters_createFilter( "filterSobel" );
		PFBitmap32 imageSobel = image_createImageLike( imageLoaded );
		filters_setParameterImage( filterSobel, "inImage", imageLoaded );
		filters_setParameterImage( filterSobel, "outImage", imageSobel );
		filters_setParameterInteger( filterSobel, "blurIteration", 1 );
		filters_setParameterInteger( filterSobel, "gain", 5 );
		filters_setParameterInteger( filterSobel, "thresholdLower", 1 );
		filters_setParameterInteger( filterSobel, "thresholdUpper", 255 );
		filters_run( filterSobel );
		// save image
		{
			char strImageToSave[255];
			strcpy( strImageToSave, "testFiltersDllC_output_sobel.jpg" );
			printf( "save image to [%s]\n", strImageToSave );
			__int32 filterImageSaver = filters_createFilter( "filterImageSaver" );
			filters_setParameterString( filterImageSaver, "fileName", strImageToSave );
			filters_setParameterImage( filterImageSaver, "inImage", imageSobel );
			filters_run( filterImageSaver );	
			filters_deleteFilter( filterImageSaver );
		}
		// dispose
		image_freeImage( imageSobel );
		filters_deleteFilter( filterSobel );
	}
	filters_deleteFilter( filterImageLoader );
	
	// dispose Filters
	filters_unInitialize();

	printf("\n------------------------------\n");
	printf("contact : filters@edurand.com\n");

	printf("\n(press [ENTER] to close)\n");

	fgetc(stdin);
	return 0;
} 
