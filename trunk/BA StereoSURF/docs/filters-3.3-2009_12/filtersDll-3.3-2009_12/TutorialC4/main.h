//---------------------------------------------------------------------------

#ifndef mainH
#define mainH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ExtCtrls.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TLabel *Label1;
	TEdit *txtFiltersVersion;
	TEdit *txtImageToLoad;
	TButton *cmdLoad;
	TButton *cmdSave;
	TEdit *txtImageToSave;
	TButton *cmdSobel;
	TPanel *Panel1;
	TImage *ViewerImage;
	void __fastcall FormCreate(TObject *Sender);
	void __fastcall FormDestroy(TObject *Sender);
	void __fastcall cmdLoadClick(TObject *Sender);
	void __fastcall cmdSobelClick(TObject *Sender);
	void __fastcall cmdSaveClick(TObject *Sender);
private:	// User declarations
	__int32 filterImageLoader;
	__int32 filterImageSaver;
	__int32 filterSobel;
	Filters::PFBitmap32 imageLoaded;
	Filters::PFBitmap32 imageSobel;
	void __fastcall showImage(const Filters::PFBitmap32 aImage, const TImage *aViewer);
public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
