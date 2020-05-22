using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TransactionUploader.WebApi.Controllers
{
	[ApiController]
	[Route("api/transaction")]
	public class TransactionApiController : ControllerBase
	{

		private readonly ILogger<TransactionApiController> _logger;

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
	}
}
