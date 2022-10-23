using UraniumUI;
using CommunityToolkit.Maui;
using TrackTheGains.MAUI.Extensions;
using TrackTheGains.MAUI.Configurations.Extensions;

namespace TrackTheGains.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
            })
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddUraniumUIHandlers();
            })
            .UseMauiCommunityToolkit();

            builder.Services.AddViews();
            builder.Services.AddClients();
            builder.Services.AddViewModels();

            RouteConfiguration.RegisterRouting();

            return builder.Build();
        }
    }
}