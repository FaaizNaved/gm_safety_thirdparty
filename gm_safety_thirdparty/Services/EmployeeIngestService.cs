using gm_safety_thirdparty.Models;
using gm_safety_thirdparty.Options;
using gm_safety_thirdparty.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using gm_safety_thirdparty.Auth;

namespace gm_safety_thirdparty.Services
{
    public class EmployeeIngestService
    {
        private readonly DarwinBoxClient _client;
        private readonly EmployeeRepository _repo;
        private readonly AuthOptions _auth;
        private readonly ILogger<EmployeeIngestService> _logger;


        public EmployeeIngestService(
            DarwinBoxClient client,
            EmployeeRepository repo,
            IOptions<AuthOptions> auth,
            ILogger<EmployeeIngestService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _auth = auth?.Value ?? throw new ArgumentNullException(nameof(auth));
            _logger = logger;
        }

        public bool ValidateApiKey(string apiKey) =>
            !string.IsNullOrWhiteSpace(apiKey) &&
            string.Equals(apiKey, _auth.ApiKey, StringComparison.Ordinal);

        public bool ValidateDataSetKey(string dataSetKey) =>
            !string.IsNullOrWhiteSpace(dataSetKey) &&
            string.Equals(dataSetKey, _auth.DataSetKey, StringComparison.Ordinal);

        public async Task<int> SyncAsync(string? dataSetKey = null, CancellationToken ct = default)
        {
            _logger.LogInformation("Starting employee sync with datasetKey: {DatasetKey}", dataSetKey ?? _auth.DataSetKey);

            var root = await _client.FetchEmployeesAsync(dataSetKey, ct);

            var employees = root?.employee_data?
                .Where(e => e != null)
                .ToArray() ?? Array.Empty<Employee>();

            _logger.LogInformation("Fetched {Count} employees from DarwinBox", employees.Length);

            if (employees.Length == 0)
            {
                _logger.LogWarning("No employees returned from DarwinBox.");
                return 0;
            }

            foreach (var e in employees)
            {
                e.RECORD_SOURCE_NAME ??= "DarwinBox";
            }

            await _repo.InsertOrIgnoreAsync(employees, ct);

            _logger.LogInformation("Inserted {Count} new employees into database", employees.Length);

            return employees.Length;
        }
        public async Task<List<Employee>> GetAllEmployeesAsync(CancellationToken ct = default)
        {
            return await _repo.GetAllEmployeesAsync(ct);
        }
    }
}