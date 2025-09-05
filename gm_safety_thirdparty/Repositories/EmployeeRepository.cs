using gm_safety_thirdparty.Data;
using gm_safety_thirdparty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace gm_safety_thirdparty.Repositories
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;


        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertOrIgnoreAsync(IEnumerable<Employee> employees, CancellationToken ct = default)
        {
            int insertedCount = 0, skippedCount = 0;

            foreach (var emp in employees)
            {
                if (!(emp.EmployeeId.HasValue && emp.EmployeeId.Value != 0))
                {
                    skippedCount++;
                    continue;
                }
                // Check if Employee already exists (like ON CONFLICT DO NOTHING)
                bool exists = await _context.Employees
                    .AnyAsync(e => e.EmployeeId == emp.EmployeeId, ct);

                if (!exists)
                {
                    // Add only if not exists
                    _context.Employees.Add(emp);
                    insertedCount++;
                }
                else
                {
                    skippedCount++;
                }
            }

            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("InsertOrIgnore completed: Inserted={Inserted}, Skipped={Skipped}",
                              insertedCount, skippedCount);
        }
        public async Task<List<Employee>> GetAllEmployeesAsync(CancellationToken ct = default)
        {
            return await _context.Employees.ToListAsync(ct);
        }
    }
}