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
    public partial class frmBookingAudit : Form
    {


        List<Booking_Log> list;



        public frmBookingAudit(List<Booking_Log> listofBookingReport, string BookingId)
        {
            InitializeComponent();
            this.list = listofBookingReport;

            this.Text = "Job Audit Trail";
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            dataGridView1.ReadOnly = true;
          
        }

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                int Id=  dataGridView1.Rows[e.RowIndex].Cells["BookingId"].Value.ToInt();
                if(Id> 0)
                    ShowBookingForm(Id, true);

            }

        }


        private void ShowBookingForm(int id, bool showOnDialog)
        {
            General.ShowBookingForm(id, showOnDialog, "", "", Enums.BOOKING_TYPES.LOCAL);


        }




       
        private void frmBookingAudit_Load(object sender, EventArgs e)
        {
            try
            {

                if (list != null)
                {

                    dataGridView1.RowCount = list.Count;

                    for (int i = 0; i < list.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["BookingId"].Value = list[i].BookingId;
                        dataGridView1.Rows[i].Cells["DateTime"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", list[i].UpdateDate);
                        dataGridView1.Rows[i].Cells["Operator"].Value = list[i].User;
                        dataGridView1.Rows[i].Cells["Description"].Value = list[i].AfterUpdate;


                    }





                }

            }
            catch (Exception ex)
            {



            }

        }
    }
}
