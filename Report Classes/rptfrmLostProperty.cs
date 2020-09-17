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
    public partial class rptfrmLostProperty : UI.SetupBase
    {
        int LostId = 0;
        private List<stp_GetLostPropertyResult> _DataSource;

        public List<stp_GetLostPropertyResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        public rptfrmLostProperty(int Id)
        {
            InitializeComponent();
            LostId = Id;
        }

        private void rptfrmLostProperty_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            try
            {
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                this.stp_GetLostPropertyResultBindingSource.DataSource = GetData(LostId);





                string Name = AppVars.objSubCompany.CompanyName;
                string PhoneNo = "Phone No: " + AppVars.objSubCompany.TelephoneNo;
                string address = AppVars.objSubCompany.Address;
                //string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                //string heading = string.Empty;
                //if (dtFrom != null && dtTill != null)
                //{
                //heading = string.Format("{0:dd/MM/yy}", dtFrom) + " to " + string.Format("{0:dd/MM/yy}", dtTill);
                // }


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", Name);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyAddress", address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyPhoneNo", PhoneNo);
                // string address = AppVars.objSubCompany.Address;
                // string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                string heading = string.Empty;
                //if (dtFrom != null && dtTill != null)
                //{
                //heading = string.Format("{0:dd/MM/yy}", dtFrom) + " to " + string.Format("{0:dd/MM/yy}", dtTill);
                // }


              //  param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);

                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
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

            saveFileDlg.FileName = "Lost Property Report";

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
        private List<stp_GetLostPropertyResult> GetData(int id)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_GetLostProperty(id)
                    select new stp_GetLostPropertyResult
                    {
                        Id = a.Id,
                        LPNo = a.LPNo,
                        LostDate = a.LostDate,
                        Complaint = a.Complaint,
                        CustomerName = a.CustomerName,
                        CustomerMobileNo = a.CustomerMobileNo,
                        CustomerPhoneNo = a.CustomerPhoneNo,
                        CustomerAddress = a.CustomerAddress,
                        Disposition = a.Disposition,
                        Remarks = a.Remarks,
                        ReportedDate = a.ReportedDate,
                        Result = a.Result,
                        BookingNo = a.BookingNo,
                        BookingDate = a.BookingDate,
                        PickupDateTime = a.PickupDateTime,
                        FareRate = a.FareRate,
                        FromAddress = a.FromAddress,
                        ToAddress = a.ToAddress,
                        TotalCharges = a.TotalCharges,
                        NotesString = a.NotesString,
                        VehicleType = a.VehicleType,
                        DriverNo = a.DriverNo,
                        DriverName = a.DriverName,
                        JobId = a.JobId,
                        CheckedBy=a.CheckedBy
                    }).ToList();
        }
        public void SendEmail()
        {
            LoadReport();
          
            General.ShowEmailForm(reportViewer1, "Lost Property Report");
        }
    }
}
