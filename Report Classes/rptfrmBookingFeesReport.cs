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
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.Enumerations;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Taxi_AppMain
{
    public partial class rptfrmBookingFeesReport : UI.SetupBase
    {

        bool IsLoaded;

        public rptfrmBookingFeesReport()
        {
            InitializeComponent();
            this.dtpFromDate.Value = DateTime.Now.AddDays(-6).ToDate();
            dtpTillDate.Value = DateTime.Now.AddDays(1).ToDate();
            this.Load += new EventHandler(frmDriverReport_Load);

            this.chkAllSubCompany.ToggleStateChanged += new StateChangedEventHandler(chkAllSubCompany_ToggleStateChanged);
            this.chkAllDriver.ToggleStateChanged += ChkAllDriver_ToggleStateChanged;
            this.ddlSubCompany.Enter += DdlSubCompany_Enter;
            this.ddlDriver.Enter += DdlDriver_Enter;

        }

        private void ChkAllDriver_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlDriver.SelectedValue = null;
                ddlDriver.Enabled = false;
            }
            else
            {
                ddlDriver.Enabled = true;
            }
        }

        private void DdlDriver_Enter(object sender, EventArgs e)
        {
            if (ddlDriver.DataSource == null)
            {
                ComboFunctions.FillDriverCombo(ddlDriver);
            }
        }

        private void DdlSubCompany_Enter(object sender, EventArgs e)
        {
            if (ddlSubCompany.DataSource == null)
            {
                ComboFunctions.FillSubCompanyCombo(ddlSubCompany);
            }
        }

        void chkAllSubCompany_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlSubCompany.SelectedValue = null;
                ddlSubCompany.Enabled = false;
            }
            else
            {
                ddlSubCompany.Enabled = true;
            }
        }


        void frmDriverReport_Load(object sender, EventArgs e)
        {

            
           // IsLoaded = true;
        }


        
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }


        private string _Period;

        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }



        List<Vu_BookingBase> _DataSource;

        public List<Vu_BookingBase> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }





        public void GenerateReport()
        {


            try
            {
                int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();
                int DriverId = ddlDriver.SelectedValue.ToInt();
                

                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;


                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }

                this.DataSource= GetDataSource(SubCompanyId,DriverId, fromDate , tillDate + TimeSpan.Parse("23:59:59"));

                this.Period = "Period: "+ string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);

                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[8];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;







                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);




                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });

                ClsLogoBindingSource.DataSource = objLogo;


                // param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", AppVars.objSubCompany.CompanyName.ToStr().Trim());
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", Period);


                // Summary Calculations




                decimal OwnedjobsTotal = this.DataSource.Sum(c => c.FareRate.ToDecimal());


                decimal commissionTotal = this.DataSource.Sum(c => c.AgentCommission.ToDecimal());


                decimal owed = OwnedjobsTotal - commissionTotal;


                string jobsCnt = this.DataSource.Count.ToStr();





                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", string.Format("£ {0:f2}", OwnedjobsTotal));



                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt);
                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", string.Format("£ {0:f2}", commissionTotal));
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Owed", string.Format("£ {0:f2}", owed));

                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterHeader", AppVars.objSubCompany.CompanyName);



                reportViewer1.LocalReport.SetParameters(param);


                this.Vu_BookingBaseBindingSource.DataSource = this.DataSource;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();




               // this.reportViewer1.LocalReport.Refresh();
               // this.reportViewer1.Refresh();
                IsLoaded = true;
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }


        private List<Vu_BookingBase> listofTempDataSource = new List<Vu_BookingBase>();


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
            saveFileDlg.FileName = "Booking Fees Report";

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



        private List<Vu_BookingBase> GetDataSource(int SubCompanyId, int DriverId, DateTime? fromDate, DateTime? tillDate)
        {



            var list = General.GetQueryable<Vu_BookingBase>(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
            && c.PaymentTypeId==Enums.PAYMENT_TYPES.CASH
            && c.ServiceCharges>0
                && (SubCompanyId == 0 || c.SubCompanyId == SubCompanyId)
                && (DriverId == 0 || c.DriverId == DriverId)
                )

                    .Where(b => (b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate))
                        .OrderBy(c => c.PickupDateTime).ToList();


            System.Collections.Generic.List<Vu_BookingBase> listofData = new List<Vu_BookingBase>();
            Vu_BookingBase obj = null;
            foreach (var item in list)
            {
                obj = new Vu_BookingBase();
                obj.CustomerName = item.CustomerName;
                obj.CompanyName = item.CompanyName;
                obj.DepartmentName = item.DepartmentName;
                obj.CompanyAddress = item.CompanyAddress;
                obj.CompanyId = item.CompanyId;
                obj.BookedBy = item.BookedBy;
                obj.BookingNo = item.BookingNo;
                obj.PickupDateTime = item.PickupDateTime;
                obj.FromAddress = item.FromAddress;
                obj.ToAddress = item.ToAddress;
                obj.VehicleType = item.VehicleType;
                obj.DriverNo = item.DriverNo;
                obj.BookingStatusId = item.BookingStatusId;
                obj.StatusName = item.StatusName;
                obj.FareRate = item.FareRate;
                obj.CompanyPrice = item.FareRate.ToDecimal();
                obj.WaitingCharges = item.WaitingCharges.ToDecimal();
                obj.AgentCommission = item.ServiceCharges.ToDecimal();

                listofData.Add(obj);

            }


            return listofData;
        }


        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (IsLoaded)
            {
                GenerateReport();
            }
            else
            {
                GenerateReport();
                ExportReport();
            }
            //frm.ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (IsLoaded)
            {
                SendEmail();
            }
            else
            {
                GenerateReport();
                SendEmail();
            }

        }

        public void SendEmail()
        {

            General.ShowEmailForm(reportViewer1, "Booking Fees Report");

        }

        private void frmDriverCommissionReport_Load(object sender, EventArgs e)
        {

        }






    }
}
