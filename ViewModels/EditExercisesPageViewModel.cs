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

namespace WorkoutTracker.ViewModels
{
    [QueryProperty(nameof(SelectedWorkoutId), "WorkoutId")]
    public class EditExercisesPageViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly WorkoutDatabase _workoutDatabase;

        #endregion Fields

        #region Properties

        public event PropertyChangedEventHandler? PropertyChanged;

        private int _selectedWorkoutId;
        public int SelectedWorkoutId
        {
            get { return _selectedWorkoutId; }
            set 
            { 
                _selectedWorkoutId = value;
                Task.Run(LoadExercisesAsync);
            }
        }

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

        public AsyncRelayCommand AddExerciseCommand => new AsyncRelayCommand(HandleException, AddExercise);
        public ICommand EditExerciseCommand => new Command<Exercise>(EditExercise);
        public AsyncRelayCommand<Exercise> DeleteExerciseCommand => new AsyncRelayCommand<Exercise>(HandleException, DeleteExercise);
        public AsyncRelayCommand<Exercise> SaveExerciseCommand => new AsyncRelayCommand<Exercise>(HandleException, SaveExercise);

        #endregion Commands

        #region ctor

        public EditExercisesPageViewModel(WorkoutDatabase workoutDatabase)
        {
            _workoutDatabase = workoutDatabase;
        }

        #endregion ctor

        #region Methods

        public async Task LoadExercisesAsync()
        {
            var exerciseList = await _workoutDatabase.GetExercisesAsync(SelectedWorkoutId).ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Exercises = new ObservableCollection<Exercise>(exerciseList);
            });
        }

        public async Task AddExercise()
        {
            //TODO: check for empty exercise name, NumberOfSets and NumberOfReps

            var exerciseToAdd = new Exercise()
            {
                WorkoutId = SelectedWorkoutId,
                Name = ExerciseName,
                NumberOfSets = NumberOfSets,
                NumberOfReps = NumberOfReps,
                Weight = Weight,
                IsEditEnabled = false,
                IsSaveVisible = false
            };

            ClearEntries();
            await _workoutDatabase.SaveExerciseAsync(exerciseToAdd).ConfigureAwait(false);
            await LoadExercisesAsync().ConfigureAwait(false);
        }

        public void EditExercise(Exercise currentExercise)
        {
            currentExercise.IsEditEnabled = true;
            currentExercise.IsSaveVisible = true;
        }

        public async Task DeleteExercise(Exercise currentExercise)
        {
            await _workoutDatabase.DeleteItemAsync(currentExercise).ConfigureAwait(false);
            await LoadExercisesAsync().ConfigureAwait(false);
        }

        public async Task SaveExercise(Exercise currentExercise)
        {
            var tempExercise = new Exercise()
            {
                WorkoutId = SelectedWorkoutId,
                ExerciseId = currentExercise.ExerciseId,
                Name = currentExercise.Name,
                NumberOfSets = currentExercise.NumberOfSets,
                NumberOfReps = currentExercise.NumberOfReps,
                Weight = currentExercise.Weight
            };

            await _workoutDatabase.SaveExerciseAsync(tempExercise).ConfigureAwait(false);

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

        public void HandleException(Exception ex)
        {
            Console.WriteLine(ex);
        }

        #endregion Methods
    }
}
