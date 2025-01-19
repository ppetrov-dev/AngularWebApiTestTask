using AngularWebApiTestTask.Server.Controllers;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using AngularWebApiTestTask.Server.Infrastructure;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Controllers;

public class ProvincesControllerTests
{
    private readonly Mock<IProvinceRepository> _provinceRepositoryMock;
    private readonly ProvincesController _controller;

    public ProvincesControllerTests()
    {
        _provinceRepositoryMock = new Mock<IProvinceRepository>();
        _controller = new ProvincesController(_provinceRepositoryMock.Object);
    }

    [Fact]
    public async Task OkResult_WithEmptyList_WhenNoProvinces()
    {
        _provinceRepositoryMock.Setup(repository => repository.GetProvincesByCountryIdAsync(It.IsAny<int>()))
            .ReturnsAsync([]);

        var result = await _controller.GetProvinces(1);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<Province>>()
            .Which.Should().BeEmpty();
    }

    [Fact]
    public async Task OkResult_WithListOfProvinces()
    {
        const int countryId = 1;
        var provinces = new List<Province>
        {
            ProvinceBuilder.Any(), ProvinceBuilder.Any()
        };
        _provinceRepositoryMock.Setup(repository => repository.GetProvincesByCountryIdAsync(countryId))
            .ReturnsAsync(provinces);

        var result = await _controller.GetProvinces(countryId);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(provinces);
    }
}