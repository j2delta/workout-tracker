using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutTracker.Commands;
using WorkoutTracker.Data;
using WorkoutTracker.Models;
using WorkoutTracker.Views;

namespace WorkoutTracker.ViewModels
{
    public class WorkoutsPageViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly WorkoutDatabase _workoutDatabase;

        #endregion Fields

        #region Properies

        private ObservableCollection<Workout> _workouts;
        public ObservableCollection<Workout> Workouts
        {
            get { return _workouts; }
            set { _workouts = value; NotifyPropertyChanged(nameof(Workouts)); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private Workout _selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return _selectedWorkout; }
            set { _selectedWorkout = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Properties

        #region Commands

        public AsyncRelayCommand GoToEditExercisesCommand => new AsyncRelayCommand(HandleException, GoToEditExercisesPage);
        public AsyncRelayCommand GoToNewWorkoutCommand => new AsyncRelayCommand(HandleException, GoToNewWorkoutPage);
        public AsyncRelayCommand GoToDoWorkoutCommand => new AsyncRelayCommand(HandleException, GoToDoWorkoutPage);

        #endregion Commands

        #region ctor

        public WorkoutsPageViewModel(WorkoutDatabase workoutDatabase)
        {
            _workoutDatabase = workoutDatabase;

            Task.Run(LoadWorkoutsAsync);
        }

        #endregion ctor


        #region Methods

        public async Task LoadWorkoutsAsync()
        {
            var workoutList = await _workoutDatabase.GetWorkoutsAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Workouts = new ObservableCollection<Workout>(workoutList);
            });
        }

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task GoToEditExercisesPage()
        {
            await Shell.Current.GoToAsync($"{ nameof(EditExercisesPageView)}?WorkoutId={SelectedWorkout.WorkoutId}");
        }

        public async Task GoToNewWorkoutPage()
        {
            await Shell.Current.GoToAsync(nameof(NewWorkoutPageView));
        }

        public async Task GoToDoWorkoutPage()
        {
            if (SelectedWorkout == null)
            {
                //await DisplayAlert("Alert", "Please select a workout to do", "Cancel");
                await Application.Current.MainPage.DisplayAlert("Alert", "Please select a workout to do", "Cancel");
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(DoWorkoutPageView)}?WorkoutId={SelectedWorkout.WorkoutId}");
        }

        public void HandleException(Exception ex)
        {
            Console.WriteLine(ex);
        }

        #endregion Methods
    }
}
