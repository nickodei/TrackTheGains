﻿namespace TrackTheGains.WebApi.Controllers.Dtos;

public class ExerciseVm
{
    public Guid? Id { get; set; }
    public int OrderNr { get; set; }
    public string? Name { get; set; }
}
