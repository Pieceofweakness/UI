using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Практическая
{
    public class RGB
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public RGB(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public override string ToString() => $"Red - {Red} Green - {Green} Blue - {Blue}";
    }
}
