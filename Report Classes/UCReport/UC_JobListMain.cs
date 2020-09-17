using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taxi_AppMain.Report_Classes.UCReport
{
    public partial class UC_JobListMain : UserControl
    {
        public UC_JobListMain()
        {
            InitializeComponent();
        }
        public void showreport()
        {
            UC_Joblist_report s = new UC_Joblist_report();
            if (splitContainer1.Panel2.Controls.Count > 0)
            {

                splitContainer1.Panel2.Controls.Clear();
            }
            s.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(s);
        }
        private void UC_JobListMain_Load(object sender, EventArgs e)
        {

        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            showreport();
        }
    }
}
