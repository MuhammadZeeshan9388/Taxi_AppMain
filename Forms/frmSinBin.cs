using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using System.Collections;

namespace Taxi_AppMain
{
    public partial class frmSinBin : UI.SetupBase
    {


        public frmSinBin()
        {
            InitializeComponent();

            InitializeSettings();

        }


            public frmSinBin(int driverId)
        {
            InitializeComponent();

            InitializeSettings();
            ddl_Driver.SelectedValue = driverId;

        }


        private void InitializeSettings()
        {

       


            ComboFunctions.FillSinBinDriversCombo(ddl_Driver);
            this.Shown += new EventHandler(frmSinBin_Shown);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmSinBin_KeyDown);

        }

        void frmSinBin_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.End)
            {
                Save();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }



        void frmSinBin_Shown(object sender, EventArgs e)
        {

            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            if (ddl_Driver.SelectedItem == null)
            {
                OptUnBlocked.Checked = true;

            }
            else
            {
                optBlocked.Checked = true;
                numBlocked.Visible = true;
                lblMin.Visible = true;
            }

        }

  

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                int? driverId = ddl_Driver.SelectedValue.ToIntorNull();


                if (driverId == null)
                {
                    ENUtils.ShowMessage("Required: Driver");

                    return;

                }

                if (OptUnBlocked.Checked == true && driverId != null)
                {
                    new TaxiDataContext().stp_UnBlockDriver(driverId, AppVars.LoginObj.UserName.ToStr());
                    AppVars.frmMDI.RefreshRequiredDashBoard();
                    this.Close();
                }
                else if (optBlocked.Checked == true && driverId != null && numBlocked.Value > 0)
                {

                    if (numBlocked.Value == 0)
                    {
                        ENUtils.ShowMessage("Required : SIN BIN minutes");

                        return;
                    }

                    new TaxiDataContext().stp_BlockDriver(driverId, numBlocked.Value.ToInt());
                    AppVars.frmMDI.RefreshRequiredDashBoard();
                    this.Close();

                }

              
            }
            catch (Exception ex)
            {


            }
        }

        private void ddl_Driver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                var cnt = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true
                                                                        && c.DriverId == ddl_Driver.SelectedValue.ToInt()   
                                                                        && c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.SINBIN).Count();
                if (cnt>0)
                {
                    OptUnBlocked.Checked = true;
                    optBlocked.Checked = false;
                    numBlocked.Visible = false;
                    lblMin.Visible = false;


                }
                else
                {
                    optBlocked.Checked = true;
                    numBlocked.Visible = true;
                    lblMin.Visible = true;
                }
            }
            catch (Exception ex)
            {


            }


        }

        private void optBlocked_CheckedChanged(object sender, EventArgs e)
        {
            if (optBlocked.Checked == true)
            {
                numBlocked.Visible = true;
                lblMin.Visible = true;

            }
            else
            {
                numBlocked.Visible = false;
                lblMin.Visible = false;
            }




        }

        public DateTime SinBinAddMin;
    













    }
}
