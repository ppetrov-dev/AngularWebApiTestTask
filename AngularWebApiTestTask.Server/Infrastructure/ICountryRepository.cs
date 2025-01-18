using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Infrastructure;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
}