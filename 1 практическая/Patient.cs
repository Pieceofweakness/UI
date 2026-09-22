using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _1_практическая
{
    public struct Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString() => $"{Year:D4}-{Month:D2}-{Day:D2}";
    }
    public struct RGB
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

    public class Patient
    {
        public string Passport { get; set; } //ss ss-nnnnnn
        public string Name { get; set; } //any
        public Date BirthDate { get; set; } // yyyy-mm-dd
        public string Phone { get; set; } //+X(XXX) XXX-XX-XX or X(XXX) XXX-XXXX 
        public double Temperature { get; set; } //XX,XX
        public RGB SkinColor { get; set; }

        public string Print()
        {
            return $"\nИмя - {Name}\nПаспорт - {Passport}\nДата рождения - {BirthDate}\nНомер телефона - {Phone}\nТемпература - {Temperature}\nЦвет кожи - {SkinColor}";
        }


    }
}
