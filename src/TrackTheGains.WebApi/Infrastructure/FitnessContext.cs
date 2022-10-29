using Microsoft.EntityFrameworkCore;
using TrackTheGains.WebApi.Infrastructure.EntityConfigurations;
using TrackTheGains.WebApi.Models.Workout;

namespace TrackTheGains.WebApi.Infrastructure;

public class FitnessContext : DbContext
{
    public const string DEFAULT_SCHEMA = "fitness";

    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Exercise> Exercises => Set<Exercise>();

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
