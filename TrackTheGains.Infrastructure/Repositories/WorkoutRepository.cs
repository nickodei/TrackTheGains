using Microsoft.EntityFrameworkCore;
using TrackTheGains.Domain.Models;

namespace TrackTheGains.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly FitnessContext _context;

        public WorkoutRepository(FitnessContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Workout workout)
        {
            _context.Add(workout);
            await _context.SaveChangesAsync();
            return workout.Id;
        }

        public async Task Delete(Guid workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);
            if(workout is not null)
            {
                _context.Remove(workout);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Workout> GetDetailedAsync(Guid id)
        {
            var workout = await _context.Workouts
                .Include(x => x.Excercises)
                .FirstOrDefaultAsync(x => x.Id == id);

            return workout;
        }

        public async Task<IEnumerable<Workout>> GetAllAsync()
        {
            var workouts = await _context.Workouts.ToListAsync();
            return workouts;
        }

        public void Update(Workout workout)
        {
            _context.Entry(workout).State = EntityState.Modified;
        }
    }
}
