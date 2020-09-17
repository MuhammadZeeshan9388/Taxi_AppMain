using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;

using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;

using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmEditFare : Form
    {
        BookingBO ObjMaster;
        int openForm = 0;

        public decimal farerate;
        //public decimal customerprice;
        //public decimal companyprice;

        public frmEditFare(long ID, int open)
        {
            InitializeComponent();
            ObjMaster = new BookingBO();
            ObjMaster.GetByPrimaryKey(ID);
            DisplayRecord();
            

            openForm = open;
          
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
            decimal Fare = txtFare.Value;
            decimal custFare = txtCustFare.Value;
            decimal compFare = txtCompFare.Value;

            decimal oldFares = 0.00m;

            decimal oldTotalCharges = 0.00m;
            decimal TotalCharges = 0.00m;


                if (ObjMaster.Current != null)
                {
                bool allowUpdateTransaction = false;

                try
                    {

                        long id = ObjMaster.Current.Id;
                   
                    if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" && (c.functionId == "LOCK BOOKING AFTER TRANSACTION")) > 0)
                    {
                        int reasonCnt = 0;
                        string error = string.Empty;
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            if (db.Invoice_Charges.Count(c => c.BookingId == id) > 0)
                            {
                                reasonCnt++;
                                error = reasonCnt + ". Account Invoice Exist" + Environment.NewLine;
                            }

                            if (db.DriverRent_Charges.Count(c => c.BookingId == id) > 0)
                            {
                                reasonCnt++;
                                error += reasonCnt + ". Driver Rent Statement Exist" + Environment.NewLine;
                            }

                            if (db.Fleet_DriverCommision_Charges.Count(c => c.BookingId == id) > 0)
                            {
                                reasonCnt++;
                                error += reasonCnt + ". Driver Commission Statement Exist" + Environment.NewLine;
                            }
                        }

                        if (error.Length > 0)
                        {
                            ENUtils.ShowMessage("You cannot edit this booking" + Environment.NewLine + Environment.NewLine + "Reasons:-" + Environment.NewLine + error);
                            return;
                        }

                    }
                    else
                        allowUpdateTransaction = true;
                    }
                    catch
                    {


                    }



                    string pdafares = AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().ToLower().Trim();
                


                    ObjMaster.CheckDataValidation = false;
                ObjMaster.AllowUpdateTransaction = allowUpdateTransaction;
                    ObjMaster.Edit();

                    oldFares = ObjMaster.Current.FareRate.ToDecimal();
                    oldTotalCharges = ObjMaster.Current.TotalCharges.ToDecimal();


                    ObjMaster.Current.FareRate = Fare;
                    ObjMaster.Current.CustomerPrice = custFare;
                    ObjMaster.Current.CompanyPrice = compFare;

                    ObjMaster.DisableUpdateReturnJob = true;
                    ObjMaster.CheckCustomerValidation = false;

                    farerate = Fare;
                  

                    if (oldFares != Fare)
                    {
                        ObjMaster.Current.Booking_Logs.Add(new Booking_Log { BookingId = ObjMaster.Current.Id, User = AppVars.LoginObj.LoginName, BeforeUpdate = "Fares : "+oldFares, AfterUpdate = "Update from (Edit Fare) ,Fares : "+Fare, UpdateDate = DateTime.Now });


                        

                         General.UpdateOnlineBookingFares(ObjMaster.Current.OnlineBookingId.ToLong(), Fare,ObjMaster.Current.BookingTypeId.ToInt());
                        
                    
                    }


                    if (ObjMaster.Current.CompanyId == null)
                    {
                        ObjMaster.Current.TotalCharges = ObjMaster.Current.FareRate.ToDecimal()
                                     + ObjMaster.Current.ExtraDropCharges.ToDecimal() + ObjMaster.Current.MeetAndGreetCharges.ToDecimal() + ObjMaster.Current.CongtionCharges.ToDecimal();
                    }
                    else
                    {
                        ObjMaster.Current.TotalCharges = ObjMaster.Current.CompanyPrice.ToDecimal() + ObjMaster.Current.ParkingCharges.ToDecimal() + ObjMaster.Current.WaitingCharges.ToDecimal() + ObjMaster.Current.ExtraDropCharges.ToDecimal();

                    }


                    TotalCharges = ObjMaster.Current.TotalCharges.ToDecimal();

                    ObjMaster.Save();
                    this.Close();


               
                    //if (openForm == 1)
                    //{
                    //   // AppVars.frmMDI.RefreshDashBoardBookings();

                    //   // General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                    //}
                    if (openForm == 2)
                    {
                        (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshBookingList();
                       // AppVars.frmMDI.RefreshDashBoardBookings();
                    }
                    else if (openForm == 0)
                    {
                        General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");
                    }



                    // For TCP Connection
                    if (ObjMaster.Current.DriverId != null && ObjMaster.Current.Gen_PaymentType.ShowFaresOnPDA.ToStr()=="1"  &&
                        (ObjMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || ObjMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                        || ObjMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.POB || ObjMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.STC)
                        && AppVars.objPolicyConfiguration.IsListenAll.ToBool() && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim()))
                    {


                        if (pdafares == "totalcharges")
                        {

                            oldFares = oldTotalCharges;
                            Fare = TotalCharges;
                        }


                        if (oldFares != Fare)
                        {

                            string A_Phone = ObjMaster.Current.CustomerPhoneNo.ToStr();
                            string Phone = ObjMaster.Current.CustomerPhoneNo.ToStr();
                            string mobNo = ObjMaster.Current.CustomerMobileNo;
                            if (string.IsNullOrEmpty(mobNo))
                                mobNo = " ";


                            else if (!string.IsNullOrEmpty(A_Phone))
                            {
                                mobNo = Phone + "/" + A_Phone;
                            }


                            
                            string A_Account = ObjMaster.Current.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();

                                if (string.IsNullOrEmpty(A_Account))
                                {
                                    A_Account = " ";
                                }



                                string babySeats = ObjMaster.Current.BabySeats.ToStr().Trim();

                                if (string.IsNullOrEmpty(babySeats))
                                    babySeats = " ";


                                string A_journeyType = "O/W";

                                if (ObjMaster.Current.JourneyTypeId.ToInt()==Enums.JOURNEY_TYPES.WAITANDRETURN)
                                    A_journeyType = "W/R";


                                string A_Via = " ";

                                if (ObjMaster.Current.ViaString.ToStr().Trim().Length>0)
                                {
                                    int i = 1;
                                    A_Via = string.Join(" * ",ObjMaster.Current.Booking_ViaLocations.Select(c => "(" + i++.ToStr() + ")" + c.ViaLocValue.ToStr()).ToArray<string>());
                                }


                               

                     
                            string msg = (!string.IsNullOrEmpty(ObjMaster.Current.FromDoorNo) ? ObjMaster.Current.FromDoorNo + "-" + ObjMaster.Current.FromAddress + ObjMaster.Current.Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr() : ObjMaster.Current.FromAddress + ObjMaster.Current.Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr()) +
                           ">>" +
                           (!string.IsNullOrEmpty(ObjMaster.Current.ToDoorNo) ? ObjMaster.Current.ToDoorNo + "-" + ObjMaster.Current.ToAddress + ObjMaster.Current.Gen_Zone.DefaultIfEmpty().ZoneName.ToStr() : ObjMaster.Current.ToAddress + ObjMaster.Current.Gen_Zone.DefaultIfEmpty().ZoneName.ToStr()) +
                           ">>" +
                           string.Format("{0:dd/MM/yyyy   HH:mm}", ObjMaster.Current.PickupDateTime) +
                            ">>" +
                            ObjMaster.Current.CustomerName +
                            ">>" +
                            mobNo +
                            ">>" + ObjMaster.Current.SpecialRequirements.ToStr()+" "
                            + ">>" + Fare+
                            ">>" + ObjMaster.Current.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr()
                             + ">>" + A_Account
                           
                             + ">>" + A_journeyType
                             + ">>" + ObjMaster.Current.Gen_PaymentType.PaymentType.ToStr()
                             + ">>" + A_Via
                             + ">>" + ObjMaster.Current.NoofPassengers.ToInt()
                             + ">>" +ObjMaster.Current.NoofLuggages.ToInt()
                             + ">>" + babySeats;
                            
                            new Thread(delegate()
                            {
                                General.SendMessageToPDA("request pda=" + ObjMaster.Current.DriverId + "=" + ObjMaster.Current.Id + "=" + "Update Job>>" + ObjMaster.Current.DriverId + ">>" + ObjMaster.Current.Id + ">>" + msg + "=8");
                            }).Start();
                        }
                    }

                }
           
        }

        public  void DisplayRecord()
        {
            try
            {
                if (ObjMaster.Current == null) return;

               txtFare.Value = ObjMaster.Current.FareRate.ToDecimal();;
               txtCustFare.Value = ObjMaster.Current.CustomerPrice.ToDecimal();


               if (ObjMaster.Current.IsCompanyWise.ToBool() && ObjMaster.Current.CompanyId != null)
               {
                   if (ObjMaster.Current.Gen_Company.DefaultIfEmpty().DisableCompanyFaresForController.ToBool() == false || AppVars.LoginObj.LgroupId != 2)
                   {
                       txtCompFare.Visible = true;
                       lblCompFare.Visible = true;
                   }

                   txtCompFare.Value = ObjMaster.Current.CompanyPrice.ToDecimal();

               }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }

        private void frmEditFare_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                    this.Dispose();

                }

                if (e.Control)
                {

                    if (e.KeyCode == Keys.S)
                    {

                        Save();
                    }
                    else if (e.KeyCode == Keys.Q)
                    {

                        this.Close();
                    }
                        
                }

            
        }

      

        private void frmEditFare_Shown(object sender, EventArgs e)
        {

            txtFare.Select();
         //   txtFare.Focus();
        }

       
    }
}
