using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using UI;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;


using System.Diagnostics;
using System.Threading;
using System.Data.Linq;


using System.Net;
using System.Xml.Linq;
using System.IO;
using System.Runtime.InteropServices;
using Taxi_AppMain.Forms;
using Telerik.WinControls.UI.Docking;
using System.Xml;

using Telerik.WinControls;


using Taxi_AppMain.Classes;

using System.Collections;


namespace Taxi_AppMain
{
    public partial class frmDriverShortcut : Telerik.WinControls.UI.RadForm
    {


        public frmDriverShortcut()
        {

            InitializeComponent();
          
       
            LoadData();

            ddl_Driver.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        
            this.ddl_Driver.TextChanged+=new EventHandler(ddl_Driver_TextChanged);
          //  this.ddl_Driver.DropDownListElement.TextChanged += new EventHandler(DropDownListElement_TextChanged);
            this.KeyDown += new KeyEventHandler(frmDriverShortcut_KeyDown);
          //  this.ddl_Driver.DropDownListElement.TextChanging += new TextChangingEventHandler(DropDownListElement_TextChanging);
        }

        //void DropDownListElement_TextChanging(object sender, TextChangingEventArgs e)
        //{
        //    //this.ddl_Driver.DropDownListElement.TextBox.cle.Filter = ClearFilterItem;
          
                
        //    //this.ddl_Driver.Filter = FilterItem;

        //    string s = ddl_Driver.Text;
          
        //    this.ddl_Driver.FilterExpression = "DriverName LIKE '%" + s + "%'";
        //}

      


        //private bool FilterItem(RadListDataItem item)
        //{
        //    if (item == null)
        //        return false;

        //    if (item.Text.Contains(ddl_Driver.DropDownListElement.TextBox.TextBoxItem.Text))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private void LoadData()
        {
            try
            {
                ComboFunctions.FillDriverNoCombo(ddl_Driver);

            }
            catch(Exception ex)
            {

            }

        }
       

     

        private  void ddl_Driver_TextChanged(object sender, EventArgs e)
        {
            if (ddl_Driver.SelectedValue != null)
            {
                var obj = ddl_Driver.Items.FirstOrDefault(c => c.Value.ToInt() == ddl_Driver.SelectedValue.ToInt());

                if (obj != null && ddl_Driver.Text.Length > 0)
                {
                    ddl_Driver.TextChanged -= new EventHandler(ddl_Driver_TextChanged);
                    ddl_Driver.Text = obj.Text;
                    ddl_Driver.DropDownListElement.SelectAllText();

                    ddl_Driver.TextChanged += new EventHandler(ddl_Driver_TextChanged);
                }
            }

           

        }

  


        private void frmDriverShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }

                if (ddl_Driver.SelectedValue == null)
                    return;



                int driverId = ddl_Driver.SelectedValue.ToInt();


                 if (e.KeyCode == Keys.A)
                {
                    ClearJob();
                }
                 else if (e.KeyCode == Keys.B)
                 {

                     BreakDriver();
                 }
                 else if (e.KeyCode == Keys.E)
                 {
                     ForceArrived();
                 }

                 else if (e.KeyCode == Keys.F)
                 {

                     LogoutDriver();
                 }


                 else if (e.KeyCode == Keys.I)
                 {
                     DriverInformation();
                 }
                 else if (e.KeyCode == Keys.J)
                 {
                     CompletedJobs();
                 }
                 else if (e.KeyCode == Keys.K)
                 {
                     PDAMessage();
                 }

               
               
                else if (e.KeyCode == Keys.M)
                {
                    ForcePOB();
                }

                else if (e.KeyCode == Keys.N)
                {
                    NoShow();
                }

                 else if (e.KeyCode == Keys.R)
                 {
                     ReCallJob();
                 }
                
                else if (e.KeyCode == Keys.S)
                {

                    LoginDriver();
                }

                 if (e.KeyCode == Keys.T)
                 {
                     TrackDriver();

                 }
              

                else if (e.KeyCode == Keys.U)
                {
                    SinBinDriver();
                }


                 else if (e.KeyCode == Keys.V)
                 {
                     ViewBooking();


                 }     

               

                else if (e.KeyCode == Keys.Z)
                {
                    SMSMessage();
                }
                    


            }
       
            catch (Exception ex)
            {


            }

        }


        private void TrackDriver()
        {
            if (ddl_Driver.SelectedValue == null)
                ENUtils.ShowMessage("Select any Driver");

            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var objLogin = db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Status == true && c.DriverId == driverId);

                    if (objLogin == null)
                    {
                        ENUtils.ShowMessage("Driver is not Login");

                    }
                    else
                    {
                        long jobId = objLogin.CurrentJobId.ToLong();

                        if (File.Exists(Application.StartupPath + "\\Booking\\TreasureBooking.exe"))
                        {
                            ClsDataTransfer polObj = new ClsDataTransfer();

                            foreach (System.Reflection.PropertyInfo item in AppVars.objPolicyConfiguration.GetType().GetProperties())
                            {
                                try
                                {

                                    if (polObj.GetType().GetProperty(item.Name) != null)
                                        polObj.GetType().GetProperty(item.Name).SetValue(polObj, item.GetValue(AppVars.objPolicyConfiguration, null), null);
                                }
                                catch
                                {


                                }
                            }


                            polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                            string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");
                            string s = "XXX";
                            Process pp = new Process();
                            pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                            pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");
                            pp.StartInfo.Arguments += " " + "rptJobRouthPathGoogle" + " " + jobId + " " + "true" + " " + driverId;
                            pp.Start();
                            Thread.Sleep(500);
                         //   pp.WaitForExit();
                        }
                        else
                        {
                            rptJobRouthPathGoogle rpt = new rptJobRouthPathGoogle(jobId > 0 ? db.Bookings.FirstOrDefault(c => c.Id == jobId) : null, true, driverId);
                            rpt.ShowDialog();
                            rpt.Dispose();

                            GC.Collect();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

   
        private void NoShow()
        {

            try
            {

                if (ddl_Driver.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Select any Driver");

                    return;
                }
                else
                {

                    int driverId = ddl_Driver.SelectedValue.ToInt();
                    var list = from a in General.GetQueryable<Fleet_DriverQueueList>(c => c.Id > 0)
                               where a.DriverId == driverId && a.Status == true && a.CurrentJobId != null
                               select new
                               {

                                   Id = a.Id,

                               };

                    string val = list.ToStr();

                    if (list.Count() != 0)
                    {


                        long jobId = 0;

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            jobId = db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Id == list.FirstOrDefault().Id).DefaultIfEmpty().CurrentJobId.ToLong();

                        }




                        if (jobId != 0)
                        {

                            string[] driverNo = (ddl_Driver.SelectedItem.Text).Split('-');

                            new Thread(delegate()
                            {
                                int loopCnt = 1;
                                while (loopCnt < 3)
                                {

                                    bool success = General.ReCallBookingWithStatus(jobId.ToLong(), driverId.ToInt(), Enums.BOOKINGSTATUS.NOPICKUP, Enums.Driver_WORKINGSTATUS.AVAILABLE);

                                    if (success)
                                    {
                                        break;

                                    }
                                    else
                                        loopCnt++;

                                }
                            }).Start();

                            Thread.Sleep(500);


                            new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);
                           // (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshData();


                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                db.stp_BookingLog(jobId, AppVars.LoginObj.UserName.ToStr(), "Controller Pressed NO Pickup from Driver (" + driverNo[0] + ")");
                            }
                        }
                        else
                        {

                            MessageBox.Show("No Current Job Found");
                        }


                    }
                }
            }
            catch (Exception ex)
            {

               
            }
        }

        private void ReCallJob()
        {
            if (ddl_Driver.SelectedValue == null)
                return;
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();
              
                int statusId=0;
                long jobId=0;
                string driverNO=string.Empty;
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    Fleet_DriverQueueList objLogin = db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Status == true && c.DriverId == driverId);

                    if (objLogin != null)
                    {

                        statusId = objLogin.DriverWorkStatusId.ToInt();
                        jobId = objLogin.CurrentJobId.ToLong();
                        driverNO = objLogin.Fleet_Driver.DriverNo.ToStr();

                    }
                }


                if (jobId == 0)
                {

                     
                    MessageBox.Show("No Current Job Found");
                    return;
                }
                else
                {
                    if (statusId == Enums.Driver_WORKINGSTATUS.NOTAVAILABLE || statusId == Enums.Driver_WORKINGSTATUS.SOONTOCLEAR)
                    {

                        MessageBox.Show("Job cannot be Re-Call as driver is on " + "POB or STC Status.");
                        return;

                    }
                }


                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to Re-Call a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    new Thread(delegate()
                    {
                        int loopCnt = 1;
                        bool success = false;
                        while (loopCnt < 3)
                        {



                                success = General.ReCallBooking(jobId, driverId);
                     
                            if (success)
                            {
                                break;

                            }
                            else
                                loopCnt++;



                        }
                    }).Start();


                    using (TaxiDataContext dbX = new TaxiDataContext())
                    {
                        dbX.stp_BookingLog(jobId, AppVars.LoginObj.UserName.ToStr(), "Recall Job from Driver (" + driverNO + ")");
                    }

                   // new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);

                
                }
                
            }
            catch
            {


            }
        }


        private void ViewBooking()
        {
            try
            {

                int driverId = ddl_Driver.SelectedValue.ToInt();

                long currentJobId = General.GetQueryable<Fleet_DriverQueueList>(a => a.Status == true && a.DriverId == driverId).FirstOrDefault().DefaultIfEmpty().CurrentJobId.ToLong();

                if (currentJobId > 0)
                {

                    General.ShowBookingForm(currentJobId.ToInt(), true, "", "", Enums.BOOKING_TYPES.LOCAL);
                }
                else
                {
                    MessageBox.Show("No Current Job Found");
                }
            }
            catch(Exception ex)
            {


            }

        }


        private void ForceArrived()
        {




            if (ddl_Driver.SelectedValue == null)
                return;

            try
            {

                int driverId = ddl_Driver.SelectedValue.ToInt();

                string actionType = "";



                actionType = "<<Arrive Job>>";



                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var obj = db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Status == true && c.DriverId == driverId && c.CurrentJobId != null && (c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONROUTE));


                    if (obj != null)
                    {

                        int statusId = obj.DriverWorkStatusId.ToInt();

                        long jobId = obj.CurrentJobId.ToLong();

                        string msg = "request pda=" + driverId + "=" + jobId + "=" + actionType + jobId + "=11";


                        new Thread(delegate()
                        {
                            General.SendMessageToPDA(msg);


                        }).Start();

                        System.Threading.Thread.Sleep(1000);

                    }
                    else
                    {
                        MessageBox.Show("No Current Job Found");

                    }


                }


            }
            catch
            {


            }
        }

        private void ForcePOB()
        {
            if (ddl_Driver.SelectedValue == null)
                return;

            try
            {

                int driverId = ddl_Driver.SelectedValue.ToInt();

                string actionType = "";



                actionType = "<<POB Job>>";



                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var obj = db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Status == true && c.DriverId == driverId && c.CurrentJobId != null && (c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONROUTE || c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ARRIVED));


                    if (obj != null)
                    {

                        int statusId = obj.DriverWorkStatusId.ToInt();

                        long jobId = obj.CurrentJobId.ToLong();

                        string msg = "request pda=" + driverId + "=" + jobId + "=" + actionType + jobId + "=11";


                        new Thread(delegate()
                        {
                            General.SendMessageToPDA(msg);


                        }).Start();

                        System.Threading.Thread.Sleep(1000);

                    }
                    else
                    {
                        MessageBox.Show("No Current Job Found");

                    }


                }


            }
            catch
            {


            }

        }

        private void LoginDriver()
        {
            if (ddl_Driver.SelectedValue == null)
                return;
            try
            {
                int driverId=ddl_Driver.SelectedValue.ToInt();

                using (TaxiDataContext db = new TaxiDataContext())
                {



                    if(db.Fleet_DriverQueueLists.Count(c=>c.Status==true && c.DriverId==driverId)==0)
                    {

                        if (db.Fleet_Driver_PDASettings.Where(c => c.DriverId == driverId && c.HasCompanyCars != null && c.HasCompanyCars == true).Count() > 0)
                        {



                        }
                        else
                        {

                            db.stp_LoginLogoutDriver(driverId, true, null);
                        }
                        new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD_DRIVER);
                    }
                }

               
               // (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshDashBoardDrivers();

            }
            catch
            {


            }


        }

        private void BreakDriver()
        {
            if (ddl_Driver.SelectedValue == null)
                return;
            try
            {

                if (General.GetQueryable<Fleet_DriverQueueList>(null).Count(c => c.Status == true && c.DriverId == ddl_Driver.SelectedValue.ToInt()) == 0)
                    return;
               

             //   ddl_Driver.DropDownListElement.AutoCompleteSuggest.SU.AutoCompleteSuggest.SuggestMode

                int statusId=Enums.Driver_WORKINGSTATUS.ONBREAK;

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var obj= db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Status == true && c.DriverId == ddl_Driver.SelectedValue.ToInt()); 
                   

                    if(obj!=null && (obj.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.AVAILABLE || obj.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.ONBREAK))
                    {
                        if(obj.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.ONBREAK)
                            statusId=Enums.Driver_WORKINGSTATUS.AVAILABLE;

                         db.stp_ChangeDriverStatus(ddl_Driver.SelectedValue.ToInt(),statusId);

                         new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD_DRIVER);

                          try
                          {

                              int loopCnt = 1;


                              while (loopCnt < 3)
                              {

                                  bool success = General.SendMessageToPDA("request pda=" + ddl_Driver.SelectedValue.ToInt() + "=" + 0 + "="
                                                          + "Message>>" + "onbreak--x" + ">>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "=4").Result.ToBool();

                                  if (success)
                                  {
                                    

                                      break;

                                  }
                                  else
                                      loopCnt++;


                              }


                          }
                          catch (Exception ex)
                          {


                          }
                         

                    }
                }
             

              
               
            }
            catch
            {


            }
        }


        private void CompletedJobs()
        {

            try
            {
                if (ddl_Driver.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Select any Driver");
                    return;
                }
                else
                {


                    frmDriverEarning frm = new frmDriverEarning();
                    frm.SelectedDriverId = ddl_Driver.SelectedValue.ToInt();
                    frm.ShowDialog();
                    frm.Dispose();
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void SMSMessage()
        {

            try
            {
                if (ddl_Driver.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Select any Driver");

                    return;
                }
                else
                {
                    int driverId = ddl_Driver.SelectedValue.ToInt();



                    string mobileNo = General.GetObject<Fleet_Driver>(c => c.Id == driverId).DefaultIfEmpty().MobileNo.ToStr();



                    frmSMSAll frm = new frmSMSAll(mobileNo);
                    frm.ShowDialog();



                }

            }
            catch (Exception ex)
            {


            }
        }

        private void DriverInformation()
        {

            try
            {
                if (ddl_Driver.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Select any Driver");

                    return;
                }
                else
                {
                    int driverId = ddl_Driver.SelectedValue.ToInt();
                    frmSearchDriver frm = new frmSearchDriver(driverId);
                    frm.ShowDialog();

                }

            }
            catch (Exception ex)
            {

            }
        }



        private void SinBinDriver()
        {

            if (ddl_Driver.SelectedValue == null)
            {
                ENUtils.ShowMessage("Select any Driver");

                return;
            }



            try
            {

                frmSinBin frm = new frmSinBin(ddl_Driver.SelectedValue.ToInt());
                frm.ShowDialog();
                frm.Dispose();

            }
            catch (Exception ex)
            {


            }
        }

        private void LogoutDriver()
        {
            try
            {
                if (ddl_Driver.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Select any Driver");

                    return;
                }
                else
                {
                    int driverId = ddl_Driver.SelectedValue.ToInt();
                    long Id = 0;
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        Id = db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Status == true && c.DriverId == driverId).DefaultIfEmpty().Id;
                    }

                    if (Id > 0)
                    {


                        ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard"))
                            .ForceLogoutDriver(Id, ddl_Driver.SelectedValue.ToInt(),false);

                        Thread.Sleep(500);
                        new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD_DRIVER);
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void PDAMessage()
        {
            try
            {
                if (ddl_Driver.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Select any Driver");

                    return;
                }
                else
                {
                    int driverId = ddl_Driver.SelectedValue.ToInt();
                    frmMessages frm = new frmMessages(driverId);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    frm.Dispose();
                }

            }
            catch (Exception ex)
            {

            }

        }

        public void ShowDespatchForm(Booking obj)
        {
            try
            {
                bool rtn = false;

                frmDespatchJob frm = new frmDespatchJob(obj);

                frm.ShowDialog();



                if (frm.SmsThread != null)
                    frm.SmsThread.Start();

                rtn = frm.SuccessDespatched;

                frm.Dispose();

                GC.Collect();

            }
            catch (Exception ex)
            {


            }

        }


        private void ClearJob()
        {
            try
            {
                if (ddl_Driver.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Select any Driver");

                    return;
                }
                else
                {
                    int driverId = ddl_Driver.SelectedValue.ToInt();

                    var list = from a in General.GetQueryable<Fleet_DriverQueueList>(c => c.Id > 0)
                               where a.DriverId == driverId && a.Status == true && a.CurrentJobId != null
                               select new
                               {

                                   Id = a.Id,
                                   currentjobId = a.CurrentJobId
                               };

                    string val = list.ToStr();

                    if (list.Count() != 0)
                    {

                        new Thread(delegate()
                        {
                            General.ClearDriverCurrentJob(list.FirstOrDefault().Id);

                        }).Start();


                        System.Threading.Thread.Sleep(1000);

                        new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);
                       // (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("No Current Job Found");

                    }

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void btnTrack_Click(object sender, EventArgs e)
        {
     
                TrackDriver();
        }

    
        private void btnSMSMessage_Click(object sender, EventArgs e)
        {

            SMSMessage();
        }      

        private void btnRecoverJob_Click(object sender, EventArgs e)
        {
            ReCallJob();
          
        }
       
        private void btnDriverInformation_Click(object sender, EventArgs e)
        {
            DriverInformation();
           
        }

        private void btnSinBin_Click(object sender, EventArgs e)
        {
            SinBinDriver();
        }    


        private void btnLogoutDriver_Click(object sender, EventArgs e)
        {
           
            LogoutDriver();
        }
     
        private void btnPDAMessage_Click(object sender, EventArgs e)
        {
            PDAMessage();
        }

        

        private void btnViewBooking_Click(object sender, EventArgs e)
        {
            ViewBooking();
        }
      
       

        private void btnClearJob_Click(object sender, EventArgs e)
        {
            ClearJob();
        }

       

        private void btnNoShow_Click(object sender, EventArgs e)
        {
            NoShow();
        }

        private void btnMobile_Click(object sender, EventArgs e)
        {
            ForcePOB();
        }

        private void btnForceArrivw_Click(object sender, EventArgs e)
        {
            ForceArrived();
        }


      
      

        private void btnBreak_Click(object sender, EventArgs e)
        {
            BreakDriver();
        }

        private void btnJobsDriverEarning_Click(object sender, EventArgs e)
        {

            CompletedJobs();
           
        }


        private void UnPanic(int driverId)
        {
            if (driverId == 0)
                return;




            try
            {

                int loopCnt = 1;


               

                    bool success = General.SendMessageToPDA("request pda=" + driverId + "=" + 0 + "="
                                            + "Message>>" + "unpanic" + ">>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "=4").Result.ToBool();

                    if (success)
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_PanicUnPanicDriver(driverId, false);

                        }

                        new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD_DRIVER);                    

                    }
                  


              

            
            }
            catch (Exception ex)
            {


            }



        }



        private void btnPlot_Click(object sender, EventArgs e)
        {

            UnPanic(ddl_Driver.SelectedValue.ToInt());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginDriver();
        }


       
    }
}
