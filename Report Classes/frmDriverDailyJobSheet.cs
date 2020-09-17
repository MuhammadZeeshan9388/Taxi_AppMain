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
    public partial class frmDriverDailyJobSheet: UI.SetupBase
    {
        private List<stp_DriverDailyJobSheetResult> _DataSource;

        public List<stp_DriverDailyJobSheetResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public frmDriverDailyJobSheet()
        {
            InitializeComponent();
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDriverDailyJobSheet_Load(object sender, EventArgs e)
        {
            LoadDate();
        }
        private void LoadDate()
        {
            dtpFromDate.Value = DateTime.Now.ToDate();
            dtpTillDate.Value = DateTime.Now.ToDate();
            ComboFunctions.FillDriverNoCombo(ddl_Driver);
        }
        private void ReInitializeReportViewer()
        {

            //if (prevValue == newValue)
            //    return;

            reportViewer1.Clear();
            reportViewer1.Dispose();
            this.Controls.Remove(this.reportViewer1);

            GC.Collect();

            // 
            // reportViewer1
            // 
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Taxi_Model_stp_DriverDailyJobSheetResult";
            reportDataSource1.Value = this.stp_DriverDailyJobSheetResultBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.Template1_rptDriverDailyJobSheet.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 141);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1103, 472);
            this.reportViewer1.TabIndex = 110;

            this.Controls.Add(this.reportViewer1);
            reportViewer1.BringToFront();

           // prevValue = newValue;

        }
        public void LoadReport()
        {
            try
            {
                
                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;


                int DriverId = ddl_Driver.SelectedValue.ToInt(); ;
                string Error = string.Empty;
             
                if (DriverId == 0)
                {
                   Error= "Requird : Driver";
                
                }
                UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);
                if (objTemplate==null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Requird : Report Template";
                    }
                    else
                    {
                        Error +=Environment.NewLine+ "Requird : Report Template";
                    }
                }
 

                    if (!string.IsNullOrEmpty(Error))
                    {
                        ENUtils.ShowMessage(Error);
                        return;
                    }

                    ReInitializeReportViewer();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpTillDate.Value.ToDate()  + dtpTillTime.Value.Value.TimeOfDay).ToDateTime();



                string criteria = string.Format("{0:dd-MM-yy}", fromDate) + " to " + string.Format("{0:dd-MM-yy}", tillDate);



                 string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";
                
                if (objTemplate.TemplateName.ToStr() == "Template1")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverDailyJobSheet.rdlc";
                }
                else if (objTemplate.TemplateName.ToStr() == "Template2")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverDailyJobSheet.rdlc";
                }
                this.reportViewer1.RefreshReport();

                this.reportViewer1.LocalReport.EnableExternalImages = true;
                
                //var list = (from a in new Taxi_Model.TaxiDataContext().stp_DriverDailyJobSheet(DriverId, fromDate, tillDate)
                //            select new stp_DriverDailyJobSheetResult
                //            {
                //                StartDateTime = a.StartDateTime,
                //                clearedDateTimeBookingGroup=a.clearedDateTimeBookingGroup,
                //                FromAddress=a.FromAddress,
                //                ToAddress=a.ToAddress,
                //                noofpassengers=a.noofpassengers,
                //                noofluggages=a.noofluggages,
                //                FareRate=a.FareRate,
                //                acceptedDateTime=a.acceptedDateTime,
                //                CustomerName=a.CustomerName,
                //                BookingNo=a.BookingNo,
                //                DriverId=a.DriverId,
                //                clearedDateTime=a.clearedDateTime,
                //                Id=a.Id,
                //                DriverName=a.DriverName,
                //                groupJobId=a.groupJobId,
                //                Pickupdatetime=a.Pickupdatetime,
                                
     
                //                VehicleNo=a.VehicleNo,
                //                RoomNo=a.RoomNo
                //            }).ToList();



                var list = new Taxi_Model.TaxiDataContext().stp_DriverDailyJobSheet(DriverId, fromDate, tillDate).ToList();
               
                    var grpList = list


                        .GroupBy(argss =>
                            new
                            {

                                GrpId = argss.groupJobId


                            })
                            .Select(args =>
                                new
                                {
                                    GroupId = args.Key.GrpId,
                                    GrpCnt = args.Count()


                                }).ToList();



                    foreach (var item in grpList)
                    {

                        for (int i = 0; i < 28 - item.GrpCnt; i++)
                        {
                            list.Add(new stp_DriverDailyJobSheetResult { groupJobId = item.GroupId });


                        }

                    }

                    stp_DriverDailyJobSheetResultBindingSource.DataSource = list;
                    Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];
                   


                    string websiteUrl = AppVars.objSubCompany.WebsiteUrl.ToStr();

                    websiteUrl = websiteUrl.Replace("www.", "").Trim();
                   

                    websiteUrl=websiteUrl.Replace("http://","").Trim();



                    param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_WebsiteUrl", websiteUrl);
                    param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", criteria);
                    param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", AppVars.objSubCompany.CompanyName.ToStr());
                    param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyAddress", AppVars.objSubCompany.Address.ToStr());
                   // param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", criteria);


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
        private string Time(DateTime? dt)
        {
           string time = string.Empty;
           time= dt.Value.ToShortTimeString();
           return time;
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
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
            saveFileDlg.FileName = "Driver Daily Job Sheet";

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

        private void btnSendEmail_Click(object sender, EventArgs e)
        {

            try
            {
                int id = ddl_Driver.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;

                if (ddl_Driver.SelectedIndex == -1)
                {
                    ENUtils.ShowMessage("Requird: Driver");
                    return;
                }
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

                //ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                DataSource = GetData(id, fromDate, tillDate);

                LoadReport();


                SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private List<stp_DriverDailyJobSheetResult> GetData(int id,DateTime? fromDate, DateTime? tillDate )
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_DriverDailyJobSheet(id, fromDate, tillDate)
                    select new stp_DriverDailyJobSheetResult
                    {
                        StartDateTime = a.StartDateTime,
                        clearedDateTimeBookingGroup = a.clearedDateTimeBookingGroup,
                        FromAddress = a.FromAddress,
                        ToAddress = a.ToAddress,
                        noofpassengers = a.noofpassengers,
                        noofluggages = a.noofluggages,
                        FareRate = a.FareRate,
                        acceptedDateTime = a.acceptedDateTime,
                        CustomerName = a.CustomerName,
                        BookingNo = a.BookingNo,
                        DriverId = a.DriverId,
                        clearedDateTime = a.clearedDateTime,
                        Id = a.Id,
                        DriverName = a.DriverName,
                        groupJobId = a.groupJobId,
                        Pickupdatetime = a.Pickupdatetime,

                        VehicleNo = a.VehicleNo,
                        RoomNo = a.RoomNo

                    }).ToList();
        }
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Daily Job Sheet");
        }
       
    }
}
