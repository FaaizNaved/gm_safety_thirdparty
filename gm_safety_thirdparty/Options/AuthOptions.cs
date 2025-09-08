namespace gm_safety_thirdparty.Auth;

public class AuthOptions
{
    public BasicUser[] Users { get; set; } = Array.Empty<BasicUser>();
    public string ApiKeyHeader { get; set; } = "X-Api-Key";
    public string DatasetKeyHeader { get; set; } = "X-Dataset-Key";
    public string ExpectedApiKey { get; set; } = "";       // from config
    public string ExpectedDatasetKey { get; set; } = "";   // from config
    public string ApiKey { get; set; } = string.Empty;
    public string DataSetKey { get; set; } = string.Empty; // strict validation
}

public class BasicUser
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = ""; // store securely in real deployments
}
