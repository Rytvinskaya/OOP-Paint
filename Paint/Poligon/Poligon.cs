using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShapeSpace;

namespace PoligonSpace
{
    public class Poligon:Shape
    {
        public List<Point> points;
        public Poligon(List<Point> ps,Pen pen, Point p1, Point p2) : base(pen, p1, p2)
        {
            points = ps;
        }
        public override void Draw(Graphics g)
        {
            for (int i = 0; i < points.Count - 2; i++)
            {
                g.DrawLine(mypen, points[i], points[i + 1]);
            }
            if (!((Math.Abs(points[0].X - points[points.Count - 1].X) < 5) && (Math.Abs(points[0].Y - points[points.Count - 1].Y) < 5) && (points.Count > 2)))
            {
                g.DrawLine(mypen, points[points.Count - 2], points[points.Count - 1]);
                g.DrawLine(mypen, points[points.Count - 1], points[0]);
            }
            else
            {
                points[points.Count - 1] = points[0];
                g.DrawLine(mypen, points[points.Count - 2], points[0]);
            }
        }
    }
}
