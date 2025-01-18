using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Infrastructure;

public interface IProvinceRepository
{
    Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId);
}