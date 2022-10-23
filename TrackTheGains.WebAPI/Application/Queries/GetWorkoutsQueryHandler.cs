using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackTheGains.Domain.Models;
using TrackTheGains.Infrastructure;
using TrackTheGains.Shared.Queries;

namespace TrackTheGains.WebAPI.Application.Queries
{
    public class GetWorkoutsQueryHandler : IRequestHandler<GetWorkoutsQuery, IEnumerable<WorkoutOverviewVM>>
    {
        private readonly FitnessContext _context;
        public GetWorkoutsQueryHandler(FitnessContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutOverviewVM>> Handle(GetWorkoutsQuery request, CancellationToken cancellationToken)
        {
            var workouts = await _context.Workouts.ToListAsync();
            return workouts.Select(workout => new WorkoutOverviewVM()
            {
                Id = workout.Id,
                Name = workout.Name
            }).ToList();
        }
    }
}
