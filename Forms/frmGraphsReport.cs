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
using System.Drawing.Imaging;
using System.Net.Mail;

namespace Taxi_AppMain
{
    public partial class frmGraphsReport : UI.SetupBase
    {

        string ReportType = "";
        string DateType = "";
        int Month = 0;
        int Year = 0;
        int User_Id;
        string DayName;
        DateTime from;
        DateTime till;
        public bool _cond = true ;
        public frmGraphsReport()
        {
            InitializeComponent();
        }

        public frmGraphsReport(int UserId, string Day_Name,  string Type, string Checkdate, int Pmonth, int Pyear, string FromDate, string TillDate)
        {
            InitializeComponent();
            ReportType = Type;
            DateType = Checkdate;
            Month = Pmonth;
            Year = Pyear;
            from = FromDate.ToDate();
            till = TillDate.ToDate() + TimeSpan.Parse("23:59:59"); ;
            User_Id = UserId;
            DayName = Day_Name;
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
                DateTime FromDate = DateTime.Now.ToDate();
                DateTime TillDate = DateTime.Now.ToDate();
                DateTime Last_WK_FromDate = DateTime.Now.ToDate();
                DateTime Last_WK_TillDate = DateTime.Now.ToDate();

                using (Taxi_Model.TaxiDataContext db = new TaxiDataContext())
                {
                    if (DateType == "CURRENT_WEEK")
                    {
                        FromDate = DateTime.Now.GetStartOfCurrentWeek().ToDate();
                        TillDate = DateTime.Now.GetEndOfCurrentWeek().ToDate();
                        Last_WK_FromDate = DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-7);
                        Last_WK_TillDate = DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-1);

                        DateStatus = " For " + string.Format("{0:dd/MM/yyyy}", FromDate.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", TillDate.ToDate());

                    }
                    //else if (DateType == "LAST_WEEK")
                    //{

                    //}
                    else if (DateType == "MONTHLY")
                    {
                        FromDate = new DateTime(Year, Month, 1);

                        TillDate = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));


                        //Last_WK_FromDate = new DateTime(Year, Month - 1, 1);

                        //Last_WK_TillDate = new DateTime(Year, Month - 1, DateTime.DaysInMonth(Year, Month - 1));

                        DateStatus = " For " + string.Format("{0:dd/MM/yyyy}", FromDate.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", TillDate.ToDate());
                    }

                    else if (DateType == "Datewise")
                    {
                        FromDate = from;

                        TillDate = till;


                        //Last_WK_FromDate = new DateTime(Year, Month - 1, 1);

                        //Last_WK_TillDate = new DateTime(Year, Month - 1, DateTime.DaysInMonth(Year, Month - 1));

                        DateStatus = " For " + string.Format("{0:dd/MM/yyyy}", FromDate.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", TillDate.ToDate());
                    }

                    else if (DateType == "DAILY")
                    {
                        FromDate = DateTime.Today.ToDateTime();
                        TillDate = DateTime.Now.ToDateTime();

                        DateStatus = "For " + string.Format("{0:dd/MM/yyyy}", FromDate.ToDate()) + " to " + string.Format("{0:dd/MM/yyyy}", TillDate.ToDate());
                    }

                    FromDate = FromDate.ToDate();
                    TillDate = TillDate.ToDate() + TimeSpan.Parse("23:59:59");
                    var Value = db.stp_GraphData(User_Id, DayName, ReportType, FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).ToList();

                    if (Value.Count > 0)
                    {


                        if (ReportType == "ACCOUNT_CASH")
                        {
                            //int CashBooking = Value[0].CashBookings.ToInt();
                            //int AccountBooking = Value[0].AccountBookings.ToInt();

                            //int[] yValues = { CashBooking, AccountBooking };
                            //string[] xNames = { "Cash: " + CashBooking, "Account: " + AccountBooking };
                            //MainChart.Series[0].Points.DataBindXY(xNames, yValues);
                            //MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                            //MainChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                            //lblTop.Text = "Account VS Cash";

                            //MainChart.Titles[0].Text = "Account VS Cash " + DateStatus;
                        }
                        else if (ReportType == "WEEKLY_JOBS")
                        {

                            //int?[] Totalbookings = Value.Where(c => c.Status == "CurrentWeek").Select(c => c.Total_Booking).ToArray();
                            //string[] DaysName = Value.Where(c => c.Status == "CurrentWeek").Select(c => c.Days).ToArray();
                            //MainChart.Series[0].Name = "Job Cureent Week";

                            //MainChart.Series[0].Points.DataBindXY(DaysName, Totalbookings);
                            //MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                            //int?[] LastTotalbookings = Value.Where(c => c.Status == "lastWeek").Select(c => c.Total_Booking).ToArray();
                            //string[] LastDaysName = Value.Where(c => c.Status == "lastWeek").Select(c => c.Days).ToArray();
                            //MainChart.Series[1].Name = "History Average";
                            //MainChart.Series[1].Points.DataBindXY(LastDaysName, LastTotalbookings);
                            //lblTop.Text = "Job History";


                            //for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                            //{
                            //    MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();

                            //}

                            //for (int i = 0; i < MainChart.Series[1].Points.Count; i++)
                            //{
                            //    MainChart.Series[1].Points[i].Label = MainChart.Series[1].Points[i].YValues[0].ToStr();

                            //}


                            //MainChart.Titles[0].Text = "Jobs History " + DateStatus;


                        }



                        else if (ReportType == "DAILY_TOTAL_JOBS")
                        {

                            //int?[] Totalbookings = Value.Where(c => c.Status == "CurrentDay").Select(c => c.Total_Booking).ToArray();
                            //string[] DaysName = Value.Where(c => c.Status == "CurrentDay").Select(c => c.Days).ToArray();
                            //MainChart.Series[0].Name = "Job Cureent Day";

                            //MainChart.Series[0].Points.DataBindXY(DaysName, Totalbookings);
                            //MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                            //int?[] LastTotalbookings = Value.Where(c => c.Status == "lastDay").Select(c => c.Total_Booking).ToArray();
                            //string[] LastDaysName = Value.Where(c => c.Status == "lastDay").Select(c => c.Days).ToArray();
                            //MainChart.Series[1].Name = "History Average";
                            //MainChart.Series[1].Points.DataBindXY(LastDaysName, LastTotalbookings);
                            //lblTop.Text = "Job History";


                            //for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                            //{
                            //    MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                            //    MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                            //}

                            //for (int i = 0; i < MainChart.Series[1].Points.Count; i++)
                            //{
                            //    MainChart.Series[1].Points[i].Label = MainChart.Series[1].Points[i].YValues[0].ToStr();

                            //}


                            //MainChart.Titles[0].Text = "Jobs History " + DateStatus;


                        }


                        else if (ReportType == "HOUR_JOBS")
                        {
                            //int?[] Totalbookings = Value.Where(c => c.HourStatus == "Today_Hour").Select(c => c.TotalHourBookings).ToArray();

                            //string[] hour = Value.Where(c => c.HourStatus == "Today_Hour").Select(c => c.OnHour.ToStr()).ToArray();
                            //MainChart.Series[0].Name = "Today";
                            //MainChart.Series[0].Points.DataBindXY(hour, Totalbookings);
                            //MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



                            //int?[] AvgTotalbooking = Value.Where(c => c.HourStatus == "Avg_Hour").Select(c => c.TotalHourBookings).ToArray();
                            //string[] Avghour = Value.Where(c => c.HourStatus == "Avg_Hour").Select(c => c.OnHour.ToStr()).ToArray();
                            //MainChart.Series[1].Name = "Average";
                            //MainChart.Series[1].Points.DataBindXY(Avghour, AvgTotalbooking);
                            //lblTop.Text = "jobs Per Hour";

                            //for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                            //{
                            //    MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();

                            //}

                            //for (int i = 0; i < MainChart.Series[1].Points.Count; i++)
                            //{
                            //    MainChart.Series[1].Points[i].Label = MainChart.Series[1].Points[i].YValues[0].ToStr();

                            //}
                            //MainChart.Titles[0].Text = "jobs Per Hour" + DateStatus;


                        }
                        else if (ReportType == "TOP_BOTTOM_DRIVERS")
                        {
                            //FromDate = FromDate.ToDate();
                            //TillDate = TillDate.ToDate() + TimeSpan.Parse("23:59:59");
                            //var DriverList = db.stp_GraphData(User_Id, DayName, ReportType, FromDate, TillDate, Last_WK_FromDate, Last_WK_TillDate).OrderByDescending(c => c.DriverStatus).ToList();

                            //int?[] Totalbookings = DriverList.Select(c => c.TotalDriverJobs).ToArray();

                            //string[] Driver = DriverList.Select(c => c.DriverNo).ToArray();
                            //MainChart.Series[0].Name = "Top Driver";
                            //MainChart.Series[0].Points.DataBindXY(Driver, Totalbookings);
                            //MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                            //for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                            //{
                            //    MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();

                            //}

                            //for (int i = 0; i < MainChart.Series[1].Points.Count; i++)
                            //{
                            //    MainChart.Series[1].Points[i].Label = MainChart.Series[1].Points[i].YValues[0].ToStr();

                            //}

                            //MainChart.Titles[0].Text = "Top=Bottom Drivers " + DateStatus;


                            //MainChart.Series[0].Points[3].Color = Color.Orange;
                            //MainChart.Series[0].Points[4].Color = Color.Orange;
                            //MainChart.Series[0].Points[5].Color = Color.Orange;
                            //MainChart.Series[1].Name = "Bottom Driver";
                            //MainChart.Series[1].Color = Color.Orange;

                            //lblTop.Text = "Top=Bottom Drivers";


                        }
                        else if (ReportType == "YEARLY_BOOKINGS")
                        {
                            //foreach (var obj in Value.Where(c => c.YearlyStatus == "Cureent_Year"))
                            //{
                            //    DataPoint point = new DataPoint();
                            //    point.Label = obj.TotalMonthlyBooking.ToStr();
                            //    point.AxisLabel = obj.month.ToStr();
                            //    point.LabelForeColor = Color.Navy;
                            //    point.YValues[0] = (double)obj.TotalMonthlyBooking;
                            //    point.XValue = Convert.ToDouble(obj.month_No);
                            //    MainChart.Series[0].Points.Add(point);
                            //}
                            //MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                            //foreach (var obj in Value.Where(c => c.YearlyStatus == "Last_Year"))
                            //{
                            //    DataPoint point = new DataPoint();
                            //    point.Label = obj.TotalMonthlyBooking.ToStr();
                            //    point.AxisLabel = obj.month.ToStr();
                            //    point.LabelForeColor = Color.Navy;
                            //    point.YValues[0] = (double)obj.TotalMonthlyBooking;
                            //    point.XValue = Convert.ToDouble(obj.month_No);
                            //    MainChart.Series[1].Points.Add(point);
                            //}
                            //MainChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                            //MainChart.Series[0].Name = "Job This Year";
                            //MainChart.Series[1].Name = "History Average";
                            //MainChart.Series[0].BorderWidth = 5;
                            //MainChart.Series[1].BorderWidth = 5;
                            //lblTop.Text = "Job This Year/historical";


                            //MainChart.Titles["Title1"].Text = "Job This Year/historical";


                        }
                        else if (ReportType == "TOTAL_DESPATCH")
                        {
                            //int?[] TotalDespatch = Value.Select(c => c.TotalDespatch).ToArray();

                            //string[] Controller = Value.Select(c => c.Despatchby).ToArray();
                            //MainChart.Series[0].Name = "Top 3 operater(jobs Dispatch)";
                            //MainChart.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                            //MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                            //for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                            //{
                            //    MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();

                            //}

                            //MainChart.Series[1].Enabled = false;
                            //lblTop.Text = "Jobs Dispatch";

                            //MainChart.Titles[0].Text = "Jobs Dispatch" + DateStatus;
                        }

                        //    else if (ReportType == "TOTAL_TAKEN_All_OPERATOR")
                        //    {
                        //        int?[] TotalDespatch = Value.Select(c => c.TotalDespatch).ToArray();

                        //        string[] Controller = Value.Select(c => c.ControllerName).ToArray();
                        //        MainChart.Series[0].Name = "All operater(jobs Taken)";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //        var colors = new List<Color>
                        //        {

                        //            Color.Blue,
                        //            Color.Red,
                        //            Color.Yellow,
                        //            Color.Green,
                        //            Color.Pink,
                        //            Color.AliceBlue,
                        //             Color.Aqua,
                        //              Color.Aquamarine,
                        //               Color.Azure,
                        //                Color.Beige,
                        //                 Color.Bisque,
                        //                  Color.BlueViolet,
                        //                   Color.Chartreuse,
                        //                    Color.Coral,
                        //                     Color.Cornsilk,
                        //                      Color.DarkOrange,
                        //                       Color.DarkSalmon,
                        //                        Color.Fuchsia,

                        //            Color.Maroon
                        //        };

                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Color = colors[i];
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Jobs Taken";
                        //        MainChart.Titles[1].Text = "Operators";
                        //        MainChart.Titles[2].Text = "No of Bookings";
                        //        MainChart.Titles[0].Text = "Jobs Taken" + DateStatus;
                        //    }

                        //    else if (ReportType == "WEEKLY_JOBS_TAKEN")
                        //    {
                        //        int?[] TotalDespatch = Value.Select(c => c.TotalHourBookings).ToArray();
                        //        string User = Value.Select(c => c.ControllerName).FirstOrDefault().ToStrIfEmpty();
                        //        string[] Controller = Value.Select(c => c.OnHour.ToStr()).ToArray();
                        //        MainChart.Series[0].Name = "Weekly Jobs By " + User + "";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Weekly Jobs";

                        //        MainChart.Titles[0].Text = "Weekly Jobs" + DateStatus;
                        //        MainChart.Titles[1].Text = "Week No";
                        //        MainChart.Titles[2].Text = "No of Bookings";

                        //    }

                        //    else if (ReportType == "DAILY_JOBS")
                        //    {
                        //        int?[] TotalDespatch = Value.Select(c => c.TotalHourBookings).ToArray();
                        //        string User = Value.Select(c => c.ControllerName).FirstOrDefault().ToStrIfEmpty();
                        //        int?[] Controller = Value.Select(c => c.OnHour).ToArray();
                        //        MainChart.Series[0].Name = "Daily Jobs By " + User + "";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //           // MainChart.ChartAreas.["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 2.25F, System.Drawing.FontStyle.Bold);
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        //MainChart.Series[1].Font = Font.Bold;
                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Daily Jobs";

                        //        MainChart.Titles[0].Text = "Daily Jobs" + DateStatus;
                        //        MainChart.Titles[1].Text = "Hours";
                        //        MainChart.Titles[2].Text = "No of Bookings";
                        //    }


                        //    else if (ReportType == "MONTHLY_JOBS_TAKEN")
                        //    {
                        //        int?[] TotalDespatch = Value.Select(c => c.Total_Booking).ToArray();
                        //        string User = Value.Select(c => c.ControllerName).FirstOrDefault().ToStrIfEmpty();
                        //        string[] Controller = Value.Select(c => c.Bookingmonth).ToArray();
                        //        MainChart.Series[0].Name = "Monthly Jobs By " + User +"";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Monthly Jobs";
                        //        MainChart.Titles[2].Text = "No of Bookings";
                        //        MainChart.Titles[1].Text = "Month";
                        //        MainChart.Titles[0].Text = "Monthly Jobs" + DateStatus;
                        //    }

                        //    else if (ReportType == "VEHICLE_EARNING")
                        //    {
                        //        decimal?[] TotalEarning = Value.Select(c => c.DriverTotalEarning).ToArray();

                        //        string[] Controller = Value.Select(c => c.ControllerName).ToArray();
                        //        MainChart.Series[0].Name = "All Vehicle(Vehicle Earning)";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalEarning);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Vehicle Earning";

                        //      //  MainChart.Titles[0].Text = "Jobs Taken" + DateStatus;
                        //        MainChart.Titles[1].Text = "Vehicle";
                        //        MainChart.Titles[2].Text = "Bookings";
                        //    }

                        //    else if (ReportType == "COMPANY_VEHICLE_EARNING")
                        //    {
                        //        decimal?[] TotalEarning = Value.Select(c => c.DriverTotalEarning).ToArray();

                        //        string[] Controller = Value.Select(c => c.ControllerName).ToArray();
                        //        MainChart.Series[0].Name = "All Company Vehicle(Company Vehicle Earning)";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalEarning);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;



                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Company Vehicle Earning";

                        //       // MainChart.Titles[0].Text = "Jobs Taken" + DateStatus;
                        //        MainChart.Titles[1].Text = "Company Vehicle";
                        //        MainChart.Titles[2].Text = "Bookings";
                        //    }

                        //   else if (ReportType == "TOTAL_DESPATCH_All_OPERATOR")
                        //    {
                        //        int?[] TotalDespatch = Value.Select(c => c.TotalDespatch).ToArray();

                        //        string[] Controller = Value.Select(c => c.Despatchby).ToArray();
                        //        MainChart.Series[0].Name = "All operater(jobs Dispatch)";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalDespatch);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //        var colors = new List<Color>
                        //        {

                        //            Color.Blue,
                        //            Color.Red,
                        //            Color.Yellow,
                        //            Color.Green,
                        //            Color.Pink,
                        //            Color.AliceBlue,
                        //             Color.Aqua,
                        //              Color.Aquamarine,
                        //               Color.Azure,
                        //                Color.Beige,
                        //                 Color.Bisque,
                        //                  Color.BlueViolet,
                        //                   Color.Chartreuse,
                        //                    Color.Coral,
                        //                     Color.Cornsilk,
                        //                      Color.DarkOrange,
                        //                       Color.DarkSalmon,
                        //                        Color.Fuchsia,

                        //            Color.Maroon
                        //        };

                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Color = colors[i];
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Jobs Dispatch";
                        //        MainChart.Titles[1].Text = "Operators";
                        //        MainChart.Titles[2].Text = "No of Bookings";
                        //        MainChart.Titles[0].Text = "Jobs Dispatch" + DateStatus;
                        //    }

                        //    else if (ReportType == "DRIVER_RECOVERED_BOOKING")
                        //    {
                        //        int?[] TotalRejectedJobs = Value.Select(c => c.rejectedjobs).ToArray();

                        //        string[] DriverName = Value.Select(c => c.DriverName).ToArray();
                        //        MainChart.Series[0].Name = "Drivers(Recovered Jobs)";
                        //        MainChart.Series[0].Points.DataBindXY(DriverName, TotalRejectedJobs);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Drivers Recovered Jobs";
                        //        MainChart.Titles[1].Text = "Drivers";
                        //        MainChart.Titles[2].Text = "No of Booking";
                        //        MainChart.Titles[0].Text = "Drivers Recovered Jobs" + DateStatus;
                        //    }

                        //    else if (ReportType == "TOTAL_CALLRECIVE")
                        //    {
                        //        int?[] TotalCall = Value.Select(c => c.TotalCallRecive).ToArray();

                        //        string[] Controller = Value.Select(c => c.ControllerName).ToArray();
                        //        MainChart.Series[0].Name = "Top 3 operater(Call Received)";
                        //        MainChart.Series[0].Points.DataBindXY(Controller, TotalCall);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Jobs Taken";

                        //        MainChart.Titles[0].Text = "Jobs Taken" + DateStatus;
                        //    }
                        //    else if (ReportType == "DRIVER_REJECT_BOOKING")
                        //    {
                        //        int?[] TotalRejectedJobs = Value.Select(c => c.rejectedjobs).ToArray();

                        //        string[] DriverName = Value.Select(c => c.DriverName).ToArray();
                        //        MainChart.Series[0].Name = "Top 3 Drivers(Rejected Jobs)";
                        //        MainChart.Series[0].Points.DataBindXY(DriverName, TotalRejectedJobs);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Drivers Rejected Jobs";
                        //        MainChart.Titles[1].Text = "Drivers";
                        //        MainChart.Titles[2].Text = "No of Booking";
                        //        MainChart.Titles[0].Text = "Drivers Rejected Jobs" + DateStatus;
                        //    }
                        //    else if (ReportType == "LATE_BOOKING_DISPATCH")
                        //    {
                        //        int?[] TotallateJobs = Value.Select(c => c.TotalLateBooking).ToArray();

                        //        string[] jobLateBy = Value.Select(c => c.BookingLateBy).ToArray();
                        //        MainChart.Series[0].Name = "Top 3 operator(Late Jobs)";
                        //        MainChart.Series[0].Points.DataBindXY(jobLateBy, TotallateJobs);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Operator Late Jobs";

                        //        MainChart.Titles[0].Text = "Operator Late Jobs" + DateStatus;
                        //    }
                        //    else if (ReportType == "COMPLAINT_DRIVERS")
                        //    {
                        //        int?[] TotalComplaints = Value.Select(c => c.TotalCompaintsForDriver).ToArray();

                        //        string[] DriverNameComplaint = Value.Select(c => c.CompalintForDriver).ToArray();
                        //        MainChart.Series[0].Name = "Complaints For Drivers";
                        //        MainChart.Series[0].Points.DataBindXY(DriverNameComplaint, TotalComplaints);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Complaints For Drivers";

                        //        MainChart.Titles[0].Text = "Complaints For Drivers" + DateStatus;

                        //    }
                        //    else if (ReportType == "COMPLAINT_CONTROLLER")
                        //    {
                        //        int?[] TotalComplaints = Value.Select(c => c.TotalCompaintsForController).ToArray();

                        //        string[] OperatorNameComplaint = Value.Select(c => c.CompalintForController).ToArray();
                        //        MainChart.Series[0].Name = "Complaints For Operators";
                        //        MainChart.Series[0].Points.DataBindXY(OperatorNameComplaint, TotalComplaints);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Complaints For Operators";


                        //        MainChart.Titles[0].Text = "Complaints For Operators" + DateStatus;
                        //    }
                        //    else if (ReportType == "DRIVER_TOTAL_EARNING")
                        //    {
                        //        decimal?[] TotalEarning = Value.Select(c => c.DriverTotalEarning).ToArray();

                        //        string[] DriverName = Value.Select(c => c.EarningDriverName).ToArray();
                        //        MainChart.Series[0].Name = "Top 3 Driver(Earning)";
                        //        MainChart.Series[0].Points.DataBindXY(DriverName, TotalEarning);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }

                        //        MainChart.Series[1].Enabled = false;
                        //        lblTop.Text = "Driver Earning";


                        //        MainChart.Titles[0].Text = "Driver Earning" + DateStatus;
                        //    }
                        //    else if (ReportType == "MONTHLY_BOOKINGS")
                        //    {

                        //        int?[] Totalbookings = Value.Select(c => c.MonthlyBookings).ToArray();
                        //        string[] MonthName = Value.Select(c => c.Bookingmonth).ToArray();
                        //        MainChart.Series[0].Name = "Monthly Jobs";

                        //        MainChart.Series[0].Points.DataBindXY(MonthName, Totalbookings);
                        //        MainChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        //        for (int i = 0; i < MainChart.Series[0].Points.Count; i++)
                        //        {
                        //            MainChart.Series[0].Points[i].Label = MainChart.Series[0].Points[i].YValues[0].ToStr();
                        //            MainChart.Series[0].Points[i].Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold);
                        //        }
                        //        MainChart.Series[0].Points[0].Color = Color.Orange;
                        //        //  MainChart.Series[0].Points[1].Color = Color.Orange;
                        //        MainChart.Series[1].Name = "History Average";
                        //        MainChart.Series[1].Color = Color.Orange;
                        //        lblTop.Text = "Monthly Jobs";

                        //        MainChart.Titles[0].Text = "Monthly Jobs" + DateStatus;
                        //    }
                        //}
                        //else
                        //{
                        //    ENUtils.ShowMessage("Data Not found..");
                        //     _cond = false;

                        //    //frmShowGraphs frm = new frmShowGraphs();
                        //    //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmShowGraphs1");

                        //    //if (doc == null)
                        //    //{
                        //    //    doc.Close();
                        //    //}
                        //    //MainMenuForm.MainMenuFrm.ShowForm(frm);
                        //}
                    }

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //Bitmap MyChartPanel = new Bitmap(graphPanel.Width, graphPanel.Height);
                //graphPanel.DrawToBitmap(MyChartPanel, new Rectangle(0, 0, graphPanel.Width, graphPanel.Height));

                //PrintDialog MyPrintDialog = new PrintDialog();

                //if (MyPrintDialog.ShowDialog() == DialogResult.OK)
                //{
                //    System.Drawing.Printing.PrinterSettings values;
                //    values = MyPrintDialog.PrinterSettings;
                //    MyPrintDialog.Document = MyPrintDocument;
                //    MyPrintDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
                //    MyPrintDocument.Print();
                //}

                //MyPrintDocument.Dispose();




                System.Drawing.Printing.Margins myMargins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
                //MainChart.Printing.PrintDocument.DefaultPageSettings.Margins = myMargins;
                //MainChart.Printing.Print(true);



            }
            catch (Exception ex)
            {
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmEmail frm = new frmEmail(MainChart, "GraphReport");
            //    frm.ShowDialog();
            //    frm.Dispose();

              
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Graphs";
                sfd.Filter = "Images|*.jpg;*.bmp;*.png";
                ImageFormat format = ImageFormat.Jpeg;

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //string ext = System.IO.Path.GetExtension(sfd.FileName);
                    
               //     MainChart.SaveImage(sfd.FileName, format);
                }

            }
            catch (Exception ex)
            {
            }


        }


        //MainChart.SaveImage(@"D:\abc.jpg", System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Jpeg);




    }
}
