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

namespace Taxi_AppMain
{
    public partial class frmLocationList : UI.SetupBase
    {
         LocationBO objMaster;




         public frmLocationList()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmLocationList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
             grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
             objMaster = new LocationBO();
           
            this.SetProperties((INavigation)objMaster);
         //   grdLister.CellFormatting+=new CellFormattingEventHandler(grdLister_CellFormatting);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmLocationList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
        //    grdLister.EnableFastScrolling = true;

        }

         void frmLocationList_Shown(object sender, EventArgs e)
         {

             grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

             ddlColumns.Text = "location name";

             try
             {
                 this.InitializeForm("frmLocations");

             }
             catch
             {


             }
           
             LoadLocationsList();

            grdLister.AddEditColumn();

             if (this.CanDelete)
             {
                 grdLister.AddDeleteColumn();
                 grdLister.Columns["btnDelete"].Width = 70;
             }


             grdLister.Columns["Id"].IsVisible = false;

             grdLister.Columns["LocationName"].HeaderText = "Location Name";
             grdLister.Columns["LocationName"].Width = 230;
             grdLister.Columns["LocationType"].HeaderText = "Location Type";
             grdLister.Columns["LocationType"].Width = 100;
             grdLister.Columns["Address"].Width = 370;
             grdLister.Columns["PostCode"].Width = 100;
             grdLister.Columns["ShortCutKey"].Width = 80;

             grdLister.Columns["btnEdit"].Width = 70;
         

             ddlColumns.Items.Add("Location Name");
             ddlColumns.Items.Add("Location Type");
             ddlColumns.Items.Add("Address");
             ddlColumns.Items.Add("Post Code");

            // ddlColumns.Items.AddRange(grdLister.Columns.Where(c => c.Name != "Id" && c.Name != "btnEdit" && c.Name != "btnDelete").Select(c => c.Name));
             ddlColumns.SelectedIndex = 0;


             UI.GridFunctions.SetFilter(grdLister);
           
             txtSearch.Focus();



             btnClearAddresses.Visible = AppVars.objPolicyConfiguration.RecentAddressesFrequency.ToInt() > 0;
         }

         private void LoadLocationsList()
         {

             string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
             string col = ddlColumns.Text.ToStr().Trim().ToLower();

             if (searchTxt.Length < 3)
                 searchTxt = string.Empty;


             bool col_name = false;
           
             bool col_postCode = false;
             bool col_locType = false;
             bool col_address = false;

             if (col == "location name")
             {
                 col_name = true;
             }
            
             else if (col == "location type")
             {
                 col_locType = true;
             }

             else if (col == "post code")
             {
                 col_postCode = true;
             }

             else if (col == "address")
             {
                 col_address = true;
             }




             var data1 =General.GetQueryable<Gen_Location>(null).OrderBy(c => c.LocationName).AsEnumerable();
             var data2 = General.GetQueryable<Gen_LocationType>(null).AsEnumerable();
          //   var data3 = AppVars.BLData.GetAll<Gen_Zone>();

             var query = from a in data1
                         join b in data2 on a.LocationTypeId equals b.Id

                         where
                        (col_name && (a.LocationName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                        ||   (col_locType && (b.LocationType.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                           
                        || (col_postCode && (a.PostCode.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                     
                        || (col_address &&  (a.Address!=null && a.Address.ToLower().Contains(searchTxt) || searchTxt == string.Empty))

                         select new
                         {
                             Id = a.Id,
                             LocationName = a.LocationName,
                             LocationType = b.LocationType,
                             Address = a.Address,
                             PostCode = a.PostCode,
                             //Zone =c.ZoneName
                             a.ShortCutKey
                         };


             grdLister.DataSource = query.ToList();
            // this.SetRefreshingProperties(AppVars.BLData.GetCommand(query), grdLister, false);

        //     grdLister.Columns["ColDelete"].Width = 70;


         }

         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
             {



                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Location ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                 {


                     RadGridView grid = gridCell.GridControl;
                     grid.CurrentRow.Delete();
                 }
             }
             else if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
             {
                 ViewDetailForm();


             }
         }



         void frmLocationList_Load(object sender, EventArgs e)
         {
         }

         void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
         {
             ViewDetailForm();
         }

         private void ViewDetailForm()
         {

             if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
             {
                 ShowLocationForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
             }
             else
             {
                 ENUtils.ShowMessage("Please select a record");
             }
         }


         private void ShowLocationForm(int id)
         {


             frmLocations frm = new frmLocations();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();     

         }


         void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
         {
             if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
             {

                     objMaster = new LocationBO();

                     try
                     {

                         objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                         if(objMaster.Current!=null)
                             objMaster.Delete(objMaster.Current);


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



        

       

        public override void RefreshData()
        {
            PopulateData();
        }

       

        public override void PopulateData()
        {

            LoadLocationsList();

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadLocationsList();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PopulateData();

            }
        }

        private void btnClearAddresses_Click(object sender, EventArgs e)
        {
            frmManageRecentAddresses frm = new frmManageRecentAddresses();
            frm.ShowDialog();
            frm.Dispose();
            //try
            //{
            //    using (TaxiDataContext db = new TaxiDataContext())
            //    {
            //        db.stp_RunProcedure("delete  from Gen_RecentAddresses");
            //    }

            //    MessageBox.Show("Recent addresses removed successfully");
            //}
            //catch
            //{


            //}

        }

        private void grdLister_Click(object sender, EventArgs e)
        {

        }


     

    }
}

