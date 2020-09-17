using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Taxi_AppMain.Forms;

using System.Data;
using Taxi_AppMain.Classes;
using Taxi_Model;
using Utils;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.Net;

using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using Taxi_AppMain.Report_Classes;

namespace Taxi_AppMain
{
    static class Program
    {
        public static string[] onrestartArgs = null;
        public static DataSet dtCombos=null;
       private static   DateTime? lastExceptionOn = null;

        private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static ClsLic objLic =  new ClsLic();

       

        [STAThread]
        static void Main(string[] args)
        {


           

            if (args != null && args.Count() > 0)
                onrestartArgs = args;



            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                  //  MessageBox.Show("Instance already running");
                    Environment.Exit(0);
                    return;
                }
                else
                {

                    if (args != null && args.Count() > 0)
                    {


                        foreach (var item in Process.GetProcesses().Where(c => c.ProcessName.ToLower() == "taxi_appmain"))
                        {

                            if (item.Id != Process.GetCurrentProcess().Id)
                            {

                                item.Kill();

                                item.Close();

                            }
                        }




                    }

                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);


                try
                {


                    //try
                    //{

                       

                    //    //"token=abcde123&destination=01142994032&extension=200&callerId=08458000100" https://portal.vipvoip.co.uk/vipVoipAPI/makeCall

                    //    string destination = "07907270379";

                    //    string webAddress = "https://portal.vipvoip.co.uk/vipVoipAPI/makeCall?token=" + "68a810fa5cd008c23ab9758729be5b6a" + " &destination=" + destination + "&extension=" + "210" + "&callerId=" + "";
                    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webAddress);
                    //    request.Proxy = null;
                    //    request.Timeout = 8000;

                    //    request.ContentType = "application/csv;charset=UTF-8";
                    //    request.Method = "POST";
                    //    String responseString = string.Empty;

                    //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    //    {
                    //        using (Stream stream = response.GetResponseStream())
                    //        {
                    //            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                    //            responseString = reader.ReadToEnd();
                    //        }

                    //        //  WriteLog(responseString);

                    //      //  WriteLog(responseString);
                    //     //   return true;

                    //    }
                    //}

                    //catch (Exception ex)
                    //{
                    //  //  WriteLog(ex.Message);



                    //  //  return false;
                    //    //Console.WriteLine("\nException Caught!");
                    //    //Console.WriteLine("Message :{0} ", ex.Message);
                    //    //responseBody = string.Empty;
                    //    //return responseBody;
                    //}

                    Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);


                    //if (Debugger.IsAttached)
                    //    StartApp();
                    //else
                    //{

                        if (VerifyLicense())
                        {
                            if (string.IsNullOrEmpty(clientName.ToStr()))
                            {
                                throw new Exception(throwMsg);
                            }

                           

                            StartApp();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(throwMsg.ToStr().Trim()))
                            {
                                Application.Run(new frmLicenseKey());
                            }
                            else
                            {
                                throw new Exception(throwMsg);

                            }

                        }
                  //  }
                }
                catch (Exception ex)
                {
                    ENUtils.ShowMessage(ex.Message);
                    new TaxiDataContext().stp_AddLog(ex.Message + ",Target : " + ex.TargetSite + ",Source : " + ex.Source + ",Stack Trace :" + ex.StackTrace, "Program", "");

                }

                finally
                {

                 //   Process.GetCurrentProcess().Kill();
                     Environment.Exit(0);

                }     
               
            }
               
        }

     
       

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
             
            try
            {
                if (lastExceptionOn == null || DateTime.Now.AddMinutes(-2) > lastExceptionOn)
                {

                    lastExceptionOn = DateTime.Now;
                    new TaxiDataContext().stp_AddLog(e.Exception.Message + ",Target : "+ e.Exception.TargetSite + ",Source : "+e.Exception.Source+ ",Stack Trace :" + e.Exception.StackTrace,Environment.MachineName, "");
                   
                }
            }
            catch (Exception ex)
            {


            }

        }

       static  string clientName = string.Empty;
       static string throwMsg = string.Empty;
        private static  bool VerifyLicense()
        {

            bool verify = false;
          
            try
            {
                throwMsg = string.Empty;


                clientName = General.GetObject<Gen_SysPolicy_Configuration>(c => c.SysPolicyId != null).DefaultIfEmpty().DefaultClientId;

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\SysData.dll")
                    && Cryptography.Decrypt(File.ReadAllText(System.Windows.Forms.Application.StartupPath + "\\SysData.dll"), (clientName+"!@#"), true).Equals((clientName)))
                {


                    verify = true;
                    AppVars.LicenseChecked = true;
                }

                else
                {


                    try
                    {

                        if (clientName.ToStr().Trim().Length == 0)
                        {
                            throwMsg = "Authentication Failed...";

                        }
                        else
                        {


                            
                           

                            verify = General.VerifyLicense(clientName);

                            if (verify == false)
                            {
                                if (objLic.Reason.ToStr().Trim().ToLower().Contains("could not be resolved"))
                                {
                                    throwMsg = "Authentication Failed...";
                                }
                                else
                                {
                                    throwMsg = objLic.Reason.ToStr();
                                }
                            }

                         

                        }

                    }
                    catch (Exception ex)
                    {
                        throwMsg = ex.Message.ToStr();

                    }





                    //using (LicDataContextDataContext db = new LicDataContextDataContext())
                    //{


                    //    stp_SysPolicyAuthResult objClient = db.stp_SysPolicyAuth(clientName).FirstOrDefault();

                    //    if (objClient != null)
                    //    {

                    //        if (objClient.ScriptType.ToStr().ToLower() == "valid")
                    //        {
                    //            verify = true;
                    //        }

                    //        AppVars.LicenseExpiryDate = "License will Expire on " + string.Format("{0:dd/MMM/yyyy HH:mm}", objClient.LastCondition.ToDateTimeorNull());
                    //    }
                    //    else
                    //    {

                    //        MessageBox.Show("Authentication Failed...");

                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throwMsg = ex.Message.ToStr();

                //if (ex.Message.ToLower().StartsWith("a network-related") && string.IsNullOrEmpty(clientName))
                //    verify = true;


                //else if (ex.Message.ToLower().Contains("invalid column"))
                //{
                //    MessageBox.Show(ex.Message);
                //    verify = true;

                //}
                //else
                //    verify = VerifySystemLicense();

              //  return true;
            }


            return verify;
        }

        public static string Base64Encode(string plainText) {
              var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
              return System.Convert.ToBase64String(plainTextBytes);
        }


        public static void StartApp()
        {

            try
            {
                //Application.Run(new demo_repor());
                if (onrestartArgs == null)
                    Application.Run(new frmLogin());
                else
                    Application.Run(new frmLogin(onrestartArgs));


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }




    }
}
