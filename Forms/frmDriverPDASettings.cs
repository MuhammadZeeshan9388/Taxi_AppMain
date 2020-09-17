using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using System.Threading;
using System.Collections;

namespace Taxi_AppMain
{
    public partial class frmDriverPDASettings : UI.SetupBase
    {
        BackgroundWorker worker = null;
       
        List<Fleet_Driver> objDriver = new List<Fleet_Driver>();
        public frmDriverPDASettings()
        {
            InitializeComponent();
            FormatDriverPDASettingsGrid();
            this.Load += new EventHandler(frmDriverPDASettings_Load);
            this.chkAllDriver.CheckedChanged += new EventHandler(chkAllDriver_CheckedChanged);
            this.rdoAllDriver.CheckedChanged += new EventHandler(rdoAllDriver_CheckedChanged);
            this.FormClosing += new FormClosingEventHandler(frmDriverPDASettings_FormClosing);
        }

        void frmDriverPDASettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (worker != null)
                {
                    if (worker.IsBusy)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        worker.CancelAsync();

                        worker.Dispose();
                        worker = null;

                        this.Dispose();


                    }
                }
            }
            catch
            {


            }
        }
        class CurrentRow
        {
            public int index;
         
        }


        void rdoAllDriver_CheckedChanged(object sender, EventArgs e)
        {
            chkAllDriver.Checked = true;
            if (rdoAllDriver.Checked)
            {
                    grdDriverPDASettings.Rows.Clear();
                    grdDriverPDASettings.BeginUpdate();
                    grdDriverPDASettings.RowCount = objDriver.Count();
                    for (int i = 0; i < objDriver.Count; i++)
                    {
                        grdDriverPDASettings.Rows[i].Cells[COLS.DriverId].Value = objDriver[i].Id;
                        grdDriverPDASettings.Rows[i].Cells[COLS.DriverNo].Value = objDriver[i].DriverNo;
                        grdDriverPDASettings.Rows[i].Cells[COLS.PDAVersion].Value = objDriver[i].PDARent;
                        grdDriverPDASettings.Rows[i].Cells[COLS.Check].Value = true;
                    }

                    grdDriverPDASettings.EndUpdate();
            }
            else
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = (from a in db.GetTable<Fleet_DriverQueueList>()
                                 where a.Status != null && a.Status == true && a.Fleet_Driver.HasPDA == true
                                 select new
                                 {
                                     DriverId = a.Fleet_Driver.Id,
                                     DriverNo = a.Fleet_Driver.DriverNo,
                                     CurrentPdaVersion = a.Fleet_Driver.Fleet_Driver_PDASettings.Count > 0 ? a.Fleet_Driver.Fleet_Driver_PDASettings.FirstOrDefault().CurrentPdaVersion : null
                                 }).ToList().Where(c=>c.CurrentPdaVersion!=null).OrderBy(item => item.DriverNo, new NaturalSortComparer<string>()).ToList();

                    grdDriverPDASettings.Rows.Clear();
                    grdDriverPDASettings.BeginUpdate();
                    grdDriverPDASettings.RowCount = list.Count();
                    for (int i = 0; i < list.Count; i++)
                    {
                        grdDriverPDASettings.Rows[i].Cells[COLS.DriverId].Value = list[i].DriverId;
                        grdDriverPDASettings.Rows[i].Cells[COLS.DriverNo].Value = list[i].DriverNo;
                        grdDriverPDASettings.Rows[i].Cells[COLS.PDAVersion].Value = list[i].CurrentPdaVersion;
                        grdDriverPDASettings.Rows[i].Cells[COLS.Check].Value = true;
                    }

                    grdDriverPDASettings.EndUpdate();

                }
            }
        }

        void chkAllDriver_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAllDriver.Checked == true)
                {
                    for (int i = 0; i < grdDriverPDASettings.Rows.Count; i++)
                    {
                        grdDriverPDASettings.Rows[i].Cells[COLS.Check].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; i < grdDriverPDASettings.Rows.Count; i++)
                    {
                        grdDriverPDASettings.Rows[i].Cells[COLS.Check].Value = false;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        public struct COLS
        {
            public static string DriverId = "DriverId";
            public static string DriverNo = "DriverNo";
            public static string PDAVersion = "PDAVersion";
            public static string Check = "Check";
        }
        private void FormatDriverPDASettingsGrid()
        {
            grdDriverPDASettings.ShowRowHeaderColumn = false;
            grdDriverPDASettings.AllowAddNewRow = false;
            grdDriverPDASettings.ShowGroupPanel = false;
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Check;
            cbcol.Width = 50;
            grdDriverPDASettings.Columns.Add(cbcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.DriverId;
            col.IsVisible = false;
            grdDriverPDASettings.Columns.Add(col);


            

            col = new GridViewTextBoxColumn();
            col.Name = COLS.DriverNo;
            col.HeaderText = "Driver No";
            col.Width = 110;
            col.ReadOnly = true;
            grdDriverPDASettings.Columns.Add(col);
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.PDAVersion;
            dcol.HeaderText = "Version";
            dcol.Width = 85;
            dcol.ReadOnly = false;
            grdDriverPDASettings.Columns.Add(dcol);

            UI.GridFunctions.SetFilter(grdDriverPDASettings);
            grdDriverPDASettings.AllowEditRow = true;
            grdDriverPDASettings.ReadOnly = false;
            grdDriverPDASettings.Columns[COLS.Check].ReadOnly = false;

        }



        void frmDriverPDASettings_Load(object sender, EventArgs e)
        {
            PDASettingsDriverList();
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        int total = 0;
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentRow cr = e.UserState as CurrentRow;
            if (cr != null)
            { 
                lblcounter.Text = "Updating Driver PDA Settings...." + ( cr.index) + " out of " + total + "";  
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblcounter.Visible = false;
            btnUpdateSettings.Enabled = true;
            chkAllDriver.Enabled = true;
            grdDriverPDASettings.Enabled = true;
            rdoAllDriver.Enabled = true;
            rdoLoginDriver.Enabled = true;
            RadDesktopAlert alert = new RadDesktopAlert();
            alert.CaptionText = "PDA";
            alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>Settings updated Successfully</span></b></html>";
            alert.Show();
        }

        public class ClsUpdateSettings
        {

            public List<ClsUpdate> list;
            public Gen_SysPolicy_PDASetting objPdaSettings = null;
        }

        public class ClsUpdate
        {

            public int? DriverId;
            public decimal? PDAVersion;
           

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
                try
                {
                    int cnter = 0;
                    ClsUpdateSettings objSettings = (ClsUpdateSettings)e.Argument;
                    var list2 = objSettings.list.ToList();
                    var objPda = objSettings.objPdaSettings;

                    decimal drvWaitingCharges = 0.00m;
                    decimal drvAccWaitingCharges = 0.00m;
                    
                    
                    string navigationApp = "4";
                  
                 
                    if (objPda.NavigationApp == 1)
                        navigationApp = "1";
                    else if (objPda.NavigationApp == 2)
                        navigationApp = "2";
                    else if (objPda.NavigationApp == 3)
                        navigationApp = "3";

                    else if (objPda.NavigationApp == 4)
                        navigationApp = "5";


                    List<int?> listofLoginDrivers = new List<int?>();
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                       listofLoginDrivers= db.Fleet_DriverQueueLists.Where(c => c.Status == true).Select(c => c.DriverId).ToList<int?>();

                    }


                    DriverBO objMaster = new DriverBO();
                 
                    for (int i = 0; i < list2.Count; i++)
                    {
                        cnter++;

                        objMaster.GetByPrimaryKey(list2[i].DriverId.ToInt());                         
                        objMaster.Edit(); 

                     
                       

                        string manualFares = (objPda.EnableManualFares.ToBool()  ? "1" : "0");                      


                        if (objPda.EnableOptionalManualFares.ToBool()  && objPda.EnableManualFares.ToBool())
                            manualFares = "2";

                        if (objPda.EnableJobExtraCharges.ToBool())// chkEnableJobExtraCharges.Checked)
                        {
                            var obj = General.GetObject<Fleet_VehicleType>(c => c.DriverWaitingChargesPerHour != null && c.DriverWaitingChargesPerHour > 0);


                            if (obj != null)
                            {

                                drvWaitingCharges = obj.DriverWaitingChargesPerHour.ToDecimal();
                                drvAccWaitingCharges = obj.AccountWaitingChargesPerHour.ToDecimal();

                            }
                        }

                        decimal version = list2[i].PDAVersion.ToDecimal();//0.00m;

                        StringBuilder contents = new StringBuilder();

                        contents.Append("update settings<<<");


                        //if (version >= 14)
                        //{
                            // update settings in json Format

                            DriverPDASettings pda = new DriverPDASettings();
                            pda.Ip = AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim();
                            pda.DrvId = objMaster.Current.Id.ToStr();
                           
                            pda.DrvNo = objMaster.Current.DriverNo.ToStr();
                            pda.DrvName = objMaster.Current.DriverName.ToStr().Trim();
                            pda.VehType = objMaster.Current.Fleet_VehicleType.VehicleType.ToStr().ToUpper();
                            pda.GPSInterval = "4";

                            pda.EnableJobExtraCharges =  ((objPda.EnableJobExtraCharges.ToBool()  ? "1" : "0"));  // Extra Charges
                            pda.ShowCompletedJobs = ((objPda.ShowCompletedJob.ToBool() ? "1" : "0")); // Show Completed Jobs

                            pda.DisableJobAuth = ((objPda.DisableRejectJobAuth.ToBool() ? "1" : "0")); // Show Completed Jobs

                            pda.EnableBidding = (objPda.EnableBidding.ToBool() ? "1" : "0"); // Enable Bidding

                            pda.ShowPlots = (objPda.ShowPlots.ToBool() ? "1" : "0"); // Show Plots  -- index 10

                            pda.ShowNavigation = ((objPda.ShowNavigation.ToBool() ? "1" : "0")); // Show Plots -- index 11

                            pda.JobTimeout = ((objPda.JobTimeOutInterval.ToStr() )); // Show Plots -- index 12
                            pda.ZoneInterval = (("40")); // Zone Update Interval -- index 13

                            pda.SoundOnZoneChange = ((objPda.NotifyOnZoneChange.ToBool() ? "1" : "0")); 
                          
                            pda.MessageStayOnScreen = ((objPda.MessageStayOnScreen.ToBool() ? "1" : "0")); // Message Stay -- index 15


                            pda.EnableCompanyCars = ((chkEnableCompanyCars.Checked ? "1" : "0")); // Show Plots -- index 16
                            
                            pda.EnableFareMeter = ((objPda.EnableFareMeter.ToBool() ? "1" : "0")); // Show Plots -- index 18
                            pda.ShowCustomerNo = ((objPda.ShowCustomerMobileNo.ToBool() ? "1" : "0")); // Show Plots -- index 19
                            pda.HidePickupAndDest = ((objPda.HidePickAndDestination.ToBool() ? "1" : "0")); // Show Plots -- index 20

                            if (pda.HidePickupAndDest == "1")
                            {
                                if (objPda.OldPdaVersion.ToDecimal() == 1)
                                    pda.HidePickupAndDest = "2";

                                else if (objPda.OldPdaVersion.ToDecimal() == 2)
                                    pda.HidePickupAndDest = "3";

                            }




                            pda.EnableLogoutOnRejectJob = ((objPda.LogoutOnRejectJob.ToBool() ? "1" : "0")); // Show Plots -- index 21
                            

                            pda.FontSize = "20"; // index no 23
                            pda.NavigationType = (objPda.NavigationApp.ToStr());// navigationApp); // DeviceId -- index 24


                            pda.EnableFlagDown = ((objPda.EnableFlagDown.ToBool() ? "1" : "0")); // -- index 25
                            pda.MessageStayOnScreen = ((objPda.MessageStayOnScreen.ToBool()  ? "1" : "0")); // -- index 26

                            pda.DisablePanic = ((objPda.DisablePanicButton.ToBool() ? "1" : "0")); // index 27
                            ///chkDisablePanic.Checked 
                            pda.DisableRank = ((objPda.DisableDriverRank.ToBool() ? "1" : "0")); // index 28
                            // chkDisableRank.Checked


                            pda.MeterVoice = ((objPda.EnableFareMeterVoice.ToBool()? "1" : "0"));
                            pda.DisableChangeJobPlot = ((objPda.DisableChangeJobPlots.ToBool()? "1" : "0"));// index 30

                            pda.EnableJ15Jobs = ((objPda.EnableJ15J30Jobs.ToBool() ? "1" : "0")); // index 31
                            //chkEnableJ15Jobs.Checked 
                            pda.EnableLogoutAuth = ((objPda.EnableLogoutAuthorization.ToBool() ? "1" : "0")); // index 32
                            pda.EnableIgnoreArrive = ((objPda.IgnoreArriveAction.ToBool() ? "1" : "0")); // index 33


                            pda.BiddingType = "Nearest Driver"; // index 34
                            pda.FareMeterType = "peak"; // index 35
                            //pda.BiddingType = "gasgiashgiopa hagsioh gasioh gaioashgioas hgioah siogah siogah siogh agasgasgasasgNearest Driver"; // index 34
                            //pda.FareMeterType = "diigh dfgh dfiohg dfiohg dfiohg dfioh gdfioh gdiofh giodf hgiodfh gdfioh giodfh gdfioh gdfioh giodh gdpeak"; // index 35


                            pda.EnableOptMeter = ((objPda.EnableOptionalManualFares.ToBool()  ? "1" : "0")); // index 36
                            ///chkEnableOptionalMeter.Checked
                            pda.DisableMeterForAccJob = ((objPda.DisableFareMeterOnAccJob.ToBool()? "1" : "0"));// index 37

                            pda.Courier = "0"; // index 38

                            pda.ShowFaresOnExtraCharges = ((objPda.ShowFaresOnExtraCharges.ToBool() ? "1" : "0")); // index 39
                            pda.EnableCallCustomer = ((objPda.EnableCallCustomer.ToBool() ? "1" : "0")); // index 40


                            pda.EnableRecoverJob = ((objPda.EnableRecoverJob.ToBool() ? "1" : "0"));// index 41

                            pda.EnableMeterWaitingCharges = ((objPda.EnableFareMeterWaitingCharges.ToBool() ? "1" : "0")); // index 42
                           
                            pda.LogoutOnOverShift = ((objPda.LogoutOnOverShift.ToBool()? "1" : "0")); // // Shift Logout                 
                            pda.DisableBase = ((objPda.DisableBase.ToBool()? "1" : "0")); // //Disable Base // DeviceId -- index 44               
                            pda.DisableBreak = ((objPda.DisableOnBreak.ToBool() ? "1" : "0")); // //Disable OnBreak -- index 45
                            pda.DisableRejectJob = ((objPda.DisableRejectJob.ToBool() ? "1" : "0")); // //Disable Reject Job


                            pda.DisableChangeDest = ((objPda.DisableChangeDestination.ToBool() ? "1" : "0"));


                            pda.ShowJobasAlert = ((objPda.ShowJobAsAlert.ToBool() ? "1" : "0"));
                            pda.DisableNoPickup = ((objPda.DisableNoPickup.ToBool() ? "1" : "0"));
                            pda.DisableAlarm = ((objPda.DisableSetAlarm.ToBool() ? "1" : "0"));
                            // chkDisableAlarm.Checked
                            pda.ShowSpecialReqOnFront = ((objPda.ShowSpecReqOnFront.ToBool() ? "1" : "0"));

                            pda.DisableFareOnAccJob = ((objPda.DisableFareOnAccJob.ToBool() ? "1" : "0"));
                            pda.DisableSTC = ((objPda.DisableSTC.ToBool() ? "1" : "0"));


                            // version 10.0
                            pda.ShowAlertOnJobLate = ((chkShowAlertOnJobLater.Checked ? "1" : "0"));
                            pda.EnableAutoRotate = ((objPda.EnableAutoRotateScreen.ToBool() ? "1" : "0"));
                            pda.ShowPlotOnOffer = ((objPda.ShowPlotOnJobOffer.ToBool()  ? "1" : "0"));
                            //ShowPlotOnJobOffer.Checked

                            pda.OnBreakDur = ((objPda.BreakTime.ToStr()));// numBreakDuration.Value.ToStr()));

                            pda.ManualFares = (manualFares);
                            pda.EnablePriceBid = ((objPda.EnablePriceBidding.ToBool() ? "1" : "0"));

                            pda.DrvWaitingMins = ("0");
                            pda.AccWaitingMins = ("0");

                            // need to comment
                            //    pda.DisableJobAuth = "1";

                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(pda);



                            contents.Append(json);

                     //   }
                        //else
                        //{

                        //        contents.Append(AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim() + ",");
                        //        contents.Append(objMaster.Current.Id + ",");
                        //        contents.Append(objMaster.Current.DriverNo + ",");
                        //        contents.Append(objMaster.Current.DriverName + ",");
                        //        contents.Append(objMaster.Current.Fleet_VehicleType.VehicleType.ToStr().ToUpper() + ",");

                        //        contents.Append(" ,");

                        //        contents.Append("4,");  // GPS Interval
                        //        contents.Append((objPda.EnableJobExtraCharges.ToBool() ? "1" : "0") + ",");  // Extra Charges
                        //        contents.Append((objPda.ShowCompletedJob.ToBool() ? "1" : "0") + ","); // Show Completed Jobs

                        //        contents.Append((objPda.EnableBidding.ToBool() ? "1" : "0") + ","); // Enable Bidding


                        //        contents.Append((objPda.ShowPlots.ToBool() ? "1" : "0") + ","); // Show Plots  -- index 10

                        //        contents.Append((objPda.ShowNavigation.ToBool()? "1" : "0") + ","); // Show Plots -- index 11


                        //        contents.Append((objPda.JobTimeOutInterval.ToStr() ) + ","); // Show Plots -- index 12
                        //        contents.Append(("40") + ","); // Zone Update Interval -- index 13
                        //        contents.Append((objPda.NotifyOnZoneChange.ToBool() ? "1" : "0") + ","); // Sound On Zone Change -- index 14
                        //        contents.Append((objPda.MessageStayOnScreen.ToBool() ? "1" : "0") + ","); // Message Stay -- index 15


                        //        contents.Append((objPda.HasCompanyCars.ToBool() ? "1" : "0") + ","); // Show Plots -- index 16
                        //        contents.Append((" ") + ","); // Show Plots -- index 17
                        //        contents.Append((objPda.EnableFareMeter.ToBool()  ? "1" : "0") + ","); // Show Plots -- index 18
                        //        contents.Append((objPda.ShowCustomerMobileNo.ToBool()? "1" : "0") + ","); // Show Plots -- index 19
                        //        contents.Append((objPda.HidePickAndDestination.ToBool() ? "1" : "0") + ","); // Show Plots -- index 20
                        //        contents.Append((objPda.LogoutOnRejectJob.ToBool() ? "1" : "0") + ","); // Show Plots -- index 21
                        //        contents.Append((" ") + ","); // DeviceId -- index 22
                        //        contents.Append(("20") + ","); // DeviceId -- index 23



                        //        contents.Append(navigationApp + ","); // DeviceId -- index 24


                        //        contents.Append((objPda.EnableFlagDown.ToBool() ? "1" : "0") + ","); // -- index 25
                        //        contents.Append((objPda.MessageStayOnScreen.ToBool() ? "1" : "0") + ","); // -- index 26

                        //        contents.Append((objPda.DisablePanicButton.ToBool()  ? "1" : "0") + ","); // index 27
                        //        contents.Append((objPda.DisableDriverRank.ToBool()  ? "1" : "0") + ","); // index 28


                        //        contents.Append("1,"); // // index 29

                        //        contents.Append((objPda.DisableChangeJobPlots.ToBool() ? "1" : "0") + ",");// index 30

                        //        contents.Append((objPda.EnableJ15J30Jobs.ToBool()  ? "1" : "0") + ","); // index 31
                        //        contents.Append((objPda.EnableLogoutAuthorization.ToBool()  ? "1" : "0") + ","); // index 32

                        //        contents.Append((objPda.IgnoreArriveAction.ToBool()  ? "1" : "0") + ","); // index 33


                        //        contents.Append((objPda.BiddingType.ToStr().Trim() == string.Empty ? " " : txtBiddingMessage.Text.Trim()) + ","); // index 34



                        //        contents.Append(  " " + ","); // index 35


                        //        contents.Append((objPda.OptionalFareMeter.ToBool() ? "1" : "0") + ","); // index 36
                        //        contents.Append((objPda.DisableFareMeterOnAccJob.ToBool() ? "1" : "0") + ",");// index 37
                        //        contents.Append("0" + ","); // index 38


                        //        contents.Append((objPda.ShowFaresOnExtraCharges.ToBool() ? "1" : "0") + ","); // index 39
                        //        contents.Append((objPda.EnableCallCustomer.ToBool() ? "1" : "0") + ","); // index 40


                        //        contents.Append((objPda.EnableRecoverJob.ToBool()  ? "1" : "0") + ",");// index 41

                        //        contents.Append((objPda.EnableFareMeterWaitingCharges.ToBool()  ? "1" : "0") + ","); // index 42


                        //        // Version 7.2
                        //        // Shift Logout
                        //        contents.Append((objPda.LogoutOnOverShift.ToBool() ? "1" : "0") + ","); // // Shift Logout




                        //        contents.Append((objPda.DisableBase.ToBool() ? "1" : "0") + ","); // //Disable Base // DeviceId -- index 44

                        //        contents.Append((objPda.DisableOnBreak.ToBool() ? "1" : "0") + ","); // //Disable OnBreak -- index 45
                        //        contents.Append((objPda.DisableRejectJob.ToBool()? "1" : "0") + ","); // //Disable Reject Job


                        //        contents.Append((objPda.DisableChangeDestination.ToBool() ? "1" : "0") + ",");


                        //        contents.Append((objPda.ShowJobAsAlert.ToBool() ? "1" : "0") + ",");
                        //        contents.Append((objPda.DisableNoPickup.ToBool()  ? "1" : "0") + ",");
                        //        contents.Append((objPda.DisableSetAlarm.ToBool()  ? "1" : "0") + ",");
                        //        contents.Append((objPda.ShowSpecReqOnFront.ToBool()  ? "1" : "0") + ",");

                        //        contents.Append((objPda.DisableFareOnAccJob.ToBool() ? "1" : "0") + ",");
                        //        contents.Append((objPda.DisableSTC.ToBool()  ? "1" : "0") + ",");


                        //        // version 10.0
                        //        ///chkShowAlertOnJobLater.Checked 
                        //       contents.Append((objPda.ShowJobAsAlert.ToBool() ? "1" : "0") + ",");
                        //        contents.Append((objPda.EnableAutoRotateScreen.ToBool()  ? "1" : "0") + ",");
                        //        contents.Append((objPda.ShowPlotOnJobOffer.ToBool()  ? "1" : "0") + ",");


                        //        contents.Append((objPda.BreakTime.ToStr()) + ",");


                        //        contents.Append(manualFares + ",");
                        //        contents.Append((objPda.EnablePriceBidding.ToBool()  ? "1" : "0") + ",");


                        //        contents.Append(drvWaitingCharges + ",");
                        //        contents.Append(drvAccWaitingCharges);
                        //    }


                            new Thread(delegate()
                            {
                                General.SendMessageToPDA("request pda=" + 0 + "=" + objMaster.Current.Id + "=" + contents + "=12=" + objMaster.Current.DriverNo);
                            }).Start();

                            //System.Threading.Thread.Sleep(2000);

                            //objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);

                            Fleet_Driver_PDASetting objPDA = null;

                            if (objMaster.Current.Fleet_Driver_PDASettings.Count == 0)
                                objMaster.Current.Fleet_Driver_PDASettings.Add(new Fleet_Driver_PDASetting());



                            objPDA = objMaster.Current.Fleet_Driver_PDASettings[0];


                            objPDA.CurrentPdaVersion = version;
                            objPDA.ShowPlotOnJobOffer = objPda.ShowPlotOnJobOffer;// ShowPlotOnJobOffer.Checked;
                            objPDA.DriverId = objMaster.Current.Id;
                            objPDA.ShowFaresOnExtraCharges =false;// chkShowFareonExtraCharges.Checked;

                            objPDA.EnableJobExtraCharges =false;// chkEnableJobExtraCharges.Checked;
                            objPDA.EnableFareMeterWaitingCharges = objPda.EnableFareMeterWaitingCharges;// chkEnableMeterWaitingCharges.Checked;
                            objPDA.EnableRecoverJob = objPda.EnableRecoverJob;// chkEnableRecoverJob.Checked;
                            objPDA.EnableCallCustomer = objPda.EnableCallCustomer;// chkEnableCallCustomer.Checked;
                            objPDA.EnableBidding = objPda.EnableBidding;// chkEnableBidding.Checked;
                            objPDA.EnableAutoRotateScreen = objPda.EnableAutoRotateScreen;// chkEnableAutoRotate.Checked;
                            objPDA.EnableFareMeter = objPda.EnableFareMeter;// chkEnableFareMeter.Checked;
                            objPDA.EnableFlagDown = objPda.EnableFlagDown;// chkEnableFlagDown.Checked;
                            objPDA.EnableJ15J30Jobs = objPda.EnableJ15J30Jobs;// chkEnableJ15Jobs.Checked;
                            objPDA.EnableLogoutAuthorization = objPda.EnableLogoutAuthorization;// chkEnableLogoutAuthorization.Checked;
                            objPDA.DisableChangeJobPlots = objPda.DisableChangeJobPlots;// chkDisableChangeJobPlot.Checked;
                            objPDA.BreakTime = objPda.BreakTime;// numBreakDuration.Value.ToInt();
                            objPDA.DisableDriverRank = objPda.DisableDriverRank;// chkDisableRank.Checked;
                            objPDA.DisablePanicButton = objPda.DisablePanicButton;// chkDisablePanic.Checked;
                            objPDA.DisableFareMeterOnAccJob = objPda.DisableFareMeterOnAccJob;// chkDisableMeterAccJob.Checked;


                            objPDA.NavigationApp = objPda.NavigationApp;// navigationApp.ToInt();
                            objPDA.MessageStayOnScreen = objPda.MessageStayOnScreen;// chkMessageStay.Checked;
                            objPDA.ShowCompletedJob = objPda.ShowCompletedJob;// chkShowCompletedJobs.Checked;
                            objPDA.ShowCustomerMobileNo = objPda.ShowCustomerMobileNo;// chkShowCustomerMobileNo.Checked;
                            objPDA.ShowNavigation = objPda.ShowNavigation;// chkShowNavigation.Checked;
                            objPDA.ShowPlots = objPda.ShowPlots;// chkShowPlots.Checked;


                            objPDA.FareMeterType = "peak";// txtFareMessage.Text.Trim();
                            objPDA.BiddingType = "Nearest Driver";// txtBiddingMessage.Text.Trim();

                            objPDA.JobTimeOutInterval = objPda.JobTimeOutInterval;// numJobTimeout.Value.ToInt();
                            objPDA.NotifyOnZoneChange = objPda.NotifyOnZoneChange;// chkShowSoundOnZoneChange.Checked;

                            objPDA.HasCompanyCars = objPda.HasCompanyCars;// chkEnableCompanyCars.Checked;
                            objPDA.LogoutOnRejectJob = objPda.LogoutOnRejectJob;// chkEnableLogoutOnReject.Checked;
                            objPDA.IgnoreArriveAction = objPda.IgnoreArriveAction;// chkIgnoreArriveAction.Checked;
                            objPDA.GPSInterval = objPda.GPSInterval;// 3;
                            objPDA.HidePickAndDestination = objPda.HidePickAndDestination;// chkHidePickupAndDest.Checked;


                            if (objPDA.HidePickAndDestination.ToBool())
                            {
                                if (objPda.OldPdaVersion.ToDecimal() == 1)
                                    objPDA.OldPdaVersion = 2;
                                if (objPda.OldPdaVersion.ToDecimal() == 2)
                                    objPDA.OldPdaVersion = 3;


                            }
                            else
                                objPDA.OldPdaVersion = 0;


                            objPDA.LogoutOnOverShift = objPda.LogoutOnOverShift;// chkShiftOverLogout.Checked;
                            objPDA.NotifyOnJobLate = objPda.NotifyOnJobLate;// chkShowAlertOnJobLater.Checked;
                            objPDA.EnableAutoRotateScreen = objPda.EnableAutoRotateScreen;// chkEnableAutoRotate.Checked;
                            objPDA.OptionalFareMeter = objPda.OptionalFareMeter;// chkEnableOptionalMeter.Checked;
                            objPDA.DisableChangeJobPlots = objPda.DisableChangeJobPlots;// chkDisableChangeJobPlot.Checked;
                            objPDA.DisableOnBreak = objPda.DisableOnBreak;// chkDisableOnBreak.Checked;
                            objPDA.DisableBase = objPda.DisableBase;// chkDisableBase.Checked;

                            objPDA.DisableRejectJob = objPda.DisableRejectJob;// chkDisableRejectJob.Checked;
                            objPDA.DisableChangeDestination = objPda.DisableChangeDestination;// chkDisableChangeDest.Checked;



                            objPDA.DisableFareOnAccJob = objPda.DisableFareOnAccJob;// chkDisableFareOnAccJob.Checked;
                            objPDA.DisableSTC = objPda.DisableSTC;// chkDisableSTC.Checked;
                            objPDA.DisableSetAlarm = objPda.DisableSetAlarm;// chkDisableAlarm.Checked;
                            objPDA.DisableNoPickup = objPda.DisableNoPickup;// chkDisableNoPickup.Checked;
                            objPDA.ShowJobAsAlert = objPda.ShowJobAsAlert;// chkShowJobAsAlert.Checked;
                            objPDA.ShowSpecReqOnFront = objPda.ShowSpecReqOnFront;// chkShowSpecReq.Checked;


                            objPDA.EnablePriceBidding = objPda.EnablePriceBidding;// chkEnablePriceBidding.Checked;
                            objPDA.EnableManualFares = objPda.EnableManualFares;// chkEnableManualFares.Checked;
                            objPDA.EnableOptionalManualFares = objPda.EnableOptionalManualFares;// chkEnableOptionalManualFares.Checked;
                            objPDA.EnableFareMeterVoice = objPda.EnableFareMeterVoice.ToBool();

                            if (listofLoginDrivers.Count(c => c == objMaster.Current.Id) == 0)
                            {
                                objMaster.Current.Fleet_Driver_OfflineJobs.Add(new Fleet_Driver_OfflineJob
                                {
                                    DriverId = objMaster.Current.Id
                                    ,
                                    OfflineMessage =  contents + "=12" ,
                                    UpdatedBy = AppVars.LoginObj.UserName.ToStr(),
                                    UpdatedOn = DateTime.Now
                                });


                            }

                            objMaster.Save();
                            objMaster.Clear();
                            CurrentRow cr = new CurrentRow();
                            cr.index = cnter;
                            worker.ReportProgress(cnter, cr);
                        }

                        using(TaxiDataContext db=new TaxiDataContext())
                    {
                        var objSavedSettings = db.Gen_SysPolicy_PDASettings.FirstOrDefault();

                        if (objSavedSettings == null)
                        {
                            objSavedSettings = new Gen_SysPolicy_PDASetting();
                        }

                            
                            
                            
                            objSavedSettings.CurrentPdaVersion = AppVars.objPolicyConfiguration.PDAVersion;
                            objSavedSettings.ShowPlotOnJobOffer = objPda.ShowPlotOnJobOffer;// ShowPlotOnJobOffer.Checked;
                          
                            objSavedSettings.ShowFaresOnExtraCharges = false;// chkShowFareonExtraCharges.Checked;


                            objSavedSettings.DisableRejectJobAuth = objPda.DisableRejectJobAuth.ToBool();

                            objSavedSettings.EnableJobExtraCharges =false;// chkEnableJobExtraCharges.Checked;
                            objSavedSettings.EnableFareMeterWaitingCharges = objPda.EnableFareMeterWaitingCharges;// chkEnableMeterWaitingCharges.Checked;
                            objSavedSettings.EnableRecoverJob = objPda.EnableRecoverJob;// chkEnableRecoverJob.Checked;
                            objSavedSettings.EnableCallCustomer = objPda.EnableCallCustomer;// chkEnableCallCustomer.Checked;
                            objSavedSettings.EnableBidding = objPda.EnableBidding;// chkEnableBidding.Checked;
                            objSavedSettings.EnableAutoRotateScreen = objPda.EnableAutoRotateScreen;// chkEnableAutoRotate.Checked;
                            objSavedSettings.EnableFareMeter = objPda.EnableFareMeter;// chkEnableFareMeter.Checked;
                            objSavedSettings.EnableFlagDown = objPda.EnableFlagDown;// chkEnableFlagDown.Checked;
                            objSavedSettings.EnableJ15J30Jobs = objPda.EnableJ15J30Jobs;// chkEnableJ15Jobs.Checked;
                            objSavedSettings.EnableLogoutAuthorization = objPda.EnableLogoutAuthorization;// chkEnableLogoutAuthorization.Checked;
                            objSavedSettings.DisableChangeJobPlots = objPda.DisableChangeJobPlots;// chkDisableChangeJobPlot.Checked;
                            objSavedSettings.BreakTime = objPda.BreakTime;// numBreakDuration.Value.ToInt();
                            objSavedSettings.DisableDriverRank = objPda.DisableDriverRank;// chkDisableRank.Checked;
                            objSavedSettings.DisablePanicButton = objPda.DisablePanicButton;// chkDisablePanic.Checked;
                            objSavedSettings.DisableFareMeterOnAccJob = objPda.DisableFareMeterOnAccJob;// chkDisableMeterAccJob.Checked;


                            objSavedSettings.NavigationApp = objPda.NavigationApp;// navigationApp.ToInt();
                            objSavedSettings.MessageStayOnScreen = objPda.MessageStayOnScreen;// chkMessageStay.Checked;
                            objSavedSettings.ShowCompletedJob = objPda.ShowCompletedJob;// chkShowCompletedJobs.Checked;
                            objSavedSettings.ShowCustomerMobileNo = objPda.ShowCustomerMobileNo;// chkShowCustomerMobileNo.Checked;
                            objSavedSettings.ShowNavigation = objPda.ShowNavigation;// chkShowNavigation.Checked;
                            objSavedSettings.ShowPlots = objPda.ShowPlots;// chkShowPlots.Checked;


                            objSavedSettings.FareMeterType = objPda.FareMeterType;// txtFareMessage.Text.Trim();
                            objSavedSettings.BiddingType = objPda.BiddingType;// txtBiddingMessage.Text.Trim();

                            objSavedSettings.JobTimeOutInterval = objPda.JobTimeOutInterval;// numJobTimeout.Value.ToInt();
                            objSavedSettings.NotifyOnZoneChange = objPda.NotifyOnZoneChange;// chkShowSoundOnZoneChange.Checked;

                            objSavedSettings.HasCompanyCars = objPda.HasCompanyCars;// chkEnableCompanyCars.Checked;
                            objSavedSettings.LogoutOnRejectJob = objPda.LogoutOnRejectJob;// chkEnableLogoutOnReject.Checked;
                            objSavedSettings.IgnoreArriveAction = objPda.IgnoreArriveAction;// chkIgnoreArriveAction.Checked;
                            objSavedSettings.GPSInterval = objPda.GPSInterval;// 3;
                            objSavedSettings.HidePickAndDestination = objPda.HidePickAndDestination;// chkHidePickupAndDest.Checked;


                            if (objSavedSettings.HidePickAndDestination.ToBool())
                            {
                                if (objPda.OldPdaVersion.ToDecimal() == 1)
                                    objSavedSettings.OldPdaVersion = 2;
                                if (objPda.OldPdaVersion.ToDecimal() == 2)
                                    objSavedSettings.OldPdaVersion = 3;


                            }
                            else
                                objSavedSettings.OldPdaVersion = 0;



                            objSavedSettings.LogoutOnOverShift = objPda.LogoutOnOverShift;// chkShiftOverLogout.Checked;
                            objSavedSettings.NotifyOnJobLate = objPda.NotifyOnJobLate;// chkShowAlertOnJobLater.Checked;
                            objSavedSettings.EnableAutoRotateScreen = objPda.EnableAutoRotateScreen;// chkEnableAutoRotate.Checked;
                            objSavedSettings.OptionalFareMeter = objPda.OptionalFareMeter;// chkEnableOptionalMeter.Checked;
                            objSavedSettings.DisableChangeJobPlots = objPda.DisableChangeJobPlots;// chkDisableChangeJobPlot.Checked;
                            objSavedSettings.DisableOnBreak = objPda.DisableOnBreak;// chkDisableOnBreak.Checked;
                            objSavedSettings.DisableBase = objPda.DisableBase;// chkDisableBase.Checked;

                            objSavedSettings.DisableRejectJob = objPda.DisableRejectJob;// chkDisableRejectJob.Checked;
                            objSavedSettings.DisableChangeDestination = objPda.DisableChangeDestination;// chkDisableChangeDest.Checked;



                            objSavedSettings.DisableFareOnAccJob = objPda.DisableFareOnAccJob;// chkDisableFareOnAccJob.Checked;
                            objSavedSettings.DisableSTC = objPda.DisableSTC;// chkDisableSTC.Checked;
                            objSavedSettings.DisableSetAlarm = objPda.DisableSetAlarm;// chkDisableAlarm.Checked;
                            objSavedSettings.DisableNoPickup = objPda.DisableNoPickup;// chkDisableNoPickup.Checked;
                            objSavedSettings.ShowJobAsAlert = objPda.ShowJobAsAlert;// chkShowJobAsAlert.Checked;
                            objSavedSettings.ShowSpecReqOnFront = objPda.ShowSpecReqOnFront;// chkShowSpecReq.Checked;
                            objSavedSettings.EnableFareMeterVoice = objPda.EnableFareMeterVoice.ToBool();

                            objSavedSettings.EnablePriceBidding = objPda.EnablePriceBidding;// chkEnablePriceBidding.Checked;
                            objSavedSettings.EnableManualFares = objPda.EnableManualFares;// chkEnableManualFares.Checked;
                            objSavedSettings.EnableOptionalManualFares = objPda.EnableOptionalManualFares;// chkEnableOptionalManualFares.Checked;
                            objSavedSettings.SysPolicyId = objPda.SysPolicyId;
               


                        if (objSavedSettings.Id == 0)
                        {
                            db.Gen_SysPolicy_PDASettings.InsertOnSubmit(objSavedSettings);

                        }

                        db.SubmitChanges();

                    }

                }
                catch (Exception ex)
                {
                    //ENUtils.ShowMessage(ex.Message);
                    //btnUpdateSettings.Enabled = true;
                }

        }
       


        private void PDASettingsDriverList()
        {
            try
            {
                var list = (from a in General.GetQueryable<Fleet_Driver>(c => (c.IsActive == true) && (c.HasPDA == true))
                            select new
                            {
                                Id=a.Id,
                                DriverNo=a.DriverNo,
                                CurrentPdaVersion = a.Fleet_Driver_PDASettings.Count>0 ? a.Fleet_Driver_PDASettings.FirstOrDefault().CurrentPdaVersion : 0
                            }).ToList();
                var list2 = list.OrderBy(item => item.DriverNo, new NaturalSortComparer<string>()).ToList();
                
                foreach (var item in list2)
                {
                    objDriver.Add(new Fleet_Driver {Id=item.Id,DriverNo=item.DriverNo,PDARent=item.CurrentPdaVersion });
                }
                
                grdDriverPDASettings.BeginUpdate();
                grdDriverPDASettings.RowCount = list.Count();
                for (int i = 0; i < list2.Count; i++)
                {
                    grdDriverPDASettings.Rows[i].Cells[COLS.DriverId].Value = list2[i].Id;
                    grdDriverPDASettings.Rows[i].Cells[COLS.DriverNo].Value = list2[i].DriverNo;
                    grdDriverPDASettings.Rows[i].Cells[COLS.PDAVersion].Value = list2[i].CurrentPdaVersion;
                    grdDriverPDASettings.Rows[i].Cells[COLS.Check].Value = true;
                }

                grdDriverPDASettings.EndUpdate();

                var query = General.GetObject<Gen_SysPolicy_PDASetting>(c => c.Id > 0);
                if (query != null)
                {

                    chkDisableJobAuth.Checked = query.DisableRejectJobAuth.ToBool();
                    ShowPlotOnJobOffer.Checked = query.ShowPlotOnJobOffer.ToBool();
                    chkShowFareonExtraCharges.Checked = false;

                    chkEnableJobExtraCharges.Checked = false; //
                    chkEnableMeterWaitingCharges.Checked = query.EnableFareMeterVoice.ToBool();
                    chkEnableMeterWaitingCharges.Checked = query.EnableFareMeterWaitingCharges.ToBool();
                    chkEnableRecoverJob.Checked = query.EnableRecoverJob.ToBool();
                    chkEnableCallCustomer.Checked = query.EnableCallCustomer.ToBool();
                    chkEnableBidding.Checked = query.EnableBidding.ToBool();//
                    chkEnableAutoRotate.Checked = query.EnableAutoRotateScreen.ToBool();
                    chkEnableFareMeter.Checked = query.EnableFareMeter.ToBool();//
                    chkEnableFlagDown.Checked = query.EnableFlagDown.ToBool();
                    chkEnableJ15Jobs.Checked = query.EnableJ15J30Jobs.ToBool();
                    chkEnableLogoutAuthorization.Checked = query.EnableLogoutAuthorization.ToBool();
                    chkDisableChangeJobPlot.Checked = query.DisableChangeJobPlots.ToBool();
                    numBreakDuration.Value = query.BreakTime.ToInt();
                    chkDisableRank.Checked = query.DisableDriverRank.ToBool();
                    chkDisablePanic.Checked = query.DisablePanicButton.ToBool();
                    chkDisableMeterAccJob.Checked = query.DisableFareMeterOnAccJob.ToBool(); //19


                    //ddlNavigation.SelectedItem = query.NavigationApp;
                    chkMessageStay.Checked = query.MessageStayOnScreen.ToBool();//
                    chkShowCompletedJobs.Checked = query.ShowCompletedJob.ToBool();//
                    chkShowCustomerMobileNo.Checked = query.ShowCustomerMobileNo.ToBool();
                    chkShowNavigation.Checked = query.ShowNavigation.ToBool();//
                    chkShowPlots.Checked = query.ShowPlots.ToBool();//


                    txtFareMessage.Text = query.FareMeterType;
                    txtBiddingMessage.Text = query.BiddingType;

                    numJobTimeout.Value = query.JobTimeOutInterval.ToInt();//
                    chkShowSoundOnZoneChange.Checked = query.NotifyOnZoneChange.ToBool();//

                    chkEnableCompanyCars.Checked = query.HasCompanyCars.ToBool();//
                    chkEnableLogoutOnReject.Checked = query.LogoutOnRejectJob.ToBool();
                    chkIgnoreArriveAction.Checked = query.IgnoreArriveAction.ToBool(); //31

                    chkHidePickupAndDest.CheckedChanged += new EventHandler(chkHidePickupAndDest_CheckedChanged); 
                    chkHidePickupAndDest.Checked = query.HidePickAndDestination.ToBool();

                    if (chkHidePickupAndDest.Checked)
                    {
                        ddlHidePickupAndDestinationType.Visible = true;

                    }


                    if (query.OldPdaVersion.ToInt() == 2)
                        ddlHidePickupAndDestinationType.SelectedIndex = 1;
                    else if (query.OldPdaVersion.ToInt() == 3)
                        ddlHidePickupAndDestinationType.SelectedIndex = 2;
                    else
                        ddlHidePickupAndDestinationType.SelectedIndex = 0;

                    chkShiftOverLogout.Checked = query.LogoutOnOverShift.ToBool();
                    chkShowAlertOnJobLater.Checked = query.NotifyOnJobLate.ToBool();
                    chkEnableAutoRotate.Checked = query.EnableAutoRotateScreen.ToBool();
                    chkEnableOptionalMeter.Checked = query.OptionalFareMeter.ToBool();
                    chkDisableChangeJobPlot.Checked = query.DisableChangeJobPlots.ToBool();
                    chkDisableOnBreak.Checked = query.DisableOnBreak.ToBool();
                    chkDisableBase.Checked = query.DisableBase.ToBool();

                    chkDisableRejectJob.Checked = query.DisableRejectJob.ToBool();
                    chkDisableChangeDest.Checked = query.DisableChangeDestination.ToBool();



                    chkDisableFareOnAccJob.Checked = query.DisableFareOnAccJob.ToBool();
                    chkDisableSTC.Checked = query.DisableSTC.ToBool();
                    chkDisableAlarm.Checked = query.DisableSetAlarm.ToBool();
                    chkDisableNoPickup.Checked = query.DisableNoPickup.ToBool();
                    chkShowJobAsAlert.Checked = query.ShowJobAsAlert.ToBool();
                    chkShowSpecReq.Checked = query.ShowSpecReqOnFront.ToBool();


                    chkEnablePriceBidding.Checked = query.EnablePriceBidding.ToBool();
                    chkEnableManualFares.Checked = query.EnableManualFares.ToBool();
                    chkEnableOptionalManualFares.Checked = query.EnableOptionalManualFares.ToBool();

                //    chkShowDestAfterPOB.Checked = query.ShowDestinationAfterPOB.ToBool();

                    int navigationType = query.NavigationApp.ToInt();
                    if (navigationType == 1)
                    {
                        ddlNavigation.SelectedIndex = 1;
                    }
                    else if (navigationType == 2)
                    {
                        ddlNavigation.SelectedIndex = 2;
                    }
                    else if (navigationType == 3)
                    {
                        ddlNavigation.SelectedIndex = 3;
                    }
                    else if (navigationType == 5)
                    {
                        ddlNavigation.SelectedIndex = 4;
                    }
                    else
                    {
                        ddlNavigation.SelectedIndex = 0;
                    }

                }
                else
                {
                    chkDisableJobAuth.Checked = false;
                    ShowPlotOnJobOffer.Checked = true;
                    chkShowFareonExtraCharges.Checked = false;

                    chkEnableJobExtraCharges.Checked = false;
                    chkEnableMeterWaitingCharges.Checked = false;
                    chkEnableMeterWaitingCharges.Checked = false;
                    chkEnableRecoverJob.Checked = true;
                    chkEnableCallCustomer.Checked = true;
                    chkEnableBidding.Checked = false;
                    chkEnableAutoRotate.Checked = false;
                    chkEnableFareMeter.Checked = false;
                    chkEnableFlagDown.Checked = true;
                    chkEnableJ15Jobs.Checked = true;
                    chkEnableLogoutAuthorization.Checked = false;
                    chkDisableChangeJobPlot.Checked = false;
                    numBreakDuration.Value = 60;
                    chkDisableRank.Checked = false;
                    chkDisablePanic.Checked = false;
                    chkDisableMeterAccJob.Checked = false;


                    //ddlNavigation.SelectedItem = query.NavigationApp;
                    chkMessageStay.Checked = true;
                    chkShowCompletedJobs.Checked = true;
                    chkShowCustomerMobileNo.Checked = true;
                    chkShowNavigation.Checked = true;
                    chkShowPlots.Checked = true;


                    txtFareMessage.Text = "";
                    txtBiddingMessage.Text = "";

                    numJobTimeout.Value = 60;
                    chkShowSoundOnZoneChange.Checked = true;

                    chkEnableCompanyCars.Checked = false;
                    chkEnableLogoutOnReject.Checked = false;
                    chkIgnoreArriveAction.Checked = false;

                    chkHidePickupAndDest.Checked = false;
                    chkShiftOverLogout.Checked = false;
                    chkShowAlertOnJobLater.Checked = false;
                    chkEnableAutoRotate.Checked = false;
                    chkEnableOptionalMeter.Checked = false;
                    chkDisableChangeJobPlot.Checked = false;
                    chkDisableOnBreak.Checked = false;
                    chkDisableBase.Checked = false;

                    chkDisableRejectJob.Checked = false;
                    chkDisableChangeDest.Checked = false;



                    chkDisableFareOnAccJob.Checked = false;
                    chkDisableSTC.Checked = false;
                    chkDisableAlarm.Checked = false;
                    chkDisableNoPickup.Checked = false;
                    chkShowJobAsAlert.Checked = false;
                    chkShowSpecReq.Checked = false;


                    chkEnablePriceBidding.Checked = false;
                    chkEnableManualFares.Checked = false;
                    chkEnableOptionalManualFares.Checked = false;
                                      
                   
                     ddlNavigation.SelectedIndex = 1;

                    chkShowDestAfterPOB.Checked = false;
                }
               
            }
            catch (Exception ex)
            { 
            ENUtils.ShowMessage(ex.Message);
            }
        }

      
        void chkEnableFareMeter_CheckedChanged(object sender, EventArgs e)
        {
            SetFareMeterState();
        }
        private void SetFareMeterState()
        {

            if (chkEnableFareMeter.Checked)
            {
                chkEnableOptionalMeter.Enabled = true;
                chkEnableMeterWaitingCharges.Enabled = true;
                chkDisableMeterAccJob.Enabled = true;
                chkVoiceOnClearMeter.Enabled = true;


                chkEnableManualFares.Checked = false;
                chkEnableOptionalManualFares.Checked = false;
                chkEnableManualFares.Enabled = false;
                chkEnableOptionalManualFares.Enabled = false;
            }
            else
            {
                chkEnableOptionalMeter.Enabled = false;
                chkEnableMeterWaitingCharges.Enabled = false;
                chkDisableMeterAccJob.Enabled = false;
                chkVoiceOnClearMeter.Enabled = false;

                chkEnableOptionalMeter.Checked = false;
                chkEnableMeterWaitingCharges.Checked = false;
                chkDisableMeterAccJob.Checked = false;



                chkEnableManualFares.Enabled = true;
                chkEnableOptionalManualFares.Enabled = true;


            }

        }

        void chkEnableManualFares_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableManualFares.Checked)
            {
                chkEnableFareMeter.Checked = false;
                chkEnableFareMeter.Enabled = false;


            }
            else
            {

                chkEnableFareMeter.Enabled = true;


            }
        }

        private void btnUpdateSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdDriverPDASettings.Rows.Where(c => c.Cells[COLS.Check].Value.ToBool() == true).Count() == 0)
                {
                    ENUtils.ShowMessage("Please select Driver(s) to update PDA Settings");
                    return;
                }



                if (grdDriverPDASettings.Rows.Where(c => c.Cells[COLS.Check].Value.ToBool() == true).Count() == grdDriverPDASettings.Rows.Count)
                {

                    string val = this.rdoAllDriver.Checked ? "All Driver(s)" : "All Login Driver(s)";

                    if (DialogResult.No == MessageBox.Show("Are you sure you want to Update " + val + " settings ?", "", MessageBoxButtons.YesNo))
                    {

                        return;


                    }


                }




                btnUpdateSettings.Enabled = false;

                var list = (from a in grdDriverPDASettings.Rows.Where(c => c.Cells[COLS.Check].Value.ToBool() == true)
                            select new
                            {
                                DriverId = a.Cells[COLS.DriverId].Value,
                                PDAVersion = a.Cells[COLS.PDAVersion].Value
                            }).ToList();
                total = grdDriverPDASettings.Rows.Where(c => c.Cells[COLS.Check].Value.ToBool() == true).Count();
                List<ClsUpdate> objList = new List<ClsUpdate>();

                foreach (var item in list)
                {
                    objList.Add(new ClsUpdate { DriverId = item.DriverId.ToInt(), PDAVersion = item.PDAVersion.ToDecimal() });
                }
                if (worker != null && worker.IsBusy == false)
                {

                    string navigationApp = "4";

                    if (ddlNavigation.SelectedIndex == 1)
                        navigationApp = "1";
                    else if (ddlNavigation.SelectedIndex == 2)
                        navigationApp = "2";
                    else if (ddlNavigation.SelectedIndex == 3)
                        navigationApp = "3";


                    else if (ddlNavigation.SelectedIndex == 4)
                        navigationApp = "5";

                       Gen_SysPolicy_PDASetting objPDASetting = new Gen_SysPolicy_PDASetting();


                        objPDASetting.ShowPlotOnJobOffer = ShowPlotOnJobOffer.Checked;
                        objPDASetting.SysPolicyId = 1;
                        objPDASetting.ShowFaresOnExtraCharges = false;

                        objPDASetting.EnableJobExtraCharges = false;
                        objPDASetting.EnableFareMeterWaitingCharges = chkEnableMeterWaitingCharges.Checked;
                        objPDASetting.EnableRecoverJob = chkEnableRecoverJob.Checked;
                        objPDASetting.EnableCallCustomer = chkEnableCallCustomer.Checked;
                        objPDASetting.EnableBidding = chkEnableBidding.Checked;
                        objPDASetting.EnableAutoRotateScreen = chkEnableAutoRotate.Checked;
                        objPDASetting.EnableFareMeter = chkEnableFareMeter.Checked;
                        objPDASetting.EnableFareMeterVoice = chkVoiceOnClearMeter.Checked.ToBool();
                        objPDASetting.EnableFlagDown = chkEnableFlagDown.Checked;
                        objPDASetting.EnableJ15J30Jobs = chkEnableJ15Jobs.Checked;
                        objPDASetting.EnableLogoutAuthorization = chkEnableLogoutAuthorization.Checked;
                        objPDASetting.DisableChangeJobPlots = chkDisableChangeJobPlot.Checked;
                        objPDASetting.BreakTime = numBreakDuration.Value.ToInt();
                        objPDASetting.DisableDriverRank = chkDisableRank.Checked;
                        objPDASetting.DisablePanicButton = chkDisablePanic.Checked;
                        objPDASetting.DisableFareMeterOnAccJob = chkDisableMeterAccJob.Checked;


                        objPDASetting.NavigationApp =navigationApp.ToInt();
                        objPDASetting.MessageStayOnScreen = chkMessageStay.Checked;
                        objPDASetting.ShowCompletedJob = chkShowCompletedJobs.Checked;
                        objPDASetting.ShowCustomerMobileNo = chkShowCustomerMobileNo.Checked;
                        objPDASetting.ShowNavigation = chkShowNavigation.Checked;
                        objPDASetting.ShowPlots = chkShowPlots.Checked;


                        objPDASetting.FareMeterType = txtFareMessage.Text.Trim();
                        objPDASetting.BiddingType = txtBiddingMessage.Text.Trim();

                        objPDASetting.JobTimeOutInterval = numJobTimeout.Value.ToInt();
                        objPDASetting.NotifyOnZoneChange = chkShowSoundOnZoneChange.Checked;

                        objPDASetting.HasCompanyCars = chkEnableCompanyCars.Checked;
                        objPDASetting.LogoutOnRejectJob = chkEnableLogoutOnReject.Checked;
                        objPDASetting.IgnoreArriveAction = chkIgnoreArriveAction.Checked;
                        objPDASetting.GPSInterval = 4;
                        objPDASetting.HidePickAndDestination = chkHidePickupAndDest.Checked;
                        objPDASetting.OldPdaVersion = ddlHidePickupAndDestinationType.SelectedIndex;
                        objPDASetting.LogoutOnOverShift = chkShiftOverLogout.Checked;
                        objPDASetting.NotifyOnJobLate = chkShowAlertOnJobLater.Checked;
                        objPDASetting.EnableAutoRotateScreen = chkEnableAutoRotate.Checked;
                        objPDASetting.OptionalFareMeter = chkEnableOptionalMeter.Checked;
                        objPDASetting.DisableChangeJobPlots = chkDisableChangeJobPlot.Checked;
                        objPDASetting.DisableOnBreak = chkDisableOnBreak.Checked;
                        objPDASetting.DisableBase = chkDisableBase.Checked;

                        objPDASetting.DisableRejectJob = chkDisableRejectJob.Checked;
                        objPDASetting.DisableChangeDestination = chkDisableChangeDest.Checked;



                        objPDASetting.DisableFareOnAccJob = chkDisableFareOnAccJob.Checked;
                        objPDASetting.DisableSTC = chkDisableSTC.Checked;
                        objPDASetting.DisableSetAlarm = chkDisableAlarm.Checked;
                        objPDASetting.DisableNoPickup = chkDisableNoPickup.Checked;
                        objPDASetting.ShowJobAsAlert = chkShowJobAsAlert.Checked;
                        objPDASetting.ShowSpecReqOnFront = chkShowSpecReq.Checked;


                        objPDASetting.EnablePriceBidding = chkEnablePriceBidding.Checked;
                        objPDASetting.EnableManualFares = chkEnableManualFares.Checked;
                        objPDASetting.EnableOptionalManualFares = chkEnableOptionalManualFares.Checked;


                        objPDASetting.DisableRejectJobAuth = chkDisableJobAuth.Checked;

                        btnUpdateSettings.Enabled = false;
                        chkAllDriver.Enabled = false;
                        grdDriverPDASettings.Enabled = false;
                        rdoAllDriver.Enabled = false;
                        rdoLoginDriver.Enabled = false;
                        lblcounter.Visible = true;
                        ClsUpdateSettings obj = new ClsUpdateSettings();

                        obj.list = objList;
                        obj.objPdaSettings = objPDASetting;
                        worker.RunWorkerAsync(obj);
                  
                }
            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void chkHidePickupAndDest_CheckedChanged(object sender, EventArgs e)
        {


            if (chkHidePickupAndDest.Checked)
            {

                ddlHidePickupAndDestinationType.Visible = true;
            }
            else
                ddlHidePickupAndDestinationType.Visible = false;
        }



    }
}
