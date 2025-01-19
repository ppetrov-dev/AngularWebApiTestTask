namespace AngularWebApiTestTask.Server.Domain;

public record class UserDto(int Id, string Login, bool AgreeToTerms, string ProvinceName, string CountryName);
