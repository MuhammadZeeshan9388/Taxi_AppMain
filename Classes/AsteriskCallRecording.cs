using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;

using System.Net;
using System.Threading;

namespace Taxi_AppMain
{
    class AsteriskCallRecording
    {
        private static string Token;
        private static string responseBody;
        private static string[] array = { "00:00", "00:01", "00:02", "00:03", "00:04", "00:05", "00:06", "00:07", "00:08", "00:09", "00:10" };
        private string webAddress = string.Empty;
        private string CallPhoneNo = string.Empty;
        public AsteriskCallRecording(string token)
        {
            Token = token;
        }

        public bool DownloadFile(string phoneNo, string fromDateTime, string tillDateTime, string downloadPath)
        {

            CallPhoneNo = phoneNo;
            bool rtn = false;
            DateTime dtStart = Convert.ToDateTime(fromDateTime);
            DateTime dtEnd = Convert.ToDateTime(tillDateTime);
            DateTime dt = DateTime.Parse(fromDateTime.ToString());

            try
            {

                if (array.Where(x => x.Contains(dt.ToShortTimeString())).Count() > 0)
                {
                    dtStart = Convert.ToDateTime(fromDateTime).AddDays(-1).AddHours(23).AddMinutes(50);
                }

                var allRecordings = GetAllRecording(String.Format("{0:yyyy-MM-dd}", dtStart), String.Format("{0:yyyy-MM-dd}", dtEnd.AddDays(1)));
                if (allRecordings != null && allRecordings.Count() > 0)
                {
                    var result = allRecordings.Split('\n').Where(x => x.Contains(phoneNo)).ToList();

                    if (result.Count > 1)
                    {
                        List<string> finalResult = new List<string>();
                        finalResult = result.ToList();
                        foreach (var item in result)
                        {

                            DateTime dateStr = DateTime.Parse(item.Split(',')[2].Replace("T", " ").Trim());


                            if (!(dtStart <= dateStr && dtEnd >= dateStr))
                            {
                                finalResult.Remove(item);

                            }


                        }


                        var lastElement = finalResult.Last();
                        string[] strArray = lastElement.ToString().Split(',');
                        rtn = GetCallRecordingById(strArray[0], downloadPath);

                    }
                    else if (result.Count > 0)
                    {
                        string[] strArray = result.FirstOrDefault().Split(new char[] { ',' });
                        rtn = GetCallRecordingById(strArray[0], downloadPath);

                    }
                }
            }
            catch (Exception ex)
            {

                WriteLog(ex.Message);
            }

            return rtn;
        }

        public string GetAllRecording(string fromDateTime, string tillDateTime)
        {
            try
            {
                webAddress = "https://portal.vipvoip.co.uk/vipVoipAPI/vaultRecordings?token=" + Token + "&startDate=" + fromDateTime + "&endDate=" + tillDateTime;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webAddress);
                Thread.Sleep(2000);
                request.Timeout = 30000;

                request.ContentType = "application/csv;charset=UTF-8";
                request.Method = "POST";
                String responseString = string.Empty;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                        responseString = reader.ReadToEnd();
                    }

                  //  WriteLog(responseString);
                    return responseString;

                }
            }

            catch (Exception ex)
            {
                WriteLog(ex.Message);


                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
                responseBody = string.Empty;
                return responseBody;
            }
        }

        //public  Task<string> GetAllRecording(string fromDateTime, string tillDateTime)
        //{
        //    try
        //    {
        //        webAddress = "https://portal.vipvoip.co.uk/vipVoipAPI/vaultRecordings?token=" + Token + "&startDate=" + fromDateTime + "&endDate=" + tillDateTime;
        //        //?token=77a926b93c700a3f055570e00a0725a2&startDate=2018-02-01&endDate=2018-02-08
        //        // Create a New HttpClient object.
        //        HttpClient client = new HttpClient();
        //        //send  request asynchronously
        //        Task<HttpResponseMessage> response =  client.PostAsync(webAddress, null);
        //        response.Result.EnsureSuccessStatusCode();

        //        Task<string> responseBody = response.Result.Content.ReadAsStringAsync();

        //        return responseBody;
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        Console.WriteLine("\nException Caught!");
        //        Console.WriteLine("Message :{0} ", e.Message);
        //        responseBody = string.Empty;
        //        return new Task<string>(null);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("\nException Caught!");
        //        Console.WriteLine("Message :{0} ", ex.Message);
        //        responseBody = string.Empty;
        //        return new Task<string>(null);
        //    }
        //}

        public bool GetCallRecordingById(string callRecordingId, string downloadPath)
        {
            bool rtn = false;
          
            try
            {
                webAddress = "https://portal.vipvoip.co.uk/vipVoipAPI/getCallRecording?token=" + Token + "&callRecordingId=" + callRecordingId;
                //instance of HTTPClient
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webAddress);
                

                    request.ContentType = "application/csv;charset=UTF-8";
                    request.Method = "POST";
                    String responseString = string.Empty;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        BinaryReader bin = new BinaryReader(response.GetResponseStream());

                        byte[] buffer = bin.ReadBytes((Int32)response.ContentLength);

                        using (Stream writer = File.Create(downloadPath))
                        {
                            writer.Write(buffer, 0, buffer.Length);
                            writer.Flush();
                        }
                    }
                
                rtn = true;

                WriteLog("Success");
            }


            catch (Exception ex)
            {
                // For debugging
                WriteLog(ex.Message);
                Console.WriteLine(ex.ToString());
                rtn = false;
            }
          
            return rtn;
        }


        private void WriteLog(string message)
        {

            try
            {
                File.AppendAllText(Environment.CurrentDirectory + "CallRecordingLogs.txt", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : " +this.CallPhoneNo.ToString() + " : "+ message + Environment.NewLine);


            }
            catch
            {


            }

        }

        //public bool GetCallRecordingById(string callRecordingId, string downloadPath)
        //{
        //    bool rtn = false;
        //    try
        //    {
        //        webAddress = "https://portal.vipvoip.co.uk/vipVoipAPI/getCallRecording?token=" + Token + "&callRecordingId=" + callRecordingId;
        //        //instance of HTTPClient
        //        HttpClient client = new HttpClient();

        //        //send  request asynchronously            
        //        Task<HttpResponseMessage> response =  client.PostAsync(webAddress, null);

        //        // Check that response was successful or throw exception
        //        response.Result.EnsureSuccessStatusCode();

        //        // Read response asynchronously and save asynchronously to file
        //        using (FileStream fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
        //        {
        //            //copy the content from response to filestream
        //             response.Result.Content.CopyToAsync(fileStream);
        //        }
        //        rtn = true;
        //    }

        //    catch (HttpRequestException rex)
        //    {
        //        Console.WriteLine(rex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        // For debugging
        //        Console.WriteLine(ex.ToString());
        //    }
        //    return rtn;
        //}


    }
}
