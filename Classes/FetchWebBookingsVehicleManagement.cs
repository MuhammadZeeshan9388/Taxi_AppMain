using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Taxi_BLL;
using Taxi_Model;
using Utils;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using Taxi_AppMain.Classes;
using System.Xml;
using System.Xml.Linq;

namespace Taxi_AppMain
{
    public  class FetchWebBookingsVehicleManagement : IDisposable
    {

        private bool hasWebBookingTab;

        public bool HasWebBookingTab
        {
            get { return hasWebBookingTab; }
            set { hasWebBookingTab = value; }
        }

        DateTime? fromBookingDate;

        public DateTime? FromBookingDate
        {
            get { return fromBookingDate; }
            set { fromBookingDate = value; }
        }
        DateTime? tillBookingDate;

        public DateTime? TillBookingDate
        {
            get { return tillBookingDate; }
            set { tillBookingDate = value; }
        }

       

        private List<Booking> _ListofFetechedJobs = null;

        public List<Booking> ListofFetechedJobs
        {
            get { return _ListofFetechedJobs; }
            set { _ListofFetechedJobs = value; }
        }


        public FetchWebBookingsVehicleManagement()
        {

            if(AppVars.ListOfWebsites==null)
                AppVars.ListOfWebsites=General.GetQueryable<OnlineWebsite>(null).ToList();

         
        }



        private string _CaptionText;

        public string CaptionText
        {
            get { return _CaptionText; }
            set { _CaptionText = value; }
        }
        private string _ContentText;

        public string ContentText
        {
            get { return _ContentText; }
            set { _ContentText = value; }
        }


        private int _CancelCount;

        public int CancelCount
        {
            get { return _CancelCount; }
            set { _CancelCount = value; }
        }



        public bool Fetch(ref string returnMsg)
        {
            bool rtn = false;

            try
            {
                using (DataClassesOnlineVehicleDataContext dbWeb = new DataClassesOnlineVehicleDataContext())
                {


                    if (this.fromBookingDate == null)
                        this.fromBookingDate = DateTime.Now.AddDays(-7);


              

                    string[] arr = AppVars.ListOfWebsites.Select(args => args.Name).ToArray<string>();
                    
              

                    List<OnlineBooking> listofBookings = dbWeb.OnlineBookings
                      .Where(c => arr.Contains(c.Client.Name) && (c.IsUpdated != null && c.IsUpdated == false)
                                                     && (this.FromBookingDate == null || c.BookingDate >= this.FromBookingDate)
                                                     && (this.TillBookingDate == null || c.BookingDate <= this.TillBookingDate)
                                                              ).ToList();





                    

                    if (listofBookings.Count > 0)
                    {
                        //
                        BookingBO objMaster = null;

                        DateTime pickupDate = new DateTime();
                        string pickupTime = string.Empty;
                        TimeSpan pickupTimeSpan = TimeSpan.Zero;
                        string locType = string.Empty;
                        string loc = string.Empty;
                        string vehicle = string.Empty;

                        int success = 0;
                        int failed = 0;

                        int? Companyid = null;

                        string via = string.Empty;

                        string refNo = string.Empty;


                        if ((AppVars.objPolicyConfiguration.EnableOnlineBookingAuthorization.ToBool()
                            || AppVars.objPolicyConfiguration.EnableMobileBookingAuthorization.ToBool()) && this.ListofFetechedJobs == null)
                            ListofFetechedJobs = new List<Booking>();


                        if (HasWebBookingTab && this.ListofFetechedJobs == null)
                            ListofFetechedJobs = new List<Booking>();


                        foreach (var item in listofBookings)
                        {
                            try
                            {

                            
                              

                                via = string.Empty;

                         

                                if (item.BookingStatus!=null)
                                {

                                    if (item.BookingStatus.ToLong() == 5)  // "cancelled"
                                    {

                                        if (item.SystemJobID!=null)
                                        {

                                            var obj = General.GetObject<Booking>(c => c.BookingTypeId == Enums.BOOKING_TYPES.ONLINE && c.Id==item.SystemJobID);


                                            if (obj != null && obj.BookingStatusId!=Enums.BOOKINGSTATUS.CANCELLED && obj.BookingStatusId!=Enums.BOOKINGSTATUS.DISPATCHED)
                                            {
                                                new TaxiDataContext().stp_UpdateJobStatus(obj.Id, Enums.BOOKINGSTATUS.CANCELLED);

                                            }

                                          
                                             dbWeb.stp_UpdateFetchedBookingStatus(item.ID, null, "Success", DateTime.Now, AppVars.LoginObj.UserName.ToStr());

                                            CancelCount++;
                                            continue;
                                        }

                                        else
                                        {
                                            CancelCount++;

                                            if (item.UpdateMessage.ToStr().Trim()=="" || item.UpdateMessage.ToStr().StartsWith("Failed"))
                                            {

                                             
                                                dbWeb.stp_UpdateFetchedBookingStatus(item.ID, null, "Success", DateTime.Now, AppVars.LoginObj.UserName.ToStr());
                                            }
                                            continue;

                                        }
                                    }

                                }


                                objMaster = new BookingBO();

                                if (item.CompanyID.ToInt() >0)
                                {
                                    Companyid = item.CompanyID;

                                    if (item.SystemJobID != null)
                                    {
                                        objMaster.GetByPrimaryKey(item.SystemJobID);

                                        if (objMaster.Current == null)
                                            objMaster.New();
                                    }
                                    else
                                        objMaster.New();

                                }
                                else
                                {

                                    Companyid = null;
                                    // objMaster = new BookingBO();
                                    objMaster.New();
                                }



                                if (objMaster.Current == null)
                                    continue;

                                //New Account Booking (17 Dec 2012)
                                if (Companyid.ToInt() >0)
                                {
                                    objMaster.Current.IsCompanyWise = true;
                                    objMaster.Current.CompanyId = Companyid;


                                    objMaster.Current.BookedBy = item.Row_InsertedBy.ToStr();
                                }

                               
                                //




                                objMaster.Current.BookingDate = DateTime.Now;


                                vehicle = item.Vehiclename.ToStr().Trim().ToLower();

                                if (!string.IsNullOrEmpty(vehicle))
                                {
                                    Fleet_VehicleType objvehicle = General.GetQueryable<Fleet_VehicleType>(null)
                                                         .FirstOrDefault(c => c.VehicleType.Trim().ToLower() == vehicle);

                                    if (objvehicle != null)
                                    {
                                        objMaster.Current.VehicleTypeId = objvehicle.Id;
                                    }
                                }


                                if (DateTime.TryParse(item.PickupDate.ToStr(), out pickupDate))
                                {

                                    pickupTime = item.PickupTime.ToStr();

                                    if (TimeSpan.TryParse(pickupTime, out pickupTimeSpan))
                                    {
                                        pickupDate = pickupDate.ToDate() + pickupTimeSpan;
                                    }

                                    objMaster.Current.PickupDateTime = pickupDate;

                                }


                                if (item.JourneyTypeid.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                                {
                                    if (item.ReturnDate!=null)
                                    {
                                        

                                            if (TimeSpan.TryParse(item.ReturnTime.ToStr(), out pickupTimeSpan))
                                            {
                                                pickupDate = item.ReturnDate.ToDate() + pickupTimeSpan;
                                                objMaster.Current.ReturnPickupDateTime = pickupDate;
                                            }
                                        
                                    }
                                }




                                if (AppVars.ListOfWebsites == null || AppVars.ListOfWebsites.Count == 1)
                                {

                                    objMaster.Current.SubcompanyId = AppVars.objSubCompany.Id;
                                }
                                else
                                {
                                 //   string clientName = "expressairp";




                                    if (AppVars.ListOfWebsites.Count > 1)
                                    {
                                        objMaster.Current.SubcompanyId = AppVars.ListOfWebsites.FirstOrDefault(c => c.Name == item.Client.Name).DefaultIfEmpty().SubCompanyId;

                                    }
                                }



                                //need to uncomment
                                objMaster.Current.OrderNo = item.OrderNumber.ToStr();

                                objMaster.Current.FromAddress = item.From.ToStr().ToUpper().Trim();
                                objMaster.Current.ToAddress = item.To.ToStr().ToUpper().Trim();

                                objMaster.Current.NoofPassengers = item.NoOfPassangers.ToInt();
                                objMaster.Current.NoofLuggages = item.NoOfLuggages.ToInt();
                                objMaster.Current.NoofHandLuggages = item.NoOfHandLuggages.ToInt();

                                objMaster.Current.FareRate = item.Fares.ToDecimal();
                                objMaster.Current.ReturnFareRate = item.ReturnFares.ToDecimal();
                                objMaster.Current.CustomerPrice = item.Fares.ToDecimal();



                                if (objMaster.Current.CompanyId != null)
                                    objMaster.Current.CompanyPrice = item.Fares.ToDecimal();


                                objMaster.Current.CustomerName = (item.PassangerFirstName.ToStr().ToProperCase().Trim()+ " " +item.PassangerLastName.ToStr().Trim()).Trim();

                                objMaster.Current.CustomerPhoneNo = item.Passengerphone.ToStr().Trim();
                                objMaster.Current.CustomerEmail = item.Email.ToStr().Trim();


                                objMaster.Current.CustomerMobileNo = item.PassengerMobile.ToStr().Trim();

                             
                                string babySeat1 = item.BabySeat1.ToStr().Trim();
                                string babySeat2 = item.BabySeat2.ToStr().Trim();

                                if (string.IsNullOrEmpty(babySeat1))
                                    babySeat1 = "no child seat required";

                                if (string.IsNullOrEmpty(babySeat2))
                                    babySeat2 = "no child seat required";



                               

                                 string   babyseats = string.Empty;

                                 if (babySeat1.ToStr().ToLower() != "no child seat required")
                                     babyseats = babySeat1;


                                 if (babySeat2.ToStr().ToLower() != "no child seat required")
                                     babyseats += "<<<" + babySeat2;


                                    objMaster.Current.BabySeats = babyseats.ToStr().Trim();                                  
                                


                                //if (specialReq.Contains("<babyseat3>"))
                                //{
                                //    specialReq = specialReq.Replace("<babyseat3>", "");

                                //    if (specialReq.Contains("</babyseat3>"))
                                //    {
                                //        specialReq = specialReq.Replace("</babyseat3>", "");
                                //    }

                                //}


                                    objMaster.Current.SpecialRequirements = item.SpecialRequirement.ToStr().Trim();
                                

                                //if (specialReq.Contains(">"))
                                //{
                                //    string[] specArr = specialReq.Split('>');

                                //    if (specArr.Count() == 2)
                                //    {
                                //        objMaster.Current.SpecialRequirements = specArr[0].ToStr();


                                //        if (specArr[1].ToStr().Length > 30)
                                //        {
                                //            objMaster.Current.AddLog = specArr[1].ToStr().Substring(0, 20) + " (Web)";

                                //        }
                                //        else
                                //        {

                                //            objMaster.Current.AddLog = specArr[1].ToStr() + " (Web)";
                                //        }
                                //    }

                                //}
                                //else
                                //{

                                //    objMaster.Current.SpecialRequirements = specialReq.ToStr();

                                    objMaster.Current.AddLog = AppVars.LoginObj.UserName;
                              //  }



                                objMaster.Current.AddOn = DateTime.Now;
                                objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();

                                locType = item.FromType.ToStr().Trim().ToUpper();

                                int FromLocTypeId = 0;
                                int ToLocTypeId = 0;

                                if (locType == Enums.LOCATION_TYPENAMES.ADDRESS)
                                    FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                else if (locType == Enums.LOCATION_TYPENAMES.AIRPORT)
                                {
                                    FromLocTypeId = Enums.LOCATION_TYPES.AIRPORT;

                                    objMaster.Current.FromDoorNo = item.FromFlightNumber.ToStr().Trim();
                                    objMaster.Current.FromStreet = item.ComingFrom.ToStr().Trim();


                                }

                                else if (locType == Enums.LOCATION_TYPENAMES.POSTCODE)
                                    FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;


                                else if (locType == Enums.LOCATION_TYPENAMES.STATION || locType == Enums.LOCATION_TYPENAMES.STATIONS
                                    || locType == Enums.LOCATION_TYPENAMES.RAILWAYSTATION)
                                    FromLocTypeId = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;

                                else if (locType == Enums.LOCATION_TYPENAMES.SEAPORTS || locType == Enums.LOCATION_TYPENAMES.CRUISEPORT
                                || locType == Enums.LOCATION_TYPENAMES.CRUISEPORTS)
                                    FromLocTypeId = Enums.LOCATION_TYPES.SEAPORTS;



                                objMaster.Current.FromLocTypeId = FromLocTypeId;

                                loc = item.From.ToStr().ToUpper().StripNewLine().Trim();

                                objMaster.Current.FromAddress = loc;

                                if (FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT || FromLocTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                                {
                                    Gen_Location objLoc = General.GetQueryable<Gen_Location>(null).FirstOrDefault(c => c.Address != null && c.Address.ToUpper().Trim() == loc);
                                    if (objLoc != null)
                                    {
                                        objMaster.Current.FromLocId = objLoc.Id;
                                    }
                                    else
                                    {
                                        //If Location is not found.
                                        objMaster.Current.FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                    }

                                }


                                if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE || FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                                {
                                    objMaster.Current.FromDoorNo = item.FromDoorNumber.ToStr().Trim();
                                    objMaster.Current.FromStreet = item.FromStreet.ToStr().Trim();

                                    objMaster.Current.FromPostCode = General.GetPostCodeMatch(item.From.ToStr().ToUpper().Trim());
                                }



                                // To Location 
                                locType = item.ToType.ToStr().Trim().ToUpper();


                                loc = item.To.ToStr().ToUpper().StripNewLine().Trim();

                                if (locType == Enums.LOCATION_TYPENAMES.ADDRESS)
                                    ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                else if (locType == Enums.LOCATION_TYPENAMES.AIRPORT)
                                    ToLocTypeId = Enums.LOCATION_TYPES.AIRPORT;

                                else if (locType == Enums.LOCATION_TYPENAMES.POSTCODE)
                                    ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;


                                else if (locType == Enums.LOCATION_TYPENAMES.STATION || locType == Enums.LOCATION_TYPENAMES.STATIONS
                                    || locType == Enums.LOCATION_TYPENAMES.RAILWAYSTATION)
                                    ToLocTypeId = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;

                                else if (locType == Enums.LOCATION_TYPENAMES.SEAPORTS || locType == Enums.LOCATION_TYPENAMES.CRUISEPORT
                                    || locType == Enums.LOCATION_TYPENAMES.CRUISEPORTS)
                                    ToLocTypeId = Enums.LOCATION_TYPES.SEAPORTS;


                                

                                objMaster.Current.ToLocTypeId = ToLocTypeId;

                                objMaster.Current.ToAddress = loc;

                                if (ToLocTypeId == Enums.LOCATION_TYPES.AIRPORT || ToLocTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                                {
                                    Gen_Location objLoc = General.GetQueryable<Gen_Location>(null).FirstOrDefault(c => c.Address.ToUpper().Trim() == loc);
                                    if (objLoc != null)
                                    {
                                        objMaster.Current.ToLocId = objLoc.Id;

                                    }
                                    else
                                    {
                                        //If Location is not found.
                                        objMaster.Current.ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                    }

                                }



                                if (FromLocTypeId == 0)
                                {

                                    objMaster.Current.FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
                                }

                                if (ToLocTypeId == 0)
                                {
                                    objMaster.Current.ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                }                          

                                

                                objMaster.Current.ToDoorNo = item.ToDoorNumber.ToStr().Trim();
                                objMaster.Current.ToStreet = item.ToStreet.ToStr().Trim();
                                objMaster.Current.ToPostCode = General.GetPostCodeMatch(loc);

                  
                                objMaster.Current.JourneyTypeId = item.JourneyTypeid;
                                objMaster.Current.SpecialRequirements = objMaster.Current.SpecialRequirements.ToStr().Replace("\n", "\r\n");


                                if (item.JourneyTypeid.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                                {
                                    objMaster.ReturnSpecialRequirement = objMaster.Current.SpecialRequirements.ToStr();
                               
                                }


                               

                                int paymentTypeId = Enums.PAYMENT_TYPES.CASH;
                                if (item.PaymentType.ToStr().ToLower() == "cash")
                                {
                                    paymentTypeId = Enums.PAYMENT_TYPES.CASH;
                                }
                                else if (item.PaymentType.ToStr().ToLower() == "credit card" || item.PaymentType.ToStr().ToLower() == "creditcard"
                                    || item.PaymentType.ToStr().ToLower().Contains("card") || item.PaymentType.ToStr().ToLower().Contains("paypal")
                                      || item.PaymentType.ToStr().ToLower().Contains("nochex") || item.PaymentType.ToStr().ToLower().Contains("world pay")
                                    || item.PaymentType.ToStr().ToLower().Contains("worldpay")
                                    || item.PaymentType.ToStr().ToLower().Contains("pay by phone") || item.PaymentType.ToStr().ToLower().Contains("pay pal"))
                                {
                                    paymentTypeId = Enums.PAYMENT_TYPES.CREDIT_CARD;

                                    if (item.TransactionId.ToStr().Trim().Length > 0)
                                    {

                                        if (objMaster.Current.BookingPayment == null)
                                            objMaster.Current.BookingPayment = new Booking_Payment();


                                        objMaster.Current.BookingPayment.AuthCode = item.TransactionId.ToStr().Trim();                                
                                        

                                    }

                                }
                                else if (item.PaymentType.ToStr().ToLower().EndsWith("account"))
                                {
                                    paymentTypeId = Enums.PAYMENT_TYPES.BANK_ACCOUNT;
                                }
                                else
                                {
                                    paymentTypeId = General.GetObject<Gen_PaymentType>(c => c.PaymentType.ToLower() == item.PaymentType.ToStr().ToLower()).DefaultIfEmpty().Id;
                                }


                                if (paymentTypeId == 0)
                                    paymentTypeId = Enums.PAYMENT_TYPES.CASH;

                                objMaster.Current.PaymentTypeId = paymentTypeId;


                                if (item.PostedFrom.ToStr().Trim().ToLower() == "app")
                                    objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.ONLINE;
                                else
                                    objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.WEB;

                               


                                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                                IList<Booking_ViaLocation> savedList = objMaster.Current.Booking_ViaLocations;

                                List<Booking_ViaLocation> listofDetail = new List<Booking_ViaLocation>();

                                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "Off2Exectuive")
                                {
                                    var list = dbWeb.ViaPoints.Where(c => c.BookingID == item.ID).ToList();



                                    arr = null;
                                    foreach (var viaItem in list)
                                    {

                                        Booking_ViaLocation objVia = new Booking_ViaLocation();

                                        objVia.ViaLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                        objVia.ViaLocTypeValue = "Address";
                                        objVia.ViaLocId = null;
                                        objVia.ViaLocLabel = null;



                                        arr = viaItem.ViaPoint1.ToStr().Split(new string[] { "%%%" }, StringSplitOptions.RemoveEmptyEntries);
                                        //    NETHERWOOD PLACE COWGROVE WIMBORNE DORSET BH21 4EN%%%ertyuui | 0000000%%%MR Smith

                                        if (arr.Count() > 1)
                                        {
                                            objVia.ViaLocTypeLabel = "Via";


                                            string val = arr[0].ToStr() + "%%%";

                                            string viaPax = viaItem.ViaPoint1.Replace(val, "").Trim();


                                            string[] viaPaxArr = viaPax.Split(new string[] { "%%%" }, StringSplitOptions.RemoveEmptyEntries);

                                            int cnt = 1;
                                            viaPax = string.Join("\r\n", viaPaxArr.Select(c => cnt++.ToStr() + ". " + c.ToStr()).ToArray<string>());


                                            objVia.ViaLocTypeLabel = viaPax.ToStr();
                                            objVia.ViaLocValue = arr[0].ToStr();

                                        }
                                        else
                                        {
                                            objVia.ViaLocTypeLabel = "";
                                            objVia.ViaLocValue = viaItem.ViaPoint1.ToStr();

                                        }



                                        listofDetail.Add(objVia);



                                    }


                                    int cnter = 1;
                                    via = string.Join("\r\n", list.Select(c => cnter++.ToStr() + ". " + c.ViaPoint1.ToStr().Replace("%%%", Environment.NewLine)).ToArray<string>());





                                }
                                else
                                {


                                    if (item.PostedFrom.ToStr().Trim().ToLower() == "app")
                                    {

                                        var list = dbWeb.ViaPoints.Where(c => c.BookingID == item.ID).ToList();
                                        listofDetail = (from r in list
                                                        select new Booking_ViaLocation
                                                        {

                                                            //  BookingId = r.Cells[COLS.MASTERID].Value.ToLong(),
                                                            ViaLocTypeId = Enums.LOCATION_TYPES.ADDRESS,
                                                            ViaLocTypeLabel = "Via",
                                                            ViaLocTypeValue = "Address",

                                                            ViaLocId = null,
                                                            ViaLocLabel = null,
                                                            ViaLocValue = r.ViaPoint1

                                                        }).ToList();


                                        int cnt = 1;
                                        via = string.Join("\r\n", list.Select(c => cnt++.ToStr() + ". " + c.ViaPoint1.ToStr()).ToArray<string>());

                                    }
                                    else
                                    {

                                        string[] list = item.ViaPoints.ToStr().Trim().Split(new string[] { "<span style='color:blue'> >> </span>" }, StringSplitOptions.RemoveEmptyEntries);



                                        listofDetail = (from r in list
                                                        select new Booking_ViaLocation
                                                        {

                                                            //  BookingId = r.Cells[COLS.MASTERID].Value.ToLong(),
                                                            ViaLocTypeId = Enums.LOCATION_TYPES.ADDRESS,
                                                            ViaLocTypeLabel = "Via",
                                                            ViaLocTypeValue = "Address",

                                                            ViaLocId = null,
                                                            ViaLocLabel = null,
                                                            ViaLocValue = r

                                                        }).ToList();

                                        int cnt = 1;
                                        via = string.Join("\r\n", list.Select(c => cnt++.ToStr() + ". " + c.ToStr()).ToArray<string>());
                                    }



                                   
                                }



                                if (listofDetail.Count > 0)
                                {

                                    Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);
                                }


                                objMaster.Current.BookingNo = item.ID.ToStr();

                                objMaster.Current.IsCommissionWise = false;
                                objMaster.Current.DriverCommission = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();
                                objMaster.Current.DriverCommissionType = "Percent";


                                if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() && AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool())
                                {


                                    objMaster.Current.ZoneId = GetZoneId(objMaster.Current.FromAddress.ToUpper().ToStr().Trim());

                                    objMaster.Current.DropOffZoneId = GetZoneId(objMaster.Current.ToAddress.ToUpper().ToStr().Trim());
                                }



                                if (this.HasWebBookingTab)
                                {
                                    objMaster.Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING_WEBBOOKING;

                                    objMaster.Current.BookingNo += "_";
                                }


                                //string notes = string.Empty;
                                //if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "pinkberrycars")
                                //{
                                  
                                   

                                //    var objCredit = dbWeb.CreditDatas.FirstOrDefault(c => c.BookingId == item.ID);

                                //    if (objCredit != null)
                                //    {
                                //        notes ="CardType="+objCredit.CardType.ToStr()+" , Amount="+objCredit.Amount.ToStr()+" , CardNumber=" + objCredit.CardNumber.ToStr() + Environment.NewLine + "Expiry=" + objCredit.Expiry.ToStr() + " , " + "SecurityCode=" + objCredit.SecurityCode.ToStr() + Environment.NewLine + "Name="+objCredit.FirstName.ToStr()+" " + objCredit.LastName.ToStr() + " , Address line 1=" + objCredit.CardRegisterAddress.ToStr() +" , Address line 2="+objCredit.AddressLine2.ToStr()+Environment.NewLine+"Town/City="+objCredit.Town.ToStr()+" , Post/zip code="+objCredit.Postcode.ToStr()+" , Country="+objCredit.Country.ToStr();

                                //        objMaster.Current.PaymentComments = notes;
                                //        objMaster.Current.Booking_Notes.Add(new Booking_Note { notes = notes, AddOn = DateTime.Now });
                                //    }

                                //    var objBooker = dbWeb.Booking_BookerDetails.FirstOrDefault(c => c.BookingId == item.ID);

                                //    if (objBooker != null)
                                //    {
                                //        objMaster.Current.Booking_Notes.Add(new Booking_Note { notes = "Booker's Details : " + Environment.NewLine + "Booker's Full Name : " + objBooker.BookerFullName.ToStr() + Environment.NewLine + "Email : " + objBooker.BookerEmail.ToStr() + Environment.NewLine + "Mobile No : " + objBooker.BookingMobileNo.ToStr() + Environment.NewLine + "Home No : " + objBooker.BookerHomeNo.ToStr(), AddOn = DateTime.Now });



                                //    }


                                if (item.NoOfHandLuggages.ToInt() > 0)
                                {
                                    objMaster.Current.SpecialRequirements = "Hand Lugg : " + item.NoOfHandLuggages.ToStr() + " ," + objMaster.Current.SpecialRequirements;

                                    if (item.JourneyTypeid.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                                    {
                                        objMaster.ReturnSpecialRequirement = objMaster.Current.SpecialRequirements.ToStr();

                                    }
                                }
                                //}

                                objMaster.Current.OnlineBookingId = item.ID;

                                objMaster.Current.AutoDespatch = false;
                                objMaster.Current.IsBidding = false;

                                objMaster.Save();

                                refNo = objMaster.Current.BookingNo.ToStr();

                                UpdateWebBooking(item.ID, "Success", true, objMaster.Current.Id);

                                if (ListofFetechedJobs != null)
                                {

                                    // New Work for Accept/Decline Booking
                                    ListofFetechedJobs.Add(new Booking
                                    {
                                        Id = objMaster.Current.Id,
                                        BookingNo = objMaster.Current.BookingNo,
                                        BookingDate=objMaster.Current.BookingDate,
                                        CustomerName = objMaster.Current.CustomerName,
                                        CustomerPhoneNo = objMaster.Current.CustomerPhoneNo,
                                        CustomerMobileNo = objMaster.Current.CustomerMobileNo,
                                        CustomerEmail = objMaster.Current.CustomerEmail,
                                        FromAddress = objMaster.Current.FromAddress,

                                        ToAddress = objMaster.Current.ToAddress,
                                        PickupDateTime = objMaster.Current.PickupDateTime,
                                        BookingTypeId = objMaster.Current.BookingTypeId,
                                        AddBy = item.ClientID.ToInt(),
                                        FromDoorNo = objMaster.Current.FromDoorNo,
                                        FromStreet = objMaster.Current.FromStreet,
                                        //       FromComing=item.Status,
                                        DistanceString = via,
                                        FareRate = objMaster.Current.FareRate,
                                        AddLog=item.ID.ToStr(),
                                        JourneyTypeId=1,

                                        SpecialRequirements="",
                                        CancelReason = item.PaymentType.ToStr(),
                                        BoundType = vehicle.ToStr().ToProperCase(),
                                        OnlineBookingId = objMaster.Current.OnlineBookingId,
                                        CompanyId=objMaster.Current.CompanyId,
                                         IsCompanyWise=objMaster.Current.IsCompanyWise,
                                         CompanyCreditCardDetails=objMaster.Current.Gen_Company.DefaultIfEmpty().CompanyName,
                                        IsQuotation=objMaster.Current.CompanyId!=null ?  true:false, 
                                        DriverWaitingMins=objMaster.Current.Gen_Company.DefaultIfEmpty().SysGenId
                                    });



                                    //needtorecheckfrombothwebbookingoptions
                                    //if (!hasWebBookingTab)
                                    //{

                                        if (objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && objMaster.Current.BookingReturns.Count > 0)
                                        {


                                            ListofFetechedJobs.Add(new Booking
                                            {
                                                Id = objMaster.Current.Id,
                                                BookingNo = objMaster.Current.BookingReturns[0].BookingNo,
                                                 BookingDate=objMaster.Current.BookingReturns[0].BookingDate,
                                                CustomerName = objMaster.Current.BookingReturns[0].CustomerName,
                                                CustomerPhoneNo = objMaster.Current.BookingReturns[0].CustomerPhoneNo,
                                                CustomerMobileNo = objMaster.Current.BookingReturns[0].CustomerMobileNo,
                                                CustomerEmail = objMaster.Current.BookingReturns[0].CustomerEmail,
                                                FromAddress = objMaster.Current.BookingReturns[0].FromAddress,

                                                ToAddress = objMaster.Current.BookingReturns[0].ToAddress,
                                                PickupDateTime = objMaster.Current.BookingReturns[0].PickupDateTime,
                                                BookingTypeId = objMaster.Current.BookingReturns[0].BookingTypeId,
                                                AddBy = item.ClientID.ToInt(),
                                                FromDoorNo = objMaster.Current.BookingReturns[0].FromDoorNo,
                                                FromStreet = objMaster.Current.BookingReturns[0].FromStreet,
                                                //       FromComing=item.Status,
                                                DistanceString = via,
                                                FareRate = objMaster.Current.BookingReturns[0].FareRate,
                                                AddLog = item.ID.ToStr(),
                                                JourneyTypeId = 2,

                                                CancelReason = item.PaymentType.ToStr(),
                                                BoundType = vehicle.ToStr().ToProperCase(),
                                                OnlineBookingId = objMaster.Current.OnlineBookingId,

                                                CompanyId = objMaster.Current.CompanyId,
                                                IsCompanyWise = objMaster.Current.IsCompanyWise,
                                                CompanyCreditCardDetails = objMaster.Current.Gen_Company.DefaultIfEmpty().CompanyName,
                                                IsQuotation = objMaster.Current.CompanyId != null ? true : false,
                                                DriverWaitingMins = objMaster.Current.Gen_Company.DefaultIfEmpty().SysGenId
                                            });


                                        }
                                  //  }


                                }


                                success++;
                                if (AppVars.objPolicyConfiguration.EnableOnlineBookingAuthorization.ToBool()==false && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.WebBookingText.ToStr().Trim()) && !string.IsNullOrEmpty(objMaster.Current.CustomerMobileNo))
                                {
                                    if (objMaster.Current!=null && objMaster.Current.CustomerMobileNo != null)
                                    {
                                        string messageValue=GetMessage(AppVars.objPolicyConfiguration.WebBookingText.ToStr(), objMaster.Current);
                                       

                                        new Thread(delegate()
                                        {
                                            SendSMS(messageValue, objMaster.Current.CustomerMobileNo.ToStr().Trim());
                                        }).Start();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                                if (objMaster.Errors.Count == 0 || (objMaster.Errors.Count(c => c == "WebBooking Already Exist") == 0))
                                {
                                    try
                                    {

                                       
                                        dbWeb.stp_UpdateFetchedBookingStatus(item.ID, null, "Failed : " + objMaster.ShowErrors() + " Exception : " + ex.Message, DateTime.Now, AppVars.LoginObj.UserName.ToStr());

                                    }
                                    catch (Exception ex2)
                                    {


                                    }
                                   
                                    failed++;
                                }
                            }
                        }


                        // Summary





                        int totalBookings = listofBookings.Count - CancelCount;

                        if (totalBookings>0)
                        {


                            string summary = "Total Bookings Fetched : " + totalBookings;


                            this.CaptionText = summary;
                            this.ContentText = "Success : " + success.ToStr() + Environment.NewLine + "Failed : " + failed.ToStr();
                            this.ContentText += Environment.NewLine + "Ref No : " + refNo;
                            summary += Environment.NewLine + this.ContentText;
                            returnMsg= summary;
                        }
                        else
                            returnMsg= "";



                        //
                       // returnMsg = SaveWebBookings(listofBookings);
                        rtn = true;
                    }
                }
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                rtn = false;
            


            
            }


            return rtn;
        }





    
        private int? GetZoneId(string address)
        {
            if (string.IsNullOrEmpty(General.GetPostCodeMatch(address)))
                return null;

            if (address.Contains(", UK"))
                address = address.Remove(address.LastIndexOf(", UK"));



            int? zoneId = null;

            try
            {
              
                    string postCode = General.GetPostCode(address);
                    Gen_Coordinate objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == postCode);

                    if (objCoord != null)
                    {

                        double latitude = 0, longitude = 0;

                        latitude = Convert.ToDouble(objCoord.Latitude);
                        longitude = Convert.ToDouble(objCoord.Longitude);


                        var plot = (from a in General.GetQueryable<Gen_Zone>(c => c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                           && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude))
                                                           orderby a.PlotKind
                                    select a.Id).ToArray<int>();


                        if (plot.Count() > 0)
                        {
                            var list = (from p in plot
                                        join a in General.GetQueryable<Gen_Zone_PolyVertice>(null) on p equals a.ZoneId
                                        select a).ToList();


                            foreach (int plotId in plot)
                            {
                                if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                {
                                    zoneId = plotId;
                                    break;

                                }
                            }
                        }

                    }           


            }
            catch (Exception ex)
            {


            }

            return zoneId;

        }


        public static bool FindPoint(double pointLat, double pointLng, List<Gen_Zone_PolyVertice> PontosPolig)
        {//                             X               y               
            int sides = PontosPolig.Count();
            int j = sides - 1;
            bool pointStatus = false;

            for (int i = 0; i < sides; i++)
            {
                if (PontosPolig[i].Longitude < pointLng && PontosPolig[j].Longitude >= pointLng ||
                    PontosPolig[j].Longitude < pointLng && PontosPolig[i].Longitude >= pointLng)
                {
                    if (PontosPolig[i].Latitude + (pointLng - PontosPolig[i].Longitude) /
                        (PontosPolig[j].Longitude - PontosPolig[i].Longitude) * (PontosPolig[j].Latitude - PontosPolig[i].Latitude) < pointLat)
                    {
                        pointStatus = !pointStatus;
                    }
                }
                j = i;
            }
            return pointStatus;
        }


        private string GetMessage(string message, Booking objBooking)
        {
            try
            {


                string msg = message;

                object propertyValue = string.Empty;
                foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                {




                    switch (tag.TagObjectName)
                    {
                        case "booking":

                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = objBooking.GetType().GetProperty(val[0]).GetValue(objBooking, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(tag.ConditionNotNull) && objBooking.GetType().GetProperty(tag.ConditionNotNull) != null)
                                {

                                    propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                }
                                else
                                {

                                    propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                }
                            }


                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);
                            }
                            break;                        


                        default:

                            if (objBooking.SubcompanyId == null)
                            {

                                propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);

                            }
                            else
                            {
                                if (objBooking.Gen_SubCompany != null)
                                {
                                    propertyValue = objBooking.Gen_SubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking.Gen_SubCompany, null);

                                }

                            }
                            break;

                    }




                    msg = msg.Replace(tag.TagMemberValue,
                        tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                }


                return msg.Replace("\n\n", "\n");
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);
                return "";
            }
        }



        private bool SendSMS(string msg, string mobileNo)
        {

            bool rtn = true;
            try
            {


                EuroSMS objSMS=new EuroSMS();
                string smsError1 = "";


                int idx = -1;
                if (mobileNo.StartsWith("044") == true)
                {
                    idx = mobileNo.IndexOf("044");
                    mobileNo = mobileNo.Substring(idx + 3);
                    mobileNo = mobileNo.Insert(0, "+44");
                }

                if (mobileNo.StartsWith("07"))
                {
                    mobileNo = mobileNo.Substring(1);
                }

                if (mobileNo.StartsWith("0440") == false || mobileNo.StartsWith("+440") == false)
                    mobileNo = mobileNo.Insert(0, "+44");

                objSMS.ToNumber = mobileNo;
                objSMS.Message = msg;
              //  System.Threading.Thread.Sleep(1000);
                rtn = objSMS.Send(ref smsError1);
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);


            }

            return rtn;

        }


        private void CreateAndShowAlert(string caption, string content, Image contentImg, System.Media.SystemSound sound)
        {
            RadDesktopAlert desktopAlert = new Telerik.WinControls.UI.RadDesktopAlert();
            desktopAlert.CaptionText = caption;
            desktopAlert.ContentText = content;
            desktopAlert.ContentImage = contentImg;
            desktopAlert.SoundToPlay = sound;
            desktopAlert.PlaySound = true;          
        
            desktopAlert.Show();
        }


        public static void UpdateWebBooking(long Id, string msg, bool IsUpdated, long? systemJobId)
        {
            try
            {


                

                new DataClassesOnlineVehicleDataContext().stp_UpdateFetchedBookingStatus(Id, systemJobId, msg, DateTime.Now, AppVars.LoginObj.UserName.ToStr());

            }
            catch (Exception ex)
            {


            }


        }

        #region IDisposable Members

     
        public void Dispose()
        {
           // this.Dispose();
        }

        #endregion
    }
}
