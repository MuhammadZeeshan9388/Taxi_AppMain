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
using System.Threading;
using System.Diagnostics;

namespace Taxi_AppMain.Forms
{
    public partial class frmNotification : UI.SetupBase
    {
    //    DriverBO objMaster;
        int PHCVehicleDays = 0;
        int PHCDriverDays = 0;
        int MOTDays = 0;
        int InsuranceDays = 0;
        int MOT2Days = 0;
        int LicenseDays = 0;
        int RoadTaxDays = 0;


        bool IsServerMachine = false;

        public frmNotification(int MOTDays, int MOT2Days, int PHCDriverDays, int PHCVehicleDays, int LicenseDays, int InsuranceDays, int RoadTaxDays,bool IsServer)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmNotification_Load);

            this.MOTDays = MOTDays;
            this.MOT2Days = MOT2Days;
            this.PHCDriverDays = PHCDriverDays;
            this.PHCVehicleDays = PHCVehicleDays;
            this.LicenseDays = LicenseDays;
            this.InsuranceDays = InsuranceDays;
            this.RoadTaxDays = RoadTaxDays;
            this.IsServerMachine = IsServer;


        }
        void frmNotification_Load(object sender, EventArgs e)
        {
            PopulateData();




            if (IsServerMachine && AppVars.objPolicyConfiguration.DisableDrvDocumentExpirySMS.ToBool() == false)
            {

                TimeSpan fromTime = new TimeSpan(9, 0, 0);
                TimeSpan tillTime = new TimeSpan(21, 0, 0);


                TimeSpan t = DateTime.Now.TimeOfDay;


                if (t >= fromTime && t <= tillTime)
                {

                    SendNotificationToDriver();
                }
                else
                    EnableSMSNotificationButton();
            }
            else
            {
                EnableSMSNotificationButton();


            }


        }

        public override void PopulateData()
        {

            LoadDriverList();

        }

        DateTime? dtNow = null;
        DateTime? dateVar = null;

        private void LoadDriverList()
        {

            try
            {

                dtNow = DateTime.Now.ToDate();

                dateVar = DateTime.Now.AddDays(AppVars.objPolicyConfiguration.DriverExpiryNoticeInDays.ToInt());
                //var data1 = General.GetQueryable<Fleet_Driver>(c => c.MOTExpiryDate > dateVar ).AsEnumerable().OrderBy(item => item.DriverNo);
                var data1 = General.GetQueryable<Fleet_Driver>(c => c.IsActive == true
                    &&
                    (
                        //(c.MOTExpiryDate >= dtNow && c.MOTExpiryDate <= dateVar)
                        //|| (c.MOT2ExpiryDate >= dtNow && c.MOT2ExpiryDate <= dateVar)
                        //|| (c.PCODriverExpiryDate >= dtNow && c.PCODriverExpiryDate <= dateVar)
                        //|| (c.PCOVehicleExpiryDate >= dtNow && c.PCOVehicleExpiryDate <= dateVar)
                        //|| (c.DrivingLicenseExpiryDate >= dtNow && c.DrivingLicenseExpiryDate <= dateVar)
                        // || (c.InsuranceExpiryDate >= dtNow && c.InsuranceExpiryDate <= dateVar)
                        //   || (c.RoadTaxiExpiryDate >= dtNow && c.RoadTaxiExpiryDate <= dateVar)

                         (c.MOTExpiryDate >= dtNow && c.MOTExpiryDate <= dtNow.Value.AddDays(MOTDays))
                                            || (c.MOT2ExpiryDate >= dtNow && c.MOT2ExpiryDate <=  dtNow.Value.AddDays(MOT2Days))
                                            || (c.PCODriverExpiryDate >= dtNow && c.PCODriverExpiryDate <=   dtNow.Value.AddDays(PHCDriverDays))
                                            || (c.PCOVehicleExpiryDate >= dtNow && c.PCOVehicleExpiryDate <=   dtNow.Value.AddDays(PHCVehicleDays))
                                            || (c.DrivingLicenseExpiryDate >= dtNow && c.DrivingLicenseExpiryDate <=   dtNow.Value.AddDays(LicenseDays))
                                            || (c.InsuranceExpiryDate >= dtNow && c.InsuranceExpiryDate <=   dtNow.Value.AddDays(InsuranceDays))
                                         || (c.RoadTaxiExpiryDate >= dtNow && c.RoadTaxiExpiryDate <=   dtNow.Value.AddDays(RoadTaxDays))
                  )
                    

                    ).AsEnumerable().OrderBy(item => item.DriverNo);


                var query = (from a in data1


                             select new
                             {
                                 Id = a.Id,
                                 No = a.DriverNo,
                                 Name = a.DriverName,
                                 VehicleNo = a.VehicleNo,
                                 MOTExpiry = a.MOTExpiryDate,
                                 MOT2Expiry = a.MOT2ExpiryDate,
                                 PCOVehicleExpiry = a.PCOVehicleExpiryDate,
                                 InsuranceExpiry = a.InsuranceExpiryDate,
                                 PCODriverExpiry = a.PCODriverExpiryDate,
                                 LicenseExpiry = a.DrivingLicenseExpiryDate,
                                 RoadTaxExpiry = a.RoadTaxiExpiryDate,
                                 MobileNo = a.MobileNo
                             //    HasPda = a.HasPDA
                                 //  EndDate = a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate

                             }).AsQueryable();
          
                
                
                lblTotalDrivers.Text = "Total Due Driver(s) : " + query.Count();
                grdLister.DataSource = query.ToList();

             //   grdLister.Columns["HasPda"].IsVisible = false;
                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["Name"].Width = 100;
                grdLister.Columns["VehicleNo"].Width = 60;
                grdLister.Columns["MOTExpiry"].Width = 140;
                grdLister.Columns["MOT2Expiry"].Width = 130;
                grdLister.Columns["PCOVehicleExpiry"].Width = 150;
                grdLister.Columns["InsuranceExpiry"].Width = 150;
                grdLister.Columns["LicenseExpiry"].Width = 140;
                grdLister.Columns["PCODriverExpiry"].Width = 150;
                grdLister.Columns["MobileNo"].Width = 110;

                grdLister.Columns["VehicleNo"].HeaderText = "Veh No";
                grdLister.Columns["MOTExpiry"].HeaderText = "MOT Expiry";
                grdLister.Columns["MOT2Expiry"].HeaderText = "MOT2 Expiry";

                grdLister.Columns["RoadTaxExpiry"].IsVisible = false;

                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "pinkberrycars")
                {

                    grdLister.Columns["MOT2Expiry"].IsVisible = false;
                    grdLister.Columns["RoadTaxExpiry"].HeaderText = "Road Tax Expiry";
                    grdLister.Columns["RoadTaxExpiry"].Width = 140;


                }


                grdLister.Columns["PCOVehicleExpiry"].HeaderText = "PCO Vehicle Expiry";
                grdLister.Columns["InsuranceExpiry"].HeaderText = "Insurance Expiry";
                grdLister.Columns["LicenseExpiry"].HeaderText = "License Expiry";
                grdLister.Columns["PCODriverExpiry"].HeaderText = "PCO Driver Expiry";
                grdLister.Columns["MobileNo"].HeaderText = "Mobile No";


                grdLister.CurrentRow = null;


              
            }
            catch (Exception ex)
            {


            }

        }


        List<GridViewRowInfo> listofRows = null;
        private void SendNotificationToDriver()
        {
            try
            {

                listofRows = grdLister.Rows.ToList();
                string message = string.Empty;

                DateTime dtTimeNow = DateTime.Now;

                new Thread(delegate()
                {

                    for (int i = 0; i < listofRows.Count; i++)
                    {
                        if (listofRows[i].Cells["MobileNo"].Value.ToStr().Trim() == string.Empty)
                            continue;



                        message = "Document Expiring Alert!";


                        if (listofRows[i].Cells["MOTExpiry"].Value != null && listofRows[i].Cells["MOTExpiry"].Value.ToDateTime() >= dtNow && listofRows[i].Cells["MOTExpiry"].Value.ToDateTime() <= dtNow.Value.AddDays(MOTDays))
                        {
                            message += Environment.NewLine + "MOT is Expiring on : " + string.Format("{0:dd/MM/yyyy}", listofRows[i].Cells["MOTExpiry"].Value);

                        }

                        if (listofRows[i].Cells["MOT2Expiry"].Value != null && listofRows[i].Cells["MOT2Expiry"].Value.ToDateTime() >= dtNow && listofRows[i].Cells["MOT2Expiry"].Value.ToDateTime() <= dtNow.Value.AddDays(MOT2Days))
                        {
                            message += Environment.NewLine + "MOT2 is Expiring on " + string.Format("{0:dd/MM/yyyy}", listofRows[i].Cells["MOT2Expiry"].Value);

                        }


                        if (listofRows[i].Cells["PCOVehicleExpiry"].Value != null && listofRows[i].Cells["PCOVehicleExpiry"].Value.ToDateTime() >= dtNow && listofRows[i].Cells["PCOVehicleExpiry"].Value.ToDateTime() <= dtNow.Value.AddDays(PHCVehicleDays))
                        {
                            message += Environment.NewLine + "PHC Vehicle is Expiring on " + string.Format("{0:dd/MM/yyyy}", listofRows[i].Cells["PCOVehicleExpiry"].Value);

                        }

                    

                        if (listofRows[i].Cells["InsuranceExpiry"].Value != null && listofRows[i].Cells["InsuranceExpiry"].Value.ToDateTime() >= dtTimeNow && listofRows[i].Cells["InsuranceExpiry"].Value.ToDateTime() <= DateTime.Now.AddDays(InsuranceDays))
                        {
                            message += Environment.NewLine + "Insurance is Expiring on " + string.Format("{0:dd/MM/yyyy HH:mm}", listofRows[i].Cells["InsuranceExpiry"].Value);

                        }

                        if (listofRows[i].Cells["LicenseExpiry"].Value != null && listofRows[i].Cells["LicenseExpiry"].Value.ToDateTime() >= dtNow && listofRows[i].Cells["LicenseExpiry"].Value.ToDateTime() <= dtNow.Value.AddDays(LicenseDays))
                        {
                            message += Environment.NewLine + "License is Expiring on " + string.Format("{0:dd/MM/yyyy}", listofRows[i].Cells["LicenseExpiry"].Value);

                        }

                   

                        if (listofRows[i].Cells["PCODriverExpiry"].Value != null && listofRows[i].Cells["PCODriverExpiry"].Value.ToDateTime() >= dtNow && listofRows[i].Cells["PCODriverExpiry"].Value.ToDateTime() <= dtNow.Value.AddDays(PHCDriverDays))
                        {
                            message += Environment.NewLine + "PHC Driver is Expiring on " + string.Format("{0:dd/MM/yyyy}", listofRows[i].Cells["PCODriverExpiry"].Value);

                        }

                        if (listofRows[i].Cells["RoadTaxExpiry"].Value != null && listofRows[i].Cells["RoadTaxExpiry"].Value.ToDateTime() >= dtNow && listofRows[i].Cells["RoadTaxExpiry"].Value.ToDateTime() <= dtNow.Value.AddDays(RoadTaxDays))
                        {
                            message += Environment.NewLine + "Road Tax is Expiring on " + string.Format("{0:dd/MM/yyyy}", listofRows[i].Cells["RoadTaxExpiry"].Value);

                        }



                        SendSMS(listofRows[i].Cells["MobileNo"].Value.ToStr(), message);



                        if (i == listofRows.Count - 1)
                        {

                            MethodInvoker mi = new MethodInvoker(delegate()
                            {
                                EnableSMSNotificationButton();
                            });
                            this.Invoke(mi);

                        }

                    }


                    //IsSuccess1 = SendDespatchSMS(objSMS, GetMessage(AppVars.objPolicyConfiguration.DespatchTextForCustomer.ToStr()), customerMobileNo);
                }).Start();
            }
            catch (Exception ex)
            {


            }

        }

        private void EnableSMSNotificationButton()
        {
            btnSendSMS.Enabled = true;

        }


        private void SendSMS(string mobileNo, string message)
        {
            try
            {

                string rtnMsg = string.Empty;
                EuroSMS objSMS = new EuroSMS();
                objSMS.Message = message;


                string mobNo = mobileNo;


                if (Debugger.IsAttached == false)
                {

                    int idx = -1;
                    if (mobNo.StartsWith("044") == true)
                    {
                        idx = mobNo.IndexOf("044");
                        mobNo = mobNo.Substring(idx + 3);
                        mobNo = mobNo.Insert(0, "+44");
                    }

                    if (mobNo.StartsWith("07"))
                    {
                        mobNo = mobNo.Substring(1);
                    }

                    if (mobNo.StartsWith("044") == false || mobNo.StartsWith("+44") == false)
                        mobNo = mobNo.Insert(0, "+44");
                }

                objSMS.ToNumber = mobNo.Trim();
                objSMS.Send(ref rtnMsg);
            }
            catch (Exception ex)
            {


            }

        }



        //Expir Drivers
        private void LoadExpireDriverList()
        {
            try
            {
              

                DateTime nowDate = DateTime.Now.ToDate();
                var data1 = General.GetQueryable<Fleet_Driver>(c => c.MOTExpiryDate > nowDate || c.MOT2ExpiryDate > nowDate || c.DrivingLicenseExpiryDate > nowDate || c.PCODriverExpiryDate > nowDate || c.PCOVehicleExpiryDate > nowDate).AsEnumerable().OrderBy(item => item.DriverNo);

                var query = (from a in data1

                             where

                          (a.MOTExpiryDate != null)
                          || (a.MOT2ExpiryDate != null)
                          || (a.PCOVehicleExpiryDate != null)
                          || (a.InsuranceExpiryDate != null)
                          || (a.PCODriverExpiryDate != null)
                          || (a.LicenseExpiryDate != null)
                             select new
                             {
                                 Id = a.Id,
                                 No = a.DriverNo,
                                 Name = a.DriverName,
                                 VehicleNo = a.VehicleNo,
                                 MOTExpiry = a.MOTExpiryDate,
                                 MOT2Expiry = a.MOT2ExpiryDate,
                                 PCOVehicleExpiry = a.PCOVehicleExpiryDate,
                                 InsuranceExpiry = a.InsuranceExpiryDate,
                                 PCODriverExpiry = a.PCODriverExpiryDate,
                                 LicenseExpiry = a.DrivingLicenseExpiryDate,
                                 MobileNo = a.MobileNo,
                                 EndDate = a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate

                             }).AsQueryable();
                lblTotalDrivers.Text = "Total Documents Expire Driver(s) : " + query.Count();
                grdLister.DataSource = query.ToList();

                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["Name"].Width = 110;
                grdLister.Columns["VehicleNo"].Width = 80;
                grdLister.Columns["MOTExpiry"].Width = 130;
                grdLister.Columns["MOT2Expiry"].Width = 130;


                grdLister.Columns["RoadTaxExpiry"].IsVisible = false;

                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "pinkberrycars")
                {
                    grdLister.Columns["MOT2Expiry"].IsVisible = false;
                    grdLister.Columns["RoadTaxExpiry"].IsVisible = true;
                    grdLister.Columns["RoadTaxExpiry"].Width = 140;
                }



               
                grdLister.Columns["PCOVehicleExpiry"].Width = 140;
                grdLister.Columns["InsuranceExpiry"].Width = 140;
                grdLister.Columns["LicenseExpiry"].Width = 130;
                grdLister.Columns["PCODriverExpiry"].Width = 130;
                grdLister.Columns["MobileNo"].Width = 90;
                grdLister.Columns["EndDate"].Width = 100;
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
            
          //  txtSearch2.Text = string.Empty;
            LoadExpireDriverList();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData();
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
           // txtSearch.Text = string.Empty;
            LoadDriverList();
        }

        private void btnDueDrivers_Click(object sender, EventArgs e)
        {
            LoadDriverList();
            //pnlDueDriverMain.Visible = true;
           // pnlExpiryDrivers.Visible = false;
        }

        private void btnExpiryDrivers_Click(object sender, EventArgs e)
        {
            LoadExpireDriverList();
        //   pnlExpiryDrivers.Visible = true;
          //  pnlDueDriverMain.Visible = false;
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            btnSendSMS.Enabled = false;
            SendNotificationToDriver();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

    }
}
