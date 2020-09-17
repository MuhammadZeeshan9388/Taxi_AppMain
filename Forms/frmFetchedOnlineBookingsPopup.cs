using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Utils;
using Telerik.WinControls;
using System.Reflection;
using System.Net;
using System.Threading;
using Taxi_AppMain.Classes;
using System.IO;

namespace Taxi_AppMain
{
    public partial class frmFetchedOnlineBookingsPopup :Form
    {

        private bool _IsAttachClosing = true;

        public bool IsAttachClosing
        {
            get { return _IsAttachClosing; }
            set { _IsAttachClosing = value; }
        }


        private List<Booking> _ListofFetechedJobs = null;

        public List<Booking> ListofFetechedJobs
        {
            get { return _ListofFetechedJobs; }
            set { _ListofFetechedJobs = value; }
        }


        private bool IsEmailSettingsDefined = false;

        public frmFetchedOnlineBookingsPopup(List<Booking> listofJobs)
        {
            InitializeComponent();

            this.ListofFetechedJobs = listofJobs;

            FormatGrid();

            this.Load += new EventHandler(frmFetchedOnlineBookings_Load);

            this.FormClosing += new FormClosingEventHandler(frmFetchedOnlineBookings_FormClosing);

            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
        }

        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridDataCellElement)
            {

                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {


                    if (e.Row.Cells["JOURNEYTYPEID"].Value.ToInt() == 2 && (e.Column.Name == "ACCEPT" || e.Column.Name == "DECLINE"))
                    {


                        //     ((RadButtonElement)e.CellElement.Children[0]).Text = "Re-Despatched";
                        ((RadButtonElement)e.CellElement.Children[0]).Enabled = false;

                    }
                    else
                        ((RadButtonElement)e.CellElement.Children[0]).Enabled = true;
                }
            }
        }

       

        void frmFetchedOnlineBookings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAttachClosing)
            {

                if (grdLister.Rows.Count > 0)
                 {
                    AppVars.IsLogout = false;
                    ENUtils.ShowMessage("You cannot close it until you Authorized All Bookings");

                    e.Cancel = true;
                }
                else
                {
                    RefreshDashBoardBookings();


                }
            }
           

        }

        private void RefreshDashBoardBookings()
        {

            try
            {

                if (Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBookingsList") > 0)
                {
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingsList") as frmBookingsList).SetRefreshWhenActive("");
                }

                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshTodayAndPreData();
            
                new BroadcasterData().BroadCastToAll("**close authorize web>>" + Environment.MachineName);

            }
            catch 
            {


            }
        }


       

        void frmFetchedOnlineBookings_Load(object sender, EventArgs e)
        {
            LoadData();


            if (string.IsNullOrEmpty(AppVars.objPolicyConfiguration.UserName) || string.IsNullOrEmpty(AppVars.objPolicyConfiguration.SmtpHost))
            {
                this.IsEmailSettingsDefined = false;
               // ENUtils.ShowMessage("Email Configurations is not defined in Settings");
               
            }
            else
                this.IsEmailSettingsDefined = true;

            PlaySoundNotification("Message1.wav", false);
        }

        private void PlaySoundNotification(string soundFileName, bool looping)
        {
            try
            {
                System.Media.SoundPlayer spMessaging = new System.Media.SoundPlayer();
            

                spMessaging.SoundLocation = System.Windows.Forms.Application.StartupPath + "\\sound\\" + soundFileName;


                if (File.Exists(spMessaging.SoundLocation))
                {

                    if (looping)
                        spMessaging.PlayLooping();

                    else
                        spMessaging.Play();
                }

                spMessaging.Dispose();
            }
            catch
            {


            }
        }


        private void FormatGrid()
        {


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = "ID";
            col.IsVisible = false;
            col.Name = "ID";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "ONLINEBOOKINGID";
            col.IsVisible = false;
            col.Name = "ONLINEBOOKINGID";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ref #";
            col.Name = "REFNO";
            col.IsVisible = false;
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Passenger";
            col.Name = "PASSENGER";
            col.Width = 90;
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Contact";
            col.Name = "CONTACTNO";
            col.Width = 150;
            col.ReadOnly = true;
            col.WrapText = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
           // col.HeaderText = "Contact";
            col.Name = "MOBILENO";
            col.Width = 150;
            col.ReadOnly = true;
            col.WrapText = true;
            col.IsVisible = false;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "EMAIL"; 
          
            grdLister.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            //col.IsVisible = false;
            //col.Name = "STATUS";
            //grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "BOOKINGTYPEID";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "COMPANYSYSGENID";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "JOURNEYTYPEID";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "DEFAULTCLIENTID";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle";
            col.Name = "VEHICLE";
            col.Width = 70;
            col.ReadOnly = true;
            col.WrapText = true;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colD = new GridViewDateTimeColumn();
            colD.HeaderText = "Pickup Time";
            colD.Name = "PICKUPDATETIME";
            colD.CustomFormat = "dd/MM/yyyy HH:mm";
            colD.FormatString = "{0:dd/MM/yyyy HH:mm}";
            colD.ReadOnly = false;
            colD.Width = 95;
            colD.WrapText = true;
            grdLister.Columns.Add(colD);



            colD = new GridViewDateTimeColumn();          
            colD.Name = "OLDPICKUPDATETIME";
            colD.CustomFormat = "dd/MM/yyyy HH:mm";
            colD.FormatString = "{0:dd/MM/yyyy HH:mm}";           
            colD.IsVisible=false;          
            grdLister.Columns.Add(colD);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Flight No";
            col.Name = "FlightNo";
            col.Width = 80;
            col.ReadOnly = true;
            col.WrapText = true;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup";
            col.Name = "FROMADDRESS";
            col.ReadOnly = true;
            col.Width = 140;
            col.WrapText = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Via";
            col.Name = "VIA";
            col.ReadOnly = true;
            col.Width = 90;
            col.WrapText = true;
            grdLister.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "TOADDRESS";
            col.ReadOnly = true;
            col.WrapText = true;
            col.Width = 140;
            grdLister.Columns.Add(col);



            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Fares";
            colDec.Name = "FARES";
            col.ReadOnly = false;
          //  colDec.IsVisible = true;
            colDec.Width = 65;
            grdLister.Columns.Add(colDec);


            colDec = new GridViewDecimalColumn();
            colDec.Name = "OLDFARES";
            colDec.IsVisible = false;          
            grdLister.Columns.Add(colDec);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Payment";
            col.Name = "PAYMENTTYPE";
            col.Width = 60;
            col.ReadOnly = true;
            col.WrapText = true;
            grdLister.Columns.Add(col);



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Waiting Time(Mins)";
            colDec.Name = "WaitingTime";
            colDec.IsVisible = true;
            colDec.Width = 150;
            grdLister.Columns.Add(colDec);

            

            GridViewCommandColumn commandCol = new GridViewCommandColumn();
            commandCol.UseDefaultText = true;
            commandCol.DefaultText = "Accept";
            commandCol.Name = "ACCEPT";
            commandCol.HeaderText = "";
            commandCol.Width=60;
            commandCol.TextAlignment = ContentAlignment.MiddleCenter;

            grdLister.Columns.Add(commandCol);


            commandCol = new GridViewCommandColumn();
            commandCol.UseDefaultText = true;
            commandCol.DefaultText = "Decline";
            commandCol.Name = "DECLINE";
            commandCol.HeaderText = "";
            commandCol.Width=60;
            commandCol.TextAlignment = ContentAlignment.MiddleCenter;

            grdLister.Columns.Add(commandCol);



            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdLister.ShowRowHeaderColumn = false;
            grdLister.ShowGroupPanel = false;
            grdLister.AllowAddNewRow = false;


            grdLister.TableElement.RowHeight = 60;
           // grdLister.AllowEditRow = false;
            grdLister.AllowDeleteRow = false;

        }




        void grdLister_CommandCellClick(object sender, EventArgs e)
        {

            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();


                    GridViewRowInfo row = gridCell.GridControl.CurrentRow;


                    long id = row.Cells["ID"].Value.ToLong();

                    var returnRow = grdLister.Rows.FirstOrDefault(c => c.Index != row.Index && c.Cells["id"].Value.ToLong()==id && c.Cells["JourneyTypeId"].Value.ToInt() == 2);


                    if (returnRow == null)
                    {

                        EmailCustomer(row, name);
                    }
                    else
                    {

                        EmailCustomer(row,returnRow, name);

                    }

                    if (name.Equals("decline"))
                    {
                        new TaxiDataContext().stp_UpdateJobStatus(row.Cells["ID"].Value.ToLong(), Enums.BOOKINGSTATUS.CANCELLED);


                    }

                    SleepAction();

                    row.Delete();


                    if (returnRow != null)
                    {

                        returnRow.Delete();
                    }

                    if (grdLister.Rows.Count == 0)
                    {

                        CloseForm();
                    }
                
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }

        private void SleepAction()
        {
            System.Threading.Thread.Sleep(1000);

        }

        private void CloseForm()
        {
            this.Close();

        }


        private void EmailCustomer(GridViewRowInfo row,string action)
        {
            if (!IsEmailSettingsDefined)
                return;

            long onlineBookingId = row.Cells["ONLINEBOOKINGID"].Value.ToLong();
           

            decimal fares = row.Cells["Fares"].Value.ToDecimal();
            decimal oldFare = row.Cells["OldFares"].Value.ToDecimal();
            string waitingTime = row.Cells["WaitingTime"].Value.ToStr().Trim();

            DateTime? pickupDateTime = row.Cells["PICKUPDATETIME"].Value.ToDateTimeorNull();

            int journeyTypeId = row.Cells["JourneyTypeId"].Value.ToInt();

            if (!string.IsNullOrEmpty(waitingTime.Trim()) && waitingTime.IsNumeric() && waitingTime.ToDecimal() > 0)
            {
                waitingTime = waitingTime + " mins";
            }


            if (onlineBookingId == 0)
            {
                try
                {

                    string[] arrRef = row.Cells["REFNO"].Value.ToStr().Split('/');

                    string refNo = arrRef.Count() == 1 ? arrRef[0].ToStr() : arrRef[1].ToStr();

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


                    if (refNo.ToStr().IsNumeric())
                        onlineBookingId = refNo.ToLong();

                }
                catch
                {


                }
            }

                

            int bookingTypeId = row.Cells["BOOKINGTYPEID"].Value.ToInt();


            if (action != "decline")
                action = "confirmed";



            action = action.ToProperCase();

            long jobId = row.Cells["ID"].Value.ToLong();

            if ( row.Cells["PICKUPDATETIME"].Value.ToDateTime() != row.Cells["OLDPICKUPDATETIME"].Value.ToDateTime()
            || oldFare!=fares)
            {

               

                BookingBO objBO = new BookingBO();
                objBO.GetByPrimaryKey(jobId);

                if (objBO.Current != null && objBO.PrimaryKeyValue != null)
                {
                    //if( action == "confirmed")
                    //{
                       objBO.Current.FareRate = fares;
                //    }
               
                    objBO.Current.PickupDateTime = row.Cells["PICKUPDATETIME"].Value.ToDateTime();
                    objBO.CheckDataValidation = false;
                    objBO.CheckCustomerValidation = false;
                    objBO.Save();
                }
            }



            int defaultclientId = row.Cells["DEFAULTCLIENTID"].Value.ToInt();

            int sysgenId = row.Cells["COMPANYSYSGENID"].Value.ToInt();


            //if (oldFare != fares || !string.IsNullOrEmpty(waitingTime))
            //{
            //    action = "Waiting";
            //}


            //if(defaultclientId>0)
           // {
            if (!string.IsNullOrEmpty(row.Cells["EMAIL"].Value.ToStr()))
            {

         
                string email = row.Cells["EMAIL"].Value.ToStr().Trim();
                string via = row.Cells["VIA"].Value.ToStr();
       

                string newLine = "<br>";
                string msgBody=string.Empty;

                if (action.ToLower() == "decline")
                    msgBody = "<html><body><div>We have declined your online booking request." + newLine;
                else
                    msgBody = "<html><body><div>Your online booking request is confirmed."+newLine;


                msgBody += newLine + AppVars.objPolicyConfiguration.MobileBookingEmailVerification.ToStr().Trim();

       
                if (msgBody.Contains("<BookingNo>"))
                {
                   msgBody= msgBody.Replace("<BookingNo>", onlineBookingId.ToStr());
                }

                if (msgBody.Contains("<FromDoorNo>"))
                {
                  msgBody=  msgBody.Replace("<FromDoorNo>", row.Cells["FlightNo"].Value.ToStr().Trim());

                }

                if (msgBody.Contains("<PickupPoint>"))
                {
                   msgBody= msgBody.Replace("<PickupPoint>", row.Cells["FROMADDRESS"].Value.ToStr().Trim());
                }

                if (msgBody.Contains("<ViaPoint>"))
                {
                    if(!string.IsNullOrEmpty(via))
                    {

                        via ="Via : " +newLine + via.Replace("\r\n", "<br>");
                        msgBody = msgBody.Replace("<ViaPoint>", via);
                    }
                    else
                        msgBody = msgBody.Replace("<ViaPoint>", "").Trim();

                }


                if (msgBody.Contains("<Fares>"))
                {
                   msgBody= msgBody.Replace("<Fares>", row.Cells["Fares"].Value.ToStr());
                }


                if (msgBody.Contains("<Passenger>"))
                {
                    msgBody = msgBody.Replace("<Passenger>", row.Cells["Passenger"].Value.ToStr());

                }               


                if (msgBody.Contains("<Destination>"))
                {
                  msgBody=  msgBody.Replace("<Destination>", row.Cells["TOADDRESS"].Value.ToStr());

                }

                if (msgBody.Contains("<PickupDate>"))
                {
                  msgBody=  msgBody.Replace("<PickupDate>", string.Format("{0:dd/MM/yyyy}", row.Cells["PICKUPDATETIME"].Value));

                }


                if (msgBody.Contains("<PickupTime>"))
                {
                  msgBody=  msgBody.Replace("<PickupTime>", string.Format("{0:HH:mm}", row.Cells["PICKUPDATETIME"].Value));

                }


                if (msgBody.Contains("<CompanyTelNo>"))
                {
                 msgBody=   msgBody.Replace("<CompanyTelNo>", AppVars.objSubCompany.TelephoneNo);

                }

                if (msgBody.Contains("<CompanyName>"))
                {
                  msgBody=  msgBody.Replace("<CompanyName>", AppVars.objSubCompany.CompanyName);
                }

                msgBody = msgBody.Replace("\n", newLine);           


                msgBody += newLine + "</div></body></html>";


                string mobileNo=row.Cells["MOBILENO"].Value.ToStr().Trim();

                Gen_SubCompany objSubcompany = null;

                if (AppVars.ListOfWebsites!=null && AppVars.ListOfWebsites.Count > 1)
                {
                      objSubcompany= General.GetObject<Booking>(c => c.Id == jobId).Gen_SubCompany.DefaultIfEmpty();                    
                }


                new Thread(delegate()
                {


                    if (AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                    {
                        if (action.ToLower() == "decline")
                        {
                            JATEmail.SendCustomerCancelationEmail(General.GetObject<Booking>(c => c.Id == jobId));
                        }
                        else
                        {
                            JATEmail.SendDirectBookingConfirmationEmail(General.GetObject<Booking>(c => c.Id == jobId));
                        }

                    }
                    else
                    {
                        SendEmail(objSubcompany, email, "Your Booking Ref No: " + onlineBookingId.ToStr(), msgBody);

                    }
                    
                    try
                    {

                         if (bookingTypeId == Enums.BOOKING_TYPES.THIRDPARTY)
                        {
                          //  var objBooking=General.GetObject<Booking>(c=>c.Id==jobId);


                            if (sysgenId == Enums.SYSGEN_COMPANY.KARHOO)
                            {
                                //  var objBooking=General.GetObject<Booking>(c=>c.Id==jobId);

                                WebApi.BookingInformation obj = new WebApi.BookingInformation();
                                obj.booking_id = jobId.ToStr();
                                obj.karhoo_ref = onlineBookingId.ToStr();


                                if (action.ToLower() == "decline")
                                    obj.status = "declined";
                                else
                                    obj.status = "confirmed";

                                obj.vehicle = new WebApi.Vehicle();


                                WebAPI.Karho.UpdateTripStatus(obj, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");
                            }
                            else if (sysgenId == Enums.SYSGEN_COMPANY.KABBEE)
                            {

                                WebApi.KabbeeProperties obj = new WebApi.KabbeeProperties();
                                obj.FleetBookingId = jobId.ToStr();

                                if (action.ToLower() == "decline")
                                    obj.Status = WebApi.KabbeeStatus.CANCELLED;
                                else
                                    obj.Status = WebApi.KabbeeStatus.CREATED;


                                string response = WebApi.Kabbee.BookingStatusUpdate(obj, "Y2FidHJlYXN1cmVAY2Fia2FiYmVlLmNvbTpjYWJ0cmVhc3VyZQ==");

                            }
                        }
                        else
                        {


                           

                                // NEED TO UNCOMMENT                    
                                //if (AppVars.objPolicyConfiguration.PDANewWeekMessageByDay.ToStr().Trim().ToLower() == "old")
                                //{
                                //    new WebDataClassesDataContext().spUpdateBookingConfirmationFromApp2(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime));
                                //}
                                //else
                                //{
                                //    new DataClassesOnlineVehicleDataContext().spUpdateBookingConfirmationFromApp2(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime));
                                //}

                         

                            
                         
                            
                        }

                        SendSMS(msgBody, mobileNo);

                    }
                    catch 
                    {


                    }
                }).Start();

            }

            else
            {

                string msgBody = string.Empty;
                string mobileNo = row.Cells["MOBILENO"].Value.ToStr().Trim();

                if (mobileNo.ToStr().Trim().Length > 6)
                {
                    string via = row.Cells["VIA"].Value.ToStr();


                    string newLine = Environment.NewLine;





                    if (action.ToLower() == "decline")
                        msgBody = "We have declined your online booking request." + newLine;
                    else
                        msgBody = "Your online booking request is confirmed." + newLine;


                    msgBody += newLine + AppVars.objPolicyConfiguration.MobileBookingEmailVerification.ToStr().Trim();


                    if (msgBody.Contains("<BookingNo>"))
                    {
                        msgBody = msgBody.Replace("<BookingNo>", onlineBookingId.ToStr());
                    }

                    if (msgBody.Contains("<FromDoorNo>"))
                    {
                        msgBody = msgBody.Replace("<FromDoorNo>", row.Cells["FlightNo"].Value.ToStr().Trim());

                    }

                    if (msgBody.Contains("<PickupPoint>"))
                    {
                        msgBody = msgBody.Replace("<PickupPoint>", row.Cells["FROMADDRESS"].Value.ToStr().Trim());
                    }

                    if (!string.IsNullOrEmpty(via) && msgBody.Contains("<ViaPoint>"))
                    {
                        via = "Via : " + newLine + via.Replace("\r\n", "<br>");
                        msgBody = msgBody.Replace("<ViaPoint>", via);
                    }


                    if (msgBody.Contains("<Fares>"))
                    {
                        msgBody = msgBody.Replace("<Fares>", row.Cells["Fares"].Value.ToStr());
                    }


                    if (msgBody.Contains("<Passenger>"))
                    {
                        msgBody = msgBody.Replace("<Passenger>", row.Cells["Passenger"].Value.ToStr());

                    }


                    if (msgBody.Contains("<Destination>"))
                    {
                        msgBody = msgBody.Replace("<Destination>", row.Cells["TOADDRESS"].Value.ToStr());

                    }


                       if (msgBody.Contains("<ViaPoint>"))
                       {
                            if(!string.IsNullOrEmpty(via))
                            {

                                via ="Via : " +newLine + via.Replace("\r\n", "<br>");
                                msgBody = msgBody.Replace("<ViaPoint>", via);
                            }
                            else
                                msgBody = msgBody.Replace("<ViaPoint>", "").Trim();

                       }

                    if (msgBody.Contains("<PickupDate>"))
                    {
                        msgBody = msgBody.Replace("<PickupDate>", string.Format("{0:dd/MM/yyyy}", row.Cells["PICKUPDATETIME"].Value));

                    }


                    if (msgBody.Contains("<PickupTime>"))
                    {
                        msgBody = msgBody.Replace("<PickupTime>", string.Format("{0:HH:mm}", row.Cells["PICKUPDATETIME"].Value));

                    }


                    if (msgBody.Contains("<CompanyTelNo>"))
                    {
                        msgBody = msgBody.Replace("<CompanyTelNo>", AppVars.objSubCompany.TelephoneNo);

                    }

                    if (msgBody.Contains("<CompanyName>"))
                    {
                        msgBody = msgBody.Replace("<CompanyName>", AppVars.objSubCompany.CompanyName);
                    }

                    msgBody = msgBody.Replace("\n", newLine);


                   // msgBody += newLine + "</div></body></html>";

                }


                new Thread(delegate()
                {
    
                    try
                    {

                        if (bookingTypeId == Enums.BOOKING_TYPES.THIRDPARTY)
                        {
                          //  var objBooking=General.GetObject<Booking>(c=>c.Id==jobId);


                            if (sysgenId == Enums.SYSGEN_COMPANY.KARHOO)
                            {
                                //  var objBooking=General.GetObject<Booking>(c=>c.Id==jobId);

                                WebApi.BookingInformation obj = new WebApi.BookingInformation();
                                obj.booking_id = jobId.ToStr();
                                obj.karhoo_ref = onlineBookingId.ToStr();

                                if (action.ToLower() == "decline")
                                    obj.status = "declined";
                                else
                                    obj.status = "confirmed";

                                obj.vehicle = new WebApi.Vehicle();

                                WebAPI.Karho.UpdateTripStatus(obj, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");
                            }
                            else if (sysgenId == Enums.SYSGEN_COMPANY.KABBEE)
                            {

                                WebApi.KabbeeProperties obj = new WebApi.KabbeeProperties();
                                obj.FleetBookingId = jobId.ToStr();

                                if (action.ToLower() == "decline")
                                    obj.Status = WebApi.KabbeeStatus.CANCELLED;
                                else
                                    obj.Status = WebApi.KabbeeStatus.CREATED;


                                string response = WebApi.Kabbee.BookingStatusUpdate(obj, "Y2FidHJlYXN1cmVAY2Fia2FiYmVlLmNvbTpjYWJ0cmVhc3VyZQ==");

                            }
                        }
                        //else
                        //{

                        //    if (AppVars.objPolicyConfiguration.PDANewWeekMessageByDay.ToStr().Trim().ToLower() == "old")
                        //    {
                        //        new WebDataClassesDataContext().spUpdateBookingConfirmationFromApp2(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime));
                        //    }
                        //    else
                        //    {
                        //        new DataClassesOnlineVehicleDataContext().spUpdateBookingConfirmationFromApp2(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime));
                        //    }
                        //}





                        SendSMS(msgBody, mobileNo);
                    }
                    catch 
                    {


                    }
                }).Start();


            }


        }






        private void EmailCustomer(GridViewRowInfo row,GridViewRowInfo returnRow, string action)
        {
            if (!IsEmailSettingsDefined)
                return;

            long onlineBookingId = row.Cells["ONLINEBOOKINGID"].Value.ToLong();
           
          

            decimal fares = row.Cells["Fares"].Value.ToDecimal();
            decimal oldFare = row.Cells["OldFares"].Value.ToDecimal();
            string waitingTime = row.Cells["WaitingTime"].Value.ToStr().Trim();

            DateTime? pickupDateTime = row.Cells["PICKUPDATETIME"].Value.ToDateTimeorNull();


            DateTime? returnPickupDateTime = returnRow.Cells["PICKUPDATETIME"].Value.ToDateTimeorNull();
            decimal retFares = returnRow.Cells["Fares"].Value.ToDecimal();

            int journeyTypeId = row.Cells["JourneyTypeId"].Value.ToInt();


            if (!string.IsNullOrEmpty(waitingTime.Trim()) && waitingTime.IsNumeric() && waitingTime.ToDecimal() > 0)
            {
                waitingTime = waitingTime + " mins";

            }



            if (onlineBookingId == 0)
            {
                try
                {

                    string[] arrRef = row.Cells["REFNO"].Value.ToStr().Split('/');

                    string refNo = arrRef.Count() == 1 ? arrRef[0].ToStr() : arrRef[1].ToStr();

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


                    if (refNo.ToStr().IsNumeric())
                        onlineBookingId = refNo.ToLong();

                }
                catch
                {


                }
            }


          


            int bookingTypeId = row.Cells["BOOKINGTYPEID"].Value.ToInt();
            int sysgenId = row.Cells["COMPANYSYSGENID"].Value.ToInt();


            if (action != "decline")
                action = "confirmed";



            action = action.ToProperCase();


            long jobId = row.Cells["ID"].Value.ToLong();

            if (returnRow != null || 
                (row.Cells["PICKUPDATETIME"].Value.ToDateTime() != row.Cells["OLDPICKUPDATETIME"].Value.ToDateTime() && oldFare!=fares)
                )
            {

              

                BookingBO objBO = new BookingBO();
                objBO.GetByPrimaryKey(jobId);

                if (objBO.Current != null && objBO.PrimaryKeyValue != null)
                {
                    //if( action == "confirmed")
                    //{
                    objBO.Current.FareRate = fares;
                    //    }

                    objBO.Current.PickupDateTime = row.Cells["PICKUPDATETIME"].Value.ToDateTime();


                    if (objBO.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                    {
                        objBO.Current.ReturnPickupDateTime = returnPickupDateTime;
                        objBO.Current.ReturnFareRate =retFares;



                    }


                    objBO.CheckDataValidation = false;
                    objBO.CheckCustomerValidation = false;
                    objBO.Save();
                }
            }



            int defaultclientId = row.Cells["DEFAULTCLIENTID"].Value.ToInt();



            if (!string.IsNullOrEmpty(row.Cells["EMAIL"].Value.ToStr()))
            {

                //    string email = "danish85_dj@hotmail.com";
                string email = row.Cells["EMAIL"].Value.ToStr().Trim();
                string via = row.Cells["VIA"].Value.ToStr();
                //  string newLine = "</br></br>";
                string newLine = "<br>";
                   


                string msgBody = string.Empty;



                if (action.ToLower() == "decline")
                    msgBody = "<html><body><div><b>We have declined your online booking request.</b>" + newLine;
                else
                    msgBody = "<html><body><div><b>Your online booking request is confirmed.</b>" + newLine;


                msgBody += newLine + AppVars.objPolicyConfiguration.MobileBookingEmailVerification.ToStr().Trim();


                string header = string.Empty;


                

                if (msgBody.Contains("<BookingNo>"))
                {

                  //  header=msgBody.Substring(0,msgBody.IndexOf(("<BookingNo>")));


                    msgBody = msgBody.Replace("<BookingNo>", onlineBookingId.ToStr());

                }

                if (msgBody.Contains("<FromDoorNo>"))
                {
                    msgBody = msgBody.Replace("<FromDoorNo>", row.Cells["FlightNo"].Value.ToStr().Trim());

                }

                if (msgBody.Contains("<PickupPoint>"))
                {
                    msgBody = msgBody.Replace("<PickupPoint>", row.Cells["FROMADDRESS"].Value.ToStr().Trim());

                }


                if (msgBody.Contains("<ViaPoint>"))
                {
                    if (!string.IsNullOrEmpty(via))
                    {

                        via = "Via : " + newLine + via.Replace("\r\n", "<br>");
                        msgBody = msgBody.Replace("<ViaPoint>", via);
                    }
                    else
                        msgBody = msgBody.Replace("<ViaPoint>", "").Trim();

                }


                //if (!string.IsNullOrEmpty(via) && msgBody.Contains("<ViaPoint>"))
                //{
                //    via = "Via : " + newLine + via.Replace("\r\n", "<br>");
                //    msgBody = msgBody.Replace("<ViaPoint>", via);

                //}

                if (msgBody.Contains("<Fares>"))
                {
                    msgBody = msgBody.Replace("<Fares>", row.Cells["Fares"].Value.ToStr());

                }



                if (msgBody.Contains("<Passenger>"))
                {
                    msgBody = msgBody.Replace("<Passenger>", row.Cells["Passenger"].Value.ToStr());

                }



                if (msgBody.Contains("<Destination>"))
                {
                    msgBody = msgBody.Replace("<Destination>", row.Cells["TOADDRESS"].Value.ToStr());

                }


                if (msgBody.Contains("<PickupDate>"))
                {
                    msgBody = msgBody.Replace("<PickupDate>", string.Format("{0:dd/MM/yyyy}", row.Cells["PICKUPDATETIME"].Value));

                }


                string footer = string.Empty;

                if (msgBody.Contains("<PickupTime>"))
                {

                    footer = msgBody.Substring(msgBody.IndexOf("<PickupTime>") + 12);

                   

                    msgBody = msgBody.Replace("<PickupTime>", string.Format("{0:HH:mm}", row.Cells["PICKUPDATETIME"].Value));


                    msgBody = msgBody.Replace(footer, "").Trim();
                }


                string mobileText = msgBody;
                string mobileNo = row.Cells["MobileNo"].Value.ToStr().Trim();                          
            

                msgBody += newLine + newLine + "<b>Return Journey Details:-</b>";

                msgBody += newLine + AppVars.objPolicyConfiguration.MobileBookingEmailVerification.ToStr().Trim();


                if (msgBody.Contains("<BookingNo>"))
                {
                    msgBody = msgBody.Replace("<BookingNo>", onlineBookingId.ToStr());

                }


                if (msgBody.Contains("<Passenger>"))
                {
                    msgBody = msgBody.Replace("<Passenger>", returnRow.Cells["Passenger"].Value.ToStr());

                }


                if (msgBody.Contains("<FromDoorNo>"))
                {
                    msgBody = msgBody.Replace("<FromDoorNo>", returnRow.Cells["FlightNo"].Value.ToStr().Trim());

                }

                if (msgBody.Contains("<PickupPoint>"))
                {
                    msgBody = msgBody.Replace("<PickupPoint>", returnRow.Cells["FROMADDRESS"].Value.ToStr().Trim());

                }



                if (!string.IsNullOrEmpty(via) && msgBody.Contains("<ViaPoint>"))
                {
                    via = "Via : " + newLine + via.Replace("\r\n", "<br>");
                    msgBody = msgBody.Replace("<ViaPoint>", via);

                }

                if (msgBody.Contains("<Fares>"))
                {
                    msgBody = msgBody.Replace("<Fares>", returnRow.Cells["Fares"].Value.ToStr());

                }
                
                
                if (msgBody.Contains("<Destination>"))
                {
                    msgBody = msgBody.Replace("<Destination>", returnRow.Cells["TOADDRESS"].Value.ToStr());

                }


                if (msgBody.Contains("<PickupDate>"))
                {
                    msgBody = msgBody.Replace("<PickupDate>", string.Format("{0:dd/MM/yyyy}", returnRow.Cells["PICKUPDATETIME"].Value));

                }


                if (msgBody.Contains("<PickupTime>"))
                {
                    msgBody = msgBody.Replace("<PickupTime>", string.Format("{0:HH:mm}", returnRow.Cells["PICKUPDATETIME"].Value));

                }


                if (msgBody.Contains("<CompanyTelNo>"))
                {
                    msgBody = msgBody.Replace("<CompanyTelNo>", AppVars.objSubCompany.TelephoneNo);

                }


                if (msgBody.Contains("<CompanyName>"))
                {
                    msgBody = msgBody.Replace("<CompanyName>", AppVars.objSubCompany.CompanyName);

                }

                msgBody = msgBody.Replace("\n", newLine);



                //

             
             

                msgBody += newLine + "</div></body></html>";

                Gen_SubCompany objSubcompany = null;
                if (AppVars.ListOfWebsites != null && AppVars.ListOfWebsites.Count > 1)
                {
                     objSubcompany = General.GetObject<Booking>(c => c.Id == jobId).Gen_SubCompany.DefaultIfEmpty();
                }


                new Thread(delegate()
                {

                    if (AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                    {
                        if (action.ToLower() == "decline")
                        {
                            JATEmail.SendCustomerCancelationEmail(General.GetObject<Booking>(c => c.Id == jobId));
                        }
                        else
                        {
                            JATEmail.SendDirectBookingConfirmationEmail(General.GetObject<Booking>(c => c.Id == jobId));
                        }

                    }
                    else
                    {
                        SendEmail(objSubcompany, email, "Your Booking Ref No: " + onlineBookingId.ToStr(), msgBody);

                    }

                   

                    // if (bookingTypeId == Enums.BOOKING_TYPES.ONLINE)
                    try
                    {
                        //   new WebDataClassesDataContext().spUpdateBookingFromApp(defaultclientId, refNo.ToLong(), action);
                        //  new WebDataClassesDataContext().spUpdateBookingConfirmationFromApp2(defaultclientId, refNo.ToLong(), action, fares, waitingTime);

                        if (bookingTypeId == Enums.BOOKING_TYPES.THIRDPARTY)
                        {
                          //  var objBooking=General.GetObject<Booking>(c=>c.Id==jobId);


                            if (sysgenId == Enums.SYSGEN_COMPANY.KARHOO)
                            {

                                WebApi.BookingInformation obj = new WebApi.BookingInformation();
                                obj.booking_id = jobId.ToStr();
                                obj.karhoo_ref = onlineBookingId.ToStr();

                                if (action.ToLower() == "decline")
                                    obj.status = "declined";
                                else
                                    obj.status = "confirmed";

                                obj.vehicle = new WebApi.Vehicle();


                                WebAPI.Karho.UpdateTripStatus(obj, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");
                            }
                            else if (sysgenId == Enums.SYSGEN_COMPANY.KABBEE)
                            {

                                WebApi.KabbeeProperties obj = new WebApi.KabbeeProperties();
                                obj.FleetBookingId = jobId.ToStr();
                            
                                if (action.ToLower() == "decline")
                                    obj.Status =WebApi.KabbeeStatus.CANCELLED;
                                else
                                    obj.Status =WebApi.KabbeeStatus.CREATED;                       


                              string response=    WebApi.Kabbee.BookingStatusUpdate(obj, "Y2FidHJlYXN1cmVAY2Fia2FiYmVlLmNvbTpjYWJ0cmVhc3VyZQ==");

                            }
                        }

                        //else
                        //{

                        //    if (AppVars.objPolicyConfiguration.PDANewWeekMessageByDay.ToStr().Trim().ToLower() == "old")
                        //    {

                        //        new WebDataClassesDataContext().spUpdateBookingConfirmationFromApp3(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime), string.Format("{0:d MMMM yyyy}", returnPickupDateTime), string.Format("{0:HH:mm}", returnPickupDateTime), retFares);
                        //    }
                        //    else
                        //    {

                        //        new DataClassesOnlineVehicleDataContext().spUpdateBookingConfirmationFromApp3(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime), string.Format("{0:d MMMM yyyy}", returnPickupDateTime), string.Format("{0:HH:mm}", returnPickupDateTime), retFares);

                        //    }
                        //}


                        SendSMS(mobileText, mobileNo);
                    }
                    catch 
                    {


                    }
                }).Start();





            }

            else
            {



                string msgBody = string.Empty;

                string mobileNo = row.Cells["MobileNo"].Value.ToStr().Trim();



                if (mobileNo.Length > 6)
                {
                    string newLine = Environment.NewLine;

                    if (action.ToLower() == "decline")
                        msgBody = "<html><body><div><b>We have declined your online booking request.</b>" + newLine;
                    else
                        msgBody = "<html><body><div><b>Your online booking request is confirmed.</b>" + newLine;


                    msgBody += newLine + AppVars.objPolicyConfiguration.MobileBookingEmailVerification.ToStr().Trim();


                    string header = string.Empty;




                    if (msgBody.Contains("<BookingNo>"))
                    {

                        //  header=msgBody.Substring(0,msgBody.IndexOf(("<BookingNo>")));


                        msgBody = msgBody.Replace("<BookingNo>", onlineBookingId.ToStr());

                    }

                    if (msgBody.Contains("<FromDoorNo>"))
                    {
                        msgBody = msgBody.Replace("<FromDoorNo>", row.Cells["FlightNo"].Value.ToStr().Trim());

                    }

                    if (msgBody.Contains("<PickupPoint>"))
                    {
                        msgBody = msgBody.Replace("<PickupPoint>", row.Cells["FROMADDRESS"].Value.ToStr().Trim());
                    }

                    string via = row.Cells["VIA"].Value.ToStr();

                    if (msgBody.Contains("<ViaPoint>"))
                    {
                        if (!string.IsNullOrEmpty(via))
                        {

                            via = "Via : " + newLine + via.Replace("\r\n", "<br>");
                            msgBody = msgBody.Replace("<ViaPoint>", via);
                        }
                        else
                            msgBody = msgBody.Replace("<ViaPoint>", "").Trim();

                    }

                    if (msgBody.Contains("<Fares>"))
                    {
                        msgBody = msgBody.Replace("<Fares>", row.Cells["Fares"].Value.ToStr());

                    }



                    if (msgBody.Contains("<Passenger>"))
                    {
                        msgBody = msgBody.Replace("<Passenger>", row.Cells["Passenger"].Value.ToStr());

                    }

                    if (msgBody.Contains("<Destination>"))
                    {
                        msgBody = msgBody.Replace("<Destination>", row.Cells["TOADDRESS"].Value.ToStr());

                    }

                    if (msgBody.Contains("<PickupDate>"))
                    {
                        msgBody = msgBody.Replace("<PickupDate>", string.Format("{0:dd/MM/yyyy}", row.Cells["PICKUPDATETIME"].Value));

                    }
                    string footer = string.Empty;

                    if (msgBody.Contains("<PickupTime>"))
                    {

                        footer = msgBody.Substring(msgBody.IndexOf("<PickupTime>") + 12);
                        msgBody = msgBody.Replace("<PickupTime>", string.Format("{0:HH:mm}", row.Cells["PICKUPDATETIME"].Value));
                        msgBody = msgBody.Replace(footer, "").Trim();
                    }

                }
               
               
                new Thread(delegate()
                {


                    //  if (bookingTypeId == Enums.BOOKING_TYPES.ONLINE)

                    try
                    {
                        if (bookingTypeId == Enums.BOOKING_TYPES.THIRDPARTY)
                        {
                          //  var objBooking=General.GetObject<Booking>(c=>c.Id==jobId);


                            if (sysgenId == Enums.SYSGEN_COMPANY.KARHOO)
                            {
                              
                                WebApi.BookingInformation obj = new WebApi.BookingInformation();
                                obj.booking_id = jobId.ToStr();
                                obj.karhoo_ref = onlineBookingId.ToStr();

                                if (action.ToLower() == "decline")
                                    obj.status = "declined";
                                else
                                    obj.status = "confirmed";

                                obj.vehicle = new WebApi.Vehicle();


                                WebAPI.Karho.UpdateTripStatus(obj, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");
                            }
                            else if (sysgenId == Enums.SYSGEN_COMPANY.KABBEE)
                            {

                                WebApi.KabbeeProperties obj = new WebApi.KabbeeProperties();
                                obj.FleetBookingId = jobId.ToStr();

                                if (action.ToLower() == "decline")
                                    obj.Status = WebApi.KabbeeStatus.CANCELLED;
                                else
                                    obj.Status = WebApi.KabbeeStatus.CREATED;


                                string response = WebApi.Kabbee.BookingStatusUpdate(obj, "Y2FidHJlYXN1cmVAY2Fia2FiYmVlLmNvbTpjYWJ0cmVhc3VyZQ==");

                            }
                        }
                        //else
                        //{

                        //    if (AppVars.objPolicyConfiguration.PDANewWeekMessageByDay.ToStr().Trim().ToLower() == "old")
                        //    {
                        //        new WebDataClassesDataContext().spUpdateBookingConfirmationFromApp3(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime), string.Format("{0:d MMMM yyyy}", returnPickupDateTime), string.Format("{0:HH:mm}", returnPickupDateTime), retFares);
                        //    }
                        //    else
                        //    {
                        //        new DataClassesOnlineVehicleDataContext().spUpdateBookingConfirmationFromApp3(defaultclientId, onlineBookingId, action, fares, waitingTime, string.Format("{0:d MMMM yyyy}", pickupDateTime), string.Format("{0:HH:mm}", pickupDateTime), string.Format("{0:d MMMM yyyy}", returnPickupDateTime), string.Format("{0:HH:mm}", returnPickupDateTime), retFares);

                        //    }
                        //}

                        SendSMS(msgBody, mobileNo);
                      

                    }
                    catch 
                    {


                    }
                }).Start();


            }


        }



        private bool SendSMS(string msg, string mobileNo)
        {

            if (mobileNo.ToStr().Trim().Length < 6)
                return false;

            bool rtn = true;
            try
            {

                if (msg.ToStr().Contains("<html><body><div>"))
                    msg = msg.Replace("<html><body><div>", "").Trim();

                if (msg.ToStr().Contains("<br>"))
                    msg = msg.Replace("<br>", Environment.NewLine).Trim();

                if (msg.ToStr().Contains("</div></body></html>"))
                    msg = msg.Replace("</div></body></html>", "").Trim();


                EuroSMS objSMS = new EuroSMS();
                string smsError1 = "";


                int idx = -1;
                if (mobileNo.StartsWith("044") == true)
                {
                    idx = mobileNo.IndexOf("044");
                    mobileNo = mobileNo.Substring(idx + 3);
                    mobileNo = mobileNo.Insert(0, "+44");
                }

                if (mobileNo.StartsWith("07"))
                {
                    mobileNo = mobileNo.Substring(1);
                }

                if (mobileNo.StartsWith("0440") == false || mobileNo.StartsWith("+440") == false)
                    mobileNo = mobileNo.Insert(0, "+44");

                objSMS.ToNumber = mobileNo;
                objSMS.Message = msg;
                //  System.Threading.Thread.Sleep(1000);
                rtn = objSMS.Send(ref smsError1);
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);


            }

            return rtn;

        }

       
        private void LoadData()
        {

            try
            {

                string accountName = string.Empty;


                grdLister.RowCount = this.ListofFetechedJobs.Count;


              
                for (int i = 0; i < ListofFetechedJobs.Count; i++)
                {

                    grdLister.Rows[i].Cells["ID"].Value = ListofFetechedJobs[i].Id;
                    grdLister.Rows[i].Cells["ONLINEBOOKINGID"].Value = ListofFetechedJobs[i].OnlineBookingId;
                    grdLister.Rows[i].Cells["REFNO"].Value = ListofFetechedJobs[i].BookingNo;
                    grdLister.Rows[i].Cells["PASSENGER"].Value = ListofFetechedJobs[i].CustomerName;

                    grdLister.Rows[i].Cells["BOOKINGTYPEID"].Value = ListofFetechedJobs[i].BookingTypeId;
                    grdLister.Rows[i].Cells["DEFAULTCLIENTID"].Value = ListofFetechedJobs[i].AddBy;


                    if (!string.IsNullOrEmpty(ListofFetechedJobs[i].CustomerPhoneNo) && !string.IsNullOrEmpty(ListofFetechedJobs[i].CustomerMobileNo))
                        grdLister.Rows[i].Cells["CONTACTNO"].Value = ListofFetechedJobs[i].CustomerPhoneNo + "/" + ListofFetechedJobs[i].CustomerMobileNo;
           
                    else if (!string.IsNullOrEmpty(ListofFetechedJobs[i].CustomerPhoneNo))
                        grdLister.Rows[i].Cells["CONTACTNO"].Value = ListofFetechedJobs[i].CustomerPhoneNo;
            
                    else if (!string.IsNullOrEmpty(ListofFetechedJobs[i].CustomerMobileNo))
                        grdLister.Rows[i].Cells["CONTACTNO"].Value = ListofFetechedJobs[i].CustomerMobileNo;


                    if (!string.IsNullOrEmpty(ListofFetechedJobs[i].CustomerEmail))
                    {
                        grdLister.Rows[i].Cells["CONTACTNO"].Value += Environment.NewLine + "Email : " + ListofFetechedJobs[i].CustomerEmail;
                        grdLister.Rows[i].Cells["EMAIL"].Value = ListofFetechedJobs[i].CustomerEmail;

                    }

                     if (!string.IsNullOrEmpty(ListofFetechedJobs[i].CustomerMobileNo))
                        grdLister.Rows[i].Cells["MOBILENO"].Value = ListofFetechedJobs[i].CustomerMobileNo.ToStr().Trim();

                    grdLister.Rows[i].Cells["JOURNEYTYPEID"].Value = ListofFetechedJobs[i].JourneyTypeId.ToInt();


                    // Vehicle Type and Payment Type Added on request of Maple Chauffers 14-01-2016
                    grdLister.Rows[i].Cells["VEHICLE"].Value = ListofFetechedJobs[i].BoundType.ToStr();
                    grdLister.Rows[i].Cells["PAYMENTTYPE"].Value = ListofFetechedJobs[i].CancelReason.ToStr();
                    //


                    grdLister.Rows[i].Cells["PICKUPDATETIME"].Value = ListofFetechedJobs[i].PickupDateTime;
                    grdLister.Rows[i].Cells["OLDPICKUPDATETIME"].Value = ListofFetechedJobs[i].PickupDateTime;


                    grdLister.Rows[i].Cells["FlightNo"].Value = ListofFetechedJobs[i].FromDoorNo;


                    if(!string.IsNullOrEmpty(ListofFetechedJobs[i].FromStreet.ToStr().Trim()))
                        grdLister.Rows[i].Cells["FlightNo"].Value+= " - " + ListofFetechedJobs[i].FromStreet;




                    grdLister.Rows[i].Cells["FROMADDRESS"].Value = ListofFetechedJobs[i].FromAddress;
                    grdLister.Rows[i].Cells["TOADDRESS"].Value = ListofFetechedJobs[i].ToAddress;

                    grdLister.Rows[i].Cells["FARES"].Value = ListofFetechedJobs[i].FareRate.ToDecimal();
                    grdLister.Rows[i].Cells["OLDFARES"].Value = ListofFetechedJobs[i].FareRate.ToDecimal();


                    grdLister.Rows[i].Cells["VIA"].Value = ListofFetechedJobs[i].DistanceString.ToStr();


                    if (ListofFetechedJobs[i].CompanyId != null && ListofFetechedJobs[i].IsQuotation.ToBool())
                    {
                        accountName ="Account - "+ ListofFetechedJobs[i].CompanyCreditCardDetails.ToStr();

                    }

                    // this is a karhoo/kabee sysgenid
                    grdLister.Rows[i].Cells["COMPANYSYSGENID"].Value = ListofFetechedJobs[i].DriverWaitingMins.ToInt();

                    if (ListofFetechedJobs[i].JourneyTypeId.ToInt() == 2)
                    {
                        grdLister.Rows[i].Cells["FROMADDRESS"].Style.BackColor = Color.Yellow;
                        grdLister.Rows[i].Cells["FROMADDRESS"].Style.NumberOfColors = 1;
                    
                        grdLister.Rows[i].Cells["FROMADDRESS"].Style.CustomizeFill = true;


                        grdLister.Rows[i].Cells["TOADDRESS"].Style.BackColor = Color.Yellow;
                        grdLister.Rows[i].Cells["TOADDRESS"].Style.NumberOfColors = 1;

                        grdLister.Rows[i].Cells["TOADDRESS"].Style.CustomizeFill = true;
           


                    }
                    else
                    {

                          

                        if (grdLister.Rows[i].Cells["ACCEPT"].Style.CustomizeFill == true)
                        {
                            grdLister.Rows[i].Cells["ACCEPT"].Style.CustomizeFill = false;
                            grdLister.Rows[i].Cells["DECLINE"].Style.CustomizeFill = false;
                        }
                    }
                 




                }
             
                    grdLister.Columns["WaitingTime"].ReadOnly = true;
                    grdLister.Columns["WaitingTime"].IsVisible = false;

                    grdLister.Columns["FROMADDRESS"].Width += 50;
                    grdLister.Columns["TOADDRESS"].Width += 50;
                    grdLister.Columns["VIA"].Width += 50;

                    if (accountName.Length > 0)
                    {
                        lblAccountName.Visible = true;
                        lblAccountName.Text = accountName;

                    }

            }
            catch 
            {



            }          
        }      

       

        private void btnAcceptAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Accept All Bookings?", "Authorization", MessageBoxButtons.YesNo))
                {
                    int rowCount = grdLister.Rows.Count;

                    for (int i = 0; i < rowCount; i++)
                    {
                        EmailCustomer(grdLister.Rows[i], "accept");

                    }



                    grdLister.Rows.Clear();

                    SleepAction();

                    CloseForm();
                }


            }
            catch 
            {
                grdLister.Rows.Clear();
                CloseForm();


            }
        }


        

        public void SendEmail(Gen_SubCompany objSubCompany, string ToAddress, string subject, string strBody)
        {

            try
            {


                if (ToAddress.IsValidEmailAddress() && !string.IsNullOrEmpty(strBody))
                {

                    if (objSubCompany != null && objSubCompany.SmtpHost != null)
                    {

                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.To.Add(ToAddress);
                        msg.From = new System.Net.Mail.MailAddress(objSubCompany.SmtpUserName.ToStr(), objSubCompany.CompanyName.ToStr());
                        msg.Subject = subject;



                        if (objSubCompany.EmailCC.ToStr().Trim() != string.Empty)
                        {
                            msg.CC.Add(objSubCompany.EmailCC.ToStr().Trim());
                        }




                        msg.Body = strBody;

                        msg.BodyEncoding = System.Text.Encoding.UTF8;
                        msg.IsBodyHtml = true;

                        //    msg.Priority = System.Net.Mail.MailPriority.High;

                        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                        client.Credentials = new NetworkCredential(objSubCompany.SmtpUserName.ToStr(), objSubCompany.SmtpPassword.ToStr());
                        client.Port = Convert.ToInt32(objSubCompany.SmtpPort);
                        client.Host = objSubCompany.SmtpHost;
                        client.EnableSsl = objSubCompany.SmtpHasSSL.ToBool();




                        FieldInfo transport = client.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
                        FieldInfo authModules = transport.GetValue(client).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);

                        Array modulesArray = authModules.GetValue(transport.GetValue(client)) as Array;
                        modulesArray.SetValue(modulesArray.GetValue(3), 1);

                        
                            ServicePointManager.ServerCertificateValidationCallback =
                                delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                         System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                                { return true; };
                        
                        client.Send(msg);
                    }
                    else
                    {
                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.To.Add(ToAddress);
                        msg.From = new System.Net.Mail.MailAddress(AppVars.objPolicyConfiguration.UserName.ToStr(), AppVars.objSubCompany.CompanyName.ToStr());
                        msg.Subject = subject;



                        if (AppVars.objSubCompany != null && AppVars.objSubCompany.EmailCC.ToStr().Trim() != string.Empty)
                        {
                            msg.CC.Add(AppVars.objSubCompany.EmailCC.ToStr().Trim());
                        }




                        msg.Body = strBody;
                        msg.BodyEncoding = System.Text.Encoding.UTF8;
                        msg.IsBodyHtml = true;
                        msg.Priority = System.Net.Mail.MailPriority.High;
                        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                        client.Credentials = new NetworkCredential(AppVars.objPolicyConfiguration.UserName.ToStr(), AppVars.objPolicyConfiguration.Password.ToStr());
                        client.Port = Convert.ToInt32(AppVars.objPolicyConfiguration.Port);
                        client.Host = AppVars.objPolicyConfiguration.SmtpHost;
                        client.EnableSsl =AppVars.objPolicyConfiguration.EnableSSL.ToBool();

                        FieldInfo transport = client.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
                        FieldInfo authModules = transport.GetValue(client).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);

                        Array modulesArray = authModules.GetValue(transport.GetValue(client)) as Array;
                        modulesArray.SetValue(modulesArray.GetValue(3), 1);

                        
                            ServicePointManager.ServerCertificateValidationCallback =
                                delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                         System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                                { return true; };
                        
                        client.Send(msg);


                    }
                }
            }
            catch 
            {


            }

        }


        private void btnDeclineAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Decline All Bookings?", "Authorization", MessageBoxButtons.YesNo))
                {
                    int rowCount = grdLister.Rows.Count;

                    for (int i = 0; i < rowCount; i++)
                    {
                        EmailCustomer(grdLister.Rows[i], "decline");
                        new TaxiDataContext().stp_UpdateJobStatus(grdLister.Rows[i].Cells["ID"].Value.ToLong(), Enums.BOOKINGSTATUS.CANCELLED);
                    }


                    //AppVars.frmMDI.RefreshDashBoard();

                    grdLister.Rows.Clear();

                    SleepAction();
                    CloseForm();
                    
                }


            }
            catch 
            {

                grdLister.Rows.Clear();
                CloseForm();

            }
        }



    }
}
