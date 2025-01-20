using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Tests.Database.Models;

public class UserBuilder
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int ProvinceId { get; set; }

    public User Build() => new()
    {
        Id = Id,
        Login = Login,
        Password = Password,
        ProvinceId = ProvinceId
    };

    public static User Any() => new UserBuilder().Build();
}