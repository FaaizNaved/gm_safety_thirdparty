using GM_Safety_Functional.Models;
using GM_Safety_Functional.Services;
using Microsoft.AspNetCore.Mvc;

namespace gm_safety_thirdparty.Controllers;

[ApiController]
[Route("api/functional")]
public class FunctionalController : ControllerBase
{
    private readonly IFunctionalService _service;
    public FunctionalController(IFunctionalService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FunctionalTask>>> GetAll(CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FunctionalTask>> Get(int id, CancellationToken ct)
    {
        var item = await _service.GetAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<FunctionalTask>> Create([FromBody] FunctionalTask model, CancellationToken ct)
    {
        var created = await _service.CreateAsync(model, ct);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<FunctionalTask>> Update(int id, [FromBody] FunctionalTask model, CancellationToken ct)
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
