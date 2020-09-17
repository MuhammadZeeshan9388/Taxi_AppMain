using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using DAL;
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Telerik.WinControls;

namespace Taxi_AppMain.Forms
{
    public partial class frmExpiredDrivers : UI.SetupBase
    {
    
      

        public frmExpiredDrivers()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmExpiredDrivers_Load);
        }
        void frmExpiredDrivers_Load(object sender, EventArgs e)
        {
            LoadExpireDriverList();
        }

        private void frmExpiredDrivers_Shown(object sender, EventArgs e)
        {
            LoadExpireDriverList();
        


        }
        //Expir Drivers
        private void LoadExpireDriverList()
        {
            try
            {

                string searchTxt = txtSearch2.Text.ToStr().ToLower().Trim();
                string col = ddlColumns2.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;


                bool col_no = false;
                bool col_name = false;
                bool col_Vechno = false;
                if (col == "driver no")
                {
                    col_no = true;
                }

                if (col == "driver name")
                {
                    col_name = true;
                }
                if (col == "vehicle no")
                {
                    col_Vechno = true;
                }
                DateTime nowDate = DateTime.Now.ToDate();
                //For Expired Driver

                //var data1 = General.GetQueryable<Fleet_Driver>(c => c.MOTExpiryDate > nowDate || c.MOT2ExpiryDate > nowDate || c.DrivingLicenseExpiryDate > nowDate || c.PCODriverExpiryDate > nowDate || c.PCOVehicleExpiryDate > nowDate || c.InsuranceExpiryDate > nowDate).AsEnumerable().OrderBy(item => item.DriverNo);


                //For Fire Driver

                var data1 = General.GetQueryable<Fleet_Driver>(null).AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>());
                var data2 = General.GetQueryable<Fleet_Driver_Availability>(c => c.EndingDate != null);


                var query = (from a in data1
                             join b in data2 on a.Id equals b.DriverId

                             where
                             (col_no && (a.DriverNo.ToStr().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_name && (a.DriverName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            || (col_Vechno && (a.VehicleNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                             select new
                             {
                                 Id = a.Id,
                                 No = a.DriverNo,
                                 Name = a.DriverName,
                                 EndDate = string.Format("{0:dd/MM/yyyy}", b.EndingDate),
                                 VehicleNo = a.VehicleNo,
                                 MobileNo = a.MobileNo,
                                 MOTExpiry = a.MOTExpiryDate,
                                 MOT2Expiry = a.MOT2ExpiryDate,
                                 PCOVehicleExpiry = a.PCOVehicleExpiryDate,
                                 InsuranceExpiry = a.InsuranceExpiryDate,
                                 PCODriverExpiry = a.PCODriverExpiryDate,
                                 LicenseExpiry = a.DrivingLicenseExpiryDate,
                                 RoadTaxExpiry=a.RoadTaxiExpiryDate

                             }).AsQueryable();
                grdLister.DataSource = query.ToList();

                grdLister.CurrentRow = null;

                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["Name"].Width = 100;
                grdLister.Columns["VehicleNo"].Width = 60;
                grdLister.Columns["MOTExpiry"].Width = 140;
                grdLister.Columns["MOT2Expiry"].Width = 130;
                grdLister.Columns["MOT2Expiry"].IsVisible = true;
                grdLister.Columns["PCOVehicleExpiry"].Width = 140;
                grdLister.Columns["InsuranceExpiry"].Width = 120;
                grdLister.Columns["LicenseExpiry"].Width = 120;
                grdLister.Columns["PCODriverExpiry"].Width = 120;
                grdLister.Columns["RoadTaxExpiry"].Width = 130;


                grdLister.Columns["RoadTaxExpiry"].IsVisible = false;
                grdLister.Columns["MobileNo"].Width = 90;
                grdLister.Columns["EndDate"].Width = 80;

                grdLister.Columns["EndDate"].HeaderText = "End Date";
                grdLister.Columns["VehicleNo"].HeaderText = "Veh No";
                grdLister.Columns["MOTExpiry"].HeaderText = "MOT Expiry";
                grdLister.Columns["MOT2Expiry"].HeaderText = "MOT2 Expiry";
                grdLister.Columns["PCOVehicleExpiry"].HeaderText = "PCO Vehicle Expiry";
                grdLister.Columns["InsuranceExpiry"].HeaderText = "Insurance Expiry";
                grdLister.Columns["LicenseExpiry"].HeaderText = "License Expiry";
                grdLister.Columns["PCODriverExpiry"].HeaderText = "PCO Driver Expiry";
                grdLister.Columns["MobileNo"].HeaderText = "Mobile No";




                foreach (var doc in General.GetQueryable<Gen_Syspolicy_DriverDocumentList>(c=>c.IsVisible==false).ToList())
                {
                    if (doc.DocumentName.ToStr().ToLower() == "mot 2")
                        grdLister.Columns["MOT2Expiry"].IsVisible = false;

                    else if (doc.DocumentName.ToStr().ToLower() == "road tax")
                        grdLister.Columns["RoadTaxExpiry"].IsVisible = false;
                    
                } 
            }
            catch (Exception ex)
            {


            }

        }

        private void btnFind2_Click(object sender, EventArgs e)
        {
            LoadExpireDriverList();
            
        }
        private void btnShowAll2_Click(object sender, EventArgs e)
        {
            txtSearch2.Text = string.Empty;
            LoadExpireDriverList();
        }


    }
}
