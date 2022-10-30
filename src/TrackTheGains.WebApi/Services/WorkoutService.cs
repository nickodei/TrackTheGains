using Microsoft.EntityFrameworkCore;
using TrackTheGains.WebApi.Controllers.Dtos;
using TrackTheGains.WebApi.Infrastructure;
using TrackTheGains.WebApi.Models.Workout;

namespace TrackTheGains.WebApi.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly FitnessContext _context;

        public WorkoutService(FitnessContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutOverviewVm>> GetWorkoutOverviewsAsync()
        {
            return await (
                from workout in _context.Workouts
                join exercise in _context.Exercises on workout.Id equals exercise.Workout.Id into st
                from x in st.DefaultIfEmpty()
                group workout by new { workout.Id, workout.Name } into g
                select new WorkoutOverviewVm()
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    ExerciseAmount = g.Count(x => x.Exercises.Count > 0)
                }).ToListAsync();
        }

        public async Task<WorkoutVm?> GetWorkoutByIdAsync(Guid workoutId)
        {
            return await _context.Workouts
                .Include(x => x.Exercises)
                .Select(w => new WorkoutVm()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Exercises = w.Exercises.Select(e => new ExerciseVm()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        OrderNr = e.OrderNr
                    }).ToList()
                })
                .SingleOrDefaultAsync(x => x.Id == workoutId);
        }

        public async Task CreateWorkoutAsync(WorkoutVm workoutVm)
        {
            var workout = new Workout()
            {
                Name = workoutVm.Name,
                Exercises = workoutVm.Exercises.Select(e => new Exercise()
                {
                    Name = e.Name,
                    OrderNr = e.OrderNr,
                }).ToList()
            };

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
        }

        public async Task<Workout?> UpdateWorkoutAsync(WorkoutVm workoutVm)
        {
            var workout = await _context.Workouts.Include(x => x.Exercises).SingleOrDefaultAsync(x => x.Id == workoutVm.Id);
            if (workout is null)
            {
                return null;
            }

            workout.Name = workoutVm.Name;
            workout.Exercises = workoutVm.Exercises.Select(e => new Exercise()
            {
                Id = e.Id.GetValueOrDefault(),
                Name = e.Name,
                OrderNr = e.OrderNr,
                IsDeleted = false,
                Workout = workout
            }).ToList();

            _context.Entry(workout).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return workout;
        }

        public async Task<Workout?> DeleteWorkoutAsync(Guid workoutId)
        {
            var workout = await _context.Workouts.SingleOrDefaultAsync(x => x.Id == workoutId);
            if (workout is null)
            {
                return null;
            }

            workout.IsDeleted = true;
            await _context.SaveChangesAsync();

            return workout;
        }
    }
}
