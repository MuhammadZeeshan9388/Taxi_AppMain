using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.Data;

namespace Taxi_AppMain
{

    public partial class frmComplaint : UI.SetupBase
    {

        int savedId = 0;
        ComplaintBO objMaster;
        bool IsHidePopulateDate = false;
        public frmComplaint()
        {
            InitializeComponent();
            InitializeConstructor();
            rdoDriver.IsChecked = true;



            this.Load += new EventHandler(frmLocations_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            grdLister.RowsChanged += new GridViewCollectionChangedEventHandler(grdLister_RowsChanged);
            objMaster = new ComplaintBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;


            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

        }
        public frmComplaint(int Id, bool IsHide)
        {
            InitializeComponent();
            InitializeConstructor();


            grdLister.ShowRowHeaderColumn = false;

            ComboFunctions.FillCompany_AllCombo(ddlAccount);


            objMaster.GetByPrimaryKey(Id);
            DisplayRecord();
            radPanel2.Enabled = false;
            grdLister.Enabled = false;
            btnOnNew.Enabled = false;
            IsHidePopulateDate = IsHide;



        }

        public frmComplaint(int Id, List<Complaint> listofComplaints, long jobId, string jobRefNo,string customerName,string customerPhoneNo,string customerAddress, bool IsHide)
        {
            try
            {
                InitializeComponent();
                InitializeConstructor();





                grdLister.ShowRowHeaderColumn = false;

                ComboFunctions.FillCompanyCombo(ddlAccount);




                IsHidePopulateDate = IsHide;

                txtJobRef.Text = jobRefNo;
                txtJobRef.Tag = jobId;

                txtJobRef.Enabled = false;



                if (Id != 0)
                {

                    this.listOfBookingComplaints = listofComplaints;

                    objMaster.GetByPrimaryKey(Id);

                    DisplayRecord();


                }
                else
                {
                    grdLister.Enabled = false;

                    radPanel2.Enabled = false;

                    btnOnNew.Enabled = false;

                    txtCustomerName.Text = customerName.ToStr();
                    txtAddressDetail.Text = customerAddress.ToStr();
                    txtPhoneNo.Text = customerPhoneNo.ToStr();
                }
            }
            catch (Exception ex)
            {


            }

        }


        void grdLister_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                {
                    lblTotal.Text = "Total Complaint(s) : " + grdLister.Rows.Count.ToStr();

                }
            }
            catch
            {


            }
        }


        private void AddCreateBookingColumn()
        {

            if (grdLister.Columns.Contains("btnCreateBooking") == true)
                return;

            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 100;

            col.Name = "btnCreateBooking";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Create Booking";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grdLister.Columns.Add(col);

        }

        void frmComplaint_Shown(object sender, EventArgs e)
        {



            //if(listOfBookingComplaints!=null && listOfBookingComplaints.Count > 0)
            //{
            LoadComplaintGrid();

            //     }

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            if (this.CanDelete)
            {
                grdLister.AddDeleteColumn();
                grdLister.Columns["btnDelete"].Width = 70;
            }
            UI.GridFunctions.SetFilter(grdLister);


            txtSearch.Focus();
            txtComplainDate.Value = DateTime.Now.ToDate();
            txtincidentDate.Value = DateTime.Now.ToDate();


        }


        void frmLocations_Load(object sender, EventArgs e)
        {

            FillDriverNoComboIfNull();
            ComboFunctions.FillCompany_AllCombo(ddlAccount);

            if (objMaster.Current == null)
            {
                OnNewAllocation();

            }






        }

        private void LoadComplaintGrid()
        {

            try
            {

                PopulateData();
                AddCreateBookingColumn();
                grdLister.Columns["ID"].IsVisible = false;
                grdLister.Columns["PhoneNumber"].IsVisible = false;
                grdLister.Columns["RefNo"].Width = 55;
                grdLister.Columns["RefNo"].HeaderText = "Ref #";
                grdLister.Columns["ComplainDate"].HeaderText = "Complain Date";
                grdLister.Columns["ComplainDate"].Width = 150;
                grdLister.Columns["Name"].Width = 100;


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void InitializeConstructor()
        {
            objMaster = new ComplaintBO();
            this.SetProperties((INavigation)objMaster);

            //grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);

            ddlDriver.LostFocus += new EventHandler(ddlDriver_LostFocus);
            ddlDriver.Leave += new EventHandler(ddlDriver_Leave);
            this.Shown += new EventHandler(frmComplaint_Shown);

        }
        void ddlDriver_LostFocus(object sender, EventArgs e)
        {

        }

        void ddlDriver_Leave(object sender, EventArgs e)
        {
            FocusOnSave();
        }
        private void FocusOnSave()
        {
            //btnSaveNew.ButtonElement.Focus();
        }


        //Save
        public override void Save()
        {
            try
            {
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }

                objMaster.Current.ComplainDateTime = txtComplainDate.Value.ToDate();
                objMaster.Current.IncidentDateTime = txtincidentDate.Value.ToDate();
                objMaster.Current.CustomerName = txtCustomerName.Text.Trim();
                objMaster.Current.CustomerAddress = txtAddressDetail.Text.Trim();
                objMaster.Current.CustomerPhoneNo = txtPhoneNo.Text.Trim();

                objMaster.Current.JobDetail = txtJobDetail.Text.Trim();
                objMaster.Current.ControllerName = txtControlerName.Text.Trim();
                objMaster.Current.CustomerPhoneNo = txtPhoneNo.Text.Trim();

                objMaster.Current.ComplainDescription = txtComplain.Text.Trim();
                objMaster.Current.DealtWith = txtDealt.Text.Trim();

                objMaster.Current.ResultDescription = txtResult.Text.Trim();

                int? driverId = null;
                int? companyId = null;
                if (rdoDriver.IsChecked == true)
                {
                    driverId = ddlDriver.SelectedValue.ToIntorNull();
                }

                else if (rdoAccount.IsChecked == true)
                {
                    companyId = ddlAccount.SelectedValue.ToIntorNull();
                }
                objMaster.Current.CompanyId = companyId;
                objMaster.Current.DriverId = driverId;
                long BookingId = txtJobRef.Tag.ToLong();
                if (BookingId > 0 && txtJobRef.Text.Trim() != string.Empty)
                {
                    objMaster.Current.BookingId = BookingId;
                }

                else
                {

                    objMaster.Current.BookingId = null;
                }

                if (txtControlerName.Visible == true)
                {
                    objMaster.Current.ControllerName = txtControlerName.Text.Trim();
                }
                else
                {
                    objMaster.Current.ControllerName = "";
                }

                if (rdoDriver.IsChecked == true)
                {
                    objMaster.Current.ComplaintBy = 1;
                }
                else
                {
                    objMaster.Current.ComplaintBy = 0;
                }
                //New Code 15 Aprail

                if (rdoAccount.IsChecked == true)
                {
                    objMaster.Current.ComplaintBy = 2;

                }
                //
                objMaster.Save();
                savedId = objMaster.Current.Id;

                txtRefno.Text = objMaster.Current.RefNo.ToStr();
                if (IsHidePopulateDate == false)
                {
                    PopulateData();
                }

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }


        public override void AddNew()
        {
            OnNew();
        }


        private void OnNewAllocation()
        {
            txtRefno.Text = "Not Allocated";


        }

        public override void OnNew()
        {
            ClearAll();
            OnNewAllocation();
            txtincidentDate.Value = DateTime.Now;
            txtComplainDate.Value = DateTime.Now;

            txtCustomerName.Focus();
            objMaster.New();
            savedId = 0;
        }


        private void FillDriverNoComboIfNull()
        {

            if (ddlDriver.DataSource == null)
            {
                ComboFunctions.FillDriverNoCombo(ddlDriver,null);

            }
        }

        private void rdoDriver_MouseClick(object sender, MouseEventArgs e)
        {
            FillDriverNoComboIfNull();


            if (rdoDriver.IsChecked == false)
            {
                ddlDriver.Visible = true;
                txtControlerName.Visible = false;
                lblController.Text = "Driver / Controller";
                ddlAccount.Visible = false;
                ddlDriver.Text = "";
                //ddlAccount.SendToBack();
            }
        }

        private void rdoController_MouseClick(object sender, MouseEventArgs e)
        {
            if (rdoController.IsChecked == false)
            {
                txtControlerName.Visible = true;
                ddlDriver.Visible = false;
                lblController.Text = "Driver / Controller";
                ddlAccount.Visible = false;
                //ddlAccount.SendToBack();
            }
        }

        // Show Data
        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {

            // ViewDetailForm();
            objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());

            DisplayRecord();
        }

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            savedId = 0;
            txtRefno.Text = objMaster.Current.RefNo;
            txtComplainDate.Value = objMaster.Current.ComplainDateTime;
            txtincidentDate.Value = objMaster.Current.IncidentDateTime;
            txtCustomerName.Text = objMaster.Current.CustomerName;
            txtAddressDetail.Text = objMaster.Current.CustomerAddress;
            txtPhoneNo.Text = objMaster.Current.CustomerPhoneNo;
            txtJobDetail.Text = objMaster.Current.JobDetail;

            txtControlerName.Text = objMaster.Current.ControllerName;

            long BookingId = 0;



            if (objMaster.Current.ComplaintBy == 0)
            {
                ddlDriver.Visible = false;
                txtControlerName.Visible = true;
                rdoController.IsChecked = true;
                rdoDriver.IsChecked = false;
                ddlAccount.Visible = false;
                lblController.Text = "Driver / Controller";
            }
            else if (objMaster.Current.ComplaintBy == 1)
            {
                FillDriverNoComboIfNull();



                ddlDriver.Visible = true;
                txtControlerName.Visible = false;
                rdoController.IsChecked = false;
                rdoDriver.IsChecked = true;
                ddlAccount.Visible = false;
                lblController.Text = "Driver / Controller";
            }
            else if (objMaster.Current.ComplaintBy == 2)
            {

                ddlAccount.Visible = true;
                ddlAccount.BringToFront();
                txtControlerName.Visible = false;
                rdoAccount.IsChecked = true;
                lblController.Text = "Account";
                BookingId = txtJobRef.Tag.ToLong();

            }

            ddlDriver.SelectedValue = objMaster.Current.DriverId;
            ddlAccount.SelectedValue = objMaster.Current.CompanyId;


            txtJobRef.Tag = objMaster.Current.BookingId;
            txtJobRef.Text = objMaster.Current.Booking.DefaultIfEmpty().BookingNo;


            txtComplain.Text = objMaster.Current.ComplainDescription;
            txtDealt.Text = objMaster.Current.DealtWith;
            txtResult.Text = objMaster.Current.ResultDescription;
            //   ddlDriver.SelectedValue = objMaster.Current.DriverId;
        }


        List<Complaint> listOfBookingComplaints = null;

        private void LoadComplaintList()
        {
            try
            {
                string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                string col = ddlColumns.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;


                bool col_ref = false;
                bool col_name = false;
                bool col_date = false;
                if (col == "refrence no")
                {
                    col_ref = true;
                }

                if (col == "controller name")
                {
                    col_name = true;
                }
                if (col == "complain date")
                {
                    col_date = true;
                }



                var data1 = listOfBookingComplaints != null ? listOfBookingComplaints.AsEnumerable() : General.GetQueryable<Complaint>(null).AsEnumerable();
                //      var data2 = General.GetQueryable<Complaint>(null).AsEnumerable();


                var query = (from a in data1

                             where
                            (col_ref && (a.RefNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_date && (a.ComplainDateTime.ToStr().Contains(searchTxt) || searchTxt == string.Empty))

                             select new
                             {
                                 ID = a.Id,
                                 RefNo = a.RefNo,
                                 ComplainDate = a.ComplainDateTime,
                                 Name = a.CustomerName,
                                 PhoneNumber = a.CustomerPhoneNo

                             }).OrderByDescending(c => c.ComplainDate);

                grdLister.DataSource = query.ToList();
                grdLister.CurrentRow = null;

                lblTotal.Text = "Total Complaint(s) : " + grdLister.Rows.Count.ToStr();
            }
            catch
            {


            }

        }

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Complain ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {


                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "btncreatebooking")
            {
                General.ShowBookingForm(0, false, gridCell.RowInfo.Cells["Name"].Value.ToStr(), gridCell.RowInfo.Cells["PhoneNumber"].Value.ToStr(), Enums.BOOKING_TYPES.LOCAL);

            }

        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new ComplaintBO();

                try
                {

                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                    if (objMaster.Current != null)
                    {
                        objMaster.Delete(objMaster.Current);

                    }
                }
                catch (Exception ex)
                {
                    if (objMaster.Errors.Count > 0)
                        ENUtils.ShowMessage(objMaster.ShowErrors());
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }
                    e.Cancel = true;

                }

            }
        }

        public override void PopulateData()
        {

            LoadComplaintList();

        }

        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //int? driverId = ddlDriver.SelectedValue.ToIntorNull();
            //if (objMaster != null && objMaster.PrimaryKeyValue == null && driverId != null && chkAutoDespatch.Checked == false)
            //{
            //    btnSaveNew.Text = "Save and Dispatch";

            //}
            //else
            //{
            //    btnSaveNew.Text = "Save Booking";
            //}
        }

        private void ddlDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.P)
            {


                //frmPlotDriver frm = new frmPlotDriver(true);
                //frm.ShowDialog();

                //if (frm.Plotted)
                //{
                //    ComboFunctions.FillDriverNoQueueCombo(ddlDriver);
                //    ddlDriver.SelectedValue = frm.PlottedDriverId;
                //}
            }
        }

        private void btnShowAll_Click_1(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadComplaintList();
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            PopulateData();
        }
        public void ClearAll()
        {
            txtCustomerName.Text = string.Empty;
            txtDealt.Text = string.Empty;
            txtJobDetail.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtRefno.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtComplain.Text = string.Empty;
            txtAddressDetail.Text = string.Empty;
            txtControlerName.Text = string.Empty;
            ddlDriver.SelectedValue = null;
            ddlDriver.SelectedText = "";
            ddlAccount.SelectedText = "";
            txtJobRef.Text = "";
            txtJobRef.Tag = "";


        }

        private void btnOnNew_Click(object sender, EventArgs e)
        {
            AddNew();
        }

        private void frmComplaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }
        public void ComplainJobRef(long Id, string BookingNo)
        {
            txtJobRef.Tag = Id;
            txtJobRef.Text = BookingNo;
        }
        private void rdoAccount_MouseClick(object sender, MouseEventArgs e)
        {
            if (rdoAccount.IsChecked == false)
            {
                txtControlerName.Visible = true;
                ddlDriver.Visible = false;
                lblController.Text = "Account";
                ddlAccount.Visible = true;
                ddlAccount.Text = "";
                //ddlAccount.BringToFront();
            }
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoAccount.IsChecked == true)
                {
                    int CompanyId = 0;
                    CompanyId = ddlAccount.SelectedValue.ToInt();
                    if (CompanyId == 0)
                    {
                        ENUtils.ShowMessage("Required : Account");
                        return;
                    }
                    frmSearchBooking frm = new frmSearchBooking(CompanyId, true);
                    frm.ShowDialog();
                }
                else if (rdoDriver.IsChecked == true)
                {
                    int DriverId = 0;
                    DriverId = ddlDriver.SelectedValue.ToInt();
                    if (DriverId == 0)
                    {
                        ENUtils.ShowMessage("Required : Driver");
                        return;
                    }
                    frmSearchBooking frm = new frmSearchBooking(DriverId, true, true);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                long Id = 0;
                Id = txtJobRef.Tag.ToLong();
                if (Id > 0)
                {
                    frmBooking frm = new frmBooking();
                    frm.OnDisplayRecord(Id);
                    frm.ShowDialog();

                }
                else
                {
                    ENUtils.ShowMessage("Required : Booking Id");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = objMaster.PrimaryKeyValue != null ? objMaster.PrimaryKeyValue.ToInt() : savedId;

                if (Id>0)
                {
                  
                    rptfrmComplaints frm = new rptfrmComplaints(Id);
                    frm.ShowDialog();

                    frm.Dispose();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = objMaster.PrimaryKeyValue != null ? objMaster.PrimaryKeyValue.ToInt() : savedId;

                if (Id > 0)
                {
                    rptfrmComplaints frm = new rptfrmComplaints(Id);
                    frm.LoadReport();
                    frm.ExportReport();

                    frm.Dispose();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = objMaster.PrimaryKeyValue != null ? objMaster.PrimaryKeyValue.ToInt() : savedId;

                if (Id > 0)
                {
                    rptfrmComplaints frm = new rptfrmComplaints(Id);
                    frm.SendEmail();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        RadGridViewExcelExporter exporter = null;
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    radGridView1.Columns.Clear();
                    radGridView1.Rows.Clear();
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("RefNo", "RefNo"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("ComplainDate", "ComplainDate"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("CustomerName", "CustomerName"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("PhoneNumber", "PhoneNumber"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("ResultDescription", "ResultDescription"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("ComplainDescription", "ComplainDescription"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("ComplaintAgainst", "ComplaintAgainst"));
                   // radGridView1.Columns.Add(new GridViewTextBoxColumn("PhoneNumber", "PhoneNumber"));
                    string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                    string col = ddlColumns.Text.ToStr().Trim().ToLower();

                    if (searchTxt.Length < 3)
                        searchTxt = string.Empty;


                    bool col_ref = false;
                    bool col_name = false;
                    bool col_date = false;
                    if (col == "refrence no")
                    {
                        col_ref = true;
                    }

                    if (col == "controller name")
                    {
                        col_name = true;
                    }
                    if (col == "complain date")
                    {
                        col_date = true;
                    }

                    var data1 = listOfBookingComplaints != null ? listOfBookingComplaints.AsEnumerable() : General.GetQueryable<Complaint>(null).AsEnumerable();
                    
                    var query = (from a in data1

                                 where
                                (col_ref && (a.RefNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_date && (a.ComplainDateTime.ToStr().Contains(searchTxt) || searchTxt == string.Empty))

                                 select new
                                 {
                                     //string.Format("{0:dd/MMM/yyyy}", a.LostDate).ToStr(),
                                     RefNo = a.RefNo,
                                     ComplainDate = " " + string.Format(" {0:dd/MM/yyyy} ", a.ComplainDateTime) + "  ",
                                     CustomerName = a.CustomerName,
                                     PhoneNumber = a.CustomerPhoneNo,
                                     
                                     ComplainDescription = a.ComplainDescription,
                                     ResultDescription = a.ResultDescription,
                                     ComplaintAgainst=a.ComplaintBy==1 ? "Driver - "+ a.Fleet_Driver.DriverNo : a.ComplaintBy==2 ? "Controller - "+ a.ControllerName : "Account - "+ a.Gen_Company.CompanyName

                                 }).OrderByDescending(c => c.ComplainDate).ToList();

                    radGridView1.RowCount = query.Count;
                    for (int i = 0; i < query.Count; i++)
                    {
                        radGridView1.Rows[i].Cells["RefNo"].Value = query[i].RefNo;
                        radGridView1.Rows[i].Cells["ComplainDate"].Value = query[i].ComplainDate ;
                        radGridView1.Rows[i].Cells["CustomerName"].Value = query[i].CustomerName;
                        radGridView1.Rows[i].Cells["PhoneNumber"].Value = query[i].PhoneNumber;
                        radGridView1.Rows[i].Cells["ComplaintAgainst"].Value = query[i].ComplaintAgainst;
                        radGridView1.Rows[i].Cells["ResultDescription"].Value = query[i].ResultDescription;
                        radGridView1.Rows[i].Cells["ComplainDescription"].Value = query[i].ComplainDescription;
                        //radGridView1.Rows[i].Cells["PhoneNumber"].Value = query[i].PhoneNumber;
                    }
                    exporter = new RadGridViewExcelExporter();
                    BackgroundWorker worker = new BackgroundWorker();
                  //  worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                   // worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
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

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;
            ENUtils.ShowMessage("Export successfully.");
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.IsDisposed)
                {
                    e.Cancel = true;
                    return;

                }
                exporter.Export(this.radGridView1, (String)e.Argument, "Customer List");
            }
            catch (Exception ex)
            { }
        }
        private void exportProgress(object sender, ProgressEventArgs e)
        {
            if (this.IsDisposed)
                return;
            // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
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

    }
}
