//---------------------------------------------------------------------------

#include "windows.h"

namespace Filters{
extern "C" {
  #include "wrapper_filtersDll.h"
}
}

#include <vcl.h>
#pragma hdrstop

#include "main.h"


//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormCreate(TObject *Sender)
{
	Filters::filters_initialize();
	txtFiltersVersion->Text = Filters::filters_getVersion();
	filterImageLoader = Filters::filters_createFilter( "filterImageLoader" );
	filterImageSaver = Filters::filters_createFilter( "filterImageSaver" );
	filterSobel = Filters::filters_createFilter( "filterSobel" );
}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormDestroy(TObject *Sender)
{
	// we don't have to delete 'imageLoaded',
	//   because it's a image created, and then managed, by 'filterImageLoader'
	// we have to delete 'imageSobel', because it was created by our program,
	//  and only filled by the 'filterSobel'
	Filters::image_freeImage( imageSobel );
	Filters::filters_deleteFilter( filterImageLoader );
	Filters::filters_deleteFilter( filterImageSaver );
	Filters::filters_deleteFilter( filterSobel );
	Filters::filters_unInitialize();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::cmdLoadClick(TObject *Sender)
{
	// exception if file not found, so be careful of your working directory
	char* strImageToLoad = txtImageToLoad->Text.c_str();
	Filters::filters_setParameterString( filterImageLoader, "filesName", strImageToLoad );
	Filters::filters_run( filterImageLoader );
	Filters::PArrayOfPFBitmap32 images = Filters::filters_getOutputImages( filterImageLoader, "outImages" );
	__int32 imagesCount = Filters::image_getImagesCount( images );
	if( imagesCount>0 ){
		imageLoaded = Filters::image_getImagesImageAtIndex( images, 0 );
		showImage( imageLoaded, ViewerImage );
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::cmdSobelClick(TObject *Sender)
{
	if( imageLoaded!=NULL ){
		imageSobel = Filters::image_eraseOrCreateImageLike( imageSobel, imageLoaded );
		Filters::filters_setParameterImage( filterSobel, "inImage", imageLoaded );
		Filters::filters_setParameterImage( filterSobel, "outImage", imageSobel );
		Filters::filters_setParameterInteger( filterSobel, "blurIteration", 1 );
		Filters::filters_setParameterInteger( filterSobel, "gain", 5 );
		Filters::filters_setParameterInteger( filterSobel, "thresholdLower", 1 );
		Filters::filters_setParameterInteger( filterSobel, "thresholdUpper", 255 );
		Filters::filters_run( filterSobel );
		showImage( imageSobel, ViewerImage );
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::cmdSaveClick(TObject *Sender)
{
	if( imageSobel!=NULL ){
		char* strImageToSave = txtImageToSave->Text.c_str();
		Filters::filters_setParameterString( filterImageSaver, "fileName", strImageToSave );
		Filters::filters_setParameterImage( filterImageSaver, "inImage", imageSobel );
		Filters::filters_run( filterImageSaver );
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::showImage(const Filters::PFBitmap32 aImage, const TImage *aViewer)
{
	const SizeOfColor32 = sizeof(Filters::TFColor32);
	int row;
	Filters::PFColor32 pSrc, pDest;
	Graphics::TBitmap *b;

	if (aImage != NULL){
		b = aViewer->Picture->Bitmap;
		b->PixelFormat = pf32bit;
		b->Width = aImage->Width;
		b->Height = aImage->Height;
		pSrc = aImage->Bits;

		for (row=0; row < b->Height; row++){
			pDest = (unsigned *)b->ScanLine[row];
			Move(pSrc, pDest, SizeOfColor32*b->Width);
			pSrc += b->Width;
		}
		aViewer->Invalidate();
		aViewer->Refresh();
	}
}
//---------------------------------------------------------------------------

