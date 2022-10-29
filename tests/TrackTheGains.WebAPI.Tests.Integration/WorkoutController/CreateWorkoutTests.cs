namespace TrackTheGains.WebAPI.Tests.Integration.WorkoutController
{
    public class CreateWorkoutTests
    {
        private readonly HttpClient _client;

        public CreateWorkoutTests(WebApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        //[Fact]
        //public async Task Creating_A_Workout_Without_Excercises_Returns200()
        //{
        //    var command = new CreateWorkoutCommand()
        //    {
        //        Name = "push",
        //        Excercises = new List<NewExcercise>()
        //    };

        //    var response = await _client.PostAsJsonAsync("api/workout", command);

        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        //[Fact]
        //public async Task Creating_A_Workout_With_Excercises_Returns200()
        //{
        //    var command = new CreateWorkoutCommand()
        //    {
        //        Name = "push",
        //        Excercises = new List<NewExcercise>()
        //        {
        //            new NewExcercise() { Name = "test1" }
        //        }
        //    };

        //    var response = await _client.PostAsJsonAsync("api/workout", command);

        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
    }
}
