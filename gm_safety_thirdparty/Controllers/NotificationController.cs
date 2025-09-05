using GM_Safety_Notification.Models;
using GM_Safety_Notification.Services;
using Microsoft.AspNetCore.Mvc;

namespace gm_safety_thirdparty.Controllers;

[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _service;
    public NotificationController(INotificationService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetAll(CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Notification>> Get(int id, CancellationToken ct)
    {
        var item = await _service.GetAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Notification>> Create([FromBody] Notification model, CancellationToken ct)
    {
        var created = await _service.CreateAsync(model, ct);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Notification>> Update(int id, [FromBody] Notification model, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, model, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var ok = await _service.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
