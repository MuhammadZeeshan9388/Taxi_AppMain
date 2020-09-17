using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Taxi_Model;
using Utils;
using CheckBoxInHeader;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmDriverLoginList : UI.SetupBase
    {
        DriverBO objMaster;

        public frmDriverLoginList()
        {
            InitializeComponent();

       
            //this.radGridView1.CreateCell += new GridViewCreateCellEventHandler(radGridView1_CreateCell);  //second approach
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.EnableFiltering = true;

            this.grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
             

            grdLister.ViewCellFormatting+=new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.ShowRowHeaderColumn = false;
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLister.AutoCellFormatting = true;
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            this.Shown += new EventHandler(frmDriversList_Shown);
        }



        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }

        RadButtonElement button = null;
        string cellValue = string.Empty;
        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
           
            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = _HeaderRowBackColor;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }

            else if (e.CellElement is GridFilterCellElement)
            {
                e.CellElement.Font = oldFont;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.White;
                e.CellElement.RowElement.BackColor = Color.White;
                e.CellElement.RowElement.NumberOfColors = 1;

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
            }

            else if (e.CellElement is GridDataCellElement)
            {


                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    // This is how we get the RadButtonElement instance from the cell
                    button = (RadButtonElement)e.CellElement.Children[0];

                    //if (e.Column.Name == "ColEdit")
                    //{
                    //    button.Image = Resource1.edit2;
                    //}
                    //if (e.Column.Name == "ColDelete")
                    //{

                    //    button.Image = Resources.Resource1.delete;
                    
                    //}
                }

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;

                if (e.CellElement.RowElement.IsSelected == true)
                {

                    e.CellElement.RowElement.NumberOfColors = 1;
                    e.CellElement.RowElement.BackColor = Color.DeepSkyBlue;

                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.DeepSkyBlue;
                    e.CellElement.ForeColor = Color.White;
                    e.CellElement.Font = newFont;




                }


                else
                {
                    e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);
                }


            }
            //new
            if (e.CellElement is GridFilterCellElement && e.CellElement.ColumnInfo.Name == "Select")
            {
                e.CellElement.Children.Clear();
            }

           

        }


        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btnlogout")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to logout this Driver ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {


                    RadGridView grid = gridCell.GridControl;
                    if( Logout(grid.CurrentRow.Cells["Id"].Value.ToLong(),grid.CurrentRow.Cells["DriverId"].Value.ToIntorNull()))
                    {

                        General.AddUserLog("Driver {" + grid.CurrentRow.Cells["DriverNo"].Value.ToStr() + "} manually logout by Controller", 3);
                    }
                    PopulateData();

                    General.BroadCastRefresh(RefreshTypes.REFRESH_DASHBOARD_DRIVER);

                }
            }
           
        }


        void frmDriversList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.InitializeForm("frmDriver");
            }
            catch
            {


            }

            grdLister.AllowAutoSizeColumns = true;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.None;
            grdLister.EnableHotTracking = false;
            grdLister.ShowGroupPanel = false;
            grdLister.AllowAddNewRow = false;
            grdLister.AllowEditRow = false;


            //General.AddCheckBoxColumn("chkLogout", grdLister);
            this.AddCheckColumn();
            
            ddlColumns.Items.Add("DriverNo");
            ddlColumns.Items.Add("Name");
            ddlColumns.SelectedIndex = 0;

            PopulateData();

            AddLogoutColumn(grdLister);

            if (grdLister.Columns.Count > 2)
            {
                grdLister.Columns["Id"].IsVisible = false;

                grdLister.Columns["DriverId"].IsVisible = false;

                //grdLister.Columns["Address"].Width = 200;
                grdLister.Columns["DriverNo"].Width = 100;

                (grdLister.Columns["LoginDateTime"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
                (grdLister.Columns["LoginDateTime"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


                grdLister.Columns["LoginDateTime"].HeaderText = "Login Date-Time";
                grdLister.Columns["LoginDateTime"].Width = 190;
                //grdLister.Columns["Email"].Width = 130;
                // grdLister.Columns["Phone"].Width = 130;
                grdLister.Columns["Name"].Width = 300;

                grdLister.AllowEditRow = true;
            }

            ////start adil 21/05/13
            //ddlColumns.Items.AddRange(grdLister.Columns.Where(c => c.Name != "Id" && c.Name != "btnEdit" && c.Name != "btnDelete" && c.Name != "DriverId" && c.Name != "btnLogout" && c.Name != "LoginDateTime" && c.Name!="Select")
            //                            .Select(c => c.Name));
           

            // End of lines
        }

        private void AddLogoutColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.BestFit();
            col.Width = 70;
            col.Name = "btnLogout";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Log out";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }


        private bool Logout(long id, int? driverId)
        {
            bool rtn = false;
            try
            {

                DriverQueueBO objMaster = new DriverQueueBO();
                objMaster.GetByPrimaryKey(id);

                if (objMaster.Current != null)
                {
                    objMaster.Current.LogoutDateTime = DateTime.Now;
                    objMaster.Current.Status = false;

                    objMaster.Save();

                    rtn = true;
                   
                    if (objMaster.Current.Fleet_Driver.HasPDA.ToBool())
                    {


                        new Thread(delegate()
                        {
                            General.SendMessageToPDA("request force logout=" + objMaster.Current.Fleet_Driver.DriverNo + "=" + driverId);
                        }).Start();

                      //  SendMessage("request force logout=" + objMaster.Current.Fleet_Driver.DriverNo);

                    }
                }               


            
            }
            catch (Exception ex)
            {
                //ENUtils.ShowMessage(ex.Message);


            }
           

                return rtn;

            
        }

        private void SendMessage(string msg)
        {
            try
            {
                if (!string.IsNullOrEmpty(AppVars.objPolicyConfiguration.ListenerIP.ToStr()))
                {

                    byte[] data = Encoding.UTF8.GetBytes(msg);

                    TcpClient tcpClient = new TcpClient();
                    tcpClient.Connect(new IPEndPoint(IPAddress.Parse(AppVars.objPolicyConfiguration.ListenerIP.ToStr()), 1101));
                    tcpClient.SendTimeout = 3000;
                    tcpClient.ReceiveTimeout = 3000;
                    tcpClient.GetStream().Write(data, 0, data.Length);

                    tcpClient.Close();
                }
            }
            catch (Exception ex)
            {


            }
        }


       


        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ViewDetailForm();
        }

        private void ViewDetailForm()
        {

            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
                ShowDriverForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }


        private void ShowDriverForm(int id)
        {


            frmDriver frm = new frmDriver();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();

        }


        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                //if (this.CanDelete == false)
                //{
                //    ENUtils.ShowMessage("Permission Denied");
                //    e.Cancel = true;
                //}
                //else
                //{
                    objMaster = new DriverBO();

                    try
                    {

                        objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                        objMaster.Delete(objMaster.Current);


                    }
                    catch (Exception ex)
                    {
                        if (objMaster.Errors.Count > 0)
                            ENUtils.ShowMessage(objMaster.ShowErrors());
                        else
                        {
                            ENUtils.ShowMessage(ex.Message);

                        }
                        e.Cancel = true;

                    }
                //}

            }
        }



        Font f = new Font("Tahoma", 10, FontStyle.Bold);
        Color clr = Color.Yellow;
        private void grdLister_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column != null && e.Row != null && e.Row.Cells["Id"].Value != null)
            {
                if (e.Column.Name == "Name")
                {
                    e.CellElement.Font = f;

                }


            }
        }

        
       


        public override void RefreshData()
        {
            PopulateData();
        }

        public override void PopulateData()
        {
            //Start lines adil 21/5/13
            string searchTxt = txtSearch.Text.ToLower().Trim();
            string col = ddlColumns.Text.Trim().ToLower();

         
            bool col_driverno = false;
            bool col_name = false;
         
            if (col == string.Empty)
            {
                col_name = true;
            }
           
            else if (col == "driverno")
            {
                col_driverno = true;
            }
            else if (col == "name")
            {
                col_name = true;
            }
           
           
 

            //End of lines



          
            //var data1 = General.GetQueryable<Fleet_DriverQueueList>(null).AsEnumerable().OrderBy(item => item.Fleet_Driver.DriverNo, new NaturalSortComparer<string>());
            
            //var query = (from a in data1
            //             where a.Status.ToBool() &&
            //               ((col_driverno && (a.Fleet_Driver.DriverNo.ToStr().ToLower()== searchTxt 
            //               || searchTxt == string.Empty))
            //               || (col_name && (a.Fleet_Driver.DriverName.ToStr().ToLower().Contains(searchTxt)
            //               || searchTxt == string.Empty)) )


            //             select new
            //             {
            //                 Id = a.Id,
            //                 DriverId = a.Fleet_Driver.Id,
            //                 DriverNo = a.Fleet_Driver.DriverNo,
            //                 Name = a.Fleet_Driver.DriverName,
            //                 LoginDateTime = a.LoginDateTime
            //             }).ToList();


         //   var data1 = General.GetQueryable<Fleet_DriverQueueList>(null).AsEnumerable().OrderBy(item => item.Fleet_Driver.DriverNo, new NaturalSortComparer<string>());

            using (TaxiDataContext db = new TaxiDataContext())
            {

                var query = (from a in db.GetTable<Fleet_DriverQueueList>()
                             where a.Status!=null && a.Status==true &&
                               ((col_driverno && (a.Fleet_Driver.DriverNo.ToLower() == searchTxt
                               || searchTxt == string.Empty))
                               || (col_name && (a.Fleet_Driver.DriverName.ToLower().Contains(searchTxt)
                               || searchTxt == string.Empty)))


                             select new
                             {
                                 Id = a.Id,
                                 DriverId = a.Fleet_Driver.Id,
                                 DriverNo = a.Fleet_Driver.DriverNo,
                                 Name = a.Fleet_Driver.DriverName,
                                 LoginDateTime = a.LoginDateTime
                             }).ToList().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>());


                grdLister.DataSource = query;

            
            }

            lblTotalLogins.Text = "Total Login Driver(s) : " + grdLister.Rows.Count.ToStr();

            
        }

        private void btnMultiLogout_Click(object sender, EventArgs e)
        {
            var selectedRows = grdLister.Rows.Where(c => c.Cells["Select"].Value.ToBool());

             if (selectedRows.Count() > 0)
             {
                 foreach (var item in selectedRows)
                 {

                     Logout(item.Cells["Id"].Value.ToLong(), item.Cells["DriverId"].Value.ToIntorNull());

                 }

                General.AddUserLog("Driver(s) {" +string.Join(",",selectedRows.Select(c=>c.Cells["DriverNo"].Value.ToStr())) + "} manually logout by Controller", 3);


                PopulateData();

                 General.BroadCastRefresh(RefreshTypes.REFRESH_DASHBOARD_DRIVER);
             
             }
        }

        private void AddCheckColumn()
        {
            CustomCheckBoxColumn1 checkColumn = new CustomCheckBoxColumn1();
            checkColumn.Name = "Select";
            //checkColumn.HeaderText = "All";
            this.grdLister.Columns.Insert(0, checkColumn);
        }
        void grdLister_CreateCell(object sender, GridViewCreateCellEventArgs e)
        {

            if (e.CellType == typeof(GridHeaderCellElement) && e.Column.HeaderText == "All")
            {
                e.CellType = typeof(CheckBoxHeaderCell);
            }
        }
        public class CustomCheckBoxColumn1 : GridViewCheckBoxColumn
        {
            public override Type GetCellType(GridViewRowInfo row)
            {
                if (row is GridViewTableHeaderRowInfo)
                {
                    return typeof(CheckBoxHeaderCell);
                }
                return base.GetCellType(row);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void btnShowAllCompanies_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            ddlColumns.SelectedIndex = 0;
            PopulateData();

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                PopulateData();
            }
        }

      
     

    }
}
