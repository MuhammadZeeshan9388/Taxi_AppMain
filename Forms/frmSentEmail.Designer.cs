﻿namespace Taxi_AppMain
{
    partial class frmSentEmail
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
            this.lblHeader = new Telerik.WinControls.UI.RadLabel();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.grdLister = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = false;
            this.lblHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            // 
            // 
            // 
            this.lblHeader.RootElement.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Size = new System.Drawing.Size(1039, 23);
            this.lblHeader.TabIndex = 231;
            this.lblHeader.Text = "Sent Email Items";
            this.lblHeader.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 23);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(1039, 654);
            this.radSplitContainer1.TabIndex = 234;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.grdLister);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(1039, 654);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.2736264F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 61);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // grdLister
            // 
            this.grdLister.AutoSizeRows = true;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLister.Location = new System.Drawing.Point(0, 0);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.MasterTemplate.AllowColumnReorder = false;
            this.grdLister.MasterTemplate.EnableFiltering = true;
            this.grdLister.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.ReadOnly = true;
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.Size = new System.Drawing.Size(1039, 654);
            this.grdLister.TabIndex = 233;
            this.grdLister.Text = "radGridView1";
            this.grdLister.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdLister_KeyDown);
            this.grdLister.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdLister_CellClick);
            // 
            // frmSentEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 677);
            this.Controls.Add(this.radSplitContainer1);
            this.Controls.Add(this.lblHeader);
            this.MaximizeBox = false;
            this.Name = "frmSentEmail";
            this.ShowIcon = false;
            this.Text = "Sent Email Items";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSMSReply_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.lblHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblHeader;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.RadGridView grdLister;
    }
}