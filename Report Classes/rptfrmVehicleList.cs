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
    public partial class rptfrmVehicleList : UI.SetupBase
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
            public static string Make = "Make";
            public static string Model = "Model";
            public static string Colour = "Colour";
            public static string Reg = "Reg";
            public static string Owner = "Owner";
            public static string Address = "Address";
            public static string PHVExp = "PHVExp";
            public static string PHVBadge = "PHVBadge";
            public static string MOT = "MOT";
            public static string Insurance = "Insurance";
            public static string RoadTax = "RoadTax";

        }

        public rptfrmVehicleList()
        {
            InitializeComponent();
                               
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdLister.DoubleClick += new EventHandler(grdLister_DoubleClick);
            this.Load += new EventHandler(rptfrmCustomerAppUsers_Load);
            this.Load += new EventHandler(rptfrmVehicleList_Load);

        }

        void grdLister_DoubleClick(object sender, EventArgs e)
        {
            
        }

        void rptfrmVehicleList_Load(object sender, EventArgs e)
        {
           GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.HeaderText = "Id";
            col.Name = "Id";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Make";
            col.Name = COLS.Make;
            col.Width = 120;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.HeaderText = "Model";
            col.ReadOnly = true;
            col.Width = 80;
            col.Name = COLS.Model;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Color";
            col.Width = 80;
            col.Name = COLS.Colour;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Reg";
            col.Width = 80;
            col.Name = COLS.Reg;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = true;
            col.HeaderText = "Owner";
            col.Name = COLS.Owner;
            col.Width = 130;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = false;
            col.HeaderText = "Address";
            col.Width = 190;
            col.Name = COLS.Address;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.ReadOnly = false;
            col.HeaderText = "PHV Badge";
            col.Width = 130;
            col.Name = COLS.PHVBadge;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.IsVisible = true;
            colDate.ReadOnly = false;
            colDate.HeaderText = "PHV Exp";
            colDate.Width = 110;
            colDate.Name = COLS.PHVExp;
            grdLister.Columns.Add(colDate);

            colDate = new GridViewDateTimeColumn();
            colDate.IsVisible = true;
            colDate.ReadOnly = false;
            colDate.HeaderText = "MOT";
            colDate.Width = 110;
            colDate.Name = COLS.MOT;
            grdLister.Columns.Add(colDate);

            colDate = new GridViewDateTimeColumn();
            colDate.IsVisible = true;
            colDate.ReadOnly = false;
            colDate.HeaderText = "Insurance";
            colDate.Width = 110;
            colDate.Name = COLS.Insurance;
            grdLister.Columns.Add(colDate);

            colDate = new GridViewDateTimeColumn();
            colDate.IsVisible = true;
            colDate.ReadOnly = false;
            colDate.HeaderText = "Road Tax";
            colDate.Width = 110;
            colDate.Name = COLS.RoadTax;
            grdLister.Columns.Add(colDate);


            (grdLister.Columns["PHVExp"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            (grdLister.Columns["PHVExp"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

            (grdLister.Columns["RoadTax"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            (grdLister.Columns["RoadTax"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

            (grdLister.Columns["Insurance"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            (grdLister.Columns["Insurance"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

            (grdLister.Columns["MOT"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            (grdLister.Columns["MOT"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";



            grdLister.EnableFiltering = true;
            grdLister.ShowFilteringRow = true;
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

                if (e.Column.Name != "PHVExp" && e.Column.Name != "MOT" && e.Column.Name != "Insurance" && e.Column.Name != "RoadTax" 
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

                if (e.Column.Name != "PHVExp" && e.Column.Name != "MOT" && e.Column.Name != "Insurance" && e.Column.Name != "RoadTax" 
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


                rptfrmVehicleListReportViewer frm = new rptfrmVehicleListReportViewer();

                //frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                frm.DataSource = GetDataSource().ToList();
              
                frm.GenerateReport();

                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverListReport");

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
            
        }


        private List<stp_GetVehicleListReportResult> GetDataSource()
        {

            if (rdAllVehicle.IsChecked == true)
            {

                List<stp_GetVehicleListReportResult> objDriverList = new List<stp_GetVehicleListReportResult>();

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    objDriverList = db.stp_GetVehicleListReport(1).ToList();               
                }

                return objDriverList; 
            }
            else 
            {
                List<stp_GetVehicleListReportResult> objDriverList = new List<stp_GetVehicleListReportResult>();
                   

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    objDriverList = db.stp_GetVehicleListReport(2).ToList();              
                }
                return objDriverList;

            }         

        }
       
        private void ViewReport()
        {
            //List<Gen_Syspolicy_DriverDocumentList> listofDocs = new List<Gen_Syspolicy_DriverDocumentList>();

            listofDocs = General.GetQueryable<Gen_Syspolicy_DriverDocumentList>(c => c.SysPolicyId == 1).ToList();

            var list = GetDataSource().ToList();

            lblTotalCustomer.Text = "Total Vehicle : " + list.Count.ToStr();
       
            grdLister.RowCount = list.Count;

            GridViewRowInfo row = null;

            for (int i = 0; i < list.Count; i++)
            {

                row = grdLister.Rows[i];

                row.Cells[COLS.Id].Value = list[i].Id;
                row.Cells[COLS.Make].Value = list[i].VehicleMake;
                row.Cells[COLS.Model].Value = list[i].VehicleModel;
                row.Cells[COLS.Owner].Value = list[i].VehicleOwner;
                row.Cells[COLS.Colour].Value = list[i].VehicleColor;
                row.Cells[COLS.Address].Value = list[i].Address;
                row.Cells[COLS.Reg].Value = list[i].VehicleNo;
                row.Cells[COLS.MOT].Value = list[i].MOTExpiryDate.ToDateorNull();
                row.Cells[COLS.Insurance].Value = list[i].InsuranceExpiryDate.ToDateorNull();
                row.Cells[COLS.PHVBadge].Value = list[i].PHVBadge;
                row.Cells[COLS.PHVExp].Value = list[i].PHVExpiry.ToDateorNull();
                row.Cells[COLS.RoadTax].Value = list[i].RoadTaxiExpiryDate.ToDateorNull();

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

                rptfrmVehicleListReportViewer frm = new rptfrmVehicleListReportViewer();

               

              //  frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);
                frm.DataSource = GetDataSource().ToList();

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
                if (rdAllVehicle.IsChecked == true)
                {
                     ShowCompanyVehicleForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                }
                else
                {
                    ShowDriverForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                }
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }

        private void ShowDriverForm(int id)
        {

            frmDriver frm = new frmDriver();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();

        }

        private void ShowCompanyVehicleForm(int id)
        {

            frmCompanyVehcile frm = new frmCompanyVehcile();
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

                rptfrmVehicleListReportViewer frm = new rptfrmVehicleListReportViewer();
             

                frm.DataSource = GetDataSource().ToList();

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


                rptfrmVehicleListReportViewer frm = new rptfrmVehicleListReportViewer();
                frm.DataSource = GetDataSource().ToList();
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

                if ((AppVars.frmMDI.ActiveControl != null && AppVars.frmMDI.ActiveControl.Name.Equals("rptfrmVehicleList1") == true))
                {
                   
                    foreach (var item in grdLister.Rows)
                    {

                        if (item.Cells["PHVExp"].Value.ToDate() < dtNow)
                        {
                            item.Cells["PHVExp"].Style.BackColor = Color.Pink;
                            item.Cells["PHVExp"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["PHVExp"].Value.ToDate() < dtNow.AddDays(PHCVehicleDays))
                        {

                            if (item.Cells["PHVExp"].Style.BackColor == Color.White)
                            {

                                item.Cells["PHVExp"].Style.BackColor = Color.Orange;
                            }
                            else
                            {

                                item.Cells["PHVExp"].Style.BackColor = Color.White;
                            }

                            item.Cells["PHVExp"].Style.CustomizeFill = true;

                        }



                        if (item.Cells["MOT"].Value.ToDate() < dtNow)
                        {
                            item.Cells["MOT"].Style.BackColor = Color.Pink;
                            item.Cells["MOT"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["MOT"].Value.ToDate() < dtNow.AddDays(MOTDays))
                        {

                            if (item.Cells["MOT"].Style.BackColor == Color.White)
                            {

                                item.Cells["MOT"].Style.BackColor = Color.Yellow;
                            }
                            else
                            {

                                item.Cells["MOT"].Style.BackColor = Color.White;
                            }

                            item.Cells["MOT"].Style.CustomizeFill = true;

                        }



                        if (item.Cells["Insurance"].Value.ToDate() < dtNow)
                        {
                            item.Cells["Insurance"].Style.BackColor = Color.Pink;
                            item.Cells["Insurance"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["Insurance"].Value.ToDate() < dtNow.AddDays(InsuranceDays))
                        {

                            if (item.Cells["Insurance"].Style.BackColor == Color.White)
                            {

                                item.Cells["Insurance"].Style.BackColor = Color.LightBlue;
                            }
                            else
                            {

                                item.Cells["Insurance"].Style.BackColor = Color.White;
                            }

                            item.Cells["Insurance"].Style.CustomizeFill = true;

                        }


                        if (item.Cells["RoadTax"].Value.ToDate() < dtNow)
                        {
                            item.Cells["RoadTax"].Style.BackColor = Color.Pink;
                            item.Cells["RoadTax"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["RoadTax"].Value.ToDate() < dtNow.AddDays(RoadTaxDays))
                        {

                            if (item.Cells["RoadTax"].Style.BackColor == Color.White)
                            {

                                item.Cells["RoadTax"].Style.BackColor = Color.Lavender;
                            }
                            else
                            {

                                item.Cells["RoadTax"].Style.BackColor = Color.White;
                            }

                            item.Cells["RoadTax"].Style.CustomizeFill = true;

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
