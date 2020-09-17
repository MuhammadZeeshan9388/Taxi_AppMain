using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using CallerIdData;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using UI;
using System.Threading;

using System.Net;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;

namespace Taxi_AppMain
{
    public partial class CallerIdPopup :Form
    {


        RadDropDownMenu objContextMenu = null;

        private string _BlackListReason;

        public string BlackListReason
        {
            get { return _BlackListReason; }
            set { _BlackListReason = value; }
        }


        private int? _BookingTypeId=Enums.BOOKING_TYPES.LOCAL;

        public int? BookingTypeId
        {
            get { return _BookingTypeId; }
            set { _BookingTypeId = value; }
        }


        eCallerIdType callerId_Call;
        private string _CallerIdName;

        public string CallerIdName
        {
            get { return _CallerIdName; }
            set { _CallerIdName = value; }
        }
        private string _CallerIdNumber;

        public string CallerIdNumber
        {
            get { return _CallerIdNumber; }
            set { _CallerIdNumber = value; }
        }


        private DateTime _CallerIdDateCall;

        public DateTime CallerIdDateCall
        {
            get { return _CallerIdDateCall; }
            set { _CallerIdDateCall = value; }
        }


        private string _CallerIdDoorNo;

        public string CallerIdDoorNo
        {
            get { return _CallerIdDoorNo; }
            set { _CallerIdDoorNo = value; }
        }
        private string _CallerIdAddress;

        public string CallerIdAddress
        {
            get { return _CallerIdAddress; }
            set { _CallerIdAddress = value; }
        }


        private string _CallerEmail;

        public string CallerEmail
        {
            get { return _CallerEmail; }
            set { _CallerEmail = value; }
        }



        private string _CallReferenceNo;

        public string CallReferenceNo
        {
            get { return _CallReferenceNo; }
            set { _CallReferenceNo = value; }
        }


     
        private int? _CalledToSubcompanyId;

        public int? CalledToSubcompanyId
        {
            get { return _CalledToSubcompanyId; }
            set { _CalledToSubcompanyId = value; }
        }


        private bool IsAccountCall;

        private int? AccountId;



        public bool ShowWindowOnFront;
        public int WindowPositionType;

        public string customerNotes;
        public string excludedDriversList;

        public CallerIdPopup(eCallerIdType callerIdType,string name,string phoneNumber,string doorNo,string address,string email, DateTime callDate,bool recvVoipFromFone,string callRefNo,bool IsAccount,int? accountId)
        {
        
           
            InitializeComponent();
            this.Load += new EventHandler(CallerIdPopup_Load);
            this.Shown += new EventHandler(CallerIdPopup_Shown);
            this.Resize += new EventHandler(CallerIdPopup_Resize);
          



            this.Tag = phoneNumber;
            this.callerId_Call = callerIdType;
            this.CallerIdName = name;
            this.CallerIdNumber = phoneNumber;
            this.CallerEmail = email;
            this.CallerIdDateCall = callDate;
            this.grdBookings.ViewCellFormatting += new CellFormattingEventHandler(MyGridView_ViewCellFormatting);
            this.grdBookings.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdBookings_ContextMenuOpening);

         
            this.CallerIdDoorNo = doorNo;
            this.CallerIdAddress = address;

            this.IsAccountCall = IsAccount;
            this.AccountId = accountId;

          
              this.CallReferenceNo = callRefNo; 

            if (!string.IsNullOrEmpty(doorNo))
            {
                address = address.Length > 27 ? "Add: " + address.Substring(0, 27) + "..." : "Add: " + address;

                txtDoorNo.Text = "Door No : " + doorNo;
            }
            else
            {
               
                txtDoorNo.Visible = false;

                txtAddress.Font = new Font("Tahoma", 11, FontStyle.Bold);
                txtAddress.Location = new Point(410, 8);
                address = address.Length > 22 ? "Add: " + address.Substring(0, 22) + "..." : "Add: " + address;

            }

            txtAddress.Text = address;




            if (AppVars.objPolicyConfiguration.CLIPopupAutoCloseInterval.ToInt() > 0)
            {
                timerAutoClose = new System.Windows.Forms.Timer();
                timerAutoClose.Interval = AppVars.objPolicyConfiguration.CLIPopupAutoCloseInterval.ToInt() * 1000;
                timerAutoClose.Enabled = true;
                timerAutoClose.Tick += new EventHandler(timerAutoClose_Tick);
                timerAutoClose.Start();
            }

        }

        void CallerIdPopup_Resize(object sender, EventArgs e)
        {
           
        }



        bool ClosePopup = false;
        public void StartAutoCloseTimer()
        {
            ClosePopup = true;

            

        }

        void timerAutoClose_Tick(object sender, EventArgs e)
        {
            if (ClosePopup)
            {
                this.Close();
            }
        }

        void grdBookings_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (objContextMenu == null)
            {
                objContextMenu = new RadDropDownMenu();
                objContextMenu.BackColor = Color.Orange;

                RadMenuItem EditFareItem1 = new RadMenuItem("Select as Pickup");
                EditFareItem1.ForeColor = Color.Black;
                EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);
                EditFareItem1.Click += new EventHandler(EditFareItem1_Click);
                objContextMenu.Items.Add(EditFareItem1);

                EditFareItem1 = new RadMenuItem("Select as Destination");
                EditFareItem1.ForeColor = Color.Black;
                EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);
                EditFareItem1.Click += new EventHandler(EditFareItem1_Click);
                objContextMenu.Items.Add(EditFareItem1);




                EditFareItem1 = new RadMenuItem("UnSelect All");
                EditFareItem1.ForeColor = Color.Black;
                EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);
                EditFareItem1.Click += new EventHandler(EditFareItem1_Click);
                objContextMenu.Items.Add(EditFareItem1);


               


            }

            if (grdBookings.CurrentColumn != null && (grdBookings.CurrentColumn.Name == "From" || grdBookings.CurrentColumn.Name == "To"))
                e.ContextMenu = objContextMenu;
        }

        void EditFareItem1_Click(object sender, EventArgs e)
        {

            try
            {
                if (grdBookings.CurrentColumn != null && grdBookings.CurrentRow != null && grdBookings.CurrentRow is GridViewDataRowInfo)
                {
                    RadMenuItem obj = (RadMenuItem)sender;

                    if (objIndividualBooking == null)
                        objIndividualBooking = new Booking();

                    if (obj.Text == "UnSelect All")
                    {
                        objIndividualBooking = null;




                    }

                    else if (obj.Text == "Select as Pickup")
                    {
                        if (grdBookings.CurrentColumn.Name == "FromAdd" || grdBookings.CurrentColumn.Name == "From")
                        {
                            objIndividualBooking.FromAddress = grdBookings.CurrentRow.Cells["From"].Value.ToStr();

                            objIndividualBooking.FromDoorNo = grdBookings.CurrentRow.Cells["FromDoorNo"].Value.ToStr();
                            objIndividualBooking.FromLocTypeId = grdBookings.CurrentRow.Cells["FromTypeId"].Value.ToIntorNull();
                            objIndividualBooking.FromLocId = grdBookings.CurrentRow.Cells["FromId"].Value.ToIntorNull();

                        }
                        else if (grdBookings.CurrentColumn.Name == "ToAdd" || grdBookings.CurrentColumn.Name == "To")
                        {
                            objIndividualBooking.FromAddress = grdBookings.CurrentRow.Cells["To"].Value.ToStr();
                            objIndividualBooking.FromDoorNo = grdBookings.CurrentRow.Cells["ToDoorNo"].Value.ToStr();


                            objIndividualBooking.FromLocTypeId = grdBookings.CurrentRow.Cells["ToTypeId"].Value.ToIntorNull();
                            objIndividualBooking.FromLocId = grdBookings.CurrentRow.Cells["ToId"].Value.ToIntorNull();

                        }

                    }
                    else if (obj.Text == "Select as Destination")
                    {
                        if (grdBookings.CurrentColumn.Name == "ToAdd" || grdBookings.CurrentColumn.Name == "To")
                        {
                            objIndividualBooking.ToAddress = grdBookings.CurrentRow.Cells["To"].Value.ToStr();
                            objIndividualBooking.ToDoorNo = grdBookings.CurrentRow.Cells["ToDoorNo"].Value.ToStr();


                            objIndividualBooking.ToLocTypeId = grdBookings.CurrentRow.Cells["ToTypeId"].Value.ToIntorNull();
                            objIndividualBooking.ToLocId = grdBookings.CurrentRow.Cells["ToId"].Value.ToIntorNull();

                        }
                        else if (grdBookings.CurrentColumn.Name == "FromAdd" || grdBookings.CurrentColumn.Name == "From")
                        {
                            objIndividualBooking.ToAddress = grdBookings.CurrentRow.Cells["From"].Value.ToStr();
                            objIndividualBooking.ToDoorNo = grdBookings.CurrentRow.Cells["FromDoorNo"].Value.ToStr();


                            objIndividualBooking.ToLocTypeId = grdBookings.CurrentRow.Cells["FromTypeId"].Value.ToIntorNull();
                            objIndividualBooking.ToLocId = grdBookings.CurrentRow.Cells["FromId"].Value.ToIntorNull();

                        }

                    }
                }
            }
            catch
            {


            }
        }



        void ViewBookingItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdWaitingBookings.CurrentRow != null && grdWaitingBookings.CurrentRow is GridViewDataRowInfo)
                {

                    long jobId = grdWaitingBookings.CurrentRow.Cells["Id"].Value.ToLong();

                    General.ShowBookingForm(jobId.ToInt(), true, "", "", Enums.BOOKING_TYPES.LOCAL);

                }
            }
            catch
            {


            }
        }

    
        void CallerIdPopup_Shown(object sender, EventArgs e)
        {
         //   new Thread(new ThreadStart(LoadCLIHistory)).Start();


            try
            {

                if (ShowWindowOnFront==false && Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmBooking")).Count() > 0)
                {

                    var frms = Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmBooking"));
                    foreach (Form frm in frms)
                    {

                        frm.BringToFront();
                        frm.Focus();
                        frm.Activate();
                    }

                }
                else
                {


                    this.BringToFront();

                    this.Focus();
                    grdBookings.Focus();
                }


                if (this.customerNotes.ToStr().Trim().Length > 0)
                {
                    grdBookings.Dock = DockStyle.Top;
                    grdBookings.Size = new Size(724, 227);

                    lblNotes.Visible = true;
                    lblNotes.Text = "Notes: " + this.customerNotes.ToStr().Trim();

                }




              //  timer1.Interval = 900000;
             //   timer1.Start();
            }
            catch 
            {


            }

           
        }


        private void LoadCLIHistory()
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UIDelegate(LoadCallHistory));

            }
            else
            {
                LoadCallHistory();

            }

        }


        private void FormatGrid()
        {

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = "Vehicle";
            col.HeaderText = "Vehicle";
            col.Width = 75;
            col.IsVisible = true;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "From";
            col.HeaderText = "Pickup Point";
            col.Width = 180;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "Via";
            col.HeaderText = "Via";
            col.Width = 40;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "ToType";
            col.HeaderText = "To Type";
            col.Width = 65;
            col.IsVisible = false;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "To";
            col.HeaderText = "Destination";
            col.Width = 180;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = "PickUpDate";
            col.HeaderText = "Pickup Date/Time";
            col.Width = 120;
            grdBookings.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            //col.Name = "Email";
            //col.IsVisible = false;
            //grdBookings.Columns.Add(col);
          



            GridViewCheckBoxColumn col2 = new GridViewCheckBoxColumn();
            col2.Width = 60;
            col2.HeaderText = "Reverse";
            col2.Name = "Reverse";
            grdBookings.Columns.Add(col2);
            
        }


        delegate void UIDelegate();
      
        private void LoadCallHistory()
        {

            try
            {

             

                using (TaxiDataContext db = new TaxiDataContext())
                {
                 


                    
                    db.CommandTimeout = 8;


                    var list = db.stp_getcustomerhistory(this.CallerIdNumber).ToList();



                    //var list = (from a in db.Bookings.Where(c => c.CustomerPhoneNo == this.CallerIdNumber || c.CustomerMobileNo == this.CallerIdNumber)
                    //            select new
                    //            {

                    //                PickUpDate = a.PickupDateTime,
                    //                FromTypeId = a.FromLocTypeId,
                    //                FromId = a.FromLocId,


                    //                From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                    //                To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,


                    //                FromAdd = a.FromAddress,
                    //                ToAdd = a.ToAddress,

                    //                ToId = a.ToLocId,
                    //                ToTypeId = a.ToLocTypeId,

                    //                Fare = a.FareRate,
                    //                BookingTypeId = a.BookingTypeId,
                    //                BookingStatusId = a.BookingStatusId,
                    //                CompanyId = a.CompanyId,
                    //                VehicleTypeId = a.VehicleTypeId,
                    //                ViaString = a.ViaString

                    //            })
                    //        .ToList();


                 




                    //var parentList = (from a in list
                    //                  group a by new
                    //                  {

                    //                      FromId = a.FromId,
                    //                      FromTypeId = a.FromTypeId,
                                       
                    //                      From = a.From,
                    //                      FromAdd = a.FromAdd,
                    //                      ToAdd = a.ToAdd,
                    //                      a.To,

                    //                      a.ToTypeId,
                    //                      a.ToId,

                    //                      BookingTypeId = a.BookingTypeId,
                    //                      a.CompanyId,
                    //                      a.VehicleTypeId,
                    //                      a.ViaString

                    //                  } into g

                    //                  let Pickup = g.Max(i => i.PickUpDate)
                    //                  let fareMax = g.Max(i => i.Fare)


                    //                  select new
                    //                  {

                    //                      PickUpDate = Pickup,
                    //                      g.Key.FromTypeId,
                    //                      g.Key.FromId,

                                         
                    //                      g.Key.From,
                    //                      g.Key.ViaString,
                    //                      g.Key.To,

                    //                      g.Key.FromAdd,
                    //                      g.Key.ToAdd,

                    //                      g.Key.ToId,
                    //                      g.Key.ToTypeId,

                    //                      Fare = fareMax,
                    //                      BookingTypeId = g.Key.BookingTypeId,
                    //                      g.Key.CompanyId,
                    //                      g.Key.VehicleTypeId


                    //                  })
                    //                .OrderByDescending(c => c.PickUpDate).Take(10);




                    this.grdBookings.DataSource = list;

                    grdBookings.EnableHotTracking = false;
                    grdBookings.AllowEditRow = true;

                   grdBookings.MasterTemplate.AllowEditRow = true;



                    GridViewTemplate temp = grdBookings.MasterTemplate;
                    if (temp.Columns.Count > 0)
                    {
                      //  grdBookings.AllowEditRow = false;
                     //   temp.Columns["BookingTypeId"].IsVisible = false;

                        temp.Columns["FromTypeId"].IsVisible = false;
                        temp.Columns["FromId"].IsVisible = false;
                        temp.Columns["ToId"].IsVisible = false;
                        temp.Columns["ToTypeId"].IsVisible = false;
                 //       temp.Columns["PickupDate"].IsVisible = false;
                        temp.Columns["VehicleTypeId"].IsVisible = false;
                        temp.Columns["ViaString"].Width = 120;
                        temp.Columns["ViaString"].HeaderText = "Via";
                     //   temp.Columns["FromAdd"].IsVisible = false;
                     //   temp.Columns["ToAdd"].IsVisible = false;
                        //      temp.Columns["Email"].IsVisible = false;

                        temp.Columns["CompanyId"].IsVisible = false;
                        temp.Columns["CancelledJobCount"].IsVisible = false;
                        temp.Columns["NoPickupJobCount"].IsVisible = false;


                      //  temp.Columns["BookingStatusId"].IsVisible = false;
                        temp.Columns["FromDoorNo"].IsVisible = false;
                        temp.Columns["ToDoorNo"].IsVisible = false;

                      
                        //    temp.Columns["FromType"].HeaderText = "From Type";
                        temp.Columns["From"].Width = 200;
                        temp.Columns["From"].ReadOnly = true;

                        temp.Columns["From"].HeaderText = "Pickup Point";
                        temp.Columns["To"].HeaderText = "Destination";
                        temp.Columns["To"].ReadOnly = true;


                        temp.Columns["ViaString"].ReadOnly = true;
                        temp.Columns["Fare"].ReadOnly = true;




                          //     temp.Columns["PickupDate"].IsVisible = false;
                        temp.Columns["PickUpDate"].Width = 100;
                        temp.Columns["PickUpDate"].HeaderText = "Date/Time";
                        (temp.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yy HH:mm";
                        (temp.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yy HH:mm}";






                        //  temp.Columns["ToType"].IsVisible = false;

                        //    temp.Columns["ToType"].Width = 65;
                        //    temp.Columns["ToType"].HeaderText = "To Type";
                        temp.Columns["To"].Width = 190;

                        GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();

                        col.Width = 55;
                        col.HeaderText = "Reverse";
                        col.Name = "Reverse";
                        temp.Columns.Add(col);


                     //   lblUsed.Visible = true;
                      //  lblCancelled.Visible = true;
                      //  lblNoFares.Visible = true;

                       // lblUsed.Text = "Used : " + list.Count();
                      //  lblCancelled.Text = "Cancelled : " + list.FirstOrDefault().DefaultIfEmpty().CancelledJobCount.ToInt();
                      //  lblNoFares.Text = "No Fares : " + list.FirstOrDefault().DefaultIfEmpty().NoPickupJobCount.ToInt();

                        GetCurrentJob();



                        ShowWaitingBookings(true);

                        grdBookings.Columns.Move(temp.Columns["PickUpDate"].Index, 0);
                    }
                    else
                    {

                        FormatGrid();

                    }
                }

               
            }
            catch(Exception ex)
            {
                try
                {
                    System.IO.File.AppendAllText("excep_calleridcatch.txt", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt") + ":" +  " ," + ex.Message + Environment.NewLine);
                }
                catch
                {


                }

            }
        }


        private string _TrackDriverNo;

        public string TrackDriverNo
        {
            get { return _TrackDriverNo; }
            set { _TrackDriverNo = value; }
        }





        long jobId = 0;

        private void GetCurrentJob()
        {
            try
            {

               string currJob=   (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).GetCurrentBookingByCustomer(this.CallerIdNumber);


               if (!string.IsNullOrEmpty(currJob))
               {
                  

                   if(currJob.Contains(">>>>"))
                   {

                       InitializeCurrentJobPanel();

                       string[] arr = currJob.Split(new string[] { ">>>>" }, StringSplitOptions.RemoveEmptyEntries);

                       lblCurrJobDetails.Text = arr[0].ToStr();

                       if (!string.IsNullOrEmpty(arr[1]))
                       {


                           this.TrackDriverNo = arr[1].ToStr();


                       }

                      jobId= arr[2].ToLong();

                   }

               }
            }
            catch 
            {



            }


        }

        private void InitializeCurrentJobPanel()
        {
            this.Size = new Size(970, 420);

            if (lblCurrJobHeading != null)
                return;


            this.lblCurrJobHeading = new System.Windows.Forms.Label();
            this.lblCurrJobDetails = new System.Windows.Forms.Label();
            this.btnTrackDriver = new System.Windows.Forms.Button();

            this.chkShowETA = new System.Windows.Forms.CheckBox();
            this.lblETA = new System.Windows.Forms.Label();

            this.pnlGrid.Controls.Add(this.btnTrackDriver);
            this.pnlGrid.Controls.Add(this.lblCurrJobDetails);
            this.pnlGrid.Controls.Add(this.lblCurrJobHeading);

            this.pnlGrid.Controls.Add(this.lblETA);
            this.pnlGrid.Controls.Add(this.chkShowETA);


            // 
            // lblCurrJobHeading
            // 
            this.lblCurrJobHeading.BackColor = System.Drawing.Color.Yellow;
            this.lblCurrJobHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrJobHeading.Location = new System.Drawing.Point(745, 0);
            this.lblCurrJobHeading.Name = "lblCurrJobHeading";
            this.lblCurrJobHeading.Size = new System.Drawing.Size(220, 24);
            this.lblCurrJobHeading.TabIndex = 10;
            this.lblCurrJobHeading.Text = "Current Job";
            this.lblCurrJobHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrJobDetails
            // 
            this.lblCurrJobDetails.BackColor = System.Drawing.Color.AliceBlue;
            this.lblCurrJobDetails.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrJobDetails.Location = new System.Drawing.Point(745, 26);
            this.lblCurrJobDetails.Name = "lblCurrJobDetails";
            this.lblCurrJobDetails.Size = new System.Drawing.Size(220, 260);
            this.lblCurrJobDetails.TabIndex = 11;







            // 
            // chkShowETA
            // 
            this.chkShowETA.AutoSize = true;
            this.chkShowETA.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowETA.ForeColor = System.Drawing.Color.Black;
            this.chkShowETA.Location = new System.Drawing.Point(748, 291);
            this.chkShowETA.Name = "chkShowETA";
            this.chkShowETA.Size = new System.Drawing.Size(108, 18);
            this.chkShowETA.TabIndex = 13;
            this.chkShowETA.Text = "Show ETA";
            this.chkShowETA.UseVisualStyleBackColor = true;
            this.chkShowETA.CheckedChanged += new System.EventHandler(this.chkShowETA_CheckedChanged);
            this.chkShowETA.Visible = true;
            this.chkShowETA.BringToFront();
            // 
            // lblETA
            // 
            this.lblETA.BackColor = System.Drawing.Color.Red;
            this.lblETA.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblETA.ForeColor = System.Drawing.Color.White;
            this.lblETA.Location = new System.Drawing.Point(748, 262);
            this.lblETA.Name = "lblETA";
            this.lblETA.Size = new System.Drawing.Size(216, 22);
            this.lblETA.TabIndex = 14;
            this.lblETA.Text = "1 HOUR 10 MINS";
            this.lblETA.Visible = false;






            // 
            // btnTrackDriver
            // 
            this.btnTrackDriver.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrackDriver.Location = new System.Drawing.Point(840, 281);
            this.btnTrackDriver.Name = "btnTrackDriver";
            this.btnTrackDriver.Size = new System.Drawing.Size(110, 30);
            this.btnTrackDriver.TabIndex = 12;
            this.btnTrackDriver.Text = "Track Driver";
            this.btnTrackDriver.UseVisualStyleBackColor = true;
            this.btnTrackDriver.Click += new System.EventHandler(this.btnTrackDriver_Click);
   

        }

    

        void CallerIdPopup_Load(object sender, EventArgs e)
        {
            try
            {
            

                this.FormClosing += new FormClosingEventHandler(CallerIdPopup_FormClosing);
                //if (this.WindowPositionType == 0)
                //{
                //    this.Top = this._top;
                //    this.Left = this._left + 5;
                //}
                //else
                //{
                //    this.StartPosition = FormStartPosition.CenterScreen;
                //}

                if (this.WindowPositionType == 2)
                {
                    this.Top = this.Top + 80;
                }
                else
                {
                    if (this.WindowPositionType == 0)
                    {
                        this.Top = this._top;
                        this.Left = this._left + 5;
                    }

                }
                lblPopuptext.Text = this._popupText;


                if (!string.IsNullOrEmpty(this.CallerIdName))
                {
                   
                    new Thread(new ThreadStart(LoadCLIHistory)).Start();


                    
                }
                else
                {

                    FormatGrid();
                }


          
               


                if (this.lblPopuptext.BackColor == Color.LightGray)
                {
                    txtAddress.BackColor = Color.Red;
                    txtDoorNo.BackColor = Color.Red;
                    lblPopuptext.BackColor = Color.Red;
                    pictureBox1.BackColor = Color.Red;

                    lblPopuptext.ForeColor = Color.White;
                    txtAddress.ForeColor = Color.White;
                    txtDoorNo.ForeColor = Color.White;

                    lblBlackListReason.ForeColor = Color.White;
                    lblBlackListReason.BackColor = Color.Red;
                    lblBlackListReason.Visible = true;
                    lblBlackListReason.Text =  this.BlackListReason;
                    //pnlBottom.Visible = false;

                }
            }
            catch 
            {


            }
        }

   

        void CallerIdPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearCall();


            if (AppVars.objPolicyConfiguration.CLIPopupAutoCloseInterval.ToInt() > 0)
            {



                new BroadcasterData().BroadCastToAll("**internalmessage>>" + "close clipopup" + ">>" + this.CallerIdNumber + ">>" + Environment.MachineName.ToLower());
            }

        }

      
   


        Font oldFont = new Font("Tahoma", 9, FontStyle.Regular);
        Font newFont = new Font("Tahoma", 8, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }



        void MyGridView_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {

            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = _HeaderRowBackColor;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }

            else if (e.CellElement is GridDataCellElement)
            {

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;

                if (e.CellElement.RowElement.IsSelected == true)
                {

                    e.CellElement.RowElement.NumberOfColors = 1;
                    e.CellElement.RowElement.BackColor = Color.DeepSkyBlue;

                    //   e.CellElement.RowElement.ForeColor = Color.White;


                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.DeepSkyBlue;

                    e.CellElement.ForeColor = Color.White;

                    e.CellElement.Font = newFont;


                }

                else
                {
                    e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.All);

                }

            }


        }

      
        public object PopupText
        {
            get
            {
                return lblPopuptext.Text;
            }
            set
            {
                this._popupText = (string)(value);
                this.Text =" ";
            }
        }

        public object Position
        {
            set
            {
                this._left = 0;
                this._top = (lblPopuptext.Height * System.Convert.ToInt32(value) + (5 * System.Convert.ToInt32(value))) + 40;

              //  AppVars.LastPopupTop = this.Height;
            }
        }
        private int _top;
        private int _left;
        private string _popupText;
      
     
      

        

        private void ClearCall()
        {
          //  AppVars.LineNo = string.Empty;
            AppVars.openedPhoneNo = string.Empty;

        }



        Booking objIndividualBooking = null;
      
        private void btnSelect_Click(object sender, EventArgs e)
        {

            SelectBooking();          
          
        }


        private void SelectBooking()
        {


           

                if (grdBookings.Rows.Count == 0)
                {
                    ShowBookingForm(0, false, this.CallerIdName, this.CallerIdNumber, this.BookingTypeId);
                }
                else
                {


                    GridViewRowInfo row = grdBookings.CurrentRow;
                    if (row != null && row is GridViewDataRowInfo)
                    {


                        if (objIndividualBooking == null)
                        {

                            this.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;


                            int? fromLocTypeId = row.Cells["FromTypeId"].Value.ToIntorNull();
                            int? fromLocId = row.Cells["FromId"].Value.ToIntorNull();
                            string from = row.Cells["From"].Value.ToStr();

                            string fromDoor = row.Cells["FromDoorNo"].Value.ToStr();
                            //if (fromDoor.Contains(" - "))
                            //    fromDoor = fromDoor.Substring(0, fromDoor.IndexOf(" - "));
                            //else
                            //    fromDoor = string.Empty;

                            string toDoor = row.Cells["ToDoorNo"].Value.ToStr();
                            if (toDoor.Contains(" - "))
                                toDoor = toDoor.Substring(0, toDoor.IndexOf(" - "));
                            else
                                toDoor = string.Empty;


                            int? toLocTypeId = row.Cells["ToTypeId"].Value.ToIntorNull();
                            int? toLocId = row.Cells["ToId"].Value.ToIntorNull();
                            string to = row.Cells["To"].Value.ToStr();
                            bool IsReverse = row.Cells["Reverse"].Value.ToBool();


                            decimal fare = row.Cells["Fare"].Value.ToDecimal();

                            int? companyId = row.Cells["CompanyId"].Value.ToIntorNull();






                            ShowBookingForm(0, false, this.CallerIdName, this.CallerIdNumber, fromLocTypeId, toLocTypeId,
                                                              fromLocId, toLocId, from, to, fare, IsReverse, fromDoor, toDoor, BookingTypeId, companyId, this.CallerEmail
                                                              ,row.Cells["VehicleTypeId"].Value.ToInt(),row.Cells["ViaString"].Value.ToStr());

                        }
                        else
                        {

                            this.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;

                            int? fromLocTypeId = objIndividualBooking.FromLocTypeId.ToIntorNull();
                            int? fromLocId = objIndividualBooking.FromLocId.ToIntorNull();
                            string from = objIndividualBooking.FromAddress.ToStr().ToUpper().Trim();

                            string fromDoor = objIndividualBooking.FromDoorNo.ToStr().Trim();

                            if (fromDoor.Contains(" - "))
                                fromDoor = fromDoor.Substring(0, fromDoor.IndexOf(" - "));
                            else
                                fromDoor = string.Empty;


                          


                            int? toLocTypeId = objIndividualBooking.ToLocTypeId.ToIntorNull();
                            int? toLocId = objIndividualBooking.ToLocId.ToIntorNull();
                            string to = objIndividualBooking.ToAddress.ToStr().ToUpper().Trim();

                            string toDoor = objIndividualBooking.ToDoorNo.ToStr().Trim();
                            if (toDoor.Contains(" - "))
                                toDoor = toDoor.Substring(0, toDoor.IndexOf(" - "));
                            else
                                toDoor = string.Empty;
                            bool IsReverse = false;



                            if (from.Length == 0)
                            {
                                fromLocTypeId = row.Cells["FromTypeId"].Value.ToIntorNull();
                                fromLocId = row.Cells["FromId"].Value.ToIntorNull();

                                 from = row.Cells["From"].Value.ToStr();

                                 fromDoor = row.Cells["FromDoorNo"].Value.ToStr();
                                 //if (fromDoor.Contains(" - "))
                                 //    fromDoor = fromDoor.Substring(0, fromDoor.IndexOf(" - "));
                                 //else
                                 //    fromDoor = string.Empty;

                              

                            }

                            if (to.Length == 0)
                            {

                                 toDoor = row.Cells["ToDoorNo"].Value.ToStr();
                                //if (toDoor.Contains(" - "))
                                //    toDoor = toDoor.Substring(0, toDoor.IndexOf(" - "));
                                //else
                                //    toDoor = string.Empty;


                                 toLocTypeId = row.Cells["ToTypeId"].Value.ToIntorNull();
                                 toLocId = row.Cells["ToId"].Value.ToIntorNull();
                                  to = row.Cells["To"].Value.ToStr();


                            }


                            decimal fare = row.Cells["Fare"].Value.ToDecimal();

                            int? companyId = row.Cells["CompanyId"].Value.ToIntorNull();






                            ShowBookingForm(0, false, this.CallerIdName, this.CallerIdNumber, fromLocTypeId, toLocTypeId,
                                                              fromLocId, toLocId, from, to, fare, IsReverse, fromDoor, toDoor, BookingTypeId, companyId, this.CallerEmail
                                                              , row.Cells["VehicleTypeId"].Value.ToInt(), row.Cells["ViaString"].Value.ToStr());



                        }



                    }
                }


                CLICall();

                this.Close();
           

        }






        private void ClearCLICall()
        {

            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    db.stp_AddUserLogs(AppVars.LoginObj.LuserId.ToInt(), "CLI CLEARED", 1);

                }
            }
            catch
            {


            }

        }


        private void CLICall()
        {

            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    db.stp_AddUserLogs(AppVars.LoginObj.LuserId.ToInt(), "CLI CALL", 2);

                }
            }
            catch
            {


            }

        }

   
  

        private void btnNewBooking_Click(object sender, EventArgs e)
        {
            NewBooking();
        }


        private void NewBooking()
        {

            ShowBookingForm(0, false, this.CallerIdName, this.CallerIdNumber, this.CallerIdDoorNo, this.CallerIdAddress, this.BookingTypeId);

            CLICall();

            this.Close();
        }

       

      

        

        private void CallerIdPopup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {

                    NewBooking();
                }
                else if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
                {
                    if(grdBookings.Focused == false)
                       grdBookings.Focus();

                    if (grdBookings.RowCount > 0 && grdBookings.CurrentRow == null)
                    {
                        if (e.KeyCode == Keys.Up)
                            grdBookings.CurrentRow = grdBookings.Rows[grdBookings.Rows.Count - 1];
                        else if (e.KeyCode == Keys.Down)
                            grdBookings.CurrentRow = grdBookings.Rows[0];

                    }


                }
                else if (e.KeyCode == Keys.Escape)
                {
                    Close();

                }
               


              
            
            }
            catch 
            {


            }
        }

        private void grdBookings_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SelectBooking();

                }
            }

            catch
            {


            }
        }

        private void btnTrackDriver_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TrackDriverNo))
            {

                TrackDriver();

            }
        }


        rptJobRouthPathGoogle rpt = null;

        private void TrackDriver()
        {
            try
            {
            
                if (string.IsNullOrEmpty(AppVars.objPolicyConfiguration.ListenerIP.ToStr()))
                {
                    ENUtils.ShowMessage("Server IP is not configured in Settings.");
                    return;
                }


                if (AppVars.objPolicyConfiguration.TrackDriverType.ToBool() == true)
                {
                    if (jobId > 0)
                    {

                        string driverNo = General.GetObject<Booking>(c => c.Id == jobId).DefaultIfEmpty().Fleet_Driver.DefaultIfEmpty().DriverNo.ToStr().Trim();

                        if (driverNo.Length > 0)
                        {

                            Thread smsThread = new Thread(delegate()
                            {
                                new BroadcasterData().BroadCastToPort("**track driver=" + driverNo + "=" + Environment.MachineName, 3530);

                            });

                            smsThread.Start();

                            System.Threading.Thread.Sleep(1000);
                        }
                    }

                }
                else
                {

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
                        pp.StartInfo.Arguments += " " + "rptJobRouthPathGoogle" + " " + jobId + " " + "true" + " " + 0;
                        pp.Start();
                        Thread.Sleep(500);
                    }
                    else
                    {

                        if (rpt != null && rpt.IsDisposed == false)
                        {
                            rpt.BringToFront();
                            rpt.Show();
                        }
                        else
                        {

                            rpt = new rptJobRouthPathGoogle(General.GetObject<Booking>(c => c.Id == jobId), true);
                            rpt.Show();


                        }
                    }

                }

              

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }


       

        System.Windows.Forms.Timer timerAutoClose = null;

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (AppVars.objPolicyConfiguration.EnableBidding.ToBool()==false && jobId==0)
        //    {

        //        this.Close();
        //    }

        //}

        private void btnRecentBookings_Click(object sender, EventArgs e)
        {
            ShowWaitingBookings(false);
        }

        private void FormatWaitingBookings()
        {
            if (grdWaitingBookings.Columns.Count == 0)
            {

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.Name = "Id";
                col.IsVisible = false;
                grdWaitingBookings.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.Name = "RefNo";
                col.HeaderText = "Ref No";
                col.Width = 120;
                col.ReadOnly = true;
                grdWaitingBookings.Columns.Add(col);



                GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
                colDt.Name = "PickUpDate";
                colDt.HeaderText = "Pickup Date";
                colDt.Width = 120;
                colDt.CustomFormat = "dd/MM/yyyy";
                colDt.FormatString = "{0:dd/MM/yyyy}";
                grdWaitingBookings.Columns.Add(colDt);


                colDt = new GridViewDateTimeColumn();
                colDt.Name = "PickUpTime";
                colDt.HeaderText = "Time";
                colDt.Width = 60;
                colDt.CustomFormat = "HH:mm";
                colDt.FormatString = "{0:HH:mm}";
                grdWaitingBookings.Columns.Add(colDt);



                col = new GridViewTextBoxColumn();
                col.Name = "From";
                col.HeaderText = "From";
                col.Width = 150;
                col.ReadOnly = true;
                grdWaitingBookings.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.Name = "To";
                col.HeaderText = "Destination";
                col.Width = 150;
                col.ReadOnly = true;
                grdWaitingBookings.Columns.Add(col);



                col = new GridViewTextBoxColumn();
                col.Name = "IsQuotation";
                col.HeaderText = "IsQuotation";
                col.Width = 150;
             //   col.ReadOnly = true;
                col.IsVisible = false;
                grdWaitingBookings.Columns.Add(col);


                AddCommandColumn(grdWaitingBookings, "btnUpdate", "Update");

                grdWaitingBookings.ShowGroupPanel = false;
                grdWaitingBookings.AllowAddNewRow = false;
                grdWaitingBookings.AllowDeleteRow = false;



                //col = new GridViewTextBoxColumn();
                //col.Name = "Email";
                //col.IsVisible = false;
                //grdBookings.Columns.Add(col);




                if (jobId > 0)
                {
                    grdWaitingBookings.Size = new Size(this.Size.Width - 10, grdWaitingBookings.Size.Height);
                    grdWaitingBookings.Columns["From"].Width = 260;
                    grdWaitingBookings.Columns["To"].Width = 260;
                    grdWaitingBookings.Columns["PickUpTime"].Width = 70;

                    grdWaitingBookings.Columns["PickUpDate"].Width = 130;

                }
                else
                {
                    grdWaitingBookings.Columns["From"].Width = 170;
                    grdWaitingBookings.Columns["To"].Width = 170;

                }



                this.Size = new Size(this.Size.Width, 641);

                lblWaitingBookings.Visible = true;

                grdWaitingBookings.Visible = true;

            }
        }


      

        private void ShowWaitingBookings(bool openByDefault)
        {

            try
            {


                    List<Booking> list = (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).GetWaitingBookingsOfCustomer(this.CallerIdNumber);

                    if (openByDefault && list.Count == 0)
                        return;



                    if (lblWaitingBookings == null)
                    {
                        this.lblWaitingBookings = new System.Windows.Forms.Label();
                        this.grdWaitingBookings = new Telerik.WinControls.UI.RadGridView();

                        this.grdWaitingBookings.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdWaitingBookings_ContextMenuOpening);

                        this.pnlGrid.Controls.Add(this.grdWaitingBookings);
                        this.pnlGrid.Controls.Add(this.lblWaitingBookings);


                        ((System.ComponentModel.ISupportInitialize)(this.grdWaitingBookings)).BeginInit();
                        ((System.ComponentModel.ISupportInitialize)(this.grdWaitingBookings.MasterTemplate)).BeginInit();


                        // 
                        // lblWaitingBookings
                        // 
                        this.lblWaitingBookings.BackColor = System.Drawing.Color.Yellow;
                        this.lblWaitingBookings.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
                        this.lblWaitingBookings.Location = new System.Drawing.Point(0, 316);
                        this.lblWaitingBookings.Name = "lblWaitingBookings";
                        this.lblWaitingBookings.Size = new System.Drawing.Size(742, 22);
                        this.lblWaitingBookings.TabIndex = 10;
                        this.lblWaitingBookings.Text = "Waiting Bookings";
                        this.lblWaitingBookings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.lblWaitingBookings.Visible = false;
                        // 
                        // grdWaitingBookings
                        // 
                        this.grdWaitingBookings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.grdWaitingBookings.Location = new System.Drawing.Point(0, 339);
                        this.grdWaitingBookings.Name = "grdWaitingBookings";
                        this.grdWaitingBookings.Size = new System.Drawing.Size(745, 183);
                        this.grdWaitingBookings.TabIndex = 11;
                        this.grdWaitingBookings.Text = "radGridView1";
                        this.grdWaitingBookings.Visible = false;


                        ((System.ComponentModel.ISupportInitialize)(this.grdWaitingBookings.MasterTemplate)).EndInit();
                        ((System.ComponentModel.ISupportInitialize)(this.grdWaitingBookings)).EndInit();

                    }
                   
                        FormatWaitingBookings();
                   
           

                        grdWaitingBookings.RowCount = list.Count;

                        for (int i = 0; i < list.Count; i++)
                        {
                            grdWaitingBookings.Rows[i].Cells["Id"].Value = list[i].Id;
                            grdWaitingBookings.Rows[i].Cells["RefNo"].Value = list[i].BookingNo;
                            grdWaitingBookings.Rows[i].Cells["From"].Value = list[i].FromAddress;
                            grdWaitingBookings.Rows[i].Cells["To"].Value = list[i].ToAddress;
                            grdWaitingBookings.Rows[i].Cells["PickupDate"].Value = list[i].PickupDateTime;
                            grdWaitingBookings.Rows[i].Cells["PickupTime"].Value = list[i].PickupDateTime;

                            grdWaitingBookings.Rows[i].Cells["PickupDate"].Style.BackColor = Color.White;
                            grdWaitingBookings.Rows[i].Cells["PickupTime"].Style.BackColor = Color.White;

                            grdWaitingBookings.Rows[i].Cells["PickupDate"].Style.ForeColor = Color.Red;
                            grdWaitingBookings.Rows[i].Cells["PickupTime"].Style.ForeColor = Color.Red;
                            grdWaitingBookings.Rows[i].Cells["PickupDate"].Style.Font = new Font("Tahoma", 9, FontStyle.Bold);

                            grdWaitingBookings.Rows[i].Cells["PickupTime"].Style.Font = new Font("Tahoma", 9, FontStyle.Bold);

                            grdWaitingBookings.Rows[i].Cells["PickupDate"].Style.CustomizeFill = true;
                            grdWaitingBookings.Rows[i].Cells["PickupTime"].Style.CustomizeFill = true;
                            grdWaitingBookings.Rows[i].Cells["IsQuotation"].Value = list[i].IsQuotation.ToBool();

                       
                            if (grdWaitingBookings.Rows[i].Cells["IsQuotation"].Value.ToBool())
                            {
                                grdWaitingBookings.Rows[i].Cells["RefNo"].Style.CustomizeFill = true;
                                grdWaitingBookings.Rows[i].Cells["RefNo"].Style.BackColor = Color.Orange;

                                grdWaitingBookings.Rows[i].Cells["RefNo"].Value = list[i].BookingNo+ "[Quotation]";


                            }

                        }



                      
                  
                
            }
            catch 
            {


            }
        }


        RadDropDownMenu objWaitingContextMenu = null;
        void grdWaitingBookings_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (objWaitingContextMenu == null)
            {
                objWaitingContextMenu = new RadDropDownMenu();
                objWaitingContextMenu.BackColor = Color.Orange;

                RadMenuItem EditFareItem1 = new RadMenuItem("View Booking");
                EditFareItem1.ForeColor = Color.Black;
                EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);
                EditFareItem1.Click += new EventHandler(ViewBookingItem_Click);
                objWaitingContextMenu.Items.Add(EditFareItem1);

            }
            e.ContextMenu = objWaitingContextMenu;
        }

        private void AddCommandColumn(RadGridView grid, string colName, string caption)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 60;

            col.Name = colName;
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = caption;
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);
            grid.CommandCellClick+=new CommandCellClickEventHandler(grid_CommandCellClick);
        }



        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            try
            {


                if (grdWaitingBookings.CurrentRow == null || grdWaitingBookings.CurrentRow is GridViewDataRowInfo)
                {

                  
                    DateTime? pickupDate = grdWaitingBookings.CurrentRow.Cells["PickupDate"].Value.ToDateorNull();
                    DateTime? pickupTime = grdWaitingBookings.CurrentRow.Cells["PickupTime"].Value.ToDateTimeorNull();



                    if (pickupDate == null || pickupTime == null)
                    {
                        MessageBox.Show("Pickup Date or Time cannot be Empty");
                        return;
                    }

                    long jobId = grdWaitingBookings.CurrentRow.Cells["Id"].Value.ToLong();

                    BookingBO objBooking = new BookingBO();
                    objBooking.GetByPrimaryKey(jobId);


                    if (objBooking.Current != null)
                    {
                        try
                        {

                            DateTime? OldPickupDateTime = objBooking.Current.PickupDateTime;                          
                         
                        
                            objBooking.Current.PickupDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", pickupDate + pickupTime.Value.TimeOfDay).ToDateTime();

                            string Get = "";
                            string old = "";

                            if (OldPickupDateTime != objBooking.Current.PickupDateTime)
                            {
                                Get += " PickupDate/Time: " + string.Format("{0:dd/MM/yyyy HH:mm}", objBooking.Current.PickupDateTime) + ": ";
                                old += " PickupDate/Time: " + string.Format("{0:dd/MM/yyyy HH:mm}", OldPickupDateTime.Value) + ": ";
                            }

                            var NewRec = Get.TrimEnd(':', ' ', '\n');
                            var oldRec = old.TrimEnd(':', ' ', '\n');

                            if (NewRec != "" && old != "")
                            {
                                int? LoginID = AppVars.LoginObj.LgroupId.ToInt();
                                long BokingID = objBooking.PrimaryKeyValue.ToLong();
                                DateTime now = DateTime.Now.ToDateTime();

                                objBooking.Current.Booking_Logs.Add(new Booking_Log { BookingId = BokingID, User = AppVars.LoginObj.LoginName, BeforeUpdate = oldRec, AfterUpdate = NewRec, UpdateDate = now });


                            }

                            objBooking.CheckCustomerValidation = false;
                            objBooking.CheckDataValidation = false;

                            objBooking.Save();


                            (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshTodayAndPreData();
                        }
                        catch (Exception ex)
                        {
                            ENUtils.ShowMessage(ex.Message);


                        }
                    }
                }

               
               
             
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void ShowBookingForm(int id, bool showOnDialog, string name, string phone, int? bookingTypeId)
        {

            try
            {

                if (phone.ToStr().Trim().ToLower() == "anonymous")
                    phone = string.Empty;



                if (File.Exists(Application.StartupPath + "\\Booking\\TreasureBooking.exe"))
                {
                    try
                    {
                        FillDataset();



                        var s = Newtonsoft.Json.JsonConvert.SerializeObject(Program.dtCombos, Newtonsoft.Json.Formatting.Indented);

                        s = s.Replace(" ", "").Trim();

                        s = s.Replace(Environment.NewLine, "").Trim();
                        s = s.Replace("\"", "*");


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


                        polObj.CallerId_PhoneNo = phone;
                        polObj.CallerId_CustomerName = name;

                        polObj.CallerId_IsAccountCall = IsAccountCall;

                        polObj.CallerId_CompanyId = this.AccountId;

                        polObj.CallerId_SubCompanyId = this.CalledToSubcompanyId;
                        polObj.CanTransferJob = AppVars.CanTransferJob;
                        polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                        polObj.PermanentNotes = this.customerNotes;
                        polObj.ExcludedDrivers = excludedDriversList;
                        string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");

                        Process pp = new Process();
                        pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                        pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");

                        pp.Start();
                    }
                    catch (Exception ex)
                    {
                        frmBooking frm = new frmBooking(name, phone, this.AccountId, this.IsAccountCall);
                        frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                        frm.PickBookingTypeId = bookingTypeId;

                        frm.PickSubCompanyId = this.CalledToSubcompanyId;
                        frm.Tag = "cli";
                        frm.CustomerPermanentNotes = this.customerNotes;
                        if (id != 0)
                        {
                            frm.OnDisplayRecord(id);
                        }
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.MaximizeBox = false;

                        if (showOnDialog)
                        {
                            frm.ShowDialog();
                        }
                        else
                            frm.Show();


                        File.AppendAllText(Application.StartupPath + "\\exception_openingexebooking.txt", "Constructor2 :" + ex.Message);
                    }

                }
                else
                {
                    if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
                    {
                        frmBooking2 frm = new frmBooking2(name, phone, this.AccountId, this.IsAccountCall);
                        frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                        frm.PickBookingTypeId = bookingTypeId;

                        frm.PickSubCompanyId = this.CalledToSubcompanyId;
                        frm.Tag = "cli";

                        if (excludedDriversList.ToStr().Trim().Length > 0)
                        {
                            frm.btnExcludeDrivers.Tag = excludedDriversList.ToStr().Trim();

                        }

                        frm.CustomerPermanentNotes = this.customerNotes;
                        if (id != 0)
                        {
                            frm.OnDisplayRecord(id);
                        }
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.MaximizeBox = false;

                        if (showOnDialog)
                        {
                            frm.ShowDialog();
                        }
                        else
                            frm.Show();


                    }
                    else
                    {

                        frmBooking frm = new frmBooking(name, phone, this.AccountId, this.IsAccountCall);
                        frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                        frm.PickBookingTypeId = bookingTypeId;

                        frm.PickSubCompanyId = this.CalledToSubcompanyId;
                        frm.Tag = "cli";

                        if (excludedDriversList.ToStr().Trim().Length > 0)
                        {
                            frm.btnExcludeDrivers.Tag = excludedDriversList.ToStr().Trim();

                        }

                        frm.CustomerPermanentNotes = this.customerNotes;
                        if (id != 0)
                        {
                            frm.OnDisplayRecord(id);
                        }
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.MaximizeBox = false;

                        if (showOnDialog)
                        {
                            frm.ShowDialog();
                        }
                        else
                            frm.Show();

                    }



                }



            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }


        private void ShowBookingForm(int id, bool showOnDialog, string name, string phone, string doorNo, string address, int? bookingTypeId)
        {
            if (phone.ToStr().Trim().ToLower() == "anonymous")
                phone = string.Empty;


          



            if (File.Exists(Application.StartupPath + "\\Booking\\TreasureBooking.exe"))
            {

                try
                {
                    FillDataset();



                    var s = Newtonsoft.Json.JsonConvert.SerializeObject(Program.dtCombos, Newtonsoft.Json.Formatting.Indented);

                    s = s.Replace(" ", "").Trim();

                    s = s.Replace(Environment.NewLine, "").Trim();
                    s = s.Replace("\"", "*");


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




                    polObj.CallerId_PhoneNo = phone;
                    polObj.CallerId_CustomerName = name;
                    polObj.CallerId_From = address;

                    polObj.CallerId_CompanyId = AccountId;
                    polObj.CallerId_IsAccountCall = IsAccountCall;

                    polObj.CallerId_SubCompanyId = this.CalledToSubcompanyId;
                    polObj.CanTransferJob = AppVars.CanTransferJob;
                    polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                    polObj.PermanentNotes = this.customerNotes;
                    polObj.ExcludedDrivers = excludedDriversList;

                    string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");

                    Process pp = new Process();
                    pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                    pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");

                    pp.Start();
                }
                catch (Exception ex)
                {

                    frmBooking frm = new frmBooking(name, phone, doorNo, address, AccountId, IsAccountCall);
                    frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                    frm.PickBookingTypeId = bookingTypeId;
                    frm.PickSubCompanyId = this.CalledToSubcompanyId;
                    frm.Tag = "cli";
                    frm.CustomerPermanentNotes = this.customerNotes;
                    if (id != 0)
                    {
                        frm.OnDisplayRecord(id);
                    }
                    frm.ControlBox = true;
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.MaximizeBox = false;

                    if (showOnDialog)
                    {
                        frm.ShowDialog();
                    }
                    else
                        frm.Show();


                    File.AppendAllText(Application.StartupPath + "\\exception_openingexebooking.txt", "Constructor2 :" + ex.Message);


                }

            }
            else
            {

                if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
                {
                    frmBooking2 frm = new frmBooking2(name, phone, doorNo, address, AccountId, IsAccountCall);
                    frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                    frm.PickBookingTypeId = bookingTypeId;
                    frm.PickSubCompanyId = this.CalledToSubcompanyId;
                    frm.Tag = "cli";

                    if (excludedDriversList.ToStr().Trim().Length > 0)
                    {
                        frm.btnExcludeDrivers.Tag = excludedDriversList.ToStr().Trim();

                    }

                    frm.CustomerPermanentNotes = this.customerNotes;
                    if (id != 0)
                    {
                        frm.OnDisplayRecord(id);
                    }
                    frm.ControlBox = true;
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.MaximizeBox = false;

                    if (showOnDialog)
                    {
                        frm.ShowDialog();
                    }
                    else
                        frm.Show();

                }
                else
                {


                    frmBooking frm = new frmBooking(name, phone, doorNo, address, AccountId, IsAccountCall);
                    frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                    frm.PickBookingTypeId = bookingTypeId;
                    frm.PickSubCompanyId = this.CalledToSubcompanyId;
                    frm.Tag = "cli";

                    if (excludedDriversList.ToStr().Trim().Length > 0)
                    {
                        frm.btnExcludeDrivers.Tag = excludedDriversList.ToStr().Trim();

                    }

                    frm.CustomerPermanentNotes = this.customerNotes;
                    if (id != 0)
                    {
                        frm.OnDisplayRecord(id);
                    }
                    frm.ControlBox = true;
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.MaximizeBox = false;

                    if (showOnDialog)
                    {
                        frm.ShowDialog();
                    }
                    else
                        frm.Show();

                }





            }



        }


      


        private void ShowBookingForm(int id, bool showOnDialog, string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                                          int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, string fromdoorNo, string toDoorNo, int? bookingTypeId,int? companyId
            , string email,int vehicleTypeId,string viaString)
        {






            if (phone.ToStr().Trim().ToLower() == "anonymous")
                phone = string.Empty;





            if (File.Exists(Application.StartupPath + "\\Booking\\TreasureBooking.exe"))
            {

                try
                {

                    FillDataset();

                    var s = Newtonsoft.Json.JsonConvert.SerializeObject(Program.dtCombos, Newtonsoft.Json.Formatting.Indented);

                    s = s.Replace(" ", "").Trim();

                    s = s.Replace(Environment.NewLine, "").Trim();
                    s = s.Replace("\"", "*");


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


                    polObj.CallerId_PhoneNo = phone;
                    polObj.CallerId_CustomerName = name;
                    polObj.CallerId_From = fromAddress;
                    polObj.CallerId_To = toAddress;
                    polObj.CallerId_ViaString = viaString;
                    polObj.CallerId_FromDoorNo = fromdoorNo;
                    polObj.CallerId_ToDoorNo = toDoorNo;

                    polObj.CallerId_IsAccountCall = IsAccountCall;
                    polObj.CallerId_CompanyId = companyId;
                    polObj.CallerId_Fares = fare;
                    polObj.CallerId_fromLocTypeId = fromLocTypeId;
                    polObj.CallerId_FromLocId = fromLocId;
                    polObj.CallerId_ToLocId = toLocId;
                    polObj.CallerId_toLocTypeId = toLocTypeId;
                    polObj.CallerId_SubCompanyId = this.CalledToSubcompanyId;
                    polObj.CallerId_IsReverse = IsReverse;
                    polObj.ExcludedDrivers = excludedDriversList;
                    polObj.CanTransferJob = AppVars.CanTransferJob;

                    polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                    polObj.PermanentNotes = this.customerNotes;

                    string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");

                    Process pp = new Process();
                    pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                    pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");

                    pp.Start();


                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {

                    frmBooking frm = new frmBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse, fromdoorNo, toDoorNo, email, companyId, IsAccountCall);
                    frm.PickBookingTypeId = bookingTypeId;
                    frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                    frm.PickSubCompanyId = this.CalledToSubcompanyId;
                    frm.PickVehicleTypeId = vehicleTypeId;
                    frm.PickViaString = viaString;
                    frm.Tag = "cli";

                    frm.CustomerPermanentNotes = this.customerNotes;
                    if (id != 0)
                    {


                        frm.OnDisplayRecord(id);
                    }
                    frm.ControlBox = true;
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.MaximizeBox = false;

                    if (showOnDialog)
                    {
                        frm.ShowDialog();
                    }
                    else
                        frm.Show();



                    File.AppendAllText(Application.StartupPath + "\\exception_openingexebooking.txt","Constructor3 :"+ ex.Message);


                }
            }
            else
            {
                if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
                {
                    frmBooking2 frm = new frmBooking2(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse, fromdoorNo, toDoorNo, email, companyId, IsAccountCall);
                    frm.PickBookingTypeId = bookingTypeId;
                    frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                    frm.PickSubCompanyId = this.CalledToSubcompanyId;
                    frm.PickVehicleTypeId = vehicleTypeId;
                    frm.PickViaString = viaString;
                    frm.Tag = "cli";

                    if (excludedDriversList.ToStr().Trim().Length > 0)
                    {
                        frm.btnExcludeDrivers.Tag = excludedDriversList.ToStr().Trim();

                    }

                    frm.CustomerPermanentNotes = this.customerNotes;
                    if (id != 0)
                    {


                        frm.OnDisplayRecord(id);
                    }
                    frm.ControlBox = true;
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.MaximizeBox = false;

                    if (showOnDialog)
                    {
                        frm.ShowDialog();
                    }
                    else
                        frm.Show();

                }
                else
                {

                    frmBooking frm = new frmBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse, fromdoorNo, toDoorNo, email, companyId, IsAccountCall);
                    frm.PickBookingTypeId = bookingTypeId;
                    frm.CallRefNo = this.CallReferenceNo.ToStr().Trim();
                    frm.PickSubCompanyId = this.CalledToSubcompanyId;
                    frm.PickVehicleTypeId = vehicleTypeId;
                    frm.PickViaString = viaString;
                    if (excludedDriversList.ToStr().Trim().Length > 0)
                    {
                        frm.btnExcludeDrivers.Tag = excludedDriversList.ToStr().Trim();

                    }

                    frm.Tag = "cli";

                    frm.CustomerPermanentNotes = this.customerNotes;
                    if (id != 0)
                    {


                        frm.OnDisplayRecord(id);
                    }
                    frm.ControlBox = true;
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.MaximizeBox = false;

                    if (showOnDialog)
                    {
                        frm.ShowDialog();
                    }
                    else
                        frm.Show();
                }


            }
        
    
        
                   
        
            


        }



        private void FillDataset()
        {
            try
            {

                if (Program.dtCombos == null)
                {

                    Program.dtCombos = new DataSet();
                    using (System.Data.SqlClient.SqlConnection sqlconn = new System.Data.SqlClient.SqlConnection(Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToStr(), "softeuroconnskey", true)))
                    {

                        sqlconn.Open();

                        using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                        {

                            cmd.Connection = sqlconn;

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "stp_fillbookingcombos";

                            using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            {
                                // Fill the DataSet using default values for DataTable names, etc
                                da.Fill(Program.dtCombos);
                            }

                        }

                        Program.dtCombos.WriteXml(Application.StartupPath + "\\Booking\\booking.xml", XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch
            {


            }
        }

        private void chkShowETA_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (chkShowETA.Checked == false)
                {
                    lblETA.Text = string.Empty;
                    lblETA.Visible = false;

                }
                else
                {


                    string key = string.Empty;


                    using (TaxiDataContext dbX = new TaxiDataContext())
                    {
                        try
                        {
                            dbX.CommandTimeout = 5;
                            key = dbX.ExecuteQuery<string>("select apikey from mapkeys where maptype='here'").FirstOrDefault();


                            if (key.ToStr().Trim().Length == 0)
                            {
                                key = dbX.ExecuteQuery<string>("select apikey from mapkeys where maptype='google'").FirstOrDefault();

                            }
                        }
                        catch
                        {


                        }


                    }


                    if (key.Length == 0)
                    {
                        lblETA.Text = "NOT AVAILABLE";
                        lblETA.Visible = true;
                        lblETA.BringToFront();
                    }
                    else
                    {


                        GetDistance.Coords Orign = new GetDistance.Coords();
                        GetDistance.Coords dest = new GetDistance.Coords();


                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.DeferredLoadingEnabled = false;
                            var objBooking = db.Bookings.FirstOrDefault(c => c.Id == this.jobId);


                            if (objBooking != null)
                            {


                                var drvLoc = db.Fleet_Driver_Locations.FirstOrDefault(c => c.DriverId == objBooking.DriverId);


                                if (drvLoc != null)
                                {
                                    Orign.Latitude = drvLoc.Latitude;
                                    Orign.Longitude = drvLoc.Longitude;

                                }

                                string destination = objBooking.FromAddress.ToStr().ToUpper();

                                if (objBooking.BookingStatusId == Enums.BOOKINGSTATUS.POB || objBooking.BookingStatusId == Enums.BOOKINGSTATUS.STC)
                                {
                                    destination = objBooking.ToAddress.ToStr().ToUpper();

                                }

                                var obj1 = db.stp_getCoordinatesByAddress(destination, General.GetPostCodeMatch(destination)).FirstOrDefault();


                                if (obj1 != null)
                                {

                                    dest.Latitude = Convert.ToDouble(obj1.Latitude);
                                    dest.Longitude = Convert.ToDouble(obj1.Longtiude);

                                    string eta = GetDistance.GetLocationDetailsByMapHere(Orign, dest, key, null);
                                    if (objBooking.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || objBooking.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED)
                                    {
                                        lblETA.Text ="Pickup ETA : "+ eta;
                                    }
                                    else
                                    {
                                        lblETA.Text = "DropOff. ETA : " + eta;

                                    }
                            
                                    lblETA.Visible = true;
                                    lblETA.BringToFront();
                                }
                                else
                                {
                                    lblETA.Text = "NOT AVAILABLE";
                                    lblETA.Visible = true;
                                    lblETA.BringToFront();

                                }



                              


                            }
                        }


                       
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

       
    
    }
}
