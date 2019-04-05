using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllipseSpace
{
    public class Ellipse:Rectangle
    {
        public Ellipse(int x, int y, Pen pen, Point p)
            : base(x, y, pen, p)
        { }
        public override void Draw(Graphics g)
        {
            h = Math.Abs(y1 - pos.Y);
            w = Math.Abs(x1 - pos.X);
            g.DrawEllipse(mypen, Math.Min(x1, pos.X), Math.Min(y1, pos.Y), w, h);
        }
    }
}
