﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Taxi_BLL;
using DAL;
using System.Data.SqlClient;
using Telerik.WinControls;
using System.Collections;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmCopyCompanyFares : UI.SetupBase
    {
        bool SaveFares = false;
        bool ApplyAdditionOrSubtractionOnFareRates = false;
        FareBO objMaster;
        BackgroundWorker worker = null;
        ArrayList selectlist = new ArrayList();
        List<Gen_Company> Company = new List<Gen_Company>();
        List<Fare_ChargesDetail> ChargesDetail = new List<Fare_ChargesDetail>();
        List<Fare_OtherCharge> OtherCharge = new List<Fare_OtherCharge>();
        List<Fare_PDAMeter> PDAMeter = new List<Fare_PDAMeter>();
        List<Fare_ZoneWisePricing> ZoneWisePricing = new List<Fare_ZoneWisePricing>();
        List<Gen_Company_AgentCommission> CompanyAirportandStation = new List<Gen_Company_AgentCommission>();
        //List<Gen_Company> globalcompanyidfilter = new List<Gen_Company>();
        public int Vechileid;
        public int CompanyId;
        public frmCopyCompanyFares()
        {
            InitializeComponent();
            FormatFaresGrid();
            objMaster = new FareBO();
            this.SetProperties((INavigation)objMaster);
            grdCompany.EnableFiltering = true;
            grdCompany.CellFormatting += new CellFormattingEventHandler(grdFares_CellFormatting);
            this.Load += new EventHandler(frmShowcompanyCopyFare_Load);
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            this.chkAllCompany.CheckedChanged += new EventHandler(chkAllCompany_CheckedChanged);

        }

        void chkAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAllCompany.Checked == true)
                {
                    if (grdCompany.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompany.Rows.Count; i++)
                        {
                            grdCompany.Rows[i].Cells[COLS.colcheck].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else if (chkAllCompany.Checked == false)
                {
                    if (grdCompany.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompany.Rows.Count; i++)
                        {
                            grdCompany.Rows[i].Cells[COLS.colcheck].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        void frmShowcompanyCopyFare_Load(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        void grdFares_CellFormatting(object sender, CellFormattingEventArgs e)
        {

            if (e.Row.Cells[COLS.RowNo].Value.ToInt() == 1)
            {
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.LightGreen;
                e.CellElement.DrawFill = true;
            }
        }
        public void FaresSaved(int id)
        {

            try
            {
                for (int i = 0; i < grdCompany.RowCount; i++)
                {

                    if (grdCompany.Rows[i].Cells[COLS.Id].Value.ToInt() == id)
                    {
                        grdCompany.Rows[i].Cells[COLS.RowNo].Value = 1;

                    }
                }


            }
            catch (Exception ex)
            {
                // hasError = true;
            }

        }

        public void bindgridother(List<Gen_Company> filterlist)
        {

            try
            {

                var listC = filterlist;
                Company = filterlist;
                int Count = listC.Count;
                grdCompany.RowCount = Count;
                for (int i = 0; i < grdCompany.RowCount; i++)
                {
                    grdCompany.Rows[i].Cells[COLS.Id].Value = listC[i].Id;
                    grdCompany.Rows[i].Cells[COLS.CompanyName].Value = listC[i].CompanyName;
                    grdCompany.Rows[i].Cells[COLS.RowNo].Value = 0;
                    bool IsSelected = grdCompany.Rows[i].Cells[COLS.colcheck].Value.ToBool();
                }
            }
            catch (Exception ex)
            {
                // hasError = true;
            }

        }


        public struct COLS
        {
            public static string Id = "Id";
            public static string CompanyName = "CompanyName";
            public static string RowNo = "RowNo";
            public static string colcheck = "Colcheck";
        }


        private void FormatFaresGrid()
        {
            grdCompany.AllowAddNewRow = false;
            grdCompany.AllowEditRow = true;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdCompany.Columns.Add(col);

            GridViewCheckBoxColumn colcheck = new GridViewCheckBoxColumn();

            colcheck.HeaderText = "";
            colcheck.Name = COLS.colcheck;
            grdCompany.Columns.Add(colcheck);
            col = new GridViewTextBoxColumn();
            col.HeaderText = "Company Name";
            col.Name = COLS.CompanyName;
            col.Width = 300;
            col.ReadOnly = true;
            grdCompany.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.RowNo;
            col.IsVisible = false;
            grdCompany.Columns.Add(col);

        }
        private bool HasOffPeakRate = false;
        public void showfare(int CompanyId)
        {
            worker.RunWorkerAsync();
            //Thread th = new Thread(() => faretoallcompany(cVehicleTypeId));
            //th.IsBackground = true;
            //th.Start();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCopyfares.Enabled = true;
            if (e.Result.ToStr() == "close")
            {
                this.Close();
            }
        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                e.Result = e.Argument.ToStr();
            
                //faretoallcompany(Vechileid);
                CopyCompanyFares(CompanyId);
            }
            catch (Exception ex)
            {
            }
        }
        public void CopyCompanyFares(int CompanyId)
        {
            try
            {
                //  int VehicleTypeId = cVehicleTypeId;
                if (SaveFares == false)
                {
                    int FareId = 0;
                   

                    var Query = General.GetObject<Fare>(c => c.CompanyId == CompanyId);
                    if (CompanyId == 0 && Vechileid > 0)
                    {
                        var Query2 = General.GetObject<Fare>(c => c.VehicleTypeId == Vechileid);
                        if (Query2 != null)
                        {
                            FareId = Query2.Id;


                        }
                    }
                    if (Query != null)
                    {
                        FareId = Query.Id;
                    }
                    var FareList = (from a in General.GetQueryable<Fare_ChargesDetail>(c => c.FareId == FareId)
                                    select new
                                    {
                                        Id = a.Id,
                                        FareId = a.FareId,
                                        OriginLocationTypeId = a.OriginLocationTypeId,
                                        DestinationLocationTypeId = a.DestinationLocationTypeId,
                                        OriginId = a.OriginId,
                                        DestinationId = a.DestinationId,
                                        FromAddress = a.FromAddress,
                                        ToAddress = a.ToAddress,
                                        Rate = a.Rate,
                                        NightTimeRate = HasOffPeakRate ? a.NightTimeRate : 0,
                                    }).ToList();
                    ChargesDetail.Clear();
                    foreach (var item in FareList)
                    {
                        decimal Rate = item.Rate.ToDecimal();
                        if (ApplyAdditionOrSubtractionOnFareRates)
                        {
                            decimal Amount = 0.00m;
                            decimal PercentValue = numPercent.Value.ToDecimal();

                            if (rbtnAdd.IsChecked)
                            {
                                Amount = ((Rate * PercentValue) / 100);
                                Rate = (Rate + Amount);
                            }
                            else
                            {
                                Amount = ((Rate * PercentValue) / 100);
                                Rate = (Rate - Amount);
                            }
                        }
                        ChargesDetail.Add(new Fare_ChargesDetail { Id = item.Id, FareId = item.FareId, OriginLocationTypeId = item.OriginLocationTypeId, DestinationLocationTypeId = item.DestinationLocationTypeId, OriginId = item.OriginId, DestinationId = item.DestinationId, FromAddress = item.FromAddress, ToAddress = item.ToAddress, Rate = Rate, NightTimeRate = item.NightTimeRate });
                    }
                    var OtherChargesList = (from a in General.GetQueryable<Fare_OtherCharge>(c => c.FareId == FareId)
                                            select new
                                            {
                                                Id = a.Id,
                                                FareId = a.FareId,
                                                FromMile = a.FromMile,
                                                ToMile = a.ToMile,
                                                Rate = a.Rate,
                                                FromStartTime = HasOffPeakRate ? a.FromStartTime : null,
                                                TillStartTime = HasOffPeakRate ? a.TillStartTime : null,
                                                FromEndTime = HasOffPeakRate ? a.FromEndTime : null,
                                                TillEndTime = HasOffPeakRate ? a.TillEndTime : null,
                                                PeakTimeRate = HasOffPeakRate ? a.PeakTimeRate : 0,
                                                OffPeakTimeRate = HasOffPeakRate ? a.OffPeakTimeRate : 0,
                                                NightTimeRate = HasOffPeakRate ? a.NightTimeRate : 0,
                                            }).ToList();
                    OtherCharge.Clear();
                    foreach (var item in OtherChargesList)
                    {
                        OtherCharge.Add(new Fare_OtherCharge { Id = item.Id, FareId = item.FareId, FromMile = item.FromMile, ToMile = item.ToMile, Rate = item.Rate, FromStartTime = item.FromStartTime, TillStartTime = item.TillStartTime, FromEndTime = item.TillEndTime, TillEndTime = item.TillEndTime, PeakTimeRate = item.PeakTimeRate, OffPeakTimeRate = item.OffPeakTimeRate, NightTimeRate = item.NightTimeRate });
                    }

                    var PDAMeterList = (from a in General.GetQueryable<Fare_PDAMeter>(c => c.FareId == FareId)
                                        select new
                                        {
                                            Id = a.Id,
                                            FareId = a.FareId,
                                            FromMile = a.FromMile,
                                            ToMile = a.ToMile,
                                            Rate = a.Rate,
                                            FromStartTime = HasOffPeakRate ? a.FromStartTime : null,
                                            TillStartTime = HasOffPeakRate ? a.TillStartTime : null,
                                            FromEndTime = HasOffPeakRate ? a.FromEndTime : null,
                                            TillEndTime = HasOffPeakRate ? a.TillEndTime : null,
                                            PeakTimeRate = HasOffPeakRate ? a.PeakTimeRate : 0,
                                            OffPeakTimeRate = HasOffPeakRate ? a.OffPeakTimeRate : 0,
                                            NightTimeRate = HasOffPeakRate ? a.NightTimeRate : 0,
                                        }).ToList();
                    PDAMeter.Clear();
                    foreach (var item in PDAMeterList)
                    {
                        PDAMeter.Add(new Fare_PDAMeter { Id = item.Id, FareId = item.FareId, FromMile = item.FromMile, ToMile = item.ToMile, Rate = item.Rate, FromStartTime = item.FromStartTime, TillStartTime = item.TillStartTime, FromEndTime = item.FromEndTime, TillEndTime = item.TillEndTime, PeakTimeRate = item.PeakTimeRate, OffPeakTimeRate = item.OffPeakTimeRate, NightTimeRate = item.NightTimeRate });
                    }
                    var ZoneWisePricingList = (from a in General.GetQueryable<Fare_ZoneWisePricing>(c => c.FareId == FareId)
                                               select new
                                               {
                                                   Id = a.Id,
                                                   FareId = a.FareId,
                                                   FromZoneId = a.FromZoneId,
                                                   ToZoneId = a.ToZoneId,
                                                   Price = a.Price,

                                               }).ToList();
                    ZoneWisePricing.Clear();
                    foreach (var item in ZoneWisePricingList)
                    {
                        ZoneWisePricing.Add(new Fare_ZoneWisePricing { Id = item.Id, FareId = item.FareId, FromZoneId = item.FromZoneId, ToZoneId = item.ToZoneId, Price = item.Price });
                    }


                    var AirportAndStationList = (from a in General.GetQueryable<Gen_Company_AgentCommission>(c => c.FareId == FareId)
                                                 select new
                                                 {

                                                     Id = a.Id,
                                                     a.CompanyId,
                                                     FareId = a.FareId,
                                                     LocationId = a.LocationId,
                                                     LocationTypeId = a.LocationTypeId,
                                                     CommissionPercent = a.CommissionPercent,
                                                     CommissionAmount = a.CommissionAmount,
                                                     CommissionOnPercent = a.CommissionOnPercent,
                                                     DayWise = a.DayWise,
                                                     NightWise = a.NightWise,
                                                     VehicleTypeId = a.VehicleTypeId,

                                                     CompanyPrice = a.CompanyPrice,
                                                     DriverPrice = a.DriverPrice,
                                                     CustomerPrice = a.CustomerPrice
                                                 }).ToList();
                    CompanyAirportandStation.Clear();
                    foreach (var item in AirportAndStationList)
                    {
                        CompanyAirportandStation.Add(new Gen_Company_AgentCommission { Id = item.Id, FareId = item.FareId, CompanyId=item.CompanyId, LocationId = item.LocationId, LocationTypeId = item.LocationTypeId, CommissionAmount = item.CommissionAmount, CommissionOnPercent = item.CommissionOnPercent, CommissionPercent = item.CommissionPercent, CompanyPrice = item.CompanyPrice, CustomerPrice = item.CustomerPrice, DayWise = item.DayWise, NightWise = item.NightWise, DriverPrice = item.DriverPrice, VehicleTypeId = item.VehicleTypeId });
                    }
                }
                else
                {
                    var CompanyList = (from a in General.GetQueryable<Gen_Company>(null)
                                       select new
                                       {
                                           Id = a.Id,
                                           CompanyName = a.CompanyName
                                       }).ToList();


                    var queryy = CompanyList.Where(item => selectlist.Contains(item.Id));
                    foreach (var item in queryy)
                    {
                        if (objMaster.PrimaryKeyValue == null)
                        {
                            objMaster.New();

                        }
                        else
                        {
                            objMaster.Edit();
                        }
                        objMaster.Current.VehicleTypeId = Vechileid;
                        // objMaster.Current.VehicleTypeId = VehicleTypeId.ToInt();
                        objMaster.Current.CompanyId = item.Id;
                        objMaster.Current.IsCompanyWise = true;
                        objMaster.Current.AddOn = DateTime.Now;
                        objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();
                        objMaster.Current.StartRate = numStartRate.Value;
                        objMaster.Current.StartRateValidMiles = numStartRateValidMiles.Value;


                        string[] skipProperties = {"Gen_Company", "Gen_Location", "Gen_Location1","Gen_LocationType",
                                              "Gen_LocationType1","Fare","Gen_Zone1","Gen_Zone","Fare_ZoneWisePricing1","Fare_ZoneWisePricing","Fleet_VehicleType"};
                        IList<Fare_ChargesDetail> savedList = objMaster.Current.Fare_ChargesDetails;
                        List<Fare_ChargesDetail> ListDetail = (from a in ChargesDetail
                                                               select new Fare_ChargesDetail
                                                               {
                                                                   Id = a.Id,
                                                                   FareId = objMaster.Current.Id,
                                                                   OriginLocationTypeId = a.OriginLocationTypeId,
                                                                   DestinationLocationTypeId = a.OriginLocationTypeId,
                                                                   OriginId = a.OriginId,
                                                                   DestinationId = a.DestinationId,
                                                                   FromAddress = a.FromAddress,
                                                                   ToAddress = a.ToAddress,
                                                                   Rate = a.Rate,
                                                                   NightTimeRate = a.NightTimeRate

                                                               }).ToList();

                        Utils.General.SyncChildCollection(ref savedList, ref ListDetail, "Id", skipProperties);

                        // OtherCharge

                        IList<Fare_OtherCharge> savedList2 = objMaster.Current.Fare_OtherCharges;
                        List<Fare_OtherCharge> listofOtherDetail = (from a in OtherCharge
                                                                    select new Fare_OtherCharge
                                                                    {
                                                                        Id = a.Id,
                                                                        FareId = a.FareId,
                                                                        FromMile = a.FromMile,
                                                                        ToMile = a.ToMile,
                                                                        Rate = a.Rate,
                                                                        FromStartTime = HasOffPeakRate ? a.FromStartTime : null,
                                                                        TillStartTime = HasOffPeakRate ? a.TillStartTime : null,
                                                                        FromEndTime = HasOffPeakRate ? a.FromEndTime : null,
                                                                        TillEndTime = HasOffPeakRate ? a.TillEndTime : null,
                                                                        PeakTimeRate = HasOffPeakRate ? a.PeakTimeRate : 0,
                                                                        OffPeakTimeRate = HasOffPeakRate ? a.OffPeakTimeRate : 0,
                                                                        NightTimeRate = HasOffPeakRate ? a.NightTimeRate : 0
                                                                    }).ToList();


                        Utils.General.SyncChildCollection(ref savedList2, ref listofOtherDetail, "Id", skipProperties);

                        // PDA METER

                        IList<Fare_PDAMeter> savedList3 = objMaster.Current.Fare_PDAMeters;
                        List<Fare_PDAMeter> listofpdaOtherDetail = (from a in PDAMeter
                                                                    select new Fare_PDAMeter
                                                                    {
                                                                        Id = a.Id,
                                                                        FareId = a.FareId,
                                                                        FromMile = a.FromMile,
                                                                        ToMile = a.ToMile,
                                                                        Rate = a.Rate,
                                                                        FromStartTime = HasOffPeakRate ? a.FromStartTime : null,
                                                                        TillStartTime = HasOffPeakRate ? a.TillStartTime : null,
                                                                        FromEndTime = HasOffPeakRate ? a.FromEndTime : null,
                                                                        TillEndTime = HasOffPeakRate ? a.TillEndTime : null,
                                                                        PeakTimeRate = HasOffPeakRate ? a.PeakTimeRate : 0,
                                                                        OffPeakTimeRate = HasOffPeakRate ? a.OffPeakTimeRate : 0,
                                                                        NightTimeRate = HasOffPeakRate ? a.NightTimeRate : 0
                                                                    }).ToList();


                        Utils.General.SyncChildCollection(ref savedList3, ref listofpdaOtherDetail, "Id", skipProperties);

                        IList<Fare_ZoneWisePricing> saveList4 = objMaster.Current.Fare_ZoneWisePricings;
                        List<Fare_ZoneWisePricing> listofDetail4 = (from a in ZoneWisePricing
                                                                    select new Fare_ZoneWisePricing
                                                                    {
                                                                        Id = a.Id,
                                                                        FareId = a.FareId,
                                                                        FromZoneId = a.FromZoneId,
                                                                        ToZoneId = a.ToZoneId,
                                                                        Price = a.Price,
                                                                    }).ToList();

                        Utils.General.SyncChildCollection(ref saveList4, ref listofDetail4, "Id", skipProperties);
                        //HP

                        IList<Gen_Company_AgentCommission> saveList5 = objMaster.Current.Gen_Company_AgentCommissions;
                        List<Gen_Company_AgentCommission> listofDetail5 = (from a in CompanyAirportandStation
                                                                           select new Gen_Company_AgentCommission
                                                                    {
                                                                        Id = a.Id,
                                                                        FareId = a.FareId,
                                                                        LocationId = a.LocationId,
                                                                        LocationTypeId = a.LocationTypeId,
                                                                        CommissionPercent = a.CommissionPercent,
                                                                        CommissionAmount = a.CommissionAmount,
                                                                        CommissionOnPercent = a.CommissionOnPercent,
                                                                        DayWise = a.DayWise,
                                                                        NightWise = a.NightWise,
                                                                        VehicleTypeId = a.VehicleTypeId,

                                                                        CompanyPrice = a.CompanyPrice,
                                                                        DriverPrice = a.DriverPrice,
                                                                        CustomerPrice = a.CustomerPrice,
                                                                        CompanyId = item.Id
                                                                    }).ToList();

                        Utils.General.SyncChildCollection(ref saveList5, ref listofDetail5, "Id", skipProperties);
                        //

                        objMaster.Save();
                        objMaster.Clear();
                        objMaster = new FareBO();
                        FaresSaved(item.Id);

                    }
                }

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

        private void btnCopyfares_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (grdCompany.Rows.Count(c => c.Cells[COLS.colcheck].Value.ToBool()) == 0)
                {
                    ENUtils.ShowMessage("Please select any company to copy fares");
                    return;
                }

                for (int i = 0; i < grdCompany.RowCount; i++)
                {
                    if (grdCompany.Rows[i].Cells[COLS.colcheck].Value.ToBool() == true)
                    {
                        var CId = grdCompany.Rows[i].Cells[COLS.Id].Value.ToInt();
                        selectlist.Add(CId);
                    }
                }


                btnCopyfares.Enabled = false;
                SaveFares = true;
                worker.RunWorkerAsync("close");
                // showfare(CompanyId);
                //General.RefreshListWithoutSelected<frmFaresList>("frmFaresList1");
               
            }
            catch (Exception ex)
            {
                btnCopyfares.Enabled = true;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy == true)
            {
                //RadMessageBox.Show("Please wait, Fares are copying...");
                return;

          


            }
            else
            {

                this.Close();
                this.Dispose();
                GC.Collect();

            }
        }

        private void radDocking_PreviewClose(object sender, StateChangeEventArgs e)
        {
            label1.Text = "not close";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveFares = false;
            ApplyAdditionOrSubtractionOnFareRates = true;
            worker.RunWorkerAsync();
        }



    }
}
