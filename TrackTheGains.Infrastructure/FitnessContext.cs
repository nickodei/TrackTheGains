using Microsoft.EntityFrameworkCore;
using TrackTheGains.Domain.Models;
using TrackTheGains.Infrastructure.EntityConfigurations;

namespace TrackTheGains.Infrastructure
{
    public class FitnessContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "fitness";

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Excercise> Excercises { get; set; }

        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkoutEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExcerciseEntityTypeConfiguration());
        }
    }
}
