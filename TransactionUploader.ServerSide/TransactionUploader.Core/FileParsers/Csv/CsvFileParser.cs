using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using TransactionUploader.Common;
using TransactionUploader.Core.CurrencySymbols;

namespace TransactionUploader.Core.FileParsers.Csv
{
	internal class CsvFileParser : FileParserBase, IFileParser
	{
		public CsvFileParser(ICurrencySymbolCache currencySymbolCache)
			: base(currencySymbolCache)
		{
		}

		protected override Dictionary<string, TransactionStatus> StatusMappings { get; } =
			new Dictionary<string, TransactionStatus>
			{
				{ "Approved", TransactionStatus.Approved },
				{ "Failed", TransactionStatus.Rejected },
				{ "Finished", TransactionStatus.Done }
			};

		protected override string SupportedFileExtension => ".csv";

		protected override string DateFormat => "dd/MM/yyyy HH:mm:ss";

		protected override OperationResult<IReadOnlyCollection<TransactionParserIntermediateDto>> ReadTransactions(Stream file)
		{
			using StreamReader streamReader = new StreamReader(file);
			using CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
			try
			{
				csvReader.Configuration.HasHeaderRecord = false;
				csvReader.Configuration.IgnoreBlankLines = true;
				var allRecords = csvReader.GetRecords<CsvTransactionDto>().ToList();
				IReadOnlyCollection<TransactionParserIntermediateDto> transactions = allRecords.Select(Convert).ToList();
				return OperationResult.Success(transactions);
			}
			catch (Exception)
			{
				return OperationResult.Failure<IReadOnlyCollection<TransactionParserIntermediateDto>>("Error while parsing .csv file.");
			}
		}

		public TransactionParserIntermediateDto Convert(CsvTransactionDto csvTransactionDto) =>
			new TransactionParserIntermediateDto(
				csvTransactionDto.Id.Trim(),
				csvTransactionDto.Amount.Trim(),
				csvTransactionDto.CurrencyCode.Trim(),
				csvTransactionDto.Date.Trim(),
				csvTransactionDto.Status.Trim());
	}
}