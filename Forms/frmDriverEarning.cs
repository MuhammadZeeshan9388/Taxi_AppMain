using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI;
using Utils;
using DAL;
using Taxi_BLL;
using Taxi_Model;

namespace Taxi_AppMain
{
    public partial class frmDriverEarning : Form
    {
        bool IsLoaded = false;
        Fleet_DriverQueueList obj = null;
        public int SelectedDriverId;
        public frmDriverEarning()
        {
            InitializeComponent();
           
            this.KeyDown += new KeyEventHandler(frmDriverEarning_KeyDown);
            this.Load += new EventHandler(frmDriverEarning_Load);
            this.Shown += new EventHandler(frmDriverEarning_Shown);
            this.ddlDriver.KeyDown += new KeyEventHandler(ddlDriver_KeyDown);
            this.ddlDriver.KeyUp += new KeyEventHandler(ddlDriver_KeyUp);
            this.ddlDriver.SelectedValueChanged += new EventHandler(ddlDriver_SelectedValueChanged);
       }

        void ddlDriver_SelectedValueChanged(object sender, EventArgs e)
        {
            if (IsLoaded != true)
            {
                return;
            }
            int DriverId = ddlDriver.SelectedValue.ToInt();
            if (DriverId == 0)
            {
                return;
            }
            ShowDriverJobsDetail();
        }
        private void ShowDriverJobsDetail()
        {
            if (rdoDefault.IsChecked)
            {
                ViewCurrentJobsDetail();
                ViewTodayJobsDetail();
                ViewWeeklyJobsDetail();
            }
            else
            {
                ViewCustomJobsDetail();
            }
        }
        void ddlDriver_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //int DriverId = 0;
                //DriverId = ddlDriver.SelectedValue.ToInt();

                //if (DriverId == 0)
                //{
                //    ENUtils.ShowMessage("Required : Driver");
                //    return;
                //}
                //ShowDriverJobsDetail();
                //ShowDriverEarning();
            }
        }

        void ddlDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int DriverId = 0;
                DriverId = ddlDriver.SelectedValue.ToInt();
                if (DriverId == 0)
                {
                    ENUtils.ShowMessage("Required : Driver");
                    return;
                }
                ShowDriverJobsDetail();
                // ShowDriverEarning();
            }
            else
            {

                string text = ddlDriver.DropDownListElement.TextBox.Text;
                if (text.ToStr().IsAlpha())
                {

                     //var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(c=>c.IsActive==true && c.DriverName.ToLower().StartsWith(text.ToLower()))
                      
                     //  orderby a.DriverNo
                     //  select new
                     //      {
                     //          Id = a.Id,
                     //          DriverName = a.DriverNo + " - " + a.DriverName

                     //      }).ToList();


                    //ddlDriver.DropDownListElement.AutoCompleteDataSource=list;
                    //ddlDriver.DropDownListElement.AutoCompleteDisplayMember="DriverName";
                    //ddlDriver.DropDownListElement.AutoCompleteValueMember="Id";

                    ddlDriver.ShowDropDown();
                }

            }
        }




        void frmDriverEarning_Shown(object sender, EventArgs e)
        {
            this.ddlDriver.FocusedElement.Focus();

            if (SelectedDriverId > 0)
            {
                ddlDriver.SelectedValue = SelectedDriverId;
                radPageView1.SelectedPage = radPageViewPage2;

            }
        }
        void frmDriverEarning_Load(object sender, EventArgs e)
        {
            FillCombo();
            DefaultDate();
            IsLoaded = true;
        }

        void frmDriverEarning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                //ShowDriverEarning();
            }
        }

        private void FillCombo()
        {
            ComboFunctions.FillDriverNoCombo(ddlDriver);
            ddlDriver.PopupOpened += new EventHandler(ddlDriver_PopupOpened);
           
        }

        void ddlDriver_PopupOpened(object sender, EventArgs e)
        {
        //    var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(c => c.IsActive == true && c.DriverName.ToLower().StartsWith(ddlDriver.Text.ToLower()))

        //                orderby a.DriverNo
        //                select new
        //                {
        //                    Id = a.Id,
        //                    DriverName = a.DriverNo + " - " + a.DriverName

        //                }).ToList();
        //    ddlDriver.DropDownListElement.AutoCompleteSuggest.AutoCompleteDataSource = list;
        //    ddlDriver.DropDownListElement.AutoCompleteSuggest.AutoCompleteDisplayMember = "DriverName";
        //    ddlDriver.DropDownListElement.AutoCompleteSuggest.AutoCompleteValueMember = "Id";
        //    ddlDriver.AutoCompleteDataSource = list;

        }
        private void DefaultDate()
        {
            dtpFrom.Value = DateTime.Now.GetStartOfCurrentWeek().ToDate();
            dtpTill.Value = DateTime.Now.ToDate();
        }
        private void rdoDefault_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            ShowEarning();
        }
        private void ShowEarning()
        {
            try
            {
                if (rdoDefault.IsChecked == true)
                {
                    //gpbCurrentEarning.Visible = true;
                    //gpbTodayEarning.Visible = true;
                    //gpbWeeklyEarning.Visible = true;
                    // gpbCurrentEarning.SendToBack();

                    radPageView1.Visible = true;
                    gpbCustomEarning.Visible = false;
                    lblCurrentEarned.Text = "£ 0.00";
                    lblCurrentJobs.Text = "0";
                    lblWeeklyJobs.Text = "0";
                    lblWeeklyEarned.Text = "£ 0.00";
                    lblTodayJobs.Text = "0";
                    lblTodayEarned.Text = "£ 0.00";
                    grdTodayEarning.Rows.Clear();
                    grdCurrentEarning.Rows.Clear();
                    grdWeeklyEarning.Rows.Clear();
                
                    lblCurrentJobsHeader.Text = "From";
                    lblWeeklyFrom.Text = "From";
                    lblCurrentJobsHeader.Text = "From";
                }
                else
                {
                    //gpbCurrentEarning.Visible = false;
                    //gpbTodayEarning.Visible = false;
                    //gpbWeeklyEarning.Visible = false;
                    //gpbCurrentEarning.BringToFront();
                    radPageView1.Visible = false;
                    gpbCustomEarning.Visible = true;
                    lblCustomEarned.Text = "£ 0.00";
                    lblCustomJobs.Text = "0";
                    //gpbCustomEarning.Text = "Earning From";
                    lblCustomEarning.Text = "Earning From";
                    DefaultDate();
                    ddlDriver.Focus();
                    grdCustomEarning.Rows.Clear();
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void rdoCustom_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            ShowEarning();
        }

        private void btnShowEarning_Click(object sender, EventArgs e)
        {
           // ShowDriverEarning();
        }
        private void ShowDriverEarning()
        {
            try
            {
                int DriverId = 0;
                DriverId = ddlDriver.SelectedValue.ToInt();

                if (DriverId == 0)
                {
                    ENUtils.ShowMessage("Required : Driver");
                    return;
                }
                if (rdoDefault.IsChecked == true)
                {
                    DefaultEarning(DriverId);
                }
                else
                {
                    CustomEarning(DriverId);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void CustomEarning(int DriverId)
        {
            try
            {
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                  obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
                  if (obj != null)
                  {

                      // loginDateTime = obj.LoginDateTime;
                      int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                      int? driverId = obj.DriverId.ToIntorNull();

                      decimal TotalEarning = 0;

                      var list = (from a in General.GetQueryable<Booking>(c => (c.PickupDateTime != null && c.PickupDateTime >= dtpFrom.Value.ToDate() && c.PickupDateTime <= dtpTill.Value.ToDate() + tillTime) && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                                                           && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.Fleet_Driver.DriverTypeId == driverType)
                                  select new
                                  {
                                      FareRate = a.FareRate
                                  }).ToList();



                      foreach (var item in list)
                      {
                          
                              TotalEarning += item.FareRate.ToDecimal();
                          
                      }
                      lblCustomEarned.Text = "£ " + TotalEarning;
                      lblCustomJobs.Text = list.Count().ToStr();
                      if (lblCustomEarned.Text.Contains(".") == false)
                      {
                          lblCustomEarned.Text = "£ " + TotalEarning+".00";
                      }
                      gpbCustomEarning.Text = "Earning From : " + string.Format("{0:dd/MM/yyyy}", dtpFrom.Value) + " to " + string.Format("{0:dd/MM/yyyy}", dtpTill.Value);
                      
                  }
                  else
                  {
                      lblCustomEarned.Text = "£ 0.00";
                      lblCustomJobs.Text = "0";
                  }
 
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void DefaultEarning(int DriverId)
        {
            try
            {
                DateTime? loginDateTime = null;
                DateTime dtFrom = DateTime.Now.AddDays(-7).ToDate();
                DateTime dtToday = DateTime.Now.ToDate();
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
 
                obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId && c.Status == true);
                if (obj != null)
                {
                    //  string vehicleNo = obj.FleetMasterId != null ? obj.Fleet_Master.Fleet_VehicleType.VehicleType + " - " + obj.Fleet_Master.Plateno.ToStr() : "";


                    loginDateTime = obj.LoginDateTime;
                    int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId = obj.DriverId.ToIntorNull();

                    decimal TotalEarning = 0;

                    var list = (from a in General.GetQueryable<Booking>(c => (c.PickupDateTime != null && c.PickupDateTime >= loginDateTime) && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                                                         && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.Fleet_Driver.DriverTypeId == driverType)
                                select new
                                {
                                    FareRate = a.FareRate
                                }).ToList();
                    foreach (var item in list)
                    {
                        if (item.FareRate != null)
                        {
                            TotalEarning += item.FareRate.ToDecimal();
                        }
                    }
                    lblCurrentEarned.Text = "£ " + TotalEarning;

                    lblCurrentJobs.Text =list.Count().ToStr();
                    if (lblCurrentEarned.Text.Contains(".") == false)
                    {
                        lblCurrentEarned.Text = "£ " + TotalEarning+".00";
                    }

                }
                else
                {
                    lblCurrentEarned.Text = "£ 0.00";

                    lblCurrentJobs.Text = "0";
                }
                
                
                obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
              

                if (obj != null)
                {
                    loginDateTime = obj.LoginDateTime;
                    int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId = obj.DriverId.ToIntorNull();

                    var listWeekly = (from a in General.GetQueryable<Booking>(c => (c.PickupDateTime != null && c.PickupDateTime >= dtFrom && c.PickupDateTime <= dtToday.ToDate() + tillTime) && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                                     && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.Fleet_Driver.DriverTypeId == driverType)
                                      select new
                                      {
                                          FareRate = a.FareRate
                                      }).ToList();
                    lblWeeklyJobs.Text = listWeekly.Count().ToStr();
                    decimal WeeklyEarning = 0;
                    foreach (var item in listWeekly)
                    {
                        if (item.FareRate != null)
                        {
                            WeeklyEarning += item.FareRate.ToDecimal();
                        }
                    }
                    lblWeeklyEarned.Text = "£ " + WeeklyEarning;
                    if (lblWeeklyEarned.Text.Contains(".") == false)
                    {
                        lblWeeklyEarned.Text = "£ " + WeeklyEarning + ".00";
                    }

                    var listToday = (from a in General.GetQueryable<Booking>(c => (c.PickupDateTime != null && c.PickupDateTime >= dtToday && c.PickupDateTime <= dtToday.ToDate() + tillTime) && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                                     && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.Fleet_Driver.DriverTypeId == driverType)
                                     select new
                                     {
                                         FareRate = a.FareRate
                                     }).ToList();
                    decimal TodayEarning = 0;
                    foreach (var item in listToday)
                    {
                        if (item.FareRate != null)
                        {
                            TodayEarning += item.FareRate.ToDecimal();
                        }
                    }
                    lblTodayJobs.Text = listToday.Count().ToStr();
                    lblTodayEarned.Text = "£ " + TodayEarning;
                    if (lblTodayEarned.Text.Contains(".") == false)
                    {
                        lblTodayEarned.Text = "£ " + TodayEarning+".00";
                    }

                }
                else
                {
                    lblWeeklyJobs.Text = "0";
                    lblWeeklyEarned.Text = "£ 0.00";
                    lblTodayJobs.Text = "0";
                    lblTodayEarned.Text = "£ 0.00";
                    
                }
            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnShowCurrentJobs_Click(object sender, EventArgs e)
        {
            try
            {
                int DriverId = 0;
                DateTime? loginDateTime = null;
                DateTime dtFrom = DateTime.Now.AddDays(-7).ToDate();
                DateTime dtToday = DateTime.Now.ToDate();
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                string DriverNo = string.Empty;
                DriverId = ddlDriver.SelectedValue.ToInt();
                if (DriverId == 0)
                {
                    ENUtils.ShowMessage("Required : Driver");
                    return;
                }
                obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId && c.Status == true);
                if (obj != null)
                {
                    loginDateTime = obj.LoginDateTime;
                    int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId = obj.DriverId.ToIntorNull();
                    DriverNo = obj.Fleet_Driver.DriverNo;
                   
                    var list = General.GetQueryable<Booking>(c => c.PickupDateTime >= obj.LoginDateTime
                            && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                          && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED).ToList();
                    if (list.Count > 0)
                    {
                        frmDriverJobs frmDrvJobs = new frmDriverJobs(list, DriverNo);
                        frmDrvJobs.StartPosition = FormStartPosition.CenterScreen;
                        frmDrvJobs.ShowDialog();
                        frmDrvJobs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnViewTodayJobs_Click(object sender, EventArgs e)
        {
            try
            {
                int DriverId = 0;
                DateTime? loginDateTime = null;
              //  DateTime dtFrom = DateTime.Now.AddDays(-7).ToDate();
                DateTime dtToday = DateTime.Now.ToDate();
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                string DriverNo = string.Empty;
                DriverId = ddlDriver.SelectedValue.ToInt();
                if (DriverId == 0)
                {
                    ENUtils.ShowMessage("Required : Driver");
                    return;
                }
                obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
                if (obj != null)
                {
                    loginDateTime = obj.LoginDateTime;
                    int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId = obj.DriverId.ToIntorNull();
                    DriverNo = obj.Fleet_Driver.DriverNo;

                    var list = General.GetQueryable<Booking>(c => c.PickupDateTime >=  dtToday && c.PickupDateTime <= dtToday.ToDate() + tillTime
                            && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                          && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED).ToList();
                    if (list.Count > 0)
                    {
                        frmDriverJobs frmDrvJobs = new frmDriverJobs(list, DriverNo);
                        frmDrvJobs.StartPosition = FormStartPosition.CenterScreen;
                        frmDrvJobs.ShowDialog();
                        frmDrvJobs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnViewWeeklyJobs_Click(object sender, EventArgs e)
        {
            try
            {
                int DriverId = 0;
                DateTime? loginDateTime = null;
                DateTime dtFrom = DateTime.Now.AddDays(-7).ToDate();
                DateTime dtToday = DateTime.Now.ToDate();
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                string DriverNo = string.Empty;
                DriverId = ddlDriver.SelectedValue.ToInt();
                if (DriverId == 0)
                {
                    ENUtils.ShowMessage("Required : Driver");
                    return;
                }
                obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
                if (obj != null)
                {
                    loginDateTime = obj.LoginDateTime;
                    int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId = obj.DriverId.ToIntorNull();
                    DriverNo = obj.Fleet_Driver.DriverNo;

                    var list = General.GetQueryable<Booking>(c => c.PickupDateTime >= dtFrom && c.PickupDateTime <= dtToday.ToDate() + tillTime
                            && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                          && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED).ToList();
                    if (list.Count > 0)
                    {
                        frmDriverJobs frmDrvJobs = new frmDriverJobs(list, DriverNo);
                        frmDrvJobs.StartPosition = FormStartPosition.CenterScreen;
                        frmDrvJobs.ShowDialog();
                        frmDrvJobs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnViewCustomJobs_Click(object sender, EventArgs e)
        {
            ViewCustomJobsDetail();
            //try
            //{
            //    int DriverId = 0;
            //    DateTime? loginDateTime = null;
            //    TimeSpan tillTime = TimeSpan.Zero;
            //    TimeSpan.TryParse("23:59:59", out tillTime);
            //    string DriverNo = string.Empty;
            //    DriverId = ddlDriver.SelectedValue.ToInt();
            //    if (DriverId == 0)
            //    {
            //        ENUtils.ShowMessage("Required : Driver");
            //        return;
            //    }
            //    obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
            //    if (obj != null)
            //    {
            //        loginDateTime = obj.LoginDateTime;
            //        int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

            //        int? driverId = obj.DriverId.ToIntorNull();
            //        DriverNo = obj.Fleet_Driver.DriverNo;

            //        var list = General.GetQueryable<Booking>(c => c.PickupDateTime >= dtpFrom.Value.ToDate() && c.PickupDateTime <= dtpTill.Value.ToDate() + tillTime
            //                && (c.DriverId == driverId || c.ReturnDriverId == driverId)
            //                && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED).ToList();
            //        if (list.Count > 0)
            //        {
            //            frmDriverJobs frmDrvJobs = new frmDriverJobs(list, DriverNo);
            //            frmDrvJobs.StartPosition = FormStartPosition.CenterScreen;
            //            frmDrvJobs.ShowDialog();
            //            frmDrvJobs.Dispose();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowMessage(ex.Message);
            //}
        }
        private void ViewCurrentJobsDetail()
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();
                DateTime? loginDateTime = null;
                DateTime dtFrom = DateTime.Now.AddDays(-7).ToDate();
                DateTime dtToday = DateTime.Now.ToDate();
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                string DriverNo = string.Empty;
              
               
                obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId && c.Status == true);
                if (obj != null)
                {
                    loginDateTime = obj.LoginDateTime;
                    int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId = obj.DriverId.ToIntorNull();
                    DriverNo = obj.Fleet_Driver.DriverNo;

                    var list = (from a in General.GetQueryable<Booking>(c => c.PickupDateTime >=obj.LoginDateTime
                            && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                          && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                                select new 
                                {
                                    Id = a.Id,
                                    RefNo=a.BookingNo,
                                    PickupDateTime=a.PickupDateTime,
                                    PickupPoint=a.FromAddress,
                                    Destination=a.ToAddress,
                                    Fare=a.FareRate,
                                   WaitingCharges= a.MeetAndGreetCharges,
                                   Parking=a.CongtionCharges
                                }).ToList();
                    //grdCurrentEarning.DataSource = list;

                    grdCurrentEarning.Rows.Clear();
                    if (list.Count > 0)
                    {
                        grdCurrentEarning.RowCount = list.Count;

                        if (grdCurrentEarning.RowCount < 15)
                        {

                            grdCurrentEarning.RowCount = (15 - grdCurrentEarning.RowCount) + list.Count;

                        }

                        for (int i = 0; i < list.Count; i++)
                        {
                            grdCurrentEarning.Rows[i].Cells["curId"].Value = list[i].Id;
                            grdCurrentEarning.Rows[i].Cells["curRefNo"].Value = list[i].RefNo;
                            grdCurrentEarning.Rows[i].Cells["curPickupDateTime"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", list[i].PickupDateTime);
                            grdCurrentEarning.Rows[i].Cells["curPickupPoint"].Value = list[i].PickupPoint;
                            grdCurrentEarning.Rows[i].Cells["curDestination"].Value = list[i].Destination;
                            grdCurrentEarning.Rows[i].Cells["curFare"].Value = list[i].Fare.ToDecimal() ;
                            grdCurrentEarning.Rows[i].Cells["curWaiting"].Value = list[i].WaitingCharges.ToDecimal();
                            grdCurrentEarning.Rows[i].Cells["curParking"].Value = list[i].Parking.ToDecimal();


                        }
                        lblCurrentEarned.Text = "£ " + list.Sum(c=>c.Fare + c.WaitingCharges);

                        txtCurParking.Text = "£ " + string.Format("{0:##0.00}", list.Sum(c => c.Parking));

                        lblCurrentJobs.Text = list.Count().ToStr();
                        if (lblCurrentEarned.Text.Contains(".") == false)
                        {
                            lblCurrentEarned.Text = "£ " + list.Sum(c => c.Fare + c.WaitingCharges) + ".00";
                        }
                        lblCurrentJobsHeader.Text = "From: " + string.Format("{0:dd/MM/yyyy HH:mm}", loginDateTime); ;
                    }
                    else
                    {
                        grdCurrentEarning.Rows.Clear();
                         lblCurrentEarned.Text = "£ 0.00";
                         lblCurrentJobsHeader.Text = "From";
                         lblCurrentJobs.Text = "0";
                    }
                     

                

                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    dataGridView1.Rows[i].Cells["Id"].Value = list[i].Id;
                    //    dataGridView1.Rows[i].Cells["RefNo"].Value = list[i].BookingNo;
                    //    dataGridView1.Rows[i].Cells["PickupDateTime"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", list[i].PickupDateTime);
                    //    dataGridView1.Rows[i].Cells["PickupPoint"].Value = list[i].FromAddress;
                    //    dataGridView1.Rows[i].Cells["Destination"].Value = list[i].ToAddress;
                    //    dataGridView1.Rows[i].Cells["Fare"].Value = list[i].FareRate.ToDecimal();
                    //}
                    //if (list.Count > 0)
                    //{
                    //    frmDriverJobs frmDrvJobs = new frmDriverJobs(list, DriverNo);
                    //    frmDrvJobs.StartPosition = FormStartPosition.CenterScreen;
                    //    frmDrvJobs.ShowDialog();
                    //    frmDrvJobs.Dispose();
                    //}
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void ViewTodayJobsDetail()
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();
             //   DateTime? loginDateTime = null;
                //  DateTime dtFrom = DateTime.Now.AddDays(-7).ToDate();
                DateTime dtToday = DateTime.Now.ToDate();
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                string DriverNo = string.Empty;
                
                //obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
                //if (obj != null)
                //{
                 //   loginDateTime = obj.LoginDateTime;
                  //  int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId = DriverId;
                //    DriverNo = obj.Fleet_Driver.DriverNo;

                    var list =(from a in General.GetQueryable<Booking>(c => c.PickupDateTime >= dtToday && c.PickupDateTime <= (dtToday.ToDate() + tillTime)
                            && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                          && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                               select new 
                                {
                                    Id = a.Id,
                                    RefNo=a.BookingNo,
                                    PickupDateTime=a.PickupDateTime,
                                    PickupPoint=a.FromAddress,
                                    Destination=a.ToAddress,
                                    Fare=a.FareRate,
                                       WaitingCharges = a.MeetAndGreetCharges,
                                       Parking=a.CongtionCharges
                               }).ToList();
                //grdTodayEarning.DataSource = list;

                grdTodayEarning.Rows.Clear();
                if (list.Count > 0)
                    {
                        grdTodayEarning.RowCount = list.Count;

                        if (grdTodayEarning.RowCount < 15)
                        {

                            grdTodayEarning.RowCount = (15 - grdTodayEarning.RowCount) + list.Count;

                        }

                        for (int i = 0; i < list.Count; i++)
                        {
                            grdTodayEarning.Rows[i].Cells["tId"].Value = list[i].Id;
                            grdTodayEarning.Rows[i].Cells["tRefNo"].Value = list[i].RefNo;
                            grdTodayEarning.Rows[i].Cells["tPickupDateTime"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", list[i].PickupDateTime);
                            grdTodayEarning.Rows[i].Cells["tPickupPoint"].Value = list[i].PickupPoint;
                            grdTodayEarning.Rows[i].Cells["tDestination"].Value = list[i].Destination;
                        grdTodayEarning.Rows[i].Cells["tFare"].Value = list[i].Fare.ToDecimal();
                        grdTodayEarning.Rows[i].Cells["tWaiting"].Value = list[i].WaitingCharges.ToDecimal();
                        grdTodayEarning.Rows[i].Cells["tParking"].Value = list[i].Parking.ToDecimal();



                    }
                          lblTodayJobs.Text = list.Count().ToStr();
                    lblTodayEarned.Text = "£ " + list.Sum(c=>c.Fare + c.WaitingCharges);
                    if (lblTodayEarned.Text.Contains(".") == false)
                    {
                        lblTodayEarned.Text = "£ " + list.Sum(c => (c.Fare + c.WaitingCharges)) + ".00";
                    }

                    txtTodayParking.Text = "£ " + string.Format("{0:##0.00}", list.Sum(c => c.Parking));
                    //dtToday && c.PickupDateTime <= (dtToday.ToDate() + tillTime
                    lblTodayJobsheader.Text = "From: " + string.Format("{0:dd/MM/yyyy}", dtToday) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", dtToday.ToDate() + tillTime);
                
                    }
                    else
                    {
                    //lblWeeklyJobs.Text = "0";
                    //lblWeeklyEarned.Text = "£ 0.00";
                    lblTodayJobsheader.Text = "From";
                        lblTodayJobs.Text = "0";
                        lblTodayEarned.Text = "£ 0.00";
                        grdTodayEarning.Rows.Clear();
                    }

             //   }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void ViewWeeklyJobsDetail()
        { 
        try
        {
            int DriverId = ddlDriver.SelectedValue.ToInt();
             //   DateTime? loginDateTime = null;
                DateTime dtFrom = DateTime.Now.AddDays(-7).ToDate();
                DateTime dtToday = DateTime.Now.ToDate();
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                string DriverNo = string.Empty;
               
                //obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
                //if (obj != null)
                //{
                 //   loginDateTime = obj.LoginDateTime;
                 //   int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                    int? driverId =DriverId;
                //    DriverNo = obj.Fleet_Driver.DriverNo;

                    var list =(from a in General.GetQueryable<Booking>(c => c.PickupDateTime >= dtFrom && c.PickupDateTime <= (dtToday.ToDate() + tillTime)
                            && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                          && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                          select new 
                                {
                                    Id = a.Id,
                                    RefNo=a.BookingNo,
                                    PickupDateTime=a.PickupDateTime,
                                    PickupPoint=a.FromAddress,
                                    Destination=a.ToAddress,
                                    Fare=a.FareRate,
                                       WaitingCharges = a.MeetAndGreetCharges,
                                       Parking=a.CongtionCharges
                          }).ToList();

                grdWeeklyEarning.Rows.Clear();
                if (list.Count > 0)
                    {
                        grdWeeklyEarning.RowCount = list.Count;

                        if (grdWeeklyEarning.RowCount < 15)
                        {

                            grdWeeklyEarning.RowCount = (15 - grdWeeklyEarning.RowCount) + list.Count;

                        }

                        for (int i = 0; i < list.Count; i++)
                        {
                            grdWeeklyEarning.Rows[i].Cells["Id"].Value = list[i].Id;
                            grdWeeklyEarning.Rows[i].Cells["RefNo"].Value = list[i].RefNo;
                            grdWeeklyEarning.Rows[i].Cells["PickupDateTime"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", list[i].PickupDateTime);
                            grdWeeklyEarning.Rows[i].Cells["PickupPoint"].Value = list[i].PickupPoint;
                            grdWeeklyEarning.Rows[i].Cells["Destination"].Value = list[i].Destination;
                            grdWeeklyEarning.Rows[i].Cells["Fare"].Value = list[i].Fare.ToDecimal();

                            grdWeeklyEarning.Rows[i].Cells["Waiting"].Value = list[i].WaitingCharges.ToDecimal();
                        grdWeeklyEarning.Rows[i].Cells["Parking"].Value = list[i].Parking.ToDecimal();
                    }
                        lblWeeklyJobs.Text = list.Count().ToStr();
                        lblWeeklyEarned.Text = "£ " + list.Sum(c=>c.Fare + c.WaitingCharges);
                    txtWeekParking.Text="£ "+string.Format("{0:##0.00}", list.Sum(c => c.Parking));
                    if (lblWeeklyEarned.Text.Contains(".") == false)
                        {
                            lblWeeklyEarned.Text = "£ " + list.Sum(c => c.Fare + c.WaitingCharges) + ".00";
                        }
                        lblWeeklyFrom.Text = "From: " + string.Format("{0:dd/MM/yyyy}", dtFrom) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", dtToday.ToDate() + tillTime);
                    }
                    else
                    {
                        lblWeeklyFrom.Text = "From";
                        lblWeeklyJobs.Text = "0";
                        lblWeeklyEarned.Text = "£ 0.00";
                        grdWeeklyEarning.Rows.Clear();
                    }
                
             //   }
            
        }
        catch (Exception ex)
        {
            ENUtils.ShowMessage(ex.Message);
        }
        }
        private void ViewCustomJobsDetail()
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();
               // DateTime? loginDateTime = null;
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                string DriverNo = string.Empty;
              
               // obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId == DriverId);
                //if (obj != null)
                //{
                  //  loginDateTime = obj.LoginDateTime;
                  //  int driverType = obj.Fleet_Driver.DefaultIfEmpty().DriverTypeId.ToInt();

                int? driverId = DriverId;
                  //  DriverNo = obj.Fleet_Driver.DriverNo;

                    var list =(from a in General.GetQueryable<Booking>(c => c.PickupDateTime >= dtpFrom.Value.ToDate() && c.PickupDateTime <= dtpTill.Value.ToDate() + tillTime
                            && (c.DriverId == driverId || c.ReturnDriverId == driverId)
                            && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                            select new 
                                {
                                    Id = a.Id,
                                    RefNo=a.BookingNo,
                                    PickupDateTime=a.PickupDateTime,
                                    PickupPoint=a.FromAddress,
                                    Destination=a.ToAddress,
                                    FareRate = a.FareRate,
                                WaitingCharges=a.MeetAndGreetCharges,
                                Parking=a.CongtionCharges
                            }).ToList();
                //grdCustomEarning.DataSource = list;
                grdCustomEarning.Rows.Clear();
                    if (list.Count > 0)
                    {
                        //grdCustomEarning.Rows.Clear();
                        //grdCustomEarning.DataSource = list;

                        grdCustomEarning.RowCount = list.Count;

                        if (grdCustomEarning.RowCount < 15)
                        {

                            grdCustomEarning.RowCount = (15 - grdCustomEarning.RowCount) + list.Count;

                        }

                        for (int i = 0; i < list.Count; i++)
                        {
                            grdCustomEarning.Rows[i].Cells["cusId"].Value = list[i].Id;
                            grdCustomEarning.Rows[i].Cells["cusRefNo"].Value = list[i].RefNo;
                            grdCustomEarning.Rows[i].Cells["cusPickupDateTime"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", list[i].PickupDateTime);
                            grdCustomEarning.Rows[i].Cells["cusPickupPoint"].Value = list[i].PickupPoint;
                            grdCustomEarning.Rows[i].Cells["cusDestination"].Value = list[i].Destination;
                            grdCustomEarning.Rows[i].Cells["cusFare"].Value = list[i].FareRate.ToDecimal();
                        grdCustomEarning.Rows[i].Cells["cusWaiting"].Value = list[i].WaitingCharges.ToDecimal();
                        grdCustomEarning.Rows[i].Cells["cusParking"].Value = list[i].Parking.ToDecimal();

                    }
                        lblCustomEarned.Text = "£ " + list.Sum(c=>c.FareRate + c.WaitingCharges);
                      lblCustomJobs.Text = list.Count().ToStr();
                    txtCusParking.Text = "£ " + string.Format("{0:##0.00}", list.Sum(c => c.Parking));

                    if (lblCustomEarned.Text.Contains(".") == false)
                      {
                          lblCustomEarned.Text = "£ " + list.Sum(c => c.FareRate + c.WaitingCharges) + ".00";
                      }
                      //gpbCustomEarning.Text = "Earning From : " + string.Format("{0:dd/MM/yyyy}", dtpFrom.Value) + " to " + string.Format("{0:dd/MM/yyyy}", dtpTill.Value);
                      lblCustomEarning.Text = "Earning From : " + string.Format("{0:dd/MM/yyyy}", dtpFrom.Value) + " to " + string.Format("{0:dd/MM/yyyy}", dtpTill.Value);
                 
                    }
                    else
                    {
                        lblCustomEarned.Text = "£ 0.00";
                        lblCustomJobs.Text = "0";
                        grdCustomEarning.Rows.Clear();
                    }
                
               // }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}
