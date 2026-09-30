using Microsoft.AspNetCore.Mvc;

namespace SmileCraft.API.Controllers;

/// <summary>
/// Controller for health check endpoint.
/// </summary>
[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Checks the health of the API.
    /// </summary>
    /// <returns>A simple health status string.</returns>
    /// <response code="200">Returns "Healthy" if the API is running.</response>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public ActionResult<string> GetHealth()
    {
        return Ok("Healthy");
    }
}
