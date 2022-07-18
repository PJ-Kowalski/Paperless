using CommonDatabase.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;
using TabletManager.UI.CommonControls;
using TabletManager.UI.Main.Tablets.Controls;

namespace TabletManager.UI.Controls
{
    public partial class Tablets : UserControl, ILocationAwareControl
    {
        public Tablets()
        {
            InitializeComponent();
            
            tabletDataGrid1.SelectionChanged
                .Where(x => !DesignMode)
                .Subscribe((x) => {
                    tabletHistoryGrid1.Data = CommonDatabase.CommonDbAccess.GetHistoryForTablets(x, tabletHistoryGrid1.From, tabletHistoryGrid1.To);
                    tabletInfo1.SetTablet(x);
                    bPrintBarcode.Enabled = x.Count() > 0;
                    bRemoteLock.Enabled = x.Count() > 0;
                });
            
            tabletHistoryGrid1.Filter
                .Where(x => !DesignMode)
                .Subscribe((f) => {
                    tabletHistoryGrid1.Data = CommonDatabase.CommonDbAccess.GetHistoryForTablets(tabletDataGrid1.SelectedTablets, f.minimum, f.maximum);
                });

            Observable
                .FromEventPattern(h => bLocationChange.Click += h, h => bLocationChange.Click -= h)
                .Subscribe((x) => {
                    if (tabletDataGrid1.SelectedTablets.Count() > 1)
                    {
                        MessageBox.Show("Proszę wybierz jeden tablet do zablokowania.");
                    }
                    else
                    {
                        var toChange = tabletDataGrid1.SelectedTablets.FirstOrDefault();
                        LocalizationChange lc = new LocalizationChange(toChange) ;
                        lc.ShowDialog();
                    }
                });

            Observable
                .FromEventPattern(h => bRemoteLock.Click += h, h => bRemoteLock.Click -= h)
                .Subscribe((x) => {
                    if (tabletDataGrid1.SelectedTablets.Count() > 1)
                    {
                        MessageBox.Show("Proszę wybierz jeden tablet do zablokowania.");
                    }
                    else
                    {
                        var toLock = tabletDataGrid1.SelectedTablets.FirstOrDefault();
                        if (toLock != null)
                        {
                            LockReason lr = new LockReason();
                            if (lr.ShowDialog() == DialogResult.OK)
                            {
                                CommonDatabase.CommonDbAccess.Lock(toLock, CommonDatabase.Data.LockType.REMOTE_LOCK, StateSingleton.Instance.AuthenticatedUser.Value.ACPno, lr.ReasonText, DateTime.Now);
                                tabletDataGrid1.RefreshData();
                            }
                        }
                    }
                });

            Observable
                .FromEventPattern(h => bPrintBarcode.Click += h, h => bPrintBarcode.Click -= h)
                .Subscribe((x) => {
                    
                });
        }

        public void SetTabletLocation(string location)
        {
            tabletDataGrid1.RefreshData(location);
        }

        private void bPrintBarcode_Click(object sender, EventArgs e)
        {
                    Printer p = new Printer();
                    string template = File.ReadAllText(Path.Combine("Resources", "tablet_barcode.html"));

                    List<Dictionary<string, string>> tabs = new List<Dictionary<string, string>>();

                    foreach (var tablet in tabletDataGrid1.SelectedTablets)
                    {
                        var now = DateTime.Now;

                        var tmp = new Dictionary<string, string>();
                        
                        (bool save, string barcode, string image) = BarcodeOperations.GenerateTabletBarcodeBase64(tablet);
                        int retry = 3;
                        while (save)
                        {
                            if (retry-- < 0)
                            {
                                MessageBox.Show("Generowanie kodu tabletu zakończone niepowodzeniem. Skontaktuj się z IT.");
                                return;
                            }
                            if (CommonDatabase.CommonDbAccess.SaveTabletBarcode(tablet, barcode))
                            {
                                break;
                            }
                            else
                            {
                                (save, barcode, image) = BarcodeOperations.GenerateTabletBarcodeBase64(tablet);
                            }
                        }
                        
                        tmp.Add("[BARCODE]", image);
                        tmp.Add("[HOSTNAME]", tablet.WindowsName);

                        tabs.Add(tmp);
                    }

                    p.SelectFiles(template, tabs);
                    p.ShowDialog();
        }
    }
}
