using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabletManager.UI.CommonControls
{
    public partial class Printer : Form
    {
        public Printer()
        {
            InitializeComponent();
            
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            Observable
                .Merge(
                    Observable.FromEventPattern(h => button1.Click += h, h => button1.Click -= h)
                )
                .Merge(
                    Observable.FromEventPattern(h => button2.Click += h, h => button2.Click -= h)
                )
                .Where(x => x.Sender is Button)
                .Select(x => (x.Sender as Button).Tag as string)
                .Subscribe((x) => {
                    try
                    {
                        int diff = int.Parse(x);
                        if (current + diff < pagination.Count && current + diff >= 0)
                        {
                            current += diff;
                        }
                        RefreshPagination();
                    }
                    catch (Exception ex)
                    { }
                });
            ;
        }

        internal void SelectFile(string content, Dictionary<string, string> toReplace)
        {
            pagination = new List<Dictionary<string, string>>();
            pagination.Add(toReplace);
            template = content;

            current = pagination.Count() > 0 ? 0 : -1;
            RefreshPagination();
            
        }
        private List<Dictionary<string, string>> pagination = new List<Dictionary<string, string>>();
        int current = -1;
        string template = "";
        internal void SelectFiles(string content, List<Dictionary<string, string>> toReplace)
        {
            pagination = toReplace;
            template = content;
            
            current = pagination.Count() > 0 ? 0 : -1;
            
            RefreshPagination();
        }

        private void RefreshPagination()
        {
            textBox1.Text = "1";
            textBox1.Enabled = pagination.Count() > 1;
            checkBox1.Enabled = pagination.Count() > 1;
            textBox2.Text = pagination.Count().ToString();
            button1.Enabled = pagination.Count() > 1;
            button2.Enabled = pagination.Count() > 1;

            string content = template;
            if (current >= 0)
            {
                foreach (var sth in pagination[current])
                {
                    content = content.Replace(sth.Key, sth.Value);
                }
                bPrint.Enabled = false;
            }
            else
            {
                content = "";
            }
            
            var tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, content);

            this.webBrowser1.Url = new Uri($"file:///{tmp}");
        }
        bool autoPrint = false;
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            bPrint.Enabled = true;
            if (autoPrint)
            {
                webBrowser1.Print();
                autoPrint = false;
            }
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                webBrowser1.Print();
            }
            else
            {
                var current_page = current;
                for (int i = 0; i < pagination.Count; i++)
                {
                    current = i;
                    autoPrint = true;
                    RefreshPagination();
                    while (autoPrint != false)
                    {
                        Application.DoEvents();
                        Thread.Sleep(100);
                    }
                }
                current = current_page;
                RefreshPagination();
            }
        }
    }
}
