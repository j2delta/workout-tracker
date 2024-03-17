using WorkoutTracker.ViewModels;

namespace WorkoutTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage(WorkoutTrackerViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}