using Microsoft.AspNetCore.Mvc;
using TrackTheGains.WebApi.WorkoutSessions;

namespace TrackTheGains.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutSessionsController : ControllerBase
{
    private readonly IWorkoutSessionService _service;

    public WorkoutSessionsController(IWorkoutSessionService service)
    {
        this._service = service;
    }

    // POST: api/WorkoutSessions
    [HttpPost]
    public async Task<ActionResult> CreateWorkoutSession(WorkoutSessionCreateDTO request)
    {
        var result = await _service.CreateWorkoutSessionAsync(request);
        return Created("GetWorkoutSession", result);
    }
}