using DailyManager.Web.Client.Services;

namespace TrackTheGains.MAUI.Configurations.Extensions
{
    public static class ClientsDependencyInjection
    {
        public const string BASE_URL = "http://10.0.2.2:5046";

        public static void AddClients(this IServiceCollection services)
        {
            services.AddScoped<IWorkoutClient, WorkoutClient>(x => new WorkoutClient(BASE_URL));
        }
    }
}
