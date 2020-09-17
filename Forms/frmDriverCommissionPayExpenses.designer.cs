namespace Taxi_AppMain
{
    partial class frmDriverCommissionPayExpenses
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
            this.ddlDrivers = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCommissionDue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoDebit = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoCredit = new Telerik.WinControls.UI.RadRadioButton();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numCurrBalance = new Telerik.WinControls.UI.RadSpinEditor();
            this.label8 = new System.Windows.Forms.Label();
            this.lblerror = new System.Windows.Forms.Label();
            this.lblTransactionNo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.spnCommissionPay = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.grdPaymentHistory = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDrivers)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCommissionPay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlDrivers
            // 
            this.ddlDrivers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlDrivers.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDrivers.Location = new System.Drawing.Point(217, 73);
            this.ddlDrivers.Name = "ddlDrivers";
            this.ddlDrivers.Size = new System.Drawing.Size(292, 30);
            this.ddlDrivers.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Driver No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Commission Due:";
            // 
            // lblCommissionDue
            // 
            this.lblCommissionDue.AutoSize = true;
            this.lblCommissionDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommissionDue.ForeColor = System.Drawing.Color.Red;
            this.lblCommissionDue.Location = new System.Drawing.Point(213, 126);
            this.lblCommissionDue.Name = "lblCommissionDue";
            this.lblCommissionDue.Size = new System.Drawing.Size(22, 23);
            this.lblCommissionDue.TabIndex = 10;
            this.lblCommissionDue.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "Amount :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoDebit);
            this.panel1.Controls.Add(this.rdoCredit);
            this.panel1.Controls.Add(this.txtReason);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.numCurrBalance);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblerror);
            this.panel1.Controls.Add(this.lblTransactionNo);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnExit1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.spnCommissionPay);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblCommissionDue);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ddlDrivers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 421);
            this.panel1.TabIndex = 106;
            // 
            // rdoDebit
            // 
            this.rdoDebit.Location = new System.Drawing.Point(453, 177);
            this.rdoDebit.Name = "rdoDebit";
            this.rdoDebit.Size = new System.Drawing.Size(77, 18);
            this.rdoDebit.TabIndex = 123;
            this.rdoDebit.Text = "Debit";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoDebit.GetChildAt(0))).Text = "Debit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoDebit.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoDebit.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoCredit
            // 
            this.rdoCredit.Location = new System.Drawing.Point(371, 177);
            this.rdoCredit.Name = "rdoCredit";
            this.rdoCredit.Size = new System.Drawing.Size(77, 18);
            this.rdoCredit.TabIndex = 122;
            this.rdoCredit.Text = "Credit";
            this.rdoCredit.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoCredit.GetChildAt(0))).ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoCredit.GetChildAt(0))).Text = "Credit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoCredit.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoCredit.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReason
            // 
            this.txtReason.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(215, 216);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(294, 27);
            this.txtReason.TabIndex = 121;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 23);
            this.label6.TabIndex = 119;
            this.label6.Text = "Description :";
            // 
            // numCurrBalance
            // 
            this.numCurrBalance.DecimalPlaces = 2;
            this.numCurrBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCurrBalance.InterceptArrowKeys = false;
            this.numCurrBalance.Location = new System.Drawing.Point(217, 264);
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
            this.label8.Location = new System.Drawing.Point(11, 268);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 23);
            this.label8.TabIndex = 116;
            this.label8.Text = "Current Balance : £";
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
            // lblTransactionNo
            // 
            this.lblTransactionNo.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTransactionNo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblTransactionNo.ForeColor = System.Drawing.Color.White;
            this.lblTransactionNo.Location = new System.Drawing.Point(113, 1);
            this.lblTransactionNo.Name = "lblTransactionNo";
            this.lblTransactionNo.Size = new System.Drawing.Size(595, 18);
            this.lblTransactionNo.TabIndex = 111;
            this.lblTransactionNo.Text = "jhgjhg";
            this.lblTransactionNo.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(2, 1);
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
            this.label5.Location = new System.Drawing.Point(187, 177);
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
            this.label3.Location = new System.Drawing.Point(187, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 23);
            this.label3.TabIndex = 108;
            this.label3.Text = "£";
            // 
            // btnExit1
            // 
            this.btnExit1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit1.Location = new System.Drawing.Point(304, 326);
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
            this.btnSave.Location = new System.Drawing.Point(119, 326);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(137, 46);
            this.btnSave.TabIndex = 106;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // spnCommissionPay
            // 
            this.spnCommissionPay.DecimalPlaces = 2;
            this.spnCommissionPay.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spnCommissionPay.Location = new System.Drawing.Point(216, 174);
            this.spnCommissionPay.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.spnCommissionPay.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.spnCommissionPay.Name = "spnCommissionPay";
            // 
            // 
            // 
            this.spnCommissionPay.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.spnCommissionPay.ShowBorder = true;
            this.spnCommissionPay.ShowUpDownButtons = false;
            this.spnCommissionPay.Size = new System.Drawing.Size(144, 28);
            this.spnCommissionPay.TabIndex = 13;
            this.spnCommissionPay.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.spnCommissionPay.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.spnCommissionPay.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnCommissionPay.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnCommissionPay.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 459);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(656, 27);
            this.radLabel1.TabIndex = 107;
            this.radLabel1.Text = "Payment History";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.grdPaymentHistory.Name = "grdPaymentHistory";
            this.grdPaymentHistory.ShowGroupPanel = false;
            this.grdPaymentHistory.Size = new System.Drawing.Size(656, 159);
            this.grdPaymentHistory.TabIndex = 108;
            this.grdPaymentHistory.Text = "radGridView1";
            // 
            // frmDriverCommissionPayExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 645);
            this.Controls.Add(this.grdPaymentHistory);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Driver Commission Pay";
            this.Name = "frmDriverCommissionPayExpenses";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Commission Pay  (Expenses)";
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
            ((System.ComponentModel.ISupportInitialize)(this.rdoDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCommissionPay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList ddlDrivers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCommissionDue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadSpinEditor spnCommissionPay;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTransactionNo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblerror;
        private System.Windows.Forms.Label label8;
        private Telerik.WinControls.UI.RadSpinEditor numCurrBalance;
        private System.Windows.Forms.Label label6;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGridView grdPaymentHistory;
        private System.Windows.Forms.TextBox txtReason;
        private Telerik.WinControls.UI.RadRadioButton rdoDebit;
        private Telerik.WinControls.UI.RadRadioButton rdoCredit;
    }
}