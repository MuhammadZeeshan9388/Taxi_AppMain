using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Utils;



using Taxi_Model;

namespace Taxi_AppMain
{
    public partial class frmSearchDriver : Form
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void bringToFront(string title)
        {
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }

        public frmSearchDriver()
        {
            InitializeComponent();
            LoadData();
        }

        public frmSearchDriver(int driverId)
        {
            InitializeComponent();
            LoadData();


            if (driverId > 0)
                ddl_Driver.SelectedValue = driverId;
        }

        private void LoadData()
        {
            try
            {
                ComboFunctions.FillDriverNoComboSorted(ddl_Driver);
                ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
                ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlDriver_SelectedIndexChanged);
              
                this.Shown += new EventHandler(frmSearchDriver_Shown);
            }
            catch
            {

            }

        }

        void frmSearchDriver_Shown(object sender, EventArgs e)
        {
        
            ddl_Driver.DropDownListElement.Focus();
          

            btnCallDriver.Visible= AppVars.listUserRights.Count(c => c.functionId.ToUpper() == "CLICK TO CALL") > 0;
        }


        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            SearchDriver(ddl_Driver.SelectedValue.ToInt());




        }

        void ddl_Driver_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                SearchDriver(ddl_Driver.SelectedValue.ToInt());
            }
        }

      

     
      

        private void frmTrackDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
          
        }



       


        private void SearchDriver(int driverId)
        {
            if (driverId == 0)
                return;

            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var obj =db.Fleet_Drivers.FirstOrDefault(c => c.Id == driverId);

                    if (obj != null)
                    {

                        txtName.Text = obj.DriverName.ToStr();
                        txtEmail.Text = obj.Email.ToStr();
                        txtMobileNo.Text = obj.MobileNo.ToStr();
                        txtTelNo.Text = obj.TelephoneNo.ToStr();
                        txtVehicle.Text = obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr() + " - " + obj.VehicleColor.ToStr() + " - " + obj.VehicleNo.ToStr();
                        txtMakeModel.Text = obj.VehicleMake.ToStr().Trim() + "/" + obj.VehicleModel.ToStr().Trim();
                        txtInitialBalance.Text = obj.InitialBalance.ToStr().Trim();
                        chkRentPaid.Checked = obj.PDALoginBlocked.ToBool();
                        if (obj.HasPDA.ToBool())
                        {
                            txtPDAVersion.Enabled = true;
                            string PDAVersion = obj.Fleet_Driver_PDASettings.Count > 0 ? obj.Fleet_Driver_PDASettings.FirstOrDefault().CurrentPdaVersion.ToStr() : null;
                            string LastUpdated = obj.Fleet_Driver_PDASettings.Count > 0 ? string.Format("{0:dd/MM/yyyy HH:mm}", obj.Fleet_Driver_PDASettings.FirstOrDefault().LastVersionUpdatedOn) : null;
                            txtPDAVersion.Text = PDAVersion + " ( Updated On : " + LastUpdated + " )";
                        }
                        else
                        {
                            txtPDAVersion.Text = string.Empty;
                            txtPDAVersion.Enabled = false;
                        }



                        if(db.Fleet_DriverQueueLists.Where(c=>c.DriverId==driverId && c.Status == true).Count()>0)
                        {
                            txtStatus.Text = "ONLINE";
                            txtStatus.ForeColor = Color.Green;

                        }
                        else
                        {
                            txtStatus.Text = "OFFLINE";
                            txtStatus.ForeColor = Color.Gray;

                        }


                    }
                }
            }
            catch
            {


            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int driverId = ddl_Driver.SelectedValue.ToInt();
            if (driverId > 0)
            {
                try
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.Fleet_Drivers.FirstOrDefault(c => c.Id == driverId).PDALoginBlocked = chkRentPaid.Checked;
                        db.SubmitChanges();
                    }
                }
                catch
                {


                }

            }
        }

        private void btnCallDriver_Click(object sender, EventArgs e)
        {
           
            General.ClickACallDriver(ddl_Driver.SelectedValue.ToInt(),txtMobileNo.Text);

        }
    }
}
