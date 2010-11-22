
#if !defined(__TFiltersPlugin_h)
#define __TFiltersPlugin_h


extern "C" {
#include "wrapper_filtersDll.h"
}


#define FiltersPlugin_Version "V20061214"

class TFiltersPlugin
{

protected:
    // Region Of Interest
    TFRect _roi;

public:

	TFiltersPlugin();
	~TFiltersPlugin();

	void _run(void);
	void _run( const char* aCommand );

	__int64 _getParametersCount();
	void _getParameterName( const __int32 aIndex, char* aName );
	void _getParameterHelp( const __int32 aIndex, char* aHelp );
	void _setParameterInteger( const char* aName,  const __int64 aValue );
	void _setParameterFloat( const char* aName, const float aValue );
	void _setParameterBoolean( const char* aName, const bool aValue );
	void _setParameterString( const char* aName, const char* aValue );
	void _setParameterImage( const char* aName, const PFBitmap32 aImage );
	void _setParameterImagesCount( const char* aName, const __int32 aCount );
	void _setParameterImagesImageAtIndex( const char* aName, const PFBitmap32 aImage, const __int32 aIndex );
	void _setParameterPointer( const char* aName,  const Pointer aPointer );

	__int32 _getOutputsCount();
    void _getOutputName( const __int32 aIndex, char* aName );
    PFBitmap32 _getOutputImage( const char* aName );
    __int32 _getOutputImagesCount( const char* aName );
	PFBitmap32 _getOutputImagesImageAtIndex( const char* aName, const __int32 aIndex );
    __int32 _getOutputInteger( const char* aName );
    float _getOutputFloat( const char* aName );
    __int32 _getOutputArrayPointersCount( const char* aName );
    Pointer _getOutputArrayPointersPointerAtIndex( const char* aName, const __int32 aIndex );

	void _setRegionOfInterest(  const PFRect roi );
	void _unsetRegionOfInterest();
};

#endif 
