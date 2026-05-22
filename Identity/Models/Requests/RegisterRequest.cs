using System.ComponentModel.DataAnnotations;

namespace Identity.Models.Requests;

/// <summary>Запит на реєстрацію нового користувача.</summary>
public record RegisterRequest
{
    public string? Name { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Phone]
    public string? Phone { get; init; }

    [Required]
    [MinLength(6)]
    public string Password { get; init; } = string.Empty;
}
