using AngularWebApiTestTask.Server.Database;
using AngularWebApiTestTask.Server.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApiTestTask.Server.Infrastructure;

internal class ProvinceRepository(ApplicationDbContext context) : IProvinceRepository
{
    public async Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId)
    {
        return await context.Provinces
            .Where(p => p.CountryId == countryId)
            .ToListAsync();
    }
}