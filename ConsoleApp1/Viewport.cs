using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AsciiDraw;
using AsciiDraw.Shapes;
using System.ComponentModel.DataAnnotations;

namespace AsciiDraw
{
    internal class Viewport
    {
        volatile bool _draw;

        private readonly SafeFileHandle _handle;

        private short width;
        private short height;

        int r = 0;
        int g = 0;
        int b = 0;

        ColorSpace colorSpace = new ColorSpace();

        private readonly IShape myShape = new Triangle(new Vector(20, 0), new Vector(0, 20), new Vector(0, 0));

        public Viewport(short width, short height) 
        {
            this.width = width;
            this.height = height;
            _handle = ConsoleHelper.GetOutputHandle();
        }

        public void Initialize()
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public void Draw()
        {
            _draw = true;
            Thread drawThread = new Thread(DrawLoop);
            drawThread.Start();
        }

        private void DrawLoop()
        {
            ushort mod = 0;
            while (_draw) 
            {
                DrawBuffer(CreatePage());
            }
        }

        public CharInfo[,] CreatePage(ushort mod = 0)
        {
            CharInfo[,] page = new CharInfo[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    page[y, x] = (CharInfo)colorSpace.GetColor(r, g, b)!;

                    r+= 8;
                    if (r > 255)
                    {
                        r = 0;
                        g+= 8;
                    }

                    if (g > 255)
                    {
                        g = 0;
                        b+=8;
                    }

                    if (b > 255)
                    {
                        b = 0;
                    }
                }


            }
            return page;
        }


        public void Interrupt()
        {
            _draw = false;
        }

        public void DrawBuffer(CharInfo[,] buffer)
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            static extern bool WriteConsoleOutputW(
              SafeFileHandle hConsoleOutput,
              CharInfo[,] lpBuffer,
              Coord dwBufferSize,
              Coord dwBufferCoord,
            ref Rectangle lpWriteRegion);

            Rectangle rect = new(0, 0, (short)(width-1), (short)(height-1));
            WriteConsoleOutputW(_handle, buffer, new Coord(width, height), new Coord(0, 0), ref rect);
        }

    }

}


