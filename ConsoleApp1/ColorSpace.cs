using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AsciiDraw
{
    internal class ColorSpace
    {
        CharInfo?[,,] colors = new CharInfo?[256, 256, 256];

        private readonly List<Vector3> consoleColors = new List<Vector3>
        {
            new Vector3(12, 12, 12),      // black
            new Vector3(0, 55, 218),      // dblue
            new Vector3(19, 161, 14),     // dgreen
            new Vector3(58, 150, 221),    // dcyan
            new Vector3(197, 15, 31),     // dred
            new Vector3(136, 23, 152),    // dpurp
            new Vector3(193, 156, 0),     // dgold
            new Vector3(204, 204, 204),   // lgray
            new Vector3(118, 118, 118),   // dgray
            new Vector3(59, 120, 255),    // lblue
            new Vector3(22, 198, 12),     // lgreen
            new Vector3(97, 214, 214),    // lcyan
            new Vector3(231, 72, 86),     // lred
            new Vector3(180, 0, 158),     // lpurp
            new Vector3(249, 241, 165),   // lyellow
            new Vector3(242, 242, 242)    // white
        };

        private readonly List<Vector3> seedColors = new List<Vector3>();

        public ColorSpace()
        {
            FillConsoleColours();
            FillInterpolatedColors25();
            FillInterpolatedColors50();
            FillRest();
        }

        private void FillConsoleColours()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                colors[consoleColors[i].R, consoleColors[i].G, consoleColors[i].B] = new CharInfo()
                {
                    Char = ' ',
                    Attributes = (ushort)(i + (i << 4))
                };

                seedColors.Add(consoleColors[i]);
            }
        }

        private void FillInterpolatedColors25()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                for (int j = 0; j < consoleColors.Count; j++)
                {
                    if (i == j) continue;
                    Vector3 newColor = consoleColors[i].Lerp(consoleColors[j], 0.25);
                    if (colors[newColor.R, newColor.G, newColor.B] != null) continue;

                    colors[newColor.R, newColor.G, newColor.B] = new CharInfo()
                    {
                        Char = '░',
                        Attributes = (ushort)(j + (i << 4))
                    };

                    seedColors.Add(newColor);
                }
            }
        }
        private void FillInterpolatedColors50()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                for (int j = 0; j < consoleColors.Count; j++)
                {
                    if (i == j) continue;
                    Vector3 newColor = consoleColors[i].Lerp(consoleColors[j], 0.50);
                    if (colors[newColor.R, newColor.G, newColor.B] != null) continue;

                    colors[newColor.R, newColor.G, newColor.B] = new CharInfo()
                    {
                        Char = '░',
                        Attributes = (ushort)(j + (i << 4))
                    };

                    seedColors.Add(newColor);
                }
            }


        }

        public void FillRest()
        {
            
            for (int r = 0; r < 256; r++)
            {
                for (int g = 0; g < 256; g++)
                {
                    for (int b = 0; b < 256; b++)
                    {
                        if (colors[r, g, b] != null) 
                            continue;
                        Vector3 currentColor = new Vector3(r, g, b);

                        double minDistance = double.MaxValue;
                        int minIndex = 0;

                        for (int i = 0; i < seedColors.Count; i++)
                        {
                            double distance = currentColor.FastDistanceTo(seedColors[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minIndex = i;
                            }
                        }


                        int R = seedColors[minIndex].R;
                        int G = seedColors[minIndex].G;
                        int B = seedColors[minIndex].B;

                        colors[r, g, b] = colors[R, G, B];
                    }
                }
            }
        }

        public CharInfo? GetColor(int r, int g, int b)
        {
            return colors[r, g, b];
        }
    }
}
