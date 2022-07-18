using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TabletManagerWPF.ValueConverters;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TabletManagerWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for TabletControl.xaml
    /// </summary>
    public partial class TabletControl : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CommonDatabase.Data.Tablet tablet
        {
            get { return (CommonDatabase.Data.Tablet)GetValue(tabletProperty); }
            set { SetValue(tabletProperty, value); }
        }

        // Using a DependencyProperty as the backing store for tablet.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty tabletProperty =
            DependencyProperty.Register("tablet", typeof(CommonDatabase.Data.Tablet), typeof(TabletControl),
                                        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(UpdateData)));

        public static async void UpdateData(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TabletControl tabletControl = d as TabletControl;
            if (tabletControl.tablet != null)
            {
                string name1 = tabletControl.tablet.Responsible.Name;
                string name2 = tabletControl.tablet.LockedBy.Name;
                object photo1 = new object(), photo2 = new object(); ;
                Task p1 = new Task(() => { photo1 = CommonDatabase.Tools.PhotoDownloader.GetPhoto(name1); });
                Task p2 = new Task(() => { photo2 = CommonDatabase.Tools.PhotoDownloader.GetPhoto(name2); });
                p1.Start();
                p2.Start();
                await p1;
                await p2;
                Task.WaitAll(p1, p2);
                Task t = new Task(() =>
                {
                    tabletControl.Dispatcher.Invoke(() =>
                    {
                        tabletControl.PhotoResponsible.Source = tabletControl.Convert(photo1);
                        tabletControl.PhotoLockedBy.Source = tabletControl.Convert(photo2);
                        tabletControl.WindowsName.Parameter = "Nazwa";
                        tabletControl.WindowsName.Value = tabletControl.tablet.WindowsName;
                        tabletControl.SystemAdded.Parameter = "Data dodania";
                        tabletControl.SystemAdded.Value = tabletControl.tablet.CreationDate.ToString();
                        tabletControl.Responsible.Parameter = "Odpowiedzialny";
                        tabletControl.Responsible.Value = tabletControl.tablet.Responsible.Name;
                        tabletControl.LastChange.Parameter = "Ostatnia zmiana";
                        tabletControl.LastChange.Value = tabletControl.tablet.LastChange.ToString();
                        tabletControl.Bloked.Parameter = "Blokada?";
                        tabletControl.Bloked.Value = tabletControl.tablet.Locked.ToString();
                        tabletControl.BlockType.Parameter = "Rodzaj blokady";
                        tabletControl.BlockType.Value = tabletControl.tablet.LockType.Name;
                        tabletControl.Blockedtime.Parameter = "Czas blokady";
                        tabletControl.Blockedtime.Value = tabletControl.tablet.LockedOn.ToString();
                        tabletControl.BlockedBy.Parameter = "Zablokowany przez";
                        tabletControl.BlockedBy.Value = tabletControl.tablet.LockedBy.Name;

                    });
                });
                t.Start();
                await t;
            }
        }


        public void UpdateView()
        {
            PhotoResponsible.Source = Convert(CommonDatabase.Tools.PhotoDownloader.GetPhoto(tablet.Responsible.Name));
            PhotoLockedBy.Source = Convert(CommonDatabase.Tools.PhotoDownloader.GetPhoto(tablet.LockedBy.Name));
        }


        public TabletControl()
        {
            if (tablet!=null)
            {
                PhotoResponsible.Source = Convert(CommonDatabase.Tools.PhotoDownloader.GetPhoto(tablet.Responsible.Name));
                PhotoLockedBy.Source = Convert(CommonDatabase.Tools.PhotoDownloader.GetPhoto(tablet.LockedBy.Name));
            }
            InitializeComponent();
        }

        public BitmapImage Convert(object value)
        {
            System.Drawing.Image image = value as System.Drawing.Image;
            if (image != null)
            {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                return bi;
            }
            return null;
        }
    }

    public class GridItem
    {
        string parameter;
        string value;

        public GridItem(string parameter, string value)
        {
            this.parameter = parameter;
            this.value = value;
        }
    }
}
