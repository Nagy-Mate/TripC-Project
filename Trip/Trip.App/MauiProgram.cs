using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Trip.App.ViewModels;
using Trip.App.Views;
using Trip.Data;
using Trip.Services;
using Trip.Services.Interfaces;

namespace Trip.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<TripDbContext>();

            //Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddTripPage>();
            builder.Services.AddTransient<AddDestinationPage>();
            builder.Services.AddTransient<UpdateTripPage>();
            builder.Services.AddTransient<DeleteTripPage>();

            //ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<AddTripPageViewModel>();
            builder.Services.AddTransient<AddDestinationPageViewModel>();
            builder.Services.AddTransient<UpdateTripPageViewModel>();
            builder.Services.AddTransient<DeleteTripPageViewModel>();

            //Services
            builder.Services.AddTransient<ITripService, TripService>();
            builder.Services.AddTransient<IDestinationService, DestinationService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}