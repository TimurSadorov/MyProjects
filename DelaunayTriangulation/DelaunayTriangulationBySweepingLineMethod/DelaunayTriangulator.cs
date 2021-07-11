using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DelaunayTriangulationBySweepingLineMethod
{
    public class DelaunayTriangulator
    {
        public static List<Edge> CalculateDelaunayTriangulation(IList<PointF> data)
        {
            var dataWithoutRepet = data.Distinct();
            if (dataWithoutRepet.Count() < 3)
                throw new ArgumentException();
            var sortData = dataWithoutRepet.OrderBy(p => p.X).ThenBy(p => p.Y).ToArray();
            var currentMinimalConvexHull = new MinimalConvexHull(sortData);
            var currentTriangulation = BuildAnInitialTriangulation(sortData, currentMinimalConvexHull);
            foreach (var point in sortData.Skip((currentTriangulation.Count - 3) / 2 + 3))
                AddPoint(point, currentTriangulation, currentMinimalConvexHull);
            return currentTriangulation.Select(p => p.Key).ToList();
        }

        private static Dictionary<Edge, HashSet<PointF>> BuildAnInitialTriangulation(PointF[] sortedPoints,
            MinimalConvexHull initialMinimalConvexHull)
        {
            var lastPoint = initialMinimalConvexHull.LastAddedVertex;
            var previousePoint = sortedPoints[0];
            var initialTriangulation = new Dictionary<Edge, HashSet<PointF>>();
            var lastEdge = new Edge(lastPoint, previousePoint);
            initialTriangulation[lastEdge] = new HashSet<PointF>();
            for (int i = 1; sortedPoints[i] != lastPoint; i++)
            {
                initialTriangulation[lastEdge].Add(sortedPoints[i]);
                lastEdge = new Edge(sortedPoints[i], lastPoint);
                initialTriangulation[new Edge(previousePoint, sortedPoints[i])] = new HashSet<PointF>() { lastPoint };
                initialTriangulation[lastEdge] = new HashSet<PointF>() { previousePoint };
                previousePoint = sortedPoints[i];
            }
            return initialTriangulation;
        }

        private static void AddPoint(PointF newPoint, Dictionary<Edge, HashSet<PointF>> currentTriangulation,
            MinimalConvexHull currentMinimalConvexHull)
        {
            var checkedEdgesAfterAddingPoint = new Stack<Edge>();
            var previousPoint = new PointF();
            var leftmostPointRelativeToNewPoint = new PointF();
            var rightmostPointRelativeToNewPoint = new PointF();
            var isFirstEdge = true;

            foreach (var edge in currentMinimalConvexHull.GetEdge(MinimalConvexHull.Side.Right))
            {
                if (new Vector(newPoint, edge.Item1).
                    CalculatePseudoscalarComposition(new(edge.Item1, edge.Item2)) < 0)
                {
                    var newEdge = new Edge(edge.Item1, edge.Item2);
                    currentTriangulation[newEdge].Add(newPoint);
                    checkedEdgesAfterAddingPoint.Push(newEdge);
                    if (isFirstEdge)
                        currentTriangulation[new Edge(newPoint, edge.Item1)] = new HashSet<PointF>() { edge.Item2 };
                    else
                        currentTriangulation[new Edge(newPoint, edge.Item1)] = new HashSet<PointF>() { edge.Item2, previousPoint };
                    isFirstEdge = false;
                    rightmostPointRelativeToNewPoint = edge.Item2;
                    previousPoint = edge.Item1;
                }
                else
                    break;
            }
            if (isFirstEdge)
            {
                rightmostPointRelativeToNewPoint = currentMinimalConvexHull.LastAddedVertex;
                currentTriangulation[new Edge(newPoint, rightmostPointRelativeToNewPoint)] = new HashSet<PointF>();
            }
            else
                currentTriangulation[new Edge(newPoint, rightmostPointRelativeToNewPoint)] = new HashSet<PointF>() { previousPoint };

            isFirstEdge = true;
            foreach (var edge in currentMinimalConvexHull.GetEdge(MinimalConvexHull.Side.Left))
            {
                if (new Vector(newPoint, edge.Item1).
                    CalculatePseudoscalarComposition(new(edge.Item1, edge.Item2)) > 0)
                {
                    var newEdge = new Edge(edge.Item1, edge.Item2);
                    currentTriangulation[newEdge].Add(newPoint);
                    checkedEdgesAfterAddingPoint.Push(newEdge);
                    if (isFirstEdge)
                        currentTriangulation[new Edge(newPoint, edge.Item1)].Add(edge.Item2);
                    else
                        currentTriangulation[new Edge(newPoint, edge.Item1)] = new HashSet<PointF>() { edge.Item2, previousPoint };
                    isFirstEdge = false;
                    leftmostPointRelativeToNewPoint = edge.Item2;
                    previousPoint = edge.Item1;
                }
                else
                {
                    if (!isFirstEdge)
                        currentTriangulation[new Edge(newPoint, edge.Item1)] = new HashSet<PointF>() { previousPoint };
                    leftmostPointRelativeToNewPoint = edge.Item1;
                    break;
                }
            }
            if (isFirstEdge)
                leftmostPointRelativeToNewPoint = currentMinimalConvexHull.LastAddedVertex;
            else
                currentTriangulation[new Edge(newPoint, leftmostPointRelativeToNewPoint)] = new HashSet<PointF>() { previousPoint };
            currentMinimalConvexHull.AddNextVertex(newPoint, leftmostPointRelativeToNewPoint, rightmostPointRelativeToNewPoint);
            
            RestoreTheDelaunayCondition(currentTriangulation, checkedEdgesAfterAddingPoint, newPoint);
        }

        private static void RestoreTheDelaunayCondition(Dictionary<Edge, HashSet<PointF>> currentTriangulation, 
            Stack<Edge> checkedEdges, PointF newPoint)
        {
            while (checkedEdges.Count != 0)
            {
                var edge = checkedEdges.Pop();
                if (!IsDelaunayConditionSatisfied(edge, currentTriangulation))
                    Flip(edge, checkedEdges, currentTriangulation, newPoint);
            }
        }

        private static bool IsDelaunayConditionSatisfied(Edge edge,
            Dictionary<Edge, HashSet<PointF>> currentTriangulation)
        {
            var oppositePeaks = currentTriangulation[edge].ToArray();
            var v1a = new Vector(oppositePeaks[0], edge.Vertex1);
            var v2a = new Vector(oppositePeaks[0], edge.Vertex2);
            var v1b = new Vector(oppositePeaks[1], edge.Vertex1);
            var v2b = new Vector(oppositePeaks[1], edge.Vertex2);
            var cA = v1a.X * v2a.X + v1a.Y * v2a.Y; 
            var cB = v1b.X * v2b.X + v1b.Y * v2b.Y;
            if (cA < 0 && cB < 0)
                return false;
            if (cA >= 0 && cB >= 0)
                return true;
            var sA = v1a.X * v2a.Y - v2a.X * v1a.Y;
            var sB = v1b.X * v2b.Y - v2b.X * v1b.Y;
            if (sA < 0)
                sA = -sA;
            if (sB < 0)
                sB = -sB;
            return sA * cB + sB * cA >= 0;
        }

        private static void Flip(Edge edge, Stack<Edge> checkedEdges,
            Dictionary<Edge, HashSet<PointF>> currentTriangulation, PointF newPoint)
        {

            currentTriangulation[edge].Remove(newPoint);
            var oldPoint = currentTriangulation[edge].First();

            var nextCheckedEdge1 = new Edge(oldPoint, edge.Vertex1);
            var nextCheckedEdge2 = new Edge(oldPoint, edge.Vertex2);

            var oldEdge1 = new Edge(newPoint, edge.Vertex1);
            var oldEdge2 = new Edge(newPoint, edge.Vertex2);

            currentTriangulation[oldEdge1].Remove(edge.Vertex2);
            currentTriangulation[oldEdge1].Add(oldPoint);

            currentTriangulation[oldEdge2].Remove(edge.Vertex1);
            currentTriangulation[oldEdge2].Add(oldPoint);

            currentTriangulation[nextCheckedEdge1].Remove(edge.Vertex2);
            currentTriangulation[nextCheckedEdge1].Add(newPoint);
            if (currentTriangulation[nextCheckedEdge1].Count > 1)
                checkedEdges.Push(nextCheckedEdge1);

            currentTriangulation[nextCheckedEdge2].Remove(edge.Vertex1);
            currentTriangulation[nextCheckedEdge2].Add(newPoint);
            if (currentTriangulation[nextCheckedEdge2].Count > 1)
                checkedEdges.Push(nextCheckedEdge2);

            currentTriangulation[new Edge(oldPoint, newPoint)] = new HashSet<PointF>() { edge.Vertex1, edge.Vertex2 };

            currentTriangulation.Remove(edge);
        }

    }
}
