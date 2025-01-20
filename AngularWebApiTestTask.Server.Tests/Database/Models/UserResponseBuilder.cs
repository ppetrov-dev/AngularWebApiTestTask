using AngularWebApiTestTask.Server.Contracts;

namespace AngularWebApiTestTask.Server.Tests.Database.Models;

public class UserResponseBuilder
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string ProvinceName { get; set; } = string.Empty;

    public string CountryName { get; set; } = string.Empty;

    public UserResponse Build() => new (Id, Login, ProvinceName, CountryName);

    public static UserResponse Any() => new UserResponseBuilder().Build();
}