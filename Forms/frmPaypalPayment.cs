using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utils;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Gecko;

namespace Taxi_AppMain
{
    public partial class frmPaypalPayment : Form
    {
       
        public frmPaypalPayment()
        {
            InitializeComponent();
            
        }
        public frmPaypalPayment(string Address)
        {
            InitializeComponent();
            try
           {
                Xpcom.Initialize("Firefox");
            }
           catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            //  this.webBrowser1.ScriptErrorsSuppressed = true;

            ShowPaymentForm(Address);
        }



        private  void ShowPaymentForm(string Address)
        {
          
            webBrowser1.Navigate(Address.ToString());
        }

      

    }
}
