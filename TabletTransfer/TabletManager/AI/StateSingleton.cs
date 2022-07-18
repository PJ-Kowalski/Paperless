using CommonDatabase.Data;
using CommonUI.Tools;
using HidBarcodeHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HidBarcodeHandler.BarcodeHandler;

namespace TabletManager.AI
{
    public sealed partial class StateSingleton
    {
        #region Singleton
        private static StateSingleton instance;
        private static object instance_lock = new object();
        static StateSingleton()
        {
            
        }
        public static StateSingleton Instance
        {
            get
            {
                lock (instance_lock)
                {
                    if (instance == null)
                    {
                        instance = new StateSingleton();
                    }
                }
                return instance;
            }
        }
        #endregion Singleton
        public BehaviorSubject<Operator> AuthenticatedUser { get; set; } = new BehaviorSubject<Operator>(null);
        
        public void SetAuthenticatedUser(Operator op)
        {
            AuthenticatedUser.OnNext(op);
        }
        public string Location { get; set; }
        
        BarcodeHandler barcode;
        public void Init(Form dock_Window, bool sim_barcode)
        {
            if (sim_barcode)
            {
                barcode = new SimulatedBarcodeHandler();
            }
            else
            {
                barcode = new HidBarcodeHandler.HidBarcodeHandler();
            }
            barcode.Init(dock_Window.Handle);
            barcode.LetterTimeout = 50;
            barcode.AllowedDevices.Add(@"\\?\HID#VID_0C2E");
            barcode.MessageReceived += Barcode_MessageReceived;
        }

        internal void Close()
        {
            barcode?.Close();
        }
        Subject<MessageReceivedEventArgs> barcodeHandlerMessageReceived = new Subject<MessageReceivedEventArgs>();
        public IObservable<MessageReceivedEventArgs> BarcodeHandlerMessageReceived => barcodeHandlerMessageReceived.AsObservable();

        public bool BarcodeIsSim { get { return barcode.IsSim; } }
        private void Barcode_MessageReceived(object source, BarcodeHandler.MessageReceivedEventArgs e)
        {
            barcodeHandlerMessageReceived.OnNext(e);
        }
    }
}
