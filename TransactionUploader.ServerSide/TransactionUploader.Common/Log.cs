namespace TransactionUploader.Common
{
	public class Log
	{
		public Log(int fileId, string message)
		{
			FileId = fileId;
			Message = message;
		}

		public int FileId { get; }

		public string Message { get; }
	}
}