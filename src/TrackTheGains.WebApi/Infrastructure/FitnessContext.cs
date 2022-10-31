using Microsoft.EntityFrameworkCore;
using TrackTheGains.WebApi.Infrastructure.EntityConfigurations;
using TrackTheGains.WebApi.Workouts;
using TrackTheGains.WebApi.WorkoutSessions;

namespace TrackTheGains.WebApi.Infrastructure;

public class FitnessContext : DbContext
{
    public const string DEFAULT_SCHEMA = "fitness";

    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Exercise> Exercises => Set<Exercise>();

    public DbSet<Set> Sets => Set<Set>();
    public DbSet<Repetition> Repetitions => Set<Repetition>();
    public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();
    public DbSet<FinishedExercise> FinishedExercises => Set<FinishedExercise>();

    public FitnessContext(DbContextOptions<FitnessContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WorkoutEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseEntityTypeConfiguration());
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        // Work with deleted Flag
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                //case EntityState.Added:
                //    entry.CurrentValues["isDeleted"] = false;
                //    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["isDeleted"] = true;
                    break;
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
