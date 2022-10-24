using MediatR;
using Microsoft.EntityFrameworkCore;
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
            return await (
                from workout in _context.Workouts
                join exercise in _context.Excercises on workout.Id equals exercise.Workout.Id into st
                from x in st.DefaultIfEmpty()
                group workout by new { Id = workout.Id, Name = workout.Name } into g
                select new WorkoutOverviewVM()
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    ExcerciseAmount = g.Count(x => x.Excercises.Count > 0)
                }).ToListAsync();
        }
    }
}
