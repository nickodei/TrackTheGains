namespace TrackTheGains.Domain.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Excercise> Excercises { get; set; }

        public Workout()
        {
            Excercises = new List<Excercise>();
        }
    }
}
