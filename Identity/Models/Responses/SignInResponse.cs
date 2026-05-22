namespace Identity.Models.Responses;

/// <summary>Відповідь з виданим токеном після успішної реєстрації або входу.</summary>
/// <param name="Token">JWT access-токен.</param>
/// <param name="ValidTo">Момент завершення дії токена (локальний час).</param>
/// <param name="CustomerId">Ідентифікатор користувача.</param>
public record SignInResponse(string Token, DateTime ValidTo, Guid CustomerId);
