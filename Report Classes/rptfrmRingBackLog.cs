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
    public partial class rptfrmRingBackLog : UI.SetupBase
    {
        public rptfrmRingBackLog()
        {
            InitializeComponent();
            SetDate();
        }
        void SetDate()
        {
            dtpTillDate.Value = DateTime.Now.ToDate();
            dtpFromDate.Value = DateTime.Now.ToDate().AddMonths(-1);
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }


        private void GenerateReport()
        {
            try
            {

                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();

                string phone = txtPhone.Text.Trim();
                string name = txtName.Text.Trim().ToLower();
                bool? isArrived = chkArrivalRing.Checked;
                string Stn = txtStn.Text.ToStr();



                if (chkAll.Checked)
                    isArrived = null;

                var list = (new TaxiDataContext()).stp_GetRingBackLog(fromDate, tillDate, name, Stn, phone, isArrived).ToList();               
                


                this.stp_GetRingBackLogResultBindingSource.DataSource = list;



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

                this.reportViewer1.ZoomPercent = 150;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
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

        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                chkArrivalRing.Checked = false;
                chkArrivalRing.Enabled = false;

            }
            else
            {

                chkArrivalRing.Enabled = true;
            }
        }

        private void reportViewer1_ZoomChange(object sender, Microsoft.Reporting.WinForms.ZoomChangeEventArgs e)
        {
           
             
        }

        private void reportViewer1_ReportRefresh(object sender, CancelEventArgs e)
        {
            if (this.reportViewer1.ZoomPercent < 150)
            {
                this.reportViewer1.ZoomPercent = 150;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();

            }
        }

        private void reportViewer1_ViewButtonClick(object sender, CancelEventArgs e)
        {

        }

        private void reportViewer1_RenderingBegin(object sender, CancelEventArgs e)
        {

            if (this.reportViewer1.ZoomPercent < 150 && this.reportViewer1.ZoomMode== Microsoft.Reporting.WinForms.ZoomMode.Percent)
            {
                this.reportViewer1.ZoomPercent = 150;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();

            }
           
        }

        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {

        }

    }
}
