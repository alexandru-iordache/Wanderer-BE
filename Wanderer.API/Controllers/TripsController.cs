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
    private readonly IHttpContextService httpContextService;

    public TripsController(ITripService tripService, IHttpContextService httpContextService)
    {
        this.tripService = tripService;
        this.httpContextService = httpContextService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips(FilterOptionsDto filterOptionsDto)
    {
        var userId = httpContextService.GetUserId();
        return Ok(await tripService.GetUserTrips(userId, filterOptionsDto));
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

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteTrip(Guid id)
    {
        return Ok(await tripService.CompleteTrip(id));
    }

    [HttpPost("{id}/publish")]
    public async Task<IActionResult> PublishTrip(Guid id)
    {
        return Ok(await tripService.PublishTrip(id));
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
