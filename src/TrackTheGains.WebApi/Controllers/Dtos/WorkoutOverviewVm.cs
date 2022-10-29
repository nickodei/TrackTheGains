namespace TrackTheGains.WebApi.Controllers.Dtos
{
    public class WorkoutOverviewVm
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int ExerciseAmount { get; set; }
    }
}
