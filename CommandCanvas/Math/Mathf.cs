using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw
{
    static public class Mathf
    {
        static public double Lerp(double from, double to, double weight)
        {
            return from + ((to - from) * weight); ;
        }

        
    }
}
