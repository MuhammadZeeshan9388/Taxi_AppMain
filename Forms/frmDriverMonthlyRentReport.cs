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
using Telerik.WinControls.Enumerations;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class frmDriverMonthlyRentReport : UI.SetupBase
    {


        private string _Criteria;

        public string Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }



        private List<vu_DriverRentHistory> _DataSource;

        public List<vu_DriverRentHistory> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string OrderNo = "OrderNo";
            public static string PupilNo = "PupilNo";

            public static string RefNumber = "RefNumber";

            public static string Passenger = "Passenger";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";

        }

        public void GenerateReport()
        {
            try
            {


                reportViewer1.LocalReport.EnableExternalImages = true;

           
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];

                string address = AppVars.objSubCompany.Address;
                string telNo = string.Empty;


                telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;              

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);

                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());

                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);



                Criteria =  string.Format("for the period {0:dd/MM/yyyy}", dtpFromDate.Value.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", dtpTillDate.Value.ToDate());


                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", Criteria);      

            

                reportViewer1.LocalReport.SetParameters(param);

                this.vu_DriverRentHistoryBindingSource.DataSource = this.DataSource;

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }
        public frmDriverMonthlyRentReport()
        {
            InitializeComponent();


            
            ComboFunctions.FillDriverNoCombo(ddlDriver, c => c.DriverTypeId == 1);
         //   rdoMonyhly.IsChecked = true;
            int Month = DateTime.Now.Month.ToInt();
            string Year = DateTime.Now.Year.ToStr();



            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());



          

        }






        public void ExportReport(string invoiceNo)
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
            saveFileDlg.FileName = "DriverMonthlyRentReport-" + invoiceNo;


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

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name == "btnUpdate")
            {

                GridViewRowInfo row = gridCell.RowInfo;

                if (row is GridViewDataRowInfo)
                {
                    long id = row.Cells[COLS.ID].Value.ToLong();
                    decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                    decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                    decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                    decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                    decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                    decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                    decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();


                    BookingBO objMaster = new BookingBO();
                    objMaster.GetByPrimaryKey(id);

                    if (objMaster.Current != null)
                    {
                        objMaster.Current.FareRate = fare;
                        objMaster.Current.ParkingCharges = parking;
                        objMaster.Current.WaitingCharges = waiting;
                        objMaster.Current.ExtraDropCharges = extraDrop;
                        objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                        objMaster.Current.CongtionCharges = CongtionCharge;
                        objMaster.Current.TotalCharges = TotalCharges;


                        objMaster.Save();

                        ViewReport();
                    }


                }


            }

        }


        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 50;

            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Update";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }


        private void ViewReport()
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();
                int Month = ddlMonth.SelectedIndex.ToInt();
                
                string Year = ddlYear.SelectedText.ToStr();




                if (dtpFromDate.Value == null || dtpTillDate.Value == null)
                {

                    ENUtils.ShowMessage("Required : From Date or Till Date");
                    return;
                }
                else
                {
                    if (dtpFromDate.Value.ToDate() > dtpTillDate.Value.ToDate())
                    {
                        ENUtils.ShowMessage("From Date must be less than Till date");
                        return;
                    }


                }


               
                    string Dates = ddlDates.SelectedText.ToStr();

                    DateTime FromDate = dtpFromDate.Value.ToDate();
                    DateTime ToDate = dtpTillDate.Value.ToDate();

                    var list = General.GetQueryable<vu_DriverRentHistory>(a => (a.DriverId == DriverId || DriverId==0) && (a.FromDate >= FromDate && a.ToDate <= ToDate)).ToList();


                    this.DataSource = list;
                    GenerateReport();

               // }

            }
            catch (Exception ex)
            {
            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            ViewReport();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        public void SendEmail(string invoiceNo)
        {

            General.ShowEmailForm(reportViewer1, "Driver Monthly RentReport # " + invoiceNo);

        }

        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();

                int Month = ddlWeekMonths.SelectedIndex.ToInt();

                ComboFunctions.FillDriverDates(ddlDates, c => c.DriverId == DriverId && c.FromDate.Value.Month == Month);
            }
            catch (Exception ex)
            {

            }
        }

       

        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlDriver.SelectedValue = null;
                ddlDriver.Enabled = false;
            }
            else
            {
                ddlDriver.Enabled = true;

            }
        }


    }
}
