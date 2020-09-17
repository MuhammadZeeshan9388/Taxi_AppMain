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
using Telerik.WinControls.UI;
using Taxi_Model;
using UI;
using Utils;
using Telerik.WinControls.Enumerations;
using CallerIdData;
using System.Data.Linq;
using System.IO;
using System.IO.Ports;
using Taxi_AppMain.Properties;
using System.Configuration;
using System.Xml;
using System.Reflection;
using Telerik.WinControls;
using System.Net.NetworkInformation;

namespace Taxi_AppMain
{
    public partial class frmSysPolicy : SetupBase
    {
        public struct COL_AIRPORTPICKUPS
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string AIRPORTID = "AIRPORTID";
            public static string AIRPORTNAME = "AIRPORTNAME";
            public static string CHARGES = "CHARGES";        
        }


        public struct COL_RANGEWISECOMMISSION
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string FROMPRICE = "FROMPRICE";

            public static string TOPRICE = "TOPRICE";

            public static string CHARGESPERCENT = "CHARGESPERCENT";


        }

        public struct COL_AIRPORTEXPIRY
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string LOCATIONID = "LOCATIONID";

            public static string LOCATIONNAME = "LOCATIONNAME";

            public static string EXPIRY = "EXPIRY";


        }

        public struct COL_SYSPOLICYDOCS
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string DOCUMENTID = "DOCUMENTID";
            public static string SUBCOMPANYID = "SUBCOMPANYID";

            public static string PREFIX = "PREFIX";
            public static string STARTNUMBER = "STARTNUMBER";
            public static string LASTNUMBER = "LASTNUMBER";
            public static string AUTOINCREMENT = "AUTOINCREMENT";


        }


        public struct COL_SMSTEMPLET
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string Tempplet = "Tempplet";
        }



        public struct COL_SURCHARGERATES
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string POSTCODE = "Post Code";


            public static string PERCENTAGE = "PERCENTAGE";
            public static string AMOUNT = "AMOUNT";

            public static string AMOUNTWISE = "AMOUNTWISE";

        }


        public struct COL_FARES
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string VEHICLETYPEID = "VEHICLETYPEID";
            public static string VEHICLETYPE = "VEHICLETYPE";
            public static string OPERATOR = "OPERATOR";

            public static string PERCENTAGE = "PERCENTAGE";
            public static string AMOUNT = "AMOUNT";

            public static string AMOUNTWISE = "AMOUNTWISE";
        }

        public struct COL_GATEWAY
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string GATEWAYTYPEID = "GATEWAYTYPEID";
            public static string GATEWAYTYPENAME = "GATEWAYTYPENAME";
            public static string MERCHANTID = "MERCHANTID";
            public static string MERCHANTPASSWORD = "MERCHANTPASSWORD";

            public static string APIUSERNAME = "APIUSERNAME";
            public static string APIPASSWORD = "APIPASSWORD";
            public static string APISIGNATURE = "APISIGNATURE";
            public static string APPLICATIONID = "APPLICATIONID";
            public static string ACCOUNTYPEID = "ACCOUNTYPEID";

            public static string PAYPALID = "PAYPALID";
            public static string EnableMobileIntegration = "EnableMobileIntegration";

        }
        public struct COL_MODEMSMS
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string UserName = "UserName";

            public static string Port = "Port";



        }



        public struct COL_VOIP
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string Account = "Account";
            public static string Password = "Password";
            public static string Host = "Host";
            public static string UserId = "UserId";
            public static string User = "User";
            public static string Port = "Port";
            public static string Proxy = "Proxy";
        

        }





        SysPolicyBO objMaster;
        public frmSysPolicy()
        {
            InitializeComponent();
            InitializeConstructor();

        }



        private void InitializeConstructor()
        {

            objMaster = new SysPolicyBO();
         
            this.SetProperties((INavigation)objMaster);

            ddlMapIcon.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlMapIcon_SelectedIndexChanged);
            this.Shown += new EventHandler(frmSysPolicy_Shown);
            
            string[] ports = SerialPort.GetPortNames();

            // Add all port names to the combo box:
            foreach (string port in ports)
            {
                this.ddlModemSMSPortName.Items.Add(port);
            }


            this.FormClosed += new FormClosedEventHandler(frmSysPolicy_FormClosed);

            chkEnableAutoBookingExpiry.ToggleStateChanged += new StateChangedEventHandler(chkEnableAutoBookingExpiry_ToggleStateChanged);


            txtArrivalAirportText.Enter += new EventHandler(txtArrivalAirportText_Enter);
            txtArrivalText.Enter += new EventHandler(txtArrivalText_Enter);

            chkEnablePOI.ToggleStateChanging += new StateChangingEventHandler(chkEnablePOI_ToggleStateChanging);
            this.grdPaymentGateway.CellEndEdit += new GridViewCellEventHandler(grdPaymentGateway_CellEndEdit);
            //this.grdPaymentGateway.ValueChanging += new ValueChangingEventHandler(grdPaymentGateway_ValueChanging);
        //    radPageView1.PageIndexChanged += new EventHandler<RadPageViewIndexChangedEventArgs>(radPageView1_PageIndexChanged);



            ddlAutoDespatchDrvPriorityCategory.Items.Add(new RadListDataItem { Text = "None", Value = 0 });
            ddlAutoDespatchDrvPriorityCategory.Items.Add(new RadListDataItem { Text = "Hackney", Value = 1 });
            ddlAutoDespatchDrvPriorityCategory.Items.Add(new RadListDataItem { Text = "Private Hire", Value = 2 });

            pg_localbookingexpiry.Item.Visibility = ElementVisibility.Collapsed;
        }

        void grdPaymentGateway_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdPaymentGateway.CurrentRow != null)
                {
                    if (grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.EnableMobileIntegration].Value.ToBool())
                    {
                        pnlPaymentPDASettings.Visible = true;
                    }
                    else
                    {
                        pnlPaymentPDASettings.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

       

        private bool IsDisplayingRecord = false;

        void chkEnablePOI_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {

            if (IsDisplayingRecord)
                return;

            if (args.NewValue == ToggleState.On)
            {

                if(CheckPAFDataAvailability()==false)
                     args.Canceled = true;
            }
           
        }

        void chkEnableOfflineDistance_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == ToggleState.On)
            //{
            //  //  lblOfflineDistanceNote.Visible = true;

            // //   System.Threading.Thread.Sleep(1000);
          //   General.RunWCFService();
            //}
            //else
            //{
            // //   lblOfflineDistanceNote.Visible = false;
            ////    General.StopServiceWCF();

            //}
       
        }


        private bool CheckPAFDataAvailability()
        {
            bool rtn = false;

            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    if (db.stp_GetByRoadLevelData("HA2 0DU", "", "", "").Count() > 0)
                    {

                        rtn = true;
                    }



                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
               

            }


            return rtn;
        }


        private bool ChangeEmailSettings = false;
        //void radPageView1_PageIndexChanged(object sender, RadPageViewIndexChangedEventArgs e)
        //{
        //    if (e.Page != null && e.Page.Name=="pg_email")
        //    {

        //        ChangeEmailSettings = true;
        //    }

        //}

        bool lastFocusedOn = false;

        void txtArrivalAirportText_Enter(object sender, EventArgs e)
        {
            lastFocusedOn = true;
        }

        void txtArrivalText_Enter(object sender, EventArgs e)
        {
            lastFocusedOn = false;
        }

        void chkEnableAutoBookingExpiry_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                grdBookingExpiry.Enabled = false;

            }
            else
            {

                grdBookingExpiry.Enabled = true;
            }
        }

        void frmSysPolicy_FormClosed(object sender, FormClosedEventArgs e)
        {


            GC.Collect();

        }

       

        private void InitializeFaresPanel()
        {
            this.pnlFares = new Telerik.WinControls.UI.RadGroupBox();
            this.grdFaresSettings = new UI.MyGridView();

            ((System.ComponentModel.ISupportInitialize)(this.pnlFares)).BeginInit();
            this.pnlFares.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFaresSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFaresSettings.MasterTemplate)).BeginInit();


            // 
            // pnlFares
            // 

            this.pnlFares.BackColor = System.Drawing.Color.Transparent;
            this.pnlFares.Controls.Add(this.grdFaresSettings);
            this.pnlFares.FooterImageIndex = -1;
            this.pnlFares.FooterImageKey = "";
            this.pnlFares.ForeColor = System.Drawing.Color.Black;
            this.pnlFares.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.pnlFares.HeaderImageIndex = -1;
            this.pnlFares.HeaderImageKey = "";
            this.pnlFares.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.pnlFares.HeaderText = "Fares Settings";
            this.pnlFares.Location = new System.Drawing.Point(15, 6);
            this.pnlFares.Name = "pnlFares";
            this.pnlFares.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.pnlFares.RootElement.ForeColor = System.Drawing.Color.Black;
            this.pnlFares.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.pnlFares.Size = new System.Drawing.Size(586, 360);
            this.pnlFares.TabIndex = 5;
            this.pnlFares.Text = "Fares Settings";
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlFares.GetChildAt(0).GetChildAt(1))).GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlFares.GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlFares.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.GreenYellow;
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlFares.GetChildAt(0).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor2 = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor3 = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor4 = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            // 
            // grdFaresSettings
            // 
          
            this.grdFaresSettings.AutoCellFormatting = false;
            this.grdFaresSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFaresSettings.EnableCheckInCheckOut = false;
            this.grdFaresSettings.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFaresSettings.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdFaresSettings.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdFaresSettings.Location = new System.Drawing.Point(10, 20);
            this.grdFaresSettings.Name = "grdFaresSettings";
            this.grdFaresSettings.PKFieldColumnName = "";
            this.grdFaresSettings.ShowImageOnActionButton = true;
            this.grdFaresSettings.Size = new System.Drawing.Size(566, 330);
            this.grdFaresSettings.TabIndex = 0;
            this.grdFaresSettings.Text = "myGridView1";
            this.grdFaresSettings.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.grdFaresSettings_CellBeginEdit);
            this.grdFaresSettings.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdFaresSettings_CellValueChanged);

            ((System.ComponentModel.ISupportInitialize)(this.pnlFares)).EndInit();
            this.pnlFares.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFaresSettings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFaresSettings)).EndInit();

            this.pg_fares.Controls.Add(this.pnlFares);

            FormatFaresGrid();
        }

        //CompanyFares
        private void InitializeCompanyFaresPanel()
        {
            this.pnlCompanyFares = new Telerik.WinControls.UI.RadGroupBox();
            this.grdCompanyFaresSettings = new UI.MyGridView();

            ((System.ComponentModel.ISupportInitialize)(this.pnlCompanyFares)).BeginInit();
            this.pnlCompanyFares.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyFaresSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyFaresSettings.MasterTemplate)).BeginInit();


            // 
            // pnlFares
            // 

            this.pnlCompanyFares.BackColor = System.Drawing.Color.Transparent;
            this.pnlCompanyFares.Controls.Add(this.grdCompanyFaresSettings);
            this.pnlCompanyFares.FooterImageIndex = -1;
            this.pnlCompanyFares.FooterImageKey = "";
            this.pnlCompanyFares.ForeColor = System.Drawing.Color.Black;
            this.pnlCompanyFares.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.pnlCompanyFares.HeaderImageIndex = -1;
            this.pnlCompanyFares.HeaderImageKey = "";
            this.pnlCompanyFares.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.pnlCompanyFares.HeaderText = "Company Fares Settings";
            this.pnlCompanyFares.Location = new System.Drawing.Point(610, 6);
            this.pnlCompanyFares.Name = "pnlCompanyFares";
            this.pnlCompanyFares.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.pnlCompanyFares.RootElement.ForeColor = System.Drawing.Color.Black;
            this.pnlCompanyFares.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.pnlCompanyFares.Size = new System.Drawing.Size(586, 360);
            this.pnlCompanyFares.TabIndex = 5;
            this.pnlCompanyFares.Text = "Company Fares Settings";
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1))).GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.GreenYellow;
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor2 = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor3 = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor4 = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor = System.Drawing.Color.Green;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.pnlCompanyFares.GetChildAt(0).GetChildAt(1).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            // 
            // grdFaresSettings
            // 

            this.grdCompanyFaresSettings.AutoCellFormatting = false;
            this.grdCompanyFaresSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCompanyFaresSettings.EnableCheckInCheckOut = false;
            this.grdCompanyFaresSettings.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdCompanyFaresSettings.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdCompanyFaresSettings.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdCompanyFaresSettings.Location = new System.Drawing.Point(610, 6);
            this.grdCompanyFaresSettings.Name = "grdCompanyFaresSettings";
            this.grdCompanyFaresSettings.PKFieldColumnName = "";
            this.grdCompanyFaresSettings.ShowImageOnActionButton = true;
            this.grdCompanyFaresSettings.Size = new System.Drawing.Size(566, 330);
            this.grdCompanyFaresSettings.TabIndex = 0;
            this.grdCompanyFaresSettings.Text = "myGridView1";
          //  this.grdCompanyFaresSettings.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.grdFaresSettings_CellBeginEdit);
          //  this.grdCompanyFaresSettings.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdFaresSettings_CellValueChanged);

            ((System.ComponentModel.ISupportInitialize)(this.pnlCompanyFares)).EndInit();
            this.pnlCompanyFares.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyFaresSettings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyFaresSettings)).EndInit();

            this.pg_fares.Controls.Add(this.pnlCompanyFares);
            //pnlCompanyFares
          //  FormatCompanyFaresGrid();
        }


        private void InitializeSurchargesRateGrid()
        {
            this.grdSurchargeRates = new UI.MyGridView();

            ((System.ComponentModel.ISupportInitialize)(this.grdSurchargeRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSurchargeRates.MasterTemplate)).BeginInit();

            this.pg_SurchargesRates.Controls.Add(this.grdSurchargeRates);

            // 
            // grdSurchargeRates
            // 
            this.grdSurchargeRates.AutoCellFormatting = false;
            this.grdSurchargeRates.EnableCheckInCheckOut = false;
            this.grdSurchargeRates.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdSurchargeRates.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdSurchargeRates.Location = new System.Drawing.Point(15, 27);
            this.grdSurchargeRates.Name = "grdSurchargeRates";
            this.grdSurchargeRates.PKFieldColumnName = "";
            this.grdSurchargeRates.ShowImageOnActionButton = true;
            this.grdSurchargeRates.Size = new System.Drawing.Size(500, 388);
            this.grdSurchargeRates.TabIndex = 10;
            this.grdSurchargeRates.Text = "myGridView1";

            ((System.ComponentModel.ISupportInitialize)(this.grdSurchargeRates.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSurchargeRates)).EndInit();

            FormatSurchargeRateGrid();
        }


     

      
         private void ddlMapIcon_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlMapIcon.SelectedItem != null)
            {
                pb_mapicon.Image = ddlMapIcon.SelectedItem.Image;
            }
            else
            {

                pb_mapicon.Image = null;
            }
        }

       

        void frmSysPolicy_Shown(object sender, EventArgs e)
        {

            InitializeControlValues();

            FillTapiDigitalLinesList();

            ComboFunctions.FillUsersCombo(SIP_ddlUsers);
            FormatVOIPGrid();
            if (OpenedFromCallerId == false)
            {
                FormatDocumentGrid();
                FormatSMSTempletGrid();
              //  FormatSurchargeRateGrid();
              //  FormatModemSMSGrid();
               
                FormatPaymentGatewayGrid();


                FormatAirportExpiryGrid();

                FormatLocalBookingExpiryGrid();

              
             //   ComboFunctions.FillSMSTagCombo(ddlTagEmail, null);                
             

                ddlTagEmail.Items.Add(new RadListDataItem { Text="Ref No" , Value="<BookingNo>"});
                ddlTagEmail.Items.Add(new RadListDataItem { Text="From DoorNo" , Value="<FromDoorNo>"});
                ddlTagEmail.Items.Add( new RadListDataItem { Text="Pickup Point" , Value="<PickupPoint>"});
                ddlTagEmail.Items.Add( new RadListDataItem { Text="Destination" , Value="<Destination>"});
                ddlTagEmail.Items.Add(  new RadListDataItem { Text="Pickup Date" , Value="<PickupDate>"});
                ddlTagEmail.Items.Add( new RadListDataItem { Text="Pickup Time" , Value="<PickupTime>"});
                ddlTagEmail.Items.Add(new RadListDataItem { Text = "Passenger", Value = "<Passenger>" });

                ddlTagEmail.Items.Add(new RadListDataItem { Text = "Fares", Value = "<Fares>" });

                ddlTagEmail.Items.Add(  new RadListDataItem { Text="Company TelNo" , Value="<CompanyTelNo>"});
                ddlTagEmail.Items.Add(new RadListDataItem { Text = "Company Name", Value = "<CompanyName>" });

                ddlTagEmail.Items.Add(new RadListDataItem { Text = "Via Point", Value = "<ViaPoint>" });



                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var smsTagList = db.SMSTags.ToList();

                    ComboFunctions.FillCombo<SMSTag>(smsTagList, ddlTagDriver, "TagDisplayValue", "TagMemberValue");
                    ComboFunctions.FillCombo<SMSTag>(smsTagList.ToList(), ddlTagCustomer, "TagDisplayValue", "TagMemberValue");
                    ComboFunctions.FillCombo<SMSTag>(smsTagList.ToList(), ddlTagAdvance, "TagDisplayValue", "TagMemberValue");
                    ComboFunctions.FillCombo<SMSTag>(smsTagList.ToList(), ddlTagArrive, "TagDisplayValue", "TagMemberValue");
                    ComboFunctions.FillCombo<SMSTag>(smsTagList.ToList(), ddlTagConfirmationCustomer, "TagDisplayValue", "TagMemberValue");
                    ComboFunctions.FillCombo<SMSTag>(smsTagList.ToList(), ddlTagPDA, "TagDisplayValue", "TagMemberValue");

                    ComboFunctions.FillCombo<SMSTag>(smsTagList.ToList(), ddlTagNoPickup, "TagDisplayValue", "TagMemberValue");
                    ComboFunctions.FillCombo<SMSTag>(smsTagList.ToList(), ddlTagCancelText, "TagDisplayValue", "TagMemberValue");
       
                    

                    ComboFunctions.FillCombo<SMSTag>(smsTagList.Where(c => c.TagObjectName != "driver").ToList(), ddlTagWebBooking, "TagDisplayValue", "TagMemberValue");
                 
                }

                //ComboFunctions.FillSMSTagCombo(ddlTagDriver, null);
                //ComboFunctions.FillSMSTagCombo(ddlTagCustomer, null);
                //ComboFunctions.FillSMSTagCombo(ddlTagAdvance, null);
                //ComboFunctions.FillSMSTagCombo(ddlTagArrive, null);


            //    ComboFunctions.FillSMSTagCombo(ddlTagWebBooking,c=>c.TagObjectName!="driver");


                ddlGateway.Enter += new EventHandler(ddlGateway_Enter);
            //     ComboFunctions.FillPaymentGatewayCombo(ddlGateway);
               

            }

            FillMapIconCombo();



            RadListDataItem item = new RadListDataItem();
            item.Text = "Plot Wise";
            item.Value = 0;
            item.Selected = true;
            ddlPDARankType.Items.Add(item);

            item = new RadListDataItem();
            item.Text = "Overall Waiting Queue";
            item.Value = 1;
            ddlPDARankType.Items.Add(item);


            item = new RadListDataItem();
            item.Text = "Plot/Overall Waiting Queue";
            item.Value = 2;
            ddlPDARankType.Items.Add(item);


            item = new RadListDataItem();
            item.Text = "Plot/Total Available in Plot";
            item.Value = 3;
            ddlPDARankType.Items.Add(item);

           



            Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
            if (obj != null)
            {
                objMaster.GetByPrimaryKey(obj.Id);
                DisplayRecord();
               
            }




            FormatExtraChargesGrid();
            LoadAdditionalCharges();

         //   chkEnableOfflineDistance.Visible = false;

           // chkEnableOfflineDistance.ToggleStateChanged += new StateChangedEventHandler(chkEnableOfflineDistance_ToggleStateChanged);

        }

        void ddlGateway_Enter(object sender, EventArgs e)
        {
            FillPaymentGatewayDropDown();
        }

        private void FillPaymentGatewayDropDown()
        {

            if (ddlGateway.DataSource == null)
            {
                ComboFunctions.FillPaymentGatewayCombo(ddlGateway);

            }
        }


        private bool _OpenedFromCallerId;

        public bool OpenedFromCallerId
        {
            get { return _OpenedFromCallerId; }
            set { _OpenedFromCallerId = value; }
        }
     

        public frmSysPolicy(bool openedAsCallerId)
        {
            InitializeComponent();
            this.OpenedFromCallerId = openedAsCallerId;
        
            InitializeConstructor();
            if (OpenedFromCallerId)
            {
                this.FormTitle = "CallerId Configurations";
                radPageView1.Pages[0].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[1].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[3].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[4].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[5].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[6].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[7].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[8].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[9].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

                radPageView1.SelectedPage = radPageView1.Pages[2];

            }


        }


        public frmSysPolicy(int openedAsCompany)
        {
            InitializeComponent();


            InitializeConstructor();
            if (openedAsCompany==1)
            {
                this.FormTitle = "Company Information";
                radPageView1.Pages[0].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[2].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[3].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[4].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[5].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[6].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[7].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[8].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                radPageView1.Pages[9].Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

                radPageView1.SelectedPage = radPageView1.Pages[1];

                pnlMap.Visible = false;
                pnlNetworkConfig.Visible = false;

            }


        }


        private void FillMapIconCombo()
        {
            try
            {


                string path = System.Windows.Forms.Application.StartupPath + "\\VehicleImages\\";
                RadListDataItem radItem = null;
                foreach (var item in General.GetQueryable<Gen_MapIcon>(null).ToList())
                {
                    radItem = new RadListDataItem();
                    radItem.Font = new Font("Tahoma", 12, FontStyle.Bold);
                    radItem.Text = item.MapIconName;
                    radItem.Value = item.MapIconName + "_";

                    if (System.IO.File.Exists(path + item.MapIconName + ".png"))
                    {
                        radItem.Image = Image.FromFile(path + item.MapIconName + ".png");
                    }
                    //    radItem.Height = 40;
                    ddlMapIcon.Items.Add(radItem);
                }

                ddlMapIcon.DropDownListElement.ItemHeight = 30;
                ddlMapIcon.Items[0].Height = 30;
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }

       

      

        private void FormatDocumentGrid()
        {

            grdPolicyDocuments.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SYSPOLICYDOCS.ID;
            grdPolicyDocuments.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SYSPOLICYDOCS.POLICYID;
            grdPolicyDocuments.Columns.Add(col);


            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();

            colCombo.Name = COL_SYSPOLICYDOCS.DOCUMENTID;
            //colCombo.DataSource = General.GetGeneralList<Gen_SysPolicyDocumentsList>(null);
            colCombo.DataSource = AppVars.BLData.GetAll<Gen_SysPolicyDocumentsList>(null);
            colCombo.HeaderText = "Document";
            colCombo.DisplayMember = "DocumentTitle";
            colCombo.ValueMember = "DocumentId";
            colCombo.NullValue = "Select";
            colCombo.Width = 230;
            colCombo.ReadOnly = true;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            grdPolicyDocuments.Columns.Add(colCombo);




            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = COL_SYSPOLICYDOCS.SUBCOMPANYID;
        
            colCombo.DataSource = General.GetQueryable<Gen_SubCompany>(null).ToList();
            colCombo.HeaderText = "Sub Company";
            colCombo.DisplayMember = "CompanyName";
            colCombo.ValueMember = "Id";
           // colCombo.NullValue = "Select";
            colCombo.Width = 200;
           // colCombo.ReadOnly = true;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            grdPolicyDocuments.Columns.Add(colCombo);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Prefix";
            col.Width = 150;
            col.Name = COL_SYSPOLICYDOCS.PREFIX;
            grdPolicyDocuments.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Start Number";
            col.Width = 150;
            col.Name = COL_SYSPOLICYDOCS.STARTNUMBER;
            grdPolicyDocuments.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Last Number";
            col.ReadOnly = true;
            col.Width = 150;
            col.Name = COL_SYSPOLICYDOCS.LASTNUMBER;
            grdPolicyDocuments.Columns.Add(col);




            GridViewCheckBoxColumn  colChk = new GridViewCheckBoxColumn();
            colChk.HeaderText = "Auto Increment";
            colChk.ReadOnly = false;
            colChk.Width = 60;
            colChk.Name = COL_SYSPOLICYDOCS.AUTOINCREMENT;
            grdPolicyDocuments.Columns.Add(colChk);


            grdPolicyDocuments.AllowAddNewRow = false;
            grdPolicyDocuments.ShowGroupPanel = false;
            grdPolicyDocuments.ShowRowHeaderColumn = false;
        }


        private void FormatSurchargeRateGrid()
        {

            grdSurchargeRates.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SURCHARGERATES.ID;
            grdSurchargeRates.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SURCHARGERATES.POLICYID;
            grdSurchargeRates.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_SURCHARGERATES.POSTCODE;
            col.Width = 100;
            col.Name = COL_SURCHARGERATES.POSTCODE;
            grdSurchargeRates.Columns.Add(col);
            
            GridViewCheckBoxColumn colChk = new GridViewCheckBoxColumn();
            colChk.HeaderText = "Amount Wise";
            colChk.Width = 110;
            colChk.Name = COL_SURCHARGERATES.AMOUNTWISE;
            grdSurchargeRates.Columns.Add(colChk);


            GridViewDecimalColumn col2 = new GridViewDecimalColumn();
            col2.HeaderText = "Percentage (%)";
            col2.Width = 110;
            col2.Maximum = 100;
            col2.Name = COL_SURCHARGERATES.PERCENTAGE;
            grdSurchargeRates.Columns.Add(col2);


            col2 = new GridViewDecimalColumn();
            col2.HeaderText = "Amount (£)";
            col2.Width = 110;
            col2.Maximum = 100000;
            col2.Name = COL_SURCHARGERATES.AMOUNT;
            grdSurchargeRates.Columns.Add(col2);



            //col = new GridViewTextBoxColumn();
            //col.HeaderText = "Percentage (%)";
            //col.Width = 130;
            //col.Name = COL_SURCHARGERATES.PERCENTAGE;
            //grdSurchargeRates.Columns.Add(col);



      
            UI.GridFunctions.AddDeleteColumn(grdSurchargeRates);

            grdSurchargeRates.ShowGroupPanel = false;
            grdSurchargeRates.AddNewRowPosition = SystemRowPosition.Bottom;


            grdSurchargeRates.CellBeginEdit += new GridViewCellCancelEventHandler(grdSurchargeRates_CellBeginEdit);

        
        }


        private void FormatRangeWiseCommissionGrid()
        {


            

            grdRangeWiseComm.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_RANGEWISECOMMISSION.ID;
            grdRangeWiseComm.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_RANGEWISECOMMISSION.POLICYID;
            grdRangeWiseComm.Columns.Add(col);


          


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.HeaderText = "From";
            colD.Width = 70;
            colD.ReadOnly = false;
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.Maximum = 100000;
            colD.Name = COL_RANGEWISECOMMISSION.FROMPRICE;
            grdRangeWiseComm.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.HeaderText = "Till";
            colD.Width = 70;
            colD.ReadOnly = false;
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.Maximum = 100000;
            colD.Name = COL_RANGEWISECOMMISSION.TOPRICE;
            grdRangeWiseComm.Columns.Add(colD);



            colD = new GridViewDecimalColumn();
            colD.HeaderText = "Commission %";
            colD.Width = 90;
            colD.ReadOnly = false;
            colD.DecimalPlaces = 0;
            colD.Minimum = 0;
            colD.Maximum = 100;
            colD.Name = COL_RANGEWISECOMMISSION.CHARGESPERCENT;
            grdRangeWiseComm.Columns.Add(colD);

        



            UI.GridFunctions.AddDeleteColumn(grdRangeWiseComm);

            //   grdSurchargeRates.ShowGroupPanel = false;
            grdRangeWiseComm.AddNewRowPosition = SystemRowPosition.Bottom;

        }


        private void FormatAirportWisePickupGrid()
        {


            grdAirportPickupChrges.AllowAddNewRow = false;

            grdAirportPickupChrges.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTPICKUPS.ID;
            grdAirportPickupChrges.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTPICKUPS.POLICYID;
            grdAirportPickupChrges.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTPICKUPS.AIRPORTID;
            grdAirportPickupChrges.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Airport";
            col.Width = 170;
            col.ReadOnly = true;
            col.Name = COL_AIRPORTPICKUPS.AIRPORTNAME;
            grdAirportPickupChrges.Columns.Add(col);


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.HeaderText = "Price";
            colD.Width = 90;
            colD.ReadOnly = false;
            colD.DecimalPlaces = 0;
            colD.Minimum = 0;
            colD.Maximum = 1000;
            colD.Name = COL_AIRPORTPICKUPS.CHARGES;
            grdAirportPickupChrges.Columns.Add(colD);


            var list = General.GetQueryable<Gen_Location>(c => c.LocationTypeId != null && c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT).ToList();

            grdAirportPickupChrges.RowCount = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                grdAirportPickupChrges.Rows[i].Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value = list[i].Id;
                grdAirportPickupChrges.Rows[i].Cells[COL_AIRPORTPICKUPS.AIRPORTNAME].Value = list[i].LocationName.ToStr();
            }




            if (AppVars.objPolicyConfiguration.HasAirportDropOffCharges.ToBool())
            {

                // Airport Wise Dropoffgrid


                grdAirportDropOff.AllowAddNewRow = false;

                grdAirportDropOff.ShowGroupPanel = false;
                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COL_AIRPORTPICKUPS.ID;
                grdAirportDropOff.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COL_AIRPORTPICKUPS.POLICYID;
                grdAirportDropOff.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COL_AIRPORTPICKUPS.AIRPORTID;
                grdAirportDropOff.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.HeaderText = "Airport";
                col.Width = 170;
                col.ReadOnly = true;
                col.Name = COL_AIRPORTPICKUPS.AIRPORTNAME;
                grdAirportDropOff.Columns.Add(col);


                colD = new GridViewDecimalColumn();
                colD.HeaderText = "Price";
                colD.Width = 90;
                colD.ReadOnly = false;
                colD.DecimalPlaces = 0;
                colD.Minimum = 0;
                colD.Maximum = 1000;
                colD.Name = COL_AIRPORTPICKUPS.CHARGES;
                grdAirportDropOff.Columns.Add(colD);


                var list2 = list.ToList();

                grdAirportDropOff.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdAirportDropOff.Rows[i].Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value = list[i].Id;
                    grdAirportDropOff.Rows[i].Cells[COL_AIRPORTPICKUPS.AIRPORTNAME].Value = list[i].LocationName.ToStr();
                }
            }
                                  

        }


      

        private void FormatAirportExpiryGrid()
        {
            grdAirportExpiry.AllowAddNewRow = false;
            grdAirportExpiry.AllowDeleteRow = false;
            grdAirportExpiry.Font = new Font("Tahoma", 10, FontStyle.Bold);

            grdAirportExpiry.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTEXPIRY.ID;
            grdAirportExpiry.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTEXPIRY.POLICYID;
            grdAirportExpiry.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTEXPIRY.LOCATIONID;
            grdAirportExpiry.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Airport";
            col.Width = 310;
            col.ReadOnly = true;
            col.Name = COL_AIRPORTEXPIRY.LOCATIONNAME;
            grdAirportExpiry.Columns.Add(col);


            GridViewDecimalColumn  colD = new GridViewDecimalColumn();
            colD.HeaderText = "Expiry (Mins)";
            colD.Width = 130;
            colD.ReadOnly = false;
            colD.DecimalPlaces = 0;
            colD.Minimum = 0;
            colD.Maximum = 1000;
            colD.Name = COL_AIRPORTEXPIRY.EXPIRY;
            grdAirportExpiry.Columns.Add(colD);


            var list = General.GetQueryable<Gen_Location>(c => c.LocationTypeId != null && c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT).ToList();

            grdAirportExpiry.RowCount = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                grdAirportExpiry.Rows[i].Cells[COL_AIRPORTEXPIRY.LOCATIONID].Value = list[i].Id;
                grdAirportExpiry.Rows[i].Cells[COL_AIRPORTEXPIRY.LOCATIONNAME].Value = list[i].LocationName.ToStr();



            }




         //   UI.GridFunctions.AddDeleteColumn(grdSurchargeRates);

         //   grdSurchargeRates.ShowGroupPanel = false;
         //   grdSurchargeRates.AddNewRowPosition = SystemRowPosition.Bottom;

        }


        private void FormatLocalBookingExpiryGrid()
        {
         //   grdBookingExpiry.AllowAddNewRow = false;
        //    grdBookingExpiry.AllowDeleteRow = false;
            grdBookingExpiry.Font = new Font("Tahoma", 10, FontStyle.Bold);

            grdBookingExpiry.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTEXPIRY.ID;
            grdBookingExpiry.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_AIRPORTEXPIRY.POLICYID;
            grdBookingExpiry.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "PostCode";
            col.Width = 200;
            col.ReadOnly = false;
            col.Name = COL_AIRPORTEXPIRY.LOCATIONNAME;
            grdBookingExpiry.Columns.Add(col);


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.HeaderText = "Expiry (Mins)";
            colD.Width = 130;
            colD.ReadOnly = false;
            colD.DecimalPlaces = 0;
            colD.Minimum = 0;
            colD.Maximum = 1000;
            colD.Name = COL_AIRPORTEXPIRY.EXPIRY;
            grdBookingExpiry.Columns.Add(colD);          

        }






        private void FormatFaresGrid()
        {

            grdFaresSettings.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_FARES.ID;
            grdFaresSettings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_FARES.POLICYID;
            grdFaresSettings.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_FARES.VEHICLETYPEID;
            grdFaresSettings.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle Type";
            col.Width = 120;
            col.Name = COL_FARES.VEHICLETYPE;
            col.ReadOnly = true;
            grdFaresSettings.Columns.Add(col);


            List<RadComboBoxItem> OperatorList=new List<RadComboBoxItem>();
            OperatorList.Add(new RadComboBoxItem("Plus(+)","+"));
            OperatorList.Add(new RadComboBoxItem("Minus(-)","-"));

            var list = (from a in OperatorList
                        select new
                        {
                            Name = a.Text,
                            Id = a.Value
                        }).ToList();

            GridViewComboBoxColumn comboCol = new GridViewComboBoxColumn();
            comboCol.DataSource = list;
            comboCol.DisplayMember = "Name";
            comboCol.ValueMember = "Id";
            comboCol.HeaderText = "Operator";
            comboCol.Width = 80;
            comboCol.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            comboCol.Name = COL_FARES.OPERATOR;
            
            grdFaresSettings.Columns.Add(comboCol);


            GridViewCheckBoxColumn colChk = new GridViewCheckBoxColumn();
            colChk.HeaderText = "Amount Wise";
            colChk.Width = 110;
            colChk.Name = COL_FARES.AMOUNTWISE;
            grdFaresSettings.Columns.Add(colChk);


            GridViewDecimalColumn col2 = new GridViewDecimalColumn();
            col2.HeaderText = "Percentage (%)";
            col2.Width = 110;
            col2.Name = COL_FARES.PERCENTAGE;
            grdFaresSettings.Columns.Add(col2);

            
             col2 = new GridViewDecimalColumn();
             col2.HeaderText = "Amount (£)";
             col2.Width = 110;
             col2.Name = COL_FARES.AMOUNT;
             grdFaresSettings.Columns.Add(col2);


             grdFaresSettings.ShowGroupPanel = false;
             grdFaresSettings.AllowAddNewRow = false;

           
             GridViewRowInfo row = null;
             int defaultVehicleTypeId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
             foreach (var item in General.GetQueryable<Fleet_VehicleType>(c=>c.Id!=defaultVehicleTypeId).OrderBy(c=>c.OrderNo))

             {
                 row=   grdFaresSettings.Rows.AddNew();
                 row.Cells[COL_FARES.VEHICLETYPE].Value   = item.VehicleType;
                 row.Cells[COL_FARES.VEHICLETYPEID].Value = item.Id;                 
             }     

        }

        //private void FormatCompanyFaresGrid()
        //{
        //    if (grdCompanyFaresSettings == null)
        //        return;
        //    grdCompanyFaresSettings.ShowGroupPanel = false;
        //    GridViewTextBoxColumn col = new GridViewTextBoxColumn();
        //    col.IsVisible = false;
        //    col.Name = COL_FARES.ID;
        //    grdCompanyFaresSettings.Columns.Add(col);


        //    col = new GridViewTextBoxColumn();
        //    col.IsVisible = false;
        //    col.Name = COL_FARES.POLICYID;
        //    grdCompanyFaresSettings.Columns.Add(col);




        //    col = new GridViewTextBoxColumn();
        //    col.IsVisible = false;
        //    col.Name = COL_FARES.VEHICLETYPEID;
        //    grdCompanyFaresSettings.Columns.Add(col);




        //    col = new GridViewTextBoxColumn();
        //    col.HeaderText = "Vehicle Type";
        //    col.Width = 120;
        //    col.Name = COL_FARES.VEHICLETYPE;
        //    col.ReadOnly = true;
        //    grdCompanyFaresSettings.Columns.Add(col);


        //    List<RadComboBoxItem> OperatorList = new List<RadComboBoxItem>();
        //    OperatorList.Add(new RadComboBoxItem("Plus(+)", "+"));
        //    OperatorList.Add(new RadComboBoxItem("Minus(-)", "-"));

        //    var list = (from a in OperatorList
        //                select new
        //                {
        //                    Name = a.Text,
        //                    Id = a.Value
        //                }).ToList();

        //    GridViewComboBoxColumn comboCol = new GridViewComboBoxColumn();
        //    comboCol.DataSource = list;
        //    comboCol.DisplayMember = "Name";
        //    comboCol.ValueMember = "Id";
        //    comboCol.HeaderText = "Operator";
        //    comboCol.Width = 80;
        //    comboCol.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
        //    comboCol.Name = COL_FARES.OPERATOR;

        //    grdCompanyFaresSettings.Columns.Add(comboCol);


        //    GridViewCheckBoxColumn colChk = new GridViewCheckBoxColumn();
        //    colChk.HeaderText = "Amount Wise";
        //    colChk.Width = 110;
        //    colChk.Name = COL_FARES.AMOUNTWISE;
        //    grdCompanyFaresSettings.Columns.Add(colChk);


        //    GridViewDecimalColumn col2 = new GridViewDecimalColumn();
        //    col2.HeaderText = "Percentage (%)";
        //    col2.Width = 110;
        //    col2.Name = COL_FARES.PERCENTAGE;
        //    grdCompanyFaresSettings.Columns.Add(col2);


        //    col2 = new GridViewDecimalColumn();
        //    col2.HeaderText = "Amount (£)";
        //    col2.Width = 110;
        //    col2.Name = COL_FARES.AMOUNT;
        //    grdCompanyFaresSettings.Columns.Add(col2);


        //    grdCompanyFaresSettings.ShowGroupPanel = false;
        //    grdCompanyFaresSettings.AllowAddNewRow = false;


        //    GridViewRowInfo row = null;
        //    int defaultVehicleTypeId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
        //    foreach (var item in General.GetQueryable<Fleet_VehicleType>(c => c.Id != defaultVehicleTypeId).OrderBy(c => c.OrderNo))
        //    {
        //        row = grdCompanyFaresSettings.Rows.AddNew();
        //        row.Cells[COL_FARES.VEHICLETYPE].Value = item.VehicleType;
        //        row.Cells[COL_FARES.VEHICLETYPEID].Value = item.Id;
        //    }

        //}


        private void FormatPaymentGatewayGrid()
        {

            grdPaymentGateway.AllowEditRow = true;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_GATEWAY.ID;
            grdPaymentGateway.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_GATEWAY.POLICYID;
            grdPaymentGateway.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_GATEWAY.ACCOUNTYPEID;
            grdPaymentGateway.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_GATEWAY.GATEWAYTYPEID;
            grdPaymentGateway.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Gateway";
            col.Width = 100;
            col.Name = COL_GATEWAY.GATEWAYTYPENAME;
            col.ReadOnly = true;
            grdPaymentGateway.Columns.Add(col);
            
            col = new GridViewTextBoxColumn();
            col.HeaderText = "Merchant ID";
            col.Width = 120;
            col.Name = COL_GATEWAY.MERCHANTID;
            col.ReadOnly = true;
            grdPaymentGateway.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Password";
            col.Width = 120;
            col.Name = COL_GATEWAY.MERCHANTPASSWORD;
            col.ReadOnly = true;
            grdPaymentGateway.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Url";
            col.Width = 120;
            col.Name = COL_GATEWAY.APPLICATIONID;
            col.ReadOnly = true;
            grdPaymentGateway.Columns.Add(col);
            
            col = new GridViewTextBoxColumn();
            col.HeaderText = "Gateway ID";
            col.Width = 100;
            col.Name = COL_GATEWAY.PAYPALID;
            col.ReadOnly = true;
            grdPaymentGateway.Columns.Add(col);

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_GATEWAY.EnableMobileIntegration;
            cbcol.HeaderText = "Mobile Integration";
            cbcol.Width = 120;
            cbcol.ReadOnly = false;
            grdPaymentGateway.Columns.Add(cbcol);

            UI.GridFunctions.AddDeleteColumn(grdPaymentGateway);

        }

      




        private void FormatVOIPGrid()
        {

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_VOIP.ID;
            grdVOIP.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_VOIP.POLICYID;
            grdVOIP.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_VOIP.Account;
            col.Width = 100;
            col.ReadOnly = true;
            col.Name = COL_VOIP.Account;
            grdVOIP.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_VOIP.Password;
            col.Width = 80;
            col.ReadOnly = true;
            col.Name = COL_VOIP.Password;
            grdVOIP.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_VOIP.Host;
            col.Width = 80;
            col.ReadOnly = true;
            col.Name = COL_VOIP.Host;
            grdVOIP.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_VOIP.Port;
            col.Width = 80;
            col.ReadOnly = true;
            col.IsVisible = false;
            col.Name = COL_VOIP.Port;
            grdVOIP.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_VOIP.Proxy;
            col.Width = 80;
            col.ReadOnly = true;
            col.IsVisible = false;
            col.Name = COL_VOIP.Proxy;
            grdVOIP.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_VOIP.UserId;
            col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = COL_VOIP.UserId;
            grdVOIP.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COL_VOIP.User;
            col.Width = 90;
            col.ReadOnly = true;
            col.Name = COL_VOIP.User;
            grdVOIP.Columns.Add(col);



           


            grdVOIP.AllowAddNewRow = false;
            grdVOIP.ShowGroupPanel = false;
            grdVOIP.ShowRowHeaderColumn = false;
        }


        public override void Save()
        {
            try
            {
                Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
                if (obj != null)
                {
                    objMaster.GetByPrimaryKey(obj.Id);
                    objMaster.Edit();

                  

                }



                if (OpenedFromCallerId == false)
                {

                    if (objMaster.Current.Gen_SysPolicy_Configurations.Count == 0)
                    {
                        objMaster.Current.Gen_SysPolicy_Configurations.Add(new Gen_SysPolicy_Configuration());
                        objMaster.Current.Gen_SysPolicy_Configurations[0].SysPolicyId = objMaster.Current.Id;
                    }


                    EnableFullAutoDespatch = objMaster.Current.Gen_SysPolicy_Configurations[0].AutoCloseDrvPopup.ToBool();

                    // Commission and Charges


                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoLogoutInActiveDrvMins = numAutoLogoutDrvMins.Value.ToInt();


                    //

                    objMaster.Current.Gen_SysPolicy_Configurations[0].SharedNetworkPath = txtSharedNetworkPath.Text;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].CallRecordingToken = txtRecordingToken.Text.Trim();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].PreferredMileageFares = chkPreferredMileagePrice.Checked;

              //      objMaster.Current.Gen_SysPolicy_Configurations[0].
                    objMaster.Current.Gen_SysPolicy_Configurations[0].PDANotificationSound = chkNotifySound_PDA.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].SMSNotificationSound = chkNotifySound_SMS.Checked;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].SmtpHost = txtSmtpHost.Text;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].Port = txtPort.Text;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].UserName = txtUserName.Text;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].Password = txtPassword.Text;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableSSL = chkIsSecureConn.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DefaultEmailBody = txtBody.Text.Trim();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].DriverCommissionPerBooking = numDriverCommission.Value.ToDecimal();
                    
                    objMaster.Current.Gen_SysPolicy_Configurations[0].PickCommissionFromCharges = chkPickCommissionFromCharges.Checked.ToBool();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].PickCommissionFromChargesAndWaiting = chkPickCommissionFromFareAndWaiting.Checked.ToBool();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].NoCommissionFromAccount = chkNoACCommission.Checked;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].CashCallCharges =numCashCallCharges.Value.ToDecimal();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoCalculateFares = chkAutoCalculateFares.Checked;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].ApplyAccBgColorOnRow = chkApplyAccClrOnRow.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DisablePopupNotifications = chkDisableNotifications.Checked;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].DriverExpiryNoticeInDays = numDriverExpNoticeDays.Value.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].DiscountForReturnedJourneyPercent = numDiscountPercentageReturn.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DiscountForWRJourneyPercent = numDiscountWaitReturnPercent.Value.ToInt();
              
                    
                    objMaster.Current.Gen_SysPolicy_Configurations[0].BookingExpiryNoticeInMins = numBookingExpNotinceMins.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AirportBookingExpiryNoticeInMins = numAirportBookingExpNotinceMins.Value.ToInt();
                  //  objMaster.Current.Gen_SysPolicy_Configurations[0].BookingAlertExpiryNoticeInMins = numBookingAlertExpNotinceMins.Value.ToInt();


                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchDriverCategoryPriority = ddlAutoDespatchDrvPriorityCategory.SelectedValue.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].FOJLimit = numfojlimit.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AirportPickupCharges = numAirportPickupChrgs.Value.ToDecimal();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePassengerText = chkEnablePassengerText.Checked.ToBool();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableOnlineBookingAuthorization = chkWebBookingAuthorization.Checked;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableMobileBookingAuthorization = chkMobileBookingAuthorization.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].MobileBookingEmailVerification = txtEmailMobileBooking.Text.Trim();


                    objMaster.Current.Gen_SysPolicy_Configurations[0].EarningLoginHours = numShiftEarnHour.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableBookingOtherCharges = chkShowBookingOtherCharges.Checked.ToBool();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].GridRowSize = numGridRowSize.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableReplaceNoToZoneSuggesstion = chkBookingLimits.Checked;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoShowBookingNearestDrv = chkZeroRoundFigures.Checked;
                    
                    // Auto Despatch Tab
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableAutoDespatch = chkEnableAutoDespatch.Checked;

                    if (optAutoDespRule1.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchType = 1;
                   
                    else if (optAutoDespRule2.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchType = 2;
                   
                    else if (optAutoDespRule3.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchType = 3;
                 
                    else if (optAutoDespRule4.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchType = 4;

                    else if (optAutoDespRule5.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchType = 5;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].NoResponseRetry = NumAutoDespAutoRetry.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchElapsedTime = numAutoDespElapsedTime.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].LeastEstimedTimeToClear = numAutoDespLeastEstTime.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchMinsBeforeDue = numAutoDespBeforeDue.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchNearestDrvRadius = numAutoDespDrvRadius.Value.ToDecimal();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchLongestWaitingMins = numAutoDespLongestWaitingMins.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchAllJobs = chkAutoDespatchAllJobs.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchFOJRadius = numAutoDespFOJRadius.Value.ToDecimal();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchASAPMins = numBidPriceLimit.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchPriorityForAllocatedDrv = chkautoDespPriorityToDriver.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchPriorityForAccJobs = chkAutoDespPriorityAccJobs.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoCloseDrvPopup = chkAutoDespatchMode.Checked;



                    // Bidding Tab
                  
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableFixedPlotting = chkAutoPlotting.Checked;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableBidding = chkEnableBidding.Checked;



                    if (optBidRule1.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].BiddingType = 1;

                    else if (optBidRule2.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].BiddingType = 2;

                    else if (optBidRule3.ToggleState == ToggleState.On)
                        objMaster.Current.Gen_SysPolicy_Configurations[0].BiddingType = 3;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].BiddingElapsedTime = numBidElapsedTime.Value.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableBiddingForChauffers = chkEnableBiddingchauffers.Checked;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].FromBidPriceLimit = numBidPriceLimitFrom.Value;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].TillBidPriceLimit = numBidPriceLimitTill.Value;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].ShowJobInBiddingByHours = numShowBiddingJobInHours.Value.ToInt();

                    //
                    objMaster.Current.Gen_SysPolicy_Configurations[0].LogoutInActivityElapsedTime = numTimeoutController.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AccJobsShowNotificationDay = numAcJobNotifyOn.Text.ToStr().Trim();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].SinBinTimer = numsinbin.Value.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableFOJ = chkFOJ.Checked;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoEmailControllerReport = chkAutoEmailControllerReport.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DayOfWeekControllerReport = ddlControllerDay.Text.Trim();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].HourControllerReport = numPreBookingDays.Value.ToInt();


                    objMaster.Current.Gen_SysPolicy_Configurations[0].PreferredShortestDistance = chkShortestDistance.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableOfflineDistance = chkEnableOfflineDistance.Checked.ToBool();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].RemoteIPs = txtRemoteIPs.Text.Trim();



                    objMaster.Current.Gen_SysPolicy_Configurations[0].ExpiredPDAJobHours = numExpiredPDAJobsInHours.Value.ToInt();
              //      objMaster.Current.Gen_SysPolicy_Configurations[0].PDADataFetchInterval = numPDAFetchInSecs.Value.ToInt();

                  //  objMaster.Current.Gen_SysPolicy_Configurations[0].EnableAutoRefreshPDAData = chkEnableAutoRefreshData.Checked;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].ListenerIP = txtListenerIP.Text.ToStr().Trim();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].IsListenAll = chkTCPConnection.Checked;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].ShowAreaWithPlots = chkShowAreaPlots.Checked;


                        

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableQuotation = chkEnableQuotation.Checked;

                    // 21 Jan 2013
                    objMaster.Current.Gen_SysPolicy_Configurations[0].RoundMileageFares = chkIsRoundMileageFare.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].CreditCardExtraCharges = numCreditCardExtraCharge.Value.ToDecimal();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].CreditCardChargesType = ddlCreditCardChrgsType.SelectedIndex + 1;



                    int mapTypeId = 1;
                    if (optMapPoint.ToggleState == ToggleState.On)
                        mapTypeId = Enums.MAP_TYPE.MAPPOINT;
                    else if (optNoneMap.ToggleState == ToggleState.On)
                    {
                        mapTypeId = Enums.MAP_TYPE.NONE;
                    }

                    objMaster.Current.Gen_SysPolicy_Configurations[0].MapType = mapTypeId;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].IsAllCounty = true;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].CountyString = string.Join(",", lstCounty.Items.Select(c => c.Text).ToArray<string>());

                    objMaster.Current.Gen_SysPolicy_Configurations[0].PriorityPostCodes = string.Join(",", LstPriorityPostcodes.Items.Select(c => c.Text).ToArray<string>());



                    objMaster.Current.Gen_SysPolicy_Configurations[0].DaysInTodayBooking = ddlTodayBookingDays.Text.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].DespatchTextForDriver = txtDriverText.Text.Trim();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DespatchTextForCustomer = txtCustomerText.Text.Trim();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePdaDespatchSms = chkDespatchPDASms.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DespatchTextForPDA = txtPDAText.Text.Trim();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].WebBookingText = txtWebBookingText.Text.Trim();


                    objMaster.Current.Gen_SysPolicy_Configurations[0].SMSNoPickup = txtNoPickupText.Text.Trim();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].SMSCancelJob = txtCancelText.Text.Trim();
                
                    objMaster.Current.Gen_SysPolicy_Configurations[0].BaseAddress = txtBaseAddress.Text.Trim();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableAdvanceBookingSMSConfirmation = chkEnableAdvanceBookingText.Checked.ToBool();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AdvanceBookingSMSConfirmationMins = numAfterBookingSMSMinutes.Value.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].AdvanceBookingSMSText = txtAdvBookingTxt.Text.ToStr().Trim();


                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableArrivalBookingText = chkEnableArrivalText.Checked.ToBool();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].ArrivalBookingText = txtArrivalText.Text.ToStr().Trim();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].ArrivalAirportBookingText = txtArrivalAirportText.Text.ToStr().Trim();


                    objMaster.Current.Gen_SysPolicy_Configurations[0].RoundJourneyMiles = numRoundJourneyMiles.Value;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].DriverMonthlyRent = numDriverRent.Value.ToDecimal();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].RentForProcessedJobs = chkRentForProcJobs.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].RentFromDateTime = dtpRentFromDate.Value.ToDateTimeorNull();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].RentToDateTime = dtpRentToDate.Value.ToDateTimeorNull();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DriverSuspensionDateTime = dtpDriverSuspDayTime.Value.ToDateTimeorNull();


                   // objMaster.Current.Gen_SysPolicy_Configurations[0].EnableSpecialDriverText = chkEnableSpecialSMS.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePOI = chkEnablePOI.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].DriverRankType = ddlPDARankType.SelectedIndex.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableApplyStartRateWithInMiles = chkApplyStartRateMiles.Checked; 

                    objMaster.Current.Gen_SysPolicy_Configurations[0].ApplyStartRateWithInMiles = numApplyWithInMiles.Value.ToDecimal();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].RoundUpTo = numRoundUpTo.Value.ToDecimal();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePeakOffPeakFares = chkEnablePeakOffPeakTimeFare.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].FareMeterType = chkEnablePeakOffPeakTimeFare.Checked ? 2 : 1;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableZoneWiseFares = chkEnableZoneWisePricing.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].ZoneWiseFareType = chkEnableZoneWisePricing.Checked ? 4 : 2;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].DeadMileage = numdeadMileage.Value.ToDecimal();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].ListingPagingSize = numPagingSize.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].RecentBookingDays = numRecentJobDays.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].BookingAlertExpiryNoticeInMins = chkBookingAlert.Checked ? 1 : 0;

                    objMaster.Current.Gen_SysPolicy_Configurations[0].AutoBookingDueAlert = chkEnableAutoBookingExpiry.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].AvgFirstMileExpiryMins = numAvgBookingExpMins.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EachAdditionalMileExpiryMins = numminuteseachexpirymile.Value.ToInt();



                    objMaster.Current.Gen_SysPolicy_Configurations[0].ControllerWelcomeMessage = numPaymentTerms.Value.ToStr();
                    // Web Booking Settings
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableWebBooking = chkEnableWebBooking.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].HasWebAccounts = chkWebAccounts.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].WebBookingFetchInMins = numFetchWebBookingInMins.Value.ToInt();
                  //  objMaster.Current.Gen_SysPolicy_Configurations[0].WebBookingBackgroundColor = txtWebBookingBgColor.Tag.ToStr().Trim();


                  //  objMaster.Current.Gen_SysPolicy_Configurations[0].VIPBookingBackgroundColor = txtVIPBookingBgColor.Tag.ToStr().Trim();


                    // PDA Settings
                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePDA = chkEnablePDA.Checked;
                    objMaster.Current.Gen_SysPolicy_Configurations[0].PDAJobOfferRequestTimeout = numJobOfferTimeOut.Value.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].MapRefreshInterval = numMapRefreshInterval.Value.ToInt();

                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnableOnBoardDrivers = chkEnableOnBoardDrivers.Checked;


                    objMaster.Current.Gen_SysPolicy_Configurations[0].PlotsJobExpiryValue1 = numPlotJobExpiry1.Value.ToInt();
                    objMaster.Current.Gen_SysPolicy_Configurations[0].PlotsJobExpiryValue2 = numPlotJobExpiry2.Value.ToInt();


                    objMaster.Current.Gen_SysPolicy_Configurations[0].ConfirmationSMSText = txtConfirmationText.Text;
                 
                    // SMS Settings


                        if (objMaster.Current.Gen_SysPolicy_SMSConfigurations.Count == 0)
                        {
                            objMaster.Current.Gen_SysPolicy_SMSConfigurations.Add(new Gen_SysPolicy_SMSConfiguration());
                            objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].SysPolicyId = objMaster.Current.Id;
                        }

                        int accountType = 0;
                        
                        if(optClickATell.ToggleState== ToggleState.On)
                        {
                            accountType=Enums.SMSACCOUNT_TYPE.CLICKATELL;
                        }
                        else if(optModemSMS.ToggleState== ToggleState.On)
                        {
                            accountType=Enums.SMSACCOUNT_TYPE.MODEMSMS;                     
                        }




                        objMaster.Current.Gen_SysPolicy_Configurations[0].UseMultipleSMSGateways = chkUseSMSAandB.Checked;

          
                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].CanReadSMS = chkCanReadSMS.Checked;

                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].SMSAccountType = accountType;
                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].SMSModemType = optTMobile.ToggleState == ToggleState.On ? "tmobile" : "o2";


                     
                        // CLICK A TELL SMS
                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].ClickSMSUserName = txtClickSMSUserName.Text.Trim();
                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].ClickSMSPassword = txtClickSMSPassword.Text.Trim();
                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].ClickSMSApiKey = txtClickSMSAPIKey.Text.Trim();
                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].ClickSMSSenderName = txtClickSMSSenderName.Text.Trim();


                        // MODEM SMS
                        objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].ModemSMSPortName = ddlModemSMSPortName.Text.ToStr().Trim();



                        // booking fee
                        objMaster.Current.Gen_SysPolicy_Configurations[0].PickCommissionDeductionFromJobsTotal = chkEnableBookingFee.Checked;
                        if (chkEnableBookingFee.Checked)
                        {
                            try
                            {

                                using (TaxiDataContext db = new TaxiDataContext())
                                {

                                    db.CommandTimeout = 5;
                                    var objSer=   db.Gen_ServiceCharges.FirstOrDefault();

                                    if (objSer == null)
                                        objSer = new Gen_ServiceCharge();


                                    objSer.AmountWise = ddlbookingfeetype.Text.ToLower() == "amount"?true:false;
                                    objSer.FromValue = numbookingfee_minumumfare.Value;
                                    objSer.ServiceChargeAmount = numbookingfeevalue.Value;
                                    objSer.ServiceChargePercent = numbookingfeevalue.Value;

                                    objSer.IsAccount = chkbookingfee_onlyaccountjobs.Checked;

                                    if (objSer.Id == 0)
                                        db.Gen_ServiceCharges.InsertOnSubmit(objSer);
                                    db.SubmitChanges();
                                }


                            }
                            catch
                            {

                            }

                        }
                        
                            

                       // Range Wise Commission And Airport Wise Pickup Charges = 03 feb 15

                        objMaster.Current.Gen_SysPolicy_Configurations[0].HasMultipleAirportPickupCharges = chkAirportWisePickup.Checked;

                        string[] skipProperties = new string[] { "Gen_SysPolicy", "Gen_Location" };
                        if (grdAirportPickupChrges.Columns.Count > 0)
                        {


                           
                            List<Gen_SysPolicy_AirportPickupCharge> listofAirportPickup = (from a in grdAirportPickupChrges.Rows
                                                                                           select new Gen_SysPolicy_AirportPickupCharge
                                                                                      {
                                                                                          Id = a.Cells[COL_AIRPORTPICKUPS.ID].Value.ToInt(),
                                                                                          SysPolicyId = objMaster.Current.Id,
                                                                                          AirportId = a.Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value.ToIntorNull(),
                                                                                          Charges = a.Cells[COL_AIRPORTPICKUPS.CHARGES].Value.ToDecimal(),
                                                                                      }).ToList();
                            IList<Gen_SysPolicy_AirportPickupCharge> savedList = objMaster.Current.Gen_SysPolicy_AirportPickupCharges;
                            Utils.General.SyncChildCollection(ref savedList, ref listofAirportPickup, "Id", skipProperties);





                            if (AppVars.objPolicyConfiguration.HasAirportDropOffCharges.ToBool() && grdAirportDropOff.Columns.Count >0)
                            {

                                List<Gen_SysPolicy_AirportDropOffCharge> listofAirportDropOff = (from a in grdAirportDropOff.Rows
                                                                                               select new Gen_SysPolicy_AirportDropOffCharge
                                                                                               {
                                                                                                   Id = a.Cells[COL_AIRPORTPICKUPS.ID].Value.ToInt(),
                                                                                                   SysPolicyId = objMaster.Current.Id,
                                                                                                   AirportId = a.Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value.ToIntorNull(),
                                                                                                   Charges = a.Cells[COL_AIRPORTPICKUPS.CHARGES].Value.ToDecimal(),
                                                                                               }).ToList();
                                IList<Gen_SysPolicy_AirportDropOffCharge> savedListDropOff = objMaster.Current.Gen_SysPolicy_AirportDropOffCharges;
                                Utils.General.SyncChildCollection(ref savedListDropOff, ref listofAirportDropOff, "Id", skipProperties);
                            }
                        }


                        objMaster.Current.Gen_SysPolicy_Configurations[0].PriceRangeWiseCommission = chkRangeWiseCommission.Checked;


                        if ( grdRangeWiseComm.Columns.Count > 0)
                        {
                            List<Gen_SysPolicy_CommissionPriceRange> listofRangeWiseComm = (from a in grdRangeWiseComm.Rows
                                                                                            select new Gen_SysPolicy_CommissionPriceRange
                                                                                           {
                                                                                               Id = a.Cells[COL_RANGEWISECOMMISSION.ID].Value.ToInt(),
                                                                                               SysPolicyId = objMaster.Current.Id,
                                                                                               FromPrice = a.Cells[COL_RANGEWISECOMMISSION.FROMPRICE].Value.ToDecimal(),
                                                                                               ToPrice = a.Cells[COL_RANGEWISECOMMISSION.TOPRICE].Value.ToDecimal(),
                                                                                               CommissionValue = a.Cells[COL_RANGEWISECOMMISSION.CHARGESPERCENT].Value.ToDecimal(),
                                                                                           }).ToList();
                            IList<Gen_SysPolicy_CommissionPriceRange> savedListRange = objMaster.Current.Gen_SysPolicy_CommissionPriceRanges;
                            Utils.General.SyncChildCollection(ref savedListRange, ref listofRangeWiseComm, "Id", skipProperties);
                        }


                       //


                        skipProperties = new string[] { "Gen_SysPolicy", "Gen_SubCompany" ,"Suffix","LastNumber"};
                        List<Gen_SysPolicy_DocumentNumberSetup> list = (from a in grdPolicyDocuments.Rows
                                                                    select new Gen_SysPolicy_DocumentNumberSetup
                                                                    {
                                                                        Id = a.Cells[COL_SYSPOLICYDOCS.ID].Value.ToInt(),
                                                                        SysPolicyId = a.Cells[COL_SYSPOLICYDOCS.POLICYID].Value.ToIntorNull(),
                                                                        DocumentId = a.Cells[COL_SYSPOLICYDOCS.DOCUMENTID].Value.ToStr(),
                                                                        Prefix = a.Cells[COL_SYSPOLICYDOCS.PREFIX].Value.ToStr(),
                                                                        StartNumber = a.Cells[COL_SYSPOLICYDOCS.STARTNUMBER].Value.ToLong(),
                                                                        LastNumber = a.Cells[COL_SYSPOLICYDOCS.LASTNUMBER].Value.ToLong(),
                                                                        SubcompanyId=a.Cells[COL_SYSPOLICYDOCS.SUBCOMPANYID].Value.ToIntorNull(),
                                                                        AutoIncrement=a.Cells[COL_SYSPOLICYDOCS.AUTOINCREMENT].Value.ToBool()

                                                                    }).ToList();
                    IList<Gen_SysPolicy_DocumentNumberSetup> savedList1 = objMaster.Current.Gen_SysPolicy_DocumentNumberSetups;
                    Utils.General.SyncChildCollection(ref savedList1, ref list, "Id", skipProperties);



                  


                    //if (grdSurchargeRates != null)
                    //{
                    //    skipProperties = new string[] { "Gen_SysPolicy", "Fleet_VehicleType" };
                    //    List<Gen_SysPolicy_SurchargeRate> listRates = (from a in grdSurchargeRates.Rows
                    //                                                   select new Gen_SysPolicy_SurchargeRate
                    //                                                         {
                    //                                                             Id = a.Cells[COL_SURCHARGERATES.ID].Value.ToLong(),
                    //                                                             SysPolicyId = objMaster.Current.Id,
                    //                                                             PostCode = a.Cells[COL_SURCHARGERATES.POSTCODE].Value.ToStr().Trim().ToUpper(),
                    //                                                             Percentage = a.Cells[COL_SURCHARGERATES.PERCENTAGE].Value.ToDecimal(),
                    //                                                             IsAmountWise=a.Cells[COL_SURCHARGERATES.AMOUNTWISE].Value.ToBool(),
                    //                                                             Amount=a.Cells[COL_SURCHARGERATES.AMOUNT].Value.ToDecimal()
                    //                                                         }).ToList();

                    //    IList<Gen_SysPolicy_SurchargeRate> savedListRates = objMaster.Current.Gen_SysPolicy_SurchargeRates;
                    //    Utils.General.SyncChildCollection(ref savedListRates, ref listRates, "Id", skipProperties);
                    //}


                    skipProperties = new string[] { "Gen_SysPolicy", "Gen_Location" };
                    List<Gen_Syspolicy_LocationExpiry> listofLocExpiry = (from a in grdAirportExpiry.Rows
                                                                    select new Gen_Syspolicy_LocationExpiry
                                                                   {
                                                                       Id = a.Cells[COL_AIRPORTEXPIRY.ID].Value.ToInt(),
                                                                       SysPolicyId = objMaster.Current.Id,
                                                                        LocationId = a.Cells[COL_AIRPORTEXPIRY.LOCATIONID].Value.ToIntorNull(),
                                                                        ExpiryMins = a.Cells[COL_AIRPORTEXPIRY.EXPIRY].Value.ToInt(),
                                                                   }).ToList();

                 


                    listofLocExpiry.AddRange ((from a in grdBookingExpiry.Rows
                                                                          select new Gen_Syspolicy_LocationExpiry
                                                                          {
                                                                              Id = a.Cells[COL_AIRPORTEXPIRY.ID].Value.ToInt(),
                                                                              SysPolicyId = objMaster.Current.Id,
                                                                               LocationPostCode = a.Cells[COL_AIRPORTEXPIRY.LOCATIONNAME].Value.ToStr().Trim(),
                                                                              ExpiryMins = a.Cells[COL_AIRPORTEXPIRY.EXPIRY].Value.ToInt(),
                                                                          }).ToList());


                    IList<Gen_Syspolicy_LocationExpiry> savedListLocExpiry = objMaster.Current.Gen_Syspolicy_LocationExpiries;
                    Utils.General.SyncChildCollection(ref savedListLocExpiry, ref listofLocExpiry, "Id", skipProperties);



                    //skipProperties = new string[] { "Gen_SysPolicy", "PaymentGateway","EnableMobileIntegration" };
                    skipProperties = new string[] { "Gen_SysPolicy", "PaymentGateway" };

                    List<Gen_SysPolicy_PaymentDetail> listGateways = (from a in grdPaymentGateway.Rows
                                                                      select new Gen_SysPolicy_PaymentDetail
                                                                      {
                                                                          Id = a.Cells[COL_GATEWAY.ID].Value.ToInt(),
                                                                          SysPolicyId = a.Cells[COL_GATEWAY.POLICYID].Value.ToInt(),
                                                                          MerchantID = a.Cells[COL_GATEWAY.MERCHANTID].Value.ToStr(),
                                                                          MerchantPassword = a.Cells[COL_GATEWAY.MERCHANTPASSWORD].Value.ToStr(),
                                                                        //  ApiUsername = a.Cells[COL_GATEWAY.APIUSERNAME].Value.ToStr(),
                                                                        //  ApiPassword = a.Cells[COL_GATEWAY.APIPASSWORD].Value.ToStr(),
                                                                        //  ApiSignature = a.Cells[COL_GATEWAY.APISIGNATURE].Value.ToStr(),
                                                                        
                                                                          ApplicationId = a.Cells[COL_GATEWAY.APPLICATIONID].Value.ToStr(),
                                                                          PaymentGatewayId = a.Cells[COL_GATEWAY.GATEWAYTYPEID].Value.ToInt(),
                                                                          ApiCertificate = a.Cells[COL_GATEWAY.ACCOUNTYPEID].Value.ToStr(),
                                                                          PaypalID = a.Cells[COL_GATEWAY.PAYPALID].Value.ToStr(),
                                                                          EnableMobileIntegration=a.Cells[COL_GATEWAY.EnableMobileIntegration].Value.ToBool()
                                                                      }).ToList();

                    IList<Gen_SysPolicy_PaymentDetail> savedListGateways = objMaster.Current.Gen_SysPolicy_PaymentDetails;
                    Utils.General.SyncChildCollection(ref savedListGateways, ref listGateways, "Id", skipProperties);


                    if (pnlFares != null)
                    {
                       

                        skipProperties = new string[] { "Gen_SysPolicy", "Fleet_VehicleType", "EnableForBookOnline"};
                        List<Gen_SysPolicy_FaresSetting> listofFares = (from a in grdFaresSettings.Rows
                                                                        select new Gen_SysPolicy_FaresSetting
                                                                        {
                                                                            Id = a.Cells[COL_FARES.ID].Value.ToInt(),
                                                                            SysPolicyId = objMaster.Current.Id,
                                                                            IsAmountWise = a.Cells[COL_FARES.AMOUNTWISE].Value.ToBool(),
                                                                            Amount = a.Cells[COL_FARES.AMOUNT].Value.ToDecimal(),
                                                                            Percentage = a.Cells[COL_FARES.PERCENTAGE].Value.ToInt(),
                                                                            VehicleTypeId = a.Cells[COL_FARES.VEHICLETYPEID].Value.ToIntorNull(),
                                                                            Operator = a.Cells[COL_FARES.OPERATOR].Value.ToStr()
                                                                        }).ToList();

                      
                   
                         //List<Gen_SysPolicy_FaresSetting> listofCompanyFares = (from a in grdCompanyFaresSettings.Rows
                         //                                                   select new Gen_SysPolicy_FaresSetting
                         //                                                   {
                         //                                                       Id = a.Cells[COL_FARES.ID].Value.ToInt(),
                         //                                                       SysPolicyId = objMaster.Current.Id,
                         //                                                       IsAmountWise = a.Cells[COL_FARES.AMOUNTWISE].Value.ToBool(),
                         //                                                       Amount = a.Cells[COL_FARES.AMOUNT].Value.ToDecimal(),
                         //                                                       Percentage = a.Cells[COL_FARES.PERCENTAGE].Value.ToInt(),
                         //                                                       VehicleTypeId = a.Cells[COL_FARES.VEHICLETYPEID].Value.ToIntorNull(),
                         //                                                       Operator = a.Cells[COL_FARES.OPERATOR].Value.ToStr(),
                         //                                                       IsCompanyWise=true
                         //                                                   }).ToList();

                          

                         //   List<Gen_SysPolicy_FaresSetting> finalFareList = listofFares.Union(listofCompanyFares).ToList();


                            IList<Gen_SysPolicy_FaresSetting> savedListFares = objMaster.Current.Gen_SysPolicy_FaresSettings;

                            Utils.General.SyncChildCollection(ref savedListFares, ref listofFares, "Id", skipProperties);
                        
                    }


                    // for SMS Templet

                    skipProperties = new string[] { "Gen_SysPolicy" };
                    IList<Gen_SysPolicy_SMSTemplet> savedListTemp = objMaster.Current.Gen_SysPolicy_SMSTemplets;
                    List<Gen_SysPolicy_SMSTemplet> listofDetailTemp = (from r in grdSMSTemplets.Rows

                                                                       select new Gen_SysPolicy_SMSTemplet
                                                                       {
                                                                           Id = r.Cells[COL_SMSTEMPLET.ID].Value.ToInt(),
                                                                           Templet = r.Cells[COL_SMSTEMPLET.Tempplet].Value.ToString(),
                                                                           SysPolicyId = r.Cells[COL_SMSTEMPLET.POLICYID].Value.ToInt(),

                                                                       }).ToList();


                    Utils.General.SyncChildCollection(ref savedListTemp, ref listofDetailTemp, "Id", skipProperties);

                }

                if (objMaster.Current.CallerIdType_Configurations.Count == 0)
                {
                    objMaster.Current.CallerIdType_Configurations.Add(new CallerIdType_Configuration());
                    objMaster.Current.CallerIdType_Configurations[0].SysPolicyId = objMaster.Current.Id;
                }
              
                // Analog
                objMaster.Current.CallerIdType_Configurations[0].IsAnalog = chkAnalog.Checked;

                objMaster.Current.CallerIdType_Configurations[0].AnalogCLIType = optAnalogEthernet.ToggleState == ToggleState.On ? Enums.ANALOG_CLITYPE.ETHERNET
                                                                                                                               : Enums.ANALOG_CLITYPE.SERIALPORT;

              
                
                objMaster.Current.CallerIdType_Configurations[0].SerialBaudRate = cmbBaudRate.Text.Trim();
                objMaster.Current.CallerIdType_Configurations[0].SerialComPort = cmbPortName.Text.Trim();
                objMaster.Current.CallerIdType_Configurations[0].SerialDataBits = cmbDataBits.Text.Trim();
                objMaster.Current.CallerIdType_Configurations[0].SerialParityBits = cmbParity.Text.Trim();
                objMaster.Current.CallerIdType_Configurations[0].SerialStopBits = cmbStopBits.Text.Trim();

                // Digital Tapi
                bool IsDigital=chkDigital.Checked;
                objMaster.Current.CallerIdType_Configurations[0].IsDigital = IsDigital;

               

                if (opt_Digital_CTE.ToggleState == ToggleState.On)
                {
                    string lines = string.Join(",", lstLines.CheckedItems.Cast<ListViewItem>().Select(c => c.Text).ToArray<string>());
                    objMaster.Current.CallerIdType_Configurations[0].TapiDriverName = lines.ToStr().Trim();
                    objMaster.Current.CallerIdType_Configurations[0].DigitalCLIType = 1;

                }
                else if (opt_Digital_CDR.ToggleState == ToggleState.On)
                {
                    objMaster.Current.CallerIdType_Configurations[0].DigitalCLIType = 2;


                }
                else if (opt_digital_cti.ToggleState == ToggleState.On)
                {
                    string lines = string.Join(",", lstCTILines.CheckedItems.Cast<ListViewItem>().Select(c => c.Text).ToArray<string>());
                    objMaster.Current.CallerIdType_Configurations[0].TapiDriverName = lines.ToStr().Trim();
                    objMaster.Current.CallerIdType_Configurations[0].DigitalCLIType = 3;


                }


                //objMaster.Current.CallerIdType_Configurations[0].DigitalCLIType = opt_Digital_CTE.ToggleState == ToggleState.On ? 1      : 2;

                objMaster.Current.Gen_SysPolicy_Configurations[0].ShowCLIPopupOnAnswer = chkAnswerCTE.Checked;


                objMaster.Current.CallerIdType_Configurations[0].CdrIP = txt_cdr_ip.Text.Trim();
                objMaster.Current.CallerIdType_Configurations[0].CdrUserName = txt_cdr_username.Text.Trim();
                objMaster.Current.CallerIdType_Configurations[0].CdrPassword = txt_cdr_password.Text.Trim();

                // Voip SIP
                objMaster.Current.CallerIdType_Configurations[0].IsVOIP =chkSip.Checked;
              //  objMaster.Current.CallerIdType_Configurations[0].ReceiveVOIPCallFromPhone = chkRecvFromFone.Checked.ToBool();

               


                // FILE CLI
                objMaster.Current.CallerIdType_Configurations[0].IsFileCLI = chkFileCli.Checked;
                objMaster.Current.CallerIdType_Configurations[0].FileCliDirPath = txtFileCliDirPath.Text.ToStr().Trim();



                if (opt_voipsip.ToggleState == ToggleState.On )
                {

                   
                       objMaster.Current.CallerIdType_Configurations[0].VOIPCLIType = 1;
                 


                    List<CallerIdVOIP_Configuration> listofVOIP = (from a in grdVOIP.Rows
                                                                   select new CallerIdVOIP_Configuration
                                                                   {
                                                                       Id = a.Cells[COL_VOIP.ID].Value.ToInt(),
                                                                       SysPolicyId = a.Cells[COL_VOIP.POLICYID].Value.ToInt(),
                                                                       UserId = a.Cells[COL_VOIP.UserId].Value.ToIntorNull(),
                                                                       AccountId = a.Cells[COL_VOIP.Account].Value.ToStr(),
                                                                       Password = a.Cells[COL_VOIP.Password].Value.ToStr(),
                                                                       Host = a.Cells[COL_VOIP.Host].Value.ToStr(),
                                                                       UserName = a.Cells[COL_VOIP.User].Value.ToStr(),
                                                                    //    ProxyAddress = a.Cells[COL_VOIP.Proxy].Value.ToStr(),
                                                                 //       Port = a.Cells[COL_VOIP.Port].Value.ToStr()
                                                                   }).ToList();


                 
                    IList<CallerIdVOIP_Configuration> savedListVOIP = objMaster.Current.CallerIdVOIP_Configurations;
                    Utils.General.SyncChildCollection(ref savedListVOIP, ref listofVOIP, "Id", new string[] { "Gen_SysPolicy" });
                }
                else if (opt_voipasterisk.ToggleState == ToggleState.On)
                {
                      
                   
                        objMaster.Current.CallerIdType_Configurations[0].VOIPCLIType = 2;
             
                       


                    if (objMaster.Current.CallerIdVOIP_Configurations.Count > 1)
                    {
                        objMaster.Current.CallerIdVOIP_Configurations.Clear();

                    }

                    if (objMaster.Current.CallerIdVOIP_Configurations.Count == 1)
                    {

                        
                            objMaster.Current.CallerIdVOIP_Configurations[0].Port = txtVoip_AsteriskPortName.Text.ToStr();
                            objMaster.Current.CallerIdVOIP_Configurations[0].UserName = SIP_txtUserName.Text.ToStr();
                            objMaster.Current.CallerIdVOIP_Configurations[0].AccountId = SIP_txtUserName.Text.ToStr();
                            objMaster.Current.CallerIdVOIP_Configurations[0].Host = SIP_txtHost.Text.ToStr();
                            objMaster.Current.CallerIdVOIP_Configurations[0].Password = SIP_txtPassword.Text.ToStr();
                            objMaster.Current.CallerIdVOIP_Configurations[0].ProxyAddress = SIP_BTProxy.Text.ToStr();
                       
                       
                    }
                    else
                    {
                        

                            objMaster.Current.CallerIdVOIP_Configurations.Add(new CallerIdVOIP_Configuration
                            {
                                Port = SIP_BT_port.Text.ToStr(),
                                UserName = SIP_txtUserName.Text.ToStr(),
                                AccountId = txtVoip_AsteriskPortName.Text.ToStr(),
                                Password = SIP_txtPassword.Text.ToStr(),
                                Host = SIP_txtHost.Text.ToStr(),
                                SysPolicyId = objMaster.Current.Id,
                                ProxyAddress = SIP_BTProxy.Text.Trim().ToStr(),

                            }
                            );
                        
                  

                    }


                }
                else if (opt_voipbt.ToggleState == ToggleState.On)
                {

                    objMaster.Current.CallerIdType_Configurations[0].VOIPCLIType = 3;



                    List<CallerIdVOIP_Configuration> listofVOIP = (from a in grdVOIP.Rows
                                                                   select new CallerIdVOIP_Configuration
                                                                   {
                                                                       Id = a.Cells[COL_VOIP.ID].Value.ToInt(),
                                                                       SysPolicyId = a.Cells[COL_VOIP.POLICYID].Value.ToInt(),
                                                                    
                                                                       AccountId = a.Cells[COL_VOIP.Account].Value.ToStr(),
                                                                       Password = a.Cells[COL_VOIP.Password].Value.ToStr(),
                                                                       Host = a.Cells[COL_VOIP.Host].Value.ToStr(),
                                                                       UserName = a.Cells[COL_VOIP.User].Value.ToStr(),

                                                                       ProxyAddress = a.Cells[COL_VOIP.Proxy].Value.ToStr(),
                                                                       Port = a.Cells[COL_VOIP.Port].Value.ToStr()
                                                                   }).ToList();



                    IList<CallerIdVOIP_Configuration> savedListVOIP = objMaster.Current.CallerIdVOIP_Configurations;
                    Utils.General.SyncChildCollection(ref savedListVOIP, ref listofVOIP, "Id", new string[] { "Gen_SysPolicy" });

                }

                else if  (opt_emerald.ToggleState == ToggleState.On)
                {


                    objMaster.Current.CallerIdType_Configurations[0].VOIPCLIType = 4;




                    if (objMaster.Current.CallerIdVOIP_Configurations.Count > 1)
                    {
                        objMaster.Current.CallerIdVOIP_Configurations.Clear();

                    }

                    if (objMaster.Current.CallerIdVOIP_Configurations.Count == 1)
                    {


                        objMaster.Current.CallerIdVOIP_Configurations[0].Port = txtVoip_AsteriskPortName.Text.ToStr();
                        objMaster.Current.CallerIdVOIP_Configurations[0].UserName = SIP_txtUserName.Text.ToStr();
                        objMaster.Current.CallerIdVOIP_Configurations[0].AccountId = SIP_txtUserName.Text.ToStr();
                        objMaster.Current.CallerIdVOIP_Configurations[0].Host = SIP_txtHost.Text.ToStr();
                        objMaster.Current.CallerIdVOIP_Configurations[0].Password = SIP_txtPassword.Text.ToStr();
                        objMaster.Current.CallerIdVOIP_Configurations[0].ProxyAddress = SIP_BTProxy.Text.ToStr();


                    }
                    else
                    {


                        objMaster.Current.CallerIdVOIP_Configurations.Add(new CallerIdVOIP_Configuration
                        {
                            Port = SIP_BT_port.Text.ToStr(),
                            UserName = SIP_txtUserName.Text.ToStr(),
                            AccountId = txtVoip_AsteriskPortName.Text.ToStr(),
                            Password = SIP_txtPassword.Text.ToStr(),
                            Host = SIP_txtHost.Text.ToStr(),
                            SysPolicyId = objMaster.Current.Id,
                            ProxyAddress = SIP_BTProxy.Text.Trim().ToStr(),

                        }
                        );



                    }


                }


                if (EnableFullAutoDespatch == false && objMaster.Current.Gen_SysPolicy_Configurations.Count > 0 && objMaster.Current.Gen_SysPolicy_Configurations[0].AutoCloseDrvPopup.ToBool())
                {
                    try
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {

                            if (db.Gen_Zones.Count(c =>c.EnableAutoDespatch==null || c.EnableAutoDespatch == false) == db.Gen_Zones.Count())
                            {

                                int expiry = objMaster.Current.Gen_SysPolicy_Configurations[0].BookingExpiryNoticeInMins.ToInt();

                                if (expiry == 0)
                                    expiry = 10;
                                else if (expiry >= 60)
                                    expiry = 15;

                                foreach (var item in db.Gen_Zones)
                                {
                                    item.EnableAutoDespatch = true;
                                    item.EnableBidding = true;

                           

                                    item.JobDueTime = new DateTime(1753, 1, 1, 0, expiry, 0);
                                    
                                   
                                }

                                db.SubmitChanges();



                            }


                        }



                    }
                    catch
                    {



                    }

                }

                objMaster.Save();

               
                if (ChangeEmailSettings)
                {
                    SaveMainCompany();
                }


                objMaster.GetByPrimaryKey(objMaster.Current.Id);

                SaveExtensions();


                if (grdPaymentGateway.Rows.Count(c=>c.Cells[COL_GATEWAY.EnableMobileIntegration].Value.ToBool())>0)
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var objPayment = db.Gen_PaymentColumnSettings.FirstOrDefault(c => c.Id > 0); //General.GetObject<Gen_PaymentColumnSetting>(c => c.Id > 0);
                        if (objPayment != null)
                        {
                            objPayment.ShowFares = chkShowFares.Checked;
                            objPayment.EditFares = chkEditFares.Checked;
                            objPayment.ShowParking = chkShowParking.Checked;
                            objPayment.EditParking = chkEditParking.Checked;
                            objPayment.ShowWaiting = chkShowWaiting.Checked;
                            objPayment.EditWaiting = chkEditWaiting.Checked;



                            objPayment.ShowTotal = true;

                            objPayment.ChargesType = Enums.PAYMENT_CHARGESTYPE.CHARGESTYPE2;
                            db.SubmitChanges();
                        }
                        else
                        {
                            Gen_PaymentColumnSetting objPaymentColumnSetting = new Gen_PaymentColumnSetting();
                            objPaymentColumnSetting.ShowFares = chkShowFares.Checked;
                            objPaymentColumnSetting.EditFares = chkEditFares.Checked;
                            objPaymentColumnSetting.ShowParking = chkShowParking.Checked;
                            objPaymentColumnSetting.EditParking = chkEditParking.Checked;
                            objPaymentColumnSetting.ShowWaiting = chkShowWaiting.Checked;
                            objPaymentColumnSetting.EditWaiting = chkEditWaiting.Checked;
                            objPaymentColumnSetting.ShowTotal = true;
                            objPaymentColumnSetting.ChargesType = Enums.PAYMENT_CHARGESTYPE.CHARGESTYPE2;
                            db.Gen_PaymentColumnSettings.InsertOnSubmit(objPaymentColumnSetting);
                            db.SubmitChanges();

                        }
                    }
                }

                DisplayRecord();

                if (grdSurchargeRates != null)
                    DisplaySurchargeRates();

                if (pnlFares != null)
                    DisplayFaresSettings();

                //if (pnlCompanyFares != null)
                //    DisplayCompanyFaresSettings();

                General.LoadConfiguration();
              //  AppVars.objPolicyConfiguration = General.GetObject<Gen_SysPolicy_Configuration>(c => c.SysPolicyId == 1);

     

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());

            }

        }

        private bool EnableFullAutoDespatch = false;


        private void SaveExtensions()
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\Service.xml";
                if (File.Exists(path))
                {


                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode ParentNode = doc.GetElementsByTagName("extensions").OfType<XmlNode>().FirstOrDefault();

                    ParentNode.InnerXml = string.Empty;
                    doc.Save(path);

                 

                    foreach (DataGridViewRow row in grdCLIExtensions.Rows)
                    {

                        if (row.Cells["MachineName"].Value == null)
                            continue;

                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "extension", "");



                        node.InnerXml = "<machinename>" + row.Cells["MachineName"].Value.ToStr() + "</machinename>" + Environment.NewLine
                               + "<stn>" + row.Cells["Extension"].Value.ToStr() + "</stn>";
                               
                               //+Environment.NewLine+
                               //" <ForwardNumber>"+row.Cells["ForwardNumber"].Value.ToStr() + "</ForwardNumber>";




                       // doc.AppendChild(ParentNode);

                        ParentNode.AppendChild(node);


                    }

                    doc.Save(path);
                }
         
            }
            catch (Exception ex)
            {



            }

        }


        private void DisplayExtensions()
        {
            try
            {
                grdCLIExtensions.Rows.Clear();
                string path = System.Windows.Forms.Application.StartupPath + "\\Service.xml";
                if (File.Exists(path))
                {


                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode ParentNode = doc.GetElementsByTagName("extensions").OfType<XmlNode>().FirstOrDefault();




                    foreach (XmlNode node in ParentNode.ChildNodes)
                    {




                      

                            grdCLIExtensions.Rows.Add(node.FirstChild.InnerText, node.LastChild.InnerText);
                       
                        //else if(node.ChildNodes.Count > 2)
                        //{
                        //    grdCLIExtensions.Rows.Add(node.FirstChild.InnerText, node.ChildNodes[1].InnerText,node.LastChild.InnerText);

                        //}


                    }

                
                }

            }
            catch (Exception ex)
            {



            }

        }
     



      
        private void FillTapiDigitalLinesList()
        {
            try
            {
                lstLines.Items.Clear();

                lstLines.Items.AddRange(CIDTapiLauncher.GetDriversListArray());


                lstCTILines.Items.AddRange(CIDCTITapiLauncher.GetDriversListArray());
            }
            catch
            {


            }
        }


        public override void DisplayRecord()
        {
            try
            {
                if (objMaster.Current != null)
                {
                    IsDisplayingRecord = true;
                    if (OpenedFromCallerId == false)
                    {

                        Gen_SysPolicy_Configuration obj;
                        if (objMaster.Current.Gen_SysPolicy_Configurations.Count == 1)
                        {
                            obj = objMaster.Current.Gen_SysPolicy_Configurations[0];



                            txtRecordingToken.Text = obj.CallRecordingToken.ToStr().Trim();

                            txtSharedNetworkPath.Text = obj.SharedNetworkPath;
                            txtSmtpHost.Text = obj.SmtpHost;
                            txtPort.Text = obj.Port;
                            txtUserName.Text = obj.UserName;
                            txtPassword.Text = obj.Password;
                            chkIsSecureConn.Checked = obj.EnableSSL.ToBool();
                            txtBody.Text = obj.DefaultEmailBody.ToStr().Trim();

                            chkNotifySound_SMS.Checked = obj.SMSNotificationSound.ToBool();
                            chkNotifySound_PDA.Checked = obj.PDANotificationSound.ToBool();

                            numDriverCommission.Value = obj.DriverCommissionPerBooking.ToDecimal();
                            chkPickCommissionFromCharges.Checked = obj.PickCommissionFromCharges.ToBool();
                            chkPickCommissionFromFareAndWaiting.Checked = obj.PickCommissionFromChargesAndWaiting.ToBool();

                            chkNoACCommission.Checked = obj.NoCommissionFromAccount.ToBool();

                            numdeadMileage.Value = obj.DeadMileage.ToDecimal();

                            numCashCallCharges.Value = obj.CashCallCharges.ToDecimal();
                            numBookingExpNotinceMins.Value = obj.BookingExpiryNoticeInMins.ToDecimal();
                            numAirportBookingExpNotinceMins.Value = obj.AirportBookingExpiryNoticeInMins.ToDecimal();
                        
                            numAirportPickupChrgs.Value = obj.AirportPickupCharges.ToDecimal();


                            // Charges and Range Wise commission

                            chkAirportWisePickup.Checked = obj.HasMultipleAirportPickupCharges.ToBool();
                            chkRangeWiseCommission.Checked = obj.PriceRangeWiseCommission.ToBool();


                            chkDisableNotifications.Checked = obj.DisablePopupNotifications.ToBool();
                            chkApplyAccClrOnRow.Checked = obj.ApplyAccBgColorOnRow.ToBool();

                            

                                foreach (var item in objMaster.Current.Gen_SysPolicy_AirportPickupCharges.Where(c => c.AirportId != null))
                                {


                                    GridViewRowInfo grow = grdAirportPickupChrges.Rows.FirstOrDefault(a => a.Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value.ToInt() == item.AirportId.ToInt());
                                    if (grow != null)
                                    {
                                        grow.Cells[COL_AIRPORTPICKUPS.ID].Value = item.Id;
                                        grow.Cells[COL_AIRPORTPICKUPS.POLICYID].Value = item.SysPolicyId;
                                        grow.Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value = item.AirportId.ToIntorNull();
                                        grow.Cells[COL_AIRPORTPICKUPS.AIRPORTNAME].Value = item.Gen_Location.DefaultIfEmpty().LocationName.ToStr();
                                        grow.Cells[COL_AIRPORTPICKUPS.CHARGES].Value = item.Charges.ToDecimal();

                                     //   grow.Cells[COL_AIRPORTPICKUPS.AIRPORTNAME].Value = item.AirportName.ToStr();
                                     //   grow.Cells[COL_AIRPORTPICKUPS.CHARGES].Value = item.Charges.ToDecimal();


                                    }
                                }



                                if (AppVars.objPolicyConfiguration.HasAirportDropOffCharges.ToBool() && grdAirportPickupChrges.Columns.Count >0)
                                {
                                    foreach (var item in objMaster.Current.Gen_SysPolicy_AirportDropOffCharges.Where(c => c.AirportId != null))
                                    {


                                        GridViewRowInfo grow = grdAirportDropOff.Rows.FirstOrDefault(a => a.Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value.ToInt() == item.AirportId.ToInt());
                                        if (grow != null)
                                        {
                                            grow.Cells[COL_AIRPORTPICKUPS.ID].Value = item.Id;
                                            grow.Cells[COL_AIRPORTPICKUPS.POLICYID].Value = item.SysPolicyId;
                                            grow.Cells[COL_AIRPORTPICKUPS.AIRPORTID].Value = item.AirportId.ToIntorNull();
                                            grow.Cells[COL_AIRPORTPICKUPS.AIRPORTNAME].Value = item.Gen_Location.DefaultIfEmpty().LocationName.ToStr();
                                            grow.Cells[COL_AIRPORTPICKUPS.CHARGES].Value = item.Charges.ToDecimal();



                                        }
                                    }
                                }




                                // booking fee
                                chkEnableBookingFee.Checked = objMaster.Current.Gen_SysPolicy_Configurations[0].PickCommissionDeductionFromJobsTotal.ToBool();
                                if (chkEnableBookingFee.Checked)
                                {
                                    try
                                    {

                                        using (TaxiDataContext db = new TaxiDataContext())
                                        {

                                           // db.CommandTimeout = 5;
                                            var objSer = db.Gen_ServiceCharges.FirstOrDefault();

                                            if (objSer != null)
                                            {


                                                ddlbookingfeetype.Text=  objSer.AmountWise.ToBool() ?"Amount":"Percent";
                                              numbookingfee_minumumfare.Value=  objSer.FromValue.ToDecimal() ;

                                                if(objSer.AmountWise.ToBool())
                                                {
                                               numbookingfeevalue.Value=  objSer.ServiceChargeAmount.ToDecimal();
                                                }
                                                else
                                                {
                                                    numbookingfeevalue.Value=  objSer.ServiceChargePercent.ToDecimal();
                                                }

                                               chkbookingfee_onlyaccountjobs.Checked= objSer.IsAccount.ToBool();
                                            }
                                        }


                                    }
                                    catch
                                    {

                                    }

                                }
                           

                                foreach (var item in objMaster.Current.Gen_SysPolicy_CommissionPriceRanges)
                                {

                                    if (grdRangeWiseComm.Columns.Count == 0)
                                        FormatRangeWiseCommissionGrid();

                                    GridViewRowInfo grow = grdRangeWiseComm.Rows.AddNew();
                                    if (grow != null)
                                    {
                                        grow.Cells[COL_RANGEWISECOMMISSION.ID].Value = item.Id;
                                        grow.Cells[COL_RANGEWISECOMMISSION.POLICYID].Value = item.SysPolicyId;
                                        grow.Cells[COL_RANGEWISECOMMISSION.FROMPRICE].Value = item.FromPrice.ToDecimal();
                                        grow.Cells[COL_RANGEWISECOMMISSION.TOPRICE].Value = item.ToPrice.ToDecimal();
                                        grow.Cells[COL_RANGEWISECOMMISSION.CHARGESPERCENT].Value = item.CommissionValue.ToDecimal();



                                    }
                                }

                            


                            //


                                numTimeoutController.Value = obj.LogoutInActivityElapsedTime.ToDecimal();
                                numAcJobNotifyOn.Text = obj.AccJobsShowNotificationDay.ToStr().Trim();

                                var itemAc = numAcJobNotifyOn.Items.FirstOrDefault(c => c.Text == numAcJobNotifyOn.Text);
                                numAcJobNotifyOn.SelectedItem = itemAc;



                                chkAutoEmailControllerReport.Checked = obj.AutoEmailControllerReport.ToBool();
                                numPreBookingDays.Value = obj.HourControllerReport.ToInt();

                                ddlControllerDay.Text = obj.DayOfWeekControllerReport.ToStr().Trim();
                                var itemAc2 = ddlControllerDay.Items.FirstOrDefault(c => c.Text == ddlControllerDay.Text);
                                ddlControllerDay.SelectedItem = itemAc2;
                                


                                ddlCreditCardChrgsType.Text = obj.CreditCardChargesType.ToInt() == 1 ? "Amount" : "Percent";
                               var itemList= ddlCreditCardChrgsType.Items.FirstOrDefault(c=>c.Text==ddlCreditCardChrgsType.Text);

                               if (itemList != null)
                                   ddlCreditCardChrgsType.SelectedItem = itemList;
                               else
                                   ddlCreditCardChrgsType.SelectedIndex = 0;
                             

                            numCreditCardExtraCharge.Value = obj.CreditCardExtraCharges.ToDecimal();
                            chkIsRoundMileageFare.Checked = obj.RoundMileageFares.ToBool();

                            numDiscountPercentageReturn.Value = obj.DiscountForReturnedJourneyPercent.ToDecimal();
                            numDiscountWaitReturnPercent.Value = obj.DiscountForWRJourneyPercent.ToDecimal();

                            numDriverExpNoticeDays.Value = obj.DriverExpiryNoticeInDays.ToDecimal();
                            numShiftEarnHour.Value = obj.EarningLoginHours.ToDecimal();

                            txtConfirmationText.Text = objMaster.Current.Gen_SysPolicy_Configurations[0].ConfirmationSMSText.ToStr().Trim();

                            int? mapType = obj.MapType.ToInt();
                            if (mapType == Enums.MAP_TYPE.GOOGLEMAPS)
                                optGoogleMap.ToggleState = ToggleState.On;
                            else if (mapType == Enums.MAP_TYPE.MAPPOINT)
                                optMapPoint.ToggleState = ToggleState.On;
                            else if (mapType == Enums.MAP_TYPE.NONE)
                                optNoneMap.ToggleState = ToggleState.On;

                       
                            lstCounty.Items.Clear();

                            foreach (string countyStr in obj.CountyString.ToStr().Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
                            {
                                lstCounty.Items.Add(new RadListDataItem  { Font=new Font("Tahoma",10, FontStyle.Bold), Text=countyStr, Value=countyStr});                                 
                            }


                            LstPriorityPostcodes.Items.Clear();

                            foreach (string it in obj.PriorityPostCodes.ToStr().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                LstPriorityPostcodes.Items.Add(new RadListDataItem { Font = new Font("Tahoma", 10, FontStyle.Bold), Text = it, Value = it });
                            }                         



                            ddlTodayBookingDays.Text = obj.DaysInTodayBooking.ToInt().ToStr();

                            if (AppVars.objPolicyConfiguration.EnableGhostJob.ToBool())
                            {
                                radLabel53.Visible = false;
                                radLabel54.Visible = false;
                                ddlTodayBookingDays.Visible = false;

                            }

                            chkEnableQuotation.Checked = obj.EnableQuotation.ToBool();

                            chkShowBookingOtherCharges.Checked = obj.EnableBookingOtherCharges.ToBool();


                            // AutoDespatch Tab
                            chkEnableAutoDespatch.Checked = obj.EnableAutoDespatch.ToBool();

                            int autoDespatchType = obj.AutoDespatchType.ToInt();

                            if (autoDespatchType == 1)
                                optAutoDespRule1.ToggleState = ToggleState.On;

                            else if (autoDespatchType == 2)
                                optAutoDespRule2.ToggleState = ToggleState.On;

                            else if (autoDespatchType == 3)
                                optAutoDespRule3.ToggleState = ToggleState.On;

                            else if (autoDespatchType == 4)
                                optAutoDespRule4.ToggleState = ToggleState.On;

                            else if (autoDespatchType == 5)
                                optAutoDespRule5.ToggleState = ToggleState.On;



                            NumAutoDespAutoRetry.Value = obj.NoResponseRetry.ToInt();
                            numAutoDespElapsedTime.Value = obj.AutoDespatchElapsedTime.ToInt();
                            numAutoDespLeastEstTime.Value = obj.LeastEstimedTimeToClear.ToInt();
                            numAutoDespBeforeDue.Value = obj.AutoDespatchMinsBeforeDue.ToInt();
                            numAutoDespDrvRadius.Value = obj.AutoDespatchNearestDrvRadius.ToDecimal();
                            numAutoDespLongestWaitingMins.Value = obj.AutoDespatchLongestWaitingMins.ToDecimal();
                            numAutoDespFOJRadius.Value = obj.AutoDespatchFOJRadius.ToDecimal();

                            numBidPriceLimit.Value = obj.AutoDespatchASAPMins.ToDecimal();
                            chkAutoDespatchAllJobs.Checked = obj.AutoDespatchAllJobs.ToBool();
                            chkautoDespPriorityToDriver.Checked = obj.AutoDespatchPriorityForAllocatedDrv.ToBool();
                            chkAutoDespPriorityAccJobs.Checked = obj.AutoDespatchPriorityForAccJobs.ToBool();
                            chkAutoDespatchMode.Checked = obj.AutoCloseDrvPopup.ToBool();

                            // Bidding Tab
                            chkEnableBidding.Checked = obj.EnableBidding.ToBool();

                            int biddingType = obj.BiddingType.ToInt();

                            if (biddingType == 1)
                                optBidRule1.ToggleState = ToggleState.On;

                            else if (biddingType == 2)
                                optBidRule2.ToggleState = ToggleState.On;

                            else if (biddingType == 3)
                                optBidRule3.ToggleState = ToggleState.On;


                            chkEnableBiddingchauffers.Checked = objMaster.Current.Gen_SysPolicy_Configurations[0].EnableBiddingForChauffers.ToBool();

                            numBidElapsedTime.Value = obj.BiddingElapsedTime.ToInt();


                            numBidPriceLimitFrom.Value = objMaster.Current.Gen_SysPolicy_Configurations[0].FromBidPriceLimit.ToDecimal();
                            numBidPriceLimitTill.Value = objMaster.Current.Gen_SysPolicy_Configurations[0].TillBidPriceLimit.ToDecimal();
                            numShowBiddingJobInHours.Value = objMaster.Current.Gen_SysPolicy_Configurations[0].ShowJobInBiddingByHours.ToInt();

                            ddlPDARankType.SelectedIndex = objMaster.Current.Gen_SysPolicy_Configurations[0].DriverRankType.ToInt() == -1 ? 0 : objMaster.Current.Gen_SysPolicy_Configurations[0].DriverRankType.ToInt();

                            ddlAutoDespatchDrvPriorityCategory.SelectedValue = objMaster.Current.Gen_SysPolicy_Configurations[0].AutoDespatchDriverCategoryPriority.ToInt();
                            //


                            numAutoLogoutDrvMins.Value = obj.AutoLogoutInActiveDrvMins.ToDecimal();


                            chkAutoCalculateFares.Checked = obj.AutoCalculateFares.ToBool();
                            chkAutoPlotting.Checked = obj.EnableFixedPlotting.ToBool();
                            numGridRowSize.Value = obj.GridRowSize.ToDecimal()<20 ? 20: obj.GridRowSize.ToDecimal();

                            chkBookingAlert.Checked = obj.BookingAlertExpiryNoticeInMins.ToInt() == 1 ? true : false;

                            chkWebBookingAuthorization.Checked = obj.EnableOnlineBookingAuthorization.ToBool();
                            chkMobileBookingAuthorization.Checked = obj.EnableMobileBookingAuthorization.ToBool();
                            txtEmailMobileBooking.Text = obj.MobileBookingEmailVerification.ToStr().Trim();

                            chkBookingLimits.Checked = objMaster.Current.Gen_SysPolicy_Configurations[0].EnableReplaceNoToZoneSuggesstion.ToBool();
                            chkZeroRoundFigures.Checked= objMaster.Current.Gen_SysPolicy_Configurations[0].AutoShowBookingNearestDrv.ToBool();


                            numsinbin.Value = obj.SinBinTimer.ToInt();
                            numMapRefreshInterval.Value = obj.MapRefreshInterval.ToDecimal();
                            txtListenerIP.Text = obj.ListenerIP.ToStr().Trim();
                            chkTCPConnection.Checked = obj.IsListenAll.ToBool();


                            chkShowAreaPlots.Checked = obj.ShowAreaWithPlots.ToBool();

                            txtBaseAddress.Text = obj.BaseAddress.ToStr();

                            chkEnableAdvanceBookingText.Checked = obj.EnableAdvanceBookingSMSConfirmation.ToBool();
                            numAfterBookingSMSMinutes.Value = obj.AdvanceBookingSMSConfirmationMins.ToInt();

                            chkEnableArrivalText.Checked = obj.EnableArrivalBookingText.ToBool();

                            chkEnablePassengerText.Checked = obj.EnablePassengerText.ToBool();

                            txtAdvBookingTxt.Text = obj.AdvanceBookingSMSText.ToStr().Trim();
                            txtArrivalText.Text = obj.ArrivalBookingText.ToStr().Trim();
                            txtArrivalAirportText.Text = obj.ArrivalAirportBookingText.ToStr().Trim();


                            txtDriverText.Text = obj.DespatchTextForDriver.ToStr().Trim();
                            txtCustomerText.Text = obj.DespatchTextForCustomer.ToStr().Trim();
                            txtPDAText.Text = obj.DespatchTextForPDA.ToStr().Trim();
                            txtWebBookingText.Text = obj.WebBookingText.ToStr().Trim();
                            txtNoPickupText.Text = obj.SMSNoPickup.ToStr().Trim();
                            txtCancelText.Text = obj.SMSCancelJob.ToStr().Trim();


                            numRoundJourneyMiles.Value = obj.RoundJourneyMiles.ToDecimal();

                            numRecentJobDays.Value = obj.RecentBookingDays.ToInt();
                            numPagingSize.Value = obj.ListingPagingSize.ToInt();                          
                       
                            numDriverRent.Value = obj.DriverMonthlyRent.ToDecimal();
                            chkRentForProcJobs.Checked = obj.RentForProcessedJobs.ToBool();
                            dtpRentFromDate.Value = obj.RentFromDateTime.ToDateTimeorNull();
                            dtpRentToDate.Value = obj.RentToDateTime.ToDateTimeorNull();
                            dtpDriverSuspDayTime.Value = obj.DriverSuspensionDateTime.ToDateTimeorNull();


                            chkEnablePOI.Checked = obj.EnablePOI.ToBool();

                            chkApplyStartRateMiles.Checked = obj.EnableApplyStartRateWithInMiles.ToBool();
                            SetStartRateMileToggle(chkApplyStartRateMiles.Checked ? ToggleState.On : ToggleState.Off);
                            numApplyWithInMiles.Value = obj.ApplyStartRateWithInMiles.ToDecimal();
                            RountUpToToggle(chkIsRoundMileageFare.Checked ? ToggleState.On : ToggleState.Off);
                            numRoundUpTo.Value = obj.RoundUpTo.ToDecimal();
                            numExpiredPDAJobsInHours.Value = obj.ExpiredPDAJobHours.ToDecimal();
                            chkEnablePeakOffPeakTimeFare.Checked = obj.EnablePeakOffPeakFares.ToBool();
                            chkEnableZoneWisePricing.Checked = obj.EnableZoneWiseFares.ToBool();
                            chkPreferredMileagePrice.Checked = obj.PreferredMileageFares.ToBool();

                          //  ddlPeakOffPeakType.SelectedIndex = obj.OffPeakMinFares.ToInt() <= 1 ? 0 : 1;
                           // ddlPeakOffPeakType.SelectedIndex = obj.OffPeakMinFares.ToInt() <= 1 ? 0 : 1;
                            
                            // Web Booking Settings
                            chkEnableWebBooking.Checked = obj.EnableWebBooking.ToBool();
                            chkWebAccounts.Checked = obj.HasWebAccounts.ToBool();
                            numFetchWebBookingInMins.Value = obj.WebBookingFetchInMins.ToInt();


                            numPaymentTerms.Value =obj.ControllerWelcomeMessage.ToStr().Trim()==string.Empty ? 0: obj.ControllerWelcomeMessage.ToDecimal();
                            //if (!string.IsNullOrEmpty(obj.WebBookingBackgroundColor.ToStr().Trim()))
                            //{
                            //    Color clr = Color.FromArgb((obj.WebBookingBackgroundColor.ToStr().Trim().ToInt()));

                            //    (txtWebBookingBgColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                            //    txtWebBookingBgColor.Tag = clr.ToArgb();
                            //}


                            //if (!string.IsNullOrEmpty(obj.VIPBookingBackgroundColor.ToStr().Trim()))
                            //{
                            //    Color clr = Color.FromArgb((obj.VIPBookingBackgroundColor.ToStr().Trim().ToInt()));

                            //    (txtVIPBookingBgColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                            //    txtVIPBookingBgColor.Tag = clr.ToArgb();
                            //}

                            // PDA Settings
                            chkEnablePDA.Checked = obj.EnablePDA.ToBool();




                            numJobOfferTimeOut.Value = obj.PDAJobOfferRequestTimeout.ToDecimal();
                        //    numPDAFetchInSecs.Value = obj.PDADataFetchInterval.ToDecimal();

                            chkEnableOfflineDistance.Checked = obj.EnableOfflineDistance.ToBool();
                            chkShortestDistance.Checked= objMaster.Current.Gen_SysPolicy_Configurations[0].PreferredShortestDistance.ToBool();

                            numPlotJobExpiry1.Value = obj.PlotsJobExpiryValue1.ToDecimal();
                            numPlotJobExpiry2.Value = obj.PlotsJobExpiryValue2.ToDecimal();

                            chkEnableOnBoardDrivers.Checked = obj.EnableOnBoardDrivers.ToBool();
                            chkDespatchPDASms.Checked = obj.EnablePdaDespatchSms.ToBool();


                            chkFOJ.Checked = obj.EnableFOJ.ToBool();
                            chkFOJ.Enabled = (obj.PDAVersion.ToDecimal() >= 1.7m);
                            numfojlimit.Value = obj.FOJLimit.ToInt();

                            chkEnableAutoBookingExpiry.Checked = objMaster.Current.Gen_SysPolicy_Configurations[0].AutoBookingDueAlert.ToBool();
                            numAvgBookingExpMins.Value = objMaster.Current.Gen_SysPolicy_Configurations[0].AvgFirstMileExpiryMins.ToDecimal();

                            numminuteseachexpirymile.Value = objMaster.Current.Gen_SysPolicy_Configurations[0].EachAdditionalMileExpiryMins.ToDecimal();

                            txtRemoteIPs.Text = obj.RemoteIPs.ToStr().Trim();



                        }

                        
                      
                     


                        // SMS Settings

                        Gen_SysPolicy_SMSConfiguration objSMS;
                        if (objMaster.Current.Gen_SysPolicy_SMSConfigurations.Count == 1)
                        {
                            objSMS = objMaster.Current.Gen_SysPolicy_SMSConfigurations[0];

                            chkUseSMSAandB.Checked = objMaster.Current.Gen_SysPolicy_Configurations[0].UseMultipleSMSGateways.ToBool();

                            int SMSAccountType = objSMS.SMSAccountType.ToInt();
                            if (SMSAccountType == Enums.SMSACCOUNT_TYPE.CLICKATELL)
                            {
                                optClickATell.ToggleState = ToggleState.On;
                            }
                            else if ( SMSAccountType == Enums.SMSACCOUNT_TYPE.MODEMSMS)
                            {
                                optModemSMS.ToggleState = ToggleState.On;

                               
                                    if (objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].SMSModemType == "tmobile")
                                        optTMobile.ToggleState = ToggleState.On;
                                    else
                                        optO2.ToggleState= ToggleState.On;


                                    ddlModemSMSPortName.Text = objMaster.Current.Gen_SysPolicy_SMSConfigurations[0].ModemSMSPortName.ToStr().Trim();

                                int userId = AppVars.LoginObj.LuserId.ToInt();     

                                

                            }

                            chkCanReadSMS.Checked = objSMS.CanReadSMS.ToBool();

                          
                            txtClickSMSUserName.Text = objSMS.ClickSMSUserName.ToStr().Trim();
                            txtClickSMSPassword.Text = objSMS.ClickSMSPassword.ToStr().Trim();
                            txtClickSMSAPIKey.Text = objSMS.ClickSMSApiKey.ToStr().Trim();
                            txtClickSMSSenderName.Text = objSMS.ClickSMSSenderName.ToStr().Trim();


                            
                            // For SMS Template
                            grdSMSTemplets.Rows.Clear();

                            GridViewRowInfo objRowTemplet = null;
                            foreach (var item in objMaster.Current.Gen_SysPolicy_SMSTemplets)
                            {
                                objRowTemplet = grdSMSTemplets.Rows.AddNew();
                                objRowTemplet.Cells[COL_SMSTEMPLET.ID].Value = item.Id;
                                objRowTemplet.Cells[COL_SMSTEMPLET.POLICYID].Value = item.SysPolicyId;
                                objRowTemplet.Cells[COL_SMSTEMPLET.Tempplet].Value = item.Templet;

                            }
                            ClearTemplet();
                            grdSMSTemplets.CurrentRow = null;
                        }

                        //

                        DisplayLocationExpirySettings();

                        DisplayLocalBookingExpirySettings();

                        grdPolicyDocuments.Rows.Clear();

                        GridViewRowInfo row;
                        foreach (var item in objMaster.Current.Gen_SysPolicy_DocumentNumberSetups)
                        {
                            row = grdPolicyDocuments.Rows.AddNew();
                            row.Cells[COL_SYSPOLICYDOCS.ID].Value = item.Id;
                            row.Cells[COL_SYSPOLICYDOCS.POLICYID].Value = item.SysPolicyId;
                            row.Cells[COL_SYSPOLICYDOCS.DOCUMENTID].Value = item.DocumentId;
                            row.Cells[COL_SYSPOLICYDOCS.SUBCOMPANYID].Value = item.SubcompanyId;


                            row.Cells[COL_SYSPOLICYDOCS.PREFIX].Value = item.Prefix;
                            row.Cells[COL_SYSPOLICYDOCS.STARTNUMBER].Value = item.StartNumber;
                            row.Cells[COL_SYSPOLICYDOCS.LASTNUMBER].Value = item.LastNumber;

                            row.Cells[COL_SYSPOLICYDOCS.AUTOINCREMENT].Value = item.AutoIncrement.ToBool();

                        }


                        grdPaymentGateway.Rows.Clear();

                        foreach (var item in objMaster.Current.Gen_SysPolicy_PaymentDetails)
                        {
                            row = grdPaymentGateway.Rows.AddNew();
                            row.Cells[COL_GATEWAY.ID].Value = item.Id;
                            row.Cells[COL_GATEWAY.POLICYID].Value = item.SysPolicyId;
                            row.Cells[COL_GATEWAY.GATEWAYTYPEID].Value = item.PaymentGatewayId.ToInt();
                            row.Cells[COL_GATEWAY.GATEWAYTYPENAME].Value = item.PaymentGateway.DefaultIfEmpty().Name;
                            row.Cells[COL_GATEWAY.MERCHANTID].Value = item.MerchantID.ToStr();
                            row.Cells[COL_GATEWAY.MERCHANTPASSWORD].Value = item.MerchantPassword.ToStr();

                        
                            row.Cells[COL_GATEWAY.ACCOUNTYPEID].Value = item.ApiCertificate.ToStr();
                            row.Cells[COL_GATEWAY.PAYPALID].Value = item.PaypalID.ToStr();
                            row.Cells[COL_GATEWAY.APPLICATIONID].Value = item.ApplicationId.ToStr();

                            row.Cells[COL_GATEWAY.EnableMobileIntegration].Value = item.EnableMobileIntegration.ToBool();

                        }

                        ClearGatewayDetails();


                        if (grdPaymentGateway.Rows.Count(c=>c.Cells[COL_GATEWAY.EnableMobileIntegration].Value.ToBool())>0)
                        {
                            pnlPaymentPDASettings.Visible = true;
                            var objPaymentSettings = General.GetObject<Gen_PaymentColumnSetting>(c => c.Id > 0);
                            if (objPaymentSettings != null)
                            {
                                chkShowFares.Checked = objPaymentSettings.ShowFares.ToBool();
                                chkEditFares.Checked = objPaymentSettings.EditFares.ToBool();
                                chkShowParking.Checked = objPaymentSettings.ShowParking.ToBool();
                                chkEditParking.Checked = objPaymentSettings.EditParking.ToBool();
                                chkShowWaiting.Checked = objPaymentSettings.ShowWaiting.ToBool();
                                chkEditWaiting.Checked = objPaymentSettings.EditParking.ToBool();
                            }
                        }
                        else
                        {
                            pnlPaymentPDASettings.Visible = false;
                        }

                   
                    }
                    // For Caller Id
                    CallerIdType_Configuration objCallerId;

                    //     eCallerIdType callerIdType= eCallerIdType.Analog;
                    if (objMaster.Current.CallerIdType_Configurations.Count == 1)
                    {
                        objCallerId = objMaster.Current.CallerIdType_Configurations[0];

                        // Analog
                        chkAnalog.Checked = objCallerId.IsAnalog.ToBool();

                        if (objCallerId.AnalogCLIType == Enums.ANALOG_CLITYPE.ETHERNET)
                        {
                            optAnalogEthernet.ToggleState = ToggleState.On;

                        }
                        else
                        {
                            optAnalogSerialPort.ToggleState = ToggleState.On;

                            cmbBaudRate.Text = objCallerId.SerialBaudRate.ToStr();
                            cmbDataBits.Text = objCallerId.SerialDataBits.ToStr();
                            cmbParity.Text = objCallerId.SerialParityBits.ToStr();
                            cmbPortName.Text = objCallerId.SerialComPort.ToStr();
                            cmbStopBits.Text = objCallerId.SerialStopBits.ToStr();
                        }


                        // Digital 
                        chkDigital.Checked = objCallerId.IsDigital.ToBool();

           




                        chkAnswerCTE.Checked = objMaster.Current.Gen_SysPolicy_Configurations[0].ShowCLIPopupOnAnswer.ToBool();

                        if (objCallerId.DigitalCLIType.ToInt() == 1)
                        {

                            opt_Digital_CTE.ToggleState = ToggleState.On;
                            tab_DigitalTypes.SelectedTab = tab_page_cte;



                            IEnumerable<string> listitemsArr = objCallerId.TapiDriverName.ToStr().SplitTo<string>(new char[] { ',' });

                            foreach (ListViewItem item in lstLines.Items)
                            {
                                if (listitemsArr.Contains(item.Text))
                                {
                                    item.Checked = true;

                                }
                                else
                                {

                                    item.Checked = false;

                                }
                            }


                            var exceptList = listitemsArr.Except(lstLines.Items.OfType<ListViewItem>().Select(c => c.Text).ToArray<string>());

                            foreach (string item in exceptList)
                            {
                                lstLines.Items.Add(new ListViewItem { Text = item, Checked = true, Font = new Font("Tahoma", 11, FontStyle.Italic) });

                            }

                        }
                        else if(objCallerId.DigitalCLIType.ToInt()==2)
                        {
                            opt_Digital_CDR.ToggleState = ToggleState.On;
                            tab_DigitalTypes.SelectedTab = tab_page_cdr;
                            grdCLIExtensions.Visible = true;

                        }
                        else if (objCallerId.DigitalCLIType.ToInt() == 3)
                        {
                            opt_digital_cti.ToggleState = ToggleState.On;

                            IEnumerable<string> listitemsArr = objCallerId.TapiDriverName.ToStr().SplitTo<string>(new char[] { ',' });

                            foreach (ListViewItem item in lstCTILines.Items)
                            {
                                if (listitemsArr.Contains(item.Text))
                                {
                                    item.Checked = true;

                                }
                                else
                                {

                                    item.Checked = false;

                                }
                            }


                            var exceptList = listitemsArr.Except(lstCTILines.Items.OfType<ListViewItem>().Select(c => c.Text).ToArray<string>());

                            foreach (string item in exceptList)
                            {
                                lstCTILines.Items.Add(new ListViewItem { Text = item, Checked = true, Font = new Font("Tahoma", 11, FontStyle.Italic) });

                            }

                            grdCLIExtensions.Visible = true;
                        }

                        txt_cdr_ip.Text = objCallerId.CdrIP.ToStr().Trim();
                        txt_cdr_username.Text = objCallerId.CdrUserName.ToStr().Trim();
                        txt_cdr_password.Text = objCallerId.CdrPassword.ToStr().Trim();


                        // VOIP
                        chkSip.Checked = objCallerId.IsVOIP.ToBool();

                     //  chkRecvFromFone.Checked = objCallerId.ReceiveVOIPCallFromPhone.ToBool();




                        if (objCallerId.VOIPCLIType.ToInt() == 1)
                            opt_voipsip.ToggleState = ToggleState.On;
                        else if (objCallerId.VOIPCLIType.ToInt() == 2)
                            opt_voipasterisk.ToggleState = ToggleState.On;
                        else if (objCallerId.VOIPCLIType.ToInt() == 3)
                            opt_voipbt.ToggleState = ToggleState.On;
                        else if (objCallerId.VOIPCLIType.ToInt() == 4)
                            opt_emerald.ToggleState = ToggleState.On;



                        if (opt_voipsip.ToggleState == ToggleState.On || opt_voipbt.ToggleState== ToggleState.On)
                        {

                            grdVOIP.Rows.Clear();
                            GridViewRowInfo voipRow = null;
                            foreach (var item in objMaster.Current.CallerIdVOIP_Configurations)
                            {
                                voipRow = grdVOIP.Rows.AddNew();
                                voipRow.Cells[COL_VOIP.ID].Value = item.Id;
                                voipRow.Cells[COL_VOIP.POLICYID].Value = item.SysPolicyId;
                                voipRow.Cells[COL_VOIP.UserId].Value = item.UserId;
                                voipRow.Cells[COL_VOIP.User].Value = item.UserName.ToStr();
                                voipRow.Cells[COL_VOIP.Password].Value = item.Password.ToStr();
                                voipRow.Cells[COL_VOIP.Account].Value = item.AccountId.ToStr();
                                voipRow.Cells[COL_VOIP.Host].Value = item.Host.ToStr();

                                voipRow.Cells[COL_VOIP.Proxy].Value = item.ProxyAddress.ToStr();
                                voipRow.Cells[COL_VOIP.Port].Value = item.Port.ToStr();

                            }


                            grdVOIP.CurrentRow = null;
                        }
                        else if (opt_voipasterisk.ToggleState == ToggleState.On || opt_voipbt.ToggleState == ToggleState.On || opt_emerald.ToggleState== ToggleState.On)
                        {

                            grdCLIExtensions.Visible = true;
            

                            var objSip = objMaster.Current.CallerIdVOIP_Configurations.FirstOrDefault();

                            if (objSip != null)
                            {
                                if (objCallerId.VOIPCLIType.ToInt() == 2)
                                {

                                    SIP_txtHost.Text = objSip.Host.ToStr();
                                    SIP_txtPassword.Text = objSip.Password.ToStr();
                                    SIP_txtUserName.Text = objSip.AccountId.ToStr();
                                    txtVoip_AsteriskPortName.Text = objSip.Port.ToStr();
                                    SIP_BTProxy.Text = objSip.ProxyAddress.ToStr();
                                }
                                else if (objCallerId.VOIPCLIType.ToInt() == 3)
                                {

                                    SIP_txtHost.Text = objSip.Host.ToStr();
                                    SIP_txtPassword.Text = objSip.Password.ToStr();
                                    SIP_txtUserName.Text = objSip.UserName.ToStr();
                                    txtVoip_AsteriskPortName.Text = objSip.AccountId.ToStr();
                                    SIP_BTProxy.Text = objSip.ProxyAddress.ToStr();
                                    SIP_BT_port.Text = objSip.Port.ToStr();
                                                                      

                                }
                                else if (objCallerId.VOIPCLIType.ToInt() == 4)
                                {

                                    SIP_txtHost.Text = objSip.Host.ToStr();
                                    SIP_txtPassword.Text = objSip.Password.ToStr();
                                    SIP_txtUserName.Text = objSip.UserName.ToStr();
                                    txtVoip_AsteriskPortName.Text = objSip.Port.ToStr();
                                    SIP_BTProxy.Text = objSip.ProxyAddress.ToStr();
                                    SIP_BT_port.Text = objSip.Port.ToStr();


                                }
                            }


                            

                        }

                        // File CLI

                        chkFileCli.Checked = objCallerId.IsFileCLI.ToBool();
                        if (chkFileCli.Checked)
                            grdCLIExtensions.Visible = true;


                        txtFileCliDirPath.Text = objCallerId.FileCliDirPath.ToStr().Trim();
                        DisplayExtensions();

                       
                    }

                    IsDisplayingRecord = false;

                }

                pic_CompanyLogo.ShowActionPanel = true;

                if (File.Exists(Application.StartupPath + "\\ManageKeys.exe") == false)
                {
                    btnManageETAKeys.Visible = false;

                }

            }
            catch (Exception ex)
            {
                IsDisplayingRecord = false;
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void grdPolicyDocuments_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {

            try
            {

                int? lastNumber = grdPolicyDocuments.CurrentRow.Cells[COL_SYSPOLICYDOCS.LASTNUMBER].Value.ToIntorNull();
                if (lastNumber != null && grdPolicyDocuments.CurrentColumn.Name == COL_SYSPOLICYDOCS.STARTNUMBER)
                    e.Cancel = true;

            }
            catch (Exception ex)
            {


            }
        }

      

        private void chkEnableAdvanceBookingText_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            numAfterBookingSMSMinutes.Enabled = args.ToggleState == ToggleState.On;
            if (args.ToggleState == ToggleState.Off)
            {
                numAfterBookingSMSMinutes.Value = 0;
            }

        }

      

    

        private void optClickATell_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                tab_SMS.SelectedTab = tab_SMS.TabPages[0];

            }
        }

        private void optModemSMS_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                tab_SMS.SelectedTab = tab_SMS.TabPages[1];

            }
        }

        

        private void btnPickWebBgColor_Click(object sender, EventArgs e)
        {
          //  SetColor(txtWebBookingBgColor);
        }

        private void SetColor(RadTextBox txt)
        {
            ColorDialog colorDialog1 = new ColorDialog();
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

        private void btnClearWebBgColor_Click(object sender, EventArgs e)
        {
         //   ClearColor(txtWebBookingBgColor);
        }

        private void grdFaresSettings_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column != null && e.Column.Name == COL_FARES.AMOUNTWISE)
            {

                bool IsAmountWise = e.Value.ToBool();

                if (IsAmountWise)
                {
                    e.Row.Cells[COL_FARES.PERCENTAGE].Value = null;

                }
                else
                {

                    e.Row.Cells[COL_FARES.AMOUNT].Value = null;
                }

            }
        }

        private void grdSurchargeRates_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column != null)
            {
                bool IsAmountWise = e.Row.Cells[COL_FARES.AMOUNTWISE].Value.ToBool();

                if (IsAmountWise && e.Column.Name == COL_FARES.PERCENTAGE)
                {
                    e.Cancel = true;

                }

                if (IsAmountWise == false && e.Column.Name == COL_FARES.AMOUNT)
                {
                    e.Cancel = true;

                }
                


            }
        }

        private void grdFaresSettings_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column != null)
            {
                bool IsAmountWise=   e.Row.Cells[COL_FARES.AMOUNTWISE].Value.ToBool();

                if (IsAmountWise && e.Column.Name == COL_FARES.PERCENTAGE)
                {
                    e.Cancel = true;

                }

                if (IsAmountWise==false && e.Column.Name == COL_FARES.AMOUNT)
                {
                    e.Cancel = true;

                }



            }
        }

       

        private void chkApplyStartRateMiles_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            SetStartRateMileToggle(args.NewValue);
        }


        private void SetStartRateMileToggle(ToggleState toggle)
        {

            if (toggle == ToggleState.On)
            {
                numApplyWithInMiles.Value = 0;
                numApplyWithInMiles.Enabled = false;  
            }
            else
            {
                numApplyWithInMiles.Enabled = true;
            }

        }

        private void chkAllCounty_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
          //  SetAllCounty(args.ToggleState);
        }


        private void SetAllCounty(ToggleState toggle)
        {

            //if (toggle == ToggleState.On)
            //{
            //    foreach (RadListDataItem item in lstCounty.Items)
            //    {
            //        item.Selected = false;

            //    }

            //    lstCounty.Enabled = false;
            //}
            //else
            //{
            //    lstCounty.Enabled = true;
            //}


        }

        private void btnGetKeyDriver_Click(object sender, EventArgs e)
        {
            try

            {

                string tagValue = ddlTagDriver.SelectedValue.ToStr().Trim();

                if(string.IsNullOrEmpty(tagValue))return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtDriverText.Paste();

                Clipboard.Clear();
         
            }
            catch
            {


            }



        }

     

        private void btnTagCustomer_Click(object sender, EventArgs e)
        {

            try
            {
                string tagValue = ddlTagCustomer.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtCustomerText.Paste();

                Clipboard.Clear();
            }
            catch
            {


            }
        }


        #region Serial Port Analog

        #region Local Variables

        // The main control for communicating through the RS-232 port
        private SerialPort comport = new SerialPort();

        // Various colors for logging info
        private Color[] LogMsgTypeColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };


        private Settings settings = Settings.Default;
        #endregion


        private void InitializeControlValues()
        {
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            //cmbParity.Text = settings.Parity.ToString();
            //cmbStopBits.Text = settings.StopBits.ToString();
            //cmbDataBits.Text = settings.DataBits.ToString();
            //cmbParity.Text = settings.Parity.ToString();
            //cmbBaudRate.Text = settings.BaudRate.ToString();
         //   CurrentDataMode = settings.DataMode;

            RefreshComPortList();

         //   chkClearOnOpen.Checked = settings.ClearOnOpen;
         //   chkClearWithDTR.Checked = settings.ClearWithDTR;

            // If it is still avalible, select the last com port used
            //if (cmbPortName.Items.Contains(settings.PortName)) cmbPortName.Text = settings.PortName;
            //else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            //else
            //{
            //    MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
        }


        private void RefreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, comport.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }

        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }


        #endregion

        private void optAnalogEthernet_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            pnlAnalogSerialPort.Enabled = args.ToggleState == ToggleState.Off;
        }

        private void radGroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void ddlGateway_SelectedValueChanged(object sender, EventArgs e)
        {
            int gatewayId = ddlGateway.SelectedValue.ToInt();

            if (gatewayId == 0 || gatewayId == 1 || gatewayId == 6)
            {
                pnlOtherGateway.Visible = true;
                pnlPaypal.Visible = false;
                pnlConnectPay.Visible = false;

                ShowPaymentProcessUrl(false);

            }
            else if (gatewayId == 2 || gatewayId==3 || gatewayId==4 )
            {
                pnlOtherGateway.Visible = false;
                pnlPaypal.Visible = true;
                pnlConnectPay.Visible = false;

                if (gatewayId == 4)
                {
                    ShowPaymentProcessUrl(true);
                }
                else
                {
                    ShowPaymentProcessUrl(false);
                }

            }
            else if (gatewayId == 5)
            {
                pnlOtherGateway.Visible = false;
                pnlPaypal.Visible = false;

                pnlConnectPay.Visible = true;

            }
            else if (gatewayId == 7)
            {
                pnlOtherGateway.Visible = false;
                pnlPaypal.Visible = true;

                pnlConnectPay.Visible = false;

            }
         

        }


        private void ShowPaymentProcessUrl(bool show)
        {

            lblPaymentProcessUrl.Visible = show;
            txtPaymentProcessUrl.Visible = show;


            if (!show)
                txtPaymentProcessUrl.Text = string.Empty;

        }

        private void btnAddGateway_Click(object sender, EventArgs e)
        {
            try
            {
                string error = string.Empty;
                int? ppAccountType = null;
                int gatewayId = ddlGateway.SelectedValue.ToInt();
                string gatewayName = ddlGateway.Text.Trim();

                // For Payment Sense
                string merchantId = txtMerchantID.Text.Trim();
                string merchantPass = txtMerchantPass.Text.Trim();



                // For Paypal
                 string paypalid = txtPaypalId.Text.Trim();


                // Barchlay
                 string paymentProcessUrl = txtPaymentProcessUrl.Text.Trim();



                if (gatewayId == 1 || gatewayId==6)
                {
                    if (string.IsNullOrEmpty(merchantId))
                    {
                        error += "Required :  Merchant Id" + Environment.NewLine;
                    }

                    if (string.IsNullOrEmpty(merchantPass))
                    {
                        error += "Required :  Merchant Password" + Environment.NewLine;
                    }
                    ppAccountType = null;

                    if (gatewayId == 6)
                    {
                        paypalid = txtInstId.Text;


                    }
                }
                else if (gatewayId == 2 || gatewayId==3 || gatewayId==4)
                {
                    if (string.IsNullOrEmpty(paypalid))
                    {
                        error += "Required Gateway Id" + Environment.NewLine;
                    }


                    if (gatewayId == 4 && string.IsNullOrEmpty(paymentProcessUrl))
                    {
                        error += "Required Url" + Environment.NewLine;

                    }

                }
                else if (gatewayId ==5)
                {
                    merchantId = txt_connectpay_username.Text.ToStr().Trim();
                    merchantPass = txt_ConnectPay_Password.Text.ToStr().Trim();
                    paypalid = txt_ConnectPay_ProfileId.Text.ToStr().Trim();
                    paymentProcessUrl = txt_connectpay_accountno.Text.ToStr().Trim();

                    if (string.IsNullOrEmpty(paypalid))
                    {
                        error += "Required Gateway Id" + Environment.NewLine;
                    }


                    if (gatewayId == 4 && string.IsNullOrEmpty(paymentProcessUrl))
                    {
                        error += "Required Url" + Environment.NewLine;

                    }

                }
                else if (gatewayId == 7) // STRIPE PAYMENT
                {

                    if (paypalid.ToStr().Trim().Length == 0)
                        error += "Required : GatewayID/Signature";

                }
           
                else
                {
                    ENUtils.ShowMessage("Required : Gateway");
                }


                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }



                GridViewRowInfo row = grdPaymentGateway.CurrentRow;
                if (row == null)
                {
                    row = grdPaymentGateway.Rows.AddNew();
                }
                

                row.Cells[COL_GATEWAY.GATEWAYTYPEID].Value = gatewayId;
                row.Cells[COL_GATEWAY.GATEWAYTYPENAME].Value = gatewayName;
                row.Cells[COL_GATEWAY.MERCHANTID].Value = merchantId;
                row.Cells[COL_GATEWAY.MERCHANTPASSWORD].Value = merchantPass;
              
             
                row.Cells[COL_GATEWAY.ACCOUNTYPEID].Value = ppAccountType;
                row.Cells[COL_GATEWAY.PAYPALID].Value = paypalid;
                row.Cells[COL_GATEWAY.APPLICATIONID].Value = paymentProcessUrl;
                row.Cells[COL_GATEWAY.EnableMobileIntegration].Value = false;

                ClearGatewayDetails();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }


        private void ClearGatewayDetails()
        {
            string empty = string.Empty;

       
            txtMerchantPass.Text = empty;
            txtMerchantID.Text = empty;

            txtPaymentProcessUrl.Text = string.Empty;
            txtPaypalId.Text = string.Empty;

            grdPaymentGateway.CurrentRow = null;


        }

        private void grdPaymentGateway_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdPaymentGateway.CurrentRow != null && grdPaymentGateway.CurrentRow is GridViewDataRowInfo)
                {
                    FillPaymentGatewayDropDown();

                    ddlGateway.SelectedValue = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.GATEWAYTYPEID].Value.ToInt();
                    txtMerchantID.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.MERCHANTID].Value.ToStr();
                    txtMerchantPass.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.MERCHANTPASSWORD].Value.ToStr();

                    txtPaymentProcessUrl.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.APPLICATIONID].Value.ToStr();
                    txtPaypalId.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.PAYPALID].Value.ToStr();
                    txtInstId.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.PAYPALID].Value.ToStr();
                    string accType = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.ACCOUNTYPEID].Value.ToStr();
                    if (accType == "1")
                    {
                        optPP_Sandbox.ToggleState = ToggleState.On;

                    }
                    else
                        optPP_Live.ToggleState = ToggleState.On;
                    
                      ShowPaymentProcessUrl(ddlGateway.SelectedValue.ToInt() == 4);

                      txt_connectpay_username.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.MERCHANTID].Value.ToStr();
                      txt_ConnectPay_Password.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.MERCHANTPASSWORD].Value.ToStr();

                      txt_ConnectPay_ProfileId.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.PAYPALID].Value.ToStr();
                      txt_connectpay_accountno.Text = grdPaymentGateway.CurrentRow.Cells[COL_GATEWAY.APPLICATIONID].Value.ToStr();

                  

                    if(ddlGateway.SelectedValue.ToInt()==6)
                    {
                        radLabel76.Text = "Secret";
                        lblMercPass.Text = "Token";
                        this.radLabel94.Text = "Judo ID :";

                    }
                    else
                    {
                        radLabel76.Text = "MerchantID :";
                        lblMercPass.Text = "Password :";

                        this.radLabel94.Text = "Gateway ID :";
                    }
                

                }


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnClearGateway_Click(object sender, EventArgs e)
        {
            ClearGatewayDetails();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
           // SetColor(txtVIPBookingBgColor);
        }

        private void btnClearVIPBgColor_Click(object sender, EventArgs e)
        {
          //  ClearColor(txtVIPBookingBgColor);
        }

        private void btnClearVoipFields_Click(object sender, EventArgs e)
        {
            ClearVOIP_Fields();
        }


        private void ClearVOIP_Fields()
        {
            SIP_ddlUsers.SelectedValue = null;
            SIP_txtHost.Text = string.Empty;
            SIP_txtPassword.Text = string.Empty;
            SIP_txtUserName.Text = string.Empty;

           // SIP_BT_port.Text = string.Empty;

            SIP_BTProxy.Text = string.Empty;


            grdVOIP.CurrentRow = null;
            SIP_txtUserName.Focus();



        }

        private void btnAddVOIPFields_Click(object sender, EventArgs e)
        {
            try
            {
                string error = string.Empty;


             

                    int? userId = SIP_ddlUsers.SelectedValue.ToIntorNull();

                    string userName = SIP_ddlUsers.Text.Trim();
                    string accountID = SIP_txtUserName.Text.Trim();
                    string Password = SIP_txtPassword.Text.Trim();
                    string host = SIP_txtHost.Text.Trim();


                    if (opt_voipsip.ToggleState == ToggleState.On)
                    {

                        if (userId == null)
                        {
                            error += "Required :  User" + Environment.NewLine;
                        }

                        if (string.IsNullOrEmpty(accountID))
                        {
                            error += "Required :  Account ID" + Environment.NewLine;
                        }


                        if (string.IsNullOrEmpty(Password))
                        {
                            error += "Required :  Password" + Environment.NewLine;
                        }



                        if (string.IsNullOrEmpty(host))
                        {
                            error += "Required :  host" + Environment.NewLine;
                        }



                        if (!string.IsNullOrEmpty(error))
                        {
                            ENUtils.ShowMessage(error);
                            return;
                        }


                        int rowIndex = grdVOIP.CurrentRow != null ? grdVOIP.CurrentRow.Index : -1;


                        GridViewRowInfo row = grdVOIP.CurrentRow;
                        if (row == null || row is GridViewNewRowInfo)
                        {
                            row = grdVOIP.Rows.AddNew();
                        }

                        row.Cells[COL_VOIP.UserId].Value = userId;
                        row.Cells[COL_VOIP.User].Value = userName;
                        row.Cells[COL_VOIP.Account].Value = accountID;
                        row.Cells[COL_VOIP.Password].Value = Password;
                        row.Cells[COL_VOIP.Host].Value = host;
                    }
                    else if (opt_voipbt.ToggleState == ToggleState.On)
                    {
                        string port = SIP_BT_port.Text.ToStr();
                        string accountId = txtVoip_AsteriskPortName.Text.ToStr();
                        string BtuserName = SIP_txtUserName.Text.ToStr();
                        string proxy = SIP_BTProxy.Text.ToStr(); ;


                        //objMaster.Current.CallerIdVOIP_Configurations[0].Port = SIP_BT_port.Text.ToStr();
                        //objMaster.Current.CallerIdVOIP_Configurations[0].AccountId = txtVoip_AsteriskPortName.Text.ToStr();
                        //objMaster.Current.CallerIdVOIP_Configurations[0].UserName = SIP_txtUserName.Text.ToStr();
                        //objMaster.Current.CallerIdVOIP_Configurations[0].Host = SIP_txtHost.Text.ToStr();
                        //objMaster.Current.CallerIdVOIP_Configurations[0].Password = SIP_txtPassword.Text.ToStr();
                        //objMaster.Current.CallerIdVOIP_Configurations[0].ProxyAddress = SIP_BTProxy.Text.ToStr();


                       

                        if (string.IsNullOrEmpty(accountID))
                        {
                            error += "Required :  Account ID" + Environment.NewLine;
                        }


                        if (string.IsNullOrEmpty(Password))
                        {
                            error += "Required :  Password" + Environment.NewLine;
                        }



                        if (string.IsNullOrEmpty(host))
                        {
                            error += "Required :  host" + Environment.NewLine;
                        }



                        if (!string.IsNullOrEmpty(error))
                        {
                            ENUtils.ShowMessage(error);
                            return;
                        }


                        int rowIndex = grdVOIP.CurrentRow != null ? grdVOIP.CurrentRow.Index : -1;


                        GridViewRowInfo row = grdVOIP.CurrentRow;
                        if (row == null || row is GridViewNewRowInfo)
                        {
                            row = grdVOIP.Rows.AddNew();
                        }

                        row.Cells[COL_VOIP.Port].Value = port;
                        row.Cells[COL_VOIP.Proxy].Value = proxy;

                        row.Cells[COL_VOIP.User].Value = BtuserName;
                        row.Cells[COL_VOIP.Account].Value = accountId;
                        row.Cells[COL_VOIP.Password].Value = Password;
                        row.Cells[COL_VOIP.Host].Value = host;




                    }
                    ClearVOIP_Fields();
          
              
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void grdVOIP_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {

                if (e.Row != null && e.Row is GridViewDataRowInfo)
                {
                    SIP_ddlUsers.SelectedValue = e.Row.Cells[COL_VOIP.UserId].Value.ToInt();
                    SIP_txtHost.Text = e.Row.Cells[COL_VOIP.Host].Value.ToStr();
                    SIP_txtPassword.Text = e.Row.Cells[COL_VOIP.Password].Value.ToStr();
                    SIP_txtUserName.Text = e.Row.Cells[COL_VOIP.Account].Value.ToStr();
                    SIP_BT_port.Text = e.Row.Cells[COL_VOIP.Port].Value.ToStr();
                    SIP_BTProxy.Text = e.Row.Cells[COL_VOIP.Proxy].Value.ToStr();
                    txtVoip_AsteriskPortName.Text = e.Row.Cells[COL_VOIP.Account].Value.ToStr();


                    //objMaster.Current.CallerIdVOIP_Configurations[0].Port = SIP_BT_port.Text.ToStr();
                    //objMaster.Current.CallerIdVOIP_Configurations[0].AccountId = txtVoip_AsteriskPortName.Text.ToStr();
                    //objMaster.Current.CallerIdVOIP_Configurations[0].UserName = SIP_txtUserName.Text.ToStr();
                    //objMaster.Current.CallerIdVOIP_Configurations[0].Host = SIP_txtHost.Text.ToStr();
                    //objMaster.Current.CallerIdVOIP_Configurations[0].Password = SIP_txtPassword.Text.ToStr();
                    //objMaster.Current.CallerIdVOIP_Configurations[0].ProxyAddress = SIP_BTProxy.Text.ToStr();
                   
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnTagPDAText_Click(object sender, EventArgs e)
        {
            try
            {

                string tagValue = ddlTagPDA.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtPDAText.Paste();

                Clipboard.Clear();

            }
            catch
            {


            }

        }

        private void btnTagAdvance_Click(object sender, EventArgs e)
        {
            try
            {

                string tagValue = ddlTagAdvance.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtAdvBookingTxt.Paste();

                Clipboard.Clear();

            }
            catch
            {


            }
        }


        private void chkShowCdrPassword_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
           txt_cdr_password.PasswordChar=args.ToggleState== ToggleState.On?'\0':'*';
        }

        private void opt_Digital_CTE_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            tab_DigitalTypes.SelectedTab = args.ToggleState == ToggleState.On ? tab_page_cte : tab_page_cdr;


        }

        private void btnAddSMSTemplet_Click(object sender, EventArgs e)
        {
            try
            {
                string error = string.Empty;
                string Templet = TxtSMSTemplet.Text.ToStr();



                if (Templet == "")
                {
                    error += "Required :  Template";
                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }


                GridViewRowInfo row = null;

                if (grdSMSTemplets.CurrentRow != null)
                {
                    row = grdSMSTemplets.CurrentRow;
                }

                else
                {
                    row = grdSMSTemplets.Rows.AddNew();
                }

                row.Cells[COL_SMSTEMPLET.Tempplet].Value = Templet;

                ClearTemplet();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void ClearTemplet()
        {
            TxtSMSTemplet.Text = string.Empty;
            grdSMSTemplets.CurrentRow = null;
        }

        private void FormatSMSTempletGrid()
        {
            grdSMSTemplets.CellDoubleClick += new GridViewCellEventHandler(grdSMSTemplets_CellDoubleClick);
            grdSMSTemplets.CommandCellClick += new CommandCellClickEventHandler(grdSMSTemplets_CommandCellClick);

            grdSMSTemplets.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SMSTEMPLET.ID;
            grdSMSTemplets.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SMSTEMPLET.POLICYID;
            grdSMSTemplets.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Templates Message";
            col.Width = 250;
            col.ReadOnly = true;
            col.Name = COL_SMSTEMPLET.Tempplet;
            grdSMSTemplets.Columns.Add(col);



            grdSMSTemplets.AddDeleteColumn();


            grdSMSTemplets.AllowAddNewRow = false;
            grdSMSTemplets.ShowGroupPanel = false;
            grdSMSTemplets.ShowRowHeaderColumn = false;

            grdSMSTemplets.CurrentRow = null;
        }
        void grdSMSTemplets_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (e.Row is GridViewDataRowInfo)
                {
                    TxtSMSTemplet.Text = e.Row.Cells[COL_SMSTEMPLET.Tempplet].Value.ToStr();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }


        }
        void grdSMSTemplets_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {

                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnClearSMSTemplet_Click(object sender, EventArgs e)
        {
            ClearTemplet();
        }

        private void btnTagArrive_Click(object sender, EventArgs e)
        {
            try
            {

                string tagValue = ddlTagArrive.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

               

                    if (lastFocusedOn)
                    {
                        txtArrivalAirportText.Paste();
                    }
                    else
                    {

                        txtArrivalText.Paste();
                    }

                Clipboard.Clear();

            }
            catch
            {


            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string prefArea = txtPrefArea.Text.ToUpper().Trim();


                if (lstCounty.Items.Count(c => c.Value.ToStr() == prefArea) > 0)
                {
                    MessageBox.Show("Area already exist");
                }
                else
                {
                    RadListDataItem item = new RadListDataItem();
                    item.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    item.Text = prefArea;
                    item.Value = prefArea;

                    lstCounty.Items.Add(item);
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void btnDeletePrefArea_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstCounty.SelectedItem != null)
                {
                    lstCounty.Items.Remove(lstCounty.SelectedItem);

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            try
            {

                if (radPageView1.SelectedPage == Pg_Company)
                {
                    this.ShowSaveAndCloseButton = false;
                    this.ShowSaveButton = false;

                    btnSaveCompany.Visible = true;
                    btnSaveAndCloseCompany.Visible = true;

                    if (objMainCompany == null)
                    {
                        objMainCompany = General.GetObject<Gen_MainCompany>(c => c.CompanyName != null);
                        if (objMainCompany != null)
                        {
                            DisplayMainCompany();

                        }

             

                    }
                }
                else if (radPageView1.SelectedPage == pg_fares)
                {
                    if (pnlFares == null)
                    {
                        InitializeFaresPanel();
                        DisplayFaresSettings();
                       // InitializeCompanyFaresPanel();
                       // DisplayCompanyFaresSettings();
                    }
                    
                }
                else if (radPageView1.SelectedPage == pg_SurchargesRates)
                {
                    if (grdSurchargeRates == null)
                    {
                        InitializeSurchargesRateGrid();

                        DisplaySurchargeRates();
                    }
                }
                else if (radPageView1.SelectedPage == pg_email)
                {
                    ChangeEmailSettings = true;

                }

                else
                {
                    this.ShowSaveAndCloseButton = true;
                    this.ShowSaveButton = true;

                    btnSaveCompany.Visible = false;
                    btnSaveAndCloseCompany.Visible = false;

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        private void DisplaySurchargeRates()
        {
            grdSurchargeRates.Rows.Clear();

            foreach (var item in objMaster.Current.Gen_SysPolicy_SurchargeRates)
            {
                GridViewRowInfo row = grdSurchargeRates.Rows.AddNew();
                row.Cells[COL_SURCHARGERATES.ID].Value = item.Id;
                row.Cells[COL_SURCHARGERATES.POLICYID].Value = item.SysPolicyId;
                row.Cells[COL_SURCHARGERATES.POSTCODE].Value = item.PostCode;
                row.Cells[COL_SURCHARGERATES.PERCENTAGE].Value = item.Percentage;

                row.Cells[COL_SURCHARGERATES.AMOUNTWISE].Value = item.IsAmountWise.ToBool();
                row.Cells[COL_SURCHARGERATES.AMOUNT].Value = item.Amount.ToDecimal();


            }

        }


        private void DisplayLocationExpirySettings()
        {

            foreach (var item in objMaster.Current.Gen_Syspolicy_LocationExpiries.Where(c=>c.LocationId!=null))
            {
                GridViewRowInfo row = grdAirportExpiry.Rows.FirstOrDefault(a => a.Cells[COL_AIRPORTEXPIRY.LOCATIONID].Value.ToInt() == item.LocationId.ToInt());
                if (row != null)
                {
                    row.Cells[COL_AIRPORTEXPIRY.ID].Value = item.Id;
                    row.Cells[COL_AIRPORTEXPIRY.POLICYID].Value = item.SysPolicyId;
                    row.Cells[COL_AIRPORTEXPIRY.LOCATIONID].Value = item.LocationId.ToIntorNull();
                    row.Cells[COL_AIRPORTEXPIRY.LOCATIONNAME].Value = item.Gen_Location.DefaultIfEmpty().LocationName.ToStr();
                    row.Cells[COL_AIRPORTEXPIRY.EXPIRY].Value = item.ExpiryMins.ToInt();
                


                }
            }

        }



        private void DisplayLocalBookingExpirySettings()
        {
            grdBookingExpiry.Rows.Clear();



            foreach (var item in objMaster.Current.Gen_Syspolicy_LocationExpiries.Where(c=>c.LocationId==null))
            {
                GridViewRowInfo row = grdBookingExpiry.Rows.AddNew();
             
                    row.Cells[COL_AIRPORTEXPIRY.ID].Value = item.Id;
                    row.Cells[COL_AIRPORTEXPIRY.POLICYID].Value = item.SysPolicyId;
            
                    row.Cells[COL_AIRPORTEXPIRY.LOCATIONNAME].Value = item.LocationPostCode.ToStr();
                    row.Cells[COL_AIRPORTEXPIRY.EXPIRY].Value = item.ExpiryMins.ToInt();



                
            }

        }


        private void DisplayFaresSettings()
        {

            foreach (var item in objMaster.Current.Gen_SysPolicy_FaresSettings.Where(c=>c.IsCompanyWise==null || c.IsCompanyWise==false))
            {
                GridViewRowInfo row = grdFaresSettings.Rows.FirstOrDefault(a => a.Cells[COL_FARES.VEHICLETYPEID].Value.ToInt() == item.VehicleTypeId);
                if (row != null)
                {
                    row.Cells[COL_FARES.ID].Value = item.Id;
                    row.Cells[COL_FARES.POLICYID].Value = item.SysPolicyId;
                    row.Cells[COL_FARES.AMOUNTWISE].Value = item.IsAmountWise.ToBool();
                    row.Cells[COL_FARES.AMOUNT].Value = item.Amount.ToDecimal();
                    row.Cells[COL_FARES.PERCENTAGE].Value = item.Percentage.ToInt();
                    row.Cells[COL_FARES.OPERATOR].Value = item.Operator.ToStr();

                }
            }

        }

        private void DisplayCompanyFaresSettings()
        {

            foreach (var item in objMaster.Current.Gen_SysPolicy_FaresSettings.Where(c=>c.IsCompanyWise==true))
            {
                GridViewRowInfo row = grdCompanyFaresSettings.Rows.FirstOrDefault(a => a.Cells[COL_FARES.VEHICLETYPEID].Value.ToInt() == item.VehicleTypeId);
                if (row != null)
                {
                    row.Cells[COL_FARES.ID].Value = item.Id;
                    row.Cells[COL_FARES.POLICYID].Value = item.SysPolicyId;
                    row.Cells[COL_FARES.AMOUNTWISE].Value = item.IsAmountWise.ToBool();
                    row.Cells[COL_FARES.AMOUNT].Value = item.Amount.ToDecimal();
                    row.Cells[COL_FARES.PERCENTAGE].Value = item.Percentage.ToInt();
                    row.Cells[COL_FARES.OPERATOR].Value = item.Operator.ToStr();

                }
            }

        }

        Gen_MainCompany objMainCompany = null;
        private void btnSaveCompany_Click(object sender, EventArgs e)
        {
            SaveMainCompany();
        }

        private void btnSaveAndCloseCompany_Click(object sender, EventArgs e)
        {
            if (SaveMainCompany())
            {
                this.Close();
            }
        }


        private bool SaveMainCompany()
        {
            try
            {  
                
                
                string mapIcon = ddlMapIcon.SelectedValue.ToStr().Trim();
                if (string.IsNullOrEmpty(mapIcon))
                    mapIcon = ddlMapIcon.Items[0].Value.ToStr();


              



                if (pic_CompanyLogo.GetImage() != null)
                {

                    System.Data.Linq.Binary img = new System.Data.Linq.Binary(General.imageToByteArray(pic_CompanyLogo.PictureBoxElement.Image));


                    new TaxiDataContext().stp_SaveMainCompany(txtName.Text.Trim(), txtAddress.Text.Trim(), txtTelNo.Text.Trim(), "", txtCompanyEmail.Text.Trim(),
                                             txtCompanyFax.Text.Trim(), txtCompanyEmergencyNo.Text.Trim(), txtCompanyWebsite.Text.Trim(),
                                             txtSortCode.Text.Trim(), txtAccountNo.Text.Trim(), txtAccountTitle.Text.Trim(), txtBank.Text.Trim(), txtCompanyNumber.Text.Trim()
                                             , txtVATNumber.Text.Trim(), img, txtBgColor.Tag.ToInt(), mapIcon,txtIBAN.Text.Trim(),txtBLC.Text.Trim(),txtSmtpHost.Text.Trim(),txtPort.Text.Trim().ToStr(),txtUserName.Text.Trim(),txtPassword.Text.Trim(),chkIsSecureConn.Checked);
                }
                else
                {

                    new TaxiDataContext().stp_SaveMainCompany(txtName.Text.Trim(), txtAddress.Text.Trim(), txtTelNo.Text.Trim(), "", txtCompanyEmail.Text.Trim(),
                                           txtCompanyFax.Text.Trim(), txtCompanyEmergencyNo.Text.Trim(), txtCompanyWebsite.Text.Trim(),
                                           txtSortCode.Text.Trim(), txtAccountNo.Text.Trim(), txtAccountTitle.Text.Trim(), txtBank.Text.Trim(), txtCompanyNumber.Text.Trim()
                                           , txtVATNumber.Text.Trim(), null, txtBgColor.Tag.ToInt(), mapIcon, txtIBAN.Text.Trim(), txtBLC.Text.Trim(), txtSmtpHost.Text.Trim(), txtPort.Text.Trim().ToStr(), txtUserName.Text.Trim(), txtPassword.Text.Trim(), chkIsSecureConn.Checked);

                }


                return true;

            }
            catch (Exception ex)
            {
                
                ENUtils.ShowMessage(ex.Message);

                
                return false;
            }

        }


        private void DisplayMainCompany()
        {
            try
            {
                if (objMainCompany == null) return;




                txtName.Text = objMainCompany.CompanyName.ToStr();
                txtCompanyEmail.Text = objMainCompany.EmailAddress.ToStr();
                txtCompanyFax.Text = objMainCompany.Fax.ToStr();
                txtTelNo.Text = objMainCompany.TelephoneNo.ToStr();
                txtCompanyEmergencyNo.Text = objMainCompany.EmergencyNo.ToStr();
                txtCompanyWebsite.Text = objMainCompany.WebsiteUrl.ToStr();
                txtAddress.Text = objMainCompany.Address.ToStr();

                if (objMainCompany.CompanyLogo != null)
                {
                    pic_CompanyLogo.SetImage(objMainCompany.CompanyLogo.ToArray());

                }

                txtSortCode.Text = objMainCompany.SortCode.ToStr().Trim();
                txtAccountNo.Text = objMainCompany.AccountNo.ToStr().Trim();
                txtAccountTitle.Text = objMainCompany.AccountTitle.ToStr().Trim();
                txtBank.Text = objMainCompany.BankName.ToStr().Trim();
                txtCompanyNumber.Text = objMainCompany.CompanyNumber.ToStr().Trim();
                txtVATNumber.Text = objMainCompany.CompanyVatNumber.ToStr().Trim();
                txtIBAN.Text = objMainCompany.IbanNumber.ToStr().Trim();
                txtBLC.Text = objMainCompany.BlcNumber.ToStr().Trim();



                if (objMainCompany.BackgroundColor != 0)
                {
                    Color clr = Color.FromArgb(objMainCompany.BackgroundColor.ToInt());
                    txtBgColor.BackColor = clr;
                    txtBgColor.Tag = clr.ToArgb();
                }


                ddlMapIcon.SelectedValue = objMainCompany.MapIcon.ToStr().Trim();


            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
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



        // Adil 28/5/13
        private void SetColor(TextBox txt)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {

                txt.BackColor = colorDialog1.Color;
                txt.Tag = colorDialog1.Color.ToArgb();
            }

        }
        private void ClearColor(TextBox txt)
        {

            txt.BackColor = Color.White;
            txt.Tag = null;


        }

        private void btnTagWebBookig_Click(object sender, EventArgs e)
        {
            try
            {

                string tagValue = ddlTagWebBooking.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtWebBookingText.Paste();

                Clipboard.Clear();

            }
            catch
            {


            }

        }

        private void chkEnableAutoDespatch_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleAutoDespatch(args.ToggleState);
        }


        private void ToggleAutoDespatch(ToggleState toggle)
        {
           
                pnlAutoDespatch.Enabled =toggle == ToggleState.On? true:false;

                //if (optAutoDespRule1.ToggleState == ToggleState.Off && optAutoDespRule2.ToggleState == ToggleState.Off
                //     && optAutoDespRule3.ToggleState == ToggleState.Off && optAutoDespRule4.ToggleState == ToggleState.Off)
                //    optAutoDespRule5.ToggleState = ToggleState.On;               


           



        }

        private void frmSysPolicy_Load(object sender, EventArgs e)
        {

        }

        private void btnTagEmail_Click(object sender, EventArgs e)
        {
            try
            {

                string tagValue = ddlTagEmail.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtEmailMobileBooking.Paste();

                Clipboard.Clear();

            }
            catch
            {


            }

        }

        private void opt_voipsip_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
          
                
                    grdVOIP.Visible = true;
                    lblVoipUserandPort.Text = "User :";
                    SIP_ddlUsers.BringToFront();
                    SIP_ddlUsers.Visible = true;
                    btnAddVOIPFields.Visible = true;
                    btnAddVOIPFields.Visible = true;
                    lblVoipAccountId.Text = "Account ID :";
                 //   SIP_BT_port.Visible = false;
                    SIP_BTProxy.Visible = false;
                    SIP_BTlblProxy.Visible = false;
               
        }


        private void opt_emerald_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            grdVOIP.Visible = false;
            lblVoipUserandPort.Text = "Port :";


            radLabel135.Visible = false;
            SIP_BT_port.Visible = false;
            lblVoipUserandPort.Visible = true;
            txtVoip_AsteriskPortName.Visible = true;

            lblVoipAccountId.Text = "User Name :";

            SIP_ddlUsers.Visible = false;
            SIP_ddlUsers.SendToBack();

            btnAddVOIPFields.Visible = false;
            btnAddVOIPFields.Visible = false;


            //    SIP_BT_port.Visible = false;
            SIP_BTProxy.Visible = false;
            SIP_BTlblProxy.Visible = false;




            if (args.ToggleState == ToggleState.On)
            {

                lblRecordingToken.Visible = true;
                txtRecordingToken.Visible = true;


            }
            else
            {
                lblRecordingToken.Visible = false;
                txtRecordingToken.Visible = false;
                //   txtRecordingToken.Text = string.Empty;
            }
        }
    

        private void opt_voipasterisk_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

              
                    grdVOIP.Visible = false;
                    lblVoipUserandPort.Text = "Port :";
                    lblVoipAccountId.Text = "User Name :";

                    SIP_ddlUsers.SendToBack();

                    btnAddVOIPFields.Visible = false;
                    btnAddVOIPFields.Visible = false;

                    lblVoipUserandPort.Visible = true;
                    txtVoip_AsteriskPortName.Visible = true;


                    radLabel135.Visible = true;
                    SIP_BT_port.Visible = true;

                //    SIP_BT_port.Visible = false;
                    SIP_BTProxy.Visible = false;
                    SIP_BTlblProxy.Visible = false;


                    if (args.ToggleState == ToggleState.On)
                    {

                        lblRecordingToken.Visible = true;
                        txtRecordingToken.Visible = true;
                       

                    }
                    else
                    {
                        lblRecordingToken.Visible = false;
                        txtRecordingToken.Visible = false;
                     //   txtRecordingToken.Text = string.Empty;
                    }
        }

        private void opt_voipbt_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
       
                grdVOIP.Visible = true;
               
                lblVoipUserandPort.Text = "Auth ID :";
                lblVoipAccountId.Text = "User Name :";


                SIP_ddlUsers.SendToBack();


                btnAddVOIPFields.Visible = true;


                lblVoipUserandPort.Visible = true;
                txtVoip_AsteriskPortName.Visible = true;

              //  btnAddVOIPFields.Visible = false;
             //   btnAddVOIPFields.Visible = true;
           //     lblVoipAccountId.Text = "Account ID :";

                radLabel135.Visible = true;
                SIP_BT_port.Visible = true;
                SIP_BTProxy.Visible = true;
                SIP_BTlblProxy.Visible = true;
        }

        private void chkRangeWiseCommission_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                grdRangeWiseComm.Visible = true;
                lblRangeWiseCommission.Visible = true;
                numDriverCommission.Enabled = false;


                if (grdRangeWiseComm.Columns.Count == 0)
                {
                    FormatRangeWiseCommissionGrid();

                }

            }
            else
            {
                numDriverCommission.Enabled = true;
                grdRangeWiseComm.Visible = false;
                lblRangeWiseCommission.Visible = false;


            }
        }

        private void chkAirportWisePickup_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                numAirportPickupChrgs.Enabled = false;
                lblAirportPickupChrges.Visible = true;
                grdAirportPickupChrges.Visible = true;


                lblAirportDropOff.Visible = true;
                grdAirportDropOff.Visible = true;
                if (grdAirportPickupChrges.Columns.Count == 0)
                {
                    FormatAirportWisePickupGrid();

                }

            }
            else
            {
                numAirportPickupChrgs.Enabled = true;
                lblAirportPickupChrges.Visible = false;
                grdAirportPickupChrges.Visible = false;



            }
        }

        

        private void ddlCreditCardChrgsType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlCreditCardChrgsType.Text.ToStr() == "Amount")
            {
                numCreditCardExtraCharge.Maximum = 10000;
                numCreditCardExtraCharge.DecimalPlaces = 2;
            }
            else
            {
                numCreditCardExtraCharge.Maximum = 100;
                numCreditCardExtraCharge.DecimalPlaces = 0;

            }
        }

        private void chkEnableBidding_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleBidding(args.ToggleState);
        }



        private void ToggleBidding(ToggleState toggle)
        {
            pnlBidding.Enabled = toggle== ToggleState.On;
        }

        private void btnAllEnableBidding_Click(object sender, EventArgs e)
        {
            EnableBiddingAllDrivers(true);
        }

        private void btnAllDisableBidding_Click(object sender, EventArgs e)
        {
            EnableBiddingAllDrivers(false);
        }

        private void EnableBiddingAllDrivers(bool enable)
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    db.Fleet_Drivers.Where(c=>c.IsActive==true).ToList().ForEach(c => c.EnableBidding = enable);
                    db.SubmitChanges();

                }

            }
            catch (Exception ex)
            {

            }


        }

        private void btnDeadMileages_Click(object sender, EventArgs e)
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list=  db.Bookings.Where(c => c.BookingStatusId == Enums.BOOKINGSTATUS.WAITING && c.PickupDateTime.Value.Date >= DateTime.Now.Date && c.ZoneId!=null && c.DeadMileage==null);


                    foreach (var item in list)
                    {

                       

                       decimal  mile = General.CalculateDistanceFromBaseFull(item.FromAddress.ToStr());

                       item.DeadMileage = mile;

                        if (mile > 0 && mile < 1)
                        {
                            mile = 1;
                        }
                        else
                        {
                            mile = Math.Round(mile, 0);
                        }


                        item.ExtraMile = mile;


                        db.SubmitChanges();
                        
                    }


                }


            }
            catch (Exception ex)
            {


            }
        }

        private void btnAddPriorPostCode_Click(object sender, EventArgs e)
        {
            try
            {
                string prefArea = txtPriorPostCode.Text.ToUpper().Trim();


                if (LstPriorityPostcodes.Items.Count(c => c.Value.ToStr() == prefArea) > 0)
                {
                    MessageBox.Show("Postcode already exist");
                }
                else
                {
                    RadListDataItem item = new RadListDataItem();
                    item.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    item.Text = prefArea;
                    item.Value = prefArea;

                    LstPriorityPostcodes.Items.Add(item);
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void btnDeletePriorPostCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (LstPriorityPostcodes.SelectedItem != null)
                {
                    LstPriorityPostcodes.Items.Remove(LstPriorityPostcodes.SelectedItem);

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void chkIsRoundMileageFare_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            RountUpToToggle(args.NewValue);
        }
        private void RountUpToToggle(ToggleState toggle)
        {

            if (toggle == ToggleState.On)
            {
                numRoundUpTo.Enabled = false;
                numRoundUpTo.Value = 0;
                chkZeroRoundFigures.Enabled = false;
            }
            else
            {
                numRoundUpTo.Enabled = true;
                chkZeroRoundFigures.Enabled = true;
               
            }

        }

        private void chkFOJ_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                lblfojlimit.Visible = true;
                numfojlimit.Visible = true;

            }
            else
            {
                lblfojlimit.Visible = false;
                numfojlimit.Visible = false;

            }
        }

        private void chkEnablePeakOffPeakTimeFare_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == ToggleState.On)
            //{
            //    ddlPeakOffPeakType.Visible = true;

            //}
            //else
            //{
            //    ddlPeakOffPeakType.Visible = false;

            //}


            //if (ddlPeakOffPeakType.SelectedIndex == -1)
            //    ddlPeakOffPeakType.SelectedIndex = 0;

        }

        private void btnTagConfirmationCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string tagValue = ddlTagConfirmationCustomer.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);
                txtConfirmationText.Paste();
                Clipboard.Clear();

            }
            catch
            {


            }
        }

        private void optAutoDespRule_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetAutoDespatchRuleConfig();
        }


        private void SetAutoDespatchRuleConfig()
        {
            string bidString = string.Empty;


            pnlAutoDespAdvancedSettings.Visible = true;

            if (optBidRule1.ToggleState == ToggleState.On)
            {
                bidString = "and Fastest Finger Bidding Driver";
               
            }
            else if (optBidRule2.ToggleState == ToggleState.On)
            {
                bidString = "and Nearest Bidding Driver";

            }
            else if (optBidRule3.ToggleState == ToggleState.On)
            {
                bidString = "and Longest Waiting Bidding Driver";

            }

            if (optAutoDespRule1.ToggleState == ToggleState.On) // top standing driver only
            {


                txtAutoDespDescription.Text = "Rule 1 => Top Standing Driver in a Pickup Plot "+bidString;
                pnlAutoDespAdvancedSettings.Visible = false;
            }
            else if (optAutoDespRule2.ToggleState == ToggleState.On) // top standing with nearest and longest waiting driver only
            {
                txtAutoDespDescription.Text = "Rule 2 => Top Standing Driver in a Pickup Plot with (Nearest / Longest Waiting) Driver " + bidString;
                
            }
            else if (optAutoDespRule3.ToggleState == ToggleState.On)  // nearest driver only
            {

                txtAutoDespDescription.Text = "Rule 3 => (Nearest / Longest Waiting) Driver only " + bidString;

            }
            else if (optAutoDespRule4.ToggleState == ToggleState.On)  // nearest driver only
            {

                txtAutoDespDescription.Text = "Rule 4 => Longest Waiting in Job Plot and nearest driver " + bidString;

            }
            else if (optAutoDespRuleX.ToggleState == ToggleState.On)
            {

                txtAutoDespDescription.Text = "";

            }


        }

        private void pnlAutoDespatch_Enter(object sender, EventArgs e)
        {

        }

        private void btnManageETAKeys_Click(object sender, EventArgs e)
        {

            try
            {

                string connString = Application.StartupPath + "\\ManageKeys.exe";




                System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcesses().FirstOrDefault(c => c.ProcessName.Contains("ManageKeys"));

                if (proc != null)
                {
                    proc.Kill();
                    proc.CloseMainWindow();
                    proc.Close();
                }

                if (File.Exists(connString))
                {


                    string conn = Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToStr(), "softeuroconnskey", true).Replace(" ", "**");


                    System.Diagnostics.Process.Start(connString, conn);
                }
                else
                {
                    MessageBox.Show("File not found");

                }

            }
            catch(Exception ex)
            {


            }


           
        }

        private void btnAdditionalCharges_Click(object sender, EventArgs e)
        {
            SaveAdditionalCharges();


            if (grdAdditionalCharges.Rows.Count(c => c.Cells[COL_ExtraCharges.Check].Value.ToBool() == false) == grdAdditionalCharges.Rows.Count)
            {

                ENUtils.ShowMessage("Please choose the charges.");
            }
            else
            {
                frmCompanyCharges companyCharges = new frmCompanyCharges();
                companyCharges.Show();
            }
        }

        private void SaveAdditionalCharges()
        {
            using (TaxiDataContext db = new TaxiDataContext())
            {
                foreach (var item in grdAdditionalCharges.Rows)
                {
                    var obj = db.Gen_Charges.FirstOrDefault(c => c.Id == item.Cells[COL_ExtraCharges.Id].Value.ToInt());
                    obj.IsVisible = item.Cells[COL_ExtraCharges.Check].Value.ToBool();
                    db.SubmitChanges();

                }
            }

        }

        private void LoadAdditionalCharges()
        {
           
            var list = (from a in General.GetQueryable<Gen_Charge>(c => c.Id > 0)
                        select new
                        {
                            Id = a.Id,
                            ChargesName = a.ChargesName,
                            IsVisible = a.IsVisible
                        }).ToList();



            int cnt = list.Count;
            grdAdditionalCharges.RowCount = cnt;
            for (int i = 0; i < cnt; i++)
            {
                grdAdditionalCharges.Rows[i].Cells[COL_ExtraCharges.Id].Value = list[i].Id;
                grdAdditionalCharges.Rows[i].Cells[COL_ExtraCharges.ChargesName].Value = list[i].ChargesName;
                grdAdditionalCharges.Rows[i].Cells[COL_ExtraCharges.Check].Value = list[i].IsVisible;

            }
        }

        public struct COL_ExtraCharges
        {
            public static string Id = "Id";
            public static string ChargesName = "ChargesName";
            public static string Check = "Check";

        }

        private void FormatExtraChargesGrid()
        {
            grdAdditionalCharges.AllowAddNewRow = false;
            grdAdditionalCharges.ShowGroupPanel = false;
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_ExtraCharges.Check;
            cbcol.HeaderText = "";
            cbcol.Width = 30;
            grdAdditionalCharges.Columns.Add(cbcol);

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COL_ExtraCharges.Id;
            col.IsVisible = false;
            grdAdditionalCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_ExtraCharges.ChargesName;
            col.HeaderText = "Additional Charges";
            col.ReadOnly = true;
            col.Width = 120;
            grdAdditionalCharges.Columns.Add(col);
            grdAdditionalCharges.Visible = true;

        }

        private void chkUseSMSAandB_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                optClickATell.Enabled = false;
                optModemSMS.Enabled = false;
                optModemSMS.ToggleState = ToggleState.On;


            }
            else
            {

                optClickATell.Enabled = true;
                optModemSMS.Enabled = true;

            }

        }

        private void btnTagNoPickup_Click(object sender, EventArgs e)
        {
            try
            {

                string tagValue = ddlTagNoPickup.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtNoPickupText.Paste();

                Clipboard.Clear();

            }
            catch
            {


            }
        }

        private void btnTagCancel_Click(object sender, EventArgs e)
        {
            try
            {

                string tagValue = ddlTagCancelText.SelectedValue.ToStr().Trim();

                if (string.IsNullOrEmpty(tagValue)) return;

                Clipboard.Clear();
                Clipboard.SetText(tagValue);

                txtCancelText.Paste();

                Clipboard.Clear();

            }
            catch
            {


            }
        }

        private void chkEnableBookingFee_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                lblBookingFee.Visible = true;
                numbookingfeevalue.Visible = true;
                lblBookingfee_minimumfare.Visible = true;
                numbookingfee_minumumfare.Visible = true;
                ddlbookingfeetype.Visible = true;
                chkbookingfee_onlyaccountjobs.Visible = true;
            }
            else
            {
                lblBookingFee.Visible = false;
                numbookingfeevalue.Visible = false;
                numbookingfeevalue.Value = 0;
                lblBookingfee_minimumfare.Visible = false;
                numbookingfee_minumumfare.Visible = false;
                ddlbookingfeetype.Visible = false;
                chkbookingfee_onlyaccountjobs.Visible = true;

            }
        }

        private void ddlbookingfeetype_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlbookingfeetype.Text == "Amount")
                numbookingfeevalue.Maximum = 10000;
            else
                numbookingfeevalue.Maximum = 100;
           
        }
        void txtBaseAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtBaseAddress.Text.Trim().Length > 0 && txtBaseAddress.Text.Trim().Contains(" ") == false)
            {
                MessageBox.Show("Please enter correct BASE ADDRESS / PostCode");
                e.Cancel = true;

            }
        }
      
     

    }
}
