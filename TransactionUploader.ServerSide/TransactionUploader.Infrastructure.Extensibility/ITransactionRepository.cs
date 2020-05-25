using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Infrastructure.Extensibility
{
	public interface ITransactionRepository
	{
		void InsertOrUpdateTransactions(IReadOnlyCollection<Transaction> transactions);

		Task<IReadOnlyCollection<Transaction>> GetTransactionsAsync(TransactionFilter transactionFilter);
	}
}