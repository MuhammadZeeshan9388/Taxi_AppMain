using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Utils;

namespace Taxi_AppMain.Forms
{
    public partial class frmSMSReply : Forms.BaseForm
    {



        private string _MessageType = "Inbox";

        public string MessageType
        {
            get { return _MessageType; }
            set
            {
                _MessageType = value;


                if (value.ToStr() == "Pda")
                    lblHeader.Text = "PDA Incoming Messages";
                else
                    lblHeader.Text = "SMS Inbox Messages";

                PopulateData();
                FormatColumns();


            }
        }
        RadDropDownMenu menu = null;
        public frmSMSReply()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmSMSReply_Load);
            btnForceRead.Visible = false;
        }

        void frmSMSReply_Load(object sender, EventArgs e)
        {




            grdLister.EnableAlternatingRowColor = true;
            grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;

            menu = new RadDropDownMenu();
            menu.BackColor = Color.Orange;

            RadMenuItem subMenu1 = new RadMenuItem("Delete");
            subMenu1.ForeColor = Color.DarkBlue;
            subMenu1.BackColor = Color.Orange;
            subMenu1.Font = new Font("Tahoma", 10, FontStyle.Bold);

            subMenu1.Click += new EventHandler(btnDelete_Click);
            menu.Items.Add(subMenu1);

            subMenu1 = new RadMenuItem("Reply");
            subMenu1.ForeColor = Color.DarkBlue;
            subMenu1.BackColor = Color.Orange;
            subMenu1.Font = new Font("Tahoma", 10, FontStyle.Bold);

            subMenu1.Click += new EventHandler(btnReply_Click);
            menu.Items.Add(subMenu1);

            subMenu1 = new RadMenuItem("Show Job History");
            subMenu1.ForeColor = Color.DarkBlue;
            subMenu1.BackColor = Color.Orange;
            subMenu1.Font = new Font("Tahoma", 10, FontStyle.Bold);

            subMenu1.Click += new EventHandler(btnLastBookings_Click);
            menu.Items.Add(subMenu1);

            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);
        }


        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                    return;

                else if (cell.GridControl.Name == "grdLister")
                {
                    if (this.MessageType.ToLower() == "pda")
                        menu.Items[2].Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                    
                    else
                        menu.Items[2].Visibility = Telerik.WinControls.ElementVisibility.Visible;


                    e.ContextMenu = menu;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    long Id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();

                    using (Taxi_Model.TaxiDataContext db = new Taxi_Model.TaxiDataContext())
                    {
                        db.Messages.DeleteOnSubmit(db.Messages.FirstOrDefault(c => c.Id == Id));
                        db.SubmitChanges();


                    }

                    PopulateData();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void btnReply_Click(object sender, EventArgs e)
        {
            ReplyMessage();
        }
        void btnLastBookings_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    string MobileNo = grdLister.CurrentRow.Cells["Name"].Value.ToStr();

                    int a = MobileNo.IndexOf("(");

                    if (a == -1)
                        a = 0;

                    MobileNo = MobileNo.Substring(a, MobileNo.Length - a);


                    MobileNo = MobileNo.Replace("(", "");
                    MobileNo = MobileNo.Replace(")", "");
                    frmSearchBooking frm = new frmSearchBooking(MobileNo);
                    frm.ShowDialog();


                    //using (Taxi_Model.TaxiDataContext db = new Taxi_Model.TaxiDataContext())
                    //{
                    //    db.Messages.DeleteOnSubmit(db.Messages.FirstOrDefault(c => c.Id == Id));
                    //    db.SubmitChanges();


                    //}

                    //PopulateData();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void ReplyMessage()
        {

            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int DriverId = grdLister.CurrentRow.Cells["SenderId"].Value.ToInt();
                    string mobileNo = "";


                    string sendFrom = grdLister.CurrentRow.Cells["SendFrom"].Value.ToStr();

                    string messageType = "Inbox";

                    if (sendFrom.ToLower() == "pda")
                        messageType = "pda";
                    else
                    {
                        mobileNo = grdLister.CurrentRow.Cells["Name"].Value.ToStr();
                        int index = mobileNo.LastIndexOf('(');

                        if (index != -1)
                        {


                            mobileNo = mobileNo.Substring(mobileNo.LastIndexOf('(') + 1);
                            mobileNo = mobileNo.Replace(")", "");
                        }
                        else
                            mobileNo = "";
                    }

                    frmMessageReply frmReply = new frmMessageReply();
                    frmReply.MessageType = messageType;
                    frmReply.FromMessage = grdLister.CurrentRow.Cells["Message"].Value.ToStr();
                    frmReply.CustomerMobileNo = mobileNo;
                    frmReply.DriverId = DriverId;
                    frmReply.ReceiverName = grdLister.CurrentRow.Cells["Name"].Value.ToStr();

                    frmReply.ShowDialog();
                    frmReply.Dispose();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }



        void btnDeleteAll_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            //    {
            //        string message = AppVars.objPolicyConfiguration.ArrivalBookingText.ToStr();
            //        frmSMSAll frm = new frmSMSAll(grdLister.CurrentRow.Cells["MobileNo"].Value.ToStr(), message);
            //        frm.ShowDialog();
            //        frm.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowMessage(ex.Message);
            //}
        }


        private void FormatColumns()
        {
            try
            {
                grdLister.Columns["SendFrom"].IsVisible = false;
                grdLister.Columns["MessageType"].IsVisible = false;
                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["SenderId"].IsVisible = false;
                grdLister.Columns["Name"].HeaderText = "Name/Number";
                grdLister.Columns["Name"].Width = 160;
                grdLister.Columns["Message"].Width = 350;
                grdLister.Columns["Time"].Width = 150;


            //    (grdLister.Columns["Time"] as GridViewDateTimeColumn).Sort(RadSortOrder.Descending, true);

              //  grdLister.SortDescriptors.Add("Time", ListSortDirection.Descending);
            }
            catch (Exception ex)
            {


            }
        }



        private void PopulateData()
        {

            try
            {
                if (this.MessageType == "Inbox")
                {

                    var data = (from a in General.GetQueryable<Taxi_Model.Message>(c => c.MessageType != null && c.MessageType == "Inbox")

                                select new
                                {
                                    Id = a.Id,
                                    SenderId = a.SenderId,
                                    Name = a.SenderName,
                                    Message = a.MessageBody,
                                    Time = a.MessageCreatedOn,
                                    SendFrom = a.SendFrom,
                                    MessageType = a.MessageType

                                }).OrderByDescending(c=>c.Id).Take(1000).ToList();

                    grdLister.DataSource = data;

                }
                else
                {
                    var data = (from a in General.GetQueryable<Taxi_Model.Message>(c => c.SendFrom != null && c.SendFrom == "pda" && c.MessageCreatedOn.Value.Date == DateTime.Now.Date)

                                select new
                                {
                                    Id = a.Id,
                                    SenderId = a.SenderId,
                                    Name = a.SenderName,
                                    Message = a.MessageBody,
                                    Time = a.MessageCreatedOn,
                                    SendFrom = a.SendFrom,
                                    MessageType = a.MessageType


                                }).OrderByDescending(c => c.Id).Take(1000).ToList();

                    grdLister.DataSource = data;


                }

                if (grdLister.Rows.Count > 0)
                    grdLister.CurrentRow = grdLister.Rows[grdLister.Rows.Count - 1];


                ShowMessageDetails();



            }
            catch (Exception ex)
            {


            }

        }



        private void ShowMessageDetails()
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {


                    txtmsgDetails.Text = string.Empty;
                    txtmsgDetails.Text += "Name/Number : " + grdLister.CurrentRow.Cells["Name"].Value.ToStr();
                    txtmsgDetails.Text += Environment.NewLine + Environment.NewLine + "Message : " + grdLister.CurrentRow.Cells["Message"].Value.ToStr();
                    txtmsgDetails.Text += Environment.NewLine + Environment.NewLine + "Received On : " + grdLister.CurrentRow.Cells["Time"].Value.ToStr();

                }
            }
            catch (Exception ex)
            {


            }
        }

        private void grdLister_CellClick(object sender, GridViewCellEventArgs e)
        {
            ShowMessageDetails();
        }

        private void frmSMSReply_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void grdLister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {

                ReplyMessage();

            }
        }

        private void btnForceRead_Click(object sender, EventArgs e)
        {
            btnForceRead.Enabled = false;


          //  ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).ForceReadSMS();


            System.Threading.Thread.Sleep(2000);
            btnForceRead.Enabled = true;

        }


    }
}
