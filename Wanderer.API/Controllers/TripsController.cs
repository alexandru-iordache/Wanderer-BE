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
    private readonly ITripService tripService;

    public TripsController(ITripService tripService)
    {
        this.tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips(FilterOptionsDto filterOptionsDto)
    {
        return Ok(await tripService.Get(filterOptionsDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTripById(Guid id)
    {
        return Ok(await tripService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostTrip([FromBody] AddTripDto addTripDto)
    {
        return Created(nameof(GetTrips), await tripService.InsertTrip(addTripDto));
    }

    [HttpPost("{id}/status")]
    public async Task<IActionResult> ChangeTripsStatus(Guid id, [FromBody] ChangeTripStatusDto changeTripStatusDto)
    {
        var trip = await tripService.ChangeTripStatus(id, changeTripStatusDto);
        return Ok(trip);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrip(Guid id, [FromBody] TripDto tripDto)
    {
        var trip = await tripService.UpdateTrip(id, tripDto);

        return Ok(trip);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(Guid id)
    {
        await tripService.DeleteTrip(id);
        return NoContent();
    }
}
