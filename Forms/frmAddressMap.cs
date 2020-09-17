using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Taxi_AppMain
{
    public partial class frmAddressMap : Form
    {
        public frmAddressMap(string Address)
        {
            InitializeComponent();
            Locations(Address);
            this.KeyUp += new KeyEventHandler(frmAddressMap_KeyUp);
            this.Shown += new EventHandler(frmAddressMap_Shown);
        }

        void frmAddressMap_Shown(object sender, EventArgs e)
        {
            pnlTop.Focus();
        }

        void frmAddressMap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void Locations(string Address)
        {
            pnlAddress.Text = " Address: " + Address;
            string url = "https://maps.google.com/?output=embed&q=" + Address;

            webBrowser1.Navigate(url);
        }

        private void frmAddressMap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
