using AngularWebApiTestTask.Server.Contracts;
using AngularWebApiTestTask.Server.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiTestTask.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CountriesController(ICountryRepository countryRepository) : ControllerBase
{
    /// <summary>
    /// Gets all countries with included provinces
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of countries with their provinces</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CountriesResponse>> GetCountries(CancellationToken cancellationToken)
    {
        var countries = await countryRepository.GetAllCountriesAsync(cancellationToken);
        return Ok(new CountriesResponse(countries));
    }
}