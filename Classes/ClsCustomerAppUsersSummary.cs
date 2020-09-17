using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain.Classes
{
   public  class ClsCustomerAppUsersSummary
    {

        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _MobileNo;

        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        private string _TelephoneNo;

        public string TelephoneNo
        {
            get { return _TelephoneNo; }
            set { _TelephoneNo = value; }
        }
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Address1;

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }


        private int _TotalJobs;

        public int TotalJobs
        {
            get { return _TotalJobs; }
            set { _TotalJobs = value; }
        }


       public ClsCustomerAppUsersSummary()
       {


       }





    }
}
