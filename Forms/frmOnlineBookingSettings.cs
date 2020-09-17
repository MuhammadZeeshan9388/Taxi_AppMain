using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using CheckBoxInHeader;

namespace Taxi_AppMain
{
    public partial class frmOnlineBookingSettings : UI.SetupBase
    {
        OnlineBookingSettingBO objOnlineBookingSetting = null;
        public frmOnlineBookingSettings()
        {
            InitializeComponent();
            btnSave.Click += new EventHandler(btnSave_Click);
            btnExitForm.Click += new EventHandler(btnExitForm_Click);
            FormateGrid();
            FormateOnlineBookingSettingsGrid();
            objOnlineBookingSetting = new OnlineBookingSettingBO();
            grdOnlineBookingSettings.CellEndEdit += GrdOnlineBookingSettings_CellEndEdit;
            this.chkDisableWebBooking.ToggleStateChanged += ChkDisableWebBooking_ToggleStateChanged;
            this.grdZones.ViewCellFormatting += GrdZones_ViewCellFormatting;

        }


        private void GrdZones_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {



                if (e.CellElement is GridDataCellElement)
                {
                    if (e.Column.Name == COL_ZONE.BlockPickup)
                    {
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.DrawFill = true;

                        if (e.Row.Cells[COL_ZONE.BlockPickup].Value.ToBool())
                        {

                            e.CellElement.BackColor = Color.Pink;
                            // e.CellElement.ForeColor = Color.FromArgb(textColor.ToInt());
                        }
                        else
                        {
                            e.CellElement.BackColor = Color.Transparent;
                        }

                    }
                    else if (e.Column.Name == COL_ZONE.IsLocalArea)
                    {
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.DrawFill = true;

                        if (e.Row.Cells[COL_ZONE.IsLocalArea].Value.ToBool())
                        {

                            e.CellElement.BackColor = Color.Pink;
                            // e.CellElement.ForeColor = Color.FromArgb(textColor.ToInt());
                        }
                        else
                        {
                            e.CellElement.BackColor = Color.Transparent;
                        }

                    }
                }
            }
            catch
            {


            }
        }

        private void ChkDisableWebBooking_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                grdOnlineBookingSettings.Enabled = false;
                grdZones.Enabled = false;
                txtDescription.Enabled = true;
            }
            else
            {
                grdOnlineBookingSettings.Enabled = true;
                grdZones.Enabled = true;
              
                txtDescription.Enabled = false;
            }
            //throw new NotImplementedException();
        }

        private void GrdOnlineBookingSettings_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.Row.Cells[COL_ZONE.DateWise].Value.ToBool())
            {
                e.Row.Cells[COL_ZONE.FromDateTime].ReadOnly = false;
                e.Row.Cells[COL_ZONE.TillDateTime].ReadOnly = false;

                //e.Row.Cells[COL_ZONE.FromDateTime].Value = DateTime.Now.ToDate();
                //e.Row.Cells[COL_ZONE.TillDateTime].Value = DateTime.Now.ToDate();

            }
            else
            {
                e.Row.Cells[COL_ZONE.FromDateTime].ReadOnly = true;
                e.Row.Cells[COL_ZONE.TillDateTime].ReadOnly = true;
            }
        }

        void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmOnlineBookingSettings_Load(object sender, EventArgs e)
        {
            PopulateZones();
        }

        public struct COL_ZONE
        {
            public static string Id = "Id";
            public static string PlotNo = "PlotNo";
            public static string PlotName = "PlotName";
            public static string IsActive = "IsActive";


            public static string BlockPickup = "BlockPickup";
            public static string BlockDropOff = "BlockDropOff";
            public static string IsLocalArea = "IsLocalArea";

            public static string OnlineBookingSettingId = "OnlineBookingSettingId";
            public static string DateWise = "DateWise";
            public static string FromDateTime = "FromDateTime";
            public static string TillDateTime = "TillDateTime";

            public static string FromTime = "FromTime";
            public static string TillTime = "TillTime";

            public static string Description = "Description";


        }

        private void PopulateZones()
        {
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {


                    db.DeferredLoadingEnabled = false;

                    //var data1 = General.GetQueryable<Gen_Zone>(null).OrderBy(c => c.ZoneName).ToList();

                    var data1 = db.Gen_Zones.Where(c => c.ZoneName != "SIN BIN" && c.OrderNo != null && c.ZoneName != "OnBreak").OrderBy(c => c.OrderNo).ToList();

                    //var data1 = General.GetQueryable<Gen_Zone>(c => c.ZoneName != "SIN BIN" && c.OrderNo != null && c.ZoneName != "OnBreak").OrderBy(c => c.OrderNo).ToList();
                    //var OnlineBookingSetting = General.GetQueryable<OnlineBookingSetting>(null).ToList().FirstOrDefault();

                    grdZones.RowCount = data1.Count;

                    for (int i = 0; i < data1.Count; i++)
                    {
                        grdZones.Rows[i].Cells[COL_ZONE.Id].Value = data1[i].Id;
                        grdZones.Rows[i].Cells[COL_ZONE.PlotName].Value = data1[i].OrderNo + " - " + data1[i].ZoneName;
                        grdZones.Rows[i].Cells[COL_ZONE.BlockPickup].Value = data1[i].BlockPickup;
                        grdZones.Rows[i].Cells[COL_ZONE.BlockDropOff].Value = data1[i].BlockDropOff;
                        grdZones.Rows[i].Cells[COL_ZONE.IsLocalArea].Value = data1[i].IsOutsideArea;


                        // grdZones.Rows[i].Cells[COL_ZONE.IsActive].Value = data1[i].DisableOnlineBooking.ToBool();
                    }

                    var objOnlineBookingSettings = db.OnlineBookingSettings.FirstOrDefault();
                    if (objOnlineBookingSettings != null)
                    {
                        chkDisableAppBooking.Checked = objOnlineBookingSettings.BlockAppBooking.ToBool();
                        chkDisableWebBooking.Checked = objOnlineBookingSettings.BlockWebBooking.ToBool();
                        txtDescription.Text = objOnlineBookingSettings.Description;
                        //objOnlineBookingSetting.Current.Description = txtDescription.Text.Trim();
                    }

                    var list = db.OnlineBookingSettings_Details.Where(c => c.OnlineBookingSettingId != null).OrderByDescending(c => c.FromDateTime).ToList();


                    //foreach (var item in list)
                    grdOnlineBookingSettings.RowCount = list.Count;
                    for (int i = 0; i < list.Count; i++)
                    {
                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.Id].Value = list[i].Id;
                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.OnlineBookingSettingId].Value = list[i].OnlineBookingSettingId;
                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.FromDateTime].Value = list[i].FromDateTime;
                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.TillDateTime].Value = list[i].TillDateTime;

                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.FromTime].Value = list[i].FromDateTime;
                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.TillTime].Value = list[i].TillDateTime;

                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.Description].Value = list[i].Description;

                        grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.DateWise].Value = list[i].DateWise;
                        if (list[i].DateWise.ToBool() == false)
                        {
                            grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.FromDateTime].ReadOnly = true;
                            grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.TillDateTime].ReadOnly = true;
                            grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.FromDateTime].Value = null;
                            grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.TillDateTime].Value = null;
                        }

                        //if (list[i].FromDateTime == null)
                        //{
                        //    grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.FromDateTime].Value = DateTime.Now.ToDate();
                        //}
                        //if (list[i].TillDateTime == null)
                        //{
                        //    grdOnlineBookingSettings.Rows[i].Cells[COL_ZONE.TillDateTime].Value = DateTime.Now.ToDate();
                        //}

                    }
                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }

        }

        private void FormateGrid()
        {
            try
            {

                grdZones.ReadOnly = false;
                grdZones.AllowEditRow = true;
                //grdZones.AllowAutoSizeColumns = true;
                grdZones.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                grdZones.ShowRowHeaderColumn = false;
                grdZones.AllowAddNewRow = false;
                grdZones.ShowGroupPanel = false;


                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COL_ZONE.Id;
                grdZones.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COL_ZONE.PlotName;
                col.HeaderText = "Plot Name";
                col.ReadOnly = true;
                col.Width = 130;
                grdZones.Columns.Add(col);


                GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
                //cbcol.Name = COL_ZONE.IsActive;
                //cbcol.Width = 100;
                //cbcol.ReadOnly = false;
                //grdZones.Columns.Add(cbcol);

                cbcol = new GridViewCheckBoxColumn();
                cbcol.Name = COL_ZONE.IsLocalArea;
                cbcol.HeaderText = "Outside Area";
                cbcol.Width = 100;
                cbcol.ReadOnly = false;
                grdZones.Columns.Add(cbcol);


                cbcol = new GridViewCheckBoxColumn();
                cbcol.Name = COL_ZONE.BlockPickup;
                cbcol.HeaderText = "Block Pickup";
                cbcol.Width = 100;
                cbcol.ReadOnly = false;
                grdZones.Columns.Add(cbcol);

                cbcol = new GridViewCheckBoxColumn();
                cbcol.Name = COL_ZONE.BlockDropOff;
                cbcol.HeaderText = "Out Of Town";
                cbcol.Width = 100;
                cbcol.IsVisible = true;
                cbcol.ReadOnly = false;
                grdZones.Columns.Add(cbcol);



                grdZones.EnableFiltering = true;
                //UI.GridFunctions.SetFilter(grdZones);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void FormateOnlineBookingSettingsGrid()
        {
            try
            {

                //grdZones.ReadOnly = false;
                grdOnlineBookingSettings.AllowEditRow = true;
                //grdZones.AllowAutoSizeColumns = true;
                grdOnlineBookingSettings.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                grdOnlineBookingSettings.ShowRowHeaderColumn = false;
                // grdOnlineBookingSettings.AllowAddNewRow = false;
                grdOnlineBookingSettings.ShowGroupPanel = false;

                grdOnlineBookingSettings.AddNewRowPosition = SystemRowPosition.Top;
                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COL_ZONE.Id;
                grdOnlineBookingSettings.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COL_ZONE.OnlineBookingSettingId;
                //col.HeaderText = "Plot Name";
                col.ReadOnly = true;
                col.IsVisible = false;
                col.Width = 130;
                grdOnlineBookingSettings.Columns.Add(col);

                GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();

                cbcol.Name = COL_ZONE.DateWise;
                cbcol.HeaderText = "Date Wise";
                cbcol.Width = 90;
                cbcol.ReadOnly = false;
                grdOnlineBookingSettings.Columns.Add(cbcol);

                GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
                dtcol.Name = COL_ZONE.FromDateTime;
                dtcol.HeaderText = "Start Date";

                dtcol.CustomFormat = "dd/MM/yyyy";
                dtcol.FormatString = "{0:dd/MM/yyyy}";
                dtcol.Width = 90;
                grdOnlineBookingSettings.Columns.Add(dtcol);


                dtcol = new GridViewDateTimeColumn();
                dtcol.Name = COL_ZONE.FromTime;
                dtcol.HeaderText = "Start Time";

                dtcol.CustomFormat = "HH:mm";
                dtcol.FormatString = "{0:HH:mm}";
                dtcol.Width = 90;
                grdOnlineBookingSettings.Columns.Add(dtcol);

                dtcol = new GridViewDateTimeColumn();
                dtcol.Name = COL_ZONE.TillDateTime;
                dtcol.HeaderText = "End Date";
                dtcol.CustomFormat = "dd/MM/yyyy";
                dtcol.FormatString = "{0:dd/MM/yyyy}";
                dtcol.Width = 100;
                grdOnlineBookingSettings.Columns.Add(dtcol);


                dtcol = new GridViewDateTimeColumn();
                dtcol.Name = COL_ZONE.TillTime;
                dtcol.HeaderText = "End Time";

                dtcol.CustomFormat = "HH:mm";
                dtcol.FormatString = "{0:HH:mm}";
                dtcol.Width = 90;
                grdOnlineBookingSettings.Columns.Add(dtcol);

                col = new GridViewTextBoxColumn();
                col.Name = COL_ZONE.Description;
                col.HeaderText = "Message";
                col.ReadOnly = false;

                col.Width = 160;
                grdOnlineBookingSettings.Columns.Add(col);

                GridViewCommandColumn cmd = new GridViewCommandColumn();
                cmd.Width = 80;
                cmd.Name = "btnDelete";
                cmd.UseDefaultText = true;
                cmd.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                cmd.DefaultText = "Delete";
                cmd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                grdOnlineBookingSettings.Columns.Add(cmd);
                grdOnlineBookingSettings.CommandCellClick += GrdOnlineBookingSettings_CommandCellClick;

                // grdZones.EnableFiltering = true;
                //UI.GridFunctions.SetFilter(grdZones);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void GrdOnlineBookingSettings_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Entery? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        int Id = grdOnlineBookingSettings.CurrentRow.Cells[COL_ZONE.Id].Value.ToInt();
                        if (Id > 0)
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                var query = db.OnlineBookingSettings_Details.FirstOrDefault(c => c.Id == Id);
                                db.OnlineBookingSettings_Details.DeleteOnSubmit(query);
                                db.SubmitChanges();
                            }
                        }
                        RadGridView grid = gridCell.GridControl;

                        grid.CurrentRow.Delete();

                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public override void Save()
        {
            SaveOnlineBooking();
            UpdateZones();
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            Save();

        }
        private void UpdateZones()
        {
            ZoneBO objMaster = null;
            try
            {
                objMaster = new ZoneBO();
                for (int i = 0; i < grdZones.RowCount; i++)
                {
                    int Id = grdZones.Rows[i].Cells[COL_ZONE.Id].Value.ToInt();

                    objMaster.GetByPrimaryKey(Id);
                    objMaster.Edit();

                    objMaster.Current.BlockPickup = grdZones.Rows[i].Cells[COL_ZONE.BlockPickup].Value.ToBool();
                    objMaster.Current.BlockDropOff = grdZones.Rows[i].Cells[COL_ZONE.BlockDropOff].Value.ToBool();
                    objMaster.Current.IsOutsideArea = grdZones.Rows[i].Cells[COL_ZONE.IsLocalArea].Value.ToBool();


                    objMaster.Save();
                    objMaster.Clear();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }

        public void SaveOnlineBooking()
        {
            try
            {

                //var query = General.GetObject<OnlineBookingSetting>(a => a.Id != null).DefaultIfEmpty().Id;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var query = db.OnlineBookingSettings.FirstOrDefault();
                    if (query != null)
                    {
                        objOnlineBookingSetting.GetByPrimaryKey(query.Id);
                    }
                }
                if (objOnlineBookingSetting.PrimaryKeyValue != null)
                {
                    objOnlineBookingSetting.Edit();
                }
                else
                {
                    objOnlineBookingSetting.New();
                }

                objOnlineBookingSetting.Current.BlockAppBooking = chkDisableWebBooking.Checked;
                objOnlineBookingSetting.Current.BlockWebBooking = chkDisableWebBooking.Checked;
                objOnlineBookingSetting.Current.Description = txtDescription.Text.Trim();

                string[] skipProperties = { "OnlineBookingSetting" };

                //r.Cells[COL_ZONE.FromTime].Value.ToDateT.TimeOfDay

                DateTime NullDate = new DateTime(1900, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).ToDate();
                IList<OnlineBookingSettings_Detail> savedList3 = objOnlineBookingSetting.Current.OnlineBookingSettings_Details;
                List<OnlineBookingSettings_Detail> listofDetail3 = (from r in grdOnlineBookingSettings.Rows

                                                                    select new OnlineBookingSettings_Detail
                                                                    {
                                                                        Id = r.Cells[COL_ZONE.Id].Value.ToInt(),
                                                                        OnlineBookingSettingId = r.Cells[COL_ZONE.OnlineBookingSettingId].Value.ToInt(),

                                                                        FromDateTime = r.Cells[COL_ZONE.DateWise].Value.ToBool() == true ?
                                                                        (r.Cells[COL_ZONE.FromDateTime].Value.ToDateorNull() != null ? (string.Format("{0:dd/MM/yyyy HH:mm}", r.Cells[COL_ZONE.FromDateTime].Value.ToDateorNull() + Convert.ToDateTime(r.Cells[COL_ZONE.FromTime].Value).TimeOfDay).ToDateTime())
                                                                                                                                                : r.Cells[COL_ZONE.FromDateTime].Value.ToDateorNull()) : (string.Format("{0:dd/MM/yyyy HH:mm}", (NullDate + r.Cells[COL_ZONE.FromTime].Value.ToDateTime().TimeOfDay).ToDateTime())).ToDateTime(),

                                                                        //

                                                                        TillDateTime = r.Cells[COL_ZONE.DateWise].Value.ToBool() == true ?
                                                                        (r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull() != null ? (string.Format("{0:dd/MM/yyyy HH:mm}", r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull() + Convert.ToDateTime(r.Cells[COL_ZONE.TillTime].Value).TimeOfDay).ToDateTime())
                                                                                                                                                : r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull()) : (string.Format("{0:dd/MM/yyyy HH:mm}", (NullDate + r.Cells[COL_ZONE.TillTime].Value.ToDateTime().TimeOfDay).ToDateTime())).ToDateTime(),
                                                                        //TillDateTime = r.Cells[COL_ZONE.DateWise].Value.ToBool() == true ? (r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull() != null ? r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull() + Convert.ToDateTime(r.Cells[COL_ZONE.TillTime].Value).TimeOfDay 

                                                                        //: 


                                                                        //r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull())
                                                                        //: 
                                                                        //(NullDate + Convert.ToDateTime(r.Cells[COL_ZONE.TillTime].Value).TimeOfDay),
                                                                        //FromDateTime = r.Cells[COL_ZONE.DateWise].Value.ToBool()==false?(r.Cells[COL_ZONE.FromDateTime].Value.ToDateorNull()!=null?r.Cells[COL_ZONE.FromDateTime].Value.ToDateorNull()+ Convert.ToDateTime(r.Cells[COL_ZONE.FromTime].Value).TimeOfDay: r.Cells[COL_ZONE.FromDateTime].Value.ToDateorNull()) : r.Cells[COL_ZONE.FromDateTime].Value.ToDateorNull(),
                                                                        //TillDateTime = r.Cells[COL_ZONE.DateWise].Value.ToBool() == false ? (r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull() != null ? r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull() + Convert.ToDateTime(r.Cells[COL_ZONE.TillTime].Value).TimeOfDay : r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull()) : r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull(),
                                                                        //TillDateTime = r.Cells[COL_ZONE.TillDateTime].Value.ToDateorNull(),
                                                                        Description = r.Cells[COL_ZONE.Description].Value.ToStr(),
                                                                        DateWise = r.Cells[COL_ZONE.DateWise].Value.ToBool()

                                                                    }).ToList();


                Utils.General.SyncChildCollection(ref savedList3, ref listofDetail3, "Id", skipProperties);


                objOnlineBookingSetting.Save();




            }
            catch (Exception ex)
            {
                if (objOnlineBookingSetting.Errors.Count > 0)
                    ENUtils.ShowMessage(objOnlineBookingSetting.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }


        }


        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (chkAll.Checked == true)
            //{
            //    for (int i = 0; i < grdZones.RowCount; i++)
            //    {
            //        grdZones.Rows[i].Cells[COL_ZONE.IsActive].Value = true;

            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < grdZones.RowCount; i++)
            //    {
            //        grdZones.Rows[i].Cells[COL_ZONE.IsActive].Value = false;

            //    }
            //}
        }


    }
}
