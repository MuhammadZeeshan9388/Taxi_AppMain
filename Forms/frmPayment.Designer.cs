namespace Taxi_AppMain
{
    partial class frmPayment
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdPayment = new UI.MyGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBalance = new Telerik.WinControls.UI.RadSpinEditor();
            this.numPayment = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.dtpPaymentDate = new UI.MyDatePicker();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.txtInvoiceTotal = new Telerik.WinControls.UI.RadTextBox();
            this.txtInvoiceno = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.ddlCompany = new Telerik.WinControls.UI.RadComboBox();
            this.lblDiscount = new Telerik.WinControls.UI.RadLabel();
            this.txtCreditNote = new Telerik.WinControls.UI.RadTextBox();
            this.lblCreditNote = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayment.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPaymentDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreditNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCreditNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(655, 187);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(655, 116);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(655, 266);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(655, 340);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(655, 415);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdPayment);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.radLabel11);
            this.panel1.Location = new System.Drawing.Point(12, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 289);
            this.panel1.TabIndex = 107;
            // 
            // grdPayment
            // 
            this.grdPayment.AutoCellFormatting = false;
            this.grdPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPayment.EnableCheckInCheckOut = false;
            this.grdPayment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPayment.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPayment.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPayment.Location = new System.Drawing.Point(0, 89);
            // 
            // grdPayment
            // 
            this.grdPayment.MasterTemplate.AllowAddNewRow = false;
            this.grdPayment.MasterTemplate.AllowEditRow = false;
            this.grdPayment.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdPayment.Name = "grdPayment";
            this.grdPayment.PKFieldColumnName = "";
            this.grdPayment.ShowGroupPanel = false;
            this.grdPayment.ShowImageOnActionButton = true;
            this.grdPayment.Size = new System.Drawing.Size(609, 200);
            this.grdPayment.TabIndex = 97;
            this.grdPayment.Text = "myGridView1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtBalance);
            this.panel2.Controls.Add(this.numPayment);
            this.panel2.Controls.Add(this.radLabel5);
            this.panel2.Controls.Add(this.dtpPaymentDate);
            this.panel2.Controls.Add(this.radLabel4);
            this.panel2.Controls.Add(this.radLabel1);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(609, 71);
            this.panel2.TabIndex = 96;
            // 
            // txtBalance
            // 
            this.txtBalance.DecimalPlaces = 2;
            this.txtBalance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalance.Location = new System.Drawing.Point(257, 10);
            this.txtBalance.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtBalance.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.txtBalance.Name = "txtBalance";
            // 
            // 
            // 
            this.txtBalance.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.txtBalance.ShowBorder = true;
            this.txtBalance.ShowUpDownButtons = false;
            this.txtBalance.Size = new System.Drawing.Size(91, 21);
            this.txtBalance.TabIndex = 227;
            this.txtBalance.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.txtBalance.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtBalance.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtBalance.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtBalance.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numPayment
            // 
            this.numPayment.DecimalPlaces = 2;
            this.numPayment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPayment.Location = new System.Drawing.Point(99, 10);
            this.numPayment.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numPayment.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numPayment.Name = "numPayment";
            // 
            // 
            // 
            this.numPayment.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numPayment.ShowBorder = true;
            this.numPayment.ShowUpDownButtons = false;
            this.numPayment.Size = new System.Drawing.Size(86, 21);
            this.numPayment.TabIndex = 226;
            this.numPayment.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numPayment.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numPayment.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numPayment.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numPayment.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel5
            // 
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(358, 9);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(103, 22);
            this.radLabel5.TabIndex = 220;
            this.radLabel5.Text = "Payment Date";
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.Culture = new System.Globalization.CultureInfo("en-AU");
            this.dtpPaymentDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPaymentDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpPaymentDate.Location = new System.Drawing.Point(469, 8);
            this.dtpPaymentDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpPaymentDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPaymentDate.Size = new System.Drawing.Size(111, 24);
            this.dtpPaymentDate.TabIndex = 221;
            this.dtpPaymentDate.TabStop = false;
            this.dtpPaymentDate.Text = "radDateTimePicker1";
            this.dtpPaymentDate.Value = new System.DateTime(2011, 9, 21, 2, 48, 22, 638);
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.ForeColor = System.Drawing.Color.Red;
            this.radLabel4.Location = new System.Drawing.Point(191, 9);
            this.radLabel4.Name = "radLabel4";
            // 
            // 
            // 
            this.radLabel4.RootElement.ForeColor = System.Drawing.Color.Red;
            this.radLabel4.Size = new System.Drawing.Size(60, 22);
            this.radLabel4.TabIndex = 222;
            this.radLabel4.Text = "Balance";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(9, 9);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(67, 22);
            this.radLabel1.TabIndex = 220;
            this.radLabel1.Text = "Payment";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(468, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 25);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // radLabel11
            // 
            this.radLabel11.AutoSize = false;
            this.radLabel11.BackColor = System.Drawing.Color.Purple;
            this.radLabel11.BorderVisible = true;
            this.radLabel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.ForeColor = System.Drawing.Color.White;
            this.radLabel11.Location = new System.Drawing.Point(0, 0);
            this.radLabel11.Name = "radLabel11";
            // 
            // 
            // 
            this.radLabel11.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel11.Size = new System.Drawing.Size(609, 18);
            this.radLabel11.TabIndex = 95;
            this.radLabel11.Text = "Payment";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel11.GetChildAt(0))).BorderVisible = true;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel11.GetChildAt(0))).Text = "Payment";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;
            // 
            // txtInvoiceTotal
            // 
            this.txtInvoiceTotal.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceTotal.Location = new System.Drawing.Point(372, 89);
            this.txtInvoiceTotal.Name = "txtInvoiceTotal";
            this.txtInvoiceTotal.ReadOnly = true;
            this.txtInvoiceTotal.Size = new System.Drawing.Size(109, 24);
            this.txtInvoiceTotal.TabIndex = 219;
            this.txtInvoiceTotal.TabStop = false;
            // 
            // txtInvoiceno
            // 
            this.txtInvoiceno.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceno.Location = new System.Drawing.Point(111, 89);
            this.txtInvoiceno.Name = "txtInvoiceno";
            this.txtInvoiceno.ReadOnly = true;
            this.txtInvoiceno.Size = new System.Drawing.Size(109, 24);
            this.txtInvoiceno.TabIndex = 218;
            this.txtInvoiceno.TabStop = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(20, 90);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(80, 22);
            this.radLabel2.TabIndex = 216;
            this.radLabel2.Text = "Invoice No";
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.ForeColor = System.Drawing.Color.Red;
            this.radLabel3.Location = new System.Drawing.Point(269, 90);
            this.radLabel3.Name = "radLabel3";
            // 
            // 
            // 
            this.radLabel3.RootElement.ForeColor = System.Drawing.Color.Red;
            this.radLabel3.Size = new System.Drawing.Size(96, 22);
            this.radLabel3.TabIndex = 217;
            this.radLabel3.Text = "Invoice Total";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 221;
            this.label1.Text = "Company";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(381, 54);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(199, 16);
            this.lblMessage.TabIndex = 222;
            this.lblMessage.Text = "No Invoice exists of this Company";
            this.lblMessage.Visible = false;
            // 
            // ddlCompany
            // 
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.Location = new System.Drawing.Point(111, 49);
            this.ddlCompany.Name = "ddlCompany";
            // 
            // 
            // 
            this.ddlCompany.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlCompany.Size = new System.Drawing.Size(249, 24);
            this.ddlCompany.TabIndex = 250;
            this.ddlCompany.TabStop = false;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.ForeColor = System.Drawing.Color.Blue;
            this.lblDiscount.Location = new System.Drawing.Point(273, 115);
            this.lblDiscount.Name = "lblDiscount";
            // 
            // 
            // 
            this.lblDiscount.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.lblDiscount.Size = new System.Drawing.Size(2, 2);
            this.lblDiscount.TabIndex = 251;
            this.lblDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCreditNote
            // 
            this.txtCreditNote.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditNote.Location = new System.Drawing.Point(583, 89);
            this.txtCreditNote.Name = "txtCreditNote";
            this.txtCreditNote.ReadOnly = true;
            this.txtCreditNote.Size = new System.Drawing.Size(109, 24);
            this.txtCreditNote.TabIndex = 253;
            this.txtCreditNote.TabStop = false;
            this.txtCreditNote.Visible = false;
            // 
            // lblCreditNote
            // 
            this.lblCreditNote.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditNote.ForeColor = System.Drawing.Color.Red;
            this.lblCreditNote.Location = new System.Drawing.Point(496, 90);
            this.lblCreditNote.Name = "lblCreditNote";
            // 
            // 
            // 
            this.lblCreditNote.RootElement.ForeColor = System.Drawing.Color.Red;
            this.lblCreditNote.Size = new System.Drawing.Size(85, 22);
            this.lblCreditNote.TabIndex = 252;
            this.lblCreditNote.Text = "Credit Note";
            this.lblCreditNote.Visible = false;
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 450);
            this.ControlBox = true;
            this.Controls.Add(this.txtCreditNote);
            this.Controls.Add(this.lblCreditNote);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.ddlCompany);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInvoiceTotal);
            this.Controls.Add(this.txtInvoiceno);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.FormTitle = "Invoice Payment";
            this.Name = "frmPayment";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Invoice Payment";
            this.ThemeName = "ControlDefault";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.radLabel3, 0);
            this.Controls.SetChildIndex(this.radLabel2, 0);
            this.Controls.SetChildIndex(this.txtInvoiceno, 0);
            this.Controls.SetChildIndex(this.txtInvoiceTotal, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblMessage, 0);
            this.Controls.SetChildIndex(this.ddlCompany, 0);
            this.Controls.SetChildIndex(this.lblDiscount, 0);
            this.Controls.SetChildIndex(this.lblCreditNote, 0);
            this.Controls.SetChildIndex(this.txtCreditNote, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPayment.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayment)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPaymentDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreditNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCreditNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UI.MyGridView grdPayment;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAdd;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadTextBox txtInvoiceTotal;
        private Telerik.WinControls.UI.RadTextBox txtInvoiceno;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel1;

        private Telerik.WinControls.UI.RadLabel radLabel5;
     
        private UI.MyDatePicker dtpPaymentDate;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadSpinEditor numPayment;
        private System.Windows.Forms.Label lblMessage;
        private Telerik.WinControls.UI.RadSpinEditor txtBalance;
        private Telerik.WinControls.UI.RadComboBox ddlCompany;
        private Telerik.WinControls.UI.RadLabel lblDiscount;
        private Telerik.WinControls.UI.RadTextBox txtCreditNote;
        private Telerik.WinControls.UI.RadLabel lblCreditNote;
    }
}
