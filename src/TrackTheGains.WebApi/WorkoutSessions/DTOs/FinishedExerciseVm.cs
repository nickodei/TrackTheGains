namespace TrackTheGains.WebApi.WorkoutSessions.DTOs;

public class FinishedExerciseVm
{
    public Guid? Id {get; set;}
    public Guid ExerciseId {get; set;}

    public List<SetVm> Sets { get; set;} = new();
}