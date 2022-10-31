namespace TrackTheGains.WebApi.WorkoutSessions;

public class Repetition
{
    public Guid Id {get; set;}

    public int Order {get; set;}
    public int WeightsInKg {get; set;}
}