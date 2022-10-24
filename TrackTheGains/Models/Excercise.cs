using TrackTheGains.Domain.Common;

namespace TrackTheGains.Domain.Models
{
    public class Excercise : AuditableEntity
    {
        public string? Name { get; set; }
        public int OrderNr { get; set; }
        public Workout Workout { get; set; } = null!;
    }
}
