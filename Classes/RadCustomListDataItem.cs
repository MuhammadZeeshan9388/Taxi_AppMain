using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace Taxi_AppMain
{
    public class RadCustomListDataItem : RadListDataItem
    {
        private object _Tag;

        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

    }
}
