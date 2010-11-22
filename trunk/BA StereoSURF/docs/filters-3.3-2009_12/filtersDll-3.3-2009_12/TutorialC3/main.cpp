// main.cpp of TutorialC3
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
#include <windows.h>
#include <winbase.h>


void testVideo(void)
{
    printf( "\ntest [filtersPlugin_Video]\n" );
    // we create the [filtersPlugin_Video]
    __int32 filtersPlugin_Video = filters_createFilter( "filtersPlugin_Video" );
    // for debug only, we would like to see it's window, to see the video
    filters_setParameterBoolean( filtersPlugin_Video, "showViewer", filters_true );
    filters_setParameterInteger( filtersPlugin_Video, "win_x", 0 );
    filters_setParameterInteger( filtersPlugin_Video, "win_y", 0 );
    // we set the file name of the video to load
    filters_setParameterString( filtersPlugin_Video, "fileName", "C:/DEV2/Images/pieces200608/320/OK_320_1.avi" );
    // we ask to load it
    filters_runCommand( filtersPlugin_Video, "LOAD" );
    // and to play it
    printf( "\nplay the video" );
    filters_runCommand( filtersPlugin_Video, "PLAY" );
    
    // now we will capture images of the playing video, and apply a [filterContrastExplorer] on each captured image
    __int32 filterContrastExplorer = filters_createFilter( "filterContrastExplorer" );
    PFBitmap32 fImageContrast = NULL;
    
    // we will use [filtersPlugin_Viewer] to show the image contrast of the image capture
    __int32 filtersPlugin_Viewer = filters_createFilter( "filtersPlugin_Viewer" );
    filters_setParameterBoolean( filtersPlugin_Viewer, "showViewer", filters_true );
    filters_setParameterInteger( filtersPlugin_Viewer, "win_x", 0 );
    filters_setParameterInteger( filtersPlugin_Viewer, "win_y", 200 );
    
    // we capture n images
    int n = 20;
    for( int i=0; i<n; i++ ){
        // we sleep 1s between each capture
        Sleep(500);
        // we capture a image from the video
        printf( "\ncapture image %d", i+1 );
        filters_run( filtersPlugin_Video );
        PFBitmap32 fImageCapture = filters_getOutputImage( filtersPlugin_Video, "outImage" );
        // we can save it
        helper_saveImage( fImageCapture, "C:/DEV/FiltersTutorial/Bin/tutorialC3_capture.bmp" );
        // we apply the contrast filter on it
        printf( "\napply contrast on captured image" );
        if( fImageContrast!=NULL ){
            image_freeImage( fImageContrast );
        }
        fImageContrast = image_createImageLike( fImageCapture );
        filters_setParameterImage( filterContrastExplorer, "inImage", fImageCapture );
        filters_setParameterImage( filterContrastExplorer, "outImage", fImageContrast );
        filters_setParameterInteger( filterContrastExplorer, "precision", 1 );
        filters_setParameterString( filterContrastExplorer, "mode", "NORMAL" );
        filters_run( filterContrastExplorer );
        // we show it in the viewer
        filters_setParameterImage( filtersPlugin_Viewer, "inImage", fImageContrast );
        filters_run( filtersPlugin_Viewer );
        // we can save it too
        helper_saveImage( fImageContrast, "C:/DEV/FiltersTutorial/Bin/tutorialC3_contrast.bmp" );
    }    
        
    printf( "\nstop the video" );
    filters_runCommand( filtersPlugin_Video, "STOP" );
    
    
	printf("\n(press [ENTER] to close)\n");
	fgetc(stdin);
    
    filters_deleteFilter( filterContrastExplorer );
    filters_deleteFilter( filtersPlugin_Viewer );
    filters_deleteFilter( filtersPlugin_Video );
    image_freeImage( fImageContrast );
}

void testCoOccurrenceMatrix()
{
    printf( "\ntest [filterCoOccurrenceMatrix]\n" );
	PFBitmap32 fImageTest = image_createImageTest( 512, 512 );
	//PFBitmap32 outImageMonitoring = image_createImageLike( fImageTest );
	
	__int32 filterCoOccurrenceMatrix = filters_createFilter( "filterCoOccurrenceMatrix" );
	
	filters_setParameterImage( filterCoOccurrenceMatrix, "inImage", fImageTest );
    //filters_setParameterImage( filterCoOccurrenceMatrix, "outImageMonitoring", outImageMonitoring );
    filters_setParameterInteger( filterCoOccurrenceMatrix, "horizontalDistance", 1 );
    filters_setParameterInteger( filterCoOccurrenceMatrix, "verticalDistance", 0 );
    filters_setParameterInteger( filterCoOccurrenceMatrix, "xAnalyse", 0 ); // Intensity
    filters_setParameterInteger( filterCoOccurrenceMatrix, "yAnalyse", 0 ); // Intensity
    filters_setParameterFloat( filterCoOccurrenceMatrix, "scale",  1 );

	filters_run( filterCoOccurrenceMatrix );
	
	// get outImage
	PFBitmap32 outImage = filters_getOutputImage( filterCoOccurrenceMatrix, "outImage" );
	helper_saveImage( outImage, "C:/DEV/FiltersTutorial/Bin/tutorialC3_CoOccurrenceMatrix_outImage.bmp" );
	// and features
	float tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_angularSecondMoment" );
    printf( "features_angularSecondMoment = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_energy" );
    printf( "features_angularSecondMoment = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_entropy" );
    printf( "features_entropy = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_contrast" );
    printf( "features_contrast = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_dissimilarity" );
    printf( "features_dissimilarity = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_inverseDifferenceMoment" );
    printf( "features_inverseDifferenceMoment = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_mean_i" );
    printf( "features_mean_i = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_mean_j" );
    printf( "features_mean_j = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_variance_i" );
    printf( "features_variance_i = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_variance_j" );
    printf( "features_variance_j = %f \n", tmpSingle );
    tmpSingle = filters_getOutputFloat( filterCoOccurrenceMatrix, "features_correlation" );
    printf( "features_correlation = %f \n", tmpSingle );
        
	filters_deleteFilter( filterCoOccurrenceMatrix );
	
	image_freeImage( fImageTest );
}

void testWebCam(void)
{
    printf( "\ntest [filtersPlugin_WebCam]\n" );
    // we create the [filtersPlugin_WebCam]
    __int32 filtersPlugin_WebCam = filters_createFilter( "filtersPlugin_WebCam" );
	// get devices list
	filters_setParameterBoolean( filtersPlugin_WebCam, "showViewer", filters_true );
  	filters_runCommand( filtersPlugin_WebCam, "READ_DEVICES" );
	PArrayOfPointer paPointerDevice = filters_getOutputArrayPointers( filtersPlugin_WebCam, "devices" );
	ArrayOfPointer aPointerDevice = *paPointerDevice;
	__int32 length_pointersDevice = filters_getOutputArrayPointersLength( filtersPlugin_WebCam, "devices" );
    printf( "found [%d] device(s) \n", length_pointersDevice );
	for(int d=0; d<length_pointersDevice; d++){
		char* device = (char*)(aPointerDevice[d]);
		printf( "--\n" );
	    printf( "device[%d]=[%s] \n", d, device );
	    // get format
	    filters_setParameterInteger( filtersPlugin_WebCam, "deviceIndex", d );
    	filters_runCommand( filtersPlugin_WebCam, "READ_DEVICE_FORMAT" );
	    PArrayOfPointer paPointerDeviceFormats = filters_getOutputArrayPointers( filtersPlugin_WebCam, "deviceFormats" );
		ArrayOfPointer aPointerDeviceFormats = *paPointerDeviceFormats;
		__int32 length_pointersDeviceFormats = filters_getOutputArrayPointersLength( filtersPlugin_WebCam, "deviceFormats" );
	    printf( "  with [%d] formats :\n", length_pointersDeviceFormats );
		for(int f=0; f<length_pointersDeviceFormats; f++){
			char* deviceFormat = (char*)(aPointerDeviceFormats[f]);
		    printf( "    deviceFormat[%d]=[%s] \n", f, deviceFormat );
		}
	}

	// we take a picture with cam device 0
	if(length_pointersDevice>0){
		// open the device		
		printf("\n(press [ENTER] to open cam device 0 with format 0)\n");
		fgetc(stdin);
		//   step 1 : set deviceIndex by reading format
	    filters_setParameterInteger( filtersPlugin_WebCam, "deviceIndex", 0 );
    	filters_runCommand( filtersPlugin_WebCam, "READ_DEVICE_FORMAT" );
    	//   step 2 : choose a format by opening the device
	    filters_setParameterBoolean( filtersPlugin_WebCam, "showViewer", filters_true );
	    filters_setParameterInteger( filtersPlugin_WebCam, "formatIndex", 0 );
	    filters_runCommand( filtersPlugin_WebCam, "DEVICE_OPEN" );
	    
		// take a picture
		printf("\n(press [ENTER] to take a picture)\n");
		fgetc(stdin);
		filters_run( filtersPlugin_WebCam );
  		PFBitmap32 fImageCapture = filters_getOutputImage( filtersPlugin_WebCam, "outImage" );
        // we can save it
        helper_saveImage( fImageCapture, "C:/DEV/FiltersTutorial/Bin/tutorialC3_captureWebCam.bmp" );
		
		// close the device
		printf("\n(press [ENTER] to close cam device 0)\n");
		fgetc(stdin);
	    filters_setParameterBoolean( filtersPlugin_WebCam, "showViewer", filters_false );
    	filters_runCommand( filtersPlugin_WebCam, "DEVICE_CLOSE" );	    

	}

	filters_deleteFilter( filtersPlugin_WebCam );
}


int main()
 {
	printf("TutorialC3 : Test Filters Plugins\n");

	// initialize Filters
	//   -> to test 'setProperty' function,
	//    we don't have setted the working directory in the Eclipse project,
	//    so Filters will start with a working directory = the source directory
	//    then Filters can't find Filters Plugins, unitl we provide it the right directory
	filters_setProperty( "PLUGINS_DIRECTORY", "C:/DEV/FiltersTutorial/Bin" );
	filters_initialize();
	printf("\nFilters version = [%s] \n\n", filters_getVersion() );

/*	
  	PFBitmap32 fImageTest = image_createImageTest( 512, 512 );
	__int32 filtersPlugin_Viewer = filters_createFilter( "filtersPlugin_Viewer" );
	filters_setParameterImage( filtersPlugin_Viewer, "inImage", fImageTest );
	filters_setParameterBoolean( filtersPlugin_Viewer, "showViewer", filters_true );
	filters_setParameterInteger( filtersPlugin_Viewer, "win_x", 0 );
	filters_setParameterInteger( filtersPlugin_Viewer, "win_y", 200 );
	filters_run( filtersPlugin_Viewer );
	filters_deleteFilter( filtersPlugin_Viewer );
*/

	//testVideo();	
	
	//testCoOccurrenceMatrix();
	
	testWebCam();

	printf("\n(press [ENTER] to close)\n");
	fgetc(stdin);
	
	
	// dispose Filters
	filters_unInitialize();

	printf("\n------------------------------\n");
	printf("contact : filters@edurand.com\n");

	return 0;
} 
