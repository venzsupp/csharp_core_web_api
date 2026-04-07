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
            _oAuthCredentialsTokenDbContext.OAuthCredentialsToken.Add(oAuthToken); // Stage insert
            var res = await _oAuthCredentialsTokenDbContext.SaveChangesAsync(); // Commit to database 
            return res;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

}
