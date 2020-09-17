using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace Taxi_AppMain.Forms
{
    public partial class frmMessageReply : BaseForm
    {
        private string _FromMessage;

        public string FromMessage
        {
            get { return _FromMessage; }
            set { _FromMessage = value; }
        }


        private string _MessageType;

        public string MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }

        private string _CustomerMobileNo;

        public string CustomerMobileNo
        {
            get { return _CustomerMobileNo; }
            set { _CustomerMobileNo = value; }
        }
        private int _DriverId;

        public int DriverId
        {
            get { return _DriverId; }
            set { _DriverId = value; }
        }

        private string _ReceiverName;

        public string ReceiverName
        {
            get { return _ReceiverName; }
            set { _ReceiverName = value; }
        }




        public frmMessageReply()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmMessageReply_Shown);
        }

        void frmMessageReply_Shown(object sender, EventArgs e)
        {
           
            lblFrom.Text = this.ReceiverName;
            txtFromMsg.Text = this.FromMessage;
            txtReply.Focus();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }


        private void SendMessage()
        {
            try
            {

                string messageBody = txtReply.Text.Trim();

                General.SP_SendMessage(AppVars.LoginObj.LuserId.ToInt(), DriverId, AppVars.LoginObj.UserName.ToStr(), this.ReceiverName, messageBody
                    , DateTime.Now, "", this.CustomerMobileNo, "", this.MessageType.ToStr().ToLower()=="pda"?true:false);


                this.Close();
            }
            catch (Exception ex)
            {


            }



        }

        private void frmMessageReply_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home)
            {

                SendMessage();

            }
        }

    }
}
