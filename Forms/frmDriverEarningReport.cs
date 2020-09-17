using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Utils;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using System.Collections;

namespace Taxi_AppMain
{
    public partial class frmDriverEarningReport : UI.SetupBase
    {
        bool IsReportLoaded = false;
        public frmDriverEarningReport()
        {

            InitializeComponent();
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnView.Click += new EventHandler(btnView_Click);
        }

        void btnView_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
            DateTime? dtTill = dtpToDate.Value.ToDateorNull();


            if (dtFrom != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                dtFrom = (dtFrom.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



            if (dtTill != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                dtTill = (dtTill.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();
            string Error = string.Empty;
            if (dtFrom == null)
            {
                Error = "Required: From Date";
            }
            if (dtTill == null)
            {
                if (string.IsNullOrEmpty(Error))
                {
                    Error = "Required: To Date";
                }
                else
                {
                    Error += Environment.NewLine + "Required: To Date";
                }
            }
            if (!string.IsNullOrEmpty(Error))
            {
                ENUtils.ShowMessage(Error);
                return;
            }
            rptfrmDriverEarning frm = new rptfrmDriverEarning();
            frm.LoadReport();

            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverEarning1");

            if (doc != null)
            {
                doc.Close();
            }
            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            //LoadReport();
        }
        public void LoadReport()
        {
            try
            {
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate.Value.ToDateorNull();


                if (dtFrom != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    dtFrom = (dtFrom.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



                if (dtTill != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    dtTill = (dtTill.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();
                string Error = string.Empty;
                if (dtFrom == null)
                {
                    Error = "Required: From Date";
                }
                if (dtTill == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required: To Date";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required: To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

               // grdLister.DataSource = GetData(dtFrom, dtTill, ddlAllDriver.SelectedValue.ToInt());

                var list = (from a in new Taxi_Model.TaxiDataContext().stp_GetDriverEarning(dtFrom, dtTill, ddlAllDriver.SelectedValue.ToInt(), Enums.BOOKINGSTATUS.DISPATCHED, 0)
                            select new
                            {
                                //DriverId = a.DriverId,
                                //DriverNo = a.DriverNo,
                                //Name = a.Name,
                                //LoginHour = a.LoginHour,
                                //TotalDays = a.TotalDays,
                                //TotalHrs = a.TotalHrs,
                                //Total = a.Total,
                                //Noshow = a.Noshow,
                                //JobsDone = a.JobsDone,
                                //Decline = a.Decline,
                                //Earning =  (a.Total-a.Commission),
                                //Account = a.Account,
                                //Cash = a.Cash,
                                //Commission = a.Commission,
                                //AvgJob = AvgJob(a.Total.ToDecimal(),a.Commission.ToDecimal(),a.JobsDone.ToInt()),//(((a.Total - a.Commission) / (a.JobsDone))).ToDecimal(),
                                //Avghour = ((a.Total/(a.LoginHour.ToInt()==0?1:a.LoginHour))).ToDecimal(),
                                //Avgday = ((a.Total /( a.LoginDateTime.ToInt()==0?1:a.LoginDateTime))).ToDecimal(),
                                //LoginDateTime = a.LoginDateTime,
                                DriverId = a.DriverId,
                                DriverNo = a.DriverNo,

                                Name = a.Name,
                                LoginDateTime = a.LoginDateTime,
                                LoginHour = a.LoginHour,
                                BreakTime = 0,
                                // TotalDays = a.TotalDays,
                               // TotalHrs = a.TotalHrs,
                                JobsDone = a.JobsDone,
                                Decline = a.Decline,
                                Noshow = a.Noshow,
                               
                              


                                //Earning = (a.TotalDays==2?(a.Total-a.Commission):a.Total).ToDecimal(),

                                Cash = a.Cash,
                                Account = a.Account,
                                
                                Total = a.Total,
                                Commission = a.Commission,
                                Rent= a.TotalDays==1? a.Commission:0.00m,
                                Earning = (a.TotalDays == 2 ? (a.Total - a.Commission) : a.Total).ToDecimal(),
                                AvgJob = AvgJob(a.Total.ToDecimal(), a.TotalDays == 2 ? a.Commission.ToDecimal() : 0, a.JobsDone.ToInt()),//(((a.Total - a.Commission) / (a.JobsDone))).ToDecimal(),
                                Avgday = ((a.Total / (a.LoginDateTime.ToInt() == 0 ? 1 : a.LoginDateTime))).ToDecimal(),
                                Avghour = ((a.Total / (a.LoginHour.ToInt() == 0 ? 1 : a.LoginHour))).ToDecimal(),
                              

                            }).ToList();
                var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();

                grdLister.DataSource = list2;
                grdLister.Columns["DriverId"].IsVisible = false;
                grdLister.Columns["DriverNo"].HeaderText = "Driver";
                grdLister.Columns["Name"].Width = 140;
                grdLister.Columns["LoginDateTime"].HeaderText = "Days";
                grdLister.Columns["LoginHour"].HeaderText = "Hrs";
                grdLister.Columns["JobsDone"].HeaderText = "Complete";
                grdLister.Columns["JobsDone"].HeaderText = "Complete";
                grdLister.Columns["Noshow"].HeaderText = "N/S";
                grdLister.Columns["Commission"].HeaderText = "Comm.";
                grdLister.Columns["AvgJob"].HeaderText = "Avg/Job";
                grdLister.Columns["BreakTime"].HeaderText = "Break";
                grdLister.Columns["Avghour"].HeaderText = "Avg/Hr";
                (grdLister.Columns["Cash"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";
                (grdLister.Columns["Account"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";
                (grdLister.Columns["Total"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";

                (grdLister.Columns["Commission"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";
                (grdLister.Columns["Rent"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";
                (grdLister.Columns["Earning"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";

                (grdLister.Columns["AvgJob"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";
                (grdLister.Columns["Avgday"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";
                (grdLister.Columns["Avghour"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";
                grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                IsReportLoaded = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private decimal AvgJob(decimal Total, decimal Commission, int JobsDone)
        { 
            decimal Avg=0.00m;

            if (JobsDone == 0)
                JobsDone = 1;


          
                Avg = ((Total - Commission) / JobsDone);
            
            return Avg;
        }

      

    


     

       

       

       

        private void rptfrmJobsList_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;



            TimeSpan tillTime = TimeSpan.Zero;

            TimeSpan.TryParse("23:59:59", out tillTime);

            dtpToDate.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue()).Date);



            dtptilltime.Value = dtpToDate.Value.Value.Date + tillTime;

            FillCombo();
        }

       


        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate.Value.ToDateorNull();


                if (dtFrom != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    dtFrom = (dtFrom.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



                if (dtTill != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    dtTill = (dtTill.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();
                string Error = string.Empty;
                if (dtFrom == null)
                {
                    Error = "Required: From Date";
                }
                if (dtTill == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required: To Date";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required: To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                rptfrmDriverEarning frm = new rptfrmDriverEarning(); 
                frm.LoadReport();
                frm.ExportReport();

                //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverEarning1");

                //if (doc != null)
                //{
                //    doc.Close();
                //}
                //UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }



        private void FillCombo()
        {
            ComboFunctions.FillDriverNoComboSorted(ddlAllDriver);

        }
        
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate.Value.ToDateorNull();


                if (dtFrom != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    dtFrom = (dtFrom.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



                if (dtTill != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    dtTill = (dtTill.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();
                string Error = string.Empty;
                if (dtFrom == null)
                {
                    Error = "Required: From Date";
                }
                if (dtTill == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required: To Date";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required: To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                rptfrmDriverEarning frm = new rptfrmDriverEarning();
                frm.LoadReport();
                frm.ExportReportToExcel("Excel");

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void chkAllDriver_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlAllDriver.SelectedValue = null;
                ddlAllDriver.Enabled = false;
            }
            else
            {
                ddlAllDriver.Enabled = true;
            }
        }



      

       





    }
}
