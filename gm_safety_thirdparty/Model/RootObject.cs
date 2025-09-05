using System.Text.Json.Serialization;

namespace gm_safety_thirdparty.Models
{
    public class RootObject
    {
        [JsonPropertyName("employee_data")]
        public Employee[]? employee_data { get; set; }
    }
}