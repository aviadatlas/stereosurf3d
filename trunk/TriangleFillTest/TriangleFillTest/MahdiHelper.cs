using System;
using System.Drawing;
using Microsoft.Xna.Framework;

static class MahdiHelper
{
    public struct Line
    {
        public double X1;
        public double X2;
        public double Y1;
        public double Y2;

        public static Line V2L(Vector2 v1, Vector2 v2)
        {
            Line l = new Line();
            l.X1 = (double)v1.X;
            l.X2 = (double)v2.X;
            l.Y1 = (double)v1.Y;
            l.Y2 = (double)v2.Y;
            return l;
        }

        public Vector2 V1
        {
            get { Vector2 v = new Vector2((float)X1, (float)Y1); return v; }
            set { X1 = value.X; Y1 = value.Y; }
        }
        public Vector2 V2
        {
            get { Vector2 v = new Vector2((float)X2, (float)Y2); return v; }
            set { X2 = value.X; Y2 = value.Y; }
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
            return this;
        }
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
        return false;
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
}
