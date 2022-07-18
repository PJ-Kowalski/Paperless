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

namespace TabletManager.UI.Main.Tablets.Controls
{
    public partial class LockReason : Form
    {
        public const int MaxLen = 256;
        public string ReasonText { get { return textBox1.Text; } }
        public LockReason()
        {
            InitializeComponent();
            
            textBox1.MaxLength = MaxLen;

            textBox1_TextChanged(null, null);
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = $"({textBox1.Text.Length}/{MaxLen})";
        }
    }
}
