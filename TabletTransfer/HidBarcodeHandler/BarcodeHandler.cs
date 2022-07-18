using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidBarcodeHandler
{
    public abstract class BarcodeHandler
    {
        #region Events
        public class MessageReceivedEventArgs
        {
            public string Content { get; set; }
        }
        public delegate void MessageReceivedEvent(object source, MessageReceivedEventArgs e); 
        protected void RaiseMessageReceived(BarcodeHandler hidBarcodeHandler, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            MessageReceived?.Invoke(hidBarcodeHandler, messageReceivedEventArgs);
        }
        #endregion Events
        public event MessageReceivedEvent MessageReceived;

        public static bool ShouldIgnore = false;
        public int LetterTimeout { get; set; } = 100;
        public List<string> AllowedDevices { get; set; } = new List<string>();

        public abstract void Init(IntPtr windowHandle);
        public abstract void Close();

        public bool IsSim { get; set; } = false;
    }
}
