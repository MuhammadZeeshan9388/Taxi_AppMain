using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Utils;

namespace Taxi_AppMain
{
    public class WebUploadFile
    {
        //Live APIURL
        private string APIURL =Program.objLic.CabTrackUrl.ToStr()+ "/WebUploadFile.asmx";

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

        public string UploadFileGetPhotoLinkId(byte[] f, string fileName, string LinkId, string DriverNo, string ClientId)
        {
            
            string response=string.Empty;
            //Calling CreateSOAPWebRequest method  
            HttpWebRequest request = CreateSOAPWebRequest("UploadFile");

            UploadFile uploadFile = new UploadFile() { 
            f = f,
            fileName = fileName,
            LinkId = LinkId,
            DriverNo = DriverNo,
            ClientId = ClientId
            };

            
            string xmlBody = ToXML(uploadFile)
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

                    response = UploadFileResponseBodyXMLString(ServiceResult);
                }
            }

            return response;
        }

        public string ToXML(UploadFile uploadfile)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(uploadfile.GetType());
            serializer.Serialize(stringwriter, uploadfile);
            return stringwriter.ToString();
        }

        public UploadFile UploadFileXMLString(string xmlText)
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(UploadFile));
            return serializer.Deserialize(stringReader) as UploadFile;
        }

        public string UploadFileResponseBodyXMLString(string xmlText)
        {
            string response = xmlText.Substring(xmlText.IndexOf("<UploadFileResult>") + "<UploadFileResult>".Length).Replace("</UploadFileResult></UploadFileResponse></soap:Body></soap:Envelope>", "");            
            return response;//serializer.Deserialize(stringReader) as UploadFileResponseBody;
        }

        public class UploadFile
        {
        
            public byte[] f;            
        
            public string fileName;            
        
            public string LinkId;            
        
            public string DriverNo;            
        
            public string ClientId;            
            
        }

        public class UploadFileResponseBody
        {
            public string UploadFileResult;
        }
       
    }
}
