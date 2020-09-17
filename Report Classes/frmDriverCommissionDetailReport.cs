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
    public partial class frmDriverCommissionDetailReport : UI.SetupBase
    {
        RadDropDownMenu menu_Job = null;
        bool IsLoaded;

        private bool NoACCommission;

        //   private bool _ShowCharges = false;

        bool prevValue = false;
        bool newValue = false;

  
        public frmDriverCommissionDetailReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDriverReport_Load);

            this.NoACCommission = AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool();
           
            
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

      


        private string _TemplatePath;

        public string TemplatePath
        {
            get { return _TemplatePath; }
            set { _TemplatePath = value; }
        }


        void frmDriverReport_Load(object sender, EventArgs e)
        {

           
            ComboFunctions.FillCompanyCombo(ddlCompany);

            ComboFunctions.FillDriverNoComboSorted(ddlDriver);
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());

            IsLoaded = true;        

        }


        private void ViewReport()
        {
            try
            {

                int companyId = ddlCompany.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;
                if (companyId == 0)
                {
                    error += "Required : Agent";
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


       
        public override void Print()
        {

        
           
            
            //if (chkAllBookedBy.Checked == false)
            //{
            //    BookedBy = ddlBookedBy.Text.ToStr().Trim();
            //}

         


            //this.DataSource = GetDataSource();
            GenerateReport();
        }

        List<stp_DriverCommisionDetailReportResult> _DataSource;

        public List<stp_DriverCommisionDetailReportResult> DataSource
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

            reportDataSource3.Name = "Taxi_Model_Vu_BookingBase";
            reportDataSource3.Value = this.Vu_BookingBaseBindingSource;
            reportDataSource4.Name = "Taxi_AppMain_Classes_ClsLogo";
            reportDataSource4.Value = this.ClsLogoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.Template2_rptAgentCommissionStatement.rdlc";
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

            int DriverId = ddlDriver.SelectedValue.ToInt();

            int companyId = ddlCompany.SelectedValue.ToInt();
            
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

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

            //this.Period = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            int paymenttype = 0;
            if (opBoth.IsChecked)
            {
                paymenttype = 0;
            }
            if (opCash.IsChecked)
            {
                paymenttype = Enums.PAYMENT_TYPES.CASH;
            }
            

       //     ReInitializeReportViewer();

              string reportPath = "Taxi_AppMain.ReportDesigns.";

            if (objSubCompany == null)
                objSubCompany = AppVars.objSubCompany;
        

            reportViewer1.LocalReport.EnableExternalImages = true;

            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

            string address = objSubCompany.Address;
            string telNo = "Tel No. " + objSubCompany.TelephoneNo;

            UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);
                          
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            FromDate = string.Format("{0:MMMM dd,yyyy}", dtpFromDate.Value);
            ToDate = string.Format("{0:MMMM dd,yyyy}", dtpTillDate.Value);


            //List<ClsLogo> objLogo = new List<ClsLogo>();
            //objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });

            //ClsLogoBindingSource.DataSource = objLogo;
           
            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", objSubCompany.CompanyName.ToStr().Trim());
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_FromDate", FromDate);
            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_ToDate", ToDate);
            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_ReportDate", DateTime.Now.ToString() );

            reportViewer1.LocalReport.SetParameters(param);
           
            this.reportViewer1.LocalReport.ReportEmbeddedResource = reportPath + "rptDriverCommissionDetailReport.rdlc";

            using (TaxiDataContext db = new TaxiDataContext())
            {
                this.stp_DriverCommisionDetailReportResultBindingSource.DataSource = db.stp_DriverCommisionDetailReport(fromDate, tillDate, DriverId, paymenttype, companyId).ToList();
            }

            this.reportViewer1.ZoomPercent = 100;
           this.reportViewer1.SetDisplayMode(DisplayMode.Normal);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
           
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.Refresh();
        }


        private List<Vu_BookingBase> listofTempDataSource = new List<Vu_BookingBase>();


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
            saveFileDlg.FileName = "Driver Commission Detail Report";

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
            
            int DriverId = ddlDriver.SelectedValue.ToInt();
            int companyId = ddlCompany.SelectedValue.ToInt();
            int departmentId = ddlDriver.SelectedValue.ToInt();

            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

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


            this.Period = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            string BookedBy = string.Empty;
            //if (chkAllBookedBy.Checked == false)
            //{
            //    BookedBy = ddlBookedBy.Text.ToStr().Trim();
            //}

            int paymenttype = 0;
            if (opBoth.IsChecked)
            {
                paymenttype = 0;
            }
            if (opCash.IsChecked)
            {
                paymenttype = Enums.PAYMENT_TYPES.CASH;
            }
           

           // this.DataSource = GetDataSource(companyId, departmentId, fromDate, tillDate, BookedBy, GroupId,paymenttype);
            GenerateReport();
            ExportReport();
            //frm.ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            
            int DriverId = ddlDriver.SelectedValue.ToInt();
            int companyId = ddlCompany.SelectedValue.ToInt();
            int departmentId = ddlDriver.SelectedValue.ToInt();

            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

            string error = string.Empty;

            if (companyId == 0 && DriverId == 0)
            {
                error += "Required : Agent";
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
            //if (chkAllBookedBy.Checked == false)
            //{
            //    BookedBy = ddlBookedBy.Text.ToStr().Trim();
            //}

            int paymenttype = 0;
            if (opBoth.IsChecked)
            {
                paymenttype = 0;
            }
            if (opCash.IsChecked)
            {
                paymenttype = Enums.PAYMENT_TYPES.CASH;
            }
            

        //    this.DataSource = GetDataSource(companyId, departmentId, fromDate, tillDate, BookedBy, GroupId, paymenttype);
            GenerateReport();
            SendEmail();

        }

        public void SendEmail()
        {

            General.ShowEmailForm(reportViewer1, "Driver Commission Detail Report");

        }

        private void frmDriverCommissionReport_Load(object sender, EventArgs e)
        {

        }
 
        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
                ddlCompany.SelectedValue = null;
        }
   

        private void chkShowCharges_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            prevValue = newValue;
            newValue = args.ToggleState == ToggleState.On ? true : false;
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }


        private void chkAllAgents_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlCompany.SelectedValue = null;
                ddlCompany.Enabled = false;
            }
            else
            {
                ddlCompany.Enabled = true; 

            }
        }

        private void chkAllDriver_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlDriver.SelectedValue = null;
                ddlDriver.Enabled = false;
            }
            else
            {
                ddlDriver.Enabled = true;

            }
        }
    }
}
