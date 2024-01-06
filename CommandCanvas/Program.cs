// See https://aka.ms/new-console-template for more information
#pragma warning disable CA1416 // Validate platform compatibility


using AsciiDraw;
using Microsoft.Win32.SafeHandles;
using System.Drawing;
using System.Runtime.InteropServices;
using System;
using static System.Net.Mime.MediaTypeNames;
using CommandCanvas;

if (args.Length != 1)
{
    Console.WriteLine("Usage: CommandCanvas <path>");
}
Bitmap image;
try
{
    image = new Bitmap(args[0]);
}
catch
{
    Console.WriteLine("An exception occured loading the image file. Please check the file path.");
    Console.WriteLine("Accepted file formats are: BMP, GIF, EXIF, JPG, PNG and TIFF.");
    return;
}

Canvas canvas = new Canvas(
    image.Width < Console.LargestWindowWidth ? (short)image.Width : (short)Console.LargestWindowWidth,
    image.Height < Console.LargestWindowHeight ? (short)image.Height : (short)Console.LargestWindowHeight,
    image);

canvas.Initialize();


// main loop

Renderer renderer = new Renderer(canvas);
renderer.StartRenderer();
