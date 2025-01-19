using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Domain;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
}