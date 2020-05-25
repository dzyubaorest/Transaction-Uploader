using Microsoft.EntityFrameworkCore;
using TransactionUploader.Infrastructure.Entities;

namespace TransactionUploader.Infrastructure
{
	public class TransactionDbContext : DbContext
	{
		public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
		{
		}

		public DbSet<Transaction> Transactions { get; set; }

		public DbSet<Log> Logs { get; set; }

		public DbSet<File> Files { get; set; }
	}
}