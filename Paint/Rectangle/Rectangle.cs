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
        public int weight;
        public Rectangle(Pen pen, Point p1, Point p2)
            : base(pen, p1, p2)
        {
        }
        public override void Draw(Graphics g)
        {
            height = Math.Abs(pos1.Y - pos2.Y);
            weight = Math.Abs(pos1.X - pos2.X);
            g.DrawRectangle(mypen, Math.Min(pos1.X, pos2.X), Math.Min(pos1.Y, pos2.Y), weight, height);
        }
    }
}
