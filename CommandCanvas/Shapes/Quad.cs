using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw.Shapes
{
    internal class Quad : IShape
    {
        public Vector2i A { get; set; }
        public Vector2i B { get; set; }
        public Vector2i C { get; set; }
        public Vector2i D { get; set; }
        public Color? Color { get; set; }
        public Quad(Vector2i topLeft, Vector2i topRight, Vector2i bottomRight, Vector2i bottomLeft, Color? color)
        {
            A = topLeft;
            B = topRight;
            C = bottomRight;
            D = bottomLeft;
            Color = color;
        }
        public bool PointIsInside(Vector2i point)
        {
            Vector2i AM = A.VectorTo(point);
            Vector2i AB = A.VectorTo(B);
            Vector2i BC = B.VectorTo(C);
            Vector2i BM = B.VectorTo(point);

            double AMAB = AM.Dot(AB);
            double ABAB = AB.Dot(AB);
            double BCBC = BC.Dot(BC);
            double BCBM = BC.Dot(BM);



            if ((0 <= AMAB && AMAB <= ABAB) && (0 <= BCBM && BCBM <= BCBC)) return true;
            else return false;

        }

    }
}
