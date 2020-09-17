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
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmPeakOffPeakTimeSettings : UI.SetupBase
    {
      
        public frmPeakOffPeakTimeSettings()
        {
            InitializeComponent();
            InitializeConstructor();
        }


      

        private void InitializeConstructor()
        {


            this.Load += new EventHandler(frmFareIncrement_Load);
            this.FormClosed += new FormClosedEventHandler(frmLocations_FormClosed);

            this.grdPeak.ViewCellFormatting += new CellFormattingEventHandler(grdPeak_ViewCellFormatting);


            Dictionary<string, int> days = new Dictionary<string, int>();
            days.Add("Sunday", 0);
            days.Add("Monday", 1);
            days.Add("Tuesday", 2);
            days.Add("Wednesday", 3);
            days.Add("Thursday", 4);
            days.Add("Friday", 5);
            days.Add("Saturday", 6);


            Dictionary<string, int> days2 = new Dictionary<string, int>();
            days2.Add("Sunday", 0);
            days2.Add("Monday", 1);
            days2.Add("Tuesday", 2);
            days2.Add("Wednesday", 3);
            days2.Add("Thursday", 4);
            days2.Add("Friday", 5);
            days2.Add("Saturday", 6);
          



            (grdPeak.Columns["FromDay"] as GridViewComboBoxColumn).DataSource = days;
            (grdPeak.Columns["TillDay"] as GridViewComboBoxColumn).DataSource = days2;

            (grdPeak.Columns["FromTime"] as GridViewDateTimeColumn).CustomFormat = "hh:mm tt";
            (grdPeak.Columns["FromTime"] as GridViewDateTimeColumn).FormatString = "{0:hh:mm tt}";

            (grdPeak.Columns["TillTime"] as GridViewDateTimeColumn).CustomFormat = "hh:mm tt";
            (grdPeak.Columns["TillTime"] as GridViewDateTimeColumn).FormatString = "{0:hh:mm tt}";




            GridViewComboBoxColumn combo =(GridViewComboBoxColumn) grdPeak.Columns["Plot"];

        

            combo.DataSource = General.GetQueryable<Gen_Zone>(c => c.MaxLatitude != null).OrderBy(c => c.OrderNo).ToList();
            combo.DisplayMember = "ZoneName";
            combo.ValueMember = "Id";
            combo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
      


       //     this.ddlIncrementType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlIncrementType_SelectedIndexChanged);
         
        }

        void grdPeak_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name=="Plot" && e.CellElement is GridComboBoxCellElement && e.CellElement.Value.ToStr().Length>0)
            {
               
                
                var data=(List<Gen_Zone>) (e.CellElement.ColumnInfo as GridViewComboBoxColumn).DataSource;
                 e.CellElement.Text =data.FirstOrDefault(c=>c.Id==e.CellElement.Value.ToInt()).DefaultIfEmpty().ZoneName.ToStr();
            }
        }

        void ddlIncrementType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            //if (ddlIncrementType.Text.ToStr().ToLower() == "percent")
            //{
            //    numIncrementRate.Maximum = 100;

            //}
            //else
            //{

            //    numIncrementRate.Maximum = 10000;
            //}
        }

        void frmFareIncrement_Load(object sender, EventArgs e)
        {
            DisplaySettings();
        }

        private void DisplaySettings()
        {

            foreach (var item in General.GetQueryable<PeakTimeSetting>(null))
            {
                var row= grdPeak.Rows.AddNew();

                row.Cells["Id"].Value = item.Id;
                row.Cells["FromDay"].Value =GetString(item.FromDay.ToInt());
                row.Cells["TillDay"].Value = GetString(item.ToDay.ToInt());
                row.Cells["FromTime"].Value = item.FromTime;
                row.Cells["TillTime"].Value = item.ToTill;

                row.Cells["Plot"].Value = item.ZoneId;

               
            

                row.Cells["IncrementPercentage"].Value = item.IncrementPercent.ToDecimal();


                row.Cells["AmountWise"].Value = item.IsAmountWise.ToBool();
                row.Cells["Amount"].Value = item.Amount.ToDecimal();
            }

            numIncrementRate.Value = AppVars.objPolicyConfiguration.PeakTimeIncPercent.ToDecimal();
        }

        private string GetString(int day)
        {
            string rtn="";
            if (day == 0)
                rtn = "Sunday," + day.ToStr();
            else if (day == 1)
            {
                rtn = "Monday," + day.ToStr();

            }
            else if (day == 2)
            {
                rtn = "Tuesday," + day.ToStr();

            }
            else if (day == 3)
            {
                rtn = "Wednesday," + day.ToStr();

            }
            else if (day == 4)
            {
                rtn = "Thursday," + day.ToStr();

            }
            else if (day == 5)
            {
                rtn = "Friday," + day.ToStr();

            }
            else if (day == 6)
            {
                rtn = "Saturday," + day.ToStr();

            }

            return rtn;



        }

        void frmLocations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
        }
        
      

      
     

      

        



  
     
        void frmLocations_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveSettings())
            {
                Close();

            }
        }

        private bool SaveSettings()
        {
            bool rtn = false;

            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.PeakTimeSettings.DeleteAllOnSubmit(db.PeakTimeSettings);

                    db.SubmitChanges();


                    foreach (var item in grdPeak.Rows)
                    {
                        int fromDay = item.Cells["FromDay"].Value.ToStr().Split(new char[]{','})[1].Trim().Replace("]","").Trim().ToInt();
                        int tillDay = item.Cells["TillDay"].Value.ToStr().Split(new char[] { ',' })[1].Trim().Replace("]","").Trim().ToInt();


                        db.PeakTimeSettings.InsertOnSubmit(new PeakTimeSetting
                        {
                            FromDay = fromDay,
                            ToDay = tillDay,
                            FromTime = item.Cells["FromTime"].Value.ToDateTime(),
                            ToTill = item.Cells["TillTime"].Value.ToDateTime(),
                             ZoneId=item.Cells["Plot"].Value.ToInt(),
                              IncrementPercent=item.Cells["IncrementPercentage"].Value.ToDecimal(),
                               IsAmountWise=item.Cells["AmountWise"].Value.ToBool(),
                                Amount=item.Cells["Amount"].Value.ToDecimal()
                        });

                        
                        db.SubmitChanges();
                        
                    }

                    var objGen = db.Gen_SysPolicy_Configurations.FirstOrDefault();

                    objGen.PeakTimeIncPercent = numIncrementRate.Value.ToInt();

                    db.SubmitChanges();                  



                }

                AppVars.objPolicyConfiguration = General.GetObject<Gen_SysPolicy_Configuration>(c => c.Id != 0);




              
                rtn = true;
            }
            catch (Exception ex)
            {
                
                ENUtils.ShowMessage(ex.Message);
            }


            return rtn;

        }

        private void radRadioButton1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            if (args.ToggleState == ToggleState.On)
            {
                //dtpFromDate.CustomFormat = "dd/MM/yyyy HH:mm";
                //dtpTillDate.CustomFormat = "dd/MM/yyyy HH:mm";

                //dtpFromDate.ShowUpDown = false;
                //dtpTillDate.ShowUpDown = false;
            }

        }

        private void optDateWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                //dtpFromDate.CustomFormat = "dd/MM/yyyy";
                //dtpTillDate.CustomFormat = "dd/MM/yyyy";
                //dtpFromDate.ShowUpDown = false;
                //dtpTillDate.ShowUpDown = false;
            }
        }

        private void optTimeWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
              

                //dtpFromDate.ShowUpDown = true;
                //dtpTillDate.ShowUpDown = true;



                //dtpFromDate.CustomFormat = "HH:mm";
                //dtpTillDate.CustomFormat = "HH:mm";
            }
        }

        private void grdPeak_Click(object sender, EventArgs e)
        {

        }

    }



}
