using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Threading;

using AForge;
using AForge.Imaging.Filters;

using OpenSURFcs;

namespace BA_StereoSURF
{
    public enum InterestAlgorithm { SURF, SIFT };

    public class ExtendedImage
    {
        // members
        private Bitmap _image;
        private Bitmap _image_surfed;
        private List<IPoint> _interestPoints;

        private bool _applySurfFilter;
        private bool _applyEdgeFilter;

        private bool _appliedSurfFilter;
        private bool _appliedEdgeFilter;


        Form1 __root;

        // constructors
        public ExtendedImage(Form1 root, string path)
        {
            __root = root;

            _image = new Bitmap(path);
            _interestPoints = new List<IPoint>();
            _appliedEdgeFilter = false;
            _appliedSurfFilter = false;
        }


        // methods
        public void calculatePointsOfInterest(InterestAlgorithm algo)
        {
            _image_surfed = null;
            switch (algo)
            {
                case InterestAlgorithm.SIFT:
                    // not supported yet
                    break;
                case InterestAlgorithm.SURF:
                    IntegralImage iImg = IntegralImage.FromImage(_image);
                    _interestPoints = new List<IPoint>();

                    Thread th = new Thread(delegate()
                        {
                            _interestPoints = FastHessian.getIpoints((float)__root.Pref_Surf_Threshold,
                                                                (int)__root.Pref_Surf_Octaves,
                                                                (int)__root.Pref_Surf_Samples,
                                                                iImg);
                            SurfDescriptor.DecribeInterestPoints(_interestPoints, false, false, iImg);
                        });
                    th.Start();

                    /*_interestPoints = FastHessian.getIpoints(   (float)__root.Pref_Surf_Threshold,
                                                                (int)__root.Pref_Surf_Octaves,
                                                                (int)__root.Pref_Surf_Samples, 
                                                                iImg);*/

                    
                    break;
            }
        }

        // props
        /// <summary>
        /// Gibt/Setzt Bild
        /// </summary>
        public Bitmap Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public Bitmap ImageSurfed
        {
            get
            {
                if (_image_surfed == null || _appliedSurfFilter!=_applySurfFilter || _appliedEdgeFilter != _applyEdgeFilter)
                {       
                    _image_surfed = new Bitmap(_image);  
                    Graphics g = Graphics.FromImage(_image_surfed);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    if (_applySurfFilter)
                    {                        
                        if (_interestPoints.Count == 0)
                        {
                            IntegralImage iImg = IntegralImage.FromImage(_image);
                            _interestPoints = new List<IPoint>();
                            _interestPoints = FastHessian.getIpoints((float)__root.Pref_Surf_Threshold,
                                                                        (int)__root.Pref_Surf_Octaves,
                                                                        (int)__root.Pref_Surf_Samples,
                                                                        iImg);
                            SurfDescriptor.DecribeInterestPoints(_interestPoints, false, false, iImg);
                        }

                        Pen penWhite = new Pen(Color.FromArgb(31, 255, 255 , 255));
                        Pen penBlack = new Pen(Color.FromArgb(31, 0, 0 , 0));
                        Pen penGreen = new Pen(Color.Green);
                        Pen myPen;

                        foreach (IPoint ip in _interestPoints)
                        {
                            int s = 2 * Convert.ToInt32(2.5f * ip.scale);
                            int r = Convert.ToInt32(s / 2f);

                            Point pt = new Point(Convert.ToInt32(ip.x), Convert.ToInt32(ip.y));
                            Point ptR = new Point(Convert.ToInt32(r * Math.Cos(ip.orientation)), Convert.ToInt32(r * Math.Sin(ip.orientation)));

                            myPen = (ip.laplacian > 0 ? penWhite : penBlack);
                           
                            g.FillEllipse(new SolidBrush(Color.FromArgb(63, 255-(int)myPen.Color.R, 255-(int)myPen.Color.G, 255-(int)myPen.Color.B)), pt.X - r, pt.Y - r, s, s); 
                            g.DrawEllipse(myPen, pt.X - r, pt.Y - r, s, s);                        
                            g.DrawLine(new Pen(Color.FromArgb(90, 255, 255, 0)), new Point(pt.X, pt.Y), new Point(pt.X+ptR.X,pt.Y+ptR.Y));
                        }
                    }

                    if (_applyEdgeFilter)
                    {
                        AForge.Imaging.Filters.SobelEdgeDetector sobel = new AForge.Imaging.Filters.SobelEdgeDetector();
                        AForge.Imaging.Filters.Grayscale grayFilter = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709;

                        Bitmap tmpImage = grayFilter.Apply(_image_surfed);
                        sobel.ApplyInPlace(tmpImage);

                        /*Bitmap tmpImage2 = new Bitmap(tmpImage);

                        for (int x=0; x<tmpImage.Width; x++)
                        {
                            for (int y=0; y<tmpImage.Height; y++)
                            {
                                Color c = tmpImage.GetPixel(x,y);
                                if (c.R+c.G+c.B > 155)
                                    tmpImage2.SetPixel(x,y,Color.White);
                                else
                                    tmpImage2.SetPixel(x,y,Color.Black);
                            }
                        }*/

                        /*AForge.Imaging.Filters.FiltersSequence filterSequence = new AForge.Imaging.Filters.FiltersSequence();
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { 0, 0, 0 }, { -1, 1, -1 }, { 1, 1, 1 } },
                            HitAndMiss.Modes.Thinning));
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { -1, 0, 0 }, { 1, 1, 0 }, { -1, 1, -1 } },
                            HitAndMiss.Modes.Thinning));
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { 1, -1, 0 }, { 1, 1, 0 }, { 1, -1, 0 } },
                            HitAndMiss.Modes.Thinning));
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { -1, 1, -1 }, { 1, 1, 0 }, { -1, 0, 0 } },
                            HitAndMiss.Modes.Thinning));
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { 1, 1, 1 }, { -1, 1, -1 }, { 0, 0, 0 } },
                            HitAndMiss.Modes.Thinning));
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { -1, 1, -1 }, { 0, 1, 1 }, { 0, 0, -1 } },
                            HitAndMiss.Modes.Thinning));
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { 0, -1, 1 }, { 0, 1, 1 }, { 0, -1, 1 } },
                            HitAndMiss.Modes.Thinning));
                        filterSequence.Add(new AForge.Imaging.Filters.HitAndMiss(
                            new short[,] { { 0, 0, -1 }, { 0, 1, 1 }, { -1, 1, -1 } },
                            HitAndMiss.Modes.Thinning));



                        AForge.Imaging.Filters.FilterIterator filter = new AForge.Imaging.Filters.FilterIterator(filterSequence, 5);
                        System.Drawing.Bitmap tmpImage2 = filter.Apply(tmpImage);*/

                        ContrastStretch contrast = new ContrastStretch();
                        contrast.ApplyInPlace(tmpImage);

                        /*AForge.Imaging.Filters.Add addFilter = new AForge.Imaging.Filters.Add(tmpImage);
                        addFilter.ApplyInPlace(_image_surfed);*/
                        _image_surfed = tmpImage;
                    }

                    _appliedSurfFilter = _applySurfFilter;
                    _appliedEdgeFilter = _applyEdgeFilter;

                    g.Dispose();
                }                              
                return _image_surfed;
            }
        }

        /// <summary>
        /// Liefert Punkte von räumlicher-relevanz mittels SURF
        /// </summary>
        public List<IPoint> InterestPointsSURF
        {
            get { return _interestPoints; }
            //set { _interestPoints = value; }
        }

        public bool ApplySurfFilter
        {
            get { return _applySurfFilter; }
            set { _applySurfFilter = value; }
        }
        public bool ApplyEdgeFilter
        {
            get { return _applyEdgeFilter; }
            set { _applyEdgeFilter = value; }
        }
    }
}
