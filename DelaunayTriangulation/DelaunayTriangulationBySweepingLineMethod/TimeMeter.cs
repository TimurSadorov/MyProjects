using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelaunayTriangulationBySweepingLineMethod
{
    class TimeMeter
    {
        public double MeasureTriangulationExecutionTimeInMs(int repetitionCount, int amountOfPoints)
        {
            var data = new List<PointF>();
            var r = new Random();

            for (int i = 0; i < amountOfPoints; i++)
                data.Add(new PointF(r.Next(), r.Next()));

            DelaunayTriangulator.CalculateDelaunayTriangulation(data);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < repetitionCount; i++)
                DelaunayTriangulator.CalculateDelaunayTriangulation(data);
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalMilliseconds / repetitionCount;
        }
    }
}
