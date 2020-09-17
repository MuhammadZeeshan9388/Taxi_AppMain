using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using MySql.Data.MySqlClient;
using Utils;

namespace Taxi_AppMain
{
   public   class ClsRingBack : IDisposable
    {

      public  enum eCallTo { Driver,Customer};






      private string _CallerNumber;

      public string CallerNumber
      {
          get { return _CallerNumber; }
          set { _CallerNumber = value; }
      }

       

       private string fileName;

       private string _AccountCode;

        public string AccountCode
        {
          get { return _AccountCode; }
          set { _AccountCode = value; }
        }

       private string _Extension;

        public string Extension
        {
          get { return _Extension; }
          set { _Extension = value; }
        }



      

       
       
       public ClsRingBack()
       {

              this.fileName = System.Windows.Forms.Application.StartupPath + "\\Service.xml";

               if (File.Exists(this.fileName) == true)
               {

                   XmlDocument doc = new XmlDocument();
                   doc.Load(this.fileName);

                   XmlNode node = doc.GetElementsByTagName("accountcode").OfType<XmlNode>().FirstOrDefault();
                   XmlNode nodePhone = doc.GetElementsByTagName("callerid").OfType<XmlNode>().FirstOrDefault();


                   if(node!=null && nodePhone!=null && !string.IsNullOrEmpty(node.InnerText) && !string.IsNullOrEmpty(nodePhone.InnerText))
                   {

                        this.AccountCode= node.InnerText.ToStr().Trim();
                        this.CallerNumber = nodePhone.InnerText.ToStr().Trim();
                     
                   }                   

               }


       }


       public  string MakeCall(eCallTo callTo,string name, string phoneNumber)
       {
          


           try
           {
             

               if (!string.IsNullOrEmpty(this.AccountCode))
               {


                   //    string MyConString = "";
                   using (MySqlConnection connection = new MySqlConnection("SERVER=db1.247365isp.net;DATABASE=taxi_autocall;UID=taxi_autocall;PASSWORD=taxi_autocall__991_;"))
                   {
                       MySqlCommand command = connection.CreateCommand();
                       command.CommandText = "INSERT INTO call_queue (`dialout_number`,`accountcode`,`callerid`) VALUES ('" + phoneNumber + "','" + this.AccountCode + "','"+this.CallerNumber+"')";

                    //   command.CommandText = "INSERT INTO call_queue (`dialout_number`,`accountcode`) VALUES ('07411330306','781123');";
                       if (connection.State == ConnectionState.Open)
                           connection.Close();

                       connection.Open();
                       int row = command.ExecuteNonQuery();

                       connection.Close();
                       

                   }

                   (new Taxi_Model.TaxiDataContext()).stp_InsertRingBackLog(name,General.GetCLIFirstExtensions().ToStr().Trim(), phoneNumber, callTo == eCallTo.Customer ? true : false);


                   if (callTo == eCallTo.Driver)
                   {
                       return "<html><b><span><color=Blue>Driver No : " + name + Environment.NewLine + "Phone No : " + phoneNumber + "</span></b></html>";


                   }
                   else
                   {
                       return "<html><b><span><color=Blue>Customer Name : " + name + Environment.NewLine + "Phone No : " + phoneNumber + "</span></b></html>";

                   }


               }
               else
                   return "<html><b><span><color=Blue>CALL FAILED : "+Environment.NewLine+"Account Code is not specified</span></b></html>";



             
           }
           catch (MySqlException ex)
           {

                  return "<html><b><span><color=Blue>CALL FAILED :"+Environment.NewLine+"Name : " + name + "(" + phoneNumber +")"+Environment.NewLine+"Reason : "+ex.Message+ "</span></b></html>";

           }
           



       }







       #region IDisposable Members

       public void Dispose()
       {
           
       }

       #endregion
    }
}
