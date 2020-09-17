using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi_AppMain.Classes
{
    public class TempCallHistory
    {
        public int Id { get; set; }
        public int ControllerId { get; set; }
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string Line { get; set; }
        public string STN { get; set; }
        public string CompanyName { get; set; }

        public DateTime? CallDateTime { get; set; }
        public DateTime? AnsweredDateTime { get; set; }

        public bool IsMissed { get; set; }
        public int? Sno { get; set; }
        public string Duration { get; set; }
    }
}
