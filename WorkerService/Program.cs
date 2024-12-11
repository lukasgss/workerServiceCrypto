using Hangfire;
using WorkerService.DependencyInjections;
using WorkerService.Hangfire;

namespace WorkerService;

public class Program
{
	public static void Main(string[] args)
	{
		var host = CreateHostBuilder(args).Build();

		DatabaseInitializer.InitializeDatabase(host.Services.GetRequiredService<IConfiguration>());

		host.Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddWorkerService(hostContext.Configuration);
				services.AddHostedService<HangfireWorker>();
			});
}