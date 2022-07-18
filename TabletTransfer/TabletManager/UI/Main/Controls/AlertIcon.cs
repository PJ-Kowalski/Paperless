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

namespace TabletManager.UI.Controls
{
    public partial class AlertIcon : UserControl
    {
        public bool Blink { get; set; } = false;
        public AlertIcon()
        {
            InitializeComponent();
            Observable
                .FromEventPattern(h => label1.Click += h, h => label1.Click -= h)
                .Subscribe((x) => {
                    this.OnClick(x.EventArgs as EventArgs);
                });
        }

        private bool on = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Blink)
            {
                label1.Text = "Brak alertów";
                label1.BackColor = SystemColors.Control;
                label1.ForeColor = SystemColors.ControlText;
            }
            else
            {
                label1.Text = "Uwaga!";

                if (on)
                {
                    label1.BackColor = Color.Red;
                    label1.ForeColor = Color.White;
                }
                else
                {
                    label1.BackColor = Color.White;
                    label1.ForeColor = Color.Red;
                }
                on = !on;
            }
        }
    }
}
