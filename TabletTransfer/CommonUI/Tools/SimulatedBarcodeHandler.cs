using HidBarcodeHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CommonUI.Tools
{
    public class SimulatedBarcodeHandler : BarcodeHandler
    {
        public SimulatedBarcodeHandler()
        {
            IsSim = true;
        }
        SimulatedBarcodeHandlerForm ui = null;
        public override void Close()
        {
            ui.Invoke(new Action(() => {
                ui.Close();
            }));
        }

        public override void Init(IntPtr windowHandle)
        {
            Task.Run(() => {
                ui = new SimulatedBarcodeHandlerForm();
                
                ui.NewBarcode.Subscribe((barcode) => {
                    RaiseMessageReceived(this, new MessageReceivedEventArgs() { Content = barcode });
                });

                ui.ShowDialog();
            });
        }
    }
}
