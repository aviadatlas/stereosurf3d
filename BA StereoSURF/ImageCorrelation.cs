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
        public const float ACCURACY_JOINT       = 0.01f;
        public const float ACCURACY_ORIENTATION = 0.05f;
        public const float ACCURACY_SCALE       = 20f; // total Pixel-difference
        public const float ACCURACY_SURF        = 0.10f;

        /* members */
        private ExtendedImage _baseImage;
        private List<ExtendedImage> _refImages;
        private Dictionary<ExtendedImage, List<CorrelationInfo>> _correlations;
        private Poly2Tri.PointSet _pointSet;

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
                // TEST: Tri
                TriangulationPoint[] pts = new TriangulationPoint[_correlations[_refImages.ElementAt(0)].Count];
                int n = 0;
                /*List<ceometric.DelaunayTriangulator.Point> ps = new List<ceometric.DelaunayTriangulator.Point>();*/
                foreach (CorrelationInfo ci in _correlations[_refImages.ElementAt(0)])
                {
                    /*ps.Add(new ceometric.DelaunayTriangulator.Point((double)ci.Xa, (double)ci.Ya, 0));*/
                    pts[n] = new TriangulationPoint((double)ci.Xa, (double)ci.Ya);
                    n++;
                }
                _pointSet = new PointSet(pts.ToList<TriangulationPoint>());
                try
                {
                    P2T.Triangulate(_pointSet );
                }
                catch (Exception e) { System.Diagnostics.Debug.WriteLine("FEHLER: " + e.Message); }

                System.Diagnostics.Debug.WriteLine(_pointSet.Triangles.Count.ToString() + " Dreiecke, " + _pointSet.Points.Count.ToString() + " Punkte");
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

                    // growing 'n stuff

                    // dieser arschlahm
                    /*
                    bool blackLeft = false;
                    do
                    {
                        blackLeft = false;
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            for (int y = 0; y < bmp.Height; y++)
                            {
                                Color c_xy = bmp.GetPixel(x, y);
                                if (c_xy == Color.FromArgb(0, 0, 0))
                                {
                                    int sum = 0;
                                    int n = 0;
                                    if (x + 1 < bmp.Width)
                                    {
                                        Color c_r = bmp.GetPixel(x + 1, y);
                                        if (c_r != Color.FromArgb(0, 0, 0))
                                        {
                                            sum += c_r.B;
                                            n++;
                                        }
                                    }
                                    if (y + 1 < bmp.Height)
                                    {
                                        Color c_d = bmp.GetPixel(x, y + 1);
                                        if (c_d != Color.FromArgb(0, 0, 0))
                                        {
                                            sum += c_d.B;
                                            n++;
                                        }
                                    }
                                    if (n > 0)
                                        bmp.SetPixel(x, y, Color.FromArgb((int)Math.Round((decimal)(sum / n)), (int)Math.Round((decimal)(sum / n)), (int)Math.Round((decimal)(sum / n))));
                                    else
                                        blackLeft = true;
                                }
                            }
                        }
                    } while (blackLeft);
                    */
                    
                    // probieren wirs mal so
                    int blackLeft = bmp.Width*bmp.Height;
                    int [,] bmpVector = new int[bmp.Width,bmp.Height];                    
                    int r = 1;
                    List<CorrelationInfo> finished = new List<CorrelationInfo>();
                    do
                    {
                        foreach (CorrelationInfo ci in _correlations[_refImages.ElementAt(0)].FindAll(delegate (CorrelationInfo thisCI) { return !finished.Contains(thisCI);}))
                        {
                            int blackLeft_cache = blackLeft;

                            //System.Diagnostics.Debug.WriteLine(String.Format("Nächster CP:\tx={0}\ty={1}", ci.Xa, ci.Ya));
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
                                        //bmp.SetPixel((int)ci.Xa + offsetX, (int)ci.Ya + offsetY, Color.FromArgb(255, c, c, c));
                                        bmpVector[(int)ci.Xa + offsetX, (int)ci.Ya + offsetY] = c;
                                        blackLeft--;
                                        //System.Diagnostics.Debug.WriteLine(String.Format("Zeichne #{0}:\tx={1}\ty={2}", c, ci.Xa + offsetX, ci.Ya + offsetY));
                                    }
                                    else
                                    {
                                        //System.Diagnostics.Debug.WriteLine(String.Format("Finde #{0}:\tx={1}\ty={2}", bmp.GetPixel((int)ci.Xa + offsetX, (int)ci.Ya + offsetY).B, ci.Xa + offsetX, ci.Ya + offsetY));
                                    }
                                }
                            }

                            if (blackLeft_cache == blackLeft)
                                finished.Add(ci);
                        }
                        r++;
                        System.Diagnostics.Debug.WriteLine(String.Format("Noch {0} Pixel", blackLeft));
                    }while(blackLeft>600000);
                    
                    for (int curX = 0; curX < bmp.Width; curX++)
                        for (int curY = 0; curY < bmp.Height; curY++)
                            bmp.SetPixel(curX,curY, Color.FromArgb(bmpVector[curX,curY],bmpVector[curX,curY],bmpVector[curX,curY]));

                    // draw tris
                    Graphics g2 = Graphics.FromImage(bmp);
                    g2.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g2.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    foreach (DelaunayTriangle tri in _pointSet.Triangles)
                    {
                        Pen p = new Pen(Color.FromArgb(11, Color.Red));
                        g2.DrawLine(p, tri.Points[0].Xf, tri.Points[0].Yf, tri.Points[1].Xf, tri.Points[1].Yf);
                        g2.DrawLine(p, tri.Points[1].Xf, tri.Points[1].Yf, tri.Points[2].Xf, tri.Points[2].Yf);
                        g2.DrawLine(p, tri.Points[2].Xf, tri.Points[2].Yf, tri.Points[0].Xf, tri.Points[0].Yf);
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
            return returnList;
        }

       /* public static List<Vector2[]> GetTriangles(List<Vector2> pts, float width, float height)
        {
            
        }*/

        public static bool InTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
        {
            Vector2 v0 = C - A;
            Vector2 v1 = B - A;
            Vector2 v2 = P - A;
            float[,] p = new float[2, 3];
            p[0, 0] = Vector2.Dot(v0, v0);
            p[0, 1] = Vector2.Dot(v0, v1);
            p[0, 2] = Vector2.Dot(v0, v2);
            p[1, 0] = Vector2.Dot(v1, v1);
            p[1, 1] = Vector2.Dot(v1, v2);
            float invD = 1 / (p[0,0]*p[1,1] - p[0,1]*p[0,1]);
            float u = (p[1, 1] * p[0, 2] - p[0, 1] * p[1, 2]) * invD;
            float v = (p[0, 0] * p[1, 2] - p[0, 1] * p[0, 2]) * invD;
            return (u > 0) && (v > 0) && (u + v < 1);
        }

        /* props */ 

    }
}
