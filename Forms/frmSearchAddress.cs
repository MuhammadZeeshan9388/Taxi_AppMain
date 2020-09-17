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
using System.Collections;
using Taxi_AppMain.Forms;

namespace Taxi_AppMain
{
    public partial class frmSearchAddress : BaseForm
    {

        public IList<Booking_Note> Points { get; set; }

        AutoCompleteTextBox aTxt;
        WebClient wc = new WebClient();
        bool UseGoogleMap = true;
        private int _MapType;
        private bool EnablePOI = false;

        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }

        public frmSearchAddress()
        {
            InitializeComponent();

            SetFocus();
            InitializeConstructor();
 
           
          
        }

        private void InitializeConstructor()
        {
           
            
            this.txtAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            txtAddress.ListBoxElement.Width = txtAddress.DefaultWidth;
            txtAddress.ListBoxElement.Height = txtAddress.DefaultHeight;

            EnablePOI = AppVars.objPolicyConfiguration.EnablePOI.ToBool();
            timer1.Tick += new EventHandler(timer1_Tick);
            txtAddress.Select();
        }

        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {


            try
            {
                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

          



                string text = aTxt.Text;
                if (text.Length > 2)
                {

                    if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
                    {
                        aTxt.ListBoxElement.Items.Clear();
                        // aTxt.Values = null;
                        aTxt.ResetListBox();


                        string formerValue = aTxt.FormerValue.ToLower().Trim();

                        int? loctypeId = 0;
                        if (AppVars.keyLocations.Contains(formerValue))
                        {
                            Gen_Location loc = General.GetObject<Gen_Location>(c => c.ShortCutKey == formerValue);
                            if (loc != null)
                            {
                                loctypeId = loc.LocationTypeId;
                            }
                        }

                        if (loctypeId != 0)
                        {

                           
                        }

                        aTxt.FormerValue = string.Empty;

                        return;
                    }





                        if (UseGoogleMap)
                        {
                            wc.CancelAsync();
                            aTxt.Values = null;
                        }
                    text = text.ToLower();


                    if (AppVars.keyLocations.Contains(text))
                    {


                        aTxt.ListBoxElement.Items.Clear();
                        var res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey == text)
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                    ).ToArray<string>();


                        aTxt.ListBoxElement.Items.AddRange(res);
                        aTxt.ShowListBox();
                        

                        if (this.Text != aTxt.FormerValue)
                        {
                            aTxt.FormerValue = aTxt.Text;
                        }
                    }


                    if (MapType == Enums.MAP_TYPE.NONE) return;

                    StartAddressTimer(text);

                }
                else
                {
                    if (MapType == Enums.MAP_TYPE.NONE) return;


                    if (UseGoogleMap)
                    {
                        wc.CancelAsync();
                        aTxt.Values = null;
                    }
                }
                //txtAddress.Focus();


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
       
        string[] res = null;
        string searchTxt = "";
        bool IsPOI = false;
        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
           
                timer1.Stop();

                searchTxt = searchTxt.ToUpper();


              

                string postCode = General.GetPostCodeMatch(searchTxt);
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
                        else if (postCode.Length > 3 && Char.IsNumber(postCode[3]))
                        {

                            IsAsc = 2;
                        }


                    }

                }





                //if (EnablePOI && IsPOI)
                //{

                //    res = (from a in AppVars.listofPOI

                //           where (a.FullAddress.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))
                //           select a.FullAddress
                //                       ).Take(1000).ToArray<string>();
                //}
                //else
                //{


                    if (IsAsc == 1)
                    {

                        res = (from a in AppVars.listOfAddress

                               where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                               orderby a.PostalCode

                               select a.AddressLine1

                                       ).Take(1000).ToArray<string>();


                    }
                    else if (IsAsc == 2)
                    {
                        res = (from a in AppVars.listOfAddress

                               where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                               orderby a.PostalCode descending

                               select a.AddressLine1

                                       ).Take(1000).ToArray<string>();


                    }
                    else
                    {
                        //  ISingleResult<stp_GetAddressesResult> result = (new TaxiDataContext()).stp_GetAddresses(street, postCode);
                        //   res = result.Select(c => c.AddressLine1).ToArray<string>();


                        res = (from a in AppVars.listOfAddress

                               where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))



                               select a.AddressLine1

                                       ).Take(1000).ToArray<string>();


                    }


                    if (UseGoogleMap && res.Count() == 0)
                    {


                        string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + searchTxt + ", UK&sensor=false";

                        wc.CancelAsync();

                        wc = new WebClient();
                        wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                        wc.DownloadStringAsync(new Uri(url));


                        return;

                    }
              //  }

                ShowAddresses();

            }
            catch (Exception ex)
            {


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

                       where elm.Name == "formatted_address"
                       select elm.Value).ToArray<string>();


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



            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            ShowMap();
        }


        private void ShowMap()
        {

            if (txtAddress.Text != "")
            {
                frmAddressMap map = new frmAddressMap(txtAddress.Text.ToStr());
                map.StartPosition = FormStartPosition.CenterScreen;
                map.ShowDialog();
                map.Dispose();
            }
            else
            {
                ENUtils.ShowMessage("Required: Address");
            }
        }

        private void frmViaPoints_Load(object sender, EventArgs e)
        {
            SetFocus();
        }

        private void frmViaPoints_Shown(object sender, EventArgs e)
        {
            pnlTop.Focus();
            SendKeys.Send("{TAB}");
         //   SetFocus();
        }

        private void SetFocus()
        {
            txtAddress.Focus();
            txtAddress.Select();


        }

        private void frmSearchAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home)
            {

                ShowMap();
            }
        }
        
    }
}
