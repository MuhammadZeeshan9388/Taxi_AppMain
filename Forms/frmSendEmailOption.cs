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
    public partial class frmSendEmailOption : Form
    {
        public frmSendEmailOption()
        {
            InitializeComponent();
        }
        private bool _IsSend;

        public bool IsSend
        {
            get { return _IsSend; }
            set { _IsSend = value; }
        }
        private int _InvoiceType;

        public int InvoiceType
        {
            get { return _InvoiceType; }
            set { _InvoiceType = value; }
        }
        private void btnSendInvoice_Click(object sender, EventArgs e)
        {
            IsSend = true;
            if (rdoAccount.IsChecked)
            {
                InvoiceType = 1;
            }
            else
            {
                InvoiceType = 2;
            }
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
