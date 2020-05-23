using TransactionUploader.Core.Contracts.Dto;
using TransactionUploader.Core.Extensibility.Dto;

namespace TransactionUploader.WebApi.Models
{
	public class Response
	{
		public Response(OperationResult operationResult, string fileName)
		{
			Status = operationResult.Status == OperationResultStatus.Success ? "Success" : "Failure";
			Message = $"[{fileName}]: {operationResult.Message}";
		}

		public string Status { get; }

		public string Message { get; }
	}
}