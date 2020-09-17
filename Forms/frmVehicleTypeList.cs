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
 
     
    public partial class frmVehicleTypeList: UI.SetupBase
    {
         VehicleTypeBO objMaster;
        

        public frmVehicleTypeList()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmVehicleTypeList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
             grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
             objMaster = new VehicleTypeBO();
           
            this.SetProperties((INavigation)objMaster);
          //  grdLister.CellFormatting+=new CellFormattingEventHandler(grdLister_CellFormatting);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmVehicleTypeList_Shown);


            grdLister.ShowGroupPanel = false;
            grdLister.AllowAddNewRow = false;
            grdLister.ShowRowHeaderColumn = false;


            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);


          //  grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;

         //   grdLister.AutoCellFormatting = TR;
       //     grdLister.EnableHotTracking = false;
         //   grdLister.ViewCellFormatting += new CellFormattingEventHandler(MyGridView_ViewCellFormatting);
        }


         private void LoadVehicleTypeList()
         {

             var data1 =General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo);

             var query = from a in data1


                         select new
                         {
                             Id = a.Id,
                             Vehicle = a.VehicleType,
                             Passengers = a.NoofPassengers,
                             Luggages = a.NoofLuggages,
                             HandLuggages = a.NoofHandLuggages,
                             StartRate = a.StartRate,
                             StartRateUpToMiles = a.StartRateValidMiles

                         };



            // this.SetRefreshingProperties(AppVars.BLData.GetCommand(query), grdLister, this.CanDelete);

             grdLister.DataSource = query.ToList();


         }

         void frmVehicleTypeList_Shown(object sender, EventArgs e)
         {
             try
             {
                 this.InitializeForm("frmVehicleType");
             }
             catch
             {


             }
                 
             grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

             grdLister.AllowAutoSizeColumns = true;

             LoadVehicleTypeList();


             grdLister.AddEditColumn();

             if (this.CanDelete)
             {
                 grdLister.AddDeleteColumn();
                 grdLister.Columns["btnDelete"].Width = 80;

             }


             UI.GridFunctions.SetFilter(grdLister);

             grdLister.Columns["btnEdit"].Width = 80;
            

             grdLister.Columns["StartRateUpToMiles"].HeaderText = "No of Miles for Start Rate";

            // grdLister.Columns["ColDelete"].Width = 70;
             grdLister.Columns["Id"].IsVisible = false;

             //grdLister.Columns["Vehicle"].Width = 130;
             //grdLister.Columns["Passengers"].Width = 150;
             //grdLister.Columns["Email"].Width = 130;
             //grdLister.Columns["Phone"].Width = 130;
             //grdLister.Columns["MobileNo"].Width = 110;
         }


         void frmVehicleTypeList_Load(object sender, EventArgs e)
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
                 ShowVehicleForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
             }
             else
             {
                 ENUtils.ShowMessage("Please select a record");
             }
         }


         private void ShowVehicleForm(int id)
         {


             frmVehicleType frm = new frmVehicleType();
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

                 
                     objMaster = new VehicleTypeBO();

                     try
                     {

                         objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
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


         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             RadGridView grid = gridCell.GridControl;
             if (gridCell.ColumnInfo.Name == "btnDelete")
             {

                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Vehicle ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                 {
                     grid.CurrentRow.Delete();
                 }
             }
             else if (gridCell.ColumnInfo.Name == "ColEdit")
             {
                 ViewDetailForm();


             }
           
         }



       

        public override void RefreshData()
        {
            PopulateData();
        }

       

        public override void PopulateData()
        {

            LoadVehicleTypeList();
            
        }

        private void btnVehicleOrder_Click(object sender, EventArgs e)
        {
            frmVehicleOrder frmvehicle = new frmVehicleOrder();
            frmvehicle.Show();
        }

      




     

    }
}

