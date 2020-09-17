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
using System.Data.Linq;

namespace Taxi_AppMain
{
    public partial class frmFareIncrement : UI.SetupBase
    {
      
        public frmFareIncrement()
        {
            InitializeComponent();
            InitializeConstructor();
			

		}

     

        private void InitializeConstructor()
        {


            this.Load += new EventHandler(frmFareIncrement_Load);
            this.FormClosed += new FormClosedEventHandler(frmLocations_FormClosed);

            this.ddlIncrementType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlIncrementType_SelectedIndexChanged);
         



        }

        void ddlIncrementType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            if (ddlIncrementType.Text.ToStr().ToLower() == "percent")
            {
                numIncrementRate.Maximum = 100;

            }
            else
            {

                numIncrementRate.Maximum = 10000;
            }
        }
		

		
		private void PopulateData()
		{
			

			using (TaxiDataContext db = new TaxiDataContext())
			{



				Table<Fare_IncrementSetting> Fare = db.GetTable<Fare_IncrementSetting>();

				var list = from c in Fare
						   select new FareIncreament
						   {
							   Id = c.Id,
							   IsFareIncreament = c.EnableIncrement,
							   FromDateTime = c.FromDate,
							   TillDateTime = c.TillDate,
							   IncreamentRate =Convert.ToDecimal(c.IncrementRate),
							   IncreamentType = c.IncrementType,
                              CriteriaBy=  c.CriteriaBy
						   };




				grdFareIncreament.DataSource = list.ToList();

				//var query = from c in General.GetQueryable<Fare_IncrementSetting>(null).AsEnumerable()

				//			select new
				//			{
				//				Id = c.Id,
				//				EnableIncrement = c.EnableIncrement,
				//				FromDate = c.FromDate,
				//				TillDate = c.TillDate,
				//				IncrementRate = c.IncrementRate,
				//				IncrementType = c.IncrementType,
				//			};

				//grdFareIncreament.DataSource = list.ToList();

				(grdFareIncreament.Columns["FromDateTime"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";
				(grdFareIncreament.Columns["TillDateTime"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";



                grdFareIncreament.Columns["CriteriaBy"].IsVisible = false;
                grdFareIncreament.Columns["Id"].IsVisible = false;
				grdFareIncreament.Columns["IsFareIncreament"].HeaderText = "Active";
				grdFareIncreament.Columns["FromDateTime"].HeaderText = "From Date/Time";
				grdFareIncreament.Columns["TillDateTime"].HeaderText = "Till Date/Time";
				grdFareIncreament.Columns["IncreamentType"].HeaderText = "Type";
				grdFareIncreament.Columns["IncreamentRate"].HeaderText = "Rate";

				grdFareIncreament.Columns["FromDateTime"].Width = 120;
				grdFareIncreament.Columns["TillDateTime"].Width = 120;
				grdFareIncreament.Columns["IncreamentType"].Width = 100;
				grdFareIncreament.Columns["IncreamentRate"].Width = 100;


				if (grdFareIncreament.Rows.Count > 0)
				{
					chkEnableIncrement.Checked = grdFareIncreament.Rows[0].Cells["IsFareIncreament"].Value.ToBool();
				}


			}

			GridViewCommandColumn col = new GridViewCommandColumn();
			col.Width = 60;

			col.Name = "delete";
			col.UseDefaultText = true;
			col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
			col.DefaultText = "Delete";
			col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

			grdFareIncreament.Columns.Add(col);
			grdFareIncreament.CommandCellClick += GrdFareIncreament_CommandCellClick;
		}

		private void GrdFareIncreament_CommandCellClick(object sender, EventArgs e)
		{
			try
			{

				GridCommandCellElement gridCell = (GridCommandCellElement)sender;


				if (gridCell.ColumnInfo.Name == "delete")
				{
					int id=gridCell.RowInfo.Cells["Id"].Value.ToInt();


					if (id > 0)
					{
						using(TaxiDataContext db = new TaxiDataContext())
						{
							Fare_IncrementSetting objfare = db.Fare_IncrementSettings.FirstOrDefault(c => c.Id == id);
						    db.Fare_IncrementSettings.DeleteOnSubmit(objfare);
							db.SubmitChanges();

							gridCell.RowInfo.Delete();
						}

					}

				

				}


			}
			catch
			{

			}

		}

		void frmFareIncrement_Load(object sender, EventArgs e)
        {
			PopulateData();
         //   DisplaySettings();
        }

        private void DisplaySettings()
        {
            var obj = General.GetObject<Fare_IncrementSetting>(c => c.Id != 0);

            if (obj != null)
            {
                chkEnableIncrement.Checked = obj.EnableIncrement.ToBool();
                dtpFromDate.Value = obj.FromDate;
                dtpTillDate.Value = obj.TillDate;

                ddlIncrementType.Text = obj.IncrementType.ToStr().Trim().ToProperCase();
                numIncrementRate.Value = obj.IncrementRate.ToDecimal();
				
                if (obj.CriteriaBy.ToInt() == 1)
                {
                    optDateTimeWise.ToggleState = ToggleState.On;
                }
                else if (obj.CriteriaBy.ToInt() == 2)
                {
                    optDateWise.ToggleState = ToggleState.On;
                }
                else if (obj.CriteriaBy.ToInt() == 3)
                {
                    optTimeWise.ToggleState = ToggleState.On;
                }

            }

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
				//if (dtpFromDate.Value == null)
				//{
				//	ENUtils.ShowMessage("Required : From Date");
				//	return rtn;

				//}

				//if (dtpTillDate.Value == null)
				//{
				//	ENUtils.ShowMessage("Required : Till Date");
				//	return rtn;

				//}


				//if (dtpFromDate.Value.ToDate() > dtpTillDate.Value.ToDate())
				//{
				//	ENUtils.ShowMessage("Required : From Date must be less than Till Date");
				//	return rtn;

				//}


				//string criteriaBy = ddlIncrementType.Text.Trim().ToLower();

				//if (optDateTimeWise.ToggleState == ToggleState.On)
				//	criteriaBy += "=1";
				//else if (optDateWise.ToggleState == ToggleState.On)
				//	criteriaBy += "=2";
				//else if (optTimeWise.ToggleState == ToggleState.On)
				//	criteriaBy += "=3";


				using (TaxiDataContext db = new TaxiDataContext())
				{
					List<int> Ids = grdFareIncreament.Rows.Select(callnotification => Convert.ToInt32(callnotification.Cells["Id"].Value)).ToList();

					for (int i=0; i<grdFareIncreament.Rows.Count; i++)
					{

						int id = grdFareIncreament.Rows[i].Cells["Id"].Value.ToInt();
						Fare_IncrementSetting objFare =null;

						if (id.ToInt() > 0)
							objFare = db.Fare_IncrementSettings.FirstOrDefault(c => c.Id == id);
						else
							objFare = new Fare_IncrementSetting();
						
							
								objFare.FromDate = grdFareIncreament.Rows[i].Cells["FromDateTime"].Value.ToDateTime();
								objFare.TillDate = grdFareIncreament.Rows[i].Cells["TillDateTime"].Value.ToDateTime();
								objFare.IncrementRate = grdFareIncreament.Rows[i].Cells["IncreamentRate"].Value.ToDecimal();
                                objFare.EnableIncrement = chkEnableIncrement.Checked;
								objFare.IncrementType = grdFareIncreament.Rows[i].Cells["IncreamentType"].Value.ToString().ToLower();


                        objFare.CriteriaBy = grdFareIncreament.Rows[i].Cells["CriteriaBy"].Value.ToInt();

                        

							if (objFare.Id == 0)
								db.Fare_IncrementSettings.InsertOnSubmit(objFare);

								db.SubmitChanges();

							

						
						
					}


				}


                return true;
			}
			//db.stp_SaveFareIncrementSettings(dtpFromDate.Value, dtpTillDate.Value, criteriaBy, numIncrementRate.Value.ToDecimal(), chkEnableIncrement.Checked);

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
                dtpFromDate.CustomFormat = "dd/MM/yyyy HH:mm";
                dtpTillDate.CustomFormat = "dd/MM/yyyy HH:mm";

                dtpFromDate.ShowUpDown = false;
                dtpTillDate.ShowUpDown = false;
            }

        }

        private void optDateWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpFromDate.CustomFormat = "dd/MM/yyyy";
                dtpTillDate.CustomFormat = "dd/MM/yyyy";
                dtpFromDate.ShowUpDown = false;
                dtpTillDate.ShowUpDown = false;
            }
        }

        private void optTimeWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
              

                dtpFromDate.ShowUpDown = true;
                dtpTillDate.ShowUpDown = true;



                dtpFromDate.CustomFormat = "HH:mm";
                dtpTillDate.CustomFormat = "HH:mm";
            }
        }

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				
				if (dtpFromDate.Value == null)
				{
					ENUtils.ShowMessage("Required : From Date");
					return ;

				}

				if (dtpTillDate.Value == null)
				{
					ENUtils.ShowMessage("Required : Till Date");
					return ;

				}


				if (dtpFromDate.Value.ToDate() > dtpTillDate.Value.ToDate())
				{
					ENUtils.ShowMessage("Required : From Date must be less than Till Date");
					return ;

				}

				string dtfromTime = dtpFromDate.Value.ToString();
					string dtTillTime = dtpTillDate.Value.ToString();

                int criteriaby = 0;

                if (optDateTimeWise.ToggleState == ToggleState.On)
                {
                    criteriaby = 1;
                }
                else if (optDateWise.ToggleState == ToggleState.On)
                {
                    criteriaby = 2;
                }
                else if (optTimeWise.ToggleState == ToggleState.On)
                {
                    criteriaby = 3;
                }
                //dtfromTime =  dtfromTime.ToDateTime();
                //dtTillTime =  dtTillTime.ToDateTime();

                var row = new FareIncreament()
					{

						Id = 0,
						IsFareIncreament = chkEnableIncrement.Checked,
						IncreamentType = ddlIncrementType.Text,
						IncreamentRate = numIncrementRate.Value.ToDecimal(),
						FromDateTime = Convert.ToDateTime(dtfromTime),
						TillDateTime = Convert.ToDateTime(dtTillTime)
                        , CriteriaBy= criteriaby
                   };

					var grd = (List<FareIncreament>)grdFareIncreament.DataSource;
					grd.Add(row);

					grdFareIncreament.DataSource = grd.ToList();

					numIncrementRate.Value = 0;
					dtpFromDate.Value = null;
					dtpTillDate.Value = null;




                grdFareIncreament.CurrentRow = null;


            }
			catch (Exception ex)
			{

				throw;
			}
		}
	}

	class FareIncreament
	{
		public int Id { get; set; }
		public DateTime? FromDateTime { get; set; }
		public DateTime? TillDateTime { get; set; }
		
		
		public decimal IncreamentRate { get; set; }
		public string IncreamentType { get; set; }


        public Boolean? IsFareIncreament { get; set; }
        public int? CriteriaBy { get; set; }

    }

}
