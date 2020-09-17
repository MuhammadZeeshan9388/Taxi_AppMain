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
using Taxi_BLL;
using Utils;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmBiddings : RadForm
    {
        public frmBiddings()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmBiddings_Load);
            timer1.Tick += new EventHandler(timer1_Tick);
            grdPendingJobs.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdPreBookings.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
   


            grdPendingJobs.ViewCellFormatting+=new CellFormattingEventHandler(grdPendingJobs_ViewCellFormatting);
            grdPreBookings.ViewCellFormatting += new CellFormattingEventHandler(grdPendingJobs_ViewCellFormatting);
       
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {

            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();

          


                GridViewRowInfo row = gridCell.RowElement.RowInfo;
                long jobId = row.Cells["Id"].Value.ToLong();
                int? driverId = row.Cells["DriverId"].Value.ToIntorNull();
                if (name == "accept")
                {
                    General.ShowDespatchForm(jobId, driverId);
                    PopulateData();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            PopulateData();
        }

        void frmBiddings_Load(object sender, EventArgs e)
        {
            try
            {

                GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                col.Width = 60;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "";
                col.Name = "chkBid";
                grdPendingJobs.Columns.Add(col);


                col = new GridViewCheckBoxColumn();
                col.Width = 60;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "";
                col.Name = "chkBid";
                grdPreBookings.Columns.Add(col);

                //this.grdPendingJobs.MasterTemplate.Reset();
            //    this.grdPendingJobs.TableElement.RowHeight = 20;

                PopulateData();

                FormatGrid();



                GridViewCommandColumn cmdCol = new GridViewCommandColumn();
                cmdCol.UseDefaultText = true;
                cmdCol.DefaultText = "Accept";
                cmdCol.Name = "accept";
                cmdCol.Width = 70;
                cmdCol.TextAlignment = ContentAlignment.MiddleCenter;
                grdPendingJobs.Templates[0].Columns.Add(cmdCol);


                cmdCol = new GridViewCommandColumn();
                cmdCol.UseDefaultText = true;
                cmdCol.DefaultText = "Accept";
                cmdCol.Name = "accept";
                cmdCol.Width = 70;
                cmdCol.TextAlignment = ContentAlignment.MiddleCenter;
                grdPreBookings.Templates[0].Columns.Add(cmdCol);

              
                GridViewRelation relation = new GridViewRelation(grdPendingJobs.MasterTemplate, grdPendingJobs.Templates[0]);
                relation.RelationName = "BookingBiddings"; relation.ParentColumnNames.Add("Id");
                relation.ChildColumnNames.Add("Id");
                this.grdPendingJobs.Relations.Add(relation);


                relation = new GridViewRelation(grdPreBookings.MasterTemplate, grdPreBookings.Templates[0]);
                relation.RelationName = "BookingBiddings"; relation.ParentColumnNames.Add("Id");
                relation.ChildColumnNames.Add("Id");
                this.grdPreBookings.Relations.Add(relation);


                grdPendingJobs.Rows.ToList().ForEach(c => c.Cells["chkBid"].Value = c.Cells["Bid"].Value);
                grdPreBookings.Rows.ToList().ForEach(c => c.Cells["chkBid"].Value = c.Cells["Bid"].Value);
  


                timer1.Start();

                this.SizeChanged += new EventHandler(frmBiddings_SizeChanged);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        private void AddCommandColumn(RadGridView grid, string name, string headerText)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();

            col.UseDefaultText = true;
            col.DefaultText = headerText;
            col.Name = name;
            col.Width = 70;
            col.TextAlignment = ContentAlignment.MiddleCenter;
            grid.Columns.Add(col);

        }


        public  void PopulateData()
        {
            try
            {

                DateTime? dt = DateTime.Now.ToDateorNull();
                DateTime dtTom = dt.Value.AddDays(1);


                var data1 = General.GetQueryable<Booking>(null).OrderByDescending(c => c.PickupDateTime);

                var query = (from a in data1

                             select new
                             {
                                Id = a.Id,
                                Bid=a.IsBidding,
                                DriverId=a.DriverId,
                                RefNumber = a.BookingNo,
                                PickupDate = a.PickupDateTime,
                                PickupPoint = a.FromAddress,
                                Destination = a.ToAddress,
                                Driver = a.Fleet_Driver.DriverNo,
                                Fare = a.FareRate,
                              //  TotalBids = a.Booking_Biddings.Count,
                                StatusId=a.BookingStatusId
                           }).ToList();

                DateTime prevDates = dt.Value.AddDays(-3);

               int  DaysInTodayBooking = AppVars.objPolicyConfiguration.DaysInTodayBooking.ToInt();
                // var pendingQuery1 = query.Where(a => (a.PickupDateTemp >= dt && a.PickupDateTemp <= dtTom) && (a.StatusId == Enums.BOOKINGSTATUS.WAITING || a.StatusId == Enums.BOOKINGSTATUS.PENDING));
                var pendingQuery2 = query.Where(a => (a.PickupDate >= prevDates && a.PickupDate.Value.Date <= dt.Value.AddDays(DaysInTodayBooking))
                                    && (a.StatusId == Enums.BOOKINGSTATUS.WAITING || a.StatusId == Enums.BOOKINGSTATUS.PENDING
                                       || a.StatusId == Enums.BOOKINGSTATUS.NOSHOW));

                // var pendingResult = pendingQuery1.Union(pendingQuery2).OrderBy(c => c.PickupDateTemp).ToList();

                int rowIndex = grdPendingJobs.CurrentRow != null ? grdPendingJobs.CurrentRow.Index : -1;


                //grdPendingJobs.DataSource = pendingQuery2.OrderBy(c => c.PickupDate).ToList();

                //grdPendingJobs.CurrentRow = grdPendingJobs.Rows.FirstOrDefault(c => c.Index == rowIndex);          

                grdPreBookings.DataSource = query.Where(a => a.PickupDate.ToDate() > dt && (a.StatusId == Enums.BOOKINGSTATUS.WAITING || a.StatusId == Enums.BOOKINGSTATUS.PENDING))
                                                .OrderBy(c => c.PickupDate).ToList();







                List<long?> jobIds=grdPendingJobs.Rows.Select(c=>c.Cells["Id"].Value.ToLongorNull()).ToList();

           
                           

                   var todaysBiddings = from a in  General.GetQueryable<Booking_Bidding>(c => jobIds.Contains(c.JobId ))

                      


                               select new
                               {
                                   Id = a.JobId,
                                   DriverId = a.DriverId,                               
                                 
                                   Driver = a.Fleet_Driver.DriverNo,
                                   Name=a.Fleet_Driver.DriverName,
                                //   Fare = a.Booking.FareRate,
                                   Bid = a.BidRate
                               };


                GridViewTemplate template = new GridViewTemplate();

                if (grdPendingJobs.Templates.Count == 1)
                    template = grdPendingJobs.Templates[0];
                else
                {
                    template.AllowAddNewRow = false;
                    this.grdPendingJobs.Templates.Add(template);
                }
                template.DataSource = todaysBiddings.ToList();
            

                template.Columns["Id"].IsVisible = false;
                template.Columns["DriverId"].IsVisible = false;

                template.Columns["Driver"].HeaderText ="No";

                template.Columns["Driver"].Width = 80;
                template.Columns["Name"].Width = 140;

                template.Columns["Bid"].Width = 100;
                template.Columns["Bid"].HeaderText = "Bid (£)";




                  jobIds = grdPreBookings.Rows.Select(c => c.Cells["Id"].Value.ToLongorNull()).ToList();




                var preBiddings = from a in General.GetQueryable<Booking_Bidding>(c => jobIds.Contains(c.JobId))




                                     select new
                                     {
                                         Id = a.JobId,
                                         DriverId = a.DriverId,

                                         Driver = a.Fleet_Driver.DriverNo,
                                         Name = a.Fleet_Driver.DriverName,
                                         //   Fare = a.Booking.FareRate,
                                         Bid = a.BidRate
                                     };


                template = new GridViewTemplate();

                if (grdPreBookings.Templates.Count == 1)
                    template = grdPreBookings.Templates[0];
                else
                {
                    template.AllowAddNewRow = false;
                    this.grdPreBookings.Templates.Add(template);
                }
                template.DataSource = preBiddings.ToList();


                template.Columns["Id"].IsVisible = false;
                template.Columns["DriverId"].IsVisible = false;

                template.Columns["Driver"].HeaderText = "No";

                template.Columns["Driver"].Width = 80;
                template.Columns["Name"].Width = 140;

                template.Columns["Bid"].Width = 100;
                template.Columns["Bid"].HeaderText = "Bid (£)";




                grdPendingJobs.Rows.ToList().ForEach(c => c.Cells["chkBid"].Value = c.Cells["Bid"].Value);
                grdPreBookings.Rows.ToList().ForEach(c => c.Cells["chkBid"].Value = c.Cells["Bid"].Value);



               


            }
            catch (Exception ex)
            {
             //   ENUtils.ShowMessage(ex.Message);

            }

        }


        private void frmBiddings_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size == new Size(900, 600))
            {
                grdPendingJobs.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
                FormatGrid();
            }
            else
            {


                grdPendingJobs.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            }

            PopulateData();

        }

      

        private void FormatGrid()
        {
          //  AddCommandColumn(grdPendingJobs, "btnShowBids", "Show Bids");

            grdPendingJobs.Columns["Bid"].IsVisible = false;

            grdPendingJobs.Columns["Id"].IsVisible = false;
            grdPendingJobs.Columns["StatusId"].IsVisible = false;
            grdPendingJobs.Columns["DriverId"].IsVisible = false;

            grdPendingJobs.Columns["RefNumber"].HeaderText = "Ref #";
            grdPendingJobs.Columns["RefNumber"].Width = 60;

            grdPendingJobs.Columns["PickupDate"].HeaderText = "Pickup Date/Time";
            grdPendingJobs.Columns["PickupDate"].Width = 140;

            (grdPendingJobs.Columns["PickupDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdPendingJobs.Columns["PickupDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdPendingJobs.Columns["PickupPoint"].Width = 230;
            grdPendingJobs.Columns["Destination"].Width = 230;

         //   grdPendingJobs.Columns["Driver"].HeaderText = "Driver No";
            grdPendingJobs.Columns["Driver"].Width = 60;


            grdPendingJobs.Columns["Fare"].HeaderText = "Fare (£)";
            grdPendingJobs.Columns["Fare"].Width = 80;
            grdPendingJobs.Columns["TotalBids"].Width = 80;
            grdPendingJobs.Columns["TotalBids"].HeaderText = "Total Bids";

  //          ((grdPendingJobs.Columns["Bid"]) as GridViewCheckBoxColumn).ReadOnly=false;
    //        grdPendingJobs.AllowEditRow = true;


           // AddCommandColumn(grdPreBookings, "btnShowBids", "Show Bids");

            grdPreBookings.Columns["Bid"].IsVisible = false;

            grdPreBookings.Columns["Id"].IsVisible = false;
            grdPreBookings.Columns["StatusId"].IsVisible = false;
            grdPreBookings.Columns["DriverId"].IsVisible = false;

            grdPreBookings.Columns["RefNumber"].HeaderText = "Ref #";
            grdPreBookings.Columns["RefNumber"].Width = 60;

            grdPreBookings.Columns["PickupDate"].HeaderText = "Pickup Date/Time";
            grdPreBookings.Columns["PickupDate"].Width = 140;

            (grdPreBookings.Columns["PickupDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdPreBookings.Columns["PickupDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdPreBookings.Columns["TotalBids"].Width = 80;
            grdPreBookings.Columns["TotalBids"].HeaderText = "Total Bids";


            grdPreBookings.Columns["PickupPoint"].Width = 230;
            grdPreBookings.Columns["Destination"].Width = 230;

          //  grdPreBookings.Columns["Driver"].HeaderText = "Driver No";
            grdPreBookings.Columns["Driver"].Width = 60;


            grdPreBookings.Columns["Fare"].HeaderText = "Fare (£)";
            grdPreBookings.Columns["Fare"].Width = 80;

            //grdLister.Columns["Bid"].HeaderText = "Bid (£)";
            //grdLister.Columns["Bid"].Width = 80;

            //grdLister.Columns["Accept"].Width = 80;
        }

        private void btnSaveBooking_Click(object sender, EventArgs e)
        {
          //   PDAClass.JobsForBidding(grdPendingJobs.Rows.Union(grdPreBookings.Rows).ToList());
             this.Close();
        }

        //public  void PopulateData()
        //{
        //    try
        //    {


        //        var data1 = General.GetQueryable<Booking_Bidding>(c => c.Booking.DriverId == null && c.Booking.BookingStatusId == Enums.BOOKINGSTATUS.WAITING || c.Booking.BookingStatusId == Enums.BOOKINGSTATUS.PENDING)
        //                    .OrderByDescending(c => c.Booking.PickupDateTime).AsEnumerable();

        //        var query = from a in data1

        //                    where a.Booking.PickupDateTime.Value.Date == DateTime.Now.Date


        //                    select new
        //                    {
        //                        Id = a.Id,
        //                        JobId=a.JobId,
        //                        DriverId=a.DriverId,
        //                        RefNumber = a.Booking.BookingNo,
        //                        PickupDate = a.Booking.PickupDateTime,
        //                        PickupPoint = a.Booking.FromAddress,
        //                        Destination = a.Booking.ToAddress,
        //                        Driver = a.Fleet_Driver.DriverNo,
        //                        Fare = a.Booking.FareRate,
        //                        Bid = a.BidRate
        //                    };



        //        grdPendingJobs.DataSource = query.ToList();


              

        //    }
        //    catch (Exception ex)
        //    {


        //    }
      


        //}


        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }

        string cellValue = string.Empty;
        void grdPendingJobs_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {



                if (e.CellElement is GridHeaderCellElement)
                {
                    //    e.CellElement
                    e.CellElement.BorderColor = _HeaderRowBorderColor;
                    e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                    e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                    e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                    // e.CellElement.DrawBorder = false;
                    e.CellElement.BackColor = _HeaderRowBackColor;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.Font = newFont;
                    e.CellElement.ForeColor = Color.White;
                    e.CellElement.DrawFill = true;

                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                }

                else if (e.CellElement is GridFilterCellElement)
                {



                    e.CellElement.Font = oldFont;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.White;
                    e.CellElement.RowElement.BackColor = Color.White;
                    e.CellElement.RowElement.NumberOfColors = 1;

                    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                    e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                    e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                }

                else if (e.CellElement is GridDataCellElement)
                {


                    e.CellElement.ToolTipText = e.CellElement.Text;

                    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                    e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                    e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                    e.CellElement.ForeColor = Color.Black;

                    e.CellElement.Font = oldFont;         



                    e.CellElement.DrawFill = false;



                    if (e.Column.Name == "TotalBids" && e.CellElement.Value.ToInt() > 0)
                    {
                     
                            e.CellElement.RowElement.NumberOfColors = 1;
                            e.CellElement.RowElement.DrawFill = true;

                            e.CellElement.RowElement.BackColor = Color.LightYellow;   
                       


                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.DrawFill = true;
                        e.CellElement.Font = newFont;
                        e.CellElement.ForeColor = Color.White;
                        e.CellElement.BackColor = Color.Red;
                    }            
                }
            }
            catch { }
        }
     

    }
}
