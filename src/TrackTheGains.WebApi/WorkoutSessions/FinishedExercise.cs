using TrackTheGains.WebApi.Workouts;

namespace TrackTheGains.WebApi.WorkoutSessions;

public class FinishedExercise
{
    public Guid Id { get; set;}

    public Guid ExerciseId { get; set;}
    public Exercise? Exercises { get; set;}

    public List<Set> Sets { get; set;} = new ();
}