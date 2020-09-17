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
using Taxi_Model;
using Telerik.WinControls.UI;
using Utils;
using System.Threading;
using System.Diagnostics;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;

using Taxi_AppMain.Classes;

using System.Net;
using System.Xml;
using Telerik.WinControls.Enumerations;
using System.IO;
using System.Collections;
using Taxi_AppMain.Forms;

using Telerik.WinControls.Primitives;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using DotNetCoords;
using System.Data.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Linq.Expressions;
using System.Reflection;
using System.Net.Mail;

namespace Taxi_AppMain
{
    public partial class frmJobAcceptor : UI.SetupBase
    {
        PoolBooking objBooking=null;

        public frmJobAcceptor(PoolBooking obj)
        {
            InitializeComponent();
           
            grdAcceptorPooljobs.CellDoubleClick += new GridViewCellEventHandler(grdAcceptorPooljobs_CellDoubleClick);
            this.Load += new EventHandler(frmJobAcceptor_Load);
            btnAccept.Click += new EventHandler(btnAccept_Click);
            btnDecline.Click += new EventHandler(btnDecline_Click);


            objBooking = obj;

        }
        System.Windows.Forms.Timer timer1 = null;




        void btnAccept_Click(object sender, EventArgs e)
        {

            try
            {

                if (grdAcceptorPooljobs.Rows[0].Cells["Bid"].Value.ToDecimal() < grdAcceptorPooljobs.Rows[0].Cells["OfferPrice"].Value.ToDecimal())
                {
                    MessageBox.Show("You cannot Bid on less price than Job Price " + grdAcceptorPooljobs.Rows[0].Cells["OfferPrice"].Value.ToDecimal());

                }
                else
                {


                    string serverip = string.Empty;
                    using (TaxiDataContext db = new TaxiDataContext("Data Source=213.171.197.98,58416;Initial Catalog=Invoicing;User ID=inv;Password=inv;Trusted_Connection=False;"))
                    {

                        serverip = db.ExecuteQuery<string>("select staticip from gen_client where defaultclientid='" + objBooking.DefaultClientId + "'").FirstOrDefault();

                    }


                    string msg = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objBooking);
                    General.SendSockMessage(("request updatebid>>" + objBooking.ClientBookingId + ">>" + grdAcceptorPooljobs.Rows[0].Cells["Bid"].Value.ToDecimal() + ">>2>>" + AppVars.objSubCompany.CompanyName.ToStr() +">>"+AppVars.objPolicyConfiguration.DefaultClientId.ToStr()+">>" + msg), serverip, 1106);
                    Close();
                
                
                }
                
            }
            catch
            {


            }
        }

        void btnDecline_Click(object sender, EventArgs e)
        {
            try
            {

                string serverip = string.Empty;
                using (TaxiDataContext db = new TaxiDataContext("Data Source=213.171.197.98,58416;Initial Catalog=Invoicing;User ID=inv;Password=inv;Trusted_Connection=False;"))
                {

                    serverip = db.ExecuteQuery<string>("select staticip from gen_client where defaultclientid='" + objBooking.DefaultClientId + "'").FirstOrDefault();

                }
                string msg = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objBooking);
                General.SendSockMessage(("request updatebid>>" + objBooking.ClientBookingId + ">>" + grdAcceptorPooljobs.Rows[0].Cells["Bid"].Value.ToDecimal() + ">>3>>" + AppVars.objSubCompany.CompanyName.ToStr()  + ">>" + AppVars.objPolicyConfiguration.DefaultClientId.ToStr() + ">>" + msg), serverip, 1106);

                Close();
            }
            catch
            {


            }
        }

       

        void frmJobAcceptor_Load(object sender, EventArgs e)
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = (60000 * 5);
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;
            timer1.Start();


            FormateGrid();
            PopulateJobsPool();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        void grdAcceptorPooljobs_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
           
        }


        private void FormateGrid()
        {
            try
            {
                grdAcceptorPooljobs.ShowGroupPanel = false;
                grdAcceptorPooljobs.ShowGroupedColumns = false;
                grdAcceptorPooljobs.AllowAddNewRow = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.HeaderText = "Ref #";
                col.Name = "RefNumber";
                col.Width = 70;
                col.ReadOnly = true;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "PickupDateTime";
                col.Name = "PickupDateTime";
                col.IsVisible = false;
                col.Width = 140;
                col.ReadOnly = true;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Date/Time";
                col.Name = "PickUpDate";
                col.Width = 140;
                col.ReadOnly = true;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Time";
                col.Name = "Time";
                col.ReadOnly = true;
                col.IsVisible = false;
                col.Width = 45;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Passenger";
                col.Name = "Passenger";
                col.ReadOnly = true;
                col.Width = 90;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "From";
                col.ReadOnly = true;
                col.Name = "From";
                col.Width = 160;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "To";
                col.ReadOnly = true;
                col.Name = "To";
                col.Width = 160;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Vehicle";
                col.ReadOnly = true;
                col.Name = "Vehicle";
                col.Width = 90;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Account";
                col.Name = "Account";
                col.ReadOnly = true;
                col.Width = 65;
                col.IsVisible = true;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Notes";
                col.Name = "Notes";
                col.Width = 90;
                grdAcceptorPooljobs.Columns.Add(col);

                GridViewDecimalColumn col2 = new GridViewDecimalColumn();
                col2.HeaderText = "Offer Price";
                col2.Name = "OfferPrice";
                col2.DecimalPlaces = 2;
                col2.FormatString = "{0:#,###0.00}";
                col2.Maximum = 10000;
                col2.Minimum = 0;
                col2.Width = 80;
                col2.ReadOnly = true;
                grdAcceptorPooljobs.Columns.Add(col2);


                 col2 = new GridViewDecimalColumn();
                col2.HeaderText = "Bid";
                col2.Name = "Bid";
                col2.DecimalPlaces = 2;
                col2.ReadOnly = false;
                col2.FormatString = "{0:#,###0.00}";
                col2.Maximum = 10000;
                col2.Minimum = 0;
                col2.Width = 63;
                col2.ReadOnly = false;
                grdAcceptorPooljobs.Columns.Add(col2);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Provider";
                col.Name = "Provider";
                col.Width = 80;
                col.IsVisible = false;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Status";
                col.Name = "Status";
                col.Width = 70;
                col.IsVisible = false;
                grdAcceptorPooljobs.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Driver No";
                col.Name = "DriverNo";
                col.Width = 65;
                col.IsVisible = false;
                grdAcceptorPooljobs.Columns.Add(col);

                //col = new GridViewTextBoxColumn();
                //col.HeaderText = "Accept";
                //col.Name = "Accept";
                //col.Width = 55;
                //grdAcceptorPooljobs.Columns.Add(col);

                grdAcceptorPooljobs.ShowRowHeaderColumn = false;


            }
            catch (Exception ex)
            {

            }
        }

       

        private void PopulateJobsPool()
        {
            try
            {

               var row=   grdAcceptorPooljobs.Rows.AddNew();


                //for (int i = 0; i < jobpool.Count; i++)
                //{

               string vehicle = string.Empty;
               using (TaxiDataContext db = new TaxiDataContext())
               {
                  vehicle= db.Fleet_VehicleTypes.FirstOrDefault(c => c.Id == objBooking.VehicleTypeId.ToInt()).DefaultIfEmpty().VehicleType.ToStr();

               }

               row.Cells["RefNumber"].Value = objBooking.BookingNo;
;
                 //   row.Cells["PickupDateTime"].Value = objBooking.PickupDateTime;
                    row.Cells["PickUpDate"].Value = objBooking.PickupDateTime;
             //       row.Cells["Time"].Value = objBooking.Time;
                    row.Cells["Passenger"].Value = objBooking.CustomerName;
                    row.Cells["From"].Value = objBooking.FromAddress;
                    row.Cells["To"].Value = objBooking.ToAddress;
                    row.Cells["Vehicle"].Value = vehicle;

                    row.Cells["Account"].Value = objBooking.CompanyCreditCardDetails;

                 //   row.Cells["Account"].Value = objBooking.Account;
                    row.Cells["Notes"].Value = objBooking.SpecialRequirements.ToStr();
                    row.Cells["OfferPrice"].Value = objBooking.FareRate.ToDecimal();
                    row.Cells["Bid"].Value = objBooking.FareRate.ToDecimal();
                    row.Cells["Provider"].Value = objBooking.ClientName.ToStr();

                    this.FormTitle ="Job Request By : "+ objBooking.ClientName.ToStr();

                  //  row.Cells["Status"].Value = objBooking.Status;
                   // row.Cells["DriverNo"].Value = objBooking.DriverNo;
                  //  row.Cells["Accept"].Value = objBooking.Accept;

               // }

                //var jobpool = (from a in this.objBooking
                //               select new
                //               {
                //                   Id = a.Id,
                //                   RefNumber = a.BookingNo,
                //                   PickupDateTemp = a.PickupDateTime,
                //                   PickUpDate = string.Format("{0:dd/MM/yyyy}", a.PickupDateTime),
                //                   Time = string.Format("{0:HH:mm}", a.PickupDateTime),
                //                   Passenger = a.CustomerName,
                //                   From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                //                   To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                //                   //Fare = a.FareRate,
                //                   Vehicle = a.Fleet_VehicleType.VehicleType,
                //                   Account = a.Gen_Company != null ? a.Gen_Company.CompanyName : "",
                //                   Notes = a.NotesString,
                //                   OfferPrice = a.FareRate,
                //                   Provider = "",
                //                   Status = a.BookingStatus.StatusName,
                //                   DriverNo = a.DriverId > 0 ? a.Fleet_Driver.DriverNo : "",
                //                   Accept = "Accepted"
                //               }).ToList();
               

               // grdAcceptorPooljobs.DataSource = jobpool;

              //  FormatJobPoolsGrid();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        Font font_pooljob = new Font("Tahoma", 8, FontStyle.Regular);

        private void FormatJobPoolsGrid()
        {
            try
            {
                grdAcceptorPooljobs.Font = font_pooljob;

                if (grdAcceptorPooljobs.Columns.Contains("btnDelete"))
                    grdAcceptorPooljobs.Columns["btnDelete"].Width = 60;

                grdAcceptorPooljobs.Columns["PickupDateTemp"].IsVisible = false;
                (grdAcceptorPooljobs.Columns["PickupDateTemp"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
                (grdAcceptorPooljobs.Columns["PickupDateTemp"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";

                grdAcceptorPooljobs.Columns["PickUpDate"].Width = 70;
                grdAcceptorPooljobs.Columns["PickUpDate"].HeaderText = "Date";

                grdAcceptorPooljobs.Columns["Time"].Width = 45;
                grdAcceptorPooljobs.Columns["Time"].HeaderText = "Time";

                grdAcceptorPooljobs.Columns["Id"].IsVisible = false;

                grdAcceptorPooljobs.Columns["OfferPrice"].HeaderText = "Offer Price";
                grdAcceptorPooljobs.Columns["OfferPrice"].Width = 63;

                grdAcceptorPooljobs.Columns["Notes"].HeaderText = "Notes";
                grdAcceptorPooljobs.Columns["Status"].HeaderText = "Status";
 
                grdAcceptorPooljobs.Columns["DriverNo"].HeaderText = "DriverNo";
                grdAcceptorPooljobs.Columns["Status"].Width = 70;
                grdAcceptorPooljobs.Columns["DriverNo"].Width = 65;
                grdAcceptorPooljobs.Columns["Notes"].Width = 90;

                grdAcceptorPooljobs.Columns["RefNumber"].HeaderText = "Ref #";
                grdAcceptorPooljobs.Columns["RefNumber"].Width = 50;

                grdAcceptorPooljobs.Columns["Vehicle"].Width = 80;

                grdAcceptorPooljobs.Columns["Passenger"].Width = 80;

                grdAcceptorPooljobs.Columns["Account"].Width = 80;
                grdAcceptorPooljobs.Columns["Account"].HeaderText = "A/C";

                grdAcceptorPooljobs.Columns["From"].HeaderText = "Pickup Point";
                grdAcceptorPooljobs.Columns["To"].HeaderText = "Destination";
                grdAcceptorPooljobs.Columns["From"].Width = 120;
                grdAcceptorPooljobs.Columns["To"].Width = 120;

                grdAcceptorPooljobs.Columns["Accept"].HeaderText = "Accept";
                grdAcceptorPooljobs.Columns["Accept"].Width = 55;
                grdAcceptorPooljobs.Columns["Provider"].HeaderText = "Provider";
                grdAcceptorPooljobs.Columns["Provider"].Width = 55;
               // grdAcceptorPooljobs.AllowEditRow = true;


            }
            catch
            {


            }

        }

       

    
     
    }
}
