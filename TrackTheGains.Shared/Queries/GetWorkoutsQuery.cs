using MediatR;

namespace TrackTheGains.Shared.Queries
{
    public class GetWorkoutsQuery : IRequest<IEnumerable<WorkoutOverviewVM>>
    {
    }
}
