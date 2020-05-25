using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionUploader.Infrastructure.Entities
{
	public class Log
	{
		[Key]
		public int Id { get; set; }

		public LogLevel Level { get; set; }

		[Required]
		public string Message { get; set; }

		public int FileId { get; set; }

		[ForeignKey("FileId")]
		public File File { get; set; }
	}

	public enum LogLevel
	{
		Info = 1,
		Warning,
		Error
	}
}