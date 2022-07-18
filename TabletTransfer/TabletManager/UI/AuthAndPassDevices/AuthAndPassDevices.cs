using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;

namespace TabletManager
{
    public partial class AuthAndPassDevices : Form
    {
        BehaviorSubject<int> ExpectedTabletCount = new BehaviorSubject<int>(-1);
        public AuthAndPassDevices()
        {
            InitializeComponent();

            Text = $"TabletManager v.{Application.ProductVersion}";

            DialogResult = DialogResult.Cancel;

            tabletPickup1.SetExpectedTabletCount(ExpectedTabletCount);

            Observable
                .FromEventPattern(h => bRefresh.Click += h, h => bRefresh.Click -= h)
                .StartWith(new EventPattern<object>(null,null))
                .Subscribe((x) => {
                    tabletDataGrid1.RefreshData();
                });

            substituteAuth1.AuthenticatedUser
                .Subscribe((x) => {
                    if (x?.PasswordVerified == Operator.PasswordVerificationResult.OK)
                    {
                        ExpectedTabletCount.OnNext(tabletDataGrid1.TabletsCount);
                    }
                    else
                    {
                        ExpectedTabletCount.OnNext(-1);
                    }
                });

            tabletPickup1.OkClicked
                .Subscribe((verification_ok) => {
                    if (verification_ok)
                    {
                        DialogResult = DialogResult.OK;
                    }
                    this.Close();
                });
        }
        public Operator AuthenticatedUser { get => substituteAuth1.AuthenticatedUser.Value; }
    }
}
