using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Poly2Tri;

namespace TriangleFillTest
{
    public enum TestMode { RANDOM, DOUBLETRI, COLUMN };

    public partial class Form2 : Form
    {
        private const int POINTS = 30;
        private const TestMode TESTMODE = TestMode.DOUBLETRI;

        private Poly2Tri.PointSet _pointSet;
        private Dictionary<TriangulationPoint, float> _triLookUp;

        TriangulationPoint _currentPoint = null;
        private bool _repaintMesh = true;

        private List<Vector2> _intersections;

        public Form2()
        {
            InitializeComponent();

            reinit();
        }

        private void reinit()
        {
            _triLookUp = new Dictionary<TriangulationPoint, float>();
            _pointSet = new PointSet(new List<TriangulationPoint>());
            _intersections = new List<Vector2>();
            Random rand = new Random();

            if (TESTMODE == TestMode.RANDOM)
            {
                TriangulationPoint[] pts = new TriangulationPoint[POINTS];                
                for (int n = 0; n < POINTS; n++)
                {
                    pts[n] = new TriangulationPoint((double)rand.Next(pictureBox1.Width), (double)rand.Next(pictureBox1.Height));
                    _triLookUp.Add(pts[n], (float)(rand.Next(255000) / 1000f));
                }
                _pointSet = new PointSet(pts.ToList<TriangulationPoint>());                
            }
            else if (TESTMODE == TestMode.DOUBLETRI)
            {
                TriangulationPoint[] pts = new TriangulationPoint[5];
                pts[0] = new TriangulationPoint(60, 120);
                pts[1] = new TriangulationPoint(53, 520);
                pts[2] = new TriangulationPoint(365, 220);
                pts[3] = new TriangulationPoint(620, 70);
                pts[4] = new TriangulationPoint(730, 410);
                foreach (TriangulationPoint tp in pts)
                    _triLookUp.Add(tp, (float)(rand.Next(255000) / 1000f));
                _pointSet = new PointSet(pts.ToList<TriangulationPoint>()); 
            }


            try
            {
                P2T.Triangulate(_pointSet);
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine("FEHLER: " + e.Message); }

            _repaintMesh = true;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp;
            if (pictureBox1.Image == null)
            {
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = bmp;
                pictureBox1.Invalidate();
                return;
            }
            else
            {
                bmp = (Bitmap)pictureBox1.Image;
            }

            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;            

            if (_repaintMesh)
            {
                g.Clear(Color.Black);

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

                    if (triVals[0] + triVals[1] + triVals[2] > 0)
                    {   // draw this triangle
                        float minX = float.MaxValue;
                        float minY = float.MaxValue;
                        float maxX = float.MinValue;
                        float maxY = float.MinValue;
                        for (int pn = 0; pn < 3; pn++)
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
                                if (MahdiHelper.InTriangle(new Vector2(tri.Points._0.Xf, tri.Points._0.Yf),
                                           new Vector2(tri.Points._1.Xf, tri.Points._1.Yf),
                                           new Vector2(tri.Points._2.Xf, tri.Points._2.Yf),
                                           new Vector2(tri_x, tri_y)))
                                {
                                    drawn = true;
                                    if (tri_x < bmp.Width && tri_y < bmp.Height && tri_x >= 0 && tri_y >= 0)
                                    {
                                        Color c = MahdiHelper.ColorInTriangle(new Vector2((float)tri_x, (float)tri_y), triPoints, triVals);
                                        if (c.R + c.G + c.B != 0)
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

                foreach (DelaunayTriangle tri in _pointSet.Triangles)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        int nAdd1 = n + 1;
                        if (nAdd1 > 2)
                            nAdd1 = 0;
                        g.DrawLine(new Pen(Color.FromArgb(31, Color.LimeGreen)), tri.Points[n].Xf, tri.Points[n].Yf, tri.Points[nAdd1].Xf, tri.Points[nAdd1].Yf);
                    }
                }

                _repaintMesh = false;
            }

            foreach (TriangulationPoint tp in _pointSet.Points)
            {
                if (_currentPoint != null && _currentPoint == tp)
                {
                    g.FillEllipse(new SolidBrush(Color.Red), tp.Xf - 1, tp.Yf - 1, 3, 3);
                    g.DrawEllipse(new Pen(Color.LimeGreen), tp.Xf - 2, tp.Yf - 2, 5, 5);
                }
                else
                {
                    g.FillEllipse(new SolidBrush(Color.Yellow), tp.Xf - 1, tp.Yf - 1, 3, 3);
                    g.DrawEllipse(new Pen(Color.LimeGreen), tp.Xf - 2, tp.Yf - 2, 5, 5);
                }
            }
            
            _intersections.ForEach((item) =>
            {
                g.FillEllipse(new SolidBrush(Color.Orange), item.X - 1, item.Y - 1, 3, 3);
            });
            _intersections.Sort(delegate(Vector2 v1, Vector2 v2) { if (v1.Y < v2.Y) { return 1; } else if (v1.Y > v2.Y) { return -1; } else { return 0; } });
            for (int n = 0; n < _intersections.Count; n++)
            {
                if (n + 1 < _intersections.Count)
                {
                    Color c = Color.FromArgb(80, Color.Blue);
                    
                    bool intersects = false;
                    foreach (DelaunayTriangle dTri in _pointSet.Triangles)
                    {
                        Vector2[] tri = new Vector2[3] {    new Vector2(dTri.Points[0].Xf, dTri.Points[0].Yf),
                                                            new Vector2(dTri.Points[1].Xf, dTri.Points[1].Yf),
                                                            new Vector2(dTri.Points[2].Xf, dTri.Points[2].Yf)};

                        for (int i = 0; i < 3; i++)
                        {
                            int i2 = i + 1;
                            if (i2 > 2)
                                i2 = 0;

                            Vector2 isP = new Vector2();
                            if (MahdiHelper.DoLinesIntersect(MahdiHelper.Line.V2L(tri[i], tri[i2]),
                                                                MahdiHelper.Line.V2L(_intersections[n], _intersections[n + 1]), ref isP))
                            {
                                g.FillEllipse(new SolidBrush(Color.Red), (int)isP.X - 3, (int)isP.Y - 3, 7, 7);
                                intersects = true;
                            }
                        }
                    }
                    if (intersects)
                        c = Color.FromArgb(80, Color.Green);
                    g.DrawLine(new Pen(c, 2), new System.Drawing.Point((int)_intersections[n].X, (int)_intersections[n].Y), new System.Drawing.Point((int)_intersections[n + 1].X, (int)_intersections[n + 1].Y));

                }
                g.FillEllipse(new SolidBrush(Color.Green), _intersections[n].X - 2, _intersections[n].Y - 2, 5, 5);
            }
            g.Dispose();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            for (int n = 0; n < _triLookUp.Keys.Count; n++)
            {
                if (_currentPoint == null)
                    _currentPoint = _triLookUp.Keys.ElementAt(n);

                Vector2 v1 = new Vector2(_currentPoint.Xf, _currentPoint.Yf);
                Vector2 v2 = new Vector2(_triLookUp.Keys.ElementAt(n).Xf, _triLookUp.Keys.ElementAt(n).Yf);
                Vector2 p = new Vector2((float)e.X, (float)e.Y);
                if ( Vector2.Distance(v2, p) < Vector2.Distance(v1,p) )
                {
                    _currentPoint = _triLookUp.Keys.ElementAt(n);
                }
            }

            label1.Text = String.Format("Punkt\n{0}, {1}", _currentPoint.Xf, _currentPoint.Yf);
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reinit();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            _intersections.Add(new Vector2((float)e.X, (float)e.Y));
        }

        private static void TriangulateByIntersection(List<MahdiHelper.Line> intersectingLines, ref PointSet pointSet)
        {

        }
    }
}
