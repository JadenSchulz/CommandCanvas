using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw.Shapes
{
    internal interface IShape
    {
        public bool PointIsInside(Vector point);
    }
}
