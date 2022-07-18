
namespace TabletManager
{
    partial class AuthAndPassDevices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthAndPassDevices));
            this.tabletDataGrid1 = new TabletManager.UI.Controls.TabletDataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.substituteAuth1 = new TabletManager.UI.Controls.SubstituteAuth();
            this.bRefresh = new System.Windows.Forms.Button();
            this.tabletPickup1 = new TabletManager.UI.Controls.TabletPickup();
            this.SuspendLayout();
            // 
            // tabletDataGrid1
            // 
            this.tabletDataGrid1.AllowToChangeOnlyMine = false;
            this.tabletDataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabletDataGrid1.Location = new System.Drawing.Point(12, 45);
            this.tabletDataGrid1.Name = "tabletDataGrid1";
            this.tabletDataGrid1.ShowOnlyMine = true;
            this.tabletDataGrid1.Size = new System.Drawing.Size(582, 706);
            this.tabletDataGrid1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Urządzenia do przekazania:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(600, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Autoryzacja zmiennika:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(600, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 37);
            this.label5.TabIndex = 10;
            this.label5.Text = "Odbiór:";
            // 
            // substituteAuth1
            // 
            this.substituteAuth1.Location = new System.Drawing.Point(607, 49);
            this.substituteAuth1.Name = "substituteAuth1";
            this.substituteAuth1.Size = new System.Drawing.Size(427, 184);
            this.substituteAuth1.TabIndex = 11;
            // 
            // bRefresh
            // 
            this.bRefresh.Location = new System.Drawing.Point(474, 9);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(120, 23);
            this.bRefresh.TabIndex = 20;
            this.bRefresh.Text = "Odśwież listę";
            this.bRefresh.UseVisualStyleBackColor = true;
            // 
            // tabletPickup1
            // 
            this.tabletPickup1.Location = new System.Drawing.Point(607, 286);
            this.tabletPickup1.Name = "tabletPickup1";
            this.tabletPickup1.Size = new System.Drawing.Size(427, 465);
            this.tabletPickup1.TabIndex = 21;
            // 
            // AuthAndPassDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 763);
            this.Controls.Add(this.tabletPickup1);
            this.Controls.Add(this.bRefresh);
            this.Controls.Add(this.substituteAuth1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabletDataGrid1);
            this.Name = "AuthAndPassDevices";
            this.Text = "AuthAndPassDevices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Controls.TabletDataGrid tabletDataGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private UI.Controls.SubstituteAuth substituteAuth1;
        private System.Windows.Forms.Button bRefresh;
        private UI.Controls.TabletPickup tabletPickup1;
    }
}