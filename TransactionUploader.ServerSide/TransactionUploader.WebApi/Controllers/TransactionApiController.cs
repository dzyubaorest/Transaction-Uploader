using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionUploader.Core.Contracts.Dto;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.Core.Extensibility.Dto;
using TransactionUploader.WebApi.Models;

namespace TransactionUploader.WebApi.Controllers
{
	[ApiController]
	[Route("api/transaction")]
	public class TransactionApiController : ControllerBase
	{
		private readonly IFileUploader _fileUploader;

		public TransactionApiController(IFileUploader fileUploader)
		{
			_fileUploader = fileUploader;
		}

		[HttpGet]
		public IEnumerable<Transaction> Get([FromQuery]TransactionFilter filter)
		{
			if (filter.StartDate == null && filter.Status == null && filter.CurrencyCode == null)
			{
				return Transactions.TransactionList;
			}

			return Transactions.TransactionList.Where(transaction =>
			(filter.CurrencyCode == null || transaction.CurrencyCode == filter.CurrencyCode) &&
			(filter.Status == null || transaction.Status == filter.Status) &&
			(filter.StartDate == null || transaction.Date >= filter.StartDate.Value) &&
			(filter.EndDate == null || transaction.Date <= filter.EndDate.Value));
		}

		[HttpPost("post-file")]
		public async Task<IActionResult> PostFile([FromForm] IFormFile file)
		{
			await using (MemoryStream memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				memoryStream.Seek(0, SeekOrigin.Begin);
				OperationResult operationResult = _fileUploader.Upload(new FileDto(memoryStream, file.FileName));
				Response response = new Response(operationResult, file.Name);

				if (operationResult.Status == OperationResultStatus.Success)
				{
					return Ok(response);
				}
				return BadRequest(response);
			}
		}
	}
}
