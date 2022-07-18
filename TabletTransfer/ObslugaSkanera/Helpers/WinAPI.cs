using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ObslugaSkanera.Helpers
{
    class WinAPI
    {
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pnt);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(int hWnd, StringBuilder title, int size);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public static string GetWindowTitle(Point position)
        {
            IntPtr hWnd = WindowFromPoint(position);
            if (hWnd != IntPtr.Zero)
            {
                var length = GetWindowTextLength(hWnd) + 1;
                var title = new StringBuilder(length);
                GetWindowText((int)hWnd, title, length);
                return title.ToString();
            }
            return null;
        }
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        static public string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    }
}
