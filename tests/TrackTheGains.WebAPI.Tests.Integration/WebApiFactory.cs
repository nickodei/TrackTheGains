using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackTheGains.WebApi.Infrastructure;
using TrackTheGains.WebApi.Models.Workout;

namespace TrackTheGains.WebAPI.Tests.Integration
{
    public class WebApiFactory : WebApplicationFactory<Program>
    {
        private readonly TestcontainerDatabase testcontainers = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "TrackTheGains",
                Username = "postgres",
                Password = "postgres",
            })
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
                services.AddDbContext<FitnessContext>(
                    opt => opt.UseNpgsql(testcontainers.ConnectionString))
            );
        }

        public string ConnectionString => testcontainers.ConnectionString;

        public void CreateNewDatabase()
        {
            using var scope = Services.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<FitnessContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }


        public async Task CreateWorkouts(List<Workout> workouts)
        {
            using var scope = Services.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<FitnessContext>();

            if(context is not null)
            {
                context.Workouts.AddRange(workouts);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateWorkout(string name, int availableExercises, int deletedExercises)
        {
            using var scope = Services.GetService<IServiceScopeFactory>()?.CreateScope();
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

        public async Task InitializeAsync()
        {
            await testcontainers.StartAsync();
        }

        public async new Task DisposeAsync()
        {
            await testcontainers.DisposeAsync();
        }
    }
}
