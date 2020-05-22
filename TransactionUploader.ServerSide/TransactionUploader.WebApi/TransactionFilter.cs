using System;

namespace TransactionUploader.WebApi
{
	public class TransactionFilter
	{
		public string CurrencyCode { get; set; }

		public string Status { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }
	}
}
