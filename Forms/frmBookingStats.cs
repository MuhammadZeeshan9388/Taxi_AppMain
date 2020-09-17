using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Telerik.WinControls.UI;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class frmBookingStats : Form
    {

        private Booking objBooking;

        public Booking ObjBooking
        {
            get { return objBooking; }
            set { objBooking = value; }
        }


        private IQueryable<Gen_Zone> listofZones=null;


        public frmBookingStats(long  jobId)
        {
            InitializeComponent();

            this.ObjBooking = General.GetObject<Booking>(c => c.Id == jobId);
            this.Load += new EventHandler(frmBookingStats_Load);
        }

        void frmBookingStats_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            try
            {
                listofZones = General.GetQueryable<Gen_Zone>(null);


                var driversList = (from a in General.GetQueryable<Fleet_Driver_Location>(null).AsEnumerable()
                                   join b in General.GetQueryable<Fleet_DriverQueueList>(null).AsEnumerable() on a.DriverId equals b.DriverId
                                   where b.Status == true
                                   orderby b.DriverWorkStatusId
                                   select new
                                   {
                                       Id = a.DriverId,
                                       DriverNo = a.Fleet_Driver.DriverNo,
                                       DriverName = a.Fleet_Driver.DriverName,
                                       LocationName = a.LocationName,
                                       LocationPostCode = a.LocationPostCode,


                                       Latitude = a.Latitude,
                                       Longitude = a.Longitude,
                                       UpdateDate = a.UpdateDate,
                                       StatusId = b.DriverWorkStatusId
                                   }).ToList();



                grdPickupPlot.RowCount = 0;


                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.HeaderText = this.objBooking.Gen_Zone.ZoneName.ToString();
                col.Name = this.objBooking.Gen_Zone.Id.ToString();
                col.Width = 70;
                grdPickupPlot.Columns.Add(col);



                Gen_Zone pickupZone = this.objBooking.Gen_Zone;

                if (pickupZone != null)
                {



                    var dr = (from a in driversList.Where(c => (c.Latitude >= pickupZone.MinLatitude && c.Latitude <= pickupZone.MaxLatitude)
                                                                && (c.Longitude <= pickupZone.MaxLongitude && c.Longitude >= pickupZone.MinLongitude))

                              select new
                              {

                                  Id = a.Id,

                                  DriverNo = a.DriverNo

                              }).ToList();





                    grdPickupPlot.RowCount = dr.Count;
                    for (int i = 0; i < grdPickupPlot.RowCount; i++)
                    {

                        grdPickupPlot.Rows[i].Cells[pickupZone.Id.ToString()].Value = dr[i].DriverNo.ToString();
                    }

                }





                // Load Nearest Drivers

                string jobAddress = objBooking.FromPostCode.Replace(' ', '+');



                var nearestDrivers = driversList.Select(args => new
                {
                    args.Id,
                    MilesAwayFromPickup = !string.IsNullOrEmpty(jobAddress) ? General.CalculateDistance(General.GetPostCodeMatch(args.LocationName), jobAddress) : 0,
                    args.DriverNo,
                    Latitude = args.Latitude,
                    Longitude = args.Longitude,
                    Location = args.LocationName,
                    StatusId=args.StatusId
                }).OrderBy(args => args.MilesAwayFromPickup).Take(5).ToList();


            }
            catch (Exception ex)
            {


            }

        }


   

        private void grdPickupPlot_Click(object sender, EventArgs e)
        {

        }
    }
}
