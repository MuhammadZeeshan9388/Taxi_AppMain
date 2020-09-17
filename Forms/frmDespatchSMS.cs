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
    public partial class frmDespatchSMS : Form
    {
        
       
        private long _JobId;

        public long JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
        }
        public frmDespatchSMS(Booking booking)
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


        private void FormatGrid()
        {
            try
            {

                grdNearestDrv.Visible = true;


                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.HeaderText = "Id";
                col.Name = "Id";
                col.IsVisible = false;
                grdNearestDrv.Columns.Add(col);



                col = new GridViewTextBoxColumn();
                col.HeaderText = "Driver No";
                col.Name = "Driver";
                col.Width = 380;
                grdNearestDrv.Columns.Add(col);


                grdNearestDrv.CellDoubleClick += new GridViewCellEventHandler(grdNearestDrv_CellDoubleClick);
                grdNearestDrv.RowFormatting += new RowFormattingEventHandler(grdNearestDrv_RowFormatting);
            }
            catch (Exception ex)
            {


            }
        }

        void grdNearestDrv_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            try
            {

                e.RowElement.RowInfo.Height = 30;
            }
            catch (Exception ex)
            {

            }
        }

        void grdNearestDrv_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row != null && e.Row is GridViewDataRowInfo)
            {

                ddl_Driver.SelectedValue = e.Row.Cells["Id"].Value.ToInt();

            }
           
        }


    


        private void LoadAndSelectDriver()
        {
            try
            {


                ComboFunctions.FillDriverNoCombo(ddl_Driver);
               // ComboFunctions.FillDriverNoQueueCombo(ddl_Driver);
            
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

                   
                   lblDespatchHeading.Text = "Dispatch SMS To Job Ref # : " + objBooking.BookingNo.ToStr();
                    

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
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    if(  db.Fleet_DriverQueueLists.Where(c=>c.Status==true && c.DriverId==driverId).Count()==0)
                    {
                        ENUtils.ShowMessage("Please Login this Driver.");
                        return;


                    }

                }
                  

            }


            if (ObjDriver.VehicleTypeId != null && objBooking.Fleet_VehicleType.VehicleType.ToLower().StartsWith("mpv")
                && objBooking.VehicleTypeId != ObjDriver.VehicleTypeId)
            {
                if (DialogResult.No == RadMessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                          "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine
                                                      + "Do you still want to Despatch this Job to that Driver " + ObjDriver.DriverNo + " ?", "Dispatch", MessageBoxButtons.YesNo))
                {
                    return;

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
                                                        + "Are you sure you want to Despatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
            else if (lblNocMessage.Text.Contains("MOT 2 Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver MOT 2 has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
            else if (lblNocMessage.Text.Contains("License Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver Driving License has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
            else if (lblNocMessage.Text.Contains("Insurance Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver Insurance has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }

            else if (lblNocMessage.Text.Contains("PCO Driver Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver PCO has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }


            else if (lblNocMessage.Text.Contains("PCO Vehicle Expired"))
            {
                if (DialogResult.No == RadMessageBox.Show("This Driver PCO Vehicle has been expired" + Environment.NewLine
                                                        + "Are you sure you want to Despatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                {
                    IsContinue = false;

                }
            }
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


        private System.Threading.Thread smsThread = null;

        public System.Threading.Thread SmsThread
        {
            get { return smsThread; }
            set { smsThread = value; }
        }


   
        bool enablePDA = false;
        public bool OnDespatching(ref List<string> listofErrors)
        {
            bool rtn = false;

            bool IsSuccess1 = true;
            bool IsSuccess2 = false;
            string smsError1 = string.Empty;
            string smsError2 = string.Empty;


         try
            {

                if (ObjDriver != null && objBooking != null && chkCompleteJob.Checked == false)
                {

                    string customerMobileNo = txtCustomerMobNo.Text.Trim();
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


                    EuroSMS objSMS = new EuroSMS();
                    enablePDA = AppVars.objPolicyConfiguration.EnablePDA.ToBool();

                    string custNo = !string.IsNullOrEmpty(objBooking.CustomerMobileNo) ? objBooking.CustomerMobileNo : objBooking.CustomerPhoneNo;


                    // Send To Driver

                    IsSuccess2 = true;

                        objSMS.ToNumber = txtDriverMobNo.Text.Trim();
                        objSMS.Message = GetMessage(AppVars.objPolicyConfiguration.DespatchTextForDriver.ToStr());
                        objSMS.BookingSMSAccountType = objBooking.SMSType.ToInt();
                        if (objSMS.Message.ToStr().Length > 0)
                        {

                            if (chkReturnDetails.Checked)
                            {
                                objSMS.Message += GetReturnMessage(AppVars.objPolicyConfiguration.DespatchTextForDriver.ToStr());
                            }

                            new Thread(delegate()
                            {
                                objSMS.Send(ref smsError2);
                            }).Start();

                        }
                        // IsSuccess2 = objSMS.Send(ref smsError2);

                   
                 




                    // Send To Customer
                    //if (AppVars.objPolicyConfiguration.EnablePassengerText.ToBool() && objBooking.DisablePassengerSMS.ToBool() == false)
                    //{

                    //    if (!string.IsNullOrEmpty(customerMobileNo))
                    //    {

                    //        smsThread = new Thread(delegate()
                    //            {
                    //                IsSuccess1 = SendDespatchSMS(objSMS, GetMessage(AppVars.objPolicyConfiguration.DespatchTextForCustomer.ToStr()), customerMobileNo);
                    //            });

                    //    }
                    //}


                    if (IsSuccess1 && IsSuccess2)
                    {
                        rtn = true;
                    }

                    //if (IsSuccess1 == false)
                    //{
                    //    listofErrors.Add("[Customer] : " + smsError1);

                    //}

                    if (IsSuccess2 == false)
                    {
                        listofErrors.Add("[Driver] : " + smsError2);
                    }




                }
                else
                    rtn = true;
            }
            catch (Exception ex)
            {
                IsSuccess1 = false;

                listofErrors.Add(ex.Message);
                rtn = false;
            }


            return rtn;

        }



    




        private string GetMessage(string message)
        {
            try
            {


                string msg = message;

                object propertyValue = string.Empty;
                foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                {

                    


                    switch (tag.TagObjectName)
                    {
                        case "booking":

                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = objBooking.GetType().GetProperty(val[0]).GetValue(objBooking, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(tag.ConditionNotNull) && objBooking.GetType().GetProperty(tag.ConditionNotNull) != null)
                                {

                                    if (tag.ConditionNotNull.ToStr() == "BabySeats" && tag.TagPropertyValue.ToStr() == "BabySeats")
                                    {
                                        propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);

                                        if (!string.IsNullOrEmpty(propertyValue.ToStr().Trim()) && propertyValue.ToStr().Contains("<<<"))
                                        {
                                            string[] arr = propertyValue.ToStr().Split(new string[] { "<<<" }, StringSplitOptions.None);

                                            propertyValue = "B Seat 1 : " + arr[0].ToStr() + Environment.NewLine + "B Seat 2 : " + arr[1].ToStr();

                                        }

                                    }
                                    else if (objBooking.GetType().GetProperty(tag.ConditionNotNull).GetValue(objBooking, null) != null)
                                    {
                                        propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                    }


                                    //else
                                    //{
                                    //    propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                    //}


                                }
                                else
                                {

                                    propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                }
                            }


                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);
                            }
                            break;



                        case "Booking_ViaLocations":
                            if (tag.TagPropertyValue == "ViaLocValue")
                            {


                                string[] VilLocs = null;
                                int cnt = 1;
                                VilLocs = objBooking.Booking_ViaLocations.Select(c => cnt++.ToStr() + ". " + c.ViaLocValue).ToArray();
                                if (VilLocs.Count() > 0)
                                {

                                    string Locations = "VIA POINT(s) : \n" + string.Join("\n", VilLocs);
                                    propertyValue = Locations;
                                }
                                else
                                    propertyValue = string.Empty;

                            }
                            break;



                        case "driver":


                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = ObjDriver.GetType().GetProperty(val[0]).GetValue(ObjDriver, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }

                            else
                            {
                                propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue).GetValue(ObjDriver, null);
                            }

                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue2).GetValue(ObjDriver, null);
                            }
                            break;


                        default:
                            propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                            break;

                    }




                    msg = msg.Replace(tag.TagMemberValue,
                        tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                }


                return msg.Replace("\n\n", "\n");
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);
                return "";
            }
        }



        private string GetReturnMessage(string message)
        {
            try
            {
                if (objBooking.BookingReturns.Count == 0)
                    return "";


                Booking objReturn = objBooking.BookingReturns[0];

                string msg = message;

                msg += Environment.NewLine + "Return Details : " + Environment.NewLine;


                object propertyValue = string.Empty;
                foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                {




                    switch (tag.TagObjectName)
                    {
                        case "booking":

                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = objReturn.GetType().GetProperty(val[0]).GetValue(objReturn, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }
                            else
                            {
                                if (tag.ConditionNotNull.ToStr() == "BabySeats" && tag.TagPropertyValue.ToStr() == "BabySeats")
                                {
                                    propertyValue = objReturn.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objReturn, null);

                                    if (!string.IsNullOrEmpty(propertyValue.ToStr().Trim()) && propertyValue.ToStr().Contains("<<<"))
                                    {
                                        string[] arr = propertyValue.ToStr().Split(new string[] { "<<<" }, StringSplitOptions.None);

                                        propertyValue = "B Seat 1 : " + arr[0].ToStr() + Environment.NewLine + "B Seat 2 : " + arr[1].ToStr();

                                    }

                                }
                                else
                                {
                                    propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                }
                                //if (!string.IsNullOrEmpty(tag.ConditionNotNull) && objBooking.GetType().GetProperty(tag.ConditionNotNull) != null)
                                //{

                                //    propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                //}
                                //else
                                //{

                                //    propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                //}
                            }


                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = objReturn.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objReturn, null);
                            }
                            break;


                        case "driver":


                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = ObjDriver.GetType().GetProperty(val[0]).GetValue(ObjDriver, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }

                            else
                            {
                                propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue).GetValue(ObjDriver, null);
                            }

                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue2).GetValue(ObjDriver, null);
                            }
                            break;


                        default:
                            propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                            break;

                    }




                    msg = msg.Replace(tag.TagMemberValue,
                        tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                }


                return msg.Replace("\n\n", "\n");
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);
                return "";
            }
        }


        //private bool SendDespatchSMS(EuroSMS objSMS, string msg, string mobileNo)
        //{

        //    bool rtn = true;
        //    try
        //    {
            

        //        string smsError1 = "";

        //        objSMS.ToNumber = mobileNo;
        //        objSMS.Message = msg;

        //        System.Threading.Thread.Sleep(7000);
        //        rtn = objSMS.Send(ref smsError1);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ENUtils.ShowMessage(ex.Message);


        //    }

        //    return rtn;

        //}

        private void ClosePort(SerialPort port)
        {
            if (port != null)
                port.Close();


        }

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

                    if (chkCompleteJob.Checked == false)
                    {

                        bool autoDespatch = objBooking.AutoDespatch.ToBool();
                        if ((!this.IsAutoDespatchActivity || !autoDespatch))
                        {


                            

                                int? driverId = ObjDriver.Id.ToIntorNull();

                                Fleet_DriverQueueList driverCurrent = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == driverId)
                                                                                   .OrderByDescending(c => c.Id).FirstOrDefault();

                                if (driverCurrent != null)
                                {

                                    General.OnDespatchUpdateDriverQueue(driverCurrent.Id, objBooking.Id, General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim()));
                                    RefreshBookingList();

                                }                      



                                SuccessDespatched = true;


                           
                                RadDesktopAlert alert = new RadDesktopAlert();
                                alert.AutoCloseDelay = 5;
                                alert.FadeAnimationType = FadeAnimationType.None;

                                alert.CaptionText = "Job No : " + objBooking.BookingNo.ToStr() + " Despatch Successfully";
                                alert.ContentText = "Driver : " + ObjDriver.DriverName;

                                alert.ContentText += Environment.NewLine + "Pickup Date-Time : "
                                                                        + string.Format("{0:dd/MM/yyyy hh:mm tt}", objBooking.PickupDateTime);

                                alert.Show();

                            }
                        
                    }
                

                    if (!this.IsAutoDespatchActivity)
                    {
                        new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ONLY_DASHBOARD);                   
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                   
                    ENUtils.ShowMessage(ex.Message);
               
                }
            }
            else
            {

                lblNocMessage.Text = "Job Despatch Failed..";
                lblNocMessage.ForeColor = Color.Red;
                lblNocMessage.Visible = true;

                int cnt = listofErrors.Count;
                if (cnt == 1)
                {
                    lblSmsError1.Visible = true;
                    lblSmsError1.Text = listofErrors[0].ToStr();

                }

                if (cnt == 2)
                {
                    lblSmsError1.Visible = true;
                    lblSmsError1.Text = listofErrors[0].ToStr();

                    lblSMSError2.Visible = true;
                    lblSMSError2.Text = listofErrors[1].ToStr();

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

        private void chkCompleteJob_CheckedChanged(object sender, EventArgs e)
        {

            if (objBooking != null)
            {

                if (chkCompleteJob.Checked)
                {
                    lblDespatchHeading.Text = "Complete Job Ref # : " + objBooking.BookingNo.ToStr();

                }
                else
                {

                    lblDespatchHeading.Text = "Dispatch SMS To Job Ref # : " + objBooking.BookingNo.ToStr();

                }
            }
        }

       


    }
}
