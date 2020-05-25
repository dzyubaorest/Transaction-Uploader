using System.ComponentModel.DataAnnotations;

namespace TransactionUploader.Infrastructure.Entities
{
	public class File
	{
		[Key]
		public int Id { get; set; }

		public string FileName { get; set; }

		public byte[] Content { get; set; }
	}
}