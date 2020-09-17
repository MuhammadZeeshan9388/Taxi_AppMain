using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using SpreadsheetGear.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Telerik.WinControls;
using System.Reflection;

namespace Taxi_AppMain
{
    public partial class frmComcabBooking : UI.SetupBase
    {
        public struct COLS
        {
            public static string ID = "ID";
            public static string BookingReference = "BookingReference";

            public static string PickupDateTime = "PickupDateTime";

            public static string VehicleType = "VehicleType";

            public static string Passenger = "Passenger";
            public static string Account = "Account";

            public static string PhoneNo = "PhoneNo";
            public static string From = "From";
            public static string To = "To";
    
            public static string Fare = "Fare";
            public static string PaymentType = "PaymentType";

            public static string Notes = "Notes";

        }

        BookingBO objMaster = null;

        WorkbookView objWBView = null;
        public frmComcabBooking()
        {
            
          

            InitializeComponent();
            this.Load += new EventHandler(frmComcabBooking_Load);
            objWBView = new WorkbookView();
            objMaster=new BookingBO();
            this.SetProperties((INavigation)objMaster);
            grdBookings.TableElement.RowHeight = 70;
            grdBookings.TableElement.AlternatingRowColor = Color.AliceBlue;
            grdBookings.EnableAlternatingRowColor = true;
            grdBookings.ViewCellFormatting+=new CellFormattingEventHandler(grdBookings_ViewCellFormatting);
            this.InitializeForm("frmBooking");

            grdBookings.ShowRowHeaderColumn = true;
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

        RadButtonElement button = null;
       
        void grdBookings_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

           

            else if (e.CellElement is GridDataCellElement)
            {

                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {

                    button = (RadButtonElement)e.CellElement.Children[0];


                    if (e.Column.Name == "btnDelete")
                    {

                        button.Image = Resources.Resource1.delete;
                    }

                  
                }



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
                 
                    e.CellElement.Font = newFont;




                }


                else
                {
                    e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);

                }            


            }




        }

        private void FormatGrid()
        {


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = "BookingReference";
            col.HeaderText = " Ref #";
            col.FieldName = "Booking Reference";
            col.WrapText = true;
            col.Width = 70;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.ID;
            col.IsVisible = false;
           
            grdBookings.Columns.Add(col);


            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.Name = "PickupDateTime";
            colDate.WrapText = true;
            colDate.HeaderText = "Pickup Date-Time";
            colDate.Width = 140;
            colDate.FieldName = "Pickup Time";
            colDate.CustomFormat = "dd/MM/yyyy HH:mm";
            colDate.FormatString = "{0:dd/MM/yyyy HH:mm}";
            grdBookings.Columns.Add(colDate);


            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "VehicleType";
            colCombo.HeaderText = "Vehicle";
            colCombo.FieldName = "Vehicle Class";
            colCombo.DataSource = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).ToList();
            colCombo.DisplayMember = "VehicleType";
            colCombo.ValueMember = "Id";
            colCombo.Width = 80;
            col.WrapText = true;
            grdBookings.Columns.Add(colCombo);


            col = new GridViewTextBoxColumn();
            col.Name = "Passenger";
            col.HeaderText = "Passenger";
            col.FieldName = "Passenger Name";
            col.WrapText = true;
            col.Width = 100;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = "Account";
            col.HeaderText = "Account";
            col.FieldName = "Booking Account";
            col.WrapText = true;
            col.Width = 120;
            grdBookings.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.Name = "PhoneNo";
            col.HeaderText = "Phone No";
            col.Width = 120;
            col.WrapText = true;
            col.FieldName = "Phone";
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "From";

            col.HeaderText = "Pickup Point";
            col.FieldName = "0";
            col.WrapText = true;
            
            col.Width = 130;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = "To";
            col.WrapText = true;
            col.HeaderText = "Destination";
            col.Width = 130;
            col.FieldName = "1";
            grdBookings.Columns.Add(col);


            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.Name = "Fare";
            colDec.FieldName = "Pricing";

            colDec.DecimalPlaces = 2;
            colDec.Minimum = 0;
            colDec.Maximum = 999999;
            colDec.HeaderText = "Fare";
            colDec.Width = 60;
            grdBookings.Columns.Add(colDec);

            

            col = new GridViewTextBoxColumn();
            col.Name = "PaymentType";
            col.WrapText = true;
            col.FieldName = "Card";

            col.HeaderText = "Payment";
            col.Width = 100;

            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "Notes";
            col.WrapText = true;
            col.FieldName = "Driver Notes";

            col.HeaderText = "Notes";
            col.Width = 120;

            grdBookings.Columns.Add(col);



            ConditionalFormattingObject objSave = new ConditionalFormattingObject();
            objSave.ApplyToRow = true;
            objSave.RowBackColor = Color.LightGreen;
            objSave.ConditionType = ConditionTypes.Greater;
            objSave.TValue1 = "0";
            grdBookings.Columns[COLS.ID].ConditionalFormattingObjectList.Add(objSave);


            ConditionalFormattingObject objError = new ConditionalFormattingObject();
            objError.ApplyToRow = true;
            objError.RowBackColor = Color.Pink;
            objError.ConditionType = ConditionTypes.Equal;
            objError.TValue1 = "0";
            grdBookings.Columns[COLS.ID].ConditionalFormattingObjectList.Add(objError);



            grdBookings.AddDeleteColumn();
            grdBookings.Columns["btnDelete"].PinPosition = PinnedColumnPosition.Right;
            grdBookings.Columns["btnDelete"].IsPinned = true;
            grdBookings.Columns["btnDelete"].Width = 70;


            grdBookings.CommandCellClick+=new CommandCellClickEventHandler(grdBookings_CommandCellClick);
        }

        private void grdBookings_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete ?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {



                    grid.CurrentRow.Delete();
                }
            }
           
        }


        void frmComcabBooking_Load(object sender, EventArgs e)
        {
            FormatGrid();

            //try
            //{
            //    string path= Application.StartupPath + "\\SpreadsheetGear.dll";
            //    var DLL = Assembly.LoadFile(path);
            //    foreach (Type type in DLL.GetExportedTypes())
            //    {
            //        if( type.Name=="WorkbookView")
            //        {
            //            WorkbookView c = (WorkbookView)Activator.CreateInstance(type);
            //        }
            //     //   var c = Activator.CreateInstance(DLL.FullName,"SpreadsheetGear.Windows.Forms.WorkbookView");
            //    //    type.InvokeMember("Output", BindingFlags.InvokeMethod, null, c, new object[] { @"Hello" });
            //    }

            //    Console.ReadLine();
            //}
            //catch (Exception ex)
            //{


            //}
        }

        private void PasteData()
        {
            GridViewRowInfo row = null;
            objWBView.GetLock();
            GridViewColumn col = null;
         
           
            int rowsCnt = objWBView.ActiveWorksheet.UsedRange.RowCount;
            int cellCnt = objWBView.ActiveWorksheet.UsedRange.CellCount.ToInt();
            if (cellCnt < 2) return;


            row = grdBookings.Rows.AddNew();
            for (int i = 0; i < rowsCnt; i++)
            {

                for (int j = 0; j < 1; j++)
                {
                    string cellHeading = objWBView.ActiveWorksheet.Cells[i, j].Value.ToStr().Trim().ToLower();
                    string cellValue= objWBView.ActiveWorksheet.Cells[i, j + 1].Value.ToStr().Trim();

                    if (string.IsNullOrEmpty(cellHeading))
                    {
                        row = grdBookings.Rows.AddNew();

                    }

                    col = grdBookings.Columns.FirstOrDefault(c => c.FieldName.ToLower() == cellHeading);

                    if (col != null)
                    {
                        if (col is GridViewDateTimeColumn)
                        {
                            if (cellValue.ToLower().Contains("today"))
                                cellValue = cellValue.ToLower().Strip("today").Trim();
                            DateTime tempDate=new DateTime();

                           bool canParse=  DateTime.TryParse(cellValue,out tempDate);


                      

                           cellValue = string.Format("{0:HH:mm}", cellValue);
                            TimeSpan pickupTime=TimeSpan.Zero;

                           TimeSpan.TryParse(cellValue,out pickupTime); 
                            
                          

                            DateTime pickupDate = DateTime.Now.ToDate();
                            pickupDate = pickupDate + pickupTime;
                            row.Cells[col.Name].Value = pickupDate;
                          
                        }

                        else if (col is GridViewComboBoxColumn)
                        {

                            int vehicleTypeId = General.GetObject<Fleet_VehicleType>(c => c.VehicleType.ToLower() == cellValue.ToLower()).DefaultIfEmpty().Id;

                            if (vehicleTypeId == 0)
                            {
                                vehicleTypeId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                            }

                            row.Cells[col.Name].Value = vehicleTypeId;
                        }
                        else
                        {
                            if (col is GridViewDecimalColumn && cellValue.IsNumeric()==false)
                                cellValue = "0.00";


                            if (col.FieldName == "Card")
                                cellValue = "Bank Account";


                            if (col.FieldName == "Booking Account")
                                cellValue = "Comcab";


                            row.Cells[col.Name].Value = cellValue;
                        }
                    }


                }
            }


            objWBView.ActiveWorksheet.Cells.Delete();

            objWBView.ReleaseLock();
        }

        protected override void OnClosed(EventArgs e)
        {
            General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
        }

     

        public override void Save()
        {
               var rows= grdBookings.Rows.Where(c=>c.Cells[COLS.ID].Value.ToStr()=="" || c.Cells[COLS.ID].Value.ToInt()==0);


               foreach (GridViewRowInfo row in rows)
                {
                    try
                    {
                        objMaster = new BookingBO();
                        objMaster.New();

                        objMaster.Current.SubcompanyId = AppVars.objSubCompany.Id;

                        objMaster.Current.BookingDate = DateTime.Now;
                        objMaster.Current.BookingNo = row.Cells[COLS.BookingReference].Value.ToStr();
                        objMaster.Current.VehicleTypeId = row.Cells[COLS.VehicleType].Value.ToIntorNull();

                        objMaster.Current.FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
                        objMaster.Current.FromAddress = row.Cells[COLS.From].Value.ToStr();

                        objMaster.Current.ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
                        objMaster.Current.ToAddress = row.Cells[COLS.To].Value.ToStr();
                        objMaster.Current.JourneyTypeId = 1;
                        objMaster.Current.FareRate = row.Cells[COLS.Fare].Value.ToDecimal();

                        objMaster.Current.PickupDateTime = row.Cells[COLS.PickupDateTime].Value.ToDateTime();
                        string companyName = row.Cells[COLS.Account].Value.ToStr();
                        int? companyId = null;

                        if (!string.IsNullOrEmpty(companyName))
                        {
                            companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == companyName.ToLower()).DefaultIfEmpty().Id.ToIntorNull();

                        }

                        if (companyId != null)
                        {
                            objMaster.Current.CompanyId = companyId;
                            objMaster.Current.IsCompanyWise = true;

                        }


                        objMaster.Current.CustomerName = row.Cells[COLS.Passenger].Value.ToStr().ToProperCase();

                        string phoneNo = row.Cells[COLS.PhoneNo].Value.ToStr();

                        if (!string.IsNullOrEmpty(phoneNo))
                        {
                            if (phoneNo.Contains("+44"))
                                phoneNo = phoneNo.Strip("+44").Trim();

                            else if (phoneNo.Contains("0044"))
                                phoneNo = phoneNo.Strip("0044").Trim();


                            if (phoneNo.Length > 0)
                            {
                                if (phoneNo[0] != '0')
                                    phoneNo = phoneNo.Insert(0, "0");

                            }
                        }

                        if (phoneNo.StartsWith("07"))
                        {
                            objMaster.Current.CustomerMobileNo = phoneNo;

                        }
                        else
                            objMaster.Current.CustomerPhoneNo = phoneNo;

                        string payment = row.Cells[COLS.PaymentType].Value.ToStr();
                        int? paymentType = General.GetObject<Gen_PaymentType>(c => c.PaymentType.ToLower() == payment.ToLower()).DefaultIfEmpty().Id.ToIntorNull();

                        if (paymentType != 0)
                            objMaster.Current.PaymentTypeId = paymentType;
                        else
                        {

                            paymentType = General.GetObject<Gen_PaymentType>(c => c.PaymentType.ToLower() == "bank account").DefaultIfEmpty().Id.ToIntorNull();
                            objMaster.Current.PaymentTypeId = paymentType;
                        }


                        objMaster.Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING;



                        objMaster.AddedBy = AppVars.LoginObj.UserName.ToStr();

                        objMaster.Save();

                        row.Cells[COLS.ID].Value = objMaster.Current.Id;

                        row.ErrorText = "";
                        

                    }
                    catch (Exception ex)
                    {
                        row.Cells[COLS.ID].Value = objMaster.Current.Id;

                        row.ErrorText = objMaster.ShowErrors();
                    }
                }



          
           

        }

        private void btnPasteBooking_Click(object sender, EventArgs e)
        {
            this.objWBView.Paste();
         
            PasteData();
        }

       
        private void BtnClearGrid_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }

        private void ClearGrid()
        {
            grdBookings.Rows.Clear();

        }

      

        private void radButtonElement_Click(object sender, EventArgs e)
        {
            object[] obj= General.ShowLister(General.GetQueryable<Gen_Company>(null).Select(args => new { Id = args.Id, Account = args.CompanyName }).ToList(), "Id");



        }

      

       
    }
}
