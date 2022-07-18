
namespace TabletManager.UI.Controls
{
    partial class SubstituteAuth
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
            this.bClear = new System.Windows.Forms.Button();
            this.bOk = new System.Windows.Forms.Button();
            this.operatorBox21 = new CommonUI.Controls.OperatorBox2();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(4, 146);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(169, 27);
            this.bClear.TabIndex = 16;
            this.bClear.Text = "Wyczyść";
            this.bClear.UseVisualStyleBackColor = true;
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(4, 91);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(169, 49);
            this.bOk.TabIndex = 15;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // operatorBox21
            // 
            this.operatorBox21.LabelsSize = 10F;
            this.operatorBox21.Location = new System.Drawing.Point(179, 3);
            this.operatorBox21.Name = "operatorBox21";
            this.operatorBox21.OnlyPhoto = false;
            this.operatorBox21.Size = new System.Drawing.Size(240, 170);
            this.operatorBox21.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hasło:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "ACPNO:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(73, 65);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(73, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 10;
            // 
            // SubstituteAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.operatorBox21);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "SubstituteAuth";
            this.Size = new System.Drawing.Size(427, 184);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bOk;
        private CommonUI.Controls.OperatorBox2 operatorBox21;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}
