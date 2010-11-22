import FiltersDllPy as FDP 
import Image 
import time
from ctypes import *


## convert 'PIL' image to 'Filters' image
    ## Filters image format is Little Endian (x86 / MS Windows, Linux) : BGR(A) order (0xAARRGGBB)
    ## but PIL image format is Big Endian (PPC / Linux, MaxOSX) : (A)RGB order (0xRRGGBBAA)
def createImageFromImagePIL(aImagePIL):
    result = f.CreateImage(aImagePIL.size[0], aImagePIL.size[1])
    imagePIL_RGBA = imagePIL.convert('RGBA') #make sure that the format is RGBA
    imagePIL_BGRA = Image.fromstring('RGBA', imagePIL_RGBA.size, imagePIL_RGBA.tostring(), 'raw', 'BGRA')
    s = imagePIL_BGRA.tostring()
    memmove(result[0].Bits, s, len(s))
    return result

## convert 'Filters' image to 'PIL' image
    ## Filters image format is Little Endian (x86 / MS Windows, Linux) : BGR(A) order (0xAARRGGBB)
    ## but PIL image format is Big Endian (PPC / Linux, MaxOSX) : (A)RGB order (0xRRGGBBAA)
def createImagePILFromImage(aImage):
    imageSize = aImage[0].Width, aImage[0].Height
    s = create_string_buffer(aImage[0].Width*aImage[0].Height*4)
    memmove(s, aImage[0].Bits, len(s))
    imagePIL = Image.fromstring('RGBA', imageSize, s, 'raw', 'BGRA')
    return imagePIL


f = FDP.FiltersDllPy()
f.Initialize()

#get a image in PIL format from PIL library
imagePIL = Image.open('C:/DEV/FiltersTutorial/Bin/lenna_color.bmp')
print 'imagePIL : width=[' + str(imagePIL.size[0]) +'], height=[' + str(imagePIL.size[1]) + ']'

#convert it to Filters image format
t = time.time()
fImage = createImageFromImagePIL(imagePIL)
duration = ((time.time()-t)*1000)
print 'createImageFromImagePIL duration : %3.0f' % duration + 'ms'

#use it in Filters library...

#for example we save it
print 'save fImage'
f.saveImage(fImage, 'C:/DEV/FiltersTutorial/Bin/filtersImageFromPIL.bmp')

#for example we process it with [filterCanny]
print 'process fImage to fImageCanny with filterCanny'
fImageCanny = f.CreateImageLike(fImage) 
par = (
('inImage', fImage), 
('outImage', fImageCanny), 
('threshold1', 130), 
('threshold2', 255), 
) 
filterCanny = f.createFilterRun('filterCanny', par)
print 'save fImageCanny'
#don't forget that we have provided [outImage] as a parameter,
#so it's not a image created by [filterCanny], and this filter just fill it
#then we can directly use it
f.saveImage(fImageCanny, 'C:/DEV/FiltersTutorial/Bin/filtersImageFromPIL_canny.bmp')

#now for example we can process it with [filterRotation]
print 'process fImage to fImageRotation with filterRotation'
par = (
('inImage', fImage), 
('angle', 75.0), 
('interpolationMode', 1), #basic fast
('autoAdjustSize', True), 
("missingPixelColorMode", "BLACK"), 
)
filterRotation = f.createFilterRun('filterRotation', par)
print 'save fImageRotation'
#don't forget that we DON'T provided [outImage] as a parameter
#because [filterRotation] must create it (because it calcul the correct size)
#so IT'S a image created by [filterRotation]
#then we MUST obtain it with the function [GetOutputImage]
fImageRotation = f.GetOutputImage(filterRotation, 'outImage')

#print 'fImageRotation : width=[' + str(FDP.TFBitmap32(fImageRotation).Width) +'], height=[' + str(fImageRotation.Height) + ']'
f.saveImage(fImageRotation, 'C:/DEV/FiltersTutorial/Bin/filtersImageFromPIL_rotation.bmp')

print 'Compute Laplace Convolution'
filterConvolution = f.CreateFilter( 'filterConvolution' )
fImageLaplace = f.CreateImageLike(fImageRotation)
fImageConvMatrix = f.CreateImage(3,3)
f.SetParameterImage( filterConvolution, 'imageConv', fImageConvMatrix )
f.SetParameterInteger( filterConvolution, 'z', 1 )
f.SetParameterString( filterConvolution, 'convType', str('Laplace') )
f.SetParameterImage( filterConvolution, 'inImage', fImageRotation )
f.SetParameterImage( filterConvolution, 'outImage', fImageLaplace )
f.Run( filterConvolution )
f.saveImage(fImageLaplace, 'C:/DEV/FiltersTutorial/Bin/filtersImageFromPIL_laplace.bmp')


#for other example, and until a real documentation,
# you can have a look on the code of 'FiltersTest' at :
# http://filters.sourceforge.net/wikifilters/index.php/Code_of_FiltersTest

#convert it to PIL image format
t = time.time()
imagePIL = createImagePILFromImage(fImageLaplace)
duration = ((time.time()-t)*1000)
print 'createImagePILFromImage duration : %3.0f' % duration + 'ms'

#use it in PIL library...
imagePIL.show()

print 'free memory'
f.FreeImage(fImage)
f.FreeImage(fImageCanny)
f.FreeImage(fImageLaplace)
f.DeleteFilter(filterCanny)
f.DeleteFilter(filterRotation)
f.DeleteFilter(filterConvolution)

print 'press [ENTER] to finish...'
ch=raw_input()

f.UnInitialize()
