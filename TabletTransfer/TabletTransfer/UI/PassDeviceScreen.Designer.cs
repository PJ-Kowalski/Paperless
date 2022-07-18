
using CommonUI.Controls;

namespace TabletTransfer.UI
{
    partial class PassDeviceScreen
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lPrevScanConfirm = new System.Windows.Forms.Label();
            this.cbCancel = new System.Windows.Forms.CheckBox();
            this.operatorBox1 = new CommonUI.Controls.OperatorBox();
            this.operatorBox2 = new CommonUI.Controls.OperatorBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lInstruction = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tlpConfirm = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lNextScanConfirm = new System.Windows.Forms.Label();
            this.bTabletOk = new System.Windows.Forms.Button();
            this.bTabletNG = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpConfirm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lPrevScanConfirm, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbCancel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.operatorBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.operatorBox2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlpConfirm, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 3, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.47826F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.04348F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.47826F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1920, 1200);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(23, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(619, 100);
            this.label4.TabIndex = 4;
            this.label4.Text = "Poprzedni użytkownik:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(1273, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(619, 100);
            this.label5.TabIndex = 4;
            this.label5.Text = "Następny użytkownik:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lPrevScanConfirm
            // 
            this.lPrevScanConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lPrevScanConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lPrevScanConfirm.Location = new System.Drawing.Point(23, 664);
            this.lPrevScanConfirm.Name = "lPrevScanConfirm";
            this.lPrevScanConfirm.Size = new System.Drawing.Size(619, 434);
            this.lPrevScanConfirm.TabIndex = 1;
            this.lPrevScanConfirm.Text = "Skan potwierdzony";
            this.lPrevScanConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbCancel
            // 
            this.cbCancel.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbCancel.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbCancel.Location = new System.Drawing.Point(648, 537);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(619, 124);
            this.cbCancel.TabIndex = 7;
            this.cbCancel.Text = "ANULUJ";
            this.cbCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbCancel.UseVisualStyleBackColor = true;
            this.cbCancel.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // operatorBox1
            // 
            this.operatorBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorBox1.LabelsSize = 0F;
            this.operatorBox1.Location = new System.Drawing.Point(23, 103);
            this.operatorBox1.Name = "operatorBox1";
            this.operatorBox1.OnlyPhoto = false;
            this.tableLayoutPanel1.SetRowSpan(this.operatorBox1, 2);
            this.operatorBox1.Size = new System.Drawing.Size(619, 558);
            this.operatorBox1.TabIndex = 8;
            // 
            // operatorBox2
            // 
            this.operatorBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorBox2.LabelsSize = 0F;
            this.operatorBox2.Location = new System.Drawing.Point(1273, 103);
            this.operatorBox2.Name = "operatorBox2";
            this.operatorBox2.OnlyPhoto = false;
            this.tableLayoutPanel1.SetRowSpan(this.operatorBox2, 2);
            this.operatorBox2.Size = new System.Drawing.Size(619, 558);
            this.operatorBox2.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lInstruction, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(648, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(619, 528);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // lInstruction
            // 
            this.lInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lInstruction.Location = new System.Drawing.Point(3, 352);
            this.lInstruction.Name = "lInstruction";
            this.lInstruction.Size = new System.Drawing.Size(613, 176);
            this.lInstruction.TabIndex = 4;
            this.lInstruction.Text = "Zeskanuj aktualnego operatora";
            this.lInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 129.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(3, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(561, 174);
            this.label2.TabIndex = 3;
            this.label2.Text = "➠";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpConfirm
            // 
            this.tlpConfirm.ColumnCount = 3;
            this.tlpConfirm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tlpConfirm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpConfirm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpConfirm.Controls.Add(this.label1, 1, 0);
            this.tlpConfirm.Controls.Add(this.lNextScanConfirm, 0, 1);
            this.tlpConfirm.Controls.Add(this.bTabletOk, 0, 0);
            this.tlpConfirm.Controls.Add(this.bTabletNG, 2, 0);
            this.tlpConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpConfirm.Location = new System.Drawing.Point(1273, 667);
            this.tlpConfirm.Name = "tlpConfirm";
            this.tlpConfirm.RowCount = 2;
            this.tlpConfirm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpConfirm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tlpConfirm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfirm.Size = new System.Drawing.Size(619, 428);
            this.tlpConfirm.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(209, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 213);
            this.label1.TabIndex = 1;
            this.label1.Text = "Czy tablet jest OK?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lNextScanConfirm
            // 
            this.tlpConfirm.SetColumnSpan(this.lNextScanConfirm, 3);
            this.lNextScanConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lNextScanConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lNextScanConfirm.Location = new System.Drawing.Point(3, 213);
            this.lNextScanConfirm.Name = "lNextScanConfirm";
            this.lNextScanConfirm.Size = new System.Drawing.Size(613, 215);
            this.lNextScanConfirm.TabIndex = 1;
            this.lNextScanConfirm.Text = "Skan potwierdzony";
            this.lNextScanConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bTabletOk
            // 
            this.bTabletOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bTabletOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bTabletOk.Location = new System.Drawing.Point(3, 3);
            this.bTabletOk.Name = "bTabletOk";
            this.bTabletOk.Size = new System.Drawing.Size(200, 207);
            this.bTabletOk.TabIndex = 2;
            this.bTabletOk.Text = "✓";
            this.bTabletOk.UseVisualStyleBackColor = true;
            this.bTabletOk.Click += new System.EventHandler(this.bTabletOk_Click);
            // 
            // bTabletNG
            // 
            this.bTabletNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bTabletNG.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bTabletNG.Location = new System.Drawing.Point(415, 3);
            this.bTabletNG.Name = "bTabletNG";
            this.bTabletNG.Size = new System.Drawing.Size(201, 207);
            this.bTabletNG.TabIndex = 2;
            this.bTabletNG.Text = "✘";
            this.bTabletNG.UseVisualStyleBackColor = true;
            this.bTabletNG.Click += new System.EventHandler(this.bTabletNG_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(1273, 1101);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(619, 20);
            this.textBox1.TabIndex = 10;
            // 
            // PassDeviceScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1200);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PassDeviceScreen";
            this.Text = "PassDevice";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpConfirm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lNextScanConfirm;
        private System.Windows.Forms.Label lPrevScanConfirm;
        private System.Windows.Forms.CheckBox cbCancel;
        private OperatorBox operatorBox1;
        private OperatorBox operatorBox2;
        private System.Windows.Forms.Button bTabletOk;
        private System.Windows.Forms.Button bTabletNG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lInstruction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}