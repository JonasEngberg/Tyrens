using WebApi.Models;

namespace WebApi.Services;

public class NotificationService : INotificationService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<NotificationService> _logger;

	public NotificationService(IHttpClientFactory httpClientFactory, ILogger<NotificationService> logger)
	{
		_httpClientFactory = httpClientFactory;
		_logger = logger;
	}

	public async Task NotifyCallerAsync(Job job)
	{
		try
		{
			using HttpClient client = _httpClientFactory.CreateClient();

			HttpResponseMessage response = await client.PostAsync(job.WebhookUrl, new StringContent(job.Id.ToString()));

			if (response.IsSuccessStatusCode)
			{
				_logger.LogInformation($"{job.Id}: Webhook call was successful ");
			}
			else
			{
				// Do not logg as an error as this is an user input...
				_logger.LogInformation($"{job.Id}: Something is wrong with the Webhook. Not successful...");
			}
		}
		catch (Exception ex)
		{
			// Do not logg as an error as this is an user input...
			_logger.LogInformation($"{job.Id}: Something is wrong with the Webhook", ex);

			// Swollow...
		}
	}
}
