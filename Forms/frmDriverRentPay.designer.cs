namespace Taxi_AppMain
{
    partial class frmDriverRentPay
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.ddlDrivers = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRentDue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.chkOther = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ddlRentPayReason = new Telerik.WinControls.UI.RadDropDownList();
            this.numCurrBalance = new Telerik.WinControls.UI.RadSpinEditor();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRentPaidSign = new System.Windows.Forms.Label();
            this.txtRentPaid = new System.Windows.Forms.Label();
            this.lblerror = new System.Windows.Forms.Label();
            this.txtIsPaid = new System.Windows.Forms.Label();
            this.lblTransactionNo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.spnRentPay = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnPaymentHistory = new Telerik.WinControls.UI.RadButton();
            this.grdPaymentHistory = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDrivers)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRentPayReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnRentPay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.radLabel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPaymentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlDrivers
            // 
            this.ddlDrivers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlDrivers.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDrivers.Location = new System.Drawing.Point(247, 108);
            this.ddlDrivers.Name = "ddlDrivers";
            this.ddlDrivers.Size = new System.Drawing.Size(292, 30);
            this.ddlDrivers.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Driver No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(58, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Rent Due:";
            // 
            // lblRentDue
            // 
            this.lblRentDue.AutoSize = true;
            this.lblRentDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRentDue.ForeColor = System.Drawing.Color.Red;
            this.lblRentDue.Location = new System.Drawing.Point(243, 164);
            this.lblRentDue.Name = "lblRentDue";
            this.lblRentDue.Size = new System.Drawing.Size(22, 23);
            this.lblRentDue.TabIndex = 10;
            this.lblRentDue.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(58, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "Rent Pay:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtReason);
            this.panel1.Controls.Add(this.chkOther);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ddlRentPayReason);
            this.panel1.Controls.Add(this.numCurrBalance);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtRentPaidSign);
            this.panel1.Controls.Add(this.txtRentPaid);
            this.panel1.Controls.Add(this.lblerror);
            this.panel1.Controls.Add(this.txtIsPaid);
            this.panel1.Controls.Add(this.lblTransactionNo);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnExit1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.spnRentPay);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblRentDue);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ddlDrivers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 421);
            this.panel1.TabIndex = 106;
            // 
            // txtReason
            // 
            this.txtReason.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(244, 256);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(292, 27);
            this.txtReason.TabIndex = 121;
            this.txtReason.Visible = false;
            // 
            // chkOther
            // 
            this.chkOther.AutoSize = true;
            this.chkOther.Location = new System.Drawing.Point(552, 256);
            this.chkOther.Name = "chkOther";
            this.chkOther.Size = new System.Drawing.Size(64, 22);
            this.chkOther.TabIndex = 120;
            this.chkOther.Text = "Other";
            this.chkOther.UseVisualStyleBackColor = true;
            this.chkOther.CheckedChanged += new System.EventHandler(this.chkOther_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(58, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 23);
            this.label6.TabIndex = 119;
            this.label6.Text = "Reason :";
            // 
            // ddlRentPayReason
            // 
            this.ddlRentPayReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlRentPayReason.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlRentPayReason.Location = new System.Drawing.Point(244, 253);
            this.ddlRentPayReason.Name = "ddlRentPayReason";
            this.ddlRentPayReason.Size = new System.Drawing.Size(292, 30);
            this.ddlRentPayReason.TabIndex = 118;
            // 
            // numCurrBalance
            // 
            this.numCurrBalance.DecimalPlaces = 2;
            this.numCurrBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCurrBalance.InterceptArrowKeys = false;
            this.numCurrBalance.Location = new System.Drawing.Point(252, 302);
            this.numCurrBalance.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCurrBalance.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numCurrBalance.Name = "numCurrBalance";
            this.numCurrBalance.ReadOnly = true;
            // 
            // 
            // 
            this.numCurrBalance.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCurrBalance.ShowBorder = true;
            this.numCurrBalance.ShowUpDownButtons = false;
            this.numCurrBalance.Size = new System.Drawing.Size(144, 28);
            this.numCurrBalance.TabIndex = 117;
            this.numCurrBalance.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCurrBalance.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Layouts.BoxLayout)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(1))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(1))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCurrBalance.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(58, 306);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 23);
            this.label8.TabIndex = 116;
            this.label8.Text = "Current Balance : £";
            // 
            // txtRentPaidSign
            // 
            this.txtRentPaidSign.AutoSize = true;
            this.txtRentPaidSign.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRentPaidSign.ForeColor = System.Drawing.Color.Red;
            this.txtRentPaidSign.Location = new System.Drawing.Point(548, 40);
            this.txtRentPaidSign.Name = "txtRentPaidSign";
            this.txtRentPaidSign.Size = new System.Drawing.Size(22, 23);
            this.txtRentPaidSign.TabIndex = 115;
            this.txtRentPaidSign.Text = "£";
            this.txtRentPaidSign.Visible = false;
            // 
            // txtRentPaid
            // 
            this.txtRentPaid.AutoSize = true;
            this.txtRentPaid.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRentPaid.ForeColor = System.Drawing.Color.Red;
            this.txtRentPaid.Location = new System.Drawing.Point(567, 40);
            this.txtRentPaid.Name = "txtRentPaid";
            this.txtRentPaid.Size = new System.Drawing.Size(52, 23);
            this.txtRentPaid.TabIndex = 114;
            this.txtRentPaid.Text = "0.00";
            this.txtRentPaid.Visible = false;
            // 
            // lblerror
            // 
            this.lblerror.AutoSize = true;
            this.lblerror.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblerror.ForeColor = System.Drawing.Color.Red;
            this.lblerror.Location = new System.Drawing.Point(3, 3);
            this.lblerror.Name = "lblerror";
            this.lblerror.Size = new System.Drawing.Size(0, 19);
            this.lblerror.TabIndex = 113;
            // 
            // txtIsPaid
            // 
            this.txtIsPaid.AutoSize = true;
            this.txtIsPaid.Font = new System.Drawing.Font("Tahoma", 22.25F, System.Drawing.FontStyle.Bold);
            this.txtIsPaid.ForeColor = System.Drawing.Color.Red;
            this.txtIsPaid.Location = new System.Drawing.Point(548, 4);
            this.txtIsPaid.Name = "txtIsPaid";
            this.txtIsPaid.Size = new System.Drawing.Size(94, 36);
            this.txtIsPaid.TabIndex = 112;
            this.txtIsPaid.Text = "PAID";
            this.txtIsPaid.Visible = false;
            // 
            // lblTransactionNo
            // 
            this.lblTransactionNo.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTransactionNo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblTransactionNo.ForeColor = System.Drawing.Color.White;
            this.lblTransactionNo.Location = new System.Drawing.Point(113, 69);
            this.lblTransactionNo.Name = "lblTransactionNo";
            this.lblTransactionNo.Size = new System.Drawing.Size(553, 18);
            this.lblTransactionNo.TabIndex = 111;
            this.lblTransactionNo.Text = "jhgjhg";
            this.lblTransactionNo.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(2, 69);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 18);
            this.lblTitle.TabIndex = 110;
            this.lblTitle.Text = "Statement No:";
            this.lblTitle.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(217, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 23);
            this.label5.TabIndex = 109;
            this.label5.Text = "£";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(217, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 23);
            this.label3.TabIndex = 108;
            this.label3.Text = "£";
            // 
            // btnExit1
            // 
            this.btnExit1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit1.Location = new System.Drawing.Point(304, 364);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(144, 46);
            this.btnExit1.TabIndex = 107;
            this.btnExit1.Text = "Exit";
            this.btnExit1.Click += new System.EventHandler(this.btnExit1_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(119, 364);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(137, 46);
            this.btnSave.TabIndex = 106;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // spnRentPay
            // 
            this.spnRentPay.DecimalPlaces = 2;
            this.spnRentPay.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spnRentPay.Location = new System.Drawing.Point(246, 215);
            this.spnRentPay.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.spnRentPay.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.spnRentPay.Name = "spnRentPay";
            // 
            // 
            // 
            this.spnRentPay.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.spnRentPay.ShowBorder = true;
            this.spnRentPay.ShowUpDownButtons = false;
            this.spnRentPay.Size = new System.Drawing.Size(144, 28);
            this.spnRentPay.TabIndex = 13;
            this.spnRentPay.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.spnRentPay.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.spnRentPay.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnRentPay.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnRentPay.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.radLabel1.Controls.Add(this.btnPaymentHistory);
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 459);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(665, 27);
            this.radLabel1.TabIndex = 107;
            this.radLabel1.Text = "Payment History";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPaymentHistory
            // 
            this.btnPaymentHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaymentHistory.Location = new System.Drawing.Point(466, 3);
            this.btnPaymentHistory.Name = "btnPaymentHistory";
            this.btnPaymentHistory.Size = new System.Drawing.Size(193, 22);
            this.btnPaymentHistory.TabIndex = 120;
            this.btnPaymentHistory.Text = "See Full Payment History";
            this.btnPaymentHistory.Click += new System.EventHandler(this.btnPaymentHistory_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPaymentHistory.GetChildAt(0))).Text = "See Full Payment History";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPaymentHistory.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPaymentHistory.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            // 
            // grdPaymentHistory
            // 
            this.grdPaymentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPaymentHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPaymentHistory.Location = new System.Drawing.Point(0, 486);
            // 
            // grdPaymentHistory
            // 
            this.grdPaymentHistory.MasterTemplate.AllowAddNewRow = false;
            this.grdPaymentHistory.MasterTemplate.AllowDeleteRow = false;
            this.grdPaymentHistory.MasterTemplate.AllowEditRow = false;
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Payment Date";
            gridViewTextBoxColumn1.Name = "PaymentDate";
            gridViewTextBoxColumn1.Width = 130;
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Old Balance";
            gridViewTextBoxColumn2.Name = "Balance";
            gridViewTextBoxColumn2.Width = 100;
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Rent Pay";
            gridViewTextBoxColumn3.Name = "RentPay";
            gridViewTextBoxColumn3.Width = 100;
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "Balance Due";
            gridViewTextBoxColumn4.Name = "BalanceDue";
            gridViewTextBoxColumn4.Width = 100;
            gridViewTextBoxColumn5.FormatString = "";
            gridViewTextBoxColumn5.HeaderText = "Reason";
            gridViewTextBoxColumn5.Name = "Reason";
            gridViewTextBoxColumn5.Width = 200;
            this.grdPaymentHistory.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5});
            this.grdPaymentHistory.Name = "grdPaymentHistory";
            this.grdPaymentHistory.ShowGroupPanel = false;
            this.grdPaymentHistory.Size = new System.Drawing.Size(665, 112);
            this.grdPaymentHistory.TabIndex = 108;
            this.grdPaymentHistory.Text = "radGridView1";
            // 
            // frmDriverRentPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 598);
            this.Controls.Add(this.grdPaymentHistory);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Driver Rent Pay";
            this.Name = "frmDriverRentPay";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Rent Pay";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.radLabel1, 0);
            this.Controls.SetChildIndex(this.grdPaymentHistory, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDrivers)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRentPayReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnRentPay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.radLabel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPaymentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList ddlDrivers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRentDue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadSpinEditor spnRentPay;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTransactionNo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label txtIsPaid;
        private System.Windows.Forms.Label lblerror;
        private System.Windows.Forms.Label txtRentPaidSign;
        private System.Windows.Forms.Label txtRentPaid;
        private System.Windows.Forms.Label label8;
        private Telerik.WinControls.UI.RadSpinEditor numCurrBalance;
        private System.Windows.Forms.Label label6;
        private Telerik.WinControls.UI.RadDropDownList ddlRentPayReason;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGridView grdPaymentHistory;
        private Telerik.WinControls.UI.RadButton btnPaymentHistory;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.CheckBox chkOther;
    }
}