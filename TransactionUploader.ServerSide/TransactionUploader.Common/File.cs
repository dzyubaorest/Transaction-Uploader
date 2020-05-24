using System.IO;

namespace TransactionUploader.Common
{
	public class File
	{
		public File(MemoryStream data, string name)
		{
			Data = data;
			Name = name;
		}

		public MemoryStream Data { get; }

		public string Name { get; }
	}
}