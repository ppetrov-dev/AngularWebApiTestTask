using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Controllers;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using AngularWebApiTestTask.Server.Tests.Database.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AngularWebApiTestTask.Server.Tests.Controllers;

public class ProvincesControllerTests: ControllerTestsBase
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
        _provinceRepositoryMock.Setup(repository => repository.GetProvincesByCountryIdAsync(It.IsAny<int>(), CancellationTokenSource.Token))
            .ReturnsAsync([]);

        var result = await _controller.GetProvinces(1, CancellationTokenSource.Token);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<ProvincesResponse>()
            .Which.Provinces.Should().BeEmpty();
    }

    [Fact]
    public async Task OkResult_WithListOfProvinces()
    {
        const int countryId = 1;
        var provinces = new List<Province>
        {
            ProvinceBuilder.Any(), ProvinceBuilder.Any()
        };
        _provinceRepositoryMock.Setup(repository => repository.GetProvincesByCountryIdAsync(countryId, CancellationTokenSource.Token))
            .ReturnsAsync(provinces);

        var result = await _controller.GetProvinces(countryId, CancellationTokenSource.Token);

        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<ProvincesResponse>()
            .Which.Provinces.Should().Equal(provinces, ReferenceEquals);
    }
}