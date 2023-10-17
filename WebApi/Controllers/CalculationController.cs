using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
public class CalculationController : ControllerBase
{
	private readonly ILogger<CalculationController> _logger;
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
	private readonly IJobRepository _repository;

	public CalculationController(
		ILogger<CalculationController> logger,
		IServiceScopeFactory serviceScopeFactory,
		BackgroundWorkerQueue backgroundWorkerQueue,
		IJobRepository repository)
	{
		_logger = logger;
		_serviceScopeFactory = serviceScopeFactory;
		_backgroundWorkerQueue = backgroundWorkerQueue;
		_repository = repository;
	}

	[HttpPost("api/calculation")]
	public IActionResult Calculate([FromBody] NewJobDTO dto)
	{
		if (IsValidCalculationInput(dto) == false)
		{
			return BadRequest();
		}

		Job newJob = Job.Create(dto.User, dto.InputFolder, dto.OutputFolder, dto.WebhookUrl);

		QueueCalculation(newJob);

		return Ok(newJob.Id);
	}

	[HttpGet("api/calculation")]
	public IEnumerable<Job> Get()
	{
		return _repository.GetAll();
	}

	private bool IsValidCalculationInput(NewJobDTO dto)
	{
		// check user, input folder, output folder and webhook url...
		return true;
	}

	private void QueueCalculation(Job job)
	{
		_logger.LogInformation($"{job.Id}: Insert into queue");

		_backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
		{
			using IServiceScope scope = _serviceScopeFactory.CreateScope();

			IServiceProvider scopedServiceProvider = scope.ServiceProvider;
			IJobRepository repository = scopedServiceProvider.GetRequiredService<IJobRepository>();
			INotificationService service = scopedServiceProvider.GetRequiredService<INotificationService>();

			_logger.LogInformation($"{job.Id}: Start calculations ");
			await repository.AddAsync(job);

			await DoCalculation(job, token);

			_logger.LogInformation($"{job.Id}: Calculations complete");
			job.SetCompleted();
			await repository.UpdateAsync(job);
			await service.NotifyCallerAsync(job);
		});
	}

	private async Task DoCalculation(Job job, CancellationToken token)
	{
		int calculationDuration = 10000;

		_logger.LogInformation($"{job.Id}: Calculations start");
		await Task.Delay(calculationDuration, token);
		_logger.LogInformation($"{job.Id}: Calculations end");

	}
}
