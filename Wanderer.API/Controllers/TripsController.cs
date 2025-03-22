using Microsoft.AspNetCore.Mvc;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Services.Interfaces;

namespace Wanderer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        return Ok(await _tripService.Get());
    }

    [HttpPost]
    public async Task <IActionResult> PostTrip([FromBody] AddTripDto addTripDto)
    {
        return Created(nameof(GetTrips), await _tripService.InsertTrip(addTripDto));
    }
}
