using Microsoft.AspNetCore.Mvc;

namespace Wanderer.Application.Dtos.Trip.Request;

public class FilterOptionsDto
{
    [FromQuery(Name = "isOrderedByDate")]
    public bool IsOrderedByDate { get; set; }

    [FromQuery(Name = "status")]
    public string Status { get; set; }

    [FromQuery(Name = "minDate")]
    public DateTime? MinDate { get; set; }

    [FromQuery(Name = "maxDate")]
    public DateTime? MaxDate { get; set; }

    [FromQuery(Name = "isPublished")]
    public bool? IsPublished { get; set; }
}
