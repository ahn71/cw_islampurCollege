using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DS.DAL.ComplexScripting
{
    public class WinApi
    {
        private static IntPtr HWND_TOP = IntPtr.Zero;
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const int SWP_SHOWWINDOW = 64;

        public static int ScreenX
        {
            get
            {
                return WinApi.GetSystemMetrics(0);
            }
        }

        public static int ScreenY
        {
            get
            {
                return WinApi.GetSystemMetrics(1);
            }
        }

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width, int height, uint flags);

        public static void SetWinFullScreen(IntPtr hwnd)
        {
            WinApi.SetWindowPos(hwnd, WinApi.HWND_TOP, 0, 0, WinApi.ScreenX, WinApi.ScreenY, 64U);
        }
    }
}
