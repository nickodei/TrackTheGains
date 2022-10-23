using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrackTheGains.Shared.Commands;
using TrackTheGains.Shared.Queries;
using TrackTheGains.WebAPI.Application.Commands;
using TrackTheGains.WebAPI.Application.Queries;

namespace TrackTheGains.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WorkoutController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkoutOverviewVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetWorkoutsAsync()
        {
            var workouts = await _mediator.Send(new GetWorkoutsQuery());
            return Ok(workouts);
        }

        [HttpGet("{workoutId:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(WorkoutVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetFullWorkoutAsync(Guid workoutId)
        {
            var workout = await _mediator.Send(new GetFullWorkoutQuery() { Id = workoutId });
            if(workout is not null)
            {
                return Ok(workout);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateWorkoutAsync([FromBody] CreateWorkoutCommand createWorkoutCommand)
        {
            await _mediator.Send(createWorkoutCommand);
            return Ok();
        }

        [HttpDelete("{workoutId:Guid}")]
        public async Task<ActionResult> Delete(Guid workoutId)
        {
            await _mediator.Send(new DeleteWorkoutCommand() { Id = workoutId });
            return Ok();
        }
    }
}
