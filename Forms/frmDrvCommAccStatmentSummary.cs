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
    public partial class frmDrvCommAccStatmentSummary  : UI.SetupBase
    {
        public struct COLS
        {
            public static string DrvNo = "DrvNo";

            public static string AccountCode = "AccountCode";
            public static string AccountCode2 = "AccountCode2";
            public static string AccountCode3 = "AccountCode3";
            public static string AccountCode4 = "AccountCode4";
            public static string AccountCode5 = "AccountCode5";
            public static string AccountCode6 = "AccountCode6";
            public static string AccountCode7 = "AccountCode7";
            public static string AccountCode8 = "AccountCode8";
            public static string AccountCode9 = "AccountCode9";
            public static string AccountCode10 = "AccountCode10";

            public static string Account = "Account";

            public static string TotalJobs = "TotalJobs";
            public static string Amount = "Amount";
            public static string Balance = "Balance";

        }


        public frmDrvCommAccStatmentSummary()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDriverReport_Load);

            grdLister.EnableHotTracking = false;
            //grdLister.AutoCellFormatting = true;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.None;
            grdLister.ShowRowHeaderColumn = false;

     //       grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);

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

                if (e.Column.Name == COLS.Amount || e.Column.Name==COLS.Balance)
                {
                    e.CellElement.ForeColor = Color.DarkRed;
                    e.CellElement.Font = newFont;
                }

            }




        }


        void frmDriverReport_Load(object sender, EventArgs e)
        {
          //  ViewReport();

            ComboFunctions.FillDriverNoCombo(ddl_Driver, c => c.DriverTypeId == 2);
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
             col.Width = 60;
            col.ReadOnly = true;
            col.HeaderText = "Drv";
            col.Name = COLS.DrvNo;
            grdLister.Columns.Add(col);

         

            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 1";
            col.Name = COLS.AccountCode;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 2";
            col.Name = COLS.AccountCode2;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 3";
            col.Name = COLS.AccountCode3;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 4";
            col.Name = COLS.AccountCode4;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 5";
            col.Name = COLS.AccountCode5;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 6";
            col.Name = COLS.AccountCode6;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 7";
            col.Name = COLS.AccountCode7;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 8";
            col.Name = COLS.AccountCode8;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 9";
            col.Name = COLS.AccountCode9;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = true;
            col.HeaderText = "Acc 10";
            col.Name = COLS.AccountCode10;
            grdLister.Columns.Add(col);


         


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
          //  col.Width = 200;
            col.ReadOnly = true;
            col.Name = COLS.Account;
            col.HeaderText = "Account";
            grdLister.Columns.Add(col);







            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.IsVisible = false;
            colD.Width = 110;
            colD.DecimalPlaces = 0;
            colD.Minimum = 0;
            colD.HeaderText = "Total Jobs";
            colD.Name = COLS.TotalJobs;
            colD.Maximum = 9999999;
           
            grdLister.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.Width = 90;
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Amount";
            colD.Name = COLS.Amount;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.Width = 90;
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Balance";
            colD.Name = COLS.Balance;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);
                     
        }

        private List<ClsAccStatment> GetDataSource(int? driverId, DateTime? fromDate, DateTime? tillDate)
        {
           // decimal rent = AppVars.objPolicyConfiguration.DriverMonthlyRent.ToDecimal();

            decimal commission = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();

            var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);

            var list= query.Where(a => a.DriverId != null && a.Fleet_Driver.DriverTypeId ==2 &&
                           (driverId == null || a.DriverId == driverId) && a.CompanyId != null
                           && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate))
                           .GroupBy(args => new
                           {
                               args.Gen_Company.CompanyCode,
                           //    args.Gen_Company.CompanyName,
                               args.Fleet_Driver.DriverNo,
                               args.DriverId,
                               args.Fleet_Driver.DriverMonthlyRent
                           })
                           .Select(args => new 
                           {
                               DriverId=args.Key.DriverId,
                               DriverNo=args.Key.DriverNo,
                               CompanyCode = args.Key.CompanyCode,
                            //   CompanyName = args.Key.CompanyName,
                             //  TotalJobs = args.Count(),
                               TotalAmount = args.Sum(c => c.TotalCharges),
                               Commission= args.Sum(c=>(c.DriverCommissionType=="Commission"? (c.TotalCharges*commission)/100: c.DriverCommission  ))
                              // Balance = rent - args.Sum(c => c.TotalCharges).ToDecimal()
                           }).OrderBy(c=>c.DriverNo).ToList();


            List<ClsAccStatment> resultList = new List<ClsAccStatment>();

            ClsAccStatment obj = null;
            int cnt = 0;
            int? oldDriverId = null;
            
          
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].DriverId != oldDriverId)
                {
                   

                    obj = new ClsAccStatment();
                    obj.CompanyCode = list[i].CompanyCode;
                    cnt = 1;

                    resultList.Add(obj);
                    
                }
                else
                {
                    obj = resultList.FirstOrDefault(c => c.DriverId == oldDriverId);
                    cnt++;
                    if (cnt <= 10)
                    {
                        obj.GetType().GetProperty("CompanyCode" + cnt).SetValue(obj, list[i].CompanyCode, null);
                    }                  
                   
                }
                oldDriverId = list[i].DriverId;
                obj.DriverId = list[i].DriverId;
                obj.DriverNo = list[i].DriverNo;
                obj.DriverRent = list[i].Commission.ToDecimal();
            
                obj.TotalAmount = obj.TotalAmount.ToDecimal() + list[i].TotalAmount.ToDecimal();


                if ((list.Count-1 > i + 1 && list[i+1].DriverId!=list[i].DriverId) || list.Count==i+1)
                {
                  //  obj.Balance = obj.TotalAmount - rent;
                    obj.Balance = obj.TotalAmount - obj.DriverRent.ToDecimal();
                }


            }


            return resultList;
                     


        }

     


        private void ViewReport()
        {
            try
            {
                int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
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

                //    lblCriteria.Text = "Driver Report Related to '" + ddl_Driver.Text.ToStr() + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);


                var list = GetDataSource(driverId, fromDate, tillDate);





                grdLister.RowCount = list.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    grdLister.Rows[i].Cells[COLS.DrvNo].Value = list[i].DriverNo.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode].Value = list[i].CompanyCode.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode2].Value = list[i].CompanyCode2.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode3].Value = list[i].CompanyCode3.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode4].Value = list[i].CompanyCode4.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode5].Value = list[i].CompanyCode5.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode6].Value = list[i].CompanyCode6.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode7].Value = list[i].CompanyCode7.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode8].Value = list[i].CompanyCode8.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode9].Value = list[i].CompanyCode9.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountCode10].Value = list[i].CompanyCode10.ToStr();

                    grdLister.Rows[i].Cells[COLS.Account].Value = list[i].CompanyName.ToStr();
                  
                    //grdLister.Rows[i].Cells[COLS.TotalJobs].Value = list[i].TotalJobs.ToInt();
                    grdLister.Rows[i].Cells[COLS.Amount].Value = list[i].TotalAmount.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Balance].Value = list[i].Balance.ToDecimal();

                }

                grdLister.Columns[COLS.DrvNo].Width = 60;
                grdLister.Columns[COLS.AccountCode].Width = 100;
                grdLister.Columns[COLS.AccountCode2].Width = 100;
                grdLister.Columns[COLS.AccountCode3].Width = 100;
                grdLister.Columns[COLS.AccountCode4].Width = 100;
                grdLister.Columns[COLS.AccountCode5].Width = 100;
                grdLister.Columns[COLS.AccountCode6].Width = 100;
                grdLister.Columns[COLS.AccountCode7].Width = 100;
                grdLister.Columns[COLS.AccountCode8].Width = 100;
                grdLister.Columns[COLS.AccountCode9].Width = 100;
                grdLister.Columns[COLS.AccountCode10].Width = 100;
            //    grdLister.Columns[COLS.Account].Width = 200;
              //  grdLister.Columns[COLS.TotalJobs].Width = 130;
                grdLister.Columns[COLS.Amount].Width = 90;
                grdLister.Columns[COLS.Balance].Width = 90;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
                  

        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            ViewReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {
            try
            {
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
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







            rptfrmDrvRentAccStatmentSummary frm = new rptfrmDrvRentAccStatmentSummary();



            frm.DataSource = GetDataSource(driverId,fromDate,tillDate);



            frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            frm.GenerateReport();

            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDrvRentAccStatmentSummary1");

            if (doc != null)
            {
                doc.Close();
            }
            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

      
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
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




            rptfrmDrvRentAccStatmentSummary frm = new rptfrmDrvRentAccStatmentSummary();



            frm.DataSource = GetDataSource(driverId, fromDate, tillDate);

            frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
          
            frm.GenerateReport();
            frm.ExportReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
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




            rptfrmDrvRentAccStatmentSummary frm = new rptfrmDrvRentAccStatmentSummary();

            frm.DataSource = GetDataSource(driverId, fromDate, tillDate);

            frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
         
            frm.GenerateReport();
            frm.SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
    }
}
