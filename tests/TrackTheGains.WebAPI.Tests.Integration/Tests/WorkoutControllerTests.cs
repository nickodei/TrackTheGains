using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TrackTheGains.WebApi.Controllers.Dtos;
using TrackTheGains.WebApi.Workouts;
using TrackTheGains.WebAPI.Tests.Integration;
using TrackTheGains.WebAPI.Tests.Integration.Extensions;
using Xunit;

namespace Workout_Spec
{
    public class WorkoutControllerTests: IClassFixture<WebApiFactory>
    {
        private readonly HttpClient client;
        private readonly WebApiFactory factory;

        private static string GetWorkoutById(Guid id) => $"api/workouts/{id}";

        public WorkoutControllerTests(WebApiFactory factory)
        {
            this.factory = factory;
            this.client = factory.CreateClient();
        }

        #region Get WorkoutOverviews Request

        [Fact]
        public async Task GetWorkoutOverviews_ReturnsNoWorkouts_WhenNonExist()
        {
            await factory.EnsureCleanDatabase();

            var response = await client.GetAsync("api/workouts");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
            workouts.Should().NotBeNull();
            workouts?.Count().Should().Be(0);
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
            workouts?.Count().Should().Be(1);
            workouts?.First().Name.Should().Be("push");
            workouts?.First().ExerciseAmount.Should().Be(0);
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

        #region Get Workout Request

        [Fact]
        public async Task GetWorkout_ReturnsNotFound_WhenWorkoutDoesNotExist()
        {
            await factory.EnsureCleanDatabase();

            var response = await client.GetAsync(GetWorkoutById(Guid.NewGuid()));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetWorkout_ReturnsNotFound_WhenWorkoutIsDeleted()
        {
            await factory.EnsureCleanDatabase();
            Workout workout = await factory.CreateWorkout(new Workout()
            {
                Name = "test",
                IsDeleted = true
            });

            var response = await client.GetAsync(GetWorkoutById(workout.Id));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetWorkout_ReturnsWorkout_WhenFound()
        {
            await factory.EnsureCleanDatabase();
            var workout = await factory.CreateWorkout(new Workout()
            {
                Name = "test",
                Exercises = new List<Exercise>()
                {
                    new Exercise() { Name = "ex1", Order = 2 },
                    new Exercise() { Name = "ex3", IsDeleted = true }
                }
            });

            var response = await client.GetAsync(GetWorkoutById(workout.Id));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<WorkoutVm>();
            result.Should().NotBeNull();
            result?.Name.Should().Be("test");
            result?.Exercises.Count.Should().Be(1);
            result?.Exercises[0].Name.Should().Be("ex1");
            result?.Exercises[0].OrderNr.Should().Be(2);
        }

        #endregion

        #region Create Workout Request

        [Fact]
        public async Task CreateWorkout_ReturnsWorkout_WhenRequestIsValid()
        {
            // Arrange
            await factory.EnsureCleanDatabase();

            // Act
            var response = await client.PostAsJsonAsync("api/workouts", new WorkoutVm()
            {
                Name = "test",
                Exercises = new List<ExerciseVm>()
                {
                    new ExerciseVm() { Name = "ex1", OrderNr = 1},
                    new ExerciseVm() { Name = "ex2", OrderNr = 2}
                }
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var workout = await factory.GetFirstWorkout();
            workout.Should().NotBeNull();
            workout?.Name.Should().Be("test");
            workout?.Exercises.Count.Should().Be(2);
            workout?.Exercises.Any(x => x.Name == "ex1" && x.Order == 1).Should().Be(true);
            workout?.Exercises.Any(x => x.Name == "ex2" && x.Order == 2).Should().Be(true);
        }

        #endregion
    }
}