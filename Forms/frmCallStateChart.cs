using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_BLL;
using System.Collections;
using Taxi_Model;
using Utils;
using System.Windows.Forms.DataVisualization.Charting;
using Telerik.WinControls.Enumerations;
using System.Data.Sql;

namespace Taxi_AppMain
{
    public partial class frmCallStateChart : UI.SetupBase
    {
        public frmCallStateChart()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmCallStateChart_Load);
            chkRefNo.Checked = true;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (chkRefNo.Checked == true)
            {
                PopulateData();
            }
            else
            {
                PopulateData2();
            }
            
            //this.fromDate = dtpStatsFromDate.Value.ToDate();
            //this.tillDate = dtpStatsTillDate.Value.ToDate();
            //LoadDriverBookingStats();
        }

        private void frmCallStateChart_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now.GetStartOfCurrentWeek();
            dtpTillDate.Value = DateTime.Now.GetEndOfCurrentWeek();
            PopulateData();
            //AddCreateBookingColumn(grdCalls);
        }

        public override void PopulateData()
        {
            if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                dtpFromDate.Value = null;

            if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                dtpTillDate.Value = null;

            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime now = DateTime.Now.ToDateTime();
            DateTime? tillDate = dtpTillDate.Value.ToDateorNull();



            string hours = ddlHours.SelectedItem.ToStr();
            var total = DateTime.Now.AddHours(-hours.ToInt());


                //var list = (from a in GeneralBLL.GetQueryable<Vw_CallStat>(null).AsEnumerable()
                //            where (fromDate == null || a.CallDateTime.ToDate() >= fromDate)
                //             && (tillDate == null || a.CallDateTime.ToDate() <= tillDate)
                //            orderby a.CallDateTime descending
                //            select new
                //        {
                //            Name = a.Name,
                //            PhoneNumber = a.PhoneNumber,
                //            CallDateTime = a.CallDateTime,
                //            No = a.NoOfCalls
                //        }).ToList();
                

            
           var list = (from a in GeneralBLL.GetQueryable<Vw_CallStat>(c => c.CallDateTime >= total && c.CallDateTime <= now).AsEnumerable()
                       orderby a.CallDateTime descending
                       

                        select new
                        {
                            Name = a.Name,
                            PhoneNumber = a.PhoneNumber,
                            CallDateTime = a.CallDateTime,
                            No = a.NoOfCalls
                        }).ToList();
            

            grdStats.DataSource = list;
         //   DriverChart.Series[0].Points.Clear();

            
            foreach (var obj in list)
            {
                //DataPoint point = new DataPoint();
                //point.Label = obj.Name;
                //point.AxisLabel = obj.PhoneNumber;
                //point.LabelForeColor = Color.Navy;
                //point.LabelToolTip = obj.Name;
                //point.YValues[0] = (double)obj.No;
                //point.SetCustomProperty("BarLabelStyle", "Outside");
                //if (obj.PhoneNumber.Contains("abcdefghijklmnopqrstuvwxyz"))
                //{
                //}
                //else
                //{
                //    point.XValue = Convert.ToDouble(obj.PhoneNumber);
                //}
              //  DriverChart.Series[0].Points.Add(point);
            }

            //grdStats.Columns["ID"].IsVisible = false;
            //grdStats.Columns["Accepted"].IsVisible = false;
            grdStats.Columns["Name"].Width = 170;
            grdStats.Columns["PhoneNumber"].Width = 150;
            grdStats.Columns["CallDateTime"].Width = 180;
            //(grdStats.Columns["CallDateTime"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            grdStats.Columns["CallDateTime"].HeaderText = "Call Date Time";


        }

         void PopulateData2()
        {
            if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                dtpFromDate.Value = null;

            if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                dtpTillDate.Value = null;

            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime now = DateTime.Now.ToDateTime();
            DateTime? tillDate = dtpTillDate.Value.ToDateorNull();



            string hours = ddlHours.SelectedItem.ToStr();
            var total = DateTime.Now.AddHours(-hours.ToInt());


            var list = (from a in GeneralBLL.GetQueryable<Vw_CallStat>(null).AsEnumerable()
                        where (fromDate == null || a.CallDateTime.ToDate() >= fromDate)
                         && (tillDate == null || a.CallDateTime.ToDate() <= tillDate)
                        orderby a.CallDateTime descending
                        select new
                    {
                        Name = a.Name,
                        PhoneNumber = a.PhoneNumber,
                        CallDateTime = a.CallDateTime,
                        No = a.NoOfCalls
                    }).ToList();


            grdStats.DataSource = list;
           // DriverChart.Series[0].Points.Clear();


            foreach (var obj in list)
            {
            //    DataPoint point = new DataPoint();
            //    point.Label = obj.Name;
            //    point.AxisLabel = obj.Name;
            //    point.LabelForeColor = Color.Navy;
            //    point.LabelToolTip = obj.Name;
            //    point.YValues[0] = (double)obj.No;
            //    point.SetCustomProperty("BarLabelStyle", "Outside");
            //    if (obj.PhoneNumber.Contains("abcdefghijklmnopqrstuvwxyz"))
            //    {
            //    }
            //    else
            //    {
            //        point.XValue = Convert.ToDouble(obj.PhoneNumber);
            //    }
            //    DriverChart.Series[0].Points.Add(point);
            }

            //grdStats.Columns["ID"].IsVisible = false;
            //grdStats.Columns["Accepted"].IsVisible = false;
            grdStats.Columns["Name"].Width = 170;
            grdStats.Columns["PhoneNumber"].Width = 150;
            grdStats.Columns["CallDateTime"].Width = 180;
            //(grdStats.Columns["CallDateTime"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            grdStats.Columns["CallDateTime"].HeaderText = "Call Date Time";


        }

        private void chkRefNo_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            DisableChange(args.ToggleState);
        }
        private void DisableChange(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlHours.Enabled = true;
                dtpFromDate.Enabled = false;
                dtpTillDate.Enabled = false;

            }
            else
            {
                ddlHours.Enabled = false;
                dtpFromDate.Enabled = true;
                dtpTillDate.Enabled = true;

            }
        }

    }
}
