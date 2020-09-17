using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using Utils;
using UI.UserControls;
using Telerik.WinControls.UI;
using System.Data.Linq;
using System.Xml.Linq;
using Taxi_BLL;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using DAL;
using Telerik.WinControls;
using System.Text.RegularExpressions;
using System.Net.Cache;
using System.Threading;
using UI;
using System.Collections;
using Telerik.WinControls.UI.Docking;
using System.Data.Linq.SqlClient;
using Taxi_AppMain.Classes;
using System.Diagnostics;

namespace Taxi_AppMain
{
    public partial class frmCancelReason : UI.SetupBase
    {
        BookingBO objMaster = new BookingBO();

        private long _onlineBookingId;

        private long _BookingId;
        private string _CancelReason;

        public long BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }
        public string CancelReason
        {
            get { return _CancelReason; }
            set { _CancelReason = value; }
        }


        private int BookingTypeId;
        private string MobileNo;
        private string RefNumber;

        public bool IsRefresh;

        public frmCancelReason(long id, string cancelreason)
        {
            InitializeComponent();
            this.BookingId = id;
            this.CancelReason = cancelreason;
            DisplayCancelReason();
        }

        public frmCancelReason(long id,string refNo,int bookingType,string mobileNo)
        {
            InitializeComponent();
            this.BookingId = id;
           
            this.BookingTypeId = bookingType;
            this.MobileNo = mobileNo;
            this.RefNumber = refNo;
        }
        
        public void DisplayCancelReason()
        {
            objMaster.GetByPrimaryKey(BookingId);

            if (objMaster != null)
            {
                txtCancelReason.Text = objMaster.Current.CancelReason;


                this.BookingTypeId = objMaster.Current.BookingTypeId.ToInt();
                this.MobileNo = objMaster.Current.CustomerMobileNo.ToStr();
                this.RefNumber = objMaster.Current.BookingNo.ToStr();
                this._onlineBookingId = objMaster.Current.OnlineBookingId.ToLong();
            }         
           
        }

        private void btnSaveCancelReason_Click(object sender, EventArgs e)
        {

            SaveBooking();

        }


        public bool IsSaved = false;

        private void SaveBooking()
        {

            string reason = txtCancelReason.Text.Trim();
            if (string.IsNullOrEmpty(reason))
            {
                reason = "XXX";
                //ENUtils.ShowMessage("Please enter Cancel Reason...");
                //return;

            }




            if (objMaster == null || objMaster.Current == null)
                objMaster.GetByPrimaryKey(BookingId);



            if (objMaster.Current == null)
            {
                Close();
                return;

            }

            bool cancelReturnJob = false;
            long? returnBookingId = null;

            if (objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
            {


                DialogResult dialog = MessageBox.Show("Do you want to cancel Return Booking as well ?", "", MessageBoxButtons.YesNoCancel);
                if (dialog == System.Windows.Forms.DialogResult.Yes)
                {
                    cancelReturnJob = true;

                }
                else if (dialog == System.Windows.Forms.DialogResult.Cancel)
                {

                    return;
                }

            }


            this._onlineBookingId = objMaster.Current.OnlineBookingId.ToLong();


            using (TaxiDataContext db = new TaxiDataContext())
            {
                //db.stp_CancelBooking(BookingId, reason,AppVars.LoginObj.UserName.ToStr());
                db.stp_CancelBookingWithUserLog(BookingId, reason, AppVars.LoginObj.UserName.ToStr(), AppVars.LoginObj.LuserId.ToInt());

                jobIds = objMaster.Current.Id.ToStr();
                if (cancelReturnJob && objMaster.Current.BookingReturns.Count > 0)
                {
                    returnBookingId = objMaster.Current.BookingReturns[0].Id;
                    db.stp_CancelBookingWithUserLog(returnBookingId, reason, AppVars.LoginObj.UserName.ToStr(), AppVars.LoginObj.LuserId.ToInt());
                    jobIds +=","+ returnBookingId.ToStr();
                }

               

                //db.stp_AddUserLogs(AppVars.LoginObj.LuserId.ToInt(),"JOB CANCELLED",3);
                // db.stp_BookingLog(BookingId, AppVars.LoginObj.UserName.ToStr(), "Job is Cancelled ! Reason : " + reason);
            }

            IsSaved = true;



            try
            {
                int driverId = objMaster.Current.DriverId.ToInt();



                if (objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.WAITING && driverId > 0)
                {

                    if (objMaster.Current.JobCode.ToStr().Length > 0 && AppVars.objPolicyConfiguration.EnableGhostJob.ToBool())
                    {

                        //  new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ACTIVE_DASHBOARD);
                        General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_ACTIVE_DASHBOARD);
                    }
                    else
                    {

                        if (General.GetQueryable<Fleet_DriverQueueList>(c => c.DriverId == driverId && c.CurrentJobId == BookingId).Count() > 0)
                        {
                            new Thread(delegate()
                            {
                                CancelCurrentBookingFromPDA(BookingId, driverId);
                            }).Start();

                            //   RefreshBookingListOnActive();

                            //  new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ACTIVE_DASHBOARD);
                            General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_ACTIVE_DASHBOARD);
                        }
                        else
                        {
                            if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING_START)
                            {
                                new Thread(delegate()
                                {
                                    General.ReCallPreBooking(BookingId, driverId);

                                }).Start();
                            }
                            else
                            {

                                new Thread(delegate()
                                {
                                    ReCallFOJBookingFromPDA(BookingId, driverId);

                                }).Start();
                            }



                            //  new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ACTIVE_DASHBOARD);
                            General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_ACTIVE_DASHBOARD);
                            //   RefreshActiveData();
                        }
                    }

                    this.IsRefresh = true;

                    Thread.Sleep(500);
                }


                if (objMaster.Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.THIRDPARTY)
                {
                    General.UpdateThirdPartyJobStatus(objMaster.Current, objMaster.Current.Id, "declined");
                }



                if (chkCancellationSMS.Checked && objMaster.Current != null && objMaster.Current.CustomerMobileNo.ToStr().Length >= 9 && AppVars.objPolicyConfiguration.SMSCancelJob.ToStr().Trim().Length > 0)
                {
                    string rtnMsg = string.Empty;
                    EuroSMS sms = new EuroSMS();
                    sms.Message = GetMessage(AppVars.objPolicyConfiguration.SMSCancelJob.ToStr(), objMaster.Current, objMaster.Current.Id);
                    sms.ToNumber = objMaster.Current.CustomerMobileNo.ToStr().Trim();
                    sms.Send(ref rtnMsg);
                }

            }
            catch
            {



            }

            SendCancelEmail();


            this.Close();

        }

        private string GetMessage(string message, Booking objBooking, long jobId)
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

                            if (objBooking == null)
                                objBooking = General.GetObject<Booking>(c => c.Id == jobId);

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

                                }
                                else
                                {

                                    if (tag.ExpressionValue.ToStr().Trim().Length > 0)
                                    {
                                        try
                                        {
                                            char[] splitArr = new char[] { ',' };
                                            char[] splitArr2 = new char[] { '|' };
                                            string[] val = tag.ExpressionValue.Split(splitArr);

                                            string replaceMessage = val[0].ToStr();
                                            int? expressionApplied = null;
                                            foreach (var item in val.Where(c => c.EndsWith("|replacemessage") == false))
                                            {
                                                var str = item.Split(splitArr2);

                                                if (objBooking.GetType().GetProperty(str[0]) != null)
                                                {
                                                    if (objBooking.GetType().GetProperty(str[0]).GetValue(objBooking, null).ToStr() == str[1])
                                                    {
                                                        if (expressionApplied == null)
                                                            expressionApplied = 1;
                                                    }
                                                    else
                                                        expressionApplied = null;

                                                }
                                            }

                                            if (expressionApplied != null && expressionApplied == 1)
                                            {
                                                var replacearr = replaceMessage.Split(splitArr2);

                                                msg = msg.Replace(replacearr[0], replacearr[1]);
                                            }
                                            else
                                            {
                                                propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                            }
                                        }
                                        catch
                                        {
                                            propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);

                                        }

                                    }
                                    else
                                    {

                                        propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                    }



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

                            if (objBooking.DriverId != null)
                            {
                                if (tag.TagPropertyValue.Contains('.'))
                                {

                                    string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                    object parentObj = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(val[0]).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);

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
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                                }

                                if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                                }
                            }
                            break;


                        case "Fleet_Driver_Image":




                            if (!string.IsNullOrEmpty(tag.TagPropertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                if (objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Images.Count > 0)
                                {
                                    string linkId = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Images[0].PhotoLinkId.ToStr();

                                    if (linkId.ToStr().Length == 0)
                                        propertyValue = " ";
                                    else
                                    {
                                        
                                        if (tag.TagMemberValue.ToStr().Trim().ToLower() == "<trackdrv>")
                                        {
                                            string encrypt = Cryptography.Encrypt(objBooking.BookingNo.ToStr() + ":" + linkId + ":" + AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim() + ":" + objBooking.Id, "softeuroconnskey", true);
                                            propertyValue = Program.objLic.CabTrackUrl.ToStr()+"/tck.aspx?q=" + encrypt;

                                        }
                                        else
                                        {

                                            propertyValue =Program.objLic.CabTrackUrl.ToStr() +"/drv.aspx?ref=" + objBooking.BookingNo.ToStr() + ":" + linkId;
                                        }
                                    }
                                }
                                else
                                    propertyValue = " ";


                                //      propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                            }
                            break;


                        case "Fleet_Driver_Documents":



                            if (!string.IsNullOrEmpty(tag.TagPropertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {

                                if (tag.TagPropertyValue.Contains("PHC Vehicle"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.PCOVehicle)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("PHC Driver"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.PCODriver)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("License"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.LICENSE)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("Insurance"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.Insurance)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();

                                }
                                else if (tag.TagPropertyValue.Contains("MOT"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.MOT)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();

                                }



                            }
                            break;



                        default:
                            if (objBooking.SubcompanyId == null)
                                propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                            else
                                propertyValue = objBooking.Gen_SubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking.Gen_SubCompany, null);

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


        private bool ReCallFOJBookingFromPDA(long jobId, int driverId)
        {

            bool rtn = true;

            try
            {
            //    (new TaxiDataContext()).stp_UpdateJobStatus(jobId, Enums.BOOKINGSTATUS.WAITING);




                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {
                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Foj Job>>" + jobId + "=2").Result.ToBool();
                    }

                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Foj Job>>" + jobId + "=2").Result.ToBool();
                    }


                }

            }
            catch
            {

                //  ENUtils.ShowMessage(ex.Message);


            }


            return rtn;

        }

        private  bool CancelCurrentBookingFromPDA(long jobId, int driverId)
        {

            try
            {

                bool rtn = true;

                (new TaxiDataContext()).stp_UpdateJob(jobId, driverId, Enums.BOOKINGSTATUS.CANCELLED, Enums.Driver_WORKINGSTATUS.AVAILABLE, AppVars.objPolicyConfiguration.SinBinTimer.ToInt());


                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }


                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }

                }


                if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_SaveOfflineMessage(jobId, driverId, "", AppVars.LoginObj.LoginName.ToStr(), "Cancelled Job>>" + jobId + "=2");
                    }

                }

                return rtn;
            }
            catch 
            {

                return false;
                //   ENUtils.ShowMessage(ex.Message);


            }

        }




        private void SendCancelEmail()
        {
            try
            {

                if (chkSendEmailtoCustomer.Checked && AppVars.listUserRights.Count(c => c.functionId == "EMAIL - BCL") > 0)
                {

                    new Thread(delegate()
                     {

                         JATEmail.SendCancelBookingEmail(General.GetObject<Booking>(c => c.Id == BookingId));
                     }).Start();

                }
                else
                {
                    if (chkSendEmailtoCustomer.Checked)
                    {
                        new Thread(delegate()
                        {

                            JATEmail.SendCustomerCancelationEmail(General.GetObject<Booking>(c => c.Id == BookingId));
                        }).Start();
                    }
                }
           

                if (AppVars.objPolicyConfiguration.EnableWebBooking.ToBool() && BookingTypeId==Enums.BOOKING_TYPES.ONLINE && this.RefNumber.ToStr().Length > 0)
                {
                    string refNo = string.Empty;
                    string mobile = this.MobileNo;

                    if (this._onlineBookingId == 0)
                    {

                        //  string mobileNo = objMaster.Current.CustomerMobileNo.ToStr().Trim();
                        string[] arrRef = this.RefNumber.ToStr().Trim().Split('/');

                        refNo = arrRef.Count() == 1 ? arrRef[0].ToStr() : arrRef[1].ToStr();

                       

                        string newRefNo = string.Empty;
                        for (int i = 0; i < refNo.Length; i++)
                        {
                            if (char.IsLetter(refNo[i]))
                                newRefNo += refNo[i];
                        }

                        if (!string.IsNullOrEmpty(newRefNo) && newRefNo.Length > 0)
                        {
                            refNo = refNo.Replace(newRefNo, "").Trim();
                        }
                    }
                    else
                    {

                        refNo = this._onlineBookingId.ToStr();

                    }                    

                    if (refNo.IsNumeric())
                    {

                        new Thread(delegate()
                        {

                            General.CancelWebBooking(mobile, refNo);

                        }).Start();
                    }


                }
            }
            catch 
            {


            }

      
            

          


        }


        private void btnExitCancelForm_Click(object sender, EventArgs e)
        {
            Close();
        }

     

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCancelReason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
            else if (e.KeyCode == Keys.Home)
            {
                SaveBooking();

            }
        }

        private void frmCancelReason_Shown(object sender, EventArgs e)
        {
            txtCancelReason.Select();
            this.FormClosing += new FormClosingEventHandler(frmCancelReason_FormClosing);
        }

        string jobIds = "";

        void frmCancelReason_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (this.IsRefresh == false && this.CancelReason.ToStr().Length==0)
            {
                //testing
                //new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);
          
                // live test
                 General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_CANCELBOOKING+">>>"+jobIds);

                // live
                // General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_DASHBOARD);
            }



        }

      


     

    }
}
