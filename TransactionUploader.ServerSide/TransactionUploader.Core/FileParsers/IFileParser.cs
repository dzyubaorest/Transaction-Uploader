using System.Collections.Generic;
using System.IO;
using TransactionUploader.Common;

namespace TransactionUploader.Core.FileParsers
{
	interface IFileParser
	{
		bool CanHandle(string fileName);

		OperationResult<IReadOnlyCollection<Transaction>> Parse(Stream file);
	} 
}