using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Controllers;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Controllers;

public class CountriesControllerTests : ControllerTestsBase
{
    private readonly Mock<ICountryRepository> _countryRepositoryMock;
    private readonly CountriesController _controller;

    public CountriesControllerTests()
    {
        _countryRepositoryMock = new Mock<ICountryRepository>();
        _controller = new CountriesController(_countryRepositoryMock.Object);
    }

    [Fact]
    public async Task OkResult_WithEmptyList_WhenNoCountriesExist()
    {
        _countryRepositoryMock.Setup(repository => repository.GetAllCountriesAsync(CancellationTokenSource.Token))
            .ReturnsAsync([]);

        var getCountriesResult = await _controller.GetCountries(CancellationTokenSource.Token);

        getCountriesResult.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<CountriesResponse>()
            .Which.Countries.Should().BeEmpty();
    }

    [Fact]
    public async Task OkResult_WithCountries()
    {
        var expectedCountries = new List<Country>
        {
            CountryBuilder.Any(), CountryBuilder.Any()
        };
        _countryRepositoryMock.Setup(repository => repository.GetAllCountriesAsync(CancellationTokenSource.Token))
            .ReturnsAsync(expectedCountries);

        var getCountriesResult = await _controller.GetCountries(CancellationTokenSource.Token);

        getCountriesResult.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<CountriesResponse>()
            .Which.Countries.Should().Equal(expectedCountries, ReferenceEquals);
    }
}