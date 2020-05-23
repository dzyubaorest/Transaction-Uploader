namespace TransactionUploader.Core.FileParsers
{
	internal class TransactionParserIntermediateDto
	{
		public TransactionParserIntermediateDto(
			string id,
			string amount,
			string currencyCode,
			string transactionDate,
			string status)
		{
			Id = id;
			Amount = amount;
			CurrencyCode = currencyCode;
			TransactionDate = transactionDate;
			Status = status;
		}

		public string Id { get; }

		public string Amount { get; }

		public string CurrencyCode { get; }

		public string TransactionDate { get; }

		public string Status { get; }
	}
}