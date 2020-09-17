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
using System.Collections;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverAccountBookings : UI.SetupBase
    {
        bool IsReportLoaded = false;

        string PaymentType = string.Empty;



        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);



        private List<stp_DriverAccountBookingsResult> _DataSource;

        public List<stp_DriverAccountBookingsResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        public rptfrmDriverAccountBookings()
        {
            InitializeComponent();
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            btnExport.Click += new EventHandler(btnExport_Click);
            this.btnEmail.Click += new EventHandler(btnEmail_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.panel1.Click += new EventHandler(pnlCriteria_Click);
            this.grdPaymentTypes.CellBeginEdit += new GridViewCellCancelEventHandler(grdPaymentTypes_CellBeginEdit);


            FormatPaymentTypeGrid();
            LoadPaymentTypes();
          
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnEmail_Click(object sender, EventArgs e)
        {
            DateTime? fromDate = dtpFrom.Value.ToDateorNull();
            DateTime? tillDate = dtpTill.Value.ToDateorNull() ;

            string error = string.Empty;


            if (fromDate == null)
            {
                if (string.IsNullOrEmpty(error))
                  //  error += Environment.NewLine;

                error += "Required : From Date";
            }

            if (tillDate == null)
            {
                if (!string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : To Date";
            }
            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;
            }

            DataSource = GetData(fromDate, tillDate + TimeSpan.Parse("23:59:59"));
            
            SendEmail();
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded == true)
            {
                ExportReport();
            }
            else
            {
                LoadReport();
                ExportReport();
            }

        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void rptfrmDriverAccountBookings_Load(object sender, EventArgs e)
        {
            DefaultDate();
        }
        private List<stp_DriverAccountBookingsResult> GetData(DateTime? fromDate, DateTime? tillDate )
        {
            string vat = "2";

            if (optIsNonVat.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                vat = "0";
            }
            else if (optIsVat.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                vat = "1";
            }

            var result =   (from a in new Taxi_Model.TaxiDataContext().stp_DriverAccountBookings(fromDate, tillDate)
                    where ((vat=="2") 
                    || (vat=="1" && a.FromPostCode=="1")
                     || (vat == "0" && a.FromPostCode == "0"))
                    select new stp_DriverAccountBookingsResult
                    {
                        Id = a.Id,
                        AgentCommission = a.AgentCommission,
                        BookedBy = a.BookedBy,
                        BookingDate = a.BookingDate,
                        BookingNo = a.BookingNo,
                        CompanyPrice = a.CompanyPrice,
                        DriverId = a.DriverId,
                        DriverNo = a.DriverNo,
                        ExtraDropCharges = a.ExtraDropCharges,
                        WaitingCharges = a.WaitingCharges,
                        VehicleType = a.VehicleType,
                        FareRate = a.FareRate,
                        FromAddress = a.FromAddress,
                        ToAddress = a.ToAddress,
                        TotalCharges = a.TotalCharges,
                        PickupDateTime = a.PickupDateTime,
                        ToPostCode=a.ToPostCode
                     
                    }).AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>()).ToList();

            if (ddlAllDriver.SelectedValue != null)
            {
                result  = result.Where(x => x.DriverId == Convert.ToInt32(ddlAllDriver.SelectedValue)).ToList();
            }
            if (txtPaymentTypes.Text.Length > 0)
            {
                result = result.Where(x => x.PaymentType == (txtPaymentTypes.Text.ToLower().ToString())).ToList();
                 
            }
            return result;
        }
        public struct COLSP
        {
            public static string Id = "Id";
            public static string PaymentType = "PaymentType";
            public static string Check = "Check";
        }
        private void FormatPaymentTypeGrid()
        {
            grdPaymentTypes.AllowEditRow = true;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLSP.Id;
            col.IsVisible = false;
            grdPaymentTypes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLSP.PaymentType;
            col.HeaderText = "Payment Type";
            col.Width = 100;
            col.ReadOnly = true;

            grdPaymentTypes.Columns.Add(col);
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLSP.Check;
            col.ReadOnly = false;
            cbcol.Width = 40;
            grdPaymentTypes.Columns.Add(cbcol);
        }

        IList listofPaymentTypes = null;

        private void LoadPaymentTypes()
        {
            try
            {
                var list = (from a in General.GetQueryable<Gen_PaymentType>(c => c.IsVisible == true)
                            select new
                            {
                                Id = a.Id,
                                PaymentType = a.PaymentType
                            }).ToList();
                grdPaymentTypes.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {

                    grdPaymentTypes.Rows[i].Cells[COLSP.Id].Value = list[i].Id;
                    grdPaymentTypes.Rows[i].Cells[COLSP.PaymentType].Value = list[i].PaymentType;

                }


                grdPaymentTypes.ShowColumnHeaders = false;

                listofPaymentTypes = list;

            }
            catch (Exception ex)
            {

            }
        }

        void pnlCriteria_Click(object sender, EventArgs e)
        {
            grdPaymentTypes.Visible = false;
            btnSelectPaymentType.ToggleState = ToggleState.Off;
        }

        void btnSelectPaymentType_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (btnSelectPaymentType.ToggleState == ToggleState.On)
            {
                grdPaymentTypes.Visible = true;
            }
            else
            {
                grdPaymentTypes.Visible = false; ;
            }
        }
        void grdPaymentTypes_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            //if (grdPaymentTypes.CurrentRow is GridViewRowInfo && grdPaymentTypes.CurrentRow != null)
            //{


            if (e.Column != null && e.Column.Name == COLSP.PaymentType)
                e.Cancel = true;

            try
            {


                string Payment = string.Join(",", grdPaymentTypes.Rows.Where(c => c.Cells[COLSP.Check].Value.ToBool()).Select(c => c.Cells[COLSP.PaymentType].Value.ToStr()).ToArray<string>());
                txtPaymentTypes.Text = Payment;
            }
            catch (Exception ex)
            {


            }

        }
        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {



                //ddlCompany.SelectedValue = null;
               // ddlCompany.Enabled = false;


            }
            else
            {
               // if (ddlCompany.DataSource == null)
                {
                 //   ComboFunctions.FillCompanyCombo(ddlCompany);

                }

               // ddlCompany.Enabled = true;

            }
        }
        private void chkAllDriver_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            
        }


        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Account Report");
        }
        private void DefaultDate()
        {
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpTill.Value = DateTime.Now;
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

            saveFileDlg.FileName = "Driver Account Report";

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
        private void LoadReport()
        {
            try
            {
                DateTime? dtFrom = dtpFrom.Value.ToDateorNull();
                DateTime? dtTill = dtpTill.Value.ToDateorNull() ; ;
                string error = string.Empty;


                if (dtFrom == null)
                {
                    if (string.IsNullOrEmpty(error))
                        //error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (dtTill == null)
                {
                    if (!string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";
                }
                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                this.stp_DriverAccountBookingsResultBindingSource.DataSource =GetData(dtFrom, dtTill + TimeSpan.Parse("23:59:59"));
                                                                          



                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

                string heading = string.Empty;
               //heading = "From: "+string.Format("{0:dd/MM/yyyy}", dtFrom) + " To: " + string.Format("{0:dd/MM/yyyy}", dtTill);
                string From=string.Format("{0:dd/MM/yyyy}",dtFrom);
                string To = string.Format("{0:dd/MM/yyyy}", dtTill);
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", AppVars.objSubCompany.CompanyName);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_FromDate", From);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_ToDate", To);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_ReportDate", string.Format("{0:dd MMMM yyyy}", DateTime.Now));
           

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

        private void chkAllDriver_ToggleStateChanged_1(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlAllDriver.SelectedValue = null;
                ddlAllDriver.Enabled = false;
                //ddlAllDriver.SelectedValue = 0;
            }

            else
            {
                if (ddlAllDriver.DataSource == null)
                {
                    //ComboFunctions.FillDriverCombo(ddlAllDriver);
                    ComboFunctions.FillDriverNoCombo(ddlAllDriver);
                }
                ddlAllDriver.Enabled = true;
            }

        }
    }
}
