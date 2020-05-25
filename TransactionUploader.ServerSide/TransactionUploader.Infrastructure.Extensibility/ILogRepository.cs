using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Infrastructure.Extensibility
{
	public interface ILogRepository
	{
		Task<IReadOnlyCollection<Log>> GetLogsAsync();

		Task AddErrorLogAsync(string fileName, string message, Stream stream);
	}
}