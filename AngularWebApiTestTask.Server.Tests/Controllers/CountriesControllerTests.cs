using AngularWebApiTestTask.Server.Controllers;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Infrastructure;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Controllers;

public class CountriesControllerTests
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
        _countryRepositoryMock.Setup(repository => repository.GetAllCountriesAsync())
            .ReturnsAsync([]);

        var getCountriesResult = await _controller.GetCountries();

        getCountriesResult.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<Country>>()
            .Which.Should().BeEmpty();
    }

    [Fact]
    public async Task OkResult_WithCountries()
    {
        var expectedCountries = new List<Country>
        {
            CountryBuilder.Any(), CountryBuilder.Any()
        };
        _countryRepositoryMock.Setup(repository => repository.GetAllCountriesAsync())
            .ReturnsAsync(expectedCountries);

        var getCountriesResult = await _controller.GetCountries();

        getCountriesResult.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(expectedCountries);
    }
}