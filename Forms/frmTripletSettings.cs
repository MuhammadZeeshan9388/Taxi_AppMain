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
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmTripletSettings : UI.SetupBase
    {

        public frmTripletSettings()
        {
            InitializeComponent();
            InitializeConstructor();
        }

     

        private void InitializeConstructor()
        {


            this.Load += new EventHandler(frmFareIncrement_Load);
            this.FormClosed += new FormClosedEventHandler(frmLocations_FormClosed);

          //  this.ddlIncrementType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlIncrementType_SelectedIndexChanged);
         
        }

        void ddlIncrementType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            //if (ddlIncrementType.Text.ToStr().ToLower() == "percent")
            //{
            //    numJobPrice.Maximum = 100;

            //}
            //else
            //{

            //    numJobPrice.Maximum = 10000;
            //}
        }

        void frmFareIncrement_Load(object sender, EventArgs e)
        {
            DisplaySettings();
        }

        private void DisplaySettings()
        {
            var objPolicy = General.GetObject<Gen_SysPolicy_Configuration>(c => c.Id != 0);


            int autoDespatchType = objPolicy.AutoDespatchType.ToInt();

            if (autoDespatchType == 1)
                optAutoDespRule1.ToggleState = ToggleState.On;

            else if (autoDespatchType == 2)
                optAutoDespRule2.ToggleState = ToggleState.On;

            else if (autoDespatchType == 3)
                optAutoDespRule3.ToggleState = ToggleState.On;

            else if (autoDespatchType == 4)
                optAutoDespRule4.ToggleState = ToggleState.On;

            else if (autoDespatchType == 5)
                optAutoDespRule5.ToggleState = ToggleState.On;



            numAutoDespDrvRadius.Value = objPolicy.AutoDespatchNearestDrvRadius.ToDecimal();
            numAutoDespLongestWaitingMins.Value = objPolicy.AutoDespatchLongestWaitingMins.ToDecimal();
            chkAccJobs.Checked = objPolicy.AutoDespatchPriorityForAccJobs.ToBool();
            chkPriorityForAllocDrvs.Checked = objPolicy.AutoDespatchPriorityForAllocatedDrv.ToBool();

            var obj = General.GetObject<TripletSetting>(c => c.Id != 0);

            if (obj != null)
            {
                chkEnableIncrement.Checked = obj.ShiftType.ToInt()==1?true:false;
             //   dtpFromDate.Value = obj.FromDate;
               // dtpTillDate.Value = obj.TillDate;

               // ddlIncrementType.Text = obj.IncrementType.ToStr().Trim().ToProperCase();
                numJobPrice.Value = obj.JobPrice.ToDecimal();



                //if (obj.CriteriaBy.ToInt() == 1)
                //{
                //    optDateTimeWise.ToggleState = ToggleState.On;
                //}
                //else if (obj.CriteriaBy.ToInt() == 2)
                //{
                //    optDateWise.ToggleState = ToggleState.On;
                //}
                //else if (obj.CriteriaBy.ToInt() == 3)
                //{
                //    optTimeWise.ToggleState = ToggleState.On;
                //}

            }

        }

        void frmLocations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
        }
        
      

      
     

      

        



  
     
        void frmLocations_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveSettings())
            {
                Close();

            }
        }

        private bool SaveSettings()
        {
            bool rtn = false;

            try
            {
                


                using (TaxiDataContext db = new TaxiDataContext())
                {


                   var obj=  db.TripletSettings.FirstOrDefault();

                   if (obj == null)
                   {

                       obj = new TripletSetting();
                   }


                   obj.JobPrice = numJobPrice.Value.ToDecimal();
                   obj.ShiftType = chkEnableIncrement.Checked ? 1 : 0;


                   if (obj.Id == 0)
                   {
                       db.TripletSettings.InsertOnSubmit(obj);

                   }


                   db.SubmitChanges();


                   var objPolicy = db.Gen_SysPolicy_Configurations.FirstOrDefault();

                   if (optAutoDespRule1.ToggleState == ToggleState.On)
                       objPolicy.AutoDespatchType = 1;

                   else if (optAutoDespRule2.ToggleState == ToggleState.On)
                       objPolicy.AutoDespatchType = 2;

                   else if (optAutoDespRule3.ToggleState == ToggleState.On)
                       objPolicy.AutoDespatchType = 3;

                   else if (optAutoDespRule4.ToggleState == ToggleState.On)
                       objPolicy.AutoDespatchType = 4;

                   else if (optAutoDespRule5.ToggleState == ToggleState.On)
                       objPolicy.AutoDespatchType = 5;



                   objPolicy.AutoDespatchNearestDrvRadius = numAutoDespDrvRadius.Value.ToDecimal();
                   objPolicy.AutoDespatchLongestWaitingMins = numAutoDespLongestWaitingMins.Value.ToInt();
                   objPolicy.AutoDespatchPriorityForAccJobs = chkAccJobs.Checked;

                   objPolicy.AutoDespatchPriorityForAllocatedDrv = chkPriorityForAllocDrvs.Checked;

                   db.SubmitChanges();

                }

                General.LoadConfiguration();


                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).SetAutoDespatchMode(AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool(), false);


              //  (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).GetTripletObject();

                rtn = true;
            }
            catch (Exception ex)
            {
                
                ENUtils.ShowMessage(ex.Message);
            }


            return rtn;

        }

        private void radRadioButton1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            //if (args.ToggleState == ToggleState.On)
            //{
            //    dtpFromDate.CustomFormat = "dd/MM/yyyy HH:mm";
            //    dtpTillDate.CustomFormat = "dd/MM/yyyy HH:mm";

            //    dtpFromDate.ShowUpDown = false;
            //    dtpTillDate.ShowUpDown = false;
            //}

        }

        private void optDateWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == ToggleState.On)
            //{
            //    dtpFromDate.CustomFormat = "dd/MM/yyyy";
            //    dtpTillDate.CustomFormat = "dd/MM/yyyy";
            //    dtpFromDate.ShowUpDown = false;
            //    dtpTillDate.ShowUpDown = false;
            //}
        }

        private void optTimeWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == ToggleState.On)
            //{
              

            //    dtpFromDate.ShowUpDown = true;
            //    dtpTillDate.ShowUpDown = true;



            //    dtpFromDate.CustomFormat = "HH:mm";
            //    dtpTillDate.CustomFormat = "HH:mm";
            //}
        }

       


        private void ShowNearestDrv(bool show)
        {

            lblnearest1.Visible = show;
            lblnearest2.Visible = show;
            numAutoDespDrvRadius.Visible = show;
        }

        private void ShowLongestWaitingDrv(bool show)
        {

            lblLongestwaiting.Visible = show;
             lbllongestwaiting2.Visible = show;
            numAutoDespLongestWaitingMins.Visible = show;
        }

        private void optAutoDespRule1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {

                ShowNearestDrv(false);
                ShowLongestWaitingDrv(false);
            }
        }

        private void optAutoDespRule2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {

                ShowNearestDrv(true);
                ShowLongestWaitingDrv(false);
            }

        }

        private void optAutoDespRule3_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {

                ShowNearestDrv(true);
                ShowLongestWaitingDrv(false);
            }
        }

        private void optAutoDespRule4_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {

                ShowNearestDrv(false);
                ShowLongestWaitingDrv(true);
            }
        }

        private void optAutoDespRule5_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {

                ShowNearestDrv(true);
                ShowLongestWaitingDrv(true);
            }
        }

    }



}
