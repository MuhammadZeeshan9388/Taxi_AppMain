using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Data.Linq;
using Utils;
using Taxi_Model;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class frmTodaysBooking : RadForm
    {
        public frmTodaysBooking()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmTodaysBooking_Load);
            grdPendingJobs.AutoCellFormatting = true;
            grdPendingJobs.AllowAddNewRow = false;
            grdPendingJobs.ShowRowHeaderColumn = false;
            grdPendingJobs.AllowEditRow = false;
            grdPendingJobs.ShowGroupPanel = false;
            grdPendingJobs.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            timer1.Tick += new EventHandler(timer1_Tick);

        }

        void frmTodaysBooking_Load(object sender, EventArgs e)
        {
            LoadTodaysBooking();


            grdPendingJobs.Columns["PickUpDate"].Width = 70;
            grdPendingJobs.Columns["PickUpDate"].HeaderText = "Pickup Date";
            grdPendingJobs.Columns["Time"].Width = 50;

            grdPendingJobs.Columns["PickupDateTempAirport"].IsVisible = false;
            grdPendingJobs.Columns["PickupDateTemp"].IsVisible = false;
            (grdPendingJobs.Columns["PickupDateTemp"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdPendingJobs.Columns["PickupDateTemp"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdPendingJobs.Columns["FromLocTypeId"].IsVisible = false;
            grdPendingJobs.Columns["ToLocTypeId"].IsVisible = false;

            grdPendingJobs.Columns["Status"].Width = 60;
            grdPendingJobs.Columns["StatusId"].IsVisible = false;
            grdPendingJobs.Columns["Via"].IsVisible = false;
            grdPendingJobs.Columns["Id"].IsVisible = false;




            grdPendingJobs.Columns["RefNumber"].HeaderText = "Ref #";
            grdPendingJobs.Columns["RefNumber"].Width = 50;

            grdPendingJobs.Columns["UpdateBy"].Width = 70;
            grdPendingJobs.Columns["UpdateBy"].HeaderText = "Update By";

            grdPendingJobs.Columns["Fare"].HeaderText = "Fare(£)";
            grdPendingJobs.Columns["Fare"].Width = 50;
            grdPendingJobs.Columns["Vehicle"].Width = 60;

            grdPendingJobs.Columns["Passenger"].Width = 60;
            grdPendingJobs.Columns["From"].Width = 120;
            grdPendingJobs.Columns["To"].Width = 120;

            grdPendingJobs.Columns["Account"].Width = 60;
            grdPendingJobs.Columns["Account"].HeaderText = "A/C";

            grdPendingJobs.Columns["From"].HeaderText = "Pickup Point";
            grdPendingJobs.Columns["To"].HeaderText = "Destination";


            grdPendingJobs.Columns["BackgroundColor"].IsVisible = false;
            grdPendingJobs.Columns["TextColor"].IsVisible = false;




            timer1.Start();

         


        }

        void timer1_Tick(object sender, EventArgs e)
        {
            LoadTodaysBooking();
        }


        private void LoadTodaysBooking()
        {
        
            DateTime? dt = DateTime.Now.ToDateorNull();
            DateTime dtTom = dt.Value.AddDays(1);

            var data1 = General.GetQueryable<Booking>(null).OrderByDescending(c => c.PickupDateTime);

            var query = (from a in data1
                       
                         select new
                         {
                             Id = a.Id,
                             RefNumber = a.BookingNo,
                             PickupDateTempAirport = string.Format("{0:" + Convert.ToString(a.FromLocTypeId == 1 || a.ToLocTypeId == 1 ? 1 : 2) + ",dd/MM/yyyy HH:mm}", a.PickupDateTime),
                             PickupDateTemp = a.PickupDateTime,
                             PickUpDate = string.Format("{0:dd/MM/yyyy}", a.PickupDateTime),
                             Time = string.Format("{0:HH:mm}", a.PickupDateTime),
                             Passenger = a.CustomerName,
                             From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                             To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                             Fare = a.FareRate,
                             FromLocTypeId = a.FromLocTypeId,
                             ToLocTypeId = a.ToLocTypeId,
                             Account = a.Gen_Company != null ? a.Gen_Company.CompanyName : "",
                             BackgroundColor = a.Fleet_VehicleType.BackgroundColor,
                             TextColor = a.Fleet_VehicleType.TextColor,
                             Vehicle = a.Fleet_VehicleType.VehicleType,
                             Via = string.Join("|", a.Booking_ViaLocations.Select(c => c.ViaLocValue).ToArray<string>()),
                             UpdateBy = a.EditLog,
                             StatusId = a.BookingStatusId,
                             Status = a.BookingStatus.StatusName,
                             //PopupShown=default(bool)
                         }).ToList();





            grdPendingJobs.DataSource = query.Where(a => (a.PickupDateTemp >= dt && a.PickupDateTemp <= dtTom) 
                                && a.StatusId == Enums.BOOKINGSTATUS.WAITING).OrderBy(c => c.PickupDateTemp).ToList();


        }

        private void btnRefreshJobs_Click(object sender, EventArgs e)
        {
            LoadTodaysBooking();
        }
    }
}
