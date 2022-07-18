using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TabletManagerWPF.View
{
    /// <summary>
    /// Interaction logic for Printer.xaml
    /// </summary>
    public partial class Printer : Window
    {
        public Printer(string html)
        {
            InitializeComponent();
            WebBrowser.NavigateToString(html);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mshtml.IHTMLDocument2 doc = WebBrowser.Document as mshtml.IHTMLDocument2;
            doc.execCommand("Print", true, null);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mshtml.IHTMLDocument2 doc = WebBrowser.Document as mshtml.IHTMLDocument2;
            doc.execCommand("Print", true, null);
        }
    }
}
