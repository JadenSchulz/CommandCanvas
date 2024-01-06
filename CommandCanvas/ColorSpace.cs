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
        Dictionary<Vector3i, CharInfo> colors = new Dictionary<Vector3i, CharInfo>();

        private readonly List<Vector3i> consoleColors = new List<Vector3i>
        {
            new Vector3i(12, 12, 12),      // black
            new Vector3i(0, 55, 218),      // dblue
            new Vector3i(19, 161, 14),     // dgreen
            new Vector3i(58, 150, 221),    // dcyan
            new Vector3i(197, 15, 31),     // dred
            new Vector3i(136, 23, 152),    // dpurp
            new Vector3i(193, 156, 0),     // dgold
            new Vector3i(204, 204, 204),   // lgray
            new Vector3i(118, 118, 118),   // dgray
            new Vector3i(59, 120, 255),    // lblue
            new Vector3i(22, 198, 12),     // lgreen
            new Vector3i(97, 214, 214),    // lcyan
            new Vector3i(231, 72, 86),     // lred
            new Vector3i(180, 0, 158),     // lpurp
            new Vector3i(249, 241, 165),   // lyellow
            new Vector3i(242, 242, 242)    // white
        };

        private readonly List<Vector3i> seedColors = new List<Vector3i>();

        public ColorSpace()
        {
            FillConsoleColours();
            FillInterpolatedColors12();
            FillInterpolatedColors25();
            //FillInterpolatedColors60(); //⛆
            FillInterpolatedColors75();
            //ManualCorrections();
        }

        private void ManualCorrections()
        {
            var gray85 = new Vector3i(85, 85, 85);
            colors[gray85] = new CharInfo()
            {
                Char = '▒',
                Attributes = (0 + (8 << 4))
            };
            seedColors.Add(gray85);

            var gray101 = new Vector3i(101, 101, 101);
            colors[gray101] = new CharInfo()
            {
                Char = '▒',
                Attributes = (8 + (0 << 4))
            };
            seedColors.Add(gray101);
        }

        private void FillConsoleColours()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                colors[new Vector3i(consoleColors[i].R, consoleColors[i].G, consoleColors[i].B)] = new CharInfo()
                {
                    Char = '`',
                    Attributes = (ushort)(i + (i << 4))
                };

                seedColors.Add(consoleColors[i]);
            }
        }

        private void FillInterpolatedColors12()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                for (int j = 0; j < consoleColors.Count; j++)
                {
                    if (i == j) continue;
                    Vector3i newColor = consoleColors[i].Lerp(consoleColors[j], 0.125);
                    if (colors.ContainsKey(new Vector3i(newColor.R, newColor.G, newColor.B))) continue;

                    colors[new Vector3i(newColor.R, newColor.G, newColor.B)] = new CharInfo()
                    {
                        Char = '░',
                        Attributes = (ushort)(j + (i << 4))
                    };

                    seedColors.Add(newColor);
                }
            }
        }
        private void FillInterpolatedColors25()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                for (int j = 0; j < consoleColors.Count; j++)
                {
                    if (i == j) continue;
                    Vector3i newColor = consoleColors[i].Lerp(consoleColors[j], 0.25);
                    if (colors.ContainsKey(new Vector3i(newColor.R, newColor.G, newColor.B))) continue;

                    colors[new Vector3i(newColor.R, newColor.G, newColor.B)] = new CharInfo()
                    {
                        Char = '▒',
                        Attributes = (ushort)(j + (i << 4))
                    };

                    seedColors.Add(newColor);
                }
            }
        }
        private void FillInterpolatedColors60()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                for (int j = 0; j < consoleColors.Count; j++)
                {
                    if (i == j) continue;
                    Vector3i newColor = consoleColors[i].Lerp(consoleColors[j], 0.625);
                    if (colors.ContainsKey(new Vector3i(newColor.R, newColor.G, newColor.B))) continue;

                    colors[new Vector3i(newColor.R, newColor.G, newColor.B)] = new CharInfo()
                    {
                        Char = '⛆',
                        Attributes = (ushort)(j + (i << 4))
                    };

                    seedColors.Add(newColor);
                }
            }
        }
        private void FillInterpolatedColors75()
        {
            for (int i = 0; i < consoleColors.Count; i++)
            {
                for (int j = 0; j < consoleColors.Count; j++)
                {
                    if (i == j) continue;
                    Vector3i newColor = consoleColors[i].Lerp(consoleColors[j], 0.6250);
                    if (colors.ContainsKey(new Vector3i(newColor.R, newColor.G, newColor.B))) continue;

                    colors[new Vector3i(newColor.R, newColor.G, newColor.B)] = new CharInfo()
                    {
                        Char = '▓',
                        Attributes = (ushort)(j + (i << 4))
                    };

                    seedColors.Add(newColor);
                }
            }
        }

        public CharInfo GetCharInfo(Vector3i rgbColor)
        {

            if (colors.ContainsKey(rgbColor)) return colors[rgbColor];

            double minDistance = double.MaxValue;
            int minIndex = 0;

            for (int i = 0; i < seedColors.Count; i++)
            {
                double distance = rgbColor.DistanceTo(seedColors[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minIndex = i;
                }
            }

            colors[rgbColor] = colors[new Vector3i(seedColors[minIndex].R, seedColors[minIndex].G, seedColors[minIndex].B)];

            return colors[rgbColor];
        }
    }
}
