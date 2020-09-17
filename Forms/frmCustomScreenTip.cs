using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Taxi_Model;
using Utils;
using System.Linq;
using System.Data.Linq;

namespace Taxi_AppMain.Forms
{
    public partial class frmCustomScreenTip : Form
    {
        public frmCustomScreenTip(Booking objBooking)
        {
            InitializeComponent();


            if (objBooking != null)
            {

                string via = string.Empty;
                string notes = string.Empty;


                if (objBooking.Booking_ViaLocations.Count > 0)
                {
                    via = "Via Point : " + string.Join(Environment.NewLine + ",", objBooking.Booking_ViaLocations.Select(c => c.ViaLocValue.ToStr()).ToArray<string>())
                                    + "<br><br>";


                }

                if (objBooking.Booking_Notes.Count > 0)
                {
                    int notesCnt = 1;
                    notes = "<br>" + string.Join(Environment.NewLine, objBooking.Booking_Notes.Select(c => "(" + (notesCnt++).ToStr() + "). " + c.notes.ToStr()).ToArray<string>()) + "<br><br>";

                    if (!string.IsNullOrEmpty(notes))
                    {
                        notes = notes.Insert(0, "<u>Notes :</u>");

                    }
                }

                string newLine=Environment.NewLine;

                String text = String.Format("<html><b><span><color=Blue>Ref #  :   {13}"+newLine+"<b>Vehicle : {0}<br>Pickup Time : {1}"+newLine+newLine+"Pickup Point : {2}"+newLine+newLine+"{3}" +
                                       "Destination  :  {4}"+newLine+newLine+"Fare : £ {5}" +
                                   newLine+newLine+"Customer Name : {6}"+newLine+"Telephone No : {7}"+newLine+"Mobile No : {8}"+newLine+newLine+"Accounts : {9}" +
                                   newLine+"Payment Mode : {10}"+newLine+newLine+"Special Requirement : {11}"+newLine+newLine+"Status : {12}"+newLine+newLine +
                                   notes + "Driver : {14}</span></b>",

                     objBooking.Fleet_VehicleType.VehicleType, string.Format("{0:dd/MM/yyyy HH:mm}", objBooking.PickupDateTime), objBooking.FromAddress, via, objBooking.ToAddress, objBooking.FareRate, objBooking.NoofPassengers, objBooking.CustomerPhoneNo.ToStr(), objBooking.CustomerMobileNo.ToStr(), objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr(),
                     objBooking.PaymentTypeId != null ? objBooking.Gen_PaymentType.PaymentType.ToStr() : "", objBooking.SpecialRequirements.ToStr(), objBooking.BookingStatus != null ? objBooking.BookingStatus.StatusName.ToStr() : "", objBooking.BookingNo.ToStr(), objBooking.DriverId == null ? "" : objBooking.Fleet_Driver.DefaultIfEmpty().DriverNo + " - " + objBooking.Fleet_Driver.DefaultIfEmpty().DriverName.ToStr());

                richTextBox1.AddHTML(text);
              //  richTextBox1.Rtf = text;
              //  richTextBox1.Rtf = "<b>ok</b>";
             //   richTextBox1.Text = text;
               
            }
        }

        private KeyEventArgs _LastSendEventArgs;

        public KeyEventArgs LastSendEventArgs
        {
            get { return _LastSendEventArgs; }
            set { _LastSendEventArgs = value; }
        }

      

        private void frmCustomScreenTip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.I && e.KeyCode != Keys.Escape)
            {

                this.LastSendEventArgs = e;
            }

                this.Close();
        }
    }
}
