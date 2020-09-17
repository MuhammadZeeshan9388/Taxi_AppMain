using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_Model;
using System.Web.UI.WebControls;
using Telerik.WinControls.UI;

namespace Taxi_AppMain
{
    public partial class frmControllerInternalMessages : Form
    {
        public frmControllerInternalMessages()
        {
            InitializeComponent();
            FormatInboxMessagesGrid();
            FormatSentMessagesGrid();
            
            this.Load += new EventHandler(frmControllerInternalMessages_Load);
            this.KeyDown += new KeyEventHandler(frmControllerInternalMessages_KeyDown);
            this.ddlGroup.SelectedValueChanged += new EventHandler(ddlGroup_SelectedValueChanged);
            this.grdInbox.SelectionChanged += new EventHandler(grdInbox_SelectionChanged);
            this.grdSent.SelectionChanged += new EventHandler(grdSent_SelectionChanged);

            //this.grdMessages.CellMouseDown += new DataGridViewCellMouseEventHandler(grdMessages_CellMouseDown);
            this.grdInbox.MouseClick += new MouseEventHandler(grdInbox_MouseClick);
            this.grdSent.MouseClick += new MouseEventHandler(grdSent_MouseClick);
        }



        void grdSent_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string Sender = grdSent.CurrentRow.Cells["Sender"].Value.ToStr();
                if (!string.IsNullOrEmpty(Sender))
                {
                    int currentRow = grdSent.CurrentCell.RowIndex;
                    grdSent.Rows[currentRow].Selected = true;
                }
                else
                {
                    //grdMessages.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grdSent.ClearSelection();
                }
            }
            catch (Exception ex)
            { }
        }

        void grdSent_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && grdSent.CurrentRow.Cells[COLS.Id].Value.ToInt() > 0)
            {
                ContextMenu m = new ContextMenu();
                //m.MenuItems.Add(new System.Windows.Forms.MenuItem("Delete"));
                System.Windows.Forms.MenuItem menuSent = new System.Windows.Forms.MenuItem();
                menuSent.Text = "Delete";
                menuSent.Name = "Delete";
                menuSent.Click += new EventHandler(menuSent_Click);
                m.MenuItems.Add(menuSent);

                int currentMouseOverRow = grdSent.HitTest(e.X, e.Y).RowIndex;


                //if (currentMouseOverRow >= 0)
                //{
                //    m.MenuItems.Add(new System.Windows.Forms.MenuItem(string.Format("Do something to row {0}", Id.ToStr())));
                //}

                m.Show(grdSent, new Point(e.X, e.Y));

            }
        }

    

        void grdInbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && grdInbox.CurrentRow.Cells[COLS.Id].Value.ToInt() > 0)
            {
                ContextMenu m = new ContextMenu();
                //m.MenuItems.Add(new System.Windows.Forms.MenuItem("Delete"));
                System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem();
                menu.Text = "Delete";
                menu.Name = "Delete";
                menu.Click += new EventHandler(menu_Click);
                m.MenuItems.Add(menu);

                int currentMouseOverRow = grdInbox.HitTest(e.X, e.Y).RowIndex;


                //if (currentMouseOverRow >= 0)
                //{
                //    m.MenuItems.Add(new System.Windows.Forms.MenuItem(string.Format("Do something to row {0}", Id.ToStr())));
                //}

                m.Show(grdInbox, new Point(e.X, e.Y));

            }
        }

        void grdInbox_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string Reciever = grdInbox.CurrentRow.Cells["Reciever"].Value.ToStr();
                if (!string.IsNullOrEmpty(Reciever))
                {
                    int currentRow = grdInbox.CurrentCell.RowIndex;
                    grdInbox.Rows[currentRow].Selected = true;
                }
                else
                {
                    //grdMessages.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grdInbox.ClearSelection();
                }
            }
            catch (Exception ex)
            { }
        }

        void grdMessages_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    var hti = grdMessages.HitTest(e.X, e.Y);
            //    grdMessages.ClearSelection();
            //    grdMessages.Rows[hti.RowIndex].Selected = true;
            //}
        }

        
        //void grdMessages_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right && grdMessages.CurrentRow.Cells[COLS.Id].Value.ToInt()>0)
        //    {
        //        ContextMenu m = new ContextMenu();
        //        //m.MenuItems.Add(new System.Windows.Forms.MenuItem("Delete"));
        //        System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem();
        //        menu.Text = "Delete";
        //        menu.Name = "Delete";
        //        menu.Click+=new EventHandler(menu_Click);
        //        m.MenuItems.Add(menu);
                
        //        int currentMouseOverRow = grdMessages.HitTest(e.X, e.Y).RowIndex;


        //        //if (currentMouseOverRow >= 0)
        //        //{
        //        //    m.MenuItems.Add(new System.Windows.Forms.MenuItem(string.Format("Do something to row {0}", Id.ToStr())));
        //        //}

        //        m.Show(grdMessages, new Point(e.X, e.Y));

        //    }
        //}



        
        //ContextMenu plotsContextMenu;

        //void grdMessages_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Button == MouseButtons.Right)
        //        {

        //            if (e.RowIndex != -1 && e.ColumnIndex != -1 && grdMessages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
        //            {





        //                if (plotsContextMenu == null)
        //                {

        //                    plotsContextMenu = new ContextMenu();

        //                    System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem();
        //                    menu.Text = "Delete";
        //                    menu.Name = "Delete";
        //                    menu.Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();
        //                    menu.Click += new EventHandler(menu_Click);
        //                    plotsContextMenu.MenuItems.Add(menu);
        //                    //MenuItem mItem = new MenuItem();
        //                    //mItem.Text = "Move Up";
        //                    //mItem.Name = "moveupitem";
        //                    //mItem.Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();
        //                    //mItem.Click += new EventHandler(DriverPositionMoveContextMenuItem1_Click);
        //                    //plotsContextMenu.MenuItems.Add(mItem);



        //                    //mItem = new MenuItem();
        //                    //mItem.Text = "Move Down";
        //                    //mItem.Name = "movedownitem";
        //                    //mItem.Visible = false;

        //                    //mItem.Click += new EventHandler(DriverPositionMoveContextMenuItem1_Click);
        //                    //plotsContextMenu.MenuItems.Add(mItem);


        //                    //mItem = new MenuItem();
        //                    //mItem.Text = "UnBlock";
        //                    //mItem.Name = "movedownitem";
        //                    //mItem.Visible = false;
        //                    //mItem.Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();
        //                    //mItem.Click += new EventHandler(unBlockViewItem_Click);
        //                    //plotsContextMenu.MenuItems.Add(mItem);


        //                    //mItem = new MenuItem();
        //                    //mItem.Text = "Logout";
        //                    //mItem.Name = "logoutitem";
        //                    //mItem.Visible = true;
        //                    //mItem.Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();
        //                    //mItem.Click += new EventHandler(DriverPositionMoveContextMenuItem1_Click);
        //                    //plotsContextMenu.MenuItems.Add(mItem);



        //                    //mItem = new MenuItem("UnBlock");
        //                    //mItem.ForeColor = Color.Black;
        //                    //unBlockViewItem.BackColor = Color.Red;
        //                    //unBlockViewItem.Click += new EventHandler(unBlockViewItem_Click);
        //                    //unBlockViewItem.Font = new Font("Tahoma", 10, FontStyle.Bold);
        //                    //driverContextMenu.Items.Add(unBlockViewItem);

        //                }

        //                //if (grdOnPlotDrivers.Columns[e.ColumnIndex].Name == "SIN BIN")
        //                //{

        //                //    plotsContextMenu.MenuItems[2].Visible = true;
        //                //    plotsContextMenu.MenuItems[0].Visible = false;
        //                //    plotsContextMenu.MenuItems[1].Visible = false;
        //                //    plotsContextMenu.MenuItems[3].Visible = false;

        //                //    plotsContextMenu.MenuItems[2].Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();

        //                //}
        //                //else
        //                //{

        //                //    plotsContextMenu.MenuItems[0].Visible = true;
        //                //    plotsContextMenu.MenuItems[1].Visible = true;
        //                //    plotsContextMenu.MenuItems[3].Visible = true;

        //                //    plotsContextMenu.MenuItems[0].Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();
        //                //    plotsContextMenu.MenuItems[1].Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();

        //                //    plotsContextMenu.MenuItems[3].Tag = e.RowIndex.ToStr() + "," + e.ColumnIndex.ToStr();
        //                //}








        //                //plotsContextMenu.Show(grdMessages, new Point(e.X, e.Y));

        //               // int mouseLoc = e.Location.X.ToInt();
        //               // int mouseLocY = e.Location.Y.ToInt();
        //               //// listView1.Location = new Point(mouseLoc.X, mouseLoc.Y + dataGridView1[e.ColumnIndex, e.RowIndex].Size.Height);
        //               // plotsContextMenu.Show(grdMessages, new Point(mouseLoc, mouseLocY));

        //                //int currentMouseOverRow = grdMessages.HitTest(e.X, e.Y).RowIndex;
        //                plotsContextMenu.Show(grdMessages, new Point(e.X, e.Y));
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //}

        void menuSent_Click(object sender, EventArgs e)
        {
            if (grdSent.CurrentRow.Cells[COLS.Id].Value != null)
            {
                int Id = grdSent.CurrentRow.Cells[COLS.Id].Value.ToInt();
                if (Id > 0)
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        InternalMessaging objInternalMessaging = db.InternalMessagings.Single(c => c.Id == Id);
                        db.InternalMessagings.DeleteOnSubmit(objInternalMessaging);
                        db.SubmitChanges();
                        PopulateSent();
                        PopulateInbox();
                    }
                }
            }
        }

        void menu_Click(object sender, EventArgs e)
        {
            if (grdInbox.CurrentRow.Cells[COLS.Id].Value != null)
            {
                int Id = grdInbox.CurrentRow.Cells[COLS.Id].Value.ToInt();
                if (Id > 0)
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        InternalMessaging objInternalMessaging = db.InternalMessagings.Single(c => c.Id == Id);
                        db.InternalMessagings.DeleteOnSubmit(objInternalMessaging);
                        db.SubmitChanges();
                        PopulateInbox();
                        PopulateSent();
                    }
                }
            }
        }
        



        //void grdMessages_SelectionChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string Sender = grdMessages.CurrentRow.Cells["Sender"].Value.ToStr();
        //        if (!string.IsNullOrEmpty(Sender))
        //        {
        //            int currentRow = grdMessages.CurrentCell.RowIndex;
        //            grdMessages.Rows[currentRow].Selected = true;
        //        }
        //        else
        //        {
        //            //grdMessages.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //            grdMessages.ClearSelection();
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}


        void ddlGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int Index = ddlGroup.SelectedIndex.ToInt();
                if (Index == 0)
                {
                    // ComboFunctions.FillUsersCombo(ddlController);
                    ddlController.DataSource = General.GetQueryable<UM_User>(null).OrderBy(c => c.UserName).ToList();
                    // dropdown.Items.Clear();

                    ddlController.DisplayMember = "UserName";
                    ddlController.ValueMember = "Id";
                    ddlController.SelectedIndex = -1;

                }
                else if (Index == 1)
                {
                    ddlController.DataSource = General.GetQueryable<UM_User>(c => c.SecurityGroupId == 1).OrderBy(c => c.UserName).ToList();
                    // dropdown.Items.Clear();

                    ddlController.DisplayMember = "UserName";
                    ddlController.ValueMember = "Id";
                    ddlController.SelectedIndex = -1;
                }
                else if (Index == 2)
                {
                    ddlController.DataSource = General.GetQueryable<UM_User>(c => c.SecurityGroupId == 2).OrderBy(c => c.UserName).ToList();
                    // dropdown.Items.Clear();

                    ddlController.DisplayMember = "UserName";
                    ddlController.ValueMember = "Id";
                    ddlController.SelectedIndex = -1;
                }
                else
                {
                    ddlController.DataSource = General.GetQueryable<UM_User>(c => c.SecurityGroupId == 3).OrderBy(c => c.UserName).ToList();
                    // dropdown.Items.Clear();

                    ddlController.DisplayMember = "UserName";
                    ddlController.ValueMember = "Id";
                    ddlController.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            { }
        }

        void frmControllerInternalMessages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                Close();
            }
        }

        void frmControllerInternalMessages_Load(object sender, EventArgs e)
        {
            try
            {
               // ComboFunctions.FillControllerCombo(ddlController);
                Group();

                PopulateInbox();
                PopulateSent();
            }
            catch (Exception ex)
            {


            }
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string Sender = "Sender";
            public static string Reciever = "Reciever";
            public static string MessageText = "MessageText";
            public static string DateTime = "DateTime";
        }
      
        private void FormatSentMessagesGrid()
        {
            grdSent.AllowUserToDeleteRows = true;
            //GridViewDateTimeColumn dcol = new GridViewDateTimeColumn();
            //dcol.Name = COLS.DateTime;
            //dcol.HeaderText = "Date Time";
            //dcol.Width = 130;
            //grdMessages.Columns.Add(dcol);
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.Visible = false;
            grdSent.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.Name = COLS.DateTime;
            col.HeaderText = "Date Time";
            col.Width = 160;
            col.ReadOnly = true;
            grdSent.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.Name = COLS.Sender;
            col.HeaderText = "Name";
            col.Width = 130;
            col.ReadOnly = true;
            grdSent.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.Name = COLS.MessageText;
            col.HeaderText = "Message Text";
            col.Width = 800;
            col.ReadOnly = true;
            grdSent.Columns.Add(col);
            grdSent.AllowUserToDeleteRows = true;
        }

        private void FormatInboxMessagesGrid()
        {
            grdInbox.AllowUserToDeleteRows = true;
            //GridViewDateTimeColumn dcol = new GridViewDateTimeColumn();
            //dcol.Name = COLS.DateTime;
            //dcol.HeaderText = "Date Time";
            //dcol.Width = 130;
            //grdMessages.Columns.Add(dcol);
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.Visible = false;
            grdInbox.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.Name = COLS.DateTime;
            col.HeaderText = "Date Time";
            col.Width = 160;
            col.ReadOnly = true;
            grdInbox.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.Name = COLS.Reciever;
            col.HeaderText = "Sender";
            col.Width = 130;
            col.ReadOnly = true;
            grdInbox.Columns.Add(col);
            col = new DataGridViewTextBoxColumn();
            col.Name = COLS.MessageText;
            col.HeaderText = "Message Text";
            col.Width = 800;
            col.ReadOnly = true;
            grdInbox.Columns.Add(col);
            grdInbox.AllowUserToDeleteRows = true;
        }


        private void PopulateSent()
        {
            try
            {
                grdSent.Rows.Clear();
                var list = (from a in General.GetQueryable<InternalMessaging>(c=>c.SenderName.ToLower()==AppVars.LoginObj.UserName.ToLower())
                            orderby a.AddOn descending
                            select new
                            {
                                Id=a.Id,
                                Sender=a.SenderName,
                                MessageText = a.MessageText,
                                DateTime = string.Format("{0:MM/dd/yyyy HH:mm:ss tt}", a.AddOn.Value)
                            }).ToList();
                int cnt = list.Count;

                int minRows = 8;

                if (cnt < minRows)
                {
                    for (int i = 0; i < minRows - cnt; i++)
                    {
                        // list.Add(new vu_Invoice { Id = data.Id, BookingId = data.BookingId, HasBookedBy = data.HasBookedBy, HasOrderNo = data.HasOrderNo, HasPupilNo = data.HasPupilNo });
                        list.Add(null);
                    }

                }
                int rowCount = list.Count;
                grdSent.RowCount = rowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    if (list[i] != null)
                    {
                        grdSent.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdSent.Rows[i].Cells[COLS.DateTime].Value = list[i].DateTime;
                        grdSent.Rows[i].Cells[COLS.Sender].Value = list[i].Sender != null ? list[i].Sender : "";
                        grdSent.Rows[i].Cells[COLS.MessageText].Value = list[i].MessageText != null ? list[i].MessageText : "";
                    }
                }
                grdSent.MultiSelect = false;
                this.grdSent.CellBorderStyle = DataGridViewCellBorderStyle.None;
               
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void PopulateInbox()
        {
            try
            {
                grdInbox.Rows.Clear();
                //var list = (from a in General.GetQueryable<InternalMessaging>(null) 
                //            join b in General.GetQueryable<UM_User>(null)
                //            on a.ReceiveTo equals b.Id 
                //            orderby a.AddOn ascending
                //            select new
                //            {
                //                Id = a.Id,
                //                Reciever = b.UserName,
                //                MessageText = a.MessageText,
                //                DateTime = string.Format("{0:MM/dd/yyyy HH:mm:ss tt}", a.AddOn.Value)
                //            }).ToList();

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = (from a in db.InternalMessagings
                                join b in db.UM_Users
                                on a.ReceiveTo equals b.Id
                                where a.ReceiveTo==AppVars.LoginObj.LuserId
                                orderby a.AddOn descending
                                select new
                                {
                                    Id = a.Id,
                                    Reciever = a.SenderName,
                                    MessageText = a.MessageText,
                                    DateTime = a.AddOn //!= null ? string.Format("{0:MM/dd/yyyy HH:mm:ss tt}", a.AddOn.Value) : ""
                                }).ToList();


                    int cnt = list.Count;

                    int minRows = 8;

                    if (cnt < minRows)
                    {
                        for (int i = 0; i < minRows - cnt; i++)
                        {
                            // list.Add(new vu_Invoice { Id = data.Id, BookingId = data.BookingId, HasBookedBy = data.HasBookedBy, HasOrderNo = data.HasOrderNo, HasPupilNo = data.HasPupilNo });
                            list.Add(null);
                        }

                    }
                    int rowCount = list.Count;
                    grdInbox.RowCount = rowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (list[i] != null)
                        {
                            grdInbox.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                            grdInbox.Rows[i].Cells[COLS.DateTime].Value = list[i].DateTime;
                            grdInbox.Rows[i].Cells[COLS.Reciever].Value = list[i].Reciever != null ? list[i].Reciever : "";
                            grdInbox.Rows[i].Cells[COLS.MessageText].Value = list[i].MessageText != null ? list[i].MessageText : "";
                        }
                    }
                    grdInbox.MultiSelect = false;
                    this.grdInbox.CellBorderStyle = DataGridViewCellBorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        static DataGridViewHeaderBorderStyle ProperColumnHeadersBorderStyle
        {
            get
            {
                return (SystemFonts.MessageBoxFont.Name == "Segoe UI") ?
                DataGridViewHeaderBorderStyle.None :
                DataGridViewHeaderBorderStyle.Raised;
            }
        }
        private void Group()
        {
            ddlGroup.Items.Add("All");
            ddlGroup.Items.Add("Admin");
            ddlGroup.Items.Add("Controller");
            ddlGroup.Items.Add("Super Admin");
            ddlGroup.SelectedIndex = 0;
            chkWelcome.Checked = false;
        }
   

        private void chkWelcome_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWelcome.Checked)
            {
                chkAll.Checked = false;
               // chkAll.Enabled = false;
            }
            else
            {

                chkAll.Enabled = true;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
          
            if (chkAll.Checked)
            {
                ddlController.SelectedIndex = -1;
                ddlController.Enabled = false;
            }
            else
            {
                ddlController.Enabled = true;

            }
        }
        

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int controllerId = ddlController.SelectedValue.ToInt();

                bool IsAll = chkAll.Checked;


                if (IsAll == false && controllerId == 0)
                {
                    ENUtils.ShowMessage("Required : Controller");
                    return;
                }
                string msg = txtMessage.Text.Trim();


                if (string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage("Required : Message");
                    return;
                }

                if (chkWelcome.Checked)
                {

                    new Taxi_Model.TaxiDataContext().stp_SaveInternalMessage(null, AppVars.LoginObj.UserName.ToStr(), msg, false, true, controllerId);


                }
                else
                {

                    new Taxi_Model.TaxiDataContext().stp_SaveInternalMessage(null, AppVars.LoginObj.UserName.ToStr(), msg, false,false, controllerId);


                    msg = msg.Replace("<<", "").Trim();


                    General.SendMessageToPDA("request broadcast=" + "**internalmessage>>" + controllerId + ">>" + IsAll.ToStr() + ">>" + msg + ">>" + AppVars.LoginObj.LuserId.ToStr()+">>"+AppVars.LoginObj.UserName.ToStr());

                    //  new BroadcasterData().BroadCastToAll("**internalmessage>>" + controllerId + ">>" + IsAll.ToStr() + ">>" + msg + ">>" + AppVars.LoginObj.LuserId.ToStr());
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
            Close();
        }

   

 

    
    }
}
