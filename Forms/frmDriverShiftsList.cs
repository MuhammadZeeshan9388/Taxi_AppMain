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
    public partial class frmDriverShiftsList : UI.SetupBase
    {
        ShiftsBO objMaster = null;
        public frmDriverShiftsList()
        {
            InitializeComponent();
            objMaster = new ShiftsBO();
            this.SetProperties((INavigation)objMaster);
            grdLister.AllowAddNewRow = false;
            grdLister.ShowGroupPanel = false;
            grdLister.TableElement.RowHeight = 30;
            grdLister.MasterTemplate.ShowRowHeaderColumn = false;
            FormatGrid();

            this.Load += FrmAddShifts_Load;
            this.btnAddShift.Click += BtnAddShift_Click;
        }

        private void BtnAddShift_Click(object sender, EventArgs e)
        {
            frmDriverShifts frm = new frmDriverShifts();
            frm.ShowDialog();
			frm.Dispose();
			PopulateData();
        }

        public struct COLS
        {
            public static string Id = "Id";
            public static string Check = "Check";
			public static string Image = "Image";
			public static string ShiftName = "ShiftName";
            public static string FromTime = "FromTime";
            public static string TillTime = "TillTime";

            public static string ShiftStarts = "ShiftStarts";
            public static string ShiftEnds = "ShiftEnds";
        }
        private void FormatGrid()
        {

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Check;
            cbcol.HeaderText = "Active";
            cbcol.ReadOnly = false;
			cbcol.IsVisible = false;
			cbcol.Width = 80;
            grdLister.Columns.Add(cbcol);
			
			GridViewImageColumn CImage = new GridViewImageColumn();
			CImage.Name = COLS.Image;
			CImage.HeaderText = "Active";
			CImage.ReadOnly = true;
			CImage.Width = 80;			
			grdLister.Columns.Add(CImage);
		


			GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShiftName;
            col.HeaderText = "Shift Name";
            col.Width = 190;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShiftStarts;
            col.HeaderText = "Shift Starts";
            col.Width = 160;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShiftEnds;
            col.HeaderText = "Shift Ends";
            col.Width = 160;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            GridViewDateTimeColumn dcol = new GridViewDateTimeColumn();

            dcol.HeaderText = "From Time";
            dcol.IsVisible = false;
            dcol.Name = COLS.FromTime;
            dcol.CustomFormat = "HH:mm";
            dcol.FormatString = "{0:HH:mm}";
            dcol.Width = 120;
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDateTimeColumn();

            dcol.HeaderText = "Till Time";
            dcol.IsVisible = false;
            dcol.Name = COLS.TillTime;
            dcol.CustomFormat = "HH:mm";
            dcol.FormatString = "{0:HH:mm}";
            dcol.Width = 120;
            grdLister.Columns.Add(dcol);

            GridViewCommandColumn cmdcol = new GridViewCommandColumn();
           
            cmdcol.Width = 80;
            cmdcol.Name = "btnEdit";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Edit";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(cmdcol);

            cmdcol = new GridViewCommandColumn();
            cmdcol.Width = 80;
            cmdcol.Name = "btnDelete";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Delete";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(cmdcol);


            grdLister.CommandCellClick += GrdDriverShift_CommandCellClick;
        }


        private void FrmAddShifts_Load(object sender, EventArgs e)
        {

            PopulateData();


        }
        //public string ShowDayName(int FromDay)
        //{
        //    string Name = string.Empty;
        //    if (FromDay == 2)
        //    {
        //        Name = "Monday";
        //    }
        //    else if (FromDay == 3)
        //    {
        //        Name = "Tuesday";
        //    }
        //    else if (FromDay == 4)

        //    {
        //        Name = "Wednesday";
        //    }
        //    else if (FromDay == 5)
        //    {
        //        Name = "Thursday";
        //    }
        //    else if (FromDay == 6)
        //    {
        //        Name = "Friday";
        //    }
        //    else if (FromDay == 7)
        //    {
        //        Name = "Saturday";
        //    }
        //    else if (FromDay == 1)
        //    {
        //        Name = "Sunday";
        //    }
        //    return Name;
        //}
        private void GrdDriverShift_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;

            try
            {
                if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
                {
                    GridViewRowInfo row = grdLister.CurrentRow;
                    int Id = row.Cells["Id"].Value.ToInt();
                    objMaster.GetByPrimaryKey(Id);
                    frmDriverShifts frm = new frmDriverShifts();
                    frm.OnDisplayRecord(Id);
                    frm.ShowDialog();
                    PopulateData();
                    //OnDisplayRecord(Id);

                }
                else if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        GridViewRowInfo row = grdLister.CurrentRow;

                        int Id = row.Cells["Id"].Value.ToInt();
                        objMaster.GetByPrimaryKey(Id);

                        objMaster.Delete(objMaster.Current); ;
                        grdLister.CurrentRow.Delete();
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }

        }
        private void BtnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DateTime GetTillDateTime(TimeSpan TimeStart,TimeSpan TimeEnd)
        {
            DateTime dtNow = DateTime.Now;
            dtNow.Add(TimeStart);
            dtNow.Add(TimeEnd);
            return dtNow;
        }
        public override void PopulateData()
        {
            try
            {

                //using (TaxiDataContext db = new TaxiDataContext())
                //{
                //    var list = db.stp_GetShifts().ToList();

                //    grdLister.RowCount = list.Count;

                //    for (int i = 0; i < list.Count; i++)
                //    {

                //        grdLister.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                //        grdLister.Rows[i].Cells[COLS.ShiftName].Value = list[i].ShiftName;
                //        grdLister.Rows[i].Cells[COLS.FromTime].Value = list[i].FromTime;
                //        grdLister.Rows[i].Cells[COLS.TillTime].Value = list[i].TillTime;

                //        if (list[i].ShiftStart > 0)
                //        {
                //            grdLister.Rows[i].Cells[COLS.ShiftStarts].Value = list[i].StartDayName + " " + string.Format("{0:HH:mm}", list[i].FromTime);
                //            grdLister.Rows[i].Cells[COLS.ShiftEnds].Value = list[i].EndDayName + " " + string.Format("{0:HH:mm}", list[i].TillTime);
                //        }
                //        else
                //        {
                //            grdLister.Rows[i].Cells[COLS.ShiftStarts].Value = string.Format("{0:HH:mm}", list[i].FromTime);
                //            grdLister.Rows[i].Cells[COLS.ShiftEnds].Value = string.Format("{0:HH:mm}", list[i].TillTime);
                //        }
                //        if (list[i].IsActive.ToInt() == 1)
                //        {
                //            grdLister.Rows[i].Cells[COLS.Image].Value = Resources.Resource1.success;
                //        }
                //        else
                //        {
                //            grdLister.Rows[i].Cells[COLS.Image].Value = Resources.Resource1.blocked;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}
