using Microsoft.Extensions.Logging;
using Trip.App.ViewModels;
using Trip.Data;

namespace Trip.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<TripDbContext>();

            //Views
            builder.Services.AddTransient<MainPage>();

            //ViewModels
            builder.Services.AddTransient<MainPageViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
