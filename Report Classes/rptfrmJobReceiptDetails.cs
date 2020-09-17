using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Utils;
using System.IO;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;


namespace Taxi_AppMain
{
    public partial class rptfrmJobReceiptDetails : Form
    {
       


        private List<Vu_BookingDetail> _DataSource;

        public List<Vu_BookingDetail> DataSource
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



        public void GenerateReport()
        {


            try
            {
                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyLogo", path);



                string header ="     "+ AppVars.objSubCompany.Address.ToStr() + ",\n" + 
                            "       Telephone No:" + AppVars.objSubCompany.TelephoneNo;
                header += ". Fax:" + AppVars.objSubCompany.Fax.ToStr();
                header += "\nEmergency Contact No:" +AppVars.objSubCompany.EmergencyNo.ToStr() + ". " + AppVars.objSubCompany.WebsiteUrl;
                header += "\n                Email:" + AppVars.objSubCompany.EmailAddress.ToStr();


                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", header);



                reportViewer1.LocalReport.SetParameters(param);

                //ReportDataSource data = new ReportDataSource("vuBookingDetailBindingSource", this.DataSource);
                //this.reportViewer1.LocalReport.DataSources.Add(data);

                this.Vu_BookingDetailBindingSource.DataSource = this.DataSource;
                this.reportViewer1.ZoomMode= ZoomMode.PageWidth;
              //  this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();


               

           
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        public rptfrmJobReceiptDetails()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(rptfrmJobReceiptDetails_KeyDown);
            this.KeyPreview = true;
          
        }

        void rptfrmJobReceiptDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }


       

        


        public void SendEmail(string refNo, string email)
        {

            General.ShowEmailForm(reportViewer1, "Job Receipt - " + refNo, email);

        }

        private void rptfrmJobReceiptDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
