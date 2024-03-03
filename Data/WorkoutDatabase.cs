using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            await Database.CreateTableAsync<WorkoutExerciseLink>().ConfigureAwait(false);
        }

        public async Task<List<Exercise>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Exercise>().ToListAsync().ConfigureAwait(false); ;
        }

        public async Task<Exercise> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Exercise>().Where(i => i.ExerciseId == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
        }

        public async Task<int> SaveItemAsync(Exercise item)
        {
            await Init();

            var existingRecord = await GetItemAsync(item.ExerciseId).ConfigureAwait(false);

            if (existingRecord != null)
            {
                return await Database.UpdateAsync(item).ConfigureAwait(false); ;
            }
            else
            {
                return await Database.InsertAsync(item).ConfigureAwait(false); ;
            }
        }

        public async Task<int> DeleteItemAsync(Exercise item)
        {
            await Init();
            return await Database.DeleteAsync(item).ConfigureAwait(false); ;
        }
    }
}
