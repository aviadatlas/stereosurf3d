﻿using System;
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
    }

    public static bool InTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
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

    public static float CalculateTriangleArea(Vector2 a, Vector2 b, Vector2 c)
    {
        return Math.Abs(0.5f * (a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)));
    }

    public static float ValueInTriangle(Vector2 p, Vector2[] points, float[] values)
    {
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
            //System.Diagnostics.Debug.WriteLine(String.Format("p1:{0}\tp2:{1}\tp3:{2}", indieces[0], indieces[1], indieces[2]));
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
}
