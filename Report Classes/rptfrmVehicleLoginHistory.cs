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
    public partial class rptfrmVehicleLoginHistory :UI.SetupBase
    {
        public rptfrmVehicleLoginHistory()
        {
            InitializeComponent();
            this.chkAllVehicle.CheckedChanged += new EventHandler(chkAllVehicle_CheckedChanged);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void chkAllVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllVehicle.Checked)
            {
                ddlCompanyVehicle.SelectedValue = null;
                ddlCompanyVehicle.Enabled = false;
            }
            else
            {
                ddlCompanyVehicle.Enabled = true;
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

                int VehicleId = ddlCompanyVehicle.SelectedValue.ToInt();




                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();
                
                string Error=string.Empty;
                if (fromDate.Value == null)
                {
                    Error = "Required : From Date";
                }
                if (tillDate.Value == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : To Date";
                    }
                    else
                    {
                        Error +=Environment.NewLine+ "Required : To Date";
                    }
                }

                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                if (tillDate != null)
                    tillDate = tillDate + TimeSpan.Parse("23:59:59");



                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_GetVehicleLoginHistory(fromDate.Value, tillDate.Value, VehicleId).ToList();


                    if(ddlDriver.SelectedValue.ToInt()!=0)
                    {

                        list = list.Where(c => c.DriverId == ddlDriver.SelectedValue.ToInt()).ToList();

                    }

                    var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                    this.stp_GetVehicleLoginHistoryResultBindingSource.DataSource = list2;
                }
               

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Heading", AppVars.objSubCompany.CompanyName.ToStr());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);
                //Report_Parameter_AverageJobsPerDay
                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                    heading = "Period: "+ string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                }
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", heading);
                //param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AverageJobsPerDay", s.ToStr());
                //param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AverageWorkingPerDay", sWorking);
              

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

        void DdlDriver_Enter(object sender, EventArgs e)
        {
          
            if (ddlDriver.DataSource == null)
            {
                ComboFunctions.FillDriverNoCombo(ddlDriver);


            }
        }

        private void rptfrmDriverLoginHistory_Load(object sender, EventArgs e)
        {
            try
            {
                ComboFunctions.FillVehicleCombo(ddlCompanyVehicle);

                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
                ddlDriver.Enter += DdlDriver_Enter;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
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
