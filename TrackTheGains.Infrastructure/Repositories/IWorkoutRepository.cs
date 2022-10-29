using TrackTheGains.Domain.Models;

namespace TrackTheGains.Infrastructure.Repositories
{
    public interface IWorkoutRepository
    {
        Task<Guid> AddAsync(Workout workout);
        Task Delete(Guid workoutId);
        Task<Workout> GetDetailedAsync(Guid id);
        Task<IEnumerable<Workout>> GetAllAsync();
        void Update(Workout workout);
    }
}