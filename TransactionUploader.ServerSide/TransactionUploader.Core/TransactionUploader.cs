using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.Core.FileParsers;
using TransactionUploader.Infrastructure.Extensibility;

namespace TransactionUploader.Core
{
	internal class TransactionUploader : ITransactionUploader
	{
		private readonly IEnumerable<IFileParser> _fileParsers;
		private readonly ITransactionRepository _transactionRepository;

		public TransactionUploader(IEnumerable<IFileParser> fileParsers, ITransactionRepository transactionRepository)
		{
			_fileParsers = fileParsers;
			_transactionRepository = transactionRepository;
		}
		public async Task<OperationResult> UploadAsync(File file)
		{
			IFileParser fileParser = _fileParsers.SingleOrDefault(parser => parser.CanHandle(file.Name));

			if (fileParser == null)
			{
				return OperationResult.Failure($"[{file.Name}]: Unsupported file format.");
			}

			OperationResult<IReadOnlyCollection<Transaction>> transactions = fileParser.Parse(file.Data);
			if (transactions.Status == OperationResultStatus.Failure)
			{
				return OperationResult.Failure($"[{file.Name}]: {transactions.Message}");
			}

			await _transactionRepository.InsertOrUpdateTransactionsAsync(transactions.Data);

			return OperationResult.Success($"[{file.Name}] has been uploaded.");
		}
	}
}