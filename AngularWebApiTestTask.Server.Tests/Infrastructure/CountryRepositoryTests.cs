using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Infrastructure;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;

namespace AngularWebApiTestTask.Server.Tests.Infrastructure;

public class CountryRepositoryTests : RepositoryBaseTests
{
    private readonly CountryRepository _repository;

    public CountryRepositoryTests()
    {
        _repository = new CountryRepository(Context);
    }

    [Fact]
    public async Task EmptyList_WhenNoCountriesExist()
    {
        var countries = await _repository.GetAllCountriesAsync(CancellationTokenSource.Token);

        countries.Should().BeEmpty();
    }

    [Fact]
    public async Task AllCountries_AsAreFromDbContext()
    {
        var expectedCountries = new List<Country>
        {
            CountryBuilder.Any(),CountryBuilder.Any(), CountryBuilder.Any()
        };
        await Context.Countries.AddRangeAsync(expectedCountries);
        await Context.SaveChangesAsync();

        var actualCountries = await _repository.GetAllCountriesAsync(CancellationTokenSource.Token);

        actualCountries.Should().Equal(expectedCountries, ReferenceEquals);
    }
}