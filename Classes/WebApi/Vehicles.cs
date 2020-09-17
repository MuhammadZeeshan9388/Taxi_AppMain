using System;
using System.Collections.Generic;



namespace Taxi_AppMain
{
    public class Vehicles
    {
      

        public string color { get; set; }
        public Direction direction { get; set; }
        public string driver_first_name { get; set; }
        public string driver_id { get; set; }
        public string driver_last_name { get; set; }
        public string driver_phone { get; set; }
        public object eta_minutes { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string vehicle_id { get; set; }
        public string vehicle_plate { get; set; }
        public string vehicle_type { get; set; }





    }

    public class Direction
    {
        public int kph { get; set; }
        public int heading { get; set; }
    }


    public class RootObject
    {
        public List<Vehicles> vehicles { get; set; }
    }

}

