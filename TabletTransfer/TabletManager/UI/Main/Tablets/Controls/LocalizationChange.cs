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
using TabletManager.AI;

namespace TabletManager.UI.Main.Tablets.Controls
{
    public partial class LocalizationChange : Form
    {
        public Tablet ToChange { get; }

        public LocalizationChange(Tablet toChange )
        {
            InitializeComponent();
            lTablet.Text = toChange.WindowsName.ToString();
            textBox1.Text = toChange.Responsible.ACPno.ToString();
            cbLocation.Items.Clear();
            cbLocation.Items.AddRange(CommonDatabase.CommonDbAccess.GetAllLocations());
            ToChange = toChange;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            string location = cbLocation.Text;
            Operator op = CommonDatabase.CommonDbAccess.GetOperator(tbNewResponsible.Text, location, tbPassword.Text);
            if (op!=null)
            {
                CommonDatabase.CommonDbAccess.UpdateTabletLocation(ToChange.WindowsName.ToString(), tbNewResponsible.Text, location);
                CommonDatabase.CommonDbAccess.RegisterEvent(ToChange, ToChange.Responsible.ACPno.ToString(), "LOCALIZATION_CHANGED", $"tablet changed location to {location} by {tbNewResponsible.Text}");
                this.Close();
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
