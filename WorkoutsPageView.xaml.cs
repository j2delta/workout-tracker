using WorkoutTracker.ViewModels;

namespace WorkoutTracker;

public partial class WorkoutsPageView : ContentPage
{
	public WorkoutsPageView(WorkoutsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}