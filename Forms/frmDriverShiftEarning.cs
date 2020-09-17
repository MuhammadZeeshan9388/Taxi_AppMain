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
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmDriverShiftEarning : UI.SetupBase
    {
        public frmDriverShiftEarning()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDriverShiftEarning_Load);
        }

        void frmDriverShiftEarning_Load(object sender, EventArgs e)
        {
          
            rdoCurrent.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
        }
      
        public struct COLS
        {
            public static string DriverNo = "DriverNo";
            public static string DriverName = "DriverName";
            public static string Eanred = "Eanred";
            public static string JobsDone = "JobsDone";
            public static string AverageEarning = "AverageEarning";
        }
      
      

        private void rdoCurrent_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (rdoCurrent.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ShowDriverEarning(1,null,null);
            }
            else if(rdoToday.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ShowDriverEarning(2,DateTime.Now.ToDate(),DateTime.Now.ToDate());
            }
        }

        private void ShowDriverEarning(int earningType,DateTime? fromDateTime,DateTime? tillDate)
        {

            try
            {
                var list = new TaxiDataContext().stp_DriverShiftEarning(fromDateTime, tillDate, true, earningType);


                grdDriverShiftEarning.DataSource = list.ToList();



                if (grdDriverShiftEarning.Columns.Count > 0)
                {
                    grdDriverShiftEarning.Columns["DriverId"].IsVisible = false;
                    grdDriverShiftEarning.Columns[COLS.DriverNo].Width = 200;
                    grdDriverShiftEarning.Columns[COLS.DriverName].Width = 200;
                    grdDriverShiftEarning.Columns["Earned"].Width = 180;
                    grdDriverShiftEarning.Columns["AvgEarning"].Width = 180;
                    grdDriverShiftEarning.Columns[COLS.JobsDone].Width = 160;



                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }
    }
}
