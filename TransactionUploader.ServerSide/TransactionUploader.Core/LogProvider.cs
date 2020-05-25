using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.Infrastructure.Extensibility;

namespace TransactionUploader.Core
{
	internal class LogProvider : ILogProvider
	{
		private readonly ILogRepository _logRepository;

		public LogProvider(ILogRepository logRepository)
		{
			_logRepository = logRepository;
		}

		public async Task<IReadOnlyCollection<Log>> GetLogsAsync()
		{
			return await _logRepository.GetLogsAsync();
		}
	}
}