using RawInput_dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HidBarcodeHandler
{
    public sealed class HidBarcodeHandler : BarcodeHandler
    {
        /// <summary>
        /// MessageFilter is a bit fucked - we need to inform it if we want to send given character to message queue,
        /// but we have (at this stage) no information from where it came...
        /// but. it is true, that if we have incoming keyboard state and we know from whitch keyboard it is, we can assume
        /// that nex incoming character might come from this source...
        /// 
        /// UGLY HACK it is.
        /// 
        /// let's hope noone is using true keyboard and barcode reader simultanously.
        /// </summary>
        private class MyMessageFilter : IMessageFilter
        {
            // true  to filter the message and stop it from being dispatched 
            // false to allow the message to continue to the next filter or control.
            public bool PreFilterMessage(ref Message m)
            {
                //return m.Msg == Win32.WM_KEYDOWN;
                return m.Msg == Win32.WM_KEYDOWN && ShouldIgnore;
            }
        }

        private RawInput rawInput;
        public override void Init(IntPtr windowHandle)
        {
            rawInput = new RawInput(windowHandle, false);

            rawInput.AddMessageFilter(new MyMessageFilter());
            Win32.DeviceAudit();

            rawInput.KeyPressed += RawInput_KeyPressed;
        }
        public override void Close()
        {
            rawInput.KeyPressed -= RawInput_KeyPressed;
        }

        string buf = "";
        object buf_lock = new object();
        DateTime lastPush = DateTime.MaxValue;
        byte[] keyboardState = new byte[256];
        private void RawInput_KeyPressed(object sender, RawInputEventArg e)
        {
            if (AllowedDevices.Any(x => e.KeyPressEvent.DeviceName.StartsWith(x)))
            {
                var key = e.KeyPressEvent.RawVKey;
                if (e.KeyPressEvent.Message == KeyPressEvent.MessagesEnum.WM_KEYDOWN)//press
                {
                    keyboardState[(int)key] = 0xff;

                    var tmp = new StringBuilder(256);
                    Interop.ToUnicode((uint)key, 0, keyboardState, tmp, 256, 0);//! flags?, scan code?

                    buf += tmp.ToString();
                    lastPush = DateTime.Now;

                    Task.Delay(TimeSpan.FromMilliseconds(LetterTimeout)).ContinueWith(o =>
                    {
                        lock (buf_lock)
                        {
                            //if after last character elapsed enaught time, test call my event
                            if (buf != "" && DateTime.Now - lastPush > TimeSpan.FromMilliseconds(LetterTimeout))
                            {
                                //Debug.WriteLine($"\n\n{buf}\n\n");

                                RaiseMessageReceived(this, new MessageReceivedEventArgs() { Content = buf });

                                buf = "";
                            }
                        }
                    });
                    ShouldIgnore = true;
                }
                else if (e.KeyPressEvent.Message == KeyPressEvent.MessagesEnum.WM_KEYUP)//depress
                {
                    keyboardState[(int)key] = 0x00;
                }

                //Debug.WriteLine($"{(e.KeyPressEvent.Message == KeyPressEvent.MessagesEnum.WM_KEYDOWN ? "+" : "-")} {e.KeyPressEvent.VKeyName} ({buf})");
            }
            else
            {
                ShouldIgnore = false;
            }
        }
    }
}
