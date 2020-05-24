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
	}
}