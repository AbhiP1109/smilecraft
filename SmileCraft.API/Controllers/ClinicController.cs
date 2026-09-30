using Microsoft.AspNetCore.Mvc;
using SmileCraft.API.Models;
using SmileCraft.API.Services;

namespace SmileCraft.API.Controllers;

/// <summary>
/// Controller for clinic-related data endpoints.
/// </summary>
[ApiController]
public class ClinicController : ControllerBase
{
    private readonly ClinicDataService _dataService;

    public ClinicController()
    {
        _dataService = ClinicDataService.Instance;
    }

    /// <summary>
    /// Gets the clinic's general information.
    /// </summary>
    [HttpGet("api/clinicinfo")]
    [ProducesResponseType(typeof(ClinicInfo), StatusCodes.Status200OK)]
    public ActionResult<ClinicInfo> GetClinicInfo()
    {
        return Ok(_dataService.GetClinicInfo());
    }

    /// <summary>
    /// Gets all dental services offered by the clinic.
    /// </summary>
    [HttpGet("api/services")]
    [ProducesResponseType(typeof(List<Service>), StatusCodes.Status200OK)]
    public ActionResult<List<Service>> GetAllServices()
    {
        return Ok(_dataService.GetAllServices());
    }

    /// <summary>
    /// Gets all doctors at the clinic.
    /// </summary>
    [HttpGet("api/doctors")]
    [ProducesResponseType(typeof(List<Doctor>), StatusCodes.Status200OK)]
    public ActionResult<List<Doctor>> GetAllDoctors()
    {
        return Ok(_dataService.GetAllDoctors());
    }

    /// <summary>
    /// Gets clinic statistics.
    /// </summary>
    [HttpGet("api/stats")]
    [ProducesResponseType(typeof(List<Stat>), StatusCodes.Status200OK)]
    public ActionResult<List<Stat>> GetAllStats()
    {
        return Ok(_dataService.GetAllStats());
    }

    /// <summary>
    /// Gets all patient testimonials.
    /// </summary>
    [HttpGet("api/testimonials")]
    [ProducesResponseType(typeof(List<Testimonial>), StatusCodes.Status200OK)]
    public ActionResult<List<Testimonial>> GetAllTestimonials()
    {
        return Ok(_dataService.GetAllTestimonials());
    }
}
