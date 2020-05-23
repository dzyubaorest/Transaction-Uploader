using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Core;

namespace TransactionUploader.Module
{
	public static class ModuleDependenciesRegister
	{
		public static void Register(IServiceCollection services)
		{
			CoreDependenciesRegister.Register(services);
		}
	}
}