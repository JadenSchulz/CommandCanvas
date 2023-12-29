using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AsciiDraw;
using AsciiDraw.Shapes;
using System.Xml.Linq;

namespace AsciiDraw
{
    internal class Viewport
    {
        volatile bool _draw;

        private readonly SafeFileHandle _handle;

        private short width;
        private short height;

        //private readonly Quad myRectangle = new Quad(
            //new Vector(1, 1), new Vector(2, 1), new Vector(2, 3), new Vector(1, 3), System.Drawing.Color.MistyRose);

        public Viewport(short width, short height) 
        {
            this.width = width;
            this.height = height;
            _handle = ConsoleHelper.GetOutputHandle();
        }

        public void Draw()
        {
            _draw = true;
            Console.SetWindowSize(width, height);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Thread drawThread = new Thread(DrawLoop);
            drawThread.Start();
        }

        private void DrawLoop()
        {
            int mod = 32;
            while (_draw) 
            {
                DrawBuffer(CreatePage(++mod));
                Thread.Sleep(128);
            }
        }

        public ConsoleHelper.CharInfo[,] CreatePage(int mod = 0)
        {
            ConsoleHelper.CharInfo[,] page = new ConsoleHelper.CharInfo[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    // is there something here?

                    //if (myRectangle.PointIsInside(new Vector(column, row)))
                    //{
                    //    page[row, column] = new ConsoleHelper.CharInfo
                    //    {
                    //        Char = '@',
                    //        Attributes = 0x0004
                    //    };
                    //}
                    //else
                    //{
                        page[row, column] = new ConsoleHelper.CharInfo
                        {
                            Char = 'X',
                            Attributes = 0x0400
                        };
                    //}

                }
            }
            return page;
        }


        public void Interrupt()
        {
            _draw = false;
        }

        public void DrawBuffer(ConsoleHelper.CharInfo[,] buffer)
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            static extern bool WriteConsoleOutputW(
              SafeFileHandle hConsoleOutput,
              ConsoleHelper.CharInfo[,] lpBuffer,
              ConsoleHelper.Coord dwBufferSize,
              ConsoleHelper.Coord dwBufferCoord,
            ref ConsoleHelper.Rectangle lpWriteRegion);

            ConsoleHelper.Rectangle rect = new(0, 0, (short)(width-1), (short)(height-1));
            WriteConsoleOutputW(_handle, buffer, new ConsoleHelper.Coord(width, height), new ConsoleHelper.Coord(0, 0), ref rect);
        }

    }
}
