using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RectangleSpace;

namespace EllipseSpace
{
    public class Ellipse:RectangleSpace.Rectangle
    {
        public Ellipse(Pen pen, Point p1, Point p2)
            : base(pen, p1, p2)
        { }
        public override void Draw(Graphics g)
        {
            height = Math.Abs(pos1.Y - pos2.Y);
            weight = Math.Abs(pos1.X - pos2.X);
            g.DrawEllipse(mypen, Math.Min(pos1.X, pos2.X), Math.Min(pos1.Y, pos2.Y), weight, height);
        }
    }
}
