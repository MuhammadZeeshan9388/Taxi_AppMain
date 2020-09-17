using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Taxi_BLL;
using DAL;
using System.Data.SqlClient;
using Telerik.WinControls;
using System.Collections;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmShowSetFares : UI.SetupBase
    {

        public frmShowSetFares()
        {
            InitializeComponent();
            FormatFaresGrid();
            grdFares.AllowEditRow = true;
            grdFares.ShowColumnHeaders = true;
            grdFares.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
           
            this.Load += new EventHandler(frmAddFaresToWeb_Load);

        }      

        

        void frmAddFaresToWeb_Load(object sender, EventArgs e)
        {

           SetDisplayRecords();

        }


      //  delegate void UIDelegate(List<Fare_CustomJourney> list);   


        private void SetDisplayRecords()
        {

            try
            {
                var list = (from obj in General.GetQueryable<Fare_CustomJourney>(c => c.Id != null)
                            select new
                            {
                                Id = obj.Id,
                                DriverFares = obj.DriverFares,
                                DriverRtnFares = obj.DriverRtnFares,
                                CustomerFares = obj.CustomerFares,
                                CustomerRtnFares = obj.CustomerRtnFares,
                                CompanyId = obj.CompanyId,
                                CompanyFares = obj.CompanyFares,
                                CompanyRtnFares = obj.CompanyRtnFares,
                                Pickup = obj.Pickup,
                                Destination = obj.Destination,
                                ViaPoints = obj.ViaPoints,
                                EscortFares = obj.EscortFares,
                                EscortRtnFares = obj.EscortRtnFares,
                                DriverId = obj.DriverId,
                                EscortId = obj.EscortId,
                                CompanyName = obj.Gen_Company.CompanyName
                            }).ToList();

                int Count = list.Count;
                grdFares.Rows.Clear();
                grdFares.RowCount = Count;
                for (int i = 0; i < grdFares.RowCount; i++)
                {
                    grdFares.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    grdFares.Rows[i].Cells[COLS.RowNo].Value = 0;
                    grdFares.Rows[i].Cells[COLS.Pickup].Value = list[i].Pickup;
                    grdFares.Rows[i].Cells[COLS.Destination].Value = list[i].Destination;
                    grdFares.Rows[i].Cells[COLS.ViaPoints].Value = list[i].ViaPoints;

                    grdFares.Rows[i].Cells[COLS.DriverFares].Value = list[i].DriverFares;
                    grdFares.Rows[i].Cells[COLS.DriverRtnFares].Value = list[i].DriverRtnFares;
                    grdFares.Rows[i].Cells[COLS.CustomerFares].Value = list[i].CustomerFares;
                    grdFares.Rows[i].Cells[COLS.CustomerRtnFares].Value = list[i].CustomerRtnFares;

                    grdFares.Rows[i].Cells[COLS.CompanyFares].Value = list[i].CompanyFares;
                    grdFares.Rows[i].Cells[COLS.CompanyRtnFares].Value = list[i].CompanyRtnFares;

                    grdFares.Rows[i].Cells[COLS.CompanyName].Value = list[i].CompanyName;
                   

                }

            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
       
       
        public struct COLS
        {
            public static string Id = "Id";
            public static string Pickup = "Pickup";
            public static string Destination = "Destination";
            public static string ViaPoints = "ViaPoints";
            public static string DriverFares = "DriverFares";
            public static string DriverRtnFares = "DriverRtnFares";
            public static string RowNo = "RowNo";
            public static string CustomerFares = "CustomerFares";
            public static string CustomerRtnFares = "CustomerRtnFares";
            public static string CompanyFares = "CompanyFares";
            public static string CompanyRtnFares = "CompanyRtnFares";
            public static string CompanyId = "CompanyId";
            public static string CompanyName = "CompanyName";
            public static string CheckBox = "CheckBox";
        }



        private void FormatFaresGrid()
        {
            grdFares.AllowAddNewRow = false;
            grdFares.AllowEditRow = true;

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.HeaderText = "";
            cbcol.Name = COLS.CheckBox;
            //cbcol.AutoSizeMode = BestFitColumnMode.None;
            cbcol.Width = 40;
            cbcol.ReadOnly = false;

            grdFares.Columns.Add(cbcol);
  
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            col.ReadOnly = true;
            grdFares.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.RowNo;
            col.IsVisible = false;
            col.ReadOnly = true;
            grdFares.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup";
            col.Name = COLS.Pickup;
            col.Width = 240;
            col.IsVisible = true;
            col.ReadOnly = true;
            grdFares.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = COLS.Destination;
            col.Width = 240;
            col.IsVisible = true;
            col.ReadOnly = true;
            grdFares.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "ViaPoints";
            col.Name = COLS.ViaPoints;
            col.Width = 150;
            col.IsVisible = true;
            col.ReadOnly = true;
            grdFares.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "A/C";
            col.Name = COLS.CompanyName;
            col.Width = 100;
            col.IsVisible = true;
            col.ReadOnly = true;
            grdFares.Columns.Add(col);


            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Drv Fares";
            colDec.Name = COLS.DriverFares;
            colDec.Width = 70;
            col.IsVisible = true;
            col.ReadOnly = false;
            colDec.ThousandsSeparator = true;
            grdFares.Columns.Add(colDec);


            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Drv R/Fares";
            colDec.Name = COLS.DriverRtnFares;
            colDec.Width = 85;
            col.IsVisible = true;
            col.ReadOnly = false;
            colDec.ThousandsSeparator = true;
            grdFares.Columns.Add(colDec);



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Cust Fares";
            colDec.Name = COLS.CustomerFares;
            colDec.Width = 80;
            colDec.DecimalPlaces = 2;
            col.ReadOnly = false;
            colDec.ThousandsSeparator = true;
           
            grdFares.Columns.Add(colDec);
            //grdFares.MasterTemplate.ShowRowHeaderColumn = false;

            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Cust R/Fares";
            colDec.Name = COLS.CustomerRtnFares;
            colDec.Width = 95;
            colDec.IsVisible = true;
            col.ReadOnly = false;
            colDec.ThousandsSeparator = true;
            grdFares.Columns.Add(colDec);



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "A/C Fares";
            colDec.Name = COLS.CompanyFares;
            colDec.Width = 80;
            colDec.IsVisible = true;
            col.ReadOnly = false;
            colDec.ThousandsSeparator = true;
            grdFares.Columns.Add(colDec);


            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "A/C RtnFares";
            colDec.Name = COLS.CompanyRtnFares;
            colDec.Width = 95;
            colDec.IsVisible = true;
            col.ReadOnly = false;
            colDec.ThousandsSeparator = true;
            grdFares.Columns.Add(colDec);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "CompanyId";
            col.Name = COLS.CompanyId;
            col.Width = 70;
            col.IsVisible = false;
            col.ReadOnly = true;
            grdFares.Columns.Add(col);
            
         

            GridViewCommandColumn colbtn = new GridViewCommandColumn();
            colbtn.Width = 70;

            colbtn.UseDefaultText = true;
            colbtn.DefaultText = "Update";
            colbtn.Name = "Update";
            colbtn.IsVisible = true;
            grdFares.Columns.Add(colbtn);


            colbtn = new GridViewCommandColumn();
            colbtn.Width = 60;

            colbtn.UseDefaultText = true;
            colbtn.DefaultText = "Delete";
            colbtn.Name = "Delete";
            colbtn.IsVisible = true;
            grdFares.Columns.Add(colbtn);



            UI.GridFunctions.SetFilter(grdFares);

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        SetCustomFaresBO objeCustomBO = null;
        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            try

            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();

                GridViewRowInfo row = gridCell.RowElement.RowInfo;
                long id = row.Cells[COLS.Id].Value.ToLong();


                if (name == "update")
                {


                    RadGridView grid = gridCell.GridControl;

                    if (grid.CurrentRow != null && grid.CurrentRow is GridViewDataRowInfo)
                    {

                        objeCustomBO = new SetCustomFaresBO();

                        objeCustomBO.GetByPrimaryKey(id);

                        if (objeCustomBO.Current != null)
                        {

                            objeCustomBO.Current.Pickup = grid.CurrentRow.Cells[COLS.Pickup].Value.ToString();

                            objeCustomBO.Current.Destination = grid.CurrentRow.Cells[COLS.Destination].Value.ToString();

                            objeCustomBO.Current.ViaPoints = grid.CurrentRow.Cells[COLS.ViaPoints].Value.ToStr();
                            objeCustomBO.Current.DriverFares = grid.CurrentRow.Cells[COLS.DriverFares].Value.ToDecimal();


                            objeCustomBO.Current.DriverRtnFares = grid.CurrentRow.Cells[COLS.DriverRtnFares].Value.ToDecimal();
                            objeCustomBO.Current.CustomerFares = grid.CurrentRow.Cells[COLS.CustomerFares].Value.ToDecimal();


                            objeCustomBO.Current.CustomerRtnFares = grid.CurrentRow.Cells[COLS.CustomerRtnFares].Value.ToDecimal();
                            objeCustomBO.Current.CompanyFares = grid.CurrentRow.Cells[COLS.CompanyFares].Value.ToDecimal();


                            objeCustomBO.Current.CompanyRtnFares = grid.CurrentRow.Cells[COLS.CompanyRtnFares].Value.ToDecimal();
                            if (grid.CurrentRow.Cells[COLS.CompanyId].Value != null)
                            {
                                objeCustomBO.Current.CompanyId = grid.CurrentRow.Cells[COLS.CompanyId].Value.ToInt();
                            }

                            objeCustomBO.CheckDataValidation = false;



                            objeCustomBO.Save();
                            //   SetDisplayRecords();
                        }
                    }
                }


                else if (name == "delete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Selected Booking(s) ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        objeCustomBO = new SetCustomFaresBO();
                        objeCustomBO.GetByPrimaryKey(id);
                        objeCustomBO.Delete(objeCustomBO.Current);
                        grdFares.Refresh();
                        row.Delete();
                        //  SetDisplayRecords();
                    }
                }
            }
            catch

            {


            }

        }

        public override void RefreshData()
        {

            SetDisplayRecords();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    StringBuilder st = new StringBuilder();
            //    if (grdFares.Rows.Where(c => c.Cells[COLS.CheckBox].Value.ToBool()).Count() == 0) return;
            //    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Selected Booking(s) ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            //    {

            //        foreach (GridViewRowInfo row in grdFares.Rows)
            //        {
            //            if (row.Cells[COLS.CheckBox].Value.ToBool())
            //            {
            //             st.Append("delete from Fare_CustomJourneys where Id =" + row.Cells[COLS.Id].Value.ToInt() + " ");
            //            }
            //        }
            //        using (TaxiDataContext db = new TaxiDataContext())
            //        {
            //            db.stp_RunProcedure(st.ToStr());
            //        }                 


            //        SetDisplayRecords();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (objeCustomBO.Errors.Count > 0)
            //        ENUtils.ShowMessage(objeCustomBO.ShowErrors());
            //    else
            //    {
            //        ENUtils.ShowMessage(ex.Message);

            //    }


            //}
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                StringBuilder st = new StringBuilder();
                if (grdFares.Rows.Where(c => c.Cells[COLS.CheckBox].Value.ToBool()).Count() == 0) return;
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Selected Booking(s) ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    foreach (GridViewRowInfo row in grdFares.Rows.Where(c => c.Cells[COLS.CheckBox].Value.ToBool()))
                    {                      
                            st.Append("delete from Fare_CustomJourneys where Id =" + row.Cells[COLS.Id].Value.ToInt() + " ");
                       
                    }
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_RunProcedure(st.ToStr());
                    }


                    SetDisplayRecords();
                }
            }
            catch (Exception ex)
            {
                if (objeCustomBO.Errors.Count > 0)
                    ENUtils.ShowMessage(objeCustomBO.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }


            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < grdFares.RowCount; i++)
                    {

                        grdFares.Rows[i].Cells[COLS.CheckBox].Value = 1;

                    }

                }
                else
                {
                    for (int i = 0; i < grdFares.RowCount; i++)
                    {

                        grdFares.Rows[i].Cells[COLS.CheckBox].Value = 0;

                    }

                }
            }
            catch (Exception ex)
            { }
        }

        private void grdFares_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Row is GridViewDataRowInfo)
            {
                e.CellElement.ToolTipText =  e.CellElement.Text;
            }
        }


    }
}
