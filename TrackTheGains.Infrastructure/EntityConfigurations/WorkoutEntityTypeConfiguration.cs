using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackTheGains.Domain.Models;

namespace TrackTheGains.Infrastructure.EntityConfigurations
{
    internal class WorkoutEntityTypeConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> workoutConfiguration)
        {
            workoutConfiguration.ToTable("workouts", FitnessContext.DEFAULT_SCHEMA);

            workoutConfiguration.HasKey(w => w.Id);
        }
    }
}
