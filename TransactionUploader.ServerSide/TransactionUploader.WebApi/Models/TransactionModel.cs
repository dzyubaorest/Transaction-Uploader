using System;
using TransactionUploader.Common;

namespace TransactionUploader.WebApi.Models
{
	public class TransactionModel
	{
		public TransactionModel(Transaction transaction)
		{
			Id = transaction.Id;
			Payment = $"{transaction.Amount} {transaction.CurrencyCode}";
			Date = transaction.Date;
			Status = transaction.Status.ToString()[0].ToString();
		}

		public string Id { get; }

		public string Payment { get; }

		public DateTime Date { get; }

		public string Status { get; }
	}
}