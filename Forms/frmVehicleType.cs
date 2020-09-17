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
using Utils;
using Taxi_Model;
using Telerik.WinControls.UI;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmVehicleType : UI.SetupBase
    {
        VehicleTypeBO objMaster;
        public string CurrentAttributeValues { get; set; }
        public frmVehicleType()
        {
            InitializeComponent();

            objMaster = new VehicleTypeBO();
            this.SetProperties((INavigation)objMaster);

            SetPeakOffPeakFares(AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool() ? ToggleState.On : ToggleState.Off);

         //   chkEnablePeakFares.Enabled = AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool() ? true : false;

            this.Shown += new EventHandler(frmVehicleType_Shown);
            this.Load += new EventHandler(frmVehicleType_Load);

         
        }

        void frmVehicleType_Load(object sender, EventArgs e)
        {
           
        }

        void frmVehicleType_Shown(object sender, EventArgs e)
        {
            txtVehicleType.Focus();


            if (AppVars.listUserRights.Count(c => c.formName == "frmVehicleType" && c.functionId == "VEHICLE ATTRIBUTES") > 0)
            {

                btnAttribute.Visible = true;



                if (btnAttribute.Visible && (objMaster.Current!=null))
                {

                    if (!string.IsNullOrEmpty(objMaster.Current.AttributeValues))
                    {
                        int count = objMaster.Current.AttributeValues.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Count();
                        btnAttribute.Text = "Attributes (" + count + ")";
                    }
                    else
                    {
                        btnAttribute.Text = "Attributes (0)";
                    }
                }
            }
        }

        #region Overridden Methods




        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            txtVehicleType.Text = objMaster.Current.VehicleType;

            numHandLuggages.Value = objMaster.Current.NoofHandLuggages.ToInt();
            numValidMiles.Value = objMaster.Current.StartRateValidMiles.ToDecimal();
            numLuggages.Value = objMaster.Current.NoofLuggages.ToInt();
            numPassengers.Value = objMaster.Current.NoofPassengers.ToInt();
            numStartRate.Value = objMaster.Current.StartRate.ToDecimal();
            pic_Vehicle.SetImage(objMaster.Current.Photo);


          

          
            dtpFromStartTime.Value = objMaster.Current.FromStartTime.ToDateTime();
            dtpTillStartTime.Value = objMaster.Current.TillStartTime.ToDateTime();

            numFromStartRate.Value = objMaster.Current.FromStartRate.ToDecimal();
            numFromValidMiles.Value = objMaster.Current.FromStartRateValidMiles.ToDecimal();


            dtpFromEndTime.Value = objMaster.Current.FromEndTime.ToDateTime();
            dtpTillEndTime.Value = objMaster.Current.TillEndTime.ToDateTime();

            numTillStartRate.Value = objMaster.Current.TillStartRate.ToDecimal();
            numTillValidMiles.Value = objMaster.Current.TillStartRateValidMiles.ToDecimal();
           
            if (!string.IsNullOrEmpty(objMaster.Current.BackgroundColor))
            {
                Color clr = Color.FromArgb(objMaster.Current.BackgroundColor.ToInt());

                (txtBgColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                txtBgColor.Tag = clr.ToArgb();
            }

            if (!string.IsNullOrEmpty(objMaster.Current.TextColor))
            {
                Color clr = Color.FromArgb(objMaster.Current.TextColor.ToInt());

                (txtTextColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                txtTextColor.Tag = clr.ToArgb();
            }


            numDrvWaitingCharges.Value = objMaster.Current.DriverWaitingChargesPerHour.ToDecimal();
            numAccountWaitingChargesMins.Value = objMaster.Current.AccountWaitingChargesPerHour.ToDecimal();


         

         
        }



        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
            pic_Vehicle.Clear();
            txtVehicleType.Focus();

            (txtBgColor.RootElement.Children[0] as RadTextBoxElement).BackColor = Color.White;
                txtBgColor.Tag=null;
            (txtTextColor.RootElement.Children[0] as RadTextBoxElement).BackColor = Color.White;
            txtTextColor.Tag=null;
        }


        private bool IsSaved = false;
        public override void Save()
        {
            try
            {
                IsSaved = false;
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                    Fleet_VehicleType objVehicle = General.GetQueryable<Fleet_VehicleType>(null).OrderByDescending(c => c.Id).FirstOrDefault();
                    if (objVehicle != null)
                    {
                     //   objMaster.Current.Id = objVehicle.Id+1;
                        objMaster.Current.OrderNo = objVehicle.Id + 1;
                    }
                }
                else
                {
                    objMaster.Edit();
                }

                objMaster.Current.VehicleType = txtVehicleType.Text.Trim();
                objMaster.Current.NoofPassengers = numPassengers.Value.ToInt();
                objMaster.Current.NoofLuggages = numLuggages.Value.ToInt();
                objMaster.Current.NoofHandLuggages = numHandLuggages.Value.ToInt();
                objMaster.Current.StartRate = numStartRate.Value.ToDecimal();
                objMaster.Current.StartRateValidMiles = numValidMiles.Value.ToDecimal();
                objMaster.Current.Photo = pic_Vehicle.GetByteArrayOfImage();

                objMaster.Current.BackgroundColor = txtBgColor.Tag.ToStr();
                objMaster.Current.TextColor = txtTextColor.Tag.ToStr();


                DateTime dateValue = new DateTime(1900, 1, 1, 0, 0, 0);

                DateTime? fromPeakTimeStartTime = null;
                DateTime? tillPeakTimeStartTime =null;
                DateTime? fromOffPeakFromEndTime = null;
                DateTime? tillOffPeakToEndTime = null;


                if (dtpFromStartTime.Value != null)
                {
                    fromPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpFromStartTime.Value.Value.TimeOfDay).ToDateTime();
                }

                if (dtpTillStartTime.Value != null)
                {
                    tillPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpTillStartTime.Value.Value.TimeOfDay).ToDateTime();
                }


                if (dtpFromEndTime.Value != null)
                {
                    fromOffPeakFromEndTime =  string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpFromEndTime.Value.Value.TimeOfDay).ToDateTime();
                }

                if (dtpTillEndTime.Value != null)
                {
                    tillOffPeakToEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpTillEndTime.Value.Value.TimeOfDay).ToDateTime();
                }


              //  objMaster.Current.EnablePeakOffPeak = chkEnablePeakFares.Checked;
                objMaster.Current.FromStartTime = fromPeakTimeStartTime;
                objMaster.Current.TillStartTime = tillPeakTimeStartTime;

                objMaster.Current.FromStartRate = numFromStartRate.Value.ToDecimal();
                objMaster.Current.FromStartRateValidMiles = numFromValidMiles.Value.ToDecimal();

                objMaster.Current.FromEndTime = fromOffPeakFromEndTime;
                objMaster.Current.TillEndTime = tillOffPeakToEndTime;

                objMaster.Current.TillStartRate = numTillStartRate.Value.ToDecimal();
                objMaster.Current.TillStartRateValidMiles = numTillValidMiles.Value.ToDecimal();

                objMaster.Current.DriverWaitingChargesPerHour = numDrvWaitingCharges.Value.ToDecimal();
                objMaster.Current.AccountWaitingChargesPerHour = numAccountWaitingChargesMins.Value.ToDecimal();


                objMaster.Save();

                IsSaved = true;
                General.RefreshListWithoutSelected<frmVehicleTypeList>("frmVehicleTypeList1");

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }

        #endregion

        private void btnPickBgColor_Click(object sender, EventArgs e)
        {
            SetColor(txtBgColor);
        }

        private void SetColor(RadTextBox txt)
        {
            if(DialogResult.OK==  colorDialog1.ShowDialog())
            {

               (txt.RootElement.Children[0] as RadTextBoxElement).BackColor = colorDialog1.Color;
               txt.Tag = colorDialog1.Color.ToArgb();
            }

        }

        private void btnPickTextColor_Click(object sender, EventArgs e)
        {
            SetColor(txtTextColor);
        }

        private void btnClearBgColor_Click(object sender, EventArgs e)
        {
            ClearColor(txtBgColor);
        }

        private void ClearColor(RadTextBox txt)
        {

            (txt.RootElement.Children[0] as RadTextBoxElement).BackColor = Color.White;
            txt.Tag = null;
            

        }

        private void btnClearTextColor_Click(object sender, EventArgs e)
        {
            ClearColor(txtTextColor);
        }

        private void frmVehicleType_FormClosed(object sender, FormClosedEventArgs e)
        {
            General.DisposeForm(this);
        }

        private void chkEnablePeakFares_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetPeakOffPeakFares(args.ToggleState);
        }


        private void SetPeakOffPeakFares(ToggleState toggle)
        {

            if (toggle == ToggleState.On)
            {
                pnlOffPeak.Enabled = true;
                pnlWithoutPeakFares.Enabled = false;

            }

            else
            {
                pnlOffPeak.Enabled = false;
                pnlWithoutPeakFares.Enabled = true;


            }
      




        }

        private void frmVehicleType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void btnAttribute_Click(object sender, EventArgs e)
        {
            Save();

            if (IsSaved)
            {



                frmVehicleAttributes frm = new frmVehicleAttributes();

                if (objMaster.Current != null)
                {
                    frm.CurrentVehicleId = objMaster.Current.Id;
                    if (!string.IsNullOrEmpty(CurrentAttributeValues))
                    {
                        frm.OldAttributeValues = CurrentAttributeValues;
                    }
                    else
                    {
                        frm.OldAttributeValues = objMaster.Current.AttributeValues;
                    }
                }
                frm.ShowDialog();
                if (!string.IsNullOrEmpty(frm.CurrentAttributeValues))
                {
                    if (!string.IsNullOrEmpty(frm.CurrentAttributeValues))
                    {
                        int count = frm.CurrentAttributeValues.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).Count();
                        btnAttribute.Text = "Attributes (" + count + ")";

                        this.CurrentAttributeValues = frm.CurrentAttributeValues;
                    }
                    else
                    {
                        btnAttribute.Text = "Attributes (0)";
                    }
                }
            }
        }

    }
}
