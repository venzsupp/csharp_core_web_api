using Microsoft.AspNetCore.Mvc;
using csharp_core_web_api.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Text.Json;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using csharp_core_web_api.RequestResponse;
using csharp_core_web_api.Actions;

namespace csharp_core_web_api.Controllers;


[Route("api/[Controller]")]
[ApiController]

public class OauthController : ControllerBase
{
    private readonly ILogger<OauthController> _logger;
    private readonly OAuthCredentials _config;
    private readonly OauthAuthorizationCode _configCode;

    private OAuthCredentialsTokenDbContext _oAuthTokenActionDbContext;


    public OauthController(ILogger<OauthController> logger, IOptions<OAuthCredentials> options, IOptions<OauthAuthorizationCode> optionsCode, OAuthCredentialsTokenDbContext oAuthTokenActionDbContext)
    {
        _logger = logger;
        _config = options.Value;
        _configCode = optionsCode.Value;
        _oAuthTokenActionDbContext = oAuthTokenActionDbContext;
    }

    [HttpGet("authorize")]
    public async Task<IActionResult> AuthorizationCode([FromQuery] OauthAuthorizationCode oauthAuthorizationCode)
    {
        try
        {
            var clientId = _configCode.ClientId;
            var state = _configCode.State;
            var scope = _configCode.Scope;
            var redirectUri = _configCode.RedirectUri;
            var parameters = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", clientId },
                { "redirect_uri", redirectUri },
                { "state", state },
                {"scope",scope}
            };

            var content = new FormUrlEncodedContent(parameters);
            string queryString = await content.ReadAsStringAsync();
            string responseBody = "";
            var url = "https://dev-53038owxmae5eghj.au.auth0.com/authorize?" + queryString;
            
            return new OkObjectResult(new { result = url });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { error = ex.Message });
        }

    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code, string state)
    {
        if (string.IsNullOrEmpty(code))
        {
            return BadRequest("Missing authorization code.");
        }

        //var token = await ExchangeCodeForTokenAsync(code);

        // Optional: Fetch user info using the access token
        //var userInfo = await GetUserInfoAsync(token.AccessToken);
        Console.WriteLine(code);
        return new OkObjectResult(new { result = code });
    }

    [HttpPost("token")]
    public async Task<IActionResult> ExchangeCodeForTokenAsync(string code)
    {
 
            var client = new HttpClient();
            
            var domain = _config.Domain;
            var clientId = _config.ClientID;
            var clientSecret = _config.ClientSecret;
            var redirectUri = _config.RedirectUrl;
            // string code = "6P1jkEzqDBfX_xvBeORvkzPQtouISQAV6PTira0Xgqkhk";
            Console.WriteLine(code);
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "code", code },
                { "redirect_uri", redirectUri }
            };

            var content = new FormUrlEncodedContent(parameters);
            Console.WriteLine(content);
            var response = await client.PostAsync($"https://{domain}/oauth/token", content);
            var body = await response.Content.ReadAsStringAsync();

            // if (!responseji)NM pl.ui  .IsSuccessStatusCode)
            //     throw new Exception($"Auth0 token exchange failed: {body}");

            return new OkObjectResult(body);

    }


    [HttpGet("oauth_token")]
    public async Task<IActionResult> OauthTokenAsync()
    {
        try { 
            var client = new HttpClient();
            var domain = _config.Domain;
            var clientId = _config.ClientID;
            var clientSecret = _config.ClientSecret;
            var redirectUri = _config.RedirectUrl;
            var audienceUrl = $"https://{domain}/api/v2/";
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                {"audience", audienceUrl}
            };

            var content = new FormUrlEncodedContent(parameters);

            //return new OkObjectResult(new { result = "code" });
            var response = await client.PostAsync($"https://{domain}/oauth/token", content);
            var body = await response.Content.ReadAsStringAsync();

            var OAuthData = JsonSerializer.Deserialize<OAuthCredentialsResponse>(body);
            
            DateTime TokenExpire = DateTime.Now;

            if (OAuthData.Expire.HasValue) 
            {
                TokenExpire = DateTime.UtcNow.AddSeconds(OAuthData.Expire.Value);
                // TokenExpire = DateTimeOffset.FromUnixTimeSeconds(expiry).UtcDateTime;
            }
            OAuthCredentialsToken _oAuthCredentialsToken = new()
            {
                Token = OAuthData.AccessToken,
                ExpiryDate = TokenExpire

            };
            Console.WriteLine($"Date:: {_oAuthCredentialsToken.ExpiryDate}");
            OAuthTokenAction oAuthTokenAction = new( _oAuthTokenActionDbContext);
            await oAuthTokenAction.SaveOAuthToken(_oAuthCredentialsToken);

            return new OkObjectResult(body);
        } 
        catch (Exception ex) 
        {
            return new BadRequestObjectResult(new { 
                Error = "Error", 
                Details = ex.Message
            });
        }

    }


}