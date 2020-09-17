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
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class rptfrmSinBin : UI.SetupBase
    {
        public struct COLS
        {
            public static string DRIVERID = "DRIVERID";
            public static string BOOKINGID = "BOOKINGID";

            public static string DRIVERNO = "DRIVERNO";
            public static string PICKUPPOINT = "PICKUPPOINT";
            public static string DESTINATION = "DESTINATION";

            public static string PICKUPDATETIME = "PICKUPDATETIME";

            public static string BLOCKEDON = "BLOCKEDON";
            public static string UNBLOCKEDON = "UNBLOCKEDON";

            public static string UNBLOCKBY = "UNBLOCKBY";

            public static string ACTION = "ACTION";
           
        }


        public rptfrmSinBin()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDriverReport_Load);

            grdLister.EnableHotTracking = false;
          
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

      
        void frmDriverReport_Load(object sender, EventArgs e)
        {


            ComboFunctions.FillUsersCombo(ddlController);

            ComboFunctions.FillDriverNoCombo(ddl_Driver, c => c.HasPDA!=null && c.HasPDA==true);
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.DRIVERID;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            //     col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Action";
            col.Name = COLS.ACTION;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "Drv";
            col.Name = COLS.DRIVERNO;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
            colDt.Name = COLS.PICKUPDATETIME;
            colDt.ReadOnly = true;
            colDt.HeaderText = "Pickup Date Time";
            grdLister.Columns.Add(colDt);

            col = new GridViewTextBoxColumn();          
            col.HeaderText = "Pickup Point";
            col.Name = COLS.PICKUPPOINT;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            //  col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = "Destination";
            col.HeaderText = COLS.DESTINATION;
            grdLister.Columns.Add(col);



            colDt = new GridViewDateTimeColumn();
            colDt.Name = COLS.BLOCKEDON;
            colDt.ReadOnly = true;
            colDt.HeaderText = "Blocked On";
            grdLister.Columns.Add(colDt);


            colDt = new GridViewDateTimeColumn();
            colDt.Name = COLS.UNBLOCKEDON;
            colDt.ReadOnly = true;
            colDt.HeaderText = "UnBlocked On";
            grdLister.Columns.Add(colDt);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.UNBLOCKBY;
            col.ReadOnly = true;
            col.HeaderText = "UnBlock By";
            grdLister.Columns.Add(col);





            //(grdLister.Columns[COLS.PICKUPDATETIME] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
           // (grdLister.Columns[COLS.PICKUPDATETIME] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";

            (grdLister.Columns[COLS.PICKUPDATETIME] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns[COLS.PICKUPDATETIME] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            (grdLister.Columns[COLS.BLOCKEDON] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns[COLS.BLOCKEDON] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";

            (grdLister.Columns[COLS.UNBLOCKEDON] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns[COLS.UNBLOCKEDON] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            
            grdLister.Columns[COLS.DRIVERNO].Width = 60;
            grdLister.Columns[COLS.PICKUPPOINT].Width = 160;
            grdLister.Columns[COLS.DESTINATION].Width = 160;
            grdLister.Columns[COLS.PICKUPDATETIME].Width = 100;
            grdLister.Columns[COLS.BLOCKEDON].Width = 130;
            grdLister.Columns[COLS.UNBLOCKEDON].Width = 130;
            grdLister.Columns[COLS.UNBLOCKBY].Width = 100;
            grdLister.Columns[COLS.ACTION].Width = 100;


           

           

            grdLister.AllowEditRow = true;

        }

      


        private void ViewReport()
        {
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();
                int userId = ddlController.SelectedValue.ToInt();
                string userName=ddlController.Text.Trim();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();
             


                string error = string.Empty;
              

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


               

                int reportType = eReportType.ALL;
                if (optReject.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    reportType = eReportType.REJECTED;
                }
                else if (optNotAcceped.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    reportType = eReportType.NOTACCEPTED;
                }
                else if (optRecover.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    reportType = eReportType.RECOVER;
                }
             
                    


          

            
                List<vu_SinBin> list = GetDataSource(driverId, reportType, fromDate, tillDate, userName);

                var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();          

                // grdLister.DataSource = list;
                grdLister.RowCount = list2.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    grdLister.Rows[i].Cells[COLS.DRIVERNO].Value = list2[i].DriverNo;
                    grdLister.Rows[i].Cells[COLS.PICKUPPOINT].Value = list2[i].FromAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.DESTINATION].Value = list2[i].ToAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.PICKUPDATETIME].Value = list[i].PickupDateTime;
                    grdLister.Rows[i].Cells[COLS.BLOCKEDON].Value = list2[i].RejectedDateTime;
                    grdLister.Rows[i].Cells[COLS.UNBLOCKEDON].Value = list2[i].UnBlockOn;
                    grdLister.Rows[i].Cells[COLS.UNBLOCKBY].Value = list2[i].UnBlockBy.ToStr();
                    grdLister.Rows[i].Cells[COLS.ACTION].Value = list2[i].StatusName.ToStr();

                   
                }

              
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                ViewReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {

            int driverId = ddl_Driver.SelectedValue.ToInt();
            int userId = ddlController.SelectedValue.ToInt();
            string userName = ddlController.Text.Trim();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();



            string error = string.Empty;


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




            int reportType = eReportType.ALL;
            if (optReject.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.REJECTED;
            }
            else if (optNotAcceped.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.NOTACCEPTED;
            }
            else if (optRecover.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.RECOVER;
            }

            

         



            rptfrmSinBinReport frm = new rptfrmSinBinReport();


            frm.DataSource =     GetDataSource(driverId, reportType, fromDate, tillDate, userName);

         

            frm.Criteria = "For the Period : " +Environment.NewLine+ string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
        //    frm.StatementType = statementType;
            frm.GenerateReport();

            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmSinBinReport1");

            if (doc != null)
            {
                doc.Close();
            }
            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);

        }


        private List<vu_SinBin> GetDataSource(int? driverId, int reportType, DateTime? fromDate, DateTime? tillDate, string userName)
        {
            return (from b in General.GetQueryable<vu_SinBin>(a =>
                         (a.Id == driverId || driverId == 0) && (a.UnBlockBy == userName || userName == string.Empty)
                         && ((reportType == eReportType.ALL)
                            || (reportType == eReportType.REJECTED && a.BookingStatusId == Enums.BOOKINGSTATUS.REJECTED)
                            || (reportType == eReportType.NOTACCEPTED && a.BookingStatusId == Enums.BOOKINGSTATUS.NOTACCEPTED)
                            || (reportType == eReportType.RECOVER && a.BookingStatusId == Enums.BOOKINGSTATUS.NOSHOW))

                            && (a.RejectedDateTime.Value.Date >= fromDate && a.RejectedDateTime.Value.Date <= tillDate)
                            )
                            orderby b.RejectedDateTime descending,b.DriverNo
                           select b ).ToList();

        }

        public struct eReportType
        {
            public static int ALL = 1;
            public static int REJECTED = 2;
            public static int NOTACCEPTED = 3;
            public static int RECOVER = 4;


        } ;

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            int driverId = ddl_Driver.SelectedValue.ToInt();
            int userId = ddlController.SelectedValue.ToInt();
            string userName = ddlController.Text.Trim();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();



            string error = string.Empty;


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




            int reportType = eReportType.ALL;
            if (optReject.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.REJECTED;
            }
            else if (optNotAcceped.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.NOTACCEPTED;
            }
            else if (optRecover.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.RECOVER;
            }







            rptfrmSinBinReport frm = new rptfrmSinBinReport();


            frm.DataSource = GetDataSource(driverId, reportType, fromDate, tillDate, userName);
            frm.Criteria = "For the Period : " + Environment.NewLine + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            //    frm.StatementType = statementType;
            frm.GenerateReport();
            frm.ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            int driverId = ddl_Driver.SelectedValue.ToInt();
            int userId = ddlController.SelectedValue.ToInt();
            string userName = ddlController.Text.Trim();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();



            string error = string.Empty;


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




            int reportType = eReportType.ALL;
            if (optReject.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.REJECTED;
            }
            else if (optNotAcceped.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.NOTACCEPTED;
            }
            else if (optRecover.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.RECOVER;
            }



             rptfrmSinBinReport frm = new rptfrmSinBinReport();


            frm.DataSource = GetDataSource(driverId, reportType, fromDate, tillDate, userName);
            frm.Criteria = "For the Period : " + Environment.NewLine + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            frm.GenerateReport();
            frm.SendEmail();

        }

      

        private void chkAll_ToggleStateChanged_1(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddl_Driver.SelectedValue = null;
                ddl_Driver.Enabled = false;
              

            }
            else
            {
                ddl_Driver.Enabled = true;
                
            }

        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
              
                ddlController.SelectedValue = null;
                ddlController.Enabled = false;
            }
            else
            {
                ddlController.Enabled = true;
               
            }
        }
    }
}
