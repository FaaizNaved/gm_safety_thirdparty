using System.Collections.Generic;
using System.Threading.Tasks;
using gm_safety_thirdparty.Models;
using gm_safety_thirdparty.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gm_safety_thirdparty.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EmployeeIngestService _employeeService;
        private readonly ExportService _exportService;

        public IndexModel(EmployeeIngestService employeeService, ExportService exportService)
        {
            _employeeService = employeeService;
            _exportService = exportService;
        }

        public List<Employee> Employees { get; set; } = new();

        public async Task OnGetAsync()
        {
            Employees = await _employeeService.GetAllEmployeesAsync();
        }

        public async Task<IActionResult> OnGetExportExcel()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            var excelBytes = _exportService.ExportToExcel(employees);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
        }

       
    }
}
