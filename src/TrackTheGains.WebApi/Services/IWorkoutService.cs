using TrackTheGains.WebApi.Controllers.Dtos;
using TrackTheGains.WebApi.Workouts;

namespace TrackTheGains.WebApi.Services
{
    public interface IWorkoutService
    {
        Task CreateWorkoutAsync(WorkoutVm workoutVm);
        Task<Workout?> DeleteWorkoutAsync(Guid workoutId);
        Task<WorkoutVm?> GetWorkoutByIdAsync(Guid workoutId);
        Task<IEnumerable<WorkoutOverviewVm>> GetWorkoutOverviewsAsync();
        Task<Workout?> UpdateWorkoutAsync(WorkoutVm workoutVm);
    }
}