using Microsoft.Extensions.Logging;
using WorkoutTracker.Data;
using WorkoutTracker.ViewModels;
using WorkoutTracker;

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

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<WorkoutTrackerViewModel>();

            builder.Services.AddTransient<ExercisesPageView>();
            builder.Services.AddTransient<ExercisesPageViewModel>();

            builder.Services.AddTransient<EditExercisesPageView>();
            builder.Services.AddTransient<EditExercisesPageViewModel>();

            builder.Services.AddTransient<WorkoutsPageView>();
            builder.Services.AddTransient<WorkoutsPageViewModel>();

            builder.Services.AddTransient<NewWorkoutPageView>();
            builder.Services.AddTransient<NewWorkoutPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
