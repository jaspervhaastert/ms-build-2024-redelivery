namespace WebApp.Configuration;

public class AzureOpenAiConfiguration
{
    public const string SectionKey = "AzureOpenAi";

    public string DeploymentName { get; set; } = null!;
    public string Endpoint { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}