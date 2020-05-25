using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Common;
using TransactionUploader.Infrastructure.Extensibility;
using TransactionDb = TransactionUploader.Infrastructure.Entities.Transaction;

namespace TransactionUploader.Infrastructure.Repositories
{
	internal class TransactionRepository : ITransactionRepository
	{
		private static readonly Object _lockObject = new Object();

		private readonly TransactionDbContext _transactionDbContext;
		public TransactionRepository(TransactionDbContext transactionDbContext)
		{
			_transactionDbContext = transactionDbContext;
		}

		public  void InsertOrUpdateTransactions(IReadOnlyCollection<Transaction> transactions)
		{
			lock (_lockObject)
			{
				var transactionsToInsert = new List<TransactionDb>();
				foreach (var transaction in transactions)
				{
					var existingTransaction = _transactionDbContext.Transactions.Find(transaction.Id);
					if (existingTransaction == null)
					{
						transactionsToInsert.Add(Convert(transaction));
					}
					else
					{
						_transactionDbContext.Entry(existingTransaction).CurrentValues.SetValues(transaction);
					}
				}

				_transactionDbContext.Transactions.AddRange(transactionsToInsert);

				 _transactionDbContext.SaveChanges();
			}
		}

		public async Task<IReadOnlyCollection<Transaction>> GetTransactionsAsync(TransactionFilter transactionFilter)
		{
			List<TransactionDb> dbTransactions =
				await _transactionDbContext.Transactions.Where(transaction => 
						(string.IsNullOrEmpty(transactionFilter.CurrencyCode) || transaction.CurrencyCode == transactionFilter.CurrencyCode) && 
						(!transactionFilter.Status.HasValue || transaction.Status == transactionFilter.Status.Value) && 
						(!transactionFilter.StartDate.HasValue || transaction.Date >= transactionFilter.StartDate.Value) && 
						(!transactionFilter.EndDate.HasValue|| transaction.Date <= transactionFilter.EndDate.Value))
					.ToListAsync();

			return dbTransactions.Select(Convert).ToList();
		}

		private static TransactionDb Convert(Transaction transaction)
		{
			return new TransactionDb
			{
				Status = transaction.Status,
				Amount = transaction.Amount,
				Date = transaction.Date,
				CurrencyCode = transaction.CurrencyCode,
				Id = transaction.Id
			};
		}

		private static Transaction Convert(TransactionDb transaction)
		{
			return new Transaction(
				transaction.Id,
				transaction.Amount,
				transaction.CurrencyCode,
				transaction.Date,
				transaction.Status);
		}
	}
}