using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Linq.Expressions;
using DAL;
using System.IO;
using Utils;
using Taxi_Model;
using System.Reflection;
using Taxi_AppMain;


namespace Taxi_AppMain
{
   public  class Email
    {
       public static  List<Attachment> Attachments=new List<Attachment>();




       public static void Send(string subject, string Emailmessage, string FromEmail, string ToEmail, List<Attachment> attachments, Gen_SubCompany objSubCompany)
       {
           Attachments = attachments;

           Send(subject, Emailmessage, FromEmail, ToEmail, objSubCompany);

       }





       public static void Send(string subject, string Emailmessage, string FromEmail, string ToEmail, Gen_SubCompany objSubCompany)
       {


           Gen_SysPolicy_Configuration obj = Taxi_AppMain.General.GetObject<Gen_SysPolicy_Configuration>(c => c.SysPolicyId == 1);
           if (obj == null)
           {
               throw new Exception("Email Configuration is not defined in Settings.");


           }
           else
           {
               if (string.IsNullOrEmpty(obj.SmtpHost) || string.IsNullOrEmpty(obj.Port) || string.IsNullOrEmpty(obj.UserName) || string.IsNullOrEmpty(obj.Password))
               {
                   throw new Exception("InComplete Email Configuration. Please contact it to Admin.");

               }
           }

           using (MailMessage message = new MailMessage())
           {
               string emptyName = "...";

               string smptHost = AppVars.objPolicyConfiguration.SmtpHost;
               int port = AppVars.objPolicyConfiguration.Port.ToInt();
               string userName = obj.UserName.ToStr().Trim();
               string pwd = obj.Password.ToStr().Trim();
               string emailcc = AppVars.objSubCompany.EmailCC.ToStr().Trim();
               bool enableSSL = AppVars.objPolicyConfiguration.EnableSSL.ToBool();
               string companyName = AppVars.objSubCompany.CompanyName.ToStr();


               

               if (objSubCompany != null && objSubCompany.SmtpHost.ToStr().Trim().Length > 0)
               {

                   smptHost = objSubCompany.SmtpHost.ToStr().Trim();
                   port = objSubCompany.SmtpPort.ToInt();
                   userName = objSubCompany.SmtpUserName.ToStr().Trim();
                   pwd = objSubCompany.SmtpPassword.ToStr().Trim();
                   emailcc = objSubCompany.EmailCC.ToStr().Trim();
                   enableSSL = objSubCompany.SmtpHasSSL.ToBool();
                   companyName = objSubCompany.CompanyName.ToStr().Trim();


                   if (objSubCompany.IsTpCompany.ToBool() && objSubCompany.UseDifferentEmailForInvoices.ToBool())
                   {

                       smptHost = objSubCompany.SmtpInvoiceHost.ToStr().Trim();
                       port = objSubCompany.SmtpInvoicePort.ToInt();
                       userName = objSubCompany.SmtpInvoiceUserName.ToStr().Trim();
                       pwd = objSubCompany.SmtpInvoicePassword.ToStr().Trim();
                       enableSSL = objSubCompany.SmtpInvoiceSSL.ToBool();
                        FromEmail = userName;
                   }


               }

               foreach (var item in Attachments)
               {
                   emptyName = item.Name;

                   message.Attachments.Add(item);
               }

               System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smptHost, port);
               smtp.EnableSsl = enableSSL;

               NetworkCredential mailAuthentication = new NetworkCredential(userName, pwd);


               char[] arr = new char[] { ',' };
               string[] toArr = ToEmail.Split(arr);

               foreach (var item in toArr)
               {

                   message.To.Add(new MailAddress(item.Trim()));
               }

               if (string.IsNullOrEmpty(subject))
                   subject = emptyName;

               if (string.IsNullOrEmpty(Emailmessage))
                   Emailmessage = emptyName;


               if (emailcc.Length > 0)
               {

                   message.CC.Add(emailcc);
               }



               message.From = new MailAddress(FromEmail.Trim(), companyName);
               message.IsBodyHtml = true;
               message.Subject = subject;
               message.Body = Emailmessage;
               //      smtp.DeliveryMethod= SmtpDeliveryMethod.

               smtp.Credentials = mailAuthentication;

               FieldInfo transport = smtp.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
               FieldInfo authModules = transport.GetValue(smtp).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);

               Array modulesArray = authModules.GetValue(transport.GetValue(smtp)) as Array;
               modulesArray.SetValue(modulesArray.GetValue(3), 1);


               smtp.Send(message);
           }





       }


        public static void Send(string subject, string Emailmessage, string FromEmail, string ToEmail)
        {
          

            Gen_SysPolicy_Configuration obj =Taxi_AppMain.General.GetObject<Gen_SysPolicy_Configuration>(c=>c.SysPolicyId==1);
            if (obj == null)
            {
                throw new Exception("Email Configuration is not defined in Settings.");
              

            }
            else
            {
                if (string.IsNullOrEmpty(obj.SmtpHost) || string.IsNullOrEmpty(obj.Port) || string.IsNullOrEmpty(obj.UserName) || string.IsNullOrEmpty(obj.Password))
                {
                    throw new Exception("InComplete Email Configuration. Please contact it to Admin.");
                  
                }
            }

            using (MailMessage message = new MailMessage())
            {
                string emptyName = "...";
                foreach (var item in Attachments)
                {
                    emptyName = item.Name;

                    message.Attachments.Add(item);
                }

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(AppVars.objPolicyConfiguration.SmtpHost, AppVars.objPolicyConfiguration.Port.ToInt());
                smtp.EnableSsl = AppVars.objPolicyConfiguration.EnableSSL.ToBool();

                NetworkCredential mailAuthentication = new NetworkCredential(obj.UserName, obj.Password);
              

                char[] arr = new char[] { ',' };
                string[] toArr = ToEmail.Split(arr);

                foreach (var item in toArr)
                {

                    message.To.Add(new MailAddress(item.Trim()));
                }

                if (string.IsNullOrEmpty(subject))
                    subject = emptyName;

                if (string.IsNullOrEmpty(Emailmessage))
                    Emailmessage = emptyName;


                if (AppVars.objSubCompany.EmailCC.ToStr().Length > 0)
                {

                    message.CC.Add(AppVars.objSubCompany.EmailCC.ToStr().Trim());
                }

                //if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr().Trim() == "Ambercarswatford")
                //{
                //    message.CC.Add("mussarrat@ambers.co.uk");
                //}

                message.From = new MailAddress(FromEmail.Trim(), AppVars.objSubCompany.CompanyName.ToStr());
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = Emailmessage;
                //      smtp.DeliveryMethod= SmtpDeliveryMethod.

                smtp.Credentials = mailAuthentication;

                FieldInfo transport = smtp.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo authModules = transport.GetValue(smtp).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);

                Array modulesArray = authModules.GetValue(transport.GetValue(smtp)) as Array;
                modulesArray.SetValue(modulesArray.GetValue(3), 1);


              
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                 System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        { return true; };
               

                smtp.Send(message);
            }





        }


        public static void Send(string subject, string Emailmessage, string FromEmail, string ToEmail,List<Attachment> attachments)
        {
            Attachments = attachments;

            Send(subject, Emailmessage, FromEmail, ToEmail);

        }



        public static void EmailLicenseExpiry(Gen_SysPolicy_LCompany objLisc,string defaultClientId,string currentDateTime,string licenseExpiryDateTime,string expiredEvent)
        {

            try
            {

                MailMessage message = new MailMessage();

                string[] emailCredentials = Program.objLic.OtherInformation1.ToStr().Split(new string[] { ">>" }, StringSplitOptions.None)[1].Split(new string[] { "," }, StringSplitOptions.None);

                //if (objLic != null)
                //{



                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(emailCredentials[0].ToStr(), emailCredentials[1].ToInt());
                smtp.EnableSsl = emailCredentials[2].ToBool();

                    NetworkCredential mailAuthentication = new NetworkCredential(emailCredentials[3].ToStr(), emailCredentials[4].ToStr());

                    char[] arr = new char[] { ',' };
                    string[] toArr =Program.objLic.OtherInformation1.ToStr().Split(new string[] { ">>" }, StringSplitOptions.None)[0].ToStr().Split(arr);

                    foreach (var item in toArr)
                        message.To.Add(new MailAddress(item.Trim()));


                    string companyName = string.Empty;

                    if (AppVars.objSubCompany != null)
                        companyName = AppVars.objSubCompany.CompanyName.ToStr();
                    else
                        companyName = Taxi_AppMain.General.GetObject<Gen_MainCompany>(c => c.CompanyName != null).DefaultIfEmpty().CompanyName.ToStr();


                    message.From = new MailAddress(emailCredentials[3].ToStr());
                    message.IsBodyHtml = true;
                    message.Subject = companyName + " License has been Expired";
                    message.Body = "<b>Dear All,</b><br>" + message.Subject
                               + "<br><br>DefaultClientId : " + defaultClientId.ToStr()
                                 + "<br>License Expiry Date : " + string.Format("{0:dd/MMM/yyyy HH:mm}", licenseExpiryDateTime)
                                 + "<br>System Locked on : " + string.Format("{0:dd/MMM/yyyy HH:mm}", currentDateTime)
                                 +"<br>Event Occured on : " + expiredEvent.ToStr();

                    smtp.Credentials = mailAuthentication;

                    FieldInfo transport = smtp.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
                    FieldInfo authModules = transport.GetValue(smtp).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);

                    Array modulesArray = authModules.GetValue(transport.GetValue(smtp)) as Array;
                    modulesArray.SetValue(modulesArray.GetValue(3), 1);

                  
                        ServicePointManager.ServerCertificateValidationCallback =
                            delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                     System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                            { return true; };
                   
                    smtp.Send(message);
             //   }
            }
            catch (Exception ex)
            {

            }


        }
    }
}
