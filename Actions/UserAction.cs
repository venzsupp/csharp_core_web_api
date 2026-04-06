using System.Data;
using System.Numerics;
using csharp_core_web_api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; 

namespace csharp_core_web_api.Actions;

public class UserAction
{
    private UserDbContext _userDbContext;

    public UserAction( UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<Int32> SaveUser(Users user)
    {
        try
        {
            _userDbContext.Users.Add(user); // Stage insert
            var res = await _userDbContext.SaveChangesAsync(); // Commit to database 
            return res;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    public async Task<List<Users>> GetUsers()
    {
        try 
        {
            return await _userDbContext.Users.ToListAsync();
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }
}