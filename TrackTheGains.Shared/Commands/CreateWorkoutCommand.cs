using MediatR;

namespace TrackTheGains.Shared.Commands
{
    public class CreateWorkoutCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public List<NewExcercise> Excercises { get; set; }

    }

    public record NewExcercise
    {
        public string Name { get; set; }
    }
}