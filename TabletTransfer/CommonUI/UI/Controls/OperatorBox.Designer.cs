
namespace CommonUI.Controls
{
    partial class OperatorBox
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.lName = new System.Windows.Forms.Label();
            this.lAcpNo = new System.Windows.Forms.Label();
            this.lOrgCode = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbPhoto, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lAcpNo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lOrgCode, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 564);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pbPhoto
            // 
            this.pbPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPhoto.Location = new System.Drawing.Point(10, 10);
            this.pbPhoto.Margin = new System.Windows.Forms.Padding(10);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(498, 445);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPhoto.TabIndex = 1;
            this.pbPhoto.TabStop = false;
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lName.Location = new System.Drawing.Point(3, 465);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(512, 37);
            this.lName.TabIndex = 5;
            this.lName.Text = "Jacek Jonda";
            this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lAcpNo
            // 
            this.lAcpNo.AutoSize = true;
            this.lAcpNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lAcpNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lAcpNo.Location = new System.Drawing.Point(3, 502);
            this.lAcpNo.Name = "lAcpNo";
            this.lAcpNo.Size = new System.Drawing.Size(512, 37);
            this.lAcpNo.TabIndex = 5;
            this.lAcpNo.Text = "ACP173001";
            this.lAcpNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lOrgCode
            // 
            this.lOrgCode.AutoSize = true;
            this.lOrgCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lOrgCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lOrgCode.Location = new System.Drawing.Point(3, 539);
            this.lOrgCode.Name = "lOrgCode";
            this.lOrgCode.Size = new System.Drawing.Size(512, 25);
            this.lOrgCode.TabIndex = 5;
            this.lOrgCode.Text = "ORG";
            this.lOrgCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OperatorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OperatorBox";
            this.Size = new System.Drawing.Size(518, 564);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.Label lAcpNo;
        private System.Windows.Forms.Label lOrgCode;
    }
}
