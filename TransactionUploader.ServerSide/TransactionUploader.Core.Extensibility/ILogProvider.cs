using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Core.Extensibility
{
	public interface ILogProvider
	{
		Task<IReadOnlyCollection<Log>> GetLogsAsync();
	}
}