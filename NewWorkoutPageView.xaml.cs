using WorkoutTracker.ViewModels;

namespace WorkoutTracker;

public partial class NewWorkoutPageView : ContentPage
{
	public NewWorkoutPageView(NewWorkoutPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}