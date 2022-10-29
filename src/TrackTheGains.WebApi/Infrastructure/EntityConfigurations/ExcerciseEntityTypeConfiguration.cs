using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackTheGains.WebApi.Models.Workout;

namespace TrackTheGains.WebApi.Infrastructure.EntityConfigurations
{
    internal class ExerciseEntityTypeConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> exerciseConfiguration)
        {
            exerciseConfiguration.ToTable("exercises", FitnessContext.DEFAULT_SCHEMA);

            exerciseConfiguration.HasKey(e => e.Id);
            exerciseConfiguration.HasQueryFilter(e => !EF.Property<bool>(e, "IsDeleted"));
        }
    }
}
