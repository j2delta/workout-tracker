using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Models
{
    public class ObservableExercise : INotifyPropertyChanged
    {
        public int ExerciseId { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private int _numberOfSets;
        public int NumberOfSets
        {
            get { return _numberOfSets; }
            set { _numberOfSets = value;  NotifyPropertyChanged(nameof(NumberOfSets)); }
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

        private bool _isEditEnabled;
        public bool IsEditEnabled
        {
            get { return _isEditEnabled; }
            set { _isEditEnabled = value; NotifyPropertyChanged(nameof(IsEditEnabled)); }
        }

        private bool _isSaveVisible;
        public bool IsSaveVisible
        {
            get { return _isSaveVisible; }
            set { _isSaveVisible = value; NotifyPropertyChanged(nameof(IsSaveVisible)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
