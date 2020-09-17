using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Utils;
using Telerik.WinControls;
using System.Reflection;
using System.Net;
using System.Threading;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class frmBookingCancelledPopup :Form
    {

        private bool _IsAttachClosing = true;

        public bool IsAttachClosing
        {
            get { return _IsAttachClosing; }
            set { _IsAttachClosing = value; }
        }


        private Booking _ObjBooking = null;

        public Booking ObjBooking
        {
            get { return _ObjBooking; }
            set { _ObjBooking = value; }
        }


        private long _jobId;


       

        public frmBookingCancelledPopup(long jobId)
        {
            InitializeComponent();

        //    this.ListofFetechedJobs = listofJobs;

            this._jobId = jobId;

            FormatGrid();


            this.Shown += new EventHandler(frmBookingCancelledPopup_Shown);
      
        }

        void frmBookingCancelledPopup_Shown(object sender, EventArgs e)
        {
            if (this._jobId > 0)
            {

                this.ObjBooking = General.GetObject<Booking>(c => c.Id == this._jobId);


                if (ObjBooking != null)
                {

                    LoadData();
                    this.FormClosing += new FormClosingEventHandler(frmBookingCancelledPopup_FormClosing);

                    Thread.Sleep(300);
                    if (this.ObjBooking.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.CANCELLED)
                    {

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_CancelBooking(this._jobId, "", AppVars.LoginObj.UserName.ToStr());
                            // db.stp_BookingLog(BookingId, AppVars.LoginObj.UserName.ToStr(), "Job is Cancelled ! Reason : " + reason);
                        }

                    }

                }
            }
        }

        void frmBookingCancelledPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDashBoardBookings();
        }

       
       


        private void RefreshDashBoardBookings()
        {

            try
            {

                if (Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBookingsList") > 0)
                {
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingsList") as frmBookingsList).SetRefreshWhenActive("");
                }


           
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshTodayAndPreData();
            

            }
            catch (Exception ex)
            {


            }
        }


       

    


        private void FormatGrid()
        {


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = "ID";
            col.IsVisible = false;
            col.Name = "ID";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ref #";
            col.Name = "REFNO";
            col.IsVisible = false;
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Passenger";
            col.Name = "PASSENGER";
            col.Width = 100;
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Contact";
            col.Name = "CONTACTNO";
            col.Width = 150;
            col.ReadOnly = true;
            col.WrapText = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "EMAIL"; 
          
            grdLister.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            //col.IsVisible = false;
            //col.Name = "STATUS";
            //grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "BOOKINGTYPEID";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "JOURNEYTYPEID";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "DEFAULTCLIENTID";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle";
            col.Name = "VEHICLE";
            col.Width = 70;
            col.ReadOnly = true;
            col.WrapText = true;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colD = new GridViewDateTimeColumn();
            colD.HeaderText = "Pickup Time";
            colD.Name = "PICKUPDATETIME";
            colD.CustomFormat = "dd/MM/yyyy HH:mm";
            colD.FormatString = "{0:dd/MM/yyyy HH:mm}";
            colD.ReadOnly = false;
            colD.Width = 130;
            colD.WrapText = true;
            grdLister.Columns.Add(colD);



            colD = new GridViewDateTimeColumn();          
            colD.Name = "OLDPICKUPDATETIME";
            colD.CustomFormat = "dd/MM/yyyy HH:mm";
            colD.FormatString = "{0:dd/MM/yyyy HH:mm}";           
            colD.IsVisible=false;          
            grdLister.Columns.Add(colD);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Flight No";
            col.Name = "FlightNo";
            col.Width = 80;
            col.ReadOnly = true;
            col.IsVisible = false;
            col.WrapText = true;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup";
            col.Name = "FROMADDRESS";
            col.ReadOnly = true;
            col.Width = 220;
            col.WrapText = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Via";
            col.Name = "VIA";
            col.IsVisible = false;
            col.Width = 150;
            col.WrapText = true;
            grdLister.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "TOADDRESS";
            col.ReadOnly = true;
            col.WrapText = true;
            col.Width = 220;
            grdLister.Columns.Add(col);



            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Fares";
            colDec.Name = "FARES";
            col.ReadOnly = false;
          //  colDec.IsVisible = true;
            colDec.Width = 70;
            grdLister.Columns.Add(colDec);


            colDec = new GridViewDecimalColumn();
            colDec.Name = "OLDFARES";
            colDec.IsVisible = false;          
            grdLister.Columns.Add(colDec);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Payment";
            col.Name = "PAYMENTTYPE";
            col.Width = 60;
            col.IsVisible = false;
            col.ReadOnly = true;
            col.WrapText = true;
            grdLister.Columns.Add(col);



            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Waiting Time(Mins)";
            colDec.Name = "WaitingTime";
            colDec.IsVisible = false;
            colDec.Width = 150;
            grdLister.Columns.Add(colDec);

            

            //GridViewCommandColumn commandCol = new GridViewCommandColumn();
            //commandCol.UseDefaultText = true;
            //commandCol.DefaultText = "Accept";
            //commandCol.Name = "ACCEPT";
            //commandCol.HeaderText = "";
            //commandCol.Width=60;
            //commandCol.TextAlignment = ContentAlignment.MiddleCenter;

            //grdLister.Columns.Add(commandCol);


            //commandCol = new GridViewCommandColumn();
            //commandCol.UseDefaultText = true;
            //commandCol.DefaultText = "Decline";
            //commandCol.Name = "DECLINE";
            //commandCol.HeaderText = "";
            //commandCol.Width=60;
            //commandCol.TextAlignment = ContentAlignment.MiddleCenter;

            //grdLister.Columns.Add(commandCol);



            //grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdLister.ShowRowHeaderColumn = false;
            grdLister.ShowGroupPanel = false;
            grdLister.AllowAddNewRow = false;


            grdLister.TableElement.RowHeight = 70;
           // grdLister.AllowEditRow = false;
            grdLister.AllowDeleteRow = false;

        }




       
        private void SleepAction()
        {
            System.Threading.Thread.Sleep(1000);

        }

        private void CloseForm()
        {
            this.Close();

        }


   
        private void LoadData()
        {

            try
            {
                grdLister.RowCount = 1;


              
                

                    grdLister.Rows[0].Cells["ID"].Value = this.ObjBooking.Id;
                    grdLister.Rows[0].Cells["REFNO"].Value = this.ObjBooking.BookingNo;
                    grdLister.Rows[0].Cells["PASSENGER"].Value = this.ObjBooking.CustomerName;

                    grdLister.Rows[0].Cells["BOOKINGTYPEID"].Value = this.ObjBooking.BookingTypeId;
                    grdLister.Rows[0].Cells["DEFAULTCLIENTID"].Value = this.ObjBooking.AddBy;


                    if (!string.IsNullOrEmpty(this.ObjBooking.CustomerPhoneNo) && !string.IsNullOrEmpty(this.ObjBooking.CustomerMobileNo))
                        grdLister.Rows[0].Cells["CONTACTNO"].Value = this.ObjBooking.CustomerPhoneNo + "/" + this.ObjBooking.CustomerMobileNo;
           
                    else if (!string.IsNullOrEmpty(this.ObjBooking.CustomerPhoneNo))
                        grdLister.Rows[0].Cells["CONTACTNO"].Value = this.ObjBooking.CustomerPhoneNo;
            
                    else if (!string.IsNullOrEmpty(this.ObjBooking.CustomerMobileNo))
                        grdLister.Rows[0].Cells["CONTACTNO"].Value = this.ObjBooking.CustomerMobileNo;


                    if (!string.IsNullOrEmpty(this.ObjBooking.CustomerEmail))
                    {
                        grdLister.Rows[0].Cells["CONTACTNO"].Value += Environment.NewLine + "Email : " + this.ObjBooking.CustomerEmail;
                        grdLister.Rows[0].Cells["EMAIL"].Value = this.ObjBooking.CustomerEmail;

                    }

                    grdLister.Rows[0].Cells["JOURNEYTYPEID"].Value = this.ObjBooking.JourneyTypeId.ToInt();


                    // Vehicle Type and Payment Type Added on request of Maple Chauffers 14-01-2016
                    grdLister.Rows[0].Cells["VEHICLE"].Value = this.ObjBooking.Fleet_VehicleType.VehicleType.ToStr();
                  //  grdLister.Rows[0].Cells["PAYMENTTYPE"].Value = this.ObjBooking..ToStr();
                    //


                    grdLister.Rows[0].Cells["PICKUPDATETIME"].Value = this.ObjBooking.PickupDateTime;
                    grdLister.Rows[0].Cells["OLDPICKUPDATETIME"].Value = this.ObjBooking.PickupDateTime;


                    grdLister.Rows[0].Cells["FlightNo"].Value = this.ObjBooking.FromDoorNo;


                    if(!string.IsNullOrEmpty(this.ObjBooking.FromStreet.ToStr().Trim()))
                        grdLister.Rows[0].Cells["FlightNo"].Value+= " - " + this.ObjBooking.FromStreet;




                    grdLister.Rows[0].Cells["FROMADDRESS"].Value = this.ObjBooking.FromAddress;
                    grdLister.Rows[0].Cells["TOADDRESS"].Value = this.ObjBooking.ToAddress;

                    grdLister.Rows[0].Cells["FARES"].Value = this.ObjBooking.FareRate.ToDecimal();
                    grdLister.Rows[0].Cells["OLDFARES"].Value = this.ObjBooking.FareRate.ToDecimal();


                


                   grdLister.Columns["FARES"].ReadOnly = true;
                   

                    grdLister.Columns["FROMADDRESS"].Width += 50;
                    grdLister.Columns["TOADDRESS"].Width += 50;
                  
              

            }
            catch (Exception ex)
            {



            }


          
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CloseForm();
        }    
       

       

    


    }
}
