using TrackTheGains.Shared.Models.WorkoutsSessions;
using TrackTheGains.WebApi.Infrastructure;

namespace TrackTheGains.WebApi.WorkoutSessions;

public class WorkoutSessionService : IWorkoutSessionService
{
    private readonly FitnessContext _context;

    public WorkoutSessionService(FitnessContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateWorkoutSessionAsync(WorkoutSessionCreateDTO request)
    {
        var workoutSession = new WorkoutSession()
        {
            WorkoutId = request.WorkoutId,
            EndingTime = request.EndingTime,
            StartingTime = request.StartingTime,
            FinishedExercises = request.FinishedExercises.Select(ex => new FinishedExercise()
            {
                ExerciseId = ex.ExerciseId,
                Sets = ex.Sets.Select(set => new Set()
                {
                    Order = set.Order,
                    Repetitions = set.Repetitions.Select(rep => new Repetition()
                    {
                        Order = rep.Order,
                        WeightsInKg = rep.WeightsInKg
                    }).ToList()
                }).ToList()
            }).ToList()
        };

        _context.WorkoutSessions.Add(workoutSession);
        await _context.SaveChangesAsync();

        return workoutSession.Id;
    }
}