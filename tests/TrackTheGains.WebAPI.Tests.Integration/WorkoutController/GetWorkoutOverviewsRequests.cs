using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using TrackTheGains.WebApi.Controllers.Dtos;
using TrackTheGains.WebApi.Models.Workout;
using TrackTheGains.WebAPI.Tests.Integration;

namespace Workout_Spec
{













    public class When_No_Workout_Exists : ApiTestClass
    {
        [Test]
        public async Task Then_An_Empty_List_With_Result_200_Is_Returned()
        {
            var result = await Factory.CreateClient().GetAsync("api/workouts");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

    public class When_An_Workout_Exists : ApiTestClass
    {
        [TestCase(0, 0)]
        [TestCase(5, 0)]
        [TestCase(0, 7)]
        [TestCase(2, 8)]
        public async Task Then_The_Exercise_With_The_Correct_Amount_Of_Exercises_And_Result_200_Is_Returned(int availableExercises, int deletedExercises)
        {
            await Factory.CreateWorkout("testWorkout", availableExercises, deletedExercises);

            var response = await Factory.CreateClient().GetAsync("api/workouts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
            workouts.Should().NotBeNull();
            workouts?.Count().Should().Be(1);
            workouts?.First().Name.Should().Be("testWorkout");
            workouts?.First().ExerciseAmount.Should().Be(availableExercises);
        }
    }

    public class When_Multiple_Workouts_Exists : ApiTestClass
    {
        [TestCase(0, 0)]
        [TestCase(5, 0)]
        [TestCase(0, 7)]
        [TestCase(2, 8)]
        public async Task Then_The_Correct_Amount_Of_Workouts_And_Result_200_Is_Returned(int availableWorkouts, int deletedWorkouts)
        {
            await Factory.CreateWorkouts(Enumerable.Range(0, availableWorkouts).Select(_ => new Workout()
            {
                Name = Guid.NewGuid().ToString(),
                IsDeleted = false
            }).ToList());

            await Factory.CreateWorkouts(Enumerable.Range(0, deletedWorkouts).Select(_ => new Workout()
            {
                Name = Guid.NewGuid().ToString(),
                IsDeleted = true
            }).ToList());

            var response = await Factory.CreateClient().GetAsync("api/workouts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workouts = await response.Content.ReadFromJsonAsync<IEnumerable<WorkoutOverviewVm>>();
            workouts.Should().NotBeNull();
            workouts?.Count().Should().Be(availableWorkouts);
        }
    }
}