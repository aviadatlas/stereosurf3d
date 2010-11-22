// TutorialC1.cpp
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

#define _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES  1
#include "stdafx.h"


extern "C" {
  #include "wrapper_filtersDll.h"
}


// ----------------------------------------------------------------------------------------------------
void test1()
{
	printf("\n------------------------------\n");
	printf("test1 begin :\n");
	printf("->get the version of FiltersDll\n\n");
	printf("Filters version = [%s] \n", filters_getVersion() );
	printf("test1 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test2()
{
	printf("\n------------------------------\n");
	printf("test2 begin :\n");
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
		char parameterName[256];
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
	printf("test2 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test3()
{
	printf("\n------------------------------\n");
	printf("test3 begin :\n");
	printf("->load images\n\n");
	__int32 filterImageLoader = filters_createFilter( "filterImageLoader" );
  // exception if file not found, so be careful of your working directory
	char* strImageToLoad = "blob2.tif;lenna_color.bmp";
	//char* strImageToLoad = "G:\\DEVBAKCVS\\test\\filtersTest-1.7-2006_03\\images\\lenna.jpg;G:\\DEVBAKCVS\\test\\filtersTest-1.7-2006_03\\images\\blob2.tif";
	printf( "load images [%s]\n", strImageToLoad );
	filters_setParameterString( filterImageLoader, "filesName", strImageToLoad );
	filters_run( filterImageLoader );
	PArrayOfPFBitmap32 images = filters_getOutputImages( filterImageLoader, "outImages" );	
	__int32 imagesCount = image_getImagesCount( images );
	printf( "  imagesCount loaded [%d]\n", imagesCount );
	filters_deleteFilter( filterImageLoader );
	printf("test3 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test4()
{
	printf("\n------------------------------\n");
	printf("test4 begin :\n");
	printf("->save an image\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "blob2.tif" );
	__int32 filterImageSaver = filters_createFilter( "filterImageSaver" );
	char strImageToSave[255];
	strcpy( strImageToSave, "blob2.jpg" );
	printf( "save image to [%s] (change the format from tif to jpg)\n", strImageToSave );
	filters_setParameterString( filterImageSaver, "fileName", strImageToSave );
	filters_setParameterImage( filterImageSaver, "inImage", imageLoaded );
	filters_run( filterImageSaver );	
	image_freeImage( imageLoaded );
	printf("test4 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test5()
{
	printf("\n------------------------------\n");
	printf("test5 begin :\n");
	printf("->test filterSobel\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "blob2.tif" );
	if( imageLoaded != NULL ){
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
		helper_saveImage( imageSobel, "testFiltersDllC_output_sobel.jpg" );
		// dispose
		image_freeImage( imageSobel );
		filters_deleteFilter( filterSobel );
		image_freeImage( imageLoaded );
	}
	printf("test5 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test6()
{
	printf("\n------------------------------\n");
	printf("test6 begin :\n");
	printf("->test filterRotation\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "blob2.tif" );
	if( imageLoaded != NULL ){
		__int32 filterRotation = filters_createFilter( "filterRotation" );
		filters_setParameterImage( filterRotation, "inImage", imageLoaded );
		filters_setParameterFloat( filterRotation, "angle", (float)66.66 );
		filters_setParameterFloat( filterRotation, "xCenter", (float)(imageLoaded->Width / 2) );
		filters_setParameterFloat( filterRotation, "yCenter", (float)(imageLoaded->Height / 2) );
		filters_setParameterBoolean( filterRotation, "autoAdjustSize", filters_true );
		filters_setParameterInteger( filterRotation, "interpolationMode", 1 );
		filters_setParameterString( filterRotation, "missingPixelColorMode", "BLACK" );
		filters_run( filterRotation );
		PFBitmap32 imageRotation = filters_getOutputImage( filterRotation, "outImage" );
		// save image
		helper_saveImage( imageRotation, "testFiltersDllC_output_rotation.jpg" );
		// dispose
		filters_deleteFilter( filterRotation );
		image_freeImage( imageLoaded );
	}
	printf("test6 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test7()
{
	printf("\n------------------------------\n");
	printf("test7 begin :\n");
	printf("->test filterStackCreator and filterStackProcessor and filterCanny\n\n");
	PFBitmap32 imageLoaded1 = helper_loadImage( "blob2.tif" );
	PFBitmap32 imageLoaded2 = helper_loadImage( "testFiltersDllC_output_sobel.jpg" );
	if( imageLoaded1 != NULL && imageLoaded2 != NULL ){
		__int32 filterStackCreator = filters_createFilter( "filterStackCreator" );
		__int32 filterStackProcessor = filters_createFilter( "filterStackProcessor" );
		__int32 filterCanny = filters_createFilter( "filterCanny" );
		filters_setParameterImage( filterStackCreator, "inImage", imageLoaded1 );
		filters_run( filterStackCreator );
		filters_setParameterImage( filterStackCreator, "inImage", imageLoaded2 );
		filters_run( filterStackCreator );
		__int32 imagesCount = filters_getOutputInteger( filterStackCreator, "currentCount" );
		printf( "stack currentCount = [%d]\n", imagesCount );
		PArrayOfPFBitmap32 images = filters_getOutputImages( filterStackCreator, "outImages" );	
		filters_setParameterImages( filterStackProcessor, "inImages", images );
		filters_setParameterPointer( filterStackProcessor, "filter", (void*)filterCanny );
		filters_run( filterStackProcessor );
		images = filters_getOutputImages( filterStackProcessor, "outImages" );	
		imagesCount = image_getImagesCount( images );
		// save images
		char strImageToSave[255];
		for(int j=0;j<imagesCount;j++){
			sprintf( strImageToSave, "testFiltersDllC_output_stack%d.jpg", j );
			PFBitmap32 tmpImage = image_getImagesImageAtIndex( images, j );	
			helper_saveImage( tmpImage, strImageToSave );
		}
		// dispose
		image_freeImage( imageLoaded1 );
		image_freeImage( imageLoaded2 );
		filters_deleteFilter( filterCanny );
		filters_deleteFilter( filterStackProcessor );
		filters_deleteFilter( filterStackCreator );
	}
	printf("test7 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test8()
{
	printf("\n------------------------------\n");
	printf("test8 begin :\n");
	printf("->test filterBlobRepositioning2\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "testFiltersDllC_output_rotation.jpg" );
	if( imageLoaded != NULL ){
		__int32 filterBlobRepositioning2 = filters_createFilter( "filterBlobRepositioning2" );
		filters_setParameterImage( filterBlobRepositioning2, "inImage", imageLoaded );
		filters_setParameterInteger( filterBlobRepositioning2, "blob_ThresholdBackground", 100 );
		filters_setParameterInteger( filterBlobRepositioning2, "blob_AreaMin", 10 );
		filters_run( filterBlobRepositioning2 );
		float angleToRestorOrientation = filters_getOutputFloat( filterBlobRepositioning2, "angleToRestorOrientation" );
		printf( "angleToRestorOrientation = [%f] degree\n", angleToRestorOrientation );
		PFBitmap32 imageBlobRepositioning2 = filters_getOutputImage( filterBlobRepositioning2, "outImage" );
		// save image
		helper_saveImage( imageBlobRepositioning2, "testFiltersDllC_output_blobRepositioning2.jpg" );
		// dispose
		filters_deleteFilter( filterBlobRepositioning2 );
		image_freeImage( imageLoaded );
	}
	printf("test8 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test9()
{
	printf("\n------------------------------\n");
	printf("test9 begin :\n");
	printf("->test filterBlobExplorer\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "blob2.tif" );
	if( imageLoaded != NULL ){
		__int32 filterBlobExplorer = filters_createFilter( "filterBlobExplorer" );
		PFBitmap32 imageBlob = image_createImageLike( imageLoaded );
		filters_setParameterImage( filterBlobExplorer, "inImage", imageLoaded );
		filters_setParameterImage( filterBlobExplorer, "outImage", imageBlob );
		filters_setParameterInteger( filterBlobExplorer, "intensityBackground", 100 );
		filters_setParameterInteger( filterBlobExplorer, "intensityPrecision", 10 );
		filters_setParameterString( filterBlobExplorer, "enableBlobArea", "TRUE" );
		filters_setParameterInteger( filterBlobExplorer, "blobAreaMin", 40 );
		filters_setParameterBoolean( filterBlobExplorer, "criticalPoints", filters_true );
		filters_setParameterInteger( filterBlobExplorer, "contourCriticalPointsAppoximationAccuracy", 10 );
		filters_setParameterBoolean( filterBlobExplorer, "blobSurfaceInfo", filters_true );	
		filters_run( filterBlobExplorer );
		// save image
		helper_saveImage( imageBlob, "testFiltersDllC_output_blobExplorer.jpg" );
		// show some blobs data
		PArrayOfPointer paPFBlobs = filters_getOutputArrayPointers( filterBlobExplorer, "blobs" );
		ArrayOfPointer aPFBlobs = *paPFBlobs;
		//__int32 length_blobs = filters_getOutputInteger( filterBlobExplorer, "length_blobs" ); // deprecated
		__int32 length_blobs = filters_getOutputArrayPointersLength( filterBlobExplorer, "blobs" );
		printf( "filterBlobExplorer length_blobs = [%d]\n", length_blobs );
		for(int b=0;b<length_blobs;b++){
			PFBlob blob = (PFBlob)(aPFBlobs[b]);
			printf( "  blob[%d] :\n", b );
			printf( "    blob->index = [%d] \n", blob->index );
			printf( "    blob->color = [%u] \n", blob->color );
			printf( "    blob->length_segmentList = [%d] \n", blob->length_segmentList );
			printf( "    blob->length_approximatedSegmentList = [%d] :\n", blob->length_approximatedSegmentList );
			for(int s=0;s<blob->length_approximatedSegmentList;s++){
				TFSegment segment = blob->approximatedSegmentList[s];
				printf( "      (%4.0f,%4.0f) -> (%4.0f,%4.0f) \n", segment.p1.x, segment.p1.y, segment.p2.x, segment.p2.y );
			}
			printf( "    blob->length_vectorChain = [%d] \n", blob->length_vectorChain );
			for(int v=0;v<blob->length_vectorChain;v++){
				TFVector vector = blob->vectorChain[v];
				printf( "      point=(%4.0f,%4.0f), angle=(%f), length=(%f) \n", vector.point.x, vector.point.y, vector.angle, vector.length );
			}
			printf( "    blob->perimeter = [%f] \n", blob->perimeter );
			printf( "    blob->gravityCenter = (%f,%f) \n", blob->gravityCenter.x, blob->gravityCenter.y );
		}
		// dispose
		filters_deleteFilter( filterBlobExplorer );
		image_freeImage( imageLoaded );
	}
	printf("test9 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test10()
{
	printf("\n------------------------------\n");
	printf("test10 begin :\n");
	printf("->test filterEnvelope and RegionOfInterest (ROI)\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "blob2.tif" );
	if( imageLoaded != NULL ){
		__int32 filterEnvelope = filters_createFilter( "filterEnvelope" );
		filters_setParameterImage( filterEnvelope, "inImage", imageLoaded );
		filters_setParameterInteger( filterEnvelope, "smooth", 25 );
		// set ROI
		TIRect roi;
		roi.Left = 20; 
		roi.Top = 60;
		roi.Right = 280;
		roi.Bottom = 170;
		filters_setRegionOfInterest( filterEnvelope, &roi );
		filters_run( filterEnvelope );
		PArrayOfPFBitmap32 imagesEnvelope = filters_getOutputImages( filterEnvelope, "outImages" );	
		// save image envelope min with ROI
		PFBitmap32 imageEnvelopeMin = image_getImagesImageAtIndex( imagesEnvelope, 0 ); 
		helper_saveImage( imageEnvelopeMin, "testFiltersDllC_output_envelopeMin_ROI.jpg" );
		// remove ROI
		filters_unsetRegionOfInterest( filterEnvelope );
		filters_run( filterEnvelope );
		imagesEnvelope = filters_getOutputImages( filterEnvelope, "outImages" );	
		// save image envelope min without ROI
		imageEnvelopeMin = image_getImagesImageAtIndex( imagesEnvelope, 0 ); 
		helper_saveImage( imageEnvelopeMin, "testFiltersDllC_output_envelopeMin.jpg" );
		// dispose
		filters_deleteFilter( filterEnvelope );
		image_freeImage( imageLoaded );
	}
	printf("test10 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test11()
{
	printf("\n------------------------------\n");
	printf("test11 begin :\n");
	printf("->test image buffer : search the intensity min and max of the image\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "lenna_color.bmp" );
	if( imageLoaded != NULL ){
		TFColor32 *src = imageLoaded->Bits;
		unsigned __int8 intensityMin = 255;
		unsigned __int8 intensityMax = 0;
		for(int row=0; row<imageLoaded->Height; row++){
		  for(int col=0; col<imageLoaded->Width; col++){
			TFColor32 color = *src;
			unsigned __int8 intensity = image_intensity( color );
			if( intensity<intensityMin) intensityMin = intensity;
			if( intensity>intensityMax) intensityMax = intensity;
			src++;
		  }
		}
		printf( " intensityMin = [%d] \n", intensityMin );
		printf( " intensityMax = [%d] \n", intensityMax );
	}
	printf("test11 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test12()
{
	printf("\n------------------------------\n");
	printf("test12 begin :\n");
	printf("->test image  : create a sample image\n\n");
	PFBitmap32 imageCreated = image_createImage( 256, 256 );
	if( imageCreated != NULL ){
		TFColor32 *src = imageCreated->Bits;
		for(int row=0; row<imageCreated->Height; row++){
		  for(int col=0; col<imageCreated->Width; col++){
			PFColor32 color = src;
			*color = image_gray32( row );
			src++;
		  }
		}
		// save image created
		helper_saveImage( imageCreated, "testFiltersDllC_output_test12.jpg" );
		// dispose
		image_freeImage( imageCreated );
	}
	printf("test12 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test13()
{
	printf("\n------------------------------\n");
	printf("test13 begin :\n");
	printf("->test filterAdjust\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "lenna_color.bmp" );
	if( imageLoaded != NULL ){
		__int32 filterAdjust = filters_createFilter( "filterAdjust" );
		PFBitmap32 imageOut = image_createImageLike( imageLoaded );
		filters_setParameterImage( filterAdjust, "inImage", imageLoaded );
		filters_setParameterImage( filterAdjust, "outImage", imageOut );
		filters_setParameterInteger( filterAdjust, "contrast", 255 ); // 128 = no change
		filters_setParameterInteger( filterAdjust, "brightness", 0 ); // 0 = no change
		filters_run( filterAdjust );
		// save 
		helper_saveImage( imageOut, "testFiltersDllC_output_adjust.jpg" );
		// dispose
		image_freeImage( imageOut );
		filters_deleteFilter( filterAdjust );
		image_freeImage( imageLoaded );
	}
	printf("test13 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test14()
{
	printf("\n------------------------------\n");
	printf("test14 begin :\n");
	printf("->test image functions\n\n");
	// create image test
	PFBitmap32 image1 = image_createImageTest( 512, 512 );
	// keep only the red channel in the rect (100,100)(200,200)
	for( int row=100; row<=200; row++ ){
		for( int col=100; col<=200; col++ ){
			TFColor32 color = image_getPixel( image1, col, row );
			unsigned __int8 redChannel = image_redComponent( color );
			TFColor32 newColor = image_color32( redChannel, 0, 0 );
			image_setPixel( image1, col, row, newColor );
		}
	}
	// save 
	helper_saveImage( image1, "testFiltersDllC_output_test14_1.jpg" );
	// create image of the same size than image1
	PFBitmap32 image2 = image_createImageLike( image1 );
	// copy image
	image_copyImageToImage( image1, image2 );
	// draw lines
	image_drawLine( image2, 50, 300, 100, 300, clYellow32, 1 );
	image_drawLine( image2, 50, 350, 100, 350, clAqua32, 5 );
	image_drawLine( image2, 50, 400, 100, 450, clFuchsia32, 1 );
	image_drawLine( image2, 50, 400, 100, 500, clBlue32, 1 );
	TFSegment segment;
	segment.p1.x = 10; segment.p1.y = 10; segment.p2.x = 500; segment.p2.y = 10;
	for(int i=1; i<=10; i++ ){
		image_drawLineSegment( image2, &segment, clWhite32, i );
		segment.p2.y += 20;
	}
	// draw rects
	TFRect r;
	r.Left = 270; r.Top = 300; r.Right = 410; r.Bottom = 370;
	image_drawRect( image2, &r, clWhite32, 1 );
	r.Top += 120; r.Bottom += 120; 
	image_drawRectFilled( image2, &r, clWhite32 );
	// draw disk
	image_drawDisk( image2, 100.0, 150.0, 50.0, clLime32 );
	// save 
	helper_saveImage( image2, "testFiltersDllC_output_test14_2.jpg" );
	// dispose
	image_freeImage( image1 );
	image_freeImage( image2 );
	printf("test14 end.\n\n");
}
// ----------------------------------------------------------------------------------------------------
void test15()
{
	printf("\n------------------------------\n");
	printf("test15 begin :\n");
	printf("->test filterConvolution\n\n");
	PFBitmap32 imageLoaded = helper_loadImage( "lenna_color.bmp" );
	if( imageLoaded != NULL ){
		__int32 filterConvolution = filters_createFilter( "filterConvolution" );
		PFBitmap32 imageOut = image_createImageLike( imageLoaded );
		PFBitmap32 convImage = image_createImage( 3, 3 );
		filters_setParameterImage( filterConvolution, "imageConv", convImage );
		filters_setParameterImage( filterConvolution, "inImage", imageLoaded );
		filters_setParameterImage( filterConvolution, "outImage", imageOut );

		// test convolution "Laplace"
		// 'None', 'Laplace', 'Hipass', 'FindEdges', 'Sharpen', 'EdgeEnhance', 'Emboss', 'Soften', 'Blur', 'SobelHorizontal', 'SobelVertical'
		filters_setParameterString( filterConvolution, "convType", "Laplace" );
		filters_run( filterConvolution );
		helper_saveImage( imageOut, "testFiltersDllC_output_convolution_Laplace.jpg" );

		// test convolution "Personal"
		image_setPixel( convImage, 0, 0, image_gray32(128-16) );
		image_setPixel( convImage, 1, 0, image_gray32(128-16) );
		image_setPixel( convImage, 2, 0, image_gray32(128-16) );
		image_setPixel( convImage, 0, 1, image_gray32(128-16) );
		image_setPixel( convImage, 1, 1, image_gray32(128+127) );
		image_setPixel( convImage, 2, 1, image_gray32(128-16) );
		image_setPixel( convImage, 0, 2, image_gray32(128-16) );
		image_setPixel( convImage, 1, 2, image_gray32(128-16) );
		image_setPixel( convImage, 2, 2, image_gray32(128-16) );
		filters_setParameterInteger( filterConvolution, "z", 10 );
		filters_setParameterImage( filterConvolution, "imageConv", convImage );
		filters_setParameterString( filterConvolution, "convType", "Personal" );
		filters_run( filterConvolution );
		helper_saveImage( imageOut, "testFiltersDllC_output_convolution_Personal.jpg" );

		// dispose
		image_freeImage( imageOut );
		filters_deleteFilter( filterConvolution );
		image_freeImage( imageLoaded );
	}
	printf("test15 end.\n\n");
}


// ----------------------------------------------------------------------------------------------------
// ----------------------------------------------------------------------------------------------------
int main(int argc, char* argv[])
{
	printf("TutorialC1 : Hello Filters !\n");

	filters_initialize();
	test1();
	test2();
	test3();
	test4();
	test5();
	test6();
	test7();
	test8();
	test9();
	test10();
	test11();
	test12();
	test13();
	test14();
	test15();
	filters_unInitialize();

	printf("\n------------------------------\n");
	printf("contact : filters@edurand.com\n");

	printf("\n(press [ENTER] to close)\n");

	getchar();
	return 0;
}
// ----------------------------------------------------------------------------------------------------
// ----------------------------------------------------------------------------------------------------

