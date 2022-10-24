namespace TrackTheGains.Shared.Queries
{
    public record WorkoutOverviewVM
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int ExcerciseAmount { get; init; }
    }

    public record WorkoutVM
    {
        public Guid Id { get; init; }
        public string Name { get; init; }        
        public List<ExcerciseVM> Excercises { get; set; }
    }

    public record ExcerciseVM
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int OrderNr { get; init; }
    }
}
