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
using Newtonsoft.Json;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverLoginHistory :UI.SetupBase
    {
        public rptfrmDriverLoginHistory()
        {
            InitializeComponent();
        
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // GenerateReport();
            send(0);
        
        }

        private void GenerateReport()
        {
            try
            {

                int? driverid=ddl_Driver.SelectedValue.ToIntorNull();


                if (driverid == null)
                {
                    ENUtils.ShowMessage("Required :  Driver");
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

              vu_DriverLoginHistoryBindingSource.DataSource=General.GetQueryable<vu_DriverLoginHistory>(c=>c.driverid==driverid
                                                                        && (
                                                                           (fromDate==null || c.logindatetime>=fromDate) &&
                                                                           (tillDate==null || c.logindatetime<=tillDate)
                                                                           ))
                                                                         .OrderByDescending(c=>c.logindatetime).ToList();



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




            }
        }
        public void send(int a)
        {

        




            string connString = Application.StartupPath + @"\Reports\Report.exe";
            




            System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcesses().FirstOrDefault(c => c.ProcessName.Contains("Report"));

            //if (proc != null)
            //{
            //    proc.Kill();
            //    proc.CloseMainWindow();
            //    proc.Close();
            //}
            int? driverid = ddl_Driver.SelectedValue.ToIntorNull();


            if (driverid == null)
            {
                ENUtils.ShowMessage("Required :  Driver");
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
            string conn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr();

            string address = AppVars.objSubCompany.Address;
            string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
            string heading = string.Empty;
            if (fromDate != null && tillDate != null)
            {
                heading = string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            }
            Classes.JArg_DriverLoginHistory j = new Classes.JArg_DriverLoginHistory()
            {
                ConnectionString = conn,

                fromDate = fromDate,

                toDate = tillDate,
                DriverId = driverid,
                rptheading= AppVars.objSubCompany.CompanyName.ToStr(),
                rptaddress = address,
                rptTel = telNo,
                rptCriteria = heading,
                reportname=this.Name

            };

            // Convert BlogSites object to JOSN string format  
            string jsonData = JsonConvert.SerializeObject(j);
            jsonData = Cryptography.Encrypt(jsonData, "report", true);

            System.Diagnostics.Process.Start(connString, jsonData);
        }
        private void rptfrmDriverLoginHistory_Load(object sender, EventArgs e)
        {
            try
            {

                ComboFunctions.FillDriverNoCombo(ddl_Driver);

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
            saveFileDlg.FileName = "DriverLoginHistory";

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

    }
}
