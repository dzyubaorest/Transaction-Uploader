using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Common;
using TransactionUploader.Infrastructure.Extensibility;

namespace TransactionUploader.Infrastructure.Repositories
{
	internal class FileRepository : IFileRepository
	{
		private readonly TransactionDbContext _transactionDbContext;

		public FileRepository(TransactionDbContext transactionDbContext)
		{
			_transactionDbContext = transactionDbContext;
		}

		public async Task<string> GetFileNameAsync(int fileId)
		{
			var file = await _transactionDbContext.Files.FindAsync(fileId);
			return file.FileName;
		}

		public async Task<OperationResult<MemoryStream>> GetFileContentAsync(int fileId)
		{
			string connectionString = _transactionDbContext.Database.GetDbConnection().ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("SELECT [Content] FROM [Files] WHERE [Id]=@id", connection))
				{
					command.Parameters.AddWithValue("@id", fileId);

					using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
					{
						if (await reader.ReadAsync())
						{
							if (!(await reader.IsDBNullAsync(0)))
							{
								MemoryStream memoryStream = new MemoryStream();
								using (Stream data = reader.GetStream(0))
								{
									await data.CopyToAsync(memoryStream);
									memoryStream.Seek(0, SeekOrigin.Begin);
									return OperationResult.Success(memoryStream);
								}
							}
						}
					}
				}
			}

			return OperationResult.Failure<MemoryStream>("Failed to download  file content.");
		}
	}
}