
namespace TabletManager
{
    partial class Main
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tablets1 = new TabletManager.UI.Controls.Tablets();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.users1 = new TabletManager.UI.Controls.Users();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.events1 = new TabletManager.UI.Controls.Events();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.bAddLocation = new System.Windows.Forms.Button();
            this.userInfo1 = new TabletManager.UI.Controls.UserInfo();
            this.alertIcon1 = new TabletManager.UI.Controls.AlertIcon();
            this.bRefresh = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(16, 54);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1488, 802);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tablets1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1480, 773);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Statusy tabletów";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tablets1
            // 
            this.tablets1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablets1.Location = new System.Drawing.Point(4, 4);
            this.tablets1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tablets1.Name = "tablets1";
            this.tablets1.Size = new System.Drawing.Size(1472, 765);
            this.tablets1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.users1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1480, 773);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Użytkownicy";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // users1
            // 
            this.users1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.users1.Location = new System.Drawing.Point(4, 4);
            this.users1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.users1.MinimumSize = new System.Drawing.Size(817, 565);
            this.users1.Name = "users1";
            this.users1.Size = new System.Drawing.Size(1472, 765);
            this.users1.TabIndex = 0;
            this.users1.TabletLocation = null;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.events1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Size = new System.Drawing.Size(1480, 773);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Zdarzenia";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // events1
            // 
            this.events1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.events1.Location = new System.Drawing.Point(4, 4);
            this.events1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.events1.Name = "events1";
            this.events1.Size = new System.Drawing.Size(1472, 765);
            this.events1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Lokalizacja:";
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(116, 15);
            this.cbLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(132, 24);
            this.cbLocation.TabIndex = 7;
            this.cbLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocation_SelectedIndexChanged);
            // 
            // bAddLocation
            // 
            this.bAddLocation.Location = new System.Drawing.Point(257, 15);
            this.bAddLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bAddLocation.Name = "bAddLocation";
            this.bAddLocation.Size = new System.Drawing.Size(32, 28);
            this.bAddLocation.TabIndex = 9;
            this.bAddLocation.Text = "+";
            this.bAddLocation.UseVisualStyleBackColor = true;
            this.bAddLocation.Click += new System.EventHandler(this.bAddLocation_Click);
            // 
            // userInfo1
            // 
            this.userInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userInfo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userInfo1.Location = new System.Drawing.Point(637, 2);
            this.userInfo1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.userInfo1.Name = "userInfo1";
            this.userInfo1.Size = new System.Drawing.Size(751, 70);
            this.userInfo1.TabIndex = 10;
            // 
            // alertIcon1
            // 
            this.alertIcon1.Blink = false;
            this.alertIcon1.Location = new System.Drawing.Point(335, 2);
            this.alertIcon1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.alertIcon1.Name = "alertIcon1";
            this.alertIcon1.Size = new System.Drawing.Size(295, 71);
            this.alertIcon1.TabIndex = 11;
            // 
            // bRefresh
            // 
            this.bRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRefresh.Location = new System.Drawing.Point(1404, 2);
            this.bRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(100, 71);
            this.bRefresh.TabIndex = 12;
            this.bRefresh.Text = "Odśwież";
            this.bRefresh.UseVisualStyleBackColor = true;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1520, 871);
            this.Controls.Add(this.bRefresh);
            this.Controls.Add(this.alertIcon1);
            this.Controls.Add(this.userInfo1);
            this.Controls.Add(this.bAddLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbLocation);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLocation;
        private UI.Controls.Users users1;
        private System.Windows.Forms.Button bAddLocation;
        private System.Windows.Forms.TabPage tabPage3;
        private UI.Controls.Tablets tablets1;
        private UI.Controls.UserInfo userInfo1;
        private UI.Controls.Events events1;
        private UI.Controls.AlertIcon alertIcon1;
        private System.Windows.Forms.Button bRefresh;
    }
}

