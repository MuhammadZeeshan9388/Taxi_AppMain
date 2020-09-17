using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_BLL;
using System.Collections;
using Taxi_Model;
using Utils;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Taxi_AppMain.Forms;
using Taxi_AppMain.Classes;
using Telerik.WinControls.UI.Docking;
using System.Net;

namespace Taxi_AppMain
{
    public partial class frmCallHistory : UI.SetupBase
    {
        private CallerIdType_Configuration objCLISettings;

        private bool IsDataLoaded = false;
        public List<TempCallHistory> DataSource { get; set; }
        // List<TempCallHistory> objTempCallHistory = new List<TempCallHistory>();
        public frmCallHistory(CallerIdType_Configuration obj)
        {
            InitializeComponent();

            this.objCLISettings = obj;
            this.Load += new EventHandler(frmCallHistory_Load);
            FormatGrid();



            ddlCustomer.Enter += new EventHandler(ddlCustomer_Enter);


            //  ComboFunctions.FillCustomerCombo(ddlCustomer);

            grdCalls.CommandCellClick += new CommandCellClickEventHandler(grdCalls_CommandCellClick);
            this.chkSubCompanyWise.ToggleStateChanged += ChkSubCompanyWise_ToggleStateChanged;
            grdCalls.ViewCellFormatting += grdLister_ViewCellFormatting;
        }

       

        private void ChkSubCompanyWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlSubCompanyId.SelectedValue = null;
                ddlSubCompanyId.Enabled = false;
            }
            else
            {
                ddlSubCompanyId.Enabled = true;
            }
        }
        void ddlCustomer_Enter(object sender, EventArgs e)
        {
            if (ddlCustomer.DataSource == null)
            {

                FillCustomers();



                //   ddlCustomer.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;


            }
        }



        private void FillCustomers()
        {


            try
            {

                var list = General.GetQueryable<Customer>(c => c.Name.Length > 1).Select(c => c.Name).Distinct().OrderBy(c => c).Take(50000);

                ddlCustomer.DataSource = list;


                ddlCustomer.SelectedIndex = -1;
            }
            catch
            {


            }


        }




        GridViewRowInfo row = null;
        void grdCalls_CommandCellClick(object sender, EventArgs e)
        {
            row = grdCalls.CurrentRow;
            if (row != null && row is GridViewRowInfo)
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name.ToLower() == "btncreatebooking")
                {


                    General.ShowBookingForm(0, false, row.Cells[COLS.Name].Value.ToStr(), row.Cells["PhoneNumber"].Value.ToStr(), Enums.BOOKING_TYPES.LOCAL);


                }
                else if (gridCell.ColumnInfo.Name.ToLower() == "btnaddcustomer" && string.IsNullOrEmpty(row.Cells[COLS.Name].Value.ToStr()))
                {

                    ShowCustomerForm(row.Cells["PhoneNumber"].Value.ToStr());


                }
                else if (gridCell.ColumnInfo.Name.ToLower() == "recording")
                {

                    try
                    {
                        if (Directory.Exists(Application.StartupPath + "\\Recordings") == false)
                        {
                            try
                            {

                                Directory.CreateDirectory(Application.StartupPath + "\\Recordings");
                            }
                            catch
                            {

                            }
                        }


                        string duration = row.Cells["Duration"].Value.ToStr();


                    



                        if (File.Exists(Application.StartupPath + "\\Recordings\\" + row.Cells["Duration"].Value.ToStr()))
                        {

                            Process.Start(Application.StartupPath + "\\Recordings\\" + row.Cells["Duration"].Value.ToStr());

                        }
                        else
                        {

                            string phoneNo = row.Cells["PhoneNumber"].Value.ToStr();

                            if (phoneNo.StartsWith("44") == false)
                            {

                                phoneNo = phoneNo.Substring(1);
                                phoneNo = phoneNo.Insert(0, "44");
                            }


                            string baseurl = System.Configuration.ConfigurationManager.AppSettings["recordingurl"].ToStr();
                            string username = System.Configuration.ConfigurationManager.AppSettings["recordingusername"].ToStr();

                           
                            if (baseurl.ToStr().Trim().Length == 0)
                            {
                                MessageBox.Show("Recording Url is not defined in Configurations");
                                return;
                            }
                            if (username.ToStr().Trim().Length == 0)
                            {
                                MessageBox.Show("Recording UserName is not defined in Configurations");
                                return;
                            }



                            string recordingPath =General.DownloadRecordingFile(Application.StartupPath + "\\Recordings", baseurl, username, row.Cells["Duration"].Value.ToStr(), phoneNo, row.Cells["CallDateTime"].Value.ToDateTime());


                            if (recordingPath.ToStr().Trim().Length > 0)
                            {

                                Process.Start(recordingPath);

                            }


                        }

                        GC.Collect();
                    }
                    catch (Exception ex)
                    {

                        ENUtils.ShowMessage(ex.Message);

                    }





                }
            }
        }

        //public static string DownloadRecordingFile(string FolderPath, string BaseURL, string ClientUserName, string UniqueID, string CallerId)
        //{
        //    try
        //    {

        //        string fileName = UniqueID + "_" + CallerId + ".wav";
        //        string FileUrl = BaseURL.Trim().TrimEnd('/') + "/" + ClientUserName + "/" + fileName;
        //        //string FloderPath = System.IO.Directory.GetCurrentDirectory() + "\\" + "Recordings";

        //        if (!System.IO.Directory.Exists(FolderPath))
        //        {
        //            System.IO.Directory.CreateDirectory(FolderPath);
        //        }

        //        using (WebClient wc = new WebClient())
        //        {
        //            //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
        //            wc.DownloadFile(
        //                // Param1 = Link of file
        //                new System.Uri(FileUrl),
        //                // Param2 = Path to save
        //                FolderPath + "\\" + fileName
        //            );
        //        }

        //        return FolderPath + "\\" + fileName;
        //    }
        //    catch (Exception exe)
        //    {
        //    }

        //    return string.Empty;
        //}



        private void ShowCustomerForm(string phoneNumber)
        {

            try
            {

                frmCustomer frm = new frmCustomer(phoneNumber);

                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {


            }
        }

        void frmCallHistory_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.Value = DateTime.Now.AddDays(-1).ToDate();
                dtpTillDate.Value = DateTime.Now.AddDays(1).ToDate() + TimeSpan.Parse("23:59:59");
                // PopulateData();
                ViewReport();
                AddRecordingColumn();
                AddCreateBookingColumn(grdCalls);
                AddCustomerColumn(grdCalls);

                ddlSubCompanyId.Enter += DdlSubCompanyId_Enter;

                if (this.objCLISettings.VOIPCLIType.ToInt() == 2 || this.objCLISettings.VOIPCLIType.ToInt() == 4)
                {
                  //  grdCalls.Columns["Line"].IsVisible = true;
                   // grdCalls.Columns["Duration"].IsVisible = false;
                      grdCalls.Columns["Recording"].IsVisible = true;
                }
            }
            catch
            {


            }
        }

        private void DdlSubCompanyId_Enter(object sender, EventArgs e)
        {
            if (ddlSubCompanyId.DataSource == null)
                ComboFunctions.FillSubCompanyCombo(ddlSubCompanyId);
        }

        private void AddRecordingColumn()
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 100;

            col.Name = "Recording";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Play Recording";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            col.IsVisible = false;
            grdCalls.Columns.Add(col);

        }

        private void AddCreateBookingColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 100;

            col.Name = "btnCreateBooking";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Create Booking";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

        private void AddCustomerColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 100;

            col.Name = "btnAddCustomer";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Add Customer";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

        public override void PopulateData()
        {
            if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                dtpFromDate.Value = null;

            if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                dtpTillDate.Value = null;

            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime? tillDate = dtpTillDate.Value.ToDateorNull();

            string phone = txtPhone.Text.Trim();
            string name = ddlCustomer.Text.Trim().ToLower();

            string line = txtLine.Text.Trim();
            string stn = txtSTN.Text.Trim();

            bool MissedCalls = optMissedCalls.Checked;

            string SubCompanyNo = string.Empty;

            int SubCompanyId = ddlSubCompanyId.SelectedValue.ToInt();
            if (SubCompanyId > 0)
            {
                using (TaxiDataContext dbX = new TaxiDataContext())
                {
                    SubCompanyNo = dbX.Gen_SubCompanies.FirstOrDefault(c => c.Id == SubCompanyId).ConnectionString;
                }


                if (string.IsNullOrEmpty(SubCompanyNo))
                {
                    grdCalls.Rows.Clear(); ;
                    MessageBox.Show("Sub Company CallerId Number is not defined");
                    return;
                    //  SubCompanyNo = null;    
                }
            }
            else
            {
                SubCompanyNo = string.Empty;
            }



            TaxiDataContext db = new TaxiDataContext();

            var list = (from a in db.CallHistories
                        join b in db.Gen_SubCompanies on a.CalledToNumber equals b.ConnectionString into table2
                        from b in table2.DefaultIfEmpty()

                        where (fromDate == null || a.CallDateTime.Value.Date >= fromDate)
                         && (tillDate == null || a.CallDateTime.Value.Date <= tillDate)
                         && (name == string.Empty || a.Name.Trim().ToLower() == name)
                         && (MissedCalls == false || (a.STN == null || a.STN == ""))
                         && (phone == string.Empty || a.PhoneNumber.Trim() == phone)
                          && (line == string.Empty || (a.Line != null && a.Line.Trim() == line))
                           && (stn == string.Empty || (a.STN != null && a.STN.Trim() == stn))
                           && (IsAccepted == true || (a.IsAccepted != null && a.IsAccepted == true))
                           && (SubCompanyNo == string.Empty || (a.CalledToNumber != null && a.CalledToNumber.Trim() == SubCompanyNo))
                        orderby a.CallDateTime descending
                        select new
                        {
                            Sno = a.Sno,
                            Name = a.Name,
                            PhoneNumber = a.PhoneNumber,
                            CallDateTime = a.CallDateTime,
                            Line = a.Line,
                            STN = a.STN,
                            Duration = a.CallDuration,
                            IsMissed = (a.IsAccepted != null && a.IsAccepted == true) ? "1" : "0",
                            Company = b != null && b.CompanyName != "" ? b.CompanyName : a.CalledToNumber
                        }).ToList();

            //foreach (var item in list)
            //{
            //    //objTempCallHistory.Add(new TempCallHistory { Name = item.Name, PhoneNo = item.PhoneNumber, CallDateTime = item.CallDateTime, STN = item.STN, Line = item.Line, CompanyName = item.Company });
            //}

            db.Dispose();



            //var list2 = (from a in General.GetQueryable<CallHistory>(null)
            //            where (fromDate == null || a.CallDateTime.Value.Date >= fromDate)
            //             && (tillDate == null || a.CallDateTime.Value.Date <= tillDate)
            //             && (name == string.Empty || a.Name.ToStr().Trim().ToLower() == name)
            //             && (phone == string.Empty || a.PhoneNumber.Trim() == phone)
            //              && (line == string.Empty || (a.Line != null && a.Line.Trim() == line))
            //               && (stn == string.Empty || (a.STN != null && a.STN.Trim() == stn))
            //               && (IsAccepted == true || (a.IsAccepted != null && a.IsAccepted == true))
            //            orderby a.CallDateTime descending
            //            select new
            //            {
            //                Sno = a.Sno,
            //                Name = a.Name,
            //                PhoneNumber = a.PhoneNumber,
            //                CallDateTime = a.CallDateTime,
            //                Line = a.Line,
            //                STN = a.STN,
            //                Duration = a.CallDuration,
            //                IsMissed = (a.IsAccepted != null && a.IsAccepted == true) ? "1" : "0"
            //            }).ToList();

            lblTotalAnsweredCalls.Text = "Total Answered Calls : " + list.Where(c => c.STN.Length > 0).Count();
            lblTotalMissedCalls.Text = "Total Missed Calls : " + list.Where(c => (c.STN == null || c.STN == "")).Count();
            lblTotalBookings.Text = "Total Bookings : " + 0;
            grdCalls.DataSource = list;



            if (this.objCLISettings.IsAnalog.ToBool())
            {
                grdCalls.Columns["Line"].IsVisible = true;
                grdCalls.Columns["Line"].Width = 50;
                grdCalls.Columns["CallDateTime"].Width = 160;
                grdCalls.Columns["Name"].Width = 140;
            }

            else
            {

                grdCalls.Columns["Line"].IsVisible = false;
                grdCalls.Columns["CallDateTime"].Width = 180;
                grdCalls.Columns["Name"].Width = 140;
            }


            if (this.objCLISettings.DigitalCLIType.ToInt() == 2)
            {

                grdCalls.Columns["Line"].IsVisible = true;
                grdCalls.Columns["Line"].Width = 40;
                grdCalls.Columns["CallDateTime"].Width = 160;
                grdCalls.Columns["Name"].Width = 140;

                grdCalls.Columns["Duration"].Width = 120;


                if (IsDataLoaded == false)
                    this.Width = this.Width + 190;


              //  btnMissedCalls.Visible = true;
              
            }
            else
            {
                grdCalls.Columns["Sno"].IsVisible = false;
                grdCalls.Columns["STN"].IsVisible = false;
                grdCalls.Columns["Duration"].IsVisible = false;
            }



            if (this.objCLISettings.VOIPCLIType.ToInt() == 2 || this.objCLISettings.VOIPCLIType.ToInt()==4)
            {
                grdCalls.Columns["Line"].IsVisible = true;
                grdCalls.Columns["Duration"].IsVisible = false;
                grdCalls.Columns["Recording"].IsVisible = true;

            }

            grdCalls.Columns["Sno"].Width = 60;
            grdCalls.Columns["CompanyName"].Width = 140;
            grdCalls.Columns["CompanyName"].HeaderText = "Company";
            grdCalls.Columns["STN"].IsVisible = true;
            grdCalls.Columns["PhoneNumber"].Width = 120;
            grdCalls.Columns["PhoneNumber"].HeaderText = "Phone Number";

            (grdCalls.Columns["CallDateTime"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            grdCalls.Columns["CallDateTime"].HeaderText = "Call Date Time";

            grdCalls.Columns["IsMissed"].IsVisible = false;




            if (grdCalls.Columns["IsMissed"].ConditionalFormattingObjectList.Count == 0)
            {
                ConditionalFormattingObject objMissedCondition = new ConditionalFormattingObject();
                objMissedCondition.TValue1 = "1";
                objMissedCondition.ApplyToRow = true;
                objMissedCondition.RowBackColor = Color.LightPink;
                objMissedCondition.ConditionType = ConditionTypes.Equal;

                grdCalls.Columns["IsMissed"].ConditionalFormattingObjectList.Add(objMissedCondition);

            }


            if(this.objCLISettings!=null && this.objCLISettings.VOIPCLIType.ToInt() == 4 && File.Exists(Application.StartupPath+ "\\CabTreasureEmeraldCallRecording.exe"))
            {
                btnMissedCalls.Visible = true;
               



            }

            IsDataLoaded = true;

        }

        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {






                if (e.CellElement is GridDataCellElement)
                {



                if (e.Column.Name.ToLower() == "recording")
                {




                    if (e.Row.Cells["Duration"].Value.ToStr().Contains("."))
                    {


                        ((RadButtonElement)e.CellElement.Children[0]).Enabled = true;
                    }
                    else
                    {
                            //     ((RadButtonElement)e.CellElement.Children[0]).Text = "Re-Despatched";
                            ((RadButtonElement)e.CellElement.Children[0]).Enabled = false;

                        }
                }
               



            }


                //}
        }
            catch (Exception ex)
            {

            }
        }


        public struct COLS
        {
            public static string Name = "Name";
            public static string SessionId = "SessionId";
            public static string CallStatus = "CallStatus";
            public static string Duration = "Duration";
            public static string Phone = "Phone";
            public static string DateTime = "DateTime";
            public static string Address = "Address";
            public static string IO = "IO";


        }


        private void FormatGrid()
        {
            grdCalls.ShowGroupPanel = false;
            grdCalls.AllowAddNewRow = false;
            grdCalls.AllowEditRow = false;
            grdCalls.ShowRowHeaderColumn = false;

        }


        private bool IsAccepted = true;



        private void btnViewCallHistory_Click(object sender, EventArgs e)
        {
            IsAccepted = true;
            // PopulateData();

            ViewReport();


        }

        private void ViewReport()
        {
            try
            {
                DataSource = FillDataSource();

                var list = (from a in DataSource
                            select new
                    {
                        Sno = a.Sno,
                        Name = a.Name,
                        PhoneNumber = a.PhoneNo,
                        CallDateTime = a.CallDateTime,
                        Line = a.Line,
                        STN = a.STN,
                        Duration = a.Duration,
                        IsMissed = a.IsMissed,
                        Company = a.CompanyName
                    }).ToList();

                lblTotalAnsweredCalls.Text = "Total Answered Calls : " + list.Where(c => c.STN.Length > 0).Count();
                lblTotalMissedCalls.Text = "Total Missed Calls : " + list.Where(c => (c.STN == null || c.STN == "")).Count();
                lblTotalBookings.Text = "Total Bookings : " + 0;
                grdCalls.DataSource = list;



                if (this.objCLISettings.IsAnalog.ToBool())
                {
                    grdCalls.Columns["Line"].IsVisible = true;
                    grdCalls.Columns["Line"].Width = 50;
                    grdCalls.Columns["CallDateTime"].Width = 150;
                    grdCalls.Columns["Name"].Width = 160;
                }

                else
                {

                    grdCalls.Columns["Line"].IsVisible = false;
                    grdCalls.Columns["CallDateTime"].Width = 140;
                    grdCalls.Columns["Name"].Width = 180;
                }


                if (this.objCLISettings.DigitalCLIType.ToInt() == 2)
                {

                    grdCalls.Columns["Line"].IsVisible = true;
                    grdCalls.Columns["Line"].Width = 40;
                    grdCalls.Columns["CallDateTime"].Width = 140;
                    grdCalls.Columns["Name"].Width = 140;

                    grdCalls.Columns["Duration"].Width = 120;


                    if (IsDataLoaded == false)
                        this.Width = this.Width + 190;


                 //   btnMissedCalls.Visible = true;
                }
                else
                {
                    grdCalls.Columns["Sno"].IsVisible = false;
                    grdCalls.Columns["STN"].IsVisible = false;
                    grdCalls.Columns["Duration"].IsVisible = false;
                }



                if (this.objCLISettings.VOIPCLIType.ToInt() == 2 || this.objCLISettings.VOIPCLIType.ToInt() == 4)
                {
                    grdCalls.Columns["Line"].IsVisible = true;
                    grdCalls.Columns["Duration"].IsVisible = false;
                  //  grdCalls.Columns["Recording"].IsVisible = true;
                }

                grdCalls.Columns["Sno"].Width = 60;
                grdCalls.Columns["Company"].Width = 140;
                //grdCalls.Columns["CompanyName"].HeaderText = "Company";
                grdCalls.Columns["STN"].IsVisible = true;
                grdCalls.Columns["PhoneNumber"].Width = 120;
                grdCalls.Columns["PhoneNumber"].HeaderText = "Phone Number";

                (grdCalls.Columns["CallDateTime"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
                grdCalls.Columns["CallDateTime"].HeaderText = "Call Date Time";

                grdCalls.Columns["IsMissed"].IsVisible = false;




                if (grdCalls.Columns["IsMissed"].ConditionalFormattingObjectList.Count == 0)
                {
                    ConditionalFormattingObject objMissedCondition = new ConditionalFormattingObject();
                    objMissedCondition.TValue1 = "1";
                    objMissedCondition.ApplyToRow = true;
                    objMissedCondition.RowBackColor = Color.LightPink;
                    objMissedCondition.ConditionType = ConditionTypes.Equal;

                    grdCalls.Columns["IsMissed"].ConditionalFormattingObjectList.Add(objMissedCondition);

                }


                if (this.objCLISettings != null && this.objCLISettings.VOIPCLIType.ToInt() == 4 && File.Exists(Application.StartupPath + "\\CabTreasureEmeraldCallRecording.exe"))
                {
                    btnMissedCalls.Visible = true;




                }

                IsDataLoaded = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void frmCallHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void btnMissedCalls_Click(object sender, EventArgs e)
        {
            //IsAccepted = false;
            //PopulateData();

            try
            {
                using (TaxiDataContext dbX = new TaxiDataContext())
                {
                    var obj = dbX.CallerIdVOIP_Configurations.FirstOrDefault();


                    if (obj != null)
                    {
                        System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcesses().FirstOrDefault(c => c.ProcessName.Contains("CabTreasureEmeraldCallRecording"));

                        if (proc != null)
                        {
                            proc.Kill();
                            proc.CloseMainWindow();
                            proc.Close();
                        }






                        string arg = obj.UserName.ToStr() + " " + obj.Password.ToStr();
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\CabTreasureEmeraldCallRecording.exe", arg);
                    }


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? toDate = dtpTillDate.Value.ToDateTimeorNull();


                rptfrmCallHistoryReport frm = new rptfrmCallHistoryReport();

                //972, 531
                frm.Height = 800;
                frm.Width = 1000;
                frm.ReportHeading = "Period: " + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", toDate);
                frm.DataSource = FillDataSource();
                // frm.TemplateValue = templateNo + "_rptJobsList.rdlc";
                frm.LoadReport();
                frm.ShowDialog();

                //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmCallHistoryReport1");

                //if (doc != null)
                //{
                //    doc.Close();
                //}
                //UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }

        private List<TempCallHistory> FillDataSource()
        {
            List<TempCallHistory> objTempCallHistory = new List<TempCallHistory>();
            try
            {
                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

                string phone = txtPhone.Text.Trim();
                string name = ddlCustomer.Text.Trim().ToLower();

                string line = txtLine.Text.Trim();
                string stn = txtSTN.Text.Trim();

                bool AllCalls = optAllCalls.Checked;
                bool MissedCalls = optMissedCalls.Checked;
                bool AnsweredCalls = optAnsweredCalls.Checked;

                string SubCompanyNo = string.Empty;

                int SubCompanyId = ddlSubCompanyId.SelectedValue.ToInt();
                if (SubCompanyId > 0)
                {
                    using (TaxiDataContext dbX = new TaxiDataContext())
                    {
                        SubCompanyNo = dbX.Gen_SubCompanies.FirstOrDefault(c => c.Id == SubCompanyId).ConnectionString;
                    }

                    if (string.IsNullOrEmpty(SubCompanyNo))
                    {

                        grdCalls.Rows.Clear(); 

                        MessageBox.Show("Sub Company CallerId Number is not defined");
                        return objTempCallHistory;
                        //  SubCompanyNo = null;    
                    }
                }
                else
                {
                    SubCompanyNo = string.Empty;
                }

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list = (from a in db.CallHistories
                                join b in db.Gen_SubCompanies on a.CalledToNumber equals b.ConnectionString into table2
                                from b in table2.DefaultIfEmpty()

                                where (fromDate == null || a.CallDateTime.Value >= fromDate)
                                 && (tillDate == null || a.CallDateTime.Value <= tillDate)
                                 && (name == string.Empty || a.Name.Trim().ToLower() == name)
                                 && (MissedCalls == false || (a.STN == null || a.STN == ""))
                                 && (AnsweredCalls == false || (a.STN != ""))
                                 && (phone == string.Empty || a.PhoneNumber.Trim() == phone)
                                  && (line == string.Empty || (a.Line != null && a.Line.Trim() == line))
                                   && (stn == string.Empty || (a.STN != null && a.STN.Trim() == stn))
                                   && (IsAccepted == true || (a.IsAccepted != null && a.IsAccepted == true))
                                   && (SubCompanyNo == string.Empty || (a.CalledToNumber != null && a.CalledToNumber.Trim() == SubCompanyNo))
                                orderby a.CallDateTime descending
                                select new
                                {
                                    Sno = a.Sno,
                                    Name = a.Name,
                                    PhoneNumber = a.PhoneNumber,
                                    CallDateTime = a.CallDateTime,
                                    Line = a.Line,
                                    STN = a.STN,
                                    Duration = a.CallDuration,
                                    IsMissed = (a.IsAccepted != null && a.IsAccepted == true) ? "1" : "0",
                                    Company = b != null && b.CompanyName != "" ? b.CompanyName : a.CalledToNumber
                                }).ToList();

                    foreach (var item in list)
                    {
                        objTempCallHistory.Add(new TempCallHistory { Name = item.Name, PhoneNo = item.PhoneNumber, CallDateTime = item.CallDateTime, STN = item.STN, Line = item.Line, CompanyName = item.Company, Sno = item.Sno, IsMissed = item.IsMissed == "1" ? true : false, Duration = item.Duration });
                    }
                    return objTempCallHistory;
                }
            }
            catch (Exception)
            {
                return objTempCallHistory;

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? toDate = dtpTillDate.Value.ToDateTimeorNull();


                rptfrmCallHistoryReport frm = new rptfrmCallHistoryReport();

                frm.ReportHeading = "Period: " + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", toDate);
                frm.DataSource = FillDataSource();
                // frm.TemplateValue = templateNo + "_rptJobsList.rdlc";
                frm.LoadReport();
                frm.ExportReport();
               
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }
    }
}
