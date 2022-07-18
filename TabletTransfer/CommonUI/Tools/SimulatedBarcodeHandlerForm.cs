using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HidBarcodeHandler.BarcodeHandler;

namespace CommonUI.Tools
{
    public partial class SimulatedBarcodeHandlerForm : Form
    {
        private Subject<string> newBarcode = new Subject<string>();
        public IObservable<string> NewBarcode => this.newBarcode.AsObservable();
        public SimulatedBarcodeHandlerForm()
        {
            InitializeComponent();
            try
            {
                LoadHistory();
                SaveHistory();
            }
            catch (Exception ex)
            {
                historyFile = "";
            }
        }
        void SaveHistory()
        {
            if (historyFile != "")
            {
                var toSave = new List<string>();
                foreach (var item in listBox1.Items)
                {
                    toSave.Add(item.ToString());
                }
                File.WriteAllLines(historyFile, toSave.ToArray());
            }
        }
        void LoadHistory()
        {
            if (File.Exists(historyFile))
            {
                listBox1.Items.AddRange(File.ReadAllLines(historyFile));
            }
        }
        string historyFile = "sim_barcode_history.txt";
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Contains(textBox1.Text))
            {
                listBox1.Items.Remove(textBox1.Text);
            }
            listBox1.Items.Insert(0, textBox1.Text);
            newBarcode.OnNext(textBox1.Text);
            SaveHistory();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                button1_Click(null, null);
            }
        }

        private void SimulatedBarcodeHandlerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //WindowState = FormWindowState.Minimized;
        }

        private void SimulatedBarcodeHandlerForm_Load(object sender, EventArgs e)
        {
            TopLevel = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            //this.BringToFront();
            //this.TopMost = true;
            //this.Focus();
        }
    }
}
