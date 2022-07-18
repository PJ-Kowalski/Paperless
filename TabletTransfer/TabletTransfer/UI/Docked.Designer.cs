
using TabletTransfer.UI;

namespace TabletTransfer
{
    partial class Docked
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Docked));
            this.bPassDevice = new System.Windows.Forms.Button();
            this.label1 = new TabletTransfer.UI.TransparentLabel();
            this.bStorage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bPassDevice
            // 
            this.bPassDevice.BackgroundImage = global::TabletTransfer.Properties.Resources.iconfinder_UI_Basic_GLYPH_47_4733377;
            this.bPassDevice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bPassDevice.Location = new System.Drawing.Point(152, 4);
            this.bPassDevice.Name = "bPassDevice";
            this.bPassDevice.Size = new System.Drawing.Size(38, 33);
            this.bPassDevice.TabIndex = 1;
            this.bPassDevice.UseVisualStyleBackColor = true;
            this.bPassDevice.Click += new System.EventHandler(this.bPassDevice_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // bStorage
            // 
            this.bStorage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bStorage.BackgroundImage")));
            this.bStorage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bStorage.Location = new System.Drawing.Point(196, 4);
            this.bStorage.Name = "bStorage";
            this.bStorage.Size = new System.Drawing.Size(38, 33);
            this.bStorage.TabIndex = 2;
            this.bStorage.UseVisualStyleBackColor = true;
            // 
            // Docked
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 40);
            this.Controls.Add(this.bStorage);
            this.Controls.Add(this.bPassDevice);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Docked";
            this.Opacity = 0.5D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Docked_FormClosing);
            this.Load += new System.EventHandler(this.Docked_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Docked_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Docked_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Docked_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Docked_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Docked_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TransparentLabel label1;
        private System.Windows.Forms.Button bPassDevice;
        private System.Windows.Forms.Button bStorage;
    }
}

