using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TransactionUploader.Common;
using TransactionUploader.Core.CurrencySymbols;

namespace TransactionUploader.Core.FileParsers
{
	internal abstract class FileParserBase
	{
		private readonly ICurrencySymbolCache _currencySymbolCache;

		protected abstract Dictionary<string, TransactionStatus> StatusMappings { get; }

		protected abstract string DateFormat { get; }

		protected abstract string SupportedFileExtension { get; }

		protected FileParserBase(ICurrencySymbolCache currencySymbolCache)
		{
			_currencySymbolCache = currencySymbolCache;
		}

		public bool CanHandle(string fileName)
		{
			return fileName.EndsWith(SupportedFileExtension);
		}

		public OperationResult<IReadOnlyCollection<Transaction>> Parse(Stream file)
		{
			OperationResult<IReadOnlyCollection<TransactionParserIntermediateDto>> transactionsOperationResult = ReadTransactions(file);
			if (transactionsOperationResult.Status == OperationResultStatus.Failure)
			{
				return GetFailureResult(transactionsOperationResult.Message);
			}

			var transactions = new List<Transaction>();
			foreach (TransactionParserIntermediateDto transactionFromFile in transactionsOperationResult.Data)
			{
				if (transactionFromFile.Id.Length >= Constants.MaxTransactionIdLength)
				{
					return GetFailureResult($"TransactionId length must not be more than {Constants.MaxTransactionIdLength}.");
				}

				if (!decimal.TryParse(transactionFromFile.Amount, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
				{
					return GetFailureResult($"Amount field is invalid : {transactionFromFile.Amount}.");
				}

				if (!_currencySymbolCache.Exists(transactionFromFile.CurrencyCode))
				{
					return GetFailureResult($"Invalid Currency Code : {transactionFromFile.CurrencyCode}.");
				}

				if (!DateTime.TryParseExact(
					transactionFromFile.TransactionDate,
					DateFormat,
					CultureInfo.InvariantCulture,
					DateTimeStyles.None, out DateTime date))
				{
					return GetFailureResult($"Unsupported DateTime format: {transactionFromFile.TransactionDate}.");
				}

				if (!StatusMappings.TryGetValue(transactionFromFile.Status, out TransactionStatus status))
				{
					return GetFailureResult($"Unsupported status: {transactionFromFile.Status}.");
				}

				var transaction = new Transaction(
					transactionFromFile.Id,
					amount,
					transactionFromFile.CurrencyCode,
					date,
					status);
				transactions.Add(transaction);
			}

			return OperationResult.Success<IReadOnlyCollection<Transaction>>(transactions);
		}

		protected abstract OperationResult<IReadOnlyCollection<TransactionParserIntermediateDto>> ReadTransactions(Stream file);

		private OperationResult<IReadOnlyCollection<Transaction>> GetFailureResult(string message) =>
			OperationResult.Failure<IReadOnlyCollection<Transaction>>(message);
	}
}