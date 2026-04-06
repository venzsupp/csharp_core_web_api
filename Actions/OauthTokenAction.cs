using System.Data;
using System.Numerics;
using csharp_core_web_api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; 


namespace csharp_core_web_api.Actions;

public class OAuthTokenAction
{
    private OAuthCredentialsTokenDbContext _oAuthCredentialsTokenDbContext;

    public OAuthTokenAction( OAuthCredentialsTokenDbContext oAuthCredentialsTokenDbContext)
    {
        _oAuthCredentialsTokenDbContext = oAuthCredentialsTokenDbContext;
    }

    public async Task<Int32> SaveOAuthToken(OAuthCredentialsToken oAuthToken)
    {
        try
        {
            Console.WriteLine("1-----res");
            // _oAuthCredentialsTokenDbContext.OAuthCredentialsToken.Add
            _oAuthCredentialsTokenDbContext.OAuthCredentialsToken.Add(oAuthToken); // Stage insert
            Console.WriteLine("2-----res");
            var res = await _oAuthCredentialsTokenDbContext.SaveChangesAsync(); // Commit to database 
            Console.WriteLine("res");
            Console.WriteLine(res);
            return res;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

}
