using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace Taxi_AppMain
{
 public   class ClsPaymentGatewayRequest
    {

     private string methName = string.Empty;


     public class CardDetailsEx
     {
         public string CardAddressCity;
         public string CardAddressLine1;

         public double amount;
         public string cardNumber;
         public string expiryDate;
         public string expiryMonth;
         public string expiryYear;
         public string cv2;
         public string currency;
         public string mobileNumber;
         public string emailAddress;
         public bool TestMode;
         public string paymentgateway;
         public string merchantid;
         public string merchantpassword;
         public string signature;
         public string cardtext;


     }

     public class ClsPaymentGatewayResponse
     {
         public string AuthCode = string.Empty;
         public bool success = false;
         public string Message = string.Empty;

     }


        private string APIURL = Program.objLic.AppServiceUrl;

        //Local APIURL
        //private string APIURL = "http://localhost:88/WebUploadFile.asmx";


        private XmlDocument CreateSoapEnvelope(string content)
        {
            string _soapEnvelope = @"<soap:Envelope
                                    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
                                    xmlns:xsd='http://www.w3.org/2001/XMLSchema'
                                    xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                                <soap:Body></soap:Body></soap:Envelope>";


            StringBuilder sb = new StringBuilder(_soapEnvelope);
            sb.Insert(sb.ToString().IndexOf("</soap:Body>"), content);

            // create an empty soap envelope
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(sb.ToString());

            return soapEnvelopeXml;
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        private HttpWebRequest CreateSOAPWebRequest(string action)
        {
            //Making Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(APIURL);
            //SOAPAction  
            Req.Headers.Add("SOAPAction", "http://tempuri.org/" + action);
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";
            //return HttpWebRequest  
            return Req;
        }

                

        public string MakePayment(string methodName, string jsonString, string dataVal)
        {
            this.methName=methodName;

            string response = string.Empty;
            //Calling CreateSOAPWebRequest method  


            HttpWebRequest request = CreateSOAPWebRequest(this.methName);

             jsonString=  Cryptography.Encrypt(jsonString,dataVal , true);

             MakePaymentByGatewayDispatch MakePayment = new MakePaymentByGatewayDispatch()
            {
             defaultclientid=0,
               jsonString = jsonString,
                dataVal = dataVal,
            };

            
            string xmlBody = ToXML(MakePayment)
                            .Replace(@"xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""", @"xmlns=""http://tempuri.org/""")
                            .Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n", "");

            XmlDocument SOAPReqBody = CreateSoapEnvelope(xmlBody);
            InsertSoapEnvelopeIntoWebRequest(SOAPReqBody, request);

            //Geting response from request  
            using (WebResponse Serviceres = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                {
                    //reading stream  
                    var ServiceResult = rd.ReadToEnd();

                    response = MakePaymentResponseBodyXMLString(ServiceResult);
                }
            }

            return response;
        }

        private string ToXML(MakePaymentByGatewayDispatch MakePayment)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(MakePayment.GetType());
            serializer.Serialize(stringwriter, MakePayment);
            return stringwriter.ToString();
        }

        private MakePaymentByGatewayDispatch MakePaymentXMLString(string xmlText)
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(MakePaymentByGatewayDispatch));
            return serializer.Deserialize(stringReader) as MakePaymentByGatewayDispatch;
        }

        private string MakePaymentResponseBodyXMLString(string xmlText)
        {

            string response = xmlText.Substring(xmlText.IndexOf("<"+this.methName+"Result>") +  ("<"+this.methName+"Result"+">").Length).Replace("</"+this.methName+"Result></"+this.methName+"Response></soap:Body></soap:Envelope>", "");
            return response;//serializer.Deserialize(stringReader) as UploadFileResponseBody;
        }

        public class MakePaymentByGatewayDispatch
        {

            public int defaultclientid;
            public string jsonString;

            public string dataVal;
   

        }

        public class UploadFileResponseBody
        {
            public string UploadFileResult;
        }

    }
}
