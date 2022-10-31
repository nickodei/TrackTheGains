namespace TrackTheGains.WebApi.WorkoutSessions.DTOs;

public class SetVm
{
    public Guid? Id { get; set;}
    public int Order { get; set;}
    public List<RepetitionVm> Repetitions { get; set;} = new ();
}