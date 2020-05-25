using System.Threading.Tasks;
using TransactionUploader.Common;
using File = TransactionUploader.Common.File;

namespace TransactionUploader.Core.Extensibility
{
	public interface IFileProvider
	{
		Task<OperationResult<File>> GetFileAsync(int fileId);
	}
}