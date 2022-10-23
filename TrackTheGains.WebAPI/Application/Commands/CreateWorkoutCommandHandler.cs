using MediatR;
using TrackTheGains.Domain.Models;
using TrackTheGains.Infrastructure.Repositories;
using TrackTheGains.Shared.Commands;

namespace TrackTheGains.WebAPI.Application.Commands
{
    public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, bool>
    {
        private readonly IWorkoutRepository _workoutRepository;

        public CreateWorkoutCommandHandler(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<bool> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
        {
            var workout = new Workout()
            {
                Name = request.Name
            };

            foreach (NewExcercise excerciseDto in request.Excercises)
            {
                var excercise = new Excercise()
                {
                    Name = excerciseDto.Name
                };

                workout.Excercises.Add(excercise);
            }

            await _workoutRepository.Add(workout);
            return true;
        }
    }
}
