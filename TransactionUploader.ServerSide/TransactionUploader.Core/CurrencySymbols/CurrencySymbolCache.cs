using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TransactionUploader.Core.CurrencySymbols
{
	internal class CurrencySymbolCache : ICurrencySymbolCache
	{
		private readonly HashSet<string> _currencySymbols;

		public CurrencySymbolCache()
		{
			_currencySymbols = CultureInfo
				.GetCultures(CultureTypes.SpecificCultures)
				.Select(x => new RegionInfo(x.Name).ISOCurrencySymbol)
				.ToHashSet();
		}

		public bool Exists(string currencySymbol)
		{
			return _currencySymbols.Contains(currencySymbol);
		}
	}
}