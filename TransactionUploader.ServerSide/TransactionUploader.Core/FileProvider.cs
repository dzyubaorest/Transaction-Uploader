using System.Threading.Tasks;
using TransactionUploader.Common;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.Infrastructure.Extensibility;
using File = TransactionUploader.Common.File;

namespace TransactionUploader.Core
{
	internal class FileProvider : IFileProvider
	{
		private readonly IFileRepository _fileRepository;

		public FileProvider(IFileRepository fileRepository)
		{
			_fileRepository = fileRepository;
		}

		public async Task<OperationResult<File>> GetFileAsync(int fileId)
		{
			string fileName = await _fileRepository.GetFileNameAsync(fileId);
			var fileContentResult = await _fileRepository.GetFileContentAsync(fileId);
			if (fileContentResult.Status == OperationResultStatus.Failure)
			{
				return OperationResult.Failure<File>(fileContentResult.Message);
			}

			return OperationResult.Success(new File(fileContentResult.Data, fileName));
		}
	}
}