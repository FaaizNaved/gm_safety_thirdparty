namespace gm_safety_thirdparty.Options
{
    public class DarwinBoxOptions
    {
        public string ApiUrl { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;   // Basic auth user
        public string Password { get; set; } = string.Empty;   // Basic auth pass
        public string ApiKey { get; set; } = string.Empty;     // DarwinBox API key
        public string DatasetKey { get; set; } = string.Empty; // DarwinBox dataset key (default)
    }
}