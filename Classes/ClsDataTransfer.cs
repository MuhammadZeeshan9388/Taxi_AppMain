using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain
{
   public  class ClsDataTransfer
    {
        private string _RemoteIPs;

        public string RemoteIPs
        {
            get { return _RemoteIPs; }
            set { _RemoteIPs = value; }
        }


        private decimal? _BookingFormType;

        public decimal? BookingFormType
        {
            get { return _BookingFormType; }
            set { _BookingFormType = value; }
        }


        private string _ClientType;

        public string ClientType
        {
            get { return _ClientType; }
            set { _ClientType = value; }
        }

        private decimal? _AirportPickupCharges;

        public decimal? AirportPickupCharges
        {
            get { return _AirportPickupCharges; }
            set { _AirportPickupCharges = value; }
        }
        private decimal? _ApplyStartRateWithInMiles;

        public decimal? ApplyStartRateWithInMiles
        {
            get { return _ApplyStartRateWithInMiles; }
            set { _ApplyStartRateWithInMiles = value; }
        }
        private bool? _AutoCalculateFares;

        public bool? AutoCalculateFares
        {
            get { return _AutoCalculateFares; }
            set { _AutoCalculateFares = value; }
        }
        private bool? _AutoCloseDrvPopup;

        public bool? AutoCloseDrvPopup
        {
            get { return _AutoCloseDrvPopup; }
            set { _AutoCloseDrvPopup = value; }
        }
        private string _BaseAddress;

        public string BaseAddress
        {
            get { return _BaseAddress; }
            set { _BaseAddress = value; }
        }
        private decimal _DeadMileage;

        public decimal DeadMileage
        {
            get { return _DeadMileage; }
            set { _DeadMileage = value; }
        }
        private int? _DefaultBookingTypeId;

        public int? DefaultBookingTypeId
        {
            get { return _DefaultBookingTypeId; }
            set { _DefaultBookingTypeId = value; }
        }
        private string _DefaultClientId;

        public string DefaultClientId
        {
            get { return _DefaultClientId; }
            set { _DefaultClientId = value; }
        }
        private int? _DefaultVehicleTypeId;

        public int? DefaultVehicleTypeId
        {
            get { return _DefaultVehicleTypeId; }
            set { _DefaultVehicleTypeId = value; }
        }
        private bool? _DisableDriverCommissionTick;

        public bool? DisableDriverCommissionTick
        {
            get { return _DisableDriverCommissionTick; }
            set { _DisableDriverCommissionTick = value; }
        }
        private bool? _DisablePopupNotifications;

        public bool? DisablePopupNotifications
        {
            get { return _DisablePopupNotifications; }
            set { _DisablePopupNotifications = value; }
        }
        private int? _DiscountForReturnedJourneyPercent;

        public int? DiscountForReturnedJourneyPercent
        {
            get { return _DiscountForReturnedJourneyPercent; }
            set { _DiscountForReturnedJourneyPercent = value; }
        }
        private int? _DiscountForWRJourneyPercent;

        public int? DiscountForWRJourneyPercent
        {
            get { return _DiscountForWRJourneyPercent; }
            set { _DiscountForWRJourneyPercent = value; }
        }
        private decimal? _DriverCommissionPerBooking;

        public decimal? DriverCommissionPerBooking
        {
            get { return _DriverCommissionPerBooking; }
            set { _DriverCommissionPerBooking = value; }
        }
        private bool? _EnableApplyStartRateWithInMiles;

        public bool? EnableApplyStartRateWithInMiles
        {
            get { return _EnableApplyStartRateWithInMiles; }
            set { _EnableApplyStartRateWithInMiles = value; }
        }
        private bool? _EnableAutoDespatch;

        public bool? EnableAutoDespatch
        {
            get { return _EnableAutoDespatch; }
            set { _EnableAutoDespatch = value; }
        }
        private bool? _EnableBidding;

        public bool? EnableBidding
        {
            get { return _EnableBidding; }
            set { _EnableBidding = value; }
        }
        private bool? _EnableBookingOtherCharges;

        public bool? EnableBookingOtherCharges
        {
            get { return _EnableBookingOtherCharges; }
            set { _EnableBookingOtherCharges = value; }
        }
        private bool? _EnablePassengerText;

        public bool? EnablePassengerText
        {
            get { return _EnablePassengerText; }
            set { _EnablePassengerText = value; }
        }
        private bool? _EnablePDA;

        public bool? EnablePDA
        {
            get { return _EnablePDA; }
            set { _EnablePDA = value; }
        }
        private bool? _EnablePeakOffPeakFares;

        public bool? EnablePeakOffPeakFares
        {
            get { return _EnablePeakOffPeakFares; }
            set { _EnablePeakOffPeakFares = value; }
        }
        private bool? _EnablePOI;

        public bool? EnablePOI
        {
            get { return _EnablePOI; }
            set { _EnablePOI = value; }
        }
        private bool? _EnableQuotation;

        public bool? EnableQuotation
        {
            get { return _EnableQuotation; }
            set { _EnableQuotation = value; }
        }
        private bool? _EnableReplaceNoToZoneSuggesstion;

        public bool? EnableReplaceNoToZoneSuggesstion
        {
            get { return _EnableReplaceNoToZoneSuggesstion; }
            set { _EnableReplaceNoToZoneSuggesstion = value; }
        }
        private bool? _EnableWebBooking;

        public bool? EnableWebBooking
        {
            get { return _EnableWebBooking; }
            set { _EnableWebBooking = value; }
        }
        private bool? _EnableZoneWiseFares;

        public bool? EnableZoneWiseFares
        {
            get { return _EnableZoneWiseFares; }
            set { _EnableZoneWiseFares = value; }
        }
        private decimal? _FareMeterRoundedCalc;

        public decimal? FareMeterRoundedCalc
        {
            get { return _FareMeterRoundedCalc; }
            set { _FareMeterRoundedCalc = value; }
        }
        private bool? _HasAirportDropOffCharges;

        public bool? HasAirportDropOffCharges
        {
            get { return _HasAirportDropOffCharges; }
            set { _HasAirportDropOffCharges = value; }
        }
        private bool? _HasMultipleAirportPickupCharges;

        public bool? HasMultipleAirportPickupCharges
        {
            get { return _HasMultipleAirportPickupCharges; }
            set { _HasMultipleAirportPickupCharges = value; }
        }
        private bool? _HasWebAccounts;

        public bool? HasWebAccounts
        {
            get { return _HasWebAccounts; }
            set { _HasWebAccounts = value; }
        }
        private bool? _IsAllCounty;

        public bool? IsAllCounty
        {
            get { return _IsAllCounty; }
            set { _IsAllCounty = value; }
        }
        private bool? _IsListenAll;

        public bool? IsListenAll
        {
            get { return _IsListenAll; }
            set { _IsListenAll = value; }
        }
        private string _ListenerIP;

        public string ListenerIP
        {
            get { return _ListenerIP; }
            set { _ListenerIP = value; }
        }
        private int? _MapType;

        public int? MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }

       private int? _NoofMilesStartRate;

       public int? NoofMilesStartRate
       {
           get { return _NoofMilesStartRate; }
           set { _NoofMilesStartRate = value; }
       }


       private string _ExcludedDrivers;

       public string ExcludedDrivers
       {
           get { return _ExcludedDrivers; }
           set { _ExcludedDrivers = value; }
       }

       private string _PermanentNotes;

       public string PermanentNotes
       {
           get { return _PermanentNotes; }
           set { _PermanentNotes = value; }
       }

       private string _DataString;

       public string DataString
       {
           get { return _DataString; }
           set { _DataString = value; }
       }


       private decimal? _OffPeakMinFares;

       public decimal? OffPeakMinFares
       {
           get { return _OffPeakMinFares; }
           set { _OffPeakMinFares = value; }
       }
       private bool? _PreferredMileageFares;

       public bool? PreferredMileageFares
       {
           get { return _PreferredMileageFares; }
           set { _PreferredMileageFares = value; }
       }
       private bool? _PreferredShortestDistance;

       public bool? PreferredShortestDistance
       {
           get { return _PreferredShortestDistance; }
           set { _PreferredShortestDistance = value; }
       }
       private string _PriorityPostCodes;

       public string PriorityPostCodes
       {
           get { return _PriorityPostCodes; }
           set { _PriorityPostCodes = value; }
       }
       private int _RecentAddressesFrequency;

       public int RecentAddressesFrequency
       {
           get { return _RecentAddressesFrequency; }
           set { _RecentAddressesFrequency = value; }
       }
       private decimal? _RoundJourneyMiles;

       public decimal? RoundJourneyMiles
       {
           get { return _RoundJourneyMiles; }
           set { _RoundJourneyMiles = value; }
       }
       private bool? _RoundMileageFares;

       public bool? RoundMileageFares
       {
           get { return _RoundMileageFares; }
           set { _RoundMileageFares = value; }
       }
       private decimal? _RoundUpTo;

       public decimal? RoundUpTo
       {
           get { return _RoundUpTo; }
           set { _RoundUpTo = value; }
       }
       private bool? _StripDoorNoOnAddress;

       public bool? StripDoorNoOnAddress
       {
           get { return _StripDoorNoOnAddress; }
           set { _StripDoorNoOnAddress = value; }
       }
       private int? _ZoneWiseFareType;

       public int? ZoneWiseFareType
       {
           get { return _ZoneWiseFareType; }
           set { _ZoneWiseFareType = value; }
       }
       private bool? _ViaPointFareCalculationType;

       public bool? ViaPointFareCalculationType
       {
           get { return _ViaPointFareCalculationType; }
           set { _ViaPointFareCalculationType = value; }
       }
       private bool? _UseMultipleSMSGateways;

       public bool? UseMultipleSMSGateways
       {
           get { return _UseMultipleSMSGateways; }
           set { _UseMultipleSMSGateways = value; }
       }


       private string _CallerId_CustomerName;

       public string CallerId_CustomerName
       {
           get { return _CallerId_CustomerName; }
           set { _CallerId_CustomerName = value; }
       }
       private string _CallerId_PhoneNo;

       public string CallerId_PhoneNo
       {
           get { return _CallerId_PhoneNo; }
           set { _CallerId_PhoneNo = value; }
       }
       private decimal? _CallerId_Fares;

       public decimal? CallerId_Fares
       {
           get { return _CallerId_Fares; }
           set { _CallerId_Fares = value; }
       }
       private string _CallerId_From;

       public string CallerId_From
       {
           get { return _CallerId_From; }
           set { _CallerId_From = value; }
       }
       private string _CallerId_To;

       public string CallerId_To
       {
           get { return _CallerId_To; }
           set { _CallerId_To = value; }
       }
       private string _CallerId_FromDoorNo;

       public string CallerId_FromDoorNo
       {
           get { return _CallerId_FromDoorNo; }
           set { _CallerId_FromDoorNo = value; }
       }
       private string _CallerId_ToDoorNo;

       public string CallerId_ToDoorNo
       {
           get { return _CallerId_ToDoorNo; }
           set { _CallerId_ToDoorNo = value; }
       }
       private int? _CallerId_CompanyId;

       public int? CallerId_CompanyId
       {
           get { return _CallerId_CompanyId; }
           set { _CallerId_CompanyId = value; }
       }
       private string _CallerId_ViaString;

       public string CallerId_ViaString
       {
           get { return _CallerId_ViaString; }
           set { _CallerId_ViaString = value; }
       }

       private string _MapKey;

       public string MapKey
       {
           get { return _MapKey; }
           set { _MapKey = value; }
       }


       private int? _CallerId_fromLocTypeId;

       public int? CallerId_fromLocTypeId
       {
           get { return _CallerId_fromLocTypeId; }
           set { _CallerId_fromLocTypeId = value; }
       }
       private int? _CallerId_toLocTypeId;

       public int? CallerId_toLocTypeId
       {
           get { return _CallerId_toLocTypeId; }
           set { _CallerId_toLocTypeId = value; }
       }
       private int? _CallerId_FromLocId;

       public int? CallerId_FromLocId
       {
           get { return _CallerId_FromLocId; }
           set { _CallerId_FromLocId = value; }
       }
       private int? _CallerId_ToLocId;

       public int? CallerId_ToLocId
       {
           get { return _CallerId_ToLocId; }
           set { _CallerId_ToLocId = value; }
       }
       private string _CallerId_Email;

       public string CallerId_Email
       {
           get { return _CallerId_Email; }
           set { _CallerId_Email = value; }
       }



       private bool? _CallerId_IsReverse;

       public bool? CallerId_IsReverse
       {
           get { return _CallerId_IsReverse; }
           set { _CallerId_IsReverse = value; }
       }


       private bool? _CallerId_IsAccountCall;

       public bool? CallerId_IsAccountCall
       {
           get { return _CallerId_IsAccountCall; }
           set { _CallerId_IsAccountCall = value; }
       }


       private int? _CallerId_SubCompanyId;

       public int? CallerId_SubCompanyId
       {
           get { return _CallerId_SubCompanyId; }
           set { _CallerId_SubCompanyId = value; }
       }


       private bool _CanTransferJob;

       public bool CanTransferJob
       {
           get { return _CanTransferJob; }
           set { _CanTransferJob = value; }
       }


       private bool? _EnableAdvanceBookingSMSConfirmation;

       public bool? EnableAdvanceBookingSMSConfirmation
       {
           get { return _EnableAdvanceBookingSMSConfirmation; }
           set { _EnableAdvanceBookingSMSConfirmation = value; }
       }

       private int? _AdvanceBookingSMSConfirmationMins;

       public int? AdvanceBookingSMSConfirmationMins
       {
           get { return _AdvanceBookingSMSConfirmationMins; }
           set { _AdvanceBookingSMSConfirmationMins = value; }
       }



    }
}
