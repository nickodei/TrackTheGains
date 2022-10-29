namespace TrackTheGains.WebApi.Models.Workout
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<Exercise> Exercises { get; set; } = new();
    }
}