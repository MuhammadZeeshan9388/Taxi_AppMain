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


namespace Taxi_AppMain
{
    public partial class rptfrmCallHistoryBooking :UI.SetupBase
    {
        public rptfrmCallHistoryBooking()
        {
            InitializeComponent();
        
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GenerateReport();
        
        }

        private void GenerateReport()
        {
            try
            {

                int SubCompanyId = ddSubCompany.SelectedValue.ToInt();


                if (chkAllSubCompany.Checked == false && SubCompanyId == 0)
                {
                    ENUtils.ShowMessage("Required : Sub Company");
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

                bool ShowDetail = chkDetailReport.Checked.ToBool();
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list = db.stp_GetBookingByCallHistory(fromDate, tillDate, SubCompanyId, ShowDetail).ToList(); ;
                   
                    this.stp_GetBookingByCallHistoryResultBindingSource.DataSource = list;
                }


                if (ShowDetail)
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptCallHistoryBookingDetail.rdlc";
                }
                else
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptCallHistoryBookingCount.rdlc";
                }

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);
                ////Report_Parameter_AverageJobsPerDay
                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                    heading = string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
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

        private void rptfrmDriverLoginHistory_Load(object sender, EventArgs e)
        {
            try
            {
                ComboFunctions.FillSubCompanyCombo(ddSubCompany);
                if (ddSubCompany.Items.Count == 1)
                {
                    ddSubCompany.SelectedIndex = 0;
                }
                //ComboFunctions.Fill(ddSubCompany);

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
            saveFileDlg.FileName = "Call History Booking Report";

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

        private void chkAllSubCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSubCompany.Checked)
            {
                ddSubCompany.SelectedValue = null;
                ddSubCompany.Enabled = false;
            }
            else
            {
                ddSubCompany.Enabled = true;
            }
        }

    }
}
