using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.Infrastructure.Extensibility;

namespace TransactionUploader.Core
{
	internal class TransactionProvider : ITransactionProvider
	{
		private readonly ITransactionRepository _transactionRepository;

		public TransactionProvider(ITransactionRepository transactionRepository)
		{
			_transactionRepository = transactionRepository;
		}

		public async Task<IReadOnlyCollection<Transaction>> GetTransactionsAsync(TransactionFilter filter)
		{
			return await _transactionRepository.GetTransactionsAsync(filter);
		}
	}
}