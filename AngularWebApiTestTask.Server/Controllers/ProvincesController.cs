using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiTestTask.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProvincesController(IProvinceRepository provinceRepository): ControllerBase
{
    /// <summary>
    /// Gets all provinces
    /// </summary>
    /// <returns>A list of provinces</returns>
    [HttpGet("{countryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Province>>> GetProvinces(int countryId)
    {
        var result = await provinceRepository.GetProvincesByCountryIdAsync(countryId);

        return Ok(result);
    }
}