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
using System.IO;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverJobsList : UI.SetupBase
    {
        public rptfrmDriverJobsList()
        {
            InitializeComponent();
            grdDriverJobsList.AllowEditRow = false;
            grdDriverJobsList.AllowRowReorder = false;
            this.btnExportXL.Click += new EventHandler(btnExportXL_Click);
            this.btnExit.Click += new EventHandler(btnExit_Click);
            grdDriverJobsList.ViewCellFormatting += new CellFormattingEventHandler(grdDriverJobsList_ViewCellFormatting);
            this.Load += new EventHandler(rptfrmDriverJobsList_Load);
        }

        Font f = new Font("Tahoma", 11, FontStyle.Bold);
        void grdDriverJobsList_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            e.CellElement.DrawFill = false;
            if (e.CellElement is GridDataCellElement)
            {

               

                e.CellElement.ToolTipText = e.CellElement.Text;

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;



                if (e.CellElement.RowIndex < 6)
                    e.CellElement.BorderWidth = 2;
                else if (e.CellElement.RowIndex > 7)
                    e.CellElement.BorderWidth = 1;


                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = f;


                e.CellElement.Alignment = ContentAlignment.MiddleCenter;
              //  e.CellElement.DrawFill = true;
             

             //   e.CellElement.DrawFill = false;

                if (e.CellElement.RowIndex >= 7)
                {
                     
                    int col = e.Column.Index;

                    if (e.Row.Cells[e.Column.Index].Tag != null)
                    {

                        if (e.Row.Cells[e.Column.Index].Tag.ToInt() == Enums.PAYMENT_TYPES.CASH && e.CellElement.Value.ToDecimal() > 25)
                        {

                            e.CellElement.NumberOfColors = 1;

                            e.CellElement.ForeColor = Color.Red;
                            e.CellElement.DrawFill = true;
                        }
                        else if (e.Row.Cells[e.Column.Index].Tag.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD || e.Row.Cells[e.Column.Index].Tag.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                        {

                            e.CellElement.NumberOfColors = 1;

                            e.CellElement.ForeColor = Color.Green;
                            e.CellElement.DrawFill = true;
                        }
                        else if (e.Row.Cells[e.Column.Index].Tag.ToInt() != Enums.PAYMENT_TYPES.CASH &&
                            e.Row.Cells[e.Column.Index].Tag.ToInt() != Enums.PAYMENT_TYPES.CREDIT_CARD &&
                            e.Row.Cells[e.Column.Index].Tag.ToInt() != Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                        {

                            e.CellElement.NumberOfColors = 1;

                            e.CellElement.ForeColor = Color.Blue;
                            e.CellElement.DrawFill = true;
                        }

                    }
                }


                //if (e.Column.Name == "Name" && (e.Row.Cells["BlackList"].Value = true).ToBool())
                //if (e.Column.Name == "Name")
                //{

                //    bool Chk = e.Row.Cells["BlackList"].Value.ToBool();
                //    if (Chk == true)
                //    {
                //        e.CellElement.NumberOfColors = 1;
                //        e.CellElement.BackColor = Color.Maroon;
                //        e.CellElement.ForeColor = Color.White;
                //        e.CellElement.DrawFill = true;
                //    }
                //}
                //if (e.Column.Name == "Phone")
                //{

                //    bool Chk = e.Row.Cells["BlackList"].Value.ToBool();
                //    if (Chk == true)
                //    {
                //        e.CellElement.NumberOfColors = 1;
                //        e.CellElement.BackColor = Color.Maroon;
                //        e.CellElement.ForeColor = Color.White;
                //        e.CellElement.DrawFill = true;
                //    }
                //}
                //if (e.Column.Name == "MobileNo")
                //{

                //    bool Chk = e.Row.Cells["BlackList"].Value.ToBool();
                //    if (Chk == true)
                //    {
                //        e.CellElement.NumberOfColors = 1;
                //        e.CellElement.BackColor = Color.Maroon;
                //        e.CellElement.ForeColor = Color.White;
                //        e.CellElement.DrawFill = true;
                //    }
                //}
                //if (e.Column.Name == "Address")
                //{

                //    bool Chk = e.Row.Cells["BlackList"].Value.ToBool();
                //    if (Chk == true)
                //    {
                //        e.CellElement.NumberOfColors = 1;
                //        e.CellElement.BackColor = Color.Maroon;
                //        e.CellElement.ForeColor = Color.White;
                //        e.CellElement.DrawFill = true;
                //    }
                //}




            }
        }

        void rptfrmDriverJobsList_Load(object sender, EventArgs e)
        {
            grdDriverJobsList.ShowColumnHeaders = false;
          //  grdDriverJobsList.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            DefaultDate();
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DefaultDate()
        {
            dtpFromDate.Value = DateTime.Now.ToDate();
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23,59, 59);

        }
        void btnExportXL_Click(object sender, EventArgs e)
        {
            if (grdDriverJobsList.Rows.Count == 0)
                return;

            try
            {


                saveFileDialog1.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

                saveFileDialog1.Title = "Save File";
                saveFileDialog1.FileName = "Driver Booking Stats";


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //grdLister.Columns["IsPaid"].IsVisible = false;
                    //grdLister.Columns["Paid"].IsVisible = true;
                    ////grdLister.Columns["btnUpdate"].IsVisible = false;

                    //grdLister.Columns[COLS.Active].Width = 50;
                    //grdLister.Columns[COLS.Rent].Width = 60;
                    //grdLister.Columns[COLS.DriverNo].Width = 50;
                    //grdLister.Columns[COLS.PreviousBalance].Width = 85;
                    //grdLister.Columns[COLS.Adjustment].Width = 60;
                    //grdLister.Columns[COLS.Collection].Width = 60;
                    //grdLister.Columns[COLS.AgentCommission].Width = 70;
                    //grdLister.Columns[COLS.OldAgentBalance].Width = 90;

                    //grdLister.Columns[COLS.Total].Width = 45;
                    //grdLister.Columns[COLS.Paid].Width = 35;


                    //var row = grdLister.Rows.OrderByDescending(c => c.Cells["ToDate"].Value.ToDate()).FirstOrDefault();

                    //DateTime? dtCurrent = row.Cells["FromDate"].Value.ToDate();
                    //DateTime dtEnd = row.Cells["ToDate"].Value.ToDate();



                    Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];
                    string heading = string.Empty;
                    heading = "" + string.Format("from {0:dd/MM/yyyy}", dtpFromDate.Value) + " till " + string.Format("{0:dd/MM/yyyy}", dtpTillDate.Value);

                    ClsExportGridView obj = new ClsExportGridView(grdDriverJobsList, saveFileDialog1.FileName);
                    obj.ApplyCellFormatting = true;
                    obj.ApplyCustomCellFormatting = true;
                    obj.Heading = heading;
                    obj.ExportExcelAsync(radProgressBar1);
                    //obj.ConditionalFormattingObject = new StyleDataRowConditionalFormattingObject();
                    //obj.ConditionalFormattingObject.ConditionFormattingColumnName = "Paid";
                    //obj.ConditionalFormattingObject.RowBackColor = Color.LightGreen;
                    //obj.ConditionalFormattingObject.RowForeColor = Color.Black;
                    //obj.ConditionalFormattingObject.TValue = "Paid";


               
                    //if (obj.ExportExcel())
                    //{
                    //    RadDesktopAlert alert = new RadDesktopAlert();
                    //    alert.CaptionText = "Export";
                    //    alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>Export Successfully</span></b></html>";
                    //    alert.Show();

                    //}

                    //grdLister.Columns["IsPaid"].IsVisible = true;
                    //grdLister.Columns["Paid"].IsVisible = false;
                 //   grdLister.Columns["IsPaid"].IsVisible = false;
                   // grdLister.Columns["Paid"].IsVisible = true;
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
            finally
            {

                SetDefaultColumnSettings();

            }
        }

        private const int DEFAULT_WIDTH = 80;

        private void SetDefaultColumnSettings()
        {
            foreach (var item in grdDriverJobsList.Columns)
            {
                item.Width = DEFAULT_WIDTH;
                
            }

        }

      
        private void btnView_Click(object sender, EventArgs e)
        {


            try
            {
                grdDriverJobsList.Columns.Clear();


                string Err = string.Empty;
                DateTime? dtFrom = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? dtTill = dtpTillDate.Value.ToDateTimeorNull();


                int sameDay = dtFrom.Value.Date == dtTill.Value.Date?1:0;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_DriverJobsList(dtFrom, dtTill, sameDay).ToList();


                    if (list.Count > 0)
                    {

                        //var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                        var drivernolist = list.Select(args => new { args.DriverId, args.DriverNo, args.DriverName, args.VehicleType }).Distinct().ToList();

                        var driverJobslist = list.Select(args => new { args.DriverId, args.DriverNo, args.DriverName, args.VehicleType, args.FareRate, args.Id,args.PaymentTypeId }).Distinct().ToList();




                        int DrvCount = list.Select(c => c.DriverNo).Distinct().Count();
                        int Column = (DrvCount + 1);
                        int rows = 8 + list.Count;
                        grdDriverJobsList.RowCount = rows;


                        grdDriverJobsList.ColumnCount = Column;
                        grdDriverJobsList.Columns[0].Name = "heading";

                        for (int x = 0; x < drivernolist.Count; x++)
                        {
                            grdDriverJobsList.Columns[x + 1].Name = drivernolist[x].DriverId.ToStr();

                        }

                        grdDriverJobsList.Columns[0].Width = 20;

                        for (int i = 0; i < 8; i++)
                        {
                            if (i == 0)
                            {
                                grdDriverJobsList.Rows[i].Cells[0].Value = "Name";

                                for (int j = 1; j <= drivernolist.Count; j++)
                                {

                                    grdDriverJobsList.Rows[i].Cells[j].Value = drivernolist[j - 1].DriverName;

                                }

                            }
                            else if (i == 1)
                            {
                                grdDriverJobsList.Rows[i].Cells[0].Value = "Number";

                                for (int j = 1; j <= drivernolist.Count; j++)
                                {

                                    grdDriverJobsList.Rows[i].Cells[j].Value = drivernolist[j - 1].DriverNo;

                                }

                            }
                            else if (i == 2)
                            {
                                grdDriverJobsList.Rows[i].Cells[0].Value = "Vehicle";

                                for (int j = 1; j <= drivernolist.Count; j++)
                                {

                                    grdDriverJobsList.Rows[i].Cells[j].Value = drivernolist[j - 1].VehicleType;

                                }

                            }
                            else if (i == 3)
                            {
                                grdDriverJobsList.Rows[i].Cells[0].Value = "Start";

                                string startTime = "???";
                                for (int j = 1; j <= drivernolist.Count; j++)
                                {
                                    startTime= list.Where(c => c.DriverId == drivernolist[j - 1].DriverId).FirstOrDefault().StartTime.ToStr();

                                    if (startTime.Length > 0 && startTime != "???")
                                        startTime = startTime.Substring(startTime.LastIndexOf(' ') + 1);
                                    else
                                        startTime = "???";

                                    grdDriverJobsList.Rows[i].Cells[j].Value = startTime;

                                }

                            }

                            else if (i == 4)
                            {
                                grdDriverJobsList.Rows[i].Cells[0].Value = "Finish";

                                //for (int j = 1; j <= drivernolist.Count; j++)
                                //{

                                //    grdDriverJobsList.Rows[i].Cells[j].Value = "???";

                                //}

                                string endTime = "???";
                                for (int j = 1; j <= drivernolist.Count; j++)
                                {
                                    endTime = list.Where(c => c.DriverId == drivernolist[j - 1].DriverId).FirstOrDefault().EndTime.ToStr();

                                    if (endTime.Length > 0 && endTime != "???")
                                        endTime = endTime.Substring(endTime.LastIndexOf(' ') + 1);
                                    else
                                        endTime = "???";



                                    grdDriverJobsList.Rows[i].Cells[j].Value = endTime;

                                }

                            }
                            else if (i == 5)
                            {
                                grdDriverJobsList.Rows[i].Cells[0].Value = "Total";

                                for (int j = 1; j <= drivernolist.Count; j++)
                                {

                                    grdDriverJobsList.Rows[i].Cells[j].Value = list.Where(c => c.DriverId == drivernolist[j - 1].DriverId).Sum(c => c.FareRate).ToDecimal();


                                }

                            }
                            else if (i >= 7)
                            {

                                for (int a = 0; a < drivernolist.Count; a++)
                                {

                                    int cnt = 1;
                                    foreach (var item in driverJobslist.Where(c => c.DriverId == drivernolist[a].DriverId))
                                    {
                                        grdDriverJobsList.Rows[6 + cnt].Cells[0].Value = cnt;

                                        grdDriverJobsList.Rows[6 + cnt].Cells[a + 1].Value = item.FareRate;
                                        grdDriverJobsList.Rows[6 + cnt].Cells[a + 1].Tag = item.PaymentTypeId;
                                        //      grdDriverJobsList.Rows[i].Cells[j].Value = list.Where(c => c.Id1 == drivernolist[j - 1].Id1).Sum(c => c.FareRate).ToDecimal();
                                        cnt++;

                                    }


                                }

                            }

                        }

                        SetDefaultColumnSettings();
                    }
                }




            }
            catch
            {

            }
        }

      
             

    }
}
