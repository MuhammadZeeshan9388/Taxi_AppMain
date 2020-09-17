using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Utils;
using System.IO;
using Taxi_BLL;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;


namespace Taxi_AppMain
{
    public partial class rptfrmDriverCommissionStatement2: UI.SetupBase
    {


        private decimal _PDARent;

        public decimal PDARent
        {
            get { return _PDARent; }
            set { _PDARent = value; }
        }

        private string _TempPath;

        public string TempPath
        {
            get { return _TempPath; }
            set { _TempPath = value; }
        }


        private bool NoACCommission;

        public rptfrmDriverCommissionStatement2()
        {
            InitializeComponent();


            NoACCommission = AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool();
        }

        private string _DatePeriod;

        public string DatePeriod
        {
            get { return _DatePeriod; }
            set { _DatePeriod = value; }
        }
        private int _Commision;

        public int Commision
        {
            get { return _Commision; }
            set { _Commision = value; }
        }


       public   List<Fleet_Driver_CommissionRange> listofCommRange = null;

        private List<stp_DriverCommisionResult> _DataSource;

        public List<stp_DriverCommisionResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        //private List<Vu_BookingBase> _DataSource1;

        //public List<Vu_BookingBase> DataSource1
        //{
        //    get { return _DataSource1; }
        //    set { _DataSource1 = value; }
        //}

        //private List<stp_DriverCommisionResult> _DataSource;

        //public List<stp_DriverCommisionResult> DataSource
        //{
        //    get { return _DataSource; }
        //    set { _DataSource = value; }
        //}

        private string _ReportHeading;

        public string ReportHeading
        {
            get { return _ReportHeading; }
            set { _ReportHeading = value; }
        }


        private int _StatementType;

        public int StatementType
        {
            get { return _StatementType; }
            set { _StatementType = value; }
        }

        public void LoadDriverStatementReportByTemplate4(DateTime fromdate, DateTime todate, int? driverid, int? companyid, int statusid)
        {
            try
            {

                // string className = "Taxi_AppMain.ReportDesigns.Template2_rptDriverCommissionStatement.rdlc";


                string className = TempPath;


                this.reportViewer1.LocalReport.ReportEmbeddedResource = className;
                string pickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool() == true ? "1" : "0";
                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[32];

                this.stp_DriverCommisionResultBindingSource.DataSource = (from a in new Taxi_Model.TaxiDataContext().stp_DriverCommision(fromdate, todate, driverid, companyid, statusid, 0)
                                                                          select new stp_DriverCommisionResult
                                                                          {
                                                                              Id = a.Id,
                                                                              AcceptedDateTime = a.AcceptedDateTime,
                                                                              AccountTypeId = a.AccountTypeId,
                                                                              AgentCommission = a.AgentCommission,
                                                                              AgentCommissionPercent = a.AgentCommissionPercent,
                                                                              ArrivalDateTime = a.ArrivalDateTime,
                                                                              AuthCode = a.AuthCode,
                                                                              BookingStatusId = a.BookingStatusId,
                                                                              BookingTypeId = a.BookingTypeId,
                                                                              ClearedDateTime = a.ClearedDateTime,
                                                                              CompanyCode = a.CompanyCode,
                                                                              CompanyPrice = a.CompanyPrice,
                                                                              CongtionCharges = a.CongtionCharges,
                                                                              CostCenterId = a.CostCenterId,
                                                                              CostCenterName = a.CostCenterName,
                                                                              CustomerPrice = a.CustomerPrice,
                                                                              DepartmentId = a.DepartmentId,
                                                                              DepartmentName = a.DepartmentName,
                                                                              Despatchby = a.Despatchby,
                                                                              DriverAddress = a.DriverAddress,
                                                                              DriverCommission = a.DriverCommission,
                                                                              DriverCommissionType = a.DriverCommissionType,
                                                                              DriverFullName = a.DriverFullName,
                                                                              DriverId = a.DriverId,
                                                                              FromDoorNo = a.FromDoorNo,
                                                                              FromLocType = a.FromLocType,
                                                                              FromStreet = a.FromStreet,
                                                                              IsCommissionWise = a.IsCommissionWise,
                                                                              JobTakenByCompany = a.JobTakenByCompany,
                                                                              MeetAndGreetCharges = a.MeetAndGreetCharges,
                                                                              NoofHandLuggages = a.NoofHandLuggages,
                                                                              PaymentType = a.PaymentType,
                                                                              POBDateTime = a.POBDateTime,
                                                                              PupilNo = a.PupilNo,
                                                                              ReturnDriverFullName = a.ReturnDriverFullName,
                                                                              ReturnDriverId = a.ReturnDriverId,
                                                                              ReturnFareRate = a.ReturnFareRate,
                                                                              ReturnPickupDateTime = a.ReturnPickupDateTime,
                                                                              StatusName = a.StatusName,
                                                                              STCDateTime = a.STCDateTime,
                                                                              SubCompanyId = a.SubCompanyId,
                                                                              ToDoorNo = a.ToDoorNo,
                                                                              ToLocType = a.ToLocType,
                                                                              ToStreet = a.ToStreet,
                                                                              TotalTravelledMiles = a.TotalTravelledMiles,
                                                                              VehicleTypeId = a.VehicleTypeId,
                                                                              BookingDate = a.BookingDate,
                                                                              BookingNo = a.BookingNo,
                                                                              CompanyId = a.CompanyId,
                                                                              CompanyName = a.CompanyName,
                                                                              CustomerMobileNo = a.CustomerMobileNo,
                                                                              CustomerName = a.CustomerName,
                                                                              CustomerPhoneNo = a.CustomerPhoneNo,
                                                                              DriverName = a.DriverName,
                                                                              DriverNo = a.DriverNo,
                                                                              ExtraDropCharges = a.ExtraDropCharges,
                                                                              FareRate = a.FareRate,
                                                                              FromAddress = a.FromAddress,
                                                                              NoofLuggages = a.NoofLuggages,
                                                                              NoofPassengers = a.NoofPassengers,
                                                                              OrderNo = a.OrderNo,
                                                                              ParkingCharges = a.ParkingCharges,
                                                                              PaymentTypeId = a.PaymentTypeId,
                                                                              PickupDateTime = a.PickupDateTime,
                                                                              SpecialRequirements = a.SpecialRequirements,
                                                                              ToAddress = a.ToAddress,
                                                                              TotalCharges = a.TotalCharges,
                                                                              WaitingCharges = a.WaitingCharges,
                                                                              Via1 = a.Via1,
                                                                              VehicleType = a.VehicleType
                                                                          }).ToList();





                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);




                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);


                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


                // Summary Calculations

                decimal ExtraSum = this.DataSource.Sum(c => c.ExtraDropCharges.ToDecimal());
                string TotalExtraSum = string.Format("{0:c}", ExtraSum);
                TotalExtraSum = TotalExtraSum.Substring(1);
                TotalExtraSum = TotalExtraSum.Insert(0, "£ ");

                decimal JobsSum = this.DataSource.Sum(c => c.FareRate.ToDecimal());
                int jobsCnt = this.DataSource.Count;
                string jobsTotal = string.Format("{0:c}", JobsSum);
                jobsTotal = jobsTotal.Substring(1);
                jobsTotal = jobsTotal.Insert(0, "£ ");
                decimal zeroValue = 0.00m;
                string zeroStr = string.Format("{0:c}", zeroValue);
                zeroStr = zeroStr.Substring(1);
                zeroStr = zeroStr.Insert(0, "£ ");



                // Old Calculation 27 Nov 14

                string totalAccountBookings = this.DataSource.Count(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).ToStr();
                decimal totalAccountCharges = this.DataSource.Where(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => c.FareRate.ToDecimal());
                string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
                totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

                decimal totalCashBooking = this.DataSource.Count(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).ToDecimal();
                decimal SumofTotalCashBookingChrgs = this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal());
                string totalCashBookingCharges = string.Format("{0:c}", this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal()));
                totalCashBookingCharges = totalCashBookingCharges.Substring(1);
                totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");


                decimal SumOfAccountandCashJobTotal = totalAccountCharges + SumofTotalCashBookingChrgs;
                string TotalSumOfAccountandCashJobTotal = string.Format("{0:c}", SumOfAccountandCashJobTotal);
                TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Substring(1);
                TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Insert(0, "£ ");



                decimal totalCommission = 0.00m;

                if (AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool())
                {
                    if (NoACCommission == false)
                    {
                        totalCommission = this.DataSource.Sum(c => c.IsCommissionWise == true ? (c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission) : ((c.FareRate * Commision) / 100)).ToDecimal();

                       // totalCommission = this.DataSource.Sum(c => c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();
                    }
                    else
                    {
                        totalCommission = this.DataSource.Where(c => c.CompanyId == null)
                                    .Sum(c => c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();


                    }

                }
                else
                {
                    if (Commision > 0)
                    {
                        if (NoACCommission == false)
                        {
                           
                                totalCommission = this.DataSource.Sum(c => ((c.TotalCharges * Commision) / 100)).ToDecimal();

                           
                           
                        }
                        else
                        {

                            totalCommission = this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * Commision) / 100) : c.DriverCommission).ToDecimal();
                        }
                    }
                    else
                    {
                        if (NoACCommission == false)
                        {
                            totalCommission = this.DataSource.Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();
                        }
                        else
                        {
                            totalCommission = this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();


                        }

                    }
                }


                var list2 = (from a in new Taxi_Model.TaxiDataContext().stp_VehicleUsage(fromdate, todate, driverid, companyid, statusid)
                             select new stp_VehicleUsageResult
                             {
                                 VehicleType = a.VehicleType,
                                 VehicleNo = a.VehicleNo,
                                 Accbkgs = a.Accbkgs,
                                 AccTotat = a.AccTotat,
                                 CashBkgs = a.CashBkgs,
                                 CashTotal = a.CashTotal,
                                 Booking = a.Booking,
                                 Total = a.Total
                             }).ToList();


                ReportDataSource courierDataSource = new ReportDataSource("Taxi_Model_stp_VehicleUsageResult", list2);
                this.reportViewer1.LocalReport.DataSources.Add(courierDataSource);

                string driverTotalCommission = string.Format("{0:c}", totalCommission);

                driverTotalCommission = driverTotalCommission.Substring(1);
                driverTotalCommission = driverTotalCommission.Insert(0, "£ ");


                string totalSettingsCommission = driverTotalCommission;


                decimal agentFeesTotal=this.DataSource.Sum(c=>c.AgentCommission.ToDecimal().ToDecimal());

                string driverOwedRate = "£ " + string.Format("{0:c}", totalCommission).Substring(1);

                if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.CashStatement)
                {
                    driverTotalCommission = "£ 0.00";
                    driverOwedRate = jobsTotal;
                }
                else if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.Both
                    || StatementType == Taxi_AppMain.frmDriverReport.eStatementType.AccountStatement)
                {


                    driverOwedRate = "£ " + string.Format("{0:#.##}", ((agentFeesTotal + PDARent +totalCommission) - totalAccountCharges)  );
                }



                this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);


                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", TotalSumOfAccountandCashJobTotal);
                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions", driverTotalCommission);
                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", TotalSumOfAccountandCashJobTotal);
                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);

                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate);

                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());
                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", totalSettingsCommission);
                param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PickCommissionFromCharges", pickCommissionFromCharges);
                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalExtraDrop", TotalExtraSum);

                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_CommissionWise", (Commision).ToStr());

                // this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_NoACComm", AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool() ? "1" : "0");

                var query = General.GetObject<Fleet_Driver>(c => c.Id == driverid);
                string PCODRiverExpiryDate = string.Format("{0:dd/MM/yyyy}", query.PCODriverExpiryDate.ToDateorNull());
                string PCOVehicleExpiryDate = string.Format("{0:dd/MM/yyyy}", query.PCOVehicleExpiryDate.ToDateorNull());
                string MOT2ExpiryDate = string.Format("{0:dd/MM/yyyy}", query.MOT2ExpiryDate.ToDateorNull());
                string MOTExpiryDate = string.Format("{0:dd/MM/yyyy}", query.MOTExpiryDate.ToDateorNull());
                string InsuranceExpiryDate = string.Format("{0:dd/MM/yyyy}", query.InsuranceExpiryDate.ToDateorNull());
                string LicenseExpiryDate = string.Format("{0:dd/MM/yyyy}", query.DrivingLicenseExpiryDate.ToDateorNull());
                string RoadTaxiExpiryDate = string.Format("{0:dd/MM/yyyy}", query.RoadTaxiExpiryDate.ToDateorNull());




                param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCODRiverExpiryDate", PCODRiverExpiryDate);
                param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCOVehicleExpiryDate", PCOVehicleExpiryDate);
                param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOT2ExpiryDate", MOT2ExpiryDate);
                param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOTExpiryDate", MOTExpiryDate);
                param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InsuranceExpiryDate", InsuranceExpiryDate);
                param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_LicenseExpiryDate", LicenseExpiryDate);
                param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RoadTaxExpiryDate", RoadTaxiExpiryDate);


                param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RangeWise", "0");


                param[31] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverPDARent", this.PDARent.ToStr());



                reportViewer1.LocalReport.SetParameters(param);



                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                //   }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }



        public void LoadDriverStatementReport(DateTime fromdate, DateTime todate, int? driverid, int? companyid, int statusid)
        {
            try
            {
           
               // string className = "Taxi_AppMain.ReportDesigns.Template2_rptDriverCommissionStatement.rdlc";


                string className = TempPath;


                this.reportViewer1.LocalReport.ReportEmbeddedResource = className;
                    string pickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool() == true ? "1" : "0";
                    reportViewer1.LocalReport.EnableExternalImages = true;

                    Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[32];

                    this.stp_DriverCommisionResultBindingSource.DataSource = (from a in new Taxi_Model.TaxiDataContext().stp_DriverCommision(fromdate, todate, driverid, companyid, statusid,0)
                                                                              select new stp_DriverCommisionResult
                                                                              {
                                                                                  Id = a.Id,
                                                                                  AcceptedDateTime = a.AcceptedDateTime,
                                                                                  AccountTypeId = a.AccountTypeId,
                                                                                  AgentCommission = a.AgentCommission,
                                                                                  AgentCommissionPercent = a.AgentCommissionPercent,
                                                                                  ArrivalDateTime = a.ArrivalDateTime,
                                                                                  AuthCode = a.AuthCode,
                                                                                  BookingStatusId = a.BookingStatusId,
                                                                                  BookingTypeId = a.BookingTypeId,
                                                                                  ClearedDateTime = a.ClearedDateTime,
                                                                                  CompanyCode = a.CompanyCode,
                                                                                  CompanyPrice = a.CompanyPrice,
                                                                                  CongtionCharges = a.CongtionCharges,
                                                                                  CostCenterId = a.CostCenterId,
                                                                                  CostCenterName = a.CostCenterName,
                                                                                  CustomerPrice = a.CustomerPrice,
                                                                                  DepartmentId = a.DepartmentId,
                                                                                  DepartmentName = a.DepartmentName,
                                                                                  Despatchby = a.Despatchby,
                                                                                  DriverAddress = a.DriverAddress,
                                                                                  DriverCommission = a.DriverCommission,
                                                                                  DriverCommissionType = a.DriverCommissionType,
                                                                                  DriverFullName = a.DriverFullName,
                                                                                  DriverId = a.DriverId,
                                                                                  FromDoorNo = a.FromDoorNo,
                                                                                  FromLocType = a.FromLocType,
                                                                                  FromStreet = a.FromStreet,
                                                                                  IsCommissionWise = a.IsCommissionWise,
                                                                                  JobTakenByCompany = a.JobTakenByCompany,
                                                                                  MeetAndGreetCharges = a.MeetAndGreetCharges,
                                                                                  NoofHandLuggages = a.NoofHandLuggages,
                                                                                  PaymentType = a.PaymentType,
                                                                                  POBDateTime = a.POBDateTime,
                                                                                  PupilNo = a.PupilNo,
                                                                                  ReturnDriverFullName = a.ReturnDriverFullName,
                                                                                  ReturnDriverId = a.ReturnDriverId,
                                                                                  ReturnFareRate = a.ReturnFareRate,
                                                                                  ReturnPickupDateTime = a.ReturnPickupDateTime,
                                                                                  StatusName = a.StatusName,
                                                                                  STCDateTime = a.STCDateTime,
                                                                                  SubCompanyId = a.SubCompanyId,
                                                                                  ToDoorNo = a.ToDoorNo,
                                                                                  ToLocType = a.ToLocType,
                                                                                  ToStreet = a.ToStreet,
                                                                                  TotalTravelledMiles = a.TotalTravelledMiles,
                                                                                  VehicleTypeId = a.VehicleTypeId,
                                                                                  BookingDate = a.BookingDate,
                                                                                  BookingNo = a.BookingNo,
                                                                                  CompanyId = a.CompanyId,
                                                                                  CompanyName = a.CompanyName,
                                                                                  CustomerMobileNo = a.CustomerMobileNo,
                                                                                  CustomerName = a.CustomerName,
                                                                                  CustomerPhoneNo = a.CustomerPhoneNo,
                                                                                  DriverName = a.DriverName,
                                                                                  DriverNo = a.DriverNo,
                                                                                  ExtraDropCharges = a.ExtraDropCharges,
                                                                                  FareRate = a.FareRate,
                                                                                  FromAddress = a.FromAddress,
                                                                                  NoofLuggages = a.NoofLuggages,
                                                                                  NoofPassengers = a.NoofPassengers,
                                                                                  OrderNo = a.OrderNo,
                                                                                  ParkingCharges = a.ParkingCharges,
                                                                                  PaymentTypeId = a.PaymentTypeId,
                                                                                  PickupDateTime = a.PickupDateTime,
                                                                                  SpecialRequirements = a.SpecialRequirements,
                                                                                  ToAddress = a.ToAddress,
                                                                                  TotalCharges = a.TotalCharges,
                                                                                  WaitingCharges = a.WaitingCharges,
                                                                                  Via1 = a.Via1,
                                                                                  VehicleType = a.VehicleType
                                                                              }).ToList();





                    string address = AppVars.objSubCompany.Address;
                    string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                  
                    param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                    param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);




                    List<ClsLogo> objLogo = new List<ClsLogo>();
                    objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                    ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                    this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);


                    string path = @"File:";
                    param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                    param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


                    // Summary Calculations

                    decimal ExtraSum = this.DataSource.Sum(c => c.ExtraDropCharges.ToDecimal());
                    string TotalExtraSum = string.Format("{0:c}", ExtraSum);
                    TotalExtraSum = TotalExtraSum.Substring(1);
                    TotalExtraSum = TotalExtraSum.Insert(0, "£ ");

                    decimal JobsSum = this.DataSource.Sum(c => c.TotalCharges.ToDecimal());
                    int jobsCnt = this.DataSource.Count;
                    string jobsTotal = string.Format("{0:c}", JobsSum);
                    jobsTotal = jobsTotal.Substring(1);
                    jobsTotal = jobsTotal.Insert(0, "£ ");
                    decimal zeroValue = 0.00m;
                    string zeroStr = string.Format("{0:c}", zeroValue);
                    zeroStr = zeroStr.Substring(1);
                    zeroStr = zeroStr.Insert(0, "£ ");



                    // Old Calculation 27 Nov 14

                    string totalAccountBookings = this.DataSource.Count(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).ToStr();
                    decimal totalAccountCharges = this.DataSource.Where(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => c.TotalCharges.ToDecimal());
                   
                
                    string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
                    totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

                    decimal totalCashBooking = this.DataSource.Count(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).ToDecimal();
                    decimal SumofTotalCashBookingChrgs = this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal());
                    string totalCashBookingCharges = string.Format("{0:c}", this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal()));
                    totalCashBookingCharges = totalCashBookingCharges.Substring(1);
                    totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");


                    decimal SumOfAccountandCashJobTotal = totalAccountCharges + SumofTotalCashBookingChrgs;
                    string TotalSumOfAccountandCashJobTotal = string.Format("{0:c}", SumOfAccountandCashJobTotal);
                    TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Substring(1);
                    TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Insert(0, "£ ");



                    decimal totalCommission = 0.00m;

                    if (AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool())
                    {
                        if (NoACCommission == false)
                        {
                            totalCommission = this.DataSource.Sum(c => c.IsCommissionWise == true ? (c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission) : ((c.FareRate * Commision) / 100)).ToDecimal();

                          //  totalCommission = this.DataSource.Sum(c => c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();
                        }
                        else
                        {
                            totalCommission = this.DataSource.Where(c => c.CompanyId == null)
                                        .Sum(c => c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();


                        }

                    }
                    else
                    {
                        if (Commision > 0)
                        {
                            if (NoACCommission == false)
                            {
                                if (this.TempPath.ToLower().Contains("template4"))
                                {
                                    totalCommission = this.DataSource.Sum(c =>  ((c.TotalCharges * Commision) / 100)).ToDecimal();

                                }
                                else
                                {

                                    totalCommission = this.DataSource.Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * Commision) / 100) : c.DriverCommission).ToDecimal();

                                }
                            }
                            else
                            {
                              
                                 totalCommission = this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * Commision) / 100) : c.DriverCommission).ToDecimal();


                               




                            }
                        }
                        else
                        {
                            if (NoACCommission == false)
                            {
                                totalCommission = this.DataSource.Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();
                            }
                            else
                            {
                                totalCommission = this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();


                            }

                        }
                    }
            

                    var list2 = (from a in new Taxi_Model.TaxiDataContext().stp_VehicleUsage(fromdate, todate, driverid, companyid, statusid)
                                 select new stp_VehicleUsageResult
                                 {
                                     VehicleType = a.VehicleType,
                                     VehicleNo = a.VehicleNo,
                                     Accbkgs = a.Accbkgs,
                                     AccTotat = a.AccTotat,
                                     CashBkgs = a.CashBkgs,
                                     CashTotal = a.CashTotal,
                                     Booking = a.Booking,
                                     Total = a.Total
                                 }).ToList();
                   
                
                    ReportDataSource courierDataSource = new ReportDataSource("Taxi_Model_stp_VehicleUsageResult", list2);
                    this.reportViewer1.LocalReport.DataSources.Add(courierDataSource);

                    string driverTotalCommission = string.Format("{0:c}", totalCommission);

                    driverTotalCommission = driverTotalCommission.Substring(1);
                    driverTotalCommission = driverTotalCommission.Insert(0, "£ ");


                    string totalSettingsCommission = driverTotalCommission;


                    string driverOwedRate = "£ " + string.Format("{0:c}", totalCommission).Substring(1);

                    if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.CashStatement)
                    {
                        driverTotalCommission = "£ 0.00";
                        driverOwedRate = jobsTotal;
                    }
                    else if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.Both
                        || StatementType == Taxi_AppMain.frmDriverReport.eStatementType.AccountStatement)
                    {


                        driverOwedRate = "£ " + string.Format("{0:#.##}", totalCommission - (totalAccountCharges + PDARent));
                    }



                    this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                    param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

                    param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
                    param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
                    param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);


                    param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
                    param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
                    param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

                    param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", TotalSumOfAccountandCashJobTotal);
                    param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions", driverTotalCommission);
                    param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", TotalSumOfAccountandCashJobTotal);
                    param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);

                    param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate);

                    param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
                    param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());
                    param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", totalSettingsCommission);
                    param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PickCommissionFromCharges", pickCommissionFromCharges);
                    param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalExtraDrop", TotalExtraSum);

                    param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_CommissionWise", (Commision).ToStr());

                    // this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                    param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_NoACComm", AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool() ? "1" : "0");

                    var query = General.GetObject<Fleet_Driver>(c => c.Id == driverid);
                    string PCODRiverExpiryDate = string.Format("{0:dd/MM/yyyy}", query.PCODriverExpiryDate.ToDateorNull());
                    string PCOVehicleExpiryDate = string.Format("{0:dd/MM/yyyy}", query.PCOVehicleExpiryDate.ToDateorNull());
                    string MOT2ExpiryDate = string.Format("{0:dd/MM/yyyy}", query.MOT2ExpiryDate.ToDateorNull()); 
                    string MOTExpiryDate = string.Format("{0:dd/MM/yyyy}", query.MOTExpiryDate.ToDateorNull()); 
                    string InsuranceExpiryDate = string.Format("{0:dd/MM/yyyy}", query.InsuranceExpiryDate.ToDateorNull());
                    string LicenseExpiryDate = string.Format("{0:dd/MM/yyyy}", query.DrivingLicenseExpiryDate.ToDateorNull()); 
                    string RoadTaxiExpiryDate = string.Format("{0:dd/MM/yyyy}", query.RoadTaxiExpiryDate.ToDateorNull()); 
               



                    param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCODRiverExpiryDate",PCODRiverExpiryDate);
                    param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCOVehicleExpiryDate", PCOVehicleExpiryDate);
                    param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOT2ExpiryDate",  MOT2ExpiryDate);
                    param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOTExpiryDate", MOTExpiryDate);
                    param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InsuranceExpiryDate",  InsuranceExpiryDate);
                    param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_LicenseExpiryDate",  LicenseExpiryDate);
                    param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RoadTaxExpiryDate",  RoadTaxiExpiryDate);


                    param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RangeWise", "0");


                    param[31] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverPDARent",this.PDARent.ToStr() );

                
                
                    reportViewer1.LocalReport.SetParameters(param);



                    this.reportViewer1.ZoomPercent = 100;
                    this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                    this.reportViewer1.RefreshReport();
             //   }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }



        public void LoadDriverStatementReportRangeWiseComm(DateTime fromdate, DateTime todate, int? driverid, int? companyid, int statusid)
        {
            try
            {

               // string className = "Taxi_AppMain.ReportDesigns.Template2_rptDriverCommissionStatement.rdlc";

                string className = TempPath;

                this.reportViewer1.LocalReport.ReportEmbeddedResource = className;
                string pickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool() == true ? "1" : "0";
                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[32];

                this.stp_DriverCommisionResultBindingSource.DataSource = this.DataSource;




                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);




                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);


                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


                // Summary Calculations

                decimal ExtraSum = this.DataSource.Sum(c => c.ExtraDropCharges.ToDecimal());
                string TotalExtraSum = string.Format("{0:c}", ExtraSum);
                TotalExtraSum = TotalExtraSum.Substring(1);
                TotalExtraSum = TotalExtraSum.Insert(0, "£ ");

                decimal JobsSum = this.DataSource.Sum(c => c.TotalCharges.ToDecimal());
                int jobsCnt = this.DataSource.Count;
                string jobsTotal = string.Format("{0:c}", JobsSum);
                jobsTotal = jobsTotal.Substring(1);
                jobsTotal = jobsTotal.Insert(0, "£ ");
                decimal zeroValue = 0.00m;
                string zeroStr = string.Format("{0:c}", zeroValue);
                zeroStr = zeroStr.Substring(1);
                zeroStr = zeroStr.Insert(0, "£ ");



                // Old Calculation 27 Nov 14

                string totalAccountBookings = this.DataSource.Count(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).ToStr();
                decimal totalAccountCharges = this.DataSource.Where(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => c.TotalCharges.ToDecimal());
                string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
                totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

                decimal totalCashBooking = this.DataSource.Count(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).ToDecimal();
                decimal SumofTotalCashBookingChrgs = this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal());
                string totalCashBookingCharges = string.Format("{0:c}", this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal()));
                totalCashBookingCharges = totalCashBookingCharges.Substring(1);
                totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");


                decimal SumOfAccountandCashJobTotal = totalAccountCharges + SumofTotalCashBookingChrgs;
                string TotalSumOfAccountandCashJobTotal = string.Format("{0:c}", SumOfAccountandCashJobTotal);
                TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Substring(1);
                TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Insert(0, "£ ");



                decimal totalCommission = 0.00m;

                if (AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool())
                {
                    if (NoACCommission == false)
                    {
                        totalCommission = this.DataSource.Sum(c => c.IsCommissionWise.ToBool() ? (c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission) : (((c.FareRate * listofCommRange.FirstOrDefault(a=>c.FareRate>=a.FromPrice && c.FareRate<=a.ToPrice).DefaultIfEmpty().CommissionValue.ToDecimal()) / 100) )).ToDecimal();
                    }
                    else
                    {
                        totalCommission = this.DataSource.Where(c => c.CompanyId == null)
                                  .Sum(c => c.IsCommissionWise.ToBool() ? (c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission) : (((c.FareRate * listofCommRange.FirstOrDefault(a => c.FareRate >= a.FromPrice && c.FareRate <= a.ToPrice).DefaultIfEmpty().CommissionValue.ToDecimal()) / 100))).ToDecimal();

                    }

                }
                else
                {
                   if (NoACCommission == false)
                        {
                            totalCommission = this.DataSource
                                  .Sum(c => c.IsCommissionWise.ToBool() ? (c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission) : (((c.TotalCharges * listofCommRange.FirstOrDefault(a => c.TotalCharges >= a.FromPrice && c.TotalCharges <= a.ToPrice).DefaultIfEmpty().CommissionValue.ToDecimal()) / 100))).ToDecimal();
                        }
                        else
                        {
                            totalCommission = this.DataSource.Where(c => c.CompanyId == null)
                                  .Sum(c => c.IsCommissionWise.ToBool() ? (c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission) : (((c.TotalCharges * listofCommRange.FirstOrDefault(a => c.TotalCharges >= a.FromPrice && c.TotalCharges <= a.ToPrice).DefaultIfEmpty().CommissionValue.ToDecimal()) / 100))).ToDecimal();


                        }                 
                }


                var list2 = (from a in new Taxi_Model.TaxiDataContext().stp_VehicleUsage(fromdate, todate, driverid, companyid, statusid)
                             select new stp_VehicleUsageResult
                             {
                                 VehicleType = a.VehicleType,
                                 VehicleNo = a.VehicleNo,
                                 Accbkgs = a.Accbkgs,
                                 AccTotat = a.AccTotat,
                                 CashBkgs = a.CashBkgs,
                                 CashTotal = a.CashTotal,
                                 Booking = a.Booking,
                                 Total = a.Total
                             }).ToList();


                ReportDataSource courierDataSource = new ReportDataSource("Taxi_Model_stp_VehicleUsageResult", list2);
                this.reportViewer1.LocalReport.DataSources.Add(courierDataSource);

                string driverTotalCommission = string.Format("{0:c}", totalCommission);

                driverTotalCommission = driverTotalCommission.Substring(1);
                driverTotalCommission = driverTotalCommission.Insert(0, "£ ");


                string totalSettingsCommission = driverTotalCommission;


                string driverOwedRate = "£ " + string.Format("{0:c}", totalCommission).Substring(1);

                if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.CashStatement)
                {
                    driverTotalCommission = "£ 0.00";
                    driverOwedRate = jobsTotal;
                }
                else if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.Both
                    || StatementType == Taxi_AppMain.frmDriverReport.eStatementType.AccountStatement)
                {


                    driverOwedRate = "£ " + string.Format("{0:#.##}", totalCommission - (totalAccountCharges+ PDARent));
                }
                this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);


                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", TotalSumOfAccountandCashJobTotal);
                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions", driverTotalCommission);
                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", TotalSumOfAccountandCashJobTotal);
                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);

                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate);

                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());
                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", totalSettingsCommission);
                param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PickCommissionFromCharges", pickCommissionFromCharges);
                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalExtraDrop", TotalExtraSum);

                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_CommissionWise", (Commision).ToStr());

                // this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_NoACComm", AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool() ? "1" : "0");

                var query = General.GetObject<Fleet_Driver>(c => c.Id == driverid);
                string PCODRiverExpiryDate = string.Format("{0:dd/MM/yyyy}", query.PCODriverExpiryDate.ToDateorNull());
                string PCOVehicleExpiryDate = string.Format("{0:dd/MM/yyyy}", query.PCOVehicleExpiryDate.ToDateorNull());
                string MOT2ExpiryDate = string.Format("{0:dd/MM/yyyy}", query.MOT2ExpiryDate.ToDateorNull());
                string MOTExpiryDate = string.Format("{0:dd/MM/yyyy}", query.MOTExpiryDate.ToDateorNull());
                string InsuranceExpiryDate = string.Format("{0:dd/MM/yyyy}", query.InsuranceExpiryDate.ToDateorNull());
                string LicenseExpiryDate = string.Format("{0:dd/MM/yyyy}", query.DrivingLicenseExpiryDate.ToDateorNull());
                string RoadTaxiExpiryDate = string.Format("{0:dd/MM/yyyy}", query.RoadTaxiExpiryDate.ToDateorNull());

                param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCODRiverExpiryDate", PCODRiverExpiryDate);
                param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCOVehicleExpiryDate", PCOVehicleExpiryDate);
                param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOT2ExpiryDate", MOT2ExpiryDate);
                param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOTExpiryDate", MOTExpiryDate);
                param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InsuranceExpiryDate", InsuranceExpiryDate);
                param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_LicenseExpiryDate", LicenseExpiryDate);
                param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RoadTaxExpiryDate", RoadTaxiExpiryDate);

                param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RangeWise", "1");


                param[31] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverPDARent", this.PDARent.ToStr());


                reportViewer1.LocalReport.SetParameters(param);

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                //   }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(this.reportViewer1.LocalReport.DataSources[2]);
        }
        public void ExportReport()
        {

            Microsoft.Reporting.WinForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer1.LocalReport.Render(
             "Pdf", null, out mimeType, out encoding,
              out extension,
             out streamids, out warnings);
            

            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Driver Statement";
          
         //   saveFileDlg.RestoreDirectory = false;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
              
            
                try
                {
                    FileStream fs = new FileStream(saveFileDlg.FileName,FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


                

        }



       

        private void btnViewReport_Click(object sender, EventArgs e)
        {

          
        }

        private void rptfrmDriverStatement_Load(object sender, EventArgs e)
        {

        }

        private void rptfrmDriverCommissionStatement_Load(object sender, EventArgs e)
        {

        }


        public void SendEmail()
        {

            General.ShowEmailForm(reportViewer1,"Driver Commission Report");

        }
     

     
    }
}
