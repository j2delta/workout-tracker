using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.ViewModels
{
    [QueryProperty(nameof(SelectedWorkoutId), "WorkoutId")]
    public class EditExercisesPageViewModel : INotifyPropertyChanged
    {
        private string _selectedWorkoutId;
        public string SelectedWorkoutId
        {
            get { return _selectedWorkoutId; }
            set { _selectedWorkoutId = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
