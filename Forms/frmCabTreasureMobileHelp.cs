using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Taxi_BLL;
using DAL;
using System.Data.SqlClient;
using Telerik.WinControls;
using System.Diagnostics;


namespace Taxi_AppMain
{
    public partial class frmCabTreasureMobileHelp : UI.SetupBase
    {
        public frmCabTreasureMobileHelp()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmCabTreasureMobileHelp_Load);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnSend.Click += new EventHandler(btnSend_Click);
            this.ddlApplicationType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlApplicationType_SelectedIndexChanged);
            this.chkEmail.ToggleStateChanged += new StateChangedEventHandler(chkEmail_ToggleStateChanged);
            this.chkSMS.ToggleStateChanged += new StateChangedEventHandler(chkSMS_ToggleStateChanged);
            this.btnSelectContact.Click += new EventHandler(btnSelectContact_Click);
            this.btnSelectEmail.Click += new EventHandler(btnSelectEmail_Click);
            this.btnEdit.Click += new EventHandler(btnEdit_Click);
            this.btnUpdate.Click += new EventHandler(btnUpdate_Click);
        }

        void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateRecord();
        }

        private void UpdateRecord()
        {
            try
            {
                string AppType = ddlApplicationType.Text.ToStr();
                string Error = "";
                int ? ApplicationTypeId  = null;// ddlApplicationType.SelectedValue.ToInt();
                string Link = txtLink.Text.Trim();
                string Version = txtVersion.Text.Trim();
                if (AppType == "Android Driver App")
                {
                    ApplicationTypeId = 1;
                }
                else if (AppType == "Iphone Driver App")
                {
                    ApplicationTypeId = 2;
                }
                if (ApplicationTypeId == null)
                {
                    Error = "Required : Application Type";
                }
                if (string.IsNullOrEmpty(Link))
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : App Url";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : App Url";
                    }
                }
                if (string.IsNullOrEmpty(Version))
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : App Version";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : App Version";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    int I = db.ExecuteCommand("update AppsLink set Link='" + Link + "', Version = '"+Version+"' where Id= "+ApplicationTypeId+"");
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private string pdaMeterPwd = string.Empty;
        void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pdaMeterPwd))
            {
                frmLockingPwd frmUnLock = new frmLockingPwd();
                frmUnLock.ShowDialog();

                if (string.IsNullOrEmpty(frmUnLock.ReturnValue1))
                    return;
                else
                    pdaMeterPwd = frmUnLock.ReturnValue1;

                frmUnLock.Dispose();
            }
            if (pdaMeterPwd != null)
            {
                txtVersion.Visible = true;
                btnUpdate.Visible = true;
                lblVersion.Visible = false;
                txtVersion.Text = lblVersion.Text;
                btnUpdate.Enabled = true;
            }
        }

        void btnSelectEmail_Click(object sender, EventArgs e)
        {
            var list = (from a in General.GetQueryable<Fleet_Driver>(c=>c.Email!=null && c.Email.Length >0)
                        select new
                        {
                            Id = a.Id,
                            No = a.DriverNo,
                            Name = a.DriverName,
                            Email = a.Email,
                            PDAVersion =a.Fleet_Driver_PDASettings.Count > 0 ? a.Fleet_Driver_PDASettings.FirstOrDefault().CurrentPdaVersion : 0.00m

                        }).ToList();
            frmLister frm = new frmLister(list, "Id", true, new string[] { "Id" });
            frm.ShowDialog();

            if (frm.ListofData != null && frm.ListofData.Count > 0)
            {
                string email = string.Join(",", frm.ListofData.Select(c => c[3].ToStr()).ToArray<string>());
                if (email.Length > 0)
                {
                    txtEmail.Text = email;
                }
            }

            frm.Dispose();
          
        }

        void btnSelectContact_Click(object sender, EventArgs e)
        {
            var list = (from a in General.GetQueryable<Fleet_Driver>(c=>c.MobileNo!=null && c.MobileNo.Length >0)
                        select new
                        {
                            //Dr=a.DriverName,
                            Id=a.Id,
                            No = a.DriverNo,
                            Name=a.DriverName,
                            MobileNo = a.MobileNo,
                            PDAVersion = a.Fleet_Driver_PDASettings.Count > 0 ? a.Fleet_Driver_PDASettings.FirstOrDefault().CurrentPdaVersion : 0.00m


                        }).ToList();
            frmLister frm = new frmLister(list,"Id",true,new string[]{"Id"});
            frm.ShowDialog();

            if(frm.ListofData!=null && frm.ListofData.Count > 0)
            {
                string mobNo = string.Join(",", frm.ListofData.Select(c => c[3].ToStr()).ToArray<string>());
               if(mobNo.Length > 0)
               {
                   txtMobileNo.Text=mobNo;
               }
            }

            frm.Dispose();
        }

        void chkSMS_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkSMS.ToggleState.ToBool() == true)
            {
                txtMobileNo.Visible = true;
                btnSelectContact.Enabled = true;
            }
            else
            {
                txtMobileNo.Visible = false;
                btnSelectContact.Enabled = false;
            }   
        }

        void chkEmail_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkEmail.ToggleState.ToBool() == true)
            {
                txtEmail.Visible = true;
                btnSelectEmail.Enabled = true;
            }
            else
            {
                btnSelectEmail.Enabled = false;
                txtEmail.Visible = false;            
            }
        }

        void ddlApplicationType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlApplicationType.SelectedIndex.ToInt() > -1)
            {
                btnEdit.Enabled = true;
                txtVersion.Visible = false;
                btnUpdate.Enabled = true;
                lblVersion.Visible = true;
            }
            if (ddlApplicationType.Text.ToStr() == "Android Driver App")
            {
                Display(1);
            }
            else if (ddlApplicationType.Text.ToStr() == "Iphone Driver App")
            {
                Display(2);
            }
            else
            {
                txtEmail.Text = "";
                lblVersion.Text = "0";
            }
        }


        void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string Error = string.Empty;
                string MobileNo = txtMobileNo.Text.ToStr().Trim();
                string Email = txtEmail.Text.ToStr().Trim();
                string Link = txtLink.Text.ToStr().Trim();
                if (ddlApplicationType.SelectedIndex < 0)
                {
                    Error = "Please select Application Type";
                }
                if (chkSMS.Checked.ToBool() && string.IsNullOrEmpty(MobileNo))
                {
                    Error = "Required : Mobile No";
                }

                if (chkEmail.Checked.ToBool() && string.IsNullOrEmpty(Email))
                {
                    Error = "Required : Email";
                }

                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                bool IsSent = false;

                if (chkSMS.Checked.ToBool() && !string.IsNullOrEmpty(MobileNo))
                {
                    SendSMS(MobileNo, Link);
                    IsSent = true;
                }


                if (chkEmail.Checked.ToBool() && !string.IsNullOrEmpty(Email))
                {
                    SendEmail(txtEmail.Text);
                    IsSent = true;
                }

                if(IsSent)
                SendNotification();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void SendNotification()
        {

            RadDesktopAlert alert = new RadDesktopAlert();
            alert.CaptionText = "Application Link";
            alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>Link sent Successfully</span></b></html>";
            alert.Show();
        }

        private void SendSMS(string mobileNo, string message)
        {

            string rtnMsg = string.Empty;
            EuroSMS objSMS = new EuroSMS();
            objSMS.Message = message;

            foreach (var item in mobileNo.Split(new char[] { ',' }))
            {



                string mobNo = item;
                if (item.StartsWith("+44") == false)
                {

                    if (Debugger.IsAttached == false)
                    {

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
                    }
                }
                objSMS.ToNumber = mobNo.Trim();
                objSMS.Send(ref rtnMsg);

            }

         


         

        }


        public void SendEmail(string to)
        {

            if (to.Length > 0)
            {

                string subject = ddlApplicationType.Text + " link Version(" + lblVersion.Text + ")";

                string body = subject + Environment.NewLine + " " + txtLink.Text;

                body = body.Replace("\r\n", "<br/>").Trim();



                foreach (var item in to.Split(new char[]{','}))
                {

                    try
                    {

                        Email.Send(subject, body, AppVars.objSubCompany.EmailAddress.ToStr(), item);
                    }
                    catch
                    {


                    }
                }

             



            }

           

        }



        public class ClsAppsLink
        {
            public string Link;
            public string Version;


        }

        private void Display(int AppId)
        {
            try
            {

                ClsAppsLink dt = null;
                using (TaxiDataContext db = new TaxiDataContext())
                {
                       dt = db.ExecuteQuery<ClsAppsLink>("select Link,Version from AppsLink where AppId =" + AppId).FirstOrDefault();
                }
                if (dt != null)
                {
                    txtLink.Text = dt.Link;
                    lblVersion.Text = dt.Version;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frmCabTreasureMobileHelp_Load(object sender, EventArgs e)
        {
            FillApplicationType();
        }
        private void FillApplicationType()
        {
            try
            {
                ddlApplicationType.NullText = "Select Application";
                ddlApplicationType.Items.Add("Android Driver App");
                ddlApplicationType.Items.Add("Iphone Driver App");
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void SendSMS()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}
