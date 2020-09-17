using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using Taxi_AppMain.Classes;
using Taxi_BLL;
using Taxi_Model;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using UI;
using Utils;
namespace Taxi_AppMain
{
    public partial class frmCompany : UI.SetupBase
    {
        public struct COLS
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string DepartmentName = "DepartmentName";
            public static string Pickup = "Pickup";
            public static string DropOff = "DropOff";
        }


        public struct COLS_CC
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string CostCenterName = "CostCenterName";
        }



        public struct COLS_Contacts
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string ContactName = "ContactName";
            public static string Email = "Email";

            public static string Password = "Password";
            public static string TelephoneNo = "TelephoneNo";

            public static string MobileNo = "MobileNo";
            public static string Primary = "Primary";

        }
        public struct COLS_Adress
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Address = "Address";
        }
        public struct COLS_FixedFare
        {
            public static string Id = "Id";
            public static string FareId = "FareId";
            public static string MasterId = "MasterId";
            public static string VehicleType = "VehicleType";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string FromAddress = "FromAddress";
            public static string ToAddress = "ToAddress";
            public static string Rate = "Rate";
            public static string FromLocationTypeId = "FromLocationTypeId";
            public static string ToLocationTypeId = "ToLocationTypeId";

        }
        public struct COLS_MileageSetting
        {
            public static string Id = "Id";
            public static string FareId = "FareId";
            public static string MasterId = "MasterId";
            public static string VehicleType = "VehicleType";
            public static string FromMile = "FromMile";
            public static string ToMile = "ToMile";
            public static string Rate = "Rate";
        }

        public struct COLS_WebLogin
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string UserName = "UserName";
            public static string LoginId = "LoginId";

            public static string Password = "Password";
            public static string TelephoneNo = "TelephoneNo";

            public static string MobileNo = "MobileNo";
            public static string IsActive = "IsActive";
            public static string AccountNo = "AccountNo";

        }
        public struct COLS_BookedBy
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string BookedBy = "BookedBy";
            public static string Email = "Email";
        }
        public struct COLS_AirportCommission
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Location = "Location";
            public static string LocationId = "LocationId";
            public static string LocationTypeId = "LocationTypeId";
            public static string CommissionPercent = "CommissionPercent";
            public static string CommissionOnPercent = "CommissionOnPercent";
            public static string CommissionAmount = "CommissionAmount";
            public static string DayWise = "DayWise";
            public static string NightWise = "NightWise";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string VehicleType = "VehicleType";
            public static string CustomerPrice = "CustomerPrice";
            public static string DriverPrice = "DriverPrice";
            public static string CompanyPrice = "CompanyPrice";
            public static string FareId = "FareId";

        }
        public struct COLS_PaymentTypes
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string PaymentTypeId = "PaymentTypeId";
            public static string PaymentType = "PaymentType";
            public static string ChargesName = "ChargesName";
            public static string Check = "Check";
        }
        private void FormatPaymentTypeGrid()
        {

            grdPaymentTypes.ShowRowHeaderColumn = false;
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS_PaymentTypes.Check;
            cbcol.HeaderText = "";
            cbcol.Width = 30;
            grdPaymentTypes.Columns.Add(cbcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.Id;
            col.IsVisible = false;
            grdPaymentTypes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.MasterId;
            col.IsVisible = false;
            grdPaymentTypes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.PaymentTypeId;
            col.IsVisible = false;
            grdPaymentTypes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.PaymentType;
            col.HeaderText = "Payment Type";
            col.ReadOnly = true;
            col.Width = 120;
            grdPaymentTypes.Columns.Add(col);
        }



        private void FormatAdditionalChargesGrid()
        {

            grdAdditionalCharges.ShowRowHeaderColumn = false;
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS_PaymentTypes.Check;
            cbcol.HeaderText = "";
            cbcol.Width = 30;
            grdAdditionalCharges.Columns.Add(cbcol);

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.Id;
            col.IsVisible = false;
            grdAdditionalCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.MasterId;
            col.IsVisible = false;
            grdAdditionalCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.PaymentTypeId;
            col.IsVisible = false;
            grdAdditionalCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_PaymentTypes.ChargesName;
            col.HeaderText = "Charges";
            col.ReadOnly = true;
            col.Width = 120;
            grdAdditionalCharges.Columns.Add(col);
        }


        CompanyBO objMaster;

        private bool HasAdditonalCharges = false;
        public frmCompany()
        {
            InitializeComponent();

            InitializeConstructor();
        }







        private int CopiedCompanyId;

        public frmCompany(int copyCompanyId)
        {
            InitializeComponent();
            InitializeConstructor();
            CopiedCompanyId = copyCompanyId;
            btNpASTE.Visible = true;
        }


        private void InitializeConstructor()
        {
            objMaster = new CompanyBO();
            this.SetProperties((INavigation)objMaster);
            this.Shown += new EventHandler(frmCompany_Shown);

            FormatDepartmentGrid();
            InitializeCompanyAddress();
            FormatAdressGrid();

            FormatCostCenterGrid();
            FormatContactsrGrid();

            FormatWebLoginGrid();

            FormatOrderNoGrid();

            FormatPaymentTypeGrid();
            LoadPaymentTypes();


            if (AppVars.listUserRights.Count(c => c.formName == "frmCompany" && c.functionId == "SHOW ADDITIONAL CHARGES") > 0)
            {

                HasAdditonalCharges = true;

                FormatAdditionalChargesGrid();
                LoadAdditionalCharges();


            }

            if (AppVars.listUserRights.Count(c => c.formName == "frmFares" && c.functionId == "SHOW AIRPORT FARES") > 0)
            {
                InitializeAirportCommission();
                FormatAirportCommissionGrid();
            }
            else
            {
                this.pageAirportCommission.Item.Visibility = ElementVisibility.Collapsed;
            }

            if (AppVars.listUserRights.Count(c => c.formName == "frmCompany" && c.functionId == "SHOW FARES TAB") > 0)
            {
                InitializeFixedFares();
                FormatFixedFareGrid();

                InitializeMilageSettings();
                FormatMilageSettingGrid();


            }
            else
            {
                this.pageFixedFares.Item.Visibility = ElementVisibility.Collapsed;
                this.pageMileageSettings.Item.Visibility = ElementVisibility.Collapsed;
                //this.Fixed_Fares.Item.Visibility = ElementVisibility.Collapsed;
                //this.Mileage_Settings.Item.Visibility = ElementVisibility.Collapsed;
            }

            if (AppVars.listUserRights.Count(c => c.formName == "frmCompany" && c.functionId == "SHOW BOOKEDBY TAB") > 0)
            {
                InitializeBookedBy();
                FormatBookedByGrid();
            }
            else
            {
                this.BookedBy.Item.Visibility = ElementVisibility.Collapsed;
            }
            radPageViewPage2.Item.Visibility = ElementVisibility.Collapsed;



            grdDepartment.CommandCellClick += new CommandCellClickEventHandler(grdDeparments_CommandCellClick);
            grdDepartment.RowsChanging += new GridViewCollectionChangingEventHandler(grdDeparments_RowsChanging);
            grdDepartment.ViewCellFormatting += new CellFormattingEventHandler(grdDeparments_ViewCellFormatting);




            grdCostCenter.CommandCellClick += new CommandCellClickEventHandler(grdCostCenter_CommandCellClick);
            grdCostCenter.RowsChanging += new GridViewCollectionChangingEventHandler(grdCostCenter_RowsChanging);
            grdCostCenter.ViewCellFormatting += new CellFormattingEventHandler(grdDeparments_ViewCellFormatting);


            grdContacts.CommandCellClick += new CommandCellClickEventHandler(grdContacts_CommandCellClick);
            grdContacts.RowsChanging += new GridViewCollectionChangingEventHandler(grdContacts_RowsChanging);
            grdContacts.ViewCellFormatting += new CellFormattingEventHandler(grdDeparments_ViewCellFormatting);



            grdWebLogin.CommandCellClick += new CommandCellClickEventHandler(grdWebLogin_CommandCellClick);
            grdWebLogin.RowsChanging += new GridViewCollectionChangingEventHandler(grdWebLogin_RowsChanging);
            grdWebLogin.ViewCellFormatting += new CellFormattingEventHandler(grdDeparments_ViewCellFormatting);

            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);


            grdOrderNo.CommandCellClick += new CommandCellClickEventHandler(grdOrderNo_CommandCellClick);

            this.btnSaveAndClose1.Click += new EventHandler(btnSaveAndClose1_Click);








            ComboFunctions.FillAccountTypeCombo(ddlAccountType);
            ComboFunctions.FillCompanyGroupCombo(ddlCompanyGroup);
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);


            OnNew();
        }




        private void LoadPaymentTypes()
        {
            var list = (from a in General.GetQueryable<Gen_PaymentType>(c => c.IsVisible == true)
                        select new
                        {
                            Id = a.Id,
                            PaymentType = a.PaymentType
                        }).ToList();
            int cnt = list.Count;
            grdPaymentTypes.RowCount = cnt;
            for (int i = 0; i < cnt; i++)
            {
                grdPaymentTypes.Rows[i].Cells[COLS_PaymentTypes.PaymentTypeId].Value = list[i].Id;
                grdPaymentTypes.Rows[i].Cells[COLS_PaymentTypes.PaymentType].Value = list[i].PaymentType;
            }

        }

        private void LoadAdditionalCharges()
        {
            var list = General.GetQueryable<Gen_Charge>(c => c.IsVisible == true).ToList();

                       
            int cnt = list.Count;
            grdAdditionalCharges.RowCount = cnt;
            for (int i = 0; i < cnt; i++)
            {
                grdAdditionalCharges.Rows[i].Cells[COLS_PaymentTypes.PaymentTypeId].Value = list[i].Id;
                grdAdditionalCharges.Rows[i].Cells[COLS_PaymentTypes.ChargesName].Value = list[i].ChargesName;
            }

        }

        void btnSaveAndClose1_Click(object sender, EventArgs e)
        {
            bool IsSave = OnSave();
            if (IsSave)
            {
                this.Close();
            }
        }
        private UI.MyGridView grdMileageSetting;
        private Telerik.WinControls.UI.RadLabel radLabel34;
        private Telerik.WinControls.UI.RadButton btnAddMileage;
        private void InitializeMilageSettings()
        {
            try
            {
                this.grdMileageSetting = new UI.MyGridView();
                this.radLabel34 = new Telerik.WinControls.UI.RadLabel();
                this.btnAddMileage = new Telerik.WinControls.UI.RadButton();

                this.pageMileageSettings.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.grdMileageSetting)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdMileageSetting.MasterTemplate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel34)).BeginInit();

                this.pageMileageSettings.Controls.Add(this.grdMileageSetting);
                this.pageMileageSettings.Controls.Add(this.radLabel34);
                // 
                // grdMileageSetting
                // 
                this.grdMileageSetting.AutoCellFormatting = false;
                this.grdMileageSetting.Dock = System.Windows.Forms.DockStyle.Fill;
                this.grdMileageSetting.EnableCheckInCheckOut = false;
                this.grdMileageSetting.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdMileageSetting.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdMileageSetting.Location = new System.Drawing.Point(0, 23);
                // 
                // 
                // 
                this.grdMileageSetting.MasterTemplate.AllowAddNewRow = false;
                this.grdMileageSetting.Name = "grdMileageSetting";
                this.grdMileageSetting.PKFieldColumnName = "";
                this.grdMileageSetting.ShowGroupPanel = false;
                this.grdMileageSetting.ShowImageOnActionButton = true;
                this.grdMileageSetting.Size = new System.Drawing.Size(934, 608);
                this.grdMileageSetting.TabIndex = 70;
                this.grdMileageSetting.Text = "myGridView1";
                // 
                // radLabel34
                // 
                this.radLabel34.AutoSize = false;
                this.radLabel34.BackColor = System.Drawing.Color.Crimson;
                this.radLabel34.Controls.Add(this.btnAddMileage);
                this.radLabel34.Dock = System.Windows.Forms.DockStyle.Top;
                this.radLabel34.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel34.ForeColor = System.Drawing.Color.White;
                this.radLabel34.Location = new System.Drawing.Point(0, 0);
                this.radLabel34.Name = "radLabel34";
                // 
                // 
                // 
                this.radLabel34.RootElement.ForeColor = System.Drawing.Color.White;
                this.radLabel34.Size = new System.Drawing.Size(934, 23);
                this.radLabel34.TabIndex = 69;
                this.radLabel34.Text = "Mileage Settings ";
                // 
                // btnAddMileage
                // 
                this.btnAddMileage.Image = global::Taxi_AppMain.Properties.Resources.add;
                this.btnAddMileage.Location = new System.Drawing.Point(758, 0);
                this.btnAddMileage.Name = "btnAddMileage";
                this.btnAddMileage.Size = new System.Drawing.Size(156, 24);
                this.btnAddMileage.TabIndex = 64;
                this.btnAddMileage.Text = "Add Mileage";
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddMileage.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddMileage.GetChildAt(0))).Text = "Add Mileage";
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddMileage.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddMileage.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.pageMileageSettings.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.grdMileageSetting.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdMileageSetting)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel34)).EndInit();
                this.radLabel34.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.btnAddMileage)).EndInit();

                //Events
                grdMileageSetting.ViewCellFormatting += new CellFormattingEventHandler(grdMileageSetting_ViewCellFormatting);
                this.btnAddMileage.Click += new EventHandler(btnAddMileage_Click);
            }
            catch (Exception ex)
            { }
        }
        private UI.MyGridView grdFixedFare;
        private Telerik.WinControls.UI.RadLabel radLabel33;
        private Telerik.WinControls.UI.RadButton btnFixFareDetails;
        private Telerik.WinControls.UI.RadButton btnAddFares;
        private void InitializeFixedFares()
        {
            try
            {

                this.grdFixedFare = new UI.MyGridView();
                this.radLabel33 = new Telerik.WinControls.UI.RadLabel();
                this.btnFixFareDetails = new Telerik.WinControls.UI.RadButton();
                this.btnAddFares = new Telerik.WinControls.UI.RadButton();

                ((System.ComponentModel.ISupportInitialize)(this.grdFixedFare)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel33)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdFixedFare.MasterTemplate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.btnFixFareDetails)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.btnAddFares)).BeginInit();

                this.pageFixedFares.Controls.Add(this.grdFixedFare);
                this.pageFixedFares.Controls.Add(this.radLabel33);

                // 
                // grdFixedFare
                // 
                this.grdFixedFare.AutoCellFormatting = false;
                this.grdFixedFare.Dock = System.Windows.Forms.DockStyle.Fill;
                this.grdFixedFare.EnableCheckInCheckOut = false;
                this.grdFixedFare.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdFixedFare.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdFixedFare.Location = new System.Drawing.Point(0, 23);
                // 
                // 
                // 
                this.grdFixedFare.MasterTemplate.AllowAddNewRow = false;
                this.grdFixedFare.Name = "grdFixedFare";
                this.grdFixedFare.PKFieldColumnName = "";
                this.grdFixedFare.ShowGroupPanel = false;
                this.grdFixedFare.ShowImageOnActionButton = true;
                this.grdFixedFare.Size = new System.Drawing.Size(934, 608);
                this.grdFixedFare.TabIndex = 70;
                this.grdFixedFare.Text = "myGridView1";
                // 
                // radLabel33
                // 
                this.radLabel33.AutoSize = false;
                this.radLabel33.BackColor = System.Drawing.Color.Crimson;
                this.radLabel33.Controls.Add(this.btnFixFareDetails);
                this.radLabel33.Controls.Add(this.btnAddFares);
                this.radLabel33.Dock = System.Windows.Forms.DockStyle.Top;
                this.radLabel33.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel33.ForeColor = System.Drawing.Color.White;
                this.radLabel33.Location = new System.Drawing.Point(0, 0);
                this.radLabel33.Name = "radLabel33";
                this.radLabel33.RootElement.ForeColor = System.Drawing.Color.White;
                this.radLabel33.Size = new System.Drawing.Size(934, 23);
                this.radLabel33.TabIndex = 69;
                this.radLabel33.Text = "Fixed Fares";
                // 
                // btnFixFareDetails
                // 
                this.btnFixFareDetails.Location = new System.Drawing.Point(107, 0);
                this.btnFixFareDetails.Name = "btnFixFareDetails";
                this.btnFixFareDetails.Size = new System.Drawing.Size(123, 24);
                this.btnFixFareDetails.TabIndex = 65;
                this.btnFixFareDetails.Text = "Show Details";
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnFixFareDetails.GetChildAt(0))).Image = null;
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnFixFareDetails.GetChildAt(0))).Text = "Show Details";
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFixFareDetails.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFixFareDetails.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // btnAddFares
                // 
                this.btnAddFares.Image = global::Taxi_AppMain.Properties.Resources.add;
                this.btnAddFares.Location = new System.Drawing.Point(758, 0);
                this.btnAddFares.Name = "btnAddFares";
                this.btnAddFares.Size = new System.Drawing.Size(156, 24);
                this.btnAddFares.TabIndex = 64;
                this.btnAddFares.Text = "Add Fixed Fares";
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddFares.GetChildAt(0))).Text = "Add Fixed Fares";
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.pageFixedFares.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.grdFixedFare)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdFixedFare.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel33)).EndInit();
                this.radLabel33.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.btnFixFareDetails)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.btnAddFares)).EndInit();
                //Events
                grdFixedFare.CommandCellClick += new CommandCellClickEventHandler(grdFixedFare_CommandCellClick);
                grdFixedFare.RowsChanging += new GridViewCollectionChangingEventHandler(grdFixedFare_RowsChanging);
                grdFixedFare.ViewCellFormatting += new CellFormattingEventHandler(grdFixedFare_ViewCellFormatting);
                this.btnAddFares.Click += new EventHandler(btnAddFares_Click);
                this.btnFixFareDetails.Click += new EventHandler(btnFixFareDetails_Click);
            }
            catch (Exception ex)
            { }
        }

        //private UI.MyGridView grdStationCommission;
        //private Telerik.WinControls.UI.RadLabel radLabel37;

        //private void InitializeStationCommission()
        //{

        //    if (grdStationCommission != null)
        //        return;

        //    try
        //    {
        //        this.grdStationCommission = new UI.MyGridView();
        //        this.radLabel37 = new Telerik.WinControls.UI.RadLabel();
        //        this.pageStationCommission.SuspendLayout();
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission)).BeginInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission.MasterTemplate)).BeginInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.radLabel37)).BeginInit();
        //        // 
        //        // pageStationCommission
        //        // 
        //        this.pageStationCommission.Controls.Add(this.grdStationCommission);
        //        this.pageStationCommission.Controls.Add(this.radLabel37);
        //        this.pageStationCommission.Location = new System.Drawing.Point(10, 37);
        //        this.pageStationCommission.Name = "pageStationCommission";
        //        this.pageStationCommission.Size = new System.Drawing.Size(913, 125);
        //        this.pageStationCommission.Text = "Station Charges";
        //        // 
        //        // grdStationCommission
        //        // 
        //        this.grdStationCommission.AutoCellFormatting = false;
        //        this.grdStationCommission.Dock = System.Windows.Forms.DockStyle.Fill;
        //        this.grdStationCommission.EnableCheckInCheckOut = false;
        //        this.grdStationCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
        //        this.grdStationCommission.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
        //        this.grdStationCommission.Location = new System.Drawing.Point(0, 23);
        //        // 
        //        // 
        //        // 
        //        this.grdStationCommission.MasterTemplate.AllowAddNewRow = false;
        //        this.grdStationCommission.Name = "grdStationCommission";
        //        this.grdStationCommission.PKFieldColumnName = "";
        //        this.grdStationCommission.ShowGroupPanel = false;
        //        this.grdStationCommission.ShowImageOnActionButton = true;
        //        this.grdStationCommission.Size = new System.Drawing.Size(913, 102);
        //        this.grdStationCommission.TabIndex = 71;
        //        this.grdStationCommission.Text = "myGridView1";
        //        // 
        //        // radLabel37
        //        // 
        //        this.radLabel37.AutoSize = false;
        //        this.radLabel37.BackColor = System.Drawing.Color.Crimson;
        //        this.radLabel37.Dock = System.Windows.Forms.DockStyle.Top;
        //        this.radLabel37.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //        this.radLabel37.ForeColor = System.Drawing.Color.White;
        //        this.radLabel37.Location = new System.Drawing.Point(0, 0);
        //        this.radLabel37.Name = "radLabel37";
        //        // 
        //        // 
        //        // 
        //        this.radLabel37.RootElement.ForeColor = System.Drawing.Color.White;
        //        this.radLabel37.Size = new System.Drawing.Size(913, 23);
        //        this.radLabel37.TabIndex = 70;
        //        this.radLabel37.Text = "Station Charges";
        //        this.pageStationCommission.ResumeLayout(false);
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission.MasterTemplate)).EndInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.grdStationCommission)).EndInit();
        //        ((System.ComponentModel.ISupportInitialize)(this.radLabel37)).EndInit();
        //        grdStationCommission.ViewCellFormatting += new CellFormattingEventHandler(grdStationCommission_ViewCellFormatting);
        //        this.grdStationCommission.CellEndEdit += new GridViewCellEventHandler(grdStationCommission_CellEndEdit);
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        //void grdStationCommission_CellEndEdit(object sender, GridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        decimal Company = e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();
        //        decimal Driver = e.Row.Cells[COLS_AirportCommission.DriverPrice].Value.ToDecimal();
        //        decimal Agent = e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value.ToDecimal();


        //        if (e.Column.Name == COLS_AirportCommission.CompanyPrice)
        //        {
        //            Company = e.Value.ToDecimal();
        //            Agent = Company - Driver;
        //            e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;

        //            e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Company - Agent;

        //        }
        //        else if (e.Column.Name == COLS_AirportCommission.CommissionAmount)
        //        {

        //            Agent = e.Value.ToDecimal();
        //            Driver = Company - Agent;
        //            e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Driver;
        //            e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;



        //        }
        //        else if (e.Column.Name == COLS_AirportCommission.DriverPrice)
        //        {
        //            Driver = e.Value.ToDecimal();
        //            Agent = Company - Driver;
        //            e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;



        //            e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private UI.MyGridView grdAirportCommission;
        private Telerik.WinControls.UI.RadLabel radLabel36;


        private void InitializeAirportCommission()
        {
            if (grdAirportCommission != null)
                return;
            try
            {
                this.grdAirportCommission = new UI.MyGridView();
                this.radLabel36 = new Telerik.WinControls.UI.RadLabel();
                //this.AirportCommission.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission.MasterTemplate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel36)).BeginInit();
                // 
                // pageAirportCommission
                // 
                pageAirportCommission.Item.Visibility = ElementVisibility.Visible;

                this.pageAirportCommission.Controls.Add(this.grdAirportCommission);
                this.pageAirportCommission.Controls.Add(this.radLabel36);
                this.pageAirportCommission.Location = new System.Drawing.Point(10, 37);
                this.pageAirportCommission.Name = "pageAirportCommission";
                this.pageAirportCommission.Size = new System.Drawing.Size(913, 125);
                this.pageAirportCommission.Text = "Airport Charges";
                // 
                // grdAirportCommission
                // 
                this.grdAirportCommission.AutoCellFormatting = false;
                this.grdAirportCommission.Dock = System.Windows.Forms.DockStyle.Fill;
                this.grdAirportCommission.EnableCheckInCheckOut = false;
                this.grdAirportCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdAirportCommission.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdAirportCommission.Location = new System.Drawing.Point(0, 23);
                // 
                // grdAirportCommission
                // 
                this.grdAirportCommission.MasterTemplate.AllowAddNewRow = false;
                this.grdAirportCommission.Name = "grdAirportCommission";
                this.grdAirportCommission.PKFieldColumnName = "";
                this.grdAirportCommission.ShowGroupPanel = false;
                this.grdAirportCommission.ShowImageOnActionButton = true;
                this.grdAirportCommission.Size = new System.Drawing.Size(913, 102);
                this.grdAirportCommission.TabIndex = 71;
                this.grdAirportCommission.Text = "myGridView1";
                // 
                // radLabel36
                // 
                this.radLabel36.AutoSize = false;
                this.radLabel36.BackColor = System.Drawing.Color.Crimson;
                this.radLabel36.Dock = System.Windows.Forms.DockStyle.Top;
                this.radLabel36.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel36.ForeColor = System.Drawing.Color.White;
                this.radLabel36.Location = new System.Drawing.Point(0, 0);
                this.radLabel36.Name = "radLabel36";
                // 
                // 
                // 
                this.radLabel36.RootElement.ForeColor = System.Drawing.Color.White;
                this.radLabel36.Size = new System.Drawing.Size(913, 23);
                this.radLabel36.TabIndex = 70;
                this.radLabel36.Text = "Airport Charges";
                //this.AirportCommission.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdAirportCommission)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel36)).EndInit();
                grdAirportCommission.ViewCellFormatting += new CellFormattingEventHandler(grdAirportCommission_ViewCellFormatting);
                this.grdAirportCommission.CellEndEdit += new GridViewCellEventHandler(grdAirportCommission_CellEndEdit);
            }
            catch (Exception ex)
            { }
        }

        void grdAirportCommission_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            try
            {
                decimal Company = e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();
                decimal Driver = e.Row.Cells[COLS_AirportCommission.DriverPrice].Value.ToDecimal();
                decimal Agent = e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value.ToDecimal();


                if (e.Column.Name == COLS_AirportCommission.CompanyPrice)
                {
                    Company = e.Value.ToDecimal();
                    Agent = Company - Driver;
                    e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;

                    e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Company - Agent;

                }
                else if (e.Column.Name == COLS_AirportCommission.CommissionAmount)
                {

                    Agent = e.Value.ToDecimal();
                    Driver = Company - Agent;
                    e.Row.Cells[COLS_AirportCommission.DriverPrice].Value = Driver;
                    e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;



                }
                else if (e.Column.Name == COLS_AirportCommission.DriverPrice)
                {
                    Driver = e.Value.ToDecimal();
                    Agent = Company - Driver;
                    e.Row.Cells[COLS_AirportCommission.CommissionAmount].Value = Agent;



                    e.Row.Cells[COLS_AirportCommission.CompanyPrice].Value = Driver + Agent;

                }
            }
            catch (Exception ex)
            {

            }
        }
        private UI.MyGridView grdCompanyAddress;
        private Telerik.WinControls.UI.RadLabel radLabel32;
        private Telerik.WinControls.UI.RadButton btnCompanyAddresses;
        private void InitializeCompanyAddress()
        {
            try
            {
                this.grdCompanyAddress = new UI.MyGridView();
                this.radLabel32 = new Telerik.WinControls.UI.RadLabel();
                this.btnCompanyAddresses = new Telerik.WinControls.UI.RadButton();
                this.Company_Addresses.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.grdCompanyAddress)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdCompanyAddress.MasterTemplate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel32)).BeginInit();
                this.radLabel32.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.btnCompanyAddresses)).BeginInit();
                this.BookedBy.SuspendLayout();

                // 
                // Company_Addresses
                // 
                this.Company_Addresses.Controls.Add(this.grdCompanyAddress);
                this.Company_Addresses.Controls.Add(this.radLabel32);
                //this.Company_Addresses.Location = new System.Drawing.Point(10, 37);
                //this.Company_Addresses.Name = "Company_Addresses";
                //this.Company_Addresses.Size = new System.Drawing.Size(913, 125);
                //this.Company_Addresses.Text = "Company Addresses";
                // 
                // grdCompanyAddress
                // 
                this.grdCompanyAddress.AutoCellFormatting = false;
                this.grdCompanyAddress.Dock = System.Windows.Forms.DockStyle.Fill;
                this.grdCompanyAddress.EnableCheckInCheckOut = false;
                this.grdCompanyAddress.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdCompanyAddress.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdCompanyAddress.Location = new System.Drawing.Point(0, 23);
                // 
                // grdCompanyAddress
                // 
                this.grdCompanyAddress.MasterTemplate.AllowAddNewRow = false;
                this.grdCompanyAddress.Name = "grdCompanyAddress";
                this.grdCompanyAddress.PKFieldColumnName = "";
                this.grdCompanyAddress.ShowGroupPanel = false;
                this.grdCompanyAddress.ShowImageOnActionButton = true;
                this.grdCompanyAddress.Size = new System.Drawing.Size(913, 102);
                this.grdCompanyAddress.TabIndex = 68;
                this.grdCompanyAddress.Text = "myGridView1";
                // 
                // radLabel32
                // 
                this.radLabel32.AutoSize = false;
                this.radLabel32.BackColor = System.Drawing.Color.Crimson;
                this.radLabel32.Controls.Add(this.btnCompanyAddresses);
                this.radLabel32.Dock = System.Windows.Forms.DockStyle.Top;
                this.radLabel32.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel32.ForeColor = System.Drawing.Color.White;
                this.radLabel32.Location = new System.Drawing.Point(0, 0);
                this.radLabel32.Name = "radLabel32";
                // 
                // 
                // 
                this.radLabel32.RootElement.ForeColor = System.Drawing.Color.White;
                this.radLabel32.Size = new System.Drawing.Size(913, 23);
                this.radLabel32.TabIndex = 67;
                this.radLabel32.Text = "Company Addresses";
                // 
                // btnCompanyAddresses
                // 
                this.btnCompanyAddresses.Image = global::Taxi_AppMain.Properties.Resources.add;
                this.btnCompanyAddresses.Location = new System.Drawing.Point(758, 0);
                this.btnCompanyAddresses.Name = "btnCompanyAddresses";
                this.btnCompanyAddresses.Size = new System.Drawing.Size(156, 24);
                this.btnCompanyAddresses.TabIndex = 64;
                this.btnCompanyAddresses.Text = "Company Addresses";
                this.btnCompanyAddresses.Click += new System.EventHandler(this.btnCompanyAddresses_Click);
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnCompanyAddresses.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnCompanyAddresses.GetChildAt(0))).Text = "Company Addresses";
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCompanyAddresses.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCompanyAddresses.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Company_Addresses.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.grdCompanyAddress.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdCompanyAddress)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel32)).EndInit();
                this.radLabel32.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.btnCompanyAddresses)).EndInit();

                grdCompanyAddress.CommandCellClick += new CommandCellClickEventHandler(grdCompanyAddress_CommandCellClick);
                grdCompanyAddress.RowsChanging += new GridViewCollectionChangingEventHandler(grdCompanyAddress_RowsChanging);
                grdCompanyAddress.ViewCellFormatting += new CellFormattingEventHandler(grdCompanyAddress_ViewCellFormatting);
            }
            catch (Exception ex)
            {

            }
        }
        private UI.MyGridView grdBookedBy;
        private Telerik.WinControls.UI.RadLabel radLabel35;
        private Telerik.WinControls.UI.RadButton btnBookedBy;
        private void InitializeBookedBy()
        {
            try
            {
                this.grdBookedBy = new UI.MyGridView();
                this.radLabel35 = new Telerik.WinControls.UI.RadLabel();
                this.btnBookedBy = new Telerik.WinControls.UI.RadButton();
                this.BookedBy.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.grdBookedBy)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdBookedBy.MasterTemplate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel35)).BeginInit();
                this.radLabel35.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.btnBookedBy)).BeginInit();
                // 
                // BookedBy
                // 
                this.BookedBy.Controls.Add(this.grdBookedBy);
                this.BookedBy.Controls.Add(this.radLabel35);
                //this.BookedBy.Location = new System.Drawing.Point(10, 37);
                //this.BookedBy.Name = "BookedBy";
                //this.BookedBy.Size = new System.Drawing.Size(913, 125);
                //this.BookedBy.Text = "Booked By";
                // 
                // grdBookedBy
                // 
                this.grdBookedBy.AutoCellFormatting = false;
                this.grdBookedBy.Dock = System.Windows.Forms.DockStyle.Fill;
                this.grdBookedBy.EnableCheckInCheckOut = false;
                this.grdBookedBy.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
                this.grdBookedBy.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
                this.grdBookedBy.Location = new System.Drawing.Point(0, 23);
                // 
                // 
                // 
                this.grdBookedBy.MasterTemplate.AllowAddNewRow = false;
                this.grdBookedBy.Name = "grdBookedBy";
                this.grdBookedBy.PKFieldColumnName = "";
                this.grdBookedBy.ShowGroupPanel = false;
                this.grdBookedBy.ShowImageOnActionButton = true;
                this.grdBookedBy.Size = new System.Drawing.Size(913, 102);
                this.grdBookedBy.TabIndex = 70;
                this.grdBookedBy.Text = "myGridView1";
                // 
                // radLabel35
                // 
                this.radLabel35.AutoSize = false;
                this.radLabel35.BackColor = System.Drawing.Color.Crimson;
                this.radLabel35.Controls.Add(this.btnBookedBy);
                this.radLabel35.Dock = System.Windows.Forms.DockStyle.Top;
                this.radLabel35.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel35.ForeColor = System.Drawing.Color.White;
                this.radLabel35.Location = new System.Drawing.Point(0, 0);
                this.radLabel35.Name = "radLabel35";
                // 
                // 
                // 
                this.radLabel35.RootElement.ForeColor = System.Drawing.Color.White;
                this.radLabel35.Size = new System.Drawing.Size(913, 23);
                this.radLabel35.TabIndex = 69;
                this.radLabel35.Text = "Booked By";
                // 
                // btnBookedBy
                // 
                this.btnBookedBy.Image = global::Taxi_AppMain.Properties.Resources.add;
                this.btnBookedBy.Location = new System.Drawing.Point(758, 0);
                this.btnBookedBy.Name = "btnBookedBy";
                this.btnBookedBy.Size = new System.Drawing.Size(156, 24);
                this.btnBookedBy.TabIndex = 64;
                this.btnBookedBy.Text = "Booked By";
                this.btnBookedBy.Click += new System.EventHandler(this.btnBookedBy_Click);
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnBookedBy.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
                ((Telerik.WinControls.UI.RadButtonElement)(this.btnBookedBy.GetChildAt(0))).Text = "Booked By";
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnBookedBy.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnBookedBy.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.BookedBy.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.grdBookedBy.MasterTemplate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.grdBookedBy)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel35)).EndInit();
                this.radLabel35.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.btnBookedBy)).EndInit();

                grdBookedBy.CommandCellClick += new CommandCellClickEventHandler(grdBookedBy_CommandCellClick);
                grdBookedBy.RowsChanging += new GridViewCollectionChangingEventHandler(grdBookedBy_RowsChanging);
                grdBookedBy.ViewCellFormatting += new CellFormattingEventHandler(grdBookedBy_ViewCellFormatting);
            }
            catch (Exception ex)
            { }
        }
        void btnFixFareDetails_Click(object sender, EventArgs e)
        {
            if (objMaster.Current != null)
            {
                int CompanyId = objMaster.Current.Id;
                frmFares frm = new frmFares(CompanyId, 1);
                if (grdFixedFare.RowCount > 0)
                {
                    long FareId = grdFixedFare.Rows.FirstOrDefault().Cells[COLS_FixedFare.FareId].Value.ToLong();
                    frm.OnDisplayRecord(FareId);
                }

                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;
                frm.ShowDialog();
                Save();
                DisplayRecord();
            }
            else
            {

                ENUtils.ShowMessage("Please save a Company First");
            }
        }

        void btnAddMileage_Click(object sender, EventArgs e)
        {
            bool saved = true;
            if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                            + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    SaveFromDepartment = true;
                    saved = OnSave();
                }
                else
                    saved = false;

            }

            if (saved == false) return;
            if (objMaster.Current != null)
            {
                int CompanyId = objMaster.Current.Id;
                frmFares frm = new frmFares(CompanyId, 2);
                //if (grdFixedFare.CurrentRow != null)
                if (grdMileageSetting.RowCount > 0)
                {
                    long FareId = grdMileageSetting.Rows.FirstOrDefault().Cells[COLS_MileageSetting.FareId].Value.ToLong();
                    frm.OnDisplayRecord(FareId);
                }
                else if (grdFixedFare.RowCount > 0)
                {
                    long FareId = grdFixedFare.Rows.FirstOrDefault().Cells[COLS_FixedFare.FareId].Value.ToLong();
                    frm.OnDisplayRecord(FareId);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;
                frm.ShowDialog();
                Save();
                DisplayRecord();
            }
        }

        void btnAddFares_Click(object sender, EventArgs e)
        {
            bool saved = true;
            if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                            + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    SaveFromDepartment = true;
                    saved = OnSave();
                }
                else
                    saved = false;

            }

            if (saved == false) return;
            if (objMaster.Current != null)
            {
                int CompanyId = objMaster.Current.Id;
                frmFares frm = new frmFares(CompanyId, 1);
                if (grdFixedFare.RowCount > 0)
                {
                    long FareId = grdFixedFare.Rows.FirstOrDefault().Cells[COLS_FixedFare.FareId].Value.ToLong();
                    frm.OnDisplayRecord(FareId);
                }
                else if (grdMileageSetting.RowCount > 0)
                {
                    long FareId = grdMileageSetting.Rows.FirstOrDefault().Cells[COLS_MileageSetting.FareId].Value.ToLong();
                    frm.OnDisplayRecord(FareId);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;
                frm.ShowDialog();
                Save();
                DisplayRecord();
            }
        }


        void grdAirportCommission_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {

            if (e.CellElement is GridDataCellElement && e.CellElement.Text == "")
            {
                if (e.Column.Name == COLS_AirportCommission.LocationId)
                {

                    e.CellElement.Text = e.Row.Cells[COLS_AirportCommission.Location].Value.ToStr();
                }
                else if (e.Column.Name == COLS_AirportCommission.VehicleTypeId)
                {

                    e.CellElement.Text = e.Row.Cells[COLS_AirportCommission.VehicleType].Value.ToStr();
                }

            }


            //if (e.Column.Name == COLS_AirportCommission.LocationId && e.CellElement is GridDataCellElement && e.CellElement.Text == "")
            //{
            //    //e.Column.Name == COLS_AirportCommission.LocationId &&
            //    //  e.CellElement.Text = e.Row.Cells[COLS_StationCommission.Location].Value.ToStr();

            //    e.CellElement.Text = e.Row.Cells[COLS_AirportCommission.Location].Value.ToStr();

            //}

            //if (e.CellElement.Text != null)

            //{
            //    string a = e.CellElement.Value.ToStr();
            //    e.Row.Cells[COLS_AirportCommission.Location].Value = e.CellElement.Text.ToStr();
            //    string Loc = e.CellElement.Value.ToStr();
            //}
        }



        void grdBookedBy_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
                e.CellElement.ForeColor = Color.Black;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }


            else if (e.CellElement is GridDataCellElement)
            {


                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    // This is how we get the RadButtonElement instance from the cell
                    button = (RadButtonElement)e.CellElement.Children[0];

                    if (button.Text == "Edit")
                    {
                        button.Image = Resources.Resource1.edit2;
                    }
                    if (button.Text == "Delete")
                    {

                        button.Image = Resources.Resource1.delete;

                    }
                }



                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;




            }
        }

        void grdBookedBy_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdBookedBy.CurrentRow is GridViewDataRowInfo == false) return;
                CompanyBookedByBO objCompanyBookedBy = new CompanyBookedByBO();
                try
                {
                    long Id = grdBookedBy.CurrentRow.Cells[COLS_BookedBy.Id].Value.ToLong();

                    objCompanyBookedBy.GetByPrimaryKey(Id);
                    if (objCompanyBookedBy.Current != null)
                    {
                        objCompanyBookedBy.Delete(objCompanyBookedBy.Current);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    if (objCompanyBookedBy.Errors.Count() > 0)
                    {
                        ENUtils.ShowMessage(objCompanyBookedBy.ShowErrors());
                    }
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }

                    e.Cancel = true;


                }




            }
        }

        void grdBookedBy_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                RadGridView grid = gridCell.GridControl;
                if (gridCell.ColumnInfo.Name == "btnDelete")
                {



                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a BookedBy  ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {

                        grid.CurrentRow.Delete();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "btnEdit")
                {
                    frmCompanyBookedBy frm = new frmCompanyBookedBy(gridCell.RowInfo.Cells[COLS_BookedBy.MasterId].Value.ToInt());
                    frm.OnDisplayRecord(gridCell.RowInfo.Cells[COLS_BookedBy.Id].Value.ToLong());
                    frm.ShowDialog();


                    if (frm.Saved)
                    {
                        frm.Dispose();

                        objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                        DisplayRecord();
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdMileageSetting_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
                e.CellElement.ForeColor = Color.Black;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }


            else if (e.CellElement is GridDataCellElement)
            {


                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    // This is how we get the RadButtonElement instance from the cell
                    button = (RadButtonElement)e.CellElement.Children[0];

                    if (button.Text == "Edit")
                    {
                        button.Image = Resources.Resource1.edit2;
                    }
                    if (button.Text == "Delete")
                    {

                        button.Image = Resources.Resource1.delete;

                    }
                }



                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;




            }

        }

        void grdFixedFare_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
                e.CellElement.ForeColor = Color.Black;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }


            else if (e.CellElement is GridDataCellElement)
            {


                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    // This is how we get the RadButtonElement instance from the cell
                    button = (RadButtonElement)e.CellElement.Children[0];

                    if (button.Text == "Update")
                    {
                        button.Image = Resources.Resource1.edit2;
                    }
                    if (button.Text == "Delete")
                    {

                        button.Image = Resources.Resource1.delete;

                    }
                }



                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;




            }
        }

        void grdFixedFare_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdFixedFare.CurrentRow is GridViewDataRowInfo == false) return;
                //  FareBO objFare = new FareBO();
                try
                {
                    long Id = grdFixedFare.CurrentRow.Cells[COLS_FixedFare.Id].Value.ToLong();
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        Fare_ChargesDetail objChargesDetail = db.Fare_ChargesDetails.Single(c => c.Id == Id);
                        db.Fare_ChargesDetails.DeleteOnSubmit(objChargesDetail);
                        db.SubmitChanges();
                        //grdFixedFare.CurrentRow.Delete();
                    }

                }
                catch (Exception ex)
                {
                    ENUtils.ShowMessage(ex.Message);
                    e.Cancel = true;
                }
            }
        }

        void grdFixedFare_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                RadGridView grid = gridCell.GridControl;
                if (gridCell.ColumnInfo.Name == "btnDelete")
                {



                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Fixed Fare ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {

                        grid.CurrentRow.Delete();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "btnUpdate")
                {
                    //frmCompanyAddresses frm = new frmCompanyAddresses(gridCell.RowInfo.Cells[COLS.MasterId].Value.ToInt());
                    //frm.OnDisplayRecord();
                    //frm.ShowDialog();
                    long Id = gridCell.RowInfo.Cells[COLS_FixedFare.Id].Value.ToLong();
                    decimal Rate = gridCell.RowInfo.Cells[COLS_FixedFare.Rate].Value.ToDecimal();

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_RunProcedure("update Fare_ChargesDetails set Rate=" + Rate + " where Id=" + Id + "");
                    }
                    DisplayRecord();

                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdCompanyAddress_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdCompanyAddress.CurrentRow is GridViewDataRowInfo == false) return;
                CompanyAddressesBO objCompanyAddresses = new CompanyAddressesBO();
                try
                {
                    long Id = grdCompanyAddress.CurrentRow.Cells[COLS_Adress.Id].Value.ToLong();

                    objCompanyAddresses.GetByPrimaryKey(Id);
                    if (objCompanyAddresses.Current != null)
                    {
                        objCompanyAddresses.Delete(objCompanyAddresses.Current);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    if (objCompanyAddresses.Errors.Count() > 0)
                    {
                        ENUtils.ShowMessage(objCompanyAddresses.ShowErrors());
                    }
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }

                    e.Cancel = true;


                }




            }
        }

        void grdCompanyAddress_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
                e.CellElement.ForeColor = Color.Black;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }


            else if (e.CellElement is GridDataCellElement)
            {


                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    // This is how we get the RadButtonElement instance from the cell
                    button = (RadButtonElement)e.CellElement.Children[0];

                    if (button.Text == "Edit")
                    {
                        button.Image = Resources.Resource1.edit2;
                    }
                    if (button.Text == "Delete")
                    {

                        button.Image = Resources.Resource1.delete;

                    }
                }



                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;




            }
        }

        void grdCompanyAddress_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                RadGridView grid = gridCell.GridControl;
                if (gridCell.ColumnInfo.Name == "btnDelete")
                {



                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Address ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {

                        grid.CurrentRow.Delete();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "btnEdit")
                {
                    frmCompanyAddresses frm = new frmCompanyAddresses(gridCell.RowInfo.Cells[COLS.MasterId].Value.ToInt());
                    frm.OnDisplayRecord(gridCell.RowInfo.Cells[COLS.Id].Value.ToLong());
                    frm.ShowDialog();


                    if (frm.Saved)
                    {
                        frm.Dispose();

                        objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                        DisplayRecord();
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdOrderNo_CommandCellClick(object sender, EventArgs e)
        {
            if (grdOrderNo.CurrentRow != null && grdOrderNo.CurrentRow is GridViewDataRowInfo)
            {

                grdOrderNo.CurrentRow.Delete();

            }
        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int Id = 0;
                    Id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
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

        void grdContacts_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdCostCenter.CurrentRow is GridViewDataRowInfo == false) return;
                CompanyContactBO objCC = new CompanyContactBO();

                try
                {
                    int Id = grdContacts.CurrentRow.Cells[COLS.Id].Value.ToInt();

                    objCC.GetByPrimaryKey(Id);
                    if (objCC.Current != null)
                    {
                        objCC.Delete(objCC.Current);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    if (objCC.Errors.Count() > 0)
                    {
                        ENUtils.ShowMessage(objCC.ShowErrors());
                    }
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }

                    e.Cancel = true;


                }




            }
        }

        void grdContacts_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Contact ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name == "btnEdit")
            {
                frmCompanyContacts frm = new frmCompanyContacts(gridCell.RowInfo.Cells[COLS.MasterId].Value.ToInt());
                frm.OnDisplayRecord(gridCell.RowInfo.Cells[COLS.Id].Value.ToInt());
                frm.ShowDialog();


                if (frm.Saved)
                {
                    frm.Dispose();

                    objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                    DisplayRecord();
                }
            }

        }







        void grdCostCenter_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdCostCenter.CurrentRow is GridViewDataRowInfo == false) return;
                CompanyCostCenterBO objCC = new CompanyCostCenterBO();

                try
                {
                    int Id = grdCostCenter.CurrentRow.Cells[COLS.Id].Value.ToInt();

                    objCC.GetByPrimaryKey(Id);
                    if (objCC.Current != null)
                    {
                        objCC.Delete(objCC.Current);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    if (objCC.Errors.Count() > 0)
                    {
                        ENUtils.ShowMessage(objCC.ShowErrors());
                    }
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }

                    e.Cancel = true;


                }




            }
        }

        void grdCostCenter_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Cost Center ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name == "btnEdit")
            {
                frmCompanyCostCenter frm = new frmCompanyCostCenter(gridCell.RowInfo.Cells[COLS.MasterId].Value.ToInt());
                frm.OnDisplayRecord(gridCell.RowInfo.Cells[COLS.Id].Value.ToInt());
                frm.ShowDialog();


                if (frm.Saved)
                {
                    frm.Dispose();

                    objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                    DisplayRecord();
                }
            }

        }

        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.AliceBlue;

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
        string cellValue = string.Empty;
        void grdDeparments_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
                e.CellElement.ForeColor = Color.Black;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }


            else if (e.CellElement is GridDataCellElement)
            {


                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    // This is how we get the RadButtonElement instance from the cell
                    button = (RadButtonElement)e.CellElement.Children[0];

                    if (button.Text == "Edit")
                    {
                        button.Image = Resources.Resource1.edit2;
                    }
                    if (button.Text == "Delete")
                    {

                        button.Image = Resources.Resource1.delete;

                    }
                }



                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;




            }




        }

        void grdDeparments_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdDepartment.CurrentRow is GridViewDataRowInfo == false) return;
                CompanyDepartmentBO objDepartment = new CompanyDepartmentBO();

                try
                {
                    long Id = grdDepartment.CurrentRow.Cells[COLS.Id].Value.ToLong();

                    objDepartment.GetByPrimaryKey(Id);
                    if (objDepartment.Current != null)
                    {
                        objDepartment.Delete(objDepartment.Current);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    if (objDepartment.Errors.Count() > 0)
                    {
                        ENUtils.ShowMessage(objDepartment.ShowErrors());
                    }
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }

                    e.Cancel = true;


                }




            }
        }

        void grdDeparments_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Department ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name == "btnEdit")
            {
                frmCompanyDepartments frm = new frmCompanyDepartments(gridCell.RowInfo.Cells[COLS.MasterId].Value.ToInt());
                frm.OnDisplayRecord(gridCell.RowInfo.Cells[COLS.Id].Value.ToLong());
                frm.ShowDialog();


                if (frm.Saved)
                {
                    frm.Dispose();

                    objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                    DisplayRecord();
                }
            }

        }


        void grdWebLogin_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Department ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name == "btnEdit")
            {
                frmCompanyWebLogin frm = new frmCompanyWebLogin(gridCell.RowInfo.Cells[COLS.MasterId].Value.ToInt());
                frm.OnDisplayRecord(gridCell.RowInfo.Cells[COLS.Id].Value.ToLong());
                frm.ShowDialog();


                if (frm.Saved)
                {
                    frm.Dispose();

                    objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                    DisplayRecord();
                    Save();
                }
            }

        }



        void grdWebLogin_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (grdWebLogin.CurrentRow is GridViewDataRowInfo == false) return;
                CompanyWebLoginBO objWebLogin = new CompanyWebLoginBO();

                try
                {
                    long Id = grdWebLogin.CurrentRow.Cells[COLS_WebLogin.Id].Value.ToLong();
                    string loginId = grdWebLogin.CurrentRow.Cells[COLS_WebLogin.LoginId].Value.ToStr().Trim();


                    objWebLogin.GetByPrimaryKey(Id);
                    if (objWebLogin.Current != null)
                    {
                        objWebLogin.Delete(objWebLogin.Current);

                        //if (objMaster.Current != null && objMaster.Current.Id > 0)
                        //    UpdateOnlineData(loginId, objMaster.Current.Id);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    if (objWebLogin.Errors.Count() > 0)
                    {
                        ENUtils.ShowMessage(objWebLogin.ShowErrors());
                    }
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }

                    e.Cancel = true;


                }




            }
        }
        private void FormatAdressGrid()
        {
            if (grdCompanyAddress == null)
                return;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_Adress.Id;
            col.IsVisible = false;
            grdCompanyAddress.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_Adress.MasterId;
            col.IsVisible = false;
            grdCompanyAddress.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_Adress.Address;
            col.HeaderText = "Address";
            col.Width = 400;
            col.ReadOnly = true;

            grdCompanyAddress.Columns.Add(col);
            grdCompanyAddress.AddEditColumn();
            grdCompanyAddress.AddDeleteColumn();

            grdCompanyAddress.ShowRowHeaderColumn = false;
            grdCompanyAddress.Columns["btnEdit"].Width = 100;
            grdCompanyAddress.Columns["btnDelete"].Width = 100;

        }
        private void FormatFixedFareGrid()
        {
            if (grdFixedFare == null)
                return;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.Id;
            col.IsVisible = false;
            grdFixedFare.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.MasterId;
            col.IsVisible = false;
            grdFixedFare.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.FareId;
            col.IsVisible = false;
            grdFixedFare.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.VehicleType;
            col.ReadOnly = true;
            col.Width = 120;
            col.HeaderText = "Vehicle Type";
            grdFixedFare.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.VehicleTypeId;
            col.IsVisible = false;

            grdFixedFare.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.FromLocationTypeId;
            col.IsVisible = false;
            col.Width = 240;

            grdFixedFare.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.FromAddress;
            col.ReadOnly = true;
            col.Width = 240;
            col.HeaderText = "From Address";
            grdFixedFare.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.ToLocationTypeId;
            col.IsVisible = false;
            col.Width = 240;
            grdFixedFare.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_FixedFare.ToAddress;
            col.ReadOnly = true;
            col.Width = 240;
            col.HeaderText = "To Address";
            grdFixedFare.Columns.Add(col);
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_FixedFare.Rate;
            dcol.Width = 100;
            dcol.HeaderText = "Rate (£)";
            grdFixedFare.Columns.Add(dcol);
            //grdFixedFare.AddEditColumn();

            GridViewCommandColumn colbtn = new GridViewCommandColumn();
            colbtn.Width = 100;
            colbtn.Name = "btnUpdate";
            colbtn.UseDefaultText = true;
            colbtn.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            colbtn.DefaultText = "Update";
            colbtn.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdFixedFare.Columns.Add(colbtn);
            grdFixedFare.AddDeleteColumn();

            grdFixedFare.ShowRowHeaderColumn = false;
            grdFixedFare.Columns["btnDelete"].Width = 100;


            grdFixedFare.EnableFiltering = true;
            grdFixedFare.ShowFilteringRow = true;

        }

        private void FormatMilageSettingGrid()
        {
            if (grdMileageSetting == null)
                return;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_MileageSetting.Id;
            col.IsVisible = false;
            grdMileageSetting.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_MileageSetting.MasterId;
            col.IsVisible = false;
            grdMileageSetting.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_MileageSetting.FareId;
            col.IsVisible = false;
            grdMileageSetting.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_MileageSetting.VehicleType;
            col.ReadOnly = true;
            col.Width = 120;
            col.HeaderText = "Vehicle Type";
            grdMileageSetting.Columns.Add(col);
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_MileageSetting.FromMile;
            dcol.HeaderText = "From Mile";
            dcol.Width = 120;
            dcol.ReadOnly = true;
            grdMileageSetting.Columns.Add(dcol);
            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_MileageSetting.ToMile;
            dcol.HeaderText = "To Mile";
            dcol.Width = 120;
            dcol.ReadOnly = true;
            grdMileageSetting.Columns.Add(dcol);
            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_MileageSetting.Rate;
            dcol.HeaderText = "Rate";
            dcol.Width = 120;
            dcol.ReadOnly = true;
            grdMileageSetting.Columns.Add(dcol);
            grdMileageSetting.ShowRowHeaderColumn = false;
            //grdMileageSetting.AddEditColumn();
            //grdMileageSetting.AddDeleteColumn();
            //grdMileageSetting.ShowRowHeaderColumn = false;
            //grdMileageSetting.Columns["btnEdit"].Width = 100;
            //grdMileageSetting.Columns["btnDelete"].Width = 100;

            grdMileageSetting.EnableFiltering = true;
            grdMileageSetting.ShowFilteringRow = true;
        }


        private void FormatBookedByGrid()
        {
            if (grdBookedBy == null)
                return;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_BookedBy.Id;
            col.IsVisible = false;
            grdBookedBy.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_BookedBy.MasterId;
            col.IsVisible = false;
            grdBookedBy.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_BookedBy.BookedBy;
            col.HeaderText = "Booked By";
            col.ReadOnly = true;
            col.Width = 300;
            grdBookedBy.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_BookedBy.Email;
            col.HeaderText = "Email";
            col.ReadOnly = true;
            col.Width = 300;
            grdBookedBy.Columns.Add(col);

            grdBookedBy.AddEditColumn();
            grdBookedBy.AddDeleteColumn();

            grdBookedBy.ShowRowHeaderColumn = false;
            grdBookedBy.Columns["btnEdit"].Width = 100;
            grdBookedBy.Columns["btnDelete"].Width = 100;
        }
        private void FormatDepartmentGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdDepartment.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.MasterId;
            col.IsVisible = false;
            grdDepartment.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.DepartmentName;
            col.HeaderText = "Department Name";
            col.Width = 200;
            grdDepartment.Columns.Add(col);
            col.ReadOnly = true;


            col = new GridViewTextBoxColumn();
            col.Name = COLS.Pickup;
            col.HeaderText = "Pickup";
            col.Width = 200;
            grdDepartment.Columns.Add(col);
            col.ReadOnly = true;



            col = new GridViewTextBoxColumn();
            col.Name = COLS.DropOff;
            col.HeaderText = "DropOff";
            col.Width = 200;
            grdDepartment.Columns.Add(col);
            col.ReadOnly = true;


            grdDepartment.AddEditColumn();
            grdDepartment.AddDeleteColumn();

            grdDepartment.ShowRowHeaderColumn = false;
            grdDepartment.Columns["btnEdit"].Width = 100;
            grdDepartment.Columns["btnDelete"].Width = 100;


        }


        private void FormatCostCenterGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_CC.Id;
            col.IsVisible = false;
            grdCostCenter.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_CC.MasterId;
            col.IsVisible = false;
            grdCostCenter.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_CC.CostCenterName;
            col.HeaderText = "Cost Center";
            col.Width = 300;
            grdCostCenter.Columns.Add(col);
            col.ReadOnly = true;


            grdCostCenter.AddEditColumn();
            grdCostCenter.AddDeleteColumn();

            grdCostCenter.ShowRowHeaderColumn = false;

            grdCostCenter.Columns["btnEdit"].Width = 100;
            grdCostCenter.Columns["btnDelete"].Width = 100;


        }


        private void FormatContactsrGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_Contacts.Id;
            col.IsVisible = false;
            grdContacts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_Contacts.MasterId;
            col.IsVisible = false;
            grdContacts.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_Contacts.ContactName;
            col.HeaderText = "Name";
            col.Width = 110;
            grdContacts.Columns.Add(col);
            col.ReadOnly = true;


            col = new GridViewTextBoxColumn();
            col.Name = COLS_Contacts.Email;
            col.HeaderText = "Email";
            col.Width = 130;
            grdContacts.Columns.Add(col);
            col.ReadOnly = true;


            col = new GridViewTextBoxColumn();
            col.Name = COLS_Contacts.Password;
            col.HeaderText = "Password";
            col.Width = 60;
            grdContacts.Columns.Add(col);
            col.ReadOnly = true;



            col = new GridViewTextBoxColumn();
            col.Name = COLS_Contacts.TelephoneNo;
            col.HeaderText = "Telephone No";
            col.Width = 120;
            grdContacts.Columns.Add(col);
            col.ReadOnly = true;





            col = new GridViewTextBoxColumn();
            col.Name = COLS_Contacts.MobileNo;
            col.HeaderText = "Mobile No";
            col.Width = 120;
            grdContacts.Columns.Add(col);
            col.ReadOnly = true;

            GridViewCheckBoxColumn colPrimary = new GridViewCheckBoxColumn();
            colPrimary.Name = COLS_Contacts.Primary;
            //colPrimary.HeaderText = "Telephone No";
            colPrimary.IsVisible = false;
            grdContacts.Columns.Add(colPrimary);
            colPrimary.ReadOnly = true;


            grdContacts.AddEditColumn();
            grdContacts.AddDeleteColumn();

            grdContacts.ShowRowHeaderColumn = false;

            grdContacts.Columns["btnEdit"].Width = 100;
            grdContacts.Columns["btnDelete"].Width = 100;


        }


        private void FormatWebLoginGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_WebLogin.Id;
            col.IsVisible = false;
            grdWebLogin.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_WebLogin.MasterId;
            col.IsVisible = false;
            grdWebLogin.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_WebLogin.AccountNo;
            col.HeaderText = "A/C No";
            col.Width = 110;
            grdWebLogin.Columns.Add(col);
            col.ReadOnly = true;



            col = new GridViewTextBoxColumn();
            col.Name = COLS_WebLogin.LoginId;
            col.HeaderText = "Login Id";
            col.Width = 120;
            grdWebLogin.Columns.Add(col);
            col.ReadOnly = true;





            col = new GridViewTextBoxColumn();
            col.Name = COLS_WebLogin.Password;
            col.HeaderText = "Password";
            col.Width = 100;
            grdWebLogin.Columns.Add(col);
            col.ReadOnly = true;



            col = new GridViewTextBoxColumn();
            col.Name = COLS_WebLogin.TelephoneNo;
            col.HeaderText = "Telephone No";
            col.Width = 120;
            grdWebLogin.Columns.Add(col);
            col.ReadOnly = true;



            col = new GridViewTextBoxColumn();
            col.Name = COLS_WebLogin.MobileNo;
            col.HeaderText = "Mobile No";
            col.Width = 120;
            grdWebLogin.Columns.Add(col);
            col.ReadOnly = true;




            GridViewCheckBoxColumn colPrimary = new GridViewCheckBoxColumn();
            colPrimary.Name = COLS_WebLogin.IsActive;
            //colPrimary.HeaderText = "Telephone No";
            colPrimary.IsVisible = false;
            grdWebLogin.Columns.Add(colPrimary);
            colPrimary.ReadOnly = true;


            grdWebLogin.AddEditColumn();
            grdWebLogin.AddDeleteColumn();

            grdWebLogin.ShowRowHeaderColumn = false;

            grdWebLogin.Columns["btnEdit"].Width = 100;
            grdWebLogin.Columns["btnDelete"].Width = 100;


        }


        private void FormatOrderNoGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdOrderNo.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.MasterId;
            col.IsVisible = false;
            grdOrderNo.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "OrderNo";
            col.HeaderText = "Order No";
            col.Width = 250;
            grdOrderNo.Columns.Add(col);
            col.ReadOnly = false;





            grdOrderNo.AllowEditRow = true;
            grdOrderNo.AllowAddNewRow = true;
            grdOrderNo.AllowDeleteRow = true;
            grdOrderNo.AddDeleteColumn();
            grdOrderNo.AddNewRowPosition = SystemRowPosition.Top;
            grdOrderNo.ShowRowHeaderColumn = false;

            grdOrderNo.Columns["btnDelete"].Width = 100;


        }
        private void FormatAirportCommissionGrid()
        {
            if (grdAirportCommission == null)
                return;
            //grdAirportCommission.AllowAddNewRow = true;
            //grdAirportCommission.AddNewRowPosition = SystemRowPosition.Top;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.Id;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.MasterId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.LocationId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.FareId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.LocationTypeId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_AirportCommission.VehicleTypeId;
            col.IsVisible = false;
            grdAirportCommission.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.Width = 240;
            col.HeaderText = "Airport";
            col.Name = COLS_AirportCommission.Location;
            grdAirportCommission.Columns.Add(col);





            GridViewDecimalColumn dcol = new GridViewDecimalColumn();

            dcol.Name = COLS_AirportCommission.CompanyPrice;
            dcol.HeaderText = "Company Price";
            dcol.Width = 130;
            dcol.DecimalPlaces = 2;
            dcol.Maximum = 1000000;
            grdAirportCommission.Columns.Add(dcol);
            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_AirportCommission.DriverPrice;
            dcol.HeaderText = "Driver Price";
            dcol.Width = 130;
            dcol.DecimalPlaces = 2;
            dcol.Maximum = 1000000;
            grdAirportCommission.Columns.Add(dcol);
            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_AirportCommission.CommissionAmount;
            dcol.HeaderText = "Agent Commission";
            dcol.Width = 140;
            dcol.DecimalPlaces = 2;
            dcol.Maximum = 1000000;
            grdAirportCommission.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_AirportCommission.CommissionPercent;
            dcol.HeaderText = "Commission Amount";
            dcol.Width = 130;
            dcol.IsVisible = false;
            dcol.Maximum = 10000;
            grdAirportCommission.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS_AirportCommission.CustomerPrice;
            dcol.HeaderText = "Commission Percent";
            dcol.Width = 130;
            dcol.IsVisible = false;
            dcol.DecimalPlaces = 0;
            dcol.Maximum = 100;
            grdAirportCommission.Columns.Add(dcol);




            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS_AirportCommission.CommissionOnPercent;
            cbcol.HeaderText = "Percentage Wise";
            cbcol.Width = 130;
            cbcol.IsVisible = false;
            grdAirportCommission.Columns.Add(cbcol);


            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS_AirportCommission.DayWise;
            cbcol.HeaderText = "Day Wise";
            cbcol.Width = 100;
            cbcol.IsVisible = false;

            grdAirportCommission.Columns.Add(cbcol);
            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS_AirportCommission.NightWise;
            cbcol.HeaderText = "Night Wise";
            cbcol.Width = 100;
            cbcol.IsVisible = false;
            grdAirportCommission.Columns.Add(cbcol);
            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.Width = 130;
            col.HeaderText = "Vehicle Type";
            col.Name = COLS_AirportCommission.VehicleType;
            grdAirportCommission.Columns.Add(col);


            //colCombo = new GridViewComboBoxColumn();
            //colCombo.Name = COLS_AirportCommission.VehicleTypeId;
            ////  colCombo.IsVisible = false;
            //colCombo.HeaderText = "Vehicle Type";
            //colCombo.DataSource = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).Select(args => new { args.Id, args.VehicleType }).ToList();
            //colCombo.DisplayMember = "VehicleType";
            //colCombo.ValueMember = "Id";
            //colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            //colCombo.AutoCompleteMode = AutoCompleteMode.None;
            //colCombo.ReadOnly = false;
            //colCombo.Width = 150;
            //grdAirportCommission.Columns.Add(colCombo);

            //GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            //colCombo.Name = COLS_AirportCommission.LocationId;
            ////  colCombo.IsVisible = false;
            //colCombo.HeaderText = "Location";

            //colCombo.DataSource = General.GetQueryable<Gen_Location>(c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT).OrderBy(c => c.LocationName).ToList();
            //colCombo.DisplayMember = "LocationName";
            //colCombo.ValueMember = "Id";
            //colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            //colCombo.AutoCompleteMode = AutoCompleteMode.None;
            //colCombo.ReadOnly = false;

            //colCombo.Width = 250;

            //grdAirportCommission.Columns.Add(colCombo);

            //GridViewTextBoxColumn col2 = new GridViewTextBoxColumn();
            //col2.IsVisible = false;
            //col2.HeaderText = "Vehicle";
            //col2.Name = COLS_AirportCommission.VehicleType;
            //grdAirportCommission.Columns.Add(col2);



            this.grdAirportCommission.AutoGenerateColumns = false;

            this.grdAirportCommission.CellValueChanged += new GridViewCellEventHandler(grdAirportCommission_CellValueChanged);
        }

        void grdAirportCommission_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "LocationId")
            {
                GridViewComboBoxColumn ed = this.grdAirportCommission.Columns["LocationId"] as GridViewComboBoxColumn;
                if (ed != null)
                {
                    int LocId = e.Row.Cells[COLS_AirportCommission.LocationId].Value.ToInt();
                    List<Gen_Location> list = (List<Gen_Location>)ed.DataSource;
                    e.Row.Cells[COLS_AirportCommission.Location].Value = list.FirstOrDefault(c => c.Id == LocId).LocationName;
                }
            }

        }







        public struct COLSC
        {
            public static string RefNo = "RefNo";
            public static string JobRefNo = "JobRefNo";
            public static string ComplainDate = "ComplainDate";
            public static string CompanyName = "CompanyName";
            public static string IncidentDate = "IncidentDate";
            public static string ComplainDescription = "ComplainDescription";
            public static string ResultDescription = "ResultDescription";
            public static string CustomerName = "CustomerName";
        }
        public void FormatComplainGrid()
        {
            //GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            //col.Name = COLSC.RefNo;
            //col.HeaderText = "Ref No";
            //col.Width = 80;
            //grdLister.Columns.Add(col);
            //col = new GridViewTextBoxColumn();
            //col.Name = COLSC.JobRefNo;
            //col.HeaderText = "Job Ref No";
            //col.Width=90;
            //grdLister.Columns.Add(col);
            //col = new GridViewTextBoxColumn();
            //col.Name = COLSC.ComplainDate;
            //col.HeaderText = "Complain Date";
            //col.Width = 110;
            //grdLister.Columns.Add(col);
            //col = new GridViewTextBoxColumn();
            //col.Name = COLSC.IncidentDate;
            //col.HeaderText = "Incident Date";
            //col.Width = 110;
            //grdLister.Columns.Add(col);
            //col = new GridViewTextBoxColumn();
            //col.Name = COLSC.CustomerName;
            //col.HeaderText = "Customer Name";
            //col.Width = 150;
            //grdLister.Columns.Add(col);
            //col = new GridViewTextBoxColumn();
            //col.Name = COLSC.ComplainDescription;
            //col.HeaderText = "Complain Description";
            //col.Width = 180;
            //grdLister.Columns.Add(col);
            //col = new GridViewTextBoxColumn();
            //col.Name = COLSC.ResultDescription;
            //col.HeaderText = "Result Description";
            //col.Width = 180;
            //grdLister.Columns.Add(col);

            // AddComplaintButton();
        }
        private void AddComplaintButton()
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 80;

            col.Name = "btnEdit";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Edit";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(col);
            //col = new GridViewCommandColumn();
            //col.Width = 80;
            //col.Name = "btnDelete";
            //col.UseDefaultText = true;
            //col.UseDefaultText = true;
            //col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            //col.DefaultText = "Delete";
            //col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            //grdLister.Columns.Add(col);

        }
        void frmCompany_Shown(object sender, EventArgs e)
        {
            try
            {
                txtCompanyName.Focus();


                if (grdLister.Columns.Count > 0)
                {
                    grdLister.Columns["Id"].IsVisible = false;
                    grdLister.Columns["RefNo"].Width = 80;
                    grdLister.Columns["ComplainDate"].Width = 110;
                    grdLister.Columns["IncidentDate"].Width = 110;
                    grdLister.Columns["CustomerName"].Width = 150;
                    grdLister.Columns["JobRefNo"].Width = 90;
                    grdLister.Columns["ComplainDescription"].Width = 180;
                    grdLister.Columns["ResultDescription"].Width = 180;
                    grdLister.Columns["RefNo"].HeaderText = "Ref No";
                    grdLister.Columns["JobRefNo"].HeaderText = "Job Ref No";
                    grdLister.Columns["ComplainDate"].HeaderText = "Complain Date";
                    grdLister.Columns["IncidentDate"].HeaderText = "Incident Date";
                    (grdLister.Columns["IncidentDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                    (grdLister.Columns["ComplainDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                    grdLister.Columns["CustomerName"].HeaderText = "Customer Name";
                    grdLister.Columns["ComplainDescription"].HeaderText = "Complain Description";
                    grdLister.Columns["ResultDescription"].HeaderText = "Result Description";
                }
            }
            catch
            {

            }
        }












        #region Overridden Methods


        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;


            chkHasBookedBy.Checked = objMaster.Current.HasBookedBy.ToBool();

            pageComplaint.Item.Visibility = ElementVisibility.Visible;

            txtCompanyName.Text = objMaster.Current.CompanyName.ToStr();
            txtEmail.Text = objMaster.Current.Email.ToStr();
            txtFax.Text = objMaster.Current.Fax.ToStr();
            txtMobileNo.Text = objMaster.Current.MobileNo.ToStr();
            txtPhone.Text = objMaster.Current.TelephoneNo.ToStr();
            txtWebsite.Text = objMaster.Current.WebsiteUrl.ToStr();
            txtContactName.Text = objMaster.Current.ContactName.ToStr();
            txtAddress.Text = objMaster.Current.Address.ToStr();

            chkOrderNo.Checked = objMaster.Current.HasOrderNo.ToBool();
            chkSingleOrderNo.Checked = objMaster.Current.HasSingleOrderNo.ToBool();
            chkPupilNo.Checked = objMaster.Current.HasPupilNo.ToBool();
            chkPreferredEmails.Checked = objMaster.Current.PreferredEmails.ToBool();
            ddlAdminFeeType.SelectedValue = objMaster.Current.AdminFeeType.ToStr();

            numAdminFees.Value = objMaster.Current.AdminFees.ToInt();
            numDiscount.Value = objMaster.Current.DiscountPercentage.ToDecimal();
            numFareDeductPer.Value = objMaster.Current.FareDeductionPercent.ToDecimal();
            chkHasVAT.Checked = objMaster.Current.HasVat.ToBool();
            chkIsComcabCharges.Checked = objMaster.Current.HasComcabCharges.ToBool();

            chkHasEscort.Checked = objMaster.Current.HasEscort.ToBool();

            txtCompanyCode.Text = objMaster.Current.CompanyCode.ToStr();

            ddlAccountType.SelectedValue = objMaster.Current.AccountTypeId.ToInt();

            chkCompanyPriceAmountWise.Checked = objMaster.Current.HasRoomCharge.ToBool();
            numCompanyPricePercent.Value = objMaster.Current.CompanyPricePercent.ToDecimal();

            txtAccountNo.Text = objMaster.Current.AccountNo.ToStr();
            txtLoginID.Text = objMaster.Current.WebLoginId.ToStr();
            txtPassword.Text = objMaster.Current.WebLoginPassword.ToStr();
            chkActivateLogin.Checked = objMaster.Current.IsWebLoginActive.ToBool();

            chkPasswordAccount.Checked = objMaster.Current.PasswordEnable.ToBool();
            txtPasswordAccount.Text = objMaster.Current.PasswordAccount.ToStr();
            chkClosed.Visible = true;
            chkClosed.Checked = objMaster.Current.IsClosed.ToBool();
           

            chkAgent.Checked = objMaster.Current.IsAgent.ToBool();
            // numAgentCommission.Value = objMaster.Current.CommissionPerBooking.ToDecimal();
          //  chkHasRoomCharge.Checked = objMaster.Current.HasRoomCharge.ToBool();


            txtCreditCard.Text = objMaster.Current.CreditCardDetails.ToStr().Trim();

            chkDisableCompanyFares.Checked = objMaster.Current.DisableCompanyFaresForController.ToBool();


            ddlDrvFareReductionType.SelectedValue = objMaster.Current.DriverFareReductionType.ToStr();
            numDrvFareReductionValue.Value = objMaster.Current.DriverFareReductionValue.ToDecimal();
            chkMandatoryOrderNo.Checked = objMaster.Current.MandatoryOrderNo.ToBool();

            txtInformation.Text = objMaster.Current.CompanyInformation.ToStr().Trim();
            chkResetAllFares.Checked = objMaster.Current.ResetAllFares.ToBool();
            chkDisableCustomerText.Checked = objMaster.Current.DisableCustomerText.ToBool();
            chkDisableArrivalText.Checked = objMaster.Current.DisableArrivalText.ToBool();
            chkDisableAdvanceText.Checked = objMaster.Current.DisableAdvanceText.ToBool();

            chkVatOnlyOnAdminFees.Checked = objMaster.Current.VatOnlyOnAdminFees.ToBool();
            //18/may/16
            chkIsAmountWiseComm.Checked = objMaster.Current.IsAmountWiseComm.ToBool();
            ddlCompanyGroup.SelectedValue = objMaster.Current.GroupId;

            ddlSubCompany.SelectedValue = objMaster.Current.SubCompanyId;
            //if (chkDayAndNightWise.Checked)
            //{
            //    if (chkIsAmountWiseComm.Checked)
            //    {
            //        numAgentCommission.Minimum = 0;
            //        numAgentCommission.Maximum = 1000;
            //        numAgentCommission.Value = objMaster.Current.CommissionPerBooking.ToDecimal();
            //    }
            //    else
            //    {
            //        numAgentCommission.Minimum = 0;
            //        numAgentCommission.Maximum = 100;
            //        numAgentCommission.Value = objMaster.Current.CommissionPerBooking.ToDecimal();
            //    }
            //    if (chkNightIsAmountWiseComm.Checked)
            //    {
            //        numNightAgentCommission.Minimum = 0;
            //        numNightAgentCommission.Maximum = 1000;
            //        numNightAgentCommission.Value = objMaster.Current.NightAgentCommission.ToDecimal();
            //    }
            //    else
            //    {
            //        numNightAgentCommission.Minimum = 0;
            //        numNightAgentCommission.Maximum = 100;
            //        numNightAgentCommission.Value = objMaster.Current.NightAgentCommission.ToDecimal();
            //    }
            //}
            //else
            //{
            if (chkIsAmountWiseComm.Checked)
            {
                numAgentCommission.Minimum = 0;
                numAgentCommission.Maximum = 1000;
                numAgentCommission.Value = objMaster.Current.CommissionPerBooking.ToDecimal();
            }
            else
            {
                numAgentCommission.Minimum = 0;
                numAgentCommission.Maximum = 100;
                numAgentCommission.Value = objMaster.Current.CommissionPerBooking.ToDecimal();
            }

            //  numNightAgentCommission.Minimum = 0;
            //  numNightAgentCommission.Maximum = 100;
            //  numNightAgentCommission.Value = objMaster.Current.NightAgentCommission.ToDecimal();
            //     }
            // Adil 28/5/13

            if (!string.IsNullOrEmpty(objMaster.Current.BackgroundColor))
            {
                Color clr = Color.FromArgb(objMaster.Current.BackgroundColor.ToInt());

                (txtBgColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                txtBgColor.Tag = clr.ToArgb();
            }

            if (!string.IsNullOrEmpty(objMaster.Current.TextColor))
            {
                Color clr = Color.FromArgb(objMaster.Current.TextColor.ToInt());

                (txtTextColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                txtTextColor.Tag = clr.ToArgb();
            }




            //   txtCompanyNo.Text = objMaster.Current.CompanyNumber.ToStr();
            //    txtVATNo.Text = objMaster.Current.VATNumber.ToStr();

            GridViewRowInfo row = null;

            ClearDepartments();

            foreach (var item in objMaster.Current.Gen_Company_Departments.OrderBy(c=>c.DepartmentName))
            {
                row = grdDepartment.Rows.AddNew();

                row.Cells[COLS.Id].Value = item.Id;
                row.Cells[COLS.MasterId].Value = item.CompanyId;
                row.Cells[COLS.DepartmentName].Value = item.DepartmentName;
                row.Cells[COLS.Pickup].Value = item.ComapanyFromAddress.ToStr();
                row.Cells[COLS.DropOff].Value = item.ComapnyToAddress.ToStr();
            }

            ClearCostCenters();



            foreach (var item in General.GetQueryable<Gen_Company_CostCenter>(c => c.CompanyId == objMaster.Current.Id))
            {
                row = grdCostCenter.Rows.AddNew();

                row.Cells[COLS_CC.Id].Value = item.Id;
                row.Cells[COLS_CC.MasterId].Value = item.CompanyId;
                row.Cells[COLS_CC.CostCenterName].Value = item.CostCenterName;
            }

            ClearContacts();

            foreach (var item in General.GetQueryable<Gen_Company_Contact>(c => c.CompanyId == objMaster.Current.Id))
            {
                row = grdContacts.Rows.AddNew();

                row.Cells[COLS_Contacts.Id].Value = item.Id;
                row.Cells[COLS_Contacts.MasterId].Value = item.CompanyId;
                row.Cells[COLS_Contacts.ContactName].Value = item.ContactName.ToStr();
                row.Cells[COLS_Contacts.Email].Value = item.Email.ToStr();

                row.Cells[COLS_Contacts.TelephoneNo].Value = item.TelephoneNo.ToStr();
                row.Cells[COLS_Contacts.MobileNo].Value = item.MobileNo.ToStr();
                row.Cells[COLS_Contacts.Primary].Value = item.IsDefault.ToBool();
                //  row.Cells[COLS_Contacts.Password].Value = item.Passwrd.ToStr();

            }


            ClearWebAccounts();


            foreach (var item in General.GetQueryable<Gen_Company_WebAccount>(c => c.CompanyId == objMaster.Current.Id))
            {
                row = grdWebLogin.Rows.AddNew();

                row.Cells[COLS_WebLogin.Id].Value = item.Id;
                row.Cells[COLS_WebLogin.MasterId].Value = item.CompanyId;
                row.Cells[COLS_WebLogin.AccountNo].Value = item.AccountNo.ToStr();
                row.Cells[COLS_WebLogin.LoginId].Value = item.LoginId.ToStr();

                row.Cells[COLS_WebLogin.TelephoneNo].Value = item.TelephoneNo.ToStr();
                row.Cells[COLS_WebLogin.MobileNo].Value = item.MobileNo.ToStr();
                row.Cells[COLS_WebLogin.IsActive].Value = item.IsActive.ToBool();
                row.Cells[COLS_WebLogin.Password].Value = item.Passwrd.ToStr();

            }



            ClearOrderNos();

            foreach (var item in objMaster.Current.Gen_Company_OrderNumbers)
            {
                row = grdOrderNo.Rows.AddNew();

                row.Cells[COLS.Id].Value = item.Id;
                row.Cells[COLS.MasterId].Value = item.CompanyId;
                row.Cells["OrderNo"].Value = item.OrderNo;
            }

            if (grdCompanyAddress != null)
            {
                ClearAddresses();
                foreach (var item in objMaster.Current.Gen_Company_Addresses)
                {
                    row = grdCompanyAddress.Rows.AddNew();
                    row.Cells[COLS_Adress.Id].Value = item.Id;
                    row.Cells[COLS_Adress.MasterId].Value = item.CompanyId;
                    row.Cells[COLS_Adress.Address].Value = item.Address;

                }

            }

            var ComapnyPaymetTypesList=(from a in objMaster.Current.Gen_Company_PaymentTypes
                                            select new 
                                            {
                                            Id=a.Id,
                                            PaymentTypeId=a.PaymentTypeId
                                            }).ToList();
           
            for (int i = 0; i < grdPaymentTypes.RowCount; i++)
            {
                bool IsChecked = false;
                foreach (var item in ComapnyPaymetTypesList)
                {
                    if (item.PaymentTypeId == grdPaymentTypes.Rows[i].Cells[COLS_PaymentTypes.PaymentTypeId].Value.ToInt())
                    {
                        IsChecked = true;
                        break;
                    }
                    else
                    {
                        IsChecked = false;
                    }
                }
                grdPaymentTypes.Rows[i].Cells[COLS_PaymentTypes.Check].Value = IsChecked;
            }

            if (HasAdditonalCharges)
            {

                var ComapnyChargesTypesList = (from a in objMaster.Current.Gen_Company_ExtraCharges
                                               select new
                                               {
                                                   Id = a.Id,
                                                   PaymentTypeId = a.Charges
                                               }).ToList();

                for (int i = 0; i < grdAdditionalCharges.RowCount; i++)
                {
                    bool IsChecked = false;
                    foreach (var item in ComapnyChargesTypesList)
                    {
                        if (item.PaymentTypeId == grdAdditionalCharges.Rows[i].Cells[COLS_PaymentTypes.PaymentTypeId].Value.ToInt())
                        {
                            IsChecked = true;
                            break;
                        }
                        else
                        {
                            IsChecked = false;
                        }
                    }

                    grdAdditionalCharges.Rows[i].Cells[COLS_PaymentTypes.Check].Value = IsChecked;
                }
            }



            //List<Gen_Location> listofLocation = null;

            //if (objMaster.Current.Gen_Company_AgentCommissions.Count > 0)
            //    listofLocation = General.GetQueryable<Gen_Location>(c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT || c.LocationTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION).ToList();

            if (grdAirportCommission != null)
            {
                ClearAirportCommissionGrid();

                //  grdAirportCommission.BeginUpdate();
                foreach (var item in objMaster.Current.Gen_Company_AgentCommissions.Where(c => c.LocationTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT))
                {
                    row = grdAirportCommission.Rows.AddNew();
                    row.Cells[COLS_AirportCommission.Id].Value = item.Id;
                    row.Cells[COLS_AirportCommission.MasterId].Value = item.CompanyId;
                    row.Cells[COLS_AirportCommission.FareId].Value = item.FareId;

                    row.Cells[COLS_AirportCommission.Location].Value = item.LocationId != null ? item.Gen_Location.LocationName : "";
                    row.Cells[COLS_AirportCommission.LocationId].Value = item.LocationId;
                    row.Cells[COLS_AirportCommission.LocationTypeId].Value = item.LocationTypeId;
                    row.Cells[COLS_AirportCommission.CommissionPercent].Value = item.CommissionPercent;
                    row.Cells[COLS_AirportCommission.CommissionAmount].Value = item.CommissionAmount;
                    row.Cells[COLS_AirportCommission.CommissionOnPercent].Value = item.CommissionOnPercent;
                    row.Cells[COLS_AirportCommission.DayWise].Value = item.DayWise;
                    row.Cells[COLS_AirportCommission.NightWise].Value = item.NightWise;
                    row.Cells[COLS_AirportCommission.VehicleTypeId].Value = item.VehicleTypeId;
                    row.Cells[COLS_AirportCommission.VehicleType].Value = item.VehicleTypeId != null ? item.Fleet_VehicleType.VehicleType.ToStr() : "";
                    row.Cells[COLS_AirportCommission.CompanyPrice].Value = item.CompanyPrice;
                    row.Cells[COLS_AirportCommission.DriverPrice].Value = item.DriverPrice;
                    row.Cells[COLS_AirportCommission.CustomerPrice].Value = item.CustomerPrice;
                }

                //  grdAirportCommission.EndUpdate();
            }


            if (AppVars.listUserRights.Count(c => c.formName == "frmCompany" && c.functionId == "SHOW FARES TAB") > 0)
            {

                ClearFixedFareGrid();
                //int FareId = objMaster.Current.Fares.Count > 0 ? objMaster.Current.Fares.FirstOrDefault().Id : 0;

                var listFixedFare = (from a in General.GetQueryable<Fare_ChargesDetail>(c => c.Fare.CompanyId == objMaster.Current.Id)
                                     select new
                                     {
                                         Id = a.Id,
                                         FareId = a.FareId,
                                         CompanyId = a.Fare.CompanyId,
                                         VehicleType = a.Fare.VehicleTypeId != null ? a.Fare.Fleet_VehicleType.VehicleType : "",
                                         FromAddress = a.FromAddress,
                                         ToAddress = a.ToAddress,
                                         FromLocTypeId = a.OriginId,
                                         ToLocTypeId = a.DestinationId,
                                         Rate = a.Rate,
                                         a.Fare.VehicleTypeId
                                     }).ToList();

                if (grdFixedFare == null)
                    return;
                grdFixedFare.RowCount = listFixedFare.Count;

                grdFixedFare.BeginUpdate();
                for (int i = 0; i < grdFixedFare.RowCount; i++)
                {

                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.Id].Value = listFixedFare[i].Id;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.FareId].Value = listFixedFare[i].FareId;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.MasterId].Value = listFixedFare[i].CompanyId;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.VehicleType].Value = listFixedFare[i].VehicleType;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.VehicleTypeId].Value = listFixedFare[i].VehicleTypeId;

                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.FromLocationTypeId].Value = listFixedFare[i].FromLocTypeId;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.FromAddress].Value = listFixedFare[i].FromAddress;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.ToLocationTypeId].Value = listFixedFare[i].ToLocTypeId;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.ToAddress].Value = listFixedFare[i].ToAddress;
                    grdFixedFare.Rows[i].Cells[COLS_FixedFare.Rate].Value = listFixedFare[i].Rate;
                }

                grdFixedFare.EndUpdate();

                ClearMileageSettingGrid();

                var ListMileageSetting = (from a in General.GetQueryable<Fare_OtherCharge>(c => c.Fare.CompanyId == objMaster.Current.Id)
                                          select new
                                          {
                                              Id = a.Id,
                                              FareId = a.FareId,
                                              CompanyId = a.Fare.CompanyId,
                                              VehicleType = a.Fare.VehicleTypeId != null ? a.Fare.Fleet_VehicleType.VehicleType : "",
                                              FromMile = a.FromMile,
                                              ToMile = a.ToMile,
                                              Rate = a.Rate
                                          }).ToList();


                if (grdMileageSetting == null)
                    return;
                grdMileageSetting.RowCount = ListMileageSetting.Count;

                for (int i = 0; i < grdMileageSetting.RowCount; i++)
                {
                    grdMileageSetting.Rows[i].Cells[COLS_MileageSetting.Id].Value = ListMileageSetting[i].Id;
                    grdMileageSetting.Rows[i].Cells[COLS_MileageSetting.FareId].Value = ListMileageSetting[i].FareId;
                    grdMileageSetting.Rows[i].Cells[COLS_MileageSetting.MasterId].Value = ListMileageSetting[i].CompanyId;
                    grdMileageSetting.Rows[i].Cells[COLS_MileageSetting.VehicleType].Value = ListMileageSetting[i].VehicleType;
                    grdMileageSetting.Rows[i].Cells[COLS_MileageSetting.FromMile].Value = ListMileageSetting[i].FromMile;
                    grdMileageSetting.Rows[i].Cells[COLS_MileageSetting.ToMile].Value = ListMileageSetting[i].ToMile;
                    grdMileageSetting.Rows[i].Cells[COLS_MileageSetting.Rate].Value = ListMileageSetting[i].Rate;
                }

            }
            if (AppVars.listUserRights.Count(c => c.formName == "frmCompany" && c.functionId == "SHOW BOOKEDBY TAB") > 0 && chkHasBookedBy.Checked)
            {
                if (grdBookedBy != null)
                {
                    ClearBookedBy();
                    foreach (var item in objMaster.Current.Gen_Company_BookedBies)
                    {
                        row = grdBookedBy.Rows.AddNew();
                        row.Cells[COLS_BookedBy.Id].Value = item.Id;
                        row.Cells[COLS_BookedBy.MasterId].Value = item.CompanyId;
                        row.Cells[COLS_BookedBy.BookedBy].Value = item.BookedBy;
                        row.Cells[COLS_BookedBy.Email].Value = item.EmailAddress.ToStr();

                    }
                }
            }
            else
            {
                this.BookedBy.Item.Visibility = ElementVisibility.Collapsed;
            }
            try
            {

                int? Id = objMaster.Current.Id;
                var list = (from a in General.GetQueryable<Complaint>(c => c.CompanyId == Id)
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

                    grdLister.DataSource = list;



                    //AddComplaintButton();
                }
            }
            catch
            {


            }


        }


        private void ClearOrderNos()
        {
            grdOrderNo.Rows.Clear();

        }
        private void ClearFixedFareGrid()
        {
            if (grdFixedFare == null)
                return;
            grdFixedFare.Rows.Clear();
        }
        private void ClearMileageSettingGrid()
        {
            if (grdMileageSetting == null)
                return;
            grdMileageSetting.Rows.Clear();
        }
        private void ClearAddresses()
        {
            if (grdCompanyAddress == null)
                return;
            grdCompanyAddress.Rows.Clear();
        }

        private void ClearWebAccounts()
        {
            grdWebLogin.Rows.Clear();

        }

        private void ClearDepartments()
        {
            grdDepartment.Rows.Clear();

        }

        private void ClearBookedBy()
        {
            if (grdBookedBy == null)
                return;
            grdBookedBy.Rows.Clear();
        }
        private void ClearCostCenters()
        {
            grdCostCenter.Rows.Clear();

        }

        private void ClearContacts()
        {
            grdContacts.Rows.Clear();

        }
        private void ClearAirportCommissionGrid()
        {
            if (grdAirportCommission == null)
                return;
            grdAirportCommission.Rows.Clear();
        }

        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
            chkPupilNo.Checked = false;

            chkOrderNo.Checked = false;
            chkSingleOrderNo.Checked = false;
            chkHasVAT.Checked = false;
            chkPreferredEmails.Checked = false;
            chkCompanyPriceAmountWise.Checked = false;
            txtCompanyName.Focus();
            ClearDepartments();
            ClearCostCenters();
            ClearContacts();
            ClearWebAccounts();
            numAdminFees.Value = 0;
            txtInformation.Text = string.Empty;
            ddlAccountType.SelectedValue = Enums.ACCOUNT_TYPE.ACCOUNT;


            //pageComplaint.Item.Visibility = ElementVisibility.Collapsed;
            ClearOrderNos();
            ClearAddresses();
            ClearBookedBy();
            ClearFixedFareGrid();
            ClearMileageSettingGrid();


            if (ddlSubCompany.Items.Count == 1)
                ddlSubCompany.SelectedIndex = 0;

            objMaster.Clear();

        }


        public override void Save()
        {
            if (OnSave())
            {
                General.RefreshListWithoutSelected<frmCompanyList>("frmCompanyList1");


            }

        }


        public bool OnSave()
        {
            try
            {
                if (ddlSubCompany.SelectedValue == null && ddlSubCompany.Items.Count > 1)
                {
                    ENUtils.ShowMessage("Required : SubCompany");
                    return false;


                }

               

                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }

                objMaster.Current.CompanyName = txtCompanyName.Text.Trim();
                objMaster.Current.Email = txtEmail.Text.Trim();
                objMaster.Current.ContactName = txtContactName.Text.Trim();
                objMaster.Current.Address = txtAddress.Text.Trim();
                objMaster.Current.TelephoneNo = txtPhone.Text.Trim();
                objMaster.Current.MobileNo = txtMobileNo.Text.Trim();
                objMaster.Current.WebsiteUrl = txtWebsite.Text.Trim();
                objMaster.Current.Fax = txtFax.Text.Trim();

                objMaster.Current.IsClosed = chkClosed.Checked;

                objMaster.Current.HasOrderNo = chkOrderNo.Checked;
                objMaster.Current.HasSingleOrderNo = chkSingleOrderNo.Checked;
                objMaster.Current.HasPupilNo = chkPupilNo.Checked;
                objMaster.Current.AdminFees = numAdminFees.Value.ToInt();
                objMaster.Current.HasComcabCharges = chkIsComcabCharges.Checked;
                objMaster.Current.HasEscort = chkHasEscort.Checked;
                objMaster.Current.DiscountPercentage = numDiscount.Value.ToDecimal();

                objMaster.Current.FareDeductionPercent = numFareDeductPer.Value.ToDecimal();

                objMaster.Current.HasVat = chkHasVAT.Checked;

                objMaster.Current.HasBookedBy = chkHasBookedBy.Checked;

                objMaster.Current.CompanyCode = txtCompanyCode.Text.ToStr();

                objMaster.Current.AccountTypeId = ddlAccountType.SelectedValue.ToInt();

                objMaster.Current.AdminFeeType = ddlAdminFeeType.Text.Trim();

                objMaster.Current.SubCompanyId = ddlSubCompany.SelectedValue.ToIntorNull();

                objMaster.Current.CompanyPricePercent = numCompanyPricePercent.Value.ToInt();

                objMaster.Current.AccountNo = txtAccountNo.Text.Trim();
                objMaster.Current.WebLoginId = txtLoginID.Text.Trim();
                objMaster.Current.WebLoginPassword = txtPassword.Text.Trim();
                objMaster.Current.IsWebLoginActive = chkActivateLogin.Checked;

                //  objMaster.Current.CompanyNumber = txtCompanyNo.Text.Trim();
                //  objMaster.Current.VATNumber = txtVATNo.Text.Trim();
                objMaster.Current.BackgroundColor = txtBgColor.Tag.ToStr();
                objMaster.Current.TextColor = txtTextColor.Tag.ToStr();
                objMaster.Current.PasswordAccount = txtPasswordAccount.Text.Trim();
                objMaster.Current.PasswordEnable = chkPasswordAccount.Checked;
                objMaster.Current.DisableArrivalText = chkDisableArrivalText.Checked;
                objMaster.Current.DisableCustomerText = chkDisableCustomerText.Checked;
                objMaster.Current.ResetAllFares = chkResetAllFares.Checked;
                objMaster.Current.VatOnlyOnAdminFees = chkVatOnlyOnAdminFees.Checked;
                objMaster.Current.IsAgent = chkAgent.Checked;
                //objMaster.Current.CommissionPerBooking = numAgentCommission.Value.ToDecimal();
              //  objMaster.Current.HasRoomCharge = chkHasRoomCharge.Checked;

                objMaster.Current.DisableCompanyFaresForController = chkDisableCompanyFares.Checked;
                objMaster.Current.DisableAdvanceText = chkDisableAdvanceText.Checked;

                objMaster.Current.CreditCardDetails = txtCreditCard.Text.Trim();

                objMaster.Current.PreferredEmails = chkPreferredEmails.Checked;
                objMaster.Current.MandatoryOrderNo = chkMandatoryOrderNo.Checked;
                objMaster.Current.DriverFareReductionType = ddlDrvFareReductionType.Text.Trim();
                objMaster.Current.DriverFareReductionValue = numDrvFareReductionValue.Value.ToDecimal();

                objMaster.Current.CompanyInformation = txtInformation.Text.Trim();
                //

                objMaster.Current.IsAmountWiseComm = chkIsAmountWiseComm.Checked;
                objMaster.Current.CommissionPerBooking = numAgentCommission.Value.ToDecimal();
                objMaster.Current.GroupId = ddlCompanyGroup.SelectedValue.ToIntorNull();


                objMaster.Current.HasRoomCharge = chkCompanyPriceAmountWise.Checked;

                string[] skipProperties = { "Gen_Company" };
                IList<Gen_Company_OrderNumber> savedList = objMaster.Current.Gen_Company_OrderNumbers;
                List<Gen_Company_OrderNumber> listofDetail = (from r in grdOrderNo.Rows
                                                              select new Gen_Company_OrderNumber
                                                              {
                                                                  Id = r.Cells["ID"].Value.ToInt(),
                                                                  OrderNo = r.Cells["OrderNo"].Value.ToStr().Trim(),
                                                                  CompanyId = r.Cells["MasterId"].Value.ToInt()

                                                              }).ToList();


                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

                //Hyde Park


                string[] skipPaymentProperties = { "Gen_Company", "Gen_PaymentType" };
                IList<Gen_Company_PaymentType> savedPaymentList = objMaster.Current.Gen_Company_PaymentTypes;
                List<Gen_Company_PaymentType> listofPaymentDetail = (from r in grdPaymentTypes.Rows.Where(c => c.Cells[COLS_PaymentTypes.Check].Value.ToBool() == true)
                                                                     select new Gen_Company_PaymentType
                                                                 {
                                                                     Id = r.Cells[COLS_PaymentTypes.Id].Value.ToInt(),
                                                                     CompanyId = r.Cells[COLS_PaymentTypes.MasterId].Value.ToInt(),
                                                                     PaymentTypeId = r.Cells[COLS_PaymentTypes.PaymentTypeId].Value.ToIntorNull(),


                                                                 }).ToList();
                Utils.General.SyncChildCollection(ref savedPaymentList, ref listofPaymentDetail, "Id", skipPaymentProperties);

               
                if(HasAdditonalCharges)
                {
                    objMaster.Current.ShowExtraCharges = true;

                    string[] skipChargesProperties = { "Gen_Company", "Gen_Charge" };
                    IList<Gen_Company_ExtraCharge> savedChargesList = objMaster.Current.Gen_Company_ExtraCharges;
                    List<Gen_Company_ExtraCharge> listofChargesDetail = (from r in grdAdditionalCharges.Rows.Where(c => c.Cells[COLS_PaymentTypes.Check].Value.ToBool() == true)
                                                                         select new Gen_Company_ExtraCharge
                                                                         {
                                                                             Id = r.Cells[COLS_PaymentTypes.Id].Value.ToInt(),
                                                                             CompanyId = r.Cells[COLS_PaymentTypes.MasterId].Value.ToInt(),
                                                                             Charges = r.Cells[COLS_PaymentTypes.PaymentTypeId].Value.ToIntorNull(),


                                                                         }).ToList();
                    Utils.General.SyncChildCollection(ref savedChargesList, ref listofChargesDetail, "Id", skipChargesProperties);
                }

                if (grdAirportCommission != null)
                {

                    StringBuilder query = new StringBuilder();

                    foreach (var item in grdAirportCommission.Rows.Where(c => c.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal() > 0))
                    {
                        var fixfaresRows = grdFixedFare.Rows.Where(c => c.Cells[COLS_FixedFare.FromLocationTypeId].Value.ToInt() == item.Cells[COLS_AirportCommission.LocationId].Value.ToInt()
                                                                              && c.Cells[COLS_FixedFare.VehicleTypeId].Value.ToInt() == item.Cells[COLS_AirportCommission.VehicleTypeId].Value.ToInt());

                        foreach (var item2 in fixfaresRows)
                        {
                            item2.Cells[COLS_FixedFare.Rate].Value = item.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();



                            query.Append("update Fare_ChargesDetails set Rate=" + item2.Cells[COLS_FixedFare.Rate].Value.ToDecimal() + " where id=" + item2.Cells[COLS_FixedFare.Id].Value.ToInt() + ";");


                        }


                        fixfaresRows = grdFixedFare.Rows.Where(c => c.Cells[COLS_FixedFare.ToLocationTypeId].Value.ToInt() == item.Cells[COLS_AirportCommission.LocationId].Value.ToInt()
                                && c.Cells[COLS_FixedFare.VehicleTypeId].Value.ToInt() == item.Cells[COLS_AirportCommission.VehicleTypeId].Value.ToInt());

                        foreach (var item2 in fixfaresRows)
                        {
                            item2.Cells[COLS_FixedFare.Rate].Value = item.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal();

                            query.Append("update Fare_ChargesDetails set Rate=" + item2.Cells[COLS_FixedFare.Rate].Value.ToDecimal() + " where id=" + item2.Cells[COLS_FixedFare.Id].Value.ToInt() + ";");

                        }
                    }


                    try
                    {

                        string queryLen = query.ToString();

                        if (queryLen.Length > 0)
                        {

                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                db.stp_RunProcedure(queryLen);

                            }


                        }
                    }
                    catch
                    {


                    }



                    string[] skipCommissionProperties = { "Fare", "Gen_Company", "Gen_Location", "Fleet_VehicleType" };

                    IList<Gen_Company_AgentCommission> savedListCommission = objMaster.Current.Gen_Company_AgentCommissions;
                    List<Gen_Company_AgentCommission> listofDetailCommission = (from r in grdAirportCommission.Rows
                                                                                select new Gen_Company_AgentCommission
                                                                                {
                                                                                    Id = r.Cells[COLS_AirportCommission.Id].Value.ToInt(),
                                                                                    CommissionPercent = r.Cells[COLS_AirportCommission.CommissionOnPercent].Value.ToBool() == true ? r.Cells[COLS_AirportCommission.CommissionPercent].Value.ToInt() : 0,
                                                                                    CompanyId = objMaster.Current.Id,//r.Cells[COLS_AirportCommission.MasterId].Value.ToInt(),
                                                                                    LocationTypeId = Enums.LOCATION_TYPES.AIRPORT, //r.Cells[COLS_AirportCommission.LocationTypeId].Value.ToInt(),
                                                                                    LocationId = r.Cells[COLS_AirportCommission.LocationId].Value.ToIntorNull(),
                                                                                    CommissionAmount = r.Cells[COLS_AirportCommission.CommissionOnPercent].Value.ToBool() != true ? r.Cells[COLS_AirportCommission.CommissionAmount].Value.ToDecimal() : 0,
                                                                                    CommissionOnPercent = r.Cells[COLS_AirportCommission.CommissionOnPercent].Value.ToBool(),
                                                                                    DayWise = r.Cells[COLS_AirportCommission.DayWise].Value.ToBool(),
                                                                                    NightWise = r.Cells[COLS_AirportCommission.NightWise].Value.ToBool(),
                                                                                    VehicleTypeId = r.Cells[COLS_AirportCommission.VehicleTypeId].Value.ToIntorNull(),
                                                                                    FareId = r.Cells[COLS_AirportCommission.FareId].Value.ToIntorNull(),

                                                                                    CompanyPrice = r.Cells[COLS_AirportCommission.CompanyPrice].Value.ToDecimal(),
                                                                                    DriverPrice = r.Cells[COLS_AirportCommission.DriverPrice].Value.ToDecimal(),
                                                                                    CustomerPrice = r.Cells[COLS_AirportCommission.CustomerPrice].Value.ToDecimal(),
                                                                                }).ToList();

                    Utils.General.SyncChildCollection(ref savedListCommission, ref listofDetailCommission, "Id", skipCommissionProperties);
                }


                if(ddlSubCompany.Items.Count==1 && objMaster.Current.SubCompanyId==null)
                {
                    ddlSubCompany.SelectedIndex=0;
                    objMaster.Current.SubCompanyId = ddlSubCompany.SelectedValue.ToIntorNull();


                }



                objMaster.Save();




                GridViewRowInfo row = grdContacts.Rows.FirstOrDefault(c => c.Cells[COLS_Contacts.Primary].Value.ToBool());
                if (row != null)
                {
                    CompanyContactBO objContactBO = new CompanyContactBO();

                    try
                    {
                        objContactBO.GetByPrimaryKey(row.Cells[COLS_Contacts.Id].Value.ToInt());
                        if (objContactBO.PrimaryKeyValue == null)
                        {
                            objContactBO.New();
                        }
                        else
                            objContactBO.Edit();


                        objContactBO.Current.CompanyId = objMaster.Current.Id;
                        objContactBO.Current.ContactName = txtContactName.Text.Trim();
                        objContactBO.Current.Email = txtEmail.Text.Trim();
                        objContactBO.Current.TelephoneNo = txtPhone.Text.Trim();
                        objContactBO.Current.MobileNo = txtMobileNo.Text.Trim();
                        objContactBO.Current.IsDefault = true;
                        //      objContactBO.Current.Passwrd = txtPassword.Text.Trim();

                        objContactBO.CheckDataValidation = false;

                        objContactBO.Save();

                    }
                    catch (Exception ex)
                    {
                        if (objContactBO.Errors.Count > 0)
                            ENUtils.ShowMessage(objContactBO.ShowErrors());
                        else
                        {
                            ENUtils.ShowMessage(ex.Message);

                        }
                        return false;
                    }
                }

                
                

                return true;

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
                return false;
            }


        }


    
      



        #endregion

        private void frmCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
            GC.Collect();
        }

        private bool _SaveFromDepartment;

        public bool SaveFromDepartment
        {
            get { return _SaveFromDepartment; }
            set { _SaveFromDepartment = value; }
        }



        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            SaveFromDepartment = false;
            bool saved = true;
            if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                            + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    SaveFromDepartment = true;
                    saved = OnSave();
                }
                else
                    saved = false;

            }

            if (saved == false) return;

            frmCompanyDepartments frm = new frmCompanyDepartments(objMaster.PrimaryKeyValue.ToInt());
            frm.ShowDialog();

            if (frm.Saved)
            {
                frm.Dispose();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                DisplayRecord();
            }



        }

        private void btnAddCostCenter_Click(object sender, EventArgs e)
        {

            bool saved = true;
            if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                            + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    saved = OnSave();
                }
                else
                    saved = false;

            }

            if (saved == false) return;

            frmCompanyCostCenter frm = new frmCompanyCostCenter(objMaster.PrimaryKeyValue.ToInt());
            frm.ShowDialog();

            if (frm.Saved)
            {
                frm.Dispose();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                DisplayRecord();
            }

        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            bool saved = true;
            if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                            + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    saved = OnSave();
                }
                else
                    saved = false;

            }

            if (saved == false) return;

            frmCompanyContacts frm = new frmCompanyContacts(objMaster.PrimaryKeyValue.ToInt());
            frm.ShowDialog();

            if (frm.Saved)
            {
                frm.Dispose();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                DisplayRecord();
            }
        }


        private void SetPrimaryContact()
        {

            GridViewRowInfo row = grdContacts.Rows.FirstOrDefault(c => c.Cells[COLS_Contacts.Primary].Value.ToBool());

            if (row == null)
                row = grdContacts.Rows.AddNew();


            row.Cells[COLS_Contacts.Primary].Value = true;
            row.Cells[COLS_Contacts.ContactName].Value = txtContactName.Text.Trim();
            row.Cells[COLS_Contacts.Email].Value = txtEmail.Text.Trim();
            row.Cells[COLS_Contacts.TelephoneNo].Value = txtPhone.Text.Trim();
            row.Cells[COLS_Contacts.MobileNo].Value = txtMobileNo.Text.Trim();

        }

        private void txtPhone_Validated(object sender, EventArgs e)
        {
            SetPrimaryContact();
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            SetPrimaryContact();

        }

        private void txtContactName_Validated(object sender, EventArgs e)
        {
            SetPrimaryContact();

        }

        private void txtMobileNo_Validated(object sender, EventArgs e)
        {
            SetPrimaryContact();

        }



        private void chkPasswordAccount_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkPasswordAccount.Checked)
            {
                txtPasswordAccount.Enabled = true;
            }
            else
            {
                txtPasswordAccount.Text = string.Empty;
                txtPasswordAccount.Enabled = false;
            }

        }

        private void btnPickBgColor_Click(object sender, EventArgs e)
        {
            SetColor(txtBgColor);
        }

        private void btnClearBgColor_Click(object sender, EventArgs e)
        {
            ClearColor(txtBgColor);
        }

        private void btnPickTextColor_Click(object sender, EventArgs e)
        {
            SetColor(txtTextColor);
        }

        private void btnClearTextColor_Click(object sender, EventArgs e)
        {
            ClearColor(txtTextColor);
        }

        // Adil 28/5/13
        private void SetColor(RadTextBox txt)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {

                (txt.RootElement.Children[0] as RadTextBoxElement).BackColor = colorDialog1.Color;
                txt.Tag = colorDialog1.Color.ToArgb();
            }

        }
        private void ClearColor(RadTextBox txt)
        {

            (txt.RootElement.Children[0] as RadTextBoxElement).BackColor = Color.White;
            txt.Tag = null;


        }

        private void frmCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void btnAddNewWebLogin_Click(object sender, EventArgs e)
        {
            /// SaveFromDepartment = false;
            bool saved = true;
            if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                            + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    // SaveFromDepartment = true;
                    saved = OnSave();
                }
                else
                    saved = false;

            }

            if (saved == false) return;

            frmCompanyWebLogin frm = new frmCompanyWebLogin(objMaster.PrimaryKeyValue.ToInt());
            frm.ShowDialog();

            if (frm.Saved)
            {
                frm.Dispose();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                DisplayRecord();
                Save();
            }
        }

        private void ddlAdminFeeType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlAdminFeeType.SelectedValue.ToStr() == "Percent")
            {
                numAdminFees.Maximum = 100;

            }
            else
            {
                numAdminFees.Maximum = 100000;

            }
        }





        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }
        private void New()
        {
            txtCompanyName.Text = "";
            txtCompanyCode.Text = "";
            //txtAddress.Text = "";
            txtAccountNo.Text = "";
            txtContactName.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtLoginID.Text = "";
            txtMobileNo.Text = "";
            txtPassword.Text = "";
            txtPasswordAccount.Text = "";
            ddlAccountType.Text = "";
            txtPhone.Text = "";
            txtTextColor.Text = "";
            txtWebsite.Text = "";
            // objMaster.Clear();
            grdContacts.Rows.Clear();
            grdCostCenter.Rows.Clear();
            grdDepartment.Rows.Clear();
            grdWebLogin.Rows.Clear();
            grdCompanyAddress.Rows.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void chkClosed_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ddlDrvFareReductionType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlAdminFeeType.SelectedValue.ToStr() == "Percent")
            {
                numDrvFareReductionValue.Maximum = 100;

            }
            else
            {
                numDrvFareReductionValue.Maximum = 100000;

            }
        }

        private void btnCompanyAddresses_Click(object sender, EventArgs e)
        {
            bool saved = true;
            if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                            + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    // SaveFromDepartment = true;
                    saved = OnSave();
                }
                else
                    saved = false;

            }

            if (saved == false) return;

            frmCompanyAddresses frm = new frmCompanyAddresses(objMaster.PrimaryKeyValue.ToInt());
            frm.ShowDialog();

            if (frm.Saved)
            {
                frm.Dispose();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                DisplayRecord();
                Save();
            }
        }

        private void chkHasVAT_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkHasVAT.Checked)
            {
                chkVatOnlyOnAdminFees.Enabled = true;
            }
            else
            {
                chkVatOnlyOnAdminFees.Checked = false;
                chkVatOnlyOnAdminFees.Enabled = false;
            }
        }


        private void btnBookedBy_Click(object sender, EventArgs e)
        {
            try
            {
                bool saved = true;
                if (objMaster.Current == null || objMaster.PrimaryKeyValue == null)
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Company Information is not saved " + Environment.NewLine
                                                + "Click on 'yes' to Save Company Information First", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        SaveFromDepartment = true;
                        saved = OnSave();
                    }
                    else
                        saved = false;

                }

                if (saved == false) return;

                if (objMaster.Current.HasBookedBy.ToBool())
                {
                    frmCompanyBookedBy frm = new frmCompanyBookedBy(objMaster.PrimaryKeyValue.ToInt());
                    frm.ShowDialog();

                    if (frm.Saved)
                    {
                        frm.Dispose();

                        objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                        DisplayRecord();
                        Save();
                    }
                }
                else
                {
                    ENUtils.ShowMessage("Has Booked By is not saved");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }

        private void chkHasBookedBy_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkHasBookedBy.Checked)
            {
                if (AppVars.listUserRights.Count(c => c.formName == "frmCompany" && c.functionId == "SHOW BOOKEDBY TAB") > 0)
                {
                    this.BookedBy.Item.Visibility = ElementVisibility.Visible;
                }
                else
                {
                    this.BookedBy.Item.Visibility = ElementVisibility.Collapsed;

                }
            }
            else
            {
                this.BookedBy.Item.Visibility = ElementVisibility.Collapsed;
            }
        }

        private void chkAgent_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            //{

            //    chkHasRoomCharge.Visible = true;
            //}
            //else
            //{
            //    chkHasRoomCharge.Checked = false;
            //    chkHasRoomCharge.Visible = false;


            //}
        }

        private void chkIsAmountWiseComm_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkIsAmountWiseComm.Checked)
            {
                lblCommPercent.Visible = false;
                //    lblCommission.Text = "Day Commission";
                numAgentCommission.Minimum = 0;
                numAgentCommission.Maximum = 1000;

            }
            else
            {
                //  lblCommission.Text = "Commission";
                lblCommPercent.Visible = true;
                numAgentCommission.Minimum = 0;
                numAgentCommission.Maximum = 100;
            }
        }

        private void chkHasRoomCharge_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }

        private void ddlAccountType_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (ddlAccountType.SelectedValue.ToInt() == Enums.ACCOUNT_TYPE.ACCOUNT)
            //{
            //    chkHasRoomCharge.Checked = false;
            //    chkHasRoomCharge.Visible = false;

            //}
            //else
            //{

            //    chkHasRoomCharge.Visible = true;

            //}
        }

        private void chkCompanyPriceAmountWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                numCompanyPricePercent.DecimalPlaces = 2;
                radLabel27.Visible = false;

            }
            else
            {
                numCompanyPricePercent.DecimalPlaces = 0;
                radLabel27.Visible = true;
            }
        }

        private void btNpASTE_Click(object sender, EventArgs e)
        {
            PasteDetails();
        }

        private void PasteDetails()
        {

            try
            {
                Gen_Company c = General.GetObject<Gen_Company>(a => a.Id == CopiedCompanyId);


                if (c != null)
                {
                    txtCompanyCode.Text = c.CompanyCode.ToStr();
                    txtCompanyName.Text = c.CompanyName.ToStr();
                    txtAccountNo.Text = c.AccountNo.ToStr();
                    txtEmail.Text = c.Email.ToStr();
                    txtAddress.Text = c.Address.ToStr();
                    txtContactName.Text = c.ContactName.ToStr();
                    txtInformation.Text = c.CompanyInformation.ToStr();
                    txtPassword.Text = c.PasswordAccount.ToStr();
                    txtPhone.Text = c.TelephoneNo.ToStr();
                    txtMobileNo.Text = c.MobileNo.ToStr();
                    txtWebsite.Text = c.WebsiteUrl.ToStr();

                    txtLoginID.Text = c.WebLoginId.ToStr();
                    txtPassword.Text = c.WebLoginPassword.ToStr();
                    txtCreditCard.Text = c.CreditCardDetails.ToStr().Trim();

                    if (!string.IsNullOrEmpty(c.BackgroundColor))
                    {
                        Color clr = Color.FromArgb(c.BackgroundColor.ToInt());

                        (txtBgColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                        txtBgColor.Tag = clr.ToArgb();
                    }

                    if (!string.IsNullOrEmpty(c.TextColor))
                    {
                        Color clr = Color.FromArgb(c.TextColor.ToInt());

                        (txtTextColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                        txtTextColor.Tag = clr.ToArgb();
                    }


                    chkPasswordAccount.Checked = c.PasswordEnable.ToBool();
                    chkHasBookedBy.Checked = c.HasBookedBy.ToBool();
                    chkOrderNo.Checked = c.HasOrderNo.ToBool();
                    chkDisableAdvanceText.Checked = c.DisableAdvanceText.ToBool();
                    chkDisableArrivalText.Checked = c.DisableArrivalText.ToBool();
                    chkDisableCompanyFares.Checked = c.DisableCompanyFaresForController.ToBool();
                    chkDisableCustomerText.Checked = c.DisableCustomerText.ToBool();
                    chkHasVAT.Checked = c.HasVat.ToBool();
                    chkMandatoryOrderNo.Checked = c.MandatoryOrderNo.ToBool();
                    chkSingleOrderNo.Checked = c.HasSingleOrderNo.ToBool();
                    chkPupilNo.Checked = c.HasPupilNo.ToBool();
                    chkPreferredEmails.Checked = c.PreferredEmails.ToBool();
                    chkIsComcabCharges.Checked = objMaster.Current.HasComcabCharges.ToBool();
                    chkHasEscort.Checked = objMaster.Current.HasEscort.ToBool();
                    chkResetAllFares.Checked = c.ResetAllFares.ToBool();
                    chkActivateLogin.Checked = c.IsWebLoginActive.ToBool();
                    chkCompanyPriceAmountWise.Checked = c.HasRoomCharge.ToBool();
                    chkVatOnlyOnAdminFees.Checked = c.VatOnlyOnAdminFees.ToBool();
                    chkIsAmountWiseComm.Checked = c.IsAmountWiseComm.ToBool();
                    chkAgent.Checked = c.IsAgent.ToBool();
                    chkMandatoryOrderNo.Checked = c.MandatoryOrderNo.ToBool();




                    ddlSubCompany.SelectedValue = c.SubCompanyId;
                    ddlCompanyGroup.SelectedValue = c.GroupId;
                    ddlAccountType.SelectedValue = c.AccountTypeId;
                    ddlAdminFeeType.SelectedValue = c.AdminFeeType.ToStr();
                    ddlDrvFareReductionType.SelectedValue = c.DriverFareReductionType.ToStr();


                    numAdminFees.Value = c.AdminFees.ToInt();
                    numDiscount.Value = c.DiscountPercentage.ToDecimal();
                    numFareDeductPer.Value = c.FareDeductionPercent.ToDecimal();
                    numCompanyPricePercent.Value = c.CompanyPricePercent.ToDecimal();
                    numDrvFareReductionValue.Value = c.DriverFareReductionValue.ToDecimal();

                    if (chkIsAmountWiseComm.Checked)
                    {
                        numAgentCommission.Minimum = 0;
                        numAgentCommission.Maximum = 1000;
                        numAgentCommission.Value = c.CommissionPerBooking.ToDecimal();
                    }
                    else
                    {
                        numAgentCommission.Minimum = 0;
                        numAgentCommission.Maximum = 100;
                        numAgentCommission.Value = c.CommissionPerBooking.ToDecimal();
                    }

                    GridViewRowInfo row = null;


                    foreach (var item in c.Gen_Company_Departments)
                    {
                        row = grdDepartment.Rows.AddNew();

                        row.Cells[COLS.Id].Value = item.Id;
                        row.Cells[COLS.MasterId].Value = item.CompanyId;
                        row.Cells[COLS.DepartmentName].Value = item.DepartmentName;
                    }






                    foreach (var item in c.Gen_Company_OrderNumbers)
                    {
                        row = grdOrderNo.Rows.AddNew();

                        row.Cells[COLS.Id].Value = item.Id;
                        row.Cells[COLS.MasterId].Value = item.CompanyId;
                        row.Cells["OrderNo"].Value = item.OrderNo;
                    }

                    if (grdCompanyAddress != null)
                    {

                        foreach (var item in c.Gen_Company_Addresses)
                        {
                            row = grdCompanyAddress.Rows.AddNew();
                            row.Cells[COLS_Adress.Id].Value = item.Id;
                            row.Cells[COLS_Adress.MasterId].Value = item.CompanyId;
                            row.Cells[COLS_Adress.Address].Value = item.Address;

                        }

                    }

                    var ComapnyPaymetTypesList = (from a in c.Gen_Company_PaymentTypes
                                                  select new
                                                  {
                                                      Id = a.Id,
                                                      PaymentTypeId = a.PaymentTypeId
                                                  }).ToList();

                    for (int i = 0; i < grdPaymentTypes.RowCount; i++)
                    {
                        bool IsChecked = false;
                        foreach (var item in ComapnyPaymetTypesList)
                        {
                            if (item.PaymentTypeId == grdPaymentTypes.Rows[i].Cells[COLS_PaymentTypes.PaymentTypeId].Value.ToInt())
                            {
                                IsChecked = true;
                                break;
                            }
                            else
                            {
                                IsChecked = false;
                            }
                        }
                        grdPaymentTypes.Rows[i].Cells[COLS_PaymentTypes.Check].Value = IsChecked;
                    }

                    if (HasAdditonalCharges)
                    {

                        var ComapnyChargesTypesList = (from a in c.Gen_Company_ExtraCharges
                                                       select new
                                                       {
                                                           Id = a.Id,
                                                           PaymentTypeId = a.Charges
                                                       }).ToList();

                        for (int i = 0; i < grdAdditionalCharges.RowCount; i++)
                        {
                            bool IsChecked = false;
                            foreach (var item in ComapnyChargesTypesList)
                            {
                                if (item.PaymentTypeId == grdAdditionalCharges.Rows[i].Cells[COLS_PaymentTypes.PaymentTypeId].Value.ToInt())
                                {
                                    IsChecked = true;
                                    break;
                                }
                                else
                                {
                                    IsChecked = false;
                                }
                            }

                            grdAdditionalCharges.Rows[i].Cells[COLS_PaymentTypes.Check].Value = IsChecked;
                        }
                    }





                    if (AppVars.listUserRights.Count(a => a.formName == "frmCompany" && a.functionId == "SHOW BOOKEDBY TAB") > 0 && chkHasBookedBy.Checked)
                    {
                        if (grdBookedBy != null)
                        {
                            ClearBookedBy();
                            foreach (var item in c.Gen_Company_BookedBies)
                            {
                                row = grdBookedBy.Rows.AddNew();
                                row.Cells[COLS_BookedBy.Id].Value = item.Id;
                                row.Cells[COLS_BookedBy.MasterId].Value = item.CompanyId;
                                row.Cells[COLS_BookedBy.BookedBy].Value = item.BookedBy;
                                row.Cells[COLS_BookedBy.Email].Value = item.EmailAddress.ToStr();

                            }
                        }
                    }


                }

            }
            catch
            {

            }

        }




        //private void btnFixFareDetails_Click(object sender, EventArgs e)
        //{
        //    if (objMaster.Current != null)
        //    {
        //        int CompanyId = objMaster.Current.Id;
        //        frmFares frm = new frmFares(CompanyId, 1);
        //        if (grdFixedFare.RowCount > 0)
        //        {
        //            long FareId = grdFixedFare.Rows.FirstOrDefault().Cells[COLS_FixedFare.FareId].Value.ToLong();
        //            frm.OnDisplayRecord(FareId);
        //        }

        //        frm.ControlBox = true;
        //        frm.FormBorderStyle = FormBorderStyle.Fixed3D;
        //        frm.MaximizeBox = false;
        //        frm.ShowDialog();
        //        Save();
        //        DisplayRecord();
        //    }
        //    else
        //    {

        //        ENUtils.ShowMessage("Please save a Company First");
        //    }
        //}


    }
}
