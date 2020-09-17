using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Utils;
using Taxi_Model;
using Telerik.WinControls;
using Taxi_BLL;
using System.Threading;
using System.IO.Ports;
using Taxi_AppMain.Classes;
using System.Reflection;
using System.Diagnostics;

using System.IO;
using System.Net.Sockets;
using System.Net;
using DotNetCoords;

namespace Taxi_AppMain
{
    public partial class frmTransferJobInOffice : Form
    {
        private bool IsFojJob;


        private long _JobId;

        public long JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
        }

        public bool ReDespatchJob = false;

        public frmTransferJobInOffice(Booking booking)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            FillCombo();
         //   this.ddlCommissionType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlCommissionType_SelectedIndexChanged);
         //   numCommissionPercent.ValueChanged += new EventHandler(numCommissionPercent_ValueChanged);
        }

        void numCommissionPercent_ValueChanged(object sender, EventArgs e)
        {
            try
            {
          //      decimal FareRate = lblFareRate.Text.ToDecimal();
          //      decimal Percent = numCommissionPercent.Value.ToDecimal();
                //    peakFares = ((fareVal * item.IncrementPercent.ToDecimal()) / 100);
        //        numTransferJobCommission.Value = (FareRate * Percent / 100);
            }
            catch (Exception ex)
            { 
            
            }
        }

        void ddlCommissionType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                //if (ddlCommissionType.Text.ToStr() == "Percent")
                //{
                //    numCommissionPercent.Enabled = true;
                //    numTransferJobCommission.Value = 0;
                //    numTransferJobCommission.Enabled = false;
                //    //numTransferJobCommission.Value = 0;
                //    //numTransferJobCommission.Minimum = 0;
                //    //numTransferJobCommission.Maximum = 100;
                //}
                //else
                //{
                //    numCommissionPercent.Value = 0;
                //    numCommissionPercent.Enabled = false;
                //    numTransferJobCommission.Enabled = true;
                //    numTransferJobCommission.Value = 0;
                //    numTransferJobCommission.Minimum = 0;
                //    numTransferJobCommission.Maximum = 10000;                
                //}
            }
            catch (Exception ex)
            { 
            
            }

        }



        Booking objBooking = null;
        delegate void UIDel();

        void frmDespatchJob_Load(object sender, EventArgs e)
        {

            try
            {
                lblTransferHeading.Text = "Transfer Job " + objBooking.BookingNo;
                //lblFareRate.Text = objBooking.FareRate.ToStr();
                //if (objBooking.DriverCommissionType.ToStr() == "Percent")
                //{
                //    ddlCommissionType.Text = "Percent";
                //}
                //else
                //{
                //    ddlCommissionType.Text = "Amount";
                //}

     



            }
            catch (Exception ex)
            {


            }
        }





        private void FillCombo()
        {
            try
            {
                ddlThirdPartyCompany.DisplayMember = "CompanyName";
                ddlThirdPartyCompany.ValueMember = "Id";
                ddlThirdPartyCompany.DataSource =General.GetQueryable<Gen_SubCompany>(c=>c.Id!=objBooking.SubcompanyId).Select(args => new { args.Id, args.CompanyName }).ToList();

                ddlThirdPartyCompany.DropDownStyle =  RadDropDownStyle.DropDownList;
                ddlThirdPartyCompany.SelectedIndex = -1;
            }
            catch (Exception ex)
            { }
        }



        private void btnDespatch_Click(object sender, EventArgs e)
        {
            TransferJob();

        }
        private void TransferJob()
        {
            try
            {
                int ThirdPartyCompanyId = ddlThirdPartyCompany.SelectedValue.ToInt();
                if (ThirdPartyCompanyId == 0)
                {
                    ENUtils.ShowMessage("Required : Company to transfer job");
                    return;
                }
               

                string subCompanyName=ddlThirdPartyCompany.Text.Trim();
                


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    Booking obj= db.Bookings.FirstOrDefault(c => c.Id == JobId);


                    if (obj != null)
                    {
                        string OldSubCompanyName=this.objBooking.Gen_SubCompany.DefaultIfEmpty().CompanyName.ToStr();

                        obj.SubcompanyId = ThirdPartyCompanyId;

                        obj.Booking_Logs.Add(new Booking_Log
                        {
                            User = AppVars.LoginObj.UserName.ToStr(),
                            BeforeUpdate = "Transfer From : " + OldSubCompanyName 
                                                       , AfterUpdate="Transfer To : "+subCompanyName, BookingId=JobId, UpdateDate=DateTime.Now});
                        db.SubmitChanges();

                    }
                }

                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_BOOKING_DASHBOARD);

                if (Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBookingsList") > 0)
                {
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingsList") as frmBookingsList).SetRefreshWhenActive("");
                }

                this.Close();

                //}
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }







        private void frmDespatchJob_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }












    }
}
