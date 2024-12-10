using Hangfire;
using WorkerService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWorkerService(builder.Configuration);
builder.Services.AddHangfireServer();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
	Worker worker = scope.ServiceProvider.GetRequiredService<Worker>();
	await worker.Execute();
}

RecurringJob.AddOrUpdate<Worker>(
	"coin-job",
	job => job.Execute(),
	Cron.Hourly());

await host.RunAsync();