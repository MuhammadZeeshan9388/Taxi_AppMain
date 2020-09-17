using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using DAL;
using Taxi_Model;
using Telerik.WinControls.UI;
using Utils;
using Telerik.WinControls;
using Telerik.Data;
using Taxi_AppMain.Forms;
using System.Threading;
using Telerik.WinControls.UI.Docking;
using System.Collections;
using Telerik.WinControls.UI.Export;
using System.Windows.Forms.DataVisualization.Charting;
using Telerik.WinControls.Enumerations;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace Taxi_AppMain
{
    public partial class frmBookingsList : UI.SetupBase
    {

        RadGridViewExcelExporter exporter = null;

        BookingBO objMaster;

        private bool _RefreshOnActive;

        public bool RefreshOnActive
        {
            get { return _RefreshOnActive; }
            set { _RefreshOnActive = value; }
        }



        int pageSize = 0;
        bool lockCompletedBooking;
        bool lockCancelledBooking;
        bool lockNoFareBooking;

        RadDropDownMenu EditFare = null;
        public frmBookingsList()
        {




            InitializeComponent();
            this.Load += new EventHandler(frmBookingsList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            grdLister.FilterChanged += new GridViewCollectionChangedEventHandler(grdLister_FilterChanged);

            objMaster = new BookingBO();

            this.SetProperties((INavigation)objMaster);
            grdLister.ShowGroupPanel = false;
            grdLister.EnableHotTracking = false;

            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.VerticalScroll.LargeChange = 100;
            grdLister.TableElement.VScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            grdLister.EnableSorting = false;
         
            pageSize = AppVars.objPolicyConfiguration.ListingPagingSize.ToInt();

            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);


            this.Enter += new EventHandler(frmBookingsList_Enter);
            //    this.GotFocus += new EventHandler(frmBookingsList_GotFocus);

            this.Shown += new EventHandler(frmBookingsList_Shown);
            this.FormClosing += new FormClosingEventHandler(frmBookingsList_FormClosing);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmBookingsList_KeyDown);

        }



        void frmBookingsList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if ((txtSearch.FocusedElement!=null && txtSearch.FocusedElement.IsFocused == true) || dtpFromDate.DateTimePickerElement.IsFocused == true || dtpToDate.DateTimePickerElement.IsFocused == true)
                //    return;


                if (e.KeyCode == Keys.Enter)
                {

                    if (txtSearch.Text.ToLower() == "comp")
                    {
                        optCompletedJobs.ToggleState = ToggleState.On;
                        txtSearch.Text = string.Empty;
                    }

                    Find();

                }

                else if (e.KeyCode == Keys.F1)
                {
                    optCompletedJobs.ToggleState = ToggleState.On;
                    FindBookings();

                }
                else if (e.KeyCode == Keys.F2)
                {
                    optNoFares.ToggleState = ToggleState.On;
                    FindBookings();

                }
                else if (e.KeyCode == Keys.F3)
                {
                    optCancelledJobs.ToggleState = ToggleState.On;

                    FindBookings();

                }
                else if (e.KeyCode == Keys.F4)
                {


                    optAllJobs.ToggleState = ToggleState.On;

                    FindBookings();


                }

            }
            catch
            {


            }
        }


        void grdLister_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                //if (grdLister.RowCount > 0)
                //{
                lblBookingCount.Visible = true;
                lblBookingCount.Text = "Booking(s) Found : " + grdLister.ChildRows.Count();//grdLister.Rows.Where(c => c.IsVisible==true).Count();
                //  }
            }
            catch
            {

            }
        }


        private bool IsClosed = false;
        void frmBookingsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsClosed = true;

            if (bWorker != null && bWorker.WorkerSupportsCancellation)
            {
                bWorker.CancelAsync();
                bWorker.Dispose();

            }


        }

        void frmBookingsList_Shown(object sender, EventArgs e)
        {
            try
            {

                InitializeLoading();

                lockCompletedBooking = AppVars.listUserRights.Count(c => c.formName == "frmBooking" && c.functionId == "LOCK COMPLETED BOOKING") > 0;
                lockCancelledBooking = AppVars.listUserRights.Count(c => c.formName == "frmBooking" && c.functionId == "LOCK CANCELLED BOOKING") > 0;
                lockNoFareBooking = AppVars.listUserRights.Count(c => c.formName == "frmBooking" && c.functionId == "LOCK NOFARE BOOKING") > 0;


                if (this.CanDelete)
                {
                    GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                    col.Width = 40;
                    col.AutoSizeMode = BestFitColumnMode.None;
                    col.HeaderText = "";
                    col.Name = "Check";
                    //col.IsPinned = true;
                    grdLister.Columns.Add(col);
                    btnDeleteSelected.Visible = true;
                    chkSelectAll.Visible = true;
                }



                ddlColumns.Items.Add("Reference");
                ddlColumns.Items.Add("Passenger");
                ddlColumns.Items.Add("Telephone No");
                ddlColumns.Items.Add("Mobile No");
                ddlColumns.Items.Add("Pickup Point");
                ddlColumns.Items.Add("Destination");

                ddlColumns.Items.Add("Vehicle");
                ddlColumns.Items.Add("Driver");
                ddlColumns.Items.Add("Status");

                ddlColumns.Items.Add("SubCompany");


                ddlColumns.SelectedIndex = 0;



                SetDefaultDateCriteria();


                //  new Thread(new ThreadStart(LoadingData)).Start();

                PopulateData();


                if (this.CanDelete)
                {
                    grdLister.Columns["Check"].Width = 40;
                }




                var hiddenColumnsList = General.GetQueryable<UM_Form_UserDefinedSetting>(c => c.UM_Form.FormName == this.Name && (c.IsVisible == false || c.GridColMoveTo != null)).ToList();



                for (int i = 0; i < hiddenColumnsList.Count; i++)
                {

                    grdLister.Columns[hiddenColumnsList[i].GridColumnName].IsVisible = hiddenColumnsList[i].IsVisible.ToBool();


                    if (hiddenColumnsList[i].GridColMoveTo != null)
                    {
                        grdLister.Columns.Move(grdLister.Columns[hiddenColumnsList[i].GridColumnName].Index, hiddenColumnsList[i].GridColMoveTo.ToInt());
                    }

                }



                grdLister.Columns["FromLocTextColor"].IsVisible = false;
                grdLister.Columns["ToLocTextColor"].IsVisible = false;



                grdLister.Columns["FromLocBgColor"].IsVisible = false;
                grdLister.Columns["ToLocBgColor"].IsVisible = false;
                grdLister.Columns["BookingBackgroundColor"].IsVisible = false;

                grdLister.Columns["SubCompanyBgColor"].IsVisible = false;
                //   grdLister.Columns["MobileNo"].IsVisible = false;
                grdLister.Columns["MobileNo"].HeaderText = "Tel. No";
                grdLister.Columns["MobileNo"].Width = 110;

                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["DriverId"].IsVisible = false;

                grdLister.Columns["VehicleBgColor"].IsVisible = false;
                grdLister.Columns["VehicleTextColor"].IsVisible = false;

                grdLister.Columns["BackgroundColor1"].IsVisible = false;
                grdLister.Columns["TextColor1"].IsVisible = false;

                grdLister.Columns["StatusId"].IsVisible = false;

                grdLister.Columns["IsAutoDespatch"].IsVisible = false;
                grdLister.Columns["IsBidding"].IsVisible = false;


                grdLister.Columns["BookingTypeId"].IsVisible = false;


                grdLister.Columns["StatusColor"].IsVisible = false;
                grdLister.Columns["FromLocTypeId"].IsVisible = false;
                grdLister.Columns["ToLocTypeId"].IsVisible = false;

                AddCommandColumn("btnRecall", "Re-Call", 50);
                AddCommandColumn("btnReDespatch", "Re-Dispatch", 70);

                if (this.CanDelete)
                {
                    AddCommandColumn("btnDelete", "Delete", 70);

                    grdLister.Columns["btnDelete"].Width = 70;
                }

                UI.GridFunctions.SetFilter(grdLister);


                grdLister.AllowEditRow = true;


                //    grdLister.Columns["RefNumber"].IsVisible = false;
                grdLister.Columns["RefNumber"].Width = 70;
                grdLister.Columns["RefNumber"].HeaderText = "Ref #";
                grdLister.Columns["Fare"].Width = 70;
                grdLister.Columns["Fare"].HeaderText = "Fare £";
                grdLister.Columns["Vehicle"].Width = 70;
                grdLister.Columns["Driver"].Width = 50;

                grdLister.Columns["Status"].Width = 80;
                grdLister.Columns["Passenger"].Width = 80;

                grdLister.Columns["From"].Width = 150;
                grdLister.Columns["From"].HeaderText = "Pickup Point";

                grdLister.Columns["To"].Width = 150;
                grdLister.Columns["To"].HeaderText = "Destination";


                grdLister.Columns["BookingDate"].IsVisible = false;
                // grdLister.Columns["BookingDate"].Width = 90;
                grdLister.Columns["BookingDate"].HeaderText = "Booking Date";


                (grdLister.Columns["PickupDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
                (grdLister.Columns["PickupDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


                grdLister.Columns["PickupDate"].Width = 130;
                grdLister.Columns["PickupDate"].HeaderText = "Pickup Date-Time";


                grdLister.Columns["Account"].Width = 140;
                grdLister.Columns["Account"].HeaderText = "A/C";


                grdLister.Columns["FromPostCode"].HeaderText = "Area";
                grdLister.Columns["ToPostCode"].HeaderText = "Area";


                grdLister.Columns["FromPostCode"].Width = 80;
                grdLister.Columns["ToPostCode"].Width = 80;

                grdLister.Columns["Pickup"].Width = 150;
                grdLister.Columns["GoingTo"].Width = 150;


                grdLister.Columns["AccountFare"].Width = 80;
                grdLister.Columns["AccountFare"].HeaderText = "A/C Price";

                grdLister.Columns["CustomerFare"].Width = 80;
                grdLister.Columns["CustomerFare"].HeaderText = "Cust Price";


                grdLister.Columns["Token"].Width = 60;
                grdLister.Columns["Token"].HeaderText = "Token #";
                // grdLister.Columns["GroupId"].HeaderText = "Token";


                FinishLoading();
                if (AppVars.listUserRights.Count(c => c.formName == "frmBookingsList" && c.functionId == "PRINT BOOKING LIST") > 0)
                {
                    if (grdLister.Columns.Contains("Check") == false)
                    {
                        GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                        col.Width = 40;
                        col.AutoSizeMode = BestFitColumnMode.None;
                        col.HeaderText = "";
                        col.Name = "Check";
                        //col.IsPinned = true;
                        grdLister.Columns.Add(col);
                    }
                    btnPrintSelected.Visible = true;
                }
                else
                {
                    btnPrintSelected.Visible = false;
                }


                this.dtpFromDate.Opened += new System.EventHandler(this.dtpFromDate_Opened);
                this.dtpFromDate.Closed += new Telerik.WinControls.UI.RadPopupClosedEventHandler(this.dtpFromDate_Closed);

                dtpToDate.Opened += DtpToDate_Opened;
                dtpToDate.Closed += new RadPopupClosedEventHandler(dtpToDate_Closed);


                txtSearch.Focus();

            }
            catch
            {


            }
        }


        private void SetDefaultDateCriteria()
        {
            dtpFromDate.Value = DateTime.Now.ToDate();
            dtpToDate.Value = DateTime.Now.ToDate() + TimeSpan.Parse("23:59:59");
            this.IsFind = true;

        }




        public void SetRefreshWhenActive(string msg)
        {
            try
            {
                if ((AppVars.frmMDI.Controls[0] as RadDock).ActiveWindow.Name == "frmBookingsList1")
                {

                   // InitializeBWorker();


                    //if (msg.ToStr().Trim().Length == 0)
                    //{




                    //    if (bWorker != null && bWorker.IsBusy == false)
                    //    {


                    //        if (bWorker.IsBusy == false)
                    //        {

                    //            bWorker.RunWorkerAsync();
                    //        }

                            
                    //    }
                      
                    //}
                    //else
                    //{
                    //    string[] arr = msg.Split(new string[] { ">>" },StringSplitOptions.RemoveEmptyEntries);

                    //    if(arr.Count()>1)
                    //    {
                    //        long jobId = arr[1].ToLong();
                    //        int statusId = arr[3].ToInt();

                    //        var row = grdLister.Rows.FirstOrDefault(c => c.Cells["Id"].Value.ToLong() == jobId);

                    //        if(row!=null)
                    //        {
                    //            if (statusId == Enums.BOOKINGSTATUS.ONROUTE)
                    //            {
                    //                row.Cells["Status"].Value = "OnRoute";
                    //                row.Cells["StatusId"].Value = statusId;
                    //                row.Cells["StatusColor"].Value = "-256";

                    //            }
                    //            else if (statusId == Enums.BOOKINGSTATUS.ARRIVED)
                    //            {
                    //                row.Cells["Status"].Value = "Arrived";
                    //                row.Cells["StatusId"].Value = statusId;
                    //                row.Cells["StatusColor"].Value = "-38476";
                                   

                    //            }
                    //            else if (statusId == Enums.BOOKINGSTATUS.POB)
                    //            {
                                   
                                    
                    //                row.Cells["Status"].Value = "POB";
                    //                row.Cells["StatusId"].Value = statusId;
                    //                row.Cells["StatusColor"].Value = " -65536";
                                  
                    //            }
                    //            else if (statusId == Enums.BOOKINGSTATUS.STC)
                    //            {
                    //                row.Cells["Status"].Value = "STC";
                    //                row.Cells["StatusId"].Value = statusId;
                    //                row.Cells["StatusColor"].Value = "-16728065";

                    //            }
                    //            else if (statusId == Enums.BOOKINGSTATUS.DISPATCHED)
                    //            {
                    //                row.Cells["Status"].Value = "Completed";
                    //                row.Cells["StatusId"].Value = statusId;
                    //                row.Cells["StatusColor"].Value = "-6559587";

                    //            }
                    //            else
                    //            {
                    //                if (bWorker != null && bWorker.IsBusy == false)
                    //                {


                    //                    if (bWorker.IsBusy == false)
                    //                    {

                    //                        bWorker.RunWorkerAsync();
                    //                    }


                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            //if (bWorker != null && bWorker.IsBusy == false)
                    //            //{


                    //            //    if (bWorker.IsBusy == false)
                    //            //    {

                    //            //        bWorker.RunWorkerAsync();
                    //            //    }


                    //            //}
                    //        }


                    //    }

                      

                    //}
                }
                else
                {

                    this.RefreshOnActive = true;
                }
            }
            catch
            {


            }

        }


        BackgroundWorker bWorker = null;
        void frmBookingsList_Enter(object sender, EventArgs e)
        {

            if (this.RefreshOnActive)
            {
                this.RefreshOnActive = false;


                if (dtpFromDate.Value != null && dtpToDate.Value != null && dtpFromDate.Value.ToDate() == DateTime.Now.Date && dtpToDate.Value.ToDate() == DateTime.Now.Date)
                {

                    InitializeBWorker();



                    if (bWorker.IsBusy == false)
                    {

                        bWorker.RunWorkerAsync();
                    }


                }
            }


            txtSearch.Focus();


        }


        private void InitializeBWorker()
        {

            if (bWorker == null)
            {

                bWorker = new BackgroundWorker();
                bWorker.WorkerSupportsCancellation = true;

                bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
                bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
            }
        }

        IList listofData = null;

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IsClosed || IsDisposed)
                    return;


                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new DisplayProgressBar(PopulateGridByWorker));



                }
                else
                {
                    PopulateGridByWorker();


                }
            }
            catch
            {



            }
        }


        private void PopulateGridByWorker()
        {

            try
            {


                if (IsClosed)
                    return;





                grdLister.Enabled = false;

                long JobIndex = grdLister.CurrentRow != null ? grdLister.CurrentRow.Cells["Id"].Value.ToLong() : -1;

                int val = grdLister.TableElement.VScrollBar.Value;

                grdLister.DataSource = listofData;


                if (JobIndex > 0)
                    grdLister.CurrentRow = grdLister.Rows.FirstOrDefault(c => c.Cells["Id"].Value.ToLong() == JobIndex);


                if (grdLister.TableElement.VScrollBar.Maximum >= val)
                {
                    grdLister.TableElement.VScrollBar.Value = val;
                }

                grdLister.Enabled = true;
                lblBookingCount.Text = "Booking(s) Found :" + grdLister.Rows.Count;
                //   lblBookingCount.Visible = false;

            }
            catch
            {

                grdLister.Enabled = true;

            }

        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            PopulateDataByWorker();
        }


        private void PopulateDataByWorker()
        {
            try
            {
                if (IsClosed)
                    return;


                int bookingstatusId = 0;


                if (optCancelledJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = 3;

                else if (optNoFares.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = 13;

                else if (optRejectedJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = 11;

                else if (optCompletedJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = Enums.BOOKINGSTATUS.DISPATCHED.ToInt();

                var data1 = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING && c.BookingStatusId != Enums.BOOKINGSTATUS.ONHOLD
                    && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING_WEBBOOKING
                    && c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING_WEBBOOKING && c.BookingStatusId != Enums.BOOKINGSTATUS.REJECTED_WEBBOOKING
                    && (c.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
                    && (bookingstatusId == 0 || (bookingstatusId == 13 && (c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP || c.BookingStatusId == Enums.BOOKINGSTATUS.NOSHOW || c.BookingStatusId == Enums.BOOKINGSTATUS.REJECTED)) || c.BookingStatusId == bookingstatusId))


                    .OrderByDescending(c => c.PickupDateTime);

                //var data1 = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING && c.BookingStatusId != Enums.BOOKINGSTATUS.ONHOLD
                //    && (c.SubcompanyId == AppVars.DefaultSubCompanyId || AppVars.DefaultSubCompanyId == 0))
                //               .OrderByDescending(c => c.PickupDateTime);


                if (this.IsFind)
                {

                    string searchTxt = txtSearch.Text.ToLower().Trim();
                    string col = ddlColumns.Text.Trim().ToLower();

                    if (searchTxt.Length < 3)
                        searchTxt = string.Empty;


                    DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                    DateTime? toDate = dtpToDate.Value.ToDateTimeorNull();

                    bool col_name = false;
                    bool col_refNo = false;
                    bool col_telNo = false;
                    bool col_mobileno = false;
                    bool col_driver = false;
                    bool col_vehicle = false;
                    bool col_status = false;
                    bool col_pickupPoint = false;
                    bool col_destination = false;
                    bool col_subcompany = false;

                    if (col == "passenger")
                    {
                        col_name = true;
                    }
                    else if (col == "reference")
                    {
                        col_refNo = true;
                    }
                    else if (col == "telephone no")
                    {
                        col_telNo = true;
                    }

                    else if (col == "mobile no")
                    {
                        col_mobileno = true;
                    }

                    else if (col == "driver")
                    {
                        col_driver = true;
                    }

                    else if (col == "vehicle")
                    {
                        col_vehicle = true;
                    }

                    else if (col == "status")
                    {
                        col_status = true;
                    }

                    else if (col == "pickup point")
                    {
                        col_pickupPoint = true;
                    }

                    else if (col == "destination")
                    {
                        col_destination = true;
                    }
                    else if (col == "subcompany")
                    {
                        col_subcompany = true;
                    }


                    //int cnt = data1.Count();
                    //if (skip + pageSize > cnt && cnt - pageSize > 0)
                    //    skip = cnt - pageSize;
                    //else if (cnt <= pageSize)
                    skip = 0;



                    var query = (from a in data1

                                 where

                               //  (fromDate != null ||

                               (
                                 (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_refNo && (a.BookingNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_telNo && (a.CustomerPhoneNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_mobileno && (a.CustomerMobileNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_driver && (a.Fleet_Driver != null && a.Fleet_Driver.DriverNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))

                                     //|| (col_pickupPoint && (a.FromAddress.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     //|| (col_destination && (a.ToAddress.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_pickupPoint && (searchTxt == string.Empty || (a.FromDoorNo != null && a.FromDoorNo.ToLower().Contains(searchTxt)) || a.FromAddress.ToLower().Contains(searchTxt)))
                           || (col_destination && (searchTxt == string.Empty || (a.ToDoorNo != null && a.ToDoorNo.ToLower().Contains(searchTxt)) || a.ToAddress.ToLower().Contains(searchTxt)))



                                     || (col_vehicle && (a.Fleet_VehicleType != null && a.Fleet_VehicleType.VehicleType.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_status && (a.BookingStatus != null && a.BookingStatus.StatusName.ToLower().Contains(searchTxt)))

                                      || (col_subcompany && (a.SubcompanyId != null && a.Gen_SubCompany.CompanyName.ToLower().Contains(searchTxt)))

                                 )
                                     && ((fromDate == null || a.PickupDateTime.Value >= fromDate) && (toDate == null || a.PickupDateTime.Value <= toDate))



                                 select new
                                 {
                                     Id = a.Id,
                                     Token = a.JobCode,
                                     RefNumber = a.BookingNo,
                                     BookingDate = a.BookingDate,
                                     PickupDate = a.PickupDateTime,
                                     Passenger = a.CustomerName,
                                     MobileNo = a.CustomerMobileNo != null && a.CustomerMobileNo != "" ? a.CustomerMobileNo : a.CustomerPhoneNo,

                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     Pickup = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromOther : a.FromOther,
                                     FromPostCode = a.FromPostCode,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     GoingTo = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToOther : a.ToOther,
                                     ToPostCode = a.ToPostCode,



                                     Fare = a.FareRate,
                                     PaymentMethod = a.Gen_PaymentType.PaymentType,
                                     AccountFare = a.CompanyPrice,
                                     CustomerFare = a.CompanyPrice,
                                     Account = a.OrderNo != null && a.OrderNo != string.Empty ? a.Gen_Company.CompanyName + " - " + a.OrderNo : a.Gen_Company.CompanyName,
                                     Driver = a.Fleet_Driver.DriverNo,
                                     DriverId = a.DriverId,
                                     Vehicle = a.Fleet_VehicleType.VehicleType,
                                     Status = a.BookingStatus.StatusName,
                                     StatusColor = a.BookingStatus.BackgroundColor,
                                     BookingTypeId = a.BookingTypeId,
                                     VehicleBgColor = a.Fleet_VehicleType.BackgroundColor,
                                     VehicleTextColor = a.Fleet_VehicleType.TextColor,
                                     BackgroundColor1 = a.Gen_Company.BackgroundColor,
                                     TextColor1 = a.Gen_Company.TextColor,

                                     FromLocTypeId = a.FromLocTypeId,
                                     ToLocTypeId = a.ToLocTypeId,
                                     SubCompanyBgColor = a.SubcompanyId != null ? a.Gen_SubCompany.BackgroundColor : -1,
                                     StatusId = a.BookingStatusId,
                                     BookingBackgroundColor = a.BookingType.BackgroundColor,
                                     FromLocBgColor = a.FromLocId != null ? a.Gen_Location1.BackgroundColor : -1,
                                     ToLocBgColor = a.ToLocId != null ? a.Gen_Location2.BackgroundColor : -1,
                                     FromLocTextColor = a.FromLocId != null ? a.Gen_Location1.TextColor : -1,
                                     ToLocTextColor = a.ToLocId != null ? a.Gen_Location2.TextColor : -1,
                                     IsAutoDespatch = a.AutoDespatch,
                                     IsBidding = a.IsBidding
                                 }).ToList();



                    listofData = query;
                }
                else
                {

                    int cnt = data1.Count();
                    if (skip + pageSize > cnt && cnt - pageSize > 0)
                        skip = cnt - pageSize;
                    else if (cnt <= pageSize)
                        skip = 0;


                    var query = (from a in data1


                                 select new
                                 {
                                     Id = a.Id,
                                     Token = a.JobCode,
                                     RefNumber = a.BookingNo,
                                     BookingDate = a.BookingDate,
                                     PickupDate = a.PickupDateTime,
                                     Passenger = a.CustomerName,
                                     MobileNo = a.CustomerMobileNo != null && a.CustomerMobileNo != "" ? a.CustomerMobileNo : a.CustomerPhoneNo,

                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     Pickup = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromOther : a.FromOther,
                                     FromPostCode = a.FromPostCode,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     GoingTo = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToOther : a.ToOther,
                                     ToPostCode = a.ToPostCode,
                                     Fare = a.FareRate,
                                     PaymentMethod = a.Gen_PaymentType.PaymentType,
                                     AccountFare = a.CompanyPrice,
                                     CustomerFare = a.CompanyPrice,
                                     Account = a.OrderNo != null && a.OrderNo != string.Empty ? a.Gen_Company.CompanyName + " - " + a.OrderNo : a.Gen_Company.CompanyName,
                                     Driver = a.Fleet_Driver.DriverNo,
                                     DriverId = a.DriverId,
                                     Vehicle = a.Fleet_VehicleType.VehicleType,
                                     Status = a.BookingStatus.StatusName,
                                     StatusColor = a.BookingStatus.BackgroundColor,
                                     BookingTypeId = a.BookingTypeId,
                                     VehicleBgColor = a.Fleet_VehicleType.BackgroundColor,
                                     VehicleTextColor = a.Fleet_VehicleType.TextColor,
                                     BackgroundColor1 = a.Gen_Company.BackgroundColor,
                                     TextColor1 = a.Gen_Company.TextColor,

                                     FromLocTypeId = a.FromLocTypeId,
                                     ToLocTypeId = a.ToLocTypeId,
                                     SubCompanyBgColor = a.SubcompanyId != null ? a.Gen_SubCompany.BackgroundColor : -1,
                                     StatusId = a.BookingStatusId,
                                     BookingBackgroundColor = a.BookingType.BackgroundColor,
                                     FromLocBgColor = a.FromLocId != null ? a.Gen_Location1.BackgroundColor : -1,
                                     ToLocBgColor = a.ToLocId != null ? a.Gen_Location2.BackgroundColor : -1,
                                     FromLocTextColor = a.FromLocId != null ? a.Gen_Location1.TextColor : -1,
                                     ToLocTextColor = a.ToLocId != null ? a.Gen_Location2.TextColor : -1,
                                     IsAutoDespatch = a.AutoDespatch,
                                     IsBidding = a.IsBidding
                                 }).Skip(skip).Take(pageSize).ToList();


                    listofData = query;
                    //  this.grdLister.TableElement.BeginUpdate();

                    //  grdLister.DataSource = query;
                    // this.grdLister.TableElement.EndUpdate();
                }
            }
            catch
            {


            }


        }



        private void StartLoading()
        {

            InitializeLoading();
        }

        delegate void DisplayProgressBar();
        private void InitializeLoading()
        {

            if (this.InvokeRequired)
            {
                DisplayProgressBar d = new DisplayProgressBar(ShowLoadingImage);
                this.BeginInvoke(d);
            }
            else
            {
                ShowLoadingImage();

            }

        }

        frmLoadingScreen frmLoading = null;
        private void ShowLoadingImage()
        {

            frmLoading = new frmLoadingScreen();
            frmLoading.ShowInTaskbar = false;
            frmLoading.Show();
        }



        private void FinishLoading()
        {
            if (frmLoading != null)
            {
                frmLoading.BackgroundImage.Dispose();
                frmLoading.Dispose();
                frmLoading.Close();
            }
        }



        private void InitializeExportGrid()
        {
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();


            this.radGridView1.Location = new System.Drawing.Point(405, 60);
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(240, 150);
            this.radGridView1.TabIndex = 18;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.Visible = false;

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();

            this.radPanel1.Controls.Add(this.radGridView1);
        }


        void EditFareItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {

                    string message = grdLister.CurrentRow.Cells["FromLocTypeId"].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT ? AppVars.objPolicyConfiguration.ArrivalAirportBookingText.ToStr()
                        : AppVars.objPolicyConfiguration.ArrivalBookingText.ToStr();


                    frmSMSAll frm = new frmSMSAll(grdLister.CurrentRow.Cells["MobileNo"].Value.ToStr(), message, 0);
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }



        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);
        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


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


        void EditFareItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    frmEditFare frm = new frmEditFare(grdLister.CurrentRow.Cells["Id"].Value.ToLong(), 0);
                    frm.ShowDialog();
                    frm.Dispose();

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                {
                    e.Cancel = true;
                    return;

                }
                else if (cell.GridControl.Name == "grdLister")
                {

                    if (EditFare == null)
                    {
                        EditFare = new RadDropDownMenu();
                        EditFare.BackColor = Color.Orange;

                        RadMenuItem EditFareItem1 = new RadMenuItem("Edit Fare");
                        EditFareItem1.ForeColor = Color.DarkBlue;
                        EditFareItem1.BackColor = Color.Orange;
                        EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        EditFareItem1.Click += new EventHandler(EditFareItem1_Click);
                        EditFare.Items.Add(EditFareItem1);


                        RadMenuItem EditFareItem2 = new RadMenuItem("Arrival Text");
                        EditFareItem2.ForeColor = Color.DarkBlue;
                        EditFareItem2.BackColor = Color.Orange;
                        EditFareItem2.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        EditFareItem2.Click += new EventHandler(EditFareItem2_Click);
                        EditFare.Items.Add(EditFareItem2);


                        RadMenuItem EditFareItem3 = new RadMenuItem("Copy Booking");
                        EditFareItem3.ForeColor = Color.Black; 
                        EditFareItem3.BackColor = Color.Orange;
                        EditFareItem3.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        EditFareItem3.Click += new EventHandler(CopyBooking_Click);
                        EditFare.Items.Add(EditFareItem3);


                        if (AppVars.listUserRights.Count(c => c.formName == "frmBookingDashBoard" && c.functionId == "JOB AUDIT TRAIL") > 0)
                        {
                            RadMenuItem EditFareItem4 = new RadMenuItem("Job Audit Trail");
                            EditFareItem4.ForeColor = Color.Black;
                            EditFareItem4.BackColor = Color.Orange;
                            EditFareItem4.Font = new Font("Tahoma", 10, FontStyle.Bold);
                            EditFareItem4.Click += new EventHandler(AuditReport_Click);
                            EditFare.Items.Add(EditFareItem4);

                        }

                        RadMenuItem trackdriver = new RadMenuItem("Track Driver");
                        trackdriver.ForeColor = Color.Black;
                        trackdriver.BackColor = Color.Orange;
                        trackdriver.Name = "TrackDriver";
                        trackdriver.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        trackdriver.Click += new EventHandler(TrackDriver_Click);
                        trackdriver.Visibility = ElementVisibility.Collapsed;
                        EditFare.Items.Add(trackdriver);



                    }

                    if(cell.RowInfo!=null && cell.RowInfo is GridViewDataRowInfo )
                    {
                        if (cell.RowInfo.Cells["StatusId"].Value.ToInt() == 21)
                        {
                            EditFare.Items["TrackDriver"].Visibility = ElementVisibility.Visible;
                        }
                        else
                            EditFare.Items["TrackDriver"].Visibility = ElementVisibility.Collapsed;
                    }

                    e.ContextMenu = EditFare;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        void CopyBooking_Click(object sender, EventArgs e)
        {
            try
            {
                long jobId = 0;


                jobId = grdLister.CurrentRow.Cells["Id"].Value.ToLong();




                if (jobId > 0)
                {

                    AppVars.objCopyBooking = General.GetObject<Booking>(c => c.Id == jobId);

                }




            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void TrackDriver_Click(object sender, EventArgs e)
        {
            try
            {
                long jobId = 0;


                jobId = grdLister.CurrentRow.Cells["Id"].Value.ToLong();




                if (jobId > 0)
                {

                    TrackDriver(jobId);

                }




            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }  


        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
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

                else if (e.CellElement is GridFilterCellElement)
                {



                    e.CellElement.Font = oldFont;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.White;
                    e.CellElement.RowElement.BackColor = Color.White;
                    e.CellElement.RowElement.NumberOfColors = 1;

                    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                    e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                    e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                }
                else if (e.CellElement is GridRowHeaderCellElement)
                {

                    if (e.CellElement is GridTableHeaderCellElement)
                    {

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
                    else if (e.CellElement is GridRowHeaderCellElement && e.Row is GridViewFilteringRowInfo)
                    {

                        e.CellElement.Font = oldFont;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.White;
                        e.CellElement.RowElement.BackColor = Color.White;
                        e.CellElement.RowElement.NumberOfColors = 1;

                        e.CellElement.BorderColor = Color.DarkSlateBlue;
                        e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                        e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                        e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                        e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                    }
                    else
                    {
                        e.CellElement.DrawFill = false;

                      
                      




                            e.CellElement.BackColor = Color.FromArgb(e.Row.Cells["SubCompanyBgColor"].Value.ToInt());
                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.BorderColor = Color.DarkSlateBlue;
                            e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                            e.CellElement.DrawFill = true;

                      

                    }

                    //else
                    //{

                    //    e.CellElement.BackColor = Color.FromArgb(e.Row.Cells["SubCompanyBgColor"].Value.ToInt());
                    //    e.CellElement.NumberOfColors = 1;
                    //    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    //    e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                    //    e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                    //    e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                    //    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                    //    e.CellElement.DrawFill = true;

                    //}



                }


                else if (e.CellElement is GridDataCellElement)
                {



                    if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                    {


                        if (e.Column.Name == "btnReDespatch" || e.Column.Name == "btnRecall")
                        {

                            if (
                                (lockCompletedBooking && e.CellElement.RowInfo.Cells["StatusId"].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED)
                               || (lockCancelledBooking && e.CellElement.RowInfo.Cells["StatusId"].Value.ToInt() == Enums.BOOKINGSTATUS.CANCELLED)
                                || (lockNoFareBooking && e.CellElement.RowInfo.Cells["StatusId"].Value.ToInt() == Enums.BOOKINGSTATUS.NOPICKUP))
                            {


                                // ((RadButtonElement)e.CellElement.Children[0]).Text = "Completed";
                                ((RadButtonElement)e.CellElement.Children[0]).Enabled = false;
                            }
                            else
                            {
                                //     ((RadButtonElement)e.CellElement.Children[0]).Text = "Re-Despatched";
                                ((RadButtonElement)e.CellElement.Children[0]).Enabled = true;

                            }

                        }
                        else if (e.Column.Name == "btnDelete")
                        {


                            ((RadButtonElement)e.CellElement.Children[0]).TextImageRelation = TextImageRelation.ImageBeforeText;
                            ((RadButtonElement)e.CellElement.Children[0]).TextAlignment = ContentAlignment.MiddleCenter;
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.delete;




                        }


                    }


                 

                    e.CellElement.ToolTipText = e.CellElement.Text;

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

                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.DeepSkyBlue;
                        e.CellElement.ForeColor = Color.White;
                        e.CellElement.Font = newFont;

                    }

                    else
                    {
                        e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);

                    }


                    e.CellElement.DrawFill = false;

                    if (e.Column.Name == "RefNumber" || e.Column.Name == "BookingDate")
                    {

                        e.CellElement.NumberOfColors = 1;

                        if (e.Row.Cells["BookingBackgroundColor"].Value != null)
                        {

                            e.CellElement.BackColor = Color.FromArgb((e.Row.Cells["BookingBackgroundColor"].Value.ToInt()));
                            e.CellElement.DrawFill = true;
                        }


                    }

                    //if (e.Column.Name == "RefNumber" || e.Column.Name == "PickUpDate" || e.Column.Name == "Time")
                    //{
                    //    if (e.Row.Cells["BookingTypeId"].Value.ToInt() == Enums.BOOKING_TYPES.VIP)
                    //    {
                    //        e.CellElement.NumberOfColors = 1;
                    //        e.CellElement.DrawFill = true;

                    //        string bgColor = AppVars.objPolicyConfiguration.VIPBookingBackgroundColor.ToStr();

                    //        if (!string.IsNullOrEmpty(bgColor))
                    //        {

                    //            e.CellElement.BackColor = Color.FromArgb(bgColor.ToInt());
                    //        }

                    //    }
                    //    else if (e.Row.Cells["BookingTypeId"].Value.ToInt() == Enums.BOOKING_TYPES.WEB)
                    //    {
                    //        e.CellElement.NumberOfColors = 1;
                    //        e.CellElement.DrawFill = true;

                    //        string bgColor = AppVars.objPolicyConfiguration.WebBookingBackgroundColor.ToStr();

                    //        if (!string.IsNullOrEmpty(bgColor))
                    //        {

                    //            e.CellElement.BackColor = Color.FromArgb(bgColor.ToInt());
                    //        }

                    //    }
                    //}



                    if (e.Column.Name == "Account" && e.CellElement.Value.ToStr() != string.Empty)
                    {


                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.DrawFill = true;


                        string Bgcolor = e.Row.Cells["BackgroundColor1"].Value.ToStr().Trim();
                        string textColor = e.Row.Cells["TextColor1"].Value.ToStr().Trim();

                        if (Bgcolor != string.Empty && textColor != string.Empty)
                        {

                            Color bgClr = Color.FromArgb(Bgcolor.ToInt());
                            Color txtClr = Color.FromArgb(textColor.ToInt());

                            e.CellElement.BackColor = bgClr;
                            e.CellElement.ForeColor = txtClr;

                        }
                        else
                        {
                            e.CellElement.ForeColor = Color.White;
                            e.CellElement.BackColor = Color.Crimson;


                        }
                    }

                    else if (e.Column.Name == "Vehicle")
                    {
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.DrawFill = true;


                        string Bgcolor = e.Row.Cells["VehicleBgColor"].Value.ToStr().Trim();
                        string textColor = e.Row.Cells["VehicleTextColor"].Value.ToStr().Trim();

                        if (Bgcolor != string.Empty && textColor != string.Empty)
                        {

                            e.CellElement.BackColor = Color.FromArgb(Bgcolor.ToInt());
                            e.CellElement.ForeColor = Color.FromArgb(textColor.ToInt());

                        }
                        else
                        {
                            e.CellElement.BackColor = Color.White;
                            e.CellElement.ForeColor = Color.Black;

                        }
                    }


                    else if (e.Column.Name == "From")
                    {
                        if (e.Row.Cells["FromLocTypeId"].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        {

                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.DrawFill = true;

                            e.CellElement.BackColor = Color.FromArgb(-5374161);
                            e.CellElement.ForeColor = Color.Black;

                            //e.CellElement.BackColor = Color.FromArgb(e.Row.Cells["FromLocBgColor"].Value.ToInt());
                            //// e.CellElement.BackColor = Color.GreenYellow;
                            //// e.CellElement.ForeColor = Color.Black;

                            //if (e.Row.Cells["FromLocTextColor"].Value != null)
                            //{

                            //    e.CellElement.ForeColor = Color.FromArgb(e.Row.Cells["FromLocTextColor"].Value.ToInt());

                            //}
                            //else
                            //    e.CellElement.ForeColor = Color.Black;
                        }

                    }

                    else if (e.Column.Name == "To")
                    {
                        if (e.Row.Cells["ToLocTypeId"].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        {

                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.DrawFill = true;


                            e.CellElement.BackColor = Color.FromArgb(-5374161);
                            e.CellElement.ForeColor = Color.Black;
                            //e.CellElement.BackColor = Color.FromArgb(e.Row.Cells["ToLocBgColor"].Value.ToInt());
                            //// e.CellElement.BackColor = Color.GreenYellow;
                            ////e.CellElement.ForeColor = Color.Black;


                            //if (e.Row.Cells["ToLocTextColor"].Value != null)
                            //{

                            //    e.CellElement.ForeColor = Color.FromArgb(e.Row.Cells["ToLocTextColor"].Value.ToInt());

                            //}
                            //else
                            //    e.CellElement.ForeColor = Color.Black;
                        }
                    }

                    else if (e.Column.Name == "Status")
                    {

                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.FromArgb(e.CellElement.RowInfo.Cells["StatusColor"].Value.ToInt());
                        e.CellElement.ForeColor = Color.Black;

                        e.CellElement.DrawFill = true;
                    }



                }
            }
            catch { }
        }



        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();

                GridViewRowInfo row = gridCell.RowElement.RowInfo;
                long id = row.Cells["Id"].Value.ToLong();

                int driverId = row.Cells["DriverId"].Value.ToInt();
                string driverNo = row.Cells["Driver"].Value.ToStr();

                bool rtn = false;

                int bookingStatusId = row.Cells["StatusId"].Value.ToInt();

                if (name == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {

                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();
                    }
                }
                else if (name == "btnrecall")
                {
                    if (driverId != 0 && (row.Cells["Status"].Value.ToStr() == "POB" || row.Cells["Status"].Value.ToStr() == "STC"))
                    {

                        ENUtils.ShowMessage("Job cannot be Re-Call as driver is on " + row.Cells["Status"].Value.ToStr() + " Status.");
                        return;

                    }
                    else if (driverId != 0 &&
                        (row.Cells["StatusId"].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED || row.Cells["StatusId"].Value.ToInt() == Enums.BOOKINGSTATUS.CANCELLED)
                        )
                    {


                        if (General.GetQueryable<Booking>(null).Count(c => c.Id == id && (c.AcceptedDateTime != null || c.Fleet_Driver != null && c.Fleet_Driver.HasPDA == true)) > 0)
                        {
                            ENUtils.ShowMessage("Job cannot be Re-Call as driver is on " + row.Cells["Status"].Value.ToStr() + " Status.");
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

                                if (bookingStatusId.ToInt() == Enums.BOOKINGSTATUS.FOJ)
                                {
                                    success = ReCallFOJBooking(id, driverId);
                                }
                                else if (bookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING_START)
                                {
                                    success = General.ReCallPreBooking(id, driverId);

                                }

                                else
                                {

                                    success = General.ReCallBooking(id, driverId);
                                }


                                if (success)
                                {
                                    break;

                                }
                                else
                                    loopCnt++;



                            }
                        }).Start();


                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_BookingLog(id, AppVars.LoginObj.UserName.ToStr(), "Recall Job from Driver (" + driverNo + ")");
                        }
                    }
                    else
                    {

                        return;
                    }


                }
                else if (name == "btnredespatch")
                {



                    rtn = General.ShowReDespatchForm(General.GetObject<Booking>(c => c.Id == id));

                }

                if (name == "btnrecall" || name == "btnredespatch")
                {
                    if (name == "btnredespatch" && rtn == false)
                        return;


                    Thread.Sleep(500);
                    //                 PopulateData();


                    if (bWorker != null && bWorker.IsBusy == false)
                    {

                        bWorker.RunWorkerAsync();
                    }
                    //   bWorker.RunWorkerAsync();

                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshAllActiveData();




                    // General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                }
            }
            catch
            {

            }
        }


        public bool ReCallFOJBooking(long jobId, int driverId)
        {

            try
            {
                (new TaxiDataContext()).stp_UpdateJobStatus(jobId, Enums.BOOKINGSTATUS.WAITING);




                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {
                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Foj Job>>" + jobId + "=2");
                    }

                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Foj Job>>" + jobId + "=2");
                    }


                }

                return true;

            }
            catch
            {

                return true;
                //ENUtils.ShowMessage(ex.Message);


            }




        }




        private void AddCommandColumn(string name, string headerText, int width)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = width;

            col.UseDefaultText = true;
            col.DefaultText = headerText;
            col.Name = name;
            grdLister.Columns.Add(col);

        }



        void frmBookingsList_Load(object sender, EventArgs e)
        {
            grdLister.AllowColumnReorder = false;
            grdLister.AllowDrop = false;
            grdLister.AllowRowReorder = false;
            grdLister.AllowColumnResize = false;
            grdLister.AllowRowResize = false;

            this.InitializeForm("frmBooking");



        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ViewDetailForm();
        }

        private void ViewDetailForm()
        {

            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
                ShowBookingForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }


        private void ShowBookingForm(int id)
        {
            General.ShowBookingForm(id, true, "", "", Enums.BOOKING_TYPES.LOCAL);


            //frmBooking frm = new frmBooking();
            //frm.OnDisplayRecord(id);
            //frm.ControlBox = true;
            //frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            //frm.MaximizeBox = false;
            //frm.ShowDialog();

        }


        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new BookingBO();

                try
                {
                    long jobId = grdLister.CurrentRow.Cells["Id"].Value.ToLong();

                    objMaster.GetByPrimaryKey(jobId);

                    if (objMaster.Current != null)
                    {

                        int bookingStatusId = objMaster.Current.BookingStatusId.ToInt();
                        int driverId = objMaster.Current.DriverId.ToInt();

                        objMaster.DeletedBy = AppVars.LoginObj.UserName.ToStr();
                        objMaster.Delete(objMaster.Current);

                        //
                        if (driverId != 0 && (bookingStatusId == Enums.BOOKINGSTATUS.PENDING_START || bookingStatusId == Enums.BOOKINGSTATUS.PENDING))
                        {

                            new Thread(delegate()
                            {
                                int loopCnt = 1;
                                bool success = false;
                                while (loopCnt < 3)
                                {

                                    if (bookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING)
                                        success = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=deletedjobid>>" + jobId + "=2").Result.ToBool();

                                    else
                                        success = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=deletedfuturejobid>>" + jobId + "=2").Result.ToBool();

                                    if (success)
                                        break;

                                    else
                                        loopCnt++;

                                }
                            }).Start();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (objMaster.Errors.Count > 0)
                        ENUtils.ShowMessage(objMaster.ShowErrors());
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }
                    e.Cancel = true;

                }

            }
        }





        public override void RefreshData()
        {
            try
            {
                ClearFilter();
                SetDefaultDateCriteria();

                InitializeBWorker();

                if (bWorker.IsBusy == false)
                {

                    bWorker.RunWorkerAsync();
                }
            }
            catch
            {


            }
        }


        public override void PopulateData()
        {
            try
            {
                if (bWorker != null && bWorker.IsBusy)
                    return;



                int bookingstatusId = 0;


                if (optCancelledJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = 3;

                else if (optNoFares.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = 13;

                else if (optRejectedJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = 11;

                else if (optCompletedJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    bookingstatusId = Enums.BOOKINGSTATUS.DISPATCHED.ToInt();

                var data1 = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING
                    && c.BookingStatusId != Enums.BOOKINGSTATUS.ONHOLD && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING_WEBBOOKING
                    && c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING_WEBBOOKING && c.BookingStatusId != Enums.BOOKINGSTATUS.REJECTED_WEBBOOKING

                    && (c.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
                    && (bookingstatusId == 0 || (bookingstatusId == 13 && (c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP || c.BookingStatusId == Enums.BOOKINGSTATUS.NOSHOW || c.BookingStatusId == Enums.BOOKINGSTATUS.REJECTED)) || c.BookingStatusId == bookingstatusId))

                    .OrderByDescending(c => c.PickupDateTime);


                if (this.IsFind)
                {

                    string searchTxt = txtSearch.Text.ToLower().Trim();
                    string col = ddlColumns.Text.Trim().ToLower();

                    if (searchTxt.Length < 3)
                        searchTxt = string.Empty;


                    DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                    DateTime? toDate = dtpToDate.Value.ToDateTimeorNull();

                    bool col_name = false;
                    bool col_refNo = false;
                    bool col_telNo = false;
                    bool col_mobileno = false;
                    bool col_driver = false;
                    bool col_vehicle = false;
                    bool col_status = false;
                    bool col_pickupPoint = false;
                    bool col_destination = false;
                    bool col_subcompany = false;

                    if (col == "passenger")
                    {
                        col_name = true;
                    }
                    else if (col == "reference")
                    {
                        col_refNo = true;
                    }
                    else if (col == "telephone no")
                    {
                        col_telNo = true;
                    }

                    else if (col == "mobile no")
                    {
                        col_mobileno = true;
                    }

                    else if (col == "driver")
                    {
                        col_driver = true;
                    }

                    else if (col == "vehicle")
                    {
                        col_vehicle = true;
                    }

                    else if (col == "status")
                    {
                        col_status = true;
                    }

                    else if (col == "pickup point")
                    {
                        col_pickupPoint = true;
                    }

                    else if (col == "destination")
                    {
                        col_destination = true;
                    }
                    else if (col == "subcompany")
                    {
                        col_subcompany = true;
                    }

                    skip = 0;

                    //int cnt = data1.Count();
                    //if (skip + pageSize > cnt && cnt - pageSize > 0)
                    //    skip = cnt - pageSize;
                    //else if (cnt <= pageSize)
                    //    skip = 0;



                    var query = (from a in data1

                                 where


                                 (
                                 (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_refNo && (searchTxt == string.Empty || a.BookingNo.ToLower().Contains(searchTxt)))
                                     || (col_telNo && (a.CustomerPhoneNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_mobileno && (a.CustomerMobileNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_driver && (a.Fleet_Driver != null && a.Fleet_Driver.DriverNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))

                                  || (col_pickupPoint && (searchTxt == string.Empty || (a.FromDoorNo != null && a.FromDoorNo.ToLower().Contains(searchTxt)) || a.FromAddress.ToLower().Contains(searchTxt)))
                            || (col_destination && (searchTxt == string.Empty || (a.ToDoorNo != null && a.ToDoorNo.ToLower().Contains(searchTxt)) || a.ToAddress.ToLower().Contains(searchTxt)))



                                     || (col_vehicle && (a.Fleet_VehicleType != null && a.Fleet_VehicleType.VehicleType.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_status && (a.BookingStatus != null && a.BookingStatus.StatusName.ToLower().Contains(searchTxt)))

                                      || (col_subcompany && (a.SubcompanyId != null && a.Gen_SubCompany.CompanyName.ToLower().Contains(searchTxt)))

                                 )

                                  &&

                                  ((fromDate == null || a.PickupDateTime.Value >= fromDate) && (toDate == null || a.PickupDateTime.Value <= toDate))

                                 select new
                                 {
                                     Id = a.Id,
                                     Token = a.JobCode,
                                     RefNumber = a.BookingNo,
                                     BookingDate = a.BookingDate,
                                     PickupDate = a.PickupDateTime,
                                     Passenger = a.CustomerName,
                                     MobileNo = a.CustomerMobileNo != null && a.CustomerMobileNo != "" ? a.CustomerMobileNo : a.CustomerPhoneNo,
                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     Pickup = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromOther : a.FromOther,
                                     FromPostCode = a.FromPostCode,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     GoingTo = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToOther : a.ToOther,
                                     ToPostCode = a.ToPostCode,
                                     Fare = a.FareRate,
                                     PaymentMethod = a.Gen_PaymentType.PaymentType,
                                     AccountFare = a.CompanyPrice,
                                     CustomerFare = a.CompanyPrice,
                                     Account = a.OrderNo != null && a.OrderNo != string.Empty ? a.Gen_Company.CompanyName + " - " + a.OrderNo : a.Gen_Company.CompanyName,
                                     Driver = a.Fleet_Driver.DriverNo,
                                     DriverId = a.DriverId,
                                     Vehicle = a.Fleet_VehicleType.VehicleType,
                                     Status = a.BookingStatus.StatusName,
                                     StatusColor = a.BookingStatus.BackgroundColor,
                                     BookingTypeId = a.BookingTypeId,
                                     VehicleBgColor = a.Fleet_VehicleType.BackgroundColor,
                                     VehicleTextColor = a.Fleet_VehicleType.TextColor,
                                     BackgroundColor1 = a.Gen_Company.BackgroundColor,
                                     TextColor1 = a.Gen_Company.TextColor,

                                     FromLocTypeId = a.FromLocTypeId,
                                     ToLocTypeId = a.ToLocTypeId,
                                     SubCompanyBgColor = a.SubcompanyId != null ? a.Gen_SubCompany.BackgroundColor : -1,
                                     StatusId = a.BookingStatusId,
                                     BookingBackgroundColor = a.BookingType.BackgroundColor,
                                     FromLocBgColor = a.FromLocId != null ? a.Gen_Location1.BackgroundColor : -1,
                                     ToLocBgColor = a.ToLocId != null ? a.Gen_Location2.BackgroundColor : -1,
                                     FromLocTextColor = a.FromLocId != null ? a.Gen_Location1.TextColor : -1,
                                     ToLocTextColor = a.ToLocId != null ? a.Gen_Location2.TextColor : -1,
                                     IsAutoDespatch = a.AutoDespatch,
                                     IsBidding = a.IsBidding
                                 }).ToList();



                    grdLister.DataSource = query;


                }
                else
                {

                    int cnt = data1.Count();
                    if (skip + pageSize > cnt && cnt - pageSize > 0)
                        skip = cnt - pageSize;
                    else if (cnt <= pageSize)
                        skip = 0;


                    var query = (from a in data1


                                 select new
                                 {
                                     Id = a.Id,
                                     Token = a.JobCode,
                                     RefNumber = a.BookingNo,
                                     BookingDate = a.BookingDate,
                                     PickupDate = a.PickupDateTime,
                                     Passenger = a.CustomerName,
                                     MobileNo = a.CustomerMobileNo != null && a.CustomerMobileNo != "" ? a.CustomerMobileNo : a.CustomerPhoneNo,

                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     Pickup = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromOther : a.FromOther,
                                     FromPostCode = a.FromPostCode,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     GoingTo = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToOther : a.ToOther,
                                     ToPostCode = a.ToPostCode,


                                     Fare = a.FareRate,
                                     PaymentMethod = a.Gen_PaymentType.PaymentType,
                                     AccountFare = a.CompanyPrice,
                                     CustomerFare = a.CompanyPrice,
                                     Account = a.OrderNo != null && a.OrderNo != string.Empty ? a.Gen_Company.CompanyName + " - " + a.OrderNo : a.Gen_Company.CompanyName,
                                     Driver = a.Fleet_Driver.DriverNo,
                                     DriverId = a.DriverId,
                                     Vehicle = a.Fleet_VehicleType.VehicleType,
                                     Status = a.BookingStatus.StatusName,
                                     StatusColor = a.BookingStatus.BackgroundColor,
                                     BookingTypeId = a.BookingTypeId,
                                     VehicleBgColor = a.Fleet_VehicleType.BackgroundColor,
                                     VehicleTextColor = a.Fleet_VehicleType.TextColor,
                                     BackgroundColor1 = a.Gen_Company.BackgroundColor,
                                     TextColor1 = a.Gen_Company.TextColor,

                                     FromLocTypeId = a.FromLocTypeId,
                                     ToLocTypeId = a.ToLocTypeId,
                                     SubCompanyBgColor = a.SubcompanyId != null ? a.Gen_SubCompany.BackgroundColor : -1,
                                     StatusId = a.BookingStatusId,
                                     BookingBackgroundColor = a.BookingType.BackgroundColor,
                                     FromLocBgColor = a.FromLocId != null ? a.Gen_Location1.BackgroundColor : -1,
                                     ToLocBgColor = a.ToLocId != null ? a.Gen_Location2.BackgroundColor : -1,
                                     FromLocTextColor = a.FromLocId != null ? a.Gen_Location1.TextColor : -1,
                                     ToLocTextColor = a.ToLocId != null ? a.Gen_Location2.TextColor : -1,
                                     IsAutoDespatch = a.AutoDespatch,
                                     IsBidding = a.IsBidding
                                 }).Skip(skip).Take(pageSize).ToList();


                    grdLister.DataSource = query;


                }

                lblBookingCount.Text = "Booking(s) Found :" + grdLister.Rows.Count;


            }
            catch
            {


            }


        }




        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool()).Count() == 0) return;
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Selected Booking(s) ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    foreach (GridViewRowInfo row in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool()))
                    {
                        objMaster = new BookingBO();

                        objMaster.GetByPrimaryKey(row.Cells["Id"].Value.ToInt());
                        if (objMaster.Current != null)
                        {
                            objMaster.Delete(objMaster.Current);
                        }
                    }

                    PopulateData();
                }
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }


            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindBookings();
        }

        private void FindBookings()
        {

            lblBookingCount.Visible = true;
            Find();

        }

        //private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        //{

        //}



        private bool IsFind = false;

        private void Find()
        {

            this.IsFind = true;
            skip = 0;
            PopulateData();
        }


        private void ClearFilter()
        {

            skip = 0;
            this.IsFind = false;
            this.dtpFromDate.Value = null;
            this.dtpToDate.Value = null;
            this.txtSearch.Text = string.Empty;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            //lblBookingCount.Visible = false;
            ClearFilter();
            PopulateData();
        }

        int skip = 0;

        private void btnFirstRecords_Click(object sender, EventArgs e)
        {



            try
            {

                InitializeBWorker();

                if (bWorker.IsBusy == false)
                {
                    skip = 0;

                    bWorker.RunWorkerAsync();
                }


                btnFirstRecords.Enabled = false;

                Thread.Sleep(1000);

                btnFirstRecords.Enabled = true;
                //PopulateData();
            }
            catch
            {


            }
        }

        private void btnPreviousRecords_Click(object sender, EventArgs e)
        {

            try
            {





                InitializeBWorker();

                if (bWorker.IsBusy == false)
                {

                    if (skip - pageSize < 0)
                        skip = 0;
                    else
                        skip = skip - pageSize;

                    bWorker.RunWorkerAsync();
                }


                btnPreviousRecords.Enabled = false;

                Thread.Sleep(1000);

                btnPreviousRecords.Enabled = true;

            }
            catch
            {


            }
            // PopulateData();
        }

        private void btnNextRecord_Click(object sender, EventArgs e)
        {

            try
            {




                InitializeBWorker();

                if (bWorker.IsBusy == false)
                {
                    skip = skip + pageSize;

                    bWorker.RunWorkerAsync();
                }

                //  PopulateData();

                btnNextRecord.Enabled = false;

                Thread.Sleep(1000);

                btnNextRecord.Enabled = true;
            }
            catch
            {


            }
        }

        private void btnLastRecords_Click(object sender, EventArgs e)
        {




            try
            {




                InitializeBWorker();

                if (bWorker.IsBusy == false)
                {
                    int cnt = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING && c.BookingStatusId != Enums.BOOKINGSTATUS.ONHOLD && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING_WEBBOOKING && c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING_WEBBOOKING).Count();

                    if (cnt <= pageSize)
                    {
                        skip = 0;

                    }
                    else if (cnt > pageSize)
                    {

                        skip = cnt - pageSize;

                    }

                    bWorker.RunWorkerAsync();
                }



                btnLastRecords.Enabled = false;

                Thread.Sleep(1000);

                btnLastRecords.Enabled = true;

            }
            catch
            {


            }
            //PopulateData();
        }


        private void btnExport_Click(object sender, EventArgs e)
        {

            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    if (radGridView1 == null)
                        InitializeExportGrid();


                    radGridView1.Columns.Clear();
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("PickupDate", "PickupDate"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("From", "From"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("To", "To"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("CompanyName", "CompanyName"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Customer", "Customer"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Telephone", "Telephone"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Driver", "Driver"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Vehicle", "Vehicle"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Price", "Price"));

                    //this.radGridView1.Columns["Telephone"].ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Text;
                    //this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Custom;

                    string searchTxt = txtSearch.Text.ToLower().Trim();
                    string col = ddlColumns.Text.Trim().ToLower();

                    if (searchTxt.Length < 3)
                        searchTxt = string.Empty;


                    DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                    DateTime? toDate = dtpToDate.Value.ToDateTimeorNull();

                    bool col_name = false;
                    bool col_refNo = false;
                    bool col_telNo = false;
                    bool col_mobileno = false;
                    bool col_driver = false;
                    bool col_vehicle = false;
                    bool col_status = false;
                    bool col_pickupPoint = false;
                    bool col_destination = false;
                    bool col_subcompany = false;
                    if (col == "passenger")
                    {
                        col_name = true;
                    }
                    else if (col == "reference")
                    {
                        col_refNo = true;
                    }
                    else if (col == "telephone no")
                    {
                        col_telNo = true;
                    }

                    else if (col == "mobile no")
                    {
                        col_mobileno = true;
                    }

                    else if (col == "driver")
                    {
                        col_driver = true;
                    }

                    else if (col == "vehicle")
                    {
                        col_vehicle = true;
                    }

                    else if (col == "status")
                    {
                        col_status = true;
                    }


                    else if (col == "pickup point")
                    {
                        col_pickupPoint = true;
                    }

                    else if (col == "destination")
                    {
                        col_destination = true;
                    }
                    else if (col == "subcompany")
                    {
                        col_subcompany = true;
                    }


                    int bookingstatusId = 0;


                    if (optCancelledJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                        bookingstatusId = 3;

                    else if (optNoFares.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                        bookingstatusId = 13;

                    else if (optRejectedJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                        bookingstatusId = 11;

                    var data1 = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING
                        && c.BookingStatusId != Enums.BOOKINGSTATUS.ONHOLD && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING_WEBBOOKING
                        && c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING_WEBBOOKING && c.BookingStatusId != Enums.BOOKINGSTATUS.REJECTED_WEBBOOKING
                        && (c.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
                        && bookingstatusId == 0 || c.BookingStatusId == bookingstatusId)
                                   .OrderByDescending(c => c.PickupDateTime);


                    //var data1 = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING && c.BookingStatusId != Enums.BOOKINGSTATUS.ONHOLD && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING_WEBBOOKING && c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING_WEBBOOKING
                    //                                             && (c.SubcompanyId == AppVars.DefaultSubCompanyId || AppVars.DefaultSubCompanyId == 0))
                    //                                        .OrderByDescending(c => c.PickupDateTime).AsEnumerable();


                    var query = (from a in data1
                                 where

                              //   (fromDate != null ||

                        (
                        (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_refNo && (a.BookingNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_telNo && (a.CustomerPhoneNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_mobileno && (a.CustomerMobileNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_driver && (a.Fleet_Driver != null && a.Fleet_Driver.DriverName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))

                            || (col_pickupPoint && (searchTxt == string.Empty || (a.FromDoorNo != null && a.FromDoorNo.ToLower().Contains(searchTxt)) || a.FromAddress.ToLower().Contains(searchTxt)))
                            || (col_destination && (searchTxt == string.Empty || (a.ToDoorNo != null && a.ToDoorNo.ToLower().Contains(searchTxt)) || a.ToAddress.ToLower().Contains(searchTxt)))


                            || (col_vehicle && (a.Fleet_VehicleType != null && a.Fleet_VehicleType.VehicleType.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_status && (a.BookingStatus != null && a.BookingStatus.StatusName.ToLower().Contains(searchTxt)))
                              || (col_subcompany && (a.SubcompanyId != null && a.Gen_SubCompany.CompanyName.ToLower().Contains(searchTxt)))

                        )
                            && ((fromDate == null || a.PickupDateTime.Value >= fromDate) && (toDate == null || a.PickupDateTime.Value <= toDate))

                                 select new
                                 {

                                     PickupDate = a.PickupDateTime,
                                     //  PickupDate = " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", a.PickupDateTime) + "  ",
                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     CompanyName = a.CompanyId != null ? a.Gen_Company.CompanyName : "",
                                     Customer = a.CustomerName,
                                     Telephone = (a.CustomerPhoneNo != string.Empty ? "" + a.CustomerPhoneNo + "" : "" + a.CustomerMobileNo + "").ToString(),
                                     //Telephone = a.CustomerPhoneNo != string.Empty ? " " +  a.CustomerPhoneNo + " " : " " +  a.CustomerMobileNo + " ",
                                     //  Telephone = a.CustomerPhoneNo != string.Empty ? " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", a.CustomerPhoneNo) + " " : " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", a.CustomerMobileNo) + " ",
                                     Driver = a.Fleet_Driver != null ? a.Fleet_Driver.DriverNo : "",
                                     Vehicle = a.Fleet_VehicleType != null ? a.Fleet_VehicleType.VehicleType : "",
                                     Price = a.FareRate
                                 }).ToList();

                    //   radGridView1.DataSource = query;


                    radGridView1.RowCount = query.Count;
                    for (int i = 0; i < query.Count; i++)
                    {
                        radGridView1.Rows[i].Cells["PickupDate"].Value = " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", query[i].PickupDate) + "  ";
                        radGridView1.Rows[i].Cells["From"].Value = query[i].From.Replace("=", "").Trim();
                        radGridView1.Rows[i].Cells["To"].Value = query[i].To.Replace("=", "").Trim();
                        radGridView1.Rows[i].Cells["CompanyName"].Value = query[i].CompanyName.Replace("=", "").Trim();
                        radGridView1.Rows[i].Cells["Customer"].Value = query[i].Customer.Replace("=", "").Trim();
                        radGridView1.Rows[i].Cells["Telephone"].Value = string.Format(" {0:F0} ", query[i].Telephone) + "."; //" " + string.Format(" {0:dd/MM/yyyy HH:mm} ", query[i].Telephone.Replace("=", "").Trim()) + " ";
                        radGridView1.Rows[i].Cells["Driver"].Value = query[i].Driver;
                        radGridView1.Rows[i].Cells["Vehicle"].Value = query[i].Vehicle;
                        radGridView1.Rows[i].Cells["Price"].Value = query[i].Price;
                        //  radGridView1.Rows[i].Cells["Telephone"].ViewInfo.;
                        //radGridView1.Rows[i].Cells["Telephone"].Value = (radGridView1.Rows[i].Cells["Telephone"].Value.ToString().Replace(".", ""));
                    }

                    // this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Fixed;
                    //this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Custom;
                    //ExportToExcelML export = new ExportToExcelML(this.radGridView1);
                    //export.ExportVisualSettings = true;
                    //export.HiddenColumnOption = HiddenOption.ExportAsHidden;
                    //export.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                    //export.ExcelCellFormatting += new Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventHandler(export_ExcelCellFormatting);
                    ////

                    //
                    radGridView1.Columns["PickupDate"].HeaderText = "Pickup Date-Time";

                    radGridView1.Columns["From"].HeaderText = "Pick-up Address";
                    radGridView1.Columns["To"].HeaderText = "Drop-off Address";
                    radGridView1.Columns["CompanyName"].HeaderText = "Account";

                    //Me.RadGridView1.Columns(0).ExcelExportType = Export.DisplayFormatType.Text

                    //CompanyName
                    exporter = new RadGridViewExcelExporter();

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                    worker.RunWorkerAsync(saveFileDialog1.FileName);
                    exporter.Progress += new ProgressHandler(exportProgress);

                    this.btnExport.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void export_ExcelCellFormatting(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {

            e.ExcelStyleElement.AlignmentElement.WrapText = false;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.IsDisposed)
            {
                e.Cancel = true;
                return;
            }

            exporter.Export(this.radGridView1, (String)e.Argument, "Booking List");


        }

        //Update the progress bar with the export progress    
        private void exportProgress(object sender, ProgressEventArgs e)
        {

            if (this.IsDisposed)
                return;
            // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(
                    delegate
                    {
                        if (e.ProgressValue <= 100)
                        {
                            radProgressBar1.Value1 = e.ProgressValue;
                        }
                        else
                        {
                            radProgressBar1.Value1 = 100;
                        }
                    }));
                }
                else
                {
                    if (e.ProgressValue <= 100)
                    {
                        radProgressBar1.Value1 = e.ProgressValue;
                    }
                    else
                    {
                        radProgressBar1.Value1 = 100;
                    }
                }
            }
            catch
            {

            }
        }

        void AuditReport_Click(object sender, EventArgs e)
        {
            try
            {
                RadMenuItem item = (RadMenuItem)sender;
                GridViewRowInfo row = grdLister.CurrentRow;

                if (row != null && row is GridViewDataRowInfo)
                {
                    int BookingId = row.Cells["Id"].Value.ToInt();

                    //  Booking_Log obj = General.GetObject<Booking_Log>(c => c.Id == BookingId);


                    var list = General.GetQueryable<Booking_Log>(c => c.BookingId == BookingId).OrderBy(c => c.UpdateDate).ToList();


                    if (list != null)
                    {
                        if (list.Count > 0)
                        {

                            frmBookingAudit frmbookingaudit = new frmBookingAudit(list, row.Cells["Id"].Value.ToStr());
                            frmbookingaudit.StartPosition = FormStartPosition.CenterScreen;
                            frmbookingaudit.ShowDialog();
                            frmbookingaudit.Dispose();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        // when the worker finishes the export, we can do some post processing   
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {



            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;

            ENUtils.ShowMessage("Export successfully.");

        }

        private void btnPrintSelected_Click(object sender, EventArgs e)
        {
            PrintBooking();
        }

        private void PrintBooking()
        {
            try
            {
                UM_Form_Template objReport = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmJobDetails" && c.IsDefault == true);

                if (grdLister.Rows.Count(c => c.Cells["Check"].Value.ToBool()) > 0)
                {
                    foreach (var item in grdLister.Rows)
                    {
                        if (item.Cells["Check"].Value.ToBool() == true)
                        {

                            long Id = item.Cells["Id"].Value.ToLong();
                            if (Id > 0)
                            {
                                var list = General.GetQueryable<Vu_BookingDetail>(c => c.Id == Id || c.MasterJobId == Id).ToList();
                                rptfrmJobDetails frm = null;
                                rptfrmJobDetails2 frm2 = null;
                                rptfrmJobDetails3 frm3 = null;
                                rptfrmJobDetails4 frm4 = null;
                                ReportPrintDocument rpt = null;
                                if (objReport != null)
                                {
                                    switch (objReport.TemplateValue)
                                    {
                                        case "rptfrmJobDetails":
                                            frm = new rptfrmJobDetails();
                                            frm.DataSource = list;
                                            frm.GenerateReport();
                                            rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                                            rpt.Print();
                                            rpt.Dispose();
                                            break;


                                        case "rptfrmJobDetails2":
                                            frm2 = new rptfrmJobDetails2();
                                            frm2.DataSource = list;
                                            frm2.GenerateReport();
                                            rpt = new ReportPrintDocument(frm2.reportViewer1.LocalReport);
                                            rpt.Print();
                                            rpt.Dispose();
                                            break;
                                        case "rptfrmJobDetails3":
                                            frm3 = new rptfrmJobDetails3();
                                            frm3.DataSource = list;
                                            frm3.GenerateReport();
                                            rpt = new ReportPrintDocument(frm3.reportViewer1.LocalReport);
                                            rpt.Print();
                                            rpt.Dispose();
                                            break;


                                        case "rptfrmJobDetails4":
                                            frm4 = new rptfrmJobDetails4();
                                            frm4.DataSource = list;
                                            frm4.GenerateReport();
                                            rpt = new ReportPrintDocument(frm4.reportViewer1.LocalReport);
                                            rpt.Print();
                                            rpt.Dispose();
                                            break;

                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ENUtils.ShowMessage("Please select booking to print");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void chkSelectAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SelectAll(args.ToggleState);
        }
        public void SelectAll(ToggleState toggle)
        {
            try
            {
                bool SelectAll = toggle == ToggleState.On;

                foreach (var item in grdLister.ChildRows)
                {
                    item.Cells["Check"].Value = SelectAll;
                }
                //grdLister.Rows.ToList().ForEach(c => c.Cells["COLCheckBox"].Value = SelectAll);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }



        }




        private void dtpFromDate_Opened(object sender, EventArgs e)
        {

            dtpFromDate.Tag = dtpFromDate.DateTimePickerElement.Value.TimeOfDay;

            if (dtpFromDate.Tag != null)
            {
                dtpFromDate.DateTimePickerElement.TextBoxElement.Text = dtpFromDate.DateTimePickerElement.Value.ToStr();
                // dtpFromDate.Value = dtpFromDate.DateTimePickerElement.Value.ToDate() + (TimeSpan)dtpFromDate.Tag;
            }
        }



        private void dtpFromDate_Closed(object sender, RadPopupClosedEventArgs args)
        {
            if (dtpFromDate.Tag != null)
            {
                dtpFromDate.Value = dtpFromDate.DateTimePickerElement.Value.ToDate() + (TimeSpan)dtpFromDate.Tag;
            }


        }


        private void DtpToDate_Opened(object sender, EventArgs e)
        {

            dtpToDate.Tag = dtpToDate.DateTimePickerElement.Value.TimeOfDay;

            if (dtpToDate.Tag != null)
            {
                dtpToDate.DateTimePickerElement.TextBoxElement.Text = dtpToDate.DateTimePickerElement.Value.ToStr();
                // dtpFromDate.Value = dtpFromDate.DateTimePickerElement.Value.ToDate() + (TimeSpan)dtpFromDate.Tag;
            }

        }



        private void dtpToDate_Closed(object sender, RadPopupClosedEventArgs args)
        {
            if (dtpToDate.Tag != null)
            {
                dtpToDate.Value = dtpToDate.DateTimePickerElement.Value.ToDate() + (TimeSpan)dtpToDate.Tag;
            }

        }


        private void TrackDriver( long jobID)
        {
            try
            {
                string baseUrl = "http://cabtreasureappapi.co.uk/CabTreasureJobPoolAPI/JobPool.asmx";
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.DeferredLoadingEnabled = false;

                    var objBooking = db.Bookings.FirstOrDefault(m => m.Id == jobID);


                    if (objBooking.CompanyId != null)
                        objBooking.CompanyCreditCardDetails = db.Gen_Companies.FirstOrDefault(c => c.Id == objBooking.CompanyId).DefaultIfEmpty().CompanyName.ToStr();

                    objBooking.CustomerCreditCardDetails = db.Fleet_VehicleTypes.FirstOrDefault(c => c.Id == objBooking.VehicleTypeId).DefaultIfEmpty().VehicleType.ToStr();

                    Taxi_AppMain.Classes.WebService JobPoolAPI = new Taxi_AppMain.Classes.WebService(baseUrl);
                    JobPoolAPI.PreInvoke();
                    var bookingInformation = Newtonsoft.Json.JsonConvert.SerializeObject(objBooking);
                    JobPoolAPI.AddParameter("BookingID", jobID.ToStr());
                    //JobPoolAPI.AddParameter("clientName", clientName);
                    //JobPoolAPI.AddParameter("bookingInformation", bookingInformation);
                    //JobPoolAPI.AddParameter("BroadcastClientsID", string.Empty);
                    try
                    {
                        JobPoolAPI.Invoke("GetTrackDirverLink");
                    }
                    finally { JobPoolAPI.PosInvoke(); }

                    var result = JobPoolAPI.ResultString;

                    using (System.Data.DataSet ds = new System.Data.DataSet())
                    {
                        ds.ReadXml(new XmlTextReader(new StringReader(result)));

                        if (ds.Tables["GetTrackDirverLinkResult"] != null && ds.Tables["GetTrackDirverLinkResult"].Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(ds.Tables["GetTrackDirverLinkResult"].Rows[0]["HasError"]) == false)
                            {
                                string link=  ds.Tables["GetTrackDirverLinkResult"].Rows[0]["Data"].ToStr();
                                Process.Start("chrome.exe", link);


                                //   db.stp_RunProcedure("update Booking set BookingStatusId=25,FlightDepartureDate=getdate() where Id=" + jobID);
                                //   General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                                //  RefreshActiveData();
                            }
                            else
                            {
                                ENUtils.ShowMessage(ds.Tables["GetTrackDirverLinkResult"].Rows[0]["Message"].ToStr());
                            }
                        }
                        else
                        {
                            ENUtils.ShowMessage("Job Transfer failed.");
                        }
                    }
                }
            }
            catch (Exception exe)
            {
                ENUtils.ShowMessage(exe.Message);
            }
        }



    }
}

