namespace TransactionUploader.Core.CurrencySymbols
{
	internal interface ICurrencySymbolCache
	{
		bool Exists(string currencySymbol);
	}
}