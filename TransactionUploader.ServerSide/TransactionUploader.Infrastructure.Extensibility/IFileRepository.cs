using System.IO;
using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Infrastructure.Extensibility
{
	public interface IFileRepository
	{
		Task<string> GetFileNameAsync(int fileId);

		Task<OperationResult<MemoryStream>> GetFileContentAsync(int fileId);
	}
}