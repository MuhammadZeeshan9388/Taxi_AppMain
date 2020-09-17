namespace Taxi_AppMain
{
    partial class frmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.radToolStripSeparatorItem1 = new Telerik.WinControls.UI.RadToolStripSeparatorItem();
            this.mnu_Booking = new Telerik.WinControls.UI.RadMenuItem();
            this.mnu_Report = new Telerik.WinControls.UI.RadMenuItem();
            this.mnu_UserManagement = new Telerik.WinControls.UI.RadMenuItem();
            this.mnu_Management = new Telerik.WinControls.UI.RadMenuItem();
            this.mnu_Help = new Telerik.WinControls.UI.RadMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menu_DashBoard = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.btnAddBooking = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.menu_Booking = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.frmBooking = new Telerik.WinControls.UI.RadMenuItem();
            this.frmBookingsList = new Telerik.WinControls.UI.RadMenuItem();
            this.frmTrashBooking = new Telerik.WinControls.UI.RadMenuItem();
            this.menu_Fares = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.frmFares = new Telerik.WinControls.UI.RadMenuItem();
            this.frmFaresList = new Telerik.WinControls.UI.RadMenuItem();
            this.menu_Driver = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.frmShifts = new Telerik.WinControls.UI.RadMenuItem();
            this.menu_Locations = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.frmLocations = new Telerik.WinControls.UI.RadMenuItem();
            this.frmLocationList = new Telerik.WinControls.UI.RadMenuItem();
            this.frmAddNewAddress = new Telerik.WinControls.UI.RadMenuItem();
            this.frmAddressList = new Telerik.WinControls.UI.RadMenuItem();
            this.frmZones = new Telerik.WinControls.UI.RadMenuItem();
            this.frmZonesList = new Telerik.WinControls.UI.RadMenuItem();
            this.menu_Customer = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.frmCustomer = new Telerik.WinControls.UI.RadMenuItem();
            this.frmCustomersList = new Telerik.WinControls.UI.RadMenuItem();
            this.frmComplaints = new Telerik.WinControls.UI.RadMenuItem();
            this.frmLostProperty = new Telerik.WinControls.UI.RadMenuItem();
            this.frmLostPropertyList = new Telerik.WinControls.UI.RadMenuItem();
            this.menu_Vehicle = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.frmVehicleType = new Telerik.WinControls.UI.RadMenuItem();
            this.frmVehicleTypeList = new Telerik.WinControls.UI.RadMenuItem();
            this.frmCompanyVehcile = new Telerik.WinControls.UI.RadMenuItem();
            this.frmCompanyVehcileList = new Telerik.WinControls.UI.RadMenuItem();
            this.btnCallerId = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.btnCallHistory = new Telerik.WinControls.UI.RadMenuItem();
            this.frmSysPolicy = new Telerik.WinControls.UI.RadButtonElement();
            this.btn_RefreshForm = new Telerik.WinControls.UI.RadButtonElement();
            this.btnLogOut = new Telerik.WinControls.UI.RadButtonElement();
            this.txtCurrentTimer = new Telerik.WinControls.UI.RadToolStripLabelElement();
            this.chkEnableAutoDespatch = new Telerik.WinControls.UI.RadCheckBoxElement();
            this.chkEnableBidding = new Telerik.WinControls.UI.RadCheckBoxElement();
            this.chkEnableOnBreak = new Telerik.WinControls.UI.RadCheckBoxElement();
            this.btn_JobPool = new Telerik.WinControls.UI.RadButtonElement();
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportMenuBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_StatusBar)).BeginInit();
            this.SuspendLayout();
            // 
            // radDock1
            // 
            this.radDock1.DocumentManager.DocumentInsertOrder = Telerik.WinControls.UI.Docking.DockWindowInsertOrder.InFront;
            this.radDock1.Location = new System.Drawing.Point(0, 92);
            // 
            // 
            // 
            this.radDock1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radDock1.RootElement.Padding = new System.Windows.Forms.Padding(5);
            this.radDock1.Size = new System.Drawing.Size(1376, 475);
            this.radDock1.Click += new System.EventHandler(this.radDock1_Click);
            // 
            // radPanelBar1
            // 
            this.radPanelBar1.Location = new System.Drawing.Point(0, 22);
            this.radPanelBar1.Size = new System.Drawing.Size(203, 393);
            // 
            // toolWindowLeft
            // 
            this.toolWindowLeft.Size = new System.Drawing.Size(205, 439);
            // 
            // toolbar_Custom
            // 
            this.toolbar_Custom.AutoSize = false;
            this.toolbar_Custom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.toolbar_Custom.Bounds = new System.Drawing.Rectangle(0, 0, 1500, 70);
            this.toolbar_Custom.Class = "ToolStripItem";
            this.toolbar_Custom.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolbar_Custom.ForeColor = System.Drawing.Color.Black;
            this.toolbar_Custom.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.menu_DashBoard,
            this.btnAddBooking,
            this.menu_Booking,
            this.menu_Fares,
            this.menu_Driver,
            this.menu_Locations,
            this.menu_Customer,
            this.menu_Vehicle,
            this.btnCallerId,
            this.radToolStripSeparatorItem1,
            this.frmSysPolicy,
            this.btn_RefreshForm,
            this.btnLogOut,
            this.txtCurrentTimer,
            this.chkEnableAutoDespatch,
            this.chkEnableBidding,
            this.btn_JobPool,
            this.chkEnableOnBreak});
            this.toolbar_Custom.MinSize = new System.Drawing.Size(25, 21);
            this.toolbar_Custom.Text = "toolbar_Custom";
            this.toolbar_Custom.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // ReportMenuBar
            // 
            this.ReportMenuBar.Size = new System.Drawing.Size(203, 414);
            // 
            // radPanel1
            // 
            this.radPanel1.Size = new System.Drawing.Size(1376, 72);
            // 
            // pnl_StatusBar
            // 
            this.pnl_StatusBar.Location = new System.Drawing.Point(0, 567);
            this.pnl_StatusBar.Size = new System.Drawing.Size(1376, 25);
            // 
            // radToolStripSeparatorItem1
            // 
            this.radToolStripSeparatorItem1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radToolStripSeparatorItem1.AutoSize = false;
            this.radToolStripSeparatorItem1.Bounds = new System.Drawing.Rectangle(151, 0, 2, 30);
            this.radToolStripSeparatorItem1.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.radToolStripSeparatorItem1.MaxSize = new System.Drawing.Size(0, 30);
            this.radToolStripSeparatorItem1.MinSize = new System.Drawing.Size(2, 0);
            this.radToolStripSeparatorItem1.Name = "radToolStripSeparatorItem1";
            this.radToolStripSeparatorItem1.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.radToolStripSeparatorItem1.Text = "radToolStripSeparatorItem1";
            // 
            // mnu_Booking
            // 
            this.mnu_Booking.Name = "mnu_Booking";
            this.mnu_Booking.Text = "Booking";
            this.mnu_Booking.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // mnu_Report
            // 
            this.mnu_Report.Name = "mnu_Report";
            this.mnu_Report.Text = "Report";
            // 
            // mnu_UserManagement
            // 
            this.mnu_UserManagement.Name = "mnu_UserManagement";
            this.mnu_UserManagement.Text = "User Management";
            // 
            // mnu_Management
            // 
            this.mnu_Management.Name = "mnu_Management";
            this.mnu_Management.Text = "Management";
            // 
            // mnu_Help
            // 
            this.mnu_Help.Name = "mnu_Help";
            this.mnu_Help.Text = "Help";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menu_DashBoard
            // 
            this.menu_DashBoard.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_DashBoard.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.menu_DashBoard.BackColor = System.Drawing.Color.Transparent;
            this.menu_DashBoard.DefaultItem = null;
            this.menu_DashBoard.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.menu_DashBoard.ExpandArrowButton = false;
            this.menu_DashBoard.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_DashBoard.Image = global::Taxi_AppMain.Properties.Resources.home;
            this.menu_DashBoard.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.menu_DashBoard.Name = "menu_DashBoard";
            this.menu_DashBoard.ShowArrow = false;
            this.menu_DashBoard.Tag = "frmBookingDashBoard";
            this.menu_DashBoard.Text = "DashBoard";
            this.menu_DashBoard.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_DashBoard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menu_DashBoard.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            this.menu_DashBoard.Click += new System.EventHandler(this.menu_Item_Click);
            // 
            // btnAddBooking
            // 
            this.btnAddBooking.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddBooking.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.btnAddBooking.AutoSize = true;
            this.btnAddBooking.DefaultItem = null;
            this.btnAddBooking.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.btnAddBooking.ExpandArrowButton = false;
            this.btnAddBooking.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddBooking.Image = global::Taxi_AppMain.Properties.Resources.plus_icon;
            this.btnAddBooking.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddBooking.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnAddBooking.Name = "btnAddBooking";
            this.btnAddBooking.ShowArrow = false;
            this.btnAddBooking.Text = "New Booking";
            this.btnAddBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddBooking.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.btnAddBooking.Click += new System.EventHandler(this.btnAddBooking_Click);
            // 
            // menu_Booking
            // 
            this.menu_Booking.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_Booking.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.menu_Booking.DefaultItem = null;
            this.menu_Booking.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.menu_Booking.ExpandArrowButton = false;
            this.menu_Booking.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_Booking.Image = global::Taxi_AppMain.Properties.Resources.Book;
            this.menu_Booking.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.menu_Booking.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.frmBooking,
            this.frmBookingsList,
            this.frmTrashBooking});
            this.menu_Booking.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.menu_Booking.Name = "menu_Booking";
            this.menu_Booking.Padding = new System.Windows.Forms.Padding(0);
            this.menu_Booking.ShowArrow = false;
            this.menu_Booking.Tag = "frmBookingsList";
            this.menu_Booking.Text = "Booking";
            this.menu_Booking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menu_Booking.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmBooking
            // 
            this.frmBooking.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmBooking.Name = "frmBooking";
            this.frmBooking.Tag = "false";
            this.frmBooking.Text = "Add New Booking";
            this.frmBooking.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmBookingsList
            // 
            this.frmBookingsList.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmBookingsList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmBookingsList.Name = "frmBookingsList";
            this.frmBookingsList.Text = "Bookings List";
            this.frmBookingsList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmTrashBooking
            // 
            this.frmTrashBooking.DescriptionFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmTrashBooking.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmTrashBooking.Name = "frmTrashBooking";
            this.frmTrashBooking.Tag = "true";
            this.frmTrashBooking.Text = "Trash Bookings";
            this.frmTrashBooking.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // menu_Fares
            // 
            this.menu_Fares.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_Fares.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.menu_Fares.DefaultItem = null;
            this.menu_Fares.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.menu_Fares.ExpandArrowButton = false;
            this.menu_Fares.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_Fares.Image = global::Taxi_AppMain.Properties.Resources.fares;
            this.menu_Fares.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.menu_Fares.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.frmFares,
            this.frmFaresList});
            this.menu_Fares.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.menu_Fares.Name = "menu_Fares";
            this.menu_Fares.Padding = new System.Windows.Forms.Padding(0);
            this.menu_Fares.ShowArrow = false;
            this.menu_Fares.Tag = "frmFaresList";
            this.menu_Fares.Text = "Fares";
            this.menu_Fares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menu_Fares.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmFares
            // 
            this.frmFares.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmFares.Name = "frmFares";
            this.frmFares.Tag = "true";
            this.frmFares.Text = "Add New Fare";
            this.frmFares.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmFaresList
            // 
            this.frmFaresList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmFaresList.Name = "frmFaresList";
            this.frmFaresList.Text = "Fare List";
            this.frmFaresList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // menu_Driver
            // 
            this.menu_Driver.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_Driver.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.menu_Driver.DefaultItem = null;
            this.menu_Driver.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.menu_Driver.ExpandArrowButton = false;
            this.menu_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_Driver.Image = global::Taxi_AppMain.Properties.Resources.driver;
            this.menu_Driver.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.menu_Driver.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.frmShifts});
            this.menu_Driver.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.menu_Driver.Name = "menu_Driver";
            this.menu_Driver.Padding = new System.Windows.Forms.Padding(0);
            this.menu_Driver.ShowArrow = false;
            this.menu_Driver.Tag = "frmDriversList";
            this.menu_Driver.Text = "Driver";
            this.menu_Driver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menu_Driver.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmShifts
            // 
            this.frmShifts.DescriptionFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmShifts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmShifts.Name = "frmShifts";
            this.frmShifts.Tag = "true";
            this.frmShifts.Text = "Driver Shifts";
            this.frmShifts.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // menu_Locations
            // 
            this.menu_Locations.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_Locations.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.menu_Locations.DefaultItem = null;
            this.menu_Locations.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.menu_Locations.ExpandArrowButton = false;
            this.menu_Locations.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_Locations.Image = global::Taxi_AppMain.Properties.Resources.Home_icon;
            this.menu_Locations.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.frmLocations,
            this.frmLocationList,
            this.frmAddNewAddress,
            this.frmAddressList,
            this.frmZones,
            this.frmZonesList});
            this.menu_Locations.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.menu_Locations.Name = "menu_Locations";
            this.menu_Locations.ShowArrow = false;
            this.menu_Locations.Text = "Location";
            this.menu_Locations.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menu_Locations.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmLocations
            // 
            this.frmLocations.ClickMode = Telerik.WinControls.ClickMode.Release;
            this.frmLocations.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmLocations.Name = "frmLocations";
            this.frmLocations.Tag = "true";
            this.frmLocations.Text = "Add New Location";
            this.frmLocations.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmLocationList
            // 
            this.frmLocationList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmLocationList.HintText = "";
            this.frmLocationList.Name = "frmLocationList";
            this.frmLocationList.Tag = "";
            this.frmLocationList.Text = "Locations List";
            this.frmLocationList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmAddNewAddress
            // 
            this.frmAddNewAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmAddNewAddress.HintText = "";
            this.frmAddNewAddress.Name = "frmAddNewAddress";
            this.frmAddNewAddress.Tag = "true";
            this.frmAddNewAddress.Text = "Add New Address";
            this.frmAddNewAddress.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmAddressList
            // 
            this.frmAddressList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmAddressList.HintText = "";
            this.frmAddressList.Name = "frmAddressList";
            this.frmAddressList.Tag = "";
            this.frmAddressList.Text = "Address List";
            this.frmAddressList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmZones
            // 
            this.frmZones.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmZones.Name = "frmZones";
            this.frmZones.Tag = "true";
            this.frmZones.Text = "Add New Zone";
            this.frmZones.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmZonesList
            // 
            this.frmZonesList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmZonesList.Name = "frmZonesList";
            this.frmZonesList.Text = "Zones List";
            this.frmZonesList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // menu_Customer
            // 
            this.menu_Customer.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_Customer.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.menu_Customer.DefaultItem = null;
            this.menu_Customer.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.menu_Customer.ExpandArrowButton = false;
            this.menu_Customer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_Customer.Image = global::Taxi_AppMain.Properties.Resources.customer;
            this.menu_Customer.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.menu_Customer.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.frmCustomer,
            this.frmCustomersList,
            this.frmComplaints,
            this.frmLostProperty,
            this.frmLostPropertyList});
            this.menu_Customer.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.menu_Customer.Name = "menu_Customer";
            this.menu_Customer.Padding = new System.Windows.Forms.Padding(0);
            this.menu_Customer.ShowArrow = false;
            this.menu_Customer.Tag = "frmCustomersList";
            this.menu_Customer.Text = "Customer";
            this.menu_Customer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menu_Customer.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmCustomer
            // 
            this.frmCustomer.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCustomer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCustomer.Name = "frmCustomer";
            this.frmCustomer.Tag = "true";
            this.frmCustomer.Text = "Add New Customer";
            this.frmCustomer.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmCustomersList
            // 
            this.frmCustomersList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCustomersList.Name = "frmCustomersList";
            this.frmCustomersList.Text = "Customers List";
            this.frmCustomersList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmComplaints
            // 
            this.frmComplaints.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmComplaints.Name = "frmComplaints";
            this.frmComplaints.Text = "Complaints";
            this.frmComplaints.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmLostProperty
            // 
            this.frmLostProperty.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmLostProperty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmLostProperty.Name = "frmLostProperty";
            this.frmLostProperty.Tag = "true";
            this.frmLostProperty.Text = "Add Lost Property";
            // 
            // frmLostPropertyList
            // 
            this.frmLostPropertyList.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmLostPropertyList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmLostPropertyList.Name = "frmLostPropertyList";
            this.frmLostPropertyList.Text = "Lost Property List";
            // 
            // menu_Vehicle
            // 
            this.menu_Vehicle.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu_Vehicle.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.menu_Vehicle.DefaultItem = null;
            this.menu_Vehicle.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.menu_Vehicle.ExpandArrowButton = false;
            this.menu_Vehicle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_Vehicle.Image = global::Taxi_AppMain.Properties.Resources.com_worldstreet_lab_calltaxi____82988;
            this.menu_Vehicle.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.menu_Vehicle.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.frmVehicleType,
            this.frmVehicleTypeList,
            this.frmCompanyVehcile,
            this.frmCompanyVehcileList});
            this.menu_Vehicle.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.menu_Vehicle.Name = "menu_Vehicle";
            this.menu_Vehicle.Padding = new System.Windows.Forms.Padding(0);
            this.menu_Vehicle.ShowArrow = false;
            this.menu_Vehicle.Tag = "frmVehicleTypeList";
            this.menu_Vehicle.Text = "Vehicle";
            this.menu_Vehicle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menu_Vehicle.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmVehicleType
            // 
            this.frmVehicleType.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmVehicleType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmVehicleType.Name = "frmVehicleType";
            this.frmVehicleType.Tag = "true";
            this.frmVehicleType.Text = "Add New Vehicle";
            this.frmVehicleType.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmVehicleTypeList
            // 
            this.frmVehicleTypeList.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmVehicleTypeList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmVehicleTypeList.Name = "frmVehicleTypeList";
            this.frmVehicleTypeList.Text = "Vehicles List";
            this.frmVehicleTypeList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmCompanyVehcile
            // 
            this.frmCompanyVehcile.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCompanyVehcile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCompanyVehcile.Name = "frmCompanyVehcile";
            this.frmCompanyVehcile.Tag = "true";
            this.frmCompanyVehcile.Text = "Add Company Vehicle";
            this.frmCompanyVehcile.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmCompanyVehcileList
            // 
            this.frmCompanyVehcileList.DescriptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCompanyVehcileList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCompanyVehcileList.Name = "frmCompanyVehcileList";
            this.frmCompanyVehcileList.Text = "Company Vehicle List";
            this.frmCompanyVehcileList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnCallerId
            // 
            this.btnCallerId.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallerId.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.btnCallerId.DefaultItem = null;
            this.btnCallerId.DropDownDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.btnCallerId.ExpandArrowButton = false;
            this.btnCallerId.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCallerId.Image = global::Taxi_AppMain.Properties.Resources.callerid1;
            this.btnCallerId.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCallerId.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnCallHistory});
            this.btnCallerId.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnCallerId.Name = "btnCallerId";
            this.btnCallerId.Padding = new System.Windows.Forms.Padding(0);
            this.btnCallerId.ShowArrow = false;
            this.btnCallerId.Text = "Call History";
            this.btnCallerId.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCallerId.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            this.btnCallerId.ChildrenChanged += new Telerik.WinControls.ChildrenChangedEventHandler(this.btnCallerId_ChildrenChanged);
            // 
            // btnCallHistory
            // 
            this.btnCallHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCallHistory.Name = "btnCallHistory";
            this.btnCallHistory.Text = "View Call History";
            this.btnCallHistory.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // frmSysPolicy
            // 
            this.frmSysPolicy.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.frmSysPolicy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmSysPolicy.Image = global::Taxi_AppMain.Properties.Resources.Settings;
            this.frmSysPolicy.Margin = new System.Windows.Forms.Padding(10, 1, 1, 1);
            this.frmSysPolicy.Name = "frmSysPolicy";
            this.frmSysPolicy.ShowBorder = false;
            this.frmSysPolicy.Text = "Settings";
            this.frmSysPolicy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.frmSysPolicy.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btn_RefreshForm
            // 
            this.btn_RefreshForm.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RefreshForm.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RefreshForm.Image = global::Taxi_AppMain.Properties.Resources.refresh;
            this.btn_RefreshForm.Margin = new System.Windows.Forms.Padding(20, 1, 1, 1);
            this.btn_RefreshForm.Name = "btn_RefreshForm";
            this.btn_RefreshForm.ShowBorder = false;
            this.btn_RefreshForm.Text = "Refresh";
            this.btn_RefreshForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_RefreshForm.Click += new System.EventHandler(this.btn_RefreshForm_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogOut.AutoSize = false;
            this.btnLogOut.Bounds = new System.Drawing.Rectangle(255, 0, 121, 38);
            this.btnLogOut.Image = global::Taxi_AppMain.Properties.Resources.logout_120x37;
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(25, 1, 1, 1);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.ShowBorder = false;
            this.btnLogOut.Text = "";
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // txtCurrentTimer
            // 
            this.txtCurrentTimer.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtCurrentTimer.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.txtCurrentTimer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(130)))), ((int)(((byte)(243)))));
            this.txtCurrentTimer.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtCurrentTimer.Name = "txtCurrentTimer";
            this.txtCurrentTimer.Text = "";
            // 
            // chkEnableAutoDespatch
            // 
            this.chkEnableAutoDespatch.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkEnableAutoDespatch.Checked = false;
            this.chkEnableAutoDespatch.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableAutoDespatch.Name = "chkEnableAutoDespatch";
            this.chkEnableAutoDespatch.Text = "AutoDespatch Mode";
            this.chkEnableAutoDespatch.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // chkEnableBidding
            // 
            this.chkEnableBidding.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkEnableBidding.Checked = false;
            this.chkEnableBidding.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableBidding.ForeColor = System.Drawing.Color.Black;
            this.chkEnableBidding.Margin = new System.Windows.Forms.Padding(-120, 45, 0, 0);
            this.chkEnableBidding.Name = "chkEnableBidding";
            this.chkEnableBidding.Text = "Bidding";
            this.chkEnableBidding.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // chkEnableOnBreak
            // 
            this.chkEnableOnBreak.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkEnableOnBreak.Checked = false;
            this.chkEnableOnBreak.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableOnBreak.Margin = new System.Windows.Forms.Padding(10, 45, 0, 0);
            this.chkEnableOnBreak.Name = "chkEnableOnBreak";
            this.chkEnableOnBreak.Text = "OnBreak Mode";
            this.chkEnableOnBreak.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btn_JobPool
            // 
            this.btn_JobPool.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_JobPool.Name = "btn_JobPool";
            this.btn_JobPool.ShowBorder = false;
            this.btn_JobPool.Text = "JOb Pool";
            this.btn_JobPool.Click += new System.EventHandler(this.btn_JobPool_Click);
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 592);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmMainMenu";
            this.ShowCustomToolbar = true;
            this.ShowDefaultToolbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmMainMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainMenu_FormClosing);
            this.Load += new System.EventHandler(this.frmMainMenu_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportMenuBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_StatusBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitButtonElement menu_DashBoard;
        private Telerik.WinControls.UI.RadSplitButtonElement btnCallerId;
        private Telerik.WinControls.UI.RadButtonElement btn_RefreshForm;
        private Telerik.WinControls.UI.RadToolStripSeparatorItem radToolStripSeparatorItem1;
        private Telerik.WinControls.UI.RadButtonElement btnLogOut;
        private Telerik.WinControls.UI.RadMenuItem btnCallHistory;
        private Telerik.WinControls.UI.RadSplitButtonElement btnAddBooking;
        private Telerik.WinControls.UI.RadMenuItem mnu_Booking;
        private Telerik.WinControls.UI.RadMenuItem mnu_Report;
        private Telerik.WinControls.UI.RadMenuItem mnu_UserManagement;
        private Telerik.WinControls.UI.RadMenuItem mnu_Management;
        private Telerik.WinControls.UI.RadMenuItem mnu_Help;
        private Telerik.WinControls.UI.RadSplitButtonElement menu_Fares;
        private Telerik.WinControls.UI.RadSplitButtonElement menu_Driver;
        private Telerik.WinControls.UI.RadSplitButtonElement menu_Customer;
        private Telerik.WinControls.UI.RadSplitButtonElement menu_Vehicle;
        private Telerik.WinControls.UI.RadSplitButtonElement menu_Booking;
        private Telerik.WinControls.UI.RadMenuItem frmFares;
        private Telerik.WinControls.UI.RadMenuItem frmFaresList;
        private Telerik.WinControls.UI.RadMenuItem frmCustomer;
        private Telerik.WinControls.UI.RadMenuItem frmCustomersList;
        private Telerik.WinControls.UI.RadMenuItem frmVehicleType;
        private Telerik.WinControls.UI.RadMenuItem frmVehicleTypeList;
        private Telerik.WinControls.UI.RadMenuItem frmBooking;
        private Telerik.WinControls.UI.RadMenuItem frmBookingsList;
        private Telerik.WinControls.UI.RadButtonElement frmSysPolicy;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadSplitButtonElement menu_Locations;
        private Telerik.WinControls.UI.RadMenuItem frmLocations;
        private Telerik.WinControls.UI.RadMenuItem frmLocationList;
        private Telerik.WinControls.UI.RadMenuItem frmAddNewAddress;
        private Telerik.WinControls.UI.RadMenuItem frmAddressList;


        //Telerik.WinControls.Keyboard.InputBinding inputBinding13 = new Telerik.WinControls.Keyboard.InputBinding();
        //Telerik.WinControls.Keyboard.Chord chord13 = new Telerik.WinControls.Keyboard.Chord();
        //Telerik.WinControls.Keyboard.ChordModifier chordModifier13 = new Telerik.WinControls.Keyboard.ChordModifier(); 
        //Telerik.WinControls.Elements.ClickCommand clickCommand2 = new Telerik.WinControls.Elements.ClickCommand();
        private Telerik.WinControls.UI.RadMenuItem frmZones;
        private Telerik.WinControls.UI.RadMenuItem frmZonesList;
        private Telerik.WinControls.UI.RadMenuItem frmComplaints;
        private Telerik.WinControls.UI.RadMenuItem frmTrashBooking;
        private Telerik.WinControls.UI.RadMenuItem frmShifts;
        private Telerik.WinControls.UI.RadMenuItem frmLostProperty;
        private Telerik.WinControls.UI.RadMenuItem frmLostPropertyList;
        private Telerik.WinControls.UI.RadMenuItem frmCompanyVehcile;
        private Telerik.WinControls.UI.RadMenuItem frmCompanyVehcileList;
        private Telerik.WinControls.UI.RadToolStripLabelElement txtCurrentTimer;
        private Telerik.WinControls.UI.RadCheckBoxElement chkEnableAutoDespatch;
        private Telerik.WinControls.UI.RadCheckBoxElement chkEnableBidding;

        private Telerik.WinControls.UI.RadCheckBoxElement chkEnableOnBreak;
        private Telerik.WinControls.UI.RadButtonElement btn_JobPool;
    }
}