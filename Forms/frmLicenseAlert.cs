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
    public partial class frmLicenseAlert : Form
    {


      
        public frmLicenseAlert(string licenseExpiryDate,Point loc)
        {
            InitializeComponent();

            try
            {
                try
                {

                    licenseExpiryDate = licenseExpiryDate.Replace("License will Expire on ", "").Trim();


                    licenseExpiryDate = licenseExpiryDate.Replace("License Expired on ", "").Trim();
                }
                catch
                {


                }

                DateTime licenseDate;

               if (DateTime.TryParse(licenseExpiryDate, out licenseDate))
               {
                   int days =licenseDate.Subtract(DateTime.Now).Days;


                   if (days > 1)
                   {
                       txtLicense.Text = "Your Treasure Cab System License will expire in " + days + " days!";
                   }
                   else if (days == 1)
                   {
                       txtLicense.Text = "Your Treasure Cab System License will expire after " + days + " day!";

                   }

                   else
                   {
                       txtLicense.Text = "Your Treasure Cab System License will expire soon.";
                   }



               }
            }
            catch (Exception ex)
            {



            }

          
        }

      

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void frmLicenseAlert_MouseHover(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        private void frmLicenseAlert_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.9;
        }

        
    }
}
