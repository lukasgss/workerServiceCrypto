using WorkerService.ExternalServices;
using Hangfire;
using Hangfire.SqlServer;
using WorkerService.InternalServices;

namespace WorkerService;

public static class DependencyInjection
{
	public static IServiceCollection AddWorkerService(this IServiceCollection services, IConfiguration configuration)
	{
		GlobalConfiguration.Configuration
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
			{
				CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
				SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
				QueuePollInterval = TimeSpan.Zero,
				UseRecommendedIsolationLevel = true,
				DisableGlobalLocks = true,
			});

		services.AddHangfire(config =>
		{
			config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
				.UseSimpleAssemblyNameTypeSerializer()
				.UseRecommendedSerializerSettings()
				.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"),
					new SqlServerStorageOptions
					{
						CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
						SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
						QueuePollInterval = TimeSpan.Zero,
						UseRecommendedIsolationLevel = true,
						DisableGlobalLocks = true,
					});
		});

		services.AddHttpClient(CryptoConfig.ClientKey,
			client => { client.BaseAddress = CryptoConfig.BaseUrl; });

		services.AddHttpClient(InternalApiConfig.ClientKey,
			client => { client.BaseAddress = InternalApiConfig.BaseUrl; });

		services.AddScoped<ICryptoClient, CryptoClient>();
		services.AddScoped<IInternalApiClient, InternalApiClient>();
		services.AddScoped<Worker>();

		return services;
	}
}