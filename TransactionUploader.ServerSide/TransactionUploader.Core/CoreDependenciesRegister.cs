using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Core.CurrencySymbols;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.Core.FileParsers;
using TransactionUploader.Core.FileParsers.Csv;
using TransactionUploader.Core.FileParsers.Xml;

namespace TransactionUploader.Core
{
	public static class CoreDependenciesRegister
	{
		public static void Register(IServiceCollection services)
		{
			services.AddScoped<IFileParser, XmlFileParser>();
			services.AddScoped<IFileParser, CsvFileParser>();
			services.AddSingleton<ICurrencySymbolCache, CurrencySymbolCache>();
			services.AddScoped<ITransactionUploader, TransactionUploader>();
			services.AddScoped<ITransactionProvider, TransactionProvider>();
			services.AddScoped<IFileProvider, FileProvider>();
			services.AddScoped<ILogProvider, LogProvider>();
		}
	}
}