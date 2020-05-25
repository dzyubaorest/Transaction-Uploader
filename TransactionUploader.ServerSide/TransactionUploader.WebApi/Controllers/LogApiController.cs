using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionUploader.Common;
using TransactionUploader.Core.Extensibility;

namespace TransactionUploader.WebApi.Controllers
{
	[ApiController]
	[Route("api/log")]
	public class LogApiController : ControllerBase
	{
		private readonly ILogProvider _logProvider;

		public LogApiController(ILogProvider logProvider)
		{
			_logProvider = logProvider;
		}

		[HttpGet]
		public async Task<IReadOnlyCollection<Log>> Get()
		{
			return await _logProvider.GetLogsAsync();
		}
	}
}
