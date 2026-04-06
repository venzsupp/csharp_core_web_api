using System.Text.Json;
using System.Text.Json.Serialization;

namespace csharp_core_web_api.RequestResponse;

public class OAuthCredentialsResponse 
{
    [JsonPropertyName("access_token")]
    public string? AccessToken {get; set;}

    [JsonPropertyName("expires_in")]
    public long? Expire {get; set;}

    public DateTime ExpiryDate {get; set;}

    [JsonPropertyName("scope")]
    public string? Scope {get; set;}

    [JsonPropertyName("token_type")]
    public string? TokenType {get; set;}

}