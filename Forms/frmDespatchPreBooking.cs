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
using System.Net.Sockets;
using System.Net;

namespace Taxi_AppMain
{
    public partial class frmDespatchPreBooking : Form
    {
      
       
        private long _JobId;

        public long JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
        }
        public frmDespatchPreBooking(Booking booking)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = booking.DriverId;


            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
            FormatShiftGrid();
        }


        public frmDespatchPreBooking(Booking booking,bool reSendJob)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = booking.DriverId;
         
            FormatShiftGrid();
            ddl_Driver.Enabled = !reSendJob;
        }

        public frmDespatchPreBooking(Booking booking,int? driverId, bool reSendJob)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = driverId;

            FormatShiftGrid();
            ddl_Driver.Enabled = !reSendJob;
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
                if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                {
                    ComboFunctions.FillAllPDADLoginriverIdCombo(ddl_Driver);
                }
                else
                {

                    ComboFunctions.FillPDADLoginriverIdCombo(ddl_Driver);
                }
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
                    lblJobPickupTime.Text = "Job Pickup :  " + string.Format("{0:dd/MM/yyyy HH:mm}", this.objBooking.PickupDateTime);
                      
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



            if (ObjDriver.VehicleTypeId != null)
            {
                if (objBooking.AttributeValues.ToStr().Trim().Length > 0)
                {

                    string[] bookingAttrs = objBooking.AttributeValues.ToStr().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string drvAttributes = ObjDriver.AttributeValues.ToStr() + "," + ObjDriver.Fleet_VehicleType.AttributeValues;

                    int totalAttr = bookingAttrs.Count();
                    int matchCnt = 0;
                    string unmatchedAttrValue = string.Empty;
                    string[] drvAttrsArr = drvAttributes.ToStr().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in bookingAttrs)
                    {


                        if (drvAttrsArr.Count(c => c.ToLower() == item.ToLower()) > 0)
                        {
                            matchCnt++;

                        }
                        else
                        {

                            unmatchedAttrValue += item + ",";
                        }
                    }

                    if (matchCnt != totalAttr)
                    {

                        if (unmatchedAttrValue.EndsWith(","))
                        {
                            unmatchedAttrValue = unmatchedAttrValue.Substring(0, unmatchedAttrValue.LastIndexOf(","));

                        }

                        MessageBox.Show(("Driver : " + ObjDriver.DriverNo + " doesn't have attributes (" + unmatchedAttrValue + ")"), "Warning");
                        return;
                    }
                }

                else if (AppVars.listUserRights.Count(c => c.functionId == "RESTRICT ON DESPATCH JOB TO INVALID VEHICLE DRIVER") > 0)
                {

                    string vehAttributes = objBooking.Fleet_VehicleType.DefaultIfEmpty().AttributeValues.ToStr().Trim();

                    if (vehAttributes.Length > 0)
                    {

                        bool MatchedAttr = false;
                        foreach (var item in vehAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (ObjDriver.VehicleTypeId.ToInt() == item.ToInt())
                            {
                                MatchedAttr = true;
                                break;

                            }

                        }



                        if (MatchedAttr == false)
                        {

                            MessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                    "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + ".");

                            return;

                        }

                    }
                    else
                    {

                        if (ObjDriver.Fleet_VehicleType.NoofPassengers.ToInt() < objBooking.Fleet_VehicleType.NoofPassengers.ToInt())
                        {
                            MessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                      "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + ".");


                            return;
                        }
                    }

                }
                else
                {


                    if (ObjDriver.Fleet_VehicleType.NoofPassengers.ToInt() < objBooking.Fleet_VehicleType.NoofPassengers.ToInt())
                    {
                        if (DialogResult.No == MessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                  "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine
                                                              + "Do you still want to Dispatch this Job to that Driver " + ObjDriver.DriverNo + " ?", "Dispatch", MessageBoxButtons.YesNo))
                        {
                            return;

                        }
                    }

                }

                //if (ObjDriver.Fleet_VehicleType.NoofPassengers.ToInt() < objBooking.Fleet_VehicleType.NoofPassengers.ToInt())
                //{
                //    if (DialogResult.No == RadMessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                //                                              "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine
                //                                          + "Do you still want to Despatch this Job to that Driver " + ObjDriver.DriverNo + " ?", "Despatch", MessageBoxButtons.YesNo))
                //    {
                //        return;

                //    }



                //}
                //else
                //{

                //    if (objBooking.Fleet_VehicleType.VehicleType.ToLower().StartsWith("mpv") && objBooking.VehicleTypeId != ObjDriver.VehicleTypeId)
                //    {
                //        if (DialogResult.No == RadMessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                //                                                  "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine
                //                                              + "Do you still want to Despatch this Job to that Driver " + ObjDriver.DriverNo + " ?", "Despatch", MessageBoxButtons.YesNo))
                //        {
                //            return;

                //        }

                //    }
                //}
            }


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


            if (lblNocMessage.Text.Contains("Expired") || lblNocMessage.Text.ToStr().ToLower().Contains("expiring"))
            {
                if (HasDespatchExpiredDriversRights())
                {

                    if (DialogResult.No == RadMessageBox.Show(lblNocMessage.Text.Replace(" | ", Environment.NewLine) + Environment.NewLine + Environment.NewLine
                                                            + "Are you sure you want to Dispatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                    {
                        IsContinue = false;

                    }
                }
                else
                {

                    if (DialogResult.OK == RadMessageBox.Show("You cannot Dispatch the Job to this Driver!" + Environment.NewLine + lblNocMessage.Text.Replace(" | ", Environment.NewLine), "Dispatch", MessageBoxButtons.OK))
                    {
                        IsContinue = false;

                    }
                }
            }

            //if (lblNocMessage.Text.Contains("MOT Expired"))
            //{


            //    if (HasDespatchExpiredDriversRights())
            //    {

            //        if (DialogResult.No == RadMessageBox.Show("This Driver MOT has been expired" + Environment.NewLine
            //                                                + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
            //        {
            //            IsContinue = false;

            //        }
            //    }
            //    else
            //    {
            //        if (DialogResult.OK == RadMessageBox.Show("This Driver MOT has been expired" + Environment.NewLine
            //                                                + "You cannot Despatch the Job!", "Despatch", MessageBoxButtons.OK))
            //        {
            //            IsContinue = false;

            //        }

            //    }
            //}
            //else if (lblNocMessage.Text.Contains("MOT 2 Expired"))
            //{
            //    if (HasDespatchExpiredDriversRights())
            //    {


            //        if (DialogResult.No == RadMessageBox.Show("This Driver MOT 2 has been expired" + Environment.NewLine
            //                                                + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
            //        {
            //            IsContinue = false;

            //        }
            //    }
            //    else
            //    {
            //        if (DialogResult.OK == RadMessageBox.Show("This Driver MOT 2 has been expired" + Environment.NewLine
            //                                                + "You cannot Despatch the Job!", "Despatch", MessageBoxButtons.OK))
            //        {
            //            IsContinue = false;

            //        }

            //    }

            //}
            //else if (lblNocMessage.Text.Contains("License Expired"))
            //{
            //    if (HasDespatchExpiredDriversRights())
            //    {
            //        if (DialogResult.No == RadMessageBox.Show("This Driver Driving License has been expired" + Environment.NewLine
            //                                                + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
            //        {
            //            IsContinue = false;

            //        }
            //    }
            //    else
            //    {
            //        if (DialogResult.OK == RadMessageBox.Show("This Driver Driving License has been expired" + Environment.NewLine
            //                                               + "You cannot Despatch the Job!", "Despatch", MessageBoxButtons.OK))
            //        {
            //            IsContinue = false;

            //        }


            //    }
            //}
            //else if (lblNocMessage.Text.Contains("Insurance Expired"))
            //{
            //    if (HasDespatchExpiredDriversRights())
            //    {
            //        if (DialogResult.No == RadMessageBox.Show("This Driver Insurance has been expired" + Environment.NewLine
            //                                                + "Are you sure you want to Despatch the Job ?!", "Despatch", MessageBoxButtons.YesNo))
            //        {
            //            IsContinue = false;

            //        }
            //    }
            //    else
            //    {
            //        if (DialogResult.OK == RadMessageBox.Show("This Driver Insurance has been expired" + Environment.NewLine
            //                                              + "You cannot Despatch the Job!", "Despatch", MessageBoxButtons.OK))
            //        {
            //            IsContinue = false;

            //        }

            //    }
            //}

            //else if (lblNocMessage.Text.Contains("PCO Driver Expired"))
            //{
            //    if (HasDespatchExpiredDriversRights())
            //    {
            //        if (DialogResult.No == RadMessageBox.Show("This Driver PCO has been expired" + Environment.NewLine
            //                                                + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
            //        {
            //            IsContinue = false;

            //        }
            //    }
            //    else
            //    {
            //        if (DialogResult.OK == RadMessageBox.Show("This Driver PCO has been expired" + Environment.NewLine
            //                                              + "You cannot Despatch the Job!", "Despatch", MessageBoxButtons.OK))
            //        {
            //            IsContinue = false;

            //        }

            //    }
            //}


            //else if (lblNocMessage.Text.Contains("PCO Vehicle Expired"))
            //{
            //    if (HasDespatchExpiredDriversRights())
            //    {
            //        if (DialogResult.No == RadMessageBox.Show("This Driver PCO Vehicle has been expired" + Environment.NewLine
            //                                                + "Are you sure you want to Despatch the Job ?", "Despatch", MessageBoxButtons.YesNo))
            //        {
            //            IsContinue = false;

            //        }
            //    }
            //    else
            //    {
            //        if (DialogResult.OK == RadMessageBox.Show("This Driver PCO Vehicle has been expired" + Environment.NewLine
            //                                              + "You cannot Despatch the Job!", "Despatch", MessageBoxButtons.OK))
            //        {
            //            IsContinue = false;

            //        }

            //    }
            //}
            else if (ObjDriver != null)
            {


              
                    if (AppVars.objPolicyConfiguration.DisableJobOfferToOnBreakDrv.ToBool() == false &&
                       General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK
                                                                            && c.DriverId == ObjDriver.Id).Count() > 0)
                    {
                        if (DialogResult.No == RadMessageBox.Show("This Driver is on Break." + Environment.NewLine
                                                           + "Do you still want to dispatch the job ?", "Dispatch", MessageBoxButtons.YesNo))
                        {
                            IsContinue = false;

                        }
                    }

               
            
            }




            if (IsContinue == false) return;

            DespatchJob();

        }


        private bool HasDespatchExpiredDriversRights()
        {

            return AppVars.listUserRights.Count(c => c.formName == "frmDriver" && c.functionId == "DESPATCH JOB TO EXPIRED DRIVERS") > 0;
            


            

        }


        private System.Threading.Thread smsThread = null;

        public System.Threading.Thread SmsThread
        {
            get { return smsThread; }
            set { smsThread = value; }
        }


   
        bool enablePDA = false;
        public bool OnDespatching(ref List<string> listofErrors)
        {
            bool rtn = true;

         


         try
            {

                if (ObjDriver != null && objBooking != null)
                {

                    string customerMobileNo = objBooking.CustomerMobileNo.ToStr().Trim();
                    // For testing Purpose
                  //  customerMobileNo = "03323755646"; 
                    //
                    string customerName = objBooking.CustomerName;

                    string via = string.Join(",", objBooking.Booking_ViaLocations.Select(c => c.ViaLocValue.ToStr()).ToArray<string>());
                    
                    if (!string.IsNullOrEmpty(via.Trim()))
                        via = "Via: " + via;

                    string specialReq = objBooking.SpecialRequirements.ToStr().Trim();
                    if (!string.IsNullOrEmpty(specialReq))
                        specialReq = "Special Req: " + specialReq;


                      enablePDA = AppVars.objPolicyConfiguration.EnablePDA.ToBool();

                    string custNo = !string.IsNullOrEmpty(objBooking.CustomerMobileNo) ? objBooking.CustomerMobileNo : objBooking.CustomerPhoneNo;
              


                    // Send To Driver






                    string paymentType = objBooking.Gen_PaymentType.PaymentCategoryId == null ? objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr()
                             : objBooking.Gen_PaymentType.Gen_PaymentCategory.CategoryName.ToStr();

                       

                                string strDeviceRegistrationId = ObjDriver.DeviceId.ToStr();                           
                                string journey = "O/W";

                                //if (objBooking.JourneyTypeId.ToInt() == 2)
                                //{
                                //    journey = "Return";

                                //}
                                if (objBooking.JourneyTypeId.ToInt() == 3)
                                {
                                    journey = "W/R";
                                }


                                string IsExtra = (objBooking.CompanyId != null || objBooking.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT || objBooking.ToLocTypeId == Enums.LOCATION_TYPES.AIRPORT) ? "1" : "0";
                                int i = 1;
                                string viaP = "";

                               

                                if (objBooking.Booking_ViaLocations.Count > 0)
                                {
                               
                                    viaP =  string.Join(" * ", objBooking.Booking_ViaLocations.Select(c =>"("+i++.ToStr() + ")"+ c.ViaLocValue.ToStr()).ToArray<string>());
                                }


                                string mobileNo = objBooking.CustomerMobileNo.ToStr();
                                string telNo=objBooking.CustomerPhoneNo.ToStr();

                                decimal drvPdaVersion = ObjDriver.Fleet_Driver_PDASettings.Count > 0 ? ObjDriver.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() : 9.40m;


                                if (string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(telNo))
                                {
                                    mobileNo = telNo;
                                }
                                else if (!string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(telNo))
                                {
                                    mobileNo += "/" + telNo;
                                }


                                string pickUpPlot = "";                         
                                string dropOffPlot = "";
                                string companyName = string.Empty;

                                //if (objBooking.CompanyId != null && objBooking.Gen_Company.DefaultIfEmpty().AccountTypeId.ToInt() != Enums.ACCOUNT_TYPE.CASH)
                                //    companyName = objBooking.Gen_Company.DefaultIfEmpty().CompanyName;

                                if (drvPdaVersion < 11 && objBooking.CompanyId != null && objBooking.Gen_Company.DefaultIfEmpty().AccountTypeId.ToInt() != Enums.ACCOUNT_TYPE.CASH)
                                    companyName = objBooking.Gen_Company.DefaultIfEmpty().CompanyName;
                                else
                                    companyName = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();



                                if (drvPdaVersion > 9 && drvPdaVersion != 13.4m)
                                {

                                    pickUpPlot = objBooking.ZoneId != null ? "<<<" + objBooking.Gen_Zone1.ZoneName.ToStr() : "";
                                    dropOffPlot = objBooking.DropOffZoneId != null ? "<<<" + objBooking.Gen_Zone.ZoneName.ToStr() : "";
                                }



                                string fromdoorno = objBooking.FromDoorNo.ToStr().Trim();
                                if (fromdoorno.Length > 0 && fromdoorno.WordCount() > 2 && fromdoorno.Contains(" "))
                                {

                                    try
                                    {

                                        fromdoorno = fromdoorno.Replace(" ", "-");
                                    }
                                    catch
                                    {


                                    }




                                }



                                    string startJobPrefix = "PreJobId:";
                                    if (AppVars.objPolicyConfiguration.PDAJobAlertOnly.ToBool() && ObjDriver.Fleet_Driver_PDASettings.Count > 0 && ObjDriver.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() >= 8.3m &&  ObjDriver.Fleet_Driver_PDASettings[0].ShowJobAsAlert.ToBool())
                                    {
                                        startJobPrefix = "AlertPreJobId:";
                                    }


                                    string fromAddress = objBooking.FromAddress.ToStr().Trim();
                                    string toAddress = objBooking.ToAddress.ToStr().Trim();

                                    if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE || objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                                    {
                                        fromAddress = objBooking.FromStreet.ToStr() + " " + objBooking.FromAddress.ToStr();

                                    }

                                    if (objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE || objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                                    {
                                        toAddress = objBooking.ToStreet.ToStr() + " " + objBooking.ToAddress.ToStr();

                                    }



                                    //try
                                    //{
                                    //    if (string.IsNullOrEmpty(General.GetPostCodeMatch(fromAddress.ToStr().Trim())) && fromAddress.ToStr().EndsWith(", UK") == false)
                                    //    {
                                    //        fromAddress = fromAddress.ToStr() + ", UK";

                                    //    }

                                    //    if (string.IsNullOrEmpty(General.GetPostCodeMatch(toAddress.ToStr().Trim())) && toAddress.ToStr().EndsWith(", UK") == false)
                                    //    {
                                    //        toAddress = toAddress.ToStr() + ", UK";

                                    //    }
                                    //}
                                    //catch
                                    //{

                                    //}

                    
                                    //half card and cash
                                    string specialRequirements = objBooking.SpecialRequirements.ToStr();
                                    if (objBooking.SecondaryPaymentTypeId != null && objBooking.CashFares.ToDecimal() > 0)
                                    {

                                        specialRequirements += " , Additional Cash Payment : " + objBooking.CashFares.ToDecimal();
                                    }


                      //              decimal pdafares = objBooking.FareRate.ToDecimal();

                    //                pdafares = objBooking.TotalCharges.ToDecimal();

                                    decimal pdafares = objBooking.GetType().GetProperty(AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim()).GetValue(objBooking, null).ToDecimal();


                                    string msg = string.Empty;

                                    if (drvPdaVersion >= 11 && AppVars.objPolicyConfiguration.PDAJobAlertOnly.ToBool() == false)
                                    {
                                        //string showFares = ",\"ShowFares\":\"" + objBooking.Gen_PaymentType.ShowFaresOnPDA.ToStr().Trim() + "\"";
                                        //string showSummary = ",\"ShowSummary\":\"" + "1" + "\"";

                                        string showFaresValue = objBooking.Gen_PaymentType.ShowFaresOnPDA.ToStr().Trim();

                                        string showFares = ",\"ShowFares\":\"" + showFaresValue + "\"";
                                        string showSummary = ",\"ShowSummary\":\"" + showFaresValue + "\"";

                        //string agentDetails = string.Empty;
                        //string parkingandWaiting = string.Empty;
                        //if (objBooking.CompanyId != null)
                        //{
                        //    agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objBooking.AgentCommission) + "\"";
                        //    parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objBooking.ParkingCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objBooking.WaitingCharges) + "\"";

                        //}
                        //else
                        //{

                        //    parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objBooking.CongtionCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objBooking.MeetAndGreetCharges)+"\"";
                        //    //

                        //}




                        string agentDetails = string.Empty;
                        string parkingandWaiting = string.Empty;
                        if (objBooking.CompanyId != null)
                        {


                            //if (AppVars.objPolicyConfiguration.PickCommissionDeductionFromJobsTotal.ToBool())
                            //{
                            agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objBooking.AgentCommission + objBooking.ServiceCharges.ToDecimal()) + "\"";
                            //}
                            //else
                            //{
                            //    agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objBooking.AgentCommission) + "\"";

                            //}

                            parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objBooking.ParkingCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objBooking.WaitingCharges) + "\"";


                            if (objBooking.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CASH)
                            {
                                pdafares = objBooking.FareRate.ToDecimal() + objBooking.ParkingCharges.ToDecimal() + objBooking.WaitingCharges.ToDecimal() + objBooking.AgentCommission.ToDecimal();


                            }

                        }
                        else
                        {

                            //if (objBooking.ServiceCharges>0)
                            //{
                            agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objBooking.ServiceCharges.ToDecimal()) + "\"";

                            //  }


                            parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objBooking.CongtionCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objBooking.MeetAndGreetCharges) + "\"";
                            //


                        }










                        if (drvPdaVersion == 23.50m && fromAddress.ToStr().Trim().Contains("-"))
                                        {
                                            fromAddress = fromAddress.Replace("-", "  ");

                                        }


                        if (specialRequirements.ToStr().Contains("\""))
                            specialRequirements = specialRequirements.ToStr().Replace("\"", "-").Trim();

                        msg =  startJobPrefix + "{ \"JobId\" :\"" + JobId.ToStr() +
                                              "\", \"Pickup\":\"" + (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? fromdoorno + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +
                                              "\", \"Destination\":\"" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) + "\"," +
                                              "\"PickupDateTime\":\"" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) + "\"" +
                                              ",\"Cust\":\"" + objBooking.CustomerName + "\",\"Mob\":\"" + mobileNo + " " + "\",\"Fare\":\"" + pdafares + "\",\"Vehicle\":\"" + objBooking.Fleet_VehicleType.VehicleType + "\",\"Account\":\"" + companyName + " " + "\"" +
                                             ",\"Lug\":\"" + objBooking.NoofLuggages.ToInt() + "\",\"Passengers\":\"" + objBooking.NoofPassengers.ToInt() + "\",\"Journey\":\"" + journey + "\",\"Payment\":\"" + paymentType + "\",\"Special\":\"" + specialRequirements + " " + "\",\"Extra\":\"" + IsExtra + "\",\"Via\":\"" + viaP + " " + "\"" +

                                           ",\"CompanyId\":\"" + objBooking.CompanyId.ToInt() + "\",\"SubCompanyId\":\"" + objBooking.SubcompanyId.ToInt() + "\",\"QuotedPrice\":\"" + (objBooking.IsQuotedPrice.ToBool() ? "1" : "0") + "\"" +
                   
                                             
                                             parkingandWaiting + ",\"DriverFares\":\"" + String.Format("{0:0.00}", (objBooking.FareRate - objBooking.ServiceCharges.ToDecimal())) + "\"" +
                                               agentDetails +


                                             ",\"Did\":\"" + ObjDriver.Id + "\",\"BabySeats\":\"" + objBooking.BabySeats.ToStr() + "\"" + showFares + showSummary + " }";
                                  


                                        //msg=  FOJJob + startJobPrefix + objBooking.Id +
                                        //   ":Pickup:" + (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +

                                        //   ":Destination:" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) +
                                        //     ":PickupDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) +
                                        //          ":Cust:" + objBooking.CustomerName + ":Mob:" + mobileNo + " " + ":Fare:" + pdafares
                                        //         + ":Vehicle:" + objBooking.Fleet_VehicleType.VehicleType + ":Account:" + companyName + " " +
                                        //         ":Lug:" + objBooking.NoofLuggages.ToInt() + ":Passengers:" + objBooking.NoofPassengers.ToInt() + ":Journey:" + journey +
                                        //         ":Payment:" + paymentType + ":Special:" + specialRequirements + " "
                                        //         + ":Extra:" + IsExtra + ":Via:" + viaP + " " + ":Did:" + ObjDriver.Id;


                                    }
                                    else
                                    {

                                         msg = startJobPrefix + objBooking.Id +
                                        ":Pickup:" + (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +

                                        ":Destination:" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) +
                                          ":PickupDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) +
                                               ":Cust:" + objBooking.CustomerName + ":Mob:" + mobileNo + " " + ":Fare:" + pdafares
                                              + ":Vehicle:" + objBooking.Fleet_VehicleType.VehicleType + ":Account:" + companyName + " " +
                                              ":Lug:" + objBooking.NoofLuggages.ToInt() + ":Passengers:" + objBooking.NoofPassengers.ToInt() + ":Journey:" + journey +
                                              ":Payment:" + paymentType + ":Special:" + specialRequirements + " "
                                              + ":Extra:" + IsExtra + ":Via:" + viaP + " " + ":Did:" + ObjDriver.Id;


                                        if (AppVars.objPolicyConfiguration.EnableBabySeats.ToBool())
                                        {
                                            msg += ":BabySeats:" + objBooking.BabySeats.ToStr() + " ";
                                        }

                                    }

                                    



                                //if (objBooking.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.SHUTTLE && objBooking.GroupJobId!=null )
                                    

                               
                                //{


                                //    msg = msg.Replace(startJobPrefix, "PreGroupJobId:");
                                //    msg += ":NoOfChilds:" + objBooking.NoOfChilds.ToInt() + ":DepartureDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.FlightDepartureDate)
                                //           + ":GroupNo:" + objBooking.GroupJobId;



                                //    StringBuilder groupMsgs = new StringBuilder();
                                //    foreach (var groupBooking in General.GetQueryable<Booking>(c=>c.GroupJobId==objBooking.GroupJobId && c.Id!=objBooking.Id && 
                                //                    (c.BookingStatusId==Enums.BOOKINGSTATUS.WAITING || c.BookingStatusId==Enums.BOOKINGSTATUS.NOTACCEPTED ||
                                //                     c.BookingStatusId==Enums.BOOKINGSTATUS.ONHOLD || c.BookingStatusId==Enums.BOOKINGSTATUS.PENDING)
                                //                    || c.BookingStatusId==Enums.BOOKINGSTATUS.REJECTED))
                                //    {
                                //         groupMsgs.Append(

                                //               "<<<PreGroupJobId:" + groupBooking.Id +
                                //              ":Pickup:" + (!string.IsNullOrEmpty(groupBooking.FromDoorNo) ? groupBooking.FromDoorNo + "-" + groupBooking.FromAddress + pickUpPlot : groupBooking.FromAddress + pickUpPlot) +

                                //               ":Destination:" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? groupBooking.ToDoorNo + "-" + groupBooking.ToAddress + dropOffPlot : groupBooking.ToAddress + dropOffPlot) +
                                //               ":PickupDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", groupBooking.PickupDateTime) +
                                //               ":Cust:" + groupBooking.CustomerName + ":Mob:" + mobileNo + " " + ":Fare:" + groupBooking.FareRate
                                //                 + ":Vehicle:" + groupBooking.Fleet_VehicleType.VehicleType + ":Account:" + companyName + " " +
                                //               ":Lug:" + groupBooking.NoofLuggages.ToInt() + ":Passengers:" + groupBooking.NoofPassengers.ToInt() + ":Journey:" + journey +
                                //              ":Payment:" + groupBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr() + ":Special:" + groupBooking.SpecialRequirements.ToStr() + " "
                                //             + ":Extra:" + IsExtra + ":Via:" + viaP + " " + ":Did:" + ObjDriver.Id    + ":NoOfChilds:" + objBooking.NoOfChilds.ToInt() + ":DepartureDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.FlightDepartureDate)
                                //           + ":GroupNo:" + objBooking.GroupJobId


                                //          );
                                         
                                //    }


                                //    msg = msg + groupMsgs.ToString();


                                //}

                               


                                if (msg.Contains("\r\n"))
                                {
                                    msg = msg.Replace("\r\n", " ").Trim();
                                }
                                else
                                {
                                    if (msg.Contains("\n"))
                                    {
                                        msg = msg.Replace("\n", " ").Trim();

                                    }

                                }

                                if (msg.Contains("&"))
                                {
                                    msg = msg.Replace("&", "And");
                                }

                                if (msg.Contains(">"))
                                    msg = msg.Replace(">", " ");


                                if (msg.Contains("="))
                                    msg = msg.Replace("=", " ");

                                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                                {
                                   // For TCP Connection
                                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                                    {
                                         new Thread(delegate()
                                        {
                                            General.SendMessageToPDA("request pda=" + JobId + "=" + ObjDriver.Id + "=" + msg + "=1=" + ObjDriver.DriverNo);                      
                                        }).Start();                                      


                                    }
                                   
                                }
                                else
                                {
                                    // For TCP Connection
                                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                                    {
                                       // SuccessRecv = General.SendPDAMessage("request pda=" + JobId + "=" + ObjDriver.Id + "=" + msg + "=1=" + ObjDriver.DriverNo);

                                        new Thread(delegate()
                                        {
                                            General.SendMessageToPDA("request pda=" + JobId + "=" + ObjDriver.Id + "=" + msg + "=1=" + ObjDriver.DriverNo);
                                        }).Start();        

                                    }
                                   
                                }



                                int drvId = objBooking.DriverId.ToInt();
                                int newDriverid = ObjDriver.Id;
                                if ((objBooking.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING_START || objBooking.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING)
                                     && objBooking.DriverId != null && objBooking.DriverId.ToInt() != newDriverid)
                                {

                                    new Thread(delegate()
                                    {
                                        General.SendMessageToPDA("request pda=" + drvId + "=" + JobId + "=Cancelled Pre Job>>" + JobId + "=2");
                                    }).Start();


                                }              




                    //     ClosePort(objSMS.port);

                }
            }
            catch (Exception ex)
            {
               
                rtn = false;
            }


            return rtn;

        }



    




    


       
        public bool SuccessDespatched = false;



       
        public void DespatchJob()
        {

            List<string> listofErrors = new List<string>();

            bool IsDespatched = OnDespatching(ref listofErrors);

            if (IsDespatched)
            {
            
                try
                {

                  //  if (this.JobStatusId == null)
                     //   this.JobStatusId = Enums.BOOKINGSTATUS.PENDING;



                    (new TaxiDataContext()).stp_DespatchedJobWithLog(this.JobId, ObjDriver.Id, ObjDriver.DriverNo.ToStr(), ObjDriver.HasPDA.ToBool(), enablePDA, false, false, AppVars.LoginObj.LoginName.ToStr(), Enums.BOOKINGSTATUS.PENDING, AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool());

                  //  (new TaxiDataContext()).stp_DespatchedJob(this.JobId, ObjDriver.Id, ObjDriver.HasPDA.ToBool(), enablePDA, false,false, AppVars.LoginObj.LoginName.ToStr(), Enums.BOOKINGSTATUS.PENDING);

             

                    bool autoDespatch = objBooking.AutoDespatch.ToBool();
                    if ((!this.IsAutoDespatchActivity || !autoDespatch))
                    {

                        if (enablePDA == false || ObjDriver.HasPDA==false)
                        {
                            int? driverId = ObjDriver.Id.ToIntorNull();

                            Fleet_DriverQueueList driverCurrent = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == driverId)
                                                                               .OrderByDescending(c => c.Id).FirstOrDefault();

                            if (driverCurrent != null)
                            {
                                if ((enablePDA == false && AppVars.objPolicyConfiguration.EnableOnBoardDrivers.ToBool() == true) || (ObjDriver.HasPDA.ToBool() == false && enablePDA))
                                {
                                    General.OnDespatchUpdateDriverQueue(driverCurrent.Id, objBooking.Id, General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim()));

                                    RefreshBookingList();
                                 

                                }
                                else if (enablePDA == false)
                                {
                                    General.UpdateDriverQueue(driverCurrent.Id, objBooking.Id, General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim()));
                                    RefreshBookingList();
                                }
                            }
                        }

                        SuccessDespatched = true;



                        if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                        {

                            RadDesktopAlert alert = new RadDesktopAlert();
                            alert.AutoCloseDelay = 5;
                            alert.FadeAnimationType = FadeAnimationType.None;
                            alert.FadeAnimationSpeed = 1;
                            //  desktopAlert.FadeAnimationType = FadeAnimationType.None;
                            //     desktopAlert.Popup.Opacity = 10;
                            alert.Popup.AlertElement.Opacity = 100;


                            alert.CaptionText = "Job No : " + objBooking.BookingNo.ToStr() + " Dispatch Successfully";
                            alert.ContentText = "Driver : " + ObjDriver.DriverName;

                            alert.ContentText += Environment.NewLine + "Pickup Date-Time : "
                                                                    + string.Format("{0:dd/MM/yyyy hh:mm tt}", objBooking.PickupDateTime);

                            alert.Show();
                        }
                        
                    }


                    new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_TODAY_AND_PREBOOKING_DASHBOARD);                   
                  ///  }

                    this.Close();
                }
                catch (Exception ex)
                {
                   
                    ENUtils.ShowMessage(ex.Message);
               
                }
            }
           


        }


        private void RefreshBookingList()
        {

            if (Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBookingsList") > 0)
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingsList") as frmBookingsList).SetRefreshWhenActive("");
            }

           // General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");


        }
     





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

                var list=  this.ObjDriver.Fleet_Driver_Shifts.ToList();             


                grdShifts.RowCount = list.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    grdShifts.Rows[i].Cells[COL_SHIFT.SHIFT].Value = list[i].Driver_Shift.DefaultIfEmpty().ShiftName.ToStr().Trim();
                    grdShifts.Rows[i].Cells[COL_SHIFT.FROMTIME].Value = string.Format("{0:HH:mm}", list[i].FromTime.ToDateTimeorNull());
                    grdShifts.Rows[i].Cells[COL_SHIFT.TOTIME].Value =string.Format("{0:HH:mm}",list[i].ToTime.ToDateTimeorNull());

                }

                TimeSpan jobPickupTime=this.objBooking.PickupDateTime.Value.TimeOfDay;


                // New :- Check to Restrict Controller to Despatch Job for UnAvailble Driver on Shift
               
                    if (ObjDriver.Fleet_Driver_Shifts.Count > 0
                        && ObjDriver.Fleet_Driver_Shifts.Count(c => c.Driver_Shift_ID != null && c.Driver_Shift_ID != 7) > 0)
                    {
                        string msg = string.Empty;
                        bool IsExist = false;
                        foreach (var item in ObjDriver.Fleet_Driver_Shifts)
                        {
                            if (item.Driver_Shift_ID != null && item.FromTime != null && item.ToTime != null)
                            {

                                if (item.Driver_Shift_ID.ToInt() == 7)
                                    IsExist = true;

                                if (item.Driver_Shift_ID.ToInt() != 7 && IsExist == false)
                                {
                                    string str = jobPickupTime.ToStr();

                                    str = str.Substring(0, str.LastIndexOf(':'));
                                    str = str.Replace(":", "").Trim();

                                    int time = str.ToInt();


                                    str = item.FromTime.Value.TimeOfDay.ToStr();
                                    str = str.Substring(0, str.LastIndexOf(':'));
                                    str = str.Replace(":", "").Trim();
                                    int fromTime = str.ToInt();


                                    str = item.ToTime.Value.TimeOfDay.ToStr();
                                    str = str.Substring(0, str.LastIndexOf(':'));
                                    str = str.Replace(":", "").Trim();
                                    int toTime = str.ToInt();

                                    if (time < 1000)
                                    {

                                        // PEAK FARES

                                        if (fromTime < 1000 && toTime < 1000)
                                        {
                                            if (time >= fromTime && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }
                                        // 6 AM (600) TO 15 PM (1500)
                                        else if (fromTime < 1000 && toTime > 1000)
                                        {
                                            if (time >= fromTime && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                        // 6 PM (1800) TO 6 AM (600)
                                        else if (fromTime > 1000 && toTime < 1000)
                                        {

                                            if (time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                        // OFF PEAK FARES

                                        if (fromTime < 1000 && toTime < 1000)
                                        {
                                            if (time >= fromTime
                                                    && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }
                                        // 6 AM (600) TO 15 PM (1500)
                                        else if (fromTime < 1000 && toTime > 1000)
                                        {
                                            if (time >= fromTime
                                                    && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                        // 6 PM (1800) TO 6 AM (600)
                                        else if (fromTime > 1000 && toTime < 1000)
                                        {

                                            if (time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                    }

                                    else if (time >= 1000)
                                    {
                                        if ((fromTime < 1000 && toTime >= 1000)
                                                || (fromTime >= 1000 && toTime >= 1000))
                                        {

                                            // 6 AM (600) TO 6PM (1700)
                                            if (time >= fromTime && time <= toTime)
                                            {
                                                IsExist = true;
                                            }

                                            else if ((fromTime >= 1000 && toTime < 1000))
                                            {

                                                if (time >= fromTime)
                                                {
                                                    IsExist = true;
                                                }
                                            }
                                            else if ((toTime > fromTime && time < (toTime - fromTime))
                                                || (fromTime > toTime && time > (fromTime - toTime)))
                                            {
                                                IsExist = true;

                                            }

                                        }

                                        else if ((fromTime < 1000 && toTime >= 1000)
                                                || (fromTime >= 1000 && toTime >= 1000))
                                        {

                                            // 6 AM (600) TO 6PM (1700)
                                            if (time >= fromTime
                                                    && time <= toTime)
                                            {
                                                IsExist = true;
                                            }

                                        }

                                        else if ((fromTime >= 1000 && toTime < 1000))
                                        {

                                            // 6 AM (600) TO 6PM (1700)
                                            if (time >= fromTime)
                                            {
                                                IsExist = true;
                                            }

                                        }

                                    }
                                }                  



                            }
                        }


                        if (IsExist == false)
                        {
                            lblWarning.Visible = true;  
                          
                        }
                    }

                

                //if (list.Count(c => c.FromTime.Value.TimeOfDay < jobPickupTime && c.ToTime.Value.TimeOfDay > jobPickupTime) > 0
                //    || list.Count(c => c.FromTime.Value.TimeOfDay == TimeSpan.Zero && c.ToTime.Value.TimeOfDay == TimeSpan.Zero)>0)
                //    lblWarning.Visible = false;               
                //else
                //    lblWarning.Visible = true;            


                    lblNocMessage.Visible = false;
                    lblNocMessage.Text = string.Empty;
                    DateTime nowDate=DateTime.Now.ToDate();
                    
                    //

                    DateTime? pickupDateTime = objBooking.PickupDateTime;

                    if (driver.MOTExpiryDate != null && (driver.MOTExpiryDate.ToDate() < nowDate  || ObjDriver.MOTExpiryDate.ToDate()< pickupDateTime.ToDate()))
                    {
                        lblNocMessage.Visible = true;
                       // lblNocMessage.Text += "Driver MOT Expired : " + string.Format("{0:dd/MM/yyyy}", driver.MOTExpiryDate) + Environment.NewLine;

                        if (ObjDriver.MOTExpiryDate.ToDate() < nowDate)
                        {

                            lblNocMessage.Text += "Driver MOT Expired :       " + string.Format("{0:dd/MM/yyyy}", ObjDriver.MOTExpiryDate) + Environment.NewLine;
                        }
                        else
                        {
                            lblNocMessage.Text += "Driver MOT is Expiring at :       " + string.Format("{0:dd/MM/yyyy}", ObjDriver.MOTExpiryDate) + Environment.NewLine;


                        }
                        
                        
                        lblNocMessage.ForeColor = Color.Red;


                    }
                     if (driver.MOT2ExpiryDate != null && driver.MOT2ExpiryDate.ToDate() < nowDate)
                    {
                        lblNocMessage.Visible = true;
                        lblNocMessage.Text += "Driver MOT 2 Expired : " + string.Format("{0:dd/MM/yyyy}", driver.MOT2ExpiryDate) + Environment.NewLine;
                        lblNocMessage.ForeColor = Color.Red;

                    }
                     if (driver.InsuranceExpiryDate != null && (driver.InsuranceExpiryDate.ToDateTime() < DateTime.Now || ObjDriver.InsuranceExpiryDate < pickupDateTime))
                    {
                        lblNocMessage.Visible = true;
                        //lblNocMessage.Text += "Insurance Expired : " + string.Format("{0:dd/MM/yyyy HH:mm}", driver.InsuranceExpiryDate) + Environment.NewLine;

                        if (ObjDriver.InsuranceExpiryDate < nowDate)
                            lblNocMessage.Text += "Insurance Expired :          " + string.Format("{0:dd/MM/yyyy HH:mm}", ObjDriver.InsuranceExpiryDate) + Environment.NewLine;
                        else
                            lblNocMessage.Text += "Insurance is Expiring at :          " + string.Format("{0:dd/MM/yyyy HH:mm}", ObjDriver.InsuranceExpiryDate) + Environment.NewLine;

                         
                         lblNocMessage.ForeColor = Color.Red;

                    }

                     if (driver.PCODriverExpiryDate != null && (driver.PCODriverExpiryDate.ToDate() < nowDate || ObjDriver.PCODriverExpiryDate.ToDate()< pickupDateTime.ToDate()))
                    {
                        lblNocMessage.Visible = true;
                       // lblNocMessage.Text += "PCO Driver Expired : " + string.Format("{0:dd/MM/yyyy}", driver.PCODriverExpiryDate) + Environment.NewLine;

                        if (ObjDriver.PCODriverExpiryDate.ToDate() < nowDate)
                            lblNocMessage.Text += "PCO Driver Expired :       " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCODriverExpiryDate) + Environment.NewLine;
                        else
                            lblNocMessage.Text += "PCO Driver is Expiring at :        " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCODriverExpiryDate) + Environment.NewLine;

                         
                         lblNocMessage.ForeColor = Color.Red;

                    }

                     if (driver.PCOVehicleExpiryDate != null && (driver.PCOVehicleExpiryDate.ToDate() < nowDate || ObjDriver.PCOVehicleExpiryDate.ToDate() < pickupDateTime.ToDate()))
                    {
                        lblNocMessage.Visible = true;
                       // lblNocMessage.Text += "PCO Vehicle Expired : " + string.Format("{0:dd/MM/yyyy}", driver.PCOVehicleExpiryDate) + Environment.NewLine;

                        if (ObjDriver.PCOVehicleExpiryDate.ToDate() < nowDate)
                            lblNocMessage.Text += "PCO Vehicle Expired :     " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCOVehicleExpiryDate) + Environment.NewLine;
                        else
                            lblNocMessage.Text += "PCO Vehicle is Expiring at :      " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCOVehicleExpiryDate) + Environment.NewLine;

                         lblNocMessage.ForeColor = Color.Red;
                    }

                     if (driver.DrivingLicenseExpiryDate != null && (driver.DrivingLicenseExpiryDate.ToDate() < nowDate  || ObjDriver.DrivingLicenseExpiryDate.ToDate() < pickupDateTime.ToDate()))
                    {
                        lblNocMessage.Visible = true;
                      //  lblNocMessage.Text += "Driving License Expired : " + string.Format("{0:dd/MM/yyyy}", driver.DrivingLicenseExpiryDate) + Environment.NewLine;

                        if (ObjDriver.DrivingLicenseExpiryDate.ToDate() < nowDate)
                            lblNocMessage.Text += "Driving License Expired :  " + string.Format("{0:dd/MM/yyyy}", ObjDriver.DrivingLicenseExpiryDate) + Environment.NewLine;
                        else
                            lblNocMessage.Text += "Driving License is Expiring at :  " + string.Format("{0:dd/MM/yyyy}", ObjDriver.DrivingLicenseExpiryDate) + Environment.NewLine;

                         
                         lblNocMessage.ForeColor = Color.Red;
                    }
                     if (driver.RoadTaxiExpiryDate != null && (driver.RoadTaxiExpiryDate.ToDate() < nowDate  || ObjDriver.RoadTaxiExpiryDate.ToDate() < pickupDateTime.ToDate()))
                    {
                        lblNocMessage.Visible = true;
                       // lblNocMessage.Text += "Road Tax Expired : " + string.Format("{0:dd/MM/yyyy}", driver.RoadTaxiExpiryDate) + Environment.NewLine;

                        if (ObjDriver.RoadTaxiExpiryDate.ToDate() < nowDate)
                            lblNocMessage.Text += "Road Tax Expired :            " + string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate);
                        else
                            lblNocMessage.Text += "Road Tax is Expiring at :            " + string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate);

                         
                         lblNocMessage.ForeColor = Color.Red;
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


        public struct COL_SHIFT
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string SHIFT = "SHIFT";

            public static string SHIFT_ID = "SHIFT_ID";

            public static string FROMTIME = "FROMTIME";

            public static string TOTIME = "TOTIME";


        }



        private void FormatShiftGrid()
        {
            grdShifts.AllowAutoSizeColumns = true;
            grdShifts.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdShifts.AllowAddNewRow = false;
            grdShifts.ShowGroupPanel = false;
        //    grdShifts.AutoCellFormatting = true;
            grdShifts.ShowRowHeaderColumn = false;

            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);




            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = "Shifts";
            col.IsVisible = true;
            col.Name = COL_SHIFT.SHIFT;
            col.Width = 150;
            grdShifts.Columns.Add(col);

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "From Time";
            colDate.Name = COL_SHIFT.FROMTIME;
            colDate.Format = DateTimePickerFormat.Custom;
            //colDate.CustomFormat = "dd/MM/yyyy";
            //colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            colDate.Width = 120;
            col.ReadOnly = false;
            grdShifts.Columns.Add(colDate);

            colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "To Time";
            colDate.Name = COL_SHIFT.TOTIME;
            colDate.Format = DateTimePickerFormat.Custom;
            //colDate.CustomFormat = "dd/MM/yyyy";
            //colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            colDate.Width = 120;
            col.ReadOnly = false;
            grdShifts.Columns.Add(colDate);




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
