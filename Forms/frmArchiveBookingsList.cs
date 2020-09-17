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


namespace Taxi_AppMain
{
    public partial class frmArchiveBookingsList : UI.SetupBase
    {

        RadGridViewExcelExporter exporter = null;

         BookingBO objMaster;

         public frmArchiveBookingsList()
        {

           

            InitializeComponent();
            this.Load += new EventHandler(frmBookingsList_Load);
          //  grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
          
           
            objMaster = new BookingBO();

            this.SetProperties((INavigation)objMaster);
            grdLister.ShowGroupPanel = false;

   
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.VerticalScroll.LargeChange = 100;
            grdLister.TableElement.VScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
      
                 
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

       
      

         void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
                 else if (e.CellElement is GridRowHeaderCellElement)
                 {

                     if (e.CellElement is GridTableHeaderCellElement)
                     {

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
                     else if (e.CellElement is GridRowHeaderCellElement && e.Row is GridViewFilteringRowInfo)
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

                     else
                     {

                         e.CellElement.BackColor = Color.FromArgb(e.Row.Cells["SubCompanyBgColor"].Value.ToInt());
                         e.CellElement.NumberOfColors = 1;
                         e.CellElement.BorderColor = Color.DarkSlateBlue;
                         e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                         e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                         e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                         e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                         e.CellElement.DrawFill = true;

                     }



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

                     if (e.CellElement.RowElement.IsSelected == true)
                     {

                         e.CellElement.RowElement.NumberOfColors = 1;
                         e.CellElement.RowElement.BackColor = Color.DeepSkyBlue;

                         e.CellElement.NumberOfColors = 1;
                         e.CellElement.BackColor = Color.DeepSkyBlue;
                         e.CellElement.ForeColor = Color.White;
                         e.CellElement.Font = newFont;

                     }

                     else
                     {
                         e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);

                     }

                     e.CellElement.DrawFill = false;

                    


                     if (e.Column.Name == "Account" && e.CellElement.Value.ToStr() != string.Empty)
                     {


                         e.CellElement.NumberOfColors = 1;
                         e.CellElement.DrawFill = true;


                         string Bgcolor = e.Row.Cells["BackgroundColor1"].Value.ToStr().Trim();
                         string textColor = e.Row.Cells["TextColor1"].Value.ToStr().Trim();

                         if (Bgcolor != string.Empty && textColor != string.Empty)
                         {

                             Color bgClr = Color.FromArgb(Bgcolor.ToInt());
                             Color txtClr = Color.FromArgb(textColor.ToInt());

                             e.CellElement.BackColor = bgClr;
                             e.CellElement.ForeColor = txtClr;

                         }
                         else
                         {
                             e.CellElement.ForeColor = Color.White;
                             e.CellElement.BackColor = Color.Crimson;


                         }
                     }

                     else if (e.Column.Name == "Vehicle")
                     {
                         e.CellElement.NumberOfColors = 1;
                         e.CellElement.DrawFill = true;


                         string Bgcolor = e.Row.Cells["VehicleBgColor"].Value.ToStr().Trim();
                         string textColor = e.Row.Cells["VehicleTextColor"].Value.ToStr().Trim();

                         if (Bgcolor != string.Empty && textColor != string.Empty)
                         {

                             e.CellElement.BackColor = Color.FromArgb(Bgcolor.ToInt());
                             e.CellElement.ForeColor = Color.FromArgb(textColor.ToInt());

                         }
                     }


                     else if (e.Column.Name == "From")
                     {
                         if (e.Row.Cells["FromLocTypeId"].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                         {

                             e.CellElement.NumberOfColors = 1;
                             e.CellElement.DrawFill = true;

                             e.CellElement.BackColor = Color.GreenYellow;
                             e.CellElement.ForeColor = Color.Black;

                         }

                     }

                     else if (e.Column.Name == "To")
                     {
                         if (e.Row.Cells["ToLocTypeId"].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                         {

                             e.CellElement.NumberOfColors = 1;
                             e.CellElement.DrawFill = true;

                             e.CellElement.BackColor = Color.GreenYellow;
                             e.CellElement.ForeColor = Color.Black;

                         }
                     }

                     else if (e.Column.Name == "Status")
                     {

                         e.CellElement.NumberOfColors = 1;
                         e.CellElement.BackColor = Color.FromArgb(e.CellElement.RowInfo.Cells["StatusColor"].Value.ToInt());
                         e.CellElement.ForeColor = Color.Black;

                         e.CellElement.DrawFill = true;
                     }



                 }
             }
             catch { }
         }



         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             string name = gridCell.ColumnInfo.Name.ToLower();

             GridViewRowInfo row = gridCell.RowElement.RowInfo;
             long id = row.Cells["Id"].Value.ToLong();

             int driverId = row.Cells["DriverId"].Value.ToInt();

          //   bool rtn = false;
             if (name == "btndelete")
             {
                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                 {

                     RadGridView grid = gridCell.GridControl;
                     grid.CurrentRow.Delete();
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

             this.InitializeForm("frmBooking");
             ClearFilter();

             ddlColumns.Items.Add(new RadListDataItem { Text = "All", Value = 0 , Selected=true});
             ddlColumns.Items.Add(new RadListDataItem { Text = "Waiting", Value = Enums.BOOKINGSTATUS.WAITING, Selected = false });
             ddlColumns.Items.Add(new RadListDataItem { Text = "Cancelled", Value = Enums.BOOKINGSTATUS.CANCELLED, Selected = false });
             ddlColumns.Items.Add(new RadListDataItem { Text = "Despatched", Value = Enums.BOOKINGSTATUS.DISPATCHED, Selected = false });


             grdLister.TableElement.RowHeight = 32;



             PopulateData();         

           
             grdLister.Columns["MobileNo"].IsVisible = false;
             grdLister.Columns["Id"].IsVisible = false;
             grdLister.Columns["DriverId"].IsVisible = false;

             grdLister.Columns["VehicleBgColor"].IsVisible = false;
             grdLister.Columns["VehicleTextColor"].IsVisible = false;

             grdLister.Columns["BackgroundColor1"].IsVisible = false;
             grdLister.Columns["TextColor1"].IsVisible = false;


             grdLister.Columns["BookingTypeId"].IsVisible = false;


             grdLister.Columns["StatusColor"].IsVisible = false;
             grdLister.Columns["FromLocTypeId"].IsVisible = false;
             grdLister.Columns["ToLocTypeId"].IsVisible = false;







          //   AddCommandColumn("btnReDespatch", "Despatch", 70);

             //if (this.CanDelete)
             //{
             //  //  grdLister.AddDeleteColumn();
             //    grdLister.Columns["btnDelete"].Width = 70;
             //}

             UI.GridFunctions.SetFilter(grdLister);


             grdLister.AllowEditRow = true;

           
             grdLister.Columns["RefNumber"].Width = 50;
             grdLister.Columns["RefNumber"].HeaderText = "Ref #";
             grdLister.Columns["Fare"].Width = 60;
             grdLister.Columns["Fare"].HeaderText = "Fare £";
             grdLister.Columns["Vehicle"].Width = 70;
             grdLister.Columns["Driver"].Width = 50;

             grdLister.Columns["Status"].Width = 80;
             grdLister.Columns["Passenger"].Width = 70;

             grdLister.Columns["From"].Width = 110;
             grdLister.Columns["From"].HeaderText = "Pickup Point";

             grdLister.Columns["To"].Width = 110;
             grdLister.Columns["To"].HeaderText = "Destination";


             grdLister.Columns["BookingDate"].Width = 90;
             grdLister.Columns["BookingDate"].HeaderText = "Booking Date";


             (grdLister.Columns["PickupDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
             (grdLister.Columns["PickupDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


             grdLister.Columns["PickupDate"].Width = 130;
             grdLister.Columns["PickupDate"].HeaderText = "Pickup Date-Time";


             grdLister.Columns["Account"].Width = 60;
             grdLister.Columns["Account"].HeaderText = "A/C";



             grdLister.Columns["ContactNo"].Width = 100;
             grdLister.Columns["ContactNo"].HeaderText = "Contact No";

           
         }

         //void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
         //{
         //    ViewDetailForm();
         //}

         private void ViewDetailForm()
         {

             if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
             {
                 ShowBookingForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
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

                int bookingstatusId = ddlColumns.SelectedValue.ToInt();

                DateTime? fromDate=dtpFromDate.Value.ToDateorNull();
                DateTime? toDate=dtpToDate.Value.ToDateorNull();

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var query = (from a in db.GetTable<ArchiveBooking>().Where(c => (bookingstatusId == 0 || c.BookingStatusId == bookingstatusId) &&
                               ((fromDate == null || c.PickupDateTime.Value.Date >= fromDate) && (toDate == null || c.PickupDateTime.Value.Date <= toDate)
                               )

                                )

                                 join b in db.GetTable<Fleet_VehicleType>() on a.VehicleTypeId equals b.Id
                                 join e in db.GetTable<BookingStatus>() on a.BookingStatusId equals e.Id

                                 join c in db.GetTable<Fleet_Driver>() on a.DriverId equals c.Id into table2
                                 from z in table2.DefaultIfEmpty()
                                 join d in db.GetTable<Gen_Company>() on a.CompanyId equals d.Id into table3
                                 from acc in table3.DefaultIfEmpty()

                                 select new
                                 {
                                     Id = a.Id,
                                     RefNumber = a.BookingNo,
                                     BookingDate = a.BookingDate,
                                     PickupDate = a.PickupDateTime,
                                     Passenger = a.CustomerName,
                                     ContactNo = a.CustomerMobileNo != null && a.CustomerMobileNo != "" ? a.CustomerMobileNo + Environment.NewLine + a.CustomerPhoneNo : a.CustomerPhoneNo,
                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     Fare = a.FareRate,
                                     Account = acc != null ? acc.CompanyName : "",
                                     Driver = z != null ? z.DriverNo : "",
                                     DriverId = a.DriverId,
                                     Vehicle = b.VehicleType,
                                     Status = e.StatusName,
                                     StatusColor = e.BackgroundColor,
                                     BookingTypeId = a.BookingTypeId,
                                     VehicleBgColor = b.BackgroundColor,
                                     VehicleTextColor = b.TextColor,
                                     BackgroundColor1 = acc.BackgroundColor,
                                     TextColor1 = acc.TextColor,
                                     MobileNo = a.CustomerMobileNo,
                                     FromLocTypeId = a.FromLocTypeId,
                                     ToLocTypeId = a.ToLocTypeId,

                                 }).OrderByDescending(c => c.PickupDate).ToList();


                //    this.grdLister.TableElement.BeginUpdate();

                    grdLister.DataSource = query;
                //    this.grdLister.TableElement.EndUpdate();
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
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Selected Booking(s) ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    foreach (GridViewRowInfo row in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool()))
                    {
                        objMaster = new BookingBO();

                        objMaster.GetByPrimaryKey(row.Cells["Id"].Value.ToInt());
                        if (objMaster.Current != null)
                        {
                            objMaster.Delete(objMaster.Current);
                        }
                    }

                    PopulateData();
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



       

      
        private void btnExport_Click(object sender, EventArgs e)
        {

            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    if (radGridView1 == null)
                       InitializeExportGrid();

                    DateTime recentDays = DateTime.Now.AddDays(-1);

                 //   radGridView1.VirtualMode = true;
                    radGridView1.Columns.Clear();
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("RefNo", "RefNo"));

                    radGridView1.Columns.Add(new GridViewTextBoxColumn("PickupDate","PickupDate"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("From", "From"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("To", "To"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Customer", "Customer"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Telephone", "Telephone"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("MobileNo", "MobileNo"));

                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Account", "Account"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Vehicle", "Vehicle"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Price", "Price"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Drv", "Drv"));

                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Status", "Status"));


                    int bookingstatusId = ddlColumns.SelectedValue.ToInt();



                    DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                    DateTime? toDate = dtpToDate.Value.ToDateorNull();


                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        var query = (from a in db.GetTable<ArchiveBooking>().Where(c => (bookingstatusId == 0 || c.BookingStatusId == bookingstatusId) &&
                                   ((fromDate == null || c.PickupDateTime.Value.Date >= fromDate) && (toDate == null || c.PickupDateTime.Value.Date <= toDate)
                                   )


                                    )


                                     join b in db.GetTable<Fleet_VehicleType>() on a.VehicleTypeId equals b.Id
                                     join ee in db.GetTable<BookingStatus>() on a.BookingStatusId equals ee.Id

                                     join c in db.GetTable<Fleet_Driver>() on a.DriverId equals c.Id into table2
                                     from z in table2.DefaultIfEmpty()
                                     join d in db.GetTable<Gen_Company>() on a.CompanyId equals d.Id into table3
                                     from acc in table3.DefaultIfEmpty()


                                     select new
                                     {
                                         Id = a.Id,
                                         RefNumber = a.BookingNo,
                                         BookingDate = a.BookingDate,
                                         PickupDate = a.PickupDateTime,
                                         Passenger = a.CustomerName,
                                         TelephoneNo = a.CustomerPhoneNo,
                                         MobileNo = a.CustomerMobileNo,
                                         From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                         To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                         Fare = a.FareRate,
                                         Account = acc != null ? acc.CompanyName : "",
                                         Driver = z != null ? z.DriverNo : "",
                                         DriverId = a.DriverId,
                                         Vehicle = b.VehicleType,
                                         Status = ee.StatusName,
                                         StatusColor = ee.BackgroundColor,
                                         BookingTypeId = a.BookingTypeId,
                                         VehicleBgColor = b.BackgroundColor,
                                         VehicleTextColor = b.TextColor,
                                         BackgroundColor1 = acc.BackgroundColor,
                                         TextColor1 = acc.TextColor,

                                         FromLocTypeId = a.FromLocTypeId,
                                         ToLocTypeId = a.ToLocTypeId,

                                     }).OrderByDescending(c => c.PickupDate).ToList();






                        radGridView1.RowCount = query.Count;
                        for (int i = 0; i < query.Count; i++)
                        {
                            radGridView1.Rows[i].Cells["RefNo"].Value = query[i].RefNumber;

                            radGridView1.Rows[i].Cells["PickupDate"].Value = " " + string.Format("{0:dd/MM/yyyy HH:mm}", query[i].PickupDate) + " ";
                            radGridView1.Rows[i].Cells["From"].Value = query[i].From;
                            radGridView1.Rows[i].Cells["To"].Value = query[i].To;
                            radGridView1.Rows[i].Cells["Customer"].Value = query[i].Passenger;
                            radGridView1.Rows[i].Cells["Telephone"].Value = " " + query[i].TelephoneNo + " ";
                            radGridView1.Rows[i].Cells["MobileNo"].Value = " " + query[i].MobileNo + " ";

                            radGridView1.Rows[i].Cells["Account"].Value = query[i].Account;
                            radGridView1.Rows[i].Cells["Vehicle"].Value = query[i].Vehicle;
                            radGridView1.Rows[i].Cells["Drv"].Value = query[i].Driver;

                            radGridView1.Rows[i].Cells["Price"].Value = query[i].Fare;
                            radGridView1.Rows[i].Cells["Status"].Value = query[i].Status;

                        }


                        radGridView1.Columns["RefNo"].HeaderText = "Ref #";

                        radGridView1.Columns["PickupDate"].HeaderText = "Pickup Date-Time";

                        radGridView1.Columns["From"].HeaderText = "Pick-up Address";
                        radGridView1.Columns["To"].HeaderText = "Drop-off Address";


                        exporter = new RadGridViewExcelExporter();
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                        worker.RunWorkerAsync(saveFileDialog1.FileName);
                        exporter.Progress += new ProgressHandler(exportProgress);

                        this.btnExport.Enabled = false;

                    }
                    
                  
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.IsDisposed)
            {
                e.Cancel = true;
                return;

            }

            exporter.Export(this.radGridView1, (String)e.Argument, "InComplete Bookings");
        }

        //Update the progress bar with the export progress    
        private void exportProgress(object sender, ProgressEventArgs e)
        {

            if (this.IsDisposed)
                return;
            // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(
                delegate
                {
                    if (e.ProgressValue <= 100)
                    {
                        radProgressBar1.Value1 = e.ProgressValue;
                    }
                    else
                    {
                        radProgressBar1.Value1 = 100;
                    }
                }));
            }
            else
            {
                if (e.ProgressValue <= 100)
                {
                    radProgressBar1.Value1 = e.ProgressValue;
                }
                else
                {
                    radProgressBar1.Value1 = 100;
                }
            }
        }
        // when the worker finishes the export, we can do some post processing   
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;
           
            ENUtils.ShowMessage("Export successfully.");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {

            ClearFilter();

            PopulateData();

        }


        private void ClearFilter()
        {
            dtpFromDate.Value = DateTime.Now.AddYears(-2);
            dtpToDate.Value = DateTime.Now.GetEndOfCurrentWeek();
            ddlColumns.SelectedIndex = 0;

        }
      


     

    }
}

