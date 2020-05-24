using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Infrastructure.Extensibility
{
	public interface ITransactionRepository
	{
		Task InsertOrUpdateTransactionsAsync(IReadOnlyCollection<Transaction> transactions);

		Task<IReadOnlyCollection<Transaction>> GetTransactionsAsync(TransactionFilter transactionFilter);
	}
}