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
using Taxi_AppMain.Classes;
using Telerik.WinControls.UI;
using System.Collections;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmAssignShifts : UI.SetupBase
    {
        DriverBO objDriver;
        public frmAssignShifts()
        {
            InitializeComponent();
            objDriver = new DriverBO();
            this.SetProperties((INavigation)objDriver);
            FormatDriverGrid();
            FormatShiftGrid();

            this.Load += new EventHandler(frmAddDayWiseDriverShift_Load);
            this.KeyDown += new KeyEventHandler(frmAddDayWiseDriverShift_KeyDown);
            this.btnSave.Click += BtnSave_Click;
            this.btnExit1.Click += BtnExit1_Click;
        }

        private void BtnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public struct COLS
        {

            public static string Id = "Id";
            public static string Check = "Check";
            public static string DriverNo = "DriverNo";
            public static string Driver = "Driver";
            public static string DriverId = "DriverId";
            public static string Driver_Shift_ID = "Driver_Shift_ID";
            public static string ShiftName = "ShiftName";
            public static string FromTime = "FromTime";
            public static string TillTime = "TillTime";

            public static string FromDay = "FromDay";
            public static string TillDay = "TillDay";

            public static string DayValues = "DayValues";

            public static string ShiftStarts = "ShiftStarts";
            public static string ShiftEnds = "ShiftEnds";

        }
        private void FormatDriverGrid()
        {
            grdDriver.AllowAddNewRow = false;
            grdDriver.ShowGroupPanel = false;
            grdDriver.TableElement.RowHeight = 30;
            grdDriver.EnableFiltering = true;
            grdDriver.MasterTemplate.ShowRowHeaderColumn = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdDriver.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.DriverNo;
            col.HeaderText = "No";
            col.Width = 90;
            col.ReadOnly = true;
            grdDriver.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Driver;
            col.HeaderText = "Driver Name";
            col.Width = 140;
            col.ReadOnly = true;
            grdDriver.Columns.Add(col);
            grdDriver.CellClick += GrdDriver_CellClick;
        }

        private void GrdDriver_CellClick(object sender, GridViewCellEventArgs e)
        {
            Display();
        }

        private void GrdDriver_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            Display();
        }

        private void GrdDriver_CommandCellClick(object sender, EventArgs e)
        {

            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "colviewdetail")
            {
                Display();
            }
        }

        private void Display()
        {
            try
            {
                if (grdDriver.CurrentRow != null && grdDriver.CurrentRow is GridViewRowInfo)
                {
                    int Id = grdDriver.CurrentRow.Cells["Id"].Value.ToInt();

                     string Driver = grdDriver.CurrentRow.Cells[COLS.DriverNo].Value + "-" + grdDriver.CurrentRow.Cells[COLS.Driver].Value+" Shifts";

                    radLabel24.Text = Driver;
                    // grdDriverShift.Rows.Clear();
                    for (int i = 0; i < grdShift.RowCount; i++)
                    {
                        grdShift.Rows[i].Cells[COLS.Check].Value = false;
                        grdShift.Rows[i].Cells[COLS.Id].Value = null;//list.FirstOrDefault(c => c.Driver_Shift_ID.ToInt() == grdShift.Rows[i].Cells[COLS.Driver_Shift_ID].Value.ToInt()).Id;
                    }
                    var list = (from a in General.GetQueryable<Fleet_Driver_Shift>(c => c.DriverId == Id)
                                select new
                                {
                                    a.Id,
                                    a.DriverId,
                                    a.Driver_Shift_ID,
                                    //ShiftName = a.Driver_Shift_ID != null ? a.Driver_Shift.ShiftName : "",
                                    //FromTime = a.Driver_Shift.FromTime,
                                    //TillTime = a.Driver_Shift.TillTime,
                                    //ShiftStart = a.Driver_Shift.ShiftStart,
                                    //ShiftEnd = a.Driver_Shift.ShiftEnd
                                }).ToList();
                    int newIndex = 0;
                    for (int i = 0; i < grdShift.RowCount; i++)
                    {
                        if (list.Count(c => c.Driver_Shift_ID.ToInt() == grdShift.Rows[i].Cells[COLS.Driver_Shift_ID].Value.ToInt()) > 0)
                        {
                            grdShift.Rows[i].Cells[COLS.Check].Value = true;
                            grdShift.Rows[i].Cells[COLS.Id].Value = list.FirstOrDefault(c => c.Driver_Shift_ID.ToInt() == grdShift.Rows[i].Cells[COLS.Driver_Shift_ID].Value.ToInt()).Id;
                            int oldIndex = grdShift.Rows[i].Index;
                            grdShift.Rows.Move(oldIndex,newIndex);
                            newIndex++;
                        }
                    }


                    //grdDriverShift.Rows.Clear();
                    //grdDriverShift.RowCount = list.Count;
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    grdDriverShift.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    //    grdDriverShift.Rows[i].Cells[COLS.DriverId].Value = list[i].DriverId;
                    //    grdDriverShift.Rows[i].Cells[COLS.Driver_Shift_ID].Value = list[i].Driver_Shift_ID;
                    //    grdDriverShift.Rows[i].Cells[COLS.ShiftName].Value = list[i].ShiftName;
                    //    grdDriverShift.Rows[i].Cells[COLS.FromTime].Value = list[i].FromTime;


                    //    //StartShiftDay = ShowDayName(list[i].ShiftStart.ToInt());
                    //    //EndShiftDay = ShowDayName(list[i].ShiftEnd.ToInt());
                    //    //if ((ToDay == StartShiftDay || ToDay == EndShiftDay) && (StartTime >= Shiftlist[i].FromTime || StartTime <= Shiftlist[i].TillTime))
                    //    //{
                    //    //    grdShift.Rows[i].Cells[COLS.Check].Value = true;
                    //    //}
                    //    //else
                    //    //{
                    //    //    grdShift.Rows[i].Cells[COLS.Check].Value = false;
                    //    //}
                    //    grdDriverShift.Rows[i].Cells[COLS.ShiftStarts].Value = ShowDayName(list[i].ShiftStart.ToInt()) + " " + string.Format("{0:HH:mm}", list[i].FromTime);
                    //    grdDriverShift.Rows[i].Cells[COLS.ShiftEnds].Value = ShowDayName(list[i].ShiftEnd.ToInt()) + " " + string.Format("{0:HH:mm}", list[i].TillTime);

                    //}
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void FormatShiftGrid()
        {
            grdShift.EnableFiltering = true;
            grdShift.AllowAddNewRow = false;
            grdShift.ShowGroupPanel = false;
            grdShift.TableElement.RowHeight = 30;
            grdShift.MasterTemplate.ShowRowHeaderColumn = false;
            //grdDriverShift.EnableHotTracking = false;
            grdShift.AllowDeleteRow = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Driver_Shift_ID;
            col.IsVisible = false;
            grdShift.Columns.Add(col);

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Check;
            cbcol.Width = 80;
            cbcol.ReadOnly = false;
            grdShift.Columns.Add(cbcol);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.DayValues;
            col.IsVisible = false;
            grdShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShiftName;
            col.HeaderText = "Shift Name";
            col.Width = 140;
            col.ReadOnly = true;
            grdShift.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShiftStarts;
            col.HeaderText = "Shift Starts";
            col.Width = 160;
            col.ReadOnly = true;
            grdShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShiftEnds;
            col.HeaderText = "Shift Ends";
            col.Width = 160;
            col.ReadOnly = true;
            grdShift.Columns.Add(col);


            GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();

            dtcol.Name = COLS.FromTime;
            dtcol.HeaderText = "From Time";
            dtcol.Width = 100;
            dtcol.CustomFormat = "{0:HH:mm}";
            dtcol.FormatString = "{0:HH:mm}";
            dtcol.ReadOnly = true;
            dtcol.IsVisible = false;
            grdShift.Columns.Add(dtcol);
            dtcol = new GridViewDateTimeColumn();
            dtcol.Name = COLS.TillTime;
            dtcol.HeaderText = "Till Time";
            dtcol.Width = 100;
            dtcol.CustomFormat = "{0:HH:mm}";
            dtcol.FormatString = "{0:HH:mm}";
            dtcol.ReadOnly = true;
            dtcol.IsVisible = false;
            grdShift.Columns.Add(dtcol);

            grdShift.ViewCellFormatting += GrdShift_ViewCellFormatting;
            //GridViewCommandColumn cmdcol = new GridViewCommandColumn();

            //cmdcol.Width = 80;
            //cmdcol.Name = "btnAdd";
            //cmdcol.UseDefaultText = true;
            //cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            //cmdcol.DefaultText = "Add";
            //cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            //grdShift.Columns.Add(cmdcol);
            //grdShift.CommandCellClick += GrdShift_CommandCellClick;
            // grdDriverShift.CellDoubleClick += GrdDriverShift_CellDoubleClick;
        }

        private void GrdShift_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridDataCellElement)
            {
                if (e.Row.Cells[COLS.Check].Value.ToBool())
                {

                    //e.CellElement.NumberOfColors = 1;
                    //e.CellElement.DrawFill = true;

                    //e.CellElement.BackColor = Color.FromArgb(-5374161);
                    //e.CellElement.ForeColor = Color.Black;

                    e.CellElement.BackColor = Color.LightGreen;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                    e.CellElement.DrawFill = true;

                }
                else
                {


                    e.CellElement.BackColor = Color.Transparent;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                    e.CellElement.DrawFill = true;
                }
            }
        }

        private void GrdShift_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btnadd")
            {
                // if (grdDriver.CurrentRow == null)
                // {
                //     ENUtils.ShowMessage("Required : Driver");
                //     return;
                // }
                // int DriverId = grdDriver.CurrentRow.Cells[COLS.Id].Value.ToInt();

                // GridViewRowInfo shiftrow = grdShift.CurrentRow;
                // int ShiftId = shiftrow.Cells[COLS.Id].Value.ToInt();

                // if (grdDriverShift.Rows.Count(c => c.Cells[COLS.Driver_Shift_ID].Value.ToInt() == ShiftId) > 0)
                // {
                //     ENUtils.ShowMessage("Shift already exists");
                //     return;
                // }

                // //int idx = grdDriverShift.Rows.Where(a=>a.Cells[""].Value.ToInt()==1).FirstOrDefault().Index;

                //// grdDriver.Rows[idx].IsCurrent=true;


                // GridViewRowInfo row;
                // row = grdDriverShift.Rows.AddNew();

                // row.Cells[COLS.DriverId].Value = DriverId;
                // row.Cells[COLS.Driver_Shift_ID].Value = ShiftId;

                // row.Cells[COLS.ShiftName].Value = shiftrow.Cells[COLS.ShiftName].Value;
                // row.Cells[COLS.ShiftStarts].Value = shiftrow.Cells[COLS.ShiftStarts].Value.ToStr();
                // row.Cells[COLS.ShiftEnds].Value = shiftrow.Cells[COLS.ShiftEnds].Value.ToStr();

            }
        }

        private void GrdDriverShift_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            //EditShit(e.Row);
        }




        void frmAddDayWiseDriverShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }



        void frmAddDayWiseDriverShift_Load(object sender, EventArgs e)
        {
            LoadDriverAndShifts();
            grdDriver.Rows[0].IsCurrent = true;
            Display();
        }
        public void LoadDriverAndShifts()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list2 =(from a in db.Fleet_Drivers.Where(x=>x.IsActive==true)//(from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true)
                                     // var list2 = (from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true)
                                 select new
                             {
                                 Id = a.Id,
                                 DriverNo = a.DriverNo,//+ "-" + a.DriverName,
                                 DriverName = a.DriverName
                             }).ToList();

                var list = (list2.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                grdDriver.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdDriver.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    grdDriver.Rows[i].Cells[COLS.DriverNo].Value = list[i].DriverNo;
                    grdDriver.Rows[i].Cells[COLS.Driver].Value = list[i].DriverName;
                }


               
                    var Shiftlist = (from a in db.Driver_Shifts
                                     select new
                                     {
                                         a.Id,
                                         a.ShiftName,
                                         a.FromTime,
                                         a.TillTime,
                                         a.ShiftStart,
                                         a.ShiftEnd
                                     }).OrderBy(a => a.ShiftName).ToList();

                    grdShift.RowCount = Shiftlist.Count;

                    string ToDay = string.Format("{0:dddd}", DateTime.Now);
                    DateTime StartTime = new DateTime(1900, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    for (int i = 0; i < Shiftlist.Count; i++)
                    {
                        grdShift.Rows[i].Cells[COLS.Driver_Shift_ID].Value = Shiftlist[i].Id;
                        grdShift.Rows[i].Cells[COLS.ShiftName].Value = Shiftlist[i].ShiftName;
                        grdShift.Rows[i].Cells[COLS.FromTime].Value = Shiftlist[i].FromTime;
                        grdShift.Rows[i].Cells[COLS.TillTime].Value = Shiftlist[i].TillTime;

                        //StartShiftDay = ShowDayName(Shiftlist[i].ShiftStart.ToInt());
                        //EndShiftDay = ShowDayName(Shiftlist[i].ShiftEnd.ToInt());
                        //if ((ToDay == StartShiftDay || ToDay == EndShiftDay) && (StartTime >= Shiftlist[i].FromTime || StartTime <= Shiftlist[i].TillTime))
                        //{
                        //    grdShift.Rows[i].Cells[COLS.Check].Value = true;
                        //}
                        //else
                        //{
                        //    grdShift.Rows[i].Cells[COLS.Check].Value = false;
                        //}
                        grdShift.Rows[i].Cells[COLS.ShiftStarts].Value = ShowDayName(Shiftlist[i].ShiftStart.ToInt()) + " " + string.Format("{0:HH:mm}", Shiftlist[i].FromTime);
                        grdShift.Rows[i].Cells[COLS.ShiftEnds].Value = ShowDayName(Shiftlist[i].ShiftEnd.ToInt()) + " " + string.Format("{0:HH:mm}", Shiftlist[i].TillTime);
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public string ShowDayName(int FromDay)
        {
            string Name = string.Empty;
            if (FromDay == 2)
            {
                Name = "Monday";
            }
            else if (FromDay == 3)
            {
                Name = "Tuesday";
            }
            else if (FromDay == 4)
            {
                Name = "Wednesday";
            }
            else if (FromDay == 5)
            {
                Name = "Thursday";
            }
            else if (FromDay == 6)
            {
                Name = "Friday";
            }
            else if (FromDay == 7)
            {
                Name = "Saturday";
            }
            else if (FromDay == 1)
            {
                Name = "Sunday";
            }
            return Name;
        }


        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public override void Save()
        {
            try
            {

                if (grdDriver.CurrentRow == null)
                {
                    ENUtils.ShowMessage("Please select any Driver");
                    return;
                }
                int DriverId = grdDriver.CurrentRow.Cells[COLS.Id].Value.ToInt();

                objDriver.GetByPrimaryKey(DriverId);
                objDriver.Edit();

                string[] skipProperties = { "Fleet_Driver", "Driver_Shift", "Fleet_VehicleType", "Fleet_Driver_Note", "Gen_DebitCreditNote", "Fleet_DriverVehicleDetail", "Fleet_Master" };
                IList<Fleet_Driver_Shift> savedList3 = objDriver.Current.Fleet_Driver_Shifts;
                List<Fleet_Driver_Shift> listofDetail3 = (from r in grdShift.Rows.Where(c => c.Cells[COLS.Check].Value.ToBool() == true)

                                                          select new Fleet_Driver_Shift
                                                          {
                                                              Id = r.Cells[COLS.Id].Value.ToInt(),
                                                              DriverId = DriverId,
                                                              Driver_Shift_ID = r.Cells[COLS.Driver_Shift_ID].Value.ToIntorNull(),
                                                              //FromTime = r.Cells[COLS.FromTime].Value.ToDateTimeorNull(),
                                                              //ToTime = r.Cells[COLS.TillTime].Value.ToDateTimeorNull(),
                                                              //DayValues = r.Cells[COLS.DayValues].Value.ToStr()

                                                          }).ToList();

                Utils.General.SyncChildCollection(ref savedList3, ref listofDetail3, "Id", skipProperties);
                objDriver.Save();

            }
            catch (Exception ex)
            {
                if (objDriver.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objDriver.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }


            }
        }


    }
}
