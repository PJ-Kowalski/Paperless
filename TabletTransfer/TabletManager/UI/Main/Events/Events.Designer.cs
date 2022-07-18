
namespace TabletManager.UI.Controls
{
    partial class Events
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Events));
            this.tabletHistoryGrid1 = new TabletManager.UI.Controls.TabletHistoryGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.alertsGrid1 = new TabletManager.UI.Controls.AlertsGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabletHistoryGrid1
            // 
            this.tabletHistoryGrid1.Data = ((System.Collections.Generic.List<CommonDatabase.Data.TabletEvent>)(resources.GetObject("tabletHistoryGrid1.Data")));
            this.tabletHistoryGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabletHistoryGrid1.Location = new System.Drawing.Point(0, 0);
            this.tabletHistoryGrid1.Name = "tabletHistoryGrid1";
            this.tabletHistoryGrid1.Size = new System.Drawing.Size(1298, 390);
            this.tabletHistoryGrid1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.alertsGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabletHistoryGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(1298, 706);
            this.splitContainer1.SplitterDistance = 312;
            this.splitContainer1.TabIndex = 1;
            // 
            // alertsGrid1
            // 
            this.alertsGrid1.Data = ((System.Collections.Generic.List<CommonDatabase.Data.Alert>)(resources.GetObject("alertsGrid1.Data")));
            this.alertsGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertsGrid1.Location = new System.Drawing.Point(0, 0);
            this.alertsGrid1.Name = "alertsGrid1";
            this.alertsGrid1.Size = new System.Drawing.Size(1298, 312);
            this.alertsGrid1.TabIndex = 0;
            // 
            // Events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Events";
            this.Size = new System.Drawing.Size(1298, 706);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private TabletHistoryGrid tabletHistoryGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AlertsGrid alertsGrid1;
    }
}
