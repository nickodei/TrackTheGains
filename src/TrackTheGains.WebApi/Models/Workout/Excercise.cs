namespace TrackTheGains.WebApi.Models.Workout
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public int OrderNr { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }

        public Workout Workout { get; set; } = null!;
    }
}
