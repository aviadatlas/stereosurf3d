import FiltersDllPy as FDP

if __name__ == "__main__":
    f = FDP.FiltersDllPy()
    inImg = "H:/DEV/FiltersTutorial/Bin/blob2.tif"
    outImg = "H:/DEV/FiltersTutorial/Bin/testFiltersDllPy_output_sobel.jpg"
    filterName = "filterSobel" 	
    imageLoaded = f.loadImage(inImg)
    imageSobel = f.CreateImageLike(imageLoaded)
    par = (("inImage", imageLoaded),
    ("outImage", imageSobel),
    ("blurIteration", 1),
    ("gain", 5),
    ("thresholdLower", 1),
    ("thresholdUpper", 255),
    )
    f.createFilterRun(filterName, par)
    f.saveImage(imageSobel, outImg)
