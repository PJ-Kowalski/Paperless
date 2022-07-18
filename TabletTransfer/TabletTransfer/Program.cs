using CommonDatabase.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletTransfer.AI;
using TabletTransfer.AI.Tools;

namespace TabletTransfer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (Mutex mutex = new Mutex(false, @"TabletTransfer.exe"))//Local -> other sessions are separate, if you want Global, please consider mutex permissions between sessions.
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Problem z uruchomieniem programu TabletTransfer, zrestartuj urządzenie.");
                    return;
                }
                GC.Collect();
                
                Properties.Settings.Default["ProgAuthConnectionString"] = ConnStringHelperLib.ConnStringHelperLib.Decrypt(Properties.Settings.Default.ProgAuthConnectionStringEnc);
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Docked tmp = new Docked();
                StateSingleton.Instance.DoStartup(tmp, args.Length == 1); // simulate barcode handler
                
                List<string> NameList = CommonDatabase.CommonDbAccess.GetUserNameListForLocation(StateSingleton.Instance.Myself.Location);
                Task t = new Task(()=>PhotoDownloader.GetPhotosForLocation(NameList));
                t.Start();
                Application.Run(tmp);
            }
        }
    }
}
