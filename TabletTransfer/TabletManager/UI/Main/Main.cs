using CommonUI.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;
using TabletManager.UI;
using TabletManager.UI.Controls;

namespace TabletManager
{
    public partial class Main : Form
    {
        StateSingleton s;
        public Main()
        {
            s = StateSingleton.Instance;

            InitializeComponent();

            Text = $"TabletManager v.{Application.ProductVersion}";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            events1.UnackedExist.Subscribe((x) => {
                alertIcon1.Blink = x;
            });

            Observable
                .FromEventPattern(h => alertIcon1.Click += h, h => alertIcon1.Click -= h)
                .Subscribe((x) => {
                    tabControl1.SelectedTab = tabPage3;
                });
            
            Observable
                .FromEventPattern(h => bRefresh.Click += h, h => bRefresh.Click -= h)
                .Select(x => 0L)
                .StartWith(0L)
                .Merge(Observable.Timer(TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60)))
                .ObserveOn(this)
                .Subscribe((x) => {
                    ReloadLocations();
                });
            //ReloadLocations(); :-D

            userInfo1.SetOperator(StateSingleton.Instance.AuthenticatedUser);
            userInfo1.OperatorChangeRequested
                .ObserveOn(this)
                .Subscribe((x) => {
                    var tablets = CommonDatabase.CommonDbAccess.GetTablets(op: StateSingleton.Instance.AuthenticatedUser.Value);
                    if (tablets.Any())
                    {
                        var tmp1 = MessageBox.Show("Do twojego konta są przyporządkowane tablety.\n\nNadal za nie odpowiadasz.\n\nCzy chcesz je przekazać zmiennikowi?", "Uwaga!", MessageBoxButtons.YesNoCancel);
                        if (tmp1 == DialogResult.Yes)
                        {
                            AuthAndPassDevices tmp = new AuthAndPassDevices();
                            if (tmp.ShowDialog() == DialogResult.OK)
                            {
                                bool allOk = true;
                                foreach (var t in tablets)
                                {
                                    allOk &= CommonDatabase.CommonDbAccess.PassTheDevice(t, tmp.AuthenticatedUser, false, null, "MAGAZYN");//no need for transaction
                                }
                                if (allOk)
                                {
                                    StateSingleton.Instance.SetAuthenticatedUser(tmp.AuthenticatedUser);
                                    MessageBox.Show("OK, wszystko przekazane, nowy użytkownik zalogowany do systemu.");
                                }
                                else
                                {
                                    MessageBox.Show("Wystąpił problem systemowy z przekazaniem części tabletów. Zweryfikuj aktualny stan.");
                                }
                            }
                            else if (tmp.ShowDialog() == DialogResult.Retry)
                            {
                                MessageBox.Show("Przekazanie nie udane... wprowadzono złą ilość.");
                            }
                            else
                            {
                                MessageBox.Show("Przekazanie nie udane... (przerwane przez użytkownika)");
                            }
                            return;
                        }
                        if (tmp1 == DialogResult.No)
                        {
                            Application.Restart();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Brak tabletów do przekazania, nowy użytkownik może się zalogować");
                        Application.Restart();
                    }
                });
        }

        int selectedIndexXbLocation;
        private void ReloadLocations()
        {
            cbLocation.Items.Clear();
            cbLocation.Items.AddRange(CommonDatabase.CommonDbAccess.GetLocationsForSomeone(s.AuthenticatedUser.Value));
            //cbLocation.Text = s.Location;

            cbLocation.SelectedIndex = selectedIndexXbLocation;
            //cbLocation_SelectedIndexChanged(null, null);
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndexXbLocation = cbLocation.SelectedIndex;
            this.ProcessAllControls()
                .Where(x => x is ILocationAwareControl)
                .Select(x => x as ILocationAwareControl)
                .ToList()
                .ForEach(x => x.SetTabletLocation(cbLocation.Text));
            cbLocation.SelectedIndex = selectedIndexXbLocation;
        }

        private void bAddLocation_Click(object sender, EventArgs e)
        {
            var required = CommonDatabase.Data.Operator.PositionEnum.Service;
            if (!s.AuthenticatedUser.Value.IsAllowed(required))
            {
                MessageBox.Show($"Tylko osoby z uprawnieniem {required} mogą dodawać lokalizacje.");
            }
            else
            {
                LocationManager al = new LocationManager();
                if (al.ShowDialog() == DialogResult.OK)
                {
                    ReloadLocations();
                }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            ReloadLocations();
        }
    }
}
