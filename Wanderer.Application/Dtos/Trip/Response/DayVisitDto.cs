﻿using Wanderer.Application.Dtos.Trip.Request;

namespace Wanderer.Application.Dtos.Trip.Response;

public class DayVisitDto
{
    public Guid Id { get; set; }

    public required DateTime Date { get; set; }

    public IEnumerable<WaypointVisitDto> WaypointVisits { get; set; } = [];
}
