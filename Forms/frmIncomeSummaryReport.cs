using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.Enumerations;


namespace Taxi_AppMain
{
    public partial class frmIncomeSummaryReport : UI.SetupBase
    {
        public frmIncomeSummaryReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmIncomeReport_Load);

            grdLister.EnableHotTracking = false;
            //grdLister.AutoCellFormatting = true;
            grdLister.AllowAddNewRow = false;
         //   grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;


            grdLister.ShowGroupPanel = false;

            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);


            grdLister.EnableAlternatingRowColor = true;
            grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;
            this.chkAllVehicle.ToggleStateChanged += new StateChangedEventHandler(chkAllVehicle_ToggleStateChanged);
            this.chkAllCompany.ToggleStateChanged += new StateChangedEventHandler(chkAllCompany_ToggleStateChanged);
            this.chkAllDriver.ToggleStateChanged += new StateChangedEventHandler(chkAllDriver_ToggleStateChanged);
        }

        void chkAllDriver_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlDriver.SelectedValue = null;
                ddlDriver.Enabled = false;
                //ddlAllDriver.SelectedValue = 0;
            }
            else
            {
                if (ddlDriver.DataSource == null)
                {
                    ComboFunctions.FillDriverNoCombo(ddlDriver);
                }
                ddlDriver.Enabled = true;
            }
        }

        void chkAllCompany_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlCompany.SelectedValue = null;
                ddlCompany.Enabled = false;
            }
            else
            {
                if (ddlCompany.DataSource == null)
                {
                    ComboFunctions.FillCompanyCombo(ddlCompany);
                }

                ddlCompany.Enabled = true;

            }
        }

        void chkAllVehicle_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlCompanyVehicle.SelectedValue = null;
                ddlCompanyVehicle.Enabled = false;
                //ddlAllDriver.SelectedValue = 0;
            }
            else
            {
                if (ddlCompanyVehicle.DataSource == null)
                {
                    ComboFunctions.FillVehicleCombo(ddlCompanyVehicle);
                }
                ddlCompanyVehicle.Enabled = true;
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

        string cellValue = string.Empty;
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


        void frmIncomeReport_Load(object sender, EventArgs e)
        {

            TimeSpan tillTime = TimeSpan.Zero;

            TimeSpan.TryParse("23:59:59", out tillTime);

            dtpTillDate.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue()).Date);

            dtptilltime.Value = dtpTillDate.Value.Value.Date + tillTime;



            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());



            //(grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            //(grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            //grdLister.Columns["PickUpDate"].Width = 90;
            //grdLister.Columns["RefNumber"].Width = 40;
            //grdLister.Columns["Vehicle"].Width = 50;
            //grdLister.Columns["Account"].Width = 60;
            //grdLister.Columns["PickUpPoint"].Width = 110;
            //grdLister.Columns["Destination"].Width = 110;


            //grdLister.Columns["Charges"].Width = 45;
            //grdLister.Columns["Parking"].Width = 45;
            //grdLister.Columns["Waiting"].Width = 50;
            //grdLister.Columns["ExtraDrop"].Width = 60;
            ////grdLister.Columns["MeetAndGreet"].Width = 60;
            ////grdLister.Columns["CongtionCharge"].Width = 70;
            //grdLister.Columns["Total"].Width = 40;
            //grdLister.Columns["Via"].Width = 80;
            //grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            //grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            //grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
            //grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";


        }



        private void ViewReport()
        {
            try
            {


                DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
                DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

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


                int companyId = ddlCompany.SelectedValue.ToInt();
                int driverId = ddlDriver.SelectedValue.ToInt();
                int FleetMasterId = ddlCompanyVehicle.SelectedValue.ToInt();


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    grdLister.DataSource = db.stp_GetIncomeSummary(fromDate.Value, tillDate.Value, companyId, driverId, FleetMasterId).ToList(); ;
                }


                (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
                (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

                grdLister.Font = new Font("Tahoma", 9, FontStyle.Bold);
                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["InvId"].IsVisible = false;
                grdLister.Columns["InvoiceDate"].HeaderText = "Date";
                grdLister.Columns["InvoiceDate"].Width = 180;
                grdLister.Columns["Description"].Width = 220;
                grdLister.Columns["Income"].Width = 120;
                grdLister.Columns["Expense"].Width = 120;


                grdLister.AllowEditRow = false;

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {

            ViewReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {
            DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
            DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

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

            int companyId = ddlCompany.SelectedValue.ToInt();
            int driverId = ddlDriver.SelectedValue.ToInt();

            int FleetMasterId = ddlCompanyVehicle.SelectedValue.ToInt();
            rptfrmIncomeSummaryReport frm = new rptfrmIncomeSummaryReport();
            frm.Period = "Income Summary Report for Date Range" + Environment.NewLine + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            frm.DataSource = GetDataSource(fromDate, tillDate, companyId, driverId, FleetMasterId);

            frm.LoadReport();

            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmIncomeSummaryReport1");

            if (doc != null)
            {
                doc.Close();
            }
            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);

        }


        private List<stp_GetIncomeSummaryResult> GetDataSource(DateTime? fromDate, DateTime? tillDate, int companyId, int driverId, int FleetMasterId)
        {
            using (TaxiDataContext db = new TaxiDataContext())
            {
                return db.stp_GetIncomeSummary(fromDate, tillDate, companyId, driverId, FleetMasterId).ToList();
            }

        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
            DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

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


            int companyId = ddlCompany.SelectedValue.ToInt();
            int driverId = ddlDriver.SelectedValue.ToInt();

            int FleetMasterId = ddlCompanyVehicle.SelectedValue.ToInt();
            rptfrmIncomeSummaryReport frm = new rptfrmIncomeSummaryReport();
            frm.Period = "Income Summary Report for Date Range" + Environment.NewLine + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            frm.DataSource = GetDataSource(fromDate, tillDate, companyId, driverId, FleetMasterId);


            frm.LoadReport();

            frm.ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
            DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

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

            int companyId = ddlCompany.SelectedValue.ToInt();
            int driverId = ddlDriver.SelectedValue.ToInt();

            int FleetMasterId = ddlCompanyVehicle.SelectedValue.ToInt();
            rptfrmIncomeSummaryReport frm = new rptfrmIncomeSummaryReport();
            frm.Period = "Income Summary Report for Date Range" + Environment.NewLine + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            frm.DataSource = GetDataSource(fromDate, tillDate, companyId, driverId, FleetMasterId);


            frm.LoadReport();

            frm.SendEmail();

        }



    }
}
