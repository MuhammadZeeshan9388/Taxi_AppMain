using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using DAL;
using Taxi_Model;
using Telerik.WinControls.UI;
using Utils;
using System.Threading;
using System.Diagnostics;
using Telerik.WinControls;
//using Telerik.Charting;
using System.Windows.Forms.DataVisualization.Charting;
using Telerik.WinControls.UI.Docking;
using System.Data.SqlClient;
using Taxi_AppMain.Classes;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using Telerik.WinControls.Enumerations;
using System.IO;
using System.Collections;
using Taxi_AppMain.Forms;
using UI;
using System.Drawing.Printing;
using System.Web.UI;

namespace Taxi_AppMain
{
    public partial class frmMultiGraphsReports : UI.SetupBase
    {

        string ReportType = "";
        string DateType = "";
        string DayName;
        int Month = 0;
        int Year = 0;
        int User_Id;
        DateTime from = DateTime.Now.ToDate();
        DateTime to = DateTime.Now.ToDate();

        public frmMultiGraphsReports()
        {
            InitializeComponent();
        }

        public frmMultiGraphsReports(int UserId, string Day_Name, string Type, string Checkdate, int Pmonth, int Pyear, DateTime FromDate, DateTime TillDate)
        {
            InitializeComponent();

            //chart1.Height = 320;
            //chart1.Width = 540;
            //chart2.Height = 320;
            //chart2.Width = 540;
            //chart3.Height = 320;
            //chart3.Width = 540;
            //chart4.Height = 320;
            //chart4.Width = 540;
            //chart5.Height = 320;
            //chart5.Width = 540;
            //chart6.Height = 320;
            //chart6.Width = 540;
            //chart7.Height = 320;
            //chart7.Width = 540;


            //chart3.Location = new Point(38, 370);
            //chart4.Location = new Point(594, 370);
            //chart5.Location = new Point(38, 700);
            //chart6.Location = new Point(594, 700);
            //chart7.Location = new Point(38, 900);

            ReportType = Type;
            DateType = Checkdate;
            Month = Pmonth;
            Year = Pyear;
            User_Id = UserId;
            DayName = Day_Name;
            from = FromDate.ToDateTime();
            to = TillDate.ToDateTime();
            PopulateGraph();

        }
        private void frmGraphsReport_Load(object sender, EventArgs e)
        {

        }
        private void frmGraphsReport_Shown(object sender, EventArgs e)
        {

        }
        private void PopulateGraph()
        {
            try
            {
                string DateStatus = "";
                DateTime FromDate = from;  //   DateTime.Now.ToDate();
                DateTime TillDate = to;
                DateTime Last_WK_FromDate = from; // DateTime.Now.ToDate();
                DateTime Last_WK_TillDate = to; // DateTime.Now.ToDate();

                using (Taxi_Model.TaxiDataContext db = new TaxiDataContext())
                {
                    if (DateType == "CURRENT_WEEK")
                    {
                        FromDate = from; // DateTime.Now.GetStartOfCurrentWeek().ToDate();
                        TillDate = to; //DateTime.Now.GetEndOfCurrentWeek().ToDate();
                        Last_WK_FromDate = from; // DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-7);
                        Last_WK_TillDate = to; // DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-1);

                        DateStatus = " For " + string.Format("{0:dd/MM/yyyy}", FromDate.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", TillDate.ToDate());

                    }
                    else if (DateType == "MONTHLY")
                    {
                        FromDate = from; // new DateTime(Year, Month, 1);

                        TillDate = to; // new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));


                        Last_WK_FromDate = from; // new DateTime(Year, Month - 1, 1);

                        Last_WK_TillDate = to; new DateTime(Year, Month - 1, DateTime.DaysInMonth(Year, Month - 1));

                        DateStatus = " For " + string.Format("{0:dd/MM/yyyy}", FromDate.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", TillDate.ToDate());
                    }
                    else if (DateType == "DAILY")
                    {
                        FromDate = from; // DateTime.Today.ToDateTime();
                        TillDate = to; // DateTime.Now.ToDateTime();

                        DateStatus = "For " + string.Format("{0:dd/MM/yyyy}", FromDate.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", TillDate.ToDate());
                    }

                    // for jobs Graphs
               //     #region // for jobs Graphs
                    if (ReportType == "JOBS")
                    {
                        // chart 1
                        var Acc_Csh = db.stp_GraphData(User_Id, DayName, "ACCOUNT_CASH", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        int CashBooking = Acc_Csh[0].CashBookings.ToInt();
                        int AccountBooking = Acc_Csh[0].AccountBookings.ToInt();

                        int[] yValues = { CashBooking, AccountBooking };
                        string[] xNames = { "Cash: " + CashBooking, "Account: " + AccountBooking };
                        //chart1.Series[0].Points.DataBindXY(xNames, yValues);
                        //chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                        //chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

                        //chart1.Titles[0].Text = "Account VS Cash " + DateStatus;


                        // chart 2

                        //if (DateType == "CURRENT_WEEK")
                        //{
                        //    var WeeklyJobs = db.stp_GraphData(User_Id, DayName, "WEEKLY_JOBS", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //    int?[] Totalbookings = WeeklyJobs.Where(c => c.Status == "CurrentWeek").Select(c => c.Total_Booking).ToArray();
                        //    string[] DaysName = WeeklyJobs.Where(c => c.Status == "CurrentWeek").Select(c => c.Days).ToArray();
                        //    chart2.Series[0].Name = "Job Cureent Week";

                        //    chart2.Series[0].Points.DataBindXY(DaysName, Totalbookings);
                        //    chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //    int?[] LastTotalbookings = WeeklyJobs.Where(c => c.Status == "lastWeek").Select(c => c.Total_Booking).ToArray();
                        //    string[] LastDaysName = WeeklyJobs.Where(c => c.Status == "lastWeek").Select(c => c.Days).ToArray();
                        //    chart2.Series[1].Name = "History Average";
                        //    chart2.Series[1].Points.DataBindXY(LastDaysName, LastTotalbookings);


                        //    for (int i = 0; i < chart2.Series[0].Points.Count; i++)
                        //    {
                        //        chart2.Series[0].Points[i].Label = chart2.Series[0].Points[i].YValues[0].ToStr();

                        //    }

                        //    for (int i = 0; i < chart2.Series[1].Points.Count; i++)
                        //    {
                        //        chart2.Series[1].Points[i].Label = chart2.Series[1].Points[i].YValues[0].ToStr();

                        //    }


                        //    chart2.Titles[0].Text = "Jobs History " + DateStatus;
                        //}
                        //else if (DateType == "MONTHLY")
                        //    //{
                        //        var MonthlyJobs = db.stp_GraphData(User_Id, DayName, "MONTHLY_BOOKINGS", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //        int?[] Totalbookings = MonthlyJobs.Select(c => c.MonthlyBookings).ToArray();
                        //        string[] MonthName = MonthlyJobs.Select(c => c.Bookingmonth).ToArray();
                        //        chart2.Series[0].Name = "Total Jobs";

                        //        chart2.Series[0].Points.DataBindXY(MonthName, Totalbookings);
                        //        chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //        for (int i = 0; i < chart2.Series[0].Points.Count; i++)
                        //        {
                        //            chart2.Series[0].Points[i].Label = chart2.Series[0].Points[i].YValues[0].ToStr();

                        //        }
                        //        chart2.Series[0].Points[1].Color = Color.Orange;
                        //        chart2.Series[1].Name = "History Average";
                        //        chart2.Series[1].Color = Color.Orange;

                        //        chart2.Titles[0].Text = "Total Jobs" + DateStatus;
                        //    //}
                        //    // chart 3

                        //    var hourlyJobs = db.stp_GraphData(User_Id, DayName, "HOUR_JOBS", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                        //    int?[] Totalhourlybookings = hourlyJobs.Where(c => c.HourStatus == "Today_Hour").Select(c => c.TotalHourBookings).ToArray();

                        //    string[] hour = hourlyJobs.Where(c => c.HourStatus == "Today_Hour").Select(c => c.OnHour.ToStr()).ToArray();
                        //    chart3.Series[0].Name = "Today";
                        //    chart3.Series[0].Points.DataBindXY(hour, Totalhourlybookings);
                        //    chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



                        //    int?[] AvgTotalbooking = hourlyJobs.Where(c => c.HourStatus == "Avg_Hour").Select(c => c.TotalHourBookings).ToArray();
                        //    string[] Avghour = hourlyJobs.Where(c => c.HourStatus == "Avg_Hour").Select(c => c.OnHour.ToStr()).ToArray();
                        //    chart3.Series[1].Name = "Average";
                        //    chart3.Series[1].Points.DataBindXY(Avghour, AvgTotalbooking);

                        //    for (int i = 0; i < chart3.Series[0].Points.Count; i++)
                        //    {
                        //        chart3.Series[0].Points[i].Label = chart3.Series[0].Points[i].YValues[0].ToStr();

                        //    }

                        //    for (int i = 0; i < chart3.Series[1].Points.Count; i++)
                        //    {
                        //        chart3.Series[1].Points[i].Label = chart3.Series[1].Points[i].YValues[0].ToStr();

                        //    }
                        //    chart3.Titles[0].Text = "jobs Per Hour";


                        //    // chart 4


                        //    var YearlyJobs = db.stp_GraphData(User_Id, DayName, "YEARLY_BOOKINGS", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                        //    foreach (var obj in YearlyJobs.Where(c => c.YearlyStatus == "Cureent_Year"))
                        //    {
                        //        DataPoint point = new DataPoint();
                        //        //point.Label = obj.TotalMonthlyBooking.ToStr();
                        //        point.AxisLabel = obj.month.ToStr();
                        //        point.LabelForeColor = Color.Navy;
                        //        point.YValues[0] = (double)obj.TotalMonthlyBooking;
                        //        point.XValue = Convert.ToDouble(obj.month_No);
                        //        chart4.Series[0].Points.Add(point);
                        //    }
                        //    chart4.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                        //    foreach (var obj in YearlyJobs.Where(c => c.YearlyStatus == "Last_Year"))
                        //    {
                        //        DataPoint point = new DataPoint();
                        //        //point.Label = obj.TotalMonthlyBooking.ToStr();
                        //        point.AxisLabel = obj.month.ToStr();
                        //        point.LabelForeColor = Color.Navy;
                        //        point.YValues[0] = (double)obj.TotalMonthlyBooking;
                        //        point.XValue = Convert.ToDouble(obj.month_No);
                        //        chart4.Series[1].Points.Add(point);
                        //    }
                        //    chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        //    chart4.Series[0].Name = "Job This Year";
                        //    chart4.Series[1].Name = "History Average";
                        //    chart4.Series[0].BorderWidth = 5;
                        //    chart4.Series[1].BorderWidth = 5;


                        //    chart4.Titles["Title1"].Text = "Job This Year/historical";

                        //    //chart5
                        //    var Value = db.stp_GraphData(User_Id, DayName, "DAILY_JOBS", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //    int?[] TotalDespatch = Value.Select(c => c.TotalHourBookings).ToArray();

                        //    string User = Value.Select(c => c.ControllerName).FirstOrDefault().ToStrIfEmpty();
                        //    int?[] Controller = Value.Select(c => c.OnHour).ToArray();
                        //    chart5.Series[0].Name = "Daily Jobs By " + User + "";
                        //    chart5.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                        //    chart5.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //    for (int i = 0; i < chart5.Series[0].Points.Count; i++)
                        //    {
                        //        chart5.Series[0].Points[i].Label = chart5.Series[0].Points[i].YValues[0].ToStr();

                        //        chart5.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //    }

                        //    chart5.Series[1].Enabled = false;
                        //    chart5.Titles[0].Text = "Daily Jobs" + DateStatus;

                        //    //chart6

                        //    Value = db.stp_GraphData(User_Id, DayName, "WEEKLY_JOBS_TAKEN", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //    TotalDespatch = Value.Select(c => c.TotalHourBookings).ToArray();
                        //    User = Value.Select(c => c.ControllerName).FirstOrDefault().ToStrIfEmpty();
                        //    string[] Controller1 = Value.Select(c => c.OnHour.ToStr()).ToArray();
                        //    chart6.Series[0].Name = "Weekly Jobs By " + User + "";
                        //    chart6.Series[0].Points.DataBindXY(Controller1, TotalDespatch);
                        //    chart6.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //    for (int i = 0; i < chart6.Series[0].Points.Count; i++)
                        //    {
                        //        chart6.Series[0].Points[i].Label = chart6.Series[0].Points[i].YValues[0].ToStr();
                        //        chart6.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //    }

                        //    chart6.Series[1].Enabled = false;
                        //    chart6.Titles[0].Text = "Weekly Jobs" + DateStatus;

                        //    //chart7
                        //    Value = db.stp_GraphData(User_Id, DayName, "MONTHLY_JOBS_TAKEN", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //    TotalDespatch = Value.Select(c => c.Total_Booking).ToArray();
                        //    User = Value.Select(c => c.ControllerName).FirstOrDefault().ToStrIfEmpty();
                        //    Controller1 = Value.Select(c => c.Bookingmonth).ToArray();
                        //    chart7.Series[0].Name = "Monthly Jobs By " + User + "";
                        //    chart7.Series[0].Points.DataBindXY(Controller1, TotalDespatch);
                        //    chart7.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //    for (int i = 0; i < chart7.Series[0].Points.Count; i++)
                        //    {
                        //        chart7.Series[0].Points[i].Label = chart7.Series[0].Points[i].YValues[0].ToStr();
                        //        chart7.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //    }

                        //    chart7.Series[1].Enabled = false;
                        //    chart7.Titles[0].Text = "Monthly Jobs" + DateStatus;


                        //}
                        //#endregion

                        //// for Operator Graph

                        //#region// for Operator Graphs
                        //else if (ReportType == "OPERATORS")
                        //{

                        //    chart7.Visible = false;
                        //    // chart 1
                        //    var OperatorDispatch = db.stp_GraphData(User_Id, DayName, "TOTAL_DESPATCH", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //    int?[] TotalDespatch = OperatorDispatch.Select(c => c.TotalDespatch).ToArray();

                        //    string[] Controller = OperatorDispatch.Select(c => c.Despatchby).ToArray();
                        //    chart1.Series[0].Name = "Operators";
                        //    chart1.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                        //    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart1.Series[0].Points.Count; i++)
                        //    {
                        //        chart1.Series[0].Points[i].Label = chart1.Series[0].Points[i].YValues[0].ToStr();

                        //    }

                        //    chart1.Series[1].Enabled = false;

                        //    chart1.Titles[0].Text = "Top 3 operator(jobs Dispatch)" + DateStatus;


                        //    // chart 2

                        //    var CallRecive = db.stp_GraphData(User_Id, DayName, "TOTAL_CALLRECIVE", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                        //    int?[] TotalCall = CallRecive.Select(c => c.TotalCallRecive).ToArray();

                        //    string[] ControllercallRecive = CallRecive.Select(c => c.ControllerName).ToArray();
                        //    chart2.Series[0].Name = "operators job Taken";
                        //    chart2.Series[0].Points.DataBindXY(ControllercallRecive, TotalCall);
                        //    chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart2.Series[0].Points.Count; i++)
                        //    {
                        //        chart2.Series[0].Points[i].Label = chart2.Series[0].Points[i].YValues[0].ToStr();

                        //    }

                        //    chart2.Series[1].Enabled = false;

                        //    chart2.Titles[0].Text = "Top 3 operater(Call Received)" + DateStatus;

                        //    // chart 3
                        //    var LateDispatch = db.stp_GraphData(User_Id, DayName, "LATE_BOOKING_DISPATCH", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                        //    int?[] TotallateJobs = LateDispatch.Select(c => c.TotalLateBooking).ToArray();

                        //    string[] jobLateBy = LateDispatch.Select(c => c.BookingLateBy).ToArray();
                        //    chart3.Series[0].Name = "operator(Late Jobs)";
                        //    chart3.Series[0].Points.DataBindXY(jobLateBy, TotallateJobs);
                        //    chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart3.Series[0].Points.Count; i++)
                        //    {
                        //        chart3.Series[0].Points[i].Label = chart3.Series[0].Points[i].YValues[0].ToStr();

                        //    }

                        //    chart3.Series[1].Enabled = false;

                        //    chart3.Titles[0].Text = "Top 3 operator(Late Jobs)" + DateStatus;

                        //    // chart 4 

                        //    var operatorsCompaints = db.stp_GraphData(User_Id, DayName, "COMPLAINT_CONTROLLER", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //    int?[] TotalComplaints = operatorsCompaints.Select(c => c.TotalCompaintsForController).ToArray();

                        //    string[] OperatorNameComplaint = operatorsCompaints.Select(c => c.CompalintForController).ToArray();
                        //    chart4.Series[0].Name = "Operators Complaints";
                        //    chart4.Series[0].Points.DataBindXY(OperatorNameComplaint, TotalComplaints);
                        //    chart4.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart4.Series[0].Points.Count; i++)
                        //    {
                        //        chart4.Series[0].Points[i].Label = chart4.Series[0].Points[i].YValues[0].ToStr();
                        //    }

                        //    chart4.Series[1].Enabled = false;

                        //    chart4.Titles[0].Text = "Complaints For Operators" + DateStatus;


                        //    //Chart5

                        //     var Value = db.stp_GraphData(User_Id, DayName, "TOTAL_TAKEN_All_OPERATOR", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //     int?[] TotalTakenValue = Value.Select(c => c.TotalDespatch).ToArray();
                        //     string[] TotalTakenController = Value.Select(c => c.ControllerName).ToArray();
                        //    chart5.Series[0].Name = "All operater(jobs Taken)";
                        //    chart5.Series[0].Points.DataBindXY(TotalTakenController, TotalTakenValue);
                        //    chart5.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //    var colors = new List<Color>
                        //        {

                        //            Color.Blue,
                        //            Color.Red,
                        //            Color.Yellow,
                        //            Color.Green,
                        //            Color.Pink,
                        //            Color.AliceBlue,
                        //            Color.Aqua,
                        //            Color.Aquamarine,
                        //            Color.Azure,
                        //            Color.Beige,
                        //            Color.Bisque,
                        //            Color.BlueViolet,
                        //            Color.Chartreuse,
                        //            Color.Coral,
                        //            Color.Cornsilk,
                        //            Color.DarkOrange,
                        //            Color.DarkSalmon,
                        //            Color.Fuchsia,

                        //            Color.Maroon
                        //        };

                        //    for (int i = 0; i < chart5.Series[0].Points.Count; i++)
                        //    {
                        //        chart5.Series[0].Points[i].Label = chart5.Series[0].Points[i].YValues[0].ToStr();
                        //        chart5.Series[0].Points[i].Color = colors[i];
                        //    }

                        //    chart5.Series[1].Enabled = false;
                        //    chart5.Titles[0].Text = "Jobs Taken" + DateStatus;


                        //    //Chart6
                        //    var Value1 = db.stp_GraphData(User_Id, DayName, "TOTAL_DESPATCH_All_OPERATOR", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();
                        //    int?[] TotalDespatch1 = Value1.Select(c => c.TotalDespatch).ToArray();
                        //    string[] Controller1 = Value1.Select(c => c.Despatchby).ToArray();
                        //    chart6.Series[0].Name = "All operater(jobs Dispatch)";
                        //    chart6.Series[0].Points.DataBindXY(Controller1, TotalDespatch1);
                        //    chart6.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart6.Series[0].Points.Count; i++)
                        //    {
                        //        chart6.Series[0].Points[i].Label = chart6.Series[0].Points[i].YValues[0].ToStr();
                        //        chart6.Series[0].Points[i].Color = colors[i];
                        //        chart6.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //    }
                        //    chart6.Series[1].Enabled = false;
                        //    chart6.Titles[0].Text = "Jobs Dispatch" + DateStatus;


                        //}

                        //#endregion

                        //else if (ReportType == "VEHICLE")
                        //{

                        //    chart1.Height = 320;
                        //    chart1.Width = 540;
                        //    chart2.Height = 320;
                        //    chart2.Width = 540;

                        //    chart3.Visible = false;
                        //    chart4.Visible = false;
                        //    chart5.Visible = false;
                        //    chart6.Visible = false;
                        //    chart7.Visible = false;

                        ////chart1
                        //    var Value = db.stp_GraphData(User_Id, DayName, "COMPANY_VEHICLE_EARNING", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).OrderByDescending(c => c.DriverStatus).ToList();
                        //    decimal?[] TotalEarning = Value.Select(c => c.DriverTotalEarning).ToArray();

                        //    string[] Controller = Value.Select(c => c.ControllerName).ToArray();
                        //    chart1.Series[0].Name = "All Company Vehicle(Company Vehicle Earning)";
                        //    chart1.Series[0].Points.DataBindXY(Controller, TotalEarning);
                        //    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



                        //    for (int i = 0; i < chart1.Series[0].Points.Count; i++)
                        //    {
                        //        chart1.Series[0].Points[i].Label = chart1.Series[0].Points[i].YValues[0].ToStr();
                        //        chart1.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //    }

                        //    chart1.Series[1].Enabled = false;
                        //    chart1.Titles[0].Text = "Company Vehicle";

                        //    //chart2
                        //    Value = db.stp_GraphData(User_Id, DayName, "VEHICLE_EARNING", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).OrderByDescending(c => c.DriverStatus).ToList();
                        //    TotalEarning = Value.Select(c => c.DriverTotalEarning).ToArray();

                        //    Controller = Value.Select(c => c.ControllerName).ToArray();
                        //    chart2.Series[0].Name = "All Vehicle(Vehicle Earning)";
                        //    chart2.Series[0].Points.DataBindXY(Controller, TotalEarning);
                        //    chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart2.Series[0].Points.Count; i++)
                        //    {
                        //        chart2.Series[0].Points[i].Label = chart2.Series[0].Points[i].YValues[0].ToStr();
                        //        chart2.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //    }

                        //    chart2.Series[1].Enabled = false;



                        //}

                        //// for Drivers Graphs
                        //#region// for Drivers Graphs
                        //else if (ReportType == "DRIVERS")
                        //{
                        //    //chart 1
                        //    var DriverList = db.stp_GraphData(User_Id, DayName, "TOP_BOTTOM_DRIVERS", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).OrderByDescending(c => c.DriverStatus).ToList();

                        //    int?[] Totalbookings = DriverList.Select(c => c.TotalDriverJobs).ToArray();

                        //    string[] Driver = DriverList.Select(c => c.DriverNo).ToArray();
                        //    chart1.Series[0].Name = "Top Driver";
                        //    chart1.Series[0].Points.DataBindXY(Driver, Totalbookings);
                        //    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart1.Series[0].Points.Count; i++)
                        //    {
                        //        chart1.Series[0].Points[i].Label = chart1.Series[0].Points[i].YValues[0].ToStr();

                        //    }

                        //    for (int i = 0; i < chart1.Series[1].Points.Count; i++)
                        //    {
                        //        chart1.Series[1].Points[i].Label = chart1.Series[1].Points[i].YValues[0].ToStr();

                        //    }

                        //    chart1.Titles[0].Text = "Top=Bottom Drivers " + DateStatus;


                        //    chart1.Series[0].Points[3].Color = Color.Orange;
                        //    chart1.Series[0].Points[4].Color = Color.Orange;
                        //    chart1.Series[0].Points[5].Color = Color.Orange;
                        //    chart1.Series[1].Name = "Bottom Driver";
                        //    chart1.Series[1].Color = Color.Orange;

                        //    // chart 2
                        //    var DriverRejectJobs = db.stp_GraphData(User_Id, DayName, "DRIVER_REJECT_BOOKING", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                        //    int?[] TotalRejectedJobs = DriverRejectJobs.Select(c => c.rejectedjobs).ToArray();

                        //    string[] DriverName = DriverRejectJobs.Select(c => c.DriverName).ToArray();
                        //    chart2.Series[0].Name = "Drivers(Rejected Jobs)";
                        //    chart2.Series[0].Points.DataBindXY(DriverName, TotalRejectedJobs);
                        //    chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart2.Series[0].Points.Count; i++)
                        //    {
                        //        chart2.Series[0].Points[i].Label = chart2.Series[0].Points[i].YValues[0].ToStr();

                        //    }

                        //    chart2.Series[1].Enabled = false;
                        //    chart2.Titles[0].Text = "Top 3 Drivers(Rejected Jobs)" + DateStatus;

                        //    //chart 3

                        //    var DriverComplaints = db.stp_GraphData(User_Id, DayName, "COMPLAINT_DRIVERS", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                        //    int?[] TotalComplaints = DriverComplaints.Select(c => c.TotalCompaintsForDriver).ToArray();

                        //    string[] DriverNameComplaint = DriverComplaints.Select(c => c.CompalintForDriver).ToArray();
                        //    chart3.Series[0].Name = "Drivers Complaints";
                        //    chart3.Series[0].Points.DataBindXY(DriverNameComplaint, TotalComplaints);
                        //    chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart3.Series[0].Points.Count; i++)
                        //    {
                        //        chart3.Series[0].Points[i].Label = chart3.Series[0].Points[i].YValues[0].ToStr();
                        //    }

                        //    chart3.Series[1].Enabled = false;

                        //    chart3.Titles[0].Text = "Complaints For Drivers" + DateStatus;

                        //    //chart 4

                        //    var DriverEarning = db.stp_GraphData(User_Id, DayName, "DRIVER_TOTAL_EARNING", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                        //    decimal?[] TotalEarning = DriverEarning.Select(c => c.DriverTotalEarning).ToArray();

                        //    string[] EarnDriver = DriverEarning.Select(c => c.EarningDriverName).ToArray();
                        //    chart4.Series[0].Name = "Top 3 Driver(Earning)";
                        //    chart4.Series[0].Points.DataBindXY(EarnDriver, TotalEarning);
                        //    chart4.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart4.Series[0].Points.Count; i++)
                        //    {
                        //        chart4.Series[0].Points[i].Label = chart4.Series[0].Points[i].YValues[0].ToStr();
                        //    }

                        //    chart4.Series[1].Enabled = false;
                        //    chart4.Titles[0].Text = "Driver Earning" + DateStatus;

                        //    //chart5

                        //    var Value = db.stp_GraphData(User_Id, DayName, "DRIVER_RECOVERED_BOOKING", FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).OrderByDescending(c => c.DriverStatus).ToList();
                        //    TotalRejectedJobs = Value.Select(c => c.rejectedjobs).ToArray();
                        //    DriverName = Value.Select(c => c.DriverName).ToArray();
                        //    chart5.Series[0].Name = "Drivers(Recovered Jobs)";
                        //    chart5.Series[0].Points.DataBindXY(DriverName, TotalRejectedJobs);
                        //    chart5.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //    for (int i = 0; i < chart5.Series[0].Points.Count; i++)
                        //    {
                        //        chart5.Series[0].Points[i].Label = chart5.Series[0].Points[i].YValues[0].ToStr();
                        //        chart5.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //    }

                        //    chart5.Series[1].Enabled = false;
                        //    chart5.Titles[0].Text = "Drivers Recovered Jobs" + DateStatus;

                        //    chart6.Visible = false;
                        //    chart7.Visible = false;
                        //}
                        //#endregion

                    }
                }
            }
            catch (Exception ex)
            {
            }

        }



    }

}
