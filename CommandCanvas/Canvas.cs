using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AsciiDraw;
using AsciiDraw.Shapes;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AsciiDraw
{
    internal class Canvas
    {
        volatile bool _draw;

        private short width;
        private short height;

        private int imageWidth;
        private int imageHeight;

        private volatile int offsetX;
        private volatile int offsetY;

        readonly Bitmap image;

        int q = 0;

        ColorSpace colorSpace = new ColorSpace();

        private readonly IShape myShape = new Triangle(new Vector2i(20, 0), new Vector2i(0, 20), new Vector2i(0, 0));

        public Canvas(short width, short height, Bitmap image) 
        {
            this.width = width;
            this.height = height;
            this.image = image;

            imageWidth = image.Width;
            imageHeight = image.Height;
        }

        public void Initialize()
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width > 20 ? width : 20, height > 20 ? height : 20);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public void Render()
        {
            CharInfo[,] page = new CharInfo[height, width];
            
            for (int y = 0; y < height; y++) 
            {
                for (int x = 0; x < width; x++)
                {
                    Color c;
                    try
                    {
                        c = image.GetPixel(x + offsetX, y + offsetY);
                    }
                    catch
                    {
                        c = Color.Black;
                    }
                    page[y, x] = colorSpace.GetCharInfo(new Vector3i(c.R, c.G, c.B));
                }
            }

            ConsoleHelper.DrawBuffer(page, width, height);
        }

        public void MoveCanvas(Vector2i direction)
        {
            offsetX += direction.X;
            offsetY += direction.Y;
        }


        public void Interrupt()
        {
            _draw = false;
        }



    }

}


