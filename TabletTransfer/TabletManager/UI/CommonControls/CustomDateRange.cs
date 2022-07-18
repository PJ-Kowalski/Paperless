using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace TabletManager.UI.Controls
{
    public partial class CustomDateRange : UserControl
    {
        public bool HideTo { get { return !dateTimePicker2.Visible; } set { dateTimePicker2.Visible = value; } }
        
        Subject<(DateTime minimum, DateTime maximum)> filter = new System.Reactive.Subjects.Subject<(DateTime minimum, DateTime maximum)>();
        public IObservable<(DateTime minimum, DateTime maximum)> Filter => filter.AsObservable();
        List<RadioButton> rbs = new List<RadioButton>();
        public CustomDateRange()
        {
            InitializeComponent();

            var now = DateTime.Now;
            dateTimePicker1.Value = now.Date.AddDays(-30);
            dateTimePicker2.Value = now.Date.AddDays(1);

            rbs.Add(rb1);
            rbs.Add(rb2);
            rbs.Add(rb3);
            rbs.Add(rb4);
            rbs.Add(rb5);

            rb1.Tag = TimeSpan.FromHours(1);
            rb2.Tag = TimeSpan.FromHours(12);
            rb3.Tag = TimeSpan.FromHours(24);
            rb4.Tag = TimeSpan.FromHours(24*7);
            rb5.Tag = TimeSpan.MaxValue;

            rbs.ForEach(x => rbx_CheckedChanged(x, null));
        }

        private void rbx_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = rb5.Checked;
            dateTimePicker2.Enabled = rb5.Checked;
            
            filter.OnNext((Minimum, Maximum));
        }
        public DateTime Minimum
        {
            get
            {
                var ret = rbs.Where(x => x.Checked).Select(x => (TimeSpan)x.Tag).FirstOrDefault();
                if (ret == TimeSpan.MaxValue)
                {
                    return dateTimePicker1.Value;
                }
                return DateTime.Now - ret;
            } 
        }
        public DateTime Maximum
        {
            get
            {
                if (HideTo)
                {
                    return DateTime.MaxValue;
                }
                else
                {
                    var ret = rbs.Where(x => x.Checked).Select(x => (TimeSpan)x.Tag).FirstOrDefault();
                    if (ret == TimeSpan.MaxValue)
                    {
                        return dateTimePicker2.Value;
                    }
                    return DateTime.Now;
                }
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (rb5.Checked)
            {
                filter.OnNext((Minimum, Maximum));
            }
        }
    }
}
