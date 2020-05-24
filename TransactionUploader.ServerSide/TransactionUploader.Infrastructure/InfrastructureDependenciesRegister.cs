using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Infrastructure.Extensibility;
using TransactionUploader.Infrastructure.Repositories;

namespace TransactionUploader.Infrastructure
{
	public static class InfrastructureDependenciesRegister
	{
		public static void Register(IConfiguration configuration, IServiceCollection services)
		{
			services.AddDbContext<TransactionDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

			services.AddScoped<ITransactionRepository, TransactionRepository>();
		}
	}
}