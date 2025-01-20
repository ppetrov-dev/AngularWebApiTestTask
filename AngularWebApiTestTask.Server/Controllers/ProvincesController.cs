using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiTestTask.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ProvincesController(IProvinceRepository provinceRepository): ControllerBase
{
    /// <summary>
    /// Gets all provinces
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of provinces</returns>
    [HttpGet("{countryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProvincesResponse>> GetProvinces(int countryId, CancellationToken cancellationToken)
    {
        var provinces = await provinceRepository.GetProvincesByCountryIdAsync(countryId, cancellationToken);
        return Ok(new ProvincesResponse(provinces));
    }
}