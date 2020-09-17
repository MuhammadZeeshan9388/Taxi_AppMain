using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_AppMain.Classes;
using Taxi_Model;
using Telerik.WinControls.UI;
using System.Net.NetworkInformation;
using System.Threading;
namespace Taxi_AppMain.Forms
{
    public partial class frmMessages : Forms.BaseForm
    {
        delegate void UIDelegate();
      //  System.Net.NetworkInformation.Ping ping=null;
        private int _driverId;

        public int DriverId
        {
            get { return _driverId; }
            set { _driverId = value; }
        }

        private Fleet_Driver _ObjDriver;

        public Fleet_Driver ObjDriver
        {
            get { return _ObjDriver; }
            set { _ObjDriver = value; }
        }

        public frmMessages(int driverId)
        {
            InitializeComponent();

            this.DriverId = driverId;
            InitializeConstructor();

        
        }


        private void InitializeConstructor()
        {

            this.ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == this.DriverId);
            btnTestConn.Enabled =Convert.ToBoolean(ObjDriver.HasPDA);
            if (btnTestConn.Enabled == false)
                txtTitle.Width += 112;


            LoadConversation();

        

         //   timer1.Start();
            this.Shown += new EventHandler(frmMessages_Shown);

            txtTitle.Text =  ObjDriver.DriverNo.ToStr() + " - " + ObjDriver.DriverName.ToStr();


            RadListDataItem item = new RadListDataItem();
            item.Text = "Today";
            item.Value = "0";
            item.Selected = true;
            ddlShow.Items.Add(item);


            item = new RadListDataItem();
            item.Text = "Yesterday";
            item.Value = "1";
            ddlShow.Items.Add(item);


            item = new RadListDataItem();
            item.Text = "Customer";
            item.Value = "2";
            ddlShow.Items.Add(item);

            item = new RadListDataItem();
            item.Text = "All";
            item.Value = "365";
            ddlShow.Items.Add(item);


            ddlShow.SelectedIndexChanged+=new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlShow_SelectedIndexChanged);
         //   this.FormClosing += new FormClosingEventHandler(frmMessages_FormClosing);

            

        }


        
        //void frmMessages_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //  //  IsOnClosed = true;
        //}

        void frmMessages_Shown(object sender, EventArgs e)
        {
            txtMessage.Focus();
        }


        private void LoadConversation()
        {
            try
            {
                txtConversation.Clear();
                string result=string.Empty;

                int val=ddlShow.SelectedValue.ToInt();

                if (val == 2)
                {
                    string[] data = General.GetQueryable<Taxi_Model.Booking_Note>(c =>c.AddBy!=null && c.AddOn!=null &&  c.AddBy==this.DriverId)
                            .OrderBy(c=>c.AddOn).AsEnumerable()
                                .Select(a => "[Ref #:"+a.Booking.BookingNo+"] <b> " + a.Booking.CustomerName+"("+a.Booking.CustomerMobileNo+")" + ": </b>" + a.notes).ToArray<string>();

                    result = string.Join("\n\n", data);


                }

                else
                {

                    DateTime filterDate = DateTime.Now.AddDays(-val).ToDate();
                    string dateFormat = ddlShow.SelectedValue.ToInt() == 0 ? "{0:HH:mm}" : "{0:dd/MM HH:mm}";

                  //  .Select(a =>"[" + string.Format(dateFormat, a.MessageCreatedOn) + "] <b>" + a.SenderName + ": </b>" + a.MessageBody)
                     string[] data =null;
                    if (ddlShow.SelectedValue.ToInt()==0)
                    {
                        data = (from a in General.GetQueryable<Taxi_Model.Message>(null)

                                         where a.MessageCreatedOn.Value.Date >= filterDate.Date &&

                                    ((a.SenderId != null && a.SenderId == DriverId) || (a.ReceiverId != null && a.ReceiverId == DriverId))
                                         orderby a.MessageCreatedOn
                                         orderby a.Id
                                         select ("[" + a.MessageSource.Substring(a.MessageSource.IndexOf(' ') + 1) + "] <b>" + a.SenderName + ": </b>" + a.MessageBody))

                                    .ToArray<string>();
                    }
                    else
                    {
                       data = (from a in General.GetQueryable<Taxi_Model.Message>(null)

                                         where a.MessageCreatedOn.Value.Date >= filterDate.Date &&

                                    ((a.SenderId != null && a.SenderId == DriverId) || (a.ReceiverId != null && a.ReceiverId == DriverId))
                                         orderby a.MessageCreatedOn
                                         orderby a.Id
                                         select ("[" + a.MessageSource + "] <b>" + a.SenderName + ": </b>" + a.MessageBody))

                                   .ToArray<string>();

                    }


                    if (data != null)
                    {
                        result = string.Join("\n\n", data);
                    }

                   
                }

                txtConversation.AddHTML(result);
            }
            catch (Exception ex)
            {
              //  ENUtils.ShowMessage(ex.Message);

            }
        }




        private void btnSend_Click(object sender, EventArgs e)
        {
            SendAndClear();

        }

        private void SendAndClear()
        {
            new Thread(delegate()
            {
                SendMessage();
                ClearText();
            }).Start(); 
            

        }


        private void SendMessage()
        {

            try
            {
             

                string text = txtMessage.Text.Trim();

                if(string.IsNullOrEmpty(text))
                    return;

                bool IsPdaMsg = ObjDriver.HasPDA.ToBool();

                if (IsPdaMsg)
                {
                    //if (ping == null)
                    //{
                    //    ping = new System.Net.NetworkInformation.Ping();
                    //    ping.PingCompleted += new System.Net.NetworkInformation.PingCompletedEventHandler(ping_PingCompleted);
                    //}

                    //object[] arr = new object[2];
                    //arr[0] = text;
                    //arr[1] = IsPdaMsg;

                    //object[] obj = (object[])e.UserState;

                    SendData(text,IsPdaMsg);

                   // ping.SendAsync("www.google.com", arr);                   
                }
                else
                {

                    General.SP_SendMessage(AppVars.LoginObj.LuserId.ToInt(), DriverId, AppVars.LoginObj.UserName.ToStr(), ObjDriver.DriverNo, text, DateTime.Now, "", ObjDriver.MobileNo.ToStr().Trim(), ObjDriver.PDAMobileNo.ToStr().Trim(), IsPdaMsg);



                }


                General.SaveSentSMS(text, "Drv " + ObjDriver.DriverNo + " (" + ObjDriver.MobileNo.ToStr().Trim()+")");


            }
            catch (Exception ex)
            {


            }
        }

      

        private void SendData(string text, bool IsPdaMsg)
        {


           

                try
                {

                    int loopCnt = 1;


                    while (loopCnt < 3)
                    {

                        bool success = General.SP_SendMessage(AppVars.LoginObj.LuserId.ToInt(), DriverId, AppVars.LoginObj.UserName.ToStr(), ObjDriver.DriverNo, text, DateTime.Now, "", ObjDriver.MobileNo.ToStr().Trim(), ObjDriver.PDAMobileNo.ToStr().Trim(), IsPdaMsg);

                        if (success)
                        {
                            break;

                        }
                        else
                             loopCnt++;


                    }

                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new UIDelegate(LoadConversation));
                    }
                    else
                    {

                        LoadConversation();
                    }
                }
                catch (Exception ex)
                {


                }

            

        }

        //void ping_PingCompleted(object sender, System.Net.NetworkInformation.PingCompletedEventArgs e)
        //{
        //    if (IsOnClosed==true || this.IsHandleCreated == false)
        //        return;

        //    if (e.Reply == null)
        //    {

        //        CallConnectionError();
        //        return;
        //    }

        //    if (e.UserState is object[])
        //    {
        //        // if (e.Reply.Status == IPStatus.Success)
        //        // {
        //        object[] obj=(object[])e.UserState;

        //        SendData(obj[0].ToStr(),obj[1].ToBool());

        //        // }
        //    }
        //}

        private void CallConnectionError()
        {


            if (this.IsHandleCreated == false || this.IsDisposed)
                return;

            MethodInvoker mi = new MethodInvoker(delegate() { ShowConnectionError(); });
            this.Invoke(mi);


        }


        private void ShowConnectionError()
        {

            RadDesktopAlert alert = new Telerik.WinControls.UI.RadDesktopAlert();

            alert.AutoCloseDelay = 5;
            alert.FadeAnimationType = FadeAnimationType.None;

            alert.ShowOptionsButton = false;
            alert.ShowPinButton = false;

            alert.FixedSize = new Size(250, 100);

            alert.CaptionText = "Connection Problem";
            alert.ContentText = "<html> <b><span style=font-size:medium><color=Red>Your Internet Connection is not working..</span></b></html>";
            alert.Show();

        }     

    

        private void ClearText()
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UIDelegate(OnClearText));
            }
            else
            {

                OnClearText();
            }

            

          
        }

        private void OnClearText()
        {

            txtMessage.Clear();

            txtMessage.ResetText();

        }


        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Shift && e.KeyCode == Keys.Enter)
                {
                    txtMessage.AppendText(Environment.NewLine);

                }
                else if (e.KeyCode == Keys.Enter)
                {


                    SendAndClear();

                }
            }
            catch (Exception ex)
            {
             //   ENUtils.ShowMessage(ex.Message);

            }
        }

        private void ddlShow_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            LoadConversation();
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "test connection from server";


           
                SendAndClear();
            
        }


     

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                frmDrivertemplet frm = new frmDrivertemplet();
                frm.ShowDialog();

                this.txtMessage.Text = frm.SelectedTemplate.ToStr();
                frm.Dispose();

            }
            catch (Exception ex)
            {


            }
        }

        private void addTemplatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                frmAddDrivertemplet frmadd = new frmAddDrivertemplet();
                frmadd.ShowDialog();
                frmadd.Dispose();


            }
            catch (Exception ex)
            {

            }
        }
    }
}
