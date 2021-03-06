﻿using System;

namespace TransactionUploader.Common
{
	public class Transaction
	{
		public Transaction(
			string id,
			decimal amount,
			string currencyCode,
			DateTime date,
			TransactionStatus status)
		{
			Id = id;
			Amount = amount;
			CurrencyCode = currencyCode;
			Date = date;
			Status = status;
		}

		public string Id { get; }

		public decimal Amount { get; }

		public string CurrencyCode { get; }

		public DateTime Date { get; }

		public TransactionStatus Status { get; }
	}
}