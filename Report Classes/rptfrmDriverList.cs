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
    public partial class rptfrmDriverList : UI.SetupBase
    {
        List<Gen_Syspolicy_DriverDocumentList> listofDocs = new List<Gen_Syspolicy_DriverDocumentList>();
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

            public static string Id = "Id";
            public static string FirstName = "FirstName";
            public static string SurName = "SurName";
            public static string DriverNo = "DriverNo";
            public static string PHCBadge = "PHCBadge";
            public static string PHCExp = "PHCExp";
            public static string Address = "Address";
            public static string MobileNo = "MobileNo";
            public static string LicenceNo = "LicenceNo";
            public static string LicenceExp = "LicenceExp";
            public static string NINo = "NINo";

        }

        public rptfrmDriverList()
        {
            InitializeComponent();
           
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            this.Load += new EventHandler(rptfrmCustomerAppUsers_Load);
            chkAll.CheckedChanged += new EventHandler(chkAll_CheckedChanged);
         
        }

        void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                if (grdLister.Rows.Count > 0)
                {
                    for (int i = 0; i < grdLister.Rows.Count; i++)
                    {
                        grdLister.Rows[i].Cells["Check"].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (chkAll.Checked == false)
            {
                if (grdLister.Rows.Count > 0)
                {
                    for (int i = 0; i < grdLister.Rows.Count; i++)
                    {
                        grdLister.Rows[i].Cells["Check"].Value = false;//..CurrentCell.Value;

                    }
                }
            }
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

        private void StopTimer()
        {
            timer1.Stop();

        }

        private void StartTimer()
        {
            timer1.Start();

        }

        void rptfrmCustomerAppUsers_Load(object sender, EventArgs e)
        {
            chkAll.Checked = true;
            grdLister.AllowEditRow = true;

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = "Check";
            cbcol.Width = 50;
            grdLister.Columns.Add(cbcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.HeaderText = "Id";
            col.Name = "Id";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "First Name";
            col.Name = COLS.FirstName;
            col.Width = 120;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.HeaderText = "Surname";
            col.ReadOnly = true;
            col.Width = 100;
            col.Name = COLS.SurName;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Driver No";
            col.Width = 120;
            col.Name = COLS.DriverNo;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "PHC Badge";
            col.Name = COLS.PHCBadge;
            col.Width = 120;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.IsVisible = true;
            colDate.ReadOnly = false;
            colDate.HeaderText = "PHC Exp";
            colDate.Width = 120;
            colDate.Name = COLS.PHCExp;
            grdLister.Columns.Add(colDate);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = false;
            col.HeaderText = "Address";
            col.Width = 170;
            col.Name = COLS.Address;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = false;
            col.HeaderText = "Mobile No";
            col.Width = 120;
            col.Name = COLS.MobileNo;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = false;
            col.HeaderText = "Licence No";
            col.Width = 100;
            col.Name = COLS.LicenceNo;
            grdLister.Columns.Add(col);

            colDate = new GridViewDateTimeColumn();
            colDate.IsVisible = true;
            colDate.ReadOnly = false;
            colDate.HeaderText = "Licence Exp";
            colDate.Width = 120;
            colDate.Name = COLS.LicenceExp;
            grdLister.Columns.Add(colDate);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = false;
            col.HeaderText = "NI No";
            col.Width = 80;
            col.Name = COLS.NINo;
            grdLister.Columns.Add(col);



            (grdLister.Columns["LicenceExp"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            (grdLister.Columns["LicenceExp"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

            (grdLister.Columns["PHCExp"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            (grdLister.Columns["PHCExp"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

            grdLister.EnableFiltering = true;
            grdLister.ShowFilteringRow = true;


            ViewReport();
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

            if (e.CellElement.RowElement.IsSelected == true)
            {

                if (e.Column.Name != "PHCExp" && e.Column.Name != "PHCExp" && e.Column.Name != "LicenseExp"
                   )
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

            }


            else
            {

                if (e.Column.Name != "PHCExp" && e.Column.Name != "LicenseExpiry"
                    )
                {

                    e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);
                }
            }

        }

        public override void Print()
        {
            try
            {


                var list = (from row in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true)
                            select new
                                {
                                    Id = row.Cells[COLS.Id].Value.ToInt(),
                                    DriverName = row.Cells[COLS.FirstName].Value.ToStr(),
                                    Surname = row.Cells[COLS.SurName].Value.ToStr(),
                                    DriverNo = row.Cells[COLS.DriverNo].Value.ToStr(),
                                    PHCBadge = row.Cells[COLS.PHCBadge].Value.ToStr(),
                                    PHCExp = row.Cells[COLS.PHCExp].Value.ToDateorNull(),
                                    Address = row.Cells[COLS.Address].Value.ToStr(),
                                    MobileNo = row.Cells[COLS.MobileNo].Value.ToStr(),
                                    LicenseNo = row.Cells[COLS.LicenceNo].Value.ToStr(),
                                    LicenseExpiry = row.Cells[COLS.LicenceExp].Value.ToDateorNull(),
                                    NI = row.Cells[COLS.NINo].Value.ToStr()
                                }).ToList();



                rptfrmDriverListReportViewer frm = new rptfrmDriverListReportViewer();

                //frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                //stp_GetDriversListReportResult
                List<stp_GetDriversListReportResult> obj = new List<stp_GetDriversListReportResult>();

                foreach (var item in list)
                {


                    obj.Add(new stp_GetDriversListReportResult
                    {
                        Id = item.Id,
                        Address = item.Address,
                        DriverName = item.DriverName,
                        DriverNo = item.DriverNo,
                        LicenseExpiry = item.LicenseExpiry,
                        LicenseNo = item.LicenseNo,
                        MobileNo = item.MobileNo,
                        NI = item.NI,
                        PHCExp = item.PHCExp,
                        PHCBadge = item.PHCBadge
                    });
                }
                // frm.DataSource = //GetDataSource().ToList();
             

                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverListReportViewer1");

                if (doc != null)
                {
                    doc.Close();
                }
                frm.DataSource = obj;
                frm.GenerateReport();
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
            
        }


        private List<stp_GetDriversListReportResult> GetDataSource()
        {

            if (rdAllDriver.IsChecked == true)
            {

                List<stp_GetDriversListReportResult> objDriverList = new List<stp_GetDriversListReportResult>();

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    objDriverList = db.stp_GetDriversListReport(1).ToList();
                }

                return objDriverList;
            }
            else
            {
                List<stp_GetDriversListReportResult> objDriverList = new List<stp_GetDriversListReportResult>();


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    objDriverList = db.stp_GetDriversListReport(2).ToList();
                }
                return objDriverList;

            }

        }

        private void ViewReport()
        {

            chkAll.Checked = true;

            listofDocs = General.GetQueryable<Gen_Syspolicy_DriverDocumentList>(c => c.SysPolicyId == 1).ToList();

            var list = GetDataSource().ToList();

            lblTotalCustomer.Text = "Total Driver : " + list.Count.ToStr();

            grdLister.RowCount = list.Count;

            GridViewRowInfo row = null;

            for (int i = 0; i < list.Count; i++)
            {

                row = grdLister.Rows[i];

                row.Cells["Check"].Value = true;

                row.Cells[COLS.Id].Value = list[i].Id;
                row.Cells[COLS.FirstName].Value = list[i].DriverName;
                row.Cells[COLS.SurName].Value = list[i].Surname;
                row.Cells[COLS.DriverNo].Value = list[i].DriverNo;
                row.Cells[COLS.PHCBadge].Value = list[i].PHCBadge;
                row.Cells[COLS.PHCExp].Value = list[i].PHCExp.ToDateorNull();
                row.Cells[COLS.Address].Value = list[i].Address;
                row.Cells[COLS.MobileNo].Value = list[i].MobileNo;
                row.Cells[COLS.LicenceNo].Value = list[i].LicenseNo;
                row.Cells[COLS.LicenceExp].Value = list[i].LicenseExpiry.ToDateorNull();
                row.Cells[COLS.NINo].Value = list[i].NI;

            }

            grdLister.Refresh();

            PHCVehicleDays = listofDocs.FirstOrDefault(c => c.Id == 1).DefaultIfEmpty().ExpiryDays.ToInt();
            PHCDriverDays = listofDocs.FirstOrDefault(c => c.Id == 2).DefaultIfEmpty().ExpiryDays.ToInt();
            MOTDays = listofDocs.FirstOrDefault(c => c.Id == 3).DefaultIfEmpty().ExpiryDays.ToInt();
            InsuranceDays = listofDocs.FirstOrDefault(c => c.Id == 4).DefaultIfEmpty().ExpiryDays.ToInt();
            MOT2Days = listofDocs.FirstOrDefault(c => c.Id == 5).DefaultIfEmpty().ExpiryDays.ToInt();
            LicenseDays = listofDocs.FirstOrDefault(c => c.Id == 6).DefaultIfEmpty().ExpiryDays.ToInt();
            RoadTaxDays = listofDocs.FirstOrDefault(c => c.Id == 7).DefaultIfEmpty().ExpiryDays.ToInt();


            timer1.Start();
        }


        private void btnExportPDF_Click(object sender, EventArgs e)
        {


            try
            {

                var list = (from row in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true)
                            select new
                            {
                                Id = row.Cells[COLS.Id].Value.ToInt(),
                                DriverName = row.Cells[COLS.FirstName].Value.ToStr(),
                                Surname = row.Cells[COLS.SurName].Value.ToStr(),
                                DriverNo = row.Cells[COLS.DriverNo].Value.ToStr(),
                                PHCBadge = row.Cells[COLS.PHCBadge].Value.ToStr(),
                                PHCExp = row.Cells[COLS.PHCExp].Value.ToDateorNull(),
                                Address = row.Cells[COLS.Address].Value.ToStr(),
                                MobileNo = row.Cells[COLS.MobileNo].Value.ToStr(),
                                LicenseNo = row.Cells[COLS.LicenceNo].Value.ToStr(),
                                LicenseExpiry = row.Cells[COLS.LicenceExp].Value.ToDateorNull(),
                                NI = row.Cells[COLS.NINo].Value.ToStr()
                            }).ToList();



                rptfrmDriverListReportViewer frm = new rptfrmDriverListReportViewer();

                //frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                //stp_GetDriversListReportResult
                List<stp_GetDriversListReportResult> obj = new List<stp_GetDriversListReportResult>();

                foreach (var item in list)
                {


                    obj.Add(new stp_GetDriversListReportResult
                    {
                        Id = item.Id,
                        Address = item.Address,
                        DriverName = item.DriverName,
                        DriverNo = item.DriverNo,
                        LicenseExpiry = item.LicenseExpiry,
                        LicenseNo = item.LicenseNo,
                        MobileNo = item.MobileNo,
                        NI = item.NI,
                        PHCExp = item.PHCExp,
                        PHCBadge = item.PHCBadge
                    });
                }
                // frm.DataSource = //GetDataSource().ToList();


                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverListReportViewer1");

                if (doc != null)
                {
                    doc.Close();
                }
                frm.DataSource = obj;

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

            frmDriver frm = new frmDriver();
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

                var list = (from row in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true)
                            select new
                            {
                                Id = row.Cells[COLS.Id].Value.ToInt(),
                                DriverName = row.Cells[COLS.FirstName].Value.ToStr(),
                                Surname = row.Cells[COLS.SurName].Value.ToStr(),
                                DriverNo = row.Cells[COLS.DriverNo].Value.ToStr(),
                                PHCBadge = row.Cells[COLS.PHCBadge].Value.ToStr(),
                                PHCExp = row.Cells[COLS.PHCExp].Value.ToDateorNull(),
                                Address = row.Cells[COLS.Address].Value.ToStr(),
                                MobileNo = row.Cells[COLS.MobileNo].Value.ToStr(),
                                LicenseNo = row.Cells[COLS.LicenceNo].Value.ToStr(),
                                LicenseExpiry = row.Cells[COLS.LicenceExp].Value.ToDateorNull(),
                                NI = row.Cells[COLS.NINo].Value.ToStr()
                            }).ToList();



                rptfrmDriverListReportViewer frm = new rptfrmDriverListReportViewer();

                //frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                //stp_GetDriversListReportResult
                List<stp_GetDriversListReportResult> obj = new List<stp_GetDriversListReportResult>();

                foreach (var item in list)
                {


                    obj.Add(new stp_GetDriversListReportResult
                    {
                        Id = item.Id,
                        Address = item.Address,
                        DriverName = item.DriverName,
                        DriverNo = item.DriverNo,
                        LicenseExpiry = item.LicenseExpiry,
                        LicenseNo = item.LicenseNo,
                        MobileNo = item.MobileNo,
                        NI = item.NI,
                        PHCExp = item.PHCExp,
                        PHCBadge = item.PHCBadge
                    });
                }
                // frm.DataSource = //GetDataSource().ToList();


                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverListReportViewer1");

                if (doc != null)
                {
                    doc.Close();
                }
                frm.DataSource = obj;

                frm.GenerateReport();

                frm.ExportReportToExcel();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {


                string error = string.Empty;


                var list = (from row in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true)
                            select new
                            {
                                Id = row.Cells[COLS.Id].Value.ToInt(),
                                DriverName = row.Cells[COLS.FirstName].Value.ToStr(),
                                Surname = row.Cells[COLS.SurName].Value.ToStr(),
                                DriverNo = row.Cells[COLS.DriverNo].Value.ToStr(),
                                PHCBadge = row.Cells[COLS.PHCBadge].Value.ToStr(),
                                PHCExp = row.Cells[COLS.PHCExp].Value.ToDateorNull(),
                                Address = row.Cells[COLS.Address].Value.ToStr(),
                                MobileNo = row.Cells[COLS.MobileNo].Value.ToStr(),
                                LicenseNo = row.Cells[COLS.LicenceNo].Value.ToStr(),
                                LicenseExpiry = row.Cells[COLS.LicenceExp].Value.ToDateorNull(),
                                NI = row.Cells[COLS.NINo].Value.ToStr()
                            }).ToList();



                rptfrmDriverListReportViewer frm = new rptfrmDriverListReportViewer();

                //frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                //stp_GetDriversListReportResult
                List<stp_GetDriversListReportResult> obj = new List<stp_GetDriversListReportResult>();

                foreach (var item in list)
                {


                    obj.Add(new stp_GetDriversListReportResult
                    {
                        Id = item.Id,
                        Address = item.Address,
                        DriverName = item.DriverName,
                        DriverNo = item.DriverNo,
                        LicenseExpiry = item.LicenseExpiry,
                        LicenseNo = item.LicenseNo,
                        MobileNo = item.MobileNo,
                        NI = item.NI,
                        PHCExp = item.PHCExp,
                        PHCBadge = item.PHCBadge
                    });
                }
                // frm.DataSource = //GetDataSource().ToList();


                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverListReportViewer1");

                if (doc != null)
                {
                    doc.Close();
                }
                frm.DataSource = obj;
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

        int PHCVehicleDays = 0;
        int PHCDriverDays = 0;
        int MOTDays = 0;
        int InsuranceDays = 0;
        int MOT2Days = 0;
        int LicenseDays = 0;
        int RoadTaxDays = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.Columns.Count == 0) return;


                DateTime dtNow = DateTime.Now.ToDate();

                if ((AppVars.frmMDI.ActiveControl != null && AppVars.frmMDI.ActiveControl.Name.Equals("rptfrmDriverList1") == true))
                {

                    foreach (var item in grdLister.Rows)
                    {

                        if (item.Cells["PHCExp"].Value.ToDate() < dtNow)
                        {
                            item.Cells["PHCExp"].Style.BackColor = Color.Pink;
                            item.Cells["PHCExp"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["PHCExp"].Value.ToDate() < dtNow.AddDays(PHCVehicleDays))
                        {

                            if (item.Cells["PHCExp"].Style.BackColor == Color.White)
                            {

                                item.Cells["PHCExp"].Style.BackColor = Color.Orange;
                            }
                            else
                            {

                                item.Cells["PHCExp"].Style.BackColor = Color.White;
                            }

                            item.Cells["PHCExp"].Style.CustomizeFill = true;

                        }



                        if (item.Cells["LicenceExp"].Value.ToDate() < dtNow)
                        {
                            item.Cells["LicenceExp"].Style.BackColor = Color.Pink;
                            item.Cells["LicenceExp"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["LicenceExp"].Value.ToDate() < dtNow.AddDays(LicenseDays))
                        {

                            if (item.Cells["LicenceExp"].Style.BackColor == Color.White)
                            {

                                item.Cells["LicenceExp"].Style.BackColor = Color.PaleGoldenrod;
                            }
                            else
                            {

                                item.Cells["LicenceExp"].Style.BackColor = Color.White;
                            }

                            item.Cells["LicenceExp"].Style.CustomizeFill = true;

                        }


                    }

                }
            }
            catch (Exception ex)
            {


            }
        }


    }
}
