using WorkoutTracker.ViewModels;
using WorkoutTracker.Views;

namespace WorkoutTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(WorkoutsPageView), typeof(WorkoutsPageView));
            Routing.RegisterRoute(nameof(NewWorkoutPageView), typeof(NewWorkoutPageView));
            Routing.RegisterRoute(nameof(DoWorkoutPageView), typeof(DoWorkoutPageView));
            Routing.RegisterRoute(nameof(EditExercisesPageView), typeof(EditExercisesPageView));
        }
    }
}
