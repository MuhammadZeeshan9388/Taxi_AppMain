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
using Utils;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using System.Collections;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class rptfrmCustomerAppUsers : UI.SetupBase
    {
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

        public struct COLS
        {

            public static string ID = "ID";
            public static string Name = "Name";
            public static string TelephoneNo = "TelephoneNo";
            public static string MobileNo = "MobileNo";
            public static string Address = "Address";
            public static string Email = "Email";
            public static string TotalJobs = "TotalJobs";
            public static string DoorNo = "DoorNo";

        }

        public rptfrmCustomerAppUsers()
        {
            InitializeComponent();
           
          
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
            TimeSpan tillTime = TimeSpan.Zero;
            TimeSpan.TryParse("23:59:59", out tillTime);
            dtpToDate.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue()).Date);
            dtptilltime.Value = dtpToDate.Value.Value.Date + tillTime;
            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            this.Load += new EventHandler(rptfrmCustomerAppUsers_Load);
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;

            if (gridCell.ColumnInfo.Name == "btnCreateBooking")
            {

                GridViewRowInfo row = grid.CurrentRow;
                if (row != null && row is GridViewRowInfo)
                {

                    if (gridCell.ColumnInfo.Name == "btnCreateBooking")
                    {

                        string phone = row.Cells["TelephoneNo"].Value.ToStr().Trim();
                            string mobileNo = row.Cells["MobileNo"].Value.ToStr().Trim();
                            string email = row.Cells["Email"].Value.ToStr().Trim();

                            General.ShowBookingForm(0, false, row.Cells["Name"].Value.ToStr(), phone, mobileNo,
                                                             row.Cells["DoorNo"].Value.ToStr(), row.Cells["Address"].Value.ToStr(), email);
                        
                    }
                }
            }
            else if (gridCell.ColumnInfo.Name == "ColEdit")
            {
                ViewDetailForm();
            }
        }
        RadDropDownMenu statsContextMenu = null;
       
        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                    return;

                if (statsContextMenu == null)
                {
                    statsContextMenu = new RadDropDownMenu();

                    RadMenuItem menu_showJobs = new RadMenuItem("Show Bookings");
                    menu_showJobs.ForeColor = Color.Blue;
                    menu_showJobs.BackColor = Color.Blue;
                    menu_showJobs.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    menu_showJobs.Click += new EventHandler(menu_showJobs_Click);
                    statsContextMenu.Items.Add(menu_showJobs);
                }

                e.ContextMenu = statsContextMenu;
            }
            catch (Exception ex)
            {
                //   ENUtils.ShowMessage(ex.Message);

            }
        }

        void menu_showJobs_Click(object sender, EventArgs e)
        {

            try
            {

                RadMenuItem item = (RadMenuItem)sender;
                GridViewRowInfo row = grdLister.CurrentRow;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.DeferredLoadingEnabled = false;

                    if (row != null && row is GridViewDataRowInfo)
                    {
                        string mobileNo = row.Cells["MobileNo"].Value.ToString();
                        string name = row.Cells["Name"].Value.ToString();
                        DateTime? fromDate = DateTime.Now.ToDate();
                        DateTime? tillDate = DateTime.Now.ToDate();

                        List<Booking> list = null;


                        if (rdAll.ToggleState == ToggleState.On)
                        {
                            fromDate = null;
                            tillDate = null;

                           // list = db.stp_AppUsers(fromDate, tillDate, 1).ToList(); // .Where(c => (c.CustomerMobileNo == mobileNo || c.CustomerPhoneNo == mobileNo) && c.CustomerName == name).ToList();
                            list = db.Bookings.Where(c => (c.CustomerMobileNo == mobileNo) && (c.CustomerName == name)).ToList();


                        }
                        else if (rdRegular.ToggleState == ToggleState.On)
                        {
                            fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
                            tillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            list = db.Bookings.Where(c => (c.CustomerMobileNo == mobileNo) && (c.CustomerName == name) &&
                                            (c.BookingDate.Value >= fromDate && c.BookingDate.Value <= tillDate)
                                      ).ToList();

                        }
                        else if (rdDateTime.ToggleState == ToggleState.On)
                        {


                            fromDate = dtpFromDate.Value.ToDateTimeorNull();
                            tillDate = dtpToDate.Value.ToDateTimeorNull();

                            if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                                fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();

                            if (tillDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                                tillDate = (tillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();


                            //list = General.GetQueryable<Booking>(c => (c.BookingDate.Value >= fromDate && c.BookingDate.Value <= tillDate)
                            //         && (c.CustomerMobileNo == mobileNo)).ToList();
                            //  list = db.Bookings.Where(c => c.CustomerMobileNo == mobileNo || c.CustomerPhoneNo == mobileNo).ToList();
                            list = db.Bookings.Where(c => (c.CustomerMobileNo == mobileNo) && (c.CustomerName == name)).ToList();


                        }

                        if (list.Count > 0)
                        {

                            frmDriverJobs frmDrvJobs = new frmDriverJobs(list, row.Cells["Name"].Value.ToStr(), "Customer");
                            frmDrvJobs.StartPosition = FormStartPosition.CenterScreen;
                            frmDrvJobs.ShowDialog();
                            frmDrvJobs.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("No Booking(s) Found");

                        }

                    }
                }

            }
            catch
            {


            }

                
        }

        void rptfrmCustomerAppUsers_Load(object sender, EventArgs e)
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.HeaderText = "ID";
            col.Name = "Id";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Name";
            col.Name = COLS.Name;
            col.Width = 270;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.HeaderText = "Phone";
            col.ReadOnly = true;
            col.Width = 210;
            col.Name = COLS.TelephoneNo;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Mobile No";
            col.Width = 260;
            col.Name = COLS.MobileNo;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Email";
            col.Name = COLS.Email;
            col.Width = 230;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = false;
            col.HeaderText = "Address";
            col.Width = 280;
            col.Name = COLS.Address;
            grdLister.Columns.Add(col);

            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.IsVisible = true;
            colD.ReadOnly = false;
            colD.HeaderText = "Total Bookings";
            colD.Width = 130;
            colD.Name = COLS.TotalJobs;
            grdLister.Columns.Add(colD);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.ReadOnly = false;
            col.Width = 100;
            col.Name = COLS.DoorNo;
            grdLister.Columns.Add(col);

            AddCreateBookingColumn(grdLister);
            AddEditColumn(grdLister);

            grdLister.EnableFiltering = true;
            grdLister.ShowFilteringRow = true;
        }

        private void AddCreateBookingColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 80;

            col.Name = "btnCreateBooking";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Create Booking";
            col.Width = 100;
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }


        public static void AddEditColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.BestFit();

            col.Name = "ColEdit";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Edit";
            col.Width = 70;
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

            else if (e.CellElement is GridDataCellElement)
            {

                e.CellElement.ToolTipText = e.CellElement.Text;
                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;

            }
        }

        public override void Print()
        {
            try
            {

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? toDate = dtpToDate.Value.ToDateorNull();

                if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();

                if (toDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    toDate = (toDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

                rptfrmCustomerAppUsersReport frm = new rptfrmCustomerAppUsersReport();

                frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                frm.DataSource = GetDataSource(fromDate, toDate);
              
                frm.GenerateReport();

                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmCustomerAppUsers");

                if (doc != null)
                {
                    doc.Close();
                }
                UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void btnViewReport_Click_1(object sender, EventArgs e)
        {
            ViewReport();
            btnSMS.Enabled = true;
        }


        private List<stp_AppUsersResult> GetDataSource(DateTime? From, DateTime? to)
        {

            if (rdAll.IsChecked == true)
            {

                List<stp_AppUsersResult> objAppUsers = new List<stp_AppUsersResult>();

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    objAppUsers = db.stp_AppUsers(From, to,1).OrderBy(c=>c.Name).ToList();                  
                }

                return objAppUsers; 
            }
            else if (rdRegular.IsChecked == true)
            {
                List<stp_AppUsersResult> objAppUsers = new List<stp_AppUsersResult>();
                From = null;
                to = null;

                From = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
                to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);           

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    objAppUsers = db.stp_AppUsers(From, to,2).OrderBy(c => c.Name).ToList();               
                }
                return objAppUsers;

            }

            else
            {
                if (From == null)
                    From = DateTime.Now.AddDays(-1000);

                if (to == null)
                    to = DateTime.Now.AddDays(1);


                List<stp_AppUsersResult> objAppUsers = new List<stp_AppUsersResult>();

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    objAppUsers = db.stp_AppUsers(From, to,3).OrderBy(c => c.Name).ToList();                 
                }

                return objAppUsers;

            }

        }

        private void ViewReport()
        {
            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime? toDate = dtpToDate.Value.ToDateorNull();

            if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();

            if (toDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                toDate = (toDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

            var list = GetDataSource(fromDate, toDate).ToList();

            lblTotalCustomer.Text = "Total Customer : " + list.Count.ToStr();
       
            grdLister.RowCount = list.Count;

            GridViewRowInfo row = null;

            for (int i = 0; i < list.Count; i++)
            {

                row = grdLister.Rows[i];

                row.Cells[COLS.ID].Value = list[i].Id;
                row.Cells[COLS.Name].Value = list[i].Name;
                //row.Cells[COLS.TelephoneNo].Value = list[i].TelephoneNo;
                row.Cells[COLS.MobileNo].Value = list[i].MobileNo;
                row.Cells[COLS.Email].Value = list[i].Email;
                row.Cells[COLS.Address].Value = list[i].Address1;
                row.Cells[COLS.TotalJobs].Value = list[i].TotalJobs;

            }

            lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Sum(c => c.Cells[COLS.TotalJobs].Value.ToInt());

            grdLister.Refresh();
        }


        private void btnExportPDF_Click(object sender, EventArgs e)
        {


            try
            {

                rptfrmCustomerAppUsersReport frm = new rptfrmCustomerAppUsersReport();

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? toDate = dtpToDate.Value.ToDateorNull();

                if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();

                if (toDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    toDate = (toDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

                frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);
                frm.DataSource = GetDataSource(fromDate,toDate);

                frm.GenerateReport();

                frm.ExportReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
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

            frmCustomer frm = new frmCustomer();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();

        }


        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {

                rptfrmCustomerAppUsersReport frm = new rptfrmCustomerAppUsersReport();

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? toDate = dtpToDate.Value.ToDateorNull();

                if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();

                if (toDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    toDate = (toDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

                frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                frm.DataSource = GetDataSource(fromDate,toDate);

                frm.GenerateReport();

                frm.ExportReportToExcel();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
         
            try
            {


                List<string> objList = new List<string>();
                var list = (from a in grdLister.Rows
                            select new {
                             MobileNo=a.Cells["MobileNo"].Value.ToStr()
                            }).ToList(); ;
                for (int i = 0; i < list.Count; i++)
                {
                    objList.Add(list[i].MobileNo);
                }


                frmSMSAll frm = new frmSMSAll(objList);

                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpToDate.Value.ToDate();


                if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();

                if (tillDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    tillDate = (tillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

                string error = string.Empty;


                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";
                }
                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }

                rptfrmCustomerAppUsersReport frm = new rptfrmCustomerAppUsersReport();
                frm.DataSource = GetDataSource(fromDate, tillDate);
                frm.GenerateReport();
                frm.SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
     

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }
  

        private void rdAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpFromDate.Enabled = false;
                dtpFromTime.Enabled = false;

                dtpToDate.Enabled = false;
                dtptilltime.Enabled = false;

            }

        }

        private void rdRegular_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpFromDate.Enabled = false;
                dtpFromTime.Enabled = false;

                dtpToDate.Enabled = false;
                dtptilltime.Enabled = false;

            }
        }

        private void rdDateTime_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpFromDate.Enabled = true;
                dtpFromTime.Enabled = true;

                dtpToDate.Enabled = true;
                dtptilltime.Enabled = true;

            }
        }

    }
}
