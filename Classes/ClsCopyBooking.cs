using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain
{
   public class ClsCopyBooking
    {

        private string _FromAddress;

        public string FromAddress
        {
            get { return _FromAddress; }
            set { _FromAddress = value; }
        }
        private string _ToAddress;

        public string ToAddress
        {
            get { return _ToAddress; }
            set { _ToAddress = value; }
        }
        private int? _FromLocTypeId;

        public int? FromLocTypeId
        {
            get { return _FromLocTypeId; }
            set { _FromLocTypeId = value; }
        }
        private int? _ToLocTypeId;

        public int? ToLocTypeId
        {
            get { return _ToLocTypeId; }
            set { _ToLocTypeId = value; }
        }
        private string _CustomerName;

        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private string _CustomerPhoneNo;

        public string CustomerPhoneNo
        {
            get { return _CustomerPhoneNo; }
            set { _CustomerPhoneNo = value; }
        }
        private string _CustomerMobileNo;

        public string CustomerMobileNo
        {
            get { return _CustomerMobileNo; }
            set { _CustomerMobileNo = value; }
        }
        private string _SpecialRequirements;

        public string SpecialRequirements
        {
            get { return _SpecialRequirements; }
            set { _SpecialRequirements = value; }
        }
        private string _FromDoorNo;

        public string FromDoorNo
        {
            get { return _FromDoorNo; }
            set { _FromDoorNo = value; }
        }
        private string _ToDoorNo;

        public string ToDoorNo
        {
            get { return _ToDoorNo; }
            set { _ToDoorNo = value; }
        }
        private decimal? _FareRate;

        public decimal? FareRate
        {
            get { return _FareRate; }
            set { _FareRate = value; }
        }
        private decimal? _CompanyPrice;

        public decimal? CompanyPrice
        {
            get { return _CompanyPrice; }
            set { _CompanyPrice = value; }
        }
        private decimal? _ParkingCharges;

        public decimal? ParkingCharges
        {
            get { return _ParkingCharges; }
            set { _ParkingCharges = value; }
        }
        private decimal? _WaitingCharges;

        public decimal? WaitingCharges
        {
            get { return _WaitingCharges; }
            set { _WaitingCharges = value; }
        }
        private int? _CompanyId;

        public int? CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        private string _OrderNo;

        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }
        private int? _VehicleTypeId;

        public int? VehicleTypeId
        {
            get { return _VehicleTypeId; }
            set { _VehicleTypeId = value; }
        }
        private int? _DepartmentId;

        public int? DepartmentId
        {
            get { return _DepartmentId; }
            set { _DepartmentId = value; }
        }
        private int? _ZoneId;

        public int? ZoneId
        {
            get { return _ZoneId; }
            set { _ZoneId = value; }
        }



        private int? _DropOffZoneId;

        public int? DropOffZoneId
        {
            get { return _DropOffZoneId; }
            set { _DropOffZoneId = value; }
        }

       
        private string _CustomerEmail;

        public string CustomerEmail
        {
            get { return _CustomerEmail; }
            set { _CustomerEmail = value; }
        }
        private string _ViaString;

        public string ViaString
        {
            get { return _ViaString; }
            set { _ViaString = value; }
        }
        private int? _JourneyTypeId;

        public int? JourneyTypeId
        {
            get { return _JourneyTypeId; }
            set { _JourneyTypeId = value; }
        }
        private decimal? _ServiceCharges;

        public decimal? ServiceCharges
        {
            get { return _ServiceCharges; }
            set { _ServiceCharges = value; }
        }
        private string _BookedBy;

        public string BookedBy
        {
            get { return _BookedBy; }
            set { _BookedBy = value; }
        }
        private string _FromStreet;

        public string FromStreet
        {
            get { return _FromStreet; }
            set { _FromStreet = value; }
        }
        private string _ToStreet;

        public string ToStreet
        {
            get { return _ToStreet; }
            set { _ToStreet = value; }
        }

        private int? _PaymentTypeId;

        public int? PaymentTypeId
        {
            get { return _PaymentTypeId; }
            set { _PaymentTypeId = value; }
        }
        private int? _SubcompanyId;

        public int? SubcompanyId
        {
            get { return _SubcompanyId; }
            set { _SubcompanyId = value; }
        }

        private DateTime? _PickupDateTime;

        public DateTime? PickupDateTime
        {
            get { return _PickupDateTime; }
            set { _PickupDateTime = value; }
        }

        private bool? _IsCompanyWise;

        public bool? IsCompanyWise
        {
            get { return _IsCompanyWise; }
            set { _IsCompanyWise = value; }
        }
        private int? _BookingTypeId;

        public int? BookingTypeId
        {
            get { return _BookingTypeId; }
            set { _BookingTypeId = value; }
        }
        private decimal? _CustomerPrice;

        public decimal? CustomerPrice
        {
            get { return _CustomerPrice; }
            set { _CustomerPrice = value; }
        }
        private int? _ReturnFromLocId;

        public int? ReturnFromLocId
        {
            get { return _ReturnFromLocId; }
            set { _ReturnFromLocId = value; }
        }
        private string _PupilNo;

        public string PupilNo
        {
            get { return _PupilNo; }
            set { _PupilNo = value; }
        }
        private bool? _JobTakenByCompany;

        public bool? JobTakenByCompany
        {
            get { return _JobTakenByCompany; }
            set { _JobTakenByCompany = value; }
        }
        private int? _AgentCommissionPercent;

        public int? AgentCommissionPercent
        {
            get { return _AgentCommissionPercent; }
            set { _AgentCommissionPercent = value; }
        }
        private string _FromFlightNo;

        public string FromFlightNo
        {
            get { return _FromFlightNo; }
            set { _FromFlightNo = value; }
        }
        private decimal? _AgentCommission;

        public decimal? AgentCommission
        {
            get { return _AgentCommission; }
            set { _AgentCommission = value; }
        }
        private DateTime? _ReturnPickupDateTime;

        public DateTime? ReturnPickupDateTime
        {
            get { return _ReturnPickupDateTime; }
            set { _ReturnPickupDateTime = value; }
        }
        private int? _NoofPassengers;

        public int? NoofPassengers
        {
            get { return _NoofPassengers; }
            set { _NoofPassengers = value; }
        }
        private int? _NoofLuggages;

        public int? NoofLuggages
        {
            get { return _NoofLuggages; }
            set { _NoofLuggages = value; }
        }
        private decimal? _ReturnFareRate;

        public decimal? ReturnFareRate
        {
            get { return _ReturnFareRate; }
            set { _ReturnFareRate = value; }
        }
        private decimal? _WaitingMins;

        public decimal? WaitingMins
        {
            get { return _WaitingMins; }
            set { _WaitingMins = value; }
        }
        private decimal? _ExtraDropCharges;

        public decimal? ExtraDropCharges
        {
            get { return _ExtraDropCharges; }
            set { _ExtraDropCharges = value; }
        }
        private decimal? _MeetAndGreetCharges;

        public decimal? MeetAndGreetCharges
        {
            get { return _MeetAndGreetCharges; }
            set { _MeetAndGreetCharges = value; }
        }
        private decimal? _CongtionCharges;

        public decimal? CongtionCharges
        {
            get { return _CongtionCharges; }
            set { _CongtionCharges = value; }
        }
        private decimal? _TotalCharges;

        public decimal? TotalCharges
        {
            get { return _TotalCharges; }
            set { _TotalCharges = value; }
        }
        private string _ToPostCode;

        public string ToPostCode
        {
            get { return _ToPostCode; }
            set { _ToPostCode = value; }
        }
        private string _FromPostCode;

        public string FromPostCode
        {
            get { return _FromPostCode; }
            set { _FromPostCode = value; }
        }
        private bool? _IsCommissionWise;

        public bool? IsCommissionWise
        {
            get { return _IsCommissionWise; }
            set { _IsCommissionWise = value; }
        }
        private string _DriverCommissionType;

        public string DriverCommissionType
        {
            get { return _DriverCommissionType; }
            set { _DriverCommissionType = value; }
        }
        private decimal? _DriverCommission;

        public decimal? DriverCommission
        {
            get { return _DriverCommission; }
            set { _DriverCommission = value; }
        }
        private bool? _IsQuotedPrice;

        public bool? IsQuotedPrice
        {
            get { return _IsQuotedPrice; }
            set { _IsQuotedPrice = value; }
        }
        private string _DistanceString;

        public string DistanceString
        {
            get { return _DistanceString; }
            set { _DistanceString = value; }
        }


        private bool? _IsQuotation;

        public bool? IsQuotation
        {
            get { return _IsQuotation; }
            set { _IsQuotation = value; }
        }

        private bool? _AutoDespatch;

        public bool? AutoDespatch
        {
            get { return _AutoDespatch; }
            set { _AutoDespatch = value; }
        }

        private bool? _IsBidding;

        public bool? IsBidding
        {
            get { return _IsBidding; }
            set { _IsBidding = value; }
        }

        private DateTime? _AutoDespatchTime;

        public DateTime? AutoDespatchTime
        {
            get { return _AutoDespatchTime; }
            set { _AutoDespatchTime = value; }
        }
      

            
    }
}
