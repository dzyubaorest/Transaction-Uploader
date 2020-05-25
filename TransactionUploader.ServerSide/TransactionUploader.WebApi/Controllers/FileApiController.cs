using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionUploader.Common;
using TransactionUploader.Core.Extensibility;
using File = TransactionUploader.Common.File;

namespace TransactionUploader.WebApi.Controllers
{
	[ApiController]
	[Route("api/file")]
	public class FileApiController : ControllerBase
	{
		private readonly IFileProvider _fileProvider;

		public FileApiController(IFileProvider fileProvider)
		{
			_fileProvider = fileProvider;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			OperationResult<File> operationResult = await _fileProvider.GetFileAsync(id);
			if (operationResult.Status == OperationResultStatus.Failure)
			{
				return BadRequest(OperationResult.Failure(operationResult.Message));
			}

			return File(operationResult.Data.Data, "application/octet-stream", operationResult.Data.Name);
		}
	}
}