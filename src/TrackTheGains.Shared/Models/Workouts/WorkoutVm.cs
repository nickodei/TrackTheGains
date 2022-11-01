namespace TrackTheGains.Shared.Models.Workouts;

public class WorkoutVm
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public List<ExerciseVm> Exercises { get; set; } = new();
}
