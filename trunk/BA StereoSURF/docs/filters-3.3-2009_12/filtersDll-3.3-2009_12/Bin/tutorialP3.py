import FiltersDllPy as FDP
import time

## test WebCam
def testWebCam():
    filtersPlugin_WebCam = f.CreateFilter( "filtersPlugin_WebCam" )
    f.SetParameterBoolean( filtersPlugin_WebCam, "showViewer", False )
    f.RunCommand( filtersPlugin_WebCam, 'READ_DEVICES' )
    devices = f.GetOutputArrayPointers( filtersPlugin_WebCam, 'devices' )
    devices_length = f.GetOutputArrayPointersLength( filtersPlugin_WebCam, 'devices' )
    print 'found [%d] devices :' % devices_length
    f.DeleteFilter( filtersPlugin_WebCam )



## test Video
def testVideo():
    # we create the [filtersPlugin_Video]
    filtersPlugin_Video = f.CreateFilter( "filtersPlugin_Video" )
    # for debug only, we would like to see it's window, to see the video
    f.SetParameterBoolean( filtersPlugin_Video, "showViewer", True )
    f.SetParameterInteger( filtersPlugin_Video, "win_x", 0 )
    f.SetParameterInteger( filtersPlugin_Video, "win_y", 0 )
    # we set the file name of the video to load
    f.SetParameterString( filtersPlugin_Video, "fileName", "C:/DEV2/Images/pieces200608/320/OK_320_1.avi" )
    # we ask to load it
    f.RunCommand( filtersPlugin_Video, 'LOAD' )
    # and to play it
    print 'play the video'
    f.RunCommand( filtersPlugin_Video, 'PLAY' )
    
    # now we will capture images of the playing video, and apply a [filterContrastExplorer] on each captured image
    filterContrastExplorer = f.CreateFilter( "filterContrastExplorer" )
    fImageContrast = None
    
    # we will use [filtersPlugin_Viewer] to show the image contrast of the image capture
    filtersPlugin_Viewer = f.CreateFilter( "filtersPlugin_Viewer" )
    f.SetParameterBoolean( filtersPlugin_Viewer, "showViewer", True )
    f.SetParameterInteger( filtersPlugin_Viewer, "win_x", 0 )
    f.SetParameterInteger( filtersPlugin_Viewer, "win_y", 200 )
    
    # we capture n images
    n = 20
    for i in range(n):
        # we sleep 1s between each capture
        time.sleep(0.5)
        # we capture a image from the video
        print 'capture image ' + str(i+1)
        f.Run( filtersPlugin_Video )
        fImageCapture = f.GetOutputImage( filtersPlugin_Video, 'outImage')
        # we can save it
        f.saveImage( fImageCapture, 'C:/DEV/FiltersTutorial/Bin/tutorialP3_capture.bmp')
        # we apply the contrast filter on it
        print 'apply contrast on captured image'
        if fImageContrast<>None:
            f.FreeImage( fImageContrast )
        fImageContrast = f.CreateImageLike( fImageCapture )
        f.SetParameterImage( filterContrastExplorer, 'inImage', fImageCapture );
        f.SetParameterImage( filterContrastExplorer, 'outImage', fImageContrast );
        f.SetParameterInteger( filterContrastExplorer, 'precision', 1 );
        f.SetParameterString( filterContrastExplorer, 'mode', 'NORMAL' );
        f.Run( filterContrastExplorer )
        # we show it in the viewer
        f.SetParameterImage( filtersPlugin_Viewer, 'inImage', fImageContrast )
        f.Run( filtersPlugin_Viewer )
        # we can save it too
        f.saveImage( fImageContrast, 'C:/DEV/FiltersTutorial/Bin/tutorialP3_contrast.bmp')    
        
    print 'stop the video'
    f.RunCommand( filtersPlugin_Video, 'STOP' )
    
    
    print 'press [ENTER] to finish...'
    ch=raw_input()
    
    f.DeleteFilter( filterContrastExplorer )
    f.DeleteFilter( filtersPlugin_Viewer )
    f.DeleteFilter( filtersPlugin_Video )
    f.FreeImage( fImageContrast )



f = FDP.FiltersDllPy()
## we would like to be sure of the directory used by Filters to search for plugins
f.setProperty("PLUGINS_DIRECTORY","c:/DEV/FiltersTutorial/Bin/")
print 'Initialize'
f.Initialize()
print 'Filters version : ' + f.version

testWebCam()
testVideo()

print 'UnInitialize'
f.UnInitialize()
