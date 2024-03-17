using WorkoutTracker.ViewModels;

namespace WorkoutTracker
{
    public partial class ExercisesPageView : ContentPage
    {
        public ExercisesPageView(ExercisesPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
