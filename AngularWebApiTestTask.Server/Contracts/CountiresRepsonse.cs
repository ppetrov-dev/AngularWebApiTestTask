using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Contracts;

public record CountriesResponse(IEnumerable<Country> Countries);
