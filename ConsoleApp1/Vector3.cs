using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw
{
    internal class Vector3
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int R => X;
        public int G => Y;
        public int B => Z;

        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Lerp(Vector3 to, double amount)
        {
            return new Vector3(
                (int)Math.Round(Mathf.Lerp(X, to.X, amount)),
                (int)Math.Round(Mathf.Lerp(Y, to.Y, amount)),
                (int)Math.Round(Mathf.Lerp(Z, to.Z, amount)));
        }

        public double FastDistanceTo(Vector3 to)
        {
            return (to.X - X) + (to.Y - Y) + (to.Z - Z);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
