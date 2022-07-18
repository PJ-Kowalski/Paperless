
namespace TabletManager
{
    partial class AuthPassChange
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
            this.tbAcpNo = new System.Windows.Forms.TextBox();
            this.tbNewPass1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOldPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNewPass2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbAcpNo
            // 
            this.tbAcpNo.Location = new System.Drawing.Point(147, 31);
            this.tbAcpNo.Name = "tbAcpNo";
            this.tbAcpNo.Size = new System.Drawing.Size(100, 20);
            this.tbAcpNo.TabIndex = 0;
            this.tbAcpNo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbAcpNo_PreviewKeyDown);
            // 
            // tbNewPass1
            // 
            this.tbNewPass1.Location = new System.Drawing.Point(147, 83);
            this.tbNewPass1.Name = "tbNewPass1";
            this.tbNewPass1.PasswordChar = '*';
            this.tbNewPass1.Size = new System.Drawing.Size(100, 20);
            this.tbNewPass1.TabIndex = 1;
            this.tbNewPass1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbNewPass1_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TabletManager";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ACPNO:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nowe hasło:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(12, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(235, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Anuluj";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Aktualne hasło:";
            // 
            // tbOldPass
            // 
            this.tbOldPass.Location = new System.Drawing.Point(147, 57);
            this.tbOldPass.Name = "tbOldPass";
            this.tbOldPass.PasswordChar = '*';
            this.tbOldPass.Size = new System.Drawing.Size(100, 20);
            this.tbOldPass.TabIndex = 8;
            this.tbOldPass.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbOldPass_PreviewKeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Powtórz nowe hasło:";
            // 
            // tbNewPass2
            // 
            this.tbNewPass2.Location = new System.Drawing.Point(147, 109);
            this.tbNewPass2.Name = "tbNewPass2";
            this.tbNewPass2.PasswordChar = '*';
            this.tbNewPass2.Size = new System.Drawing.Size(100, 20);
            this.tbNewPass2.TabIndex = 10;
            this.tbNewPass2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbNewPass2_PreviewKeyDown);
            // 
            // AuthPassChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 206);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbNewPass2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbOldPass);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNewPass1);
            this.Controls.Add(this.tbAcpNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AuthPassChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAcpNo;
        private System.Windows.Forms.TextBox tbNewPass1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbOldPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNewPass2;
    }
}