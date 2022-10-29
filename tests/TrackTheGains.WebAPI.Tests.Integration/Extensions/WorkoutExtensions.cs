using Microsoft.Extensions.DependencyInjection;
using TrackTheGains.WebApi.Infrastructure;
using TrackTheGains.WebApi.Models.Workout;

namespace TrackTheGains.WebAPI.Tests.Integration.Extensions
{
    public static class WorkoutExtensions
    {
        public static async Task CreateWorkouts(this WebApiFactory factory, List<Workout> workouts)
        {
            using var scope = factory.Services.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<FitnessContext>();

            if (context is not null)
            {
                context.Workouts.AddRange(workouts);
                await context.SaveChangesAsync();
            }
        }

        public static async Task CreateWorkout(this WebApiFactory factory, string name, int availableExercises, int deletedExercises)
        {
            using var scope = factory.Services.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<FitnessContext>();

            var workout = new Workout() { Name = name };
            workout.Exercises.AddRange(Enumerable.Range(0, availableExercises).Select(_ => new Exercise()
            {
                Name = Guid.NewGuid().ToString(),
                IsDeleted = false
            }));
            workout.Exercises.AddRange(Enumerable.Range(0, deletedExercises).Select(_ => new Exercise()
            {
                Name = Guid.NewGuid().ToString(),
                IsDeleted = true
            }));

            context?.Workouts.Add(workout);
            await context?.SaveChangesAsync();
        }
    }
}
