using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taxi_AppMain
{
    public partial class rptfrmJobNo : Form
    {
        string BookingNo = string.Empty;
        public rptfrmJobNo(string JobNo)
        {
            InitializeComponent();
            BookingNo = JobNo;
        }

        private void rptfrmJobNo_Load(object sender, EventArgs e)
        {
            LaodReport();
        }
        public void LaodReport()
        {
            try
            {
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobNo", BookingNo);

                //List<ClsLogo> objLogo = new List<ClsLogo>();
                //objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //this.AdminActivity.LocalReport.DataSources.Add(imageDataSource);
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
    }
}
