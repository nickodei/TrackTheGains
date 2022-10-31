namespace TrackTheGains.WebApi.Workouts;

public class Exercise
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string? Name { get; set; }
    public bool IsDeleted { get; set; }

    public Workout Workout { get; set; } = null!;
}
