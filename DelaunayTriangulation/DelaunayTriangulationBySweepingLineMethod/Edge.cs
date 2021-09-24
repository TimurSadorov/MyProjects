using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DelaunayTriangulationBySweepingLineMethod
{
    public class Edge
    {
        public readonly PointF Vertex1;
        public readonly PointF Vertex2;

        public Edge(PointF vertex1, PointF vertex2)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
        }

        public override int GetHashCode()
        {
            return Vertex1.GetHashCode() * 43 + Vertex2.GetHashCode() * 19 +
                Vertex2.GetHashCode() * 43 + Vertex1.GetHashCode() * 19;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Edge)
                return false;
            var edge = (Edge)obj;
            return edge.Vertex1 == Vertex1 && edge.Vertex2 == Vertex2 
                || edge.Vertex2 == Vertex1 && edge.Vertex1 == Vertex2;
        }
    }
}
