﻿using System;
using System.Drawing;


namespace CircleSpace
{
    public class Circle:Square
    {
        public Circle(Pen pen, Point p1, Point p2)
            : base(pen, p1, p2)
        {
        }
        public override void Draw(Graphics g)
        {
            height = Math.Abs(pos1.Y - pos2.Y);
            g.DrawEllipse(mypen, Math.Min(pos1.X, pos2.X), Math.Min(pos1.Y, pos2.Y), height, height);
        }
    }
}
