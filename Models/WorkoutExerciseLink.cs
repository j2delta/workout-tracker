using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Models
{
    public class WorkoutExerciseLink
    {
        [PrimaryKey, AutoIncrement]
        public int WorkoutExerciseLinkId { get; set; }
        [ForeignKey(nameof(WorkoutId))]
        public int WorkoutId { get; set; }
        [ForeignKey(nameof(ExerciseId))]
        public int ExerciseId { get; set;}
    }
}
