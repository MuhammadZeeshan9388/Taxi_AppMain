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

namespace Taxi_AppMain
{
    public partial class frmPDAInbox : Forms.BaseForm
    {
        RadDropDownMenu menu = null;
        public frmPDAInbox()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmSMSReply_Load);
        }

        void frmSMSReply_Load(object sender, EventArgs e)
        {

            PopulateData();

            FormatColumns();

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


            //RadMenuItem subMenu2 = new RadMenuItem("Delete All");
            //subMenu2.ForeColor = Color.DarkBlue;
            //subMenu2.BackColor = Color.Orange;
            //subMenu2.Font = new Font("Tahoma", 10, FontStyle.Bold);
            //subMenu2.Click += new EventHandler(btnDeleteAll_Click);
            //menu.Items.Add(subMenu2);

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
                    long Id=grdLister.CurrentRow.Cells["Id"].Value.ToLong();

                    using (Taxi_Model.TaxiDataContext db = new Taxi_Model.TaxiDataContext())
                    {
                        db.Messages.DeleteOnSubmit(db.Messages.FirstOrDefault(c=>c.Id==Id));
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
                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["SenderId"].IsVisible = false;
                grdLister.Columns["Name"].HeaderText = "Name/Number";
                grdLister.Columns["Name"].Width = 160;
                grdLister.Columns["Message"].Width = 350;
                grdLister.Columns["Time"].Width = 150;


                (grdLister.Columns["Time"] as GridViewDateTimeColumn).Sort(RadSortOrder.Descending, true);
            
                grdLister.SortDescriptors.Add("Time", ListSortDirection.Descending);
            }
            catch (Exception ex)
            {


            }
        }



        private void PopulateData()
        {

            try
            {

                var data = (from a in General.GetQueryable<Taxi_Model.Message>(c => c.SendFrom != null && c.SendFrom == "pda")
                             
                            select new
                            {
                                Id=a.Id,
                                SenderId=a.SenderId,
                                Name = a.SenderName,
                                Message = a.MessageBody,
                                Time = a.MessageCreatedOn


                            }).ToList();

                grdLister.DataSource = data;




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

                   // int senderId = grdLister.CurrentRow.Cells["Id"].Value.ToInt();

                    txtmsgDetails.Text = string.Empty;
                    txtmsgDetails.Text +="Name/Number : "+ grdLister.CurrentRow.Cells["Name"].Value.ToStr();
                    txtmsgDetails.Text += Environment.NewLine +Environment.NewLine+ "Message : " + grdLister.CurrentRow.Cells["Message"].Value.ToStr();
                    txtmsgDetails.Text += Environment.NewLine  +Environment.NewLine + "Received On : " + grdLister.CurrentRow.Cells["Time"].Value.ToStr();

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

      
    }
}
