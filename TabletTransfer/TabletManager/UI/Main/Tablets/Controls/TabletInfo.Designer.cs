
namespace TabletManager.UI.Controls
{
    partial class TabletInfo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Par = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lPlaceholder = new System.Windows.Forms.Label();
            this.operatorBox1 = new CommonUI.Controls.OperatorBox2();
            this.gbReponsible = new System.Windows.Forms.GroupBox();
            this.gbLockedBy = new System.Windows.Forms.GroupBox();
            this.operatorBox2 = new CommonUI.Controls.OperatorBox2();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbReponsible.SuspendLayout();
            this.gbLockedBy.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.lPlaceholder);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(615, 394);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacje o tablecie";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Par,
            this.Value});
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(312, 372);
            this.dataGridView1.TabIndex = 0;
            // 
            // Par
            // 
            this.Par.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Par.HeaderText = "Parametr";
            this.Par.Name = "Par";
            this.Par.ReadOnly = true;
            this.Par.Width = 74;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Wartość";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // lPlaceholder
            // 
            this.lPlaceholder.Location = new System.Drawing.Point(26, 60);
            this.lPlaceholder.Name = "lPlaceholder";
            this.lPlaceholder.Size = new System.Drawing.Size(100, 23);
            this.lPlaceholder.TabIndex = 1;
            this.lPlaceholder.Text = "PLACEHOLDER LABEL";
            this.lPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lPlaceholder.Visible = false;
            // 
            // operatorBox1
            // 
            this.operatorBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorBox1.LabelsSize = 8F;
            this.operatorBox1.Location = new System.Drawing.Point(3, 16);
            this.operatorBox1.Name = "operatorBox1";
            this.operatorBox1.OnlyPhoto = false;
            this.operatorBox1.Size = new System.Drawing.Size(273, 163);
            this.operatorBox1.TabIndex = 2;
            // 
            // gbReponsible
            // 
            this.gbReponsible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbReponsible.Controls.Add(this.operatorBox1);
            this.gbReponsible.Location = new System.Drawing.Point(3, 3);
            this.gbReponsible.Name = "gbReponsible";
            this.gbReponsible.Size = new System.Drawing.Size(279, 182);
            this.gbReponsible.TabIndex = 3;
            this.gbReponsible.TabStop = false;
            this.gbReponsible.Text = "Odpowiedzialny";
            // 
            // gbLockedBy
            // 
            this.gbLockedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLockedBy.Controls.Add(this.operatorBox2);
            this.gbLockedBy.Location = new System.Drawing.Point(3, 191);
            this.gbLockedBy.Name = "gbLockedBy";
            this.gbLockedBy.Size = new System.Drawing.Size(279, 183);
            this.gbLockedBy.TabIndex = 4;
            this.gbLockedBy.TabStop = false;
            this.gbLockedBy.Text = "Zablokowany przez";
            // 
            // operatorBox2
            // 
            this.operatorBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorBox2.LabelsSize = 8F;
            this.operatorBox2.Location = new System.Drawing.Point(3, 16);
            this.operatorBox2.Name = "operatorBox2";
            this.operatorBox2.OnlyPhoto = false;
            this.operatorBox2.Size = new System.Drawing.Size(273, 164);
            this.operatorBox2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gbReponsible, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbLockedBy, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(324, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(285, 377);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // TabletInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Name = "TabletInfo";
            this.Size = new System.Drawing.Size(615, 394);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbReponsible.ResumeLayout(false);
            this.gbLockedBy.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Par;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Label lPlaceholder;
        private CommonUI.Controls.OperatorBox2 operatorBox1;
        private System.Windows.Forms.GroupBox gbLockedBy;
        private CommonUI.Controls.OperatorBox2 operatorBox2;
        private System.Windows.Forms.GroupBox gbReponsible;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
