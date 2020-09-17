using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for AndroidGCMPushNotification
/// </summary>
public class AndroidGCMPushNotification
{
    public string SendNotification(string deviceId, string message)
    {
        var value = message;
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";

        // --- text
        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
        string postData = "collapse_key=score_update&time_to_live=0&delay_while_idle=0&data.message="
            + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

        tRequest.Timeout = 8000;
      
        tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleApiKey));

      //  Console.WriteLine(postData);
        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse tResponse = tRequest.GetResponse();

        dataStream = tResponse.GetResponseStream();

        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();


        tReader.Close();
        dataStream.Close();
        tResponse.Close();
        return sResponseFromServer;
    }


  



    public string GoogleApiKey
    {
        get { return m_strGoogleAppID; }
        set { m_strGoogleAppID = value; }
    }


    public string SenderId
    {
        get { return m_strSenderId; }
        set { m_strSenderId = value; }
    }


    string m_strSenderId ;
    string m_strGoogleAppID ;
}