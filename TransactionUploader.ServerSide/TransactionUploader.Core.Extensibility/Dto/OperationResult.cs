namespace TransactionUploader.Core.Extensibility.Dto
{
	public class OperationResult<T> : OperationResult
	{
		public OperationResult(string message, OperationResultStatus status, T data)
			: base(message, status)
		{
			Data = data;
		}

		public OperationResult(T data)
			: base(string.Empty, OperationResultStatus.Success)
		{
			Data = data;
		}

		public T Data { get; }
	}

	public class OperationResult
	{
		public OperationResult(string message, OperationResultStatus status)
		{
			Message = message;
			Status = status;
		}

		public string Message { get; }

		public OperationResultStatus Status { get; }

		public static OperationResult<TData> Failure<TData>(string message)
		{
			return new OperationResult<TData>(message, OperationResultStatus.Failure, default(TData));
		}

		public static OperationResult<TData> Success<TData>(TData data)
		{
			return new OperationResult<TData>(string.Empty, OperationResultStatus.Success, data);
		}

		public static OperationResult Success(string message)
		{
			return new OperationResult(message, OperationResultStatus.Success);
		}

		public static OperationResult Failure(string message)
		{
			return new OperationResult(message, OperationResultStatus.Failure);
		}
	}

	public enum OperationResultStatus
	{
		Success = 1,

		Failure = 2
	}
}