using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletTransfer.AI;
using TabletTransfer.AI.Tools;
using TabletTransfer.UI;
using HidBarcodeHandler;
using CommonDatabase.Data;
using System.Reactive.Linq;
using Oracle.ManagedDataAccess.Client;

namespace TabletTransfer
{
    public partial class Docked : MyScreen
    {
        public Docked()
        {
            InitializeComponent();

            Hide();
        }

        private void Docked_Load(object sender, EventArgs e)
        {
            Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2 - Width / 2, 0);
            WindowState = FormWindowState.Normal;

            Hide();
            
            StateSingleton.Instance.CurrentUserChanged = new Action<Operator>((op) => {
                Invoke(new Action(() => {
                    if (op != null)
                    {
                        label1.Text = $"{op.Name} ({op.ACPno})\n{op.Location}";
                        bStorage.Visible = op.IsAllowed(Operator.PositionEnum.SL);
                    }
                }));
            });
            Observable
                .FromEventPattern(h => bStorage.Click += h, h => bStorage.Click -= h)
                .Subscribe((x) =>
                {
                    StateSingleton.Instance.PutInStorage();
                    OracleConnection.ClearAllPools();
                    this.Hide();
                });
        }
        private void Docked_FormClosing(object sender, FormClosingEventArgs e)
        {
            StateSingleton.Instance.DoShutdown();
        }
        #region mouse management
        Point moveStart;
        bool isMoving;
        private void Docked_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMoving == false)
            {
                moveStart = e.Location;
                isMoving = true;
            }
        }
        private void Docked_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                var newPos = e.X + Location.X - moveStart.X;
                if (newPos < 0)
                {
                    newPos = 0;
                }
                var x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                if (newPos + Size.Width > x)
                {
                    newPos = x - Size.Width;
                }
                Location = new Point(newPos, Location.Y);
                //moveStart = new Point(e.X, e.Y);
                Size = new Size(Size.Width, 40);
                label1.Visible = true;
            }
        }
        private void Docked_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }
        private void Docked_MouseLeave(object sender, EventArgs e)
        {
            Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => {
                Invoke(new Action(() => { 
                    Size = new Size(Size.Width, 5);
                    label1.Visible = false;
                }));
            });
        }
        private void Docked_MouseEnter(object sender, EventArgs e)
        {
            Size = new Size(Size.Width, 40);
            label1.Visible = true;
        }
        #endregion mouse management
        private void bPassDevice_Click(object sender, EventArgs e)
        {
            this.Hide();
            StateSingleton.Instance.DoPass();
            //this.Show();
        }
    }
}
