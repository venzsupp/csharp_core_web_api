namespace csharp_core_web_api.Models;

public class OauthAuthorizationCode
{
    public string? ClientId { get; set; }
    public string? RedirectUri { get; set; }
    public string? ResponseType { get; set; }
    public string? Scope { get; set; }
    public string? State { get; set; }

}