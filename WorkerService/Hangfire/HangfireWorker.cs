using Hangfire;

namespace WorkerService.Hangfire;

public sealed class HangfireWorker : IHostedService
{
	private readonly IServiceProvider _serviceProvider;
	private const string CoinJob = "coin-job";
	private const string ExchangeJob = "exchange-job";

	public HangfireWorker(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		RecurringJob.AddOrUpdate<Worker>(
			CoinJob,
			job => job.ExecuteCoins(),
			Cron.Hourly);

		RecurringJob.AddOrUpdate<Worker>(
			ExchangeJob,
			job => job.ExecuteExchanges(),
			Cron.Hourly);

		RecurringJob.TriggerJob(CoinJob);
		RecurringJob.TriggerJob(ExchangeJob);

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}