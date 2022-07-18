
namespace TabletManager.UI.Controls
{
    partial class Users
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Users));
            this.bAdd = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ACPNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imię = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nazwisko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Org = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOnPremise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBlue = new System.Windows.Forms.CheckBox();
            this.cbSL = new System.Windows.Forms.CheckBox();
            this.cbTL = new System.Windows.Forms.CheckBox();
            this.cbService = new System.Windows.Forms.CheckBox();
            this.addOperator1 = new TabletManager.UI.AddOperator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bBadge = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(3, 5);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(108, 23);
            this.bAdd.TabIndex = 8;
            this.bAdd.Text = "Dodaj do listy";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bRemove
            // 
            this.bRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemove.Location = new System.Drawing.Point(225, 5);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(75, 23);
            this.bRemove.TabIndex = 7;
            this.bRemove.Text = "Usuń z listy";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ACPNO,
            this.Role,
            this.Imię,
            this.Nazwisko,
            this.Org,
            this.IsOnPremise});
            this.dataGridView1.Location = new System.Drawing.Point(3, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(297, 425);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // ACPNO
            // 
            this.ACPNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ACPNO.HeaderText = "ACPNO";
            this.ACPNO.Name = "ACPNO";
            this.ACPNO.ReadOnly = true;
            this.ACPNO.Width = 69;
            // 
            // Role
            // 
            this.Role.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Role.HeaderText = "Rola";
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            this.Role.Width = 5;
            // 
            // Imię
            // 
            this.Imię.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Imię.HeaderText = "Name";
            this.Imię.Name = "Imię";
            this.Imię.ReadOnly = true;
            this.Imię.Width = 60;
            // 
            // Nazwisko
            // 
            this.Nazwisko.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Nazwisko.HeaderText = "LastName";
            this.Nazwisko.Name = "Nazwisko";
            this.Nazwisko.ReadOnly = true;
            this.Nazwisko.Width = 80;
            // 
            // Org
            // 
            this.Org.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Org.HeaderText = "Org";
            this.Org.Name = "Org";
            this.Org.ReadOnly = true;
            this.Org.Width = 49;
            // 
            // IsOnPremise
            // 
            this.IsOnPremise.HeaderText = "Obecność";
            this.IsOnPremise.Name = "IsOnPremise";
            this.IsOnPremise.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lista aktywnych użytkowników:";
            // 
            // cbBlue
            // 
            this.cbBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBlue.AutoSize = true;
            this.cbBlue.Location = new System.Drawing.Point(3, 370);
            this.cbBlue.Name = "cbBlue";
            this.cbBlue.Size = new System.Drawing.Size(77, 17);
            this.cbBlue.TabIndex = 10;
            this.cbBlue.Text = "Operatorzy";
            this.cbBlue.UseVisualStyleBackColor = true;
            // 
            // cbSL
            // 
            this.cbSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSL.AutoSize = true;
            this.cbSL.Location = new System.Drawing.Point(3, 393);
            this.cbSL.Name = "cbSL";
            this.cbSL.Size = new System.Drawing.Size(39, 17);
            this.cbSL.TabIndex = 11;
            this.cbSL.Text = "SL";
            this.cbSL.UseVisualStyleBackColor = true;
            // 
            // cbTL
            // 
            this.cbTL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbTL.AutoSize = true;
            this.cbTL.Location = new System.Drawing.Point(3, 416);
            this.cbTL.Name = "cbTL";
            this.cbTL.Size = new System.Drawing.Size(39, 17);
            this.cbTL.TabIndex = 12;
            this.cbTL.Text = "TL";
            this.cbTL.UseVisualStyleBackColor = true;
            // 
            // cbService
            // 
            this.cbService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbService.AutoSize = true;
            this.cbService.Location = new System.Drawing.Point(3, 439);
            this.cbService.Name = "cbService";
            this.cbService.Size = new System.Drawing.Size(57, 17);
            this.cbService.TabIndex = 13;
            this.cbService.Text = "Serwis";
            this.cbService.UseVisualStyleBackColor = true;
            // 
            // addOperator1
            // 
            this.addOperator1.Location = new System.Drawing.Point(3, 31);
            this.addOperator1.Name = "addOperator1";
            this.addOperator1.Size = new System.Drawing.Size(289, 334);
            this.addOperator1.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bBadge);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.bRemove);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addOperator1);
            this.splitContainer1.Panel2.Controls.Add(this.cbService);
            this.splitContainer1.Panel2.Controls.Add(this.bAdd);
            this.splitContainer1.Panel2.Controls.Add(this.cbTL);
            this.splitContainer1.Panel2.Controls.Add(this.cbBlue);
            this.splitContainer1.Panel2.Controls.Add(this.cbSL);
            this.splitContainer1.Size = new System.Drawing.Size(613, 459);
            this.splitContainer1.SplitterDistance = 303;
            this.splitContainer1.TabIndex = 14;
            // 
            // bBadge
            // 
            this.bBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bBadge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bBadge.BackgroundImage")));
            this.bBadge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bBadge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBadge.Location = new System.Drawing.Point(192, 5);
            this.bBadge.Name = "bBadge";
            this.bBadge.Size = new System.Drawing.Size(27, 23);
            this.bBadge.TabIndex = 14;
            this.toolTip1.SetToolTip(this.bBadge, "Drukuj tymczasowy badge");
            this.bBadge.UseVisualStyleBackColor = true;
            this.bBadge.Click += new System.EventHandler(this.bBadge_Click);
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(613, 459);
            this.Name = "Users";
            this.Size = new System.Drawing.Size(613, 459);
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AddOperator addOperator1;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbBlue;
        private System.Windows.Forms.CheckBox cbSL;
        private System.Windows.Forms.CheckBox cbTL;
        private System.Windows.Forms.CheckBox cbService;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACPNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imię;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwisko;
        private System.Windows.Forms.DataGridViewTextBoxColumn Org;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsOnPremise;
        private System.Windows.Forms.Button bBadge;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
