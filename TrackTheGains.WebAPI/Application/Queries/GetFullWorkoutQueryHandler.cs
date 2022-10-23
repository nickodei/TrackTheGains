using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackTheGains.Infrastructure;
using TrackTheGains.Shared.Queries;

namespace TrackTheGains.WebAPI.Application.Queries
{
    public class GetFullWorkoutQueryHandler : IRequestHandler<GetFullWorkoutQuery, WorkoutVM>
    {
        private readonly FitnessContext _context;
        public GetFullWorkoutQueryHandler(FitnessContext context)
        {
            _context = context;
        }

        public async Task<WorkoutVM> Handle(GetFullWorkoutQuery request, CancellationToken cancellationToken)
        {
            var workout = await _context.Workouts
                .Include(x => x.Excercises)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if(workout is null)
            {
                return null;
            }

            return workout.Adapt<WorkoutVM>();
        }
    }
}
