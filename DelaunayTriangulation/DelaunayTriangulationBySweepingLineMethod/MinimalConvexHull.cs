using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DelaunayTriangulationBySweepingLineMethod
{
    class MinimalConvexHull
    {
        class VertexOfShell
        {
            public PointF Point;
            public VertexOfShell NextVertex;
        }

        class LastAddedVertexOfShell
        {
            public PointF Point;
            public VertexOfShell NextLeftPoint;
            public VertexOfShell NextRightPoint;
        }

        public MinimalConvexHull(PointF[] sortedPoints)
        {
            var p1 = sortedPoints[0];
            var p2 = sortedPoints[1];
            var a = p2.Y - p1.Y;
            var b = p2.X - p1.X;
            Predicate<PointF> IsBelongsToStraightLine = (PointF p) => Math.Abs(a * (p.X - p1.X) - b * (p.Y - p1.Y)) < 1e-9;
            var i = 2;
            var chainOfStraight = new VertexOfShell() { Point = p2, NextVertex = new VertexOfShell() { Point = p1 } };
            for (; i < sortedPoints.Length && IsBelongsToStraightLine(sortedPoints[i]); i++)
                chainOfStraight = new VertexOfShell() { NextVertex = chainOfStraight, Point = sortedPoints[i] };
            if (i == sortedPoints.Length)
                throw new ArgumentException("All points on ine straight line");
            if (Vector.CalculatePseudoscalarComposition(p1, sortedPoints[i - 1], p1, sortedPoints[i]) > 0)
                lastVertex = new LastAddedVertexOfShell()
                {
                    Point = sortedPoints[i],
                    NextRightPoint = new VertexOfShell() { Point = p1 },
                    NextLeftPoint = chainOfStraight
                };
            else
                lastVertex = new LastAddedVertexOfShell()
                {
                    Point = sortedPoints[i],
                    NextRightPoint = chainOfStraight,
                    NextLeftPoint = new VertexOfShell() { Point = p1 }
                };
        }

        private LastAddedVertexOfShell lastVertex;

        public PointF LastAddedVertex => lastVertex.Point;

        public void AddNextVertex(PointF newVertex, PointF newLeftVertexForHim, PointF newRightVertexForHim)
        {
            VertexOfShell newLeftVertex = new VertexOfShell() { NextVertex = lastVertex.NextLeftPoint, Point = lastVertex.Point };
            while (newLeftVertex.Point != newLeftVertexForHim)
                newLeftVertex = newLeftVertex.NextVertex;
            VertexOfShell newRightVertex = new VertexOfShell() { NextVertex = lastVertex.NextRightPoint, Point = lastVertex.Point };
            while (newRightVertex.Point != newRightVertexForHim)
                newRightVertex = newRightVertex.NextVertex;
            lastVertex.Point = newVertex;
            lastVertex.NextLeftPoint = newLeftVertex;
            lastVertex.NextRightPoint = newRightVertex;
        }

        public enum Side
        {
            Left,
            Right
        }

        public IEnumerable<(PointF, PointF)> GetEdge(Side sideOfBypassOfEdges)
        {
            var previousPoint = lastVertex.Point;
            VertexOfShell nextPoint;
            if (sideOfBypassOfEdges == Side.Left)
                nextPoint = lastVertex.NextLeftPoint;
            else nextPoint = lastVertex.NextRightPoint;
            while (nextPoint != null)
            {
                yield return (previousPoint, nextPoint.Point);
                previousPoint = nextPoint.Point;
                nextPoint = nextPoint.NextVertex;
            }
        }
    }
}
