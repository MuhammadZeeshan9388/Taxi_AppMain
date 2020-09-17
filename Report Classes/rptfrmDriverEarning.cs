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

namespace Taxi_AppMain
{
    public partial class rptfrmDriverEarning : UI.SetupBase
    {
        bool IsReportLoaded = false;
        public rptfrmDriverEarning()
        {
            InitializeComponent();
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        string templateName = null;
        public void LoadReport()
        {
            try
            {
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate.Value.ToDateorNull();


                if (dtFrom != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    dtFrom = (dtFrom.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



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







                if (templateName == null)
                    templateName = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true).DefaultIfEmpty().TemplateName.ToStr();


                int cnt = 0;


                if (templateName.ToStr().ToLower().Trim() == "template2")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.template2_rptDriverEarning.rdlc";

                    var list = (from a in new Taxi_Model.TaxiDataContext().stp_GetDriverEarning(dtFrom, dtTill, ddlAllDriver.SelectedValue.ToInt(), Enums.BOOKINGSTATUS.DISPATCHED, 0)
                                select new
                                {
                            
                                    DriverId = a.DriverId,
                                    DriverNo = a.DriverNo,
                                    Name = a.Name,
                                    LoginHour = a.LoginHour,
                                    TotalDays = a.TotalDays,
                                    TotalHrs = a.TotalHrs,
                                    BreakTime = a.Break,
                                    Total = a.Total,
                                    Noshow = a.Noshow,
                                    JobsDone = a.JobsDone,

                                    Decline = a.Decline,
                                    //Earning = (a.TotalDays==2?(a.Total-a.Commission):a.Total).ToDecimal(),
                                    Earning =  (a.Account - a.Commission).ToDecimal(),
                                    Account = a.Account,
                                    Cash = a.Cash,
                                    Commission = a.Commission,
                                    AvgJob = a.PDARent,
                                    Avghour = a.Parking+ a.ExtraDropCharges,
                                    Avgday = ((a.Account - a.Commission).ToDecimal() + (a.Parking + a.ExtraDropCharges))- a.PDARent,
                                    LoginDateTime = a.LoginDateTime,
                                    a.PDARent,
                                    a.Parking,
                                    a.Waiting,
                                    a.ExtraDropCharges,
                                    a.DriverCommissionPerBooking

                                }).ToList();



                    var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                    this.stp_GetDriverCommissionEarningResultBindingSource.DataSource = list2;
                    cnt = list.Count;
                }

                else
                {









                    var list = (from a in new Taxi_Model.TaxiDataContext().stp_GetDriverEarning(dtFrom, dtTill, ddlAllDriver.SelectedValue.ToInt(), Enums.BOOKINGSTATUS.DISPATCHED, 0)
                                select new
                                {
                                    //DriverId = a.DriverId,
                                    //DriverNo = a.DriverNo,
                                    //Name = a.Name,
                                    //LoginHour = a.LoginHour,
                                    //TotalDays = a.TotalDays,
                                    //TotalHrs = a.TotalHrs,
                                    //Total = a.Total,
                                    //Noshow = a.Noshow,
                                    //JobsDone = a.JobsDone,
                                    //Decline = a.Decline,
                                    //Earning =  (a.Total-a.Commission),
                                    //Account = a.Account,
                                    //Cash = a.Cash,
                                    //Commission = a.Commission,
                                    //AvgJob = AvgJob(a.Total.ToDecimal(),a.Commission.ToDecimal(),a.JobsDone.ToInt()),//(((a.Total - a.Commission) / (a.JobsDone))).ToDecimal(),
                                    //Avghour = ((a.Total/(a.LoginHour.ToInt()==0?1:a.LoginHour))).ToDecimal(),
                                    //Avgday = ((a.Total /( a.LoginDateTime.ToInt()==0?1:a.LoginDateTime))).ToDecimal(),
                                    //LoginDateTime = a.LoginDateTime,
                                    DriverId = a.DriverId,
                                    DriverNo = a.DriverNo,
                                    Name = a.Name,
                                    LoginHour = a.LoginHour,
                                    TotalDays = a.TotalDays,
                                    TotalHrs = a.TotalHrs,
                                    BreakTime = a.Break,
                                    Total = a.Total,
                                    Noshow = a.Noshow,
                                    JobsDone = a.JobsDone,

                                    Decline = a.Decline,
                                    //Earning = (a.TotalDays==2?(a.Total-a.Commission):a.Total).ToDecimal(),
                                    Earning = (a.TotalDays == 2 ? (a.Total - a.Commission) : a.Total).ToDecimal(),
                                    Account = a.Account,
                                    Cash = a.Cash,
                                    Commission = a.Commission,
                                    AvgJob = AvgJob(a.Total.ToDecimal(), a.TotalDays == 2 ? a.Commission.ToDecimal() : 0, a.JobsDone.ToInt()),//(((a.Total - a.Commission) / (a.JobsDone))).ToDecimal(),
                                    Avghour = ((a.Total / (a.LoginHour.ToInt() == 0 ? 1 : a.LoginHour))).ToDecimal(),
                                    Avgday = ((a.Total / (a.LoginDateTime.ToInt() == 0 ? 1 : a.LoginDateTime))).ToDecimal(),
                                    LoginDateTime = a.LoginDateTime,
                                    a.PDARent,
                                    a.Parking,
                                    a.Waiting,
                                    a.ExtraDropCharges,
                                    a.DriverCommissionPerBooking

                                }).ToList();



                    var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                    this.stp_GetDriverCommissionEarningResultBindingSource.DataSource = list2;



                    cnt = list.Count;


                }







                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];
                string heading = string.Empty;
                heading = "Date Range: " + string.Format("{0:yyyy-MM-dd HH:mm}", dtFrom) + " to " + string.Format("{0:yyyy-MM-dd HH:mm}", dtTill);
              

                string Earning = "Driver Earnings - "+ cnt.ToStr()+" found";

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterPeriod", heading);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterTotalDrivers", Earning);
                
              









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
        private decimal AvgJob(decimal Total, decimal Commission, int JobsDone)
        { 
            decimal Avg=0.00m;

            if (JobsDone == 0)
                JobsDone = 1;


          
                Avg = ((Total - Commission) / JobsDone);
            
            return Avg;
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

            saveFileDlg.FileName = "Driver Earning Report";

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
            saveFileDlg.FileName = "Driver Earning Report";


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
