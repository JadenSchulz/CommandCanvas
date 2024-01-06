using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw.Shapes
{
    internal class Triangle : IShape
    {
        public Vector A { get; set; }
        public Vector B { get; set; }
        public Vector C { get; set; }

        public Triangle (Vector A, Vector B, Vector C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public bool PointIsInside(Vector p)
        {
            var s = (A.X - C.X) * (p.Y - C.Y) - (A.Y - C.Y) * (p.X - C.X);
            var t = (B.X - A.X) * (p.Y - A.Y) - (B.Y - A.Y) * (p.X - A.X);

            if ((s < 0) != (t < 0) && s != 0 && t != 0)
                return false;

            var d = (C.X - B.X) * (p.Y - B.Y) - (C.Y - B.Y) * (p.X - B.X);
            return d == 0 || (d < 0) == (s + t <= 0);
        }


    }
}
