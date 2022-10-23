using MediatR;

namespace TrackTheGains.Shared.Queries
{
    public class GetFullWorkoutQuery : IRequest<WorkoutVM>
    {
        public Guid Id { get; set; }
    }
}
