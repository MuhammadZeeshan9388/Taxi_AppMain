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
    public partial class frmInCompleteBookingsList : UI.SetupBase
    {

        RadGridViewExcelExporter exporter = null;

         BookingBO objMaster;

         int pageSize = 0;
         RadDropDownMenu EditFare = null;
         public frmInCompleteBookingsList()
        {

            InitializeLoading();          
                 

            InitializeComponent();
            this.Load += new EventHandler(frmBookingsList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
          
           
            objMaster = new BookingBO();

            this.SetProperties((INavigation)objMaster);
            grdLister.ShowGroupPanel = false;

   
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.VerticalScroll.LargeChange = 100;
            grdLister.TableElement.VScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
      
            pageSize = AppVars.objPolicyConfiguration.ListingPagingSize.ToInt();

            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);           
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
                     frmSMSAll frm = new frmSMSAll(grdLister.CurrentRow.Cells["MobileNo"].Value.ToStr());
                     frm.ShowDialog();
                     frm.Dispose();
                 }
             }
             catch (Exception ex)
             {
                 ENUtils.ShowMessage(ex.Message);
             }
         }



         void SMSJob_Click(object sender, EventArgs e)
         {
             try
             {
                 long jobId = grdLister.CurrentRow.Cells["Id"].Value.ToLong();


                
                
               


                 if (jobId > 0)
                 {

                     Booking objJob = General.GetObject<Booking>(c => c.Id == jobId);
                     if (objJob != null)
                     {
                         frmSMSAll frmSMS = new frmSMSAll("", GetMessage(AppVars.objPolicyConfiguration.DespatchTextForDriver.ToStr(), objJob),objJob.SMSType.ToInt());

                         frmSMS.StartPosition = FormStartPosition.CenterScreen;
                         frmSMS.ShowDialog();

                         Thread.Sleep(500);
                         frmSMS.Dispose();

                     }
                 }




             }
             catch (Exception ex)
             {
                 ENUtils.ShowMessage(ex.Message);

             }
         }

         private string GetMessage(string message, Booking objBooking)
         {
             try
             {


                 string msg = message;

                 object propertyValue = string.Empty;
                 foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                 {


                     switch (tag.TagObjectName)
                     {
                         case "booking":

                             if (tag.TagPropertyValue.Contains('.'))
                             {

                                 string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                 object parentObj = objBooking.GetType().GetProperty(val[0]).GetValue(objBooking, null);

                                 if (parentObj != null)
                                 {
                                     propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                 }
                                 else
                                     propertyValue = string.Empty;


                                 break;
                             }
                             else
                             {
                                 if (tag.ConditionNotNull.ToStr() == "BabySeats" && tag.TagPropertyValue.ToStr() == "BabySeats")
                                 {
                                     propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);

                                     if (!string.IsNullOrEmpty(propertyValue.ToStr().Trim()) && propertyValue.ToStr().Contains("<<<"))
                                     {
                                         string[] arr = propertyValue.ToStr().Split(new string[] { "<<<" }, StringSplitOptions.None);

                                         propertyValue = "B Seat 1 : " + arr[0].ToStr() + Environment.NewLine + "B Seat 2 : " + arr[1].ToStr();

                                     }

                                 }
                                 else
                                 {
                                     propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                 }

                                 //if (!string.IsNullOrEmpty(tag.ConditionNotNull) && objBooking.GetType().GetProperty(tag.ConditionNotNull) != null)
                                 //{

                                 //    propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                 //}
                                 //else
                                 //{

                                 //    propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                 //}
                             }


                             if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                             {
                                 propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);
                             }
                             break;


                         case "Booking_ViaLocations":
                             if (tag.TagPropertyValue == "ViaLocValue")
                             {


                                 string[] VilLocs = null;
                                 int cnt = 1;
                                 VilLocs = objBooking.Booking_ViaLocations.Select(c => cnt++.ToStr() + ". " + c.ViaLocValue).ToArray();
                                 if (VilLocs.Count() > 0)
                                 {

                                     string Locations = "VIA POINT(s) : \n" + string.Join("\n", VilLocs);
                                     propertyValue = Locations;
                                 }
                                 else
                                     propertyValue = string.Empty;

                             }
                             break;


                         case "driver":


                             if (tag.TagPropertyValue.Contains('.'))
                             {

                                 string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                 object parentObj = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(val[0]).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);

                                 if (parentObj != null)
                                 {
                                     propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                 }
                                 else
                                     propertyValue = string.Empty;


                                 break;
                             }

                             else
                             {
                                 propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                             }

                             if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                             {
                                 propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                             }
                             break;


                         default:
                             propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                             break;

                     }




                     msg = msg.Replace(tag.TagMemberValue,
                         tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                 }


                 return msg.Replace("\n\n", "\n");
             }
             catch (Exception ex)
             {
                 // ENUtils.ShowMessage(ex.Message);
                 return "";
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

         void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
         {
             try
             {
                 GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                 if (cell == null)
                     return;

                 else if (cell.GridControl.Name == "grdLister")
                 {

                     if (EditFare == null)
                     {
                         EditFare = new RadDropDownMenu();
                         EditFare.BackColor = Color.Orange;

                         RadMenuItem EditFareItem1 = new RadMenuItem("Edit Fare");
                         EditFareItem1.ForeColor = Color.DarkBlue;
                         EditFareItem1.BackColor = Color.Orange;
                         EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                         EditFareItem1.Click += new EventHandler(EditFareItem1_Click);
                         EditFare.Items.Add(EditFareItem1);


                         RadMenuItem EditFareItem2 = new RadMenuItem("Arrival Text");
                         EditFareItem2.ForeColor = Color.DarkBlue;
                         EditFareItem2.BackColor = Color.Orange;
                         EditFareItem2.Font = new Font("Tahoma", 10, FontStyle.Bold);
                         EditFareItem2.Click += new EventHandler(EditFareItem2_Click);
                         EditFare.Items.Add(EditFareItem2);


                         EditFareItem2 = new RadMenuItem("SMS Job Details");
                         EditFareItem2.ForeColor = Color.DarkBlue;
                         EditFareItem2.BackColor = Color.Orange;
                         EditFareItem2.Font = new Font("Tahoma", 10, FontStyle.Bold);
                         EditFareItem2.Click += new EventHandler(SMSJob_Click);
                         EditFare.Items.Add(EditFareItem2);


                         EditFareItem1 = new RadMenuItem("Complete Job");
                         EditFareItem1.ForeColor = Color.Black;
                         EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);
                         EditFareItem1.Click += new EventHandler(ForceCompleteJob_Click);
                         EditFare.Items.Add(EditFareItem1);



                     }

                     e.ContextMenu = EditFare;
                     return;
                 }
             }
             catch (Exception ex)
             {
                 ENUtils.ShowMessage(ex.Message);

             }
         }


         void ForceCompleteJob_Click(object sender, EventArgs e)
         {
             try
             {



                 if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                 {
                     long Id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();


                     frmForceCompleteJob frmComp = new frmForceCompleteJob(General.GetObject<Booking>(c => c.Id == Id));
                     frmComp.StartPosition = FormStartPosition.CenterScreen;
                     frmComp.ShowDialog();
                     frmComp.Dispose();


                     GC.Collect();

                     PopulateData();
                 
                 }





             }
             catch (Exception ex)
             {
                 //   ENUtils.ShowMessage(ex.Message);

             }
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

                     if (e.Column.Name == "RefNumber" || e.Column.Name == "PickUpDate" || e.Column.Name == "Time")
                     {
                         //if (e.Row.Cells["BookingTypeId"].Value.ToInt() == Enums.BOOKING_TYPES.VIP)
                         //{
                         //    e.CellElement.NumberOfColors = 1;
                         //    e.CellElement.DrawFill = true;

                         //    string bgColor = AppVars.objPolicyConfiguration.VIPBookingBackgroundColor.ToStr();

                         //    if (!string.IsNullOrEmpty(bgColor))
                         //    {

                         //        e.CellElement.BackColor = Color.FromArgb(bgColor.ToInt());
                         //    }

                         //}
                         //else if (e.Row.Cells["BookingTypeId"].Value.ToInt() == Enums.BOOKING_TYPES.WEB)
                         //{
                         //    e.CellElement.NumberOfColors = 1;
                         //    e.CellElement.DrawFill = true;

                         //    string bgColor = AppVars.objPolicyConfiguration.WebBookingBackgroundColor.ToStr();

                         //    if (!string.IsNullOrEmpty(bgColor))
                         //    {

                         //        e.CellElement.BackColor = Color.FromArgb(bgColor.ToInt());
                         //    }

                         //}
                     }



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

             bool rtn = false;
             if (name == "btndelete")
             {
                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                 {

                     RadGridView grid = gridCell.GridControl;
                     grid.CurrentRow.Delete();
                 }
             }
            
          
             else if (name == "btnredespatch")
             {
                rtn= General.ShowDespatchForm(General.GetObject<Booking>(c => c.Id == id));               
                
             }

             if (name == "btnredespatch")
             {
                 if(name == "btnredespatch" && rtn==false)
                     return;

                 PopulateData();
               
                 General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
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

             if (this.CanDelete)
             {
                 GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                 col.Width=40;
                 col.AutoSizeMode = BestFitColumnMode.None;
                 col.HeaderText = "";
                 col.Name = "Check";
                 grdLister.Columns.Add(col);
                 btnDeleteSelected.Visible = true;
             }



         

             PopulateData();
             if (this.CanDelete)
             {
                 grdLister.Columns["Check"].Width = 40;
             }

            

             grdLister.Columns["SubCompanyBgColor"].IsVisible = false;
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

            
             AddCommandColumn("btnReDespatch", "Despatch", 70);

             if (this.CanDelete)
             {
                 grdLister.AddDeleteColumn();
                 grdLister.Columns["btnDelete"].Width = 70;
             }

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


             FinishLoading();
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


         void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
         {
             if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
             {
               
                     objMaster = new BookingBO();

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


     
       

        public override void RefreshData()
        {
         
            PopulateData();
        }


        public override void PopulateData()
        {
            try
            {
                DateTime recentDays = DateTime.Now.AddDays(-1).Date;

                var data1 = General.GetQueryable<Booking>(c => (c.PickupDateTime.Value < recentDays)
                    &&
                    (
                    c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || c.BookingStatusId == Enums.BOOKINGSTATUS.WAITING || c.BookingStatusId==Enums.BOOKINGSTATUS.ONHOLD
                    || c.BookingStatusId == Enums.BOOKINGSTATUS.NOTACCEPTED || c.BookingStatusId == Enums.BOOKINGSTATUS.REJECTED || c.BookingStatusId == Enums.BOOKINGSTATUS.BID
                   )
                    
                    && (c.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
                    
                    && (c.PickupDateTime.Value.Date < recentDays)
                    
                    )
                               .OrderByDescending(c => c.PickupDateTime);



                    int cnt = data1.Count();
                    if (skip + pageSize > cnt && cnt - pageSize > 0)
                        skip = cnt - pageSize;
                    else if (cnt <= pageSize)
                        skip = 0;


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
                                     SubCompanyBgColor = a.SubcompanyId != null ? a.Gen_SubCompany.BackgroundColor : -1
                                 }).Skip(skip).Take(pageSize).ToList();

                 

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
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("PickupDate","PickupDate"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("From", "From"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("To", "To"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Customer", "Customer"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Telephone", "Telephone"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Driver", "Driver"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Vehicle", "Vehicle"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Price", "Price"));
   

                    var data1 = General.GetQueryable<Booking>(c => (c.PickupDateTime.Value.Date < recentDays)
                    &&
                    (
                    c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || c.BookingStatusId == Enums.BOOKINGSTATUS.WAITING || c.BookingStatusId==Enums.BOOKINGSTATUS.ONHOLD
                    || c.BookingStatusId == Enums.BOOKINGSTATUS.NOTACCEPTED || c.BookingStatusId == Enums.BOOKINGSTATUS.REJECTED || c.BookingStatusId == Enums.BOOKINGSTATUS.BID
                   )
                    
                    && (c.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
                    
                    && (c.PickupDateTime.Value.Date < recentDays)
                    
                    )
                               .OrderByDescending(c => c.PickupDateTime).AsEnumerable();


                    var query = (from a in data1

                                

                                 select new
                                 {

                                     PickupDate = " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", a.PickupDateTime) + "  ",
                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     Customer = a.CustomerName,
                                     Telephone = a.CustomerPhoneNo != string.Empty ? " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", a.CustomerPhoneNo) + " " : " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", a.CustomerMobileNo) + " ",
                                     Driver = a.Fleet_Driver != null ? a.Fleet_Driver.DriverNo : "",
                                     Vehicle = a.Fleet_VehicleType!=null ? a.Fleet_VehicleType.VehicleType:"",
                                     Price = a.FareRate
                                 }).ToList();

                    //radGridView1.DataSource = query;

                    radGridView1.RowCount = query.Count;
                    for (int i = 0; i < query.Count; i++)
                    {
                        radGridView1.Rows[i].Cells["PickupDate"].Value = query[i].PickupDate;
                        radGridView1.Rows[i].Cells["From"].Value = query[i].From;
                        radGridView1.Rows[i].Cells["To"].Value = query[i].To;
                        radGridView1.Rows[i].Cells["Customer"].Value = query[i].Customer;
                        radGridView1.Rows[i].Cells["Telephone"].Value = query[i].Telephone;
                        radGridView1.Rows[i].Cells["Driver"].Value = query[i].Driver;
                        radGridView1.Rows[i].Cells["Vehicle"].Value = query[i].Vehicle;
                        radGridView1.Rows[i].Cells["Price"].Value = query[i].Price;

                    }


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

      


     

    }
}

