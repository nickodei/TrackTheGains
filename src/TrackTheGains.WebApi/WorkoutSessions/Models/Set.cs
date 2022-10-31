namespace TrackTheGains.WebApi.WorkoutSessions;

public class Set
{
    public Guid Id {get; set;}

    public int Order {get; set;}
    public List<Repetition> Repetitions {get; set;} = new();
}