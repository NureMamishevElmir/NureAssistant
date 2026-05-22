using Identity.Exceptions;
using Identity.Models.Requests;
using Identity.Models.Responses;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NureAssistant.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerAuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public CustomerAuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Реєстрація нового користувача.</summary>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SignInResponse>> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _authService.RegisterAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (AuthException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>Вхід користувача.</summary>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SignInResponse>> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _authService.LoginAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (AuthException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
}
