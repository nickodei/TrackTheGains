using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TrackTheGains.WebApi.Controllers.Dtos;
using TrackTheGains.WebApi.Models.Workout;
using TrackTheGains.WebAPI.Tests.Integration;
using TrackTheGains.WebAPI.Tests.Integration.Extensions;
using Xunit;

namespace Workout_Spec
{
    public class WorkoutControllerTests: IClassFixture<WebApiFactory>
    {
        private readonly WebApiFactory factory;

        public WorkoutControllerTests(WebApiFactory factory)
        {
            this.factory = factory;
        }

        #region Get WorkoutOverviews Request

        [Fact]
        public async Task GetWorkoutOverviews_ReturnsNoWorkouts_WhenNonExist()
        {
            await factory.EnsureCleanDatabase();

            var response = await factory.CreateClient().GetAsync("api/workouts");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
            workouts.Should().NotBeNull();
            workouts.Count().Should().Be(0);
        }

        [Fact]
        public async Task GetWorkoutOverviews_ReturnsWorkoutWithoutExercises_WhenNoExercisesWhereCreated()
        {
            await factory.EnsureCleanDatabase();
            await factory.CreateWorkouts(new() { new Workout() { Name = "push" } });

            var response = await factory.CreateClient().GetAsync("api/workouts");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
            workouts.Should().NotBeNull();
            workouts.Count().Should().Be(1);
            workouts.First().Name.Should().Be("push");
            workouts.First().ExerciseAmount.Should().Be(0);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 0)]
        [InlineData(0, 7)]
        [InlineData(2, 8)]
        public async Task GetWorkoutOverviews_ReturnsNonDeletedExercisesInWorkout_WhenBothExistInObject(int availableExercises, int deletedExercises)
        {
            await factory.EnsureCleanDatabase();
            await factory.CreateWorkout("testWorkout", availableExercises, deletedExercises);

            var response = await factory.CreateClient().GetAsync("api/workouts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
            workouts.Should().NotBeNull();
            workouts?.Count().Should().Be(1);
            workouts?.First().Name.Should().Be("testWorkout");
            workouts?.First().ExerciseAmount.Should().Be(availableExercises);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 0)]
        [InlineData(0, 7)]
        [InlineData(2, 8)]
        public async Task GetWorkoutOverviews_ReturnsNonDeletedWorkouts_WhenBothExist(int availableWorkouts, int deletedWorkouts)
        {
            await factory.EnsureCleanDatabase();
            await factory.CreateWorkouts(Enumerable.Range(0, availableWorkouts).Select(_ => new Workout()
            {
                Name = Guid.NewGuid().ToString(),
                IsDeleted = false
            }).ToList());

            await factory.CreateWorkouts(Enumerable.Range(0, deletedWorkouts).Select(_ => new Workout()
            {
                Name = Guid.NewGuid().ToString(),
                IsDeleted = true
            }).ToList());

            var response = await factory.CreateClient().GetAsync("api/workouts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
            workouts.Should().NotBeNull();
            workouts?.Count().Should().Be(availableWorkouts);
        }

        #endregion
    }
}