using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Utils;
using Taxi_Model;
using Telerik.WinControls;
using Taxi_BLL;
using System.Threading;
using System.IO.Ports;
using Taxi_AppMain.Classes;
using System.Reflection;
using System.Diagnostics;

using System.IO;

using System.Net;

namespace Taxi_AppMain
{
    public partial class frmForceCompleteJob : Form
    {
        
       
        private long _JobId;

        public long JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
        }
        public frmForceCompleteJob(Booking booking)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = booking.DriverId;


            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
        }


       




       



       

        void ddl_Driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                OnKeyDespatch();
            }
        }

        private void OnKeyDespatch()
        {
              string text = ddl_Driver.Text;

                if (!string.IsNullOrEmpty(text.Trim()))
                {
                    RadListDataItem item = ddl_Driver.Items.FirstOrDefault(c => c.Text == text);

                    if (item != null)
                    {

                        ddl_Driver.SelectedItem = item;
                    }
                }

                Despatch();

        }

        void ddl_Driver_KeyUp(object sender, KeyEventArgs e)
        {
             if ( e.KeyCode == Keys.Enter)
            {
               OnKeyDespatch();
            }
        }

      


        private Fleet_Driver _objDriver;

        public Fleet_Driver ObjDriver
        {
            get { return _objDriver; }
            set { _objDriver = value; }
        }

        private bool _IsAutoDespatchActivity;

        public bool IsAutoDespatchActivity
        {
            get { return _IsAutoDespatchActivity; }
            set { _IsAutoDespatchActivity = value; }
        }

       





        private int? _SelectedDriverId;

        public int? SelectedDriverId
        {
            get { return _SelectedDriverId; }
            set { _SelectedDriverId = value; }
        }


 


        Booking objBooking = null;
        delegate void UIDel();

        void frmDespatchJob_Load(object sender, EventArgs e)
        {

            try
            {

                LoadDespatchSettings(objBooking);

                LoadAndSelectDriver();


                            

            
            }
            catch (Exception ex)
            {


            }
        }


      


    


        private void LoadAndSelectDriver()
        {
            try
            {

                ComboFunctions.FillDriverNoCombo(ddl_Driver);
                //ComboFunctions.FillDriverNoQueueCombo(ddl_Driver);
            
                IsLoaded = true;

                if (this.SelectedDriverId != null)
                {
                    ddl_Driver.SelectedValue = this.SelectedDriverId;
                }

            }
            catch (Exception ex)
            {


            }


        }

        public void LoadDespatchSettings(Booking booking)
        {
            try
            {

                if (booking != null)
                {
                    this.objBooking = booking;


                    lblDespatchHeading.Text = "Complete Job Ref # : " + objBooking.BookingNo.ToStr();
                    

                    if (booking.CustomerMobileNo.ToStr().Trim() == string.Empty)
                    {
                        lblCustomerMobNo.Visible = false;
                        txtCustomerMobNo.Visible = false;
                    }
                    else
                    {
                        lblCustomerMobNo.Visible = true;
                        txtCustomerMobNo.Visible = true;



                        string mobNo = booking.CustomerMobileNo.ToStr().Trim();
                        if (!Debugger.IsAttached)
                        {
                            int idx = -1;
                            if (mobNo.StartsWith("044") == true)
                            {
                                idx = mobNo.IndexOf("044");
                                mobNo = mobNo.Substring(idx + 3);
                                mobNo = mobNo.Insert(0, "+44");


                            }

                            if (mobNo.StartsWith("07"))
                            {
                                mobNo = mobNo.Substring(1);
                            }

                            if (mobNo.StartsWith("0440") == false || mobNo.StartsWith("+440") == false)
                                mobNo = mobNo.Insert(0, "+44");
                        }



                        txtCustomerMobNo.Text = mobNo;


                       

                    }

                }
            }
            catch (Exception ex)
            {


            }

        }



        private void btnDespatch_Click(object sender, EventArgs e)
        {
           
            Despatch();
           
        }

        private void Despatch()
        {
             
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            if (driverId == null)
            {
                ENUtils.ShowMessage("Please select a Driver");
                return;
            }
            else
            {

                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr().Trim() == "green_metro_cAr$_reAd!ng_ltd")
                {


                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        if (db.Fleet_DriverQueueLists.Where(c => c.Status == true && c.DriverId == driverId).Count() == 0)
                        {
                            ENUtils.ShowMessage("Please Login this Driver.");
                            return;


                        }

                    }
                }


            }


            //if (ObjDriver.VehicleTypeId != null && objBooking.Fleet_VehicleType.VehicleType.ToLower().StartsWith("mpv")
            //       && objBooking.VehicleTypeId != ObjDriver.VehicleTypeId)
            //{
            //    if (DialogResult.No == RadMessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
            //                                              "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine
            //                                          + "Do you still want to Despatch this Job to that Driver " + ObjDriver.DriverNo + " ?", "Despatch", MessageBoxButtons.YesNo))
            //    {
            //        return;

            //    }

            //}


            if (AppVars.objSubCompany.CompanyName.ToStr().Trim() == string.Empty)
            {
                ENUtils.ShowMessage("InComplete Company Information.." + Environment.NewLine +
                                    "Please Enter Company Information");


                frmSysPolicy frm = new frmSysPolicy(1);
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.ControlBox = true;
                frm.MaximizeBox = false;
                frm.Size = new Size(750, 600);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }


            bool IsContinue = true;

            if (lblNocMessage.Text.Contains("MOT Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver MOT has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
            else if (lblNocMessage.Text.Contains("MOT 2 Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver MOT 2 has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
            else if (lblNocMessage.Text.Contains("License Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver Driving License has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
            else if (lblNocMessage.Text.Contains("Insurance Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver Insurance has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }

            else if (lblNocMessage.Text.Contains("PCO Driver Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver PCO has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }


            else if (lblNocMessage.Text.Contains("PCO Vehicle Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver PCO Vehicle has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
            //else if (ObjDriver != null)
            //{
            //    Fleet_DriverQueueList drv = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK
            //                                                                                 && c.DriverId == ObjDriver.Id).FirstOrDefault();

            //    if (drv != null)
            //    {
            //        if (DialogResult.No == RadMessageBox.Show("This Driver is on Break." + Environment.NewLine
            //                                           + "Do you still want to dispatch the job ?", "Despatch", MessageBoxButtons.YesNo))
            //        {
            //            IsContinue = false;

            //        }
            //    }
            //}


            if (ObjDriver!=null && ObjDriver.VehicleTypeId != null)
            {

                if (objBooking.ExcludedDriverIds.ToStr().Trim().Length > 0 && objBooking.ExcludedDriverIds.ToStr().Trim().Contains("," + ObjDriver.Id + ","))
                {

                    MessageBox.Show("Driver exist in Excluded List");
                    return;


                }


            }


                if (IsContinue == false) return;

            DespatchJob();

        }


        private System.Threading.Thread smsThread = null;

        public System.Threading.Thread SmsThread
        {
            get { return smsThread; }
            set { smsThread = value; }
        }


   
     //   bool enablePDA = false;
        public bool OnDespatching(ref List<string> listofErrors)
        {
            bool rtn = false;

        
            string smsError1 = string.Empty;
            string smsError2 = string.Empty;

            try
            {

              
                    rtn = true;
            }
            catch (Exception ex)
            {
              //  IsSuccess1 = false;

                listofErrors.Add(ex.Message);
                rtn = false;
            }


            return rtn;

        }

        //private void ClosePort(SerialPort port)
        //{
        //    if (port != null)
        //        port.Close();


        //}

        public bool SuccessDespatched = false;

        private bool _IsFOJ;

        public bool IsFOJ
        {
            get { return _IsFOJ; }
            set { _IsFOJ = value; }
        }


        public void DespatchJob()
        {

            List<string> listofErrors = new List<string>();

            bool IsDespatched = OnDespatching(ref listofErrors);

            if (IsDespatched)
            {
            
                try
                {

               
                    (new TaxiDataContext()).stp_DespatchedJob(this.JobId, ObjDriver.Id,false, false, false, false, AppVars.LoginObj.LoginName.ToStr(), Enums.BOOKINGSTATUS.DISPATCHED);
               
                

                    if (!this.IsAutoDespatchActivity)
                    {
                        new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ACTIVEBOOKINGS_DASHBOARD);                   
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                   
                    ENUtils.ShowMessage(ex.Message);
               
                }
            }
      

        }


        //private void RefreshBookingList()
        //{
        //    General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");


        //}
     





        bool IsLoaded = false;
        private void ddl_Driver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (IsLoaded)
                {
                    int id = ddl_Driver.SelectedValue.ToInt();
                    Fleet_Driver obj = General.GetObject<Fleet_Driver>(c => c.Id == id);
                    LoadDriverSettings(obj);
                }

            }
            catch (Exception ex)
            {
               

            }


        }


        private void LoadDriverSettings(Fleet_Driver driver)
        {
             try
            {
            if (driver != null)
            {
                this.ObjDriver = driver;
                lblDriverMobNo.Visible = true;
                txtDriverMobNo.Visible = true;
                if (string.IsNullOrEmpty(driver.MobileNo.ToStr().Trim()))
                {
                    txtDriverMobNo.Text = "Not found";
                    txtDriverMobNo.ForeColor = Color.Red;
                }
                else
                {


                    string mobNo = driver.MobileNo.ToStr().Trim();
                    int idx = -1;
                    if (!Debugger.IsAttached)
                    {
                        if (mobNo.StartsWith("044") == true)
                        {
                            idx = mobNo.IndexOf("044");
                            mobNo = mobNo.Substring(idx + 3);
                            mobNo = mobNo.Insert(0, "+44");


                        }

                        if (mobNo.StartsWith("07"))
                        {
                            mobNo = mobNo.Substring(1);
                        }

                        if (mobNo.StartsWith("044") == false || mobNo.StartsWith("+44") == false)
                            mobNo = mobNo.Insert(0, "+44");
                    }

                    txtDriverMobNo.Text = mobNo;
                    DateTime nowDate = DateTime.Now.ToDate();

                    // New Code for PDA Driver No
                      
                    //

                    if (driver.MOTExpiryDate != null && driver.MOTExpiryDate.ToDate() < nowDate)
                    {
                        lblNocMessage.Visible = true;
                        lblNocMessage.Text = "Driver MOT Expired : " + string.Format("{0:dd/MM/yyyy}", driver.MOTExpiryDate);
                        lblNocMessage.ForeColor = Color.Red;


                    }
                    else if (driver.MOT2ExpiryDate != null && driver.MOT2ExpiryDate.ToDate() < nowDate)
                    {
                        lblNocMessage.Visible = true;
                        lblNocMessage.Text = "Driver MOT 2 Expired : " + string.Format("{0:dd/MM/yyyy}", driver.MOT2ExpiryDate);
                        lblNocMessage.ForeColor = Color.Red;

                    }
                    else if (driver.InsuranceExpiryDate != null && driver.InsuranceExpiryDate.ToDate() < nowDate)
                    {
                        lblNocMessage.Visible = true;
                        lblNocMessage.Text = "Insurance Expired : " + string.Format("{0:dd/MM/yyyy}", driver.InsuranceExpiryDate);
                        lblNocMessage.ForeColor = Color.Red;

                    }

                    else if (driver.PCODriverExpiryDate != null && driver.PCODriverExpiryDate.ToDate() < nowDate)
                    {
                        lblNocMessage.Visible = true;
                        lblNocMessage.Text = "PCO Driver Expired : " + string.Format("{0:dd/MM/yyyy}", driver.PCODriverExpiryDate);
                        lblNocMessage.ForeColor = Color.Red;

                    }

                    else if (driver.PCOVehicleExpiryDate != null && driver.PCOVehicleExpiryDate.ToDate() < nowDate)
                    {
                        lblNocMessage.Visible = true;
                        lblNocMessage.Text = "PCO Vehicle Expired : " + string.Format("{0:dd/MM/yyyy}", driver.PCOVehicleExpiryDate);
                        lblNocMessage.ForeColor = Color.Red;
                    }

                    else if (driver.DrivingLicenseExpiryDate != null && driver.DrivingLicenseExpiryDate.ToDate() < nowDate)
                    {
                        lblNocMessage.Visible = true;
                        lblNocMessage.Text = "Driving License Expired : " + string.Format("{0:dd/MM/yyyy}", driver.DrivingLicenseExpiryDate);
                        lblNocMessage.ForeColor = Color.Red;
                    }

                    else
                    {
                        lblNocMessage.Visible = false;
                    }
                }
            }

            }
             catch (Exception ex)
             {


             }

        }

      
        private void frmDespatchJob_KeyDown(object sender, KeyEventArgs e)
        {
            
             if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

       
       


    }
}
