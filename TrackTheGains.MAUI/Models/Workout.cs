using System.Collections.ObjectModel;

namespace TrackTheGains.MAUI.Models
{
    public class Workout
    {
        public string Name { get; set; }
        public ObservableCollection<Excercise> Excercises { get; } = new();
    }
}
