using csharp_core_web_api.Models;
using csharp_core_web_api.Actions;

namespace UnitTest.UserTest;

public class UserActionTest
{

    [Fact]
    public async Task SaveUser_ReturnInt()
    {
        Users user = new();
        user.UserName = "TestFName";
        user.Password = "TestPassword";

        UserAction userAction = new();
        await userAction.SaveUser(user);
    }
}
