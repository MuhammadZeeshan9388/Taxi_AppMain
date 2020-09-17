using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_AppMain.Forms;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmDrvStreetView : BaseForm
    {
        public frmDrvStreetView()
        {
            InitializeComponent();
            ComboFunctions.FillPDADLoginriverIdCombo(ddl_Driver);
           
            ddl_Driver.KeyUp+=new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown+=new KeyEventHandler(ddl_Driver_KeyDown);
        }


        void ddl_Driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                ShowView();
            }
        }

        void ddl_Driver_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                ShowView();
            }
        }

       

        private void btnTrack_Click(object sender, EventArgs e)
        {

            ShowView();

        }


        private void ShowView()
        {
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();

                if (driverId != 0)
                {
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).ShowStreetView(driverId);


                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
    }
}
