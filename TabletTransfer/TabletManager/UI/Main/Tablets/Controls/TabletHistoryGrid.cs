using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;
using System.Reactive.Subjects;

namespace TabletManager.UI.Controls
{
    public partial class TabletHistoryGrid : UserControl
    {
        List<TabletEvent> data = new List<TabletEvent>();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<TabletEvent> Data { get { return data; } set { FillDgv(value); } }
        public IObservable<(DateTime minimum, DateTime maximum)> Filter => customDateRange1.Filter;
        public DateTime From { get => customDateRange1.Minimum; }
        public DateTime To { get => customDateRange1.Maximum; }
        private void FillDgv(List<TabletEvent> tablets)
        {
            if (tablets != null)
            {
                if (!data.SequenceEqual(tablets))
                {
                    data = tablets;

                    RefreshData();
                }
            }
        }
        private void RefreshData()
        {
            dataGridView1.Rows.Clear();

            foreach (var t in data)
            {
                if (!cbOnlyPass.Checked || t.EventType == "TABLET_RECEIVED")
                {
                    dataGridView1.Rows.Add(t.GetDefaultRow());
                    dataGridView1.Rows[dataGridView1.Rows.Count-1].Tag = t;
                }
            }
                    dataGridView1.Sort(dataGridView1.Columns["CreationDate"], ListSortDirection.Descending);
        }
        public TabletHistoryGrid()
        {
            InitializeComponent();
        }

        private void cbOnlyPass_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
