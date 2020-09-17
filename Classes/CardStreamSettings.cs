using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi_AppMain.Classes
{
	public class CardStreamSettings
	{
		public string merchantId { get; set; }
		public string action { get; set; }
		public int transType { get; set; }
		public string uniqueIdentifier { get; set; }
		public int currencyCode { get; set; }
		public decimal amount { get; set; }
		public String orderRef { get; set; }
		public string cardNumber { get; set; }
		public string cardExpiryMM { get; set; }
		public string cardExpiryYY { get; set; }
		public string cardCVV { get; set; }
		public string customerName { get; set; }
		public string customerEmail { get; set; }
		public string customerPhone { get; set; }
		public string customerAddress { get; set; }
		public int countryCode { get; set; }
		public string customerPostcode { get; set; }
		public string threeDSMD { get; set; }
		public string threeDSPaRes { get; set; }
		public string threeDSPaReq { get; set; }
		public string threeDSACSURL { get; set; }
	}
}
