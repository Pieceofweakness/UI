using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Практическая
{
    public abstract class Shape
    {
        public RGB Color { get; set; } = new RGB(0, 0, 0);
        public abstract override string ToString();
    }
}
