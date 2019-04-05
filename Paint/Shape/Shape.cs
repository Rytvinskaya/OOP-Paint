using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShapeSpace
{
    public class Shape
    {
        public Point pos1, pos2;
        public Pen mypen;
        public Shape()
        { }
        public Shape(Pen pen, Point p1, Point p2)
        {
            mypen = pen;
            pos1 = p1;
            pos2 = p2;
        }
        public virtual void Draw(Graphics g)
        {
        }
    }
}
