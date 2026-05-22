namespace Identity.Exceptions;

/// <summary>
/// Очікувана помилка автентифікації (наприклад, email уже зайнятий або невірні облікові дані).
/// Контролер перетворює її на відповідь 400/401 замість 500.
/// </summary>
public class AuthException : Exception
{
    public AuthException(string message) : base(message)
    {
    }
}
