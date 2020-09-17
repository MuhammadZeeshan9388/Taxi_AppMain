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
    public partial class rptfrmCallHistory : UI.SetupBase
    {
        public rptfrmCallHistory()
        {
            InitializeComponent();
            SetDate();

           
            optMissedCalls.Visible = true;
        }
        void SetDate()
        {
            dtpTillDate.Value = DateTime.Now.ToDate();
            dtpFromDate.Value = DateTime.Now.ToDate().AddMonths(-1);
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            //GenerateReport();
            send(0);
        }


        private void GenerateReport()
        {
            try
            {

                bool isMissed = optMissedCalls.Checked;


               

                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();

                string phone = txtPhone.Text.Trim();
                string name = txtName.Text.Trim().ToLower();
                string Line = txtLine.Text.ToStr();
                string Stn = txtStn.Text.ToStr();


                var list = (from a in GeneralBLL.GetQueryable<CallHistory>(null)
                            where (fromDate == null || a.CallDateTime.Value.Date >= fromDate)
                             && (tillDate == null || a.CallDateTime.Value.Date <= tillDate)
                             && (name == string.Empty || a.Name.Trim().ToLower().StartsWith(name))
                             && (phone == string.Empty || a.PhoneNumber.Trim() == phone)
                             && (Line == string.Empty || a.Line == Line)
                             && (Stn == string.Empty || a.STN == Stn)
                              && (isMissed == false || (a.IsAccepted!=null && a.IsAccepted == true))
                            orderby a.CallDateTime descending
                            select new
                            {
                                Name = a.Name,
                                PhoneNumber = a.PhoneNumber,
                                CallDateTime = a.CallDateTime,
                                Line = a.Line,
                                STN = a.STN
                            }).ToList();
                


                CallHistoryBindingSource.DataSource = list;



                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[6];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);

                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                   heading=  string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                }
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", heading);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalCalls", list.Count.ToStr());

                
                CallerIdType_Configuration obj = General.GetObject<CallerIdType_Configuration>(c=>c.Id !=null);
                string DigitalCLIType = obj.DigitalCLIType.ToStr();

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DigitalCLIType", DigitalCLIType == "" ? "0" : DigitalCLIType);

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
        public void send(int a)
        {


            bool isMissed = optMissedCalls.Checked;



            string connString = Application.StartupPath + @"\Reports\Report.exe";





            System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcesses().FirstOrDefault(c => c.ProcessName.Contains("Report"));

            //if (proc != null)
            //{
            //    proc.Kill();
            //    proc.CloseMainWindow();
            //    proc.Close();
            //}
           



            if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                dtpFromDate.Value = null;

            if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                dtpTillDate.Value = null;

            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime? tillDate = dtpTillDate.Value.ToDateorNull();

            string phone = txtPhone.Text.Trim();
            string name = txtName.Text.Trim().ToLower();
            string Line = txtLine.Text.ToStr();
            string Stn = txtStn.Text.ToStr();


        
            string conn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr();

            string address = AppVars.objSubCompany.Address;
            string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
            string heading = string.Empty;
            if (fromDate != null && tillDate != null)
            {
                heading = string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            }
            Classes.JArg_CallHistory j = new Classes.JArg_CallHistory()
            {
                ConnectionString = conn,

                fromDate = fromDate,

                toDate = tillDate,
               phone=phone,
               name=name,
               Line=Line,
               Stn=Stn,
                rptheading = AppVars.objSubCompany.CompanyName.ToStr(),
                rptaddress = address,
                rptTel = telNo,
                rptCriteria = heading,
                reportname = this.Name,
                IsMissed=isMissed

            };

            // Convert BlogSites object to JOSN string format  
            string jsonData = JsonConvert.SerializeObject(j);
            jsonData = Cryptography.Encrypt(jsonData, "report", true);

            System.Diagnostics.Process.Start(connString, jsonData);
        }
        private void rptfrmCallHistory_Load(object sender, EventArgs e)
        {

        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {

                GenerateReport();

                ExportReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
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
            saveFileDlg.FileName = "CallHistory";

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

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

    }
}
