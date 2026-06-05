using DomainEntity.Request;
using Identity.Exceptions;
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

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] CustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var (token, validTo, customer) = await _authService.RegisterAsync(request, cancellationToken);
            return Ok(new { token, validTo, customer });
        }
        catch (AuthException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LoginAsync([FromBody] CustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var (token, validTo, customer) = await _authService.LoginAsync(request, cancellationToken);
            return Ok(new { token, validTo, customer });
        }
        catch (AuthException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
}
