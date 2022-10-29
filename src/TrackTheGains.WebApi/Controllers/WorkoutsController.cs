using Microsoft.AspNetCore.Mvc;
using TrackTheGains.WebApi.Controllers.Dtos;
using TrackTheGains.WebApi.Models.Workout;
using TrackTheGains.WebApi.Services;

namespace TrackTheGains.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService service)
        {
            _workoutService = service;
        }

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutOverviewVm>>> GetWorkouts()
        {
            var workouts = await _workoutService.GetWorkoutOverviewsAsync();
            return Ok(workouts);
        }

        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutVm>> GetWorkout(Guid id)
        {
            var workout = await _workoutService.GetWorkoutByIdAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        // PUT: api/Workouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(Guid id, WorkoutVm workout)
        {
            if (id != workout.Id)
            {
                return BadRequest();
            }

            var result = await _workoutService.UpdateWorkoutAsync(workout);
            if(result is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Workouts
        [HttpPost]
        public async Task<ActionResult<Workout>> CreateWorkout(WorkoutVm workout)
        {
            await _workoutService.CreateWorkoutAsync(workout);
            return Created("GetWorkout", workout);
        }

        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(Guid id)
        {
            var workout = await _workoutService.DeleteWorkoutAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
