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
		private readonly TransactionDbContext _transactionDbContext;
		public TransactionRepository(TransactionDbContext transactionDbContext)
		{
			_transactionDbContext = transactionDbContext;
		}

		public async Task InsertOrUpdateTransactionsAsync(IReadOnlyCollection<Transaction> transactions)
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

			await _transactionDbContext.SaveChangesAsync();
		}

		public async Task<IReadOnlyCollection<Transaction>> GetTransactionsAsync(TransactionFilter transactionFilter)
		{
			List<TransactionDb> dbTransactions =
				await _transactionDbContext.Transactions.Where(transaction => 
						(transactionFilter.CurrencyCode == null || transaction.CurrencyCode == transactionFilter.CurrencyCode) && 
						(transactionFilter.Status == 0 || transaction.Status == transactionFilter.Status) && 
						(transactionFilter.StartDate == null || transaction.Date >= transactionFilter.StartDate.Value) && 
						(transactionFilter.EndDate == null || transaction.Date <= transactionFilter.EndDate.Value))
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