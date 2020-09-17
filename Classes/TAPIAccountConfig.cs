using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain
{
    public  class TAPIAccountConfig
    {
        private string[] _LinesArray;

        public string[] LinesArray
        {
            get { return _LinesArray; }
            set { _LinesArray = value; }
        }


        private string _Line;

        public string Line
        {
            get { return _Line; }
            set { _Line = value; }
        }
        private string _BookingUrl;

        public string BookingUrl
        {
            get { return _BookingUrl; }
            set { _BookingUrl = value; }
        }
    }
}
