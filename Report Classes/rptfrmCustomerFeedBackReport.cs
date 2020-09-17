using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Utils;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using System.Collections;
using System.Web.UI.WebControls;

namespace Taxi_AppMain
{
    public partial class rptfrmCustomerFeedBackReport : UI.SetupBase
    {
        bool IsReportLoaded = false;
        public rptfrmCustomerFeedBackReport()
        {
            InitializeComponent();
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.chkRating.ToggleStateChanged += new StateChangedEventHandler(chkRating_ToggleStateChanged);

            for (int i = 1; i <= 5; i++) {

                ddlRating.Items.Add(i.ToStr());
            
            }


            ddlSubCompany.Enter += DdlSubCompany_Enter;
        }

        private void DdlSubCompany_Enter(object sender, EventArgs e)
        {
            if(ddlSubCompany.DataSource==null)
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

        }

        void chkRating_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkRating.Checked == true)
            {
                ddlRating.Text = "";
                ddlRating.Enabled = false;
            }
            else
            {
                ddlRating.Enabled = true;
            }

        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            try
            {
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate.Value.ToDateorNull();


                if (dtFrom != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    dtFrom = (dtFrom.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
               
                int driverId = ddlAllDriver.SelectedValue.ToInt();
                int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();
                int Rating ;
                if (ddlRating.Text == "")
                {
                    Rating = 0;
                }
                else
                {
                    Rating = ddlRating.Text.ToInt();
                }
                                

                if (dtTill != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    dtTill = (dtTill.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();
                string Error = string.Empty;
                if (dtFrom == null)
                {
                    Error = "Required: From Date";
                }
                if (dtTill == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required: To Date";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required: To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                this.reportViewer1.LocalReport.EnableExternalImages = true;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                   var list2 = db.stp_CustomerFeedback(dtFrom,dtTill,SubCompanyId,driverId,Rating) .ToList(); ;

                    this.stp_CustomerFeedbackResultBindingSource.DataSource = list2;
                }
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                string heading = string.Empty;
                heading = "Date Range: " + string.Format("{0:yyyy-MM-dd HH:mm}", dtFrom) + " to " + string.Format("{0:yyyy-MM-dd HH:mm}", dtTill);
                //heading = "From: "+string.Format("{0:dd/MM/yyyy}", dtFrom) + " To: " + string.Format("{0:dd/MM/yyyy}", dtTill);
                // string EndDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                //string To = string.Format("{0:dd/MM/yyyy}", dtTill);
              //  string Earning = "Driver Earnings - "+list.Count().ToStr()+" found";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeading", AppVars.objSubCompany.CompanyName);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", AppVars.objSubCompany.Address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterPeriod", heading);
                
                //List<ClsLogo> objLogo = new List<ClsLogo>();
                //objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
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

            saveFileDlg.FileName = "Customer Feedback Report";

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
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Earning Report");
        }
        public void ExportReportToExcel(string exportTo)
        {



            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //if (exportTo.ToLower() == "pdf")
            //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            //else
            saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Customer Feedback Report";


            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {


                try
                {
                    FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);

                    Microsoft.Reporting.WinForms.Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytes = reportViewer1.LocalReport.Render(
                     exportTo.ToLower(), null, out mimeType, out encoding,
                      out extension,
                     out streamids, out warnings);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

      

    


     

       

       

       

        private void rptfrmJobsList_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;

            TimeSpan tillTime = TimeSpan.Zero;

            TimeSpan.TryParse("23:59:59", out tillTime);

            dtpToDate.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue()).Date);

            dtptilltime.Value = dtpToDate.Value.Value.Date + tillTime;

            ddlAllDriver.Enter += DdlAllDriver_Enter;
        }

        private void DdlAllDriver_Enter(object sender, EventArgs e)
        {
            if (ddlAllDriver.DataSource == null)
                FillCombo();
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {


            try
            {

                //rptfrmJobListViewer frm = new rptfrmJobListViewer();

                //DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                //DateTime? toDate = dtpToDate.Value.ToDateorNull();



                //frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                if (IsReportLoaded)
                {
                    ExportReport();
                }
                else
                {
                    LoadReport();
                    ExportReport();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }



        private void FillCombo()
        {
            ComboFunctions.FillDriverNoComboSorted(ddlAllDriver);

        }
        
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsReportLoaded)
                {
                    ExportReportToExcel("Excel");
                }
                else
                {
                    LoadReport();
                    ExportReportToExcel("Excel");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void chkAllDriver_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlAllDriver.SelectedValue = null;
                ddlAllDriver.Enabled = false;
            }
            else
            {
                ddlAllDriver.Enabled = true;
            }
        }

      

       





    }
}
