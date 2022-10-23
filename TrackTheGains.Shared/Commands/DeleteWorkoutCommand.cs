using MediatR;

namespace TrackTheGains.Shared.Commands
{
    public class DeleteWorkoutCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
