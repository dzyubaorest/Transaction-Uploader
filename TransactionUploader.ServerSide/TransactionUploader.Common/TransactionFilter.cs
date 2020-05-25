using System;

namespace TransactionUploader.Common
{
	public class TransactionFilter
	{
		public TransactionFilter(
			string currencyCode,
			TransactionStatus? status,
			DateTime? startDate,
			DateTime? endDate)
		{
			CurrencyCode = currencyCode;
			Status = status;
			StartDate = startDate;
			EndDate = endDate;
		}

		public string CurrencyCode { get; set; }

		public TransactionStatus? Status { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }
	}
}