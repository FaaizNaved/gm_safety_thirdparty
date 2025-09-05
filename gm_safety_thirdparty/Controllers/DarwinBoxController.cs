using gm_safety_thirdparty.Models;
using gm_safety_thirdparty.Services;
using Microsoft.AspNetCore.Mvc;

namespace gm_safety_thirdparty.Controllers
{
    [ApiController]
    [Route("api/darwinbox")]
    public class DarwinBoxController : ControllerBase
    {
        private readonly EmployeeIngestService _svc;
        private readonly ExportService _exportService = new ExportService();

        public DarwinBoxController(EmployeeIngestService svc)
        {
            _svc = svc;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync()
        {
            var count = await _svc.SyncAsync(); // optional: pass CancellationToken.None
            return Ok(new { inserted = count });
        }
        // DarwinBoxController.cs
        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _svc.GetAllEmployeesAsync();
            return Ok(employees);
        }
        [HttpGet("employees/export/excel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var employees = await _svc.GetAllEmployeesAsync();
            var excelBytes = _exportService.ExportToExcel(employees);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
        }

    }

}