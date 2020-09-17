using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi_AppMain.Classes
{
    public class TempIncomeSummary
    {
        public int Id { get; set; }
        public long InvId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Description { get; set; }

        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public string Header { get; set; }
        public string Address { get; set; }

        public string Period { get; set; }
    }
}
