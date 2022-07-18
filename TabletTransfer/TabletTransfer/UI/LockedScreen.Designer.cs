
using CommonUI.Controls;

namespace TabletTransfer.UI
{
    partial class LockedScreen
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
            this.operatorBox1 = new CommonUI.Controls.OperatorBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.bOk = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lWho = new System.Windows.Forms.Label();
            this.lLockdownComment = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lLockdownType = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lMsg = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbReason = new System.Windows.Forms.TextBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.operatorBox1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lDescription, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.bOk, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.bClear, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lMsg, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbReason, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.versionLabel, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.47826F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.04348F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.47826F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1920, 1200);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // operatorBox1
            // 
            this.operatorBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorBox1.LabelsSize = 0F;
            this.operatorBox1.Location = new System.Drawing.Point(1273, 111);
            this.operatorBox1.Name = "operatorBox1";
            this.operatorBox1.OnlyPhoto = false;
            this.tableLayoutPanel1.SetRowSpan(this.operatorBox1, 2);
            this.operatorBox1.Size = new System.Drawing.Size(619, 549);
            this.operatorBox1.TabIndex = 7;
            // 
            // lDescription
            // 
            this.lDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lDescription.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lDescription.Location = new System.Drawing.Point(23, 108);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(619, 427);
            this.lDescription.TabIndex = 6;
            this.lDescription.Text = "Tablet został zablokowany.\r\n\r\nUrządzenie może zostać odblokowane\r\njedynie przez p" +
    "racownika który je zablokował\r\nlub przez serwis.\r\n\r\n";
            this.lDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bOk
            // 
            this.bOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOk.Enabled = false;
            this.bOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bOk.Location = new System.Drawing.Point(1273, 666);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(619, 421);
            this.bOk.TabIndex = 8;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bClear
            // 
            this.bClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bClear.Location = new System.Drawing.Point(1273, 3);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(619, 102);
            this.bClear.TabIndex = 8;
            this.bClear.Text = "Anuluj skan";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(648, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 421);
            this.panel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lWho, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lLockdownComment, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lLockdownType, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(619, 421);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // lWho
            // 
            this.lWho.AutoSize = true;
            this.lWho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lWho.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lWho.Location = new System.Drawing.Point(3, 301);
            this.lWho.Name = "lWho";
            this.lWho.Size = new System.Drawing.Size(613, 120);
            this.lWho.TabIndex = 2;
            this.lWho.Text = "ACP173001";
            this.lWho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lLockdownComment
            // 
            this.lLockdownComment.AutoSize = true;
            this.lLockdownComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lLockdownComment.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lLockdownComment.Location = new System.Drawing.Point(3, 162);
            this.lLockdownComment.Name = "lLockdownComment";
            this.lLockdownComment.Size = new System.Drawing.Size(613, 119);
            this.lLockdownComment.TabIndex = 1;
            this.lLockdownComment.Text = "komentarz";
            this.lLockdownComment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Rodzaj blokady:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(3, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Komentarz:";
            // 
            // lLockdownType
            // 
            this.lLockdownType.AutoSize = true;
            this.lLockdownType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lLockdownType.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lLockdownType.Location = new System.Drawing.Point(3, 23);
            this.lLockdownType.Name = "lLockdownType";
            this.lLockdownType.Size = new System.Drawing.Size(613, 119);
            this.lLockdownType.TabIndex = 0;
            this.lLockdownType.Text = "SERWIS";
            this.lLockdownType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(3, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(244, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Zablokowany przez:";
            // 
            // label3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(23, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1244, 108);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z A B L O K O W A N Y";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lMsg
            // 
            this.lMsg.AutoSize = true;
            this.lMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lMsg.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lMsg.ForeColor = System.Drawing.Color.Red;
            this.lMsg.Location = new System.Drawing.Point(648, 535);
            this.lMsg.Name = "lMsg";
            this.lMsg.Size = new System.Drawing.Size(619, 128);
            this.lMsg.TabIndex = 10;
            this.lMsg.Text = "tresc wiadomosci";
            this.lMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(1273, 1093);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(619, 20);
            this.textBox1.TabIndex = 11;
            // 
            // tbReason
            // 
            this.tbReason.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReason.Location = new System.Drawing.Point(648, 666);
            this.tbReason.Multiline = true;
            this.tbReason.Name = "tbReason";
            this.tbReason.Size = new System.Drawing.Size(619, 421);
            this.tbReason.TabIndex = 12;
            this.tbReason.Visible = false;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(23, 1090);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(49, 14);
            this.versionLabel.TabIndex = 13;
            this.versionLabel.Text = "label1";
            // 
            // LockedScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1200);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LockedScreen";
            this.Text = "PassDevice";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lDescription;
        private OperatorBox operatorBox1;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lWho;
        private System.Windows.Forms.Label lLockdownComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lLockdownType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lMsg;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbReason;
        private System.Windows.Forms.Label versionLabel;
    }
}