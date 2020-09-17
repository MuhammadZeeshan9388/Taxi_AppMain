using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taxi_AppMain
{
    public partial class frmVatCalculator : Form
    {
       

        public frmVatCalculator()
        {
            InitializeComponent();
            fillCombo();
        }

        private void fillCombo()
        {
           
            for (int year = 2011; year < 2050; ++year)
            {
                cmbYear.Items.Add(year);
                
            }
           // cmbYear.SelectedValue = DateTime.UtcNow.Year;
            cmbYear.SelectedItem = DateTime.UtcNow.Year;
           
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string year = cmbYear.SelectedItem.ToString();

            string fromDate;
            string TillDate;
            string Quarter;


            if (radFirst.IsChecked == true)
            {
                fromDate = year + "-01-01 00:00:00";
                TillDate = year + "-03-31 23:59:59";
                Quarter = "First Quarter";
            }
            else if (radSecond.IsChecked == true)
            {
                fromDate = year + "-04-01 00:00:00";
                TillDate = year + "-06-30 23:59:59";
                Quarter = "Second Quarter";
            }
            else if (radThird.IsChecked == true)
            {
                fromDate = year + "-07-01 00:00:00";
                TillDate = year + "-09-30 23:59:59";
                Quarter = "Third Quarter";
            }
            else
            {
                fromDate = year + "-10-01 00:00:00";
                TillDate = year + "-12-31 23:59:59";
                Quarter = "Fourth Quarter";
            }

            frmVATInvoice frm = new frmVATInvoice(fromDate, TillDate, Quarter);
            frm.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
