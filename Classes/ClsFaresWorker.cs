using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taxi_Model;

namespace Taxi_AppMain
{
    public class clsfaresworker
    {
        public int? vehicleTypeId;
        public bool IsMoreFareWise;
        public int defaultVehicleId;
        public int fromZoneId;
        public int toZoneId;
        public string tempFromPostCode = "";
        public string tempToPostCode = "";
        public string fromAddress = "";
        public string toAddress = "";
        public decimal distance;
        public string distancestring;
        public decimal fareVal;
        public decimal returnfares;
        public decimal companyPrice;
        public decimal agentPrice;
        public decimal agentPercent;
        public bool IsAmountWiseAgentFees;
        public bool IsAirportAgentFares;
        public bool IsAgent;
        public bool hasVia;
        public int? fromLocTypeId;
        public int? toLocTypeId;
        public int? fromLocationId;
        public int? toLocationId;
        public string[] viaList;
        public int tempToLocId;
        public int tempFromLocId;
        public string fromLocName;
        public string toLocName;
        public string fromPostCode;
        public string toPostCode;
        public int? CompanyId;
        public decimal airportPickupChrgs = 0;
        public DateTime? pickupDateTime;
        public DateTime? returnpickupdateTime;
        public int SubCompanyId;
        public bool IsReverse;
        public decimal dd;
        public int PaymentTypeId;
        public int JourneyTypeId;
      
      

        public Gen_ServiceCharge objServiceCharge;
        public List<ClsViaLocations> ViaLocations = null;
    }


    public class ClsViaLocations
    {
        public int OrderNo;
        public int? LocId;
        public int LocTypeId;
        public string ViaLocValue;


    }
}
