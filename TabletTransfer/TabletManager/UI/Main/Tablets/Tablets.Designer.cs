
namespace TabletManager.UI.Controls
{
    partial class Tablets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tablets));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabletDataGrid1 = new TabletManager.UI.Controls.TabletDataGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bPrintBarcode = new System.Windows.Forms.Button();
            this.bRemoteLock = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabletInfo1 = new TabletManager.UI.Controls.TabletInfo();
            this.tabletHistoryGrid1 = new TabletManager.UI.Controls.TabletHistoryGrid();
            this.bLocationChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1038, 565);
            this.splitContainer1.SplitterDistance = 542;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabletDataGrid1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(542, 565);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabletDataGrid1
            // 
            this.tabletDataGrid1.AllowToChangeOnlyMine = true;
            this.tabletDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabletDataGrid1.Location = new System.Drawing.Point(4, 33);
            this.tabletDataGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.tabletDataGrid1.Name = "tabletDataGrid1";
            this.tabletDataGrid1.ShowOnlyMine = false;
            this.tabletDataGrid1.Size = new System.Drawing.Size(534, 528);
            this.tabletDataGrid1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.bPrintBarcode);
            this.flowLayoutPanel1.Controls.Add(this.bRemoteLock);
            this.flowLayoutPanel1.Controls.Add(this.bLocationChange);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(542, 29);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // bPrintBarcode
            // 
            this.bPrintBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bPrintBarcode.Location = new System.Drawing.Point(464, 3);
            this.bPrintBarcode.Name = "bPrintBarcode";
            this.bPrintBarcode.Size = new System.Drawing.Size(75, 23);
            this.bPrintBarcode.TabIndex = 0;
            this.bPrintBarcode.Text = "Drukuj kody";
            this.bPrintBarcode.UseVisualStyleBackColor = true;
            this.bPrintBarcode.Click += new System.EventHandler(this.bPrintBarcode_Click);
            // 
            // bRemoteLock
            // 
            this.bRemoteLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemoteLock.Location = new System.Drawing.Point(383, 3);
            this.bRemoteLock.Name = "bRemoteLock";
            this.bRemoteLock.Size = new System.Drawing.Size(75, 23);
            this.bRemoteLock.TabIndex = 1;
            this.bRemoteLock.Text = "Zablokuj";
            this.bRemoteLock.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.tabletInfo1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabletHistoryGrid1);
            this.splitContainer2.Size = new System.Drawing.Size(492, 565);
            this.splitContainer2.SplitterDistance = 280;
            this.splitContainer2.TabIndex = 1;
            // 
            // tabletInfo1
            // 
            this.tabletInfo1.AutoScroll = true;
            this.tabletInfo1.AutoScrollMinSize = new System.Drawing.Size(500, 308);
            this.tabletInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabletInfo1.Location = new System.Drawing.Point(0, 0);
            this.tabletInfo1.Margin = new System.Windows.Forms.Padding(4);
            this.tabletInfo1.Name = "tabletInfo1";
            this.tabletInfo1.Size = new System.Drawing.Size(492, 280);
            this.tabletInfo1.TabIndex = 0;
            // 
            // tabletHistoryGrid1
            // 
            this.tabletHistoryGrid1.Data = ((System.Collections.Generic.List<CommonDatabase.Data.TabletEvent>)(resources.GetObject("tabletHistoryGrid1.Data")));
            this.tabletHistoryGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabletHistoryGrid1.Location = new System.Drawing.Point(0, 0);
            this.tabletHistoryGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.tabletHistoryGrid1.Name = "tabletHistoryGrid1";
            this.tabletHistoryGrid1.Size = new System.Drawing.Size(492, 281);
            this.tabletHistoryGrid1.TabIndex = 0;
            // 
            // bLocationChange
            // 
            this.bLocationChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bLocationChange.Location = new System.Drawing.Point(278, 3);
            this.bLocationChange.Name = "bLocationChange";
            this.bLocationChange.Size = new System.Drawing.Size(99, 23);
            this.bLocationChange.TabIndex = 1;
            this.bLocationChange.Text = "Zmień lokalizacje";
            this.bLocationChange.UseVisualStyleBackColor = true;
            // 
            // Tablets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Tablets";
            this.Size = new System.Drawing.Size(1038, 565);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabletDataGrid tabletDataGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TabletHistoryGrid tabletHistoryGrid1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private TabletInfo tabletInfo1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button bPrintBarcode;
        private System.Windows.Forms.Button bRemoteLock;
        private System.Windows.Forms.Button bLocationChange;
    }
}
