using MediatR;
using TrackTheGains.Infrastructure.Repositories;
using TrackTheGains.Shared.Commands;

namespace TrackTheGains.WebAPI.Application.Commands
{
    public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand, bool>
    {
        private readonly IWorkoutRepository _workoutRepository;

        public DeleteWorkoutCommandHandler(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<bool> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
        {
            await _workoutRepository.Delete(request.Id);
            return true;
        }
    }
}
