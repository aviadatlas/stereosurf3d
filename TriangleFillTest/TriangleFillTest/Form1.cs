using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;

namespace TriangleFillTest
{
    public partial class Form1 : Form
    {
        public const float SCHWELLWERT = 0.1f;
        public const float LUMPING_SLICE = 0.1f;    // percentage of an edge-length, where intersections will be ignored

        float[] values = new float[3];
        Vector2[] points = new Vector2[3];
        List<Vector2> intersections;
        int set = -1;
        List<List<Vector2>> _points;
        List<List<Vector2[]>> _triangles;

        public Form1()
        {
            InitializeComponent();
            
            values[0] = 240.8f;
            values[1] = 34.4f;
            values[2] = 160.9f;            
            points[2] = new Vector2(250, 50);
            points[1] = new Vector2(10, 350);
            points[0] = new Vector2(410, 380);

            intersections = new List<Vector2>();

            _points = new List<List<Vector2>>();
            _triangles = new List<List<Vector2[]>>();
            _points.Add(new List<Vector2>(points));
            List<Vector2[]> triLevel0 = new List<Vector2[]>();
            triLevel0.Add(points);
            _triangles.Add(triLevel0);

            float pointsMinX = MahdiHelper.GetMin(new float[] { points[0].X, points[1].X, points[2].X });
            float pointsMinY = MahdiHelper.GetMin(new float[] { points[0].Y, points[1].Y, points[2].Y });
            float pointsMaxX = MahdiHelper.GetMax(new float[] { points[0].X, points[1].X, points[2].X });
            float pointsMaxY = MahdiHelper.GetMax(new float[] { points[0].Y, points[1].Y, points[2].Y });

            Random rand = new Random();
            float s1_x = /*(float)rand.Next((int)pointsMaxX - (int)pointsMinX) + (pointsMinX-30f)*/ 200f;
            for (int n = 0; n <= 6; n++)
            {
                Vector2 v1 = new Vector2(((float)rand.Next(80) - 40) + s1_x, ((pointsMaxY - pointsMinY) / 6) * (n+1));
                intersections.Add(v1);
            }
            drawIt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // divide & conquer
            intersections.Sort(delegate(Vector2 v1, Vector2 v2) { if (v1.Y < v2.Y) { return 1; } else if (v1.Y > v2.Y) { return -1; } else { return 0; } });

            List<Vector2[]> triNewLevel = new List<Vector2[]>();
            List<Vector2> ptsNewLevel = new List<Vector2>();
            foreach (Vector2[] tri in _triangles[(int)numericUpDown1.Value])
            {
                ptsNewLevel.Add(tri[0]);
                ptsNewLevel.Add(tri[1]);
                ptsNewLevel.Add(tri[2]);
                bool finish = false;
                uint[] intersects = new uint[] { 0, 0, 0 };
                Vector2[] isPts = new Vector2[3] {new Vector2(), new Vector2(), new Vector2()};                        
                MahdiHelper.Line[] edges = new MahdiHelper.Line[3] {MahdiHelper.Line.V2L(tri[0], tri[1]),
                                                                            MahdiHelper.Line.V2L(tri[1], tri[2]),
                                                                            MahdiHelper.Line.V2L(tri[0], tri[2])};
                for (int n = 0; n < intersections.Count; n++)
                {
                    if (n + 1 < intersections.Count)
                    {                        
                        for (int init_n = 0; init_n < 3; init_n++)
                        {                            
                            if (MahdiHelper.DoLinesIntersect(edges[init_n], MahdiHelper.Line.V2L(intersections[n], intersections[n + 1]), ref isPts[init_n]))
                                intersects[init_n] = 1;                            
                        }
                    }
                }

                if (intersects[0] + intersects[1] + intersects[2] > 1)
                {
                    for (int pN1 = 0; pN1 < 3; pN1++)
                    {
                        int pN2 = pN1 + 1;
                        if (pN2 > 2)
                            pN2 = pN2 - 3;
                        int pN3 = pN1 + 2;
                        if (pN3 > 2)
                            pN3 = pN3 - 3;

                        if (intersects[pN1] + intersects[pN2] == 2)
                        {
                            // 1. Mittelpunkt der Sekante bestimmen
                            Vector2 cutMid = (isPts[pN1] + isPts[pN2]) / 2;
                            // 2. Orthogonalen Vektor aufspannen
                            Vector2 ortho = new Vector2(-(isPts[pN2].Y - isPts[pN1].Y), isPts[pN2].X - isPts[pN1].X);
                            MahdiHelper.Line otrhoLine = MahdiHelper.Line.V2L(cutMid, cutMid + ortho);
                            // 3. Prüfen ob Schnittpunkt von Mittelpunkt und Orthogonalen Vektor auf gegenüberliegender Kante ist                                                                
                            Vector2 S = new Vector2();
                            if (MahdiHelper.DoLinesIntersect(edges[pN3], otrhoLine, ref S))
                            {   // Gegenkante wird geschnitten
                                // S,isP01,isP12    
                                // S,tri[0],isP01
                                // S,tri[2],isP12
                                ptsNewLevel.Add(S);
                                triNewLevel.Add(new Vector2[3] { S, isPts[pN1], isPts[pN2] });
                                triNewLevel.Add(new Vector2[3] { S, tri[pN1], isPts[pN1] });
                                triNewLevel.Add(new Vector2[3] { S, tri[pN3], isPts[pN2] });
                            }
                            else if (MahdiHelper.DoLinesIntersect(edges[pN1], otrhoLine, ref S))
                            {   // tri[0],isP12,isP01
                                // tri[0],tri[2],isP12
                                triNewLevel.Add(new Vector2[3] { tri[pN1], isPts[pN2], isPts[pN1] });
                                triNewLevel.Add(new Vector2[3] { tri[pN1], tri[pN3], isPts[pN2] });
                            }
                            else if (MahdiHelper.DoLinesIntersect(edges[pN2], otrhoLine, ref S))
                            {   // tri[2],isP12,isP01
                                // tri[2],tri[0],isP01
                                triNewLevel.Add(new Vector2[3] { tri[pN3], isPts[pN1], isPts[pN2] });
                                triNewLevel.Add(new Vector2[3] { tri[pN3], tri[pN1], isPts[pN1] });
                            }
                            ptsNewLevel.Add(isPts[pN1]);
                            ptsNewLevel.Add(isPts[pN2]);
                            //ptsNewLevel.Add(tri[pN3]);

                            // doppeleinträge
                            Vector2 isPts1 = new Vector2(isPts[pN1].X, isPts[pN1].Y);
                            Vector2 isPts2 = new Vector2(isPts[pN2].X, isPts[pN2].Y);
                            ptsNewLevel.Add(isPts1);
                            ptsNewLevel.Add(isPts2);
                            triNewLevel.Add(new Vector2[3] { tri[pN2], isPts1, isPts2 });

                        }
                    }/*
                    if (intersects[0] + intersects[1] == 2)
                    { // intersects pt0-pt1 & pt1-pt2 => cut(pt2-pt0)
                        // 1. Mittelpunkt der Sekante bestimmen
                        Vector2 cutMid = (isP01 + isP12) / 2;
                        // 2. Orthogonalen Vektor aufspannen
                        Vector2 ortho = new Vector2( -(isP12.Y - isP01.Y), isP12.X - isP01.X);
                        MahdiHelper.Line otrhoLine = MahdiHelper.Line.V2L(cutMid, cutMid + ortho);
                        // 3. Prüfen ob Schnittpunkt von Mittelpunkt und Orthogonalen Vektor auf gegenüberliegender Kante ist                                                                
                        Vector2 S = new Vector2();
                        if (MahdiHelper.DoLinesIntersect(edge02, otrhoLine, ref S))
                        {   // Gegenkante wird geschnitten
                            // S,isP01,isP12    
                            // S,tri[0],isP01
                            // S,tri[2],isP12
                            ptsNewLevel.Add(S);
                            ptsNewLevel.Add(isP01);
                            ptsNewLevel.Add(isP12);
                            triNewLevel.Add(new Vector2[3] { S, isP01, isP12 });
                            triNewLevel.Add(new Vector2[3] { S, tri[0], isP01 });
                            triNewLevel.Add(new Vector2[3] { S, tri[2], isP12 });
                        }
                        else if (MahdiHelper.DoLinesIntersect(edge01, otrhoLine, ref S))
                        {   // tri[0],isP12,isP01
                            // tri[0],tri[2],isP12
                            ptsNewLevel.Add(isP01);
                            ptsNewLevel.Add(isP12);
                            triNewLevel.Add(new Vector2[3] { tri[0], isP12, isP01 });
                            triNewLevel.Add(new Vector2[3] { tri[0], tri[2], isP12 });
                        }
                        else if (MahdiHelper.DoLinesIntersect(edge12, otrhoLine, ref S))
                        {   // tri[2],isP12,isP01
                            // tri[2],tri[0],isP01
                            ptsNewLevel.Add(isP01);
                            ptsNewLevel.Add(isP12);
                            triNewLevel.Add(new Vector2[3] { tri[2], isP01, isP12 });
                            triNewLevel.Add(new Vector2[3] { tri[2], tri[0], isP01 });
                        }
                    }*/
                }
                else
                {   // no intersections
                    triNewLevel.Add(tri);
                }
            }

            _triangles.Add(triNewLevel);
            _points.Add(ptsNewLevel);

            numericUpDown1.Maximum += 1;
            numericUpDown1.Value++;
                                     
            
            //draw2nd();
        }

        private void drawIt()
        {
            Bitmap bmp = new Bitmap(444, 401);

            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            g.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(values[0]), (int)Math.Round(values[0]), (int)Math.Round(values[0]))), points[0].X - 20, points[0].Y - 20, 40, 40);
            g.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(values[1]), (int)Math.Round(values[1]), (int)Math.Round(values[1]))), points[1].X - 20, points[1].Y - 20, 40, 40);
            g.FillEllipse(new SolidBrush(Color.FromArgb((int)Math.Round(values[2]), (int)Math.Round(values[2]), (int)Math.Round(values[2]))), points[2].X - 20, points[2].Y - 20, 40, 40);

            /*float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;
            foreach (Vector2 p in points)
            {
                if (p.X < minX)
                    minX = p.X;
                else if (p.X > maxX)
                    maxX = p.X;
                if (p.Y < minY)
                    minY = p.Y;
                else if (p.Y > maxY)
                    maxY = p.Y;
            }

            for (int y = (int)Math.Floor(minY); y < (int)Math.Ceiling(maxY); y++)
            {
                for (int x = (int)Math.Floor(minX); x < (int)Math.Ceiling(maxX); x++)
                {
                    float v = ValueInTriangle(new Vector2(x, y), points, values);
                    if (v > 0 && v <= 255)
                        bmp.SetPixel(x, y, Color.FromArgb((int)Math.Round(v), (int)Math.Round(v), (int)Math.Round(v)));
                }
            }*/

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    float v = MahdiHelper.ValueInTriangle(new Vector2(x, y), points, values);
                    if (v > 0 && v <= 255)
                        bmp.SetPixel(x, y, Color.FromArgb((int)Math.Round(v), (int)Math.Round(v), (int)Math.Round(v)));
                }
            }            


            g.DrawPolygon(new Pen(Color.Green), new System.Drawing.Point[] 
                                                    { 
                                                        new System.Drawing.Point((int)Math.Round(points[0].X), (int)Math.Round(points[0].Y)), 
                                                        new System.Drawing.Point((int)Math.Round(points[1].X), (int)Math.Round(points[1].Y)),
                                                        new System.Drawing.Point((int)Math.Round(points[2].X), (int)Math.Round(points[2].Y))});
            // draw intersection points
            intersections.ForEach((item) => {
                g.FillEllipse(new SolidBrush(Color.Black), item.X - 1, item.Y - 1, 3, 3);
            });
            intersections.Sort(delegate(Vector2 v1, Vector2 v2) { if (v1.Y < v2.Y) { return 1; } else if (v1.Y > v2.Y) { return -1; } else { return 0; } });
            List<Vector2> sekanten = new List<Vector2>();
            for (int n = 0; n < intersections.Count; n++)
            {
                if (n + 1 < intersections.Count)
                {
                    Color c = Color.FromArgb(80, Color.Blue);

                    bool intersects = false;
                    for (int i = 0; i < 3; i++)
                    {
                        int i2 = i + 1;
                        if (i2 > 2)
                            i2 = 0;

                        Vector2 isP = new Vector2();
                        if (MahdiHelper.DoLinesIntersect(MahdiHelper.Line.V2L(points[i], points[i2]),
                                                            MahdiHelper.Line.V2L(intersections[n], intersections[n + 1]), ref isP))
                        {
                            g.FillEllipse(new SolidBrush(Color.Red), (int)isP.X - 3, (int)isP.Y - 3, 7, 7);
                            intersects = true;
                        }
                    }
                    if (intersects)
                        c = Color.FromArgb(80, Color.Green);
                    g.DrawLine(new Pen(c, 2), new System.Drawing.Point((int)intersections[n].X, (int)intersections[n].Y), new System.Drawing.Point((int)intersections[n + 1].X, (int)intersections[n + 1].Y));
                }

                g.FillEllipse(new SolidBrush(Color.Black), intersections[n].X - 2, intersections[n].Y - 2, 5, 5);
            }

            g.Dispose();

            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
        }

        private void draw2nd()
        {
            Bitmap bmp = new Bitmap(444, 401);

            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            Pen triPen = new Pen(Color.FromArgb(80, Color.Black));
            foreach (Vector2[] pts in _triangles[(int)numericUpDown1.Value])
            {
                if (pts.Length > 2)
                {
                    g.DrawLine(triPen, pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
                    g.DrawLine(triPen, pts[2].X, pts[2].Y, pts[1].X, pts[1].Y);
                    g.DrawLine(triPen, pts[0].X, pts[0].Y, pts[2].X, pts[2].Y);
                }
            }

            g.Dispose();
            
            pictureBox2.Image = bmp;
            pictureBox2.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (MahdiHelper.InTriangle(points[1], points[0], points[2], new Vector2(e.X, e.Y)))
                label1.BackColor = Color.Yellow;
            else
                label1.BackColor = Color.White;

            float v = MahdiHelper.ValueInTriangle(new Vector2(e.X, e.Y), points, values);
            
            label1.Text = String.Format("x:{0}\ny:{1}\nv:{2}", e.X, e.Y, v);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "Setze A";
            set = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "Setze B";
            set = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "Setze C";
            set = 2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (set >= 0)
            {
                points[set] = new Vector2(e.X, e.Y);
                //set = -1;
                label2.Text = "";
                drawIt();
                
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = String.Format("{0} tris\n{1} pts", _triangles[(int)numericUpDown1.Value].Count, _points[(int)numericUpDown1.Value].Count);
            draw2nd();
        }
    }
}
