using Hangfire;
using WorkerService;
using WorkerService.DependencyInjections;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWorkerService(builder.Configuration);
builder.Services.AddHangfireServer();

var host = builder.Build();

using (IServiceScope scope = host.Services.CreateScope())
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

await host.RunAsync();