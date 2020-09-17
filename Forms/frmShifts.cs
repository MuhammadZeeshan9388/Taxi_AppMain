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

namespace Taxi_AppMain
{
    public partial class frmShifts : UI.SetupBase
    {
        DriverBO objdriver;
        DriverShiftsBO objMaster;
        public frmShifts()
        {
            InitializeComponent();
            objdriver = new DriverBO();
            objMaster = new DriverShiftsBO();
            FormateShiftGride();
            ComboFunctions.FillDriverNoCombo(ddlDriver);
            ComboFunctions.FillDriverShiftsCombo(ddlShifts);
            grdShifts.CellDoubleClick += new GridViewCellEventHandler(grdShifts_CellDoubleClick);
            grdShifts.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
        }
        private void frmShifts_Load(object sender, EventArgs e)
        {
            dtpFromTime.Value = DateTime.Now.ToDateorNull();
            dtpTOTime.Value = DateTime.Now.ToDateorNull();
        }
        private void frmShifts_Shown(object sender, EventArgs e)
        {
            
        }
        public struct COL_SHIFT
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string SHIFT = "SHIFT";

            public static string SHIFT_ID = "SHIFT_ID";

            public static string FROMTIME = "FROMTIME";

            public static string TOTIME = "TOTIME";


        }
        void grdShifts_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            int? DriverID = ddlDriver.SelectedValue.ToInt();
            if (e.Row is GridViewDataRowInfo)
            {
                ddlShifts.SelectedValue = e.Row.Cells[COL_SHIFT.SHIFT_ID].Value.ToInt();
                dtpFromTime.Value = e.Row.Cells[COL_SHIFT.FROMTIME].Value.ToDateTime();
                dtpTOTime.Value = e.Row.Cells[COL_SHIFT.TOTIME].Value.ToDateTime();
                

            }
            Fleet_Driver_Shift obj = General.GetObject<Fleet_Driver_Shift>(c => c.DriverId == DriverID);
            if (obj != null)
            {
                int ID = obj.Id.ToInt();

                objMaster.PrimaryKeyValue = ID;
            }
        }
       

        //private void AddAvailability()
        //{
        //    //DateTime? becameAvail = dtpAvailDate.Value;
        //    //DateTime? endDate = dtpEndingDate.Value;

        //    DateTime? From = dtpFromTime.Value;
        //    DateTime? To = dtpTOTime.Value.Value;
        //    int? DriverShiftID = ddlShifts.SelectedValue.ToInt();
        //    string DriverShift = ddlShifts.SelectedItem.Text.ToStr();


        //    string error = string.Empty;

        //    if (From == null)
        //    {
        //        error += "Required : Became Available Date";
        //    }

        //    //if (To != null && To < from)
        //    if (To < From)
        //    {
        //        error += "Required : TO Time must be greater than End Time";
        //    }


        //    if (!string.IsNullOrEmpty(error))
        //    {
        //        ENUtils.ShowMessage(error);
        //        return;
        //    }


        //    GridViewRowInfo row = null;

        //    if (grdAvailability.CurrentRow != null)
        //    {
        //        row = grdAvailability.CurrentRow;
        //    }

        //    else
        //    {
        //        row = grdAvailability.Rows.AddNew();

        //    }

        //    row.Cells[COL_SHIFT.SHIFT_ID].Value = DriverShiftID;
        //    row.Cells[COL_SHIFT.SHIFT].Value = DriverShift;
        //    row.Cells[COL_SHIFT.FROMTIME].Value = From;
        //    row.Cells[COL_SHIFT.TOTIME].Value = To;




        //    ClearAvailability();

        //}

        private void AddShift()
        {

            try
            {

                DateTime? From = dtpFromTime.Value;
                DateTime? To = dtpTOTime.Value;

                int? DriverShiftID = ddlShifts.SelectedValue.ToInt();
                string DriverShift = ddlShifts.SelectedItem.Text.ToStr();


                string error = string.Empty;

                if (From == null)
                {
                    error += "Required : From Time";
                }
                if (To == null)
                {
                    error += "Required : To Time";
                }
                if (DriverShiftID == 0)
                {
                    error += "Required : Driver Shift";
                }

                //if (To != null && To < from)
                if (To < From)
                {
                    error += "Required : TO Time must be greater than End Time";
                }


                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }


                GridViewRowInfo row = null;

                if (grdShifts.CurrentRow != null)
                {
                    row = grdShifts.CurrentRow;
                }

                else
                {
                    if (grdShifts.Rows.Count > 0)
                    {
                        for (int index = 0; index < grdShifts.Rows.Count; index++)
                        {
                            int? ShiftID = grdShifts.Rows[index].Cells["SHIFT_ID"].Value.ToInt();

                            TimeSpan TimeNow = DateTime.Now.TimeOfDay;

                            DateTime FTime = grdShifts.Rows[index].Cells["FROMTIME"].Value.ToDateTime();
                            TimeSpan FromTime;
                            FromTime = FTime.TimeOfDay;


                            DateTime TTime = grdShifts.Rows[index].Cells["TOTIME"].Value.ToDateTime();
                            TimeSpan ToTime;
                            ToTime = TTime.TimeOfDay;

                            DateTime DTPF = dtpFromTime.Value.ToDateTime();
                            TimeSpan DTPFROM;
                            DTPFROM = DTPF.TimeOfDay;

                            DateTime DTPT = dtpTOTime.Value.ToDateTime();
                            TimeSpan DTPTO;
                            DTPTO = DTPT.TimeOfDay;

                            if (DriverShiftID == 7)
                            {
                                if (grdShifts.Rows.Count > 0)
                                {
                                    ENUtils.ShowMessage("Please Remove All Driver Shift.");
                                    return;
                                }

                            }
                            if (ShiftID == DriverShiftID)
                            {
                                ENUtils.ShowMessage("Shift Already In a List");
                                FormateGride();
                                return;

                            }
                            if (ToTime > DTPFROM)
                            {
                                ENUtils.ShowMessage("This timings already in " + grdShifts.Rows[index].Cells["SHIFT"].Value.ToString() + " Shift.");
                                FormateGride();
                                return;
                            }
                            if (ShiftID == 7)
                            {
                                ENUtils.ShowMessage("Please Remove Any Time Shift");
                                return;
                            }
                        }
                    }

                    row = grdShifts.Rows.AddNew();
                }
                row.Cells[COL_SHIFT.SHIFT_ID].Value = DriverShiftID;
                row.Cells[COL_SHIFT.SHIFT].Value = DriverShift;
                row.Cells[COL_SHIFT.FROMTIME].Value = From;
                row.Cells[COL_SHIFT.TOTIME].Value = To;

                ClearShift();
                FormateGride();
            }
            catch (Exception ex)
            {

            }
        }
        private void FormateGride()
        {
            grdShifts.Columns["FROMTIME"].Width = 100;
            grdShifts.Columns["TOTIME"].Width = 100;
            grdShifts.Columns["SHIFT"].Width = 200;
            grdShifts.Columns["SHIFT_ID"].IsVisible = false;

        }
        private void ClearShift()
        {
            dtpFromTime.Value = null;
            dtpTOTime.Value = null;
            grdShifts.CurrentRow = null;
            dtpFromTime.Focus();

        }
        private void FormateShiftGride()
        {
            grdShifts.AllowAutoSizeColumns = true;
            grdShifts.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdShifts.AllowAddNewRow = false;
            grdShifts.ShowGroupPanel = false;
            grdShifts.AutoCellFormatting = true;
            grdShifts.ShowRowHeaderColumn = false;

            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SHIFT.ID;
            grdShifts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SHIFT.MASTERID;
            grdShifts.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Shifts ID";
            col.IsVisible = false;
            col.Name = COL_SHIFT.SHIFT_ID;
            grdShifts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Shifts";
            col.IsVisible = true;
            col.Name = COL_SHIFT.SHIFT;
            col.Width = 200;
            grdShifts.Columns.Add(col);

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "From Time";
            colDate.Name = COL_SHIFT.FROMTIME;
            colDate.Format = DateTimePickerFormat.Custom;
            //colDate.CustomFormat = "dd/MM/yyyy";
            //colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            colDate.Width = 80;
            col.ReadOnly = false;
            grdShifts.Columns.Add(colDate);

            colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "To Time";
            colDate.Name = COL_SHIFT.TOTIME;
            colDate.Format = DateTimePickerFormat.Custom;
            //colDate.CustomFormat = "dd/MM/yyyy";
            //colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            colDate.Width = 80;
            col.ReadOnly = false;
            grdShifts.Columns.Add(colDate);


            //UI.GridFunctions.AddDeleteColumn(grdShifts);

            grdShifts.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

           
                grdShifts.AddDeleteColumn();
                grdShifts.Columns["btnDelete"].Width = 70;
            
            UI.GridFunctions.SetFilter(grdShifts);


        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public override void Save()
        {
            try
            {
               
                int? DriverID = ddlDriver.SelectedValue.ToInt();
                if (objDriverMaster.PrimaryKeyValue != null)
                {
                    objDriverMaster.Edit();


                    string[] skipProperties = { "Fleet_Driver", "Driver_Shift" };

                    IList<Fleet_Driver_Shift> savedList3 = objDriverMaster.Current.Fleet_Driver_Shifts;
                    List<Fleet_Driver_Shift> listofDetail3 = (from r in grdShifts.Rows

                                                              select new Fleet_Driver_Shift
                                                              {
                                                                  Id = r.Cells[COL_SHIFT.ID].Value.ToInt(),
                                                                  DriverId = r.Cells[COL_SHIFT.MASTERID].Value.ToInt(),
                                                                  Driver_Shift_ID = r.Cells[COL_SHIFT.SHIFT_ID].Value.ToInt(),
                                                                  FromTime = r.Cells[COL_SHIFT.FROMTIME].Value.ToDateTime(),
                                                                  ToTime = r.Cells[COL_SHIFT.TOTIME].Value.ToDateTime(),

                                                              }).ToList();


                    Utils.General.SyncChildCollection(ref savedList3, ref listofDetail3, "Id", skipProperties);


                    objDriverMaster.Save();


                }

            }
            catch (Exception ex)
            {
                if (objDriverMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objDriverMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }


        }

        
       


        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }


        DriverBO objDriverMaster = new DriverBO();

        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                grdShifts.Rows.Clear();

                int DriverID = ddlDriver.SelectedValue.ToInt();

                objDriverMaster.GetByPrimaryKey(DriverID);


                //Fleet_Driver_Shift obj = General.GetObject<Fleet_Driver_Shift>(c => c.DriverId == DriverID);
                //if (obj != null)
                //{
                //    int ID = obj.Id.ToInt();

                //    objMaster.PrimaryKeyValue = ID;
                //}
                var data1 = objDriverMaster.Current.Fleet_Driver_Shifts.OrderBy(c => c.Id);
                var query = (from a in data1
                             select new
                             {
                                 Id = a.Id,
                                 Shifts = a.Driver_Shift.ShiftName,
                                 Shift_Id = a.Driver_Shift_ID,
                                 FromTime = a.FromTime.Value.TimeOfDay,
                                 ToTime = a.ToTime.Value.TimeOfDay
                             }).AsQueryable();
                if (query.Count() > 0)
                {
                    //grdLister.DataSource = query.ToList();
                    DataTable dt = query.ToDataTable();
                    //for (int i = 0; i < grdShifts.Rows.Count; i++)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridViewRowInfo row = null;

                        if (grdShifts.CurrentRow != null)
                        {
                            row = grdShifts.CurrentRow;
                        }

                        else
                        {
                            row = grdShifts.Rows.AddNew();

                        }
                        row.Cells[COL_SHIFT.SHIFT].Value = dt.Rows[i]["Shifts"].ToStr();
                        row.Cells[COL_SHIFT.SHIFT_ID].Value = dt.Rows[i]["Shift_Id"].ToStr();
                        row.Cells[COL_SHIFT.FROMTIME].Value = dt.Rows[i]["FromTime"].ToStr();
                        row.Cells[COL_SHIFT.TOTIME].Value = dt.Rows[i]["ToTime"].ToStr();
                        row.Cells[COL_SHIFT.ID].Value = dt.Rows[i]["Id"].ToInt();
                        row.Cells[COL_SHIFT.MASTERID].Value = DriverID;





                        ClearShift();
                        //grdShifts.Rows[i].Cells["FROMTIME"].Value = dt.Rows[i]["FromTime"].ToStr();
                        //grdShifts.Rows[i].Cells["TOTIME"].Value = dt.Rows[i]["ToTime"].ToStr();

                    }
                }
                else
                {

                }
                dtpFromTime.Value = DateTime.Now.ToDateorNull();
                dtpTOTime.Value = DateTime.Now.ToDateorNull();
            }
            catch (Exception ex)
            {


            }
        }

        private void btnClearAvail_Click(object sender, EventArgs e)
        {
            ClearShift();
        }

        private void dtpFromTime_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void dtpTOTime_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Shift ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    objMaster.GetByPrimaryKey(grdShifts.CurrentRow.Cells["Id"].Value.ToLong());
                    if (objMaster.Current != null)
                    {

                        objMaster.Delete(objMaster.Current);
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();


                    }
                    else
                    {
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();
                    }
                    
                    
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddShift();
            grdShifts.Columns["FROMTIME"].Width = 80;
            grdShifts.Columns["TOTIME"].Width = 80;
            grdShifts.Columns["SHIFT"].Width = 200;
            grdShifts.Columns["SHIFT_ID"].IsVisible = false;
        }

        private void ddlShifts_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            int? ShiftId = ddlShifts.SelectedValue.ToInt();
            if (ShiftId == 7)
            {
                dtpFromTime.Value = DateTime.Now.ToDateorNull();
                dtpTOTime.Value = DateTime.Now.ToDateorNull();
                dtpFromTime.Enabled = false;
                dtpTOTime.Enabled = false;
            }
            else
            {
                dtpFromTime.Value = DateTime.Now.ToDateorNull();
                dtpTOTime.Value = DateTime.Now.ToDateorNull();
                dtpFromTime.Enabled = true;
                dtpTOTime.Enabled = true;
            }
        }






    }
}
