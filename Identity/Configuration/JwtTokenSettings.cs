namespace Identity.Configuration;

/// <summary>
/// Налаштування для генерації JWT-токенів. Заповнюється з секції "JwtTokenSettings" в appsettings.json.
/// </summary>
public class JwtTokenSettings
{
    /// <summary>Видавець токена (iss).</summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>Аудиторія токена (aud).</summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>Симетричний ключ для підпису токена (HMAC-SHA256). Має бути достатньо довгим (>= 32 символи).</summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>Час життя токена у хвилинах.</summary>
    public int TokenLifespanInMinutes { get; set; } = 60;
}
