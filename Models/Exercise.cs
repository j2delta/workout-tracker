using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public int NumberOfSets{ get; set; }
        public int NumberOfReps{ get; set; }
        public int Weight {  get; set; }
    }
}
