using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Models
{
    public class Workout
    {
        public string Name{ get; set; }
        public ObservableCollection<ObservableExercise> Exercises { get; set; }
    }
}
