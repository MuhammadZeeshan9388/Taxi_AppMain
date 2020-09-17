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
using DAL;
using Utils;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using Telerik.WinControls;
using UI;

namespace Taxi_AppMain
{
    public partial class frmFares : UI.SetupBase
    {
        FareBO objMaster;
        public struct COLS_DETAILS
        {
            public static string ID = "ID";
            public static string FAREID = "FAREID";
            public static string FROMLOCTYPEID = "FROMLOCTYPEID";
            public static string TOLOCTYPEID = "TOLOCTYPEID";

            public static string FROMLOCATIONID = "FROMLOCATIONID";
            public static string TOLOCATIONID = "TOLOCATIONID";

            public static string FROMLOCATION = "FromLocation";
            public static string TOLOCATION = "ToLocation";
            public static string FARE = "Fare";
            public static string COMPANYFARE = "COMPANYFare";

        }


        private bool HasOffPeakRate = false;


        public struct COLS_OTHERDETAILS
        {
            public static string ID = "ID";
            public static string FAREID = "FAREID";


            public static string FROMMILE = "FromMile";
            public static string TOMILE = "ToMile";


            public static string PEAKTIME = "Peak";
            public static string PEAKRATE = "PeakRate";


            public static string OFFPEAKTIME = "OffPeak";
            public static string OFFPEAKRATE = "OffPeakRate";



            public static string FROMSTARTTIME = "FromStartTime";
            public static string TILLSTARTTIME = "TillStartTime";


            public static string FROMENDTIME = "FromEndTime";
            public static string TILLENDTIME = "TillEndTime";


            public static string FARE = "Fare";
            public static string COMPANYFARE = "COMPANYFare";

        }
        public struct COLS_PLOTEWISE
        {
            public static string Id = "Id";
            public static string FareId = "FareId";
            public static string FromZoneId = "FromZoneId";
            public static string ToZoneId = "ToZoneId";

            public static string FromPlotNo = "FromPlotNo";
            public static string FromPlot = "FromPlot";
            public static string ToPlot = "ToPlot";
            public static string ToPlotNo = "ToPlotNo";

            public static string Price = "Price";
            public static string COMPANYFARE = "COMPANYFare";
        }
        //
        //Hyde Park 
        public struct COLS_AirportCommission
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Location = "Location";
            public static string LocationId = "LocationId";
            public static string LocationTypeId = "LocationTypeId";
            public static string CommissionPercent = "CommissionPercent";
            public static string CommissionOnPercent = "CommissionOnPercent";
            public static string CommissionAmount = "CommissionAmount";
            public static string FareId = "FareId";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string VehicleType = "VehicleType";
            public static string CustomerPrice = "CustomerPrice";
            public static string DriverPrice = "DriverPrice";
            public static string CompanyPrice = "CompanyPrice";


        }
        public struct COLS_StationCommission
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Location = "Location";
            public static string LocationId = "LocationId";
            public static string LocationTypeId = "LocationTypeId";
            public static string CommissionPercent = "CommissionPercent";
            public static string CommissionOnPercent = "CommissionOnPercent";
            public static string CommissionAmount = "CommissionAmount";
            public static string FareId = "FareId";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string VehicleType = "VehicleType";
            public static string CustomerPrice = "CustomerPrice";
            public static string DriverPrice = "DriverPrice";
            public static string CompanyPrice = "CompanyPrice";
        }
        private UI.MyGridView grdAirportCommission;
        private Telerik.WinControls.UI.RadLabel radLabel36;
        private void InitializeAirportCommission()
        {
            if (grdAirportCommission != null)
                return;
            try
            {
                this.grdAirportCommission = new UI.MyGridView();
                this.radLabel36 = new Telerik.WinControls.UI.RadLabel();
                this.pageAirportCharges.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission.MasterTemplate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel36)).BeginInit();
                // 
                // pageAirportCommission
                // 

                this.pageAirportCharges.Controls.Add(this.grdAirportCommission);
                this.pageAirportCharges.Controls.Add(this.radLabel36);
                this.pageAirportCharges.Location = new System.Drawing.Point(10, 37);
                this.pageAirportCharges.Name = "pageAirportCharges";
                this.pageAirportCharges.Size = new System.Drawing.Size(913, 125);
                this.pageAirportCharges.Text = "Airport Charges";
                // 
                // grdAirportCommission
                // 
                this.grdAirportCommission.AutoCellFormatting = false;
                this.grdAirportCommission.Dock = System.Windows.Forms.DockStyle.Fill;
                this.grdAirportCommission.EnableCheckInCheckOut = false;
                this.grdAirportCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdAirportCommission.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdAirportCommission.Location = new System.Drawing.Point(0, 23);
                // 
                // grdAirportCommission
                // 
                this.grdAirportCommission.MasterTemplate.AllowAddNewRow = false;
                this.grdAirportCommission.Name = "grdAirportCommission";
                this.grdAirportCommission.PKFieldColumnName = "";
                this.grdAirportCommission.ShowGroupPanel = false;
                this.grdAirportCommission.ShowImageOnActionButton = true;
                this.grdAirportCommission.Size = new System.Drawing.Size(913, 102);
                this.grdAirportCommission.TabIndex = 71;
                this.grdAirportCommission.Text = "myGridView1";
                // 
                // radLabel36
                // 
                this.radLabel36.AutoSize = false;
                this.radLabel36.BackColor = System.Drawing.Color.Crimson;
                this.radLabel36.Dock = System.Windows.Forms.DockStyle.Top;
                this.radLabel36.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel36.ForeColor = System.Drawing.Color.White;
                this.radLabel36.Location = new System.Drawing.Point(0, 0);
                this.radLabel36.Name = "radLabel36";
                // 
                // 
                // 
                this.radLabel36.RootElement.ForeColor = System.Drawing.Color.White;
                this.radLabel36.Size = new System.Drawing.Size(913, 23);
                this.radLabel36.TabIndex = 70;
                this.radLabel36.Text = "Airport Charges";
                this.pageAirportCharges.ResumeLayout(true);
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel36)).EndInit();
                //grdAirportCommission.ViewCellFormatting += new CellFormattingEventHandler(grdAirportCommission_ViewCellFormatting);
                grdAirportCommission.CellEndEdit += new GridViewCellEventHandler(grdAirportCommission_CellEndEdit);
            }
            catch 
            {
            }
        }

        void grdAirportCommission_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            try
            {
                decimal Company = e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();
                decimal Driver = e.Row.Cells[COLS_AirportCommission.DriverPrice].Value.ToDecimal();
                decimal Agent = e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value.ToDecimal();


                if (e.Column.Name == COLS_AirportCommission.CompanyPrice)
                {
                    Company = e.Value.ToDecimal();
                    Agent =Company - Driver;
                    e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;

                    e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Company - Agent;

                }
                else if (e.Column.Name == COLS_AirportCommission.CommissionAmount)
                {
                  
                    Agent = e.Value.ToDecimal();
                    Driver = Company - Agent;
                    e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Driver;                 
                    e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;

                    

                }
                else if (e.Column.Name == COLS_AirportCommission.DriverPrice)
                {
                    Driver = e.Value.ToDecimal();
                    Agent = Company - Driver;
                    e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;



                    e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;

                }


                //if(Company<0 || Driver<0)
                //e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value=(Driver+Agent);
                //e.Row.Cells[COLS_AirportCommission.DriverPrice].Value=(Company-Agent);
                //e.Row.Cells[COLS_AirportCommission.CustomerPrice].Value=(Company-Driver);
            }
            catch 
            { 
            
            }
        }

        //Station

        //private UI.MyGridView grdStationCommission;
        //private Telerik.WinControls.UI.RadLabel radLabel37;

        //private void InitializeStationCommission()
        //{

        //    if (grdStationCommission != null)
        //        return;

        //    try
        //    {
        //        this.grdStationCommission = new UI.MyGridView();
        //        this.radLabel37 = new Telerik.WinControls.UI.RadLabel();
        //        this.pageStationCharges.SuspendLayout();
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission)).BeginInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission.MasterTemplate)).BeginInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.radLabel37)).BeginInit();
        //        // 
        //        // pageStationCommission
        //        // 
        //        this.pageStationCharges.Controls.Add(this.grdStationCommission);
        //        this.pageStationCharges.Controls.Add(this.radLabel37);
        //        this.pageStationCharges.Location = new System.Drawing.Point(10, 37);
        //        this.pageStationCharges.Name = "pageStationCharges";
        //        this.pageStationCharges.Size = new System.Drawing.Size(913, 125);
        //        this.pageStationCharges.Text = "Station Charges";
        //        // 
        //        // grdStationCommission
        //        // 
        //        this.grdStationCommission.AutoCellFormatting = false;
        //        this.grdStationCommission.Dock = System.Windows.Forms.DockStyle.Fill;
        //        this.grdStationCommission.EnableCheckInCheckOut = false;
        //        this.grdStationCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
        //        this.grdStationCommission.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
        //        this.grdStationCommission.Location = new System.Drawing.Point(0, 23);
        //        // 
        //        // 
        //        // 
        //        this.grdStationCommission.MasterTemplate.AllowAddNewRow = false;
        //        this.grdStationCommission.Name = "grdStationCommission";
        //        this.grdStationCommission.PKFieldColumnName = "";
        //        this.grdStationCommission.ShowGroupPanel = false;
        //        this.grdStationCommission.ShowImageOnActionButton = true;
        //        this.grdStationCommission.Size = new System.Drawing.Size(913, 102);
        //        this.grdStationCommission.TabIndex = 71;
        //        this.grdStationCommission.Text = "myGridView1";
        //        // 
        //        // radLabel37
        //        // 
        //        this.radLabel37.AutoSize = false;
        //        this.radLabel37.BackColor = System.Drawing.Color.Crimson;
        //        this.radLabel37.Dock = System.Windows.Forms.DockStyle.Top;
        //        this.radLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //        this.radLabel37.ForeColor = System.Drawing.Color.White;
        //        this.radLabel37.Location = new System.Drawing.Point(0, 0);
        //        this.radLabel37.Name = "radLabel37";
        //        // 
        //        // 
        //        // 
        //        this.radLabel37.RootElement.ForeColor = System.Drawing.Color.White;
        //        this.radLabel37.Size = new System.Drawing.Size(913, 23);
        //        this.radLabel37.TabIndex = 70;
        //        this.radLabel37.Text = "Station Charges";
        //        this.pageStationCharges.ResumeLayout(false);
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission.MasterTemplate)).EndInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission)).EndInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.radLabel37)).EndInit();
        //        this.grdStationCommission.CellEndEdit += new GridViewCellEventHandler(grdStationCommission_CellEndEdit);
        //        //grdStationCommission.ViewCellFormatting += new CellFormattingEventHandler(grdStationCommission_ViewCellFormatting);
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        void grdStationCommission_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            try
            {
                decimal Company = e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();
                decimal Driver = e.Row.Cells[COLS_AirportCommission.DriverPrice].Value.ToDecimal();
                decimal Agent = e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value.ToDecimal();


                if (e.Column.Name == COLS_AirportCommission.CompanyPrice)
                {
                    Company = e.Value.ToDecimal();
                    Agent = Company - Driver;
                    e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;

                    e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Company - Agent;

                }
                else if (e.Column.Name == COLS_AirportCommission.CommissionAmount)
                {

                    Agent = e.Value.ToDecimal();
                    Driver = Company - Agent;
                    e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Driver;
                    e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;



                }
                else if (e.Column.Name == COLS_AirportCommission.DriverPrice)
                {
                    Driver = e.Value.ToDecimal();
                    Agent = Company - Driver;
                    e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;



                    e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;

                }


                //if(Company<0 || Driver<0)
                //e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value=(Driver+Agent);
                //e.Row.Cells[COLS_AirportCommission.DriverPrice].Value=(Company-Agent);
                //e.Row.Cells[COLS_AirportCommission.CustomerPrice].Value=(Company-Driver);
            }
            catch 
            {

            }
        }

        private void FormatAirportCommissionGrid()
        {
            if (grdAirportCommission == null)
                return;
            // grdAirportCommission.AllowEditRow = true;
            //grdAirportCommission.AddNewRowPosition = SystemRowPosition.Top;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.Id;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.FareId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.MasterId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.LocationTypeId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.LocationId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Width = 240;
            col.HeaderText = "Location";
            col.Name = COLS_AirportCommission.Location;
            col.ReadOnly = true;
            grdAirportCommission.Columns.Add(col);


            GridViewDecimalColumn dcol = new GridViewDecimalColumn();

            dcol.Name = COLS_AirportCommission.CompanyPrice;
            dcol.HeaderText = "Company Price";
            dcol.Width = 130;
            //dcol.ReadOnly = false;
            //dcol.Expression = "DriverPrice-CustomerPrice";
            dcol.DecimalPlaces = 2;
            dcol.Maximum = 1000000;
            dcol.ReadOnly = false;
            grdAirportCommission.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_AirportCommission.DriverPrice;
            dcol.HeaderText = "Driver Price";
            dcol.Width = 130;
           // dcol.Expression = "CompanyPrice-CustomerPrice";
            dcol.DecimalPlaces = 2;
            //dcol.ReadOnly = false;
            dcol.Maximum = 1000000;
            dcol.ReadOnly = false;
            grdAirportCommission.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_AirportCommission.CommissionAmount;
            dcol.HeaderText = "Agent Commission";
            dcol.Width = 140;
            dcol.DecimalPlaces = 2;
            //dcol.ReadOnly = false;
            dcol.Maximum = 1000000;
           // dcol.Expression = "CompanyPrice-DriverPrice";
            dcol.ReadOnly = false;
            grdAirportCommission.Columns.Add(dcol);




            var Airportlist = (from a in General.GetQueryable<Gen_Location>(c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT)
                               select new
                               {
                                   Id = a.Id,
                                   LocationName = a.LocationName
                               }).ToList();
            grdAirportCommission.RowCount = Airportlist.Count;

            grdAirportCommission.BeginUpdate();
            for (int i = 0; i < grdAirportCommission.RowCount; i++)
            {
                grdAirportCommission.Rows[i].Cells[COLS_AirportCommission.LocationId].Value = Airportlist[i].Id;
                grdAirportCommission.Rows[i].Cells[COLS_AirportCommission.Location].Value = Airportlist[i].LocationName;
            }
            grdAirportCommission.EndUpdate();


            grdAirportCommission.ReadOnly = false;
            grdAirportCommission.AllowEditRow = true;


           // grdAirportCommission.CellEndEdit+=new GridViewCellEventHandler(grdAirportCommission_CellEndEdit);

        }
        private void FormatStationCommissionGrid()
        {
           // if (grdStationCommission == null)
           //     return;
           
           // grdStationCommission.ReadOnly = false;
           // grdStationCommission.AllowEditRow = true;
           // GridViewTextBoxColumn col = new GridViewTextBoxColumn();
           // col.Name = COLS_StationCommission.Id;
           // col.IsVisible = false;
           // grdStationCommission.Columns.Add(col);
           // col = new GridViewTextBoxColumn();
           // col.Name = COLS_StationCommission.FareId;
           // col.IsVisible = false;
           // grdStationCommission.Columns.Add(col);
           //// grdStationCommission.Columns.Add(col);
           // col = new GridViewTextBoxColumn();
           // col.Name = COLS_StationCommission.MasterId;
           // col.IsVisible = false;
           // grdStationCommission.Columns.Add(col);
           // col = new GridViewTextBoxColumn();
           // col.IsVisible = false;
           // col.Name = COLS_StationCommission.LocationTypeId;

           // grdStationCommission.Columns.Add(col);
           // col = new GridViewTextBoxColumn();
           // col.IsVisible = false;
           // col.Name = COLS_StationCommission.LocationId;

           // grdStationCommission.Columns.Add(col);
           // col = new GridViewTextBoxColumn();
           // col.Width = 240;
           // col.ReadOnly = true;
           // col.HeaderText = "Location";
           // col.Name = COLS_StationCommission.Location;
           // grdStationCommission.Columns.Add(col);
           // GridViewDecimalColumn dcol = new GridViewDecimalColumn();

           // dcol.Name = COLS_StationCommission.CompanyPrice;
           // dcol.HeaderText = "Company Price";
           // dcol.Width = 130;
           //// dcol.Expression = "DriverPrice-CustomerPrice";
           // dcol.DecimalPlaces = 2;
           // dcol.Maximum = 1000000;
           // dcol.ReadOnly = false;
           // grdStationCommission.Columns.Add(dcol);


           // dcol = new GridViewDecimalColumn();
           // dcol.Name = COLS_StationCommission.DriverPrice;
           // dcol.HeaderText = "Driver Price";
           // dcol.Width = 130;
           // //dcol.Expression = "CompanyPrice-CustomerPrice";
           // dcol.DecimalPlaces = 2;
           // dcol.Maximum = 1000000;
           // dcol.ReadOnly = false;
           // grdStationCommission.Columns.Add(dcol);

           // dcol = new GridViewDecimalColumn();
           // dcol.Name = COLS_StationCommission.CommissionAmount;
           // dcol.HeaderText = "Agent Commission";
           // dcol.Width = 140;
           // dcol.DecimalPlaces = 2;
           // dcol.Maximum = 1000000;
           // dcol.ReadOnly = false;
           //// dcol.Expression = "CompanyPrice-DriverPrice";
           // grdStationCommission.Columns.Add(dcol);

           // var Stationlist = (from a in General.GetQueryable<Gen_Location>(c => c.LocationTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
           //                    select new
           //                    {
           //                        Id = a.Id,
           //                        LocationName = a.LocationName
           //                    }).ToList();
           // grdStationCommission.RowCount = Stationlist.Count;

           // grdStationCommission.BeginUpdate();
           // for (int i = 0; i < grdStationCommission.RowCount; i++)
           // {
           //     grdStationCommission.Rows[i].Cells[COLS_StationCommission.LocationId].Value = Stationlist[i].Id;
           //     grdStationCommission.Rows[i].Cells[COLS_StationCommission.Location].Value = Stationlist[i].LocationName;
           // }

           // grdStationCommission.EndUpdate();
        }


        //
        public void FormatPlotWiseGrid()
        {
            grdPlotWiseFare.AllowAddNewRow = false;
            //   grdDetails.AllowEditRow = false;
            grdPlotWiseFare.AutoCellFormatting = true;
            grdPlotWiseFare.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.Id;
            col.IsVisible = false;
            grdPlotWiseFare.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.FareId;
            col.IsVisible = false;
            grdPlotWiseFare.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.FromZoneId;
            col.IsVisible = false;
            grdPlotWiseFare.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.ToZoneId;
            col.IsVisible = false;
            grdPlotWiseFare.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.FromPlotNo;
            col.HeaderText = "From Plot No";
            col.Width = 100;
            col.ReadOnly = true;
            grdPlotWiseFare.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.FromPlot;
            col.HeaderText = "From Plot";
            col.Width = 200;
            col.ReadOnly = true;
            grdPlotWiseFare.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.ToPlotNo;
            col.HeaderText = "To Plot No";
            col.Width = 100;
            col.ReadOnly = true;
            grdPlotWiseFare.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_PLOTEWISE.ToPlot;
            col.HeaderText = "To Plot";
            col.Width = 200;
            col.ReadOnly = true;
            grdPlotWiseFare.Columns.Add(col);

            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_PLOTEWISE.Price;
            dcol.HeaderText = "Price (£)";
            dcol.Width = 80;
            dcol.ReadOnly = true;
            grdPlotWiseFare.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_PLOTEWISE.COMPANYFARE;
            dcol.HeaderText = "Company Price (£)";
            dcol.Width = 140;
            dcol.IsVisible = false;
            dcol.ReadOnly = true;
            grdPlotWiseFare.Columns.Add(dcol);

            grdPlotWiseFare.MasterTemplate.ShowRowHeaderColumn = false;



            UI.GridFunctions.AddDeleteColumn(grdPlotWiseFare);
            grdPlotWiseFare.Columns["ColDelete"].Width = 80;

            UI.GridFunctions.SetFilter(grdPlotWiseFare);

            grdPlotWiseFare.RowsChanged += new GridViewCollectionChangedEventHandler(grdPlotWiseFare_RowsChanged);

        }

        void grdPlotWiseFare_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                ClearPlotDetails();
            }
        }
        public frmFares(int CompanyId, int ShowTabIndex)
        {
            InitializeComponent();
            InitializeConstructors();
            ddlCompany.SelectedValue = CompanyId;
            chkCompanyWise.Checked = true;
            chkCompanyWise.Enabled = false;
            ddlCompany.Enabled = false;

            //
            //rbtnAdd.Visible = false;
            //rbtnSubtract.Visible = false;
            //lblPercent.Visible = false;
            //numPercent.Visible = false;
            //btnApply.Visible = false;
            if (ShowTabIndex == 1)
            {
                //pg_FixFareList.Show();
                ((Telerik.WinControls.UI.RadPageView)pg_FixFareList.Parent).SelectedPage = pg_FixFareList;
            }
            else
            {
                //radPageViewPage2.Show();
                ((Telerik.WinControls.UI.RadPageView)radPageViewPage2.Parent).SelectedPage = radPageViewPage2;
                //     myRadPanelBarGroupElement.Expanded = true;
            }
        }
        public void InitializeConstructors()
        {
            this.Load += new EventHandler(frmFares_Load);
            objMaster = new FareBO();
            this.SetProperties((INavigation)objMaster);

            if (AppVars.objPolicyConfiguration != null)
            {

                HasOffPeakRate = AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool();
            }


            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool())
            {
                pg_PDAMeter.Item.Visibility = ElementVisibility.Visible;

            }
            else
            {
                pg_PDAMeter.Item.Visibility = ElementVisibility.Collapsed;


            }










            //if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool())
            //{
            //    this.pg_FixFareList.Item.Visibility = ElementVisibility.Collapsed;
            //}

            if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool() == false)
            {
                this.pg_PlotWiseFare.Item.Visibility = ElementVisibility.Collapsed;
            }
            else
            {
                radPageView1.SelectedPage = pg_PlotWiseFare;
                this.pg_FixFareList.Item.Visibility = ElementVisibility.Visible;

            }


            FormatFaresDetailGrid();
            FormatFaresOtherDetailGrid();
            FormatFaresPDAOtherDetailGrid();
            FormatPlotWiseGrid();
            FillCombos();

            OnNew();
            //   FillPlotCombo();

            grdPlotWiseFare.CellClick += new GridViewCellEventHandler(grdPlotWiseFare_CellDoubleClick);



            this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            //    txtFromAddress.TextBoxElement.KeyDown += new KeyEventHandler(TextBoxFromAddressElement_KeyDown);

            txtFromAddress.ListBoxElement.Width = 550;
            txtFromAddress.ListBoxElement.Height = 400;
            txtToAddress.ListBoxElement.Width = 400;
            txtFromAddress.KeyPress += new KeyPressEventHandler(txtAddress_KeyPress);
            txtToAddress.KeyPress += new KeyPressEventHandler(txtAddress_KeyPress);
            Font font = new Font("Tahoma", 9, FontStyle.Bold);
            txtFromAddress.ListBoxElement.Font = font;
            txtToAddress.ListBoxElement.Font = font;

            txtFromAddress.ListBoxElement.ItemHeight = 30;
            txtToAddress.ListBoxElement.ItemHeight = 30;
            this.btnApply.Click += new EventHandler(btnApply_Click);
            


      
            //Hyde Park
            if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "SHOW AIRPORT FARES") > 0)
            {
                InitializeAirportCommission();
                this.pageAirportCharges.Item.Visibility = ElementVisibility.Collapsed;  
                FormatAirportCommissionGrid();
            }
          
         //   InitializeStationCommission();
        //    FormatStationCommissionGrid();
            //

            this.FormClosing += new FormClosingEventHandler(frmFares_FormClosing);
        }

        void frmFares_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeObjects();

        }

        private void DisposeObjects()
        {
            try
            {
                if (timer1 != null)
                {
                    timer1.Stop();
                    timer1.Dispose();
                    timer1 = null;

                    if (POIWorker != null)
                    {
                        if (POIWorker.IsBusy)
                        {

                            POIWorker.CancelAsync();
                        }


                        POIWorker.Dispose();
                        POIWorker = null;
                        GC.Collect();

                    }
                }
            }
            catch
            {

            }

        }


        void btnApply_Click(object sender, EventArgs e)
        {
            ApplyChangesOnFixedFares();
            ApplyZoneWiseFares();
        }


        private void ApplyZoneWiseFares()
        {
            if (grdPlotWiseFare == null)
                return;

            if (rbtnAdd.IsChecked)
            {
                grdPlotWiseFare.Rows.ToList().ForEach(c=>c.Cells[COLS_PLOTEWISE.Price].Value=(c.Cells[COLS_PLOTEWISE.Price].Value.ToDecimal()+ (c.Cells[COLS_PLOTEWISE.Price].Value.ToDecimal()*numPercent.Value.ToDecimal())/100));
            }
            else if(rbtnSubtract.IsChecked)
            {
                     grdPlotWiseFare.Rows.ToList().ForEach(c=>c.Cells[COLS_PLOTEWISE.Price].Value=(c.Cells[COLS_PLOTEWISE.Price].Value.ToDecimal()- (c.Cells[COLS_PLOTEWISE.Price].Value.ToDecimal()*numPercent.Value.ToDecimal())/100));


            }


        }

        private void ApplyChangesOnFixedFares()
        {
            try
            {
                decimal percent = numPercent.Value.ToDecimal();

                if (grdDetails.RowCount > 0)
                { //COLS_DETAILS



                    if (chkApplyToAll.Checked.ToBool())
                    {

                        if (rbtnAdd.IsChecked)
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {

                                decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() + Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);

                                //decimal CompanyAmount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                //grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() + CompanyAmount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {
                                decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() - Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);

                                //decimal CompanyAmount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                //grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() - CompanyAmount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                            }
                        }
                    }
                    else
                    {
                        if (rbtnAdd.IsChecked)
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {
                                if ( (grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                                    ||
                                    (grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION))

                                {

                                    continue;
                                }
                                else
                                {

                                    decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                    grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() + Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);

                                    //decimal CompanyAmount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                    //grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() + CompanyAmount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {
                                if ( (grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                                ||
                                    (grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION))

                                {

                                    continue;
                                }
                                else
                                {
                                    //decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                    //grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() - Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);

                                    //decimal CompanyAmount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                    //grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal() - CompanyAmount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                }
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public frmFares()
        {
            InitializeComponent();
            InitializeConstructors();






        }

        void grdPlotWiseFare_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdPlotWiseFare.CurrentRow != null && grdPlotWiseFare.CurrentRow is GridViewRowInfo)
                {
                    btnAddFromPlot.Enabled = false;
                    btnAddToPlot.Enabled = false;

                    lstFromPlot.Enabled = false;
                    lstToPlot.Enabled = false;

                    ddlFromPlot.SelectedValue = grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.FromZoneId].Value.ToInt();
                    ddlToPlot.SelectedValue = grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.ToZoneId].Value.ToInt();
                    spnPlotWiseFare.Value = grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.Price].Value.ToDecimal();
                    spnPlotWiseCompanyFare.Value = grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.COMPANYFARE].Value.ToDecimal();
                //spnPlotWiseCompanyFare
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void frmFares_Load(object sender, EventArgs e)
        {
            //if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "DISABLE FARE LIST") == 1)
            //{
            //    this.pg_FixFareList.Item.Visibility = ElementVisibility.Collapsed;
            //}

            //if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "DISABLE PLOT WISE FARE LIST") == 1)
            //{
            //    this.pg_PlotWiseFare.Item.Visibility = ElementVisibility.Collapsed;
            //}
        }





        private void FillCombos()
        {
            ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
            ComboFunctions.FillLocationTypeCombo(ddlToLocType);

            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);
            ComboFunctions.FillCompanyCombo(ddlCompany);

            if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool())
            {
                FillPlotCombo();
            }


            ComboFunctions.FillSubCompanyCombo(ddlSubCompanyId);

            if (ddlSubCompanyId.Items.Count == 0)
                ddlSubCompanyId.SelectedIndex = 0;


        }

        private void FillPlotCombo()
        {
            ComboFunctions.FillPlotWiseFare(ddlFromPlot);
            ComboFunctions.FillPlotWiseFare(ddlToPlot);
        }

        private void FormatFaresDetailGrid()
        {
            grdDetails.AllowAddNewRow = false;
            //   grdDetails.AllowEditRow = false;
            grdDetails.AutoCellFormatting = true;
            grdDetails.ShowGroupPanel = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.ID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.FAREID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.FROMLOCTYPEID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.TOLOCTYPEID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.FROMLOCATIONID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.TOLOCATIONID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "From Location";
            col.Name = COLS_DETAILS.FROMLOCATION;
            col.Width = 170;
            col.ReadOnly = true;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "To Location";
            col.ReadOnly = true;
            col.Name = COLS_DETAILS.TOLOCATION;
            col.Width = 200;
            grdDetails.Columns.Add(col);



            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Fare (£)";
            colDec.Width = 110;
            colDec.ReadOnly = false;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_DETAILS.FARE;
            grdDetails.Columns.Add(colDec);



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Company Fare (£)";
            colDec.Width = 150;
            colDec.ReadOnly = false;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_DETAILS.COMPANYFARE;
            grdDetails.Columns.Add(colDec);

            grdDetails.MasterTemplate.ShowRowHeaderColumn = false;


            UI.GridFunctions.AddDeleteColumn(grdDetails);
            grdDetails.Columns["ColDelete"].Width = 80;
            UI.GridFunctions.SetFilter(grdDetails);

            colDec.ReadOnly = false;
            grdDetails.AllowEditRow = true;
        }





        private void FormatFaresOtherDetailGrid()
        {
            try
            {
                grdOtherCharges.AllowAddNewRow = false;
                // grdOtherCharges.AllowEditRow = false;
                grdOtherCharges.AutoCellFormatting = true;
                grdOtherCharges.ShowGroupPanel = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.Name = COLS_DETAILS.ID;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_DETAILS.FAREID;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);



                GridViewDecimalColumn colDec = null;


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "From Mile";
                colDec.Name = COLS_OTHERDETAILS.FROMMILE;
                colDec.Width = 200;
                colDec.DecimalPlaces = 2;
                colDec.ReadOnly = true;
                grdOtherCharges.Columns.Add(colDec);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "To Mile";
                colDec.Name = COLS_OTHERDETAILS.TOMILE;
                colDec.Width = 200;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                grdOtherCharges.Columns.Add(colDec);



                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Rate (£)";
                colDec.Width = 80;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.FARE;
                grdOtherCharges.Columns.Add(colDec);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Company Rate (£)";
                colDec.Width = 140;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.IsVisible = false;
                colDec.Name = COLS_OTHERDETAILS.COMPANYFARE;
                grdOtherCharges.Columns.Add(colDec);



                if (HasOffPeakRate)
                {

                    grdOtherCharges.Columns[COLS_OTHERDETAILS.FROMMILE].Width = 85;
                    grdOtherCharges.Columns[COLS_OTHERDETAILS.TOMILE].Width = 85;
                    grdOtherCharges.Columns[COLS_OTHERDETAILS.FARE].Width = 100;


                    pnlOffPeak.Enabled = true;


                    GridViewTextBoxColumn colTxt = new GridViewTextBoxColumn();
                    colTxt.HeaderText = "Peak Time";
                    colTxt.Name = COLS_OTHERDETAILS.PEAKTIME;
                    colTxt.Width = 110;
                    colTxt.ReadOnly = true;
                    grdOtherCharges.Columns.Add(colTxt);


                    colDec = new GridViewDecimalColumn();
                    colDec.HeaderText = "Peak Rate (£)";
                    colDec.Width = 120;
                    colDec.ReadOnly = false;
                    colDec.DecimalPlaces = 2;
                    colDec.ThousandsSeparator = true;
                    colDec.Name = COLS_OTHERDETAILS.PEAKRATE;
                    grdOtherCharges.Columns.Add(colDec);



                    colTxt = new GridViewTextBoxColumn();
                    colTxt.HeaderText = "Off Peak Time";
                    colTxt.Name = COLS_OTHERDETAILS.OFFPEAKTIME;
                    colTxt.Width = 110;
                    colTxt.ReadOnly = true;
                    grdOtherCharges.Columns.Add(colTxt);


                    colDec = new GridViewDecimalColumn();
                    colDec.HeaderText = "Off Peak Rate (£)";
                    colDec.Width = 120;
                    colDec.ReadOnly = false;
                    colDec.DecimalPlaces = 2;
                    colDec.ThousandsSeparator = true;
                    colDec.Name = COLS_OTHERDETAILS.OFFPEAKRATE;
                    grdOtherCharges.Columns.Add(colDec);




                    GridViewDateTimeColumn colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.FROMSTARTTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.TILLSTARTTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.FROMENDTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.TILLENDTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdOtherCharges.Columns.Add(colDtp);

                }




                grdOtherCharges.MasterTemplate.ShowRowHeaderColumn = false;


                UI.GridFunctions.AddDeleteColumn(grdOtherCharges);
                grdOtherCharges.Columns["ColDelete"].Width = 80;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }




        private void FormatFaresPDAOtherDetailGrid()
        {
            try
            {
                grdPDAOtherCharges.AllowAddNewRow = false;
                // grdOtherCharges.AllowEditRow = false;
                grdPDAOtherCharges.AutoCellFormatting = true;
                grdPDAOtherCharges.ShowGroupPanel = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.Name = COLS_DETAILS.ID;
                col.IsVisible = false;
                grdPDAOtherCharges.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_DETAILS.FAREID;
                col.IsVisible = false;
                grdPDAOtherCharges.Columns.Add(col);



                GridViewDecimalColumn colDec = null;


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "From Mile";
                colDec.Name = COLS_OTHERDETAILS.FROMMILE;
                colDec.Width = 200;
                colDec.DecimalPlaces = 2;
                colDec.ReadOnly = true;
                grdPDAOtherCharges.Columns.Add(colDec);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "To Mile";
                colDec.Name = COLS_OTHERDETAILS.TOMILE;
                colDec.Width = 200;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                grdPDAOtherCharges.Columns.Add(colDec);



                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Rate (£)";
                colDec.Width = 80;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.FARE;
                grdPDAOtherCharges.Columns.Add(colDec);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Company Rate (£)";
                colDec.Width = 140;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.IsVisible = false;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.COMPANYFARE;
                grdPDAOtherCharges.Columns.Add(colDec);


                if (HasOffPeakRate)
                {

                    grdPDAOtherCharges.Columns[COLS_OTHERDETAILS.FROMMILE].Width = 85;
                    grdPDAOtherCharges.Columns[COLS_OTHERDETAILS.TOMILE].Width = 85;
                    grdPDAOtherCharges.Columns[COLS_OTHERDETAILS.FARE].Width = 100;


                    pnlpdaoffpeak.Enabled = true;


                    GridViewTextBoxColumn colTxt = new GridViewTextBoxColumn();
                    colTxt.HeaderText = "Peak Time";
                    colTxt.Name = COLS_OTHERDETAILS.PEAKTIME;
                    colTxt.Width = 110;
                    colTxt.ReadOnly = true;
                    grdPDAOtherCharges.Columns.Add(colTxt);


                    colDec = new GridViewDecimalColumn();
                    colDec.HeaderText = "Peak Rate (£)";
                    colDec.Width = 120;
                    colDec.ReadOnly = false;
                    colDec.DecimalPlaces = 2;
                    colDec.ThousandsSeparator = true;
                    colDec.Name = COLS_OTHERDETAILS.PEAKRATE;
                    grdPDAOtherCharges.Columns.Add(colDec);



                    colTxt = new GridViewTextBoxColumn();
                    colTxt.HeaderText = "Off Peak Time";
                    colTxt.Name = COLS_OTHERDETAILS.OFFPEAKTIME;
                    colTxt.Width = 110;
                    colTxt.ReadOnly = true;
                    grdPDAOtherCharges.Columns.Add(colTxt);


                    colDec = new GridViewDecimalColumn();
                    colDec.HeaderText = "Off Peak Rate (£)";
                    colDec.Width = 120;
                    colDec.ReadOnly = false;
                    colDec.DecimalPlaces = 2;
                    colDec.ThousandsSeparator = true;
                    colDec.Name = COLS_OTHERDETAILS.OFFPEAKRATE;
                    grdPDAOtherCharges.Columns.Add(colDec);




                    GridViewDateTimeColumn colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.FROMSTARTTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.TILLSTARTTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.FROMENDTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);


                    colDtp = new GridViewDateTimeColumn();
                    colDtp.Name = COLS_OTHERDETAILS.TILLENDTIME;
                    colDtp.IsVisible = false;
                    colDtp.CustomFormat = "hh:tt";
                    colDtp.FormatString = "hh:tt";
                    grdPDAOtherCharges.Columns.Add(colDtp);

                }




                grdPDAOtherCharges.MasterTemplate.ShowRowHeaderColumn = false;

    

                UI.GridFunctions.AddDeleteColumn(grdPDAOtherCharges);
                grdPDAOtherCharges.Columns["ColDelete"].Width = 80;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        #region Overridden Methods


        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
            dtpEffectiveDate.Value = DateTime.Now;


            grdDetails.Rows.Clear();
            ddlCompany.SelectedValue = null;

            chkCompanyWise.Checked = false;
            CompanyWise(ToggleState.Off);
            ClearFareDetails();
            ClearOtherChargesDetails();

            FillFromLocations();
            FillToLocations();


            //dtpEffectiveDate.Focus();
            ddlVehicleType.SelectedValue = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;
            ddlVehicleType.Focus();


        }


        private void ClearFromLocation()
        {
            ddlFromLocation.SelectedValue = null;

        }


        private void ClearToLocation()
        {
            ddlToLocation.SelectedValue = null;


        }

        private void ClearFareDetails()
        {
            ClearFromLocation();
            ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.POSTCODE;

            ClearToLocation();
            ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.POSTCODE;
            numRate_FareCharges.Value = 0;
            numCompanyFares.Value = 0;
            ddlFromLocation.Focus();
            grdDetails.CurrentRow = null;



            ddlFromLocation.Tag = null;
            ddlToLocation.Tag = null;

            lstFromLocation.Items.Clear();
            lstToLocation.Items.Clear();

            txtFromAddress.Text = string.Empty;
            txtToAddress.Text = string.Empty;
        }

        public override void Save()
        {
            try
            {
                //  Fare objOldFares = null;

                //if(IsOpenFromCopyFares)
                //{
                 
                //    int? companyId=ddlCompany.SelectedValue.ToIntorNull();
                //    int? vehicleId=ddlVehicleType.SelectedValue.ToIntorNull();

                //    if (companyId != null)
                //    {

                //        objOldFares = General.GetObject<Fare>(c => c.VehicleTypeId ==vehicleId && c.CompanyId == companyId);
                //    }
                //    else
                //    {
                //        objOldFares = General.GetObject<Fare>(c => c.VehicleTypeId ==vehicleId);
                //    }

                //}

                if (objMaster.PrimaryKeyValue == null)
                {
                    //if(IsOpenFromCopyFares && objOldFares!=null)
                    //{
                    //    objMaster.GetByPrimaryKey(objOldFares.Id);
                    //    objMaster.Edit();

                    //}
                    //else
                    //{

                        objMaster.New();
                  //  }
                }
                else
                {
                    objMaster.Edit();
                }

                //  objMaster.Current.EffectiveDate = dtpEffectiveDate.Value.ToDateorNull();
                objMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                objMaster.Current.IsCompanyWise = chkCompanyWise.Checked;
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();


                objMaster.Current.SubCompanyId = ddlSubCompanyId.SelectedValue.ToIntorNull();
                //   Fare_ChargesDetail c = new Fare_ChargesDetail();

                objMaster.Current.StartRate = numStartRate.Value;
                objMaster.Current.StartRateValidMiles = numStartRateValidMiles.Value;

                if (objMaster.Current.CompanyId != null)
                {

                    if (grdAirportCommission != null)
                    {

                        foreach (var item in grdAirportCommission.Rows.Where(c => c.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal() > 0))
                        {
                            var fixfaresRows = grdDetails.Rows.Where(c => c.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToInt() == item.Cells[COLS_AirportCommission.LocationId].Value.ToInt());

                            foreach (var item2 in fixfaresRows)
                            {
                                item2.Cells[COLS_DETAILS.FARE].Value = item.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();
                            }


                            fixfaresRows = grdDetails.Rows.Where(c => c.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToInt() == item.Cells[COLS_AirportCommission.LocationId].Value.ToInt());

                            foreach (var item2 in fixfaresRows)
                            {
                                item2.Cells[COLS_DETAILS.FARE].Value = item.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();
                            }
                        }

                    }
                }
              





                string[] skipProperties = { "Gen_Location", "Gen_Location1","Gen_LocationType",
                                            "Gen_LocationType1","Fare","Gen_Zone1","Gen_Zone","Fare_ZoneWisePricing1","Fare_ZoneWisePricing"};



                //if (IsOpenFromCopyFares)
                //{
                //    if (objOldFares != null)
                //    {

                //        List<Fare_ChargesDetail> listofOldChargesDetails = objOldFares.Fare_ChargesDetails.Where(c => (c.OriginLocationTypeId == Enums.LOCATION_TYPES.AIRPORT || c.OriginLocationTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                //                                                                                                  || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.AIRPORT || c.DestinationLocationTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)).ToList();



                //        grdDetails.Rows.ToList().RemoveAll(c => (c.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT || c.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                //                                         || (c.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT || c.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION));
 

                            
                //        grdDetails.BeginUpdate();
                //        GridViewRowInfo row=null;
                //        for (int i = 0; i < listofOldChargesDetails.Count; i++)
                //        {
                //            row=grdDetails.Rows.AddNew();

                      
                //            row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value = listofOldChargesDetails[i].OriginLocationTypeId;
                //            row.Cells[COLS_DETAILS.TOLOCTYPEID].Value = listofOldChargesDetails[i].DestinationLocationTypeId;

                //            row.Cells[COLS_DETAILS.FROMLOCATIONID].Value = listofOldChargesDetails[i].OriginId;

                //            row.Cells[COLS_DETAILS.TOLOCATIONID].Value = listofOldChargesDetails[i].DestinationId;



                //            if (listofOldChargesDetails[i].OriginLocationTypeId.ToInt() == 100)
                //            {
                //                row.Cells[COLS_DETAILS.FROMLOCATIONID].Value = listofOldChargesDetails[i].FromZoneId;

                //                row.Cells[COLS_DETAILS.FROMLOCATION].Value = listofOldChargesDetails[i].Gen_Zone.DefaultIfEmpty().ZoneName;
                //            }
                //            else
                //            {

                //                row.Cells[COLS_DETAILS.FROMLOCATION].Value = listofOldChargesDetails[i].FromAddress.ToStr();

                             
                //            }


                //            if (listofOldChargesDetails[i].DestinationLocationTypeId.ToInt() == 100)
                //            {
                //                row.Cells[COLS_DETAILS.TOLOCATIONID].Value = listofOldChargesDetails[i].ToZoneId;


                //                row.Cells[COLS_DETAILS.TOLOCATION].Value = listofOldChargesDetails[i].Gen_Zone1.DefaultIfEmpty().ZoneName;

                //            }
                //            else
                //            {
                //                row.Cells[COLS_DETAILS.TOLOCATION].Value = listofOldChargesDetails[i].ToAddress.ToStr();                         
                //            }

                //            row.Cells[COLS_DETAILS.FARE].Value = listofOldChargesDetails[i].Rate;
                //        }

                //        grdDetails.EndUpdate(); 

                //        objMaster.Current.Fare_ChargesDetails.Clear();
                //    }

                //}

                //else
                //{

                    IList<Fare_ChargesDetail> savedList = objMaster.Current.Fare_ChargesDetails;
                    List<Fare_ChargesDetail> listofDetail = (from r in grdDetails.Rows
                                                             select new Fare_ChargesDetail
                                                             {
                                                                 Id = r.Cells[COLS_DETAILS.ID].Value.ToLong(),
                                                                 FareId = r.Cells[COLS_DETAILS.FAREID].Value.ToInt(),
                                                                 OriginLocationTypeId = r.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToIntorNull(),
                                                                 DestinationLocationTypeId = r.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToIntorNull(),
                                                                 OriginId = r.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() != 100 ? r.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToIntorNull() : null,
                                                                 DestinationId = r.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() != 100 ? r.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToIntorNull() : null,
                                                                 FromZoneId = r.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == 100 ? r.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToIntorNull() : null,
                                                                 ToZoneId = r.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == 100 ? r.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToIntorNull() : null,
                                                                 //  FromZoneId=null,
                                                                 //  ToZoneId=null,
                                                                 FromAddress = r.Cells[COLS_DETAILS.FROMLOCATION].Value.ToStr().Trim(),
                                                                 ToAddress = r.Cells[COLS_DETAILS.TOLOCATION].Value.ToStr().Trim(),
                                                                 Rate = r.Cells[COLS_DETAILS.FARE].Value.ToDecimal(),
                                                                  CompanyRate = r.Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal()

                                                             }).ToList();


                    Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

              //  }


                    DateTime dateValue = new DateTime(1900, 1, 1, 0, 0, 0);

                    //DateTime? fromPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() +dtpFromStartTime.Value.Value.TimeOfDay).ToDateTime();
                    //DateTime? tillPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpTillStartTime.Value.Value.TimeOfDay).ToDateTime();
                    //DateTime? fromOffPeakFromEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpFromEndTime.Value.Value.TimeOfDay).ToDateTime();
                    //DateTime? tillOffPeakToEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpTillEndTime.Value.Value.TimeOfDay).ToDateTime();


                IList<Fare_OtherCharge> savedList2 = objMaster.Current.Fare_OtherCharges;
                List<Fare_OtherCharge> listofOtherDetail = (from r in grdOtherCharges.Rows
                                                            select new Fare_OtherCharge
                                                              {
                                                                  Id = r.Cells[COLS_OTHERDETAILS.ID].Value.ToLong(),
                                                                  FareId = r.Cells[COLS_OTHERDETAILS.FAREID].Value.ToInt(),
                                                                  FromMile = r.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal(),
                                                                  ToMile = r.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal(),
                                                                  Rate = r.Cells[COLS_OTHERDETAILS.FARE].Value.ToDecimal(),
                                                                  CompanyRate = r.Cells[COLS_OTHERDETAILS.COMPANYFARE].Value.ToDecimal(),

                                                                  //FromStartTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTimeorNull() : null,
                                                                  //TillStartTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTimeorNull() : null,
                                                                  //FromEndTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTimeorNull() : null,
                                                                  //TillEndTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTimeorNull() : null,

                                                                  FromStartTime =HasOffPeakRate?( string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime()) :dateValue,
                                                                  TillStartTime =HasOffPeakRate? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,

                                                                  FromEndTime =HasOffPeakRate? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,
                                                                  TillEndTime =HasOffPeakRate? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,





                                                                  PeakTimeRate = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.PEAKRATE].Value.ToDecimal() : 0,
                                                                  OffPeakTimeRate = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value.ToDecimal() : 0,
                                                              }).ToList();


                Utils.General.SyncChildCollection(ref savedList2, ref listofOtherDetail, "Id", skipProperties);



                // fromPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPDAFromStartTime.Value.Value.TimeOfDay).ToDateTime();
                //tillPeakTimeStartTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPDATillStartTime.Value.Value.TimeOfDay).ToDateTime();
                // fromOffPeakFromEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPDAFromEndTime.Value.Value.TimeOfDay).ToDateTime();
                // tillOffPeakToEndTime = string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + dtpPDATillEndTime.Value.Value.TimeOfDay).ToDateTime();



                // PDA METER

                IList<Fare_PDAMeter> savedList3 = objMaster.Current.Fare_PDAMeters;
                List<Fare_PDAMeter> listofpdaOtherDetail = (from r in grdPDAOtherCharges.Rows
                                                            select new Fare_PDAMeter
                                                         {
                                                             Id = r.Cells[COLS_OTHERDETAILS.ID].Value.ToLong(),
                                                             FareId = r.Cells[COLS_OTHERDETAILS.FAREID].Value.ToInt(),
                                                             FromMile = r.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal(),
                                                             ToMile = r.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal(),
                                                             Rate = r.Cells[COLS_OTHERDETAILS.FARE].Value.ToDecimal(),
                                                             CompanyRate = r.Cells[COLS_OTHERDETAILS.COMPANYFARE].Value.ToDecimal(),

                                                             //FromStartTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTimeorNull() : null,
                                                             //TillStartTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTimeorNull() : null,
                                                             //FromEndTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTimeorNull() : null,
                                                             //TillEndTime = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTimeorNull() : null,
                                                             FromStartTime =HasOffPeakRate? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,
                                                             TillStartTime =HasOffPeakRate? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,

                                                             FromEndTime =HasOffPeakRate? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,
                                                             TillEndTime =HasOffPeakRate? string.Format("{0:dd/MM/yyyy HH:mm}", dateValue.ToDate() + r.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTimeorNull().Value.TimeOfDay).ToDateTime():dateValue,


                                                             PeakTimeRate = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.PEAKRATE].Value.ToDecimal() : 0,
                                                             OffPeakTimeRate = HasOffPeakRate ? r.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value.ToDecimal() : 0,
                                                         }).ToList();


                Utils.General.SyncChildCollection(ref savedList3, ref listofpdaOtherDetail, "Id", skipProperties);



                IList<Fare_ZoneWisePricing> saveList4 = objMaster.Current.Fare_ZoneWisePricings;
                List<Fare_ZoneWisePricing> listofDetail4 = (from a in grdPlotWiseFare.Rows
                                                            select new Fare_ZoneWisePricing
                                                            {
                                                                Id = a.Cells[COLS_PLOTEWISE.Id].Value.ToLong(),
                                                                FareId = a.Cells[COLS_PLOTEWISE.FareId].Value.ToInt(),
                                                                FromZoneId = a.Cells[COLS_PLOTEWISE.FromZoneId].Value.ToInt(),
                                                                ToZoneId = a.Cells[COLS_PLOTEWISE.ToZoneId].Value.ToInt(),
                                                                Price = a.Cells[COLS_PLOTEWISE.Price].Value.ToDecimal(),
                                                                CompanyRate=a.Cells[COLS_PLOTEWISE.COMPANYFARE].Value.ToDecimal()
                                                            }).ToList();

                Utils.General.SyncChildCollection(ref saveList4, ref listofDetail4, "Id", skipProperties);


                // Hyde Park
                if (grdAirportCommission != null )
                {


                    string[] skipCommissionProperties = { "Fare", "Gen_Company", "Gen_Location", "Fleet_VehicleType" };


                    IList<Gen_Company_AgentCommission> savedListCommission = objMaster.Current.Gen_Company_AgentCommissions;
                    List<Gen_Company_AgentCommission> listofDetailCommission = (from r in grdAirportCommission.Rows
                                                                                select new Gen_Company_AgentCommission
                                                                                {
                                                                                    Id = r.Cells[COLS_AirportCommission.Id].Value.ToInt(),
                                                                                    FareId = objMaster.Current.Id,
                                                                                   // CommissionPercent = r.Cells[COLS_AirportCommission.CommissionOnPercent].Value.ToBool() == true ? r.Cells[COLS_AirportCommission.CommissionPercent].Value.ToInt() : 0,
                                                                                    CompanyId = ddlCompany.SelectedValue.ToIntorNull(),
                                                                                    VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull(),
                                                                                    LocationTypeId = Enums.LOCATION_TYPES.AIRPORT,
                                                                                    LocationId = r.Cells[COLS_AirportCommission.LocationId].Value.ToIntorNull(),
                                                                                    //CommissionAmount = r.Cells[COLS_AirportCommission.CommissionOnPercent].Value.ToBool() != true ? r.Cells[COLS_AirportCommission.CommissionAmount].Value.ToDecimal() : 0,
                                                                                    //CommissionOnPercent = r.Cells[COLS_AirportCommission.CommissionOnPercent].Value.ToBool(),
                                                                                    CommissionAmount = r.Cells[COLS_AirportCommission.CommissionAmount].Value.ToDecimal(),
                                                                                    CompanyPrice = r.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal(),
                                                                                    DriverPrice = r.Cells[COLS_AirportCommission.DriverPrice].Value.ToDecimal(),

                                                                                }).ToList();


                    //List<Gen_Company_AgentCommission> listofDetailCommissionStations = (from r in grdStationCommission.Rows
                    //                                                                    select new Gen_Company_AgentCommission
                    //                                                                    {
                    //                                                                        Id = r.Cells[COLS_StationCommission.Id].Value.ToInt(),
                    //                                                                        FareId = objMaster.Current.Id,
                    //                                                                        // CommissionPercent = r.Cells[COLS_StationCommission.CommissionOnPercent].Value.ToBool() == true ? r.Cells[COLS_StationCommission.CommissionPercent].Value.ToInt() : 0,
                    //                                                                        CompanyId = ddlCompany.SelectedValue.ToIntorNull(),//r.Cells[COLS_AirportCommission.MasterId].Value.ToInt(),

                    //                                                                        VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull(),
                    //                                                                        LocationTypeId = Enums.LOCATION_TYPES.UNDERGROUNDSTATION, //r.Cells[COLS_StationCommission.LocationTypeId].Value.ToInt(),
                    //                                                                        LocationId = r.Cells[COLS_StationCommission.LocationId].Value.ToIntorNull(),
                    //                                                                          CommissionAmount = r.Cells[COLS_StationCommission.CommissionAmount].Value.ToDecimal(),
                    //                                                                        CompanyPrice = r.Cells[COLS_StationCommission.CompanyPrice].Value.ToDecimal(),
                    //                                                                        DriverPrice = r.Cells[COLS_StationCommission.DriverPrice].Value.ToDecimal(),

                    //                                                                        // CommissionAmount = r.Cells[COLS_StationCommission.CommissionOnPercent].Value.ToBool() != true ? r.Cells[COLS_StationCommission.CommissionAmount].Value.ToDecimal() : 0,
                    //                                                                        //   CommissionOnPercent = r.Cells[COLS_StationCommission.CommissionOnPercent].Value.ToBool(),

                    //                                                                    }).ToList();

                    List<Gen_Company_AgentCommission> finalAgentList = listofDetailCommission;


                    Utils.General.SyncChildCollection(ref savedListCommission, ref finalAgentList, "Id", skipCommissionProperties);
                }
                //


                objMaster.Save();

                General.RefreshListWithoutSelected<frmFaresList>("frmFaresList1");
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }


        }

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            Fare obj = objMaster.Current;


            ddlVehicleType.SelectedValue = obj.VehicleTypeId;
            chkCompanyWise.Checked = obj.IsCompanyWise.ToBool();
            ddlCompany.SelectedValue = obj.CompanyId;
            ddlSubCompanyId.SelectedValue = obj.SubCompanyId;
            numStartRate.Value = obj.StartRate.ToDecimal();
            numStartRateValidMiles.Value = obj.StartRateValidMiles.ToDecimal();


            DisplayFareDetails(obj);
            DisplayFareOtherDetails(obj);
            DisplayFarePDAOtherDetails(obj);
            DisplayZoneDetails(obj);
            
            
            if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "SHOW AIRPORT FARES") >0)
            {
                DisplayAirportAndStationDetails();
            }
        }
        private void DisplayAirportAndStationDetails()
        {
            try
            {
                if (grdAirportCommission != null)
                {
                    foreach (var item in objMaster.Current.Gen_Company_AgentCommissions.Where(c => c.LocationId != null && c.LocationTypeId==Enums.LOCATION_TYPES.AIRPORT))
                    {
                        GridViewRowInfo row = grdAirportCommission.Rows.FirstOrDefault(a => a.Cells[COLS_AirportCommission.LocationId].Value.ToInt() == item.LocationId.ToInt());
                        if (row != null)
                        {
                            row.Cells[COLS_AirportCommission.Id].Value = item.Id;
                            row.Cells[COLS_AirportCommission.FareId].Value = item.FareId;
                            row.Cells[COLS_AirportCommission.MasterId].Value = item.CompanyId;
                            row.Cells[COLS_AirportCommission.LocationId].Value = item.LocationId.ToIntorNull();
                            row.Cells[COLS_AirportCommission.Location].Value = item.Gen_Location.DefaultIfEmpty().LocationName.ToStr();
                            row.Cells[COLS_AirportCommission.CompanyPrice].Value = item.CompanyPrice;
                            row.Cells[COLS_AirportCommission.DriverPrice].Value = item.DriverPrice;
                            row.Cells[COLS_AirportCommission.CommissionAmount].Value = item.CommissionAmount.ToDecimal();
                        }
                    }
                    //var Airportlist = (from a in General.GetQueryable<Gen_Location>(c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT)
                    //                   select new
                    //                   {
                    //                       Id = a.Id,
                    //                       LocationName = a.LocationName
                    //                   }).ToList();
                    //grdAirportCommission.RowCount = Airportlist.Count;
                    //for (int i = 0; i < grdAirportCommission.RowCount; i++)
                    //{
                    //    grdAirportCommission.Rows[i].Cells[COLS_AirportCommission.LocationId].Value = Airportlist[i].Id;
                    //    grdAirportCommission.Rows[i].Cells[COLS_AirportCommission.Location].Value = Airportlist[i].LocationName;
                    //}
                }



                //if (grdStationCommission != null)
                //{
                //    foreach (var item in objMaster.Current.Gen_Company_AgentCommissions.Where(c => c.LocationId != null && c.LocationTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION))
                //    {
                //        GridViewRowInfo row = grdStationCommission.Rows.FirstOrDefault(a => a.Cells[COLS_StationCommission.LocationId].Value.ToInt() == item.LocationId.ToInt());
                //        if (row != null)
                //        {
                //            row.Cells[COLS_StationCommission.Id].Value = item.Id;
                //            row.Cells[COLS_StationCommission.FareId].Value = item.FareId;
                //            row.Cells[COLS_StationCommission.MasterId].Value = item.CompanyId;
                //            row.Cells[COLS_StationCommission.LocationId].Value = item.LocationId.ToIntorNull();
                //            row.Cells[COLS_StationCommission.Location].Value = item.Gen_Location.DefaultIfEmpty().LocationName.ToStr();
                //            row.Cells[COLS_StationCommission.CompanyPrice].Value = item.CompanyPrice;
                //            row.Cells[COLS_StationCommission.DriverPrice].Value = item.DriverPrice;
                //            row.Cells[COLS_StationCommission.CommissionAmount].Value = item.CommissionAmount.ToDecimal();
                //        }
                //    }

                    
                //}
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void DisplayZoneDetails(Fare obj)
        {
            if (obj == null) return;

            grdPlotWiseFare.BeginUpdate();

            grdPlotWiseFare.RowCount = obj.Fare_ZoneWisePricings.Count;
            //Gen_ZonesType_Pricing zone = null;
            for (int i = 0; i < grdPlotWiseFare.RowCount; i++)
            {
                grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.Id].Value = obj.Fare_ZoneWisePricings[i].Id;
                grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FareId].Value = obj.Fare_ZoneWisePricings[i].FareId;
                grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FromZoneId].Value = obj.Fare_ZoneWisePricings[i].FromZoneId;
                grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.ToZoneId].Value = obj.Fare_ZoneWisePricings[i].ToZoneId;


                if (obj.Fare_ZoneWisePricings[i].FromZoneId != null)
                {

                    grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FromPlotNo].Value = obj.Fare_ZoneWisePricings[i].Gen_Zone.DefaultIfEmpty().OrderNo.ToStr();

                    grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FromPlot].Value = obj.Fare_ZoneWisePricings[i].Gen_Zone.DefaultIfEmpty().ZoneName.ToStr().Trim();


                }

                if (obj.Fare_ZoneWisePricings[i].ToZoneId != null)
                {
                    grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.ToPlotNo].Value = obj.Fare_ZoneWisePricings[i].Gen_Zone1.DefaultIfEmpty().OrderNo.ToStr();

                    grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.ToPlot].Value = obj.Fare_ZoneWisePricings[i].Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr().Trim();


                }
                // obj.Fare_ZoneWisePricings[i].Gen_Zone.ShortName;
                grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.Price].Value = obj.Fare_ZoneWisePricings[i].Price;
                grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.COMPANYFARE].Value = obj.Fare_ZoneWisePricings[i].CompanyRate;
                //  zone = obj.Fare_ZoneWisePricings[i].de.DefaultIfEmpty();
            }

            grdPlotWiseFare.EndUpdate();
        }
        private void DisplayFareDetails(Fare obj)
        {
            if (obj == null) return;



            grdDetails.RowCount = obj.Fare_ChargesDetails.Count;
          

            grdDetails.BeginUpdate();
            for (int i = 0; i < grdDetails.RowCount; i++)
            {
                grdDetails.Rows[i].Cells[COLS_DETAILS.ID].Value = obj.Fare_ChargesDetails[i].Id;
                grdDetails.Rows[i].Cells[COLS_DETAILS.FAREID].Value = obj.Fare_ChargesDetails[i].FareId;
                grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value = obj.Fare_ChargesDetails[i].OriginLocationTypeId;
                grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value = obj.Fare_ChargesDetails[i].DestinationLocationTypeId;

                grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATIONID].Value = obj.Fare_ChargesDetails[i].OriginId;

                grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATIONID].Value = obj.Fare_ChargesDetails[i].DestinationId;



                if (obj.Fare_ChargesDetails[i].OriginLocationTypeId.ToInt() == 100)
                {
                    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATIONID].Value = obj.Fare_ChargesDetails[i].FromZoneId;

                    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = obj.Fare_ChargesDetails[i].Gen_Zone.DefaultIfEmpty().ZoneName;
                }
                else
                {

                    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = obj.Fare_ChargesDetails[i].FromAddress.ToStr();

                    //Loc = obj.Fare_ChargesDetails[i].Gen_Location1.DefaultIfEmpty();
                    //if (Loc.LocationTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = Loc.PostCode;
                    //}
                    //else if (obj.Fare_ChargesDetails[i].OriginLocationTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = obj.Fare_ChargesDetails[i].FromAddress.ToStr();


                    //}


                    //else
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = Loc.PostCode == string.Empty ? Loc.LocationName :
                    //                                                                 Loc.LocationName + " - " + Loc.PostCode;
                    //}
                }


                if (obj.Fare_ChargesDetails[i].DestinationLocationTypeId.ToInt() == 100)
                {
                    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATIONID].Value = obj.Fare_ChargesDetails[i].ToZoneId;


                    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = obj.Fare_ChargesDetails[i].Gen_Zone1.DefaultIfEmpty().ZoneName;

                }
                else
                {
                    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = obj.Fare_ChargesDetails[i].ToAddress.ToStr();

                    //Loc = obj.Fare_ChargesDetails[i].Gen_Location.DefaultIfEmpty();
                    //if (Loc.LocationTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = Loc.PostCode;


                    //}
                    //else if (obj.Fare_ChargesDetails[i].DestinationLocationTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = obj.Fare_ChargesDetails[i].ToAddress.ToStr();


                    //}
                    //else
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = Loc.PostCode == string.Empty ? Loc.LocationName :
                    //                                                                 Loc.LocationName + " - " + Loc.PostCode;
                    //}
                }


                grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = obj.Fare_ChargesDetails[i].Rate;
                grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value = obj.Fare_ChargesDetails[i].CompanyRate.ToDecimal();
            }

            grdDetails.EndUpdate();

            ClearFareDetails();
        }


        private void DisplayFareOtherDetails(Fare obj)
        {
            if (obj == null) return;

            grdOtherCharges.RowCount = obj.Fare_OtherCharges.Count;
            for (int i = 0; i < grdOtherCharges.RowCount; i++)
            {
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.ID].Value = obj.Fare_OtherCharges[i].Id;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FAREID].Value = obj.Fare_OtherCharges[i].FareId;



                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMMILE].Value = obj.Fare_OtherCharges[i].FromMile.ToDecimal();
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TOMILE].Value = obj.Fare_OtherCharges[i].ToMile.ToDecimal();

                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FARE].Value = obj.Fare_OtherCharges[i].Rate;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.COMPANYFARE].Value = obj.Fare_OtherCharges[i].CompanyRate;

                if (AppVars.objPolicyConfiguration != null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                {





                    DateTime? fromStartTime = obj.Fare_OtherCharges[i].FromStartTime;
                    DateTime? tillStartTime = obj.Fare_OtherCharges[i].TillStartTime;
                    DateTime? fromEndTime = obj.Fare_OtherCharges[i].FromEndTime;
                    DateTime? tillEndTime = obj.Fare_OtherCharges[i].TillEndTime;



                    string peakTime = string.Empty;
                    string offPeakTime = string.Empty;


                    if (fromStartTime != null && tillStartTime != null)
                    {
                        peakTime = string.Format("{0:hh:tt}", fromStartTime) + " to " + string.Format("{0:hh:tt}", tillStartTime);
                    }

                    if (fromEndTime != null && tillEndTime != null)
                    {
                        offPeakTime = string.Format("{0:hh:tt}", fromEndTime) + " to " + string.Format("{0:hh:tt}", tillEndTime);
                    }




                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.PEAKRATE].Value = obj.Fare_OtherCharges[i].PeakTimeRate.ToDecimal();
                    grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = obj.Fare_OtherCharges[i].OffPeakTimeRate.ToDecimal();
                }
            }

            ClearOtherChargesDetails();
        }




        private void DisplayFarePDAOtherDetails(Fare obj)
        {
            if (obj == null) return;

            grdPDAOtherCharges.RowCount = obj.Fare_PDAMeters.Count;
            for (int i = 0; i < grdPDAOtherCharges.RowCount; i++)
            {
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.ID].Value = obj.Fare_PDAMeters[i].Id;
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FAREID].Value = obj.Fare_PDAMeters[i].FareId;



                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMMILE].Value = obj.Fare_PDAMeters[i].FromMile.ToDecimal();
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TOMILE].Value = obj.Fare_PDAMeters[i].ToMile.ToDecimal();

                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FARE].Value = obj.Fare_PDAMeters[i].Rate;
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.COMPANYFARE].Value = obj.Fare_PDAMeters[i].CompanyRate;


                if (AppVars.objPolicyConfiguration != null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                {

                    DateTime? fromStartTime = obj.Fare_PDAMeters[i].FromStartTime;
                    DateTime? tillStartTime = obj.Fare_PDAMeters[i].TillStartTime;
                    DateTime? fromEndTime = obj.Fare_PDAMeters[i].FromEndTime;
                    DateTime? tillEndTime = obj.Fare_PDAMeters[i].TillEndTime;

                    string peakTime = string.Empty;
                    string offPeakTime = string.Empty;


                    if (fromStartTime != null && tillStartTime != null)
                    {
                        peakTime = string.Format("{0:hh:tt}", fromStartTime) + " to " + string.Format("{0:hh:tt}", tillStartTime);
                    }

                    if (fromEndTime != null && tillEndTime != null)
                    {
                        offPeakTime = string.Format("{0:hh:tt}", fromEndTime) + " to " + string.Format("{0:hh:tt}", tillEndTime);
                    }




                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.PEAKRATE].Value = obj.Fare_PDAMeters[i].PeakTimeRate.ToDecimal();
                    grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = obj.Fare_PDAMeters[i].OffPeakTimeRate.ToDecimal();
                }
            }

            ClearPDAOtherChargesDetails();
        }



    //    private bool IsOpenFromCopyFares = false;

        public void CopyFare(Fare obj)
        {

          //  OnNew();
        //    this.IsOpenFromCopyFares = true;
            DisplayFareDetails(obj);
            DisplayFareOtherDetails(obj);
            DisplayFarePDAOtherDetails(obj);
            DisplayFareAirportDetails(obj);
            DisplayZoneDetails(obj);
            pnlPercentage.Visible = true;
         }


        private void DisplayFareAirportDetails(Fare obj)
        {
            try
            {
                if (grdAirportCommission != null)
                {
                    foreach (var item in obj.Gen_Company_AgentCommissions.Where(c => c.LocationId != null && c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT))
                    {
                        GridViewRowInfo row = grdAirportCommission.Rows.FirstOrDefault(a => a.Cells[COLS_AirportCommission.LocationId].Value.ToInt() == item.LocationId.ToInt());
                        if (row != null)
                        {
                            row.Cells[COLS_AirportCommission.Id].Value = item.Id;
                            row.Cells[COLS_AirportCommission.FareId].Value = item.FareId;
                            row.Cells[COLS_AirportCommission.MasterId].Value = item.CompanyId;
                            row.Cells[COLS_AirportCommission.LocationId].Value = item.LocationId.ToIntorNull();
                            row.Cells[COLS_AirportCommission.Location].Value = item.Gen_Location.DefaultIfEmpty().LocationName.ToStr();
                            row.Cells[COLS_AirportCommission.CompanyPrice].Value = item.CompanyPrice;
                            row.Cells[COLS_AirportCommission.DriverPrice].Value = item.DriverPrice;
                            row.Cells[COLS_AirportCommission.CommissionAmount].Value = item.CommissionAmount.ToDecimal();
                        }
                    }
                }  
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }


        }

        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlVehicleType.SelectedValue == null)
            {
                ENUtils.ShowMessage("Required : Vehicle");
                return;

            }
            AddDetail();
        }

        private void AddDetail()
        {

            decimal fares = numRate_FareCharges.Value.ToDecimal();

            decimal Companyfares = numCompanyFares.Value.ToDecimal();

            //numCompanyFares


            int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
            int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            int? fromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
            int? toLocId = ddlToLocation.SelectedValue.ToIntorNull();


            string fromLocation = ddlFromLocation.Text.ToStr();
            string toLocation = ddlToLocation.Text.ToStr();
            string fromLocType = ddlFromLocType.Text.ToStr();
            string toLocType = ddlToLocType.Text.ToStr();
            string fromAddress = txtFromAddress.Text.ToStr().Trim().ToUpper();
            string toAddress = txtToAddress.Text.ToStr().Trim().ToUpper();


            if (fromLocTypeId == 100 && toLocTypeId == 100)
            {
                ENUtils.ShowMessage("Please enter Plot to NonPlot or NonPlot to Plot Fares");
                return;

            }

            string msg = string.Empty;


            string fromPostCode = "";
            string toPostCode = "";


            if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                GridViewRowInfo row;

                if (grdDetails.CurrentRow != null)
                    row = grdDetails.CurrentRow;
                else
                    row = grdDetails.Rows.AddNew();

                if (fromLocTypeId != Enums.LOCATION_TYPES.ADDRESS)
                    fromAddress = fromLocation;

                if (toLocTypeId != Enums.LOCATION_TYPES.ADDRESS)
                    toAddress = toLocation;

                row.Cells[COLS_DETAILS.FROMLOCATION].Value = fromAddress;
                row.Cells[COLS_DETAILS.TOLOCATION].Value = toAddress;
                row.Cells[COLS_DETAILS.FROMLOCATIONID].Value = fromLocId;
                row.Cells[COLS_DETAILS.TOLOCATIONID].Value = toLocId;

                row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value = fromLocTypeId;
                row.Cells[COLS_DETAILS.TOLOCTYPEID].Value = toLocTypeId;
                row.Cells[COLS_DETAILS.FARE].Value = fares;
                row.Cells[COLS_DETAILS.COMPANYFARE].Value = Companyfares;
            }

            else
            {




                if (lstFromLocation.Items.Count == 0)
                {


                    if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        if (fromLocId == null)
                        {
                            fromPostCode = General.GetPostCodeMatch(fromLocation);
                            if (!string.IsNullOrEmpty(fromPostCode) && fromPostCode.IsAlpha() == false)
                            {
                                AddLocation(fromLocation, ref fromLocId, ref fromPostCode);
                                FillFromLocations();
                                ddlFromLocation.SelectedValue = fromLocId;
                            }
                        }
                    }

                    if (fromLocId == null)
                    {
                        msg += "Required : From Location." + Environment.NewLine;

                    }


                    if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        if (toLocId == null)
                        {
                            toPostCode = General.GetPostCodeMatch(toLocation);
                            if (!string.IsNullOrEmpty(toPostCode) && toPostCode.IsAlpha() == false)
                            {
                                AddLocation(toLocation, ref toLocId, ref toPostCode);
                                FillToLocations();
                                ddlToLocation.SelectedValue = toLocId;
                            }
                        }
                    }

                    if (toLocId == null)
                    {
                        msg += "Required : To Location." + Environment.NewLine;
                    }

                    if (fares == 0)
                    {
                        msg += "Required : Fare rate.";
                    }

                    if (!string.IsNullOrEmpty(msg))
                    {
                        ENUtils.ShowMessage(msg);
                        return;

                    }


                    GridViewRowInfo row;
                    if (grdDetails.CurrentRow == null &&
                        grdDetails.Rows.Count(c =>
                                                c.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToInt() == fromLocId
                                            && c.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToInt() == toLocId
                                             ) > 0)
                    {
                        ENUtils.ShowMessage("From Location and To Location already exist");
                        ddlFromLocation.Focus();
                        return;

                    }


                    if (grdDetails.CurrentRow != null)
                        row = grdDetails.CurrentRow;
                    else
                        row = grdDetails.Rows.AddNew();

                    row.Cells[COLS_DETAILS.FROMLOCATION].Value = fromLocation;
                    row.Cells[COLS_DETAILS.TOLOCATION].Value = toLocation;
                    row.Cells[COLS_DETAILS.FROMLOCATIONID].Value = fromLocId;
                    row.Cells[COLS_DETAILS.TOLOCATIONID].Value = toLocId;

                    row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value = fromLocTypeId;
                    row.Cells[COLS_DETAILS.TOLOCTYPEID].Value = toLocTypeId;
                    row.Cells[COLS_DETAILS.FARE].Value = fares;
                    row.Cells[COLS_DETAILS.COMPANYFARE].Value = Companyfares;
                }
                else
                {

                    if (lstToLocation.Items.Count == 0)
                    {
                        ENUtils.ShowMessage("Required : To Location" + Environment.NewLine + "To Location Box cannot be Empty");
                        return;
                    }


                    if (fares == 0)
                    {
                        ENUtils.ShowMessage("Required : Fare Rate");
                        return;
                    }




                    GridViewRowInfo detailRow = null;
                    foreach (RadListDataItem locFrom in lstFromLocation.Items)
                    {

                        fromLocTypeId = locFrom.Value.ToStr().Substring(locFrom.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
                        fromLocId = locFrom.Value.ToStr().Remove(locFrom.Value.ToStr().IndexOf(',')).ToIntorNull();
                        fromLocation = locFrom.Text.ToStr();

                        foreach (RadListDataItem locTo in lstToLocation.Items)
                        {
                            detailRow = grdDetails.Rows.AddNew();

                            detailRow.Cells[COLS_DETAILS.FROMLOCATION].Value = fromLocation;
                            detailRow.Cells[COLS_DETAILS.FROMLOCATIONID].Value = fromLocId;
                            detailRow.Cells[COLS_DETAILS.FROMLOCTYPEID].Value = fromLocTypeId;


                            detailRow.Cells[COLS_DETAILS.TOLOCATION].Value = locTo.Text.ToStr();
                            detailRow.Cells[COLS_DETAILS.TOLOCATIONID].Value = locTo.Value.ToStr().Remove(locTo.Value.ToStr().IndexOf(',')).ToIntorNull();
                            detailRow.Cells[COLS_DETAILS.TOLOCTYPEID].Value = locTo.Value.ToStr().Substring(locTo.Value.ToStr().IndexOf(',') + 1).ToIntorNull();

                            detailRow.Cells[COLS_DETAILS.FARE].Value = fares;
                            detailRow.Cells[COLS_DETAILS.COMPANYFARE].Value = Companyfares;
                            //                row.Cells[COLS_DETAILS.COMPANYFARE].Value = Companyfares;
                        }
                    }




                }
            }

            ClearFareDetails();

        }


        private void AddLocation(string postCode, ref int? locId, ref string postCodeValue)
        {
            LocationBO loc = new LocationBO();
            try
            {

                loc.New();
                loc.Current.LocationName = postCode;
                loc.Current.PostCode = postCode;
                //    loc.Current.LocationName = "";
                loc.Current.LocationTypeId = Enums.LOCATION_TYPES.POSTCODE;
                loc.Save();



                locId = loc.Current.Id.ToIntorNull();
                postCodeValue = loc.Current.PostCode;

            }
            catch (Exception ex)
            {
                if (loc.Errors.Count > 0)
                    ENUtils.ShowMessage(loc.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFareDetails();
        }

        private void grdDetails_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdDetails.CurrentRow != null && grdDetails.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdDetails.CurrentRow;

                ddlFromLocType.SelectedValue = row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt();
                ddlToLocType.SelectedValue = row.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt();
                ddlFromLocation.SelectedValue = row.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToInt();
                ddlToLocation.SelectedValue = row.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToInt();




                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = row.Cells[COLS_DETAILS.FROMLOCATION].Value.ToStr();
                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = row.Cells[COLS_DETAILS.TOLOCATION].Value.ToStr();
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                numRate_FareCharges.Value = row.Cells[COLS_DETAILS.FARE].Value.ToDecimal();
                numCompanyFares.Value = row.Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal();


            }
        }

        private void chkCompanyWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            CompanyWise(args.ToggleState);
        }

        private void CompanyWise(ToggleState args)
        {
            if (args == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlCompany.Enabled = true;
              //  pnlPercentage.Visible = false;
                chkApplyToAll.Checked = false;
                chkApplyToAll.Visible = false;
                
                if (pageAirportCharges != null)
                {

                    pageAirportCharges.Item.Visibility = ElementVisibility.Visible;

                }

                //if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool() == false)
                //{
                    pnlStartRates.Visible = true;
              //  }
            }
            else
            {
                ddlCompany.Enabled = false;
                ddlCompany.SelectedValue = null;
                if (pageAirportCharges != null)
                {
                    pageAirportCharges.Item.Visibility = ElementVisibility.Collapsed;
                }
                pnlPercentage.Visible = true;

                pnlStartRates.Visible = false;
                numStartRate.Value = 0.00m;
                numStartRateValidMiles.Value = 0.00m;
            }

        }

        private void ddlFromLocType_Validated(object sender, EventArgs e)
        {
            //  FillFromLocations();
        }

        private void ddlToLocType_Validated(object sender, EventArgs e)
        {
            //  FillToLocations();
        }

        private void FillFromLocations()
        {
            int locTypeId = ddlFromLocType.SelectedValue.ToInt();
            if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                ComboFunctions.FillPostCodeLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);

                txtFromAddress.Visible = false;
                ddlFromLocation.Visible = true;
                lstFromLocation.Visible = true;
                btnAddFromLoc.Visible = true;
                btnEditFromLoc.Visible = true;
                btnDeleteFromLst.Visible = true;
            }
            else if (locTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                txtFromAddress.Visible = true;
                ddlFromLocation.Visible = false;
                lstFromLocation.Visible = false;
                btnAddFromLoc.Visible = false;
                btnEditFromLoc.Visible = false;
                btnDeleteFromLst.Visible = false;


            }
            //else if (ddlFromLocType.Text.Trim().ToLower() == "zone")
            //{

            //    ComboFunctions.FillZonesPlottedCombo(ddlFromLocation);


            //}
            else
            {
                txtFromAddress.Visible = false;
                ddlFromLocation.Visible = true;
                lstFromLocation.Visible = true;
                btnAddFromLoc.Visible = true;
                btnEditFromLoc.Visible = true;

                btnDeleteFromLst.Visible = true;

                if (locTypeId != 100)
                {
                    ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);
                }
                else
                {
                    ComboFunctions.FillZonesCombo(ddlFromLocation);

                }
            }
        }

        private void FillToLocations()
        {
            int locTypeId = ddlToLocType.SelectedValue.ToInt();
            if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                ComboFunctions.FillPostCodeLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                txtToAddress.Visible = false;
                ddlToLocation.Visible = true;
                lstToLocation.Visible = true;
                btnAddToLoc.Visible = true;
                btnEditToLoc.Visible = true;
                btnDeleteToLst.Visible = true;
                btnAddToLocation.Visible = true;
            }
            else if (locTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                txtToAddress.Visible = true;
                ddlToLocation.Visible = false;
                lstToLocation.Visible = false;
                btnAddToLoc.Visible = false;
                btnEditToLoc.Visible = false;
                btnDeleteToLst.Visible = false;
                btnAddToLocation.Visible = false;

            }
            //else if (ddlToLocType.Text.Trim().ToLower() == "zone")
            //{

            //    ComboFunctions.FillZonesPlottedCombo(ddlToLocation);


            //}
            else
            {
                txtToAddress.Visible = false;
                ddlToLocation.Visible = true;
                lstToLocation.Visible = true;
                btnAddToLoc.Visible = true;
                btnEditToLoc.Visible = true;
                btnDeleteToLst.Visible = true;
                btnAddToLocation.Visible = true;


                if (locTypeId != 100)
                {
                    ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                }
                else
                {
                    ComboFunctions.FillZonesCombo(ddlToLocation);

                }


            }
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {

        }

        private void radLabel8_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_OtherCharges_Click(object sender, EventArgs e)
        {

            AddOtherChargesDetail();
        }


        private void AddOtherChargesDetail()
        {
            try
            {

                decimal fromMile = numFromMile.Value.ToDecimal();
                decimal toMile = numToMile.Value.ToDecimal();
                decimal rate = numRates_OtherCharges.Value.ToDecimal();

                decimal companyrate = numCompanyRates_OtherCharges.Value.ToDecimal();
                string msg = string.Empty;


                TimeSpan FromStartTime = TimeSpan.Zero;
                TimeSpan TillStartTime = TimeSpan.Zero;
                TimeSpan FromEndTime = TimeSpan.Zero;
                TimeSpan TillEndTime = TimeSpan.Zero;

                string peakTime = string.Empty;
                string offPeakTime = string.Empty;
                decimal peakRate = 0;
                decimal offPeakRate = 0;
                if (AppVars.objPolicyConfiguration != null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                {







                    peakRate = numPeakRate.Value.ToDecimal();
                    offPeakRate = numOffPeakRate.Value.ToDecimal();


                }


                if (toMile == 0)
                {
                    msg += "Required : To Mile." + Environment.NewLine;
                }

                if (rate == 0)
                {
                    msg += "Required : Fare rate.";
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;

                }


                GridViewRowInfo row;
                if (grdOtherCharges.CurrentRow == null &&
                    grdOtherCharges.Rows.Count(c =>
                                            c.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal() == fromMile
                                        && c.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal() == toMile
                                         ) > 0)
                {
                    ENUtils.ShowMessage("From Mile and To Mile already exist");
                    numFromMile.Focus();
                    return;
                }


                DateTime? fromStartTime = dtpFromStartTime.Value;
                DateTime? tillStartTime = dtpTillStartTime.Value;
                DateTime? fromEndTime = dtpFromEndTime.Value;
                DateTime? tillEndTime = dtpTillEndTime.Value;


                if (HasOffPeakRate)
                {


                    string error = string.Empty;


                    if (fromStartTime == null)
                    {
                        error += "Required : From Start Time" + Environment.NewLine;

                    }

                    if (tillStartTime == null)
                    {
                        error += "Required : Till Start Time" + Environment.NewLine;

                    }

                    if (fromEndTime == null)
                    {
                        error += "Required : From End Time" + Environment.NewLine;


                    }


                    if (tillEndTime == null)
                    {
                        error += "Required : Till End Time";

                    }


                    if (!string.IsNullOrEmpty(error))
                    {
                        ENUtils.ShowMessage(error);
                        return;
                    }

                    FromStartTime = fromStartTime.Value.TimeOfDay;
                    TillStartTime = tillStartTime.Value.TimeOfDay;

                    FromEndTime = fromEndTime.Value.TimeOfDay;
                    TillEndTime = tillEndTime.Value.TimeOfDay;

                    error = string.Empty;


                    //if (TillStartTime > FromEndTime)
                    //{
                    //    error += "'From End' Time cannot be less than 'To Start' Time" + Environment.NewLine;
                    //}


                    //if (TillEndTime > FromStartTime)
                    //{

                    //    error += "'To End' Time cannot be greater than 'From Start' Time";
                    //}


                    if (!string.IsNullOrEmpty(error))
                    {
                        ENUtils.ShowMessage(error);
                        return;
                    }



                    peakTime = string.Format("{0:hh tt}", fromStartTime) + " to " + string.Format("{0:hh tt}", tillStartTime);
                    offPeakTime = string.Format("{0:hh tt}", fromEndTime) + " to " + string.Format("{0:hh tt}", tillEndTime);

                }


                if (grdOtherCharges.CurrentRow != null)
                    row = grdOtherCharges.CurrentRow;
                else
                    row = grdOtherCharges.Rows.AddNew();


                row.Cells[COLS_OTHERDETAILS.FROMMILE].Value = fromMile;
                row.Cells[COLS_OTHERDETAILS.TOMILE].Value = toMile;

                row.Cells[COLS_OTHERDETAILS.FARE].Value = rate;

                row.Cells[COLS_OTHERDETAILS.COMPANYFARE].Value = companyrate;


                if (HasOffPeakRate)
                {
                    row.Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                    row.Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                    row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                    row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                    row.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                    row.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                    row.Cells[COLS_OTHERDETAILS.PEAKRATE].Value = peakRate;
                    row.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = offPeakRate;
                }
                ClearOtherChargesDetails();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        private void ClearOtherChargesDetails()
        {
            numFromMile.Value = 0;
            numToMile.Value = 0;
            numRates_OtherCharges.Value = 0;
            numCompanyRates_OtherCharges.Value = 0;
            grdOtherCharges.CurrentRow = null;


            numPeakRate.Value = 0;
            numOffPeakRate.Value = 0;

            numFromMile.Focus();

        }

        private void grdOtherCharges_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdOtherCharges.CurrentRow != null && grdOtherCharges.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdOtherCharges.CurrentRow;

                numFromMile.Value = row.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal();
                numToMile.Value = row.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal();

                numRates_OtherCharges.Value = row.Cells[COLS_DETAILS.FARE].Value.ToDecimal();
                numCompanyRates_OtherCharges.Value = row.Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal();

                if (HasOffPeakRate)
                {
                    dtpFromStartTime.Value = row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTime();
                    dtpTillStartTime.Value = row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTime();
                    dtpFromEndTime.Value = row.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTime();
                    dtpTillEndTime.Value = row.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTime();


                    numPeakRate.Value = row.Cells[COLS_OTHERDETAILS.PEAKRATE].Value.ToDecimal();
                    numOffPeakRate.Value = row.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value.ToDecimal();
                }

            }
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ddlFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillFromLocations();
        }

        private void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();
        }

        private void btnAddFromLocation_Click(object sender, EventArgs e)
        {
            RadButton ddl = (RadButton)sender;

            int? locTypeId = null;
            string name = ddl.Name;
            if (name == "btnAddFromLocation")
            {
                locTypeId = ddlFromLocType.SelectedValue.ToIntorNull();

            }
            else if (name == "btnAddToLocation")
            {
                locTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            }


            int? locId = General.ShowLocationForm(locTypeId);


            if (name == "btnAddFromLocation")
            {
                FillFromLocations();
                ddlFromLocation.SelectedValue = locId;

            }
            else if (name == "btnAddToLocation")
            {
                FillToLocations();
                ddlToLocation.SelectedValue = locId;

            }



        }

        private void btnAddFromLoc_Click(object sender, EventArgs e)
        {
            int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
            int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            int? fromLocId = ddlFromLocation.SelectedValue.ToIntorNull();

            string msg = string.Empty;
            string fromLocation = ddlFromLocation.Text.ToStr();
            string fromPostCode = "";


            if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                if (fromLocId == null)
                {
                    fromPostCode = General.GetPostCodeMatch(fromLocation);
                    if (!string.IsNullOrEmpty(fromPostCode) && fromPostCode.IsAlpha() == false)
                    {
                        AddLocation(fromLocation, ref fromLocId, ref fromPostCode);
                        FillFromLocations();



                    }
                }
            }




            if (fromLocId == null)
            {
                msg += "Required : From Location." + Environment.NewLine;

            }

            if (!string.IsNullOrEmpty(msg))
            {
                ENUtils.ShowMessage(msg);
                return;

            }

            AddLocationToListBox(lstFromLocation, fromLocId, fromLocation, fromLocTypeId, ddlFromLocation.Tag.ToIntorNull());

            ClearFromLocation();
        }

        private void AddLocationToListBox(RadListControl lst, int? LocId, string locName, int? locTypeId, int? index)
        {


            string value = LocId.ToStr() + "," + locTypeId.ToStr();

            RadListDataItem item = null;
            if (index == null || index == -1)
            {

                item = new RadListDataItem();
                lst.Items.Add(item);
            }
            else
            {
                item = lst.SelectedItem;

            }


            item.Text = locName;
            item.Value = value;



        }

        private void btnAddToLoc_Click(object sender, EventArgs e)
        {
            int? LocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            int? LocId = ddlToLocation.SelectedValue.ToIntorNull();

            string msg = string.Empty;
            string locName = ddlToLocation.Text.ToStr();
            string PostCode = "";




            if (LocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                if (LocId == null)
                {
                    PostCode = General.GetPostCodeMatch(locName);
                    if (!string.IsNullOrEmpty(PostCode) && PostCode.IsAlpha() == false)
                    {
                        AddLocation(locName, ref LocId, ref PostCode);
                        FillToLocations();



                    }
                }
            }





            if (LocId == null)
            {
                msg += "Required : To Location." + Environment.NewLine;

            }

            if (!string.IsNullOrEmpty(msg))
            {
                ENUtils.ShowMessage(msg);
                return;

            }


            AddLocationToListBox(lstToLocation, LocId, locName, LocTypeId, ddlToLocation.Tag.ToIntorNull());

            ClearToLocation();
        }

        private void btnEditFromLoc_Click(object sender, EventArgs e)
        {
            if (lstFromLocation.SelectedItem == null)
            {
                ENUtils.ShowMessage("Please select a value to Edit");
                return;
            }

            RadListDataItem item = lstFromLocation.SelectedItem;


            ddlFromLocType.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
            FillFromLocations();
            ddlFromLocation.SelectedValue = item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();

            ddlFromLocation.Tag = lstFromLocation.SelectedIndex;
        }

        private void btnEditToLoc_Click(object sender, EventArgs e)
        {
            if (lstToLocation.SelectedItem == null)
            {
                ENUtils.ShowMessage("Please select a value to Edit");
                return;
            }

            RadListDataItem item = lstToLocation.SelectedItem;


            ddlToLocType.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
            FillToLocations();
            ddlToLocation.SelectedValue = item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();


            ddlToLocation.Tag = lstToLocation.SelectedIndex;
        }

        private void btnDeleteFromLst_Click(object sender, EventArgs e)
        {
            DeleteItemFromListBox(lstFromLocation, lstFromLocation.SelectedItem);
        }


        private void DeleteItemFromListBox(RadListControl lst, RadListDataItem item)
        {
            if (lst.Items.Contains(item))
                lst.Items.Remove(item);

        }

        private void btnDeleteToLst_Click(object sender, EventArgs e)
        {
            DeleteItemFromListBox(lstToLocation, lstToLocation.SelectedItem);
        }

        private void btnClear_OtherChrges_Click(object sender, EventArgs e)
        {
            ClearOtherChargesDetails();
        }

        private void frmFares_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }

        }


        #region


        private void AddPDAOtherChargesDetail()
        {
            try
            {

                decimal fromMile = numpdafrommile.Value.ToDecimal();
                decimal toMile = numpdatomile.Value.ToDecimal();
                decimal rate = numPDARates_OtherCharges.Value.ToDecimal();
                decimal Companyrate = numCompanyPDARates_OtherCharges.Value.ToDecimal();
                string msg = string.Empty;


                TimeSpan FromStartTime = TimeSpan.Zero;
                TimeSpan TillStartTime = TimeSpan.Zero;
                TimeSpan FromEndTime = TimeSpan.Zero;
                TimeSpan TillEndTime = TimeSpan.Zero;

                string peakTime = string.Empty;
                string offPeakTime = string.Empty;
                decimal peakRate = 0;
                decimal offPeakRate = 0;
                if (AppVars.objPolicyConfiguration != null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                {







                    peakRate = numPDAPeakRate.Value.ToDecimal();
                    offPeakRate = numPDAOffPeakRate.Value.ToDecimal();


                }


                if (toMile == 0)
                {
                    msg += "Required : To Mile." + Environment.NewLine;
                }

                if (rate == 0)
                {
                    msg += "Required : Fare rate.";
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;

                }


                GridViewRowInfo row;
                if (grdPDAOtherCharges.CurrentRow == null &&
                    grdPDAOtherCharges.Rows.Count(c =>
                                            c.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal() == fromMile
                                        && c.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal() == toMile
                                         ) > 0)
                {
                    ENUtils.ShowMessage("From Mile and To Mile already exist");
                    numFromMile.Focus();
                    return;
                }


                DateTime? fromStartTime = dtpPDAFromStartTime.Value;
                DateTime? tillStartTime = dtpPDATillStartTime.Value;
                DateTime? fromEndTime = dtpPDAFromEndTime.Value;
                DateTime? tillEndTime = dtpPDATillEndTime.Value;


                if (HasOffPeakRate)
                {


                    string error = string.Empty;


                    if (fromStartTime == null)
                    {
                        error += "Required : From Start Time" + Environment.NewLine;

                    }

                    if (tillStartTime == null)
                    {
                        error += "Required : Till Start Time" + Environment.NewLine;

                    }

                    if (fromEndTime == null)
                    {
                        error += "Required : From End Time" + Environment.NewLine;


                    }


                    if (tillEndTime == null)
                    {
                        error += "Required : Till End Time";

                    }


                    if (!string.IsNullOrEmpty(error))
                    {
                        ENUtils.ShowMessage(error);
                        return;
                    }

                    FromStartTime = fromStartTime.Value.TimeOfDay;
                    TillStartTime = tillStartTime.Value.TimeOfDay;

                    FromEndTime = fromEndTime.Value.TimeOfDay;
                    TillEndTime = tillEndTime.Value.TimeOfDay;

                    error = string.Empty;


                    //if (TillStartTime > FromEndTime)
                    //{
                    //    error += "'From End' Time cannot be less than 'To Start' Time" + Environment.NewLine;
                    //}


                    //if (TillEndTime > FromStartTime)
                    //{

                    //    error += "'To End' Time cannot be greater than 'From Start' Time";
                    //}


                    if (!string.IsNullOrEmpty(error))
                    {
                        ENUtils.ShowMessage(error);
                        return;
                    }



                    peakTime = string.Format("{0:hh tt}", fromStartTime) + " to " + string.Format("{0:hh tt}", tillStartTime);
                    offPeakTime = string.Format("{0:hh tt}", fromEndTime) + " to " + string.Format("{0:hh tt}", tillEndTime);

                }


                if (grdPDAOtherCharges.CurrentRow != null)
                    row = grdPDAOtherCharges.CurrentRow;
                else
                    row = grdPDAOtherCharges.Rows.AddNew();


                row.Cells[COLS_OTHERDETAILS.FROMMILE].Value = fromMile;
                row.Cells[COLS_OTHERDETAILS.TOMILE].Value = toMile;

                row.Cells[COLS_OTHERDETAILS.FARE].Value = rate;
                row.Cells[COLS_OTHERDETAILS.COMPANYFARE].Value = Companyrate;


                if (HasOffPeakRate)
                {
                    row.Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                    row.Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                    row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                    row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                    row.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                    row.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                    row.Cells[COLS_OTHERDETAILS.PEAKRATE].Value = peakRate;
                    row.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = offPeakRate;
                }
                ClearPDAOtherChargesDetails();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        private void ClearPDAOtherChargesDetails()
        {
            numpdafrommile.Value = 0;
            numpdatomile.Value = 0;
            numPDARates_OtherCharges.Value = 0;
            numCompanyPDARates_OtherCharges.Value = 0;
            grdPDAOtherCharges.CurrentRow = null;


            numPDAPeakRate.Value = 0;
            numPDAOffPeakRate.Value = 0;


            numpdafrommile.Focus();

        }

        private void grdPDAOtherCharges_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdPDAOtherCharges.CurrentRow != null && grdPDAOtherCharges.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdPDAOtherCharges.CurrentRow;

                numpdafrommile.Value = row.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal();
                numpdatomile.Value = row.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal();

                numPDARates_OtherCharges.Value = row.Cells[COLS_DETAILS.FARE].Value.ToDecimal();
                numCompanyPDARates_OtherCharges.Value = row.Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal();


                if (HasOffPeakRate)
                {
                    dtpPDAFromStartTime.Value = row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTime();
                    dtpPDATillStartTime.Value = row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTime();
                    dtpPDAFromEndTime.Value = row.Cells[COLS_OTHERDETAILS.FROMENDTIME].Value.ToDateTime();
                    dtpPDATillEndTime.Value = row.Cells[COLS_OTHERDETAILS.TILLENDTIME].Value.ToDateTime();


                    numPDAPeakRate.Value = row.Cells[COLS_OTHERDETAILS.PEAKRATE].Value.ToDecimal();
                    numPDAOffPeakRate.Value = row.Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value.ToDecimal();
                }

            }
        }


        #endregion


        private string pdaMeterPwd = string.Empty;

        private void btnPDAAdd_OtherCharges_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pdaMeterPwd))
            {
                frmLockingPwd frmUnLock = new frmLockingPwd();
                frmUnLock.ShowDialog();

                if (string.IsNullOrEmpty(frmUnLock.ReturnValue1))
                    return;
                else
                    pdaMeterPwd = frmUnLock.ReturnValue1;


                frmUnLock.Dispose();
            }




            AddPDAOtherChargesDetail();
        }

        private void btnPDAClear_OtherChrges_Click(object sender, EventArgs e)
        {
            ClearPDAOtherChargesDetails();
        }

        private void btnAddFromPlot_Click(object sender, EventArgs e)
        {
            int? fromPlotId = ddlFromPlot.SelectedValue.ToIntorNull();
            string msg = string.Empty;
            string fromPlot = ddlFromPlot.Text.ToStr();

            if (fromPlotId == null)
            {
                msg += "Required : From Plot." + Environment.NewLine;

            }

            if (!string.IsNullOrEmpty(msg))
            {
                ENUtils.ShowMessage(msg);
                return;

            }

            AddPlotListBox(lstFromPlot, fromPlotId, fromPlot, ddlToPlot.Tag.ToIntorNull());//, fromLocTypeId, ddlFromLocation.Tag.ToIntorNull());

            ClearFromPlot();
        }
        public void ClearFromPlot()
        {
            ddlFromPlot.SelectedValue = null;
        }
        public void ClearToPlot()
        {
            ddlToPlot.SelectedValue = null;
        }


        private void AddPlotListBox(RadListControl lst, int? FromPlotId, string PlotName, int? index)
        {
            try
            {
                //int? toPlotId = ddlToPlot.SelectedValue.ToIntorNull();

                string msg = string.Empty;
                //string toPlot = ddlToPlot.Text.ToStr();

                if (FromPlotId == null)
                {
                    msg += "Required : To Plot.";

                }
                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;
                }
                string value = FromPlotId.ToStr();

                RadListDataItem item = null;
                if (index == null || index == -1)
                {

                    item = new RadListDataItem();
                    lst.Items.Add(item);
                }
                else
                {
                    item = lst.SelectedItem;

                }


                item.Text = PlotName;
                item.Value = value;

            }
            catch 
            {

            }
        }

        private void btnEditFromPlot_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstFromPlot.SelectedItem == null)
                {
                    ENUtils.ShowMessage("Please select a value to Edit");
                    return;
                }

                RadListDataItem item = lstFromPlot.SelectedItem;


                // ddlFromLocType.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();

                //ddlFromLocation.SelectedValue = item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();

                //ddlFromLocation.Tag = lstFromLocation.SelectedIndex;


                ddlFromPlot.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
                // item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();
                ddlFromPlot.SelectedValue = lstFromPlot.SelectedIndex;
            }
            catch
            {

            }
        }

        private void btnDeleteFromPlot_Click(object sender, EventArgs e)
        {
            DeleteItemFromPlotListBox(lstFromPlot, lstFromPlot.SelectedItem);
        }




        private void DeleteItemFromPlotListBox(RadListControl lst, RadListDataItem item)
        {
            if (lst.Items.Contains(item))
                lst.Items.Remove(item);

        }

        private void btnAddToPlot_Click(object sender, EventArgs e)
        {
            int? toPlotId = ddlToPlot.SelectedValue.ToIntorNull();

            // string msg = string.Empty;
            string toPlot = ddlToPlot.Text.ToStr();


            //if (toPlotId == null)
            //{
            //    msg += "Required : To Plot." + Environment.NewLine;

            //}

            //if (!string.IsNullOrEmpty(msg))
            //{
            //    ENUtils.ShowMessage(msg);
            //    return;

            //}

            AddPlotListBox(lstToPlot, toPlotId, toPlot, ddlToPlot.Tag.ToIntorNull());//, fromLocTypeId, ddlFromLocation.Tag.ToIntorNull());

            ClearToPlot();
        }

        private void btnEditToPlot_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstToPlot.SelectedItem == null)
                {
                    ENUtils.ShowMessage("Please select a value to Edit");
                    return;
                }

                RadListDataItem item = lstToPlot.SelectedItem;


                // ddlFromLocType.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();

                //ddlFromLocation.SelectedValue = item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();

                //ddlFromLocation.Tag = lstFromLocation.SelectedIndex;


                ddlToPlot.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
                // item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();
                ddlToPlot.SelectedValue = lstToPlot.SelectedIndex;
            }
            catch 
            {

            }
        }

        private void btnDeleteToPlot_Click(object sender, EventArgs e)
        {
            DeleteItemFromPlotListBox(lstToPlot, lstToPlot.SelectedItem);
        }
        private void AddPlotWiseDetail()
        {
            try
            {
                decimal fares = spnPlotWiseFare.Value.ToDecimal();

                decimal companyfares = spnPlotWiseCompanyFare.Value.ToDecimal();
                int? fromLocId = ddlFromPlot.SelectedValue.ToIntorNull();
                int? toLocId = ddlToPlot.SelectedValue.ToIntorNull();

                string fromLocation = ddlFromPlot.Text.ToStr();
                string toLocation = ddlToPlot.Text.ToStr();

                string msg = string.Empty;


                if (fromLocId == null && lstFromPlot.Items.Count == 0)
                {
                    msg += "Required : From Plot." + Environment.NewLine;
                }
                if (toLocId == null && lstToPlot.Items.Count == 0)
                {
                    msg += "Required : To Plot." + Environment.NewLine;
                }
                if (fares == 0)
                {
                    msg += "Required : Fare Rate." + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;
                }

                //if (lstFromPlot.Items.Count == 0)
                //{
                //AddPlotListBox(lstFromPlot, fromLocId, fromLocation, ddlToPlot.Tag.ToIntorNull());
                //}
                //if (lstToPlot.Items.Count == 0)
                //{

                //    AddPlotListBox(lstToPlot, toLocId, toLocation, ddlToPlot.Tag.ToIntorNull());
                //}


                //if (lstToPlot.Items.Count == 0)
                //{
                //    ENUtils.ShowMessage("Required : To Plot " + Environment.NewLine + "To Plot Box cannot be Empty");
                //    return;
                //}



                if (lstFromPlot.Items.Count == 0 && lstToPlot.Items.Count == 0)
                {


                    GridViewRowInfo row = grdPlotWiseFare.CurrentRow;

                    if (row == null)
                        row = grdPlotWiseFare.Rows.AddNew();


                    row.Cells[COLS_PLOTEWISE.FromZoneId].Value = fromLocId;
                    row.Cells[COLS_PLOTEWISE.FromPlot].Value = fromLocation.ToStr();

                    row.Cells[COLS_PLOTEWISE.ToZoneId].Value = toLocId;
                    row.Cells[COLS_PLOTEWISE.ToPlot].Value = toLocation.ToStr();

                    row.Cells[COLS_PLOTEWISE.Price].Value = fares;
                    row.Cells[COLS_PLOTEWISE.COMPANYFARE].Value = companyfares;

                }
                else
                {


                    GridViewRowInfo detailRow = null;
                    foreach (RadListDataItem locFrom in lstFromPlot.Items)
                    {
                      
                        fromLocId = locFrom.Value.ToStr().Substring(locFrom.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
                      
                        fromLocation = locFrom.Text.ToStr();

                        foreach (RadListDataItem locTo in lstToPlot.Items)
                        {

                            // if (grdPlotWiseFare.CurrentRow == null)
                            detailRow = grdPlotWiseFare.Rows.AddNew();


                            detailRow.Cells[COLS_PLOTEWISE.FromZoneId].Value = fromLocId;
                            detailRow.Cells[COLS_PLOTEWISE.FromPlot].Value = fromLocation;

                            detailRow.Cells[COLS_PLOTEWISE.ToZoneId].Value = locTo.Value.ToStr().Substring(locTo.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
                            detailRow.Cells[COLS_PLOTEWISE.ToPlot].Value = locTo.Text.ToStr();

                            detailRow.Cells[COLS_PLOTEWISE.Price].Value = fares;
                            detailRow.Cells[COLS_PLOTEWISE.COMPANYFARE].Value = companyfares;
                        }
                    }
                }
                ClearPlotDetails();

                grdPlotWiseFare.CurrentRow = null;
            }
            catch 
            {

            }

        }
        public void ClearPlotDetails()
        {
            btnAddFromPlot.Enabled = true;
            btnAddToPlot.Enabled = true;

            lstFromPlot.Enabled = true;
            lstToPlot.Enabled = true;
            ClearFromPlot();
            ClearToPlot();
            ddlFromPlot.Tag = null;
            ddlToPlot.Tag = null;

            lstFromPlot.Items.Clear();
            lstToPlot.Items.Clear();
            spnPlotWiseFare.Value = 0;
            spnPlotWiseCompanyFare.Value = 0;
            grdPlotWiseFare.CurrentRow = null;
            ddlFromPlot.Focus();


        }

        private void btnAddPlotWiseFare_Click(object sender, EventArgs e)
        {
            AddPlotWiseDetail();
        }

        private void btnClearFare_Click(object sender, EventArgs e)
        {

            ClearPlotDetails();
        }


        AutoCompleteTextBox aTxt = null;
        bool IsKeyword = false;

   

        private void ShowAddresses()
        {
            int sno = 1;



            var finalList = (from a in AppVars.zonesList
                             from b in res
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && b[b.IndexOf(a) - 1] == ' ' && GeneralBLL.GetHalfPostCodeMatch(b) == a)




                             select b).ToArray<string>();


            if (finalList.Count() > 0)
            {
                finalList = finalList.Union(res).ToArray<string>();

                //  finalList = finalList.OrderBy(c=>AppVars.zonesList. c== AppVars.zonesList) c =.Union(res).ToArray<string>();

            }
            else
                finalList = res;


            if (finalList.Count() < 10)
            {

                finalList = finalList.Select(args => (sno++) + ". " + args).ToArray();

            }



            aTxt.ListBoxElement.Items.Clear();
            aTxt.ListBoxElement.Items.AddRange(finalList);


            if (aTxt.ListBoxElement.Items.Count == 0)
                aTxt.ResetListBox();
            else
            {

                //  aTxt.ListBoxElement.Visible = true;

                aTxt.ShowListBox();


            }

            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }
        }



        string[] res = null;
        string searchTxt = "";

        private void frmFares_Load_1(object sender, EventArgs e)
        {

        }

        private void pnlPercentage_Paint(object sender, PaintEventArgs e)
        {

        }


        #region TextChanged

        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {


            try
            {

                // IsKeyword = false;

                InitializeTimer();
                timer1.Stop();

                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

                //if (aTxt.Name == "txtFromAddress")
                //    txtToAddress.SendToBack();

                //else if (aTxt.Name == "txtToAddress")
                //    txtToAddress.BringToFront();



                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                {

                    InitializeSearchPOIWorker();

                    if (POIWorker.IsBusy)
                    {
                        POIWorker.CancelAsync();

                        POIWorker.Dispose();
                        POIWorker = null;
                        GC.Collect();
                        InitializeSearchPOIWorker();

                    }


                    AddressTextChangePOI();
                }
                else
                {

                    AddressTextChangeWOPOI();
                }
            }
            catch (Exception ex)
            {

            }
        }

        BackgroundWorker POIWorker = null;
        private void InitializeSearchPOIWorker()
        {
            if (POIWorker == null)
            {
                POIWorker = new BackgroundWorker();
                POIWorker.WorkerSupportsCancellation = true;
                POIWorker.DoWork += new DoWorkEventHandler(POIWorker_DoWork);
                POIWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(POIWorker_RunWorkerCompleted);
            }



        }

        void POIWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null || e.Cancelled || (sender as BackgroundWorker) == null)
                return;

            try
            {


                ShowAddressesPOI((string[])e.Result);

            }
            catch
            {


            }
        }

        void POIWorker_DoWork(object sender, DoWorkEventArgs e)
        {


            string searchValue = e.Argument.ToStr();
            try
            {
                if (POIWorker == null)
                {
                    e.Cancel = true;
                    return;


                }

                //   Console.WriteLine("Start work : " + searchValue);

                string postCode = General.GetPostCodeMatchOpt(searchValue);

                string doorNo = string.Empty;
                string place = string.Empty;

                if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                    postCode = string.Empty;

                string street = searchValue;

                if (postCode.Length > 0)
                {
                    street = street.Replace(postCode, "").Trim();
                }


                if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(postCode) && street.IsAlpha() == false && street.Length < 4 && searchValue.IndexOf(postCode) < searchValue.IndexOf(street))
                {
                    street = "";
                    postCode = searchTxt;
                }


                if (street.Length > 0)
                {

                    if (char.IsNumber(street[0]))
                    {

                        for (int i = 0; i <= 3; i++)
                        {
                            if (char.IsNumber(street[i]) || (doorNo.Length > 0 && doorNo.Length == i && char.IsLetter(street[i])))
                                doorNo += street[i];
                            else
                                break;
                        }
                    }
                }


                if (street.EndsWith("#"))
                {
                    street = street.Replace("#", "").Trim();
                    place = "p=";
                }

                if (doorNo.Length > 0 && place.Length == 0)
                {
                    street = street.Replace(doorNo, "").Trim();


                }


                if (postCode.Length == 0 && street.Length < 3)
                {
                    e.Cancel = true;
                    return;

                }


                if (street.Length > 1 || postCode.Length > 0)
                {
                    if (postCode.Length > 0)
                    {
                        if (doorNo.Length > 0 && postCode == General.GetPostCodeMatch(postCode))
                        {
                            doorNo = string.Empty;
                        }

                    }

                    if (postCode.Length >= 5 && postCode.Contains(" ") == false)
                    {
                        string resultPostCode = AppVars.listOfAddress.FirstOrDefault(a => a.PostalCode.Strip(' ') == postCode).DefaultIfEmpty().PostalCode.ToStr().Trim();


                        if (resultPostCode.Length >= 5 && resultPostCode.Contains(" "))
                        {
                            postCode = resultPostCode;

                        }

                    }


                    if (POIWorker == null || POIWorker.CancellationPending || ((sender as BackgroundWorker) == null || (sender as BackgroundWorker).CancellationPending))
                    {
                        e.Cancel = true;
                        return;
                    }



                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        e.Result = db.stp_GetByRoadLevelData(postCode, doorNo, street, place).Select(c => c.AddressLine1).ToArray<string>();

                    }

                    if (POIWorker == null || POIWorker.CancellationPending || ((sender as BackgroundWorker) == null || (sender as BackgroundWorker).CancellationPending))
                    {
                        e.Cancel = true;
                        return;
                    }




                    //   Console.WriteLine("end work : " + searchValue);

                }
            }
            catch
            {
                //     Console.WriteLine("Start work catch: " + searchValue);

            }
        }



        private void AddressTextChangeWOPOI()
        {
            string text = aTxt.Text;
            string doorNo = string.Empty;

            if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool())
            {
                if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower())
                {
                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();


                    if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                    {

                        aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                    }

                    aTxt.SelectedItem = aTxt.Text.Trim();
                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    //    }               

                }

            }

            if (text.Length > 2 && text.EndsWith(".") == false && text.EndsWith(",") == false)
            {

                if (aTxt.SelectedItem == null || (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() != aTxt.Text.ToLower()))
                {


                    for (int i = 0; i <= 2; i++)
                    {
                        if (char.IsNumber(text[i]))
                            doorNo += text[i];
                        else
                            break;

                    }
                    text = text.Remove(text.IndexOf(doorNo), doorNo.Length).TrimStart(new char[] { ' ' });
                }
            }



            if (text.Length > 1 && text != "BASX")
            {
                if (text.EndsWith("   "))
                {
                    //if (aTxt.Name == "txtFromAddress")
                    //{
                    //    FocusOnPickupPlot();
                    //}
                    //else if (aTxt.Name == "txtToAddress")
                    //{
                    //    FocusOnDropOffPlot();
                    //}

                    return;

                }

                else if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
                {
                    aTxt.ListBoxElement.Items.Clear();

                    aTxt.ResetListBox();

                    string locName = aTxt.SelectedItem.ToLower();
                    int commaIndex = aTxt.SelectedItem.LastIndexOf(',');
                    if (commaIndex != -1)
                    {
                        locName = locName.Remove(commaIndex);
                    }


                    string formerValue = aTxt.FormerValue.ToLower().Trim();

                    int? loctypeId = 0;
                    Gen_Location loc = null;
                    if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                    || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 3 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                    {


                        if (aTxt.FormerValue.EndsWith("  ") || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 2 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                        {
                            loc = General.GetObject<Gen_Location>(c => c.LocationName.ToLower() == locName);
                        }
                        else
                            loc = General.GetObject<Gen_Location>(c => c.ShortCutKey == formerValue && c.LocationName.ToLower() == locName);

                        if (loc != null)
                        {
                            loctypeId = loc.LocationTypeId;
                        }
                    }

                    if (loctypeId != 0)
                    {

                        if (aTxt.Name == "txtFromAddress")
                        {



                            if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                            {
                                aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                aTxt.Text = aTxt.Text.Trim();
                                aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                                //if (aTxt.Name == "txtFromAddress")
                                //{
                                //    SetPickupZone(aTxt.Text);

                                //    UpdateAutoCalculateFares();
                                //}


                            }


                        }
                        else if (aTxt.Name == "txtToAddress")
                        {


                            if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                            {
                                aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                aTxt.Text = aTxt.Text.Trim();
                                aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                                //  SetDropOffZone(aTxt.Text);
                                //  UpdateAutoCalculateFares();


                            }




                        }
                    }
                    else if (aTxt.Text.Contains('.'))
                    {

                        //   RemoveNumbering(doorNo);


                    }
                    else if (!string.IsNullOrEmpty(doorNo))
                    {
                        aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        aTxt.Text = doorNo + " " + aTxt.Text;
                        aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    }
                    //else
                    //{
                    //    if (aTxt.Name == "txtFromAddress")
                    //    {


                    //        SetPickupZone(aTxt.SelectedItem);


                    //    }

                    //    else if (aTxt.Name == "txtToAddress")
                    //    {
                    //        SetDropOffZone(aTxt.SelectedItem);


                    //    }

                    //    if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                    //    {
                    //        UpdateAutoCalculateFares();

                    //    }


                    //}

                    aTxt.FormerValue = string.Empty;


                    return;
                }

              

                text = text.ToLower();

               

              

                StartAddressTimer(text);

            }
          
            else
            {
               
                aTxt.ResetListBox();
                //  aTxt.ListBoxElement.Visible = false;
                aTxt.ListBoxElement.Items.Clear();

                //   CancelWebClientAsync();
                //  wc.CancelAsync();
                aTxt.Values = null;

            }



        }

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (aTxt == null)
                {

                    timer1.Stop();
                    return;
                }

                timer1.Stop();

                searchTxt = searchTxt.ToUpper();


                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                {

                    if (POIWorker.IsBusy)
                        POIWorker.CancelAsync();



                    POIWorker.RunWorkerAsync(searchTxt);
                }
                else
                {

                    PerformAddressChangeTimerWOPOI();
                }


            }
            catch (Exception ex)
            {


            }

        }


        private void StartAddressTimer(string text)
        {
            text = text.ToLower();
            searchTxt = text;
            InitializeTimer();
            timer1.Start();
        }

        private void InitializeTimer()
        {
            if (this.timer1 == null)
            {
                this.timer1 = new System.Windows.Forms.Timer();
                this.timer1.Tick += timer1_Tick;
                this.timer1.Interval = 500;
            }

        }








        private void AddressTextChangePOI()
        {
            string text = aTxt.Text;
            string doorNo = string.Empty;

            if (aTxt.SelectedItem != null && aTxt.ListBoxElement.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower()
               && aTxt.Text.Length > 0)
            {
                aTxt.TextChanged -= TextBoxElement_TextChanged;
                //  aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();

                if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                {
                    aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                }

                aTxt.SelectedItem = aTxt.Text.Trim();
                aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            }




            for (int i = 0; i <= 2; i++)
            {
                if (char.IsNumber(text[i]))
                    doorNo += text[i];
                else
                    break;
            }





            if (text.Length > 1 && text != "BASX")
            {
                if (text.EndsWith("   "))
                {
                    //if (aTxt.Name == "txtFromAddress")
                    //{
                    //    FocusOnPickupPlot();
                    //}
                    //else if (aTxt.Name == "txtToAddress")
                    //{
                    //    FocusOnDropOffPlot();
                    //}
                    //return;
                }


                else if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
                {
                    aTxt.ListBoxElement.Items.Clear();
                    aTxt.ResetListBox();

                    string locName = aTxt.SelectedItem.ToLower();
                    int commaIndex = aTxt.SelectedItem.LastIndexOf(',');
                    if (commaIndex != -1)
                    {
                        locName = locName.Remove(commaIndex);
                    }


                    string formerValue = aTxt.FormerValue.ToLower().Trim();





                    //else if (!string.IsNullOrEmpty(doorNo))
                    //{
                    //    aTxt.TextChanged -= TextBoxElement_TextChanged;
                    //    aTxt.Text = aTxt.Text;
                    //    aTxt.TextChanged += TextBoxElement_TextChanged;
                    //}
                    //else
                    //{
                    //    if (aTxt.Name == "txtFromAddress")
                    //    {
                    //        SetPickupZone(aTxt.SelectedItem);
                    //    }

                    //    else if (aTxt.Name == "txtToAddress")
                    //    {
                    //        SetDropOffZone(aTxt.SelectedItem);

                    //    }

                    //    if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                    //    {
                    //        UpdateAutoCalculateFares();
                    //    }
                    //}

                    aTxt.FormerValue = string.Empty;
                    return;
                }

               
                text = text.ToLower();              

                StartAddressTimer(text);

            }
           
            else
            {
                //if (MapType == Enums.MAP_TYPE.NONE)
                //    return;
                aTxt.ResetListBox();
                aTxt.ListBoxElement.Items.Clear();
                aTxt.Values = null;

            }



        }






        private void ShowAddressesPOI(string[] resValue)
        {
            int sno = 1;

            // var finalList = resValue;



            var finalList = (from a in AppVars.zonesList
                             from b in resValue
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && (b.IndexOf(a) - 1) >= 0 && b[b.IndexOf(a) - 1] == ' ' && GeneralBLL.GetHalfPostCodeMatch(b) == a)

                             select b).ToArray<string>();


            if (finalList.Count() > 0)
            {
                finalList = finalList.Union(resValue).ToArray<string>();

            }
            else
                finalList = resValue;



            if (finalList.Count() < 10)
            {
                finalList = finalList.Select(args => (sno++) + ". " + args).ToArray();
            }


            aTxt.ListBoxElement.Items.Clear();
            aTxt.ListBoxElement.Items.AddRange(finalList);


            if (aTxt.ListBoxElement.Items.Count == 0)
                aTxt.ResetListBox();
            else
            {


                aTxt.ShowListBox();


            }

            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }
        }




        private void PerformAddressChangeTimerWOPOI()
        {

            string postCode = General.GetPostCodeMatch(searchTxt);
            string fullPostCode = postCode;


            if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                postCode = string.Empty;


            string street = searchTxt;



            int IsAsc = 0;
            if (!string.IsNullOrEmpty(postCode))
            {
                street = street.Replace(postCode, "").Trim();

                if (postCode.Contains(' ') == false)
                {
                    if (postCode.Length == 3 && Char.IsNumber(postCode[2]))
                    {

                        IsAsc = 1;
                    }
                    else if (postCode.Length == 2 && Char.IsNumber(postCode[1]))
                    {

                        IsAsc = 1;
                    }
                    else if (postCode.Length > 3 && Char.IsNumber(postCode[3]))
                    {

                        IsAsc = 2;
                    }


                }

            }


            if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(postCode) && street.IsAlpha() == false && street.Length < 4 && searchTxt.IndexOf(postCode) < searchTxt.IndexOf(street))
            {
                street = "";
                postCode = searchTxt;
            }


            if (IsAsc == 1)
            {



                if (!string.IsNullOrEmpty(street))
                {


                    res = (from a in AppVars.listOfAddress

                           where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                           orderby a.PostalCode

                           select a.AddressLine1

                                   ).Take(1000).ToArray<string>();

                }
                else
                {

                    res = (from a in AppVars.listOfAddress

                           where a.PostalCode.StartsWith(postCode)

                           orderby a.PostalCode

                           select a.AddressLine1

                         ).Take(600).ToArray<string>();
                }

            }
            else if (IsAsc == 2)
            {


                res = (from a in AppVars.listOfAddress

                       where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                       orderby a.PostalCode descending

                       select a.AddressLine1

                               ).Take(500).ToArray<string>();


                if (street.Contains(' ') && res.Count() == 0)
                {

                    string[] vals = street.Split(' ');
                    int valCnt = vals.Count();

                    res = (from a in AppVars.listOfAddress

                           where (vals.Count(c => a.AddressLine1.Contains(c)) == valCnt)

                           select a.AddressLine1

                         ).Take(30).ToArray<string>();


                }


            }
            else
            {

                if (postCode.Contains(' '))
                {

                    res = null;

                    if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool()
                        && AppVars.zonesList.Count() > 0
                        && fullPostCode.Length > 0)
                    {

                        fullPostCode = General.GetPostCodeMatch(fullPostCode);

                        if (fullPostCode.Length > 0 && searchTxt.Trim() == fullPostCode)
                        {


                            string[] res1 = (from a in AppVars.listOfAddress

                                             where a.PostalCode == postCode

                                             select a.AddressLine1

                                       ).Take(1).ToArray<string>();





                            res = (from a in new TaxiDataContext().stp_GetRoadLevelData(fullPostCode)
                                   select a.AddressLine1).ToArray<string>();


                            res = res1.Union(res).Distinct().ToArray<string>();


                        }


                        if (res.Count() == 0)
                        {
                            res = (from a in AppVars.listOfAddress

                                   where a.PostalCode.StartsWith(postCode)

                                   orderby a.PostalCode

                                   select a.AddressLine1

                                  ).Take(100).ToArray<string>();


                        }




                    }
                    else
                    {

                        res = (from a in AppVars.listOfAddress

                               where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))

                               select a.AddressLine1

                                  ).Take(500).ToArray<string>();
                    }
                }
                else
                {


                    if (street.Length == 3 && street.IsAlpha() && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.CountyString))
                    {


                        string[] areas = AppVars.objPolicyConfiguration.CountyString.Split(',');

                        string last = street[2].ToStr();
                        street = street.Remove(2);

                        res = (from b in AppVars.listOfAddress.Where(a => areas.Any(c => a.AddressLine1.Contains(c)) && a.AddressLine1.Split(' ').Count() > 5)
                               //  let x = (areas.Any(c => b.Address.Contains(c)) ? b.Address.Split(' ') : null)
                               let x = b.AddressLine1.Split(' ')
                               where

                                  (

                               (x.ElementAt(0).StartsWith(street) && x.ElementAt(1).StartsWith(last))
                            || (x.ElementAt(0).StartsWith(street) && areas.Contains(x.ElementAt(2)) == false && x.ElementAt(2).StartsWith(last))
                                )

                               select b.AddressLine1

                                  ).Take(200).ToArray<string>();



                    }
                    else
                    {


                        if (street.WordCount() == 1 && street.ContainsNoSpaces())
                        {
                            //  street = street + " ";




                            if (AppVars.zonesList.Count() == 0)
                            {
                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.StartsWith(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                                       select a.AddressLine1

                                ).Take(500).ToArray<string>();
                            }
                            else
                            {
                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.StartsWith(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                                       select a.AddressLine1

                               ).Take(100).ToArray<string>();

                            }


                            if (AppVars.zonesList.Count() > 0)
                            {

                                string[] res2 = (from a in AppVars.listOfAddress

                                                 where (a.AddressLine1.StartsWith(street))
                                                 && AppVars.zonesList.Count(c => a.PostalCode.StartsWith(c)) > 0
                                                 select a.AddressLine1

                                    ).Take(200).ToArray<string>();

                                res = res2.Union(res).Distinct().ToArray<string>();


                            }










                        }
                        else
                        {



                            if (AppVars.zonesList.Count() > 0)
                            {




                                if (postCode.Length == 0)
                                {

                                    res = (from a in AppVars.listOfAddress


                                           where

                                           (a.AddressLine1.StartsWith(street))
                                           select a.AddressLine1

                                       ).Take(500).ToArray<string>();
                                }
                                else
                                {
                                    res = (from a in AppVars.listOfAddress


                                           where

                                           ((a.AddressLine1.StartsWith(street))
                                        && ((a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                                           select a.AddressLine1

                                      ).Take(500).ToArray<string>();


                                }


                                res = res.Union((from a in AppVars.listOfAddress


                                                 where

                                                 (a.AddressLine1.Contains(street)
                                                 && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))




                                                 select a.AddressLine1

                                   ).Take(2000)
                                     ).Distinct().ToArray<string>();




                            }
                            else
                            {

                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))



                                       select a.AddressLine1

                                    ).Take(1000).ToArray<string>();
                            }

                        }






                    }



                    if (street.Contains(' ') && res.Count() == 0)
                    {



                        string[] vals = street.Split(' ');
                        int valCnt = vals.Count();


                        res = (from a in AppVars.listOfAddress

                               where (vals.Count(c => a.AddressLine1.Contains(c)) == valCnt)



                               select a.AddressLine1

                             ).Take(30).ToArray<string>();


                    }



                }



            }

            ShowAddresses();

        }



        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == '1' || e.KeyChar == '2' || e.KeyChar == '3' || e.KeyChar == '4'
                    || e.KeyChar == '5' || e.KeyChar == '6' || e.KeyChar == '7'
                    || e.KeyChar == '8' || e.KeyChar == '9')
                {




                    AutoCompleteTextBox txtData = (AutoCompleteTextBox)sender;
                    if (txtData.Text.StartsWith("W1"))
                        return;



                    if (txtData.Text.Length > 4 && txtData.ListBoxElement.Visible == true && txtData.ListBoxElement.Items.Count < 10)
                    {
                        string idx = e.KeyChar.ToStr();

                        txtData.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        string item = txtData.ListBoxElement.Items[idx.ToInt() - 1].ToStr();

                        string doorNo = string.Empty;
                        for (int i = 0; i <= 2; i++)
                        {
                            if (char.IsNumber(txtData.FormerValue[i]))
                                doorNo += txtData.FormerValue[i];
                            else
                                break;

                        }


                        if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool())
                        {
                            txtData.Text = (item.Remove(0, item.IndexOf('.') + 1).Trim()).Trim();
                        }
                        else
                        {

                            txtData.Text = (doorNo + " " + item.Remove(0, item.IndexOf('.') + 1).Trim()).Trim();
                        }


                        //if (txtData.Name == "txtFromAddress")
                        //{
                        //    SetPickupZone(txtData.Text);
                        //    FocusOnFromDoor();
                        //}
                        //else if (txtData.Name == "txtToAddress")
                        //{
                        //    SetDropOffZone(txtData.Text);
                        //    FocusOnToDoor();
                        //}
                        //else if (txtData.Name == "txtViaAddress")
                        //{
                        //    txtData.ResetListBox();
                        //    AddViaPoint();

                        //}
                        txtData.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        e.Handled = true;

                        aTxt.ResetListBox();
                        aTxt.ListBoxElement.Items.Clear();


                        //   UpdateAutoCalculateFares();
                        //   txtViaAddress.Focus();
                    }



                }
            }
            catch (Exception ex)
            {


            }
        }


        #endregion
       
        //  bool IsPOI = false;


    }
}
