using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw
{
    static public class Mathf
    {
        static public double Lerp(double a, double b, double f)
        {
            return (a * (1.0 - f)) + (b * f);
        }

        
    }
}
