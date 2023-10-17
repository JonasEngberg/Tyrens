namespace WebApi.Models;

public class NewJobDTO
{
    public string User { get; set; }

    public string InputFolder { get; set; }

	public string OutputFolder { get; set; }

	public string WebhookUrl { get; set; }
}
