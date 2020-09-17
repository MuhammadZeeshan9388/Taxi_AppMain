using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmDriver : UI.SetupBase
    {

        private bool _OpenedFromInActiveList;
        bool IsNotesEdit = false;
        public bool OpenedFromInActiveList
        {
            get { return _OpenedFromInActiveList; }
            set { _OpenedFromInActiveList = value; }
        }


        //public string BeforeDriverNo { get; set; }
        //public decimal BeforeMaxCommission { get; set; }

        //public string AfterDriverNo { get; set; }
        //public decimal AfterMaxCommission { get; set; }

        Font regularFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font oldFont = new Font("Tahoma", 10, FontStyle.Bold);
        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);
        Font bigFont = new Font("Tahoma", 12, FontStyle.Bold);

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
        public struct COL_DOCUMENT
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string EXPIRYDATE = "ExpiryDate";

            public static string DOCUMENTTITLEID = "DOCUMENTTITLEID";

            public static string DOCUMENTTITLE = "Document Title";
            public static string FILENAME = "File Name";

            public static string FROMFULLPATH = "FULLPATH";

            public static string FULLPATH = "FULLPATH";
            public static string BADGENUMBER = "BADGENUMBER";

        }
        public struct COL_CompanyVehicle
        {

            public static string DeriverVehicleID = "DeriverVehicleID";
            public static string ID = "ID";
            public static string Check = "Check";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string Vehicles = "Vehicles";
        }

        public struct COL_AVAILABILITY
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string BECAMEAVAIL = "BECAMEAVAIL";

            public static string ENDINGDATE = "ENDINGDATE";


        }
        public struct COL_SHIFT
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string SHIFT = "SHIFT";

            public static string SHIFT_ID = "SHIFT_ID";

            public static string FROMTIME = "FROMTIME";

            public static string TOTIME = "TOTIME";


        }


        public struct COL_VEHICLES
        {
            public static string ID = "ID";

            public static string ASSIGNEDON = "ASSIGNEDON";

            public static string VEHICLETYPEID = "VEHICLETYPEID";
            public static string VEHICLETYPENAME = "VEHICLETYPENAME";

            public static string VEHNO = "VEHNO";

            public static string COLOR = "COLOR";

            public static string OWNER = "OWNER";

            public static string MAKE = "MAKE";

            public static string MODEL = "MODEL";
            public static string VEHLOGBOOK = "VEHLOGBOOK";

            public static string ENDON = "ENDON";


        }
        public struct COL_NOTES
        {
            public static string Id = "Id";
            public static string Notes = "Notes";
            public static string Time = "Time";
            public static string AddBy = "AddBy";
            public static string AddOn = "AddOn";
            public static string DateTime = "DateTime";
            public static string DriverId = "DriverId";
        }
        public struct COL_DebitCreditNotes
        {
            public static string Id = "Id";
            public static string DriverId = "DriverId";
            public static string DCNoteId = "DCNoteId";
            public static string Name = "Name";
            public static string Type = "Type";
            public static string Charges = "Charges";
            public static string IsWeekly = "IsWeekly";
        }

        public struct Col_DriverAttributes
        {
            public static string Id = "Id";
            public static string DetailId = "DetailId";


            public static string Name = "Name";
            public static string ShortName = "ShortName";
            public static string Default = "Default";
        }


        private void FormatDebitCreditNotesGrid()
        {
            grdDebitCreditNotes.AllowEditRow = true;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COL_DebitCreditNotes.Id;
            col.IsVisible = false;
            grdDebitCreditNotes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COL_DebitCreditNotes.DriverId;
            col.IsVisible = false;
            grdDebitCreditNotes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COL_DebitCreditNotes.DCNoteId;
            col.IsVisible = false;
            grdDebitCreditNotes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COL_DebitCreditNotes.Name;
            col.HeaderText = COL_DebitCreditNotes.Name;
            col.Width = 200;
            col.ReadOnly = true;
            grdDebitCreditNotes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COL_DebitCreditNotes.Type;
            col.HeaderText = COL_DebitCreditNotes.Type;
            col.Width = 140;
            col.ReadOnly = true;
            grdDebitCreditNotes.Columns.Add(col);
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COL_DebitCreditNotes.Charges;
            dcol.HeaderText = COL_DebitCreditNotes.Charges;
            dcol.Width = 120;
            dcol.DecimalPlaces = 2;
            dcol.Maximum = 1000;
            dcol.ReadOnly = false;
            grdDebitCreditNotes.Columns.Add(dcol);
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_DebitCreditNotes.IsWeekly;
            cbcol.HeaderText = "Weekly";
            cbcol.ReadOnly = false;
            cbcol.Width = 120;
            grdDebitCreditNotes.Columns.Add(cbcol);



            pg_charges.Item.Visibility = ElementVisibility.Collapsed;

            //int DriverTypeId = ddlDriverType.SelectedValue.ToInt();
            //var list = (from a in General.GetQueryable<Gen_DebitCreditNote>(null)//c=>c.ApplyIn==DriverTypeId || c.ApplyIn==0)
            //            select new
            //            {
            //                Id = a.Id,
            //                NoteName = a.NoteName,
            //                ExpenseType = a.ExpenseType,
            //                Charges = a.Charges
            //            }).ToList();
            //int cnt = list.Count;

            //grdDebitCreditNotes.RowCount = cnt;
            //for (int i = 0; i < cnt; i++)
            //{
            //    grdDebitCreditNotes.Rows[i].Cells[COL_DebitCreditNotes.DCNoteId].Value = list[i].Id;
            //    grdDebitCreditNotes.Rows[i].Cells[COL_DebitCreditNotes.Name].Value = list[i].NoteName;
            //    grdDebitCreditNotes.Rows[i].Cells[COL_DebitCreditNotes.Type].Value = list[i].ExpenseType;
            //    grdDebitCreditNotes.Rows[i].Cells[COL_DebitCreditNotes.Charges].Value = list[i].Charges;
            //}

          
        }

      

        public void FormatNotesGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COL_NOTES.Id;
            col.IsVisible = false;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COL_NOTES.Notes;
            col.HeaderText = "Notes";
            col.Width = 520;
            grdLister.Columns.Add(col);




            //        dtcol = new GridViewDateTimeColumn();


            GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();

            dtcol.Name = COL_NOTES.AddOn;
            dtcol.Width = 140;
            dtcol.HeaderText = "Added On";
            grdLister.Columns.Add(dtcol);


            dtcol = new GridViewDateTimeColumn();

            dtcol.Name = COL_NOTES.DateTime;
            dtcol.Width = 140;
            dtcol.SortOrder = RadSortOrder.Ascending;
            dtcol.AllowSort = true;

            grdLister.EnableSorting = true;
            dtcol.HeaderText = "Date Time";
            dtcol.IsVisible = false;
            grdLister.Columns.Add(dtcol);


            dtcol = new GridViewDateTimeColumn();
            dtcol.Name = COL_NOTES.Time;
            dtcol.HeaderText = "Time";
            dtcol.Width = 70;
            grdLister.Columns.Add(dtcol);



            col = new GridViewTextBoxColumn();
            col.Name = COL_NOTES.AddBy;
            col.HeaderText = "Add By";
            col.Width = 90;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COL_NOTES.DriverId;
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            GridViewCommandColumn col2 = new GridViewCommandColumn();
            col2.HeaderText = "Edit";
            col2.UseDefaultText = true;
            col2.Width = 80;
            col2.DefaultText = "Edit";
            col2.TextAlignment = ContentAlignment.MiddleCenter;
            col2.Name = "btnEdit";
            grdLister.Columns.Add(col2);
            col2 = new GridViewCommandColumn();
            // col2.TextAlignment=
            col2.HeaderText = "Delete";
            col2.Width = 80;
            col2.UseDefaultText = true;
            col2.DefaultText = "Delete";
            col2.Name = "btnDelete";
            col2.TextAlignment = ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(col2);
            grdLister.ShowRowHeaderColumn = false;


            (grdLister.Columns["AddOn"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            (grdLister.Columns[COL_NOTES.DateTime] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy hh:mm:ss}";
            (grdLister.Columns[COL_NOTES.Time] as GridViewDateTimeColumn).FormatString = "{0:HH:mm}";
            //(grdLister.Columns["AddOn"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            //(grdLister.Columns[COL_NOTES.DateTime] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy hh:mm:ss}";
            //(grdLister.Columns[COL_NOTES.Time] as GridViewDateTimeColumn).FormatString = "{0:HH:mm}";


            ////grdLister.GroupDescriptors.Expression = "AddOn";
            ////grdLister.GroupDescriptors[0].Format = "{1:dddd dd/MM/yyyy}";
            ////grdLister.AutoExpandGroups = true;

            Pg_notes.Item.Visibility = ElementVisibility.Collapsed;
        }

        DriverBO objMaster;
        public frmDriver()
        {
            InitializeComponent();


            FormatDriverAttributesGrid();

            LoadVehicleAttributesGrid();
            objMaster = new DriverBO();
            this.SetProperties((INavigation)objMaster);
            this.Load += new EventHandler(frmDriver_Load);
           
          

            numPrimeCompanyRent.Visible = false;
            lblPrimeCompanyRent.Visible = false;

            FillCombos();
            FormatDocumentsGrid();
            FormatAvailabilityGrid();
            FormatNotesGrid();
            FormatDebitCreditNotesGrid();
            FormatDriverShiftGrid();

            LoadDriverShifts();
            radpageview1.Pages.Remove(Pg_Rating);
           // Pg_Rating.Item.Visibility = ElementVisibility.Hidden;


            openFileDialog1 = new OpenFileDialog();


            this.Shown += new EventHandler(frmDriver_Shown);
            grdAvailability.CellDoubleClick += new GridViewCellEventHandler(grdAvailability_CellDoubleClick);

            timer1.Tick += new EventHandler(timer1_Tick);

            this.FormClosed += new FormClosedEventHandler(frmDriver_FormClosed);


            if (AppVars.objPolicyConfiguration != null)
            {

                MapType = AppVars.objPolicyConfiguration.MapType.ToInt();
            }

            txtAddress.ListBoxElement.Width = 500;
            txtAddress.ListBoxElement.Font = new Font("Tahoma", 10, FontStyle.Bold);
            txtAddress.DefaultHeight = 250;

            txtAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            txtAddress.KeyPress += new KeyPressEventHandler(txtAddress_KeyPress);
            grdAvailability.RowsChanged += new GridViewCollectionChangedEventHandler(grdAvailability_RowsChanged);


            radpageview1.SelectedPageChanged += new EventHandler(radPageView1_SelectedPageChanged);
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdDriverNotes_CommandCellClick);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdDriverNotes_CellDoubleClick);
            grdLister.CellFormatting += new CellFormattingEventHandler(grdDriverNotes_CellFormatting);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdDriverNotes_ViewCellFormatting);
            //grdDriverNotes.ScreenTipNeeded += new ScreenTipNeededEventHandler(grdDriverNotes_ScreenTipNeeded);
            grdLister.GroupDescriptors.Expression = "AddOn";
            grdLister.GroupDescriptors[0].Format = "{1:dddd dd/MM/yyyy}";
            grdLister.AutoExpandGroups = true;

            grdDriverComplaints.CellDoubleClick += new GridViewCellEventHandler(grdDriverComplaints_CellDoubleClick);
            
            //grdDriverNotes.AllowAddNewRow = false;

         //   chkBidding.Visible = AppVars.objPolicyConfiguration.EnableBidding.ToBool();
            chkUseCompanyVehicle.ToggleStateChanged += new StateChangedEventHandler(chkUseCompanyVehicle_ToggleStateChanged);


            try
            {

                radPageViewPage2.Item.Visibility = ElementVisibility.Collapsed;
            }
            catch
            {


            }
            
        }


        private void FormateCompanyVehicle()
        {
            grdCompanyVehicles.AllowAutoSizeColumns = true;
            grdCompanyVehicles.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


            grdCompanyVehicles.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);



            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyVehicle.ID;
            col.IsVisible = false;
            grdCompanyVehicles.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_CompanyVehicle.DeriverVehicleID;
            grdCompanyVehicles.Columns.Add(col);



            GridViewCheckBoxColumn col1 = new GridViewCheckBoxColumn();
            col1.IsVisible = true;
            col1.Name = COL_CompanyVehicle.Check;
            col1.Width = 10;
            grdCompanyVehicles.Columns.Add(col1);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_CompanyVehicle.VehicleTypeId;
            grdCompanyVehicles.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = true;
            col.HeaderText = "Company Vehicle";
            col.Name = COL_CompanyVehicle.Vehicles;
            grdCompanyVehicles.Columns.Add(col);

            UI.GridFunctions.SetFilter(grdCompanyVehicles);

            grdCompanyVehicles.AllowEditRow = true;



            DisplayCompanyVehicle();


        
        }

        private void FormatDriverAttributesGrid()
        {


            grdDriverAttributes.AllowAddNewRow = false;
            //   grdDetails.AllowEditRow = false;
            //grdVehicleAttributes.AutoCellFormatting = true;
            grdDriverAttributes.ShowGroupPanel = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = Col_DriverAttributes.Id;
            col.IsVisible = false;
            grdDriverAttributes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = Col_DriverAttributes.DetailId;
            col.IsVisible = false;
            grdDriverAttributes.Columns.Add(col);


            GridViewCheckBoxColumn col1 = new GridViewCheckBoxColumn();
            col1.HeaderText = "Default";
            col1.Name = Col_DriverAttributes.Default;
            col1.HeaderText = "";
            col1.Width = 60;
            //col1.ReadOnly = true;
            grdDriverAttributes.Columns.Add(col1);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "ShortName";
            col.Name = Col_DriverAttributes.ShortName;
            col.IsVisible = false;

            // col.ReadOnly = true;
            grdDriverAttributes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Name";
            col.Name = Col_DriverAttributes.Name;
            col.Width = 180;
            // col.ReadOnly = true;
            grdDriverAttributes.Columns.Add(col);




            grdDriverAttributes.MasterTemplate.ShowRowHeaderColumn = false;

            UI.GridFunctions.SetFilter(grdDriverAttributes);

            grdDriverAttributes.AllowEditRow = true;

            //DetailGridButton();

        }

        void chkUseCompanyVehicle_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                lblCarInsurance.Visible = true;
                numCarInsuranceRent.Visible = true;
                lblCarRent.Visible = true;
                numCarRent.Visible = true;

            }
            else
            {
                numCarInsuranceRent.Value = 0.00m;
                numCarRent.Value = 0.00m;
                numCarInsuranceRent.Visible = false;
                numCarRent.Visible = false;
                lblCarInsurance.Visible = false;
                lblCarRent.Visible = false;

            }


        }

        void grdDriverComplaints_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdDriverComplaints.CurrentRow != null && grdDriverComplaints.CurrentRow is GridViewDataRowInfo)
                {
                    int Id = 0;
                    Id = grdDriverComplaints.CurrentRow.Cells["Id"].Value.ToInt();
                    if (Id > 0)
                    {

                        frmComplaint frm = new frmComplaint(Id, true);
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.ShowDialog();

                        frm.Dispose();
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdDriverNotes_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridGroupContentCellElement)
                {


                    e.Row.Height = 30;
                    e.CellElement.Font = bigFont;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.GhostWhite;
                    e.CellElement.RowElement.BackColor = Color.GhostWhite;
                    e.CellElement.RowElement.NumberOfColors = 1;
                    e.CellElement.ForeColor = Color.Blue;

                    e.CellElement.BorderColor = Color.DarkSlateBlue;
                    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                }

                //if (e. == "AddOn")
                //    {
                //       // e.CellElement.BackgroundImage = null;
                //        e.CellElement.NumberOfColors = 1;
                //       // e.CellElement.DrawFill = true;


                //        string Bgcolor = e.Row.Cells["BackgroundColor"].Value.ToStr().Trim();
                //        string textColor = e.Row.Cells["TextColor"].Value.ToStr().Trim();

                //        if (Bgcolor != string.Empty && textColor != string.Empty)
                //        {

                //            e.CellElement.BackColor = Color.FromArgb(Bgcolor.ToInt());
                //            e.CellElement.ForeColor = Color.Blue;// Color.FromArgb(textColor.ToInt());
                //            e.CellElement.DrawFill = true;
                //            e.CellElement.Font = Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //        }
                //    }
            }
            catch (Exception ex)
            {

            }
        }

        void grdDriverNotes_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridDataCellElement)
                {
                    if (e.Column is GridViewCommandColumn)
                    {

                        if (e.Column.Name == "btnDelete")
                        {
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.delete;
                        }
                        else if (e.Column.Name == "btnEdit")
                        {
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.edit2;
                        }
                    }


                    //    e.CellElement.RowElement.DrawFill = false;

                    if (e.Row.Cells["InvoiceSent"].Value.ToInt() == 1)
                    {

                        e.CellElement.RowElement.NumberOfColors = 1;
                        e.CellElement.RowElement.BackColor = Color.SkyBlue;
                        e.CellElement.RowElement.DrawFill = true;
                    }

                    if (e.Row.Cells["IsPaid"].Value.ToInt() == 1)
                    {

                        e.CellElement.RowElement.NumberOfColors = 1;
                        e.CellElement.RowElement.BackColor = Color.LightGreen;
                        e.CellElement.RowElement.DrawFill = true;
                    }
                    if (e.CellElement.RowElement.RowInfo.Cells["IsPaid"].Value.ToInt() == 0 && e.Row.Cells["InvoiceSent"].Value.ToInt() == 0)
                    {
                        e.CellElement.RowElement.DrawFill = false;
                    }
                }
                // e.CellElement.Font = f;
            }
            catch
            {

            }
        }



        //void grdDriverNotes_ScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
        //{
        //    GridDataCellElement cell = e.Item as GridDataCellElement;
        //    if (cell != null && cell.ColumnInfo != null)
        //    {
        //        this.ShowScreenTipForCell(cell);
        //    }
        //}
        private void ShowScreenTipForCell(GridDataCellElement cell)
        {
            try
            {
                if (cell.RowInfo is GridViewDataRowInfo == false) return;

                GridViewDataRowInfo row = (GridViewDataRowInfo)cell.RowInfo;

                int id = cell.RowInfo.Cells["Id"].Value.ToInt();

                Fleet_Driver_Note obj = General.GetObject<Fleet_Driver_Note>(c => c.Id == id);
                if (obj != null)
                {
                    if (obj.Id >= 1)// Enums.BOOKINGSTATUS.BID && cell.ColumnInfo.Name.ToStr() == "HasNotesImg")
                    {
                        RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();

                        screenTip.CaptionLabel.Margin = new Padding(3);

                        string[] arr = (from a in General.GetQueryable<Fleet_Driver_Note>(c => c.Id == obj.Id)
                                        select ("   Driver : " + a.Fleet_Driver.DriverNo + "  bid at " + string.Format("{0:HH:mm}", a.AddOnTime) + Environment.NewLine + "<br>"))
                                                 .ToArray<string>();
                        int cnter = 1;
                        string biddingType = string.Empty;

                        if (AppVars.objPolicyConfiguration.BiddingType.ToInt() == Enums.BIDDING_TYPES.NEAREST_DRIVER)
                        {
                            biddingType = " (For Nearest Drivers)";
                        }
                        else
                        {
                            biddingType = "(For Longest Waiting Drivers)";
                        }



                        string text = "<html><span><color=Red><b>Bidding Details " + biddingType + "</b><br><br><color=Blue><b>        " + string.Join((cnter++).ToStr() + ".", arr) + "</b></span></html>";


                        screenTip.CaptionLabel.Text = text;
                        screenTip.MainTextLabel.Text = string.Empty;
                        screenTip.EnableCustomSize = false;

                        cell.ScreenTip = screenTip;


                    }

                }
            }

            catch
            {

            }
        }

        void grdDriverNotes_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewRowInfo)
                {
                    txtNotes.Text = grdLister.CurrentRow.Cells["Notes"].Value.ToStr();
                    IsNotesEdit = true;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdDriverNotes_CommandCellClick(object sender, EventArgs e)
        {
            try
            {

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();

                GridViewRowInfo row = gridCell.RowElement.RowInfo;
                //long id = row.Cells["Id"].Value.ToLong();

                //int driverId = row.Cells["DriverId"].Value.ToInt();

                //bool rtn = false;

                //int bookingStatusId = row.Cells["StatusId"].Value.ToInt();

                if (name == "btndelete")
                {
                    grdLister.CurrentRow.Delete();
                }
                else if (name == "btnedit")
                {
                    txtNotes.Text = grdLister.CurrentRow.Cells["Notes"].Value.ToStr();
                    IsNotesEdit = true;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (radpageview1.SelectedPage == radPageViewPage2)
            {
                if (radPanel2 == null)
                {
                    InitializeShiftPanel();

                }

                btnSaveAndClose.BringToFront();
            }
            else if (radpageview1.SelectedPage == radPageViewPage3)
            {

                InitializeVehicleHistoryPanel();

                btnSaveAndClose.BringToFront();
            }
            else if (radpageview1.SelectedPage == pg_pdasettings)
            {
                InitializePdaSettingsPanel();

                btnSaveAndClose.SendToBack();
            }
            else if (radpageview1.SelectedPage == Pg_Rating)
            {
                InitializeRatingPanel();
               
               
                PopulateDriverRating();
            
            }
            else
            {

                btnSaveAndClose.BringToFront();
            }

        }

       

        private void InitializeRatingPanel()
        {
            if (grdRating != null)
                return;
            //if (pnlSettings == null)
            //{
               // private UI.MyGridView grdRating;
                this.grdRating = new UI.MyGridView();

                //this.Pg_Rating.Controls.Add(this.grdRating);
                //this.Pg_Rating.Location = new System.Drawing.Point(10, 37);
                //this.Pg_Rating.Name = "Pg_Rating";
                //this.Pg_Rating.Size = new System.Drawing.Size(879, 625);
                //this.Pg_Rating.Text = "Rating";
                // 
                // grdRating
                // 
                this.grdRating.AutoCellFormatting = false;
                this.grdRating.EnableCheckInCheckOut = false;
                this.grdRating.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.grdRating.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdRating.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdRating.Location = new System.Drawing.Point(8, 8);
                // 
                // grdRating
                // 
                this.grdRating.MasterTemplate.AllowAddNewRow = false;
                this.grdRating.MasterTemplate.AllowEditRow = false;
                this.grdRating.MasterTemplate.ShowRowHeaderColumn = false;
                this.grdRating.Name = "grdRating";
                this.grdRating.PKFieldColumnName = "";
                this.grdRating.ShowGroupPanel = false;
                this.grdRating.ShowImageOnActionButton = true;
                this.grdRating.Size = new System.Drawing.Size(644, 600);
                this.grdRating.TabIndex = 98;
                this.grdRating.Text = "myGridView1";

                ((System.ComponentModel.ISupportInitialize)(this.grdRating)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdRating.MasterTemplate)).BeginInit();


                ((System.ComponentModel.ISupportInitialize)(this.grdRating.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdRating)).EndInit();
                this.Pg_Rating.Controls.Add(grdRating);
               grdRating.CommandCellClick += new CommandCellClickEventHandler(grdRating_CommandCellClick);

            FormateRatingGride();
            // }
        }

        private void InitializePdaSettingsPanel()
        {
            if (pnlSettings == null)
            {
                this.pnlSettings = new System.Windows.Forms.GroupBox();
                this.txtPDAVer = new System.Windows.Forms.Label();
                this.btnUpdateSettings = new Telerik.WinControls.UI.RadButton();
                this.chkDisableOnBreak = new System.Windows.Forms.CheckBox();
                this.chkShiftOverLogout = new System.Windows.Forms.CheckBox();
                this.chkDisableBase = new System.Windows.Forms.CheckBox();
                this.chkShowFareonExtraCharges = new System.Windows.Forms.CheckBox();
                this.chkEnableLogoutOnReject = new System.Windows.Forms.CheckBox();
                this.chkHidePickupAndDest = new System.Windows.Forms.CheckBox();
                this.label7 = new System.Windows.Forms.Label();
                this.numJobTimeout = new System.Windows.Forms.NumericUpDown();
                this.label8 = new System.Windows.Forms.Label();
                this.txtBiddingMessage = new System.Windows.Forms.Label();
                this.txtFareMessage = new System.Windows.Forms.Label();
                this.label6 = new System.Windows.Forms.Label();
                this.numBreakDuration = new System.Windows.Forms.NumericUpDown();
                this.label5 = new System.Windows.Forms.Label();
                this.chkDisableMeterAccJob = new System.Windows.Forms.CheckBox();
                this.chkEnableMeterWaitingCharges = new System.Windows.Forms.CheckBox();
                this.chkEnableOptionalMeter = new System.Windows.Forms.CheckBox();
                this.chkShowSoundOnZoneChange = new System.Windows.Forms.CheckBox();
                this.chkDisableChangeJobPlot = new System.Windows.Forms.CheckBox();
                this.chkDisableRank = new System.Windows.Forms.CheckBox();
                this.chkDisablePanic = new System.Windows.Forms.CheckBox();
                this.chkIgnoreArriveAction = new System.Windows.Forms.CheckBox();
                this.chkMessageStay = new System.Windows.Forms.CheckBox();
                this.ShowPlotOnJobOffer = new System.Windows.Forms.CheckBox();
                this.chkShowAlertOnJobLater = new System.Windows.Forms.CheckBox();
                this.chkShowCustomerMobileNo = new System.Windows.Forms.CheckBox();
                this.chkShowNavigation = new System.Windows.Forms.CheckBox();
                this.chkShowCompletedJobs = new System.Windows.Forms.CheckBox();
                this.chkShowPlots = new System.Windows.Forms.CheckBox();
                this.chkEnableAutoRotate = new System.Windows.Forms.CheckBox();
                this.chkEnableCompanyCars = new System.Windows.Forms.CheckBox();
                this.chkEnableJ15Jobs = new System.Windows.Forms.CheckBox();
                this.chkEnableCallCustomer = new System.Windows.Forms.CheckBox();
                this.chkEnableJobExtraCharges = new System.Windows.Forms.CheckBox();
                this.chkEnableRecoverJob = new System.Windows.Forms.CheckBox();
                this.chkEnableLogoutAuthorization = new System.Windows.Forms.CheckBox();
                this.chkEnableFlagDown = new System.Windows.Forms.CheckBox();
                this.chkEnableFareMeter = new System.Windows.Forms.CheckBox();
                this.label4 = new System.Windows.Forms.Label();
                this.chkEnableBidding = new System.Windows.Forms.CheckBox();
                this.ddlNavigation = new System.Windows.Forms.ComboBox();

                this.chkDisableChangeDest = new System.Windows.Forms.CheckBox();
                this.chkDisableRejectJob = new System.Windows.Forms.CheckBox();

                chkEnableOptionalManualFares = new CheckBox();

                this.pnlSettings.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.btnUpdateSettings)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numJobTimeout)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numBreakDuration)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();


                //   ((System.ComponentModel.ISupportInitialize)(this.chkDisableChangeDest)).BeginInit();
                //   ((System.ComponentModel.ISupportInitialize)(this.chkDisableRejectJob)).BeginInit();

                // 
                // pnlSettings
                // 

                this.pg_pdasettings.Controls.Add(this.pnlSettings);



                this.pnlSettings.Controls.Add(this.chkEnablePriceBidding);
                this.pnlSettings.Controls.Add(this.chkEnableManualFares);
                this.pnlSettings.Controls.Add(this.chkEnableOptionalManualFares);
                this.pnlSettings.Controls.Add(this.txtPDAVer);
                this.pnlSettings.Controls.Add(this.btnUpdateSettings);
                this.pnlSettings.Controls.Add(this.chkDisableOnBreak);
                this.pnlSettings.Controls.Add(this.chkShiftOverLogout);
                this.pnlSettings.Controls.Add(this.chkDisableBase);
                this.pnlSettings.Controls.Add(this.chkShowFareonExtraCharges);
                this.pnlSettings.Controls.Add(this.chkEnableLogoutOnReject);
                this.pnlSettings.Controls.Add(this.chkHidePickupAndDest);
                this.pnlSettings.Controls.Add(this.label7);
                this.pnlSettings.Controls.Add(this.numJobTimeout);
                this.pnlSettings.Controls.Add(this.label8);
                this.pnlSettings.Controls.Add(this.txtBiddingMessage);
                this.pnlSettings.Controls.Add(this.txtFareMessage);
                this.pnlSettings.Controls.Add(this.label6);
                this.pnlSettings.Controls.Add(this.numBreakDuration);
                this.pnlSettings.Controls.Add(this.label5);
                this.pnlSettings.Controls.Add(this.chkDisableMeterAccJob);
                this.pnlSettings.Controls.Add(this.chkEnableMeterWaitingCharges);
                this.pnlSettings.Controls.Add(this.chkEnableOptionalMeter);
                this.pnlSettings.Controls.Add(this.chkShowSoundOnZoneChange);
                this.pnlSettings.Controls.Add(this.chkDisableChangeJobPlot);
                this.pnlSettings.Controls.Add(this.chkDisableRank);
                this.pnlSettings.Controls.Add(this.chkDisablePanic);
                this.pnlSettings.Controls.Add(this.chkIgnoreArriveAction);
                this.pnlSettings.Controls.Add(this.chkMessageStay);
                this.pnlSettings.Controls.Add(this.ShowPlotOnJobOffer);
                this.pnlSettings.Controls.Add(this.chkShowAlertOnJobLater);
                this.pnlSettings.Controls.Add(this.chkShowCustomerMobileNo);
                this.pnlSettings.Controls.Add(this.chkShowNavigation);
                this.pnlSettings.Controls.Add(this.chkShowCompletedJobs);
                this.pnlSettings.Controls.Add(this.chkShowPlots);
                this.pnlSettings.Controls.Add(this.chkEnableAutoRotate);
                this.pnlSettings.Controls.Add(this.chkEnableCompanyCars);
                this.pnlSettings.Controls.Add(this.chkEnableJ15Jobs);
                this.pnlSettings.Controls.Add(this.chkEnableCallCustomer);
                this.pnlSettings.Controls.Add(this.chkEnableJobExtraCharges);
                this.pnlSettings.Controls.Add(this.chkEnableRecoverJob);
                this.pnlSettings.Controls.Add(this.chkEnableLogoutAuthorization);
                this.pnlSettings.Controls.Add(this.chkEnableFlagDown);
                this.pnlSettings.Controls.Add(this.chkEnableFareMeter);
                this.pnlSettings.Controls.Add(this.label4);
                this.pnlSettings.Controls.Add(this.chkEnableBidding);
                this.pnlSettings.Controls.Add(this.ddlNavigation);
                this.pnlSettings.Controls.Add(this.chkDisableChangeDest);
                this.pnlSettings.Controls.Add(this.chkDisableRejectJob);

                this.pnlSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.pnlSettings.Location = new System.Drawing.Point(13, 21);
                this.pnlSettings.Name = "pnlSettings";
                this.pnlSettings.Size = new System.Drawing.Size(870, 600);
                this.pnlSettings.TabIndex = 12;
                this.pnlSettings.TabStop = false;
                this.pnlSettings.Text = "Settings";
                // 
                // txtPDAVer
                // 
                this.txtPDAVer.AutoSize = true;
                this.txtPDAVer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.txtPDAVer.Location = new System.Drawing.Point(517, 70);
                this.txtPDAVer.Name = "txtPDAVer";
                this.txtPDAVer.Size = new System.Drawing.Size(170, 18);
                this.txtPDAVer.TabIndex = 54;
                this.txtPDAVer.Text = "Current PDA Version :";
                // 
                // btnUpdateSettings
                // 
                this.btnUpdateSettings.Location = new System.Drawing.Point(680, 515);
                this.btnUpdateSettings.Name = "btnUpdateSettings";
                this.btnUpdateSettings.Size = new System.Drawing.Size(184, 67);
                this.btnUpdateSettings.TabIndex = 53;
                this.btnUpdateSettings.Text = "Update Settings >>";
                this.btnUpdateSettings.Click += new System.EventHandler(this.btnUpdateSettings_Click);
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnUpdateSettings.GetChildAt(0))).Text = "Update Settings >>";
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnUpdateSettings.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnUpdateSettings.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // chkDisableOnBreak
                // 
                this.chkDisableOnBreak.AutoSize = true;
                this.chkDisableOnBreak.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkDisableOnBreak.Location = new System.Drawing.Point(478, 331);
                this.chkDisableOnBreak.Name = "chkDisableOnBreak";
                this.chkDisableOnBreak.Size = new System.Drawing.Size(133, 22);
                this.chkDisableOnBreak.TabIndex = 52;
                this.chkDisableOnBreak.Text = "Disable OnBreak";
                this.chkDisableOnBreak.UseVisualStyleBackColor = true;
                // 
                // chkShiftOverLogout
                // 
                this.chkShiftOverLogout.AutoSize = true;
                this.chkShiftOverLogout.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShiftOverLogout.Location = new System.Drawing.Point(241, 481);
                this.chkShiftOverLogout.Name = "chkShiftOverLogout";
                this.chkShiftOverLogout.Size = new System.Drawing.Size(162, 22);
                this.chkShiftOverLogout.TabIndex = 51;
                this.chkShiftOverLogout.Text = "Logout on Shift Over";
                this.chkShiftOverLogout.UseVisualStyleBackColor = true;
                // 
                // chkDisableBase
                // 
                this.chkDisableBase.AutoSize = true;
                this.chkDisableBase.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkDisableBase.Location = new System.Drawing.Point(478, 418);
                this.chkDisableBase.Name = "chkDisableBase";
                this.chkDisableBase.Size = new System.Drawing.Size(109, 22);
                this.chkDisableBase.TabIndex = 50;
                this.chkDisableBase.Text = "Disable Base";
                this.chkDisableBase.UseVisualStyleBackColor = true;
                // 
                // chkShowFareonExtraCharges
                // 
                this.chkShowFareonExtraCharges.AutoSize = true;
                this.chkShowFareonExtraCharges.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShowFareonExtraCharges.Location = new System.Drawing.Point(241, 512);
                this.chkShowFareonExtraCharges.Name = "chkShowFareonExtraCharges";
                this.chkShowFareonExtraCharges.Size = new System.Drawing.Size(220, 22);
                this.chkShowFareonExtraCharges.TabIndex = 49;
                this.chkShowFareonExtraCharges.Text = "Show Fares on Extra Charges";
                this.chkShowFareonExtraCharges.UseVisualStyleBackColor = true;
                // 
                // chkEnableLogoutOnReject
                // 
                this.chkEnableLogoutOnReject.AutoSize = true;
                this.chkEnableLogoutOnReject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableLogoutOnReject.Location = new System.Drawing.Point(17, 447);
                this.chkEnableLogoutOnReject.Name = "chkEnableLogoutOnReject";
                this.chkEnableLogoutOnReject.Size = new System.Drawing.Size(212, 22);
                this.chkEnableLogoutOnReject.TabIndex = 48;
                this.chkEnableLogoutOnReject.Text = "Enable Logout on Reject Job";
                this.chkEnableLogoutOnReject.UseVisualStyleBackColor = true;
                // 
                // chkHidePickupAndDest
                // 
                this.chkHidePickupAndDest.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkHidePickupAndDest.Location = new System.Drawing.Point(478, 285);
                this.chkHidePickupAndDest.Name = "chkHidePickupAndDest";
                this.chkHidePickupAndDest.Size = new System.Drawing.Size(197, 43);
                this.chkHidePickupAndDest.TabIndex = 47;
                this.chkHidePickupAndDest.Text = "Hide Details On Job Offer";
                this.chkHidePickupAndDest.UseVisualStyleBackColor = true;
              //  this.chkHidePickupAndDest.CheckedChanged += new EventHandler(chkHidePickupAndDest_CheckedChanged);
                // 
                // label7
                // 
                this.label7.AutoSize = true;
                this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
                this.label7.Location = new System.Drawing.Point(784, 275);
                this.label7.Name = "label7";
                this.label7.Size = new System.Drawing.Size(45, 14);
                this.label7.TabIndex = 46;
                this.label7.Text = "(secs)";
                // 
                // numJobTimeout
                // 
                this.numJobTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numJobTimeout.Location = new System.Drawing.Point(713, 270);
                this.numJobTimeout.Name = "numJobTimeout";
                this.numJobTimeout.Size = new System.Drawing.Size(66, 22);
                this.numJobTimeout.TabIndex = 45;
                this.numJobTimeout.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
                // 
                // label8
                // 
                this.label8.AutoSize = true;
                this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label8.Location = new System.Drawing.Point(670, 250);
                this.label8.Name = "label8";
                this.label8.Size = new System.Drawing.Size(191, 18);
                this.label8.TabIndex = 44;
                this.label8.Text = "Job Notification Timeout";
                // 
                // txtBiddingMessage
                // 
                this.txtBiddingMessage.AutoSize = true;
                this.txtBiddingMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
                this.txtBiddingMessage.ForeColor = System.Drawing.Color.Red;
                this.txtBiddingMessage.Location = new System.Drawing.Point(140, 76);
                this.txtBiddingMessage.Name = "txtBiddingMessage";
                this.txtBiddingMessage.Size = new System.Drawing.Size(283, 14);
                this.txtBiddingMessage.TabIndex = 43;
                this.txtBiddingMessage.Text = "Problem on getting Bidding Info from Server";
                this.txtBiddingMessage.Visible = false;
                // 
                // txtFareMessage
                // 
                this.txtFareMessage.AutoSize = true;
                this.txtFareMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
                this.txtFareMessage.ForeColor = System.Drawing.Color.Red;
                this.txtFareMessage.Location = new System.Drawing.Point(32, 131);
                this.txtFareMessage.Name = "txtFareMessage";
                this.txtFareMessage.Size = new System.Drawing.Size(238, 14);
                this.txtFareMessage.TabIndex = 42;
                this.txtFareMessage.Text = "Problem on getting Fares from Server";
                this.txtFareMessage.Visible = false;
                // 
                // label6
                // 
                this.label6.AutoSize = true;
                this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
                this.label6.Location = new System.Drawing.Point(784, 221);
                this.label6.Name = "label6";
                this.label6.Size = new System.Drawing.Size(45, 14);
                this.label6.TabIndex = 41;
                this.label6.Text = "(Mins)";
                // 
                // numBreakDuration
                // 
                this.numBreakDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numBreakDuration.Location = new System.Drawing.Point(713, 219);
                this.numBreakDuration.Name = "numBreakDuration";
                this.numBreakDuration.Size = new System.Drawing.Size(66, 22);
                this.numBreakDuration.TabIndex = 40;
                this.numBreakDuration.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
                // 
                // label5
                // 
                this.label5.AutoSize = true;
                this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label5.Location = new System.Drawing.Point(690, 198);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(130, 18);
                this.label5.TabIndex = 39;
                this.label5.Text = "Break Duration :";
                // 
                // chkDisableMeterAccJob
                // 
                this.chkDisableMeterAccJob.AutoSize = true;
                this.chkDisableMeterAccJob.Font = new System.Drawing.Font("Tahoma", 10F);
                this.chkDisableMeterAccJob.ForeColor = System.Drawing.Color.Blue;
                this.chkDisableMeterAccJob.Location = new System.Drawing.Point(307, 107);
                this.chkDisableMeterAccJob.Name = "chkDisableMeterAccJob";
                this.chkDisableMeterAccJob.Size = new System.Drawing.Size(189, 21);
                this.chkDisableMeterAccJob.TabIndex = 38;
                this.chkDisableMeterAccJob.Text = "Disable Meter For Acc Jobs";
                this.chkDisableMeterAccJob.UseVisualStyleBackColor = true;
                // 
                // chkEnableMeterWaitingCharges
                // 
                this.chkEnableMeterWaitingCharges.AutoSize = true;
                this.chkEnableMeterWaitingCharges.Font = new System.Drawing.Font("Tahoma", 10F);
                this.chkEnableMeterWaitingCharges.ForeColor = System.Drawing.Color.Blue;
                this.chkEnableMeterWaitingCharges.Location = new System.Drawing.Point(510, 107);
                this.chkEnableMeterWaitingCharges.Name = "chkEnableMeterWaitingCharges";
                this.chkEnableMeterWaitingCharges.Size = new System.Drawing.Size(165, 21);
                this.chkEnableMeterWaitingCharges.TabIndex = 37;
                this.chkEnableMeterWaitingCharges.Text = "Meter Waiting Charges";
                this.chkEnableMeterWaitingCharges.UseVisualStyleBackColor = true;
                // 
                // chkEnableOptionalMeter
                // 
                this.chkEnableOptionalMeter.AutoSize = true;
                this.chkEnableOptionalMeter.Font = new System.Drawing.Font("Tahoma", 10F);
                this.chkEnableOptionalMeter.ForeColor = System.Drawing.Color.Blue;
                this.chkEnableOptionalMeter.Location = new System.Drawing.Point(177, 107);
                this.chkEnableOptionalMeter.Name = "chkEnableOptionalMeter";
                this.chkEnableOptionalMeter.Size = new System.Drawing.Size(115, 21);
                this.chkEnableOptionalMeter.TabIndex = 36;
                this.chkEnableOptionalMeter.Text = "Optional Meter";
                this.chkEnableOptionalMeter.UseVisualStyleBackColor = true;
                // 
                // chkShowSoundOnZoneChange
                // 
                this.chkShowSoundOnZoneChange.AutoSize = true;
                this.chkShowSoundOnZoneChange.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShowSoundOnZoneChange.Location = new System.Drawing.Point(478, 257);
                this.chkShowSoundOnZoneChange.Name = "chkShowSoundOnZoneChange";
                this.chkShowSoundOnZoneChange.Size = new System.Drawing.Size(182, 22);
                this.chkShowSoundOnZoneChange.TabIndex = 31;
                this.chkShowSoundOnZoneChange.Text = "Sound On Zone Change";
                this.chkShowSoundOnZoneChange.UseVisualStyleBackColor = true;
                // 
                // chkDisableChangeJobPlot
                // 
                this.chkDisableChangeJobPlot.AutoSize = true;
                this.chkDisableChangeJobPlot.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkDisableChangeJobPlot.Location = new System.Drawing.Point(478, 233);
                this.chkDisableChangeJobPlot.Name = "chkDisableChangeJobPlot";
                this.chkDisableChangeJobPlot.Size = new System.Drawing.Size(181, 22);
                this.chkDisableChangeJobPlot.TabIndex = 30;
                this.chkDisableChangeJobPlot.Text = "Disable Change Job Plot";
                this.chkDisableChangeJobPlot.UseVisualStyleBackColor = true;
                // 
                // chkDisableRank
                // 
                this.chkDisableRank.AutoSize = true;
                this.chkDisableRank.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkDisableRank.Location = new System.Drawing.Point(478, 197);
                this.chkDisableRank.Name = "chkDisableRank";
                this.chkDisableRank.Size = new System.Drawing.Size(152, 22);
                this.chkDisableRank.TabIndex = 29;
                this.chkDisableRank.Text = "Disable Driver Rank";
                this.chkDisableRank.UseVisualStyleBackColor = true;
                // 
                // chkDisablePanic
                // 
                this.chkDisablePanic.AutoSize = true;
                this.chkDisablePanic.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkDisablePanic.Location = new System.Drawing.Point(478, 161);
                this.chkDisablePanic.Name = "chkDisablePanic";
                this.chkDisablePanic.Size = new System.Drawing.Size(158, 22);
                this.chkDisablePanic.TabIndex = 28;
                this.chkDisablePanic.Text = "Disable Panic Button";
                this.chkDisablePanic.UseVisualStyleBackColor = true;
                // 
                // chkIgnoreArriveAction
                // 
                this.chkIgnoreArriveAction.AutoSize = true;
                this.chkIgnoreArriveAction.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkIgnoreArriveAction.Location = new System.Drawing.Point(241, 414);
                this.chkIgnoreArriveAction.Name = "chkIgnoreArriveAction";
                this.chkIgnoreArriveAction.Size = new System.Drawing.Size(156, 22);
                this.chkIgnoreArriveAction.TabIndex = 27;
                this.chkIgnoreArriveAction.Text = "Ignore Arrive Action";
                this.chkIgnoreArriveAction.UseVisualStyleBackColor = true;
                // 
                // chkMessageStay
                // 
                this.chkMessageStay.AutoSize = true;
                this.chkMessageStay.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkMessageStay.Location = new System.Drawing.Point(241, 447);
                this.chkMessageStay.Name = "chkMessageStay";
                this.chkMessageStay.Size = new System.Drawing.Size(189, 22);
                this.chkMessageStay.TabIndex = 26;
                this.chkMessageStay.Text = "Message Stay on Screen";
                this.chkMessageStay.UseVisualStyleBackColor = true;
                // 
                // ShowPlotOnJobOffer
                // 
                this.ShowPlotOnJobOffer.AutoSize = true;
                this.ShowPlotOnJobOffer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ShowPlotOnJobOffer.Location = new System.Drawing.Point(241, 343);
                this.ShowPlotOnJobOffer.Name = "ShowPlotOnJobOffer";
                this.ShowPlotOnJobOffer.Size = new System.Drawing.Size(176, 22);
                this.ShowPlotOnJobOffer.TabIndex = 25;
                this.ShowPlotOnJobOffer.Text = "Show Plot on Job Offer";
                this.ShowPlotOnJobOffer.UseVisualStyleBackColor = true;
                // 
                // chkShowAlertOnJobLater
                // 
                this.chkShowAlertOnJobLater.AutoSize = true;
                this.chkShowAlertOnJobLater.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShowAlertOnJobLater.Location = new System.Drawing.Point(241, 308);
                this.chkShowAlertOnJobLater.Name = "chkShowAlertOnJobLater";
                this.chkShowAlertOnJobLater.Size = new System.Drawing.Size(174, 22);
                this.chkShowAlertOnJobLater.TabIndex = 24;
                this.chkShowAlertOnJobLater.Text = "Show Alert On JobLate";
                this.chkShowAlertOnJobLater.UseVisualStyleBackColor = true;
                // 
                // chkShowCustomerMobileNo
                // 
                this.chkShowCustomerMobileNo.AutoSize = true;
                this.chkShowCustomerMobileNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShowCustomerMobileNo.Location = new System.Drawing.Point(241, 271);
                this.chkShowCustomerMobileNo.Name = "chkShowCustomerMobileNo";
                this.chkShowCustomerMobileNo.Size = new System.Drawing.Size(197, 22);
                this.chkShowCustomerMobileNo.TabIndex = 23;
                this.chkShowCustomerMobileNo.Text = "Show Customer Mobile No";
                this.chkShowCustomerMobileNo.UseVisualStyleBackColor = true;
                // 
                // chkShowNavigation
                // 
                this.chkShowNavigation.AutoSize = true;
                this.chkShowNavigation.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShowNavigation.Location = new System.Drawing.Point(241, 233);
                this.chkShowNavigation.Name = "chkShowNavigation";
                this.chkShowNavigation.Size = new System.Drawing.Size(133, 22);
                this.chkShowNavigation.TabIndex = 22;
                this.chkShowNavigation.Text = "Show Navigation";
                this.chkShowNavigation.UseVisualStyleBackColor = true;
                // 
                // chkShowCompletedJobs
                // 
                this.chkShowCompletedJobs.AutoSize = true;
                this.chkShowCompletedJobs.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShowCompletedJobs.Location = new System.Drawing.Point(241, 197);
                this.chkShowCompletedJobs.Name = "chkShowCompletedJobs";
                this.chkShowCompletedJobs.Size = new System.Drawing.Size(169, 22);
                this.chkShowCompletedJobs.TabIndex = 21;
                this.chkShowCompletedJobs.Text = "Show Completed Jobs";
                this.chkShowCompletedJobs.UseVisualStyleBackColor = true;
                // 
                // chkShowPlots
                // 
                this.chkShowPlots.AutoSize = true;
                this.chkShowPlots.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkShowPlots.Location = new System.Drawing.Point(241, 161);
                this.chkShowPlots.Name = "chkShowPlots";
                this.chkShowPlots.Size = new System.Drawing.Size(96, 22);
                this.chkShowPlots.TabIndex = 20;
                this.chkShowPlots.Text = "Show Plots";
                this.chkShowPlots.UseVisualStyleBackColor = true;
                // 
                // chkEnableAutoRotate
                // 
                this.chkEnableAutoRotate.AutoSize = true;
                this.chkEnableAutoRotate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableAutoRotate.Location = new System.Drawing.Point(17, 414);
                this.chkEnableAutoRotate.Name = "chkEnableAutoRotate";
                this.chkEnableAutoRotate.Size = new System.Drawing.Size(196, 22);
                this.chkEnableAutoRotate.TabIndex = 19;
                this.chkEnableAutoRotate.Text = "Enable AutoRotate Screen";
                this.chkEnableAutoRotate.UseVisualStyleBackColor = true;
                // 
                // chkEnableCompanyCars
                // 
                this.chkEnableCompanyCars.AutoSize = true;
                this.chkEnableCompanyCars.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableCompanyCars.Location = new System.Drawing.Point(17, 379);
                this.chkEnableCompanyCars.Name = "chkEnableCompanyCars";
                this.chkEnableCompanyCars.Size = new System.Drawing.Size(170, 22);
                this.chkEnableCompanyCars.TabIndex = 18;
                this.chkEnableCompanyCars.Text = "Enable Company Cars";
                this.chkEnableCompanyCars.UseVisualStyleBackColor = true;
                this.chkEnableCompanyCars.CheckedChanged += new EventHandler(chkEnableCompanyCars_CheckedChanged);
                // 
                // chkEnableJ15Jobs
                // 
                this.chkEnableJ15Jobs.AutoSize = true;
                this.chkEnableJ15Jobs.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableJ15Jobs.Location = new System.Drawing.Point(17, 343);
                this.chkEnableJ15Jobs.Name = "chkEnableJ15Jobs";
                this.chkEnableJ15Jobs.Size = new System.Drawing.Size(187, 22);
                this.chkEnableJ15Jobs.TabIndex = 17;
                this.chkEnableJ15Jobs.Text = "Enable J15 And J30 Jobs";
                this.chkEnableJ15Jobs.UseVisualStyleBackColor = true;
                // 
                // chkEnableCallCustomer
                // 
                this.chkEnableCallCustomer.AutoSize = true;
                this.chkEnableCallCustomer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableCallCustomer.Location = new System.Drawing.Point(17, 308);
                this.chkEnableCallCustomer.Name = "chkEnableCallCustomer";
                this.chkEnableCallCustomer.Size = new System.Drawing.Size(163, 22);
                this.chkEnableCallCustomer.TabIndex = 16;
                this.chkEnableCallCustomer.Text = "Enable Call Customer";
                this.chkEnableCallCustomer.UseVisualStyleBackColor = true;
                // 
                // chkEnableJobExtraCharges
                // 
                this.chkEnableJobExtraCharges.AutoSize = true;
                this.chkEnableJobExtraCharges.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableJobExtraCharges.Location = new System.Drawing.Point(17, 271);
                this.chkEnableJobExtraCharges.Name = "chkEnableJobExtraCharges";
                this.chkEnableJobExtraCharges.Size = new System.Drawing.Size(193, 22);
                this.chkEnableJobExtraCharges.TabIndex = 15;
                this.chkEnableJobExtraCharges.Text = "Enable Job Extra Charges";
                this.chkEnableJobExtraCharges.UseVisualStyleBackColor = true;
                // 
                // chkEnableRecoverJob
                // 
                this.chkEnableRecoverJob.AutoSize = true;
                this.chkEnableRecoverJob.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableRecoverJob.Location = new System.Drawing.Point(17, 233);
                this.chkEnableRecoverJob.Name = "chkEnableRecoverJob";
                this.chkEnableRecoverJob.Size = new System.Drawing.Size(154, 22);
                this.chkEnableRecoverJob.TabIndex = 14;
                this.chkEnableRecoverJob.Text = "Enable Recover Job";
                this.chkEnableRecoverJob.UseVisualStyleBackColor = true;
                // 
                // chkEnableLogoutAuthorization
                // 
                this.chkEnableLogoutAuthorization.AutoSize = true;
                this.chkEnableLogoutAuthorization.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableLogoutAuthorization.Location = new System.Drawing.Point(17, 197);
                this.chkEnableLogoutAuthorization.Name = "chkEnableLogoutAuthorization";
                this.chkEnableLogoutAuthorization.Size = new System.Drawing.Size(206, 22);
                this.chkEnableLogoutAuthorization.TabIndex = 13;
                this.chkEnableLogoutAuthorization.Text = "Enable Logout Authorization";
                this.chkEnableLogoutAuthorization.UseVisualStyleBackColor = true;
                // 
                // chkEnableFlagDown
                // 
                this.chkEnableFlagDown.AutoSize = true;
                this.chkEnableFlagDown.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableFlagDown.Location = new System.Drawing.Point(17, 161);
                this.chkEnableFlagDown.Name = "chkEnableFlagDown";
                this.chkEnableFlagDown.Size = new System.Drawing.Size(141, 22);
                this.chkEnableFlagDown.TabIndex = 12;
                this.chkEnableFlagDown.Text = "Enable Flag Down";
                this.chkEnableFlagDown.UseVisualStyleBackColor = true;
                // 
                // chkEnableFareMeter
                // 
                this.chkEnableFareMeter.AutoSize = true;
                this.chkEnableFareMeter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableFareMeter.Location = new System.Drawing.Point(17, 106);
                this.chkEnableFareMeter.Name = "chkEnableFareMeter";
                this.chkEnableFareMeter.Size = new System.Drawing.Size(141, 22);
                this.chkEnableFareMeter.TabIndex = 11;
                this.chkEnableFareMeter.Text = "Enable FareMeter";
                this.chkEnableFareMeter.UseVisualStyleBackColor = true;
                this.chkEnableFareMeter.CheckedChanged += new System.EventHandler(this.chkEnableFareMeter_CheckedChanged);
                // 
                // label4
                // 
                this.label4.AutoSize = true;
                this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label4.Location = new System.Drawing.Point(14, 31);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(115, 18);
                this.label4.TabIndex = 10;
                this.label4.Text = "Navigation App :";
                // 
                // chkEnableBidding
                // 
                this.chkEnableBidding.AutoSize = true;
                this.chkEnableBidding.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableBidding.Location = new System.Drawing.Point(17, 72);
                this.chkEnableBidding.Name = "chkEnableBidding";
                this.chkEnableBidding.Size = new System.Drawing.Size(119, 22);
                this.chkEnableBidding.TabIndex = 8;
                this.chkEnableBidding.Text = "Enable Bidding";
                this.chkEnableBidding.UseVisualStyleBackColor = true;
                // 
                // ddlNavigation
                // 
                this.ddlNavigation.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ddlNavigation.FormattingEnabled = true;
                this.ddlNavigation.Items.AddRange(new object[] {
            "All",
            "Google Navigation",
            "Waze Navigation",
            "Here we Go Navigation",
            "None"});
                this.ddlNavigation.Location = new System.Drawing.Point(141, 27);
                this.ddlNavigation.Name = "ddlNavigation";
                this.ddlNavigation.Size = new System.Drawing.Size(336, 26);
                this.ddlNavigation.TabIndex = 9;



                // 
                // chkDisableChangeDest
                // 
                this.chkDisableChangeDest.BackColor = System.Drawing.Color.Transparent;
                this.chkDisableChangeDest.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkDisableChangeDest.Location = new System.Drawing.Point(480, 390);
                this.chkDisableChangeDest.Name = "chkDisableChangeDest";
                this.chkDisableChangeDest.Size = new System.Drawing.Size(236, 22);
                this.chkDisableChangeDest.TabIndex = 1;
                this.chkDisableChangeDest.Text = "Disable Change Job Destination";

                // 
                // chkDisableRejectJob
                // 
                this.chkDisableRejectJob.BackColor = System.Drawing.Color.Transparent;
                this.chkDisableRejectJob.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkDisableRejectJob.Location = new System.Drawing.Point(480, 360);
                this.chkDisableRejectJob.Name = "chkDisableRejectJob";
                this.chkDisableRejectJob.Size = new System.Drawing.Size(146, 22);
                this.chkDisableRejectJob.TabIndex = 0;
                this.chkDisableRejectJob.Text = "Disable Reject Job";




                // 
                // chkEnableManualFares
                // 
                this.chkEnableManualFares.AutoSize = true;
                this.chkEnableManualFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableManualFares.Location = new System.Drawing.Point(17, 481);
                this.chkEnableManualFares.Name = "chkEnableManualFares";
                this.chkEnableManualFares.Size = new System.Drawing.Size(161, 22);
                this.chkEnableManualFares.TabIndex = 55;
                this.chkEnableManualFares.Text = "Enable Manual Fares";
                this.chkEnableManualFares.UseVisualStyleBackColor = true;
                this.chkEnableManualFares.CheckedChanged += new EventHandler(chkEnableManualFares_CheckedChanged);


                this.chkEnableOptionalManualFares.AutoSize = true;
                this.chkEnableOptionalManualFares.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnableOptionalManualFares.Location = new System.Drawing.Point(50, 500);
                this.chkEnableOptionalManualFares.Name = "chkEnableOptionalManualFares";
                this.chkEnableOptionalManualFares.Size = new System.Drawing.Size(161, 22);
                this.chkEnableOptionalManualFares.TabIndex = 55;
                this.chkEnableOptionalManualFares.Text = "(Optional)";
                this.chkEnableOptionalManualFares.UseVisualStyleBackColor = true;
                this.chkEnableOptionalManualFares.Visible = true;

                // 
                // chkEnablePriceBidding
                // 
                this.chkEnablePriceBidding.AutoSize = true;
                this.chkEnablePriceBidding.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkEnablePriceBidding.Location = new System.Drawing.Point(17, 512);
                this.chkEnablePriceBidding.Name = "chkEnablePriceBidding";
                this.chkEnablePriceBidding.Size = new System.Drawing.Size(154, 22);
                this.chkEnablePriceBidding.TabIndex = 56;
                this.chkEnablePriceBidding.Text = "Enable Price Bidding";
                this.chkEnablePriceBidding.UseVisualStyleBackColor = true;



                this.pnlSettings.ResumeLayout(false);
                this.pnlSettings.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.btnUpdateSettings)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numJobTimeout)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numBreakDuration)).EndInit();

                //((System.ComponentModel.ISupportInitialize)(this.chkDisableChangeDest)).EndInit();
                // ((System.ComponentModel.ISupportInitialize)(this.chkDisableRejectJob)).EndInit();

                DisplayPDASettingsPanel();

            }


        }

        void chkEnableCompanyCars_CheckedChanged(object sender, EventArgs e)
        {
          
            if (chkEnableCompanyCars.Checked == true)
            {
                CompanyVehicle.Item.Visibility = ElementVisibility.Visible;
                //radpageview1.Pages.Add(CompanyVehicle);
                
            }
            else if (chkEnableCompanyCars.Checked == false)
            {
               CompanyVehicle.Item.Visibility = ElementVisibility.Hidden;
               // radpageview1.Pages.Remove(CompanyVehicle);
                if (objMaster.Current != null && objMaster.Current.Id > 0)
                {
                    int driverId = objMaster.Current.Id;
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var list = db.Fleet_Driver_CompanyVehicles.Where(c => c.DriverId == driverId).ToList();
                        if (list.Count > 0)
                        {
                            db.Fleet_Driver_CompanyVehicles.DeleteAllOnSubmit(list);
                            db.SubmitChanges();
                        }
                    }
                }
               
                DisplayCompanyVehicle();

            }
        }

        void chkHidePickupAndDest_CheckedChanged(object sender, EventArgs e)
        {


            if (chkHidePickupAndDest.Checked)
            {

                ddlHidePickupAndDestinationType.Visible = true;
            }
            else
                ddlHidePickupAndDestinationType.Visible = false;
        }

        void chkEnableManualFares_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableManualFares.Checked)
            {
                chkEnableFareMeter.Checked = false;
                chkEnableFareMeter.Enabled = false;


            }
            else
            {

                chkEnableFareMeter.Enabled = true;


            }
        }

        private void InitializeVehicleHistoryPanel()
        {
            if (grdVehicles == null)
            {


                this.radLabel30 = new Telerik.WinControls.UI.RadLabel();
                this.grdVehicles = new UI.MyGridView();

                ((System.ComponentModel.ISupportInitialize)(this.radLabel30)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdVehicles)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdVehicles.MasterTemplate)).BeginInit();

                this.radPageViewPage3.Controls.Add(this.radLabel30);
                this.radPageViewPage3.Controls.Add(this.grdVehicles);


                // 
                // radLabel30
                // 
                this.radLabel30.AutoSize = false;
                this.radLabel30.BackColor = System.Drawing.Color.Crimson;
                this.radLabel30.BorderVisible = true;
                this.radLabel30.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
                this.radLabel30.ForeColor = System.Drawing.Color.White;
                this.radLabel30.Location = new System.Drawing.Point(0, 3);
                this.radLabel30.Name = "radLabel30";
                // 
                // 
                // 
                this.radLabel30.RootElement.ForeColor = System.Drawing.Color.White;
                this.radLabel30.Size = new System.Drawing.Size(855, 34);
                this.radLabel30.TabIndex = 89;
                this.radLabel30.Text = "Vehicles History";
                this.radLabel30.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel30.GetChildAt(0))).BorderVisible = true;
                ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel30.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel30.GetChildAt(0))).Text = "Vehicles History";
                ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel30.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
                ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel30.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
                ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel30.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;
                // 
                // grdVehicles
                // 
                this.grdVehicles.AutoCellFormatting = false;
                this.grdVehicles.EnableCheckInCheckOut = false;
                this.grdVehicles.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.grdVehicles.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdVehicles.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdVehicles.Location = new System.Drawing.Point(1, 37);
                // 
                // grdVehicles
                // 
                this.grdVehicles.MasterTemplate.AllowAddNewRow = false;
                this.grdVehicles.MasterTemplate.AllowEditRow = false;
                this.grdVehicles.MasterTemplate.ShowRowHeaderColumn = false;
                this.grdVehicles.Name = "grdVehicles";
                this.grdVehicles.PKFieldColumnName = "";
                this.grdVehicles.ShowGroupPanel = false;
                this.grdVehicles.ShowImageOnActionButton = true;
                this.grdVehicles.Size = new System.Drawing.Size(854, 396);
                this.grdVehicles.TabIndex = 88;
                this.grdVehicles.Text = "myGridView1";


                ((System.ComponentModel.ISupportInitialize)(this.radLabel30)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdVehicles.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdVehicles)).EndInit();


                FormatAssignedVehicleGrid();

                DisplayDriverVehicleHistory();
            }

        }


        private void DisplayPDASettingsPanel()
        {
            if (objMaster.Current != null && objMaster.PrimaryKeyValue != null)
            {


                if (objMaster.Current.Fleet_Driver_PDASettings.Count > 0)
                {
                    SetFareMeterState();

                    chkEnableFareMeter.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableFareMeter.ToBool();

                 
                    chkEnableJobExtraCharges.Checked = false;
                    chkShowFareonExtraCharges.Checked = false;

                    chkEnableJobExtraCharges.Visible = false;
                    chkShowFareonExtraCharges.Visible = false;

                    txtFareMessage.Text = objMaster.Current.Fleet_Driver_PDASettings[0].FareMeterType.ToStr().Trim();
                    chkEnableFlagDown.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableFlagDown.ToBool();
                    chkEnableJ15Jobs.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableJ15J30Jobs.ToBool();
                    chkEnableCallCustomer.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableCallCustomer.ToBool();
                    chkEnableCompanyCars.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].HasCompanyCars.ToBool();
                    chkEnableJobExtraCharges.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableJobExtraCharges.ToBool();
                    chkEnableLogoutAuthorization.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableLogoutAuthorization.ToBool();
                    chkEnableLogoutOnReject.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].LogoutOnRejectJob.ToBool();
                    chkEnableMeterWaitingCharges.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableFareMeterWaitingCharges.ToBool();
                    chkEnableOptionalMeter.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].OptionalFareMeter.ToBool();
                    chkVoiceOnClearMeter.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableFareMeterVoice.ToBool();
               
                    chkEnableRecoverJob.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableRecoverJob.ToBool();
                    chkEnableBidding.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableBidding.ToBool();

                    chkEnableAutoRotate.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableAutoRotateScreen.ToBool();
                    chkDisableRank.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableDriverRank.ToBool();
                    chkDisablePanic.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisablePanicButton.ToBool();
                    chkDisableOnBreak.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableOnBreak.ToBool();
                    chkDisableMeterAccJob.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableFareMeterOnAccJob.ToBool();
                    chkDisableChangeJobPlot.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableChangeJobPlots.ToBool();
                    chkDisableBase.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableBase.ToBool();
                    chkDisableJobAuth.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableRejectJobAuth.ToBool();

                    if (chkEnableBidding.Checked)
                    {

                        if (AppVars.objPolicyConfiguration.BiddingType.ToInt() == 1)
                        {
                            txtBiddingMessage.Text = "Fastest Finger";
                        }
                        else if (AppVars.objPolicyConfiguration.BiddingType.ToInt() == 2)
                        {
                            txtBiddingMessage.Text = "Nearest Driver";
                        }
                        else if (AppVars.objPolicyConfiguration.BiddingType.ToInt() == 3)
                        {
                            txtBiddingMessage.Text = "Longest Waiting in Queue";
                        }

                    }


                    numBreakDuration.Value = objMaster.Current.Fleet_Driver_PDASettings[0].BreakTime.ToDecimal();
                    numJobTimeout.Value = objMaster.Current.Fleet_Driver_PDASettings[0].JobTimeOutInterval.ToDecimal();

                    if( objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal()>=23.29m)
                    {

                        chkHidePickupAndDest.CheckedChanged+=new EventHandler(chkHidePickupAndDest_CheckedChanged); 
                    }

                    chkHidePickupAndDest.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].HidePickAndDestination.ToBool();

                    if (chkHidePickupAndDest.Checked && objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() >= 23.29m)
                    {
                        ddlHidePickupAndDestinationType.Visible = true;

                    }

                    if (objMaster.Current.Fleet_Driver_PDASettings[0].OldPdaVersion.ToInt() == 2)
                        ddlHidePickupAndDestinationType.SelectedIndex = 1;
                    else if (objMaster.Current.Fleet_Driver_PDASettings[0].OldPdaVersion.ToInt() == 3)
                        ddlHidePickupAndDestinationType.SelectedIndex = 2;

                    else if (objMaster.Current.Fleet_Driver_PDASettings[0].OldPdaVersion.ToInt() == 4)
                        ddlHidePickupAndDestinationType.SelectedIndex = 3;
                    else
                        ddlHidePickupAndDestinationType.SelectedIndex = 0;


                    chkIgnoreArriveAction.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].IgnoreArriveAction.ToBool();
                    chkMessageStay.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].MessageStayOnScreen.ToBool();
                    chkShiftOverLogout.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].LogoutOnOverShift.ToBool();
                    chkShowAlertOnJobLater.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].NotifyOnJobLate.ToBool();
                    chkShowCompletedJobs.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowCompletedJob.ToBool();
                    chkShowCustomerMobileNo.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowCustomerMobileNo.ToBool();
                    chkShowFareonExtraCharges.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowFaresOnExtraCharges.ToBool();
                    chkShowNavigation.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowNavigation.ToBool();
                    chkShowPlots.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowPlots.ToBool();
                    chkShowSoundOnZoneChange.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].NotifyOnZoneChange.ToBool();
                    ShowPlotOnJobOffer.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowPlotOnJobOffer.ToBool();
                    chkDisableRejectJob.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableRejectJob.ToBool();
                    chkDisableChangeDest.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableChangeDestination.ToBool();


                    chkEnablePriceBidding.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnablePriceBidding.ToBool();
                    chkEnableManualFares.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableManualFares.ToBool();

                    chkEnableOptionalManualFares.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].EnableOptionalManualFares.ToBool();

                    

                    if (AppVars.objPolicyConfiguration.TcpConnectionType.ToInt() == 2 || objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() < 8.90m)
                    {
                        chkShowSoundOnZoneChange.Checked = false;
                        chkShowSoundOnZoneChange.Visible = false;

                    }

                    // 8.3
                    if (objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() >= 8.3m)
                    {
                        chkDisableNoPickup.Visible = true;
                        chkDisableSTC.Visible = true;
                        chkDisableAlarm.Visible = true;
                        chkShowSpecReq.Visible = true;
                        chkShowJobAsAlert.Visible = true;
                        chkDisableFareOnAccJob.Visible = true;


                        chkDisableNoPickup.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableNoPickup.ToBool();
                        chkDisableSTC.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableSTC.ToBool();
                        chkDisableAlarm.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableSetAlarm.ToBool();
                        chkShowSpecReq.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowSpecReqOnFront.ToBool();
                        chkShowJobAsAlert.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].ShowJobAsAlert.ToBool();
                        chkDisableFareOnAccJob.Checked = objMaster.Current.Fleet_Driver_PDASettings[0].DisableFareOnAccJob.ToBool();


                    }


                    if (AppVars.objPolicyConfiguration.PDAJobAlertOnly.ToBool() == false)
                    {
                        chkShowJobAsAlert.Checked = false;
                        chkShowJobAsAlert.Visible = false;
                    }


                    if (AppVars.listUserRights.Count(c => c.formName == "frmBookingDashBoard" && c.functionId == "PRICE BIDDING") == 0)
                    {

                        chkEnablePriceBidding.Checked = false;
                        chkEnablePriceBidding.Visible = false;

                    }

                    //if (AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool() == false || AppVars.objPolicyConfiguration.EnableBidding.ToBool() == false)
                    //{
                    //    chkEnableBidding.Checked = false;
                    //    chkEnableBidding.Visible = false;

                    //}

                    //

                    if (objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() < 10.10m)
                    {

                        chkEnableOptionalManualFares.Visible = false;
                    }

                    if (chkEnableFareMeter.Checked)
                    {

                        if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                        {
                            txtFareMessage.Text = "PeakOff Peak Fares";
                        }
                        else
                        {
                            txtFareMessage.Text = "Mileage Fares";

                        }

                    }



                    txtPDAVer.Text = "Current PDA Version : " + (objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion != null ? objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToStr() : "???");
                    //  txtPDAVer.Visible = true;




                    if (objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() < 7.2m)
                    {
                        pnlSettings.Enabled = false;


                    }



                    int navigationType = objMaster.Current.Fleet_Driver_PDASettings[0].NavigationApp.ToInt();


                    if (navigationType == 1)
                    {
                        ddlNavigation.SelectedIndex = 1;
                    }
                    else if (navigationType == 2)
                    {
                        ddlNavigation.SelectedIndex = 2;
                    }
                    else if (navigationType == 3)
                    {
                        ddlNavigation.SelectedIndex = 3;
                    }
                    else if (navigationType == 5)
                    {
                        ddlNavigation.SelectedIndex = 4;
                    }
                    else
                    {
                        ddlNavigation.SelectedIndex = 0;
                    }






                }
                else
                {
                    pnlSettings.Enabled = false;
                    txtPDAVer.Text = "Current PDA Version : " + AppVars.objPolicyConfiguration.PDAVersion.ToStr();

                }
            }

        }


        System.Windows.Forms.Timer tmrDelete = null;

        void grdAvailability_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                if (tmrDelete == null)
                {
                    tmrDelete = new System.Windows.Forms.Timer();
                    tmrDelete.Enabled = true;
                    tmrDelete.Tick += new EventHandler(tmrDelete_Tick);
                    tmrDelete.Interval = 2000;
                    tmrDelete.Start();
                }

            }
        }




        void tmrDelete_Tick(object sender, EventArgs e)
        {
            try
            {

                ClearAvailability();
                tmrDelete.Stop();
                tmrDelete.Dispose();
                tmrDelete = null;
            }
            catch
            {


            }
        }

        private int _MapType;

        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }

        void frmDriver_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                DisposeObjects();


                openFileDialog1.Dispose();
                this.Dispose(true);
                GC.Collect();
            }
            catch (Exception ex)
            {

            }
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


        void grdAvailability_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewDataRowInfo)
            {
                dtpAvailDate.Value = e.Row.Cells[COL_AVAILABILITY.BECAMEAVAIL].Value.ToDateorNull();
                dtpEndingDate.Value = e.Row.Cells[COL_AVAILABILITY.ENDINGDATE].Value.ToDateorNull();



                chkEndDrier.Checked = dtpEndingDate.Value != null;

            }
        }

        void frmDriver_Shown(object sender, EventArgs e)
        {
            txtDriverNo.Focus();


            if (objMaster.PrimaryKeyValue == null)
            {
                chkActiveDriver.Checked = true;
                chkActiveDriver.Enabled = false;

            }


            if (grdDriverComplaints.Columns.Count > 0)
            {
                grdDriverComplaints.Columns["Id"].IsVisible = false;
                grdDriverComplaints.Columns["RefNo"].Width = 80;
                grdDriverComplaints.Columns["ComplainDate"].Width = 100;
                grdDriverComplaints.Columns["IncidentDate"].Width = 100;
                grdDriverComplaints.Columns["CustomerName"].Width = 145;
                grdDriverComplaints.Columns["JobRefNo"].Width = 80;
                grdDriverComplaints.Columns["ComplainDescription"].Width = 170;
                grdDriverComplaints.Columns["ResultDescription"].Width = 170;
                grdDriverComplaints.Columns["RefNo"].HeaderText = "Ref No";
                grdDriverComplaints.Columns["JobRefNo"].HeaderText = "Job Ref No";
                grdDriverComplaints.Columns["ComplainDate"].HeaderText = "Complain Date";
                grdDriverComplaints.Columns["IncidentDate"].HeaderText = "Incident Date";
                (grdDriverComplaints.Columns["IncidentDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                (grdDriverComplaints.Columns["ComplainDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                grdDriverComplaints.Columns["CustomerName"].HeaderText = "Customer Name";
                grdDriverComplaints.Columns["ComplainDescription"].HeaderText = "Complain Description";
                grdDriverComplaints.Columns["ResultDescription"].HeaderText = "Result Description";
            }
        }
        private void FillCombos()
        {
            //ComboFunctions.FillVehicleCombo(ddlVehicle);
            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);
            ComboFunctions.FillDriverTypeCombo(ddlDriverType);
            ComboFunctions.FillVehicleColorsCombo(ddlVehicleColor);
            FillSubCompanyCombo(ddlSubCompany);
            ddlSubCompany.SelectedIndexChanged += new EventHandler(ddlSubCompany_SelectedIndexChanged);

          //  ddlCategory.Items.Add(new RadListDataItem { Text = "Hackney", Value = 0 });
            ddlCategory.Items.Add(new RadListDataItem { Text="Hackney", Value=1 });
            ddlCategory.Items.Add(new RadListDataItem { Text = "Private Hire", Value = 2 });
        }

        void ddlSubCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (General.GetQueryable<Gen_MainCompany>(c => c.CompanyName == ddlSubCompany.Text).Count() > 0)
                {
                    lblPrimeCompanyRent.Visible = false;
                    numPrimeCompanyRent.Visible = false;
                   
                }
                else
                {
                    lblPrimeCompanyRent.Visible = true;
                    numPrimeCompanyRent.Value = 0.00m;
                    numPrimeCompanyRent.Visible = true;
                 
                }
            }
            catch (Exception ex)
            {


            }
        }


        private void FillSubCompanyCombo(ComboBox cbo)
        {
            cbo.DisplayMember = "CompanyName";
            cbo.ValueMember = "Id";
            cbo.DataSource = General.GetQueryable<Gen_SubCompany>(null).OrderBy(c => c.CompanyName).ToList();

            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.SelectedValue = AppVars.objSubCompany.Id;


            if (cbo.Items.Count == 1 || AppVars.DefaultDriverSubCompanyId != 0)
            {
                lblSubCompany.Visible = false;
                cbo.Visible = false;
            }
        }


        string[] res = null;
        string searchTxt = "";

     


        AutoCompleteTextBox aTxt = null;
        WebClient wc = null;

     
        private void CancelWebClientAsync()
        {
            if (wc != null)
            {
                wc.CancelAsync();
            }
        }

      



        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {


                if (e.Cancelled)
                {
                    return;
                }


                var xmlElm = XElement.Parse(e.Result);


                res = (from elm in xmlElm.Descendants()

                       // where elm.Name == "description"
                       //&& (elm.Value.ToLower().Contains("united kingdom") || elm.Value.ToLower().Contains("uk"))
                       where elm.Name == "formatted_address"
                       select elm.Value).ToArray<string>();


                ShowAddresses();

            }
            catch
            {


            }
        }

        private void ShowAddresses()
        {

            var finalList = (from a in AppVars.zonesList
                             from b in res
                             where b.Contains(a)

                             select b).ToArray<string>();


            if (finalList.Count() > 0)
                finalList = finalList.Union(res).ToArray<string>();

            else
                finalList = res;


            aTxt.ListBoxElement.Items.Clear();
            aTxt.ListBoxElement.Items.AddRange(finalList);


            if (aTxt.ListBoxElement.Items.Count == 0)
                aTxt.ResetListBox();
            else
                aTxt.ShowListBox();



        }

        private void FormatDocumentsGrid()
        {
            grdDocuments.AllowAutoSizeColumns = true;
            grdDocuments.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


            grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_DOCUMENT.ID;
            grdDocuments.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_DOCUMENT.MASTERID;
            grdDocuments.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_DOCUMENT.FULLPATH;
            grdDocuments.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_DOCUMENT.DOCUMENTTITLEID;
            grdDocuments.Columns.Add(col);

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "Expiry Date";
            colDate.Name = COL_DOCUMENT.EXPIRYDATE;
            // colDate.Format = DateTimePickerFormat.Custom;
            colDate.CustomFormat = "dd/MM/yyyy HH:mm";
            colDate.FormatString = "{0:dd/MM/yyyy HH:mm}";
            colDate.Width = 110;
            col.ReadOnly = false;
            grdDocuments.Columns.Add(colDate);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Badge #";
            col.Width = 60;
            col.ReadOnly = false;
            col.Name = COL_DOCUMENT.BADGENUMBER;
            grdDocuments.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_DOCUMENT.DOCUMENTTITLE;
            col.Width = 80;
            col.ReadOnly = true;
            col.Name = COL_DOCUMENT.DOCUMENTTITLE;
            grdDocuments.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_DOCUMENT.FILENAME;
            col.Width = 40;
            col.ReadOnly = true;
            col.Name = COL_DOCUMENT.FILENAME;
            grdDocuments.Columns.Add(col);


            GridViewCommandColumn col2 = new GridViewCommandColumn();
            col2.HeaderText = "";
            col2.Width = 30;
            col2.UseDefaultText = true;
            col2.DefaultText = "Clear";
            col2.Name = "Clear";
            grdDocuments.Columns.Add(col2);



            col2 = new GridViewCommandColumn();
            col2.HeaderText = "";
            col2.Width = 40;
            col2.UseDefaultText = true;
            col2.DefaultText = "Browse";
            col2.Name = "Load";
            grdDocuments.Columns.Add(col2);

            col2 = new GridViewCommandColumn();
            col2.HeaderText = "";
            col2.Width = 30;
            col2.UseDefaultText = true;
            col2.DefaultText = "View";
            col2.Name = "View";
            grdDocuments.Columns.Add(col2);


            OnNewDocuments();


            grdDocuments.AllowEditRow = true;

        }


        private void FormatAvailabilityGrid()
        {
            grdAvailability.AllowAutoSizeColumns = true;
            grdAvailability.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdAvailability.AllowAddNewRow = false;
            grdAvailability.ShowGroupPanel = false;
            grdAvailability.AutoCellFormatting = true;
            grdAvailability.ShowRowHeaderColumn = false;

            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AVAILABILITY.ID;
            grdAvailability.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AVAILABILITY.MASTERID;
            grdAvailability.Columns.Add(col);



            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "Became Available";
            colDate.Name = COL_AVAILABILITY.BECAMEAVAIL;
            colDate.Format = DateTimePickerFormat.Custom;
            colDate.CustomFormat = "dd/MM/yyyy";
            colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.Width = 120;
            col.ReadOnly = false;
            grdAvailability.Columns.Add(colDate);

            colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "Ending Date";
            colDate.Name = COL_AVAILABILITY.ENDINGDATE;
            colDate.Format = DateTimePickerFormat.Custom;
            colDate.CustomFormat = "dd/MM/yyyy";
            colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.Width = 120;
            col.ReadOnly = false;
            grdAvailability.Columns.Add(colDate);


            UI.GridFunctions.AddDeleteColumn(grdAvailability);




        }


        private void FormatAssignedVehicleGrid()
        {
            grdVehicles.AllowAutoSizeColumns = true;

            grdVehicles.AllowAddNewRow = false;
            grdVehicles.ShowGroupPanel = false;
            grdVehicles.AutoCellFormatting = true;
            grdVehicles.ShowRowHeaderColumn = false;
            grdVehicles.AllowColumnReorder = false;
            grdAvailability.AllowRowReorder = false;
            grdAvailability.EnableSorting = false;


            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_VEHICLES.ID;
            grdVehicles.Columns.Add(col);



            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "Assigned On";
            colDate.Name = COL_VEHICLES.ASSIGNEDON;
            colDate.Format = DateTimePickerFormat.Custom;
            colDate.CustomFormat = "dd/MM/yyyy";
            colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.Width = 110;
            col.ReadOnly = false;
            grdVehicles.Columns.Add(colDate);



            col = new GridViewTextBoxColumn();
            col.Name = COL_VEHICLES.VEHICLETYPEID;
            col.Width = 100;
            col.IsVisible = false;
            grdVehicles.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle Type";
            col.Name = COL_VEHICLES.VEHICLETYPENAME;
            col.Width = 100;
            grdVehicles.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Veh No";
            col.Name = COL_VEHICLES.VEHNO;
            col.Width = 90;
            grdVehicles.Columns.Add(col);


            col = new GridViewTextBoxColumn();

            col.HeaderText = "Color";
            col.Name = COL_VEHICLES.COLOR;
            col.Width = 80;
            grdVehicles.Columns.Add(col);


            col = new GridViewTextBoxColumn();

            col.HeaderText = "Owner";
            col.Name = COL_VEHICLES.OWNER;
            col.Width = 100;
            grdVehicles.Columns.Add(col);


            col = new GridViewTextBoxColumn();

            col.HeaderText = "Make";
            col.Name = COL_VEHICLES.MAKE;
            col.Width = 100;
            grdVehicles.Columns.Add(col);


            col = new GridViewTextBoxColumn();

            col.HeaderText = "Model";
            col.Name = COL_VEHICLES.MODEL;
            col.Width = 100;
            grdVehicles.Columns.Add(col);


            col = new GridViewTextBoxColumn();

            col.HeaderText = "Veh Log Book";
            col.Name = COL_VEHICLES.VEHLOGBOOK;
            col.Width = 100;
            grdVehicles.Columns.Add(col);



            colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "End On";
            colDate.Name = COL_VEHICLES.ENDON;
            colDate.Format = DateTimePickerFormat.Custom;
            colDate.CustomFormat = "dd/MM/yyyy";
            colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.Width = 110;
            col.ReadOnly = false;
            grdVehicles.Columns.Add(colDate);


            if (this.CanDelete)
            {

                UI.GridFunctions.AddDeleteColumn(grdVehicles);

                grdVehicles.Columns["ColDelete"].Width = 70;

                grdVehicles.RowsChanging += new GridViewCollectionChangingEventHandler(grdVehicles_RowsChanging);
            }
            else
                grdAvailability.AllowDeleteRow = false;



        }

        void grdVehicles_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdVehicles.CurrentRow != null && grdVehicles.CurrentRow is GridViewDataRowInfo)
                {

                    DeleteDriverVehicleHistoryRow(grdVehicles.CurrentRow.Cells[COL_VEHICLES.ID].Value.ToInt());
                }
            }
        }

        private void DeleteDriverVehicleHistoryRow(int Id)
        {
            try
            {
                Fleet_DriverAssignedVehicleBO objAssVehBO = new Fleet_DriverAssignedVehicleBO();
                objAssVehBO.GetByPrimaryKey(Id);
                if (objAssVehBO.Current != null)
                {

                    objAssVehBO.Delete(objAssVehBO.Current);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }




        public struct DRIVER_DOCUMENTS
        {
            public static int PCOVehicle = 1;
            public static int PCODriver = 2;
            public static int MOT = 3;
            public static int Insurance = 4;
            public static int MOT2 = 5;
            public static int LICENSE = 6;
            public static int ROADTAX = 7;


        }

        private void DisplayCompanyVehicle()
        {
            grdCompanyVehicles.Rows.Clear();

            GridViewRowInfo row = null;

            var list = (from a in General.GetQueryable<Fleet_Master>(null)
                        select new
                        {
                          Id =  a.Id,
                          VehicleNo =  a.VehicleNo,
                          VehicleType = a.Plateno + " - " + a.VehicleNo + " - " +  a.Fleet_VehicleType.VehicleType,
                          VehicleId = a.Id,
                        
                        }).ToList();

            foreach (var item in list)
            {
                row = grdCompanyVehicles.Rows.AddNew();

                  row.Cells[COL_CompanyVehicle.Vehicles].Value = item.VehicleType.ToString();
                  row.Cells[COL_CompanyVehicle.VehicleTypeId].Value = item.VehicleId.ToString();
                  row.Cells[COL_CompanyVehicle.ID].Value = 0;
                
            }

            int driverId = objMaster.Current.Id; 
            var rows1 = General.GetQueryable<Fleet_Driver_CompanyVehicle>(c => c.DriverId == driverId).ToList();
         
            for (int i = 0; i < grdCompanyVehicles.RowCount; i++)
            {
                foreach (var item in rows1)
                {
                    if(grdCompanyVehicles.Rows[i].Cells[COL_CompanyVehicle.VehicleTypeId].Value.ToInt()==item.FleetMasterId)
                    {
                        grdCompanyVehicles.Rows[i].Cells[COL_CompanyVehicle.Check].Value = true;
                        grdCompanyVehicles.Rows[i].Cells[COL_CompanyVehicle.ID].Value = item.ID;
                    }
                }
            }
        }


        private void OnNewDocuments()
        {
            grdDocuments.Rows.Clear();




            GridViewRowInfo row = null;

            foreach (var item in General.GetQueryable<Gen_Syspolicy_DriverDocumentList>(c => c.IsVisible == true))
            {
                row = grdDocuments.Rows.AddNew();

                row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = item.DocumentName.ToStr();
                row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = item.Id;

            }



            //row = grdDocuments.Rows.AddNew();
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = "PHC Vehicle";
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = 1;

            //row = grdDocuments.Rows.AddNew();
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = "PHC Driver";
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = 2;

            //row = grdDocuments.Rows.AddNew();
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = "MOT";
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = 3;




            //if (AppVars.objPolicyConfiguration.EnableBabySeats.ToBool())
            //{

            //row = grdDocuments.Rows.AddNew();
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = "MOT 2";
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = 5;
            //  }


            //row = grdDocuments.Rows.AddNew();
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = "Insurance";
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = 4;


            //row = grdDocuments.Rows.AddNew();
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = "License";
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = DRIVER_DOCUMENTS.LICENSE;



            //row = grdDocuments.Rows.AddNew();
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value = "Road Tax";
            //row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value = DRIVER_DOCUMENTS.ROADTAX;

        }



        void grdDocuments_CommandCellClick(object sender, EventArgs e)
        {
            string colName = grdDocuments.CurrentColumn.Name;
            GridViewRowInfo row = grdDocuments.CurrentRow;
            if (colName == "Clear")
            {
                ClearDocument(row);

            }
            else if (colName == "Load")
            {
                LoadDocument(row);

            }
            else if (colName == "View")
            {
                ViewDocument(row);

            }

        }

        private void ClearDocument(string path)
        {

            txtLogBookDocPath.Text = string.Empty;
            //  row.Cells[COL_DOCUMENT.EXPIRYDATE].Value = null;
        }


        private void ClearDocument(GridViewRowInfo row)
        {

            row.Cells[COL_DOCUMENT.FILENAME].Value = "";
            //  row.Cells[COL_DOCUMENT.EXPIRYDATE].Value = null;
        }

        OpenFileDialog openFileDialog1;
        private void LoadDocument(GridViewRowInfo row)
        {
            this.openFileDialog1.Filter = "Documents (All files (*.*)|*.*";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {

                row.Cells[COL_DOCUMENT.FILENAME].Value = openFileDialog1.SafeFileName;
                row.Cells[COL_DOCUMENT.FULLPATH].Value = openFileDialog1.FileName;
            }



        }

        private void LoadDocument(string path)
        {
            this.openFileDialog1.Filter = "Documents (All files (*.*)|*.*";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                txtLogBookDocPath.Text = openFileDialog1.FileName;


            }



        }


        private void ViewDocument(GridViewRowInfo row)
        {

            string fullDirectoryPath = "";
            DirectoryInfo di = null;


            try
            {

                if (row.Cells[COL_DOCUMENT.FULLPATH].Value.ToStr() != string.Empty)
                {
                    fullDirectoryPath = row.Cells[COL_DOCUMENT.FULLPATH].Value.ToStr();

                    if (File.Exists(fullDirectoryPath))
                    {

                        di = new DirectoryInfo(fullDirectoryPath);

                        System.Diagnostics.Process.Start(di.FullName);
                    }
                    else
                    {
                        if (fullDirectoryPath.ToStr().StartsWith("http:"))
                        {

                            System.Diagnostics.Process.Start(fullDirectoryPath.ToStr().Trim());
                        }
                        else
                        {


                            throw new Exception("File not found");
                        }

                    }


                }
                else
                {
                    ENUtils.ShowMessage("Please select a Document");
                    return;

                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void ViewDocument(string path)
        {

            string fullDirectoryPath = "";
            DirectoryInfo di = null;


            try
            {

                if (path.ToStr() != string.Empty)
                {
                    fullDirectoryPath = path.ToStr();

                    if (File.Exists(fullDirectoryPath))
                    {

                        di = new DirectoryInfo(fullDirectoryPath);

                        System.Diagnostics.Process.Start(di.FullName);
                    }
                    else
                    {
                        if (fullDirectoryPath.ToStr().StartsWith("http:"))
                        {

                            System.Diagnostics.Process.Start(fullDirectoryPath.ToStr().Trim());
                        }
                        else
                        {


                            throw new Exception("File not found");
                        }

                    }


                }
                else
                {
                    ENUtils.ShowMessage("Please select a Document");
                    return;

                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        string sharedPath = string.Empty;
        string fullDirectoryPath = string.Empty;
        private void UploadAll()
        {
           // bool rtn = true;
         
            if (grdDocuments.Rows.Count(c => c.Cells[COL_DOCUMENT.FILENAME].Value.ToStr().Trim().Length > 0) > 0)
            {

                string errors = "";

                if (string.IsNullOrEmpty(sharedPath))
                    errors += Environment.NewLine + "Shared Network Path is not defined in System Policy";
                else
                {
                    try
                    {
                        Directory.Exists(sharedPath);
                    }
                    catch (Exception ex)
                    {

                        ENUtils.ShowMessage(ex.Message);
                        return ;
                    }

                }

                if (!string.IsNullOrEmpty(errors))
                {
                    ENUtils.ShowMessage(errors);
                    return;
                }


            }

            string fileName = "";
            string fullPath = "";


            DirectoryInfo di = null;

            try
            {

                foreach (var item in grdDocuments.Rows.Where(c => c.Cells[COL_DOCUMENT.FILENAME].Value.ToStr().Trim().Length > 0))
                {

                    fileName = item.Cells[COL_DOCUMENT.FILENAME].Value.ToStr();
                    fullPath = item.Cells[COL_DOCUMENT.FULLPATH].Value.ToStr();


                    try
                    {



                        if (!Directory.Exists(fullDirectoryPath))
                        {
                            di = Directory.CreateDirectory(fullDirectoryPath);
                        }
                        else
                        {
                            di = new DirectoryInfo(fullDirectoryPath);
                        }


                        string filePath = di.FullName + "\\" + fileName;
                        if (!File.Exists(filePath))
                        {

                            File.Copy(fullPath, filePath);

                        }



                    }
                    catch (Exception ex)
                    {

                        ENUtils.ShowMessage(ex.Message);
                        break;
                    }

                }


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        void frmDriver_Load(object sender, EventArgs e)
        {
            try
            {
                chkEndDrier.Checked = false;
                dtpEndingDate.Enabled = false;

                radpageview1.SelectedPage = radpageview1.Pages[0];

                SetUserRights();

                pic_Driver.ShowActionPanel = true;
                //if (grdDriverNotes.RowCount > 0)
                //{
                //    (grdDriverNotes.Columns["AddOn"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                //}
            }
            catch
            {


            }
        }

        private void SetUserRights()
        {
            if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "EDIT") == 0)
            {
                if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "PARTIAL EDIT") > 0)
                {
                    btnAssignedNew.Enabled = false;


                    txtDriverNo.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    txtMobileNo.ReadOnly = true;
                    txtNI.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    txtWebLoginPwd.ReadOnly = true;
                    txtTelephoneNo.ReadOnly = true;
                    txtVehMake.ReadOnly = true;
                    txtVehModel.ReadOnly = true;
                    txtVehNo.ReadOnly = true;
                    txtVehOwner.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    txtDriverName.ReadOnly = true;


                    chkActiveDriver.Enabled = false;
                    chkHasRentPaid.Enabled = false;

                    btnAddAvailability.Enabled = false;

                    grdDocuments.Enabled = false;
                    grdAvailability.Enabled = false;

                    ddlDriverType.Enabled = false;
                    ddlSubCompany.Enabled = false;
                    ddlVehicleColor.Enabled = false;
                    ddlVehicleType.Enabled = false;

                    dtpDOB.Enabled = false;

                    btnSaveChanges.Visible = true;
                    btnSaveChanges.BringToFront();

                    this.ShowSaveAndCloseButton = false;

                }

            }
            else
            {
                if (objMaster.PrimaryKeyValue != null && this.CanEdit)
                    btnAssignedNew.Enabled = true;


            }



            if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "SHOW PDA SETTINGS TAB") == 0 || objMaster.PrimaryKeyValue == null)
            {
                pg_pdasettings.Item.Visibility = ElementVisibility.Collapsed;

            }




        }


        #region Overridden Methods


        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {

            //ddlVehicle.SelectedValue = null;
            OnNewDocuments();
            chkActiveDriver.Checked = true;
            pic_Driver.Clear();

            ClearAvailability();
            grdAvailability.Rows.Clear();

            txtDriverNo.Focus();
            ClearDriverVehicleHistory();

            ClearCommissionGrid();

            Pg_notes.Item.Visibility = ElementVisibility.Collapsed;
            numInitialBalance.Enabled = true;
            numInitialBalance.Value = 0.00m;
            numMaxCommission.Value = 0.00m;


            numTrainingCompletedHours.Value = 0.00m;
            numTrainingReqHours.Value = 0.00m;
        }


        private void AskInActive()
        {

            if (DialogResult.Yes == RadMessageBox.Show("Do you want to InActive this Driver ?", "Driver Availability", MessageBoxButtons.YesNo))
            {
                chkActiveDriver.ToggleStateChanging -= new StateChangingEventHandler(chkActiveDriver_ToggleStateChanging);
                chkActiveDriver.Checked = false;
                chkActiveDriver.ToggleStateChanging += new StateChangingEventHandler(chkActiveDriver_ToggleStateChanging);

            }

        }



        long oldPhotoLength = 0;
        long newPhotoLength = 0;

        private bool IsEditMode = false;

        public override void Save()
        {

            try
            {
                if (this.OpenedFromInActiveList == false)
                {

                    if (grdAvailability.Rows.Count == 1 && grdAvailability.Rows[0].Cells[COL_AVAILABILITY.ENDINGDATE].Value != null)
                    {
                        AskInActive();

                    }
                    else if (grdAvailability.Rows.Count > 1)
                    {


                        GridViewRowInfo lastRow = grdAvailability.Rows.LastOrDefault();


                        if (lastRow != null && lastRow.Cells[COL_AVAILABILITY.ENDINGDATE].Value != null)
                        {


                            AskInActive();
                        }

                    }
                }


                string oldDriverNo = string.Empty;
                string oldPassword = string.Empty;


                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();

                }
                else
                {
                    objMaster.Edit();
                    IsEditMode = true;
                }

                // oldPDALoginBlocked = objMaster.Current.PDALoginBlocked.ToBool();
                oldDriverNo = objMaster.Current.DriverNo.ToStr().Trim();
                oldPassword = objMaster.Current.LoginPassword.ToStr().Trim();



                objMaster.Current.DriverNo = txtDriverNo.Text.Trim();

                objMaster.Current.LoginId = txtWebLoginPwd.Text.Trim();

                objMaster.Current.LoginPassword = txtPassword.Text.Trim();
                objMaster.Current.PDALoginBlocked = chkHasRentPaid.Checked.ToBool();


                objMaster.Current.IsActive = chkActiveDriver.Checked;
                objMaster.Current.DriverName = txtDriverName.Text.Trim();
                objMaster.Current.Surname = txtSurName.Text.Trim();
                objMaster.Current.Email = txtEmail.Text.Trim();
                objMaster.Current.TelephoneNo = txtTelephoneNo.Text.Trim();
                objMaster.Current.MobileNo = txtMobileNo.Text.Trim();
                //    objMaster.Current.PDAMobileNo = txtWebLoginPwd.Text.Trim();

                objMaster.Current.DateOfBirth = dtpDOB.Value.ToDateorNull();
                objMaster.Current.Address = txtAddress.Text.Trim();
                objMaster.Current.Nationality = txtVehicleLogBookNo.Text.Trim();
                objMaster.Current.Gender = txtLogBookDocPath.Text.Trim();

                objMaster.Current.VehicleNo = txtVehNo.Text.Trim();
                objMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();


                if (objMaster.Current.VehicleTypeId == null)
                    objMaster.Current.VehicleTypeId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;


                objMaster.Current.VehicleColor = ddlVehicleColor.Text.ToStr().Trim();
                objMaster.Current.VehicleMake = txtVehMake.Text.Trim();
                objMaster.Current.VehicleModel = txtVehModel.Text.Trim();
                objMaster.Current.VehicleOwner = txtVehOwner.Text.Trim();


                objMaster.Current.InitialBalance = numInitialBalance.Value.ToDecimal();
                objMaster.Current.RentLimit = numRentLimit.Value.ToDecimal();

                objMaster.Current.NI = txtNI.Text.ToStr().Trim();
                objMaster.Current.DriverTypeId = ddlDriverType.SelectedValue.ToIntorNull();

                objMaster.Current.HasPDA = chkHasPDA.Checked.ToBool();
                objMaster.Current.EnableBidding = chkBidding.Checked.ToBool();

                objMaster.Current.PDARent = numPDARent.Value.ToDecimal();

                objMaster.Current.City = numTrainingReqHours.Value.ToStr();
                objMaster.Current.PostCode = numTrainingCompletedHours.Value.ToStr();

                objMaster.Current.SubcompanyId = ddlSubCompany.SelectedValue.ToIntorNull();
                if (groupOtherCharges.Visible)
                {
                    objMaster.Current.UseCompanyVehicle = chkUseCompanyVehicle.Checked;
                    if (chkUseCompanyVehicle.Checked)
                    {
                        objMaster.Current.CarRent = numCarRent.Value.ToDecimal();
                        objMaster.Current.CarInsuranceRent = numCarInsuranceRent.Value.ToDecimal();
                    }
                    else
                    {
                        objMaster.Current.CarRent = 0;
                        objMaster.Current.CarInsuranceRent = 0;
                    }


                 
                    if (General.GetQueryable<Gen_MainCompany>(c => c.CompanyName == ddlSubCompany.Text).Count() > 0)
                    {
                        objMaster.Current.PrimeCompanyRent = 0;
                        objMaster.Current.IsPrimeCompanyDriver = false;

                       
                    }
                    else
                    {
                        objMaster.Current.PrimeCompanyRent = numPrimeCompanyRent.Value.ToDecimal();
                        objMaster.Current.IsPrimeCompanyDriver = true;
                    }
                }
                else
                {
                    objMaster.Current.UseCompanyVehicle = false;
                    objMaster.Current.CarRent = 0;
                    objMaster.Current.CarInsuranceRent = 0;
                    objMaster.Current.PrimeCompanyRent = 0;
                    objMaster.Current.IsPrimeCompanyDriver = false;
                }

                if (objMaster.Current.SubcompanyId == null)
                    objMaster.Current.SubcompanyId = AppVars.objSubCompany.Id;



                objMaster.Current.LicenseExpiryDate = dtpVehAssignedOn.Value.ToDateorNull();

                //MaxCommission
                objMaster.Current.MaxCommission = numMaxCommission.Value.ToDecimal();
                //AfterDriverNo = objMaster.Current.DriverNo;
                //AfterMaxCommission = objMaster.Current.MaxCommission.ToDecimal();

                //

                //sajid
                int? driverTypeId = ddlDriverType.SelectedValue.ToIntorNull();
                if (driverTypeId != null)
                {
                    if (driverTypeId.Equals(1))
                    {
                        objMaster.Current.DriverMonthlyRent = numDriverRentComm.Value.ToDecimal();
                    }
                    else if (driverTypeId.Equals(2))
                    {
                        objMaster.Current.DriverCommissionPerBooking = numDriverRentComm.Value.ToDecimal();

                    }
                }


                sharedPath = General.GetSharedNetworkPath();
                fullDirectoryPath = sharedPath + "\\Taxi\\Driver" + objMaster.Current.DriverNo;





                if (objMaster.Current.Fleet_Driver_Images.Count == 0)
                {
                    objMaster.Current.Fleet_Driver_Images.Add(new Fleet_Driver_Image());
                }


                if(objMaster.Current.Fleet_Driver_Images[0].Photo!=null)
                     oldPhotoLength = objMaster.Current.Fleet_Driver_Images[0].Photo.Length;
          
                objMaster.Current.Fleet_Driver_Images[0].Photo = pic_Driver.GetByteArrayOfImage();
                if (objMaster.Current.Fleet_Driver_Images[0].Photo != null)
                       newPhotoLength = objMaster.Current.Fleet_Driver_Images[0].Photo.Length;


                if (newPhotoLength == 0 && objMaster.Current.Id > 0)
                {
                    objMaster.Current.Fleet_Driver_Images[0].PhotoLinkId = null;
                }

                if (objMaster.Current.Fleet_Driver_DeviceInfos == null)
                    objMaster.Current.Fleet_Driver_DeviceInfos = new Fleet_Driver_DeviceInfo();


                objMaster.Current.Fleet_Driver_DeviceInfos.IMEINumber = txt_SIM_IMEI.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.NetworkAPN = txt_SIM_NetworkAPN.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.SIMNetworkName = txt_SIM_NetworkName.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.SIMNumber = txt_SIM_Number.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.Comments = txt_SIM_Comments.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.DataAllowance = txt_SIM_DataAllowance.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.DeviceDeposits = txt_SIM_PDADeposits.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.DeviceMake = txt_SIM_Make.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.DeviceModel = txt_SIM_Model.Text.Trim();
                objMaster.Current.Fleet_Driver_DeviceInfos.DeviceDateGiven = dtp_SIM_PDADateGiven.Value.ToDateorNull();


                foreach (var row in grdDocuments.Rows)
                {
                    if (row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt() == DRIVER_DOCUMENTS.Insurance)
                    {
                        objMaster.Current.InsuranceExpiryDate = row.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateTimeorNull();

                    }
                    else if (row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt() == DRIVER_DOCUMENTS.PCODriver)
                    {
                        objMaster.Current.PCODriverExpiryDate = row.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateorNull();

                    }
                    else if (row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt() == DRIVER_DOCUMENTS.MOT)
                    {
                        objMaster.Current.MOTExpiryDate = row.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateorNull();

                    }
                    else if (row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt() == DRIVER_DOCUMENTS.PCOVehicle)
                    {
                        objMaster.Current.PCOVehicleExpiryDate = row.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateorNull();

                    }


                    else if (row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt() == DRIVER_DOCUMENTS.MOT2)
                    {
                        objMaster.Current.MOT2ExpiryDate = row.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateorNull();

                    }


                    else if (row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt() == DRIVER_DOCUMENTS.LICENSE)
                    {
                        objMaster.Current.DrivingLicenseExpiryDate = row.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateorNull();

                    }

                    else if (row.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt() == DRIVER_DOCUMENTS.ROADTAX)
                    {
                        objMaster.Current.RoadTaxiExpiryDate = row.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateorNull();

                    }


                }




                string[] skipProperties = { "Fleet_Driver", "Driver_Shift", "Fleet_VehicleType", "Fleet_Driver_Note", "Gen_DebitCreditNote", "Fleet_DriverVehicleDetail", "Fleet_Master" };
                IList<Fleet_Driver_Availability> savedList2 = objMaster.Current.Fleet_Driver_Availabilities;
                List<Fleet_Driver_Availability> listofDetail2 = (from r in grdAvailability.Rows

                                                                 select new Fleet_Driver_Availability
                                                                 {
                                                                     Id = r.Cells[COL_AVAILABILITY.ID].Value.ToInt(),
                                                                     DriverId = r.Cells[COL_AVAILABILITY.MASTERID].Value.ToInt(),
                                                                     AvailableDate = r.Cells[COL_AVAILABILITY.BECAMEAVAIL].Value.ToDateorNull(),
                                                                     EndingDate = r.Cells[COL_AVAILABILITY.ENDINGDATE].Value.ToDateorNull(),

                                                                 }).ToList();


                Utils.General.SyncChildCollection(ref savedList2, ref listofDetail2, "Id", skipProperties);



                // Save Range Commission
                if (grdRangeWiseComm != null && grdRangeWiseComm.Columns.Count > 0)
                {

                    List<Fleet_Driver_CommissionRange> listofRangeWiseComm = (from a in grdRangeWiseComm.Rows
                                                                              select new Fleet_Driver_CommissionRange
                                                                              {
                                                                                  Id = a.Cells["ID"].Value.ToInt(),
                                                                                  DriverId = objMaster.Current.Id,
                                                                                  FromPrice = a.Cells["FROMPRICE"].Value.ToDecimal(),
                                                                                  ToPrice = a.Cells["TOPRICE"].Value.ToDecimal(),
                                                                                  CommissionValue = a.Cells["COMMISSIONPERCENT"].Value.ToDecimal(),
                                                                              }).ToList();
                    IList<Fleet_Driver_CommissionRange> savedListRange = objMaster.Current.Fleet_Driver_CommissionRanges;
                    Utils.General.SyncChildCollection(ref savedListRange, ref listofRangeWiseComm, "Id", skipProperties);
                }


                //if (radPanel2 != null)
                //{

                //    IList<Fleet_Driver_Shift> savedList3 = objMaster.Current.Fleet_Driver_Shifts;
                //    List<Fleet_Driver_Shift> listofDetail3 = (from r in grdShifts.Rows

                //                                              select new Fleet_Driver_Shift
                //                                              {
                //                                                  Id = r.Cells[COL_SHIFT.ID].Value.ToInt(),
                //                                                  DriverId = r.Cells[COL_SHIFT.MASTERID].Value.ToInt(),
                //                                                  Driver_Shift_ID = r.Cells[COL_SHIFT.SHIFT_ID].Value.ToInt(),
                //                                                  FromTime = r.Cells[COL_SHIFT.FROMTIME].Value.ToDateTime(),
                //                                                  ToTime = r.Cells[COL_SHIFT.TOTIME].Value.ToDateTime(),

                //                                              }).ToList();


                //    Utils.General.SyncChildCollection(ref savedList3, ref listofDetail3, "Id", skipProperties);
                //}


                IList<Fleet_Driver_Document> savedList = objMaster.Current.Fleet_Driver_Documents;
                List<Fleet_Driver_Document> listofDetail = (from r in grdDocuments.Rows
                                                            //where r.Cells[COL_DOCUMENT.FILENAME].Value.ToStr()!=string.Empty
                                                            select new Fleet_Driver_Document
                                                            {
                                                                Id = r.Cells[COL_DOCUMENT.ID].Value.ToLong(),
                                                                DriverId = r.Cells[COL_DOCUMENT.MASTERID].Value.ToInt(),
                                                                DocumentId = r.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToIntorNull(),
                                                                DocumentName = r.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value.ToStr(),
                                                                ExpiryDate = r.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateorNull(),
                                                                FileName = r.Cells[COL_DOCUMENT.FILENAME].Value.ToStr(),
                                                                FilePath = fullDirectoryPath + "\\" + r.Cells[COL_DOCUMENT.FILENAME].Value.ToStr(),
                                                                BadgeNumber = r.Cells[COL_DOCUMENT.BADGENUMBER].Value.ToStr(),
                                                            }).ToList();

                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);



                IList<Fleet_Driver_Note> savedList4 = objMaster.Current.Fleet_Driver_Notes;
                List<Fleet_Driver_Note> listofNotes = (from a in grdLister.Rows
                                                       select new Fleet_Driver_Note
                                                       {
                                                           Id = a.Cells[COL_NOTES.Id].Value.ToInt(),
                                                           Notes = a.Cells[COL_NOTES.Notes].Value.ToStr(),
                                                           AddOnTime = a.Cells[COL_NOTES.Time].Value.ToDateTimeorNull(),
                                                           AddBy = a.Cells[COL_NOTES.AddBy].Value.ToStr(),
                                                           AddOn = a.Cells[COL_NOTES.AddOn].Value.ToDate(),
                                                           DriverId = a.Cells[COL_NOTES.DriverId].Value.ToInt(),

                                                       }).ToList();

                Utils.General.SyncChildCollection(ref savedList4, ref listofNotes, "Id", skipProperties);


             
                IList<Fleet_Driver_DebitCreditNote> savedList5 = objMaster.Current.Fleet_Driver_DebitCreditNotes;
                List<Fleet_Driver_DebitCreditNote> listofDebitCreditNotes = (from a in grdDebitCreditNotes.Rows
                                                                             select new Fleet_Driver_DebitCreditNote
                                                                  {
                                                                      Id = a.Cells[COL_DebitCreditNotes.Id].Value.ToInt(),
                                                                      DriverId = a.Cells[COL_DebitCreditNotes.DriverId].Value.ToInt(),
                                                                      DCNoteId = a.Cells[COL_DebitCreditNotes.DCNoteId].Value.ToInt(),
                                                                      Charges = a.Cells[COL_DebitCreditNotes.Charges].Value.ToDecimal(),
                                                                      IsWeekly = a.Cells[COL_DebitCreditNotes.IsWeekly].Value.ToBool()
                                                                  }).ToList();

                Utils.General.SyncChildCollection(ref savedList5, ref listofDebitCreditNotes, "Id", skipProperties);




              
                    IList<Fleet_Driver_CompanyVehicle> savedList6 = objMaster.Current.Fleet_Driver_CompanyVehicles;
                    List<Fleet_Driver_CompanyVehicle> listofDriverVehicleDetail = (from a in grdCompanyVehicles.Rows
                                                                                   where a.Cells[COL_CompanyVehicle.Check].Value.ToBool() == true// && a.Cells[COL_CompanyVehicle.ID].Value.ToInt() != 0
                                                                                   select new Fleet_Driver_CompanyVehicle
                                                                                 {
                                                                                     ID = a.Cells[COL_CompanyVehicle.ID].Value.ToInt(),
                                                                                     DriverId = objMaster.Current.Id,
                                                                                     FleetMasterId = a.Cells[COL_CompanyVehicle.VehicleTypeId].Value.ToIntorNull()

                                                                                 }).ToList();

                    Utils.General.SyncChildCollection(ref savedList6, ref listofDriverVehicleDetail, "ID", skipProperties);


                //

                IList<Fleet_Driver_Shift> savedList7 = objMaster.Current.Fleet_Driver_Shifts;
                List<Fleet_Driver_Shift> listofDriverShift = (from a in grdDriverShifts.Rows
                                                                               where a.Cells["Check"].Value.ToBool() == true// && a.Cells[COL_CompanyVehicle.ID].Value.ToInt() != 0
                                                                               select new Fleet_Driver_Shift
                                                                               {
                                                                                   Id = a.Cells[COL_SHIFT.ID].Value.ToInt(),
                                                                                   DriverId = objMaster.Current.Id,
                                                                                   Driver_Shift_ID= a.Cells[COL_SHIFT.SHIFT_ID].Value.ToIntorNull(),
                                                                                   //FleetMasterId = a.Cells[COL_CompanyVehicle.VehicleTypeId].Value.ToIntorNull()

                                                                               }).ToList();
                
                Utils.General.SyncChildCollection(ref savedList7, ref listofDriverShift, "Id", skipProperties);
                //






                UploadAll();


               


                if (grdDriverAttributes != null)
                {

                    objMaster.Current.AttributeValues = string.Join(",", grdDriverAttributes.Rows.Where(c => c.Cells[Col_DriverAttributes.Default].Value.ToBool() == true).Select(c => c.Cells[Col_DriverAttributes.ShortName].Value.ToStr()).ToArray<string>());
                }

                if (objMaster.Current.AttributeValues.ToStr().Trim().Length > 0)
                {

                    objMaster.Current.AttributeValues = "," + objMaster.Current.AttributeValues + ",";
                }

              //  objMaster.Current.Fleet_DriverVehicleDetails = string.Join(",", grdCompanyVehicles.Rows.Where(c => c.Cells[COL_CompanyVehicle.Check].Value.ToBool() == true).Select(c => c.Cells[COL_CompanyVehicle.Vehicles].Value.ToStr()).ToArray<string>());

                objMaster.Current.DriverCategory = ddlCategory.SelectedValue.ToInt();


              

             

                objMaster.Save();


                if (IsEditMode)
                {
                    SaveDriverLog();

                }

                General.RefreshListWithoutSelectedOnRefreshData<frmDriversList>("frmDriversList1");
                General.RefreshListWithoutSelected<frmInActiveDriversList>("frmInActiveDriversList1");


                

                    if (newPhotoLength>0 && AppVars.listofSMSTags.Count(c => c.TagMemberValue == "<DrvImage>") > 0 && objMaster.Current.Fleet_Driver_Images.Count > 0 && objMaster.Current.Fleet_Driver_Images[0].Photo != null)
                        UploadImage(objMaster.Current.Id, objMaster.Current.DriverNo, objMaster.Current.Fleet_Driver_Images[0].Photo, AppVars.objPolicyConfiguration.DefaultClientId.ToStr().Replace("/", "").Trim().Replace("*", "").Replace("_", "").Trim() + "_" + objMaster.Current.DriverNo, objMaster.Current.Fleet_Driver_Images[0].PhotoLinkId.ToStr());
                
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

        private void UploadImage(int driverId,string driverNo, byte[] arrImage,string fileName,string smsLinkId)
        {
            
            try
            {
               


                if (smsLinkId.Length == 0 || (oldPhotoLength>0 && oldPhotoLength!=newPhotoLength))
                    smsLinkId = null;

                 WebUploadFile obje = new WebUploadFile();

                string response = obje.UploadFileGetPhotoLinkId(arrImage, fileName, smsLinkId, driverNo, AppVars.objPolicyConfiguration.DefaultClientId.ToStr());




                if (response.IsNumeric())
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        db.ExecuteCommand("update Fleet_Driver_Image set PhotoLinkId=" + response.ToInt() + " where driverId=" + driverId);
                    }
                }
                else
                {
                    ENUtils.ShowMessage(response);
                }


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }         
        }
        List<Fleet_Driver_CommissionRange> CommissionRangeList = null;

        Fleet_Driver DriverList = null;

        private void SaveDriverLog()
        {
            try
            {
                // Driver Log
                string Before = string.Empty;
                string After = string.Empty;
                DateTime dtNow = DateTime.Now;
                List<Fleet_Driver_Log> objLog = new List<Fleet_Driver_Log>();
                if (grdRangeWiseComm != null && grdRangeWiseComm.Rows.Count > 0)
                {
                    var listofRangeWiseComm = General.GetQueryable<Fleet_Driver_CommissionRange>(c => c.DriverId == objMaster.Current.Id).ToList();

                    if (listofRangeWiseComm.Count > 0 && (CommissionRangeList != null && listofRangeWiseComm.Count >= CommissionRangeList.Count))
                    {

                        var distinct = listofRangeWiseComm.Where(x => !CommissionRangeList.Select(y => y.FromPrice).Contains(x.FromPrice) || !CommissionRangeList.Select(y => y.ToPrice).Contains(x.ToPrice) || !CommissionRangeList.Select(y => y.CommissionValue).Contains(x.CommissionValue)).ToList();

                        if (distinct != null)
                        {
                            foreach (var item in distinct)
                            {
                                var objOld = CommissionRangeList.FirstOrDefault(c => c.Id == item.Id);
                                if (objOld == null)
                                {
                                    continue;
                                }
                                Before = "From:" + objOld.FromPrice + " Till:" + objOld.ToPrice + " Commission%:" + objOld.CommissionValue + "%";
                                After = "From:" + item.FromPrice + " Till:" + item.ToPrice + " Commission%:" + item.CommissionValue + "%";
                                objLog.Add(new Fleet_Driver_Log { DriverId = objMaster.Current.Id, User = AppVars.LoginObj.LoginName, BeforeUpdate = Before, AfterUpdate = After, UpdateDate = dtNow, ActionType = "2" });
                            }
                        }

                    }
                }


                var DList = (from a in grdDocuments.Rows
                             select new
                             {
                                 DocumentName = a.Cells[COL_DOCUMENT.DOCUMENTTITLE].Value.ToStr(),
                                 ExpiryDate = a.Cells[COL_DOCUMENT.EXPIRYDATE].Value.ToDateTimeorNull(),
                                 DOCUMENTTITLEID = a.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt()
                             }).ToList();
                foreach (var item in DList)
                {
                    if (item.ExpiryDate == null)
                    {
                        continue;
                    }
                    Before = string.Empty;
                    After = string.Empty;
                    if (item.DOCUMENTTITLEID == DRIVER_DOCUMENTS.Insurance)
                    {

                        if (objMaster.Current.InsuranceExpiryDate != null && objMaster.Current.InsuranceExpiryDate != DriverList.InsuranceExpiryDate)
                        {
                            Before = "Insurance: " + DriverList.InsuranceExpiryDate;
                            After = "Insurance: " + item.ExpiryDate;
                        }
                    }
                    else if (item.DOCUMENTTITLEID == DRIVER_DOCUMENTS.PCOVehicle)
                    {

                        if (objMaster.Current.PCOVehicleExpiryDate != null && objMaster.Current.PCOVehicleExpiryDate != DriverList.PCOVehicleExpiryDate)
                        {
                            Before = "PCO Vehicle: " + DriverList.PCOVehicleExpiryDate;
                            After = "PCO Vehicle: " + item.ExpiryDate.ToDateTimeorNull();
                        }
                    }
                    else if (item.DOCUMENTTITLEID == DRIVER_DOCUMENTS.PCODriver)
                    {

                        if (objMaster.Current.PCODriverExpiryDate != null && objMaster.Current.PCODriverExpiryDate != DriverList.PCODriverExpiryDate)
                        {
                            Before = "PCO DRIVER: " + DriverList.PCODriverExpiryDate;
                            After = "PCO DRIVER: " + item.ExpiryDate.ToDateTimeorNull();
                        }
                    }
                    else if (item.DOCUMENTTITLEID == DRIVER_DOCUMENTS.LICENSE)
                    {

                        if (objMaster.Current.DrivingLicenseExpiryDate != null && objMaster.Current.DrivingLicenseExpiryDate != DriverList.DrivingLicenseExpiryDate)
                        {
                            Before = "License: " + DriverList.LicenseExpiryDate;
                            After = "License: " + item.ExpiryDate.ToDateTimeorNull();
                        }
                    }
                    else if (item.DOCUMENTTITLEID == DRIVER_DOCUMENTS.MOT)
                    {

                        if (objMaster.Current.MOTExpiryDate != null && objMaster.Current.MOTExpiryDate != DriverList.MOTExpiryDate)
                        {
                            Before = "MOT: " + DriverList.MOTExpiryDate;
                            After = "MOT: " + item.ExpiryDate.ToDateTimeorNull();
                        }
                    }
                    else if (item.DOCUMENTTITLEID == DRIVER_DOCUMENTS.MOT2)
                    {

                        if (objMaster.Current.MOT2ExpiryDate != null && objMaster.Current.MOT2ExpiryDate != DriverList.MOT2ExpiryDate)
                        {
                            Before = "MOT2: " + DriverList.MOT2ExpiryDate;
                            After = "MOT2: " + item.ExpiryDate.ToDateTimeorNull();
                        }
                    }
                    else if (item.DOCUMENTTITLEID == DRIVER_DOCUMENTS.ROADTAX)
                    {

                        if (objMaster.Current.RoadTaxiExpiryDate != null && objMaster.Current.RoadTaxiExpiryDate != DriverList.RoadTaxiExpiryDate)
                        {
                            Before = "ROAD TAX: " + DriverList.RoadTaxiExpiryDate;
                            After = "ROAD TAX: " + item.ExpiryDate.ToDateTimeorNull();
                        }
                    }

                    if (!string.IsNullOrEmpty(Before) && !string.IsNullOrEmpty(After))
                    {
                        objLog.Add(new Fleet_Driver_Log { DriverId = objMaster.Current.Id, User = AppVars.LoginObj.LoginName, BeforeUpdate = Before, AfterUpdate = After, UpdateDate = dtNow, ActionType = "1" });
                    }
                }

                if (DriverList.DriverNo != objMaster.Current.DriverNo)
                {
                    Before = "DriverNo: " + DriverList.DriverNo + " ";
                    After = "DriverNo: " + objMaster.Current.DriverNo + " ";
                    objLog.Add(new Fleet_Driver_Log { DriverId = objMaster.Current.Id, User = AppVars.LoginObj.LoginName, BeforeUpdate = Before, AfterUpdate = After, UpdateDate = dtNow, ActionType = "1" });
                }
                if (DriverList.MaxCommission != objMaster.Current.MaxCommission)
                {
                    Before = "Max Commission: " + DriverList.MaxCommission;
                    string AfterMax = string.Format("{0:0.00}", objMaster.Current.MaxCommission);
                    After = "Max Commission: " + AfterMax;
                    objLog.Add(new Fleet_Driver_Log { DriverId = objMaster.Current.Id, User = AppVars.LoginObj.LoginName, BeforeUpdate = Before, AfterUpdate = After, UpdateDate = dtNow, ActionType = "1" });
                }
                if (DriverList.VehicleTypeId != objMaster.Current.VehicleTypeId)
                {
                    Before = "VehicleType: " + DriverList.VehicleNo;
                    After = "VehicleType: " + objMaster.Current.VehicleTypeId != null ? objMaster.Current.Fleet_VehicleType.VehicleType + " " : "";
                    objLog.Add(new Fleet_Driver_Log { DriverId = objMaster.Current.Id, User = AppVars.LoginObj.LoginName, BeforeUpdate = Before, AfterUpdate = After, UpdateDate = dtNow, ActionType = "1" });
                }


                if (objLog.Count > 0)
                {
                    foreach (var item in objLog)
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            Fleet_Driver_Log objDriver_Log = new Fleet_Driver_Log();
                            objDriver_Log.DriverId = item.DriverId;
                            objDriver_Log.User = AppVars.LoginObj.LoginName;
                            objDriver_Log.BeforeUpdate = item.BeforeUpdate;
                            objDriver_Log.AfterUpdate = item.AfterUpdate;
                            objDriver_Log.UpdateDate = dtNow;
                            objDriver_Log.ActionType = item.ActionType;
                            db.Fleet_Driver_Logs.InsertOnSubmit(objDriver_Log);
                            db.SubmitChanges();
                        }
                    }
                }
                //
            }
            catch
            {


            }
        }


        public override void DisplayRecord()
        {

            try
            {

                if (objMaster.Current == null) return;

                radpageview1.Pages.Insert(8,Pg_Rating);
                
                //Pg_Rating.Item.Visibility = ElementVisibility.Visible;
                txtRating.Visible = true;
                label1.Visible = true;
                label3.Visible = true;

                btnPrintDriver.Enabled = true;
                this.ShowSaveAndNewButton = false;

                chkActiveDriver.ToggleStateChanging -= new StateChangingEventHandler(chkActiveDriver_ToggleStateChanging);
                chkActiveDriver.Checked = objMaster.Current.IsActive.ToBool();
                chkActiveDriver.ToggleStateChanging += new StateChangingEventHandler(chkActiveDriver_ToggleStateChanging);

                txtDriverNo.Text = objMaster.Current.DriverNo;
                txtPassword.Text = objMaster.Current.LoginPassword.ToStr().Trim();

                txtWebLoginPwd.Text = objMaster.Current.LoginId.ToStr();


                chkHasRentPaid.Checked = objMaster.Current.PDALoginBlocked.ToBool();

                txtDriverName.Text = objMaster.Current.DriverName;
                txtSurName.Text = objMaster.Current.Surname.ToStr().Trim();
                txtEmail.Text = objMaster.Current.Email.ToStr();
                dtpDOB.Value = objMaster.Current.DateOfBirth;
                txtTelephoneNo.Text = objMaster.Current.TelephoneNo.ToStr();
                txtMobileNo.Text = objMaster.Current.MobileNo.ToStr();
                txtVehicleLogBookNo.Text = objMaster.Current.Nationality.ToStr().Trim();
                txtLogBookDocPath.Text = objMaster.Current.Gender.ToStr();

                txtAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtAddress.Text = objMaster.Current.Address.ToStr();
                txtAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                ddlVehicleType.SelectedValue = objMaster.Current.VehicleTypeId;
                txtVehNo.Text = objMaster.Current.VehicleNo;

                numInitialBalance.Value = objMaster.Current.InitialBalance.ToDecimal();
                numRentLimit.Value = objMaster.Current.RentLimit.ToDecimal();

                txtVehOwner.Text = objMaster.Current.VehicleOwner.ToStr();
                txtVehModel.Text = objMaster.Current.VehicleModel.ToStr();
                txtVehMake.Text = objMaster.Current.VehicleMake.ToStr();
                ddlVehicleColor.SelectedText = objMaster.Current.VehicleColor.ToStr();
                ddlSubCompany.SelectedValue = objMaster.Current.SubcompanyId;


                txtNI.Text = objMaster.Current.NI.ToStr().Trim();

                ddlDriverType.SelectedValue = objMaster.Current.DriverTypeId;
              
                ddlCategory.SelectedValue = objMaster.Current.DriverCategory.ToInt();

                chkHasPDA.Checked = objMaster.Current.HasPDA.ToBool();
                chkBidding.Checked = objMaster.Current.EnableBidding.ToBool();

                
                if (objMaster.Current.Fleet_Driver_PDASettings.Count() > 0 && objMaster.Current.Fleet_Driver_PDASettings.FirstOrDefault().DefaultIfEmpty().HasCompanyCars == true)
                {
                    if (objMaster.Current.Fleet_VehicleType.Fleet_Masters.Where(c => c.VehicleTypeId == objMaster.Current.VehicleTypeId).Count() > 0)
                    {
                        CompanyVehicle.Item.Visibility = ElementVisibility.Visible;
                       // radpageview1.Pages.Add(CompanyVehicle);
                    }
                    else
                    {
                        CompanyVehicle.Item.Visibility = ElementVisibility.Hidden;
                        
                        //radpageview1.Pages.Remove(CompanyVehicle);
                    }
                }
                else
                {
                    CompanyVehicle.Item.Visibility = ElementVisibility.Hidden;
                   // radpageview1.Pages.Remove(CompanyVehicle);
                }
                ///
  
                if (objMaster.Current.City.ToStr().Length > 0 && objMaster.Current.City.ToStr().Length <= 4 && objMaster.Current.City.ToStr().IsNumeric())
                {
                    numTrainingReqHours.Value = objMaster.Current.City.ToInt();
                }


                if (objMaster.Current.PostCode.ToStr().Length > 0 && objMaster.Current.PostCode.ToStr().Length <= 4 && objMaster.Current.PostCode.ToStr().IsNumeric())
                {
                    numTrainingCompletedHours.Value = objMaster.Current.PostCode.ToInt();
                }

                //MaxCommission
                numMaxCommission.Value = objMaster.Current.MaxCommission.ToDecimal();
                //BeforeDriverNo = objMaster.Current.DriverNo;
                //BeforeMaxCommission = objMaster.Current.MaxCommission.ToDecimal();
                //
                if (objMaster.Current.DriverRents.Count > 0)
                {
                    numInitialBalance.Enabled = false;

                }

                if (objMaster.Current.Fleet_Driver_Images.Count == 1)
                {
                    try
                    {
                        pic_Driver.SetImage(objMaster.Current.Fleet_Driver_Images[0].Photo);
                    }
                    catch
                    {


                    }
                }

                if (ddlDriverType.SelectedValue.Equals(1))
                {
                    numDriverRentComm.Value = objMaster.Current.DriverMonthlyRent.ToDecimal();
                }
                else if (ddlDriverType.SelectedValue.Equals(2))
                {
                    numDriverRentComm.Value = objMaster.Current.DriverCommissionPerBooking.ToDecimal();

                }

                numPDARent.Value = objMaster.Current.PDARent.ToDecimal();


                dtpVehAssignedOn.Value = objMaster.Current.LicenseExpiryDate.ToDateorNull();
         
                if (objMaster.Current.DriverTypeId.ToInt() == 1)
                {
                    chkUseCompanyVehicle.Checked = objMaster.Current.UseCompanyVehicle.ToBool();
                    numCarRent.Value = objMaster.Current.CarRent.ToDecimal();
                    numCarInsuranceRent.Value = objMaster.Current.CarInsuranceRent.ToDecimal();
                    numPrimeCompanyRent.Value = objMaster.Current.PrimeCompanyRent.ToDecimal();
                }

                // PDA/SIM Details

                if (objMaster.Current.Fleet_Driver_DeviceInfos != null)
                {

                    txt_SIM_Comments.Text = objMaster.Current.Fleet_Driver_DeviceInfos.Comments.ToStr().Trim();
                    txt_SIM_DataAllowance.Text = objMaster.Current.Fleet_Driver_DeviceInfos.DataAllowance.ToStr().Trim();
                    txt_SIM_IMEI.Text = objMaster.Current.Fleet_Driver_DeviceInfos.IMEINumber.ToStr().Trim();
                    txt_SIM_Make.Text = objMaster.Current.Fleet_Driver_DeviceInfos.DeviceMake.ToStr().Trim();
                    txt_SIM_Model.Text = objMaster.Current.Fleet_Driver_DeviceInfos.DeviceModel.ToStr().Trim();
                    txt_SIM_NetworkAPN.Text = objMaster.Current.Fleet_Driver_DeviceInfos.NetworkAPN.ToStr().Trim();
                    txt_SIM_NetworkName.Text = objMaster.Current.Fleet_Driver_DeviceInfos.SIMNetworkName.ToStr().Trim();
                    txt_SIM_Number.Text = objMaster.Current.Fleet_Driver_DeviceInfos.SIMNumber.ToStr().Trim();
                    txt_SIM_PDADeposits.Text = objMaster.Current.Fleet_Driver_DeviceInfos.DeviceDeposits.ToStr().Trim();
                    dtp_SIM_PDADateGiven.Value = objMaster.Current.Fleet_Driver_DeviceInfos.DeviceDateGiven.ToDateorNull();
                }

                //

                int titleId = 0;
                DateTime? dtExpiry = null;


                Fleet_Driver_Document doc = null;
                foreach (GridViewRowInfo Grow in grdDocuments.Rows)
                {

                    titleId = Grow.Cells[COL_DOCUMENT.DOCUMENTTITLEID].Value.ToInt();
                    if (titleId == DRIVER_DOCUMENTS.Insurance)
                    {
                        dtExpiry = objMaster.Current.InsuranceExpiryDate;

                    }
                    else if (titleId == DRIVER_DOCUMENTS.MOT)
                    {
                        dtExpiry = objMaster.Current.MOTExpiryDate;

                    }
                    else if (titleId == DRIVER_DOCUMENTS.PCODriver)
                    {
                        dtExpiry = objMaster.Current.PCODriverExpiryDate;

                    }

                    else if (titleId == DRIVER_DOCUMENTS.PCOVehicle)
                    {
                        dtExpiry = objMaster.Current.PCOVehicleExpiryDate;

                    }

                    else if (titleId == DRIVER_DOCUMENTS.MOT2)
                    {
                        dtExpiry = objMaster.Current.MOT2ExpiryDate;

                    }

                    else if (titleId == DRIVER_DOCUMENTS.LICENSE)
                    {
                        dtExpiry = objMaster.Current.DrivingLicenseExpiryDate;

                    }


                    else if (titleId == DRIVER_DOCUMENTS.ROADTAX)
                    {
                        dtExpiry = objMaster.Current.RoadTaxiExpiryDate;

                    }

                    Grow.Cells[COL_DOCUMENT.EXPIRYDATE].Value = dtExpiry;


                    doc = objMaster.Current.Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == titleId);

                    if (doc != null)
                    {


                        Grow.Cells[COL_DOCUMENT.ID].Value = doc.Id;
                        Grow.Cells[COL_DOCUMENT.MASTERID].Value = doc.DriverId;

                        Grow.Cells[COL_DOCUMENT.FILENAME].Value = doc.FileName;
                        Grow.Cells[COL_DOCUMENT.FULLPATH].Value = doc.FilePath;

                        Grow.Cells[COL_DOCUMENT.BADGENUMBER].Value = doc.BadgeNumber.ToStr().Trim();
                    }
                }


                grdAvailability.Rows.Clear();

                GridViewRowInfo row = null;
                foreach (var objAvail in objMaster.Current.Fleet_Driver_Availabilities)
                {
                    row = grdAvailability.Rows.AddNew();

                    row.Cells[COL_AVAILABILITY.ID].Value = objAvail.Id;
                    row.Cells[COL_AVAILABILITY.MASTERID].Value = objAvail.DriverId;
                    row.Cells[COL_AVAILABILITY.BECAMEAVAIL].Value = objAvail.AvailableDate.ToDateorNull();
                    row.Cells[COL_AVAILABILITY.ENDINGDATE].Value = objAvail.EndingDate.ToDateorNull();

                }

               

                ClearAvailability();




                if (grdRangeWiseComm != null)
                {
                    ClearCommissionGrid();

                    CommissionRangeList = new List<Fleet_Driver_CommissionRange>();
                      

                    if (grdRangeWiseComm.Visible == true)
                    {
                        foreach (var obj in objMaster.Current.Fleet_Driver_CommissionRanges)
                        {
                            row = grdRangeWiseComm.Rows.AddNew();

                            row.Cells["ID"].Value = obj.Id;
                            row.Cells["MASTERID"].Value = obj.DriverId;
                            row.Cells["FROMPRICE"].Value = obj.FromPrice.ToDecimal();
                            row.Cells["TOPRICE"].Value = obj.ToPrice.ToDecimal();
                            row.Cells["COMMISSIONPERCENT"].Value = obj.CommissionValue.ToDecimal();

                        }

                        grdRangeWiseComm.CurrentRow = null;
                    }
                }

                // For Driver Log
                DriverList = (new Fleet_Driver
                {
                    InsuranceExpiryDate = objMaster.Current.InsuranceExpiryDate.ToDateTimeorNull(),
                    LicenseExpiryDate = objMaster.Current.LicenseExpiryDate.ToDateTimeorNull(),
                    PCOVehicleExpiryDate = objMaster.Current.PCOVehicleExpiryDate.ToDateTimeorNull()
                    ,
                    PCODriverExpiryDate = objMaster.Current.PCODriverExpiryDate.ToDateTimeorNull()
                    ,
                    MOTExpiryDate = objMaster.Current.MOTExpiryDate.ToDateTimeorNull()
                    ,
                    MOT2ExpiryDate = objMaster.Current.MOT2ExpiryDate.ToDateTimeorNull()
                    ,
                    RoadTaxiExpiryDate = objMaster.Current.RoadTaxiExpiryDate.ToDateTimeorNull(),
                    DrivingLicenseExpiryDate=objMaster.Current.DrivingLicenseExpiryDate.ToDateTimeorNull(),
                    DriverNo=objMaster.Current.DriverNo,
                    MaxCommission=objMaster.Current.MaxCommission,
                    VehicleTypeId=objMaster.Current.VehicleTypeId,
                    VehicleNo=objMaster.Current.VehicleTypeId!=null?objMaster.Current.Fleet_VehicleType.VehicleType:""
                });
                //

                grdLister.Rows.Clear();



                Pg_notes.Item.Visibility = ElementVisibility.Visible;


                GridViewRowInfo notesrow = null;
                foreach (var objnotes in objMaster.Current.Fleet_Driver_Notes)
                {
                    notesrow = grdLister.Rows.AddNew();


                    //   string AddOnDate = string.Format("{0:dd/MM/yyyy}", objnotes.AddOn);



                    notesrow.Cells[COL_NOTES.Id].Value = objnotes.Id;
                    notesrow.Cells[COL_NOTES.Notes].Value = objnotes.Notes;
                    notesrow.Cells[COL_NOTES.DriverId].Value = objnotes.DriverId;
                    notesrow.Cells[COL_NOTES.Time].Value = objnotes.AddOnTime.ToDateTime();
                    notesrow.Cells[COL_NOTES.AddOn].Value = objnotes.AddOn.ToDate(); //AddOnDate; //objnotes.AddOn;
                    notesrow.Cells[COL_NOTES.AddBy].Value = objnotes.AddBy;
                    notesrow.Cells[COL_NOTES.DateTime].Value = objnotes.AddOnTime.ToDateTime();
                    //if (grdDriverNotes.RowCount > 0)
                    //{

                    //}

                }


                FormateCompanyVehicle();


                grdLister.GroupDescriptors.Expression = "AddOn";
                grdLister.GroupDescriptors[0].Format = "{1:dddd dd/MM/yyyy}";
                grdLister.AutoExpandGroups = true;

                grdLister.CurrentRow = null;

                //DisplayDriverVehicleHistory();


                //S,BC,SALO 
                string[] attributeValuesArr = objMaster.Current.AttributeValues.ToStr().Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in attributeValuesArr)
                {
                    var attrRow = grdDriverAttributes.Rows.FirstOrDefault(c => c.Cells[Col_DriverAttributes.ShortName].Value.ToStr().ToLower().Trim() == item.ToLower().Trim());

                    if (attrRow != null)
                    {
                        attrRow.Cells[Col_DriverAttributes.Default].Value = true;
                    }
                }
                var Shiftlist = objMaster.Current.Fleet_Driver_Shifts.Select(args => new { args.Driver_Shift_ID, args.Id }).ToList();
                            //select new
                            //{
                            //    a.Id,
                            //    a.Driver_Shift_ID
                                
                            //}).ToList();


                for (int i = 0; i < grdDriverShifts.RowCount; i++)
                {
                    if (Shiftlist.Count(c => c.Driver_Shift_ID == grdDriverShifts.Rows[i].Cells[COL_SHIFT.SHIFT_ID].Value.ToInt()) > 0)
                    {
                        grdDriverShifts.Rows[i].Cells["Check"].Value = true;
                        grdDriverShifts.Rows[i].Cells[COL_SHIFT.ID].Value = Shiftlist.Where(c => c.Driver_Shift_ID == grdDriverShifts.Rows[i].Cells[COL_SHIFT.SHIFT_ID].Value.ToInt()).FirstOrDefault().Id;
                    }
                }
                grdDriverAttributes.AllowEditRow = true;

                try
                {

                    int? Id = objMaster.Current.Id;
                    var list = (from a in General.GetQueryable<Complaint>(c => c.DriverId == Id)
                                orderby a.ComplainDateTime descending
                                select new
                                {
                                    Id = a.Id,
                                    RefNo = a.RefNo,
                                    JobRefNo = a.Booking.BookingNo,
                                    ComplainDate = a.ComplainDateTime,
                                    IncidentDate = a.IncidentDateTime,
                                    CustomerName = a.CustomerName,
                                    ComplainDescription = a.ComplainDescription,
                                    ResultDescription = a.ResultDescription
                                }).ToList();
                    if (list.Count() > 0)
                    {
                        grdDriverComplaints.DataSource = list;



                        //AddComplaintButton();
                    }

                    //    RateDriver.CurrentRating = objMaster.Current.AvgRating.ToInt();

                    if (objMaster.Current.AvgRating.ToDecimal() > 0)
                    txtRating.Text = Math.Round(objMaster.Current. AvgRating.ToDecimal(), 1).ToStr();
                }
                catch
                {


                }

            }
            catch (Exception ex)
            {


            }
        }

        private void ClearCommissionGrid()
        {
            if (grdRangeWiseComm != null)
                grdRangeWiseComm.Rows.Clear();



        }


        private void ClearDriverVehicleHistory()
        {

            if (grdVehicles != null)
                grdVehicles.Rows.Clear();
        }


        private void DisplayDriverVehicleHistory()
        {

            if (objMaster.Current != null && objMaster.PrimaryKeyValue != null)
            {

                grdVehicles.Rows.Clear();

                List<Fleet_Driver_AssignedVehicle> listofAssignedVehicles = General.GetQueryable<Fleet_Driver_AssignedVehicle>(c => c.DriverId == objMaster.Current.Id).ToList();



                GridViewRowInfo row = null;

                foreach (var item in listofAssignedVehicles)
                {
                    row = grdVehicles.Rows.AddNew();

                    row.Cells[COL_VEHICLES.ID].Value = item.Id;
                    row.Cells[COL_VEHICLES.ASSIGNEDON].Value = item.AssignedOn.ToDateorNull();
                    row.Cells[COL_VEHICLES.ENDON].Value = item.EndOn.ToDateorNull();
                    row.Cells[COL_VEHICLES.VEHICLETYPEID].Value = item.VehicleTypeId.ToIntorNull();
                    row.Cells[COL_VEHICLES.VEHICLETYPENAME].Value = item.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr();
                    row.Cells[COL_VEHICLES.VEHNO].Value = item.VehicleNo.ToStr();
                    row.Cells[COL_VEHICLES.OWNER].Value = item.VehicleOwner.ToStr();
                    row.Cells[COL_VEHICLES.MAKE].Value = item.VehicleMake.ToStr();
                    row.Cells[COL_VEHICLES.MODEL].Value = item.VehicleModel.ToStr();

                    row.Cells[COL_VEHICLES.VEHLOGBOOK].Value = item.VehicleLogBookNo.ToStr();

                    row.Cells[COL_VEHICLES.COLOR].Value = item.VehicleColor.ToStr();

                }
            }


        }


        #endregion

        private void btnAddAvailability_Click(object sender, EventArgs e)
        {
            if (chkEndDrier.Checked == true)
            {
                if (dtpEndingDate.Value == null)
                {
                    ENUtils.ShowMessage("Required End Date");
                    return;
                }

                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to End Driver", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    frmAdminPwd frm = new frmAdminPwd();
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    frm.Dispose();

                    string RetVAl = frm.ReturnValue1;
                    if (RetVAl == "OK")
                    {
                        AddAvailability();
                    }
                }
            }
            else
            {
                AddAvailability();
            }


        }


        private void AddAvailability()
        {
            DateTime? becameAvail = dtpAvailDate.Value;
            DateTime? endDate = dtpEndingDate.Value;

            string error = string.Empty;

            if (becameAvail == null)
            {
                error += "Required : Became Available Date";
            }

            if (endDate != null && endDate < becameAvail)
            {
                error += "Required : Ending Date must be greater than Available Date";
            }


            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;
            }


            GridViewRowInfo row = null;

            if (grdAvailability.CurrentRow != null)
            {
                row = grdAvailability.CurrentRow;
            }

            else
            {
                row = grdAvailability.Rows.AddNew();

            }
            row.Cells[COL_AVAILABILITY.BECAMEAVAIL].Value = becameAvail;
            row.Cells[COL_AVAILABILITY.ENDINGDATE].Value = endDate;


            ClearAvailability();

        }

        private void ClearAvailability()
        {
            chkEndDrier.Checked = false;
            dtpAvailDate.Value = DateTime.Now;
            dtpEndingDate.Value = null;
            grdAvailability.CurrentRow = null;
            dtpAvailDate.Focus();

        }

        private void btnClearAvail_Click(object sender, EventArgs e)
        {
            ClearAvailability();
        }

        private void ddlDriverType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            SetDriverType();
        }


        private void SetDriverType()
        {
            if (ddlDriverType.SelectedValue != null)
            {

                //ddlDriverType.SelectedValue = 1;
                if (ddlDriverType.SelectedValue.Equals(1))
                {
                    lblDriverType.Text = "Weekly Rent";
                    lblDriverType.Visible = true;
                    numDriverRentComm.Visible = true;
                    //  numDriverRentComm.Value = AppVars.objPolicyConfiguration.DriverMonthlyRent.ToDecimal();
                    ShowMaxCommission(false);
                    ShowCommissionRange(false);
                    groupOtherCharges.Visible = true;
                }
                else if (ddlDriverType.SelectedValue.Equals(2))
                {
                    lblDriverType.Text = "Commision %";
                    numDriverRentComm.Value = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();
                    ShowMaxCommission(true);
                    groupOtherCharges.Visible = false;
                    ShowCommissionRange(true);
                }
            }

        }

        private void ShowMaxCommission(bool ShowMaxCommission)
        {
            numMaxCommission.Value = 0.00m;
            numMaxCommission.Visible = ShowMaxCommission;
            lblMaxCommission.Visible = ShowMaxCommission;
        }



        private void ShowCommissionRange(bool show)
        {

            if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool() == false)
                return;



            InitializeCommissionRangeGrid();




            if (show)
            {

                if (grdRangeWiseComm.Columns.Count == 0)
                {

                    FormatRangeWiseCommissionGrid();
                }
            }



            lblRangeWiseCommission.Visible = show;
            grdRangeWiseComm.Visible = show;

        }

        private void InitializeCommissionRangeGrid()
        {
            if (lblRangeWiseCommission != null)
                return;

            this.lblRangeWiseCommission = new Telerik.WinControls.UI.RadLabel();
            this.grdRangeWiseComm = new Telerik.WinControls.UI.RadGridView();


            ((System.ComponentModel.ISupportInitialize)(this.lblRangeWiseCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangeWiseComm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangeWiseComm.MasterTemplate)).BeginInit();

            this.radPanel1.Controls.Add(this.lblRangeWiseCommission);
            this.radPanel1.Controls.Add(this.grdRangeWiseComm);


            // 
            // lblRangeWiseCommission
            // 
            this.lblRangeWiseCommission.AutoSize = false;
            this.lblRangeWiseCommission.BackColor = System.Drawing.Color.Wheat;
            this.lblRangeWiseCommission.BorderVisible = true;
            this.lblRangeWiseCommission.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblRangeWiseCommission.ForeColor = System.Drawing.Color.Black;
            this.lblRangeWiseCommission.Location = new System.Drawing.Point(507, 215);
            this.lblRangeWiseCommission.Name = "lblRangeWiseCommission";
            // 
            // 
            // 
            this.lblRangeWiseCommission.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblRangeWiseCommission.Size = new System.Drawing.Size(353, 21);
            this.lblRangeWiseCommission.TabIndex = 226;
            this.lblRangeWiseCommission.Text = "Commission Price Range";
            this.lblRangeWiseCommission.Visible = false;
            // 
            // grdRangeWiseComm
            // 
            this.grdRangeWiseComm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRangeWiseComm.Location = new System.Drawing.Point(507, 236);
            this.grdRangeWiseComm.Name = "grdRangeWiseComm";
            this.grdRangeWiseComm.Size = new System.Drawing.Size(353, 89);
            this.grdRangeWiseComm.TabIndex = 225;
            this.grdRangeWiseComm.Text = "radGridView1";
            this.grdRangeWiseComm.Visible = false;


            ((System.ComponentModel.ISupportInitialize)(this.lblRangeWiseCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangeWiseComm.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangeWiseComm)).EndInit();

        }

        private void chkEndDrier_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            EndDriver(args.ToggleState);
        }
        private void EndDriver(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {

                dtpEndingDate.Enabled = true;


            }
            else
            {
                dtpEndingDate.Enabled = false;

                dtpEndingDate.Value = null;

            }
        }
        // page 2 
        private void btnAddShift_Click(object sender, EventArgs e)
        {
            try
            {
                AddShift();
                FormateShiftGride();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        private void AddShift()
        {

            DateTime? From = dtpFromTime.Value;
            DateTime? To = dtpTOTime.Value.Value;

            int? DriverShiftID = ddlShifts.SelectedValue.ToInt();


            string error = string.Empty;

            if (From == null)
            {
                error += "Required : From Time";
            }
            if (To == null)
            {
                error += "Required : To Time";
            }
            if (DriverShiftID == 0)
            {
                error += "Required : Driver Shift";
            }




            //if (To != null && To < from)
            if (From.Value.Hour <= 12 && To.Value.Hour <= 12)
            {
                if (To < From)
                {

                    error += "Required : TO Time must be greater than End Time";
                }

            }
            else if (From.Value.Hour > 12 && To.Value.Hour > 12)
            {
                if (To < From)
                {

                    error += "Required : TO Time must be greater than End Time";
                }

            }




            //{
            //    error += "Required : TO Time must be greater than End Time";
            //}


            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;
            }


            string DriverShift = ddlShifts.SelectedItem.Text.ToStr();

            GridViewRowInfo row = null;

            if (grdShifts.CurrentRow != null)
            {
                row = grdShifts.CurrentRow;
            }

            else
            {
                if (grdShifts.Rows.Count > 0)
                {
                    for (int index = 0; index < grdShifts.Rows.Count; index++)
                    {
                        int? ShiftID = grdShifts.Rows[index].Cells["SHIFT_ID"].Value.ToInt();

                        TimeSpan TimeNow = DateTime.Now.TimeOfDay;

                        DateTime FTime = grdShifts.Rows[index].Cells["FROMTIME"].Value.ToDateTime();
                        TimeSpan FromTime;
                        FromTime = FTime.TimeOfDay;


                        DateTime TTime = grdShifts.Rows[index].Cells["TOTIME"].Value.ToDateTime();
                        TimeSpan ToTime;
                        ToTime = TTime.TimeOfDay;

                        DateTime DTPF = dtpFromTime.Value.ToDateTime();
                        TimeSpan DTPFROM;
                        DTPFROM = DTPF.TimeOfDay;

                        DateTime DTPT = dtpTOTime.Value.ToDateTime();
                        TimeSpan DTPTO;
                        DTPTO = DTPT.TimeOfDay;

                        if (DriverShiftID == 7)
                        {
                            if (grdShifts.Rows.Count > 0)
                            {
                                ENUtils.ShowMessage("Please Remove All Driver Shift.");
                                return;
                            }

                        }
                        if (ShiftID == DriverShiftID)
                        {
                            ENUtils.ShowMessage("Shift Already In a List");
                            FormateShiftGride();
                            return;

                        }
                        if (ToTime > DTPFROM)
                        {
                            ENUtils.ShowMessage("This timings already in " + grdShifts.Rows[index].Cells["SHIFT"].Value.ToString() + " Shift.");
                            FormateShiftGride();
                            return;
                        }
                        if (ShiftID == 7)
                        {
                            ENUtils.ShowMessage("Please Remove Any Time Shift");
                            return;
                        }
                    }
                }

                row = grdShifts.Rows.AddNew();
            }
            row.Cells[COL_SHIFT.SHIFT_ID].Value = DriverShiftID;
            row.Cells[COL_SHIFT.SHIFT].Value = DriverShift;
            row.Cells[COL_SHIFT.FROMTIME].Value = From;
            row.Cells[COL_SHIFT.TOTIME].Value = To;

            ClearShift();
            FormateShiftGride();
        }
        private void FormateShiftGride()
        {
            grdShifts.Columns["FROMTIME"].Width = 100;
            grdShifts.Columns["TOTIME"].Width = 100;
            grdShifts.Columns["SHIFT"].Width = 200;
            grdShifts.Columns["SHIFT_ID"].IsVisible = false;

        }
        private void FormatDriverShiftGrid()
        {
            grdDriverShifts.AllowAutoSizeColumns = true;
            grdDriverShifts.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdDriverShifts.AllowAddNewRow = false;
            grdDriverShifts.ShowGroupPanel = false;
            // grdShifts.AutoCellFormatting = true;
            grdDriverShifts.ShowRowHeaderColumn = false;
            grdDriverShifts.EnableFiltering = true;
            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = "Check";
            cbcol.Width = 50;
            grdDriverShifts.Columns.Add(cbcol);

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SHIFT.ID;
            grdDriverShifts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SHIFT.MASTERID;
            grdDriverShifts.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Shifts ID";
            col.IsVisible = false;
            col.Name = COL_SHIFT.SHIFT_ID;
            grdDriverShifts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Shifts";
            col.ReadOnly = true;
            col.Width = 160;
            col.Name = COL_SHIFT.SHIFT;
            grdDriverShifts.Columns.Add(col);

            
        }
        private void ClearShift()
        {
            dtpFromTime.Value = DateTime.Now;
            dtpTOTime.Value = DateTime.Now;
            grdShifts.CurrentRow = null;
            dtpFromTime.Focus();

        }
        private void FormatShiftGrid()
        {
            grdShifts.AllowAutoSizeColumns = true;
            grdShifts.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdShifts.AllowAddNewRow = false;
            grdShifts.ShowGroupPanel = false;
            // grdShifts.AutoCellFormatting = true;
            grdShifts.ShowRowHeaderColumn = false;

            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SHIFT.ID;
            grdShifts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SHIFT.MASTERID;
            grdShifts.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Shifts ID";
            col.IsVisible = false;
            col.Name = COL_SHIFT.SHIFT_ID;
            grdShifts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Shifts";
            col.IsVisible = true;
            col.Name = COL_SHIFT.SHIFT;
            grdShifts.Columns.Add(col);

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "From Time";
            colDate.Name = COL_SHIFT.FROMTIME;
            colDate.Format = DateTimePickerFormat.Custom;
            //colDate.CustomFormat = "dd/MM/yyyy";
            //colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            colDate.Width = 120;
            col.ReadOnly = false;
            grdShifts.Columns.Add(colDate);

            colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "To Time";
            colDate.Name = COL_SHIFT.TOTIME;
            colDate.Format = DateTimePickerFormat.Custom;
            //colDate.CustomFormat = "dd/MM/yyyy";
            //colDate.FormatString = "{0:dd/MM/yyyy}";
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            colDate.Width = 120;
            col.ReadOnly = false;
            grdShifts.Columns.Add(colDate);


            UI.GridFunctions.AddDeleteColumn(grdShifts);


        }
        void grdShifts_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewDataRowInfo)
            {
                dtpFromTime.Value = e.Row.Cells[COL_SHIFT.FROMTIME].Value.ToDateTime();
                dtpTOTime.Value = e.Row.Cells[COL_SHIFT.TOTIME].Value.ToDateTime();
                ddlShifts.SelectedValue = e.Row.Cells[COL_SHIFT.SHIFT_ID].Value.ToInt();

            }
        }

        private void dtpFromTime_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void dtpTOTime_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearShift();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptfrmDriverInfo Driverinfo = new rptfrmDriverInfo(objMaster.Current.Id, false);
            Driverinfo.FormBorderStyle = FormBorderStyle.FixedSingle;
            Driverinfo.StartPosition = FormStartPosition.CenterScreen;

            Driverinfo.ShowDialog();
            Driverinfo.Dispose();
        }

        private void btnPrintRejectJobs_Click(object sender, EventArgs e)
        {
            rptfrmDriverInfo Driverinfo = new rptfrmDriverInfo(objMaster.Current.Id, true);
            Driverinfo.FormBorderStyle = FormBorderStyle.FixedSingle;
            Driverinfo.StartPosition = FormStartPosition.CenterScreen;
            Driverinfo.DriverId = objMaster.Current.Id;
            Driverinfo.ShowDialog();
            Driverinfo.Dispose();
        }



        private void btnBrekReport_Click(object sender, EventArgs e)
        {
            try
            {
                //fahad
                rptfrmDriverBreakHIstory frm = new rptfrmDriverBreakHIstory(objMaster.Current.Id);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


        private void ddlShifts_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            int? ShiftId = ddlShifts.SelectedValue.ToInt();
            if (ShiftId == 7)
            {
                dtpFromTime.Value = DateTime.Now.ToDateorNull();
                dtpTOTime.Value = DateTime.Now.ToDateorNull();
                dtpFromTime.Enabled = false;
                dtpTOTime.Enabled = false;
            }
            else
            {
                dtpFromTime.Enabled = true;
                dtpTOTime.Enabled = true;
            }
        }


        private void frmDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    Fleet_Driver objDrv = db.Fleet_Drivers.FirstOrDefault(c => c.Id == objMaster.Current.Id);

                    if (objDrv != null)
                    {
                        objDrv.HasPDA = chkHasPDA.Checked;
                        db.SubmitChanges();

                    }


                }

                this.Close();


            }
            catch (Exception ex)
            {


            }
        }

        private void chkActiveDriver_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            try
            {

                if (objMaster.PrimaryKeyValue != null)
                {

                    if (args.NewValue == ToggleState.On)
                    {
                        if (GeneralBLL.GetQueryable<Fleet_Driver>(c => ((c.DriverNo.Trim().ToLower() == objMaster.Current.DriverNo) && c.IsActive == true)
                                        && (c.Id != objMaster.Current.Id)).Count() > 0)
                        {
                            ENUtils.ShowMessage("You cannot Active Same Drivers at a time" + Environment.NewLine + "Other Driver with Same No " + objMaster.Current.DriverNo + " is Active");
                            args.Canceled = true;
                            return;

                        }

                    }


                    frmAdminPwd frm = new frmAdminPwd();
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();


                    string RetVAl = frm.ReturnValue1;
                    frm.Dispose();

                    if (RetVAl == "Exit")
                    {
                        args.Canceled = true;

                    }

                }
            }
            catch (Exception ex)
            {


            }


        }


        private void InitializeShiftPanel()
        {
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel27 = new Telerik.WinControls.UI.RadLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdShifts = new Telerik.WinControls.UI.RadGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radLabel26 = new Telerik.WinControls.UI.RadLabel();
            this.dtpTOTime = new UI.MyDatePicker();
            this.ddlShifts = new UI.MyDropDownList();
            this.dtpFromTime = new UI.MyDatePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.radLabel12 = new Telerik.WinControls.UI.RadLabel();
            this.btnAddShift = new System.Windows.Forms.Button();
            this.radLabel19 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel20 = new Telerik.WinControls.UI.RadLabel();


            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts.MasterTemplate)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTOTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel20)).BeginInit();



            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.Color.Transparent;
            this.radPanel2.Controls.Add(this.radLabel27);
            this.radPanel2.Controls.Add(this.panel3);
            this.radPanel2.Location = new System.Drawing.Point(3, 14);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(841, 547);
            this.radPanel2.TabIndex = 0;
            // 
            // radLabel27
            // 
            this.radLabel27.AutoSize = false;
            this.radLabel27.BackColor = System.Drawing.Color.DarkRed;
            this.radLabel27.BorderVisible = true;
            this.radLabel27.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel27.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel27.ForeColor = System.Drawing.Color.White;
            this.radLabel27.Location = new System.Drawing.Point(0, 0);
            this.radLabel27.Name = "radLabel27";
            // 
            // 
            // 
            this.radLabel27.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel27.Size = new System.Drawing.Size(841, 27);
            this.radLabel27.TabIndex = 108;
            this.radLabel27.Text = "Driver Shifts";
            this.radLabel27.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel27.GetChildAt(0))).BorderVisible = true;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel27.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel27.GetChildAt(0))).Text = "Driver Shifts";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel27.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel27.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel27.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdShifts);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.radLabel20);
            this.panel3.Location = new System.Drawing.Point(89, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(609, 223);
            this.panel3.TabIndex = 107;
            // 
            // grdShifts
            // 
            // this.grdShifts.AutoCellFormatting = false;
            this.grdShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            //  this.grdShifts.EnableCheckInCheckOut = false;
            this.grdShifts.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //   this.grdShifts.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            //   this.grdShifts.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdShifts.Location = new System.Drawing.Point(0, 90);
            // 
            // grdShifts
            // 
            this.grdShifts.MasterTemplate.AllowAddNewRow = false;
            this.grdShifts.MasterTemplate.AllowEditRow = false;
            this.grdShifts.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdShifts.Name = "grdShifts";
            //  this.grdShifts.PKFieldColumnName = "";
            this.grdShifts.ShowGroupPanel = false;
            //  this.grdShifts.ShowImageOnActionButton = true;
            this.grdShifts.Size = new System.Drawing.Size(609, 133);
            this.grdShifts.TabIndex = 97;
            this.grdShifts.Text = "myGridView1";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.radLabel26);
            this.panel4.Controls.Add(this.dtpTOTime);
            this.panel4.Controls.Add(this.ddlShifts);
            this.panel4.Controls.Add(this.dtpFromTime);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.radLabel12);
            this.panel4.Controls.Add(this.btnAddShift);
            this.panel4.Controls.Add(this.radLabel19);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 18);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(609, 72);
            this.panel4.TabIndex = 96;
            // 
            // radLabel26
            // 
            this.radLabel26.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel26.Location = new System.Drawing.Point(11, 15);
            this.radLabel26.Name = "radLabel26";
            this.radLabel26.Size = new System.Drawing.Size(48, 23);
            this.radLabel26.TabIndex = 217;
            this.radLabel26.Text = "Shifts";
            // 
            // dtpTOTime
            // 
            this.dtpTOTime.AllowDrop = true;
            this.dtpTOTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpTOTime.CustomFormat = "HH:mm";
            this.dtpTOTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTOTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTOTime.Location = new System.Drawing.Point(330, 40);
            this.dtpTOTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTOTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTOTime.Name = "dtpTOTime";
            this.dtpTOTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTOTime.Size = new System.Drawing.Size(83, 24);
            this.dtpTOTime.TabIndex = 68;
            this.dtpTOTime.TabStop = false;
            this.dtpTOTime.Text = "myDatePicker1";
            this.dtpTOTime.Value = null;
            this.dtpTOTime.Opening += new System.ComponentModel.CancelEventHandler(this.dtpTOTime_Opening);
            // 
            // ddlShifts
            // 
            this.ddlShifts.Caption = null;
            this.ddlShifts.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlShifts.Location = new System.Drawing.Point(68, 13);
            this.ddlShifts.Name = "ddlShifts";
            this.ddlShifts.Property = null;
            this.ddlShifts.ShowDownArrow = true;
            this.ddlShifts.Size = new System.Drawing.Size(179, 26);
            this.ddlShifts.TabIndex = 218;
            this.ddlShifts.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlShifts_SelectedIndexChanged);
            // 
            // dtpFromTime
            // 
            this.dtpFromTime.AllowDrop = true;
            this.dtpFromTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFromTime.CustomFormat = "HH:mm";
            this.dtpFromTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromTime.Location = new System.Drawing.Point(331, 12);
            this.dtpFromTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Name = "dtpFromTime";
            this.dtpFromTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Size = new System.Drawing.Size(83, 24);
            this.dtpFromTime.TabIndex = 67;
            this.dtpFromTime.TabStop = false;
            this.dtpFromTime.Text = "myDatePicker1";
            this.dtpFromTime.Value = null;
            this.dtpFromTime.Opening += new System.ComponentModel.CancelEventHandler(this.dtpFromTime_Opening);
            // 
            // button1
            // 
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(529, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "New";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radLabel12
            // 
            this.radLabel12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel12.Location = new System.Drawing.Point(268, 43);
            this.radLabel12.Name = "radLabel12";
            this.radLabel12.Size = new System.Drawing.Size(51, 18);
            this.radLabel12.TabIndex = 65;
            this.radLabel12.Text = "To Time";
            // 
            // btnAddShift
            // 
            this.btnAddShift.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAddShift.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddShift.Location = new System.Drawing.Point(440, 37);
            this.btnAddShift.Name = "btnAddShift";
            this.btnAddShift.Size = new System.Drawing.Size(72, 23);
            this.btnAddShift.TabIndex = 3;
            this.btnAddShift.Text = "Add";
            this.btnAddShift.UseVisualStyleBackColor = true;
            this.btnAddShift.Click += new System.EventHandler(this.btnAddShift_Click);
            // 
            // radLabel19
            // 
            this.radLabel19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel19.Location = new System.Drawing.Point(261, 16);
            this.radLabel19.Name = "radLabel19";
            this.radLabel19.Size = new System.Drawing.Size(65, 18);
            this.radLabel19.TabIndex = 63;
            this.radLabel19.Text = "From Time";
            // 
            // radLabel20
            // 
            this.radLabel20.AutoSize = false;
            this.radLabel20.BackColor = System.Drawing.Color.Purple;
            this.radLabel20.BorderVisible = true;
            this.radLabel20.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel20.ForeColor = System.Drawing.Color.White;
            this.radLabel20.Location = new System.Drawing.Point(0, 0);
            this.radLabel20.Name = "radLabel20";
            // 
            // 
            // 
            this.radLabel20.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel20.Size = new System.Drawing.Size(609, 18);
            this.radLabel20.TabIndex = 95;
            this.radLabel20.Text = "Shift Details";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel20.GetChildAt(0))).BorderVisible = true;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel20.GetChildAt(0))).Text = "Shift Details";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel20.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel20.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel20.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;



            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTOTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel20)).EndInit();


            this.radPageViewPage2.Controls.Add(this.radPanel2);

            dtpFromTime.Value = DateTime.Now;
            dtpTOTime.Value = DateTime.Now;

            ComboFunctions.FillDriverShiftsCombo(ddlShifts);
            FormatShiftGrid();
            grdShifts.CellDoubleClick += new GridViewCellEventHandler(grdShifts_CellDoubleClick);


            if (txtDriverNo.ReadOnly)
            {
                btnAddShift.Enabled = false;
                grdShifts.Enabled = false;


            }

            DisplayShiftData();

        }


        private void DisplayShiftData()
        {
            if (objMaster.Current != null)
            {

                // page 2
                FormateShiftGride();

                grdShifts.Rows.Clear();

                GridViewRowInfo row = null;
                foreach (var objShift in objMaster.Current.Fleet_Driver_Shifts)
                {
                    row = grdShifts.Rows.AddNew();

                    row.Cells[COL_SHIFT.ID].Value = objShift.Id;
                    row.Cells[COL_SHIFT.MASTERID].Value = objShift.DriverId;
                    row.Cells[COL_SHIFT.SHIFT_ID].Value = objShift.Driver_Shift_ID;
                    row.Cells[COL_SHIFT.SHIFT].Value = objShift.Driver_Shift.ShiftName;
                    row.Cells[COL_SHIFT.FROMTIME].Value = objShift.FromTime;
                    row.Cells[COL_SHIFT.TOTIME].Value = objShift.ToTime;
                }

                ClearShift();
            }
        }

        private void btnAssignedNew_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMsg = string.Empty;

                if (dtpVehAssignedOn.Value == null)
                {

                    errorMsg = "Required : Assigned On" + Environment.NewLine;
                }

                if (ddlVehicleType.SelectedValue == null)
                {
                    errorMsg += "Required : Vehicle Type" + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(ddlVehicleColor.Text))
                {
                    errorMsg += "Required : Vehicle Color" + Environment.NewLine;
                }


                if (dtpVehEndOn.Value == null)
                {
                    errorMsg += "Required : Vehicle End Date" + Environment.NewLine;
                }


                if (!string.IsNullOrEmpty(errorMsg))
                {
                    ENUtils.ShowMessage(errorMsg);
                    return;
                }

                InitializeVehicleHistoryPanel();

                SaveDriverVehicleHistory();

                GridViewRowInfo row = grdVehicles.Rows.AddNew();

                row.Cells[COL_VEHICLES.ASSIGNEDON].Value = dtpVehAssignedOn.Value;
                row.Cells[COL_VEHICLES.VEHICLETYPEID].Value = ddlVehicleType.SelectedValue;
                row.Cells[COL_VEHICLES.VEHICLETYPENAME].Value = ddlVehicleType.Text.ToStr().Trim();
                row.Cells[COL_VEHICLES.COLOR].Value = ddlVehicleColor.Text.Trim().ToStr();
                row.Cells[COL_VEHICLES.OWNER].Value = txtVehOwner.Text.ToStr().Trim();
                row.Cells[COL_VEHICLES.MAKE].Value = txtVehMake.Text.ToStr().Trim();
                row.Cells[COL_VEHICLES.MODEL].Value = txtVehModel.Text.ToStr().Trim();
                row.Cells[COL_VEHICLES.VEHNO].Value = txtVehNo.Text.ToStr().Trim();

                row.Cells[COL_VEHICLES.VEHLOGBOOK].Value = txtVehicleLogBookNo.Text.ToStr().Trim();

                row.Cells[COL_VEHICLES.ENDON].Value = dtpVehEndOn.Value;


                ENUtils.ShowMessage("This Vehicle has been Added to Vehicle History Info");

                ClearCurrentVehicleDetails();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void SaveDriverVehicleHistory()
        {
            try
            {
                Fleet_DriverAssignedVehicleBO objDriverVehMaster = new Fleet_DriverAssignedVehicleBO();
                objDriverVehMaster.New();
                objDriverVehMaster.Current.AssignedOn = dtpVehAssignedOn.Value.ToDateorNull();
                objDriverVehMaster.Current.DriverId = objMaster.Current.Id;
                objDriverVehMaster.Current.EndOn = dtpVehEndOn.Value.ToDateorNull();
                objDriverVehMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                objDriverVehMaster.Current.VehicleColor = ddlVehicleColor.Text.ToStr().Trim();
                objDriverVehMaster.Current.VehicleMake = txtVehMake.Text.Trim();
                objDriverVehMaster.Current.VehicleModel = txtVehModel.Text.Trim();
                objDriverVehMaster.Current.VehicleOwner = txtVehOwner.Text.Trim();
                objDriverVehMaster.Current.VehicleNo = txtVehNo.Text.Trim();
                objDriverVehMaster.Current.VehicleLogBookNo = txtVehicleLogBookNo.Text.Trim();
                objDriverVehMaster.Save();

            }
            catch (Exception ex)
            {



            }


        }


        private void ClearCurrentVehicleDetails()
        {
            dtpVehAssignedOn.Value = DateTime.Now;
            dtpVehEndOn.Value = null;
            ddlVehicleType.SelectedValue = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;
            ddlVehicleColor.SelectedValue = 2;
            txtVehMake.Text = string.Empty;
            txtVehModel.Text = string.Empty;
            txtVehNo.Text = string.Empty;
            txtVehOwner.Text = string.Empty;
            txtVehicleLogBookNo.Text = string.Empty;

        }

        public struct COL_RATING
        {
            public static string Id = "Id";
            public static string BookingId = "BookingId";
            public static string Rating = "Rating";
            public static string FeedBack = "FeedBack";
            public static string UpdatedOn = "UpdatedOn";
        }

        void grdRating_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name == "btnView")
            {
                if (grdRating.CurrentRow != null && grdRating.CurrentRow is GridViewDataRowInfo)
                {
                   int Id = grdRating.CurrentRow.Cells["BookingId"].Value.ToInt();
                    if (Id > 0)
                    General.ShowBookingForm(Id, true, "", "", Enums.BOOKING_TYPES.LOCAL);
                }
            }
        }
       

        private void PopulateDriverRating()
        {
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.DeferredLoadingEnabled = true;


                    var data1 =db.Fleet_Driver_Ratings.Where(c => c.DriverId == objMaster.Current.Id).OrderByDescending(c => c.UpdatedOn).ToList();

                    grdRating.RowCount = data1.Count;
                    for (int i = 0; i < data1.Count; i++)
                    {
                        grdRating.Rows[i].Cells[COL_RATING.Id].Value = data1[i].Id;
                        grdRating.Rows[i].Cells[COL_RATING.Rating].Value = data1[i].Rating;
                        grdRating.Rows[i].Cells[COL_RATING.BookingId].Value = data1[i].BookingId;
                        grdRating.Rows[i].Cells[COL_RATING.FeedBack].Value = data1[i].FeedBack;
                        grdRating.Rows[i].Cells[COL_RATING.UpdatedOn].Value = data1[i].UpdatedOn;

                    }
                }

            }
            catch (Exception ex)
            {


            }

        }

        private void FormateRatingGride()
        {
            try
            {


                grdRating.AllowAutoSizeColumns = true;
                grdRating.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                grdRating.AllowAddNewRow = false;
                grdRating.ShowGroupPanel = false;
                grdRating.AutoCellFormatting = true;
                grdRating.ShowRowHeaderColumn = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COL_RATING.Id;
                grdRating.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COL_RATING.BookingId;
                col.Width = 30;
                grdRating.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.IsVisible = true;
                col.HeaderText = "Rating";
                col.Name = COL_RATING.Rating;
                col.Width = 60;
                col.TextAlignment = ContentAlignment.MiddleCenter;
                grdRating.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Feedback";
                col.IsVisible = true;
                col.ReadOnly = true;
                col.Name = COL_RATING.FeedBack;
                col.Width = 200;
                grdRating.Columns.Add(col);
              
                GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
                colDate.HeaderText = "Feedback On";
                colDate.Name = COL_RATING.UpdatedOn;
                colDate.Format = DateTimePickerFormat.Custom;
                colDate.CustomFormat = "MM:dd:yyyy HH:mm:ss";
                colDate.FormatString = "{0:MM:dd:yyyy HH:mm:ss}";
                colDate.Width = 150;
                col.ReadOnly = true;
                grdRating.Columns.Add(colDate);


                //grdRating.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                //grdRating.AddDeleteColumn();
                //grdRating.Columns["btnViewBooking"].Width = 70;
                AddCommandColumn("btnView", "View Booking", 60);
                UI.GridFunctions.SetFilter(grdRating);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void AddCommandColumn(string name, string headerText, int width)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = width;
            col.UseDefaultText = true;
            col.DefaultText = headerText;
            col.Name = name;
            grdRating.Columns.Add(col);

        }
        private void btnUpdateSettings_Click(object sender, EventArgs e)
        {

            try
            {

                var objQueue = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == objMaster.Current.Id).OrderByDescending(c => c.Id).FirstOrDefault();


                if (objQueue == null)
                {
                    ENUtils.ShowMessage("You can't update pda settings at this time " + Environment.NewLine + "Please Login a Driver before updating it");
                    return;
                }
                else
                {
                   
                    if (objMaster.Current.Fleet_Driver_PDASettings.Count > 0 && objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion < 10)
                    {
                       if (General.GetQueryable<Fleet_Driver_Location>(c => c.DriverId == objMaster.Current.Id && c.UpdateDate.AddMinutes(1) < DateTime.Now).Count() > 0)
                        {
                            ENUtils.ShowMessage("Driver PDA is not working at the moment! Please Re-Login it or Check pda Internet settings");
                            return;

                        }
                    }

                }


                decimal version = objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal();



                if (chkEnableBidding.Checked)
                {

                    if (AppVars.objPolicyConfiguration.BiddingType.ToInt() == 1)
                    {
                        txtBiddingMessage.Text = "Fastest Finger";
                    }
                    else if (AppVars.objPolicyConfiguration.BiddingType.ToInt() == 2)
                    {
                        txtBiddingMessage.Text = "Nearest Driver";
                    }
                    else if (AppVars.objPolicyConfiguration.BiddingType.ToInt() == 3)
                    {
                        txtBiddingMessage.Text = "Longest Waiting in Queue";
                    }

                }



                if (chkEnableFareMeter.Checked)
                {

                    if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                    {
                        txtFareMessage.Text = "PeakOff Peak Fares";
                    }
                    else
                    {
                        txtFareMessage.Text = "Mileage Fares";

                    }

                }


                decimal drvWaitingCharges = 0.00m;
                decimal drvAccWaitingCharges = 0.00m;
                string navigationApp = "4";

                string manualFares = (chkEnableManualFares.Checked ? "1" : "0");
                 


                if (ddlNavigation.SelectedIndex == 1)
                    navigationApp = "1";
                else if (ddlNavigation.SelectedIndex == 2)
                    navigationApp = "2";
                else if (ddlNavigation.SelectedIndex == 3)
                    navigationApp = "3";


                else if (ddlNavigation.SelectedIndex == 4)
                    navigationApp = "5";



                if (chkEnableOptionalManualFares.Checked && chkEnableManualFares.Checked)
                    manualFares = "2";          

                if (chkEnableJobExtraCharges.Checked)
                {
                    var obj = General.GetObject<Fleet_VehicleType>(c => c.DriverWaitingChargesPerHour != null && c.DriverWaitingChargesPerHour > 0);


                    if (obj != null)
                    {

                        drvWaitingCharges = obj.DriverWaitingChargesPerHour.ToDecimal();
                        drvAccWaitingCharges = obj.AccountWaitingChargesPerHour.ToDecimal();

                    }
                }
                

                btnUpdateSettings.Enabled = false;


                StringBuilder contents = new StringBuilder();            

                contents.Append("update settings<<<");

              
                if (version >= 14)
                {
                    // update settings in json Format

                    DriverPDASettings pda = new DriverPDASettings();
                    pda.Ip = AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim();
                    pda.DrvId = objMaster.Current.Id.ToStr();

                    pda.DrvNo = objMaster.Current.DriverNo.ToStr();
                    pda.DrvName = objMaster.Current.DriverName.ToStr().Trim();
                    pda.VehType = objMaster.Current.Fleet_VehicleType.VehicleType.ToStr().ToUpper();
                    pda.GPSInterval = "4";
                   
                    pda.EnableJobExtraCharges= ((chkEnableJobExtraCharges.Checked ? "1" : "0"));  // Extra Charges
                    pda.ShowCompletedJobs=((chkShowCompletedJobs.Checked ? "1" : "0") ); // Show Completed Jobs

                    pda.EnableBidding=(chkEnableBidding.Checked ? "1" : "0") ; // Enable Bidding

                    pda.ShowPlots=(chkShowPlots.Checked ? "1" : "0") ; // Show Plots  -- index 10

                    pda.ShowNavigation=((chkShowNavigation.Checked ? "1" : "0") ); // Show Plots -- index 11

                    pda.JobTimeout=((numJobTimeout.Value.ToStr())); // Show Plots -- index 12
                    pda.ZoneInterval=(("40") ); // Zone Update Interval -- index 13
                    pda.SoundOnZoneChange=((chkShowSoundOnZoneChange.Checked ? "1" : "0") ); // Sound On Zone Change -- index 14
                    pda.MessageStayOnScreen=((chkMessageStay.Checked ? "1" : "0") ); // Message Stay -- index 15


                    pda.EnableCompanyCars=((chkEnableCompanyCars.Checked ? "1" : "0") ); // Show Plots -- index 16
                  //  pda.Append((" ") + ","); // Show Plots -- index 17
                    pda.EnableFareMeter=((chkEnableFareMeter.Checked ? "1" : "0") ); // Show Plots -- index 18
                    pda.ShowCustomerNo=((chkShowCustomerMobileNo.Checked ? "1" : "0") ); // Show Plots -- index 19
                    pda.HidePickupAndDest=((chkHidePickupAndDest.Checked ? "1" : "0") ); // Show Plots -- index 20

                    if (pda.HidePickupAndDest == "1")
                    {
                        if (ddlHidePickupAndDestinationType.SelectedIndex == 1)
                            pda.HidePickupAndDest = "2";

                        else if (ddlHidePickupAndDestinationType.SelectedIndex == 2)
                            pda.HidePickupAndDest = "3";

                        else if (ddlHidePickupAndDestinationType.SelectedIndex == 3)
                            pda.HidePickupAndDest = "4";

                    }


                    pda.EnableLogoutOnRejectJob=((chkEnableLogoutOnReject.Checked ? "1" : "0") ); // Show Plots -- index 21
                   // contents.Append((" ") + ","); // DeviceId -- index 22
                 

                    pda.FontSize = "20"; // index no 23
                    pda.NavigationType=(navigationApp ); // DeviceId -- index 24


                    pda.EnableFlagDown=((chkEnableFlagDown.Checked ? "1" : "0") ); // -- index 25
                    pda.MessageStayOnScreen=((chkMessageStay.Checked ? "1" : "0") ); // -- index 26

                    pda.DisablePanic=((chkDisablePanic.Checked ? "1" : "0")); // index 27
                    pda.DisableRank=((chkDisableRank.Checked ? "1" : "0") ); // index 28



                    pda.MeterVoice = ((chkVoiceOnClearMeter.Checked ? "1" : "0")); // index 28
                    pda.DisableChangeJobPlot=((chkDisableChangeJobPlot.Checked ? "1" : "0") );// index 30

                    pda.EnableJ15Jobs=((chkEnableJ15Jobs.Checked ? "1" : "0")); // index 31
                    pda.EnableLogoutAuth=((chkEnableLogoutAuthorization.Checked ? "1" : "0") ); // index 32
                    pda.EnableIgnoreArrive=((chkIgnoreArriveAction.Checked ? "1" : "0") ); // index 33
                    pda.BiddingType=((txtBiddingMessage.Text.Trim() == string.Empty ? " " : txtBiddingMessage.Text.Trim())); // index 34

                    pda.FareMeterType=((txtFareMessage.Text.Trim() == string.Empty ? " " : txtFareMessage.Text.Trim())); // index 35


                    pda.EnableOptMeter=((chkEnableOptionalMeter.Checked ? "1" : "0") ); // index 36
                    pda.DisableMeterForAccJob=((chkDisableMeterAccJob.Checked ? "1" : "0") );// index 37
                
                    pda.Courier = "0"; // index 38

                    pda.ShowFaresOnExtraCharges=((chkShowFareonExtraCharges.Checked ? "1" : "0") ); // index 39
                    pda.EnableCallCustomer=((chkEnableCallCustomer.Checked ? "1" : "0")); // index 40


                    pda.EnableRecoverJob=((chkEnableRecoverJob.Checked ? "1" : "0") );// index 41

                    pda.EnableMeterWaitingCharges=((chkEnableMeterWaitingCharges.Checked ? "1" : "0") ); // index 42

           
                    pda.LogoutOnOverShift=((chkShiftOverLogout.Checked ? "1" : "0")); // // Shift Logout                 
                    pda.DisableBase=((chkDisableBase.Checked ? "1" : "0") ); // //Disable Base // DeviceId -- index 44               
                    pda.DisableBreak=((chkDisableOnBreak.Checked ? "1" : "0") ); // //Disable OnBreak -- index 45
                    pda.DisableRejectJob=((chkDisableRejectJob.Checked ? "1" : "0")); // //Disable Reject Job

               
                    pda.DisableChangeDest=((chkDisableChangeDest.Checked ? "1" : "0"));



                    
                    
                    pda.ShowJobasAlert=((chkShowJobAsAlert.Checked ? "1" : "0") );
                    pda.DisableNoPickup=((chkDisableNoPickup.Checked ? "1" : "0") );
                    
                    
                    pda.DisableAlarm=((chkDisableAlarm.Checked ? "1" : "0") );
                    pda.ShowSpecialReqOnFront=((chkShowSpecReq.Checked ? "1" : "0") );



                    pda.DisableFareOnAccJob=((chkDisableFareOnAccJob.Checked ? "1" : "0"));
                    
                    
                    pda.DisableSTC=((chkDisableSTC.Checked ? "1" : "0") );




                    // version 10.0
                    
                    
                    pda.ShowAlertOnJobLate=((chkShowAlertOnJobLater.Checked ? "1" : "0") );
                    pda.EnableAutoRotate=((chkEnableAutoRotate.Checked ? "1" : "0") );
                    
                    
                    pda.ShowPlotOnOffer=((ShowPlotOnJobOffer.Checked ? "1" : "0") );




                    pda.OnBreakDur=((numBreakDuration.Value.ToStr()) );



                    pda.ManualFares=(manualFares );
                    
                    
                    pda.EnablePriceBid=((chkEnablePriceBidding.Checked ? "1" : "0"));

                    
                    
                    pda.DrvWaitingMins=(drvWaitingCharges.ToStr() );
                    pda.AccWaitingMins=(drvAccWaitingCharges.ToStr());



                    // need to comment
                    
                    
                    pda.DisableJobAuth = ((chkDisableJobAuth.Checked ? "1" : "0"));

                  
                       
                       string json= Newtonsoft.Json.JsonConvert.SerializeObject(pda);





                   
                       
                       contents.Append(json);
                    //fahad
                    General.SendMessageToPDA(contents.ToString(), pda.DrvId);

                }


                else
                {



                    
                    
                    contents.Append(AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim() + ",");
                    contents.Append(objMaster.Current.Id + ",");
                    
                    
                    contents.Append(objMaster.Current.DriverNo + ",");
                    contents.Append(objMaster.Current.DriverName + ",");
                    
                    
                    contents.Append(objMaster.Current.Fleet_VehicleType.VehicleType.ToStr().ToUpper() + ",");

                    
                    
                    contents.Append(" ,");

                    
                    
                    contents.Append("3,");  // GPS Interval
                    contents.Append((chkEnableJobExtraCharges.Checked ? "1" : "0") + ",");  // Extra Charges
                    
                    
                    contents.Append((chkShowCompletedJobs.Checked ? "1" : "0") + ","); // Show Completed Jobs

                    
                    
                    contents.Append((chkEnableBidding.Checked ? "1" : "0") + ","); // Enable Bidding




                    contents.Append((chkShowPlots.Checked ? "1" : "0") + ","); // Show Plots  -- index 10



                    contents.Append((chkShowNavigation.Checked ? "1" : "0") + ","); // Show Plots -- index 11




                    
                    
                    contents.Append((numJobTimeout.Value.ToStr()) + ","); // Show Plots -- index 12
                    contents.Append(("40") + ","); // Zone Update Interval -- index 13
                    
                    
                    contents.Append((chkShowSoundOnZoneChange.Checked ? "1" : "0") + ","); // Sound On Zone Change -- index 14
                    contents.Append((chkShowAlertOnJobLater.Checked ? "1" : "0") + ","); // Message Stay -- index 15




                    
                    
                    contents.Append((chkEnableCompanyCars.Checked ? "1" : "0") + ","); // Show Plots -- index 16
                    contents.Append((" ") + ","); // Show Plots -- index 17
                    
                    
                    contents.Append((chkEnableFareMeter.Checked ? "1" : "0") + ","); // Show Plots -- index 18
                    contents.Append((chkShowCustomerMobileNo.Checked ? "1" : "0") + ","); // Show Plots -- index 19
                    
                    
                    contents.Append((chkHidePickupAndDest.Checked ? "1" : "0") + ","); // Show Plots -- index 20
                    contents.Append((chkEnableLogoutOnReject.Checked ? "1" : "0") + ","); // Show Plots -- index 21
                    
                    
                    contents.Append((" ") + ","); // DeviceId -- index 22
                    contents.Append(("20") + ","); // DeviceId -- index 23
       






                    contents.Append(navigationApp + ","); // DeviceId -- index 24




                    
                    
                    contents.Append((chkEnableFlagDown.Checked ? "1" : "0") + ","); // -- index 25
                    contents.Append((chkMessageStay.Checked ? "1" : "0") + ","); // -- index 26



                    contents.Append((chkDisablePanic.Checked ? "1" : "0") + ","); // index 27
                    
                    
                    contents.Append((chkDisableRank.Checked ? "1" : "0") + ","); // index 28




                    contents.Append((chkVoiceOnClearMeter.Checked ? "1" : "0") + ",");



                    contents.Append((chkDisableChangeJobPlot.Checked ? "1" : "0") + ",");// index 30



                    contents.Append((chkEnableJ15Jobs.Checked ? "1" : "0") + ","); // index 31
                    
                    
                    contents.Append((chkEnableLogoutAuthorization.Checked ? "1" : "0") + ","); // index 32

                    
                    
                    contents.Append((chkIgnoreArriveAction.Checked ? "1" : "0") + ","); // index 33




                    contents.Append((txtBiddingMessage.Text.Trim() == string.Empty ? " " : txtBiddingMessage.Text.Trim()) + ","); // index 34




                    
                    
                    if (objMaster.Current.Fleet_Driver_PDASettings.Count > 0 && objMaster.Current.Fleet_Driver_PDASettings[0].CurrentPdaVersion >= 10)
                    {



                        if (chkEnableFareMeter.Checked)
                        
                        
                        {
                            string[] arr = null;



                            string splitChar = "#";
                            
                            
                            if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                            {



                                arr = General.GetQueryable<Fare_PDAMeter>(null).AsEnumerable()
                                
                                    
                                    .Where(c => c.FromStartTime != null && c.FromEndTime != null).Select(args => args.Fare.Fleet_VehicleType.VehicleType +
                                    splitChar + args.FromMile + splitChar + args.ToMile + splitChar + args.Rate + splitChar + args.Fare.Fleet_VehicleType.StartRate + "," + args.Fare.Fleet_VehicleType.StartRateValidMiles
                                    
                                    
                                    + splitChar + string.Format("{0:HH:mm}", args.FromStartTime).Strip(":") + splitChar + string.Format("{0:HH:mm}", args.TillStartTime).Strip(":") + splitChar + args.PeakTimeRate + splitChar + args.Fare.Fleet_VehicleType.FromStartRate + splitChar + args.Fare.Fleet_VehicleType.FromStartRateValidMiles
                                    + splitChar + string.Format("{0:HH:mm}", args.FromEndTime).Strip(":") + splitChar + string.Format("{0:HH:mm}", args.TillEndTime).Strip(":") + splitChar + args.OffPeakTimeRate + splitChar + args.Fare.Fleet_VehicleType.TillStartRate + splitChar + args.Fare.Fleet_VehicleType.TillStartRateValidMiles




                                    
                                    
                                    ).ToArray<string>();
                            }
                            else


                            {



                                arr = General.GetQueryable<Fare_PDAMeter>(null).Select(args => args.Fare.Fleet_VehicleType.VehicleType +
                                
                                    
                                    splitChar + args.FromMile + splitChar + args.ToMile + splitChar + args.Rate + splitChar + args.Fare.Fleet_VehicleType.StartRate + splitChar + args.Fare.Fleet_VehicleType.StartRateValidMiles

                                    
                                    
                                    ).ToArray<string>();

                            }



                            
                            
                            txtFareMessage.Text += "<<" + string.Join(">>", arr);
                        }
                        else


                        {
                            
                            
                            txtFareMessage.Text = " ";
                        }
                    }



                    
                    
                    contents.Append((txtFareMessage.Text.Trim() == string.Empty ? " " : txtFareMessage.Text.Trim()) + ","); // index 35




                    contents.Append((chkEnableOptionalMeter.Checked ? "1" : "0") + ","); // index 36
                    
                    
                    contents.Append((chkDisableMeterAccJob.Checked ? "1" : "0") + ",");// index 37
                    contents.Append("0" + ","); // index 38




                    
                    
                    contents.Append((chkShowFareonExtraCharges.Checked ? "1" : "0") + ","); // index 39
                    contents.Append((chkEnableCallCustomer.Checked ? "1" : "0") + ","); // index 40




                    
                    
                    contents.Append((chkEnableRecoverJob.Checked ? "1" : "0") + ",");// index 41

                    
                    
                    contents.Append((chkEnableMeterWaitingCharges.Checked ? "1" : "0") + ","); // index 42




                    // Version 7.2
                    
                    
                    // Shift Logout
                    contents.Append((chkShiftOverLogout.Checked ? "1" : "0") + ","); // // Shift Logout







                  
                    
                    
                    contents.Append((chkDisableBase.Checked ? "1" : "0") + ","); // //Disable Base // DeviceId -- index 44
                 
                    
                    
                    contents.Append((chkDisableOnBreak.Checked ? "1" : "0") + ","); // //Disable OnBreak -- index 45
                     contents.Append((chkDisableRejectJob.Checked ? "1" : "0") + ","); // //Disable Reject Job



               
                    
                    
                    contents.Append((chkDisableChangeDest.Checked ? "1" : "0") + ",");




                    contents.Append((chkShowJobAsAlert.Checked ? "1" : "0") + ",");
                    
                    
                    contents.Append((chkDisableNoPickup.Checked ? "1" : "0") + ",");
                    contents.Append((chkDisableAlarm.Checked ? "1" : "0") + ",");
                    
                    
                    contents.Append((chkShowSpecReq.Checked ? "1" : "0") + ",");

                    
                    
                    contents.Append((chkDisableFareOnAccJob.Checked ? "1" : "0") + ",");
                    contents.Append((chkDisableSTC.Checked ? "1" : "0") + ",");




                    
                    
                    // version 10.0
                    contents.Append((chkShowAlertOnJobLater.Checked ? "1" : "0") + ",");
                    
                    
                    contents.Append((chkEnableAutoRotate.Checked ? "1" : "0") + ",");
                    contents.Append((ShowPlotOnJobOffer.Checked ? "1" : "0") + ",");




                    
                    
                    contents.Append((numBreakDuration.Value.ToStr()) + ",");
                   



                    contents.Append(manualFares + ",");
                    
                    
                    contents.Append((chkEnablePriceBidding.Checked ? "1" : "0") + ",");
                   



                    contents.Append(drvWaitingCharges + ",");
                    
                    
                    contents.Append(drvAccWaitingCharges);
                }



                //



                new Thread(delegate ()


                    {
                        General.SendMessageToPDA("request pda=" + 0 + "=" + objMaster.Current.Id + "=" + contents + "=12=" + objMaster.Current.DriverNo);
                    }).Start();
                //fahad








                System.Threading.Thread.Sleep(2000);

                
                
                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);





                
                
                Fleet_Driver_PDASetting objPDA = null;

                
                
                if (objMaster.Current.Fleet_Driver_PDASettings.Count == 0)
                    objMaster.Current.Fleet_Driver_PDASettings.Add(new Fleet_Driver_PDASetting());







                objPDA = objMaster.Current.Fleet_Driver_PDASettings[0];







                objPDA.ShowPlotOnJobOffer = ShowPlotOnJobOffer.Checked;
                
                
                objPDA.DriverId = objMaster.Current.Id;
                objPDA.ShowFaresOnExtraCharges = chkShowFareonExtraCharges.Checked;



                objPDA.EnableJobExtraCharges = chkEnableJobExtraCharges.Checked;
                
                
                objPDA.EnableFareMeterWaitingCharges = chkEnableMeterWaitingCharges.Checked;
                objPDA.EnableRecoverJob = chkEnableRecoverJob.Checked;
                
                
                objPDA.EnableCallCustomer = chkEnableCallCustomer.Checked;
                objPDA.EnableBidding = chkEnableBidding.Checked;
         
       

                
                
                
                objPDA.EnableAutoRotateScreen = chkEnableAutoRotate.Checked;
                objPDA.EnableFareMeter = chkEnableFareMeter.Checked;
                


                
                
                
                objPDA.EnableFlagDown = chkEnableFlagDown.Checked;
                objPDA.EnableJ15J30Jobs = chkEnableJ15Jobs.Checked;
                


                
                
                
                objPDA.EnableLogoutAuthorization = chkEnableLogoutAuthorization.Checked;
                objPDA.DisableChangeJobPlots = chkDisableChangeJobPlot.Checked;
                


                
                
                
                objPDA.BreakTime = numBreakDuration.Value.ToInt();
                objPDA.DisableDriverRank = chkDisableRank.Checked;
                


                
                
                
                objPDA.DisablePanicButton = chkDisablePanic.Checked;
                objPDA.DisableFareMeterOnAccJob = chkDisableMeterAccJob.Checked;








                
                
                objPDA.NavigationApp = navigationApp.ToInt();
                objPDA.MessageStayOnScreen = chkMessageStay.Checked;
                
                
                objPDA.ShowCompletedJob = chkShowCompletedJobs.Checked;
                objPDA.ShowCustomerMobileNo = chkShowCustomerMobileNo.Checked;
                
                
                objPDA.ShowNavigation = chkShowNavigation.Checked;
                objPDA.ShowPlots = chkShowPlots.Checked;




                
                
                objPDA.FareMeterType = txtFareMessage.Text.Trim();
                objPDA.BiddingType = txtBiddingMessage.Text.Trim();



                objPDA.JobTimeOutInterval = numJobTimeout.Value.ToInt();
                
                
                objPDA.NotifyOnZoneChange = chkShowSoundOnZoneChange.Checked;

                
                
                objPDA.HasCompanyCars = chkEnableCompanyCars.Checked;
                objPDA.LogoutOnRejectJob = chkEnableLogoutOnReject.Checked;
                
                
                objPDA.IgnoreArriveAction = chkIgnoreArriveAction.Checked;
                objPDA.GPSInterval = 3;
                
                
                objPDA.HidePickAndDestination = chkHidePickupAndDest.Checked;


                //if (objPDA.HidePickAndDestination.ToBool())
                //{
                //    if (ddlHidePickupAndDestinationType.SelectedIndex == 1)
                //        objPDA.OldPdaVersion = 2;
                //    if (ddlHidePickupAndDestinationType.SelectedIndex == 2)
                //        objPDA.OldPdaVersion = 3;


                //}
                //else
                //    objPDA.OldPdaVersion = 0;



                if (objPDA.HidePickAndDestination.ToBool())
                {

                    if (ddlHidePickupAndDestinationType.SelectedIndex == 0)
                        objPDA.OldPdaVersion = 1;

                    if (ddlHidePickupAndDestinationType.SelectedIndex == 1)
                        objPDA.OldPdaVersion = 2;
                    if (ddlHidePickupAndDestinationType.SelectedIndex == 2)
                        objPDA.OldPdaVersion = 3;

                    if (ddlHidePickupAndDestinationType.SelectedIndex == 3)
                        objPDA.OldPdaVersion = 4;


                }
                else
                    objPDA.OldPdaVersion = 0;


                objPDA.LogoutOnOverShift = chkShiftOverLogout.Checked;
                
                
                objPDA.NotifyOnJobLate = chkShowAlertOnJobLater.Checked;
                objPDA.EnableAutoRotateScreen = chkEnableAutoRotate.Checked;
                
                
                objPDA.OptionalFareMeter = chkEnableOptionalMeter.Checked;
                objPDA.DisableChangeJobPlots = chkDisableChangeJobPlot.Checked;
                
                
                objPDA.DisableOnBreak = chkDisableOnBreak.Checked;
                objPDA.DisableBase = chkDisableBase.Checked;



                objPDA.DisableRejectJob = chkDisableRejectJob.Checked;
                
                
                objPDA.DisableChangeDestination = chkDisableChangeDest.Checked;
                objPDA.DisableRejectJobAuth = chkDisableJobAuth.Checked;




                
                
                objPDA.DisableFareOnAccJob = chkDisableFareOnAccJob.Checked;
                objPDA.DisableSTC = chkDisableSTC.Checked;
                
                
                objPDA.DisableSetAlarm = chkDisableAlarm.Checked;
                objPDA.DisableNoPickup = chkDisableNoPickup.Checked;
                
                
                objPDA.ShowJobAsAlert = chkShowJobAsAlert.Checked;
                objPDA.ShowSpecReqOnFront = chkShowSpecReq.Checked;




                
                
                objPDA.EnablePriceBidding = chkEnablePriceBidding.Checked;
                objPDA.EnableManualFares = chkEnableManualFares.Checked;
                
                
                objPDA.EnableOptionalManualFares = chkEnableOptionalManualFares.Checked;
                objPDA.EnableFareMeterVoice = chkVoiceOnClearMeter.Checked;
                
                
                objMaster.Save();

                var query = General.GetObject<Fleet_Driver_PDASetting>(c => c.DriverId == objMaster.Current.Id);

                if (query.HasCompanyCars == true)
                {
                    //radpageview1.SelectedPage = radpageview1.Pages[9];
                    CompanyVehicle.Item.Visibility = ElementVisibility.Visible;
                    //radpageview1.Pages.Add(CompanyVehicle);
                }
                else
                {
                    CompanyVehicle.Item.Visibility = ElementVisibility.Hidden;
                    //radpageview1.Pages.Remove(radpageview1.Pages[9]);
                   // radpageview1.Pages.Remove(CompanyVehicle);
                }
                
                btnUpdateSettings.Enabled = true;

                
                
                //,"3","0","1","0","1","1","60","40","1","1","0","","0","1",
                //            "0","0",deviceId,"20","1","0","1","1","0","1","0","1","0","0","not specified","not specified",
                
                
                //            "0","0","0","0","0","0","0");	

                
                
                // contents.Append("," + DriverString.Replace(",", "<<")); // Show Plots
            }
            
             catch (Exception ex)
            {



                btnUpdateSettings.Enabled = true;
            }


        }


        private void radpageview1_SelectedPageChanging(object sender, RadPageViewCancelEventArgs e)
        {
            //if (e.Page == Pg_Attributes && Pg_Attributes.Tag == null)
            //{
            //    Pg_Attributes.Tag = true;
            //    FormatDriverAttributesGrid();
            //    LoadVehicleAttributesGrid();




            //    if (objMaster != null && objMaster.Current != null && objMaster.PrimaryKeyValue != null)
            //    {
            //        string[] attributeValuesArr = objMaster.Current.AttributeValues.ToStr().Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //        foreach (var item in attributeValuesArr)
            //        {
            //            var attrRow = grdDriverAttributes.Rows.FirstOrDefault(c => c.Cells[Col_DriverAttributes.ShortName].Value.ToStr().ToLower().Trim() == item.ToLower().Trim());

            //            if (attrRow != null)
            //            {
            //                attrRow.Cells[Col_DriverAttributes.Default].Value = true;
            //            }
            //        }
            //        grdDriverAttributes.AllowEditRow = true;

            //    }
            //}
            //else if (e.Page == pg_charges && Pg_Attributes.Tag == null)
            //{
            //    Pg_Attributes.Tag = true;

            //    DisplayCompanyVehicle();

            //}
            //else if (e.Page == Pg_notes)
            //{

            //    DisplayComplaint();

            //}

        }


        private void DisplayComplaint()
        {
            try
            {

                int? Id = objMaster.Current.Id;
                var list = (from a in General.GetQueryable<Complaint>(c => c.DriverId == Id)
                            orderby a.ComplainDateTime descending
                            select new
                            {
                                Id = a.Id,
                                RefNo = a.RefNo,
                                JobRefNo = a.Booking.BookingNo,
                                ComplainDate = a.ComplainDateTime,
                                IncidentDate = a.IncidentDateTime,
                                CustomerName = a.CustomerName,
                                ComplainDescription = a.ComplainDescription,
                                ResultDescription = a.ResultDescription
                            }).ToList();
                if (list.Count() > 0)
                {
                    grdDriverComplaints.DataSource = list;



                    //AddComplaintButton();
                }

                //    RateDriver.CurrentRating = objMaster.Current.AvgRating.ToInt();

            }
            catch
            {


            }

        }
     
        private void radpageview1_PageIndexChanged(object sender, RadPageViewIndexChangedEventArgs e)
        
        
        {
            

        }



        private void chkEnableFareMeter_CheckedChanged(object sender, EventArgs e)
        
        
        {

        
            
            SetFareMeterState();




        }



        private void SetFareMeterState()
        
        
        {

        
            
            if (chkEnableFareMeter.Checked)
            {
            
                
                chkEnableOptionalMeter.Enabled = true;
                chkEnableMeterWaitingCharges.Enabled = true;
                
                
                chkDisableMeterAccJob.Enabled = true;
                chkVoiceOnClearMeter.Enabled = true;



                chkEnableManualFares.Checked = false;
                
                
                chkEnableOptionalManualFares.Checked = false;
                chkEnableManualFares.Enabled = false;
                
                
                chkEnableOptionalManualFares.Enabled = false;              
            }
            else


            {
                
                
                chkEnableOptionalMeter.Enabled = false;
                chkEnableMeterWaitingCharges.Enabled = false;
                
                
                chkDisableMeterAccJob.Enabled = false;
                chkVoiceOnClearMeter.Enabled = false;



                chkEnableOptionalMeter.Checked = false;
                
                
                chkEnableMeterWaitingCharges.Checked = false;
                chkDisableMeterAccJob.Checked = false;
                
                
                chkVoiceOnClearMeter.Checked = false;

              




                
                
                chkEnableManualFares.Enabled = true;
                
                


                chkEnableOptionalManualFares.Enabled = true;
        







            }







        }















        
        
        private void FormatRangeWiseCommissionGrid()
        {








            
            
            grdRangeWiseComm.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            
            
            col.IsVisible = false;
            col.Name = "ID";
            
            
            grdRangeWiseComm.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            
            
            col.IsVisible = false;
            col.Name = "MASTERID";
            
            
            grdRangeWiseComm.Columns.Add(col);

            
            
            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.HeaderText = "From";
            
            
            colD.Width = 70;
            colD.ReadOnly = false;
            
            
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            
            
            colD.Maximum = 100000;
            colD.Name = "FROMPRICE";
            
            
            grdRangeWiseComm.Columns.Add(colD);




            colD = new GridViewDecimalColumn();
            
            
            colD.HeaderText = "Till";
            colD.Width = 70;
            
            
            colD.ReadOnly = false;
            colD.DecimalPlaces = 2;
            
            
            colD.Minimum = 0;
            colD.Maximum = 100000;
            
            
            colD.Name = "TOPRICE";
            grdRangeWiseComm.Columns.Add(colD);







            colD = new GridViewDecimalColumn();
            
            
            colD.HeaderText = "Commission %";
            colD.Width = 90;
            
            
            colD.ReadOnly = false;
            colD.DecimalPlaces = 0;
            
            
            colD.Minimum = 0;
            colD.Maximum = 100;
            
            
            colD.Name = "COMMISSIONPERCENT";
            grdRangeWiseComm.Columns.Add(colD);







            UI.GridFunctions.AddDeleteColumn(grdRangeWiseComm);



            //   grdSurchargeRates.ShowGroupPanel = false;
            
            
            grdRangeWiseComm.AddNewRowPosition = SystemRowPosition.Bottom;





            
            
            GridViewRowInfo row = null;
            foreach (var item in General.GetQueryable<Gen_SysPolicy_CommissionPriceRange>(c => c.SysPolicyId != null).ToList())
            
            
            {
                row = grdRangeWiseComm.Rows.AddNew();




                
                
                row.Cells["FROMPRICE"].Value = item.FromPrice.ToDecimal();
                row.Cells["TOPRICE"].Value = item.ToPrice.ToDecimal();
                
                
                row.Cells["COMMISSIONPERCENT"].Value = item.CommissionValue.ToDecimal();




            }




            
            
            grdRangeWiseComm.CurrentRow = null;




        }



        private void btnAdd_Click(object sender, EventArgs e)
        
        
        {
            try
        
            
            {

            
                
                string error = string.Empty;

                
                
                string Notes = txtNotes.Text.Trim();
               
                
                
                // DriverId = objMaster.Current.Id;




                if (string.IsNullOrEmpty(Notes))
                
                
                {
                    error = "Required : Notes";
                
                
                }




                //if (DriverId == 0)
                
                
                //{ 
                //    error+=Environment.NewLine+"Required Driver";
                
                
                //}




                if (!string.IsNullOrEmpty(error))
                
                
                {
                    ENUtils.ShowMessage(error);
                
                    
                    return;
                }
                //if               
              //  (grdDriverNotes.Rows.Count(c => c.Cells["Notes"].Value.ToStr() == Notes) > 0)
                //{



                //    ENUtils.ShowMessage("Note already exists");
                
                
                //    return;

                
                
                //}
                if (IsNotesEdit == true && grdLister.CurrentRow != null)
                
                
                {
                    grdLister.CurrentRow.Delete();
                
                
                }




                DateTime dt = DateTime.Now;
                
                
                GridViewRowInfo row = grdLister.Rows.AddNew();




                row.Cells[COL_NOTES.Notes].Value = Notes;
                
                
                row.Cells[COL_NOTES.Time].Value = dt; //string.Format("{0: HH:mm}",DateTime.Now);
                row.Cells[COL_NOTES.AddBy].Value = AppVars.LoginObj.LoginName;
                
                
                row.Cells[COL_NOTES.AddOn].Value = dt.ToDate();
                row.Cells[COL_NOTES.DateTime].Value = DateTime.Now.ToDateTime();



                IsNotesEdit = false;
                
                
                txtNotes.Text = "";
                txtNotes.Focus();
            }


            catch (Exception ex)
            {


                ENUtils.ShowMessage(ex.Message);
            }


        }



        private void btnNew_Click(object sender, EventArgs e)
        
        
        {
            txtNotes.Text = "";
        
            
            txtNotes.Focus();
            //grdDriverNotes.Rows.Clear();
        }



        
        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
        
            
            LoadDocument("");
        }



        private void btnView_Click(object sender, EventArgs e)
        
        
        {
            ViewDocument(txtLogBookDocPath.Text.Trim());
        
        
        }


        private void LoadVehicleAttributesGrid()
        {
            try
            {
         
                var list = General.GetQueryable<Gen_Attribute>(null).OrderBy(c => c.Name).ToList();

                foreach (var item in list)
                {


                    var row = grdDriverAttributes.Rows.AddNew();


                    row.Cells[Col_DriverAttributes.Id].Value = item.Id;
                    row.Cells[Col_DriverAttributes.Default].Value = item.IsDefault;
                    row.Cells[Col_DriverAttributes.Name].Value = item.Name + "[" + item.ShortName + "]";
                    row.Cells[Col_DriverAttributes.ShortName].Value = item.ShortName;
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


        private void LoadDriverShifts()
        {
            try
            {

                var list = General.GetQueryable<Driver_Shift>(null).OrderBy(c => c.ShiftName).ToList();

                foreach (var item in list)
                {


                    var row = grdDriverShifts.Rows.AddNew();


                    row.Cells[COL_SHIFT.SHIFT_ID].Value = item.Id;
                   // row.Cells[COL_SHIFT.MASTERID].Value = item.;
                    row.Cells[COL_SHIFT.SHIFT].Value = item.ShiftName;

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


        private void btnClear_Click(object sender, EventArgs e)
        {
        
            
            ClearDocument("");
        }



        private void frmDriver_Load_1(object sender, EventArgs e)
        
        
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



                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    //   CancelWebClientAsync();
                    // wc.CancelAsync();
                    aTxt.Values = null;

                }
                text = text.ToLower();

                if (AppVars.keyLocations.Contains(text) || (text.Length <= 4 && (text.EndsWith("  ") || (text[1] == ' ' || (text.Length > 2 && char.IsLetter(text[1]) && text[2] == ' ' && text.Trim().WordCount() == 2))))
                   || (text.Length < 13 && text.WordCount() == 2 && text.Remove(text.IndexOf(' ')).Trim().Length <= 3 && text.Strip(' ').IsAlpha()))
                {


                    aTxt.ListBoxElement.Items.Clear();


                    string[] res = null;

                    if (text.EndsWith("  "))
                    {

                        text = text.Trim();

                        res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey.StartsWith(text))
                               select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                ).ToArray<string>();


                    }
                    else
                    {
                        if (text.Contains(' ') && text.Substring(text.IndexOf(' ')).Trim().Length > 1)
                        {
                            string shortcut = text.Remove(text.IndexOf(' ')).Trim();

                            string locName = text.Substring(text.IndexOf(' ')).Trim().ToLower();

                            res = (from a in General.GetQueryable<Gen_Location>(c => c.Gen_LocationType.ShortCutKey == shortcut &&
                                        c.LocationName.ToLower().Contains(locName))
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                      ).ToArray<string>();

                        }
                        else
                        {


                            res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey == text)
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                       ).ToArray<string>();
                        }
                    }


                    if (res.Count() > 0)
                    {
                     


                        var finalList = (from a in AppVars.zonesList
                                         from b in res
                                         where b.Contains(a)
                                         select b).ToArray<string>();


                        if (finalList.Count() > 0)
                            finalList = finalList.Union(res).ToArray<string>();

                        else
                            finalList = res;


                        aTxt.ListBoxElement.Items.AddRange(finalList);


                        aTxt.ShowListBox();
                    }


                    if (aTxt.Text != aTxt.FormerValue)
                    {
                        aTxt.FormerValue = aTxt.Text;
                    }
                }


                if (MapType == Enums.MAP_TYPE.NONE) return;

                StartAddressTimer(text);

            }
            else if (text.Length > 0)
            {
                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    //   CancelWebClientAsync();
                    // wc.CancelAsync();
                    aTxt.Values = null;

                }
                text = text.ToLower();

                if (AppVars.keyLocations.Contains(text))
                {

                    aTxt.ListBoxElement.Items.Clear();


                    string[] res = null;

                    if (text.EndsWith("  "))
                    {

                        text = text.Trim();

                        res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey.ToLower() == text)
                               select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                ).ToArray<string>();


                    }
                    else
                    {
                        if (text.Contains(' ') && text.Substring(text.IndexOf(' ')).Trim().Length > 1)
                        {
                            string shortcut = text.Remove(text.IndexOf(' ')).Trim();

                            string locName = text.Substring(text.IndexOf(' ')).Trim().ToLower();

                            res = (from a in General.GetQueryable<Gen_Location>(c => c.Gen_LocationType.ShortCutKey == shortcut &&
                                        c.LocationName.ToLower().Contains(locName))
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                      ).ToArray<string>();

                        }
                        else
                        {


                            res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey == text)
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                       ).ToArray<string>();
                        }
                    }


                    if (res.Count() > 0)
                    {
                     


                        var finalList = (from a in AppVars.zonesList
                                         from b in res
                                         where b.Contains(a)
                                         select b).ToArray<string>();


                        if (finalList.Count() > 0)
                            finalList = finalList.Union(res).ToArray<string>();

                        else
                            finalList = res;


                        aTxt.ListBoxElement.Items.AddRange(finalList);

                        if (text == "." && finalList.Count() == 1)
                        {
                            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                            aTxt.Text = finalList[0];
                            aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                           
                        }
                        else
                        {

                            aTxt.ShowListBox();
                        }
                    }


                    if (aTxt.Text != aTxt.FormerValue)
                    {
                        aTxt.FormerValue = aTxt.Text;
                    }




                    StartAddressTimer(text);
                }


            }
            else
            {
                if (MapType == Enums.MAP_TYPE.NONE) return;
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

                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    aTxt.Values = null;

                }

                text = text.ToLower();

                


                if (MapType == Enums.MAP_TYPE.NONE) return;

                StartAddressTimer(text);

            }
            else if (text.Length > 0)
            {
                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    aTxt.Values = null;

                }
                text = text.ToLower();

                if (AppVars.keyLocations.Contains(text))
                {

                    aTxt.ListBoxElement.Items.Clear();


                    string[] res = null;

                    if (text.EndsWith("  "))
                    {

                        text = text.Trim();

                        res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey.ToLower() == text)
                               select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                ).ToArray<string>();


                    }
                    else
                    {
                        if (text.Contains(' ') && text.Substring(text.IndexOf(' ')).Trim().Length > 1)
                        {
                            string shortcut = text.Remove(text.IndexOf(' ')).Trim();

                            string locName = text.Substring(text.IndexOf(' ')).Trim().ToLower();

                            res = (from a in General.GetQueryable<Gen_Location>(c => c.Gen_LocationType.ShortCutKey == shortcut &&
                                        c.LocationName.ToLower().Contains(locName))
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                      ).ToArray<string>();

                        }
                        else
                        {


                            res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey == text)
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                       ).ToArray<string>();
                        }
                    }


                    if (res.Count() > 0)
                    {
                      //  IsKeyword = true;


                        var finalList = (from a in AppVars.zonesList
                                         from b in res
                                         where b.Contains(a)
                                         select b).ToArray<string>();


                        if (finalList.Count() > 0)
                            finalList = finalList.Union(res).ToArray<string>();

                        else
                            finalList = res;


                        aTxt.ListBoxElement.Items.AddRange(finalList);

                        if (text == "." && finalList.Count() == 1)
                        {
                            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                            aTxt.Text = finalList[0];
                            aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                         
                        }
                        else
                        {

                            aTxt.ShowListBox();
                        }
                    }


                    if (aTxt.Text != aTxt.FormerValue)
                    {
                        aTxt.FormerValue = aTxt.Text;
                    }

                    StartAddressTimer(text);
                }


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

        private void mnuDriverLog_Click(object sender, EventArgs e)
        {
            if (objMaster.Current != null)
            {
                rptfrmDriverLog frm = new rptfrmDriverLog(objMaster.Current.Id);
                frm.ShowDialog();
                frm.Dispose();
            }
        }

        private void DrirverRating_Click(object sender, EventArgs e)
        {
            //if (objMaster.Current != null)
            //{
            //    rptfrmDriverRating frm = new rptfrmDriverRating(objMaster.Current.Id);
            //    frm.ShowDialog();
            //    frm.Dispose();
            //}
        }

     
    }







}