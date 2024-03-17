﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutTracker.Commands;
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

        public AsyncRelayCommand GoToExercisesCommand => new AsyncRelayCommand(HandleException, GoToExercisesPage);

        public AsyncRelayCommand GoToNewWorkoutCommand => new AsyncRelayCommand(HandleException, GoToNewWorkoutPage);

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

        public async Task GoToExercisesPage()
        {
            await Shell.Current.GoToAsync(nameof(ExercisesPageView));
        }

        public async Task GoToNewWorkoutPage()
        {
            await Shell.Current.GoToAsync(nameof(NewWorkoutPageView));
        }

        public void HandleException(Exception ex)
        {
            Console.WriteLine(ex);
        }

        #endregion Methods
    }
}
