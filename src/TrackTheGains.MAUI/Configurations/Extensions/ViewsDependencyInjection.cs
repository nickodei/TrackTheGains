using TrackTheGains.MAUI.Views;

namespace TrackTheGains.MAUI.Configurations.Extensions
{
    public static class ViewsDependencyInjection
    {
        public static void AddViews(this IServiceCollection services)
        {
            services.AddTransient<WorkoutOverviewPage>();
            services.AddTransient<WorkoutEditPage>();
        }
    }
}
