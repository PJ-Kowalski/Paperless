using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletTransfer.AI.Tools
{
    class DesktopAccess
    {
        public static void LockExplorer()
        {
            if (false)
            {
                //when running explorer.exe.Kill(), it restarts.
                //when using taskkill it does not...

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo("taskkill.exe");
                p.StartInfo.Arguments = "/F /IM explorer.exe";
                p.StartInfo.WorkingDirectory = @"c:\windows\system32";

                //to have no cmd window shown to user:
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;

                p.Start();
            }
        }
        public static void UnlockExplorer()
        {
            if (false)
            {
                Process.Start(@"c:\windows\explorer.exe");
            }
        }
    }
}
