using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Threading;
using Telerik.WinControls;
using Taxi_Model;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmMessageAllDrivers : Form
    {
        public frmMessageAllDrivers()
        {
            InitializeComponent();
            this.Load+=new EventHandler(frmMessageAllDrivers_Load);
            this.KeyDown += new KeyEventHandler(frmMessageAllDrivers_KeyDown);



            txtMessage.TextChanged += new EventHandler(txt_TextChanged);
            txtMessage.ListBoxElement.Width = 500;
            txtMessage.ListBoxElement.Font = new Font("Tahoma", 10, FontStyle.Bold);
            txtMessage.ListBoxElement.Height = 100;
          

            txtMessage.KeyDown += new KeyEventHandler(txtMessage_KeyDown);
            this.Shown += new EventHandler(frmMessageAllDrivers_Shown);
        }

        void frmMessageAllDrivers_Shown(object sender, EventArgs e)
        {
            txtMessage.Focus();
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


                    SendMessage();

                }
            }
            catch (Exception ex)
            {
                //   ENUtils.ShowMessage(ex.Message);

            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            try
            {





                TextSelect();

            }
            catch (Exception ex)
            {

            }
        }

        private void TextSelect()
        {

            try
            {

                if (txtMessage.Text.Trim().Length == 0)
                {

                    txtMessage.ListBoxElement.Items.Clear();
                  //  this.Size = new Size(this.Size.Width, 388);
                  //  radPanel2.Size = new Size(radPanel2.Size.Width, 56);
                    return;
                }

                txtMessage.ListBoxElement.Items.Clear();
                string[] res = null;
                using (Taxi_Model.TaxiDataContext con = new TaxiDataContext())
                {
                    con.CommandTimeout = 4;
                    if (txtMessage.Text == "")
                    { res = con.Fleet_DriverTemplets.Take(0).Select(w => w.Templets).ToArray(); }
                    else
                        res = con.Fleet_DriverTemplets.Where(w => w.Templets.StartsWith(txtMessage.Text)).Select(w => w.Templets).ToArray();

                }
                txtMessage.ListBoxElement.Items.AddRange(res);

                if (res.Count() > 0)
                {
                    txtMessage.ShowListBox();

                    txtMessage.ListBoxElement.BringToFront();
                    txtMessage.ListBoxElement.ScrollAlwaysVisible = true;

                  //  this.Size = new Size(this.Size.Width, 490);
                   // radPanel2.Size = new Size(radPanel2.Size.Width, 160);
                    txtMessage.ListBoxElement.Height = 100;
                }
                else
                {
                    txtMessage.ListBoxElement.Hide();
                    txtMessage.ListBoxElement.Items.RemoveAt(0);

                   // this.Size = new Size(this.Size.Width, 388);
                   // radPanel2.Size = new Size(radPanel2.Size.Width, 56);
                }
                if (txtMessage.Text != txtMessage.FormerValue)
                {
                    txtMessage.FormerValue = txtMessage.Text;
                }
            }
            catch
            {
              //  this.Size = new Size(this.Size.Width, 388);
              //  radPanel2.Size = new Size(radPanel2.Size.Width, 56);
            }

        }


        void frmMessageAllDrivers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();

            }
            else if (e.KeyCode == Keys.Insert)
            {
                PickTemplate();


            }
            else if (e.KeyCode == Keys.End)
            {
                SendMessage();
              

            }
        }

        private void frmMessageAllDrivers_Load(object sender, EventArgs e)
        {
            try
            {
                var list = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && (c.Fleet_Driver.HasPDA != null && c.Fleet_Driver.HasPDA == true) && (c.Fleet_Driver.IsActive!=null && c.Fleet_Driver.IsActive==true)).ToList();




                foreach (var query in list )
                {

                    RadListDataItem item = new RadListDataItem();
                    item.Font=new Font("Tahoma",10, FontStyle.Bold);
                    item.Text=query.Fleet_Driver.DriverNo + " - " +query.Fleet_Driver.DriverName;
                    item.Value = query.Fleet_Driver.Id;

                    radListControl1.Items.Add(item);
                   
                }




            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        

        private void btnSendEmail_Click(object sender, EventArgs e)
        {

            SendMessage();
        }

        private void SendMessage()
        {
            try
            {
                btnSendEmail.Enabled = false;
                string error = string.Empty;

                string msg = txtMessage.Text.Trim();



                if (string.IsNullOrEmpty(msg))
                {
                    error += "Cannot send empty message..";
                }


                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }


                Thread smsThread = new Thread(delegate()
                {
                    if (this.IsDisposed == false)
                    {
                        SendPDAMessage(msg, radListControl1.Items);
                    }
                });


                smsThread.Start();




                RadDesktopAlert alert = new RadDesktopAlert();
                alert.CaptionText = "Message sent successfully";

                alert.ContentText = msg;
                alert.ContentImage = Resources.Resource1.email;
                alert.Show();


                System.Threading.Thread.Sleep(2000);
                this.Close();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
            finally
            {

                btnSendEmail.Enabled = true;

            }

        }


        private void SendPDAMessage(string msg,RadListDataItemCollection items)
        {

            try
            {
                string drvIds=string.Join(",", items.Select(c=>c.Value.ToStr()).ToArray<string>());


                 if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                 {
                     General.SendMessageToPDA("request pda=" + drvIds + "=" + 0 + "="
                                    + "Message>>" + msg + ">>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "=4");
                 }              
               
            }
            catch (Exception ex)
            {


            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnPickTemplet_Click(object sender, EventArgs e)
        {
            PickTemplate();
        }

        private void PickTemplate()
        {
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list = (from a in db.Fleet_DriverTemplets
                                select new
                                {
                                    Id = a.Id,
                                    Templates = a.Templets,

                                }).ToList();

                    object[] obj = General.ShowFormLister(list, "Id", true);





                    if (obj != null)
                    {
                        txtMessage.Text = obj[2].ToString();
                    }
                }
            }
            catch
            {


            }

        }
    }
}
