using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Core.Extensibility
{
	public interface ITransactionProvider
	{
		Task<IReadOnlyCollection<Transaction>> GetTransactionsAsync(TransactionFilter filter);
	}
}