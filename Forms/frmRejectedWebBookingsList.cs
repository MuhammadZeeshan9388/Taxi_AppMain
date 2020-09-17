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
    public partial class frmRejectedWebBookingsList : UI.SetupBase
    {

      
         BookingBO objMaster;

         public frmRejectedWebBookingsList()
        {

           

            InitializeComponent();
            this.Load += new EventHandler(frmBookingsList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
          
           
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
             try
             {
                 GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                 string name = gridCell.ColumnInfo.Name.ToLower();

                 GridViewRowInfo row = gridCell.RowElement.RowInfo;
                 long id = row.Cells["Id"].Value.ToLong();

                

                 //bool rtn = false;
                 //if (name == "btndelete")
                 //{
                 //    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                 //    {

                 //        RadGridView grid = gridCell.GridControl;
                 //        grid.CurrentRow.Delete();
                 //    }
                 //}


                 if (name == "btnrecall")
                 {

                     new TaxiDataContext().stp_UpdateJobStatus(id, Enums.BOOKINGSTATUS.PENDING_WEBBOOKING);
                     row.Delete();

                     (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).PopulatePendingWebBookings(null);


                 }
             }
             catch (Exception ex)
             {
                 ENUtils.ShowMessage(ex.Message);
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

            
             AddCommandColumn("btnReCall", "Re-Call", 70);

             //if (this.CanDelete)
             //{
             //    AddCommandColumn("btnDelete", "Delete", 70);

             //    //  grdLister.AddDeleteColumn();
             //   // grdLister.Columns["btnDelete"].Width = 70;
             //}

             UI.GridFunctions.SetFilter(grdLister);


             grdLister.AllowEditRow = true;

           
             grdLister.Columns["RefNumber"].Width = 50;
             grdLister.Columns["RefNumber"].HeaderText = "Ref #";
             grdLister.Columns["Fare"].Width = 70;
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



             dtpFromDate.Value = null;
             dtpToDate.Value = null;
         }

         void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
         {
             ViewDetailForm();
         }

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




               
                var data1 = General.GetQueryable<Booking>(c =>
                            (
                            (fromDate==null || c.PickupDateTime.Value.Date>=fromDate) && (toDate==null || c.PickupDateTime.Value.Date<=toDate)

                            )
                                      
                    
                    && (c.BookingTypeId==Enums.BOOKING_TYPES.WEB || c.BookingTypeId==Enums.BOOKING_TYPES.ONLINE)
                    

                    && (c.BookingStatusId==Enums.BOOKINGSTATUS.REJECTED)

                    )


                               .OrderByDescending(c => c.PickupDateTime);



                 


                    var query = (from a in data1


                                 select new
                                 {
                                     Id = a.Id,
                                     RefNumber = a.BookingNo,
                                     BookingDate = a.BookingDate,
                                     PickupDate = a.PickupDateTime,
                                     Passenger = a.CustomerName,
                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     Fare = a.FareRate,
                                     Account = a.Gen_Company.CompanyName,
                                     Driver = a.Fleet_Driver.DriverNo,
                                     DriverId = a.DriverId,
                                     Vehicle = a.Fleet_VehicleType.VehicleType,
                                     Status = a.BookingStatus.StatusName,
                                     StatusColor = a.BookingStatus.BackgroundColor,
                                     BookingTypeId = a.BookingTypeId,
                                     VehicleBgColor = a.Fleet_VehicleType.BackgroundColor,
                                     VehicleTextColor = a.Fleet_VehicleType.TextColor,
                                     BackgroundColor1 = a.Gen_Company.BackgroundColor,
                                     TextColor1 = a.Gen_Company.TextColor,
                                     MobileNo = a.CustomerMobileNo,
                                     FromLocTypeId = a.FromLocTypeId,
                                     ToLocTypeId = a.ToLocTypeId,
                                    
                                 }).ToList();
                 

                    this.grdLister.TableElement.BeginUpdate();
                  
                    grdLister.DataSource = query;
                    this.grdLister.TableElement.EndUpdate();
               
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
            dtpFromDate.Value = null;
            dtpToDate.Value = null;
            ddlColumns.SelectedIndex = 0;

        }
      


     

    }
}

