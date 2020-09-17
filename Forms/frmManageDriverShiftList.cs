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
using Telerik.WinControls.Data;

namespace Taxi_AppMain
{   // frmManageDriverShiftList
    public partial class frmManageDriverShiftList : UI.SetupBase
    {
        DriverShiftsBO objDriverShifts;
        Font bigFont = new Font("Tahoma", 12, FontStyle.Bold);
        public frmManageDriverShiftList()
        {
            InitializeComponent();
            objDriverShifts = new DriverShiftsBO();
            this.SetProperties((INavigation)objDriverShifts);
            grdDriverShift.GroupDescriptors.Expression = "ShiftDate";
            grdDriverShift.GroupDescriptors[0].Format = "{1:dddd}";
            grdDriverShift.AutoExpandGroups = true;
            //GroupDescriptor descriptor1 = new GroupDescriptor();
            //// descriptor1.GroupNames.Add("Country", ListSortDirection.Ascending);
            //descriptor1.GroupNames.Add("", ListSortDirection.Ascending);
            //descriptor1.Expression = "ShiftDate";
            
            //descriptor1.Format = "{1:dddd}";
            ////descriptor1.GroupNames.Add("ContactTitle", ListSortDirection.Descending);
            //this.grdDriverShift.GroupDescriptors.Add(descriptor1);
            FormatDriverShiftGrid();
            this.grdDriverShift.AllowEditRow = false;
            this.Load += new EventHandler(frmShiftWiseDriverList_Load);
            this.grdDriverShift.ViewCellFormatting += new CellFormattingEventHandler(grdDriverShift_ViewCellFormatting);
            this.grdDriverShift.CellFormatting += new CellFormattingEventHandler(grdDriverShift_CellFormatting);
            this.grdDriverShift.CommandCellClick += new CommandCellClickEventHandler(grdDriverShift_CommandCellClick);
            this.grdDriverShift.KeyPress += new KeyPressEventHandler(grdDriverShift_KeyPress);
            this.grdDriverShift.MouseClick += new MouseEventHandler(grdDriverShift_MouseClick);
            this.grdDriverShift.GridViewElement.TableElement.VScrollBar.ValueChanged += new EventHandler(VScrollBar_ValueChanged);
        }



        void grdDriverShift_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {

                if (e.CellElement is GridDataCellElement)
                {
                    if (e.Column is GridViewCommandColumn)
                    {

                        if (e.Column.Name == "btnDelete")
                        {
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.delete;
                        }
                        else if (e.Column.Name == "btnUpdate")
                        {
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.edit2;
                        }
                    }
                }
            }
            catch { }
        }

        void VScrollBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    grdDriverShift.AllowEditRow = false;
                    grdDriverShift.Columns[COLS.FromTime].ReadOnly = true;
                    grdDriverShift.Columns[COLS.ToTime].ReadOnly = true;
                    if (this.grdDriverShift.IsInEditMode)
                    {
                        this.grdDriverShift.BeginEdit();
                    }
                }
                catch (NullReferenceException ex)
                {
                    //  MessageBox.Show("" + ex);
                }
                catch (Exception ex)
                {
                  //  MessageBox.Show("" + ex); ;
                }
            }
            catch (Exception ex)
            { }
        }

        void grdDriverShift_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (grdDriverShift.CurrentRow.Cells[COLS.IsDelete].IsCurrent == true)
                {
                    if (grdDriverShift.CurrentRow.Cells[COLS.IsDelete].Value.ToBool())
                        grdDriverShift.CurrentRow.Cells[COLS.IsDelete].Value = false;
                    else
                        grdDriverShift.CurrentRow.Cells[COLS.IsDelete].Value = true;
                }
                else if (grdDriverShift.CurrentRow.Cells[COLS.IsActive].IsCurrent == true)
                {
                    if (grdDriverShift.CurrentRow.Cells[COLS.IsActive].Value.ToBool())
                        grdDriverShift.CurrentRow.Cells[COLS.IsActive].Value = false;
                    else
                        grdDriverShift.CurrentRow.Cells[COLS.IsActive].Value = true;
                }
                else
                {
                    grdDriverShift.AllowEditRow = true;
                    grdDriverShift.Columns[COLS.FromTime].ReadOnly = false;
                    grdDriverShift.Columns[COLS.ToTime].ReadOnly = false;
                    if (this.grdDriverShift.BeginEditMode.ToBool())
                    {
                        this.grdDriverShift.BeginEdit();
                    }
                    grdDriverShift.AllowEditRow = false;
                    grdDriverShift.Columns[COLS.FromTime].ReadOnly = true;
                    grdDriverShift.Columns[COLS.ToTime].ReadOnly = true;
                }
            }
            catch (NullReferenceException ex)
            {
                // MessageBox.Show("" + ex);
            }
            catch (InvalidOperationException ex)
            {
                // MessageBox.Show("" + ex);
            }
            catch (Exception ex)
            {
                //   MessageBox.Show("" + ex);
            }
        }

        void grdDriverShift_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (grdDriverShift.CurrentRow.Cells[COLS.IsDelete].IsCurrent == true)
                 {
                     if (grdDriverShift.CurrentRow.Cells[COLS.IsDelete].Value.ToBool())
                        grdDriverShift.CurrentRow.Cells[COLS.IsDelete].Value = false;
                    else
                        grdDriverShift.CurrentRow.Cells[COLS.IsDelete].Value = true;
                }
                else if (grdDriverShift.CurrentRow.Cells[COLS.IsActive].IsCurrent == true)
                {
                    if (grdDriverShift.CurrentRow.Cells[COLS.IsActive].Value.ToBool())
                        grdDriverShift.CurrentRow.Cells[COLS.IsActive].Value = false;
                    else
                        grdDriverShift.CurrentRow.Cells[COLS.IsActive].Value = true;
                }
                else
                {
                    grdDriverShift.AllowEditRow = true;
                    //   grdDriverShift.Columns[COLS.IsDelete].ReadOnly = false;
                    grdDriverShift.Columns[COLS.FromTime].ReadOnly = false;
                    grdDriverShift.Columns[COLS.ToTime].ReadOnly = false;
                    // grdDriverShift.Columns[COLS.IsActive].ReadOnly = false;


                    if (this.grdDriverShift.BeginEditMode.ToBool())
                    {
                        this.grdDriverShift.BeginEdit();
                        //this.grdDriverShift.EndUpdate();
                        //this.grdDriverShift.EndEdit();
                        //this.grdDriverShift.Refresh();
                    }
                    grdDriverShift.AllowEditRow = false;
                    //grdDriverShift.Columns[COLS.IsDelete].ReadOnly = true;
                    grdDriverShift.Columns[COLS.FromTime].ReadOnly = true;
                    grdDriverShift.Columns[COLS.ToTime].ReadOnly = true;
                }
                //grdDriverShift.Columns[COLS.IsActive].ReadOnly = true;
            }

            catch (NullReferenceException ex)
            {
                // MessageBox.Show("" + ex);
            }
            catch (InvalidOperationException ex)
            {
                // MessageBox.Show("" + ex);
            }
            catch (Exception ex)
            {
                //   MessageBox.Show("" + ex);
            }
        }

        void grdDriverShift_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                RadGridView grid = gridCell.GridControl;
                if (gridCell.ColumnInfo.Name == "btnUpdate")
                {
                    //if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Cost Center ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    //{
                    int Id = this.grdDriverShift.CurrentRow.Cells[COLS.Id].Value.ToInt();

                    //string.Format("{0:HH:mm}", a.FromTime),
                    bool IsActiveDriver = grdDriverShift.CurrentRow.Cells[COLS.IsActive].Value.ToBool();
                    DateTime? dtFrom = grdDriverShift.CurrentRow.Cells[COLS.Date].Value.ToDate();

                    TimeSpan FromTime = grdDriverShift.CurrentRow.Cells[COLS.FromTime].Value.ToDateTime().TimeOfDay;
                    DateTime? FromDateTime = dtFrom + FromTime;

                    TimeSpan ToTime = grdDriverShift.CurrentRow.Cells[COLS.ToTime].Value.ToDateTime().TimeOfDay;
                    DateTime? ToDateTime = dtFrom + ToTime;
                    if (FromDateTime > ToDateTime)
                    {
                        ENUtils.ShowMessage("Start time can't greater then end time");
                        return;
                    }
                    objDriverShifts.GetByPrimaryKey(Id);
                    objDriverShifts.Edit();
                    objDriverShifts.Current.FromTime = FromDateTime;
                    objDriverShifts.Current.ToTime = ToDateTime;
                    objDriverShifts.Current.IsActive = IsActiveDriver;
                    objDriverShifts.Save();
                    objDriverShifts.Clear();
                    PopulateData();
                    // }
                }
                else //if (gridCell.ColumnInfo.Name == "btnDelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Driver Shift ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        int Id = this.grdDriverShift.CurrentRow.Cells[COLS.Id].Value.ToInt();
                        objDriverShifts.GetByPrimaryKey(Id);
                        objDriverShifts.Delete(objDriverShifts.Current);
                        grdDriverShift.CurrentRow.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
                RadButtonElement button = null;
        void grdDriverShift_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridGroupContentCellElement)
                {
                    e.CellElement.Font = bigFont;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.GhostWhite;
                    e.CellElement.RowElement.BackColor = Color.GhostWhite;
                    e.CellElement.RowElement.NumberOfColors = 1;
                    e.CellElement.ForeColor = Color.Blue;
                    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                }



              
            }

            catch (Exception ex)
            { }
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string DriverNo = "DriverNo";
            public static string DayNo = "DayNo";
            public static string FromTime = "FromTime";
            public static string ToTime = "ToTime";
            public static string IsActive = "IsActive";
            public static string ShiftDate = "ShiftDate";
            public static string Date = "Date";
            public static string IsDelete = "IsDelete";
            public static string Time = "Time";
        }

        public void FormatDriverShiftGrid()
        {
            try
            {
                this.grdDriverShift.ShowGroupPanel = false;
                this.grdDriverShift.AllowAddNewRow = false;
                GridViewCheckBoxColumn chcol1 = new GridViewCheckBoxColumn();
                chcol1.Name = COLS.IsDelete;
                chcol1.Width = 100;
                chcol1.ReadOnly = true;
                grdDriverShift.Columns.Add(chcol1);
                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.Name = COLS.Id;
                col.IsVisible = false;
                grdDriverShift.Columns.Add(col);
                //   GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
                col = new GridViewTextBoxColumn();
                col.Name = COLS.ShiftDate;
                col.HeaderText = "Shift Date";
                //  dtcol.FormatString="{1:dddd}";
                col.Width = 260;
                col.ReadOnly = true;
                grdDriverShift.Columns.Add(col);
                // GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
                //dtcol.Name = COLS.Date;
                //dtcol.IsVisible = false;
                //grdDriverShift.Columns.Add(dtcol);
                // dtcol = new GridViewDateTimeColumn();
                // dtcol.Width = 100;
                // dtcol.Name = COLS.Time;
                // dtcol.FormatString = "{0:HH:mm}";
                // dtcol.CustomFormat = "HH:mm";
                //// dtcol.Format = "HH:mm ";
                // grdDriverShift.Columns.Add(dtcol);
                col = new GridViewTextBoxColumn();
                col.Name = COLS.DriverNo;
                col.HeaderText = "Driver No";
                col.ReadOnly = true;
                col.Width = 320;
                grdDriverShift.Columns.Add(col);
                GridViewDateTimeColumn tcol = new GridViewDateTimeColumn();
                tcol.Name = COLS.Date;
                tcol.IsVisible = false;
                grdDriverShift.Columns.Add(tcol);
                tcol = new GridViewDateTimeColumn();
                tcol.Name = COLS.FromTime;
                tcol.HeaderText = "From Time";
                tcol.Width = 180;
                tcol.ReadOnly = true;
                tcol.FormatString = "{0:HH:mm}";
                tcol.CustomFormat = "HH:mm";
                grdDriverShift.Columns.Add(tcol);
                tcol = new GridViewDateTimeColumn();
                tcol.Name = COLS.ToTime;
                tcol.HeaderText = "To Time";
                tcol.Width = 180;
                tcol.ReadOnly = true;
                tcol.FormatString = "{0:HH:mm}";
                tcol.CustomFormat = "HH:mm";
                grdDriverShift.Columns.Add(tcol);

                //col = new GridViewTextBoxColumn();
                //col.Name = COLS.FromTime;
                //col.HeaderText = "From Time";
                //col.Width = 180;
                //col.ReadOnly = true;
                //grdDriverShift.Columns.Add(col);
                //col = new GridViewTextBoxColumn();
                //col.Name = COLS.ToTime;
                //col.HeaderText = "To Time";
                //col.Width = 180;
                //col.ReadOnly = true;
                //grdDriverShift.Columns.Add(col);
                GridViewCheckBoxColumn chcol = new GridViewCheckBoxColumn();
                chcol.Name = COLS.IsActive;
                chcol.HeaderText = "Is Active";
                chcol.Width = 180;
                chcol.ReadOnly = true;
                grdDriverShift.Columns.Add(chcol);
                //chcol = new GridViewCheckBoxColumn();
                //chcol.Name = COLS.IsDelete;
                //chcol.Width = 100;
                //chcol.ReadOnly = false;
                //grdDriverShift.Columns.Add(chcol);
            }
            catch (Exception ex)
            { }
        }
        void frmShiftWiseDriverList_Load(object sender, EventArgs e)
        {
            PopulateData();
        }

        public override void PopulateData()
        {
            try
            {
                grdDriverShift.Rows.Clear();
                var list = (from a in General.GetQueryable<Fleet_Driver_Shift>(c => c.DriverId != null)
                            orderby a.Id
                            select new
                            {
                                Id = a.Id,
                                DriverNo = a.Fleet_Driver.DriverNo,
                                ShiftDate = string.Format("{0:dddd}", a.FromTime),
                                Date = a.FromTime,
                                FromTime =a.FromTime, //string.Format("{0:HH:mm}", a.FromTime),
                                ToTime = a.ToTime, //string.Format("{0:HH:mm}", a.ToTime),
                                DayNo = a.DayNo,
                                IsActive = a.IsActive,

                            }).ToList();
                grdDriverShift.BeginUpdate();
                grdDriverShift.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdDriverShift.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    grdDriverShift.Rows[i].Cells[COLS.ShiftDate].Value = list[i].ShiftDate;
                    grdDriverShift.Rows[i].Cells[COLS.Date].Value = list[i].Date;
                    grdDriverShift.Rows[i].Cells[COLS.DriverNo].Value = list[i].DriverNo;
                    grdDriverShift.Rows[i].Cells[COLS.FromTime].Value = list[i].FromTime;
                    grdDriverShift.Rows[i].Cells[COLS.ToTime].Value = list[i].ToTime;
                    grdDriverShift.Rows[i].Cells[COLS.IsActive].Value = list[i].IsActive;
                    //if (list[i].DayNo == 1)
                    //{
                    //    grdDriverShift.Rows[i].Cells[COLS.DayNo].Value = "Tuesday";
                    //}

                }
                grdDriverShift.EndUpdate();
                grdDriverShift.AutoExpandGroups = true;
                //(grdDriverShift.Columns[COLS.ShiftDate] as GridViewTextBoxColumn).FormatString = "{0:dddd}";
                //(grdDriverShift.Columns["ToTime"] as GridViewTextBoxColumn).FormatString = "{0:HH:mm}";
                //(grdDriverShift.Columns["FromTime"] as GridViewDateTimeColumn).FormatString = "{0:HH:mm}";
                //(grdDriverShift.Columns["ToTime"] as GridViewDateTimeColumn).FormatString = "{0:HH:mm}";
                // grdDriverShift.GroupDescriptors.Expression = "ShiftDate";
                // grdDriverShift.GroupDescriptors[0].Format = "{1:dddd}";
                if (grdDriverShift.Columns.Contains("btnUpdate") == false)
                {
                    AddUpdateColumn(grdDriverShift);
                }
                //grdDriverShift.Columns[COLS.IsDelete].IsPinned = true;
                // grdDriverShift.DataSource = list;
            }
            catch (Exception ex)
            { }
        }
        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 120;
            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Update";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grid.Columns.Add(col);
            col = new GridViewCommandColumn();
            col.Width = 120;
            col.Name = "btnDelete";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Delete";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grid.Columns.Add(col);
        }

        private void btnAddDriverShift_Click(object sender, EventArgs e)
        {
            frmAddDayWiseDriverShift frm = new frmAddDayWiseDriverShift();
            frm.ShowDialog();
        }

        private void chkDelete_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                if (chkDelete.Checked == true)
                {
                    foreach (var item in grdDriverShift.Rows)
                    {
                        item.Cells[COLS.IsDelete].Value = true;
                    }
                }
                else
                {
                    foreach (var item in grdDriverShift.Rows)
                    {
                        item.Cells[COLS.IsDelete].Value = false;
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnDeleteShift_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in grdDriverShift.Rows.Where(c => c.Cells[COLS.IsDelete].Value.ToBool()))
                {
                    int Id = item.Cells[COLS.Id].Value.ToInt();
                    objDriverShifts.GetByPrimaryKey(Id);
                    objDriverShifts.Delete(objDriverShifts.Current);
                }
                PopulateData();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}
