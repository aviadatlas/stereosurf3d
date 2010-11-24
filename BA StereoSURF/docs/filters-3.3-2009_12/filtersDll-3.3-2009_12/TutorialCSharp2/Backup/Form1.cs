/* ***** BEGIN LICENSE BLOCK *****
 * Copyright (C) 2004-2007 Durand Emmanuel
 * Copyright (C) 2004-2007 Burgel Eric
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
 * ***** END LICENSE BLOCK ***** *
*/

/*
 edurand (filters@edurand.com)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using FiltersDllDotNet;
using System.Runtime.InteropServices;

namespace TutorialCSharp2
{
    public partial class Form1 : Form
    {
        private Int32 filterImageLoader;
        private Int32 filterImageSaver;
        private IntPtr imageLoaded;
        private IntPtr imageTmp;
        private IntPtr imageToSave = IntPtr.Zero;

        public Form1()
        {
            InitializeComponent();
            Filters.initialize();
            filterImageLoader = Filters.createFilter("filterImageLoader");
            filterImageSaver = Filters.createFilter("filterImageSaver");
        }

        ~Form1()
        {
            Filters.image_freeImage(imageTmp);
            Filters.deleteFilter(filterImageLoader);
            Filters.deleteFilter(filterImageSaver);
            Filters.unInitialize();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            cmdLoad.PerformClick();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    txtFileName.Text = "lenna_color.bmp";
                    break;
                case 1:
                    txtFileName.Text = "blob2.tif";
                    break;
                case 2:
                    txtFileName.Text = "lenna_color.bmp";
                    break;
                case 3:
                    txtFileName.Text = "blobRotation.tif";
                    break;
                case 4:
                    txtFileName.Text = "blob2.tif";
                    break;
                case 5:
                    txtFileName.Text = "blob2.tif";
                    break;
                case 6:
                    txtFileName.Text = "320_240.bmp";
                    break;
                case 7:
                    txtFileName.Text = "lenna_color.bmp";
                    break;
                case 8:
                    txtFileName.Text = "lenna_color.bmp";
                    break;
            }
            cmdLoad.PerformClick();
        }

        private void showImage(IntPtr image)
        {
            Filters.TFBitmap32 bitmapFilters = Filters.helper_ptrToTFBitmap32(image);
            Bitmap returnMap = new Bitmap(bitmapFilters.width, bitmapFilters.height,
                                   PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapFilters.bits;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                int imagePointer2LineLength = (bitmapData2.Width * 4);
                byte[] tempBits = new byte[bitmapFilters.width*4];
                for (int i = 0; i < bitmapFilters.height; i++)
                {
                    Marshal.Copy((IntPtr)imagePointer1, tempBits, 0, tempBits.Length);
                    // a supprimer
                    for (int j = 0; j < tempBits.Length; j += 4)
                    {
                        tempBits[j+3] = 255;
                    }
                    Marshal.Copy(tempBits, 0, (IntPtr)imagePointer2, tempBits.Length);
                    imagePointer2 += imagePointer2LineLength;
                    imagePointer1 += bitmapFilters.width * 4;
                }
            }
            returnMap.UnlockBits(bitmapData2);
            pictureBox1.Image = returnMap;
        }

        private void setImageToSave(IntPtr image)
        {
            if(imageToSave!=null){
                Filters.image_freeImage(imageToSave);
            }
            imageToSave = Filters.image_createImageFromImage(image);
            showImage(imageToSave);
            Filters.TFBitmap32 bitmapFilters = Filters.helper_ptrToTFBitmap32( imageToSave );
            txtImageSize.Text = "width:" + bitmapFilters.width + ", height:" + bitmapFilters.height;
        }

        private void test1()
        {
            Filters.TFBitmap32 bitmapFilters = Filters.helper_ptrToTFBitmap32( imageToSave );
            Bitmap returnMap = new Bitmap(bitmapFilters.width, bitmapFilters.height,
                                   PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapFilters.bits;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                int strideOffset = bitmapData2.Stride - (bitmapData2.Width * 4);
                for (int i = 0; i < bitmapFilters.height; i++)
                {
                    for (int j = 0; j < bitmapFilters.width; j++)
                    {
                        imagePointer2[0] = imagePointer1[0]; // Blue
                        imagePointer2[1] = imagePointer1[1]; // Green
                        imagePointer2[2] = imagePointer1[2]; // Red
                        imagePointer2[3] = imagePointer1[3]; // Alpha
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }//end for j
                    //the remaining
                    imagePointer2 += strideOffset;
                }//end for i
            }//end unsafe
            returnMap.UnlockBits(bitmapData2);
            pictureBox1.Image = returnMap;
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            string strImageToLoad = txtFileName.Text;
            Filters.setParameterString(filterImageLoader, "filesName", strImageToLoad);
            Filters.run(filterImageLoader);
            IntPtr images = Filters.getOutputImages(filterImageLoader, "outImages");
            Int32 imagesCount = Filters.image_getImagesCount(images);
            txtImageCount.Text = imagesCount.ToString();
            if (imagesCount == 1)
            {
                imageLoaded = Filters.image_getImagesImageAtIndex(images, 0);
                imageTmp = Filters.image_createImageFromImage(imageLoaded);
                setImageToSave(imageTmp);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string strImageToSave = txtFileNameToSave.Text;
            Filters.setParameterString(filterImageSaver, "fileName", strImageToSave);
            Filters.setParameterImage(filterImageSaver, "inImage", imageToSave);
            Filters.run(filterImageSaver);
        }

        private void cmdSobel_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Int32 filterSobel = Filters.createFilter("filterSobel");
            Filters.setParameterImage(filterSobel, "inImage", imageLoaded);
            Filters.setParameterImage(filterSobel, "outImage", imageTmp);
            Filters.setParameterInteger(filterSobel, "blurIteration", 1);
            Filters.setParameterInteger(filterSobel, "gain", 5);
            Filters.setParameterInteger(filterSobel, "thresholdLower", 1);
            Filters.setParameterInteger(filterSobel, "thresholdUpper", 255);
            Filters.run(filterSobel);
            Filters.deleteFilter(filterSobel);
            setImageToSave(imageTmp);
        }

        private void cmdRotation_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Int32 filterRotation = Filters.createFilter("filterRotation");
            Filters.setParameterImage(filterRotation, "inImage", imageLoaded);
            float angle;
            Single.TryParse(txtRotationAngle.Text, out angle);
            Filters.setParameterFloat(filterRotation, "angle", angle);
            Filters.TFBitmap32 imageLoadedNET = Filters.helper_ptrToTFBitmap32(imageLoaded);
            Filters.setParameterFloat(filterRotation, "xCenter", (float)(imageLoadedNET.width / 2));
            Filters.setParameterFloat(filterRotation, "yCenter", (float)(imageLoadedNET.height / 2));
            Filters.setParameterBoolean(filterRotation, "autoAdjustSize", true);
            Filters.setParameterInteger(filterRotation, "interpolationMode", 1);
            Filters.setParameterString(filterRotation, "missingPixelColorMode", "BLACK");
            Filters.run(filterRotation);
            IntPtr imageRotation = Filters.getOutputImage(filterRotation, "outImage");
            setImageToSave(imageRotation);
            Filters.deleteFilter(filterRotation);
        }

        private void cmdStack_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            IntPtr imageLoaded1 = Filters.helper_loadImage("lenna_color.bmp");
            IntPtr imageLoaded2 = Filters.helper_loadImage("blob2.tif");
            if (imageLoaded1 != IntPtr.Zero && imageLoaded2 != IntPtr.Zero)
            {
                Int32 filterStackCreator = Filters.createFilter("filterStackCreator");
                Int32 filterStackProcessor = Filters.createFilter("filterStackProcessor");
                Int32 filterCanny = Filters.createFilter("filterCanny");
                Filters.setParameterImage(filterStackCreator, "inImage", imageLoaded1);
                Filters.run(filterStackCreator);
                Filters.setParameterImage(filterStackCreator, "inImage", imageLoaded2);
                Filters.run(filterStackCreator);
                Int32 imagesCount = Filters.getOutputInteger(filterStackCreator, "currentCount");
                IntPtr images = Filters.getOutputImages(filterStackCreator, "outImages");
                Filters.setParameterImages(filterStackProcessor, "inImages", images);
                Filters.setParameterPointer(filterStackProcessor, "filter", (IntPtr)filterCanny);
                Filters.run(filterStackProcessor);
                images = Filters.getOutputImages(filterStackProcessor, "outImages");
                imagesCount = Filters.image_getImagesCount(images);
		        // save images
		        string strImageToSave;
		        for(int j=0;j<imagesCount;j++){
			        strImageToSave="testFiltersCSharp_output_stack"+j+".jpg";
                    txtInfo.Items.Add("see files ["+strImageToSave+"]");
                    IntPtr tmpImage = Filters.image_getImagesImageAtIndex(images, j);
                    Filters.helper_saveImage(tmpImage, strImageToSave);
                    setImageToSave(tmpImage);
                }
		        // dispose
                Filters.image_freeImage(imageLoaded1);
                Filters.image_freeImage(imageLoaded2);
                Filters.deleteFilter(filterCanny);
                Filters.deleteFilter(filterStackProcessor);
                Filters.deleteFilter(filterStackCreator);
	        }
        }

        private void cmdBlobRepositioning2_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Int32 filterBlobRepositioning2 = Filters.createFilter("filterBlobRepositioning2");
            Filters.setParameterImage(filterBlobRepositioning2, "inImage", imageLoaded);
            Filters.setParameterInteger(filterBlobRepositioning2, "blob_ThresholdBackground", 100);
            Filters.setParameterInteger(filterBlobRepositioning2, "blob_AreaMin", 10);
            Filters.run(filterBlobRepositioning2);
            float angleToRestorOrientation = Filters.getOutputFloat(filterBlobRepositioning2, "angleToRestorOrientation");
            txtInfo.Items.Add( "angleToRestorOrientation = [" + angleToRestorOrientation + "] degree" );
            IntPtr imageBlobRepositioning2 = Filters.getOutputImage(filterBlobRepositioning2, "outImage");
            setImageToSave(imageBlobRepositioning2);
            Filters.deleteFilter(filterBlobRepositioning2);
        }

        private void cmdBlobExplorer_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Int32 filterBlobExplorer = Filters.createFilter("filterBlobExplorer");
            IntPtr imageBlob = Filters.image_createImageLike(imageLoaded);
            Filters.setParameterImage(filterBlobExplorer, "inImage", imageLoaded);
            Filters.setParameterImage(filterBlobExplorer, "outImage", imageBlob);
            Filters.setParameterInteger(filterBlobExplorer, "intensityBackground", 100);
            Filters.setParameterInteger(filterBlobExplorer, "intensityPrecision", 10);
            Filters.setParameterString(filterBlobExplorer, "enableBlobArea", "TRUE");
            Filters.setParameterInteger(filterBlobExplorer, "blobAreaMin", 40);
            Filters.setParameterBoolean(filterBlobExplorer, "criticalPoints", true);
            Filters.setParameterInteger(filterBlobExplorer, "contourCriticalPointsAppoximationAccuracy", 10);
            Filters.setParameterBoolean(filterBlobExplorer, "blobSurfaceInfo", true);
            Filters.run(filterBlobExplorer);
            setImageToSave(imageBlob);
            IntPtr paPFBlobs = Filters.getOutputArrayPointers(filterBlobExplorer, "blobs");
            Int32 length_blobs = Filters.getOutputArrayPointersLength(filterBlobExplorer, "blobs");
            txtInfo.Items.Add("filterBlobExplorer length_blobs = [" + length_blobs + "]");
            for (int b = 0; b < length_blobs; b++)
            {
                txtInfo.Items.Add("  blob[" + b + "] :");
                IntPtr ptrOnBlob = Filters.helper_getItemOfArrayPointers(paPFBlobs, b);
                Filters.TFBlob blob = Filters.helper_ptrToTFBlob(ptrOnBlob);
                txtInfo.Items.Add("    blob->index = [" + blob.index + "]");
                txtInfo.Items.Add("    blob->length_segmentList = [" + blob.length_segmentList + "]");
                txtInfo.Items.Add("    blob->length_approximatedSegmentList = [" + blob.length_approximatedSegmentList + "]");
                Filters.TFSegment[] segments = Filters.helper_ptrToTFSegmentArray(blob.approximatedSegmentList,blob.length_approximatedSegmentList);
                for (int s = 0; s < blob.length_approximatedSegmentList; s++)
                {
                    Filters.TFSegment segment = segments[s];
                    txtInfo.Items.Add("      (" + segment.p1.x + "," + segment.p1.y + ") -> (" + segment.p2.x + "," + segment.p2.y + ")");
                }
                txtInfo.Items.Add("    blob->length_vectorChain = [" + blob.length_vectorChain + "]");
                Filters.TFVector[] vectors = Filters.helper_ptrToTFVectorArray(blob.vectorChain, blob.length_vectorChain);
                for (int s = 0; s < blob.length_vectorChain; s++)
                {
                    Filters.TFVector vector = vectors[s];
                    txtInfo.Items.Add("      point=(" + vector.point.x + "," + vector.point.y + "), angle=(" + vector.angle + "), length=("+vector.length+")");
                }
                txtInfo.Items.Add("    blob->perimeter = [" + blob.perimeter + "]");
                txtInfo.Items.Add("    blob->gravityCenter = (" + blob.gravityCenter.x + "," + blob.gravityCenter.y + ")");
            }
            // dispose
            Filters.deleteFilter(filterBlobExplorer);
        }

        private void bROI_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Int32 filterEnvelope = Filters.createFilter("filterEnvelope");
            Filters.setParameterImage(filterEnvelope, "inImage", imageLoaded);
            Filters.setParameterInteger(filterEnvelope, "smooth", 25);
            // set ROI
            Filters.TFIRect roi = new Filters.TFIRect();
            roi.left = 20;
            roi.top = 60;
            roi.right = 280;
            roi.bottom = 170;
            Filters.setRegionOfInterest(filterEnvelope, roi);
            Filters.run(filterEnvelope);
            IntPtr imagesEnvelope = Filters.getOutputImages(filterEnvelope, "outImages");
            // save image envelope min with ROI
            IntPtr imageEnvelopeMin = Filters.image_getImagesImageAtIndex(imagesEnvelope, 0);
            setImageToSave(imageEnvelopeMin);
            Filters.helper_saveImage(imageEnvelopeMin, "testFiltersDllCSharp_output_envelopeMin_ROI.jpg");
            txtInfo.Items.Add("see file [testFiltersDllCSharp_output_envelopeMin_ROI.jpg]");
            // remove ROI
            Filters.unsetRegionOfInterest(filterEnvelope);
            Filters.run(filterEnvelope);
            imagesEnvelope = Filters.getOutputImages(filterEnvelope, "outImages");
            // save image envelope min without ROI
            imageEnvelopeMin = Filters.image_getImagesImageAtIndex(imagesEnvelope, 0);
            Filters.helper_saveImage(imageEnvelopeMin, "testFiltersDllCSharp_output_envelopeMin.jpg");
            txtInfo.Items.Add("see file [testFiltersDllCSharp_output_envelopeMin.jpg]");
            // dispose
            Filters.deleteFilter(filterEnvelope);
        }

        private void bAnalyseImage_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Filters.TFBitmap32 image = Filters.helper_ptrToTFBitmap32(imageLoaded);
		    int src = image.bits.ToInt32();
		    byte intensityMin = 255;
		    byte intensityMax = 0;
		    for(int row=0; row<image.height; row++){
		        for(int col=0; col<image.width; col++){
                    IntPtr ptrSrc = new IntPtr(src);
			        Filters.TFColor32 color = Filters.helper_ptrToTFColor32(ptrSrc);
			        byte intensity = Filters.image_intensity( color );
			        if( intensity<intensityMin) intensityMin = intensity;
			        if( intensity>intensityMax) intensityMax = intensity;
			        src+=4;
		        }
		    }
		    txtInfo.Items.Add( "intensityMin = ["+intensityMin+"]" );
            txtInfo.Items.Add( "intensityMax = ["+intensityMax+"]");
        }

        private void bCreateDrawImage_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            IntPtr imageCreated = Filters.image_createImage(512, 512);
            Filters.TFBitmap32 image = Filters.helper_ptrToTFBitmap32(imageCreated);
            if (imageCreated != null){
                // draw a gray image
                for (Int32 row = 0; row < image.height; row++){
                    for (Int32 col = 0; col < image.width; col++){
                        Filters.TFColor32 color;
                        // with a little of Lenna inside (but just the red channel)
                        if (row > 200 && row < 400 && col > 200 && col < 350)
                        {
                            color = Filters.image_getPixelColor32(imageLoaded, col, row);
                            byte redChannel = Filters.image_redComponent(color);
                            color = Filters.image_toColor32(redChannel, 0, 0);
                        }
                        else
                        {
                            color = Filters.image_toGray32((byte)row);
                        }
                        Filters.image_setPixelColor32(imageCreated, col, row, color);
                    }
                }
                // draw some lines of different color and thick
                Filters.image_drawLine(imageCreated, 50, 300, 100, 300, Filters.clYellow32, 1);
                Filters.image_drawLine(imageCreated, 50, 350, 100, 350, Filters.clAqua32, 5);
                Filters.image_drawLine(imageCreated, 50, 400, 100, 450, Filters.clFuchsia32, 1);
                Filters.image_drawLine(imageCreated, 50, 400, 100, 500, Filters.clBlue32, 1);
                // draw segments
                Filters.TFSegment segment= new Filters.TFSegment();
                segment.p1.x = 10; segment.p1.y = 10; segment.p2.x = 500; segment.p2.y = 10;
                for (int i = 1; i <= 10; i++)
                {
                    Filters.image_drawLineSegment(imageCreated, segment, Filters.clWhite32, i);
                    segment.p2.y += 20;
                }
                // draw rects
                Filters.TFRect r = new Filters.TFRect();
                r.left = 370; r.top = 300; r.right = 490; r.bottom = 370;
                Filters.image_drawRect(imageCreated, r, Filters.clWhite32, 1);
                r.top += 120; r.bottom += 120;
                Filters.image_drawRectFilled(imageCreated, r, Filters.clWhite32);
                // draw disk
                Filters.image_drawDisk(imageCreated, (float)100.0, (float)150.0, (float)50.0, Filters.clLime32);
                // show and save
                setImageToSave(imageCreated);
                Filters.helper_saveImage(imageCreated, "testFiltersDllCSharp_output_createDrawImage.jpg");
                Filters.image_freeImage(imageCreated);
            }
        }

        private void bConvolution_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Int32 filterConvolution = Filters.createFilter("filterConvolution");
            IntPtr imageOut = Filters.image_createImageLike(imageLoaded);
            IntPtr convImage = Filters.image_createImage(3, 3);
            Filters.setParameterImage(filterConvolution, "imageConv", convImage);
            Filters.setParameterImage(filterConvolution, "inImage", imageLoaded);
            Filters.setParameterImage(filterConvolution, "outImage", imageOut);
            // test convolution "Laplace"
            // 'None', 'Laplace', 'Hipass', 'FindEdges', 'Sharpen', 'EdgeEnhance', 'Emboss', 'Soften', 'Blur', 'SobelHorizontal', 'SobelVertical'
            Filters.setParameterString(filterConvolution, "convType", "Laplace");
            Filters.run(filterConvolution);
            setImageToSave(imageOut);
            Filters.helper_saveImage(imageOut, "testFiltersDllCSharp_output_convolution_Laplace.jpg");
            Filters.image_freeImage(imageOut);
            Filters.deleteFilter(filterConvolution);
        }

        private void bConvolutionPersonal_Click(object sender, EventArgs e)
        {
            txtInfo.Items.Clear();
            Int32 filterConvolution = Filters.createFilter("filterConvolution");
            IntPtr imageOut = Filters.image_createImageLike(imageLoaded);
            IntPtr convImage = Filters.image_createImage(3, 3);
            Filters.setParameterImage(filterConvolution, "imageConv", convImage);
            Filters.setParameterImage(filterConvolution, "inImage", imageLoaded);
            Filters.setParameterImage(filterConvolution, "outImage", imageOut);
            Filters.setParameterString(filterConvolution, "convType", "Laplace");
            // test convolution "Personal"
            Filters.image_setPixelColor32(convImage, 0, 0, Filters.image_toGray32(128 - 16));
            Filters.image_setPixelColor32(convImage, 1, 0, Filters.image_toGray32(128 - 16));
            Filters.image_setPixelColor32(convImage, 2, 0, Filters.image_toGray32(128 - 16));
            Filters.image_setPixelColor32(convImage, 0, 1, Filters.image_toGray32(128 - 16));
            Filters.image_setPixelColor32(convImage, 1, 1, Filters.image_toGray32(128 + 127));
            Filters.image_setPixelColor32(convImage, 2, 1, Filters.image_toGray32(128 - 16));
            Filters.image_setPixelColor32(convImage, 0, 2, Filters.image_toGray32(128 - 16));
            Filters.image_setPixelColor32(convImage, 1, 2, Filters.image_toGray32(128 - 16));
            Filters.image_setPixelColor32(convImage, 2, 2, Filters.image_toGray32(128 - 16));
            Filters.setParameterInteger(filterConvolution, "z", 10);
            Filters.setParameterImage(filterConvolution, "imageConv", convImage);
            Filters.setParameterString(filterConvolution, "convType", "Personal");
            Filters.run(filterConvolution);
            setImageToSave(imageOut);
            Filters.helper_saveImage(imageOut, "testFiltersDllCSharp_output_convolution_personal.jpg");
            Filters.image_freeImage(imageOut);
            Filters.deleteFilter(filterConvolution);
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}