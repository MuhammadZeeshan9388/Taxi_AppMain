using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using System.Web;
using System.Diagnostics;
using Utils;
using Taxi_BLL;
using System.IO.Ports;
using Telerik.WinControls.UI;
using System.Threading;
using System.Windows.Forms;

namespace Taxi_AppMain
{
   public  class EuroSMS
    {
        private string _SMSUserName;

        public string SMSUserName
        {
            get { return _SMSUserName; }
            set { _SMSUserName = value; }
        }
        private string _SMSUserPassword;
        
        public string SMSUserPassword
        {
            get { return _SMSUserPassword; }
            set { _SMSUserPassword = value; }
        }

       public static string SMSFromCaption="";


       private int _BookingSMSAccountType;

       public int BookingSMSAccountType
       {
           get { return _BookingSMSAccountType; }
           set { _BookingSMSAccountType = value; }
       }

       public EuroSMS(string userName,string password,string fromCaption)
       {

           this.SMSUserName = userName;
           this.SMSUserPassword = password;
           SMSFromCaption = fromCaption;


       }
     
       public EuroSMS()
       {
          
       }

       private string _Message;

       public string Message
       {
           get { return _Message; }
           set { _Message = value; }
       }

       private string _ToNumber;

       public string ToNumber
       {
           get { return _ToNumber; }
           set {
          
               _ToNumber = value;
         
             }
       }

       private string _CountryCode;

       public string CountryCode
       {
           get { return _CountryCode; }
           set { _CountryCode = value; }
       }


      


       public bool Send(ref string returnMsg)
       {
           bool rtn = true;


           //if (Debugger.IsAttached)
           //    return true;

           try
           {
               if (AppVars.objSMSConfiguration == null) return false;


               int accountType = AppVars.objSMSConfiguration.SMSAccountType.ToInt();



               if (BookingSMSAccountType == 0 && accountType==Enums.SMSACCOUNT_TYPE.MODEMSMS)
                   BookingSMSAccountType = Enums.SMSACCOUNT_TYPE.MODEMSMS;

               if (accountType == Enums.SMSACCOUNT_TYPE.BULKSMS )
               {

                   string url = "http://www.bulksms.co.uk:5567/eapi/submission/send_sms/2/2.0";

                   string data = seven_bit_message(AppVars.objSMSConfiguration.BulkSMSUserName,AppVars.objSMSConfiguration.BulkSMSPassword, this.ToNumber, this.Message);
                   int idx = -1;
                   Hashtable result = send_sms(data, url);
                   if ((int)result["success"] == 1)
                   {
                       rtn = true;
                       returnMsg = formatted_server_response(result);


                   }
                   else
                   {
                       returnMsg = formatted_server_response(result);
                       rtn = false;


                       idx = returnMsg.IndexOf("|");
                       if (idx > 0)
                       {
                           returnMsg = returnMsg.Substring(idx + 1);

                           idx = returnMsg.IndexOf("|");
                           if (idx > 0)
                           {
                               returnMsg = returnMsg.Remove(idx);
                           }

                       }
                   }
               }
               else if (accountType == Enums.SMSACCOUNT_TYPE.CLICKATELL || BookingSMSAccountType==Enums.SMSACCOUNT_TYPE.CLICKATELL)
               {

                   string url = "http://api.clickatell.com/http/sendmsg";

                   string data = seven_bit_message(AppVars.objSMSConfiguration.ClickSMSUserName,AppVars.objSMSConfiguration.ClickSMSPassword,AppVars.objSMSConfiguration.ClickSMSApiKey,
                                                           AppVars.objSMSConfiguration.ClickSMSSenderName.ToStr().Trim(),this._ToNumber,this._Message);
                   string postMsg= Post(url, data);
                   if (postMsg.StartsWith("ERR"))
                   {

                       if (postMsg.StartsWith("ERR: 001") == true)
                       {
                           returnMsg = postMsg;
                           rtn = false;

                       }
                       else
                       {
                           General.AddLog(postMsg, "SMS : Click A Tell");
                          
                       }
                     
                   }
               }
               else if (accountType == Enums.SMSACCOUNT_TYPE.MODEMSMS || BookingSMSAccountType==Enums.SMSACCOUNT_TYPE.MODEMSMS)
               {

                   //try
                   //{
                   //    File.AppendAllText(Application.StartupPath + "\\remotesmsentry.txt", DateTime.Now.ToStr() + ":" + this._ToNumber.ToStr()+ ":" +Message);
                   //}
                   //catch
                   //{


                   //}

                   
                       if (Message.Length > 450)
                       {
                           Message = Message.Substring(0, 447);
                           Message += "...";
                       }


                    General.SendMessageToPDA("request dispatchsms=" + this._ToNumber.ToStr() + "=" + this.Message);
                    //   File.WriteAllText(Application.StartupPath + "\\SMSFiles\\" + this._ToNumber.ToStr() + "_" + string.Format("{0:ddMMyyhhmmssfff}", DateTime.Now), this.Message);




                }
           }
           catch (Exception ex)
           {
               returnMsg = ex.Message;
               //if (returnMsg.ToStr().ToLower() == "response received is incomplete")
               //{
               //    rtn = true;
               //}
               //else
               //{
               //    rtn = false;
               //}

               //try
               //{
               //    File.AppendAllText(Application.StartupPath + "\\remotesmsexception.txt", DateTime.Now.ToStr() + ":" + returnMsg);
               //}
               //catch
               //{


               //}


               rtn = true;
           }


           return rtn;
       }


       public char[] specialCharacters=new char[] {':','(',')','#','/'};



       public static string formatted_server_response(Hashtable result)
       {
           string ret_string = "";
           if ((int)result["success"] == 1)
           {
               ret_string += "Success: batch ID " + (string)result["api_batch_id"] + "API message: " + (string)result["api_message"] + "\nFull details " + (string)result["details"];
           }
           else
           {
               ret_string += "Fatal error: HTTP status " + (string)result["http_status_code"] + " API status " + (string)result["api_status_code"] + " API message " + (string)result["api_message"] + "\nFull details " + (string)result["details"];
           }

           return ret_string;
       }

       public static Hashtable send_sms(string data, string url)
       {
           string sms_result = Post(url, data);

           Hashtable result_hash = new Hashtable();

           string tmp = "";
           tmp += "Response from server: " + sms_result + "\n";
           string[] parts = sms_result.Split('|');

           string statusCode = parts[0];
           string statusString = parts[1];

           result_hash.Add("api_status_code", statusCode);
           result_hash.Add("api_message", statusString);

           if (parts.Length != 3)
           {
               tmp += "Error: could not parse valid return data from server.\n";
           }
           else
           {
               if (statusCode.Equals("0"))
               {
                   result_hash.Add("success", 1);
                   result_hash.Add("api_batch_id", parts[2]);
                   tmp += "Message sent - batch ID " + parts[2] + "\n";
               }
               else if (statusCode.Equals("1"))
               {
                   // Success: scheduled for later sending.
                   result_hash.Add("success", 1);
                   result_hash.Add("api_batch_id", parts[2]);
               }
               else
               {
                   result_hash.Add("success", 0);
                   tmp += "Error sending: status code " + parts[0] + " description: " + parts[1] + "\n";
               }
           }
           result_hash.Add("details", tmp);
           return result_hash;
       }

       public static string Post(string url, string data)
       {

           string result = null;
           try
           {
               byte[] buffer = Encoding.Default.GetBytes(data);

               HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);
         
             

               WebReq.Method = "POST";
               WebReq.ContentType = "application/x-www-form-urlencoded";
               WebReq.ContentLength = buffer.Length;
               Stream PostData = WebReq.GetRequestStream();

               PostData.Write(buffer, 0, buffer.Length);
               PostData.Close();
               HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
               //Console.WriteLine(WebResp.StatusCode);

         

               Stream Response = WebResp.GetResponseStream();
               StreamReader _Response = new StreamReader(Response);
               result = _Response.ReadToEnd();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
               //Console.WriteLine(ex.Message);
           }
           return result.Trim() + "\n";
       }

       public static string character_resolve(string body)
       {
           Hashtable chrs = new Hashtable();
           chrs.Add('Ω', "Û");
           chrs.Add('Θ', "Ô");
           chrs.Add('Δ', "Ð");
           chrs.Add('Φ', "Þ");
           chrs.Add('Γ', "¬");
           chrs.Add('Λ', "Â");
           chrs.Add('Π', "º");
           chrs.Add('Ψ', "Ý");
           chrs.Add('Σ', "Ê");
           chrs.Add('Ξ', "±");

           string ret_str = "";
           foreach (char c in body)
           {
               if (chrs.ContainsKey(c))
               {
                   ret_str += chrs[c];
               }
               else
               {
                   ret_str += c;
               }
           }
           return ret_str;
       }

       public static string seven_bit_message(string username, string password, string msisdn, string message)
       {
         



           string data = "";
           data += "username=" + HttpUtility.UrlEncode(username, System.Text.Encoding.GetEncoding("ISO-8859-1"));
           data += "&password=" + HttpUtility.UrlEncode(password, System.Text.Encoding.GetEncoding("ISO-8859-1"));
           data += "&message=" + HttpUtility.UrlEncode(character_resolve(message), System.Text.Encoding.GetEncoding("ISO-8859-1"));
           data += "&msisdn=" + msisdn;
           data += "&want_report=1";
           data += "&sender=" + HttpUtility.UrlEncode(character_resolve(AppVars.objSMSConfiguration.BuikSMSCaption.ToStr()), System.Text.Encoding.GetEncoding("ISO-8859-1"));
   
           return data;
       }

       public static string seven_bit_message(string username, string password, string api_id, string from,string to, string message)
       {
           int concat = 1;

           if (message.Length > 150 && message.Length < 300)
               concat = 2;
           else if (message.Length > 300 && message.Length < 450)
               concat = 3;
           else if (message.Length > 450)
               concat = 4;

           string data = "";
           data += "user=" + HttpUtility.UrlEncode(username, System.Text.Encoding.GetEncoding("ISO-8859-1"));
           data += "&password=" + HttpUtility.UrlEncode(password, System.Text.Encoding.GetEncoding("ISO-8859-1"));
           data += "&api_id=" + HttpUtility.UrlEncode(api_id, System.Text.Encoding.GetEncoding("ISO-8859-1"));

           if (!string.IsNullOrEmpty(from))
           {
               data += "&from=" + HttpUtility.UrlEncode(from, System.Text.Encoding.GetEncoding("ISO-8859-1"));
           }
        

           data += "&to=" + HttpUtility.UrlEncode(to, System.Text.Encoding.GetEncoding("ISO-8859-1"));

           data += "&text=" + HttpUtility.UrlEncode(character_resolve(message), System.Text.Encoding.GetEncoding("ISO-8859-1"));
           data += "&concat=" + HttpUtility.UrlEncode(concat.ToStr(), System.Text.Encoding.GetEncoding("ISO-8859-1"));

           return data;
       }


     


     
    }
}
