#!/usr/bin/env python
# -*- coding: utf-8 -*-

# ------------------------------------------ #
# Welcome to FiltersDllPy, a python wrapper 
# for Filters (http://filters.sf.net)
# 
# For use me, you can need only 
# python >= 2.4.x     www.python.org
# ctypes >= 0.9.9.6   sf.net/projects/ctypes/
# filtersDll          sf.net/projects/filters/ (now filtersDll-2.1-2006_07_22.zip)
#
# Project files:
#    FiltersDllPy.py -> main developed file
#           aka: the wrapper
#    test_FiltersDllPy.py -> main test file
#    py2exe_test_FiltersDllPy.py -> python file
#           for create standalone exe with the
#           current test_FiltersDllPy.py
#
# Version: 0.2
#
# License:
#    LGPL
#
# Thanks to:
#   Durand Emmanuel filters@edurand.com
#   All the ctypes users that help me
#
# Copyright Michele Petrazzo
# michele.petrazzo@unipex.it
# ----------------------------------------- #

import ctypes as C, sys, os, string
from ctypes.util import find_library

LIBNAME = "FiltersDll.dll"

#Image path separator. Useful for see if the image exists
IMG_PATH_SEP = ";"

INT = C.c_int
UINT = C.c_uint
FLOAT = C.c_float
CHAR = C.c_char_p
VOID = C.c_void_p

clBlack32 = 0xFF000000
clDimGray32= 0xFF3F3F3F
clGray32= 0xFF7F7F7F
clLightGray32= 0xFFBFBFBF
clWhite32= 0xFFFFFFFF
clMaroon32= 0xFF7F0000
clGreen32= 0xFF007F00
clOlive32= 0xFF7F7F00
clNavy32= 0xFF00007F
clPurple32= 0xFF7F007F
clTeal32= 0xFF007F7F
clRed32= 0xFFFF0000
clLime32= 0xFF00FF00
clYellow32= 0xFFFFFF00
clBlue32= 0xFF0000FF
clFuchsia32= 0xFFFF00FF
clAqua32= 0xFF00FFFF

TFColor32 = C.c_int
PFColor32 = C.POINTER(TFColor32)

class TFBitmap32(C.Structure):
    _fields_ = [("Width", INT),
                ("Height", INT),
                ("Bits", PFColor32)]


class TIRect(C.Structure):
    _fields_ = [("Left", INT),
                ("Top", INT),
                ("Right", INT),
                ("Bottom", INT),]

PIRect = C.POINTER(TIRect)

class TFRect(C.Structure):
    _fields_ = [("Left", INT),
                ("Top", INT),
                ("Right", INT),
                ("Bottom", INT),]

PFRect = C.POINTER(TFRect)


class TFPoint(C.Structure):
    _fields_ = [("x", FLOAT),
                ("y", FLOAT),]

PFPoint = C.POINTER(TFPoint)

class TFSegment(C.Structure):
    _fields_ = [("p1", TFPoint),
                ("p2", TFPoint),
                ("width", FLOAT),]

PFSegment = C.POINTER(TFSegment)
TFSegmentList = C.POINTER(TFSegment)

class TFVector(C.Structure):
    _fields_ = [ ("point", FLOAT),
                ("length", FLOAT),
                ("angle", FLOAT),]

PFVector = C.POINTER(TFVector)
TFVectorList = C.POINTER(TFVector)

class TFBlob(C.Structure):
    _fields_ = [("index", INT),
                ("color", TFColor32),
                ("segmentList", PFSegment),
                ("length_segmentList", INT),
                ("approximatedSegmentList", TFSegmentList),
                ("length_approximatedSegmentList", INT),
                ("vectorChain", TFVectorList),
                ("length_vectorChain", INT),
                ("rectangleContainer", INT),
                ("pixelArea", INT),
                ("gravityCenter", TFPoint),
                ("perimeter", FLOAT), ]
                

PFBlob = C.POINTER(TFBlob)

PFBitmap32 = C.POINTER(TFBitmap32)
ArrayOfPFBitmap32 = C.POINTER(PFBitmap32)
PArrayOfPFBitmap32 = C.POINTER(ArrayOfPFBitmap32)

Pointer = C.c_void_p
ArrayOfPointer = C.POINTER(Pointer)
PArrayOfPointer = C.POINTER(ArrayOfPointer)

#Set Parameters consts
P_BOOL = 1
P_FLOAT = 2
P_IMAGE = 3
P_IMAGES = 4
P_INT = 5
P_POINT = 6
P_STR = 7

dSetParameters = {P_BOOL: "SetParameterBoolean", P_FLOAT: "SetParameterFloat",
                  P_IMAGE: "SetParameterImage", P_IMAGES: "SetParameterImages",
                  P_INT: "SetParameterInteger", P_POINT: "SetParameterPointer",
                  P_STR: "SetParameterString",}

#Exceptions

class FiltersDllPy(Exception): 
    """Main wrapper Exception"""
    pass

class SetParameterError(FiltersDllPy):
    """ Exception raised when set the paramters and there is an error"""
    pass

#One per one error types
#Not need to explain all :)

class SetParameterBooleanError(SetParameterError):
    pass
class SetParameterFloatError(SetParameterError):
    pass
class SetParameterImageError(SetParameterError):
    pass
class SetParameterImagesError(SetParameterError):
    pass
class SetParameterIntegerError(SetParameterError):
    pass
class SetParameterPointerError(SetParameterError):
    pass
class SetParameterStringError(SetParameterError):
    pass

class FiltersDllPy(object):
    def __init__(self, libraryName=None):
        """
        """
        os.environ['PATH'] = os.environ['PATH'] + ';' + os.path.abspath(os.path.dirname(__file__))
        l_path = find_library(LIBNAME)
        if not l_path:
            raise AttributeError, "I cannot find the library %s. Did you pass me a valid name?" % LIBNAME
        self._FD_lib = getattr(C.windll, l_path)

        #Function names and its return type
        funct_lst = ( 
                      ("setProperty", VOID),
                      ("initialize", VOID),
                      ("unInitialize", VOID),
                      ("createFilter", INT),
                      ("deleteFilter", INT),
                      ("getFiltersCount", INT),
                      ("getFiltersNameAtIndex", CHAR),
                      ("getOutputArrayPointers", PArrayOfPointer),
                      ("getOutputArrayPointersLength", INT),

                      ("getOutputsCount", INT),
                      ("getOutputFloat", FLOAT),
                      ("getOutputImage", PFBitmap32),
                      ("getOutputImages", PArrayOfPFBitmap32),
                      ("getOutputInteger", INT),
                      ("getOutputName", VOID),
                      
                      ("getParameterHelp", VOID),
                      ("getParameterName", VOID),
                      ("getParametersCount", INT),
                        
                      ("getVersion", CHAR),
                      ("getLastError", CHAR),
                      
                      ("image_color32", TFColor32),
                      ("image_copyImageToImage", VOID),
                      ("image_createImage", PFBitmap32),
                      ("image_createImageFromImage", PFBitmap32),
                      ("image_createImageLike", PFBitmap32),
                      ("image_createImageTest", PFBitmap32),
                      ("image_drawLine", VOID),
                      ("image_drawLineSegment", VOID),
                      ("image_drawRect", VOID),
                      ("image_drawRectFilled", VOID),
                      ("image_drawDisk", VOID),
                      ("image_eraseOrCreateImageLike", PFBitmap32),
                      ("image_eraseImage", VOID),
                      ("image_freeImage", VOID),
                      ("image_freeImages", VOID),
                      ("image_getPixel", TFColor32),
                      ("image_getPixelAsSingle", FLOAT),
                      ("image_getPixelAsString", VOID),
                      ("image_HSLtoRGB", TFColor32),
                      ("image_getImagesCount", INT),
                      ("image_getImagesImageAtIndex", PFBitmap32),
                      ("image_gray32", TFColor32),
                      ("image_intensity", INT),
                      ("image_luminosity", UINT),
                      ("image_redComponent", UINT),
                      ("image_greenComponent", UINT),
                      ("image_blueComponent", UINT),
                      ("image_setPixel", VOID),
                      ("image_setPixelAsSingle", VOID),
                      
                      ("run", VOID),
                      ("runCommand", VOID),
                      
                      ("setParameterBoolean", VOID),
                      ("setParameterFloat", VOID),
                      ("setParameterImage", VOID),
                      ("setParameterImages", VOID),
                      ("setParameterInteger", VOID),
                      ("setParameterPointer", VOID),
                      ("setParameterString", VOID),
                     
                      ("setRegionOfInterest", VOID),
                      ("unsetRegionOfInterest", VOID),
                     )

        for funct_name, restype in funct_lst:
            funct = getattr(self._FD_lib, funct_name)

            if restype:
               funct.restype = restype

    #
    # Library methods
    #


    def SetProperty(self, p_name, p_value ):
        name, value = C.c_char_p(p_name), C.c_char_p(p_value)
        return self._FD_lib.setProperty(name, value)

    def setProperty(self, p_name, p_value ):
	return self.SetProperty(p_name,p_value)

    def Initialize(self):
        return self._FD_lib.initialize()

    def initialize(self):
	return self.Initialize()

    def UnInitialize(self):
        return self._FD_lib.unInitialize()

    def unInitialize(self):
	return self.UnInitialize()

    def createFilter(self, filterName):
        """ Create the filter with the names passed
            @par filterName: The filter name
            @type filterName: string
            @return: Filter id
        """
        if not string.upper(filterName) in self.getFiltersNames():
            raise ValueError, "The filter [%s] is not available" % filterName
        
        return self._FD_lib.createFilter( C.c_char_p(filterName) )

    def CreateFilter(self, filterName):
	return self.createFilter( filterName )
    
    def DeleteFilter(self, id_filter):
        """ Delete the filter
            @par id_filter: Filter id
            @type id_filter: int
        """
        return self._FD_lib.deleteFilter(id_filter)

    def GetFiltersCount(self):
        """ Return the number of filters avaible
            @return: int
        """
        return self._FD_lib.getFiltersCount()

    def GetFiltersNameAtIndex(self, index):
        """ Return filter name at that index
            @par index: The parameter index
            @type index: int
        """
        return self._FD_lib.getFiltersNameAtIndex(index)
    
    def GetOutputArrayPointers(self, id_filter, aOutputName):
        """ Return array of pointers
            @par id_filter: Filter id
            @type id_filter: int
        """
        return self._FD_lib.getOutputArrayPointers(id_filter, aOutputName)
    
    def GetOutputArrayPointersLength(self, id_filter, aOutputName):
        """ Return the length of the array of pointers returned by the function 'GetOutputArrayPointers'
            @par id_filter: Filter id
            @type id_filter: int
        """
        return self._FD_lib.getOutputArrayPointersLength(id_filter, aOutputName)
    
    def GetOutputsCount(self, id_filter):
        """ Return output counts
            @par id_filter: Filter id
            @type id_filter: int
        """
        return self._FD_lib.getOutputsCount(id_filter)

    def GetOutputFloat(self, filter, outName):
        """
        """
        return self._FD_lib.getOutputFloat(filter, outName)

    def GetOutputInteger(self, filter, outName):
        """
        """
        return self._FD_lib.getOutputInteger(filter, outName)
    
    def GetOutputImage(self, id_filter, aOutputName):
        """ Return the image
        """
        if not isinstance(aOutputName, basestring):
            raise ValueError, "Pass me a valid string, not: " + repr( aOutputName )
        
        img = self._FD_lib.getOutputImage(id_filter, aOutputName)
        return img
    
    def GetOutputImages(self, id_filter, aOutputName):
        """ Return images array
        """
        if not isinstance(aOutputName, basestring):
            raise ValueError, "Pass me a valid string, not: " + repr( aOutputName )
        
        imgs = self._FD_lib.getOutputImages(id_filter, aOutputName)
        return imgs
    
    def GetOutputName(self, id_filter, o_count):
        """ Return output counts
            @par id_filter: Filter id
            @type id_filter: int
            @par o_count: Output count index
            @type o_count: int
            @return: string
        """
        b = C.create_string_buffer(256)
        self._FD_lib.getOutputName(id_filter, o_count, C.pointer(b))
        return b.value

    def GetParameterName(self, id_filter, p_count):
        """ Return parameter name
            @par id_filter: Filter id
            @type id_filter: int
            @par p_count: Parameter count index
            @type p_count: int
            @return: string
        """
        b = C.create_string_buffer(256)
        self._FD_lib.getParameterName(id_filter, p_count, C.pointer(b))
        return b.value

    def GetParameterHelp(self, id_filter, p_count):
        """ Return parameter help description
            @par id_filter: Filter id
            @type id_filter: int
            @par p_count: Parameter count index
            @type p_count: int
            @return: string
        """
        b = C.create_string_buffer(256)
        self._FD_lib.getParameterHelp(id_filter, p_count, b)
        return b.value

    def GetParametersCount(self, id_filter):
        """ Return the parameters count for this filter
        """
        return self._FD_lib.getParametersCount(id_filter)

    def GetVersion(self):
        """ Return the dll version
        """
        return self._FD_lib.getVersion()

    def GetLastError(self):
        """ Return the last error message
        """
        return self._FD_lib.getLastError()
    
    def Run(self, filter):
        """ Run the filter
        """
        return self._FD_lib.run(filter)
    
    def RunCommand(self, filter, p_command):
        """ Run a Command of the filter
        """
        command = C.c_char_p(p_command)
        return self._FD_lib.runCommand(filter,command)
    
    def SetParameterBoolean(self, id_filter, p_name, p_value ):
        """ Set the parameter bool
        """
        name, value = C.c_char_p(p_name), C.c_int(p_value)
        try:
            return self._FD_lib.setParameterBoolean(id_filter, name, value)
        except WindowsError:
            raise SetParameterBooleanError, self.GetLastError()

    def SetParameterFloat(self, id_filter, p_name, p_value ):
        """ Set the parameter float
        """
        name, value = C.c_char_p(p_name), C.c_float(p_value)
        try:
            return self._FD_lib.setParameterFloat(id_filter, name, value)
        except WindowsError:
            raise SetParameterFloatError, self.GetLastError()

    def SetParameterImage(self, id_filter, p_name, p_value ):
        """ Set the parameter image
        """
        name, value = C.c_char_p(p_name), p_value
        try:
            return self._FD_lib.setParameterImage(id_filter, name, value)
        except WindowsError:
            raise SetParameterImageError, self.GetLastError()        

    def SetParameterImages(self, id_filter, p_name, p_value ):
        """ Set the parameter images
        """
        name, value = C.c_char_p(p_name), p_value
        try:
            return self._FD_lib.setParameterImages(id_filter, name, value)
        except WindowsError:
            raise SetParameterImagesError, self.GetLastError()        

    def SetParameterInteger(self, id_filter, p_name, p_value ):
        """ Set the parameter int
        """
        name, value = C.c_char_p(p_name), C.c_longlong(p_value)
        
        try:
            ret = self._FD_lib.setParameterInteger(id_filter, name, value)
        except WindowsError:
            raise SetParameterIntegerError, self.GetLastError()        

    def SetParameterPointer(self, id_filter, p_name, p_value ):
        """ Set the parameter pointer
        """
        name, value = C.c_char_p(p_name), C.c_void_p(p_value)
        try:
            return self._FD_lib.setParameterPointer(id_filter, name, value)
        except WindowsError:
            raise SetParameterPointerError, self.GetLastError()        

    def SetParameterString(self, id_filter, p_name, p_value ):
        """ Set the parameter string
        """
        name, value = C.c_char_p(p_name), C.c_char_p(p_value)
        try:
            return self._FD_lib.setParameterString(id_filter, name, value)
        except WindowsError:
            raise SetParameterStringError, self.GetLastError()        
    
    def SetRegionOfInterest(self, id_filter, roi ):
        """ Set the RegionOfInterest
        """
        return self._FD_lib.setRegionOfInterest(id_filter, C.addressof(roi))

    def UnsetRegionOfInterest(self, id_filter ):
        """ Unset the RegionOfInterest
        """
        return self._FD_lib.unsetRegionOfInterest(id_filter)

    #
    # Image methods
    #
    
    def Color32(self, r,g,b):
        """
        """
        return self._FD_lib.image_color32(r,g,b)
    
    def CopyImageToImage(self, aImageFrom, aImageTo):
        """
        """
        return self._FD_lib.image_copyImageToImage(aImageFrom, aImageTo)
    
    def CreateImage(self, width, height):
        """ Create and image with the size passed
            @type width: int
            @type height: int
        """
        return self._FD_lib.image_createImage(width, height)
    
    def CreateImageFromImage(self, image):
        """ Create an image from an image
        """
        return self._FD_lib.image_createImageFromImage(image)
    
    def CreateImageLike(self, image):
        """ Create (clone?) image
        """
        return self._FD_lib.image_createImageLike(image)
    
    def CreateImageTest(self, width, height):
        """ Create an image with this size
        """
        return self._FD_lib.image_createImageTest(width, height)
    
    def DrawLine(self, aImage, FromX, FromY, ToX, ToY, aColor32, aThick):
        """ Draw a line into the image at the given points
        """
        aThick = C.c_float(aThick)
        return self._FD_lib.image_drawLine(aImage, FromX, FromY, ToX, ToY, aColor32, aThick)
    
    def DrawLineSegment(self, aImage, aSegment, aColor32, aThick):
        """ Draw a segment
        """
        aSegment = C.addressof(aSegment)
        aThick = C.c_float(aThick)
        return self._FD_lib.image_drawLineSegment( aImage, aSegment, aColor32, aThick )
    
    def DrawRect(self, aImage, aRect, aColor32, aThick ):
        """ Draw a rect
        """
        aRect = C.addressof(aRect)
        aThick = C.c_float(aThick)
        return self._FD_lib.image_drawRect( aImage, aRect, aColor32, aThick )
    
    def DrawRectFilled(self, aImage, aRect, aColor32):
        """ Draw a rect filled by the aColor32
        """
        aRect = C.addressof(aRect)
        return self._FD_lib.image_drawRectFilled(aImage, aRect, aColor32)
    
    def DrawDisk(self, aImage, aCenterX, aCenterY, aradius, aColor32):
        """ Draw a cirle
        """
        aCenterX, aCenterY, aradius = map(C.c_float, (aCenterX, aCenterY, aradius))
        
        return self._FD_lib.image_drawDisk(aImage, aCenterX, aCenterY, aradius, aColor32)
    
    def EraseImage(self, aImage):
        """
        """
        return self._FD_lib.image_eraseImage( aImage )
    
    # don't work
    ##def EraseOrCreateImageLike(self, lastImage, aImage):
    ##    """
    ##    """
    ##    return self._FD_lib.image_eraseOrCreateImageLike(lastImage, aImage)
    
    def FreeImage(self, image):
        """ Free the image
        """
        return self._FD_lib.image_freeImage(image)
        
    def FreeImages(self, images):
        """ Free the images
        """
        return self._FD_lib.image_freeImages(images)
    
    def GetPixel(self, aImage, x, y):
        """
        """
        return self._FD_lib.image_getPixel(aImage, x, y)
    
    def GetPixelAsSingle(self, aImage, x, y):
        """
        """
        return self._FD_lib.image_getPixelAsSingle(aImage, x, y)
    
    def GetPixelAsString(self, aImage, x, y, aStr):
        """
        """
        aStr = C.c_char_p(aStr)
        return self._FD_lib.image_getPixelAsString(aImage, x, y, aStr)
    
    def GetImagesCount(self, images):
        """ Return the number of images
        """
        return self._FD_lib.image_getImagesCount(images)
    
    def GetImagesImageAtIndex(self, images, index):
        """ Get image at the index. Return the image
        """
        return self._FD_lib.image_getImagesImageAtIndex(images, INT(index))
    
    def Gray32(self, intensity):
        """
        """
        return self._FD_lib.image_gray32(intensity)
    
    def HSLtoRGB(self, H, S, L):
        """ Convert HSL to RGB
        """
        H,S,L = map(C.c_float, (H,S,L))
        return self._FD_lib.image_HSLtoRGB(H,S,L)
    
    def Intensity(self, aColor32):
        """ Return the image intensity
        """
        return self._FD_lib.image_intensity(aColor32)

    def Luminosity(self, aColor32):
        """ Return the image luminosity
        """
        return self._FD_lib.image_luminosity(aColor32)

    def RedComponent(self, aColor32):
        """ Return the image red
        """
        return self._FD_lib.image_redComponent(aColor32)

    def GreenComponent(self, aColor32):
        """ Return the image green
        """
        return self._FD_lib.image_greenComponent(aColor32)

    def BlueComponent(self, aColor32):
        """ Return the image blue
        """
        return self._FD_lib.image_blueComponent(aColor32)
    
    def SetPixel(self, aImage, x, y, aColor32):
        """
        """
        return self._FD_lib.image_setPixel(aImage, x, y, aColor32)
    
    def SetPixelAsSingle(self, aImage, x, y, data):
        """
        """
        return self._FD_lib.image_setPixelAsSingle(aImage, x, y, data)
    #
    # Utility methods
    #
    
    #Do the filter work
    def createFilterRun(self, filterName, parameters, id_filter=None):
        """ Create the filter (if filter is None), set the parameters and run it
            Paraters must be: (("par_name", "par_value", "paramter_type"), )
            Return the filter
        """
        
        #control the paramters type
        if not isinstance(parameters, (list, tuple)):
            raise ValueError, "Pass me a valid list or tuple at createFilterRun"
        
        #and for what paramters we need to understand the type
        lstParNoType = [ x[0] for x in filter(lambda x: len(x) != 3, parameters) ]
        
        newParameters = []
        for par in parameters:
            par_name, par_value = par[:2]
            
            if par_name in lstParNoType:
                #Try to understand the parameter type from its value type
                if isinstance(par_value, basestring): 
                    par_type = P_STR
                elif isinstance(par_value, bool): 
                    par_type = P_BOOL
                elif isinstance(par_value, int): 
                    par_type = P_INT
                elif isinstance(par_value, float): 
                    par_type = P_FLOAT
                elif isinstance(par_value, (PFBitmap32, TFBitmap32)): 
                    par_type = P_IMAGE
                elif isinstance(par_value, PArrayOfPFBitmap32): 
                    par_type = P_IMAGES
                else:
                    raise ValueError, "Parameter %s not understood. Pass me its type (%s)" % (par_name, type(par_value) )
            else:
                par_name, par_value, par_type = par
            
            #Recreate the list
            newParameters.append( ( par_name, par_value, par_type ) )
        
        #Create the filter, is need
        if id_filter == None:
            fil = self.CreateFilter( filterName )
        else:
            fil = id_filter
        
        for par_name, par_value, par_type in newParameters:
            
            #Retrieve the functions for set the parameters
            functCall = getattr(self, dSetParameters[par_type])
            
            #call it
            functCall( fil, par_name, par_value)
        
        self.Run(fil)
        
        return fil

    def CreateFilterRun(self, filterName, parameters, id_filter=None):
	return self.createFilterRun(filterName, parameters, id_filter)
    
    #Load and save
    def loadImage(self, img_path):
        """ Load the image path
        """
        
        #Control if image(s) exists and not leave the dll raise an (unreadble)
        #windows error. We are wait for a better dll response :)
        
        imgNotExists = filter(lambda x: not os.path.exists(x), img_path.split(IMG_PATH_SEP) )
        if imgNotExists:
            raise "The image(s) %s not exists" % str(imgNotExists)
        
        imageOut = None
        
        par = (("filesName", img_path, P_STR), )
        fil = self.createFilterRun( "filterImageLoader", par )
        imgs = self.GetOutputImages(fil, "outImages")
        
        ic = self.GetImagesCount(imgs)
        
        if ic > 0:
            imageLoaded = self.GetImagesImageAtIndex(imgs, 0)
            if imageLoaded:
                imageOut = self.CreateImageFromImage( imageLoaded );
                
        self.DeleteFilter(fil)
        
        return imageOut

    def saveImage(self, image, img_path):
        """ Save the image into the path
        """
        
        par = (("fileName", img_path, P_STR), ("inImage", image, P_IMAGE))
        fil = self.createFilterRun( "filterImageSaver", par )
        self.DeleteFilter(fil)
    
    #Filters list
    def getFiltersNames(self):
        """ Return all the filter names avaible
        """
        l = [ self.GetFiltersNameAtIndex(x) for x in range(self.GetFiltersCount()) ]
        return l

    def getFilterParameters(self, name_filter):
        """ Get the filter paramters
        """
        #Load the data
        fid = self.CreateFilter(name_filter)
        p_count = self.GetParametersCount(fid)
        o_count = self.GetOutputsCount(fid)

        #Parameter work
        parms = dict()
        for p_c in range(p_count):
            pname = self.GetParameterName(fid, p_c)
            phelp = self.GetParameterHelp(fid, p_c)
            parms[pname] = phelp

        out = list()
        #Output work
        for o_c in range(o_count):
            out.append( self.GetOutputName(fid, o_c) )

        #Delete the fitler
        self.DeleteFilter(fid)

        return parms, out
   
    def getAllFiltersParamters(self):
       """ Return all the filters with its parameters
       """

       fp = dict()
       for num, f_name in enumerate(self.getFiltersNames()):
           fp[f_name] = self.getFilterParameters(f_name)

       return fp 

    #Free the memory
    def freeImage(self, imgs):
        """ Free the image(s)
            @par imgs: Image istances or list of 
        """
        if not isinstance(imgs, (list, tuple)):
            imgs = [imgs]
        
        for img in imgs:
            self.FreeImage(img)

    filtersNames = property(getFiltersNames)
    version = property(GetVersion)
