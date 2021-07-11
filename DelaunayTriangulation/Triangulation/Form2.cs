using DelaunayTriangulationBySweepingLineMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangulation
{
    public partial class Form2 : Form
    {
        private List<PointF> points;
        private Pen pen;
        private Pen[] pens;

        public Form2()
        {
            InitializeComponent();
            points = new List<PointF>();
            float thickness = 1;
            pen = new Pen(Color.Black, thickness);
            pens = new Pen[] {new Pen(Color.Black, thickness), new Pen(Color.Red, thickness), new Pen(Color.Green, thickness),
                new Pen(Color.Blue, thickness), new Pen(Color.Yellow, thickness), new Pen(Color.Lime, thickness),
                new Pen(Color.Purple, thickness), new Pen(Color.Pink, thickness),
                new Pen(Color.Orange, thickness), new Pen(Color.Peru, thickness) };
        }

        private void pointGenerationButton_Click(object sender, EventArgs e)
        {
            var graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            points.Clear();
            var amountOfPoints = Convert.ToInt32(textBoxWithAmountOfPoints.Text);
            points.AddRange(GenerateRandomPoints(amountOfPoints, pictureBox1.Width,
                pictureBox1.Height));
            DrawPoints(points, graphics, pen, 3);
        }

        private IEnumerable<PointF> GenerateRandomPoints(int amountOfPoints, float maxX, float maxY)
        {
            var r = new Random();
            for (int i = 0; i < amountOfPoints; i++)
                yield return new PointF((float)(r.NextDouble() * maxX), (float)(r.NextDouble() * maxY));
        }

        private void DrawPoints(IEnumerable<PointF> points, Graphics graphics, Pen pen, float radius)
        {
            foreach (var p in points)
                graphics.DrawEllipse(pen, new RectangleF(p.X - radius / 2, p.Y - radius / 2, radius, radius));
        }

        private void triangulationConstructionButton_Click(object sender, EventArgs e)
        {
            var r = new Random();
            var g = pictureBox1.CreateGraphics();
            var result = DelaunayTriangulator.CalculateDelaunayTriangulation(points);
            foreach (var edge in result)
                g.DrawLine(pens[r.Next(pens.Length)], edge.Vertex1, edge.Vertex2);
        }
    }
}
