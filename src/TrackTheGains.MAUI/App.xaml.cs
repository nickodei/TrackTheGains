using TrackTheGains.MAUI.Models;

namespace TrackTheGains.MAUI
{
    public partial class App : Application
    {
        public static List<Workout> Workouts = new List<Workout>();

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}