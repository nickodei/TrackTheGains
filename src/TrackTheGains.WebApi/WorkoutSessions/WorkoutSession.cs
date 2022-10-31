using TrackTheGains.WebApi.Workouts;

namespace TrackTheGains.WebApi.WorkoutSessions;

public class WorkoutSession
{
    public Guid Id {get; set;}
    public DateTime StartingTime {get; set;}
    public DateTime EndingTime {get; set;}

    public Guid WorkoutId {get; set;}
    public Workout? Workout {get; set;}

    public List<FinishedExercise> FinishedExercises {get; set;} = new();
}