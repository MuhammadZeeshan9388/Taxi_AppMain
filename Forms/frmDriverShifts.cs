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
    public partial class frmDriverShifts : UI.SetupBase
    {
        ShiftsBO objMaster;
        public frmDriverShifts()
        {
            InitializeComponent();
            objMaster = new ShiftsBO();
            this.SetProperties((INavigation)objMaster);
            FormatShiftGrid();
            Days();
		
			this.Load += new EventHandler(frmAddDayWiseDriverShift_Load);
            this.KeyDown += new KeyEventHandler(frmAddDayWiseDriverShift_KeyDown);
          //  this.btnSave.Click += BtnAdd_Click;
            this.btnNew.Click += BtnNew_Click;
            this.btnExit1.Click += BtnExit1_Click;
            this.chkAll.CheckedChanged += ChkAll_CheckedChanged;
            this.chkDayWise.CheckedChanged += ChkDayWise_CheckedChanged;
           // this.btnSaveShift.Click += BtnSaveShift_Click;
        }

        private void ChkDayWise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDayWise.Checked == true)
            {
                ddlStartDay.Enabled = true;
                ddlEndDay.Enabled = true;
            }
            else
            {
                ddlEndDay.NullText = "Select";
                ddlEndDay.Enabled = false;

                ddlStartDay.NullText = "Select";
                ddlStartDay.Enabled = false;
            }
        }

        private void BtnSaveShift_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void BtnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                if (grdDriverShift.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverShift.Rows.Count; i++)
                    {
                        grdDriverShift.Rows[i].Cells[COLS.Check].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (chkAll.Checked == false)
            {
                if (grdDriverShift.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverShift.Rows.Count; i++)
                    {
                        grdDriverShift.Rows[i].Cells[COLS.Check].Value = false;//..CurrentCell.Value;

                    }
                }
            }
        }

        private void Clear()
        {
            for (int i = 0; i < grdDriverShift.Rows.Count; i++)
            {
                grdDriverShift.Rows[i].Cells[COLS.Check].Value = false;//..CurrentCell.Value;

            }
			txtShiftName.Text = string.Empty;
			ddlEndDay.Items.Clear();
			ddlStartDay.Items.Clear();
			Days();
			chkAll.Checked = false;
	  	  //  dtpPenaltyLOgOnWindow.Value = "00:00".ToDateTime();
			dtpFromTime.Value = "00:00".ToDateTime();
			dtpTillTime.Value = "00:00".ToDateTime();
			dtpLogOnWindows.Value = "00:00".ToDateTime();
            				                    
            txtShiftName.Focus();
			objMaster.Clear();
		}

     
        public override void DisplayRecord()
        {
            try
            {
                if (objMaster.Current == null) return;
                txtShiftName.Text = objMaster.Current.ShiftName;

                ddlStartDay.Text = ShowDayName(objMaster.Current.ShiftStart.ToInt());
                ddlEndDay.Text = ShowDayName(objMaster.Current.ShiftEnd.ToInt());

				dtpFromTime.Value = objMaster.Current.FromTime;
				dtpTillTime.Value = objMaster.Current.TillTime;
				
                int PenaltyApplyAfter = objMaster.Current.PenaltyApplyAfter.ToInt(); ;
                int PenaltyAppliedAfterLogin = objMaster.Current.PenaltyAppliedAfterLogin.ToInt(); ;

                if (objMaster.Current.ShiftStart.ToInt() == 0 || objMaster.Current.ShiftEnd.ToInt() == 0)
                {
                    ddlEndDay.NullText = "Select";
                    ddlEndDay.Enabled = false;

                    ddlStartDay.NullText = "Select";
                    ddlStartDay.Enabled = false;
                    chkDayWise.Checked = false;
                }


				string fromTimeString = string.Format("{0:00}:{1:00}",  (PenaltyApplyAfter / 60) % 60, PenaltyApplyAfter % 60);//result.ToString("hh':'mm");

				dtpLogOnWindows.Value = fromTimeString.ToDateTime();

				//string fromTimeString1 = string.Format("{0:00}:{1:00}",  (PenaltyAppliedAfterLogin / 60) % 60, PenaltyAppliedAfterLogin % 60); // result1.ToString("hh':'mm");
				//dtpPenaltyLOgOnWindow.Value = fromTimeString1.ToDateTime();

			}
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void FormatShiftGrid()
        {
            grdDriverShift.AllowAddNewRow = false;
            grdDriverShift.AllowDeleteRow = false;
            grdDriverShift.ShowGroupPanel = false;
            grdDriverShift.TableElement.RowHeight = 30;
            grdDriverShift.EnableFiltering = true;
            grdDriverShift.MasterTemplate.ShowRowHeaderColumn = false;
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Check;
            cbcol.ReadOnly = false;
            cbcol.Width = 80;

            //cbcol.IsVisible = false;
            grdDriverShift.Columns.Add(cbcol);

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdDriverShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.DriverId;
            col.IsVisible = false;
            grdDriverShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Driver_Shift_ID;
            col.IsVisible = false;
            grdDriverShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Fleet_Driver_Shift_ID;
            col.IsVisible = false;
            grdDriverShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.DriverNo;
            col.HeaderText = "Driver No";
            col.Width = 170;
            col.ReadOnly = true;
            grdDriverShift.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Driver;
            col.HeaderText = "Driver Name";
            col.Width = 300;
            col.ReadOnly = true;
            grdDriverShift.Columns.Add(col);
            
        }
        public struct COLS
        {

            public static string Id = "Id";
            public static string Check = "Check";
            public static string DriverNo = "DriverNo";
            public static string Driver = "Driver";
            public static string DriverId = "DriverId";
            public static string Fleet_Driver_Shift_ID = "Fleet_Driver_Shift_ID";
            public static string Driver_Shift_ID = "Driver_Shift_ID";
            public static string ShiftName = "ShiftName";
            public static string FromTime = "FromTime";
            public static string TillTime = "TillTime";

            public static string FromDay = "FromDay";
            public static string TillDay = "TillDay";

            public static string DayValues = "DayValues";
        }


        
        private void GrdDriverShift_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            // EditShit(e.Row);
        }

        private void GrdDriverShift_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
            {
                GridViewRowInfo row = grdDriverShift.CurrentRow;

                //EditShit(row);
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {

                GridViewRowInfo row = grdDriverShift.CurrentRow;
                int Id = row.Cells[COLS.Id].Value.ToInt();
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var query = db.Fleet_Driver_Shifts.FirstOrDefault(c => c.Id == Id);
                    db.Fleet_Driver_Shifts.DeleteOnSubmit(query);
                    db.SubmitChanges();
                }
                RadGridView grid = gridCell.GridControl;
                grid.CurrentRow.Delete();

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

        void frmAddDayWiseDriverShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }



        void frmAddDayWiseDriverShift_Load(object sender, EventArgs e)
        {

            GetDriver();

            ddlStartDay.Focus();
        }
        public void GetDriver()
        {
            try
            {

                int Id = objMaster.Current == null ? 0 : objMaster.Current.Id;
                //if (Id > 0)
                //{
                //    btnSaveShift.Enabled = true;
                //}
                //else
                //{
                //    btnSaveShift.Enabled = false;
                //}
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list2 = db.stp_GetDriverShifts(Id).ToList();

                    var list = (list2.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                    list = list.OrderByDescending(c => c.Fleet_Driver_ShiftsId).ToList();
                    grdDriverShift.RowCount = list.Count;
                    for (int i = 0; i < list.Count; i++)
                    {
                        grdDriverShift.Rows[i].Cells[COLS.DriverId].Value = list[i].Id;
                        grdDriverShift.Rows[i].Cells[COLS.Check].Value = list[i].Fleet_Driver_ShiftsId!=null?true:false;
                        grdDriverShift.Rows[i].Cells[COLS.DriverNo].Value = list[i].DriverNo;
                        grdDriverShift.Rows[i].Cells[COLS.Driver].Value = list[i].DriverName;
                        grdDriverShift.Rows[i].Cells[COLS.Driver_Shift_ID].Value = list[i].Driver_Shift_ID;
                        grdDriverShift.Rows[i].Cells[COLS.Fleet_Driver_Shift_ID].Value = list[i].Fleet_Driver_ShiftsId;
                  
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void Days()
        {
            try
            {
				ddlStartDay.NullText = "Select";
				ddlStartDay.Items.Add("Monday");
                ddlStartDay.Items.Add("Tuesday");
                ddlStartDay.Items.Add("Wednesday");
                ddlStartDay.Items.Add("Thursday");
                ddlStartDay.Items.Add("Friday");
                ddlStartDay.Items.Add("Saturday");
                ddlStartDay.Items.Add("Sunday");


				ddlEndDay.NullText = "Select";
				ddlEndDay.Items.Add("Monday");
                ddlEndDay.Items.Add("Tuesday");
                ddlEndDay.Items.Add("Wednesday");
                ddlEndDay.Items.Add("Thursday");
                ddlEndDay.Items.Add("Friday");
                ddlEndDay.Items.Add("Saturday");
                ddlEndDay.Items.Add("Sunday");


            }
            catch (Exception ex)
            { }
        }



        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveDriverShift()
        {
            try
            {
                List<Fleet_Driver_Shift> objShiftList = new List<Fleet_Driver_Shift>();

                List<int> objFleet_Driver_ShiftId = new List<int>();

                for (int i = 0; i < grdDriverShift.Rows.Count; i++)
                {
                    int DriverId = grdDriverShift.Rows[i].Cells[COLS.DriverId].Value.ToInt();

                    int Driver_Shift_ID = grdDriverShift.Rows[i].Cells[COLS.Driver_Shift_ID].Value.ToInt();

                    int Fleet_Driver_Shift_ID = grdDriverShift.Rows[i].Cells[COLS.Fleet_Driver_Shift_ID].Value.ToInt();

                    if (Driver_Shift_ID == objMaster.Current.Id)
                    {
                        if (grdDriverShift.Rows[i].Cells[COLS.Check].Value.ToBool() == false)
                        {
                            objFleet_Driver_ShiftId.Add(Fleet_Driver_Shift_ID);
                        }
                    }
                    else
                    {
                        if (grdDriverShift.Rows[i].Cells[COLS.Check].Value.ToBool())
                        {
                            objShiftList.Add(new Fleet_Driver_Shift { DriverId = DriverId, Driver_Shift_ID = objMaster.Current.Id });
                        }
                    }
                }

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list = db.Fleet_Driver_Shifts.Where(x => objFleet_Driver_ShiftId.Contains(x.Id)).ToList();
                    if (list.Count > 0)
                    {
                        db.Fleet_Driver_Shifts.DeleteAllOnSubmit(list);
                    }
                    if (objShiftList.Count > 0)
                    {
                        db.Fleet_Driver_Shifts.InsertAllOnSubmit(objShiftList);
                    }
                    db.SubmitChanges();
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            Save();
        }
        public  void Save()
        {
            try
            {
                string FromDay = ddlStartDay.Text.Trim();
                string TillDay = ddlEndDay.Text.Trim();

                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }
                objMaster.Current.ShiftName = txtShiftName.Text.Trim();
                int FromDayId = 0;
                int TillDayId = 0;
				if (FromDay == "Monday")
				{
					FromDayId = 2;
				}
				else if (FromDay == "Tuesday")
				{
					FromDayId = 3;
				}
				else if (FromDay == "Wednesday")
				{
					FromDayId = 4;
				}
				else if (FromDay == "Thursday")
				{
					FromDayId = 5;
				}
				else if (FromDay == "Friday")
				{
					FromDayId = 6;
				}
				else if (FromDay == "Saturday")
				{
					FromDayId = 7;
				}
				else if (FromDay == "Sunday")
				{
					FromDayId = 1;
				}
			

				if (TillDay == "Monday")
				{
					TillDayId = 2;
				}
				else if (TillDay == "Tuesday")
				{
					TillDayId = 3;
				}
				else if (TillDay == "Wednesday")
				{
					TillDayId = 4;
				}
				else if (TillDay == "Thursday")
				{
					TillDayId = 5;
				}
				else if (TillDay == "Friday")
				{
					TillDayId = 6;
				}
				else if (TillDay == "Saturday")
				{
					TillDayId = 7;
				}
				else if (TillDay == "Sunday")
				{
					TillDayId = 1;
				}

                if (chkDayWise.Checked == false)
                {
                    FromDayId = 0;
                    TillDayId=0;
                }

                objMaster.Current.ShiftStart = FromDayId;
                objMaster.Current.ShiftEnd = TillDayId;
                ////DateTime StartTime = new DateTime(1900, 1, 1, numStartHour.Value.ToInt(), numStartMinute.Value.ToInt(), 0);

                ////DateTime EndTime = new DateTime(1900, 1, 1, numEndHour.Value.ToInt(), numEndMinute.Value.ToInt(), 0);

				//int LogOnWindowHr = numLogOnWindowHour.Value.ToInt();
				//int LogOnWindowMinute = numLogOnWindowMinute.Value.ToInt();

				int LogOnWindowHr = dtpLogOnWindows.Value.Value.TimeOfDay.Hours.ToInt();  //Date.Add(dtpLogOnWindows.Value.TimeOfDay); //dtpLogOnWindows.Value.ToStr();
				int LogOnWindowMinute = dtpLogOnWindows.Value.Value.TimeOfDay.Minutes.ToInt();


				//int PenaltyLogOnWindowHr = dtpPenaltyLOgOnWindow.Value.Value.TimeOfDay.Hours.ToInt();
				//int PenaltyLogOnWindowMinute = dtpLogOnWindows.Value.Value.TimeOfDay.Minutes.ToInt();

			//	int PenaltyApplyAfter = ((LogOnWindowHr*60)+LogOnWindowMinute);
              //  int PenaltyAppliedAfterLogin = ((PenaltyLogOnWindowHr*60)+PenaltyLogOnWindowMinute);
//
              //  objMaster.Current.PenaltyApplyAfter = PenaltyApplyAfter;
              //  objMaster.Current.PenaltyAppliedAfterLogin = PenaltyAppliedAfterLogin;


                objMaster.Current.FromTime = dtpFromTime.Value;
                objMaster.Current.TillTime = dtpTillTime.Value;
                objMaster.Save();
               
				SaveDriverShift();
				GetDriver();

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

    }
}
