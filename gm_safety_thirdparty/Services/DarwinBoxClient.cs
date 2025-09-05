using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using gm_safety_thirdparty.Models;
using gm_safety_thirdparty.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace gm_safety_thirdparty.Services
{
    public class DarwinBoxClient
    {
        private readonly HttpClient _http;
        private readonly DarwinBoxOptions _opt;

        public DarwinBoxClient(HttpClient http, IOptions<DarwinBoxOptions> opt)
        {
            _http = http;
            _opt = opt.Value;
        }

        public async Task<RootObject?> FetchEmployeesAsync(string? overrideDatasetKey, CancellationToken ct = default)
        {
            // var datasetKey = string.IsNullOrWhiteSpace(overrideDatasetKey) ? _opt.DatasetKey : overrideDatasetKey;

            // Basic auth
            var bytes = Encoding.ASCII.GetBytes($"{_opt.Username}:{_opt.Password}");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var body = new
            {
                api_key = _opt.ApiKey,
                datasetKey = _opt.DatasetKey
            };

            using var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            using var req = new HttpRequestMessage(HttpMethod.Post, _opt.ApiUrl) { Content = content };

            using var resp = await _http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var err = await resp.Content.ReadAsStringAsync(ct);
                throw new InvalidOperationException($"DarwinBox API failed: {(int)resp.StatusCode} {resp.ReasonPhrase}. Body: {err}");
            }

            var json = await resp.Content.ReadAsStringAsync(ct);
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(handler);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new Utility.DateOnlyJsonConverter());
            options.Converters.Add(new Utility.StringToNullableIntConverter());
            //options.Converters.Add(new Utility.StringToBoolConverter());

            return JsonSerializer.Deserialize<RootObject>(json, options);
        }
    }
}