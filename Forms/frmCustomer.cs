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
using System.Net;
using System.Xml.Linq;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmCustomer : UI.SetupBase
    {
        CustomerBO objMaster;
        public frmCustomer()
        {
            InitializeComponent();
            InitializeConstructor();
          
        }

        public bool OpenFromBooking = false;


        public frmCustomer(string phoneNumber)
        {
            InitializeComponent();
            this.txtMobileNo.Text = phoneNumber;

            InitializeConstructor();
        }

        private void InitializeConstructor()
        {

            objMaster = new CustomerBO();
            this.SetProperties((INavigation)objMaster);



            timer1.Tick += new EventHandler(timer1_Tick);

            txtAddress1.ListBoxElement.Width = 400;
            txtAddress2.ListBoxElement.Width = 400;
            FillCombo();
            this.Shown += new EventHandler(frmCustomer_Shown);

            if (AppVars.objPolicyConfiguration != null)
            {

                MapType = AppVars.objPolicyConfiguration.MapType.ToInt();

            }

            
        }


        private void FillCombo()
        {
          
        }


      //  bool IsExistData = false;

      
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






                string postCode = General.GetPostCodeMatch(searchTxt.ToUpper());
                if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                    postCode = string.Empty;

                string street = searchTxt;
                if (!string.IsNullOrEmpty(postCode))
                    street = street.ToLower().Replace(postCode.ToLower(), "").Trim();




                res = (from a in AppVars.listOfAddress

                       where (a.AddressLine1.ToLower().Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))
                       select a.AddressLine1

                                   ).Take(1000).ToArray<string>();

                //}

                if (res.Count() == 0)
                {


                    string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + searchTxt + " UK&sensor=false";
                  //  IsAutoComplete = IsAutoComplete == true ? false : true;

                    wc.CancelAsync();

                    wc = new WebClient();
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                    wc.DownloadStringAsync(new Uri(url));


                    return;
                }


                // Zone Working

                //   searchTxt = searchTxt.Substring(0, 3);



                ShowAddresses();
            }
            catch (Exception ex)
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

        void frmCustomer_Shown(object sender, EventArgs e)
        {
            txtName.Focus();


            if(OpenFromBooking)
             this.ShowSaveAndNewButton=false;

        }

     
        AutoCompleteTextBox aTxt = null;
        WebClient wc = new WebClient();
        private int _MapType;

        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }

        string[] res = null;
        string searchTxt = "";
        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();

          
            if (MapType == Enums.MAP_TYPE.NONE) return;
            try
            {


                aTxt = (AutoCompleteTextBox)sender;

                aTxt.ResetListBox();
                string text = aTxt.Text;
                if (text.Length > 2)
                {

                   

                        if (aTxt.SelectedItem != null && aTxt.SelectedItem == aTxt.Text)
                        {
                            return;
                        }



                            StartAddressTimer(text);
                     
                     


                        if (aTxt.Name == "txtAddress1")
                        {

                            txtAddress2.SendToBack();
                       

                        }
                        else if (aTxt.Name == "txtAddress2")
                        {
                            txtAddress2.BringToFront();
                         
                        }

                        radPanel2.SendToBack();
                  
               
                }
                    else
                    {
                       
                            wc.CancelAsync();
                            aTxt.Values = null;

                        
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
            timer1.Start();

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

        #region Overridden Methods


        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
         


        }

      

        public override void Save()
        {
            try
            {
                //if (chkBlackList.Checked == true && txtResion.Text == "")
                //{
                //    ENUtils.ShowMessage("Requried: Resion");
                //}

                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();

                }
                else
                {
                    objMaster.Edit();
                }


             
                objMaster.Current.Name = txtName.Text.Trim();
                objMaster.Current.Email = txtEmail.Text.Trim();
                objMaster.Current.TelephoneNo = txtTelephoneNo.Text.Trim();
                objMaster.Current.MobileNo = txtMobileNo.Text.Trim();
                objMaster.Current.Address1 = txtAddress1.Text.Trim();
                objMaster.Current.Address2 = txtAddress2.Text.Trim();
                objMaster.Current.DoorNo = txtDoorNo.Text.Trim();

                objMaster.Current.AccountNo = chkDisableIVR.Checked ? "1" : "0";
                objMaster.Current.BlackList = chkBlackList.Checked.ToBool();
                objMaster.Current.BlackListResion = txtResion.Text.ToString();
                objMaster.Current.TotalCalls = txtTotalCalls.Value.ToInt();

           
            //    objMaster.Current.CreditCardDetails = txtCreditCard.Text.Trim();
                objMaster.Current.LikesAndDislikes = txtNotes.Text.Trim();
                objMaster.Save();

          //      General.RefreshListWithoutSelected<frmCustomersList>("frmCustomersList1");
               // General.RefreshListWithoutSelected<frmBlackCustomersList>("frmBlackCustomersList1");


              
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

        public override void DisplayRecord()
        {
            try
            {
                if (objMaster.Current == null) return;


                txtName.Text = objMaster.Current.Name.ToStr();
                txtEmail.Text = objMaster.Current.Email.ToStr();
                txtTelephoneNo.Text = objMaster.Current.TelephoneNo.ToStr();
                txtMobileNo.Text = objMaster.Current.MobileNo.ToStr();

                txtAddress1.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtAddress1.Text = objMaster.Current.Address1.ToStr();
                txtAddress1.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtAddress2.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtAddress2.Text = objMaster.Current.Address2.ToStr();
                txtAddress2.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtDoorNo.Text = objMaster.Current.DoorNo.ToStr();

                chkBlackList.Checked = objMaster.Current.BlackList.ToBool();
             
                txtCreditCard.Text = objMaster.Current.CreditCardDetails.ToStr().Trim();

                if (chkBlackList.Checked == false)
                {
                    txtResion.Text = "";
                }
                else
                {
                    txtResion.Text = objMaster.Current.BlackListResion.ToStr();
                }


                chkDisableIVR.Checked = objMaster.Current.AccountNo.ToStr().Trim() == "1";

                txtTotalCalls.Value = objMaster.Current.TotalCalls.ToDecimal();

                txtNotes.Text = objMaster.Current.LikesAndDislikes.ToStr().Trim();

                if (objMaster.Current.ExcludedDriverIds != "" && objMaster.Current.ExcludedDriverIds != null)
                    btnExcludedDrivers.Text = "Excluded Drivers (" + (objMaster.Current.ExcludedDriverIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length).ToStr() + ")";


                this.excludedDriverIds = objMaster.Current.ExcludedDriverIds.ToStr().Trim();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

       

        #endregion

        private void txtName_Validated(object sender, EventArgs e)
        {
            txtName.Text =txtName.Text.ToStr().ToProperCase();
        }

        private void myDatePicker1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void myDatePicker1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            General.DisposeForm(this);

            GC.Collect();
        }

        private void chkBlackList_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            BlackList(args.ToggleState);
        }
        private void BlackList(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                lblResion.Visible = true;
                txtResion.Visible = true;
            }
            else
            {
                lblResion.Visible = false;
                txtResion.Visible = false;
            }
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
           // bool success = General.SendPDAMessage("request app fares=Airport=GATWICK AIRPORT NORTH RH6 0PJ=Station=SOUTHALL RAILWAY STATION UB2 4AA= = =0=0= =45.46");


        }


        public string excludedDriverIds;
        public string excludedDriverNos;
        private void btnExcludedDrivers_Click(object sender, EventArgs e)
        {

            try
            {

                if (objMaster != null)
                {
                    if (objMaster.Current.Id != 0)
                    {
                        string Ids = objMaster.Current.ExcludedDriverIds.ToStr();
                        frmCustomerExcDriversList frm = new frmCustomerExcDriversList(Ids, objMaster.Current.Id);
                        frm.ShowDialog();


                        this.excludedDriverIds= frm.input_Ids;
                        this.excludedDriverNos = frm.input_values;
                        frm.Dispose();


                        objMaster.GetByPrimaryKey(objMaster.Current.Id);

                        DisplayRecord();

                    }

                }
            }
            catch
            {


            }
               
            
        }
    }
}
