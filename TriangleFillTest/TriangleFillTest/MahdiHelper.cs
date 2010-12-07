using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

static class MahdiHelper
{
    public struct Line
    {
        public double X1;
        public double X2;
        public double Y1;
        public double Y2;

        private bool _recalcedBB;
        private BoundingBox2D _bb;

        private void recalcBB()
        {
            _bb = new BoundingBox2D();
            _bb.AddPoint(new Vector2((float)X1,(float)Y1));
            _bb.AddPoint(new Vector2((float)X2,(float)Y2));
            _recalcedBB = true;
        }

        public static Line V2L(Vector2 v1, Vector2 v2)
        {
            Line l = new Line();
            l.X1 = (double)v1.X;
            l.X2 = (double)v2.X;
            l.Y1 = (double)v1.Y;
            l.Y2 = (double)v2.Y;            
            return l;
        }

        public MahdiHelper.BoundingBox2D BoundingBox2D
        {
            get { if (!_recalcedBB) { recalcBB(); } return _bb; }
        }
        public Vector2 V1
        {
            get { Vector2 v = new Vector2((float)X1, (float)Y1); return v; }
            set { X1 = value.X; Y1 = value.Y; _recalcedBB = false; }
        }
        public Vector2 V2
        {
            get { Vector2 v = new Vector2((float)X2, (float)Y2); return v; }
            set { X2 = value.X; Y2 = value.Y; _recalcedBB = false; }
        }
        public float Length
        {
            get { return Vector2.Distance(this.V1, this.V2); }
        }

        public MahdiHelper.Line Multiply(float faktor)
        {
            Vector2 joint = new Vector2((float)(X2 - X1), (float)(Y2 - Y1));
            this.V1 -= faktor * joint;
            this.V2 += faktor * joint;
            _recalcedBB = false;
            return this;
        }
    }

    public struct BoundingBox2D
    {
        private double _minX;
        private double _minY;
        private double _maxX;
        private double _maxY;

        private bool _calced;
        private List<Vector2> _points;

        public double Area { get { return (this.MaxX - this.MinX) * (this.MaxY - this.MinY); } }
        public List<Vector2> Points { get { return _points; } }
        public double MinX { get { if (!_calced) { recalc(); } return _minX; } }
        public double MinY { get { if (!_calced) { recalc(); } return _minY; } }
        public double MaxX { get { if (!_calced) { recalc(); } return _maxX; } }
        public double MaxY { get { if (!_calced) { recalc(); } return _maxY; } }

        public void AddPoint(Vector2 pt)
        {
            if (_points == null)
                _points = new List<Vector2>();
            _calced = false;
            _points.Add(pt);
        }
        public void AddPoints(List<Vector2> pts)
        {
            if (_points == null)
                _points = new List<Vector2>();
            _calced = false;
            _points.AddRange(pts);
        }
        public void Clear()
        {
            _points = new List<Vector2>();
            _calced = false;
            _minX = double.MaxValue;
            _minY = double.MaxValue;
            _maxX = double.MinValue;
            _maxY = double.MinValue;
        }

        public bool IntersectsBoundingBox(MahdiHelper.BoundingBox2D bb)
        {
            foreach (Vector2 pt in bb.Points)
            {
                if (pt.X >= this.MinX && pt.Y >= this.MinY
                    && pt.X <= this.MaxX && pt.Y <= this.MaxY)
                    return true;
            }
            return false;
        }

        private void recalc()
        {
            _minX = double.MaxValue;
            _minY = double.MaxValue;
            _maxX = double.MinValue;
            _maxY = double.MinValue;
            foreach (Vector2 pt in _points)
            {
                if (pt.X < _minX)
                    _minX = pt.X;
                if (pt.Y < _minY)
                    _minY = pt.Y;
                if (pt.X > _maxX)
                    _maxX = pt.X;
                if (pt.Y > _maxY)
                    _maxY = pt.Y;
            }
            _calced = true;
        }
            // nadler:iabmdr1
            // hidden & dangerous
    }

    public static bool InTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
    {
        float minX = float.MaxValue; 
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        Vector2[] v = new Vector2[3] {A,B,C};
        for (int n=0; n<3; n++)
        {
            if ( v[n].X < minX)
                minX = v[n].X;
            if ( v[n].Y < minY)
                minY = v[n].Y;
            if ( v[n].X > maxX)
                maxX = v[n].X;
            if ( v[n].Y > maxY)
                maxY = v[n].Y;
        }

        if (P.X >= minX && P.X <= maxX && P.Y >= minY && P.Y <= maxY)
        {
            float f0 = CalculateTriangleArea(A, B, C);
            float f1 = CalculateTriangleArea(A, B, P);
            float f2 = CalculateTriangleArea(A, P, C);
            float f3 = CalculateTriangleArea(P, B, C);
            if (f0 == (f1 + f2 + f3))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public static float CalculateTriangleArea(Vector2 a, Vector2 b, Vector2 c)
    {
        return Math.Abs(0.5f * (a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)));
    }

    public static Vector2 GetJoint(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v2.X - v1.X, v2.Y - v1.Y);
    }

    public static float ValueInTriangle(Vector2 p, Vector2[] points, float[] values)
    {
        if (points[0].Y == points[1].Y)
        {
            if (points[2].Y < points[0].Y)
                points[0].Y++;
            else
                points[0].Y--;

            if (points[0].X < points[1].X)
            {
                points[0].X--;
                points[1].X++;
            }
            else
            {
                points[0].X++;
                points[1].X--;
            }

        }
        if (points[2].Y == points[1].Y)
        {
            if (points[0].Y < points[2].Y)
                points[2].Y++;
            else
                points[2].Y--;
        }
        if (points[2].Y == points[0].Y)
        {
            if (points[1].Y < points[2].Y)
                points[2].Y++;
            else
                points[2].Y--;
        }

        if (InTriangle(points[0], points[1], points[2], p))
        {
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
            
            if (float.IsNaN(r1) || float.IsNaN(r2))
                System.Diagnostics.Debug.WriteLine(String.Format("p:{6},{7}\tp1:{0},{1}\tp2:{2},{3}\tp3:{4},{5}", points[0].X,points[0].Y,points[1].X,points[1].Y,points[2].X,points[2].Y,p.X,p.Y));

            return (p.X - x1) / (x2 - x1) * (edge2_val - edge1_val) + edge1_val;
        }
        else
        {
            return 0.0f;
        }
    }

    /// <summary>
    /// This is based off an explanation and expanded math presented by Paul Bourke:
    /// It takes two lines as inputs and returns true if they intersect, false if they 
    /// don't. If they do, ptIntersection returns the point where the two lines intersect.  
    /// </summary>
    /// <param name="L1">The first line</param>
    /// <param name="L2">The second line</param>
    /// <param name="ptIntersection">The point where both lines intersect (if they do).</param>
    /// <returns></returns>
    /// <remarks>See http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/</remarks>
    public static bool DoLinesIntersect(Line L1, Line L2, ref Vector2 ptIntersection)
    {
        return DoLinesIntersect(L1, L2, ref ptIntersection, false);
    }
    public static bool DoLinesIntersect(Line L1, Line L2, ref Vector2 ptIntersection, bool infiniteLines)
    {
        // Denominator for ua and ub are the same, so store this calculation
        double d =
           (L2.Y2 - L2.Y1) * (L1.X2 - L1.X1)
           -
           (L2.X2 - L2.X1) * (L1.Y2 - L1.Y1);

        //n_a and n_b are calculated as seperate values for readability
        double n_a =
           (L2.X2 - L2.X1) * (L1.Y1 - L2.Y1)
           -
           (L2.Y2 - L2.Y1) * (L1.X1 - L2.X1);

        double n_b =
           (L1.X2 - L1.X1) * (L1.Y1 - L2.Y1)
           -
           (L1.Y2 - L1.Y1) * (L1.X1 - L2.X1);

        // Make sure there is not a division by zero - this also indicates that
        // the lines are parallel.  
        // If n_a and n_b were both equal to zero the lines would be on top of each 
        // other (coincidental).  This check is not done because it is not 
        // necessary for this implementation (the parallel check accounts for this).
        if (d == 0)
            return false;

        // Calculate the intermediate fractional point that the lines potentially intersect.
        double ua = n_a / d;
        double ub = n_b / d;

        // The fractional point will be between 0 and 1 inclusive if the lines
        // intersect.  If the fractional calculation is larger than 1 or smaller
        // than 0 the lines would need to be longer to intersect.
        if (ua >= 0d && ua <= 1d && ub >= 0d && ub <= 1d)
        {
            ptIntersection.X = (float)(L1.X1 + (ua * (L1.X2 - L1.X1)));
            ptIntersection.Y = (float)(L1.Y1 + (ua * (L1.Y2 - L1.Y1)));
            return true;
        }
        else if (infiniteLines)
        {
            ptIntersection = Vector2.Zero;
            return true;
        }

        return false;
    }

    public static bool OnLine(Vector2 x, Line line)
    {
        Vector2 xp1 = Vector2.Subtract(line.V1, x);        
        Vector2 xp2 = Vector2.Subtract(line.V2, x);

        if (Math.Round((double)line.Length, 2, MidpointRounding.AwayFromZero) == Math.Round((double)xp1.Length() + (double)xp2.Length(), 2, MidpointRounding.AwayFromZero))
            return true;
        else
            return false;
        
        /*Vector2 p2p1 = Vector2.Subtract(line.V1, line.V2);

        float len_xp1 = xp1.Length();
        float len_xp2 = xp2.Length();
        float len_pp = p2p1.Length();

        xp1.Normalize();
        p2p1.Normalize();

        if (    (xp1.X == p2p1.X && xp1.Y == p2p1.Y)
             || (xp1.X == -p2p1.X && xp1.Y == -p2p1.Y))
        {
            if (len_pp == len_xp1 + len_xp2)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }*/
    }

    public static int GetMax(int[] range)
    {
        int max = int.MinValue;
        foreach (int v in range)
            if (v > max)
                max = v;
        return max;
    }
    public static float GetMax(float[] range)
    {
        float max = float.MinValue;
        foreach (float v in range)
            if (v > max)
                max = v;
        return max;
    }
    public static double GetMax(double[] range)
    {
        double max = double.MinValue;
        foreach (double v in range)
            if (v > max)
                max = v;
        return max;
    }
    public static int GetMin(int[] range)
    {
        int min = int.MaxValue;
        foreach (int v in range)
            if (v < min)
                min = v;
        return min;
    }
    public static float GetMin(float[] range)
    {
        float min = float.MaxValue;
        foreach (float v in range)
            if (v < min)
                min = v;
        return min;
    }
    public static double GetMin(double[] range)
    {
        double min = double.MaxValue;
        foreach (double v in range)
            if (v < min)
                min = v;
        return min;
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

    public static string Vector2ToString(Vector2 v)
    {
        return String.Format("({0},{1}", v.X, v.Y);
    }
}
