using SQLite;
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
        [PrimaryKey, AutoIncrement]
        public int WorkoutId { get; set; }
        public string Name{ get; set; }
    }
}
