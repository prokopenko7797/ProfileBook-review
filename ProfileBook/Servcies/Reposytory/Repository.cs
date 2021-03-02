using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ProfileBook.Models;
using System.IO;
using System.Threading.Tasks;
using ProfileBook.Constants;

namespace ProfileBook.Servcies.Repository
{
    public class Repository<T> : IRepository<T> where T : IModel, new()
    {
        private readonly SQLiteAsyncConnection _database;

        public Repository()
        {
            _database = new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), Constant.DB_Name));
            _database.CreateTableAsync<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public Task<List<T>> Query(string query)
        {
            return _database.QueryAsync<T>(query);
        }

        public async Task<T> GetById(int id)
        {
            return await _database.GetAsync<T>(id);
        }

        public async Task<int> Delete(int id)
        {
            return await _database.DeleteAsync<T>(id);
        }

        public async Task<int> Insert(T item)
        {
            return await _database.InsertAsync(item);
        }

        public async Task<int> Update(T item)
        {
            return await _database.UpdateAsync(item);
        }

        public async Task<T> FindWithQuery(string query)
        {
            return await _database.FindWithQueryAsync<T>(query);
        }
    }
}
