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
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.Enumerations;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;
using UI;

namespace Taxi_AppMain
{
    public partial class frmViewDriverRent : UI.SetupBase
    {

        private Fleet_Driver _ObjDriver;

        public Fleet_Driver ObjDriver
        {
            get { return _ObjDriver; }
            set { _ObjDriver = value; }
        }


        private string _DatePeriod;

        public string DatePeriod
        {
            get { return _DatePeriod; }
            set { _DatePeriod = value; }
        }



        private List<Vu_BookingBase> _DataSource;

        public List<Vu_BookingBase> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }


        private List<stp_DriverCommisionResult> _DataSource2;

        public List<stp_DriverCommisionResult> DataSource2
        {
            get { return _DataSource2; }
            set { _DataSource2 = value; }
        }



        private string _ReportHeading;

        public string ReportHeading
        {
            get { return _ReportHeading; }
            set { _ReportHeading = value; }
        }


        private int _StatementType;

        public int StatementType
        {
            get { return _StatementType; }
            set { _StatementType = value; }
        }

        private int _Rent;

        public int Rent
        {
            get { return _Rent; }
            set { _Rent = value; }
        }
        public frmViewDriverRent()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmViewDriverRent_Load);
            //grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            this.Shown += new EventHandler(frmViewDriverRent_Shown);
            this.FormClosed += new FormClosedEventHandler(frmViewDriverRent_FormClosed);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmViewDriverRent_KeyDown);
        }

        void frmViewDriverRent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        void frmViewDriverRent_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
            GC.Collect();
        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewRowInfo)
            {
                ViewDetailForm();
            }
        }

        void frmViewDriverRent_Shown(object sender, EventArgs e)
        {
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;



            this.InitializeForm("frmInvoice");
            LoadRentList(0);


          //  grdLister.AddEditColumn();

            //if (this.CanDelete)
            //{
            //   grdLister.AddDeleteColumn();
            //    grdLister.Columns["btnDelete"].Width = 70;
            //}



            grdLister.Columns["Id"].IsVisible = false;
            grdLister.Columns["DriverID"].IsVisible = false;


            grdLister.Columns["TransNo"].HeaderText = "Transaction No";
            grdLister.Columns["TransNo"].Width = 80;

            grdLister.Columns["TransDate"].HeaderText = "Transaction Date";
            grdLister.Columns["TransDate"].Width = 100;

            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";




            grdLister.Columns["Rent"].Width = 60;
            grdLister.Columns["Rent"].HeaderText = "Driver rent";

            grdLister.Columns["TransactionFor"].Width = 80;
            grdLister.Columns["TransactionFor"].HeaderText = "Transaction For";


            grdLister.Columns["Driver"].Width = 120;
            grdLister.Columns["DriverNo"].Width = 80;

            grdLister.Columns["Balance"].Width = 80;

            //grdLister.Columns["btnEdit"].Width = 70;


            UI.GridFunctions.SetFilter(grdLister);
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
        

        //void grdLister_CommandCellClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridCommandCellElement gridCell = (GridCommandCellElement)sender;
        //        if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
        //        {



        //            if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
        //            {
        //                string Transaction = grdLister.CurrentRow.Cells["TransNo"].Value.ToStr();
        //                int DriverId = grdLister.CurrentRow.Cells["DriverID"].Value.ToInt();

        //                var query = General.GetQueryable<DriverRent>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

        //                if (query != null)
        //                {
        //                    string Transno = query.TransNo.ToStr();

        //                    if (Transno == Transaction)
        //                    {
        //                        RadGridView grid = gridCell.GridControl;
        //                        grid.CurrentRow.Delete();
        //                    }
        //                    else
        //                    {
        //                        ENUtils.ShowMessage("You Can not delete a record..");
        //                    }
        //                }
                        
        //            }
        //        }
        //        else if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
        //        {
        //            ViewDetailForm();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowMessage(ex.Message);
        //    }
        //}
        private void ViewDetailForm()
        {
            try
            {


                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    long id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();

                    ShowForm(id);
                }
                else
                {
                    ENUtils.ShowMessage("Please select a record");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        void ShowForm(long id)
        {


            frmDriverRent frm = new frmDriverRent(id,true);
            //frm.OnDisplayRecord(id);
            frm.ShowDialog();
            //frm.Dispose();

            //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRent1");

            //if (doc != null)
            //{
            //    doc.Close();
            //}

            //MainMenuForm.MainMenuFrm.ShowForm(frm);

        }
        void frmViewDriverRent_Load(object sender, EventArgs e)
        {
            FillCombo();   
        }
        private void FillCombo() 
        {
            ComboFunctions.FillDriverNoCombo(ddl_Driver, c => c.DriverTypeId == 1 && (c.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0));
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
            dtpTillDate.Value = dtpTillDate.Value.Value.AddHours(23).AddMinutes(59);


        }

        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 50;

            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Update";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

        private List<Vu_BookingBase> GetDataSource(int? driverId, int statementType, DateTime? fromDate, DateTime? tillDate, int? companyId)
        {

            int creditCardAccountId = 0;

            if (chkExcludeCC.Checked)
            {
                creditCardAccountId = General.GetObject<Gen_Company>(c => c.CompanyName.ToUpper().Replace(" ", "").Trim() == "CREDITCARD").DefaultIfEmpty().Id;
            }

            return General.GetQueryable<Vu_BookingBase>(c =>

                   c.DriverId == driverId && ((statementType == eStatementType.AccountStatement && c.CompanyId != null && (creditCardAccountId == 0 || (c.CompanyId != creditCardAccountId)))
                                             || (statementType == eStatementType.CashStatement && c.CompanyId == null)
                                                     || (statementType == eStatementType.Both && (creditCardAccountId == 0 || (c.CompanyId == null || c.CompanyId != creditCardAccountId))))
                                                                                   && (companyId == null || c.CompanyId == companyId))
                         .Where(b => (b.PickupDateTime.Value >= fromDate && b.PickupDateTime.Value <= tillDate))
                             .OrderByDescending(c => c.PickupDateTime).ToList();
        }

        private List<stp_DriverCommisionResult> GetDataSource2(DateTime? fromDate, DateTime? tillDate, int? driverId, int? companyId, int? statementType)
        {
            int creditCardAccountId = 0;

            if (chkExcludeCC.Checked)
            {
                creditCardAccountId = General.GetObject<Gen_Company>(c => c.CompanyName.ToUpper().Replace(" ", "").Trim() == "CREDITCARD").DefaultIfEmpty().Id;
            }

            return new TaxiDataContext().stp_DriverCommision(dtpFromDate.Value, dtpTillDate.Value, driverId, companyId, statementType, creditCardAccountId).ToList();
        }

        public struct eStatementType
        {
            public static int AccountStatement = 1;
            public static int CashStatement = 2;
            public static int Both = 3;
        }
        public DateTime? fromDate;
        public DateTime? tillDate;
        public int? driverId;
        public int? companyId;
        public int statementType;


        ReportDataSource imageDataSource = null;
        public void GenerateReport3()
        {
            try
            {

                int? companyId = null;
                int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
                DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

                string error = string.Empty;

                if (driverId == null)
                {

                    error += "Required : Driver";
                }

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


                if (optCreditCard.ToggleState == ToggleState.On)
                {
                    companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == "credit card" || c.CompanyName.ToLower() == "creditcard").DefaultIfEmpty().Id;


                }



                int statementType = 0;
                if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On || optCreditCard.ToggleState == ToggleState.On)
                {
                    statementType = eStatementType.AccountStatement;


                }
                else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.CashStatement;


                }
                else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.Both;
                }
                StatementType=statementType;
                
                
                //var list = General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId).AsEnumerable()
                //                .Where(b => (b.PickupDateTime.ToDate() >= fromDate && b.PickupDateTime.ToDate() <= tillDate))
                //                    .OrderByDescending(c => c.PickupDateTime).ToList();

                // frm.ReportHeading = "Driver Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == driverId).DefaultIfEmpty();
                if (ChkRent.Checked == true)
                {
                    Rent = txtRent.Value.ToInt();
                }
                else
                {
                    Rent = 0;
                }
               // this.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);
                this.DataSource2 = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);
                Microsoft.Reporting.WinForms.ReportParameter[] param = null;
               // StatementType=
                // UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);


                string className = "Taxi_AppMain.ReportDesigns." + "Template3" + "_";

                //if (objTemplate.TemplateName.ToStr() == "Template1")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                //    param = new Microsoft.Reporting.WinForms.ReportParameter[19];
                //}
                //else if (objTemplate.TemplateName.ToStr() == "Template2")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                //    param = new Microsoft.Reporting.WinForms.ReportParameter[19];
                //}
                //else if (objTemplate.TemplateName.ToStr() == "Template3")
                //{

                this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);      
                    
                    //+= new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                param = new Microsoft.Reporting.WinForms.ReportParameter[22];
                //  }

                reportViewer1.LocalReport.EnableExternalImages = true;


                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                //param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);


                if (imageDataSource == null)
                {

                    List<ClsLogo> objLogo = new List<ClsLogo>();
                    objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                    imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                    this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                }



                string path = @"File:";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                //  param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


                // Summary Calculations
                decimal JobsSum = this.DataSource2.Sum(c => c.FareRate.ToDecimal() + c.WaitingCharges.ToDecimal());
                int jobsCnt = this.DataSource2.Count;
                string jobsTotal = string.Format("{0:c}", JobsSum);
                jobsTotal = jobsTotal.Substring(1);
                jobsTotal = jobsTotal.Insert(0, "£ ");
                decimal zeroValue = 0.00m;
                string zeroStr = string.Format("{0:c}", zeroValue);
                zeroStr = zeroStr.Substring(1);
                zeroStr = zeroStr.Insert(0, "£ ");




                decimal totalCashAccountAmount = this.DataSource2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                                         && c.DriverCommissionType.ToStr() == "Amount").Sum(c => c.DriverCommission.ToDecimal());

                string totalCashAccountAmountStr = string.Format("{0:c}", totalCashAccountAmount);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Substring(1);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Insert(0, "£ ");

                string totalAccountBookings = this.DataSource2.Count(c => c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT).ToStr();
                decimal totalAccountCharges = this.DataSource2.Where(c => c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT).Sum(c => c.FareRate.ToDecimal() + c.WaitingCharges.ToDecimal());
                string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
                totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

                string totalCashBooking = this.DataSource2.Count(c => c.CompanyId == null || (c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)).ToStr();
                string totalCashBookingCharges = string.Format("{0:c}", this.DataSource2.Where(c => c.CompanyId == null || (c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH))
                                                                                            .Sum(c => c.FareRate.ToDecimal() + c.WaitingCharges.ToDecimal()));
                totalCashBookingCharges = totalCashBookingCharges.Substring(1);
                totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");

                decimal driverMonthlyRent = 0;

                if (Rent > 0)
                {
                    driverMonthlyRent = (Rent).ToDecimal() + totalCashAccountAmount;
                }
                else
                {
                    driverMonthlyRent = ObjDriver.DriverMonthlyRent.ToDecimal() + totalCashAccountAmount;
                }


                string driverMonthlyRentRate = string.Format("{0:c}", driverMonthlyRent);
                driverMonthlyRentRate = driverMonthlyRentRate.Substring(1);
                driverMonthlyRentRate = driverMonthlyRentRate.Insert(0, "£ ");


                decimal driverOwed = totalAccountCharges - driverMonthlyRent;
                string driverOwedRate = "£ " + string.Format("{0:#.00}", driverOwed);


                if (StatementType == 2)
                {
                    driverMonthlyRentRate = "£ 0.00";

                    driverOwedRate = jobsTotal;
                }

               



                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);



                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", totalAccountBookingsCharges);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions", driverMonthlyRentRate);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", jobsTotal);
                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);



                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate.ToStr());


                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());

                //  param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalCommissionAmount", totalCashAccountAmountStr.ToStr());

                //param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_RentWise", (Rent).ToStr());
                //


                // var query =ObjDriver General.GetObject<Fleet_Driver>(c => c.Id == driverid);
                string PCODRiverExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.PCODriverExpiryDate.ToDateorNull());
                string PCOVehicleExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.PCOVehicleExpiryDate.ToDateorNull());
                string MOT2ExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.MOT2ExpiryDate.ToDateorNull());
                string MOTExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.MOTExpiryDate.ToDateorNull());
                string InsuranceExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.InsuranceExpiryDate.ToDateorNull());
                string LicenseExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.DrivingLicenseExpiryDate.ToDateorNull());
                string RoadTaxiExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate.ToDateorNull());

                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCODRiverExpiryDate", PCODRiverExpiryDate);
                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCOVehicleExpiryDate", PCOVehicleExpiryDate);
                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOT2ExpiryDate", MOT2ExpiryDate);
                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOTExpiryDate", MOTExpiryDate);
                param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InsuranceExpiryDate", InsuranceExpiryDate);
                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_LicenseExpiryDate", LicenseExpiryDate);
                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RoadTaxExpiryDate", RoadTaxiExpiryDate);





                var list2 = (from a in new Taxi_Model.TaxiDataContext().stp_VehicleUsage(fromDate, tillDate, driverId, companyId, statementType)
                             select new stp_VehicleUsageResult
                             {
                                 VehicleType = a.VehicleType,
                                 VehicleNo = a.VehicleNo,
                                 Accbkgs = a.Accbkgs,
                                 AccTotat = a.AccTotat,
                                 CashBkgs = a.CashBkgs,
                                 CashTotal = a.CashTotal,
                                 Booking = a.Booking,
                                 Total = a.Total
                             }).ToList();





                if (courierDataSource == null)
                {

                    courierDataSource = new ReportDataSource("Taxi_Model_stp_VehicleUsageResult", list2);
                    this.reportViewer1.LocalReport.DataSources.Add(courierDataSource);
                }
                else
                {
                    this.reportViewer1.LocalReport.DataSources[3].Value = list2;

                }


                reportViewer1.LocalReport.SetParameters(param);
                //this.Vu_BookingBaseBindingSource.DataSource = this.DataSource;
                this.stp_DriverCommisionResultBindingSource.DataSource = this.DataSource2;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        ReportDataSource courierDataSource = null;

        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(this.reportViewer1.LocalReport.DataSources[3]);
        }
        public void GenerateReport()
        {
            try
            {
                int? companyId = null;
                int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
                DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

                string error = string.Empty;

                if (driverId == null)
                {

                    error += "Required : Driver";
                }

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


                if (optCreditCard.ToggleState == ToggleState.On)
                {
                    companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == "credit card" || c.CompanyName.ToLower() == "creditcard").DefaultIfEmpty().Id;


                }


                
                int statementType = 0;
                if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On || optCreditCard.ToggleState == ToggleState.On)
                {
                    statementType = eStatementType.AccountStatement;


                }
                else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.CashStatement;


                }
                else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.Both;
                }
                StatementType = statementType;
                //var list = General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId).AsEnumerable()
                //                .Where(b => (b.PickupDateTime.ToDate() >= fromDate && b.PickupDateTime.ToDate() <= tillDate))
                //                    .OrderByDescending(c => c.PickupDateTime).ToList();

                // frm.ReportHeading = "Driver Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                if (ChkRent.Checked == true)
                {
                    Rent = txtRent.Value.ToInt();
                }
                else
                {
                    Rent = 0;
                }
                DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == driverId).DefaultIfEmpty();

                this.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);
                Microsoft.Reporting.WinForms.ReportParameter[] param = null;

                UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmDriverStatement" && c.IsDefault == true);


                string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";

                if (objTemplate.TemplateName.ToStr() == "Template1")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                    param = new Microsoft.Reporting.WinForms.ReportParameter[19];
                }
                else if (objTemplate.TemplateName.ToStr() == "Template2")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                    param = new Microsoft.Reporting.WinForms.ReportParameter[19];
                }
                else if (objTemplate.TemplateName.ToStr() == "Template3")
                {
                    GenerateReport3();
                    return;
                    //  this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                    //  param = new Microsoft.Reporting.WinForms.ReportParameter[26];
                }

                reportViewer1.LocalReport.EnableExternalImages = true;




                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


                // Summary Calculations
                decimal JobsSum = this.DataSource.Sum(c => c.TotalCharges.ToDecimal());
                int jobsCnt = this.DataSource.Count;
                string jobsTotal = string.Format("{0:c}", JobsSum);
                jobsTotal = jobsTotal.Substring(1);
                jobsTotal = jobsTotal.Insert(0, "£ ");
                decimal zeroValue = 0.00m;
                string zeroStr = string.Format("{0:c}", zeroValue);
                zeroStr = zeroStr.Substring(1);
                zeroStr = zeroStr.Insert(0, "£ ");




                decimal totalCashAccountAmount = this.DataSource.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                                         && c.DriverCommissionType.ToStr() == "Amount").Sum(c => c.DriverCommission.ToDecimal());

                string totalCashAccountAmountStr = string.Format("{0:c}", totalCashAccountAmount);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Substring(1);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Insert(0, "£ ");

                string totalAccountBookings = this.DataSource.Count(c => c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT).ToStr();
                decimal totalAccountCharges = this.DataSource.Where(c => c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT).Sum(c => c.TotalCharges.ToDecimal());
                string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
                totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

                string totalCashBooking = this.DataSource.Count(c => c.CompanyId == null || (c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)).ToStr();
                string totalCashBookingCharges = string.Format("{0:c}", this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH))
                                                                                            .Sum(c => c.TotalCharges.ToDecimal()));
                totalCashBookingCharges = totalCashBookingCharges.Substring(1);
                totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");

                decimal driverMonthlyRent = 0;

                if (Rent > 0)
                {
                    driverMonthlyRent = (Rent).ToDecimal() + totalCashAccountAmount;
                }
                else
                {
                    driverMonthlyRent = ObjDriver.DriverMonthlyRent.ToDecimal() + totalCashAccountAmount;
                }


                string driverMonthlyRentRate = string.Format("{0:c}", driverMonthlyRent);
                driverMonthlyRentRate = driverMonthlyRentRate.Substring(1);
                driverMonthlyRentRate = driverMonthlyRentRate.Insert(0, "£ ");


                decimal driverOwed = totalAccountCharges - driverMonthlyRent;
                string driverOwedRate = "£ " + string.Format("{0:#.00}", driverOwed);


                if (StatementType == 2)
                {
                    driverMonthlyRentRate = "£ 0.00";

                    driverOwedRate = jobsTotal;
                }

                //UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);


                //string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";

                //if (objTemplate.TemplateName.ToStr() == "Template1")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";

                //}
                //else if (objTemplate.TemplateName.ToStr() == "Template2")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                //}
                //else if (objTemplate.TemplateName.ToStr() == "Template3")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                //}



                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);



                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", totalAccountBookingsCharges);
                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions", driverMonthlyRentRate);
                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", jobsTotal);
                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);



                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate.ToStr());


                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());

                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalCommissionAmount", totalCashAccountAmountStr.ToStr());

                //param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_RentWise", (Rent).ToStr());
                //


                reportViewer1.LocalReport.SetParameters(param);
               
               // this.vuBookingBaseBindingSource.DataSource =DataSource;
              //  this.vuBookingBaseBindingSource.DataSource = this.DataSource;
               // this.vuBookingBaseBindingSource.DataSource = this.DataSource;

                this.Vu_BookingBaseBindingSource.DataSource =this.DataSource;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            int DriverId = 0;
            DriverId = ddl_Driver.SelectedValue.ToInt();
            if (DriverId == 0)
            {
                ENUtils.ShowMessage("Required : Driver");
                return;
            }

            LoadRentList(DriverId);
            GenerateReport();
           // GenerateReport();
        }
        private void btnAddNewRent_Click(object sender, EventArgs e)
        {
            frmDriverRent frm = new frmDriverRent();
            frm.ShowDialog();
            frm.Dispose();
        }


        private void LoadRentList(int DriverId)
        {
            try
            {
                var query = from a in General.GetQueryable<DriverRent>(c => c.DriverId == DriverId)
                            select new
                            {
                                Id = a.Id,
                                TransNo = a.TransNo,
                                TransDate = a.TransDate,
                                DriverNo = a.Fleet_Driver.DriverNo,
                                Driver = a.Fleet_Driver.DriverName,
                                DriverID = a.DriverId,
                                Rent = a.DriverRent1,
                                Balance = a.Balance,
                                //JobsTotal = a.JobsTotal
                                TransactionFor = a.TransFor
                            };


                grdLister.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void ChkRent_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (ChkRent.Checked == true)
            {
                txtRent.Enabled = true;
            }
            else
            {
                txtRent.Enabled = false;
            }
        }
    }
}
