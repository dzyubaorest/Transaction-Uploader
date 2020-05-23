using System.Collections.Generic;
using System.Linq;
using TransactionUploader.Core.Contracts.Dto;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.Core.Extensibility.Dto;
using TransactionUploader.Core.FileParsers;

namespace TransactionUploader.Core
{
	internal class FileUploader : IFileUploader
	{
		private readonly IEnumerable<IFileParser> _fileParsers;

		public FileUploader(IEnumerable<IFileParser> fileParsers)
		{
			_fileParsers = fileParsers;
		}
		public OperationResult Upload(FileDto file)
		{
			IFileParser fileParser = _fileParsers.SingleOrDefault(parser => parser.CanHandle(file.Name));

			if (fileParser == null)
			{
				return OperationResult.Failure("Unsupported file format");
			}

			OperationResult<IReadOnlyCollection<TransactionDto>> transactions = fileParser.Parse(file.Data);

			return OperationResult.Success("File has been uploaded");
		}
	}
}