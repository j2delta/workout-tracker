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
            var result = await Database.CreateTableAsync<Exercise>();
        }

        public async Task<List<Exercise>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Exercise>().ToListAsync();
        }

        public async Task<Exercise> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Exercise>().Where(i => i.ExerciseId == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Exercise item)
        {
            await Init();
            if (item.ExerciseId != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(Exercise item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
