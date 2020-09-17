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
    public partial class frmCustomMessageBox : Form
    {
        private string formCaption;
        private string commandAction;
        public frmCustomMessageBox(string caption, string action)
        {
            InitializeComponent();
            this.commandAction = action.ToStr().ToLower();

            this.formCaption = caption;
            this.Load += new EventHandler(frmCustomMessageBox_Load);

        }

        void frmCustomMessageBox_Load(object sender, EventArgs e)
        {
            try
            {
                txtTitle.Text ="Booking Ref: "+this.formCaption;

                if (commandAction == "accept")
                {
                    txtAccept.Visible = true;
                    txtAcceptLabel.Visible = true;
                    txtAcceptLabel.BringToFront();
                }
                else if (commandAction == "pendingaccept")
                {
                    txtPendingAccept.Visible = true;
                    txtPendingAcceptLabel.Visible = true;
                    txtPendingAcceptLabel.BringToFront();
                }
                else if (commandAction == "decline")
                {
                    txtDecline.Visible = true;
                    txtDeclineLabel.Visible = true;
                    txtDeclineLabel.BringToFront();

                }
            }
            catch
            {


            }
        }

        private void CloseForm()
        {

            Close();
            Dispose(true);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            CloseForm();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            CloseForm();
        }
    }
}
