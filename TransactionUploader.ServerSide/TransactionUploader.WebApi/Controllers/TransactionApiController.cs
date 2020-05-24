using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionUploader.Common;
using TransactionUploader.Core.Extensibility;
using TransactionUploader.WebApi.Models;

namespace TransactionUploader.WebApi.Controllers
{
	[ApiController]
	[Route("api/transaction")]
	public class TransactionApiController : ControllerBase
	{
		private readonly ITransactionUploader _transactionUploader;
		private readonly ITransactionProvider _transactionProvider;

		public TransactionApiController(ITransactionUploader transactionUploader, ITransactionProvider transactionProvider)
		{
			_transactionUploader = transactionUploader;
			_transactionProvider = transactionProvider;
		}

		[HttpGet]
		public async Task<IReadOnlyCollection<TransactionModel>> Get([FromQuery]TransactionFilter filter)
		{
			IReadOnlyCollection<Transaction> transactions = await _transactionProvider.GetTransactionsAsync(filter);
			return transactions.Select(transaction => new TransactionModel(transaction)).ToList();
		}

		[HttpPost("file")]
		public async Task<IActionResult> PostFile([FromForm] IFormFile file)
		{
			await using MemoryStream memoryStream = new MemoryStream();
			await file.CopyToAsync(memoryStream);
			memoryStream.Seek(0, SeekOrigin.Begin);

			OperationResult operationResult = await _transactionUploader.UploadAsync(new Common.File(memoryStream, file.FileName));

			return operationResult.Status == OperationResultStatus.Success ?
				(IActionResult)Ok(operationResult) :
				BadRequest(operationResult);
		}
	}
}
