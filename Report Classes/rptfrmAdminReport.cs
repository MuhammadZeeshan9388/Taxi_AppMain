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
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;


namespace Taxi_AppMain
{
    public partial class rptfrmAdminReport : UI.SetupBase
    {
        bool IsReportLoaded = false;
        private List<stp_ControllerReportResult> _DataSource;

        public List<stp_ControllerReportResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }


        private string _ReportHeading;

        public string ReportHeading
        {
            get { return _ReportHeading; }
            set { _ReportHeading = value; }
        }

        public rptfrmAdminReport()
        {
            InitializeComponent();
            this.Load+=new System.EventHandler(rptfrmAdminReport_Load);
        }

        void  rptfrmAdminReport_Load(object sender, System.EventArgs e)
        {
            LoadUsers();
        }
        public void LoadUsers()
        {
            ComboFunctions.FillUsers(ddlUsers);
            dtpFromDate.Value = DateTime.Now.GetStartOfCurrentWeek();
            dtpTillDate.Value = DateTime.Now.ToDate();
        }
        public void LoadReport(string ControllerName,DateTime? dtFrom, DateTime? dtTill,int Id,string Comment)
        {
            try
            {

                //if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                //    dtpFromDate.Value = null;

                //if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                //    dtpTillDate.Value = null;
                //    string Comment = string.Empty;
                //    if (chkAutoLogoutController.Checked == true)
                //    {
                //        Comment = "Auto Logout , No Activity was Performed from Last 10 Mins";
                //    }


                //    DateTime? fromDate = dtpFromDate.Value;
                //    DateTime? tillDate = dtpTillDate.Value;


                //    string name = this.ddlUsers.Text.ToStr();
                //    int id = ddlUsers.SelectedValue.ToInt();

                    //dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    //dtpTillDate.Value = DateTime.Now.ToDate();
                    this.AdminActivity.LocalReport.EnableExternalImages = true;
                    //var list =
                    this.stp_ControllerReportResultBindingSource.DataSource = (from a in new Taxi_Model.TaxiDataContext().stp_ControllerReport(ControllerName, dtFrom, dtTill, Id,Comment)
                                                                               select new stp_ControllerReportResult
                                                                               {
                                                                                  
                                                                                  // AddOn = a.AddOn,
                                                                                   //despatchdatetime = a.despatchdatetime,
                                                                                   LoginDate = a.LoginDate,
                                                                                   LogoutDate = a.LogoutDate,
                                                                                   TotalBooked=a.TotalBooked,
                                                                                   TotalDespatched=a.TotalDespatched,
                                                                                   Comment=a.Comment,
                                                                                   UserName = a.UserName,
                                                                                   AnsweredCalls=a.AnsweredCalls
                                                                                   


                                                                               }).ToList();



                    Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];

                   // string address = AppVars.objSubCompany.Address;
                   // string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                    string heading = string.Empty;
                    //if (dtFrom != null && dtTill != null)
                    //{
                        heading = string.Format("{0:dd/MM/yy}", dtFrom) + " to " + string.Format("{0:dd/MM/yy}", dtTill);
                   // }


                    param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);
                    
                    List<ClsLogo> objLogo = new List<ClsLogo>();
                    objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                    ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                    this.AdminActivity.LocalReport.DataSources.Add(imageDataSource);
                    AdminActivity.LocalReport.SetParameters(param);



                    this.AdminActivity.ZoomPercent = 100;
                    this.AdminActivity.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                    this.AdminActivity.RefreshReport();
                    IsReportLoaded = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
            //var list = new Taxi_Model.TaxiDataContext().stp_ControllerReport("Admin", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), 1);
        }
        public void ExportReport()
        {

            Microsoft.Reporting.WinForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = AdminActivity.LocalReport.Render(
             "Pdf", null, out mimeType, out encoding,
              out extension,
             out streamids, out warnings);


            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            saveFileDlg.Title = "Save File";
            
            saveFileDlg.FileName = "Controller Activity Report";

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


        public string ExportReport2(string Name)
        {
            string Dest = string.Empty;


            Microsoft.Reporting.WinForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = AdminActivity.LocalReport.Render(
             "Pdf", null, out mimeType, out encoding,
              out extension,
             out streamids, out warnings);


            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            saveFileDlg.Title = "Save File";
            if (!string.IsNullOrEmpty(Name))
            {
                saveFileDlg.FileName = Name;
                // FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);
                // //C:\Users\eurofoft\Documents\Tony.pdf
                // fs.Write(bytes, 0, bytes.Length);
                //// fs.Close();

            }
            else
            {
                saveFileDlg.FileName = "Controller Activity Report";
            }

            //   saveFileDlg.RestoreDirectory = false;
           

                try
                {
                    string Name2 = saveFileDlg.FileName;
                    Dest = Application.StartupPath + "\\WeeklyReport\\" + Name+".pdf";
                    FileStream fs = new FileStream(Dest, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
           
            return Dest;

        }
       
       


        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;
                string name = this.ddlUsers.Text.ToStr();
                int id = ddlUsers.SelectedValue.ToInt();
                string Comment = string.Empty;
                    //DateTime? fromDate = dtpFromDate.Value;
                    //DateTime? tillDate = dtpTillDate.Value;

                if (chkAutoLogoutController.Checked == true)
                {
                    Comment = "Auto Logout";// , No Activity was Performed from Last 10 Mins";
                }
                LoadReport(name,dtpFromDate.Value.ToDate(),dtpTillDate.Value.ToDate(),id,Comment);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
            
        }

       
      
        private List<stp_ControllerReportResult> GetData(string name, DateTime? fromDate, DateTime? tillDate, int id,string Comment)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_ControllerReport(name, fromDate, tillDate, id,Comment)
                    select new stp_ControllerReportResult
                    {
                        LoginDate= a.LoginDate,
                        LogoutDate=a.LogoutDate,
                        TotalBooked=a.TotalBooked,
                        TotalDespatched=a.TotalDespatched,
                        Comment = a.Comment,
                        UserName = a.UserName,
                        AnsweredCalls=a.AnsweredCalls

                    }).ToList();
        }
      

        public void SendEmail()
        {
            General.ShowEmailForm(AdminActivity, "Controller Activity Report");
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string name = this.ddlUsers.Text.ToStr();
                int id = 0;
                id = ddlUsers.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;


                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";
                }

                ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                string Comment = string.Empty;
                if (chkAutoLogoutController.Checked == true)
                {
                    Comment = "Auto Logout , No Activity was Performed from Last 10 Mins";
                }

                DataSource = GetData(name, fromDate, tillDate, id,Comment);
                LoadReport(name,fromDate,tillDate,id,Comment);

                SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded == true)
            {
                ExportReport();
            }
            else
            {
                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;
                string name = this.ddlUsers.Text.ToStr();
                int id = ddlUsers.SelectedValue.ToInt();
                string Comment = string.Empty;
                //DateTime? fromDate = dtpFromDate.Value;
                //DateTime? tillDate = dtpTillDate.Value;

                if (chkAutoLogoutController.Checked == true)
                {
                    Comment = "Auto Logout";// , No Activity was Performed from Last 10 Mins";
                }
                LoadReport(name, dtpFromDate.Value.ToDate(), dtpTillDate.Value.ToDate(), id, Comment);
                ExportReport();
            }
        }
    }
}
