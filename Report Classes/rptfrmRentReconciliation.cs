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
using Taxi_AppMain.Classes;


namespace Taxi_AppMain
{
    public partial class rptfrmRentReconciliation : UI.SetupBase
    {
        bool IsReportLoaded = false;
        bool IsLoaded = false;
        public rptfrmRentReconciliation()
        {
            InitializeComponent();
            this.chkAllDriver.CheckedChanged += new EventHandler(chkAllDriver_CheckedChanged);
            this.chkAllSubCompany.CheckedChanged += new EventHandler(chkAllSubCompany_CheckedChanged);
            this.ddlSubCompany.SelectedValueChanged += new EventHandler(ddlSubCompany_SelectedValueChanged);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ddlSubCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (IsLoaded)
            {
                int Id = ddlSubCompany.SelectedValue.ToInt();
                if (Id > 0)
                {
                    var list = (from a in General.GetQueryable<Fleet_Driver>(c => (c.SubcompanyId == Id) && (c.DriverTypeId == Enums.DRIVERTYPES.RENT) && (c.IsActive == true))
                                orderby a.DriverNo
                                select new
                                {
                                    Id = a.Id,
                                    DriverName = a.DriverNo + " - " + a.DriverName

                                }).ToList();
                    ComboFunctions.FillCombo(list, ddl_Driver, "DriverName", "Id");
                }

            }
        }

        void chkAllSubCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSubCompany.Checked)
            {
                ddlSubCompany.SelectedValue = null;
                ddlSubCompany.Enabled = false;
            }
            else
            {
                ddlSubCompany.Enabled = true;
            }
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

                int driverid = ddl_Driver.SelectedValue.ToInt();

                int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();

                string Error = string.Empty;

                if (SubCompanyId == 0 && !chkAllSubCompany.Checked)
                {
                    Error = "Required : SubCompany";

                }
                if (driverid == 0 && !chkAllDriver.Checked)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : Driver";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : Driver";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                using (TaxiDataContext db = new TaxiDataContext())
                {


                    if (optCommission.Checked)
                        SubCompanyId= -1;


                    var list = db.stp_GetDriverRentReconciliation(SubCompanyId, driverid).ToList();
                    list = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                    this.stp_GetDriverRentReconciliationResultBindingSource.DataSource = list;
                }

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());

                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                //Report_Parameter_AverageJobsPerDay
                //string heading = string.Empty;
                //if (fromDate != null && tillDate != null)
                //{
                //    heading =  string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                //}
                //param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", heading);
                //param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AverageJobsPerDay", s.ToStr());
                //param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AverageWorkingPerDay", sWorking);


                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                reportViewer1.LocalReport.SetParameters(param);



                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                IsReportLoaded = true;
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

                // ComboFunctions.FillDriverNoCombo(ddl_Driver, c => c.DriverTypeId == Enums.DRIVERTYPES.RENT);
                ComboFunctions.FillSubCompanyCombo(ddlSubCompany);
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded)
            {
                ExportReport();
            }
            else
            {
                GenerateReport();
                ExportReport();
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
            saveFileDlg.FileName = "Rent Reconciliation Report";

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
