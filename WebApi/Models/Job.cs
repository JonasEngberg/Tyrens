using System.Reflection.Metadata.Ecma335;

namespace WebApi.Models
{
	public class Job
	{
		public Job(Guid id, string user, string inputFolder, string outputFolder, string? webhookUrl, DateTime startTime, bool isComplete)
        {
			Id = id;
			User = user;
			InputFolder = inputFolder;
			OutputFolder = outputFolder;
			WebhookUrl = webhookUrl;
			IsComplete = isComplete;
			StartTime = startTime;
		}

		public static Job Create(string user, string inputFolder, string outputFolder, string? webhookUrl)
		{
			return new Job(
				Guid.NewGuid(),
				user,
				inputFolder,
				outputFolder,
				webhookUrl,
				DateTime.UtcNow,
				false);
		}

		internal void SetCompleted()
		{
			IsComplete = true;
			EndTime = DateTime.UtcNow;
		}

		public Guid Id { get; init; }

		public string User { get; init; }

		public string InputFolder { get; init; }

		public string? WebhookUrl { get; init; } = string.Empty;

		public DateTime StartTime { get; init; }

		public string OutputFolder { get; private set; } = string.Empty;

		public bool IsComplete { get; private set; }

		public DateTime? EndTime { get; private set; }
	}
}
