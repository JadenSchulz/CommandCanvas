using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw
{
    static class Colour
    {

        public static ushort[] Hues = new ushort[]
        {
            0b0001, // blue
            0b1001, // blue

            0b0011, // blue-green
            0b1011, // blue-green

            0b0010, // green
            0b1010, // green

            0b0110, // yellow
            0b1110, // yellow

            0b0100, // red
            0b1100, // red

            0b0101, // purple
            0b1101, // purple

            0b0000, // gray
            0b1000, // white

            0b0111, // black
            0b1111, // gray


        };
        //private const int FOREGROUND_BLUE = 0x0001;
        //private const int FOREGROUND_GREEN = 0x0002;
        //private const int FOREGROUND_RED = 0x0004;
        //private const int FOREGROUND_INTENSITY = 0x0008;
        //private const int BACKGROUND_BLUE = 0x0010;
        //private const int BACKGROUND_GREEN = 0x0020;
        //private const int BACKGROUND_RED = 0x0040;
        //private const int BACKGROUND_INTENSITY = 0x0080;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h">0-5</param>
        /// <param name="s">0-3</param>
        /// <param name="l">0-3</param>
        /// <returns></returns>
        static public ushort GetHSLColourAttribute(int h, int s, int l)
        {
            return 0;
        }
    }
}
