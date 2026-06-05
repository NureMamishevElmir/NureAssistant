using System.Net.Http.Json;
using System.Text.Json;
using AIIntegration.Configuration;
using DomainEntity.Exchange;
using DomainEntity.Request;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AIIntegration.Services.Impl;

public class AssistantService : IAssistantService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient;
    private readonly OpenWebUiSettings _settings;
    private readonly ILogger<AssistantService> _logger;

    public AssistantService(
        HttpClient httpClient,
        IOptions<OpenWebUiSettings> settings,
        ILogger<AssistantService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<MessageExchange> AskAsync(MessageRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            throw new ArgumentException("Текст повідомлення не може бути порожнім.", nameof(request));
        }

        var payload = new Dictionary<string, object?>
        {
            ["model"] = _settings.DefaultModel,
            ["stream"] = false,
            ["messages"] = new[] { new { role = "user", content = request.Text } },
        };

        if (request.FileId != Guid.Empty)
        {
            payload["files"] = new[] { new { type = "collection", id = request.FileId.ToString() } };
        }

        _logger.LogInformation(
            "OpenWebUI -> Model={Model}, FileId={FileId}, HasFile={HasFile}, Payload={Payload}",
            _settings.DefaultModel,
            request.FileId,
            request.FileId != Guid.Empty,
            JsonSerializer.Serialize(payload, JsonOptions));

        using var response = await _httpClient.PostAsJsonAsync(
            "/api/chat/completions", payload, JsonOptions, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Open WebUI повернув {StatusCode}: {Body}", (int)response.StatusCode, error);
            throw new HttpRequestException(
                $"Open WebUI повернув {(int)response.StatusCode} ({response.ReasonPhrase}).");
        }

        var raw = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogInformation("OpenWebUI <- {Raw}", raw);

        var content = ExtractContent(raw);

        return new MessageExchange
        {
            Text = content,
            CustomerId = request.CustomerId,
            ChatId = request.ChatId,
            FileId = request.FileId,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await _httpClient.GetAsync("/health", cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Open WebUI недоступний за адресою {BaseUrl}.", _settings.BaseUrl);
            return false;
        }
    }

    private static string ExtractContent(string rawJson)
    {
        using var document = JsonDocument.Parse(rawJson);

        if (document.RootElement.TryGetProperty("choices", out var choices)
            && choices.ValueKind == JsonValueKind.Array
            && choices.GetArrayLength() > 0
            && choices[0].TryGetProperty("message", out var message)
            && message.TryGetProperty("content", out var contentProperty))
        {
            return contentProperty.GetString() ?? string.Empty;
        }

        return string.Empty;
    }
}
