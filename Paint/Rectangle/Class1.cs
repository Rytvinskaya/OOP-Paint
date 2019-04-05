using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SquareSpace;

namespace RectangleSpace
{
    public class Rectangle:Square
    {
        public int w;
        public Rectangle(int x, int y, Pen pen, Point p)
            : base(x, y, pen, p)
        {

        }
        public override void Draw(Graphics g)
        {
            h = Math.Abs(y1 - pos.Y);
            w = Math.Abs(x1 - pos.X);
            g.DrawRectangle(mypen, Math.Min(x1, pos.X), Math.Min(y1, pos.Y), w, h);
        }
    }
}
