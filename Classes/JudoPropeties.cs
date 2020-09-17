using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MapApp
{
    //Request Classes
    public class JudoProperties
    {
        public JudoProperties()
        {

            yourPaymentMetaData = new YourPaymentMetaData();
            cardAddress = new CardAddress();
            consumerLocation = new ConsumerLocation();
        }
        public string yourConsumerReference { get; set; }
        public string yourPaymentReference { get; set; }
        public YourPaymentMetaData yourPaymentMetaData { get; set; }
        public string judoId { get; set; }
        public string Acc_Token { get; set; }
        public string Acc_SecretKey { get; set; }
        public double amount { get; set; }
        public string cardNumber { get; set; }
        public string expiryDate { get; set; }
        public string cv2 { get; set; }
        public string currency { get; set; }
        public CardAddress cardAddress { get; set; }
        public ConsumerLocation consumerLocation { get; set; }
        public string mobileNumber { get; set; }
        public string emailAddress { get; set; }
        public bool TestMode { get; set; }
    }

    public class YourPaymentMetaData
    {
    }

    public class CardAddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line3 { get; set; }
        public string town { get; set; }
        public string postCode { get; set; }
    }

    public class ConsumerLocation
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }








    //// Response Classes



    public class JudoCardDetails
    {
        public string cardLastfour { get; set; }
        public string endDate { get; set; }
        public string cardToken { get; set; }
        public int cardType { get; set; }
    }

    public class Consumer
    {
        public string consumerToken { get; set; }
        public string yourConsumerReference { get; set; }
    }

    public class Risks
    {
        public string postCodeCheck { get; set; }
    }

    public class ResponseJudo
    {
        public ResponseJudo()
        {

            cardDetails = new JudoCardDetails();
            consumer = new Consumer();
            risks = new Risks();
        }
        public string receiptId { get; set; }
        public string yourPaymentReference { get; set; }
        public string type { get; set; }
        public string createdAt { get; set; }
        public string result { get; set; }
        public string message { get; set; }
        public int judoId { get; set; }
        public string merchantName { get; set; }
        public string appearsOnStatementAs { get; set; }
        public string originalAmount { get; set; }
        public string netAmount { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public JudoCardDetails cardDetails { get; set; }
        public Consumer consumer { get; set; }
        public Risks risks { get; set; }
    }

}
