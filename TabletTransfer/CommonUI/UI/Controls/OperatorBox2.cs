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

namespace CommonUI.Controls
{
    public partial class OperatorBox2 : UserControl
    {
        List<Label> labels = new List<Label>();
        public OperatorBox2()
        {
            InitializeComponent();
            labels.Add(lName);
            labels.Add(lAcpNo);
            //labels.Add(lOrgCode);
        }
        private float size = 10;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public float LabelsSize { get => size; set => SetSize(value); }
        public void SetSize(float size)
        {
            labels.ForEach(x => x.Font = new Font(tableLayoutPanel1.Font.FontFamily, size));
            this.size = size;
        }
        private bool onlyPhoto = false;
        public bool OnlyPhoto
        {
            get => onlyPhoto;
            set {
                onlyPhoto = value;
                lName.Visible = !onlyPhoto;
                lAcpNo.Visible = !onlyPhoto;
                //lOrgCode.Visible = !onlyPhoto;
            }
        }
        public void Clear()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { Clear(); }));
                return;
            }
            pbPhoto.Image = null;
            lName.Text = "";
            lAcpNo.Text = "";
            //lOrgCode.Text = "";
            tableLayoutPanel1.BackColor = SystemColors.Control;
        }
        Operator currentOperator;
        public void Set(Operator op)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(()=> { Set(op); }));
                return;
            }
            if (op == null)
            {
                Clear();
            }
            else
            {
                pbPhoto.Image = op.Photo;
                lName.Text = op.Name;
                lAcpNo.Text = op.ACPno;
                //lOrgCode.Text = op.OrgCode;
                switch (op.IsOnPremise)
                {
                    case Operator.OnPremiseEnum.Yes:
                        tableLayoutPanel1.BackColor = Color.Green;
                        tableLayoutPanel1.ForeColor = Color.White;
                        break;
                    case Operator.OnPremiseEnum.Maybe:
                        tableLayoutPanel1.BackColor = Color.Yellow;
                        tableLayoutPanel1.ForeColor = Color.Black;
                        break;
                    case Operator.OnPremiseEnum.No:
                        tableLayoutPanel1.BackColor = Color.Red;
                        tableLayoutPanel1.ForeColor = Color.White;
                        break;
                    default:
                        tableLayoutPanel1.BackColor = SystemColors.Control;
                        tableLayoutPanel1.ForeColor = SystemColors.ControlText;
                        break;
                }
            }
            currentOperator = op;
        }
        public Operator Get()
        {
            return currentOperator;
        }
    }
}
