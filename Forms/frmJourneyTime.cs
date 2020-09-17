using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_BLL;
using DAL;
using Utils;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using Telerik.WinControls;
using UI;

namespace Taxi_AppMain
{
    public partial class frmJourneyTime : UI.SetupBase
    {
        FareBO objMaster = null;
        public frmJourneyTime()
        {
            InitializeComponent();
            
            objMaster = new FareBO();
            FormatGrid();
            this.SetProperties((INavigation)objMaster);
            this.Load += new EventHandler(frmJourneyTime_Load);
            this.KeyDown += new KeyEventHandler(frmJourneyTime_KeyDown);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string CompanyId = "CompanyId";
            public static string SubCompanyId = "SubCompanyId";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string Company = "Company";
            public static string SubCompanyName = "SubCompanyName";
            public static string VehicleType = "VehicleType";
            public static string PerMinJourneyCharges = "PerMinJourneyCharges";
      
        }
        private void FormatGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.CompanyId;
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.SubCompanyId;
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.VehicleTypeId;
            col.IsVisible = false;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.VehicleType;
            col.HeaderText = "Vehicle Type";
            col.ReadOnly = true;
            col.Width = 200;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.Company;
            col.HeaderText = COLS.Company;
            col.ReadOnly = true;
            col.Width = 140;
            grdLister.Columns.Add(col);





            col = new GridViewTextBoxColumn();
            col.Name = COLS.SubCompanyName;
            col.HeaderText = "Sub Company Name";
            col.ReadOnly = true;
            col.Width = 180;
            grdLister.Columns.Add(col);

        

            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = false;
            colD.HeaderText = "Charges (per min)";
            colD.Name = COLS.PerMinJourneyCharges; //"PerMinJourneyCharges";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.Width = 150;
            grdLister.Columns.Add(colD);

            grdLister.EnableFiltering = true;
            grdLister.ShowFilteringRow = true;
        }
        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        public override void Save()
        {
            try
            {
                int Id=0;
                foreach (var item in grdLister.Rows)
                {
                    Id = item.Cells["Id"].Value.ToInt();
                    if (Id > 0)
                    {
                        objMaster.GetByPrimaryKey(Id);
                        objMaster.Edit();
                        objMaster.Current.PerMinJourneyCharges = item.Cells[COLS.PerMinJourneyCharges].Value.ToDecimal();
                        objMaster.CheckDataValidation = false;
                        objMaster.Save();
                        objMaster.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }

        void frmJourneyTime_Load(object sender, EventArgs e)
        {

            grdLister.ShowRowHeaderColumn = false;
            grdLister.AutoCellFormatting = true;
            grdLister.EnableHotTracking = false;
            grdLister.ShowGroupPanel = false;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            PopulateData();
        }

        void frmJourneyTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        public override void PopulateData()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = (from a in db.Fares
                                join sub in db.Gen_SubCompanies on a.SubCompanyId equals sub.Id into table2
                                from sub in table2.DefaultIfEmpty()
                                select new
                                {
                                    Id = a.Id,
                                    CompanyId = a.CompanyId != null ? a.CompanyId : null,
                                    SubCompanyId = a.SubCompanyId != null ? a.SubCompanyId : null,
                                    VehicleTypeId = a.VehicleTypeId != null ? a.VehicleTypeId : null,
                                    Company = a.CompanyId != null ? a.Gen_Company.CompanyName : null,
                                    SubCompanyName = sub != null ? sub.CompanyName : "",
                                    VehicleType = a.VehicleTypeId != null ? a.Fleet_VehicleType.VehicleType : null,
                                    PerMinJourneyCharges = a.PerMinJourneyCharges
                                }).ToList();

                    grdLister.RowCount = list.Count;
                    grdLister.BeginUpdate();
                    for (int i = 0; i < list.Count; i++)
                    {
                        grdLister.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdLister.Rows[i].Cells[COLS.CompanyId].Value = list[i].CompanyId;
                        grdLister.Rows[i].Cells[COLS.SubCompanyId].Value = list[i].SubCompanyId;
                        grdLister.Rows[i].Cells[COLS.VehicleTypeId].Value = list[i].VehicleTypeId;
                        grdLister.Rows[i].Cells[COLS.Company].Value = list[i].Company;
                        grdLister.Rows[i].Cells[COLS.SubCompanyName].Value = list[i].SubCompanyName;
                        grdLister.Rows[i].Cells[COLS.VehicleType].Value = list[i].VehicleType;
                        grdLister.Rows[i].Cells[COLS.PerMinJourneyCharges].Value = list[i].PerMinJourneyCharges;
                    }
                    grdLister.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
       
    }
}
