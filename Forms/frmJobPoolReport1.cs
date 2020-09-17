using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Telerik.WinControls;
using Utils;
using Telerik.WinControls.UI.Export;
using Telerik.Data;
using System.IO;
using System.Diagnostics;
using Telerik.WinControls.UI.Export.ExcelML;
using System.Threading;
using System.Security.Cryptography;


namespace Taxi_AppMain
{
    public partial class frmJobPoolReport1 : UI.SetupBase
    {
        public float sum_fare { get; set; }
        bool IsOperatorVehicleRecordExported = false;
        public frmJobPoolReport1()
        {
            InitializeComponent();
            grdTransferredJob.ReadOnly = true;
            grdTransferredJob.ShowGroupPanel = false;
            grdTransferredJob.EnableHotTracking = false;
            grdTransferredJob.EnableFiltering = true;
           
           // this.btnShowOperator.Click += new EventHandler(btnShowOperator_Click);
            
            
            
            this.Load += new EventHandler(frmJobPoolReport1_Load);
        }

        void frmJobPoolReport1_Load(object sender, EventArgs e)
        {
            TransferredJob();
            AcceptedJob();
        }

        void btnExport2_Click(object sender, EventArgs e)
        {
           
        }
        


        void btnShowVehicle_Click(object sender, EventArgs e)
        {
            
        }
        void btnExport_Click(object sender, EventArgs e)
        {
           
           
        }
        

       
        void btnShowOperator_Click(object sender, EventArgs e)
        {
            TransferredJob();
        }
        
        

        public void TransferredJob()
        {
            try
            {


                
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = (from p in db.Bookings

                                  join v in db.Fleet_VehicleTypes on   p.VehicleTypeId equals v.Id
                               join s in db.BookingStatus on p.BookingStatusId equals s.Id

                                  where (p.BookingStatusId==21 ||p.BookingStatusId==25  || p.BookingStatusId==27) && ( (p.PickupDateTime>=dtp_TranferredTo.Value && p.PickupDateTime <= dtp_TransferredFrom.Value))


                                  select new
                                  {
                                      p.PickupDateTime,
                                      Pickup=p.FromAddress,
                                      Via=p.ViaString,
                                      Destination=p.ToAddress,
                                      p.CustomerName,
                                      Vehile = v.VehicleType,
                                      p.FareRate,
                                     Status=s.StatusName,
                                      
                                   //  Driver=p.CompanyCreditCardDetails,
                                      AcceptedSubCompanies = p.NotesString,
                                      DriverSignCall = p.CompanyCreditCardDetails

                                  }).ToList();
                    grdTransferredJob.DataSource = list;


                    //--------------
                    lbl_status_0.Text = "Total Booking(s) : " + grdTransferredJob.Rows.Count.ToString() + "  Total Fares : $ " + grdTransferredJob.Rows.Sum(c => c.Cells["FareRate"].Value.ToDecimal());



                    grdColSetting(grdTransferredJob, "PickupDateTime", "Pickup Date", 200);
                    grdColSetting(grdTransferredJob, "Pickup", "Pickup ", 200);
                    grdColSetting(grdTransferredJob, "Via", "Via", 200);
                    grdColSetting(grdTransferredJob, "Destination", "Destination", 200);
                    grdColSetting(grdTransferredJob, "CustomerName", "Passenger Name", 200);
                    grdColSetting(grdTransferredJob, "Vehile", "Vehicle Type", 200);
                    grdColSetting(grdTransferredJob, "FareRate", "Fare", 200);
                    grdColSetting(grdTransferredJob, "Status", "Status", 200);
                    grdColSetting(grdTransferredJob, "AcceptedSubCompanies", "Accepted Sub Company", 200);
                    grdColSetting(grdTransferredJob, "DriverSignCall", "Driver Call Sign", 200);
                   
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }
        public void grdColSetting(RadGridView gv,string colname,string headertext,int width)
        {
            gv.AllowAutoSizeColumns = true;
            gv.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gv.Columns[colname].HeaderText = headertext;
            gv.Columns[colname].Width = width;

           
        }
        public void AcceptedJob()
        {
            try
            {



                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = (from p in db.Bookings


                                join v in db.Fleet_VehicleTypes on p.VehicleTypeId equals v.Id
                                join d in db.Fleet_Drivers on p.DriverId equals d.Id

                                join s in db.BookingStatus on p.BookingStatusId equals s.Id

                               where p.BookingTypeId == 100 &&  ( (p.PickupDateTime >= dtp_AcceptedTo.Value && p.PickupDateTime <= dtp_AcceptedFrom.Value))
                                 


                                select new
                                {
                                    p.PickupDateTime,
                                    Pickup = p.FromAddress,
                                    Via = p.ViaString,
                                    Destination = p.ToAddress,
                                    p.CustomerName,
                                    Vehile = v.VehicleType,
                                    p.FareRate,
                                    Status = s.StatusName,

                                    Driver=d.DriverNo,
                                    // AcceptedSubCompanies=
                                    OriginSubCompany = p.NotesString
                                    //  DriverSignCall=   p.CompanyCreditCardDetails

                                }).ToList();
                    grdAcceptedJob.DataSource = list;

                    grdColSetting(grdAcceptedJob, "PickupDateTime", "Pickup Date", 200);
                    grdColSetting(grdAcceptedJob, "Pickup", "Pickup ", 200);
                    grdColSetting(grdAcceptedJob, "Via", "Via", 200);
                    grdColSetting(grdAcceptedJob, "Destination", "Destination", 200);
                    grdColSetting(grdAcceptedJob, "CustomerName", "Passenger Name", 200);
                    grdColSetting(grdAcceptedJob, "Vehile", "Vehicle Type", 200);
                    grdColSetting(grdAcceptedJob, "FareRate", "Fare", 200);
                    grdColSetting(grdAcceptedJob, "Status", "Status", 200);
                    //  grdColSetting(grdAcceptedJob, "Driver", "Accepted Sub Company", 200);
                    grdColSetting(grdAcceptedJob, "Driver", "Driver No", 200);
                    grdColSetting(grdAcceptedJob, "OriginSubCompany", "Origin Sub Company", 200);

                    lbl_status_1.Text = "Total Booking(s) : " + grdAcceptedJob.Rows.Count.ToString() + "  Total Fares :  $ " + grdAcceptedJob.Rows.Sum(c => c.Cells["FareRate"].Value.ToDecimal());
                    sum(grdAcceptedJob,"Fare");

                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveOn_Click(object sender, EventArgs e)
        {

        }

        public float sum(RadGridView rgv,string column)
        {
            //  float sum = 0;
            try
            {
                for (int i = 0; i < rgv.Rows.Count - 1; i++)
                {


                    sum_fare += (float)rgv.Rows[i].Cells["FareRate"].Value;
                }
            }
            catch (Exception)
            {
                sum_fare = 0;
            }
            return sum_fare;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void radProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOnNew_Click(object sender, EventArgs e)
        {

        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void grdTransferredJob_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {

        }

        private void btnShowOperator_Click_1(object sender, EventArgs e)
        {
            TransferredJob();
        }

        private void rdoCanceled_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void rdoCompleted_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void rdoAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ddlSubCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void grdOperatorVehicleRecord_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radProgressBar2_Click(object sender, EventArgs e)
        {

        }

        private void btnExport2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnShowVehicle_Click_1(object sender, EventArgs e)
        {

        }

        private void rdoCanceled2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void rdoCompleted2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void rdoAll2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void dtpToDate2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dtpFromDate2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ddlSubCompany2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btn_Accepted_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_ShowTransferred_job_Click(object sender, EventArgs e)
        {
            TransferredJob();
        }

        private void btn_ShowAccepted_job_Click(object sender, EventArgs e)
        {
            AcceptedJob();

        }

        private void btn_export_accepted_Click(object sender, EventArgs e)
        {
            ExportAccepted();
        }

        private void btn_export_transferred_Click(object sender, EventArgs e)
        {
            ExportTransferred();


        }
        private void ExportTransferred()
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    this.btn_export_transferred.Enabled = false;

                    for (int a = 0; a < grdTransferredJob.Columns.Count; a++)
                    {
                        grdTransferredJob.Columns[a].Width = 80;
                    }

                    ClsExportGridView objClsExportGridView = new ClsExportGridView(this.grdTransferredJob, saveFileDialog1.FileName);
                    objClsExportGridView.ApplyCellFormatting = true;
                    string headerText = "Date Range : " + string.Format("{0:dd-MMM-yy}", dtp_TransferredFrom.Value) + " to " + string.Format("{0:dd-MMM-yy}", dtp_TranferredTo.Value);
                    objClsExportGridView.Heading = headerText;
                    objClsExportGridView.TitleFontSize = 18;
                    objClsExportGridView.TitleBackColor = Color.Red;
                    objClsExportGridView.TitleForeColor = Color.White;
                    //objClsExportGridView.HeaderBackColor = Color.Black;
                    objClsExportGridView.HeaderForeColor = Color.White;
                    objClsExportGridView.ExportExcel();
                    //objClsExportGridView.ExportExcelAsync(radProgressBar1);
                    objClsExportGridView = null;
                    this.btn_export_transferred.Enabled = true;

                    for (int a = 0; a < grdTransferredJob.Columns.Count; a++)
                    {
                        grdTransferredJob.Columns[a].Width = 160;
                    }
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);

            }
        }
        private void ExportAccepted()
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    this.btn_export_accepted.Enabled = false;
                    for (int a = 0; a < grdAcceptedJob.Columns.Count; a++)
                    {
                        grdAcceptedJob.Columns[a].Width = 80;
                    }
                    ClsExportGridView objClsExportGridView = new ClsExportGridView(this.grdAcceptedJob, saveFileDialog1.FileName);
                    objClsExportGridView.ApplyCellFormatting = true;
                    string headerText = "Date Range : " + string.Format("{0:dd-MMM-yy}", dtp_TransferredFrom.Value) + " to " + string.Format("{0:dd-MMM-yy}", dtp_TranferredTo.Value);
                    objClsExportGridView.Heading = headerText;
                    objClsExportGridView.TitleFontSize = 18;
                    objClsExportGridView.TitleBackColor = Color.Red;
                    objClsExportGridView.TitleForeColor = Color.White;
                    //objClsExportGridView.HeaderBackColor = Color.Black;
                    objClsExportGridView.HeaderForeColor = Color.White;
                    objClsExportGridView.ExportExcel();
                    //objClsExportGridView.ExportExcelAsync(radProgressBar1);
                    objClsExportGridView = null;
                    this.btn_export_accepted.Enabled = true;
                    for (int a = 0; a < grdAcceptedJob.Columns.Count; a++)
                    {
                        grdAcceptedJob.Columns[a].Width = 160;
                    }
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);

            }
        }
    }
}
