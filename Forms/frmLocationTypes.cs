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
namespace Taxi_AppMain
{
    public partial class frmLocationTypes : UI.SetupBase
    {
        public struct COLS
        {
            public static string Id = "Id";
            public static string LocationType = "LocationType";
            public static string ShortCutKey = "ShortCutKey";
            public static string ShortKey = "ShortKey";
        }
        public frmLocationTypes()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(frmLocationTypes_FormClosed);
        }

        void frmLocationTypes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        public void LoadLocations()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdLocationTypes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = "LocationType";
            col.ReadOnly = true;
            col.HeaderText = "Location Type";
            grdLocationTypes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = "ShortCutKey";
            col.ReadOnly = false;
            col.HeaderText = "Short Cut Key";
            grdLocationTypes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = "ShortKey";
            col.ReadOnly = true;
            col.HeaderText = "ShortKey";
            grdLocationTypes.Columns.Add(col);


          //  grdLocationTypes.CellEndEdit += new GridViewCellEventHandler(grdLocationTypes_CellEndEdit);
            grdLocationTypes.CellValidating += new CellValidatingEventHandler(grdLocationTypes_CellValidating);
            
            var data1 = General.GetQueryable<Gen_LocationType>(c=>c.Id!=Enums.LOCATION_TYPES.ADDRESS && c.Id!=Enums.LOCATION_TYPES.POSTCODE);// (c => c.CustomerMobileNo != null && c.CustomerPhoneNo != null);
            var query = (from a in data1
                         select new
                         {
                             Id = a.Id,
                             LocationType = a.LocationType,
                             ShortCutKey = a.ShortCutKey,
                         }).ToList();
            grdLocationTypes.RowCount = query.Count;
            for (int i = 0; i < query.Count; i++)
            {
                grdLocationTypes.Rows[i].Cells[COLS.Id].Value = query[i].Id;
                grdLocationTypes.Rows[i].Cells[COLS.LocationType].Value = query[i].LocationType;
                grdLocationTypes.Rows[i].Cells[COLS.ShortCutKey].Value = query[i].ShortCutKey;
                grdLocationTypes.Rows[i].Cells[COLS.ShortKey].Value = query[i].ShortCutKey;
            }
                           

            grdLocationTypes.Columns["LocationType"].HeaderText = "Location Type";
            grdLocationTypes.Columns["ShortCutKey"].HeaderText = "Short Cut Key";
            grdLocationTypes.Columns["LocationType"].Width = 200;

            grdLocationTypes.Columns["ShortCutKey"].Width = 157;
            grdLocationTypes.Columns["ShortKey"].IsVisible = false;
            grdLocationTypes.Columns["Id"].IsVisible = false;

         
        }

        void grdLocationTypes_CellValidating(object sender, CellValidatingEventArgs e)
        {

            if (e.Row!=null )
            {

                if(e.Value.ToStr().Length > 3)
                {
                    e.Row.ErrorText = "ShortKey Length cannot exceed 3 Characters";
                    e.Cancel = true;
                }
                else
                e.Row.ErrorText="";
            }
           



        }

        

        private void frmLocationTypes_Load(object sender, EventArgs e)
        {
            LoadLocations();
        }

        private void btnSaveLocationType_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdLocationTypes.Refresh();
                int Id = grdLocationTypes.CurrentRow.Cells["Id"].Value.ToInt();
                string OldKey = grdLocationTypes.CurrentRow.Cells["ShortKey"].Value.ToStr();
                string NewKey = grdLocationTypes.CurrentRow.Cells["ShortCutKey"].Value.ToStr();
                 using( Taxi_Model.TaxiDataContext objTaxiDataContext = new TaxiDataContext())
                {
                    objTaxiDataContext.stp_UpdateShortCutKey(Id, OldKey, NewKey);
                }
            }
            catch (Exception ex)
            {


            }

            ENUtils.ShowMessage("Changes takes affect after restarting Despatch System");
            
        }

        private void btnSaveCloseLocationType_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdLocationTypes.Refresh();


                foreach (var item in grdLocationTypes.Rows)
                {
                    int Id = item.Cells["Id"].Value.ToInt();
                    string OldKey = item.Cells["ShortKey"].Value.ToStr();
                    string NewKey = item.Cells["ShortCutKey"].Value.ToStr();
                    using (Taxi_Model.TaxiDataContext objTaxiDataContext = new TaxiDataContext())
                    {
                        objTaxiDataContext.stp_UpdateShortCutKey(Id, OldKey, NewKey);

                    }
                }
               

                //int Id = grdLocationTypes.CurrentRow.Cells["Id"].Value.ToInt();
                //string OldKey = grdLocationTypes.CurrentRow.Cells["ShortKey"].Value.ToStr();
                //string NewKey = grdLocationTypes.CurrentRow.Cells["ShortCutKey"].Value.ToStr();
                //using (Taxi_Model.TaxiDataContext objTaxiDataContext = new TaxiDataContext())
                //{
                //    objTaxiDataContext.stp_UpdateShortCutKey(Id, OldKey, NewKey);

                //}
              
                this.Close();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
    }
}
