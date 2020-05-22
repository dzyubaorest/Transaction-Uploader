using System;

namespace TransactionUploader.WebApi
{
	public class Transaction
	{
		public decimal Amount { get; set; }

		public string CurrencyCode { get; set; }

		public DateTime Date { get; set; }

		public string Status { get; set; }
	}
}
