using WorkoutTracker.ViewModels;

namespace WorkoutTracker.Views;

public partial class DoWorkoutPageView : ContentPage
{
	public DoWorkoutPageView(DoWorkoutPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}