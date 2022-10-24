using TrackTheGains.Domain.Common;

namespace TrackTheGains.Domain.Models
{
    public class Workout : AuditableEntity
    {
        public string? Name { get; set; }
        public IList<Excercise> Excercises { get; private set; } = new List<Excercise>();
    }
}
