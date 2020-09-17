namespace Taxi_AppMain
{
    partial class frmCancelReason
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pnlMain = new Telerik.WinControls.UI.RadPanel();
            this.chkSendEmailtoCustomer = new Telerik.WinControls.UI.RadCheckBox();
            this.btnSaveCancelReason = new Telerik.WinControls.UI.RadButton();
            this.btnExitCancelForm = new Telerik.WinControls.UI.RadButton();
            this.txtCancelReason = new UI.AutoCompleteTextBox();
            this.lblCancelReason = new Telerik.WinControls.UI.RadLabel();
            this.chkCancellationSMS = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSendEmailtoCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCancelReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitCancelForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCancelReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCancelReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCancellationSMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(631, 233);
            this.btnSaveOn.Size = new System.Drawing.Size(37, 10);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(635, 162);
            this.btnOnNew.Size = new System.Drawing.Size(42, 10);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(711, 0);
            this.btnExit.Size = new System.Drawing.Size(77, 38);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(631, 279);
            this.btnSaveAndClose.Size = new System.Drawing.Size(10, 10);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(631, 461);
            this.btnSaveAndNew.Size = new System.Drawing.Size(10, 10);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(123, 58);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 20);
            this.textBox1.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.AllowShowFocusCues = true;
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.pnlMain.Controls.Add(this.chkCancellationSMS);
            this.pnlMain.Controls.Add(this.chkSendEmailtoCustomer);
            this.pnlMain.Controls.Add(this.btnSaveCancelReason);
            this.pnlMain.Controls.Add(this.btnExitCancelForm);
            this.pnlMain.Controls.Add(this.txtCancelReason);
            this.pnlMain.Controls.Add(this.lblCancelReason);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 38);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(783, 352);
            this.pnlMain.TabIndex = 107;
            this.pnlMain.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.pnlMain.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlMain.GetChildAt(0).GetChildAt(1))).Width = 0F;
            // 
            // chkSendEmailtoCustomer
            // 
            this.chkSendEmailtoCustomer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendEmailtoCustomer.ForeColor = System.Drawing.Color.Red;
            this.chkSendEmailtoCustomer.Location = new System.Drawing.Point(13, 281);
            this.chkSendEmailtoCustomer.Name = "chkSendEmailtoCustomer";
            // 
            // 
            // 
            this.chkSendEmailtoCustomer.RootElement.ForeColor = System.Drawing.Color.Red;
            this.chkSendEmailtoCustomer.Size = new System.Drawing.Size(162, 19);
            this.chkSendEmailtoCustomer.TabIndex = 210;
            this.chkSendEmailtoCustomer.Text = "Send Cancellation Email";
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.chkSendEmailtoCustomer.GetChildAt(0))).Text = "Send Cancellation Email";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkSendEmailtoCustomer.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkSendEmailtoCustomer.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSaveCancelReason
            // 
            this.btnSaveCancelReason.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCancelReason.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSaveCancelReason.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveCancelReason.Location = new System.Drawing.Point(468, 267);
            this.btnSaveCancelReason.Name = "btnSaveCancelReason";
            this.btnSaveCancelReason.Size = new System.Drawing.Size(112, 56);
            this.btnSaveCancelReason.TabIndex = 209;
            this.btnSaveCancelReason.Text = "Save (HOME)";
            this.btnSaveCancelReason.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveCancelReason.Click += new System.EventHandler(this.btnSaveCancelReason_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCancelReason.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCancelReason.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCancelReason.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCancelReason.GetChildAt(0))).Text = "Save (HOME)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveCancelReason.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveCancelReason.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitCancelForm
            // 
            this.btnExitCancelForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitCancelForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitCancelForm.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitCancelForm.Location = new System.Drawing.Point(608, 267);
            this.btnExitCancelForm.Name = "btnExitCancelForm";
            this.btnExitCancelForm.Size = new System.Drawing.Size(112, 56);
            this.btnExitCancelForm.TabIndex = 208;
            this.btnExitCancelForm.Text = "Exit";
            this.btnExitCancelForm.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExitCancelForm.Click += new System.EventHandler(this.btnExitCancelForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitCancelForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitCancelForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitCancelForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitCancelForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitCancelForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitCancelForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtCancelReason
            // 
            this.txtCancelReason.BackColor = System.Drawing.Color.White;
            this.txtCancelReason.DefaultHeight = 0;
            this.txtCancelReason.DefaultWidth = 0;
            this.txtCancelReason.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCancelReason.ForceListBoxToUpdate = false;
            this.txtCancelReason.FormerValue = "";
            this.txtCancelReason.Location = new System.Drawing.Point(150, 17);
            this.txtCancelReason.Multiline = true;
            this.txtCancelReason.Name = "txtCancelReason";
            // 
            // 
            // 
            this.txtCancelReason.RootElement.StretchVertically = true;
            this.txtCancelReason.SelectedItem = null;
            this.txtCancelReason.Size = new System.Drawing.Size(570, 232);
            this.txtCancelReason.TabIndex = 4;
            this.txtCancelReason.TabStop = false;
            this.txtCancelReason.Values = null;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtCancelReason.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtCancelReason.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtCancelReason.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtCancelReason.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtCancelReason.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // lblCancelReason
            // 
            this.lblCancelReason.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelReason.Location = new System.Drawing.Point(13, 21);
            this.lblCancelReason.Name = "lblCancelReason";
            this.lblCancelReason.Size = new System.Drawing.Size(131, 22);
            this.lblCancelReason.TabIndex = 134;
            this.lblCancelReason.Text = "Cancel Reason :";
            // 
            // chkCancellationSMS
            // 
            this.chkCancellationSMS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCancellationSMS.ForeColor = System.Drawing.Color.Red;
            this.chkCancellationSMS.Location = new System.Drawing.Point(13, 304);
            this.chkCancellationSMS.Name = "chkCancellationSMS";
            // 
            // 
            // 
            this.chkCancellationSMS.RootElement.ForeColor = System.Drawing.Color.Red;
            this.chkCancellationSMS.Size = new System.Drawing.Size(155, 19);
            this.chkCancellationSMS.TabIndex = 211;
            this.chkCancellationSMS.Text = "Send Cancellation SMS";
            this.chkCancellationSMS.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          //  this.chkCancellationSMS.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkCancellationSMS_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.chkCancellationSMS.GetChildAt(0))).ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.chkCancellationSMS.GetChildAt(0))).Text = "Send Cancellation SMS";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkCancellationSMS.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkCancellationSMS.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmCancelReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 390);
            this.ControlBox = true;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.textBox1);
            this.FixedExitButtonOnTopRight = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Cancel Reason";
            this.KeyPreview = true;
            this.Name = "frmCancelReason";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Cancel Reason";
            this.Shown += new System.EventHandler(this.frmCancelReason_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCancelReason_KeyDown);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSendEmailtoCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCancelReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitCancelForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCancelReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCancelReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCancellationSMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private Telerik.WinControls.UI.RadPanel pnlMain;
        private UI.AutoCompleteTextBox txtCancelReason;
        private Telerik.WinControls.UI.RadButton btnExitCancelForm;
        private Telerik.WinControls.UI.RadButton btnSaveCancelReason;
        private Telerik.WinControls.UI.RadLabel lblCancelReason;
        private Telerik.WinControls.UI.RadCheckBox chkSendEmailtoCustomer;
        private Telerik.WinControls.UI.RadCheckBox chkCancellationSMS;
    }
}