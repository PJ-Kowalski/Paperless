using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadingWindow
{
    public class LoadingIndicator
    {
        Loading loadingForm;
        Thread loadingThread;
        public  void Show()
        {
            loadingThread = new Thread(new ThreadStart(LoadingProcess));
            loadingThread.Start();

        }


        public void Close()
        {
            if (loadingForm!=null)
            {
                loadingForm.Invoke(new System.Threading.ThreadStart(loadingForm.CloaseLoadingForm));
                loadingForm = null;
                loadingThread = null;
            }
        }



        private void LoadingProcess()
        {
            loadingForm = new Loading();
            loadingForm.ShowDialog();
        }

    }
}
