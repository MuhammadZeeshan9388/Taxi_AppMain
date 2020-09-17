using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taxi_AppMain.Report_Classes
{
    public partial class demo_repor : Form
    {
        List<string> lst = new List<string>();
        public demo_repor()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void load()
        {
            ComboFunctions.FillBookingTypeCombo(comboBox1);

        }
        private void demo_repor_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lst.Contains(comboBox1.Text))
            {
            }
            else
            {
                lst.Add(comboBox1.Text);
                lstbox_.Items.Add(comboBox1.Text);
            }
        }
    }
}
