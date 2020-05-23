using System.Collections.Generic;
using System.IO;
using TransactionUploader.Core.Contracts.Dto;
using TransactionUploader.Core.Extensibility.Dto;

namespace TransactionUploader.Core.FileParsers
{
	interface IFileParser
	{
		bool CanHandle(string fileName);

		OperationResult<IReadOnlyCollection<TransactionDto>> Parse(Stream file);
	} 
}