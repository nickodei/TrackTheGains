namespace TrackTheGains.WebAPI.Tests.Integration.WorkoutController
{
    public class GetWorkoutTests 
    {
        private readonly WebApiFactory _factory;

        public GetWorkoutTests(WebApiFactory factory)
        {
            _factory = factory;
        }

        //[Fact]
        //public async Task Getting_A_Workout_Without_Excercises_Returns_The_Object()
        //{
        //    //Arrage
        //    Guid id;
        //    using(var scope = _factory.GetServiceScope)
        //    {
        //        IWorkoutRepository workoutRepository = scope.ServiceProvider.GetService<IWorkoutRepository>();
        //        id = await workoutRepository.AddAsync(new Workout()
        //        {
        //            Name = "test"
        //        });
        //    } 

        //    var response = await _factory.CreateClient().GetAsync($"api/workout/{id}");

        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var workout = await response.Content.ReadFromJsonAsync<WorkoutVM>();
        //    workout.Should().NotBeNull();
        //    workout?.Name.Should().Be("test");
        //    workout?.Excercises.Count().Should().Be(0);
        //}

        //[Fact]
        //public async Task Getting_A_Workout_With_Excercises_Returns_The_Object()
        //{
        //    //Arrage
        //    Guid id;
        //    using (var scope = _factory.GetServiceScope)
        //    {
        //        IWorkoutRepository workoutRepository = scope.ServiceProvider.GetService<IWorkoutRepository>();
        //        id = await workoutRepository.AddAsync(new Workout()
        //        {
        //            Name = "test",
        //            Excercises = new List<Excercise>()
        //            {
        //                new Excercise() { Name = "excercise1" },
        //                new Excercise() { Name = "excercise2" },
        //                new Excercise() { Name = "excercise3" },
        //            }
        //        });
        //    }

        //    var response = await _factory.CreateClient().GetAsync($"api/workout/{id}");

        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var workout = await response.Content.ReadFromJsonAsync<WorkoutVM>();
        //    workout.Should().NotBeNull();
        //    workout?.Name.Should().Be("test");
        //    workout?.Excercises.Count().Should().Be(3);
        //    workout?.Excercises.Any(x => x.Name.Equals("excercise1"));
        //    workout?.Excercises.Any(x => x.Name.Equals("excercise2"));
        //    workout?.Excercises.Any(x => x.Name.Equals("excercise3"));
        //}
    }
}
