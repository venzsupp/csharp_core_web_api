// using System.Data;
using System.Numerics;
using csharp_core_web_api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; 
using csharp_core_web_api.Abstracts.Exceptions;

namespace csharp_core_web_api.Actions;

public class UserAction(UserDbContext userDbContext)
{
    private UserDbContext _userDbContext = userDbContext;

    public async Task<Int32> SaveUser(Users user)
    {
        try
        {
            _userDbContext.Users.Add(user); // Stage insert
            var res = await _userDbContext.SaveChangesAsync(); // Commit to database 
            return res;
        }
        catch (Exception ex)
        {
            throw new DataAccessException("Failed to save", ex);
        }
        
    }

    public async Task<List<Users>> GetUsers()
    {
        try 
        {
            return await _userDbContext.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new DataAccessException("Get user data", ex);
        }
    }
}