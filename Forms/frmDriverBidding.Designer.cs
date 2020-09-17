namespace Taxi_AppMain
{
    partial class frmDriverBidding
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new Telerik.WinControls.UI.RadButton();
            this.lablltest = new Telerik.WinControls.UI.RadLabel();
            this.grdDrivers = new Telerik.WinControls.UI.RadGridView();
            this.object_3e0d7595_e73a_41b7_935a_265ec96696fd = new Telerik.WinControls.UI.RadLabel.RadLabelRootElement();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtJobOfferedTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lablltest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDrivers.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(633, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Driver Bidding";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.SteelBlue;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(236, 640);
            this.btnClose.Name = "btnClose";
            // 
            // 
            // 
            this.btnClose.RootElement.ForeColor = System.Drawing.Color.White;
            this.btnClose.Size = new System.Drawing.Size(177, 32);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Exit Form";
            this.btnClose.Click += new System.EventHandler(this.radButton1_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(0))).Text = "Exit Form";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(0))).Font = new System.Drawing.Font("Arial", 8.25F);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.SteelBlue;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.SteelBlue;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.SteelBlue;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.SteelBlue;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            // 
            // lablltest
            // 
            this.lablltest.Location = new System.Drawing.Point(10, 148);
            this.lablltest.Name = "lablltest";
            this.lablltest.Size = new System.Drawing.Size(2, 2);
            this.lablltest.TabIndex = 11;
            // 
            // grdDrivers
            // 
            this.grdDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDrivers.Location = new System.Drawing.Point(10, 66);
            // 
            // grdDrivers
            // 
            this.grdDrivers.MasterTemplate.AllowAddNewRow = false;
            this.grdDrivers.MasterTemplate.AllowDeleteRow = false;
            gridViewTextBoxColumn1.HeaderText = "Driver";
            gridViewTextBoxColumn1.Name = "DriverNo";
            gridViewTextBoxColumn1.Width = 360;
            gridViewTextBoxColumn2.HeaderText = "column1";
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "DriverId";
            gridViewTextBoxColumn3.HeaderText = "column1";
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "Id";
            gridViewDecimalColumn1.HeaderText = "Price £";
            gridViewDecimalColumn1.Name = "Bid";
            gridViewDecimalColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewDecimalColumn1.Width = 100;
            gridViewCommandColumn1.DefaultText = "Despatch Job";
            gridViewCommandColumn1.HeaderText = " ";
            gridViewCommandColumn1.Name = "btndespatchjob";
            gridViewCommandColumn1.UseDefaultText = true;
            gridViewCommandColumn1.Width = 120;
            this.grdDrivers.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewDecimalColumn1,
            gridViewCommandColumn1});
            this.grdDrivers.MasterTemplate.ShowFilteringRow = false;
            this.grdDrivers.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdDrivers.Name = "grdDrivers";
            this.grdDrivers.ShowGroupPanel = false;
            this.grdDrivers.Size = new System.Drawing.Size(617, 513);
            this.grdDrivers.TabIndex = 12;
            this.grdDrivers.Text = "radGridView1";
            this.grdDrivers.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.grdDrivers_CellBeginEdit);
            // 
            // object_3e0d7595_e73a_41b7_935a_265ec96696fd
            // 
            this.object_3e0d7595_e73a_41b7_935a_265ec96696fd.ForeColor = System.Drawing.Color.Red;
            this.object_3e0d7595_e73a_41b7_935a_265ec96696fd.Name = "object_3e0d7595_e73a_41b7_935a_265ec96696fd";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(478, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(155, 31);
            this.lblTotal.TabIndex = 13;
            this.lblTotal.Text = "Total Bids :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtJobOfferedTo
            // 
            this.txtJobOfferedTo.AutoSize = true;
            this.txtJobOfferedTo.BackColor = System.Drawing.Color.Red;
            this.txtJobOfferedTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobOfferedTo.ForeColor = System.Drawing.Color.White;
            this.txtJobOfferedTo.Location = new System.Drawing.Point(13, 39);
            this.txtJobOfferedTo.Name = "txtJobOfferedTo";
            this.txtJobOfferedTo.Size = new System.Drawing.Size(0, 16);
            this.txtJobOfferedTo.TabIndex = 15;
            // 
            // frmDriverBidding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(633, 676);
            this.Controls.Add(this.txtJobOfferedTo);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.grdDrivers);
            this.Controls.Add(this.lablltest);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Name = "frmDriverBidding";
            this.ShowIcon = false;
            this.Text = "Driver Bidding";
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lablltest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDrivers.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDrivers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton btnClose;
        private Telerik.WinControls.UI.RadLabel lablltest;
        private Telerik.WinControls.UI.RadGridView grdDrivers;
        private Telerik.WinControls.UI.RadLabel.RadLabelRootElement object_3e0d7595_e73a_41b7_935a_265ec96696fd;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label txtJobOfferedTo;
    }
}