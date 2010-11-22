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
using System.Runtime.InteropServices;
using FiltersDllDotNet;

namespace TutorialCSharp3
{
    public partial class Form1 : Form
    {

        private Int32 filtersPlugin_WebCam;

        public Form1()
        {
            InitializeComponent();
            Filters.initialize();
            filtersPlugin_WebCam = Filters.createFilter("filtersPlugin_WebCam");
        }

        ~Form1()
        {
            Filters.deleteFilter(filtersPlugin_WebCam);
            Filters.unInitialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Filters.setParameterBoolean( filtersPlugin_WebCam, "showViewer", false );
            Filters.runCommand( filtersPlugin_WebCam, "READ_DEVICES" );
            // get devices list
            IntPtr pointersDevices = Filters.getOutputArrayPointers( filtersPlugin_WebCam, "devices" );
            Int32 length_pointersDevices = Filters.getOutputArrayPointersLength(filtersPlugin_WebCam, "devices");
            cbWebCamDevice.Items.Clear();
            for (int i = 0; i < length_pointersDevices; i++)
            {
                IntPtr ptrOnDevice = Filters.helper_getItemOfArrayPointers(pointersDevices, i);
                String device = Filters.helper_ptrToString(ptrOnDevice);
                cbWebCamDevice.Items.Add(device);
            }
        }

        private void cbWebCamDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 deviceIndex = cbWebCamDevice.SelectedIndex;
            if (deviceIndex >= 0)
            {
                Filters.setParameterInteger(filtersPlugin_WebCam, "deviceIndex", deviceIndex);
                Filters.runCommand(filtersPlugin_WebCam, "READ_DEVICE_FORMAT");
                // get format list
                IntPtr pointersFormat = Filters.getOutputArrayPointers(filtersPlugin_WebCam, "deviceFormats");
                Int32 length_pointersFormat = Filters.getOutputArrayPointersLength(filtersPlugin_WebCam, "deviceFormats");
                lstReadDeviceFormat.Items.Clear();
                for (int i = 0; i < length_pointersFormat; i++)
                {
                    IntPtr ptrOnFormat = Filters.helper_getItemOfArrayPointers(pointersFormat, i);
                    String format = Filters.helper_ptrToString(ptrOnFormat);
                    lstReadDeviceFormat.Items.Add(format);
                }
            }
        }

        private void lstReadDeviceFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filters.setParameterBoolean(filtersPlugin_WebCam, "showViewer", true);
        }

        private void chkWebCamLive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWebCamLive.Checked == true)
            {
                Filters.setParameterInteger(filtersPlugin_WebCam, "formatIndex", lstReadDeviceFormat.SelectedIndex);
                Filters.runCommand(filtersPlugin_WebCam, "DEVICE_OPEN");
            }
            else
            {
                Filters.runCommand(filtersPlugin_WebCam, "DEVICE_CLOSE");
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            Filters.run(filtersPlugin_WebCam);
            IntPtr outImage = Filters.getOutputImage(filtersPlugin_WebCam, "outImage");
            IntPtr image = Filters.image_createImageFromImage(outImage);
            showImage(image);
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
                byte[] tempBits = new byte[bitmapFilters.width * 4];
                for (int i = 0; i < bitmapFilters.height; i++)
                {
                    Marshal.Copy((IntPtr)imagePointer1, tempBits, 0, tempBits.Length);
                    // a supprimer
                    for (int j = 0; j < tempBits.Length; j += 4)
                    {
                        tempBits[j + 3] = 255;
                    }
                    Marshal.Copy(tempBits, 0, (IntPtr)imagePointer2, tempBits.Length);
                    imagePointer2 += imagePointer2LineLength;
                    imagePointer1 += bitmapFilters.width * 4;
                }
            }
            returnMap.UnlockBits(bitmapData2);
            pictureBox1.Image = returnMap;
        }


    }
}