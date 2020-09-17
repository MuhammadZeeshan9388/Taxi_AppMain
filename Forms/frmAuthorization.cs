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
using Utils;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Taxi_AppMain
{
    public partial class frmAuthorization : Form
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void bringToFront(string title)
        {
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }


        //System.Media.SoundPlayer sp = new System.Media.SoundPlayer(System.Windows.Forms.Application.StartupPath + "\\sound\\auth.wav");
        private DateTime _OpenedDateTime;

        public DateTime OpenedDateTime
        {
            get { return _OpenedDateTime; }
            set { _OpenedDateTime = value; }
        }







        private long _JobId;

        public long JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
        }



        private int _DriverId;

        public int DriverId
        {
            get { return _DriverId; }
            set { _DriverId = value; }
        }



        private int _JobStatusId;

        public int JobStatusId
        {
            get { return _JobStatusId; }
            set { _JobStatusId = value; }
        }


        private int _DriverStatusId;

        public int DriverStatusId
        {
            get { return _DriverStatusId; }
            set { _DriverStatusId = value; }
        }





        private string _AuthorizedBy;

        public string AuthorizedBy
        {
            get { return _AuthorizedBy; }
            set { _AuthorizedBy = value; }
        }

        private bool _IsAuthorized;

        public bool IsAuthorized
        {
            get { return _IsAuthorized; }
            set { _IsAuthorized = value; }
        }









        public frmAuthorization(long jobId, int driverId, int jobStatusId, int driverStatusId)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmAuthorization_Load);



            this.JobStatusId = jobStatusId;
            this.DriverId = driverId;
            this.JobId = jobId;
            this.DriverStatusId = driverStatusId;
            this.OpenedDateTime = DateTime.Now;
            this.KeyDown += new KeyEventHandler(frmAuthorization_KeyDown);
            this.Shown += new EventHandler(frmAuthorization_Shown);
            this.KeyPreview = true;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 475, 25);


        }

        void frmAuthorization_Shown(object sender, EventArgs e)
        {

            UpdatePopup();
          

        }

        void frmAuthorization_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                Allow();

            }
            else if (e.KeyCode == Keys.D)
            {
                Deny();

            }
           
        }

        private void TrackDriver()
        {


            try
            {
                if (Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("rptJobRouthPathGoogle")).Count() > 0)
                {
                    try
                    {
                        Application.OpenForms.Cast<Form>().FirstOrDefault(c => c.Name.Equals("rptJobRouthPathGoogle")).Close();
                    }
                    catch
                    {


                    }
                }



                if (File.Exists(Application.StartupPath + "\\Booking\\TreasureBooking.exe"))
                {
                    ClsDataTransfer polObj = new ClsDataTransfer();

                    foreach (System.Reflection.PropertyInfo item in AppVars.objPolicyConfiguration.GetType().GetProperties())
                    {
                        try
                        {

                            if (polObj.GetType().GetProperty(item.Name) != null)
                                polObj.GetType().GetProperty(item.Name).SetValue(polObj, item.GetValue(AppVars.objPolicyConfiguration, null), null);
                        }
                        catch
                        {


                        }
                    }


                    polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                    string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");
                    string s = "XXX";
                    Process pp = new Process();
                    pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                    pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");
                    pp.StartInfo.Arguments += " " + "rptJobRouthPathGoogle" + " " + this.JobId + " " + "true" + " " + this.DriverId;
                    pp.Start();
                    Thread.Sleep(500);
                    //   pp.WaitForExit();
                }
                else
                {
                    rptJobRouthPathGoogle rpt = new rptJobRouthPathGoogle(this.JobId > 0 ? new TaxiDataContext().Bookings.FirstOrDefault(c => c.Id == this.JobId) : null, true, this.DriverId);
                    rpt.ShowDialog();
                    rpt.Dispose();

                    GC.Collect();
                }

           

            }
            catch
            {


            }
        }

        void frmAuthorization_Load(object sender, EventArgs e)
        {

           

            try
            {
                //System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
                //st.Start();
                var ObjBooking = General.GetQueryable<Booking>(c => c.Id == this.JobId && c.DriverId == this.DriverId)
                               .Select(args => new { DriverNo = args.Fleet_Driver.DriverNo, args.FromAddress, args.ToAddress,args.PickupDateTime }).FirstOrDefault();

                //st.Stop();
                //Console.WriteLine(st.ElapsedMilliseconds);


              

                if (ObjBooking == null)
                {
                    AllowInvalidReject();

                }
                else
                {
                    if (this.JobStatusId == Enums.BOOKINGSTATUS.NOSHOW)
                    {
                        txtAction.Text = "Recover Job Authorization";

                    }
                    else if (this.JobStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                    {
                        txtAction.Text = "No Pickup Authorization";
                    }
                    else if (this.JobStatusId == Enums.BOOKINGSTATUS.REJECTED)
                    {
                        txtAction.Text = "Job Reject Authorization";
                        txtAction.BackColor = Color.FromArgb(-466540);
                        txtAction.ForeColor = Color.Black;
                    }


                    txtDriver.Text = "Driver : " + ObjBooking.DriverNo;
                    txtPickupPoint.Text ="@ "+string.Format("{0:HH:mm}",ObjBooking.PickupDateTime) + " , "+ ObjBooking.FromAddress;
                    txtDestination.Text = ObjBooking.ToAddress;
                    //   PlaySound();
                }



               

            }
            catch (Exception ex)
            {



            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {

            Allow();


        }

        private void Allow()
        {

            try
            {
               


                this.IsAuthorized = true;

                new Thread(delegate()
                {
                    AuthorizationPermit();
                }).Start();


                Thread.Sleep(2000);

                CloseForm();

            }
            catch (Exception ex)
            {


            }

        }


        private void AllowInvalidReject()
        {

            try
            {
                this.IsAuthorized = true;

                new Thread(delegate()
                {
                    AuthorizationInvalidPermit();
                }).Start();


                Thread.Sleep(1000);

                CloseForm();

            }
            catch (Exception ex)
            {


            }

        }


        private void AuthorizationPermit()
        {
            try
            {

                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                          new Thread(delegate()
                          {
                        // For Google Map Use Socket To Send/Receive Data
                        General.SendMessageToPDA("request pda=" + _DriverId + "=" + _JobId + "=auth status>>yes>>" + _JobId + "=5", _DriverId.ToString());
                         }).Start();

                    }


             

                }
                else
                {
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                         new Thread(delegate()
                          {
                        // For Google Map Use Socket To Send/Receive Data
                        General.SendMessageToPDA("request pda=" + _DriverId + "=" + _JobId + "=auth status>>yes>>" + _JobId + "=5", _DriverId.ToString());
                          }).Start();
                    }

                }

                (new TaxiDataContext()).stp_UpdateJob(this.JobId, this.DriverId, this.JobStatusId.ToIntorNull(), this.DriverStatusId.ToIntorNull(), AppVars.objPolicyConfiguration.SinBinTimer.ToInt());



                if (this.JobStatusId == Enums.BOOKINGSTATUS.NOPICKUP && AppVars.objPolicyConfiguration.SMSNoPickup.ToStr().Trim().Length > 0)
                {

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var objBook=  db.Bookings.FirstOrDefault(c => c.Id == JobId);

                        if (objBook != null && objBook.CustomerMobileNo.ToStr().Trim().Length > 0)
                        {
                            SendSMS(objBook.CustomerMobileNo.ToStr().Trim(), GetMessage(AppVars.objPolicyConfiguration.SMSNoPickup.ToStr().Trim(), objBook, objBook.Id), objBook.SMSType.ToInt());
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }

        private string GetMessage(string message, Booking objBooking, long jobId)
        {
            try
            {


                string msg = message;

                object propertyValue = string.Empty;
                foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                {


                    switch (tag.TagObjectName)
                    {
                        case "booking":

                            if (objBooking == null)
                                objBooking = General.GetObject<Booking>(c => c.Id == jobId);

                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = objBooking.GetType().GetProperty(val[0]).GetValue(objBooking, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(tag.ConditionNotNull) && objBooking.GetType().GetProperty(tag.ConditionNotNull) != null)
                                {

                                    if (tag.ConditionNotNull.ToStr() == "BabySeats" && tag.TagPropertyValue.ToStr() == "BabySeats")
                                    {
                                        propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);

                                        if (!string.IsNullOrEmpty(propertyValue.ToStr().Trim()) && propertyValue.ToStr().Contains("<<<"))
                                        {
                                            string[] arr = propertyValue.ToStr().Split(new string[] { "<<<" }, StringSplitOptions.None);

                                            propertyValue = "B Seat 1 : " + arr[0].ToStr() + Environment.NewLine + "B Seat 2 : " + arr[1].ToStr();

                                        }

                                    }
                                    else if (objBooking.GetType().GetProperty(tag.ConditionNotNull).GetValue(objBooking, null) != null)
                                    {
                                        propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                    }

                                }
                                else
                                {

                                    if (tag.ExpressionValue.ToStr().Trim().Length > 0)
                                    {
                                        try
                                        {
                                            char[] splitArr = new char[] { ',' };
                                            char[] splitArr2 = new char[] { '|' };
                                            string[] val = tag.ExpressionValue.Split(splitArr);

                                            string replaceMessage = val[0].ToStr();
                                            int? expressionApplied = null;
                                            foreach (var item in val.Where(c => c.EndsWith("|replacemessage") == false))
                                            {
                                                var str = item.Split(splitArr2);

                                                if (objBooking.GetType().GetProperty(str[0]) != null)
                                                {
                                                    if (objBooking.GetType().GetProperty(str[0]).GetValue(objBooking, null).ToStr() == str[1])
                                                    {
                                                        if (expressionApplied == null)
                                                            expressionApplied = 1;
                                                    }
                                                    else
                                                        expressionApplied = null;

                                                }
                                            }

                                            if (expressionApplied != null && expressionApplied == 1)
                                            {
                                                var replacearr = replaceMessage.Split(splitArr2);

                                                msg = msg.Replace(replacearr[0], replacearr[1]);
                                            }
                                            else
                                            {
                                                propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                            }
                                        }
                                        catch
                                        {
                                            propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);

                                        }

                                    }
                                    else
                                    {

                                        propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                    }



                                }
                            }


                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);
                            }
                            break;


                        case "Booking_ViaLocations":
                            if (tag.TagPropertyValue == "ViaLocValue")
                            {


                                string[] VilLocs = null;
                                int cnt = 1;
                                VilLocs = objBooking.Booking_ViaLocations.Select(c => cnt++.ToStr() + ". " + c.ViaLocValue).ToArray();
                                if (VilLocs.Count() > 0)
                                {

                                    string Locations = "VIA POINT(s) : \n" + string.Join("\n", VilLocs);
                                    propertyValue = Locations;
                                }
                                else
                                    propertyValue = string.Empty;

                            }
                            break;


                        case "driver":


                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(val[0]).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }

                            else
                            {
                                propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                            }

                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                            }
                            break;


                        case "Fleet_Driver_Image":




                            if (!string.IsNullOrEmpty(tag.TagPropertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                if (objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Images.Count > 0)
                                {
                                    string linkId = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Images[0].PhotoLinkId.ToStr();

                                    if (linkId.ToStr().Length == 0)
                                        propertyValue = " ";
                                    else
                                    {
                                        // propertyValue = "http://cabtreasure.co.uk/drv.aspx?ref=" + objBooking.BookingNo.ToStr() + ":" + linkId;
                                        if (tag.TagMemberValue.ToStr().Trim().ToLower() == "<trackdrv>")
                                        {
                                            string encrypt = Cryptography.Encrypt(objBooking.BookingNo.ToStr() + ":" + linkId + ":" + AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim() + ":" + objBooking.Id, "softeuroconnskey", true);
                                            propertyValue = Program.objLic.CabTrackUrl.ToStr() + "/tck.aspx?q=" + encrypt;

                                        }
                                        else
                                        {

                                            propertyValue = Program.objLic.CabTrackUrl.ToStr()+"/drv.aspx?ref=" + objBooking.BookingNo.ToStr() + ":" + linkId;
                                        }
                                    }
                                }
                                else
                                    propertyValue = " ";


                                //      propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                            }
                            break;


                        case "Fleet_Driver_Documents":



                            if (!string.IsNullOrEmpty(tag.TagPropertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {

                                if (tag.TagPropertyValue.Contains("PHC Vehicle"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.PCOVehicle)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("PHC Driver"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.PCODriver)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("License"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.LICENSE)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("Insurance"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.Insurance)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();

                                }
                                else if (tag.TagPropertyValue.Contains("MOT"))
                                {
                                    propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.MOT)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();

                                }



                            }
                            break;



                        default:
                            if (objBooking.SubcompanyId == null)
                                propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                            else
                                propertyValue = objBooking.Gen_SubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking.Gen_SubCompany, null);

                            break;

                    }




                    msg = msg.Replace(tag.TagMemberValue,
                        tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                }


                return msg.Replace("\n\n", "\n");
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);
                return "";
            }
        }



        private void SendSMS(string mobileNo, string message, int smsType)
        {

            try
            {
                string rtnMsg = string.Empty;
                EuroSMS objSMS = new EuroSMS();
                objSMS.Message = message;
                objSMS.BookingSMSAccountType = smsType;

                string mobNo = mobileNo;



                if (mobNo.ToStr().StartsWith("00") == false)
                {

                    int idx = -1;
                    if (mobNo.StartsWith("044") == true)
                    {
                        idx = mobNo.IndexOf("044");
                        mobNo = mobNo.Substring(idx + 3);
                        mobNo = mobNo.Insert(0, "+44");
                    }

                    if (mobNo.StartsWith("07"))
                    {
                        mobNo = mobNo.Substring(1);
                    }

                    if (mobNo.StartsWith("044") == false || mobNo.StartsWith("+44") == false)
                        mobNo = mobNo.Insert(0, "+44");
                }

                objSMS.ToNumber = mobNo.Trim();



                objSMS.Send(ref rtnMsg);

            }
            catch
            {


            }


        }






        private void AuthorizationInvalidPermit()
        {
            try
            {

                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                        // For Google Map Use Socket To Send/Receive Data
                        General.SendMessageToPDA("request pda=" + _DriverId + "=" + _JobId + "=auth status>>yes>>" + _JobId + "=5");


                    }

                }
                else
                {
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                        // For Google Map Use Socket To Send/Receive Data
                        General.SendMessageToPDA("request pda=" + _DriverId + "=" + _JobId + "=auth status>>yes>>" + _JobId + "=5");
                    }

                }

                
            }
            catch (Exception ex)
            {


            }

        }


        private void btnNo_Click(object sender, EventArgs e)
        {
            Deny();

        }


        private void Deny()
        {

            try
            {
                this.IsAuthorized = true;

                new Thread(delegate()
                {
                    AuthorizationDenied();
                }).Start();


                Thread.Sleep(1000);

                CloseForm();

            }
            catch (Exception ex)
            {


            }

        }



        private void AuthorizationDenied()
        {

            try
            {
                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                        // For Google Map Use Socket To Send/Receive Data
                        General.SendMessageToPDA("request pda=" + _DriverId + "=" + _JobId + "=auth status>>no>>" + _JobId + "=5", _DriverId.ToString());
                    }

                }
                else
                {
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                        // For Google Map Use Socket To Send/Receive Data
                        General.SendMessageToPDA("request pda=" + _DriverId  + "=" + _JobId + "=auth status>>no>>" + _JobId + "=5", _DriverId.ToString());
                    }

                }
            }
            catch (Exception ex)
            {


            }


        }


        public void CloseForm()
        {


            AppVars.frmMDI.RefreshActiveDashBoard();

            // AppVars.frmMDI.RefreshDashBoard();

            new BroadcasterData().BroadCastToAll("**broadcast close auth job>>" + Environment.MachineName);


           

            this.Close();

        }


        //private void StopSound()
        //{

        //    sp.Stop();

        //}

        private void PlaySound()
        {
            // sp.Play();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            UpdatePopup();
           
        }

        private void UpdatePopup()
        {

            //if (Debugger.IsAttached)
            //    return;

            if (DateTime.Now.Subtract(this.OpenedDateTime).TotalSeconds > 59)
            {
                CloseForm();
            }
            else
            {


                try
                {








                    if (Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmBooking")).Count() == 0)
                    {


                        if (Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessageAllDrivers")).Count() > 0)
                        {

                            var frms = Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessageAllDrivers"));
                            foreach (Form frm in frms)
                            {

                                frm.BringToFront();
                                frm.Focus();
                                frm.Activate();
                            }
                          //  SendToBack();

                        }
                        else
                        {


                            if (Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessages")).Count() > 0)
                            {

                                var frms = Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessages"));
                                foreach (Form frm in frms)
                                {

                                    frm.BringToFront();
                                    frm.Focus();
                                    frm.Activate();
                                }
                            //    SendToBack();

                            }
                            else
                                SetForegroundWindow(this.Handle);
                        }

                       
                    }
                    else
                    {


                        if (Process.GetProcesses().Count(c => c.ProcessName == "Treasurebooking.exe") > 0)
                        {

                            SendToBack();
                            // WindowHelper.BringProcessToFront(Process.GetProcesses().FirstOrDefault(c => c.ProcessName == "Treasurebooking.exe"));

                        }
                        else
                        {

                            if (Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmBooking")).Count() > 0)
                            {

                                var frms = Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmBooking"));
                                foreach (Form frm in frms)
                                {




                                    frm.BringToFront();
                                    frm.Focus();
                                    frm.Activate();
                                }


                            }
                        }


                        if (Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessageAllDrivers")).Count() > 0)
                        {

                            var frms = Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessageAllDrivers"));
                            foreach (Form frm in frms)
                            {

                                frm.BringToFront();
                                frm.Focus();
                                frm.Activate();
                            }
                            SendToBack();

                        }
                        else
                        {


                            if (Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessages")).Count() > 0)
                            {

                                var frms = Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("frmMessages"));
                                foreach (Form frm in frms)
                                {

                                    frm.BringToFront();
                                    frm.Focus();
                                    frm.Activate();
                                }
                                SendToBack();

                            }
                        }

                    }

                }
                catch
                {


                }
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            txtTimer.Text = (txtTimer.Text.ToInt() - 1).ToStr();
        }

        private void btnTrackDriver_Click(object sender, EventArgs e)
        {
            TrackDriver();
        }
    }
}
