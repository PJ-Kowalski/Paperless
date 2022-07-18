using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadingWindow
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public Loading(Form parent)
        {
            InitializeComponent();
            if (parent != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(parent.Location.X + parent.Width / 2 - this.Width / 2, parent.Location.Y + parent.Height / 2 - this.Height / 2);
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterParent;
            }
        }

        public void CloaseLoadingForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        int n;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (n<100)
            {
                progressBar1.Value = n;
                n++;
            }
            else
            {
                n = 0;
            }
        }
    }
}
