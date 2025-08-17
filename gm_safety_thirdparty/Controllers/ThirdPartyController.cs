using Microsoft.AspNetCore.Mvc;
using gm_safety_thirdparty.Models;
using gm_safety_thirdparty.Repositories;

namespace gm_safety_thirdparty.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThirdPartyController : ControllerBase
    {
        private readonly IThirdPartyRepository _repository;

        public ThirdPartyController(IThirdPartyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vendor = _repository.GetById(id);
            if (vendor == null) return NotFound();
            return Ok(vendor);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ThirdPartyVendor vendor)
        {
            _repository.Add(vendor);
            return CreatedAtAction(nameof(GetById), new { id = vendor.Id }, vendor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ThirdPartyVendor vendor)
        {
            if (id != vendor.Id) return BadRequest();
            _repository.Update(vendor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}
