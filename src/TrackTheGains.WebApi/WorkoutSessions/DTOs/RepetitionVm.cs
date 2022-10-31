namespace TrackTheGains.WebApi.WorkoutSessions.DTOs;

public class RepetitionVm
{
    public Guid? Id {get; set;}
    public int Order {get; set;}
    public int WeightsInKg {get; set;}
}