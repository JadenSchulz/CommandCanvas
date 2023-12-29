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
        public Vector A { get; set; }
        public Vector B { get; set; }
        public Vector C { get; set; }
        public Vector D { get; set; }
        public Color Color { get; set; }
        public Quad(Vector topLeft, Vector topRight, Vector bottomRight, Vector bottomLeft, Color color)
        {
            A = topLeft;
            B = topRight;
            C = bottomRight;
            D = bottomLeft;
            Color = color;
        }
        public bool PointIsInside(Vector point)
        {
            Vector AM = A.VectorTo(point);
            Vector AB = A.VectorTo(B);
            Vector AD = A.VectorTo(D);

            double AMAB = AM.Dot(AB);
            double ABAB = AB.Dot(AB);
            double AMAD = AM.Dot(AD);
            double ADAD = AD.Dot(AD);



            if ((0 < AMAB && AMAB < ABAB) && (0 < AMAD && AMAD < ADAD)) return true;
            else return false;

        }

    }
}
