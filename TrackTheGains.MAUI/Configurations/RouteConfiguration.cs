using TrackTheGains.MAUI.Views;

namespace TrackTheGains.MAUI.Extensions
{
    public static class RouteConfiguration
    {
        public static void RegisterRouting()
        {
            Routing.RegisterRoute("workout/overview", typeof(WorkoutOverviewPage));
            Routing.RegisterRoute("workout-template/edit", typeof(WorkoutEditPage));
        }
    }
}
