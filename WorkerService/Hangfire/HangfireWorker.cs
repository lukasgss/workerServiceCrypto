using Hangfire;

namespace WorkerService.Hangfire;

public sealed class HangfireWorker : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	public HangfireWorker(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		using (var scope = _serviceProvider.CreateScope())
		{
			Worker worker = scope.ServiceProvider.GetRequiredService<Worker>();

			BackgroundJob.Enqueue(() => worker.ExecuteCoins());
			BackgroundJob.Enqueue(() => worker.ExecuteExchanges());
		}

		RecurringJob.AddOrUpdate<Worker>(
			"coin-job",
			job => job.ExecuteCoins(),
			Cron.Hourly());

		RecurringJob.AddOrUpdate<Worker>(
			"exchange-job",
			job => job.ExecuteExchanges(),
			Cron.Hourly());

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}