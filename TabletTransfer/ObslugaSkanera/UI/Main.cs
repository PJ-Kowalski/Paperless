using HidBarcodeHandler;
using ObslugaSkanera.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObslugaSkanera
{
    public partial class Main : Form
    {
        List<RadioButton> radios = new List<RadioButton>();
        public Main()
        {
            barcode = new HidBarcodeHandler.HidBarcodeHandler();
            InitializeComponent();

            radios.AddRange(new[] { radioButton1, radioButton2, radioButton3 });

            Text += $" v. {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }
        BarcodeHandler barcode;
        private void Main_Load(object sender, EventArgs e)
        {
            barcode.Init(this.Handle);
            barcode.LetterTimeout = 50;
            barcode.AllowedDevices.Add(@"\\?\HID#VID_0C2E");
            barcode.MessageReceived += Barcode_MessageReceived;

            var serials = SerialPort.GetPortNames();
            //comboBox1.Items.AddRange(serials);
            //comboBox2.Items.AddRange(serials);

            windowChooser1.OnChoosenWindowSelected += WindowChooser1_OnChoosenWindowSelected;
        }

        private void Barcode_MessageReceived(object source, HidBarcodeHandler.HidBarcodeHandler.MessageReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { Barcode_MessageReceived(source, e); }));
                return;
            }
            var active = WinAPI.GetActiveWindowTitle();
            
            label3.Text = active;
            if (windowChooser1.ChoosenWindowTitle == active)
            {
                Console.Beep();
            }
        }

        private void WindowChooser1_OnChoosenWindowSelected(object sender, string title)
        {
            label4.Text = $"[{title}]";
        }
        
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                radios.Where(x => x != sender).ToList().ForEach(x => x.Checked = false);
            }
        }
    }
}
