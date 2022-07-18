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
    public partial class AlertsGrid : UserControl
    {
        List<Alert> data = new List<Alert>();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<Alert> Data { get { return data; } set { FillDgv(value); } }
        public IObservable<(DateTime minimum, DateTime maximum)> Filter => customDateRange1.Filter;
        public DateTime From { get => customDateRange1.Minimum; }
        public DateTime To { get => customDateRange1.Maximum; }

        private void FillDgv(List<Alert> tablets)
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
                dataGridView1.Rows.Add(t.GetDefaultRow());
                dataGridView1.Rows[dataGridView1.Rows.Count-1].Tag = t;
            }
        }
        public AlertsGrid()
        {
            InitializeComponent();
            Observable
                .FromEventPattern<DataGridViewRowsAddedEventArgs>(dataGridView1, "RowsAdded").Subscribe((x) => {
                    var dgv = x.Sender as DataGridView;
                    var cell = dgv.Rows[x.EventArgs.RowIndex].Cells["AckButton"] as DataGridViewButtonCell;
                    cell.Value = "OK";
                });
            
            Observable
                .FromEventPattern<DataGridViewCellEventArgs>(dataGridView1, "CellClick").Subscribe((x) => {
                    var dgv = x.Sender as DataGridView;
                    var row = dgv.Rows[x.EventArgs.RowIndex];
                    var cell = row.Cells[x.EventArgs.ColumnIndex] as DataGridViewButtonCell;
                    var payload = row.Tag as Alert;
                    if (cell != null && dgv.Columns[x.EventArgs.ColumnIndex].Name == "AckButton" && payload != null)
                    {
                        if (!payload.AckDate.HasValue)
                        {
                            row.Tag = CommonDatabase.CommonDbAccess.AckAlert(payload, StateSingleton.Instance.AuthenticatedUser.Value);
                            row.Cells["Ack"].Value = StateSingleton.Instance.AuthenticatedUser.Value;
                            row.Cells["AckDate"].Value = DateTime.Now;
                        }
                    }
                });

        }
    }
}
