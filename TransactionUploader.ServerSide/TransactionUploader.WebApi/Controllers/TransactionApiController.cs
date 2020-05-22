using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TransactionUploader.WebApi.Controllers
{
	[ApiController]
	[Route("api/transaction")]
	public class TransactionApiController : ControllerBase
	{

		private readonly ILogger<TransactionApiController> _logger;

		private static int someNumber = 1;

		public TransactionApiController(ILogger<TransactionApiController> logger)
		{
			_logger = logger;
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
		public async Task<IActionResult> PostFile(int someId, string someString, [FromForm] IFormFile file)
		{
			var z = 5;

			await Task.Delay(3000);

			if (someNumber++ % 2 == 0)
			{
				return Ok(new Response
				{
					Status = "Success"
				});
			}
			else
			{
				return Ok(new Response
				{
					Status = "Failure"
				});
			}
		}
	}

	class Response
	{
		public string Status { get; set; }
	}
}
