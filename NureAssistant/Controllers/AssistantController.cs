using AIIntegration.Services;
using DomainEntity.Exchange;
using DomainEntity.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NureAssistant.Services;

namespace NureAssistant.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class AssistantController : ControllerBase
{
    private readonly IAssistantService _assistantService;
    private readonly IAskGraphService _askGraphService;

    public AssistantController(IAssistantService assistantService, IAskGraphService askGraphService)
    {
        _assistantService = assistantService;
        _askGraphService = askGraphService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MessageExchange), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<MessageExchange>> AskAsync([FromBody] MessageRequest request, CancellationToken cancellationToken)
    {
        await _askGraphService.EnsureGraphAsync(request, cancellationToken);
        var reply = await _assistantService.AskAsync(request, cancellationToken);
        return Ok(reply);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> HealthAsync(CancellationToken cancellationToken)
    {
        var available = await _assistantService.IsAvailableAsync(cancellationToken);
        return Ok(available);
    }
}
