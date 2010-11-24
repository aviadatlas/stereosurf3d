using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using OpenSURFcs;
using Poly2Tri;

namespace BA_StereoSURF
{
    public class ImageCorrelation
    {       
        /* statics */
        // accuracy in percent
        public const float ACCURACY_JOINT       = 0.0005f;
        public const float ACCURACY_ORIENTATION = 0.05f;
        public const float ACCURACY_SCALE       = 15f;  // total Pixel-difference
        public const float ACCURACY_SURF        = 0.10f;
        public const float CLAMP_TOTAL_PEEKS    = 0.075f; // percentage of of width as max linkingvectorlength
        public const float CLAMP_TOTAL_LOWS     = 0.01f; // percentage of of width as max linkingvectorlength (!high values cutting the background!)

        /* members */
        private ExtendedImage _baseImage;
        private List<ExtendedImage> _refImages;
        private Dictionary<ExtendedImage, List<CorrelationInfo>> _correlations;
        private Dictionary<ExtendedImage, Vector2> _correlationsDirection;
        private Poly2Tri.PointSet _pointSet;
        private Dictionary<TriangulationPoint, float> _triLookUp;

        /* constructors */
        /// <summary>
        /// Erzeugt ein Objekt, das die Beziehung eines Bildes zu den Anderen repräsentiert.
        /// </summary>
        /// <param name="baseImage">Ausgangsbild mit gefülltem SurfDescriptor</param>
        public ImageCorrelation(ExtendedImage baseImage)
        {
            _baseImage = baseImage;
            _refImages = new List<ExtendedImage>();
            _correlations = new Dictionary<ExtendedImage, List<CorrelationInfo>>();
        }

        /* methods */
        /// <summary>
        /// Fügt diverses weitere Bild hinzu um daraus weitere Korrelationen zu errechnen
        /// </summary>
        /// <param name="eImg">Bild mit zuvor berechneten SURF-Daten</param>
        public void AddReferenceImage(ExtendedImage eImg)
        {
            if (eImg.InterestPointsSURF.Count > 0)
            {
                _refImages.Add(eImg);
                _correlations.Add(eImg, getCorrelationInfos(eImg));
            }    
        }   
        /// <summary>
        /// Fügt diverse weitere Bilder hinzu um daraus weitere Korrelationen zu errechnen
        /// </summary>
        /// <param name="range">Bilder mit zuvor berechneten SURF-Daten</param>
        public void AddReferenceImages(List<ExtendedImage> range)
        {
            range.ForEach((item) => { AddReferenceImage(item); });
        }
        /// <summary>
        /// Gibt eine Liste mit allen gefundenen Korrelationen zurück
        /// </summary>
        /// <param name="eImg">Referenzbild</param>
        /// <returns>Eine Liste mit allen Korrelationsinformationen</returns>
        public List<CorrelationInfo> GetCorrelationInfo(ExtendedImage eImg)
        {
            if (_correlations.ContainsKey(eImg))
                return _correlations[eImg];
            else
                return null;
        }
        /// <summary>
        /// Liefert ein fertiges Depthmap-Bild in der Perspektive des Ausgangsbildes
        /// </summary>
        /// <param name="simple">Nur ein Referenzbild wird genutzt</param>
        /// <param name="hdr">Resultierendes Bild hat 32bit Farbtiefe (not supported yet)</param>
        /// <returns></returns>
        public Bitmap GenerateDepthmap(bool simple, bool hdr)
        {
            if (_correlations.Count > 0)
            {
                // calc PODs (peaks of depths - huh how epical^^)
                float zMin = float.MaxValue;
                float zMax = float.MinValue;
                _correlations[_refImages.ElementAt(0)].ForEach((item) =>
                {
                    if (item.Depth < zMin)
                        zMin = item.Depth;
                    if (item.Depth > zMax)
                        zMax = item.Depth;
                });

                // TEST: Tri
                TriangulationPoint[] pts = new TriangulationPoint[_correlations[_refImages.ElementAt(0)].Count];
                _triLookUp = new Dictionary<TriangulationPoint, float>();
                int n = 0;
                foreach (CorrelationInfo ci in _correlations[_refImages.ElementAt(0)])
                {
                    pts[n] = new TriangulationPoint((double)ci.Xa, (double)ci.Ya);
                    _triLookUp.Add(pts[n], ((ci.Depth-zMin) / (zMax - zMin)) * 255.0f );
                    n++;
                }                
                _pointSet = new PointSet(pts.ToList<TriangulationPoint>());

                for (int div = 0; div <= 20; div++)
                {
                    double cur_x = (double)_baseImage.Image.Width * ((double)div / 20);
                    double cur_y = (double)_baseImage.Image.Height * ((double)div / 20);

                    // search closest point and get it's value
                    // perhaps I rather should try such normal-vector plan like in tri-intersection
                    TriangulationPoint[] tp_set = new TriangulationPoint[4] {
                        new TriangulationPoint(cur_x, 0),
                        new TriangulationPoint(cur_x, (double)_baseImage.Image.Height),
                        new TriangulationPoint(0, cur_y),
                        new TriangulationPoint((double)_baseImage.Image.Width, cur_y)
                    };

                    TriangulationPoint[] tp_closest = new TriangulationPoint[4] {
                        new TriangulationPoint(double.MaxValue, double.MaxValue),
                        new TriangulationPoint(double.MaxValue, double.MaxValue),
                        new TriangulationPoint(double.MaxValue, double.MaxValue),
                        new TriangulationPoint(double.MaxValue, double.MaxValue)
                    };
                    foreach (TriangulationPoint tp in _triLookUp.Keys)
                    {
                        for (int tp_n = 0; tp_n < 4; tp_n++)
                        {
                            double distance1 = TriangulationPointDistance(tp_closest[tp_n], tp_set[tp_n]);
                            double distance2 = TriangulationPointDistance(tp, tp_set[tp_n]);
                            if (double.IsInfinity(distance1) || distance1 < distance2)
                            {
                                tp_closest[tp_n] = tp;
                            }
                        }
                    }

                    for (int tp_n1 = 0; tp_n1 < 4; tp_n1++)
                    {
                        _pointSet.Points.Add(tp_set[tp_n1]);
                        if (_triLookUp.ContainsKey(tp_closest[tp_n1]))
                            _triLookUp.Add(tp_set[tp_n1], _triLookUp[tp_closest[tp_n1]]);
                    }
                }
                try
                {
                    P2T.Triangulate(_pointSet );
                }
                catch (Exception e) { System.Diagnostics.Debug.WriteLine("FEHLER: " + e.Message); }

                //System.Diagnostics.Debug.WriteLine(_pointSet.Triangles.Count.ToString() + " Dreiecke, " + _pointSet.Points.Count.ToString() + " Punkte");
                /*
                try
                {
                    DelaunayTriangulation2d dt = new DelaunayTriangulation2d();
                    dt.Triangulate(ps);
                }
                catch (Exception e) { System.Diagnostics.Debug.Write("FEHLER: " + e.Message); }
                */

                Bitmap bmp = new Bitmap(_baseImage.Image.Width, _baseImage.Image.Height);
                if (simple)
                {
                    // TODO:    Kantenerkennungsdingens muss noch mit rein
                                        
                    // Draw to bitmap                
                    Graphics g = Graphics.FromImage(bmp);
                    g.FillRectangle(new SolidBrush(Color.Black), 0, 0, bmp.Width - 1f, bmp.Height - 1f);
                    foreach (CorrelationInfo ci in _correlations[_refImages.ElementAt(0)])
                    {
                        // TODO:    Sollte man evtl. nicht Abs() machen, denn der Fehler bei Fehlergebnissen verdoppelt sich so
                        //          müsst quasi geschaut werden wie die tendenzielle Richtung aufm Zahlenstrahl ist
                        float d = Math.Abs(ci.Depth); 
                        int c = (int)Math.Round(((d-zMin) / (zMax-zMin)) * 255); // TODO:    only for non-hdr
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255,c,c,c)), ci.Xa, ci.Ya, 1, 1);
                    }
                    g.Dispose();

                    // render
                    List<DelaunayTriangle> tmpTriList = _pointSet.Triangles.ToList<DelaunayTriangle>();
                    foreach (DelaunayTriangle tri in tmpTriList)
                    {
                        Vector2[] triPoints = new Vector2[3];
                        float[] triVals = new float[3];
                        for (int n1 = 0; n1 < 3; n1++)
                        {
                            triPoints[n1] = new Vector2(tri.Points[n1].Xf, tri.Points[n1].Yf);
                            if (_triLookUp.ContainsKey(tri.Points[n1]))
                                triVals[n1] = _triLookUp[tri.Points[n1]];
                            else
                                triVals[n1] = 0;
                        }

                        if (triVals[0] + triVals[1] + triVals[2] > 0 )
                        {   // draw this triangle
                            float minX = float.MaxValue;
                            float minY = float.MaxValue;
                            float maxX = float.MinValue;
                            float maxY = float.MinValue;
                            for (int pn=0; pn<3; pn++)
                            {                                
                                if (tri.Points[pn].Xf < minX)
                                    minX = tri.Points[pn].Xf;
                                if (tri.Points[pn].Xf > maxX)
                                    maxX = tri.Points[pn].Xf;
                                if (tri.Points[pn].Yf < minY)
                                    minY = tri.Points[pn].Yf;
                                if (tri.Points[pn].Yf > maxY)
                                    maxY = tri.Points[pn].Yf;
                            }

                            for (int tri_y = (int)Math.Floor(minY); tri_y <= (int)Math.Ceiling(maxY); tri_y++)
                            {
                                bool drawn = false;
                                for (int tri_x = (int)Math.Floor(minX); tri_x <= (int)Math.Ceiling(maxX); tri_x++)
                                {
                                    if (InTriangle(new Vector2(tri.Points._0.Xf, tri.Points._0.Yf),
                                               new Vector2(tri.Points._1.Xf, tri.Points._1.Yf),
                                               new Vector2(tri.Points._2.Xf, tri.Points._2.Yf),
                                               new Vector2(tri_x, tri_y)))
                                    {
                                        drawn = true;
                                        if (tri_x < bmp.Width && tri_y < bmp.Height && tri_x >= 0 && tri_y >= 0)
                                        {
                                            Color c = ColorInTriangle(new Vector2(tri_x, tri_y), triPoints, triVals);
                                            if (c.R+c.G+c.B != 0)
                                                bmp.SetPixel(tri_x, tri_y, c);
                                        }
                                    }
                                    else if (drawn)
                                    {
                                        break;
                                    }
                                }
                            }                          
                        }
                    }
                    // growing 'n stuff
                    /*
                    int blackLeft = bmp.Width*bmp.Height;
                    int [,] bmpVector = new int[bmp.Width,bmp.Height];                    
                    int r = 1;
                    List<CorrelationInfo> finished = new List<CorrelationInfo>();
                    do
                    {
                        foreach (CorrelationInfo ci in _correlations[_refImages.ElementAt(0)].FindAll(delegate (CorrelationInfo thisCI) { return !finished.Contains(thisCI);}))
                        {
                            int blackLeft_cache = blackLeft;
                            float d = Math.Abs(ci.Depth);
                            int c = (int)Math.Round(((d - zMin) / (zMax - zMin)) * 254) + 1;

                            int minOffsetX = r*-1;
                            if (minOffsetX + (int)ci.Xa < 0)
                                minOffsetX = (int)ci.Xa * -1;
                            int minOffsetY = r*-1;
                            if (minOffsetY + ci.Ya < 0)
                                minOffsetY = (int)ci.Ya * -1;

                            int maxOffsetX = r;
                            if (maxOffsetX + ci.Xa > bmp.Width)
                                maxOffsetX = bmp.Width - (int)ci.Xa;
                            int maxOffsetY = r;
                            if (maxOffsetY + ci.Ya > bmp.Height)
                                maxOffsetY = bmp.Height - (int)ci.Ya;

                            for (int offsetX = minOffsetX; offsetX < maxOffsetX; offsetX++)
                            {
                                for (int offsetY = minOffsetY; offsetY < maxOffsetY; offsetY++)
                                {
                                    if ((bmpVector[(int)ci.Xa + offsetX, (int)ci.Ya + offsetY] == 0) && Math.Sqrt(Math.Pow(offsetX, 2) + Math.Pow(offsetY, 2)) < r)
                                    {                   
                                        bmpVector[(int)ci.Xa + offsetX, (int)ci.Ya + offsetY] = c;
                                        blackLeft--;                                     
                                    }
                                }
                            }
                            if (blackLeft_cache == blackLeft)
                                finished.Add(ci);
                        }
                        r++;
                        System.Diagnostics.Debug.WriteLine(String.Format("Noch {0} Pixel", blackLeft));
                    }while(blackLeft>6000);
                    
                    for (int curX = 0; curX < bmp.Width; curX++)
                        for (int curY = 0; curY < bmp.Height; curY++)
                            bmp.SetPixel(curX,curY, Color.FromArgb(bmpVector[curX,curY],bmpVector[curX,curY],bmpVector[curX,curY]));
                    */

                    // draw tri-mesh
                    Graphics g2 = Graphics.FromImage(bmp);
                    g2.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g2.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    foreach (DelaunayTriangle tri in _pointSet.Triangles)
                    {
                        Pen p = new Pen(Color.FromArgb(1, Color.White));
                        g2.DrawLine(p, tri.Points[0].Xf, tri.Points[0].Yf, tri.Points[1].Xf, tri.Points[1].Yf);
                        g2.DrawLine(p, tri.Points[1].Xf, tri.Points[1].Yf, tri.Points[2].Xf, tri.Points[2].Yf);
                        g2.DrawLine(p, tri.Points[2].Xf, tri.Points[2].Yf, tri.Points[0].Xf, tri.Points[0].Yf);
                        g2.FillEllipse(new SolidBrush(Color.FromArgb(15, Color.Orange)), (float)((tri.Points._0.X + tri.Points._1.X + tri.Points._2.X) / 3 - 2),
                                                                      (float)((tri.Points._0.Y + tri.Points._1.Y + tri.Points._2.Y) / 3 - 2), 4, 4);
                    }
                    g2.Dispose();
                }
                else
                {   // iterativ mit allen Bildern der Range
                }
                return bmp;
            }
            else
            {
                return null;
            }
        }

        private List<CorrelationInfo> getCorrelationInfos(ExtendedImage eImg)
        {
            List<CorrelationInfo> returnList = new List<CorrelationInfo>();
            foreach (IPoint ip in eImg.InterestPointsSURF)
            {
                // das lustige Finde den richtigen KeyPoint zum KeyPoint-Spiel
                float scale_W = eImg.Image.Width / _baseImage.Image.Width;
                float scale_H = eImg.Image.Height / _baseImage.Image.Height;

                // 1. angle-guess
                List<IPoint> withCorrectAngle = _baseImage.InterestPointsSURF.FindAll(delegate(IPoint thisIPoint)
                {
                    if (thisIPoint.laplacian == ip.laplacian)
                    {
                        if (Math.Abs(thisIPoint.orientation - ip.orientation) / (2 * Math.PI) < ImageCorrelation.ACCURACY_ORIENTATION)
                        {
                            if (Math.Abs(Convert.ToInt32(5f * ip.scale) - Convert.ToInt32(5f * thisIPoint.scale)) < ImageCorrelation.ACCURACY_SCALE)
                            {
                                return true;
                            }
                            else { return false; }
                        }
                        else { return false; }
                    } 
                    else { return false; }
                });

                // 2. Translation guess
                // TODO:    wenn der DescriptorProof eingebastelt ist, müsste man hier mehrere durchlassen, 
                //          die in der Nähe sind (evtl. auch nochmal mit Winkel & PatternScale abgleichen
                float best_distance = float.MaxValue;
                IPoint best_fit = null;
                foreach (IPoint base_ip in withCorrectAngle)
                {   // catch the closest
                    Vector2 v2 = new Vector2(base_ip.x - ip.x, base_ip.y - ip.y);
                    float d = v2.Length();
                    if (d < best_distance)
                    {
                        best_distance = d;
                        best_fit = base_ip;
                    }
                }
                if (best_fit != null) 
                {
                    CorrelationInfo ci = new CorrelationInfo(best_fit.x, best_fit.y, ip.x, ip.y, _baseImage.Image.Size, eImg.Image.Size, Vector3.Right);
                    if (ci.Valid)
                    {
                        returnList.Add(ci);
                        //System.Diagnostics.Debug.WriteLine(String.Format("CP #{0}:\tx={1}\ty={2}\trot={3}", returnList.Count, ci.Xa, ci.Ya, best_fit.orientation));
                        // ich glaubs nicht da kommt ja auf anhieb was bei raus xD
                    }
                }

                // TODO:    3. rotation-normalized descriptor proof                            

            }

            // filter those dismatching the general correlation-vector
            Vector3 sumVector_pos = new Vector3(0, 0, 0);
            int cntVector_pos = 0;
            Vector3 sumVector_neg = new Vector3(0, 0, 0);
            int cntVector_neg = 0;
            returnList.ForEach((item) => {
                if (item.Joint.X > 0)
                {
                    sumVector_pos += item.Joint;
                    cntVector_pos++;
                }
                else if (item.Joint.X < 0)
                {
                    sumVector_neg += item.Joint;
                    cntVector_neg++;
                }
            });
            Vector3 jointAverage = new Vector3();
            if (cntVector_pos > cntVector_neg)
                jointAverage = new Vector3(sumVector_pos.X / cntVector_pos,sumVector_pos.Y / cntVector_pos,sumVector_pos.Z / cntVector_pos);
            else if (cntVector_neg > cntVector_pos)
                jointAverage = new Vector3(sumVector_neg.X / cntVector_neg, sumVector_neg.Y / cntVector_neg, sumVector_neg.Z / cntVector_neg);
            else
            {
                jointAverage = sumVector_pos + sumVector_neg;
                int cntTotal = cntVector_pos + cntVector_neg;
                jointAverage = new Vector3(jointAverage.X/cntTotal, jointAverage.Y/cntTotal, jointAverage.Z/cntTotal);
            }

            List<CorrelationInfo> tmpList2 = returnList.FindAll(delegate(CorrelationInfo ci)
            {
                return ( ( ci.Joint.X > 0 && jointAverage.X > 0) || (ci.Joint.X < 0 && jointAverage.X < 0) );
            });

            // filter total Peeks & Lows
            // TODO:    should depend on camera matrices
            // TODO:    implement total lows
            return tmpList2.FindAll(delegate (CorrelationInfo ci) {
                return (Math.Abs(ci.Xa - ci.Xb) < _baseImage.Image.Width * ImageCorrelation.CLAMP_TOTAL_PEEKS) && 
                       (Math.Abs(ci.Xa - ci.Xb) > _baseImage.Image.Width * ImageCorrelation.CLAMP_TOTAL_LOWS);
            });
            /*
            float sum = 0;
            float min = float.MaxValue;
            float max = float.MinValue;
            returnList.ForEach((item) => {
                sum += item.Depth;
                if (item.Depth < min)
                    min = item.Depth;
                if (item.Depth > max)
                    max = item.Depth;
            });
            float avg = sum / returnList.Count;
            float range = max - min;
            return returnList.FindAll(delegate(CorrelationInfo ci) {
                return (ci.Depth > min + (ImageCorrelation.CLAMP_TOTAL_PEEKS*(range/2))) && (ci.Depth < max - (ImageCorrelation.CLAMP_TOTAL_PEEKS*(range/2)));
            });*/

            // TODO:    filter region peeks

            return returnList;
        }

        public static float ValueInTriangle(Vector2 p, Vector2[] points, float[] values/*Origin*/)
        {
            if (points[0].Y == points[1].Y)
                points[0].Y += 0.000000000001f;
            if (points[2].Y == points[0].Y || points[2].Y == points[1].Y)
                points[2].Y -= 0.000000000001f;

            if (InTriangle(points[0], points[1], points[2], p))
            {
                #region prepare values
                /*float min = float.MaxValue;
                float max = float.MinValue;
                float[] values = new float[valuesOrigin.Length];
                foreach (float v in valuesOrigin)
                {
                    if (v < min)
                        min = v;
                    if (v > max)
                        max = v;
                }
                for (int valuesN=0; valuesN<valuesOrigin.Length; valuesN++)
                {
                    values[valuesN] = ((valuesOrigin[valuesN] - min) / (max - min)) * 255.0f;
                }*/
                #endregion

                int[] indieces = new int[3];
                #region static sort indicies
                if (points[0].Y < points[1].Y && points[0].Y < points[2].Y)
                {
                    indieces[0] = 0;
                    if (points[1].Y < points[2].Y)
                    {
                        indieces[1] = 1;
                        indieces[2] = 2;
                    }
                    else
                    {
                        indieces[1] = 2;
                        indieces[2] = 1;
                    }
                }
                else if (points[1].Y < points[0].Y && points[1].Y < points[2].Y)
                {
                    indieces[0] = 1;
                    if (points[0].Y < points[2].Y)
                    {
                        indieces[1] = 0;
                        indieces[2] = 2;
                    }
                    else
                    {
                        indieces[1] = 2;
                        indieces[2] = 0;
                    }
                }
                else if (points[2].Y < points[0].Y && points[2].Y < points[1].Y)
                {
                    indieces[0] = 2;
                    if (points[0].Y < points[1].Y)
                    {
                        indieces[1] = 0;
                        indieces[2] = 1;
                    }
                    else
                    {
                        indieces[1] = 1;
                        indieces[2] = 0;
                    }
                }
                #endregion

                float r1 = (p.Y - points[indieces[2]].Y) / (points[indieces[0]].Y - points[indieces[2]].Y); ;
                float edge1_val = ((values[indieces[0]] - values[indieces[2]]) * r1) + values[indieces[2]]; ;
                float x1 = points[indieces[2]].X + r1 * (points[indieces[0]].X - points[indieces[2]].X);
                float r2 = (p.Y - points[indieces[1]].Y) / (points[indieces[0]].Y - points[indieces[1]].Y);
                float edge2_val = ((values[indieces[0]] - values[indieces[1]]) * r2) + values[indieces[1]];
                float x2 = points[indieces[1]].X + r2 * (points[indieces[0]].X - points[indieces[1]].X);
                if (p.Y > points[indieces[1]].Y)
                {
                    r1 = (p.Y - points[indieces[1]].Y) / (points[indieces[2]].Y - points[indieces[1]].Y);
                    edge1_val = ((values[indieces[2]] - values[indieces[1]]) * r1) + values[indieces[1]];
                    x1 = points[indieces[1]].X + r1 * (points[indieces[2]].X - points[indieces[1]].X);
                    r2 = (p.Y - points[indieces[2]].Y) / (points[indieces[0]].Y - points[indieces[2]].Y);
                    edge2_val = ((values[indieces[0]] - values[indieces[2]]) * r2) + values[indieces[2]];
                    x2 = points[indieces[2]].X + r2 * (points[indieces[0]].X - points[indieces[2]].X);
                }
                return (p.X - x1) / (x2 - x1) * (edge2_val - edge1_val) + edge1_val;
            }
            else
            {
                return 0.0f;
            }
        }
        public static Color ColorInTriangle(Vector2 p, Vector2[] points, float[] values)
        {
            float v = ValueInTriangle(p, points, values);
            int c = (int)Math.Round(v);
            if (c >= 0 && c <= 255)
                return Color.FromArgb(c, c, c);
            else 
                return Color.Transparent;
        }

        public static bool InTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
        {
            float f0 = CalculateTriangleArea(A, B, C);
            float f1 = CalculateTriangleArea(A, B, P);
            float f2 = CalculateTriangleArea(A, P, C);
            float f3 = CalculateTriangleArea(P, B, C);
            if (Math.Round(f0) == Math.Round(f1 + f2 + f3))
                return true;
            else
                return false;
        }

        public static float CalculateTriangleArea(Vector2 a, Vector2 b, Vector2 c)
        {
            return Math.Abs(0.5f * (a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)));
        }

        public static double TriangulationPointDistance(TriangulationPoint p1, TriangulationPoint p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        /* props */ 

    }
}
