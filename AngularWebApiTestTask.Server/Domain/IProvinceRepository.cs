using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Domain;

public interface IProvinceRepository
{
    Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId);
}