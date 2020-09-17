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
using Telerik.WinControls.UI.Docking;


namespace Taxi_AppMain
{
    public partial class frmAdvanceMultiVehicleList : UI.SetupBase
    {


        public frmAdvanceMultiVehicleList()
        {

         
          
            InitializeComponent();
            this.Load += new EventHandler(frmBookingsList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
  

         
            grdLister.ShowGroupPanel = false;

   
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
           grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
          //  grdLister.VerticalScroll.LargeChange = 100;
         //   grdLister.TableElement.VScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;



             grdLister.CommandCellClick+=new CommandCellClickEventHandler(grdLister_CommandCellClick);
           grdLister.RowsChanging += new GridViewCollectionChangingEventHandler(grdLister_RowsChanging);
          

         
        }

         void grdLister_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
         {
             if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
             {

                

                     AdvanceBookingBO objMaster = new AdvanceBookingBO();
                 
                     try
                     {
                         objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                         string customer=objMaster.Current.CustomerName.ToStr();
                         string from=objMaster.Current.FromAddress.ToStr();
                         string toAddress=objMaster.Current.ToAddress.ToStr();
                       //  string totalJobs=objMaster.Current.Bookings.Count.ToStr();




                         new TaxiDataContext().stp_DeleteMultiBooking(objMaster.Current.Id, AppVars.LoginObj.UserName.ToStr());

                      
                        // objMaster.Delete(objMaster.Current);




                      //   new TaxiDataContext().stp_AddLog("MULTI BOOKING DELETED :Total bookings : "+totalJobs + " , CUST : " + customer + " , Pickup : " + from + " , Destination : " + toAddress, AppVars.LoginObj.UserName.ToStr(), AppVars.LoginObj.UserName.ToStr());

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

       
         void EditFareItem1_Click(object sender, EventArgs e)
         {
             try
             {
                 if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                 {
                     frmEditFare frm = new frmEditFare(grdLister.CurrentRow.Cells["Id"].Value.ToLong(), 0);
                     frm.ShowDialog();
                     frm.Dispose();

                 }
             }
             catch (Exception ex)
             {
                 ENUtils.ShowMessage(ex.Message);

             }
         }

         private void AddDeleteColumn(RadGridView grid)
         {
             GridViewCommandColumn col = new GridViewCommandColumn();
             col.BestFit();

             col.Name = "btnDelete";
             col.UseDefaultText = true;
             col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
             col.DefaultText = "Delete";
             col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

             grid.Columns.Add(col);

         }


    
         void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

                 //else if (e.CellElement is GridFilterCellElement)
                 //{
                 //    e.CellElement.Font = oldFont;
                 //    e.CellElement.NumberOfColors = 1;
                 //    e.CellElement.BackColor = Color.White;
                 //    e.CellElement.RowElement.BackColor = Color.White;
                 //    e.CellElement.RowElement.NumberOfColors = 1;

                 //    e.CellElement.BorderColor = Color.DarkSlateBlue;
                 //    e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                 //    e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                 //    e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                 //    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                 //}
                 //else if (e.CellElement is GridRowHeaderCellElement)
                 //{

                 //    if (e.CellElement is GridTableHeaderCellElement)
                 //    {

                 //        e.CellElement.BorderColor = _HeaderRowBorderColor;
                 //        e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                 //        e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                 //        e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                 //        // e.CellElement.DrawBorder = false;
                 //        e.CellElement.BackColor = _HeaderRowBackColor;
                 //        e.CellElement.NumberOfColors = 1;
                 //        e.CellElement.Font = newFont;
                 //        e.CellElement.ForeColor = Color.White;
                 //        e.CellElement.DrawFill = true;

                 //        e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                 //    }
                 //    else if (e.CellElement is GridRowHeaderCellElement && e.Row is GridViewFilteringRowInfo)
                 //    {

                 //        e.CellElement.Font = oldFont;
                 //        e.CellElement.NumberOfColors = 1;
                 //        e.CellElement.BackColor = Color.White;
                 //        e.CellElement.RowElement.BackColor = Color.White;
                 //        e.CellElement.RowElement.NumberOfColors = 1;

                 //        e.CellElement.BorderColor = Color.DarkSlateBlue;
                 //        e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                 //        e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                 //        e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                 //        e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                 //    }

                 //    else
                 //    {

                 //        e.CellElement.BackColor = Color.FromArgb(e.Row.Cells["SubCompanyBgColor"].Value.ToInt());
                 //        e.CellElement.NumberOfColors = 1;
                 //        e.CellElement.BorderColor = Color.DarkSlateBlue;
                 //        e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                 //        e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                 //        e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                 //        e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                 //        e.CellElement.DrawFill = true;

                 //    }
                 //}


              
            // }
           
         }



         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             string name = gridCell.ColumnInfo.Name.ToLower();

             GridViewRowInfo row = gridCell.RowElement.RowInfo;
             long id = row.Cells["Id"].Value.ToLong();

             int driverId = row.Cells["DriverId"].Value.ToInt();

             bool rtn = false;
             if (name == "btndelete")
             {
                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                 {

                     RadGridView grid = gridCell.GridControl;
                     grid.CurrentRow.Delete();
                 }
             }
             else if (name == "btnrecall")
             {
                 if (row.Cells["Status"].Value.ToStr() == "POB" || row.Cells["Status"].Value.ToStr() == "STC")
                     
                 {





                     ENUtils.ShowMessage("Job cannot be Re-Call as driver is on " + row.Cells["Status"].Value.ToStr() + " Status.");
                     return;

                 }
                 else if (row.Cells["StatusId"].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED || row.Cells["StatusId"].Value.ToInt() == Enums.BOOKINGSTATUS.CANCELLED)
                 {

                     
                     if (General.GetQueryable<Booking>(null).Count(c => c.Id == id && (c.AcceptedDateTime != null || c.Fleet_Driver != null && c.Fleet_Driver.HasPDA==true)) > 0)
                     {
                         ENUtils.ShowMessage("Job cannot be Re-Call as driver is on " + row.Cells["Status"].Value.ToStr() + " Status.");
                         return;

                     }

                 }


                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to Re-Call a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                 {

                     new Thread(delegate()
                     {
                         General.ReCallBooking(id, driverId);
                     }).Start(); 
                    
                 }
                 else
                 {

                     return;
                 }
                

             }
             else if (name == "btnredespatch")
             {

         

                rtn= General.ShowDespatchForm(General.GetObject<Booking>(c => c.Id == id));               
                
             }

             if (name == "btnrecall" || name == "btnredespatch")
             {
                 if(name == "btnredespatch" && rtn==false)
                     return;


                 Thread.Sleep(500);
                 PopulateData();

                 (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshActiveData();


          

                // General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
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


             this.InitializeForm("frmAdvanceMultiVehicleList");

             ddlColumns.Items.Add("Passenger");
             ddlColumns.Items.Add("Telephone No");
             ddlColumns.Items.Add("Mobile No");
             ddlColumns.Items.Add("Pickup Point");
             ddlColumns.Items.Add("Destination");       


             ddlColumns.SelectedIndex = 0;

             PopulateData();

             AddDeleteColumn(grdLister);

             grdLister.Columns["Id"].IsVisible = false;
     

             UI.GridFunctions.SetFilter(grdLister);
             grdLister.Font = oldFont;


             (grdLister.Columns["AddOn"] as GridViewDateTimeColumn).SortOrder = RadSortOrder.Descending;

             grdLister.Columns["AddOn"].IsVisible = false;

             grdLister.Columns["RefNumber"].IsVisible=false;
      
             grdLister.Columns["Passenger"].Width = 100;

             grdLister.Columns["ContactNo"].Width = 140;


             grdLister.Columns["From"].Width = 230;
             grdLister.Columns["From"].HeaderText = "Pickup Point";

             grdLister.Columns["To"].Width = 230;
             grdLister.Columns["To"].HeaderText = "Destination";


             grdLister.Columns["BookingDate"].Width = 100;
             grdLister.Columns["BookingDate"].HeaderText = "Booking Date";     


            
         }

         void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
         {
             ViewDetailForm();
         }

         private void ViewDetailForm()
         {

             if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
             {
                 ShowBookingForm(grdLister.CurrentRow.Cells["Id"].Value.ToLong());
             }
             else
             {
                 ENUtils.ShowMessage("Please select a record");
             }
         }


         private void ShowBookingForm(long id)
         {


             frmAdvanceVehicleCustomization frm = new frmAdvanceVehicleCustomization(id);
           
            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();

            frm.Dispose();
            GC.Collect();

         }


      
     
       

        public override void RefreshData()
        {
            ClearFilter();
            PopulateData();
        }


        public override void PopulateData()
        {
            try
            {


              
                

                    string searchTxt = txtSearch.Text.ToLower().Trim();
                    string col = ddlColumns.Text.Trim().ToLower();

                    if (searchTxt.Length < 3)
                        searchTxt = string.Empty;


                    DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                    DateTime? toDate = dtpToDate.Value.ToDateTimeorNull();

                    bool col_name = false;
                    bool col_refNo = false;
                    bool col_telNo = false;
                    bool col_mobileno = false;
                  
                    bool col_pickupPoint = false;
                    bool col_destination = false;
                   

                    if (col == "passenger")
                    {
                        col_name = true;
                    }
                    else if (col == "reference")
                    {
                        col_refNo = true;
                    }
                    else if (col == "telephone no")
                    {
                        col_telNo = true;
                    }

                    else if (col == "mobile no")
                    {
                        col_mobileno = true;
                    }

               
                    else if (col == "pickup point")
                    {
                        col_pickupPoint = true;
                    }

                    else if (col == "destination")
                    {
                        col_destination = true;
                    }



                    if (string.IsNullOrEmpty(searchTxt) && fromDate == null && toDate == null)
                    {

                        //var data1 = 
                        //     .OrderByDescending(c => c.AddOn);


                        var query = (from a in General.GetQueryable<AdvanceBooking>(null)
                                     where a.AdvBookingTypeId ==1
                                     select new
                                     {
                                         Id = a.Id,
                                         AddOn=a.AddOn,
                                         RefNumber = a.AdvanceBookingNo,
                                         BookingDate = a.AddOn,

                                         Passenger = a.CustomerName,
                                         ContactNo = a.CustomerTelephoneNo != null && a.CustomerTelephoneNo != "" ? a.CustomerMobileNo + " - " + a.CustomerTelephoneNo : a.CustomerMobileNo,
                                         From = a.FromAddress,
                                         To = a.ToAddress,

                                     }).ToList();


                    //    grdLister.MasterTemplate.BeginUpdate();
                        grdLister.DataSource = query;

                     //   grdLister.MasterTemplate.EndUpdate();
                    }
                    else
                    {




                        var query = (from a in General.GetQueryable<AdvanceBooking>(c => c.AdvBookingTypeId == 1)

                                     where 

                                     (fromDate != null ||


                                     (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                         || (col_refNo && (a.AdvanceBookingNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                         || (col_telNo && (a.CustomerTelephoneNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                         || (col_mobileno && (a.CustomerMobileNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))

                                         || (col_pickupPoint && (a.FromAddress.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                         || (col_destination && (a.ToAddress.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                         
                                     )



                                     select new
                                     {
                                         Id = a.Id,
                                         AddOn = a.AddOn,
                                         RefNumber = a.AdvanceBookingNo,
                                         BookingDate = a.AddOn,
                                       
                                         Passenger = a.CustomerName,
                                         ContactNo =a.CustomerTelephoneNo!=null && a.CustomerTelephoneNo!="" ? a.CustomerMobileNo + " - " + a.CustomerTelephoneNo:a.CustomerMobileNo,
                                         From = a.FromAddress,
                                         To = a.ToAddress,
                                       

                                     }).ToList();



                    //    grdLister.MasterTemplate.BeginUpdate();
                        grdLister.DataSource = query;

                    //    grdLister.MasterTemplate.EndUpdate();
                    }
                
               
            }
            catch (Exception ex)
            {


            }
        
            
        }




      

        private void btnFind_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Find();

            }
        }



      

        private void Find()
        {

        
           
            PopulateData();
        }


        private void ClearFilter()
        {

          
          
            this.dtpFromDate.Value = null;
            this.dtpToDate.Value = null;
            this.txtSearch.Text = string.Empty;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ClearFilter();
            PopulateData();
        }


        private void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            string name = gridCell.ColumnInfo.Name.ToLower();

            GridViewRowInfo row = gridCell.RowElement.RowInfo;
            long id = row.Cells["Id"].Value.ToLong();

           

       //     bool rtn = false;
            if (name == "btndelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
            }


           
        }

      

       

      
        
      


     

    }
}

