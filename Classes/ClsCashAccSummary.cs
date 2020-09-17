using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain
{
   public  class ClsCashAccSummary
    {

        private DateTime? _CollectionDate;

        public DateTime? CollectionDate
        {
            get { return _CollectionDate; }
            set { _CollectionDate = value; }
        }
        private DateTime? _CollectionTime;

        public DateTime? CollectionTime
        {
            get { return _CollectionTime; }
            set { _CollectionTime = value; }
        }
        private string _Passenger;

        public string Passenger
        {
            get { return _Passenger; }
            set { _Passenger = value; }
        }
        private string _Pickup;

        public string Pickup
        {
            get { return _Pickup; }
            set { _Pickup = value; }
        }
        private string _Destination;

        public string Destination
        {
            get { return _Destination; }
            set { _Destination = value; }
        }
        private decimal? _Price;

        public decimal? Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        private decimal? _CommissionRate;

        public decimal? CommissionRate
        {
            get { return _CommissionRate; }
            set { _CommissionRate = value; }
        }
        private decimal? _Amount;

        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }




       public ClsCashAccSummary()
       {


       }





    }
}
