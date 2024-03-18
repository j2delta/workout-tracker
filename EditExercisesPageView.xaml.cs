using WorkoutTracker.ViewModels;

namespace WorkoutTracker;

public partial class EditExercisesPageView : ContentPage
{
	public EditExercisesPageView(EditExercisesPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}