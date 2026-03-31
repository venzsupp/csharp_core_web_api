using Microsoft.AspNetCore.Mvc;
using csharp_core_web_api.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Text.Json;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace csharp_core_web_api.Controllers;


[Route("api/[Controller]")]
[ApiController]

public class OauthController : ControllerBase
{
    private readonly ILogger<OauthController> _logger;
    private readonly OAuthCredentials _config;

    public OauthController(ILogger<OauthController> logger, IOptions<OAuthCredentials> options)
    {
        _logger = logger;
        _config = options.Value;

        
    }

    [HttpGet("authorize")]
    public async Task<IActionResult> AuthorizationCode([FromQuery] OauthAuthorizationCode oauthAuthorizationCode)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", "AsvMJsJwFhnpBP0PDUm0r1aBXQcH1PVd" },
                { "redirect_uri", "http://api.hscsharp.com:5021/api/Oauth/callback" },
                { "state", "test12345" },
                {"scope","openid uditiitg@gmail.com"}
            };

            var content = new FormUrlEncodedContent(parameters);
            string queryString = await content.ReadAsStringAsync();
            string responseBody = "";
            var url = "https://dev-53038owxmae5eghj.au.auth0.com/authorize?" + queryString;
            /*using (HttpClient client = new HttpClient())
            {
                Console.WriteLine(url);
                HttpResponseMessage response = await client.GetAsync("https://dev-53038owxmae5eghj.au.auth0.com/authorize?" + queryString);
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP status code is not 2xx

                responseBody = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(responseBody);
            }*/
            return Redirect(url);
            // return new OkObjectResult(new { result = responseBody });
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
            //Console.WriteLine(code);
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
            Console.WriteLine("content");
            Console.WriteLine(content);

            //return new OkObjectResult(new { result = "code" });
            var response = await client.PostAsync($"https://{domain}/oauth/token", content);
            var body = await response.Content.ReadAsStringAsync();

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