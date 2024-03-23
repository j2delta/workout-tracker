using Microsoft.Extensions.Logging;
using WorkoutTracker.Data;
using WorkoutTracker.ViewModels;
using WorkoutTracker;
using WorkoutTracker.Views;

namespace WorkoutTracker
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

            builder.Services.AddSingleton<WorkoutDatabase>();

            //builder.Services.AddSingleton<MainPage>();

            builder.Services.AddTransient<EditExercisesPageView>();
            builder.Services.AddTransient<EditExercisesPageViewModel>();

            builder.Services.AddTransient<WorkoutsPageView>();
            builder.Services.AddTransient<WorkoutsPageViewModel>();

            builder.Services.AddTransient<NewWorkoutPageView>();
            builder.Services.AddTransient<NewWorkoutPageViewModel>();

            builder.Services.AddTransient<DoWorkoutPageView>();
            builder.Services.AddTransient<DoWorkoutPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
