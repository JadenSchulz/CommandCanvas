using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw
{
    internal class Vector3i
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int R => X;
        public int G => Y;
        public int B => Z;

        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3i Lerp(Vector3i to, double amount)
        {
            return new Vector3i(
                (int)Math.Round(Mathf.Lerp(X, to.X, amount)),
                (int)Math.Round(Mathf.Lerp(Y, to.Y, amount)),
                (int)Math.Round(Mathf.Lerp(Z, to.Z, amount)));
        }

        public double DistanceTo(Vector3i to)
        {
            return Math.Sqrt(Math.Pow(to.X - X, 2) + Math.Pow(to.Y - Y, 2) + Math.Pow(to.Z - Z, 2));
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public override bool Equals(object? obj)
        {
            Vector3i vector = obj as Vector3i;
            if (vector == null) return false;
            if (vector.X != X) return false;
            if (vector.Y != Y) return false;
            if (vector.Z != Z) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
