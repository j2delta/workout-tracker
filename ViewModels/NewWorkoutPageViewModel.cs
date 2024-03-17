using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Commands;
using WorkoutTracker.Data;
using WorkoutTracker.Models;

namespace WorkoutTracker.ViewModels
{
    public class NewWorkoutPageViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly WorkoutDatabase _workoutDatabase;
        #endregion Fields

        #region Properties
        private string _workoutName;

        public string WorkoutName
        {
            get { return _workoutName; }
            set { _workoutName = value; NotifyPropertyChanged(nameof(WorkoutName)); }
        }

        #endregion Properties

        #region Commands
        public AsyncRelayCommand SaveWorkoutCommand => new AsyncRelayCommand(HandleException, SaveWorkout);
        #endregion Commands

        public event PropertyChangedEventHandler? PropertyChanged;

        public NewWorkoutPageViewModel(WorkoutDatabase workoutDatabase)
        {
            _workoutDatabase = workoutDatabase;
        }

        #region Methods
        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task SaveWorkout()
        {
            var workout = new Workout()
            {
                Name = WorkoutName
            };

            await _workoutDatabase.SaveWorkoutAsync(workout);
            await Shell.Current.GoToAsync("..");
        }

        public void HandleException(Exception ex)
        {
            Console.WriteLine(ex);
        }
        #endregion Methods
    }
}
