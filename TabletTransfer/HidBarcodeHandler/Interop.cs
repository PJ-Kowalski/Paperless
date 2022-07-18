using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HidBarcodeHandler
{
    class Interop
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int ToUnicode(
               uint virtualKeyCode,
               uint scanCode,
               byte[] keyboardState,
               StringBuilder receivingBuffer,
               int bufferSize,
               uint flags
           );
        public const int ErrorSuccess = 0;

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NetGetJoinInformation(string server, out IntPtr domain, out NetJoinStatus status);

        [DllImport("Netapi32.dll")]
        public static extern int NetApiBufferFree(IntPtr Buffer);

        public enum NetJoinStatus
        {
            NetSetupUnknownStatus = 0,
            NetSetupUnjoined,
            NetSetupWorkgroupName,
            NetSetupDomainName
        }
    }
}
