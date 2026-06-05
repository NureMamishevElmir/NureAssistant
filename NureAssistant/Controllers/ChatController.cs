using DomainEntity.CustomerEntities.Enums;
using DomainEntity.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace NureAssistant.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _service;

    public ChatController(IChatService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Set([FromQuery] Guid? id, [FromBody] ChatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _service.SetAsync(id, request, cancellationToken));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();

    [Authorize(Roles = nameof(CustomerType.Administrator))]
    [HttpDelete]
    public async Task<IActionResult> DeleteStale([FromQuery] int days, CancellationToken cancellationToken)
    {
        if (days <= 0)
        {
            days = 365;
        }

        var deleted = await _service.DeleteStaleAsync(days, cancellationToken);
        return Ok(new { deleted });
    }

    [Authorize(Roles = nameof(CustomerType.Administrator))]
    [HttpDelete]
    public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAllAsync(cancellationToken);
        return Ok(new { deleted });
    }
}
