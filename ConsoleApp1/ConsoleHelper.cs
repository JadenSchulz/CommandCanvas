using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDraw
{
    [StructLayout(LayoutKind.Sequential)]
    struct Rectangle
    {
        public short left, top, right, bottom;
        public Rectangle(short left, short top, short right, short bottom)
        {
            this.left = left; this.top = top; this.right = right; this.bottom = bottom;
        }
    }
    [StructLayout(LayoutKind.Explicit)]
    struct CharInfo
    {
        [FieldOffset(0)] public ushort Char;
        [FieldOffset(2)] public ushort Attributes;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct Coord
    {
        public short x, y;
        public Coord(short x, short y)
        {
            this.x = x; this.y = y;
        }
    }
    internal class ConsoleHelper
    {
        public static SafeFileHandle GetOutputHandle()
        {
            [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            static extern SafeFileHandle CreateFile(
                string fileName,
                [MarshalAs(UnmanagedType.U4)] uint fileAccess,
                [MarshalAs(UnmanagedType.U4)] uint fileShare,
                IntPtr securityAttributes,
                [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                [MarshalAs(UnmanagedType.U4)] int flags,
                IntPtr template);

            SafeFileHandle outputHandle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            if (outputHandle.IsInvalid) throw new Exception("outputHandle is invalid!");
            return outputHandle;
        }

    }
}
