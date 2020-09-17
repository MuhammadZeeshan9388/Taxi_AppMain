using System.ComponentModel;
using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
[global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.PoolBooking")]


public partial class PoolBooking : INotifyPropertyChanging, INotifyPropertyChanged
{

    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

    private long _Id;

    private System.Nullable<int> _FromLocTypeId;

    private System.Nullable<int> _ToLocTypeId;

    private System.Nullable<int> _FromLocId;

    private System.Nullable<int> _ToLocId;

    private System.Nullable<int> _VehicleTypeId;

    private System.Nullable<int> _DriverId;

    private System.Nullable<int> _ReturnDriverId;

    private System.Nullable<int> _CustomerId;

    private string _CustomerName;

    private string _CustomerEmail;

    private string _CustomerPhoneNo;

    private string _CustomerMobileNo;

    private System.Nullable<int> _JourneyTypeId;

    private string _BookingNo;

    private System.Nullable<System.DateTime> _BookingDate;

    private System.Nullable<int> _NoofPassengers;

    private System.Nullable<int> _NoofLuggages;

    private System.Nullable<int> _NoofHandLuggages;

    private System.Nullable<System.DateTime> _PickupDateTime;

    private System.Nullable<System.DateTime> _ReturnPickupDateTime;

    private System.Nullable<bool> _IsCompanyWise;

    private System.Nullable<int> _CompanyId;

    private System.Nullable<decimal> _FareRate;

    private System.Nullable<int> _PaymentTypeId;

    private string _SpecialRequirements;

    private string _FromAddress;

    private string _ToAddress;

    private string _FromPostCode;

    private string _ToPostCode;

    private string _FromDoorNo;

    private string _ToDoorNo;

    private string _FromStreet;

    private string _ToStreet;

    private string _FromFlightNo;

    private string _FromComing;

    private System.Nullable<int> _BookingStatusId;

    private string _DistanceString;

    private System.Nullable<bool> _AutoDespatch;

    private System.Nullable<System.DateTime> _AutoDespatchTime;

    private System.Nullable<System.DateTime> _AddOn;

    private System.Nullable<int> _AddBy;

    private string _AddLog;

    private System.Nullable<System.DateTime> _EditOn;

    private System.Nullable<int> _EditBy;

    private string _EditLog;

    private string _OrderNo;

    private string _PupilNo;

    private System.Nullable<decimal> _ParkingCharges;

    private System.Nullable<decimal> _WaitingCharges;

    private System.Nullable<decimal> _ExtraDropCharges;

    private System.Nullable<decimal> _MeetAndGreetCharges;

    private System.Nullable<decimal> _CongtionCharges;

    private System.Nullable<decimal> _TotalCharges;

    private System.Nullable<long> _DepartmentId;

    private System.Nullable<decimal> _ReturnFareRate;

    private System.Nullable<System.DateTime> _ArrivalDateTime;

    private System.Nullable<long> _MasterJobId;

    private System.Nullable<bool> _DisablePassengerSMS;

    private System.Nullable<bool> _DisableDriverSMS;

    private System.Nullable<bool> _IsCommissionWise;

    private System.Nullable<decimal> _DriverCommission;

    private System.Nullable<System.DateTime> _DespatchDateTime;

    private System.Nullable<System.DateTime> _JobOfferDateTime;

    private System.Nullable<int> _BookingTypeId;

    private string _DriverCommissionType;

    private System.Nullable<bool> _IsBidding;

    private System.Nullable<bool> _IsQuotation;

    private System.Nullable<int> _CostCenterId;

    private System.Nullable<decimal> _CashRate;

    private System.Nullable<decimal> _AccountRate;

    private System.Nullable<decimal> _WaitingMins;

    private System.Nullable<decimal> _ExtraMile;

    private System.Nullable<System.DateTime> _AcceptedDateTime;

    private System.Nullable<System.DateTime> _POBDateTime;

    private System.Nullable<System.DateTime> _STCDateTime;

    private System.Nullable<System.DateTime> _ClearedDateTime;

    private string _CancelReason;

    private System.Nullable<decimal> _TotalTravelledMiles;

    private System.Nullable<decimal> _CompanyPrice;

    private System.Nullable<int> _InvoicePaymentTypeId;

    private System.Nullable<int> _FleetMasterId;

    private string _Despatchby;

    private System.Nullable<int> _ZoneId;

    private System.Nullable<int> _DropOffZoneId;

    private System.Nullable<int> _ReturnFromLocId;

    private System.Nullable<int> _SubcompanyId;

    private System.Nullable<decimal> _CustomerPrice;

    private System.Nullable<int> _AgentCommissionPercent;

    private System.Nullable<decimal> _AgentCommission;

    private System.Nullable<bool> _JobTakenByCompany;

    private System.Nullable<System.DateTime> _FlightDepartureDate;

    private System.Nullable<int> _NoOfChilds;

    private System.Nullable<long> _GroupJobId;

    private string _RoomNo;

    private string _FaresPostedFrom;

    private System.Nullable<long> _AdvanceBookingId;

    private string _BoundType;

    private string _BookedBy;

    private string _BabySeats;

    private string _FromOther;

    private string _ToOther;

    private string _EscortName;

    private System.Nullable<bool> _IsProcessed;

    private System.Nullable<long> _EscortId;

    private System.Nullable<decimal> _EscortPrice;

    private string _PaymentComments;

    private System.Nullable<bool> _DisableDriverCommissionTick;

    private System.Nullable<int> _SecondaryPaymentTypeId;

    private System.Nullable<decimal> _CashFares;

    private System.Nullable<bool> _IsConfirmedDriver;

    private System.Nullable<decimal> _DeadMileage;

    private System.Nullable<int> _OnHoldWaitingMins;

    private System.Nullable<System.DateTime> _OnHoldDateTime;

    private string _OnHoldReason;

    private string _CustomerCreditCardDetails;

    private string _CompanyCreditCardDetails;

    private System.Nullable<bool> _EnableFareMeter;

    private System.Nullable<System.DateTime> _PriceBiddingExpiryDate;

    private System.Nullable<int> _DriverWaitingMins;

    private string _JobCancelledBy;

    private System.Nullable<System.DateTime> _JobCancelledOn;

    private string _CallRefNo;

    private System.Nullable<bool> _IsReverse;

    private string _ViaString;

    private string _NotesString;

    private System.Nullable<long> _TransferJobId;

    private System.Nullable<decimal> _TransferJobCommission;

    private System.Nullable<int> _PartyId;

    private System.Nullable<long> _OnlineBookingId;

    private System.Nullable<decimal> _ServiceCharges;

    private System.Nullable<bool> _ApplyServiceCharges;

    private string _JobCode;

    private System.Nullable<decimal> _ExtraPickup;

    private System.Nullable<decimal> _ExtraDropOff;

    private System.Nullable<decimal> _TipAmount;

    private System.Nullable<decimal> _JourneyTimeInMins;

    private System.Nullable<bool> _IsQuotedPrice;

    private System.Nullable<System.DateTime> _ReAutoDespatchTime;

    private string _AttributeValues;

    private System.Nullable<int> _SMSType;

    private System.Nullable<decimal> _PaidAmount;

    private string _DefaultClientId;

    private int _ClientID;

    private string _ClientName;

    private long _ClientBookingId;

    private System.Nullable<long> _ClientMasterJobId;

 

    private EntitySet<PoolBooking> _Bookings;

    private EntityRef<PoolBooking> _Booking1;

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnFromLocTypeIdChanging(System.Nullable<int> value);
    partial void OnFromLocTypeIdChanged();
    partial void OnToLocTypeIdChanging(System.Nullable<int> value);
    partial void OnToLocTypeIdChanged();
    partial void OnFromLocIdChanging(System.Nullable<int> value);
    partial void OnFromLocIdChanged();
    partial void OnToLocIdChanging(System.Nullable<int> value);
    partial void OnToLocIdChanged();
    partial void OnVehicleTypeIdChanging(System.Nullable<int> value);
    partial void OnVehicleTypeIdChanged();
    partial void OnDriverIdChanging(System.Nullable<int> value);
    partial void OnDriverIdChanged();
    partial void OnReturnDriverIdChanging(System.Nullable<int> value);
    partial void OnReturnDriverIdChanged();
    partial void OnCustomerIdChanging(System.Nullable<int> value);
    partial void OnCustomerIdChanged();
    partial void OnCustomerNameChanging(string value);
    partial void OnCustomerNameChanged();
    partial void OnCustomerEmailChanging(string value);
    partial void OnCustomerEmailChanged();
    partial void OnCustomerPhoneNoChanging(string value);
    partial void OnCustomerPhoneNoChanged();
    partial void OnCustomerMobileNoChanging(string value);
    partial void OnCustomerMobileNoChanged();
    partial void OnJourneyTypeIdChanging(System.Nullable<int> value);
    partial void OnJourneyTypeIdChanged();
    partial void OnBookingNoChanging(string value);
    partial void OnBookingNoChanged();
    partial void OnBookingDateChanging(System.Nullable<System.DateTime> value);
    partial void OnBookingDateChanged();
    partial void OnNoofPassengersChanging(System.Nullable<int> value);
    partial void OnNoofPassengersChanged();
    partial void OnNoofLuggagesChanging(System.Nullable<int> value);
    partial void OnNoofLuggagesChanged();
    partial void OnNoofHandLuggagesChanging(System.Nullable<int> value);
    partial void OnNoofHandLuggagesChanged();
    partial void OnPickupDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnPickupDateTimeChanged();
    partial void OnReturnPickupDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnReturnPickupDateTimeChanged();
    partial void OnIsCompanyWiseChanging(System.Nullable<bool> value);
    partial void OnIsCompanyWiseChanged();
    partial void OnCompanyIdChanging(System.Nullable<int> value);
    partial void OnCompanyIdChanged();
    partial void OnFareRateChanging(System.Nullable<decimal> value);
    partial void OnFareRateChanged();
    partial void OnPaymentTypeIdChanging(System.Nullable<int> value);
    partial void OnPaymentTypeIdChanged();
    partial void OnSpecialRequirementsChanging(string value);
    partial void OnSpecialRequirementsChanged();
    partial void OnFromAddressChanging(string value);
    partial void OnFromAddressChanged();
    partial void OnToAddressChanging(string value);
    partial void OnToAddressChanged();
    partial void OnFromPostCodeChanging(string value);
    partial void OnFromPostCodeChanged();
    partial void OnToPostCodeChanging(string value);
    partial void OnToPostCodeChanged();
    partial void OnFromDoorNoChanging(string value);
    partial void OnFromDoorNoChanged();
    partial void OnToDoorNoChanging(string value);
    partial void OnToDoorNoChanged();
    partial void OnFromStreetChanging(string value);
    partial void OnFromStreetChanged();
    partial void OnToStreetChanging(string value);
    partial void OnToStreetChanged();
    partial void OnFromFlightNoChanging(string value);
    partial void OnFromFlightNoChanged();
    partial void OnFromComingChanging(string value);
    partial void OnFromComingChanged();
    partial void OnBookingStatusIdChanging(System.Nullable<int> value);
    partial void OnBookingStatusIdChanged();
    partial void OnDistanceStringChanging(string value);
    partial void OnDistanceStringChanged();
    partial void OnAutoDespatchChanging(System.Nullable<bool> value);
    partial void OnAutoDespatchChanged();
    partial void OnAutoDespatchTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnAutoDespatchTimeChanged();
    partial void OnAddOnChanging(System.Nullable<System.DateTime> value);
    partial void OnAddOnChanged();
    partial void OnAddByChanging(System.Nullable<int> value);
    partial void OnAddByChanged();
    partial void OnAddLogChanging(string value);
    partial void OnAddLogChanged();
    partial void OnEditOnChanging(System.Nullable<System.DateTime> value);
    partial void OnEditOnChanged();
    partial void OnEditByChanging(System.Nullable<int> value);
    partial void OnEditByChanged();
    partial void OnEditLogChanging(string value);
    partial void OnEditLogChanged();
    partial void OnOrderNoChanging(string value);
    partial void OnOrderNoChanged();
    partial void OnPupilNoChanging(string value);
    partial void OnPupilNoChanged();
    partial void OnParkingChargesChanging(System.Nullable<decimal> value);
    partial void OnParkingChargesChanged();
    partial void OnWaitingChargesChanging(System.Nullable<decimal> value);
    partial void OnWaitingChargesChanged();
    partial void OnExtraDropChargesChanging(System.Nullable<decimal> value);
    partial void OnExtraDropChargesChanged();
    partial void OnMeetAndGreetChargesChanging(System.Nullable<decimal> value);
    partial void OnMeetAndGreetChargesChanged();
    partial void OnCongtionChargesChanging(System.Nullable<decimal> value);
    partial void OnCongtionChargesChanged();
    partial void OnTotalChargesChanging(System.Nullable<decimal> value);
    partial void OnTotalChargesChanged();
    partial void OnDepartmentIdChanging(System.Nullable<long> value);
    partial void OnDepartmentIdChanged();
    partial void OnReturnFareRateChanging(System.Nullable<decimal> value);
    partial void OnReturnFareRateChanged();
    partial void OnArrivalDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnArrivalDateTimeChanged();
    partial void OnMasterJobIdChanging(System.Nullable<long> value);
    partial void OnMasterJobIdChanged();
    partial void OnDisablePassengerSMSChanging(System.Nullable<bool> value);
    partial void OnDisablePassengerSMSChanged();
    partial void OnDisableDriverSMSChanging(System.Nullable<bool> value);
    partial void OnDisableDriverSMSChanged();
    partial void OnIsCommissionWiseChanging(System.Nullable<bool> value);
    partial void OnIsCommissionWiseChanged();
    partial void OnDriverCommissionChanging(System.Nullable<decimal> value);
    partial void OnDriverCommissionChanged();
    partial void OnDespatchDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnDespatchDateTimeChanged();
    partial void OnJobOfferDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnJobOfferDateTimeChanged();
    partial void OnBookingTypeIdChanging(System.Nullable<int> value);
    partial void OnBookingTypeIdChanged();
    partial void OnDriverCommissionTypeChanging(string value);
    partial void OnDriverCommissionTypeChanged();
    partial void OnIsBiddingChanging(System.Nullable<bool> value);
    partial void OnIsBiddingChanged();
    partial void OnIsQuotationChanging(System.Nullable<bool> value);
    partial void OnIsQuotationChanged();
    partial void OnCostCenterIdChanging(System.Nullable<int> value);
    partial void OnCostCenterIdChanged();
    partial void OnCashRateChanging(System.Nullable<decimal> value);
    partial void OnCashRateChanged();
    partial void OnAccountRateChanging(System.Nullable<decimal> value);
    partial void OnAccountRateChanged();
    partial void OnWaitingMinsChanging(System.Nullable<decimal> value);
    partial void OnWaitingMinsChanged();
    partial void OnExtraMileChanging(System.Nullable<decimal> value);
    partial void OnExtraMileChanged();
    partial void OnAcceptedDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnAcceptedDateTimeChanged();
    partial void OnPOBDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnPOBDateTimeChanged();
    partial void OnSTCDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnSTCDateTimeChanged();
    partial void OnClearedDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnClearedDateTimeChanged();
    partial void OnCancelReasonChanging(string value);
    partial void OnCancelReasonChanged();
    partial void OnTotalTravelledMilesChanging(System.Nullable<decimal> value);
    partial void OnTotalTravelledMilesChanged();
    partial void OnCompanyPriceChanging(System.Nullable<decimal> value);
    partial void OnCompanyPriceChanged();
    partial void OnInvoicePaymentTypeIdChanging(System.Nullable<int> value);
    partial void OnInvoicePaymentTypeIdChanged();
    partial void OnFleetMasterIdChanging(System.Nullable<int> value);
    partial void OnFleetMasterIdChanged();
    partial void OnDespatchbyChanging(string value);
    partial void OnDespatchbyChanged();
    partial void OnZoneIdChanging(System.Nullable<int> value);
    partial void OnZoneIdChanged();
    partial void OnDropOffZoneIdChanging(System.Nullable<int> value);
    partial void OnDropOffZoneIdChanged();
    partial void OnReturnFromLocIdChanging(System.Nullable<int> value);
    partial void OnReturnFromLocIdChanged();
    partial void OnSubcompanyIdChanging(System.Nullable<int> value);
    partial void OnSubcompanyIdChanged();
    partial void OnCustomerPriceChanging(System.Nullable<decimal> value);
    partial void OnCustomerPriceChanged();
    partial void OnAgentCommissionPercentChanging(System.Nullable<int> value);
    partial void OnAgentCommissionPercentChanged();
    partial void OnAgentCommissionChanging(System.Nullable<decimal> value);
    partial void OnAgentCommissionChanged();
    partial void OnJobTakenByCompanyChanging(System.Nullable<bool> value);
    partial void OnJobTakenByCompanyChanged();
    partial void OnFlightDepartureDateChanging(System.Nullable<System.DateTime> value);
    partial void OnFlightDepartureDateChanged();
    partial void OnNoOfChildsChanging(System.Nullable<int> value);
    partial void OnNoOfChildsChanged();
    partial void OnGroupJobIdChanging(System.Nullable<long> value);
    partial void OnGroupJobIdChanged();
    partial void OnRoomNoChanging(string value);
    partial void OnRoomNoChanged();
    partial void OnFaresPostedFromChanging(string value);
    partial void OnFaresPostedFromChanged();
    partial void OnAdvanceBookingIdChanging(System.Nullable<long> value);
    partial void OnAdvanceBookingIdChanged();
    partial void OnBoundTypeChanging(string value);
    partial void OnBoundTypeChanged();
    partial void OnBookedByChanging(string value);
    partial void OnBookedByChanged();
    partial void OnBabySeatsChanging(string value);
    partial void OnBabySeatsChanged();
    partial void OnFromOtherChanging(string value);
    partial void OnFromOtherChanged();
    partial void OnToOtherChanging(string value);
    partial void OnToOtherChanged();
    partial void OnEscortNameChanging(string value);
    partial void OnEscortNameChanged();
    partial void OnIsProcessedChanging(System.Nullable<bool> value);
    partial void OnIsProcessedChanged();
    partial void OnEscortIdChanging(System.Nullable<long> value);
    partial void OnEscortIdChanged();
    partial void OnEscortPriceChanging(System.Nullable<decimal> value);
    partial void OnEscortPriceChanged();
    partial void OnPaymentCommentsChanging(string value);
    partial void OnPaymentCommentsChanged();
    partial void OnDisableDriverCommissionTickChanging(System.Nullable<bool> value);
    partial void OnDisableDriverCommissionTickChanged();
    partial void OnSecondaryPaymentTypeIdChanging(System.Nullable<int> value);
    partial void OnSecondaryPaymentTypeIdChanged();
    partial void OnCashFaresChanging(System.Nullable<decimal> value);
    partial void OnCashFaresChanged();
    partial void OnIsConfirmedDriverChanging(System.Nullable<bool> value);
    partial void OnIsConfirmedDriverChanged();
    partial void OnDeadMileageChanging(System.Nullable<decimal> value);
    partial void OnDeadMileageChanged();
    partial void OnOnHoldWaitingMinsChanging(System.Nullable<int> value);
    partial void OnOnHoldWaitingMinsChanged();
    partial void OnOnHoldDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnOnHoldDateTimeChanged();
    partial void OnOnHoldReasonChanging(string value);
    partial void OnOnHoldReasonChanged();
    partial void OnCustomerCreditCardDetailsChanging(string value);
    partial void OnCustomerCreditCardDetailsChanged();
    partial void OnCompanyCreditCardDetailsChanging(string value);
    partial void OnCompanyCreditCardDetailsChanged();
    partial void OnEnableFareMeterChanging(System.Nullable<bool> value);
    partial void OnEnableFareMeterChanged();
    partial void OnPriceBiddingExpiryDateChanging(System.Nullable<System.DateTime> value);
    partial void OnPriceBiddingExpiryDateChanged();
    partial void OnDriverWaitingMinsChanging(System.Nullable<int> value);
    partial void OnDriverWaitingMinsChanged();
    partial void OnJobCancelledByChanging(string value);
    partial void OnJobCancelledByChanged();
    partial void OnJobCancelledOnChanging(System.Nullable<System.DateTime> value);
    partial void OnJobCancelledOnChanged();
    partial void OnCallRefNoChanging(string value);
    partial void OnCallRefNoChanged();
    partial void OnIsReverseChanging(System.Nullable<bool> value);
    partial void OnIsReverseChanged();
    partial void OnViaStringChanging(string value);
    partial void OnViaStringChanged();
    partial void OnNotesStringChanging(string value);
    partial void OnNotesStringChanged();
    partial void OnTransferJobIdChanging(System.Nullable<long> value);
    partial void OnTransferJobIdChanged();
    partial void OnTransferJobCommissionChanging(System.Nullable<decimal> value);
    partial void OnTransferJobCommissionChanged();
    partial void OnPartyIdChanging(System.Nullable<int> value);
    partial void OnPartyIdChanged();
    partial void OnOnlineBookingIdChanging(System.Nullable<long> value);
    partial void OnOnlineBookingIdChanged();
    partial void OnServiceChargesChanging(System.Nullable<decimal> value);
    partial void OnServiceChargesChanged();
    partial void OnApplyServiceChargesChanging(System.Nullable<bool> value);
    partial void OnApplyServiceChargesChanged();
    partial void OnJobCodeChanging(string value);
    partial void OnJobCodeChanged();
    partial void OnExtraPickupChanging(System.Nullable<decimal> value);
    partial void OnExtraPickupChanged();
    partial void OnExtraDropOffChanging(System.Nullable<decimal> value);
    partial void OnExtraDropOffChanged();
    partial void OnTipAmountChanging(System.Nullable<decimal> value);
    partial void OnTipAmountChanged();
    partial void OnJourneyTimeInMinsChanging(System.Nullable<decimal> value);
    partial void OnJourneyTimeInMinsChanged();
    partial void OnIsQuotedPriceChanging(System.Nullable<bool> value);
    partial void OnIsQuotedPriceChanged();
    partial void OnReAutoDespatchTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnReAutoDespatchTimeChanged();
    partial void OnAttributeValuesChanging(string value);
    partial void OnAttributeValuesChanged();
    partial void OnSMSTypeChanging(System.Nullable<int> value);
    partial void OnSMSTypeChanged();
    partial void OnPaidAmountChanging(System.Nullable<decimal> value);
    partial void OnPaidAmountChanged();
    partial void OnDefaultClientIdChanging(string value);
    partial void OnDefaultClientIdChanged();
    partial void OnClientIDChanging(int value);
    partial void OnClientIDChanged();
    partial void OnClientNameChanging(string value);
    partial void OnClientNameChanged();
    partial void OnClientBookingIdChanging(long value);
    partial void OnClientBookingIdChanged();
    partial void OnClientMasterJobIdChanging(System.Nullable<long> value);
    partial void OnClientMasterJobIdChanged();
    #endregion

    public PoolBooking()
    {
       // this._Client_Biddings = new EntitySet<Client_Bidding>(new Action<Client_Bidding>(this.attach_Client_Biddings), new Action<Client_Bidding>(this.detach_Client_Biddings));
        this._Bookings = new EntitySet<PoolBooking>(new Action<PoolBooking>(this.attach_Bookings), new Action<PoolBooking>(this.detach_Bookings));
        this._Booking1 = default(EntityRef<PoolBooking>);
        OnCreated();
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
    public long Id
    {
        get
        {
            return this._Id;
        }
        set
        {
            if ((this._Id != value))
            {
                this.OnIdChanging(value);
                this.SendPropertyChanging();
                this._Id = value;
                this.SendPropertyChanged("Id");
                this.OnIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromLocTypeId", DbType = "Int")]
    public System.Nullable<int> FromLocTypeId
    {
        get
        {
            return this._FromLocTypeId;
        }
        set
        {
            if ((this._FromLocTypeId != value))
            {
                this.OnFromLocTypeIdChanging(value);
                this.SendPropertyChanging();
                this._FromLocTypeId = value;
                this.SendPropertyChanged("FromLocTypeId");
                this.OnFromLocTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToLocTypeId", DbType = "Int")]
    public System.Nullable<int> ToLocTypeId
    {
        get
        {
            return this._ToLocTypeId;
        }
        set
        {
            if ((this._ToLocTypeId != value))
            {
                this.OnToLocTypeIdChanging(value);
                this.SendPropertyChanging();
                this._ToLocTypeId = value;
                this.SendPropertyChanged("ToLocTypeId");
                this.OnToLocTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromLocId", DbType = "Int")]
    public System.Nullable<int> FromLocId
    {
        get
        {
            return this._FromLocId;
        }
        set
        {
            if ((this._FromLocId != value))
            {
                this.OnFromLocIdChanging(value);
                this.SendPropertyChanging();
                this._FromLocId = value;
                this.SendPropertyChanged("FromLocId");
                this.OnFromLocIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToLocId", DbType = "Int")]
    public System.Nullable<int> ToLocId
    {
        get
        {
            return this._ToLocId;
        }
        set
        {
            if ((this._ToLocId != value))
            {
                this.OnToLocIdChanging(value);
                this.SendPropertyChanging();
                this._ToLocId = value;
                this.SendPropertyChanged("ToLocId");
                this.OnToLocIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VehicleTypeId", DbType = "Int")]
    public System.Nullable<int> VehicleTypeId
    {
        get
        {
            return this._VehicleTypeId;
        }
        set
        {
            if ((this._VehicleTypeId != value))
            {
                this.OnVehicleTypeIdChanging(value);
                this.SendPropertyChanging();
                this._VehicleTypeId = value;
                this.SendPropertyChanged("VehicleTypeId");
                this.OnVehicleTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DriverId", DbType = "Int")]
    public System.Nullable<int> DriverId
    {
        get
        {
            return this._DriverId;
        }
        set
        {
            if ((this._DriverId != value))
            {
                this.OnDriverIdChanging(value);
                this.SendPropertyChanging();
                this._DriverId = value;
                this.SendPropertyChanged("DriverId");
                this.OnDriverIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReturnDriverId", DbType = "Int")]
    public System.Nullable<int> ReturnDriverId
    {
        get
        {
            return this._ReturnDriverId;
        }
        set
        {
            if ((this._ReturnDriverId != value))
            {
                this.OnReturnDriverIdChanging(value);
                this.SendPropertyChanging();
                this._ReturnDriverId = value;
                this.SendPropertyChanged("ReturnDriverId");
                this.OnReturnDriverIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerId", DbType = "Int")]
    public System.Nullable<int> CustomerId
    {
        get
        {
            return this._CustomerId;
        }
        set
        {
            if ((this._CustomerId != value))
            {
                this.OnCustomerIdChanging(value);
                this.SendPropertyChanging();
                this._CustomerId = value;
                this.SendPropertyChanged("CustomerId");
                this.OnCustomerIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerName", DbType = "VarChar(100)")]
    public string CustomerName
    {
        get
        {
            return this._CustomerName;
        }
        set
        {
            if ((this._CustomerName != value))
            {
                this.OnCustomerNameChanging(value);
                this.SendPropertyChanging();
                this._CustomerName = value;
                this.SendPropertyChanged("CustomerName");
                this.OnCustomerNameChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerEmail", DbType = "VarChar(50)")]
    public string CustomerEmail
    {
        get
        {
            return this._CustomerEmail;
        }
        set
        {
            if ((this._CustomerEmail != value))
            {
                this.OnCustomerEmailChanging(value);
                this.SendPropertyChanging();
                this._CustomerEmail = value;
                this.SendPropertyChanged("CustomerEmail");
                this.OnCustomerEmailChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerPhoneNo", DbType = "VarChar(50)")]
    public string CustomerPhoneNo
    {
        get
        {
            return this._CustomerPhoneNo;
        }
        set
        {
            if ((this._CustomerPhoneNo != value))
            {
                this.OnCustomerPhoneNoChanging(value);
                this.SendPropertyChanging();
                this._CustomerPhoneNo = value;
                this.SendPropertyChanged("CustomerPhoneNo");
                this.OnCustomerPhoneNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerMobileNo", DbType = "VarChar(50)")]
    public string CustomerMobileNo
    {
        get
        {
            return this._CustomerMobileNo;
        }
        set
        {
            if ((this._CustomerMobileNo != value))
            {
                this.OnCustomerMobileNoChanging(value);
                this.SendPropertyChanging();
                this._CustomerMobileNo = value;
                this.SendPropertyChanged("CustomerMobileNo");
                this.OnCustomerMobileNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JourneyTypeId", DbType = "Int")]
    public System.Nullable<int> JourneyTypeId
    {
        get
        {
            return this._JourneyTypeId;
        }
        set
        {
            if ((this._JourneyTypeId != value))
            {
                this.OnJourneyTypeIdChanging(value);
                this.SendPropertyChanging();
                this._JourneyTypeId = value;
                this.SendPropertyChanged("JourneyTypeId");
                this.OnJourneyTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BookingNo", DbType = "VarChar(50)")]
    public string BookingNo
    {
        get
        {
            return this._BookingNo;
        }
        set
        {
            if ((this._BookingNo != value))
            {
                this.OnBookingNoChanging(value);
                this.SendPropertyChanging();
                this._BookingNo = value;
                this.SendPropertyChanged("BookingNo");
                this.OnBookingNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BookingDate", DbType = "DateTime")]
    public System.Nullable<System.DateTime> BookingDate
    {
        get
        {
            return this._BookingDate;
        }
        set
        {
            if ((this._BookingDate != value))
            {
                this.OnBookingDateChanging(value);
                this.SendPropertyChanging();
                this._BookingDate = value;
                this.SendPropertyChanged("BookingDate");
                this.OnBookingDateChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoofPassengers", DbType = "Int")]
    public System.Nullable<int> NoofPassengers
    {
        get
        {
            return this._NoofPassengers;
        }
        set
        {
            if ((this._NoofPassengers != value))
            {
                this.OnNoofPassengersChanging(value);
                this.SendPropertyChanging();
                this._NoofPassengers = value;
                this.SendPropertyChanged("NoofPassengers");
                this.OnNoofPassengersChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoofLuggages", DbType = "Int")]
    public System.Nullable<int> NoofLuggages
    {
        get
        {
            return this._NoofLuggages;
        }
        set
        {
            if ((this._NoofLuggages != value))
            {
                this.OnNoofLuggagesChanging(value);
                this.SendPropertyChanging();
                this._NoofLuggages = value;
                this.SendPropertyChanged("NoofLuggages");
                this.OnNoofLuggagesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoofHandLuggages", DbType = "Int")]
    public System.Nullable<int> NoofHandLuggages
    {
        get
        {
            return this._NoofHandLuggages;
        }
        set
        {
            if ((this._NoofHandLuggages != value))
            {
                this.OnNoofHandLuggagesChanging(value);
                this.SendPropertyChanging();
                this._NoofHandLuggages = value;
                this.SendPropertyChanged("NoofHandLuggages");
                this.OnNoofHandLuggagesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PickupDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> PickupDateTime
    {
        get
        {
            return this._PickupDateTime;
        }
        set
        {
            if ((this._PickupDateTime != value))
            {
                this.OnPickupDateTimeChanging(value);
                this.SendPropertyChanging();
                this._PickupDateTime = value;
                this.SendPropertyChanged("PickupDateTime");
                this.OnPickupDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReturnPickupDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> ReturnPickupDateTime
    {
        get
        {
            return this._ReturnPickupDateTime;
        }
        set
        {
            if ((this._ReturnPickupDateTime != value))
            {
                this.OnReturnPickupDateTimeChanging(value);
                this.SendPropertyChanging();
                this._ReturnPickupDateTime = value;
                this.SendPropertyChanged("ReturnPickupDateTime");
                this.OnReturnPickupDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsCompanyWise", DbType = "Bit")]
    public System.Nullable<bool> IsCompanyWise
    {
        get
        {
            return this._IsCompanyWise;
        }
        set
        {
            if ((this._IsCompanyWise != value))
            {
                this.OnIsCompanyWiseChanging(value);
                this.SendPropertyChanging();
                this._IsCompanyWise = value;
                this.SendPropertyChanged("IsCompanyWise");
                this.OnIsCompanyWiseChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CompanyId", DbType = "Int")]
    public System.Nullable<int> CompanyId
    {
        get
        {
            return this._CompanyId;
        }
        set
        {
            if ((this._CompanyId != value))
            {
                this.OnCompanyIdChanging(value);
                this.SendPropertyChanging();
                this._CompanyId = value;
                this.SendPropertyChanged("CompanyId");
                this.OnCompanyIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FareRate", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> FareRate
    {
        get
        {
            return this._FareRate;
        }
        set
        {
            if ((this._FareRate != value))
            {
                this.OnFareRateChanging(value);
                this.SendPropertyChanging();
                this._FareRate = value;
                this.SendPropertyChanged("FareRate");
                this.OnFareRateChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PaymentTypeId", DbType = "Int")]
    public System.Nullable<int> PaymentTypeId
    {
        get
        {
            return this._PaymentTypeId;
        }
        set
        {
            if ((this._PaymentTypeId != value))
            {
                this.OnPaymentTypeIdChanging(value);
                this.SendPropertyChanging();
                this._PaymentTypeId = value;
                this.SendPropertyChanged("PaymentTypeId");
                this.OnPaymentTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SpecialRequirements", DbType = "VarChar(MAX)")]
    public string SpecialRequirements
    {
        get
        {
            return this._SpecialRequirements;
        }
        set
        {
            if ((this._SpecialRequirements != value))
            {
                this.OnSpecialRequirementsChanging(value);
                this.SendPropertyChanging();
                this._SpecialRequirements = value;
                this.SendPropertyChanged("SpecialRequirements");
                this.OnSpecialRequirementsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromAddress", DbType = "VarChar(200)")]
    public string FromAddress
    {
        get
        {
            return this._FromAddress;
        }
        set
        {
            if ((this._FromAddress != value))
            {
                this.OnFromAddressChanging(value);
                this.SendPropertyChanging();
                this._FromAddress = value;
                this.SendPropertyChanged("FromAddress");
                this.OnFromAddressChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToAddress", DbType = "VarChar(200)")]
    public string ToAddress
    {
        get
        {
            return this._ToAddress;
        }
        set
        {
            if ((this._ToAddress != value))
            {
                this.OnToAddressChanging(value);
                this.SendPropertyChanging();
                this._ToAddress = value;
                this.SendPropertyChanged("ToAddress");
                this.OnToAddressChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromPostCode", DbType = "VarChar(50)")]
    public string FromPostCode
    {
        get
        {
            return this._FromPostCode;
        }
        set
        {
            if ((this._FromPostCode != value))
            {
                this.OnFromPostCodeChanging(value);
                this.SendPropertyChanging();
                this._FromPostCode = value;
                this.SendPropertyChanged("FromPostCode");
                this.OnFromPostCodeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToPostCode", DbType = "VarChar(50)")]
    public string ToPostCode
    {
        get
        {
            return this._ToPostCode;
        }
        set
        {
            if ((this._ToPostCode != value))
            {
                this.OnToPostCodeChanging(value);
                this.SendPropertyChanging();
                this._ToPostCode = value;
                this.SendPropertyChanged("ToPostCode");
                this.OnToPostCodeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromDoorNo", DbType = "VarChar(100)")]
    public string FromDoorNo
    {
        get
        {
            return this._FromDoorNo;
        }
        set
        {
            if ((this._FromDoorNo != value))
            {
                this.OnFromDoorNoChanging(value);
                this.SendPropertyChanging();
                this._FromDoorNo = value;
                this.SendPropertyChanged("FromDoorNo");
                this.OnFromDoorNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToDoorNo", DbType = "VarChar(100)")]
    public string ToDoorNo
    {
        get
        {
            return this._ToDoorNo;
        }
        set
        {
            if ((this._ToDoorNo != value))
            {
                this.OnToDoorNoChanging(value);
                this.SendPropertyChanging();
                this._ToDoorNo = value;
                this.SendPropertyChanged("ToDoorNo");
                this.OnToDoorNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromStreet", DbType = "VarChar(200)")]
    public string FromStreet
    {
        get
        {
            return this._FromStreet;
        }
        set
        {
            if ((this._FromStreet != value))
            {
                this.OnFromStreetChanging(value);
                this.SendPropertyChanging();
                this._FromStreet = value;
                this.SendPropertyChanged("FromStreet");
                this.OnFromStreetChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToStreet", DbType = "VarChar(200)")]
    public string ToStreet
    {
        get
        {
            return this._ToStreet;
        }
        set
        {
            if ((this._ToStreet != value))
            {
                this.OnToStreetChanging(value);
                this.SendPropertyChanging();
                this._ToStreet = value;
                this.SendPropertyChanged("ToStreet");
                this.OnToStreetChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromFlightNo", DbType = "VarChar(50)")]
    public string FromFlightNo
    {
        get
        {
            return this._FromFlightNo;
        }
        set
        {
            if ((this._FromFlightNo != value))
            {
                this.OnFromFlightNoChanging(value);
                this.SendPropertyChanging();
                this._FromFlightNo = value;
                this.SendPropertyChanged("FromFlightNo");
                this.OnFromFlightNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromComing", DbType = "VarChar(200)")]
    public string FromComing
    {
        get
        {
            return this._FromComing;
        }
        set
        {
            if ((this._FromComing != value))
            {
                this.OnFromComingChanging(value);
                this.SendPropertyChanging();
                this._FromComing = value;
                this.SendPropertyChanged("FromComing");
                this.OnFromComingChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BookingStatusId", DbType = "Int")]
    public System.Nullable<int> BookingStatusId
    {
        get
        {
            return this._BookingStatusId;
        }
        set
        {
            if ((this._BookingStatusId != value))
            {
                this.OnBookingStatusIdChanging(value);
                this.SendPropertyChanging();
                this._BookingStatusId = value;
                this.SendPropertyChanged("BookingStatusId");
                this.OnBookingStatusIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DistanceString", DbType = "VarChar(200)")]
    public string DistanceString
    {
        get
        {
            return this._DistanceString;
        }
        set
        {
            if ((this._DistanceString != value))
            {
                this.OnDistanceStringChanging(value);
                this.SendPropertyChanging();
                this._DistanceString = value;
                this.SendPropertyChanged("DistanceString");
                this.OnDistanceStringChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoDespatch", DbType = "Bit")]
    public System.Nullable<bool> AutoDespatch
    {
        get
        {
            return this._AutoDespatch;
        }
        set
        {
            if ((this._AutoDespatch != value))
            {
                this.OnAutoDespatchChanging(value);
                this.SendPropertyChanging();
                this._AutoDespatch = value;
                this.SendPropertyChanged("AutoDespatch");
                this.OnAutoDespatchChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoDespatchTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> AutoDespatchTime
    {
        get
        {
            return this._AutoDespatchTime;
        }
        set
        {
            if ((this._AutoDespatchTime != value))
            {
                this.OnAutoDespatchTimeChanging(value);
                this.SendPropertyChanging();
                this._AutoDespatchTime = value;
                this.SendPropertyChanged("AutoDespatchTime");
                this.OnAutoDespatchTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AddOn", DbType = "DateTime")]
    public System.Nullable<System.DateTime> AddOn
    {
        get
        {
            return this._AddOn;
        }
        set
        {
            if ((this._AddOn != value))
            {
                this.OnAddOnChanging(value);
                this.SendPropertyChanging();
                this._AddOn = value;
                this.SendPropertyChanged("AddOn");
                this.OnAddOnChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AddBy", DbType = "Int")]
    public System.Nullable<int> AddBy
    {
        get
        {
            return this._AddBy;
        }
        set
        {
            if ((this._AddBy != value))
            {
                this.OnAddByChanging(value);
                this.SendPropertyChanging();
                this._AddBy = value;
                this.SendPropertyChanged("AddBy");
                this.OnAddByChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AddLog", DbType = "VarChar(50)")]
    public string AddLog
    {
        get
        {
            return this._AddLog;
        }
        set
        {
            if ((this._AddLog != value))
            {
                this.OnAddLogChanging(value);
                this.SendPropertyChanging();
                this._AddLog = value;
                this.SendPropertyChanged("AddLog");
                this.OnAddLogChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EditOn", DbType = "DateTime")]
    public System.Nullable<System.DateTime> EditOn
    {
        get
        {
            return this._EditOn;
        }
        set
        {
            if ((this._EditOn != value))
            {
                this.OnEditOnChanging(value);
                this.SendPropertyChanging();
                this._EditOn = value;
                this.SendPropertyChanged("EditOn");
                this.OnEditOnChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EditBy", DbType = "Int")]
    public System.Nullable<int> EditBy
    {
        get
        {
            return this._EditBy;
        }
        set
        {
            if ((this._EditBy != value))
            {
                this.OnEditByChanging(value);
                this.SendPropertyChanging();
                this._EditBy = value;
                this.SendPropertyChanged("EditBy");
                this.OnEditByChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EditLog", DbType = "VarChar(50)")]
    public string EditLog
    {
        get
        {
            return this._EditLog;
        }
        set
        {
            if ((this._EditLog != value))
            {
                this.OnEditLogChanging(value);
                this.SendPropertyChanging();
                this._EditLog = value;
                this.SendPropertyChanged("EditLog");
                this.OnEditLogChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OrderNo", DbType = "VarChar(50)")]
    public string OrderNo
    {
        get
        {
            return this._OrderNo;
        }
        set
        {
            if ((this._OrderNo != value))
            {
                this.OnOrderNoChanging(value);
                this.SendPropertyChanging();
                this._OrderNo = value;
                this.SendPropertyChanged("OrderNo");
                this.OnOrderNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PupilNo", DbType = "VarChar(MAX)")]
    public string PupilNo
    {
        get
        {
            return this._PupilNo;
        }
        set
        {
            if ((this._PupilNo != value))
            {
                this.OnPupilNoChanging(value);
                this.SendPropertyChanging();
                this._PupilNo = value;
                this.SendPropertyChanged("PupilNo");
                this.OnPupilNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ParkingCharges", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> ParkingCharges
    {
        get
        {
            return this._ParkingCharges;
        }
        set
        {
            if ((this._ParkingCharges != value))
            {
                this.OnParkingChargesChanging(value);
                this.SendPropertyChanging();
                this._ParkingCharges = value;
                this.SendPropertyChanged("ParkingCharges");
                this.OnParkingChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_WaitingCharges", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> WaitingCharges
    {
        get
        {
            return this._WaitingCharges;
        }
        set
        {
            if ((this._WaitingCharges != value))
            {
                this.OnWaitingChargesChanging(value);
                this.SendPropertyChanging();
                this._WaitingCharges = value;
                this.SendPropertyChanged("WaitingCharges");
                this.OnWaitingChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExtraDropCharges", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> ExtraDropCharges
    {
        get
        {
            return this._ExtraDropCharges;
        }
        set
        {
            if ((this._ExtraDropCharges != value))
            {
                this.OnExtraDropChargesChanging(value);
                this.SendPropertyChanging();
                this._ExtraDropCharges = value;
                this.SendPropertyChanged("ExtraDropCharges");
                this.OnExtraDropChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MeetAndGreetCharges", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> MeetAndGreetCharges
    {
        get
        {
            return this._MeetAndGreetCharges;
        }
        set
        {
            if ((this._MeetAndGreetCharges != value))
            {
                this.OnMeetAndGreetChargesChanging(value);
                this.SendPropertyChanging();
                this._MeetAndGreetCharges = value;
                this.SendPropertyChanged("MeetAndGreetCharges");
                this.OnMeetAndGreetChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CongtionCharges", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> CongtionCharges
    {
        get
        {
            return this._CongtionCharges;
        }
        set
        {
            if ((this._CongtionCharges != value))
            {
                this.OnCongtionChargesChanging(value);
                this.SendPropertyChanging();
                this._CongtionCharges = value;
                this.SendPropertyChanged("CongtionCharges");
                this.OnCongtionChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalCharges", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> TotalCharges
    {
        get
        {
            return this._TotalCharges;
        }
        set
        {
            if ((this._TotalCharges != value))
            {
                this.OnTotalChargesChanging(value);
                this.SendPropertyChanging();
                this._TotalCharges = value;
                this.SendPropertyChanged("TotalCharges");
                this.OnTotalChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DepartmentId", DbType = "BigInt")]
    public System.Nullable<long> DepartmentId
    {
        get
        {
            return this._DepartmentId;
        }
        set
        {
            if ((this._DepartmentId != value))
            {
                this.OnDepartmentIdChanging(value);
                this.SendPropertyChanging();
                this._DepartmentId = value;
                this.SendPropertyChanged("DepartmentId");
                this.OnDepartmentIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReturnFareRate", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> ReturnFareRate
    {
        get
        {
            return this._ReturnFareRate;
        }
        set
        {
            if ((this._ReturnFareRate != value))
            {
                this.OnReturnFareRateChanging(value);
                this.SendPropertyChanging();
                this._ReturnFareRate = value;
                this.SendPropertyChanged("ReturnFareRate");
                this.OnReturnFareRateChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ArrivalDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> ArrivalDateTime
    {
        get
        {
            return this._ArrivalDateTime;
        }
        set
        {
            if ((this._ArrivalDateTime != value))
            {
                this.OnArrivalDateTimeChanging(value);
                this.SendPropertyChanging();
                this._ArrivalDateTime = value;
                this.SendPropertyChanged("ArrivalDateTime");
                this.OnArrivalDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MasterJobId", DbType = "BigInt")]
    public System.Nullable<long> MasterJobId
    {
        get
        {
            return this._MasterJobId;
        }
        set
        {
            if ((this._MasterJobId != value))
            {
                if (this._Booking1.HasLoadedOrAssignedValue)
                {
                    throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                }
                this.OnMasterJobIdChanging(value);
                this.SendPropertyChanging();
                this._MasterJobId = value;
                this.SendPropertyChanged("MasterJobId");
                this.OnMasterJobIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisablePassengerSMS", DbType = "Bit")]
    public System.Nullable<bool> DisablePassengerSMS
    {
        get
        {
            return this._DisablePassengerSMS;
        }
        set
        {
            if ((this._DisablePassengerSMS != value))
            {
                this.OnDisablePassengerSMSChanging(value);
                this.SendPropertyChanging();
                this._DisablePassengerSMS = value;
                this.SendPropertyChanged("DisablePassengerSMS");
                this.OnDisablePassengerSMSChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisableDriverSMS", DbType = "Bit")]
    public System.Nullable<bool> DisableDriverSMS
    {
        get
        {
            return this._DisableDriverSMS;
        }
        set
        {
            if ((this._DisableDriverSMS != value))
            {
                this.OnDisableDriverSMSChanging(value);
                this.SendPropertyChanging();
                this._DisableDriverSMS = value;
                this.SendPropertyChanged("DisableDriverSMS");
                this.OnDisableDriverSMSChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsCommissionWise", DbType = "Bit")]
    public System.Nullable<bool> IsCommissionWise
    {
        get
        {
            return this._IsCommissionWise;
        }
        set
        {
            if ((this._IsCommissionWise != value))
            {
                this.OnIsCommissionWiseChanging(value);
                this.SendPropertyChanging();
                this._IsCommissionWise = value;
                this.SendPropertyChanged("IsCommissionWise");
                this.OnIsCommissionWiseChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DriverCommission", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> DriverCommission
    {
        get
        {
            return this._DriverCommission;
        }
        set
        {
            if ((this._DriverCommission != value))
            {
                this.OnDriverCommissionChanging(value);
                this.SendPropertyChanging();
                this._DriverCommission = value;
                this.SendPropertyChanged("DriverCommission");
                this.OnDriverCommissionChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DespatchDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> DespatchDateTime
    {
        get
        {
            return this._DespatchDateTime;
        }
        set
        {
            if ((this._DespatchDateTime != value))
            {
                this.OnDespatchDateTimeChanging(value);
                this.SendPropertyChanging();
                this._DespatchDateTime = value;
                this.SendPropertyChanged("DespatchDateTime");
                this.OnDespatchDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JobOfferDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> JobOfferDateTime
    {
        get
        {
            return this._JobOfferDateTime;
        }
        set
        {
            if ((this._JobOfferDateTime != value))
            {
                this.OnJobOfferDateTimeChanging(value);
                this.SendPropertyChanging();
                this._JobOfferDateTime = value;
                this.SendPropertyChanged("JobOfferDateTime");
                this.OnJobOfferDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BookingTypeId", DbType = "Int")]
    public System.Nullable<int> BookingTypeId
    {
        get
        {
            return this._BookingTypeId;
        }
        set
        {
            if ((this._BookingTypeId != value))
            {
                this.OnBookingTypeIdChanging(value);
                this.SendPropertyChanging();
                this._BookingTypeId = value;
                this.SendPropertyChanged("BookingTypeId");
                this.OnBookingTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DriverCommissionType", DbType = "VarChar(20)")]
    public string DriverCommissionType
    {
        get
        {
            return this._DriverCommissionType;
        }
        set
        {
            if ((this._DriverCommissionType != value))
            {
                this.OnDriverCommissionTypeChanging(value);
                this.SendPropertyChanging();
                this._DriverCommissionType = value;
                this.SendPropertyChanged("DriverCommissionType");
                this.OnDriverCommissionTypeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsBidding", DbType = "Bit")]
    public System.Nullable<bool> IsBidding
    {
        get
        {
            return this._IsBidding;
        }
        set
        {
            if ((this._IsBidding != value))
            {
                this.OnIsBiddingChanging(value);
                this.SendPropertyChanging();
                this._IsBidding = value;
                this.SendPropertyChanged("IsBidding");
                this.OnIsBiddingChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsQuotation", DbType = "Bit")]
    public System.Nullable<bool> IsQuotation
    {
        get
        {
            return this._IsQuotation;
        }
        set
        {
            if ((this._IsQuotation != value))
            {
                this.OnIsQuotationChanging(value);
                this.SendPropertyChanging();
                this._IsQuotation = value;
                this.SendPropertyChanged("IsQuotation");
                this.OnIsQuotationChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CostCenterId", DbType = "Int")]
    public System.Nullable<int> CostCenterId
    {
        get
        {
            return this._CostCenterId;
        }
        set
        {
            if ((this._CostCenterId != value))
            {
                this.OnCostCenterIdChanging(value);
                this.SendPropertyChanging();
                this._CostCenterId = value;
                this.SendPropertyChanged("CostCenterId");
                this.OnCostCenterIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashRate", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> CashRate
    {
        get
        {
            return this._CashRate;
        }
        set
        {
            if ((this._CashRate != value))
            {
                this.OnCashRateChanging(value);
                this.SendPropertyChanging();
                this._CashRate = value;
                this.SendPropertyChanged("CashRate");
                this.OnCashRateChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AccountRate", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> AccountRate
    {
        get
        {
            return this._AccountRate;
        }
        set
        {
            if ((this._AccountRate != value))
            {
                this.OnAccountRateChanging(value);
                this.SendPropertyChanging();
                this._AccountRate = value;
                this.SendPropertyChanged("AccountRate");
                this.OnAccountRateChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_WaitingMins", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> WaitingMins
    {
        get
        {
            return this._WaitingMins;
        }
        set
        {
            if ((this._WaitingMins != value))
            {
                this.OnWaitingMinsChanging(value);
                this.SendPropertyChanging();
                this._WaitingMins = value;
                this.SendPropertyChanged("WaitingMins");
                this.OnWaitingMinsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExtraMile", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> ExtraMile
    {
        get
        {
            return this._ExtraMile;
        }
        set
        {
            if ((this._ExtraMile != value))
            {
                this.OnExtraMileChanging(value);
                this.SendPropertyChanging();
                this._ExtraMile = value;
                this.SendPropertyChanged("ExtraMile");
                this.OnExtraMileChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AcceptedDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> AcceptedDateTime
    {
        get
        {
            return this._AcceptedDateTime;
        }
        set
        {
            if ((this._AcceptedDateTime != value))
            {
                this.OnAcceptedDateTimeChanging(value);
                this.SendPropertyChanging();
                this._AcceptedDateTime = value;
                this.SendPropertyChanged("AcceptedDateTime");
                this.OnAcceptedDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_POBDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> POBDateTime
    {
        get
        {
            return this._POBDateTime;
        }
        set
        {
            if ((this._POBDateTime != value))
            {
                this.OnPOBDateTimeChanging(value);
                this.SendPropertyChanging();
                this._POBDateTime = value;
                this.SendPropertyChanged("POBDateTime");
                this.OnPOBDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_STCDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> STCDateTime
    {
        get
        {
            return this._STCDateTime;
        }
        set
        {
            if ((this._STCDateTime != value))
            {
                this.OnSTCDateTimeChanging(value);
                this.SendPropertyChanging();
                this._STCDateTime = value;
                this.SendPropertyChanged("STCDateTime");
                this.OnSTCDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ClearedDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> ClearedDateTime
    {
        get
        {
            return this._ClearedDateTime;
        }
        set
        {
            if ((this._ClearedDateTime != value))
            {
                this.OnClearedDateTimeChanging(value);
                this.SendPropertyChanging();
                this._ClearedDateTime = value;
                this.SendPropertyChanged("ClearedDateTime");
                this.OnClearedDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CancelReason", DbType = "VarChar(200)")]
    public string CancelReason
    {
        get
        {
            return this._CancelReason;
        }
        set
        {
            if ((this._CancelReason != value))
            {
                this.OnCancelReasonChanging(value);
                this.SendPropertyChanging();
                this._CancelReason = value;
                this.SendPropertyChanged("CancelReason");
                this.OnCancelReasonChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalTravelledMiles", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> TotalTravelledMiles
    {
        get
        {
            return this._TotalTravelledMiles;
        }
        set
        {
            if ((this._TotalTravelledMiles != value))
            {
                this.OnTotalTravelledMilesChanging(value);
                this.SendPropertyChanging();
                this._TotalTravelledMiles = value;
                this.SendPropertyChanged("TotalTravelledMiles");
                this.OnTotalTravelledMilesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CompanyPrice", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> CompanyPrice
    {
        get
        {
            return this._CompanyPrice;
        }
        set
        {
            if ((this._CompanyPrice != value))
            {
                this.OnCompanyPriceChanging(value);
                this.SendPropertyChanging();
                this._CompanyPrice = value;
                this.SendPropertyChanged("CompanyPrice");
                this.OnCompanyPriceChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoicePaymentTypeId", DbType = "Int")]
    public System.Nullable<int> InvoicePaymentTypeId
    {
        get
        {
            return this._InvoicePaymentTypeId;
        }
        set
        {
            if ((this._InvoicePaymentTypeId != value))
            {
                this.OnInvoicePaymentTypeIdChanging(value);
                this.SendPropertyChanging();
                this._InvoicePaymentTypeId = value;
                this.SendPropertyChanged("InvoicePaymentTypeId");
                this.OnInvoicePaymentTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FleetMasterId", DbType = "Int")]
    public System.Nullable<int> FleetMasterId
    {
        get
        {
            return this._FleetMasterId;
        }
        set
        {
            if ((this._FleetMasterId != value))
            {
                this.OnFleetMasterIdChanging(value);
                this.SendPropertyChanging();
                this._FleetMasterId = value;
                this.SendPropertyChanged("FleetMasterId");
                this.OnFleetMasterIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Despatchby", DbType = "VarChar(100)")]
    public string Despatchby
    {
        get
        {
            return this._Despatchby;
        }
        set
        {
            if ((this._Despatchby != value))
            {
                this.OnDespatchbyChanging(value);
                this.SendPropertyChanging();
                this._Despatchby = value;
                this.SendPropertyChanged("Despatchby");
                this.OnDespatchbyChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ZoneId", DbType = "Int")]
    public System.Nullable<int> ZoneId
    {
        get
        {
            return this._ZoneId;
        }
        set
        {
            if ((this._ZoneId != value))
            {
                this.OnZoneIdChanging(value);
                this.SendPropertyChanging();
                this._ZoneId = value;
                this.SendPropertyChanged("ZoneId");
                this.OnZoneIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DropOffZoneId", DbType = "Int")]
    public System.Nullable<int> DropOffZoneId
    {
        get
        {
            return this._DropOffZoneId;
        }
        set
        {
            if ((this._DropOffZoneId != value))
            {
                this.OnDropOffZoneIdChanging(value);
                this.SendPropertyChanging();
                this._DropOffZoneId = value;
                this.SendPropertyChanged("DropOffZoneId");
                this.OnDropOffZoneIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReturnFromLocId", DbType = "Int")]
    public System.Nullable<int> ReturnFromLocId
    {
        get
        {
            return this._ReturnFromLocId;
        }
        set
        {
            if ((this._ReturnFromLocId != value))
            {
                this.OnReturnFromLocIdChanging(value);
                this.SendPropertyChanging();
                this._ReturnFromLocId = value;
                this.SendPropertyChanged("ReturnFromLocId");
                this.OnReturnFromLocIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SubcompanyId", DbType = "Int")]
    public System.Nullable<int> SubcompanyId
    {
        get
        {
            return this._SubcompanyId;
        }
        set
        {
            if ((this._SubcompanyId != value))
            {
                this.OnSubcompanyIdChanging(value);
                this.SendPropertyChanging();
                this._SubcompanyId = value;
                this.SendPropertyChanged("SubcompanyId");
                this.OnSubcompanyIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerPrice", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> CustomerPrice
    {
        get
        {
            return this._CustomerPrice;
        }
        set
        {
            if ((this._CustomerPrice != value))
            {
                this.OnCustomerPriceChanging(value);
                this.SendPropertyChanging();
                this._CustomerPrice = value;
                this.SendPropertyChanged("CustomerPrice");
                this.OnCustomerPriceChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AgentCommissionPercent", DbType = "Int")]
    public System.Nullable<int> AgentCommissionPercent
    {
        get
        {
            return this._AgentCommissionPercent;
        }
        set
        {
            if ((this._AgentCommissionPercent != value))
            {
                this.OnAgentCommissionPercentChanging(value);
                this.SendPropertyChanging();
                this._AgentCommissionPercent = value;
                this.SendPropertyChanged("AgentCommissionPercent");
                this.OnAgentCommissionPercentChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AgentCommission", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> AgentCommission
    {
        get
        {
            return this._AgentCommission;
        }
        set
        {
            if ((this._AgentCommission != value))
            {
                this.OnAgentCommissionChanging(value);
                this.SendPropertyChanging();
                this._AgentCommission = value;
                this.SendPropertyChanged("AgentCommission");
                this.OnAgentCommissionChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JobTakenByCompany", DbType = "Bit")]
    public System.Nullable<bool> JobTakenByCompany
    {
        get
        {
            return this._JobTakenByCompany;
        }
        set
        {
            if ((this._JobTakenByCompany != value))
            {
                this.OnJobTakenByCompanyChanging(value);
                this.SendPropertyChanging();
                this._JobTakenByCompany = value;
                this.SendPropertyChanged("JobTakenByCompany");
                this.OnJobTakenByCompanyChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FlightDepartureDate", DbType = "DateTime")]
    public System.Nullable<System.DateTime> FlightDepartureDate
    {
        get
        {
            return this._FlightDepartureDate;
        }
        set
        {
            if ((this._FlightDepartureDate != value))
            {
                this.OnFlightDepartureDateChanging(value);
                this.SendPropertyChanging();
                this._FlightDepartureDate = value;
                this.SendPropertyChanged("FlightDepartureDate");
                this.OnFlightDepartureDateChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoOfChilds", DbType = "Int")]
    public System.Nullable<int> NoOfChilds
    {
        get
        {
            return this._NoOfChilds;
        }
        set
        {
            if ((this._NoOfChilds != value))
            {
                this.OnNoOfChildsChanging(value);
                this.SendPropertyChanging();
                this._NoOfChilds = value;
                this.SendPropertyChanged("NoOfChilds");
                this.OnNoOfChildsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GroupJobId", DbType = "BigInt")]
    public System.Nullable<long> GroupJobId
    {
        get
        {
            return this._GroupJobId;
        }
        set
        {
            if ((this._GroupJobId != value))
            {
                this.OnGroupJobIdChanging(value);
                this.SendPropertyChanging();
                this._GroupJobId = value;
                this.SendPropertyChanged("GroupJobId");
                this.OnGroupJobIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RoomNo", DbType = "VarChar(200)")]
    public string RoomNo
    {
        get
        {
            return this._RoomNo;
        }
        set
        {
            if ((this._RoomNo != value))
            {
                this.OnRoomNoChanging(value);
                this.SendPropertyChanging();
                this._RoomNo = value;
                this.SendPropertyChanged("RoomNo");
                this.OnRoomNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FaresPostedFrom", DbType = "VarChar(50)")]
    public string FaresPostedFrom
    {
        get
        {
            return this._FaresPostedFrom;
        }
        set
        {
            if ((this._FaresPostedFrom != value))
            {
                this.OnFaresPostedFromChanging(value);
                this.SendPropertyChanging();
                this._FaresPostedFrom = value;
                this.SendPropertyChanged("FaresPostedFrom");
                this.OnFaresPostedFromChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AdvanceBookingId", DbType = "BigInt")]
    public System.Nullable<long> AdvanceBookingId
    {
        get
        {
            return this._AdvanceBookingId;
        }
        set
        {
            if ((this._AdvanceBookingId != value))
            {
                this.OnAdvanceBookingIdChanging(value);
                this.SendPropertyChanging();
                this._AdvanceBookingId = value;
                this.SendPropertyChanged("AdvanceBookingId");
                this.OnAdvanceBookingIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BoundType", DbType = "VarChar(MAX)")]
    public string BoundType
    {
        get
        {
            return this._BoundType;
        }
        set
        {
            if ((this._BoundType != value))
            {
                this.OnBoundTypeChanging(value);
                this.SendPropertyChanging();
                this._BoundType = value;
                this.SendPropertyChanged("BoundType");
                this.OnBoundTypeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BookedBy", DbType = "VarChar(100)")]
    public string BookedBy
    {
        get
        {
            return this._BookedBy;
        }
        set
        {
            if ((this._BookedBy != value))
            {
                this.OnBookedByChanging(value);
                this.SendPropertyChanging();
                this._BookedBy = value;
                this.SendPropertyChanged("BookedBy");
                this.OnBookedByChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BabySeats", DbType = "VarChar(200)")]
    public string BabySeats
    {
        get
        {
            return this._BabySeats;
        }
        set
        {
            if ((this._BabySeats != value))
            {
                this.OnBabySeatsChanging(value);
                this.SendPropertyChanging();
                this._BabySeats = value;
                this.SendPropertyChanged("BabySeats");
                this.OnBabySeatsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromOther", DbType = "VarChar(MAX)")]
    public string FromOther
    {
        get
        {
            return this._FromOther;
        }
        set
        {
            if ((this._FromOther != value))
            {
                this.OnFromOtherChanging(value);
                this.SendPropertyChanging();
                this._FromOther = value;
                this.SendPropertyChanged("FromOther");
                this.OnFromOtherChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToOther", DbType = "VarChar(MAX)")]
    public string ToOther
    {
        get
        {
            return this._ToOther;
        }
        set
        {
            if ((this._ToOther != value))
            {
                this.OnToOtherChanging(value);
                this.SendPropertyChanging();
                this._ToOther = value;
                this.SendPropertyChanged("ToOther");
                this.OnToOtherChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EscortName", DbType = "VarChar(MAX)")]
    public string EscortName
    {
        get
        {
            return this._EscortName;
        }
        set
        {
            if ((this._EscortName != value))
            {
                this.OnEscortNameChanging(value);
                this.SendPropertyChanging();
                this._EscortName = value;
                this.SendPropertyChanged("EscortName");
                this.OnEscortNameChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsProcessed", DbType = "Bit")]
    public System.Nullable<bool> IsProcessed
    {
        get
        {
            return this._IsProcessed;
        }
        set
        {
            if ((this._IsProcessed != value))
            {
                this.OnIsProcessedChanging(value);
                this.SendPropertyChanging();
                this._IsProcessed = value;
                this.SendPropertyChanged("IsProcessed");
                this.OnIsProcessedChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EscortId", DbType = "BigInt")]
    public System.Nullable<long> EscortId
    {
        get
        {
            return this._EscortId;
        }
        set
        {
            if ((this._EscortId != value))
            {
                this.OnEscortIdChanging(value);
                this.SendPropertyChanging();
                this._EscortId = value;
                this.SendPropertyChanged("EscortId");
                this.OnEscortIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EscortPrice", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> EscortPrice
    {
        get
        {
            return this._EscortPrice;
        }
        set
        {
            if ((this._EscortPrice != value))
            {
                this.OnEscortPriceChanging(value);
                this.SendPropertyChanging();
                this._EscortPrice = value;
                this.SendPropertyChanged("EscortPrice");
                this.OnEscortPriceChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PaymentComments", DbType = "VarChar(MAX)")]
    public string PaymentComments
    {
        get
        {
            return this._PaymentComments;
        }
        set
        {
            if ((this._PaymentComments != value))
            {
                this.OnPaymentCommentsChanging(value);
                this.SendPropertyChanging();
                this._PaymentComments = value;
                this.SendPropertyChanged("PaymentComments");
                this.OnPaymentCommentsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisableDriverCommissionTick", DbType = "Bit")]
    public System.Nullable<bool> DisableDriverCommissionTick
    {
        get
        {
            return this._DisableDriverCommissionTick;
        }
        set
        {
            if ((this._DisableDriverCommissionTick != value))
            {
                this.OnDisableDriverCommissionTickChanging(value);
                this.SendPropertyChanging();
                this._DisableDriverCommissionTick = value;
                this.SendPropertyChanged("DisableDriverCommissionTick");
                this.OnDisableDriverCommissionTickChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SecondaryPaymentTypeId", DbType = "Int")]
    public System.Nullable<int> SecondaryPaymentTypeId
    {
        get
        {
            return this._SecondaryPaymentTypeId;
        }
        set
        {
            if ((this._SecondaryPaymentTypeId != value))
            {
                this.OnSecondaryPaymentTypeIdChanging(value);
                this.SendPropertyChanging();
                this._SecondaryPaymentTypeId = value;
                this.SendPropertyChanged("SecondaryPaymentTypeId");
                this.OnSecondaryPaymentTypeIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashFares", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> CashFares
    {
        get
        {
            return this._CashFares;
        }
        set
        {
            if ((this._CashFares != value))
            {
                this.OnCashFaresChanging(value);
                this.SendPropertyChanging();
                this._CashFares = value;
                this.SendPropertyChanged("CashFares");
                this.OnCashFaresChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsConfirmedDriver", DbType = "Bit")]
    public System.Nullable<bool> IsConfirmedDriver
    {
        get
        {
            return this._IsConfirmedDriver;
        }
        set
        {
            if ((this._IsConfirmedDriver != value))
            {
                this.OnIsConfirmedDriverChanging(value);
                this.SendPropertyChanging();
                this._IsConfirmedDriver = value;
                this.SendPropertyChanged("IsConfirmedDriver");
                this.OnIsConfirmedDriverChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DeadMileage", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> DeadMileage
    {
        get
        {
            return this._DeadMileage;
        }
        set
        {
            if ((this._DeadMileage != value))
            {
                this.OnDeadMileageChanging(value);
                this.SendPropertyChanging();
                this._DeadMileage = value;
                this.SendPropertyChanged("DeadMileage");
                this.OnDeadMileageChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OnHoldWaitingMins", DbType = "Int")]
    public System.Nullable<int> OnHoldWaitingMins
    {
        get
        {
            return this._OnHoldWaitingMins;
        }
        set
        {
            if ((this._OnHoldWaitingMins != value))
            {
                this.OnOnHoldWaitingMinsChanging(value);
                this.SendPropertyChanging();
                this._OnHoldWaitingMins = value;
                this.SendPropertyChanged("OnHoldWaitingMins");
                this.OnOnHoldWaitingMinsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OnHoldDateTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> OnHoldDateTime
    {
        get
        {
            return this._OnHoldDateTime;
        }
        set
        {
            if ((this._OnHoldDateTime != value))
            {
                this.OnOnHoldDateTimeChanging(value);
                this.SendPropertyChanging();
                this._OnHoldDateTime = value;
                this.SendPropertyChanged("OnHoldDateTime");
                this.OnOnHoldDateTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OnHoldReason", DbType = "VarChar(MAX)")]
    public string OnHoldReason
    {
        get
        {
            return this._OnHoldReason;
        }
        set
        {
            if ((this._OnHoldReason != value))
            {
                this.OnOnHoldReasonChanging(value);
                this.SendPropertyChanging();
                this._OnHoldReason = value;
                this.SendPropertyChanged("OnHoldReason");
                this.OnOnHoldReasonChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerCreditCardDetails", DbType = "VarChar(MAX)")]
    public string CustomerCreditCardDetails
    {
        get
        {
            return this._CustomerCreditCardDetails;
        }
        set
        {
            if ((this._CustomerCreditCardDetails != value))
            {
                this.OnCustomerCreditCardDetailsChanging(value);
                this.SendPropertyChanging();
                this._CustomerCreditCardDetails = value;
                this.SendPropertyChanged("CustomerCreditCardDetails");
                this.OnCustomerCreditCardDetailsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CompanyCreditCardDetails", DbType = "VarChar(MAX)")]
    public string CompanyCreditCardDetails
    {
        get
        {
            return this._CompanyCreditCardDetails;
        }
        set
        {
            if ((this._CompanyCreditCardDetails != value))
            {
                this.OnCompanyCreditCardDetailsChanging(value);
                this.SendPropertyChanging();
                this._CompanyCreditCardDetails = value;
                this.SendPropertyChanged("CompanyCreditCardDetails");
                this.OnCompanyCreditCardDetailsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EnableFareMeter", DbType = "Bit")]
    public System.Nullable<bool> EnableFareMeter
    {
        get
        {
            return this._EnableFareMeter;
        }
        set
        {
            if ((this._EnableFareMeter != value))
            {
                this.OnEnableFareMeterChanging(value);
                this.SendPropertyChanging();
                this._EnableFareMeter = value;
                this.SendPropertyChanged("EnableFareMeter");
                this.OnEnableFareMeterChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PriceBiddingExpiryDate", DbType = "DateTime")]
    public System.Nullable<System.DateTime> PriceBiddingExpiryDate
    {
        get
        {
            return this._PriceBiddingExpiryDate;
        }
        set
        {
            if ((this._PriceBiddingExpiryDate != value))
            {
                this.OnPriceBiddingExpiryDateChanging(value);
                this.SendPropertyChanging();
                this._PriceBiddingExpiryDate = value;
                this.SendPropertyChanged("PriceBiddingExpiryDate");
                this.OnPriceBiddingExpiryDateChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DriverWaitingMins", DbType = "Int")]
    public System.Nullable<int> DriverWaitingMins
    {
        get
        {
            return this._DriverWaitingMins;
        }
        set
        {
            if ((this._DriverWaitingMins != value))
            {
                this.OnDriverWaitingMinsChanging(value);
                this.SendPropertyChanging();
                this._DriverWaitingMins = value;
                this.SendPropertyChanged("DriverWaitingMins");
                this.OnDriverWaitingMinsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JobCancelledBy", DbType = "VarChar(100)")]
    public string JobCancelledBy
    {
        get
        {
            return this._JobCancelledBy;
        }
        set
        {
            if ((this._JobCancelledBy != value))
            {
                this.OnJobCancelledByChanging(value);
                this.SendPropertyChanging();
                this._JobCancelledBy = value;
                this.SendPropertyChanged("JobCancelledBy");
                this.OnJobCancelledByChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JobCancelledOn", DbType = "DateTime")]
    public System.Nullable<System.DateTime> JobCancelledOn
    {
        get
        {
            return this._JobCancelledOn;
        }
        set
        {
            if ((this._JobCancelledOn != value))
            {
                this.OnJobCancelledOnChanging(value);
                this.SendPropertyChanging();
                this._JobCancelledOn = value;
                this.SendPropertyChanged("JobCancelledOn");
                this.OnJobCancelledOnChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CallRefNo", DbType = "VarChar(50)")]
    public string CallRefNo
    {
        get
        {
            return this._CallRefNo;
        }
        set
        {
            if ((this._CallRefNo != value))
            {
                this.OnCallRefNoChanging(value);
                this.SendPropertyChanging();
                this._CallRefNo = value;
                this.SendPropertyChanged("CallRefNo");
                this.OnCallRefNoChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsReverse", DbType = "Bit")]
    public System.Nullable<bool> IsReverse
    {
        get
        {
            return this._IsReverse;
        }
        set
        {
            if ((this._IsReverse != value))
            {
                this.OnIsReverseChanging(value);
                this.SendPropertyChanging();
                this._IsReverse = value;
                this.SendPropertyChanged("IsReverse");
                this.OnIsReverseChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ViaString", DbType = "VarChar(MAX)")]
    public string ViaString
    {
        get
        {
            return this._ViaString;
        }
        set
        {
            if ((this._ViaString != value))
            {
                this.OnViaStringChanging(value);
                this.SendPropertyChanging();
                this._ViaString = value;
                this.SendPropertyChanged("ViaString");
                this.OnViaStringChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NotesString", DbType = "VarChar(MAX)")]
    public string NotesString
    {
        get
        {
            return this._NotesString;
        }
        set
        {
            if ((this._NotesString != value))
            {
                this.OnNotesStringChanging(value);
                this.SendPropertyChanging();
                this._NotesString = value;
                this.SendPropertyChanged("NotesString");
                this.OnNotesStringChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TransferJobId", DbType = "BigInt")]
    public System.Nullable<long> TransferJobId
    {
        get
        {
            return this._TransferJobId;
        }
        set
        {
            if ((this._TransferJobId != value))
            {
                this.OnTransferJobIdChanging(value);
                this.SendPropertyChanging();
                this._TransferJobId = value;
                this.SendPropertyChanged("TransferJobId");
                this.OnTransferJobIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TransferJobCommission", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> TransferJobCommission
    {
        get
        {
            return this._TransferJobCommission;
        }
        set
        {
            if ((this._TransferJobCommission != value))
            {
                this.OnTransferJobCommissionChanging(value);
                this.SendPropertyChanging();
                this._TransferJobCommission = value;
                this.SendPropertyChanged("TransferJobCommission");
                this.OnTransferJobCommissionChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PartyId", DbType = "Int")]
    public System.Nullable<int> PartyId
    {
        get
        {
            return this._PartyId;
        }
        set
        {
            if ((this._PartyId != value))
            {
                this.OnPartyIdChanging(value);
                this.SendPropertyChanging();
                this._PartyId = value;
                this.SendPropertyChanged("PartyId");
                this.OnPartyIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OnlineBookingId", DbType = "BigInt")]
    public System.Nullable<long> OnlineBookingId
    {
        get
        {
            return this._OnlineBookingId;
        }
        set
        {
            if ((this._OnlineBookingId != value))
            {
                this.OnOnlineBookingIdChanging(value);
                this.SendPropertyChanging();
                this._OnlineBookingId = value;
                this.SendPropertyChanged("OnlineBookingId");
                this.OnOnlineBookingIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceCharges", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> ServiceCharges
    {
        get
        {
            return this._ServiceCharges;
        }
        set
        {
            if ((this._ServiceCharges != value))
            {
                this.OnServiceChargesChanging(value);
                this.SendPropertyChanging();
                this._ServiceCharges = value;
                this.SendPropertyChanged("ServiceCharges");
                this.OnServiceChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ApplyServiceCharges", DbType = "Bit")]
    public System.Nullable<bool> ApplyServiceCharges
    {
        get
        {
            return this._ApplyServiceCharges;
        }
        set
        {
            if ((this._ApplyServiceCharges != value))
            {
                this.OnApplyServiceChargesChanging(value);
                this.SendPropertyChanging();
                this._ApplyServiceCharges = value;
                this.SendPropertyChanged("ApplyServiceCharges");
                this.OnApplyServiceChargesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JobCode", DbType = "VarChar(10)")]
    public string JobCode
    {
        get
        {
            return this._JobCode;
        }
        set
        {
            if ((this._JobCode != value))
            {
                this.OnJobCodeChanging(value);
                this.SendPropertyChanging();
                this._JobCode = value;
                this.SendPropertyChanged("JobCode");
                this.OnJobCodeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExtraPickup", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> ExtraPickup
    {
        get
        {
            return this._ExtraPickup;
        }
        set
        {
            if ((this._ExtraPickup != value))
            {
                this.OnExtraPickupChanging(value);
                this.SendPropertyChanging();
                this._ExtraPickup = value;
                this.SendPropertyChanged("ExtraPickup");
                this.OnExtraPickupChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExtraDropOff", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> ExtraDropOff
    {
        get
        {
            return this._ExtraDropOff;
        }
        set
        {
            if ((this._ExtraDropOff != value))
            {
                this.OnExtraDropOffChanging(value);
                this.SendPropertyChanging();
                this._ExtraDropOff = value;
                this.SendPropertyChanged("ExtraDropOff");
                this.OnExtraDropOffChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TipAmount", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> TipAmount
    {
        get
        {
            return this._TipAmount;
        }
        set
        {
            if ((this._TipAmount != value))
            {
                this.OnTipAmountChanging(value);
                this.SendPropertyChanging();
                this._TipAmount = value;
                this.SendPropertyChanged("TipAmount");
                this.OnTipAmountChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JourneyTimeInMins", DbType = "Decimal(18,0)")]
    public System.Nullable<decimal> JourneyTimeInMins
    {
        get
        {
            return this._JourneyTimeInMins;
        }
        set
        {
            if ((this._JourneyTimeInMins != value))
            {
                this.OnJourneyTimeInMinsChanging(value);
                this.SendPropertyChanging();
                this._JourneyTimeInMins = value;
                this.SendPropertyChanged("JourneyTimeInMins");
                this.OnJourneyTimeInMinsChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsQuotedPrice", DbType = "Bit")]
    public System.Nullable<bool> IsQuotedPrice
    {
        get
        {
            return this._IsQuotedPrice;
        }
        set
        {
            if ((this._IsQuotedPrice != value))
            {
                this.OnIsQuotedPriceChanging(value);
                this.SendPropertyChanging();
                this._IsQuotedPrice = value;
                this.SendPropertyChanged("IsQuotedPrice");
                this.OnIsQuotedPriceChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReAutoDespatchTime", DbType = "DateTime")]
    public System.Nullable<System.DateTime> ReAutoDespatchTime
    {
        get
        {
            return this._ReAutoDespatchTime;
        }
        set
        {
            if ((this._ReAutoDespatchTime != value))
            {
                this.OnReAutoDespatchTimeChanging(value);
                this.SendPropertyChanging();
                this._ReAutoDespatchTime = value;
                this.SendPropertyChanged("ReAutoDespatchTime");
                this.OnReAutoDespatchTimeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AttributeValues", DbType = "VarChar(50)")]
    public string AttributeValues
    {
        get
        {
            return this._AttributeValues;
        }
        set
        {
            if ((this._AttributeValues != value))
            {
                this.OnAttributeValuesChanging(value);
                this.SendPropertyChanging();
                this._AttributeValues = value;
                this.SendPropertyChanged("AttributeValues");
                this.OnAttributeValuesChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SMSType", DbType = "Int")]
    public System.Nullable<int> SMSType
    {
        get
        {
            return this._SMSType;
        }
        set
        {
            if ((this._SMSType != value))
            {
                this.OnSMSTypeChanging(value);
                this.SendPropertyChanging();
                this._SMSType = value;
                this.SendPropertyChanged("SMSType");
                this.OnSMSTypeChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PaidAmount", DbType = "Decimal(18,2)")]
    public System.Nullable<decimal> PaidAmount
    {
        get
        {
            return this._PaidAmount;
        }
        set
        {
            if ((this._PaidAmount != value))
            {
                this.OnPaidAmountChanging(value);
                this.SendPropertyChanging();
                this._PaidAmount = value;
                this.SendPropertyChanged("PaidAmount");
                this.OnPaidAmountChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DefaultClientId", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
    public string DefaultClientId
    {
        get
        {
            return this._DefaultClientId;
        }
        set
        {
            if ((this._DefaultClientId != value))
            {
                this.OnDefaultClientIdChanging(value);
                this.SendPropertyChanging();
                this._DefaultClientId = value;
                this.SendPropertyChanged("DefaultClientId");
                this.OnDefaultClientIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ClientID", DbType = "Int NOT NULL")]
    public int ClientID
    {
        get
        {
            return this._ClientID;
        }
        set
        {
            if ((this._ClientID != value))
            {
                this.OnClientIDChanging(value);
                this.SendPropertyChanging();
                this._ClientID = value;
                this.SendPropertyChanged("ClientID");
                this.OnClientIDChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ClientName", DbType = "NVarChar(200) NOT NULL", CanBeNull = false)]
    public string ClientName
    {
        get
        {
            return this._ClientName;
        }
        set
        {
            if ((this._ClientName != value))
            {
                this.OnClientNameChanging(value);
                this.SendPropertyChanging();
                this._ClientName = value;
                this.SendPropertyChanged("ClientName");
                this.OnClientNameChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ClientBookingId", DbType = "BigInt NOT NULL")]
    public long ClientBookingId
    {
        get
        {
            return this._ClientBookingId;
        }
        set
        {
            if ((this._ClientBookingId != value))
            {
                this.OnClientBookingIdChanging(value);
                this.SendPropertyChanging();
                this._ClientBookingId = value;
                this.SendPropertyChanged("ClientBookingId");
                this.OnClientBookingIdChanged();
            }
        }
    }

    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ClientMasterJobId", DbType = "BigInt")]
    public System.Nullable<long> ClientMasterJobId
    {
        get
        {
            return this._ClientMasterJobId;
        }
        set
        {
            if ((this._ClientMasterJobId != value))
            {
                this.OnClientMasterJobIdChanging(value);
                this.SendPropertyChanging();
                this._ClientMasterJobId = value;
                this.SendPropertyChanged("ClientMasterJobId");
                this.OnClientMasterJobIdChanged();
            }
        }
    }

   

    [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Booking_Booking", Storage = "_Bookings", ThisKey = "Id", OtherKey = "MasterJobId")]
    public EntitySet<PoolBooking> Bookings
    {
        get
        {
            return this._Bookings;
        }
        set
        {
            this._Bookings.Assign(value);
        }
    }

    [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Booking_Booking", Storage = "_Booking1", ThisKey = "MasterJobId", OtherKey = "Id", IsForeignKey = true)]
    public PoolBooking Booking1
    {
        get
        {
            return this._Booking1.Entity;
        }
        set
        {
            PoolBooking previousValue = this._Booking1.Entity;
            if (((previousValue != value)
                        || (this._Booking1.HasLoadedOrAssignedValue == false)))
            {
                this.SendPropertyChanging();
                if ((previousValue != null))
                {
                    this._Booking1.Entity = null;
                    previousValue.Bookings.Remove(this);
                }
                this._Booking1.Entity = value;
                if ((value != null))
                {
                    value.Bookings.Add(this);
                    this._MasterJobId = value.Id;
                }
                else
                {
                    this._MasterJobId = default(Nullable<long>);
                }
                this.SendPropertyChanged("Booking1");
            }
        }
    }

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
        if ((this.PropertyChanging != null))
        {
            this.PropertyChanging(this, emptyChangingEventArgs);
        }
    }

    protected virtual void SendPropertyChanged(String propertyName)
    {
        if ((this.PropertyChanged != null))
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   

    private void attach_Bookings(PoolBooking entity)
    {
        this.SendPropertyChanging();
        entity.Booking1 = this;
    }

    private void detach_Bookings(PoolBooking entity)
    {
        this.SendPropertyChanging();
        entity.Booking1 = null;
    }
}