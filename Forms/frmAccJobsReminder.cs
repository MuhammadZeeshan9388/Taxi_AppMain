using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmAccJobsReminder : Form
    {
        public frmAccJobsReminder()
        {
            InitializeComponent();


            this.Shown += new EventHandler(frmAccJobsReminder_Shown);
        }

        void frmAccJobsReminder_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (chkDontRemind.Checked)
                {
                    using (Taxi_Model.TaxiDataContext db = new Taxi_Model.TaxiDataContext())
                    {
                        var obj = db.Gen_SysPolicy_Configurations.FirstOrDefault(c => c.SysPolicyId != null);

                        if (obj.AccJobsShowNotificationDay.ToStr().Contains(","))
                        {
                            obj.AccJobsShowNotificationDay = obj.AccJobsShowNotificationDay.ToStr().Remove(obj.AccJobsShowNotificationDay.ToStr().IndexOf(","));
                            obj.AccJobsShowNotificationDay += "," + string.Format("{0:dd/MM/yyyy}", DateTime.Now.ToDate());

                        }
                        else
                        {

                            if (obj.AccJobsShowNotificationDay.ToStr() == string.Empty)
                            {

                                obj.AccJobsShowNotificationDay = DateTime.Now.DayOfWeek.ToStr() + "," + string.Format("{0:dd/MM/yyyy}", DateTime.Now.ToDate());
                            }
                            else
                            {
                                obj.AccJobsShowNotificationDay += "," + string.Format("{0:dd/MM/yyyy}", DateTime.Now.ToDate());

                            }

                        }

                        db.SubmitChanges();

                    }
                }
            }
            catch (Exception ex)
            {


            }

            Close();
        }

        private void frmAccJobsReminder_Load(object sender, EventArgs e)
        {

        }
    }
}
