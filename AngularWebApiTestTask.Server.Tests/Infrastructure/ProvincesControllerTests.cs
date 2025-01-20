using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Infrastructure;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;

namespace AngularWebApiTestTask.Server.Tests.Infrastructure;

public class ProvinceRepositoryTests : RepositoryBaseTests
{
    private readonly ProvinceRepository _repository;

    public ProvinceRepositoryTests()
    {
        _repository = new ProvinceRepository(Context);
    }

    [Fact]
    public async Task EmptyList_WhenNoProvincesExist()
    {
        var provinces = await _repository.GetProvincesByCountryIdAsync(1, CancellationTokenSource.Token);

        provinces.Should().BeEmpty();
    }

    [Fact]
    public async Task AllProvinces_AsAreFromDbContext_WhenProvincesWithCountryIdExist()
    {
        const int countryId = 1;
        var expectedProvinces = new List<Province>
        {
            new ProvinceBuilder { Id = 1, Name = "Province 1", CountryId = countryId }.Build(),
            new ProvinceBuilder { Id = 2, Name = "Province 2", CountryId = countryId }.Build()
        };
        await Context.Provinces.AddRangeAsync(expectedProvinces);
        await Context.SaveChangesAsync();

        var actualProvinces = await _repository.GetProvincesByCountryIdAsync(countryId, CancellationTokenSource.Token);

        actualProvinces.Should().Equal(expectedProvinces, ReferenceEquals);
    }

    [Fact]
    public async Task EmptyList_WhenCountryIdDoesNotMatch()
    {
        const int countryId = 1;
        await Context.Provinces.AddRangeAsync(new List<Province>
        {
            new ProvinceBuilder { Id = 1, Name = "Province 1", CountryId = countryId }.Build(),
            new ProvinceBuilder { Id = 2, Name = "Province 2", CountryId = countryId }.Build()
        });
        await Context.SaveChangesAsync();
        const int countryIdIsNotInDatabase = 20;

        var actualProvinces = await _repository.GetProvincesByCountryIdAsync(countryIdIsNotInDatabase, CancellationTokenSource.Token);

        actualProvinces.Should().BeEmpty();
    }
}