using System.IO;

namespace TransactionUploader.Core.Extensibility.Dto
{
	public class FileDto
	{
		public FileDto(MemoryStream data, string name)
		{
			Data = data;
			Name = name;
		}

		public MemoryStream Data { get; }

		public string Name { get; }
	}
}