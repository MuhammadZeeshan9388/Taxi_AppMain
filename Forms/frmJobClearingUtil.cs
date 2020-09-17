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
using Telerik.WinControls.UI;
using Utils;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmJobClearingUtil : UI.SetupBase
    {
        public frmJobClearingUtil()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmJobClearingUtil_Load);


          


            grdLister.ShowRowHeaderColumn = false;
            lblHeading.Text = "These jobs were expired before " + AppVars.objPolicyConfiguration.ExpiredPDAJobHours.ToInt()
                                    + " Hour(s) and not Cleared for some reason..";

            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.Name = "colChk";
            col.HeaderText = "";
            col.Width=60;
            grdLister.Columns.Add(col);

        }

        private void LoadData()
        {

            //c.PickupDateTime.Value

            int hours = AppVars.objPolicyConfiguration.ExpiredPDAJobHours.ToInt();

            DateTime now = DateTime.Now.AddHours(-hours);

            var list = (from a in General.GetQueryable<Booking>(null).Where(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || c.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                                                               || c.BookingStatusId == Enums.BOOKINGSTATUS.POB || c.BookingStatusId == Enums.BOOKINGSTATUS.STC
                                                               || c.BookingStatusId==Enums.BOOKINGSTATUS.FOJ)
                                                                 //&& c.DriverId != null && DateTime.Now.Subtract(c.PickupDateTime.Value).Hours >= hours)
                                                               && c.DriverId != null && c.PickupDateTime.Value<now)

                        select new
                        {
                            Id = a.Id,
                            RefNo = a.BookingNo,
                            PickupDateTime = a.PickupDateTime,
                            Passenger = a.CustomerName,
                            PickupPoint = a.FromAddress,
                            Destination = a.ToAddress,
                            Driver = a.Fleet_Driver.DriverNo,
                            Status = a.BookingStatus.StatusName

                        }).OrderByDescending(c=>c.PickupDateTime).ToList();


            grdLister.DataSource = list;

            grdLister.Columns["Id"].IsVisible = false;
            grdLister.Columns["PickupDateTime"].HeaderText = "Pickup Date/Time";
            grdLister.Columns["RefNo"].HeaderText = "Ref #";


            grdLister.Columns["RefNo"].Width = 80;
            grdLister.Columns["PickupDateTime"].Width = 140;
            (grdLister.Columns["PickupDateTime"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickupDateTime"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";

            grdLister.Columns["Passenger"].Width = 100;
            grdLister.Columns["PickupPoint"].Width = 170;
            grdLister.Columns["Destination"].Width = 170;
            grdLister.Columns["Driver"].Width = 80;
            grdLister.Columns["Status"].Width = 100;

        }       

        void frmJobClearingUtil_Load(object sender, EventArgs e)
        {


            LoadData();

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes ==MessageBox.Show("Are you sure you want to Clear All Jobs?","Job Clearing",MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {


                    //foreach (var row in grdLister.Rows)
                    //{
                    //    ClearJob(row.Cells["Id"].Value.ToLong());
                    //}


                    string jobIds = string.Join(",", grdLister.Rows.Select(c => c.Cells["Id"].Value.ToStr()).ToArray<string>());

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_RunProcedure("update booking set bookingstatusid=2 where id in("+jobIds+")");


                    }


                    ENUtils.ShowMessage("Jobs Cleared Successfully");
                }


                LoadData();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void ClearJob(long jobId)
        {



            BookingBO obj = new BookingBO();
            obj.GetByPrimaryKey(jobId);
            if (obj.Current != null)
            {
                obj.CheckCustomerValidation = false;
                obj.CheckDataValidation = false;
                obj.DisableUpdateReturnJob = true;
                obj.Current.BookingStatusId = Enums.BOOKINGSTATUS.DISPATCHED;
                obj.Save();
            }


        }

        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var row in grdLister.Rows.Where(c=>c.Cells["colChk"].Value.ToBool()))
                {
                    ClearJob(row.Cells["Id"].Value.ToLong());

                }


                ENUtils.ShowMessage("Jobs Cleared Successfully");
                LoadData();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
    }
}
