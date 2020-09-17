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
    public  class FetchWebBookings : IDisposable
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


        public FetchWebBookings()
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
                using (WebDataClassesDataContext dbWeb = new WebDataClassesDataContext())
                {


                    if (this.fromBookingDate == null)
                        this.fromBookingDate = DateTime.Now.AddDays(-7);



                    string[] arr = AppVars.ListOfWebsites.Select(args => args.Name).ToArray<string>();
                    
              

                    List<WebBooking> listofBookings = dbWeb.WebBookings
                      .Where(c => arr.Contains(c.CLient.Name) && (c.IsUpdated != null && c.IsUpdated == false)
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


                                if (!string.IsNullOrEmpty(item.Status.ToStr().Trim()))
                                {

                                    if (item.Status.ToStr().Trim().ToLower() == "cancelled")
                                    {

                                        if (item.UpdateMessage.ToStr().Trim().ToLower() == "success")
                                        {

                                            var obj = General.GetObject<Booking>(c => c.BookingTypeId == Enums.BOOKING_TYPES.ONLINE && c.BookingNo.EndsWith(item.ID.ToStr()));


                                            if (obj != null)
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

                                            if (item.UpdateMessage == null || item.UpdateMessage.ToStr().StartsWith("Failed"))
                                            {

                                                dbWeb.stp_UpdateFetchedBookingStatus(item.ID, null, "Success", DateTime.Now, AppVars.LoginObj.UserName.ToStr());
                                            }
                                            continue;

                                        }
                                    }

                                }


                                objMaster = new BookingBO();

                                if (item.BookingAccount != null)
                                {
                                    Companyid = item.BookingAccount.AccountId;

                                    if (item.BookingAccount.SystemJobId != null)
                                    {
                                        objMaster.GetByPrimaryKey(item.BookingAccount.SystemJobId);

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
                                if (Companyid != null)
                                {
                                    objMaster.Current.IsCompanyWise = true;

                                }

                                objMaster.Current.CompanyId = Companyid;
                                //




                                objMaster.Current.BookingDate = DateTime.Now;


                                vehicle = item.VehicleName.ToStr().Trim().ToLower();

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


                                if (item.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                                {
                                    if (!string.IsNullOrEmpty(item.ReturnDate))
                                    {
                                        if (DateTime.TryParse(item.ReturnDate, out pickupDate))
                                        {

                                            if (TimeSpan.TryParse(item.ReturnTime.ToStr(), out pickupTimeSpan))
                                            {
                                                pickupDate = pickupDate.ToDate() + pickupTimeSpan;
                                                objMaster.Current.ReturnPickupDateTime = pickupDate;
                                            }
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
                                        objMaster.Current.SubcompanyId = AppVars.ListOfWebsites.FirstOrDefault(c => c.Name == item.CLient.Name).DefaultIfEmpty().SubCompanyId;

                                    }
                                }



                                objMaster.Current.OrderNo = item.FromOther.ToStr();

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

                                objMaster.Current.CustomerName = item.Name.ToStr().ToProperCase().Trim();

                                objMaster.Current.CustomerPhoneNo =GetFormattedContactNo(item.PassengerPhone.ToStr().Trim());
                                objMaster.Current.CustomerEmail = item.Email.ToStr().Trim();


                                objMaster.Current.CustomerMobileNo =GetFormattedContactNo(item.PassengerMobile.ToStr().Trim());





                                string specialReq = item.SpecialRequirement.ToStr().Trim();

                                if(specialReq.Contains("<babyseat1>") && specialReq.Contains("</babyseat2>"))
                                {
                                    string babyseats = specialReq.Substring(0, specialReq.LastIndexOf("</babyseat2>") + 12);
                          

                                    string babyseat1= babyseats.Substring(babyseats.IndexOf("<babyseat1>")+11,babyseats.IndexOf("</babyseat1>")-11);

                                    string babyseat2 = babyseats.Substring(babyseats.IndexOf("<babyseat2>") + 11, babyseats.IndexOf("</babyseat2>") - (babyseats.IndexOf("<babyseat2>") + 11));

                                    specialReq = specialReq.Replace(babyseats, "").Trim();


                                    babyseats = string.Empty;

                                    if (babyseat1.ToStr().ToLower()!="no child seat required")
                                        babyseats = babyseat1;


                                    if (babyseat2.ToStr().ToLower() != "no child seat required")
                                        babyseats +="<<<"+ babyseat2;


                                    if (!string.IsNullOrEmpty(babyseats.ToStr().Trim()) && babyseats.Contains("<<<") == false)
                                        babyseats = babyseats.ToStr().Trim() + "<<<";

                                    objMaster.Current.BabySeats = babyseats.ToStr().Trim();                                  

                                }


                                if (specialReq.Contains("<babyseat3>"))
                                {
                                    specialReq = specialReq.Replace("<babyseat3>", "");

                                    if (specialReq.Contains("</babyseat3>"))
                                    {
                                        specialReq = specialReq.Replace("</babyseat3>", "");
                                    }

                                }


                                if (specialReq.Contains(">"))
                                {
                                    string[] specArr = specialReq.Split('>');

                                    if (specArr.Count() == 2)
                                    {
                                        objMaster.Current.SpecialRequirements = specArr[0].ToStr();


                                        if (specArr[1].ToStr().Length > 30)
                                        {
                                            objMaster.Current.AddLog = specArr[1].ToStr().Substring(0, 20) + " (Web)";

                                        }
                                        else
                                        {

                                            objMaster.Current.AddLog = specArr[1].ToStr() + " (Web)";
                                        }
                                    }

                                }
                                else
                                {

                                    objMaster.Current.SpecialRequirements = specialReq.ToStr();

                                    objMaster.Current.AddLog = AppVars.LoginObj.UserName;
                                }



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

                                    objMaster.Current.FromDoorNo = item.FlightNumber.ToStr().Trim();
                                    objMaster.Current.FromStreet = item.ComingFrom.ToStr().Trim();


                                }

                                else if (locType == Enums.LOCATION_TYPENAMES.POSTCODE)
                                    FromLocTypeId = Enums.LOCATION_TYPES.POSTCODE;


                                else if (locType == Enums.LOCATION_TYPENAMES.STATION || locType == Enums.LOCATION_TYPENAMES.STATIONS
                                    || locType == Enums.LOCATION_TYPENAMES.RAILWAYSTATION)
                                    FromLocTypeId = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;
                                else
                                    FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                //else if (locType == Enums.LOCATION_TYPENAMES.SEAPORTS || locType == Enums.LOCATION_TYPENAMES.CRUISEPORT
                                //|| locType == Enums.LOCATION_TYPENAMES.CRUISEPORTS)
                                //    FromLocTypeId = Enums.LOCATION_TYPES.SEAPORTS;



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
                                    objMaster.Current.FromDoorNo = item.FromDoor.ToStr().Trim();
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
                                    ToLocTypeId = Enums.LOCATION_TYPES.POSTCODE;


                                else if (locType == Enums.LOCATION_TYPENAMES.STATION || locType == Enums.LOCATION_TYPENAMES.STATIONS
                                    || locType == Enums.LOCATION_TYPENAMES.RAILWAYSTATION)
                                    ToLocTypeId = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;
                                else
                                    ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                //else if (locType == Enums.LOCATION_TYPENAMES.SEAPORTS || locType == Enums.LOCATION_TYPENAMES.CRUISEPORT
                                //    || locType == Enums.LOCATION_TYPENAMES.CRUISEPORTS)
                                //    ToLocTypeId = Enums.LOCATION_TYPES.SEAPORTS;


                                

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



                           

                                

                                objMaster.Current.ToDoorNo = item.ToDoorNo.ToStr().Trim();
                                objMaster.Current.ToStreet = item.ToStreet.ToStr().Trim();
                                objMaster.Current.ToPostCode = General.GetPostCodeMatch(loc);


                                objMaster.Current.JourneyTypeId = item.JourneyTypeId;
                                objMaster.Current.SpecialRequirements = objMaster.Current.SpecialRequirements.ToStr().Replace("\n", "\r\n");



                                string special = objMaster.Current.SpecialRequirements.ToStr();

                                // Added on : 17/08/2015 Danish
                                //
                                if (item.FromType.ToStr().Trim().ToUpper() == Enums.LOCATION_TYPENAMES.SEAPORTS && item.FromOther.ToStr().Trim() != string.Empty)
                                {
                                    objMaster.Current.SpecialRequirements = "Cruise Name - " + item.FromOther.ToStr() + " , " + special;

                                }
                                //

                                if (item.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                                {

                                    if (item.FromType.ToStr().Trim().ToUpper() == Enums.LOCATION_TYPENAMES.SEAPORTS && item.FromOther.ToStr().Trim() != string.Empty)
                                    {
                                        objMaster.ReturnSpecialRequirement ="Return Cruise Name - " + item.ToDoorNo.ToStr()+ " , "+ special;
                                    }
                                    else
                                    {

                                        objMaster.ReturnSpecialRequirement = special;
                                    }
                                }


                               

                                int paymentTypeId = Enums.PAYMENT_TYPES.CASH;
                                if (item.PaymentType.ToStr().ToLower() == "cash")
                                {
                                    paymentTypeId = Enums.PAYMENT_TYPES.CASH;
                                }
                                else if (item.PaymentType.ToStr().ToLower() == "credit card")
                                {
                                    paymentTypeId = Enums.PAYMENT_TYPES.CREDIT_CARD;
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






                                if (item.Status.ToStr().Trim().ToLower() == "waiting")
                                    objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.ONLINE;
                                else
                                    objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.WEB;




                                var list = dbWeb.WebBooking_ViaLocations.Where(c => c.BookingId == item.ID).ToList();



                                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                                IList<Booking_ViaLocation> savedList = objMaster.Current.Booking_ViaLocations;

                                List<Booking_ViaLocation> listofDetail = new List<Booking_ViaLocation>();

                                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "Off2Exectuive")
                                {



                                    arr = null;
                                    foreach (var viaItem in list)
                                    {

                                        Booking_ViaLocation objVia = new Booking_ViaLocation();

                                        objVia.ViaLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                                        objVia.ViaLocTypeValue = "Address";
                                        objVia.ViaLocId = null;
                                        objVia.ViaLocLabel = null;



                                        arr = viaItem.ViaLocValue.ToStr().Split(new string[] { "%%%" }, StringSplitOptions.RemoveEmptyEntries);
                                        //    NETHERWOOD PLACE COWGROVE WIMBORNE DORSET BH21 4EN%%%ertyuui | 0000000%%%MR Smith

                                        if (arr.Count() > 1)
                                        {
                                            objVia.ViaLocTypeLabel = "Via";


                                            string val = arr[0].ToStr() + "%%%";

                                            string viaPax = viaItem.ViaLocValue.Replace(val, "").Trim();


                                            string[] viaPaxArr = viaPax.Split(new string[] { "%%%" }, StringSplitOptions.RemoveEmptyEntries);

                                            int cnt = 1;
                                            viaPax = string.Join("\r\n", viaPaxArr.Select(c => cnt++.ToStr() + ". " + c.ToStr()).ToArray<string>());


                                            objVia.ViaLocTypeLabel = viaPax.ToStr();
                                            objVia.ViaLocValue = arr[0].ToStr();

                                        }
                                        else
                                        {
                                            objVia.ViaLocTypeLabel = "";
                                            objVia.ViaLocValue = viaItem.ViaLocValue.ToStr();

                                        }



                                        listofDetail.Add(objVia);



                                    }


                                    int cnter = 1;
                                    via = string.Join("\r\n", list.Select(c => cnter++.ToStr() + ". " + c.ViaLocValue.ToStr().Replace("%%%", Environment.NewLine)).ToArray<string>());





                                }
                                else
                                {



                                    listofDetail = (from r in list
                                                    select new Booking_ViaLocation
                                                    {

                                                        //  BookingId = r.Cells[COLS.MASTERID].Value.ToLong(),
                                                        ViaLocTypeId = Enums.LOCATION_TYPES.ADDRESS,
                                                        ViaLocTypeLabel = "Via",
                                                        ViaLocTypeValue = "Address",

                                                        ViaLocId = null,
                                                        ViaLocLabel = null,
                                                        ViaLocValue = r.ViaLocValue

                                                    }).ToList();


                                    int cnt = 1;
                                    via = string.Join("\r\n", list.Select(c => cnt++.ToStr() + ". " + c.ViaLocValue.ToStr()).ToArray<string>());
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


                                string notes = string.Empty;
                                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "pinkberrycars")
                                {
                                  
                                   

                                    var objCredit = dbWeb.CreditDatas.FirstOrDefault(c => c.BookingId == item.ID);

                                    if (objCredit != null)
                                    {
                                        notes ="CardType="+objCredit.CardType.ToStr()+" , Amount="+objCredit.Amount.ToStr()+" , CardNumber=" + objCredit.CardNumber.ToStr() + Environment.NewLine + "Expiry=" + objCredit.Expiry.ToStr() + " , " + "SecurityCode=" + objCredit.SecurityCode.ToStr() + Environment.NewLine + "Name="+objCredit.FirstName.ToStr()+" " + objCredit.LastName.ToStr() + " , Address line 1=" + objCredit.CardRegisterAddress.ToStr() +" , Address line 2="+objCredit.AddressLine2.ToStr()+Environment.NewLine+"Town/City="+objCredit.Town.ToStr()+" , Post/zip code="+objCredit.Postcode.ToStr()+" , Country="+objCredit.Country.ToStr();

                                        objMaster.Current.PaymentComments = notes;
                                        objMaster.Current.Booking_Notes.Add(new Booking_Note { notes = notes, AddOn = DateTime.Now });
                                    }

                                    var objBooker = dbWeb.Booking_BookerDetails.FirstOrDefault(c => c.BookingId == item.ID);

                                    if (objBooker != null)
                                    {
                                        objMaster.Current.Booking_Notes.Add(new Booking_Note { notes = "Booker's Details - " + Environment.NewLine + "Booker's Full Name - " + objBooker.BookerFullName.ToStr() + Environment.NewLine + "Email - " + objBooker.BookerEmail.ToStr() + Environment.NewLine + "Mobile No - " + objBooker.BookingMobileNo.ToStr() + Environment.NewLine + "Home No - " + objBooker.BookerHomeNo.ToStr(), AddOn = DateTime.Now });
                                    }



                                    // Added on : 17/08/2015 (danish)

                                    string countryCodes=GetCountryCodeString(item.PassengerMobile.ToStr().Trim(),item.PassengerPhone.ToStr().Trim());


                                    if (!string.IsNullOrEmpty(countryCodes))
                                    {
                                        objMaster.Current.Booking_Notes.Add(new Booking_Note { notes = countryCodes, AddOn = DateTime.Now });
                                    }

                      
                                }


                               

                                if (item.NoOfHandLuggages.ToInt() > 0)
                                {
                                    objMaster.Current.SpecialRequirements = "Hand Lugg - " + item.NoOfHandLuggages.ToStr() + " ," + objMaster.Current.SpecialRequirements;

                                    if (item.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                                    {
                                        objMaster.ReturnSpecialRequirement = objMaster.Current.SpecialRequirements.ToStr();

                                    }
                                }

                                objMaster.Current.OnlineBookingId = item.ID;
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
                                        AddBy = item.CLientID.ToInt(),
                                        FromDoorNo = objMaster.Current.FromDoorNo,
                                        FromStreet = objMaster.Current.FromStreet,
                                        //       FromComing=item.Status,
                                        DistanceString = via,
                                        FareRate = objMaster.Current.FareRate,
                                        AddLog=item.ID.ToStr(),
                                        JourneyTypeId=1,

                                         SpecialRequirements=notes,
                                        CancelReason = item.PaymentType.ToStr(),
                                         BoundType=vehicle.ToStr().ToProperCase(),
                                          OnlineBookingId=objMaster.Current.OnlineBookingId
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
                                                AddBy = item.CLientID.ToInt(),
                                                FromDoorNo = objMaster.Current.BookingReturns[0].FromDoorNo,
                                                FromStreet = objMaster.Current.BookingReturns[0].FromStreet,
                                                //       FromComing=item.Status,
                                                DistanceString = via,
                                                FareRate = objMaster.Current.BookingReturns[0].FareRate,
                                                AddLog = item.ID.ToStr(),
                                                JourneyTypeId = 2,

                                                CancelReason = item.PaymentType.ToStr(),
                                                BoundType = vehicle.ToStr().ToProperCase(),
                                                OnlineBookingId = objMaster.Current.OnlineBookingId
                                            });


                                        }
                                  //  }


                                }


                                success++;
                                if (!string.IsNullOrEmpty(AppVars.objPolicyConfiguration.WebBookingText.ToStr().Trim()) && !string.IsNullOrEmpty(objMaster.Current.CustomerMobileNo))
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
                                    //  UpdateWebBooking(item.ID, "Failed : " + objMaster.ShowErrors() + " Exception : " + ex.Message.ToStr(), false, null);

                                    failed++;
                                }
                            }
                        }


                        // Summary





                        int totalBookings = listofBookings.Count;

                        if (CancelCount != totalBookings)
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







        //private  string SaveWebBookings(List<WebBooking> listofBookings)
        //{
        //    BookingBO objMaster = null;

        //    DateTime pickupDate = new DateTime();
        //    string pickupTime = string.Empty;
        //    TimeSpan pickupTimeSpan = TimeSpan.Zero;
        //    string locType = string.Empty;
        //    string loc = string.Empty;
        //    string vehicle = string.Empty;

        //    int success = 0;
        //    int failed = 0;

        //    int? Companyid = null;
       
        //    string via = string.Empty;

        //    string refNo = string.Empty;


        //    if ( (AppVars.objPolicyConfiguration.EnableOnlineBookingAuthorization.ToBool()
        //        || AppVars.objPolicyConfiguration.EnableMobileBookingAuthorization.ToBool())   && this.ListofFetechedJobs == null)
        //        ListofFetechedJobs = new List<Booking>();


        //     if(HasWebBookingTab && this.ListofFetechedJobs==null)
        //         ListofFetechedJobs = new List<Booking>();

         
        //    foreach (var item in listofBookings)
        //    {
        //        try
        //        {

        //            via = string.Empty;


        //            if(!string.IsNullOrEmpty(item.Status.ToStr().Trim()))
        //            {

        //                if (item.Status.ToStr().Trim().ToLower() == "cancelled")
        //                {

        //                    if (item.UpdateMessage.ToStr().Trim().ToLower() == "success")
        //                    {

        //                        var obj=    General.GetObject<Booking>(c => c.BookingTypeId == Enums.BOOKING_TYPES.ONLINE && c.BookingNo.EndsWith(item.ID.ToStr()));


        //                        if (obj != null)
        //                        {
        //                            new TaxiDataContext().stp_UpdateJobStatus(obj.Id, Enums.BOOKINGSTATUS.CANCELLED);
                                  
        //                        }

        //                        new WebDataClassesDataContext().stp_UpdateFetchedBookingStatus(item.ID, null, "Success", DateTime.Now, AppVars.LoginObj.UserName.ToStr());

        //                        CancelCount++;
        //                        continue;
        //                    }
                          
        //                    else
        //                    {
        //                        CancelCount++;

        //                        if (item.UpdateMessage == null || item.UpdateMessage.ToStr().StartsWith("Failed"))
        //                        {

        //                            new WebDataClassesDataContext().stp_UpdateFetchedBookingStatus(item.ID, null, "Success", DateTime.Now, AppVars.LoginObj.UserName.ToStr());
        //                        }
        //                        continue;

        //                    }
        //                }
                       
        //            }

               
        //            objMaster = new BookingBO();

        //            if (item.BookingAccount != null)
        //            {
        //                Companyid = item.BookingAccount.AccountId;

        //                if (item.BookingAccount.SystemJobId != null)
        //                {
        //                    objMaster.GetByPrimaryKey(item.BookingAccount.SystemJobId);

        //                    if (objMaster.Current == null)
        //                        objMaster.New();
        //                }
        //                else
        //                    objMaster.New();

        //            }
        //            else
        //            {
                 
        //                    Companyid = null;
        //                   // objMaster = new BookingBO();
        //                    objMaster.New();
        //            }
        //            //New Account Booking (17 Dec 2012)
        //            if (Companyid != null)
        //            {
        //                objMaster.Current.IsCompanyWise = true;

        //            }

        //            objMaster.Current.CompanyId = Companyid;
        //            //

               


        //            objMaster.Current.BookingDate = DateTime.Now;
                  

        //            vehicle =item.VehicleName.ToStr().Trim().ToLower();

        //            if (!string.IsNullOrEmpty(vehicle))
        //            {
        //                Fleet_VehicleType objvehicle = General.GetQueryable<Fleet_VehicleType>(null)
        //                                     .FirstOrDefault(c => c.VehicleType.Trim().ToLower() == vehicle);

        //                if (objvehicle != null)
        //                {
        //                    objMaster.Current.VehicleTypeId = objvehicle.Id;
        //                }
        //            }


        //            if (DateTime.TryParse(item.PickupDate.ToStr(),out pickupDate))
        //            {
                      
        //                pickupTime = item.PickupTime.ToStr();

        //                if (TimeSpan.TryParse(pickupTime, out pickupTimeSpan))
        //                {
        //                    pickupDate = pickupDate.ToDate() + pickupTimeSpan;
        //                }

        //                objMaster.Current.PickupDateTime = pickupDate;

        //            }
    
                 
        //            if(item.JourneyTypeId.ToInt()==Enums.JOURNEY_TYPES.RETURN)
        //            {
        //                if (!string.IsNullOrEmpty(item.ReturnDate))
        //                {
        //                    if (DateTime.TryParse(item.ReturnDate, out pickupDate))
        //                    {

        //                        if (TimeSpan.TryParse(item.ReturnTime.ToStr(), out pickupTimeSpan))
        //                        {
        //                            pickupDate = pickupDate.ToDate() + pickupTimeSpan;
        //                            objMaster.Current.ReturnPickupDateTime = pickupDate;
        //                        }
        //                    }
        //                }
        //            }




        //            if (AppVars.ListOfWebsites == null || AppVars.ListOfWebsites.Count==1)
        //            {

        //                objMaster.Current.SubcompanyId = AppVars.objSubCompany.Id;
        //            }
        //            else
        //            {
        //                if (AppVars.ListOfWebsites.Count > 1)
        //                {
        //                   objMaster.Current.SubcompanyId =  AppVars.ListOfWebsites.FirstOrDefault(c => c.Name == item.CLient.Name).DefaultIfEmpty().SubCompanyId;
                        
        //                }
        //            }



        //            objMaster.Current.OrderNo = item.FromOther.ToStr();

        //            objMaster.Current.FromAddress = item.From.ToStr().ToUpper().Trim();
        //            objMaster.Current.ToAddress = item.To.ToStr().ToUpper().Trim();

        //            objMaster.Current.NoofPassengers = item.NoOfPassangers.ToInt();
        //            objMaster.Current.NoofLuggages = item.NoOfLuggages.ToInt();
        //            objMaster.Current.NoofHandLuggages = item.NoOfHandLuggages.ToInt();

        //            objMaster.Current.FareRate = item.Fares.ToDecimal();
        //            objMaster.Current.ReturnFareRate = item.ReturnFares.ToDecimal();
        //            objMaster.Current.CustomerPrice = item.Fares.ToDecimal();



        //            if (objMaster.Current.CompanyId != null)
        //                objMaster.Current.CompanyPrice = item.Fares.ToDecimal();


        //            objMaster.Current.CustomerName = item.Name.ToStr().ToProperCase().Trim();
                   
        //            objMaster.Current.CustomerPhoneNo = item.PassengerPhone.ToStr().Trim();
        //            objMaster.Current.CustomerEmail = item.Email.ToStr().Trim();


        //            objMaster.Current.CustomerMobileNo = item.PassengerMobile.ToStr().Trim();


        //            if (item.SpecialRequirement.ToStr().Contains(">"))
        //            {
        //                string[] specArr = item.SpecialRequirement.ToStr().Split('>');

        //                if (specArr.Count() == 2)
        //                {
        //                    objMaster.Current.SpecialRequirements = specArr[0].ToStr();


        //                    if (specArr[1].ToStr().Length > 30)
        //                    {
        //                        objMaster.Current.AddLog = specArr[1].ToStr().Substring(0,20) + " (Web)";

        //                    }
        //                    else
        //                    {

        //                        objMaster.Current.AddLog = specArr[1].ToStr() + " (Web)";
        //                    }
        //                }

        //            }
        //            else
        //            {

        //                objMaster.Current.SpecialRequirements = item.SpecialRequirement.ToStr();
                     
        //                objMaster.Current.AddLog = AppVars.LoginObj.UserName;
        //            }



        //            objMaster.Current.AddOn = DateTime.Now;
        //            objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();

        //            locType = item.FromType.ToStr().Trim().ToUpper();

        //            int FromLocTypeId = 0;
        //            int ToLocTypeId = 0;

        //            if (locType == Enums.LOCATION_TYPENAMES.ADDRESS)
        //                FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

        //            else if (locType == Enums.LOCATION_TYPENAMES.AIRPORT)
        //            {
        //                FromLocTypeId = Enums.LOCATION_TYPES.AIRPORT;

        //                objMaster.Current.FromDoorNo = item.FlightNumber.ToStr().Trim();
        //                objMaster.Current.FromStreet = item.ComingFrom.ToStr().Trim();


        //            }

        //            else if (locType == Enums.LOCATION_TYPENAMES.POSTCODE)
        //                FromLocTypeId = Enums.LOCATION_TYPES.POSTCODE;


        //            else if (locType == Enums.LOCATION_TYPENAMES.STATION || locType == Enums.LOCATION_TYPENAMES.STATIONS
        //                || locType == Enums.LOCATION_TYPENAMES.RAILWAYSTATION)
        //                FromLocTypeId = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;

        //            else if (locType == Enums.LOCATION_TYPENAMES.SEAPORTS || locType == Enums.LOCATION_TYPENAMES.CRUISEPORT
        //            || locType == Enums.LOCATION_TYPENAMES.CRUISEPORTS)
        //                FromLocTypeId = Enums.LOCATION_TYPES.SEAPORTS;



        //            objMaster.Current.FromLocTypeId = FromLocTypeId;             

        //            loc = item.From.ToStr().ToUpper().StripNewLine().Trim();

        //            objMaster.Current.FromAddress = loc;

        //            if (FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT || FromLocTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
        //            {
        //                Gen_Location objLoc = General.GetQueryable<Gen_Location>(null).FirstOrDefault(c =>c.Address!=null && c.Address.ToUpper().Trim() == loc);
        //                if (objLoc != null)
        //                {
        //                    objMaster.Current.FromLocId = objLoc.Id;
        //                }

        //            }


        //            if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE || FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
        //            {
        //                objMaster.Current.FromDoorNo = item.FromDoor.ToStr().Trim();
        //                objMaster.Current.FromStreet = item.FromStreet.ToStr().Trim();

        //                objMaster.Current.FromPostCode =General.GetPostCodeMatch(item.From.ToStr().ToUpper().Trim());
        //            }



        //            // To Location 
        //            locType = item.ToType.ToStr().Trim().ToUpper();


        //            loc = item.To.ToStr().ToUpper().StripNewLine().Trim();

        //            if (locType == Enums.LOCATION_TYPENAMES.ADDRESS)
        //                ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

        //            else if (locType == Enums.LOCATION_TYPENAMES.AIRPORT)
        //                ToLocTypeId = Enums.LOCATION_TYPES.AIRPORT;

        //            else if (locType == Enums.LOCATION_TYPENAMES.POSTCODE)
        //                ToLocTypeId = Enums.LOCATION_TYPES.POSTCODE;


        //            else if (locType == Enums.LOCATION_TYPENAMES.STATION || locType == Enums.LOCATION_TYPENAMES.STATIONS
        //                || locType == Enums.LOCATION_TYPENAMES.RAILWAYSTATION)
        //                ToLocTypeId = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;

        //            else if (locType == Enums.LOCATION_TYPENAMES.SEAPORTS || locType == Enums.LOCATION_TYPENAMES.CRUISEPORT
        //                || locType == Enums.LOCATION_TYPENAMES.CRUISEPORTS)
        //                ToLocTypeId = Enums.LOCATION_TYPES.SEAPORTS;


        //            objMaster.Current.ToLocTypeId = ToLocTypeId;

        //            objMaster.Current.ToAddress = loc;

        //            if (ToLocTypeId == Enums.LOCATION_TYPES.AIRPORT || ToLocTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
        //            {
        //                Gen_Location objLoc = General.GetQueryable<Gen_Location>(null).FirstOrDefault(c => c.Address.ToUpper().Trim() == loc);
        //                if (objLoc != null)
        //                {
        //                    objMaster.Current.ToLocId = objLoc.Id;

        //                }

        //            }


        //            objMaster.Current.ToDoorNo = item.ToDoorNo.ToStr().Trim();
        //            objMaster.Current.ToStreet = item.ToStreet.ToStr().Trim();
        //            objMaster.Current.ToPostCode =General.GetPostCodeMatch(loc);


        //            objMaster.Current.JourneyTypeId = item.JourneyTypeId;
        //            objMaster.Current.SpecialRequirements = item.SpecialRequirement.ToStr().Replace("\n","\r\n");

        //            int paymentTypeId = Enums.PAYMENT_TYPES.CASH;
        //            if (item.PaymentType.ToStr().ToLower() == "cash")
        //            {
        //                paymentTypeId = Enums.PAYMENT_TYPES.CASH;
        //            }
        //            else if (item.PaymentType.ToStr().ToLower() == "credit card")
        //            {
        //                paymentTypeId = Enums.PAYMENT_TYPES.CREDIT_CARD;
        //            }
        //            else if (item.PaymentType.ToStr().ToLower().EndsWith("account"))
        //            {
        //                paymentTypeId = Enums.PAYMENT_TYPES.BANK_ACCOUNT;
        //            }

        //            objMaster.Current.PaymentTypeId = paymentTypeId;


                  


                           
        //                if(item.Status.ToStr().Trim().ToLower() == "waiting")
        //                  objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.ONLINE;
        //                else
        //                    objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.WEB;




        //                var list = new WebDataClassesDataContext().WebBooking_ViaLocations.Where(c => c.BookingId == item.ID).ToList();



        //                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
        //                IList<Booking_ViaLocation> savedList = objMaster.Current.Booking_ViaLocations;

        //                List<Booking_ViaLocation> listofDetail = new List<Booking_ViaLocation>();

        //                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "Off2Exectuive")
        //                {



        //                    string[] arr = null;
        //                    foreach (var viaItem in list)
        //                    {

        //                        Booking_ViaLocation objVia = new Booking_ViaLocation();

        //                        objVia.ViaLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

        //                        objVia.ViaLocTypeValue = "Address";
        //                        objVia.ViaLocId = null;
        //                        objVia.ViaLocLabel = null;



        //                        arr = viaItem.ViaLocValue.ToStr().Split(new string[] { "%%%" }, StringSplitOptions.RemoveEmptyEntries);
        //                        //    NETHERWOOD PLACE COWGROVE WIMBORNE DORSET BH21 4EN%%%ertyuui | 0000000%%%MR Smith

        //                        if (arr.Count() > 1)
        //                        {
        //                            objVia.ViaLocTypeLabel = "Via";


        //                            string val = arr[0].ToStr() + "%%%";

        //                            string viaPax = viaItem.ViaLocValue.Replace(val, "").Trim();


        //                            string[] viaPaxArr = viaPax.Split(new string[] { "%%%" }, StringSplitOptions.RemoveEmptyEntries);

        //                            int cnt = 1;
        //                            viaPax = string.Join("\r\n", viaPaxArr.Select(c => cnt++.ToStr() + ". " + c.ToStr()).ToArray<string>());


        //                            objVia.ViaLocTypeLabel = viaPax.ToStr();
        //                            objVia.ViaLocValue = arr[0].ToStr();

        //                        }
        //                        else
        //                        {
        //                            objVia.ViaLocTypeLabel = "";
        //                            objVia.ViaLocValue = viaItem.ViaLocValue.ToStr();

        //                        }



        //                        listofDetail.Add(objVia);



        //                    }


        //                    int cnter = 1;
        //                    via = string.Join("\r\n", list.Select(c => cnter++.ToStr() + ". " + c.ViaLocValue.ToStr().Replace("%%%", Environment.NewLine)).ToArray<string>());





        //                }
        //                else
        //                {



        //                    listofDetail = (from r in list
        //                                    select new Booking_ViaLocation
        //                                    {

        //                                        //  BookingId = r.Cells[COLS.MASTERID].Value.ToLong(),
        //                                        ViaLocTypeId = Enums.LOCATION_TYPES.ADDRESS,
        //                                        ViaLocTypeLabel = "Via",
        //                                        ViaLocTypeValue = "Address",

        //                                        ViaLocId = null,
        //                                        ViaLocLabel = null,
        //                                        ViaLocValue = r.ViaLocValue

        //                                    }).ToList();


        //                    int cnt = 1;
        //                    via = string.Join("\r\n", list.Select(c => cnt++.ToStr() + ". " + c.ViaLocValue.ToStr()).ToArray<string>());
        //                }



        //                if (listofDetail.Count > 0)
        //                {

        //                    Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);
        //                }
                    
                    
        //            objMaster.Current.BookingNo = item.ID.ToStr();

        //            objMaster.Current.IsCommissionWise = false;
        //            objMaster.Current.DriverCommission = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();
        //            objMaster.Current.DriverCommissionType = "Percent";


        //            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() && AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool())
        //            {


        //                objMaster.Current.ZoneId = GetZoneId(objMaster.Current.FromAddress.ToUpper().ToStr().Trim());       
                               
        //                 objMaster.Current.DropOffZoneId = GetZoneId(objMaster.Current.ToAddress.ToUpper().ToStr().Trim());                   
        //            }



        //            if (this.HasWebBookingTab)
        //            {
        //                objMaster.Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING_WEBBOOKING;

        //            }


        //            objMaster.Save();

        //            refNo = objMaster.Current.BookingNo.ToStr();

        //            UpdateWebBooking(item.ID, "Success", true,objMaster.Current.Id);


                


        //            if (ListofFetechedJobs != null)
        //            {

        //                // New Work for Accept/Decline Booking
        //                ListofFetechedJobs.Add(new Booking
        //                {
        //                    Id = objMaster.Current.Id,
        //                    BookingNo = objMaster.Current.BookingNo,
        //                    CustomerName = objMaster.Current.CustomerName,
        //                    CustomerPhoneNo = objMaster.Current.CustomerPhoneNo,
        //                    CustomerMobileNo = objMaster.Current.CustomerMobileNo,
        //                    CustomerEmail = objMaster.Current.CustomerEmail,
        //                    FromAddress = objMaster.Current.FromAddress,

        //                    ToAddress = objMaster.Current.ToAddress,
        //                    PickupDateTime = objMaster.Current.PickupDateTime,
        //                    BookingTypeId=objMaster.Current.BookingTypeId,
        //                    AddBy=item.CLientID.ToInt(),
        //                     FromDoorNo=objMaster.Current.FromDoorNo,
        //                     FromStreet=objMaster.Current.FromStreet,
        //              //       FromComing=item.Status,
        //                     DistanceString=via,
        //                 FareRate=objMaster.Current.FareRate
        //                });
        //            }


        //            success++;
        //            if (!string.IsNullOrEmpty(AppVars.objPolicyConfiguration.WebBookingText.ToStr().Trim()) && !string.IsNullOrEmpty(objMaster.Current.CustomerMobileNo))
        //            {

        //                new Thread(delegate()
        //                {
        //                    SendSMS(GetMessage(AppVars.objPolicyConfiguration.WebBookingText.ToStr(),objMaster.Current), objMaster.Current.CustomerMobileNo);
        //                }).Start();

        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            UpdateWebBooking(item.ID, "Failed : " + objMaster.ShowErrors() + " Exception : " +ex.Message.ToStr(), false,null);

        //            failed++;

        //        }
        //    }


        //    // Summary

                     

          
          
        //    int totalBookings = listofBookings.Count;

        //    if (CancelCount != totalBookings)
        //    {


        //        string summary = "Total Bookings Fetched : " + totalBookings;


        //        this.CaptionText = summary;
        //        this.ContentText = "Success : " + success.ToStr() + Environment.NewLine + "Failed : " + failed.ToStr();
        //        this.ContentText += Environment.NewLine + "Ref No : " + refNo;
        //        summary += Environment.NewLine + this.ContentText;
        //        return summary;
        //    }
        //    else
        //        return "";

           
            
        //}

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
            catch 
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
            catch
            {
                // ENUtils.ShowMessage(ex.Message);
                return "";
            }
        }


        private string GetFormattedContactNo(string number)
        {
            try
            {

                if (number.StartsWith("044") == true)
                    number = number.Substring(2);

                else if (number.StartsWith("440") == true)
                    number = number.Substring(2);

                else if (number.StartsWith("447") == true)
                {
                    number = number.Substring(2);
                    number = number.Insert(0, "0");
                }


                if (number.Contains(" "))
                    number = number.Replace(" ", "").Trim();
            }
            catch
            {


            }

            return number;

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
            catch 
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

                new WebDataClassesDataContext().stp_UpdateFetchedBookingStatus(Id, systemJobId, msg, DateTime.Now, AppVars.LoginObj.UserName.ToStr());

            }
            catch 
            {


            }


        }

        private  string GetCountryCodeString(string mobNumber,string phoneNumber)
        {
            string rtn = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(mobNumber.ToStr().Trim()) && string.IsNullOrEmpty(phoneNumber.ToStr().Trim()))
                    rtn = "";
                else
                {
                    var elems = XElement.Parse(Resources.Resource1.CountryCodes).Elements();

                    XElement elem = elems.FirstOrDefault(c => mobNumber.StartsWith(c.Attribute("phoneCode").Value));

                    if (elem != null)                   
                        rtn ="Mobile Number Country Code - "+ elem.Attribute("name").Value.ToStr() + " " + elem.Attribute("phoneCode").Value.ToStr()+" , ";


                    elem = elems.FirstOrDefault(c => phoneNumber.StartsWith(c.Attribute("phoneCode").Value));

                    if (elem != null)
                        rtn += "Home Number Country Code - " + elem.Attribute("name").Value.ToStr() + " " + elem.Attribute("phoneCode").Value.ToStr();

             
           
                }
            }
            catch
            {
            }

            return rtn;
        }

       

        #region IDisposable Members

     
        public void Dispose()
        {
           // this.Dispose();
        }

        #endregion
    }
}
