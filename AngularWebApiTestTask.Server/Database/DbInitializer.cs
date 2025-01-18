using AngularWebApiTestTask.Server.Database.Models;

namespace AngularWebApiTestTask.Server.Database;

internal static class DbInitializer
{
    public static void InitializeIfNeeded(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Countries.Any())
            return;

        var countries = new[]
        {
            new Country { Name = "Russia" },
            new Country { Name = "Not Russia" }
        };

        context.Countries.AddRange(countries);
        context.SaveChanges();

        var provinces = new[]
        {
            new Province { Name = "Moscow", CountryId = countries[0].Id },
            new Province { Name = "Nizhnii Novgorod", CountryId = countries[0].Id },
            new Province { Name = "New-York", CountryId = countries[1].Id },
            new Province { Name = "Alabama", CountryId = countries[1].Id }
        };

        context.Provinces.AddRange(provinces);
        context.SaveChanges();
    }
}