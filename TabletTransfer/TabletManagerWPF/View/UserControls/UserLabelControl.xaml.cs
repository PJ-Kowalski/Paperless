using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TabletManagerWPF.View
{
    /// <summary>
    /// Interaction logic for UserLabel.xaml
    /// </summary>
    public partial class UserLabelControl : UserControl
    {


        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(String), typeof(UserLabelControl), new PropertyMetadata("użytkownik"));



        public String Position
        {
            get { return (String)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(String), typeof(UserLabelControl), new PropertyMetadata("uprawnienia"));




        public string Region
        {
            get { return (string)GetValue(RegionProperty); }
            set { SetValue(RegionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Region.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionProperty =
            DependencyProperty.Register("Region", typeof(string), typeof(UserLabelControl), new PropertyMetadata("dział"));




        public BitmapImage Photo
        {
            get { return (BitmapImage)GetValue(PhotoProperty); }
            set { SetValue(PhotoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Photo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhotoProperty =
            DependencyProperty.Register("Photo", typeof(BitmapImage), typeof(UserLabelControl), new PropertyMetadata(null));






        public UserLabelControl()
        {
            InitializeComponent();

        }
    }
}
