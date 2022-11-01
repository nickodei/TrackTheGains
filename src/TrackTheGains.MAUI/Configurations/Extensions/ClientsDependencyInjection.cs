using TrackTheGains.MAUI.Services;

namespace TrackTheGains.MAUI.Configurations.Extensions
{
    public static class ClientsDependencyInjection
    {
        public const string BASE_URL = "http://10.0.2.2:5046";

        public static void AddClients(this IServiceCollection services)
        {
            services.AddScoped<IWorkoutsClient, WorkoutsClient>(x => new WorkoutsClient(BASE_URL));
        }
    }
}
