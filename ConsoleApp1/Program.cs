// See https://aka.ms/new-console-template for more information
#pragma warning disable CA1416 // Validate platform compatibility


using AsciiDraw;
using Microsoft.Win32.SafeHandles;
using System.Drawing;
using System.Runtime.InteropServices;
using System;

const int WindowWidth = 128;
const int WindowHeight = 32;

const string chars = " .'^:+O#@$";
const string alt = "░▒▓";

Viewport viewport = new Viewport(WindowWidth, WindowHeight);
viewport.Draw();

Console.ReadKey(false);
viewport.Interrupt();

