using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain
{
   public  class ClsCallerIdExtensions
    {

        private string _UserMachineName;

        public string UserMachineName
        {
            get { return _UserMachineName; }
            set { _UserMachineName = value; }
        }
        private string _CLIExtension;

        public string CLIExtension
        {
            get { return _CLIExtension; }
            set { _CLIExtension = value; }
        }

        private string _ForwardNumber;
        public string ForwardNumber
        {
            get { return _ForwardNumber; }
            set { _ForwardNumber = value; }
        }


        private int _UserId;

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }


    }
}
