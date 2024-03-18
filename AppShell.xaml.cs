using WorkoutTracker.ViewModels;

namespace WorkoutTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(WorkoutsPageView), typeof(WorkoutsPageView));
            Routing.RegisterRoute(nameof(ExercisesPageView), typeof(ExercisesPageView));
            Routing.RegisterRoute(nameof(NewWorkoutPageView), typeof(NewWorkoutPageView));
            Routing.RegisterRoute(nameof(EditExercisesPageView), typeof(EditExercisesPageView));
        }
    }
}
