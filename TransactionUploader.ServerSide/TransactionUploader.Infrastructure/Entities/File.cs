using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionUploader.Infrastructure.Entities
{
	public class File
	{
		[Key]
		public int Id { get; set; }

		public string FileName { get; set; }

		[NotMapped]
		public byte[] Content { get; set; }
	}
}