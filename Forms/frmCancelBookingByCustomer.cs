using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_BLL;

namespace Taxi_AppMain.Forms
{
    public partial class frmCancelBookingByCustomer : Form
    {
        public frmCancelBookingByCustomer(long jobId, string title,string customerName,string pickup,string destination,string pickupTime,string note)
        {
            InitializeComponent();

            btnViewJob.Tag = jobId;
            txtCustomer.Text = customerName;
            txtPickupPoint.Text = pickup;
            txtDestination.Text = destination;
            txtTitle.Text = title;

            lblFooter.Text = note;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnViewJob_Click(object sender, EventArgs e)
        {
            General.ShowBookingForm(btnViewJob.Tag.ToInt(), true, "", "", Enums.BOOKING_TYPES.ONLINE);

        }
    }
}
