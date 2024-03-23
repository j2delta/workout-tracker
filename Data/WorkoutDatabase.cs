using SQLite;
using WorkoutTracker.Models;

namespace WorkoutTracker.Data
{
    public class WorkoutDatabase
    {
        SQLiteAsyncConnection Database;
        public WorkoutDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<Workout>().ConfigureAwait(false);
            await Database.CreateTableAsync<Exercise>().ConfigureAwait(false);
            //await Database.CreateTableAsync<WorkoutExerciseLink>().ConfigureAwait(false);
        }

        public async Task<List<Exercise>> GetExercisesAsync(int workoutId)
        {
            await Init();

            return await Database.Table<Exercise>()
                .Where(i => i.WorkoutId == workoutId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<List<Workout>> GetWorkoutsAsync()
        {
            await Init();
            return await Database.Table<Workout>().ToListAsync().ConfigureAwait(false); ;
        }

        public async Task<Exercise> GetExerciseAsync(int workoutId, string name)
        {
            await Init();
            return await Database.Table<Exercise>()
                .Where(i => i.WorkoutId == workoutId && i.Name == name)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<Workout> GetWorkoutAsync(string name)
        {
            await Init();

            return await Database.Table<Workout>()
                .Where(i => i.Name == name)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        //public async Task<WorkoutExerciseLink> GetWorkoutExerciseLinkAsync(int workoutId, int exerciseId)
        //{
        //    await Init();
        //    return await Database.Table<WorkoutExerciseLink>().Where(i => i.WorkoutId == workoutId && i.ExerciseId == exerciseId).FirstOrDefaultAsync().ConfigureAwait(false);
        //}

        //public async Task InsertWorkoutExerciseLinkAsync(WorkoutExerciseLink workoutExerciseLink)
        //{
        //    await Init();

        //    var existingRecords = await GetWorkoutExerciseLinkAsync(workoutExerciseLink.WorkoutId, workoutExerciseLink.ExerciseId).ConfigureAwait(false);

        //    if (existingRecords != null) return;

        //    await Database.InsertAsync(workoutExerciseLink).ConfigureAwait(false);
        //} 

        public async Task SaveExerciseAsync(Exercise exercise)
        {
            await Init();

            var existingRecord = await GetExerciseAsync(exercise.WorkoutId, exercise.Name).ConfigureAwait(false);

            if (existingRecord != null)
            {
                await Database.UpdateAsync(exercise).ConfigureAwait(false);
            }
            else
            {
                await Database.InsertAsync(exercise).ConfigureAwait(false);
            }
        }

        public async Task<int> SaveWorkoutAsync(Workout workout)
        {
            await Init();

            var existingRecord = await GetWorkoutAsync(workout.Name).ConfigureAwait(false);

            if (existingRecord != null)
            {
                return await Database.UpdateAsync(workout).ConfigureAwait(false); ;
            }
            else
            {
                return await Database.InsertAsync(workout).ConfigureAwait(false); ;
            }
        }

        public async Task<int> DeleteItemAsync(Exercise exercise)
        {
            await Init();
            return await Database.DeleteAsync(exercise).ConfigureAwait(false); ;
        }
    }
}
