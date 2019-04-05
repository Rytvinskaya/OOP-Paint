using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShapeSpace;

namespace LineSpace
{
    public class Line:Shape
    {
        public int x2, y2;
        public Line(Pen pen, Point p1, Point p2)
            : base(pen, p1, p2)
        {

        }
        public override void Draw(Graphics g)
        {
            g.DrawLine(mypen, pos1.X, pos1.Y, pos2.X, pos2.Y);
        }
    }
}
