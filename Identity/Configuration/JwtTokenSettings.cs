namespace Identity.Configuration;

public class JwtTokenSettings
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public int TokenLifespanInMinutes { get; set; } = 60;
}
