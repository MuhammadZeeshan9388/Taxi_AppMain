using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
//using ThePaymentGateway.PaymentSystem;
//using ThePaymentGateway.Common;
using Utils;
using System.Net;
using Taxi_Model;
using System.Web;
using Taxi_AppMain.Forms;
using Taxi_BLL;
using DAL;
using ThePaymentGateway.PaymentSystem;
using ThePaymentGateway.Common;
using System.IO;
using System.Reflection;
using MapApp;
using Taxi_AppMain.Classes;
using System.Web.Script.Serialization;
//using ThePaymentGateway.PaymentSystem;
//using ThePaymentGateway.Common;

namespace Taxi_AppMain
{
    public partial class frmBookingPayment2 : UI.SetupBase
    {

       public  string TransactionID = string.Empty;
        BookingBO objMaster;
        bool IsSave = false;
        int? ID = 0;
        frmBooking frm = new frmBooking();

        private List<Gen_SysPolicy_PaymentDetail> _ObjMerchantInfo;

        public List<Gen_SysPolicy_PaymentDetail> ObjMerchantInfo
        {
            get { return _ObjMerchantInfo; }
            set { _ObjMerchantInfo = value; }
        }

        private Booking_Payment _ObjPayment;

        public Booking_Payment ObjPayment
        {
            get { return _ObjPayment; }
            set { _ObjPayment = value; }
        }

        private enum FORM_MODE
        {
            PAYMENT_FORM,
            THREE_D_SECURE,
            RESULTS
        }

        private FORM_MODE m_fmFormMode = FORM_MODE.PAYMENT_FORM;
        protected internal string m_szPaREQ;
        protected internal string m_szTermURL;
        protected internal string m_szACSURL;
        protected internal string m_szCrossReference;


      //  public int PaymentFor = 1;


        private string _PickedEmail;

        public string PickedEmail
        {
            get { return _PickedEmail; }
            set { _PickedEmail = value; }
        }



        decimal onewayAmount;
        decimal returnAmount;

        public frmBookingPayment2(Booking_Payment obj, List<Gen_SysPolicy_PaymentDetail> listofMerchant, decimal amount, decimal returnBookingAmount,int journeyTypeId, string BookingNO, int? BookingID)
        {
            ID = BookingID;
            InitializeComponent();

            lblBookingID.Text = BookingNO;
            this.ObjPayment = obj;
            this.ObjMerchantInfo = listofMerchant;

            objMaster = new BookingBO();

            objMaster.GetByPrimaryKey(BookingID);


            this.SetProperties((INavigation)objMaster);




            if (objPaymentColumns == null)
            {
                objPaymentColumns = General.GetObject<Gen_PaymentColumnSetting>(c => c.Id != 0);

            }


            if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN && AppVars.objPolicyConfiguration.BookingFormType.ToInt()==2)
            {
                chkIncludeReturnBooking.Visible = true;


                chkIncludeReturnBooking.ToggleStateChanged += ChkIncludeReturnBooking_ToggleStateChanged;


            }

            if (objPaymentColumns != null)
            {

                if (objPaymentColumns.ShowTip.ToBool() == false)
                {
                    numTipAmount.Visible = false;
                    radLabel17.Visible = false;
                    radLabel12.Visible = false;
                }

                if (objPaymentColumns.ChargesType.ToInt() == Enums.PAYMENT_CHARGESTYPE.CHARGESTYPE1)
                {
                    amount = objMaster.Current.FareRate.ToDecimal() + objMaster.Current.MeetAndGreetCharges.ToDecimal() + objMaster.Current.CongtionCharges.ToDecimal();


                    if(journeyTypeId==Enums.JOURNEY_TYPES.RETURN)
                    {
                        if(objMaster.Current.BookingReturns.Count > 0)
                        {
                            returnAmount = objMaster.Current.BookingReturns[0].FareRate.ToDecimal() + objMaster.Current.BookingReturns[0].MeetAndGreetCharges.ToDecimal() + objMaster.Current.BookingReturns[0].CongtionCharges.ToDecimal();


                        }

                    }

                    if (objMaster.Current.CompanyId != null)
                    {
                        amount = objMaster.Current.CompanyPrice.ToDecimal() + objMaster.Current.WaitingCharges.ToDecimal() + objMaster.Current.ParkingCharges.ToDecimal();


                        if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                        {
                            if (objMaster.Current.BookingReturns.Count > 0)
                            {
                                returnAmount = objMaster.Current.BookingReturns[0].CompanyPrice.ToDecimal() + objMaster.Current.BookingReturns[0].MeetAndGreetCharges.ToDecimal() + objMaster.Current.BookingReturns[0].CongtionCharges.ToDecimal();


                            }

                        }
                    }

                }
                else if (objPaymentColumns.ChargesType.ToInt() == Enums.PAYMENT_CHARGESTYPE.CHARGESTYPE2)
                {

                   

                    amount = objMaster.Current.FareRate.ToDecimal() + objMaster.Current.MeetAndGreetCharges.ToDecimal() + objMaster.Current.CongtionCharges.ToDecimal();


                    if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                    {
                        if (objMaster.Current.BookingReturns.Count > 0)
                        {
                            returnAmount = objMaster.Current.BookingReturns[0].FareRate.ToDecimal() + objMaster.Current.BookingReturns[0].MeetAndGreetCharges.ToDecimal() + objMaster.Current.BookingReturns[0].CongtionCharges.ToDecimal();


                        }

                    }

                    if (objMaster.Current.CompanyId != null)
                    {
                        amount = objMaster.Current.CompanyPrice.ToDecimal() + objMaster.Current.WaitingCharges.ToDecimal() + objMaster.Current.ParkingCharges.ToDecimal();

                        if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                        {
                            if (objMaster.Current.BookingReturns.Count > 0)
                            {
                                returnAmount = objMaster.Current.BookingReturns[0].CompanyPrice.ToDecimal() + objMaster.Current.BookingReturns[0].WaitingCharges.ToDecimal() + objMaster.Current.BookingReturns[0].ParkingCharges.ToDecimal();


                            }

                        }
                    }
                }
                else if (objPaymentColumns.ChargesType.ToInt() == Enums.PAYMENT_CHARGESTYPE.CHARGESTYPE3)
                {

                    amount = objMaster.Current.FareRate.ToDecimal() + objMaster.Current.MeetAndGreetCharges.ToDecimal() + objMaster.Current.CongtionCharges.ToDecimal();

                    if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                    {
                        if (objMaster.Current.BookingReturns.Count > 0)
                        {
                            returnAmount = objMaster.Current.BookingReturns[0].FareRate.ToDecimal() + objMaster.Current.BookingReturns[0].MeetAndGreetCharges.ToDecimal() + objMaster.Current.BookingReturns[0].CongtionCharges.ToDecimal();


                        }

                    }
                }
                else if (objPaymentColumns.ChargesType.ToInt() == Enums.PAYMENT_CHARGESTYPE.CHARGESTYPE4)
                {
                    amount = objMaster.Current.FareRate.ToDecimal();
                    chkIncludeAirportCharges.Visible = true;
                    if (AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                    {

                        if (objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objMaster.Current.FromLocId != null)
                        {
                            numAirportPickUp.Value = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == objMaster.Current.FromLocId.ToInt()).DefaultIfEmpty().Charges.ToDecimal();
                        }

                        if (objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT && objMaster.Current.ToLocId != null)
                        {
                            numAirportDropOf.Value = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == objMaster.Current.ToLocId.ToInt()).DefaultIfEmpty().Charges.ToDecimal();
                        }
                    }

                }

            }


            txtAmount.Value = amount.ToDecimal();
            onewayAmount = txtAmount.Value;
            lblSurcharge.Visible = true;
            lblSurcharge2.Visible = true;
            lblSurcharge3.Visible = true;
            numSurchargeAmount.Visible = true;
            numSurchargePercent.Visible = true;


            SetChargesLimit();

            //   numSurchargePercent.Enabled = false;
            numSurchargePercent.Value = 0.00m;


            if (AppVars.objPolicyConfiguration.CreditCardChargesType.ToInt() == 1)
            {


                if (chargesLimit != null && chargesLimit.Count() >= 3 && txtAmount.Value > chargesLimit[0].ToDecimal())
                {


                    if (chargesLimit[1].ToInt() == 1)
                    {


                        numSurchargeAmount.Value += chargesLimit[2].ToDecimal();

                    }
                    else if (chargesLimit[1].ToInt() == 2)
                    {
                        //     numSurchargePercent.Enabled = true;
                        numSurchargePercent.Value = chargesLimit[2].ToDecimal();
                        CalculateTotal();
                    }
                }
                else
                {

                    numSurchargeAmount.Value += AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal();
                }




            }
            else if (AppVars.objPolicyConfiguration.CreditCardChargesType.ToInt() == 2)
            {
                if (chargesLimit != null && chargesLimit.Count() >= 3 && txtAmount.Value > chargesLimit[0].ToDecimal())
                {


                    if (chargesLimit[1].ToInt() == 1)
                    {
                        numSurchargeAmount.Value += chargesLimit[2].ToDecimal();
                    }
                    else if (chargesLimit[1].ToInt() == 2)
                    {
                        //    numSurchargePercent.Enabled = true;
                        numSurchargePercent.Value = chargesLimit[2].ToDecimal();
                        CalculateTotal();
                    }
                }
                else
                {
                    //   numSurchargePercent.Enabled = true;
                    numSurchargePercent.Value = AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal();
                    CalculateTotal();
                }


            }


            ComboFunctions.FillDispatchAvailablePaymentGatewayCombo(ddlGateway);
            ComboFunctions.FillCreditCardCombo(ddlCardType);

            ddlGateway.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlGateway_SelectedIndexChanged);
            ddlGateway.SelectedValueChanged += new EventHandler(ddlGateway_SelectedValueChanged);

            this.chkIncludeAirportCharges.ToggleStateChanged += new StateChangedEventHandler(chkIncludeAirportCharges_ToggleStateChanged);
            SetDefaultPaymentGateway();

            txtAmount.SpinElement.TextChanged += new EventHandler(SpinElement_TextChanged);
            numSurchargePercent.SpinElement.TextChanged += new EventHandler(SpinElement_TextChanged);
            numTipAmount.SpinElement.TextChanged += new EventHandler(SpinElement_TextChanged);
            CalculateTotal();

            this.Shown += new EventHandler(frmBookingPayment2_Shown);


            if (obj.PaymentGatewayId != null)
                ddlGateway.SelectedValue = obj.PaymentGatewayId;


        }

        private void ChkIncludeReturnBooking_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
             if(args.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On)
            {
                txtAmount.Value = onewayAmount + returnAmount;

            }

             else
            {
                txtAmount.Value = onewayAmount ;
            }
        }

        private void SetChargesLimit()
        {
            if (chargesLimit == null)
            {
                string charges = AppVars.objPolicyConfiguration.CreditCardExtraChargesLimits.ToStr().Trim();



                if (charges.Length > 0)
                {
                    chargesLimit = charges.Split(new char[] { '|' });
                }
            }

        }

        Gen_PaymentColumnSetting objPaymentColumns = null;

        void ddlGateway_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.ATLANTE_CONNECTPAY)
                {
                    pnlBilling.Visible = false;
                    this.Size = new Size(630, this.Size.Height);
                    lblNameOnCard.Visible = false;
                    txtNameOnCard.Visible = false;

                    lblStartDate.Visible = false;
                    dtpStartDate.Visible = false;

                }
                else
                {
                    pnlBilling.Visible = true;
                    this.Size = new Size(965, this.Size.Height);
                    lblNameOnCard.Visible = true;
                    txtNameOnCard.Visible = true;
                }


                if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.PAYPAL)
                {
                    //   ObjMerchantInfo[0].PaymentGatewayId = Enums.PAYMENT_GATEWAY.PAYPAL;
                    chkSendEmailtoCustomer.Checked = true;
                    chkSendEmailtoCustomer.Visible = true;

                    ddlCardType.Enabled = false;
                    txtCardNumber.Enabled = false;
                    dtpExpiryDate.Enabled = false;
                    dtpStartDate.Enabled = false;
                    txtCV2.Enabled = false;
                    radButton1.Enabled = false;

                    lblNameOnCard.Text = "Name :";
                }
                else
                {
                    chkSendEmailtoCustomer.Checked = true;
                    chkSendEmailtoCustomer.Visible = false;
                    lblNameOnCard.Text = "Name On Card :";


                    ddlCardType.Enabled = true;
                    txtCardNumber.Enabled = true;
                    dtpExpiryDate.Enabled = true;
                    dtpStartDate.Enabled = true;
                    txtCV2.Enabled = true;
                    radButton1.Enabled = true;

                    if (objMaster != null && objMaster.Current != null)
                    {
                        txtNameOnCard.Text = objMaster.Current.CustomerName.ToStr().Trim();
                    }


                    if (ddlGateway.SelectedValue.ToInt() == 10)
                    {
                        btnSave.Visible = false;

                    }

                    // ObjMerchantInfo[0].PaymentGatewayId = ddlGateway.SelectedValue.ToInt();

                }
            }
            catch (Exception ex)
            {


            }
        }

        void chkIncludeAirportCharges_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                if (chkIncludeAirportCharges.Checked)
                {
                    lblAirportDropOf.Visible = true;
                    lblAirportPickUp.Visible = true;
                    numAirportDropOf.Visible = true;
                    numAirportPickUp.Visible = true;
                    radLabel1.Visible = true;
                    radLabel5.Visible = true;
                    numTotalCharges.Value += numAirportPickUp.Value.ToDecimal();
                    numTotalCharges.Value += numAirportDropOf.Value.ToDecimal();
                }
                else
                {
                    lblAirportDropOf.Visible = false;
                    lblAirportPickUp.Visible = false;
                    numAirportDropOf.Visible = false;
                    numAirportPickUp.Visible = false;
                    radLabel1.Visible = false;
                    radLabel5.Visible = false;
                    CalculateTotal();
                }
            }
            catch (Exception ex)
            { }
        }

        void frmBookingPayment2_Shown(object sender, EventArgs e)
        {
            ddlCardType.Focus();
        }



        void SpinElement_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();


        }

        private void CalculateTotal()
        {


            if (numSurchargePercent.Value > 0)
            {
                numSurchargeAmount.Value = (txtAmount.Value * numSurchargePercent.Value) / 100;
            }

            numTotalCharges.Value = txtAmount.Value + numSurchargeAmount.Value + numTipAmount.Value;

        }




        void ddlGateway_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {


                //if (ddlGateway.SelectedValue.ToInt() == 5)
                //{
                //    pnlBilling.Visible = false;
                //    this.Size = new Size(630, this.Size.Height);
                //    lblNameOnCard.Visible = false;
                //    txtNameOnCard.Visible = false;

                //    lblStartDate.Visible = false;
                //    dtpStartDate.Visible = false;

                //}
                //else
                //{
                //    pnlBilling.Visible = true;
                //    this.Size = new Size(965, this.Size.Height);
                //    lblNameOnCard.Visible = true;
                //    txtNameOnCard.Visible = true;
                //}

            }
            catch (Exception ex)
            {


            }

        }

        private Booking_Payment pickedPaymentObj = null;


        private void PickCreditCardDetails()
        {
            try
            {

                if (objMaster.Current != null)
                {
                    string telNo = objMaster.Current.CustomerPhoneNo.ToStr().Trim();
                    string mobNo = objMaster.Current.CustomerMobileNo.ToStr().Trim();



                    // if (!string.IsNullOrEmpty(telNo) || !string.IsNullOrEmpty(mobNo))
                    // {

                    pickedPaymentObj = GeneralBLL.GetQueryable<Booking_Payment>(c =>
                        ((mobNo != string.Empty && c.Booking.CustomerMobileNo == mobNo)
                     || (telNo != string.Empty && c.Booking.CustomerPhoneNo == telNo))
                     && (c.CardNumber != null && c.CardNumber.Length > 0)
                    ).OrderByDescending(c => c.Id).FirstOrDefault();



                    PickData();

                    //  }
                }
            }
            catch (Exception ex)
            {


            }

        }


        private void PickData()
        {

            if (pickedPaymentObj != null)
            {
                txtAddress.Text = pickedPaymentObj.Address.ToStr().Trim();
                txtCardNumber.Text = pickedPaymentObj.CardNumber.ToStr().Trim();
                txtCity.Text = pickedPaymentObj.City.ToStr().Trim();
                txtCV2.Text = pickedPaymentObj.CV2.ToStr().Trim();
                txtEmail.Text = pickedPaymentObj.Email.ToStr().Trim();
                PickedEmail = txtEmail.Text.Trim();

                txtNameOnCard.Text = pickedPaymentObj.NameOnCard.ToStr().Trim();
                txtPostCode.Text = pickedPaymentObj.PostCode.ToStr().Trim();
                dtpExpiryDate.Value = pickedPaymentObj.CardExpiryDate.Value.ToDateorNull();
                dtpStartDate.Value = pickedPaymentObj.CardStartDate.Value.ToDateorNull();
                ddlCardType.SelectedValue = pickedPaymentObj.CreditCardTypeId.ToIntorNull();


            }

        }


        private void SetDefaultPaymentGateway()
        {
            try
            {
                if (this.ObjMerchantInfo.Count > 0)
                {

                    if (ObjMerchantInfo.Count == 1)
                    {
                        ddlGateway.SelectedValue = ObjMerchantInfo[0].PaymentGatewayId;
                        ddlGateway.Enabled = false;

                    }
                    else
                    {




                        ddlGateway.SelectedValue = ObjMerchantInfo.FirstOrDefault(c => c.EnableMobileIntegration == null || c.EnableMobileIntegration == false).DefaultIfEmpty().PaymentGatewayId;



                        if (ddlGateway.SelectedValue == null)
                            ddlGateway.SelectedValue = ObjMerchantInfo[0].PaymentGatewayId;


                    }



                }
            }
            catch
            {


            }


        }

        string[] chargesLimit = null;

        private void frmBookingPayment_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.ObjPayment != null && ObjPayment.Id != 0)
                {
                    if (!string.IsNullOrEmpty(ObjPayment.AuthCode))
                    {
                        SetSuccess();
                        lblStatus.Text = ObjPayment.AuthCode.ToStr();

                        btnProcess.Enabled = false;
                        btnSave.Enabled = false;
                    }


                    if (!string.IsNullOrEmpty(ObjPayment.AuthCode) || ObjPayment.NetFares.ToDecimal() > 0)
                    {

                        txtAmount.Value = ObjPayment.NetFares.ToDecimal();
                        numSurchargePercent.Value = ObjPayment.SurchargePercent.ToDecimal();
                        numSurchargeAmount.Value = ObjPayment.SurchargeAmount.ToDecimal();
                        numTipAmount.Value = ObjPayment.TipAmount.ToDecimal();
                        numTotalCharges.Value = ObjPayment.TotalAmount.ToDecimal();
                    }


                    ddlCardType.SelectedValue = ObjPayment.CreditCardTypeId.ToInt();
                    txtNameOnCard.Text = ObjPayment.NameOnCard.ToStr();
                    txtCardNumber.Text = ObjPayment.CardNumber.ToStr();
                    dtpExpiryDate.Value = ObjPayment.CardExpiryDate;
                    dtpStartDate.Value = ObjPayment.CardStartDate;
                    txtCV2.Text = ObjPayment.CV2.ToStr();

                    txtAddress.Text = ObjPayment.Address.ToStr();
                    txtEmail.Text = ObjPayment.Email.ToStr();
                    txtCity.Text = ObjPayment.City.ToStr();
                    txtPostCode.Text = ObjPayment.PostCode.ToStr();

                    if (ObjPayment.PaymentGatewayId == 1)
                        lblStatus.Text = ObjPayment.AuthCode.ToStr();

                    //else
                    //    lblStatus.Text = ObjPayment.Status.ToStr();



                    if (ObjPayment.NameOnCard == null || ObjPayment.CardNumber == null)
                    {
                        PickData();

                    }

                    string bookingEmail = objMaster.Current.CustomerEmail.ToStr();

                    if (string.IsNullOrEmpty(txtEmail.Text) || (!string.IsNullOrEmpty(bookingEmail) && PickedEmail != bookingEmail))
                    {

                        txtEmail.Text = bookingEmail;
                    }
                }
                else
                {

                    if (objMaster.PrimaryKeyValue != null)
                    {

                        string bookingEmail = objMaster.Current.CustomerEmail.ToStr();

                        if (string.IsNullOrEmpty(txtEmail.Text) || (!string.IsNullOrEmpty(bookingEmail) && PickedEmail != bookingEmail))
                        {

                            txtEmail.Text = bookingEmail;
                        }
                    }
                }
            }
            catch (Exception ex)
            {



            }

        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            CloseForms();
        }

        private void CloseForms()
        {

            frm.Close();
            this.Close();

        }

        public string paymentInstructions = "";

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                lblStatus.Text = string.Empty;
                lblStatus.Size = new System.Drawing.Size(lblStatus.Width, 248);
             //   lblResponse.Visible = false;

                //  string authCode = string.Empty;
                DateTime? expiryDate = dtpExpiryDate.Value;
                DateTime? startDate = dtpStartDate.Value;

                string nameOnCard = txtNameOnCard.Text.Trim();
                string cardTypeName = ddlCardType.Text.Trim().ToUpper();
                string cardNumber = txtCardNumber.Text.Trim();

                //  string orderDesc = txtOrderDesc.Text.Trim();

                string cv2 = txtCV2.Text.Trim();

                decimal amount = txtAmount.Text.ToDecimal();

                int? cardTypeId = ddlCardType.SelectedValue.ToIntorNull();

                string email = txtEmail.Text.Trim();

                //     string address = txtAddress.Text.Trim();
                //     string city = txtCity.Text.Trim();

                //     string postcode = txtPostCode.Text.Trim();

                string firstName = string.Empty;
                string lastName = string.Empty;


                if (nameOnCard.WordCount() > 1)
                {
                    firstName = nameOnCard.Split(' ')[0].ToStr();
                    lastName = nameOnCard.Split(' ')[1].ToStr();
                }



                string status = string.Empty;

                string errorMsg = string.Empty;


                if (ddlGateway.SelectedValue == null)
                {
                    ENUtils.ShowMessage("Please select a Payment Gateway");
                    return;

                }


                int ddlGatewayId = ddlGateway.SelectedValue.ToInt();

                Gen_SysPolicy_PaymentDetail obj = General.GetObject<Gen_SysPolicy_PaymentDetail>(c => c.PaymentGatewayId == ddlGatewayId);


                if (obj == null)
                {
                    ENUtils.ShowMessage("Payment Gateway Configuration is not defined in Settings!");
                    return;
                }



                if (ddlGateway.SelectedValue.ToInt() != Enums.PAYMENT_GATEWAY.ATLANTE_CONNECTPAY)
                {


                    if (string.IsNullOrEmpty(nameOnCard))
                    {
                        errorMsg += "Required : Name On Card" + Environment.NewLine;
                    }

                    if (ddlCardType.SelectedValue.ToIntorNull() == null && chkSendEmailtoCustomer.Checked == false)
                    {
                        errorMsg += "Required : Name On Card" + Environment.NewLine;

                    }

                    //if (string.IsNullOrEmpty(cardNumber) && chkSendEmailtoCustomer.Checked == false)
                    //{
                    //    errorMsg += "Required : Card Type" + Environment.NewLine;
                    //}

                }

                if (string.IsNullOrEmpty(cv2) && chkSendEmailtoCustomer.Checked == false)
                {
                    errorMsg += "Required : CV2" + Environment.NewLine;
                }

                if (expiryDate == null && chkSendEmailtoCustomer.Checked == false)
                {
                    errorMsg += "Required : Expiry Date" + Environment.NewLine;
                }

                //if (dtpStartDate.Visible && startDate == null && chkSendEmailtoCustomer.Checked == false)
                //{
                //    errorMsg += "Required : Start Date" + Environment.NewLine;
                //}




                //if (dtpStartDate.Visible == true && startDate != null && expiryDate != null && expiryDate < startDate && chkSendEmailtoCustomer.Checked == false)
                //{
                //    errorMsg += "Required : Start Date cannot be greater than Expiry Date" + Environment.NewLine;
                //}


                if (!string.IsNullOrEmpty(errorMsg))
                {
                    ENUtils.ShowMessage(errorMsg);
                    return;

                }


                btnSave.Enabled = true;



                if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.CARDSAVE)
                {

                    string szMessage = null;
                    bool boTransactionSuccessful = false;
                    StringBuilder sbString;
                    int nCount = 0;

                    CardDetailsTransaction cdtCardDetailsTransaction;
                    RequestGatewayEntryPointList lrgepRequestGatewayEntryPoints;
                    GatewayOutput goGatewayOutput;
                    TransactionOutputMessage tomTransactionOutputMessage;
                    TransactionControl tcTransactionControl;
                    CardDetails cdCardDetails;
                    CreditCardDate ccdExpiryDate = null;
                    CreditCardDate ccdStartDate = null;
                    CustomerDetails cdCustomerDetails = null;
                    NullableInt nCountryCode = null;

                    string szPreviousTransactionMessage = null;
                    bool boDuplicateTransaction = false;

                    lrgepRequestGatewayEntryPoints = new RequestGatewayEntryPointList();

                    lrgepRequestGatewayEntryPoints.Add(new RequestGatewayEntryPoint("https://gw1.paymentsensegateway.com:4430", 100, 2));
                    lrgepRequestGatewayEntryPoints.Add(new RequestGatewayEntryPoint("https://gw2.paymentsensegateway.com:4430", 200, 2));
                    lrgepRequestGatewayEntryPoints.Add(new RequestGatewayEntryPoint("https://gw3.paymentsensegateway.com:4430", 300, 2));


                    tcTransactionControl = new TransactionControl(new NullableBool(true),
                        new NullableBool(false),
                        new NullableBool(false),
                        new NullableBool(true),
                        new NullableInt(60),
                        null,
                        null,
                        null,
                        null, new ThreeDSecurePassthroughData("N", "N", "A", "", ""),

                        null);



                    //   tcTransactionControl.ThreeDSecureOverridePolicy = new NullableBool(true);

                    ccdExpiryDate = new CreditCardDate((new NullableInt(expiryDate.Value.Month)), (new NullableInt(string.Format("{0:yy}", expiryDate).ToInt())));

                    if (startDate != null)
                        ccdStartDate = new CreditCardDate((new NullableInt(startDate.Value.Month)), (new NullableInt(string.Format("{0:yy}", startDate).ToInt())));

                    string cardIssuer = "";

                    cdCardDetails = new CardDetails(nameOnCard, cardNumber, ccdExpiryDate, ccdStartDate, cardIssuer, cv2);

                    nCountryCode = new NullableInt(826);

                    decimal amt = numTotalCharges.Value * 100;

                    string orderNo = "Order-Dispatch " + this.objMaster.Current != null ? this.objMaster.Current.BookingNo.ToStr() : "Order-Dispatch";

                    cdCustomerDetails = new CustomerDetails(new AddressDetails(txtAddress.Text.Trim(), string.Empty, string.Empty, string.Empty, txtCity.Text.Trim()
                                                                               , txtCity.Text.Trim(), txtPostCode.Text.Trim(), nCountryCode),
                                                                                email, "", null);

                    cdtCardDetailsTransaction = new CardDetailsTransaction(lrgepRequestGatewayEntryPoints,
                          new MerchantDetails(obj.MerchantID, obj.MerchantPassword),
                          new TransactionDetails(TRANSACTION_TYPE.SALE, new NullableInt(Convert.ToInt32(amt)), new NullableInt(826), orderNo, orderNo, tcTransactionControl, new ThreeDSecureBrowserDetails(new NullableInt(0), "*/*", "")),
                          cdCardDetails,
                          cdCustomerDetails,
                          null);





                    //   new ThreeDSecureAuthentication(null,null,new ThreeDSecureInputData(
                    //  cdtCardDetailsTransaction.TransactionDetails.TransactionControl.ThreeDSecurePassthroughData.AuthenticationValue

                    // send the SOAP request
                    if (!cdtCardDetailsTransaction.ProcessTransaction(out goGatewayOutput, out tomTransactionOutputMessage))
                    {
                        szMessage = "Couldn't communicate with payment gateway";
                        boTransactionSuccessful = false;
                    }
                    else
                    {
                        switch (goGatewayOutput.StatusCode)
                        {
                            case 0:
                                // status code of 0 - means transaction successful 
                                boTransactionSuccessful = true;
                                m_fmFormMode = FORM_MODE.RESULTS;
                                szMessage = goGatewayOutput.Message;
                                //  Save();
                                break;
                            case 3:
                                // status code of 3 - means 3D Secure authentication required 
                                m_fmFormMode = FORM_MODE.THREE_D_SECURE;
                                m_szTermURL = "";
                                m_szPaREQ = tomTransactionOutputMessage.ThreeDSecureOutputData.PaREQ;
                                m_szACSURL = tomTransactionOutputMessage.ThreeDSecureOutputData.ACSURL;
                                m_szCrossReference = tomTransactionOutputMessage.CrossReference;
                                szMessage = goGatewayOutput.Message;
                                break;
                            case 5:
                                // status code of 5 - means transaction declined 
                                boTransactionSuccessful = false;
                                m_fmFormMode = FORM_MODE.RESULTS;
                                szMessage = goGatewayOutput.Message;
                                break;
                            case 20:
                                // status code of 20 - means duplicate transaction 
                                m_fmFormMode = FORM_MODE.RESULTS;
                                szMessage = goGatewayOutput.Message;
                                if (goGatewayOutput.PreviousTransactionResult.StatusCode.Value == 0)
                                {
                                    boTransactionSuccessful = true;
                                }
                                else
                                {
                                    boTransactionSuccessful = false;
                                }
                                szPreviousTransactionMessage = goGatewayOutput.PreviousTransactionResult.Message;
                                boDuplicateTransaction = true;
                                break;
                            case 30:
                                // status code of 30 - means an error occurred 
                                boTransactionSuccessful = false;
                                m_fmFormMode = FORM_MODE.PAYMENT_FORM;

                                sbString = new StringBuilder();

                                // get any additional messages
                                if (goGatewayOutput.ErrorMessages.Count > 0)
                                {
                                    sbString.Append(Environment.NewLine);

                                    for (nCount = 0; nCount < goGatewayOutput.ErrorMessages.Count; nCount++)
                                    {
                                        sbString.AppendFormat("   {0}", goGatewayOutput.ErrorMessages[nCount]);
                                    }
                                    sbString.Append(Environment.NewLine);
                                }

                                szMessage = goGatewayOutput.Message + sbString.ToString();
                                break;
                            default:
                                // unhandled status code  
                                boTransactionSuccessful = false;
                                m_fmFormMode = FORM_MODE.PAYMENT_FORM;
                                szMessage = goGatewayOutput.Message;
                                break;
                        }
                    }

                    if (m_fmFormMode == FORM_MODE.PAYMENT_FORM)
                    {
                        lblStatus.Text = szMessage;
                        return;
                    }
                    else
                    {



                        if (!boTransactionSuccessful)
                        {
                            lblStatus.ForeColor = Color.Red;

                        }
                        else
                        {
                            SetSuccess();
                        }

                        lblStatus.Text = szMessage.ToStr();

                        // sort out the duplicate transaction reporting
                        if (boDuplicateTransaction)
                        {

                            lblStatus.Text = szPreviousTransactionMessage;
                        }

                        //  gatewayId =this.ObjMerchantInfo.PaymentGatewayId.ToInt();
                        //  authCode = lblStatus.Text.Contains("AuthCode:") ? lblStatus.Text.Replace("AuthCode:", "").Trim() : "";
                        status = lblStatus.Text.Trim();


                        if (lblStatus.ForeColor == Color.Green)
                        {
                            Save();

                        }

                    }
                }
                else if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.PAYPAL)
                {
                    string Address = txtAddress.Text.ToStr().Trim();
                    string PostCode = txtPostCode.Text.ToStr().Trim();
                    string City = txtCity.Text.ToStr().Trim();
                    string Email = txtEmail.Text.ToStr().Trim();
                    string Error = string.Empty;
                    if (string.IsNullOrEmpty(Address))
                    {
                        Error = "Required : Address";
                    }
                    if (string.IsNullOrEmpty(PostCode))
                    {
                        if (string.IsNullOrEmpty(Error))
                        {
                            Error = "Required : Post Code";
                        }
                        else
                        {
                            Error += Environment.NewLine + "Required : Post Code";
                        }
                    }
                    if (string.IsNullOrEmpty(City))
                    {
                        if (string.IsNullOrEmpty(Error))
                        {
                            Error = "Required : City";
                        }
                        else
                        {
                            Error += Environment.NewLine + "Required : City";
                        }
                    }
                    if (string.IsNullOrEmpty(Email))
                    {
                        if (string.IsNullOrEmpty(Error))
                        {
                            Error = "Required : Email";
                        }
                        else
                        {
                            Error += Environment.NewLine + "Required : Email";
                        }
                    }
                    if (!string.IsNullOrEmpty(Error))
                    {
                        ENUtils.ShowMessage(Error);
                        return;
                    }





                    string PaypalID = obj.PaypalID.ToStr();


                    int check = string.IsNullOrEmpty(obj.ApiCertificate) ? 2 : obj.ApiCertificate.ToInt();

                    //

                    string SubComapnyName = AppVars.objSubCompany.CompanyName;
                    StringBuilder sb = new StringBuilder();
                    decimal fares = numTotalCharges.Value.ToDecimal();
                    //  decimal parkingCharges = obj.CongtionCharges.ToDecimal();
                    //if (objMaster.Current.CompanyId != null)
                    //{
                    //    fares = objMaster.Current.CompanyPrice.ToDecimal();
                    //    //   parkingCharges = obj.ParkingCharges.ToDecimal();


                    //}


                    decimal total = 0.00m;


                    total = fares;

                    sb.Append("<html><head></head>");
                    // sb.Append("<style> table { table-layout:fixed;}table td { width: 100%;  overflow: hidden;  text-overflow: ellipsis;}</style>");
                    sb.Append("<body>");
                    sb.Append("<table style= width: 100%;\"font-family:Arial;font-size:14px;color:#222;line-height:20px;border-collapse:collapse;border:1px solid #e5e5e5;\" cellpadding=\"5\" cellspacing=\"5\" border=\"0\" >");



                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left;\">");
                    //  sb.Append("");
                    sb.Append("<img src='http://images.paypal.com/en_US/i/logo/paypal_logo.gif'/>");
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:center;\" colspan=\"2\">");
                    sb.Append("<img src='" + AppVars.objSubCompany.CompanyLogoOnlinePath.ToStr().Trim() + "' />"); // pinkapplelogo
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan=\"2\">");
                    //sb.Append("If you wish to book a return or onward journey please always call office.Don't book " +
                    //         "with Driver directly as it is illegal and not covered by insurance.");
                    //sb.Append("</td>");

                    //sb.Append("</tr>");
                    //
                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:center;\">");
                    //  sb.Append("");
                    sb.Append("<b>*** This is an automated Email *** </b>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    //  sb.Append("");
                    sb.Append("Hello,");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("You have received this email on behalf of (" + SubComapnyName + "), who has set up a payment of " + string.Format("{0:f2}", total) + " GBP for you to complete. <br/> This payment can be completed by clicking the link below.");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("The reference for this transaction is: " + SubComapnyName);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("Please click on the following link to complete this payment: ");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    //
                    string businesspaypalid = PaypalID;
                    string redirect = "";
                    if (check == 1)
                    {
                        redirect += "https://www.sandbox.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=" + businesspaypalid;
                    }
                    else
                    {
                        redirect += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + businesspaypalid;
                    }


                    redirect += "&item_name=Booking";
                    redirect += "&amount=" + Math.Round(numTotalCharges.Value, 2);
                    redirect += "&item_number=" + objMaster.Current.BookingNo.ToStr();
                    redirect += "&amp;currency_code=GBP";
                    redirect += "&first_name=" + firstName;
                    redirect += "&last_name=" + lastName;
                    redirect += "&address1=" + txtAddress.Text;
                    redirect += "&email=" + txtEmail.Text;
                    redirect += "&city=" + txtCity.Text;
                    redirect += "&night_phone_a=12345";
                    redirect += "&night_phone_b=00";
                    redirect += "&night_phone_c=12345";
                    redirect += "&zip=" + txtPostCode.Text;
                    redirect += "&state=" + txtPostCode.Text;
                    redirect += "&country=london";
                    sb.Append("<tr>");
                    sb.Append("<td><p style='width:50%;'>");
                    sb.Append("<a href='" + redirect + "'>" + redirect + "</a>");
                    sb.Append("</p></td>");
                    sb.Append("</tr>");
                    //

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("You will be directed to a payment form which will be pre populated with the transaction details. We will notify the merchant when the payment has been completed.");

                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("The full details of this payment are:");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("</td>");

                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("Amount: " + string.Format("{0:f2}", total) + " GBP");

                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("Reference: " + objMaster.Current.BookingNo);

                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("Description: " + objMaster.Current.BookingNo + " , Pickup at: " + string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.PickupDateTime));
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    //sb.Append("<tr>");
                    //sb.Append("<td>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td>");
                    //sb.Append("</td>");
                    //sb.Append("<tr>");
                    //sb.Append("<td>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("Thank you,");

                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append(" </body></html>");

                    //

                    //string businesspaypalid = PaypalID;
                    //string redirect = "";
                    //if (check == 1)
                    //{
                    //    redirect += "https://www.sandbox.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=" + businesspaypalid;
                    //}
                    //else
                    //{
                    //    redirect += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + businesspaypalid;
                    //}


                    //redirect += "&item_name=Booking";
                    //redirect += "&amount=" + Math.Round(numTotalCharges.Value, 2);
                    //redirect += "&item_number=1";
                    //redirect += "&currency_code=GBP";
                    //redirect += "&first_name=" + firstName;
                    //redirect += "&last_name=" + lastName;
                    //redirect += "&address1=" + txtAddress.Text;
                    //redirect += "&email=" + txtEmail.Text;
                    //redirect += "&city=" + txtCity.Text;
                    //redirect += "&night_phone_a=12345";
                    //redirect += "&night_phone_b=00";
                    //redirect += "&night_phone_c=12345";
                    //redirect += "&zip=" + txtPostCode.Text;
                    //redirect += "&state=" + txtPostCode.Text;
                    //redirect += "&country=london";

                    if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.PAYPAL && chkSendEmailtoCustomer.Checked)
                    {
                        string smtpHost = string.Empty;
                        string userName = string.Empty;
                        string pwd = string.Empty;
                        string port = string.Empty;
                        bool enableSSL = false;
                        Gen_SubCompany objSubCompany = AppVars.objSubCompany;

                        if (objSubCompany != null && !string.IsNullOrEmpty(objSubCompany.SmtpHost.ToStr().Trim()))
                        {
                            smtpHost = objSubCompany.SmtpHost.ToStr().Trim();
                            userName = objSubCompany.SmtpUserName.ToStr().Trim();
                            pwd = objSubCompany.SmtpPassword.ToStr().Trim();
                            port = objSubCompany.SmtpPort.ToStr().Trim();
                            enableSSL = objSubCompany.SmtpHasSSL.ToBool();
                        }

                        using (System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
                        {
                            msg.To.Add(txtEmail.Text.ToStr().Trim());
                            msg.From = new System.Net.Mail.MailAddress(userName, AppVars.objSubCompany.CompanyName.ToStr());
                            // msg.Subject = "Make payment with Paypal for booking Ref # " + objMaster.Current.BookingNo+ " PickUp Date Time: " +string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.PickupDateTime);
                            msg.Subject = "PayPal payment request from " + SubComapnyName + " .Please make payment to " + SubComapnyName + " via Paypal for booking Ref# " + objMaster.Current.BookingNo + " PickUp Date Time: " + string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.PickupDateTime) + ".";


                            //msg.Body = redirect;
                            msg.Body = sb.ToStr();

                            //   msg.BodyEncoding = System.Text.Encoding.UTF8;
                            msg.IsBodyHtml = true;



                            //    msg.Priority = System.Net.Mail.MailPriority.High;
                            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(smtpHost, Convert.ToInt32(port));
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential(userName, pwd);
                            client.Port = Convert.ToInt32(port);
                            client.Host = smtpHost;
                            client.EnableSsl = enableSSL;

                           
                                ServicePointManager.ServerCertificateValidationCallback =
                                    delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                             System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                                    { return true; };
                            

                            client.Send(msg);

                            General.SaveSentEmail(msg.Body, msg.Subject, txtEmail.Text.ToStr().Trim());

                        }

                        Save();

                        RadDesktopAlert alert = new RadDesktopAlert();
                        alert.CaptionText = "Email sent successfully";
                        alert.ContentText = "Paypal Link";
                        alert.ContentImage = Resources.Resource1.email;
                        alert.Show();

                        if (this.IsSave)
                        {

                            Close();
                        }
                        else
                            return;
                    }
                    else
                    {
                        frmPaypalPayment Pay = new frmPaypalPayment(redirect.ToStr());
                        Pay.Show();
                    }

                }


                else if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.WORLDPAY)
                {
                    ENUtils.ShowMessage("After Payment Process You Need To save Customer data!");
                    // Live instId = 316579

                    string redirect = "https://select.worldpay.com/wcc/purchase?";



                    redirect += "instId=" + obj.PaypalID.ToStr();
                    redirect += "&cartId=" + objMaster.Current.BookingNo.ToStr();
                    redirect += "&amount=" + numTotalCharges.Value;
                    redirect += "&currency=GBP";
                    redirect += "&desc=" + objMaster.Current.BookingNo.ToStr();
                    redirect += "&name=" + this.objMaster.Current.CustomerName.ToStr();
                    redirect += "&address1=" + txtAddress.Text.ToUpper().Trim();
                    redirect += "&town=" + txtCity.Text.Trim();
                    redirect += "&postcode=" + txtPostCode.Text.Trim();
                    redirect += "&cardtype=Visa";

                    redirect += "&country=GB";
                    redirect += "&email=" + txtEmail.Text;




                    frmPaypalPayment Pay = new frmPaypalPayment(redirect.ToStr());
                    Pay.Show();
                }
                else if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.BARCLAY)
                {
                    ENUtils.ShowMessage("After Payment Process You Need To save Customer data!");

                    string redirect = obj.ApplicationId.ToStr() + "?";

                    redirect += "PSPID=" + obj.PaypalID.ToStr();
                    redirect += "&ORDERID=" + objMaster.Current.BookingNo.ToStr();
                    redirect += "&AMOUNT=" + numTotalCharges.Value;
                    redirect += "&CURRENCY=GBP";
                    redirect += "&LANGUAGE=en_US";
                    redirect += "&CN=" + nameOnCard;
                    redirect += "&EMAIL=" + txtEmail.Text;


                    frmPaypalPayment Pay = new frmPaypalPayment(redirect.ToStr());
                    Pay.Show();

                }
                else if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.ATLANTE_CONNECTPAY)
                {
                    try
                    {


                        Taxi_AppMain.ClsPaymentGatewayRequest.CardDetailsEx objCard = new Taxi_AppMain.ClsPaymentGatewayRequest.CardDetailsEx();
                        objCard.amount = Convert.ToDouble(numTotalCharges.Value);
                        objCard.cardNumber = cardNumber;
                        objCard.expiryMonth = dtpExpiryDate.Value.Value.Month.ToStr();
                        objCard.expiryYear = dtpExpiryDate.Value.Value.Year.ToStr();
                        objCard.cv2 = cv2;
                        objCard.signature = obj.ApplicationId.ToStr();
                        objCard.paymentgateway = "Adlante";
                        objCard.merchantid = obj.MerchantID.ToStr();
                        objCard.merchantpassword = obj.MerchantPassword.ToStr();
                       // objCard.responselog = true;
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(objCard);

                        ClsPaymentGatewayRequest cls = new ClsPaymentGatewayRequest();

                        var response = cls.MakePayment("MakePaymentByGatewayDispatch", json, "softeuroconnskey");

                        if (response.Contains("{"))
                        {


                            Taxi_AppMain.ClsPaymentGatewayRequest.ClsPaymentGatewayResponse objResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Taxi_AppMain.ClsPaymentGatewayRequest.ClsPaymentGatewayResponse>(response);


                            if (objResponse != null)
                            {




                                if (objResponse.success)
                                {


                                    txtAmount.Enabled = false;
                                    numSurchargePercent.Enabled = false;
                               
                                    lblResponse.Visible = true;



                                    lblStatus.Text = "AuthCode:" + objResponse.AuthCode;
                                    TransactionID = lblStatus.Text;
                                    paymentInstructions = "Date: " + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) +Environment.NewLine + "Total: " + objCard.amount + Environment.NewLine + "Auth Code: " + objResponse.AuthCode.ToStr();


                                    try
                                    {
                                        responseLog = lblStatus.Text + " (Payment From Dispatch)";
                                        lblResponse.Text = responseLog;


                                        //if (optFirst.Checked)
                                        //    PaymentFor = 1;
                                        //else if (optReturnOnly.Checked)
                                        //    PaymentFor = 2;
                                        //else if (optBothJourneys.Checked)
                                        //    PaymentFor = 3;

                                    }
                                    catch
                                    {


                                    }
                                    //  responseLog = objResponse.Message + Environment.NewLine + "AuthCode:" + objResponse.AuthCode + Environment.NewLine + "Amount:" + objCard.amount;
                                    Save();
                                    //  responseLog = string.Empty;
                                }
                                else
                                {
                                    responseLog = "";
                                    lblStatus.Text = objResponse.Message.ToStr();


                                }
                            }
                        }
                        else
                        {
                            lblStatus.Text = "";

                        }


                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "exception" + ex.ToString();
                    }
                }
                else if (ddlGateway.SelectedValue.ToInt() == Enums.PAYMENT_GATEWAY.STRIPE)
                {
                    try
                    {

                        Taxi_AppMain.ClsPaymentGatewayRequest.CardDetailsEx objCard = new Taxi_AppMain.ClsPaymentGatewayRequest.CardDetailsEx();
                        objCard.amount =Convert.ToDouble(numTotalCharges.Value);
                        objCard.cardNumber = cardNumber;
                        objCard.expiryMonth = dtpExpiryDate.Value.Value.Month.ToStr();
                        objCard.expiryYear = dtpExpiryDate.Value.Value.Year.ToStr();
                        objCard.cv2 = cv2;
                        objCard.signature = obj.PaypalID;
                        objCard.paymentgateway="Stripe";


                          string json = Newtonsoft.Json.JsonConvert.SerializeObject(objCard);

                          ClsPaymentGatewayRequest cls = new ClsPaymentGatewayRequest();
                        var response=   cls.MakePayment("MakePaymentByGatewayDispatch", json, "softeuroconnskey");

                        if (response.Contains("{"))
                        {
                            Taxi_AppMain.ClsPaymentGatewayRequest.ClsPaymentGatewayResponse objResponse = Newtonsoft.Json.JsonConvert.DeserializeObject < Taxi_AppMain.ClsPaymentGatewayRequest.ClsPaymentGatewayResponse>(response);


                            if (objResponse != null)
                            {

                              
                               
                                lblResponse.Text = objResponse.Message;
                                if (objResponse.success)
                                {
                                    lblResponse.ForeColor = Color.Green;
                                    lblResponse.Visible = true;
                                    txtAmount.Enabled = false;
                                    numSurchargePercent.Enabled = false;


                                    lblStatus.Text = "AuthCode:" + objResponse.AuthCode;
                                    TransactionID = lblStatus.Text;


                                    responseLog = objResponse.Message + Environment.NewLine + "Auth Code:" + objResponse.AuthCode + Environment.NewLine + "Amount:"+string.Format("{0:##0.##}", objCard.amount); 
                                
                                    paymentInstructions = "Date: " + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now) + Environment.NewLine + "Total: " + string.Format("{0:##0.##}", objCard.amount) + Environment.NewLine + "Auth Code: " + objResponse.AuthCode.ToStr();


                                    try
                                    {
                                      //  responseLog = lblStatus.Text + " (Payment From Dispatch)";
                                        lblResponse.Text = objResponse.Message + Environment.NewLine  + "Amount:" + string.Format("{0:##0.##}", objCard.amount); 

                                        responseLog += " (Payment From Dispatch)";



                                    }
                                    catch
                                    {


                                    }

                                    Save();
                                    responseLog = string.Empty;
                                }
                                else
                                {
                                    lblResponse.ForeColor = Color.Red;

                                }
                              
                                   
                            }
                        }
                        else
                        {
                            lblResponse.Text = response;

                        }                    

                 

                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "exception" + ex.ToString();
                    }
                }
                else if (obj.PaymentGatewayId.ToInt() == Enums.PAYMENT_GATEWAY.JUDO)
                {

                    JudoProperties obje = new JudoProperties();
                    obje.yourConsumerReference = objMaster.Current.BookingNo.ToStr().Trim();
                    obje.yourPaymentReference = objMaster.Current.BookingNo.ToStr().Trim();

                    obje.judoId = obj.PaypalID.ToStr();
                    obje.Acc_SecretKey = obj.MerchantID.ToStr();
                    obje.Acc_Token = obj.MerchantPassword.ToStr();


                    decimal amt = numTotalCharges.Value;

                    //   decimal amt = Math.Round((numTotalCharges.Value * 100), 0);


                    obje.amount = Convert.ToDouble(amt);
                    obje.cardNumber = txtCardNumber.Text.ToStr().Trim(); // 4976000000003436
                    obje.expiryDate = string.Format("{0:MM}", dtpExpiryDate.Value) + "/" + string.Format("{0:yy}", dtpExpiryDate.Value); // 1220
                    obje.cv2 = txtCV2.Text.Trim();  // 458
                    obje.currency = "GBP";
                    obje.cardAddress.line1 = string.Empty;
                    obje.cardAddress.line2 = "";
                    obje.cardAddress.line3 = "";
                    obje.cardAddress.town = string.Empty;
                    obje.cardAddress.postCode = string.Empty;
                    obje.consumerLocation.latitude = 0.00;// 51.5214541344954;
                    obje.consumerLocation.longitude = 0.00;// -0.203098409696038;
                    obje.mobileNumber = string.Empty;
                    obje.emailAddress = txtEmail.Text.Trim();
                    obje.TestMode = false;

                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(obje);



                    try
                    {
                        string rtn = string.Empty;

                        using (WebClient client = new WebClient())
                        {
                            client.Headers.Add("API-Version: 5.0");
                            client.Headers.Add("Content-Type", "application/json");

                            rtn = client.DownloadString("http://shadowcars.co.uk/api/Jobs/MakePayment?jsonString=" + json);
                        }

                        string ssaa = rtn.Replace("\\", "").Substring(1);
                        ssaa = ssaa.Substring(0, ssaa.Length - 1);

                        ResponseJudo objCls = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseJudo>(ssaa);


                        if ((objCls.receiptId.ToStr().Length > 0))
                        {
                            SetSuccess();
                            lblStatus.Text = objCls.message.ToStr().Replace(" ", "").Trim();

                            Save();
                        }
                        //    rtn = "success:" + objCls.message.ToStr().Replace(" ", "").Trim();
                    }
                    catch (Exception ex)
                    {

                        lblStatus.Text = ex.Message.ToString();
                        SetFailure();
                    }

                }
				else if (ddlGateway.SelectedValue.ToInt() == 10)
				{
					//ENUtils.ShowMessage("After Payment Process You Need To save Customer data!");
					// Live instId = 316579

					//string redirect = fillInfotest(); //test
					string redirect = fillInfo(obj); //live

					frmPaypalPayment Pay = new frmPaypalPayment(redirect.ToStr());					
					Pay.Show();
				}
			}
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }

        }

		private string fillInfo(Gen_SysPolicy_PaymentDetail obj)
		{

			CardStreamSettings cardStreamSettings = new CardStreamSettings();
			cardStreamSettings.merchantId = obj.PaypalID.ToStr();
			cardStreamSettings.action = "SALE";
			cardStreamSettings.transType = 1;
			cardStreamSettings.uniqueIdentifier = Guid.NewGuid().ToString();
			cardStreamSettings.currencyCode = 826;
			cardStreamSettings.amount = Math.Round(numTotalCharges.Value.ToDecimal(),2); // VISA
			cardStreamSettings.orderRef = objMaster.Current.Id.ToStr() + "|" + objMaster.Current.BookingNo.ToStr();
			cardStreamSettings.cardNumber = txtCardNumber.Text; // VISA
			cardStreamSettings.cardExpiryMM = dtpExpiryDate.Value.Value.Month.ToStr();
			cardStreamSettings.cardExpiryYY = dtpExpiryDate.Value.Value.ToString("yy");
			cardStreamSettings.cardCVV = txtCV2.Text;
			cardStreamSettings.customerName = this.objMaster.Current.CustomerName.ToStr(); // VISA
			cardStreamSettings.customerEmail = txtEmail.Text;
			cardStreamSettings.customerPhone = objMaster.Current.CustomerMobileNo;
			cardStreamSettings.customerAddress = txtAddress.Text.ToUpper().Trim();
			cardStreamSettings.countryCode = 826;
			cardStreamSettings.customerPostcode = txtPostCode.Text.Trim();

			JavaScriptSerializer cc = new JavaScriptSerializer();
			string data = Cryptography.Encrypt(cc.Serialize(cardStreamSettings), "softeuroconnskey", true);
			var url = "https://eurosofttech-api.co.uk/cardstream/Default.aspx?data=" + HttpUtility.UrlEncode(data);

			return url;

		}

		private string fillInfotest()
		{
			CardStreamSettings cardStreamSettings = new CardStreamSettings();
			cardStreamSettings.merchantId = "100001";
			cardStreamSettings.action = "SALE";
			cardStreamSettings.transType = 1;
			cardStreamSettings.uniqueIdentifier = Guid.NewGuid().ToString();
			cardStreamSettings.currencyCode = 826;
			cardStreamSettings.amount = 1202; // VISA
			cardStreamSettings.orderRef = objMaster.Current.Id.ToStr() + "|" + objMaster.Current.BookingNo.ToStr();
			cardStreamSettings.cardNumber = "4012010000000000009"; // VISA
			cardStreamSettings.cardExpiryMM = "12";
			cardStreamSettings.cardExpiryYY = "19";
			cardStreamSettings.cardCVV = "332";
			cardStreamSettings.customerName = "CardStream"; // VISA
			cardStreamSettings.customerEmail = "solutions@cardstream.com";
			cardStreamSettings.customerPhone = "+44(0)8450099575";
			cardStreamSettings.customerAddress = "31 Test Card Street";
			cardStreamSettings.countryCode = 826;
			cardStreamSettings.customerPostcode = "1TEST8";

			JavaScriptSerializer cc = new JavaScriptSerializer();
			string data = Cryptography.Encrypt(cc.Serialize(cardStreamSettings), "softeuroconnskey", true);
			var url = "http://88.208.220.41/cardstream/Default.aspx?data=" + HttpUtility.UrlEncode(data);

			return url;

		}

		private string responseLog = string.Empty;

        public override void Save()
        {
            try
            {
                objMaster.PrimaryKeyValue = ID;
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }

                //card Detail

                objMaster.Current.BookingPayment.NameOnCard = txtNameOnCard.Text.Trim();
                objMaster.Current.BookingPayment.CardNumber = txtCardNumber.Text.Trim();
                objMaster.Current.BookingPayment.CardExpiryDate = dtpExpiryDate.Value.ToDateorNull();


                if (dtpStartDate.Visible)
                {
                    objMaster.Current.BookingPayment.CardStartDate = dtpStartDate.Value.ToDateorNull();
                }
                else
                {
                    objMaster.Current.BookingPayment.CardStartDate = dtpExpiryDate.Value.ToDateorNull();

                }

                objMaster.Current.BookingPayment.CV2 = txtCV2.Text.ToString();
                objMaster.Current.BookingPayment.CreditCardTypeId = ddlCardType.SelectedValue.ToIntorNull();

                //Customer Detail
                objMaster.Current.BookingPayment.BookingId = ID.ToInt();
                objMaster.Current.BookingPayment.Status = "Paid";
                // objMaster.Current.BookingPayment.OrderDescription = txtOrderDesc.Text.Trim();
                objMaster.Current.BookingPayment.Address = txtAddress.Text.Trim();
                objMaster.Current.BookingPayment.City = txtCity.Text.Trim();
                objMaster.Current.BookingPayment.PostCode = txtPostCode.Text.Trim();
                objMaster.Current.BookingPayment.Email = txtEmail.Text.Trim();

                objMaster.Current.BookingPayment.AuthCode = lblStatus.Text.ToStr().ToLower().Contains("authcode:") ? lblStatus.Text.ToStr().ToLower().Replace("authcode:", "").Trim() : "";
                objMaster.Current.BookingPayment.PaymentGatewayId = ddlGateway.SelectedValue.ToIntorNull();

                objMaster.Current.BookingPayment.NetFares = txtAmount.Value;
                objMaster.Current.BookingPayment.SurchargePercent = numSurchargePercent.Value;
                objMaster.Current.BookingPayment.SurchargeAmount = numSurchargeAmount.Value;
                objMaster.Current.BookingPayment.TipAmount = numTipAmount.Value;
                objMaster.Current.BookingPayment.TotalAmount = numTotalCharges.Value;



                objMaster.Current.PaymentComments += objMaster.Current.PaymentComments.ToStr().Trim().Length > 0 ? Environment.NewLine + "AUTH CODE :" + objMaster.Current.BookingPayment.AuthCode.ToStr() : objMaster.Current.BookingPayment.AuthCode.ToStr();



                if (responseLog.ToStr().Trim().Length > 0)
                {
                    objMaster.Current.Booking_Logs.Add(new Booking_Log { BookingId = objMaster.Current.Id, User = AppVars.LoginObj.LoginName, AfterUpdate = responseLog, UpdateDate = DateTime.Now });
                }

                objMaster.Save();
                IsSave = true;

            }
            catch (Exception ex)
            {
                IsSave = false;
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }

        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {

        }







        private void SetFailure()
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Font = new Font("Tahoma", 10, FontStyle.Bold);

        }


        private void SetSuccess()
        {

            lblStatus.ForeColor = Color.Green;
            lblStatus.Font = new Font("Tahoma", 12, FontStyle.Bold);
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            if (IsSave == true)
            {
                CloseForms();
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

            PickCreditCardDetails();
            //string CardNumber = txtCardNumber.Text.ToStr();
            //Booking_Payment obj = General.GetObject<Booking_Payment>(c => c.CardNumber != null && c.CardNumber.StartsWith(CardNumber));




            //if (obj != null)
            //{
            //    txtCV2.Text = obj.CV2.ToStr();
            //    txtNameOnCard.Text = obj.NameOnCard.ToStr();
            //    //  txtNameOnCard.Text = obj.NameOnCard.ToStr();
            //    dtpExpiryDate.Value = obj.CardExpiryDate.ToDate();
            //    dtpStartDate.Value = obj.CardStartDate.ToDate();

            //    txtCardNumber.Text = obj.CardNumber.ToStr().Trim();

            //    txtAddress.Text = obj.Address.ToStr();
            //    txtCity.Text = obj.City.ToStr();
            //    txtPostCode.Text = obj.PostCode.ToStr();
            //    txtEmail.Text = obj.Email.ToStr();


            //    ddlCardType.SelectedValue = obj.CreditCardTypeId.ToIntorNull();
            //}
            //else
            //{
            //    ENUtils.ShowMessage("No Card Details Found!");
            //}
        }

        private void chkSendEmailtoCustomer_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkSendEmailtoCustomer.Checked)
            {
                ddlCardType.Enabled = false;
                txtCardNumber.Enabled = false;
                dtpExpiryDate.Enabled = false;
                dtpStartDate.Enabled = false;
                txtCV2.Enabled = false;
                radButton1.Enabled = false;
            }
            else
            {
                ddlCardType.Enabled = true;
                txtCardNumber.Enabled = true;
                dtpExpiryDate.Enabled = true;
                dtpStartDate.Enabled = true;
                txtCV2.Enabled = true;
                radButton1.Enabled = true;

            }
        }




    }
}
