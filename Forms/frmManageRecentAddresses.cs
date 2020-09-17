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
    public partial class frmManageRecentAddresses : UI.SetupBase
    {
        public frmManageRecentAddresses()
        {
            InitializeComponent();
            FormatGrid();
            this.Load += new EventHandler(frmManageRecentAddresses_Load);
        }

        void frmManageRecentAddresses_Load(object sender, EventArgs e)
        {
            LoadRecentAddress();    
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string AddressLine1 = "AddressLine1";
            public static string Address = "Address";
        }
        public void FormatGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.AddressLine1;
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Address;
            col.HeaderText = COLS.Address;
            col.Width = 350;
            grdLister.Columns.Add(col);
            
            grdLister.AddDeleteColumn();
            grdLister.Columns["btnDelete"].Width = 80;

            this.grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);

            this.grdLister.ShowFilteringRow = true;
            this.grdLister.EnableFiltering = true;
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            try
            {

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;


                if (gridCell.ColumnInfo.Name == "btnDelete")
                {
                    GridViewRowInfo row = grdLister.CurrentRow;
                    string AddressName = row.Cells[COLS.Address].Value.ToStr();
                    string AddressLine1 = row.Cells[COLS.AddressLine1].Value.ToStr();
                    long Id = row.Cells[COLS.Id].Value.ToLong();
                    var query = General.GetObject<Gen_RecentAddress>(c => (c.Id == Id) && (c.AddressLine1.Contains(AddressName)));
                    if (query != null)
                    {
                        string[] result = query.AddressLine1.Split(new string[] { "</add>" }, StringSplitOptions.RemoveEmptyEntries);
                        if (result.Count() > 1)
                        {
                            AddressLine1 = AddressLine1.Replace(AddressName,"");
                            AddressLine1 = AddressLine1.Replace("<add></add>","");
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                if (!string.IsNullOrEmpty(AddressLine1))
                                {
                                    db.stp_RunProcedure("update Gen_RecentAddresses set AddressLine1='" + AddressLine1 + "' where Id=" + Id + "");
                                }
                                else
                                {
                                    db.stp_RunProcedure("delete  from Gen_RecentAddresses where Id=" + Id + "");
                                }
                            }
                        }
                        else
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                db.stp_RunProcedure("delete  from Gen_RecentAddresses where Id=" + Id + "");
                            }
                        }

                        row.Delete();
                     //   LoadRecentAddress();
                    //query.AddressLine1.Contains("").cou
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void LoadRecentAddress()
        {
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list = db.Gen_RecentAddresses.Where(c => c.AddressLine1 != "").ToList();

                    List<Address> objAddress = new List<Address>();
                    foreach (var item in list.OrderByDescending(a => a.SearchedOn))
                    {
                        string add = "";
                        string[] result = item.AddressLine1.Split(new string[] { "</add>" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in result)
                        {
                            add = s;
                            add = add.Replace("<add>", "").ToStr();
                            objAddress.Add(new Address { AddressLine1 = item.AddressLine1, AddressName = add, Id = item.Id });
                        }
                    }
                    grdLister.RowCount = objAddress.Count;
                    for (int i = 0; i < objAddress.Count; i++)
                    {
                        grdLister.Rows[i].Cells[COLS.Id].Value = objAddress[i].Id;
                        grdLister.Rows[i].Cells[COLS.Address].Value = objAddress[i].AddressName;
                        grdLister.Rows[i].Cells[COLS.AddressLine1].Value = objAddress[i].AddressLine1;
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnClearAddresses_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("are you sure you want to clear all recent addresses ?", "", MessageBoxButtons.YesNo))
            {
                try
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_RunProcedure("delete  from Gen_RecentAddresses");
                    }

                    LoadRecentAddress();
                }
                catch
                {


                }
            }

        }

    }
    class Address
    {
       public long Id { get; set; }
       public string AddressName { get; set; }
       public string AddressLine1 { get; set; }

    }
}
