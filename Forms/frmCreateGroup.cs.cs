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
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Taxi_AppMain.Classes;
using System.Data.Linq;


namespace Taxi_AppMain
{
    public partial class frmCreateGroup : UI.SetupBase
    {

        public   BookingGroupBO objBookingGroupMaster = null;

        public long GroupId;
        public string GroupName;
        public DateTime? FlightDeparture;
        public int? vehicleType;
        public int? Destination;
        public int? NoOfSeats;

        public frmCreateGroup()
        {
            InitializeComponent();
            InitializeConstructor();
            

        }

        public override void DisplayRecord()
        {
            try
            {
                if (objBookingGroupMaster.Current == null) return;



                ddlZone.Enabled = false;

                ddlZone.SelectedValue = objBookingGroupMaster.Current.PickupZoneId;

                num_TotalPassengers.Value = objBookingGroupMaster.Current.NoOfSeats.ToInt();

                numReservedSeats.Visible = true;
                lblReservedSeats.Visible = true;


                numReservedSeats.Value = objBookingGroupMaster.Current.Bookings.Sum(c => c.NoofPassengers).ToInt();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
           



        }

        private void InitializeConstructor()
        {
            objBookingGroupMaster = new BookingGroupBO();
            this.SetProperties((INavigation)objBookingGroupMaster);

            FillCombo();

        }

        private void FillCombo()
        {
            var list = (from a in General.GetQueryable<Gen_ShuttleZone>(null)
                        select new
                        {
                            Id = a.Id,
                            ZoneName = a.ZoneName + " ( " + a.PostCodes + " ) "

                        }).ToList();



            ComboFunctions.FillCombo(list, ddlZone, "ZoneName", "Id");

        }
        
               

        public frmCreateGroup(DateTime? flightDepartureDateTime,int? vehicleTypeId,int? destinationId,string pickup)
        {

            InitializeComponent();

            this.FlightDeparture = flightDepartureDateTime;
            this.vehicleType = vehicleTypeId;
            this.Destination = destinationId;


            InitializeConstructor();

            
        

          // ddlZone.DropDownListElement.TextBox.TextBoxItem. = 0;

           AllocatePickupZone(pickup);
        }


        private void AllocatePickupZone(string pickup)
        {

            if (!string.IsNullOrEmpty(pickup))
            {
                string postcode = General.GetPostCodeMatch(pickup.ToUpper());

                if (postcode.Contains(' '))
                {

                    postcode = postcode.Split(' ')[0];

                }

                postcode = General.CheckIfSpecialPostCode(postcode);

                if (!string.IsNullOrEmpty(postcode))
                {
                    int zoneId = General.GetQueryable<Gen_ShuttleZone_AssociatedPostCode>(c => c.PostCode == postcode).FirstOrDefault().DefaultIfEmpty().ZoneId;


                    if (zoneId != 0)
                    {
                        ddlZone.SelectedValue = zoneId;
                      //  ddlZone.Enabled = false;
                    }


                }


            }

        }


        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
           

            if (ddlZone.SelectedValue == null)
            {

                ENUtils.ShowMessage("Required : Pickup Zone");
                return;
            }

            if (num_TotalPassengers.Value == 0)
            {
                ENUtils.ShowMessage("Required : No of seats");
                return;


            }
           

            SaveGroup();
        }


        private void SaveGroup()
        {

            if (objBookingGroupMaster.PrimaryKeyValue == null)
            {

                objBookingGroupMaster.New();

                objBookingGroupMaster.Current.FlightDepartureDate = this.FlightDeparture;
                objBookingGroupMaster.Current.VehicleTypeId = vehicleType;
                objBookingGroupMaster.Current.DestinationId = Destination;
                objBookingGroupMaster.Current.PickupZoneId = ddlZone.SelectedValue.ToIntorNull();
                objBookingGroupMaster.Current.TripStatusId = Enums.BOOKING_TRIPSTATUS.WAITING;
            }
            else
            {


                if (num_TotalPassengers.Value < numReservedSeats.Value)
                {
                    ENUtils.ShowMessage("No of Seats cannot be less than Reserved Seats");
                    return;
                }



                objBookingGroupMaster.Edit();

            }

        
            objBookingGroupMaster.Current.IsActive = true;
           
            objBookingGroupMaster.Current.NoOfSeats = num_TotalPassengers.Value.ToInt();
           
            objBookingGroupMaster.Save();

            //objBookingGroupMaster.Current.GroupName=ddlVehicleType.Text

            this.GroupId = objBookingGroupMaster.Current.Id;
            this.GroupName = objBookingGroupMaster.Current.GroupName.ToStr();
            this.NoOfSeats = num_TotalPassengers.Value.ToInt();
        
            this.Close();



        }

    }
}
