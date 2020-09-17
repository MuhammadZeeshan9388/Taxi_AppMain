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
using System.IO;
using Microsoft.Reporting.WinForms;


namespace Taxi_AppMain
{
    public partial class rptfrmJobHistoryReport : UI.SetupBase
    {
        public rptfrmJobHistoryReport()
        {
            InitializeComponent();

            this.chkAllDriver.CheckedChanged += new EventHandler(chkAllDriver_CheckedChanged);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void chkAllDriver_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDriver.Checked)
            {
                ddl_Driver.SelectedValue = null;
                ddl_Driver.Enabled = false;
            }
            else
            {
                ddl_Driver.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GenerateReport();
        
        }

        private void GenerateReport()
        {
            try
            {

                int driverid=ddl_Driver.SelectedValue.ToInt();
                int vehicleId =   ddlVehicle.SelectedValue.ToInt();

                string Error = "";
                if (driverid == 0 && !chkAllDriver.Checked)
                {
                    Error = "Required :  Driver";
                   
                }
                if (vehicleId == 0)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required :  Vehicle";
                    }
                    else
                    {
                        Error +=Environment.NewLine+ "Required :  Vehicle";
                    }

                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();


                if (tillDate != null)
                    tillDate = tillDate + TimeSpan.Parse("23:59:59");

                
                using (TaxiDataContext db = new TaxiDataContext())
                {
                  this.stp_JobHIstoryResultBindingSource.DataSource  = db.stp_JobHIstory(fromDate, tillDate, driverid, vehicleId).ToList().OrderByDescending(c=>c.BookingDate);
                }
                

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);

                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                    heading =  string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                }
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", heading);
              
                reportViewer1.LocalReport.SetParameters(param);

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;                               
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);


            }
        }

        private void rptfrmJobHistoryReport_Load(object sender, EventArgs e)
        {
            try
            {

                ComboFunctions.FillDriverNoCombo(ddl_Driver);
                ComboFunctions.FillVehicleTypeCombo(ddlVehicle);

                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
            }
            catch (Exception ex)
            {


            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            GenerateReport();
            ExportReport();
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
            saveFileDlg.FileName = "DriverJobHistory";

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

        private void rptfrmJobHistoryReport_Load_1(object sender, EventArgs e)
        {
            try
            {

                ComboFunctions.FillDriverNoCombo(ddl_Driver);
                ComboFunctions.FillVehicleTypeCombo(ddlVehicle);

                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
            }
            catch (Exception ex)
            {


            }
        }

    }
}
