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
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Taxi_AppMain
{
    public partial class rptfrmThirdPartyCompanyJobStatementReport : UI.SetupBase
    {
        RadDropDownMenu menu_Job = null;
        bool IsLoaded;

        int criteriaType = 1;
        private bool NoACCommission;

        //   private bool _ShowCharges = false;

        bool prevValue = false;
        bool newValue = false;

        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string RefNumber = "RefNumber";

            public static string Account = "Account";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";
            public static string Commission = "Commission";
            public static string DriverCommission = "DriverCommission";
        }


        public rptfrmThirdPartyCompanyJobStatementReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDriverReport_Load);
            this.NoACCommission = AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool();

            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            //  ddlCompany.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddl_Driver_SelectedIndexChanged);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //void ddl_Driver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (!IsLoaded )
        //            return;

        //        //if (ddl_Driver.SelectedValue != null)
        //        //{

        //        //   var objDrv = General.GetObject<Fleet_Driver>(c=>c.Id== ddl_Driver.SelectedValue.ToInt());

        //        //   if (objDrv != null && objDrv.DriverCommissionPerBooking.ToDecimal()>0)
        //        //   {
        //        //       ChkCommission.Checked = true;
        //        //       txtCommission.Value = objDrv.DriverCommissionPerBooking.ToDecimal();


        //        //   }
        //        //}
        //    }
        //    catch
        //    {


        //    }

        //}


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


        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                    return;

                else if (cell.GridControl.Name == "grdLister")
                {

                    if (menu_Job == null)
                    {
                        menu_Job = new RadDropDownMenu();


                        RadMenuItem viewJobItem1 = new RadMenuItem("View Job");
                        viewJobItem1.ForeColor = Color.DarkBlue;
                        viewJobItem1.BackColor = Color.Orange;
                        viewJobItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        viewJobItem1.Click += new EventHandler(viewJobItem1_Click);
                        menu_Job.Items.Add(viewJobItem1);


                    }

                    e.ContextMenu = menu_Job;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void viewJobItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                //{
                //    General.ShowBookingForm(grdLister.CurrentRow.Cells[COLS.ID].Value.ToInt(), true, "", "", Enums.BOOKING_TYPES.LOCAL);

                //}
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);

            }
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {

            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name == "btnUpdate")
            {

                GridViewRowInfo row = gridCell.RowInfo;

                if (row is GridViewDataRowInfo)
                {
                    long id = row.Cells[COLS.ID].Value.ToLong();
                    decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                    decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                    decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                    decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                    decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                    decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                    decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();

                    BookingBO objMaster = new BookingBO();
                    try
                    {


                        objMaster.GetByPrimaryKey(id);

                        if (objMaster.Current != null)
                        {
                            objMaster.Current.FareRate = fare;
                            objMaster.Current.ParkingCharges = parking;
                            objMaster.Current.WaitingCharges = waiting;
                            objMaster.Current.ExtraDropCharges = extraDrop;
                            objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                            objMaster.Current.CongtionCharges = CongtionCharge;
                            objMaster.Current.TotalCharges = TotalCharges;


                            objMaster.Save();





                            ViewReport();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (objMaster.Errors.Count > 0)
                            ENUtils.ShowMessage(objMaster.ShowErrors());
                        else
                            ENUtils.ShowMessage(ex.Message);

                    }
                }
            }



        }


        private string _TemplatePath;

        public string TemplatePath
        {
            get { return _TemplatePath; }
            set { _TemplatePath = value; }
        }


        void frmDriverReport_Load(object sender, EventArgs e)
        {

            ComboFunctions.FillThirdPartyCompanyCombo(ddlThirdPartyCompany);
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


        private void ViewReport()
        {
            try
            {

                int ThirdPartySubCompanyId = ddlThirdPartyCompany.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();


                string error = string.Empty;
                if (ThirdPartySubCompanyId == 0)
                {
                    error += "Required : Third Party Company";
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


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {

                ViewReport();


            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }


        private string _Period;

        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }


        private bool CheckTemplate()
        {
            if (string.IsNullOrEmpty(this.TemplatePath))
                ENUtils.ShowMessage("Report Template is not defined in User Rights");


            return this.TemplatePath != string.Empty;
        }
        public override void Print()
        {

            //if (CheckTemplate() == false)
            //    return;




            int ThirdPartySubCompanyId = ddlThirdPartyCompany.SelectedValue.ToInt();

            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime? tillDate = dtpTillDate.Value.ToDateorNull();

            string error = string.Empty;

            if (ThirdPartySubCompanyId == 0)
            {
                error += "Required : Third Party Company";
            }

            if (fromDate == null)
            {
                if (!string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : From Date";
            }

            if (tillDate == null)
            {
                if (!string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : To Date";


            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;

            }


            this.Period = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            string BookedBy = string.Empty;
            //if (chkAllBookedBy.Checked == false)
            //{
            //    BookedBy = ddlBookedBy.Text.ToStr().Trim();
            //}
            //this.DataSource = GetDataSource(ThirdPartySubCompanyId, fromDate, tillDate);
            GenerateReport();



        }

        List<stp_GetTransferedJobsListResult> _DataSource;

        public List<stp_GetTransferedJobsListResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }





        private void ReInitializeReportViewer()
        {

            //if (prevValue == newValue)
            //    return;

            reportViewer1.Clear();
            reportViewer1.Dispose();
            this.Controls.Remove(this.reportViewer1);
             

            GC.Collect();

            // 
            // reportViewer1
            // 
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;





            reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();

            reportDataSource3.Name = "Taxi_Model_stp_GetTransferedJobsListResult";
            reportDataSource3.Value = this.stp_TransferedJobsResultBindingSource;
            reportDataSource4.Name = "Taxi_AppMain_Classes_ClsLogo";
            reportDataSource4.Value = this.ClsLogoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            //rptThirdPartyCompanyJobStatement.rdlc
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptThirdPartyCompanyJobStatement.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 137);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1040, 675);
            this.reportViewer1.TabIndex = 116;

            this.Controls.Add(this.reportViewer1);
            reportViewer1.BringToFront();

            prevValue = newValue;

        }
        public void GenerateReport()
        {

            ReInitializeReportViewer();


            if (objSubCompany == null)
                objSubCompany = AppVars.objSubCompany;


            // string departmentName = ddlDepartment.Text.Trim();

            string pickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool() == true ? "1" : "0";
            reportViewer1.LocalReport.EnableExternalImages = true;

            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[9];

            string address = objSubCompany.Address;
            string telNo = "Tel No. " + objSubCompany.TelephoneNo;



            if (opAll.IsChecked)
            {
                criteriaType = 1;
            }
            else if (opTransfer.IsChecked)
            {
                criteriaType = 2;
            }
            else
            {
                criteriaType = 3;
            }

            this.DataSource = (from a in new Taxi_Model.TaxiDataContext().stp_GetTransferedJobsList(dtpFromDate.Value.ToDate(), dtpTillDate.Value.ToDate(), ddlThirdPartyCompany.SelectedValue.ToInt(),criteriaType)
                               select new stp_GetTransferedJobsListResult
                               {
                                   BookingNo = a.BookingNo,
                                   BookingDate = a.BookingDate,
                                   PickupDateTime = a.PickupDateTime,
                                   FromAddress = a.FromAddress,
                                   AgentCommission = a.AgentCommission,
                                   ToAddress = a.ToAddress,
                                   CompanyName = a.CompanyName,
                                   CompanyPrice = a.CompanyPrice,
                                   AcceptedDateTime = a.AcceptedDateTime,
                                   JobTakenByCompany = a.JobTakenByCompany,
                                   AccountTypeId = a.AccountTypeId,
                                   VehicleType = a.VehicleType,
                                   CustomerName = a.CustomerName,
                                   TransferJobCommission = a.TransferJobCommission,
                                   FareRate = a.FareRate,
                                   CompanyId = a.CompanyId,
                                   AgentCommissionPercent = a.AgentCommissionPercent,
                                   ArrivalDateTime = a.ArrivalDateTime,
                                   AuthCode = a.AuthCode,
                                   TotalCharges = a.TotalCharges,
                                   BookedBy = a.BookedBy,
                                   BookingStatusId = a.BookingStatusId,
                                   BookingTypeId = a.BookingTypeId,
                                   ClearedDateTime = a.ClearedDateTime,
                                   CompanyCode = a.CompanyCode,
                                   CongtionCharges = a.CongtionCharges,
                                   CostCenterId = a.CostCenterId = a.CostCenterId,
                                   CostCenterName = a.CostCenterName,
                                   CustomerEmail = a.CustomerEmail,
                                   CustomerMobileNo = a.CustomerMobileNo,
                                   CustomerPhoneNo = a.CustomerPhoneNo,
                                   CustomerPrice = a.CustomerPrice,
                                   DepartmentId = a.DepartmentId,
                                   DepartmentName = a.DepartmentName,
                                   Despatchby = a.Despatchby,
                                   DriverAddress = a.DriverAddress,
                                   DriverCommission = a.DriverCommission,
                                   DriverCommissionType = a.DriverCommissionType,
                                   DriverFullName = a.DriverFullName,
                                   DriverId = a.DriverId,
                                   DriverName = a.DriverName,
                                   DriverNo = a.DriverNo,
                                   ExtraDropCharges = a.ExtraDropCharges,
                                   FromDoorNo = a.FromDoorNo,
                                   SpecialRequirements = a.SpecialRequirements,
                                   FromLocType = a.FromLocType,
                                   FromStreet = a.FromStreet,
                                   Id = a.Id,
                                   InvoiceDate = a.InvoiceDate,
                                   InvoiceNo = a.InvoiceNo,
                                   IsCommissionWise = a.IsCommissionWise,
                                   MasterJobId = a.MasterJobId,
                                   MeetAndGreetCharges = a.MeetAndGreetCharges,
                                   NoofHandLuggages = a.NoofHandLuggages,
                                   NoofLuggages = a.NoofLuggages,
                                   NoofPassengers = a.NoofPassengers = a.NoofPassengers,
                                   OrderNo = a.OrderNo,
                                   ParkingCharges = a.ParkingCharges,
                                   PaymentType = a.PaymentType,
                                   PaymentTypeId = a.PaymentTypeId,
                                   POBDateTime = a.POBDateTime,
                                   PupilNo = a.PupilNo,
                                   ReturnDriverFullName = a.ReturnDriverFullName,
                                   ReturnDriverId = a.ReturnDriverId,
                                   ReturnFareRate = a.ReturnFareRate,
                                   ReturnPickupDateTime = a.ReturnPickupDateTime,
                                   StatusName = a.StatusName,
                                   STCDateTime = a.STCDateTime,
                                   SubCompanyId = a.SubCompanyId,
                                   ToDoorNo = a.ToDoorNo,
                                   ToLocType = a.ToLocType,
                                   ToStreet = a.ToStreet,
                                   TotalTravelledMiles = a.TotalTravelledMiles,
                                   VehicleTypeId = a.VehicleTypeId,
                                   Via1 = a.Via1,
                                   WaitingCharges = a.WaitingCharges,
                                   ActionType=a.ActionType,
                                   Address=a.Address,
                                   CompanyName1=a.CompanyName1,
                                   EmailAddress=a.EmailAddress,
                                   PartyId=a.PartyId,
                                   TelephoneNo=a.TelephoneNo

                               }).ToList();


            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);




            List<ClsLogo> objLogo = new List<ClsLogo>();
            objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });

            ClsLogoBindingSource.DataSource = objLogo;

            //   ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
            //   this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);




            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", objSubCompany.CompanyName.ToStr().Trim());
            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", Period);

            // Summary Calculations


            //this.DataSource=

            decimal OwnedjobsTotal = this.DataSource.Sum(c => c.FareRate.ToDecimal());


            decimal commissionTotal = this.DataSource.Sum(c => c.TransferJobCommission.ToDecimal());


            decimal owed = OwnedjobsTotal - commissionTotal;


            string jobsCnt = this.DataSource.Count.ToStr();





            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", string.Format("£ {0:f2}", OwnedjobsTotal));


            param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", string.Format("£ {0:f2}", owed));

            param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt);
            param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", string.Format("£ {0:f2}", commissionTotal));


            param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeptName", "");


            reportViewer1.LocalReport.SetParameters(param);


            this.stp_TransferedJobsResultBindingSource.DataSource = this.DataSource;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;




            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.Refresh();
        }




        public void ExportReport()
        {

            Microsoft.Reporting.WinForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer1.LocalReport.Render(
             "Pdf", null, out mimeType, out encoding,
              out extension,
             out streamids, out warnings);


            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Transfer Job Statement Report";

            //   saveFileDlg.RestoreDirectory = false;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {


                try
                {
                    FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }




        }



        private List<stp_GetTransferedJobsListResult> GetDataSource(DateTime? fromDate, DateTime? tillDate, int ThirdPartycompanyId, int criteriaType)
        {

            return (from a in new Taxi_Model.TaxiDataContext().stp_GetTransferedJobsList(fromDate, tillDate, ThirdPartycompanyId,criteriaType)
                    select new stp_GetTransferedJobsListResult
                    {
                        BookingNo = a.BookingNo,
                        BookingDate = a.BookingDate,
                        PickupDateTime = a.PickupDateTime,
                        FromAddress = a.FromAddress,
                        AgentCommission = a.AgentCommission,
                        ToAddress = a.ToAddress,
                        CompanyName = a.CompanyName,
                        CompanyPrice = a.CompanyPrice,
                        AcceptedDateTime = a.AcceptedDateTime,
                        JobTakenByCompany = a.JobTakenByCompany,
                        AccountTypeId = a.AccountTypeId,
                        VehicleType = a.VehicleType,
                        CustomerName = a.CustomerName,
                        TransferJobCommission = a.TransferJobCommission,
                        FareRate = a.FareRate,
                        CompanyId = a.CompanyId,
                        AgentCommissionPercent = a.AgentCommissionPercent,
                        ArrivalDateTime = a.ArrivalDateTime,
                        AuthCode = a.AuthCode,
                        TotalCharges = a.TotalCharges,
                        BookedBy = a.BookedBy,
                        BookingStatusId = a.BookingStatusId,
                        BookingTypeId = a.BookingTypeId,
                        ClearedDateTime = a.ClearedDateTime,
                        CompanyCode = a.CompanyCode,
                        CongtionCharges = a.CongtionCharges,
                        CostCenterId = a.CostCenterId = a.CostCenterId,
                        CostCenterName = a.CostCenterName,
                        CustomerEmail = a.CustomerEmail,
                        CustomerMobileNo = a.CustomerMobileNo,
                        CustomerPhoneNo = a.CustomerPhoneNo,
                        CustomerPrice = a.CustomerPrice,
                        DepartmentId = a.DepartmentId,
                        DepartmentName = a.DepartmentName,
                        Despatchby = a.Despatchby,
                        DriverAddress = a.DriverAddress,
                        DriverCommission = a.DriverCommission,
                        DriverCommissionType = a.DriverCommissionType,
                        DriverFullName = a.DriverFullName,
                        DriverId = a.DriverId,
                        DriverName = a.DriverName,
                        DriverNo = a.DriverNo,
                        ExtraDropCharges = a.ExtraDropCharges,
                        FromDoorNo = a.FromDoorNo,
                        SpecialRequirements = a.SpecialRequirements,
                        FromLocType = a.FromLocType,
                        FromStreet = a.FromStreet,
                        Id = a.Id,
                        InvoiceDate = a.InvoiceDate,
                        InvoiceNo = a.InvoiceNo,
                        IsCommissionWise = a.IsCommissionWise,
                        MasterJobId = a.MasterJobId,
                        MeetAndGreetCharges = a.MeetAndGreetCharges,
                        NoofHandLuggages = a.NoofHandLuggages,
                        NoofLuggages = a.NoofLuggages,
                        NoofPassengers = a.NoofPassengers = a.NoofPassengers,
                        OrderNo = a.OrderNo,
                        ParkingCharges = a.ParkingCharges,
                        PaymentType = a.PaymentType,
                        PaymentTypeId = a.PaymentTypeId,
                        POBDateTime = a.POBDateTime,
                        PupilNo = a.PupilNo,
                        ReturnDriverFullName = a.ReturnDriverFullName,
                        ReturnDriverId = a.ReturnDriverId,
                        ReturnFareRate = a.ReturnFareRate,
                        ReturnPickupDateTime = a.ReturnPickupDateTime,
                        StatusName = a.StatusName,
                        STCDateTime = a.STCDateTime,
                        SubCompanyId = a.SubCompanyId,
                        ToDoorNo = a.ToDoorNo,
                        ToLocType = a.ToLocType,
                        ToStreet = a.ToStreet,
                        TotalTravelledMiles = a.TotalTravelledMiles,
                        VehicleTypeId = a.VehicleTypeId,
                        Via1 = a.Via1,
                        WaitingCharges = a.WaitingCharges,
                        Address = a.Address,
                        CompanyName1 = a.CompanyName1,
                        EmailAddress = a.EmailAddress,
                        PartyId = a.PartyId,
                        TelephoneNo = a.TelephoneNo,
                        ActionType=a.ActionType

                    }).ToList();
            //return General.GetQueryable<stp_TransferedJobsResult>(c =>  c.SubCompanyId == ThirdPartycompanyId)

            //        .Where(b => (b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate))
            //            .OrderBy(c => c.PickupDateTime).ToList();

        }

        public struct eStatementType
        {
            public static int AccountStatement = 1;
            public static int CashStatement = 2;
            public static int Both = 3;
            public static int CashAccountStatement = 4;


        } ;


        Gen_SubCompany objSubCompany = null;
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            //if (CheckTemplate() == false)
            //    return;


            int ThirdPartySubCompanyId = ddlThirdPartyCompany.SelectedValue.ToInt();

            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

            string error = string.Empty;

            if (ThirdPartySubCompanyId == 0)
            {
                error += "Required : Third Party Company";
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


            this.Period = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            // string BookedBy = string.Empty;
            if (opAll.IsChecked)
            {
                criteriaType = 1;
            }
            else if (opTransfer.IsChecked)
            {
                criteriaType = 2;
            }
            else
            {
                criteriaType = 3;
            }

            this.DataSource = GetDataSource(fromDate, tillDate, ThirdPartySubCompanyId, criteriaType); ;
            GenerateReport();
            ExportReport();
            //frm.ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            //if (CheckTemplate() == false)
            //    return;


            int ThirdPartySubCompanyId = ddlThirdPartyCompany.SelectedValue.ToInt();

            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

            string error = string.Empty;

            if (ThirdPartySubCompanyId == 0)
            {
                error += "Required : Third Party Company";
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


            this.Period = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            string BookedBy = string.Empty;

            if (opAll.IsChecked)
            {
                criteriaType = 1;
            }
            else if (opTransfer.IsChecked)
            {
                criteriaType = 2;
            }
            else
            {
                criteriaType = 3;
            }

            this.DataSource = GetDataSource(fromDate, tillDate, ThirdPartySubCompanyId, criteriaType); ;
            GenerateReport();
            SendEmail();

        }

        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Transfer Job Statement Report");

        }






        private void ViewReportCommissionWise()
        {
            try
            {
                //int driverId = ddlCompany.SelectedValue.ToInt();
                //int companyId = ddlCompany.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();
                bool PickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool();


                string error = string.Empty;
                //if (driverId == 0)
                //{
                //    error += "Required : Driver";
                //}

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

                //  lblCriteria.Text = "Driver Report Related to '" + ddl_Driver.Text.ToStr()
                //       + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);


            //    int statementType = 0;
                //if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                //{
                //    statementType = eStatementType.AccountStatement;
                //}
                //else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                //{
                //    statementType = eStatementType.CashStatement;
                //}
                //else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                //{
                //    statementType = eStatementType.Both;
                //}
                //else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                //{
                //    statementType = eStatementType.CashAccountStatement;
                //}


                //var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);


                ////      decimal appCommission = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();

                //var list = (from a in query
                //            where a.DriverId == driverId && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                //                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                //                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                //                                                 || (statementType == eStatementType.Both))
                //                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                //            orderby a.PickupDateTime descending
                //            select new
                //            {
                //                Id = a.Id,
                //                PickUpDate = a.PickupDateTime,
                //                RefNumber = a.BookingNo,
                //                Vehicle = a.Fleet_VehicleType.VehicleType,
                //                Account = a.Gen_Company.CompanyName,
                //                PickupPoint = a.FromAddress,
                //                Destination = a.ToAddress,
                //                Charge = a.FareRate,
                //                Parking = a.ParkingCharges,
                //                Waiting = a.WaitingCharges,
                //                ExtraDrop = a.ExtraDropCharges,
                //                MeetAndGreet = a.MeetAndGreetCharges,
                //                CongtionCharge = a.CongtionCharges,
                //                Total = a.TotalCharges,
                //                //   DriverCommission = a.IsCommissionWise == false ? appCommission : a.DriverCommission,
                //                DriverCommission = NoACCommission && a.CompanyId != null ? 0 : ( a.IsCommissionWise == false ? a.Fleet_Driver.DriverCommissionPerBooking : a.DriverCommission),
                //                DriverCommissionType = a.DriverCommissionType
                //            }).ToList();

                // grdLister.DataSource = list;
                //grdLister.RowCount = list.Count;

                //for (int i = 0; i < list.Count; i++)
                //{
                //    grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                //    grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickUpDate;
                //    grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i].RefNumber.ToStr();
                //    grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();
                //    grdLister.Rows[i].Cells[COLS.Account].Value = list[i].Account.ToStr();
                //    grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                //    grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                //    grdLister.Rows[i].Cells[COLS.Charges].Value = list[i].Charge.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.Parking].Value = list[i].Parking.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.Waiting].Value = list[i].Waiting.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();

                //    grdLister.Rows[i].Cells[COLS.DriverCommission].Value = list[i].DriverCommission.ToDecimal();


                //    if (NoACCommission==false || (NoACCommission==true && string.IsNullOrEmpty( grdLister.Rows[i].Cells[COLS.Account].Value.ToStr().Trim())) )
                //    {


                //        int Comm = txtCommission.Value.ToInt();
                //        //  grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Total.ToDecimal() * Comm) / 100 : list[i].DriverCommission.ToDecimal();




                //        if (PickCommissionFromCharges)
                //        {
                //            grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Charge.ToDecimal() * Comm) / 100 : list[i].DriverCommission.ToDecimal();

                //            //   grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Charge.ToDecimal() * list[i].DriverCommission.ToDecimal()) / 100
                //            //                                                    : list[i].DriverCommission.ToDecimal();

                //        }
                //        else
                //        {

                //            grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Total.ToDecimal() * Comm) / 100 : list[i].DriverCommission.ToDecimal();


                //            //grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Total.ToDecimal() * list[i].DriverCommission.ToDecimal()) / 100
                //            //                                                    : list[i].DriverCommission.ToDecimal();
                //        }

                //    }
                //}

                //decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Commission].Value.ToDecimal());
                //string total = totalEarning.ToStr();
                //lblTotalEarning.Text = "Total Earning £ " + total;

                //lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();

                //lblTotalExtra.Text = "Total Extra Drop £ " + grdLister.Rows.Sum(c => c.Cells[COLS.ExtraDrop].Value.ToDecimal());


                //decimal jobsTotal = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
                //lblJobsTotal.Text = "Jobs Total: £" + jobsTotal;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void frmDriverCommissionReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Enter && ddlCompany.SelectedValue.ToInt() != 0)
                //{
                //    //if (ChkCommission.Checked == false)
                //    //{
                //    //    ViewReport();
                //    //}
                //    //else
                //    //{
                //    //    ViewReportCommissionWise();
                //    //}


                //}
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void optAccount_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (optAccount.ToggleState == ToggleState.On || optCashACStatement.ToggleState == ToggleState.On)
            //    SetCustomAccount(ToggleState.On);
            //else
            //    SetCustomAccount(ToggleState.Off);

        }


        private void SetCustomAccount(ToggleState toggle)
        {
            try
            {
                //if (toggle == ToggleState.On)
                //{

                //    ddlCompany.Enabled = true;
                //    chkAll.Visible = true;
                //    if (ddlCompany.DataSource == null)
                //    {
                //        ComboFunctions.FillCompanyCombo(ddlCompany);

                //    }
                //}
                //else
                //{
                //    ddlCompany.Enabled = false;
                //    ddlCompany.SelectedValue = null;
                //    chkAll.Visible = false;
                //}
            }
            catch (Exception ex)
            {
            }
        }



        //private void ddlCompany_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{

        //    if (IsLoaded == false)
        //        return;

        //    int? companyId = ddlCompany.SelectedValue.ToIntorNull();

        //    if (companyId != null)
        //    {

        //        ComboFunctions.FillCompanyDepartmentCombo(ddlDepartment, c => c.CompanyId == ddlCompany.SelectedValue.ToInt());



        //        var list = General.GetQueryable<Booking>(c => c.CompanyId == companyId && (c.BookedBy != null && c.BookedBy != ""))
        //                                                       .Select(args => args.BookedBy).Distinct().ToArray<string>();


        //        ddlBookedBy.Items.Clear();

        //        ddlBookedBy.Items.AddRange(list);

        //         //   ComboFunctions.FillCombo(list, ddlBookedBy, "BookedBy", "Id");

        //            //   ddlBookedBy.Enabled = true;

        //    }
        //}

        private void chkShowCharges_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            prevValue = newValue;
            newValue = args.ToggleState == ToggleState.On ? true : false;
        }




    }
}
