//using Android.Health.Connect.DataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutTracker.Models;
using SQLite;
using Constants = WorkoutTracker.Constants;
using WorkoutTracker.Data;

namespace WorkoutTracker.ViewModels
{
    public class WorkoutTrackerViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly WorkoutDatabase _workoutDatabase;

        #endregion Fields

        #region Properties

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Exercise> _exercises;
        public ObservableCollection<Exercise> Exercises
        {
            get { return _exercises; }
            set
            {
                _exercises = value;
                NotifyPropertyChanged(nameof(Exercises));
            }
        }

        private string _exerciseName;
        public string ExerciseName
        {
            get { return _exerciseName; }
            set { _exerciseName = value; NotifyPropertyChanged(nameof(ExerciseName)); }
        }

        private int _numberOfSets;
        public int NumberOfSets
        {
            get { return _numberOfSets; }
            set { _numberOfSets = value; NotifyPropertyChanged(nameof(NumberOfSets)); }
        }

        private int _numberOfReps;
        public int NumberOfReps
        {
            get { return _numberOfReps; }
            set { _numberOfReps = value; NotifyPropertyChanged(nameof(NumberOfReps)); }
        }

        private int _weight;
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; NotifyPropertyChanged(nameof(Weight)); }
        }

        #endregion Properties

        #region Commands

        public ICommand AddExerciseCommand => new Command(AddExercise);
        public ICommand EditExerciseCommand => new Command<Exercise>(EditExercise);
        public ICommand DeleteExerciseCommand => new Command<Exercise>(DeleteExercise);
        public ICommand SaveExerciseCommand => new Command<Exercise>(SaveExercise);

        #endregion Commands

        #region ctor

        public WorkoutTrackerViewModel(WorkoutDatabase workoutDatabase)
        {
            _workoutDatabase = workoutDatabase;

            //Exercises = new ObservableCollection<Exercise>() {
            //    CreateNewExercise(),
            //    CreateNewExercise()
            //};

            Task.Run(LoadExercisesAsync);
        }

        #endregion ctor

        #region Methods

        public async Task LoadExercisesAsync()
        {
            //Implement IsBusyLoading functionality
            //Maybe use this https://stackoverflow.com/questions/75327214/initialize-async-data-in-viewmodel-net-maui
            //And this https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
            var exerciseList = await _workoutDatabase.GetExercisesAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Exercises = new ObservableCollection<Exercise>(exerciseList);
            });
        }

        public void AddExercise()
        {
            //TODO: check for empty exercise name, NumberOfSets and NumberOfReps

            var exerciseToAdd = new Exercise()
            {
                Name = ExerciseName,
                NumberOfSets = NumberOfSets,
                NumberOfReps = NumberOfReps,
                Weight = Weight,
                IsEditEnabled = false,
                IsSaveVisible = false
            };

            ClearEntries();
            _workoutDatabase.SaveExerciseAsync(exerciseToAdd).GetAwaiter().GetResult();
            LoadExercisesAsync().GetAwaiter().GetResult();
        }

        public void EditExercise(Exercise currentExercise)
        {
            currentExercise.IsEditEnabled = true;
            currentExercise.IsSaveVisible = true;
        }

        public void DeleteExercise(Exercise currentExercise)
        {
            _workoutDatabase.DeleteItemAsync(currentExercise).GetAwaiter().GetResult();
            LoadExercisesAsync().GetAwaiter().GetResult();
        }

        public void SaveExercise(Exercise currentExercise)
        {
            var tempExercise = new Exercise() {
                ExerciseId = currentExercise.ExerciseId,
                Name = currentExercise.Name,
                NumberOfSets = currentExercise.NumberOfSets,
                NumberOfReps = currentExercise.NumberOfReps,
                Weight= currentExercise.Weight
            };

            _workoutDatabase.SaveExerciseAsync(tempExercise).GetAwaiter().GetResult();

            currentExercise.IsEditEnabled = false;
            currentExercise.IsSaveVisible = false;
        }

        public void ClearEntries()
        {
            ExerciseName = "";
            NumberOfSets = 0;
            NumberOfReps = 0;
            Weight = 0;
        }

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion Methods
    }
}
