namespace csharp_core_web_api.Models;

public class OAuthCredentialsToken 
{
    public string? Token {get; set;}

    public int Id { get; set; }

    public DateTime ExpiryDate {get; set;}

}