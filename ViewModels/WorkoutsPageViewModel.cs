using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutTracker.Data;

namespace WorkoutTracker.ViewModels
{
    public class WorkoutsPageViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly WorkoutDatabase _workoutDatabase;

        #endregion Fields

        #region Properies

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Properties

        #region Commands

        public ICommand GoToExercisesCommand => new Command(GoToExercisesPage);

        #endregion Commands

        #region ctor

        public WorkoutsPageViewModel(WorkoutDatabase workoutDatabase)
        {
            _workoutDatabase = workoutDatabase;
                
        }

        #endregion ctor


        #region Methods

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void GoToExercisesPage()
        {
            Task.Run(async () => await Shell.Current.GoToAsync(nameof(MainPage)));
        }

        #endregion Methods
    }
}
