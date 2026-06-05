using DomainEntity.Request;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace NureAssistant.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Set([FromQuery] Guid? id, [FromBody] CustomerRequest request, CancellationToken cancellationToken)
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
}
