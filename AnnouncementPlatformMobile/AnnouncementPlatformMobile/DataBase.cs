using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementPlatformMobile
{
    public class DataBase
    {

        readonly SQLiteAsyncConnection _database;

        public DataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Announcement>().Wait();
            _database.CreateTableAsync<Lang>().Wait();
            _database.CreateTableAsync<Account>().Wait();
            _database.CreateTableAsync<Applied>().Wait();
        }
        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<int> SaveItemAsync<T>(T item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> DeleteItemAsync<T>(T item)
        {
            return _database.DeleteAsync(item);
        }
        public async Task ClearDatabaseAsync()
        {
            await _database.DropTableAsync<User>();
            await _database.CreateTableAsync<User>();
            await _database.DropTableAsync<Account>();
            await _database.CreateTableAsync<Account>();
            await _database.DropTableAsync<Lang>();
            await _database.CreateTableAsync<Lang>();
            await _database.DropTableAsync<Announcement>();
            await _database.CreateTableAsync<Announcement>();
            await _database.DropTableAsync<Applied>();
            await _database.CreateTableAsync<Applied>();
         
        }



    }

}
