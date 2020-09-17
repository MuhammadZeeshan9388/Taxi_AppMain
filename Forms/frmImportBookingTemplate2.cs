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

namespace Taxi_AppMain
{
    public partial class frmImportBookingTemplate2 : UI.SetupBase
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
            public static string Via = "Via";
            public static string FlightNo = "FlightNo";

        }

        BookingBO objMaster = null;

        WorkbookView objWBView = null;
        public frmImportBookingTemplate2()
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
            colCombo.WrapText = true;
            grdBookings.Columns.Add(colCombo);



            col = new GridViewTextBoxColumn();
            col.Name = "PhoneNo";
            col.HeaderText = "Phone No";
            col.Width = 120;
            col.WrapText = true;
            col.FieldName = "Phone";
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "Passenger";
            col.HeaderText = "Passenger";
            col.FieldName = "Passenger Name";
            col.WrapText = true;
            col.Width = 100;
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


            col = new GridViewTextBoxColumn();
            col.Name = "Notes";
            col.WrapText = true;
            col.FieldName = "Driver Notes";

            col.HeaderText = "Notes";
            col.Width = 120;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "Account";
            col.HeaderText = "Account";
            col.FieldName = "Booking Account";
            col.WrapText = true;
            col.Width = 120;
            grdBookings.Columns.Add(col);


            //GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            //colDec.Name = "Fare";
            //colDec.FieldName = "Pricing";

            //colDec.DecimalPlaces = 2;
            //colDec.Minimum = 0;
            //colDec.Maximum = 999999;
            //colDec.HeaderText = "Fare";
            //colDec.Width = 60;
            //grdBookings.Columns.Add(colDec);

            

            //col = new GridViewTextBoxColumn();
            //col.Name = "PaymentType";
            //col.WrapText = true;
            //col.FieldName = "Card";

            //col.HeaderText = "Payment";
            //col.Width = 100;

            //grdBookings.Columns.Add(col);


          



       



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
        }

        private void PasteData(string filePath)
        {


            List<Gen_Company> listofCompany = General.GetQueryable<Gen_Company>(null).ToList();


            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(filePath);
            DataTable dt = ds.Tables["td"];



            DateTime? pickupdate = null;
            string bookingNo = string.Empty;
            string passenger = string.Empty;
            string toDoorNo = string.Empty;
            string account = string.Empty;
        
            string comment = string.Empty;
            string vehicle = string.Empty;
          //  int? vehicleTypeId = null;
            string phone = string.Empty;
            string from = string.Empty;
            string to = string.Empty;

            
           int rowsCnt = dt.Rows.Count.ToInt();
         
            try
            {

                grdBookings.RowCount = rowsCnt;
                int rowNo = 0;
                for (int i = 0; i < rowsCnt; i += 14)
                {

                    //for (int j = 0; j < 1; j++)
                    //{
                    pickupdate = dt.Rows[i][0].ToDateTimeorNull();
                    passenger = dt.Rows[i + 2][0].ToStr().ToProperCase();

                     account = dt.Rows[i + 4][0].ToStr().Trim();
                     comment = dt.Rows[i + 5][0].ToStr().Trim();


                     vehicle = dt.Rows[i + 7][0].ToStr().Trim();

                     phone = dt.Rows[i + 8][0].ToStr().Trim();
                   
                     from = dt.Rows[i + 9][0].ToStr().ToUpper().Replace(",", " ").Replace("  ", " ");
                     to = dt.Rows[i + 10][0].ToStr().ToUpper().Replace(",", " ").Replace("  ", " ");               
                   
                             


                    if (!string.IsNullOrEmpty(account) &&  account != "Cash" &&  listofCompany.Count(c => c.CompanyName.ToLower() == account.ToLower()) == 0)
                    {

                       
                            CompanyBO objCompanyMaster = new CompanyBO();
                         try
                        {

                            objCompanyMaster.New();

                            objCompanyMaster.Current.CompanyName = account;
                            objCompanyMaster.Current.Email = "abc@xyz.com";
                            objCompanyMaster.Current.ContactName = account;
                            objCompanyMaster.Current.Address = "unknown";
                            objCompanyMaster.Current.TelephoneNo = string.Empty;
                            objCompanyMaster.Current.MobileNo = string.Empty;
                            objCompanyMaster.Current.WebsiteUrl = string.Empty;
                            objCompanyMaster.Current.Fax = string.Empty;

                            objCompanyMaster.Current.IsClosed = false;

                            objCompanyMaster.Current.HasOrderNo = false;
                            objCompanyMaster.Current.HasPupilNo = false;
                            objCompanyMaster.Current.AdminFees = 0;
                            objCompanyMaster.Current.HasComcabCharges = false;

                            objCompanyMaster.Current.DiscountPercentage = 0;

                            objCompanyMaster.Current.FareDeductionPercent = 0;

                            objCompanyMaster.Current.HasVat = false;

                            objCompanyMaster.Current.HasBookedBy = false;

                            objCompanyMaster.Current.CompanyCode = string.Empty;

                            objCompanyMaster.Current.AccountTypeId = Enums.ACCOUNT_TYPE.ACCOUNT;


                            objCompanyMaster.Current.AccountNo = string.Empty;
                            objCompanyMaster.Current.WebLoginId = string.Empty;
                            objCompanyMaster.Current.WebLoginPassword = string.Empty;
                            objCompanyMaster.Current.IsWebLoginActive = false;


                            objCompanyMaster.Current.BackgroundColor = string.Empty;
                            objCompanyMaster.Current.TextColor = string.Empty;
                            objCompanyMaster.Current.PasswordAccount = string.Empty;
                            objCompanyMaster.Current.PasswordEnable = false;

                            objCompanyMaster.Save();



                            listofCompany.Add(objCompanyMaster.Current);
                        }
                        catch (Exception ex)
                        {
                            if (objCompanyMaster.Errors.Count>0)
                                MessageBox.Show(objCompanyMaster.ShowErrors());

                            break;


                        }
                    }




                  ;
                  grdBookings.Rows[rowNo].Cells[COLS.PickupDateTime].Value = pickupdate;

                    int vehicleTypeId = General.GetObject<Fleet_VehicleType>(c => c.VehicleType.ToLower() == vehicle.ToLower()).DefaultIfEmpty().Id;

                    if (vehicleTypeId == 0)
                    {
                        vehicleTypeId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                    }

                    grdBookings.Rows[rowNo].Cells[COLS.VehicleType].Value = vehicleTypeId;


                    grdBookings.Rows[rowNo].Cells[COLS.PhoneNo].Value = phone;
                    grdBookings.Rows[rowNo].Cells[COLS.Passenger].Value = passenger;
                    grdBookings.Rows[rowNo].Cells[COLS.From].Value = from;
                    grdBookings.Rows[rowNo].Cells[COLS.To].Value = to;
                    grdBookings.Rows[rowNo].Cells[COLS.Notes].Value = comment;
                    grdBookings.Rows[rowNo].Cells[COLS.Account].Value = account;

                    rowNo++;



                    
                   



                    //   }
                }
            }
            catch (Exception ex)
            {



            }

            finally
            {


               
            }
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

                        if (string.IsNullOrEmpty(row.Cells[COLS.PickupDateTime].Value.ToStr().Trim()))
                            break;


                        objMaster = new BookingBO();
                        objMaster.New();

                        objMaster.Current.SubcompanyId = AppVars.objSubCompany.Id;

                        objMaster.Current.BookingDate = DateTime.Now;
                       // objMaster.Current.BookingNo = row.Cells[COLS.BookingReference].Value.ToStr();
                        objMaster.Current.VehicleTypeId = row.Cells[COLS.VehicleType].Value.ToIntorNull();

                        objMaster.Current.FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
                        objMaster.Current.FromAddress = row.Cells[COLS.From].Value.ToStr();

                        objMaster.Current.ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
                        objMaster.Current.ToAddress = row.Cells[COLS.To].Value.ToStr();
                        objMaster.Current.JourneyTypeId = 1;
                        objMaster.Current.FareRate = 0.00m;

                        objMaster.Current.PickupDateTime = row.Cells[COLS.PickupDateTime].Value.ToDateTime();
                        string companyName = row.Cells[COLS.Account].Value.ToStr();
                        int? companyId = null;


                        if (companyName.ToLower() == "cash")
                        {


                        }

                        if (!string.IsNullOrEmpty(companyName))
                        {
                            companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == companyName.ToLower()).DefaultIfEmpty().Id.ToIntorNull();

                        }

                        if (companyId != null && companyId!=0)
                        {
                            objMaster.Current.CompanyId = companyId;
                            objMaster.Current.IsCompanyWise = true;

                        }



                        objMaster.Current.CustomerName = row.Cells[COLS.Passenger].Value.ToStr().ToProperCase();

                        if (objMaster.Current.CustomerName.Length > 100)
                            objMaster.Current.CustomerName = objMaster.Current.CustomerName.Substring(0, 99);

                        string phoneNo = row.Cells[COLS.PhoneNo].Value.ToStr();
                        string mobNo = string.Empty;
                        string telNo = string.Empty;
                        if (!string.IsNullOrEmpty(phoneNo))
                        {

                            if (phoneNo.Contains("/"))
                            {

                                string[] arr = phoneNo.Split(new char[]{ '/'});

                               
                                foreach (var item in arr)
                                {

                                    if (item.Contains("+44"))
                                        mobNo = item.Strip("+44").Trim();

                                    else if (item.Contains("0044"))
                                        mobNo = item.Strip("0044").Trim();
                                    else
                                        mobNo = item.Trim();

                                    if (item.Length > 0)
                                    {
                                        if (mobNo[0] != '0')
                                            mobNo = phoneNo.Insert(0, "0");



                                        if (mobNo.StartsWith("07") == false)
                                        {
                                            telNo = mobNo;
                                            break;
                                        }
                                    }
                                    
                                }

                            }


                           
                        }


                        if (telNo.Length > 0)
                            mobNo = phoneNo.Replace(telNo, "").Replace(" ", "").Trim();
                        else
                            mobNo = phoneNo;


                        if (mobNo.Length > 50)
                            mobNo = mobNo.Substring(0, 49);

                        objMaster.Current.CustomerMobileNo = mobNo;

                       
                            objMaster.Current.CustomerPhoneNo = telNo;




                       
                        if (companyId != null && companyId!=0)
                        {
                         
                            if (companyName.ToLower().Trim() == "C/Card" || companyName.ToLower().Trim() == "Credit Card")
                                objMaster.Current.PaymentTypeId = Enums.PAYMENT_TYPES.CREDIT_CARD;
                            else
                                objMaster.Current.PaymentTypeId = Enums.PAYMENT_TYPES.BANK_ACCOUNT;


                        }
                        else
                        {

                            objMaster.Current.PaymentTypeId = Enums.PAYMENT_TYPES.CASH;
                        }


                        objMaster.Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING;
                     //   objMaster.Current.FromDoorNo = row.Cells[COLS.FlightNo].Value.ToStr();



                        //objMaster.Current.Booking_ViaLocations.Add(new Booking_ViaLocation 
                        //{ 
                        
                        // ViaLocTypeId=Enums.LOCATION_TYPES.ADDRESS,
                        //  ViaLocLabel="Via",
                        //   ViaLocTypeLabel="Address",
                        //  ViaLocTypeValue="Address",
                        //   ViaLocValue=row.Cells[COLS.Via].Value.ToStr()
                        
                        
                        //});



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
           

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                 PasteData(openFileDialog1.FileName.ToStr());

            }

          
         
           
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
