namespace TrackTheGains.WebApi.WorkoutSessions;

public interface IWorkoutSessionService
{
    Task<Guid> CreateWorkoutSessionAsync(WorkoutSessionCreateDTO request);
}