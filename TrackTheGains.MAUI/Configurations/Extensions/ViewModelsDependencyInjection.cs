using TrackTheGains.MAUI.ViewModels;

namespace TrackTheGains.MAUI.Configurations.Extensions
{
    public static class ViewModelsDependencyInjection
    {
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<WorkoutOverviewViewModel>();
            services.AddTransient<WorkoutEditViewModel>();
        }
    }
}
