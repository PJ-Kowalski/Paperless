using CommonDatabase.Data;
using ConnectionWatcher.Lotus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ConnectionWatcher
{
    public partial class ServiceConnection : ServiceBase
    {
        List <ShortTablet> tablets = new List <ShortTablet> ();
        System.Timers.Timer checkConnectionTimer = new System.Timers.Timer();
        System.Timers.Timer checkDocumentsRevisionTimer = new System.Timers.Timer();
        int updateTabletListAfterCycles = 60;
        int n = 0;
        public ServiceConnection()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            CommonDatabase.CommonDbAccess.Setup(Properties.Settings.Default.PaperlessConnectionString);
            tablets = CommonDatabase.CommonDbAccess.GetAllTabletName();

            checkConnectionTimer.Elapsed +=new ElapsedEventHandler(CheckConnectionTimer_Tick);
            checkConnectionTimer.Interval = 60000;
            checkConnectionTimer.Enabled = true;

            checkDocumentsRevisionTimer.Elapsed += new ElapsedEventHandler(CheckDocumentsRevisionTimer_Elapsed);
            checkDocumentsRevisionTimer.Interval = 43200000; //12 h
            checkDocumentsRevisionTimer.Enabled = true;

            string message= DateTime.Now + System.Environment.NewLine;
            foreach (var tablet in tablets)
            {
                message = message + tablet.WindowsName.ToString() + System.Environment.NewLine;
            }
            WriteToFile(message);
        }


        private void CheckConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (n>updateTabletListAfterCycles)
            {
                tablets = CommonDatabase.CommonDbAccess.GetAllTabletName();
                n = 0;
            }

            CheckConnectionToTablet();
            n++;
        }
        private void CheckDocumentsRevisionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CheckDocumentationRevision();
        }

        protected override void OnStop()
        {
        }
        //for debug
        public void Proccess()
        {
            CommonDatabase.CommonDbAccess.Setup(Properties.Settings.Default.PaperlessConnectionString);
            CheckConnectionToTablet();
            CheckDocumentationRevision();

        }

        public void CheckConnectionToTablet()
        {
            Parallel.ForEach(tablets, tablet =>
            {
                string hostname = tablet.WindowsName;
                int timeout = 1000;
                Ping ping = new Ping();
                try
                {
                    PingReply pingReplay = ping.Send(hostname, timeout);
                    if (pingReplay.Status == IPStatus.Success)
                    {
                        CommonDatabase.CommonDbAccess.DeadManSwitch(tablet.IdNumber);
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                }
            });
        }

        public void CheckDocumentationRevision()
        {
            List<DocumentRevisionData> documentsInOracleDB = CommonDatabase.CommonDbAccess.GetDocumentRevision();
            string message = "";
            LotusService lotus = new LotusService();
            foreach (var item in lotus.UpdateDocumentRevision())
            {
                if (documentsInOracleDB.Any(i=>i.DocNum==item.DocNum && i.revisionValue!=item.revisionValue) || !documentsInOracleDB.Any(i => i.DocNum == item.DocNum))
                {
                    CommonDatabase.CommonDbAccess.UpdateDocumentRevision(item);
                }

            }
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
