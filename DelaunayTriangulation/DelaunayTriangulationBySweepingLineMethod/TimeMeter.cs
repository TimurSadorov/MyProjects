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

            var data = new PointF[repetitionCount][];
            var r = new Random();

            for (int j = 0; j < repetitionCount; j++)
            {
                data[j] = new PointF[amountOfPoints];
                for (int i = 0; i < amountOfPoints; i++)
                    data[j][i] = new PointF(r.Next(), r.Next());
            }

            DelaunayTriangulator.CalculateDelaunayTriangulation(data[0]);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < repetitionCount; i++)
                DelaunayTriangulator.CalculateDelaunayTriangulation(data[i]);
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalMilliseconds / repetitionCount;
        }
    }
}
