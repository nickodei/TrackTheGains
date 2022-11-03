using System.Net.Http.Json;
using System.Net;
using TrackTheGains.Shared.Models.Workouts;
using TrackTheGains.WebAPI.Tests.Integration.Extensions;
using Xunit;
using FluentAssertions;
using TrackTheGains.WebAPI.Tests.Integration;

namespace Workout_Specification;

[Collection(SharedTestCollection.COLLECTION_DEFINITION)]
public class CreateWorkoutControllerTests : IAsyncLifetime
{
    private readonly WebApiFactory factory;
    public CreateWorkoutControllerTests(WebApiFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task CreateWorkout_ReturnsWorkout_WhenRequestIsValid()
    {
        //Arrange
        var newWorkout = new WorkoutVm()
        {
            Name = "test",
            Exercises = new List<ExerciseVm>()
            {
                new ExerciseVm() { Name = "ex1", OrderNr = 1},
                new ExerciseVm() { Name = "ex2", OrderNr = 2}
            }
        };

        //Act
        var response = await factory.HttpClient.PostAsJsonAsync("api/workouts", newWorkout);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var workout = await factory.GetFirstWorkout();
        workout.Should().NotBeNull();
        workout?.Name.Should().Be("test");
        workout?.Exercises.Count.Should().Be(2);
        workout?.Exercises.Any(x => x.Name == "ex1" && x.Order == 1).Should().Be(true);
        workout?.Exercises.Any(x => x.Name == "ex2" && x.Order == 2).Should().Be(true);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => factory.ResetDatabaseAsync();
}
