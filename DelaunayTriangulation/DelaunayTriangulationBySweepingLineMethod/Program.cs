using System;
using System.Collections.Generic;
using System.Drawing;

namespace DelaunayTriangulationBySweepingLineMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new TimeMeter();
            Console.WriteLine(t.MeasureTriangulationExecutionTimeInMs(1, 100000));
        }
    }
}
