using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TransactionUploader.Common;
using TransactionUploader.Core.CurrencySymbols;

namespace TransactionUploader.Core.FileParsers.Xml
{
	internal class XmlFileParser : FileParserBase, IFileParser
	{

		public XmlFileParser(ICurrencySymbolCache currencySymbolCache)
			: base(currencySymbolCache)
		{
		}

		protected override Dictionary<string, TransactionStatus> StatusMappings { get; } =
			new Dictionary<string, TransactionStatus>
			{
				{"Approved", TransactionStatus.Approved},
				{"Rejected", TransactionStatus.Rejected},
				{"Done", TransactionStatus.Done}
			};

		protected override string SupportedFileExtension => ".xml";

		protected override string DateFormat => "yyyy-MM-ddTHH:mm:ss";

		protected override OperationResult<IReadOnlyCollection<TransactionParserIntermediateDto>> ReadTransactions(Stream file)
		{
			XDocument document = XDocument.Load(file);

			var transactions = new List<TransactionParserIntermediateDto>();
			foreach (XElement transactionElement in document.Descendants("Transaction"))
			{
				string id = transactionElement.Attribute("id")?.Value.Trim();
				if (id == null)
				{
					return OperationResult.Failure<IReadOnlyCollection<TransactionParserIntermediateDto>>("Transaction Id is missing.");
				}

				string date = transactionElement.Element("TransactionDate")?.Value.Trim();
				if (date == null)
				{
					return OperationResult.Failure<IReadOnlyCollection<TransactionParserIntermediateDto>>("TransactionDate is missing.");
				}

				string status = transactionElement.Element("Status")?.Value.Trim();
				if (status == null)
				{
					return OperationResult.Failure<IReadOnlyCollection<TransactionParserIntermediateDto>>("Status is missing.");
				}


				OperationResult<(string Amount, string CurrencyCode)> paymentsDetails = GetPaymentDetails(transactionElement);
				if (paymentsDetails.Status == OperationResultStatus.Failure)
				{
					return OperationResult.Failure<IReadOnlyCollection<TransactionParserIntermediateDto>>(paymentsDetails.Message);
				}

				transactions.Add(new TransactionParserIntermediateDto(id, paymentsDetails.Data.Amount, paymentsDetails.Data.CurrencyCode, date, status));
			}

			return OperationResult.Success<IReadOnlyCollection<TransactionParserIntermediateDto>>(transactions);
		}

		private OperationResult<(string Amount, string CurrencyCode)> GetPaymentDetails(XElement parentElement)
		{
			var paymentDetailsElement = parentElement.Element("PaymentDetails");
			if (paymentDetailsElement == null)
			{
				return OperationResult.Failure<(string amount, string currencyCode)>("PaymentDetails element is missing.");
			}

			var amount = paymentDetailsElement.Element("Amount")?.Value.Trim();
			if (amount == null)
			{
				return OperationResult.Failure<(string amount, string currencyCode)>("Amount element is missing.");
			}

			var currencyCode = paymentDetailsElement.Element("CurrencyCode")?.Value.Trim();
			if (currencyCode == null)
			{
				return OperationResult.Failure<(string amount, string currencyCode)>("CurrencyCode element is missing.");
			}


			return OperationResult.Success((amount, currencyCode));
		}
	}
}