using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiTestTask.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CountriesController(ICountryRepository countryRepository) : ControllerBase
{
    /// <summary>
    /// Gets all countries with included provinces
    /// </summary>
    /// <returns>A list of countries with their provinces</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        var result = await countryRepository.GetAllCountriesAsync();
        return Ok(result);
    }
}