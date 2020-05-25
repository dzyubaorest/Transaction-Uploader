using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Infrastructure.Entities;
using TransactionUploader.Infrastructure.Extensibility;
using File = TransactionUploader.Infrastructure.Entities.File;
using Log = TransactionUploader.Common.Log;
using LogDb = TransactionUploader.Infrastructure.Entities.Log;

namespace TransactionUploader.Infrastructure.Repositories
{
	internal class LogRepository : ILogRepository
	{
		private readonly TransactionDbContext _transactionDbContext;
		public LogRepository(TransactionDbContext transactionDbContext)
		{
			_transactionDbContext = transactionDbContext;
		}

		public async Task<IReadOnlyCollection<Log>> GetLogsAsync()
		{
			return await _transactionDbContext.Logs.Select(log => new Log(log.FileId, log.Message)).ToListAsync();
		}

		public async Task AddErrorLogAsync(string fileName, string message, Stream stream)
		{
			var fileDb = new File
			{
				FileName = fileName
			};
			_transactionDbContext.Files.Add(fileDb);
			await _transactionDbContext.SaveChangesAsync();

			_transactionDbContext.Logs.Add(new LogDb
			{
				Message = message,
				Level = LogLevel.Error,
				FileId = fileDb.Id
			});
			await _transactionDbContext.SaveChangesAsync();


			await StreamFileContentToServerAsync(stream, fileDb.Id);
		}

		private async Task StreamFileContentToServerAsync(Stream stream, int fileId)
		{
			string connectionString = _transactionDbContext.Database.GetDbConnection().ConnectionString;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("UPDATE [Files] SET Content=@bindata WHERE Id=@id", conn))
				{
					cmd.Parameters.Add("@bindata", SqlDbType.Binary, -1).Value = stream;
					cmd.Parameters.AddWithValue("@id", fileId);

					await cmd.ExecuteNonQueryAsync();
				}
			}
		}
	}
}