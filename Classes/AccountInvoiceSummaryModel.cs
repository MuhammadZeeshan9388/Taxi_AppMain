using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain.Classes
{
    class AccountInvoiceSummaryModel
    {
        public long Id { get; set; }
        public string InviceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public int TotalJobs { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? GrossTotal { get; set; }
        public decimal? Margin { get; set; }
        public decimal? TotalDriverCharges { get; set; }
        public decimal? TotalInvoice { get; set; }
    }
}
