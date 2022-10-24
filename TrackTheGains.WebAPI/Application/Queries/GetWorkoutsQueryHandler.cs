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
            return await _context.Workouts
                .Include(x => x.Excercises)
                .GroupBy(x => new { Id = x.Id, Name = x.Name })
                .Select(x => new WorkoutOverviewVM()
                {
                    Id = x.First().Id,
                    Name = x.First().Name,
                    ExcerciseAmount = x.Count()
                }).ToListAsync();
        }
    }
}
