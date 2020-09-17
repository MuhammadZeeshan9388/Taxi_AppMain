using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Utils;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class frmDriverJobs : Form
    {


        List<Booking> list;



        public frmDriverJobs(List<Booking> listofDriverJobs,string driverNo)
        {
            InitializeComponent();
            this.list = listofDriverJobs.OrderByDescending(c=>c.PickupDateTime).ToList();
            this.Shown += new EventHandler(frmDriverJobs_Shown);
            this.Text = "Driver " + driverNo + " Jobs";
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            dataGridView1.ReadOnly = true;
          
        }

        public frmDriverJobs(List<Booking> listofCustomerJobs, string CustomerName, String Customer)
        {
            InitializeComponent();
            this.list = listofCustomerJobs.OrderByDescending(c => c.BookingDate).ToList();
            this.Shown += new EventHandler(frmDriverJobs_Shown);
            this.Text = "Customer " + CustomerName + "                                                                                                                                                                                       Total Record(s) :" + listofCustomerJobs.Count;
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            dataGridView1.ReadOnly = true;

        }

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                int Id=  dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToInt();
                if(Id> 0)
                    ShowBookingForm(Id, true);

            }

        }


        private void ShowBookingForm(int id, bool showOnDialog)
        {
            General.ShowBookingForm(id, showOnDialog, "", "", Enums.BOOKING_TYPES.LOCAL);


        }




        void frmDriverJobs_Shown(object sender, EventArgs e)
        {
            try
            {

                if (list != null)
                {

                    dataGridView1.RowCount = list.Count;

                    if (dataGridView1.RowCount < 15)
                    {

                        dataGridView1.RowCount = (15 - dataGridView1.RowCount) + list.Count;

                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["Id"].Value = list[i].Id;
                        dataGridView1.Rows[i].Cells["RefNo"].Value = list[i].BookingNo;
                        dataGridView1.Rows[i].Cells["PickupDateTime"].Value =string.Format("{0:dd/MM/yyyy HH:mm}", list[i].PickupDateTime);
                        dataGridView1.Rows[i].Cells["PickupPoint"].Value = list[i].FromAddress;
                        dataGridView1.Rows[i].Cells["Destination"].Value = list[i].ToAddress;
                        dataGridView1.Rows[i].Cells["Fare"].Value = list[i].FareRate.ToDecimal();


                    }





                }

            }
            catch (Exception ex)
            {



            }


        }
    }
}
