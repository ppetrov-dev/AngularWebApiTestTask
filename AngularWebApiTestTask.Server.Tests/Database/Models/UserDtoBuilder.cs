using AngularWebApiTestTask.Server.Domain;

namespace AngularWebApiTestTask.Server.Tests.Database.Models;

public class UserDtoBuilder
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public bool AgreeToTerms { get; set; }

    public string ProvinceName { get; set; } = string.Empty;

    public string CountryName { get; set; } = string.Empty;

    public UserDto Build() => new UserDto(Id, Login, AgreeToTerms, ProvinceName, CountryName);

    public static UserDto Any() => new UserDtoBuilder().Build();
}