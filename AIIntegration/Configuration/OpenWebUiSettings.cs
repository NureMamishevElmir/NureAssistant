namespace AIIntegration.Configuration;

public class OpenWebUiSettings
{
    public const string SectionName = "OpenWebUI";

    public string BaseUrl { get; set; } = "http://localhost:3001";

    public string ApiKey { get; set; } = string.Empty;

    public string DefaultModel { get; set; } = "llama3.1:8b";

    public int TimeoutSeconds { get; set; } = 120;
}
