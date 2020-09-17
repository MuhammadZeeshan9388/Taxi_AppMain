using System;
namespace Taxi_AppMain
{
    partial class frmDriverPDASettings
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
            this.pnlSettings = new System.Windows.Forms.GroupBox();
            this.ddlHidePickupAndDestinationType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDisableJobAuth = new System.Windows.Forms.CheckBox();
            this.chkVoiceOnClearMeter = new System.Windows.Forms.CheckBox();
            this.lblcounter = new System.Windows.Forms.Label();
            this.chkEnablePriceBidding = new System.Windows.Forms.CheckBox();
            this.chkEnableManualFares = new System.Windows.Forms.CheckBox();
            this.chkEnableOptionalManualFares = new System.Windows.Forms.CheckBox();
            this.txtPDAVer = new System.Windows.Forms.Label();
            this.btnUpdateSettings = new Telerik.WinControls.UI.RadButton();
            this.chkDisableOnBreak = new System.Windows.Forms.CheckBox();
            this.chkShiftOverLogout = new System.Windows.Forms.CheckBox();
            this.chkDisableBase = new System.Windows.Forms.CheckBox();
            this.chkShowFareonExtraCharges = new System.Windows.Forms.CheckBox();
            this.chkEnableLogoutOnReject = new System.Windows.Forms.CheckBox();
            this.chkHidePickupAndDest = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numJobTimeout = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBiddingMessage = new System.Windows.Forms.Label();
            this.txtFareMessage = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numBreakDuration = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkDisableMeterAccJob = new System.Windows.Forms.CheckBox();
            this.chkEnableMeterWaitingCharges = new System.Windows.Forms.CheckBox();
            this.chkEnableOptionalMeter = new System.Windows.Forms.CheckBox();
            this.chkShowSoundOnZoneChange = new System.Windows.Forms.CheckBox();
            this.chkDisableChangeJobPlot = new System.Windows.Forms.CheckBox();
            this.chkDisableRank = new System.Windows.Forms.CheckBox();
            this.chkDisablePanic = new System.Windows.Forms.CheckBox();
            this.chkIgnoreArriveAction = new System.Windows.Forms.CheckBox();
            this.chkMessageStay = new System.Windows.Forms.CheckBox();
            this.ShowPlotOnJobOffer = new System.Windows.Forms.CheckBox();
            this.chkShowAlertOnJobLater = new System.Windows.Forms.CheckBox();
            this.chkShowCustomerMobileNo = new System.Windows.Forms.CheckBox();
            this.chkShowNavigation = new System.Windows.Forms.CheckBox();
            this.chkShowCompletedJobs = new System.Windows.Forms.CheckBox();
            this.chkShowPlots = new System.Windows.Forms.CheckBox();
            this.chkEnableAutoRotate = new System.Windows.Forms.CheckBox();
            this.chkEnableCompanyCars = new System.Windows.Forms.CheckBox();
            this.chkEnableJ15Jobs = new System.Windows.Forms.CheckBox();
            this.chkEnableCallCustomer = new System.Windows.Forms.CheckBox();
            this.chkEnableJobExtraCharges = new System.Windows.Forms.CheckBox();
            this.chkEnableRecoverJob = new System.Windows.Forms.CheckBox();
            this.chkEnableLogoutAuthorization = new System.Windows.Forms.CheckBox();
            this.chkEnableFlagDown = new System.Windows.Forms.CheckBox();
            this.chkEnableFareMeter = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEnableBidding = new System.Windows.Forms.CheckBox();
            this.ddlNavigation = new System.Windows.Forms.ComboBox();
            this.chkDisableChangeDest = new System.Windows.Forms.CheckBox();
            this.chkDisableRejectJob = new System.Windows.Forms.CheckBox();
            this.chkDisableSTC = new System.Windows.Forms.CheckBox();
            this.chkDisableFareOnAccJob = new System.Windows.Forms.CheckBox();
            this.chkShowSpecReq = new System.Windows.Forms.CheckBox();
            this.chkDisableAlarm = new System.Windows.Forms.CheckBox();
            this.chkDisableNoPickup = new System.Windows.Forms.CheckBox();
            this.chkShowJobAsAlert = new System.Windows.Forms.CheckBox();
            this.grdDriverPDASettings = new UI.MyGridView();
            this.chkAllDriver = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdoLoginDriver = new System.Windows.Forms.RadioButton();
            this.rdoAllDriver = new System.Windows.Forms.RadioButton();
            this.chkShowDestAfterPOB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBreakDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverPDASettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverPDASettings.MasterTemplate)).BeginInit();
            this.grdDriverPDASettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlSettings.Controls.Add(this.chkShowDestAfterPOB);
            this.pnlSettings.Controls.Add(this.ddlHidePickupAndDestinationType);
            this.pnlSettings.Controls.Add(this.label1);
            this.pnlSettings.Controls.Add(this.chkDisableJobAuth);
            this.pnlSettings.Controls.Add(this.chkVoiceOnClearMeter);
            this.pnlSettings.Controls.Add(this.lblcounter);
            this.pnlSettings.Controls.Add(this.chkEnablePriceBidding);
            this.pnlSettings.Controls.Add(this.chkEnableManualFares);
            this.pnlSettings.Controls.Add(this.chkEnableOptionalManualFares);
            this.pnlSettings.Controls.Add(this.txtPDAVer);
            this.pnlSettings.Controls.Add(this.btnUpdateSettings);
            this.pnlSettings.Controls.Add(this.chkDisableOnBreak);
            this.pnlSettings.Controls.Add(this.chkShiftOverLogout);
            this.pnlSettings.Controls.Add(this.chkDisableBase);
            this.pnlSettings.Controls.Add(this.chkShowFareonExtraCharges);
            this.pnlSettings.Controls.Add(this.chkEnableLogoutOnReject);
            this.pnlSettings.Controls.Add(this.chkHidePickupAndDest);
            this.pnlSettings.Controls.Add(this.label7);
            this.pnlSettings.Controls.Add(this.numJobTimeout);
            this.pnlSettings.Controls.Add(this.label8);
            this.pnlSettings.Controls.Add(this.txtBiddingMessage);
            this.pnlSettings.Controls.Add(this.txtFareMessage);
            this.pnlSettings.Controls.Add(this.label6);
            this.pnlSettings.Controls.Add(this.numBreakDuration);
            this.pnlSettings.Controls.Add(this.label5);
            this.pnlSettings.Controls.Add(this.chkDisableMeterAccJob);
            this.pnlSettings.Controls.Add(this.chkEnableMeterWaitingCharges);
            this.pnlSettings.Controls.Add(this.chkEnableOptionalMeter);
            this.pnlSettings.Controls.Add(this.chkShowSoundOnZoneChange);
            this.pnlSettings.Controls.Add(this.chkDisableChangeJobPlot);
            this.pnlSettings.Controls.Add(this.chkDisableRank);
            this.pnlSettings.Controls.Add(this.chkDisablePanic);
            this.pnlSettings.Controls.Add(this.chkIgnoreArriveAction);
            this.pnlSettings.Controls.Add(this.chkMessageStay);
            this.pnlSettings.Controls.Add(this.ShowPlotOnJobOffer);
            this.pnlSettings.Controls.Add(this.chkShowAlertOnJobLater);
            this.pnlSettings.Controls.Add(this.chkShowCustomerMobileNo);
            this.pnlSettings.Controls.Add(this.chkShowNavigation);
            this.pnlSettings.Controls.Add(this.chkShowCompletedJobs);
            this.pnlSettings.Controls.Add(this.chkShowPlots);
            this.pnlSettings.Controls.Add(this.chkEnableAutoRotate);
            this.pnlSettings.Controls.Add(this.chkEnableCompanyCars);
            this.pnlSettings.Controls.Add(this.chkEnableJ15Jobs);
            this.pnlSettings.Controls.Add(this.chkEnableCallCustomer);
            this.pnlSettings.Controls.Add(this.chkEnableJobExtraCharges);
            this.pnlSettings.Controls.Add(this.chkEnableRecoverJob);
            this.pnlSettings.Controls.Add(this.chkEnableLogoutAuthorization);
            this.pnlSettings.Controls.Add(this.chkEnableFlagDown);
            this.pnlSettings.Controls.Add(this.chkEnableFareMeter);
            this.pnlSettings.Controls.Add(this.label4);
            this.pnlSettings.Controls.Add(this.chkEnableBidding);
            this.pnlSettings.Controls.Add(this.ddlNavigation);
            this.pnlSettings.Controls.Add(this.chkDisableChangeDest);
            this.pnlSettings.Controls.Add(this.chkDisableRejectJob);
            this.pnlSettings.Controls.Add(this.chkDisableSTC);
            this.pnlSettings.Controls.Add(this.chkDisableFareOnAccJob);
            this.pnlSettings.Controls.Add(this.chkShowSpecReq);
            this.pnlSettings.Controls.Add(this.chkDisableAlarm);
            this.pnlSettings.Controls.Add(this.chkDisableNoPickup);
            this.pnlSettings.Controls.Add(this.chkShowJobAsAlert);
            this.pnlSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSettings.Location = new System.Drawing.Point(2, 40);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(909, 721);
            this.pnlSettings.TabIndex = 12;
            this.pnlSettings.TabStop = false;
            this.pnlSettings.Text = "Settings";
            // 
            // ddlHidePickupAndDestinationType
            // 
            this.ddlHidePickupAndDestinationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlHidePickupAndDestinationType.FormattingEnabled = true;
            this.ddlHidePickupAndDestinationType.Items.AddRange(new object[] {
            "Hide All",
            "Show Pickup Only",
            "Show Pickup/Vehicle"});
            this.ddlHidePickupAndDestinationType.Location = new System.Drawing.Point(708, 333);
            this.ddlHidePickupAndDestinationType.Name = "ddlHidePickupAndDestinationType";
            this.ddlHidePickupAndDestinationType.Size = new System.Drawing.Size(160, 22);
            this.ddlHidePickupAndDestinationType.TabIndex = 63;
            this.ddlHidePickupAndDestinationType.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Note : Please Login your Driver atleast one time to come up on the list";
            this.label1.Visible = false;
            // 
            // chkDisableJobAuth
            // 
            this.chkDisableJobAuth.AutoSize = true;
            this.chkDisableJobAuth.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableJobAuth.Location = new System.Drawing.Point(480, 576);
            this.chkDisableJobAuth.Name = "chkDisableJobAuth";
            this.chkDisableJobAuth.Size = new System.Drawing.Size(134, 22);
            this.chkDisableJobAuth.TabIndex = 61;
            this.chkDisableJobAuth.Text = "Disable Job Auth";
            this.chkDisableJobAuth.UseVisualStyleBackColor = true;
            // 
            // chkVoiceOnClearMeter
            // 
            this.chkVoiceOnClearMeter.AutoSize = true;
            this.chkVoiceOnClearMeter.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkVoiceOnClearMeter.ForeColor = System.Drawing.Color.Blue;
            this.chkVoiceOnClearMeter.Location = new System.Drawing.Point(681, 134);
            this.chkVoiceOnClearMeter.Name = "chkVoiceOnClearMeter";
            this.chkVoiceOnClearMeter.Size = new System.Drawing.Size(151, 21);
            this.chkVoiceOnClearMeter.TabIndex = 58;
            this.chkVoiceOnClearMeter.Text = "Voice on Clear Meter";
            this.chkVoiceOnClearMeter.UseVisualStyleBackColor = true;
            // 
            // lblcounter
            // 
            this.lblcounter.AutoSize = true;
            this.lblcounter.Location = new System.Drawing.Point(517, 62);
            this.lblcounter.Name = "lblcounter";
            this.lblcounter.Size = new System.Drawing.Size(0, 14);
            this.lblcounter.TabIndex = 57;
            this.lblcounter.Visible = false;
            // 
            // chkEnablePriceBidding
            // 
            this.chkEnablePriceBidding.AutoSize = true;
            this.chkEnablePriceBidding.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnablePriceBidding.Location = new System.Drawing.Point(17, 536);
            this.chkEnablePriceBidding.Name = "chkEnablePriceBidding";
            this.chkEnablePriceBidding.Size = new System.Drawing.Size(154, 22);
            this.chkEnablePriceBidding.TabIndex = 56;
            this.chkEnablePriceBidding.Text = "Enable Price Bidding";
            this.chkEnablePriceBidding.UseVisualStyleBackColor = true;
            // 
            // chkEnableManualFares
            // 
            this.chkEnableManualFares.AutoSize = true;
            this.chkEnableManualFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableManualFares.Location = new System.Drawing.Point(17, 471);
            this.chkEnableManualFares.Name = "chkEnableManualFares";
            this.chkEnableManualFares.Size = new System.Drawing.Size(161, 22);
            this.chkEnableManualFares.TabIndex = 55;
            this.chkEnableManualFares.Text = "Enable Manual Fares";
            this.chkEnableManualFares.UseVisualStyleBackColor = true;
            this.chkEnableManualFares.CheckedChanged += new System.EventHandler(this.chkEnableManualFares_CheckedChanged);
            // 
            // chkEnableOptionalManualFares
            // 
            this.chkEnableOptionalManualFares.AutoSize = true;
            this.chkEnableOptionalManualFares.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableOptionalManualFares.Location = new System.Drawing.Point(40, 495);
            this.chkEnableOptionalManualFares.Name = "chkEnableOptionalManualFares";
            this.chkEnableOptionalManualFares.Size = new System.Drawing.Size(81, 18);
            this.chkEnableOptionalManualFares.TabIndex = 55;
            this.chkEnableOptionalManualFares.Text = "(Optional)";
            this.chkEnableOptionalManualFares.UseVisualStyleBackColor = true;
            // 
            // txtPDAVer
            // 
            this.txtPDAVer.AutoSize = true;
            this.txtPDAVer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPDAVer.Location = new System.Drawing.Point(517, 98);
            this.txtPDAVer.Name = "txtPDAVer";
            this.txtPDAVer.Size = new System.Drawing.Size(170, 18);
            this.txtPDAVer.TabIndex = 54;
            this.txtPDAVer.Text = "Current PDA Version :";
            this.txtPDAVer.Visible = false;
            // 
            // btnUpdateSettings
            // 
            this.btnUpdateSettings.Location = new System.Drawing.Point(668, 545);
            this.btnUpdateSettings.Name = "btnUpdateSettings";
            this.btnUpdateSettings.Size = new System.Drawing.Size(184, 67);
            this.btnUpdateSettings.TabIndex = 53;
            this.btnUpdateSettings.Text = "Update Settings >>";
            this.btnUpdateSettings.Click += new System.EventHandler(this.btnUpdateSettings_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnUpdateSettings.GetChildAt(0))).Text = "Update Settings >>";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnUpdateSettings.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnUpdateSettings.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkDisableOnBreak
            // 
            this.chkDisableOnBreak.AutoSize = true;
            this.chkDisableOnBreak.BackColor = System.Drawing.Color.Orange;
            this.chkDisableOnBreak.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableOnBreak.Location = new System.Drawing.Point(480, 301);
            this.chkDisableOnBreak.Name = "chkDisableOnBreak";
            this.chkDisableOnBreak.Size = new System.Drawing.Size(133, 22);
            this.chkDisableOnBreak.TabIndex = 52;
            this.chkDisableOnBreak.Text = "Disable OnBreak";
            this.chkDisableOnBreak.UseVisualStyleBackColor = false;
            // 
            // chkShiftOverLogout
            // 
            this.chkShiftOverLogout.AutoSize = true;
            this.chkShiftOverLogout.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShiftOverLogout.Location = new System.Drawing.Point(680, 436);
            this.chkShiftOverLogout.Name = "chkShiftOverLogout";
            this.chkShiftOverLogout.Size = new System.Drawing.Size(162, 22);
            this.chkShiftOverLogout.TabIndex = 51;
            this.chkShiftOverLogout.Text = "Logout on Shift Over";
            this.chkShiftOverLogout.UseVisualStyleBackColor = true;
            // 
            // chkDisableBase
            // 
            this.chkDisableBase.AutoSize = true;
            this.chkDisableBase.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableBase.Location = new System.Drawing.Point(480, 409);
            this.chkDisableBase.Name = "chkDisableBase";
            this.chkDisableBase.Size = new System.Drawing.Size(109, 22);
            this.chkDisableBase.TabIndex = 50;
            this.chkDisableBase.Text = "Disable Base";
            this.chkDisableBase.UseVisualStyleBackColor = true;
            // 
            // chkShowFareonExtraCharges
            // 
            this.chkShowFareonExtraCharges.AutoSize = true;
            this.chkShowFareonExtraCharges.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowFareonExtraCharges.Location = new System.Drawing.Point(241, 533);
            this.chkShowFareonExtraCharges.Name = "chkShowFareonExtraCharges";
            this.chkShowFareonExtraCharges.Size = new System.Drawing.Size(220, 22);
            this.chkShowFareonExtraCharges.TabIndex = 49;
            this.chkShowFareonExtraCharges.Text = "Show Fares on Extra Charges";
            this.chkShowFareonExtraCharges.UseVisualStyleBackColor = true;
            this.chkShowFareonExtraCharges.Visible = false;
            // 
            // chkEnableLogoutOnReject
            // 
            this.chkEnableLogoutOnReject.AutoSize = true;
            this.chkEnableLogoutOnReject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableLogoutOnReject.Location = new System.Drawing.Point(17, 437);
            this.chkEnableLogoutOnReject.Name = "chkEnableLogoutOnReject";
            this.chkEnableLogoutOnReject.Size = new System.Drawing.Size(212, 22);
            this.chkEnableLogoutOnReject.TabIndex = 48;
            this.chkEnableLogoutOnReject.Text = "Enable Logout on Reject Job";
            this.chkEnableLogoutOnReject.UseVisualStyleBackColor = true;
            // 
            // chkHidePickupAndDest
            // 
            this.chkHidePickupAndDest.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkHidePickupAndDest.FlatAppearance.BorderSize = 2;
            this.chkHidePickupAndDest.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.chkHidePickupAndDest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.chkHidePickupAndDest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.chkHidePickupAndDest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkHidePickupAndDest.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHidePickupAndDest.Location = new System.Drawing.Point(679, 296);
            this.chkHidePickupAndDest.Name = "chkHidePickupAndDest";
            this.chkHidePickupAndDest.Size = new System.Drawing.Size(213, 65);
            this.chkHidePickupAndDest.TabIndex = 47;
            this.chkHidePickupAndDest.Text = "Hide Pickup And Destination On Job Offer";
            this.chkHidePickupAndDest.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkHidePickupAndDest.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(868, 268);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 14);
            this.label7.TabIndex = 46;
            this.label7.Text = "(sec)";
            // 
            // numJobTimeout
            // 
            this.numJobTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numJobTimeout.Location = new System.Drawing.Point(812, 264);
            this.numJobTimeout.Name = "numJobTimeout";
            this.numJobTimeout.Size = new System.Drawing.Size(51, 22);
            this.numJobTimeout.TabIndex = 45;
            this.numJobTimeout.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(679, 266);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 16);
            this.label8.TabIndex = 44;
            this.label8.Text = "Job Offer Timeout :";
            // 
            // txtBiddingMessage
            // 
            this.txtBiddingMessage.AutoSize = true;
            this.txtBiddingMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtBiddingMessage.ForeColor = System.Drawing.Color.Red;
            this.txtBiddingMessage.Location = new System.Drawing.Point(140, 104);
            this.txtBiddingMessage.Name = "txtBiddingMessage";
            this.txtBiddingMessage.Size = new System.Drawing.Size(283, 14);
            this.txtBiddingMessage.TabIndex = 43;
            this.txtBiddingMessage.Text = "Problem on getting Bidding Info from Server";
            this.txtBiddingMessage.Visible = false;
            // 
            // txtFareMessage
            // 
            this.txtFareMessage.AutoSize = true;
            this.txtFareMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtFareMessage.ForeColor = System.Drawing.Color.Red;
            this.txtFareMessage.Location = new System.Drawing.Point(32, 159);
            this.txtFareMessage.Name = "txtFareMessage";
            this.txtFareMessage.Size = new System.Drawing.Size(238, 14);
            this.txtFareMessage.TabIndex = 42;
            this.txtFareMessage.Text = "Problem on getting Fares from Server";
            this.txtFareMessage.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(859, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 14);
            this.label6.TabIndex = 41;
            this.label6.Text = "(mins)";
            // 
            // numBreakDuration
            // 
            this.numBreakDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBreakDuration.Location = new System.Drawing.Point(799, 224);
            this.numBreakDuration.Name = "numBreakDuration";
            this.numBreakDuration.Size = new System.Drawing.Size(53, 22);
            this.numBreakDuration.TabIndex = 40;
            this.numBreakDuration.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(679, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 16);
            this.label5.TabIndex = 39;
            this.label5.Text = "Break Duration :";
            // 
            // chkDisableMeterAccJob
            // 
            this.chkDisableMeterAccJob.AutoSize = true;
            this.chkDisableMeterAccJob.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkDisableMeterAccJob.ForeColor = System.Drawing.Color.Blue;
            this.chkDisableMeterAccJob.Location = new System.Drawing.Point(307, 135);
            this.chkDisableMeterAccJob.Name = "chkDisableMeterAccJob";
            this.chkDisableMeterAccJob.Size = new System.Drawing.Size(189, 21);
            this.chkDisableMeterAccJob.TabIndex = 38;
            this.chkDisableMeterAccJob.Text = "Disable Meter For Acc Jobs";
            this.chkDisableMeterAccJob.UseVisualStyleBackColor = true;
            // 
            // chkEnableMeterWaitingCharges
            // 
            this.chkEnableMeterWaitingCharges.AutoSize = true;
            this.chkEnableMeterWaitingCharges.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkEnableMeterWaitingCharges.ForeColor = System.Drawing.Color.Blue;
            this.chkEnableMeterWaitingCharges.Location = new System.Drawing.Point(510, 135);
            this.chkEnableMeterWaitingCharges.Name = "chkEnableMeterWaitingCharges";
            this.chkEnableMeterWaitingCharges.Size = new System.Drawing.Size(165, 21);
            this.chkEnableMeterWaitingCharges.TabIndex = 37;
            this.chkEnableMeterWaitingCharges.Text = "Meter Waiting Charges";
            this.chkEnableMeterWaitingCharges.UseVisualStyleBackColor = true;
            // 
            // chkEnableOptionalMeter
            // 
            this.chkEnableOptionalMeter.AutoSize = true;
            this.chkEnableOptionalMeter.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkEnableOptionalMeter.ForeColor = System.Drawing.Color.Blue;
            this.chkEnableOptionalMeter.Location = new System.Drawing.Point(177, 135);
            this.chkEnableOptionalMeter.Name = "chkEnableOptionalMeter";
            this.chkEnableOptionalMeter.Size = new System.Drawing.Size(115, 21);
            this.chkEnableOptionalMeter.TabIndex = 36;
            this.chkEnableOptionalMeter.Text = "Optional Meter";
            this.chkEnableOptionalMeter.UseVisualStyleBackColor = true;
            // 
            // chkShowSoundOnZoneChange
            // 
            this.chkShowSoundOnZoneChange.AutoSize = true;
            this.chkShowSoundOnZoneChange.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowSoundOnZoneChange.Location = new System.Drawing.Point(677, 189);
            this.chkShowSoundOnZoneChange.Name = "chkShowSoundOnZoneChange";
            this.chkShowSoundOnZoneChange.Size = new System.Drawing.Size(175, 20);
            this.chkShowSoundOnZoneChange.TabIndex = 31;
            this.chkShowSoundOnZoneChange.Text = "Sound On Zone Change";
            this.chkShowSoundOnZoneChange.UseVisualStyleBackColor = true;
            // 
            // chkDisableChangeJobPlot
            // 
            this.chkDisableChangeJobPlot.AutoSize = true;
            this.chkDisableChangeJobPlot.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableChangeJobPlot.Location = new System.Drawing.Point(480, 263);
            this.chkDisableChangeJobPlot.Name = "chkDisableChangeJobPlot";
            this.chkDisableChangeJobPlot.Size = new System.Drawing.Size(181, 22);
            this.chkDisableChangeJobPlot.TabIndex = 30;
            this.chkDisableChangeJobPlot.Text = "Disable Change Job Plot";
            this.chkDisableChangeJobPlot.UseVisualStyleBackColor = true;
            // 
            // chkDisableRank
            // 
            this.chkDisableRank.AutoSize = true;
            this.chkDisableRank.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableRank.Location = new System.Drawing.Point(480, 225);
            this.chkDisableRank.Name = "chkDisableRank";
            this.chkDisableRank.Size = new System.Drawing.Size(152, 22);
            this.chkDisableRank.TabIndex = 29;
            this.chkDisableRank.Text = "Disable Driver Rank";
            this.chkDisableRank.UseVisualStyleBackColor = true;
            // 
            // chkDisablePanic
            // 
            this.chkDisablePanic.AutoSize = true;
            this.chkDisablePanic.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisablePanic.Location = new System.Drawing.Point(480, 189);
            this.chkDisablePanic.Name = "chkDisablePanic";
            this.chkDisablePanic.Size = new System.Drawing.Size(158, 22);
            this.chkDisablePanic.TabIndex = 28;
            this.chkDisablePanic.Text = "Disable Panic Button";
            this.chkDisablePanic.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreArriveAction
            // 
            this.chkIgnoreArriveAction.AutoSize = true;
            this.chkIgnoreArriveAction.BackColor = System.Drawing.Color.Pink;
            this.chkIgnoreArriveAction.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIgnoreArriveAction.Location = new System.Drawing.Point(679, 408);
            this.chkIgnoreArriveAction.Name = "chkIgnoreArriveAction";
            this.chkIgnoreArriveAction.Size = new System.Drawing.Size(156, 22);
            this.chkIgnoreArriveAction.TabIndex = 27;
            this.chkIgnoreArriveAction.Text = "Ignore Arrive Action";
            this.chkIgnoreArriveAction.UseVisualStyleBackColor = false;
            // 
            // chkMessageStay
            // 
            this.chkMessageStay.AutoSize = true;
            this.chkMessageStay.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMessageStay.Location = new System.Drawing.Point(679, 380);
            this.chkMessageStay.Name = "chkMessageStay";
            this.chkMessageStay.Size = new System.Drawing.Size(189, 22);
            this.chkMessageStay.TabIndex = 26;
            this.chkMessageStay.Text = "Message Stay on Screen";
            this.chkMessageStay.UseVisualStyleBackColor = true;
            // 
            // ShowPlotOnJobOffer
            // 
            this.ShowPlotOnJobOffer.AutoSize = true;
            this.ShowPlotOnJobOffer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowPlotOnJobOffer.Location = new System.Drawing.Point(241, 371);
            this.ShowPlotOnJobOffer.Name = "ShowPlotOnJobOffer";
            this.ShowPlotOnJobOffer.Size = new System.Drawing.Size(176, 22);
            this.ShowPlotOnJobOffer.TabIndex = 25;
            this.ShowPlotOnJobOffer.Text = "Show Plot on Job Offer";
            this.ShowPlotOnJobOffer.UseVisualStyleBackColor = true;
            // 
            // chkShowAlertOnJobLater
            // 
            this.chkShowAlertOnJobLater.AutoSize = true;
            this.chkShowAlertOnJobLater.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowAlertOnJobLater.Location = new System.Drawing.Point(241, 336);
            this.chkShowAlertOnJobLater.Name = "chkShowAlertOnJobLater";
            this.chkShowAlertOnJobLater.Size = new System.Drawing.Size(174, 22);
            this.chkShowAlertOnJobLater.TabIndex = 24;
            this.chkShowAlertOnJobLater.Text = "Show Alert On JobLate";
            this.chkShowAlertOnJobLater.UseVisualStyleBackColor = true;
            // 
            // chkShowCustomerMobileNo
            // 
            this.chkShowCustomerMobileNo.AutoSize = true;
            this.chkShowCustomerMobileNo.BackColor = System.Drawing.Color.LightGreen;
            this.chkShowCustomerMobileNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowCustomerMobileNo.Location = new System.Drawing.Point(241, 299);
            this.chkShowCustomerMobileNo.Name = "chkShowCustomerMobileNo";
            this.chkShowCustomerMobileNo.Size = new System.Drawing.Size(197, 22);
            this.chkShowCustomerMobileNo.TabIndex = 23;
            this.chkShowCustomerMobileNo.Text = "Show Customer Mobile No";
            this.chkShowCustomerMobileNo.UseVisualStyleBackColor = false;
            // 
            // chkShowNavigation
            // 
            this.chkShowNavigation.AutoSize = true;
            this.chkShowNavigation.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowNavigation.Location = new System.Drawing.Point(241, 261);
            this.chkShowNavigation.Name = "chkShowNavigation";
            this.chkShowNavigation.Size = new System.Drawing.Size(133, 22);
            this.chkShowNavigation.TabIndex = 22;
            this.chkShowNavigation.Text = "Show Navigation";
            this.chkShowNavigation.UseVisualStyleBackColor = true;
            // 
            // chkShowCompletedJobs
            // 
            this.chkShowCompletedJobs.AutoSize = true;
            this.chkShowCompletedJobs.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowCompletedJobs.Location = new System.Drawing.Point(241, 225);
            this.chkShowCompletedJobs.Name = "chkShowCompletedJobs";
            this.chkShowCompletedJobs.Size = new System.Drawing.Size(169, 22);
            this.chkShowCompletedJobs.TabIndex = 21;
            this.chkShowCompletedJobs.Text = "Show Completed Jobs";
            this.chkShowCompletedJobs.UseVisualStyleBackColor = true;
            // 
            // chkShowPlots
            // 
            this.chkShowPlots.AutoSize = true;
            this.chkShowPlots.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPlots.Location = new System.Drawing.Point(241, 189);
            this.chkShowPlots.Name = "chkShowPlots";
            this.chkShowPlots.Size = new System.Drawing.Size(96, 22);
            this.chkShowPlots.TabIndex = 20;
            this.chkShowPlots.Text = "Show Plots";
            this.chkShowPlots.UseVisualStyleBackColor = true;
            // 
            // chkEnableAutoRotate
            // 
            this.chkEnableAutoRotate.AutoSize = true;
            this.chkEnableAutoRotate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableAutoRotate.Location = new System.Drawing.Point(17, 403);
            this.chkEnableAutoRotate.Name = "chkEnableAutoRotate";
            this.chkEnableAutoRotate.Size = new System.Drawing.Size(196, 22);
            this.chkEnableAutoRotate.TabIndex = 19;
            this.chkEnableAutoRotate.Text = "Enable AutoRotate Screen";
            this.chkEnableAutoRotate.UseVisualStyleBackColor = true;
            // 
            // chkEnableCompanyCars
            // 
            this.chkEnableCompanyCars.AutoSize = true;
            this.chkEnableCompanyCars.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableCompanyCars.Location = new System.Drawing.Point(17, 369);
            this.chkEnableCompanyCars.Name = "chkEnableCompanyCars";
            this.chkEnableCompanyCars.Size = new System.Drawing.Size(170, 22);
            this.chkEnableCompanyCars.TabIndex = 18;
            this.chkEnableCompanyCars.Text = "Enable Company Cars";
            this.chkEnableCompanyCars.UseVisualStyleBackColor = true;
            // 
            // chkEnableJ15Jobs
            // 
            this.chkEnableJ15Jobs.AutoSize = true;
            this.chkEnableJ15Jobs.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableJ15Jobs.Location = new System.Drawing.Point(17, 333);
            this.chkEnableJ15Jobs.Name = "chkEnableJ15Jobs";
            this.chkEnableJ15Jobs.Size = new System.Drawing.Size(187, 22);
            this.chkEnableJ15Jobs.TabIndex = 17;
            this.chkEnableJ15Jobs.Text = "Enable J15 And J30 Jobs";
            this.chkEnableJ15Jobs.UseVisualStyleBackColor = true;
            // 
            // chkEnableCallCustomer
            // 
            this.chkEnableCallCustomer.AutoSize = true;
            this.chkEnableCallCustomer.BackColor = System.Drawing.Color.LightGreen;
            this.chkEnableCallCustomer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableCallCustomer.Location = new System.Drawing.Point(17, 298);
            this.chkEnableCallCustomer.Name = "chkEnableCallCustomer";
            this.chkEnableCallCustomer.Size = new System.Drawing.Size(163, 22);
            this.chkEnableCallCustomer.TabIndex = 16;
            this.chkEnableCallCustomer.Text = "Enable Call Customer";
            this.chkEnableCallCustomer.UseVisualStyleBackColor = false;
            // 
            // chkEnableJobExtraCharges
            // 
            this.chkEnableJobExtraCharges.AutoSize = true;
            this.chkEnableJobExtraCharges.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableJobExtraCharges.Location = new System.Drawing.Point(240, 505);
            this.chkEnableJobExtraCharges.Name = "chkEnableJobExtraCharges";
            this.chkEnableJobExtraCharges.Size = new System.Drawing.Size(193, 22);
            this.chkEnableJobExtraCharges.TabIndex = 15;
            this.chkEnableJobExtraCharges.Text = "Enable Job Extra Charges";
            this.chkEnableJobExtraCharges.UseVisualStyleBackColor = true;
            this.chkEnableJobExtraCharges.Visible = false;
            // 
            // chkEnableRecoverJob
            // 
            this.chkEnableRecoverJob.AutoSize = true;
            this.chkEnableRecoverJob.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableRecoverJob.Location = new System.Drawing.Point(17, 261);
            this.chkEnableRecoverJob.Name = "chkEnableRecoverJob";
            this.chkEnableRecoverJob.Size = new System.Drawing.Size(154, 22);
            this.chkEnableRecoverJob.TabIndex = 14;
            this.chkEnableRecoverJob.Text = "Enable Recover Job";
            this.chkEnableRecoverJob.UseVisualStyleBackColor = true;
            // 
            // chkEnableLogoutAuthorization
            // 
            this.chkEnableLogoutAuthorization.AutoSize = true;
            this.chkEnableLogoutAuthorization.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableLogoutAuthorization.Location = new System.Drawing.Point(17, 225);
            this.chkEnableLogoutAuthorization.Name = "chkEnableLogoutAuthorization";
            this.chkEnableLogoutAuthorization.Size = new System.Drawing.Size(206, 22);
            this.chkEnableLogoutAuthorization.TabIndex = 13;
            this.chkEnableLogoutAuthorization.Text = "Enable Logout Authorization";
            this.chkEnableLogoutAuthorization.UseVisualStyleBackColor = true;
            // 
            // chkEnableFlagDown
            // 
            this.chkEnableFlagDown.AutoSize = true;
            this.chkEnableFlagDown.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableFlagDown.Location = new System.Drawing.Point(17, 189);
            this.chkEnableFlagDown.Name = "chkEnableFlagDown";
            this.chkEnableFlagDown.Size = new System.Drawing.Size(141, 22);
            this.chkEnableFlagDown.TabIndex = 12;
            this.chkEnableFlagDown.Text = "Enable Flag Down";
            this.chkEnableFlagDown.UseVisualStyleBackColor = true;
            // 
            // chkEnableFareMeter
            // 
            this.chkEnableFareMeter.AutoSize = true;
            this.chkEnableFareMeter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableFareMeter.Location = new System.Drawing.Point(17, 134);
            this.chkEnableFareMeter.Name = "chkEnableFareMeter";
            this.chkEnableFareMeter.Size = new System.Drawing.Size(141, 22);
            this.chkEnableFareMeter.TabIndex = 11;
            this.chkEnableFareMeter.Text = "Enable FareMeter";
            this.chkEnableFareMeter.UseVisualStyleBackColor = true;
            this.chkEnableFareMeter.CheckedChanged += new System.EventHandler(this.chkEnableFareMeter_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Navigation App :";
            // 
            // chkEnableBidding
            // 
            this.chkEnableBidding.AutoSize = true;
            this.chkEnableBidding.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableBidding.Location = new System.Drawing.Point(17, 100);
            this.chkEnableBidding.Name = "chkEnableBidding";
            this.chkEnableBidding.Size = new System.Drawing.Size(119, 22);
            this.chkEnableBidding.TabIndex = 8;
            this.chkEnableBidding.Text = "Enable Bidding";
            this.chkEnableBidding.UseVisualStyleBackColor = true;
            // 
            // ddlNavigation
            // 
            this.ddlNavigation.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlNavigation.FormattingEnabled = true;
            this.ddlNavigation.Items.AddRange(new object[] {
            "All",
            "Google Navigation",
            "Waze Navigation",
            "Here we go!",
            "None"});
            this.ddlNavigation.Location = new System.Drawing.Point(141, 55);
            this.ddlNavigation.Name = "ddlNavigation";
            this.ddlNavigation.Size = new System.Drawing.Size(336, 26);
            this.ddlNavigation.TabIndex = 9;
            // 
            // chkDisableChangeDest
            // 
            this.chkDisableChangeDest.BackColor = System.Drawing.Color.Transparent;
            this.chkDisableChangeDest.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableChangeDest.Location = new System.Drawing.Point(480, 363);
            this.chkDisableChangeDest.Name = "chkDisableChangeDest";
            this.chkDisableChangeDest.Size = new System.Drawing.Size(170, 43);
            this.chkDisableChangeDest.TabIndex = 1;
            this.chkDisableChangeDest.Text = "Disable Change Job Destination";
            this.chkDisableChangeDest.UseVisualStyleBackColor = false;
            // 
            // chkDisableRejectJob
            // 
            this.chkDisableRejectJob.BackColor = System.Drawing.Color.Transparent;
            this.chkDisableRejectJob.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableRejectJob.Location = new System.Drawing.Point(480, 339);
            this.chkDisableRejectJob.Name = "chkDisableRejectJob";
            this.chkDisableRejectJob.Size = new System.Drawing.Size(146, 22);
            this.chkDisableRejectJob.TabIndex = 0;
            this.chkDisableRejectJob.Text = "Disable Reject Job";
            this.chkDisableRejectJob.UseVisualStyleBackColor = false;
            // 
            // chkDisableSTC
            // 
            this.chkDisableSTC.AutoSize = true;
            this.chkDisableSTC.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableSTC.Location = new System.Drawing.Point(480, 441);
            this.chkDisableSTC.Name = "chkDisableSTC";
            this.chkDisableSTC.Size = new System.Drawing.Size(104, 22);
            this.chkDisableSTC.TabIndex = 5;
            this.chkDisableSTC.Text = "Disable STC";
            this.chkDisableSTC.UseVisualStyleBackColor = true;
            // 
            // chkDisableFareOnAccJob
            // 
            this.chkDisableFareOnAccJob.AutoSize = true;
            this.chkDisableFareOnAccJob.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableFareOnAccJob.Location = new System.Drawing.Point(480, 475);
            this.chkDisableFareOnAccJob.Name = "chkDisableFareOnAccJob";
            this.chkDisableFareOnAccJob.Size = new System.Drawing.Size(183, 22);
            this.chkDisableFareOnAccJob.TabIndex = 4;
            this.chkDisableFareOnAccJob.Text = "Disable Fare on A/C Job";
            this.chkDisableFareOnAccJob.UseVisualStyleBackColor = true;
            // 
            // chkShowSpecReq
            // 
            this.chkShowSpecReq.AutoSize = true;
            this.chkShowSpecReq.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowSpecReq.Location = new System.Drawing.Point(240, 407);
            this.chkShowSpecReq.Name = "chkShowSpecReq";
            this.chkShowSpecReq.Size = new System.Drawing.Size(199, 22);
            this.chkShowSpecReq.TabIndex = 3;
            this.chkShowSpecReq.Text = "Show Special Req on Front";
            this.chkShowSpecReq.UseVisualStyleBackColor = true;
            // 
            // chkDisableAlarm
            // 
            this.chkDisableAlarm.AutoSize = true;
            this.chkDisableAlarm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableAlarm.Location = new System.Drawing.Point(480, 510);
            this.chkDisableAlarm.Name = "chkDisableAlarm";
            this.chkDisableAlarm.Size = new System.Drawing.Size(140, 22);
            this.chkDisableAlarm.TabIndex = 2;
            this.chkDisableAlarm.Text = "Disable Set Alarm";
            this.chkDisableAlarm.UseVisualStyleBackColor = true;
            // 
            // chkDisableNoPickup
            // 
            this.chkDisableNoPickup.AutoSize = true;
            this.chkDisableNoPickup.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableNoPickup.Location = new System.Drawing.Point(480, 543);
            this.chkDisableNoPickup.Name = "chkDisableNoPickup";
            this.chkDisableNoPickup.Size = new System.Drawing.Size(140, 22);
            this.chkDisableNoPickup.TabIndex = 1;
            this.chkDisableNoPickup.Text = "Disable No Pickup";
            this.chkDisableNoPickup.UseVisualStyleBackColor = true;
            // 
            // chkShowJobAsAlert
            // 
            this.chkShowJobAsAlert.AutoSize = true;
            this.chkShowJobAsAlert.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowJobAsAlert.Location = new System.Drawing.Point(240, 567);
            this.chkShowJobAsAlert.Name = "chkShowJobAsAlert";
            this.chkShowJobAsAlert.Size = new System.Drawing.Size(142, 22);
            this.chkShowJobAsAlert.TabIndex = 0;
            this.chkShowJobAsAlert.Text = "Show Job as Alert";
            this.chkShowJobAsAlert.UseVisualStyleBackColor = true;
            this.chkShowJobAsAlert.Visible = false;
            // 
            // grdDriverPDASettings
            // 
            this.grdDriverPDASettings.AutoCellFormatting = false;
            this.grdDriverPDASettings.Controls.Add(this.chkAllDriver);
            this.grdDriverPDASettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDriverPDASettings.EnableCheckInCheckOut = false;
            this.grdDriverPDASettings.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDriverPDASettings.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriverPDASettings.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdDriverPDASettings.Location = new System.Drawing.Point(0, 28);
            this.grdDriverPDASettings.Name = "grdDriverPDASettings";
            this.grdDriverPDASettings.PKFieldColumnName = "";
            this.grdDriverPDASettings.ShowImageOnActionButton = true;
            this.grdDriverPDASettings.Size = new System.Drawing.Size(278, 698);
            this.grdDriverPDASettings.TabIndex = 106;
            this.grdDriverPDASettings.Text = "myGridView1";
            // 
            // chkAllDriver
            // 
            this.chkAllDriver.AutoSize = true;
            this.chkAllDriver.BackColor = System.Drawing.Color.AliceBlue;
            this.chkAllDriver.Checked = true;
            this.chkAllDriver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllDriver.Location = new System.Drawing.Point(6, 5);
            this.chkAllDriver.Name = "chkAllDriver";
            this.chkAllDriver.Size = new System.Drawing.Size(40, 22);
            this.chkAllDriver.TabIndex = 107;
            this.chkAllDriver.Text = "All";
            this.chkAllDriver.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdDriverPDASettings);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(911, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 726);
            this.panel1.TabIndex = 106;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.rdoLoginDriver);
            this.panel2.Controls.Add(this.rdoAllDriver);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 28);
            this.panel2.TabIndex = 107;
            // 
            // rdoLoginDriver
            // 
            this.rdoLoginDriver.AutoSize = true;
            this.rdoLoginDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoLoginDriver.ForeColor = System.Drawing.Color.Green;
            this.rdoLoginDriver.Location = new System.Drawing.Point(142, 2);
            this.rdoLoginDriver.Name = "rdoLoginDriver";
            this.rdoLoginDriver.Size = new System.Drawing.Size(119, 22);
            this.rdoLoginDriver.TabIndex = 1;
            this.rdoLoginDriver.Text = "Login Driver";
            this.rdoLoginDriver.UseVisualStyleBackColor = true;
            // 
            // rdoAllDriver
            // 
            this.rdoAllDriver.AutoSize = true;
            this.rdoAllDriver.Checked = true;
            this.rdoAllDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAllDriver.ForeColor = System.Drawing.Color.Red;
            this.rdoAllDriver.Location = new System.Drawing.Point(19, 2);
            this.rdoAllDriver.Name = "rdoAllDriver";
            this.rdoAllDriver.Size = new System.Drawing.Size(98, 22);
            this.rdoAllDriver.TabIndex = 0;
            this.rdoAllDriver.TabStop = true;
            this.rdoAllDriver.Text = "All Driver";
            this.rdoAllDriver.UseVisualStyleBackColor = true;
            // 
            // chkShowDestAfterPOB
            // 
            this.chkShowDestAfterPOB.AutoSize = true;
            this.chkShowDestAfterPOB.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowDestAfterPOB.Location = new System.Drawing.Point(240, 440);
            this.chkShowDestAfterPOB.Name = "chkShowDestAfterPOB";
            this.chkShowDestAfterPOB.Size = new System.Drawing.Size(205, 22);
            this.chkShowDestAfterPOB.TabIndex = 67;
            this.chkShowDestAfterPOB.Text = "Show Job Details After POB";
            this.chkShowDestAfterPOB.UseVisualStyleBackColor = true;
            this.chkShowDestAfterPOB.Visible = false;
            // 
            // frmDriverPDASettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 764);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlSettings);
            this.FormTitle = "Driver PDA Settings";
            this.Name = "frmDriverPDASettings";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver PDA Settings";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.pnlSettings, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBreakDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverPDASettings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverPDASettings)).EndInit();
            this.grdDriverPDASettings.ResumeLayout(false);
            this.grdDriverPDASettings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        //NC
        private System.Windows.Forms.GroupBox pnlSettings;
        private Telerik.WinControls.UI.RadButton btnUpdateSettings;
        private System.Windows.Forms.CheckBox chkDisableOnBreak;
        private System.Windows.Forms.CheckBox chkShiftOverLogout;
        private System.Windows.Forms.CheckBox chkDisableBase;
        private System.Windows.Forms.CheckBox chkShowFareonExtraCharges;
        private System.Windows.Forms.CheckBox chkEnableLogoutOnReject;
        private System.Windows.Forms.CheckBox chkHidePickupAndDest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numJobTimeout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtBiddingMessage;
        private System.Windows.Forms.Label txtFareMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numBreakDuration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkDisableMeterAccJob;
        private System.Windows.Forms.CheckBox chkEnableMeterWaitingCharges;
        private System.Windows.Forms.CheckBox chkEnableOptionalMeter;
        private System.Windows.Forms.CheckBox chkShowSoundOnZoneChange;
        private System.Windows.Forms.CheckBox chkDisableChangeJobPlot;
        private System.Windows.Forms.CheckBox chkDisableRank;
        private System.Windows.Forms.CheckBox chkDisablePanic;
        private System.Windows.Forms.CheckBox chkIgnoreArriveAction;
        private System.Windows.Forms.CheckBox chkMessageStay;
        private System.Windows.Forms.CheckBox ShowPlotOnJobOffer;
        private System.Windows.Forms.CheckBox chkShowAlertOnJobLater;
        private System.Windows.Forms.CheckBox chkShowCustomerMobileNo;
        private System.Windows.Forms.CheckBox chkShowNavigation;
        private System.Windows.Forms.CheckBox chkShowCompletedJobs;
        private System.Windows.Forms.CheckBox chkShowPlots;
        private System.Windows.Forms.CheckBox chkEnableAutoRotate;
        private System.Windows.Forms.CheckBox chkEnableCompanyCars;
        private System.Windows.Forms.CheckBox chkEnableJ15Jobs;
        private System.Windows.Forms.CheckBox chkEnableCallCustomer;
        private System.Windows.Forms.CheckBox chkEnableJobExtraCharges;
        private System.Windows.Forms.CheckBox chkEnableRecoverJob;
        private System.Windows.Forms.CheckBox chkEnableLogoutAuthorization;
        private System.Windows.Forms.CheckBox chkEnableFlagDown;
        private System.Windows.Forms.CheckBox chkEnableFareMeter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkEnableBidding;
        private System.Windows.Forms.ComboBox ddlNavigation;
        private System.Windows.Forms.Label txtPDAVer;
        private Telerik.WinControls.UI.RadLabel lblRangeWiseCommission;
        //private Telerik.WinControls.UI.RadGridView grdRangeWiseComm;
        private System.Windows.Forms.CheckBox chkDisableChangeDest;
        private System.Windows.Forms.CheckBox chkDisableRejectJob;
        private Telerik.WinControls.UI.RadPageViewPage Pg_notes;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadLabel radLabel35;
        private Telerik.WinControls.RootRadElement object_8d9a8a89_3408_492c_97b0_6d603c29a72e;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadTextBox txtNotes;
        private Telerik.WinControls.UI.RadButton btnAdd;
        private Telerik.WinControls.UI.RadButton btnNew;
        //private Telerik.WinControls.UI.RadGridView grdLister;
        private System.Windows.Forms.CheckBox chkShowJobAsAlert;
        private System.Windows.Forms.CheckBox chkShowSpecReq;
        private System.Windows.Forms.CheckBox chkDisableAlarm;
        private System.Windows.Forms.CheckBox chkDisableNoPickup;
        private System.Windows.Forms.CheckBox chkDisableFareOnAccJob;
        private System.Windows.Forms.CheckBox chkDisableSTC;
        private Telerik.WinControls.UI.RadSpinEditor numInitialBalance;
        private Telerik.WinControls.UI.RadLabel radLabel42;
        //private Telerik.WinControls.UI.RadPageViewPage radPageViewPage5;
        //private Telerik.WinControls.UI.RadGridView grdDriverComplaints;
        private Telerik.WinControls.UI.RadSpinEditor numRentLimit;
        private Telerik.WinControls.UI.RadLabel radLabel43;
        private Telerik.WinControls.UI.RadLabel radLabel44;
        private Telerik.WinControls.UI.RadTextBox txtVehicleLogBookNo;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnView;
        private Telerik.WinControls.UI.RadButton btnBrowse;
        private Telerik.WinControls.UI.RadLabel radLabel45;
        private Telerik.WinControls.UI.RadTextBox txtLogBookDocPath;
        private Telerik.WinControls.UI.RadCheckBox chkBidding;
        private Telerik.WinControls.UI.RadSpinEditor numPDARent;
        private Telerik.WinControls.UI.RadLabel lblpdarent;

        private System.Windows.Forms.CheckBox chkEnablePriceBidding;
        private System.Windows.Forms.CheckBox chkEnableManualFares;
        private System.Windows.Forms.CheckBox chkEnableOptionalManualFares;
        private UI.MyGridView grdDriverPDASettings;
        private System.Windows.Forms.CheckBox chkAllDriver;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoLoginDriver;
        private System.Windows.Forms.RadioButton rdoAllDriver;
        private System.Windows.Forms.Label lblcounter;
        private System.Windows.Forms.CheckBox chkVoiceOnClearMeter;
        private System.Windows.Forms.CheckBox chkDisableJobAuth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlHidePickupAndDestinationType;
        //
        #endregion

        private System.Windows.Forms.CheckBox chkShowDestAfterPOB;
    }
}