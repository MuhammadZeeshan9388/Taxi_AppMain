using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Telerik.WinControls.UI;
using Taxi_Model;
using Taxi_BLL;
using  DAL;
using System.Linq;
using System.IO.Ports;


namespace Taxi_AppMain
{
   public class AppVars
    {


        public static bool? waitandreturnAsVia;

       public static string etaKey = string.Empty;
       public static string googleKey = string.Empty;
      // public static List<Gen_Zone_PolyVertice> listofVertices = null;
       public static bool IsTelephonist = false;
       public static string denyAllocatedBookingIds = string.Empty;
       public static String AppTheme = "ControlDefault";

       public static Gen_SubCompany objSubCompany = null;
       public static int DefaultSubCompanyId;

       public static int DefaultDriverSubCompanyId;
       public static int DefaultBookingSubCompanyId;

       public static string LicenseExpiryDate = string.Empty;
       public static string openedPhoneNo = string.Empty;
       public static bool LicenseChecked;
      

       public static bool IsLogout = false;

       public static string[] zonesList = null;
       public static List<OnlineWebsite> ListOfWebsites = null;

     //  public static List<POITable> listofPOI = new List<POITable>();
       public static List<SMSTag> listofSMSTags = null;
       public static List<UI.UserRights> listUserRights = new List<UI.UserRights>();
       public static List<string> keyLocations = null;

       public static List<stp_GetFullAddressesResult> listOfAddress = null;
       



       public static DAL.BLInfo<UM_User, Taxi_Model.TaxiDataContext> BLData =
                        new DAL.BLInfo<UM_User, Taxi_Model.TaxiDataContext>();


      
       public static DAL.LoginUser LoginObj = null;

       public static Gen_SysPolicy_Configuration objPolicyConfiguration;
       public static Gen_SysPolicy_SMSConfiguration objSMSConfiguration;


       public static frmMainMenu frmMDI;
       public static bool ShowAllBookings=false;

       public static DateTime? JobDueAlertLastStopped = null;


       public static Booking objCopyBooking = null;



       public static string AccessFrom = "desktop";
       public static bool enableSMSService = false;


       public static bool CanTransferJob;

       public static string appVersion;

    }
}
