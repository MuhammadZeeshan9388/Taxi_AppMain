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
using UI;
using Telerik.WinControls.UI;
using Telerik.WinControls.Enumerations;
using System.Collections;
using System.Threading;
using Taxi_AppMain.Forms;

namespace Taxi_AppMain
{
    public partial class frmAddressList :UI.SetupBase
    {
        BackgroundWorker bWorker = null;
        IList AddressList = null;
        bool HasError = false;
        int skip = 0;
        int pageSize = 5000;
        public frmAddressList()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdLister.CellFormatting += new CellFormattingEventHandler(grdLister_CellFormatting);
            this.Shown += new EventHandler(frmAddressList_Shown);
            this.FormClosing += new FormClosingEventHandler(frmAddressList_FormClosing);
        }

        private bool IsClosing;
        void frmAddressList_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
            if (bWorker != null)
            {
                bWorker.CancelAsync();
                bWorker.Dispose();



            }
           

        }

        void frmAddressList_Shown(object sender, EventArgs e)
        {
            FormatGrid();
            lblLoading.Visible = true;
           // InitializeLoading();
        }
        Font f = new Font("Tahoma", 10, FontStyle.Regular);
        void grdLister_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                e.CellElement.Font = f;
                if (e.CellElement is GridDataCellElement)
                {
                    if (e.Column is GridViewCommandColumn)
                    {

                        if (e.Column.Name == "btnEditAddress")
                        {
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.edit2;
                        }
                    }
                }
            }
            catch
            {


            }
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                    string name = gridCell.ColumnInfo.Name.ToStr();
                    if (name == "btnEditAddress")
                    {
                        string Address = string.Empty;
                        Address = grdLister.CurrentRow.Cells["Address"].Value.ToStr();
                        long Id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                        int? zoneId = grdLister.CurrentRow.Cells["Id"].Value.ToIntorNull();
                        frmAddNewAddress frm = new frmAddNewAddress(Id, Address,zoneId);
                        frm.ShowDialog();

                        frm.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        
        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    string Address = string.Empty;
                    Address = grdLister.CurrentRow.Cells["Address"].Value.ToStr();
                    long Id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                    frmAddNewAddress frm = new frmAddNewAddress(Id, Address, grdLister.CurrentRow.Cells["ZoneId"].Value.ToIntorNull());
                    frm.ShowDialog();   
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void GridButton()
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 160;
            col.Name = "btnEditAddress";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Edit Address";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(col);
        }
        private void frmAddressList_Load(object sender, EventArgs e)
        {
            if (bWorker == null)
            {

                bWorker = new BackgroundWorker();
                bWorker.WorkerSupportsCancellation = true;
                bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
                bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
                bWorker.RunWorkerAsync();
            }
        }
        
        public struct COLS
        {
            public static string Address = "Address";
            public static string PostCode = "PostCode";
            public static string Zone = "Zone";
            public static string ZoneId = "ZoneId";

            public static string EditAddress = "EditAddress";
        }
        public void FormatGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Address;
            col.HeaderText = "Address";
            col.Width = 700;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.PostCode;
            col.HeaderText = "Post Code";
            col.Width = 160;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS.Zone;
            col.HeaderText = "Zone";
            col.Width = 160;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ZoneId;
            col.HeaderText = "ZoneId";
            col.IsVisible = false;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.EditAddress;
            col.HeaderText = "Edit Address";
            col.Width = 160;
            grdLister.Columns.Add(col);
        }


        public override void PopulateData()
        {
            try
            {
                string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                string col = ddlColumns.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;

                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                {
                    string PostCode = string.Empty;
                    string Address = string.Empty;

                    if (col == "post code")
                    {
                        PostCode = txtSearch.Text.ToStr();
                    }
                    else if (col == "address")
                    {
                        Address = txtSearch.Text.ToStr();
                    }
                  
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var list = (from a in db.stp_GetRoadLevelAddress(PostCode,Address)
                                    select new
                                    {
                                        Id = a.Id,
                                        Address = a.Street,
                                        PostCode = a.PostCode,
                                        ZoneId=0,
                                        Zone=""
                                    }).ToList(); ;
                        
                        int cnt = list.Count;
                        if (skip + pageSize > cnt && cnt - pageSize > 0)
                            skip = cnt - pageSize;
                        else if (cnt <= pageSize)
                            skip = 0;
                        //.Skip(skip).Take(pageSize).OrderBy(c => c.Address)

                        var list2 = list.Skip(skip).Take(pageSize).OrderBy(c => c.Address).ToList();
                        AddressList = list2;
                    }
                }
                else
                {
                var list1 = General.GetQueryable<Gen_Address>(null).Count();


                
                if (this.IsFind)
                {
                  //  bool col_name = false;

                    bool col_postCode = false;

                    bool col_address = false;
                    bool col_zone = false;

                    if (col == "post code")
                    {
                        col_postCode = true;
                    }

                    else if (col == "address")
                    {
                        col_address = true;
                    }
                    else if (col == "zone")
                    {
                        col_zone = true;
                    }

                    int cnt = list1;
                    if (skip + pageSize > cnt && cnt - pageSize > 0)
                        skip = cnt - pageSize;
                    else if (cnt <= pageSize)
                        skip = 0;


                    int findZoneId=0;

                    if(col_zone)
                    {
                        findZoneId=General.GetObject<Gen_Zone>(c=>c.ZoneName.Contains(searchTxt) || c.ShortName.Contains(searchTxt)).DefaultIfEmpty().Id;

                    }


                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        var list = (from a in db.GetTable<Gen_Address>()
                                    join b in db.GetTable<Gen_Zone>() on a.ZoneId equals b.Id into table2
                                    from b in table2.DefaultIfEmpty()

                                     where

                                (col_postCode && (a.PostalCode.ToUpper().Contains(searchTxt) || searchTxt == string.Empty)

                                || (col_address && (a.AddressLine1 != null && a.AddressLine1.ToUpper().Contains(searchTxt) || searchTxt == string.Empty))
                                 || (col_zone && findZoneId!=0 && (a.ZoneId != null && a.ZoneId == findZoneId || searchTxt == string.Empty))
                               

                                )

                                    select new
                                    {
                                        Id = a.EntityID,
                                        Address = a.AddressLine1,
                                        PostCode = a.PostalCode,
                                        ZoneId = a.ZoneId,
                                        Zone = a.ZoneId != null ? b.ZoneName : ""
                                    }).Skip(skip).Take(pageSize).OrderBy(c=>c.Address).ToList();

                        AddressList = list;
                    }

                   

                    HasError = false;
                }
                else
                {
                
                    using(TaxiDataContext db=new TaxiDataContext())
                    {

                        var list = (from a in db.GetTable<Gen_Address>()
                                    join b  in db.GetTable<Gen_Zone>() on a.ZoneId equals b.Id into table2
                                    from b in table2.DefaultIfEmpty()

                                    select new
                                    {
                                        Id = a.EntityID,
                                        Address = a.AddressLine1,
                                        PostCode = a.PostalCode,
                                        ZoneId=a.ZoneId,
                                        Zone=a.ZoneId!=null ? b.ZoneName : ""
                                    }).Skip(skip).Take(pageSize).OrderBy(c => c.Address).ToList();

                        AddressList = list;
                    }
                }
               }
            }
            catch (Exception ex)
            {
                HasError = true;
            }
        }
        private bool IsFind = false;

        private void Find()
        {
            lblLoading.Visible = true;
            if (bWorker.IsBusy == false)
            {
                grdLister.Enabled = false;
                bWorker.RunWorkerAsync();
            }

           this.IsFind = true;
            skip = 0;
           // PopulateData();
        }
        delegate void DisplayProgressBar();
        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (IsClosing || IsDisposed)
                return;

            try
            {

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new DisplayProgressBar(PopulateData));
                }

                if (HasError == false)
                {
                    if (grdLister.Columns.Count > 0)
                    {
                        grdLister.Columns.Clear();
                    }


                    //FinishLoading();
                    lblLoading.Visible = false;
                    grdLister.DataSource = AddressList;
                 //   grdLister.Columns["Address"].SortOrder = RadSortOrder.Ascending;
                   // grdLister.Columns["Address"].AllowSort = true;
                    grdLister.Columns["Id"].IsVisible = false;
                    grdLister.Columns["ZoneId"].IsVisible = false;
                    grdLister.Columns["Address"].Width = 700;
                    grdLister.Columns["PostCode"].Width = 150;

                    grdLister.Columns["Zone"].Width = 150;

                    grdLister.Columns["PostCode"].HeaderText = "Post Code";
                    grdLister.Columns["Address"].ReadOnly = true;
                    grdLister.Columns["PostCode"].ReadOnly = true;
                    GridButton();


                    grdLister.Enabled = true;

                }
            }
            catch (Exception ex)
            {


            }
            
        }
        public void RefreshAddressList()
        {
            if (bWorker != null && bWorker.IsBusy == false)
            {
                ShowProgress(true);
                bWorker.RunWorkerAsync();

            }


        }
        delegate void UIProgress(bool show);
        private void ShowProgress(bool show)
        {
            // lblProgressBar.Visible = show;
            grdLister.Enabled = false;
            lblLoading.Visible = show;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UIProgress(DisplayProgress), show);
            }
            else
            {
                DisplayProgress(show);

            }
        }





        private void DisplayProgress(bool show)
        {
            lblLoading.Visible = show;
        }
        public override void RefreshData()
        {
            lblLoading.Visible = true;
           // grdLister.Enabled = false;
            if (bWorker.IsBusy == false)
            {
                grdLister.Enabled = false;
                bWorker.RunWorkerAsync(null);
            }
        }
        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            PopulateData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Find();
            //bWorker.RunWorkerAsync();
            //PopulateData();
        }
        private void ClearFilter()
        {
            lblLoading.Visible = true;
           // grdLister.Enabled = false;
            skip = 0;
            this.IsFind = false;
            this.txtSearch.Text = string.Empty;
            ddlColumns.Text = "Address";
            if (bWorker.IsBusy==false)
            {
                grdLister.Enabled = false;
                bWorker.RunWorkerAsync();
            }
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ClearFilter();
        }

        private void btnAddNewAddress_Click(object sender, EventArgs e)
        {
            frmAddNewAddress frm = new frmAddNewAddress();
            frm.Show();
        }
        private void btnNextRecord_Click(object sender, EventArgs e)
        {
            try
            {
                lblLoading.Visible = true;
 
                if (bWorker.IsBusy == false)
                {
                    grdLister.Enabled = false;
                    skip = skip + pageSize;

                    bWorker.RunWorkerAsync();
                }
               // grdLister.Enabled = true;
                //  PopulateData();

                btnNextRecord.Enabled = false;

                Thread.Sleep(1000);

                btnNextRecord.Enabled = true;
            }
            catch (Exception ex)
            {


            }
        }

        private void btnFirstRecords_Click(object sender, EventArgs e)
        {
            try
            {
                lblLoading.Visible = true;
               // grdLister.Enabled = false;
                if (bWorker.IsBusy == false)
                {
                    grdLister.Enabled = false;
                    skip = 0;

                    bWorker.RunWorkerAsync();
                }

               // grdLister.Enabled = true;
                btnFirstRecords.Enabled = false;

                Thread.Sleep(1000);

                btnFirstRecords.Enabled = true;
                //PopulateData();
            }
            catch (Exception ex)
            {


            }
        }

        private void btnPreviousRecords_Click(object sender, EventArgs e)
        {
            try
            {
                lblLoading.Visible = true;
               // grdLister.Enabled = false;
                if (bWorker.IsBusy == false)
                {
                    grdLister.Enabled = false;
                    if (skip - pageSize < 0)
                        skip = 0;
                    else
                        skip = skip - pageSize;

                    bWorker.RunWorkerAsync();
                }


                btnPreviousRecords.Enabled = false;

                Thread.Sleep(1000);

                btnPreviousRecords.Enabled = true;
              
            }
            catch (Exception ex)
            {


            }
        }

        private void btnLastRecords_Click(object sender, EventArgs e)
        {
            try
            {
                lblLoading.Visible = true;
               // grdLister.Enabled = false;
                if (bWorker.IsBusy == false)
                {
                    grdLister.Enabled = false;
                   // int cnt = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING).Count();
                    var list1 = General.GetQueryable<Gen_Address>(null).ToList();
                    int cnt = list1.Count();
                    if (cnt <= pageSize)
                    {
                        skip = 0;

                    }
                    else if (cnt > pageSize)
                    {

                        skip = cnt - pageSize;

                    }

                    bWorker.RunWorkerAsync();
                }
                btnLastRecords.Enabled = false;

                Thread.Sleep(1000);

                btnLastRecords.Enabled = true;

            }
            catch (Exception ex)
            {
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Find();
            }
        }
    }
}
