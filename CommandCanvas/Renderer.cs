using AsciiDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCanvas
{
    internal class Renderer
    {
        private readonly Canvas _canvas;
        private readonly object _lock = new object();   

        public Renderer(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void StartRenderer()
        {
            Thread renderThread = new Thread(RenderLoop);
            renderThread.Start();
        }

        private void RenderLoop()
        {
            while (true)
            {
                lock (_lock)
                {
                    _canvas.Render();
                }
                HandleInput();
            }

        }

        private void HandleInput()
        {
            Vector2i movementVector = new Vector2i(0, 0);
            if (Console.KeyAvailable)
            {
                lock( _lock)
                {
                    var keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.W:
                            movementVector.Y += -1;
                            break;
                        case ConsoleKey.S:
                            movementVector.Y += 1;
                            break;
                        case ConsoleKey.A:
                            movementVector.X += -1;
                            break;
                        case ConsoleKey.D:
                            movementVector.X += 1;
                            break;
                    }
                    _canvas.MoveCanvas(movementVector);
                }
            }

        }
    }
}
