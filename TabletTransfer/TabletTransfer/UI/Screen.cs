using HidBarcodeHandler;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows.Forms;
using TabletTransfer.AI;
using static HidBarcodeHandler.BarcodeHandler;

namespace TabletTransfer.UI
{
    public class MyScreen : Form
    {
        
        public MyScreen() : base()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                StateSingleton.Instance.BarcodeHandlerMessageReceived
                    .Where(x => this.Visible)
                    .Where(x => StateSingleton.Instance.BarcodeIsSim || Form.ActiveForm == this)
                    .Subscribe((x) => BarcodeHandler_MessageReceived(null, x));
            }
        }

        protected virtual void BarcodeHandler_MessageReceived(object source, HidBarcodeHandler.HidBarcodeHandler.MessageReceivedEventArgs e)
        { 
            //do nothing by default...
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyScreen
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MyScreen";
            this.Load += new System.EventHandler(this.MyScreen_Load);
            this.ResumeLayout(false);

        }

        private void MyScreen_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (StateSingleton.Instance.BarcodeIsSim)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            AddKeyboardShowerToTextBox(Controls);
        }

        private void AddKeyboardShowerToTextBox(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    (control as TextBox).Click += MyScreen_Click;
                }
                if (control.Controls.Count > 0)
                {
                    AddKeyboardShowerToTextBox(control.Controls);
                }
            }
        }

        private void MyScreen_Click(object sender, EventArgs e)
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\TabletTip\\1.7");

            registryKey?.SetValue("KeyboardLayoutPreference", 0, RegistryValueKind.DWord);
            registryKey?.SetValue("LastUsedModalityWasHandwriting", 1, RegistryValueKind.DWord);

            //Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }
    }
}