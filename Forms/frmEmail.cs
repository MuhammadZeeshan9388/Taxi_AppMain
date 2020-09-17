using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Microsoft.Reporting.WinForms;
using System.Net.Mail;
using System.IO;
using Utils;
using Telerik.WinControls.UI;
using Taxi_Model;
using System.Linq;
using System.Drawing.Imaging;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmEmail : Forms.BaseForm
    {
    //    string ReportFor = "";

        private void LoadDefaultSettings()
        {
            txtBody.Text = AppVars.objPolicyConfiguration.DefaultEmailBody.ToStr().Trim();

        }


        System.Windows.Forms.Timer timer1 = null;

        Image img = null;

        private Gen_SubCompany objSubcompany;

        public frmEmail(ReportViewer viewer, string attachmentTitle, string ComapnyEmail, Gen_SubCompany objEmailDetails)
        {
            InitializeComponent();
            this.ReportViewer1 = viewer;

            objEmailDetails.IsTpCompany =( ReportViewer1.Tag.ToStr() == "invoice" && objEmailDetails!=null && objEmailDetails.UseDifferentEmailForInvoices.ToBool())? true : false;

            attachmentTitle = attachmentTitle.Replace("/", "").Trim();

            this.FileTitle = attachmentTitle;
            txtAttachment.Text = FileTitle;

            txtFrom.Text = (viewer.Tag.ToStr() == "invoice" && objEmailDetails!=null && objEmailDetails.SmtpInvoiceUserName.ToStr().Trim().Length >0)? objEmailDetails.SmtpInvoiceUserName.ToStr().Trim() : objEmailDetails.EmailAddress.ToStr().Trim();

            txtTo.Text = ComapnyEmail;


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


            objSubcompany = objEmailDetails;

            LoadDefaultSettings();

            txtSubject.Focus();
            txtSubject.Select();



            this.FormClosed += new FormClosedEventHandler(frmEmail_FormClosed);

        }

    

        public frmEmail(ReportViewer viewer,string attachmentTitle)
        {
            InitializeComponent();
            this.ReportViewer1 = viewer;
            this.FileTitle = attachmentTitle;
            txtAttachment.Text = FileTitle;
            txtFrom.Text = AppVars.LoginObj.Email.ToStr().Trim();
            LoadDefaultSettings();

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



            this.FormClosed += new FormClosedEventHandler(frmEmail_FormClosed);
          
            
        }



        public frmEmail(Image viewer, string attachmentPath)
        {
            InitializeComponent();
            this.img = viewer;
            this.FileTitle = attachmentPath;
          //  txtAttachment.Text = FileTitle;
            txtFrom.Text = AppVars.LoginObj.Email.ToStr().Trim();

            LoadDefaultSettings();

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


          


            this.FormClosed += new FormClosedEventHandler(frmEmail_FormClosed);

        }

        void frmEmail_FormClosed(object sender, FormClosedEventArgs e)
        {

            try
            {
                if (timer1 != null)
                {
                    timer1.Stop();
                    timer1.Dispose();
                    timer1 = null;
                }
            }
            catch
            {

            }

            this.Dispose(true);
            GC.Collect();
        }



       
        
        public frmEmail(ReportViewer viewer, string attachmentTitle, string ComapnyEmail)
        {
            InitializeComponent();
            this.ReportViewer1 = viewer;

            attachmentTitle = attachmentTitle.Replace("/", "").Trim();

            this.FileTitle = attachmentTitle;
            txtAttachment.Text = FileTitle;
          
            txtFrom.Text = AppVars.LoginObj.Email.ToStr().Trim();
        
            txtTo.Text = ComapnyEmail;


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

            LoadDefaultSettings();

            txtSubject.Focus();
            txtSubject.Select();

            

            this.FormClosed+=new FormClosedEventHandler(frmEmail_FormClosed);

        }

        void item_Click(object sender, EventArgs e)
        {

            try
            {

                RadItem item = (RadItem)sender;


                if (item.Text.ToLower() == "customer")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Customer>(c => c.Email != null && c.Email != string.Empty)
                                select new
                                {
                                    Id = a.Id,
                                    Name = a.Name,
                                    Email = a.Email,
                                    Telephone = a.TelephoneNo,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    //object[] obj = General.ShowFormLister(list, "Id");
                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");


                    if (obj != null)
                    {
                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            //txtTo.Text = obj[3].ToString();
                            txtTo.Text +=","+ string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }
                        else
                        {
                            txtTo.Text = string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }
                    }
                }
                else if (item.Text.ToLower() == "driver")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(c => c.Email != null && c.Email != string.Empty && c.IsActive==true)
                                select new
                                {
                                    Id = a.Id,
                                    No = a.DriverNo,
                                    Name = a.DriverName,
                                    Email = a.Email,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    //object[] obj = General.ShowFormLister(list, "Id");

                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");

                    if (obj != null)
                    {

                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            //  txtTo.Text = obj[3].ToString();

                            txtTo.Text +=","+ string.Join(",", obj.Select(c => c[3].ToString()).ToArray<string>());
                        }
                        else
                        {
                            txtTo.Text = string.Join(",", obj.Select(c => c[3].ToString()).ToArray<string>());
                        }

                    }
                }
                else if (item.Text.ToLower() == "company")
                {

                    var list = (from a in AppVars.BLData.GetQueryable<Gen_Company>(c => c.Email != null && c.Email != string.Empty)
                                select new
                                {
                                    Id = a.Id,
                                 
                                    Name = a.CompanyName,
                                    Email = a.Email,
                                    MobileNo = a.MobileNo,

                                }).ToList();
                    //object[] obj = General.ShowFormLister(list, "Id");

                    List<object[]> obj = General.ShowFormMultiLister(list, "Id");

                    if (obj != null)
                    {

                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            //  txtTo.Text = obj[3].ToString();

                            txtTo.Text += "," + string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }
                        else
                        {
                            txtTo.Text = string.Join(",", obj.Select(c => c[2].ToString()).ToArray<string>());
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        ReportViewer _ReportViewer1;

        //System.Windows.Forms.DataVisualization.Charting.Chart _Graph1;

        //public System.Windows.Forms.DataVisualization.Charting.Chart Graph1
        //{
        //    get { return _Graph1; }
        //    set { _Graph1 = value; }
        //}

        

        

        public ReportViewer ReportViewer1
        {
          get { return _ReportViewer1; }
          set { _ReportViewer1 = value; }
        }


        private void btnSendEmail_Click(object sender, EventArgs e)
        {


            if (this.img != null)
            {
                SendEmailImage();
            }
            else
            {

                SendEmail();
                //}
            }
        }


        private void SendEmailImage()
        {
            ToEmail = txtTo.Text.Trim();
            EmailSubject = txtSubject.Text;
            string from = txtFrom.Text.Trim();


            if (string.IsNullOrEmpty(from))
            {
                ENUtils.ShowMessage("Required : From");
                return;

            }

            if (string.IsNullOrEmpty(ToEmail))
            {
                ENUtils.ShowMessage("Required : To");
                return;


            }


            if(File.Exists(this.FileTitle))
            {

                List<Attachment> myAttach = new List<Attachment>();
                myAttach.Add(new System.Net.Mail.Attachment(this.FileTitle.ToStr()));

                Taxi_AppMain.Email.Send(EmailSubject, "", from, ToEmail, myAttach);

                //      ENUtils.ShowMessage("Email has been Sent Successfully");

                myAttach[0].Dispose();

                File.Delete(this.FileTitle);

            }


            this.Close();

        }


        private string _EmailSubject;

        public string EmailSubject
        {
            get { return _EmailSubject; }
            set { _EmailSubject = value; }
        }


        private string _ToEmail;

        public string ToEmail
        {
            get { return _ToEmail; }
            set { _ToEmail = value; }
        }


        public void SendEmail()
        {
           ToEmail = txtTo.Text.Trim();
            EmailSubject = txtSubject.Text;
            string from = txtFrom.Text.Trim();


            string body = txtBody.Text.Trim();

            if (body.Length == 0)
                body = EmailSubject;

            body = body.Replace("\r\n", "<br/>").Trim();

            if (string.IsNullOrEmpty(from))
            {
                ENUtils.ShowMessage("Required : From");
                return;

            }

            if (string.IsNullOrEmpty(ToEmail))
            {
                ENUtils.ShowMessage("Required : To");
                return;


            }

            string exportType = ddlExportType.Text.ToStr().Trim();


            btnSendEmail.Enabled = false;

            if (this.IsInternalEmail == false)
            {
                if (timer1 == null)
                {
                    timer1 = new System.Windows.Forms.Timer();
                    timer1.Enabled = true;
                    timer1.Tick += FrmEmail_Tick;
                    timer1.Interval = 500;
                    timer1.Start();


                }
            }



            new Thread(delegate ()
            {
                //Thread.Sleep(1000);
                //CalculateAutoFareUI();

                EmailReport(exportType.ToLower(), from, ToEmail, EmailSubject, body);


                

            }).Start();



            //EmailReport(exportType.ToLower(), from, ToEmail, EmailSubject, body);

        }


        private bool EmailSent = false;

        private void FrmEmail_Tick(object sender, EventArgs e)
        {
            try
            {
                if (EmailSent)
                {
                    timer1.Stop();
                    timer1.Dispose();
                    timer1 = null;
                    Close();
                }
            }
            catch
            {


            }
           
        }

        private bool IsInternalEmail;

        public bool IsEmailSent = false;
        public void SendEmail(bool sendInternally)
        {
            this.IsInternalEmail = sendInternally;
            ToEmail = txtTo.Text.Trim();
            

            EmailSubject = txtSubject.Text;
            string from = txtFrom.Text.Trim();
            
            string body = txtBody.Text.Trim();

            if (body.Length == 0)
            {



                body = EmailSubject;



            }

            body = body.Replace("\r\n", "<br/>").Trim();

            if (string.IsNullOrEmpty(from))
            {
                ENUtils.ShowMessage("Required : From");
                return;

            }

            if (string.IsNullOrEmpty(ToEmail))
            {
                ENUtils.ShowMessage("Required : To");
                return;


            }

            string exportType = ddlExportType.Text.ToStr().Trim();




           IsEmailSent= EmailReport(exportType.ToLower(), from, ToEmail, EmailSubject, body);

        }


        string _FileTitle;

        public string FileTitle
        {
            get { return _FileTitle; }
            set { _FileTitle = value; }
        }



       public bool EmailReport(string reportType,string fromEmail, string toEmail, string subject, string messageBody)
        {
            bool rtn = true;
            try
            {

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = ReportViewer1.LocalReport.Render(
                 reportType, null, out mimeType, out encoding, out extension, out streamids, out warnings);



                string path = General.GetSharedNetworkPath().ToStr().Trim();
                if (path == string.Empty || Directory.Exists(path) == false)
                {
                    path = Application.StartupPath;

                  
                }
            
                if (reportType.ToStr().ToLower() == "excel")
                {
                    reportType = "xls";
                }

                if(objSubcompany==null)
                {
                    objSubcompany = AppVars.objSubCompany;
                }

                if (objSubcompany != null)
                {

                    objSubcompany.IsTpCompany = ReportViewer1.Tag.ToStr() == "invoice" && objSubcompany != null && objSubcompany.UseDifferentEmailForInvoices.ToBool() ? true : false;
                }



                path += "\\"+FileTitle + "." + reportType;

                FileInfo file = new FileInfo(path);

                using (FileStream fs = file.Create())
                {

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
               

                    List<Attachment> myAttach = new List<Attachment>();
                    myAttach.Add(new System.Net.Mail.Attachment(file.FullName));

                    Taxi_AppMain.Email.Send(subject, messageBody, fromEmail, toEmail, myAttach, objSubcompany);
             

                    myAttach[0].Dispose();

                    File.Delete(path);                 
                    
                    
                   
                }


                if (this.IsInternalEmail == false)
                {
                    try
                    {
                        this.Close();

                    }
                    catch
                    {
                        EmailSent = true;

                    }
                }
            }
            catch (Exception ex)
            {
                rtn = false;
                if (this.IsInternalEmail == false)
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }

            return rtn;
        }

       //public static void EmailImage(System.Windows.Forms.DataVisualization.Charting.Chart GraphType, string fileTitle, string toEmail, string subject, string messageBody)
       //{
       //    try
       //    {
       //        ImageFormat format = ImageFormat.Jpeg;
       //        string path = System.Windows.Forms.Application.StartupPath + "\\";
       //        if (path == string.Empty)
       //        {
       //            ENUtils.ShowMessage("Please check your shared network path before sending email.");
       //            return;
       //        }

       //        path += fileTitle + ".jpg";


       //        FileInfo file = new FileInfo(path);



       //        using (FileStream fs = file.Create())
       //        {
       //            fs.Flush();
       //            fs.Close();
       //            fs.Dispose();
       //            GraphType.SaveImage(file.FullName, format);
       //            //Graph.SaveImage(@"D:\Despatch System\New_Project\Taxi (New)\Taxi_AppMain\bin\Debug\Graphs4567.jpg", format);


       //            List<Attachment> myAttach = new List<Attachment>();
       //            myAttach.Add(new System.Net.Mail.Attachment(file.FullName));

       //           Taxi_AppMain.Email.Send(subject, messageBody, AppVars.LoginObj.Email, toEmail, myAttach);

       //            ENUtils.ShowMessage("Email has been Sent Successfully");

       //            myAttach[0].Dispose();

       //            File.Delete(path);



       //        }



       //    }
       //    catch (Exception ex)
       //    {
       //        ENUtils.ShowMessage(ex.Message);

       //    }

       //}

       private void btnEditFrom_Click(object sender, EventArgs e)
       {
           txtFrom.Enabled = true;
       }

       private void frmEmail_Load(object sender, EventArgs e)
       {

       }

       //private void btnPickEmail_Click(object sender, EventArgs e)
       //{
       //    try
       //    {



       //        var list = (from a in AppVars.BLData.GetQueryable<Customer>(c => c.Email != null && c.Email != string.Empty)
       //                    select new
       //                    {
       //                        Id = a.Id,
       //                        Name = a.Name,
       //                        Email = a.Email,
       //                        Telephone = a.TelephoneNo,
       //                        MobileNo = a.MobileNo,

       //                    }).ToList();
       //        object[] obj = General.ShowFormLister(list, "Id");


       //        if (obj != null)
       //        {
       //            txtTo.Text = obj[3].ToString();

       //        }
       //    }
       //    catch (Exception ex)
       //    {

       //        ENUtils.ShowMessage(ex.Message);
       //    }

       //}

    }
}
