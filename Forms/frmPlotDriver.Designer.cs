namespace Taxi_AppMain
{
    partial class frmPlotDriver
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.ddl_Driver = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPlot = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.ddlZone = new Telerik.WinControls.UI.RadDropDownList();
            this.lblZone = new System.Windows.Forms.Label();
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.Pg_Main = new Telerik.WinControls.UI.RadPageViewPage();
            this.ddlVehicle = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.Pg_Ordering = new Telerik.WinControls.UI.RadPageViewPage();
            this.btnCancelOrder = new Telerik.WinControls.UI.RadButton();
            this.btnSaveOrder = new Telerik.WinControls.UI.RadButton();
            this.btnMoveDownZone = new Telerik.WinControls.UI.RadButton();
            this.btnMoveUp = new Telerik.WinControls.UI.RadButton();
            this.lstOrderedDrvs = new Telerik.WinControls.UI.RadListControl();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.Pg_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicle)).BeginInit();
            this.Pg_Ordering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveDownZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstOrderedDrvs)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(0, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(478, 31);
            this.lblHeading.TabIndex = 11;
            this.lblHeading.Text = "Plot Driver";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(126, 40);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(259, 23);
            this.ddl_Driver.TabIndex = 16;
            this.ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddl_Driver_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Select Driver";
            // 
            // btnPlot
            // 
            this.btnPlot.Location = new System.Drawing.Point(170, 168);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(93, 30);
            this.btnPlot.TabIndex = 17;
            this.btnPlot.Text = "Plot";
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPlot.GetChildAt(0))).Text = "Plot";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPlot.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPlot.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(291, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 30);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Text = "Cancel";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlZone
            // 
            this.ddlZone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlZone.Location = new System.Drawing.Point(126, 81);
            this.ddlZone.Name = "ddlZone";
            this.ddlZone.Size = new System.Drawing.Size(259, 23);
            this.ddlZone.TabIndex = 20;
            // 
            // lblZone
            // 
            this.lblZone.AutoSize = true;
            this.lblZone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZone.Location = new System.Drawing.Point(30, 84);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(83, 16);
            this.lblZone.TabIndex = 19;
            this.lblZone.Text = "Select Zone";
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.Pg_Main);
            this.radPageView1.Controls.Add(this.Pg_Ordering);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageView1.Location = new System.Drawing.Point(0, 31);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.Pg_Main;
            this.radPageView1.Size = new System.Drawing.Size(478, 254);
            this.radPageView1.TabIndex = 21;
            this.radPageView1.Text = "Ordering";
          
            this.radPageView1.SelectedPageChanged += new System.EventHandler(this.radPageView1_SelectedPageChanged);
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            // 
            // Pg_Main
            // 
            this.Pg_Main.Controls.Add(this.ddlVehicle);
            this.Pg_Main.Controls.Add(this.label1);
            this.Pg_Main.Controls.Add(this.label2);
            this.Pg_Main.Controls.Add(this.btnCancel);
            this.Pg_Main.Controls.Add(this.ddlZone);
            this.Pg_Main.Controls.Add(this.btnPlot);
            this.Pg_Main.Controls.Add(this.ddl_Driver);
            this.Pg_Main.Controls.Add(this.lblZone);
            this.Pg_Main.Location = new System.Drawing.Point(10, 37);
            this.Pg_Main.Name = "Pg_Main";
            this.Pg_Main.Size = new System.Drawing.Size(457, 206);
            this.Pg_Main.Text = "Plot";
            // 
            // ddlVehicle
            // 
            this.ddlVehicle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlVehicle.Location = new System.Drawing.Point(127, 120);
            this.ddlVehicle.Name = "ddlVehicle";
            this.ddlVehicle.Size = new System.Drawing.Size(259, 23);
            this.ddlVehicle.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Vehicle";
            // 
            // Pg_Ordering
            // 
            this.Pg_Ordering.Controls.Add(this.btnCancelOrder);
            this.Pg_Ordering.Controls.Add(this.btnSaveOrder);
            this.Pg_Ordering.Controls.Add(this.btnMoveDownZone);
            this.Pg_Ordering.Controls.Add(this.btnMoveUp);
            this.Pg_Ordering.Controls.Add(this.lstOrderedDrvs);
            this.Pg_Ordering.Location = new System.Drawing.Point(10, 37);
            this.Pg_Ordering.Name = "Pg_Ordering";
            this.Pg_Ordering.Size = new System.Drawing.Size(457, 206);
            this.Pg_Ordering.Text = "Ordering";
            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnCancelOrder.Location = new System.Drawing.Point(375, 167);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(82, 36);
            this.btnCancelOrder.TabIndex = 7;
            this.btnCancelOrder.Text = "Cancel";
            this.btnCancelOrder.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelOrder.TextWrap = true;
            this.btnCancelOrder.Click += new System.EventHandler(this.btnCancel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancelOrder.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancelOrder.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancelOrder.GetChildAt(0))).Text = "Cancel";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancelOrder.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancelOrder.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancelOrder.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSaveOrder.Location = new System.Drawing.Point(274, 167);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(82, 36);
            this.btnSaveOrder.TabIndex = 6;
            this.btnSaveOrder.Text = "Save";
            this.btnSaveOrder.TextWrap = true;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveOrder.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveOrder.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveOrder.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveOrder.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveOrder.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnMoveDownZone
            // 
            this.btnMoveDownZone.Image = global::Taxi_AppMain.Properties.Resources.lc_movedown;
            this.btnMoveDownZone.Location = new System.Drawing.Point(305, 68);
            this.btnMoveDownZone.Name = "btnMoveDownZone";
            this.btnMoveDownZone.Size = new System.Drawing.Size(129, 36);
            this.btnMoveDownZone.TabIndex = 5;
            this.btnMoveDownZone.Text = "Move Down";
            this.btnMoveDownZone.TextWrap = true;
            this.btnMoveDownZone.Click += new System.EventHandler(this.btnMoveDownZone_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveDownZone.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.lc_movedown;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveDownZone.GetChildAt(0))).Text = "Move Down";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveDownZone.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveDownZone.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveDownZone.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = global::Taxi_AppMain.Properties.Resources.lc_moveup;
            this.btnMoveUp.Location = new System.Drawing.Point(305, 12);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(129, 36);
            this.btnMoveUp.TabIndex = 4;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveUp.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.lc_moveup;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveUp.GetChildAt(0))).Text = "Move Up";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveUp.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveUp.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lstOrderedDrvs
            // 
            this.lstOrderedDrvs.CaseSensitiveSort = true;
            this.lstOrderedDrvs.Location = new System.Drawing.Point(4, 13);
            this.lstOrderedDrvs.Name = "lstOrderedDrvs";
            this.lstOrderedDrvs.Size = new System.Drawing.Size(267, 190);
            this.lstOrderedDrvs.TabIndex = 3;
            this.lstOrderedDrvs.Text = "radListControl1";
            // 
            // frmPlotDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 285);
            this.Controls.Add(this.radPageView1);
            this.Controls.Add(this.lblHeading);
            this.MaximizeBox = false;
            this.Name = "frmPlotDriver";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plot Driver";
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.Pg_Main.ResumeLayout(false);
            this.Pg_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicle)).EndInit();
            this.Pg_Ordering.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveDownZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstOrderedDrvs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadButton btnPlot;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadDropDownList ddlZone;
        private System.Windows.Forms.Label lblZone;
        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPageViewPage Pg_Main;
        private Telerik.WinControls.UI.RadPageViewPage Pg_Ordering;
        private Telerik.WinControls.UI.RadButton btnMoveDownZone;
        private Telerik.WinControls.UI.RadButton btnMoveUp;
        private Telerik.WinControls.UI.RadListControl lstOrderedDrvs;
        private Telerik.WinControls.UI.RadButton btnCancelOrder;
        private Telerik.WinControls.UI.RadButton btnSaveOrder;
        private Telerik.WinControls.UI.RadDropDownList ddlVehicle;
        private System.Windows.Forms.Label label1;
    }
}