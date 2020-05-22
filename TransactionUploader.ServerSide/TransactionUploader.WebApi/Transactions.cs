using System;
using System.Collections.Generic;

namespace TransactionUploader.WebApi
{
	public class Transactions
	{
		public static IEnumerable<Transaction> TransactionList = new List<Transaction>()
		{
			new Transaction
			{
				Amount = 187.8m,
				CurrencyCode = "USD",
				Date = new DateTime(2020, 5, 21, 13, 45, 10),
				Status = "R"
			},
			new Transaction
			{
				Amount = 165.5m,
				CurrencyCode = "EUR",
				Date = new DateTime(2020, 5, 21, 13, 45, 10),
				Status = "A"
			},
			new Transaction
			{
				Amount = 198.5m,
				CurrencyCode = "UAH",
				Date = new DateTime(2020, 1, 23, 13, 45, 10),
				Status = "D"
			},
			new Transaction
			{
				Amount = 233.4m,
				CurrencyCode = "USD",
				Date = new DateTime(2020, 5, 25, 13, 45, 10),
				Status = "R"
			},
			new Transaction
			{
				Amount = 560.5m,
				CurrencyCode = "EUR",
				Date = new DateTime(2020, 5, 25, 13, 45, 10),
				Status = "A"
			},
			new Transaction
			{
				Amount = 728.6m,
				CurrencyCode = "UAH",
				Date = new DateTime(2020, 1, 25, 13, 45, 10),
				Status = "D"
			}
		};
	}
}