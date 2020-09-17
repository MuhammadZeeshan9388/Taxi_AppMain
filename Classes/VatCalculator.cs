using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain.Classes
{
    class VatCalculator
    {
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? Net { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Total { get; set; }
    }
}
