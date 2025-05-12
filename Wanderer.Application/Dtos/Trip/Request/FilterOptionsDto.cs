using Microsoft.AspNetCore.Mvc;

namespace Wanderer.Application.Dtos.Trip.Request;

public class FilterOptionsDto
{
    [FromQuery(Name = "isOrderedByDate")]
    public bool IsOrderedByDate { get; set; }

    [FromQuery(Name = "status")]
    public string Status { get; set; }

    [FromQuery(Name = "minDate")]
    public DateOnly? MinDate { get; set; }

    [FromQuery(Name = "maxDate")]
    public DateOnly? MaxDate { get; set; }
}
