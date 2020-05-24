using System.Threading.Tasks;
using TransactionUploader.Common;

namespace TransactionUploader.Core.Extensibility
{
	public interface ITransactionUploader
	{
		Task<OperationResult> UploadAsync(File file);
	}
}