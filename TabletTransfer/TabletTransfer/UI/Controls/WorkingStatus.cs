using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletTransfer.AI;

namespace TabletTransfer.UI.Controls
{
    public partial class WorkingStatus : UserControl
    {
        public WorkingStatus()
        {
            InitializeComponent();
        }

        private string _labelText;

        public string LabelText
        {
            get { return _labelText; }
            set { _labelText = value;
            }
        }


        public void SetStatus( string text, bool blink)
        {
            label1.Text = text;
            Blink = blink;
        }

        public bool Blink { get; set; }
        private bool on = false;
        public int n { get; set; }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Blink)
            {
                progressBar1.Value = n++;
                if (n>=100)
                {
                    n = 0;
                }
                on = !on;
            }

        }
    }
}
