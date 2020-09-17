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
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;


namespace Taxi_AppMain
{
    public partial class frmDriverJobLogReport : UI.SetupBase
    {

        string reportType = string.Empty;

        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string RefNumber = "RefNumber";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

        
            public static string Total = "Total";
            public static string TotalJobs = "Jobs Served";

        }


        public frmDriverJobLogReport()
        {
            InitializeComponent();
         

            grdLister.EnableHotTracking = false;
            //grdLister.AutoCellFormatting = true;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

     

            grdLister.ShowGroupPanel = false;

            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);


            grdLister.EnableAlternatingRowColor = true;
            grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;
           
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


          
                e.CellElement.ToolTipText = e.CellElement.Text;
                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;


            }




        }


       


        private void ViewReport()
        {
            try
            {
               

                int driverId = ddl_Driver.SelectedValue.ToInt();
                
                        


                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
                ;
                DateTime? tillDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpTillDate.Value.ToDate() + dtpTillTime.Value.Value.TimeOfDay).ToDateTime();



                string error = string.Empty;
                if (driverId == 0)
                {
                    error += "Required : Driver";
                }

                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }

                //lblCriteria.Text = "Account Report Related to '" + ddlCompany.Text.ToStr()
                //    + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);






                var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);


        
                var list = (from a in query
                            where a.DriverId == driverId &&

                                (a.PickupDateTime >= fromDate && a.PickupDateTime <= tillDate)
                                 
                                                               
                            orderby a.PickupDateTime descending
                            select new
                            {
                                Id = a.Id,
                                PickUpDate = a.PickupDateTime,
                                RefNumber = a.BookingNo,
                                Vehicle = a.Fleet_VehicleType.VehicleType,
                                PickupPoint = a.FromAddress,
                                Destination = a.ToAddress,
                               
                                Total = a.TotalCharges,
                              
                            }).ToList();

                // grdLister.DataSource = list;
                grdLister.RowCount = list.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                    grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickUpDate;
                    grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i].RefNumber.ToStr();
                    grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();                

                 

                    grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                    grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                 
                    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();

           

                }
             

                //lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();
                decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
                string total = totalEarning.ToStr();
                //lblTotalEarning.Text = "Total Earning £ " + total;

                lblTotalEarning.Text = " Total Jobs :" + grdLister.Rows.Count.ToStr() + "                                          Total Earning £ " + total+ "";

                
                 

            }
            catch (Exception ex)
            {


            }
        }


     


        private void btnViewReport_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        public override void Print()
        {
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();




                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
                ;
                DateTime? tillDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpTillDate.Value.ToDate() + dtpTillTime.Value.Value.TimeOfDay).ToDateTime();



                string error = string.Empty;
                if (driverId == 0)
                {
                    error += "Required : Driver";
                }

                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }






                


                rptfrmDriverJobLog frm = new rptfrmDriverJobLog();

                frm.DataSource = GetDataSource(driverId, fromDate, tillDate);



                frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", tillDate);
                //   frm.StatementType = statementType;
                frm.GenerateReport();

                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverJobLog");

                if (doc != null)
                {
                    doc.Close();
                }
                UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {


            }
        }


        private List<Vu_BookingBase> GetDataSource(int? driverId, DateTime? fromDate, DateTime? tillDate)
        {
            return General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId)
                .Where(b => (b.PickupDateTime.Value >= fromDate && b.PickupDateTime.Value <= tillDate))
                        .OrderByDescending(c => c.PickupDateTime).ToList();

        }

   

        private void btnExportPDF_Click(object sender, EventArgs e)
        {

        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            //    DateTime? fromDate = dtpFromDate.Value.ToDate();
            //    DateTime? tillDate = dtpTillDate.Value.ToDate();

            //    string error = string.Empty;

            //    if (driverId == null)
            //    {
            //        error += "Required : Driver";
            //    }

            //    if (fromDate == null)
            //    {
            //        if (string.IsNullOrEmpty(error))
            //            error += Environment.NewLine;

            //        error += "Required : From Date";
            //    }

            //    if (tillDate == null)
            //    {
            //        if (string.IsNullOrEmpty(error))
            //            error += Environment.NewLine;

            //        error += "Required : To Date";


            //    }

            //    if (!string.IsNullOrEmpty(error))
            //    {
            //        ENUtils.ShowMessage(error);
            //        return;

            //    }







            //    rptfrmCashAccStatement frm = new rptfrmCashAccStatement();

            //    frm.ReportType = this.reportType;
            //    frm.DataSource = GetDataSource(driverId, fromDate, tillDate);



            //    frm.DatePeriod = "For the Period"+Environment.NewLine + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            //    //   frm.StatementType = statementType;
            //    frm.GenerateReport();

            //    frm.SendEmail();

            //}
            //catch (Exception ex)
            //{


            //}
        }

        private void frmDriverJobLogReport_Load(object sender, EventArgs e)
        {
            try
            {

                dtpFromDate.Value = DateTime.Now.Date;
                dtpFromTime.Value = DateTime.Now.Date;

                dtpTillDate.Value = DateTime.Now.Date;
                dtpTillTime.Value = DateTime.Now.Date;


                ComboFunctions.FillDriverNoCombo(ddl_Driver);



                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = "Id";
                grdLister.Columns.Add(col);

                GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
                colDt.Name = "PickupDate";
                colDt.ReadOnly = true;
                colDt.HeaderText = "Pickup Date-Time";
                grdLister.Columns.Add(colDt);



                col = new GridViewTextBoxColumn();
                // col.IsVisible = false;
                col.ReadOnly = true;
                col.HeaderText = "Ref #";
                col.Name = "RefNumber";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                // col.IsVisible = false;
                col.HeaderText = "Vehicle";
                col.Name = "Vehicle";
                col.ReadOnly = true;
                grdLister.Columns.Add(col);


          

                col = new GridViewTextBoxColumn();
                // col.IsVisible = false;
                col.ReadOnly = true;
                col.HeaderText = "Pickup Point";
                col.Name = "PickupPoint";
                grdLister.Columns.Add(col);



                col = new GridViewTextBoxColumn();
                //     col.IsVisible = false;
                col.ReadOnly = true;
                col.HeaderText = "Destination";
                col.Name = "Destination";
                grdLister.Columns.Add(col);


                GridViewDecimalColumn colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.ReadOnly = true;
                colD.HeaderText = "Total";
                colD.Name = "Total";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);
                


                (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
                (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


                grdLister.Columns["PickUpDate"].Width = 130;
                grdLister.Columns["RefNumber"].Width = 50;
                grdLister.Columns["Vehicle"].Width = 70;
             
                grdLister.Columns["PickUpPoint"].Width = 200;
                grdLister.Columns["Destination"].Width = 200;

                grdLister.Columns["Charges"].Width = 45;
            
                grdLister.Columns["Total"].Width = 40;
          
                grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
                grdLister.Columns["RefNumber"].HeaderText = "Ref #";
                grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";       

          

                grdLister.AllowEditRow = true;
            }

            catch (Exception ex)
            {


            }
        }

        private void btnViewReport_Click_1(object sender, EventArgs e)
        {
            ViewReport();
        }

        private void dtpFromTime_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void dtpTillTime_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            Print();
        }

        private void btnExportPDF_Click_1(object sender, EventArgs e)
        {
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();




                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
                ;
                DateTime? tillDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpTillDate.Value.ToDate() + dtpTillTime.Value.Value.TimeOfDay).ToDateTime();



                string error = string.Empty;
                if (driverId == 0)
                {
                    error += "Required : Driver";
                }

                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }


                rptfrmDriverJobLog frm = new rptfrmDriverJobLog();


                frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", tillDate);
                frm.DataSource = GetDataSource(driverId, fromDate, tillDate);

                frm.GenerateReport();
                frm.ExportReport();
            }
            catch (Exception ex)
            {


            }
        }

        private void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                ViewDetailForm(e.Row);

            }

            catch
            {


            }
        }
        private void ViewDetailForm(GridViewRowInfo row)
        {

            if (row != null && row is GridViewDataRowInfo)
            {
                General.ShowBookingForm(row.Cells[COLS.ID].Value.ToInt(),true, "", "", Enums.BOOKING_TYPES.LOCAL);
             
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }
    }
}
