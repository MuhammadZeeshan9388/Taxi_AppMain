using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using DAL;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using System.Linq.Expressions;
using System.IO;

namespace Taxi_AppMain
{
    public partial class rptfrmCompanyVehicle : UI.SetupBase
    {
        bool IsReportLoaded = false;
        public rptfrmCompanyVehicle()
        {
            InitializeComponent();
            this.Load += new EventHandler(rptfrmCompnayVehicle_Load);
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.chkAllVehicle.CheckedChanged += new EventHandler(chkAllVehicle_CheckedChanged);
            this.btnExportPDF.Click += new EventHandler(btnExportPDF_Click);
            this.btnEmail.Click += new EventHandler(btnEmail_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnEmail_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded)
            {
                SendEmail();
            }
            else
            {
                GenerateReport();
                SendEmail();
            }
        }

        void btnExportPDF_Click(object sender, EventArgs e)
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

        void chkAllVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllVehicle.Checked)
            {
                ddlVehicleType.SelectedValue = null;
                ddlVehicleType.Enabled = false;
            }
            else
            {
                ddlVehicleType.Enabled = true;
            }
        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }
        private void GenerateReport()
        {
            try
            {

                int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();

                int VehicleTypeId = ddlVehicleType.SelectedValue.ToInt();

                if (SubCompanyId==0)
                {
                    ENUtils.ShowMessage("Required : Sub Company");
                    return;
                }


                if (optDetail.Checked)
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptCompanyVehicleDetail.rdlc";
                }
                else
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptCompanyVehicle.rdlc";
                }

                this.ClsAccVehDetailBindingSource.DataSource = this.GetDataSource(SubCompanyId,VehicleTypeId);
                //var list = General.GetQueryable<Fleet_Master>(c => (c.SubCompanyId == SubCompanyId) && (VehicleTypeId == 0 || c.VehicleTypeId == VehicleTypeId)).ToList();
                //this.Fleet_MasterBindingSource.DataSource = list;
                //Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];

                //string address = AppVars.objSubCompany.Address;
                //string telNo =  AppVars.objSubCompany.TelephoneNo;

                //param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);
                

                //reportViewer1.LocalReport.SetParameters(param);



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
        private List<ClsAccVehDetail> GetDataSource(int SubCompanyId, int VehicleTypeId)
        {
            try
            {
                var query = General.GetQueryable<Fleet_Master>(c => (c.SubCompanyId == SubCompanyId) && (VehicleTypeId == 0 || c.VehicleTypeId == VehicleTypeId)).ToList();

                List<ClsAccVehDetail> objClsAccVehDetail = new List<ClsAccVehDetail>();

                var objCompany = General.GetObject<Gen_SubCompany>(c => c.Id == SubCompanyId);

                for (int i = 0; i < query.Count; i++)
                {
                    objClsAccVehDetail.Add(new ClsAccVehDetail
                    {
                        Id = query[i].Id,
                        Fuel = query[i].Fleet_FuelType.FuelType,
                        InsuranceExpiry = query[i].InsuranceExpiry.ToDateTimeorNull(),
                        Labour = query[i].Labour.ToDecimal(),
                        ManufactureDate = query[i].ManufactureDate.ToDateTime(),
                        MOTExpiryDate = query[i].MOTExpiryDate.ToDateTimeorNull(),
                        PartDetails = query[i].PartDetails,
                        Parts = query[i].Parts.ToDecimal(),
                        PLateExpiryDate = query[i].PLateExpiryDate.ToDateTimeorNull(),
                        Plateno = query[i].Plateno,
                        RoadTaxExpDate = query[i].RoadTaxExpDate.ToDateTimeorNull(),
                        ServicesDate = query[i].ServicesDate.ToDateTimeorNull(),
                        TyreChanged = query[i].TyresChanged,
                        TyreChangeMileage = query[i].TyreChangeMileage.ToDecimal(),
                        VehicleColor = query[i].VehicleColor,
                        VehicleMake = query[i].VehicleMake,
                        VehicleModel = query[i].VehicleModel,
                        VehicleNo = query[i].VehicleNo,
                        VehicleOwner = query[i].VehicleOwner,
                        VehicleType = query[i].Fleet_VehicleType.VehicleType,
                        CostofTyres = query[i].CostofTyres.ToDecimal(),
                        SubCompany = objCompany.CompanyName,
                        SubCompanyTelephoneNo = objCompany.TelephoneNo,
                        SubCompanyAddress = objCompany.Address
                    });
                }

                return objClsAccVehDetail;
            }
            catch {
                return null;
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

            saveFileDlg.FileName = "Company Vehicle Report";

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
            General.ShowEmailForm(reportViewer1, "Company Vehicle Report");
        }
        void rptfrmCompnayVehicle_Load(object sender, EventArgs e)
        {
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany); ;
            if (ddlSubCompany.Items.Count() == 1)
            {
                ddlSubCompany.SelectedIndex = 0;
            }
            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);
        }

        private void rptfrmCompanyVehicle_Load(object sender, EventArgs e)
        {

        }


    }
}
