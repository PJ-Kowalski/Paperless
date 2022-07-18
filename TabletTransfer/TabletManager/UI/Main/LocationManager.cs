using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabletManager.UI
{
    public partial class LocationManager : Form
    {
        public LocationManager()
        {
            InitializeComponent();
        }

        private void LocationManager_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (var loc in CommonDatabase.CommonDbAccess.GetAllLocations())
            {
                dataGridView1.Rows.Add(loc.GetDefaultRow());
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Tag = loc;
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            foreach (var loc in toDelete)
            {
                if (!CommonDatabase.CommonDbAccess.DeleteLocation(loc))
                {
                    MessageBox.Show($"Nastąpił problem z usunięciem rekordu [{loc.Name}, {loc.Position}].");
                    return;
                }
            }
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                var loc = r.Tag as CommonDatabase.Data.Location;
                if (loc == null && (r.Cells["NameCol"].Value!=null))
                {

                    var name = ((string)r.Cells["NameCol"].Value).Trim();
                    var position = r.Cells["PositionCol"].Value.ToString();

                    if (!CommonDatabase.CommonDbAccess.GetAllLocations().Any(x => x.Name == name))
                    {
                        if (position == "" || !CommonDatabase.CommonDbAccess.AddLocation(name, int.Parse(position)))
                        {
                            MessageBox.Show($"Nastąpił problem z zapisem rekordu [{name}, {position}].");
                            return;
                        }
                    }
                }
        
               
                else if (loc != null && loc.Dirty)
                {
                    var new_name = ((string)r.Cells["NameCol"].Value).Trim();
                    var new_position = r.Cells["PositionCol"].Value.ToString();
                    if (new_position == "" || !CommonDatabase.CommonDbAccess.UpdateLocation(loc, new_name, int.Parse(new_position)))
                    {
                        MessageBox.Show($"Nastąpił problem z modyfikacją rekordu [{loc.Name} -> {new_name}, {loc.Position} -> {new_position}].");
                        return;
                    }
                }
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var loc = dataGridView1.Rows[e.RowIndex].Tag as CommonDatabase.Data.Location;

                if (loc != null)
                {
                    loc.Dirty = true;
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "PositionCol")
            {
                int ret;
                e.Cancel = e.FormattedValue.ToString() != "" && !int.TryParse(e.FormattedValue.ToString(), out ret);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bRemove.Enabled = dataGridView1.SelectedCells.Count > 0;
        }
        List<Location> toDelete = new List<Location>();
        private void bRemove_Click(object sender, EventArgs e)
        {
            List<int> rowsDone = new List<int>();
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();
            foreach (DataGridViewCell c in dataGridView1.SelectedCells)
            {
                if (!rowsDone.Contains(c.RowIndex))
                {
                    var loc = c.OwningRow.Tag as Location;
                    var name = loc?.Name;
                    if (name == null)
                    {
                        name = c.OwningRow.Cells["NameCol"].Value.ToString();
                    }
                    rowsDone.Add(c.RowIndex);
                    if (MessageBox.Show($"Czy na pewno chcesz usunąć lokalizację {name} ?", "Uwaga!", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    {
                        rowsToDelete.Add(c.OwningRow);
                        if (loc != null)
                        {
                            toDelete.Add(loc);
                        }
                    }
                }
            }
            foreach (var r in rowsToDelete)
            {
                dataGridView1.Rows.Remove(r);
            }
        }
    }
}
