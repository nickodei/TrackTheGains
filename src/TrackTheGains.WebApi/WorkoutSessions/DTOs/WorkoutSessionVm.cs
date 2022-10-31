namespace TrackTheGains.WebApi.WorkoutSessions.DTOs;

public class WorkoutSessionVm
{
    public Guid? Id { get; set; }
    public DateTime StartingTime {get; set;}
    public DateTime EndingTime {get; set;}

    public Guid WorkoutId {get; set;}
}