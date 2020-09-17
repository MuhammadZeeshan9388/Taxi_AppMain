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
using System.IO;

namespace Taxi_AppMain
{
    public partial class frmAuthorizeAutoDespAllocDrvs :Form
    {

        private bool IsFormOpened = false;


      


      
     
        public string jobs;
        public string newjobs;
        public frmAuthorizeAutoDespAllocDrvs(string jobIds)
        {
            InitializeComponent();

             this.jobs=jobIds;
             this.newjobs = jobIds;
            FormatGrid();

            this.Load += new EventHandler(frmFetchedOnlineBookings_Load);

            this.FormClosing += new FormClosingEventHandler(frmFetchedOnlineBookings_FormClosing);

          //  grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            this.Shown += new EventHandler(frmAuthorizeAutoDespAllocDrvs_Shown);
        }

        void frmAuthorizeAutoDespAllocDrvs_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

    
       

        void frmFetchedOnlineBookings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (IsAttachClosing)
            //{

                //if (grdLister.Rows.Count > 0)
                // {
                //    AppVars.IsLogout = false;
                //    ENUtils.ShowMessage("You cannot close it until you Authorized All Bookings");

                //    e.Cancel = true;
                //}
                //else
                //{
                    //RefreshDashBoardBookings();



                    timer1.Stop();
                    timer1.Dispose();

                    this.Dispose(true);
                 
          //      }
          //  }
           

        }

        private void RefreshDashBoardBookings()
        {

            try
            {

                //if (Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBookingsList") > 0)
                //{
                //    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingsList") as frmBookingsList).SetRefreshWhenActive();
                //}

                //(Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshTodayAndPreData();
            
                //new BroadcasterData().BroadCastToAll("**close authorize web>>" + Environment.MachineName);

                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ACTIVEBOOKINGS_DASHBOARD);

            }
            catch 
            {


            }
        }


       

        void frmFetchedOnlineBookings_Load(object sender, EventArgs e)
        {
            LoadData();            

            PlaySoundNotification("Message1.wav", false);
        }



        private void PlaySoundNotification(string soundFileName, bool looping)
        {
            if (IsFormOpened == false)
            {
                try
                {
                    using (System.Media.SoundPlayer spMessaging = new System.Media.SoundPlayer())
                    {


                        spMessaging.SoundLocation = System.Windows.Forms.Application.StartupPath + "\\sound\\" + soundFileName;


                        if (File.Exists(spMessaging.SoundLocation))
                        {

                            if (looping)
                                spMessaging.PlayLooping();

                            else
                                spMessaging.Play();
                        }


                    }

                    IsFormOpened = true;

                    //spMessaging.Dispose();
                }
                catch
                {


                }
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
            col.HeaderText = "ONLINEBOOKINGID";
            col.IsVisible = false;
            col.Name = "ONLINEBOOKINGID";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Driver";
            col.Name = "Driver";
            col.Width = 100;
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ref #";
            col.Name = "REFNO";
            col.Width = 110;
            col.IsVisible = true;
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

               

            GridViewDateTimeColumn colD = new GridViewDateTimeColumn();
            colD.HeaderText = "Pickup Time";
            colD.Name = "PICKUPDATETIME";
            colD.CustomFormat = "dd/MM/yyyy HH:mm";
            colD.FormatString = "{0:dd/MM/yyyy HH:mm}";
            colD.ReadOnly = false;
            colD.Width = 160;
            colD.WrapText = true;
            grdLister.Columns.Add(colD);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup";
            col.Name = "FROMADDRESS";
            col.ReadOnly = true;
            col.Width = 330;
            col.WrapText = true;
            grdLister.Columns.Add(col);
       

            GridViewCommandColumn commandCol = new GridViewCommandColumn();
            commandCol.UseDefaultText = true;
            commandCol.DefaultText = "Allow";
            commandCol.Name = "ACCEPT";
            commandCol.HeaderText = "";
            commandCol.Width=80;
            commandCol.TextAlignment = ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(commandCol);


            GridViewCommandColumn commandDenyCol = new GridViewCommandColumn();
            commandDenyCol.UseDefaultText = true;
            commandDenyCol.DefaultText = "Deny";
            commandDenyCol.Name = "DENY";
            commandDenyCol.HeaderText = "";
            commandDenyCol.Width = 70;
            commandDenyCol.TextAlignment = ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(commandDenyCol);        

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdLister.ShowRowHeaderColumn = false;
            grdLister.ShowGroupPanel = false;
            grdLister.AllowAddNewRow = false;


            grdLister.TableElement.RowHeight = 60;       
            grdLister.AllowDeleteRow = false;

        }




        void grdLister_CommandCellClick(object sender, EventArgs e)
        {

            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();


                    GridViewRowInfo row = gridCell.GridControl.CurrentRow;



                    if (row != null)
                    {

                        if (name == "accept")
                        {

                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                db.stp_AuthUnavailAllocDrvs(row.Cells["ONLINEBOOKINGID"].Value.ToLong(), "", AppVars.LoginObj.UserName.ToStr());

                            }


                        }
                        else
                        {
                            if (AppVars.denyAllocatedBookingIds.ToStr().Trim().Length == 0)
                            {
                                AppVars.denyAllocatedBookingIds += "," + row.Cells["ONLINEBOOKINGID"].Value.ToLong() + ",";
                            }
                            else
                            {
                                AppVars.denyAllocatedBookingIds += row.Cells["ONLINEBOOKINGID"].Value.ToLong() + ",";

                            }


                        }

                        row.Delete();

                        SleepAction();

                        if (name == "accept")
                        {

                            RefreshDashBoardBookings();
                        }


                        if (grdLister.Rows.Count == 0)
                        {

                            CloseForm();
                        }


                    }
                
            }
            catch (Exception ex)
            {

              //  ENUtils.ShowMessage(ex.Message);
            }


        }

        private void SleepAction()
        {
            System.Threading.Thread.Sleep(500);

        }

        private void CloseForm()
        {
            try
            {
                timer1.Dispose();
                if (worker != null)
                {

                    worker.CancelAsync();
                    worker.Dispose();
                    worker = null;
                    GC.Collect();

                }
            }
            catch
            {


            }
            this.Close();

        }










        BackgroundWorker worker = null;
       
        public void LoadData()
        {

            try
            {
                if (worker != null && worker.IsBusy)
                    return;

                if (grdLister.Rows.Count > 0 && newjobs==jobs)
                    return;

                jobs = newjobs;

                grdLister.Rows.Clear();



                if (jobs.ToStr().Trim().Length > 0)
                {
                    if (worker == null)
                    {
                        worker = new BackgroundWorker();
                        worker.WorkerSupportsCancellation = true;
                        worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    }

                    worker.RunWorkerAsync();

               
                }


              

            }
            catch 
            {



            }          
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {


                if (listofResults.Count == 0)
                {
                    CloseForm();

                }
                else
                {

                    grdLister.RowCount = listofResults.Count;

                    for (int i = 0; i < grdLister.RowCount; i++)
                    {


                        grdLister.Rows[i].Cells["ID"].Value = listofResults[i].DriverId;
                        grdLister.Rows[i].Cells["ONLINEBOOKINGID"].Value = listofResults[i].Id;


                        grdLister.Rows[i].Cells["REFNO"].Value = listofResults[i].BookingNo.ToStr();
                        grdLister.Rows[i].Cells["Driver"].Value = listofResults[i].DriverNo.ToStr();



                        grdLister.Rows[i].Cells["PICKUPDATETIME"].Value = listofResults[i].PickupDateTime;
                        grdLister.Rows[i].Cells["FROMADDRESS"].Value = listofResults[i].FromAddress.ToStr();

                    }
                }
            }
            catch
            {


            }

        }


        List<stp_GetBookingAllocatedDrvDetailsResult> listofResults = new List<stp_GetBookingAllocatedDrvDetailsResult>();
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listofResults.Clear();




                using (TaxiDataContext db = new TaxiDataContext())
                {

                    foreach (var item in jobs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {

                        if (AppVars.denyAllocatedBookingIds.ToStr().Length ==0 || AppVars.denyAllocatedBookingIds.ToStr().Contains("," + item.ToStr() + ",") == false)
                        {
                            var job = db.stp_GetBookingAllocatedDrvDetails(item.ToLong(), "").FirstOrDefault();
                            listofResults.Add(job);


                        }
                    }
                }



                


            }
            catch
            {


            }
        }      

       

        private void btnAcceptAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to ALLOW All Bookings?", "Authorization", MessageBoxButtons.YesNo))
                {
                    int rowCount = grdLister.Rows.Count;


                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        for (int i = 0; i < rowCount; i++)
                        {

                            db.stp_AuthUnavailAllocDrvs(grdLister.Rows[i].Cells["ONLINEBOOKINGID"].Value.ToLong(), "", AppVars.LoginObj.UserName.ToStr());
                        }

                    }

                    grdLister.Rows.Clear();

                    RefreshDashBoardBookings();

                    CloseForm();
                }


            }
            catch 
            {
               


            }
        }


        

    

        private void btnDeclineAll_Click(object sender, EventArgs e)
        {
           
        }


       




        private void timer1_Tick(object sender, EventArgs e)
        {
            txtTimer.Text = (txtTimer.Text.ToInt() - 1).ToStr();


            if (newjobs.ToStr().Trim().Length == 0 || txtTimer.Text.ToInt() <= 0)
            {
                timer1.Stop();
                CloseForm();

            }  


        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("are you sure you want to DENY ALL ?", "", MessageBoxButtons.YesNo))
            {

                try
                {
                    
                        int rowCount = grdLister.Rows.Count;

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            for (int i = 0; i < rowCount; i++)
                            {

                                if (AppVars.denyAllocatedBookingIds.ToStr().Trim().Length == 0)
                                {
                                    AppVars.denyAllocatedBookingIds += "," + grdLister.Rows[i].Cells["ONLINEBOOKINGID"].Value.ToLong() + ",";
                                }
                                else
                                {
                                    AppVars.denyAllocatedBookingIds += grdLister.Rows[i].Cells["ONLINEBOOKINGID"].Value.ToLong() + ",";
                                }                              
                            }
                        }

                        grdLister.Rows.Clear();
                        RefreshDashBoardBookings();
                        CloseForm();
                   

                }
                catch
                {



                }



            }
        }



    }
}
