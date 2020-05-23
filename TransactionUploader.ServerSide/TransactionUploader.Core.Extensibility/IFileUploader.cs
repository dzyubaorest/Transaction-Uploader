using TransactionUploader.Core.Contracts.Dto;
using TransactionUploader.Core.Extensibility.Dto;

namespace TransactionUploader.Core.Extensibility
{
	public interface IFileUploader
	{
		OperationResult Upload(FileDto file);
	}
}