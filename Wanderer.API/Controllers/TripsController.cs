using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Application.Services;

namespace Wanderer.API.Controllers;

[Authorize]
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTripById(Guid id)
    {
        return Ok(await _tripService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostTrip([FromBody] AddTripDto addTripDto)
    {
        return Created(nameof(GetTrips), await _tripService.InsertTrip(addTripDto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrip(Guid id, [FromBody] TripDto tripDto)
    {
        var trip = await _tripService.UpdateTrip(id, tripDto);

        return Ok(trip);
    }
}
