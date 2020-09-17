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

namespace Taxi_AppMain
{
    public partial class frmTreasureOperatorReports : UI.SetupBase
    {
        bool IsOperatorVehicleRecordExported = false;
        public frmTreasureOperatorReports()
        {
            InitializeComponent();
            grdOperatorPrivateHireDriverRecord.ReadOnly = true;
            grdOperatorPrivateHireDriverRecord.ShowGroupPanel = false;
            grdOperatorPrivateHireDriverRecord.EnableHotTracking = false;
            grdOperatorPrivateHireDriverRecord.EnableFiltering = true;
           
            this.btnShowOperator.Click += new EventHandler(btnShowOperator_Click);
            DefaultDate();
            FillCombo();
            this.btnExport.Click += new EventHandler(btnExport_Click);
            this.btnShowVehicle.Click += new EventHandler(btnShowVehicle_Click);
            this.btnExport2.Click += new EventHandler(btnExport2_Click);
            this.Load += new EventHandler(frmTreasureOperatorReports_Load);
        }

        void frmTreasureOperatorReports_Load(object sender, EventArgs e)
        {
            LoadOperatorPrivateHireDriverRecord();
            OperatorVehicleRecod();
        }

        void btnExport2_Click(object sender, EventArgs e)
        {
            ExpoerOperatorVehicleRecord();
            if (IsOperatorVehicleRecordExported)
            {
                grdOperatorVehicleRecord.Columns["OperatorLicenceNumber"].Width = 170;
                grdOperatorVehicleRecord.Columns["MonthCommencing"].Width = 160;
                grdOperatorVehicleRecord.Columns["OperatorName"].Width = 170;
                grdOperatorVehicleRecord.Columns["VehicleMake"].Width = 150;
                grdOperatorVehicleRecord.Columns["VehicleRegistrationMark"].Width = 170;
                grdOperatorVehicleRecord.Columns["PHCVehicle"].Width = 130;
            }
        }
        private void ExpoerOperatorVehicleRecord()
        {
            try
            {
                if (DialogResult.OK == saveFileDialog2.ShowDialog())
                {
                    this.btnExport2.Enabled = false;
                    grdOperatorVehicleRecord.Columns["OperatorLicenceNumber"].Width = 110;
                    grdOperatorVehicleRecord.Columns["MonthCommencing"].Width = 90;
                    grdOperatorVehicleRecord.Columns["OperatorName"].Width = 90;
                    grdOperatorVehicleRecord.Columns["VehicleMake"].Width = 90;
                    grdOperatorVehicleRecord.Columns["VehicleRegistrationMark"].Width = 100;
                    grdOperatorVehicleRecord.Columns["PHCVehicle"].Width = 90;

                    ClsExportGridView objClsExportGridView = new ClsExportGridView(grdOperatorVehicleRecord, saveFileDialog2.FileName);
                    string headerText = "Date Range : " + string.Format("{0:dd-MMM-yy}", dtpFromDate2.Value) + " to " + string.Format("{0:dd-MMM-yy}", dtpToDate2.Value);
                    objClsExportGridView.Heading = headerText;
                    objClsExportGridView.TitleFontSize = 18;
                    objClsExportGridView.ApplyCellFormatting = true;
                    objClsExportGridView.TitleBackColor = Color.Red;
                    objClsExportGridView.TitleForeColor = Color.White;
                    objClsExportGridView.HeaderBackColor = Color.Black;
                    objClsExportGridView.HeaderForeColor = Color.White;
                    objClsExportGridView.ExportExcel();
                    objClsExportGridView = null;
                    this.btnExport2.Enabled = true;
                    IsOperatorVehicleRecordExported = true;
                    //grdOperatorVehicleRecord.Columns["PHCVehicle"].HeaderText = "PHC Vehicle";
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }
       

        void btnShowVehicle_Click(object sender, EventArgs e)
        {
            OperatorVehicleRecod();
        }
        void btnExport_Click(object sender, EventArgs e)
        {
            ExportOperatorPrivateHireDriverRecord();
           
        }
        private void ExportOperatorPrivateHireDriverRecord()
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    this.btnExport.Enabled = false;
                    grdOperatorPrivateHireDriverRecord.Columns["OperatorLicenceNumber"].Width = 110;

                    grdOperatorPrivateHireDriverRecord.Columns["MonthCommencing"].HeaderText = "Month";
                    grdOperatorPrivateHireDriverRecord.Columns["MonthCommencing"].Width = 40;
                    grdOperatorPrivateHireDriverRecord.Columns["OperatorName"].Width = 90;
                    grdOperatorPrivateHireDriverRecord.Columns["FirstName"].Width = 80;
                    grdOperatorPrivateHireDriverRecord.Columns["PrivateHireLicenceNumber"].Width = 100;
                    grdOperatorPrivateHireDriverRecord.Columns["Surname"].Width = 80;
                    grdOperatorPrivateHireDriverRecord.Columns["Surname2"].Width = 60;
                    ClsExportGridView objClsExportGridView = new ClsExportGridView(this.grdOperatorPrivateHireDriverRecord, saveFileDialog1.FileName);
                    objClsExportGridView.ApplyCellFormatting = true;
                    string headerText = "Date Range : " + string.Format("{0:dd-MMM-yy}", dtpFromDate.Value) + " to " + string.Format("{0:dd-MMM-yy}", dtpToDate.Value);
                    objClsExportGridView.Heading = headerText;
                    objClsExportGridView.TitleFontSize = 18;
                    objClsExportGridView.TitleBackColor = Color.Red;
                    objClsExportGridView.TitleForeColor = Color.White;
                    //objClsExportGridView.HeaderBackColor = Color.Black;
                    objClsExportGridView.HeaderForeColor = Color.White;
                    objClsExportGridView.ExportExcel();
                    //objClsExportGridView.ExportExcelAsync(radProgressBar1);
                    objClsExportGridView = null;
                    this.btnExport.Enabled = true;
                    grdOperatorPrivateHireDriverRecord.Columns["OperatorLicenceNumber"].Width = 160;
                    grdOperatorPrivateHireDriverRecord.Columns["MonthCommencing"].Width = 160;
                    grdOperatorPrivateHireDriverRecord.Columns["OperatorName"].Width = 170;
                    grdOperatorPrivateHireDriverRecord.Columns["FirstName"].Width = 150;
                    grdOperatorPrivateHireDriverRecord.Columns["PrivateHireLicenceNumber"].Width = 180;
                    grdOperatorPrivateHireDriverRecord.Columns["Surname"].Width = 110;
                    grdOperatorPrivateHireDriverRecord.Columns["Surname2"].Width = 80;

                    grdOperatorPrivateHireDriverRecord.Columns["MonthCommencing"].HeaderText = "Month Commencing";
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);

            }
        }

        private void DefaultDate()
        {
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
            dtpFromDate2.Value = DateTime.Now.AddMonths(-1);
            dtpToDate2.Value = DateTime.Now;
        }
        private void FillCombo()
        {
            using (TaxiDataContext db = new TaxiDataContext())
            {
                var list = (from a in db.Gen_SubCompanies
                            select new
                            {
                                Id = a.Id,
                                CompanyName = a.CompanyName
                            }).ToList();


                ddlSubCompany.DataSource = list;
                ddlSubCompany.DisplayMember = "CompanyName";
                ddlSubCompany.ValueMember = "Id";
                ddlSubCompany2.DataSource = list;
                ddlSubCompany2.DisplayMember = "CompanyName";
                ddlSubCompany2.ValueMember = "Id";
            }
        }
        void btnShowOperator_Click(object sender, EventArgs e)
        {
            LoadOperatorPrivateHireDriverRecord();
        }
        private void LoadOperatorPrivateHireDriverRecord()
        {
            try
            {
                string Message = string.Empty;
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate.Value.ToDateorNull();
                int BookingStatusId = 0;
                string MonthCommencing = string.Empty;
                if (dtFrom.Value == null)
                {
                    Message = "Required : From Date";
                }
                if (dtTill.Value == null)
                {
                    if (!string.IsNullOrEmpty(Message))
                    {
                        Message = "Required : To Date";
                    }
                    else
                    {
                        Message += Environment.NewLine;// "Required : To Date";
                        Message += "Required : To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Message))
                {
                    RadMessageBox.Show(Message);
                    return;
                }
                MonthCommencing = string.Format("{0:dd/MM/yyyy}", dtFrom.Value) + "-" + string.Format("{0:dd/MM/yyyy}", dtTill.Value);
                if (rdoAll.IsChecked)
                {
                    BookingStatusId = 0;
                }
                if (rdoCompleted.IsChecked)
                {
                    BookingStatusId = 2;
                }
                if (rdoCanceled.IsChecked)
                {
                    BookingStatusId = 3;
                }
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_OperatorPrivateHireDriverRecord(ddlSubCompany.SelectedValue.ToInt(), dtFrom.Value, dtTill.Value + TimeSpan.Parse("23:59:59"), BookingStatusId, MonthCommencing).ToList();
                    grdOperatorPrivateHireDriverRecord.DataSource = list;
             
                }               

                grdOperatorPrivateHireDriverRecord.Columns["Id"].IsVisible = false;
                grdOperatorPrivateHireDriverRecord.Columns["BookingNo"].IsVisible = false;
                grdOperatorPrivateHireDriverRecord.Columns["DriverId"].IsVisible = false;
             
                grdOperatorPrivateHireDriverRecord.Columns["DriverNo"].IsVisible = false;
                grdOperatorPrivateHireDriverRecord.Columns["MonthCommencing"].HeaderText = "Month Commencing";
                grdOperatorPrivateHireDriverRecord.Columns["OperatorLicenceNumber"].HeaderText = "Operator Licence Number";
                grdOperatorPrivateHireDriverRecord.Columns["OperatorLicenceNumber"].Width = 160;
                grdOperatorPrivateHireDriverRecord.Columns["MonthCommencing"].Width = 160;
                grdOperatorPrivateHireDriverRecord.Columns["OperatorName"].HeaderText = "Operator Name";
                grdOperatorPrivateHireDriverRecord.Columns["PrivateHireLicenceNumber"].HeaderText = "Private Hire Licence Number";
                grdOperatorPrivateHireDriverRecord.Columns["FirstName"].HeaderText = "First Name";
                grdOperatorPrivateHireDriverRecord.Columns["Surname2"].HeaderText = "NI";

                grdOperatorPrivateHireDriverRecord.Columns["OperatorName"].Width = 170;
                grdOperatorPrivateHireDriverRecord.Columns["FirstName"].Width = 150;
                grdOperatorPrivateHireDriverRecord.Columns["PrivateHireLicenceNumber"].Width = 180;
                grdOperatorPrivateHireDriverRecord.Columns["Surname"].Width = 110;
                grdOperatorPrivateHireDriverRecord.Columns["Surname2"].Width = 80;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }
        //Operator Vehicle recod

        private void OperatorVehicleRecod()
        {
            try
            {
                string Message = string.Empty;
                DateTime? dtFrom = dtpFromDate2.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate2.Value.ToDateorNull();
                int BookingStatusId = 0;
                string MonthCommencing = string.Empty;
                if (dtFrom.Value == null)
                {
                    Message = "Required : From Date";
                }
                if (dtTill.Value == null)
                {
                    if (!string.IsNullOrEmpty(Message))
                    {
                        Message = "Required : To Date";
                    }
                    else
                    {
                        Message += Environment.NewLine;// "Required : To Date";
                        Message += "Required : To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Message))
                {
                    RadMessageBox.Show(Message);
                    return;
                }
                MonthCommencing = string.Format("{0:dd/MM/yyyy}", dtFrom.Value) + "-" + string.Format("{0:dd/MM/yyyy}", dtTill.Value);
                if (rdoAll2.IsChecked)
                {
                    BookingStatusId = 0;
                }
                if (rdoCompleted2.IsChecked)
                {
                    BookingStatusId = 2;
                }
                if (rdoCanceled2.IsChecked)
                {
                    BookingStatusId = 3;
                }
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_OperatorVehicleRecord(ddlSubCompany2.SelectedValue.ToInt(), dtFrom.Value, dtTill.Value + TimeSpan.Parse("23:59:59"), BookingStatusId, MonthCommencing).ToList();
                    grdOperatorVehicleRecord.DataSource = list;
                }
                grdOperatorVehicleRecord.Columns["Id"].IsVisible = false;
                grdOperatorVehicleRecord.Columns["BookingNo"].IsVisible = false;
                grdOperatorVehicleRecord.Columns["DriverId"].IsVisible = false;
                grdOperatorVehicleRecord.Columns["DriverNo"].IsVisible = false;
                grdOperatorVehicleRecord.Columns["DriverName"].IsVisible = false;
                grdOperatorVehicleRecord.Columns["MonthCommencing"].HeaderText = "Month Commencing";
                grdOperatorVehicleRecord.Columns["OperatorLicenceNumber"].HeaderText = "Operator Licence Number";
                grdOperatorVehicleRecord.Columns["OperatorLicenceNumber"].Width = 170;
                grdOperatorVehicleRecord.Columns["MonthCommencing"].Width = 160;
                grdOperatorVehicleRecord.Columns["OperatorName"].HeaderText = "Operator Name";
                grdOperatorVehicleRecord.Columns["VehicleRegistrationMark"].HeaderText = "Vehicle Registration Mark";
                grdOperatorVehicleRecord.Columns["VehicleMake"].HeaderText = "Vehicle Make";
                grdOperatorVehicleRecord.Columns["OperatorName"].Width = 170;
                grdOperatorVehicleRecord.Columns["VehicleMake"].Width = 150;
                grdOperatorVehicleRecord.Columns["VehicleRegistrationMark"].Width = 170;
                grdOperatorVehicleRecord.Columns["PHCVehicle"].Width = 130;
                grdOperatorVehicleRecord.Columns["PHCVehicle"].HeaderText = "PHC Vehicle";
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

        private void grdOperatorPrivateHireDriverRecord_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {

        }

        private void btnShowOperator_Click_1(object sender, EventArgs e)
        {

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
    }
}
