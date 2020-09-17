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
    public partial class rptfrmActivityLogReport : UI.SetupBase
    {
        bool IsReportLoaded = false;
        private List<stp_ActivityLogResult> _DataSource;

        public List<stp_ActivityLogResult> DataSource
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

        public rptfrmActivityLogReport()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(rptfrmAdminReport_Load);
        }

        void rptfrmAdminReport_Load(object sender, System.EventArgs e)
        {
            LoadUsers();
        }
        public void LoadUsers()
        {
            ComboFunctions.FillUsers(ddlUsers);
            dtpFromDate.Value = DateTime.Now.GetStartOfCurrentWeek().ToDateTimeorNull();
            dtpTillDate.Value = DateTime.Now.ToDate() + TimeSpan.Parse("23:59:59");

        }
        public void LoadReport(DateTime? dtFrom, DateTime? dtTill, int Id, string ControllerName)
        {
            try
            {
                ReInitializeReportViewer();
                //string reportPath = "Taxi_AppMain.ReportDesigns.";

                if (chkSplitByController.Checked)
                {
                    this.AdminActivity.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptActivityLogSplitByName.rdlc";
                }
                else
                {
                    this.AdminActivity.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptActivityLog.rdlc";
                }
                this.AdminActivity.LocalReport.EnableExternalImages = true;
                this.stp_ActivityLogResultBindingSource.DataSource = (from a in new Taxi_Model.TaxiDataContext().stp_ActivityLog(dtFrom, dtTill, Id, ControllerName)
                                                                      select new stp_ActivityLogResult
                                                                      {
                                                                          UserId = a.UserId,
                                                                          UserName = a.UserName,
                                                                          ActivityDescription = a.ActivityDescription,
                                                                          ActivityDateTime = a.ActivityDateTime
                                                                      }).ToList();



                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];

                // string address = AppVars.objSubCompany.Address;
                // string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                string heading = string.Empty;
                heading = "For the period of " + string.Format("{0:dd/MM/yy}", dtFrom) + " to " + string.Format("{0:dd/MM/yy}", dtTill);



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
        }
        bool prevValue = false;
        bool newValue = false;
        private void ReInitializeReportViewer()
        {

            //if (prevValue == newValue)
            //    return;

            AdminActivity.Clear();
            AdminActivity.Dispose();
            this.Controls.Remove(this.AdminActivity);
            GC.Collect();
            this.AdminActivity = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AdminActivity.Dock = System.Windows.Forms.DockStyle.Fill;




            reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();


            reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            //reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();

            //reportDataSource3.Name = "Taxi_Model_Vu_BookingBase";
            //reportDataSource3.Value = this.Vu_BookingBaseBindingSource;
            //reportDataSource4.Name = "Taxi_AppMain_Classes_ClsLogo";
            //reportDataSource4.Value = this.ClsLogoBindingSource;

            reportDataSource2.Name = "Taxi_Model_stp_ActivityLogResult";
            reportDataSource2.Value = this.stp_ActivityLogResultBindingSource;
            //reportDataSource3.Name = "Taxi_AppMain_Classes_ClsLogo";
            //reportDataSource3.Value = this.ClsLogoBindingSource;
            this.AdminActivity.LocalReport.DataSources.Add(reportDataSource2);
            this.AdminActivity.LocalReport.DataSources.Add(reportDataSource3);
            this.AdminActivity.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptActivityLog.rdlc";
            this.AdminActivity.Location = new System.Drawing.Point(0, 137);
            this.AdminActivity.Name = "AdminActivity";
            this.AdminActivity.Size = new System.Drawing.Size(1040, 675);
            this.AdminActivity.TabIndex = 116;

            this.Controls.Add(this.AdminActivity);
            AdminActivity.BringToFront();

            //prevValue = newValue;

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

            saveFileDlg.FileName = "Activity Log Report";

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

                if (chkAllController.Checked == true)
                {
                    id = 0;
                    name = string.Empty;
                }
                else
                {
                    if (id == 0)
                    {
                        ENUtils.ShowMessage("Required : Controller");
                        return;
                    }
                }
                LoadReport(dtpFromDate.Value.ToDateTimeorNull(), dtpTillDate.Value.ToDateTimeorNull(), id, name);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }


        // + TimeSpan.Parse("23:59:59")
        private List<stp_ActivityLogResult> GetData(DateTime? fromDate, DateTime? tillDate, int id, string name)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_ActivityLog(fromDate, tillDate, id, name)
                    select new stp_ActivityLogResult
                    {
                        UserId = a.UserId,
                        UserName = a.UserName,
                        ActivityDescription = a.ActivityDescription,
                        ActivityDateTime = a.ActivityDateTime
                    }).ToList();
        }


        public void SendEmail()
        {
            General.ShowEmailForm(AdminActivity, "Activity Log Report");
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

                if (chkAllController.Checked == true)
                {
                    id = 0;
                    name = string.Empty;
                }
                else
                {
                    if (id == 0)
                    {
                        ENUtils.ShowMessage("Required : Controller");
                        return;
                    }
                }
                LoadReport(dtpFromDate.Value.ToDateTimeorNull(), dtpTillDate.Value.ToDateTimeorNull(), id, name);
                ExportReport();
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string name = this.ddlUsers.Text.ToStr();
                int id = 0;
                id = ddlUsers.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

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

                ReportHeading = "For the period of " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                string Comment = string.Empty;
                if (chkAllController.Checked == true)
                {
                    id = 0;
                    name = string.Empty;
                }
                else
                {
                    if (id == 0)
                    {
                        ENUtils.ShowMessage("Required : Controller");
                        return;
                    }
                }

                DataSource = GetData(fromDate, tillDate, id, name);
                LoadReport(fromDate, tillDate, id, name);

                SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAllController_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkAllController.Checked)
            {
                ddlUsers.SelectedValue = null;
                ddlUsers.Enabled = false;
            }
            else
            {
                ddlUsers.Enabled = true;
            }
        }


    }
}
