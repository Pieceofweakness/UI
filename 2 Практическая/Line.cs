using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Практическая
{
    public class Line: Shape
    {
        public Point A { get; }
        public Point B { get; }


        public Line(Point a, Point b)
        {
            A = a;
            B = b;
        }

        public override string ToString() => $"Line({A}, {B})";
    }
}
