using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DelaunayTriangulationBySweepingLineMethod
{
    struct Vector
    {
        public readonly double X;
        public readonly double Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector(PointF startOfVector, PointF endOfVector)
        {
            X = endOfVector.X - startOfVector.X;
            Y = endOfVector.Y - startOfVector.Y;
        }

        public static double CalculatePseudoscalarComposition(PointF startVector1, PointF endVector1,
            PointF startVector2, PointF endVector2)
        {
            var vector1 = new Vector(endVector1, startVector1);
            var vector2 = new Vector(endVector2, startVector2);
            return vector1.X * vector2.Y - vector1.Y * vector2.X;
        }

        public double CalculatePseudoscalarComposition(Vector otherV)
        {
            return X * otherV.Y - otherV.X * Y;
        }
    }
}
