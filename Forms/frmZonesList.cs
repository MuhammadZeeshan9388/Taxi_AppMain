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
using Taxi_AppMain.Classes;


namespace Taxi_AppMain
{
    public partial class frmZonesList  : UI.SetupBase
    {
         ZoneBO objMaster;



         public frmZonesList()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmLocationList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
             grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
             objMaster = new ZoneBO();
           
            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmLocationList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdLister.CellFormatting += new CellFormattingEventHandler(grdLister_CellFormatting);

        }

         Font f = new Font("Tahoma", 10, FontStyle.Bold);

         void grdLister_CellFormatting(object sender, CellFormattingEventArgs e)
         {
             try
             {

                 if (e.Column != null && e.Row != null && e.Row.Cells["Id"].Value != null)
                 {
                     if (e.Column.Name == "ZoneName" && e.Row.Cells["IsBase"].Value.ToBool())
                     {
                         e.CellElement.Font = f;
                         e.CellElement.RowElement.BackColor = Color.LightYellow;
                         e.CellElement.RowElement.NumberOfColors = 1;

                     }
                 }
             }
             catch
             {


             }

         }

         void frmLocationList_Shown(object sender, EventArgs e)
         {

             grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


             try
             {

                 this.InitializeForm("frmZones");

             }
             catch
             {


             }
           
             LoadZonesList();

            grdLister.AddEditColumn();

             if (this.CanDelete)
             {
                 grdLister.AddDeleteColumn();
                 grdLister.Columns["btnDelete"].Width = 70;
             }


             grdLister.Columns["Id"].IsVisible = false;
             grdLister.Columns["IsBase"].IsVisible = false;

             grdLister.Columns["ZoneName"].HeaderText = "Zone Name";
             grdLister.Columns["ZoneName"].Width = 200;

             grdLister.Columns["PostCodes"].HeaderText = "Post Code(s)";

             grdLister.Columns["PostCodes"].Width = 500;

             grdLister.Columns["btnEdit"].Width = 70;

             grdLister.Columns["Sno"].Width = 40;


             UI.GridFunctions.SetFilter(grdLister);


           

         }

         private void LoadZonesList()
         {





             var data1 = General.GetQueryable<Gen_Zone>(c=>c.ZoneName!="SIN BIN" && c.OrderNo!=null && c.ZoneName!="OnBreak").OrderBy(c => c.OrderNo).AsEnumerable();
           

             var query = from a in data1                   

                      

                         select new
                         {
                             Id = a.Id,
                             Sno=a.OrderNo,
                             ZoneName = a.ZoneName,
                             PostCodes = a.PostCode,
                             IsBase=a.IsBase
                           
                         };


             grdLister.DataSource = query.ToList();
            // this.SetRefreshingProperties(AppVars.BLData.GetCommand(query), grdLister, false);

            

         }

         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
             {



                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Zone ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
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
                 ShowZoneForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
             }
             else
             {
                 ENUtils.ShowMessage("Please select a record");
             }
         }


         private void ShowZoneForm(int id)
         {


             frmZones frm = new frmZones();
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

                     objMaster = new ZoneBO();

                     try
                     {

                         objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                        
                         if (objMaster.Current != null)
                         {
                             int id = objMaster.Current.Id;
                             string zoneName = objMaster.Current.ZoneName;
                             objMaster.Delete(objMaster.Current);

                             //using (PDADataContext db = new PDADataContext())
                             //{

                             //    Zone objZone = db.Zones.FirstOrDefault(c => c.ZoneName == zoneName && c.ClientId==AppVars.objPolicyConfiguration.DefaultClientId);

                             //    if (objZone != null)
                             //    {
                             //        db.Zones.DeleteOnSubmit(objZone);
                             //        db.SubmitChanges();
                             //    }
                             //}
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



        

       

        public override void RefreshData()
        {
            PopulateData();
        }

       

        public override void PopulateData()
        {

            LoadZonesList();

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnAddZone_Click(object sender, EventArgs e)
        {
            ShowZoneForm();
        }

        private void ShowZoneForm()
        {
            frmZones frm = new frmZones();
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.MaximizeBox = false;
            frm.ControlBox = true;
            frm.ShowIcon = false;
            frm.ShowDialog();
        }

        private void btnDrawPlot_Click(object sender, EventArgs e)
        {
          //  General.ShowDrawZoneForm(0);
        }

        private void btnDrawEditZone_Click(object sender, EventArgs e)
        {
            //if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            //{

            //    General.ShowDrawZoneForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            //}
        }


     

    }
}

