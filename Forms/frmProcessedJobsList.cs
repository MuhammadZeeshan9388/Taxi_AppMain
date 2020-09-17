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
using Telerik.WinControls;
using Telerik.Data;
using Taxi_AppMain.Forms;
using System.Threading;


namespace Taxi_AppMain
{
    public partial class frmProcessedJobsList : UI.SetupBase
    {

        public struct COLS
        {
            public static string ID = "ID";
            public static string TransId = "TransId";
            public static string BookingId = "BookingId";
            public static string AccountTypeId = "AccountTypeId";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string VehicleID = "VehicleID";
            public static string RefNumber = "RefNumber";
            public static string PupilNo = "PupilNo";
            public static string BookedBy = "BookedBy";

            public static string Drv = "Drv";
          //  public static string Escort = "Escort";
            public static string EscortPrice = "EscortPrice";


            public static string OrderNo = "OrderNo";

            public static string Passenger = "Passenger";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string DParking = "DParking";
            public static string DWaiting = "DWaiting";


            public static string CParking = "CParking";
            public static string CWaiting = "CWaiting";


            public static string ExtraDrop = "ExtraDrop";
            public static string CompanyPrice = "CompanyPrice";
            public static string CommissionAmount = "CommissionAmount";
            public static string RemovalDescription = "RemovalDescription";
            public static string Total = "Total";
            public static string Payment_ID = "Payment_ID";

            public static string Fares = "Fares";
            public static string Account = "Account";
            public static string CompanyId = "CompanyId";

        }

        
         BookingBO objMaster;

     
        public frmProcessedJobsList()
        {

            InitializeLoading();          
                 

            InitializeComponent();
            this.Load += new EventHandler(frmBookingsList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
          //  grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
          
           
            objMaster = new BookingBO();
            FillCombo();
            this.SetProperties((INavigation)objMaster);
            grdLister.ShowGroupPanel = false;

   
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            this.ddlCompany.SelectedValueChanged += new EventHandler(ddlCompany_SelectedValueChanged);

         //   grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            

          //  grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);           
        }

        void ddlCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int CompanyId = ddlCompany.SelectedValue.ToInt();
                if (CompanyId > 0)
                {
                    Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == CompanyId);
                    if (obj.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                    {
                        ddlPaymentType.SelectedValue = null;
                    }
                }
                else
                {
                    var item = ddlPaymentType.Items.FirstOrDefault(c => c.Value.ToInt() == 9);
                    if (item != null)
                    {
                        item.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void FillCombo()
        {
            ComboFunctions.FillPaymentTypeCombo(ddlPaymentType);
        }

        private void FormatGrid()
        {

            GridViewCheckBoxColumn colc = new GridViewCheckBoxColumn();
            colc.Width = 40;
            colc.AutoSizeMode = BestFitColumnMode.None;
            colc.HeaderText = "";
            colc.Name = "Check";
            grdLister.Columns.Add(colc);
          

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();

            col.IsVisible = false;
            col.Name = COLS.BookingId;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Order No";
            col.Name = "OrderNo";
            col.Width = 90;
            grdLister.Columns.Add(col);

                  

            GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
            colDt.Name = "PickupDate";
            colDt.ReadOnly = true;
            colDt.HeaderText = "Pickup Date-Time";
            grdLister.Columns.Add(colDt);

 

           

            col = new GridViewTextBoxColumn();
           
            col.HeaderText = "Vehicle";
            col.Name = "Vehicle";
            grdLister.Columns.Add(col);


           



            col = new GridViewTextBoxColumn();
            col.Name = COLS.Passenger;
            col.HeaderText = "Passenger";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.Name = COLS.Account;
            col.HeaderText = "Account";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.Drv;
            col.HeaderText = COLS.Drv;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.AccountTypeId;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup Point";
            col.Name = "PickupPoint";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "Destination";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "D Fares";
            colD.Name = "Fares";
            colD.Maximum = 9999999;
            colD.ReadOnly = true;
            colD.FormatString = "{0:#,###0.00}";

            grdLister.Columns.Add(colD);





            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "D Parking";
            colD.Name = COLS.DParking;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";

            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "D Waiting";
            colD.Name = COLS.DWaiting;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";

            grdLister.Columns.Add(colD);



            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "C Fares";
            colD.ReadOnly = true;
            colD.Name = COLS.CompanyPrice;
            colD.Maximum = 9999999;
            // colD.ReadOnly = true;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "C Parking";
            colD.ReadOnly = true;
            colD.Name = COLS.CParking;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";

            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "C Waiting";
            colD.ReadOnly = true;
            colD.Name = COLS.CWaiting;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";

            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Escort Price";
            colD.Name = COLS.EscortPrice;
            colD.Maximum = 9999999;
            colD.IsVisible = false;
            // colD.ReadOnly = true;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);
          

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Commission Amount";
            colD.Name = COLS.CommissionAmount;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            
            grdLister.Columns.Add(colD);

           
           


            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns["PickUpDate"].Width = 100;

            grdLister.Columns["Account"].Width = 75;
            grdLister.Columns[COLS.Drv].Width = 70;
            grdLister.Columns["OrderNo"].Width = 70;
            grdLister.Columns["Vehicle"].Width = 60;
            grdLister.Columns[COLS.Passenger].Width = 60;
            grdLister.Columns["PickUpPoint"].Width = 80;
            grdLister.Columns["Destination"].Width = 80;

            grdLister.Columns["Fares"].Width = 50;
            grdLister.Columns[COLS.DParking].Width = 50;
            grdLister.Columns[COLS.DWaiting].Width = 50;
          
            grdLister.Columns["CompanyPrice"].Width = 60;
            grdLister.Columns[COLS.CParking].Width = 50;
            grdLister.Columns[COLS.CWaiting].Width = 50;
          

            grdLister.Columns["CommissionAmount"].Width = 70;
           

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns[COLS.OrderNo].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";








            AddCommandColumn(grdLister, "UnProcess", "UnProcess Job");
            



            //            grdLister.CommandCellClick+=new CommandCellClickEventHandler(grdLister_CommandCellClick);


        }


        private void AddCommandColumn(RadGridView grid, string colName, string caption)
        {

            if (grid.Columns.Contains(colName))
                return;

            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 120;

            col.Name = colName;
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = caption;
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

         private void StartLoading()
         {

             InitializeLoading();
         }

         delegate void DisplayProgressBar();
         private void InitializeLoading()
         {

             if (this.InvokeRequired)
             {
                 DisplayProgressBar d = new DisplayProgressBar(ShowLoadingImage);
                 this.BeginInvoke(d);
             }
             else
             {
                 ShowLoadingImage();

             }

         }

         frmLoadingScreen frmLoading = null;
         private void ShowLoadingImage()
         {

             frmLoading = new frmLoadingScreen();
             frmLoading.ShowInTaskbar = false;
             frmLoading.Show();
         }
             


         private void FinishLoading()
         {
             if (frmLoading != null)
             {
                 frmLoading.BackgroundImage.Dispose();
                 frmLoading.Dispose();
                 frmLoading.Close();


             }
         }



         private void InitializeExportGrid()
         {
             this.radGridView1 = new Telerik.WinControls.UI.RadGridView();

             ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
             ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();

            
             this.radGridView1.Location = new System.Drawing.Point(405, 60);
             this.radGridView1.Name = "radGridView1";
             this.radGridView1.Size = new System.Drawing.Size(240, 150);
             this.radGridView1.TabIndex = 18;
             this.radGridView1.Text = "radGridView1";
             this.radGridView1.Visible = false;

             ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
             ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();

             this.radPanel1.Controls.Add(this.radGridView1);
         }


         void EditFareItem2_Click(object sender, EventArgs e)
         {
             try
             {
                 if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                 {
                     string message = AppVars.objPolicyConfiguration.ArrivalBookingText.ToStr();
                     frmSMSAll frm = new frmSMSAll(grdLister.CurrentRow.Cells["MobileNo"].Value.ToStr(), message,0);
                     frm.ShowDialog();
                     frm.Dispose();
                 }
             }
             catch (Exception ex)
             {
                 ENUtils.ShowMessage(ex.Message);
             }
         }



       



         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             string name = gridCell.ColumnInfo.Name.ToLower();

             GridViewRowInfo row = gridCell.RowElement.RowInfo;
             long id = row.Cells[COLS.BookingId].Value.ToLong();

            

          

              if (name == "unprocess")
             {
                 

                     RadGridView grid = gridCell.GridControl;
                     objMaster = new BookingBO();

                     objMaster.GetByPrimaryKey(id);

                     if (objMaster.Current != null)
                     {

                         if (General.GetQueryable<DriverRent_Charge>(c=>c.BookingId==id).Count() > 0)
                         {
                             ENUtils.ShowMessage("Cannot UnProcess this Job as Driver Rent is generated from it");
                             return;

                         }

                         objMaster.Current.IsProcessed = false;

                         objMaster.CheckCustomerValidation = false;
                         objMaster.CheckDataValidation = false;

                         objMaster.Save();
                         row.Delete();
                     }
                


                          
                
             }

            
         }


         

         private void AddCommandColumn(string name,string headerText,int width)
         {
             GridViewCommandColumn col = new GridViewCommandColumn();
             col.Width = width;
          
             col.UseDefaultText = true;
             col.DefaultText = headerText;
             col.Name = name;
             grdLister.Columns.Add(col);

         }



         void frmBookingsList_Load(object sender, EventArgs e)
         {

             chkAllAcc.Checked = true;
             FormatGrid();


             try
             {

                 DateTime? fromDate = AppVars.objPolicyConfiguration.RentFromDateTime.ToDateTimeorNull();
                 DateTime? toDate = AppVars.objPolicyConfiguration.RentToDateTime.ToDateTimeorNull();

                 int subtracted = 7 - (int)fromDate.Value.DayOfWeek;

                 int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
                 DateTime dtFrom = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));


                 if (subtracted == 7)
                     subtracted = 6;

                 fromDate = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.AddDays(-subtracted).Day, fromDate.Value.Hour, fromDate.Value.Minute, 0, 0);


                 DateTime dtTo = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
                 toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dtTo.AddDays(toDate.Value.DayOfWeek.ToInt()).Day, toDate.Value.Hour, toDate.Value.Minute, 59, 999);


                 dtpFromDate.Value = fromDate;
                 dtpTillDate.Value = toDate;


             }
             catch (Exception ex)
             {
                 dtpFromDate.Value = DateTime.Now.AddDays(-7);
                 dtpTillDate.Value = DateTime.Now.AddDays(-1);

             }


             PopulateData();
            
            

            // grdLister.Columns["SubCompanyBgColor"].IsVisible = false;
             //grdLister.Columns["MobileNo"].IsVisible = false;
             grdLister.Columns["BookingId"].IsVisible = false;
           //  grdLister.Columns["DriverId"].IsVisible = false;

          //   grdLister.Columns["VehicleBgColor"].IsVisible = false;
           //  grdLister.Columns["VehicleTextColor"].IsVisible = false;

           //  grdLister.Columns["BackgroundColor1"].IsVisible = false;
           //  grdLister.Columns["TextColor1"].IsVisible = false;


          //   grdLister.Columns["BookingTypeId"].IsVisible = false;


            // grdLister.Columns["StatusColor"].IsVisible = false;
           //  grdLister.Columns["FromLocTypeId"].IsVisible = false;
          //   grdLister.Columns["ToLocTypeId"].IsVisible = false;
//
            
         //    AddCommandColumn("btnReDespatch", "Despatch", 70);

          

             UI.GridFunctions.SetFilter(grdLister);


             grdLister.AllowEditRow = true;

           
             grdLister.Columns["OrderNo"].Width = 80;
             grdLister.Columns["OrderNo"].HeaderText = "Order No";
             grdLister.Columns["Fares"].Width = 70;
           //  grdLister.Columns["Fares"].HeaderText = "Fare £";
             grdLister.Columns["Vehicle"].Width = 70;
             //grdLister.Columns["Driver"].Width = 50;

            // grdLister.Columns["Status"].Width = 80;
             grdLister.Columns["Passenger"].Width = 70;

             grdLister.Columns[COLS.PickupPoint].Width = 110;
             grdLister.Columns[COLS.PickupPoint].HeaderText = "Pickup Point";

             grdLister.Columns[COLS.Destination].Width = 110;
             grdLister.Columns[COLS.Destination].HeaderText = "Destination";


          ///  grdLister.Columns["BookingDate"].Width = 90;
            // grdLister.Columns["BookingDate"].HeaderText = "Booking Date";


             (grdLister.Columns["PickupDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
             (grdLister.Columns["PickupDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


             grdLister.Columns["PickupDate"].Width = 110;
             grdLister.Columns["PickupDate"].HeaderText = "Pickup Date-Time";


             grdLister.Columns["Account"].Width = 60;
             grdLister.Columns["Account"].HeaderText = "A/C";


          //   FinishLoading();
         }

         void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
         {
             ViewDetailForm();
         }

         private void ViewDetailForm()
         {

             if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
             {
                 ShowBookingForm(grdLister.CurrentRow.Cells[COLS.BookingId].Value.ToInt());
             }
             else
             {
                 ENUtils.ShowMessage("Please select a record");
             }
         }


         private void ShowBookingForm(int id)
         {


            frmBooking frm = new frmBooking();
            frm.OnDisplayRecord(id);
            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();     

         }


       

        public override void RefreshData()
        {
         
            PopulateData();
        }


        public override void PopulateData()
        {
            try
            {
                int driverId = ddlDriver.SelectedValue.ToInt();
                int PaymentTypeId = ddlPaymentType.SelectedValue.ToInt();
                int criteriaBy = 3;
                int? companyId = ddlCompany.SelectedValue.ToIntorNull();

                if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    criteriaBy = 1;

                else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    criteriaBy = 2;




                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? toDate = dtpTillDate.Value.ToDate();

                var list = General.GetQueryable<Booking>(c => (c.PickupDateTime.Value.Date >=fromDate && c.PickupDateTime.Value.Date<=toDate)
                    &&
                    (c.IsProcessed!=null && c.IsProcessed==true)
                    
                    &&
                    c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                   
                    
                    && (c.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
                       && (c.DriverId == driverId || driverId == 0)
                    && ((criteriaBy == 3 && c.CompanyId!=null && (companyId==null || c.CompanyId==companyId))
                    && (c.PaymentTypeId==PaymentTypeId|| PaymentTypeId==0)
                    || (criteriaBy == 2 && c.CompanyId == null) 
                    || (criteriaBy==1))
                    
                    )
                     .OrderByDescending(c => c.PickupDateTime).ToList();




                int cnt = list.Count;

                grdLister.RowCount = cnt;


                for (int i = 0; i < cnt; i++)
                {

                    grdLister.Rows[i].Cells[COLS.BookingId].Value = list[i].Id;
                    grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickupDateTime.ToDateTimeorNull();
                    grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].FromAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].ToAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Passenger].Value = list[i].CustomerName.ToStr();
                    grdLister.Rows[i].Cells[COLS.Account].Value = list[i].Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                    grdLister.Rows[i].Cells[COLS.AccountTypeId].Value = list[i].Gen_Company.DefaultIfEmpty().AccountTypeId.ToInt();
                    grdLister.Rows[i].Cells[COLS.OrderNo].Value = list[i].OrderNo.ToStr();
                    grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i].Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr();
                    grdLister.Rows[i].Cells[COLS.Fares].Value = list[i].FareRate.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.CompanyPrice].Value = list[i].CompanyPrice.ToDecimal();
                  
                    
                    grdLister.Rows[i].Cells[COLS.DParking].Value = list[i].CongtionCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.DWaiting].Value = list[i].MeetAndGreetCharges.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.CParking].Value = list[i].ParkingCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.CWaiting].Value = list[i].WaitingCharges.ToDecimal();


                    grdLister.Rows[i].Cells[COLS.CommissionAmount].Value = list[i].DriverCommission.ToDecimal();


                    grdLister.Rows[i].Cells[COLS.Drv].Value = list[i].Fleet_Driver.DefaultIfEmpty().DriverNo.ToStr();

                    grdLister.Rows[i].Cells[COLS.EscortPrice].Value = list[i].EscortPrice.ToDecimal();



                }





                 

                    
               
            }
            catch (Exception ex)
            {


            }
        
            
        }




        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool()).Count() == 0) return;
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to Save and Ready Selected Booking(s) ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    bool RentGenerated = false;
                  
                    foreach (GridViewRowInfo row in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool()).ToList())
                    {
                        objMaster = new BookingBO();

                        objMaster.GetByPrimaryKey(row.Cells[COLS.BookingId].Value.ToLong());
                       

                        if (objMaster.Current != null)
                        {


                            if (General.GetQueryable<DriverRent_Charge>(c => c.BookingId == row.Cells[COLS.BookingId].Value.ToLong()).Count() == 0)
                            //if (objMaster.Current.DriverRent_Charges.Count == 0)
                            {


                                objMaster.Current.FareRate = row.Cells[COLS.Fares].Value.ToDecimal();
                                objMaster.Current.CompanyPrice = row.Cells[COLS.CompanyPrice].Value.ToDecimal();
                                objMaster.Current.CongtionCharges = row.Cells[COLS.DParking].Value.ToDecimal();
                                objMaster.Current.MeetAndGreetCharges = row.Cells[COLS.DWaiting].Value.ToDecimal();
                                objMaster.Current.DriverCommission = row.Cells[COLS.CommissionAmount].Value.ToDecimal();


                                objMaster.Current.ParkingCharges = row.Cells[COLS.CParking].Value.ToDecimal();
                                objMaster.Current.WaitingCharges = row.Cells[COLS.CWaiting].Value.ToDecimal();

                                objMaster.Current.IsProcessed = false;

                                objMaster.CheckCustomerValidation = false;
                                objMaster.CheckDataValidation = false;

                                objMaster.Save();


                            }
                            else
                            {
                                RentGenerated = true;

                            }
                            //row.Delete();
                        }
                    }

                    PopulateData();



                    if (RentGenerated)
                    {

                        ENUtils.ShowMessage("Some Jobs are not UnProcessed because of Rent Transaction is Generated from it");

                    }
                    //  grdLister.Rows.Where(c=>processedJobList.Contains(c.Cells[COLS.BookingId].Value.ToLong())).ToList().RemoveAll(

                    
                }
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

        private void chkAllAcc_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {

                if (ddlCompany.DataSource == null)
                {
                    ComboFunctions.FillCompanyCombo(ddlCompany);
                }

                ddlCompany.Enabled = true;
            }
            else
            {
                ddlCompany.Enabled = false;
                ddlCompany.SelectedValue = null;

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void ddlDriver_Enter(object sender, EventArgs e)
        {
            if (ddlDriver.DataSource == null)
            {
                ComboFunctions.FillDriverNoCombo(ddlDriver);


            }
        }

       

    



       

  
    

      


     

    }
}

