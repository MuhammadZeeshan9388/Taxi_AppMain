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

namespace Taxi_AppMain
{
    public partial class frmShowGraphs : UI.SetupBase
    {
        public frmShowGraphs()
        {
            InitializeComponent();

        }
        string DayName = string.Empty;
        private void frmShowGraphs_Load(object sender, EventArgs e)
        {

 
            int Month = DateTime.Now.Month.ToInt();
            ddlMonths.SelectedIndex = Month.ToInt();
            ddlMonths.SelectedIndex = Month.ToInt();
            //ddlYear.SelectedText = DateTime.Now.Year.ToStr();

            ComboFunctions.FillUsersCombo(ddlUsers);
            
            dtpFromDate.Value = DateTime.Now.ToDateTime();
            dtpTillDate.Value = DateTime.Now.ToDateTime();

            rdoWeekly.IsChecked = true;
            chkCurrentWeek.Checked = true;
            rdoAccountVsCash.IsChecked = true;


            lblCurrentWeek.Text = "Current Week ( " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetStartOfCurrentWeek().ToDate()) + " - " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetEndOfCurrentWeek().ToDate()) + " )";

            lblLastWeek.Text = "Last Week ( " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-7)) + " - " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-1)) + " )";

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportType = "";
                string DateType = "";
                int Month = 0;
                int Year = 0;

                DateTime ? From = dtpFromDate.Value.ToDateorNull();
                DateTime? Till = dtpTillDate.Value.ToDateorNull();
                Till= Till + TimeSpan.Parse("23:59:59");
              
                if (rdoAccountVsCash.IsChecked == true)
                {
                    ReportType = "ACCOUNT_CASH";
                }
                else if (rdoTotalJobs.IsChecked == true)
                {
                    if (rdoWeekly.IsChecked == true)
                    {
                        ReportType = "WEEKLY_JOBS";
                    }
                    else if (rdoDaily.IsChecked == true)
                    {
                        ReportType = "DAILY_TOTAL_JOBS";
                    }
                    else if (rdoMonthly.IsChecked == true)
                    {
                        ReportType = "MONTHLY_BOOKINGS";
                    }
                }

                else if (rdoHourJobs.IsChecked == true)
                {
                    ReportType = "HOUR_JOBS";
                }
                else if (rdoTopDrivers.IsChecked == true)
                {
                    ReportType = "TOP_BOTTOM_DRIVERS";
                }
                else if (rdoYearlyReport.IsChecked == true)
                {
                    ReportType = "YEARLY_BOOKINGS";
                }
                else if (rdoTotalDespatch.IsChecked == true)
                {
                    ReportType = "TOTAL_DESPATCH";
                }
                else if (rdoTotalCallRecive.IsChecked == true)
                {
                    ReportType = "TOTAL_CALLRECIVE";
                }
                else if (rdoDriverRejectJobs.IsChecked == true)
                {
                    ReportType = "DRIVER_REJECT_BOOKING";
                }
                else if (rdoLateDespatchJob.IsChecked == true)
                {
                    ReportType = "LATE_BOOKING_DISPATCH";
                }
                else if (rdoDriverCompaints.IsChecked == true)
                {
                    ReportType = "COMPLAINT_DRIVERS";
                }
                else if (rdoOperatorCompaints.IsChecked == true)
                {
                    ReportType = "COMPLAINT_CONTROLLER";
                }
                else if (rdoAllOperatorTaken.IsChecked == true)
                {
                    ReportType = "TOTAL_TAKEN_All_OPERATOR";
                }
                else if (rdoAllOperatorDespatched.IsChecked == true)
                {
                    ReportType = "TOTAL_DESPATCH_All_OPERATOR";
                }
                else if (rdoDriverEarning.IsChecked == true)
                {
                    ReportType = "DRIVER_TOTAL_EARNING";
                }
                else if (rdoVehicleEarning.IsChecked == true)
                {
                    ReportType = "VEHICLE_EARNING";
                }
                else if (rdoFleetVehicleEarning.IsChecked == true)
                {
                    ReportType = "COMPANY_VEHICLE_EARNING";
                }
                else if (rdoDailyJobsTaken.IsChecked == true)
                {
                    ReportType = "DAILY_JOBS";

                    if (ddlUsers.SelectedValue == null)
                    {
                        ENUtils.ShowMessage("Operator Required");
                        return;
                    }

                }
                else if (rdoWeeklyJobsTaken.IsChecked == true)
                {
                    ReportType = "WEEKLY_JOBS_TAKEN";
                    if (ddlUsers.SelectedValue == null)
                    {
                        ENUtils.ShowMessage("Operator Required");
                        return;
                    }
                }
                else if (rdoMonthlyJobsTaken.IsChecked == true)
                {
                    ReportType = "MONTHLY_JOBS_TAKEN";
                    if (ddlUsers.SelectedValue == null)
                    {
                        ENUtils.ShowMessage("Operator Required");
                        return;
                    }
                }
                else if (rdoDriverRecoverJob.IsChecked == true)
                {
                    ReportType = "DRIVER_RECOVERED_BOOKING";
                }

                if (rdoWeekly.IsChecked == true)
                {
                    DateType = "CURRENT_WEEK";
                    if (chkLastWeek.Checked == true)
                    {
                        DateType = "LAST_WEEK";
                    }
                }
                else if (rdoMonthly.IsChecked == true)
                {
                    DateType = "MONTHLY";
                    Month = ddlMonths.SelectedIndex.ToInt();
                    string yr = ddlYear.SelectedText == "" ? DateTime.Now.Year.ToStr() : ddlYear.SelectedText.ToStr();

                    Year = (yr).ToInt();

                }
                else if (rdoDateCriteria.IsChecked == true)
                {
                    DateType = "Datewise";
                   
                }
                else if (rdoDaily.IsChecked == true)
                {
                    DateType = "DAILY";
                }

               

                if (rdoAll.Checked == true)
                {
                    DayName = "";
                }
                else if (rdoMon.Checked == true)
                {
                    DayName = "Monday";
                }
                else if (rdoTues.Checked == true)
                {
                    DayName = "Tuesday";
                }
                else if (rdoWed.Checked == true)
                {
                    DayName = "Wednesday";
                }
                else if (rdoThurs.Checked == true)
                {
                    DayName = "Thursday";
                }
                else if (rdoFri.Checked == true)
                {
                    DayName = "Friday";
                }
                else if (rdoSat.Checked == true)
                {
                    DayName = "Saturday";
                }
                else if (rdoSat.Checked == true)
                {
                    DayName = "Sunday";
                }



                frmGraphsReport frm = new frmGraphsReport(ddlUsers.SelectedValue.ToInt(), DayName, ReportType, DateType, Month, Year, From.ToString(), Till.ToString());

                if (frm._cond == true)
                {
                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmGraphsReport1");

                    if (doc != null)
                    {
                        doc.Close();
                    }


                    MainMenuForm.MainMenuFrm.ShowForm(frm);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void rdoMonthly_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            RadioMonthly(args.ToggleState);
        }
        private void RadioMonthly(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlYear.SelectedText = DateTime.Now.Year.ToStr();

                //dtpFromDate.Enabled = false;
                //dtpTillDate.Enabled = false;

                //ddlUsers.Enabled = false;
                //radPanel11.Enabled = false;
               
                ddlMonths.Visible = true;
                lblMonth.Visible = true;
                lblYear.Visible = true;
                ddlYear.Visible = true;
                chkLastWeek.Visible = false;
                chkCurrentWeek.Visible = false;

                lblCurrentWeek.Visible = false;
                lblLastWeek.Visible = false;
            }
            else
            {
                ddlMonths.Visible = false;
                lblMonth.Visible = false;
                lblYear.Visible = false;
                ddlYear.Visible = false;
                chkLastWeek.Visible = true;
                chkCurrentWeek.Visible = true;
                lblCurrentWeek.Visible = true;
                lblLastWeek.Visible = true;
            }
        }

        private void btnAllOperators_Click(object sender, EventArgs e)
        {
            try
            {
                string DateType = "";
                int Month = 0;
                int Year = 0;

                DateTime? FromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? TillDate = dtpTillDate.Value.ToDateorNull();
                TillDate = TillDate + TimeSpan.Parse("23:59:59");

                if (rdoWeekly.IsChecked == true)
                {
                    DateType = "CURRENT_WEEK";
                    if (chkLastWeek.Checked == true)
                    {
                        DateType = "LAST_WEEK";
                    }
                }
                else if (rdoMonthly.IsChecked == true)
                {
                    DateType = "MONTHLY";
                    Month = ddlMonths.SelectedIndex.ToInt();
                    string yr = ddlYear.SelectedText == "" ? DateTime.Now.Year.ToStr() : ddlYear.SelectedText.ToStr();
                    Year = (yr).ToInt();
                }
                else if (rdoDaily.IsChecked == true)
                {
                    DateType = "DAILY";
                }

                if (rdoDateCriteria.IsChecked == true)
                {
                    
                        frmMultiGraphsReports frm = new frmMultiGraphsReports(ddlUsers.SelectedValue.ToInt(), DayName, "OPERATORS", DateType, Month, Year, FromDate.ToDateTime(), TillDate.ToDateTime());


                        DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmMultiGraphsReports1");

                        if (doc != null)
                        {
                            doc.Close();
                        }

                        MainMenuForm.MainMenuFrm.ShowForm(frm);
                    
                }
                else
                {
                    ENUtils.ShowMessage("Select Date Criteria from Multi Graphs");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void btnAllJobs_Click(object sender, EventArgs e)
        {
            try
            {
                string DateType = "";
                int Month = 0;
                int Year = 0;

                if (rdoAll.Checked == true)
                {
                    DayName = "";
                }
                else if (rdoMon.Checked == true)
                {
                    DayName = "Monday";
                }
                else if (rdoTues.Checked == true)
                {
                    DayName = "Tuesday";
                }
                else if (rdoWed.Checked == true)
                {
                    DayName = "Wednesday";
                }
                else if (rdoThurs.Checked == true)
                {
                    DayName = "Thursday";
                }
                else if (rdoFri.Checked == true)
                {
                    DayName = "Friday";
                }
                else if (rdoSat.Checked == true)
                {
                    DayName = "Saturday";
                }
                else if (rdoSat.Checked == true)
                {
                    DayName = "Sunday";
                }

                DateTime? FromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? TillDate = dtpTillDate.Value.ToDateorNull();
                TillDate = TillDate + TimeSpan.Parse("23:59:59");

                if (rdoWeekly.IsChecked == true)
                {
                    DateType = "CURRENT_WEEK";
                    if (chkLastWeek.Checked == true)
                    {
                        DateType = "LAST_WEEK";
                    }
                }
                else if (rdoMonthly.IsChecked == true)
                {
                    DateType = "MONTHLY";
                    Month = ddlMonths.SelectedIndex.ToInt();
                    string yr = ddlYear.SelectedText == "" ? DateTime.Now.Year.ToStr() : ddlYear.SelectedText.ToStr();

                    Year = (yr).ToInt();

                }
                else if (rdoDaily.IsChecked == true)
                {
                    DateType = "DAILY";
                }


                if (rdoDateCriteria.IsChecked == true)
                {
                if (ddlUsers.SelectedValue != null)
                    {
                    frmMultiGraphsReports frm = new frmMultiGraphsReports(ddlUsers.SelectedValue.ToInt(), DayName, "JOBS", DateType, Month, Year, FromDate.ToDateTime(), TillDate.ToDateTime());


                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmMultiGraphsReports1");

                    if (doc != null)
                    {
                        doc.Close();
                    }


                    MainMenuForm.MainMenuFrm.ShowForm(frm);
                    }
                else
                {
                    ENUtils.ShowMessage("Controller Required");
                }
                
                 }
                else
                {
                    ENUtils.ShowMessage("Select Date Criteria from Multi Graphs");
                }
                
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void btnAllDrivers_Click(object sender, EventArgs e)
        {
            try
            {
                string DateType = "";
                int Month = 0;
                int Year = 0;

                DateTime? FromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? TillDate = dtpTillDate.Value.ToDateorNull();
                TillDate = TillDate + TimeSpan.Parse("23:59:59");

                if (rdoWeekly.IsChecked == true)
                {
                    DateType = "CURRENT_WEEK";
                    if (chkLastWeek.Checked == true)
                    {
                        DateType = "LAST_WEEK";
                    }
                }
                else if (rdoMonthly.IsChecked == true)
                {
                    DateType = "MONTHLY";
                    Month = ddlMonths.SelectedIndex.ToInt();
                    string yr = ddlYear.SelectedText == "" ? DateTime.Now.Year.ToStr() : ddlYear.SelectedText.ToStr();
                    Year = (yr).ToInt();

                }
                else if (rdoDaily.IsChecked == true)
                {
                    DateType = "DAILY";
                    
                }

                if (rdoDateCriteria.IsChecked == true)
                {
                    frmMultiGraphsReports frm = new frmMultiGraphsReports(ddlUsers.SelectedValue.ToInt(), DayName, "DRIVERS", DateType, Month, Year, FromDate.ToDateTime(), TillDate.ToDateTime());


                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmMultiGraphsReports1");

                    if (doc != null)
                    {
                        doc.Close();
                    }


                    MainMenuForm.MainMenuFrm.ShowForm(frm);
                }
                else
                {
                    ENUtils.ShowMessage("Select Date Criteria from Multi Graphs");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void rdoDaily_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            RadioDaily(args.ToggleState);
        }
        private void RadioDaily(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {

                             
                lblLastWeek.Text = "From ( " + DateTime.Today.ToDateTime() + ")";

                lblCurrentWeek.Text = "Till ( " + DateTime.Now.ToDateTime() + ")";

                lblCurrentWeek.Visible = true;
                lblLastWeek.Visible = true;
                

                
            }
            else
            {

                lblCurrentWeek.Text = "Current Week ( " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetStartOfCurrentWeek().ToDate()) + " - " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetEndOfCurrentWeek().ToDate()) + " )";

                lblLastWeek.Text = "Last Week ( " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-7)) + " - " + string.Format("{0:dd/MM/yyyy}", DateTime.Now.GetStartOfCurrentWeek().ToDate().AddDays(-1)) + " )";

            }
        }

        private void DateWise(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {

             
                rdoDailyJobsTaken.Visible = true;
                rdoWeeklyJobsTaken.Visible = true;
                rdoMonthlyJobsTaken.Visible = true;

                pnlDateWise.Visible = true;

               
            }
            else
            {

                
                rdoDailyJobsTaken.Visible = false;
                rdoWeeklyJobsTaken.Visible = false;
                rdoMonthlyJobsTaken.Visible = false;
                pnlDateWise.Visible = false;
            }
        }

        private void rdoDateCriteria_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            DateWise(args.ToggleState);
          
        }

        private void rdoWeekly_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            Weekly(args.ToggleState);       
        }

        private void Weekly(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {

                //ddlUsers.Enabled = false;
                //dtpFromDate.Enabled = false;
                //dtpTillDate.Enabled = false;

                //radPanel11.Enabled = false;

            }
        }

        private void rdoGraphVehicles_Click(object sender, EventArgs e)
        {
            try
            {
                string DateType = "";
                int Month = 0;
                int Year = 0;

                DateTime? FromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? TillDate = dtpTillDate.Value.ToDateorNull();
                TillDate = TillDate + TimeSpan.Parse("23:59:59");

                if (rdoDateCriteria.IsChecked == true)
                {
                    frmMultiGraphsReports frm = new frmMultiGraphsReports(ddlUsers.SelectedValue.ToInt(), DayName, "VEHICLE", DateType, Month, Year, FromDate.ToDateTime(), TillDate.ToDateTime());

                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmMultiGraphsReports1");

                    if (doc != null)
                    {
                        doc.Close();
                    }


                    MainMenuForm.MainMenuFrm.ShowForm(frm);
                }
                else
                {
                    ENUtils.ShowMessage("Select Date Criteria from Multi Graphs");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }



 

    }
}
