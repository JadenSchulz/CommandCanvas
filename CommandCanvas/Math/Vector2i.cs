using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw
{
    public class Vector2i
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int Dot(Vector2i with)
        {
            return (X * with.X) + (Y * with.Y);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public Vector2i VectorTo(Vector2i to)
        {
            int x2 = (to.X - X);
            int y2 = (to.Y - Y); 
            return new Vector2i(x2, y2);
        }
    }

}