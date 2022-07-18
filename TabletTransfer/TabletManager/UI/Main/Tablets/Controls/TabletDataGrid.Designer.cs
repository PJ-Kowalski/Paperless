
namespace TabletManager.UI.Controls
{
    partial class TabletDataGrid
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.WindowsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Odpowiedzialny = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LockType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbFilterMine = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lista tabletów:";
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
            this.WindowsName,
            this.Barcode,
            this.Odpowiedzialny,
            this.LockType});
            this.dataGridView1.Location = new System.Drawing.Point(3, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(705, 546);
            this.dataGridView1.TabIndex = 2;
            // 
            // WindowsName
            // 
            this.WindowsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WindowsName.HeaderText = "Nazwa";
            this.WindowsName.Name = "WindowsName";
            this.WindowsName.ReadOnly = true;
            this.WindowsName.Width = 65;
            // 
            // Barcode
            // 
            this.Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Barcode.HeaderText = "Kod";
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            this.Barcode.Width = 51;
            // 
            // Odpowiedzialny
            // 
            this.Odpowiedzialny.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Odpowiedzialny.HeaderText = "Responsible";
            this.Odpowiedzialny.Name = "Odpowiedzialny";
            this.Odpowiedzialny.ReadOnly = true;
            // 
            // LockType
            // 
            this.LockType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LockType.HeaderText = "Blokada";
            this.LockType.Name = "LockType";
            this.LockType.ReadOnly = true;
            this.LockType.Width = 71;
            // 
            // cbFilterMine
            // 
            this.cbFilterMine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilterMine.AutoSize = true;
            this.cbFilterMine.Location = new System.Drawing.Point(631, 3);
            this.cbFilterMine.Name = "cbFilterMine";
            this.cbFilterMine.Size = new System.Drawing.Size(77, 17);
            this.cbFilterMine.TabIndex = 4;
            this.cbFilterMine.Text = "Tylko moje";
            this.cbFilterMine.UseVisualStyleBackColor = true;
            // 
            // TabletDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFilterMine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TabletDataGrid";
            this.Size = new System.Drawing.Size(711, 571);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Odpowiedzialny;
        private System.Windows.Forms.DataGridViewTextBoxColumn LockType;
        private System.Windows.Forms.CheckBox cbFilterMine;
    }
}
