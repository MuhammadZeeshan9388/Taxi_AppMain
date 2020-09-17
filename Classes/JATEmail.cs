using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using UI;
using DAL;
using Taxi_BLL;
using Taxi_Model;
using System.Reflection;
using System.Net;

namespace Taxi_AppMain
{
    public class JATEmail
    {


        public static string From;


        public static string To;


        public static string Subject;


        public static string Body;




        /// <summary>
        /// Passenger Pending Payment
        /// </summary>
        /// <param name="obj"></param>
        public static void EmailPPP(Booking obj)
        {
            try
            {

                Subject = "EMAIL - PPP";
                To = obj.CustomerEmail.ToStr().Trim();

                StringBuilder sb = new StringBuilder();
                if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY)
                {
                    string BabySeat = obj.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }

                    sb.Append("<html><Body>");
                    sb.Append("<div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\">"
                    + "<p align=\"center\"style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\">" +
                    "<span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\">" +
                    "<img width=\"234\" height=\"96\" border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p>" +
                    "<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\"><tbody><tr>" +
                    "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\">" +
                    "<span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Payment Pending)<u></u><u></u></span></h4>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Staff Message<u></u><u></u></span></h6>" +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Your booking has been confirmed .Please print this page off or note down your ref. number(s) for further communication. <br>" +
                    "<strong>We will send your Pick Up Instructions and Driver Details in a separate email.</strong> <br>Please <a target=\"_blank\" href=\"" + AppVars.objSubCompany.WebsiteUrl + "\">click here</a> to amend or to create a new journey. <u></u><u></u></p></div>" +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Outbound Journey<u></u><u></u></span></h5>" +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td>" +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + " <u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.PickupDateTime + "(<span data-term=\"goog_1153804745\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", obj.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804746\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm:tt}", obj.PickupDateTime.ToDateTimeorNull()) + "</span></span><u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.FareRate.ToStr() + "<u></u><u></u></p></td></tr>" +
                    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td>" +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofPassengers.ToStr() + "<u></u><u></u></p></td></tr></tbody></table></div>");// +

                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromStreet + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    else
                    {
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:inset #bfbfbf 1.0pt;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle<u></u><u></u></span></h5>" +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td>" +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.Fleet_VehicleType.VehicleType.ToStr() + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofLuggages.ToStr() + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Special Requirement<u></u><u></u></span></h6><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + obj.SpecialRequirements + "<u></u><u></u></p></div></td>" +
                    "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Booking Details<u></u><u></u></span></h6><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Booking ID:</strong><u></u><u></u></p></td>" +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + " (Outbound) <u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Date/Time Reserved:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate.ToDateTimeorNull()) + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Total Amount:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.TotalCharges.ToStr() + "<u></u><u></u></p></td></tr></tbody></table></div>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details <u></u><u></u></span></h6><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Name:</strong><u></u><u></u></p></td>" +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Mobile Number:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div><p class=\"MsoNormal\">" + obj.CustomerMobileNo + "<u></u><u></u></p></div></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Phone Number:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerPhoneNo + " <u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Payment Method<u></u><u></u></span></h6><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Card<u></u><u></u></p></div></td></tr><tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details and Important Information to read <u></u><u></u></span></h5>" +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p><ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Please do not book any journey(s) directly with your driver as it is not legal and safe to do so and company will not accept any responsibility. <u></u><u></u></li>" +
                    "<li style=\"background:white\" class=\"MsoNormal\">We will never ask you to send us any email or fax containing your bank card details. If any payment is required you need to pay Online, Phone or via Secured Online Payment form which we will email you separately.<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">Waiting Time Charges - For pick ups from all the airports, we do not charge for the 1st hour from the landing time. Any further waiting time will be charged at £20 per hour pro rata irrespective of any reason.<u></u><u></u></li>" +
                    "<li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr><tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong>" + AppVars.objSubCompany.CompanyName.ToStr() + "</strong> <u></u><u></u></p></div></td></tr></tbody></table></div>" +
                    "<p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToStr();
                }
                else if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && obj.BookingReturns.Count > 0)
                {
                    Booking objReturn = obj.BookingReturns[0];

                    string BabySeat = objReturn.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }


                    sb.Append("<html><Body>");
                    sb.Append("<div><div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\">" +
                    "<p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\">" +
                    "<span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\">" +
                    "<img width=\"234\" height=\"96\" border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p> " +
                    "<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\">" +
                    "<tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Payment Pending)<u></u><u></u></span></h4>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Staff Message<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Your booking has been confirmed .Please print this page off or note down your ref. number(s) for further communication.  <br><strong>We will send your Pick Up Instructions and Driver Details in a separate email.</strong><br>Please <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk\">click here</a> to amend or to create a new journey. <u></u><u></u></p></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Outbound Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">36 " + obj.ToAddress + " <u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + " (<span data-term=\"goog_1153804741\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", obj.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" +
                    "<span data-term=\"goog_1153804742\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm:ss}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.FareRate + "<u></u><u></u></p></td></tr> " +
                    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div> ");
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    else
                    {
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:inset #bfbfbf 1.0pt;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + obj.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Special Requirement<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + obj.SpecialRequirements + "<u></u><u></u></p></div></td> " +
                    "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Booking Details<u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Booking ID:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + " (Outbound) <br>" + objReturn.BookingNo + " (Inbound) <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Date/Time Reserved:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate) + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Total Amount:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.TotalCharges + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details <u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Mobile Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div><p class=\"MsoNormal\">" + obj.CustomerMobileNo + "<u></u><u></u></p></div></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Phone Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerPhoneNo + " <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Return Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.FromAddress + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.ToAddress + " <u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", objReturn.PickupDateTime) + " (<span data-term=\"goog_1153804743\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", objReturn.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804744\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm tt}", objReturn.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + objReturn.FareRate + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle <u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Payment Method<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Pending<u></u><u></u></p></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details and Important Information to read<u></u><u></u></span></h5> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p>" +
                    "<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Please do not book any journey(s) directly with your driver as it is not legal and safe to do so and company will not accept any responsibility. <u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">We will never ask you to send us any email or fax containing your bank card details. If any payment is required you need to pay Online, Phone or via Secured Online Payment form which we will email you separately.<u></u><u></u></li> " +
                    "<li style=\"background:white\" class=\"MsoNormal\">Waiting Time Charges - For pick ups from all the airports, we do not charge for the 1st hour from the landing time. Any further waiting time will be charged at £20 per hour pro rata irrespective of any reason.<u></u><u></u></li>" +
                    "<li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong> <u></u><u></u></p></div></td></tr></tbody></table>" +
                    "</div><p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToString();

                }


            }
            catch (Exception ex)
            { }
        }


        /// <summary>
        /// Passenger Booking Cancel
        /// </summary>
        /// <param name="obj"></param>
        public static void EmailPBCL(Booking obj)
        {
            try
            {
                To = obj.CustomerEmail.ToStr().Trim();
                Subject = "EMAIL - PBCL";

                string BabySeat = obj.BabySeats.ToStr().Trim();
                string Seat1 = string.Empty;
                string Seat2 = string.Empty;
                if (BabySeat.Length > 0)
                {
                    string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                    if (arr.Count() == 1)
                    {
                        Seat1 = arr[0].ToStr().Trim();
                    }
                    else if (arr.Count() == 2)
                    {
                        Seat1 = arr[0].ToStr().Trim();
                        Seat2 = arr[1].ToStr().Trim();

                    }
                }


                StringBuilder sb = new StringBuilder();
                sb.Append("<html><Body>");
                //sb.Append("<div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\"><p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\"><span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\"><img width=\"234\" height=\"96\" border=\"0\" src=\"https://ci4.googleusercontent.com/proxy/817aYftMbsUhUhLBks4OaUe--YiuRwRWzLP2oKjbEnczdeLmSZh5L-nNlrURcAiksJj_x5E0rllY_f_gCXpyaqZLfD90xYw0L2iXC_CC8w=s0-d-e1-ft#http://jewelsairport.neowebservices.co.uk/images/logo.png\" class=\"CToWUd\"><u></u><u></u></span></p>" +
                //"<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\"><tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"></td>" +
                //"<td width=\"391\" style=\"border:none;padding:0in 0in 0in 0in\"><p class=\"MsoNormal\">&nbsp;</p></td></tr><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Cancelled )<u></u><u></u></span></h4>" +
                //"<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Dear " + obj.CustomerName + ",<u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">Your journey cancelled and money refunded as per our <a target=\"_blank\" href=\"" + AppVars.objSubCompany.WebsiteUrl + "\"><span style=\"color:#c64f0f\">Terms and Conditions</span></a>.<u></u><u></u></p></div>" +
                //"<p style=\"background:white\" class=\"MsoNormal\">Your refund amount will reflect on your statement after 7-15 days. <u></u><u></u></p></div><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Job Details <u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td>" +
                //"<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr><tr>" +
                //"<td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + " <u></u><u></u></p></td></tr>" +
                //"<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy}", obj.PickupDateTime.ToDate()) + "<u></u><u></u></p></td></tr>" +
                //"<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804738\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0: HH:mm}", obj.PickupDateTime.ToDateTimeorNull()) + "</span></span><u></u><u></u></p></td></tr>");
                //if (obj.ToLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                //{
                //    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo.ToStr() + "<u></u><u></u></p></td></tr></tbody></table></div></td>");
                //}
                //sb.Append("<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Reservation Details<u></u><u></u></span></h6>" +
                //"<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Reservation ID:</strong><u></u><u></u></p></td>" +
                //"<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Cancel Date:</strong><u></u><u></u></p></td>" +
                //"<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", DateTime.Now.ToDate()) + "<u></u><u></u></p></td></tr></tbody></table></div><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details<u></u><u></u></span></h6>" +
                //"<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Passanger Name:</strong><u></u><u></u></p></td>" +
                //"<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + " <u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email ID:</strong><u></u><u></u></p></td>" +
                //"<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a>" +
                //"<u></u><u></u></p></td></tr></tbody></table></div></td></tr><tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details and Important Information to read <u></u><u></u></span></h5>" +
                //"<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p>" +
                //"<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Please do not book any journey(s) directly with your driver as it is not legal and safe to do so and company will not accept any responsibility. <u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">We will never ask you to send us any email or fax containing your bank card details. If any payment is required you need to pay Online, Phone or via Secured Online Payment form which we will email you separately.<u></u><u></u></li>" +
                //"<li style=\"background:white\" class=\"MsoNormal\">Waiting Time Charges - For pick ups from all the airports, we do not charge for the 1st hour from the landing time. Any further waiting time will be charged at £20 per hour pro rata irrespective of any reason.<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">"+AppVars.objSubCompany.EmailAddress+"</span></a><u></u><u></u></li></ul></div></td></tr>" +
                //"<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><div><p style=\"background:white\" class=\"MsoNormal\"><strong>Note:</strong> If you requested “MEET AND GREET” then your driver will be waiting in the arrivals hall with your name board. Otherwise, please call on the number provided to arrange your pickup from outside the terminal. <u></u><u></u></p></div>" +
                //"<p style=\"background:white\" class=\"MsoNormal\">If you found any difficulties locating your driver then please call our office on " + AppVars.objSubCompany.TelephoneNo + " <u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">&nbsp;<u></u><u></u></p></div><div>" +
                //"<p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong><u></u><u></u></p></div></div></td></tr></tbody></table></div>" +
                //"<p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div>");

                sb.Append("<div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\"><p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\"><span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\"><img width=\"234\" height=\"96\" border=\"0\" src=\"https://ci4.googleusercontent.com/proxy/817aYftMbsUhUhLBks4OaUe--YiuRwRWzLP2oKjbEnczdeLmSZh5L-nNlrURcAiksJj_x5E0rllY_f_gCXpyaqZLfD90xYw0L2iXC_CC8w=s0-d-e1-ft#http://jewelsairport.neowebservices.co.uk/images/logo.png\" class=\"CToWUd\"><u></u><u></u></span></p><div><div align=\"center\">" +
                "<table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\"><tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"></td><td width=\"391\" style=\"border:none;padding:0in 0in 0in 0in\"><p class=\"MsoNormal\">&nbsp;</p></td></tr><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Cancelled )<u></u><u></u></span></h4><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Dear " + obj.CustomerName + ",<u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">Your journey cancelled and money refunded as per our <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk/terms-and-conditions-177.html\"><span style=\"color:#c64f0f\">Terms and Conditions</span></a>.<u></u><u></u></p></div><p style=\"background:white\" class=\"MsoNormal\">Your refund amount will reflect on your statement after 7-15 days. <u></u><u></u></p></div>" +
                "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Job Details <u></u><u></u></span></h5>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td>" +
                "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + " <u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804738\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr>");
                if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {
                    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div></td>");
                }
                else
                {
                    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div></td>");
                }
                sb.Append("<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Reservation Details<u></u><u></u></span></h6>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Reservation ID:</strong><u></u><u></u></p></td>" +
               "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Cancel Date:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + "<u></u><u></u></p></td></tr></tbody></table></div>" +
                "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details<u></u><u></u></span></h6>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\">" +
                "<p class=\"MsoNormal\"><strong>Passanger Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email ID:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:ahamedfniyas@aol.co.uk\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div></td></tr>" +
                "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details and Important Information to read <u></u><u></u></span></h5>" +
                "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p>" +
                "<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Please do not book any journey(s) directly with your driver as it is not legal and safe to do so and company will not accept any responsibility. <u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">We will never ask you to send us any email or fax containing your bank card details. If any payment is required you need to pay Online, Phone or via Secured Online Payment form which we will email you separately.<u></u><u></u></li>" +
                "<li style=\"background:white\" class=\"MsoNormal\">Waiting Time Charges - For pick ups from all the airports, we do not charge for the 1st hour from the landing time. Any further waiting time will be charged at £20 per hour pro rata irrespective of any reason.<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li>" +
                "<li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:sales@jewels-airport-transfers.com\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr>" +
                "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><div><p style=\"background:white\" class=\"MsoNormal\"><strong>Note:</strong> If you requested “MEET AND GREET” then your driver will be waiting in the arrivals hall with your name board. Otherwise, please call on the number provided to arrange your pickup from outside the terminal. <u></u><u></u></p></div>" +
                "<p style=\"background:white\" class=\"MsoNormal\">If you found any difficulties locating your driver then please call our office on " + AppVars.objSubCompany.TelephoneNo + " <u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">&nbsp;<u></u><u></u></p></div><div><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong><u></u><u></u></p></div></div></td></tr></tbody></table></div>" +
                "<p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div>");




                sb.Append("</body></html>");
                Body = sb.ToStr();

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Passenger Booking Cancel
        /// </summary>
        /// <param name="obj"></param>
        public static void EmailPBCancel(Booking obj)
        {
            try
            {
                To = obj.CustomerEmail.ToStr().Trim();
                Subject = "Your Booking is canceled.";

                string BabySeat = obj.BabySeats.ToStr().Trim();
                string Seat1 = string.Empty;
                string Seat2 = string.Empty;
                if (BabySeat.Length > 0)
                {
                    string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                    if (arr.Count() == 1)
                    {
                        Seat1 = arr[0].ToStr().Trim();
                    }
                    else if (arr.Count() == 2)
                    {
                        Seat1 = arr[0].ToStr().Trim();
                        Seat2 = arr[1].ToStr().Trim();

                    }
                }


                StringBuilder sb = new StringBuilder();
                sb.Append("<html><Body>");
                //sb.Append("<div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\"><p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\"><span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\"><img width=\"234\" height=\"96\" border=\"0\" src=\"https://ci4.googleusercontent.com/proxy/817aYftMbsUhUhLBks4OaUe--YiuRwRWzLP2oKjbEnczdeLmSZh5L-nNlrURcAiksJj_x5E0rllY_f_gCXpyaqZLfD90xYw0L2iXC_CC8w=s0-d-e1-ft#http://jewelsairport.neowebservices.co.uk/images/logo.png\" class=\"CToWUd\"><u></u><u></u></span></p>" +
                //"<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\"><tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"></td>" +
                //"<td width=\"391\" style=\"border:none;padding:0in 0in 0in 0in\"><p class=\"MsoNormal\">&nbsp;</p></td></tr><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Cancelled )<u></u><u></u></span></h4>" +
                //"<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Dear " + obj.CustomerName + ",<u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">Your journey cancelled and money refunded as per our <a target=\"_blank\" href=\"" + AppVars.objSubCompany.WebsiteUrl + "\"><span style=\"color:#c64f0f\">Terms and Conditions</span></a>.<u></u><u></u></p></div>" +
                //"<p style=\"background:white\" class=\"MsoNormal\">Your refund amount will reflect on your statement after 7-15 days. <u></u><u></u></p></div><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Job Details <u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td>" +
                //"<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr><tr>" +
                //"<td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + " <u></u><u></u></p></td></tr>" +
                //"<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy}", obj.PickupDateTime.ToDate()) + "<u></u><u></u></p></td></tr>" +
                //"<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804738\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0: HH:mm}", obj.PickupDateTime.ToDateTimeorNull()) + "</span></span><u></u><u></u></p></td></tr>");
                //if (obj.ToLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                //{
                //    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo.ToStr() + "<u></u><u></u></p></td></tr></tbody></table></div></td>");
                //}
                //sb.Append("<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Reservation Details<u></u><u></u></span></h6>" +
                //"<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Reservation ID:</strong><u></u><u></u></p></td>" +
                //"<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Cancel Date:</strong><u></u><u></u></p></td>" +
                //"<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", DateTime.Now.ToDate()) + "<u></u><u></u></p></td></tr></tbody></table></div><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details<u></u><u></u></span></h6>" +
                //"<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Passanger Name:</strong><u></u><u></u></p></td>" +
                //"<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + " <u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email ID:</strong><u></u><u></u></p></td>" +
                //"<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a>" +
                //"<u></u><u></u></p></td></tr></tbody></table></div></td></tr><tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details and Important Information to read <u></u><u></u></span></h5>" +
                //"<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p>" +
                //"<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Please do not book any journey(s) directly with your driver as it is not legal and safe to do so and company will not accept any responsibility. <u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">We will never ask you to send us any email or fax containing your bank card details. If any payment is required you need to pay Online, Phone or via Secured Online Payment form which we will email you separately.<u></u><u></u></li>" +
                //"<li style=\"background:white\" class=\"MsoNormal\">Waiting Time Charges - For pick ups from all the airports, we do not charge for the 1st hour from the landing time. Any further waiting time will be charged at £20 per hour pro rata irrespective of any reason.<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">"+AppVars.objSubCompany.EmailAddress+"</span></a><u></u><u></u></li></ul></div></td></tr>" +
                //"<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><div><p style=\"background:white\" class=\"MsoNormal\"><strong>Note:</strong> If you requested “MEET AND GREET” then your driver will be waiting in the arrivals hall with your name board. Otherwise, please call on the number provided to arrange your pickup from outside the terminal. <u></u><u></u></p></div>" +
                //"<p style=\"background:white\" class=\"MsoNormal\">If you found any difficulties locating your driver then please call our office on " + AppVars.objSubCompany.TelephoneNo + " <u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">&nbsp;<u></u><u></u></p></div><div>" +
                //"<p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong><u></u><u></u></p></div></div></td></tr></tbody></table></div>" +
                //"<p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div>");

                sb.Append("<div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\"><p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\"><span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\"><img width=\"234\" height=\"96\" border=\"0\" src=\"https://ci4.googleusercontent.com/proxy/817aYftMbsUhUhLBks4OaUe--YiuRwRWzLP2oKjbEnczdeLmSZh5L-nNlrURcAiksJj_x5E0rllY_f_gCXpyaqZLfD90xYw0L2iXC_CC8w=s0-d-e1-ft#http://jewelsairport.neowebservices.co.uk/images/logo.png\" class=\"CToWUd\"><u></u><u></u></span></p><div><div align=\"center\">" +
                "<table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\"><tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"></td><td width=\"391\" style=\"border:none;padding:0in 0in 0in 0in\"><p class=\"MsoNormal\">&nbsp;</p></td></tr><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Cancelled )<u></u><u></u></span></h4><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Dear " + obj.CustomerName + ",<u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">Your journey cancelled and money refunded as per our <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk/terms-and-conditions-177.html\"><span style=\"color:#c64f0f\">Terms and Conditions</span></a>.<u></u><u></u></p></div><p style=\"background:white\" class=\"MsoNormal\">Your refund amount will reflect on your statement after 7-15 days. <u></u><u></u></p></div>" +
                "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Job Details <u></u><u></u></span></h5>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td>" +
                "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + " <u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804738\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr>");
                if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {
                    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div></td>");
                }
                else
                {
                    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div></td>");
                }
                sb.Append("<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Reservation Details<u></u><u></u></span></h6>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Reservation ID:</strong><u></u><u></u></p></td>" +
               "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Cancel Date:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + "<u></u><u></u></p></td></tr></tbody></table></div>" +
                "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details<u></u><u></u></span></h6>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\">" +
                "<p class=\"MsoNormal\"><strong>Passanger Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email ID:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:ahamedfniyas@aol.co.uk\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div></td></tr>" +
                "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details and Important Information to read <u></u><u></u></span></h5>" +
                "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p>" +
                "<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Please do not book any journey(s) directly with your driver as it is not legal and safe to do so and company will not accept any responsibility. <u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">We will never ask you to send us any email or fax containing your bank card details. If any payment is required you need to pay Online, Phone or via Secured Online Payment form which we will email you separately.<u></u><u></u></li>" +
                "<li style=\"background:white\" class=\"MsoNormal\">Waiting Time Charges - For pick ups from all the airports, we do not charge for the 1st hour from the landing time. Any further waiting time will be charged at £20 per hour pro rata irrespective of any reason.<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li>" +
                "<li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:sales@jewels-airport-transfers.com\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr>" +
                "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><div><p style=\"background:white\" class=\"MsoNormal\"><strong>Note:</strong> If you requested “MEET AND GREET” then your driver will be waiting in the arrivals hall with your name board. Otherwise, please call on the number provided to arrange your pickup from outside the terminal. <u></u><u></u></p></div>" +
                "<p style=\"background:white\" class=\"MsoNormal\">If you found any difficulties locating your driver then please call our office on " + AppVars.objSubCompany.TelephoneNo + " <u></u><u></u></p><div><p style=\"background:white\" class=\"MsoNormal\">&nbsp;<u></u><u></u></p></div><div><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong><u></u><u></u></p></div></div></td></tr></tbody></table></div>" +
                "<p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div>");




                sb.Append("</body></html>");
                Body = sb.ToStr();

            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// Passenger Payment Invoice
        /// </summary>
        /// <param name="obj"></param>
        public static void EmailPPI(Booking obj)
        {

            try
            {
                Subject = "EMAIL - PPI";
                To = obj.CustomerEmail.ToStr().Trim();

                BookingBO objBooking = new BookingBO();
                objBooking.GetByPrimaryKey(obj.Id);


                string bb = objBooking.Current.BookingNo;

                StringBuilder sb = new StringBuilder();
                if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY)
                {
                    string BabySeat = obj.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }


                    sb.Append("<html><Body>");
                    sb.Append("<div><div><div><p align=\"center\" style=\"text-align:center\" class=\"MsoNormal\"> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\"><img border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p><div><p class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Phone :</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></span></p></div></div> " +
                    "<div><div><h4 align=\"center\" style=\"text-align:center\"><span style=\"font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Invoice Details<u></u><u></u></span> " +
                    "</h4><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Company Registeration No:</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + AppVars.objSubCompany.CompanyNumber + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Private Hire Registered Licence Number:</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">0087050101</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><h4><span style=\"font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Journey Receipt: <span>JAT13102751</span><u></u><u></u></span></h4> " +
                    "<p class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Passenger Details<u></u><u></u></span></p><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Passenger Name :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.CustomerName + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Phone No. :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.CustomerMobileNo + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Email Address :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\">" + obj.CustomerEmail + "</a></span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Passengers Travelling :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.NoofPassengers + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> " +
                    "<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Phone Number :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.CustomerPhoneNo + " </span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><p class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Car Details <u></u><u></u></span></p><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Car Details :</span></span>" +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.Fleet_VehicleType.VehicleType + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Payment Method :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Card</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> " +
                    "<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Luggages :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.NoofLuggages + "</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Baby Seat :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + Seat1 + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Baby Seat :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + Seat2 + "</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><p class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Outbound Journey Details <u></u><u></u></span></p>" +
                    "<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pickup Date &amp; Time :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + " at " + string.Format("{0:HH:mm}", obj.PickupDateTime) + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li>");
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flying From :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.FromDoorNo.ToStr() + "</span></span> " +
                        "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flight No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.FromStreet.ToStr() + " </span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> ");
                    }
                    else
                    {
                        sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From Door No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.FromDoorNo.ToStr() + "</span></span> " +
                        "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> ");
                    }
                    sb.Append("<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pick Up Address :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.FromAddress + "</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Drop Off Address :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.ToAddress + "</span></span>" +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">&nbsp;</span></span><span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">To</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Distance</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Ref No</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Amount</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Journey</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"> <span>" + obj.FromAddress + "</span> <span>" + obj.ToAddress + "</span> <span>" + obj.ExtraMile + " Mile(s)</span> <span>" + obj.BookingNo + "</span> <span>£" + obj.FareRate + "</span> <u></u><u></u></span></li> " +
                        //   "<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Sub Total: £27.50 <u></u><u></u></span></li> " +
                        //"<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Return Journey Details<u></u><u></u></span></li></ul><ul type=\"disc\"><ul type=\"circle\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pick Up Time :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">30-10-2015 at 05:00</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                        //"<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flying From :</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                        //"<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flight No :</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                        //"<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pick Up Address :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">36 Brendon Close Harlington Hayes UB3 5NQ</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                        //"<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Drop Off Address :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Heathrow Airport Terminal 3 Heathrow Airport, Hounslow TW6 3XA</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul></ul> " +
                        //"<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">&nbsp;</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From</span></span> " +
                        //"<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">To</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Distance</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Ref No</span></span> " +
                        //"<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Amount</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                        // "<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Journey1</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"> <span>UB3 5NQ, 36 Brendon Close Harlington Hayes</span> <span>Heathrow Airport Terminal 3, Heathrow Airport, Hounslow TW6 3XA</span> <span>2 Mile(s)</span> <span>AHM13102751-2</span> <span>£" + obj.FareRate + "</span> <u></u><u></u></span></li>" +
                        //   "<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Sub Total: £22.50 <u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Card Sur-charge: £ 0.00 <u></u><u></u></span></li> " +
                    "<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Total Amount: </span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">£" + obj.TotalCharges + "</span></span>" +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"> <u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><strong><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Other Information: </span></strong><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.SpecialRequirements + "<u></u><u></u></span></li> " +
                    "<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Booking Date &amp; Time: <span>" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate) + "</span> <u></u><u></u></span></li></ul> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-family:Symbol;color:black\">·</span><span style=\"font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; </span><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details<u></u><u></u></span></h5> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></span></p> " +
                    "<ul type=\"disc\"><li style=\"color:black;background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></span></li><li style=\"color:black;background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></span></li></ul></div> " +
                    "<p style=\"background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong><span style=\"font-family:&quot;Helvetica&quot;,sans-serif\">" + AppVars.objSubCompany.CompanyName + "</span></strong> <u></u><u></u></span></p></div></div></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToString();
                }
                else if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && obj.BookingReturns.Count > 0)
                {
                    Booking objReturn = obj.BookingReturns[0];
                    string BabySeat = obj.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }

                    sb.Append("<html><Body>");
                    sb.Append("<div><div><div><p align=\"center\" style=\"text-align:center\" class=\"MsoNormal\"> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\"><img border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p><div><p class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Phone :</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></span></p></div></div> " +
                    "<div><div><h4 align=\"center\" style=\"text-align:center\"><span style=\"font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Invoice Details<u></u><u></u></span> " +
                    "</h4><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Company Registeration No:</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + AppVars.objSubCompany.CompanyNumber + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Private Hire Registered Licence Number:</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">0087050101</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><h4><span style=\"font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Journey Receipt: <span>JAT13102751</span><u></u><u></u></span></h4> " +
                    "<p class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Passenger Details<u></u><u></u></span></p><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Passenger Name :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.CustomerName + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Phone No. :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.CustomerMobileNo + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Email Address :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\">" + obj.CustomerEmail + "</a></span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Passengers Travelling :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.NoofPassengers + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> " +
                    "<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Phone Number :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.CustomerPhoneNo + " </span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><p class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Car Details <u></u><u></u></span></p><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Car Details :</span></span>" +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.Fleet_VehicleType.VehicleType + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Payment Method :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Card</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> " +
                    "<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Luggages :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.NoofLuggages + "</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Baby Seat :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + Seat1 + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Baby Seat :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + Seat2 + "</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><p class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Outbound Journey Details <u></u><u></u></span></p>" +
                    "<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pickup Date &amp; Time :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + " at " + string.Format("{0:HH:mm}", obj.PickupDateTime) + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li>");
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flying From :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.FromDoorNo.ToStr() + "</span></span> " +
                        "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flight No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.FromDoorNo.ToStr() + " </span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> ");
                    }
                    else
                    {
                        sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From Door No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.FromDoorNo.ToStr() + "</span></span> " +
                        "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> ");
                    }
                    sb.Append("<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pick Up Address :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromAddress + "</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Drop Off Address :</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.ToAddress + "</span></span>" +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul><ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">&nbsp;</span></span><span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">To</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Distance</span></span> " +
                    "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Ref No</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Amount</span></span> " +
                    "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Journey1</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"> <span>" + obj.FromAddress + "</span> <span>" + obj.ToAddress + "</span> <span>" + obj.DistanceString + " Mile(s)</span> <span>" + obj.BookingNo + "</span> <span>£" + obj.FareRate + "</span> <u></u><u></u></span></li> " +
                           "<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Sub Total: £" + obj.FareRate + " <u></u><u></u></span></li> " +
                        "<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Return Journey Details<u></u><u></u></span></li></ul><ul type=\"disc\"><ul type=\"circle\">");
                    if (objReturn.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        //    sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flying From :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromDoorNo.ToStr() + "</span></span> " +
                        //   "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flight No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromFlightNo.ToStr() + " </span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> ");
                        sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flying From :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromDoorNo.ToStr() + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li>" +
                            "<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Flight No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromFlightNo.ToStr() + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li>");
                    }
                    else
                    {
                        // sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From Door No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromDoorNo.ToStr() + "</span></span> " +
                        //"<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul> ");
                        sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From Door No :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromDoorNo.ToStr() + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li>");
                    }
                    //here to code
                    sb.Append("<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pick Up Time :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + string.Format("{0:dd-MM-yyyy}", objReturn.PickupDateTime) + " at " + string.Format("{0:HH:mm}", obj.PickupDateTime) + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                  "<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Pick Up Address :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.FromAddress + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                   "<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Drop Off Address :</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + objReturn.ToAddress + "</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li></ul></ul> " +
                   "<ul type=\"disc\"><li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">&nbsp;</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">From</span></span> " +
                   "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">To</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Distance</span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Ref No</span></span> " +
                   "<span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Amount</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"><u></u><u></u></span></li> " +
                    "<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Journey1</span></span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"> <span>" + objReturn.FromAddress + "</span> <span>" + objReturn.ToAddress + "</span> <span>" + objReturn.DistanceString + " Mile(s)</span> <span>" + objReturn.BookingNo + "</span> <span>£" + objReturn.FareRate + "</span> <u></u><u></u></span></li>" +
                      "<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Sub Total: £" + objReturn.FareRate + " <u></u><u></u></span></li>" +
               "<li style=\"color:black\" class=\"MsoNormal\"><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Total Amount: </span></span><span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">£" + obj.TotalCharges + "</span></span>" +
               "<span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\"> <u></u><u></u></span></li><li style=\"color:black\" class=\"MsoNormal\"><strong><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Other Information: </span></strong><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">" + obj.SpecialRequirements + "<u></u><u></u></span></li> " +
               "<li style=\"color:black\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Booking Date &amp; Time: <span>" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate) + "</span> <u></u><u></u></span></li></ul> " +
               "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-family:Symbol;color:black\">·</span><span style=\"font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; </span><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details<u></u><u></u></span></h5> " +
               "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></span></p> " +
               "<ul type=\"disc\"><li style=\"color:black;background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></span></li><li style=\"color:black;background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></span></li></ul></div> " +
               "<p style=\"background:white\" class=\"MsoNormal\"><span style=\"font-size:10.0pt;font-family:Symbol;color:black\">·</span><span style=\"font-size:10.0pt;font-family:&quot;Helvetica&quot;,sans-serif;color:black\">&nbsp; Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong><span style=\"font-family:&quot;Helvetica&quot;,sans-serif\">" + AppVars.objSubCompany.CompanyName + "</span></strong> <u></u><u></u></span></p></div></div></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToString();
                }

            }
            catch (Exception ex)
            { }
        }


        /// <summary>
        /// Passenger Booking Confirmation
        /// </summary>
        /// <param name="obj"></param>
        public static void EmailPBC(Booking obj)
        {
            try
            {
                string paymentType = obj.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CASH ? "Cash" : "Card";

                if (obj.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT)
                    paymentType = "Account";

                else if (obj.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.CASH)
                {
                    paymentType = "Card";

                }


                Subject = "EMAIL - PBC";
                To = obj.CustomerEmail.ToStr().Trim();
                if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY)
                {
                    string BabySeat = obj.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }





                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html><Body>");
                    sb.Append("<div><div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\">" +
                    "<p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\">" +
                    "<span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\">" +
                    "<img width=\"234\" height=\"96\" border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p> " +
                    "<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\">" +
                    "<tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Confirmed )<u></u><u></u></span></h4>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Staff Message<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Your booking has been confirmed .Please print this page off or note down your ref. number(s) for further communication. <br>Please <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk\">click here</a> to amend or to create a new journey. <u></u><u></u></p></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Outbound Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + " <u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + " (<span data-term=\"goog_1153804741\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", obj.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" +
                    "<span data-term=\"goog_1153804742\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm tt}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.FareRate + "<u></u><u></u></p></td></tr> " +
                    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div> ");// +
                    //"<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5>");
                    //"<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5>");
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromStreet + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    else
                    {
                        // sb.Append("<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">"+obj.FromDoorNo+"<u></u><u></u></p></td></tr></tbody></table></div>");
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:inset #bfbfbf 1.0pt;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    sb.Append(
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + obj.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Special Requirement<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + obj.SpecialRequirements + "<u></u><u></u></p></div></td> " +
                    "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Booking Details<u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Booking ID:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + " (Outbound)<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Date/Time Reserved:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate) + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Total Amount:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.TotalCharges + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details <u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Mobile Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div><p class=\"MsoNormal\">" + obj.CustomerMobileNo + "<u></u><u></u></p></div></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Phone Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerPhoneNo + " <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div> " +

                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Payment Method<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + paymentType + "<u></u><u></u></p></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details<u></u><u></u></span></h5> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p> " +
                    "<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li> " +
                    "<li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong> <u></u><u></u></p></div></td></tr></tbody></table>" +
                    "</div><p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToString();
                }
                else if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && obj.BookingReturns.Count > 0)
                {
                    Booking objReturn = obj.BookingReturns[0];

                    string BabySeat = objReturn.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }


                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html><Body>");
                    sb.Append("<div><div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\">" +
                    "<p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\">" +
                    "<span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\">" +
                    "<img width=\"234\" height=\"96\" border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p> " +
                    "<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\">" +
                    "<tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Confirmed )<u></u><u></u></span></h4>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Staff Message<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Your booking has been confirmed .Please print this page off or note down your ref. number(s) for further communication. <br>Please <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk\">click here</a> to amend or to create a new journey. <u></u><u></u></p></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Outbound Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + " (<span data-term=\"goog_1153804741\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", obj.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" +
                    "<span data-term=\"goog_1153804742\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm tt}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.FareRate + "<u></u><u></u></p></td></tr> " +
                    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    //"<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody>");
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    else
                    {
                        // sb.Append("<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">"+obj.FromDoorNo+"<u></u><u></u></p></td></tr></tbody></table></div>");
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:inset #bfbfbf 1.0pt;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + obj.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Special Requirement<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + obj.SpecialRequirements + " <u></u><u></u></p></div></td> " +
                    "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Booking Details<u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Booking ID:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + " (Outbound) <br>" + objReturn.BookingNo + " (Inbound) <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Date/Time Reserved:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate) + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Total Amount:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.TotalCharges + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details <u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Mobile Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div><p class=\"MsoNormal\">" + obj.CustomerMobileNo + "<u></u><u></u></p></div></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Phone Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerPhoneNo + " <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Return Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.FromAddress + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.ToAddress + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", objReturn.PickupDateTime) + " (<span data-term=\"goog_1153804743\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", objReturn.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804744\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm tt}", objReturn.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + objReturn.FareRate + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle <u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Payment Method<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + paymentType + "<u></u><u></u></p></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details<u></u><u></u></span></h5> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p> " +
                    "<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li> " +
                    "<li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong> <u></u><u></u></p></div></td></tr></tbody></table>" +
                    "</div><p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToStr();
                }
            }
            catch (Exception ex)
            { }

        }


        /// <summary>
        /// Passenger Driver Confirmation
        /// </summary>
        /// <param name="obj"></param>
        public static void EmailPDD(Booking obj)
        {
            try
            {
                //string[] arrSplit = new string[] {"<<<" };
                //string[] vals= BabySeat.Split(arrSplit, StringSplitOptions.None);
                //if (vals.Count() > 1)
                //{ 

                //}

                Subject = "EMAIL - PDD";
                To = obj.CustomerEmail.ToStr().Trim();
                string BabySeat = obj.BabySeats.ToStr().Trim();
                string Seat1 = string.Empty;
                string Seat2 = string.Empty;
                if (BabySeat.Length > 0)
                {
                    string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                    if (arr.Count() == 1)
                    {
                        Seat1 = arr[0].ToStr().Trim();
                    }
                    else if (arr.Count() == 2)
                    {
                        Seat1 = arr[0].ToStr().Trim();
                        Seat2 = arr[1].ToStr().Trim();

                    }
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<html><Body>");
                sb.Append("<div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\"><p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\">" +
                "<span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\"><img width=\"234\" height=\"96\" border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p>" +
                "<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\"><tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" +
                "Dear " + obj.CustomerName + ",<br>Your journey has been confirmed and assigned to the driver. Please find your <strong>Pick Up Instructions and Driver Details</strong> below. <u></u><u></u></p></div><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Your Driver contact Number :" + obj.Fleet_Driver.MobileNo + "<u></u><u></u></span></h6>" +
                "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\"><strong>Pick Up Instructions</strong> :<br><br><strong>With “Meet and Greet” service </strong>: When you enter the Arrivals hall please make your way to the “Information Desk” ( i ) and your driver will meet you here holding your name on the board. If you can not find your driver then please call him/her on the number supplied. <strong>07533129380</strong>." +
                "<br><br><strong>Without “Meet and Greet” service </strong>: Please call your driver on the number supplied (" + obj.Fleet_Driver.MobileNo + " ) within 1 hour after landing to ensure that you are at the airport . Your Driver will guide you through to the Pickup point which is normally located outside the terminal at the Drop Off area on the departure level .<u></u><u></u></p></div><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details<u></u><u></u></span></h5>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr><tr>" +
                "<td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804739\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\">" +
                "<p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div>");//+
                //"<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr>");

                //if (obj.FromLocId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                //{
                //    sb.Append("<td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromFlightNo.ToStr() + " <u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td>" +
                //    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr>");
                //}
                //else
                //{
                //    sb.Append("<td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo.ToStr() + " <u></u><u></u></p></td></tr><tr>");
                //}
                if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {

                    sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromStreet + "<u></u><u></u></p></td></tr></tbody></table></div>");
                }
                else
                {
                    // sb.Append("<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">"+obj.FromDoorNo+"<u></u><u></u></p></td></tr></tbody></table></div>");
                    sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:inset #bfbfbf 1.0pt;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                }
                sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle<u></u><u></u></span></h5>" +
                "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofLuggages + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div>" +
                "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Special Requirement<u></u><u></u></span></h6><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + obj.SpecialRequirements + "<u></u><u></u></p></div></td>" +
                "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Booking Details<u></u><u></u></span></h6><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Booking ID:</strong><u></u><u></u></p></td>" +
                "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Payment Method:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">Card<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Total Amount:</strong><u></u><u></u></p></td>" +
                "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.FareRate + "<u></u><u></u></p></td></tr></tbody></table></div><h6 styl=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details<u></u><u></u></span></h6><div>" +
                "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr>" +
                "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Mobile Number:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div><p class=\"MsoNormal\">" + obj.CustomerMobileNo + "<u></u><u></u></p></div></td></tr></tbody></table></div></td></tr><tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details and Important Information to read <u></u><u></u></span>" +
                "</h5><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p><ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Please do not book any journey(s) directly with your driver as it is not legal and safe to do so and company will not accept any responsibility. <u></u><u></u></li>" +
                "<li style=\"background:white\" class=\"MsoNormal\">We will never ask you to send us any email or fax containing your bank card details. If any payment is required you need to pay Online, Phone or via Secured Online Payment form which we will email you separately.<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">Waiting Time Charges - For pick ups from all the airports, we do not charge for the 1st hour from the landing time. Any further waiting time will be charged at £20 per hour pro rata irrespective of any reason.<u></u><u></u></li><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li>" +
                "<li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr>" +
               "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><div><p style=\"background:white\" class=\"MsoNormal\">" + AppVars.objSubCompany.CompanyName + " wish you a pleasant journey.<br><br>To manage your journeys or to make additional bookings , please <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk/login.htm\">login</a> into your account . <br><br>Thank you for choosing " + AppVars.objSubCompany.CompanyName + " and hope to see you again. <br><br>Kind Regards,<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong> <u></u><u></u></p></div></div></td></tr></tbody></table></div><p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div>");
                sb.Append("</body></html>");
                Body = sb.ToStr();
            }
            catch (Exception ex)
            { }
        }


        /// <summary>
        /// Passenger Payment Confirmation
        /// </summary>
        /// <param name="obj"></param>
        public static void EmailPPC(Booking obj)
        {
            try
            {
                Subject = "EMAIL - PPC";
                To = obj.CustomerEmail.ToStr().Trim();
                if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY)
                {
                    string BabySeat = obj.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }


                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html><Body>");
                    sb.Append("<div><div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\">" +
                    "<p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\">" +
                    "<span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\">" +
                    "<img width=\"234\" height=\"96\" border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p> " +
                    "<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\">" +
                    "<tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Confirmed )<u></u><u></u></span></h4>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Staff Message<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Your booking has been confirmed .Please print this page off or note down your ref. number(s) for further communication. <br>Please <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk\">click here</a> to amend or to create a new journey. <u></u><u></u></p></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Outbound Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + " <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + " (<span data-term=\"goog_1153804741\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", obj.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" +
                    "<span data-term=\"goog_1153804742\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm tt}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.FareRate + "<u></u><u></u></p></td></tr> " +
                    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div> ");// +
                    //"<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody>");
                    //if (obj.FromLocId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    //{
                    //    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td> " +
                    //    "<td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromFlightNo.ToStr() + "<u></u><u></u></p></td></tr> " +
                    //    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"> " +
                    //    "<strong>Arriving from:</strong><u></u><u></u></p></td>" +
                    //    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">Bahrain<u></u><u></u></p></td></tr>");
                    //}
                    //else
                    //{
                    //    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td> " +
                    //    "<td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo.ToStr() + "<u></u><u></u></p></td></tr> ");
                    //}

                    //sb.Append("</tbody></table></div> " +
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromStreet + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    else
                    {
                        // sb.Append("<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">"+obj.FromDoorNo+"<u></u><u></u></p></td></tr></tbody></table></div>");
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:inset #bfbfbf 1.0pt;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + obj.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Special Requirement<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + obj.SpecialRequirements + "<u></u><u></u></p></div></td> " +
                    "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Booking Details<u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Booking ID:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + " (Outbound)<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Date/Time Reserved:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate) + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Total Amount:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.TotalCharges + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details <u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Mobile Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div><p class=\"MsoNormal\">" + obj.CustomerMobileNo + "<u></u><u></u></p></div></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Phone Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerPhoneNo + " <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div> " +

                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Payment Method<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Card<u></u><u></u></p></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details<u></u><u></u></span></h5> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p> " +
                    "<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li> " +
                    "<li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong> <u></u><u></u></p></div></td></tr></tbody></table>" +
                    "</div><p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToString();
                }
                else if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && obj.BookingReturns.Count > 0)
                {
                    Booking objReturn = obj.BookingReturns[0];

                    string BabySeat = objReturn.BabySeats.ToStr().Trim();
                    string Seat1 = string.Empty;
                    string Seat2 = string.Empty;
                    if (BabySeat.Length > 0)
                    {
                        string[] arr = BabySeat.Split(new string[] { "<<<" }, StringSplitOptions.None);
                        if (arr.Count() == 1)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                        }
                        else if (arr.Count() == 2)
                        {
                            Seat1 = arr[0].ToStr().Trim();
                            Seat2 = arr[1].ToStr().Trim();

                        }
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html><Body>");
                    sb.Append("<div><div style=\"border:solid #bfbfbf 1.0pt;padding:0in 0in 0in 0in;margin-top:7.5pt;margin-bottom:7.5pt;border-radius:4px\">" +
                    "<p align=\"center\" style=\"text-align:center;line-height:14.25pt;background:#f3f3f3\" class=\"MsoNormal\">" +
                    "<span style=\"font-size:9.0pt;font-family:&quot;Arial&quot;,sans-serif;color:#252525\">" +
                    "<img width=\"234\" height=\"96\" border=\"0\" src=\"" + AppVars.objSubCompany.CompanyLogoOnlinePath + "\" class=\"CToWUd\"><u></u><u></u></span></p> " +
                    "<div><div align=\"center\"><table width=\"800\" cellspacing=\"6\" cellpadding=\"0\" border=\"0\" style=\"width:600.0pt\">" +
                    "<tbody><tr><td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h4 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:13.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Journey Details (Confirmed )<u></u><u></u></span></h4>" +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Staff Message<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Your booking has been confirmed .Please print this page off or note down your ref. number(s) for further communication. <br>Please <a target=\"_blank\" href=\"http://www.jewels-airport-transfers.co.uk\">click here</a> to amend or to create a new journey. <u></u><u></u></p></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Outbound Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromAddress + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.ToAddress + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd-MM-yyyy}", obj.PickupDateTime) + " (<span data-term=\"goog_1153804741\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", obj.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" +
                    "<span data-term=\"goog_1153804742\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm tt}", obj.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.FareRate + "<u></u><u></u></p></td></tr> " +
                    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div>");// +
                    //"<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody>");
                    //if (obj.FromLocId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    //{
                    //    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td> " +
                    //    "<td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromFlightNo.ToStr() + " <u></u><u></u></p></td></tr> " +
                    //    "<tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"> " +
                    //    "<strong>Arriving from:</strong><u></u><u></u></p></td>" +
                    //    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">Bahrain<u></u><u></u></p></td></tr>");
                    //}
                    //else
                    //{
                    //    sb.Append("<tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td> " +
                    //    "<td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo.ToStr() + " <u></u><u></u></p></td></tr> ");
                    //}
                    //sb.Append("</tbody></table></div> " +
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Flight Number:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Arriving from:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    else
                    {
                        // sb.Append("<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td style=\"border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td style=\"border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">"+obj.FromDoorNo+"<u></u><u></u></p></td></tr></tbody></table></div>");
                        sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Transfer Information<u></u><u></u></span></h5><div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>From Door No:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border-top:inset #bfbfbf 1.0pt;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.FromDoorNo + "<u></u><u></u></p></td></tr></tbody></table></div>");
                    }
                    sb.Append("<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + obj.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td><td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"> " +
                    "<p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Special Requirement<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">" + obj.SpecialRequirements + "<u></u><u></u></p></div></td> " +
                    "<td width=\"50%\" valign=\"top\" style=\"width:50.0%;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><h6 style=\"margin:0in;margin-bottom:.0001pt\"> " +
                    "<span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Booking Details<u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\">" +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Booking ID:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.BookingNo + " (Outbound) <br> (Inbound)" + objReturn.BookingNo + " <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Date/Time Reserved:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + string.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.BookingDate) + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Total Amount:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + obj.TotalCharges + ".00<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Passenger Details <u></u><u></u></span></h6> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Name:</strong><u></u><u></u></p></td><td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerName + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Mobile Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><div><p class=\"MsoNormal\">" + obj.CustomerMobileNo + "<u></u><u></u></p></div></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Phone Number:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + obj.CustomerPhoneNo + " <u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Email:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><a target=\"_blank\" href=\"mailto:" + obj.CustomerEmail + "\"><span style=\"color:#c64f0f\">" + obj.CustomerEmail + "</span></a><u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Return Journey<u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"> " +
                    "<tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup From:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.FromAddress + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Drop Off To:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.ToAddress + "<u></u><u></u></p></td></tr><tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Date:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">30-10-2015 (<span data-term=\"goog_1153804743\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:dddd}", objReturn.PickupDateTime) + "</span></span>)<u></u><u></u></p></td></tr>" +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Pickup Time:</strong><u></u><u></u></p></td>" +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><span data-term=\"goog_1153804744\" class=\"aBn\" tabindex=\"0\"><span class=\"aQJ\">" + string.Format("{0:HH:mm}", objReturn.PickupDateTime) + "</span></span><u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Price:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">£" + objReturn.FareRate + "<u></u><u></u></p></td></tr><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Number of Passengers:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.NoofPassengers + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Desired Vehicle <u></u><u></u></span></h5> " +
                    "<div><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"width:100.0%;background:white;border-collapse:collapse;border:none\"><tbody><tr><td width=\"40%\" style=\"width:40.0%;border:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Vehicle Type:</strong><u></u><u></u></p></td> " +
                    "<td width=\"60%\" style=\"width:60.0%;border:inset #bfbfbf 1.0pt;border-left:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.Fleet_VehicleType.VehicleType + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Luggage:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + objReturn.NoofLuggages + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 1:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat1 + "<u></u><u></u></p></td></tr> " +
                    "<tr><td style=\"border:inset #bfbfbf 1.0pt;border-top:none;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\"><strong>Baby Seat 2:</strong><u></u><u></u></p></td> " +
                    "<td style=\"border-top:none;border-left:none;border-bottom:inset #bfbfbf 1.0pt;border-right:inset #bfbfbf 1.0pt;padding:4.5pt 4.5pt 4.5pt 4.5pt\"><p class=\"MsoNormal\">" + Seat2 + "<u></u><u></u></p></td></tr></tbody></table></div> " +
                    "<h6 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Payment Method<u></u><u></u></span></h6> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Card<u></u><u></u></p></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><h5 style=\"margin:0in;margin-bottom:.0001pt\"><span style=\"font-size:11.5pt;font-family:&quot;Calibri&quot;,sans-serif;color:#c64f0f;font-weight:normal\">Our Contact Details<u></u><u></u></span></h5> " +
                    "<div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Always keep our phone numbers and you can contact the office in case if you found any difficulty to locate the driver. <u></u><u></u></p> " +
                    "<ul type=\"disc\"><li style=\"background:white\" class=\"MsoNormal\">Phone :" + AppVars.objSubCompany.TelephoneNo + "<u></u><u></u></li> " +
                    "<li style=\"background:white\" class=\"MsoNormal\">E-Mail: <a target=\"_blank\" href=\"mailto:" + AppVars.objSubCompany.EmailAddress + "\"><span style=\"color:#c64f0f\">" + AppVars.objSubCompany.EmailAddress + "</span></a><u></u><u></u></li></ul></div></td></tr> " +
                    "<tr><td valign=\"top\" style=\"padding:4.5pt 4.5pt 4.5pt 4.5pt\" colspan=\"2\"><div style=\"border:solid #bfbfbf 1.0pt;padding:5.0pt 5.0pt 5.0pt 5.0pt;margin-bottom:7.5pt\"><p style=\"background:white\" class=\"MsoNormal\">Thank you for using us and hope to see you again in your future airport transport requirements. Have a nice journey!<br><br>Kind Regards,<br><strong>" + AppVars.objSubCompany.CompanyName + "</strong> <u></u><u></u></p></div></td></tr></tbody></table>" +
                    "</div><p class=\"MsoNormal\"><u></u>&nbsp;<u></u></p></div></div></div>");
                    sb.Append("</body></html>");
                    Body = sb.ToString();
                }
            }
            catch (Exception ex)
            { }
        }


        public static void SendDirectPaymentConfirmationEmail(Booking obj)
        {

            EmailPBC(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();
            Subject = "EMAIL - ABC";
            SendEmail();

            EmailPPC(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();
            Subject = "EMAIL - APC";
            SendEmail();

            EmailPPI(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();
            Subject = "EMAIL - API";
            SendEmail();



        }







        public static void SendCancelBookingEmail(Booking obj)
        {
            EmailPBCL(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                Subject = "EMAIL - ABCL";
                SendEmail();
            }

        }


        public static void SendPaymentPendingEmail(Booking obj)
        {

            EmailPPP(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                Subject = "EMAIL - APP";
                SendEmail();
            }

        }

        public static void SendBookingConfirmationEmail(Booking obj)
        {

            EmailPBC(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                Subject = "EMAIL - ABC";
                SendEmail();
            }
              
        }


        public static void SendDirectBookingConfirmationEmail(Booking obj)
        {

            EmailConfirmation(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                Subject = "Confirmation Email - " + obj.BookingNo;

                if(obj.JourneyTypeId.ToInt()==Enums.JOURNEY_TYPES.RETURN)
                     Subject += " (Return Journey)";
                SendEmail();
            }

        }



        public static void SendBookingQuotationEmail(Booking obj)
        {

            Subject = "Quotation Email - " + obj.BookingNo;
            EmailQuotation(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                Subject = "Quotation Email - " + obj.BookingNo;

                if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                    Subject += " (Return Journey)";
                SendEmail();
            }

        }


        public static void EmailQuotation(Booking obj)
        {

            try
            {

              
                To = obj.CustomerEmail.ToStr().Trim();

                BookingBO objBooking = new BookingBO();
                objBooking.GetByPrimaryKey(obj.Id);


                string bb = objBooking.Current.BookingNo;

                var objSubcompany = obj.Gen_SubCompany.DefaultIfEmpty();

                string mainCustomerName = obj.CustomerName.ToStr();
                StringBuilder sb = new StringBuilder();
              
                
  

                  


                    decimal fares = obj.CustomerPrice.ToDecimal();
                    //  decimal parkingCharges = obj.CongtionCharges.ToDecimal();
                    if (obj.CompanyId != null)
                    {
                        fares = obj.CompanyPrice.ToDecimal();
                        //   parkingCharges = obj.ParkingCharges.ToDecimal();

                        if (obj.Gen_Company.PreferredEmails.ToBool())
                        {
                            To = obj.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();
                            mainCustomerName = obj.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                        }
                    }


                    decimal total = 0.00m;


                    total = fares;
                  //  decimal total = fares;

                    sb.Append("<html>");
                    sb.Append("<body>");
                    sb.Append("<table style=\"width:100%;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"0\" >");
                    sb.Append("<tr>");

                    sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                    sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
                             "with Driver directly as it is illegal and not covered by insurance.");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    //
                    sb.Append("<tr>");
                    sb.Append("<td  >");
                  //  sb.Append("");
                    sb.Append("Dear "+obj.CustomerName+",");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                //
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("The quoted price is for a "+obj.Fleet_VehicleType.VehicleType);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    string JourneyType = string.Empty;
                    if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY)
                    {
                        JourneyType = "One Way";
                    }
                    else if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                    {
                        JourneyType = "Return Journey";
                    }
                    else if(obj.JourneyTypeId.ToInt()==Enums.JOURNEY_TYPES.WAITANDRETURN)
                    {
                        JourneyType = "Wait and Return";
                    }
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("Journey :" + JourneyType);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Fare : £" + string.Format("{0:f2}", fares));
                    //"£" + string.Format("{0:f2}", total)
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    decimal extraPickup = 0.00m;
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.FromLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == obj.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }


                    total += extraPickup;
                 

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Airport Pickup :"+extraPickup );
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    extraPickup = 0.00m;

                    if (obj.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.ToLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == obj.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }
                    total += extraPickup;


                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Airport DropOff :" + extraPickup);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Total : £" + string.Format("{0:f2}", total));
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("If you want us to Proceed with the booking,");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Please reply with these details,");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                   // sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Number of Passenger:"+(obj.NoofPassengers.ToInt()>0?obj.NoofLuggages.ToStr():""));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Number of Suitcase:");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                   // sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Number of Hand luggage:" + (obj.NoofHandLuggages.ToInt() > 0 ? obj.NoofHandLuggages.ToStr() : ""));
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Passenger Name:"+obj.CustomerName);
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Mobile Number:"+obj.CustomerMobileNo);
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Alt Number :"+obj.CustomerPhoneNo);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                  //  sb.Append("Pickup Date :"+string.Format("{0:dd MMMM yyyy}", obj.PickupDateTime));
                     sb.Append("Pickup Date :");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Flight Landing time:");
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                  //  sb.Append("Pickup Time after landing:" + string.Format("{0:HH:mm}", obj.PickupDateTime));
                      sb.Append("Pickup Time after landing:");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Display name :");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Flight Number:"+obj.FromDoorNo);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Coming From:"+obj.FromStreet);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                   // sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Airline Name:");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                 
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("If return booking required please provide :");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Return Pickup Date:"+string.Format("{0:dd MMMM yyyy}", obj.ReturnPickupDateTime));
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                   // sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                   // sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Return Pickup Time:" + string.Format("{0:HH:mm}", obj.ReturnPickupDateTime));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    //sb.Append("&nbsp&nbsp&nbsp");
                    sb.Append("Return Flight Number (if coming by flight):");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                   // sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Return Flight Departure time if Departing from London:");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    //sb.Append("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp");
                    sb.Append("Return Pick up Address if different from DropOff: ");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                  
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("Payment in Advance: AT NO EXTRA CHARGE.  You can pay in advance online by link or phone (Master/Visa/PayPal/American Express). We accept American Express only by paypal link ) There is no extra Surcharge paying in Advance.");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* Payment to Driver: You can pay by Cash or Card to driver(Master/Visa/Credit/Debit Card) Paying to the driver by Card will cost you 5% extra charge.");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* We provide 45 Free minutes waiting time after the Requested time of pickup at Airport.");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                   

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* Out of London all pickup(s) will be paid in Advance");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                   
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* We provide FREE MEET & GREET Service to all our customers, the Driver will park their Car & meet you");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("inside the terminal with your Name Board, there is parking Charge apply, which is £6 for");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("all airports apart from Luton & City airport(s) where pickup charge is £7 to cover up to 40 minutes of parking.");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* Our driver will welcome you at arrival with your Name Board If you have any difficulty to finding driver, please call our control office on 0044"+AppVars.objSubCompany.TelephoneNo.ToStr()+" / 0044 "+AppVars.objSubCompany.TelephoneNo.ToStr()+" or send us email: "+AppVars.objSubCompany.EmailAddress.ToStr().Trim()+" .");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* No Extra / Hidden Charges everything included in the quote.");
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* If parking charges goes up by the money you have paid then customer(s) have to pay the difference.");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("* To see the term and Conditions please check");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //sb.Append("");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append(AppVars.objSubCompany.WebsiteUrl.ToStr()+"/terms-conditions");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                   // sb.Append("");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    // sb.Append("");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("If you have any query please do not hesitate to contact us.");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    //
                    //sb.Append("/<table>");
                    //sb.Append("/<body>");
                    //sb.Append("/<html>");
                    Body = sb.ToString();
                

            }
            catch (Exception ex)
            {




            }
        }





        public static void SendBookingCompletionEmail(Booking obj)
        {


            EmailCompletion(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                Subject = "EMAIL - INVOICE";
                SendEmail();
            }

        }


        public static void EmailCompletion(Booking obj)
        {

            try
            {
                Subject = "Invoice Booking Ref - " + obj.BookingNo + " - " +string.Format("{0:dd MMMM yyyy}", obj.PickupDateTime) + ", TIME " + string.Format("{0:HH.mm}", obj.PickupDateTime) ;
                To = obj.CustomerEmail.ToStr().Trim();

                BookingBO objBooking = new BookingBO();
                objBooking.GetByPrimaryKey(obj.Id);


                string bb = objBooking.Current.BookingNo;

                var objSubcompany = obj.Gen_SubCompany.DefaultIfEmpty();

                StringBuilder sb = new StringBuilder();


                decimal fares = obj.FareRate.ToDecimal();
             //   decimal parkingCharges = obj.CongtionCharges.ToDecimal();
                string mainCustomerName = obj.CustomerName.ToStr().Trim();

                decimal extraPickup = 0.00m;
                decimal extraDropOff = 0.00m;

                if (obj.CompanyId != null && obj.Gen_Company.SysGenId.ToInt()!=2)
                {
                  //  fares = obj.CompanyPrice.ToDecimal();
                //    parkingCharges = obj.ParkingCharges.ToDecimal();
                    if (obj.Gen_Company.PreferredEmails.ToBool())
                    {
                        mainCustomerName = obj.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                        To = obj.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();
                    }
                }


                try
                {

                    sb.Append("<html>");
                    sb.Append("<head>");

                    sb.Append("</head>");
                    sb.Append("<body>");
                    sb.Append("<table style=\"width:850px;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"1\">");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                    sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr() + "' />");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td colspan=\"2\">");
                    sb.Append("If you wish to book a return or onward journey please always call office.Don't book with Driver directly as it is illegal and not covered by insurance.");

                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" align='center' colspan=\"2\">");
                    sb.Append("<b>Invoice</b>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                    sb.Append("Hi " + mainCustomerName.ToUpper()); //customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("Thank you for booking with ");
                    //sb.Append(objSubcompany.CompanyName.ToStr()); //maincompanyname
                    //sb.Append(", please find your booking confirmation and booking details below. If you have any questions in relation to your booking, please call us on ");
                    //sb.Append(objSubcompany.TelephoneNo.ToStr() + "."); // maincompanyphoneno
                    //sb.Append("</td>");
                    //sb.Append("</tr>");




                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append("Booking Reference");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append(obj.BookingNo.ToStr());//bookingno
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Date");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", obj.PickupDateTime));//pickupdatetime
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Passenger");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.CustomerName.ToStr());//customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Phone");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.CustomerMobileNo.ToStr().Length > 0 ? obj.CustomerMobileNo.ToStr() : obj.CustomerPhoneNo.ToStr()));//customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Car Type");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim()));
                    sb.Append("</td>");
                    sb.Append("</tr>");




                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Pickup");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.FromAddress.ToStr());//fromaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Destination");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.ToAddress.ToStr());//toaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    if (obj.ViaString.ToStr().Length > 0)
                    {


                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Via");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(obj.ViaString.ToStr());//toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Price");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", fares) );
                    sb.Append("</td>");
                    sb.Append("</tr>");

                  //  sb.Append("<tr>");     
                
                    
                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Parking");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", parkingCharges));//parkingcharges
                    //sb.Append("</td>");
                    //sb.Append("</tr>");


                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.FromLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == obj.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }
               
          
                    
                    
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Airport Pickup");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(string.Format("{0:f2}",extraPickup));//tip price
                        sb.Append("</td>");
                        sb.Append("</tr>");


                        if (obj.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.ToLocId != null)
                        {

                            extraDropOff = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == obj.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
                        }


                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Airport Dropoff");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(string.Format("{0:f2}", extraDropOff));//tip price
                        sb.Append("</td>");
                        sb.Append("</tr>");


                        decimal faresForServiceCharge = fares + extraPickup + extraDropOff;


                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Sub Total");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        //sb.Append("£" + string.Format("{0:f2}", faresForServiceCharge));//subtotal
                        //sb.Append("</td>");
                        //sb.Append("</tr>");





                        decimal surchargeRate = 0.00m;

                        if (obj.PaymentTypeId.ToInt()==Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() >0)
                        {
                            surchargeRate = (fares * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;


                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Surcharge (CC)");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            sb.Append(string.Format("{0:f2}", surchargeRate));//Service Charge
                            sb.Append("</td>");
                            sb.Append("</tr>");

                        }



                            //Gen_ServiceCharge objServiceCharge =  General.GetObject<Gen_ServiceCharge>(c => faresForServiceCharge>= c.FromValue && faresForServiceCharge <= c.ToValue);

                        decimal serviceCharges = obj.ServiceCharges.ToDecimal();
                     
                            //if (objServiceCharge != null)
                            //{
                            //    serviceCharges = ((faresForServiceCharge * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100).ToDecimal();                             
                            //}
                        
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Service Charge");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            sb.Append(string.Format("{0:f2}", obj.ServiceCharges.ToDecimal()));//Service Charge
                            sb.Append("</td>");
                            sb.Append("</tr>");

                       
                    

                        //extraDropOff = obj.ExtraDropCharges.ToDecimal();

                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Extra DropOff");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        //sb.Append(string.Format("{0:f2}", extraDropOff));//tip price
                        //sb.Append("</td>");
                        //sb.Append("</tr>");                  


                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Tip");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£0.00");//tip price



                 //   sb.Append("</td>");
                  //  sb.Append("</tr>");
                   
                    
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                  
                    sb.Append("<tr style=\"font-weight:bold;\">");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Total");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", (fares  + extraPickup + extraDropOff +surchargeRate + serviceCharges)+ " (" + obj.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")")); //total
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("We operate 24 hours, 7 days a week.");
                    sb.Append("<br />");
                    sb.Append("Our contact details are as follows:");
                    sb.Append("<br />");
                   
                    //sb.Append("From UK: " + objSubcompany.TelephoneNo.ToStr() + " OR " + objSubcompany.EmergencyNo.ToStr());
                    //sb.Append("<br />");
                    //sb.Append("From abroad: " + (objSubcompany.EmergencyNo.ToStr().Trim().Length > 0 && objSubcompany.EmergencyNo.StartsWith("0") ? "0044 " + objSubcompany.EmergencyNo.ToStr().Substring(1) : objSubcompany.EmergencyNo));

                    sb.Append("Outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());
                    
                    sb.Append("<br />");//companytelephoneno
                    sb.Append("<span style=\"color:#0563c1\">");
                    sb.Append(objSubcompany.WebsiteUrl.ToStr());//maincompanywebsites
                    sb.Append("</span>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</body>");
                    sb.Append("</html>");

                }
                catch (Exception ex)
                {
                }


                Body = sb.ToString();              

            }
            catch (Exception ex)
            {




            }

        }



        public static void SendPaymentLaterConfirmationEmail(Booking obj)
        {

            EmailPPC(obj);
            SendEmail();


            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();

            Subject = "EMAIL - APC";
            SendEmail();



            EmailPPI(obj);
            SendEmail();


            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();

            Subject = "EMAIL - API";
            SendEmail();


        }

        public static void CustomerCancelationEmail(Booking obj)
        {

            try
            {



                Subject = "BOOKING CANCELLED - " + string.Format("{0:dd MMMM yyyy}", obj.PickupDateTime) + ", TIME " + string.Format("{0:HH.mm}", obj.PickupDateTime) + " - " + obj.BookingNo.ToStr();


                To = obj.CustomerEmail.ToStr().Trim();

                //if (obj.CompanyId!=null )
                //{
                //    if (obj.Gen_Company.PreferredEmails.ToBool()==true)
                //    {
                //        To = obj.Gen_Company.Email.ToStr().Trim();
                //    }
                //}

                BookingBO objBooking = new BookingBO();
                objBooking.GetByPrimaryKey(obj.Id);


                string bb = objBooking.Current.BookingNo;

                var objSubcompany = obj.Gen_SubCompany.DefaultIfEmpty();

                string mainCustomerName = obj.CustomerName.ToStr();
                StringBuilder sb = new StringBuilder();
                //if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY || obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.WAITANDRETURN)
                //{

                    decimal fares = obj.CustomerPrice.ToDecimal();
                    //  decimal parkingCharges = obj.CongtionCharges.ToDecimal();
                    if (obj.CompanyId != null)
                    {
                       
                        //   parkingCharges = obj.ParkingCharges.ToDecimal();

                        if (obj.Gen_Company.PreferredEmails.ToBool())
                        {
                            To = obj.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();
                            mainCustomerName = obj.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                            fares = obj.CompanyPrice.ToDecimal();
                        }
                    }


                    decimal total = 0.00m;


                    total = fares;


                    sb.Append("<html>");
                    sb.Append("<body>");
                    sb.Append("<table style=\"width:850px;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"1\">");
                    sb.Append("<tr>");

                    sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                    sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr>");
                    ////sb.Append("<td colspan=\"2\">");
                    ////sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
                    ////         "with Driver directly as it is illegal and not covered by insurance.");
                    ////sb.Append("</td>");
                    //sb.Append("</tr>");
                    sb.Append("<tr>");


                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                    sb.Append("Hi " + mainCustomerName.ToStr().ToUpper());// customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("Your Booking has been cancelled at " + string.Format("{0:ddd dd MMM yyyy HH:mm}", obj.PickupDateTime)); 
                    
                    //   "outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());
                    //sb.Append("Thank you for booking with ");
                    //sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname

                    //sb.Append(", please find your booking confirmation " +
                    //        "below. If you have any questions in relation to your booking, please call us on " +
                    //    //    objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno

                    //   "outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());



                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append("Booking Reference");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append(obj.BookingNo.ToStr());  //bookingno
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Date");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", obj.PickupDateTime));//pickupdatetime
                    sb.Append("</td>");



                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Passenger");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.CustomerName.ToStr().Trim());  //customername
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Phone");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.CustomerMobileNo.ToStr().Length > 0 ? obj.CustomerMobileNo.ToStr() : obj.CustomerPhoneNo.ToStr())); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Car Type");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Pickup");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.FromAddress.ToStr().Trim()); //fromaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Destination");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.ToAddress.ToStr().Trim()); //toaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    if (obj.ViaString.ToStr().Length > 0)
                    {

                        //
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Via");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(obj.ViaString.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }

                    //


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Fares");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", fares) + " (" + obj.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("We operate 24 hours, 7 days a week.");
                    sb.Append("<br />");
                    sb.Append("Our contact details are as follows:");
                    sb.Append("<br />");
                   
                    //sb.Append("From UK: " + objSubcompany.TelephoneNo.ToStr() + " OR " + objSubcompany.EmergencyNo.ToStr());
                    //sb.Append("<br />");
                    //sb.Append("From abroad: " + (objSubcompany.EmergencyNo.ToStr().Trim().Length > 0 && objSubcompany.EmergencyNo.StartsWith("0") ? "0044 " + objSubcompany.EmergencyNo.ToStr().Substring(1) : objSubcompany.EmergencyNo));

                    sb.Append("Outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());
                    
                    sb.Append("<br />");//companytelephoneno
                    sb.Append("<span style=\"color:#0563c1\">");
                    sb.Append(objSubcompany.WebsiteUrl.ToStr());//maincompanywebsites
                    sb.Append("</span>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</body>");
                    sb.Append("</html>");
                    //Footer

                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("We operate 24 hours, 7 days a week.");
                    //sb.Append("<br />");
                    //sb.Append("Our contact details are as follows:");
                    //sb.Append("<br />");

                    ////sb.Append("From UK: " + objSubcompany.TelephoneNo.ToStr() + " OR " + objSubcompany.EmergencyNo.ToStr());
                    ////sb.Append("<br />");
                    ////sb.Append("From abroad: " + (objSubcompany.EmergencyNo.ToStr().Trim().Length > 0 && objSubcompany.EmergencyNo.StartsWith("0") ? "0044 " + objSubcompany.EmergencyNo.ToStr().Substring(1) : objSubcompany.EmergencyNo));

                    //sb.Append("Outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());

                    //sb.Append("<br />");//companytelephoneno
                    //sb.Append("<span style=\"color:#0563c1\">");
                    //sb.Append(objSubcompany.WebsiteUrl.ToStr());//maincompanywebsites
                    //sb.Append("</span>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("</table>");
                    //sb.Append("</body>");
                    //sb.Append("</html>");

                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Parking Charges");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£"+string.Format("{0:f2}", parkingCharges));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");



                    //decimal extraPickup = 0.00m;
                    //if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.FromLocId != null)
                    //{

                    //    extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == obj.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
                    //}


                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Airport Pickup");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");



                    //total += extraPickup;


                    //extraPickup = 0.00m;

                    //if (obj.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.ToLocId != null)
                    //{

                    //    extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == obj.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
                    //}


                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Airport Dropoff");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");


                    ////sb.Append("<tr>");
                    ////sb.Append("<td colspan=\"2\">");
                    ////sb.Append("&nbsp;");
                    ////sb.Append("</td>");


                    //total += extraPickup;


                    //sb.Append("<tr style=\"font-weight:bold;\">");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Total");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", total));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    ////sb.Append("<tr>");
                    ////sb.Append("<td colspan=\"2\">");
                    ////sb.Append("&nbsp;");
                    ////sb.Append("</td>");



                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Special Requirements");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append(obj.SpecialRequirements.ToStr().Trim());
                    //sb.Append("</td>");
                    //sb.Append("</tr>");


                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");

                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Extra Drop off");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", obj.ExtraDropCharges.ToDecimal()));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");






                    Body = sb.ToString();
            //   }
              //  else if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && obj.BookingReturns.Count > 0)
              //  {


              //      Booking objReturn = obj.BookingReturns[0];


              //      // Subject += " (Return Journey)";

              //      decimal fares = obj.CustomerPrice.ToDecimal();
              //      //   decimal parkingCharges = obj.CongtionCharges.ToDecimal();
              //      if (obj.CompanyId != null)
              //      {
              //          fares = obj.CompanyPrice.ToDecimal();
              //          //   parkingCharges = obj.ParkingCharges.ToDecimal();
              //      }



              //      decimal total = fares;

              //      sb.Append("<html>");
              //      sb.Append("<body>");
              //      sb.Append("<table style=\"width:100%;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"1\">");
              //      sb.Append("<tr>");

              //      sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
              //      sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td colspan=\"2\">");
              //      sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
              //               "with Driver directly as it is illegal and not covered by insurance.");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");


              //      sb.Append("<td colspan=\"2\">");
              //      sb.Append("&nbsp;");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
              //      sb.Append("Hi " + mainCustomerName.ToStr().ToUpper());// customername
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td colspan=\"2\">");
              //      sb.Append("Thank you for booking with ");
              //      sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname

              //      sb.Append(", please find your booking confirmation " +
              //              "below. If you have any questions in relation to your booking, please call us on " +
              //          //      objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno
              //"outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());


              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td colspan=\"2\">");
              //      sb.Append("&nbsp;");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
              //      sb.Append("Booking Reference");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
              //      sb.Append(obj.BookingNo.ToStr());  //bookingno
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Date");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", obj.PickupDateTime));//pickupdatetime
              //      sb.Append("</td>");



              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Passenger");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(obj.CustomerName.ToStr().Trim());  //customername
              //      sb.Append("</td>");
              //      sb.Append("</tr>");

              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Phone");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append((obj.CustomerMobileNo.ToStr().Length > 0 ? obj.CustomerMobileNo.ToStr() : obj.CustomerPhoneNo.ToStr())); //customerphoneno
              //      sb.Append("</td>");
              //      sb.Append("</tr>");




              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Car Type");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append((obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim()));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");




              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("From");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(obj.FromAddress.ToStr().Trim()); //fromaddress
              //      sb.Append("</td>");
              //      sb.Append("</tr>");



              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Destination");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(obj.ToAddress.ToStr().Trim()); //toaddress
              //      sb.Append("</td>");
              //      sb.Append("</tr>");

              //      if (obj.ViaString.ToStr().Length > 0)
              //      {

              //          //
              //          sb.Append("<tr>");
              //          sb.Append("<td style=\"width:30%;\">");
              //          sb.Append("Via");
              //          sb.Append("</td>");
              //          sb.Append("<td style=\"width:70%;\">");
              //          sb.Append(obj.ViaString.ToStr().Trim()); //toaddress
              //          sb.Append("</td>");
              //          sb.Append("</tr>");

              //      }







              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Fares");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", fares) + " (" + obj.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");


              //      //sb.Append("<tr>");
              //      //sb.Append("<td style=\"width:30%;\">");
              //      //sb.Append("Parking Charges");
              //      //sb.Append("</td>");
              //      //sb.Append("<td style=\"width:70%;\">");
              //      //sb.Append("£" + string.Format("{0:f2}", parkingCharges));
              //      //sb.Append("</td>");
              //      //sb.Append("</tr>");

              //      decimal extraPickup = 0.00m;

              //      if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.FromLocId != null)
              //      {

              //          extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == obj.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
              //      }

              //      total += extraPickup;

              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Airport Pickup");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", extraPickup));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");


              //      extraPickup = 0.00m;

              //      if (obj.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.ToLocId != null)
              //      {

              //          extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == obj.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
              //      }


              //      total += extraPickup;

              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Airport Dropoff");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", extraPickup));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");

              //      //sb.Append("<tr>");
              //      //sb.Append("<td colspan=\"2\">");
              //      //sb.Append("&nbsp;");
              //      //sb.Append("</td>");



              //      sb.Append("<tr style=\"font-weight:bold;\">");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Total");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", total));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");


              //      //sb.Append("<tr>");
              //      //sb.Append("<td colspan=\"2\">");
              //      //sb.Append("&nbsp;");
              //      //sb.Append("</td>");



              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Special Requirements");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(obj.SpecialRequirements.ToStr().Trim());
              //      sb.Append("</td>");
              //      sb.Append("</tr>");


              //      //sb.Append("<tr>");
              //      //sb.Append("<td colspan=\"2\">");
              //      //sb.Append("&nbsp;");
              //      //sb.Append("</td>");



              //      sb.Append("<tr>");
              //      sb.Append("<td colspan=\"2\">");
              //      sb.Append("&nbsp;");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");


              //      // Return Details

              //      fares = objReturn.FareRate.ToDecimal();
              //      //  parkingCharges = objReturn.CongtionCharges.ToDecimal();
              //      if (objReturn.CompanyId != null)
              //      {
              //          fares = objReturn.CompanyPrice.ToDecimal();
              //          //  parkingCharges = objReturn.ParkingCharges.ToDecimal();
              //      }


              //      total = fares;

              //      sb.Append("<tr>");
              //      sb.Append("<td colspan=\"2\">");
              //      sb.Append("Return Booking Details :");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
              //      sb.Append("Booking Reference");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
              //      sb.Append(objReturn.BookingNo.ToStr());  //bookingno
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Date");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objReturn.PickupDateTime));//pickupdatetime
              //      sb.Append("</td>");


              //      sb.Append("</tr>");

              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Passenger");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(objReturn.CustomerName.ToStr().Trim());  //customername
              //      sb.Append("</td>");
              //      sb.Append("</tr>");

              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Phone");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append((objReturn.CustomerMobileNo.ToStr().Length > 0 ? objReturn.CustomerMobileNo.ToStr() : objReturn.CustomerPhoneNo.ToStr())); //customerphoneno
              //      sb.Append("</td>");
              //      sb.Append("</tr>");



              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Car Type");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append((objReturn.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim()));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");





              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Pickup");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(objReturn.FromAddress.ToStr().Trim()); //fromaddress
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Destination");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(objReturn.ToAddress.ToStr().Trim()); //toaddress
              //      sb.Append("</td>");
              //      sb.Append("</tr>");



              //      if (objReturn.ViaString.ToStr().Length > 0)
              //      {

              //          //
              //          sb.Append("<tr>");
              //          sb.Append("<td style=\"width:30%;\">");
              //          sb.Append("Via");
              //          sb.Append("</td>");
              //          sb.Append("<td style=\"width:70%;\">");
              //          sb.Append(objReturn.ViaString.ToStr().Trim()); //toaddress
              //          sb.Append("</td>");
              //          sb.Append("</tr>");

              //      }


              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Fares");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", fares) + " (" + objReturn.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");



              //      //sb.Append("<tr>");
              //      //sb.Append("<td style=\"width:30%;\">");
              //      //sb.Append("Parking Charges");
              //      //sb.Append("</td>");
              //      //sb.Append("<td style=\"width:70%;\">");
              //      //sb.Append("£" + string.Format("{0:f2}", parkingCharges));
              //      //sb.Append("</td>");
              //      //sb.Append("</tr>");




              //      extraPickup = 0.00m;

              //      if (objReturn.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objReturn.FromLocId != null)
              //      {

              //          extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == objReturn.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
              //      }


              //      total += extraPickup;


              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Airport Pickup");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", extraPickup));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");



              //      extraPickup = 0.00m;

              //      if (objReturn.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objReturn.ToLocId != null)
              //      {

              //          extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == objReturn.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
              //      }


              //      total += extraPickup;


              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Airport Dropoff");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", extraPickup));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      //sb.Append("<tr>");
              //      //sb.Append("<td colspan=\"2\">");
              //      //sb.Append("&nbsp;");
              //      //sb.Append("</td>");




              //      sb.Append("<tr style=\"font-weight:bold;\">");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Total");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append("£" + string.Format("{0:f2}", total));
              //      sb.Append("</td>");
              //      sb.Append("</tr>");
              //      //sb.Append("<tr>");
              //      //sb.Append("<td colspan=\"2\">");
              //      //sb.Append("&nbsp;");
              //      //sb.Append("</td>");



              //      sb.Append("<tr>");
              //      sb.Append("<td style=\"width:30%;\">");
              //      sb.Append("Special Requirements");
              //      sb.Append("</td>");
              //      sb.Append("<td style=\"width:70%;\">");
              //      sb.Append(objReturn.SpecialRequirements.ToStr().Trim());
              //      sb.Append("</td>");
              //      sb.Append("</tr>");

              //      sb.Append("<tr>");
              //      sb.Append("<td colspan=\"2\">");
              //      sb.Append("&nbsp;");
              //      sb.Append("</td>");
              //      sb.Append("</tr>");


              //      Body = sb.ToString();
              //  }

            }
            catch (Exception ex)
            {




            }
        }

        public static void SendCustomerCancelationEmail(Booking obj)
        {
            CustomerCancelationEmail(obj);
            SendEmail();


            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                //Subject = "Your Booking is canceled";
                SendEmail();
            }



        }
        public static void SendDriverConfirmationEmail(Booking obj)
        {
            EmailPDD(obj);
            SendEmail();

            To = AppVars.objSubCompany.EmailCC.ToStr().Trim();


            if (!string.IsNullOrEmpty(To))
            {

                Subject = "EMAIL - ADD";
                SendEmail();


            }

        }


        public static void SendEmail()
        {


            if (To.ToStr().Trim().Length == 0)
                return;

            string smtpHost = string.Empty;
            string userName = string.Empty;
            string pwd = string.Empty;
            string port = string.Empty;
            bool enableSSL = false;


            string ccEmail = string.Empty;

            Gen_SubCompany objSubCompany = AppVars.objSubCompany;

            if (objSubCompany != null && !string.IsNullOrEmpty(objSubCompany.SmtpHost.ToStr().Trim()))
            {
                smtpHost = objSubCompany.SmtpHost.ToStr().Trim();
                userName = objSubCompany.SmtpUserName.ToStr().Trim();
                pwd = objSubCompany.SmtpPassword.ToStr().Trim();
                port = objSubCompany.SmtpPort.ToStr().Trim();
                enableSSL = objSubCompany.SmtpHasSSL.ToBool();


                ccEmail = objSubCompany.EmailCC.ToStr().Trim();


            }
            else
            {
                smtpHost = AppVars.objPolicyConfiguration.SmtpHost.ToStr().Trim();
                userName = AppVars.objPolicyConfiguration.UserName.ToStr().Trim();
                pwd = AppVars.objPolicyConfiguration.Password.ToStr().Trim();
                port = AppVars.objPolicyConfiguration.Port.ToStr().Trim();
                enableSSL = AppVars.objPolicyConfiguration.EnableSSL.ToBool();

                if (AppVars.objSubCompany.EmailCC.ToStr().Trim() != string.Empty)
                    ccEmail = AppVars.objSubCompany.EmailCC.ToStr().Trim();

            }

            From = userName;

            try
            {
                
                using (System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
                {
                    msg.To.Add(To);
                    msg.From = new System.Net.Mail.MailAddress(From, AppVars.objSubCompany.CompanyName.ToStr());
                    msg.Subject = Subject;


                    msg.Body = Body;


                    msg.BodyEncoding = System.Text.Encoding.UTF8;
                    msg.IsBodyHtml = true;

                    //System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                    //(System.Text.RegularExpressions.Regex.Replace(strBody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                    //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(strBody, null, "text/html");

                    //msg.AlternateViews.Add(plainView);
                    //msg.AlternateViews.Add(htmlView);



                    //    msg.Priority = System.Net.Mail.MailPriority.High;
                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    client.Credentials = new NetworkCredential(userName, pwd);
                    client.Port = Convert.ToInt32(port);
                    client.Host = smtpHost;
                    client.EnableSsl = enableSSL;

                    FieldInfo transport = client.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
                    FieldInfo authModules = transport.GetValue(client).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);

                    Array modulesArray = authModules.GetValue(transport.GetValue(client)) as Array;
                    modulesArray.SetValue(modulesArray.GetValue(3), 1);


                  
                        ServicePointManager.ServerCertificateValidationCallback =
                            delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                     System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                            { return true; };
                    
                    client.Send(msg);

                }
            }
            catch (Exception ex)
            {


            }



            //RadDesktopAlert desktopAlert = new Telerik.WinControls.UI.RadDesktopAlert();
            //desktopAlert.CaptionText = "Your email has been sent";
            //desktopAlert.ContentText = subject;
            //desktopAlert.ContentImage = Resources.Resource1.message;
            //desktopAlert.SoundToPlay = System.Media.SystemSounds.Asterisk;
            //desktopAlert.PlaySound = true;
            //desktopAlert.FixedSize = new Size(300, 120);
            //desktopAlert.Show();


        }


        public static void EmailConfirmation(Booking obj)
        {

            try
            {
              
              //  Subject = "Confirmation Email - " + obj.BookingNo;

                Subject = "BOOKING CONFIRMATION - " + string.Format("{0:dd MMMM yyyy}", obj.PickupDateTime) + ", TIME " + string.Format("{0:HH.mm}", obj.PickupDateTime) + " - " + obj.BookingNo.ToStr() +" ("+ string.Format("{0:dd MMMM yyyy}", obj.BookingDate) + ", TIME " + string.Format("{0:HH.mm}",obj.BookingDate)+")";
              

                To = obj.CustomerEmail.ToStr().Trim();

                BookingBO objBooking = new BookingBO();
                objBooking.GetByPrimaryKey(obj.Id);


                string bb = objBooking.Current.BookingNo;

                var objSubcompany = obj.Gen_SubCompany.DefaultIfEmpty();

                string mainCustomerName = obj.CustomerName.ToStr();
                StringBuilder sb = new StringBuilder();
                if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY || obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.WAITANDRETURN)
                {

                    decimal fares = obj.FareRate.ToDecimal();
                  //  decimal parkingCharges = obj.CongtionCharges.ToDecimal();
                    if (obj.CompanyId != null && obj.Gen_Company.SysGenId.ToInt() != 2)
                    {
                     //   fares = obj.CompanyPrice.ToDecimal();
                     //   parkingCharges = obj.ParkingCharges.ToDecimal();

                        if (obj.Gen_Company.PreferredEmails.ToBool())
                        {
                            To = obj.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();
                            mainCustomerName = obj.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                        }
                    }


                    decimal total = 0.00m;
                    total = fares;


                    sb.Append("<html>");
                    sb.Append("<body>");
                    sb.Append("<table style=\"width:850px;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"1\">");
                    sb.Append("<tr>");

                    sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                    sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
                             "with Driver directly as it is illegal and not covered by insurance.");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");


                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                    sb.Append("Hi " + mainCustomerName.ToStr().ToUpper());// customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("Thank you for booking with ");
                    sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname

                    sb.Append(", please find your booking confirmation " +
                            "below. If you have any questions in relation to your booking, please call us on " +
                        //    objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno

                       "outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());



                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append("Booking Reference");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append(obj.BookingNo.ToStr());  //bookingno
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Pickup Date");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", obj.PickupDateTime));//pickupdatetime
                    sb.Append("</td>");



                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Passenger");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.CustomerName.ToStr().Trim());  //customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Phone");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.CustomerMobileNo.ToStr().Length > 0 ? obj.CustomerMobileNo.ToStr() : obj.CustomerPhoneNo.ToStr())); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Car Type");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + obj.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + obj.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , "+obj.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Pickup");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.FromDoorNo.ToStr().Trim().Length > 0 ? obj.FromDoorNo.ToStr() + "," + obj.FromAddress.ToStr().Trim() : obj.FromAddress.ToStr().Trim()); //fromaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");


                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Destination");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.ToAddress.ToStr().Trim()); //toaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    if (obj.ViaString.ToStr().Length > 0)
                    {

                        //
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Via");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(obj.ViaString.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }

                    //


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Fares");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", fares) );
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Parking Charges");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£"+string.Format("{0:f2}", parkingCharges));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");



                    decimal extraPickup = 0.00m;
                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.FromLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == obj.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Airport Pickup");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    total += extraPickup;


                    extraPickup = 0.00m;

                    if (obj.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.ToLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == obj.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Airport Dropoff");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");


                    total += extraPickup;


                   decimal  surchargeRate = 0.00m;

                    if (obj.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                    {
                        surchargeRate = (fares * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;


                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Surcharge (CC)");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(string.Format("{0:f2}", surchargeRate));//Service Charge
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }

                    total += surchargeRate;


                    //Gen_ServiceCharge objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => faresForServiceCharge >= c.FromValue && faresForServiceCharge <= c.ToValue);

                    //decimal serviceCharges = 0.00m;

                    //if (objServiceCharge != null)
                    //{
                    //    serviceCharges = ((faresForServiceCharge * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100).ToDecimal();
                    //}

                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Service Charge");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(string.Format("{0:f2}", obj.ServiceCharges.ToDecimal()));//Service Charge
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    total += obj.ServiceCharges.ToDecimal();



                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");

                    sb.Append("<tr style=\"font-weight:bold;\">");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Total");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", (total)+ " (" + obj.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")")); //total
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    //decimal serviceCharge = 0.00m;

                    //if (obj.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() >0)
                    //{


                    //    serviceCharge = (fares * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;
                    //    sb.Append("<tr>");
                    //    sb.Append("<td style=\"width:30%;\">");
                    //    sb.Append("Service Charge");
                    //    sb.Append("</td>");
                    //    sb.Append("<td style=\"width:70%;\">");
                    //    sb.Append(string.Format("{0:f2}", serviceCharge));//tip price
                    //    sb.Append("</td>");
                    //    sb.Append("</tr>");

                    //}

                    //total += serviceCharge;



                    //sb.Append("<tr style=\"font-weight:bold;\">");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Total");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", total));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");

                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Special Requirements");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.SpecialRequirements.ToStr().Trim());
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Extra Drop off");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", obj.ExtraDropCharges.ToDecimal()));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");






                    Body = sb.ToString();
                }
                else if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && obj.BookingReturns.Count > 0)
                {
                   

                    Booking objReturn = obj.BookingReturns[0];


                   // Subject += " (Return Journey)";

                    decimal fares = obj.FareRate.ToDecimal();
                 //   decimal parkingCharges = obj.CongtionCharges.ToDecimal();
                    if (obj.CompanyId != null  && obj.Gen_Company.SysGenId.ToInt() != 2)
                    {
                   //     fares = obj.CompanyPrice.ToDecimal();
                     //   parkingCharges = obj.ParkingCharges.ToDecimal();


                        if (obj.Gen_Company.PreferredEmails.ToBool())
                        {
                            To = obj.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();
                            mainCustomerName = obj.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                        }
                    }



                    decimal total = fares;

                    sb.Append("<html>");
                    sb.Append("<body>");
                    sb.Append("<table style=\"width:100%;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"1\">");
                    sb.Append("<tr>");

                    sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                    sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
                             "with Driver directly as it is illegal and not covered by insurance.");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");


                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                    sb.Append("Hi "+mainCustomerName.ToStr().ToUpper());// customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("Thank you for booking with ");
                    sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname

                    sb.Append(", please find your booking confirmation " +
                            "below. If you have any questions in relation to your booking, please call us on " +
                      //      objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno
              "outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());

                      
                      sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append("Booking Reference");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append(obj.BookingNo.ToStr());  //bookingno
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Date");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", obj.PickupDateTime));//pickupdatetime
                    sb.Append("</td>");



                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Passenger");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.CustomerName.ToStr().Trim());  //customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Phone");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.CustomerMobileNo.ToStr().Length > 0 ? obj.CustomerMobileNo.ToStr() : obj.CustomerPhoneNo.ToStr())); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");
                  



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Car Type");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + obj.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + obj.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + obj.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");




                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("From");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    //    sb.Append(obj.FromAddress.ToStr().Trim()); //fromaddress
                    sb.Append(obj.FromDoorNo.ToStr().Trim().Length > 0 ? obj.FromDoorNo.ToStr() + "," + obj.FromAddress.ToStr().Trim() : obj.FromAddress.ToStr().Trim()); //fromaddress

                    sb.Append("</td>");
                    sb.Append("</tr>");
                 
                    
                    
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Destination");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.ToAddress.ToStr().Trim()); //toaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    if (obj.ViaString.ToStr().Length > 0)
                    {

                        //
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Via");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(obj.ViaString.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Fares");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", fares) );
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Parking Charges");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", parkingCharges));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");

                    decimal extraPickup = 0.00m;

                    if (obj.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.FromLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == obj.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }

                    total += extraPickup;

                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Airport Pickup");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    extraPickup = 0.00m;

                    if (obj.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && obj.ToLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == obj.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }


                    total += extraPickup;

                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Airport Dropoff");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");





                    decimal serviceCharge = 0.00m;

                    if (obj.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                    {


                        serviceCharge = (fares * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Service Charge");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(string.Format("{0:f2}", serviceCharge));//tip price
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }

                    total += serviceCharge;



                    sb.Append("<tr style=\"font-weight:bold;\">");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Total");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", total + " (" + obj.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")")); //total
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Special Requirements");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(obj.SpecialRequirements.ToStr().Trim());
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    
                    
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");



                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    // Return Details

                    fares = objReturn.FareRate.ToDecimal();
                  //  parkingCharges = objReturn.CongtionCharges.ToDecimal();
                    if (objReturn.CompanyId != null)
                    {
                       // fares = objReturn.CompanyPrice.ToDecimal();
                      //  parkingCharges = objReturn.ParkingCharges.ToDecimal();
                    }


                    total = fares;

                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("Return Booking Details :");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append("Booking Reference");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                    sb.Append(objReturn.BookingNo.ToStr());  //bookingno
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Pickup Date");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objReturn.PickupDateTime));//pickupdatetime
                    sb.Append("</td>");
                                      

                    sb.Append("</tr>");
                 
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Passenger");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(objReturn.CustomerName.ToStr().Trim());  //customername
                    sb.Append("</td>");
                    sb.Append("</tr>");
                   
                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Phone");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((objReturn.CustomerMobileNo.ToStr().Length > 0 ? objReturn.CustomerMobileNo.ToStr() : objReturn.CustomerPhoneNo.ToStr())); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Car Type");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append((objReturn.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + obj.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + obj.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + obj.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno
                    sb.Append("</td>");
                    sb.Append("</tr>");





                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Pickup");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                   // sb.Append(objReturn.FromAddress.ToStr().Trim()); //fromaddress
                    sb.Append(objReturn.FromDoorNo.ToStr().Trim().Length > 0 ? objReturn.FromDoorNo.ToStr() + "," + objReturn.FromAddress.ToStr().Trim() : objReturn.FromAddress.ToStr().Trim()); //fromaddress

                    sb.Append("</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Destination");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(objReturn.ToAddress.ToStr().Trim()); //toaddress
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    if (objReturn.ViaString.ToStr().Length > 0)
                    {

                        //
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Via");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objReturn.ViaString.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Fares");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", fares) );
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    //sb.Append("<tr>");
                    //sb.Append("<td style=\"width:30%;\">");
                    //sb.Append("Parking Charges");
                    //sb.Append("</td>");
                    //sb.Append("<td style=\"width:70%;\">");
                    //sb.Append("£" + string.Format("{0:f2}", parkingCharges));
                    //sb.Append("</td>");
                    //sb.Append("</tr>");




                    extraPickup = 0.00m;

                    if (objReturn.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objReturn.FromLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == objReturn.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }


                    total += extraPickup;


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Airport Pickup");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    sb.Append("</td>");
                    sb.Append("</tr>");



                    extraPickup = 0.00m;

                    if (objReturn.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objReturn.ToLocId != null)
                    {

                        extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == objReturn.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
                    }


                    total += extraPickup;


                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Airport Dropoff");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", extraPickup));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");



                    decimal surchargeRate = 0.00m;

                    if (objReturn.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                    {
                        surchargeRate = (fares * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;


                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Surcharge (CC)");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(string.Format("{0:f2}", surchargeRate));//Service Charge
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }
                    total += surchargeRate;


                    //Gen_ServiceCharge objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => faresForServiceCharge >= c.FromValue && faresForServiceCharge <= c.ToValue);

                    //decimal serviceCharges = 0.00m;

                    //if (objServiceCharge != null)
                    //{
                    //    serviceCharges = ((faresForServiceCharge * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100).ToDecimal();
                    //}

                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Service Charge");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(string.Format("{0:f2}", objReturn.ServiceCharges.ToDecimal()));//Service Charge
                    sb.Append("</td>");
                    sb.Append("</tr>");


             




                    sb.Append("<tr style=\"font-weight:bold;\">");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Total");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append("£" + string.Format("{0:f2}", total + " (" + objReturn.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")")); //total
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("&nbsp;");
                    //sb.Append("</td>");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"width:30%;\">");
                    sb.Append("Special Requirements");
                    sb.Append("</td>");
                    sb.Append("<td style=\"width:70%;\">");
                    sb.Append(objReturn.SpecialRequirements.ToStr().Trim());
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"2\">");
                    sb.Append("&nbsp;");
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    Body = sb.ToString();
                }

            }
            catch (Exception ex)
            {




            }
        }




    }
}
