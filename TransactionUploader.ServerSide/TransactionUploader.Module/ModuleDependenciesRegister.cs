using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Core;
using TransactionUploader.Infrastructure;

namespace TransactionUploader.Module
{
	public static class ModuleDependenciesRegister
	{
		public static void Register(IConfiguration configuration, IServiceCollection services)
		{
			CoreDependenciesRegister.Register(services);
			InfrastructureDependenciesRegister.Register(configuration, services);
		}

		public static void MigrateDb(IServiceProvider serviceProvider)
		{
			using var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
			var context = serviceScope.ServiceProvider.GetRequiredService<TransactionDbContext>();
			context.Database.Migrate();
		}
	}
}