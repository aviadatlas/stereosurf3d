import FiltersDllPy as FDP
f = FDP.FiltersDllPy()
f.Initialize()
print f.version
inImg = "c:/DEV/FiltersTutorial/Bin/blob2.tif"
outImg = "c:/DEV/FiltersTutorial/Bin/testFiltersDllPy_output_sobel.jpg"
imageLoaded = f.loadImage(inImg)
imageSobel = f.CreateImageLike(imageLoaded)
par = (("inImage", imageLoaded),
    ("outImage", imageSobel),
    ("blurIteration", 1),
    ("gain", 5),
    ("thresholdLower", 1),
    ("thresholdUpper", 255),
)
f.CreateFilterRun("filterSobel", par)
f.saveImage(imageSobel, outImg)
f.UnInitialize()
