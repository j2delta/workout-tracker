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

namespace WorkoutTracker.ViewModels
{
    public class WorkoutTrackerViewModel : INotifyPropertyChanged
    {
        #region Fields

        private int _numberOfExercises;

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

        //private bool _isEditEnabled;
        //public bool IsEditEnabled
        //{
        //    get { return _isEditEnabled; }
        //    set { _isEditEnabled = value; NotifyPropertyChanged(nameof(IsEditEnabled)); }
        //}

        #endregion Properties

        #region Commands

        public ICommand AddExerciseCommand => new Command(AddExercise);
        public ICommand EditExerciseCommand => new Command<Exercise>(EditExercise);
        public ICommand DeleteExerciseCommand => new Command<Exercise>(DeleteExercise);
        public ICommand SaveExerciseCommand => new Command<Exercise>(SaveExercise);

        #endregion Commands

        #region ctor

        public WorkoutTrackerViewModel()
        {
            _numberOfExercises = 0;

            Exercises = new ObservableCollection<Exercise>() {
                CreateNewExercise(),
                CreateNewExercise()
            };
        }

        #endregion ctor

        #region Methods

        public Exercise CreateNewExercise() // For testing
        {
            var exercise = new Exercise() { ExerciseId = _numberOfExercises, Name = "Pullups", NumberOfSets = 4, NumberOfReps = 5, Weight = 0, IsEditEnabled = false };
            _numberOfExercises++;

            return exercise;
        }

        public void AddExercise()
        {
            //TODO: check for empty exercise name, NumberOfSets and NumberOfReps
            _numberOfExercises++;
            Exercises.Add(new Exercise() {
                ExerciseId = _numberOfExercises, 
                Name = ExerciseName, 
                NumberOfSets = NumberOfSets, 
                NumberOfReps = NumberOfReps, 
                Weight = Weight, 
                IsEditEnabled = false,
                IsSaveVisible = false});

            ClearEntries();
        }

        public void EditExercise(Exercise currentExercise)
        {
            currentExercise.IsEditEnabled = true;
            currentExercise.IsSaveVisible = true;
        }

        public void DeleteExercise(Exercise currentExercise)
        {
            Exercises.Remove(currentExercise);
        }

        public void SaveExercise(Exercise currentExercise)
        {
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
