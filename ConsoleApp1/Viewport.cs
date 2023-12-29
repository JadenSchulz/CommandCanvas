using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AsciiDraw;

namespace AsciiDraw
{
    internal class Viewport
    {
        volatile bool _draw;

        private readonly SafeFileHandle _handle;

        private short width;
        private short height;


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
            int mod = 0;
            while (_draw) 
            {
                DrawBuffer(CreatePage(++mod % 2));
            }
        }

        public CharInfo[,] CreatePage(int mod = 0)
        {
            CharInfo[,] page = new CharInfo[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    page[j, i] = new CharInfo
                    {
                        Char = (char)(i + 64 + mod),
                        Attributes = 0x0040
                    };
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
