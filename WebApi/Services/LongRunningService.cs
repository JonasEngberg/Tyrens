using WebApi.Services;

public class LongRunningService : BackgroundService
{
	private readonly BackgroundWorkerQueue queue;

	public LongRunningService(BackgroundWorkerQueue queue)
	{
		this.queue = queue;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			Func<CancellationToken, Task> workItem = await queue.DequeueAsync(stoppingToken);

			await workItem(stoppingToken);
		}
	}
}