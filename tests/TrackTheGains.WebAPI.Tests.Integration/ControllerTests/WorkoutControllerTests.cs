using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TrackTheGains.Shared.Models.Workouts;
using TrackTheGains.WebApi.Workouts;
using TrackTheGains.WebAPI.Tests.Integration;
using TrackTheGains.WebAPI.Tests.Integration.Extensions;
using Xunit;

namespace Workout_Specification;

[Collection(SharedTestCollection.COLLECTION_DEFINITION)]
public class WorkoutControllerTests : IAsyncLifetime
{
    private readonly WebApiFactory factory;

    private static string GetWorkoutById(Guid id) => $"api/workouts/{id}";

    public WorkoutControllerTests(WebApiFactory factory)
    {
        this.factory = factory;
    }

    #region Get WorkoutOverviews Request

    [Fact]
    public async Task GetWorkoutOverviews_ReturnsNoWorkouts_WhenNonExist()
    {
        var response = await factory.HttpClient.GetAsync("api/workouts");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
        workouts.Should().NotBeNull();
        workouts?.Count().Should().Be(0);
    }

    [Fact]
    public async Task GetWorkoutOverviews_ReturnsWorkoutWithoutExercises_WhenNoExercisesWhereCreated()
    {
        await factory.CreateWorkouts(new() { new Workout() { Name = "push" } });

        var response = await factory.HttpClient.GetAsync("api/workouts");
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
        await factory.CreateWorkout("testWorkout", availableExercises, deletedExercises);

        var response = await factory.HttpClient.GetAsync("api/workouts");

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

        var response = await factory.HttpClient.GetAsync("api/workouts");

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
        var response = await factory.HttpClient.GetAsync(GetWorkoutById(Guid.NewGuid()));
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWorkout_ReturnsNotFound_WhenWorkoutIsDeleted()
    {
        Workout workout = await factory.CreateWorkout(new Workout()
        {
            Name = "test",
            IsDeleted = true
        });

        var response = await factory.HttpClient.GetAsync(GetWorkoutById(workout.Id));
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWorkout_ReturnsWorkout_WhenFound()
    {
        var workout = await factory.CreateWorkout(new Workout()
        {
            Name = "test",
            Exercises = new List<Exercise>()
            {
                new Exercise() { Name = "ex1", Order = 2 },
                new Exercise() { Name = "ex3", IsDeleted = true }
            }
        });

        var response = await factory.HttpClient.GetAsync(GetWorkoutById(workout.Id));
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

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => factory.ResetDatabaseAsync();

    #endregion
}