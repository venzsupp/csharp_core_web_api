using System.Data;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using Microsoft.Data.SqlClient;
using csharp_core_web_api.Models;

namespace csharp_core_web_api.Actions;

public class LoginAction
{
    private IDbConnection _dbConnection;

    private UserDbContext _userDbContext;
    

    public LoginAction( UserDbContext userDbContext)
    {
        // _dbConnection = dbConnection;
        _userDbContext = userDbContext;
    }
    public async Task Store(Users user )
    {
        // if (_dbConnection is SqlConnection sqlConnection)
        // {
        //     await sqlConnection.OpenAsync();

        //    Console.WriteLine( sqlConnection.State);

        // }

        _userDbContext.Users.Add(user); // Stage insert
        await _userDbContext.SaveChangesAsync(); // Commit to database

    }
}