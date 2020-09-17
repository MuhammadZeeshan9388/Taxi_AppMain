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
    public partial class frmPDAMeterFares : UI.SetupBase
    {
        FareBO objMaster;
        public struct COLS_DETAILS
        {
            public static string ID = "ID";
            public static string FAREID = "FAREID";
            public static string FROMLOCTYPEID = "FROMLOCTYPEID";
            public static string TOLOCTYPEID = "TOLOCTYPEID";

            public static string FROMLOCATIONID = "FROMLOCATIONID";
            public static string TOLOCATIONID = "TOLOCATIONID";

            public static string FROMLOCATION = "FromLocation";
            public static string TOLOCATION = "ToLocation";
            public static string FARE = "Fare";

        }


        private bool HasOffPeakRate = false;


        public struct COLS_OTHERDETAILS
        {
            public static string ID = "ID";
            public static string FAREID = "FAREID";


            public static string FROMMILE = "FromMile";
            public static string TOMILE = "ToMile";


            public static string PEAKTIME = "Peak";
            public static string PEAKRATE = "PeakRate";


            public static string OFFPEAKTIME = "OffPeak";
            public static string OFFPEAKRATE = "OffPeakRate";



            public static string FROMSTARTTIME = "FromStartTime";
            public static string TILLSTARTTIME = "TillStartTime";


            public static string FROMENDTIME = "FromEndTime";
            public static string TILLENDTIME = "TillEndTime";


            public static string FARE = "Fare";
            //   decimal StartRate = numStartRate.Value.ToDecimal();
            //NC
            public static string ValidMiles = "ValidMiles";
            public static string FromStartRate = "FromStartRate";
            public static string FromValidMiles = "FromValidMiles";
            public static string TillStartRate = "TillStartRate";
            public static string TillValidMiles = "TillValidMiles";

            public static string FromPeakTimeStartTime = "FromPeakTimeStartTime";
            public static string TillPeakTimeStartTime = "TillPeakTimeStartTime";
            public static string FromOffPeakEndTime = "FromOffPeakEndTime";
            public static string TillOffPeakEndTime = "TillOffPeakEndTime";

            //public static string FromPeakTimeStartTime = "FromPeakTimeStartTime";
            //public static string TillPeakTimeStartTime = "TillPeakTimeStartTime";
            public static string FromOffPeakFromEndTime = "FromOffPeakFromEndTime";
            public static string TillOffPeakToEndTime = "TillOffPeakToEndTime";

        }
        public struct COLS_PLOTEWISE
        {
            public static string Id = "Id";
            public static string FareId = "FareId";
            public static string FromZoneId = "FromZoneId";
            public static string ToZoneId = "ToZoneId";

            public static string FromPlotNo = "FromPlotNo";
            public static string FromPlot = "FromPlot";
            public static string ToPlot = "ToPlot";
            public static string ToPlotNo = "ToPlotNo";

            public static string Price = "Price";
        }
        //
        //Hyde Park 
        public struct COLS_AirportCommission
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Location = "Location";
            public static string LocationId = "LocationId";
            public static string LocationTypeId = "LocationTypeId";
            public static string CommissionPercent = "CommissionPercent";
            public static string CommissionOnPercent = "CommissionOnPercent";
            public static string CommissionAmount = "CommissionAmount";
            public static string FareId = "FareId";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string VehicleType = "VehicleType";
            public static string CustomerPrice = "CustomerPrice";
            public static string DriverPrice = "DriverPrice";
            public static string CompanyPrice = "CompanyPrice";


        }
        public struct COLS_StationCommission
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Location = "Location";
            public static string LocationId = "LocationId";
            public static string LocationTypeId = "LocationTypeId";
            public static string CommissionPercent = "CommissionPercent";
            public static string CommissionOnPercent = "CommissionOnPercent";
            public static string CommissionAmount = "CommissionAmount";
            public static string FareId = "FareId";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string VehicleType = "VehicleType";
            public static string CustomerPrice = "CustomerPrice";
            public static string DriverPrice = "DriverPrice";
            public static string CompanyPrice = "CompanyPrice";
        }
       
        private Telerik.WinControls.UI.RadLabel radLabel36;
      
        public frmPDAMeterFares(int CompanyId, int ShowTabIndex)
        {
            InitializeComponent();
            InitializeConstructors();
            ddlCompany.SelectedValue = CompanyId;
            chkCompanyWise.Checked = true;
            chkCompanyWise.Enabled = false;
            ddlCompany.Enabled = false;

            //
            //rbtnAdd.Visible = false;
            //rbtnSubtract.Visible = false;
            //lblPercent.Visible = false;
            //numPercent.Visible = false;
            //btnApply.Visible = false;
           
        }
        public void InitializeConstructors()
        {
            this.Load += new EventHandler(frmFares_Load);
            objMaster = new FareBO();
            this.SetProperties((INavigation)objMaster);

            if (AppVars.objPolicyConfiguration != null)
            {

                HasOffPeakRate = AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool();
            }


          










            //if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool())
            //{
            //    this.pg_FixFareList.Item.Visibility = ElementVisibility.Collapsed;
            //}

           


         
            FormatFaresPDAOtherDetailGrid();
          
            FillCombos();

            OnNew();
           
        }

        void btnApply_Click(object sender, EventArgs e)
        {
        
        }


  
        public frmPDAMeterFares()
        {
            InitializeComponent();
            InitializeConstructors();






        }


        public frmPDAMeterFares(int vehicleTypeId)
        {
            InitializeComponent();
            InitializeConstructors();


            ddlVehicleType.SelectedValue = vehicleTypeId;
            int fareId = General.GetObject<Fare>(c => c.VehicleTypeId == vehicleTypeId && c.CompanyId==null).DefaultIfEmpty().Id;

            ddlVehicleType.Enabled = false;

            objMaster.GetByPrimaryKey(fareId);
            OnDisplayRecord(fareId);

            if(ddlVehicleType.Text.Trim().Length >0)
              this.FormTitle = "Fare Meter Settings for " + ddlVehicleType.Text.Trim();

        }

      

        void frmFares_Load(object sender, EventArgs e)
        {
         
            
        }





        private void FillCombos()
        {
          

            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);
            ComboFunctions.FillCompanyCombo(ddlCompany);

          

            ComboFunctions.FillSubCompanyCombo(ddlSubCompanyId);

            if (ddlSubCompanyId.Items.Count == 0)
                ddlSubCompanyId.SelectedIndex = 0;


        }

      


        private void FormatFaresPDAOtherDetailGrid()
        {
            try
            {
                grdPDAOtherCharges.AllowAddNewRow = false;
                // grdOtherCharges.AllowEditRow = false;
                grdPDAOtherCharges.AutoCellFormatting = true;
                grdPDAOtherCharges.ShowGroupPanel = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.Name = COLS_DETAILS.ID;
                col.IsVisible = false;
                grdPDAOtherCharges.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_DETAILS.FAREID;
                col.IsVisible = false;
                grdPDAOtherCharges.Columns.Add(col);



                GridViewDecimalColumn colDec = null;


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "From Mile";
                colDec.Name = COLS_OTHERDETAILS.FROMMILE;
                colDec.Width = 240;
                colDec.DecimalPlaces = 2;
                colDec.ReadOnly = true;
                grdPDAOtherCharges.Columns.Add(colDec);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "To Mile";
                colDec.Name = COLS_OTHERDETAILS.TOMILE;
                colDec.Width = 240;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                grdPDAOtherCharges.Columns.Add(colDec);



                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Rate (£)";
                colDec.Width = 100;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.FARE;
                grdPDAOtherCharges.Columns.Add(colDec);



                if (HasOffPeakRate)
                {

                    grdPDAOtherCharges.Columns[COLS_OTHERDETAILS.FROMMILE].Width = 85;
                    grdPDAOtherCharges.Columns[COLS_OTHERDETAILS.TOMILE].Width = 85;
                    grdPDAOtherCharges.Columns[COLS_OTHERDETAILS.FARE].Width = 100;


                    pnlpdaoffpeak.Enabled = true;


                    GridViewTextBoxColumn colTxt = new GridViewTextBoxColumn();
                    colTxt.HeaderText = "Peak Time";
                    colTxt.Name = COLS_OTHERDETAILS.PEAKTIME;
                    colTxt.Width = 110;
                    colTxt.ReadOnly = true;
                    grdPDAOtherCharges.Columns.Add(colTxt);


                    colDec = new GridViewDecimalColumn();
                    colDec.HeaderText = "Peak Rate (£)";
                    colDec.Width = 120;
                    colDec.ReadOnly = false;
                    colDec.DecimalPlaces = 2;
                    colDec.ThousandsSeparator = true;
                    colDec.Name = COLS_OTHERDETAILS.PEAKRATE;
                    grdPDAOtherCharges.Columns.Add(colDec);



                    colTxt = new GridViewTextBoxColumn();
                    colTxt.HeaderText = "Off Peak Time";
                    colTxt.Name = COLS_OTHERDETAILS.OFFPEAKTIME;
                    colTxt.Width = 110;
                    colTxt.ReadOnly = true;
                    grdPDAOtherCharges.Columns.Add(colTxt);


                    colDec = new GridViewDecimalColumn();
                    colDec.HeaderText = "Off Peak Rate (£)";
                    colDec.Width = 150;
                    colDec.ReadOnly = false;
                    colDec.DecimalPlaces = 2;
                    colDec.ThousandsSeparator = true;
                    colDec.Name = COLS_OTHERDETAILS.OFFPEAKRATE;
                    grdPDAOtherCharges.Columns.Add(colDec);




                    GridViewDateTimeColumn colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.FROMSTARTTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.TILLSTARTTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.FROMENDTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.TILLENDTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);

                }




                grdPDAOtherCharges.MasterTemplate.ShowRowHeaderColumn = false;


                UI.GridFunctions.AddDeleteColumn(grdPDAOtherCharges);
                grdPDAOtherCharges.Columns["ColDelete"].Width = 80;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        #region Overridden Methods


        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
          
            ddlCompany.SelectedValue = null;
            chkCompanyWise.Checked = false;
            CompanyWise(ToggleState.Off);
         


            //dtpEffectiveDate.Focus();
            ddlVehicleType.SelectedValue = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;
            ddlVehicleType.Focus();


        }


      

        public override void Save()
        {
            try
            {
          

                if (objMaster.PrimaryKeyValue == null)
                {
                   

                        objMaster.New();
                 
                }
                else
                {
                    objMaster.Edit();
                }

                //  objMaster.Current.EffectiveDate = dtpEffectiveDate.Value.ToDateorNull();
                objMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                objMaster.Current.IsCompanyWise = chkCompanyWise.Checked;
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
              

                objMaster.Current.SubCompanyId = ddlSubCompanyId.SelectedValue.ToIntorNull();
                //   Fare_ChargesDetail c = new Fare_ChargesDetail();
                //NC 16/12/16
                decimal StartRate = numStartRate.Value.ToDecimal();
                decimal ValidMiles = numValidMiles.Value.ToDecimal();
                decimal FromStartRate = numFromStartRate.Value.ToDecimal();
                decimal FromValidMiles = numFromValidMiles.Value.ToDecimal();
                decimal TillStartRate = numTillStartRate.Value.ToDecimal();
                decimal TillValidMiles = numTillValidMiles.Value.ToDecimal();


                //DateTime dateValue = new DateTime(1900, 1, 1, 0, 0, 0);
               
                //DateTime? fromPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPeakTimeFromStartTime.Value.Value.TimeOfDay).ToDateTime();
                //DateTime? tillPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPeakTimeToStartTime.Value.Value.TimeOfDay).ToDateTime();
                //DateTime? fromOffPeakFromEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpOffPeakFromEndTime.Value.Value.TimeOfDay).ToDateTime();
                //DateTime? tillOffPeakToEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpOffPeakToEndTime.Value.Value.TimeOfDay).ToDateTime();


                DateTime dateValue = new DateTime(1900, 1, 1, 0, 0, 0);

                DateTime? fromPeakTimeStartTime = null;
                DateTime? tillPeakTimeStartTime = null;
                DateTime? fromOffPeakFromEndTime = null;
                DateTime? tillOffPeakToEndTime = null;


                if (dtpPeakTimeFromStartTime.Value != null)
                {
                    fromPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPeakTimeFromStartTime.Value.Value.TimeOfDay).ToDateTime();
                }

                if (dtpPeakTimeToStartTime.Value != null)
                {
                    tillPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPeakTimeToStartTime.Value.Value.TimeOfDay).ToDateTime();
                }


                if (dtpOffPeakFromEndTime.Value != null)
                {
                    fromOffPeakFromEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpOffPeakFromEndTime.Value.Value.TimeOfDay).ToDateTime();
                }

                if (dtpOffPeakToEndTime.Value != null)
                {
                    tillOffPeakToEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpOffPeakToEndTime.Value.Value.TimeOfDay).ToDateTime();
                }

                if (radPanel8.Visible == false)
                {
                    fromPeakTimeStartTime = null;
                    tillPeakTimeStartTime = null;
                    fromOffPeakFromEndTime = null;
                    tillOffPeakToEndTime = null;
                    FromStartRate = 0;
                    FromValidMiles = 0;
                    TillStartRate = 0;
                    TillValidMiles = 0;
                }
                else
                {
                    StartRate = 0;
                    ValidMiles = 0;
                }


                objMaster.FromStartTime = fromPeakTimeStartTime!=null? fromPeakTimeStartTime.Value.ToDateTimeorNull():null;
                objMaster.FromEndTime = tillPeakTimeStartTime!=null?tillPeakTimeStartTime.Value.ToDateTimeorNull():null;
                objMaster.TillStartTime =fromOffPeakFromEndTime!=null?fromOffPeakFromEndTime.Value.ToDateTimeorNull():null;
                objMaster.TillEndTime =tillOffPeakToEndTime!=null? tillOffPeakToEndTime.Value.ToDateTimeorNull():null;
                objMaster.FromStartRate = FromStartRate;
                objMaster.FromStartRateValidMiles = FromValidMiles;
                objMaster.TillStartRate = TillStartRate;
                objMaster.TillStartRateValidMiles = TillValidMiles;
                objMaster.StartRate = StartRate;
                objMaster.StartRateValidMiles = ValidMiles;
                //

               
              
              
                string[] skipProperties = { "Gen_Location", "Gen_Location1","Gen_LocationType",
                                            "Gen_LocationType1","Fare","Gen_Zone1","Gen_Zone","Fare_ZoneWisePricing1","Fare_ZoneWisePricing"};

 
             

               




                // PDA METER

                IList<Fare_PDAMeter> savedList3 = objMaster.Current.Fare_PDAMeters;
                List<Fare_PDAMeter> listofpdaOtherDetail = (from r in grdPDAOtherCharges.Rows
                                                            select new Fare_PDAMeter
                                                         {
                                                             Id = r.Cells[COLS_OTHERDETAILS.ID].Value.ToLong(),
                                                             FareId = r.Cells[COLS_OTHERDETAILS.FAREID].Value.ToInt(),
                                                             FromMile = r.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal(),
                                                             ToMile = r.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal(),
                                                             Rate = r.Cells[COLS_OTHERDETAILS.FARE].Value.ToDecimal(),


                                                             FromStartTime = fromPeakTimeStartTime != null ? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime() : dateValue,
                                                             TillStartTime =tillPeakTimeStartTime!=null? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,

                                                             FromEndTime =fromOffPeakFromEndTime!=null? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,
                                                             TillEndTime = tillOffPeakToEndTime != null ? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime() : dateValue,
                                                             PeakTimeRate = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.PEAKRATE].Value.ToDecimal() : 0,
                                                             OffPeakTimeRate = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value.ToDecimal() : 0,
                                                         }).ToList();


                Utils.General.SyncChildCollection(ref savedList3, ref listofpdaOtherDetail, "Id", skipProperties);

                objMaster.UpdateStartRates = true;
                objMaster.Save();

                General.RefreshListWithoutSelected<frmFaresList>("frmFaresList1");
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

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            chkEnablePeakOffPeakWiseFares.Checked = HasOffPeakRate;

            Fare obj = objMaster.Current;


            ddlVehicleType.SelectedValue = obj.VehicleTypeId;
            chkCompanyWise.Checked = obj.IsCompanyWise.ToBool();
            ddlCompany.SelectedValue = obj.CompanyId;

            if (obj.SubCompanyId == null && ddlSubCompanyId.Items.Count > 0)
            {
                ddlSubCompanyId.SelectedValue = ddlSubCompanyId.Items[0].Value.ToIntorNull();
            }
            else
            {

                ddlSubCompanyId.SelectedValue = obj.SubCompanyId;
            }
        
            DisplayFarePDAOtherDetails(obj);
           
            DisplayVehicleTypeDetails(obj);
            
           
        }
   
        private void DisplayVehicleTypeDetails(Fare obj)
        {
            try
            {
                if (obj == null) return;

                int VehicleTypeId = obj.VehicleTypeId.ToInt();
                if (VehicleTypeId > 0)
                {
                    var query = General.GetObject<Fleet_VehicleType>(c => c.Id == VehicleTypeId);
                    
                    numStartRate.Value = query.StartRate.ToDecimal();
                    numValidMiles.Value = query.StartRateValidMiles.ToDecimal();
                    numFromStartRate.Value = query.FromStartRate.ToDecimal();
                    numFromValidMiles.Value = query.FromStartRateValidMiles.ToDecimal();
                    numTillStartRate.Value = query.TillStartRate.ToDecimal();
                    numTillValidMiles.Value = query.TillStartRateValidMiles.ToDecimal();

                    dtpPeakTimeFromStartTime.Value = query.FromStartTime;
                    dtpPeakTimeToStartTime.Value = query.FromEndTime;
                    dtpOffPeakFromEndTime.Value = query.TillStartTime;
                    dtpOffPeakToEndTime.Value = query.TillEndTime;
                   
                }
            }
            catch (Exception ex)
            {
                //ENUtils.ShowMessage(ex.Message);
            }
        }
        //
     




        private void DisplayFarePDAOtherDetails(Fare obj)
        {
            if (obj == null) return;

            grdPDAOtherCharges.RowCount = obj.Fare_PDAMeters.Count;
            for (int i = 0; i < grdPDAOtherCharges.RowCount; i++)
            {
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.ID].Value = obj.Fare_PDAMeters[i].Id;
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FAREID].Value = obj.Fare_PDAMeters[i].FareId;



                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMMILE].Value = obj.Fare_PDAMeters[i].FromMile.ToDecimal();
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TOMILE].Value = obj.Fare_PDAMeters[i].ToMile.ToDecimal();

                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FARE].Value = obj.Fare_PDAMeters[i].Rate;


                if (AppVars.objPolicyConfiguration != null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                {

                    DateTime? fromStartTime = obj.Fare_PDAMeters[i].FromStartTime;
                    DateTime? tillStartTime = obj.Fare_PDAMeters[i].TillStartTime;
                    DateTime? fromEndTime = obj.Fare_PDAMeters[i].FromEndTime;
                    DateTime? tillEndTime = obj.Fare_PDAMeters[i].TillEndTime;

                    string peakTime = string.Empty;
                    string offPeakTime = string.Empty;


                    if (fromStartTime != null && tillStartTime != null)
                    {
                        peakTime = string.Format("{0:hh:tt}", fromStartTime) + " to " + string.Format("{0:hh:tt}", tillStartTime);
                    }

                    if (fromEndTime != null && tillEndTime != null)
                    {
                        offPeakTime = string.Format("{0:hh:tt}", fromEndTime) + " to " + string.Format("{0:hh:tt}", tillEndTime);
                    }




                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.PEAKRATE].Value = obj.Fare_PDAMeters[i].PeakTimeRate.ToDecimal();
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = obj.Fare_PDAMeters[i].OffPeakTimeRate.ToDecimal();
                }
            }

            ClearPDAOtherChargesDetails();
        }



    //    private bool IsOpenFromCopyFares = false;

      


    

        #endregion

       

        private void chkCompanyWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            CompanyWise(args.ToggleState);
        }

        private void CompanyWise(ToggleState args)
        {
            if (args == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlCompany.Enabled = true;
              //  pnlPercentage.Visible = false;
               
         
            }
            else
            {
                ddlCompany.Enabled = false;
                ddlCompany.SelectedValue = null;
              
            }

        }

       


       

      

        private void frmFares_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }

        }


        #region


        private void AddPDAOtherChargesDetail()
        {
            try
            {
                decimal fromMile = numpdafrommile.Value.ToDecimal();
                decimal toMile = numpdatomile.Value.ToDecimal();
                decimal rate = numPDARates_OtherCharges.Value.ToDecimal();
                string msg = string.Empty;


                TimeSpan FromStartTime = TimeSpan.Zero;
                TimeSpan TillStartTime = TimeSpan.Zero;
                TimeSpan FromEndTime = TimeSpan.Zero;
                TimeSpan TillEndTime = TimeSpan.Zero;

                string peakTime = string.Empty;
                string offPeakTime = string.Empty;
                decimal peakRate = 0;
                decimal offPeakRate = 0;
                if (AppVars.objPolicyConfiguration != null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                {
                    peakRate = numPDAPeakRate.Value.ToDecimal();
                    offPeakRate = numPDAOffPeakRate.Value.ToDecimal();
                }


                if (HasOffPeakRate == false)
                {
                    if (toMile == 0)
                    {
                        msg += "Required : To Mile." + Environment.NewLine;
                    }

                    if (rate == 0)
                    {
                        msg += "Required : Fare rate.";
                    }
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;

                }


                GridViewRowInfo row;
                if (grdPDAOtherCharges.CurrentRow == null &&
                    grdPDAOtherCharges.Rows.Count(c =>
                                            c.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal() == fromMile
                                        && c.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal() == toMile
                                         ) > 0)
                {
                    ENUtils.ShowMessage("From Mile and To Mile already exist");
                 //   numFromMile.Focus();
                    return;
                }


                DateTime? fromStartTime = dtpPDAFromStartTime.Value;
                DateTime? tillStartTime = dtpPDATillStartTime.Value;
                DateTime? fromEndTime = dtpPDAFromEndTime.Value;
                DateTime? tillEndTime = dtpPDATillEndTime.Value;
                
                

                if (HasOffPeakRate)
                {


                    string error = string.Empty;


                    if (fromStartTime == null)
                    {
                        error += "Required : From Start Time" + Environment.NewLine;

                    }

                    if (tillStartTime == null)
                    {
                        error += "Required : Till Start Time" + Environment.NewLine;

                    }

                    if (fromEndTime == null)
                    {
                        error += "Required : From End Time" + Environment.NewLine;


                    }


                    if (tillEndTime == null)
                    {
                        error += "Required : Till End Time";

                    }


                    if (!string.IsNullOrEmpty(error))
                    {
                        ENUtils.ShowMessage(error);
                        return;
                    }

                    FromStartTime = fromStartTime.Value.TimeOfDay;
                    TillStartTime = tillStartTime.Value.TimeOfDay;

                    FromEndTime = fromEndTime.Value.TimeOfDay;
                    TillEndTime = tillEndTime.Value.TimeOfDay;

                    error = string.Empty;


                    //if (TillStartTime > FromEndTime)
                    //{
                    //    error += "'From End' Time cannot be less than 'To Start' Time" + Environment.NewLine;
                    //}


                    //if (TillEndTime > FromStartTime)
                    //{

                    //    error += "'To End' Time cannot be greater than 'From Start' Time";
                    //}


                    if (!string.IsNullOrEmpty(error))
                    {
                        ENUtils.ShowMessage(error);
                        return;
                    }



                    peakTime = string.Format("{0:hh tt}", fromStartTime) + " to " + string.Format("{0:hh tt}", tillStartTime);
                    offPeakTime = string.Format("{0:hh tt}", fromEndTime) + " to " + string.Format("{0:hh tt}", tillEndTime);

                }


                if (grdPDAOtherCharges.CurrentRow != null)
                    row = grdPDAOtherCharges.CurrentRow;
                else
                    row = grdPDAOtherCharges.Rows.AddNew();


                row.Cells[COLS_OTHERDETAILS.FROMMILE].Value = fromMile;
                row.Cells[COLS_OTHERDETAILS.TOMILE].Value = toMile;

                row.Cells[COLS_OTHERDETAILS.FARE].Value = rate;
                //     objMaster.Current.FromStartTime = dtpFromStartTime.Value.ToDateTimeorNull();

                if (HasOffPeakRate)
                {
                    row.Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                    row.Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                    row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                    row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                    row.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                    row.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                    row.Cells[COLS_OTHERDETAILS.PEAKRATE].Value = peakRate;
                    row.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = offPeakRate;
                }
                ClearPDAOtherChargesDetails();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        private void ClearPDAOtherChargesDetails()
        {
            numpdafrommile.Value = 0;
            numpdatomile.Value = 0;
            numPDARates_OtherCharges.Value = 0;
            grdPDAOtherCharges.CurrentRow = null;


            numPDAPeakRate.Value = 0;
            numPDAOffPeakRate.Value = 0;


            numpdafrommile.Focus();

        }

        private void grdPDAOtherCharges_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdPDAOtherCharges.CurrentRow != null && grdPDAOtherCharges.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdPDAOtherCharges.CurrentRow;

                numpdafrommile.Value = row.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal();
                numpdatomile.Value = row.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal();

                numPDARates_OtherCharges.Value = row.Cells[COLS_DETAILS.FARE].Value.ToDecimal();

                if (HasOffPeakRate)
                {
                    dtpPDAFromStartTime.Value = row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTime();
                    dtpPDATillStartTime.Value = row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTime();
                    dtpPDAFromEndTime.Value = row.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTime();
                    dtpPDATillEndTime.Value = row.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTime();


                    numPDAPeakRate.Value = row.Cells[COLS_OTHERDETAILS.PEAKRATE].Value.ToDecimal();
                    numPDAOffPeakRate.Value = row.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value.ToDecimal();
                }

            }
        }


        #endregion


        private string pdaMeterPwd = string.Empty;

        private void btnPDAAdd_OtherCharges_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pdaMeterPwd))
            {
                frmLockingPwd frmUnLock = new frmLockingPwd();
                frmUnLock.ShowDialog();

                if (string.IsNullOrEmpty(frmUnLock.ReturnValue1))
                    return;
                else
                    pdaMeterPwd = frmUnLock.ReturnValue1;


                frmUnLock.Dispose();
            }




            AddPDAOtherChargesDetail();
        }

        private void btnPDAClear_OtherChrges_Click(object sender, EventArgs e)
        {
            ClearPDAOtherChargesDetails();
        }

       
      

    

      


       


       

        private void chkEnablePeakOffPeakWiseFares_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkEnablePeakOffPeakWiseFares.Checked == true)
            {
             //   label2.Text = "Peak Off Peak Wise Fares Start Rate";
              //  label1.Text = "Peak Off Peak Wise Fares PDA Meter Mileage";
                radPanel8.Visible = true;
                pnlpdaoffpeak.Visible = true;
              //  panel3.Visible = false;
                pnlWithoutPeakFares.Visible = false;
                //pnlpdaoffpeak
            }
            else
            {
             //   label2.Text = "Start Rate";
             //   label1.Text = "Meter Mileage";
                radPanel8.Visible = false;
                pnlpdaoffpeak.Visible = false;
            //    panel3.Visible = true;
                pnlWithoutPeakFares.Visible = true;
            }
        }

       
        //  bool IsPOI = false;


    }
}
