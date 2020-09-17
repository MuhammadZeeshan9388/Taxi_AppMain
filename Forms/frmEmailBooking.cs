using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Utils;
using System.Net;
using Telerik.WinControls.UI;
using System.Reflection;
using Telerik.WinControls;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Taxi_AppMain
{
    public partial class frmEmailBooking : Forms.BaseForm
    {

    //   private  bool IsPickEmail = false;
        Booking objBooking;

        public bool SendEmailToBookedBy;

        private  string DefaultFromEmail;

        private Gen_SubCompany objSubCompany;

        public bool IsOpenedFromBooking = false;

        public frmEmailBooking(Booking objBook)
        {
            InitializeComponent();

            this.Load += new EventHandler(frmEmailBooking_Load);
            this.objBooking = objBook;
            this.objSubCompany = objBook.SubcompanyId != null ? objBook.Gen_SubCompany : AppVars.objSubCompany;
            this.FormClosed += new FormClosedEventHandler(frmEmailBooking_FormClosed);

            ddlEmailToType.SelectedIndexChanged += new EventHandler(ddlEmailToType_SelectedIndexChanged);

        }

        void ddlEmailToType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnPickEmail.Items[0].Visibility = ElementVisibility.Visible;
                btnPickEmail.Items[1].Visibility = ElementVisibility.Visible;
                btnPickEmail.Items[2].Visibility = ElementVisibility.Visible;

                if (ddlEmailToType.SelectedIndex == 0)
                {
                    txtTo.Text = objBooking.CustomerEmail.ToStr().Trim();

                    btnPickEmail.Items[1].Visibility = ElementVisibility.Collapsed;
                    btnPickEmail.Items[2].Visibility = ElementVisibility.Collapsed;
                }
                else if (ddlEmailToType.SelectedIndex == 1)
                {
                    txtTo.Text = objBooking.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();

                    if (objBooking.BookedBy.ToStr().Trim().Length > 0 && AppVars.listUserRights.Count(c => c.functionId == "SEND DIRECT CONFIRMATION EMAIL") > 0)
                    {
                        string bookedbyemail = objBooking.Gen_Company.DefaultIfEmpty().Gen_Company_BookedBies.FirstOrDefault(c => c.BookedBy.ToLower().Trim() == objBooking.BookedBy.ToStr().ToLower().Trim()).DefaultIfEmpty().EmailAddress.ToStr().Trim();



                        if (bookedbyemail.Length > 0)
                        {
                            txtTo.Text += "," + bookedbyemail;
                        }
                    }

                    btnPickEmail.Items[0].Visibility = ElementVisibility.Collapsed;
                    btnPickEmail.Items[1].Visibility = ElementVisibility.Collapsed;
                }
                else
                {
                    btnPickEmail.Items[0].Visibility = ElementVisibility.Collapsed;
                    btnPickEmail.Items[2].Visibility = ElementVisibility.Collapsed;


                }
            }
            catch (Exception ex)
            {


            }
        }

        void frmEmailBooking_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.Dispose(true);
            GC.Collect();
        }

        List<Gen_SubCompany> listofSubCompanies = null;

        void frmEmailBooking_Load(object sender, EventArgs e)
        {

            try
            {

                RadMenuItem item = null;
                item = new RadMenuItem();
                item.Text = "Customer";
                item.Click += new EventHandler(item_Click);
                btnPickEmail.Items.Add(item);

                item = new RadMenuItem();
                item.Text = "Driver";
                item.Click += new EventHandler(item_Click);
                btnPickEmail.Items.Add(item);

                item = new RadMenuItem();
                item.Text = "Company";
                item.Click += new EventHandler(item_Click);
                btnPickEmail.Items.Add(item);

                this.DefaultFromEmail = this.objSubCompany.EmailAddress.ToStr().Trim();

                txtSubject.Text = "BOOKING CONFIRMATION -  " + string.Format("{0:dd MMMM yyyy}", objBooking.PickupDateTime) + ", TIME " + string.Format("{0:HH.mm}", objBooking.PickupDateTime) + " - BOOKING ID " + objBooking.BookingNo.ToStr();



                listofSubCompanies = General.GetQueryable<Gen_SubCompany>(null).ToList();

                ddlFrom.DataSource = listofSubCompanies;
                ddlFrom.DisplayMember = "EmailAddress";
                ddlFrom.ValueMember = "Id";
                ddlFrom.SelectedIndex= ddlFrom.FindStringExact(this.DefaultFromEmail.ToStr());
                  
         

                if (ddlFrom.Items.Count==1)
                {
                    ddlFrom.Enabled = false;
                }

                obj = General.GetQueryable<UM_Form_Template>(c => c.TemplateValue == this.Name && c.IsDefault != null && c.IsDefault == true).FirstOrDefault();


                txtTo.Text = objBooking.CustomerEmail.ToStr();

              

                if (obj.TemplateName.ToStr() == "Template5" || obj.TemplateName.ToStr() == "Template6" || obj.TemplateName.ToStr() == "Template7" || obj.TemplateName.ToStr() == "Template8")
                {


                    if (obj.TemplateName.ToStr() == "Template5" && objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                    {

                        chkReturnDetails.Visible = true;

                    }
                    
                    ddlEmailToType.Visible = true;

                   if (objBooking.CompanyId == null || objBooking.Gen_Company.DefaultIfEmpty().PreferredEmails.ToBool()==false)
                   {
                       ddlEmailToType.SelectedIndex = 0;
                       txtTo.Text = objBooking.CustomerEmail.ToStr();
                   }
                   else
                   {
                       ddlEmailToType.SelectedIndex = 1;
                      // txtTo.Text = objBooking.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();
                   }


                   if (obj.TemplateName.ToStr() == "Template7")
                   {


                       txtSubject.Text = "BOOKING CONFIRMATION - " + string.Format("{0:dd MMMM yyyy}", objBooking.PickupDateTime) + ", TIME "
                           + string.Format("{0:HH.mm}", objBooking.PickupDateTime) + " - " + objBooking.BookingNo.ToStr() + " (" + string.Format("{0:dd MMMM yyyy}", objBooking.BookingDate) + ", TIME " + string.Format("{0:HH.mm}", objBooking.BookingDate) + ")";




                   }
                    
                }
            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        UM_Form_Template obj = null;

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            SetConfirmationEmail();
        }


        public void SetConfirmationEmail()
        {

            try
            {


                string template = "Template1";

                if (obj != null)
                {
                    template = obj.TemplateName.ToStr();
                }


                string from = ddlFrom.Text.ToStr().Trim();
                string to = txtTo.Text.Trim();

                if (string.IsNullOrEmpty(from))
                {
                    ENUtils.ShowMessage("Required : From email");
                    return;
                }

                if (string.IsNullOrEmpty(to))
                {
                    ENUtils.ShowMessage("Required : To email");
                    return;
                }



                long Id = objBooking.Id;

                decimal returnFareRate = objBooking.ReturnFareRate.ToDecimal();

                string bookingNo = objBooking.BookingNo.ToString();
                string fromAddress = objBooking.FromAddress;
                string toAddress = objBooking.ToAddress;
                int? fromLocTypeId = objBooking.FromLocTypeId;
                int? toLocTypeId = objBooking.ToLocTypeId;

                string fromDoorNoFlightNo = string.Empty;
                string toDoorNoFlightNo = string.Empty;

                string fromStreetLabel = string.Empty;
                string toStreetLabel = string.Empty;


                string fromStreet = string.Empty;
                string toStreet = string.Empty;

                string fromTypeDoorNoFlightNo = string.Empty;
                string toTypeDoorNoFlightNo = string.Empty;

                if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                {

                    fromDoorNoFlightNo = objBooking.FromDoorNo.ToStr();

                    if (fromDoorNoFlightNo.ToStr().Trim() != string.Empty)
                        fromTypeDoorNoFlightNo = "Flight No :";


                    fromStreet = objBooking.FromStreet;

                    if (fromStreet.ToStr().Trim() != string.Empty)
                        fromStreetLabel = "Coming From :";

                }
                else if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                {

                    fromDoorNoFlightNo = objBooking.FromDoorNo.ToStr();

                    if (fromDoorNoFlightNo.ToStr().Trim() != string.Empty)
                        fromTypeDoorNoFlightNo = "Door No :";
                }
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {

                    fromDoorNoFlightNo = objBooking.FromDoorNo.ToStr();

                    if (fromDoorNoFlightNo.ToStr().Trim() != string.Empty)
                        fromTypeDoorNoFlightNo = "Door No :";


                    fromStreet = objBooking.FromStreet.ToStr();

                    if (fromStreet.ToStr().Trim() != string.Empty)
                        fromStreetLabel = "Street :";
                }
                else
                {
                    fromTypeDoorNoFlightNo = "";
                    fromStreet = "";
                }

                if (toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                {

                    toDoorNoFlightNo = objBooking.ToDoorNo.ToStr();

                    if (toDoorNoFlightNo.ToStr() != string.Empty)
                        toTypeDoorNoFlightNo = "Flight No :";

                    toStreet = objBooking.ToStreet;
                }
                else if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                {

                    toDoorNoFlightNo = objBooking.ToDoorNo.ToStr();

                    if (toDoorNoFlightNo.ToStr() != string.Empty)
                        toTypeDoorNoFlightNo = "Door No :";
                    //toStreet = objBooking.ToDoorNo.ToString();
                }
                else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {

                    toDoorNoFlightNo = objBooking.ToDoorNo.ToStr();

                    if (toDoorNoFlightNo.ToStr().Trim() != string.Empty)
                        toTypeDoorNoFlightNo = "Door No :";


                    toStreet = objBooking.ToStreet.ToStr();

                    if (toStreet.ToStr().Trim() != string.Empty)
                        toStreetLabel = "Street :";
                }
                else
                {
                    toTypeDoorNoFlightNo = "";
                    toStreet = "";
                }

                string vehicle = objBooking.Fleet_VehicleType.VehicleType.ToString();

                string header = this.objSubCompany.CompanyName.ToStr();

                string thankyouLabel = string.Empty;
                if (objBooking.BookingStatusId != Enums.BOOKINGSTATUS.DISPATCHED)
                {
                    thankyouLabel = "Thank you for booking with " + header;
                }
                else if (objBooking.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && objBooking.DriverId != null)
                {
                    thankyouLabel = "Your booking has been Dispatched to Driver : " + objBooking.Fleet_Driver.DriverNo + " - " + objBooking.Fleet_Driver.DriverName;
                }

                // Need to put Field in subcompany For CompanyLogo url for Web
                string logo = string.Empty;

                if (string.IsNullOrEmpty(logo))
                {
                    logo = "<td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; background-repeat: no-repeat;background-position: center top; background-color: #4d8fef; font-weight: bold;font-size: 18px; font-style: normal; line-height: normal; font-variant: normal;text-transform: none; color: White; text-decoration: none;'>"
                    + header + "</td>";
                }

                decimal totalFareCharges = objBooking.FareRate.ToDecimal() + objBooking.ParkingCharges.ToDecimal() + objBooking.WaitingCharges.ToDecimal()
                                           + objBooking.ExtraDropCharges.ToDecimal() + objBooking.MeetAndGreetCharges.ToDecimal() + objBooking.CongtionCharges.ToDecimal();


                string subject = txtSubject.Text.Trim();
                string body = "";
                // Email Template 1
                if (template == "Template1")
                {

                    body = "<table border='1' style='border-style: solid; border-color: #BDC8FF; height: 360px;' cellpadding='0' cellspacing='0' width='95%' align='center'><tr><td colspan='7'  align='center'><tr style='font-size: small'>"
                       + logo +
                        "</tr><tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>" +
                    "Booking Reference:" + bookingNo + "</p><p style='font-weight: bold'>Dear " + objBooking.CustomerName.ToStr() + ",</p><p>" +
                    thankyouLabel + ". Following are the details of the booking.</p></td></tr>" +
                    "<tr style='font-size: small'><td colspan='2' align='center'><table width='99%' border='1' style='border-style: solid;'><tr><td style='background-color: #09a1c4;color:White' align='center'>"
                    + "<b>From</b></td><td style='background-color: #09a1c4;color:White' align='center'><b>To</b></td></tr><tr><td>"
                    + fromAddress + "</td><td>" + toAddress + "</td></tr></table></td></tr><br/></td></tr><tr valign='top'>"
                    + "<td style='background-color: #e8e8e8; height:48px; width:50%' ><table border='0' cellspacing='0' cellpadding='0'  style='height: 354px; width: 100%'><tr height='40'><td align='center' style='height: 50px; background-color:#09a1c4' colspan='2'><span style='color:White'>"
                    + "<b>Journey Detail</b></span></td></tr>" +

                    "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Pickup Point</td></tr>" +
                    "<tr><td align='center' style='width:30%'>"
                    + "From : </td><td align='left'>  " + fromAddress + " </td></tr><tr><td style='width:30%' align='center'>" + fromTypeDoorNoFlightNo + " </td><td align='left'>     " + fromDoorNoFlightNo + " </td></tr><tr>"
                    + "<td style='width:30%' align='center'>" + fromStreetLabel + " </td><td align='left'>    " + fromStreet + "</td></tr>";


                    if (objBooking.Booking_ViaLocations.Count > 0)
                    {
                        int cnt = 1;

                        body += "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Via Point(s)</td></tr>" +
                    "<tr><td align='center' style='width:30%'>"
                    + "Via : </td><td align='left'>  " + string.Join("</br>", objBooking.Booking_ViaLocations.Select(args => cnt++.ToStr() + ". " + args.ViaLocValue).ToArray<string>()) + " </td></tr>"
                          ;

                    }


                    body += "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Destination</td></tr><tr><td style='width:30%' align='center'>"
                    + "To : </td><td align='left'>  " + toAddress + "</td></tr><tr><td style='width:30%' align='center'>"
                    + toTypeDoorNoFlightNo + " </td><td align='left'>     " + toDoorNoFlightNo + " </td></tr><tr><td style='width:30%' align='center'>"
                    + toStreetLabel + " </td><td align='left'>    " + toStreet + "</td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'></td></tr><tr><td style='width:30%' align='center'>"
                    + "Vehicle Type:</td><td align='left'>     " + vehicle + "</td></tr>" +
                    "<tr><td style='width:30%' align='center'>" + "Fares:</td><td align='left'>     £ " + totalFareCharges + " </td></tr>" +
                    (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN ? "<tr><td style='width:30%' align='center'>" + "Return Fares:</td><td align='left'>     £ " + objBooking.ReturnFareRate.ToDecimal() + " </td></tr>" : "") +

                    "<tr><td align='center' style:width:30%>"
                    + "Payment Type : </td><td>" + objBooking.Gen_PaymentType.PaymentType.ToString() + "</td></tr></table></td><td style='background-color: #e8e8e8; height:48px' ><table width='100%' border='0' cellspacing='0' cellpadding='0' style='height: 357px'><tr height='40'><td align='center' style='height: 50px; background-color: #09a1c4' colspan='7'><span style='color:White'>"
                    + "<b>Your Detail</b></span></td></tr><tr><td align='right' bgcolor='#B5BCCD' colspan='7'></tr><tr ><td align='center' style='width:30%'>          "
                    + "Pickup Date :&nbsp;</td><td>" + string.Format("{0:dd/MM/yyyy HH:mm}", objBooking.PickupDateTime) + " </td></tr>"
                    + "<tr><td align='center' style='width:30%'>          Name :&nbsp;</td><td>" + objBooking.CustomerName + " </td></tr>"
                    + "<tr><td align='center' >Mobile Phone :&nbsp;</td> <td>" + objBooking.CustomerMobileNo + "</td></tr>"
                    + "<tr><td align='center' style='width:30%'>Telephone No :&nbsp;</td><td>" + objBooking.CustomerPhoneNo + "</td></tr><tr><td align='center'  colspan='7'>"
                    + "Passenger :&nbsp;" + objBooking.NoofPassengers + " &nbsp;&nbsp;&nbsp;  Luggage : &nbsp;" + objBooking.NoofLuggages + "&nbsp;&nbsp;&nbsp;" + (objBooking.NoofHandLuggages.ToInt() > 0 ? "Hand Luggages :&nbsp; " + objBooking.NoofHandLuggages : "") + "</td></tr><tr>"
                    + "<td align='left'style='width:35%' >Special Requirement :&nbsp; </td><td>" + objBooking.SpecialRequirements + "</td></tr></table></table><tr><td colspan='5'><table width='100%' border='0' ><tr style='font-size: small' >" +
                    "<td align='center' style='font-weight: bold'>Thank You for using our service.</td></tr><tr style='font-size: small'><td  style='font-weight: bold'><p><br />Regards,<br />" + header + ",<br />" + this.objSubCompany.TelephoneNo + "<br />" + this.objSubCompany.WebsiteUrl.ToStr() + "</p></td></tr><tr style='font-size: small'>"
                    + "<td  align='center'></td></tr></table></td></tr></td></tr></table>";

                    //

                }


                else if (template == "Template2")
                {


                    // Email Template 2
                    StringBuilder StrBld = new StringBuilder();


                    StrBld.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid; background-color:White;font-family: verdana, arial;font-size: 11px;font-weight: normal;color: #000;text-decoration: none;'>");
                    StrBld.Append("<tr><td style='text-align: left; padding: 10px 20px 10px 20px; font-size: 16px;font-weight: bold; color: #ef0000; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>Booking Confirmation</td>");

                    string fullCompanyName = this.objSubCompany.CompanyName;


                    if (AppVars.objPolicyConfiguration.DefaultClientId == "StallionTravels")
                    {
                        fullCompanyName += " (Taxis & Minibuses)";
                        StrBld.Append("<td align='center' style='width=100px; border-bottom: #d4e0ee 1px solid; '><img style='width:80px;height=80px;' src='https://7556fc593b86dff9dbbf-be54c067cc795feefe2ab3711fa1fc60.ssl.cf1.rackcdn.com/2920_StallionTravelUKLtd_medium_20140313153729.png'></td> ");
                    }

                    StrBld.Append("<td style='text-align: center; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;color: #640120; font-weight: bold; font-size: 24px' colspan='2'>");



                    StrBld.Append(fullCompanyName + "</td></tr><tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='1' style='text-align: left; padding: 5px 5px 5px 0px; font-size: 16px;font-weight: bold; color: #000; border-bottom: #d4e0ee 1px solid;'>Dear " + objBooking.CustomerName.ToStr() + "," + "<td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 18px;font-weight: normal; color: #6b97c2; border-bottom: #d4e0ee 1px solid;'>");



                    StrBld.Append("Thank You for your Booking. Please check your journey details.</td></tr><tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 16px;font-weight: bold; color: #000;'>" + this.objSubCompany.TelephoneNo + "</td></tr><tr><td colspan='4'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border-top: #d4e0ee 1px solid;'><tr style='background-color: #eff3f9;'>");
                    StrBld.Append("<td width='20%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("ORDER NO:</td><td width='30%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid; color: #008000;'>" + objBooking.BookingNo.ToStr() + "</td>");

                    StrBld.Append("<td width='20%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("</td><td width='30%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;color: #008000;'>" + " " + "</td></tr></table>");

                    StrBld.Append("</td></tr><tr style='background-color: #eff3f9;'><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;'>");

                    StrBld.Append("Traveller Information</td><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; font-size: 12px;'>Carrier Details</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                    StrBld.Append("Passenger:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 25%'>" + objBooking.CustomerName.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                    StrBld.Append("Passenger No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;width: 45%'>" + objBooking.NoofPassengers.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Mobile:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerMobileNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Hand Luggage:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.NoofHandLuggages.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Phone:</td><td style='padding: 5px; bold; border-bottom: #d4e0ee 1px solid;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerPhoneNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Check-in Luggage:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.NoofLuggages.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Email:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerEmail.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Vehicle:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + vehicle + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Pickup Date/Time:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + string.Format("{0:dd-MMM-yyyy HH:mm}", objBooking.PickupDateTime) + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Special Ins:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.SpecialRequirements.ToStr() + "</td></tr><tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr valign='top'><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");
                    StrBld.Append("Pick-up Information</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromAddress + "</td></tr>");

                    if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Number:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Coming From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromStreet.ToStr() + "</td></tr>");
                        //    StrBld.Append(" <tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Landing Date:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>31 August 2014</td></tr>");
                        //    StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Landing Time:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>01 : 00</td></tr>");
                    }
                    else if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromStreet.ToStr() + "</td></tr>");


                    }
                    else
                    {

                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");


                    }
                    StrBld.Append("</table></td><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");
                    StrBld.Append("Drop-off Information</td></tr>");


                    StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>To:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToAddress + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");

                    if (objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {

                        StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToDoorNo + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("To Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToStreet.ToStr() + "</td></tr>");
                    }
                    else
                    {
                        StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToDoorNo + "</td></tr>");


                    }

                    StrBld.Append("</table></td></tr>");


                    if (objBooking.Booking_ViaLocations.Count > 0)
                    {

                        StrBld.Append("<tr><td colspan='4'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>From</td><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>To</td></tr>");

                        int cnt = objBooking.Booking_ViaLocations.Count;


                        for (int i = 0; i < cnt; i++)
                        {
                            if (i == 0)
                            {
                                StrBld.Append("<tr>");
                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.FromAddress.ToStr() + "</td>");

                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                StrBld.Append("</tr>");

                            }
                            else
                            {
                                if (i < cnt)
                                {

                                    StrBld.Append("<tr>");
                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i - 1].ViaLocValue.ToStr() + "</td>");


                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                    StrBld.Append("</tr>");
                                }
                            }


                            if (i + 1 == cnt)
                            {
                                StrBld.Append("<tr>");
                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");

                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToAddress.ToStr() + "</td>");
                                StrBld.Append("</tr>");

                            }


                        }





                        StrBld.Append("</table></td></tr>");
                    }


                    StrBld.Append("<tr><td colspan='4'>&nbsp;</td></tr>");


                    //                 <tr><td style='padding: 10px 5px 10px 5px; font-size: 14px; border: #d4e0ee 1px solid;background-color: White; text-decoration: underline; font-weight: bold;'>Meeting Point:</td><td style='padding: 10px 5px 10px 5px; font-size: 11px; border: #d4e0ee 1px solid;background-color: #eff3f9;' colspan='3'>The driver will meet you with a name board displaying the Passenger name at ARRIVALS <span style='color: Green'>05 Minutes</span> after your flight lands (as per your request). You will have a further <span style='color: Green'>35 minutes</span> of Free waiting time, meaning a total Free waiting time allowance of <span style='color: Red'>40 Minutes</span> from the time of landing which also include car park. Please Note thereafter waiting time is chargeable at the rate of <span style='color: Red'>GBP £20p</span> per minute.</td></tr>
                    StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='4' style='padding: 10px 5px 10px 5px; font-size: 18px; border-bottom: #d4e0ee 1px solid;background-color: #eff3f9;'>");
                    StrBld.Append("GBP Cost: <span style='color: #008000;'>£ " + String.Format("{0:f2}", objBooking.FareRate.ToDecimal()) + "</span>");
                    StrBld.Append(" <span style='color: #008000;'>" + objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr() + "</span></td></tr>");
                    StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='4' style='padding: 10px 5px 10px 5px; font-weight: bold; font-size: 17px;text-align: center; border-bottom: #d4e0ee 1px solid;'>Thank You & Have A Pleasant Journey from all of us on " + this.objSubCompany.CompanyName + " Team.</td></tr>");

                    StrBld.Append("<tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 18px;font-weight: normal; color: #6b97c2;'>Orders are subject to our current terms & conditions. We welcome all comments on the services that we provide.</td></tr>");



                    if (AppVars.objPolicyConfiguration.DefaultClientId == "StallionTravels")
                    {

                        StrBld.Append("<tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 12px;font-weight: normal; color: black;'>www.taxis-manchesterairport.co.uk, www.stalliontravel.co.uk and www.manchester-taxis-minibuses.co.uk are part of Stallion Travel UK Ltd.");
                        StrBld.Append("<tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 12px;font-weight: normal; color: black;'>Registered in England and Wales. No. GB 7853087</td></tr>");
                        StrBld.Append("<tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 12px;font-weight: normal; color: black;'>Our Bank Details: " + this.objSubCompany.BankName.ToStr() + ", Sort Code: " + this.objSubCompany.SortCode.ToStr() + ", A/C No." + this.objSubCompany.AccountNo.ToStr() + ", Swift Code: " + this.objSubCompany.BlcNumber + ", IBAN No. " + this.objSubCompany.IbanNumber + "</td></tr>");



                    }

                    StrBld.Append("</table>");



                    body = StrBld.ToStr();



                }


                else if (template == "Template3")
                {
                    StringBuilder SBuilder = new StringBuilder();
                    SBuilder.Append("<div><div class=\"adM\">");


                    string companyName = this.objSubCompany.CompanyName.ToStr();
                    string CompanyAddress = this.objSubCompany.Address.ToStr();
                    string CompanyEmail = this.objSubCompany.EmailAddress.ToStr();
                    string Fax = this.objSubCompany.Fax;
                    string WebUrl = this.objSubCompany.WebsiteUrl.ToStr();
                    string CompanyPhoneNo = this.objSubCompany.TelephoneNo.ToStr();
                    string DoorNo = objBooking.FromDoorNo.ToStr();

                    string passangeName = objBooking.CustomerName.ToStr().Trim();
                    string telephoneNo = objBooking.CustomerPhoneNo;

                    SBuilder.Append("</div>");

                    SBuilder.Append("<div>");
                    string fullCompanyName = null;
                    string fullCompanyAddress = null;
                    string vehicletype = objBooking.Fleet_VehicleType.VehicleType.ToStr();
                    string orderNo = objBooking.OrderNo.ToStr();
                    string bookedBy = objBooking.BookedBy.ToStr();
                    string fareRate = objBooking.FareRate.ToStr();



                    // string FlightNo=objBooking.fl
                    //AppVars.objSubCompany.CompanyName;
                    string companyId = objBooking.CompanyId.ToStr();
                    if (!string.IsNullOrEmpty(companyId))
                    {
                        fullCompanyName = objBooking.Gen_Company.CompanyName.ToStr();
                        fullCompanyAddress = objBooking.Gen_Company.Address.ToStr(); //AppVars.objSubCompany.Address;
                    }
                    //else
                    //{ 

                    //}
                    string vehicletypes = objBooking.VehicleTypeId.Value.ToStr();
                    string paymentType = objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType;
                    if (string.IsNullOrEmpty(fullCompanyName))
                    {

                        fullCompanyName = paymentType;
                    }
                    string bookingdate = string.Format("{0:ddd-dd-MMM-yyyy}", objBooking.PickupDateTime);
                    string bookingtime = string.Format("{0:HH:mm}", objBooking.PickupDateTime) + " ( " + string.Format("{0:hh:mm tt}", objBooking.PickupDateTime) + " ) ";



                    if (!string.IsNullOrEmpty(DoorNo))
                        fromAddress = DoorNo + " - " + fromAddress;

                    if (!string.IsNullOrEmpty(toDoorNoFlightNo))
                        toAddress = toDoorNoFlightNo + " - " + toAddress;



                    if (!string.IsNullOrEmpty(objBooking.CustomerMobileNo.ToStr().Trim()))
                        telephoneNo = telephoneNo + " / " + objBooking.CustomerMobileNo.ToStr().Trim();

                    SBuilder.Append("<blockquote style=\"margin-top:5.0pt;margin-bottom:5.0pt\">");

                    SBuilder.Append("<div>");

                    SBuilder.Append("<p align=\"center\" style=\"text-align:center\"><b><span lang=\"EN-GB\" style=\"font-size:18.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\">Booking Confirmation</span></b><span lang=\"EN-GB\" style=\"font-size:7.5pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\"><br>");
                    SBuilder.Append("</span><b><span lang=\"EN-GB\" style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\">" + companyName + " </span></b><span lang=\"EN-GB\" style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\">," + CompanyAddress + " </span><span lang=\"EN-GB\" style=\"font-size:7.5pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\"><br>");
                    SBuilder.Append("</span><b><span lang=\"EN-GB\" style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\">Tel No. </span></b><span lang=\"EN-GB\" style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\">" + CompanyPhoneNo + ", <b>Fax No. </b>" + Fax + "</span><span lang=\"EN-GB\" style=\"font-size:7.5pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\"><br>");
                    SBuilder.Append("</span><b><span lang=\"EN-GB\" style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\">E~mail:");
                    SBuilder.Append("</span></b><span lang=\"EN-GB\" style=\"font-size:10.0pt;font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;\"><a href=mailto:" + CompanyEmail + " target=\"_blank\">" + CompanyEmail + "</a>, <b>Web site: </b><a href=" + WebUrl + " target=\"_blank\">" + WebUrl + "</a></span><span lang=\"EN-GB\"><u></u><u></u></span></p>");

                    SBuilder.Append("<p><b><i><span lang=\"EN-GB\"><br>" + fullCompanyName + "</span></i></b><span lang=\"EN-GB\"><br>");
                    SBuilder.Append("<i>" + fullCompanyAddress + "</i><u></u><u></u></span></p>");

                    SBuilder.Append("<table border=\"0\" cellspacing=\"1\" cellpadding=\"0\" width=\"100%\" style=\"width:100.0%\"> <tbody><tr>");
                    SBuilder.Append("<td width=\"20%\" valign=\"top\" style=\"width:20.0%;padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Account Ref" + "</span></b><u></u><u></u></p>");
                    SBuilder.Append("</td>  <td width=\"80%\" valign=\"top\" style=\"width:80.0%;padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + paymentType + "</span><u></u><u></u></p>");
                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Date of booking</span></b><u></u><u></u></p></td>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookingdate + "</span><u></u><u></u></p></td></tr><tr>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Time of booking</span></b>");
                    SBuilder.Append("<u></u><u></u></p></td>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\"><span class=\"aBn\" data-term=\"goog_1199880031\" tabindex=\"0\"><span class=\"aQJ\">" + bookingtime + "</span></span></span><u></u><u></u></p>");
                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Booking No.</span></b> <u></u><u></u></p>");
                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookingNo + "</span><u></u><u></u></p>");
                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Vehicle type</span></b> <u></u><u></u></p></td>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\"> " + vehicletype + "</span><u></u><u></u></p>");
                    SBuilder.Append("</td></tr>");


                    SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Number of Passengers</span></b>");
                    SBuilder.Append("<u></u><u></u></p></td><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + objBooking.NoofPassengers.ToInt() + "</span><u></u><u></u></p></td></tr>");


                    SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Number of Luggages</span></b>");
                    SBuilder.Append("<u></u><u></u></p></td><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + objBooking.NoofLuggages.ToInt() + "</span><u></u><u></u></p></td></tr>");



                    SBuilder.Append("<tr>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">From</span></b><span style=\"font-size:10.0pt\"> </span><u></u><u></u></p>");
                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + fromAddress + "</span><u></u><u></u></p>");
                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"></td>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"></td></tr><tr>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">To</span></b> <u></u><u></u></p>");
                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + toAddress + "</span><u></u><u></u></p>");
                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Passenger name</span></b> <u></u><u></u></p>");
                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\">");
                    SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\"> " + passangeName + "</span><u></u><u></u></p></td></tr>");
                    SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"></td>");
                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"></td>");
                    SBuilder.Append("</tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                    if (string.IsNullOrEmpty(companyId))
                    {
                        //SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Order number</span></b> <u></u><u></u></p></td>");
                        //SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + orderNo + "</span><u></u><u></u></p>");
                        //SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Booked by</span></b> <u></u><u></u></p>");
                        //SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookedBy + "</span><u></u><u></u></p>");
                    }
                    else
                    {
                        SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Order number</span></b> <u></u><u></u></p></td>");
                        SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + orderNo + "</span><u></u><u></u></p>");
                        SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Booked by</span></b> <u></u><u></u></p>");
                        SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookedBy + "</span><u></u><u></u></p>");
                    }


                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Contact No.</span></b><u></u><u></u></p>");
                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + telephoneNo + "</span><u></u><u></u></p>");



                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"></td><td style=\"padding:0in 0in 0in 0in\"></td></tr>");
                    if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        // aDD Flightno Row  Flight No.
                        //objBooking.FromDoorNo

                        SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Flight No</span></b><u></u><u></u></p>");
                        SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + DoorNo + "</span><u></u><u></u></p>");
                    }
                    if (string.IsNullOrEmpty(companyId))
                    {
                        SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Job price</span></b><u></u><u></u></p>");
                        SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + fareRate.ToStr() + "</span><u></u><u></u></p>");
                    }
                    else
                    {
                        SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Job price</span></b><u></u><u></u></p>");
                        SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">----</span><u></u><u></u></p>");
                    }


                    SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Special Requirements</span></b><u></u><u></u></p>");
                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + objBooking.SpecialRequirements.ToStr() + "</span><u></u><u></u></p>");




                    SBuilder.Append("</td></tr></table><p><span lang=\"EN-GB\">&nbsp;<u></u><u></u></span></p>");




                    // Return Info


                    if (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                    {
                        if (objBooking.ReturnPickupDateTime != null)
                        {

                            bookingdate = string.Format("{0:ddd-dd-MMM-yyyy}", objBooking.ReturnPickupDateTime);

                            bookingtime = string.Format("{0:HH:mm}", objBooking.ReturnPickupDateTime) + " ( " + string.Format("{0:hh:mm tt}", objBooking.ReturnPickupDateTime) + " ) ";


                            if (string.IsNullOrEmpty(companyId))
                            {
                                fareRate = objBooking.ReturnFareRate.ToStr();
                            }


                            Booking objReturn = null;

                            if (objBooking.BookingReturns.Count > 0)
                            {



                                objReturn = objBooking.BookingReturns[0];

                                bookingNo = objReturn.BookingNo.ToStr();

                                fromAddress = objReturn.FromAddress.ToStr();
                                toAddress = objReturn.ToAddress.ToStr();



                                if (!string.IsNullOrEmpty(objReturn.FromDoorNo.ToStr().Trim()))
                                    fromAddress = objReturn.FromDoorNo.ToStr().Trim() + " - " + fromAddress;

                                if (!string.IsNullOrEmpty(objReturn.ToDoorNo.ToStr().Trim()))
                                    toAddress = objReturn.ToDoorNo.ToStr().Trim() + " - " + toAddress;




                                SBuilder.Append("<p><span lang=\"EN-GB\"><u><b>Return Booking</b></u><u></u></span></p>");

                                SBuilder.Append("<p><span lang=\"EN-GB\"><u></u><u></u></span></p>");


                                SBuilder.Append("<table border=\"0\" cellspacing=\"1\" cellpadding=\"0\" width=\"100%\" style=\"width:100.0%\"> <tbody><tr>");
                                SBuilder.Append("<td width=\"20%\" valign=\"top\" style=\"width:20.0%;padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Account Ref" + "</span></b><u></u><u></u></p>");
                                SBuilder.Append("</td>  <td width=\"80%\" valign=\"top\" style=\"width:80.0%;padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + paymentType + "</span><u></u><u></u></p>");
                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Date of booking</span></b><u></u><u></u></p></td>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookingdate + "</span><u></u><u></u></p></td></tr><tr>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Time of booking</span></b>");
                                SBuilder.Append("<u></u><u></u></p></td>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\"><span class=\"aBn\" data-term=\"goog_1199880031\" tabindex=\"0\"><span class=\"aQJ\">" + bookingtime + "</span></span></span><u></u><u></u></p>");
                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Booking No.</span></b> <u></u><u></u></p>");
                                SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookingNo + "</span><u></u><u></u></p>");
                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Vehicle type</span></b> <u></u><u></u></p></td>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\"> " + vehicletype + "</span><u></u><u></u></p>");
                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Number of vehicles</span></b>");
                                SBuilder.Append("<u></u><u></u></p></td><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">1</span><u></u><u></u></p></td></tr>");


                                SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Number of Passengers</span></b>");
                                SBuilder.Append("<u></u><u></u></p></td><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + objReturn.NoofPassengers.ToInt() + "</span><u></u><u></u></p></td></tr>");


                                SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Number of Luggages</span></b>");
                                SBuilder.Append("<u></u><u></u></p></td><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + objReturn.NoofLuggages.ToInt() + "</span><u></u><u></u></p></td></tr>");


                                SBuilder.Append("<tr>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">From</span></b><span style=\"font-size:10.0pt\"> </span><u></u><u></u></p>");
                                SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + fromAddress + "</span><u></u><u></u></p>");
                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"></td>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"></td></tr><tr>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">To</span></b> <u></u><u></u></p>");
                                SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + toAddress + "</span><u></u><u></u></p>");
                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Passenger name</span></b> <u></u><u></u></p>");
                                SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\">");
                                SBuilder.Append("<p class=\"MsoNormal\"><span style=\"font-size:10.0pt\"> " + passangeName + "</span><u></u><u></u></p></td></tr>");
                                SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"></td>");
                                SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"></td>");
                                SBuilder.Append("</tr><tr><td style=\"padding:0in 0in 0in 0in\">");
                                if (string.IsNullOrEmpty(companyId))
                                {
                                    //SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Order number</span></b> <u></u><u></u></p></td>");
                                    //SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + orderNo + "</span><u></u><u></u></p>");
                                    //SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Booked by</span></b> <u></u><u></u></p>");
                                    //SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookedBy + "</span><u></u><u></u></p>");
                                }
                                else
                                {
                                    SBuilder.Append("<p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Order number</span></b> <u></u><u></u></p></td>");
                                    SBuilder.Append("<td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + orderNo + "</span><u></u><u></u></p>");
                                    SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Booked by</span></b> <u></u><u></u></p>");
                                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + bookedBy + "</span><u></u><u></u></p>");
                                }
                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Contact No.</span></b><u></u><u></u></p>");
                                SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + telephoneNo + "</span><u></u><u></u></p>");



                                SBuilder.Append("</td></tr><tr><td style=\"padding:0in 0in 0in 0in\"></td><td style=\"padding:0in 0in 0in 0in\"></td></tr>");
                                if (objReturn.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                                {
                                    // aDD Flightno Row  Flight No.
                                    //objBooking.FromDoorNo

                                    SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Flight No</span></b><u></u><u></u></p>");
                                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + DoorNo + "</span><u></u><u></u></p>");
                                }
                                if (string.IsNullOrEmpty(companyId))
                                {
                                    SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Job price</span></b><u></u><u></u></p>");
                                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + fareRate.ToStr() + "</span><u></u><u></u></p>");
                                }
                                else
                                {
                                    SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Job price</span></b><u></u><u></u></p>");
                                    SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">----</span><u></u><u></u></p>");
                                }

                                SBuilder.Append("<tr><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><b><span style=\"font-size:10.0pt\">Special Requirements</span></b><u></u><u></u></p>");
                                SBuilder.Append("</td><td style=\"padding:0in 0in 0in 0in\"><p class=\"MsoNormal\"><span style=\"font-size:10.0pt\">" + objReturn.SpecialRequirements.ToStr() + "</span><u></u><u></u></p>");

                            }


                        }
                    }







                    SBuilder.Append("</td></tr></table><p><span lang=\"EN-GB\">&nbsp;<u></u><u></u></span></p>");







                    SBuilder.Append("<p><span lang=\"EN-GB\">&nbsp;<u></u><u></u></span></p>");
                    SBuilder.Append("<p><b><span lang=\"EN-GB\" style=\"font-size:10.0pt\">Print date :</span></b><span lang=\"EN-GB\" style=\"font-size:10.0pt\"> " + string.Format("{0:dd-MMM-yyyy HH:mm}", DateTime.Now) + "</span><span lang=\"EN-GB\"><u></u><u></u></span></p><div class=\"yj6qo\"></div><div class=\"adL\">");

                    SBuilder.Append("</div></div><div class=\"adL\"></div></blockquote><div class=\"adL\"></div></div>");
                    body = SBuilder.ToStr();


                }
                else if (template == "Template4")
                {

                    objSubCompany = listofSubCompanies.FirstOrDefault(c => c.EmailAddress.ToStr() == from);

                    if (objSubCompany == null)
                        objSubCompany = objBooking.Gen_SubCompany.DefaultIfEmpty();


                    string telephoneNo = objSubCompany.TelephoneNo.ToStr();


                    if (telephoneNo.Length == 0)
                        telephoneNo = "+";


                    body = "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                                            "<head>" +
                                            "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />" +
                                            "<title>Untitled Document</title>" +
                                            "<style>" +
                                            "body {" +
                                                "text-align: left;" +
                                                "letter-spacing: normal;" +
                                                "word-spacing: normal;" +
                                                "white-space: normal;" +
                                                "background:#FFF;" +
                                            "}" +
                                            ".body {" +
                                                "clear: both;" +
                                                "margin: 0 auto;" +
                                                "width: 100%;" +
                                            "}" +
                                            "p {" +
                                                "display: block;" +
                                            "-webkit-margin-before: 0.2em;" +
                                            "-webkit-margin-after: 0.5em;" +
                                            "-webkit-margin-start: 0px;" +
                                            "-webkit-margin-end: 0px;" +
                                                "font-family:Arial, Helvetica, sans-serif;" +
                                                "font-size: 13px;" +
                                                "font-weight: normal;" +
                                                "color: #333;" +
                                                "text-decoration: none;" +
                                            "}" +
                                            "h1 {font-size: 1.571em}" +	/* 22px */
                                            "h2 {font-size: 1.571em}" +	/* 22px */
                                            "h3 {font-size: 1.429em}" +	/* 20px */
                                            "h4 {font-size: 1.286em}" +	/* 18px */
                                            "h5 {font-size: 1.143em}" +	/* 16px */
                                            "h6 {font-size: 1em}	" +	/* 14px */

                                            "h1, h2, h3, h4, h5, h6 {" +
                                                "font-weight: 400;" +
                                                "line-height: 1.1;" +
                                                "margin-bottom: .8em;" +
                                                "font-family:Arial, Helvetica, sans-serif;" +
                                                "font-weight: bold;" +
                                            "}" +
                                            ".wrapper {" +
                                                "width: 1000px;" +
                                                "margin:0 auto;" +
                                            "}" +
                                            "</style>" +
                                            "</head>" +

                                            "<body>" +
                                            "<div class=\"wrapper\">" +
                                            "<p><strong>Dear " + objBooking.CustomerName + ".</strong>&nbsp;<br />" +
                                              "<br />" +
                                              "We thank you for booking your airport transfer  with us.&nbsp; We are confirming your booking as it is detailed  below: </p>" +
                                            "<p>Your  booking reference is:&nbsp;<strong>" + objBooking.BookingNo.ToStr() + ";</strong><br />" +
                                              "<br />" +
                                              "When you contact us, we advise you to provide us  this reference to enable us to deal with your enquiry.&nbsp;<br />" +
                                              "<br />" +
                                              "It is the responsibility of the passenger or the  person who makes this reservation to ensure that they provide us with the  correct information. To do this, we would recommend you to double check the  transfer details below this confirmation. If any of the details are incorrect,  please advise us. </p>" +
                                            "<p>Please  inform us, if there are any changes or cancellations to your travel plans with  regard to this transfer at your earliest&nbsp;convenience.<br />" +
                                              "<br />" +
                                              "If any of the details of the journey are  incorrect, then please advise us as soon as possible. Should the amendments  alter&nbsp;the total mileage, period of time the vehicle is required  or the specification of the vehicle or the price will be adjusted&nbsp;<br />" +
                                              "accordingly.<br />" +
                                              "<br />" +
                                              "<br />" +
                                              "Below statement is only for the passengers who  will be travelling from the <strong>airport/seaport  / station.</strong> Passengers, who will be travelling from other locations, please  IGNORE the following statement. </p>" +
                                            "<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" align=\"left\" width=\"100%\">" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p><strong>Airports</strong></p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p><strong>Meeting Points</strong></p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Heathrow Terminal 1</p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of Costa (coffee    shop)</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Heathrow Terminal 2</p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of Coffee Nero    (coffee shop) </p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Heathrow Terminal 3  </p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of WHSmith    (Blue, Stationary shop)</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Heathrow Terminal 4 </p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of Costa (coffee    shop)</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Heathrow Terminal 5</p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of Costa (coffee    shop)</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Gatwick North Terminal</p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of airport    information desk</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Gatwick South Terminal</p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of airport    information desk</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Stansted Airport </p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of airport    information desk</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>Luton Airport </p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of airport    information desk</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>London City     Airport </p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of airport    information desk</p></td>" +
                                              "</tr>" +
                                              "<tr>" +
                                                "<td width=\"217\" nowrap=\"nowrap\" valign=\"bottom\"><p>All the other UK airports</p></td>" +
                                                "<td width=\"426\" nowrap=\"nowrap\" valign=\"bottom\"><p>At the arrival gate barrier or in front of airport    information desk</p></td>" +
                                              "</tr>" +
                                            "</table>" +
                                            "<p>&nbsp;</p>" +
                                            "<p><strong>Meeting points at the airports:</strong> <br />" +
                                              "On arrival, our driver will be at the airport  arrival with the <strong>passenger's&nbsp;</strong><strong>name board.</strong> However, if you have any difficulties finding your  driver in the arrival hall, please call us on the contact number provided below.</p>" +
                                            "<p>&nbsp;</p>" +
                                            "<p><strong>Seaport / Cruise pickup - meeting point</strong>:<br />" +
                                              "On  arrival, our driver will be at the&nbsp;<strong>seaport</strong>&nbsp;arrival hall with passenger name  board. However, if you have any difficulties finding your driver in the arrival  hall, please call us on the contact number provided below.<br />" +
                                              "<br />" +
                                              "<strong>Train Station pickup – meeting point</strong>: On all train station pickups including Euston, St  Pancras, Kings Cross, Victoria and Waterloo, our driver will  be waiting at the dedicated minicab pickup point which is situated outside the  station. If you are not sure where this is, you can ask any member of staff,  who works at the train station; they will guide you to the pickup point.  We can also offer a meet and greet service with  an&nbsp;<strong>additional charge</strong>&nbsp;of £7.00, where our driver will wait  at the station arrival hall with the passenger name board.&nbsp;<br />" +
                                              "<br />" +
                                              "<strong>What do I do if I cannot find my driver?</strong>&nbsp;Sometimes it can be difficult to find your driver at  the airport, station or hotel, when it is very busy. For any reason, if you are  unable to locate our driver, you&nbsp;<strong>must</strong> contact us on the numbers provided below. In the event of failing to contact us  or failing to meet our driver, this will be considered as &ldquo;no show&rdquo; and you  will be liable to pay the full fare or if the fare is pre- paid the payment  will not be refunded (Please see our terms and conditions on our website for  further details).&nbsp;<br />" +
                                              "<br />" +
                                              "<strong>UK</strong><strong> Callers: 0203 3276 606 | International Callers: +44 203 3276 606</strong>&nbsp;<br />" +
                                              "<strong>UK</strong><strong> Callers: 0208 1234 971 | International Callers: +44 208 1234 971</strong>&nbsp;<br />" +
                                              "<br />" +
                                              "Should the amendments alter the total mileage,  period of time the vehicle is required or the specification of the vehicle, the  price will be adjusted accordingly.&nbsp;<br />" +
                                              "<br />" +
                                              "<br />" +
                                              "<strong>We also suggest that the passenger keeps our  contact details with their travel documents as a referral.&nbsp;</strong> <strong>Any further details,  please refer  to our website or contact our customer service team.</strong><br />" +
                                            "</p>" +
                                            "<h1 align=\"center\">IMPORTANT NOTICE </h1>" +
                                            "<p align=\"center\"><br />" +
                                              "Please Note:  If you wish to make further travel arrangements with us, such as returns trips  must be booked through our official website or with our customer service team  via our online, email or phone (not through any&nbsp;other sources). <br />" +
                                              "<br />" +
                                              "<br />" +
                                              "<br />" +
                                              "PLEASE ADD  OUR WEBSITE ON YOUR COMPUTER''S FAVORITE LIST AND YOU WILL NEVER HAVE TO SEARCH  FOR US AGAIN.&nbsp;<br />" +
                                            "</p>" +
                                            "<p>&nbsp;</p>" +
                                            "<p><br />" +
                                              "Kind Regards<br />" +
                                              "Customer Service Team<br />" +
                                              "<br />" +
                                            "</p>" +
                                            "<h2>Visit  us&nbsp;&gt;&gt;<a href=\"https://" + objSubCompany.WebsiteUrl.ToStr().Trim() + "\"" + "target=\"_blank\">" + objSubCompany.WebsiteUrl.ToStr().Trim() + "</a></h2>" +
                                            "<p><br />" +
                                              "Email -&nbsp;<a href=\"https://" + objSubCompany.EmailAddress.ToStr() + "\"" + "target=\"_blank\">" + objSubCompany.EmailAddress.ToStr().Trim() + "</a>&nbsp;| UK Callers: " + telephoneNo + " | International Callers:+44" + telephoneNo.ToStr().Remove(0, 1).ToStr() + "&nbsp;<br />" +
                                              "<img src=\"" + objSubCompany.CompanyLogoOnlinePath.ToStr().Trim() + "\"" + "alt=\"\" /></p>" +

                                            "</div><br/><br/><br/>";




                    StringBuilder StrBld = new StringBuilder();


                    StrBld.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid; background-color:White;font-family: verdana, arial;font-size: 11px;font-weight: normal;color: #000;text-decoration: none;'>");
                    StrBld.Append("<tr><td style='text-align: left; padding: 10px 20px 10px 20px; font-size: 16px;font-weight: bold; color: #ef0000; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>Booking Confirmation</td>");

                    string fullCompanyName = objSubCompany.CompanyName.ToStr();




                    StrBld.Append("<td style='text-align: center; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;color: #640120; font-weight: bold; font-size: 24px' colspan='2'>");



                    StrBld.Append(fullCompanyName + "</td></tr><tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='1' style='text-align: left; padding: 5px 5px 5px 0px; font-size: 16px;font-weight: bold; color: #000; border-bottom: #d4e0ee 1px solid;'>Dear " + objBooking.CustomerName.ToStr() + "," + "<td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 18px;font-weight: normal; color: #6b97c2; border-bottom: #d4e0ee 1px solid;'>");



                    StrBld.Append("Thank You for your Booking. Please check your journey details.</td></tr><tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 16px;font-weight: bold; color: #000;'>" + objSubCompany.TelephoneNo + "</td></tr><tr><td colspan='4'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border-top: #d4e0ee 1px solid;'><tr style='background-color: #eff3f9;'>");
                    StrBld.Append("<td width='20%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("ORDER NO:</td><td width='30%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid; color: #008000;'>" + objBooking.BookingNo.ToStr() + "</td>");

                    StrBld.Append("<td width='20%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("</td><td width='30%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;color: #008000;'>" + " " + "</td></tr></table>");

                    StrBld.Append("</td></tr><tr style='background-color: #eff3f9;'><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;'>");

                    StrBld.Append("Traveller Information</td><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; font-size: 12px;'>Carrier Details</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                    StrBld.Append("Passenger:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 25%'>" + objBooking.CustomerName.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                    StrBld.Append("Passenger No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;width: 45%'>" + objBooking.NoofPassengers.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Mobile:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerMobileNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Hand Luggage:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.NoofHandLuggages.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Phone:</td><td style='padding: 5px; bold; border-bottom: #d4e0ee 1px solid;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerPhoneNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Check-in Luggage:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.NoofLuggages.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Email:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerEmail.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Vehicle:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Pickup Date/Time:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + string.Format("{0:dd-MMM-yyyy HH:mm}", objBooking.PickupDateTime) + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Special Ins:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.SpecialRequirements.ToStr() + "</td></tr><tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr valign='top'><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");
                    StrBld.Append("Pick-up Information</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromAddress + "</td></tr>");

                    if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Number:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Coming From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromStreet.ToStr() + "</td></tr>");
                        //    StrBld.Append(" <tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Landing Date:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>31 August 2014</td></tr>");
                        //    StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Landing Time:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>01 : 00</td></tr>");
                    }
                    else if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromStreet.ToStr() + "</td></tr>");


                    }
                    else
                    {

                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");


                    }
                    StrBld.Append("</table></td><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");
                    StrBld.Append("Drop-off Information</td></tr>");


                    StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>To:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToAddress + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");

                    if (objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {

                        StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToDoorNo + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("To Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToStreet.ToStr() + "</td></tr>");
                    }
                    else
                    {
                        StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToDoorNo + "</td></tr>");


                    }

                    StrBld.Append("</table></td></tr>");


                    if (objBooking.Booking_ViaLocations.Count > 0)
                    {

                        StrBld.Append("<tr><td colspan='4'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>From</td><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>To</td></tr>");

                        int cnt = objBooking.Booking_ViaLocations.Count;


                        for (int i = 0; i < cnt; i++)
                        {
                            if (i == 0)
                            {
                                StrBld.Append("<tr>");
                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.FromAddress.ToStr() + "</td>");

                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                StrBld.Append("</tr>");

                            }
                            else
                            {
                                if (i < cnt)
                                {

                                    StrBld.Append("<tr>");
                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i - 1].ViaLocValue.ToStr() + "</td>");


                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                    StrBld.Append("</tr>");
                                }
                            }


                            if (i + 1 == cnt)
                            {
                                StrBld.Append("<tr>");
                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");

                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToAddress.ToStr() + "</td>");
                                StrBld.Append("</tr>");

                            }


                        }

                        StrBld.Append("</table></td></tr>");
                    }


                    StrBld.Append("<tr><td colspan='4'>&nbsp;</td></tr>");


                    //                 <tr><td style='padding: 10px 5px 10px 5px; font-size: 14px; border: #d4e0ee 1px solid;background-color: White; text-decoration: underline; font-weight: bold;'>Meeting Point:</td><td style='padding: 10px 5px 10px 5px; font-size: 11px; border: #d4e0ee 1px solid;background-color: #eff3f9;' colspan='3'>The driver will meet you with a name board displaying the Passenger name at ARRIVALS <span style='color: Green'>05 Minutes</span> after your flight lands (as per your request). You will have a further <span style='color: Green'>35 minutes</span> of Free waiting time, meaning a total Free waiting time allowance of <span style='color: Red'>40 Minutes</span> from the time of landing which also include car park. Please Note thereafter waiting time is chargeable at the rate of <span style='color: Red'>GBP £20p</span> per minute.</td></tr>
                    StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='4' style='padding: 10px 5px 10px 5px; font-size: 18px; border-bottom: #d4e0ee 1px solid;background-color: #eff3f9;'>");
                    StrBld.Append("GBP Cost: <span style='color: #008000;'>£ " + String.Format("{0:f2}", objBooking.FareRate.ToDecimal()) + "</span>");
                    StrBld.Append(" <span style='color: #008000;'>" + objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr() + "</span></td></tr>");
                    StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='4' style='padding: 10px 5px 10px 5px; font-weight: bold; font-size: 17px;text-align: center; border-bottom: #d4e0ee 1px solid;'>Thank You & Have A Pleasant Journey from all of us on " + AppVars.objSubCompany.CompanyName + " Team.</td></tr>");

                    StrBld.Append("<tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 18px;font-weight: normal; color: #6b97c2;'>Orders are subject to our current terms & conditions. We welcome all comments on the services that we provide.</td></tr>");


                    StrBld.Append("</table>");
                    body += StrBld.ToStr();
                    body += "</body></html>";

                }

                else if (template == "Template5")
                {


                    string emailTo = string.Empty;
                    decimal price = 0.00m;
                    decimal returnPrice = 0.00m;


                    if (ddlEmailToType.SelectedIndex == 0)
                    {
                        emailTo = objBooking.CustomerName.ToStr();
                        price = objBooking.FareRate.ToDecimal();
                        returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].ReturnFareRate.ToDecimal() : 0.00m;
                    }
                    else if (ddlEmailToType.SelectedIndex == 1)
                    {
                        if (objBooking.CompanyId == null)
                        {
                            ENUtils.ShowMessage("Company is not defined in Booking");
                            return;
                        }

                        emailTo = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();

                        price = objBooking.CompanyPrice.ToDecimal();
                        returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].CompanyPrice.ToDecimal() : 0.00m;
                    }
                    else if (ddlEmailToType.SelectedIndex == 2)
                    {

                        if (objBooking.DriverId != null)
                        {
                            emailTo = objBooking.Fleet_Driver.DefaultIfEmpty().DriverName.ToStr();

                        }
                        else
                        {

                            emailTo = objBooking.CustomerName.ToStr();
                        }
                        price = objBooking.FareRate.ToDecimal();
                        returnPrice = objBooking.ReturnFareRate.ToDecimal();
                    }


                    body = "<table border='1' style='border-style: solid; border-color: #BDC8FF; height: 360px;table-layout: fixed;overflow: hidden;word-wrap:break-word;' cellpadding='0' cellspacing='0' width='95%' align='center'><tr><td colspan='2'  align='center'><tr style='font-size: small'>"
                       + logo;



                    if (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && chkReturnDetails.Checked)
                    {

                        body += "</tr><tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>Dear " + emailTo + ",</p><p>" +
                          thankyouLabel + ". Following are the details of the booking.</p></td></tr>";


                        body += "<tr style='font-size: small'><td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; background-repeat: no-repeat;background-position: center top; background-color: #4d8fef; font-weight: bold;font-size: 18px; font-style: normal; line-height: normal; font-variant: normal;text-transform: none; color: White; text-decoration: none;'>One Way Journey Details</td></tr>" +
                       "<tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>Booking Reference:" + bookingNo + "</p></td></tr>";
                    }
                    else
                    {

                        body += "</tr><tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>" +
                        "Booking Reference:" + bookingNo + "</p><p style='font-weight: bold'>Dear " + emailTo + ",</p><p>" +
                        thankyouLabel + ". Following are the details of the booking.</p></td></tr>";
                    }

                    body += "<tr style='font-size: small'><td colspan='2' align='center'><table width='99%' border='1' style='border-style: solid;'><tr><td style='background-color: #09a1c4;color:White;width:50%;' align='left'>"
                    + "<b>From</b></td><td style='background-color: #09a1c4;color:White;width:50%;' align='left'><b>To</b></td></tr><tr><td style='width:50%;'>"
                    + fromAddress + "</td><td style='width:50%;'>" + toAddress + "</td></tr></table></td></tr><br/></td></tr><tr valign='top'>"
                    + "<td style='background-color: #e8e8e8; height:48px; width:50%' ><table border='0' cellspacing='0' cellpadding='0'  style='height: 354px; width: 100%'><tr height='40'><td align='center' style='height: 50px; background-color:#09a1c4' colspan='2'><span style='color:White'>"
                    + "<b>Journey Detail</b></span></td></tr>" +

                    "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Pickup Point</td></tr>" +
                    "<tr><td align='left' style='width:30%'>"
                    + "&nbsp;&nbsp;From : </td><td align='left'>  " + fromAddress + " </td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;" + fromTypeDoorNoFlightNo + " </td><td align='left'>     " + fromDoorNoFlightNo + " </td></tr><tr>"
                    + "<td style='width:30%' align='left'>&nbsp;&nbsp;" + fromStreetLabel + " </td><td align='left'>    " + fromStreet + "</td></tr>";


                    if (objBooking.Booking_ViaLocations.Count > 0)
                    {
                        int cnt = 1;

                        body += "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Via Point(s)</td></tr>" +
                    "<tr><td align='center' style='width:30%'>"
                    + "</td><td align='left'>  " + string.Join("</br>", objBooking.Booking_ViaLocations.Select(args => cnt++.ToStr() + ". " + args.ViaLocValue).ToArray<string>()) + " </td></tr>"
                          ;

                    }




                    string special = objBooking.SpecialRequirements.ToStr().Trim();


                    if (special.Length > 0)
                    {
                        if (special.Length > 70)
                        {

                            special.Insert(69, "</br>&nbsp;");
                            if (special.Length > 140)
                                special.Insert(139, "</br>&nbsp;");



                            if (special.Length > 210)
                                special.Insert(209, "</br>&nbsp;");


                            if (special.Length > 280)
                                special.Insert(279, "</br>&nbsp;");

                            if (special.Length > 350)
                                special.Insert(349, "</br>&nbsp;");
                        }
                    }

                    body += "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Destination</td></tr><tr><td style='width:30%' align='left'>"
                    + "&nbsp;&nbsp;To : </td><td align='left'>  " + toAddress + "</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;"
                    + toTypeDoorNoFlightNo + " </td><td align='left'>     " + toDoorNoFlightNo + " </td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;"
                    + toStreetLabel + " </td><td align='left'>    " + toStreet + "</td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'></td></tr><tr><td style='width:30%' align='left'>"
                    + "&nbsp;&nbsp;Vehicle Type:</td><td align='left'>     " + vehicle + "</td></tr>" +
                    "<tr><td style='width:30%' align='left'>" + "&nbsp;&nbsp;Fares:</td><td align='left'>     £ " + price + " </td></tr>" +

                    "<tr><td align='left' style:width:30%>"
                    + "&nbsp;&nbsp;Payment Type : </td><td>" + objBooking.Gen_PaymentType.PaymentType.ToString() + "</td></tr></table></td><td style='background-color: #e8e8e8; height:48px' ><table width='100%' border='0' cellspacing='0' cellpadding='0' style='height: 357px'><tr height='40'><td align='center' style='height: 50px; background-color: #09a1c4' colspan='7'><span style='color:White'>"
                    + "<b>Your Detail</b></span></td></tr><tr><td align='right' bgcolor='#B5BCCD' colspan='7'></tr><tr ><td align='left' style='width:30%'>          "
                    + "&nbsp;&nbsp;Pickup Date :&nbsp;</td><td>" + string.Format("{0:dd/MM/yyyy HH:mm}", objBooking.PickupDateTime) + " </td></tr>"
                    + "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Name :&nbsp;</td><td>" + objBooking.CustomerName + " </td></tr>"
                    + "<tr><td align='left' >&nbsp;&nbsp;Mobile Phone :&nbsp;</td> <td>" + objBooking.CustomerMobileNo + "</td></tr>"
                    + "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Telephone No :&nbsp;</td><td>" + objBooking.CustomerPhoneNo + "</td></tr>"

                    + "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Passenger :&nbsp;</td><td>" + objBooking.NoofPassengers + "</td></tr>"
                    + "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Luggage :&nbsp;</td><td>" + objBooking.NoofLuggages + "</td></tr>"



                    + "<tr><td style='width:40%' >&nbsp;&nbsp;</td></tr>"
                    + "<tr><td align='left'   bgcolor='#B5BCCD' colspan='7' >&nbsp;&nbsp;Special Requirement</td></tr>"
                    + "<tr><td align='left' colspan='2'   >&nbsp;&nbsp;" + special + "</td></tr></table></table>";



                    if (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && chkReturnDetails.Checked && objBooking.BookingReturns.Count > 0)
                    {
                        objBooking = objBooking.BookingReturns[0];


                        bookingNo = objBooking.BookingNo.ToString();
                        fromAddress = objBooking.FromAddress;
                        toAddress = objBooking.ToAddress;
                        fromLocTypeId = objBooking.FromLocTypeId;
                        toLocTypeId = objBooking.ToLocTypeId;

                        if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                        {

                            fromDoorNoFlightNo = objBooking.FromDoorNo.ToStr();

                            if (fromDoorNoFlightNo.ToStr().Trim() != string.Empty)
                                fromTypeDoorNoFlightNo = "Flight No :";


                            fromStreet = objBooking.FromStreet;

                            if (fromStreet.ToStr().Trim() != string.Empty)
                                fromStreetLabel = "Coming From :";

                        }
                        else if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                        {

                            fromDoorNoFlightNo = objBooking.FromDoorNo.ToStr();

                            if (fromDoorNoFlightNo.ToStr().Trim() != string.Empty)
                                fromTypeDoorNoFlightNo = "Door No :";
                        }
                        else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                        {

                            fromDoorNoFlightNo = objBooking.FromDoorNo.ToStr();

                            if (fromDoorNoFlightNo.ToStr().Trim() != string.Empty)
                                fromTypeDoorNoFlightNo = "Door No :";


                            fromStreet = objBooking.FromStreet.ToStr();

                            if (fromStreet.ToStr().Trim() != string.Empty)
                                fromStreetLabel = "Street :";
                        }
                        else
                        {
                            fromTypeDoorNoFlightNo = "";
                            fromStreet = "";
                        }

                        if (toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                        {

                            toDoorNoFlightNo = objBooking.ToDoorNo.ToStr();

                            if (toDoorNoFlightNo.ToStr() != string.Empty)
                                toTypeDoorNoFlightNo = "Flight No :";

                            toStreet = objBooking.ToStreet;
                        }
                        else if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                        {

                            toDoorNoFlightNo = objBooking.ToDoorNo.ToStr();

                            if (toDoorNoFlightNo.ToStr() != string.Empty)
                                toTypeDoorNoFlightNo = "Door No :";
                            //toStreet = objBooking.ToDoorNo.ToString();
                        }
                        else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                        {

                            toDoorNoFlightNo = objBooking.ToDoorNo.ToStr();

                            if (toDoorNoFlightNo.ToStr().Trim() != string.Empty)
                                toTypeDoorNoFlightNo = "Door No :";


                            toStreet = objBooking.ToStreet.ToStr();

                            if (toStreet.ToStr().Trim() != string.Empty)
                                toStreetLabel = "Street :";
                        }
                        else
                        {
                            toTypeDoorNoFlightNo = "";
                            toStreet = "";
                        }

                        vehicle = objBooking.Fleet_VehicleType.VehicleType.ToString();

                        // Return Booking Details
                        //  body += "<table border='1' style='border-style: solid; border-color: #BDC8FF; height: 360px;' cellpadding='0' cellspacing='0' width='95%' align='center'><tr><td colspan='7'  align='center'>
                        //<tr style='font-size: small'><td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; background-repeat: no-repeat;background-position: center top; background-color: #4d8fef; font-weight: bold;font-size: 18px; font-style: normal; line-height: normal; font-variant: normal;text-transform: none; color: White; text-decoration: none;'>Return Journey Details</td></tr>

                        //<tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>Booking Reference:TX74866/1</p><p style='font-weight: bold'></p></td></tr><tr style='font-size: small'><td colspan='2' align='center'>" +
                        //        "<table width='99%' border='1' style='border-style: solid;'><tr><td style='background-color: #09a1c4;color:White' align='left'><b>From</b></td><td style='background-color: #09a1c4;color:White' align='left'><b>To</b></td></tr><tr><td>GSDDSFDSSDSDSDSSS HA2 0DU</td><td>SUDBURY HEIGHTS AVENUE GREENFORD UB6 0LY</td></tr></table></td></tr><br/></td></tr><tr valign='top'><td style='background-color: #e8e8e8; height:48px; width:50%' ><table border='0' cellspacing='0' cellpadding='0'  style='height: 354px; width: 100%'><tr height='40'><td align='center' style='height: 50px; background-color:#09a1c4' colspan='2'><span style='color:White'><b>Journey Detail</b></span></td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Pickup Point</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;From : </td><td align='left'>  GSDDSFDSSDSDSDSSS HA2 0DU </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>      </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>    </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Via Point(s)</td></tr><tr><td align='center' style='width:30%'></td><td align='left'>  1. HEATHROW TERMINAL 4, TW6 2GA</br>2. THE GREEN TWICKENHAM TW2 5AA</br>3. FRAMPTON ROAD HOUNSLOW TW4 5AD </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Destination</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;To : </td><td align='left'>  SUDBURY HEIGHTS AVENUE GREENFORD UB6 0LY</td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>      </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>    </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'></td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;Vehicle Type:</td><td align='left'>     Saloon</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;Fares:</td><td align='left'>     £ 7.00 </td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;Return Fares:</td><td align='left'>     £ 10.00 </td></tr><tr><td align='left' style:width:30%>&nbsp;&nbsp;Payment Type : </td><td>Cash</td></tr></table></td><td style='background-color: #e8e8e8; height:48px' ><table width='100%' border='0' cellspacing='0' cellpadding='0' style='height: 357px'><tr height='40'><td align='center' style='height: 50px; background-color: #09a1c4' colspan='7'><span style='color:White'><b>Your Detail</b></span></td></tr><tr><td align='right' bgcolor='#B5BCCD' colspan='7'></tr><tr ><td align='left' style='width:30%'>          &nbsp;&nbsp;Pickup Date :&nbsp;</td><td>12/10/2015 12:35 </td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Name :&nbsp;</td><td>GDF </td></tr><tr><td align='left' >&nbsp;&nbsp;Mobile Phone :&nbsp;</td> <td>43242</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Telephone No :&nbsp;</td><td></td></tr><tr><td align='left'  colspan='7'>&nbsp;&nbsp;Passenger :&nbsp;0 &nbsp;&nbsp;&nbsp;  Luggage : &nbsp;0&nbsp;&nbsp;&nbsp;</td></tr><tr><td style='width:40%' >&nbsp;&nbsp;</td></tr><tr><td align='left'   bgcolor='#B5BCCD' colspan='7' >&nbsp;&nbsp;Special Requirement</td></tr><tr><td align='left'  style='width:40%'  >&nbsp;&nbsp;dfhgiodfhgiodfhgiodh giodh gi</td></tr></table></table>";


                        special = objBooking.SpecialRequirements.ToStr().Trim();


                        if (special.Length > 0)
                        {
                            if (special.Length > 70)
                            {

                                special.Insert(69, "</br>&nbsp;");
                                if (special.Length > 140)
                                    special.Insert(139, "</br>&nbsp;");



                                if (special.Length > 210)
                                    special.Insert(209, "</br>&nbsp;");


                                if (special.Length > 280)
                                    special.Insert(279, "</br>&nbsp;");

                                if (special.Length > 350)
                                    special.Insert(349, "</br>&nbsp;");
                            }
                        }


                        body += "</br></br><table border='1' style='border-style: solid; border-color: #BDC8FF; height: 360px;' cellpadding='0' cellspacing='0' width='95%' align='center'><tr><td colspan='7'  align='center'>" +
                        "<tr style='font-size: small'><td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; background-repeat: no-repeat;background-position: center top; background-color: #4d8fef; font-weight: bold;font-size: 18px; font-style: normal; line-height: normal; font-variant: normal;text-transform: none; color: White; text-decoration: none;'>Return Journey Details</td></tr>" +
                        "<tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>Booking Reference:" + bookingNo + "</p></td></tr>" +
                "<tr style='font-size: small'><td colspan='2' align='center'><table width='99%' border='1' style='border-style: solid;'><tr><td style='background-color: #09a1c4;color:White;width:50%;' align='left'>"
                + "<b>From</b></td><td style='background-color: #09a1c4;color:White;width:50%;' align='left'><b>To</b></td></tr><tr><td style='width:50%;'>"
                + fromAddress + "</td><td style='width:50%;'>" + toAddress + "</td></tr></table></td></tr><br/></td></tr><tr valign='top'>"
                + "<td style='background-color: #e8e8e8; height:48px; width:50%' ><table border='0' cellspacing='0' cellpadding='0'  style='height: 354px; width: 100%'><tr height='40'><td align='center' style='height: 50px; background-color:#09a1c4' colspan='2'><span style='color:White'>"
                + "<b>Journey Detail</b></span></td></tr>" +

                "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Pickup Point</td></tr>" +
                "<tr><td align='left' style='width:30%'>"
                + "&nbsp;&nbsp;From : </td><td align='left'>  " + fromAddress + " </td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;" + fromTypeDoorNoFlightNo + " </td><td align='left'>     " + fromDoorNoFlightNo + " </td></tr><tr>"
                + "<td style='width:30%' align='left'>&nbsp;&nbsp;" + fromStreetLabel + " </td><td align='left'>    " + fromStreet + "</td></tr>";


                        if (objBooking.Booking_ViaLocations.Count > 0)
                        {
                            int cnt = 1;

                            body += "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Via Point(s)</td></tr>" +
                        "<tr><td align='center' style='width:30%'>"
                        + "</td><td align='left'>  " + string.Join("</br>", objBooking.Booking_ViaLocations.Select(args => cnt++.ToStr() + ". " + args.ViaLocValue).ToArray<string>()) + " </td></tr>"
                              ;

                        }


                        body += "<tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Destination</td></tr><tr><td style='width:30%' align='left'>"
                        + "&nbsp;&nbsp;To : </td><td align='left'>  " + toAddress + "</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;"
                        + toTypeDoorNoFlightNo + " </td><td align='left'>     " + toDoorNoFlightNo + " </td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;"
                        + toStreetLabel + " </td><td align='left'>    " + toStreet + "</td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'></td></tr><tr><td style='width:30%' align='left'>"
                        + "&nbsp;&nbsp;Vehicle Type:</td><td align='left'>     " + vehicle + "</td></tr>" +
                        "<tr><td style='width:30%' align='left'>" + "&nbsp;&nbsp;Fares:</td><td align='left'>     £ " + returnPrice + " </td></tr>" +

                        "<tr><td align='left' style:width:30%>"
                        + "&nbsp;&nbsp;Payment Type : </td><td>" + objBooking.Gen_PaymentType.PaymentType.ToString() + "</td></tr></table></td><td style='background-color: #e8e8e8; height:48px' ><table width='100%' border='0' cellspacing='0' cellpadding='0' style='height: 357px'><tr height='40'><td align='center' style='height: 50px; background-color: #09a1c4' colspan='7'><span style='color:White'>"
                        + "<b>Your Detail</b></span></td></tr><tr><td align='right' bgcolor='#B5BCCD' colspan='7'></tr><tr ><td align='left' style='width:30%'>          "
                        + "&nbsp;&nbsp;Pickup Date :&nbsp;</td><td>" + string.Format("{0:dd/MM/yyyy HH:mm}", objBooking.PickupDateTime) + " </td></tr>"
                        + "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Name :&nbsp;</td><td>" + objBooking.CustomerName + " </td></tr>"
                        + (objBooking.CustomerMobileNo.ToStr().Trim() != string.Empty ? "<tr><td align='left' >&nbsp;&nbsp;Mobile Phone :&nbsp;</td> <td>" + objBooking.CustomerMobileNo + "</td></tr>" : "")
                        + (objBooking.CustomerPhoneNo.ToStr().Trim() != string.Empty ? "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Telephone No :&nbsp;</td><td>" + objBooking.CustomerPhoneNo + "</td></tr>" : "")
                        + "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Passenger :&nbsp;</td><td>" + objBooking.NoofPassengers + "</td></tr>"
                        + "<tr><td align='left' style='width:30%'>&nbsp;&nbsp;Luggage :&nbsp;</td><td>" + objBooking.NoofLuggages + "</td></tr>"

                        + "<tr><td style='width:40%' >&nbsp;&nbsp;</td></tr>"
                        + "<tr><td align='left'   bgcolor='#B5BCCD' colspan='7' >&nbsp;&nbsp;Special Requirement</td></tr>"
                        + "<tr><td align='left' colspan='2'  >&nbsp;&nbsp;" + special + "</td></tr></table></table>";


                    }



                    body += "<tr><td colspan='5'><table width='100%' border='0' ><tr style='font-size: small' >" +
                     "<td align='center' style='font-weight: bold'>Thank You for using our service.</td></tr><tr style='font-size: small'><td  style='font-weight: bold'><p><br />Regards,<br />" + header + ",<br />" + this.objSubCompany.TelephoneNo + "<br />" + this.objSubCompany.WebsiteUrl.ToStr() + "</p></td></tr><tr style='font-size: small'>"
                     + "<td  align='center'></td></tr></table></td></tr></td></tr></table>";


                }


                else if (template == "Template6")
                {

                    string Name = string.Empty;

                    //if (IsPickEmail)
                    //{

                    if (ddlEmailToType.Text == "Customer")
                    {
                        if (txtTo.Text.Contains(",") || txtTo.Text.Contains(":"))
                        {

                            List<Customer> customernames = new List<Customer>();

                            char[] delimiterChars = { ',', ':', };
                            string[] emailarray = txtTo.Text.Split(delimiterChars);
                            foreach (string s in emailarray)
                            {

                                var Query = General.GetQueryable<Customer>(null).OrderByDescending(c => c.Id).FirstOrDefault(c => c.Email == s);
                                if (Query == null)
                                {
                                    Name = "";
                                    break;
                                }
                                else
                                {
                                    customernames.Add(Query);
                                }
                            }

                            Name = string.Join(", ", customernames.Select(c => c.Name).ToArray<string>());
                        }

                    }
                    if (ddlEmailToType.Text == "Company")
                    {

                        List<Gen_Company> CompanyName = new List<Gen_Company>();

                        char[] delimiterChars = { ',', ':', };
                        string[] emailarray = txtTo.Text.Split(delimiterChars);
                        foreach (string s in emailarray)
                        {

                            var Query = General.GetObject<Gen_Company>(c => c.Email == s);
                            if (Query == null)
                            {
                                Name = "";
                                break;
                            }
                            else
                            {

                                CompanyName.Add(Query);

                                //CompanyName.AddRange(General.GetQueryable<Gen_Company>(c => c.CompanyName == Query.CompanyName).ToList());

                                //foreach (var item in CompanyName)
                                //{
                                //    Name += item.CompanyName.ToStr();
                                //}

                            }
                        }
                        Name = string.Join(", ", CompanyName.Select(c => c.CompanyName).ToArray<string>());

                    }


                    if (ddlEmailToType.Text == "Driver")
                    {

                        List<Fleet_Driver> DriverNames = new List<Fleet_Driver>();

                        char[] delimiterChars = { ',', ':', };
                        string[] emailarray = txtTo.Text.Split(delimiterChars);
                        foreach (string s in emailarray)
                        {

                            var Query = General.GetObject<Fleet_Driver>(c => c.Email == s);
                            if (Query == null)
                            {
                                Name = "";
                                break;
                            }
                            else
                            {
                                DriverNames.Add(Query);

                                //DriverNames.AddRange(General.GetQueryable<Fleet_Driver>(c => c.DriverName == Query.DriverName).ToList());

                                //foreach (var item in DriverNames)
                                //{
                                //    Name += item.DriverName.ToStr();
                                //}

                            }
                        }

                        Name = string.Join(", ", DriverNames.Select(c => c.DriverName).ToArray<string>());

                    }
                    //    }



                    string emailTo = string.Empty;
                    decimal price = 0.00m;
                    decimal returnPrice = 0.00m;


                    if (ddlEmailToType.SelectedIndex == 0)
                    {
                        emailTo = objBooking.CustomerName.ToStr();
                        price = objBooking.FareRate.ToDecimal();
                        returnPrice = objBooking.ReturnFareRate.ToDecimal();
                    }
                    else if (ddlEmailToType.SelectedIndex == 1)
                    {
                        if (objBooking.CompanyId == null)
                        {
                            ENUtils.ShowMessage("Company is not defined in Booking");
                            return;
                        }

                        emailTo = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();

                        price = objBooking.CompanyPrice.ToDecimal();
                        returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].CompanyPrice.ToDecimal() : 0.00m;
                    }
                    else if (ddlEmailToType.SelectedIndex == 2)
                    {
                        if (objBooking.DriverId != null)
                        {
                            emailTo = objBooking.Fleet_Driver.DefaultIfEmpty().DriverName.ToStr();

                        }
                        else
                        {

                            emailTo = objBooking.CustomerName.ToStr();
                        }

                        // emailTo = objBooking.CustomerName.ToStr();

                        price = objBooking.FareRate.ToDecimal();
                        returnPrice = objBooking.ReturnFareRate.ToDecimal();
                    }



                    StringBuilder StrBld = new StringBuilder();


                    StrBld.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid; background-color:White;font-family: verdana, arial;font-size: 11px;font-weight: normal;color: #000;text-decoration: none;'>");
                    //  StrBld.Append("<tr><td style='text-align: left; padding: 10px 20px 10px 20px; font-size: 16px;font-weight: bold; color: #ef0000; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>Booking Confirmation</td>");

                    string fullCompanyName = this.objSubCompany.CompanyName;


                    if (AppVars.objPolicyConfiguration.DefaultClientId == "StallionTravels")
                    {
                        fullCompanyName += " (Taxis & Minibuses)";
                        StrBld.Append("<td align='center' style='width=100px; border-bottom: #d4e0ee 1px solid; '><img style='width:80px;height=80px;' src='https://7556fc593b86dff9dbbf-be54c067cc795feefe2ab3711fa1fc60.ssl.cf1.rackcdn.com/2920_StallionTravelUKLtd_medium_20140313153729.png'></td> ");
                    }


                    StrBld.Append("<tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 18px;font-weight: normal; color: #6b97c2 align=\"Center\";'><h2 style=\"color:#6b97c2; padding:0; margin:0\">" + fullCompanyName + "</h2><h4 style=\"color:#6b97c2;  padding:0; margin:0\">" + this.objSubCompany.TelephoneNo + "<br><span style=\"color:#6b97c2;font-size:13px\">" + this.objSubCompany.Address + "</span></h4></td></tr><tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 18px;font-weight: normal; color: #6b97c2 ;'><br>");


                    StrBld.Append("Thank You for your Booking. Please check your journey details below.</td></tr><tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 16px;font-weight: bold; color: #000;'></td></tr><tr><td colspan='4'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border-top: #d4e0ee 1px solid;'><tr style='background-color: #eff3f9;'>");
                    StrBld.Append("<td width='25%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid;'>");

                    if (Name.ToStr().Trim().Length == 0)
                    {
                        Name = emailTo;

                        //          StrBld.Append("Dear: " + objBooking.CustomerName.ToStr() + "<td width='20%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid;'>ORDER NO:</td></td><td width='30%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid; color: #008000;'>" + objBooking.BookingNo.ToStr() + "</td>");
                    }
                    //else
                    //{

                    StrBld.Append("Dear: " + Name + "<td width='20%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid;'>REF NO:</td></td><td width='30%' style='padding: 10px 5px 10px 5px; font-size: 16px; font-weight: bold;border-right: #d4e0ee 1px solid; color: #008000;'>" + objBooking.BookingNo.ToStr() + "</td>");
                    //      }
                    StrBld.Append("</td></tr></table>");

                    StrBld.Append("</tr>");


                    StrBld.Append("<tr style='background-color: #eff3f9;'><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;'>");

                    StrBld.Append("Traveller Information</td><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; font-size: 12px;'>Carrier Details</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                    StrBld.Append("Passenger:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 25%'>" + objBooking.CustomerName.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                    StrBld.Append("Passenger No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;width: 45%'>" + objBooking.NoofPassengers.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Mobile:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerMobileNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("   </td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + " " + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");



                    //if (objBooking.CustomerMobileNo.ToStr().Trim().Length == 0)
                    //{
                    //    StrBld.Append("Phone:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerPhoneNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    //}
                    //else
                    //{
                    //    StrBld.Append("  </td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + "  " + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");


                    //}


                    StrBld.Append("Phone:</td><td style='padding: 5px; bold; border-bottom: #d4e0ee 1px solid;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerPhoneNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Check-in Luggage:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.NoofLuggages.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Email:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.CustomerEmail.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Vehicle:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + vehicle + "</td></tr>");
                    StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Pickup Date/Time:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + string.Format("{0:dd-MMM-yyyy HH:mm}", objBooking.PickupDateTime) + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");

                    StrBld.Append("Special Ins:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.SpecialRequirements.ToStr() + "</td></tr>");


                    //   StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr valign='top'><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");


                    StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Account:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr()  + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                    StrBld.Append("Order No</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.OrderNo.ToStr() + "</td></tr><tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr valign='top'><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");



                    StrBld.Append("Pick-up Information</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromAddress + "</td></tr>");

                    if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Number:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Coming From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromStreet.ToStr() + "</td></tr>");

                    }
                    else if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromStreet.ToStr() + "</td></tr>");


                    }
                    else
                    {
                     
                         StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objBooking.FromDoorNo.ToStr() + "</td></tr>");
               

                    }
                    StrBld.Append("</table></td><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");
                    StrBld.Append("Drop-off Information</td></tr>");


                    StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>To:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToAddress + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");

                    if (objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {

                        StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToDoorNo + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("To Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToStreet.ToStr() + "</td></tr>");
                    }
                    else
                    {

                      
                        StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToDoorNo + "</td></tr>");
                        

                    }

                    StrBld.Append("</table></td></tr>");


                    if (objBooking.Booking_ViaLocations.Count > 0)
                    {

                        StrBld.Append("<tr><td colspan='4'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>From</td><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>To</td></tr>");

                        int cnt = objBooking.Booking_ViaLocations.Count;


                        for (int i = 0; i < cnt; i++)
                        {
                            if (i == 0)
                            {
                                StrBld.Append("<tr>");
                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.FromAddress.ToStr() + "</td>");

                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                StrBld.Append("</tr>");

                            }
                            else
                            {
                                if (i < cnt)
                                {

                                    StrBld.Append("<tr>");
                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i - 1].ViaLocValue.ToStr() + "</td>");


                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                    StrBld.Append("</tr>");
                                }
                            }


                            if (i + 1 == cnt)
                            {
                                StrBld.Append("<tr>");
                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objBooking.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");

                                StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objBooking.ToAddress.ToStr() + "</td>");
                                StrBld.Append("</tr>");

                            }


                        }





                        StrBld.Append("</table></td></tr>");
                    }


                    StrBld.Append("<tr><td colspan='4'>&nbsp;</td></tr>");


                    //                 <tr><td style='padding: 10px 5px 10px 5px; font-size: 14px; border: #d4e0ee 1px solid;background-color: White; text-decoration: underline; font-weight: bold;'>Meeting Point:</td><td style='padding: 10px 5px 10px 5px; font-size: 11px; border: #d4e0ee 1px solid;background-color: #eff3f9;' colspan='3'>The driver will meet you with a name board displaying the Passenger name at ARRIVALS <span style='color: Green'>05 Minutes</span> after your flight lands (as per your request). You will have a further <span style='color: Green'>35 minutes</span> of Free waiting time, meaning a total Free waiting time allowance of <span style='color: Red'>40 Minutes</span> from the time of landing which also include car park. Please Note thereafter waiting time is chargeable at the rate of <span style='color: Red'>GBP £20p</span> per minute.</td></tr>
                    StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='4' style='padding: 10px 5px 10px 5px; font-size: 18px; border-bottom: #d4e0ee 1px solid;background-color: #eff3f9;'>");

                   
                    if (AppVars.objPolicyConfiguration.PickCommissionDeductionFromJobsTotal.ToBool())
                    {
                        StrBld.Append("GBP Cost: <span style='color: #008000;'>£ " +string.Format("{0:f2}", (price + objBooking.ServiceCharges.ToDecimal()))  + "</span>");
                    }
                    else
                    {
                        StrBld.Append("GBP Cost: <span style='color: #008000;'>£ "+string.Format("{0:f2}", (price ))    + "</span>");

                    }
                    
                    //else
                    //{

                    //}
                    StrBld.Append(" <span style='color: #008000;'>" + objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr() + "</span></td></tr>");

                    


                    if (objBooking.BookingReturns.Count > 0)
                    {

                        Booking objReturns = objBooking.BookingReturns[0];

                        StrBld.Append("<tr style='background-color: #eff3f9;'><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;'>");

                        StrBld.Append("Return Booking Details</td><td colspan='2' style='padding: 5px; text-decoration: underline;border-top: #d4e0ee 1px solid; border-bottom: #d4e0ee 1px solid; font-size: 12px;'></td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                        StrBld.Append("Passenger:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 25%'>" + objReturns.CustomerName.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid; width: 15%'>");
                        StrBld.Append("Passenger No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;width: 45%'>" + objReturns.NoofPassengers.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("Mobile:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.CustomerMobileNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("   </td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + " " + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");



                        //if (objReturns.CustomerMobileNo.ToStr().Trim().Length == 0)
                        //{
                        //    StrBld.Append("Phone:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.CustomerPhoneNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        //}
                        //else
                        //{
                        //    StrBld.Append("  </td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + "  " + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");


                        //}


                        StrBld.Append("Phone:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.CustomerPhoneNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        // StrBld.Append("Phone:</td><td style='padding: 5px; bold; border-bottom: #d4e0ee 1px solid;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objReturns.CustomerPhoneNo.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("Check-in Luggage:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.NoofLuggages.ToInt() + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("Email:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.CustomerEmail.ToStr() + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                        StrBld.Append("Vehicle:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + vehicle + "</td></tr>");
                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Pickup Date/Time:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + string.Format("{0:dd-MMM-yyyy HH:mm}", objReturns.PickupDateTime) + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");

                        StrBld.Append("Special Ins:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.SpecialRequirements.ToStr() + "</td></tr>");

                        //  StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr valign='top'><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");



                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Account:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.Gen_Company.DefaultIfEmpty().CompanyName.ToStr()  + "</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");

                        StrBld.Append("Order No</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.OrderNo.ToStr()+ "</td></tr><tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr valign='top'><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");



                        StrBld.Append("Pick-up Information</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.FromAddress + "</td></tr>");

                        if (objReturns.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        {
                            StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Flight Number:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.FromDoorNo.ToStr() + "</td></tr>");
                            StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Coming From:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.FromStreet.ToStr() + "</td></tr>");

                        }
                        else if (objReturns.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {
                            StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.FromDoorNo.ToStr() + "</td></tr>");
                            StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>From Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.FromStreet.ToStr() + "</td></tr>");


                        }
                        else
                        {
                            

                                StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>Door #:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>" + objReturns.FromDoorNo.ToStr() + "</td></tr>");
                            

                        }
                        StrBld.Append("</table></td><td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline;border-bottom: #d4e0ee 1px solid; font-size: 12px;' colspan='2'>");
                        StrBld.Append("Drop-off Information</td></tr>");


                        StrBld.Append("<tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>To:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.ToAddress + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");

                        if (objReturns.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {

                            StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.ToDoorNo + "</td></tr><tr><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;border-right: #d4e0ee 1px solid;'>");
                            StrBld.Append("To Street:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.ToStreet.ToStr() + "</td></tr>");
                        }
                        else
                        {
                           
                                StrBld.Append("To Door No:</td><td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.ToDoorNo + "</td></tr>");
                           

                        }

                        StrBld.Append("</table></td></tr>");


                        if (objReturns.Booking_ViaLocations.Count > 0)
                        {

                            StrBld.Append("<tr><td colspan='4'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='border: #d4e0ee 1px solid;background-color: White; font-family: verdana, arial; font-size: 11px; font-weight: normal;color: #000; text-decoration: none;'><tr style='background-color: #eff3f9;'><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>From</td><td style='padding: 5px; text-decoration: underline; border-bottom: #d4e0ee 1px solid;font-size: 12px; width: 50%' align='center'>To</td></tr>");

                            int cnt = objReturns.Booking_ViaLocations.Count;


                            for (int i = 0; i < cnt; i++)
                            {
                                if (i == 0)
                                {
                                    StrBld.Append("<tr>");
                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objReturns.FromAddress.ToStr() + "</td>");

                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                    StrBld.Append("</tr>");

                                }
                                else
                                {
                                    if (i < cnt)
                                    {

                                        StrBld.Append("<tr>");
                                        StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objReturns.Booking_ViaLocations[i - 1].ViaLocValue.ToStr() + "</td>");


                                        StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");
                                        StrBld.Append("</tr>");
                                    }
                                }


                                if (i + 1 == cnt)
                                {
                                    StrBld.Append("<tr>");
                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid; border-right: #d4e0ee 1px solid;'>" + objReturns.Booking_ViaLocations[i].ViaLocValue.ToStr() + "</td>");

                                    StrBld.Append("<td style='padding: 5px; border-bottom: #d4e0ee 1px solid;'>" + objReturns.ToAddress.ToStr() + "</td>");
                                    StrBld.Append("</tr>");

                                }

                            }



                            StrBld.Append("</table></td></tr>");
                        }


                        StrBld.Append("<tr><td colspan='4'>&nbsp;</td></tr>");


                        //                 <tr><td style='padding: 10px 5px 10px 5px; font-size: 14px; border: #d4e0ee 1px solid;background-color: White; text-decoration: underline; font-weight: bold;'>Meeting Point:</td><td style='padding: 10px 5px 10px 5px; font-size: 11px; border: #d4e0ee 1px solid;background-color: #eff3f9;' colspan='3'>The driver will meet you with a name board displaying the Passenger name at ARRIVALS <span style='color: Green'>05 Minutes</span> after your flight lands (as per your request). You will have a further <span style='color: Green'>35 minutes</span> of Free waiting time, meaning a total Free waiting time allowance of <span style='color: Red'>40 Minutes</span> from the time of landing which also include car park. Please Note thereafter waiting time is chargeable at the rate of <span style='color: Red'>GBP £20p</span> per minute.</td></tr>
                        StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='4' style='padding: 10px 5px 10px 5px; font-size: 18px; border-bottom: #d4e0ee 1px solid;background-color: #eff3f9;'>");



                        if (AppVars.objPolicyConfiguration.PickCommissionDeductionFromJobsTotal.ToBool())
                        {
                            StrBld.Append("GBP Cost: <span style='color: #008000;'>£ " + string.Format("{0:f2}", (returnPrice + objReturns.ServiceCharges.ToDecimal())) + "</span>");
                        }
                        else
                        {

                            StrBld.Append("GBP Cost: <span style='color: #008000;'>£ " +string.Format("{0:f2}", (returnPrice ))   + "</span>");
                        }
                       
                        StrBld.Append(" <span style='color: #008000;'>" + objReturns.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr() + "</span></td></tr>");




                    }








                    StrBld.Append("<tr><td colspan='4' style='border-bottom: #d4e0ee 1px solid;'>&nbsp;</td></tr><tr><td colspan='4' style='padding: 10px 5px 10px 5px; font-weight: bold; font-size: 17px;text-align: center; border-bottom: #d4e0ee 1px solid; line-height:10px'><p style=\"color:#6b97c2\"><strong>Please check the confirmation carefully…</strong></p></td></tr>");

                    StrBld.Append("<tr><td colspan='4' style='text-align: center; padding: 5px 0px 5px 0px; font-size: 18px;font-weight: normal; color: #6b97c2;' ><p align=\"Center\">We welcome all comments on the services that we provide.</p></td></tr>");





                    StrBld.Append("</table>");



                    body = StrBld.ToStr();



                }
                else if (template == "Template7")
                {

                    string Name = string.Empty;

                    //if (IsPickEmail)
                    //{

                    if (ddlEmailToType.Text == "Customer")
                    {
                        if (txtTo.Text.Contains(",") || txtTo.Text.Contains(":"))
                        {
                            List<Customer> customernames = new List<Customer>();

                            char[] delimiterChars = { ',', ':', };
                            string[] emailarray = txtTo.Text.Split(delimiterChars);
                            foreach (string s in emailarray)
                            {

                                var Query = General.GetQueryable<Customer>(null).OrderByDescending(c => c.Id).FirstOrDefault(c => c.Email == s);
                                if (Query == null)
                                {
                                    Name = "";
                                    break;
                                }
                                else
                                {
                                    customernames.Add(Query);
                                }
                            }

                            Name = string.Join(", ", customernames.Select(c => c.Name).ToArray<string>());
                        }


                    }
                    if (ddlEmailToType.Text == "Company")
                    {

                        List<Gen_Company> CompanyName = new List<Gen_Company>();

                        char[] delimiterChars = { ',', ':', };
                        string[] emailarray = txtTo.Text.Split(delimiterChars);
                        foreach (string s in emailarray)
                        {

                            var Query = General.GetObject<Gen_Company>(c => c.Email == s);
                            if (Query == null)
                            {
                                Name = "";
                                break;
                            }
                            else
                            {

                                CompanyName.Add(Query);

                                //CompanyName.AddRange(General.GetQueryable<Gen_Company>(c => c.CompanyName == Query.CompanyName).ToList());

                                //foreach (var item in CompanyName)
                                //{
                                //    Name += item.CompanyName.ToStr();
                                //}

                            }
                        }
                        Name = string.Join(", ", CompanyName.Select(c => c.CompanyName).ToArray<string>());

                    }


                    if (ddlEmailToType.Text == "Driver")
                    {

                        List<Fleet_Driver> DriverNames = new List<Fleet_Driver>();

                        char[] delimiterChars = { ',', ':', };
                        string[] emailarray = txtTo.Text.Split(delimiterChars);
                        foreach (string s in emailarray)
                        {

                            var Query = General.GetObject<Fleet_Driver>(c => c.Email == s);
                            if (Query == null)
                            {
                                Name = "";
                                break;
                            }
                            else
                            {
                                DriverNames.Add(Query);

                                //DriverNames.AddRange(General.GetQueryable<Fleet_Driver>(c => c.DriverName == Query.DriverName).ToList());

                                //foreach (var item in DriverNames)
                                //{
                                //    Name += item.DriverName.ToStr();
                                //}

                            }
                        }

                        Name = string.Join(", ", DriverNames.Select(c => c.DriverName).ToArray<string>());

                    }
                    //    }



                    string emailTo = string.Empty;
                    decimal price = objBooking.FareRate.ToDecimal();
                    decimal returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].FareRate.ToDecimal() : 0.00m;
                    // decimal parkingCharges = objBooking.CongtionCharges.ToDecimal();

                    // decimal rtnparkingCharges = objBooking.CongtionCharges.ToDecimal();

                    emailTo = objBooking.CustomerName.ToStr();

                    //                    if (ddlEmailToType.SelectedIndex == 0)
                    //                    {

                    //                      //  price = objBooking.CustomerPrice.ToDecimal();
                    //                      //  returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].CustomerPrice.ToDecimal() : 0.00m;
                    //                    }
                    //                    else if (ddlEmailToType.SelectedIndex == 1)
                    //                    {
                    //                        if (objBooking.CompanyId == null)
                    //                        {
                    //                            ENUtils.ShowMessage("Company is not defined in Booking");
                    //                            return;
                    //                        }

                    //                      //  emailTo = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                    ////
                    //                      //  price = objBooking.CompanyPrice.ToDecimal();
                    //                      //  returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].CompanyPrice.ToDecimal() : 0.00m;
                    //                   //     parkingCharges = objBooking.ParkingCharges.ToDecimal();
                    //                     //   rtnparkingCharges =objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].ParkingCharges.ToDecimal():0.00m;
                    //                    }
                    //                    else if (ddlEmailToType.SelectedIndex == 2)
                    //                    {
                    //                        if (objBooking.DriverId != null)
                    //                        {
                    //                            emailTo = objBooking.Fleet_Driver.DefaultIfEmpty().DriverName.ToStr();

                    //                        }
                    //                        else
                    //                        {

                    //                            emailTo = objBooking.CustomerName.ToStr();
                    //                        }

                    //                        // emailTo = objBooking.CustomerName.ToStr();

                    //                        price = objBooking.FareRate.ToDecimal();
                    //                        returnPrice = objBooking.ReturnFareRate.ToDecimal();
                    //                    }





                    var objSubcompany = objBooking.Gen_SubCompany.DefaultIfEmpty();

                    StringBuilder sb = new StringBuilder();
                    if (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY || objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.WAITANDRETURN)
                    {

                        decimal total = price;


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
                        sb.Append("Hi " + emailTo);// customername
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td colspan=\"2\">");
                        sb.Append("Thank you for booking with ");
                        sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname

                        sb.Append(", please find your booking confirmation " +
                                "below. If you have any questions in relation to your booking, please call us on " +
                            //            objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno
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
                        sb.Append(objBooking.BookingNo.ToStr());  //bookingno
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Pickup Date");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objBooking.PickupDateTime));//pickupdatetime
                        sb.Append("</td>");



                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Passenger");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.CustomerName.ToStr().Trim());  //customername
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Phone");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append((objBooking.CustomerMobileNo.ToStr().Length > 0 ? objBooking.CustomerMobileNo.ToStr() : objBooking.CustomerPhoneNo.ToStr())); //customerphoneno
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Car Type");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                        sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + objBooking.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + objBooking.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + objBooking.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Pickup");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //   sb.Append(objBooking.FromAddress.ToStr().Trim()); //fromaddress

                        sb.Append(objBooking.FromDoorNo.ToStr().Trim().Length > 0 ? objBooking.FromDoorNo.ToStr() + "," + objBooking.FromAddress.ToStr().Trim() : objBooking.FromAddress.ToStr().Trim()); //fromaddress

                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Destination");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.ToAddress.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        if (objBooking.ViaString.ToStr().Length > 0)
                        {

                            //
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Via");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            sb.Append(objBooking.ViaString.ToStr().Trim()); //toaddress
                            sb.Append("</td>");
                            sb.Append("</tr>");

                        }

                        //


                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Fares");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append("£" + string.Format("{0:f2}", price));
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
                        if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objBooking.FromLocId != null)
                        {

                            extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == objBooking.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
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

                        if (objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objBooking.ToLocId != null)
                        {

                            extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == objBooking.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
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



                        decimal surchargeRate = 0.00m;

                        if (objBooking.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                        {
                            surchargeRate = (price * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;


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
                        sb.Append(string.Format("{0:f2}", objBooking.ServiceCharges.ToDecimal()));//Service Charge
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        total += objBooking.ServiceCharges.ToDecimal();


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
                        sb.Append("£" + string.Format("{0:f2}", (total) + " (" + objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")"));
                        sb.Append("</td>");
                        sb.Append("</tr>");


                        //decimal serviceCharge = 0.00m;

                        //if (objBooking.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                        //{


                        //    serviceCharge = (price * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;
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

                        //decimal serviceCharge = 0.00m;

                        //if (objBooking.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                        //{


                        //    serviceCharge = (price * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;
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
                        sb.Append(objBooking.SpecialRequirements.ToStr().Trim());
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






                        body = sb.ToString();
                    }
                    else if (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && objBooking.BookingReturns.Count > 0)
                    {



                        Booking objReturn = objBooking.BookingReturns[0];



                        // Subject += " (Return Journey)";
                        decimal total = price;

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
                        sb.Append("Hi " + emailTo.ToStr().ToUpper());// customername
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td colspan=\"2\">");
                        sb.Append("Thank you for booking with ");
                        sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname

                        sb.Append(", please find your booking confirmation " +
                                "below. If you have any questions in relation to your booking, please call us on " +
                            //          objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno
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
                        sb.Append(objBooking.BookingNo.ToStr());  //bookingno
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Pickup Date");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objBooking.PickupDateTime));//pickupdatetime
                        sb.Append("</td>");



                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Passenger");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.CustomerName.ToStr().Trim());  //customername
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Phone");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append((objBooking.CustomerMobileNo.ToStr().Length > 0 ? objBooking.CustomerMobileNo.ToStr() : objBooking.CustomerPhoneNo.ToStr())); //customerphoneno
                        sb.Append("</td>");
                        sb.Append("</tr>");




                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Car Type");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //  sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim()));
                        sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + objBooking.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + objBooking.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + objBooking.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno

                        sb.Append("</td>");
                        sb.Append("</tr>");




                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("From");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //   sb.Append(objBooking.FromAddress.ToStr().Trim()); //fromaddress
                        sb.Append(objBooking.FromDoorNo.ToStr().Trim().Length > 0 ? objBooking.FromDoorNo.ToStr() + "," + objBooking.FromAddress.ToStr().Trim() : objBooking.FromAddress.ToStr().Trim()); //fromaddress

                        sb.Append("</td>");
                        sb.Append("</tr>");



                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Destination");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.ToAddress.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        if (objBooking.ViaString.ToStr().Length > 0)
                        {

                            //
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Via");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            sb.Append(objBooking.ViaString.ToStr().Trim()); //toaddress
                            sb.Append("</td>");
                            sb.Append("</tr>");

                        }







                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Fares");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append("£" + string.Format("{0:f2}", price));
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

                        if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objBooking.FromLocId != null)
                        {

                            extraPickup = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == objBooking.FromLocId).DefaultIfEmpty().Charges.ToDecimal();
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

                        if (objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objBooking.ToLocId != null)
                        {

                            extraPickup = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == objBooking.ToLocId).DefaultIfEmpty().Charges.ToDecimal();
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

                        if (objBooking.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                        {
                            surchargeRate = (price * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;


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
                        sb.Append(string.Format("{0:f2}", objBooking.ServiceCharges.ToDecimal()));//Service Charge
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        total += objBooking.ServiceCharges.ToDecimal();


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
                        sb.Append("£" + string.Format("{0:f2}", (total) + " (" + objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")"));
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        //decimal serviceCharge = 0.00m;

                        //if (objBooking.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                        //{


                        //    serviceCharge = (price * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;
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
                        sb.Append(objBooking.SpecialRequirements.ToStr().Trim());
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

                        total = returnPrice;

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
                        // sb.Append((objReturn.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim()));
                        sb.Append((objReturn.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + objReturn.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + objReturn.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + objReturn.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno
                        sb.Append("</td>");
                        sb.Append("</tr>");





                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Pickup");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objReturn.FromDoorNo.ToStr().Trim().Length > 0 ? objReturn.FromDoorNo.ToStr() + "," + objReturn.FromAddress.ToStr().Trim() : objReturn.FromAddress.ToStr().Trim()); //fromaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Destination");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //  sb.Append(objReturn.ToAddress.ToStr().Trim()); //toaddress
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
                        sb.Append("£" + string.Format("{0:f2}", returnPrice));
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Parking Charges");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        //sb.Append("£" + string.Format("{0:f2}", rtnparkingCharges));
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



                        surchargeRate = 0.00m;

                        if (objReturn.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                        {
                            surchargeRate = (returnPrice * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;


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

                        total += objReturn.ServiceCharges.ToDecimal();



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
                        sb.Append("£" + string.Format("{0:f2}", (total) + " (" + objReturn.Gen_PaymentType.DefaultIfEmpty().PaymentType + ")"));
                        sb.Append("</td>");
                        sb.Append("</tr>");


                        //serviceCharge = 0.00m;

                        //if (objReturn.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal() > 0)
                        //{


                        //    serviceCharge = (returnPrice * AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal()) / 100;
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
                        sb.Append(objReturn.SpecialRequirements.ToStr().Trim());
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td colspan=\"2\">");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        sb.Append("</tr>");


                        body = sb.ToString();

                    }



                }

             //Template8

                else if (template == "Template8")
                {

                    string Name = string.Empty;
                    string Company = string.Empty;
                    int CompanyId = objBooking.CompanyId.ToInt();
                    string emailTo = string.Empty;
                    int DriverId = objBooking.DriverId.ToInt();

                    decimal price = objBooking.FareRate.ToDecimal();
                    decimal returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].FareRate.ToDecimal() : 0.00m;


                    if (ddlEmailToType.Text == "Customer")
                    {
                        if (txtTo.Text.Contains(",") || txtTo.Text.Contains(":"))
                        {
                            List<Customer> customernames = new List<Customer>();

                            char[] delimiterChars = { ',', ':', };
                            string[] emailarray = txtTo.Text.Split(delimiterChars);
                            foreach (string s in emailarray)
                            {

                                var Query = General.GetQueryable<Customer>(null).OrderByDescending(c => c.Id).FirstOrDefault(c => c.Email == s);
                                if (Query == null)
                                {
                                    Name = "";
                                    break;
                                }
                                else
                                {
                                    customernames.Add(Query);
                                }
                            }

                            Name = string.Join(", ", customernames.Select(c => c.Name).ToArray<string>());
                        }
                        emailTo = objBooking.CustomerName;

                        price = objBooking.FareRate.ToDecimal();
                        returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].ReturnFareRate.ToDecimal() : 0.00m;
                    }
                    if (ddlEmailToType.Text == "Company")
                    {

                        List<Gen_Company> CompanyName = new List<Gen_Company>();

                        char[] delimiterChars = { ',', ':', };
                        string[] emailarray = txtTo.Text.Split(delimiterChars);
                        foreach (string s in emailarray)
                        {

                            var Query = General.GetObject<Gen_Company>(c => c.Email == s);
                            if (Query == null)
                            {
                                Name = "";
                                break;
                            }
                            else
                            {

                                CompanyName.Add(Query);

                                //CompanyName.AddRange(General.GetQueryable<Gen_Company>(c => c.CompanyName == Query.CompanyName).ToList());

                                //foreach (var item in CompanyName)
                                //{
                                //    Name += item.CompanyName.ToStr();
                                //}

                            }
                        }
                        Name = string.Join(", ", CompanyName.Select(c => c.CompanyName).ToArray<string>());


                        if (CompanyId > 0)
                        {
                            var query = General.GetObject<Gen_Company>(c => c.Id == CompanyId);
                            Company = query.CompanyName;
                            emailTo = query.CompanyName;

                        }

                        price = objBooking.CompanyPrice.ToDecimal();
                        returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].CompanyPrice.ToDecimal() : 0.00m;


                    }


                    if (ddlEmailToType.Text == "Driver")
                    {

                        List<Fleet_Driver> DriverNames = new List<Fleet_Driver>();

                        char[] delimiterChars = { ',', ':', };
                        string[] emailarray = txtTo.Text.Split(delimiterChars);
                        foreach (string s in emailarray)
                        {

                            var Query = General.GetObject<Fleet_Driver>(c => c.Email == s);
                            if (Query == null)
                            {
                                Name = "";
                                break;
                            }
                            else
                            {
                                DriverNames.Add(Query);

                                //DriverNames.AddRange(General.GetQueryable<Fleet_Driver>(c => c.DriverName == Query.DriverName).ToList());

                                //foreach (var item in DriverNames)
                                //{
                                //    Name += item.DriverName.ToStr();
                                //}

                            }
                        }

                        Name = string.Join(", ", DriverNames.Select(c => c.DriverName).ToArray<string>());
                        if (DriverId > 0)
                        {
                            var query = General.GetObject<Fleet_Driver>(c => c.Id == DriverId);
                            emailTo = query.DriverNo;
                        }



                        price = objBooking.FareRate.ToDecimal();
                        returnPrice = objBooking.ReturnFareRate.ToDecimal();

                    }
                    //    }




                    // decimal parkingCharges = objBooking.CongtionCharges.ToDecimal();

                    // decimal rtnparkingCharges = objBooking.CongtionCharges.ToDecimal();




                    if (CompanyId > 0 && string.IsNullOrEmpty(Company))
                    {
                        var query = General.GetObject<Gen_Company>(c => c.Id == CompanyId);
                        Company = query.CompanyName;
                    }

                    //emailTo = objBooking.CustomerName.ToStr();

                    //                    if (ddlEmailToType.SelectedIndex == 0)
                    //                    {

                    //                      //  price = objBooking.CustomerPrice.ToDecimal();
                    //                      //  returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].CustomerPrice.ToDecimal() : 0.00m;
                    //                    }
                    //                    else if (ddlEmailToType.SelectedIndex == 1)
                    //                    {
                    //                        if (objBooking.CompanyId == null)
                    //                        {
                    //                            ENUtils.ShowMessage("Company is not defined in Booking");
                    //                            return;
                    //                        }

                    //                      //  emailTo = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                    ////
                    //                      //  price = objBooking.CompanyPrice.ToDecimal();
                    //                      //  returnPrice = objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].CompanyPrice.ToDecimal() : 0.00m;
                    //                   //     parkingCharges = objBooking.ParkingCharges.ToDecimal();
                    //                     //   rtnparkingCharges =objBooking.BookingReturns.Count > 0 ? objBooking.BookingReturns[0].ParkingCharges.ToDecimal():0.00m;
                    //                    }
                    //                    else if (ddlEmailToType.SelectedIndex == 2)
                    //                    {
                    //                        if (objBooking.DriverId != null)
                    //                        {
                    //                            emailTo = objBooking.Fleet_Driver.DefaultIfEmpty().DriverName.ToStr();

                    //                        }
                    //                        else
                    //                        {

                    //                            emailTo = objBooking.CustomerName.ToStr();
                    //                        }

                    //                        // emailTo = objBooking.CustomerName.ToStr();

                    //                        price = objBooking.FareRate.ToDecimal();
                    //                        returnPrice = objBooking.ReturnFareRate.ToDecimal();
                    //                    }





                    var objSubcompany = objBooking.Gen_SubCompany.DefaultIfEmpty();

                    StringBuilder sb = new StringBuilder();
                    if (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY || objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.WAITANDRETURN)
                    {

                        decimal total = price;


                        sb.Append("<html>");
                        sb.Append("<body>");
                        sb.Append("<table style=\"width:850px;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"1\">");
                        sb.Append("<tr>");

                        sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                        sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td colspan=\"2\">");
                        //sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
                        //         "with Driver directly as it is illegal and not covered by insurance.");
                        //sb.Append("</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");


                        //sb.Append("<td colspan=\"2\">");
                        //sb.Append("&nbsp;");
                        //sb.Append("</td>");
                        //sb.Append("</tr>");
                        if (ddlEmailToType.Text == "Driver")
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                            //sb.Append("Hi " + emailTo);// customername
                            sb.Append("Driver: " + emailTo);// customername
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                            sb.Append("Hi " + emailTo);// customername
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }

                        //Hide Content from Driver 
                        if (ddlEmailToType.Text != "Driver")
                        {
                            sb.Append("<tr>");
                            sb.Append("<td colspan=\"2\">");

                            sb.Append("Thank you for booking with ");
                            sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname 

                            sb.Append(", please find your booking confirmation " +
                                    "below. If you have any questions in relation to your booking, please call us on " +
                                //            objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno
                           "outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("<tr>");
                        sb.Append("<td colspan=\"2\">");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                        //sb.Append("Booking Reference");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                        //sb.Append(objBooking.BookingNo.ToStr());  //bookingno
                        //sb.Append("</td>");
                        //sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Account Booking");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(Company);//pickupdatetime
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Booking for");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objBooking.PickupDateTime));//pickupdatetime
                        sb.Append(string.Format("{0:HH:mm}", objBooking.PickupDateTime));

                        sb.Append(" on ");

                        DateTime? dtBooking = objBooking.PickupDateTime.ToDateTimeorNull();
                        sb.Append(string.Format("{0:dddd dd}{1} {0:MMMM yyyy}", dtBooking, ((dtBooking.Value.Day % 10 == 1 && dtBooking.Value.Day != 11) ? "st"
                        : (dtBooking.Value.Day % 10 == 2 && dtBooking.Value.Day != 12) ? "nd"
                        : (dtBooking.Value.Day % 10 == 3 && dtBooking.Value.Day != 13) ? "rd" : "th")));

                        //sb.Append(string.Format("{0:dddd dd MMMM, yyyy}", objBooking.PickupDateTime));
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Passenger");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.CustomerName.ToStr().Trim());  //customername
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Contact No");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append((objBooking.CustomerMobileNo.ToStr().Length > 0 ? objBooking.CustomerMobileNo.ToStr() : objBooking.CustomerPhoneNo.ToStr())); //customerphoneno
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Car Type");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        ////   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                        //sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + objBooking.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + objBooking.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + objBooking.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno
                        //sb.Append("</td>");
                        //sb.Append("</tr>");



                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("From");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //   sb.Append(objBooking.FromAddress.ToStr().Trim()); //fromaddress

                        sb.Append(objBooking.FromDoorNo.ToStr().Trim().Length > 0 ? objBooking.FromDoorNo.ToStr() + "," + objBooking.FromAddress.ToStr().Trim() : objBooking.FromAddress.ToStr().Trim()); //fromaddress

                        sb.Append("</td>");
                        sb.Append("</tr>");
                        if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        {

                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Flight No");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            //   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                            sb.Append((objBooking.FromDoorNo)); //customerphoneno
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Coming From ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            //   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                            sb.Append((objBooking.FromStreet)); //customerphoneno
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("To ");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.ToAddress.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        //

                        if (objBooking.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.BANK_ACCOUNT && objBooking.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.CREDIT_CARD)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Fares ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            sb.Append("£" + string.Format("{0:f2}", price));
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }


                        if (objBooking.ViaString.ToStr().Length > 0)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Via");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            var viaPoint = objBooking.Booking_ViaLocations.Select(a => a.ViaLocValue).ToList();

                            string Via = string.Empty;
                            foreach (var item in viaPoint)
                            {
                                if (string.IsNullOrEmpty(Via))
                                {
                                    Via = General.GetPostCode(item);
                                }
                                else
                                {
                                    Via += "," + General.GetPostCode(item);
                                }
                            }
                            sb.Append(Via);
                            //sb.Append(objBooking.ViaString.ToStr().Trim()); //toaddress
                            sb.Append("</td>");
                            sb.Append("</tr>");


                        }

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Special Requirements ");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.SpecialRequirements.ToStr().Trim());
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






                        body = sb.ToString();
                    }
                    else if (objBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN && objBooking.BookingReturns.Count > 0)
                    {



                        Booking objReturn = objBooking.BookingReturns[0];



                        // Subject += " (Return Journey)";
                        decimal total = price;

                        sb.Append("<html>");
                        sb.Append("<body>");
                        sb.Append("<table style=\"width:850px;font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"1\">");
                        sb.Append("<tr>");

                        sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                        sb.Append("<img src='" + objSubcompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td colspan=\"2\">");
                        //sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
                        //         "with Driver directly as it is illegal and not covered by insurance.");
                        //sb.Append("</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");


                        //sb.Append("<td colspan=\"2\">");
                        //sb.Append("&nbsp;");
                        //sb.Append("</td>");
                        //sb.Append("</tr>");
                        if (ddlEmailToType.Text != "Driver")
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                            sb.Append("Hi " + emailTo.ToStr().ToUpper());// customername
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"font-size:medium;background:#f5f5f5\" colspan=\"2\">");
                            sb.Append("Driver " + emailTo.ToStr().ToUpper());// customername
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Account Booking ");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(Company);//pickupdatetime
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        //Hide Content from Driver 
                        if (ddlEmailToType.Text != "Driver")
                        {
                            sb.Append("<tr>");
                            sb.Append("<td colspan=\"2\">");


                            sb.Append("Thank you for booking with ");
                            sb.Append(objSubcompany.CompanyName.ToStr().Trim());  //companyname //Hide from Driver

                            sb.Append(", please find your booking confirmation " +
                                    "below. If you have any questions in relation to your booking, please call us on " +
                                //          objSubcompany.TelephoneNo.ToStr().Trim()); //companyphoneno
                            "outside UK " + (objSubcompany.TelephoneNo.ToStr().Trim().Length > 0 && objSubcompany.TelephoneNo.StartsWith("0") ? "+44 " + objSubcompany.TelephoneNo.ToStr().Substring(1) : objSubcompany.TelephoneNo) + " within UK " + objSubcompany.TelephoneNo.ToStr());


                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("<tr>");
                        sb.Append("<td colspan=\"2\">");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                        //sb.Append("Booking Reference");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                        //sb.Append(objBooking.BookingNo.ToStr());  //bookingno
                        //sb.Append("</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Pickup Date");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        //sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objBooking.PickupDateTime));//pickupdatetime
                        //sb.Append("</td>");
                        //sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Booking for ");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objBooking.PickupDateTime));//pickupdatetime
                        sb.Append(string.Format("{0:HH:mm}", objBooking.PickupDateTime));

                        sb.Append(" on ");

                        DateTime? dtBooking = objBooking.PickupDateTime.ToDateTimeorNull();
                        sb.Append(string.Format("{0:dddd dd}{1} {0:MMMM yyyy}", dtBooking, ((dtBooking.Value.Day % 10 == 1 && dtBooking.Value.Day != 11) ? "st"
                        : (dtBooking.Value.Day % 10 == 2 && dtBooking.Value.Day != 12) ? "nd"
                        : (dtBooking.Value.Day % 10 == 3 && dtBooking.Value.Day != 13) ? "rd" : "th")));

                        //sb.Append(string.Format("{0:dddd dd MMMM, yyyy}", objBooking.PickupDateTime));
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Passenger");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.CustomerName.ToStr().Trim());  //customername
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Contact No");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append((objBooking.CustomerMobileNo.ToStr().Length > 0 ? objBooking.CustomerMobileNo.ToStr() : objBooking.CustomerPhoneNo.ToStr())); //customerphoneno
                        sb.Append("</td>");
                        sb.Append("</tr>");




                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Car Type");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        ////  sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim()));
                        //sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + objBooking.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + objBooking.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + objBooking.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno

                        //sb.Append("</td>");
                        //sb.Append("</tr>");




                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("From");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //   sb.Append(objBooking.FromAddress.ToStr().Trim()); //fromaddress
                        sb.Append(objBooking.FromDoorNo.ToStr().Trim().Length > 0 ? objBooking.FromDoorNo.ToStr() + "," + objBooking.FromAddress.ToStr().Trim() : objBooking.FromAddress.ToStr().Trim()); //fromaddress

                        sb.Append("</td>");
                        sb.Append("</tr>");

                        if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Flight No ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            //   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                            sb.Append((objBooking.FromDoorNo)); //customerphoneno
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Coming From ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            //   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                            sb.Append((objBooking.FromStreet)); //customerphoneno
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }


                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("To");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.ToAddress.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");




                        if (objBooking.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.BANK_ACCOUNT && objBooking.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.CREDIT_CARD)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Fares ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            sb.Append("£" + string.Format("{0:f2}", price));
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }






                        if (objBooking.ViaString.ToStr().Length > 0)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Via");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            var viaPoint = objBooking.Booking_ViaLocations.Select(a => a.ViaLocValue).ToList();

                            string Via = string.Empty;
                            foreach (var item in viaPoint)
                            {
                                if (string.IsNullOrEmpty(Via))
                                {
                                    Via = General.GetPostCode(item);
                                }
                                else
                                {
                                    Via += "," + General.GetPostCode(item);
                                }
                            }
                            sb.Append(Via);
                            //sb.Append(objBooking.ViaString.ToStr().Trim()); //toaddress
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Special Requirements");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objBooking.SpecialRequirements.ToStr().Trim());
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

                        total = returnPrice;

                        sb.Append("<tr>");
                        sb.Append("<td colspan=\"2\">");
                        sb.Append("Return Booking Details: ");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;background:#3c4c87;color:#fff;font-size:16px;\">");
                        //sb.Append("Booking Reference");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;background:#3c4c87;color:#fff;font-size:16px;\">");
                        //sb.Append(objReturn.BookingNo.ToStr());  //bookingno
                        //sb.Append("</td>");
                        //sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Pickup Date");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        //sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objReturn.PickupDateTime));//pickupdatetime
                        //sb.Append("</td>");
                        //sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("Booking for ");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //sb.Append(string.Format("{0:ddd dd MMM yyyy HH:mm}", objBooking.PickupDateTime));//pickupdatetime
                        sb.Append(string.Format("{0:HH:mm}", objBooking.PickupDateTime));

                        sb.Append(" on ");

                        DateTime? dtReturnBooking = objReturn.PickupDateTime.ToDateTimeorNull();
                        sb.Append(string.Format("{0:dddd dd}{1} {0:MMMM yyyy}", dtReturnBooking, ((dtBooking.Value.Day % 10 == 1 && dtBooking.Value.Day != 11) ? "st"
                        : (dtBooking.Value.Day % 10 == 2 && dtBooking.Value.Day != 12) ? "nd"
                        : (dtBooking.Value.Day % 10 == 3 && dtBooking.Value.Day != 13) ? "rd" : "th")));

                        //sb.Append(string.Format("{0:dddd dd MMMM, yyyy}", objBooking.PickupDateTime));
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
                        sb.Append("Contact No");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append((objReturn.CustomerMobileNo.ToStr().Length > 0 ? objReturn.CustomerMobileNo.ToStr() : objReturn.CustomerPhoneNo.ToStr())); //customerphoneno
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Car Type");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        //// sb.Append((objReturn.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim()));
                        //sb.Append((objReturn.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim() + " (upto " + objReturn.Fleet_VehicleType.NoofPassengers.ToInt() + " Passengers , " + objReturn.Fleet_VehicleType.NoofLuggages.ToInt() + " Luggages , " + objReturn.Fleet_VehicleType.NoofHandLuggages.ToInt() + " Hand Luggages)")); //customerphoneno
                        //sb.Append("</td>");
                        //sb.Append("</tr>");





                        //sb.Append("<tr>");
                        //sb.Append("<td style=\"width:30%;\">");
                        //sb.Append("Pickup");
                        //sb.Append("</td>");
                        //sb.Append("<td style=\"width:70%;\">");
                        //sb.Append(objReturn.FromDoorNo.ToStr().Trim().Length > 0 ? objReturn.FromDoorNo.ToStr() + "," + objReturn.FromAddress.ToStr().Trim() : objReturn.FromAddress.ToStr().Trim()); //fromaddress
                        //sb.Append("</td>");
                        //sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("From");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        sb.Append(objReturn.FromDoorNo.ToStr().Trim().Length > 0 ? objReturn.FromDoorNo.ToStr() + "," + objReturn.FromAddress.ToStr().Trim() : objReturn.FromAddress.ToStr().Trim()); //fromaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        if (objReturn.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Flight No ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            //   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                            sb.Append((objReturn.FromDoorNo)); //customerphoneno
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Coming From ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            //   sb.Append((objBooking.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr().Trim())); //customerphoneno
                            sb.Append((objReturn.FromStreet)); //customerphoneno
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }

                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:30%;\">");
                        sb.Append("To");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:70%;\">");
                        //  sb.Append(objReturn.ToAddress.ToStr().Trim()); //toaddress
                        sb.Append(objReturn.ToAddress.ToStr().Trim()); //toaddress
                        sb.Append("</td>");
                        sb.Append("</tr>");



                        if (objBooking.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.BANK_ACCOUNT && objBooking.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.CREDIT_CARD)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Fares ");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            sb.Append("£" + string.Format("{0:f2}", returnPrice));
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }





                        if (objReturn.ViaString.ToStr().Length > 0)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"width:30%;\">");
                            sb.Append("Via");
                            sb.Append("</td>");
                            sb.Append("<td style=\"width:70%;\">");
                            var viaPoint = objReturn.Booking_ViaLocations.Select(a => a.ViaLocValue).ToList();

                            string Via = string.Empty;
                            foreach (var item in viaPoint)
                            {
                                if (string.IsNullOrEmpty(Via))
                                {
                                    Via = General.GetPostCode(item);
                                }
                                else
                                {
                                    Via += "," + General.GetPostCode(item);
                                }
                            }
                            sb.Append(Via);
                            //sb.Append(objReturn.ViaString.ToStr().Trim()); //toaddress
                            sb.Append("</td>");
                            sb.Append("</tr>");

                        }

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


                        body = sb.ToString();

                    }



                }

                SendEmail(from, to, subject, body);

                General.SaveSentEmail(body, subject, to);

                this.Close();




                try
                {


                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_BookingLog(Id, AppVars.LoginObj.UserName.ToStr(), "Confirmation Email to (" + txtTo.Text.Trim() + ")");
                    }


                    if (ddlEmailToType.SelectedIndex == 0)
                    {

                        BookingBO objBO = new BookingBO();
                        objBO.GetByPrimaryKey(Id);
                        if (objBO.Current != null && string.IsNullOrEmpty(objBO.Current.CustomerEmail.ToStr().Trim()))
                        {
                            objBO.CheckCustomerValidation = false;
                            objBO.CheckDataValidation = false;
                            objBO.Current.CustomerEmail = to;
                            objBO.Save();

                        }
                    }
                }
                catch (Exception ex)
                {


                }

                this.Close();


            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }

        }


        public void SendEmail(string fromAddress, string ToAddress, string subject, string strBody)
        {
            

                string smtpHost = string.Empty;
                string userName = string.Empty;
                string pwd = string.Empty;
                string port = string.Empty;
                bool enableSSL = false;


                string ccEmail = string.Empty;

                //if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr().Trim()=="Ambercarswatford")
                //{
                //    ccEmail = "mussarrat@ambers.co.uk";
                //}

                string name = string.Empty;
                if (objSubCompany != null && !string.IsNullOrEmpty(objSubCompany.SmtpHost.ToStr().Trim()))
                {
                    name = objSubCompany.CompanyName.ToStr().Trim();
                    smtpHost = objSubCompany.SmtpHost.ToStr().Trim();
                    userName = objSubCompany.SmtpUserName.ToStr().Trim();
                    pwd = objSubCompany.SmtpPassword.ToStr().Trim();
                    port = objSubCompany.SmtpPort.ToStr().Trim();
                    enableSSL = objSubCompany.SmtpHasSSL.ToBool();


                    ccEmail = objSubCompany.EmailCC.ToStr().Trim();
                    

                }
                else
                {
                    
                  //  name = AppVars.objPolicyConfiguration.nam.ToStr().Trim();
                    smtpHost = AppVars.objPolicyConfiguration.SmtpHost.ToStr().Trim();
                    userName = AppVars.objPolicyConfiguration.UserName.ToStr().Trim();
                    pwd = AppVars.objPolicyConfiguration.Password.ToStr().Trim();
                    port = AppVars.objPolicyConfiguration.Port.ToStr().Trim();
                    enableSSL = AppVars.objPolicyConfiguration.EnableSSL.ToBool();

                    if(AppVars.objSubCompany.EmailCC.ToStr().Trim()!=string.Empty)
                        ccEmail = AppVars.objSubCompany.EmailCC.ToStr().Trim();

                }


            using (System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
            {
                msg.To.Add(ToAddress);
                msg.From = new System.Net.Mail.MailAddress(fromAddress, name);
                msg.Subject = subject;





                if (ccEmail != string.Empty)
                {
                    msg.CC.Add(ccEmail);
                }



                msg.Body = strBody;


                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;







                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

                client.Credentials = new NetworkCredential(userName, pwd);

                client.Port = Convert.ToInt32(port);
                client.Host = smtpHost;
                client.EnableSsl = enableSSL;

                client.DeliveryMethod = SmtpDeliveryMethod.Network;




                FieldInfo transport = client.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo authModules = transport.GetValue(client).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);

                Array modulesArray = authModules.GetValue(transport.GetValue(client)) as Array;
                modulesArray.SetValue(modulesArray.GetValue(3), 1);





                ServicePointManager.ServerCertificateValidationCallback =
                         delegate (object s, X509Certificate certificate,
                                  X509Chain chain, SslPolicyErrors sslPolicyErrors)
                         { return true;
                         };














                client.Send(msg);




            //    using (System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
            //{
            //    msg.To.Add("faizan@eurosofttech.co.uk");
            //    msg.From = new System.Net.Mail.MailAddress("bookings@molevalleypremier.com", "Test");
            //    msg.Subject = "Test";
            //    //if (ccEmail != string.Empty)
            //    //{
            //    //    msg.CC.Add(ccEmail);
            //    //}
            //    msg.Body = "test";
            //    msg.BodyEncoding = System.Text.Encoding.UTF8;
            //    msg.IsBodyHtml = true;
            //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //    client.Credentials = new NetworkCredential("bookings@molevalleypremier.com", "premier1988!");
            //    client.Port = Convert.ToInt32(587);
            //    client.Host = "popmail.bta.com";
            //    client.EnableSsl = true;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    FieldInfo transport = client.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
            //    FieldInfo authModules = transport.GetValue(client).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);
            //    Array modulesArray = authModules.GetValue(transport.GetValue(client)) as Array;
            //    modulesArray.SetValue(modulesArray.GetValue(3), 1);
            //    ServicePointManager.ServerCertificateValidationCallback =
            //             delegate (object s, X509Certificate certificate,
            //                      X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //             { return true; };
            //    client.Send(msg);
            //}






        }





                RadDesktopAlert desktopAlert = new Telerik.WinControls.UI.RadDesktopAlert();
                desktopAlert.CaptionText = "Your email has been sent";
                desktopAlert.ContentText = subject;
                desktopAlert.ContentImage = Resources.Resource1.message;
                desktopAlert.SoundToPlay = System.Media.SystemSounds.Asterisk;
                desktopAlert.PlaySound = true;
                desktopAlert.FixedSize = new Size(300, 120);
                desktopAlert.Show();
           

        }

        private void btnEditFrom_Click(object sender, EventArgs e)
        {
           // txtFrom.Enabled = true;
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPickEmail_Click(object sender, EventArgs e)
        {
            //try
            //{
               
            
            

            //    var list = (from a in AppVars.BLData.GetQueryable<Customer>(c => c.Email!=null && c.Email!=string.Empty)
            //                select new
            //                {
            //                    Id = a.Id,
            //                    Name=a.Name,
            //                    Email = a.Email,
            //                    Telephone=a.TelephoneNo,
            //                    MobileNo=a.MobileNo,

            //                }).ToList();
            //    object[] obj = General.ShowFormLister(list, "Id");


            //    if (obj != null)
            //    {
            //        txtTo.Text = obj[3].ToString();

            //    }
            //}
            //catch (Exception ex)
            //{

            //    ENUtils.ShowMessage(ex.Message);
            //}

        }

        void item_Click(object sender, EventArgs e)
        {

            try
            {

                RadItem item = (RadItem)sender;

          //      IsPickEmail = true;
                if (item.Text.ToLower() == "customer")
                {

                    //var list = (from a in AppVars.BLData.GetQueryable<Customer>(c => c.Email != null && c.Email != string.Empty)
                    //            select new
                    //            {
                    //                Id = a.Id,
                    //                Name = a.Name,
                    //                Email = a.Email,
                    //                Telephone = a.TelephoneNo,
                    //                MobileNo = a.MobileNo,

                    //            }).ToList();
                    //object[] obj = General.ShowFormLister(list, "Id");

                    var list = (from a in AppVars.BLData.GetQueryable<Customer>(c => c.Email != null && c.Email != string.Empty)
                                select new
                                {
                                    Id = a.Id,
                                    Name = a.Name,
                                    Email = a.Email,
                                    Telephone = a.TelephoneNo,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                   // List<object[]> obj = General.ShowFormMultiLister(list, "Id");

                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");


                    if (obj != null)
                    {
                        txtTo.Text = string.Empty;

                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            //txtTo.Text = obj[3].ToString();
                            txtTo.Text += "," + string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }
                        else
                        {
                            txtTo.Text = string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }

                        ddlEmailToType.SelectedIndex = 0;
                           
                    }
                }
                else if (item.Text.ToLower() == "driver")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(c => c.Email != null && c.Email != string.Empty)
                                select new
                                {
                                    Id = a.Id,
                                    No=a.DriverNo,
                                    Name = a.DriverName,
                                    Email = a.Email,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    //object[] obj = General.ShowFormLister(list, "Id");


                    //if (obj != null)
                    //{
                    //    txtTo.Text = obj[3].ToString();

                    //}

                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");

                    if (obj != null)
                    {
                        txtTo.Text = string.Empty;

                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            //  txtTo.Text = obj[3].ToString();

                            txtTo.Text += "," + string.Join(",", obj.Select(c => c[3].ToString()).ToArray<string>());
                        }
                        else
                        {
                            txtTo.Text = string.Join(",", obj.Select(c => c[3].ToString()).ToArray<string>());
                        }

                        ddlEmailToType.SelectedIndex = 2;

                    }
                }
                else if (item.Text.ToLower() == "company")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Gen_Company>(c => c.Email != null && c.Email != string.Empty)
                                select new
                                {
                                    Id = a.Id,
                                   
                                    Name = a.CompanyName,
                                    Email = a.Email,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    //object[] obj = General.ShowFormLister(list, "Id");


                    //if (obj != null)
                    //{
                    //    txtTo.Text = obj[3].ToString();

                    //}

                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");

                    if (obj != null)
                    {
                        txtTo.Text = string.Empty;

                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            //  txtTo.Text = obj[3].ToString();

                            txtTo.Text += "," + string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }
                        else
                        {
                            txtTo.Text = string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }

                        ddlEmailToType.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}
