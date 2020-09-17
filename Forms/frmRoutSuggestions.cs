using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using DAL;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using System.Data.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;

using System.Reflection;
using System.Windows;
using System.Xml.Linq;



namespace Taxi_AppMain
{
    public partial class frmRoutSuggestions : Form
    {

        private DateTime? _PickupTime;

        public DateTime? PickupTime
        {
            get { return _PickupTime; }
            set { _PickupTime = value; }
        }





        private bool IsLoaded = false;


        private int _FromLocTypeId;

        public int FromLocTypeId
        {
            get { return _FromLocTypeId; }
            set { _FromLocTypeId = value; }
        }





        int? VehicleId = 0;
        int? CompanyId = 0;





        public frmRoutSuggestions(string Origin, string[] via, string Destination, int? vehicle, int? Company, int fromLocTypeId, DateTime? pickupTime)
        {
            InitializeComponent();
            grdAddress.CellFormatting += new CellFormattingEventHandler(grdAddress_CellFormatting);

            LoadMap(Origin, Destination, via);
            LoadAddressGrid(Origin, Destination, via);


            webBrowser1.ScriptErrorsSuppressed = true;


            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.CellClick += new GridViewCellEventHandler(grdLister_CellClick);
            FormateGride();
            VehicleId = vehicle;
            CompanyId = Company;
            this.FromLocTypeId = fromLocTypeId;
            this.PickupTime = pickupTime;
            this.FormClosed += new FormClosedEventHandler(frmRoutSuggestions_FormClosed);



             objTimer = new System.Windows.Forms.Timer();
            objTimer.Interval = 5000;
            objTimer.Enabled = true;
            objTimer.Tick += new EventHandler(objTimer_Tick);



        }

        System.Windows.Forms.Timer objTimer = null;

        private bool IsYahooFaresLoaded;
        void objTimer_Tick(object sender, EventArgs e)
        {
            if (IsYahooFaresLoaded == false)
            {
                ReLoadYahooMapFares();
            }
        }

        public void ReLoadYahooMapFares()
        {



            string innerHtml = string.Empty;

            innerHtml = webBrowser1.Document.Body.InnerHtml.ToStr();

            string body = string.Empty;

            body = webBrowser1.Document.Body.InnerHtml;

            grdLister.Rows.Clear();

            try
            {
                string htmlText = webBrowser1.Document.Body.InnerHtml.ToStr().ToLower();
                htmlText = htmlText.Replace("\"", "").Trim();

                string html2 = webBrowser1.Document.Body.OuterHtml.ToStr().ToLower();
                int Spoint = htmlText.ToLower().IndexOf("<div class=time yui3-u>");
                int dpoint = htmlText.ToLower().IndexOf("<span class=metric>");
                if (Spoint == -1)
                    return;
               
                int iteration = 0;

                while (Spoint != -1)
                {


                    iteration++;
                    string s = htmlText.Substring(Spoint, dpoint - Spoint);
                    GridViewRowInfo row = null;
                    if (s.Contains("<span class=units>mi</span>"))
                    {
                        IsYahooFaresLoaded = true;
                        string tempStr = s.Substring(s.IndexOf("<span class=imperial>"), s.IndexOf("<span class=units>mi</span>") - s.IndexOf("<span class=imperial>"));
                        tempStr = tempStr.Replace("<span class=imperial>", "").Trim().Replace("<span class=imperial>", "").Trim();
                        decimal miles = tempStr.ToDecimal();
                        tempStr = s.Substring(s.IndexOf("<div class=time yui3-u>"), s.IndexOf("<div class=distance yui3-u>") - s.IndexOf("<div class=ime yui3-u>"));
                        tempStr = tempStr.Replace("<div class=time yui3-u>", "").Trim().Replace("<span class=units>", "").Trim().Replace("</span></div>", "").Trim();
                        string minuts = tempStr.ToStr();

                        htmlText = htmlText.Remove(0, Spoint + s.Length);
                        // int min = rows[i].LastIndexOf(", ");

                        if (htmlText.StartsWith("<span class=metric>"))
                        {
                            htmlText = htmlText.Remove(0, "<span class=metric>".Length).Trim();

                        }

                        // int com = rows[i].IndexOf(",");
                        // //min = min - com;

                        // string miles = rows[i].Substring(0, com).Trim();
                        // string minuts = rows[i].Substring(min, rows[i].Length - min);
                        // minuts = minuts.Replace(",", "").Trim();
                        row = grdLister.Rows.AddNew();
                        row.Cells["miles"].Value = miles;
                        row.Cells["Miniuts"].Value = minuts;


                        int? companyID = 0;
                        var objFare = new TaxiDataContext().stp_CalculateGeneralFares(VehicleId, companyID, miles, this.PickupTime);
             
                        if (objFare != null)
                        {
                            var f = objFare.FirstOrDefault();

                            if ((f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                            {
                              

                                decimal fareVal = f.totalFares.ToDecimal();

                                decimal dd;
                                if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                                {

                                    fareVal = Math.Ceiling(fareVal);
                                }


                                if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool() == false)
                                {
                                    dd = fareVal.ToDecimal();
                                }
                                else
                                {
                                    string ff = string.Format("{0:#}", fareVal);
                                    if (ff == string.Empty)
                                        ff = "0";

                                    dd = ff.ToDecimal();
                                }

                                // Add Airport Pickup Charges If Pickup Point is From Airport...
                                if (this.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                                    dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
                                row.Cells["Fares"].Value = dd;
                            }
                        }

                    }
                    else
                    {
                        htmlText = htmlText.Replace(s, "");

                    }

                    Spoint = htmlText.IndexOf("<div class=time yui3-u>");
                    dpoint = htmlText.IndexOf("<span class=metric>");


                    while (dpoint - Spoint < 0)
                    {

                        Spoint = htmlText.IndexOf("<div class=time yui3-u>");
                        dpoint = htmlText.IndexOf("<span class=metric>");



                        if (dpoint - Spoint < 0)
                        {
                            int len = "<span class=metric>".Length;

                            htmlText = htmlText.Remove(0, dpoint + len);
                        }


                        Spoint = htmlText.IndexOf("<div class=time yui3-u>");
                        dpoint = htmlText.IndexOf("<span class=metric>");
                    }

                    if (iteration == 10)
                        break;
                }
                if (grdLister.Rows.Count > 0)
                {
                    grdLister.CurrentRow = grdLister.Rows[0];
                }
            }
            catch (Exception ex)
            {
            }

        }





        void frmRoutSuggestions_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                webBrowser1.Visible = false;
                this.WindowState = FormWindowState.Minimized;




                grdLister.Font.Dispose();
                grdLister.Dispose();
                // this.Dispose(true);



                webBrowser1.Controls.Clear();
                webBrowser1.Dispose();


                if (objTimer != null)
                {
                    objTimer.Stop();
                    objTimer.Dispose();

                }

                GC.Collect();

            }
            catch (Exception ex)
            {


            }
        }


        void grdAddress_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "Address")
            {

                e.CellElement.TextWrap = true;
            }
        }

        void grdLister_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {


                if (e.Row != null && e.Row is GridViewDataRowInfo)
                {
                    //   yui_3_18_1_4_1430521781421_6239
                    string min = grdLister.CurrentRow.Cells["miles"].Value.ToString();

                    HtmlElement[] list = webBrowser1.Document.GetElementsByTagName("div").OfType<HtmlElement>().ToArray<HtmlElement>();
                    foreach (var item in list)
                    {
                        if (item.InnerHtml != null && item.InnerHtml.Contains(min))
                        {


                            item.InvokeMember("click");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
            //try
            //{
            //    if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            //    {
            //        this.SelectedFares = grdLister.CurrentRow.Cells["Fares"].Value.ToDecimal();
            //        this.SeletedMiles = grdLister.CurrentRow.Cells["miles"].Value.ToString();
            //        this.SelectedTime = grdLister.CurrentRow.Cells["Miniuts"].Value.ToString();
            //    }
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowErrorMessage(ex.Message);
            //}
        }

        private void LoadAddressGrid(string Origin, string Destination, string[] via)
        {
            try
            {

                grdAddress.TableElement.RowHeight = 50;

                int unicode = 65;
                char character = (char)unicode;

                DataTable table = new DataTable("States");

                table.Columns.Add(new DataColumn("VAl", typeof(string)));
                table.Columns.Add(new DataColumn("Address", typeof(string)));

                table.Rows.Add("A", Origin);



                for (int i = 0; i < via.Count(); i++)
                {
                    unicode++;
                    character = (char)unicode;

                    table.Rows.Add(character, via[i]);
                }

                unicode++;
                character = (char)unicode;


                table.Rows.Add(character, Destination);

                grdAddress.DataSource = table;

                grdAddress.ShowGroupPanel = false;
                grdAddress.ShowGroupedColumns = false;
                grdAddress.AllowAddNewRow = false;
                grdAddress.Columns["VAl"].Width = 50;
                grdAddress.Columns["VAl"].HeaderText = "";
                grdAddress.Columns["Address"].Width = 330;
            }
            catch (Exception ex)
            {
            }
        }
        private void FormateGride()
        {
            try
            {
                grdLister.ShowGroupPanel = false;
                grdLister.ShowGroupedColumns = false;
                grdLister.AllowAddNewRow = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.HeaderText = "Time";
                col.Name = "Miniuts";
                col.Width = 130;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.HeaderText = "Miles";
                col.Name = "miles";
                col.Width = 120;
                grdLister.Columns.Add(col);

                GridViewDecimalColumn col2 = new GridViewDecimalColumn();
                col2.HeaderText = "Price";
                col2.Name = "Fares";
                col2.DecimalPlaces = 2;
                col2.FormatString = "{0:#,###0.00}";
                col2.Maximum = 10000;
                col2.Minimum = 0;
                col2.Width = 120;
                grdLister.Columns.Add(col2);

                grdLister.ShowRowHeaderColumn = false;


            }
            catch (Exception ex)
            {

            }
        }


        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {


            string innerHtml = webBrowser1.Document.Body.InnerHtml;

            string body = webBrowser1.Document.Body.InnerHtml;

            grdLister.Rows.Clear();
            try
            {

                string htmlText = webBrowser1.Document.Body.InnerHtml.ToStr().ToLower();

                htmlText = htmlText.Replace("\"", "").Trim();

                int Spoint = htmlText.ToLower().IndexOf("<div class=time yui3-u>");
                int dpoint = htmlText.ToLower().IndexOf("<span class=metric>");



                if (Spoint == -1)
                    return;




                // Fails saying google is unexpected token!
                //   var input = XElement.Parse(test);


                //foreach (KeyValuePair<string, string> item in input)
                //    Console.WriteLine("Key: {0,15} Value: {1}", item.Key, item.Value);



                int iteration = 0;

                while (Spoint != -1)
                {

                    iteration++;



                    string s = htmlText.Substring(Spoint, dpoint - Spoint);
                    GridViewRowInfo row = null;


                    if (s.Contains("<span class=units>mi</span>"))
                    {
                        IsYahooFaresLoaded = true;
                        string tempStr = s.Substring(s.IndexOf("<span class=imperial>"), s.IndexOf("<span class=units>mi</span>") - s.IndexOf("<span class=imperial>"));
                        tempStr = tempStr.Replace("<span class=imperial>", "").Trim().Replace("<span class=imperial>", "").Trim();
                        decimal miles = tempStr.ToDecimal();


                        tempStr = s.Substring(s.IndexOf("<div class=time yui3-u>"), s.IndexOf("<div class=distance yui3-u>") - s.IndexOf("<div class=ime yui3-u>"));
                        tempStr = tempStr.Replace("<div class=time yui3-u>", "").Trim().Replace("<span class=units>", "").Trim().Replace("</span></div>", "").Trim();
                        string minuts = tempStr.ToStr();

                        htmlText = htmlText.Remove(0, Spoint + s.Length);
                        // int min = rows[i].LastIndexOf(", ");

                        if (htmlText.StartsWith("<span class=metric>"))
                        {
                            htmlText = htmlText.Remove(0, "<span class=metric>".Length).Trim();

                        }

                        // int com = rows[i].IndexOf(",");
                        // //min = min - com;

                        // string miles = rows[i].Substring(0, com).Trim();
                        // string minuts = rows[i].Substring(min, rows[i].Length - min);
                        // minuts = minuts.Replace(",", "").Trim();
                        row = grdLister.Rows.AddNew();
                        row.Cells["miles"].Value = miles;
                        row.Cells["Miniuts"].Value = minuts;


                        int? companyID = 0;
                        var objFare = new TaxiDataContext().stp_CalculateGeneralFares(VehicleId, companyID, miles, this.PickupTime);
                        if (objFare != null)
                        {
                            var f = objFare.FirstOrDefault();

                            if ((f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                            {
                                decimal fareVal = f.totalFares.ToDecimal();

                                decimal dd;
                                if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                                {

                                    fareVal = Math.Ceiling(fareVal);
                                }


                                if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool() == false)
                                {
                                    dd = fareVal.ToDecimal();
                                }
                                else
                                {
                                    string ff = string.Format("{0:#}", fareVal);
                                    if (ff == string.Empty)
                                        ff = "0";

                                    dd = ff.ToDecimal();
                                }

                                // Add Airport Pickup Charges If Pickup Point is From Airport...
                                if (this.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                                    dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();




                                row.Cells["Fares"].Value = dd;






                            }
                        }

                    }
                    else
                    {
                        htmlText = htmlText.Replace(s, "");


                    }

                    Spoint = htmlText.IndexOf("<div class=time yui3-u>");
                    dpoint = htmlText.IndexOf("<span class=metric>");


                    while (dpoint - Spoint < 0)
                    {

                        Spoint = htmlText.IndexOf("<div class=time yui3-u>");
                        dpoint = htmlText.IndexOf("<span class=metric>");



                        if (dpoint - Spoint < 0)
                        {
                            int len = "<span class=metric>".Length;

                            htmlText = htmlText.Remove(0, dpoint + len);
                        }


                        Spoint = htmlText.IndexOf("<div class=time yui3-u>");
                        dpoint = htmlText.IndexOf("<span class=metric>");
                    }


                    if (iteration == 10)
                        break;

                }


                if (grdLister.Rows.Count > 0)
                {
                    grdLister.CurrentRow = grdLister.Rows[0];
                }

                //int a = 0;
                //if (rows.Count() > 0)
                //{

                //    GridViewRowInfo row = null;
                //    for (int i = 0; i < rows.Count(); i++)
                //    {

                //        if (rows[i].Contains("mi,"))
                //        {

                //            int mi = rows[i].IndexOf("mi,");
                //            int min = rows[i].LastIndexOf(", ");



                //            int com = rows[i].IndexOf(",");
                //            //min = min - com;

                //            string miles = rows[i].Substring(0, com).Trim();
                //            string minuts = rows[i].Substring(min, rows[i].Length - min);
                //            minuts =  minuts.Replace(",", "").Trim();
                //            row = grdLister.Rows.AddNew();
                //            row.Cells["miles"].Value = miles;
                //            row.Cells["Miniuts"].Value = minuts;


                //            int? companyID = 0;
                //            ISingleResult<ClsFares> objFare = General.SP_CalculateFares(VehicleId.ToIntorNull(), companyID.ToIntorNull(), miles.Replace("mi", String.Empty).ToStr(),this.PickupTime);
                //            if (objFare != null)
                //            {
                //                ClsFares f = objFare.FirstOrDefault();

                //                if ((f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                //                {
                //                    decimal fareVal = f.totalFares;

                //                    decimal dd;
                //                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                //                    {

                //                        fareVal = Math.Ceiling(fareVal);
                //                    }


                //                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool() == false)
                //                    {
                //                        dd = fareVal.ToDecimal();
                //                    }
                //                    else
                //                    {
                //                        string ff = string.Format("{0:#}", fareVal);
                //                        if (ff == string.Empty)
                //                            ff = "0";

                //                        dd = ff.ToDecimal();
                //                    }

                //                    // Add Airport Pickup Charges If Pickup Point is From Airport...
                //                    if (this.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT )
                //                        dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();




                //                    row.Cells["Fares"].Value = dd;






                //                }
                //            }

                //        }



                //    }
                //    if (grdLister.Rows.Count > 0)
                //    {
                //        grdLister.CurrentRow=grdLister.Rows[0];
                //    }
                //}
                //     lblLoading.Visible = false;


            }
            catch (Exception ex)
            {
            }
        }

        private void LoadMap(string Origin, string Destination, string[] via)
        {
            try
            {
                string originAddress = Origin;
                string destinationAddress = Destination;

                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                {

                    Origin = General.GetPostCodeMatch(Origin.ToStr().ToUpper());
                    Destination = General.GetPostCodeMatch(Destination.ToStr().ToUpper());

                }


                if (string.IsNullOrEmpty(Origin))
                {
                    Origin = originAddress;

                }
                else
                {

                    Origin += ", UK";

                }



                if (string.IsNullOrEmpty(Destination))
                {
                    Destination = destinationAddress;
                }
                else
                {
                    Destination += ", UK";
                }




                string ViaLoc = "";



                for (int i = 0; i < via.Count(); i++)
                {


                    if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                    {
                        destinationAddress = General.GetPostCodeMatch(via[i].ToStr().ToUpper());
                    }
                    else
                    {
                        destinationAddress = via[i].ToStr().ToUpper();
                    }

                    if (string.IsNullOrEmpty(destinationAddress))
                    {
                        destinationAddress = via[i].ToStr().ToUpper();

                    }
                    else
                    {
                        destinationAddress += ", UK";

                    }

                    ViaLoc += "&w" + i.ToStr() + "=" + destinationAddress;
                }



                string Url = "";


                //if (!string.IsNullOrEmpty(ViaLoc))
                //    Url = "https://maps.google.com/maps?source=s_d&saddr=" + Origin + ViaLoc + "+&daddr=" + Destination;
                //else
                Url = "  https://maps.yahoo.com/directions/?o=" + Origin.ToStr().Replace(" ", "+") + "&" + "&d=" + Destination.ToStr().Replace(" ", "+") + ViaLoc.Replace(" ", "+");


                //if(!string.IsNullOrEmpty(ViaLoc))
                // Url=   "https://maps.google.com/maps?source=s_d&saddr=" + Origin  + ViaLoc + "+&daddr=" + Destination;
                //else
                //    Url = "https://maps.google.com/maps?source=s_d&saddr=" + Origin+ "+&daddr=" + Destination;


                //string ieVersion = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer").GetValue("Version").ToStr();


                //ieVersion = ieVersion.Substring(0, ieVersion.IndexOf('.')).Trim();

                //if (ieVersion.ToDecimal() >= 9)
                //{

                //    webBrowser1.Navigate(Url, null, null, "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0)");


                //}
                //else
                //{


                webBrowser1.Navigate(Url);
                //    webBrowser1.Navigate("https://maps.google.co.uk/maps/myplaces?source=s_d&saddr=PENTAX+H+SOUTH+HILL+AVENUE+HARROW+HA2+0DU,+UK+&daddr=HEATHROW+TERMINAL+4,+TW6+2GA,+UK&dg=feature");

                // webBrowser1.Navigate(Url);

                //  }


                //CheckMiles();
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void frmRoutSuggestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

        }
        private decimal _SelectedFares;

        public decimal SelectedFares
        {
            get { return _SelectedFares; }
            set { _SelectedFares = value; }
        }
        private string _SeletedMiles;

        public string SeletedMiles
        {
            get { return _SeletedMiles; }
            set { _SeletedMiles = value; }
        }

        private string _SelectedTime;

        public string SelectedTime
        {
            get { return _SelectedTime; }
            set { _SelectedTime = value; }
        }



        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    this.SelectedFares = grdLister.CurrentRow.Cells["Fares"].Value.ToDecimal();
                    this.SeletedMiles = grdLister.CurrentRow.Cells["miles"].Value.ToString();
                    this.SelectedTime = grdLister.CurrentRow.Cells["Miniuts"].Value.ToString();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void btngetDirection_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlElement loBtn = (HtmlElement)webBrowser1.Document.GetElementById("d_sub");
                loBtn.InvokeMember("click");
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void grdAddress_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }


    }

}
