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
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls;
using Telerik.Data;
using Taxi_AppMain.Classes;
using Telerik.WinControls.UI.Export;

namespace Taxi_AppMain
{
    public partial class frmOperatorPrivateHireDriverRecord : UI.SetupBase
    {
        RadGridViewExcelExporter exporter = null;
        public frmOperatorPrivateHireDriverRecord()
        {
            InitializeComponent();
            grdLister.ReadOnly = true;
            grdLister.ShowGroupPanel = false;
            grdLister.EnableHotTracking = false;
            grdLister.EnableFiltering = true;
            this.Load += new EventHandler(frmPaymentCollection_Load);
            this.grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnShow.Click += new EventHandler(btnShow_Click);
            this.btnExport.Click += new EventHandler(btnExport_Click);
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    if (radGridView1 == null)
                        InitializeExportGrid();


                    radGridView1.Columns.Clear();
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("MonthCommencing", "MonthCommencing"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("OperatorLicenceNumber", "OperatorLicenceNumber"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("OperatorName", "OperatorName"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("PrivateHireLicenceNumber", "PrivateHireLicenceNumber"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("FirstName", "FirstName"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Surname", "Surname"));
                    //
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
                        ENUtils.ShowMessage(Message);
                        return;
                    }
                    MonthCommencing = string.Format("{0:dd/MM/yyyy}", dtFrom.Value) + "-" + string.Format("{0:dd/MM/yyyy}", dtTill.Value);
                    if (opAll.IsChecked)
                    {
                        BookingStatusId = 0;
                    }
                    if (opCompleted.IsChecked)
                    {
                        BookingStatusId = Enums.BOOKINGSTATUS.DISPATCHED.ToInt();
                    }
                    if (opCancelled.IsChecked)
                    {
                        BookingStatusId = Enums.BOOKINGSTATUS.CANCELLED.ToInt();
                    }
                    //

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var list = db.stp_OperatorPrivateHireDriverRecord(ddlSubCompany.SelectedValue.ToInt(), dtFrom.Value, dtTill.Value + TimeSpan.Parse("23:59:59"), BookingStatusId, MonthCommencing).ToList();  // grdLister.DataSource = list;



                        radGridView1.RowCount = list.Count;
                        for (int i = 0; i < list.Count; i++)
                        {
                            radGridView1.Rows[i].Cells["MonthCommencing"].Value = list[i].MonthCommencing;
                            radGridView1.Rows[i].Cells["OperatorLicenceNumber"].Value = list[i].OperatorLicenceNumber;
                            radGridView1.Rows[i].Cells["OperatorName"].Value = list[i].OperatorName;
                            radGridView1.Rows[i].Cells["PrivateHireLicenceNumber"].Value = list[i].PrivateHireLicenceNumber;
                            radGridView1.Rows[i].Cells["FirstName"].Value = list[i].FirstName;
                            radGridView1.Rows[i].Cells["Surname"].Value = list[i].Surname;
                        }
                    }

                    ExportToExcelML export = new ExportToExcelML(this.radGridView1);
                    export.ExportVisualSettings = true;
                    export.HiddenColumnOption = HiddenOption.ExportAsHidden;
                    export.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                    export.ExcelCellFormatting += new Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventHandler(export_ExcelCellFormatting);
                    //

                    //
                    //radGridView1.Columns["PickupDate"].HeaderText = "Pickup Date-Time";

                    //radGridView1.Columns["From"].HeaderText = "Pick-up Address";
                    //radGridView1.Columns["To"].HeaderText = "Drop-off Address";
                    radGridView1.Columns["MonthCommencing"].HeaderText = "Month Commencing";
                    radGridView1.Columns["OperatorLicenceNumber"].HeaderText = "Operator Licence Number";
                    radGridView1.Columns["OperatorName"].HeaderText = "Operator Name";
                    radGridView1.Columns["PrivateHireLicenceNumber"].HeaderText = "Private Hire Licence Number";
                    radGridView1.Columns["FirstName"].HeaderText = "First Name";

                    //CompanyName
                    exporter = new RadGridViewExcelExporter();

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                    worker.RunWorkerAsync(saveFileDialog1.FileName);
                    exporter.Progress += new ProgressHandler(exportProgress);

                    this.btnExport.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void btnShow_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }

        string cellValue = string.Empty;
        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = _HeaderRowBackColor;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }
        }

        void frmPaymentCollection_Load(object sender, EventArgs e)
        {
            FillSubCompanyCombo();
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
        }
        private void FillSubCompanyCombo()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    ddlSubCompany.DisplayMember = "CompanyName";
                    ddlSubCompany.ValueMember = "Id";
                    ddlSubCompany.DataSource = db.GetTable<Gen_SubCompany>().Select(args => new { args.Id, args.CompanyName }).ToList();

                    ddlSubCompany.DropDownStyle = ComboBoxStyle.DropDownList;
                    ddlSubCompany.SelectedValue = AppVars.objSubCompany.Id;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public override void PopulateData()
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
                        Message  +=Environment.NewLine;// "Required : To Date";
                        Message +=  "Required : To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Message))
                {
                    ENUtils.ShowMessage(Message);
                    return;
                }
                MonthCommencing = string.Format("{0:dd/MM/yyyy}", dtFrom.Value) + "-" + string.Format("{0:dd/MM/yyyy}", dtTill.Value);
                if (opAll.IsChecked)
                {
                    BookingStatusId = 0;
                }
                if (opCompleted.IsChecked)
                {
                    BookingStatusId = Enums.BOOKINGSTATUS.DISPATCHED.ToInt();
                }
                if (opCancelled.IsChecked)
                {
                    BookingStatusId = Enums.BOOKINGSTATUS.CANCELLED.ToInt();
                }
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_OperatorPrivateHireDriverRecord(ddlSubCompany.SelectedValue.ToInt(),dtFrom.Value, dtTill.Value + TimeSpan.Parse("23:59:59"), BookingStatusId, MonthCommencing).ToList();               
                    grdLister.DataSource = list;
                }
                grdLister.Columns["Id"].IsVisible = false;
                
                grdLister.Columns["BookingNo"].IsVisible = false;
                grdLister.Columns["DriverId"].IsVisible = false;
                grdLister.Columns["MonthCommencing"].HeaderText = "Month Commencing";
                grdLister.Columns["OperatorLicenceNumber"].HeaderText = "Operator Licence Number";
                grdLister.Columns["OperatorLicenceNumber"].Width = 200;
                grdLister.Columns["MonthCommencing"].Width = 160;
                grdLister.Columns["OperatorName"].HeaderText = "Operator Name";
                grdLister.Columns["PrivateHireLicenceNumber"].HeaderText = "Private Hire Licence Number";
                grdLister.Columns["FirstName"].HeaderText = "First Name";
                grdLister.Columns["OperatorName"].Width = 170;
                grdLister.Columns["FirstName"].Width = 150;
                grdLister.Columns["PrivateHireLicenceNumber"].Width = 210;
                //grdLister.Columns["Surname"].HeaderText = "First Name";
                grdLister.Columns["Surname"].Width = 120;
                //grdLister.Columns["Total"].Width = 130;
                //grdLister.Columns["DriverNo"].Width = 120;
                //Active
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        void export_ExcelCellFormatting(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {
            e.ExcelStyleElement.AlignmentElement.WrapText = false;
        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.IsDisposed)
            {
                e.Cancel = true;
                return;
            }
            exporter.Export(this.radGridView1, (String)e.Argument, "Operator Driver");
        }
        private void InitializeExportGrid()
        {
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();


            this.radGridView1.Location = new System.Drawing.Point(405, 60);
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(240, 150);
            this.radGridView1.TabIndex = 18;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.Visible = false;

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();

            //this.radPanel1.Controls.Add(this.radGridView1);
        }
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;
            ENUtils.ShowMessage("Export successfully.");
        }
        //Update the progress bar with the export progress    
        private void exportProgress(object sender, ProgressEventArgs e)
        {

            if (this.IsDisposed)
                return;
            // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(
                    delegate
                    {
                        if (e.ProgressValue <= 100)
                        {
                            radProgressBar1.Value1 = e.ProgressValue;
                        }
                        else
                        {
                            radProgressBar1.Value1 = 100;
                        }
                    }));
                }
                else
                {
                    if (e.ProgressValue <= 100)
                    {
                        radProgressBar1.Value1 = e.ProgressValue;
                    }
                    else
                    {
                        radProgressBar1.Value1 = 100;
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        
    }
}
