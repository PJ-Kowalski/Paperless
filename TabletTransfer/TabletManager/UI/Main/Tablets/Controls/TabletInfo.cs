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

namespace TabletManager.UI.Controls
{
    public partial class TabletInfo : UserControl
    {
        public TabletInfo()
        {
            InitializeComponent();

            Clear();
        }
        public void Clear()
        {
            dataGridView1.Rows.Clear();
            lPlaceholder.Visible = true;
            lPlaceholder.Text = "Brak danych do pokazania.";
            lPlaceholder.Dock = DockStyle.Fill;
            lPlaceholder.BringToFront();
        }
        public void SetTablet(IEnumerable<Tablet> tablets)
        {
            Clear();

            if (!tablets.Any())
            {
                lPlaceholder.Text = "Wybierz tablet";
            }
            else if (tablets.Take(2).Count() == 2)
            {
                lPlaceholder.Text = "Aby zobaczyć szczegóły tabletu, wybierz jeden tablet";
            }
            else
            {
                lPlaceholder.Visible = false;

                var t = tablets.FirstOrDefault();

                if (t != null)
                {
                    dataGridView1.Rows.Add(new object[] { "Nazwa", t.WindowsName });
                    dataGridView1.Rows.Add(new object[] { "Kod", t.Barcode });
                    dataGridView1.Rows.Add(new object[] { "Data dodania do systemu", t.CreationDate });
                    dataGridView1.Rows.Add(new object[] { "Osoba odpowiedzialna", t.Responsible.ToString() });
                    dataGridView1.Rows.Add(new object[] { "Ostatnia zmiana", t.LastChange });
                    if (t.Locked)
                    {
                        dataGridView1.Rows.Add(new object[] { "Blokada?", "TAK" });
                        dataGridView1.Rows.Add(new object[] { "Rodzaj blokady", $"{t.LockType} ({t.LockType.Description})" });
                        dataGridView1.Rows.Add(new object[] { "Przyczyna blokady", t.LockedReason });
                        dataGridView1.Rows.Add(new object[] { "Czas zablokowania", t.LockedOn });
                        dataGridView1.Rows.Add(new object[] { "Zablokowane przez", t.LockedBy });
                        gbLockedBy.Visible = true;
                        operatorBox2.Set(t.LockedBy);
                    }
                    else
                    {
                        dataGridView1.Rows.Add(new object[] { "Blokada?", "NIE" });
                        dataGridView1.Rows.Add(new object[] { "Rodzaj blokady", null });
                        dataGridView1.Rows.Add(new object[] { "Przyczyna blokady", null });
                        dataGridView1.Rows.Add(new object[] { "Czas zablokowania", null });
                        dataGridView1.Rows.Add(new object[] { "Zablokowane przez", null });
                        gbLockedBy.Visible = false;
                    }
                    operatorBox1.Set(t.Responsible);
                }
            }
        }
    }
}
