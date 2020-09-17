using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Taxi_BLL;
using DAL;
using System.Data.SqlClient;
using Telerik.WinControls;
using System.Diagnostics;
using System.IO;

namespace Taxi_AppMain
{
    public partial class frmFaresList : UI.SetupBase
    {
        string ClientName = string.Empty;
     //   int FareID = 0;
        FareBO objMaster;
        List<Gen_Company> globalcompanyidfilter = new List<Gen_Company>();
        List<Gen_SubCompany> globalsubcompanyidfilter = new List<Gen_SubCompany>();
        public struct COLS_LIST
        {
            public static string ID = "Id";
          
            public static string VEHICLENO = "Vehicle Type";
            public static string COMPANY = "Company";      


        }


        public struct COLS_DETAILS
        {
           
            public static string FROMLOCATION = "FromLocation";
            public static string TOLOCATION = "ToLocation";
            public static string FARE = "Fare";
            public static string COMPANYFARE = "CompanyFare";

        }

        public struct COLS_OTHERDETAILS
        {
            public static string ID = "ID";
            public static string FAREID = "FAREID";


            public static string FROMMILE = "FromMile";
            public static string TOMILE = "ToMile";
            public static string FARE = "Fare";



            public static string PEAKTIME = "Peak";
            public static string PEAKRATE = "PeakRate";


            public static string OFFPEAKTIME = "OffPeak";
            public static string OFFPEAKRATE = "OffPeakRate";



            public static string FROMSTARTTIME = "FromStartTime";
            public static string TILLSTARTTIME = "TillStartTime";


            public static string FROMENDTIME = "FromEndTime";
            public static string TILLENDTIME = "TillEndTime";

            public static string COMPANYFARE = "CompanyFare";
        }

        RadDropDownMenu EditFare = null;
        public frmFaresList()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmFaresList_Load);
          

            this.grdLister.RowsChanging += new GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            this.Shown += new EventHandler(frmFaresList_Shown);
            
           
            //btn_ExportWeb.Visible = AppVars.objPolicyConfiguration.EnableWebBooking.ToBool();

            //if (AppVars.objPolicyConfiguration.PDANewWeekMessageByDay.ToStr() != "new")
            //{
                btn_ExportWeb.Visible = false;

          //  }
           
            




            //grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);

            

            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool())
            {
                pg_PDAMeter.Item.Visibility = ElementVisibility.Visible;

            }
            else
            {
                pg_PDAMeter.Item.Visibility = ElementVisibility.Collapsed;


            }


            //if (AppVars.listUserRights.Count(c => c.formName == "frmFares" && c.functionId == "DISABLE FARE LIST") == 1)
            //{
            //    this.pg_FareList.Item.Visibility = ElementVisibility.Collapsed;
            //}
          




            if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool()==false)
            {
                this.pg_PlotWiseFareList.Item.Visibility = ElementVisibility.Collapsed;
            }
            else
            {
                radPageView1.SelectedPage = pg_PlotWiseFareList;
                this.pg_FareList.Item.Visibility = ElementVisibility.Visible;
            }



            if (AppVars.listUserRights.Count(c => c.formName == "frmFares" && c.functionId.Contains("EDIT")) > 0)
            {
                grdDetails.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdDetails_ContextMenuOpening);
                this.grdLister.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdLister_CellDoubleClick);

            }
            else
            {
                this.btnNewFare.Visible = false;
                this.btnAddFare.Visible = false;
                this.btnCopyCompanyFares.Visible = false;
                this.btnCopyFare.Visible = false;
                this.btnViewDetails.Visible = false;
                this.btnCopySubCompanyFares.Visible = false;
            }
              
        }

        void grdDetails_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                //if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId.Contains("Edit")) == 0)
                //{
                    GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                    if (cell == null)
                        return;

                    else if (cell.GridControl.Name == "grdDetails")
                    {

                        if (EditFare == null)
                        {
                      
                            EditFare = new RadDropDownMenu();
                            EditFare.BackColor = Color.Orange;

                            //RadMenuItem EditFareItem1 = new RadMenuItem("Edit Fare");
                            //EditFareItem1.ForeColor = Color.DarkBlue;
                            //EditFareItem1.BackColor = Color.Orange;
                            //EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                            ////EditFareItem1.Click += new EventHandler(EditFareItem1_Click);
                            //EditFareItem1.Click += new EventHandler(EditFareItem1_Click);
                            //EditFare.Items.Add(EditFareItem1);


                            //RadMenuItem EditFareItem2 = new RadMenuItem("Arrival Text");
                            //EditFareItem2.ForeColor = Color.DarkBlue;
                            //EditFareItem2.BackColor = Color.Orange;
                            //EditFareItem2.Font = new Font("Tahoma", 10, FontStyle.Bold);
                            //EditFareItem2.Click += new EventHandler(EditFareItem2_Click);
                            //EditFare.Items.Add(EditFareItem2);

                            //RadMenuItem EditFareItem3 = new RadMenuItem("Copy Booking");
                            //EditFareItem3.ForeColor = Color.DarkBlue;
                            //EditFareItem3.BackColor = Color.Orange;
                            //EditFareItem3.Font = new Font("Tahoma", 10, FontStyle.Bold);
                            //EditFareItem3.Click += new EventHandler(EditFareItem3_Click);
                            //EditFare.Items.Add(EditFareItem3);
                        }

                        e.ContextMenu = EditFare;
                        return;
                    }
                //}
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void EditFareItem1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (grdDetails.CurrentRow != null && grdDetails.CurrentRow is GridViewRowInfo)
            //    {
            //        long FareId = grdDetails.CurrentRow.Cells["Id"].Value.ToLong();
            //        string FromAddres = grdDetails.CurrentRow.Cells["FromLocation"].Value.ToStr();
            //        string Toaddress = grdDetails.CurrentRow.Cells["ToLocation"].Value.ToStr();
            //        decimal Price = grdDetails.CurrentRow.Cells["FARE"].Value.ToDecimal();
            //        frmEditFarePrice frm = new frmEditFarePrice(FareId,FromAddres,Toaddress,Price,1);
            //        frm.ShowDialog();


            //        frm.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowMessage(ex.Message);
            //}
        }


        void EditFareItemPlot_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (grdPlotWiseFare.CurrentRow != null && grdPlotWiseFare.CurrentRow is GridViewRowInfo)
            //    {
            //        long FareId = grdPlotWiseFare.CurrentRow.Cells["Id"].Value.ToLong();
            //        string FromAddres =grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.FromPlotNo].Value.ToStr() + "."+ grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.FromPlot].Value.ToStr();
            //        string Toaddress = grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.ToPlotNo].Value.ToStr() + "." +  grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.ToPlot].Value.ToStr();
            //        decimal Price = grdPlotWiseFare.CurrentRow.Cells[COLS_PLOTEWISE.Price].Value.ToDecimal();
            //        frmEditFarePrice frm = new frmEditFarePrice(FareId, FromAddres, Toaddress, Price,2);
            //        frm.ShowDialog();


            //        frm.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowMessage(ex.Message);
            //}
        }

        void frmFaresList_Shown(object sender, EventArgs e)
        {
            FormatPlotWiseGrid();
            FormatDetailsGrid();
            FormatMilesGrid();
            FormatPDAMilesGrid();
            FormatListGrid();
            //if (AppVars.listUserRights.Count(c => c.formName == "frmFares" && c.functionId == "DISABLE FARE LIST") == 1)
            //{
            //    this.pg_FareList.Item.Visibility = ElementVisibility.Collapsed;
            //}

            //if (AppVars.listUserRights.Count(c => c.formName == "frmFares" && c.functionId == "DISABLE PLOT WISE FARE LIST") == 1)
            //{
            //    this.pg_PlotWiseFareList.Item.Visibility = ElementVisibility.Collapsed;
            //}
           

          
        }

        void frmFaresList_Load(object sender, EventArgs e)
        {

            //FormatListGrid();
            //FormatDetailsGrid();

            //ViewFareDetails();


        }


        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
            
              
                this.InitializeForm("frmFares");

                if (this.CanDelete == false)
                {
                    ENUtils.ShowMessage("Permission Denied");
                    e.Cancel = true;
                }
                else
                {
                    objMaster = new FareBO();

                    try
                    {

                        objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                        objMaster.Delete(objMaster.Current);


                      
                       
                    }
                    catch (Exception ex)
                    {
                        if (objMaster.Errors.Count > 0)
                            ENUtils.ShowMessage(objMaster.ShowErrors());
                        else
                        {
                            ENUtils.ShowMessage(ex.Message);

                        }
                        e.Cancel = true;

                    }
                }

            }
        }


        public override void RefreshData()
        {
            PopulateData();
        }

     

     

        private void btnNewFare_Click(object sender, EventArgs e)
        {
            ShowFareForm(0);
        }

        private void ShowFareForm(int id)
        {
            frmFares frm = new frmFares();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();


        }



        private void FormatListGrid()
        {

            try
            {
                this.InitializeForm("frmFares");
            }
            catch
            {


            }

        
            grdLister.ShowRowHeaderColumn = false;
            grdLister.AutoCellFormatting = true;
            grdLister.EnableHotTracking = false;
            grdLister.ShowGroupPanel = false;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;


            PopulateData();

          
         
            UI.GridFunctions.SetFilter(grdLister);


            grdLister.Columns["Id"].IsVisible = false;

            //grdLister.Columns["EffectiveDate"].Width = 110;
            //grdLister.Columns["EffectiveDate"].HeaderText = "Effective Date";

            //(grdLister.Columns["EffectiveDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            //(grdLister.Columns["EffectiveDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";


      //      grdLister.Columns["ColDelete"].Width = 70;
            //grdLister.Columns["VehicleType"].HeaderText = "Vehicle Type";
            //grdLister.Columns["VehicleType"].Width = 130;
            //grdLister.Columns["Company"].Width = 220;                
        }

        public override void PopulateData()
        {

            List<stp_GetFareListResult> data1=null;

            using (TaxiDataContext db = new TaxiDataContext())
            {
                data1 = db.stp_GetFareList().ToList();
            }

            

            //var data1 = General.GetQueryable<Fare>(null);


            //var query = from a in data1

            //            //orderby a.EffectiveDate descending

            //            select new
            //            {
            //                Id = a.Id,
            //              //  EffectiveDate = a.EffectiveDate,
            //                VehicleType = a.Fleet_VehicleType.VehicleType,
            //                CompanyId=a.CompanyId,
            //                Company = a.Gen_Company!=null? a.Gen_Company.CompanyName:"",
            //                VehicleTypeId=a.Fleet_VehicleType.Id

            //            };



            //grdLister.DataSource = query.ToList();

            grdLister.DataSource = data1;


            if (grdLister.Columns["ColDelete"] == null)
            {
                if (this.CanDelete)
                {
                    UI.GridFunctions.AddDeleteColumn(grdLister);
                }


            }

            if(this.CanDelete && grdLister.Columns["ColDelete"]!=null)
                grdLister.Columns["ColDelete"].Width = 70;

          //  grdLister.Columns["ColDelete"].Width = 70;
            grdLister.Columns["VehicleType"].HeaderText = "Vehicle Type";
            grdLister.Columns["VehicleType"].Width = 110;
            grdLister.Columns["Company"].Width = 140;
            grdLister.Columns["VehicleTypeId"].IsVisible = false;
            grdLister.Columns["CompanyId"].IsVisible = false;
            grdLister.Columns["SubCompanyId"].IsVisible = false;
            grdLister.Columns["SubCompanyName"].HeaderText = "SubCompany";
            grdLister.Columns["SubCompanyName"].Width = 140;
            ViewFareDetails();


           

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
            public static string CompanyPrice = "CompanyPrice";
        }
        public void FormatPlotWiseGrid()
        {
            grdPlotWiseFare.AllowAddNewRow = false;
            grdPlotWiseFare.AutoCellFormatting = true;
            grdPlotWiseFare.ShowGroupPanel = false;
            grdPlotWiseFare.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdPlotWiseFare_ContextMenuOpening);
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
            dcol.Width = 90;
            dcol.ReadOnly = true;
            grdPlotWiseFare.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_PLOTEWISE.CompanyPrice;
            dcol.HeaderText = "Company Price (£)";
            dcol.Width = 140;
            dcol.ReadOnly = true;
            dcol.IsVisible = false;
            grdPlotWiseFare.Columns.Add(dcol);

            grdPlotWiseFare.MasterTemplate.ShowRowHeaderColumn = false;

            UI.GridFunctions.SetFilter(grdPlotWiseFare);

            //UI.GridFunctions.AddDeleteColumn(grdPlotWiseFare);
            //grdPlotWiseFare.Columns["ColDelete"].Width = 80;
        }

        RadDropDownMenu editPlotFare = null;

        void grdPlotWiseFare_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (editPlotFare == null)
            {
                editPlotFare = new RadDropDownMenu();
                editPlotFare.BackColor = Color.Orange;

                RadMenuItem EditFareItem1 = new RadMenuItem("Edit Fare");
                EditFareItem1.ForeColor = Color.DarkBlue;
                EditFareItem1.BackColor = Color.Orange;
                EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                EditFareItem1.Click += new EventHandler(EditFareItemPlot_Click);
                editPlotFare.Items.Add(EditFareItem1);
            }

            e.ContextMenu = editPlotFare;
        }

        private void FormatDetailsGrid()
        {
            grdDetails.AllowAddNewRow = false;
            grdDetails.AllowEditRow = false;
            grdDetails.AutoCellFormatting = true;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = "From Location";
            col.Name = COLS_DETAILS.FROMLOCATION;
            col.Width = 130;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "To Location";
            col.Name = COLS_DETAILS.TOLOCATION;
            col.Width = 160;
            grdDetails.Columns.Add(col);



            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText ="Fare £";
            colDec.Width = 120;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_DETAILS.FARE;
            grdDetails.Columns.Add(colDec);

            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Company Fare £";
            colDec.Width = 140;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_DETAILS.COMPANYFARE;
            grdDetails.Columns.Add(colDec);

            grdDetails.MasterTemplate.ShowRowHeaderColumn = false;

            UI.GridFunctions.SetFilter(grdDetails);

       
        }

        private void FormatMilesGrid()
        {
            grdMiles.AllowAddNewRow = false;
            grdMiles.AllowEditRow = false;
            grdMiles.AutoCellFormatting = true;
            grdMiles.ShowGroupPanel = false;


            GridViewDecimalColumn colDec = null;



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "From Mile";
            colDec.Name = COLS_OTHERDETAILS.FROMMILE;
            colDec.Width = 120;
            colDec.DecimalPlaces = 2;
            grdMiles.Columns.Add(colDec);

            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "To Mile";
            colDec.Name = COLS_OTHERDETAILS.TOMILE;
            colDec.Width = 120;
            colDec.DecimalPlaces = 2;
            grdMiles.Columns.Add(colDec);



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Rate (£)";
            colDec.Width = 80;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_OTHERDETAILS.FARE;
            grdMiles.Columns.Add(colDec);

            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Company Rate (£)";
            colDec.Width = 140;
            colDec.DecimalPlaces = 2;
            colDec.IsVisible = false;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_OTHERDETAILS.COMPANYFARE;
            grdMiles.Columns.Add(colDec);




            if (AppVars.objPolicyConfiguration!=null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
            {

                grdMiles.Dock = DockStyle.Fill;

                grdMiles.Columns[COLS_OTHERDETAILS.FROMMILE].Width = 90;
                grdMiles.Columns[COLS_OTHERDETAILS.TOMILE].Width = 90;
                grdMiles.Columns[COLS_OTHERDETAILS.FARE].Width = 90;


               


                GridViewTextBoxColumn colTxt = new GridViewTextBoxColumn();
                colTxt.HeaderText = "Peak Time";
                colTxt.Name = COLS_OTHERDETAILS.PEAKTIME;
                colTxt.Width = 110;
                colTxt.ReadOnly = true;
                grdMiles.Columns.Add(colTxt);


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Peak Rate (£)";
                colDec.Width = 120;
                colDec.ReadOnly = false;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.PEAKRATE;
                grdMiles.Columns.Add(colDec);



                colTxt = new GridViewTextBoxColumn();
                colTxt.HeaderText = "Off Peak Time";
                colTxt.Name = COLS_OTHERDETAILS.OFFPEAKTIME;
                colTxt.Width = 110;
                colTxt.ReadOnly = true;
                grdMiles.Columns.Add(colTxt);


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Off Peak Rate (£)";
                colDec.Width = 120;
                colDec.ReadOnly = false;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.OFFPEAKRATE;
                grdMiles.Columns.Add(colDec);




                GridViewDateTimeColumn colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.FROMSTARTTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdMiles.Columns.Add(colDtp);


                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.TILLSTARTTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdMiles.Columns.Add(colDtp);


                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.FROMENDTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdMiles.Columns.Add(colDtp);


                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.TILLENDTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdMiles.Columns.Add(colDtp);

            }








            grdMiles.MasterTemplate.ShowRowHeaderColumn = false;


         

        }



        private void FormatPDAMilesGrid()
        {
           grdPDAMeter.AllowAddNewRow = false;
           grdPDAMeter.AllowEditRow = false;
           grdPDAMeter.AutoCellFormatting = true;
           grdPDAMeter.ShowGroupPanel = false;


            GridViewDecimalColumn colDec = null;



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "From Mile";
            colDec.Name = COLS_OTHERDETAILS.FROMMILE;
            colDec.Width = 120;
            colDec.DecimalPlaces = 2;
            grdPDAMeter.Columns.Add(colDec);

            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "To Mile";
            colDec.Name = COLS_OTHERDETAILS.TOMILE;
            colDec.Width = 120;
            colDec.DecimalPlaces = 2;
            grdPDAMeter.Columns.Add(colDec);



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Rate (£)";
            colDec.Width = 80;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_OTHERDETAILS.FARE;
            grdPDAMeter.Columns.Add(colDec);

            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Company Rate (£)";
            colDec.Width = 140;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_OTHERDETAILS.COMPANYFARE;
            grdPDAMeter.Columns.Add(colDec);


            if (AppVars.objPolicyConfiguration != null && AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
            {

                grdPDAMeter.Dock = DockStyle.Fill;

                grdPDAMeter.Columns[COLS_OTHERDETAILS.FROMMILE].Width = 90;
                grdPDAMeter.Columns[COLS_OTHERDETAILS.TOMILE].Width = 90;
                grdPDAMeter.Columns[COLS_OTHERDETAILS.FARE].Width = 90;





                GridViewTextBoxColumn colTxt = new GridViewTextBoxColumn();
                colTxt.HeaderText = "Peak Time";
                colTxt.Name = COLS_OTHERDETAILS.PEAKTIME;
                colTxt.Width = 110;
                colTxt.ReadOnly = true;
                grdPDAMeter.Columns.Add(colTxt);


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Peak Rate (£)";
                colDec.Width = 120;
                colDec.ReadOnly = false;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.PEAKRATE;
                grdPDAMeter.Columns.Add(colDec);



                colTxt = new GridViewTextBoxColumn();
                colTxt.HeaderText = "Off Peak Time";
                colTxt.Name = COLS_OTHERDETAILS.OFFPEAKTIME;
                colTxt.Width = 110;
                colTxt.ReadOnly = true;
                grdPDAMeter.Columns.Add(colTxt);


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Off Peak Rate (£)";
                colDec.Width = 120;
                colDec.ReadOnly = false;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.OFFPEAKRATE;
                grdPDAMeter.Columns.Add(colDec);




                GridViewDateTimeColumn colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.FROMSTARTTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdPDAMeter.Columns.Add(colDtp);


                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.TILLSTARTTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdPDAMeter.Columns.Add(colDtp);


                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.FROMENDTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdPDAMeter.Columns.Add(colDtp);


                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.TILLENDTIME;
                colDtp.IsVisible = false;
                colDtp.CustomFormat = "hh:tt";
                colDtp.FormatString = "hh:tt";
                grdPDAMeter.Columns.Add(colDtp);

            }
            
            
            grdPDAMeter.MasterTemplate.ShowRowHeaderColumn = false;




        }


        private void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ViewDetailForm();
        }

        private void ViewDetailForm()
        {

            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
                ShowFareForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            ViewDetailForm();
        }

        private void grdLister_CellClick(object sender, GridViewCellEventArgs e)
        {

            bool exist = false;
            if (grdLister.CurrentRow != null)
            {
                long fareId = grdLister.CurrentRow.Cells["Id"].Value.ToLong();


                if (fareId > 0 && grdDetails.Rows.Count(c => c.Cells["FareId"].Value.ToLong() == fareId) > 0)
                {
                    exist = true;

                }
                else
                {
                    exist = false;

                }


            



            }

            if (exist == false)
            {

                ViewFareDetails();
            }
        }

        private void ViewFareDetails()
        {
            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
                int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();

                Fare obj = General.GetObject<Fare>(c => c.Id == id);
                if (obj != null)
                {
                    lblFareDetailHeading.Text = string.Empty;
                    lblFareDetailHeading.Text= "Fare Details For Vehicle Type : " + obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr();
                    //     lblFareDetailTitle.Text = "Fare Details For Vehicle Type : " + obj.Fleet_VehicleType.DefaultIfEmpty().VehicleType;



                    //List<Fare_ChargesDetail> list = obj.Fare_ChargesDetails.ToList();


                    //NC
                    var list2 = (from a in General.GetQueryable<Fare_ChargesDetail>(c => c.FareId == id)
                                 select new
                                 {
                                     Id = a.Id,
                                     FromLocation =a.OriginLocationTypeId==Enums.LOCATION_TYPES.ADDRESS ? a.FromAddress :a.OriginLocationTypeId==100 ? a.Gen_Zone.ZoneName :  a.Gen_Location1.LocationName,
                                     ToLocation =a.DestinationLocationTypeId==Enums.LOCATION_TYPES.ADDRESS ?a.ToAddress: a.DestinationLocationTypeId==100 ? a.Gen_Zone1.ZoneName :  a.Gen_Location.LocationName,
                                     FARE = a.Rate,
                                     COMPANYFARE = a.CompanyRate,
                                     a.FareId
                                 }).ToList();
                    if (list2.Count > 0)
                    {
                        grdDetails.Columns.Clear();
                        grdDetails.DataSource = list2;
                        grdDetails.Columns["Id"].IsVisible = false;
                        grdDetails.Columns["FareId"].IsVisible = false;

                        grdDetails.Columns["FromLocation"].Width = 200;
                        grdDetails.Columns["ToLocation"].Width = 250;
                        grdDetails.Columns["FromLocation"].HeaderText = "From Location";
                        grdDetails.Columns["FromLocation"].ReadOnly = true;
                        grdDetails.Columns["ToLocation"].HeaderText = "To Location";
                        grdDetails.Columns["ToLocation"].ReadOnly = true;
                        grdDetails.Columns["FARE"].HeaderText = "Rate (£)";
                        grdDetails.Columns["FARE"].Width = 120;

                        grdDetails.Columns["COMPANYFARE"].HeaderText = "Company Rate (£)";
                        grdDetails.Columns["COMPANYFARE"].Width = 150;

                        //grdDetails.Columns["FARE"].ReadOnly = false;
                        // DetailGridButton();
                        // grdDetails.AllowEditRow = true;
                        //grdDetails.EndEdit();
                    }
                    else
                    {
                        grdDetails.Rows.Clear();
                    }
                    //

                  //  grdDetails.DataSource = list;

                    //Comment 19 June 2015
                    //grdDetails.RowCount = list.Count;
                    //int cnt = grdDetails.RowCount;
                    //for (int i = 0; i < cnt; i++)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = list[i].Gen_Location1.LocationName;
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = list[i].Gen_Location.LocationName;
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = list[i].Rate;
                    //}



                    grdMiles.RowCount = obj.Fare_OtherCharges.Count;
                    for (int i = 0; i < grdMiles.RowCount; i++)
                    {
                        grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.FROMMILE].Value = obj.Fare_OtherCharges[i].FromMile.ToDecimal();
                        grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.TOMILE].Value = obj.Fare_OtherCharges[i].ToMile.ToDecimal();
                        grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.FARE].Value = obj.Fare_OtherCharges[i].Rate;
                        grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.COMPANYFARE].Value = obj.Fare_OtherCharges[i].CompanyRate;

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




                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.PEAKRATE].Value = obj.Fare_OtherCharges[i].PeakTimeRate.ToDecimal();
                            grdMiles.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = obj.Fare_OtherCharges[i].OffPeakTimeRate.ToDecimal();
                        }
                    }


                    // PDA Meter

                    grdPDAMeter.RowCount = obj.Fare_PDAMeters.Count;
                    for (int i = 0; i < grdPDAMeter.RowCount; i++)
                    {
                        grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.FROMMILE].Value = obj.Fare_PDAMeters[i].FromMile.ToDecimal();
                        grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.TOMILE].Value = obj.Fare_PDAMeters[i].ToMile.ToDecimal();
                        grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.FARE].Value = obj.Fare_PDAMeters[i].Rate;
                        grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.COMPANYFARE].Value = obj.Fare_PDAMeters[i].CompanyRate;

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




                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.PEAKTIME].Value = peakTime;
                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKTIME].Value = offPeakTime;

                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.FROMENDTIME].Value = fromEndTime;
                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.TILLENDTIME].Value = tillEndTime;

                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.PEAKRATE].Value = obj.Fare_PDAMeters[i].PeakTimeRate.ToDecimal();
                            grdPDAMeter.Rows[i].Cells[COLS_OTHERDETAILS.OFFPEAKRATE].Value = obj.Fare_PDAMeters[i].OffPeakTimeRate.ToDecimal();
                        }
                    }


                    // Plot Wise Fare

                    grdPlotWiseFare.BeginUpdate();

                    grdPlotWiseFare.RowCount = obj.Fare_ZoneWisePricings.Count;
                    //Gen_ZonesType_Pricing zone = null;
                    for (int i = 0; i < grdPlotWiseFare.RowCount; i++)
                    {
                        //string Zone=obj.Fare_ZoneWisePricings[i].Gen_Zone.ZoneName
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.Id].Value = obj.Fare_ZoneWisePricings[i].Id;
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FareId].Value = obj.Fare_ZoneWisePricings[i].FareId;
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FromZoneId].Value = obj.Fare_ZoneWisePricings[i].FromZoneId;
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.ToZoneId].Value = obj.Fare_ZoneWisePricings[i].ToZoneId;

                          grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FromPlotNo].Value=obj.Fare_ZoneWisePricings[i].Gen_Zone.DefaultIfEmpty().OrderNo.ToStr();

                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.FromPlot].Value = obj.Fare_ZoneWisePricings[i].Gen_Zone.DefaultIfEmpty().ZoneName.ToStr().Trim();
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.ToPlot].Value =  obj.Fare_ZoneWisePricings[i].Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr().Trim();
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.ToPlotNo].Value = obj.Fare_ZoneWisePricings[i].Gen_Zone1.DefaultIfEmpty().OrderNo.ToStr();

                        
                        
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.Price].Value = obj.Fare_ZoneWisePricings[i].Price;
                        grdPlotWiseFare.Rows[i].Cells[COLS_PLOTEWISE.CompanyPrice].Value = obj.Fare_ZoneWisePricings[i].CompanyRate;

                        //  zone = obj.Fare_ZoneWisePricings[i].de.DefaultIfEmpty();
                    }


                    grdPlotWiseFare.EndUpdate();

                }
            }
            else
            {
                lblFareDetailHeading.Text = string.Empty;
                grdMiles.Rows.Clear();
                grdDetails.Rows.Clear();
                grdPDAMeter.Rows.Clear();
                grdPlotWiseFare.Rows.Clear();

            }

        }

        private void btnCopyFare_Click(object sender, EventArgs e)
        {
            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
                int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();

                CopyCurrentFare(id);
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }

        private void CopyCurrentFare(int id)
        {
            frmFares frm = new frmFares();
            frm.CopyFare(General.GetObject<Fare>(c=>c.Id==id));

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();


        }

        private void grdLister_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                PopulateData();

            }
        }

        private void menu_FixedFares_Click(object sender, EventArgs e)
        {

        }

        private void btnDownloadFares_Click(object sender, EventArgs e)
        {
            frmDownloadFares frm = new frmDownloadFares();
            frm.ShowDialog();
        }

        //string VechileName ="";
        private void btn_ExportWeb_Click(object sender, EventArgs e)
        {

            try
            {

                if (File.Exists(Application.StartupPath + "\\FaresSyncUtility.exe"))
                {
                    if (DialogResult.Yes == MessageBox.Show("Are you sure ? ", "", MessageBoxButtons.YesNo))
                    {

                        Process.Start("FaresSyncUtility.exe");
                        //frmAddFaresToWeb frm = new frmAddFaresToWeb();
                        //frm.ShowDialog();
                    }
                }
                else
                {

                    MessageBox.Show("File not found");
                }
                //frm.Dispose();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
            //if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            //{


            //    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();

            //    string Client = AppVars.objPolicyConfiguration.DefaultClientId.ToString();
            //    ClientName = Client;

            //    FareBO obj = new FareBO();
            //    var data1 = General.GetQueryable<Fare_ChargesDetail>(c => c.FareId == id);

            //    var query = from a in data1
            //                select new
            //                {
            //                    ID = a.Id,
            //                    toAddress = a.Gen_Location.Address,
            //                    fromAddress = a.Gen_Location1.Address,
            //                    Fromlocation = a.Gen_Location1.LocationName,
            //                    tolocation = a.Gen_Location.LocationName,
            //                    Fare = a.Rate,
            //                    fromtype = a.Gen_Location1.LocationTypeId,
            //                    totype=a.Gen_Location.LocationTypeId
            //                };
            //    DataTable dt = query.ToDataTable();

            //    GeClientName();
            //    string abc = grdLister.CurrentRow.Cells["VehicleType"].Value.ToStr();
            //    if (VechileName == abc)
            //    {
            //    }
            //    else
            //    {
            //        ENUtils.ShowMessage("This Vechile is Not Default to Web..");
            //        return;
            //    }


            //    for (int index = 0; index < dt.Rows.Count; index++)
            //    {
            //        int? fromtype = dt.Rows[index]["fromtype"].ToInt();
            //        int? totype = dt.Rows[index]["totype"].ToInt();


            //        if (fromtype == 8 && totype==1)
            //        {
            //            string toPostCode = General.GetPostCodeMatch(dt.Rows[index]["toAddress"].ToStr());

            //            Insert_Fare(dt.Rows[index]["Fromlocation"].ToStr(), dt.Rows[index]["toAddress"].ToStr(), dt.Rows[index]["Fare"].ToDecimal(), FareID, dt.Rows[index]["Fromlocation"].ToStr(), toPostCode);


            //        }
            //        else if(fromtype==1 && totype==8)
            //        {
            //            string fromPostCode = General.GetPostCodeMatch(dt.Rows[index]["fromAddress"].ToStr());

            //            Insert_Fare(dt.Rows[index]["FromAddress"].ToStr(), dt.Rows[index]["tolocation"].ToStr(), dt.Rows[index]["Fare"].ToDecimal(), FareID, fromPostCode, dt.Rows[index]["tolocation"].ToStr());


            //        }
            //        else if(fromtype==8 && totype==8)
            //        {
            //            string FromPostCode = General.GetPostCodeMatch(dt.Rows[index]["Fromlocation"].ToStr());
            //            string ToPostCode = General.GetPostCodeMatch(dt.Rows[index]["Tolocation"].ToStr());

            //            Insert_Fare(FromPostCode, ToPostCode, dt.Rows[index]["Fare"].ToDecimal(), FareID, FromPostCode, ToPostCode);
            //        }




                    
            //        else
            //        {
            //            string FromPostCode = General.GetPostCodeMatch(dt.Rows[index]["fromAddress"].ToStr());
            //            string ToPostCode = General.GetPostCodeMatch(dt.Rows[index]["toAddress"].ToStr());

            //            Insert_Fare(dt.Rows[index]["fromAddress"].ToStr(), dt.Rows[index]["toAddress"].ToStr(), dt.Rows[index]["Fare"].ToDecimal(), FareID, FromPostCode, ToPostCode);
            //        }

            //    }

            //    ENUtils.ShowMessage("Fares Export Successfully..");


            //}
        }


        //void GeClientName()
        //{
        //    try
        //    {

        //        string online =General.DecryptConnectionString(System.Configuration.ConfigurationSettings.AppSettings["OnlineConnectionString"].ToString());
        //        SqlConnection conn = new SqlConnection();
        //        conn.ConnectionString = online;

        //        SqlCommand cmd;
        //        DataSet ds = new DataSet();

        //        // For Client ID
        //        string ID = "select ID from clients  where name = '" + ClientName + "'";
        //        SqlDataAdapter dr = new SqlDataAdapter(ID, conn);
        //        dr.Fill(ds);
        //        ds.Tables[0].TableName = "ClientID";
        //        int ClientID = ds.Tables["ClientID"].Rows[0]["ID"].ToInt();

        //        // Fare ID
        //        string Fare = "stp_GetFareID '" + ClientID + "'";
        //        dr = new SqlDataAdapter(Fare, conn);
        //        dr.Fill(ds);
        //        ds.Tables[1].TableName = "FareID";
        //        int FID = ds.Tables["FareID"].Rows[0]["FareID"].ToInt();
        //        string Vehicle = ds.Tables["FareID"].Rows[0]["VehicleName"].ToStr();
        //        FareID = FID;
        //        VechileName = Vehicle;

        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowErrorMessage(ex.Message);
        //    }

        ////}
        //private void Insert_Fare(string FromAddress, string ToAddress, Decimal Fares, Int64 FareID, string FromPostCode, string ToPostCode)
        //{
        //    try
        //    {
        //        string online =General.DecryptConnectionString(System.Configuration.ConfigurationSettings.AppSettings["OnlineConnectionString"].ToString());
        //        SqlConnection conn = new SqlConnection();
        //        conn.ConnectionString = online;
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_InsertFareFromDespatch";
        //        cmd.Parameters.Add("@FromAddress", SqlDbType.VarChar).Value = FromAddress;
        //        cmd.Parameters.Add("@ToAddress", SqlDbType.VarChar).Value = ToAddress;
        //        cmd.Parameters.Add("@Fares", SqlDbType.Decimal).Value = Fares;
        //        cmd.Parameters.Add("@FareID", SqlDbType.Int).Value = FareID;
        //        cmd.Parameters.Add("@FromPostCode", SqlDbType.VarChar).Value = FromPostCode;
        //        cmd.Parameters.Add("@ToPostCode", SqlDbType.VarChar).Value = ToPostCode;

        //        cmd.Connection = conn;
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowErrorMessage(ex.Message);
        //    }
        //}

        private void btnAddFare_Click(object sender, EventArgs e)
        {

            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewRowInfo)
                {
                    //int FareId = objMaster.Current.Id;

                   // int Id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    int VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
                    frmAddFares frm = new frmAddFares(VehicleTypeId);
                    frm.ControlBox = true;
                    frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                    frm.MaximizeBox = false;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnCopyCompanyFares_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow.IsSelected==true && grdLister.CurrentRow is GridViewRowInfo)
                {
                    int VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
                    int CompanyId = grdLister.CurrentRow.Cells["CompanyId"].Value.ToInt();
                    //if (CompanyId == 0)
                    //{
                    //    return;
                    //}
                    var data1 = General.GetQueryable<Fare>(null);


                    var query = from a in data1

                                select new
                                {
                                    Id = a.CompanyId,
                                    VehicleTypeid = a.VehicleTypeId,

                                };


                    var CompanyList = (from a in General.GetQueryable<Gen_Company>(null)
                                       select new
                                       {
                                           Id = a.Id,
                                           CompanyName = a.CompanyName
                                       }).ToList();

                    var listC = CompanyList.Where(a => !query.Any(b => b.Id == a.Id && b.VehicleTypeid == VehicleTypeId)).ToList();
                    List<Gen_Company> companylistfilter = (from a in listC
                                                           select new Gen_Company
                                                           {
                                                               Id = a.Id,
                                                               CompanyName = a.CompanyName,
                                                           }).ToList();
                    globalcompanyidfilter = companylistfilter;
                    frmCopyCompanyFares fm = new frmCopyCompanyFares();
                    fm.bindgridother(globalcompanyidfilter);
                    fm.Vechileid = VehicleTypeId;
                    fm.CompanyId = CompanyId;
                    fm.Show();
                }
                else
                {
                    ENUtils.ShowMessage("Please select any company for copy fares");
                }
            }
            catch 
            {
            }
        }

        private void btnCopySubCompanyFares_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow.IsSelected == true && grdLister.CurrentRow is GridViewRowInfo)
                {
                    int VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
                    int SubCompanyId = grdLister.CurrentRow.Cells["SubCompanyId"].Value.ToInt();
                    if (SubCompanyId == 0)
                    {
                        return;
                    }
                    var data1 = General.GetQueryable<Fare>(null);


                    var query = from a in data1

                                select new
                                {
                                    Id = a.SubCompanyId,
                                    VehicleTypeid = a.VehicleTypeId,

                                };


                    var CompanyList = (from a in General.GetQueryable<Gen_SubCompany>(null)
                                       select new
                                       {
                                           Id = a.Id,
                                           CompanyName = a.CompanyName
                                       }).ToList();

                    var listC = CompanyList.Where(a => !query.Any(b => b.Id == a.Id && b.VehicleTypeid == VehicleTypeId)).ToList();

                    List<Gen_SubCompany> companylistfilter = (from a in listC
                                                           select new Gen_SubCompany
                                                           {
                                                               Id = a.Id,
                                                               CompanyName = a.CompanyName,
                                                           }).ToList();
                    globalsubcompanyidfilter = companylistfilter;
                    frmCopySubCompanyFares fm = new frmCopySubCompanyFares();
                    fm.bindgridother(globalsubcompanyidfilter);
                    fm.Vechileid = VehicleTypeId;
                    fm.SubCompanyId = SubCompanyId;
                    fm.Show();
                }
                else
                {
                    ENUtils.ShowMessage("Please select any Sub Company for copy fares");
                }
            }
            catch
            {
            }
        }
    }
}
