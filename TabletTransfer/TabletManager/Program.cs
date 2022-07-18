using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;
using TabletManager.UI;
using TabletManager.UI.CommonControls;

namespace TabletManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Properties.Settings.Default["PaperlessConnectionString"] = ConnStringHelperLib.ConnStringHelperLib.Decrypt(Properties.Settings.Default.PaperlessConnectionStringEnc);
            CommonDatabase.CommonDbAccess.Setup(Properties.Settings.Default.PaperlessConnectionString);

            Auth auth = new Auth();
            StateSingleton.Instance.Init(auth, false);

            auth.OnAuthenticated.Subscribe(new Action<Operator>((op) => {
                (new Main()).Show();
                auth.Hide();//auth is connected to keyboard event loop used by hid barcode reader
            }));
            auth.OnCancel.Subscribe(new Action<bool>((x) => {
                if (x)
                {
                    Application.Exit();
                }
            }));

            auth.Show();

            Application.Run();
        }
    }
}
