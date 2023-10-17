using WebApi.Models;

namespace WebApi.Services
{
	public interface INotificationService
	{
		Task NotifyCallerAsync(Job job);
	}
}