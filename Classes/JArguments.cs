using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi_Model;

namespace Taxi_AppMain.Classes
{
    class JArguments
    {
        public int reportview { get; set; }
        public string ConnectionString { get; set; }
        public int? reporttype { get; set; }
        public int? companyId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public int? DriverId { get; set; }
        public int? PaymentTypeId { get; set; }
        public string PaymentType { get; set; }
        public int? GetBookingStatus { get; set; }
        public string BookedBy { get; set; }
        public int? CompanyGroupId { get; set; }
        public long? FleetMasterId { get; set; }
        public string orderNo { get; set; }
        public int? subCompanyId { get; set; }
        public int? transferredSubCompanyId { get; set; }
        public double? grdPaymentTypes { get; set; }
       
        public string bookingid { get; set; }

        public string rptheading { get; set; }
        public string rptCriteria { get; set; }
        public string rptaddress { get; set; }
        public string rptTel { get; set; }

        public string reportname { get; set; }
        public bool _Checkbox { get; set; }
        public long id { get; set; }
       
        public string AuthCode { get; set; }
      public bool optSortAsc { get; set; }
    }
    class JArg_AuditTrail
    {
        public string bookingid { get; set; }
        public string ConnectionString { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string reportname { get; set; }
        public bool _Checkbox { get; set; }
    }
    public class JArg_Receipts
    {

        public long id { get; set; }
        public string ConnectionString { get; set; }
        public bool _Checkbox { get; set; }
        public string reportname { get; set; }
        public string AuthCode { get; set; }
        public string ReportName { get; set; }
    }
    public class JArg_DriverLoginHistory
    {
        public string reportname { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public int? DriverId { get; set; }
        public string ConnectionString { get; set; }
        public string rptheading { get; set; }
        public string rptCriteria { get; set; }
        public string rptaddress { get; set; }
        public string rptTel { get; set; }
    }
    public class JArg_CallHistory
    {
        public string reportname { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
      

        public string phone { get; set; }
        public string Line { get; set; }
        public string name { get; set; }
        public string Stn { get; set; }

        public string ConnectionString { get; set; }
        public string rptheading { get; set; }
        public string rptCriteria { get; set; }
        public string rptaddress { get; set; }
        public string rptTel { get; set; }
        public bool IsMissed { get; set; }
    }
}
