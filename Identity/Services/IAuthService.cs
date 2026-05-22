using Identity.Models.Requests;
using Identity.Models.Responses;

namespace Identity.Services;

/// <summary>Реєстрація та вхід користувачів.</summary>
public interface IAuthService
{
    /// <summary>Реєструє нового користувача та одразу видає токен.</summary>
    Task<SignInResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    /// <summary>Перевіряє облікові дані та видає токен.</summary>
    Task<SignInResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
