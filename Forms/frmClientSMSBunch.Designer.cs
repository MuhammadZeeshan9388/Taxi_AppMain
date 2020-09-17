namespace Taxi_AppMain
{
    partial class frmClientSMSBunch
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
            this.grdCustomerBunch = new Telerik.WinControls.UI.RadGridView();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnPick = new Telerik.WinControls.UI.RadButton();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnRecreateBunch = new Telerik.WinControls.UI.RadButton();
            this.txtMessage = new Telerik.WinControls.UI.RadTextBox();
            this.spnBunchValue = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecreateBunch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnBunchValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCustomerBunch
            // 
            this.grdCustomerBunch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCustomerBunch.EnableHotTracking = false;
            this.grdCustomerBunch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdCustomerBunch.Location = new System.Drawing.Point(0, 154);
            // 
            // grdCustomerBunch
            // 
            this.grdCustomerBunch.MasterTemplate.AllowAddNewRow = false;
            this.grdCustomerBunch.Name = "grdCustomerBunch";
            this.grdCustomerBunch.ShowGroupPanel = false;
            this.grdCustomerBunch.Size = new System.Drawing.Size(375, 429);
            this.grdCustomerBunch.TabIndex = 114;
            this.grdCustomerBunch.Text = "myGridView1";
            // 
            // btnExit1
            // 
            this.btnExit1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit1.Location = new System.Drawing.Point(246, 10);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(114, 42);
            this.btnExit1.TabIndex = 233;
            this.btnExit1.Text = "Close";
            this.btnExit1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit1.Click += new System.EventHandler(this.btnExit1_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPick
            // 
            this.btnPick.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPick.Location = new System.Drawing.Point(52, 10);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(105, 42);
            this.btnPick.TabIndex = 234;
            this.btnPick.Text = "Pick";
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPick.GetChildAt(0))).Text = "Pick";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPick.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPick.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.btnRecreateBunch);
            this.radPanel1.Controls.Add(this.txtMessage);
            this.radPanel1.Controls.Add(this.spnBunchValue);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(375, 116);
            this.radPanel1.TabIndex = 235;
            // 
            // btnRecreateBunch
            // 
            this.btnRecreateBunch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecreateBunch.Location = new System.Drawing.Point(226, 85);
            this.btnRecreateBunch.Name = "btnRecreateBunch";
            this.btnRecreateBunch.Size = new System.Drawing.Size(145, 22);
            this.btnRecreateBunch.TabIndex = 241;
            this.btnRecreateBunch.Text = "Recreate Bunch";
            this.btnRecreateBunch.Click += new System.EventHandler(this.btnRecreateBunch_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRecreateBunch.GetChildAt(0))).Text = "Recreate Bunch";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRecreateBunch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRecreateBunch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            // 
            // 
            // 
            this.txtMessage.RootElement.StretchVertically = true;
            this.txtMessage.Size = new System.Drawing.Size(383, 77);
            this.txtMessage.TabIndex = 239;
            this.txtMessage.TabStop = false;
            // 
            // spnBunchValue
            // 
            this.spnBunchValue.Location = new System.Drawing.Point(109, 85);
            this.spnBunchValue.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.spnBunchValue.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spnBunchValue.Name = "spnBunchValue";
            // 
            // 
            // 
            this.spnBunchValue.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.spnBunchValue.ShowBorder = true;
            this.spnBunchValue.Size = new System.Drawing.Size(100, 21);
            this.spnBunchValue.TabIndex = 2;
            this.spnBunchValue.TabStop = false;
            this.spnBunchValue.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(6, 86);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(90, 19);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Bunch Value";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.btnPick);
            this.radPanel2.Controls.Add(this.btnExit1);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 583);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(375, 61);
            this.radPanel2.TabIndex = 236;
            // 
            // frmClientSMSBunch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 644);
            this.ControlBox = true;
            this.Controls.Add(this.grdCustomerBunch);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Create SMS Bunch";
            this.Name = "frmClientSMSBunch";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Create SMS Bunch";
            this.Load += new System.EventHandler(this.frmClientSMSBunch_Load);
            this.Shown += new System.EventHandler(this.frmClientSMSBunch_Shown);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.radPanel2, 0);
            this.Controls.SetChildIndex(this.grdCustomerBunch, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecreateBunch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnBunchValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdCustomerBunch;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnPick;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadSpinEditor spnBunchValue;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtMessage;
        private Telerik.WinControls.UI.RadButton btnRecreateBunch;
    }
}