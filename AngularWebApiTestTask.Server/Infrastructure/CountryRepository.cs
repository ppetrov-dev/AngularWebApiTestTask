using AngularWebApiTestTask.Server.Database;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApiTestTask.Server.Infrastructure;

internal class CountryRepository(ApplicationDbContext context) : ICountryRepository
{
    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        return await context.Countries
            .Include(country => country.Provinces)
            .ToListAsync();
    }
}