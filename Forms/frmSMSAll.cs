using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Taxi_Model;
using System.Threading;
using System.Diagnostics;
using DAL;
using Taxi_BLL;
using Utils;
using UI;


namespace Taxi_AppMain
{
    public partial class frmSMSAll : Forms.BaseForm
    {
        string BunchNo = "";
        int MessageTemplateId = 0;
        string MessageText = "";
        int TotalNos = 0;
        int TotalPickUps = 0;
        int BunchValue = 0;
        bool SMSBunchMessages = false;


        SMSBunchesBO objSMSBunches;
        public frmSMSAll()
        {
            InitializeComponent();
            objSMSBunches = new SMSBunchesBO();
           // this.SetProperties((INavigation)objSMSBunches);
        }
        public frmSMSAll(string No)
        {
            InitializeComponent();
            txtTo.Text = No;
            txtMessage.Select();
            
        }

        public frmSMSAll(List<string> objNumbers)
        {
            InitializeComponent();
            if (objNumbers != null)
            {
                
                for (int i = 0; i < objNumbers.Count ; i++)
                {
                    if (!string.IsNullOrEmpty(objNumbers[i]))
                    {
                        txtTo.Text += Convert.ToString(objNumbers[i]) + ",";
                    }
                }

                txtTo.Text = txtTo.Text.Remove(txtTo.Text.Length - 1, 1);
            }
            //txtTo.Text = Numbers;
            txtMessage.Select();

        }

        private int AccountSMSType = Enums.SMSACCOUNT_TYPE.MODEMSMS;

        public frmSMSAll(string No, string Message,int smsType)
        {
            InitializeComponent();
            txtTo.Text = No;
            txtMessage.Text = Message;
            txtMessage.Select();

            this.AccountSMSType = smsType;
        }


        //public void SMSTo(string MessageTemplate, string MobileNo, string BuncnName)
        //{
        //    SMSBunchMessages = true;
        //    txtTo.Text = MobileNo;
        //    txtMessage.Text = MessageTemplate;
        //    BunchNo = BuncnName;
        //    txtMessage.ReadOnly = true;
        //}
        //public void SMSTo(int TemplateID, string MessageTemplate, string MobileNo, string BuncnName, int TotalNo, int PickUpTotal)
        //{
        //    SMSBunchMessages = true;
        //    txtTo.Text = MobileNo;
        //    txtMessage.Text = MessageTemplate;
        //    BunchNo = BuncnName;
        //    MessageTemplateId = TemplateID;
        //    TotalNos = TotalNo;
        //    TotalPickUps = PickUpTotal;
        //    txtMessage.ReadOnly = true;
        //}



        public void SMSTo(int TemplateID, string MessageTemplate, string MobileNo, string BuncnName, int TotalNo, int PickUpTotal,int BunchValues)
        {
            SMSBunchMessages = true;
            txtTo.Text = MobileNo;
            txtMessage.Text = MessageTemplate;
            BunchNo = BuncnName;
            MessageTemplateId = TemplateID;
            TotalNos = TotalNo;
            TotalPickUps = PickUpTotal;
            BunchValue = BunchValues;
            MessageText = MessageTemplate;
            txtMessage.Enabled = false;
            if (MessageTemplateId == 0)
            {
                var query = General.GetObject<SMSBunch>(c => c.MessageTemplate == MessageTemplate);


                if (query != null)
                {
                    MessageTemplateId = query.Id.ToInt();

                    //  objSMSBunches.GetByPrimaryKey(query.Id);
                }
            }
        }

        public void Save()
        {
            try
            {
                if (objSMSBunches.PrimaryKeyValue == null)
                {
                    objSMSBunches.New();
                }
                else
                {
                    objSMSBunches.Edit();
                }

                objSMSBunches.Current.MessageTemplate = MessageText;
                objSMSBunches.Current.MessageValue = BunchValue;
                objSMSBunches.Save();
                MessageTemplateId = objSMSBunches.Current.Id;
               
            }
            catch (Exception ex)
            {
                if (objSMSBunches.Errors.Count > 0)
                    ENUtils.ShowMessage(objSMSBunches.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }

        public void SendSMSBunch()
        {
            try
            {
                if (MessageTemplateId == 0)
                {
                    Save();
                }
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.stp_SMSBunchDetal(MessageTemplateId, BunchNo, TotalNos, TotalPickUps);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void frmSMSAll_Load(object sender, EventArgs e)
        {
            try
            {
                RadMenuItem item = null;
                item = new RadMenuItem();
                item.Text = "Customer";
                item.Click += new EventHandler(item_Click);
                btnPickEmail.Items.Add(item);

                item = new RadMenuItem();
                item.Text = "Driver";
                item.Click += new EventHandler(item_Click);
                btnPickEmail.Items.Add(item);


                item = new RadMenuItem();
                item.Text = "Company";
                item.Click += new EventHandler(item_Click);
                btnPickEmail.Items.Add(item);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void item_Click(object sender, EventArgs e)
        {

            try
            {

                RadItem item = (RadItem)sender;


                if (item.Text.ToLower() == "customer")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Customer>(c => c.MobileNo != null && c.MobileNo != string.Empty)
                                select new
                                {
                                    Id = a.Id,
                                    Name = a.Name,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");


                    if (obj != null)
                    {
                        //foreach (object[] data in obj)
                        //{
                        //    txtTo.Text += data[2].ToString() + ",";

                        //}


                        txtTo.Text += string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());

                        //for (int i = 0; i < obj.Count; i++)
                        //{
                        //    txtTo.Text += obj[i][2].ToString() + ",";

                        //}



                    }
                }
                else if (item.Text.ToLower() == "driver")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(c => c.MobileNo != null && c.MobileNo != string.Empty && (c.IsActive != null && c.IsActive == true))
                                .AsEnumerable().OrderBy(i => i.DriverNo, new NaturalSortComparer<string>())
                                select new
                                {
                                    Id = a.Id,
                                    No = a.DriverNo,
                                    Name = a.DriverName,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");


                    if (obj != null)
                    {
                        foreach (object[] data in obj)
                        {
                            txtTo.Text += data[3].ToString() + ",";

                        }
                    }
                }
                else if (item.Text.ToLower() == "company")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Gen_Company>(c => c.MobileNo != null && c.MobileNo != string.Empty)
                                select new
                                {
                                    Id = a.Id,

                                    Name = a.CompanyName,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");


                    if (obj != null)
                    {
                        foreach (object[] data in obj)
                        {
                            txtTo.Text += data[2].ToString() + ",";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                btnSendEmail.Enabled = false;
                string error = string.Empty;

                string msg = txtMessage.Text.Trim();


                if (string.IsNullOrEmpty(txtTo.Text.Trim()))
                {
                    error += "Required : To" + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(msg))
                {
                    error += "Cannot send empty message..";
                }


                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }

                EuroSMS sms = new EuroSMS();


                string[] toNumbers = txtTo.Text.Split(',').Where(c => c != string.Empty).Distinct().ToArray<string>();



                Thread smsThread = new Thread(delegate()
                {

                
                  
                        SendSMS(sms, toNumbers, msg);


                        if (SMSBunchMessages == true)
                        {
                            SendSMSBunch();
                            SMSBunchMessages = false;
                            //txtMessage.ReadOnly = false;
                        }
                    
                });

                smsThread.Start();



               General.SaveSentSMS(msg, txtTo.Text.ToStr());


                if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                {

                    RadDesktopAlert alert = new RadDesktopAlert();
                    alert.CaptionText = "message sent successfully";

                    alert.ContentText = msg;
                    alert.ContentImage = Resources.Resource1.email;
                    alert.Show();

                }

                this.Close();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
            finally
            {

                btnSendEmail.Enabled = true;

            }




        }


        private void SendSMS(EuroSMS sms, string[] toNumbers, string msg)
        {

            // string[] toNumbers = txtTo.Text.Split(',').Where(c => c != string.Empty).Distinct().ToArray<string>();


            string smsError1 = string.Empty;
            //   EuroSMS sms = new EuroSMS();
            int idx = -1;
            string fullMobNo = string.Empty;

            foreach (var mobNo in toNumbers)
            {

                fullMobNo = mobNo;


                if (!Debugger.IsAttached)
                {

                    if (fullMobNo.ToStr().StartsWith("00") == false)
                    {

                        if (fullMobNo.StartsWith("044") == true)
                        {
                            idx = fullMobNo.IndexOf("044");
                            fullMobNo = fullMobNo.Substring(idx + 3);
                            fullMobNo = fullMobNo.Insert(0, "+44");

                        }

                        if (fullMobNo.StartsWith("07"))
                        {
                            fullMobNo = fullMobNo.Substring(1);
                        }

                        if (fullMobNo.StartsWith("0440") == false || fullMobNo.StartsWith("+440") == false)
                            fullMobNo = fullMobNo.Insert(0, "+44");
                    }
                }

                sms.ToNumber = fullMobNo;
                sms.Message = msg;
                sms.BookingSMSAccountType = this.AccountSMSType;


                if(AppVars.enableSMSService.ToBool()==false)
                   System.Threading.Thread.Sleep(3000);

                sms.Send(ref smsError1);

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTo.Text = string.Empty;
            txtMessage.Enabled = true;
            txtMessage.Text = string.Empty;
            SMSBunchMessages = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnPickTemplet_Click(object sender, EventArgs e)
        {
            var list = (from a in AppVars.BLData.GetQueryable<Gen_SysPolicy_SMSTemplet>(c => c.Id != null)
                        select new
                        {
                            Id = a.Id,
                            Templates = a.Templet,

                        }).ToList();

            object[] obj = General.ShowFormLister(list, "Id", true);
            if (obj != null)
            {
                txtMessage.Text = obj[2].ToString();
            }
        }

        private void btnPickSMSBunch_Click(object sender, EventArgs e)
        {
            frmClientSMSBunch frm = new frmClientSMSBunch(txtMessage.Text);
            frm.ShowDialog();
        }

        private void btnPickBunch_Click(object sender, EventArgs e)
        {
            frmPickClientSMSBunch frm = new frmPickClientSMSBunch();
            frm.ShowDialog();
        }
    }
}
